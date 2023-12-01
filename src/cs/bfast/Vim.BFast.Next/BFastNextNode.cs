using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Vim.BFast;

namespace Vim.BFastNextNS
{

   public class BFastEnumerableNode<TNode> : IBFastNextNode where TNode: unmanaged
    {
        private Func<IEnumerable<TNode>> _source;
        public BFastEnumerableNode(Func<IEnumerable<TNode>> source)
        {
            _source = source;
        }

        private BFastNextNode AsMemNode()
            => BFastNextNode.FromArray(_source().ToArray());

        public T[] AsArray<T>() where T : unmanaged
            => AsMemNode().AsArray<T>();

        public BFastNext AsBFast() => AsMemNode().AsBFast();
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



    public class BFastNextNode : IBFastNextNode
    {
        private readonly Stream _stream;
        private readonly BFastRange _range;
        private Action _cleanUp;

        public static BFastNextNode FromArray<T>(T[] array) where T : unmanaged
        {
            /*
            Is a memory leak created if a MemoryStream in .NET is not closed?
            -----------------------------------------]-------------------------
            You won't leak anything - at least in the current implementation.
            Calling Dispose won't clean up the memory used by MemoryStream any faster. It will stop your stream from being viable for Read/Write calls after the call, which may or may not be useful to you.
            If you're absolutely sure that you never want to move from a MemoryStream to another kind of stream, it's not going to do you any harm to not call Dispose. However, it's generally good practice partly because if you ever do change to use a different Stream, you don't want to get bitten by a hard-to-find bug because you chose the easy way out early on. (On the other hand, there's the YAGNI argument...)
            The other reason to do it anyway is that a new implementation may introduce resources which would be freed on Dispose.
            https://stackoverflow.com/questions/234059/is-a-memory-leak-created-if-a-memorystream-in-net-is-not-closed
            */
            var stream = new MemoryStream();
            stream.Write(array);
            return new BFastNextNode(stream, stream.FullRange());
        }


        public BFastNextNode(Stream stream, BFastRange range, Action cleanup = null)
        {
            _stream = stream;
            _range = range;
        }

        public BFastNext AsBFast()
        {
            _stream.Seek(_range.Begin, SeekOrigin.Begin);
            try
            {
                return new BFastNext(_stream);
            }
            catch (Exception ex)
            {
                // It is expected most buffers are not valid bfasts.
                return null;
            }
        }

        public T[] AsArray<T>() where T : unmanaged
        {
            _stream.Seek(_range.Begin, SeekOrigin.Begin);
            return _stream.ReadArrayBytes<T>((int)_range.Count);
        }

        public IEnumerable<T> AsEnumerable<T>() where T : unmanaged
            => AsArray<T>();


        public long GetSize()
        {
            return _range.Count;
        }

        public void Write(Stream stream)
        {
            _stream.Seek(_range.Begin, SeekOrigin.Begin);
            CopyStream(_stream, stream, (int)_range.Count);
        }

        private static void CopyStream(Stream input, Stream output, int bytes)
        {
            var buffer = new byte[32768];
            int read;
            while (bytes > 0 &&
                   (read = input.Read(buffer, 0, Math.Min(buffer.Length, bytes))) > 0)
            {
                output.Write(buffer, 0, read);
                bytes -= read;
            }
        }

        
    }
}
