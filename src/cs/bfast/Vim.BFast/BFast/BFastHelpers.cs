using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Vim.BFastLib
{
    public static class BFastHelpers
    {
        /// <summary>
        /// Opens a file as a BFast, applies func to it and closes the file.
        /// </summary>
        public static T Read<T>(string path, Func<BFast, T> func)
        {
            using (var file = new FileStream(path, FileMode.Open))
            {
                var bfast = new BFast(file);
                return func(bfast);
            }
        }
    }

    public static class BFastExtensions
    {
        /// <summary>
        /// Returns an enumerable of all nodes of the BFast as NamedBuffers.
        /// </summary>
        public static IEnumerable<INamedBuffer> ToNamedBuffers(this BFast bfast)
        {
            return bfast.Entries.Select(name => bfast.GetArray<byte>(name).ToNamedBuffer(name));
        }

        /// <summary>
        /// Writes the current bfast to a new memory streams
        /// The stream is returned at position 0.
        /// </summary>
        public static MemoryStream ToMemoryStream(this IBFastNode bfast)
        {
            var stream = new MemoryStream();
            bfast.Write(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }
}

