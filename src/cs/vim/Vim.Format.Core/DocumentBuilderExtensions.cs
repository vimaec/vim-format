using System.Collections.Generic;
using System.Linq;
using Vim.BFastLib;

namespace Vim.Format
{
    public static class DocumentBuilderExtensions
    {
        public static EntityTableBuilder CreateTableCopy(this DocumentBuilder db, EntityTable table, List<int> nodeIndexRemapping = null)
        {
            var name = table.Name;
            var tb = db.CreateTableBuilder(name);

            foreach (var col in table.IndexColumns.Values)
            {
                tb.AddIndexColumn(col.Name, col.GetTypedData().RemapData(nodeIndexRemapping));
            }

            foreach (var col in table.DataColumns)
            {
                tb.AddDataColumn(col.Name, col.CopyDataColumn(nodeIndexRemapping));
            }

            foreach (var col in table.StringColumns)
            {
                var strings = col.GetTypedData().Select(i => table.Document.GetString(i));
                tb.AddStringColumn(col.Name, strings.ToArray().RemapData(nodeIndexRemapping));
            }

            return tb;
        }

        public static DocumentBuilder CopyTablesFrom(this DocumentBuilder db, Document doc, List<int> nodeIndexRemapping = null)
        {
            foreach (var table in doc.Tables)
            {
                var name = table.Name;

                // Don't copy tables that are computed automatically
                if (VimConstants.ComputedTableNames.Contains(name))
                    continue;

                db.CreateTableCopy(table, name == TableNames.Node ? nodeIndexRemapping : null);
            }

            return db;
        }

    
    }
}
