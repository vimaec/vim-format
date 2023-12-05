using System.Linq;
using Vim.LinqArray;
using Vim.BFastNextNS;
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
            //2MB once compressed -> 0.5MB
            const int ChunkSize = 2000000;

            var chunks = new List<VimxChunk>();
            var chunk = new VimxChunk();
            var chunkSize = 0L;
            foreach (var mesh in meshes)
            {
                chunkSize += mesh.GetSize();
                if (chunkSize > ChunkSize && chunk.Meshes.Count > 0)
                {
                    chunks.Add(chunk);
                    chunk = new VimxChunk();
                    chunkSize = 0;
                }
                mesh.Chunk = chunks.Count;
                mesh.ChunkIndex = chunk.Meshes.Count();
                chunk.Meshes.Add(mesh);
            }
            if(chunk.Meshes.Count > 0)
            {
                chunks.Add(chunk);
            }

            return chunks.ToArray();
        }
    }
}
