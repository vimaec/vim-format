using System;
using Vim.BFastLib;
using System.Linq;
using System.Collections.Generic;
using Vim.Util;

namespace Vim.Format
{
    public class EntityTable
    {
        public EntityTable(Document document, SerializableEntityTable entityTable)
        {
            Document = document;
            _EntityTable = entityTable;
            Name = _EntityTable.Name;

            _dataColumns = _EntityTable.DataColumns.ToDictionary( c => c.Name, c => c);
            IndexColumns = _EntityTable.IndexColumns.ToDictionary(c => c.Name, c => c);
            _stringColumns = _EntityTable.StringColumns.ToDictionary(c => c.Name, c => c);
            NumRows = Columns.FirstOrDefault()?.NumElements() ?? 0;

            Columns.ValidateColumnRowsAreAligned();
        }

        private SerializableEntityTable _EntityTable { get; }
        public Document Document { get; }
        public string Name { get; }
        public int NumRows { get; }
        private Dictionary<string, INamedBuffer> _dataColumns { get; }
        private Dictionary<string, NamedBuffer<int>> _stringColumns { get; }
        public Dictionary<string, NamedBuffer<int>> IndexColumns { get; }


        public bool HasDataColumns(string name) => _dataColumns.ContainsKey(name);
        public IEnumerable<INamedBuffer> DataColumns => _dataColumns.Values;
        public IEnumerable<string> DataColumnNames => _dataColumns.Keys;

        public IEnumerable<NamedBuffer<int>> StringColumns => _stringColumns.Values;
        public bool HasStringColumn(string name) => _stringColumns.ContainsKey(name);
        public NamedBuffer<int> GetStringColumn(string name) => _stringColumns.GetOrDefault(name);
        public IEnumerable<string> StringColumnNames => _stringColumns.Keys;

        public IEnumerable<INamedBuffer> Columns
            => DataColumns
                .Concat(IndexColumns.Values.Select(x => (INamedBuffer)x))
                .Concat(_stringColumns.Values.Select(x => (INamedBuffer)x));

        public int[] GetIndexColumnValues(string columnName)
            => IndexColumns.GetOrDefault(columnName)?.GetColumnValues<int>();

        public string[] GetStringColumnValues(string columnName)
            => _stringColumns.GetOrDefault(columnName)
                ?.GetColumnValues<int>()
                ?.Select(Document.GetString).ToArray();

        public T[] GetDataColumnValues<T>(string columnName) where T : unmanaged
        {
            var type = typeof(T);

            if (!ColumnExtensions.DataColumnTypes.Contains(type))
                throw new Exception($"{nameof(GetDataColumnValues)} error - unsupported data column type {type}");

            var namedBuffer = _dataColumns.GetOrDefault(columnName);
            if (namedBuffer == null)
                return null;

            if (type == typeof(short))
                return namedBuffer.AsArray<int>().Select(i => (short)i) as T[];

            if (type == typeof(bool))
                return namedBuffer.AsArray<byte>().Select(b => b != 0) as T[];

            return namedBuffer.AsArray<T>();
        }
    }
}
