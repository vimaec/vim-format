using System.Collections.Generic;
using System.Linq;
using Vim.BFastLib;
using Vim.Format.Geometry;
using Vim.G3d;
using Vim.LinqArray;
using static Vim.Format.DocumentBuilder;

namespace Vim.Format
{
    public static class DocumentBuilderExtensions
    {
        public static Material ToDocumentBuilderMaterial(this G3dMaterial g3dMaterial)
            => new Material
            {
                Color = g3dMaterial.Color,
                Glossiness = g3dMaterial.Glossiness,
                Smoothness = g3dMaterial.Smoothness,
            };

        public static SubdividedMesh ToDocumentBuilderSubdividedMesh(this IMeshCommon m)
            => new SubdividedMesh(
                m.Indices.ToList(),
                m.Vertices.ToList(),
                m.SubmeshIndexOffsets.ToList(),
                m.SubmeshMaterials.ToList());

        public static EntityTableBuilder CreateTableCopy(this DocumentBuilder db, EntityTable table, List<int> nodeIndexRemapping = null)
        {
            var name = table.Name;
            var tb = db.CreateTableBuilder(name);

            foreach (var col in table.IndexColumns.Values.ToEnumerable())
            {
                tb.AddIndexColumn(col.Name, col.GetTypedData().RemapData(nodeIndexRemapping));
            }

            foreach (var col in table.DataColumns.Values.ToEnumerable())
            {
                tb.AddDataColumn(col.Name, col.CopyDataColumn(nodeIndexRemapping));
            }

            foreach (var col in table.StringColumns.Values.ToEnumerable())
            {
                var strings = col.GetTypedData().Select(i => table.Document.StringTable.ElementAtOrDefault(i, null));
                tb.AddStringColumn(col.Name, strings.ToArray().RemapData(nodeIndexRemapping));
            }

            return tb;
        }

        public static DocumentBuilder CopyTablesFrom(this DocumentBuilder db, Document doc, List<int> nodeIndexRemapping = null)
        {
            foreach (var table in doc.EntityTables.Values.ToEnumerable())
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
