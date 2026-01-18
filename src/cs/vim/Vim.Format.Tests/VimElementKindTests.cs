using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using Vim.Util.Tests;

namespace Vim.Format.Tests;

[TestFixture]
public static class VimElementKindTests
{
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
