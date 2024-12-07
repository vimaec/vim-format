using System;
using System.Collections.Generic;
using System.Linq;
using Vim.BFastLib;
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

            DataColumns = _EntityTable.DataColumns.ToDictionary(c => c.Name, c => c);
            IndexColumns = _EntityTable.IndexColumns.ToDictionary(c => c.Name, c => c);
            StringColumns = _EntityTable.StringColumns.ToDictionary(c => c.Name, c => c);
            NumRows = Columns.FirstOrDefault()?.NumElements() ?? 0;

            Columns.ValidateColumnRowsAreAligned();
        }

        private SerializableEntityTable _EntityTable { get; }
        public Document Document { get; }
        public string Name { get; }
        public int NumRows { get; }
        public IDictionary<string, INamedBuffer> DataColumns { get; }
        public IDictionary<string, NamedBuffer<int>> StringColumns { get; }
        public IDictionary<string, NamedBuffer<int>> IndexColumns { get; }
        public IList<INamedBuffer> Columns
            => DataColumns.Values
                .Concat(IndexColumns.Values.Select(x => (INamedBuffer)x))
                .Concat(StringColumns.Values.Select(x => (INamedBuffer)x))
                .ToArray();

        public IList<int> GetIndexColumnValues(string columnName)
            => IndexColumns.GetOrDefault(columnName)?.GetColumnValues<int>();

        public IList<string> GetStringColumnValues(string columnName)
            => StringColumns.GetOrDefault(columnName)
                ?.GetColumnValues<int>()
                ?.Select(Document.GetString)
                 .ToArray();

        public IList<T> GetDataColumnValues<T>(string columnName) where T : unmanaged
        {
            var type = typeof(T);

            if (!ColumnExtensions.DataColumnTypes.Contains(type))
                throw new Exception($"{nameof(GetDataColumnValues)} error - unsupported data column type {type}");

            var namedBuffer = DataColumns.GetOrDefault(columnName);
            if (namedBuffer == null)
                return null;

            if (type == typeof(short))
                return namedBuffer.GetColumnValues<int>().Select(i => (short)i) as IList<T>;

            if (type == typeof(bool))
                return namedBuffer.GetColumnValues<byte>().Select(b => b != 0) as IList<T>;

            return namedBuffer.GetColumnValues<T>();
        }
    }
}
