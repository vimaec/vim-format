using System;
using System.Collections.Generic;
using System.Linq;
using Vim.BFast;

namespace Vim.Format.api_v2
{
    /// <summary>
    /// Defines the serialized type information of entity table columns. 
    /// </summary>
    public class VimEntityTableColumnTypeInfo
    {
        public readonly VimEntityTableColumnType ColumnType;
        public readonly string TypePrefix;
        public readonly Type SerializedType;
        public readonly ISet<Type> CastTypes;

        /// <summary>
        /// Constructor
        /// </summary>
        public VimEntityTableColumnTypeInfo(
            VimEntityTableColumnType columnType, string typePrefix, Type serializedType, params Type[] castTypes)
        {
            (ColumnType, TypePrefix, SerializedType) = (columnType, typePrefix, serializedType);
            CastTypes = new HashSet<Type>(castTypes);
        }

        public IEnumerable<Type> RelatedTypes
            => CastTypes.Prepend(SerializedType);

        /// <summary>
        /// Returns the supported serialization types for each column type.
        /// </summary>
        public static readonly IReadOnlyCollection<VimEntityTableColumnTypeInfo> SupportedColumnTypes
            = new[]
            {
                new VimEntityTableColumnTypeInfo(
                    VimEntityTableColumnType.IndexColumn,  VimEntityTableColumnName.IndexColumnNameTypePrefix, typeof(int)),

                new VimEntityTableColumnTypeInfo(
                    VimEntityTableColumnType.StringColumn, VimEntityTableColumnName.StringColumnNameTypePrefix, typeof(int)),

                new VimEntityTableColumnTypeInfo(
                    VimEntityTableColumnType.DataColumn,   VimEntityTableColumnName.IntColumnNameTypePrefix, typeof(int), typeof(short)),

                new VimEntityTableColumnTypeInfo(
                    VimEntityTableColumnType.DataColumn,   VimEntityTableColumnName.LongColumnNameTypePrefix, typeof(long)),

                new VimEntityTableColumnTypeInfo(
                    VimEntityTableColumnType.DataColumn,   VimEntityTableColumnName.ByteColumnNameTypePrefix, typeof(byte), typeof(bool)),

                new VimEntityTableColumnTypeInfo(
                    VimEntityTableColumnType.DataColumn,   VimEntityTableColumnName.DoubleColumnNameTypePrefix, typeof(double)),

                new VimEntityTableColumnTypeInfo(
                    VimEntityTableColumnType.DataColumn,   VimEntityTableColumnName.FloatColumnNameTypePrefix, typeof(float)),
            };

        public static readonly IReadOnlyDictionary<string, VimEntityTableColumnType> TypePrefixToColumnTypeMap
            = SupportedColumnTypes.ToDictionary(t => t.TypePrefix, t => t.ColumnType);

        public static readonly IReadOnlyDictionary<Type, string> DataColumnTypeToPrefixMap
            = SupportedColumnTypes
                .Where(t => t.ColumnType == VimEntityTableColumnType.DataColumn)
                .SelectMany(t => t.RelatedTypes.Select(type => (Type: type, t.TypePrefix)))
                .ToDictionary(item => item.Type, item => item.TypePrefix);

        public static readonly IReadOnlyDictionary<string, Type> TypePrefixToSerializedTypeMap
            = SupportedColumnTypes
                .Where(t => t.ColumnType == VimEntityTableColumnType.DataColumn)
                .ToDictionary(t => t.TypePrefix, t => t.SerializedType);

        public static readonly ISet<Type> DataColumnTypes
            = new HashSet<Type>(SupportedColumnTypes
                .Where(t => t.ColumnType == VimEntityTableColumnType.DataColumn)
                .SelectMany(t => t.RelatedTypes));

        public static readonly ISet<string> DataColumnNameTypePrefixes
            = new HashSet<string>(SupportedColumnTypes
                .Where(t => t.ColumnType == VimEntityTableColumnType.DataColumn)
                .Select(t => t.TypePrefix));

        public static bool TryGetDataColumnType(INamedBuffer dc, out Type type)
        {
            type = null;

            if (!VimEntityTableColumnName.TryParseDataColumnNameTypePrefix(dc.Name, out var typePrefix))
                return false;

            return TypePrefixToSerializedTypeMap.TryGetValue(typePrefix, out type);
        }

        public static T[] GetDataColumnAsTypedArray<T>(IBuffer buffer) where T: unmanaged
        {
            if (buffer == null)
                return null;

            var type = typeof(T);

            if (type == typeof(short))
                return buffer.AsArray<int>().Select(i => (short)i).ToArray() as T[];

            if (type == typeof(bool))
                return buffer.AsArray<byte>().Select(b => b != 0).ToArray() as T[];

            return buffer.AsArray<T>();
        }
    }
}