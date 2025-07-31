using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.Format.ObjectModel;
using Vim.G3d;
using Vim.Util;

using ElementIndexToNodeAndGeometryMap = Vim.Util.DictionaryOfLists<int, (int NodeIndex, int GeometryIndex)>;

namespace Vim.Format.Levels
{
    public class LevelInfoService
    {
        private G3D G3d { get; }

        private ElementIndexToNodeAndGeometryMap ElementIndexToNodeAndGeometryIndexMap { get; }

        private EntityTableSet TableSet { get; }

        public LevelInfoService(FileInfo vimFileInfo)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        public LevelInfoService(
            FileInfo vimFileInfo,
            string[] stringTable,
            ElementIndexToNodeAndGeometryMap elementIndexToNodeAndGeometryIndexMap,
            G3D g3d)
        {
            TableSet = new EntityTableSet(vimFileInfo, false, stringTable,
                n =>
                    n is TableNames.Element ||
                    n is TableNames.Level ||
                    n is TableNames.FamilyInstance ||
                    n is TableNames.FamilyType ||
                    n is TableNames.Parameter ||
                    n is TableNames.ParameterDescriptor);

            ElementIndexToNodeAndGeometryIndexMap = elementIndexToNodeAndGeometryIndexMap;

            G3d = g3d;
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

            PatchBuildingStoryAbove(levelInfoByBimDocumentIndex);

            var familyInstances = familyInstanceTable.AsParallel().ToArray();
            var familyInstanceLevelInfos = CreateFamilyInstanceLevelInfos(
                familyInstances,
                levelInfoByBimDocumentIndex,
                elementTable,
                levelTable,
                parameterTable,
                elementIndexMaps,
                G3d,
                ElementIndexToNodeAndGeometryIndexMap);

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
            IReadOnlyList<FamilyInstance> familyInstances,
            IReadOnlyDictionary<int, Dictionary<long, LevelInfo>> levelInfoByBimDocumentIndex,
            ElementTable elementTable,
            LevelTable levelTable,
            ParameterTable parameterTable,
            ElementIndexMaps elementIndexMaps,
            G3D g3d,
            ElementIndexToNodeAndGeometryMap elementIndexToNodeAndGeometryMap)
        {
            var orderedLevelInfoByBimDocumentIndex = levelInfoByBimDocumentIndex.ToDictionary(
                kv => kv.Key,
                kv => kv.Value.Values.OrderBy(li => li.Level.ProjectElevation).ToArray());

            return familyInstances
                .AsParallel()
                .Select(fi =>
                {
                    var bimDocumentIndex = elementTable.GetBimDocumentIndex(fi.GetElementIndexOrNone());

                    if (!orderedLevelInfoByBimDocumentIndex.TryGetValue(bimDocumentIndex, out var orderedLevelInfosByProjectElevation))
                        orderedLevelInfosByProjectElevation = Array.Empty<LevelInfo>();

                    if (!levelInfoByBimDocumentIndex.TryGetValue(bimDocumentIndex, out var elementIdToLevelInfoMap))
                        elementIdToLevelInfoMap = new Dictionary<long, LevelInfo>();

                    return new FamilyInstanceLevelInfo(
                        fi,
                        elementTable,
                        levelTable,
                        parameterTable,
                        elementIndexMaps,
                        orderedLevelInfosByProjectElevation,
                        elementIdToLevelInfoMap,
                        g3d,
                        elementIndexToNodeAndGeometryMap);
                })
                .ToArray();
        }
    }
}
