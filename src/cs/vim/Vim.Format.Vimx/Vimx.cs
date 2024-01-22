using System.Linq;
using Vim.BFastNS;
using Vim.G3dNext;

namespace Vim.Format.VimxNS
{
    public static class BufferNames
    {
        public const string Header = "header";
        public const string Meta = "meta";
        public const string Scene = "scene";
        public const string Materials = "materials";
        public static string Chunk(int mesh) => $"chunk_{mesh}";
    }

    public static class BufferCompression
    {
        public const bool Header = false;
        public const bool Meta = false;
        public const bool Scene = true;
        public const bool Materials = true;
        public const bool Meshes = false;
        public const bool Chunks = true;
    }

    public class Vimx
    {
        public readonly SerializableHeader Header;
        public readonly MetaHeader Meta;
        public readonly G3dScene Scene;
        public readonly G3dMaterials Materials;
        public readonly G3dChunk[] Chunks;

        public Vimx(SerializableHeader header, MetaHeader meta, G3dScene scene, G3dMaterials materials, G3dChunk[] chunks)
        {
            Meta = meta;
            Header = header;
            Scene = scene;
            Materials = materials;
            Chunks = chunks;
        }

        public Vimx(BFast bfast)
        {
            Header = VimxHeader.FromBytes(bfast.GetArray<byte>(BufferNames.Header));

            Scene = new G3dScene(
                bfast.GetBFast(BufferNames.Scene, BufferCompression.Scene)
            );

            Materials = new G3dMaterials(
                bfast.GetBFast(BufferNames.Materials, BufferCompression.Materials)
            );

            Chunks = Enumerable.Range(0, Scene.GetChunksCount())
                .Select(c => bfast.GetBFast(BufferNames.Chunk(c), BufferCompression.Chunks))
                .Select(b => new G3dChunk(b))
                .ToArray();
        }

        public static Vimx FromPath(string path)
            => path.ReadBFast((b) => new Vimx(b));

        public BFast ToBFast()
        {
            var bfast = new BFast();
            bfast.SetArray(BufferNames.Meta, MetaHeader.Default.ToBytes());
            bfast.SetArray(BufferNames.Header, Header.ToVimxBytes());
            bfast.SetBFast(BufferNames.Scene, Scene.ToBFast(), BufferCompression.Scene);
            bfast.SetBFast(BufferNames.Materials, Materials.ToBFast(), BufferCompression.Materials);
            bfast.SetBFast(BufferNames.Chunk, Chunks.Select(c => c.ToBFast()), BufferCompression.Chunks);
            return bfast;
        }


    }
}
