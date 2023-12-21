using System.Linq;
using Vim.LinqArray;
using Vim.BFastNS;
using Vim.G3dNext.Attributes;
using Vim.Format.ObjectModel;
using Vim.G3dNext;
using System.Collections.Generic;
using Vim.Math3d;

namespace Vim.Format.VimxNS.Conversion
{
    public static class VimxConverter
    {
        public static Vimx FromVimPath(string vimPath)
        {
            var vim = VimScene.LoadVim(vimPath, new LoadOptions()
            {
                SkipAssets = true,
                SkipGeometry = true,
            });

            var g3d = G3dVim.FromVim(vimPath);
            return FromVim2(g3d, vim.DocumentModel);
        }


        public static Vimx FromVim2(G3dVim g3d, DocumentModel bim)
        {
            var meshOrder = VimxOrdering.OrderByBim2(g3d, bim).ToArray();
            var instanceNodes = meshOrder.SelectMany(m => g3d.GetMeshInstances(m));
            var instanceMeshes = meshOrder.SelectMany((m,i) => g3d.GetMeshInstances(m).Select(_ => i)).ToArray();
            var instanceTransforms = instanceNodes.Select(n => g3d.InstanceTransforms[n]).ToArray();
            var instanceFlags = g3d.InstanceFlags != null
                ? instanceNodes.Select(n => g3d.InstanceFlags[n]).ToArray()
                : null;

            var nodeElements = bim.NodeElementIndex.ToArray();
            var instanceGroups = instanceNodes.Select(n => nodeElements[n]).ToArray();

            var nodeElementIds = nodeElements
                .Select(n => bim.ElementId.SafeGet(n, -1))
                .ToArray();

            var chunks = SplitChunks2(g3d, meshOrder);

            var meshes = chunks.Chunks.Select(c => VimToMeshes.GetMesh2(g3d, c)).ToArray();
            var indexCount = meshOrder.Select((m, i) => meshes[chunks.MeshChunks[i]].GetMeshIndexCount(chunks.MeshIndex[i], MeshSection.All));
            var vertexCount = meshOrder.Select((m, i) => meshes[chunks.MeshChunks[i]].GetMeshVertexCount(chunks.MeshIndex[i], MeshSection.All));
            var opaqueIndexCount = meshOrder.Select((m, i) => meshes[chunks.MeshChunks[i]].GetMeshIndexCount(chunks.MeshIndex[i], MeshSection.Opaque));
            var opaqueVertexCount = meshOrder.Select((m, i) => meshes[chunks.MeshChunks[i]].GetMeshVertexCount(chunks.MeshIndex[i], MeshSection.Opaque));

            var boxes = instanceMeshes
                .Select((m, i) => meshes[chunks.MeshChunks[m]].GetAABox(chunks.MeshIndex[m], instanceTransforms[i]))
                .ToArray();
            var mins = boxes.Select(b => b.Min).ToArray();
            var maxs = boxes.Select(b => b.Max).ToArray();

            var scene = new G3dScene()
            {
                ChunkCount = new[] { chunks.Chunks.Count },
                InstanceMeshes = instanceMeshes.ToArray(),
                InstanceTransformData = instanceTransforms,
                InstanceNodes = instanceNodes.ToArray(),
                InstanceFlags = instanceFlags,
                InstanceGroups = instanceGroups,
                InstanceMaxs = maxs,
                InstanceMins = mins,
                InstanceTags = nodeElementIds,
                MeshChunks = chunks.MeshChunks.ToArray(),
                MeshChunkIndices = chunks.MeshIndex.ToArray(),
                MeshIndexCounts = indexCount.ToArray(),
                MeshVertexCounts = vertexCount.ToArray(),
                MeshOpaqueIndexCounts = opaqueIndexCount.ToArray(),
                MeshOpaqueVertexCounts = opaqueVertexCount.ToArray(),
            };

            var materials = new G3dMaterials().ReadFromVim(g3d);
            var header = VimxHeader.CreateDefault();

            return new Vimx(header, MetaHeader.Default, scene, materials, meshes);
        }


        public static Vimx FromVim(G3dVim g3d, DocumentModel bim)
        {
            var meshes = VimToMeshes.ExtractMeshes(g3d)
                .OrderByBim(bim)
                .ToArray();

            var chunks = meshes.SplitChunks();
            var scene = MeshesToScene.CreateScene(g3d, bim, chunks, meshes);
            var materials = new G3dMaterials().ReadFromVim(g3d);
            var header = VimxHeader.CreateDefault();

            return new Vimx(header, MetaHeader.Default, scene, materials, meshes);
        }

        public static VimxChunk[] SplitChunks(this IEnumerable<G3dMesh> meshes)
        {
            //2MB once compressed -> 0.5MB
            const int ChunkSize = 2000000;

            var chunks = new List<VimxChunk>();
            var chunk = new VimxChunk();
            var chunkSize = 0L;
            foreach (var mesh in meshes)
            {
                chunkSize += mesh.GetSize();
                if (chunkSize > ChunkSize && chunk.Meshes.Count > 0)
                {
                    chunks.Add(chunk);
                    chunk = new VimxChunk();
                    chunkSize = 0;
                }
                mesh.Chunk = chunks.Count;
                mesh.ChunkIndex = chunk.Meshes.Count();
                chunk.Meshes.Add(mesh);
            }
            if (chunk.Meshes.Count > 0)
            {
                chunks.Add(chunk);
            }

            return chunks.ToArray();
        }

        public static ChunkResult SplitChunks2(G3dVim g3d, int[] meshes)
        {
            // 2MB once compressed -> 0.5MB
            const int ChunkSize = 2000000;

            var meshChunks = new List<int>();
            var meshIndex = new List<int>();

            var chunks = new List<List<int>>();
            var chunk = new List<int>();
            var chunkSize = 0L;
            foreach (var mesh in meshes)
            {
                chunkSize += g3d.GetApproxSize(mesh);
                if (chunkSize > ChunkSize && chunk.Count > 0)
                {
                    chunks.Add(chunk);
                    chunk = new List<int>();
                    chunkSize = 0;
                }

                meshChunks.Add(chunks.Count);
                meshIndex.Add(chunk.Count);
                chunk.Add(mesh);
            }
            if (chunk.Count > 0)
            {
                chunks.Add(chunk);
            }

            return new ChunkResult {
                MeshChunks = meshChunks,
                MeshIndex = meshIndex,
                Chunks = chunks
            };
        }
    }
}

public class ChunkResult
{
    public List<int> MeshChunks = new List<int>();
    public List<int> MeshIndex = new List<int>();
    public List<List<int>> Chunks = new List<List<int>>();
}
