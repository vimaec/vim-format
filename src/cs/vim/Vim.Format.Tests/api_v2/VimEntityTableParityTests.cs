using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using Vim.Format;
using Vim.Format.ObjectModel;
using Vim.LinqArray;
using Vim.Util.Tests;

namespace Vim.Format.Tests;

[TestFixture]
public static class VimEntityTableParityTests
{
    [Test]
    public static void VimEntityTableParityTest()
    {
        var vimFilePath = VimFormatRepoPaths.GetDataFilePath("Dwelling*.vim", true);

        // Classic deserialization (using VIM Scene)
        var vim = VimScene.LoadVim(vimFilePath);
        var stringBuffer = vim.Document.StringTable.ToArray();
        var dm = vim.DocumentModel;

        // EntityTable_v2 manual construction.
        var fileInfo = new FileInfo(vimFilePath);
        var vim_v2 = VIM.Open(fileInfo);
        var entityTableSet = vim_v2.GetEntityTableSet();

        var baseElementCount = dm.NumElement;
        var nextElementCount = entityTableSet.ElementTable.RowCount;

        Assert.AreEqual(baseElementCount, nextElementCount);

        for (var i = 0; i < baseElementCount; ++i)
        {
            var @base = dm.ElementList[i];
            var next = entityTableSet.ElementTable.Get(i);

            Assert.AreEqual(@base.Index, next.Index);
            Assert.AreEqual(@base.Id, next.Id);
            Assert.AreEqual(@base.Type, next.Type);
            Assert.AreEqual(@base.UniqueId, next.UniqueId);
            Assert.AreEqual(@base.Location_X, next.Location_X);
            Assert.AreEqual(@base.Location_Y, next.Location_Y);
            Assert.AreEqual(@base.Location_Z, next.Location_Z);
            Assert.AreEqual(@base.FamilyName, next.FamilyName);
            Assert.AreEqual(@base.IsPinned, next.IsPinned);
            Assert.AreEqual(@base._Level.Index, next._Level.Index);
            Assert.AreEqual(@base._PhaseCreated.Index, next._PhaseCreated.Index);
            Assert.AreEqual(@base._PhaseDemolished.Index, next._PhaseDemolished.Index);
            Assert.AreEqual(@base._Category.Index, next._Category.Index);
            Assert.AreEqual(@base._Workset.Index, next._Workset.Index);
            Assert.AreEqual(@base._DesignOption.Index, next._DesignOption.Index);
            Assert.AreEqual(@base._OwnerView.Index, next._OwnerView.Index);
            Assert.AreEqual(@base._Group.Index, next._Group.Index);
            Assert.AreEqual(@base._AssemblyInstance.Index, next._AssemblyInstance.Index);
            Assert.AreEqual(@base._BimDocument.Index, next._BimDocument.Index);
            Assert.AreEqual(@base._Room.Index, next._Room.Index);
        }
    }

    [Test]
    public static void TestElementTableKinds()
    {
        var fileInfo = new FileInfo(VimFormatRepoPaths.GetDataFilePath("Dwelling*.vim", true));

        Console.WriteLine($"Loading {fileInfo.FullName}");

        var elementKinds = VimEntityTableSet.GetElementKinds(fileInfo);

        var ets = VIM.Open(fileInfo).GetEntityTableSet();

        Assert.IsNotEmpty(elementKinds);
        Assert.AreEqual(ets.ElementTable.RowCount, elementKinds.Length);
        
        var familyInstanceCount = elementKinds.Count(e => e == ElementKind.FamilyInstance);
        Assert.Greater(familyInstanceCount, 0);
        Assert.AreEqual(ets.FamilyInstanceTable.Column_ElementIndex.Distinct().Count(ei => ei != EntityRelation.None), familyInstanceCount);
        Assert.IsTrue(ets.FamilyInstanceTable.Column_ElementIndex.All(ei => ei == EntityRelation.None || elementKinds[ei] == ElementKind.FamilyInstance));

        var familyTypeCount = elementKinds.Count(e => e == ElementKind.FamilyType);
        Assert.Greater(familyTypeCount, 0);
        Assert.AreEqual(ets.FamilyTypeTable.Column_ElementIndex.Distinct().Count(ei => ei != EntityRelation.None), familyTypeCount);
        Assert.IsTrue(ets.FamilyTypeTable.Column_ElementIndex.All(ei => ei == EntityRelation.None || elementKinds[ei] == ElementKind.FamilyType));

        var familyCount = elementKinds.Count(e => e == ElementKind.Family);
        Assert.Greater(familyCount, 0);
        Assert.AreEqual(ets.FamilyTable.Column_ElementIndex.Distinct().Count(ei => ei != EntityRelation.None), familyCount);
        Assert.IsTrue(ets.FamilyTable.Column_ElementIndex.All(ei => ei == EntityRelation.None || elementKinds[ei] == ElementKind.Family));
    }
}
