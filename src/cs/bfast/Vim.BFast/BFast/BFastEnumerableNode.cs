using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.BFastLib.Core;

namespace Vim.BFastLib
{
    public class BFastEnumerableNode<TNode> : IBFastNode where TNode : unmanaged
    {
        // We use a Func<Enumerable> to prevent cases where the given IEnumerable can't iterated twice.
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
                return _source().Cast<TNode, T>().ToArray();
            }
        }

        public BFast AsBFast()
        {
            try
            {
                return new BFast(_source().ToMemoryStream());
            }
            catch (Exception e)
            {
                throw new Exception("Enumerable data is not a valid BFast", e);
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
                return _source().Cast<TNode, T>();
            }
        }

        public void Write(Stream stream)
        {
            stream.Write(_source());
        }
    }
}
