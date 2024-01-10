using System.Linq;
using Vim.LinqArray;
using Vim.BFastNS;
using Vim.G3dNext.Attributes;
using Vim.Format.ObjectModel;
using Vim.G3dNext;
using System.Collections.Generic;
using Vim.Math3d;
using System.Diagnostics;
using System;

namespace Vim.Format.VimxNS.Conversion
{
    public static class VimxConverter
    {
        public static Vimx FromVimPath(string vimPath)
        {
            var sw = Stopwatch.StartNew();
            var vim = VimScene.LoadVim(vimPath, new LoadOptions()
            {
                SkipAssets = true,
                SkipGeometry = true,
            });
            Console.WriteLine("  LoadVim " + sw.ElapsedMilliseconds);

            sw.Restart();
            var g3d = G3dVim.FromVim(vimPath);
            Console.WriteLine("  G3dVim.FromVim " + sw.ElapsedMilliseconds);

            sw.Restart();
            var vimx = FromVim(g3d, vim.DocumentModel);
            Console.WriteLine("  FromVim " + sw.ElapsedMilliseconds);

            return vimx;
        }

        public static Vimx FromVim(G3dVim g3d, DocumentModel bim)
        {
            var sw = Stopwatch.StartNew();
            // Split input Vim into chunks.
            var chunks = CreateChunks(g3d, bim);
            Console.WriteLine("    CreateChunks " + sw.ElapsedMilliseconds);

            sw.Restart();
            // Compute the scene definition from chunks.
            var scene = CreateScene(chunks, bim);
            Console.WriteLine("    CreateScene " + sw.ElapsedMilliseconds);

            sw.Restart();
            // Materials are reused from input g3d.
            var materials = new G3dMaterials().ReadFromVim(g3d);
            Console.WriteLine("    G3dMaterials " + sw.ElapsedMilliseconds);

            var header = VimxHeader.CreateDefault();

            return new Vimx(header, MetaHeader.Default, scene, materials, chunks.Chunks);
        }

        public static VimChunks CreateChunks(G3dVim g3d, DocumentModel bim)
        {
            var sw = Stopwatch.StartNew();
            // First compute a desirable presentation order.
            var ordering = Ordering.ComputeOrder(g3d, bim);
            Console.WriteLine("      ComputeOrder " + sw.ElapsedMilliseconds);

            sw.Restart();
            // Groups meshes up to a certain size.
            var groups = Chunking.ComputeChunks(ordering);
            // Append and merge geometry from g3d to create the chunks.
            Console.WriteLine("      ComputeChunks " + sw.ElapsedMilliseconds);

            sw.Restart();
            var chunks = Chunking.CreateChunks(groups);
            Console.WriteLine("      CreateChunks " + sw.ElapsedMilliseconds);

            return chunks;
        }

        public static G3dScene CreateScene(VimChunks chunks, DocumentModel bim)
        {
            var nodeElements = bim.NodeElementIndex.ToArray();
            
            var instanceCount = chunks.InstanceCount;
            var instanceNodes = new int[instanceCount];
            var instanceMeshes = new int[instanceCount];
            var instanceGroups = new int[instanceCount];
            var instanceTransforms = new Matrix4x4[instanceCount];
            var instanceFlags = new ushort[instanceCount];
            var instanceTags = new long[instanceCount];
            var instanceMins = new Vector3[instanceCount];
            var instanceMaxs = new Vector3[instanceCount];

            var meshCount = chunks.MeshCount;
            var indexCounts = new int[meshCount];
            var vertexCounts = new int[meshCount];
            var opaqueIndexCounts = new int[meshCount];
            var opaqueVertexCounts = new int[meshCount];

            var sw = Stopwatch.StartNew();
            sw.Stop();
            var instance = 0;
            for (var i = 0; i < meshCount; i++)
            {
                var meshChunk = chunks.MeshChunks[i];
                var meshIndex = chunks.MeshIndex[i];
                var instances = chunks.GetInstances(i);
                var chunk = chunks.Chunks[meshChunk];
                for (var j = 0; j < instances.Count; j++)
                {
                    var node = instances[j];
                    var element = nodeElements[node];
                    var transform = chunks.g3d.InstanceTransforms[node];

                    // geometry
                    instanceMeshes[instance] = i;
                    instanceTransforms[instance] = transform;

                    // bounding box
                    sw.Start();
                    var box = chunk.GetAABox(meshIndex, transform);
                    sw.Stop();
                    instanceMins[instance] = box.Min;
                    instanceMaxs[instance] = box.Max;

                    // bim
                    instanceNodes[instance] = node;
                    instanceGroups[instance] = element;
                    instanceTags[instance] = bim.ElementId.SafeGet(element, -1);

                    instance++;
                }

                // geometry counts
                indexCounts[i] = chunk.GetMeshIndexCount(meshIndex, MeshSection.All);
                vertexCounts[i] = chunk.GetMeshVertexCount(meshIndex, MeshSection.All);
                opaqueIndexCounts[i] = chunk.GetMeshIndexCount(meshIndex, MeshSection.Opaque);
                opaqueVertexCounts[i] = chunk.GetMeshVertexCount(meshIndex, MeshSection.Opaque); ;
            }

            // InstanceFlags is not always present. 
            if (chunks.g3d.InstanceFlags != null)
            {
                for(var i = 0; i < instanceNodes.Length; i++)
                {
                    var node = instanceNodes[i];
                    instanceFlags[i] = chunks.g3d.InstanceFlags[node];
                }
            }

            Console.WriteLine("AABB " + sw.ElapsedMilliseconds);

            var scene = new G3dScene()
            {
                ChunkCount = new[] { chunks.ChunkCount},
                InstanceMeshes = instanceMeshes,
                InstanceTransformData = instanceTransforms,
                InstanceNodes = instanceNodes,
                InstanceFlags = instanceFlags,
                InstanceGroups = instanceGroups,
                InstanceMaxs = instanceMaxs,
                InstanceMins = instanceMins,
                InstanceTags = instanceTags,
                MeshChunks = chunks.MeshChunks,
                MeshChunkIndices = chunks.MeshIndex,
                MeshIndexCounts = indexCounts,
                MeshVertexCounts = vertexCounts,
                MeshOpaqueIndexCounts = opaqueIndexCounts,
                MeshOpaqueVertexCounts = opaqueVertexCounts,
            };
            return scene;
        }

    }

    /// <summary>
    /// Initial step of vim->vimx conversion.
    /// </summary>
    public class MeshOrder
    {
        public readonly G3dVim g3d;
        public readonly int[] Meshes;
        public readonly int InstanceCount;

        public MeshOrder(G3dVim g3d, int[] meshes, int instanceCount)
        {
            this.g3d = g3d;
            Meshes = meshes;
            InstanceCount = instanceCount;
        }
    }

    /// <summary>
    /// Describes how the meshes from the vim will be grouped in the vimx.
    /// </summary>
    public class ChunksDescription
    {
        public readonly G3dVim g3d;
        public readonly int[] Meshes;
        public readonly int[] MeshChunks;
        public readonly int[] MeshIndex;
        public readonly List<List<int>> ChunkMeshes;
        public readonly int InstanceCount;

        public ChunksDescription(MeshOrder ordering, int[] meshChunks, int[] meshIndex, List<List<int>> chunkMeshes)
        {
            g3d = ordering.g3d;
            Meshes = ordering.Meshes;
            InstanceCount = ordering.InstanceCount;
            MeshChunks = meshChunks;
            MeshIndex = meshIndex;
            ChunkMeshes = chunkMeshes;
        }
    }

    /// <summary>
    /// Resulting Chunks of the vim->vimx conversion.
    /// </summary>
    public class VimChunks
    {
        public readonly G3dVim g3d;
        public readonly int[] Meshes;
        public readonly int[] MeshChunks;
        public readonly int[] MeshIndex;
        public readonly List<List<int>> ChunkMeshes;
        public readonly G3dChunk[] Chunks;
        public readonly int InstanceCount;

        public VimChunks(ChunksDescription description, G3dChunk[] chunks)
        {
            g3d = description.g3d;
            Meshes = description.Meshes;
            MeshChunks = description.MeshChunks;
            MeshIndex = description.MeshIndex;
            ChunkMeshes = description.ChunkMeshes;
            InstanceCount = description.InstanceCount;
            Chunks = chunks;
        }

        public int ChunkCount => Chunks.Length;

        public int MeshCount => Meshes.Length;

        public IReadOnlyList<int> GetInstances(int meshIndex)
        {
            var m = Meshes[meshIndex];
            return g3d.GetMeshInstances(m);
        }
    }
}
