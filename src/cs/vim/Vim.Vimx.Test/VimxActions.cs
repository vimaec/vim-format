using NUnit.Framework;
using Vim.Format.VimxNS.Conversion;
using Vim.Util.Tests;

namespace Vim.Format.VimxNS.Actions
{
    [TestFixture]
    public static class VimxActions
    {

        const string whiteleys = "_WHITELEYS-VIM-MAIN_detached.v1.2.42.vim";

        [Test, Explicit]
        public static void ConvertVimToVimx()
        {
            //var input = VimFormatRepoPaths.GetLatestWolfordResidenceVim();
            var input = Path.Join(VimFormatRepoPaths.DataDir, whiteleys);

            var name = Path.GetFileNameWithoutExtension(input);
            var output = Path.Combine(VimFormatRepoPaths.OutDir, name + ".vimx");

            var vimx = VimxConverter.FromVimPath(input);
            vimx.ToBFast().Write(output);
        }
    }
}
