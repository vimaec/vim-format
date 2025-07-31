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
    /// <summary>
    /// Represents the association between an element and its geometry.
    /// Note: one element may have more than one ElementGeometry record if it is composed of multiple nodes and geometries.
    /// </summary>
    public class ElementGeometry
    {
        public int ElementIndex { get; }
        public int NodeIndex { get; }
        public int InstanceIndex => NodeIndex; // INVARIANT: there is a 1:1 relationship between Node entities and instances in the g3d buffer.
        public int GeometryIndex { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ElementGeometry(int elementIndex, int nodeIndex, int geometryIndex)
        {
            ElementIndex = elementIndex;
            NodeIndex = nodeIndex;
            GeometryIndex = geometryIndex;
        }

        // TODO: compute bounding box per collection of element geometry so we only need it once.
    }

    /// <summary>
    /// A mapping of { ElementIndex -> [(NodeIndex, GeometryIndex)] }
    /// </summary>
    public class ElementGeometryMap : DictionaryOfLists<int, ElementGeometry>
    {
        private readonly EntityTableSet _tableSet;

        public G3D G3d { get; }
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public ElementGeometryMap(FileInfo vimFileInfo, G3D g3d = null)
        {
            _tableSet = new EntityTableSet(vimFileInfo, false, Array.Empty<string>(), n => n is TableNames.Node);

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

                    if (!TryGetGeometryIndex(g3d, instanceIndex, out var geometryIndex))
                        continue; // Skip nodes with no geometry.

                    Add(elementIndex, new ElementGeometry(elementIndex, nodeIndex, geometryIndex));
                }
            }
        }

        private static bool TryGetGeometryIndex(G3D g3d, int instanceIndex, out int geometryIndex)
        {
            geometryIndex = -1;
            geometryIndex = g3d.InstanceMeshes.ElementAtOrDefault(instanceIndex, -1);
            return geometryIndex >= 0;
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

            var g3dMesh = G3d.Meshes.ElementAtOrDefault(geometryIndex);
            if (g3dMesh == null)
                return false;

            vertexCount = g3dMesh.NumVertices;
            faceCount = g3dMesh.NumFaces;

            var transform = G3d.InstanceTransforms.ElementAtOrDefault(instanceIndex, Matrix4x4.Identity);

            // Calculate the world-space bounding box of the mesh.
            worldSpaceBb = AABox.Create(g3dMesh.Vertices.ToArray().Select(v => v.Transform(transform)));

            return true;
        }

        public Dictionary<int, AABox> GetElementWorldSpaceBoundingBoxes()
        {

        }
    }
}
