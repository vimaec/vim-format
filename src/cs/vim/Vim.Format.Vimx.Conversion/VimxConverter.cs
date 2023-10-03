using System.Linq;
using Vim.LinqArray;
using Vim.BFast;
using Vim.G3dNext.Attributes;
using Vim.Format.ObjectModel;
using Vim.G3dNext;
using System.Collections.Generic;

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

            var chunks = meshes.SplitChunks();
            var scene = MeshesToScene.CreateScene(g3d, bim, chunks, meshes);

            var materials = new G3dMaterials(g3d.ToBFast());
            var header = VimxHeader.CreateDefault();
            
            return new Vimx(header, MetaHeader.Default, scene, materials, chunks);
        }

        public static VimxChunk[] SplitChunks(this IEnumerable<G3dMesh> meshes)
        {
            var chunks = new List<VimxChunk>();
            var chunk = new VimxChunk();
            foreach (var mesh in meshes)
            {
                chunk.Meshes.Add(mesh);
                mesh.Chunk = 0;
            }
            chunks.Add(chunk);

            return chunks.ToArray();
        }
    }
}
