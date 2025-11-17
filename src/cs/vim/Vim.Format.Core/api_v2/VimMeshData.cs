using System;
using System.Linq;
using Vim.Math3d;
using Vim.Util;

namespace Vim.Format.api_v2
{
    /// <summary>
    /// An interface for simple tri-meshes with vertices and indices.
    /// </summary>
    public interface IVimMesh
    {
        Vector3[] GetVertices();
        int[] GetIndices();
        int FaceCount { get; }
    }

    public struct VimMeshData : IVimMesh
    {
        public readonly Vector3[] Vertices;
        public readonly int[] Indices;
        public const int IndexCountPerFace = 3; // VimMeshData defines meshes composed of triangles (i.e. 3 vertex indices per face)
        public int FaceCount => Indices.Length / IndexCountPerFace;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="vertices"></param>
        /// <param name="indices"></param>
        public VimMeshData(Vector3[] vertices, int[] indices)
        {
            Vertices = vertices;
            Indices = indices;
        }

        public Vector3[] GetVertices() => Vertices;
        public int[] GetIndices() => Indices;
    }

    /// <summary>
    /// A struct representing a view of triangular mesh data contained in VimGeometryData composed
    /// of submeshes each having their own materials.
    /// </summary>
    public struct VimMeshView : IVimMesh
    {
        public VimGeometryData VimGeometryData { get; }
        public int MeshIndex { get; }
        public int VertexOffset => VimGeometryData.MeshVertexOffsets[MeshIndex];
        public int VertexCount => VimGeometryData.MeshVertexCounts[MeshIndex];
        public int IndexOffset => VimGeometryData.MeshIndexOffsets[MeshIndex];
        public int IndexCount => VimGeometryData.MeshIndexCounts[MeshIndex];
        public int FaceCount => IndexCount / VimMeshData.IndexCountPerFace;

        /// <summary>
        /// Constructor.
        /// </summary>
        public VimMeshView(VimGeometryData vimGeometryData, int meshIndex)
        {
            VimGeometryData = vimGeometryData;
            MeshIndex = meshIndex;
            _submeshIndex = null; // lazily instantiated
            _submeshCount = null; // lazily instantiated
        }

        public Vector3[] GetVertices()
        {
            return GetSubArray(VimGeometryData.Vertices, VertexOffset, VertexCount);
        }

        public int[] GetIndices()
        {
            var vertexOffset = VertexOffset;
            return GetSubArray(VimGeometryData.Indices, IndexOffset, IndexCount)
                .Select(i => i - vertexOffset).ToArray();
        }

        private int? _submeshIndex;
        public int SubmeshIndex
        {
            get
            {
                if (!_submeshIndex.HasValue)
                {
                    _submeshIndex = Array.BinarySearch(VimGeometryData.SubmeshIndexOffsets, IndexOffset);
                }
                return _submeshIndex.Value;
            }
        }

        private int? _submeshCount;
        public int SubmeshCount
        {
            get
            {
                if (!_submeshCount.HasValue)
                {
                    _submeshCount = 0;
                    var submeshIndex = SubmeshIndex;
                    var submeshArray = VimGeometryData.SubmeshIndexOffsets;
                    var meshIndexOffset = IndexOffset;
                    var meshIndexCount = IndexCount;
                    for (var i = submeshIndex; i < submeshArray.Length; i++)
                    {
                        var indexOffset = submeshArray[i];
                        if (indexOffset - meshIndexOffset >= meshIndexCount)
                            break;
                        _submeshCount++;
                    }
                }
                return _submeshCount.Value;
            }
        }

        public int[] GetSubmeshMaterials()
        {
            return GetSubArray(VimGeometryData.SubmeshMaterials, SubmeshIndex, SubmeshCount);
        }

        public int[] GetSubmeshIndexOffsets()
        {
            var indexOffset = IndexOffset;
            return GetSubArray(VimGeometryData.SubmeshIndexOffsets, SubmeshIndex, SubmeshCount)
                .Select(i => i - indexOffset)
                .ToArray();
        }

        private static T[] GetSubArray<T>(T[] source, int startIndex, int count, T @default = default) where T: unmanaged
        {
            var values = new T[count];

            for (var i = 0; i < count; ++i)
            {
                var sourceIndex = startIndex + i;
                values[i] = source?.ElementAtOrDefault(sourceIndex, @default) ?? @default;
            }
            return values;
        }
    }
}