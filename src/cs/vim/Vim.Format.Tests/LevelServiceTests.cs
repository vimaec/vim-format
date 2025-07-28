using NUnit.Framework;
using System.Linq;
using Vim.Format.Levels;
using Vim.Util.Tests;

namespace Vim.Format.Tests;

[TestFixture]
public static class LevelServiceTests
{
    [Test]
    public static void TestLevelInfo()
    {
        var ctx = new CallerTestContext();
        var dir = ctx.PrepareDirectory();
        var logger = ctx.CreateLogger();

        var vim = VimFormatRepoPaths.GetDataFilePath("Dwelling*.vim", true);

        var vimScene = VimScene.LoadVim(vim);

        var levelInfos = LevelService.GetLevelInfo(vimScene);

        foreach (var levelInfo in levelInfos.OrderBy(l => l.NameWithElevationFeetAndFractionalInches))
        {
            logger.Log($@"
{levelInfo}
");
        }
    }
}
