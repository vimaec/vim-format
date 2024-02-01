using System;
using Vim.Format.ObjectModel;
using Vim.G3d;
using Vim.G3dNext;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim
{
    public class VimShape : ElementInfo
    {
        public readonly VimScene Scene;
        public readonly int ShapeIndex;

        public ShapeNext G3dShape => Scene.Document.Geometry.GetShape(ShapeIndex);
        public ArraySegment<Vector3> Vertices => G3dShape.Vertices;
        public Vector4 Color => G3dShape.Color;
        public float Width => G3dShape.Width;

        public VimShape(VimScene scene, int shapeIndex)
            : base(scene.DocumentModel, scene.DocumentModel.GetShapeElementIndex(shapeIndex))
        {
            Scene = scene;
            ShapeIndex = shapeIndex;
        }
    }
}
