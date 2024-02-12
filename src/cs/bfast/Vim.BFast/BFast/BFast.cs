using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.BFastNS;
using Vim.BFastNS.Core;

namespace Vim.BFastNS
{
    public class BFast : IBFastNode
    {
        private readonly Dictionary<string, CompressibleNode> _children = new Dictionary<string, CompressibleNode>();
        public IEnumerable<string> Entries => _children.Keys;
        private IEnumerable<(string name, IWritable buffer)> Writables => _children.Select(kvp => (kvp.Key, kvp.Value as IWritable));

        public BFast() { }
        public BFast(Stream stream)
        {
            var nodes = GetBFastNodes(stream);
            _children = nodes.ToDictionary(c => c.name, c => new CompressibleNode(c.value));
        }

        public void SetBFast(Func<int, string> getName, IEnumerable<BFast> others, bool compress = false)
        {
            var i = 0;
            foreach (var b in others)
            {
                SetBFast(getName(i++), b, compress);
            }
        }

        public void SetBFast(string name, BFast bfast, bool compress = false)
        {
            _children[name] = new CompressibleNode(bfast, compress);
        }

        public void SetEnumerable<T>(string name, Func<IEnumerable<T>> enumerable) where T : unmanaged
        {
            if (enumerable == null)
            {
                _children.Remove(name);
                return;
            }
            _children[name] = new CompressibleNode(new BFastEnumerableNode<T>(enumerable));
        }

        public void SetArray<T>(string name, T[] array) where T : unmanaged
        {
            if (array == null)
            {
                _children.Remove(name);
                return;
            }
            _children[name] = new CompressibleNode(new BFastArrayNode<T>(array));
        }

        public BFast GetBFast(string name, bool decompress = false)
        {
            var node = GetNode(name);
            if (node == null) return null;
            var n = decompress ? node.Decompress() : node.Node;
            return n.AsBFast();
        }

        public IEnumerable<T> GetEnumerable<T>(string name) where T : unmanaged
        {
            if (!_children.ContainsKey(name)) return null;
            return _children[name].Node.AsEnumerable<T>();
        }

        public T[] GetArray<T>(string name) where T : unmanaged
        {
            if (!_children.ContainsKey(name)) return null;
            return _children[name].Node.AsArray<T>();
        }

        public CompressibleNode GetNode(string name)
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

        BFast IBFastNode.AsBFast()
        {
            return this;
        }

        T[] IBFastNode.AsArray<T>()
        {
            using (var mem = ToMemoryStream())
            {
                return mem.ReadArrayBytes<T>();
            }
        }

        IEnumerable<T> IBFastNode.AsEnumerable<T>()
        {
            return (this as IBFastNode).AsArray<T>();
        }

        private static IEnumerable<(string name, BFastStreamNode value)> GetBFastNodes(Stream stream)
        {
            var offset = stream.Position;
            var raw = BFastHeader.FromStream(stream);
            foreach (var kvp in raw.Ranges)
            {
                var node = new BFastStreamNode(
                    stream,
                    kvp.Value.OffsetBy(offset)
                );

                yield return (kvp.Key, node);
            }
        }

        /// <summary>
        /// Writes the current bfast to a new memory streams
        /// The stream is returned at position 0.
        /// </summary>
        public MemoryStream ToMemoryStream()
        {
            var stream = new MemoryStream();
            Write(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public override bool Equals(object obj)
        {
            if (obj is BFast)
            {
                return Equals((BFast)obj);
            }
            return false;
        }

        public bool Equals(BFast other)
        {
            var a = (this as IBFastNode).AsEnumerable<byte>();
            var b = (other as IBFastNode).AsEnumerable<byte>();
            return a.SequenceEqual(b);
        }

        public override int GetHashCode() => (this as IBFastNode).AsEnumerable<byte>().GetHashCode();
    }
}

