using System.IO;

namespace Vim.BFastNS.Core
{
    /// <summary>
    /// Provide methods to write a buffer collection to a stream.
    /// </summary>
    public class BFastWriter
    {
        private readonly string[] _bufferNames;
        private readonly IWritable[] _buffers;
        private readonly byte[] _packedNames;

        private readonly BFastSection _preamble;
        private readonly BFastSection _ranges;
        private readonly BFastSection _names;

        public long Start => _preamble.AbsoluteStart;

        public BFastWriter(string[] names, IWritable[] buffers, long offset = 0)
        {
            if(names.Length != buffers.Length)
            {
                throw new System.Exception("Names and buffer length must match");
            }

            _bufferNames = names;
            _buffers = buffers;
            _packedNames = BFastStrings.Pack(names);

            _preamble = new BFastSection(0, 32).Offset(offset);
            _ranges = _preamble.Next((buffers.Length + 1) * 16);
            _names = _ranges.Next(_packedNames.Length);
        }

        /// <summary>
        /// Writes to given stream, which may or may not be at Position 0.
        /// </summary>
        public unsafe void Write(Stream stream)
        {
            var offset = stream.Position;
            if (Start != stream.Position)
            {
                // Offset sections if stream not at 0
                Offset(stream.Position).Write(stream);
                return;
            }

            // Leave space for preamble
            _preamble.Clear(stream);

            // Leave space for ranges and write Names range.
            _ranges.Clear(stream);
            WriteRange(stream, _ranges.AbsoluteStart, 0, _names.LocalRange);

            // Write Names
            _names.Write(stream, _packedNames);

            // Write each buffer and go back to write its Range.
            var dataPointer = _names.End;
            for (var i = 0; i < _buffers.Length; i++)
            {
                var section = WriteBuffer(stream, dataPointer, _buffers[i]).Rebase(offset);
                WriteRange(stream, _ranges.AbsoluteStart, i + 1, section.LocalRange);
                dataPointer = section.End;
            }

            // Finally go back to write the preamble.
            var preamble = new BFastPreamble()
            {
                Magic = BFastConstants.Magic,
                NumArrays = _buffers.Length + 1,
                DataStart = _ranges.End - offset,
                DataEnd = dataPointer - offset,
            };
            _preamble.Write(stream, preamble);

            // Move pointer back to end as the caller would expect
            stream.Seek(dataPointer, SeekOrigin.Begin);
        }

        private BFastWriter Offset(long offset)
        {
            return new BFastWriter(_bufferNames, _buffers, offset);
        }

        private void WriteRange(Stream stream, long start, int index, BFastRange range)
        {
            stream.Seek(start + index * 16, SeekOrigin.Begin);
            stream.WriteValue(range);
        }

        private BFastSection WriteBuffer(Stream stream, long start, IWritable buffer)
        {
            stream.Seek(start, SeekOrigin.Begin);
            return BFastSection.Write(stream, buffer);

        }
    }
}

