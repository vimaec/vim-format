using System;
using System.IO;
using Vim.BFastNS;
using Vim.LinqArray;
using System.Collections.Generic;

namespace Vim.G3d
{
    public static partial class G3DExtension
    {
        public static void WriteAttribute(Stream stream, GeometryAttribute attribute, string name, long size)
        {
            var buffer = attribute.ToBuffer();
            if (buffer.NumBytes() != size)
                throw new Exception($"Internal error while writing attribute, expected number of bytes was {size} but instead was {buffer.NumBytes()}");
            if (buffer.Name != name)
                throw new Exception($"Internal error while writing attribute, expected name was {name} but instead was {buffer.Name}");
            stream.Write(buffer);
        }

        public static BFastNS.BFast ToBFast(this IGeometryAttributes self, G3dHeader? header = null)
        {
            var bfast = new BFastNS.BFast();
            bfast.SetArray("meta", (header ?? G3dHeader.Default).ToBytes());
            foreach(var attribute in self.Attributes.ToEnumerable())
            {
                attribute.AddTo(bfast);
            }
            return bfast;
        }


   
    }
}
