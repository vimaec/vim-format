using System;
using System.Collections.Generic;
using System.Linq;
using Vim.BFast;
using Vim.Util;

namespace Vim.Format
{
    public class EntityTable_v2
    {
        private readonly string[] _stringTable;

        public Dictionary<string, NamedBuffer<int>> IndexColumns { get; } = new Dictionary<string, NamedBuffer<int>>();
        public Dictionary<string, NamedBuffer<int>> StringColumns { get; } = new Dictionary<string, NamedBuffer<int>>();
        public Dictionary<string, INamedBuffer> DataColumns { get; } = new Dictionary<string, INamedBuffer>();

        /// <summary>
        /// The entity table name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The buffers which compose the columns.
        /// </summary>
        public INamedBuffer[] Columns { get; }

        /// <summary>
        /// The number of rows in the entity table.
        /// </summary>
        public int RowCount { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public EntityTable_v2(
            SerializableEntityTable et,
            string[] stringTable)
        {
            Name = et.Name;
            Columns = et.ValidateColumnRowsAreAligned();
            RowCount = Columns.FirstOrDefault()?.NumElements() ?? 0;

            foreach (var column in et.IndexColumns)
                IndexColumns[column.Name] = column;

            foreach (var column in et.StringColumns)
                StringColumns[column.Name] = column;

            foreach (var column in et.DataColumns)
                DataColumns[column.Name] = column;

            _stringTable = stringTable;
        }

        private static T GetColumnOrDefault<T>(Dictionary<string, T> map, string key, T defaultValue = default)
            => map.TryGetValue(key, out var result) ? result : defaultValue;

        /// <summary>
        /// Returns the index column based on the given column name.
        /// </summary>
        public int[] GetIndexColumnValues(string columnName)
            => GetColumnOrDefault(IndexColumns, columnName)?.GetColumnValues<int>();

        /// <summary>
        /// Returns the string column based on the given column name.
        /// </summary>
        public string[] GetStringColumnValues(string columnName)
        {
            var stringIndices = GetColumnOrDefault(StringColumns, columnName)
                 ?.GetColumnValues<int>() ?? Array.Empty<int>();

            var strings = new string[stringIndices.Length];

            for (var i = 0; i < strings.Length; i++)
            {
                strings[i] = _stringTable == null || _stringTable.Length == 0
                    ? "" // Guard against the case where the string buffer is null or empty.
                    : _stringTable.ElementAtOrDefault(stringIndices[i], "");
            }

            return strings;
        }

        /// <summary>
        /// Returns the data column based on the given column name.
        /// </summary>
        public T[] GetDataColumnValues<T>(string columnName) where T : unmanaged
        {
            var type = typeof(T);

            if (!ColumnExtensions.DataColumnTypes.Contains(type))
                throw new Exception($"{nameof(GetDataColumnValues)} error - unsupported data column type {type}");

            var namedBuffer = GetColumnOrDefault(DataColumns, columnName);
            if (namedBuffer == null)
                return null;

            if (type == typeof(short))
                return namedBuffer.GetColumnValues<int>().Select(i => (short)i).ToArray() as T[];

            if (type == typeof(bool))
                return namedBuffer.GetColumnValues<byte>().Select(b => b != 0).ToArray() as T[];

            return namedBuffer.GetColumnValues<T>();
        }
    }
}
