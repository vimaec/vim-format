using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.Format.ElementParameterInfo;
using Vim.Format.ObjectModel;
using Vim.Util.Logging;
using Vim.Util.Tests;

namespace Vim.Format.Tests;

[TestFixture]
public static class ElementParameterInfoServiceTests
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

        var stringTable = vimFileInfo.GetStringTable();

        var infos = ElementParameterInfoService.GetElementParameterInfos(vimFileInfo, stringTable);
        var levelInfos = infos.LevelInfos;
        var elementLevelInfos = infos.ElementLevelInfos;
        var elementMeasureInfos = infos.ElementMeasureInfos;
        var parameterMeasureTypes = infos.ParameterMeasureTypes;

        var validationTableSet = new EntityTableSet(
            vimFileInfo,
            stringTable,
            n => n is TableNames.Level or TableNames.Element or TableNames.FamilyInstance or TableNames.Parameter);

        Assert.AreEqual(validationTableSet.LevelTable.RowCount, levelInfos.Length);

        var elementInstanceCount = validationTableSet.ElementTable.RowCount;
        Assert.AreEqual(elementInstanceCount, elementLevelInfos.Length);
        Assert.AreEqual(elementInstanceCount, elementMeasureInfos.Length);

        var parameterCount = validationTableSet.ParameterTable.RowCount;
        Assert.AreEqual(parameterCount, parameterMeasureTypes.Length);

        var familyInstanceElementMap = validationTableSet.ElementIndexMaps.FamilyInstanceIndexFromElementIndex;

        var knownFamilyInstanceCount = elementLevelInfos.Count(eli => familyInstanceElementMap.ContainsKey(eli.GetElementIndexOrNone()) && eli.PrimaryLevelKind != PrimaryLevelKind.Unknown);
        var unknownFamilyInstanceCount = elementLevelInfos.Count(eli => familyInstanceElementMap.ContainsKey(eli.GetElementIndexOrNone()) && eli.PrimaryLevelKind == PrimaryLevelKind.Unknown);
        Assert.GreaterOrEqual(knownFamilyInstanceCount, unknownFamilyInstanceCount);

        if (familyInstanceElementMap.Count > 0)
            Assert.Greater(knownFamilyInstanceCount, 0);
    }
}
