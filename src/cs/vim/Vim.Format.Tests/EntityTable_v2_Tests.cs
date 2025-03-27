using NUnit.Framework;
using System.IO;
using Vim.Format.ObjectModel;
using Vim.LinqArray;
using Vim.Util.Tests;

namespace Vim.Format.Tests;

[TestFixture]
public static class EntityTable_v2_Tests
{
    [Test]
    public static void TestEntityTable_v2_Parity()
    {
        var vimFilePath = VimFormatRepoPaths.GetLatestWolfordResidenceVim();

        // Classic deserialization (using VIM Scene)
        var vim = VimScene.LoadVim(vimFilePath);
        var stringBuffer = vim.Document.StringTable.ToArray();
        var dm = vim.DocumentModel;

        // EntityTable_v2 manual construction.
        var fileInfo = new FileInfo(vimFilePath);
        var entityTableSet = new EntityTableSet(fileInfo, false, stringBuffer);

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
}
