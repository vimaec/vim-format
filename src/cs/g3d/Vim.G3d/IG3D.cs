/*
    G3D Geometry Format Library
    Copyright 2019, VIMaec LLC.
    Copyright 2018, Ara 3D Inc.
    Usage licensed under terms of MIT License
*/

using System.Collections.Generic;
using Vim.G3d;
using Vim.LinqArray;
using Vim.Math3d;
using Vim.G3dNext;

namespace Vim.G3d
{
    public interface IG3D
    {
        IArray<int> FaceMaterials { get; }
        G3dHeader Header { get; }
        IArray<int> Indices { get; }
        IArray<ushort> InstanceFlags { get; }
        IArray<int> InstanceMeshes { get; }
        IArray<int> InstanceParents { get; }
        IArray<Matrix4x4> InstanceTransforms { get; }
        IArray<Vector4> MaterialColors { get; }
        IArray<float> MaterialGlossiness { get; }
        IArray<G3dMaterial> Materials { get; }
        IArray<float> MaterialSmoothness { get; }
        IArray<G3dMesh> Meshes { get; }
        IArray<int> MeshIndexCounts { get; }
        IArray<int> MeshIndexOffsets { get; }
        IArray<int> MeshSubmeshCount { get; }
        IArray<int> MeshSubmeshOffset { get; }
        IArray<int> MeshVertexCounts { get; }
        IArray<int> MeshVertexOffsets { get; }
        IArray<Vector4> ShapeColors { get; }
        IArray<G3dShape> Shapes { get; }
        IArray<int> ShapeVertexCounts { get; }
        IArray<int> ShapeVertexOffsets { get; }
        IArray<Vector3> ShapeVertices { get; }
        IArray<float> ShapeWidths { get; }
        IArray<int> SubmeshIndexCount { get; }
        IArray<int> SubmeshIndexOffsets { get; }
        IArray<int> SubmeshMaterials { get; }
        IArray<Vector4> VertexColors { get; }
        IArray<Vector3> Vertices { get; }
    }
}

public class G3DAdapter : IG3D
{
    public G3dVim g3d;

    public IArray<Vector3> Vertices => g3d.Positions.ToIArray();

    public IArray<int> Indices => g3d.Indices.ToIArray();

    public IArray<int> FaceMaterials => throw new System.NotImplementedException();

    public G3dHeader Header => throw new System.NotImplementedException();



    public IArray<ushort> InstanceFlags => throw new System.NotImplementedException();

    public IArray<int> InstanceMeshes => throw new System.NotImplementedException();

    public IArray<int> InstanceParents => throw new System.NotImplementedException();

    public IArray<Matrix4x4> InstanceTransforms => throw new System.NotImplementedException();

    public IArray<Vector4> MaterialColors => throw new System.NotImplementedException();

    public IArray<float> MaterialGlossiness => throw new System.NotImplementedException();

    public IArray<G3dMaterial> Materials => throw new System.NotImplementedException();

    public IArray<float> MaterialSmoothness => throw new System.NotImplementedException();

    public IArray<G3dMesh> Meshes => throw new System.NotImplementedException();

    public IArray<int> MeshIndexCounts => throw new System.NotImplementedException();

    public IArray<int> MeshIndexOffsets => throw new System.NotImplementedException();

    public IArray<int> MeshSubmeshCount => throw new System.NotImplementedException();

    public IArray<int> MeshSubmeshOffset => throw new System.NotImplementedException();

    public IArray<int> MeshVertexCounts => throw new System.NotImplementedException();

    public IArray<int> MeshVertexOffsets => throw new System.NotImplementedException();

    public IArray<Vector4> ShapeColors => throw new System.NotImplementedException();

    public IArray<G3dShape> Shapes => throw new System.NotImplementedException();

    public IArray<int> ShapeVertexCounts => throw new System.NotImplementedException();

    public IArray<int> ShapeVertexOffsets => throw new System.NotImplementedException();

    public IArray<Vector3> ShapeVertices => throw new System.NotImplementedException();

    public IArray<float> ShapeWidths => throw new System.NotImplementedException();

    public IArray<int> SubmeshIndexCount => throw new System.NotImplementedException();

    public IArray<int> SubmeshIndexOffsets => throw new System.NotImplementedException();

    public IArray<int> SubmeshMaterials => throw new System.NotImplementedException();

    public IArray<Vector4> VertexColors => throw new System.NotImplementedException();



    public Vector3 ComputeFaceNormal(int nFace) => throw new System.NotImplementedException();
}
