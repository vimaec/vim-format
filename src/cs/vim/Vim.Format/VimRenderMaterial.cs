using Vim.G3d;
using Vim.Math3d;

namespace Vim.Format
{
    public class VimRenderMaterial : IVimRenderMaterial
    {
        public int Index { get; }
        public Vector4 Color { get; }
        public float Glossiness { get; }
        public float Smoothness { get; }

        public VimRenderMaterial(int index, Vector4 color, float glossiness, float smoothness)
        {
            Index = index;
            Color = color;
            Glossiness = glossiness;
            Smoothness = smoothness;
        }

        public static VimRenderMaterial FromG3d(G3dVim g3d, int index)
        {
            var color = g3d.MaterialColors[index];
            var glossiness = g3d.MaterialGlossiness[index];
            var smoothness = g3d.MaterialSmoothness[index];
            return new VimRenderMaterial(index, color, glossiness, smoothness);
        }
    }
}