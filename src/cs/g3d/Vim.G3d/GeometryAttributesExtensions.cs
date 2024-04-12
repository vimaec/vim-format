using System;
using System.Collections.Generic;
using System.Linq;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim.G3d
{
    public static class GeometryAttributesExtensions
    {
        public static GeometryAttribute<T> GetAttribute<T>(this IGeometryAttributes g, string attributeName) where T : unmanaged
            => g.GetAttribute(attributeName)?.AsType<T>();

        public static IGeometryAttributes ToGeometryAttributes(this IEnumerable<GeometryAttribute> attributes)
            => new GeometryAttributes(attributes);
            
        public static IGeometryAttributes AddAttributes(this IGeometryAttributes attributes, params GeometryAttribute[] newAttributes)
            => attributes.Attributes.ToEnumerable().Concat(newAttributes).ToGeometryAttributes();

        public static IGeometryAttributes Replace(this IGeometryAttributes self, Func<AttributeDescriptor, bool> selector, GeometryAttribute attribute)
            => self.Attributes.Where(a => !selector(a.Descriptor)).Append(attribute).ToGeometryAttributes();

        public static IGeometryAttributes Remove(this IGeometryAttributes self, Func<AttributeDescriptor, bool> selector)
            => self.Attributes.Where(a => !selector(a.Descriptor)).ToGeometryAttributes();

        public static G3D ToG3d(this IGeometryAttributes self)
            => G3D.Create(self.Attributes.ToArray());
    }
}
