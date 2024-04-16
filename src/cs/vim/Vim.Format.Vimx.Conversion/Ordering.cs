using System;
using Vim.Format.ObjectModel;
using Vim.G3dNext;

namespace Vim.Format.VimxLib.Conversion
{
    public static class Ordering
    {
        public static MeshOrder ComputeOrder(G3dVim g3d, DocumentModel bim)
        {
            var meshCount = g3d.GetMeshCount();
            var resultCount = 0;
            for (var mesh = 0; mesh < meshCount; mesh++)
            {
                if (g3d.GetMeshInstances(mesh).Count > 0) resultCount++;
            }

            var i = 0;
            var order = new int[resultCount];
            var instanceCount = 0;
            for (var mesh = 0; mesh < meshCount; mesh++)
            {
                var instances = g3d.GetMeshInstances(mesh);
                if (instances.Count > 0)
                {
                    instanceCount += instances.Count;
                    order[i++] = mesh;
                }
            }
            Array.Sort(order, (a, b) =>
            {
                var prioA = GetPriority(GetMeshName(g3d, bim, a));
                var prioB = GetPriority(GetMeshName(g3d, bim, b));
                return prioA - prioB;
            });
            return new MeshOrder(g3d, order, instanceCount);
        }

        static string GetMeshName(G3dVim g3d, DocumentModel bim, int mesh)
        {
            var node = g3d.GetMeshInstances(mesh)[0];

            if (node < 0 || node >= bim.NodeElementIndex.Length) return "";
            var element = bim.NodeElementIndex[node];

            if (element < 0 || element >= bim.ElementCategoryIndex.Length) return "";
            var category = bim.ElementCategoryIndex[element];

            if (category < 0 || category >= bim.CategoryName.Length) return "";
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
