using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
    }
}
