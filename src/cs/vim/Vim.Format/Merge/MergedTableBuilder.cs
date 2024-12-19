using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Vim.BFastLib;
using Vim.Util;

namespace Vim.Format.Merge
{
    internal class MergedTableBuilder
    {
        public readonly string Name;
        private int _numRows;

        public MergedTableBuilder(string name)
            => Name = name;

        private readonly Dictionary<string, IBuffer> _dataColumns = new Dictionary<string, IBuffer>();
        private readonly DictionaryOfLists<string, int> _indexColumns = new DictionaryOfLists<string, int>();
        private readonly DictionaryOfLists<string, string> _stringColumns = new DictionaryOfLists<string, string>();

        public void AddTable(EntityTable entityTable, Dictionary<EntityTable, int> entityIndexOffsets)
        {
            Debug.Assert(entityIndexOffsets[entityTable] == _numRows);

            // Add index columns from the entity table
            foreach (var col in entityTable.IndexColumns)
            {
                var indexColumnFullName = col.Name;

                if (!_indexColumns.ContainsKey(indexColumnFullName))
                    _indexColumns.Add(indexColumnFullName, Enumerable.Repeat(-1, _numRows).ToList());

                Debug.Assert(col.Array.Length == entityTable.NumRows);
                var relatedTable = col.GetRelatedTable(entityTable.Document);
                var vals = _indexColumns[indexColumnFullName];

                var offset = entityIndexOffsets[relatedTable];
                foreach (var v in col.Array)
                    vals.Add(v < 0 ? v : v + offset);
            }

            // Add data columns from the entity table 
            foreach (var col in entityTable.DataColumns)
            {
                if (!_dataColumns.ContainsKey(col.Name))
                {
                    _dataColumns[col.Name] = col;
                }
                else
                {
                    var cur = _dataColumns[col.Name];
                    _dataColumns[col.Name] = cur.ConcatDataColumnBuffers(col,  SerializableEntityTable.GetTypeFromName(col.Name));
                }
            }

            // Add string columns from the entity table 
            foreach (var col in entityTable.StringColumns)
            {
                if (!_stringColumns.ContainsKey(col.Name))
                    _stringColumns.Add(col.Name, Enumerable.Repeat("", _numRows).ToList());

                Debug.Assert(col.Array.Length == entityTable.NumRows);
                var vals = _stringColumns[col.Name];
                foreach (var v in col.Array)
                    vals.Add(entityTable.Document.GetString(v));
            }

            // For each column in the builder but not in the entity table add default values
            foreach (var kv in _dataColumns)
            {
                var colName = kv.Key;
                var typePrefix = SerializableEntityTable.GetTypeFromName(colName);
                if (entityTable.DataColumns.All(c => c.Name != colName))
                {
                    var cur = _dataColumns[colName];
                    var defaultBuffer = ColumnExtensions.CreateDefaultDataColumnBuffer(entityTable.NumRows, typePrefix);
                    _dataColumns[colName] = cur.ConcatDataColumnBuffers(defaultBuffer, typePrefix);
                }
            }

            foreach (var kv in _indexColumns)
            {
                if (entityTable.IndexColumns.All(c => c.Name != kv.Key))
                    _indexColumns[kv.Key].AddRange(Enumerable.Repeat(-1, entityTable.NumRows));
            }

            foreach (var kv in _stringColumns)
            {
                if (entityTable.StringColumns.All(c => c.Name != kv.Key))
                    _stringColumns[kv.Key].AddRange(Enumerable.Repeat("", entityTable.NumRows));
            }

            _numRows += entityTable.NumRows;

            foreach (var kv in _dataColumns)
                Debug.Assert(kv.Value.Data.Length == _numRows);
            foreach (var kv in _indexColumns)
                Debug.Assert(kv.Value.Count == _numRows);
            foreach (var kv in _stringColumns)
                Debug.Assert(kv.Value.Count == _numRows);
        }

        public void UpdateTableBuilder(EntityTableBuilder tb, CancellationToken cancellationToken = default)
        {
            foreach (var kv in _dataColumns)
            {
                cancellationToken.ThrowIfCancellationRequested();
                tb.AddDataColumn(kv.Key, kv.Value);
            }

            foreach (var kv in _stringColumns)
            {
                cancellationToken.ThrowIfCancellationRequested();
                tb.AddStringColumn(kv.Key, kv.Value.ToArray());
            }

            foreach (var kv in _indexColumns)
            {
                cancellationToken.ThrowIfCancellationRequested();
                tb.AddIndexColumn(kv.Key, kv.Value.ToArray());
            }
        }
    }
}
