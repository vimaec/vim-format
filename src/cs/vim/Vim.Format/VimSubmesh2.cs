using Vim.Math3d;

namespace Vim.Format
{
    public class VimSubmesh2 : IVimSubmesh
    {
        public int Index { get; }
        public IVimRenderMaterial Material { get; }
        public Vector3[] Vertices { get; }
        public int[] Indices { get; }
        
        public VimSubmesh2(int index, IVimRenderMaterial material, Vector3[] vertices, int[] indices)
        {
            Index = index;
            Material = material;
            Vertices = vertices;
            Indices = indices;
        }
    }
}