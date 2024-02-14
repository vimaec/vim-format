using System.Collections.Generic;
using System.IO;

namespace Vim.BFastLib
{
    public interface IWritable
    {
        void Write(Stream stream);
    }

    public interface IBFastNode : IWritable
    {
        /// <summary>
        /// Tries to cast node data as an array of T.
        /// </summary>
        T[] AsArray<T>() where T : unmanaged;

        /// <summary>
        /// Tries to cast node data as an enumerable of T.
        /// </summary
        IEnumerable<T> AsEnumerable<T>() where T : unmanaged;

        /// <summary>
        /// Tries to interpret node data as a BFast.
        /// </summary
        BFast AsBFast();
    }
}
