using System;
using Vim.Math3d;

namespace Vim.G3dNext
{
    public partial class G3dChunk
    {
        void ISetup.Setup()
        {
            // empty
        }

        /// <summary>
        /// The total number of instances.
        /// </summary>

        public int GetSubmeshCount() => SubmeshIndexOffsets?.Length ?? 0;


        public int getMeshCount() => MeshSubmeshOffset?.Length ?? 0;

        /// <summary>
        /// The total number of submeshes.
        /// </summary>
        public int GetSubmeshCount(int mesh, MeshSection section) =>
            GetMeshSubmeshEnd(mesh, section) - GetMeshSubmeshStart(mesh, section);

        public int GetMeshSubmeshStart(int mesh, MeshSection section)
        {
            if (section == MeshSection.Opaque || section == MeshSection.All)
            {
                return MeshSubmeshOffset[mesh];
            }

            return MeshSubmeshOffset[mesh] + MeshOpaqueSubmeshCounts[mesh];
        }

        public int GetMeshSubmeshEnd(int mesh, MeshSection section)
        {
            if (section == MeshSection.Opaque)
            {
                return MeshSubmeshOffset[mesh] + MeshOpaqueSubmeshCounts[mesh];
            }
            if(mesh + 1 >= MeshSubmeshOffset.Length)
            {
                return SubmeshIndexOffsets.Length;
            }
            return MeshSubmeshOffset[mesh + 1];
        }

        public int GetMeshIndexStart(int mesh, MeshSection section)
        {
            var sub = GetMeshSubmeshStart(mesh, section);
            return GetSubmeshIndexStart(sub);
        }

        public int GetMeshIndexEnd(int mesh, MeshSection section)
        {
            var sub = GetMeshSubmeshEnd(mesh, section);
            return GetSubmeshIndexEnd(sub);
        }

        public int GetMeshIndexCount(int mesh, MeshSection section)
        {
            return GetMeshIndexEnd(mesh, section) - GetMeshIndexStart(mesh, section);
        }

        public AABox GetAABox(int mesh, Matrix4x4 matrix)
        {
            var start = GetMeshVertexStart(mesh, MeshSection.All);
            var end = GetMeshVertexEnd(mesh, MeshSection.All);
            var min = Positions[start].Transform(matrix);
            var max = min;
            for (var v = start + 1; v < end; v++)
            {
                var pos = Positions[v].Transform(matrix);
                min = min.Min(pos);
                max = max.Max(pos);
            }
            return new AABox(min, max);
        }

        /// <summary>
        /// The total number of indices.
        /// </summary>
        public int GetIndexCount() => Indices?.Length ?? 0;

        public int GetMeshVertexStart(int mesh, MeshSection section)
        {
            var sub = GetMeshSubmeshStart(mesh, section);
            return GetSubmeshVertexStart(sub);
        }

        public int GetMeshVertexEnd(int mesh, MeshSection section)
        {
            var sub = GetMeshSubmeshEnd(mesh, section);
            return GetSubmeshVertexEnd(sub);
        }

        public int GetMeshVertexCount(int mesh, MeshSection section)
        {
            return GetMeshVertexEnd(mesh, section) - GetMeshVertexStart(mesh, section);
        }

        /// <summary>
        /// The total number of vertices.
        /// </summary>
        public int GetVertexCount() => (Positions?.Length ?? 0);

        public int GetSubmeshIndexStart(int submesh)
        {
            return SubmeshIndexOffsets[submesh];
        }

        public int GetSubmeshIndexEnd(int submesh)
        {
            return submesh + 1 < GetSubmeshCount()
                ? SubmeshIndexOffsets[submesh + 1]
                : GetIndexCount();
        }

        public int GetSubmeshIndexCount(int submesh)
        {
            return GetSubmeshIndexEnd(submesh) - GetSubmeshIndexStart(submesh);
        }

        public int GetSubmeshVertexStart(int submesh)
        {
            return SubmeshVertexOffsets[submesh];
        }

        public int GetSubmeshVertexEnd(int submesh)
        {
            return submesh + 1 < GetSubmeshCount() ? SubmeshVertexOffsets[submesh + 1] : GetVertexCount();
        }

        public int GetSubmeshVertexCount(int submesh)
        {
            return GetSubmeshVertexEnd(submesh) - GetSubmeshVertexStart(submesh);
        }

        public AABox GetAABB()
        {
            var box = new AABox(Positions[0], Positions[0]);
            for (var p = 1; p < Positions.Length; p++)
            {
                var pos = Positions[p];
                box = Expand(box, pos);
            }
            return box;
        }

        static AABox Expand(AABox box, Vector3 pos)
        {
            return new AABox(
                new Vector3(
                    Math.Min(box.Min.X, pos.X),
                    Math.Min(box.Min.Y, pos.Y),
                    Math.Min(box.Min.Z, pos.Z)
                ),
                new Vector3(
                    Math.Max(box.Max.X, pos.X),
                    Math.Max(box.Max.Y, pos.Y),
                    Math.Max(box.Max.Z, pos.Z)
                )
            );
        }
    }
}
