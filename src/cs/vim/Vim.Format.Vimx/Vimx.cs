using System.Collections.Generic;
using System.Linq;
using Vim.BFastNext;
using Vim.Format.ObjectModel;
using Vim.G3dNext.Attributes;

namespace Vim.Format.Vimx
{
    public static class BufferNames
    {
        public const string Header = Format.BufferNames.Header;
        public const string Scene = "scene";
        public const string Materials = "materials";
        public static string Mesh(int mesh) => $"mesh_{mesh}";
    }

    public class Vimx
    {
        public VimxHeader header;
        public G3dScene2 scene;
        public G3dMaterials materials;
        public G3dMesh[] meshes;

        Vimx(VimxHeader header, G3dScene2 scene, G3dMaterials materials, G3dMesh[] meshes)
        {
            this.header = header;
            this.scene = scene;
            this.materials = materials;
            this.meshes = meshes;
        }

        public static Vimx FromVim(string vimPath)
        {
            var vim = VimScene.LoadVim(vimPath, new Format.LoadOptions()
            {
                SkipAssets = true,
                SkipGeometry = true,
            });
            var g3d = G3dVim.ReadFromVim(vimPath);
            return FromVim(g3d, vim.DocumentModel);
        }

        public static Vimx FromVim(G3dVim g3d, DocumentModel bim)
        {
            var meshes = g3d.ToG3dMeshes().OrderByBim(bim).ToArray();
            var scene = g3d.ToG3dScene(bim, meshes);
            var materials = g3d.ToG3dMaterials();
            var header = VimxHeader.CreateDefault();
            return new Vimx(header, scene, materials, meshes);
        }

        public static Vimx FromBFast(BFastNext.BFastNext bfast)
        {
            var headerBytes = bfast.GetArray<byte>("header");
            var header = VimxHeader.FromBytes(headerBytes);

            var bfastScene = bfast.GetBFast("scene", inflate: true);
            var scene = new G3dScene2(bfastScene);

            var bfastMaterials = bfast.GetBFast("materials", inflate: true);
            var materials = G3dMaterials.FromBFast(bfastMaterials);

            var meshes = Enumerable.Range(0, scene.MeshIndexCounts.Length).Select(i =>
            {
                var bfastMesh = bfast.GetBFast(BufferNames.Mesh(i), inflate: true);
                return G3dMesh.FromBFast(bfastMesh);
            }).ToArray();

            return new Vimx(header, scene, materials, meshes);
        }

        public static Vimx FromPath(string path)
            => path.ReadBFast(FromBFast);

        public void Write(string path)
        {
            var bfast = new BFastNext.BFastNext();
            bfast.AddArray(BufferNames.Header, header.ToBytes());
            bfast.SetBFast(BufferNames.Scene, scene.source.ToBFast(), deflate: true);
            bfast.SetBFast(BufferNames.Materials, materials.source.ToBFast(), deflate: true);
            bfast.SetBFast(BufferNames.Mesh, meshes.Select(m => m.source.ToBFast()), deflate: true);
            bfast.Write(path);
        }
    }
}
