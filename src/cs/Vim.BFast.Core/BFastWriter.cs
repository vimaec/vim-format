using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Vim.BFast.Core
{
    public static class BFastWriter
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
        public static void Write(Stream stream, string[] bufferNames, long[] bufferSizes, BFastWriterFn onBuffer)
        {
            if (bufferSizes.Any(sz => sz < 0))
                throw new Exception("All buffer sizes must be zero or greater than zero");

            if (bufferNames.Length != bufferSizes.Length)
                throw new Exception($"The number of buffer names {bufferNames.Length} is not equal to the number of buffer sizes {bufferSizes}");

            var header = CreateHeader(bufferSizes, bufferNames);
            Write(stream, header, bufferNames, bufferSizes, onBuffer);
        }

        /// <summary>
        /// Enables a user to write a BFAST from an array of names, sizes, and a custom writing function.
        /// This is useful when the header is already computed.
        /// </summary>
        public static void Write(Stream stream, BFastHeader header, string[] bufferNames, long[] bufferSizes, BFastWriterFn onBuffer)
        {
            WriteHeader(stream, header);
            BFastAlignment.Check(stream);
            WriteBody(stream, bufferNames, bufferSizes, onBuffer);
        }

        /// <summary>
        /// Must be called after "WriteBFastHeader"
        /// Enables a user to write the contents of a BFAST from an array of names, sizes, and a custom writing function.
        /// The function will receive a BinaryWriter, the index of the buffer, and is expected to return the number of bytes written.
        /// Simplifies the process of creating custom BinaryWriters, or writing extremely large arrays if necessary.
        /// </summary>
        public static void WriteBody(Stream stream, string[] bufferNames, long[] bufferSizes, BFastWriterFn onBuffer)
        {
            BFastAlignment.Check(stream);

            if (bufferSizes.Any(sz => sz < 0))
                throw new Exception("All buffer sizes must be zero or greater than zero");

            if (bufferNames.Length != bufferSizes.Length)
                throw new Exception($"The number of buffer names {bufferNames.Length} is not equal to the number of buffer sizes {bufferSizes}");

            // Then passes the binary writer for each buffer: checking that the correct amount of data was written.
            for (var i = 0; i < bufferNames.Length; ++i)
            {
                BFastAlignment.Check(stream);
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
                var padding = BFastAlignment.ComputePadding(nBytes);
                for (var j = 0; j < padding; ++j)
                    stream.WriteByte(0);
                BFastAlignment.Check(stream);
            }
        }

        /// <summary>
        /// Creates a BFAST structure, without any actual data buffers, from a list of sizes of buffers (not counting the name buffer). 
        /// Used as an intermediate step to create a BFAST. 
        /// </summary>
        public static BFastHeader CreateHeader(long[] bufferSizes, string[] bufferNames)
        {
            if (bufferNames.Length != bufferSizes.Length)
                throw new Exception($"The number of buffer sizes {bufferSizes.Length} is not equal to the number of buffer names {bufferNames.Length}");

            var header = new BFastHeader
            {
                Names = bufferNames
            };
            header.Preamble.Magic = BFastConstants.Magic;
            header.Preamble.NumArrays = bufferSizes.Length + 1;

            // Allocate the data for the ranges
            header.Ranges = new BFastRange[header.Preamble.NumArrays];
            header.Preamble.DataStart = BFastAlignment.ComputeNext(header.Preamble.RangesEnd);

            var nameBufferLength = BFastStrings.Pack(bufferNames).LongLength;
            var sizes = (new[] { nameBufferLength }).Concat(bufferSizes).ToArray();

            // Compute the offsets for the data buffers
            var curIndex = header.Preamble.DataStart;
            var i = 0;
            foreach (var size in sizes)
            {
                curIndex = BFastAlignment.ComputeNext(curIndex);
                Debug.Assert(BFastAlignment.IsAligned(curIndex));

                header.Ranges[i].Begin = curIndex;
                curIndex += size;

                header.Ranges[i].End = curIndex;
                i++;
            }

            // Finish with the header
            // Each buffer we contain is padded to ensure the next one
            // starts on alignment, so we pad our DataEnd to reflect this reality
            header.Preamble.DataEnd = BFastAlignment.ComputeNext(curIndex);

            // Check that everything adds up 
            return header.Validate();
        }


        /// <summary>
        /// Writes the BFAST header and name buffer to stream using the provided BinaryWriter. The BinaryWriter will be properly aligned by padding zeros 
        /// </summary>
        public static BinaryWriter WriteHeader(Stream stream, BFastHeader header)
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
            WriteZeroBytes(bw, BFastAlignment.ComputePadding(header.Ranges));

            BFastAlignment.Check(stream);
            var nameBuffer = BFastStrings.Pack(header.Names);
            bw.Write(nameBuffer);
            WriteZeroBytes(bw, BFastAlignment.ComputePadding(nameBuffer.LongLength));

            BFastAlignment.Check(stream);
            return bw;
        }

        /// <summary>
        /// Writes n zero bytes.
        /// </summary>
        public static void WriteZeroBytes(BinaryWriter bw, long n)
        {
            for (var i = 0L; i < n; ++i)
                bw.Write((byte)0);
        }

    }
}
