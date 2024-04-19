using NUnit.Framework;
using NUnit.Framework.Internal;
using Vim.Format.VimxLib.Conversion;
using Vim.Util.Tests;

namespace Vim.Format.VimxLib.Tests
{
    [TestFixture]
    public static class VimxTests
    {
        [Test]
        public static void Can_Convert_And_Read_Vimx()
        {
            var input = TestUtils.ResidencePath;
            var name = Path.GetFileNameWithoutExtension(TestUtils.ResidencePath);
            var output = TestUtils.PrepareOutputPath(name + ".vimx");

            var vimx = VimxConverter.FromVimPath(input);
            vimx.ToBFast().Write(output);

            var result = Vimx.FromPath(output);
            Assert.AreEqual(vimx.Scene.InstanceMeshes, result.Scene.InstanceMeshes);
            Assert.AreEqual(vimx.Materials.MaterialColors, result.Materials.MaterialColors);
            Assert.AreEqual(vimx.Chunks.Length, result.Chunks.Length);
        }
    }
}

