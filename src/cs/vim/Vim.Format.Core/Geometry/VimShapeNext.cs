using System;
using System.Collections.Generic;
using Vim.G3d;
using Vim.Math3d;

namespace Vim.Format.Geometry
{
    public class VimShapeNext
    {
        public readonly G3dVim g3d;
        public readonly int Index;
        public readonly ArraySegment<Vector3> Vertices;

        public Vector4 Color => g3d.ShapeColors[Index];
        public float Width => g3d.ShapeWidths[Index];

        public static IEnumerable<VimShapeNext> FromG3d(G3dVim g3d)
        {
            for(var i =0; i < g3d.GetShapeCount(); i++)
            {
                yield return new VimShapeNext(g3d, i);
            }
        }

        public VimShapeNext(G3dVim g3d, int index)
        {
            var start = g3d.GetShapeVertexStart(index);
            var count = g3d.GetShapeVertexCount(index);

            Vertices = new ArraySegment<Vector3>(g3d.ShapeVertices, start, count);
        }
    }
}
