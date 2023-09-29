using System.Linq;
using Vim.BFastNextNS;
using Vim.G3dNext;
using Vim.G3dNext.Attributes;

namespace Vim.Format.VimxNS
{
    public static class BufferNames
    {
        public const string Header = "header";
        public const string Meta = "meta";
        public const string Scene = "scene";
        public const string Materials = "materials";
        public static string Mesh(int mesh) => $"mesh_{mesh}";
    }

    public static class BufferCompression
    {
        public const bool Header = false;
        public const bool Meta = false;
        public const bool Scene = true;
        public const bool Materials = true;
        public const bool Meshes = true;
    }

    public class Vimx
    {
        public readonly VimxHeader Header;
        public readonly MetaHeader Meta;
        public readonly G3dScene Scene;
        public readonly G3dMaterials Materials;
        public readonly G3dMesh[] Meshes;

        public Vimx(VimxHeader header, MetaHeader meta, G3dScene scene, G3dMaterials materials, G3dMesh[] meshes)
        {
            Meta = meta;
            Header = header;
            Scene = scene;
            Materials = materials;
            Meshes = meshes;
        }

        public Vimx(BFastNext bfast)
        {
            Header = new VimxHeader(bfast.GetArray<byte>(BufferNames.Header));

            Scene = new G3dScene(
                bfast.GetBFast(BufferNames.Scene, BufferCompression.Scene)
            );

            Materials = new G3dMaterials(
                bfast.GetBFast(BufferNames.Materials, BufferCompression.Materials)
            );

            Meshes = Enumerable.Range(0, Scene.MeshIndexCounts.Length).Select(i =>
                new G3dMesh(bfast.GetBFast(BufferNames.Mesh(i), BufferCompression.Meshes))
            ).ToArray();
        }

        public static Vimx FromPath(string path)
            => path.ReadBFast((b) => new Vimx(b));

        public BFastNext ToBFast()
        {
            var bfast = new BFastNext();
            bfast.SetArray(BufferNames.Meta, MetaHeader.Default.ToBytes());
            bfast.SetArray(BufferNames.Header, Header.ToBytes());
            bfast.SetBFast(BufferNames.Scene, Scene.ToBFast(), BufferCompression.Scene);
            bfast.SetBFast(BufferNames.Materials, Materials.ToBFast(), BufferCompression.Materials);
            bfast.SetBFast(BufferNames.Mesh, Meshes.Select(m => m.ToBFast()), BufferCompression.Meshes);
            return bfast;
        }
    }
}
