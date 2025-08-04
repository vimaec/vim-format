using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.Format.Levels;
using Vim.Format.ObjectModel;
using Vim.Util.Logging;
using Vim.Util.Tests;

namespace Vim.Format.Tests;

[TestFixture]
public static class LevelInfoServiceTests
{
    public static IEnumerable<string> TestVimFilePaths => TestFiles.VimFilePaths;

    [TestCaseSource(nameof(TestVimFilePaths))]
    public static void TestLevelInfoService(string vimFilePath)
    {
        var fileName = Path.GetFileName(vimFilePath);
        var ctx = new CallerTestContext(subDirComponents: fileName);
        var dir = ctx.PrepareDirectory();
        var logger = ctx.CreateLogger();

        using var _ = logger.LogDuration($"GetLevelInfo: {vimFilePath}");

        var vimFileInfo = new FileInfo(vimFilePath);

        var stringTable = vimFileInfo.GetStringTable();

        var (levelInfos, familyInstanceLevelInfos) = LevelInfoService.GetLevelInfos(vimFileInfo, stringTable);

        var validationTableSet = new EntityTableSet(
            vimFileInfo,
            stringTable,
            n => n is TableNames.Level or TableNames.FamilyInstance);

        Assert.AreEqual(validationTableSet.LevelTable.RowCount, levelInfos.Length);
        
        var familyInstanceCount = validationTableSet.FamilyInstanceTable.RowCount;
        Assert.AreEqual(familyInstanceCount, familyInstanceLevelInfos.Length);

        var knownCount = familyInstanceLevelInfos.Count(fi => fi.PrimaryLevelKind != PrimaryLevelKind.Unknown);
        var unknownCount = familyInstanceLevelInfos.Count(fi => fi.PrimaryLevelKind == PrimaryLevelKind.Unknown);
        Assert.GreaterOrEqual(knownCount, unknownCount);

        if (familyInstanceCount > 0)
            Assert.Greater(knownCount, 0);
    }
}
