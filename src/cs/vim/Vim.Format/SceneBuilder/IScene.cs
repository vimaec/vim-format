using System.Collections.Generic;
using Vim.G3d;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim.Format.Geometry
{
    /// <summary>
    /// An IScene is a generic representation of a 3D scene graph.
    /// </summary>
    public interface IScene
    {
        VimSceneNode[] Nodes { get; }
        VimMesh[] Meshes { get; }
    }

    /// <summary>
    /// A node in a scene graph. 
    /// </summary>
    public interface ISceneNode
    {
        int Id { get; }
        VimScene Scene { get; }
        Matrix4x4 Transform { get; }
        int MeshIndex { get; }
        VimMesh GetMesh();
        VimSceneNode Parent { get; }

        // TODO: DEPRECATE: this needs to be removed, currently only used in Vim.Max.Bridge.
        VimSceneNode[] Children { get; }
    }

    public class SceneNodeComparer : EqualityComparer<VimSceneNode>, IComparer<VimSceneNode>
    {
        public static readonly SceneNodeComparer Instance = new SceneNodeComparer();

        public int Compare(VimSceneNode x, VimSceneNode y)
            => x.Id - y.Id;
        public override bool Equals(VimSceneNode x, VimSceneNode y)
            => x.Id == y.Id;
        public override int GetHashCode(VimSceneNode obj)
            => obj.Id;
    }

    public class NullNode : ISceneNode
    {
        public static NullNode Instance = new NullNode();
        public static List<ISceneNode> ListInstance = new List<ISceneNode>() { Instance };
        public int Id => -1;
        public VimScene Scene => null;
        public Matrix4x4 Transform => Matrix4x4.Identity;
        public int MeshIndex => 0;
        public VimSceneNode Parent => null;
        public VimSceneNode[] Children => null;
        public VimMesh GetMesh() => null;
    }

    public class IdNode : ISceneNode
    {
        public int Id { get; set; }
        public VimScene Scene => null;
        public Matrix4x4 Transform => Matrix4x4.Identity;
        public int MeshIndex => 0;
        public VimSceneNode Parent => null;
        public VimSceneNode[] Children => null;
        public VimMesh GetMesh() => null;
    }
}
