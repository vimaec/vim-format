using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Vim.BFastNS
{
    public class BFastEnumerableNode<TNode> : IBFastNode where TNode : unmanaged
    {
        private readonly Func<IEnumerable<TNode>> _source;

        public BFastEnumerableNode(Func<IEnumerable<TNode>> source)
        {
            _source = source;
        }

        public T[] AsArray<T>() where T : unmanaged
        {
            if (typeof(T) == typeof(TNode))
            {
                return _source().Cast<T>().ToArray();
            }
            else
            {
                return _source().ToArray().Convert<TNode, T>();
            }
        }

        public BFast AsBFast()
        {
            try
            {
                return new BFast(_source().ToMemoryStream());
            }
            catch 
            {
                return null;
            }
        } 
        public IEnumerable<T> AsEnumerable<T>() where T : unmanaged
        {
            if (typeof(T) == typeof(TNode))
            {
                return _source().Cast<T>();
            }
            else
            {
                return _source().Convert<TNode, T>();
            }
        }

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
