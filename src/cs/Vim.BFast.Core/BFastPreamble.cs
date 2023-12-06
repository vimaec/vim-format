using System;
using System.Runtime.InteropServices;

namespace Vim.BFast.Core
{
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
        public bool SameEndian => Magic == BFastConstants.SameEndian;

        public override bool Equals(object x)
            => x is BFastPreamble other && Equals(other);

        public bool Equals(BFastPreamble other)
            => Magic == other.Magic && DataStart == other.DataStart && DataEnd == other.DataEnd && NumArrays == other.NumArrays;


        /// <summary>
        /// Checks that the header values are sensible, and throws an exception otherwise.
        /// </summary>
        public BFastPreamble Validate()
        {
            if (Magic != BFastConstants.SameEndian && Magic != BFastConstants.SwappedEndian)
                throw new Exception($"Invalid magic number {Magic}");

            if (DataStart < BFastPreamble.Size)
                throw new Exception($"Data start {DataStart} cannot be before the file header size {BFastPreamble.Size}");

            if (DataStart > DataEnd)
                throw new Exception($"Data start {DataStart} cannot be after the data end {DataEnd}");

            if (!BFastAlignment.IsAligned(DataEnd))
                throw new Exception($"Data end {DataEnd} should be aligned");

            if (NumArrays < 0)
                throw new Exception($"Number of arrays {NumArrays} is not a positive number");

            if (NumArrays > DataEnd)
                throw new Exception($"Number of arrays {NumArrays} can't be more than the total size");

            if (RangesEnd > DataStart)
                throw new Exception($"End of range {RangesEnd} can't be after data-start {DataStart}");

            return this;
        }

    };
}
