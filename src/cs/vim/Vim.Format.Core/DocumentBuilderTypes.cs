using System;
using System.Collections.Generic;
using System.Linq;
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

        public class Material
        {
            //RGBA
            public Vector4 Color;
            public float Glossiness;
            public float Smoothness;
        }

        /// <summary>
        /// Appends faces while keeping the mesh invariants then subdivide into submesh.
        /// </summary>
        public class Mesh
        {
            public List<Vector3> Vertices { get; set; }
            public List<int> Indices { get; set; }
            public List<int> FaceMaterials { get; set; }
            public List<Vector4> Colors { get; set; }
            public List<Vector2> UVs { get; set; }

            public Mesh(
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

            public SubdividedMesh Subdivide()
                => new SubdividedMesh(this);
        }

        /// <summary>
        /// An immutable mesh were faces have been organized by submesh/material
        /// </summary>
        public class SubdividedMesh
        {
            public IReadOnlyList<int> Indices { get; }
            public IReadOnlyList<Vector3> Vertices { get; }
            public IReadOnlyList<int> SubmeshesIndexOffset { get; }
            public IReadOnlyList<int> SubmeshMaterials { get; }

            public SubdividedMesh(Mesh mesh)
            {
                if (mesh.Indices.Any(i => i < 0 && i >= mesh.Vertices.Count))
                    throw new Exception($"Invalid mesh. Indices out of vertex range.");

                var facesByMats = mesh.FaceMaterials
                    .Select((face, index) => (face, index))
                    .GroupBy(pair => pair.face, pair => pair.index);

                var submeshIndexOffset = new List<int>();
                var submeshMaterials = new List<int>();
                var indicesRemap = new List<int>();

                foreach (var group in facesByMats)
                {
                    submeshIndexOffset.Add(indicesRemap.Count);
                    submeshMaterials.Add(group.Key);
                    foreach (var face in group)
                    {
                        var f = face * 3;
                        indicesRemap.Add(mesh.Indices[f]);
                        indicesRemap.Add(mesh.Indices[f + 1]);
                        indicesRemap.Add(mesh.Indices[f + 2]);
                    }
                }
                Indices = indicesRemap;
                SubmeshMaterials = submeshMaterials;
                SubmeshesIndexOffset = submeshIndexOffset;

                Vertices = mesh.Vertices;
            }

            public SubdividedMesh(
                IReadOnlyList<int> indices,
                IReadOnlyList<Vector3> vertices,
                IReadOnlyList<int> submeshesIndexOffset,
                IReadOnlyList<int> submeshMaterials
            )
            {
                Indices = indices;
                Vertices = vertices;
                SubmeshesIndexOffset = submeshesIndexOffset;
                SubmeshMaterials = submeshMaterials;
            }

            public bool IsEquivalentTo(SubdividedMesh other)
                => Vertices.SequenceEqual(other.Vertices)
                   && Indices.SequenceEqual(other.Indices)
                    && SubmeshesIndexOffset.SequenceEqual(other.SubmeshesIndexOffset)
                    && SubmeshMaterials.SequenceEqual(other.SubmeshMaterials);

            public static SubdividedMesh CreateFromWorldSpaceBox(IReadOnlyList<Vector3> points, int materialIndex)
            {
                //     7 *--------------------------* 6 (max)
                //       |\                         |\
                //       | \                        | \
                //       |  \ 4                     |  \ 5
                //       |   *--------------------------*
                //       |   |                      |   |
                //     3 *---|----------------------* 2 |
                //        \  |                       \  |
                //         \ |                        \ |
                //          \|                         \|
                //           *--------------------------*
                //    (min) 0                             1

                // Faces are declared as quads in counter-clockwise order (right-handed rule, thumb points in normal direction)
                var bottomQuad = new[] { points[0], points[3], points[2], points[1] };
                var topQuad = new[] { points[4], points[5], points[6], points[7] };
                var backQuad = new[] { points[2], points[3], points[7], points[6] };
                var frontQuad = new[] { points[0], points[1], points[5], points[4] };
                var leftQuad = new[] { points[3], points[0], points[4], points[7] };
                var rightQuad = new[] { points[2], points[6], points[5], points[1] };

                var quads = new[] { bottomQuad, topQuad, backQuad, frontQuad, leftQuad, rightQuad };

                var vertices = new List<Vector3>();
                var indices = new List<int>();

                var quadIndices = new[] { 0, 1, 2, 0, 2, 3 };

                foreach (var quadVertices in quads)
                {
                    var indexOffset = vertices.Count;
                    vertices.AddRange(quadVertices);
                    indices.AddRange(quadIndices.Select(i => i + indexOffset));
                }

                return new SubdividedMesh(indices, vertices, new[] { 0 }, new[] { materialIndex });
            }
        }
    }
}
