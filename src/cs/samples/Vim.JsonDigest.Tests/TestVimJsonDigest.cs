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
        [TestCase("https://storage.cdn.vimaec.com/samples/RoomTest.vim")]
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
            var vimJsonDigest = new VimJsonDigest(memoryStream, "");

            // Assert the contained collections are not empty.
            Assert.IsNotEmpty(vimJsonDigest.BimDocumentDigestCollection, "BimDocument digest collection is empty.");
            Assert.IsNotEmpty(vimJsonDigest.ElementDigestCollection, "Element digest collection is empty.");
            Assert.IsNotEmpty(vimJsonDigest.LevelDigestCollection, "Level digest collection is empty.");
            Assert.IsNotEmpty(vimJsonDigest.RoomDigestCollection, "Room digest collection is empty.");
            Assert.IsNotEmpty(vimJsonDigest.MaterialDigestCollection, "Material digest collection is empty.");
            Assert.IsNotEmpty(vimJsonDigest.AreaDigestCollection, "Area digest collection is empty.");

            // Assert that we can read the json content and reconstruct the VIM json digest.
            var jsonContent = vimJsonDigest.ToJson();
            var readVimJsonDigest = VimJsonDigest.FromJson(jsonContent);
            Assert.AreEqual(vimJsonDigest.BimDocumentDigestCollection.Count, readVimJsonDigest.BimDocumentDigestCollection.Count);
            Assert.AreEqual(vimJsonDigest.ElementDigestCollection.Count, readVimJsonDigest.ElementDigestCollection.Count);
            Assert.AreEqual(vimJsonDigest.LevelDigestCollection.Count, readVimJsonDigest.LevelDigestCollection.Count);
            Assert.AreEqual(vimJsonDigest.RoomDigestCollection.Count, readVimJsonDigest.RoomDigestCollection.Count);
            Assert.AreEqual(vimJsonDigest.AreaDigestCollection.Count, readVimJsonDigest.AreaDigestCollection.Count);
            Assert.AreEqual(vimJsonDigest.MaterialDigestCollection.Count, readVimJsonDigest.MaterialDigestCollection.Count);
        }
    }
}
