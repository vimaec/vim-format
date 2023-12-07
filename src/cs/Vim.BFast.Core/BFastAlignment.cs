using System;
using System.IO;

namespace Vim.BFast.Core
{
    internal class BFastAlignment
    {
        /// <summary>
        /// Computes the padding requires after the array of BFastRanges are written out. 
        /// </summary>
        /// <param name="ranges"></param>
        /// <returns></returns>
        public static long ComputePadding(BFastRange[] ranges)
            => ComputePadding(BFastPreamble.Size + ranges.Length * BFastRange.Size);

        /// <summary>
        /// Given a position in the stream, computes how much padding is required to bring the value to an aligned point. 
        /// </summary>
        public static long ComputePadding(long n)
            => ComputeNext(n) - n;

        /// <summary>
        /// Given a position in the stream, tells us where the the next aligned position will be, if it the current position is not aligned.
        /// </summary>
        public static long ComputeNext(long n)
            => IsAligned(n) ? n : n + BFastConstants.ALIGNMENT - (n % BFastConstants.ALIGNMENT);

        /// <summary>
        /// Checks that the stream (if seekable) is well aligned
        /// </summary>
        public static void Check(Stream stream)
        {
            if (!stream.CanSeek)
                return;
            // TODO: Check with CD: Should we bail out here?  This means that any
            // alignment checks for a currently-writing stream are effectively ignored.
            if (stream.Position == stream.Length)
                return;
            if (!IsAligned(stream.Position))
                throw new Exception($"Stream position {stream.Position} is not well aligned");
        }

        /// <summary>
        /// Given a position in the stream, tells us whether the position is aligned.
        /// </summary>
        public static bool IsAligned(long n)
            => n % BFastConstants.ALIGNMENT == 0;
    }
}
