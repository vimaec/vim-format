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

        public IScene Scene => _Scene;
        public int Id => NodeIndex;
        public Matrix4x4 Transform { get; }

        public bool HideByDefault
        {
            get
            {
                var instanceFlags = (InstanceFlags)_Scene.Document.Geometry.InstanceFlags.ElementAtOrDefault(NodeIndex);
                return (instanceFlags & InstanceFlags.Hidden) == InstanceFlags.Hidden;
            }
        }

        public int VimIndex { get; } = -1;
        public int NodeIndex { get; } = -1;

        public IMesh GetMesh() 
            => _Scene.Meshes.ElementAtOrDefault(MeshIndex);

        public int MeshIndex { get; }

        public bool HasMesh => MeshIndex != -1;

        public Node NodeModel => _Scene.DocumentModel.GetNode(Id);
        public Geometry GeometryModel => _Scene.DocumentModel.GetGeometry(MeshIndex);
        public Vector3 ModelCenter => GeometryModel.Box.Center.Transform(Transform);

        // TODO: I think this should be "IEnumerable<ISceneNode>" in the interface
        public ISceneNode Parent => null;
        public IArray<ISceneNode> Children => LinqArray.LinqArray.Empty<ISceneNode>();

        public string DisciplineName => VimSceneHelpers.GetDisiplineFromCategory(CategoryName);

        VimSceneNode ITransformable3D<VimSceneNode>.Transform(Matrix4x4 mat)
            => new VimSceneNode(_Scene, Id, MeshIndex, mat * Transform);
    }
}
