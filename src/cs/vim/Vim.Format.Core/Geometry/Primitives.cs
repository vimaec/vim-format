﻿using System;
using System.Collections.Generic;
using System.Linq;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim.Format.Geometry
{
    // TODO: plane, cylinder, cone, ruled face, 
    public static class Primitives
    {
        public static VimMesh CreateCube()
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

            return new VimMesh(indices, vertices);
        }

        public static VimMesh CreateCube(AABox box)
        {
            return CreateCube().Scale(box.Extent).Translate(box.Center);
        }

        private static float Sqrt2 = 2.0f.Sqrt();
        public static VimMesh CreateTetrahedron()
        {
            var vertices = new[]
            {
                new Vector3(1f, 0.0f, -1f / Sqrt2),
                new Vector3(-1f, 0.0f, -1f / Sqrt2),
                new Vector3(0.0f, 1f, 1f / Sqrt2),
                new Vector3(0.0f, -1f, 1f / Sqrt2)
            };
            var indices = new[] { 0, 1, 2, 1, 0, 3, 0, 2, 3, 1, 3, 2 };
            return new VimMesh(indices, vertices);
        }
        public static VimMesh CreateSquare()
        {
            var vertices = new[]
            {
                new Vector3(-0.5f, -0.5f, 0f),
                new Vector3(-0.5f, 0.5f, 0f),
                new Vector3(0.5f, 0.5f, 0f),
                new Vector3(0.5f, -0.5f, 0f)
            };

            var indices = new[] { 0, 1, 2, 2, 3, 0 };

            return new VimMesh(indices, vertices);
        }

        // see: https://github.com/mrdoob/three.js/blob/9ef27d1af7809fa4d9943f8d4c4644e365ab6d2d/src/geometries/TorusBufferGeometry.js#L52
        public static Vector3 TorusFunction(Vector2 uv, float radius, float tube)
        {
            uv *= Math3d.Constants.TwoPi;
            return new Vector3(
                (radius + tube * uv.Y.Cos()) * uv.X.Cos(),
                (radius + tube * uv.Y.Cos()) * uv.X.Sin(),
                tube * uv.Y.Sin());
        }

        public static VimMesh Torus(float radius, float tubeRadius, int uSegs, int vSegs)
            => QuadMesh(uv => TorusFunction(uv, radius, tubeRadius), uSegs, vSegs);

        // see: https://github.com/mrdoob/three.js/blob/9ef27d1af7809fa4d9943f8d4c4644e365ab6d2d/src/geometries/SphereBufferGeometry.js#L76
        public static Vector3 SphereFunction(Vector2 uv, float radius)
            => new Vector3(
                (float)(-radius * Math.Cos(uv.X * Math3d.Constants.TwoPi) * Math.Sin(uv.Y * Math3d.Constants.Pi)),
                (float)(radius * Math.Cos(uv.Y * Math3d.Constants.Pi)),
                (float)(radius * Math.Sin(uv.X * Math3d.Constants.TwoPi) * Math.Sin(uv.Y * Math3d.Constants.Pi)));

        public static VimMesh Sphere(float radius, int uSegs, int vSegs)
            => QuadMesh(uv => SphereFunction(uv, radius), uSegs, vSegs);

        /// <summary>
        /// Returns a collection of circular points.
        /// </summary>
        public static Vector2[] CirclePoints(float radius, int numPoints)
            => CirclePoints(numPoints).Select(x => x * radius).ToArray();

        public static Vector2[] CirclePoints(int numPoints)
            => numPoints.Select(i => CirclePoint(i, numPoints)).ToArray();

        public static Vector2 CirclePoint(int i, int numPoints)
            => new Vector2((i * (Math3d.Constants.TwoPi / numPoints)).Cos(), (i * (Math3d.Constants.TwoPi / numPoints)).Sin());

        /// <summary>
        /// Computes the indices of a quad mesh astrip.
        /// </summary>
        public static int[] ComputeQuadMeshStripIndices(int usegs, int vsegs, bool wrapUSegs = false, bool wrapVSegs = false)
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

            return indices.ToArray();
        }

        /// <summary>
        /// Returns the index buffer of a quad mesh strip.
        /// Returns an empty array if either numRowPoints or numPointsPerRow is less than 2.
        /// </summary>
        public static int[] QuadMeshStripIndicesFromPointRows(
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

            return indices.ToArray();
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

        /// <summary>
        /// Creates a quad mesh given a mapping from 2 space to 3 space 
        /// </summary>
        public static VimMesh QuadMesh(this Func<Vector2, Vector3> f, int segs)
            => QuadMesh(f, segs, segs);

        /// <summary>
        /// Creates a quad mesh given a mapping from 2 space to 3 space 
        /// </summary>
        public static VimMesh QuadMesh(this Func<Vector2, Vector3> f, int usegs, int vsegs, bool wrapUSegs = false, bool wrapVSegs = false)
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
            var indices = ComputeQuadMeshStripIndices(usegs, vsegs, wrapUSegs, wrapVSegs);

            return VimMesh.FromQuad(indices, verts.ToArray());
        }
    }
}
