using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.Math3d;
using Vim.Util;
using Vim.Util.Tests;

namespace Vim.Format.Tests
{
    [TestFixture]
    public static class VimMergeServiceTests
    {
        [Test]
        public static void TestSameMergeFile()
        {
            var ctx = new CallerTestContext();
            var dir = ctx.PrepareDirectory();

            var vim = Path.Combine(VimFormatRepoPaths.DataDir, "RoomTest.vim");

            var mergedVimFilePath = Path.Combine(dir, "rooms_merged.vim");

            var configFiles = new VimMergeConfigFiles(
                new[]
                {
                    // First VIM is centered at the origin
                    (vim, Matrix4x4.Identity),
                    // Second VIM (same model) is offset by 100 units along +X
                    (vim, Matrix4x4.CreateTranslation(Vector3.UnitX * 100))
                }, mergedVimFilePath);

            var configOptions = new VimMergeConfigOptions()
            {
                GeneratorString = ctx.TestName,
                DeduplicateEntities = true,
                KeepBimData = true,
            };

            VimMergeService.Merge(configFiles, configOptions);

            var baseVim = VIM.Open(vim);
            baseVim.Validate();
            var baseVimTableSet = baseVim.GetEntityTableSet();

            var mergedVim = VIM.Open(mergedVimFilePath);
            mergedVim.Validate();
            var mergedVimTableSet = mergedVim.GetEntityTableSet();

            // The categories should be deduplicated
            var numCategoriesBase = baseVimTableSet.CategoryTable.RowCount;
            var numCategoriesMerged = mergedVimTableSet.CategoryTable.RowCount;
            Assert.AreEqual(numCategoriesBase, numCategoriesMerged);

            // The number of elements which originally had a category must be doubled in the merged VIM file.
            var numElementsWithCategoryBase = baseVimTableSet.ElementTable.Count(e => e.Category != null);
            var numElementsWithCategoryMerged = mergedVimTableSet.ElementTable.Count(e => e.Category != null);
            Assert.Greater(numElementsWithCategoryBase, 0);
            Assert.Greater(numElementsWithCategoryMerged, 0);
            Assert.AreEqual(numElementsWithCategoryBase * 2, numElementsWithCategoryMerged);

            // The display units should be deduplicated.
            var numDisplayUnitsMerged = mergedVimTableSet.DisplayUnitTable.RowCount;
            var numDisplayUnitsBase = baseVimTableSet.DisplayUnitTable.RowCount;
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

            var configFiles = new VimMergeConfigFiles(
                new[]
                {
                    (vimFilePath1, Matrix4x4.Identity),
                    (vimFilePath2, Matrix4x4.Identity)
                }, mergedVimFilePath);

            var configOptions = new VimMergeConfigOptions()
            {
                GeneratorString = ctx.TestName,
                DeduplicateEntities = true,
                KeepBimData = true,
                MergeAsGrid = true,
            };

            VimMergeService.Merge(configFiles, configOptions);

            var vim1 = VIM.Open(vimFilePath1);
            vim1.Validate();
            var vim1TableSet = vim1.GetEntityTableSet();

            var vim2 = VIM.Open(vimFilePath2);
            vim2.Validate();
            var vim2TableSet = vim2.GetEntityTableSet();

            var mergedVim = VIM.Open(mergedVimFilePath);
            mergedVim.Validate();
            var mergedVimTableSet = mergedVim.GetEntityTableSet();

            // The categories in the merged VIM must be a distinct count of the categories in vim1 and vim2.
            var categoriesVim1 = vim1TableSet.CategoryTable.ToArray();
            var categoriesVim2 = vim2TableSet.CategoryTable.ToArray();
            var distinctCategoryCount = categoriesVim1.Concat(categoriesVim2).Distinct(new CategoryEqualityComparer()).Count();
            var mergedCategoryCount = mergedVimTableSet.CategoryTable.RowCount;
            Assert.AreEqual(distinctCategoryCount, mergedCategoryCount);
            
            // The display units in the merged VIM must be a distinct count of the display units in vim1 and vim2.
            var displayUnitsVim1 = vim1TableSet.DisplayUnitTable.ToArray();
            var displayUnitsVim2 = vim2TableSet.DisplayUnitTable.ToArray();
            var distinctDisplayUnitCount = displayUnitsVim1.Concat(displayUnitsVim2).Distinct(new DisplayUnitEqualityComparer()).Count();
            var mergedDisplayUnitCount = mergedVimTableSet.DisplayUnitTable.RowCount;
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
        public static void TestMergeShouldSucceedOnCompatibleObjectModelVersions()
        {
            var ctx = new CallerTestContext();
            var dir = ctx.PrepareDirectory();

            var vim1 = Path.Combine(VimFormatRepoPaths.DataDir, "RoomTest.vim");
            var vim2 = Path.Combine(VimFormatRepoPaths.DataDir, "RoomTestModified.vim");

            var mergedVimFilePath = Path.Combine(dir, "room_merge.vim");

            var configFiles = new VimMergeConfigFiles(
                new[]
                {
                    // First VIM is centered at the origin
                    (vim1, Matrix4x4.Identity),
                    // Second VIM is offset by 100 units along +X
                    (vim2, Matrix4x4.CreateTranslation(Vector3.UnitX * 100))
                }, mergedVimFilePath);

            var configOptions = new VimMergeConfigOptions() { GeneratorString = ctx.TestName, VersionString = "0.0.0", };
            VimMergeService.Merge(configFiles, configOptions);

            Assert.IsTrue(File.Exists(mergedVimFilePath));

            var mergedVim = VIM.Open(mergedVimFilePath);
            mergedVim.Validate();
        }

        [Test]
        public static void TestMergeFailsOnMismatchedObjectModelVersions()
        {
            var ctx = new CallerTestContext();
            var dir = ctx.PrepareDirectory();

            var vim1 = Path.Combine(VimFormatRepoPaths.DataDir, "Wolford_Residence.r2023.om_v5.0.0.vim");
            var vim2 = Path.Combine(VimFormatRepoPaths.DataDir, "Wolford_Residence.r2023.om_v4.4.0.vim");
            var mergedVimFilePath = Path.Combine(dir, "should_fail.vim");

            var configFiles = new VimMergeConfigFiles(
                new[]
                {
                    // First VIM is centered at the origin
                    (vim1, Matrix4x4.Identity),
                    // Second VIM is offset by 100 units along +X
                    (vim2, Matrix4x4.CreateTranslation(Vector3.UnitX * 100))
                }, mergedVimFilePath);

            var configOptions = new VimMergeConfigOptions()
            {
                GeneratorString = ctx.TestName,
                DeduplicateEntities = true,
                KeepBimData = true,
            };

            // Validate that an exception is thrown due to mismatching object model major versions
            try
            {
                VimMergeService.Merge(configFiles, configOptions);
                Assert.Fail($"Expected an exception to be thrown ({VimErrorCode.VimMergeObjectModelMajorVersionMismatch:G})");
            }
            catch (HResultException e)
            {
                Assert.AreEqual((int) VimErrorCode.VimMergeObjectModelMajorVersionMismatch, e.HResult);
            }
        }
    }
}
