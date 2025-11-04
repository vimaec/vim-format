using System;
using System.Collections.Generic;
using System.Linq;
using Vim.Math3d;

namespace Vim.Format.api_v2
{
    public class VimSubdividedMesh
    {
        public IReadOnlyList<int> Indices { get; }
        public IReadOnlyList<Vector3> Vertices { get; }
        public IReadOnlyList<int> SubmeshesIndexOffset { get; }
        public IReadOnlyList<int> SubmeshMaterials { get; }

        public VimSubdividedMesh(VimMesh mesh)
        {
            if (mesh.Indices.Any(i => i < 0 && i >= mesh.Vertices.Count))
                throw new Exception($"Invalid mesh. Indices out of vertex range.");

            var facesByMats = mesh.FaceMaterials
                .Select((face, index) => (face, index))
                .GroupBy(pair => pair.face, pair => pair.index);

            var submeshIndexOffset = new List<int>();
            var submeshMaterials = new List<int>();
            var indicesRemap = new List<int>();

            foreach (var group in facesByMats)
            {
                submeshIndexOffset.Add(indicesRemap.Count);
                submeshMaterials.Add(group.Key);
                foreach (var face in group)
                {
                    var f = face * 3;
                    indicesRemap.Add(mesh.Indices[f]);
                    indicesRemap.Add(mesh.Indices[f + 1]);
                    indicesRemap.Add(mesh.Indices[f + 2]);
                }
            }
            Indices = indicesRemap;
            SubmeshMaterials = submeshMaterials;
            SubmeshesIndexOffset = submeshIndexOffset;

            Vertices = mesh.Vertices;
        }

        public VimSubdividedMesh(
            IReadOnlyList<int> indices,
            IReadOnlyList<Vector3> vertices,
            IReadOnlyList<int> submeshesIndexOffset,
            IReadOnlyList<int> submeshMaterials
        )
        {
            Indices = indices;
            Vertices = vertices;
            SubmeshesIndexOffset = submeshesIndexOffset;
            SubmeshMaterials = submeshMaterials;
        }

        public bool IsEquivalentTo(VimSubdividedMesh other)
            => Vertices.SequenceEqual(other.Vertices)
                && Indices.SequenceEqual(other.Indices)
                && SubmeshesIndexOffset.SequenceEqual(other.SubmeshesIndexOffset)
                && SubmeshMaterials.SequenceEqual(other.SubmeshMaterials);

        public static VimSubdividedMesh CreateFromWorldSpaceBox(IReadOnlyList<Vector3> points, int materialIndex)
        {
            //     7 *--------------------------* 6 (max)
            //       |\                         |\
            //       | \                        | \
            //       |  \ 4                     |  \ 5
            //       |   *--------------------------*
            //       |   |                      |   |
            //     3 *---|----------------------* 2 |
            //        \  |                       \  |
            //         \ |                        \ |
            //          \|                         \|
            //           *--------------------------*
            //    (min) 0                             1

            // Faces are declared as quads in counter-clockwise order (right-handed rule, thumb points in normal direction)
            var bottomQuad = new[] { points[0], points[3], points[2], points[1] };
            var topQuad = new[] { points[4], points[5], points[6], points[7] };
            var backQuad = new[] { points[2], points[3], points[7], points[6] };
            var frontQuad = new[] { points[0], points[1], points[5], points[4] };
            var leftQuad = new[] { points[3], points[0], points[4], points[7] };
            var rightQuad = new[] { points[2], points[6], points[5], points[1] };

            var quads = new[] { bottomQuad, topQuad, backQuad, frontQuad, leftQuad, rightQuad };

            var vertices = new List<Vector3>();
            var indices = new List<int>();

            var quadIndices = new[] { 0, 1, 2, 0, 2, 3 };

            foreach (var quadVertices in quads)
            {
                var indexOffset = vertices.Count;
                vertices.AddRange(quadVertices);
                indices.AddRange(quadIndices.Select(i => i + indexOffset));
            }

            return new VimSubdividedMesh(indices, vertices, new[] { 0 }, new[] { materialIndex });
        }
    }
}