using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Vim.BFastNS.Core
{
    public class BFastHeader
    {
        public readonly BFastPreamble Preamble = new BFastPreamble();
        public readonly Dictionary<string, BFastRange> Ranges;

        public BFastHeader(BFastPreamble preamble, Dictionary<string, BFastRange> ranges)
        {
            Preamble = preamble;
            Ranges = ranges;
        }

        /// <summary>
        /// Reads the preamble, the ranges, and the names of the rest of the buffers. 
        /// </summary>
        public static BFastHeader FromStream(Stream stream)
        {
            if (stream.Length - stream.Position < sizeof(long) * 4)
                throw new Exception("Stream too short");

            var preamble = stream.Read<BFastPreamble>();

            var ranges = stream.ReadArray<BFastRange>((int)preamble.NumArrays);

            var nameBytes = stream.ReadArray<byte>((int)ranges[0].Count);
            var names = BFastStrings.Unpack(nameBytes);
            var map = names
                .Zip(ranges.Skip(1), (n, r) => (n, r))
                .ToDictionary(p => p.n, p => p.r);

            return new BFastHeader(preamble, map).Validate();
        }

        public BFastHeader Validate()
        {
            Preamble.Validate();
            foreach (var range in Ranges.Values)
            {
                if (range.Begin < Preamble.DataStart)
                {
                    throw new Exception("range.Begin must be larger than Data Start");
                }
                if (range.End > Preamble.DataEnd)
                {
                    throw new Exception("range.End must be smaller than Data End");
                }
            }
            return this;
        }
    }
}
