using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.G3d;
using Vim.LinqArray;
using Vim.Math3d;
using Vim.Util;

namespace Vim.Format.ObjectModel
{
    public class ElementGeometryInfo
    {
        public int VertexCount { get; }
        public int FaceCount { get; }
        public int NodeCount { get; }
        public AABox WorldSpaceBoundingBox { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ElementGeometryInfo(int vertexCount, int faceCount, int nodeCount, AABox worldSpaceBoundingBox)
        {
            VertexCount = vertexCount;
            FaceCount = faceCount;
            NodeCount = nodeCount;
            WorldSpaceBoundingBox = worldSpaceBoundingBox;
        }
    }

    /// <summary>
    /// A mapping of { ElementIndex -> [(NodeIndex, GeometryIndex)] }. Also provides information about the element's geometry.
    /// </summary>
    public class ElementGeometryMap : DictionaryOfLists<int, (int NodeIndex, int GeometryIndex)>
    {
        private readonly EntityTableSet _tableSet;
        private readonly G3D _g3d;
        
        public IReadOnlyDictionary<int, ElementGeometryInfo> ElementGeometryInfo { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ElementGeometryMap(FileInfo vimFileInfo, G3D g3d = null)
        {
            _g3d = g3d;
            _tableSet = new EntityTableSet(vimFileInfo, false, Array.Empty<string>(), n => n is TableNames.Node);
            PopulateElementGeometryMap();
            ElementGeometryInfo = GetElementGeometryInfoMap(this);
        }

        /// <summary>
        /// Populates the mapping { ElementIndex -> [(NodeIndex, GeometryIndex)] }
        /// </summary>
        private void PopulateElementGeometryMap()
        {
            if (!(_tableSet.NodeTable is NodeTable nodeTable))
                return;

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
                    // INVARIANT: there is a 1:1 relationship between Node entities and instances in the g3d buffer.
                    var instanceIndex = nodeIndex;

                    if (!TryGetGeometryIndex(_g3d, instanceIndex, out var geometryIndex))
                        continue; // Skip nodes with no geometry.

                    Add(elementIndex, (nodeIndex, geometryIndex));
                }
            }
        }

        private static bool TryGetGeometryIndex(G3D g3d, int instanceIndex, out int geometryIndex)
        {
            geometryIndex = -1;
            geometryIndex = g3d.InstanceMeshes.ElementAtOrDefault(instanceIndex, -1);
            return geometryIndex >= 0;
        }

        private static Dictionary<int, ElementGeometryInfo> GetElementGeometryInfoMap(ElementGeometryMap egm)
        {
            var result = new Dictionary<int, ElementGeometryInfo>();

            foreach (var kv in egm)
            {
                var elementIndex = kv.Key;
                var list = kv.Value;

                var nodeCount = list.Count;
                var vertexCount = 0;
                var faceCount = 0;
                var bb = new AABox(Vector3.MaxValue, Vector3.MinValue); // world space element bounding box

                // Aggregate the geometry info
                foreach (var (nodeIndex, geometryIndex) in list)
                {
                    // INVARIANT: there is a 1:1 relationship between Node entities and instances in the g3d buffer.
                    var instanceIndex = nodeIndex;

                    if (!egm.TryGetTransformedGeometryInfo(
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

                result[elementIndex] = new ElementGeometryInfo(vertexCount, faceCount, nodeCount, bb);
            }

            return result;
        }

        public bool TryGetTransformedGeometryInfo(
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

            var g3dMesh = _g3d.Meshes.ElementAtOrDefault(geometryIndex);
            if (g3dMesh == null)
                return false;

            vertexCount = g3dMesh.NumVertices;
            faceCount = g3dMesh.NumFaces;

            var transform = _g3d.InstanceTransforms.ElementAtOrDefault(instanceIndex, Matrix4x4.Identity);

            // Calculate the world-space bounding box of the mesh.
            worldSpaceBb = AABox.Create(g3dMesh.Vertices.ToArray().Select(v => v.Transform(transform)));

            return true;
        }
    }
}
