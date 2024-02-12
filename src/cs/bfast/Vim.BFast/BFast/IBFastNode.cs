﻿using System.Collections.Generic;
using System.IO;

namespace Vim.BFastLib
{
    /// <summary>
    /// Anything that can be added to a BFAST must have a size and write to a stream.
    /// </summary>
    public interface IWritable
    {
        void Write(Stream stream);
    }

    public interface IBFastNode : IWritable
    {
        T[] AsArray<T>() where T : unmanaged;
        IEnumerable<T> AsEnumerable<T>() where T : unmanaged;

        BFast AsBFast();
    }
}
