using System;
using System.Diagnostics;
using System.IO;

namespace Vim.BFastLib.Core
{
    /// <summary>
    /// Represents a section of the bfast that will be written to at some point.
    /// </summary>
    public class BFastSection
    {
        public readonly long Origin;
        public readonly long LocalStart;
        public readonly long Length;
        public long AbsoluteStart => LocalStart + Origin;
        
        public long AbsoluteEnd => AbsoluteStart + Length;
        public long LocalEnd => LocalStart + Length;
        public long End => AbsoluteStart + Length;
        public BFastRange LocalRange => new BFastRange()
        {
            Begin = LocalStart,
            End = LocalEnd
        };

        public BFastSection(long start, long length, long origin = 0)
        {
            LocalStart = start;
            Length = length;
            Origin = origin;
        }

        /// <summary>
        /// Returns a new range offset by given amount.
        /// </summary>
        public BFastSection Offset(long offset)
        {
            return new BFastSection(AbsoluteStart, Length, offset);
        }

        /// <summary>
        /// Returns an equivalent section but with given origin.
        /// </summary>
        public BFastSection Rebase(long origin)
        {
            return new BFastSection(LocalStart - origin, Length, origin);
        }

        /// <summary>
        /// Returns a new range Starting where this one ends.
        /// </summary>
        /// <param name="length">Byte length of the section</param>
        public BFastSection Next(long length)
        {
            return new BFastSection(LocalEnd, length, Origin);
        }

        /// <summary>
        /// Writes 0 bytes over the whole section.
        /// </summary>
        public void Clear(Stream stream)
        {
            stream.Seek(AbsoluteStart, SeekOrigin.Begin);
            for (var i = 0; i < Length; i++)
            {
                stream.WriteByte(0);
            }
        }

        /// <summary>
        /// Writes given bytes in the section. Throws if bytes don't match section length.
        /// </summary>
        public void Write(Stream stream, byte[] bytes)
        {
            if (bytes.Length != Length)
                throw new Exception("Data length not matching section length");

            stream.Seek(AbsoluteStart, SeekOrigin.Begin);
            stream.Write(bytes);
        }

        /// <summary>
        /// Writes given value in the section. Throws if value don't match section length.
        /// </summary>
        unsafe public void Write<T>(Stream stream, T value) where T : unmanaged
        {
            Debug.Assert(sizeof(T) == Length);
            stream.Seek(AbsoluteStart, SeekOrigin.Begin);
            stream.WriteValue(value);
        }

        /// <summary>
        /// Writes given buffer and returns resulting section.
        /// </summary>
        public static BFastSection Write(Stream stream, IWritable buffer)
        {
            var start = stream.Position;
            buffer.Write(stream);
            return new BFastSection(start, stream.Position - start);
        }
    }
}

