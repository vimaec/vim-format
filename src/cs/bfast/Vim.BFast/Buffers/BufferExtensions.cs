﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.BFastLib.Core;

namespace Vim.BFastLib
{
    /// <summary>
    /// Helper functions for working with buffers 
    /// </summary>
    public static class BufferExtensions
    {
        public static Buffer<T> ToBuffer<T>(this T[] xs) where T : unmanaged
            => new Buffer<T>(xs);

        public static NamedBuffer<T> ToNamedBuffer<T>(this T[] xs, string name = "") where T : unmanaged
            => new NamedBuffer<T>(xs, name);

        public static NamedBuffer ToNamedBuffer(this IBuffer buffer, string name = "")
            => new NamedBuffer(buffer, name);

        public static NamedBuffer<T> ToNamedBuffer<T>(this IBuffer<T> xs, string name = "") where T : unmanaged
            => new NamedBuffer<T>(xs.GetTypedData(), name);

        public static IEnumerable<INamedBuffer> ToNamedBuffers(this IEnumerable<IBuffer> buffers, IEnumerable<string> names = null)
            => names == null ? buffers.Select(b => b.ToNamedBuffer("")) : buffers.Zip(names, ToNamedBuffer);

        public static IDictionary<string, INamedBuffer> ToDictionary(this IEnumerable<INamedBuffer> buffers)
            => buffers.ToDictionary(b => b.Name, b => b);

        public static IEnumerable<INamedBuffer> ToNamedBuffers(this IDictionary<string, IBuffer> d)
            => d.Select(kv => kv.Value.ToNamedBuffer(kv.Key));

        public static IEnumerable<INamedBuffer> ToNamedBuffers(this IDictionary<string, byte[]> d)
            => d.Select(kv => kv.Value.ToNamedBuffer(kv.Key));

        public static Array CopyBytes(this IBuffer src, Array dst, int srcOffset = 0, int destOffset = 0)
        {
            Buffer.BlockCopy(src.Data, srcOffset, dst, destOffset, (int)src.NumBytes());
            return dst;
        }

        public static byte[] ToBytes(this IBuffer src, byte[] dest = null)
            => src.ToArray(dest);

        public static byte[] ToBytes<T>(this T[] xs, byte[] dest = null) where T : unmanaged
            => xs.RecastArray(dest);

        public static byte[] ToBytes<T>(this T x) where T : unmanaged
            => ToBytes(new[] { x });

        /// <summary>
        /// Accepts an array of the given type, or creates one if necessary, copy the buffer data into it 
        /// </summary>
        public static unsafe T[] ToArray<T>(this IBuffer buffer, T[] dest = null) where T : unmanaged
            => (T[])buffer.CopyBytes(dest ?? new T[buffer.NumBytes() / sizeof(T)]);

        /// <summary>
        /// Returns the array in the buffer, if it is of the correct type, or creates a new array of the create type and copies
        /// bytes into it, as necessary. 
        /// </summary>
        public static unsafe T[] AsArray<T>(this IBuffer buffer) where T : unmanaged
            => buffer.Data is T[] r ? r : buffer.ToArray<T>();

        /// <summary>
        /// Copies an array of unmanaged types into another array of unmanaged types
        /// </summary>
        public static unsafe U[] RecastArray<T, U>(this T[] src, U[] r = null) where T : unmanaged where U : unmanaged
            => src.ToBuffer().ToArray(r);

        public static int NumElements(this IBuffer buffer)
            => buffer.Data.Length;

        public static long NumBytes(this IBuffer buffer)
            => (long)buffer.NumElements() * buffer.ElementSize;

        public static Buffer<T> ReadBufferFromNumberOfBytes<T>(this Stream stream, long numBytes) where T : unmanaged
            => stream.ReadArrayBytes<T>(numBytes).ToBuffer();

        public static Buffer<T> ReadBuffer<T>(this Stream stream, int numElements) where T : unmanaged
            => stream.ReadArray<T>(numElements).ToBuffer();

        public static Buffer<byte> ReadBuffer(this Stream stream, int numBytes)
            => stream.ReadBuffer<byte>(numBytes);

        public static void Write(this Stream stream, IBuffer buffer)
            => buffer.Write(stream);

        public static NamedBuffer<T> Fill<T>(this NamedBuffer<T> buffer, T value) where T : unmanaged
        {
            var array = new T[buffer.Data.Length];
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = value;
            }
            return array.ToNamedBuffer(buffer.Name);
        }
    }
}
