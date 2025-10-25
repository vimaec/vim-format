using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public class ColumnNameComponents
        {
            public static readonly Regex ColumnNameComponentsRegex = new Regex(@"(\w+:)(.+)", RegexOptions.Compiled);
            public static readonly Regex IndexColumnNameComponentsRegex = new Regex(@"(\w+:)((?:\w|\.)+):(.+)", RegexOptions.Compiled);

            public readonly string TypePrefix;
            public readonly string FieldName;
            public readonly string RelatedTableName; // only applicable for index columns.
            public readonly ColumnType ColumnType;

            public ColumnNameComponents(string columnName)
            {
                var match = ColumnNameComponentsRegex.Match(columnName);
                if (!match.Success)
                    throw new Exception($"Column name {columnName} could not be separated into its components.");

                // Collect the type prefix.
                TypePrefix = match.Groups[1].Value;
                switch (TypePrefix)
                {
                    case VimConstants.IndexColumnNameTypePrefix:
                        ColumnType = ColumnType.IndexColumn;
                        break;
                    case VimConstants.StringColumnNameTypePrefix:
                        ColumnType = ColumnType.StringColumn;
                        break;
                    default:
                        ColumnType = ColumnType.DataColumn;
                        break;
                }

                // Collect the field name
                FieldName = match.Groups[2].Value;

                // If it's an index column, get the related table.
                if (ColumnType == ColumnType.IndexColumn)
                {
                    var indexMatch = IndexColumnNameComponentsRegex.Match(columnName);
                    if (!match.Success)
                        throw new Exception($"Index column name {columnName} could not be separated into its components.");

                    RelatedTableName = indexMatch.Groups[2].Value;
                    FieldName = indexMatch.Groups[3].Value; // Update the field name.
                }
            }

            public static bool TryGetColumnNameComponents(string columnName, out ColumnNameComponents columnComponents)
            {
                columnComponents = null;

                try
                {
                    columnComponents = new ColumnNameComponents(columnName);
                }
                catch (Exception e)
                {
                    // Invalid column name; could not be parsed.
                    Debug.Fail($"Column components could not be parsed: {columnName}. Error: {e}");
                    return false;
                }

                return true;
            }
        }

        public static string GetRelatedTableNameFromColumnName(string name)
            => new ColumnNameComponents(name).RelatedTableName;

        public static string GetRelatedTableName(this INamedBuffer<int> ic)
            => GetRelatedTableNameFromColumnName(ic.Name);

        public static EntityTable GetRelatedTable(this INamedBuffer<int> ic, Document doc)
            => doc.GetTable(ic.GetRelatedTableName());

        public static string GetIndexColumnFieldName(this INamedBuffer<int> ic)
            => new ColumnNameComponents(ic.Name).FieldName;

        public static string GetStringColumnFieldName(this INamedBuffer<int> sc)
            => new ColumnNameComponents(sc.Name).FieldName;

        public static string GetDataColumnFieldName(this INamedBuffer dc)
            => new ColumnNameComponents(dc.Name).FieldName;

        public static bool TryGetDataColumnType(this INamedBuffer dc, out Type type)
        {
            type = null;
            var typePrefix = new ColumnNameComponents(dc.Name).TypePrefix;
            return TypePrefixToSerializedTypeMap.TryGetValue(typePrefix, out type);
        }

        public static bool TryGetColumnByFieldName(
            this SerializableEntityTable entityTable,
            string columnFieldName,
            out INamedBuffer buffer,
            out string typePrefix,
            out ColumnType columnType)
        {
            buffer = null;
            columnType = ColumnType.IndexColumn;
            typePrefix = null;

            foreach (var column in entityTable.GetAllColumns())
            {
                if (!ColumnNameComponents.TryGetColumnNameComponents(column.Name, out var components))
                    continue;

                if (components.FieldName != columnFieldName)
                    continue;

                columnType = components.ColumnType;
                typePrefix = components.TypePrefix;
                buffer = column;

                return true;
            }

            return false;
        }
    }
}
