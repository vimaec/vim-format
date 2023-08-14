using System.Collections.Generic;
using System.Linq;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim.Format.Geometry
{
    public static class SceneExtensions
    {
        public static IMesh TransformedMesh(this ISceneNode node)
            => node.GetMesh()?.Transform(node.Transform);

        public static IEnumerable<IMesh> TransformedMeshes(this IScene scene)
            => scene.Nodes.Where(n => n.GetMesh() != null).Select(TransformedMesh);

        public static IMesh MergedGeometry(this IScene scene)
            => scene.Nodes.ToEnumerable().MergedGeometry();

        public static IMesh MergedGeometry(this IEnumerable<ISceneNode> nodes)
            => nodes.Where(n => n.GetMesh() != null).Select(TransformedMesh).Merge();
        
        public static IEnumerable<Vector3> AllVertices(this IScene scene)
            => scene.TransformedMeshes().SelectMany(g => g.Vertices.ToEnumerable());

        public static AABox BoundingBox(this IScene scene)
            => AABox.Create(scene.AllVertices());
        
        public static IArray<Vector3> TransformedVertices(this ISceneNode node)
            => node.TransformedMesh()?.Vertices;

        public static AABox TransformedBoundingBox(this ISceneNode node)
            => AABox.Create(node.TransformedVertices()?.ToEnumerable());
    }
}
