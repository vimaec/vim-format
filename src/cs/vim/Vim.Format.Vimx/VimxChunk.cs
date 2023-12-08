using System.Collections.Generic;
using System.Linq;
using Vim.BFastNS;
using Vim.G3dNext.Attributes;

namespace Vim.Format.VimxNS
{
    public class VimxChunk
    {
        public List<G3dMesh> Meshes = new List<G3dMesh>();

        public VimxChunk() { }
        public VimxChunk(List<G3dMesh> meshes) { Meshes = meshes; }

        public VimxChunk(BFastNS.BFast bfast)
        {
            Meshes = bfast.Entries
                .Select(e => new G3dMesh(bfast.GetBFast(e)))
                .ToList();
        }

        public BFastNS.BFast ToBFast()
        {
            var chunk = new BFastNS.BFast();
            chunk.SetBFast(
                BufferNames.Mesh,
                Meshes.Select(m => m.ToBFast())
            );
            return chunk;
        }

        
    }
}
