using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.Format.ObjectModel;
using Vim.Util;

namespace Vim.Format.Levels
{
    public static class LevelInfoService
    {
        /// <summary>
        /// Returns an array of LevelInfo objects representing harmonized information about the levels in the given VIM Scene (see comment in LevelInfo.cs)
        /// </summary>
        public static (LevelInfo[], ElementLevelInfo[]) GetLevelInfos(
            FileInfo vimFileInfo,
            string[] stringTable = null,
            ElementGeometryMap elementGeometryMap = null)
        {
            elementGeometryMap = elementGeometryMap ?? new ElementGeometryMap(vimFileInfo);

            var tableSet = new EntityTableSet(
                vimFileInfo,
                stringTable,
                n =>
                    n is TableNames.Element ||
                    n is TableNames.Level ||
                    n is TableNames.FamilyInstance ||
                    n is TableNames.FamilyType ||
                    n is TableNames.Parameter ||
                    n is TableNames.BasePoint ||
                    n is TableNames.ParameterDescriptor);

            var elementIndexMaps = tableSet.ElementIndexMaps;
            var elementTable = tableSet.ElementTable;
            var parameterTable = tableSet.ParameterTable;
            var familyInstanceTable = tableSet.FamilyInstanceTable;
            var familyTypeTable = tableSet.FamilyTypeTable;
            var basePointTable = tableSet.BasePointTable;
            var levelTable = tableSet.LevelTable;

            var levels = levelTable.ToArray();
            var levelsByBimDocumentIndexAndElementId = levels.GroupByBimDocumentIndexAndElementId(elementTable);
            var basePointsByBimDocumentIndexAndElementId = basePointTable.GroupByBimDocumentIndexAndElementId(elementTable);

            var levelInfos = CreateLevelInfos(
                levels,
                elementTable,
                familyTypeTable,
                parameterTable,
                elementIndexMaps,
                levelsByBimDocumentIndexAndElementId,
                basePointsByBimDocumentIndexAndElementId);

            var levelInfoByBimDocumentIndex = levelInfos.GroupByBimDocumentIndexAndElementId(elementTable);

            var levelInfoMap = levelInfos.ToDictionaryIgnoreDuplicates(li => li.Level.Index, li => li);

            PatchBuildingStoryAbove(levelInfoByBimDocumentIndex);

            var elementLevelInfos = CreateElementLevelInfos(elementTable,
                familyInstanceTable,
                parameterTable,
                levelTable,
                elementIndexMaps,
                elementGeometryMap,
                levelInfoMap, levelInfoByBimDocumentIndex);

            return (levelInfos, elementLevelInfos);
        }

        /// <summary>
        /// Instantiates the level infos in parallel based on the given list of levels.
        /// </summary>
        private static LevelInfo[] CreateLevelInfos(
            IReadOnlyList<Level> levels,
            ElementTable elementTable,
            FamilyTypeTable familyTypeTable,
            ParameterTable parameterTable,
            ElementIndexMaps elementIndexMaps,
            IReadOnlyDictionary<int, Dictionary<long, Level>> levelsByBimDocumentIndexAndElementId,
            IReadOnlyDictionary<int, Dictionary<long, BasePoint>> basePointsByBimDocumentIndexAndElementId)
            => levels
                .AsParallel()
                .Select(level =>
                {
                    var bimDocumentIndex = elementTable.GetBimDocumentIndex(level.GetElementIndexOrNone());

                    if (!levelsByBimDocumentIndexAndElementId.TryGetValue(bimDocumentIndex, out var elementIdToLevelMap))
                        elementIdToLevelMap = new Dictionary<long, Level>();

                    if (!basePointsByBimDocumentIndexAndElementId.TryGetValue(bimDocumentIndex, out var elementIdToBasePointMap))
                        elementIdToBasePointMap = new Dictionary<long, BasePoint>();

                    return new LevelInfo(
                        level,
                        familyTypeTable,
                        parameterTable,
                        elementIndexMaps,
                        elementIdToLevelMap,
                        elementIdToBasePointMap);
                })
                .ToArray();

        /// <summary>
        /// Populates the LevelInfo.BuildingStoryAbove property if it is null.
        /// </summary>
        private static void PatchBuildingStoryAbove(IReadOnlyDictionary<int, Dictionary<long, LevelInfo>> levelInfoByBimDocumentIndex)
        {
            foreach (var (_, levelInfosInBimDocument) in levelInfoByBimDocumentIndex)
            {
                var levelInfosOrderedByProjectElevation = levelInfosInBimDocument.Values.OrderBy(l => l.Level.ProjectElevation).ToArray();

                foreach (var levelInfo in levelInfosOrderedByProjectElevation)
                {
                    if (levelInfo.HasBuildingStoryAbove)
                        continue;

                    levelInfo.BuildingStoryAbove = levelInfosOrderedByProjectElevation
                        .FirstOrDefault(other => other.IsBuildingStory && other.Level.ProjectElevation > levelInfo.Level.ProjectElevation)
                        ?.Level;
                }
            }
        }

        /// <summary>
        /// Returns an array of element level infos based on the given list of elements.
        /// </summary>
        private static ElementLevelInfo[] CreateElementLevelInfos(
            ElementTable elementTable,
            FamilyInstanceTable familyInstanceTable,
            ParameterTable parameterTable,
            LevelTable levelTable,
            ElementIndexMaps elementIndexMaps,
            ElementGeometryMap elementGeometryMap,
            IReadOnlyDictionary<int, LevelInfo> levelInfoMap,
            IReadOnlyDictionary<int, Dictionary<long, LevelInfo>> levelInfoByBimDocumentIndex)
        {
            var levelInfoByBimDocumentIndexOrdered = levelInfoByBimDocumentIndex.ToDictionary(
                kv => kv.Key,
                kv => kv.Value.Values.OrderBy(li => li.Level.ProjectElevation).ToArray());

            return elementTable
                .AsParallel()
                .Select(e =>
                {
                    var bimDocumentIndex = elementTable.GetBimDocumentIndex(e.Index);

                    if (!levelInfoByBimDocumentIndexOrdered.TryGetValue(bimDocumentIndex, out var orderedLevelInfosByProjectElevation))
                        orderedLevelInfosByProjectElevation = Array.Empty<LevelInfo>();

                    if (!levelInfoByBimDocumentIndex.TryGetValue(bimDocumentIndex, out var elementIdToLevelInfoMap))
                        elementIdToLevelInfoMap = new Dictionary<long, LevelInfo>();

                    return new ElementLevelInfo(
                        e,
                        elementTable,
                        familyInstanceTable,
                        levelTable,
                        parameterTable,
                        elementIndexMaps,
                        elementGeometryMap,
                        levelInfoMap,
                        orderedLevelInfosByProjectElevation,
                        elementIdToLevelInfoMap);
                })
                .ToArray();
        }
    }
}
