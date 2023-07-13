using System;
using System.Text.RegularExpressions;
using Vim.LinqArray;
using Vim.BFast;

namespace Vim.Format
{
    public static class DocumentExtensions
    {
        public static Document ToDocument(this SerializableDocument document)
            => new Document(document);

        public static EntityTable ToEntityTable(this SerializableEntityTable entityTable, Document document)
            => new EntityTable(document, entityTable);

        public static IArray<string> GetColumnNames(this EntityTable table)
            => table.Columns.Select(b => b.Name);

        public static void ValidateRelations(this Document doc)
        {
            foreach (var et in doc.EntityTables.Values.ToEnumerable())
            {
                foreach (var ic in et.IndexColumns.Values.ToEnumerable())
                {
                    var relatedTable = ic.GetRelatedTable(doc);
                    var maxValue = relatedTable.NumRows;
                    var data = ic.GetTypedData();
                    for (var i = 0; i < data.Length; ++i)
                    {
                        var v = data[i];
                        if (v < -1 || v > maxValue)
                        {
                            throw new Exception($"Invalid relation {v} out of range of -1 to {maxValue}");
                        }
                    }
                }
            }
        }

        public static readonly Regex IndexColumnNameComponentsRegex = new Regex(@"(\w+:)((?:\w|\.)+):(.+)");

        public class IndexColumnNameComponents
        {
            public readonly string TypePrefix;
            public readonly string TableName;
            public readonly string FieldName;

            public IndexColumnNameComponents(string indexColumnName)
            {
                var match = IndexColumnNameComponentsRegex.Match(indexColumnName);
                if (!match.Success)
                    throw new Exception($"Index column name {indexColumnName} could not be separated into its components.");

                TypePrefix = match.Groups[1].Value;
                TableName = match.Groups[2].Value;
                FieldName = match.Groups[3].Value;
            }
        }

        public static IndexColumnNameComponents SplitIndexColumnName(string name)
            => new IndexColumnNameComponents(name);

        public static string GetRelatedTableNameFromColumnName(string name)
            => SplitIndexColumnName(name).TableName;

        public static string GetFieldNameFromColumnName(string name)
            => SplitIndexColumnName(name).FieldName;

        public static string GetFieldName(this INamedBuffer<int> ic)
            => GetFieldNameFromColumnName(ic.Name);

        public static string GetRelatedTableName(this INamedBuffer<int> ic)
            => GetRelatedTableNameFromColumnName(ic.Name);

        public static EntityTable GetRelatedTable(this INamedBuffer<int> ic, Document doc)
            => doc.GetTable(ic.GetRelatedTableName());

        public static EntityTable GetTable(this Document doc, string name)
            => doc.EntityTables.GetOrDefault(name);

        public static SerializableDocument SetFileName(this SerializableDocument doc, string fileName)
        {
            doc.FileName = fileName;
            return doc;
        }
    }
}
