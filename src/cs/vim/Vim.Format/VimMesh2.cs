using Vim.G3d;
using Vim.Math3d;

namespace Vim.Format
{
    public class VimMesh2 : IVimMesh
    {
        public int Index { get; }
        public IVimSubmesh[] Submeshes { get; }

        public VimMesh2(int index, IVimSubmesh[] submeshes)
        {
            Index = index;
            Submeshes = submeshes;
        }
        
        public static VimMesh2 FromG3d(G3dVim g3d, int index)
        {
            var sStart = g3d.GetMeshSubmeshStart(index);
            var sEnd = g3d.GetMeshSubmeshEnd(index);
        
            var submeshes = new IVimSubmesh[sEnd - sStart];

            for (var i = 0; i < submeshes.Length; i++)
            {
                var submesh = i + sStart;
                var iStart = g3d.SubmeshIndexOffsets[submesh];
                var iEnd = submesh + 1 < sEnd ? g3d.SubmeshIndexOffsets[submesh + 1] : g3d.Indices.Length;
                var vStart = g3d.SubmeshVertexOffsets[submesh];
                var vEnd = submesh + 1 < sEnd ? g3d.SubmeshVertexOffsets[submesh + 1] : g3d.Positions.Length;
            
                var indices = new int[iEnd - iStart];
                for (var j = 0; j < indices.Length; j++)
                {
                    var k = iStart + j;
                    indices[j] = g3d.Indices[k] - vStart;
                }
            
                var vertices = new Vector3[vEnd - vStart];
                for (var j = 0; j < vertices.Length; j++)
                {
                    var v = vStart + j;
                    vertices[j] = g3d.Positions[v];
                }

                var material = VimRenderMaterial.FromG3d(g3d, g3d.SubmeshMaterials[submesh]);
            
                submeshes[i] = new VimSubmesh2(submesh, material, vertices, indices);
            }

            return new VimMesh2(index, submeshes);
        }

        public void Validate()
        {
            ValidateIndices();
        }

        private void ValidateIndices()
        {
            foreach (var submesh in Submeshes)
            {
                submesh.Validate();
            }
        }

        public IVimMesh Transform(Matrix4x4 mat)
        {
            var submeshes = new IVimSubmesh[Submeshes.Length];

            for (var i = 0; i < Submeshes.Length; i++)
            {
                submeshes[i] = Submeshes[i].Transform(mat);
            }

            return new VimMesh2(Index, submeshes);
        }

        public static bool GeometryEquals(VimMesh2 a, VimMesh2 b, float tolerance = Math3d.Constants.Tolerance)
        {
            if (a.Submeshes.Length != b.Submeshes.Length)
                return false;

            for (var i = 0; i < a.Submeshes.Length; i++)
            {
                if (!VimSubmesh2.GeometryEquals((VimSubmesh2) a.Submeshes[i], (VimSubmesh2) b.Submeshes[i], tolerance))
                    return false;
            }

            return true;
        }
    }
}