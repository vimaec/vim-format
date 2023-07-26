using NUnit.Framework;

namespace Vim.Util.Tests
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    public static class MD5Tests
    {
        [Test]
        public static void TestMD5Hashes()
        {
            const string content = "I'm a little teapot!";

            var rawHash = content.ToBytesUtf8().MD5Hash();
            var md5base64 = content.MD5HashAsBase64();
            var md5Hex = content.MD5HashAsHex();

            Assert.AreEqual(rawHash.ToBase64(), md5base64);
            Assert.AreEqual(rawHash.ToHex(), md5Hex);
            Assert.AreEqual(md5Hex, md5base64.Base64ToHex());

            Assert.IsTrue(MD5Extensions.IsMD5HexString(md5Hex));
            Assert.IsTrue(MD5Extensions.IsMD5HexString(md5base64.Base64ToHex()));
        }
    }
}
