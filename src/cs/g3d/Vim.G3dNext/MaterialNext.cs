using Vim.Math3d;

namespace Vim.G3dNext
{
    public class MaterialNext
    {
        public readonly G3dVim G3d;
        public readonly int Index;

        public MaterialNext(G3dVim g3D, int index)
        {
            G3d = g3D;
            Index = index;
        }

        public Vector4 Color => G3d.MaterialColors[Index];
        public float Glossiness => G3d?.MaterialGlossiness[Index] ?? 0f;
        public float Smoothness => G3d?.MaterialSmoothness[Index] ?? 0f;
    }
}
