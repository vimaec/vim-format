using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vim.Math3d;
using Vim.Util;

namespace Vim.Format.api_v2
{
    /// <summary>
    /// A 1:1 list mapping Element -> VimElementGeometryInfo
    /// </summary>
    public class VimElementGeometryMap : IReadOnlyList<VimElementGeometryInfo>
    {
        private readonly VimElementGeometryInfo[] _elementGeometryInfos;

        public IEnumerator<VimElementGeometryInfo> GetEnumerator()
        {
            foreach (var item in _elementGeometryInfos)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public int Count
            => _elementGeometryInfos.Length;

        public VimElementGeometryInfo this[int index]
            => _elementGeometryInfos[index];

        public VimElementGeometryInfo ElementAtOrDefault(int index)
            => _elementGeometryInfos.ElementAtOrDefault(index);

        /// <summary>
        /// Constructor
        /// </summary>
        public VimElementGeometryMap(VimEntityTableSet tableSet, VimGeometryData vimGeometryData)
        {
            var elementCount = tableSet.ElementTable?.RowCount ?? 0;

            _elementGeometryInfos = new VimElementGeometryInfo[elementCount];

            // Initialize the set of items with new geometry info.
            for (var elementIndex = 0; elementIndex < _elementGeometryInfos.Length; ++elementIndex)
                _elementGeometryInfos[elementIndex] = new VimElementGeometryInfo(elementIndex);

            if (!(tableSet.NodeTable is NodeTable nodeTable))
                return;

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
                    if (elementIndex < 0 || elementIndex >= _elementGeometryInfos.Length)
                        continue;

                    // INVARIANT: there is a 1:1 relationship between Node entities and instances in the VimGeometryData.
                    var instanceIndex = nodeIndex;

                    if (!TryGetGeometryIndex(vimGeometryData, instanceIndex, out var geometryIndex))
                        continue; // Skip nodes with no geometry.

                    _elementGeometryInfos[elementIndex].NodeAndGeometryIndices.Add((nodeIndex, geometryIndex));
                }
            }

            // Calculate the element geometry in parallel.

            Parallel.For(0, _elementGeometryInfos.Length, elementIndex =>
            {
                var item = _elementGeometryInfos[elementIndex];
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
        }

        /// <summary>
        /// Test constructor
        /// </summary>
        public VimElementGeometryMap(IEnumerable<VimElementGeometryInfo> items)
        {
            _elementGeometryInfos = items.ToArray();
        }

        /// <summary>
        /// Returns a VimElementGeometryMap based on the givem VIM file.
        /// </summary>
        public static VimElementGeometryMap GetElementGeometryMap(
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

            return new VimElementGeometryMap(tableSet, vimGeometryData);
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

            if (!vimGeometryData.TryGetVimMeshData(geometryIndex, out var meshData))
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