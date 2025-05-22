using NUnit.Framework;
using System.IO;
using Vim.Util.Logging;

namespace Vim.Format.ILMerge.Tests;

[TestFixture]
public static class VimFormatILMergeTests
{
    [Test]
    public static void TestVimFormatStandalone()
    {
        // This test makes use of the merged version of serilog.
        // We had to create a merged standalone version of Vim.Format to minimize collisions with other assemblies in Revit.
        var fileName = "standalone.log";

        var logger = Util.Logging.Serilog.Log.Init("toot", fileName, true);
        logger.LogInformation("this is a log message for the baked-in version of serilog");

        var fileInfo = new FileInfo(fileName);
        Assert.IsTrue(fileInfo.Exists, $"File not found: {fileInfo.FullName}");
    }
}
