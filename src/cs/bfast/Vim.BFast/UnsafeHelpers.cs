﻿using System;
using System.IO;

namespace Vim.BFast
{
    /// <summary>
    /// This class would benefit from being in a generic utilities class, however, having it here allows BFAST to be a standalone without dependencies.
    /// </summary>
    public static class UnsafeHelpers
    {
        /// <summary>
        /// Helper for reading arbitrary unmanaged types from a Stream. 
        /// </summary>
        public static unsafe void ReadBytesBuffered(this Stream stream, byte* dest, long count, int bufferSize = 4096)
        {
            var buffer = new byte[bufferSize];
            int bytesRead;
            fixed (byte* pBuffer = buffer)
            {
                while ((bytesRead = stream.Read(buffer, 0, (int)Math.Min(buffer.Length, count))) > 0)
                {
                    if (dest != null)
                        Buffer.MemoryCopy(pBuffer, dest, count, bytesRead);
                    count -= bytesRead;
                    dest += bytesRead;
                }
            }
        }

        /// <summary>
        /// Helper for writing arbitrary large numbers of bytes 
        /// </summary>
        public static unsafe void WriteBytesBuffered(this Stream stream, byte* src, long count, int bufferSize = 4096)
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

        /// <summary>
        /// Helper for reading arbitrary unmanaged types from a Stream. 
        /// </summary>
        public static unsafe void Read<T>(this Stream stream, T* dest) where T : unmanaged
            => stream.ReadBytesBuffered((byte*)dest, sizeof(T));

        /// <summary>
        /// Helper for reading arrays of arbitrary unmanaged types from a Stream, that might be over 2GB of size.
        /// That said, in C#, you can never load more int.MaxValue numbers of items.
        /// NOTE: Arrays are still limited to 2gb in size unless gcAllowVeryLargeObjects is set to true
        /// in the runtime environment.
        /// https://docs.microsoft.com/en-us/dotnet/api/system.array?redirectedfrom=MSDN&view=netframework-4.7.2#remarks
        /// Alternatively, we could convert to .Net Core
        /// </summary>
        public static unsafe T[] ReadArray<T>(this Stream stream, int count) where T : unmanaged
        {
            var r = new T[count];
            fixed (T* pDest = r)
            {

                var pBytes = (byte*)pDest;
                stream.ReadBytesBuffered(pBytes, (long)count * sizeof(T));
            }
            return r;
        }

        /// <summary>
        /// A wrapper for stream.Seek(numBytes, SeekOrigin.Current) to avoid allocating memory for unrecognized buffers.
        /// </summary>
        public static void SkipBytes(this Stream stream, long numBytes)
            => stream.Seek(numBytes, SeekOrigin.Current);

        /// <summary>
        /// Helper for reading arrays of arbitrary unmanaged types from a Stream, that might be over 2GB of size.
        /// That said, in C#, you can never load more int.MaxValue numbers of items. 
        /// </summary>
        public static unsafe T[] ReadArrayFromNumberOfBytes<T>(this Stream stream, long numBytes) where T : unmanaged
        {
            var count = numBytes / sizeof(T);
            if (numBytes % sizeof(T) != 0)
                throw new Exception($"The number of bytes {numBytes} is not divisible by the size of the type {sizeof(T)}");
            if (count >= int.MaxValue)
                throw new Exception($"{count} exceeds the maximum number of items that can be read into an array {int.MaxValue}");
            return stream.ReadArray<T>((int)count);
        }

        /// <summary>
        /// Helper for writing arbitrary unmanaged types 
        /// </summary>
        public static unsafe void WriteValue<T>(this Stream stream, T x) where T : unmanaged
        {
            var p = &x;
            stream.WriteBytesBuffered((byte*)p, sizeof(T));
        }
            

        /// <summary>
        /// Helper for writing arrays of unmanaged types 
        /// </summary>
        public static unsafe void Write<T>(this Stream stream, T[] xs) where T : unmanaged
        {
            fixed (T* p = xs)
            {
                stream.WriteBytesBuffered((byte*)p, xs.LongLength * sizeof(T));
            }
        }
    }
}
