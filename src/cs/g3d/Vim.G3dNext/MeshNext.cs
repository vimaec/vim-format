using System;
using System.Linq;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim.G3dNext
{
    /// <summary>
    /// A G3dMesh is a section of the G3D data that defines a mesh.
    /// This does not implement IGeometryAttributes for performance reasons. 
    /// </summary>
    public class MeshNext 
    {
        public readonly G3dVim G3D;
        public readonly int Index;

        public ArraySegment<Vector3> Vertices { get; }
        public int[] Indices { get; }

        public int[] SubmeshIndexOffsets { get; }
        public ArraySegment<int> SubmeshMaterials { get; }

        public MeshNext(G3dVim parent, int index)
        {
            (G3D, Index) = (parent, index);

            var vertexStart = G3D.GetMeshVertexStart(index);
            Vertices = new ArraySegment<Vector3>(G3D.Positions, vertexStart, G3D.GetMeshVertexCount(index));

            var indexStart = G3D.GetMeshIndexStart(index);
            Indices = new ArraySegment<int>(G3D.Indices, indexStart, G3D.GetMeshIndexCount(index))
                .Select(i => i + vertexStart)
                .ToArray();

            var submeshStart = G3D.GetMeshSubmeshStart(index);
            var submeshCount = G3D.GetMeshSubmeshCount(index);
            SubmeshIndexOffsets = new ArraySegment<int>(G3D.SubmeshIndexOffsets, submeshStart, submeshCount)
                .Select(i => i - indexStart)
                .ToArray();

            SubmeshMaterials = new ArraySegment<int>(G3D.SubmeshMaterials, submeshStart, submeshCount);
        }
    }
}
