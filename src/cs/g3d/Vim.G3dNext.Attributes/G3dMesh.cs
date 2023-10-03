using System;
using Vim.Math3d;

namespace Vim.G3dNext.Attributes
{
    public partial class G3dMesh
    {
        // Field doesn't get written but is useful in builing a Vimx.
        public int[] InstanceNodes;
        public int Chunk;

        void ISetup.Setup()
        {
            // empty
        }

        /// <summary>
        /// The total number of instances.
        /// </summary>
        public int GetInstanceCount() => InstanceTransforms?.Length ?? 0;

        /// <summary>
        /// The total number of submeshes.
        /// </summary>
        public int GetSubmeshCount() => SubmeshIndexOffsets?.Length ?? 0;

        /// <summary>
        /// The total number of submeshes.
        /// </summary>
        public int GetSubmeshCount(MeshSection section)
        {
            var count = GetSubmeshCount();
            if (OpaqueSubmeshCounts == null) return count;
            var opaque = OpaqueSubmeshCounts[0];

            return section == MeshSection.Opaque
                ? opaque
                : count - opaque;
        }

        public int GetIndexStart(MeshSection section)
        {
            if (OpaqueSubmeshCounts == null) return 0;
            if (section == MeshSection.Opaque) return 0;
            var opaque = OpaqueSubmeshCounts[0];
            return GetSubmeshIndexStart(opaque);
        }

        public int GetIndexEnd(MeshSection section)
        {
            if (OpaqueSubmeshCounts == null) return Indices.Length;
            if (section == MeshSection.Transparent) return Indices.Length;
            var opaque = OpaqueSubmeshCounts[0];
            return GetSubmeshIndexEnd(opaque - 1);
        }

        public int GetIndexCount(MeshSection section)
        {
            return GetIndexEnd(section) - GetIndexStart(section);
        }

        /// <summary>
        /// The total number of indices.
        /// </summary>
        public int GetIndexCount() => Indices?.Length ?? 0;

        public int GetVertexStart(MeshSection section)
        {
            if (OpaqueSubmeshCounts == null) return 0;
            if (SubmeshVertexOffsets == null) return 0;
            if (section == MeshSection.Opaque) return 0;
            var opaque = OpaqueSubmeshCounts[0];
            return GetSubmeshVertexStart(opaque);
        }

        public int GetVertexEnd(MeshSection section)
        {
            if (OpaqueSubmeshCounts == null) return Positions.Length;
            if (SubmeshVertexOffsets == null) return Positions.Length;
            if (section == MeshSection.Transparent) return Positions.Length;
            var opaque = OpaqueSubmeshCounts[0];
            return GetSubmeshVertexEnd(opaque - 1);
        }

        public int GetVertexCount(MeshSection section)
        {
            return GetVertexEnd(section) - GetVertexStart(section);
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
            return submesh + 1 < GetSubmeshCount() ? SubmeshIndexOffsets[submesh + 1] : GetIndexCount();
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
