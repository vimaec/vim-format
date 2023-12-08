using System;
using System.IO;
using System.Linq;

namespace Vim.BFastNS.Core
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

        /// <summary>
        /// Checks that the header values are sensible, and throws an exception otherwise.
        /// </summary>
        public BFastHeader Validate()
        {
            var preamble = Preamble.Validate();
            var ranges = Ranges;
            var names = Names;

            if (preamble.RangesEnd > preamble.DataStart)
                throw new Exception($"Computed arrays ranges end must be less than the start of data {preamble.DataStart}");

            if (ranges == null)
                throw new Exception("Ranges must not be null");

            var min = preamble.DataStart;
            var max = preamble.DataEnd;

            for (var i = 0; i < ranges.Length; ++i)
            {
                var begin = ranges[i].Begin;
                if (!BFastAlignment.IsAligned(begin))
                    throw new Exception($"The beginning of the range is not well aligned {begin}");
                var end = ranges[i].End;
                if (begin < min || begin > max)
                    throw new Exception($"Array offset begin {begin} is not in valid span of {min} to {max}");
                if (i > 0)
                {
                    if (begin < ranges[i - 1].End)
                        throw new Exception($"Array offset begin {begin} is overlapping with previous array {ranges[i - 1].End}");
                }

                if (end < begin || end > max)
                    throw new Exception($"Array offset end {end} is not in valid span of {begin} to {max}");
            }

            if (names.Length < ranges.Length - 1)
                throw new Exception($"Number of buffer names {names.Length} is not one less than the number of ranges {ranges.Length}");

            return this;
        }
    }
}
