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
        private IEnumerable<(string, IWritable)> Writables => _children.Select(kvp => (kvp.Key, kvp.Value as IWritable));

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
                var a = Deflate(bfast);
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

        public long GetSize() => GetBFastSize(Writables);

        public void Write(Stream stream)
        {
            WriteBFast(stream, Writables);
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
            var header = BFastReader.ReadHeader(stream);
            for (var i = 1; i < header.Preamble.NumArrays; i++)
            {
                var node = new BFastNode(
                    stream,
                    header.Ranges[i].OffsetBy(offset)
                );

                yield return (header.Names[i - 1], node);
            }
        }

        private static void WriteBFast(Stream stream, IEnumerable<(string name, IWritable value)> writables)
        {
            var values = writables.Select(w => w.value).ToArray();
            var sizes = values.Select(v => v.GetSize()).ToArray();
            var names = writables.Select(w => w.name).ToArray();

            long onBuffer(Stream writingStream, int bufferIdx, string bufferName, long bytesToWrite)
            {
                values[bufferIdx].Write(writingStream);
                return bytesToWrite;
            }

            BFastWriter.Write(stream, names, sizes, onBuffer);
        }

        private static long GetBFastSize(IEnumerable<(string name, IWritable value)> writables)
        {
            var values = writables.Select(w => w.value).ToArray();
            var sizes = values.Select(v => v.GetSize()).ToArray();
            var names = writables.Select(w => w.name).ToArray();

            var header = BFastWriter.CreateHeader(sizes, names);
            return header.Preamble.DataEnd;
        }
    }

    public static class BFastNextExtensions
    {
        public static T ReadBFast<T>(this string path, Func<BFast, T> process)
        {
            using (var file = new FileStream(path, FileMode.Open))
            {
                var bfast = new BFast(file);
                return process(bfast);
            }
        }

        public static IEnumerable<INamedBuffer> ToNamedBuffers(this BFast bfast)
        {
            return bfast.Entries.Select(name => bfast.GetArray<byte>(name).ToNamedBuffer(name));
        }
    }
}

