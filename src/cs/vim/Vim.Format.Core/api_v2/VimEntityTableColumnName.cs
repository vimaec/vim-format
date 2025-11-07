using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Vim.BFast;

namespace Vim.Format.api_v2
{
    /// <summary>
    /// Parses an entity table column name into its components.
    /// </summary>
    public class VimEntityTableColumnName
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
        /// Matches the type prefix and the remainder of the column name.
        /// </summary>
        public static readonly Regex ColumnNameComponentsRegex
            = new Regex(@"(\w+:)(.+)", RegexOptions.Compiled);

        /// <summary>
        /// Matches the index column's components.
        /// </summary>
        public static readonly Regex IndexColumnNameComponentsRegex
            = new Regex(@"(\w+:)((?:\w|\.)+):(.+)", RegexOptions.Compiled);

        /// <summary>
        /// The type prefix. Ex: "string:" in "string:Name".
        /// </summary>
        public string TypePrefix { get; }

        /// <summary>
        /// The field name. Ex: "Name" in "string:Name".
        /// </summary>
        public string FieldName { get; }

        /// <summary>
        /// The related table name, if the column name is an index column. Otherwise null. Ex: "Vim.Level" in "index:Vim.Level:Level"
        /// </summary>
        public string RelatedTableName { get; }

        /// <summary>
        /// The column type. Ex: StringColumn if the columnName is "string:Name"
        /// </summary>
        public readonly VimEntityTableColumnType ColumnType;

        /// <summary>
        /// Constructor. Parses the column name into its components.
        /// </summary>
        public VimEntityTableColumnName(string columnName)
        {
            var match = ColumnNameComponentsRegex.Match(columnName);
            if (!match.Success)
                throw new Exception($"Column name {columnName} could not be separated into its components.");

            // Collect the type prefix.
            TypePrefix = match.Groups[1].Value;
            switch (TypePrefix)
            {
                case VimConstants.IndexColumnNameTypePrefix:
                    ColumnType = VimEntityTableColumnType.IndexColumn;
                    break;
                case VimConstants.StringColumnNameTypePrefix:
                    ColumnType = VimEntityTableColumnType.StringColumn;
                    break;
                default:
                    ColumnType = VimEntityTableColumnType.DataColumn;
                    break;
            }

            // Collect the field name
            FieldName = match.Groups[2].Value;

            // If it's an index column, get the related table.
            if (ColumnType == VimEntityTableColumnType.IndexColumn)
            {
                var indexMatch = IndexColumnNameComponentsRegex.Match(columnName);
                if (!match.Success)
                    throw new Exception($"Index column name {columnName} could not be separated into its components.");

                RelatedTableName = indexMatch.Groups[2].Value;
                FieldName = indexMatch.Groups[3].Value; // Update the field name.
            }
        }

        /// <summary>
        /// Constructor. Parses the given buffer's name into its components.
        /// </summary>
        public VimEntityTableColumnName(INamedBuffer namedBuffer)
            : this(namedBuffer.Name)
        { }

        /// <summary>
        /// Returns the parsed VimEntityTableColumnName or null if the column name could not be parsed.
        /// </summary>
        public static VimEntityTableColumnName ParseVimEntityTableColumnNameOrNull(string columnName)
            => TryParseVimEntityTableColumnName(columnName, out var value) ? value : null;

        /// <summary>
        /// Returns true along with the parsed VimEntityTableColumnName if the column could be parsed. Returns false otherwise.
        /// </summary>
        public static bool TryParseVimEntityTableColumnName(string columnName, out VimEntityTableColumnName columnComponents)
        {
            columnComponents = null;

            if (string.IsNullOrEmpty(columnName))
                return false;

            try
            {
                columnComponents = new VimEntityTableColumnName(columnName);
            }
            catch (Exception e)
            {
                // Invalid column name; could not be parsed.
                Debug.Fail($"Column components could not be parsed: {columnName}. Error: {e}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns true along with the type prefix of the column name if it could be parsed.
        /// </summary>
        public static bool TryParseColumnTypePrefix(string columnName, out string typePrefix)
        {
            typePrefix = null;

            var name = ParseVimEntityTableColumnNameOrNull(columnName);
            if (name == null)
                return false;

            typePrefix = name.TypePrefix;

            return true;
        }

        /// <summary>
        /// Returns true along with the data column type prefix of the column name if it is a valid data column name.
        /// </summary>
        public static bool TryParseDataColumnNameTypePrefix(string columnName, out string typePrefix)
        {
            typePrefix = null;

            var name = ParseVimEntityTableColumnNameOrNull(columnName);
            if (name == null)
                return false;

            if (!VimEntityTableColumnTypeInfo.DataColumnNameTypePrefixes.Contains(name.TypePrefix))
                return false;

            typePrefix = name.TypePrefix;

            return true;
        }

        public static bool IsDataColumnName(string columnName)
            => TryParseDataColumnNameTypePrefix(columnName, out _);

        public static string GetIndexColumnName(string relatedTableName, string localFieldName)
            => VimConstants.IndexColumnNameTypePrefix + relatedTableName + ":" + localFieldName;

        public static string GetRelatedTableNameFromColumnName(string name)
            => ParseVimEntityTableColumnNameOrNull(name)?.RelatedTableName;

        public static string GetRelatedTableName(INamedBuffer<int> ic)
            => GetRelatedTableNameFromColumnName(ic.Name);

        public static string GetIndexColumnFieldName(INamedBuffer<int> ic)
            => ParseVimEntityTableColumnNameOrNull(ic.Name)?.FieldName;

        public static string GetStringColumnFieldName(INamedBuffer<int> sc)
            => ParseVimEntityTableColumnNameOrNull(sc.Name)?.FieldName;

        public static string GetDataColumnFieldName(INamedBuffer dc)
            => ParseVimEntityTableColumnNameOrNull(dc.Name)?.FieldName;
    }
}