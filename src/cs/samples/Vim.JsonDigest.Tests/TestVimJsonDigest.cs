using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;
using Vim.Util.Logging;
using Vim.Util.Tests;

namespace Vim.JsonDigest.Tests
{
    [TestFixture]
    public static class TestVimJsonDigest
    {
        [Test]
        public static async Task Test_VimJsonDigest()
        {
            // Initialize an empty test directory in <repo_root>/out/_tests/TestVimJsonDigest/Test_VimJsonDigest/
            var ctx = new CallerTestContext();
            var testDir = ctx.PrepareDirectory();
            var logger = ctx.CreateLogger();

            // Obtain a seekable stream to the VIM file.
            //var vimFileInfo = new FileInfo(Path.Combine(VimFormatRepoPaths.DataDir, "RoomTest.vim"));
            var url = "https://github.com/vimaec/vim-format/raw/mavimaec/samples/data/RoomTest.vim";
            
            using var memoryStream = new MemoryStream();
            var progressMessage = 0;
            var progress = new Progress<double>(p =>
            {
                // TODO: why is this not showing up in order???
                logger.LogInformation($"[{progressMessage++}] Downloading {p:P}");
            });
            await Util.Http.DownloadAsync(url, memoryStream, progress, bufferSize: Util.Http.DefaultDownloadBufferSize * 10);
            
            //var vimFileInfo = new FileInfo(Path.Combine(VimFormatRepoPaths.DataDir, "RoomTest.vim"));
            //using var fileStream = vimFileInfo.OpenRead();

            // Create a VimJsonDigest with the given stream.
            var vimJsonDigest = new VimJsonDigest(memoryStream);
            var jsonContent = vimJsonDigest.ToJson();

            Assert.IsNotEmpty(vimJsonDigest.RoomInfoCollection, "Room info collection is empty.");
            Assert.IsNotEmpty(vimJsonDigest.MaterialInfoCollection, "Material info collection is empty.");
            Assert.IsNotEmpty(vimJsonDigest.AreaInfoCollection, "Area info collection is empty.");

            // Write the digest to a file.
            var outputJsonFilePath = Path.Combine(testDir, "vim-digest.json");
            File.WriteAllText(outputJsonFilePath, jsonContent);
            Assert.IsTrue(File.Exists(outputJsonFilePath), "VIM json digest file not found.");

            // Assert that we can read the json file path and reconstruct the VIM json digest.
            var readJsonContent = File.ReadAllText(outputJsonFilePath);
            var readVimJsonDigest = VimJsonDigest.FromJson(readJsonContent);
            Assert.AreEqual(vimJsonDigest.RoomInfoCollection.Count, readVimJsonDigest.RoomInfoCollection.Count);
            Assert.AreEqual(vimJsonDigest.AreaInfoCollection.Count, readVimJsonDigest.AreaInfoCollection.Count);
            Assert.AreEqual(vimJsonDigest.MaterialInfoCollection.Count, readVimJsonDigest.MaterialInfoCollection.Count);
        }
    }
}
