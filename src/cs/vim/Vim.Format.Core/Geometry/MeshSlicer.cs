using System;
using System.Collections.Generic;
using Vim.Math3d;

namespace Vim.Format.Geometry
{
    public static class MeshSlicer
    {
        public delegate bool FilterTrianglesPredicate(
            int vertexIndex0,
            Vector3 vertex0,
            int vertexIndex1,
            Vector3 vertex1,
            int vertexIndex2,
            Vector3 vertex2);

        public static MeshSlicerResult FilterTriangles(
            IReadOnlyList<Vector3> vertexBuffer,
            IReadOnlyList<int> indexBuffer,
            FilterTrianglesPredicate predicate)
        {
            // BREAKDOWN
            //
            // vertexBuffer: [ ..., v0, v1, v2, v3, ..., v34, v35, v36, v37, ... ]
            //
            // indexBuffer:  [ 34, 35, 36, 34, 36, 37 ..., 0, 1, 2, 0, 2, 3, ... ]
            //
            // predicate(indexBuffer): [ 34,  36,  37,   0,   1,   2,   0,   2,   3 ] <-- triangle 34, 35, 36 was rejected
            //                            v    v    v    v    v    v    v    v    v
            // newIndexBuffer:         [  0,   1,   2,   3,   4,   5,   3,   5,   6 ]
            //                            |    |    |    |    |    |   /    /    /
            //                            |    |    |    |    |    |  /    /    /
            //                            |    |    |    |    |    | /    /    /
            //                            |    |    |    |    |    |/    /    /
            //                            |    |    |    |    |   /|    /    /
            //                            |    |    |    |   _|__/ |   /    /
            //                            |    |    |    |  / |    |  /    /
            //                            |    |    |    |_/  |    |_/    /
            //                            |    |    |    |    |    |     /
            //                            v    v    v    v    v    v    v
            // newVertexBuffer:        [ v34, v36, v37, v0,  v1,  v2,  v3 ]

            var oldToNewIndexMap = new Dictionary<int, int>();
            var newVertexBuffer = new List<Vector3>();
            var newIndexBuffer = new List<int>();

            void KeepVertexIndex(int oldVertexIndex, Vector3 vertex)
            {
                if (!oldToNewIndexMap.TryGetValue(oldVertexIndex, out var mappedVertexIndex))
                {
                    mappedVertexIndex = oldToNewIndexMap.Count; // 0, 1, 2, ...
                    oldToNewIndexMap.Add(oldVertexIndex, mappedVertexIndex);
                    newVertexBuffer.Add(vertex);
                }

                newIndexBuffer.Add(mappedVertexIndex);
            }

            // Filter the indices.
            for (var i = 0; i < indexBuffer.Count; i += 3)
            {
                var vertexIndex0 = indexBuffer[i];
                var vertexIndex1 = indexBuffer[i + 1];
                var vertexIndex2 = indexBuffer[i + 2];

                var v0 = vertexBuffer[vertexIndex0];
                var v1 = vertexBuffer[vertexIndex1];
                var v2 = vertexBuffer[vertexIndex2];

                if (!predicate(vertexIndex0, v0, vertexIndex1, v1, vertexIndex2, v2))
                    continue;

                KeepVertexIndex(vertexIndex0, v0);
                KeepVertexIndex(vertexIndex1, v1);
                KeepVertexIndex(vertexIndex2, v2);
            }

            return new MeshSlicerResult(newVertexBuffer.ToArray(), newIndexBuffer.ToArray());
        }

        public static MeshSlicerResult SliceMesh(
            IReadOnlyList<Vector3> vertexBuffer,
            IReadOnlyList<int> indexBuffer,
            int indexBufferStart,
            int indexBufferEnd)
        {
            // BREAKDOWN:
            //
            // vertexBuffer: [..., vA, vB, vC, vD, ..., vE, vF, vG, ...]
            //    (indices):  ..., 54, 55, 56, 57, ..., 89, 90, 91, ... 
            // 
            // indexBuffer:  [..., 89, 90, 91, 54, 55, 56, 54, 56, 57, ...]
            //                   [  ^indexBufferStart indexBufferEnd^ ]
            // oldIndexSlice:    [ 89, 90, 91, 54, 55, 56, 54, 56, 57 ]
            //
            // oldToNewIndexMap...
            //       {oldIndex}: { 89, 90, 91, 54, 55, 56, 57 }
            //       {newIndex}: {  0,  1,  2,  3,  4,  5,  6 }  <-- new indices are based on order of insertion of oldIndex into the map.
            //
            // newVertexBuffer:  [ vE, vF, vG, vA, vB, vC, vD ]  <-- vertices are added based on order of insertion of oldIndex into the map.
            //
            // oldIndexSlice --> [ 89, 90, 91, 54, 55, 56, 54, 56, 57 ] 
            //                      v   v   v   v   v   v   v   v   v  <-- mapped using oldToNewIndexMap
            // newIndexBuffer:   [  0,  1,  2,  3,  4,  5,  3,  5,  6 ]

            if (indexBufferStart < 0 || indexBufferStart >= indexBuffer.Count)
            {
                return MeshSlicerResult.Empty;
            }
            
            if (indexBufferEnd < 0
                || indexBufferEnd >= indexBuffer.Count
                || indexBufferEnd < indexBufferStart)
            {
                return MeshSlicerResult.Empty;
            }

            // Read the indices.
            var oldToNewIndexMap = new Dictionary<int, int>();
            var newVertexBuffer = new List<Vector3>();

            // Create a slice of the index buffer.
            var oldIndexSlice = new int[indexBufferEnd - indexBufferStart + 1];
            for (var i = 0; i < oldIndexSlice.Length; i++)
            {
                var oldIndex = indexBuffer[indexBufferStart + i];
                oldIndexSlice[i] = oldIndex;

                if (!oldToNewIndexMap.ContainsKey(oldIndex))
                {
                    var newIndex = oldToNewIndexMap.Count; // 0, 1, 2, ...
                    oldToNewIndexMap.Add(oldIndex, newIndex);

                    var vertex = vertexBuffer[oldIndex];
                    newVertexBuffer.Add(vertex);
                }
            }

            // Create a new index buffer starting at 0.
            var newIndexBuffer = new int[oldIndexSlice.Length];
            for (var i = 0; i < newIndexBuffer.Length; ++i)
            {
                var oldIndex = oldIndexSlice[i];
                var newIndex = oldToNewIndexMap[oldIndex];
                newIndexBuffer[i] = newIndex;
            }

            return new MeshSlicerResult(newVertexBuffer.ToArray(), newIndexBuffer);
        }
    }

    public class MeshSlicerResult
    {
        public Vector3[] VertexBuffer { get; }
        public int[] IndexBuffer { get; }

        public MeshSlicerResult(Vector3[] vertexBuffer, int[] indexBuffer)
        {
            VertexBuffer = vertexBuffer;
            IndexBuffer = indexBuffer;
        }

        public static MeshSlicerResult Empty
            => new MeshSlicerResult(Array.Empty<Vector3>(), Array.Empty<int>());

        public bool IsEmpty
            => VertexBuffer.Length == 0 || IndexBuffer.Length == 0;
    }
}
