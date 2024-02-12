using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Vim.BFastLib;

namespace Vim.Format
{
    /// <summary>
    /// Tracks all of the data for a particular entity type in a conceptual table.
    /// A column maybe a relation to another entity table (IndexColumn)
    /// a data value stored as a double (DataColumn) or else
    /// it is string data, stored as an index into the global lookup table (StringColumn).
    /// </summary>
    public class SerializableEntityTable
    {
        /// <summary>
        /// Name of 
        /// </summary>
        public string Name;

        /// <summary>
        /// Relation to another entity table. For example surface to element. 
        /// </summary>
        public List<NamedBuffer<int>> IndexColumns = new List<NamedBuffer<int>>();

        /// <summary>
        /// Data encoded as strings in the global string table
        /// </summary>
        public List<NamedBuffer<int>> StringColumns = new List<NamedBuffer<int>>();

        /// <summary>
        /// Numeric data encoded as byte, int, float, or doubles 
        /// </summary>
        public List<INamedBuffer> DataColumns = new List<INamedBuffer>();

        public IEnumerable<string> ColumnNames
            => IndexColumns.Select(c => c.Name)
                .Concat(StringColumns.Select(c => c.Name))
                .Concat(DataColumns.Select(c => c.Name));

        public IEnumerable<INamedBuffer> AllColumns
            => IndexColumns
            .Concat(StringColumns)
            .Concat(DataColumns);

        public static SerializableEntityTable FromBfast(string name, BFast bfast)
        {
            return null;
        }

        private readonly static Regex TypePrefixRegex = new Regex(@"(\w+:).*");

        public static string GetTypeFromName(string name)
        {
            var match = TypePrefixRegex.Match(name);
            return match.Success ? match.Groups[1].Value : "";
        }

        public BFast ToBFast()
        {
            var bfast = new BFast();
            foreach (var col in AllColumns)
            {
                bfast.SetArray(col.Name, col.AsArray<byte>());
            }
            return bfast;
        }
    }
}
