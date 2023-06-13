using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Vim.DataFormat
{
    public enum ValueSerializationStrategy
    {
        SerializeAsStringColumn,
        SerializeAsDataColumn,
        SerializeAsCompositeDataColumns,
    }

    public static partial class ColumnExtensions
    {
        public static readonly IReadOnlyCollection<ColumnInfo> AllColumnInfos
            = new[]
            {
                new ColumnInfo(ColumnType.IndexColumn, VimConstants.IndexColumnNameTypePrefix, typeof(int)),
                new ColumnInfo(ColumnType.StringColumn, VimConstants.StringColumnNameTypePrefix, typeof(int)),
                new ColumnInfo(ColumnType.DataColumn, VimConstants.IntColumnNameTypePrefix, typeof(int), typeof(short)),
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

        public static ColumnType GetColumnTypeFromTypePrefix(string typePrefix)
        {
            if (TypePrefixToColumnTypeMap.TryGetValue(typePrefix, out var columnType))
                return columnType;

            throw new Exception($"{nameof(GetColumnTypeFromTypePrefix)} error: {typePrefix} is not associated to a column name prefix.");
        }

        public const string RelatedTableNameFieldNameSeparator = ":";

        public static string GetIndexColumnName(string relatedTableName, string localFieldName)
            => VimConstants.IndexColumnNameTypePrefix + relatedTableName + RelatedTableNameFieldNameSeparator + localFieldName;

        public static string GetDataColumnNameTypePrefix(this Type type)
        {
            if (DataColumnTypeToPrefixMap.TryGetValue(type, out var typePrefix))
                return typePrefix;
            
            throw new Exception($"{nameof(GetDataColumnNameTypePrefix)} error: no matching data column name prefix for {type}");
        }

        public static bool CanSerializeAsStringColumn(this Type type)
            => type == typeof(string);

        public static bool CanSerializeAsDataColumn(this Type type)
            => DataColumnTypes.Contains(type);

        public static bool CanSerializeAsCompositeDataColumns(this Type type)
            => CompositeTypeMap.ContainsKey(type);

        public static ValueSerializationStrategy GetValueSerializationStrategy(this Type type)
        {
            if (type.CanSerializeAsStringColumn())
                return ValueSerializationStrategy.SerializeAsStringColumn;

            if (type.CanSerializeAsDataColumn())
                return ValueSerializationStrategy.SerializeAsDataColumn;

            if (type.CanSerializeAsCompositeDataColumns())
                return ValueSerializationStrategy.SerializeAsCompositeDataColumns;

            throw new Exception($"{nameof(GetValueSerializationStrategy)} error - could not find serialization strategy for {type}");
        }

        public static (ValueSerializationStrategy Strategy, string TypePrefix) GetValueSerializationStrategyAndTypePrefix(this Type type)
        {
            var strategy = type.GetValueSerializationStrategy();
            string typePrefix = null;

            switch (strategy)
            {
                case ValueSerializationStrategy.SerializeAsStringColumn:
                    typePrefix = VimConstants.StringColumnNameTypePrefix;
                    break;
                case ValueSerializationStrategy.SerializeAsDataColumn:
                    typePrefix = type.GetDataColumnNameTypePrefix();
                    break;
                case ValueSerializationStrategy.SerializeAsCompositeDataColumns:
                    typePrefix = ""; // The type prefix is computed inside GetCompositeDataColumnValues.
                    break;
            }

            return (strategy, typePrefix);
        }

        public static IEnumerable<string> GetValueColumnNames(this FieldInfo fieldInfo)
        {
            var (strategy, typePrefix) = fieldInfo.FieldType.GetValueSerializationStrategyAndTypePrefix();
            switch (strategy)
            {
                case ValueSerializationStrategy.SerializeAsStringColumn:
                case ValueSerializationStrategy.SerializeAsDataColumn:
                    return new[] { $"{typePrefix}{fieldInfo.Name}" };
                case ValueSerializationStrategy.SerializeAsCompositeDataColumns:
                    return CompositeTypeMap[fieldInfo.FieldType].GetDataColumnNames(fieldInfo.Name);
                default:
                    throw new Exception($"{nameof(GetValueColumnNames)} error - could not find serialization strategy for {fieldInfo}");
            }
        }
    }
}
