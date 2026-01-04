using System;
using System.Collections.Generic;
using System.Linq;
using Vim.BFast;
using Vim.Util;

namespace Vim.Format
{
    public interface IVimEntityTable
    {
        int RowCount { get; }
    }

    public class VimEntityTable : IVimEntityTable
    {
        public VimEntityTableData TableData { get; }
        public string Name => TableData.Name;
        protected string[] StringTable { get; }
        public Dictionary<string, NamedBuffer<int>> IndexColumnMap { get; } = new Dictionary<string, NamedBuffer<int>>();
        public Dictionary<string, NamedBuffer<int>> StringColumnMap { get; } = new Dictionary<string, NamedBuffer<int>>();
        public Dictionary<string, INamedBuffer> DataColumnMap { get; } = new Dictionary<string, INamedBuffer>();
        public int RowCount { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public VimEntityTable(VimEntityTableData tableData, string[] stringTable)
        {
            TableData = tableData;
            StringTable = stringTable;

            RowCount = tableData.GetRowCount();

            foreach (var column in TableData.IndexColumns)
                IndexColumnMap[column.Name] = column;

            foreach (var column in TableData.StringColumns)
                StringColumnMap[column.Name] = column;

            foreach (var column in TableData.DataColumns)
                DataColumnMap[column.Name] = column;
        }

        private static T GetColumnOrDefault<T>(Dictionary<string, T> map, string key, T defaultValue = default)
            => map.TryGetValue(key, out var result) ? result : defaultValue;

        /// <summary>
        /// Returns the index column based on the given column name.
        /// </summary>
        public int[] GetIndexColumnValues(string columnName)
            => GetColumnOrDefault(IndexColumnMap, columnName)?.AsArray<int>();

        /// <summary>
        /// Returns the string column based on the given column name.
        /// </summary>
        public string[] GetStringColumnValues(string columnName)
        {
            var stringIndices = GetColumnOrDefault(StringColumnMap, columnName)
                 ?.AsArray<int>() ?? Array.Empty<int>();

            var strings = new string[stringIndices.Length];

            for (var i = 0; i < strings.Length; i++)
            {
                strings[i] = StringTable == null || StringTable.Length == 0
                    ? "" // Guard against the case where the string buffer is null or empty.
                    : StringTable.ElementAtOrDefault(stringIndices[i], "");
            }

            return strings;
        }

        /// <summary>
        /// Returns the data column based on the given column name.
        /// </summary>
        public T[] GetDataColumnValues<T>(string columnName) where T : unmanaged
        {
            var type = typeof(T);

            if (!VimEntityTableColumnTypeInfo.DataColumnTypes.Contains(type))
                throw new Exception($"{nameof(GetDataColumnValues)} error - unsupported data column type {type}");

            var namedBuffer = GetColumnOrDefault(DataColumnMap, columnName);
            if (namedBuffer == null)
                return null;

            return VimEntityTableColumnTypeInfo.GetDataColumnAsTypedArray<T>(namedBuffer);
        }
    }
}
