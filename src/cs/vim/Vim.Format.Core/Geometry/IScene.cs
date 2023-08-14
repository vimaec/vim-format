using System.Collections.Generic;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim.Format.Geometry
{
    /// <summary>
    /// An IScene is a generic representation of a 3D scene graph.
    /// </summary>
    public interface IScene
    {
        IArray<ISceneNode> Nodes { get; }
        IArray<IMesh> Meshes { get; }
    }

    /// <summary>
    /// A node in a scene graph. 
    /// </summary>
    public interface ISceneNode
    {
        int Id { get; }
        IScene Scene { get; }
        Matrix4x4 Transform { get; }
        int MeshIndex { get; }
        IMesh GetMesh();
        ISceneNode Parent { get; }

        // TODO: DEPRECATE: this needs to be removed, currently only used in Vim.Max.Bridge.
        IArray<ISceneNode> Children { get; }
    }

    public class NullNode : ISceneNode
    {
        public static NullNode Instance = new NullNode();
        public static List<ISceneNode> ListInstance = new List<ISceneNode>() { Instance };
        public int Id => -1;
        public IScene Scene => null;
        public Matrix4x4 Transform => Matrix4x4.Identity;
        public int MeshIndex => 0;
        public ISceneNode Parent => null;
        public IArray<ISceneNode> Children => null;
        public IMesh GetMesh() => null;
    }
}
