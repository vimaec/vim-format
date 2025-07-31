using NUnit.Framework;
using System.IO;
using System.Linq;
using Vim.Format.Levels;
using Vim.Util.Logging;
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

        // TODO: test skanska
        // TODO: test with an IFC file
        // TODO: test with empty VIM file
        

        Assert.DoesNotThrow(() =>
        {
            using var _ = logger.LogDuration("GetLevelInfo");

            var vimFilePath = VimFormatRepoPaths.GetDataFilePath("Dwelling*.vim", true);
            var vimSchemaOnly = Serializer.Deserialize(vimFilePath, new() { SchemaOnly = true, SkipAssets = true });

            var levelService = new LevelInfoService(new FileInfo(vimFilePath));
            var (levelInfos, familyInstanceLevelInfos) = LevelService.GetLevelInfo(vimScene);

            foreach (var levelInfo in levelInfos.OrderBy(l => l.NameWithElevationFeetAndFractionalInches))
            {
                logger.Log($@"
{levelInfo.PropertiesToString()}
");
            }

            foreach (var familyInstanceInfo in familyInstanceLevelInfos)
            {
                logger.Log($@"
{familyInstanceInfo.PropertiesToString()}
");
            }
        });
    }
}
