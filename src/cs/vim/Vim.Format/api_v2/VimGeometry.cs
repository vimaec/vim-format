using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Vim.BFast;
using Vim.Math3d;
using Vim.Util;

namespace Vim.Format.api_v2
{
    public class VimGeometry
    {
        /// <summary>
        /// A header buffer which identifies that this collection of buffers is a VimGeometry. Preserved for continuity.
        /// </summary>
        public VimGeometryHeader Header { get; }
        public const string HeaderBufferName = "meta";

        /// <summary>
        /// An array of 32-bit single-precision floating point values, arranged in slices of 3 to represent the (X, Y, Z) vertices of all the meshes in the VIM. We refer to this as the "vertex buffer".
        /// </summary>
        public Vector3[] Vertices { get; set; }
        public const string VerticesBufferName = "g3d:vertex:position:0:float32:3";
        public const int VerticesBufferDataItemSizeInBytes = 4 * 3; // float32 = 4 bytes | 3 = arity -> 4 * 3 = 12 bytes per vertex

        /// <summary>
        /// An array of 32-bit integers representing the combined index buffer of all the meshes in the VIM. The values in this index buffer are relative to the beginning of the vertex buffer. Meshes in a VIM are composed of triangular faces, whose corners are defined by 3 indices.
        /// </summary>
        public int[] Indices { get; set; }
        public const string IndicesBufferName = "g3d:corner:index:0:int32:1";
        public const int IndicesBufferDataItemSizeInBytes = 4 * 1; // int32 = 4 bytes | 1 = arity -> 4 * 1 = 4 bytes per index

        /// <summary>
        /// An array of 32-bit integers representing the index offset of the index buffer of a given submesh.
        /// </summary>
        public int[] SubmeshIndexOffsets { get; set; }
        public const string SubmeshIndexOffsetsBufferName = "g3d:submesh:indexoffset:0:int32:1";
        public const int SubmeshIndexOffsetsDataItemSizeInBytes = 4 * 1; // int32 = 4 bytes | 1 = arity -> 4 * 1 = 4 bytes per submesh index offset

        /// <summary>
        /// An array of 32-bit integers representing the index of the material associated with a given submesh.
        /// </summary>
        public int[] SubmeshMaterials { get; set; }
        public const string SubmeshMaterialsBufferName = "g3d:submesh:material:0:int32:1";
        public const int SubmeshMaterialsDataItemSizeInBytes = 4 * 1; // int32 = 4 bytes | 1 = arity -> 4 * 1 = 4 bytes per submesh material index

        /// <summary>
        /// An array of 32-bit integers representing the index offset of a submesh in a given mesh.
        /// </summary>
        public int[] MeshSubmeshOffsets { get; set; }
        public const string MeshSubmeshOffsetsBufferName = "g3d:mesh:submeshoffset:0:int32:1";
        public const int MeshSubmeshOffsetsDataItemSizeInBytes = 4 * 1; // int32 = 4 bytes | 1 = arity -> 4 * 1 = 4 bytes per mesh submesh offset

        /// <summary>
        /// An array of 32-bit single-precision floating point values in the domain [0.0f, 1.0f], arranged in slices of 4 to represent the (R, G, B, A) diffuse color of a given material.
        /// </summary>
        public Vector4[] MaterialColors { get; set; }
        public const string MaterialColorsBufferName = "g3d:material:color:0:float32:4";
        public const int MaterialColorsDataItemSizeInBytes = 4 * 4; // float32 = 4 bytes | 4 = arity -> 4 * 4 = 16 bytes per material color

        /// <summary>
        /// An array of 32-bit single-precision floating point values in the domain [0.0f, 1.0f] representing the glossiness of a given material.
        /// </summary>
        public float[] MaterialGlossiness { get; set; }
        public const string MaterialGlossinessBufferName = "g3d:material:glossiness:0:float32:1";
        public const int MaterialGlossinessDataItemSizeInBytes = 4 * 1; // float32 = 4 bytes | 1 = arity -> 4 * 1 = 4 bytes per material glossiness

        /// <summary>
        /// An array of 32-bit single-precision floating point values in the domain [0.0f, 1.0f] representing the smoothness of a given material.
        /// </summary>
        public float[] MaterialSmoothness { get; set; }
        public const string MaterialSmoothnessBufferName = "g3d:material:smoothness:0:float32:1";
        public const int MaterialSmoothnessDataItemSizeInBytes = 4 * 1; // float32 = 4 bytes | 1 = arity -> 4 * 1 = 4 bytes per material smoothness

        /// <summary>
        /// An array of 32-bit single-precision floating point values, arranged in slices of 16 to represent the 4x4 row-major transformation matrix associated with a given instance.
        /// </summary>
        public Matrix4x4[] InstanceTransforms { get; set; }
        public const string InstanceTransformsBufferName = "g3d:instance:transform:0:float32:16";
        public const int InstanceTransformsDataItemSizeInBytes = 4 * 16; // float32 = 4 bytes | 16 = arity -> 4 * 16 = 64 bytes per instance transform

        /// <summary>
        /// (Optional) An array of 16-bit unsigned integers representing the flags of a given instance. The first bit of each flag designates whether the instance should be initially hidden (1) or not (0) when rendered.
        /// </summary>
        public ushort[] InstanceFlags { get; set; }
        public const string InstanceFlagsBufferName = "g3d:instance:flags:0:uint16:1";
        public const int InstanceFlagsDataItemSizeInBytes = 2 * 1; // uint16 = 2 bytes | 1 = arity -> 2 * 1 = 2 bytes per instance flag

        /// <summary>
        /// An array of 32-bit integers representing the index of the parent instance associated with a given instance.
        /// </summary>
        public int[] InstanceParents { get; set; }
        public const string InstanceParentsBufferName = "g3d:instance:parent:0:int32:1";
        public const int InstanceParentsDataItemSizeInBytes = 4 * 1; // int32 = 4 bytes | 1 = arity -> 4 * 1 = 4 bytes per instance parent index

        /// <summary>
        /// An array of 32-bit integers representing the index of a mesh associated with a given instance.
        /// </summary>
        public int[] InstanceMeshes { get; set; }
        public const string InstanceMeshesBufferName = "g3d:instance:mesh:0:int32:1";
        public const int InstanceMeshesDataItemSizeInBytes = 4 * 1; // int32 = 4 bytes | 1 = arity -> 4 * 1 = 4 bytes per instance mesh index

        /// <summary>
        /// Constructor.
        /// </summary>
        public VimGeometry(
            VimGeometryHeader header,
            Vector3[] vertices,
            int[] indices,
            int[] submeshIndexOffsets,
            int[] submeshMaterials,
            int[] meshSubmeshOffsets,
            Vector4[] materialColors,
            float[] materialGlossiness,
            float[] materialSmoothness,
            Matrix4x4[] instanceTransforms,
            ushort[] instanceFlags,
            int[] instanceParents,
            int[] instanceMeshes)
        {
            Header = header;
            Vertices = vertices;
            Indices = indices;
            SubmeshIndexOffsets = submeshIndexOffsets;
            SubmeshMaterials = submeshMaterials;
            MeshSubmeshOffsets = meshSubmeshOffsets;
            MaterialColors = materialColors;
            MaterialGlossiness = materialGlossiness;
            MaterialSmoothness = materialSmoothness;
            InstanceTransforms = instanceTransforms;
            InstanceFlags = instanceFlags;
            InstanceParents = instanceParents;
            InstanceMeshes = instanceMeshes;
        }

        public static VimGeometry Read(Stream stream)
        {
            stream.ThrowIfNotSeekable("Could not read geometry");

            VimGeometryHeader header = null;
            var vertices = Array.Empty<Vector3>();
            var indices = Array.Empty<int>();
            var submeshIndexOffsets = Array.Empty<int>();
            var submeshMaterials = Array.Empty<int>();
            var meshSubmeshOffsets = Array.Empty<int>();
            var materialColors = Array.Empty<Vector4>();
            var materialGlossiness = Array.Empty<float>();
            var materialSmoothness = Array.Empty<float>();
            var instanceTransforms = Array.Empty<Matrix4x4>();
            var instanceFlags = Array.Empty<ushort>();
            var instanceParents = Array.Empty<int>();
            var instanceMeshes = Array.Empty<int>();

            foreach (var bufferReader in stream.GetBFastBufferReaders())
            {
                var name = bufferReader.Name;
                var bufferSizeInBytes = bufferReader.Size;
                bufferReader.Seek();

                switch (name)
                {
                    case HeaderBufferName:
                        header = VimGeometryHeader.Read(stream, bufferSizeInBytes);
                        break;
                    case VerticesBufferName:
                        vertices = ReadDataItems<Vector3>(stream, bufferSizeInBytes, VerticesBufferDataItemSizeInBytes);
                        break;
                    case IndicesBufferName:
                        indices = ReadDataItems<int>(stream, bufferSizeInBytes, IndicesBufferDataItemSizeInBytes);
                        break;
                    case SubmeshIndexOffsetsBufferName:
                        submeshIndexOffsets = ReadDataItems<int>(stream, bufferSizeInBytes, SubmeshIndexOffsetsDataItemSizeInBytes);
                        break;
                    case SubmeshMaterialsBufferName:
                        submeshMaterials = ReadDataItems<int>(stream, bufferSizeInBytes, SubmeshMaterialsDataItemSizeInBytes);
                        break;
                    case MeshSubmeshOffsetsBufferName:
                        meshSubmeshOffsets = ReadDataItems<int>(stream, bufferSizeInBytes, MeshSubmeshOffsetsDataItemSizeInBytes);
                        break;
                    case MaterialColorsBufferName:
                        materialColors = ReadDataItems<Vector4>(stream, bufferSizeInBytes, MaterialColorsDataItemSizeInBytes);
                        break;
                    case MaterialGlossinessBufferName:
                        materialGlossiness = ReadDataItems<float>(stream, bufferSizeInBytes, MaterialGlossinessDataItemSizeInBytes);
                        break;
                    case MaterialSmoothnessBufferName:
                        materialSmoothness = ReadDataItems<float>(stream, bufferSizeInBytes, MaterialSmoothnessDataItemSizeInBytes);
                        break;
                    case InstanceTransformsBufferName:
                        instanceTransforms = ReadDataItems<Matrix4x4>(stream, bufferSizeInBytes, InstanceTransformsDataItemSizeInBytes);
                        break;
                    case InstanceFlagsBufferName:
                        instanceFlags = ReadDataItems<ushort>(stream, bufferSizeInBytes, InstanceFlagsDataItemSizeInBytes);
                        break;
                    case InstanceParentsBufferName:
                        instanceParents = ReadDataItems<int>(stream, bufferSizeInBytes, InstanceParentsDataItemSizeInBytes);
                        break;
                    case InstanceMeshesBufferName:
                        instanceMeshes = ReadDataItems<int>(stream, bufferSizeInBytes, InstanceMeshesDataItemSizeInBytes);
                        break;
                }
            }

            return new VimGeometry(
                header,
                vertices,
                indices,
                submeshIndexOffsets,
                submeshMaterials,
                meshSubmeshOffsets,
                materialColors,
                materialGlossiness,
                materialSmoothness,
                instanceTransforms,
                instanceFlags,
                instanceParents,
                instanceMeshes);
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

        public static (long BufferSize, Action<Stream> WriteAction) CreateWriter(
            List<VimSubdividedMesh> meshes,
            List<VimInstance> instances,
            List<VimMaterial> materials)
        {
            //-------------------------------------------
            // Calculate buffer sizes
            //-------------------------------------------

            var totalSubmeshCount = meshes.Select(s => s.SubmeshesIndexOffset.Count).Sum();

            // Compute the Vertex offsets and index offsets 
            var meshVertexOffsets = new int[meshes.Count];
            var meshIndexOffsets = new int[meshes.Count];
            var submeshIndexOffsets = new int[totalSubmeshCount];
            var meshSubmeshOffsets = new int[meshes.Count];

            var meshCount = meshes.Count;

            for (var i = 1; i < meshCount; ++i)
            {
                meshVertexOffsets[i] = meshVertexOffsets[i - 1] + meshes[i - 1].Vertices.Count;
                meshIndexOffsets[i] = meshIndexOffsets[i - 1] + meshes[i - 1].Indices.Count;
                meshSubmeshOffsets[i] = meshSubmeshOffsets[i - 1] + meshes[i - 1].SubmeshesIndexOffset.Count;
            }

            var subIndex =0;
            var previousIndexCount = 0;
            foreach(var geo in meshes)
            {
                foreach(var sub in geo.SubmeshesIndexOffset)
                {
                    submeshIndexOffsets[subIndex++] = sub + previousIndexCount;
                }
                previousIndexCount += geo.Indices.Count;
            }

            var submeshCount = meshes.Select(s => s.SubmeshesIndexOffset.Count).Sum();

            var totalVertices = meshCount == 0 ? 0 : meshVertexOffsets[meshCount - 1] + meshes[meshCount - 1].Vertices.Count;
            var totalIndices = meshCount == 0 ? 0 : meshIndexOffsets[meshCount - 1] + meshes[meshCount - 1].Indices.Count;
            long totalFaces = totalIndices / 3;

            var instanceCount = instances.Count;
            var materialCount = materials.Count;

            //-------------------------------------------
            // Prepare buffer info
            //-------------------------------------------

            var vimGeometryHeaderBuffer = new VimGeometryHeader().ToBytes().ToNamedBuffer(HeaderBufferName);

            (string BufferName, long BufferSizeInBytes) GetWritableBufferInfo(string bufferName, int dataItemSizeInBytes, int itemCount)
                => (bufferName, dataItemSizeInBytes * itemCount);

            var writableBufferInfos = new List<(string BufferName, long BufferSizeInBytes)>()
            {
                (vimGeometryHeaderBuffer.Name, vimGeometryHeaderBuffer.NumBytes()),
                GetWritableBufferInfo(VerticesBufferName, VerticesBufferDataItemSizeInBytes, totalVertices),
                GetWritableBufferInfo(IndicesBufferName, IndicesBufferDataItemSizeInBytes, totalIndices),

                GetWritableBufferInfo(MeshSubmeshOffsetsBufferName, MeshSubmeshOffsetsDataItemSizeInBytes, meshCount),

                GetWritableBufferInfo(SubmeshIndexOffsetsBufferName, SubmeshIndexOffsetsDataItemSizeInBytes, submeshCount),
                GetWritableBufferInfo(SubmeshMaterialsBufferName, SubmeshMaterialsDataItemSizeInBytes, submeshCount),

                GetWritableBufferInfo(InstanceTransformsBufferName, InstanceTransformsDataItemSizeInBytes, instanceCount),
                GetWritableBufferInfo(InstanceParentsBufferName, InstanceParentsDataItemSizeInBytes, instanceCount),
                GetWritableBufferInfo(InstanceMeshesBufferName, InstanceMeshesDataItemSizeInBytes, instanceCount),
                GetWritableBufferInfo(InstanceFlagsBufferName, InstanceFlagsDataItemSizeInBytes, instanceCount),

                GetWritableBufferInfo(MaterialColorsBufferName, MaterialColorsDataItemSizeInBytes, materialCount),
                GetWritableBufferInfo(MaterialGlossinessBufferName, MaterialGlossinessDataItemSizeInBytes, materialCount),
                GetWritableBufferInfo(MaterialSmoothnessBufferName, MaterialSmoothnessDataItemSizeInBytes, materialCount),
            };

            var bufferNames = writableBufferInfos.Select(w => w.BufferName).ToArray();
            var bufferSizesInBytes = writableBufferInfos.Select(w => w.BufferSizeInBytes).ToArray();
            var bfastHeader = BFast.BFast.CreateBFastHeader(bufferSizesInBytes, bufferNames);

            //-------------------------------------------
            // Return a stream writer
            //-------------------------------------------
            return (
                BFast.BFast.ComputeNextAlignment(bfastHeader.Preamble.DataEnd),
                stream =>
            {
                stream.WriteBFastHeader(bfastHeader);

                stream.WriteBFastBody(bfastHeader, bufferNames, bufferSizesInBytes, (_stream, index, bufferName, size) =>
                {
                    switch (bufferName)
                    {
                        case HeaderBufferName:
                            _stream.Write(vimGeometryHeaderBuffer);
                            break;

                        // Vertices
                        case VerticesBufferName:
                            meshes.ForEach(g => stream.Write(g.Vertices.ToArray()));
                            break;

                        // Indices
                        case IndicesBufferName:
                            for (var i = 0; i < meshes.Count; ++i)
                            {
                                var g = meshes[i];
                                var offset = meshVertexOffsets[i];
                                stream.Write(g.Indices.Select(idx => idx + offset).ToArray());
                            }
                            break;

                        // Meshes
                        case MeshSubmeshOffsetsBufferName:
                            stream.Write(meshSubmeshOffsets);
                            break;

                        // Submeshes
                        case SubmeshIndexOffsetsBufferName:
                            stream.Write(submeshIndexOffsets);
                            break;
                        case SubmeshMaterialsBufferName:
                            stream.Write(meshes.SelectMany(s => s.SubmeshMaterials).ToArray());
                            break;

                        // Instances
                        case InstanceMeshesBufferName:
                            stream.Write(instances.Select(i => i.MeshIndex).ToArray());
                            break;
                        case InstanceTransformsBufferName:
                            stream.Write(instances.Select(i => i.Transform).ToArray());
                            break;
                        case InstanceParentsBufferName:
                            stream.Write(instances.Select(i => i.ParentIndex).ToArray());
                            break;
                        case InstanceFlagsBufferName:
                            stream.Write(instances.Select(i => (ushort)i.InstanceFlags).ToArray());
                            break;

                        // Materials
                        case MaterialColorsBufferName:
                            stream.Write(materials.Select(i => i.Color).ToArray());
                            break;
                        case MaterialGlossinessBufferName:
                            stream.Write(materials.Select(i => i.Glossiness).ToArray());
                            break;
                        case MaterialSmoothnessBufferName:
                            stream.Write(materials.Select(i => i.Smoothness).ToArray());
                            break;

                        default:
                            Debug.Fail($"Not a recognized geometry buffer: {bufferName}");
                            break;
                    }
                    return size;
                });
            });
        }
    }
}