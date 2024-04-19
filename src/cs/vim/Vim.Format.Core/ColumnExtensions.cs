using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Vim.Format
{
    public static partial class ColumnExtensions
    {
        public static readonly IReadOnlyCollection<ColumnInfo> AllColumnInfos
            = new[]
            {
                new ColumnInfo(ColumnType.IndexColumn, VimConstants.IndexColumnNameTypePrefix, typeof(int)),
                new ColumnInfo(ColumnType.StringColumn, VimConstants.StringColumnNameTypePrefix, typeof(int)),
                new ColumnInfo(ColumnType.DataColumn, VimConstants.IntColumnNameTypePrefix, typeof(int), typeof(short)),
                new ColumnInfo(ColumnType.DataColumn, VimConstants.LongColumnNameTypePrefix, typeof(long)),
                new ColumnInfo(ColumnType.DataColumn, VimConstants.ByteColumnNameTypePrefix, typeof(byte), typeof(bool)),
                new ColumnInfo(ColumnType.DataColumn, VimConstants.DoubleColumnNameTypePrefix, typeof(double)),
                new ColumnInfo(ColumnType.DataColumn, VimConstants.FloatColumnNameTypePrefix, typeof(float)),
            };

        public static readonly IReadOnlyDictionary<Type, string> DataColumnTypeToPrefixMap
            = AllColumnInfos
                .Where(t => t.ColumnType == ColumnType.DataColumn)
                .SelectMany(t => t.RelatedTypes.Select(type => (Type: type, t.TypePrefix)))
                .ToDictionary(item => item.Type, item => item.TypePrefix);

        public static readonly ISet<Type> DataColumnTypes
            = new HashSet<Type>(AllColumnInfos.Where(t => t.ColumnType == ColumnType.DataColumn).SelectMany(t => t.RelatedTypes));

        public static readonly ISet<string> DataColumnNameTypePrefixes
            = new HashSet<string>(AllColumnInfos.Where(t => t.ColumnType == ColumnType.DataColumn).Select(t => t.TypePrefix));

        public static readonly Regex DataColumnTypePrefixRegex
            = new Regex($@"^(?:{string.Join("|", DataColumnNameTypePrefixes)})");

        public static bool TryGetDataColumnNameTypePrefix(string columnName, out string typePrefix)
        {
            typePrefix = null;
            if (string.IsNullOrEmpty(columnName))
                return false;

            var match = DataColumnTypePrefixRegex.Match(columnName);
            typePrefix = match.Value;
            return match.Success;
        }

        public static bool IsDataColumnName(string columnName)
            => TryGetDataColumnNameTypePrefix(columnName, out _);

        public const string RelatedTableNameFieldNameSeparator = ":";

        public static string GetIndexColumnName(string relatedTableName, string localFieldName)
            => VimConstants.IndexColumnNameTypePrefix + relatedTableName + RelatedTableNameFieldNameSeparator + localFieldName;
    }
}
