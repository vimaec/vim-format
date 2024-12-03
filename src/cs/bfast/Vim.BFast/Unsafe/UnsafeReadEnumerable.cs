using System;
using System.Collections.Generic;
using System.IO;

namespace Vim.BFastLib.Core
{
    public static class UnsafeReadEnumerable
    {
        /// <summary>
        /// Reads the next byteLength bytes from the stream and return the result as an enumerable of T
        /// Throws if byteLength is not a multiple of T size.
        /// </summary>
        public static IEnumerable<T> ReadEnumerableByte<T>(this Stream stream, long byteLength, int bufferSize = 4096) where T : unmanaged
        {
            var count = GetTCount<T>(byteLength);
            return ReadEnumerable<T>(stream, count, bufferSize);
        }

        /// <summary>
        /// Reads the next count values of T from the stream as an enumerable.
        /// </summary>
        public static IEnumerable<T> ReadEnumerable<T>(this Stream stream, long count, int bufferSize = 4096) where T : unmanaged
        {
            var remaining = count;
            var (array, buffer) = AllocBuffers<T>(bufferSize);

            while (remaining > 0)
            {
                var toRead = (int)Math.Min(bufferSize, remaining);
                var read = FillArray(stream, toRead, array, buffer);

                for (var i = 0; i < read; i++)
                {
                    yield return array[i];
                }
                remaining -= read;
            }
        }

        // Function is extracted because unsafe code cannot appear in generator
        private static unsafe long GetTCount<T>(long byteLength) where T : unmanaged
        {
            if (byteLength % sizeof(T) != 0)
            {
                throw new Exception("Byte length must be a multiple of T size.");
            }
            return byteLength / sizeof(T);
        }

        // Function is extracted because unsafe code cannot appear in generator
        private static unsafe (T[], byte[]) AllocBuffers<T>(int count) where T : unmanaged
        {
            return (new T[count], new byte[count * sizeof(T)]);
        }

        // Function is extracted because unsafe code cannot appear in generator
        private static unsafe int FillArray<T>(Stream stream, int count, T[] array, byte[] buffer) where T : unmanaged
        {
            fixed (T* pDestTyped = array)
            fixed (byte* pBuffer = buffer)
            {
                var pDestBytes = (byte*)pDestTyped;
                var toRead = Math.Min(buffer.Length, count * sizeof(T));
                var bytesRead = stream.Read(buffer, 0, toRead);
                Buffer.MemoryCopy(pBuffer, pDestTyped, array.Length * sizeof(T), bytesRead);
                return bytesRead / sizeof(T);
            }
        }
    }
}
