using System.Collections.Generic;
using System.IO;

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
            return _array.Convert<TData, T>();
        }

        public BFast AsBFast()
        {
            try
            {
                return new BFast(_array.ToMemoryStream());
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<T> AsEnumerable<T>() where T: unmanaged {
            return (_array as IEnumerable<TData>).Convert<TData, T>();
        }

        public void Write(Stream stream)
        {
            stream.Write(_array);
        }
    }
}
