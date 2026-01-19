using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.BFast;
using Vim.Math3d;
using Vim.Util;

namespace Vim.Format
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
        /// Reads the stream and returns a VimGeometryData instance.
        /// </summary>
        public VimGeometryData(Stream stream)
        {
            stream.ThrowIfNotSeekable("Could not read geometry");

            BFastBufferReader indicesBufferReader = null;
            var indexCount = 0;
            var vertexCount = 0;
            var submeshCount = 0;
            var meshCount = 0;
            var materialCount = 0;
            var instanceCount = 0;

            foreach (var bufferReader in stream.GetBFastBufferReaders())
            {
                var name = bufferReader.Name;

                switch (name)
                {
                    case HeaderBufferName:
                        Header = VimGeometryDataHeader.Read(bufferReader);
                        break;
                    case VerticesBufferName:
                        Vertices = ReadBufferData<Vector3>(bufferReader, out vertexCount);
                        break;
                    case IndicesBufferName:
                        indicesBufferReader = bufferReader;
                        Indices = ReadBufferData<int>(bufferReader, out indexCount);
                        break;
                    case SubmeshIndexOffsetsBufferName:
                        SubmeshIndexOffsets = ReadBufferData<int>(bufferReader, out submeshCount);
                        break;
                    case SubmeshMaterialsBufferName:
                        SubmeshMaterials = ReadBufferData<int>(bufferReader, out _);
                        break;
                    case MeshSubmeshOffsetsBufferName:
                        MeshSubmeshOffsets = ReadBufferData<int>(bufferReader, out meshCount);
                        break;
                    case MaterialColorsBufferName:
                        MaterialColors = ReadBufferData<Vector4>(bufferReader, out materialCount);
                        break;
                    case MaterialGlossinessBufferName:
                        MaterialGlossiness = ReadBufferData<float>(bufferReader, out _);
                        break;
                    case MaterialSmoothnessBufferName:
                        MaterialSmoothness = ReadBufferData<float>(bufferReader, out _);
                        break;
                    case InstanceTransformsBufferName:
                        InstanceTransforms = ReadBufferData<Matrix4x4>(bufferReader, out instanceCount);
                        break;
                    case InstanceFlagsBufferName:
                        InstanceFlags = ReadBufferData<ushort>(bufferReader, out _);
                        break;
                    case InstanceParentsBufferName:
                        InstanceParents = ReadBufferData<int>(bufferReader, out _);
                        break;
                    case InstanceMeshesBufferName:
                        InstanceMeshes = ReadBufferData<int>(bufferReader, out _);
                        break;
                }
            }

            if (indicesBufferReader == null)
                throw new Exception("VIM Geometry index buffer not found.");

            // Synchronize the optional instance flags
            InstanceFlags = EnsureInstanceFlags(InstanceCount, InstanceFlags);

            // Calculate offsets
            CalculateOffsets(
                indicesBufferReader,
                indexCount,
                vertexCount,
                meshCount,
                submeshCount,
                MeshSubmeshOffsets,
                SubmeshIndexOffsets,
                out var outMeshIndexOffsets,
                out var outMeshIndexCounts,
                out var outMeshVertexOffsets,
                out var outMeshVertexCounts,
                out var outMeshSubmeshCount,
                out var outSubmeshIndexCount
            );

            MeshIndexOffsets = outMeshIndexOffsets;
            MeshIndexCounts = outMeshIndexCounts;
            MeshVertexOffsets = outMeshVertexOffsets;
            MeshVertexCounts = outMeshVertexCounts;
            MeshSubmeshCount = outMeshSubmeshCount;
            SubmeshIndexCount = outSubmeshIndexCount;
        }

        public static T[] ReadBufferData<T>(BFastBufferReader bufferReader, out int itemCount) where T : unmanaged
        {
            var stream = bufferReader.Seek();
            itemCount = GetValidatedItemCount(bufferReader);
            var data = stream.ReadArray<T>(itemCount);
            return data;
        }

        public static ushort[] EnsureInstanceFlags(int instanceCount, ushort[] instanceFlagsCandidate)
        {
            if (instanceFlagsCandidate == null ||
                instanceFlagsCandidate.Length != instanceCount)
            {
                instanceFlagsCandidate = Enumerable.Repeat((ushort)0, instanceCount).ToArray();
            }

            return instanceFlagsCandidate;
        }

        public static void CalculateOffsets(
            BFastBufferReader indexReader,
            int indexCount,
            int vertexCount,
            int meshCount,
            int submeshCount,
            int[] meshSubmeshOffsets,
            int[] submeshIndexOffsets,
            out int[] outMeshIndexOffsets,
            out int[] outMeshIndexCounts,
            out int[] outMeshVertexOffsets,
            out int[] outMeshVertexCounts,
            out int[] outMeshSubmeshCount,
            out int[] outSubmeshIndexCount)
        {
            outMeshIndexOffsets = Array.Empty<int>();
            outMeshIndexCounts = Array.Empty<int>();
            outMeshVertexOffsets = Array.Empty<int>();
            outMeshVertexCounts = Array.Empty<int>();
            outMeshSubmeshCount = Array.Empty<int>();
            outSubmeshIndexCount = Array.Empty<int>();

            if (meshCount > 0)
            {
                if (meshSubmeshOffsets != null)
                {
                    outMeshIndexOffsets = meshSubmeshOffsets.Select(submesh => submeshIndexOffsets[submesh]).ToArray();
                    outMeshSubmeshCount = GetSubArrayCounts(meshSubmeshOffsets.Length, meshSubmeshOffsets, submeshCount);
                }

                if (outMeshIndexOffsets.Length != 0)
                {
                    outMeshIndexCounts = GetSubArrayCounts(meshCount, outMeshIndexOffsets, indexCount);
                    outMeshVertexOffsets = new int[outMeshIndexCounts.Length];
                    for (var i = 0; i < outMeshVertexOffsets.Length; ++i)
                    {
                        var startIndex = outMeshIndexOffsets[i];
                        var count = outMeshIndexCounts[i];
                        outMeshVertexOffsets[i] = GetMinIntegerValue(indexReader, startIndex, count);
                    }
                }

                if (outMeshVertexOffsets.Length != 0)
                {
                    outMeshVertexCounts = GetSubArrayCounts(meshCount, outMeshVertexOffsets, vertexCount);
                }
            }

            if (submeshIndexOffsets != null)
            {
                outSubmeshIndexCount = GetSubArrayCounts(submeshIndexOffsets.Length, submeshIndexOffsets, indexCount);
            }
        }

        private static int GetValidatedItemCount(BFastBufferReader bufferReader)
        {
            var bufferSizeInBytes = bufferReader.Size;
            var dataItemSizeInBytes = GetDataItemSizeInBytesByBufferName(bufferReader.Name);

            if (bufferSizeInBytes % dataItemSizeInBytes != 0)
                throw new Exception($"The number of bytes in the buffer {bufferSizeInBytes} does not divide by the item size in bytes {dataItemSizeInBytes}");

            var itemCount = bufferSizeInBytes / dataItemSizeInBytes;

            if (itemCount > int.MaxValue)
                throw new Exception($"Trying to read {itemCount} which is more than the maximum number of items in an array.");

            return (int) itemCount;
        }

        private static int GetMinIntegerValue(BFastBufferReader bufferReader, int startIndex, int count)
        {
            const int Int32SizeInBytes = 4;

            var stream = bufferReader.Seek(startIndex * Int32SizeInBytes); // Seek to the offset corresponding to the start index.
            var min = int.MaxValue;

            var intBuffer = new byte[Int32SizeInBytes];

            for (var i = 0; i < count; ++i)
            {
                // Sequentially read 4 bytes from the stream into the buffer
                var bytesRead = stream.Read(intBuffer, 0, Int32SizeInBytes);
                if (bytesRead < 4)
                    throw new EndOfStreamException("Not enough data left in the stream.");

                // Convert those 4 bytes to an integer
                var value = BitConverter.ToInt32(intBuffer, 0);

                // Compare the values
                if (value < min)
                    min = value;
            }

            return min;
        }

        // private static int GetMinValue(int[] array, int startIndex, int count)
        // {
        //     var min = int.MaxValue;
        //     for (var i = startIndex; i < startIndex + count; ++i)
        //     {
        //         var value = array[i];
        //         if (value < min) min = value;
        //     }
        //     return min;
        // }

        private static int[] GetSubArrayCounts(int numItems, int[] offsets, int totalCount)
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

        public IEnumerable<IGrouping<VimMeshComparer, VimMeshView>> GroupMeshViews(IEnumerable<int> meshIndices)
        {
            return meshIndices
                .AsParallel()
                .Select(i => GetMeshView(i))
                .Where(mv => mv != null)
                .OfType<VimMeshView>()
                .GroupBy(mv => new VimMeshComparer(mv))
                .AsSequential();
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

        public static long GetDataItemSizeInBytesByBufferName(string bufferName)
        {
            switch (bufferName)
            {
                case VerticesBufferName:
                    return VerticesBufferDataItemSizeInBytes;

                case IndicesBufferName:
                    return IndicesBufferDataItemSizeInBytes;

                case SubmeshIndexOffsetsBufferName:
                    return SubmeshIndexOffsetsDataItemSizeInBytes;

                case SubmeshMaterialsBufferName:
                    return SubmeshMaterialsDataItemSizeInBytes;

                case MeshSubmeshOffsetsBufferName:
                    return MeshSubmeshOffsetsDataItemSizeInBytes;

                case MaterialColorsBufferName:
                    return MaterialColorsDataItemSizeInBytes;

                case MaterialGlossinessBufferName:
                    return MaterialGlossinessDataItemSizeInBytes;

                case MaterialSmoothnessBufferName:
                    return MaterialSmoothnessDataItemSizeInBytes;

                case InstanceTransformsBufferName:
                    return InstanceTransformsDataItemSizeInBytes;

                case InstanceFlagsBufferName:
                    return InstanceFlagsDataItemSizeInBytes;

                case InstanceParentsBufferName:
                    return InstanceParentsDataItemSizeInBytes;

                case InstanceMeshesBufferName:
                    return InstanceMeshesDataItemSizeInBytes;

                default:
                    throw new ArgumentOutOfRangeException(nameof(bufferName), $"Unknown geometry buffer name: {bufferName}");
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
