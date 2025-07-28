using System;
using System.Collections.Generic;
using System.Linq;
using Vim.Format.ObjectModel;
using Vim.LinqArray;
using Vim.Util;

namespace Vim.Format.Levels
{
    public class LevelService
    {
        /// <summary>
        /// Returns an array of LevelInfo objects representing harmonized information about the levels in the given VIM Scene (see comment in LevelInfo.cs)
        /// </summary>
        public static LevelInfo[] GetLevelInfo(VimScene vimScene)
        {
            var dm = vimScene.DocumentModel;

            var levels = dm.LevelList.ToArray();

            var levelsByBimDocumentIndex = levels
                .GroupBy(l => l?.Element?.BimDocument?.IndexOrDefault() ?? EntityRelation.None)
                .ToDictionary(g => g.Key, g => (IReadOnlyList<Level>) g.ToArray());

            var basePointsByBimDocumentIndex = dm.BasePointList
                .GroupBy(b => b?.Element?.BimDocument.IndexOrDefault() ?? EntityRelation.None)
                .ToDictionary(g => g.Key, g => (IReadOnlyList<BasePoint>) g.ToArray());

            var levelInfos = CreateLevelInfos(dm, levels, levelsByBimDocumentIndex, basePointsByBimDocumentIndex);

            PatchBuildingStoryAbove(levelInfos);

            return levelInfos;
        }

        /// <summary>
        /// Instantiates the level infos in parallel based on the given list of levels.
        /// </summary>
        private static LevelInfo[] CreateLevelInfos(
            DocumentModel documentModel,
            IReadOnlyList<Level> levels,
            IReadOnlyDictionary<int, IReadOnlyList<Level>> levelsByBimDocumentIndex,
            IReadOnlyDictionary<int, IReadOnlyList<BasePoint>> basePointsByBimDocumentIndex)
            => levels
                .AsParallel()
                .Select(level =>
                {
                    var bimDocumentIndex = level?.Element?.BimDocument?.IndexOrDefault() ?? EntityRelation.None;

                    var levelsInBimDocument =
                        levelsByBimDocumentIndex.TryGetValue(bimDocumentIndex, out var levelList)
                            ? levelList
                            : Array.Empty<Level>();

                    var basePointsInBimDocument =
                        basePointsByBimDocumentIndex.TryGetValue(bimDocumentIndex, out var basePointList)
                            ? basePointList
                            : Array.Empty<BasePoint>();

                    return new LevelInfo(documentModel, level, levelsInBimDocument, basePointsInBimDocument);
                })
                .ToArray();

        /// <summary>
        /// Populates the LevelInfo.BuildingStoryAbove property if it is null.
        /// </summary>
        private static void PatchBuildingStoryAbove(IReadOnlyList<LevelInfo> levelInfos)
        {
            var levelInfosByBimDocumentIndex = levelInfos
                .GroupBy(l => l.Level?.Element?.BimDocument.IndexOrDefault() ?? EntityRelation.None)
                .ToDictionary(g => g.Key, g => (IReadOnlyList<LevelInfo>)g.ToArray());

            foreach (var (_, levelInfosInBimDocument) in levelInfosByBimDocumentIndex)
            {
                var levelInfosOrderedByProjectElevation = levelInfosInBimDocument.OrderBy(l => l.Level.ProjectElevation).ToArray();

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
    }
}
