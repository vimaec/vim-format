using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.Format.Merge;
using Vim.Format.ObjectModel;
using Vim.Format.SceneBuilder;
using Vim.LinqArray;
using Vim.Math3d;
using Vim.Util;
using Vim.Util.Tests;

namespace Vim.Format.Tests
{
    [TestFixture]
    public static class MergeTests
    {
        [Test]
        public static void TestSameMergeFile()
        {
            var ctx = new CallerTestContext();
            var dir = ctx.PrepareDirectory();

            var vim = Path.Combine(VimFormatRepoPaths.DataDir, "RoomTest.vim");

            var mergedVimFilePath = Path.Combine(dir, "rooms_merged.vim");

            var configFiles = new MergeConfigFiles(
                new[]
                {
                    // First VIM is centered at the origin
                    (vim, Matrix4x4.Identity),
                    // Second VIM (same model) is offset by 100 units along +X
                    (vim, Matrix4x4.CreateTranslation(Vector3.UnitX * 100))
                }, mergedVimFilePath);

            var configOptions = new MergeConfigOptions()
            {
                GeneratorString = ctx.TestName,
                DeduplicateEntities = true,
                KeepBimData = true,
            };

            MergeService.MergeVimFiles(configFiles, configOptions);

            var baseVim = VimScene.LoadVim(vim);
            baseVim.Validate();

            var mergedVim = VimScene.LoadVim(mergedVimFilePath);
            mergedVim.Validate();

            // The categories should be deduplicated
            var numCategoriesBase = baseVim.DocumentModel.NumCategory;
            var numCategoriesMerged = mergedVim.DocumentModel.NumCategory;
            Assert.AreEqual(numCategoriesBase, numCategoriesMerged);

            // The number of elements which originally had a category must be doubled in the merged VIM file.
            var numElementsWithCategoryBase = baseVim.DocumentModel.ElementList.ToList().Count(e => e.Category != null);
            var numElementsWithCategoryMerged = mergedVim.DocumentModel.ElementList.ToList().Count(e => e.Category != null);
            Assert.Greater(numElementsWithCategoryBase, 0);
            Assert.Greater(numElementsWithCategoryMerged, 0);
            Assert.AreEqual(numElementsWithCategoryBase * 2, numElementsWithCategoryMerged);

            // The display units should be deduplicated.
            var numDisplayUnitsMerged = mergedVim.DocumentModel.NumDisplayUnit;
            var numDisplayUnitsBase = baseVim.DocumentModel.NumDisplayUnit;
            Assert.AreEqual(numDisplayUnitsBase, numDisplayUnitsMerged);
        }

        [Test]
        public static void TestMergeDifferentFilesAsGrid()
        {
            var ctx = new CallerTestContext();
            var dir = ctx.PrepareDirectory();

            var vimFilePath1 = Path.Combine(VimFormatRepoPaths.DataDir, "RoomTest.vim");
            var vimFilePath2 = Path.Combine(VimFormatRepoPaths.DataDir, "Wolford_Residence.r2023.om_v5.0.0.vim");
            var mergedVimFilePath = Path.Combine(dir, "merged.vim");

            var configFiles = new MergeConfigFiles(
                new[]
                {
                    (vimFilePath1, Matrix4x4.Identity),
                    (vimFilePath2, Matrix4x4.Identity)
                }, mergedVimFilePath);

            var configOptions = new MergeConfigOptions()
            {
                GeneratorString = ctx.TestName,
                DeduplicateEntities = true,
                KeepBimData = true,
                MergeAsGrid = true,
            };

            MergeService.MergeVimFiles(configFiles, configOptions);

            var vim1 = VimScene.LoadVim(vimFilePath1);
            vim1.Validate();

            var vim2 = VimScene.LoadVim(vimFilePath2);
            vim2.Validate();

            var mergedVim = VimScene.LoadVim(mergedVimFilePath);
            mergedVim.Validate();

            // The categories in the merged VIM must be a distinct count of the categories in vim1 and vim2.
            var categoriesVim1 = vim1.DocumentModel.CategoryList.ToEnumerable();
            var categoriesVim2 = vim2.DocumentModel.CategoryList.ToEnumerable();
            var distinctCategoryCount = categoriesVim1.Concat(categoriesVim2).Distinct(new CategoryEqualityComparer()).Count();
            var mergedCategoryCount = mergedVim.DocumentModel.NumCategory;
            Assert.AreEqual(distinctCategoryCount, mergedCategoryCount);
            
            // The display units in the merged VIM must be a distinct count of the display units in vim1 and vim2.
            var displayUnitsVim1 = vim1.DocumentModel.DisplayUnitList.ToEnumerable();
            var displayUnitsVim2 = vim2.DocumentModel.DisplayUnitList.ToEnumerable();
            var distinctDisplayUnitCount = displayUnitsVim1.Concat(displayUnitsVim2).Distinct(new DisplayUnitEqualityComparer()).Count();
            var mergedDisplayUnitCount = mergedVim.DocumentModel.NumDisplayUnit;
            Assert.AreEqual(distinctDisplayUnitCount, mergedDisplayUnitCount);
        }

        private class CategoryEqualityComparer : IEqualityComparer<Category>
        {
            public bool Equals(Category x, Category y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Name == y.Name && x.BuiltInCategory == y.BuiltInCategory;
            }

            public int GetHashCode(Category obj)
            {
                return HashCode.Combine(obj.Name, obj.BuiltInCategory);
            }
        }

        private class DisplayUnitEqualityComparer : IEqualityComparer<DisplayUnit>
        {
            public bool Equals(DisplayUnit x, DisplayUnit y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Spec == y.Spec && x.Type == y.Type && x.Label == y.Label;
            }

            public int GetHashCode(DisplayUnit obj)
            {
                return HashCode.Combine(obj.Spec, obj.Type, obj.Label);
            }
        }

        [Test]
        public static void TestMergeFailsOnMismatchedObjectModelVersions()
        {
            var ctx = new CallerTestContext();
            var dir = ctx.PrepareDirectory();

            var vim1 = Path.Combine(VimFormatRepoPaths.DataDir, "Wolford_Residence.r2023.om_v5.0.0.vim");
            var vim2 = Path.Combine(VimFormatRepoPaths.DataDir, "Wolford_Residence.r2023.om_v4.4.0.vim");
            var mergedVimFilePath = Path.Combine(dir, "should_fail.vim");

            var configFiles = new MergeConfigFiles(
                new[]
                {
                    // First VIM is centered at the origin
                    (vim1, Matrix4x4.Identity),
                    // Second VIM is offset by 100 units along +X
                    (vim2, Matrix4x4.CreateTranslation(Vector3.UnitX * 100))
                }, mergedVimFilePath);

            var configOptions = new MergeConfigOptions()
            {
                GeneratorString = ctx.TestName,
                DeduplicateEntities = true,
                KeepBimData = true,
            };

            // Validate that an exception is thrown due to mismatching object model major versions
            try
            {
                MergeService.MergeVimFiles(configFiles, configOptions);
                Assert.Fail($"Expected an exception to be thrown ({ErrorCode.VimMergeObjectModelMajorVersionMismatch:G})");
            }
            catch (HResultException e)
            {
                Assert.AreEqual((int) ErrorCode.VimMergeObjectModelMajorVersionMismatch, e.HResult);
            }
        }
    }
}
