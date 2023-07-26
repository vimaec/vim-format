using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vim.Util;

namespace Vim.Format
{
    public static partial class ColumnExtensions
    {
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

        public static ValueSerializationStrategy GetValueSerializationStrategy(this Type type)
        {
            if (type.CanSerializeAsStringColumn())
                return ValueSerializationStrategy.SerializeAsStringColumn;

            if (type.CanSerializeAsDataColumn())
                return ValueSerializationStrategy.SerializeAsDataColumn;

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
            }

            return (strategy, typePrefix);
        }

        public static string GetSerializedValueName(this FieldInfo f)
            => f.Name.Replace('_', '.'); // "Box_Min_X" => "Box.Min.X"

        public static string GetSerializedValueColumnName(this FieldInfo fieldInfo)
        {
            var (_, typePrefix) = fieldInfo.FieldType.GetValueSerializationStrategyAndTypePrefix();
            return $"{typePrefix}{fieldInfo.GetSerializedValueName()}";
        }

        public static string GetSerializedIndexName(this FieldInfo fieldInfo)
            => fieldInfo.Name.Trim('_');

        public static bool IsRelationType(this Type t)
            => t.Name == "Relation`1";

        public static Type RelationTypeParameter(this Type t)
            => t.GenericTypeArguments[0];

        public static IEnumerable<FieldInfo> GetRelationFields(this Type t)
            => t.GetFields().Where(fi => fi.FieldType.IsRelationType());

        public static string GetEntityTableName(this Type t)
            => (t.GetCustomAttribute(typeof(TableNameAttribute)) as TableNameAttribute)?.Name;

        public static (string IndexColumnName, string LocalFieldName) GetIndexColumnInfo(this FieldInfo fieldInfo)
        {
            if (!fieldInfo.Name.StartsWith("_"))
                throw new Exception("Relation field info names must start with a leading underscore");

            var localFieldName = fieldInfo.Name.Substring(1);

            var relationTypeParameter = fieldInfo.FieldType.RelationTypeParameter();
            var relatedTableName = relationTypeParameter.GetEntityTableName();
            if (string.IsNullOrEmpty(relatedTableName))
                throw new Exception($"Could not find related table for type {relationTypeParameter}");

            return (GetIndexColumnName(relatedTableName, localFieldName), localFieldName);
        }

        public static string GetSerializedIndexColumnName(this FieldInfo fieldInfo)
        {
            var (result, _) = fieldInfo.GetIndexColumnInfo();
            return result;
        }

        public static IEnumerable<FieldInfo> GetEntityFields(this Type t, bool skipIndex = true, bool skipProperties = true)
            => t.GetFields().Where(fi =>
                    !fi.FieldType.Equals(typeof(Document))
                    && (skipIndex ? fi.Name != "Index" : true)
                    && (skipProperties ? fi.Name != "Properties" : true)
                    && !fi.FieldType.IsRelationType()
                    && !fi.IsLiteral // do not include const fields
            );

        public static EntityColumnLoaderAttribute[] GetOrderedEntityColumnLoaderAttributes(this FieldInfo fi, SerializableVersion currentSchemaVersion)
            => fi.GetCustomAttributes<EntityColumnLoaderAttribute>()
                .Prepend(new EntityColumnLoaderAttribute(fi, currentSchemaVersion)) // provide the default loader; it has the highest priority because its object model version is the current one.
                .OrderByDescending(a =>
                {
                    var v = a.ObjectModelVersion;
                    return (v.Major, v.Minor, v.Patch, v.Qualifier);
                })
                .ToArray();
    }
}
