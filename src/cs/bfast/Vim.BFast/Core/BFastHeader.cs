using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Vim.BFastLib.Core
{
    public class BFastHeader
    {
        public readonly BFastPreamble Preamble;
        public IReadOnlyDictionary<string, BFastRange> Ranges => _ranges;
        private readonly Dictionary<string, BFastRange> _ranges;

        public BFastHeader(BFastPreamble preamble, Dictionary<string, BFastRange> ranges)
        {
            Preamble = preamble;
            _ranges = ranges;
        }

        /// <summary>
        /// Reads the preamble, the ranges, and the names of the rest of the buffers. 
        /// </summary>
        public static BFastHeader FromStream(Stream stream)
        {
            if (stream.Length - stream.Position < sizeof(long) * 4)
                throw new Exception("Stream too short");

            var offset = stream.Position;

            var preamble = stream.ReadValue<BFastPreamble>();
            var ranges = stream.ReadArray<BFastRange>((int)preamble.NumArrays);

            // In a lot of existing vim there is padding before the first buffer.
            stream.Seek(offset + ranges[0].Begin, SeekOrigin.Begin);
            var nameBytes = stream.ReadArray<byte>((int)ranges[0].Count);
            var names = BFastStrings.Unpack(nameBytes);
            
            if (names.Distinct().Count() < names.Length)
            {
                throw new Exception($"Buffer names should be unique. It contains duplicates. " + string.Join(",", names));
            }

            var map = names
                .Zip(ranges.Skip(1), (n, r) => (n, r))
                .ToDictionary(p => p.n, p => p.r);

            return new BFastHeader(preamble, map).Validate();
        }

        public BFastHeader Validate()
        {
            Preamble.Validate();
            foreach (var range in _ranges.Values)
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
