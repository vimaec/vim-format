using NUnit.Framework;
using System.IO;
using System.Linq;
using Vim.LinqArray;
using Vim.Util.Tests;

namespace Vim.Format.Tests;

[TestFixture]
public static class TransformServiceTests
{
    [Test]
    public static void TestTransformService()
    {
        var ctx = new CallerTestContext();
        var dir = ctx.PrepareDirectory();

        var vimFilePath = Path.Combine(VimFormatRepoPaths.DataDir, "RoomTest.vim");
        var vim = VimScene.LoadVim(vimFilePath);

        // Just keep the walls.
        var transformService = new TransformService(nameof(TestTransformService), "0.0.0");
        var db = transformService.Transform(
            vim,
            n => n.CategoryName == "Walls",
            null,
            null,
            null,
            false);

        var transformedVimFilePath = Path.Combine(dir, "rooms_transformed.vim");
        db.Write(transformedVimFilePath);

        Assert.IsTrue(File.Exists(transformedVimFilePath));

        var transformedVim = VimScene.LoadVim(transformedVimFilePath);
        Assert.IsTrue(transformedVim.VimNodes.ToEnumerable().All(n => n.CategoryName == "Walls"));
    }
}
