using System;
using System.IO;
using Vim.BFastNextNS;
using Vim.Buffers;
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

        public static BFastNext ToBFast(this IGeometryAttributes self, G3dHeader? header = null)
        {
            var bfast = new BFastNext();
            bfast.SetArray("meta", (header ?? G3dHeader.Default).ToBytes());
            foreach(var attribute in self.Attributes.ToEnumerable())
            {
                attribute.AddTo(bfast);
            }
            return bfast;
        }

        public static G3D ReadG3d(this Stream stream, Func<string, string> renameFunc = null)
        {
            var bfast = new BFastNext(stream);

            var header = G3dHeader.FromBytesOrDefault(bfast.GetArray<byte>("meta"));
            var attributes = new List<GeometryAttribute>();
            foreach (var name in bfast.Entries)
            {
                if (name == "meta") continue;
                var attribute = GetAttribute(name);
                var a = attribute.Read(bfast);
                attributes.Add(a);
            }

            return new G3D(attributes, header);
        }
        public static GeometryAttribute GetAttribute(string name)
        {
            if (!AttributeDescriptor.TryParse(name, out var attributeDescriptor))
            {
                return null;
            }
            try
            {
                return attributeDescriptor.ToDefaultAttribute(0);
            }
            catch
            {
                return null;
            }
        }
    }
}
