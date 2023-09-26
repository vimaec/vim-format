using NUnit.Framework;
using Vim.G3dNext.Tests;

namespace Vim.Format.Vimx.Test
{
    [TestFixture]
    public static class VimxActions
    {
        [Test]
        public static void ConvertVimToVimx()
        {
            var input = TestUtils.ResidencePath;
            var name = Path.GetFileNameWithoutExtension(TestUtils.ResidencePath);
            var output = Path.Combine(TestUtils.TestOutputFolder, name + ".vimx");

            var vimx = Vimx.FromVim(input);
            vimx.Write(output);
        }
    }
}
