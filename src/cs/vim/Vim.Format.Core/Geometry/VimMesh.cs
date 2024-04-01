using System.Collections.Generic;
using System.Linq;
using Vim.G3d;
using Vim.G3dNext;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim.Format.Geometry
{
    public class VimMesh : IMeshCommon
    {
        public IArray<Vector3> Vertices => vertices.ToIArray();

        public IArray<int> Indices => indices.ToIArray();

        public IArray<int> SubmeshMaterials =>submeshMaterials.ToIArray();
        public IArray<int> SubmeshIndexOffsets => submeshes.ToIArray();
        public IArray<int> SubmeshIndexCount => submeshSizes.ToIArray();

        public int NumCornersPerFace => 3;

        public int NumVertices => vertices.Length;

        public int NumCorners => indices.Length;

        public int NumFaces => indices.Length / 3;

        public int NumInstances => 0;

        public int NumMeshes => 1;

        public int NumShapeVertices => 0;

        public int NumShapes => 0;

        public int[] indices;
        public Vector3[] vertices;
        public int[] submeshes;
        public int[] submeshMaterials;
        public int[] submeshSizes;

        public IMeshCommon Transform(Matrix4x4 mat)
        {
            var mesh = new VimMesh();
            mesh.indices = indices.ToArray();
            mesh.vertices = new Vector3[vertices.Length];
            mesh.submeshes = submeshes.ToArray();
            mesh.submeshMaterials = submeshMaterials.ToArray();

            for (var i = 0; i < vertices.Length; i++)
            {
                mesh.vertices[i] = vertices[i].Transform(mat);
            }
            return mesh;
        }

        public static VimMesh FromG3d(G3dVim g3d, int index)
        {
            var mesh = new VimMesh();

            var vStart = g3d.GetMeshVertexStart(index);
            var vEnd = g3d.GetMeshVertexEnd(index);
            mesh.vertices = new Vector3[vEnd - vStart];
            for (var i = 0; i < mesh.vertices.Length; i++)
            {
                var v = vStart + i;
                mesh.vertices[i] = g3d.Positions[v];
            }

            var iStart = g3d.GetMeshIndexStart(index);
            var iEnd = g3d.GetMeshIndexEnd(index);
            mesh.indices = new int[iEnd - iStart];
            for (var i = 0; i < mesh.indices.Length; i++)
            {
                var j = iStart + i;
                mesh.indices[i] = g3d.Indices[j] - vStart;
            }

            var sStart = g3d.GetMeshSubmeshStart(index);
            var sEnd = g3d.GetMeshSubmeshEnd(index);
            mesh.submeshes = new int[sEnd - sStart];
            mesh.submeshMaterials = new int[sEnd - sStart];
            mesh.submeshSizes = new int[sEnd - sStart];

            for (var i = 0; i < mesh.submeshMaterials.Length; i++)
            {
                var s = sStart + i;
                mesh.submeshes[i] = g3d.SubmeshIndexOffsets[s] - iStart;
                mesh.submeshMaterials[i] = g3d.SubmeshMaterials[s];
                mesh.submeshSizes[i] = g3d.GetSubmeshIndexCount(s);
            }

            return mesh;
        }

        public static IEnumerable<VimMesh> GetAllMeshes(G3dVim g3d)
        {
            return Enumerable.Range(0, g3d.GetMeshCount()).Select(i => FromG3d(g3d, i));
        }
    }
}
