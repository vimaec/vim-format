using Vim.Math3d;

namespace Vim.Format.Geometry
{
    public interface IBounded
    {
        AABox Bounds { get; }
    }
}
