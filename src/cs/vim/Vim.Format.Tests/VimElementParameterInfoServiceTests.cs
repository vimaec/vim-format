using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.Format.ElementParameterInfo;
using Vim.Util.Logging;
using Vim.Util.Tests;

namespace Vim.Format.Tests;

[TestFixture]
public static class VimElementParameterInfoServiceTests
{
    public static IEnumerable<string> TestVimFilePaths => TestFiles.VimFilePaths;

    [TestCaseSource(nameof(TestVimFilePaths))]
    public static void TestElementParameterInfoService(string vimFilePath)
    {
        var fileName = Path.GetFileName(vimFilePath);
        var ctx = new CallerTestContext(subDirComponents: fileName);
        var dir = ctx.PrepareDirectory();
        var logger = ctx.CreateLogger();

        using var _ = logger.LogDuration($"{nameof(TestElementParameterInfoService)}: {vimFilePath}");

        var vimFileInfo = new FileInfo(vimFilePath);
        var vim = VIM.Open(vimFileInfo);

        var stringTable = vim.StringTable;

        var infos = VimElementParameterInfoService.GetElementParameterInfos(vimFileInfo, stringTable);
        var levelInfos = infos.LevelInfos;
        var elementLevelInfos = infos.ElementLevelInfos;
        var elementMeasureInfos = infos.ElementMeasureInfos;
        var elementIfcInfos = infos.ElementIfcInfos;
        var parameterMeasureTypes = infos.ParameterMeasureTypes;

        var validationTableSet = vim.GetEntityTableSet();

        Assert.AreEqual(validationTableSet.LevelTable.RowCount, levelInfos.Length);

        var elementInstanceCount = validationTableSet.ElementTable.RowCount;
        Assert.AreEqual(elementInstanceCount, elementLevelInfos.Length);
        Assert.AreEqual(elementInstanceCount, elementMeasureInfos.Length);
        Assert.AreEqual(elementInstanceCount, elementIfcInfos.Length);

        var parameterCount = validationTableSet.ParameterTable.RowCount;
        Assert.AreEqual(parameterCount, parameterMeasureTypes.Length);

        var familyInstanceElementMap = validationTableSet.ElementIndexMaps.FamilyInstanceIndexFromElementIndex;

        var knownFamilyInstanceCount = elementLevelInfos.Count(eli => familyInstanceElementMap.ContainsKey(eli.GetElementIndexOrNone()) && eli.PrimaryLevelKind != PrimaryLevelKind.Unknown);
        var unknownFamilyInstanceCount = elementLevelInfos.Count(eli => familyInstanceElementMap.ContainsKey(eli.GetElementIndexOrNone()) && eli.PrimaryLevelKind == PrimaryLevelKind.Unknown);
        Assert.GreaterOrEqual(knownFamilyInstanceCount, unknownFamilyInstanceCount);

        if (familyInstanceElementMap.Count > 0)
            Assert.Greater(knownFamilyInstanceCount, 0);
    }

    [Test]
    public static void TestIfcGuidParseRoundTrip()
    {
        var testGuids = Enumerable.Range(0, 10000).Select(i => Guid.NewGuid()).Prepend(Guid.Empty);

        foreach (var guid in testGuids)
        {
            Assert.AreEqual(ElementIfcInfo.IfcGuidCanonicalLength, guid.ToString().Length);

            var ifcGuid = ElementIfcInfo.ToIfcGuid(guid);
            Assert.IsFalse(string.IsNullOrEmpty(ifcGuid), $"Converted IFC guid is null or empty. Source: {guid.ToString()}");
            Assert.AreEqual(ElementIfcInfo.IfcGuidLength, ifcGuid.Length, $"Converted IFC must be 22 characters long. Source: {guid.ToString()} | IFC Guid: {ifcGuid}");
            Assert.IsTrue(ifcGuid.All(c => ElementIfcInfo.Base64Chars.IndexOf(c) != -1), $"All IFC guid characters must be in the Base64Chars string. Source: {guid.ToString()} | IFC Guid: {ifcGuid}");
            Assert.IsTrue(ElementIfcInfo.TryParseIfcGuidAsCanonicalGuid(ifcGuid, out var parsedGuid), $"Failed to parse IFC Guid. Source: {guid.ToString()} | IFC Guid: {ifcGuid}");
            Assert.AreEqual(guid, parsedGuid);
        }
    }

    [Test]
    public static void TestUniformatLevelSplit()
    {
        var valid = "B1010240";

        Assert.AreEqual("B", FamilyTypeUniformatInfo.GetUniformatLevel1(valid));
        Assert.AreEqual("B10", FamilyTypeUniformatInfo.GetUniformatLevel2(valid));
        Assert.AreEqual("B1010", FamilyTypeUniformatInfo.GetUniformatLevel3(valid));

        var empty = "";
        Assert.AreEqual("", FamilyTypeUniformatInfo.GetUniformatLevel1(empty));
        Assert.AreEqual("", FamilyTypeUniformatInfo.GetUniformatLevel2(empty));
        Assert.AreEqual("", FamilyTypeUniformatInfo.GetUniformatLevel3(empty));

        var incomplete1 = "B";
        Assert.AreEqual("B", FamilyTypeUniformatInfo.GetUniformatLevel1(incomplete1));
        Assert.AreEqual("", FamilyTypeUniformatInfo.GetUniformatLevel2(incomplete1));
        Assert.AreEqual("", FamilyTypeUniformatInfo.GetUniformatLevel3(incomplete1));

        var partial = "B20";
        Assert.AreEqual("B", FamilyTypeUniformatInfo.GetUniformatLevel1(partial));
        Assert.AreEqual("B20", FamilyTypeUniformatInfo.GetUniformatLevel2(partial));
        Assert.AreEqual("", FamilyTypeUniformatInfo.GetUniformatLevel3(partial));
    }


    public static IEnumerable<(List<string>, int, int, int, int)> TestSortAndGetCountCases = new[]
    {
        (new List<string> {  }, 0, 0, 0, 0),
        (new List<string> { "a" }, 1, 1, 0, 1),
        (new List<string> { "" }, 1, 0, 0, 1),
        (new List<string> { "", "" }, 2, 0, 0, 1),
        (new List<string> { "a", "" }, 2, 1, 0, 2),
        (new List<string> { "-1" }, 1, 1, 1, 1),
        (new List<string> { "-1", "" }, 2, 1, 1, 2),
        (new List<string> { "-1", "0" }, 2, 2, 2, 2),
        (new List<string> { "0", "0" }, 2, 2, 2, 1),
        (new List<string> { "a", "b", "c", "d" }, 4, 4, 0, 4),
        (new List<string> { "a", "a", "a", "a" }, 4, 4, 0, 1),
        (new List<string> { "a", "b", "b", "a" }, 4, 4, 0, 2),
        (new List<string> { "", "a", "0", "-1" }, 4, 3, 2, 4),
    };

    [TestCaseSource(nameof(TestSortAndGetCountCases))]
    public static void TestSortAndGetCounts((List<string>, int, int, int, int) item)
    {
        ParameterSummary.SortAndGetCounts(item.Item1, out var countTotal, out var countFilled, out var countSuspicious, out var countDistinct);

        Assert.AreEqual(item.Item2, countTotal);
        Assert.AreEqual(item.Item3, countFilled);
        Assert.AreEqual(item.Item4, countSuspicious);
        Assert.AreEqual(item.Item5, countDistinct);
    }
}
