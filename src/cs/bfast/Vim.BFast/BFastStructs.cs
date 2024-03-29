﻿/*
    BFAST - Binary Format for Array Streaming and Transmission
    Copyright 2019, VIMaec LLC
    Copyright 2018, Ara 3D, Inc.
    Usage licensed under terms of MIT License
	https://github.com/vimaec/bfast

    The BFAST format is a simple, generic, and efficient representation of 
    buffers (arrays of binary data) with optional names.  
    
    It can be used in place of a zip when compression is not required, or when a simple protocol
    is required for transmitting data to/from disk, between processes, or over a network. 
*/

using System.Linq;
using System.Runtime.InteropServices;

namespace Vim.BFast
{
    /// <summary>
    /// This contains the BFAST data loaded or written from disk. 
    /// </summary>
    public class BFastHeader
    {
        public BFastPreamble Preamble = new BFastPreamble();
        public BFastRange[] Ranges;
        public string[] Names;

        public override bool Equals(object o)
            => o is BFastHeader other && Equals(other);

        public bool Equals(BFastHeader other)
            => Preamble.Equals(other.Preamble) &&
            Ranges.Length == other.Ranges.Length &&
            Ranges.Zip(other.Ranges, (x, y) => x.Equals(y)).All(x => x) &&
            Names.Zip(other.Names, (x, y) => x.Equals(y)).All(x => x);
    }

    /// <summary>
    /// Constants.
    /// </summary>
    public static class Constants
    {
        public const long Magic = 0xBFA5;

        // https://en.wikipedia.org/wiki/Endianness
        public const long SameEndian = Magic;
        public const long SwappedEndian = 0xA5BFL << 48;

        /// <summary>
        /// Data arrays are aligned to 64 bytes, so that they can be cast directly to AVX-512 registers.
        /// This is useful for efficiently working with floating point data. 
        /// </summary>
        public const long ALIGNMENT = 64;
    }

    /// <summary>
    /// This tells us where a particular array begins and ends in relation to the beginning of a file.
    /// * Begin must be less than or equal to End.
    /// * Begin must be greater than or equal to DataStart
    /// * End must be less than or equal to DataEnd
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 8, Size = 16)]
    public struct BFastRange
    {
        [FieldOffset(0)] public long Begin;
        [FieldOffset(8)] public long End;

        public long Count => End - Begin;
        public static long Size = 16;

        public override bool Equals(object x)
            => x is BFastRange other && Equals(other);

        public bool Equals(BFastRange other)
            => Begin == other.Begin && End == other.End;
    }

    /// <summary>
    /// The header contains a magic number, the begin and end indices of data, and the number of arrays.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Pack = 8, Size = 32)]
    public struct BFastPreamble
    {
        [FieldOffset(0)] public long Magic;         // Either Constants.SameEndian or Constants.SwappedEndian depending on endianess of writer compared to reader. 
        [FieldOffset(8)] public long DataStart;     // <= file size and >= ArrayRangesEnd and >= FileHeader.ByteCount
        [FieldOffset(16)] public long DataEnd;      // >= DataStart and <= file size
        [FieldOffset(24)] public long NumArrays;    // number of arrays 

        /// <summary>
        /// This is where the array ranges are finished. 
        /// Must be less than or equal to DataStart.
        /// Must be greater than or equal to FileHeader.ByteCount
        /// </summary>
        public long RangesEnd => Size + NumArrays * 16;

        /// <summary>
        /// The size of the FileHeader structure 
        /// </summary>
        public static long Size = 32;

        /// <summary>
        /// Returns true if the producer of the BFast file has the same endianness as the current library
        /// </summary>
        public bool SameEndian => Magic == Constants.SameEndian;

        public override bool Equals(object x)
            => x is BFastPreamble other && Equals(other);

        public bool Equals(BFastPreamble other)
            => Magic == other.Magic && DataStart == other.DataStart && DataEnd == other.DataEnd && NumArrays == other.NumArrays;
    };
}
