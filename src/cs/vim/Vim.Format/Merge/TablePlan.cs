using System;
using System.Collections.Generic;

namespace Vim.Format.Merge
{
    /// <summary>
    /// The rows one output table takes from one or more source files, in output order, and where every source row
    /// went (-1 = not written). Relations are re-pointed through <see cref="Map"/> when the table is written
    /// (<see cref="RowPlanWriter"/>).
    /// </summary>
    public sealed class TablePlan
    {
        public string Name { get; }

        /// <summary>The source file of each output row.</summary>
        public List<int> Source { get; } = new List<int>();

        /// <summary>The row of that source file each output row copies.</summary>
        public List<int> Row { get; } = new List<int>();

        /// <summary>Per source file: source row -> output row (-1 = not written).</summary>
        public int[][] Map { get; }

        public TablePlan(string name, IReadOnlyList<VimTables> sources)
        {
            Name = name;
            Map = new int[sources.Count][];
            for (var s = 0; s < sources.Count; ++s)
            {
                var m = new int[sources[s].NumRows(name)];
                for (var i = 0; i < m.Length; ++i)
                    m[i] = -1;
                Map[s] = m;
            }
        }

        public int Count => Source.Count;

        /// <summary>Appends a row whose content comes from (source, row) and maps that source row to it.</summary>
        public int Add(int source, int row)
        {
            var i = Source.Count;
            Source.Add(source);
            Row.Add(row);
            Map[source][row] = i;
            return i;
        }

        /// <summary>Maps another source row to an existing output row (its own content is not used).</summary>
        public void Alias(int source, int row, int outRow)
            => Map[source][row] = outRow;

        /// <summary>The output row of a source row, or -1.</summary>
        public int Lookup(int source, int row)
        {
            if (row < 0) return -1;
            var m = Map[source];
            return row < m.Length ? m[row] : -1;
        }

        /// <summary>Removes the output rows for which remove is true; the rows after them move up and Map follows.</summary>
        public int RemoveWhere(Func<int, bool> remove)
        {
            var newIndex = new int[Source.Count];
            var src = new List<int>();
            var row = new List<int>();
            for (var i = 0; i < Source.Count; ++i)
            {
                if (remove(i)) { newIndex[i] = -1; continue; }
                newIndex[i] = src.Count;
                src.Add(Source[i]);
                row.Add(Row[i]);
            }
            var removed = Source.Count - src.Count;
            if (removed == 0) return 0;
            Source.Clear(); Source.AddRange(src);
            Row.Clear(); Row.AddRange(row);
            foreach (var m in Map)
                for (var i = 0; i < m.Length; ++i)
                    if (m[i] >= 0) m[i] = newIndex[m[i]];
            return removed;
        }
    }
}
