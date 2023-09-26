using System.Collections.Generic;
using Vim.Math3d;

namespace Vim.Format.Vimx
{
    internal static class G3dVimFilter
    {
        /// <summary>
        /// Returns a new G3d which only contains the instances provided as filter.
        /// Delete this eventually if it finds no usage.
        /// Was developped for server side file splitting spike.
        /// </summary>
        public static G3dVim Filter(this G3dVim g3d, int[] instances)
        {
            var vim = new G3dVim();
            var instanceSet = new HashSet<int>(instances);

            // Instances
            var instanceMeshes = new int[instances.Length];
            var instanceFlags = new ushort[instances.Length];
            var instanceTransforms = new Matrix4x4[instances.Length];
            var instance_i = 0;
            for (var i = 0; i < g3d.GetInstanceCount(); i++)
            {
                if (!instanceSet.Contains(i)) continue;
                instanceFlags[instance_i] = g3d.instanceFlags[i];
                instanceMeshes[instance_i] = g3d.instanceMeshes[i];
                instanceTransforms[instance_i] = g3d.instanceTransforms[i];
                instance_i++;
            }

            // Meshes
            var meshMap = new Dictionary<int, int>();
            var meshSet = new HashSet<int>(instanceMeshes);
            meshSet.Remove(-1);
            var meshSubmeshes = new int[meshSet.Count];

            var last = -1;
            var mesh_i = 0;
            for (var i = 0; i < g3d.GetMeshCount(); i++)
            {
                if (!meshSet.Contains(i)) continue;

                var offset = mesh_i > 0 ? meshSubmeshes[mesh_i - 1] : 0;
                var lastCount = last < 0 ? 0 : g3d.GetMeshSubmeshCount(last);
                meshSubmeshes[mesh_i] = lastCount + offset;
                meshMap[i] = mesh_i;
                last = i;
                mesh_i++;
            }

            // Remap Instance Meshes
            for (var i = 0; i < instanceMeshes.Length; i++)
            {
                instanceMeshes[i] = meshMap.TryGetValue(instanceMeshes[i], out var value) ? value : -1;
            }

            // Mesh Attributes Count 
            var submeshCount = 0;
            var positionCount = 0;
            var indiceCount = 0;
            for (var m = 0; m < g3d.GetMeshCount(); m++)
            {
                if (!meshSet.Contains(m)) continue;
                positionCount += g3d.GetMeshVertexCount(m);
                indiceCount += g3d.GetMeshIndexCount(m);
                submeshCount += g3d.GetMeshSubmeshCount(m);
            }

            // Meshes
            var indices_i = 0;
            var positions_i = 0;
            var submesh_i = 0;
            var submeshOffset = 0;
            var meshOffset = 0;
            var submeshIndexOffsets = new int[submeshCount];
            var submeshMaterials = new int[submeshCount];
            var positions = new Vector3[positionCount];
            var indices = new int[indiceCount];

            for (var mesh = 0; mesh < g3d.GetMeshCount(); mesh++)
            {
                if (!meshSet.Contains(mesh)) continue;

                // submeshes
                var subStart = g3d.GetMeshSubmeshStart(mesh);
                var subEnd = g3d.GetMeshSubmeshEnd(mesh);

                for (var j = subStart; j < subEnd; j++)
                {
                    var start = g3d.submeshIndexOffsets[subStart];
                    submeshIndexOffsets[submesh_i] = g3d.submeshIndexOffsets[j] - start + submeshOffset;
                    submeshMaterials[submesh_i] = g3d.submeshMaterials[j];
                    submesh_i++;
                }
                submeshOffset += g3d.GetMeshIndexCount(mesh);

                // indices
                var indexStart = g3d.GetMeshIndexStart(mesh);
                var indexEnd = g3d.GetMeshIndexEnd(mesh);
                for (var j = indexStart; j < indexEnd; j++)
                {
                    indices[indices_i++] = g3d.indices[j] - g3d.GetMeshVertexStart(mesh) + meshOffset;
                }
                meshOffset += g3d.GetMeshVertexCount(mesh);

                // vertices
                var vertexStart = g3d.GetMeshVertexStart(mesh);
                var vertexEnd = g3d.GetMeshVertexEnd(mesh);
                for (var j = vertexStart; j < vertexEnd; j++)
                {
                    positions[positions_i++] = g3d.vertices[j];
                }
            }

            // Material Colors
            var color_i = 0;
            var materialSet = new HashSet<int>(submeshMaterials);
            var materialMap = new Dictionary<int, int>();
            var materialColors = new Vector4[materialSet.Count];

            for (var i = 0; i < g3d.GetMaterialCount(); i++)
            {
                if (materialSet.Contains(i))
                {
                    materialMap[i] = color_i;
                    materialColors[color_i] = g3d.materialColors[i];
                    color_i++;
                }
            }

            // Remap Submesh Materials
            for (var i = 0; i < submeshMaterials.Length; i++)
            {
                submeshMaterials[i] = submeshMaterials[i] < 0 ? -1 : materialMap[submeshMaterials[i]];
            }

            return new G3dVim()
            {
                instanceFlags= instanceFlags,
                instanceMeshes= instanceMeshes,
                instanceTransforms= instanceTransforms,
                meshSubmeshOffsets= meshSubmeshes,
                submeshIndexOffsets= submeshIndexOffsets,
                submeshMaterials= submeshMaterials,
                indices= indices,
                vertices= positions,
                materialColors= materialColors
            };
        }
    }
}