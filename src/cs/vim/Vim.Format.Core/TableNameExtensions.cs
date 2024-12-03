using System;
using System.Text.RegularExpressions;
using Vim.BFastLib;

namespace Vim.Format
{
    public static class TableNameExtensions
    {
        private static readonly Regex IndexColumnNameComponentsRegex = new Regex(@"(\w+:)((?:\w|\.)+):(.+)");

        public static string GetRelatedTableNameFromIndexColumnName(string indexColumnName)
        {
            var match = IndexColumnNameComponentsRegex.Match(indexColumnName);
            if (!match.Success)
                throw new Exception($"Index column name {indexColumnName} could not be separated into its components.");

            return match.Groups[2].Value;
        }

        public static string GetRelatedTableName(this INamedBuffer<int> ic)
            => GetRelatedTableNameFromIndexColumnName(ic.Name);

        public static EntityTable GetRelatedTable(this INamedBuffer<int> ic, Document doc)
            => doc.GetTable(ic.GetRelatedTableName());
    }
}
