using System;
using System.Collections.Generic;
using System.IO;
using Vim.BFastLib.Core;

namespace Vim.BFastLib
{
    public class BFastArrayNode<TData> : IBFastNode where TData : unmanaged
    {
        private readonly TData[] _array;

        public BFastArrayNode(TData[] array)
        {
            _array = array;
        }

        public T[] AsArray<T>() where T : unmanaged
        {
            if (typeof(T) == typeof(TData))
            {
                return _array as T[];
            }
            return _array.Cast<TData, T>();
        }

        public BFast AsBFast()
        {
            try
            {
                return new BFast(_array.ToMemoryStream());
            }
            catch (Exception e)
            {
                throw new Exception("Array data is not a valid BFast.", e);
            }
        }

        public IEnumerable<T> AsEnumerable<T>() where T: unmanaged {
            return (_array as IEnumerable<TData>).Cast<TData, T>();
        }

        public void Write(Stream stream)
        {
            stream.Write(_array);
        }
    }
}
