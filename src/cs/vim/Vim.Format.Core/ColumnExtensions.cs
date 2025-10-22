using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Vim.BFast;

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

        public static readonly IReadOnlyDictionary<string, ColumnType> TypePrefixToColumnTypeMap
            = AllColumnInfos.ToDictionary(t => t.TypePrefix, t => t.ColumnType);

        public static readonly IReadOnlyDictionary<Type, string> DataColumnTypeToPrefixMap
            = AllColumnInfos
                .Where(t => t.ColumnType == ColumnType.DataColumn)
                .SelectMany(t => t.RelatedTypes.Select(type => (Type: type, t.TypePrefix)))
                .ToDictionary(item => item.Type, item => item.TypePrefix);

        public static readonly IReadOnlyDictionary<string, Type> TypePrefixToSerializedTypeMap
            = AllColumnInfos
                .Where(t => t.ColumnType == ColumnType.DataColumn)
                .ToDictionary(t => t.TypePrefix, t => t.SerializedType);

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

        public class StringOrDataColumnNameComponents
        {
            public static readonly Regex StringOrDataColumnNameComponentsRegex = new Regex(@"(\w+:)(.+)");

            public readonly string TypePrefix;
            public readonly string FieldName;

            public StringOrDataColumnNameComponents(string stringOrDataColumnName)
            {
                var match = StringOrDataColumnNameComponentsRegex.Match(stringOrDataColumnName);
                if (!match.Success)
                    throw new Exception($"Column name {stringOrDataColumnName} could not be separated into its components.");

                TypePrefix = match.Groups[1].Value;
                FieldName = match.Groups[2].Value;
            }
        }

        public class IndexColumnNameComponents
        {
            public static readonly Regex IndexColumnNameComponentsRegex = new Regex(@"(\w+:)((?:\w|\.)+):(.+)");

            public readonly string TypePrefix;
            public readonly string TableName;
            public readonly string FieldName;

            public IndexColumnNameComponents(string indexColumnName)
            {
                var match = IndexColumnNameComponentsRegex.Match(indexColumnName);
                if (!match.Success)
                    throw new Exception($"Index column name {indexColumnName} could not be separated into its components.");

                TypePrefix = match.Groups[1].Value;
                TableName = match.Groups[2].Value;
                FieldName = match.Groups[3].Value;
            }
        }

        public static IndexColumnNameComponents SplitIndexColumnName(string name)
            => new IndexColumnNameComponents(name);

        public static string GetRelatedTableNameFromColumnName(string name)
            => SplitIndexColumnName(name).TableName;

        public static string GetFieldNameFromIndexColumnName(string name)
            => SplitIndexColumnName(name).FieldName;

        public static string GetRelatedTableName(this INamedBuffer<int> ic)
            => GetRelatedTableNameFromColumnName(ic.Name);

        public static EntityTable GetRelatedTable(this INamedBuffer<int> ic, Document doc)
            => doc.GetTable(ic.GetRelatedTableName());

        public static string GetIndexColumnFieldName(this INamedBuffer<int> ic)
            => GetFieldNameFromIndexColumnName(ic.Name);

        public static string GetStringColumnFieldName(this INamedBuffer<int> sc)
            => new StringOrDataColumnNameComponents(sc.Name).FieldName;

        public static string GetDataColumnFieldName(this INamedBuffer dc)
            => new StringOrDataColumnNameComponents(dc.Name).FieldName;

        public static bool TryGetDataColumnType(this INamedBuffer dc, out Type type)
        {
            type = null;
            var typePrefix = new StringOrDataColumnNameComponents(dc.Name).TypePrefix;
            return TypePrefixToSerializedTypeMap.TryGetValue(typePrefix, out type);
        }
    }
}
