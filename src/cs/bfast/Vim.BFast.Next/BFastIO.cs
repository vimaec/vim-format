using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Vim.BFastNextNS
{
    public static class BFastIO
    {
        /// <summary>
        /// Callback function allows clients to control writing the data to the output stream
        /// </summary>
        public delegate long BFastWriterFn(Stream writingStream, int bufferIdx, string bufferName, long bytesToWrite);

        /// <summary>
        /// Enables a user to write a BFAST from an array of names, sizes, and a custom writing function.
        /// The function will receive a BinaryWriter, the index of the buffer, and is expected to return the number of bytes written.
        /// Simplifies the process of creating custom BinaryWriters, or writing extremely large arrays if necessary.
        /// </summary>
        public static void WriteBFast(this Stream stream, string[] bufferNames, long[] bufferSizes, BFastWriterFn onBuffer)
        {
            if (bufferSizes.Any(sz => sz < 0))
                throw new Exception("All buffer sizes must be zero or greater than zero");

            if (bufferNames.Length != bufferSizes.Length)
                throw new Exception($"The number of buffer names {bufferNames.Length} is not equal to the number of buffer sizes {bufferSizes}");

            var header = CreateBFastHeader(bufferSizes, bufferNames);
            stream.WriteBFast(header, bufferNames, bufferSizes, onBuffer);
        }

        public static long NumBytes(this IBuffer buffer)
            => (long)buffer.NumElements() * buffer.ElementSize;

        /// <summary>
        /// Enables a user to write a BFAST from an array of names, sizes, and a custom writing function.
        /// This is useful when the header is already computed.
        /// </summary>
        public static void WriteBFast(this Stream stream, BFastHeader header, string[] bufferNames, long[] bufferSizes, BFastWriterFn onBuffer)
        {
            stream.WriteBFastHeader(header);
            CheckAlignment(stream);
            stream.WriteBFastBody(header, bufferNames, bufferSizes, onBuffer);
        }

        /// <summary>
        /// Must be called after "WriteBFastHeader"
        /// Enables a user to write the contents of a BFAST from an array of names, sizes, and a custom writing function.
        /// The function will receive a BinaryWriter, the index of the buffer, and is expected to return the number of bytes written.
        /// Simplifies the process of creating custom BinaryWriters, or writing extremely large arrays if necessary.
        /// </summary>
        public static void WriteBFastBody(this Stream stream, BFastHeader header, string[] bufferNames, long[] bufferSizes, BFastWriterFn onBuffer)
        {
            CheckAlignment(stream);

            if (bufferSizes.Any(sz => sz < 0))
                throw new Exception("All buffer sizes must be zero or greater than zero");

            if (bufferNames.Length != bufferSizes.Length)
                throw new Exception($"The number of buffer names {bufferNames.Length} is not equal to the number of buffer sizes {bufferSizes}");

            // Then passes the binary writer for each buffer: checking that the correct amount of data was written.
            for (var i = 0; i < bufferNames.Length; ++i)
            {
                CheckAlignment(stream);
                var nBytes = bufferSizes[i];
                var pos = stream.CanSeek ? stream.Position : 0;
                var nWrittenBytes = onBuffer(stream, i, bufferNames[i], nBytes);
                if (stream.CanSeek)
                {
                    if (stream.Position - pos != nWrittenBytes)
                        throw new NotImplementedException($"Buffer:{bufferNames[i]}. Stream movement {stream.Position - pos} does not reflect number of bytes claimed to be written {nWrittenBytes}");
                }

                if (nBytes != nWrittenBytes)
                    throw new Exception($"Number of bytes written {nWrittenBytes} not equal to expected bytes{nBytes}");
                var padding = ComputePadding(nBytes);
                for (var j = 0; j < padding; ++j)
                    stream.WriteByte(0);
                CheckAlignment(stream);
            }
        }


        /// <summary>
        /// Writes the BFAST header and name buffer to stream using the provided BinaryWriter. The BinaryWriter will be properly aligned by padding zeros 
        /// </summary>
        public static BinaryWriter WriteBFastHeader(this Stream stream, BFastHeader header)
        {
            if (header.Ranges.Length != header.Names.Length + 1)
                throw new Exception($"The number of ranges {header.Ranges.Length} must be equal to one more than the number of names {header.Names.Length}");
            var bw = new BinaryWriter(stream);
            bw.Write(header.Preamble.Magic);
            bw.Write(header.Preamble.DataStart);
            bw.Write(header.Preamble.DataEnd);
            bw.Write(header.Preamble.NumArrays);
            foreach (var r in header.Ranges)
            {
                bw.Write(r.Begin);
                bw.Write(r.End);
            }
            WriteZeroBytes(bw, ComputePadding(header.Ranges));

            CheckAlignment(stream);
            var nameBuffer = PackStrings(header.Names);
            bw.Write(nameBuffer);
            WriteZeroBytes(bw, ComputePadding(nameBuffer.LongLength));

            CheckAlignment(stream);
            return bw;
        }


        /// <summary>
        /// Writes n zero bytes.
        /// </summary>
        public static void WriteZeroBytes(this BinaryWriter bw, long n)
        {
            for (var i = 0L; i < n; ++i)
                bw.Write((byte)0);
        }


        /// <summary>
        /// Creates a BFAST structure, without any actual data buffers, from a list of sizes of buffers (not counting the name buffer). 
        /// Used as an intermediate step to create a BFAST. 
        /// </summary>
        public static BFastHeader CreateBFastHeader(this long[] bufferSizes, string[] bufferNames)
        {
            if (bufferNames.Length != bufferSizes.Length)
                throw new Exception($"The number of buffer sizes {bufferSizes.Length} is not equal to the number of buffer names {bufferNames.Length}");

            var header = new BFastHeader
            {
                Names = bufferNames
            };
            header.Preamble.Magic = Constants.Magic;
            header.Preamble.NumArrays = bufferSizes.Length + 1;

            // Allocate the data for the ranges
            header.Ranges = new BFastRange[header.Preamble.NumArrays];
            header.Preamble.DataStart = ComputeNextAlignment(header.Preamble.RangesEnd);

            var nameBufferLength = PackStrings(bufferNames).LongLength;
            var sizes = (new[] { nameBufferLength }).Concat(bufferSizes).ToArray();

            // Compute the offsets for the data buffers
            var curIndex = header.Preamble.DataStart;
            var i = 0;
            foreach (var size in sizes)
            {
                curIndex = ComputeNextAlignment(curIndex);
                Debug.Assert(IsAligned(curIndex));

                header.Ranges[i].Begin = curIndex;
                curIndex += size;

                header.Ranges[i].End = curIndex;
                i++;
            }

            // Finish with the header
            // Each buffer we contain is padded to ensure the next one
            // starts on alignment, so we pad our DataEnd to reflect this reality
            header.Preamble.DataEnd = ComputeNextAlignment(curIndex);

            // Check that everything adds up 
            return header.Validate();
        }

        /// <summary>
        /// Reads the preamble, the ranges, and the names of the rest of the buffers. 
        /// </summary>
        public static BFastHeader ReadBFastHeader(this Stream stream)
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

            var padding = ComputePadding(r.Ranges);
            br.ReadBytes((int)padding);
            CheckAlignment(br.BaseStream);

            var nameBytes = br.ReadBytes((int)r.Ranges[0].Count);
            r.Names = nameBytes.UnpackStrings();

            padding = ComputePadding(r.Ranges[0].End);
            br.ReadBytes((int)padding);
            CheckAlignment(br.BaseStream);

            return r.Validate();
        }

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
            => ComputeNextAlignment(n) - n;

        /// <summary>
        /// Given a position in the stream, tells us where the the next aligned position will be, if it the current position is not aligned.
        /// </summary>
        public static long ComputeNextAlignment(long n)
            => IsAligned(n) ? n : n + Constants.ALIGNMENT - (n % Constants.ALIGNMENT);


        /// <summary>
        /// Checks that the stream (if seekable) is well aligned
        /// </summary>
        public static void CheckAlignment(Stream stream)
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
            => n % Constants.ALIGNMENT == 0;


        /// <summary>
        /// Checks that the header values are sensible, and throws an exception otherwise.
        /// </summary>
        public static BFastHeader Validate(this BFastHeader header)
        {
            var preamble = header.Preamble.Validate();
            var ranges = header.Ranges;
            var names = header.Names;

            if (preamble.RangesEnd > preamble.DataStart)
                throw new Exception($"Computed arrays ranges end must be less than the start of data {preamble.DataStart}");

            if (ranges == null)
                throw new Exception("Ranges must not be null");

            var min = preamble.DataStart;
            var max = preamble.DataEnd;

            for (var i = 0; i < ranges.Length; ++i)
            {
                var begin = ranges[i].Begin;
                if (!IsAligned(begin))
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

            return header;
        }

        /// <summary>
        /// Checks that the header values are sensible, and throws an exception otherwise.
        /// </summary>
        public static BFastPreamble Validate(this BFastPreamble preamble)
        {
            if (preamble.Magic != Constants.SameEndian && preamble.Magic != Constants.SwappedEndian)
                throw new Exception($"Invalid magic number {preamble.Magic}");

            if (preamble.DataStart < BFastPreamble.Size)
                throw new Exception($"Data start {preamble.DataStart} cannot be before the file header size {BFastPreamble.Size}");

            if (preamble.DataStart > preamble.DataEnd)
                throw new Exception($"Data start {preamble.DataStart} cannot be after the data end {preamble.DataEnd}");

            if (!IsAligned(preamble.DataEnd))
                throw new Exception($"Data end {preamble.DataEnd} should be aligned");

            if (preamble.NumArrays < 0)
                throw new Exception($"Number of arrays {preamble.NumArrays} is not a positive number");

            if (preamble.NumArrays > preamble.DataEnd)
                throw new Exception($"Number of arrays {preamble.NumArrays} can't be more than the total size");

            if (preamble.RangesEnd > preamble.DataStart)
                throw new Exception($"End of range {preamble.RangesEnd} can't be after data-start {preamble.DataStart}");

            return preamble;
        }

        /// <summary>
        /// Converts a byte[] array encoding a collection of strings separate by NULL into an array of string   
        /// </summary>
        public static string[] UnpackStrings(this byte[] bytes)
        {
            var r = new List<string>();
            if (bytes.Length == 0)
                return r.ToArray();
            var prev = 0;
            for (var i = 0; i < bytes.Length; ++i)
            {
                if (bytes[i] == 0)
                {
                    r.Add(Encoding.UTF8.GetString(bytes, prev, i - prev));
                    prev = i + 1;
                }
            }
            if (prev < bytes.Length)
                r.Add(Encoding.UTF8.GetString(bytes, prev, bytes.Length - prev));
            return r.ToArray();
        }

        /// <summary>
        /// Converts a collection of strings, into a null-separated byte[] array 
        /// </summary>
        public static byte[] PackStrings(IEnumerable<string> strings)
        {
            var r = new List<byte>();
            foreach (var name in strings)
            {
                var bytes = Encoding.UTF8.GetBytes(name);
                r.AddRange(bytes);
                r.Add(0);
            }
            return r.ToArray();
        }

        public static BFastRange FullRange(this Stream stream)
            => new BFastRange()
            {
                Begin = 0,
                End = stream.Length
            };

        public static BFastRange OffsetBy(this BFastRange range, long offset)
            => new BFastRange()
            {
                Begin = range.Begin + offset,
                End = range.End + offset
            };
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
