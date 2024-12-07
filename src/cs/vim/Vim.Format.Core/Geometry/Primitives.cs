using System.Collections.Generic;
using System.Linq;
using Vim.G3d;
using Vim.Math3d;

namespace Vim.Format.Geometry
{
    public static class Primitives
    {
        public static IMesh TriMesh(IEnumerable<GeometryAttribute> attributes)
            => attributes.Where(x => x != null).ToIMesh();

        public static IMesh TriMesh(params GeometryAttribute[] attributes)
            => TriMesh(attributes.AsEnumerable());

        public static IMesh TriMesh(
            this IList<Vector3> vertices,
            IList<int> indices = null,
            IList<Vector2> uvs = null,
            IList<Vector4> colors = null,
            IList<int> materials = null,
            IList<int> submeshMaterials = null)
            => TriMesh(
                vertices?.ToPositionAttribute(),
                indices?.ToIndexAttribute(),
                uvs?.ToVertexUvAttribute(),
                materials?.ToFaceMaterialAttribute(),
                colors?.ToVertexColorAttribute(),
                submeshMaterials?.ToSubmeshMaterialAttribute()
            );

        public static IMesh TriMesh(this IList<Vector3> vertices, IList<int> indices = null, params GeometryAttribute[] attributes)
            => new GeometryAttribute[] {
                vertices?.ToPositionAttribute(),
                indices?.ToIndexAttribute(),
            }.Concat(attributes).ToIMesh();

        public static IMesh Cube
        {
            get
            {
                var vertices = new[] {
                    // front
                    new Vector3(-0.5f, -0.5f,  0.5f),
                    new Vector3(0.5f, -0.5f,  0.5f),
                    new Vector3(0.5f,  0.5f,  0.5f),
                    new Vector3(-0.5f,  0.5f,  0.5f),
                    // back
                    new Vector3(-0.5f, -0.5f, -0.5f),
                    new Vector3(0.5f, -0.5f, -0.5f),
                    new Vector3(0.5f,  0.5f, -0.5f),
                    new Vector3(-0.5f,  0.5f, -0.5f)
                };

                var indices = new[] {
                    // front
                    0, 1, 2,
                    2, 3, 0,
                    // right
                    1, 5, 6,
                    6, 2, 1,
                    // back
                    7, 6, 5,
                    5, 4, 7,
                    // left
                    4, 0, 3,
                    3, 7, 4,
                    // bottom
                    4, 5, 1,
                    1, 0, 4,
                    // top
                    3, 2, 6,
                    6, 7, 3
                };

                return vertices.TriMesh(indices);
            }
        }

        public static IMesh CubeFaceted
        {
            get
            {
                var cube = Cube;
                return cube.Indices.Select(i => cube.Vertices[i]).ToArray().TriMesh(cube.Indices.Count.Range());
            }
        }

        /// <summary>
        /// Returns the index buffer of a quad mesh strip.
        /// Returns an empty array if either numRowPoints or numPointsPerRow is less than 2.
        /// </summary>
        public static List<int> QuadMeshStripIndicesFromPointRows(
            int numPointRows,
            int numPointsPerRow,
            bool clockwise = false)
        {
            // A quad(ABCD) is defined as 4 indices, counter clock-wise:
            //
            //     col    col
            // row  D------C     quad(ABCD) = (counter-clockwise) { A, B, C, D }
            //      |t1 /  |   triangle(t0) = (counter-clockwise) { A, B, C }
            //      |  / t0|   triangle(t1) = (counter-clockwise) { A, C, D }
            // row  A------B

            var indices = new List<int>(); // 4 indices per quad.
            for (var rowIndex = 0; rowIndex < numPointRows - 1; ++rowIndex)
            {
                for (var colIndex = 0; colIndex < numPointsPerRow - 1; ++colIndex)
                {
                    // The vertices will all be inserted in a flat list in the vertex buffer
                    // [ ...row0, ...row1, ...row2, ..., ...rowN]
                    //
                    //                   colIndex
                    //                      ...    ...
                    //                       |      |
                    // rowIndex + 1: [... ---D------C--- ...]
                    //                       |t1 /  |
                    //                       |  / t0|
                    // rowIndex:     [... ---A------B--- ...]
                    //                       |      |
                    //                      ...    ...
                    //
                    // rowSize:      |<-----numColumns----->|
                    //

                    var A = colIndex + rowIndex * numPointsPerRow;
                    var B = A + 1;
                    var D = colIndex + (rowIndex + 1) * numPointsPerRow;
                    var C = D + 1;

                    if (clockwise)
                    {
                        indices.Add(D);
                        indices.Add(C);
                        indices.Add(B);
                        indices.Add(A);
                    }
                    else
                    {
                        indices.Add(A);
                        indices.Add(B);
                        indices.Add(C);
                        indices.Add(D);
                    }
                }
            }

            return indices;
        }

        public static int[] TriMeshCylinderCapIndices(int numEdgeVertices)
        {
            // Example cap where numEdgeVertices is 6:
            //
            // (!) It is assumed that vertex 0 is at the center of the cap
            // and that this center vertex is omitted from the numEdgeVertices count.
            //
            //      3<------2
            //     /  \t1 /  ^                t0 = (O, 1, 2)
            //    v t2 \ / t0 \               t1 = (O, 2, 3)
            //   4------0------1 <---- start  t2 = (O, 3, 4)
            //    \ t3 / \ t5 ^               t3 = (O, 4, 5)
            //     v  /t4 \  /                t4 = (O, 5, 6)
            //      5------>6 <---- end       t5 = (O, 6, 1) <-- special case.

            var center = 0;
            var indices = new List<int>();

            var numTriangles = numEdgeVertices;

            // Do all the triangles except the last one.
            for (var triangle = 0; triangle < numTriangles - 1; ++triangle)
            {
                var index0 = center; // 0
                var index1 = triangle + 1;
                var index2 = index1 + 1;

                indices.Add(index0);
                indices.Add(index1);
                indices.Add(index2);
            }

            // The last triangle loops back onto the first edge vertex.
            var lastTriangleIndex0 = center;
            var lastTriangleIndex1 = numEdgeVertices;
            var lastTriangleIndex2 = 1;
            indices.Add(lastTriangleIndex0);
            indices.Add(lastTriangleIndex1);
            indices.Add(lastTriangleIndex2);

            return indices.ToArray();
        }
    }
}
