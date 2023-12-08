using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Vim.BFastNS
{
    public class BFastEnumerableNode<TNode> : IBFastNode where TNode : unmanaged
    {
        private Func<IEnumerable<TNode>> _source;
        public BFastEnumerableNode(Func<IEnumerable<TNode>> source)
        {
            _source = source;
        }

        private BFastNode AsMemNode()
            => BFastNode.FromArray(_source().ToArray());

        public T[] AsArray<T>() where T : unmanaged
            => AsMemNode().AsArray<T>();

        public BFast AsBFast() => AsMemNode().AsBFast();
        public IEnumerable<T> AsEnumerable<T>() where T : unmanaged
        {
            var stream = new MemoryStream();
            var array = new T[1048576];
            var chunks = Chunkify(_source(), 1048576);
            while (chunks.MoveNext())
            {
                (var chunk, var chunkSize) = chunks.Current;
                stream.Seek(0, SeekOrigin.Begin);
                stream.Write(chunk, chunkSize);
                var count = ReadArray(stream, array);
                
                if (count > 0)
                {
                    for( var i = 0; i < count; i++)
                    {
                        yield return array[i];
                    }
                }
            }
        }

        static IEnumerator<(T[], int)> Chunkify<T>(IEnumerable<T> source, int chunkSize)
        {
            var chunk = new T[chunkSize];
            var index = 0;

            foreach (var item in source)
            {
                chunk[index++] = item;

                if (index == chunkSize)
                {
                    yield return (chunk, index);
                    index = 0;
                }
            }

            if (index > 0)
            {
                yield return (chunk, index);
            }
        }

        // Function is extracted because unsafe code cannot appear in generator
        private unsafe int ReadArray<T>(MemoryStream stream, T[] array) where T : unmanaged
        {
            var length = (int)stream.Position;
            if (length < sizeof(T))
            {
                return 0;
            }

            var count = length / sizeof(T);
            stream.Seek(0, SeekOrigin.Begin);
            stream.ReadArray<T>(array, count);
            return count;
        }

        public unsafe long GetSize() => _source().Count() * sizeof(TNode);
        public void Write(Stream stream)
        {
            //TODO: Use bigger chunks
            var array = new TNode[1];
            foreach(var value in _source())
            {
                array[0] = value;
                stream.Write(array);
            }
        }
    }
}
