using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Vim.Util
{
    // ReSharper disable once InconsistentNaming
    public static class IO
    {
        /// <summary>
        /// Deletes the file or directory (recursively) at the given path.
        /// </summary>
        public static void Delete(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        /// <summary>
        /// Deletes all contents in a folder
        /// https://stackoverflow.com/questions/1288718/how-to-delete-all-files-and-folders-in-a-directory
        /// </summary>
        public static void DeleteDirectoryContent(string folderPath)
        {
            var di = new DirectoryInfo(folderPath);

            foreach (var dir in di.EnumerateDirectories().AsParallel())
            {
                Directory.Delete(dir.FullName, true);
            }
            
            foreach (var file in di.EnumerateFiles().AsParallel())
            {
                file.Delete();
            }
        }

        /// <summary>
        /// Create the directory for the given filepath if it doesn't exist.
        /// </summary>
        public static string CreateFileDirectory(string filepath)
        {
            var dirPath = Path.GetDirectoryName(filepath);

            if (!string.IsNullOrEmpty(dirPath) && !Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            return filepath;
        }

        /// <summary>
        /// Creates a directory if needed, or clears all of its contents otherwise
        /// </summary>
        public static string CreateAndClearDirectory(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            else
            {
                DeleteDirectoryContent(dirPath);
            }

            return dirPath;
        }

        /// <summary>
        /// Returns true if the given directory contains no files or if the directory does not exist.
        /// </summary>
        public static bool DirectoryIsEmpty(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                return true;
            }

            return Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories).Length == 0;
        }

        /// <summary>
        /// Deletes the target filepath if it exists and creates the containing directory.
        /// </summary>
        public static string DeleteIfExistsAndCreateParentDirectory(string filepath)
        {
            // Delete the filepath (or directory) if it already exists.
            if (File.Exists(filepath)) { File.Delete(filepath); }
            else if (Directory.Exists(filepath)) { Directory.Delete(filepath, true); }

            // Create the target directory containing the output path.
            var fullPath = Path.GetFullPath(filepath);
            var fullDirPath = Path.GetDirectoryName(fullPath);
            Directory.CreateDirectory(fullDirPath);

            return filepath;
        }

        /// <summary>
        /// Recursively copies the files from the source directory to the target directory.
        /// </summary>
        public static void CopyDirectory(string sourceDirectory, string targetDirectory)
        {
            var diSource = new DirectoryInfo(sourceDirectory);
            var diTarget = new DirectoryInfo(targetDirectory);
            CopyAll(diSource, diTarget);
        }

        /// <summary>
        /// Recursively copies the files from the source DirectoryInfo to the target DirectoryInfo.
        /// </summary>
        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            foreach (var fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            foreach (var diSourceSubDir in source.GetDirectories())
            {
                var nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        /// <summary>
        /// Changes the directory and the extension of a file. The new extension may or may not be specified with a leading period.
        /// </summary>
        public static string ChangeDirectoryAndExt(string filePath, string newFolder, string newExt)
            => Path.ChangeExtension(IO.ChangeDirectory(filePath, newFolder), newExt); // TODO: move this to Vim.Util.IO


        /// <summary>
        /// Changes the directory of a file
        /// </summary>
        public static string ChangeDirectory(string filePath, string newFolder)
            => Path.Combine(newFolder, Path.GetFileName(filePath));


        /// <summary>
        /// Returns all the files in the given directory and optionally its subdirectories,
        /// or just returns the passed file.
        /// </summary>
        public static IEnumerable<string> GetFiles(string path, string searchPattern = "*", bool recurse = false)
            => File.Exists(path)
                ? Enumerable.Repeat(path, 1)
                : Directory.Exists(path)
                    ? Directory.EnumerateFiles(path, searchPattern,
                        recurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
                    : Array.Empty<string>();

        /// <summary>
        /// Returns the files in the given directory matching the given predicate function.
        /// </summary>
        public static IEnumerable<FileInfo> GetFilesInDirectoryWhere(string dirPath, Func<FileInfo, bool> predicateFn,
            bool recurse = true)
            => !Directory.Exists(dirPath)
                ? Enumerable.Empty<FileInfo>()
                : Directory.GetFiles(dirPath, "*",
                        recurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
                    .Select(f => new FileInfo(f))
                    .Where(predicateFn);

        /// <summary>
        /// Returns all the files in the given directory and its subdirectories,
        /// or just returns the passed file.
        /// </summary>
        public static IEnumerable<string> GetAllFiles(string path, string searchPattern = "*")
            => GetFiles(path, searchPattern, true);

        /// <summary>
        /// Generates a Regular Expression character set from an array of characters
        /// </summary>
        private static Regex CharSetToRegex(params char[] chars)
            => new Regex($"[{Regex.Escape(new string(chars))}]");

        /// <summary>
        /// Creates a regular expression for finding illegal file name characters.
        /// </summary>
        private static Regex InvalidFileNameRegex =>
            CharSetToRegex(Path.GetInvalidFileNameChars());

        /// <summary>
        /// Convert a string to a valid name
        /// https://stackoverflow.com/questions/146134/how-to-remove-illegal-characters-from-path-and-filenames
        /// https://stackoverflow.com/questions/2230826/remove-invalid-disallowed-bad-characters-from-filename-or-directory-folder?noredirect=1&lq=1
        /// https://stackoverflow.com/questions/10898338/c-sharp-string-replace-to-remove-illegal-characters?noredirect=1&lq=1
        /// </summary>
        public static string ToValidFileName(this string s, string replacement = "_", int maxLength = -1)
        {
            var replaced = InvalidFileNameRegex.Replace(s, m => replacement);
            
            if (maxLength >= 0 && maxLength != replaced.Length)
            {
                replaced = replaced.Substring(0, Math.Min(maxLength, replaced.Length));
            }

            return replaced;
        }

        /// <summary>
        /// Returns the file size in bytes, or 0 if there is no file.
        /// </summary>
        public static long FileSize(string fileName)
            => File.Exists(fileName) ? new FileInfo(fileName).Length : 0;

        /// <summary>
        /// Returns the file size in bytes, or 0 if there is no file.
        /// </summary>
        public static string FileSizeAsString(string fileName, int numPlacesToShow = 1)
            => StringFormatting.BytesToString(FileSize(fileName), numPlacesToShow);

        /// <summary>
        /// Returns the total file size of all files given
        /// </summary>
        public static long TotalFileSize(IEnumerable<string> files)
            => files.Sum(FileSize);

        /// <summary>
        /// Returns the total file size of all files given as a human readable string
        /// </summary>
        public static string TotalFileSizeAsString(IEnumerable<string> files, int numPlacesToShow = 1)
            => StringFormatting.BytesToString(TotalFileSize(files), numPlacesToShow);

        /// <summary>
        /// Given a full file path, collapses the full path into a checksum, and return a file name.
        /// </summary>
        public static string FilePathToUniqueFileName(string filePath)
            => filePath.Replace('/', '\\').MD5HashAsBitConverterLowerInvariant() + "_" + Path.GetFileName(filePath);

        public static Process OpenFolderInExplorer(string folderPath)
            => Process.Start("explorer.exe", folderPath);

        public static Process SelectFileInExplorer(string filePath)
            => Process.Start(new ProcessStartInfo
            {
                FileName = "explorer.exe",
                Arguments = $"/select,\"{filePath}\"",
                UseShellExecute = false
            });


        public static Process ShellExecute(string filePath)
            => Process.Start(new ProcessStartInfo { FileName = filePath, UseShellExecute = true });

        public static Process OpenFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("", filePath);

            // Expand the file name
            filePath = new FileInfo(filePath).FullName;

            // Open the file with the default file extension handler.
            try
            {
                return Process.Start(filePath);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }

            // If there is no default file extension handler, use shell execute
            try
            {
                return ShellExecute(filePath);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }

            // If that didn't work, show the file in explorer.
            return IO.SelectFileInExplorer(filePath);
        }

        /// <summary>
        /// Returns true if the URI is valid.
        /// </summary>
        public static bool IsValidUri(string uri)
        {
            // see: https://stackoverflow.com/a/33573227
            if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                return false;
            Uri tmp;
            if (!Uri.TryCreate(uri, UriKind.Absolute, out tmp))
                return false;
            return tmp.Scheme == Uri.UriSchemeHttp || tmp.Scheme == Uri.UriSchemeHttps;
        }

        /// <summary>
        /// Opens the URI in the browser and returns true if the uri is valid.
        /// </summary>
        public static bool OpenUri(string uri)
        {
            // see: https://stackoverflow.com/a/33573227
            if (!IsValidUri(uri))
                return false;
            Process.Start(new ProcessStartInfo(uri) { UseShellExecute = true });
            return true;
        }
    }
}
