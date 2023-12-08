using System;
using System.IO;

namespace Vim.BFastNS.Core
{
    public static class BFastReader
    {
        /// <summary>
        /// Reads the preamble, the ranges, and the names of the rest of the buffers. 
        /// </summary>
        public static BFastHeader ReadHeader(Stream stream)
        {
            var r = new BFastHeader();
            var br = new BinaryReader(stream);

            if (stream.Length - stream.Position < sizeof(long) * 4)
                throw new Exception("Stream too short");

            r.Preamble = new BFastPreamble
            {
                Magic = br.ReadInt64(),
                DataStart = br.ReadInt64(),
                DataEnd = br.ReadInt64(),
                NumArrays = br.ReadInt64(),
            }.Validate();

            r.Ranges = stream.ReadArray<BFastRange>((int)r.Preamble.NumArrays);

            var padding = BFastAlignment.ComputePadding(r.Ranges);
            br.ReadBytes((int)padding);
            BFastAlignment.Check(br.BaseStream);

            var nameBytes = br.ReadBytes((int)r.Ranges[0].Count);
            r.Names = BFastStrings.Unpack(nameBytes);

            padding = BFastAlignment.ComputePadding(r.Ranges[0].End);
            br.ReadBytes((int)padding);
            BFastAlignment.Check(br.BaseStream);

            return r.Validate();
        }
    }
}
