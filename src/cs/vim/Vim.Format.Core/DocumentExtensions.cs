using System;
using System.Text.RegularExpressions;
using Vim.BFastLib;
using System.Collections.Generic;
using System.Linq;
using Vim.Util;

namespace Vim.Format
{
    public static class DocumentExtensions
    {
        public static Document ToDocument(this SerializableDocument document)
            => new Document(document);

        public static EntityTable ToEntityTable(this SerializableEntityTable entityTable, Document document)
            => new EntityTable(document, entityTable);

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

        public static string GetRelatedTableName(this INamedBuffer<int> ic)
            => GetRelatedTableNameFromColumnName(ic.Name);

        public static EntityTable GetRelatedTable(this INamedBuffer<int> ic, Document doc)
            => doc.GetTable(ic.GetRelatedTableName());
    }
}
