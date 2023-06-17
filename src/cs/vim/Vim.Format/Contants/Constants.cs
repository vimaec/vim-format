using System;
using System.IO;
using System.Linq;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace Vim.Format
{
    /// <summary>
    /// Common constants shared across projects.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The current assembly.
        /// </summary>
        public static readonly Assembly Assembly = typeof(Constants).Assembly;

        /// <summary>
        /// Returns the assembly attribute (or null if not found).
        /// </summary>
        public static T GetAssemblyAttribute<T>()
            => (T)Assembly.GetCustomAttributes(typeof(T), true).FirstOrDefault();

        /// <summary>
        /// The properly capitalized VIM product name.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static readonly string VIM = GetAssemblyAttribute<AssemblyProductAttribute>().Product; // see: AssemblyInfo.Common.cs

        /// <summary>
        /// The properly capitalized Desktop product name.
        /// </summary>
        public static readonly string Desktop = "Desktop";

        /// <summary>
        /// The company name with proper capitalization.
        /// </summary>
        public static readonly string CompanyName = GetAssemblyAttribute<AssemblyCompanyAttribute>().Company; // see: AssemblyInfo.Common.cs

        /// <summary>
        /// The file extension with a prefixed period.
        /// </summary>
        public const string VimFileExtension = ".vim";

        /// <summary>
        /// The VIM project file extension with a prefixed period.
        /// </summary>
        public const string VimProjectExtension = ".vimprojx"; // ".vimprojx" - "x" for experimental

        /// <summary>
        /// The current assembly version.
        /// </summary>
        public static readonly Version AssemblyVersion = Assembly.GetName().Version; // see: AssemblyInfo.Common.cs

        /// <summary>
        /// The version string formatted as follows: "vX.X.X"
        /// </summary>
        public static readonly string VersionString = $"v{AssemblyVersion.Major}.{AssemblyVersion.Minor}.{AssemblyVersion.Build}";

        /// <summary>
        /// The version string, the release type and the commit hash (reverts to the VersionString in a public release)
        /// </summary>
        public static readonly string QualifiedVersionString;

        /// <summary>
        /// The commit hash used to build this assembly.
        /// </summary>
        public static readonly string CommitHash;

        /// <summary>
        /// Returns the first n characters of the commit hash used to build this assembly.
        /// </summary>
        public static string ShortCommitHash(int n)
            => CommitHash.Substring(0, n);

        /// <summary>
        /// The UTC time when this assembly was built.
        /// </summary>
        public static DateTime UtcBuildTime;

        /// <summary>
        /// Returns the trimmed string contents of the embedded resource file name.
        /// These resources are typically generated in PreBuildEvents in this project.
        /// </summary>
        private static string ReadEmbeddedResource(string resourceFileName)
        {
            using (var stream = Assembly.GetManifestResourceStream($"{Assembly.GetName().Name}.Resources.{resourceFileName}"))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd().Trim();
            }
        }

        /// <summary>
        /// Static initializer
        /// </summary>
        static Constants()
        {
            CommitHash = ReadEmbeddedResource("commit_hash.txt");
            UtcBuildTime = DateTime.Parse(ReadEmbeddedResource("utc_time.txt"));

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            QualifiedVersionString = $"{VersionString} - {ReleaseType.ToString("G").ToLowerInvariant()} (#{ShortCommitHash(6)}) {UtcBuildTime:yyyy-MM-dd}";
        }

        /// <summary>
        /// The directory path of the currently running executable.
        /// </summary>
        public static string ExeDirectoryPath
            // See: https://stackoverflow.com/questions/52797/how-do-i-get-the-path-of-the-assembly-the-code-is-in#comment2935622_52797
            => AppDomain.CurrentDomain.BaseDirectory;
            // NOTE: the approach below fails during NUnit tests, preserving it for documentation for future maintainers.
            //=> Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

        /// <summary>
        /// Appends VIM to the given base directory.
        /// </summary>
        public static string GetVimDir(string baseDir)
            => Path.Combine(baseDir, VIM);

        /// <summary>
        /// Appends VIM to the combined path.
        /// </summary>
        public static string GetVimDir(params string[] path)
            => GetVimDir(Path.Combine(path));

        /// <summary>
        /// Appends VIM to the special folder.
        /// </summary>
        public static string GetVimDir(Environment.SpecialFolder specialFolder)
            => GetVimDir(Environment.GetFolderPath(specialFolder));

        /// <summary>
        /// The path to the user's VIM directory.
        /// </summary>
        public static string UserProfileVimDir
            => GetVimDir(Environment.SpecialFolder.UserProfile);

        /// <summary>
        /// The path to the LocalAppData VIM directory.
        /// </summary>
        public static string LocalAppDataVimDir
            => GetVimDir(Environment.SpecialFolder.LocalApplicationData);

        /// <summary>
        /// The path to the LocalAppData VIM Desktop directory.
        /// </summary>
        public static string LocalAppDataVimDesktopDir
            => Path.Combine(LocalAppDataVimDir, Desktop);

        /// <summary>
        /// The path to the My Documents VIM directory.
        /// </summary>
        public static string MyDocumentsVimDir
            => GetVimDir(Environment.SpecialFolder.MyDocuments);

        /// <summary>
        /// The path to the Program Files VIM directory
        /// </summary>
        public static string ProgramFilesVimDir
            => GetVimDir(Environment.SpecialFolder.ProgramFiles);

        /// <summary>
        /// The path to the temporary VIM directory.
        /// NOTE: This avoids the direct usage of Path.GetTempPath() because in Revit 2020, the Revit developers had the wise idea to append a custom GUID to the returned path.
        /// See: https://stackoverflow.com/q/56984043
        /// </summary>
        public static string TempVimDir
            => GetVimDir(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Temp");

        /// <summary>
        /// The release type of the built executable.
        /// </summary>
        // NOTE: This is controlled by the VIM_RELEASE_TYPE environment variable when the assembly was compiled (see Directory.Build.Props and build.bat).
        public const VimReleaseType ReleaseType =
#if VIM_RELEASE_TYPE_PUBLIC
            VimReleaseType.Public;
#elif VIM_RELEASE_TYPE_INTERNAL
            VimReleaseType.Internal;
#elif VIM_RELEASE_TYPE_TESTING
            VimReleaseType.Testing;
#elif VIM_RELEASE_TYPE_DEVELOPMENT
            VimReleaseType.Development;
#else
            VimReleaseType.Development; // default
#endif

        public const DeploymentTarget DefaultDeploymentTarget =
            ReleaseType == VimReleaseType.Public
                ? DeploymentTarget.Production
                : ReleaseType == VimReleaseType.Internal
                    ? DeploymentTarget.Staging
                    : ReleaseType == VimReleaseType.Testing
                        ? DeploymentTarget.Testing
                        : DeploymentTarget.Development;

        public static class EnvironmentVariables
        {
            /// <summary>
            /// The environment variable which forces the application to enter the OOBE state.
            /// </summary>
            public const string DesktopOOBEName = "VIM_DESKTOP_OOBE";
            public static string DesktopOOBE => Environment.GetEnvironmentVariable(DesktopOOBEName);

            public const string DeploymentTargetName = "VIM_DEPLOYMENT_TARGET";
            public static string DeploymentTarget => Environment.GetEnvironmentVariable(DeploymentTargetName);
        }

        public static DeploymentTarget DeploymentTargetFromEnvironmentOrDefault
        {
            get {
                var envVar = Constants.EnvironmentVariables.DeploymentTarget;
                if (string.IsNullOrEmpty(envVar))
                    return DefaultDeploymentTarget;

                if (Enum.TryParse<DeploymentTarget>(envVar, true, out var result))
                    return result;

                return DefaultDeploymentTarget;
            }
        }

        public const string FeedbackUrl = "https://www.vimaec.com/contact";
        public const string HelpUrl = "https://www.vimaec.com/support";

        public static readonly Deployment VimCloudDeployment
            = new Deployment(
                production: "https://cloud.vimaec.com",
                staging: "https://saas-stage.vimaec.com",
                testing: "https://saas-dev.vimaec.com",
                development: "https://saas-dev.vimaec.com");

        public static Uri GetVimCloudUri(DeploymentTarget? target = null)
            => VimCloudDeployment.GetUri(target ?? DeploymentTargetFromEnvironmentOrDefault);

        public static readonly Deployment VimCloudApiDeployment
            = new Deployment(
                production: "https://saas-api.vimaec.com",
                staging: "https://saas-api-stage.vimaec.com",
                testing: "https://saas-api-dev.vimaec.com",
                development: "https://saas-api-dev.vimaec.com");

        public static Uri GetVimCloudApiUri(DeploymentTarget? target = null)
            => VimCloudApiDeployment.GetUri(target ?? DeploymentTargetFromEnvironmentOrDefault);
    }
}
