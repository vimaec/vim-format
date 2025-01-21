using System;
using System.Collections.Generic;
using System.Linq;
using Vim.Format.Geometry;
using Vim.Format.ObjectModel;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim.Format
{
    public class ElementInRoom
    {
        public int ElementIndex { get; }
        public int RoomIndex { get; }

        public ElementInRoom(int elementIndex, int roomIndex)
        {
            ElementIndex = elementIndex;
            RoomIndex = roomIndex;
        }
    }

    public static class RoomService
    {
        public delegate bool ElementInfoFilter(ElementInfo elementInfo);

        public static ElementInRoom[] ComputeElementsInRoom(VimScene vim, ElementInfoFilter elementInfoFilter)
        {
            var dm = vim.DocumentModel;

            var roomElementIndices = new HashSet<int>(dm.RoomElementIndex.ToEnumerable());
            if (roomElementIndices.Count == 0)
                return Array.Empty<ElementInRoom>(); // no rooms found.

            // Collect the bounding boxes of geometric elements
            var geometricNodesGroupedByElementIndex = vim.VimNodes
                .Where(n => n.HasMesh)
                .GroupBy(n => n.ElementIndex)
                .ToArray();

            var roomGeometryCollection = geometricNodesGroupedByElementIndex
                .AsParallel()
                .Where(g =>
                {
                    var elementIndex = g.Key;
                    return roomElementIndices.Contains(elementIndex);
                })
                .Select(g =>
                {
                    // ASSUMPTION: Rooms are typically represented by a single geometric node, so take the first item in the group.
                    var worldSpaceMesh = g.First().TransformedMesh();
                    var roomElementIndex = g.Key;

                    var roomIndexFound = dm.ElementIndexMaps.RoomIndexFromElementIndex.TryGetValue(roomElementIndex, out var roomIndex);
                    roomIndex = roomIndexFound ? roomIndex : -1;

                    return new RoomGeometry(roomIndex, worldSpaceMesh.Vertices.ToArray(), worldSpaceMesh.Indices.ToArray());
                })
                .ToArray();

            if (roomGeometryCollection.Length == 0)
                return Array.Empty<ElementInRoom>(); // no rooms with geometry found.

            var filteredElementGeometricNodes = geometricNodesGroupedByElementIndex
                .AsParallel()
                .Where(g =>
                {
                    // Filter the elements
                    var elementInfo = g.First();
                    if (!elementInfoFilter(elementInfo))
                        return false;

                    // Ignore room elements.
                    if (roomElementIndices.Contains(elementInfo.ElementIndex))
                        return false;

                    return true;
                }).Select(g =>
                {
                    var elementIndex = g.Key;

                    // Determine whether the element geometry's bounding box center is contained in one of the rooms.
                    var boundingBox = g.Select(n => n.TransformedBoundingBox()).Aggregate((acc, cur) => acc.Merge(cur));
                    var boxCenter = boundingBox.Center;

                    foreach (var roomGeometry in roomGeometryCollection)
                    {
                        // Broad-phase bounding box check.
                        var containmentType = roomGeometry.AABox.Contains(boundingBox);
                        if (containmentType == ContainmentType.Disjoint)
                            continue;

                        // Refined point-in-mesh check
                        if (roomGeometry.ContainsPoint(boxCenter))
                            return new ElementInRoom(elementIndex, roomGeometry.RoomIndex); // early return on the first room which contains the bottom center of the box.
                    }

                    return null;
                }).Where(eir => eir != null)
                .ToArray();

            return filteredElementGeometricNodes;
        }

        private class RoomGeometry
        {
            private readonly Vector3[] _vertices;
            private readonly int[] _indices;

            public int RoomIndex { get; }
            public AABox AABox { get; }

            public RoomGeometry(int roomIndex, Vector3[] vertices, int[] indices)
            {
                RoomIndex = roomIndex;
                _vertices = vertices;
                _indices = indices;
                AABox = AABox.Create(vertices);
            }

            public bool ContainsPoint(Vector3 point)
            {
                var intersections = 0;

                var ray = new Ray(point, Vector3.UnitZ);

                for (var i = 0; i < _indices.Length; i += 3)
                {
                    var v0 = _vertices[_indices[i]];
                    var v1 = _vertices[_indices[i + 1]];
                    var v2 = _vertices[_indices[i + 2]];

                    var triangle = new Triangle(v0, v1, v2);

                    if (ray.Intersects(triangle) != null)
                    {
                        intersections++;
                    }
                }

                return intersections % 2 == 1; // Inside if odd intersections
            }
        }
    }
}
