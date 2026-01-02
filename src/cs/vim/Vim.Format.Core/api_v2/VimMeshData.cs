using System;
using System.Collections.Generic;
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

    public static class VimMeshExtensions
    {
        public static AABox GetBoundingBox(this IVimMesh vimMesh)
        {
            return AABox.Create(vimMesh.GetVertices());
        }
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

    /// <summary>
    /// Compares two meshes
    /// </summary>
    public class VimMeshComparer : IVimMesh, IEquatable<VimMeshComparer>
    {
        public VimMeshView MeshView { get; }
        public int MeshIndex => MeshView.MeshIndex;
        public Vector3[] Vertices { get; }
        public int VertexCount => Vertices.Length;
        public int[] Indices { get; }
        public int FaceCount { get; }
        public int TopologyHash { get; }
        public Int3 BoxExtents { get; }
        public Int3 BoxMin { get; }
        public const float DefaultRoundingPrecision = 1f / 12f / 8f;

        /// <summary>
        /// Constructor
        /// </summary>
        public VimMeshComparer(VimMeshView vimMeshView, float roundingPrecision = DefaultRoundingPrecision)
        {
            MeshView = vimMeshView;
            Vertices = vimMeshView.GetVertices();
            Indices = vimMeshView.GetIndices();
            FaceCount = vimMeshView.FaceCount;
            TopologyHash = HashCodeStd2.GetSequenceHash(Indices);
            var box = AABox.Create(Vertices);
            BoxMin = RoundVector3(box.Min, roundingPrecision);
            BoxExtents = RoundVector3(box.Extent, roundingPrecision);
        }

        public Vector3[] GetVertices()
            => Vertices;

        public int[] GetIndices()
            => Indices;

        private static Int3 RoundVector3(Vector3 v, float roundingPrecision)
            => new Int3(
                RoundFloat(v.X, roundingPrecision),
                RoundFloat(v.Y, roundingPrecision),
                RoundFloat(v.Z, roundingPrecision)
            );

        private static int RoundFloat(float f, float roundingPrecision)
            => (int)(f / roundingPrecision);

        public override bool Equals(object obj)
            => obj is VimMeshComparer other && Equals(other);

        public bool Equals(VimMeshComparer other)
            => FaceCount == other.FaceCount
            && VertexCount == other.VertexCount
            && BoxMin.Equals(other.BoxMin)
            && BoxExtents.Equals(other.BoxExtents)
            && IsGeometryEqual(other);

        public override int GetHashCode()
            => HashCodeStd2.Combine(
                FaceCount,
                VertexCount,
                TopologyHash,
                BoxMin.GetHashCode(),
                BoxExtents.GetHashCode()
            );

        private bool IsGeometryEqual(VimMeshComparer other, float tolerance = Constants.Tolerance)
        {
            if (FaceCount != other.FaceCount || Indices.Length != other.Indices.Length || VertexCount != other.VertexCount)
                return false;

            for (var i = 0; i < FaceCount; ++i)
            {
                var i0 = i * 3;
                var i1 = i0 + 1;
                var i2 = i0 + 2;

                var a_p0 = Vertices[Indices[i0]];
                var b_p0 = other.Vertices[other.Indices[i0]];
                if (!a_p0.AlmostEquals(b_p0, tolerance))
                    return false;

                var a_p1 = Vertices[Indices[i1]];
                var b_p1 = other.Vertices[other.Indices[i1]];
                if (!a_p1.AlmostEquals(b_p1, tolerance))
                    return false;

                var a_p2 = Vertices[Indices[i2]];
                var b_p2 = other.Vertices[other.Indices[i2]];
                if (!a_p2.AlmostEquals(b_p2, tolerance))
                    return false;
            }

            return true;
        }
    }
}