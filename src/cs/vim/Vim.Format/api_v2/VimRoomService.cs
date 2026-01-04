using System;
using System.Collections.Generic;
using System.Linq;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim.Format
{
    public class VimElementInRoom
    {
        public int ElementIndex { get; }
        public int RoomIndex { get; }

        public VimElementInRoom(int elementIndex, int roomIndex)
        {
            ElementIndex = elementIndex;
            RoomIndex = roomIndex;
        }
    }

    public static class VimRoomService
    {
        public delegate bool GeometricElementInfoFilter(VimElementGeometryInfo elementInfo);

        public static VimElementInRoom[] ComputeElementsInRoom(VIM vim, GeometricElementInfoFilter geometricElementInfoFilter)
        {
            var tableSet = vim.GetEntityTableSet();

            // var roomElementIndices = new HashSet<int>(dm.RoomElementIndex.ToEnumerable());
            var roomElementIndices = new HashSet<int>(tableSet.RoomTable.Column_ElementIndex);
            if (roomElementIndices.Count == 0)
                return Array.Empty<VimElementInRoom>(); // no rooms found.

            var elementGeometryInfo = vim.GetElementGeometryInfoList();

            var roomGeometryCollection = elementGeometryInfo
                .AsParallel()
                .Where(g =>
                {
                    var elementIndex = g.ElementIndex;
                    return roomElementIndices.Contains(elementIndex);
                })
                .Select(g =>
                {
                    // ASSUMPTION: Rooms are typically represented by a single geometric node, so take the first item in the group.
                    var worldSpaceMesh = g.GetWorldSpaceMeshes(vim.GeometryData).FirstOrDefault();
                    var roomElementIndex = g.ElementIndex;

                    var roomIndexFound = tableSet.ElementIndexMaps.RoomIndexFromElementIndex.TryGetValue(roomElementIndex, out var roomIndex);
                    roomIndex = roomIndexFound ? roomIndex : -1;

                    return new RoomGeometry(roomIndex, worldSpaceMesh);
                })
                .ToArray();

            if (roomGeometryCollection.Length == 0)
                return Array.Empty<VimElementInRoom>(); // no rooms with geometry found.

            var filteredElementGeometricNodes = elementGeometryInfo
                .AsParallel()
                .Where(g =>
                {
                    // Filter the elements
                    if (!geometricElementInfoFilter(g))
                        return false;

                    // Ignore room elements.
                    if (roomElementIndices.Contains(g.ElementIndex))
                        return false;

                    return true;
                }).Select(g =>
                {
                    var elementIndex = g.ElementIndex;

                    // Determine whether the element geometry's bounding box center is contained in one of the rooms.
                    var boundingBox = g.WorldSpaceBoundingBox;
                    var boxCenter = boundingBox.Center;

                    foreach (var roomGeometry in roomGeometryCollection)
                    {
                        // Broad-phase bounding box check.
                        var containmentType = roomGeometry.AABox.Contains(boundingBox);
                        if (containmentType == ContainmentType.Disjoint)
                            continue;

                        // Refined point-in-mesh check
                        if (roomGeometry.ContainsPoint(boxCenter))
                            return new VimElementInRoom(elementIndex, roomGeometry.RoomIndex); // early return on the first room which contains the bottom center of the box.
                    }

                    return null;
                }).Where(eir => eir != null)
                .ToArray();

            return filteredElementGeometricNodes;
        }

        private class RoomGeometry
        {
            private readonly VimMeshData _vimMeshData;

            public int RoomIndex { get; }
            public AABox AABox { get; }

            public RoomGeometry(int roomIndex, VimMeshData vimMeshData)
            {
                RoomIndex = roomIndex;
                _vimMeshData = vimMeshData;
                AABox = AABox.Create(_vimMeshData.GetVertices());
            }

            public bool ContainsPoint(Vector3 point)
            {
                var intersections = 0;
                var indices = _vimMeshData.GetIndices();
                var vertices = _vimMeshData.GetVertices();
                var ray = new Ray(point, Vector3.UnitZ);

                for (var i = 0; i < indices.Length; i += 3)
                {
                    var v0 = vertices[indices[i]];
                    var v1 = vertices[indices[i + 1]];
                    var v2 = vertices[indices[i + 2]];

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
