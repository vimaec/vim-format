using System;
using System.IO;
using System.IO.Compression;

namespace Vim.Format.Vimx
{
    public static class FileUtils
    {
        public static byte[] Deflate(this byte[] inputBytes)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var gzipStream = new DeflateStream(memoryStream, CompressionMode.Compress, true))
                {
                    gzipStream.Write(inputBytes, 0, inputBytes.Length);
                }
                return memoryStream.ToArray();
            }
        }

        public static byte[] Inflate(this byte[] inputBytes)
        {
            using (var input = new MemoryStream(inputBytes))
            {
                using (var output = new MemoryStream())
                {
                    using (var deflate = new DeflateStream(input, CompressionMode.Decompress, true))
                    {
                        deflate.CopyTo(output);
                        return output.ToArray();
                    }
                }
            }

        }

    }
}
