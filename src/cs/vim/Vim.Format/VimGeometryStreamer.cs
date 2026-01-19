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
    /// A geometry loading utility which streams mesh data from the VIM file to
    /// avoid loading all the vertex buffer data into memory. Useful for loading
    /// the geometry of very large models into a target application.
    /// </summary>
    public class VimGeometryStreamer : IDisposable
    {
        private readonly Stream _stream;
        public BFastBufferReader GeometryBufferReader { get; }
        public VimEntityTableSet TableSet { get; }
        
        /// <summary>
        /// Constructor
        /// </summary>
        private VimGeometryStreamer(
            Stream stream,
            BFastBufferReader geometryBufferReader,
            VimEntityTableSet tableSet)
        {
            _stream = stream;
            GeometryBufferReader = geometryBufferReader;
            TableSet = tableSet;
        }

        public void Dispose()
        {
            _stream.Dispose();
        }

        public static VimGeometryStreamer Create(string vimFilePath)
        {
            return Create(new FileInfo(vimFilePath));
        }

        public static VimGeometryStreamer Create(FileInfo vimFileInfo)
        {
            vimFileInfo.ThrowIfNotExists("VIM file not found. Could not create geometry streamer.");
            var fileStream = vimFileInfo.OpenRead();
            return Create(fileStream, vimFileInfo.FullName);
        }

        public static VimGeometryStreamer Create(Stream vimStream, string vimFilePath)
        {
            var vimNoGeometry = VIM.Open(vimStream, vimFilePath,
                new VimOpenOptions { IncludeGeometry = false, IncludeAssets = false });
            
            var tableSet = vimNoGeometry.GetEntityTableSet();

            var geometryBufferReader = VIM.GetGeometryBufferReader(vimStream);
            
            return new VimGeometryStreamer(vimStream, geometryBufferReader, tableSet);
        }

        // Should be able to group by:
        // - instances (do not combine)
        // - arbitrary element nesting (ex a combination of category/family/type + material)
        // - material
        //
        // (!) Should avoid array limit exceptions
        //

        public IEnumerable<VimStreamedSubmesh> EnumerateSubmeshes()
        {
            // Iterate over all the nodes in the tableset to construct VimStreamedSubmeshes
            var nodeTable = TableSet.NodeTable;
            var nodeCount = nodeTable.RowCount;

            // Initialize the set of streamed submeshes using the node entity table.
            var streamedInstances = new VimStreamedInstance[nodeCount];
            for (var i = 0; i < nodeCount; ++i)
            {
                var instanceIndex = i; // invariant: there is a 1:1 association between node entities and geometry instances
                var elementIndex = nodeTable.Column_ElementIndex[i];

                var streamedInstance = new VimStreamedInstance(this, instanceIndex, elementIndex);
                streamedInstances[i] = streamedInstance;
            }

            // Visit the geometry buffers to populate the streamed submeshes.
            GeometryBufferReader.Seek(); // seek to the root of the geometry buffer.

            var instanceTransforms = ReadInstanceTransforms();
            var instanceCount = instanceTransforms.Length;
            var instanceFlags = VimGeometryData.EnsureInstanceFlags(instanceCount, ReadInstanceFlags());
            var instancesMeshes = ReadInstanceMeshes();

            throw new NotImplementedException("TODO");

            // Steps:
            // - Declare grouping strategy
            // - Bucket each VimGeometryInfo { elementIndex, instanceIndex, [meshIndices] } into the appropriate group
            // - 
        }

        private int[] ReadSubmeshIndexOffsets()
            => ReadBufferData<int>(VimGeometryData.SubmeshIndexOffsetsBufferName);

        private int[] ReadSubmeshMaterials()
            => ReadBufferData<int>(VimGeometryData.SubmeshMaterialsBufferName);

        private int[] ReadMeshSubmeshOffsets()
            => ReadBufferData<int>(VimGeometryData.MeshSubmeshOffsetsBufferName);

        private Vector4[] ReadMaterialColors()
            => ReadBufferData<Vector4>(VimGeometryData.MaterialColorsBufferName);

        private float[] ReadMaterialGlossiness()
            => ReadBufferData<float>(VimGeometryData.MaterialGlossinessBufferName);

        private float[] ReadMaterialSmoothness()
            => ReadBufferData<float>(VimGeometryData.MaterialSmoothnessBufferName);

        private Matrix4x4[] ReadInstanceTransforms()
            => ReadBufferData<Matrix4x4>(VimGeometryData.InstanceTransformsBufferName);

        private ushort[] ReadInstanceFlags()
            => ReadBufferData<ushort>(VimGeometryData.InstanceFlagsBufferName);

        private int[] ReadInstanceParents()
            => ReadBufferData<int>(VimGeometryData.InstanceParentsBufferName);

        private int[] ReadInstanceMeshes()
            => ReadBufferData<int>(VimGeometryData.InstanceMeshesBufferName);

        private T[] ReadBufferData<T>(string bufferName) where T: unmanaged
        {
            GeometryBufferReader.Seek();

            var bufferReader = _stream.GetBFastBufferReaders().FirstOrDefault(br => br.Name == bufferName);
            if (bufferReader == null)
                return Array.Empty<T>();

            return VimGeometryData.ReadBufferData<T>(bufferReader, out _);
        }
    }

    public class VimStreamedInstance
    {
        public VimGeometryStreamer GeometryStreamer { get; }
        public int ElementIndex { get; }
        public int InstanceIndex { get; }
        public List<VimStreamedSubmesh> Submeshes { get; } = new List<VimStreamedSubmesh>();

        public VimStreamedInstance(
            VimGeometryStreamer geometryStreamer,
            int elementIndex,
            int instanceIndex)
        {
            GeometryStreamer = geometryStreamer;
            ElementIndex = elementIndex;
            InstanceIndex = instanceIndex;
        }

        public int? MeshIndex { get; set; }

        public List<int> GetSubmeshIndices()
        {
            throw new NotImplementedException();
        }

        public List<int> GetSubmeshMaterials()
        {
            throw new NotImplementedException();
        }
    }

    public class VimStreamedSubmesh : IVimMesh
    {
        public VimGeometryStreamer VimGeometryStreamer { get; }
        public int ElementIndex { get; }
        public int InstanceIndex { get; }

        public VimStreamedSubmesh(
            VimGeometryStreamer vimGeometryStreamer,
            int instanceIndex,
            int elementIndex)
        {
            VimGeometryStreamer = vimGeometryStreamer;
            InstanceIndex = instanceIndex;
            ElementIndex = elementIndex;
        }

        public int? MeshIndex { get; set; } = null;
        public int? MaterialIndex { get; set; } = null;
        public int FaceCount { get; set; } = 0;

        /// <summary>
        /// Returns the local to world space transform of the submesh.
        /// </summary>
        public Matrix4x4 GetTransform()
        {
            throw new NotImplementedException();
        }

        private Vector3[] _vertices = null;

        /// <summary>
        /// Returns the local-space vertices of the submesh.
        /// </summary>
        public Vector3[] GetVertices()
        {
            if (_vertices == null)
            {
                // TODO - lazily instantiate the vertices.
            }
            return _vertices;
        }


        private int[] _indices = null;

        /// <summary>
        /// Returns the index buffer of the submesh.
        /// </summary>
        public int[] GetIndices()
        {
            if (_indices == null)
            {
                // TODO - lazily instantiate the indices.
            }
            return _indices;
        }
    }
}
