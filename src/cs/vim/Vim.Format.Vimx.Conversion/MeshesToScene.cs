using System.Collections.Generic;
using System.Linq;
using Vim.BFast;
using Vim.Format.ObjectModel;
using Vim.G3dNext.Attributes;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim.Format.VimxNS.Conversion
{
    public static class MeshesToScene
    {
        public static G3dScene CreateScene(G3dVim g3d, DocumentModel bim, VimxChunk[] chunks, G3dMesh[] meshes)
        {
            var scene = new G3dScene();
            scene.ChunkCount = new int[1] { chunks.Length };

            var nodeElements = bim.NodeElementIndex.ToArray();
            var nodeElementIds = nodeElements
                .Select(n => n < 0 || n > bim.ElementId.Count
                    ? -1
                    : bim.ElementId[n]
                ).ToArray();

            (scene.InstanceMeshes, scene.InstanceTransforms, scene.InstanceNodes) = meshes.GetInstanceIds();
            scene.InstanceGroups = scene.InstanceNodes.Select(n => nodeElements[n]).ToArray();
            scene.InstanceTags = scene.InstanceNodes.Select(n => nodeElementIds[n]).ToArray();
            (scene.InstanceMins, scene.InstanceMaxs) = ComputeBoundingBoxes(meshes, scene.InstanceMeshes, scene.InstanceTransforms);
            scene.InstanceFlags = scene.InstanceNodes.Select(i => g3d.InstanceFlags[i]).ToArray();

            // meshes
            scene.MeshChunks = new int[meshes.Length];
            scene.MeshChunkIndices = new int[meshes.Length];
            scene.MeshInstanceCounts = new int[meshes.Length];
            scene.MeshIndexCounts = new int[meshes.Length];
            scene.MeshVertexCounts = new int[meshes.Length];
            scene.MeshOpaqueIndexCounts = new int[meshes.Length];
            scene.MeshOpaqueVertexCounts = new int[meshes.Length];

            for (var i = 0; i < meshes.Length; i++)
            {
                var mesh = meshes[i];
                scene.MeshChunks[i] = mesh.Chunk;
                scene.MeshChunkIndices[i] = mesh.ChunkIndex;
                scene.MeshInstanceCounts[i] = mesh.GetInstanceCount();
                scene.MeshIndexCounts[i] = mesh.GetIndexCount();
                scene.MeshVertexCounts[i] = mesh.GetVertexCount();
                scene.MeshOpaqueIndexCounts[i] = mesh.GetIndexCount(MeshSection.Opaque);
                scene.MeshOpaqueVertexCounts[i] = mesh.GetVertexCount(MeshSection.Opaque);
            }
            return scene;
        }

        private static (Vector3[] min, Vector3[] max) ComputeBoundingBoxes(G3dMesh[] meshes, int[] instanceFiles, int[] instanceIndices)
        {
            var instanceMins = new Vector3[instanceFiles.Length];
            var instanceMaxs = new Vector3[instanceFiles.Length];
            for (var i = 0; i < instanceFiles.Length; i++)
            {
                var file = instanceFiles[i];
                var index = instanceIndices[i];

                var min = Vector3.MaxValue;
                var max = Vector3.MinValue;
                var mesh = meshes[file];
                var vertexCount = mesh.GetVertexCount();
                for (var j = 0; j < vertexCount; j++)
                {
                    var pos = mesh.Positions[j];
                    var matrix = mesh.InstanceTransforms[index];
                    var pos2 = pos.Transform(matrix);
                    min = min.Min(pos2);
                    max = max.Max(pos2);
                }
                instanceMins[i] = min;
                instanceMaxs[i] = max;
            }
            return (instanceMins, instanceMaxs);
        }

        private static (int[], int[], int[]) GetInstanceIds(this G3dMesh[] meshes)
        {
            var instanceMeshes = new List<int>();
            var instanceIndices = new List<int>();
            var instanceNodes = new List<int>();

            // Associate intances to meshes
            for (var i = 0; i < meshes.Length; i++)
            {
                var mesh = meshes[i];
                for (var j = 0; j < mesh.InstanceNodes.Length; j++)
                {
                    instanceMeshes.Add(i);
                    instanceIndices.Add(j);
                    instanceNodes.Add(mesh.InstanceNodes[j]);
                }
            }
            return (instanceMeshes.ToArray(), instanceIndices.ToArray(), instanceNodes.ToArray());
        }
    }
}
