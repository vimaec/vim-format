using NUnit.Framework;
using System.IO;
using Vim.Util;
using Vim.Util.Tests;

namespace Vim.Format.Tests;

[TestFixture]
public static class VimCppTests
{

    public class VimCppTestsShell : ShellProcess
    {
        public override string GetExePath(string projectConfig)
        {
            return Path.Combine(VimFormatRepoPaths.SrcDir, "cpp", "vim", "bin", "x64", projectConfig, "Vim.Cpp.exe");
        }
    }

    [Test]
    public static void RunVimCppTestsShell()
    {
        var vimCppTestsShell = new VimCppTestsShell();
        var result = vimCppTestsShell.StartProcess().GetResult();
        Assert.IsTrue(result.IsSuccess(), result.ToString());
    }
}
