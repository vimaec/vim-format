using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.Format.ObjectModel;
using Vim.Util;

namespace Vim.Format.Levels
{
    public class LevelInfoService
    {
        private ElementGeometryMap ElementGeometryMap { get; }

        private EntityTableSet TableSet { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public LevelInfoService(
            FileInfo vimFileInfo,
            string[] stringTable = null,
            ElementGeometryMap elementGeometryMap = null)
        {
            TableSet = new EntityTableSet(
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

            ElementGeometryMap = elementGeometryMap ?? new ElementGeometryMap(vimFileInfo);
        }

        /// <summary>
        /// Returns an array of LevelInfo objects representing harmonized information about the levels in the given VIM Scene (see comment in LevelInfo.cs)
        /// </summary>
        public (LevelInfo[], FamilyInstanceLevelInfo[]) GetLevelInfos()
        {
            var elementIndexMaps = TableSet.ElementIndexMaps;
            var elementTable = TableSet.ElementTable;
            var parameterTable = TableSet.ParameterTable;
            var familyInstanceTable = TableSet.FamilyInstanceTable;
            var familyTypeTable = TableSet.FamilyTypeTable;
            var basePointTable = TableSet.BasePointTable;
            var levelTable = TableSet.LevelTable;

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

            var familyInstanceLevelInfos = CreateFamilyInstanceLevelInfos(
                familyInstanceTable,
                elementTable,
                levelTable,
                parameterTable,
                elementIndexMaps,
                ElementGeometryMap,
                levelInfoMap,
                levelInfoByBimDocumentIndex);

            return (levelInfos, familyInstanceLevelInfos);
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
        {
            return levels
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
        }

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
        /// Returns an array of family instance level infos based on the given list of family instances.
        /// </summary>
        private static FamilyInstanceLevelInfo[] CreateFamilyInstanceLevelInfos(
            FamilyInstanceTable familyInstanceTable,
            ElementTable elementTable,
            LevelTable levelTable,
            ParameterTable parameterTable,
            ElementIndexMaps elementIndexMaps,
            ElementGeometryMap elementGeometryMap,
            IReadOnlyDictionary<int, LevelInfo> levelInfoMap,
            IReadOnlyDictionary<int, Dictionary<long, LevelInfo>> levelInfoByBimDocumentIndex)
        {
            var levelInfoByBimDocumentIndexOrdered = levelInfoByBimDocumentIndex.ToDictionary(
                kv => kv.Key,
                kv => kv.Value.Values.OrderBy(li => li.Level.ProjectElevation).ToArray());

            return familyInstanceTable
                .AsParallel()
                .Select(fi =>
                {
                    var bimDocumentIndex = elementTable.GetBimDocumentIndex(fi.GetElementIndexOrNone());

                    if (!levelInfoByBimDocumentIndexOrdered.TryGetValue(bimDocumentIndex, out var orderedLevelInfosByProjectElevation))
                        orderedLevelInfosByProjectElevation = Array.Empty<LevelInfo>();

                    if (!levelInfoByBimDocumentIndex.TryGetValue(bimDocumentIndex, out var elementIdToLevelInfoMap))
                        elementIdToLevelInfoMap = new Dictionary<long, LevelInfo>();

                    return new FamilyInstanceLevelInfo(
                        fi,
                        elementTable,
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
