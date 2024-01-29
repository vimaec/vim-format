using Vim.G3d;
using Vim.LinqArray;
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
        IArray<Vector3> Vertices { get; }
        IArray<int> Indices { get; }
        IArray<Vector4> VertexColors { get; }
        IArray<Vector3> VertexNormals { get; }

        IArray<int> SubmeshMaterials { get; }
        IArray<int> SubmeshIndexOffsets { get; }
        IArray<int> SubmeshIndexCount { get; }
    }
}
