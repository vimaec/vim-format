using NUnit.Framework;
using System.IO;
using Vim.Format.SceneBuilder;
using Vim.Util.Tests;

namespace Vim.Gltf.Converter.Tests;

[TestFixture]
public static class TestVimGltfConverter
{
    [TestCase("Fox.glb", GltfToVimStore.MetersToFeet)]
    [TestCase("SciFiHelmet.glb", GltfToVimStore.MetersToFeet)]
    [TestCase("ToyCar.glb", GltfToVimStore.MetersToFeet)]
    [TestCase("BoomBoxWithAxes.glb", GltfToVimStore.MetersToFeet)]
    public static void VimGltfConverterWritesVimFile(string gltfFileName, float scale)
    {
        var ctx = new CallerTestContext(subDirComponents: gltfFileName);
        var dir = ctx.PrepareDirectory();

        var gltfFilePath = Path.Combine(VimFormatRepoPaths.DataDir, "gltf-samples", gltfFileName);
        var vimFilePath = Path.Combine(dir, Path.ChangeExtension(Path.GetFileName(gltfFilePath), ".vim"));
        
        Assert.IsTrue(File.Exists(gltfFilePath), $"Input GLTF file not found: {gltfFilePath}");

        GltfToVimStore.Convert(gltfFilePath, vimFilePath, scale);

        Assert.IsTrue(File.Exists(vimFilePath), $"Output VIM file not found: {vimFilePath}");

        var vim = VimScene.LoadVim(vimFilePath);
        vim.Validate();
    }

    //[Test, Explicit("Local")]
    //public static void CreateGlb()
    //{
    //    var input = @"path/to/file.gltf";
    //    var model = SharpGLTF.Schema2.ModelRoot.Load(input);
    //    model.SaveGLB(Path.ChangeExtension(input, ".glb"));
    //}

    [TestCase("RoomTest.vim")]
    public static void VimGltfConverterWritesGltfFile(string vimFileName)
    {
        var ctx = new CallerTestContext(subDirComponents: vimFileName);
        var dir = ctx.PrepareDirectory();

        var vimFilePath = Path.Combine(VimFormatRepoPaths.DataDir, vimFileName);
        var gltfFilePath = Path.Combine(dir, Path.ChangeExtension(Path.GetFileName(vimFilePath), ".glb"));

        Assert.IsTrue(File.Exists(vimFilePath), $"Input VIM file path not found: {vimFilePath}");

        VimToGltfStore.Convert(vimFilePath, gltfFilePath);

        Assert.IsTrue(File.Exists(gltfFilePath), $"Output GLTF file not found: {gltfFilePath}");

        Util.IO.OpenFile(gltfFilePath);
    }

    //[Test, Explicit("Local")]
    //public static void ConvertVimToGltf()
    //{
    //    var ctx = new CallerTestContext();
    //    var dir = ctx.PrepareDirectory();

    //    var vimFilePath = @"path/to/file.vim";
    //    var gltfFilePath = Path.Combine(dir, Path.ChangeExtension(Path.GetFileName(vimFilePath), ".glb"));

    //    Assert.IsTrue(File.Exists(vimFilePath), $"Input VIM file path not found: {vimFilePath}");

    //    VimToGltfStore.Convert(vimFilePath, gltfFilePath, validateGltf: false);

    //    Assert.IsTrue(File.Exists(gltfFilePath), $"Output GLTF file not found: {gltfFilePath}");
    //}
}
