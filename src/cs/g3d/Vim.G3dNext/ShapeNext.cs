using System;
using Vim.Math3d;

namespace Vim.G3dNext
{
    public class ShapeNext
    {
        public readonly G3dVim G3D;
        public readonly int Index;
        public readonly ArraySegment<Vector3> Vertices;
        public Vector4 Color => G3D.ShapeColors[Index];
        public float Width => G3D.ShapeWidths[Index];

        public ShapeNext(G3dVim parent, int index)
        {
            (G3D, Index) = (parent, index);
            var start = G3D.GetShapeVertexStart(index);
            var end = G3D.GetShapeVertexEnd(index);
            Vertices = new ArraySegment<Vector3>(G3D.ShapeVertices, start, end - start);
        }
    }
}
