using System.Collections.Generic;
using System.Linq;
using Vim.LinqArray;
using Vim.Util;
using Vim.BFast;
using Vim.Math3d;

namespace Vim.Format.Vimx
{
    internal static class G3dVimMeshes
    {
        /// <summary>
        /// Splits a g3dVim into a sequence of vimx getMesh.  
        /// </summary>
        public static IEnumerable<G3dMesh> ToG3dMeshes(this G3dVim g3d)
        {
            var meshInstances = g3d.GetMeshInstances();

            return Enumerable.Range(0, g3d.GetMeshCount())
                .Select(m => g3d.GetMesh(m, meshInstances[m]))
                .WhereNotNull();
        }

        private static List<int>[] GetMeshInstances(this G3dVim g3d)
        {
            var result = new List<int>[g3d.GetMeshCount()];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = new List<int>();
            }

            for (var i = 0; i < g3d.instanceMeshes.Length; i++)
            {
                var mesh = g3d.instanceMeshes[i];
                if (mesh >= 0)
                {
                    result[mesh].Add(i);
                }
            }

            return result;
        }

        private static G3dMesh GetMesh(this G3dVim g3d, int mesh, List<int> instances)
        {
            if (instances.Count == 0)
            {
                return null;
            }
            var instanceNodes = new int[instances.Count];
            var instanceTransforms = new Matrix4x4[instances.Count];
            var instanceFlags = new ushort[instances.Count];

            var submeshIndexOffsets = new List<int>();
            var submeshVertexOffsets = new List<int>();
            var submeshMaterials = new List<int>();
            var indices = new List<int>();
            var vertices = new List<Vector3>();

            // instances
            for (var i = 0; i < instances.Count; i++)
            {
                var instance = instances[i];
                instanceNodes[i] = instances[i];
                instanceTransforms[i] = g3d.instanceTransforms[instance];
                instanceFlags[i] = g3d.instanceFlags[instance];
            }

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

            AppendSubmeshes(
                g3d,
                mesh,
                true,
                submeshIndexOffsets,
                submeshVertexOffsets,
                submeshMaterials,
                indices,
                vertices
            );

            var result = G3dMesh.FromArrays(
                instanceNodes.ToArray(),
                instanceTransforms.ToArray(),
                opaqueCount,
                submeshIndexOffsets.ToArray(),
                submeshVertexOffsets.ToArray(),
                submeshMaterials.ToArray(),
                indices.ToArray(),
                vertices.ToArray()
            );
            return result;
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
                var currentMat = g3d.submeshMaterials[sub];
                var color = currentMat > 0 ? g3d.materialColors[currentMat] : Vector4.One;
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

        private static (List<int>, List<Vector3>) GetSubmesh(this G3dVim g3d, int submesh)
        {
            var indices = new List<int>();
            var vertices = new List<Vector3>();
            var dict = new Dictionary<int, int>();

            var start = g3d.GetSubmeshIndexStart(submesh);
            var end = g3d.GetSubmeshIndexEnd(submesh);

            for (var i = start; i < end; i++)
            {
                var v = g3d.indices[i];
                if (dict.ContainsKey(v))
                {
                    indices.Add(dict[v]);
                }
                else
                {
                    indices.Add(vertices.Count);
                    dict.Add(v, vertices.Count);
                    vertices.Add(g3d.vertices[v]);
                }
            }
            return (indices, vertices);
        }
    }
}