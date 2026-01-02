using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.BFast;
using Vim.Math3d;
using Vim.Util;

namespace Vim.Format.api_v2
{
    /// <summary>
    /// Represents the geometric elements which compose a building design.
    /// </summary>
    public class VimGeometryData
    {
        /// <summary>
        /// A header buffer which identifies that this collection of buffers is a VimGeometry. Preserved for continuity.
        /// </summary>
        public VimGeometryDataHeader Header { get; } = new VimGeometryDataHeader();
        public const string HeaderBufferName = "meta";

        /// <summary>
        /// An array of 32-bit single-precision floating point values, arranged in slices of 3 to represent the (X, Y, Z) vertices of all the meshes in the VIM. We refer to this as the "vertex buffer".
        /// </summary>
        public Vector3[] Vertices { get; } = Array.Empty<Vector3>();
        public const string VerticesBufferName = "g3d:vertex:position:0:float32:3";
        public const int VerticesBufferDataItemSizeInBytes = 4 * 3; // float32 = 4 bytes | 3 = arity -> 4 * 3 = 12 bytes per vertex

        /// <summary>
        /// An array of 32-bit integers representing the combined index buffer of all the meshes in the VIM. The values in this index buffer are relative to the beginning of the vertex buffer. Meshes in a VIM are composed of triangular faces, whose corners are defined by 3 indices.
        /// </summary>
        public int[] Indices { get; } = Array.Empty<int>();
        public const string IndicesBufferName = "g3d:corner:index:0:int32:1";
        public const int IndicesBufferDataItemSizeInBytes = 4 * 1; // int32 = 4 bytes | 1 = arity -> 4 * 1 = 4 bytes per index

        /// <summary>
        /// An array of 32-bit integers representing the index offset of the index buffer of a given submesh.
        /// </summary>
        public int[] SubmeshIndexOffsets { get; } = Array.Empty<int>();
        public const string SubmeshIndexOffsetsBufferName = "g3d:submesh:indexoffset:0:int32:1";
        public const int SubmeshIndexOffsetsDataItemSizeInBytes = 4 * 1; // int32 = 4 bytes | 1 = arity -> 4 * 1 = 4 bytes per submesh index offset

        /// <summary>
        /// An array of 32-bit integers representing the index of the material associated with a given submesh.
        /// </summary>
        public int[] SubmeshMaterials { get; } = Array.Empty<int>();
        public const string SubmeshMaterialsBufferName = "g3d:submesh:material:0:int32:1";
        public const int SubmeshMaterialsDataItemSizeInBytes = 4 * 1; // int32 = 4 bytes | 1 = arity -> 4 * 1 = 4 bytes per submesh material index

        /// <summary>
        /// An array of 32-bit integers representing the index offset of a submesh in a given mesh.
        /// </summary>
        public int[] MeshSubmeshOffsets { get; } = Array.Empty<int>();
        public const string MeshSubmeshOffsetsBufferName = "g3d:mesh:submeshoffset:0:int32:1";
        public const int MeshSubmeshOffsetsDataItemSizeInBytes = 4 * 1; // int32 = 4 bytes | 1 = arity -> 4 * 1 = 4 bytes per mesh submesh offset

        /// <summary>
        /// An array of 32-bit single-precision floating point values in the domain [0.0f, 1.0f], arranged in slices of 4 to represent the (R, G, B, A) diffuse color of a given material.
        /// </summary>
        public Vector4[] MaterialColors { get; } = Array.Empty<Vector4>();
        public const string MaterialColorsBufferName = "g3d:material:color:0:float32:4";
        public const int MaterialColorsDataItemSizeInBytes = 4 * 4; // float32 = 4 bytes | 4 = arity -> 4 * 4 = 16 bytes per material color

        /// <summary>
        /// An array of 32-bit single-precision floating point values in the domain [0.0f, 1.0f] representing the glossiness of a given material.
        /// </summary>
        public float[] MaterialGlossiness { get; } = Array.Empty<float>();
        public const string MaterialGlossinessBufferName = "g3d:material:glossiness:0:float32:1";
        public const int MaterialGlossinessDataItemSizeInBytes = 4 * 1; // float32 = 4 bytes | 1 = arity -> 4 * 1 = 4 bytes per material glossiness

        /// <summary>
        /// An array of 32-bit single-precision floating point values in the domain [0.0f, 1.0f] representing the smoothness of a given material.
        /// </summary>
        public float[] MaterialSmoothness { get; } = Array.Empty<float>();
        public const string MaterialSmoothnessBufferName = "g3d:material:smoothness:0:float32:1";
        public const int MaterialSmoothnessDataItemSizeInBytes = 4 * 1; // float32 = 4 bytes | 1 = arity -> 4 * 1 = 4 bytes per material smoothness

        /// <summary>
        /// An array of 32-bit single-precision floating point values, arranged in slices of 16 to represent the 4x4 row-major transformation matrix associated with a given instance.
        /// </summary>
        public Matrix4x4[] InstanceTransforms { get; } = Array.Empty<Matrix4x4>();
        public const string InstanceTransformsBufferName = "g3d:instance:transform:0:float32:16";
        public const int InstanceTransformsDataItemSizeInBytes = 4 * 16; // float32 = 4 bytes | 16 = arity -> 4 * 16 = 64 bytes per instance transform

        /// <summary>
        /// (Optional) An array of 16-bit unsigned integers representing the flags of a given instance. The first bit of each flag designates whether the instance should be initially hidden (1) or not (0) when rendered.
        /// </summary>
        public ushort[] InstanceFlags { get; } = Array.Empty<ushort>();
        public const string InstanceFlagsBufferName = "g3d:instance:flags:0:uint16:1";
        public const int InstanceFlagsDataItemSizeInBytes = 2 * 1; // uint16 = 2 bytes | 1 = arity -> 2 * 1 = 2 bytes per instance flag

        /// <summary>
        /// An array of 32-bit integers representing the index of the parent instance associated with a given instance.
        /// </summary>
        public int[] InstanceParents { get; } = Array.Empty<int>();
        public const string InstanceParentsBufferName = "g3d:instance:parent:0:int32:1";
        public const int InstanceParentsDataItemSizeInBytes = 4 * 1; // int32 = 4 bytes | 1 = arity -> 4 * 1 = 4 bytes per instance parent index

        /// <summary>
        /// An array of 32-bit integers representing the index of a mesh associated with a given instance.
        /// </summary>
        public int[] InstanceMeshes { get; } = Array.Empty<int>();
        public const string InstanceMeshesBufferName = "g3d:instance:mesh:0:int32:1";
        public const int InstanceMeshesDataItemSizeInBytes = 4 * 1; // int32 = 4 bytes | 1 = arity -> 4 * 1 = 4 bytes per instance mesh index

        public int InstanceCount => InstanceTransforms?.Length ?? 0;
        public int MeshCount => MeshSubmeshOffsets?.Length ?? 0;
        public int SubmeshCount => SubmeshIndexOffsets?.Length ?? 0;
        public int IndexCount => Indices?.Length ?? 0;
        public int VertexCount => Vertices?.Length ?? 0;
        public int MaterialCount => MaterialColors?.Length ?? 0;

        public int[] MeshIndexOffsets { get; } = Array.Empty<int>();
        public int[] MeshSubmeshCount { get; } = Array.Empty<int>();
        public int[] MeshIndexCounts { get; } = Array.Empty<int>();
        public int[] MeshVertexOffsets { get; } = Array.Empty<int>();
        public int[] MeshVertexCounts { get; } = Array.Empty<int>();
        public int[] SubmeshIndexCount { get; } = Array.Empty<int>();

        /// <summary>
        /// Default empty constructor.
        /// </summary>
        public VimGeometryData()
        { }

        /// <summary>
        /// Reads the stream and returns a VimGeometry instance.
        /// </summary>
        public VimGeometryData(Stream stream)
        {
            stream.ThrowIfNotSeekable("Could not read geometry");

            foreach (var bufferReader in stream.GetBFastBufferReaders())
            {
                var name = bufferReader.Name;
                var bufferSizeInBytes = bufferReader.Size;
                bufferReader.Seek();

                switch (name)
                {
                    case HeaderBufferName:
                        Header = VimGeometryDataHeader.Read(stream, bufferSizeInBytes);
                        break;
                    case VerticesBufferName:
                        Vertices = ReadDataItems<Vector3>(stream, bufferSizeInBytes, VerticesBufferDataItemSizeInBytes);
                        break;
                    case IndicesBufferName:
                        Indices = ReadDataItems<int>(stream, bufferSizeInBytes, IndicesBufferDataItemSizeInBytes);
                        break;
                    case SubmeshIndexOffsetsBufferName:
                        SubmeshIndexOffsets = ReadDataItems<int>(stream, bufferSizeInBytes, SubmeshIndexOffsetsDataItemSizeInBytes);
                        break;
                    case SubmeshMaterialsBufferName:
                        SubmeshMaterials = ReadDataItems<int>(stream, bufferSizeInBytes, SubmeshMaterialsDataItemSizeInBytes);
                        break;
                    case MeshSubmeshOffsetsBufferName:
                        MeshSubmeshOffsets = ReadDataItems<int>(stream, bufferSizeInBytes, MeshSubmeshOffsetsDataItemSizeInBytes);
                        break;
                    case MaterialColorsBufferName:
                        MaterialColors = ReadDataItems<Vector4>(stream, bufferSizeInBytes, MaterialColorsDataItemSizeInBytes);
                        break;
                    case MaterialGlossinessBufferName:
                        MaterialGlossiness = ReadDataItems<float>(stream, bufferSizeInBytes, MaterialGlossinessDataItemSizeInBytes);
                        break;
                    case MaterialSmoothnessBufferName:
                        MaterialSmoothness = ReadDataItems<float>(stream, bufferSizeInBytes, MaterialSmoothnessDataItemSizeInBytes);
                        break;
                    case InstanceTransformsBufferName:
                        InstanceTransforms = ReadDataItems<Matrix4x4>(stream, bufferSizeInBytes, InstanceTransformsDataItemSizeInBytes);
                        break;
                    case InstanceFlagsBufferName:
                        InstanceFlags = ReadDataItems<ushort>(stream, bufferSizeInBytes, InstanceFlagsDataItemSizeInBytes);
                        break;
                    case InstanceParentsBufferName:
                        InstanceParents = ReadDataItems<int>(stream, bufferSizeInBytes, InstanceParentsDataItemSizeInBytes);
                        break;
                    case InstanceMeshesBufferName:
                        InstanceMeshes = ReadDataItems<int>(stream, bufferSizeInBytes, InstanceMeshesDataItemSizeInBytes);
                        break;
                }
            }

            // Synchronize the optional instance flags

            if ((InstanceFlags?.Length ?? 0) == 0)
            {
                InstanceFlags = Enumerable.Repeat((ushort)0, InstanceCount).ToArray();
            }

            // Compute offsets

            if (MeshCount > 0)
            {
                if (MeshSubmeshOffsets != null)
                {
                    MeshIndexOffsets = MeshSubmeshOffsets.Select(submesh => SubmeshIndexOffsets[submesh]).ToArray();
                    MeshSubmeshCount = GetSubArrayCounts(MeshSubmeshOffsets.Length, MeshSubmeshOffsets, SubmeshCount);
                }

                if (MeshIndexOffsets.Length != 0)
                {
                    MeshIndexCounts = GetSubArrayCounts(MeshCount, MeshIndexOffsets, IndexCount);
                    MeshVertexOffsets = MeshIndexOffsets.Zip(MeshIndexCounts,
                        (start, count) => GetMinValue(Indices, start, count))
                        .ToArray();
                }

                if (MeshVertexOffsets.Length != 0)
                {
                    MeshVertexCounts = GetSubArrayCounts(MeshCount, MeshVertexOffsets, VertexCount);
                }
            }

            if (SubmeshIndexOffsets != null)
            {
                SubmeshIndexCount = GetSubArrayCounts(SubmeshIndexOffsets.Length, SubmeshIndexOffsets, IndexCount);
            }
        }

        private static T[] ReadDataItems<T>(Stream stream, long bufferSizeInBytes, int dataItemSizeInBytes) where T : unmanaged
        {
            if (bufferSizeInBytes % dataItemSizeInBytes != 0)
                throw new Exception($"The number of bytes in the buffer {bufferSizeInBytes} does not divide by the item size in bytes {dataItemSizeInBytes}");

            var itemCount = bufferSizeInBytes / dataItemSizeInBytes;

            if (itemCount > int.MaxValue)
                throw new Exception($"Trying to read {itemCount} which is more than the maximum number of items in an array.");

            var data = stream.ReadArray<T>((int)itemCount);

            return data;
        }

        private static int GetMinValue(int[] array, int startIndex, int count)
        {
            var min = int.MaxValue;
            for (var i = startIndex; i < startIndex + count; ++i)
            {
                var value = array[i];
                if (value < min) min = value;
            }
            return min;
        }

        private int[] GetSubArrayCounts(int numItems, int[] offsets, int totalCount)
        {
            var counts = new int[numItems];
            for (var i = 0; i < numItems; ++i)
            {
                counts[i] = i < (numItems - 1)
                    ? offsets[i + 1] - offsets[i]
                    : totalCount - offsets[i];
            }
            return counts;
        }

        public bool TryGetVimMeshView(int meshIndex, out VimMeshView vimMeshView)
        {
            vimMeshView = default;

            if (meshIndex < 0 || meshIndex >= MeshCount)
                return false;

            vimMeshView = new VimMeshView(this, meshIndex);

            return true;
        }

        public VimMeshView? GetMeshView(int meshIndex)
        {
            if (meshIndex < 0 || meshIndex >= MeshCount)
                return null;

            return new VimMeshView(this, meshIndex);
        }

        public Dictionary<VimMeshComparer, VimMeshView[]> GroupMeshViews(IEnumerable<int> meshIndices)
        {
            var meshComparers = meshIndices
                .AsParallel()
                .Select(i => GetMeshView(i))
                .Where(mv => mv != null)
                .Select(mv => new VimMeshComparer(mv.Value))
                .ToArray();

            var groups = meshComparers.GroupBy(c => c);

            return groups.ToDictionary(g => g.Key, g => g.Select(i => i.MeshView).ToArray());
        }

        public static Vector3[] GetTransformedVertices(Vector3[] sourceVertices, Matrix4x4 transform)
        {
            var transformedVertices = new Vector3[sourceVertices.Length];
            for (var i = 0; i < sourceVertices.Length; ++i)
            {
                transformedVertices[i] = sourceVertices[i].Transform(transform);
            }
            return transformedVertices;
        }

        public bool TryGetTransformedMesh(int instanceIndex, int meshIndex, out VimMeshData vimMeshData)
        {
            vimMeshData = default;

            if (!TryGetVimMeshView(meshIndex, out var vimMeshView))
                return false;

            var transform = InstanceTransforms.ElementAtOrDefault(instanceIndex, Matrix4x4.Identity);

            var sourceVertices = vimMeshView.GetVertices();
            var transformedVertices = GetTransformedVertices(sourceVertices, transform);

            vimMeshData = new VimMeshData(transformedVertices, vimMeshView.GetIndices());
            return true;
        }

        public AABox GetWorldSpaceBoundingBox()
        {
            // Iterate over each instance and get its mesh.
            var result = AABox.Empty;
            for (var i = 0; i < InstanceCount; ++i)
            {
                var instanceMeshIndex = InstanceMeshes[i];
                if (!TryGetVimMeshView(instanceMeshIndex, out var vimMeshView))
                    continue;

                var instanceTransform = InstanceTransforms[i];
                var worldSpaceVertices = GetTransformedVertices(vimMeshView.GetVertices(), instanceTransform);
                var aabbInstance = AABox.Create(worldSpaceVertices);
                result = result.Merge(aabbInstance);
            }
            return result;
        }

        public IEnumerable<VimMeshView> GetMeshViews()
        {
            for (var i = 0; i < MeshCount; ++i)
            {
                yield return new VimMeshView(this, i);
            }
        }

        public int GetItemCountByBufferName(string bufferName)
        {
            switch (bufferName)
            {
                case VerticesBufferName:
                    return Vertices.Length;
                
                case IndicesBufferName:
                    return Indices.Length;
                
                case SubmeshIndexOffsetsBufferName:
                    return SubmeshIndexOffsets.Length;
                
                case SubmeshMaterialsBufferName:
                    return SubmeshMaterials.Length;
                
                case MeshSubmeshOffsetsBufferName:
                    return MeshSubmeshOffsets.Length;
                
                case MaterialColorsBufferName:
                    return MaterialColors.Length;

                case MaterialGlossinessBufferName:
                    return MaterialGlossiness.Length;

                case MaterialSmoothnessBufferName:
                    return MaterialSmoothness.Length;

                case InstanceTransformsBufferName:
                    return InstanceTransforms.Length;

                case InstanceFlagsBufferName:
                    return InstanceFlags.Length;

                case InstanceParentsBufferName:
                    return InstanceParents.Length;

                case InstanceMeshesBufferName:
                    return InstanceMeshes.Length;

                default:
                    return 0;
            }
        }
    }
}