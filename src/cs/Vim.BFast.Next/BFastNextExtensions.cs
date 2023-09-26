using System;
using System.IO.Compression;
using System.IO;
using System.Collections.Generic;

namespace Vim.BFastNext
{
    public static class BFastNextExtensions
    {
        public static BFastNext GetBFast(this BFastNext bfast, string name, bool inflate)
        {
            if (inflate == false) return bfast.GetBFast(name);
            var output = new MemoryStream();
            using (var input = new MemoryStream())
            {
                bfast.GetNode(name).Write(input);
                input.Seek(0, SeekOrigin.Begin);
                using (var compress = new DeflateStream(input, CompressionMode.Decompress, true))
                {
                    compress.CopyTo(output);
                    output.Seek(0, SeekOrigin.Begin);
                    return new BFastNext(output);
                }
            }
        }

        public static void SetBFast(this BFastNext bfast, string name, BFastNext other, bool deflate)
        {
            if (deflate == false) bfast.AddBFast(name, other);

            using (var output = new MemoryStream())
            {
                using (var decompress = new DeflateStream(output, CompressionMode.Compress, true))
                {
                    other.Write(decompress);
                }

                var a = output.ToArray();
                bfast.AddArray(name, a);
            }
        }

        public static void SetBFast(this BFastNext bfast, Func<int,string> getName, IEnumerable<BFastNext> others, bool deflate)
        {
            var i = 0;
            foreach(var b in others)
            {
                Console.WriteLine(i);
                bfast.SetBFast(getName(i++), b, deflate);
            }
        }

        public static T ReadBFast<T>(this string path, Func<BFastNext, T> process)
        {
            using (var file = new FileStream(path, FileMode.Open))
            {
                var bfast = new BFastNext(file);
                return process(bfast);
            }
        }


    }
}
