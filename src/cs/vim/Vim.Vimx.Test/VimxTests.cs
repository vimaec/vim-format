using NUnit.Framework;
using NUnit.Framework.Internal;
using Vim.BFast;
using Vim.Format.Vimx;
using Vim.G3dNext.Attributes;
using Vim.G3dNext.Tests;
using Vim.Math3d;

namespace Vim.Format.Vimx.Tests
{
    [TestFixture]
    public static class VimxTests
    {
        [Test]
        public static void Can_Convert_And_Read_Vimx()
        {
            var input = TestUtils.ResidencePath;
            var name = Path.GetFileNameWithoutExtension(TestUtils.ResidencePath);
            var output = Path.Combine(TestUtils.TestOutputFolder, name + ".vimx");

            var vimx = Vimx.FromVim(input);
            vimx.Write(output);

            var result = Vimx.FromPath(output);
            Assert.AreEqual(vimx.scene.InstanceFiles, result.scene.InstanceFiles);
            Assert.AreEqual(vimx.materials.materialColors, result.materials.materialColors);
            Assert.AreEqual(vimx.meshes.Length, result.meshes.Length);

        }

    }

}

