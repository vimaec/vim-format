using System.Collections.Generic;

namespace Vim.Format
{
    public class VimQuadMeshStrip
    {
        /// <summary>
        /// Computes the indices of a quad mesh astrip.
        /// </summary>
        public static List<int> ComputeQuadMeshStripIndices(int usegs, int vsegs, bool wrapUSegs = false, bool wrapVSegs = false)
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

            return indices;
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
