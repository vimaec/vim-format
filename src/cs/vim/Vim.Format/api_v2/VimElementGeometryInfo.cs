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

        public List<(int NodeIndex, int GeometryIndex)> NodeAndGeometryIndices { get; } = new List<(int NodeIndex, int GeometryIndex)>();

        public int NodeCount
            => NodeAndGeometryIndices.Count;

        public bool HasGeometry
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
            foreach (var tuple in NodeAndGeometryIndices)
            {
                var (nodeIndex, geometryIndex) = tuple;

                if (geometryIndex < 0)
                    continue;

                if (!geometryData.TryGetTransformedMesh(nodeIndex, geometryIndex, out var vimMeshData))
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

                    if (!TryGetGeometryIndex(vimGeometryData, instanceIndex, out var geometryIndex))
                        continue; // Skip nodes with no geometry.

                    result[elementIndex].NodeAndGeometryIndices.Add((nodeIndex, geometryIndex));
                }
            }

            // Calculate the element geometry in parallel.

            Parallel.For(0, result.Length, elementIndex =>
            {
                var item = result[elementIndex];
                var list = item.NodeAndGeometryIndices;
                var vertexCount = 0;
                var faceCount = 0;
                var bb = new AABox(Vector3.MaxValue, Vector3.MinValue); // world space element bounding box

                // Aggregate the geometry info
                foreach (var (nodeIndex, geometryIndex) in list)
                {
                    // INVARIANT: there is a 1:1 relationship between Node entities and instances in the g3d buffer.
                    var instanceIndex = nodeIndex;

                    if (!TryGetTransformedGeometryInfo(
                        vimGeometryData,
                        instanceIndex,
                        geometryIndex,
                        out var geometryVertexCount,
                        out var geometryFaceCount,
                        out var nodeBb))
                    {
                        continue;
                    }

                    // Aggregate the ElementGeometry data
                    vertexCount += geometryVertexCount;
                    faceCount += geometryFaceCount;
                    bb = bb.Merge(nodeBb);
                }

                item.VertexCount = vertexCount;
                item.FaceCount = faceCount;
                item.WorldSpaceBoundingBox = bb;
            });

            return result;
        }

        private static bool TryGetGeometryIndex(VimGeometryData vimGeometryData, int instanceIndex, out int geometryIndex)
        {
            geometryIndex = -1;
            geometryIndex = vimGeometryData.InstanceMeshes.ElementAtOrDefault(instanceIndex, -1);
            return geometryIndex >= 0;
        }

        private static bool TryGetTransformedGeometryInfo(
            VimGeometryData vimGeometryData,
            int instanceIndex,
            int geometryIndex,
            out int vertexCount,
            out int faceCount,
            out AABox worldSpaceBb)
        {
            vertexCount = 0;
            faceCount = 0;
            worldSpaceBb = AABox.Empty;

            if (geometryIndex < 0)
                return false;

            if (!vimGeometryData.TryGetVimMeshView(geometryIndex, out var meshData))
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