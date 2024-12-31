using System;
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

        public void Validate()
        {
            ValidateIndices();
        }

        private void ValidateIndices()
        {
            foreach (int index in Indices)
            {
                if (index < 0 || index >= Vertices.Length)
                    throw new Exception($"Invalid mesh index: {index}. Expected a value greater or equal to 0 and less than {Vertices.Length}");
            }
        }

        public IVimSubmesh Transform(Matrix4x4 mat)
        {
            var newVertices = new Vector3[Vertices.Length];

            for (var i = 0; i < Vertices.Length; i++)
            {
                newVertices[i] = Vertices[i].Transform(mat);
            }

            return new VimSubmesh2(Index, Material, newVertices, Indices);
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