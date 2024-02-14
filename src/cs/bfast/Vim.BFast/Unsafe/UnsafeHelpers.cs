using System;
using System.Collections.Generic;
using System.IO;

namespace Vim.BFastLib.Core
{
    public static class UnsafeHelpers
    {
        /// <summary>
        /// Returns an enumeration of chunks of the given size from the given enumeration.
        /// </summary>
        public static IEnumerator<(T[], int)> Chunkify<T>(IEnumerable<T> source, int chunkSize = 1048576)
        {
            var chunk = new T[chunkSize];
            var index = 0;

            foreach (var item in source)
            {
                chunk[index++] = item;

                if (index == chunkSize)
                {
                    yield return (chunk, index);
                    index = 0;
                }
            }

            if (index > 0)
            {
                yield return (chunk, index);
            }
        }

        public static void CopySome(this Stream input, Stream output, int bytes, int bufferSize = 32768)
        {
            var buffer = new byte[bufferSize];
            int read;
            while (bytes > 0 &&
                   (read = input.Read(buffer, 0, Math.Min(buffer.Length, bytes))) > 0)
            {
                output.Write(buffer, 0, read);
                bytes -= read;
            }
        }
    }
}
