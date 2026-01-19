using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.Math3d;
using Vim.Util.Tests;

namespace Vim.Format.Tests;

[TestFixture]
public static class VimTransformServiceTests
{
    [TestCase("Walls")]
    [TestCase("Doors")]
    [TestCase("BOGUS_CATEGORY_THIS_REMOVES_ALL_NODES")]
    public static void TestVimTransformService(string categoryName)
    {
        var ctx = new CallerTestContext(subDirComponents: categoryName);
        var dir = ctx.PrepareDirectory();

        bool IsFamilyInstanceWithCategoryOrTrue(int i, VimEntityTableSet t, ElementKind[] k)
            => k[i] == ElementKind.FamilyInstance
                ? t.GetElement(i)?.Category?.Name == categoryName
                : true;

        var vimFilePath = Path.Combine(VimFormatRepoPaths.DataDir, "RoomTest.vim");
        var vim = VIM.Open(vimFilePath);
        var tableSet = vim.GetEntityTableSet();
        var elementKinds = tableSet.GetElementKinds();

        // Keep the elements of the given category and rotate them upside down.
        var transformService = new VimTransformService(nameof(TestVimTransformService), "0.0.0");
        var transformResult = transformService.Transform(
            vim,
            e => IsFamilyInstanceWithCategoryOrTrue(e.ElementIndex, tableSet, elementKinds),
            (i, t) => Matrix4x4.CreateFromAxisAngle(Vector3.UnitX, (float)Math.PI) * t,
            false);

        var transformedVimFilePath = Path.Combine(dir, $"transformed_{categoryName}.vim");
        transformResult.Write(transformedVimFilePath);

        Assert.IsTrue(File.Exists(transformedVimFilePath));

        var transformedVim = VIM.Open(transformedVimFilePath);
        transformedVim.Validate();

        var transformedElementGeometryInfoList = transformedVim.GetElementGeometryInfoList();
        var transformedTableSet = transformedVim.GetEntityTableSet();
        var transformedElementKinds = transformedTableSet.GetElementKinds();

        Assert.IsTrue(transformedElementGeometryInfoList.All(e =>
            IsFamilyInstanceWithCategoryOrTrue(e.ElementIndex, transformedTableSet, transformedElementKinds)));
        
        Assert.IsTrue(transformedTableSet.NodeTable.All(n =>
            IsFamilyInstanceWithCategoryOrTrue(n.ElementIndex, transformedTableSet, transformedElementKinds)));

        Assert.IsTrue(transformedTableSet.FamilyInstanceTable.All(fi =>
            transformedElementKinds[fi.ElementIndex] == ElementKind.FamilyInstance &&
            IsFamilyInstanceWithCategoryOrTrue(fi.ElementIndex, transformedTableSet, transformedElementKinds)));
    }

    [Test]
    public static void TestSplitMerge()
    {
        var ctx = new CallerTestContext();
        var dir = ctx.PrepareDirectory();

        var vimFilePath = Path.Combine(VimFormatRepoPaths.DataDir, "RoomTest.vim");
        var vim = VIM.Open(vimFilePath);
        var transformService = new VimTransformService(nameof(TestSplitMerge), "0.0.0");

        var gd = vim.GeometryData; 
        var box = gd.GetWorldSpaceBoundingBox();

        bool IsLeft(int instanceIndex, int meshIndex)
        {
            if (!vim.GeometryData.TryGetTransformedMesh(instanceIndex, meshIndex, out var vimMeshData))
                return false;

            var meshBb = vimMeshData.GetBoundingBox();

            return meshBb.Center.X < box.Center.X;
        }

        // Get the left part of the VIM file
        var leftVimFilePath = Path.Combine(dir, "left.vim");
        {
            var filterResult = transformService.Filter(vim, e =>
            {
                if (!e.HasMesh) return true;
                return e.InstanceAndMeshIndices.Any(t => IsLeft(t.InstanceIndex, t.MeshIndex));
            });
            filterResult.Write(leftVimFilePath);
            Assert.IsTrue(File.Exists(leftVimFilePath));
        }
        var leftVim = VIM.Open(leftVimFilePath);
        leftVim.Validate();

        // Get the right part of the VIM file.
        var rightVimFilePath = Path.Combine(dir, "right.vim");
        {
            var filterResult = transformService.Filter(vim, e =>
            {
                if (!e.HasMesh) return true;
                return e.InstanceAndMeshIndices.All(t => !IsLeft(t.InstanceIndex, t.MeshIndex));
            });
            filterResult.Write(rightVimFilePath);
            Assert.IsTrue(File.Exists(rightVimFilePath));
        }
        var rightVim = VIM.Open(rightVimFilePath);
        rightVim.Validate();

        bool HasGeometry(VimElementGeometryInfo egi) => egi.HasMesh;

        Assert.AreEqual(
            vim.GetElementGeometryInfoList().Count(HasGeometry),
            leftVim.GetElementGeometryInfoList().Count(HasGeometry) + rightVim.GetElementGeometryInfoList().Count(HasGeometry));

        Assert.AreEqual(vim.EntityTableData.Count, leftVim.EntityTableData.Count);
        Assert.AreEqual(vim.EntityTableData.Count, rightVim.EntityTableData.Count);

        var mergedVimFilePath = Path.Combine(dir, "merged.vim");
        var mergeResult = VimMergeService.Merge(
            new VimMergeConfig(new[] { leftVim, rightVim }),
            new VimMergeConfigOptions());
        mergeResult.Write(mergedVimFilePath);
        Assert.IsTrue(File.Exists(mergedVimFilePath));
        var mergedVim = VIM.Open(mergedVimFilePath);
        mergedVim.Validate();

        Assert.AreEqual(
            vim.GetElementGeometryInfoList().Count(HasGeometry),
            mergedVim.GetElementGeometryInfoList().Count(HasGeometry));

        Assert.AreEqual(vim.EntityTableData.Count, mergedVim.EntityTableData.Count);

        var sourceStringSet = new HashSet<string>(vim.StringTable);
        var mergedStringSet = new HashSet<string>(mergedVim.StringTable);
        mergedStringSet.ExceptWith(sourceStringSet);
        Assert.AreEqual(0, mergedStringSet.Count);

        Assert.AreEqual(vim.Assets.Length, mergedVim.Assets.Length);
    }

    [Test]
    public static void TestFilter()
    {
        var ctx = new CallerTestContext();
        var dir = ctx.PrepareDirectory();

        var vimFilePath = Path.Combine(VimFormatRepoPaths.DataDir, "RoomTest.vim");
        var vim = VIM.Open(vimFilePath);

        // Just keep the even-numbered nodes.
        var transformService = new VimTransformService(nameof(TestFilter), "0.0.0");
        var counter = 0;
        var transformed = transformService.Filter(vim, e => !e.HasMesh || (counter++ % 2 == 0));

        var filteredVimFilePath = Path.Combine(dir, "evens.vim");
        transformed.Write(filteredVimFilePath);

        Assert.IsTrue(File.Exists(filteredVimFilePath));

        var filteredVim = VIM.Open(filteredVimFilePath);
        filteredVim.Validate();
    }

    [Test]
    public static void TestMergeDedupAndFilter()
    {
        var ctx = new CallerTestContext();
        var dir = ctx.PrepareDirectory();

        var vimFilePath = VimFormatRepoPaths.GetDataFilePath("Dwelling*.vim", true);

        // Setup: Merge two identical VIM files as a grid.
        var vim1 = VIM.Open(vimFilePath);
        var vim2 = VIM.Open(vimFilePath);

        var generatorString = nameof(TestMergeDedupAndFilter);
        var versionString = "0.0.0";

        var merged = VimMergeService.Merge(
            new VimMergeConfig(new[] { vim1, vim2 }),
            new VimMergeConfigOptions
            {
                MergeAsGrid = true,
                GridPadding = 10f,
                GeneratorString = generatorString,
                VersionString = versionString
            });

        var mergedVimFilePath = Path.Combine(dir, "merged.vim");
        merged.Write(mergedVimFilePath);
        var mergedVim = VIM.Open(mergedVimFilePath);
        mergedVim.Validate();

        // Deduplicate identical meshes using the transform service.

        var transformService = new VimTransformService(generatorString, versionString);

        var dedup = transformService.DeduplicateGeometry(mergedVim);
        var dedupVimFilePath = Path.Combine(dir, "dedup.vim");
        dedup.Write(dedupVimFilePath);
        var dedupVim = VIM.Open(dedupVimFilePath);
        dedupVim.Validate();

        Assert.Less(dedupVim.GeometryData.MeshCount, mergedVim.GeometryData.MeshCount);

        // Bonus: filter the deduplicated VIM and only keep the casework

        var filtered = transformService.Filter(dedupVim, e => !e.HasMesh || dedupVim.GetEntityTableSet().GetElement(e.ElementIndex).Category?.Name == "Casework");
        var filteredVimFilePath = Path.Combine(dir, "filtered.vim");
        filtered.Write(filteredVimFilePath);
        var filteredVim = VIM.Open(filteredVimFilePath);
        filteredVim.Validate();
    }
}
