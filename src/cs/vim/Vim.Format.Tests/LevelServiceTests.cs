using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vim.Format.Levels;
using Vim.LinqArray;
using Vim.Util;
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

        var vim = VimFormatRepoPaths.GetLatestWolfordResidenceVim();

        var vimScene = VimScene.LoadVim(vim);

        var dm = vimScene.DocumentModel;

        var levelInfos = dm.LevelList.Select(l => new LevelInfo(dm, l)).ToArray();

        foreach (var levelInfo in levelInfos)
        {
            logger.Log($@"
{levelInfo}
");
        }
    }
}
