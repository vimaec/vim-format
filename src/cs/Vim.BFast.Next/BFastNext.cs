using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Vim.BFast;

namespace Vim.BFastNext
{
    public class BFastNext : IBFastNextNode
    {
        private Dictionary<string, IBFastNextNode> _children = new Dictionary<string, IBFastNextNode>();
        public IEnumerable<string> Entries => _children.Keys;
        private IEnumerable<(string, IWritable)> Writables => _children.Select(kvp => (kvp.Key, kvp.Value as IWritable));

        public BFastNext() { }
        public BFastNext(Stream stream)
        {
            var node = GetBFastNodes(stream);
            _children = node.ToDictionary(c => c.name, c => c.value as IBFastNextNode);
        }

        public void AddBFast(string name, BFastNext bfast)
            => _children[name] = bfast;

        public void AddArray<T>(string name, T[] array) where T : unmanaged
            => _children[name] = BFastNextNode.FromArray(array);

        public void AddArray<T>(Func<int, string> getName, IEnumerable<T[]> arrays) where T : unmanaged
        {
            var index = 0;
            foreach (var array in arrays)
            {
                AddArray(getName(index++), array);
            }
        }

        public BFastNext GetBFast(string name)
        {
            if (!_children.ContainsKey(name)) return null;
            var bfast = _children[name].AsBFast();
            _children[name] = bfast;
            return bfast;
        }

        public T[] GetArray<T>(string name) where T : unmanaged
        {
            if (!_children.ContainsKey(name)) return null;
            return _children[name].AsArray<T>();
        }

        public IBFastNextNode GetNode(string name)
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

        BFastNext IBFastNextNode.AsBFast()
        {
            return this;
        }

        T[] IBFastNextNode.AsArray<T>()
        {
            using (var stream = new MemoryStream())
            {
                Write(stream);
                var end = stream.Position;
                stream.Seek(0, SeekOrigin.Begin);
                return stream.ReadArrayBytes<T>((int)end);
            }
        }

        private static IEnumerable<(string name, BFastNextNode value)> GetBFastNodes(Stream stream)
        {
            var offset = stream.Position;
            var header = stream.ReadBFastHeader();
            for (var i = 1; i < header.Preamble.NumArrays; i++)
            {
                var node = new BFastNextNode(
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

            stream.WriteBFast(names, sizes, onBuffer);
        }

        private static long GetBFastSize(IEnumerable<(string name, IWritable value)> writables)
        {
            var values = writables.Select(w => w.value).ToArray();
            var sizes = values.Select(v => v.GetSize()).ToArray();
            var names = writables.Select(w => w.name).ToArray();

            var header = BFast.BFast.CreateBFastHeader(sizes, names);
            return header.Preamble.DataEnd;
        }
    }
}
