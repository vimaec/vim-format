using System;
using Vim.Math3d;

namespace Vim.Format
{
    public static class RoomService
    {

        /// <summary>
        /// 
        /// </summary>
        public class RoomGeometry
        {
            public int RoomKey { get; }
            public Vector3[] Vertices { get; }
            public int[] Indices { get; }

            public RoomGeometry(int roomKey, Vector3[] vertices, int[] indices)
            {
                RoomKey = roomKey;
                Vertices = vertices;
                Indices = indices;
            }

            public bool ContainsPoint(Vector3 point)
            {
                var intersections = 0;

                var ray = new Ray(point, Vector3.UnitZ);

                for (var i = 0; i < Indices.Length; i += 3)
                {
                    var v0 = Vertices[Indices[i]];
                    var v1 = Vertices[Indices[i + 1]];
                    var v2 = Vertices[Indices[i + 2]];

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
