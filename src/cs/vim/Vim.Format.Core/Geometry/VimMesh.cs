using System;
using System.Collections.Generic;
using System.Linq;
using Vim.G3dNext;
using Vim.LinqArray;
using Vim.Math3d;
using Vim.Util;

namespace Vim.Format.Geometry
{
    public class VimMesh : ITransformable3D<VimMesh>
    {
        public int SubmeshCount => submeshIndexCounts.Length;

        public int NumCornersPerFace => 3;

        public int NumVertices => vertices.Length;

        public int NumCorners => indices.Length;

        public int NumFaces => indices.Length / 3;


        public int[] indices;
        public Vector3[] vertices;
        public int[] submeshIndexOffsets;
        public int[] submeshMaterials;
        public int[] submeshIndexCounts;

        public int GetIndexCount() => indices.Length;
        public int GetVertexCount() => vertices.Length;


        public VimMesh()
        {

        }

        public VimMesh(Vector3[] vertices)
            : this(Enumerable.Range(0, vertices.Length).ToArray(), vertices)
        {

        }

        public VimMesh(
            int[] indices,
            Vector3[] vertices,
            int[] submeshIndexOffsets = null,
            int[] submeshMaterials = null,
            int[] submeshIndexCounts = null
            )
        {
            this.indices = indices;
            this.vertices = vertices;

            this.submeshIndexOffsets = submeshIndexOffsets ?? new int[1] { 0 };
            this.submeshMaterials = submeshMaterials ?? new int[1] { -1 };
            this.submeshIndexCounts = submeshIndexCounts ?? ComputeCounts(this.submeshIndexOffsets, this.indices.Length);
        }

        public G3dVim ToG3d()
        {
            return new G3dVim(
                indices: indices,
                positions: vertices,
                instanceMeshes: new[] { 0 },
                instanceTransforms: new[] { Matrix4x4.Identity },
                instanceParents: null,
                instanceFlags: null,
                meshSubmeshOffsets: new[] { 0 },
                submeshIndexOffsets: new[] { 0 },
                submeshMaterials: new[] { -1 },
                materialColors: null,
                materialGlossiness: null,
                materialSmoothness: null,
                shapeVertices: null,
                shapeColors: null,
                shapeVertexOffsets: null,
                shapeWidths: null);
        }

        private static int[] ComputeCounts(int[] offsets, int max)
        {
            var result = new int[offsets.Length];
            for (var i = 0; i < result.Length - 1; i++)
            {
                result[i] = offsets[i + 1] - offsets[i];
            }
            var last = result.Length - 1;
            result[last] = max - offsets[last];
            return result;
        }


        public VimMesh(int indexCount, int vertexCount, int submeshCount)
        {
            indices = new int[indexCount];
            vertices = new Vector3[vertexCount];
            submeshIndexOffsets = new int[submeshCount];
            submeshMaterials = new int[submeshCount];
            submeshIndexCounts = new int[submeshCount];
        }

        public VimMesh Clone()
        {
            var mesh = new VimMesh(
                indices,
                vertices,
                submeshIndexOffsets,
                submeshMaterials
            );
            return mesh;
        }
        public VimMesh Transform(Matrix4x4 mat)
        {
            var mesh = Clone();

            for (var i = 0; i < vertices.Length; i++)
            {
                mesh.vertices[i] = vertices[i].Transform(mat);
            }

            return mesh;
        }

        public static VimMesh FromG3d(G3dVim g3d, int index)
        {
            var mesh = new VimMesh();

            var vStart = g3d.GetMeshVertexStart(index);
            var vEnd = g3d.GetMeshVertexEnd(index);
            mesh.vertices = new Vector3[vEnd - vStart];
            for (var i = 0; i < mesh.vertices.Length; i++)
            {
                var v = vStart + i;
                mesh.vertices[i] = g3d.Positions[v];
            }

            var iStart = g3d.GetMeshIndexStart(index);
            var iEnd = g3d.GetMeshIndexEnd(index);
            mesh.indices = new int[iEnd - iStart];
            for (var i = 0; i < mesh.indices.Length; i++)
            {
                var j = iStart + i;
                mesh.indices[i] = g3d.Indices[j] - vStart;
            }

            var sStart = g3d.GetMeshSubmeshStart(index);
            var sEnd = g3d.GetMeshSubmeshEnd(index);
            mesh.submeshIndexOffsets = new int[sEnd - sStart];
            mesh.submeshMaterials = new int[sEnd - sStart];
            mesh.submeshIndexCounts = new int[sEnd - sStart];

            for (var i = 0; i < mesh.submeshMaterials.Length; i++)
            {
                var s = sStart + i;
                mesh.submeshIndexOffsets[i] = g3d.SubmeshIndexOffsets[s] - iStart;
                mesh.submeshMaterials[i] = g3d.SubmeshMaterials[s];
                mesh.submeshIndexCounts[i] = g3d.GetSubmeshIndexCount(s);
            }

            return mesh;
        }

        public static VimMesh FromG3d(G3dVim g3d)
        {
            var mesh = new VimMesh(
                g3d.Indices,
                g3d.Positions,
                g3d.SubmeshIndexOffsets,
                g3d.SubmeshMaterials
            );

            return mesh;
        }

        public static VimMesh FromQuad(int[] indices, Vector3[] vertices)
        {
            if (indices.Length % 4 != 0)
            {
                throw new ArgumentException("Indices count should be a multiple of 4.");
            }
            var faceCount = indices.Length / 4;
            var triIndices = new int[faceCount * 6];
            var cur = 0;
            for (var i = 0; i < faceCount; ++i)
            {
                triIndices[cur++] = indices[i * 4 + 0];
                triIndices[cur++] = indices[i * 4 + 1];
                triIndices[cur++] = indices[i * 4 + 2];
                triIndices[cur++] = indices[i * 4 + 0];
                triIndices[cur++] = indices[i * 4 + 2];
                triIndices[cur++] = indices[i * 4 + 3];
            }
            return new VimMesh(triIndices, vertices);
        }

        public static IEnumerable<VimMesh> GetAllMeshes(G3dVim g3d)
        {
            return Enumerable.Range(0, g3d.GetMeshCount()).Select(i => FromG3d(g3d, i));
        }

        public void SetVertices(Vector3[] vertices)
        {
            this.vertices = vertices;
        }
        public void SetIndices(int[] indices)
        {
            this.indices = indices;
        }

        public void Validate()
        {
            //TODO: Validate better
            ValidateIndices();
        }
        private void ValidateIndices()
        {
            foreach (var index in indices)
            {
                if (index < 0 || index >= NumVertices)
                    throw new Exception($"Invalid mesh index: {index}. Expected a value greater or equal to 0 and less than {NumVertices}");
            }
        }
    }

    public static class MeshCommonExtensions
    {
        public static VimMesh ReverseWindingOrder(this VimMesh mesh)
        {
            var result = mesh.Clone();
            var count = mesh.indices.Length;
            var indices = new int[count];
            for (var i = 0; i < count; i += 3)
            {
                indices[i + 0] = mesh.indices[i + 2];
                indices[i + 1] = mesh.indices[i + 1];
                indices[i + 2] = mesh.indices[i + 0];
            }
            result.SetIndices(indices);
            return result;
        }


        public static int[] GetFaceMaterials(this VimMesh mesh)
        {
            // SubmeshIndexOffsets: [0, A, B]
            // SubmeshIndexCount:   [X, Y, Z]
            // SubmeshMaterials:    [L, M, N]
            // ---
            // FaceMaterials:       [...Repeat(L, X / 3), ...Repeat(M, Y / 3), ...Repeat(N, Z / 3)] <-- divide by 3 for the number of corners per Triangular face
            var numCornersPerFace = mesh.NumCornersPerFace;
            return mesh.submeshIndexCounts
                .SelectMany((indexCount, i) => Enumerable.Repeat(mesh.submeshMaterials[i], indexCount / numCornersPerFace))
                .ToArray();
        }

        public static VimMesh Merge2(this VimMesh mesh, params VimMesh[] others)
        {
            var meshes = Enumerable.Empty<VimMesh>()
                .Append(mesh)
                .Concat(others)
                .ToArray();

            return meshes.Merge();
        }

        public static VimMesh Merge(this VimMesh[] meshes)
        {
            void Merge(int[] from, int[] to, int offset, int increment)
            {
                for (var i = 0; i < from.Length; i++)
                {
                    to[i + offset] = from[i] + increment;
                }
            }

            // Init arrays
            var indexCount = meshes.Sum(m => m.indices.Length);
            var vertexCount = meshes.Sum(m => m.vertices.Length);
            var submeshCount = meshes.Sum(m => m.submeshIndexOffsets.Length);
            var result = new VimMesh(indexCount, vertexCount, submeshCount);

            var indexOffset = 0;
            var vertexOffset = 0;
            var submeshOffset = 0;
            // Copy and merge meshes
            for (var m = 0; m < meshes.Length; m++)
            {
                var mesh = meshes[m];

                Merge(mesh.indices, result.indices, indexOffset, vertexOffset);
                mesh.vertices.CopyTo(result.vertices, vertexOffset);
                mesh.submeshMaterials.CopyTo(result.submeshMaterials, submeshOffset);
                mesh.submeshIndexCounts.CopyTo(result.submeshIndexCounts, submeshOffset);
                Merge(mesh.submeshIndexOffsets, result.submeshIndexOffsets, submeshOffset, indexOffset);

                indexOffset += mesh.indices.Length;
                vertexOffset += mesh.vertices.Length;
                submeshOffset += mesh.submeshIndexOffsets.Length;

            }
            return result;
        }

        public static VimMesh[] SplitSubmeshes(this VimMesh mesh)
        {
            return null;
        }

        public static (int, List<int>)[] GroupSubmeshesByMaterials(this VimMesh mesh)
        {
            var submeshCount = mesh.submeshIndexOffsets.Length;
            var map = new Dictionary<int, List<int>>();
            for (var i = 0; i < submeshCount; i++)
            {
                var mat = mesh.submeshMaterials[i];
                if (map.ContainsKey(mat))
                {
                    map[mat].Add(i);
                }
                else
                {
                    map.Add(mat, new List<int>() { i });
                }
            }
            return map.Select(kvp => (kvp.Key, kvp.Value)).ToArray();
        }

        public static Triangle VertexIndicesToTriangle(this VimMesh mesh, Int3 indices)
            => new Triangle(mesh.vertices[indices.X], mesh.vertices[indices.Y], mesh.vertices[indices.Z]);

        public static bool Planar(this VimMesh mesh, float tolerance = Math3d.Constants.Tolerance)
        {
            if (mesh.NumFaces <= 1) return true;
            var normal = mesh.Triangle(0).Normal;
            return mesh.ComputedNormals().All(n => n.AlmostEquals(normal, tolerance));
        }

        public static VimMesh Unindex(this VimMesh mesh)
        {
            var vertices = mesh.indices.Select(i => mesh.vertices[i]);
            return new VimMesh(vertices.ToArray());
        }

        public static Vector3[] ComputedNormals(this VimMesh mesh)
            => mesh.Triangles().Select(t => t.Normal).ToArray();

        public static Triangle Triangle(this VimMesh mesh, int face)
            => mesh.VertexIndicesToTriangle(mesh.FaceVertexIndices(face));

        public static Triangle[] Triangles(this VimMesh mesh)
            => Enumerable.Range(0, mesh.NumFaces).Select(mesh.Triangle).ToArray();

        public static Vector3 Center(this VimMesh mesh)
            => mesh.BoundingBox().Center;

        public static AABox BoundingBox(this VimMesh mesh)
            => AABox.Create(mesh.vertices);

        public static Int3 FaceVertexIndices(this VimMesh mesh, int faceIndex)
            => new Int3(mesh.indices[faceIndex * 3], mesh.indices[faceIndex * 3 + 1], mesh.indices[faceIndex * 3 + 2]);

        public static bool GeometryEquals(this VimMesh mesh, VimMesh other, float tolerance = Math3d.Constants.Tolerance)
        {
            if (!mesh.indices.SequenceEqual(other.indices))
                return false;

            if (!mesh.submeshIndexOffsets.SequenceEqual(other.submeshIndexOffsets))
                return false;

            if (!mesh.submeshMaterials.SequenceEqual(other.submeshMaterials))
                return false;

            if (!mesh.submeshIndexCounts.SequenceEqual(other.submeshIndexCounts))
                return false;

            if (mesh.vertices.Length != other.vertices.Length)
                return false;

            for (var i = 0; i < mesh.vertices.Length; i++)
            {
                if (!mesh.vertices[i].AlmostEquals(other.vertices[i], tolerance))
                    return false;
            }

            return true;
        }

        public static (int mat, VimMesh mesh)[] SplitByMaterial(this VimMesh mesh)
        {
            var map = mesh.GroupSubmeshesByMaterials();

            var result = new (int, VimMesh)[map.Length];
            if (map.Length == 1)
            {
                result[0] = (mesh.submeshMaterials[0], mesh);
                return result;
            }

            for (var i = 0; i < map.Length; i++)
            {
                var (mat, subs) = map[i];
                var pick = mesh.PickSubmeshes(subs);
                result[i] = (mat, pick);
            }
            return result;
        }

        public static VimMesh PickSubmeshes(this VimMesh mesh, IList<int> submeshes)
        {
            var map = mesh.GroupSubmeshesByMaterials();

            // Allocate arrays of the final sizes
            var indexCount = submeshes.Sum(s => mesh.submeshIndexCounts[s]);
            var result = new VimMesh(indexCount, indexCount, submeshes.Count);

            var indexOffset = 0;
            var index = 0;
            for (var s = 0; s < submeshes.Count; s++)
            {
                var submesh = submeshes[s];

                // copy indices at their new positions
                var indexStart = mesh.submeshIndexOffsets[submesh];
                var indexEnd = indexStart + mesh.submeshIndexCounts[submesh];
                for (var i = indexStart; i < indexEnd; i++)
                {
                    result.indices[index] = indexOffset + i - indexStart;
                    result.vertices[index] = mesh.vertices[mesh.indices[i]];
                    index++;
                }

                // submesh data is mostly the same
                result.submeshIndexCounts[s] = mesh.submeshIndexCounts[submesh];
                result.submeshMaterials[s] = mesh.submeshMaterials[submesh];
                result.submeshIndexOffsets[s] = indexOffset;

                // Update offset for next submesh
                indexOffset += indexEnd - indexStart;
            }

            return result;
        }
    }
}
