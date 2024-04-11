using System;
using System.Collections.Generic;
using System.Linq;
using Vim.Format.Geometry;
using Vim.Format.ObjectModel;
using Vim.G3d;
using Vim.G3dNext;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim
{
    public sealed class VimSceneNode : ElementInfo, ITransformable3D<VimSceneNode>
    {
        public VimSceneNode(VimScene scene, int nodeIndex, int geometryIndex, Matrix4x4 transform)
            : base(scene.DocumentModel, scene.DocumentModel.GetNodeElementIndex(nodeIndex))
        {
            VimIndex = scene.VimIndex;
            Scene = scene;
            Transform = transform;
            MeshIndex = geometryIndex;
            NodeIndex = nodeIndex;
        }

        public VimScene Scene { get; }

        public int Id => NodeIndex;
        public Matrix4x4 Transform { get; }

        public bool HideByDefault
        {
            get
            {
                var instanceFlags = (InstanceFlags)Scene.Document.Geometry.InstanceFlags.ElementAtOrDefault(NodeIndex);
                return (instanceFlags & InstanceFlags.Hidden) == InstanceFlags.Hidden;
            }
        }

        public int VimIndex { get; } = -1;
        public int NodeIndex { get; } = -1;

        public VimMesh GetMesh()
            => Scene.Meshes.SafeGet(MeshIndex);

        public int MeshIndex { get; }

        public bool HasMesh => MeshIndex != -1;

        public Node NodeModel => Scene.DocumentModel.GetNode(Id);
        public Geometry GeometryModel => Scene.DocumentModel.GetGeometry(MeshIndex);

        // TODO: I think this should be "IEnumerable<ISceneNode>" in the interface
        public VimSceneNode Parent => null;
        public VimSceneNode[] Children =>  Array.Empty<VimSceneNode>();

        public string DisciplineName => VimSceneHelpers.GetDisiplineFromCategory(CategoryName);

        VimSceneNode ITransformable3D<VimSceneNode>.Transform(Matrix4x4 mat)
            => new VimSceneNode(Scene, Id, MeshIndex, mat * Transform);

        public IMeshCommon TransformedMesh()
             => GetMesh()?.Transform(Transform);

        public IArray<Vector3> TransformedVertices()
            => TransformedMesh()?.Vertices;

        public AABox TransformedBoundingBox()
            => AABox.Create(TransformedVertices()?.ToEnumerable());
    }

    public static class NodeExtensions
    {
        public static IMeshCommon MergedGeometry(this IEnumerable<VimSceneNode> nodes)
           => nodes.Where(n => n.GetMesh() != null).Select(n => n.TransformedMesh()).ToArray().Merge();
    }
}
