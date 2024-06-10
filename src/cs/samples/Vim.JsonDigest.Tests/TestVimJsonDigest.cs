using NUnit.Framework;
using System.IO;
using System.Threading.Tasks;
using Vim.Util;
using Vim.Util.Logging;

namespace Vim.JsonDigest.Tests
{
    [TestFixture]
    public static class TestVimJsonDigest
    {
        /// <summary>
        /// Validates that the given VIM file at the provided URL can be analyzed using a VimJsonDigest instance.
        /// </summary>
        [TestCase("https://vimdevelopment01storage.blob.core.windows.net/samples/RoomTest.vim")]
        public static async Task Test_VimJsonDigest(string url)
        {
            var logger = new StdLogger();

            // Download the given VIM file.
            using var memoryStream = new MemoryStream();
            using (var _ = logger.LogDuration($"Downloading VIM file from: {url}"))
            {
                var progress = new SynchronousProgress<double>(p => 
                    logger.LogInformation($"Downloading {p:P}"));

                var bytesDownloaded = await Http.DownloadAsync(url, memoryStream, progress, bufferSize: Http.DefaultDownloadBufferSize * 10);
                logger.LogInformation($"Downloaded {bytesDownloaded} bytes");
            }

            // Create a VIM json digest from the memory stream.
            var vimJsonDigest = new VimJsonDigest(memoryStream);

            // Assert the contained collections are not empty.
            Assert.IsNotEmpty(vimJsonDigest.RoomInfoCollection, "Room info collection is empty.");
            Assert.IsNotEmpty(vimJsonDigest.MaterialInfoCollection, "Material info collection is empty.");
            Assert.IsNotEmpty(vimJsonDigest.AreaInfoCollection, "Area info collection is empty.");

            // Assert that we can read the json content and reconstruct the VIM json digest.
            var jsonContent = vimJsonDigest.ToJson();
            var readVimJsonDigest = VimJsonDigest.FromJson(jsonContent);
            Assert.AreEqual(vimJsonDigest.RoomInfoCollection.Count, readVimJsonDigest.RoomInfoCollection.Count);
            Assert.AreEqual(vimJsonDigest.AreaInfoCollection.Count, readVimJsonDigest.AreaInfoCollection.Count);
            Assert.AreEqual(vimJsonDigest.MaterialInfoCollection.Count, readVimJsonDigest.MaterialInfoCollection.Count);
        }
    }
}
