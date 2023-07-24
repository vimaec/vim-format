using System.IO;
using System.Linq;

namespace Vim.Util.Tests
{
    /// <summary>
    /// A global interface to the repository's path configurations.
    /// </summary>
    public static class RepoPaths
    {

        /// <summary>
        /// This value ProjDir is set by a pre-build step to our projects folder
        /// We use it to set our Repo dir (which is the parent of this folder)
        /// </summary>
        public static readonly string ProjDir = new DirectoryInfo(Properties.Resources.ProjDir.Trim()).FullName;

        public static string SrcDir => Path.Combine(ProjDir, "..", "..", "..", "..", "src");
        public static string OutDir => Path.Combine(ProjDir, "..", "..", "..", "..", "out");
        public static string DataDir => Path.Combine(ProjDir, "..", "..", "..", "..", "data");

        /// <summary>
        /// Returns the file path to the highest versioned mechanical room file among the version snapshots.
        /// </summary>
        public static string GetLatestWolfordResidenceVim()
        {
            var matchingVim = Directory.GetFiles(DataDir, "Wolford_Residence*.vim", SearchOption.AllDirectories).FirstOrDefault();

            if (matchingVim == null)
                throw new FileNotFoundException($"Could not find the latest Wolford Residence VIM.");

            return matchingVim;
        }
    }
}
