using System.IO;
using System.IO.Compression;
using Vim.BFastLib.Core;

namespace Vim.BFastLib
{
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
                    throw new System.Exception("Cannot uncompress a non-compressed node.");
                }
                return _node;
            }
            if(_compress)
            {
                throw new System.Exception("Compressed node needs to be decompressed.");
            }
            return _node;
        }

        private IBFastNode Decompress()
        {
            var output = new MemoryStream();
            using (var input = new MemoryStream())
            {
                _node.Write(input);
                input.Seek(0, SeekOrigin.Begin);
                using (var compress = new DeflateStream(input, CompressionMode.Decompress, true))
                {
                    compress.CopyTo(output);
                    output.Seek(0, SeekOrigin.Begin);
                    return new BFastStreamNode(output, output.FullRange());
                }
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
