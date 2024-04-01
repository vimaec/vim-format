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

        IArray<int> SubmeshMaterials { get; }
        IArray<int> SubmeshIndexOffsets { get; }
        IArray<int> SubmeshIndexCount { get; }
    }

    public interface IMeshCommon : ITransformable3D<IMeshCommon>
    {
        IArray<Vector3> Vertices { get; }
        IArray<int> Indices { get; }

        IArray<int> SubmeshMaterials { get; }
        IArray<int> SubmeshIndexOffsets { get; }
        IArray<int> SubmeshIndexCount { get; }
        int NumCornersPerFace { get; }
        int NumVertices { get; }
        int NumCorners { get; }
        int NumFaces { get; }
        int NumInstances { get; }
        int NumMeshes { get; }
        int NumShapeVertices { get; }
        int NumShapes { get; }
    }
}
