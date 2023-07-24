using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Vim.Util
{
    // ReSharper disable once InconsistentNaming
    public static class IO
    {
        /// <summary>
        /// Create the directory for the given filepath if it doesn't exist.
        /// </summary>
        public static string CreateFileDirectory(string filepath)
        {
            var dirPath = Path.GetDirectoryName(filepath);
            if (!string.IsNullOrEmpty(dirPath) && !Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            return filepath;
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
            => Util.BytesToString(FileSize(fileName), numPlacesToShow);

        /// <summary>
        /// Returns the total file size of all files given
        /// </summary>
        public static long TotalFileSize(IEnumerable<string> files)
            => files.Sum(FileSize);

        /// <summary>
        /// Returns the total file size of all files given as a human readable string
        /// </summary>
        public static string TotalFileSizeAsString(IEnumerable<string> files, int numPlacesToShow = 1)
            => Util.BytesToString(TotalFileSize(files), numPlacesToShow);

        /// <summary>
        /// Given a full file path, collapses the full path into a checksum, and return a file name.
        /// </summary>
        public static string FilePathToUniqueFileName(string filePath)
            => filePath.Replace('/', '\\').MD5HashAsBitConverterLowerInvariant() + "_" + Path.GetFileName(filePath);

    }
}
