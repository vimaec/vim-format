using System.Collections.Generic;
using Vim.G3d;
using Vim.Math3d;

namespace Vim.Format.Geometry
{
    /// <summary>
    /// This is the interface for triangle meshes. 
    /// </summary>
    public interface IMesh :
        IGeometryAttributes,
        ITransformable3D<IMesh>
    {
        IList<Vector3> Vertices { get; }
        IList<int> Indices { get; }
        IList<Vector4> VertexColors { get; }
        IList<Vector3> VertexNormals { get; }
        IList<Vector2> VertexUvs { get; }

        IList<int> SubmeshMaterials { get; }
        IList<int> SubmeshIndexOffsets { get; }
        IList<int> SubmeshIndexCount { get; }
    }
}
