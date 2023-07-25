using System;

namespace Vim.Util
{
    public static class SpecialFolders
    {
        public static string RoamingAppData =>
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static string LocalAppData =>
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public static string ProgramData
            => Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

        public static string MyDocuments =>
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public static string ProgramFiles
            => Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

        public static string UserProfile
            => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        public static string CommonProgramFiles
            => Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles);
    }
}
