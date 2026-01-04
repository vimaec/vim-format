using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Vim.BFast;

namespace Vim.Format
{
    /// <summary>
    /// A helper class which writes large collections of meshes and instances efficiently into a serializable stream.
    /// </summary>
    public class VimGeometryDataWriter : IBFastComponent
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
        public VimGeometryDataWriter(
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

            var subIndex = 0;
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

            var vimGeometryHeaderBuffer = new VimGeometryDataHeader().ToBytes().ToNamedBuffer(VimGeometryData.HeaderBufferName);

            (string BufferName, long BufferSizeInBytes) GetWritableBufferInfo(string bufferName, int dataItemSizeInBytes, int itemCount)
                => (bufferName, dataItemSizeInBytes * itemCount);

            var writableBufferInfos = new List<(string BufferName, long BufferSizeInBytes)>()
            {
                (vimGeometryHeaderBuffer.Name, vimGeometryHeaderBuffer.NumBytes()),
                GetWritableBufferInfo(VimGeometryData.VerticesBufferName, VimGeometryData.VerticesBufferDataItemSizeInBytes, totalVertices),
                GetWritableBufferInfo(VimGeometryData.IndicesBufferName, VimGeometryData.IndicesBufferDataItemSizeInBytes, totalIndices),

                GetWritableBufferInfo(VimGeometryData.MeshSubmeshOffsetsBufferName, VimGeometryData.MeshSubmeshOffsetsDataItemSizeInBytes, meshCount),

                GetWritableBufferInfo(VimGeometryData.SubmeshIndexOffsetsBufferName, VimGeometryData.SubmeshIndexOffsetsDataItemSizeInBytes, submeshCount),
                GetWritableBufferInfo(VimGeometryData.SubmeshMaterialsBufferName, VimGeometryData.SubmeshMaterialsDataItemSizeInBytes, submeshCount),

                GetWritableBufferInfo(VimGeometryData.InstanceTransformsBufferName, VimGeometryData.InstanceTransformsDataItemSizeInBytes, instanceCount),
                GetWritableBufferInfo(VimGeometryData.InstanceParentsBufferName, VimGeometryData.InstanceParentsDataItemSizeInBytes, instanceCount),
                GetWritableBufferInfo(VimGeometryData.InstanceMeshesBufferName, VimGeometryData.InstanceMeshesDataItemSizeInBytes, instanceCount),
                GetWritableBufferInfo(VimGeometryData.InstanceFlagsBufferName, VimGeometryData.InstanceFlagsDataItemSizeInBytes, instanceCount),

                GetWritableBufferInfo(VimGeometryData.MaterialColorsBufferName, VimGeometryData.MaterialColorsDataItemSizeInBytes, materialCount),
                GetWritableBufferInfo(VimGeometryData.MaterialGlossinessBufferName, VimGeometryData.MaterialGlossinessDataItemSizeInBytes, materialCount),
                GetWritableBufferInfo(VimGeometryData.MaterialSmoothnessBufferName, VimGeometryData.MaterialSmoothnessDataItemSizeInBytes, materialCount),
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
                        case VimGeometryData.HeaderBufferName:
                            _stream.Write(vimGeometryHeaderBuffer);
                            break;

                        // Vertices
                        case VimGeometryData.VerticesBufferName:
                            meshes.ForEach(g => stream.Write(g.Vertices.ToArray()));
                            break;

                        // Indices
                        case VimGeometryData.IndicesBufferName:
                            for (var i = 0; i < meshes.Count; ++i)
                            {
                                var g = meshes[i];
                                var offset = meshVertexOffsets[i];
                                stream.Write(g.Indices.Select(idx => idx + offset).ToArray());
                            }
                            break;

                        // Meshes
                        case VimGeometryData.MeshSubmeshOffsetsBufferName:
                            stream.Write(meshSubmeshOffsets);
                            break;

                        // Submeshes
                        case VimGeometryData.SubmeshIndexOffsetsBufferName:
                            stream.Write(submeshIndexOffsets);
                            break;
                        case VimGeometryData.SubmeshMaterialsBufferName:
                            stream.Write(meshes.SelectMany(s => s.SubmeshMaterials).ToArray());
                            break;

                        // Instances
                        case VimGeometryData.InstanceMeshesBufferName:
                            stream.Write(instances.Select(i => i.MeshIndex).ToArray());
                            break;
                        case VimGeometryData.InstanceTransformsBufferName:
                            stream.Write(instances.Select(i => i.Transform).ToArray());
                            break;
                        case VimGeometryData.InstanceParentsBufferName:
                            stream.Write(instances.Select(i => i.ParentIndex).ToArray());
                            break;
                        case VimGeometryData.InstanceFlagsBufferName:
                            stream.Write(instances.Select(i => (ushort)i.InstanceFlags).ToArray());
                            break;

                        // Materials
                        case VimGeometryData.MaterialColorsBufferName:
                            stream.Write(materials.Select(i => i.Color).ToArray());
                            break;
                        case VimGeometryData.MaterialGlossinessBufferName:
                            stream.Write(materials.Select(i => i.Glossiness).ToArray());
                            break;
                        case VimGeometryData.MaterialSmoothnessBufferName:
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