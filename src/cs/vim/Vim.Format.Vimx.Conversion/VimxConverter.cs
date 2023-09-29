using System.Linq;
using Vim.LinqArray;
using Vim.BFast;
using Vim.G3dNext.Attributes;
using Vim.Format.ObjectModel;
using Vim.G3dNext;

namespace Vim.Format.VimxNS.Conversion
{
    public static class VimxConverter
    {
        public static Vimx FromVimPath(string vimPath)
        {
            var vim = VimScene.LoadVim(vimPath, new LoadOptions()
            {
                SkipAssets = true,
                SkipGeometry = true,
            });

            var g3d = G3dVim.FromVim(vimPath);
            return FromVim(g3d, vim.DocumentModel);
        }

        public static Vimx FromVim(G3dVim g3d, DocumentModel bim)
        {
            var meshes = VimToMeshes.ExtractMeshes(g3d)
                .OrderByBim(bim)
                .ToArray();

            var scene = MeshesToScene.CreateScene(g3d, bim, meshes);
            var materials = new G3dMaterials(g3d.ToBFast());
            var header = VimxHeader.CreateDefault();
            
            return new Vimx(header, MetaHeader.Default, scene, materials, meshes);
        }
    }
}
