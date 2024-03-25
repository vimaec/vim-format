using Vim.BFastLib;
using Vim.LinqArray;

namespace Vim.G3d
{
    public static partial class G3DExtension
    {
        public static BFast ToBFast(this IGeometryAttributes self, G3dHeader? header = null)
        {
            var bfast = new BFast();
            bfast.SetArray("meta", (header ?? G3dHeader.Default).ToBytes());
            foreach(var attribute in self.Attributes.ToEnumerable())
            {
                attribute.AddTo(bfast);
            }
            return bfast;
        }
    }
}
