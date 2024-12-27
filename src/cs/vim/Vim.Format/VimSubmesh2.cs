using System.Linq;
using Vim.Math3d;

namespace Vim.Format
{
    public class VimSubmesh2 : IVimSubmesh
    {
        public int Index { get; }
        public IVimRenderMaterial Material { get; }
        public Vector3[] Vertices { get; }
        public int[] Indices { get; }
        
        public VimSubmesh2(int index, IVimRenderMaterial material, Vector3[] vertices, int[] indices)
        {
            Index = index;
            Material = material;
            Vertices = vertices;
            Indices = indices;
        }

        public static bool GeometryEquals(VimSubmesh2 a, VimSubmesh2 b, float tolerance = Constants.Tolerance)
        {
            if (!a.Indices.SequenceEqual(b.Indices))
                return false;

            if (a.Vertices.Length != b.Vertices.Length)
                return false;

            for (var i = 0; i < a.Vertices.Length; i++)
            {
                if (!a.Vertices[i].AlmostEquals(b.Vertices[i], tolerance))
                    return false;
            }

            return true;
        }
    }
}