using System.IO;
using System.IO.Compression;
using Vim.BFastLib.Core;

namespace Vim.BFastLib
{
    /// <summary>
    /// A wrapper around a IBFastNode that manages writing and reading using compression.
    /// </summary>
    public class CompressibleNode : IWritable
    {
        private readonly IBFastNode _node;
        private readonly bool _compress;

        public CompressibleNode(IBFastNode node, bool compress = false)
        {
            _node = node;
            _compress = compress;
        }

        public void Write(Stream stream)
        {
            if (_compress)
            {
                WriteCompress(stream);
            }
            else
            {
                _node.Write(stream);
            }
        }

        /// <summary>
        /// Returns the node after it is decompressed if needed.
        /// Will throw if decompress argument doesnt match compression state.
        /// </summary>
        public IBFastNode GetNode(bool decompress = false)
        {
            if (decompress)
            {
                if (_node is BFastStreamNode)
                {
                    return Decompress();
                }
                if (!_compress)
                {
                    throw new System.Exception("Cannot uncompress non-compressed data.");
                }
                return _node;
            }
            if(_compress)
            {
                throw new System.Exception("Compressed data needs to be decompressed.");
            }
            return _node;
        }

        private IBFastNode Decompress()
        {
            // This memory stream is not disposed. But it's ok.
            // It really is just an array under the hood.
            // https://stackoverflow.com/questions/4274590/memorystream-close-or-memorystream-dispose
            var output = new MemoryStream();

            using (var input = _node.ToMemoryStream())
            using (var compress = new DeflateStream(input, CompressionMode.Decompress, true))
            {
                compress.CopyTo(output);
                output.Seek(0, SeekOrigin.Begin);
                return new BFastStreamNode(output, output.FullRange());
            }
        }

        private void WriteCompress(Stream stream)
        {
            using (var input = _node.ToMemoryStream())
            using (var compress = new DeflateStream(stream, CompressionMode.Compress, true))
            {
                input.CopyTo(compress);
            }
        }
    }
}
