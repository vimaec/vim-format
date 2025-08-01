using System.IO;
using Vim.Util.Tests;

namespace Vim.Format.Tests;

public static class TestFiles
{
    public static string[] VimFilePaths;

    static TestFiles()
    {
        VimFilePaths = Directory.GetFiles(VimFormatRepoPaths.DataDir, "*.vim", SearchOption.AllDirectories);
    }
}
