using System;
using System.Collections.Generic;
using System.Linq;
using Vim.G3d;
using Vim.G3dNext;
using Vim.LinqArray;
using Vim.Math3d;
using Vim.Util;
using static Vim.Format.DocumentBuilder;

namespace Vim.Format.Geometry
{
    public class VimMesh : IMeshCommon
    {
        public IArray<Vector3> Vertices => vertices.ToIArray();

        public IArray<int> Indices => indices.ToIArray();

        public IArray<int> SubmeshMaterials => submeshMaterials.ToIArray();
        public IArray<int> SubmeshIndexOffsets => submeshIndexOffsets.ToIArray();
        public IArray<int> SubmeshIndexCounts => submeshIndexCounts.ToIArray();

        public int NumCornersPerFace => 3;

        public int NumVertices => vertices.Length;

        public int NumCorners => indices.Length;

        public int NumFaces => indices.Length / 3;

        public int NumInstances => 0;

        public int NumMeshes => 1;

        public int NumShapeVertices => 0;

        public int NumShapes => 0;

        public int[] indices;
        public Vector3[] vertices;
        public int[] submeshIndexOffsets;
        public int[] submeshMaterials;
        public int[] submeshIndexCounts;

        public VimMesh()
        {

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
            var mesh = new VimMesh();
            mesh.indices = indices;
            mesh.vertices = vertices;
            mesh.submeshIndexOffsets = submeshIndexOffsets;
            mesh.submeshMaterials = submeshMaterials;
            mesh.submeshIndexCounts = submeshIndexCounts;
            return mesh;
        }
        IMeshCommon IMeshCommon.Clone() => Clone();

        public IMeshCommon Transform(Matrix4x4 mat)
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
    }

    public static class MeshCommonExtensions
    {
        public static IMeshCommon ReverseWindingOrder(this IMeshCommon mesh)
        {
            var result = mesh.Clone();
            var count = mesh.Indices.Count;
            var indices = new int[count];
            for (var i = 0; i < count; i += 3)
            {
                indices[i + 0] = mesh.Indices[i + 2];
                indices[i + 1] = mesh.Indices[i + 1];
                indices[i + 2] = mesh.Indices[i + 0];
            }
            result.SetIndices(indices);
            return result;
        }


        public static IArray<int> GetFaceMaterials(this IMeshCommon mesh)
        {
            // SubmeshIndexOffsets: [0, A, B]
            // SubmeshIndexCount:   [X, Y, Z]
            // SubmeshMaterials:    [L, M, N]
            // ---
            // FaceMaterials:       [...Repeat(L, X / 3), ...Repeat(M, Y / 3), ...Repeat(N, Z / 3)] <-- divide by 3 for the number of corners per Triangular face
            var numCornersPerFace = mesh.NumCornersPerFace;
            return mesh.SubmeshIndexCounts
                .ToEnumerable()
                .SelectMany((indexCount, i) => Enumerable.Repeat(mesh.SubmeshMaterials[i], indexCount / numCornersPerFace))
                .ToIArray();
        }

        public static IMeshCommon Merge2(this IMeshCommon mesh, params IMeshCommon[] others)
        {
            var meshes = Enumerable.Empty<IMeshCommon>()
                .Append(mesh)
                .Append(others)
                .ToArray();
            
            return meshes.Merge();
        }
        
        public static IMeshCommon Merge(this IMeshCommon[] meshes)
        {
            void Merge(IArray<int> from, int[] to, int offset, int increment)
            {
                for (var i = 0; i < from.Count; i++)
                {
                    to[i + offset] = from[i] + increment;
                }
            }

            // Init arrays
            var indexCount = meshes.Sum(m => m.Indices.Count);
            var vertexCount = meshes.Sum(m => m.Vertices.Count);
            var submeshCount = meshes.Sum(m => m.SubmeshIndexOffsets.Count);
            var result = new VimMesh(indexCount, vertexCount, submeshCount);

            var indexOffset = 0;
            var vertexOffset = 0;
            var submeshOffset = 0;
            // Copy and merge meshes
            for (var m = 0; m < meshes.Length; m++)
            {
                var mesh = meshes[m];

                Merge(mesh.Indices, result.indices, indexOffset, vertexOffset);
                mesh.Vertices.CopyTo(result.vertices, vertexOffset);
                mesh.SubmeshMaterials.CopyTo(result.submeshMaterials, submeshOffset);
                mesh.SubmeshIndexCounts.CopyTo(result.submeshIndexCounts, submeshOffset);
                Merge(mesh.SubmeshIndexOffsets, result.submeshIndexOffsets, submeshOffset, indexOffset);

                indexOffset += mesh.Indices.Count;
                vertexOffset += mesh.Vertices.Count;
                submeshOffset += mesh.SubmeshIndexOffsets.Count;

            }
            return result;
        }

        public static VimMesh[] SplitSubmeshes(this IMeshCommon mesh)
        {
            return null;
        }

        public static (int, List<int>)[] GroupSubmeshesByMaterials(this IMeshCommon mesh)
        {
            var submeshCount = mesh.SubmeshIndexOffsets.Count;
            var map = new Dictionary<int, List<int>>();
            for (var i = 0; i < submeshCount; i++)
            {
                var mat = mesh.SubmeshMaterials[i];
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


        public static (int mat, IMeshCommon mesh)[] SplitByMaterial(this IMeshCommon mesh)
        {
            var map = mesh.GroupSubmeshesByMaterials();

            var result = new (int, IMeshCommon)[map.Length];
            if (map.Length == 1)
            {
                result[0] = (mesh.SubmeshMaterials[0], mesh);
                return result;
            }

            for (var i =0; i < map.Length; i++)
            {
                var (mat, subs) = map[i];
                var pick = mesh.PickSubmeshes(subs);
                result[i] = (mat, pick);
            }
            return result;
        }

        public static VimMesh PickSubmeshes(this IMeshCommon mesh, IList<int> submeshes)
        {
            var map = mesh.GroupSubmeshesByMaterials();

            // Allocate arrays of the final sizes
            var indexCount = submeshes.Sum(s => mesh.SubmeshIndexCounts[s]);
            var result = new VimMesh(indexCount, indexCount, submeshes.Count);

            var indexOffset = 0;
            var index = 0;
            for (var s = 0; s < submeshes.Count; s++)
            {
                var submesh = submeshes[s];

                // copy indices at their new positions
                var indexStart = mesh.SubmeshIndexOffsets[submesh];
                var indexEnd = indexStart + mesh.SubmeshIndexCounts[submesh];
                for (var i = indexStart; i < indexEnd; i++)
                {
                    result.indices[index] = indexOffset + i - indexStart;
                    result.vertices[index] = mesh.Vertices[mesh.Indices[i]];
                    index++;
                }

                // submesh data is mostly the same
                result.submeshIndexCounts[s] = mesh.SubmeshIndexCounts[submesh];
                result.submeshMaterials[s] = mesh.SubmeshMaterials[submesh];
                result.submeshIndexOffsets[s] = indexOffset;

                // Update offset for next submesh
                indexOffset += indexEnd - indexStart;
            }

            return result;
        }
    }
}
