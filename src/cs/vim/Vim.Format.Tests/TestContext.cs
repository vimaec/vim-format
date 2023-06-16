using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Vim.Format.Logging;

namespace Vim.Format.Tests
{
#if NET5_0_OR_GREATER
    public record TestContext(string TestName, string BaseDirPath, string SubDirPath = null)
    {
        public static readonly string DefaultTestDir = Path.Combine(RepoPaths.OutDir, "_tests"); // leading underscore to find it easily in the explorer.

        public string DirPath
            => Path.Combine(new[] { BaseDirPath, SubDirPath }.Where(s => !string.IsNullOrEmpty(s)).ToArray());

        public string GetLogPath(string name = null)
            => Path.Combine(DirPath, $"{name ?? TestName}.log");

        public ILogger CreateLogger(string name = null)
            => Log.CreateLogger(name ?? TestName, GetLogPath(name ?? TestName));

        public string PrepareDirectory()
        {
            if (Directory.Exists(DirPath))
                Directory.Delete(DirPath, true);
            return Directory.CreateDirectory(DirPath).FullName;
        }

        public static string GetTestContextPath(
            string projectName,
            string testName,
            params string[] subDirPaths)
        {
            var pathSequence =
                new[] { DefaultTestDir, projectName, testName }.Concat(subDirPaths)
                .Where(s => !string.IsNullOrEmpty(s)).ToArray();

            return Path.Combine(pathSequence);
        }
    }

    /// <summary>
    /// A test context generated based on the caller information.
    /// </summary>
    public record CallerTestContext : TestContext
    {
        public CallerTestContext(
            [CallerFilePath] string sourceFilePath = null,
            [CallerMemberName] string testName = null,
            params string[] subDirComponents)
            : base(testName, GetTestContextPath(Path.GetFileNameWithoutExtension(sourceFilePath), testName, subDirComponents))
        {
            Debug.WriteLine($"Assembly Test Context: \"{TestName}\" @ \"{DirPath}\"");
        }
    }
#endif
}
