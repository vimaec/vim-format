using System;
using System.IO;
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
        public VimGeometryDataHeader Header { get; set; } = new VimGeometryDataHeader();
        public const string HeaderBufferName = "meta";

        /// <summary>
        /// An array of 32-bit single-precision floating point values, arranged in slices of 3 to represent the (X, Y, Z) vertices of all the meshes in the VIM. We refer to this as the "vertex buffer".
        /// </summary>
        public Vector3[] Vertices { get; set; } = Array.Empty<Vector3>();
        public const string VerticesBufferName = "g3d:vertex:position:0:float32:3";
        public const int VerticesBufferDataItemSizeInBytes = 4 * 3; // float32 = 4 bytes | 3 = arity -> 4 * 3 = 12 bytes per vertex

        /// <summary>
        /// An array of 32-bit integers representing the combined index buffer of all the meshes in the VIM. The values in this index buffer are relative to the beginning of the vertex buffer. Meshes in a VIM are composed of triangular faces, whose corners are defined by 3 indices.
        /// </summary>
        public int[] Indices { get; set; } = Array.Empty<int>();
        public const string IndicesBufferName = "g3d:corner:index:0:int32:1";
        public const int IndicesBufferDataItemSizeInBytes = 4 * 1; // int32 = 4 bytes | 1 = arity -> 4 * 1 = 4 bytes per index

        /// <summary>
        /// An array of 32-bit integers representing the index offset of the index buffer of a given submesh.
        /// </summary>
        public int[] SubmeshIndexOffsets { get; set; } = Array.Empty<int>();
        public const string SubmeshIndexOffsetsBufferName = "g3d:submesh:indexoffset:0:int32:1";
        public const int SubmeshIndexOffsetsDataItemSizeInBytes = 4 * 1; // int32 = 4 bytes | 1 = arity -> 4 * 1 = 4 bytes per submesh index offset

        /// <summary>
        /// An array of 32-bit integers representing the index of the material associated with a given submesh.
        /// </summary>
        public int[] SubmeshMaterials { get; set; } = Array.Empty<int>();
        public const string SubmeshMaterialsBufferName = "g3d:submesh:material:0:int32:1";
        public const int SubmeshMaterialsDataItemSizeInBytes = 4 * 1; // int32 = 4 bytes | 1 = arity -> 4 * 1 = 4 bytes per submesh material index

        /// <summary>
        /// An array of 32-bit integers representing the index offset of a submesh in a given mesh.
        /// </summary>
        public int[] MeshSubmeshOffsets { get; set; } = Array.Empty<int>();
        public const string MeshSubmeshOffsetsBufferName = "g3d:mesh:submeshoffset:0:int32:1";
        public const int MeshSubmeshOffsetsDataItemSizeInBytes = 4 * 1; // int32 = 4 bytes | 1 = arity -> 4 * 1 = 4 bytes per mesh submesh offset

        /// <summary>
        /// An array of 32-bit single-precision floating point values in the domain [0.0f, 1.0f], arranged in slices of 4 to represent the (R, G, B, A) diffuse color of a given material.
        /// </summary>
        public Vector4[] MaterialColors { get; set; } = Array.Empty<Vector4>();
        public const string MaterialColorsBufferName = "g3d:material:color:0:float32:4";
        public const int MaterialColorsDataItemSizeInBytes = 4 * 4; // float32 = 4 bytes | 4 = arity -> 4 * 4 = 16 bytes per material color

        /// <summary>
        /// An array of 32-bit single-precision floating point values in the domain [0.0f, 1.0f] representing the glossiness of a given material.
        /// </summary>
        public float[] MaterialGlossiness { get; set; } = Array.Empty<float>();
        public const string MaterialGlossinessBufferName = "g3d:material:glossiness:0:float32:1";
        public const int MaterialGlossinessDataItemSizeInBytes = 4 * 1; // float32 = 4 bytes | 1 = arity -> 4 * 1 = 4 bytes per material glossiness

        /// <summary>
        /// An array of 32-bit single-precision floating point values in the domain [0.0f, 1.0f] representing the smoothness of a given material.
        /// </summary>
        public float[] MaterialSmoothness { get; set; } = Array.Empty<float>();
        public const string MaterialSmoothnessBufferName = "g3d:material:smoothness:0:float32:1";
        public const int MaterialSmoothnessDataItemSizeInBytes = 4 * 1; // float32 = 4 bytes | 1 = arity -> 4 * 1 = 4 bytes per material smoothness

        /// <summary>
        /// An array of 32-bit single-precision floating point values, arranged in slices of 16 to represent the 4x4 row-major transformation matrix associated with a given instance.
        /// </summary>
        public Matrix4x4[] InstanceTransforms { get; set; } = Array.Empty<Matrix4x4>();
        public const string InstanceTransformsBufferName = "g3d:instance:transform:0:float32:16";
        public const int InstanceTransformsDataItemSizeInBytes = 4 * 16; // float32 = 4 bytes | 16 = arity -> 4 * 16 = 64 bytes per instance transform

        /// <summary>
        /// (Optional) An array of 16-bit unsigned integers representing the flags of a given instance. The first bit of each flag designates whether the instance should be initially hidden (1) or not (0) when rendered.
        /// </summary>
        public ushort[] InstanceFlags { get; set; }= Array.Empty<ushort>();
        public const string InstanceFlagsBufferName = "g3d:instance:flags:0:uint16:1";
        public const int InstanceFlagsDataItemSizeInBytes = 2 * 1; // uint16 = 2 bytes | 1 = arity -> 2 * 1 = 2 bytes per instance flag

        /// <summary>
        /// An array of 32-bit integers representing the index of the parent instance associated with a given instance.
        /// </summary>
        public int[] InstanceParents { get; set; } = Array.Empty<int>();
        public const string InstanceParentsBufferName = "g3d:instance:parent:0:int32:1";
        public const int InstanceParentsDataItemSizeInBytes = 4 * 1; // int32 = 4 bytes | 1 = arity -> 4 * 1 = 4 bytes per instance parent index

        /// <summary>
        /// An array of 32-bit integers representing the index of a mesh associated with a given instance.
        /// </summary>
        public int[] InstanceMeshes { get; set; } = Array.Empty<int>();
        public const string InstanceMeshesBufferName = "g3d:instance:mesh:0:int32:1";
        public const int InstanceMeshesDataItemSizeInBytes = 4 * 1; // int32 = 4 bytes | 1 = arity -> 4 * 1 = 4 bytes per instance mesh index

        /// <summary>
        /// Reads the stream and returns a VimGeometry instance.
        /// </summary>
        public static VimGeometryData Read(Stream stream)
        {
            stream.ThrowIfNotSeekable("Could not read geometry");

            var vimGeometry = new VimGeometryData();

            foreach (var bufferReader in stream.GetBFastBufferReaders())
            {
                var name = bufferReader.Name;
                var bufferSizeInBytes = bufferReader.Size;
                bufferReader.Seek();

                switch (name)
                {
                    case HeaderBufferName:
                        vimGeometry.Header = VimGeometryDataHeader.Read(stream, bufferSizeInBytes);
                        break;
                    case VerticesBufferName:
                        vimGeometry.Vertices = ReadDataItems<Vector3>(stream, bufferSizeInBytes, VerticesBufferDataItemSizeInBytes);
                        break;
                    case IndicesBufferName:
                        vimGeometry.Indices = ReadDataItems<int>(stream, bufferSizeInBytes, IndicesBufferDataItemSizeInBytes);
                        break;
                    case SubmeshIndexOffsetsBufferName:
                        vimGeometry.SubmeshIndexOffsets = ReadDataItems<int>(stream, bufferSizeInBytes, SubmeshIndexOffsetsDataItemSizeInBytes);
                        break;
                    case SubmeshMaterialsBufferName:
                        vimGeometry.SubmeshMaterials = ReadDataItems<int>(stream, bufferSizeInBytes, SubmeshMaterialsDataItemSizeInBytes);
                        break;
                    case MeshSubmeshOffsetsBufferName:
                        vimGeometry.MeshSubmeshOffsets = ReadDataItems<int>(stream, bufferSizeInBytes, MeshSubmeshOffsetsDataItemSizeInBytes);
                        break;
                    case MaterialColorsBufferName:
                        vimGeometry.MaterialColors = ReadDataItems<Vector4>(stream, bufferSizeInBytes, MaterialColorsDataItemSizeInBytes);
                        break;
                    case MaterialGlossinessBufferName:
                        vimGeometry.MaterialGlossiness = ReadDataItems<float>(stream, bufferSizeInBytes, MaterialGlossinessDataItemSizeInBytes);
                        break;
                    case MaterialSmoothnessBufferName:
                        vimGeometry.MaterialSmoothness = ReadDataItems<float>(stream, bufferSizeInBytes, MaterialSmoothnessDataItemSizeInBytes);
                        break;
                    case InstanceTransformsBufferName:
                        vimGeometry.InstanceTransforms = ReadDataItems<Matrix4x4>(stream, bufferSizeInBytes, InstanceTransformsDataItemSizeInBytes);
                        break;
                    case InstanceFlagsBufferName:
                        vimGeometry.InstanceFlags = ReadDataItems<ushort>(stream, bufferSizeInBytes, InstanceFlagsDataItemSizeInBytes);
                        break;
                    case InstanceParentsBufferName:
                        vimGeometry.InstanceParents = ReadDataItems<int>(stream, bufferSizeInBytes, InstanceParentsDataItemSizeInBytes);
                        break;
                    case InstanceMeshesBufferName:
                        vimGeometry.InstanceMeshes = ReadDataItems<int>(stream, bufferSizeInBytes, InstanceMeshesDataItemSizeInBytes);
                        break;
                }
            }

            return vimGeometry;
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
    }
}