using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.Format.Geometry;
using Vim.Format.Merge;
using Vim.Format.ObjectModel;
using Vim.Format.SceneBuilder;
using Vim.LinqArray;
using Vim.Math3d;
using Vim.Util.Tests;

namespace Vim.Format.Tests;

[TestFixture]
public static class TransformServiceTests
{
    [TestCase("Walls")]
    [TestCase("Doors")]
    [TestCase("BOGUS_CATEGORY_THIS_REMOVES_ALL_NODES")]
    public static void TestTransformService(string categoryName)
    {
        var ctx = new CallerTestContext(subDirComponents: categoryName);
        var dir = ctx.PrepareDirectory();

        var vimFilePath = Path.Combine(VimFormatRepoPaths.DataDir, "RoomTest.vim");
        var vim = VimScene.LoadVim(vimFilePath);

        // Keep the nodes of the given category and rotate them upside down.
        var transformService = new TransformService(nameof(TestTransformService), "0.0.0");
        var db = transformService.Transform(
            vim,
            n => n.CategoryName == categoryName,
            null,
            n => Matrix4x4.CreateFromAxisAngle(Vector3.UnitX, (float)Math.PI) * n.Transform,
            null,
            false);

        var transformedVimFilePath = Path.Combine(dir, $"transformed_{categoryName}.vim");
        db.Write(transformedVimFilePath);

        Assert.IsTrue(File.Exists(transformedVimFilePath));

        var transformedVim = VimScene.LoadVim(transformedVimFilePath);
        transformedVim.Validate();

        Assert.IsTrue(transformedVim.VimNodes.ToEnumerable().All(n => n.CategoryName == categoryName));
        
        var dm = transformedVim.DocumentModel;
        Assert.IsTrue(dm.NodeList.All(n => n.Element.Category.Name == categoryName));
        
        foreach (var fi in dm.FamilyInstanceList.ToArray())
        {
            var elementInfo = dm.GetElementInfo(fi.Element.Index);
            if (elementInfo.IsSystem) // TECH DEBT - skipping systems for now.
                continue;

            Assert.AreEqual(categoryName, elementInfo.CategoryName, $"FamilyInstance Element category is not {categoryName}");
        }
    }

    [Test]
    public static void TestSplitMerge()
    {
        var ctx = new CallerTestContext();
        var dir = ctx.PrepareDirectory();

        var vimFilePath = Path.Combine(VimFormatRepoPaths.DataDir, "RoomTest.vim");
        var vim = VimScene.LoadVim(vimFilePath);
        var transformService = new TransformService(nameof(TestTransformService), "0.0.0");

        var box = vim.BoundingBox();

        bool IsLeft(VimSceneNode n)
            => n.TransformedMesh().Center().X < box.Center.X;

        // Get the left part of the VIM file
        var leftVimFilePath = Path.Combine(dir, "left.vim");
        {
            var db = transformService.Filter(vim, n => n.GetMesh() != null && IsLeft(n));
            db.Write(leftVimFilePath);
            Assert.IsTrue(File.Exists(leftVimFilePath));
        }
        var leftVim = VimScene.LoadVim(leftVimFilePath);
        leftVim.Validate();

        // Get the right part of the VIM file.
        var rightVimFilePath = Path.Combine(dir, "right.vim");
        {
            var db = transformService.Filter(vim, n => n.GetMesh() != null && !IsLeft(n));
            db.Write(rightVimFilePath);
            Assert.IsTrue(File.Exists(rightVimFilePath));
        }
        var rightVim = VimScene.LoadVim(rightVimFilePath);
        rightVim.Validate();


        bool HasGeometry(ISceneNode n) => n.GetMesh() != null;
        Assert.AreEqual(vim.Nodes.Where(HasGeometry).Count(), leftVim.Nodes.Where(HasGeometry).Count() + rightVim.Nodes.Where(HasGeometry).Count());

        Assert.AreEqual(vim.Document.EntityTables.Keys.Count, leftVim.Document.EntityTables.Keys.Count);
        Assert.AreEqual(vim.Document.EntityTables.Keys.Count, rightVim.Document.EntityTables.Keys.Count);

        var mergedVimFilePath = Path.Combine(dir, "merged.vim");
        var documentBuilder = MergeService.MergeVimScenes(new MergeConfigVimScenes(new[] { leftVim, rightVim }));
        documentBuilder.Write(mergedVimFilePath);
        Assert.IsTrue(File.Exists(mergedVimFilePath));
        var mergedVim = VimScene.LoadVim(mergedVimFilePath);
        mergedVim.Validate();

        Assert.AreEqual(vim.Nodes.Where(HasGeometry).Count(), mergedVim.Nodes.Where(HasGeometry).Count());

        Assert.AreEqual(vim.Document.EntityTables.Keys.Count, vim.Document.EntityTables.Keys.Count);

        var sourceStringSet = new HashSet<string>(vim.Document.StringTable.ToEnumerable());
        var mergedStringSet = new HashSet<string>(mergedVim.Document.StringTable.ToEnumerable());
        mergedStringSet.ExceptWith(sourceStringSet);
        Assert.AreEqual(0, mergedStringSet.Count);

        Assert.AreEqual(vim.Document.Assets.Keys.Count, mergedVim.Document.Assets.Keys.Count);
    }

    [Test]
    public static void TestFilter()
    {
        var ctx = new CallerTestContext();
        var dir = ctx.PrepareDirectory();

        var vimFilePath = Path.Combine(VimFormatRepoPaths.DataDir, "RoomTest.vim");
        var vim = VimScene.LoadVim(vimFilePath);

        // Just keep the even-numbered nodes.
        var transformService = new TransformService(nameof(TestTransformService), "0.0.0");
        var counter = 0;
        var db = transformService.Filter(vim, n => n.GetMesh() != null && (counter++ % 2 == 0));

        var filteredVimFilePath = Path.Combine(dir, "evens.vim");
        db.Write(filteredVimFilePath);

        Assert.IsTrue(File.Exists(filteredVimFilePath));

        var filteredVim = VimScene.LoadVim(filteredVimFilePath);
        filteredVim.Validate();
    }

    [Test]
    public static void TestMergeDedupAndFilter()
    {
        var ctx = new CallerTestContext();
        var dir = ctx.PrepareDirectory();

        var vimFilePath = VimFormatRepoPaths.GetDataFilePath("Dwelling*.vim", true);

        // Setup: Merge two identical VIM files as a grid.
        var vim1 = VimScene.LoadVim(vimFilePath);
        var vim2 = VimScene.LoadVim(vimFilePath);

        var generatorString = nameof(TestMergeDedupAndFilter);
        var versionString = "0.0.0";

        var mergedDb = MergeService.MergeVimScenes(
            new MergeConfigVimScenes(new[] { vim1, vim2 }),
            new MergeConfigOptions
            {
                MergeAsGrid = true,
                GridPadding = 10f,
                GeneratorString = generatorString,
                VersionString = versionString
            });

        var mergedVimFilePath = Path.Combine(dir, "merged.vim");
        mergedDb.Write(mergedVimFilePath);
        var mergedVim = VimScene.LoadVim(mergedVimFilePath);
        mergedVim.Validate();

        // Deduplicate identical meshes using the transform service.

        var transformService = new TransformService(generatorString, versionString);

        var dedupDb = transformService.DeduplicateGeometry(mergedVim);
        var dedupVimFilePath = Path.Combine(dir, "dedup.vim");
        dedupDb.Write(dedupVimFilePath);
        var dedupVim = VimScene.LoadVim(dedupVimFilePath);
        dedupVim.Validate();

        Assert.Less(dedupVim.Meshes.Count, mergedVim.Meshes.Count);

        // Bonus: filter the deduplicated VIM and only keep the windows

        var filteredDb = transformService.Filter(dedupVim, n => n.CategoryName == "Casework");
        var filteredVimFilePath = Path.Combine(dir, "filtered.vim");
        filteredDb.Write(filteredVimFilePath);
        var filteredVim = VimScene.LoadVim(filteredVimFilePath);
        filteredVim.Validate();
    }
}
