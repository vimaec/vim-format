using System;
using System.Collections.Generic;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim.G3d
{
    
    public interface IMeshCommon : ITransformable3D<IMeshCommon>
    {
        IMeshCommon Clone();
        void SetVertices(Vector3[] vertices);
        void SetIndices(int[] indices);

        // To be compatible with IMesh
        IArray<Vector3> Vertices { get; }
        IArray<int> Indices { get; }

        IArray<int> SubmeshMaterials { get; }
        IArray<int> SubmeshIndexOffsets { get; }
        IArray<int> SubmeshIndexCounts { get; }

        

        // To be compatible with IGeometryAttributes
        int NumCornersPerFace { get; }
        int NumVertices { get; }
        int NumCorners { get; }
        int NumFaces { get; }
        int NumInstances { get; }
        int NumMeshes { get; }
        int NumShapeVertices { get; }
        int NumShapes { get; }
    }

    /// <summary>
    /// A G3dMesh is a section of the G3D data that defines a mesh.
    /// This does not implement IGeometryAttributes for performance reasons. 
    /// </summary>
    public class G3dMesh : IMeshCommon
    {
        public G3D G3D { get; }
        public int Index { get; }

        public int VertexOffset => G3D.MeshVertexOffsets[Index];
        public int NumVertices => G3D.MeshVertexCounts[Index];
        public int IndexOffset => G3D.MeshIndexOffsets[Index];
        public int NumCorners => G3D.MeshIndexCounts[Index];
        public int FaceOffset => IndexOffset / NumCornersPerFace;
        public int NumFaces => NumCorners / NumCornersPerFace;
        public int NumCornersPerFace => G3D.NumCornersPerFace;


        // Fillers to fullfil IGeometryAttributes for now
        public int NumInstances => 0;
        public int NumMeshes => 1;
        public int NumShapeVertices => 0;
        public int NumShapes => 0;

        // Data
        public IArray<Vector3> Vertices { get; private set; }
        public IArray<int> Indices { get; private set; }
        public IArray<int> SubmeshMaterials { get; private set; }
        public IArray<int> SubmeshIndexOffsets { get; private set; }
        public IArray<int> SubmeshIndexCounts { get; private set; }


        public G3dMesh(G3D parent, int index)
        {
            (G3D, Index) = (parent, index);
            Vertices = G3D.Vertices?.SubArray(VertexOffset, NumVertices);
            var offset = VertexOffset;
            Indices = G3D.Indices?.SubArray(IndexOffset, NumCorners).Select(i => i - offset);
          
            // TODO: Remove need for this.
            var submeshArray = (G3D.SubmeshIndexOffsets as ArrayAdapter<int>).Array;
            var submeshIndex = Array.BinarySearch(submeshArray, IndexOffset);
            var submeshCount = 0;
            for(var i = submeshIndex; i < submeshArray.Length; i++)
            {
                var indexOffset = submeshArray[i];
                if (indexOffset - IndexOffset >= NumCorners)
                    break;
                submeshCount++;
            }
            SubmeshMaterials = G3D.SubmeshMaterials?.SubArray(submeshIndex, submeshCount).Evaluate();
            SubmeshIndexOffsets = G3D.SubmeshIndexOffsets?.SubArray(submeshIndex, submeshCount).Select(i => i - IndexOffset).Evaluate();
            
            var last = NumCorners - SubmeshIndexOffsets.Last();
            SubmeshIndexCounts = SubmeshIndexOffsets.AdjacentDifferences().Append(last).Evaluate();
        }

        public IMeshCommon Transform(Matrix4x4 mat)
        {
            var mesh = Clone();
            mesh.Vertices = mesh.Vertices.Select(v => v.Transform(mat)).Evaluate();
            return mesh;
        }

        public G3dMesh Clone()
        {
            var mesh = new G3dMesh(G3D, Index);
            mesh.Vertices = mesh.Vertices;
            mesh.Indices = mesh.Indices;
            mesh.SubmeshMaterials = mesh.SubmeshMaterials;
            mesh.SubmeshIndexOffsets = mesh.SubmeshIndexOffsets;
            mesh.SubmeshIndexCounts = mesh.SubmeshIndexCounts;
            return mesh;
        }
        IMeshCommon IMeshCommon.Clone() => Clone();
        public void SetVertices(Vector3[] vertices)
        {
            Vertices = vertices.ToIArray();
        }
        public void SetIndices(int[] indices)
        {
            Indices = indices.ToIArray();
        }
    }
}
