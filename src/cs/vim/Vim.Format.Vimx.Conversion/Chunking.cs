using System.Collections.Generic;
using System.Linq;
using Vim.LinqArray;
using Vim.Math3d;
using Vim.G3dNext.Attributes;
using Vim.G3d;
using System.Xml.Serialization;
using System;

namespace Vim.Format.VimxNS.Conversion
{
    public static class Chunking
    {

        public static VimChunks CreateChunks(ChunksDescription description)
        {
            var chunks = new G3dChunk[description.ChunkMeshes.Count];
            for (var i = 0; i < chunks.Length; i++)
            {
                var meshes = description.ChunkMeshes[i];
                chunks[i] = CreateChunk2(description.g3d, meshes);
            }
            return new VimChunks(description, chunks);
        }

        public static ChunksDescription ComputeChunks(MeshOrder ordering)
        {
            // 2MB once compressed -> 0.5MB
            const int ChunkSize = 2000000;

            var meshChunks = new int[ordering.Meshes.Length];
            var meshIndex = new int[ordering.Meshes.Length];

            var chunks = new List<List<int>>();
            var chunk = new List<int>();
            var chunkSize = 0L;
            for (var i = 0; i < ordering.Meshes.Length; i++)
            {
                var mesh = ordering.Meshes[i];
                chunkSize += ordering.g3d.GetApproxSize(mesh);
                if (chunkSize > ChunkSize && chunk.Count > 0)
                {
                    chunks.Add(chunk);
                    chunk = new List<int>();
                    chunkSize = 0;
                }

                meshChunks[i] = chunks.Count;
                meshIndex[i] = chunk.Count;
                chunk.Add(mesh);
            }
            if (chunk.Count > 0)
            {
                chunks.Add(chunk);
            }

            return new ChunksDescription(ordering, meshChunks, meshIndex, chunks);
        }

        public static G3dChunk CreateChunk(G3dVim g3d, List<int> meshes)
        {
            var submeshOffsets = new List<int>() { 0 };
            var opaqueCounts = new List<int>();
            var submeshIndexOffsets = new List<int>();
            var submeshVertexOffsets = new List<int>();
            var submeshMaterials = new List<int>();
            var indices = new List<int>();
            var vertices = new List<Vector3>();

            for (var i = 0; i < meshes.Count; i++)
            {
                var mesh = meshes[i];

                var opaqueCount = AppendSubmeshes(
                    g3d,
                    mesh,
                    false,
                    submeshIndexOffsets,
                    submeshVertexOffsets,
                    submeshMaterials,
                    indices,
                    vertices
                );

                var transparentCount = AppendSubmeshes(
                    g3d,
                    mesh,
                    true,
                    submeshIndexOffsets,
                    submeshVertexOffsets,
                    submeshMaterials,
                    indices,
                    vertices
                );
                opaqueCounts.Add(opaqueCount);
                submeshOffsets.Add(submeshOffsets[i] + opaqueCount + transparentCount);
            }

            return new G3dChunk()
            {
                MeshSubmeshOffset = submeshOffsets.ToArray(),
                MeshOpaqueSubmeshCounts = opaqueCounts.ToArray(),
                SubmeshIndexOffsets = submeshIndexOffsets.ToArray(),
                SubmeshVertexOffsets = submeshVertexOffsets.ToArray(),
                SubmeshMaterials = submeshMaterials.ToArray(),
                Indices = indices.ToArray(),
                Positions = vertices.ToArray()
            };
        }


        public class SubmeshBuffer
        {
            int index = 0;
            public int[] IndexOffsets;
            public int[] VertexOffsets;
            public int[] Materials;

            public SubmeshBuffer(int count)
            {
                IndexOffsets = new int[count];
                VertexOffsets = new int[count];
                Materials = new int[count];
            }

            public void Add(int indexOffset, int vertexOffset, int material)
            {
                IndexOffsets[index] = indexOffset;
                VertexOffsets[index] = vertexOffset;
                Materials[index] = material;
                index++;
            }
        }

        public class PointsBuffer
        {
            public int[] indices;
            public List<Vector3> vertices;

            public int IndexCount { get; private set; } = 0;
            public int VertexCount => vertices.Count;  

            public PointsBuffer(int indexCount, int vertexCount)
            {
                indices = new int[indexCount];
                vertices = new List<Vector3>(vertexCount);
            }

            public void AddIndex(int index)
            {
                indices[IndexCount++] = index;
            }

            public void AddVertex(Vector3 vertex)
            {
                vertices.Add(vertex);
            }
        }

        public static G3dChunk CreateChunk2(G3dVim g3d, List<int> meshes)
        {
            var meshSubmeshOffsets = new int[meshes.Count + 1];
            var meshOpaqueCounts = new int[meshes.Count];

            var submeshCount = meshes.Sum(m => g3d.GetMeshSubmeshCount(m));
            var submeshBuffer = new SubmeshBuffer(submeshCount);

            var indexCount = meshes.Sum(m => g3d.GetMeshIndexCount(m));
            var vertexCount = meshes.Sum(m => g3d.GetMeshVertexCount(m));
            var pointsBuffer = new PointsBuffer(indexCount, vertexCount);

            for (var i = 0; i < meshes.Count; i++)
            {
                var mesh = meshes[i];

                var opaqueCount = AppendSubmeshes2(
                    g3d,
                    mesh,
                    false,
                    submeshBuffer,
                    pointsBuffer
                );

                var transparentCount = AppendSubmeshes2(
                    g3d,
                    mesh,
                    true,
                    submeshBuffer,
                    pointsBuffer
                );
                meshOpaqueCounts[i] = opaqueCount;
                meshSubmeshOffsets[i + 1] = meshSubmeshOffsets[i] + opaqueCount + transparentCount;
            }

            return new G3dChunk()
            {
                MeshSubmeshOffset = meshSubmeshOffsets,
                MeshOpaqueSubmeshCounts = meshOpaqueCounts,
                SubmeshIndexOffsets = submeshBuffer.IndexOffsets,
                SubmeshVertexOffsets = submeshBuffer.VertexOffsets,
                SubmeshMaterials = submeshBuffer.Materials,
                Indices = pointsBuffer.indices,
                Positions = pointsBuffer.vertices.ToArray()
            };
        }

        private static int AppendSubmeshes(
            G3dVim g3d,
            int mesh,
            bool transparent,
            List<int> submeshIndexOffsets,
            List<int> submeshVertexOffsets,
            List<int> submeshMaterials,
            List<int> indices,
            List<Vector3> vertices
        )
        {
            var subStart = g3d.GetMeshSubmeshStart(mesh);
            var subEnd = g3d.GetMeshSubmeshEnd(mesh);
            var count = 0;
            for (var sub = subStart; sub < subEnd; sub++)
            {
                var currentMat = g3d.SubmeshMaterials[sub];
                var color = currentMat > 0 ? g3d.MaterialColors[currentMat] : Vector4.One;
                var accept = color.W < 1 == transparent;

                if (!accept) continue;
                count++;
                submeshMaterials.Add(currentMat);
                submeshIndexOffsets.Add(indices.Count);
                submeshVertexOffsets.Add(vertices.Count);
                var (subIndices, subVertices) = g3d.GetSubmesh(sub);
                indices.AddRange(subIndices.Select(i => i + vertices.Count));
                vertices.AddRange(subVertices);
            }
            return count;
        }


        private static int AppendSubmeshes2(
            G3dVim g3d,
            int mesh,
            bool transparent,
            SubmeshBuffer submeshBuffer,
            PointsBuffer pointsBuffer
        )
        {
            var subStart = g3d.GetMeshSubmeshStart(mesh);
            var subEnd = g3d.GetMeshSubmeshEnd(mesh);
            var count = 0;
            for (var sub = subStart; sub < subEnd; sub++)
            {
                var currentMat = g3d.SubmeshMaterials[sub];
                var color = currentMat > 0 ? g3d.MaterialColors[currentMat] : Vector4.One;
                var accept = color.W < 1 == transparent;

                if (!accept) continue;
                count++;
                submeshBuffer.Add(pointsBuffer.IndexCount, pointsBuffer.VertexCount, currentMat);
                g3d.GetSubmesh2(sub, pointsBuffer);
            }
            return count;
        }


        private static (List<int>, List<Vector3>) GetSubmesh(this G3dVim g3d, int submesh)
        {
            var indices = new List<int>();
            var vertices = new List<Vector3>();
            var dict = new Dictionary<int, int>();

            var start = g3d.GetSubmeshIndexStart(submesh);
            var end = g3d.GetSubmeshIndexEnd(submesh);

            for (var i = start; i < end; i++)
            {
                var v = g3d.Indices[i];
                if (dict.ContainsKey(v))
                {
                    indices.Add(dict[v]);
                }
                else
                {
                    indices.Add(vertices.Count);
                    dict.Add(v, vertices.Count);
                    vertices.Add(g3d.Positions[v]);
                }
            }
            return (indices, vertices);
        }

        private static void GetSubmesh2(this G3dVim g3d, int submesh, PointsBuffer points)
        {
            var index = points.VertexCount;
            var dict = new Dictionary<int, int>();
            var start = g3d.GetSubmeshIndexStart(submesh);
            var end = g3d.GetSubmeshIndexEnd(submesh);

            for (var i = start; i < end; i++)
            {
                var v = g3d.Indices[i];
                if (dict.ContainsKey(v))
                {
                    points.AddIndex(dict[v]);
                }
                else
                {
                    points.AddIndex(index);
                    points.AddVertex(g3d.Positions[v]);
                    dict.Add(v, index);
                    index++;
                }
            }
        }
    }
}
