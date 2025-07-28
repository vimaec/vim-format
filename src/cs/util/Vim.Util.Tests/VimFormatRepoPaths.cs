using System.IO;
using System.Linq;

namespace Vim.Util.Tests
{
    /// <summary>
    /// A global interface to the repository's path configurations.
    /// </summary>
    public static class VimFormatRepoPaths
    {
        /// <summary>
        /// This value ProjDir is set by a pre-build step to our projects folder
        /// We use it to set our Repo dir (which is the parent of this folder)
        /// </summary>
        public static readonly string ProjDir = new DirectoryInfo(Properties.Resources.ProjDir.Trim()).FullName;

        public static string RootDir => Path.Combine(ProjDir, "..", "..", "..", "..");
        public static string DocsDir => Path.Combine(RootDir, "docs");
        public static string SrcDir => Path.Combine(RootDir, "src");
        public static string OutDir => Path.Combine(RootDir, "out");
        public static string DataDir => Path.Combine(RootDir, "data");

        /// <summary>
        /// Returns the file path to the highest versioned file among the version snapshots.
        /// Files matched by the pattern are ordered by name in ascending order.
        /// </summary>
        public static string GetDataFilePath(string filePattern, bool last)
        {
            var matches = Directory.GetFiles(DataDir, filePattern, SearchOption.AllDirectories)
                .OrderBy(f => f);

            var matchingVim = last ? matches.LastOrDefault() : matches.FirstOrDefault();

            if (matchingVim == null)
                throw new FileNotFoundException($"Could not find any files matching the pattern: {filePattern}");

            return matchingVim;
        }
    }
}
