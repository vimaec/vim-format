using System.Collections.Generic;
using System.Linq;
using Vim.Format.ObjectModel;
using Vim.G3dNext.Attributes;

namespace Vim.Format.VimxNS.Conversion
{
    public static class VimxOrdering
    {
        public static IEnumerable<G3dMesh> OrderByBim(this IEnumerable<G3dMesh> meshes, DocumentModel bim)
        {
            return meshes.OrderByDescending((m) => (
                GetPriority(GetMeshName(m, bim)),
                m.GetAABB().MaxSide)
            );
        }

        static string GetMeshName(this G3dMesh mesh, DocumentModel bim)
        {
            var node = mesh.InstanceNodes[0];

            if (node < 0 || node >= bim.NodeElementIndex.Count) return "";
            var element = bim.NodeElementIndex[node];

            if (element < 0 || element >= bim.ElementCategoryIndex.Count) return "";
            var category = bim.ElementCategoryIndex[element];

            if (category < 0 || category >= bim.CategoryName.Count) return "";
            var name = bim.CategoryName[category];

            return name;
        }

        static int GetPriority(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return 0;

            if (value.Contains("Topography")) return 110;
            if (value.Contains("Floor")) return 100;
            if (value.Contains("Slab")) return 100;
            if (value.Contains("Ceiling")) return 90;
            if (value.Contains("Roof")) return 90;

            if (value.Contains("Curtain")) return 80;
            if (value.Contains("Wall")) return 80;
            if (value.Contains("Window")) return 70;

            if (value.Contains("Column")) return 60;
            if (value.Contains("Structural")) return 60;

            if (value.Contains("Stair")) return 40;
            if (value.Contains("Doors")) return 30;

            return 1;
        }
    }
}
