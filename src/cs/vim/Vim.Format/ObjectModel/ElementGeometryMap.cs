using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vim.G3d;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim.Format.ObjectModel
{
    /// <summary>
    /// Represents the geometric information of an element.
    /// </summary>
    public class ElementGeometryInfo
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
        public ElementGeometryInfo(int elementIndex)
        {
            ElementIndex = elementIndex;
        }
    }

    /// <summary>
    /// A 1:1 list mapping ElementIndex -> ElementGeometryInfo
    /// </summary>
    public class ElementGeometryMap : IReadOnlyList<ElementGeometryInfo>
    {
        private readonly ElementGeometryInfo[] _items;

        public IEnumerator<ElementGeometryInfo> GetEnumerator()
        {
            foreach (var item in _items)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public int Count
            => _items.Length;

        public ElementGeometryInfo this[int index]
            => _items[index];

        public ElementGeometryInfo ElementAtOrDefault(int index)
            => _items.ElementAtOrDefault(index);

        /// <summary>
        /// Constructor.
        /// </summary>
        public ElementGeometryMap(FileInfo vimFileInfo, G3D g3d = null)
        {
            g3d = g3d ?? vimFileInfo.GetGeometry();

            var tableSet = new EntityTableSet(
                vimFileInfo,
                Array.Empty<string>(),
                n => n is TableNames.Node || n is TableNames.Element);

            var elementCount = tableSet.ElementTable?.RowCount ?? 0;

            _items = new ElementGeometryInfo[elementCount];

            for (var elementIndex = 0; elementIndex < _items.Length; ++elementIndex)
                _items[elementIndex] = new ElementGeometryInfo(elementIndex);

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
                    if (elementIndex < 0 || elementIndex >= _items.Length)
                        continue;

                    // INVARIANT: there is a 1:1 relationship between Node entities and instances in the g3d buffer.
                    var instanceIndex = nodeIndex;

                    if (!TryGetGeometryIndex(g3d, instanceIndex, out var geometryIndex))
                        continue; // Skip nodes with no geometry.

                    _items[elementIndex].NodeAndGeometryIndices.Add((nodeIndex, geometryIndex));
                }
            }

            // Calculate the element geometry in parallel.

            Parallel.For(0, _items.Length, elementIndex =>
            {
                var item = _items[elementIndex];
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
                            g3d,
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
        public ElementGeometryMap(IEnumerable<ElementGeometryInfo> items)
        {
            _items = items.ToArray();
        }

        private static bool TryGetGeometryIndex(G3D g3d, int instanceIndex, out int geometryIndex)
        {
            geometryIndex = -1;
            geometryIndex = g3d.InstanceMeshes.ElementAtOrDefault(instanceIndex, -1);
            return geometryIndex >= 0;
        }

        private static bool TryGetTransformedGeometryInfo(
            G3D g3d,
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

            var g3dMesh = g3d.Meshes.ElementAtOrDefault(geometryIndex);
            if (g3dMesh == null)
                return false;

            vertexCount = g3dMesh.NumVertices;
            faceCount = g3dMesh.NumFaces;

            var transform = g3d.InstanceTransforms.ElementAtOrDefault(instanceIndex, Matrix4x4.Identity);

            // Calculate the world-space bounding box of the mesh.
            worldSpaceBb = AABox.Create(g3dMesh.Vertices.ToArray().Select(v => v.Transform(transform)));

            return true;
        }
    }
}
