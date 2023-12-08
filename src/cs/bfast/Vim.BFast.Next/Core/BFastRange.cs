using System.IO;
using System.Runtime.InteropServices;

namespace Vim.BFastNS.Core
{
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

        public BFastRange OffsetBy(long offset)
            => new BFastRange()
            {
                Begin = Begin + offset,
                End = End + offset
            };
    }

    public static class BFastRangeExtensions
    {
        public static BFastRange FullRange(this Stream stream)
        => new BFastRange()
        {
            Begin = 0,
            End = stream.Length
        };
    }
}
