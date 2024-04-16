using System.Collections.Generic;
using System.Linq;
using Vim.BFastLib;
using Vim.Format.Geometry;
using Vim.Util;
using static Vim.Format.DocumentBuilder;

namespace Vim.Format
{
    public static class DocumentBuilderExtensions
    {
        public static SubdividedMesh ToDocumentBuilderSubdividedMesh(this VimMesh m)
            => new SubdividedMesh(
                m.indices,
                m.vertices,
                m.submeshIndexOffsets,
                m.submeshMaterials);

        public static EntityTableBuilder CreateTableCopy(this DocumentBuilder db, EntityTable table, List<int> nodeIndexRemapping = null)
        {
            var name = table.Name;
            var tb = db.CreateTableBuilder(name);

            foreach (var col in table.IndexColumns.Values)
            {
                tb.AddIndexColumn(col.Name, col.GetTypedData().RemapData(nodeIndexRemapping));
            }

            foreach (var col in table.DataColumns.Values)
            {
                tb.AddDataColumn(col.Name, col.CopyDataColumn(nodeIndexRemapping));
            }

            foreach (var col in table.StringColumns.Values)
            {
                var strings = col.GetTypedData().Select(i => table.Document.StringTable.ElementAtOrDefault(i, null));
                tb.AddStringColumn(col.Name, strings.ToArray().RemapData(nodeIndexRemapping));
            }

            return tb;
        }

        public static DocumentBuilder CopyTablesFrom(this DocumentBuilder db, Document doc, List<int> nodeIndexRemapping = null)
        {
            foreach (var table in doc.EntityTables.Values)
            {
                var name = table.Name;

                // Don't copy tables that are computed automatically
                if (VimConstants.ComputedTableNames.Contains(name))
                    continue;

                db.CreateTableCopy(table, name == TableNames.Node ? nodeIndexRemapping : null);
            }

            return db;
        }

        public static SerializableEntityTable ToSerializableEntityTable(this EntityTableBuilder tb,
            IReadOnlyDictionary<string, int> stringLookup)
        {
            var table = new SerializableEntityTable
            {
                // Set the table name
                Name = tb.Name,

                // Convert the columns to named buffers 
                IndexColumns = tb.IndexColumns
                    .Select(kv => kv.Value.ToNamedBuffer(kv.Key))
                    .ToList(),
                DataColumns = tb.DataColumns
                    .Select(kv => kv.Value.ToNamedBuffer(kv.Key) as INamedBuffer)
                    .ToList(),
                StringColumns = tb.StringColumns
                    .Select(kv => kv.Value
                        .Select(s => stringLookup[s ?? string.Empty])
                        .ToArray()
                        .ToNamedBuffer(kv.Key))
                    .ToList(),
            };

            table.ValidateColumnRowsAreAligned();

            return table;
        }
    }
}
