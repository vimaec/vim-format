using Vim.G3d;
using Vim.Math3d;

namespace Vim.Format.Geometry
{
    public interface IMaterial
    {
        Vector4 Color { get; }
        float Smoothness { get; }
        float Glossiness { get; }
    }

    public class VimMaterial : IMaterial
    {
        public G3dMaterial Material;
        public VimMaterial(G3dMaterial material) => Material = material;
        public Vector4 Color => Material.Color;
        public float Smoothness => Material.Smoothness;
        public float Glossiness => Material.Glossiness;
    }
}
