using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Vim.BFast;

namespace Vim.Format.api_v2
{
    /// <summary>
    /// A helper class which writes large collections of meshes and instances efficiently into a serializable stream.
    /// </summary>
    public class VimGeometryWriter : IBFastComponent
    {
        /// <summary>
        /// The deferred serialization function.
        /// </summary>
        private readonly Action<Stream> _writeAction;

        /// <summary>
        /// The number of bytes which will be written into the buffer.
        /// </summary>
        private readonly long _bufferSizeInBytes;

        /// <summary>
        /// Constructor
        /// </summary>
        public VimGeometryWriter(
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

            var vimGeometryHeaderBuffer = new VimGeometryHeader().ToBytes().ToNamedBuffer(VimGeometry.HeaderBufferName);

            (string BufferName, long BufferSizeInBytes) GetWritableBufferInfo(string bufferName, int dataItemSizeInBytes, int itemCount)
                => (bufferName, dataItemSizeInBytes * itemCount);

            var writableBufferInfos = new List<(string BufferName, long BufferSizeInBytes)>()
            {
                (vimGeometryHeaderBuffer.Name, vimGeometryHeaderBuffer.NumBytes()),
                GetWritableBufferInfo(VimGeometry.VerticesBufferName, VimGeometry.VerticesBufferDataItemSizeInBytes, totalVertices),
                GetWritableBufferInfo(VimGeometry.IndicesBufferName, VimGeometry.IndicesBufferDataItemSizeInBytes, totalIndices),

                GetWritableBufferInfo(VimGeometry.MeshSubmeshOffsetsBufferName, VimGeometry.MeshSubmeshOffsetsDataItemSizeInBytes, meshCount),

                GetWritableBufferInfo(VimGeometry.SubmeshIndexOffsetsBufferName, VimGeometry.SubmeshIndexOffsetsDataItemSizeInBytes, submeshCount),
                GetWritableBufferInfo(VimGeometry.SubmeshMaterialsBufferName, VimGeometry.SubmeshMaterialsDataItemSizeInBytes, submeshCount),

                GetWritableBufferInfo(VimGeometry.InstanceTransformsBufferName, VimGeometry.InstanceTransformsDataItemSizeInBytes, instanceCount),
                GetWritableBufferInfo(VimGeometry.InstanceParentsBufferName, VimGeometry.InstanceParentsDataItemSizeInBytes, instanceCount),
                GetWritableBufferInfo(VimGeometry.InstanceMeshesBufferName, VimGeometry.InstanceMeshesDataItemSizeInBytes, instanceCount),
                GetWritableBufferInfo(VimGeometry.InstanceFlagsBufferName, VimGeometry.InstanceFlagsDataItemSizeInBytes, instanceCount),

                GetWritableBufferInfo(VimGeometry.MaterialColorsBufferName, VimGeometry.MaterialColorsDataItemSizeInBytes, materialCount),
                GetWritableBufferInfo(VimGeometry.MaterialGlossinessBufferName, VimGeometry.MaterialGlossinessDataItemSizeInBytes, materialCount),
                GetWritableBufferInfo(VimGeometry.MaterialSmoothnessBufferName, VimGeometry.MaterialSmoothnessDataItemSizeInBytes, materialCount),
            };

            var bufferNames = writableBufferInfos.Select(w => w.BufferName).ToArray();
            var bufferSizesInBytes = writableBufferInfos.Select(w => w.BufferSizeInBytes).ToArray();
            var bfastHeader = BFast.BFast.CreateBFastHeader(bufferSizesInBytes, bufferNames);

            //-------------------------------------------
            // Store properties
            //-------------------------------------------
            _bufferSizeInBytes = BFast.BFast.ComputeNextAlignment(bfastHeader.Preamble.DataEnd);
            _writeAction = stream =>
            {
                stream.WriteBFastHeader(bfastHeader);

                stream.WriteBFastBody(bfastHeader, bufferNames, bufferSizesInBytes, (_stream, index, bufferName, size) =>
                {
                    switch (bufferName)
                    {
                        case VimGeometry.HeaderBufferName:
                            _stream.Write(vimGeometryHeaderBuffer);
                            break;

                        // Vertices
                        case VimGeometry.VerticesBufferName:
                            meshes.ForEach(g => stream.Write(g.Vertices.ToArray()));
                            break;

                        // Indices
                        case VimGeometry.IndicesBufferName:
                            for (var i = 0; i < meshes.Count; ++i)
                            {
                                var g = meshes[i];
                                var offset = meshVertexOffsets[i];
                                stream.Write(g.Indices.Select(idx => idx + offset).ToArray());
                            }
                            break;

                        // Meshes
                        case VimGeometry.MeshSubmeshOffsetsBufferName:
                            stream.Write(meshSubmeshOffsets);
                            break;

                        // Submeshes
                        case VimGeometry.SubmeshIndexOffsetsBufferName:
                            stream.Write(submeshIndexOffsets);
                            break;
                        case VimGeometry.SubmeshMaterialsBufferName:
                            stream.Write(meshes.SelectMany(s => s.SubmeshMaterials).ToArray());
                            break;

                        // Instances
                        case VimGeometry.InstanceMeshesBufferName:
                            stream.Write(instances.Select(i => i.MeshIndex).ToArray());
                            break;
                        case VimGeometry.InstanceTransformsBufferName:
                            stream.Write(instances.Select(i => i.Transform).ToArray());
                            break;
                        case VimGeometry.InstanceParentsBufferName:
                            stream.Write(instances.Select(i => i.ParentIndex).ToArray());
                            break;
                        case VimGeometry.InstanceFlagsBufferName:
                            stream.Write(instances.Select(i => (ushort)i.InstanceFlags).ToArray());
                            break;

                        // Materials
                        case VimGeometry.MaterialColorsBufferName:
                            stream.Write(materials.Select(i => i.Color).ToArray());
                            break;
                        case VimGeometry.MaterialGlossinessBufferName:
                            stream.Write(materials.Select(i => i.Glossiness).ToArray());
                            break;
                        case VimGeometry.MaterialSmoothnessBufferName:
                            stream.Write(materials.Select(i => i.Smoothness).ToArray());
                            break;

                        default:
                            Debug.Fail($"Not a recognized geometry buffer: {bufferName}");
                            break;
                    }
                    return size;
                });
            };
        }

        public long GetSize() => _bufferSizeInBytes;

        public void Write(Stream stream) => _writeAction(stream);
    }
}