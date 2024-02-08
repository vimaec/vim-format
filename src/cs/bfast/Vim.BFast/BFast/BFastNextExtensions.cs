using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Vim.BFastNS
{
    public static class BFastNextExtensions
    {
        public static T ReadBFast<T>(this string path, Func<BFast, T> process)
        {
            using (var file = new FileStream(path, FileMode.Open))
            {
                var bfast = new BFast(file);
                return process(bfast);
            }
        }

        public static IEnumerable<INamedBuffer> ToNamedBuffers(this BFast bfast)
        {
            return bfast.Entries.Select(name => bfast.GetArray<byte>(name).ToNamedBuffer(name));
        }

        public static MemoryStream ToMemoryStream(this BFast bfast)
        {
            var stream = new MemoryStream();
            bfast.Write(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }
}

