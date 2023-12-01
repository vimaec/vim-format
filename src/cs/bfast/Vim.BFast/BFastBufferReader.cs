using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Vim.BFast
{
    /// <summary>
    /// Represents a BFAST buffer whose stream can be read after calling Seek().
    /// </summary>
    public class BFastBufferReader
    {
        /// <summary>
        /// The seekable stream from which the buffer can be read.
        /// </summary>
        private readonly Stream _stream;

        /// <summary>
        /// The start position of the buffer in the stream.
        /// </summary>
        private readonly long _startPosition;

        /// <summary>
        /// The size in bytes of the buffer.
        /// </summary>
        public readonly long Size;

        /// <summary>
        /// The buffer name.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Deconstruct operator
        /// </summary>
        public void Deconstruct(out string name, out long size)
            => (name, size) = (Name, Size);

        /// <summary>
        /// Constructor.
        /// </summary>
        public BFastBufferReader(Stream stream, string name, long startPosition, long size)
        {
            _stream = stream;
            _startPosition = startPosition;
            Size = size;
            Name = name;
        }

        /// <summary>
        /// Seeks to the start of the BFAST buffer and returns the stream.
        /// </summary>
        public Stream Seek()
        {
            _stream.Seek(_startPosition, SeekOrigin.Begin);
            BFast.CheckAlignment(_stream);
            return _stream;
        }

        public NamedBuffer<T> GetBuffer<T>(bool inflate = false) where T : unmanaged
        {
            Seek();
            if (!inflate)
            {
                return _stream.ReadArray<T>((int)Size).ToNamedBuffer(Name);
            }
            using(var deflated = new DeflateStream(_stream, CompressionMode.Decompress)){
                return _stream.ReadArray<T>((int)Size).ToNamedBuffer(Name);
            }
        }

        public NamedBuffer<byte> GetBuffer(bool inflate = false)
        {
            return GetBuffer<byte>(inflate);
        }

    }

    public static class BFastBufferReaderExtensions
    {

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

            var padding = BFast.ComputePadding(r.Ranges);
            br.ReadBytes((int)padding);
            BFast.CheckAlignment(br.BaseStream);

            var nameBytes = br.ReadBytes((int)r.Ranges[0].Count);
            r.Names = nameBytes.UnpackStrings();

            padding = BFast.ComputePadding(r.Ranges[0].End);
            br.ReadBytes((int)padding);
            BFast.CheckAlignment(br.BaseStream);

            return r.Validate();
        }

        /// <summary>
        /// Returns a list of BFAST buffer readers in the stream.
        /// Assumes the stream's current position designates a BFAST header.
        /// </summary>
        public static IReadOnlyList<BFastBufferReader> GetBFastBufferReaders(
            this Stream stream,
            Func<BFastBufferReader, bool> filterFn = null)
        {
            var result = new List<BFastBufferReader>();

            using (var seekContext = new SeekContext(stream))
            {
                // Read the header
                var header = stream.ReadBFastHeader();
                BFast.CheckAlignment(stream);

                // Create a BFastBufferReader for each range.
                for (var i = 1; i < header.Ranges.Length; ++i)
                {
                    var range = header.Ranges[i];
                    var name = header.Names[i - 1];

                    var startSeekPosition = seekContext.OriginalSeekPosition + range.Begin;
                    var size = range.End - range.Begin;

                    var bfastBufferReader = new BFastBufferReader(seekContext.Stream, name, startSeekPosition, size);

                    if (filterFn?.Invoke(bfastBufferReader) ?? true)
                    {
                        result.Add(bfastBufferReader);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Returns a BFAST buffer reader corresponding to the given buffer name.
        /// Returns null if the given buffer name was not found or if the buffer name is null or empty.
        /// </summary>
        public static BFastBufferReader GetBFastBufferReader(this Stream stream, string bufferName)
            => string.IsNullOrEmpty(bufferName)
                ? null
                : stream.GetBFastBufferReaders(br => br.Name == bufferName).FirstOrDefault();


        public static NamedBuffer<byte> GetBFastBuffer(this Stream stream, string bufferName, bool inflate = false)
        {

            var buffer = stream.GetBFastBufferReader(bufferName).GetBuffer();
            return buffer;
            
            // if (!inflate) return buffer;
            // var bytes = buffer.GetTypedData();
            // using(var inflate = new DeflateStream(stream, CompressionMode.Decompress));
        }

        public static void SeekToBFastBuffer(this Stream stream, string bufferName)
            => stream.GetBFastBufferReader(bufferName).Seek();


        /// <summary>
        /// Reads a BFAST stream and returns a list of labeled results.
        /// </summary>
        public static List<(string Label, T Result)> ReadBFast<T>(
            this Stream stream,
            Func<Stream, string, long, T> onBuffer)
        {
            var result = new List<(string, T)>();

            foreach (var br in stream.GetBFastBufferReaders())
            {
                var name = br.Name;
                var s = br.Seek();
                result.Add((name, onBuffer(s, name, br.Size)));
            }

            return result;
        }

        /// <summary>
        /// Returns a named buffer corresponding to the given bufferName. Returns null if no buffer name is found.
        /// This call limits the buffers to 2GB. 
        /// </summary>
        public static NamedBuffer<T> ReadBFastBuffer<T>(this Stream stream, string bufferName) where T : unmanaged
        {
            var br = stream.GetBFastBufferReader(bufferName);
            return br?.GetBuffer<T>();
        }
    }
}
