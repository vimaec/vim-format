﻿using NUnit.Framework;
using System.Diagnostics;
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
            //var input = Path.Join(VimFormatRepoPaths.DataDir, whiteleys);
            var input = Path.Join(VimFormatRepoPaths.DataDir, "nbk.vim");

            var name = Path.GetFileNameWithoutExtension(input);
            var output = Path.Combine(VimFormatRepoPaths.OutDir, name + ".vimx");

            var sw = Stopwatch.StartNew();
            var vimx = VimxConverter.FromVimPath(input);
            Console.WriteLine("FromVimPath " + sw.ElapsedMilliseconds);

            sw.Restart();
            var bfast = vimx.ToBFast();
            Console.WriteLine("Write " + sw.ElapsedMilliseconds);

            sw.Restart();
            bfast.Write(output);
            Console.WriteLine("Write " + sw.ElapsedMilliseconds);
        }
    }
}
