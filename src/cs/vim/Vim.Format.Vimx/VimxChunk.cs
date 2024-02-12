using System.Collections.Generic;
using System.Linq;
using Vim.BFastLib;
using Vim.G3dNext;

namespace Vim.Format.VimxNS
{
    public class VimxChunk
    {
        public List<G3dChunk> Meshes = new List<G3dChunk>();

        public VimxChunk() { }
        public VimxChunk(List<G3dChunk> meshes) { Meshes = meshes; }

        public VimxChunk(BFast bfast)
        {
            Meshes = bfast.Entries
                .Select(e => new G3dChunk(bfast.GetBFast(e)))
                .ToList();
        }

        public BFast ToBFast()
        {
            var chunk = new BFast();
            chunk.SetBFast(
                BufferNames.Chunk,
                Meshes.Select(m => m.ToBFast())
            );
            return chunk;
        }

        
    }
}
