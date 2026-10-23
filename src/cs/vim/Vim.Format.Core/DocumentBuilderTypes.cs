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

            /// <summary>
            /// Optional (exporter "geometry pieces" option): the Vim.Category (usually a Revit subcategory) of each face, parallel to
            /// FaceMaterials; -1 = the element's own category. Null when not captured. Faces appended without one are padded with -1.
            /// </summary>
            public List<int> FaceCategories { get; set; }

            /// <summary>
            /// Optional (exporter "geometry pieces" option): the Vim.CompoundStructureLayer of each face, parallel to FaceMaterials;
            /// -1 = none. Null when not captured.
            /// </summary>
            public List<int> FaceLayers { get; set; }

            /// <summary>
            /// Appends the given tag to every face added since faceCountBefore, padding earlier faces with -1. Creates the list.
            /// </summary>
            public static List<int> TagFaces(List<int> tags, int faceCountBefore, int faceCountNow, int tag)
            {
                tags = tags ?? new List<int>();
                while (tags.Count < faceCountBefore)
                    tags.Add(-1);
                while (tags.Count < faceCountNow)
                    tags.Add(tag);
                return tags;
            }

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

            /// <summary>
            /// Optional: the Vim.Category of each submesh; null when the mesh carries none. Written to the g3d as
            /// g3d:submesh:category (BigG3dWriter.SubmeshCategoryAttribute) when at least one mesh carries it (exporter options
            /// "subcategory per part" or "geometry pieces"); -1 = the element's own category.
            /// </summary>
            public IReadOnlyList<int> SubmeshCategories { get; }

            /// <summary>Optional, in memory only (not a g3d buffer): the Vim.CompoundStructureLayer of each submesh; null when the mesh carries none.</summary>
            public IReadOnlyList<int> SubmeshLayers { get; }

            /// <summary>
            /// The per-vertex texture coordinates, or null when the mesh has none.
            /// When present, UVs.Count == Vertices.Count (a mesh whose UV count does not match its vertex count gets none).
            /// Written to the g3d as g3d:vertex:uv when at least one mesh carries them (exporter option "Textures").
            /// </summary>
            public IReadOnlyList<Vector2> UVs { get; }

            public SubdividedMesh(Mesh mesh)
            {
                if (mesh.Indices.Any(i => i < 0 && i >= mesh.Vertices.Count))
                    throw new Exception($"Invalid mesh. Indices out of vertex range.");

                // Faces are grouped by material (as always) and, when the mesh carries them, by category and layer too:
                // one submesh per (material, category, layer), in order of first appearance.
                var categories = mesh.FaceCategories != null && mesh.FaceCategories.Count > 0 ? mesh.FaceCategories : null;
                var layers = mesh.FaceLayers != null && mesh.FaceLayers.Count > 0 ? mesh.FaceLayers : null;
                int Tag(List<int> tags, int index) => tags != null && index < tags.Count ? tags[index] : -1;

                var facesByMats = mesh.FaceMaterials
                    .Select((face, index) => (face, index))
                    .GroupBy(pair => (material: pair.face, category: Tag(categories, pair.index), layer: Tag(layers, pair.index)), pair => pair.index);

                var submeshIndexOffset = new List<int>();
                var submeshMaterials = new List<int>();
                var submeshCategories = categories != null || layers != null ? new List<int>() : null;
                var submeshLayers = categories != null || layers != null ? new List<int>() : null;
                var indicesRemap = new List<int>();

                foreach (var group in facesByMats)
                {
                    submeshIndexOffset.Add(indicesRemap.Count);
                    submeshMaterials.Add(group.Key.material);
                    submeshCategories?.Add(group.Key.category);
                    submeshLayers?.Add(group.Key.layer);
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
                SubmeshCategories = submeshCategories;
                SubmeshLayers = submeshLayers;
                SubmeshesIndexOffset = submeshIndexOffset;

                Vertices = mesh.Vertices;
                UVs = mesh.UVs != null && mesh.UVs.Count > 0 && mesh.UVs.Count == mesh.Vertices.Count
                    ? mesh.UVs
                    : null;
            }

            public SubdividedMesh(
                IReadOnlyList<int> indices,
                IReadOnlyList<Vector3> vertices,
                IReadOnlyList<int> submeshesIndexOffset,
                IReadOnlyList<int> submeshMaterials,
                IReadOnlyList<int> submeshCategories = null,
                IReadOnlyList<Vector2> uvs = null
            )
            {
                Indices = indices;
                Vertices = vertices;
                SubmeshesIndexOffset = submeshesIndexOffset;
                SubmeshMaterials = submeshMaterials;
                SubmeshCategories = submeshCategories;
                UVs = uvs != null && uvs.Count > 0 && uvs.Count == vertices.Count ? uvs : null;
            }

            public bool IsEquivalentTo(SubdividedMesh other)
                => Vertices.SequenceEqual(other.Vertices)
                   && Indices.SequenceEqual(other.Indices)
                    && SubmeshesIndexOffset.SequenceEqual(other.SubmeshesIndexOffset)
                    && SubmeshMaterials.SequenceEqual(other.SubmeshMaterials)
                    // Categories are compared only when both sides carry them (a reader without category support sees none).
                    && (SubmeshCategories == null || other.SubmeshCategories == null || SubmeshCategories.SequenceEqual(other.SubmeshCategories))
                    && (UVs == null ? other.UVs == null : other.UVs != null && UVs.SequenceEqual(other.UVs));

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
