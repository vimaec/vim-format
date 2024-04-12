using System;
using System.Collections.Generic;
using Vim.BFastLib;

namespace Vim.G3dNext
{
    [Flags]
    public enum InstanceFlags
    {
        /// <summary>
        /// Default - no instance options defined.
        /// </summary>
        None = 0,

        /// <summary>
        /// When enabled, indicates that the renderer (or the consuming application) should hide
        /// the instance by default.
        /// </summary>
        Hidden = 1,
    }

    public partial class G3dVim
    {

        // Computed field
        public int[] MeshVertexOffsets;
        private List<int>[] _meshInstances;

        public IReadOnlyList<int> GetMeshInstances(int mesh)
        {
            return _meshInstances[mesh];
        }

        public int GetApproxSize(int mesh)
        {
            return GetMeshVertexCount(mesh) * 12 + GetMeshIndexCount(mesh) * 4;
        }

        public bool InstanceHasFlag(int index, InstanceFlags flag)
        {
            return InstanceFlags[index] == (int)flag;
        }

        void ISetup.Setup()
        {
            MeshVertexOffsets = ComputeMeshVertexOffsets();
            _meshInstances = ComputeMeshInstances();
        }

        public static G3dVim FromVim(string vimPath)
            => BFastHelpers.Read(vimPath, b => new G3dVim(b.GetBFast("geometry")));

        private int[] ComputeMeshVertexOffsets()
        {
            var result = new int[GetMeshCount()];
            for (var m = 0; m < result.Length; m++)
            {
                var min = int.MaxValue;
                var start = GetMeshIndexStart(m);
                var end = GetMeshIndexEnd(m);
                for (var i = start; i < end; i++)
                {
                    min = Math.Min(min, Indices[i]);
                }
                result[m] = min;
            }
            return result;
        }

        private List<int>[] ComputeMeshInstances()
        {
            var result = new List<int>[GetMeshCount()];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = new List<int>();
            }

            for (var i = 0; i < InstanceMeshes.Length; i++)
            {
                var mesh = InstanceMeshes[i];
                if (mesh >= 0)
                {
                    result[mesh].Add(i);
                }
            }

            return result;
        }

        public int GetTriangleCount()
        {
            return GetIndexCount() / 3;
        }

        /// <summary>
        /// The total number of instances.
        /// </summary>
        public int GetInstanceCount() => InstanceTransforms?.Length ?? 0;

        #region meshes
        /// <summary>
        /// The total number of meshes.
        /// </summary>
        public int GetMeshCount() => MeshSubmeshOffsets?.Length ?? 0;

        public int GetMeshIndexStart(int mesh)
        {
            var submesh = GetMeshSubmeshStart(mesh);
            return GetSubmeshIndexStart(submesh);
        }

        public int GetMeshIndexEnd(int mesh)
        {
            var submesh = GetMeshSubmeshEnd(mesh) - 1;
            return GetSubmeshIndexEnd(submesh);
        }

        public int GetMeshIndexCount(int mesh)
        {
            return GetMeshIndexEnd(mesh) - GetMeshIndexStart(mesh);
        }

        public int GetMeshVertexStart(int mesh)
        {
            return MeshVertexOffsets[mesh];
        }

        public int GetMeshVertexEnd(int mesh)
        {
            return mesh + 1 < GetMeshCount() ? MeshVertexOffsets[mesh + 1] : Positions.Length;
        }

        public int GetMeshVertexCount(int mesh)
        {
            return GetMeshVertexEnd(mesh) - GetMeshVertexStart(mesh);
        }

        public int GetMeshSubmeshStart(int mesh)
        {
            return MeshSubmeshOffsets[mesh];
        }

        public int GetMeshSubmeshEnd(int mesh)
        {
            return mesh + 1 < GetMeshCount()
                ? MeshSubmeshOffsets[mesh + 1]
                : GetSubmeshCount();
        }

        public int GetMeshSubmeshCount(int mesh)
        {
            return GetMeshSubmeshEnd(mesh) - GetMeshSubmeshStart(mesh);
        }

        #endregion

        #region submesh

        /// <summary>
        /// The total number of submeshes.
        /// </summary>
        public int GetSubmeshCount() => SubmeshIndexOffsets?.Length ?? 0;

        public int GetSubmeshIndexStart(int submesh)
        {
            return SubmeshIndexOffsets[submesh];
        }

        public int GetSubmeshIndexEnd(int submesh)
        {
            return submesh + 1 < GetSubmeshCount() ? SubmeshIndexOffsets[submesh + 1] : GetIndexCount();
        }

        public int GetSubmeshIndexCount(int submesh)
        {
            return GetSubmeshIndexEnd(submesh) - GetSubmeshIndexStart(submesh);
        }

        #endregion

        /// <summary>
        /// The total number of indices.
        /// </summary>
        public int GetIndexCount() => Indices?.Length ?? 0;

        /// <summary>
        /// The total number of vertices.
        /// </summary>
        public int GetVertexCount() => Positions?.Length ?? 0;

        /// <summary>
        /// The total number of materials.
        /// </summary>
        public int GetMaterialCount() => MaterialColors?.Length ?? 0;

        /// <summary>
        /// The total number of shapes.
        /// </summary>
        public int GetShapeCount() => ShapeVertexOffsets?.Length ?? 0;


        public int GetShapeVertexStart(int index)
        {
            return ShapeVertexOffsets[index];
        }
        public int GetShapeVertexEnd(int index)
        {
            if (index + 1 >= ShapeVertexOffsets.Length)
            {
                return ShapeVertices.Length;
            }
            return ShapeVertexOffsets[index + 1];
        }
        /// <summary>
        /// The total number of shape vertices.
        /// </summary>
        public int GetShapeVertexCount(int index)
        {
            return GetShapeVertexEnd(index) - GetShapeVertexStart(index);
        }
    }
}
