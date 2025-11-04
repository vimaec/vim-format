using System;
using System.Collections.Generic;
using System.Linq;
using Vim.Math3d;

namespace Vim.Format.api_v2
{
    public class VimMesh
    {
        public List<Vector3> Vertices { get; set; }
        public List<int> Indices { get; set; }
        public List<int> FaceMaterials { get; set; }
        public List<Vector4> Colors { get; set; }
        public List<Vector2> UVs { get; set; }

        public VimMesh(
            List<Vector3> vertices = null,
            List<int> indices = null,
            List<int> faceMaterials = null,
            List<Vector4> colors = null,
            List<Vector2> uvs = null)
        {
            Vertices = vertices ?? new List<Vector3>();
            Indices = indices ?? new List<int>();

            if (Indices.Any(i => i < 0 && i >= Vertices.Count))
                throw new Exception($"Invalid mesh. Indices out of vertex range.");

            if (Indices.Count % 3 != 0)
                throw new Exception("indices.Count must be a multiple of 3.");

            FaceMaterials = faceMaterials ?? new List<int>(Enumerable.Repeat(-1, Indices.Count / 3));

            if (FaceMaterials.Count * 3 != Indices.Count)
                throw new Exception("faceMaterials.Count must be indices.Count * 3");

            Colors = colors ?? new List<Vector4>();
            UVs = uvs ?? new List<Vector2>();
        }

        public void SetMeshMaterial(int material)
            => FaceMaterials = Enumerable.Repeat(material, Indices.Count / 3).ToList();

        public void AppendFaces(IList<int> indices, IList<int> materials)
        {
            if (indices.Count != materials.Count * 3)
                throw new Exception("index.Count must be material.Count*3");

            for (var i = 0; i < materials.Count; i++)
            {
                var index = i * 3;
                AppendFace(indices[index], indices[index + 1], indices[index + 2], materials[i]);
            }
        }

        public void AppendFace(int v0, int v1, int v2, int material)
        {
            Indices.Add(v0);
            Indices.Add(v1);
            Indices.Add(v2);
            FaceMaterials.Add(material);
        }

        public void AppendVertices(IEnumerable<Vector3> vertices)
            => Vertices.AddRange(vertices);

        public void AppendUVs(IEnumerable<Vector2> uvs)
            => UVs.AddRange(uvs);

        public VimSubdividedMesh Subdivide()
            => new VimSubdividedMesh(this);
    }
}