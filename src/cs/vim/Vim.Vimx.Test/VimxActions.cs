using NUnit.Framework;
using Vim.Format.VimxNS.Conversion;
using Vim.Util.Tests;

namespace Vim.Format.VimxNS.Actions
{
    [TestFixture]
    public static class VimxActions
    {

        const string whiteleys = "_WHITELEYS-VIM-MAIN_detached.v1.2.42.vim";
        const string residence = "residence.vim";

        [Test, Explicit]
        public static void ConvertVimToVimx()
        {
            // var input = VimFormatRepoPaths.GetLatestWolfordResidenceVim();
            // var input = Path.Join(VimFormatRepoPaths.DataDir, residence);
            var input = Path.Join(VimFormatRepoPaths.DataDir, "nbk.vim");

            var name = Path.GetFileNameWithoutExtension(input);
            var output = Path.Combine(VimFormatRepoPaths.OutDir, name + ".vimx");

            var vimx = VimxConverter.FromVimPath(input);

            Console.WriteLine(vimx.Chunks.SelectMany(c => c.Meshes).Sum(m => m.Indices.Length));
            Console.WriteLine(vimx.Chunks.SelectMany(c => c.Meshes).Sum(m => m.Positions.Length));

            Console.WriteLine(vimx.Header);
            vimx.ToBFast().Write(output);
        }
    }
}
