using System;
using System.Collections.Generic;
using System.Linq;
using Vim.Format.ObjectModel;
using Vim.LinqArray;
using Vim.Util;

namespace Vim.Format.Levels
{
    public static class LevelService
    {
        /// <summary>
        /// Returns an array of LevelInfo objects representing harmonized information about the levels in the given VIM Scene (see comment in LevelInfo.cs)
        /// </summary>
        public static (LevelInfo[], FamilyInstanceLevelInfo[]) GetLevelInfo(VimScene vimScene)
        {
            var dm = vimScene.DocumentModel;

            var levels = dm.LevelList.ToArray();

            var levelsByBimDocumentIndexAndElementId = levels.GroupByBimDocumentIndexAndElementId(dm);
            var basePointsByBimDocumentIndexAndElementId = dm.BasePointList.ToArray().GroupByBimDocumentIndexAndElementId(dm);

            var levelInfos = CreateLevelInfos(dm, levels, levelsByBimDocumentIndexAndElementId, basePointsByBimDocumentIndexAndElementId);
            var levelInfoByBimDocumentIndex = levelInfos.GroupByBimDocumentIndexAndElementId(dm);
            PatchBuildingStoryAbove(levelInfoByBimDocumentIndex);

            var familyInstances = dm.FamilyInstanceList.ToArray();
            var familyInstanceLevelInfos = CreateFamilyInstanceLevelInfos(vimScene, familyInstances, levelInfoByBimDocumentIndex);

            return (levelInfos, familyInstanceLevelInfos);
        }

        /// <summary>
        /// Instantiates the level infos in parallel based on the given list of levels.
        /// </summary>
        private static LevelInfo[] CreateLevelInfos(
            DocumentModel dm,
            IReadOnlyList<Level> levels,
            IReadOnlyDictionary<int, Dictionary<long, Level>> levelsByBimDocumentIndexAndElementId,
            IReadOnlyDictionary<int, Dictionary<long, BasePoint>> basePointsByBimDocumentIndexAndElementId)
        {
            var elementBimDocumentIndex = dm.ElementBimDocumentIndex.ToArray();

            return levels
                .AsParallel()
                .Select(level =>
                {
                    var bimDocumentIndex = elementBimDocumentIndex[level.GetElementIndexOrNone()];

                    if (!levelsByBimDocumentIndexAndElementId.TryGetValue(bimDocumentIndex, out var elementIdToLevelMap))
                        elementIdToLevelMap = new Dictionary<long, Level>();

                    if (!basePointsByBimDocumentIndexAndElementId.TryGetValue(bimDocumentIndex, out var elementIdToBasePointMap))
                        elementIdToBasePointMap = new Dictionary<long, BasePoint>();

                    return new LevelInfo(dm, level, elementIdToLevelMap, elementIdToBasePointMap);
                })
                .ToArray();
        }

        /// <summary>
        /// Populates the LevelInfo.BuildingStoryAbove property if it is null.
        /// </summary>
        private static void PatchBuildingStoryAbove(this IReadOnlyDictionary<int, Dictionary<long, LevelInfo>> levelInfoByBimDocumentIndex)
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
            VimScene vimScene,
            IReadOnlyList<FamilyInstance> familyInstances,
            IReadOnlyDictionary<int, Dictionary<long, LevelInfo>> levelInfoByBimDocumentIndex)
        {
            var dm = vimScene.DocumentModel;

            var elementIds = dm.ElementId.ToArray();
            var elementLevelIndices = dm.ElementLevelIndex.ToArray();
            var elementBimDocumentIndices = dm.ElementBimDocumentIndex.ToArray();
            var levelElementIndices = dm.LevelElementIndex.ToArray();
            var elementIndexToNodeIndicesMap = ElementIndexMaps.GetElementIndicesMap(vimScene.DocumentModel.NodeEntityTable);

            var orderedLevelInfoByBimDocumentIndex = levelInfoByBimDocumentIndex.ToDictionary(
                kv => kv.Key,
                kv => kv.Value.Values.OrderBy(li => li.Level.ProjectElevation).ToArray());

            return familyInstances
                .AsParallel()
                .Select(fi =>
                {
                    var bimDocumentIndex = elementBimDocumentIndices[fi.GetElementIndexOrNone()];

                    if (!orderedLevelInfoByBimDocumentIndex.TryGetValue(bimDocumentIndex, out var orderedLevelInfosByProjectElevation))
                        orderedLevelInfosByProjectElevation = Array.Empty<LevelInfo>();

                    if (!levelInfoByBimDocumentIndex.TryGetValue(bimDocumentIndex, out var elementIdToLevelInfoMap))
                        elementIdToLevelInfoMap = new Dictionary<long, LevelInfo>();

                    return new FamilyInstanceLevelInfo(
                        vimScene,
                        fi,
                        elementIds,
                        elementLevelIndices,
                        levelElementIndices,
                        orderedLevelInfosByProjectElevation,
                        elementIdToLevelInfoMap,
                        elementIndexToNodeIndicesMap);
                })
                .ToArray();
        }
    }
}
