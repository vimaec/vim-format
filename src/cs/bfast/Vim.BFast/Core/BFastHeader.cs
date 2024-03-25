using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            // Some old vim have duplicated buffers
            // It is wrong but such is life.
            MakeNamesUnique(names);

            var map = names
                .Zip(ranges.Skip(1), (n, r) => (n, r))
                .ToDictionary(p => p.n, p => p.r);

            return new BFastHeader(preamble, map).Validate();
        }

        private static void MakeNamesUnique(string[] names)
        {
            var nameSet = new Dictionary<string, int>();
            for (var i = 0; i < names.Length; i++)
            {
                if (nameSet.ContainsKey(names[i]))
                {
                    var count = nameSet[names[i]];
                    names[i] = names[i] + "_" + count;
                    Debug.WriteLine($"Duplicated Name {names[i]} in BFAST. Making name unique. This can result in unexpected behaviour.");
                }
                if (!nameSet.ContainsKey(names[i]))
                {
                    nameSet.Add(names[i], i);
                }
            }
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
