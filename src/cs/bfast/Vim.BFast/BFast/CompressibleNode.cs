using System.IO;
using System.IO.Compression;
using Vim.BFastLib.Core;

namespace Vim.BFastLib
{
    public class CompressibleNode : IWritable
    {
        public readonly IBFastNode Node;
        private readonly bool _compress;

        public CompressibleNode(IBFastNode node, bool compress = false)
        {
            Node = node;
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
                Node.Write(stream);
            }
        }

        public IBFastNode Decompress()
        {
            if (!(Node is BFastStreamNode)) return Node;

            var output = new MemoryStream();
            using (var input = new MemoryStream())
            {
                Node.Write(input);
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
            using (var input = Node.ToMemoryStream())
            using (var compress = new DeflateStream(stream, CompressionMode.Compress, true))
            {
                input.CopyTo(compress);
            }
        }
    }
}
