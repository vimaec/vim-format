using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Vim.BFastNS.Core;

namespace Vim.BFastNS
{
    public class BFast : IBFastNode
    {
        private readonly Dictionary<string, IBFastNode> _children = new Dictionary<string, IBFastNode>();
        public IEnumerable<string> Entries => _children.Keys;
        private IEnumerable<(string name, IWritable buffer)> Writables => _children.Select(kvp => (kvp.Key, kvp.Value as IWritable));

        public BFast() { }
        public BFast(Stream stream)
        {
            var node = GetBFastNodes(stream);
            _children = node.ToDictionary(c => c.name, c => c.value as IBFastNode);
        }

        public void SetBFast(Func<int, string> getName, IEnumerable<BFast> others, bool deflate = false)
        {
            var i = 0;
            foreach (var b in others)
            {
                SetBFast(getName(i++), b, deflate);
            }
        }

        public void SetBFast(string name, BFast bfast, bool deflate = false)
        {
            if (deflate == false)
            {
                _children[name] = bfast;
            }
            else
            {
                var a = Deflate2(bfast);
                SetArray(name, a);
            }
        }

        private byte[] Deflate(BFast bfast)
        {
            using (var output = new MemoryStream())
            {
                using (var decompress = new DeflateStream(output, CompressionMode.Compress, true))
                {
                    bfast.Write(decompress);
                }
                return output.ToArray();
            }
        }

        private byte[] Deflate2(BFast bfast)
        {
            using (var input = new MemoryStream())
            using (var output = new MemoryStream())
            {
                bfast.Write(input);
                input.Seek(0, SeekOrigin.Begin);
                using (var decompress = new DeflateStream(output, CompressionMode.Compress, true))
                {
                    input.CopyTo(decompress);
                }
                // This need to happen after the deflate stream is disposed.
                return output.ToArray();
            }
        }

        public void SetEnumerable<T>(string name, Func<IEnumerable<T>> enumerable) where T : unmanaged
        {
            if (enumerable == null)
            {
                _children.Remove(name);
                return;
            }
            _children[name] = new BFastEnumerableNode<T>(enumerable);
        }

        public void SetArray<T>(string name, T[] array) where T : unmanaged
        {
            if (array == null)
            {
                _children.Remove(name);
                return;
            }
            _children[name] = BFastNode.FromArray(array);
        }

        public void SetArrays<T>(Func<int, string> getName, IEnumerable<T[]> arrays) where T : unmanaged
        {
            var index = 0;
            foreach (var array in arrays)
            {
                SetArray(getName(index++), array);
            }
        }

        public void SetNode(string name, BFastNode node)
        {
            _children[name] = node;
        }

        public BFast GetBFast(string name, bool inflate = false)
        {
            var node = GetNode(name);
            if (node == null) return null;
            if (inflate == false) return node.AsBFast();
            return InflateNode(node);
        }

        private BFast InflateNode(IBFastNode node)
        {
            var output = new MemoryStream();
            using (var input = new MemoryStream())
            {
                node.Write(input);
                input.Seek(0, SeekOrigin.Begin);
                using (var compress = new DeflateStream(input, CompressionMode.Decompress, true))
                {
                    compress.CopyTo(output);
                    output.Seek(0, SeekOrigin.Begin);
                    return new BFast(output);
                }
            }
        }

        public IEnumerable<T> GetEnumerable<T>(string name) where T : unmanaged
        {
            if (!_children.ContainsKey(name)) return null;
            return _children[name].AsEnumerable<T>();
        }

        public T[] GetArray<T>(string name) where T : unmanaged
        {
            if (!_children.ContainsKey(name)) return null;
            return _children[name].AsArray<T>();
        }

        public IBFastNode GetNode(string name)
            => _children.TryGetValue(name, out var value) ? value : null;

        public void Remove(string name)
            => _children.Remove(name);

        public void Write(Stream stream)
        {
            var list = Writables.ToList();
            var strings = list.Select(n => n.name).ToArray();
            var buffers = list.Select(n => n.buffer).ToArray();
            var writer = new BFastWriter(strings, buffers);
            writer.Write(stream);
        }

        public void Write(string path)
        {
            using (var file = new FileStream(path, FileMode.Create))
            {
                Write(file);
            }
        }

        public byte[] AsBytes() => (this as IBFastNode).AsArray<byte>();

        BFast IBFastNode.AsBFast()
        {
            return this;
        }

        T[] IBFastNode.AsArray<T>()
        {
            using (var stream = new MemoryStream())
            {
                Write(stream);
                var end = stream.Position;
                stream.Seek(0, SeekOrigin.Begin);
                return stream.ReadArrayBytes<T>((int)end);
            }
        }

        public IEnumerable<T> AsEnumerable<T>() where T : unmanaged
        {
            using (var stream = new MemoryStream())
            {
                Write(stream);
                var end = stream.Position;
                stream.Seek(0, SeekOrigin.Begin);
                return stream.ReadArrayBytes<T>((int)end);
            }
        }

        private static IEnumerable<(string name, BFastNode value)> GetBFastNodes(Stream stream)
        {
            var offset = stream.Position;
            var raw = BFastHeader.FromStream(stream);
            foreach(var kvp in raw.Ranges)
            {
                var node = new BFastNode(
                    stream,
                    kvp.Value.OffsetBy(offset)
                );

                yield return (kvp.Key, node);
            }
        }
    }
}

