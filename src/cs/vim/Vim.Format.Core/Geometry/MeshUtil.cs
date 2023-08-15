using System;
using System.Collections.Generic;
using System.Linq;
using Vim.G3d;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim.Format.Geometry
{
    public static class MeshUtil
    {
        public static IMesh CreateTriMesh(IEnumerable<GeometryAttribute> attributes)
            => attributes.Where(x => x != null).ToIMesh();

        public static IMesh CreateTriMesh(params GeometryAttribute[] attributes)
            => CreateTriMesh(attributes.AsEnumerable());

        public static IMesh CreateTriMesh(
            this IArray<Vector3> vertices,
            IArray<int> indices = null,
            IArray<Vector2> uvs = null,
            IArray<Vector4> colors = null,
            IArray<int> materials = null,
            IArray<int> submeshMaterials = null)
            => CreateTriMesh(
                vertices?.ToPositionAttribute(),
                indices?.ToIndexAttribute(),
                uvs?.ToVertexUvAttribute(),
                materials?.ToFaceMaterialAttribute(),
                colors?.ToVertexColorAttribute(),
                submeshMaterials?.ToSubmeshMaterialAttribute()
            );

        public static IMesh CreateQuadMesh(params GeometryAttribute[] attributes)
            => CreateQuadMesh(attributes.AsEnumerable());

        public static IMesh CreateQuadMesh(this IEnumerable<GeometryAttribute> attributes)
            => new QuadMesh(attributes.Where(x => x != null)).ToTriMesh();

        public static IMesh CreateQuadMesh(this IArray<Vector3> vertices, IArray<int> indices = null, IArray<Vector2> uvs = null, IArray<int> materials = null, IArray<int> objectIds = null)
            => CreateQuadMesh(
                vertices.ToPositionAttribute(),
                indices?.ToIndexAttribute(),
                uvs?.ToVertexUvAttribute(),
                materials?.ToFaceMaterialAttribute()
            );

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
                }.ToIArray();

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
                }.ToIArray();

                return vertices.CreateTriMesh(indices);
            }
        }

        public static IMesh CubeFaceted
        {
            get
            {
                var cube = Cube;
                return cube.Indices.Select(i => cube.Vertices[i]).CreateTriMesh(cube.Indices.Count.Range());
            }
        }

        /// <summary>
        /// Computes the indices of a quad mesh strip.
        /// </summary>
        public static IArray<int> ComputeQuadMeshStripIndices(int usegs, int vsegs, bool wrapUSegs = false, bool wrapVSegs = false)
        {
            var indices = new List<int>();

            var maxUSegs = wrapUSegs ? usegs : usegs + 1;
            var maxVSegs = wrapVSegs ? vsegs : vsegs + 1;

            for (var i = 0; i < vsegs; ++i)
            {
                var rowA = i * maxUSegs;
                var rowB = ((i + 1) % maxVSegs) * maxUSegs;

                for (var j = 0; j < usegs; ++j)
                {
                    var colA = j;
                    var colB = (j + 1) % maxUSegs;

                    indices.Add(rowA + colA);
                    indices.Add(rowA + colB);
                    indices.Add(rowB + colB);
                    indices.Add(rowB + colA);
                }
            }

            return indices.ToIArray();
        }

        /// <summary>
        /// Returns the index buffer of a quad mesh strip.
        /// Returns an empty array if either numRowPoints or numPointsPerRow is less than 2.
        /// </summary>
        public static IArray<int> QuadMeshStripIndicesFromPointRows(
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

            return indices.ToIArray();
        }

        public static IArray<int> TriMeshCylinderCapIndices(int numEdgeVertices)
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

            return indices.ToIArray();
        }

        /// <summary>
        /// Creates a quad mesh given a mapping from 2 space to 3 space 
        /// </summary>
        public static IMesh CreateQuadMesh(this Func<Vector2, Vector3> f, int usegs, int vsegs, bool wrapUSegs = false, bool wrapVSegs = false)
        {
            var verts = new List<Vector3>();
            var maxUSegs = wrapUSegs ? usegs : usegs + 1;
            var maxVSegs = wrapVSegs ? vsegs : vsegs + 1;

            for (var i = 0; i < maxVSegs; ++i)
            {
                var v = (float)i / vsegs;
                for (var j = 0; j < maxUSegs; ++j)
                {
                    var u = (float)j / usegs;
                    verts.Add(f(new Vector2(u, v)));
                }
            }

            return CreateQuadMesh(verts.ToIArray(), ComputeQuadMeshStripIndices(usegs, vsegs, wrapUSegs, wrapVSegs));
        }
    }
}
