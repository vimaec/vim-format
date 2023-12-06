using System.Text.RegularExpressions;
using Vim.Buffers;

namespace Vim.Format
{
    public static class Serializer
    {
        public static readonly Regex TypePrefixRegex = new Regex(@"(\w+:).*");

        public static string GetTypePrefix(this string name)
        {
            var match = TypePrefixRegex.Match(name);
            return match.Success ? match.Groups[1].Value : "";
        }

        /// <summary>
        /// Returns the named buffer prefix, or null if no prefix was found.
        /// </summary>
        public static string GetTypePrefix(this INamedBuffer namedBuffer)
            => namedBuffer.Name.GetTypePrefix();

    }
}
