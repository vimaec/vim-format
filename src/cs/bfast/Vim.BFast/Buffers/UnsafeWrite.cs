using System;
using System.IO;

namespace Vim.BFastLib
{
    public static class UnsafeWrite
    {
        /// <summary>
        /// Converts given value to bytes and writes resulting bytes to the stream 
        /// </summary>
        public static unsafe void WriteValue<T>(this Stream stream, T x) where T : unmanaged
        {
            var p = &x;
            stream.WriteBytesBuffered((byte*)p, sizeof(T));
        }

        /// <summary>
        /// Converts values of the given array to bytes and writes the resulting bytes to the stream.
        /// </summary>
        public static unsafe void Write<T>(this Stream stream, T[] xs) where T : unmanaged
        {
            Write(stream, xs, xs.LongLength);
        }

        /// <summary>
        /// Converts the first Count elements of an array to bytes and writes the resulting bytes to the stream.
        /// </summary>
        public static unsafe void Write<T>(this Stream stream, T[] xs, long count) where T : unmanaged
        {
            fixed (T* p = xs)
            {
                stream.WriteBytesBuffered((byte*)p, count * sizeof(T));
            }
        }

        /// <summary>
        /// Writes an arbitrary large numbers of bytes to the stream.
        /// </summary>
        private static unsafe void WriteBytesBuffered(this Stream stream, byte* src, long count, int bufferSize = 4096)
        {
            var buffer = new byte[bufferSize];
            fixed (byte* pBuffer = buffer)
            {
                while (count > 0)
                {
                    var toWrite = (int)Math.Min(count, buffer.Length);
                    Buffer.MemoryCopy(src, pBuffer, buffer.Length, toWrite);
                    stream.Write(buffer, 0, toWrite);
                    count -= toWrite;
                    src += toWrite;
                }
            }
        }
    }
}
