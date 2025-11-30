using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vim.Math3d;
using Vim.Util;

namespace Vim.Format.api_v2
{
    /// <summary>
    /// Represents the geometric information of an element.
    /// </summary>
    public class VimElementGeometryInfo
    {
        public int ElementIndex { get; }

        public int VertexCount { get; set; }

        public int FaceCount { get; set; }

        public AABox WorldSpaceBoundingBox { get; set; } = AABox.Empty;

        public List<(int InstanceIndex, int MeshIndex)> InstanceAndMeshIndices { get; } = new List<(int InstanceIndex, int MeshIndex)>();

        public int NodeCount
            => InstanceAndMeshIndices.Count;

        public bool HasMesh
            => FaceCount > 0;

        /// <summary>
        /// Constructor
        /// </summary>
        public VimElementGeometryInfo(int elementIndex)
        {
            ElementIndex = elementIndex;
        }

        /// <summary>
        /// Returns the world-space meshes associated with this element.
        /// </summary>
        public IEnumerable<VimMeshData> GetWorldSpaceMeshes(VimGeometryData geometryData)
        {
            foreach (var tuple in InstanceAndMeshIndices)
            {
                var (nodeIndex, meshIndex) = tuple;

                if (meshIndex < 0)
                    continue;

                if (!geometryData.TryGetTransformedMesh(nodeIndex, meshIndex, out var vimMeshData))
                    continue;

                yield return vimMeshData;
            };
        }

        /// <summary>
        /// Returns a 1:1 list mapping Element -> VimElementGeometryInfo based on the given VIM file.
        /// </summary>
        public static VimElementGeometryInfo[] GetElementGeometryInfoList(
            FileInfo vimFileInfo,
            VimEntityTableSet tableSetWithNodeAndElement = null)
        {
            vimFileInfo.ThrowIfNotExists("Could not get VIM element geometry map.");

            var tableSet = tableSetWithNodeAndElement ??
                VimEntityTableSet.GetEntityTableSetByTableName(
                    vimFileInfo,
                    VimEntityTableNames.Node,
                    VimEntityTableNames.Element
                );

            var vimGeometryData = VIM.GetGeometryData(vimFileInfo);

            return GetElementGeometryInfoList(tableSet, vimGeometryData);
        }

        /// <summary>
        /// Returns a 1:1 list mapping Element -> VimElementGeometryInfo
        /// </summary>
        public static VimElementGeometryInfo[] GetElementGeometryInfoList(
            VimEntityTableSet tableSet,
            VimGeometryData vimGeometryData)
        {
            var elementCount = tableSet.ElementTable?.RowCount ?? 0;

            var result = new VimElementGeometryInfo[elementCount];

            // Initialize the set of items with new geometry info.
            for (var elementIndex = 0; elementIndex < result.Length; ++elementIndex)
                result[elementIndex] = new VimElementGeometryInfo(elementIndex);

            if (!(tableSet.NodeTable is NodeTable nodeTable))
                return result;

            // Calculate the element associations to nodes and geometry.
            // NOTE: one element may have more than one node and associated geometry

            var elementIndexGroups = nodeTable
                .Column_ElementIndex
                .AsParallel()
                .Select((elementIndex, nodeIndex) => (ElementIndex: elementIndex, NodeIndex: nodeIndex))
                .GroupBy(t => t.ElementIndex)
                .Where(g => g.Key >= 0);

            foreach (var group in elementIndexGroups)
            {
                foreach (var (elementIndex, nodeIndex) in group)
                {
                    if (elementIndex < 0 || elementIndex >= result.Length)
                        continue;

                    // INVARIANT: there is a 1:1 relationship between Node entities and instances in the VimGeometryData.
                    var instanceIndex = nodeIndex;

                    if (!TryGetMeshIndex(vimGeometryData, instanceIndex, out var meshIndex))
                        continue; // Skip nodes with no geometry.

                    result[elementIndex].InstanceAndMeshIndices.Add((nodeIndex, meshIndex));
                }
            }

            // Calculate the element geometry in parallel.

            Parallel.For(0, result.Length, elementIndex =>
            {
                var item = result[elementIndex];
                var list = item.InstanceAndMeshIndices;
                var vertexCount = 0;
                var faceCount = 0;
                var bb = new AABox(Vector3.MaxValue, Vector3.MinValue); // world space element bounding box

                // Aggregate the geometry info
                foreach (var (nodeIndex, meshIndex) in list)
                {
                    // INVARIANT: there is a 1:1 relationship between Node entities and instances in the g3d buffer.
                    var instanceIndex = nodeIndex;

                    if (!TryGetTransformedGeometryInfo(
                        vimGeometryData,
                        instanceIndex,
                        meshIndex,
                        out var meshVertexCount,
                        out var meshFaceCount,
                        out var instanceBb))
                    {
                        continue;
                    }

                    // Aggregate the ElementGeometry data
                    vertexCount += meshVertexCount;
                    faceCount += meshFaceCount;
                    bb = bb.Merge(instanceBb);
                }

                item.VertexCount = vertexCount;
                item.FaceCount = faceCount;
                item.WorldSpaceBoundingBox = bb;
            });

            return result;
        }

        private static bool TryGetMeshIndex(VimGeometryData vimGeometryData, int instanceIndex, out int meshIndex)
        {
            meshIndex = vimGeometryData.InstanceMeshes.ElementAtOrDefault(instanceIndex, -1);
            return meshIndex >= 0;
        }

        private static bool TryGetTransformedGeometryInfo(
            VimGeometryData vimGeometryData,
            int instanceIndex,
            int meshIndex,
            out int vertexCount,
            out int faceCount,
            out AABox worldSpaceBb)
        {
            vertexCount = 0;
            faceCount = 0;
            worldSpaceBb = AABox.Empty;

            if (meshIndex < 0)
                return false;

            if (!vimGeometryData.TryGetVimMeshView(meshIndex, out var meshData))
                return false;

            vertexCount = meshData.VertexCount;
            faceCount = meshData.FaceCount;

            var transform = vimGeometryData.InstanceTransforms.ElementAtOrDefault(instanceIndex, Matrix4x4.Identity);

            // Calculate the world-space bounding box of the mesh.
            worldSpaceBb = AABox.Create(meshData.GetVertices().Select(v => v.Transform(transform)));

            return true;
        }
    }
}