using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Vim.Util
{
    // ReSharper disable InconsistentNaming
    public static class MD5Extensions
    {
        public static byte[] MD5Hash(this byte[] bytes)
        {
            using (var md5 = MD5.Create())
                return md5.ComputeHash(bytes);
        }

        public const int DefaultMD5FileHashBlockSizeInBytes = 1000 * 1000; // 1MB

        public static async Task<byte[]> MD5HashAsync(
            this FileInfo fileInfo,
            IProgress<long> progress = null,
            CancellationToken token = default,
            int blockSizeInBytes = DefaultMD5FileHashBlockSizeInBytes)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, blockSizeInBytes, true)) // true means use IO async operations
                {
                    var buffer = new byte[blockSizeInBytes];
                    var totalBytesRead = 0;
                    int bytesRead;
                    do
                    {
                        bytesRead = await stream.ReadAsync(buffer, 0, blockSizeInBytes, token);
                        totalBytesRead += bytesRead;
                        if (bytesRead > 0)
                        {
                            progress?.Report(totalBytesRead);
                            md5.TransformBlock(buffer, 0, bytesRead, null, 0);
                        }
                    } while (bytesRead > 0);

                    md5.TransformFinalBlock(buffer, 0, 0);
                    return md5.Hash;
                }
            }
        }

        public static string ToBitConverterLowerInvariant(this byte[] bytes)
            => BitConverter.ToString(bytes).Replace("-", string.Empty).ToLowerInvariant();

        public static string MD5HashAsBitConverterLowerInvariant(this byte[] bytes)
            => bytes.MD5Hash().ToBitConverterLowerInvariant();

        public static string MD5HashAsBitConverterLowerInvariant(this string s)
            => s.ToBytesUtf8().MD5HashAsBitConverterLowerInvariant();

        public static string MD5HashAsBase64(this byte[] bytes)
            => bytes.MD5Hash().ToBase64();

        public static string MD5HashAsBase64(this string s)
            => s.ToBytesUtf8().MD5HashAsBase64();

        public static string MD5HashAsHex(this byte[] bytes)
            => bytes.MD5Hash().ToHex();

        public static string MD5HashAsHex(this string s)
            => s.ToBytesUtf8().MD5HashAsHex();

        public const int MD5HexHashLength = 32;

        private static readonly Regex HexStringRegex = new Regex("^[a-fA-F0-9]*$");

        /// <summary>
        /// Returns true if the string is a hex string representation of an md5 hash.
        /// </summary>
        public static bool IsMD5HexString(string value)
            => !string.IsNullOrEmpty(value) &&
               value.Length == MD5HexHashLength &&
               HexStringRegex.IsMatch(value);
    }
}
