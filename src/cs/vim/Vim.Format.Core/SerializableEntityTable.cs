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
        public Dictionary<string, NamedBuffer<int>> IndexColumnsMap = new Dictionary<string, NamedBuffer<int>>();

        /// <summary>
        /// Data encoded as strings in the global string table
        /// </summary>
        public List<NamedBuffer<int>> StringColumns = new List<NamedBuffer<int>>();
        public Dictionary<string, NamedBuffer<int>> StringColumnsMap = new Dictionary<string, NamedBuffer<int>>();

        /// <summary>
        /// Numeric data encoded as byte, int, float, or doubles 
        /// </summary>
        public List<INamedBuffer> DataColumns = new List<INamedBuffer>();
        public Dictionary<string, INamedBuffer> DataColumnsMap = new Dictionary<string, INamedBuffer>();


        public void BreakIndices(params string[] columns)
        {
            var set = new HashSet<string>(columns);
            IndexColumns = IndexColumns.Select(c => set.Contains(c.Name) ? c.Fill(VimConstants.NoEntityRelation) : c).ToList();
            IndexColumnsMap = IndexColumns.ToDictionary(c => c.Name, c => c);
        }

        public void AddDataColumn(INamedBuffer buffer)
        {
            DataColumns.Add(buffer);
            DataColumnsMap.Add(buffer.Name, buffer);
        }

        public void AddIndexColumn(NamedBuffer<int> buffer)
        {
            IndexColumns.Add(buffer);
            IndexColumnsMap.Add(buffer.Name, buffer);
        }

        public void AddStringColumn(NamedBuffer<int> buffer)
        {
            StringColumns.Add(buffer);
            StringColumnsMap.Add(buffer.Name, buffer);
        }

        public IEnumerable<string> ColumnNames
            => IndexColumns.Select(c => c.Name)
                .Concat(StringColumns.Select(c => c.Name))
                .Concat(DataColumns.Select(c => c.Name));

        public IEnumerable<INamedBuffer> AllColumns
            => IndexColumns
            .Concat(StringColumns)
            .Concat(DataColumns);

        private readonly static Regex TypePrefixRegex = new Regex(@"(\w+:).*");

        public static string GetTypeFromName(string name)
        {
            var match = TypePrefixRegex.Match(name);
            return match.Success ? match.Groups[1].Value : "";
        }

        /// <summary>
        /// Returns a SerializableEntityTable based on the given buffer reader.
        /// </summary>
        public static SerializableEntityTable FromBFast(
            BFast bfast,
            bool schemaOnly
           )
        {
            var et = new SerializableEntityTable();
            foreach (var entry in bfast.Entries)
            {
                var typePrefix = SerializableEntityTable.GetTypeFromName(entry);

                switch (typePrefix)
                {
                    case VimConstants.IndexColumnNameTypePrefix:
                        {
                            //TODO: replace named buffer with arrays
                            var col = schemaOnly ? new int[0] : bfast.GetArray<int>(entry);
                            et.AddIndexColumn(col.ToNamedBuffer(entry));
                            break;
                        }
                    case VimConstants.StringColumnNameTypePrefix:
                        {
                            var col = schemaOnly ? new int[0] : bfast.GetArray<int>(entry);
                            et.AddStringColumn(col.ToNamedBuffer(entry));
                            break;
                        }
                    case VimConstants.IntColumnNameTypePrefix:
                        {
                            var col = schemaOnly ? new int[0] : bfast.GetArray<int>(entry);
                            et.AddDataColumn(col.ToNamedBuffer(entry));
                            break;
                        }
                    case VimConstants.LongColumnNameTypePrefix:
                        {
                            var col = schemaOnly ? new long[0] : bfast.GetArray<long>(entry);
                            et.AddDataColumn(col.ToNamedBuffer(entry));
                            break;
                        }
                    case VimConstants.DoubleColumnNameTypePrefix:
                        {
                            var col = schemaOnly ? new double[0] : bfast.GetArray<double>(entry);
                            et.AddDataColumn(col.ToNamedBuffer(entry));
                            break;
                        }
                    case VimConstants.FloatColumnNameTypePrefix:
                        {
                            var col = schemaOnly ? new float[0] : bfast.GetArray<float>(entry);
                            et.AddDataColumn(col.ToNamedBuffer(entry));
                            break;
                        }
                    case VimConstants.ByteColumnNameTypePrefix:
                        {
                            var col = schemaOnly ? new byte[0] : bfast.GetArray<byte>(entry);
                            et.AddDataColumn(col.ToNamedBuffer(entry));
                            break;
                        }
                        // For flexibility, we ignore the columns which do not contain a recognized prefix.
                }
            }

            return et;
        }

        public BFast ToBFast()
        {
            var bfast = new BFast();
            foreach (var col in AllColumns)
            {
                bfast.SetArray(col.Name, col.ToBytes());
            }
            return bfast;
        }
    }
}
