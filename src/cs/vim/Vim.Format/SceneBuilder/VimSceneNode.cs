using Vim.Format.Geometry;
using Vim.Format.ObjectModel;
using Vim.G3d;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim
{
    public sealed class VimSceneNode : ElementInfo, ISceneNode, ITransformable3D<VimSceneNode>
    {
        public VimSceneNode(VimScene scene, int nodeIndex, int geometryIndex, Matrix4x4 transform)
            : base(scene.DocumentModel, scene.DocumentModel.GetNodeElementIndex(nodeIndex))
        {
            VimIndex = scene.VimIndex;
            _Scene = scene;
            Transform = transform;
            MeshIndex = geometryIndex;
            NodeIndex = nodeIndex;
        }

        public VimScene _Scene { get; }

        public Matrix4x4 Transform { get; }

        public InstanceFlags InstanceFlags
            => (InstanceFlags)_Scene.Document.Geometry.InstanceFlags.ElementAtOrDefault(NodeIndex);

        public bool HideByDefault
            => (InstanceFlags & InstanceFlags.Hidden) == InstanceFlags.Hidden;

        public int VimIndex { get; } = -1;
        public int NodeIndex { get; } = -1;

        public IMesh GetMesh() 
            => _Scene.Meshes.ElementAtOrDefault(MeshIndex);

        public int MeshIndex { get; }

        public bool HasMesh => MeshIndex != -1;

        // TODO: I think this should be "IEnumerable<ISceneNode>" in the interface
        public ISceneNode Parent => null;

        public string DisciplineName => VimSceneHelpers.GetDisiplineFromCategory(CategoryName);

        VimSceneNode ITransformable3D<VimSceneNode>.Transform(Matrix4x4 mat)
            => new VimSceneNode(_Scene, NodeIndex, MeshIndex, mat * Transform);
    }
}
