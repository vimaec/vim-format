using NUnit.Framework;
using System.IO;
using Vim.Util;
using Vim.Util.Logging;
using Serilog;

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
        var msg = "This is a log message which uses the ILMerged version of serilog";

        {
            // setup
            IO.Delete(fileName);
            Assert.IsTrue(!File.Exists(fileName));
        }

        {
            // action
            var logger = Util.Logging.Serilog.Log.Init("toot", fileName, true);
            logger.LogInformation(msg);
            Log.CloseAndFlush();
        }

        {
            // verify
            var fileInfo = new FileInfo(fileName);
            Assert.IsTrue(fileInfo.Exists, $"File not found: {fileInfo.FullName}");
            var logged = File.ReadAllText(fileInfo.FullName);
            Assert.IsTrue(logged.Contains(msg));
        }
    }
}
