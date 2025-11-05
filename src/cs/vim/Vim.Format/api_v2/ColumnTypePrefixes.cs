using System.Text.RegularExpressions;
using Vim.BFast;

namespace Vim.Format.api_v2
{
    public static class ColumnTypePrefixes
    {
        // Index column prefix

        public const string IndexColumnNameTypePrefix = "index:";

        // String column prefix

        public const string StringColumnNameTypePrefix = "string:";

        // Data column prefixes

        public const string IntColumnNameTypePrefix = "int:";
        public const string UintColumnNameTypePrefix = "uint:"; // unused for now
        public const string LongColumnNameTypePrefix = "long:";
        public const string UlongColumnNameTypePrefix = "ulong:"; // unused for now
        public const string ByteColumnNameTypePrefix = "byte:";
        public const string UbyteColumNameTypePrefix = "ubyte:"; // unused for now
        public const string FloatColumnNameTypePrefix = "float:";
        public const string DoubleColumnNameTypePrefix = "double:";


        /// <summary>
        /// A regular expression which matches the column type prefix, ex: "index:" or "string:" or "int:", etc.
        /// </summary>
        public static readonly Regex ColumnTypePrefixRegex = new Regex(@"(\w+:).*");

        /// <summary>
        /// Returns the type prefix of the column name.
        /// </summary>
        public static string GetColumnTypePrefix(string columnName)
        {
            var match = ColumnTypePrefixRegex.Match(columnName);
            return match.Success ? match.Groups[1].Value : "";
        }

        /// <summary>
        /// Returns the type prefix of the buffer's name.
        /// </summary>
        public static string GetColumnTypePrefix(INamedBuffer namedBuffer)
            => GetColumnTypePrefix(namedBuffer.Name);
    }
}
