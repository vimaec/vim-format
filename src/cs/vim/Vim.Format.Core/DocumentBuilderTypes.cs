using System;
using System.Collections.Generic;
using System.Linq;
using Vim.Format.Geometry;
using Vim.G3d;
using Vim.Math3d;

namespace Vim.Format
{
    public partial class DocumentBuilder
    {
        public class Instance
        {
            public Matrix4x4 Transform;
            public int MeshIndex;
            public int ParentIndex;
            public InstanceFlags InstanceFlags;
        }

        public class Shape
        {
            public List<Vector3> Vertices;
            public Vector4 Color;
            public float Width;
        }

        public class Material : IMaterial
        {
            //RGBA
            public Vector4 Color { get;  set; }
            public float Glossiness { get; set; }
            public float Smoothness { get; set; }
        }

        /// <summary>
        /// Appends faces while keeping the mesh invariants then subdivide into submesh.
        /// </summary>
        public class Mesh
        {
            private readonly List<Vector3> _vertices;
            public IReadOnlyList<Vector3> Vertices => _vertices;

            private readonly List<int> _indices;
            public IReadOnlyList<int> Indices => _indices;

            private List<int> _faceMaterials;
            public IReadOnlyList<int> FaceMaterials => _faceMaterials;

            private readonly List<Vector4> _colors;
            public IReadOnlyList<Vector4> Colors => _colors;

            private readonly List<Vector2> _uvs;
            public IReadOnlyList<Vector2> UVs => _uvs;

            public Mesh(List<Vector3> vertices = null, List<int> indices = null, List<int> faceMaterials = null, List<Vector4> colors = null, List<Vector2> uvs = null)
            {
                _vertices = vertices ?? new List<Vector3>();
                _indices = indices ?? new List<int>();

                if (_indices.Any(i => i < 0 && i >= _vertices.Count))
                    throw new Exception($"Invalid mesh. Indices out of vertex range.");

                if (_indices.Count % 3 != 0)
                    throw new Exception("indices.Count must be a multiple of 3.");

                _faceMaterials = faceMaterials ?? new List<int>(Enumerable.Repeat(-1, _indices.Count / 3));

                if (_faceMaterials.Count * 3 != _indices.Count)
                    throw new Exception("faceMaterials.Count must be indices.Count * 3");

                _colors = colors ?? new List<Vector4>();
                _uvs = uvs ?? new List<Vector2>();
            }


            public void SetMeshMaterial(int material)
                => _faceMaterials = Enumerable.Repeat(material, _indices.Count / 3).ToList();

            public void AppendFaces(int[] indices, int[] materials)
            {
                if (indices.Length != materials.Length * 3)
                    throw new Exception("index.Length must be material.Length*3");

                for (var i = 0; i < materials.Length; i++)
                {
                    var index = i * 3;
                    AppendFace(indices[index], indices[index + 1], indices[index + 2], materials[i]);
                }
            }

            public void AppendFace(int v0, int v1, int v2, int material)
            {
                _indices.Add(v0);
                _indices.Add(v1);
                _indices.Add(v2);
                _faceMaterials.Add(material);
            }

            public void AppendVertices(IEnumerable<Vector3> vertices)
                => _vertices.AddRange(vertices);

            public void AppendUVs(IEnumerable<Vector2> uvs)
                => _uvs.AddRange(uvs);

            public VimMesh Subdivide()
            {
                if (Indices.Any(i => i < 0 && i >= Vertices.Count))
                    throw new Exception($"Invalid mesh. Indices out of vertex range.");

                var facesByMats = FaceMaterials
                    .Select((face, index) => (face, index))
                    .GroupBy(pair => pair.face, pair => pair.index);

                var submeshIndexOffsets = new List<int>();
                var submeshMaterials = new List<int>();
                var indicesRemap = new List<int>();

                foreach (var group in facesByMats)
                {
                    submeshIndexOffsets.Add(indicesRemap.Count);
                    submeshMaterials.Add(group.Key);
                    foreach (var face in group)
                    {
                        var f = face * 3;
                        indicesRemap.Add(Indices[f]);
                        indicesRemap.Add(Indices[f + 1]);
                        indicesRemap.Add(Indices[f + 2]);
                    }
                }
                return new VimMesh(
                    indicesRemap.ToArray(),
                    Vertices.ToArray(),
                    submeshIndexOffsets.ToArray(),
                    submeshMaterials.ToArray()
                );

            }
        }
    }
}
