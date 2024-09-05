using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Vim.BFast;
using Vim.LinqArray;
using Vim.Util;

namespace Vim.Format.Merge
{
    public class MergedTableBuilder
    {
        public readonly string Name;
        public int NumRows;

        public MergedTableBuilder(string name)
            => Name = name;

        public Dictionary<string, IBuffer> DataColumns = new Dictionary<string, IBuffer>();
        public DictionaryOfLists<string, int> IndexColumns = new DictionaryOfLists<string, int>();
        public DictionaryOfLists<string, string> StringColumns = new DictionaryOfLists<string, string>();

        public void AddTable(EntityTable entityTable, Dictionary<EntityTable, int> entityIndexOffsets)
        {
            Debug.Assert(entityIndexOffsets[entityTable] == NumRows);

            // Add index columns from the entity table
            foreach (var k in entityTable.IndexColumns.Keys.ToEnumerable())
            {
                var col = entityTable.IndexColumns[k];
                var indexColumnFullName = col.Name;

                if (!IndexColumns.ContainsKey(indexColumnFullName))
                    IndexColumns.Add(indexColumnFullName, Enumerable.Repeat(-1, NumRows).ToList());

                Debug.Assert(col.Array.Length == entityTable.NumRows);
                var relatedTable = col.GetRelatedTable(entityTable.Document);
                var vals = IndexColumns[indexColumnFullName];

                var offset = entityIndexOffsets[relatedTable];
                foreach (var v in col.Array)
                    vals.Add(v < 0 ? v : v + offset);
            }

            // Add data columns from the entity table 
            foreach (var colName in entityTable.DataColumns.Keys.ToEnumerable())
            {
                var col = entityTable.DataColumns[colName];
                if (!DataColumns.ContainsKey(colName))
                {
                    DataColumns[colName] = col;
                }
                else
                {
                    var cur = DataColumns[colName];
                    DataColumns[colName] = cur.ConcatDataColumnBuffers(col, colName.GetTypePrefix());
                }
            }

            // Add string columns from the entity table 
            foreach (var k in entityTable.StringColumns.Keys.ToEnumerable())
            {
                if (!StringColumns.ContainsKey(k))
                    StringColumns.Add(k, Enumerable.Repeat("", NumRows).ToList());

                var col = entityTable.StringColumns[k];
                Debug.Assert(col.Array.Length == entityTable.NumRows);
                var vals = StringColumns[k];
                foreach (var v in col.Array)
                    vals.Add(entityTable.Document.GetString(v));
            }

            // For each column in the builder but not in the entity table add default values
            foreach (var kv in DataColumns)
            {
                var colName = kv.Key;
                var typePrefix = colName.GetTypePrefix();
                if (!entityTable.DataColumns.Contains(colName))
                {
                    var cur = DataColumns[colName];
                    var defaultBuffer = ColumnExtensions.CreateDefaultDataColumnBuffer(entityTable.NumRows, typePrefix);
                    DataColumns[colName] = cur.ConcatDataColumnBuffers(defaultBuffer, typePrefix);
                }
            }

            foreach (var kv in IndexColumns)
            {
                if (!entityTable.IndexColumns.Contains(kv.Key))
                    IndexColumns[kv.Key].AddRange(Enumerable.Repeat(-1, entityTable.NumRows));
            }

            foreach (var kv in StringColumns)
            {
                if (!entityTable.StringColumns.Contains(kv.Key))
                    StringColumns[kv.Key].AddRange(Enumerable.Repeat("", entityTable.NumRows));
            }

            NumRows += entityTable.NumRows;

            foreach (var kv in DataColumns)
                Debug.Assert(kv.Value.Data.Length == NumRows);
            foreach (var kv in IndexColumns)
                Debug.Assert(kv.Value.Count == NumRows);
            foreach (var kv in StringColumns)
                Debug.Assert(kv.Value.Count == NumRows);
        }

        public void UpdateTableBuilder(EntityTableBuilder tb, CancellationToken cancellationToken = default)
        {
            foreach (var kv in DataColumns)
            {
                cancellationToken.ThrowIfCancellationRequested();
                tb.AddDataColumn(kv.Key, kv.Value);
            }

            foreach (var kv in StringColumns)
            {
                cancellationToken.ThrowIfCancellationRequested();
                tb.AddStringColumn(kv.Key, kv.Value.ToArray());
            }

            foreach (var kv in IndexColumns)
            {
                cancellationToken.ThrowIfCancellationRequested();
                tb.AddIndexColumn(kv.Key, kv.Value.ToArray());
            }
        }
    }
}
