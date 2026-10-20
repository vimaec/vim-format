using System;
using System.Collections.Generic;
using System.Linq;
using Vim.BFast;
using Vim.G3d;

namespace Vim.Format.Merge
{
    /// <summary>
    /// A VIM file held as its raw entity tables, string table, assets and G3D geometry. No DocumentModel is built, so
    /// loading a large file costs only the deserialization (TowerS, 224 MB: about 0.2 s). Used by
    /// <see cref="VimPatchService"/> and by the callers that decide what to patch.
    /// </summary>
    public sealed class VimTables
    {
        public const string ElementColumn = "index:" + TableNames.Element + ":Element";
        public const string BimDocumentColumn = "index:" + TableNames.BimDocument + ":BimDocument";

        public SerializableDocument Document { get; }
        public string[] Strings { get; }
        public Dictionary<string, SerializableEntityTable> Tables { get; }
        public G3D Geometry { get; }

        private readonly Dictionary<string, int> _numRows = new Dictionary<string, int>();

        public VimTables(SerializableDocument document)
        {
            Document = document ?? throw new ArgumentNullException(nameof(document));
            Strings = document.StringTable ?? Array.Empty<string>();
            Tables = document.EntityTables.ToDictionary(t => t.Name, t => t);
            Geometry = document.Geometry;
        }

        public static VimTables Load(string vimFilePath)
            => new VimTables(Serializer.Deserialize(vimFilePath));

        public SerializableHeader Header => Document.Header;

        public SerializableEntityTable GetTable(string table)
            => Tables.TryGetValue(table, out var t) ? t : null;

        public int NumRows(string table)
        {
            if (_numRows.TryGetValue(table, out var n))
                return n;
            var first = GetTable(table)?.IndexColumns.Cast<INamedBuffer>()
                .Concat(GetTable(table).StringColumns)
                .Concat(GetTable(table).DataColumns)
                .FirstOrDefault();
            n = first?.NumElements() ?? 0;
            _numRows[table] = n;
            return n;
        }

        /// <summary>The values of an index column, or null when the table or the column is absent.</summary>
        public int[] Index(string table, string column)
            => GetTable(table)?.IndexColumns.FirstOrDefault(c => c.Name == column)?.Array;

        /// <summary>The string-table indices of a string column, or null.</summary>
        public int[] StringIndices(string table, string column)
            => GetTable(table)?.StringColumns.FirstOrDefault(c => c.Name == column)?.Array;

        /// <summary>The resolved values of a string column, or null.</summary>
        public string[] Strs(string table, string column)
            => StringIndices(table, column)?.Select(Str).ToArray();

        public string Str(int stringIndex)
            => stringIndex >= 0 && stringIndex < Strings.Length ? Strings[stringIndex] ?? "" : "";

        public INamedBuffer Data(string table, string column)
            => GetTable(table)?.DataColumns.FirstOrDefault(c => c.Name == column);

        public T[] Data<T>(string table, string column) where T : unmanaged
            => Data(table, column)?.Data as T[];

        /// <summary>Element-owned tables have an "index:Vim.Element:Element" column (Element itself excluded).</summary>
        public bool IsElementOwned(string table)
            => table != TableNames.Element && GetTable(table)?.IndexColumns.Any(c => c.Name == ElementColumn) == true;

        /// <summary>The table each index column of the table points at.</summary>
        public IEnumerable<(string Column, string Related)> IndexRelations(string table)
            => (GetTable(table)?.IndexColumns ?? new List<NamedBuffer<int>>())
                .Select(c => (c.Name, ColumnExtensions.GetRelatedTableNameFromColumnName(c.Name)));

        /// <summary>
        /// For each BimDocument row: true when it is the host model rather than a linked one (IsLinked = 0; without that
        /// column, a row with no parent document).
        /// </summary>
        public bool[] HostBimDocuments()
        {
            var n = NumRows(TableNames.BimDocument);
            var linked = Data<byte>(TableNames.BimDocument, "byte:IsLinked");
            var parents = Index(TableNames.BimDocument, "index:" + TableNames.BimDocument + ":Parent");
            var r = new bool[n];
            for (var i = 0; i < n; ++i)
                r[i] = linked != null && i < linked.Length
                    ? linked[i] == 0
                    : parents == null || i >= parents.Length || parents[i] < 0;
            return r;
        }

        /// <summary>For each Element row: true when its BimDocument is the host model.</summary>
        public bool[] HostElements()
        {
            var host = HostBimDocuments();
            var docs = Index(TableNames.Element, BimDocumentColumn);
            var r = new bool[NumRows(TableNames.Element)];
            for (var i = 0; i < r.Length; ++i)
            {
                var d = docs == null ? -1 : docs[i];
                r[i] = d >= 0 && d < host.Length && host[d];
            }
            return r;
        }

        /// <summary>The BimDocument title of each Element row ("" when none).</summary>
        public string[] ElementDocumentTitles()
        {
            var titles = Strs(TableNames.BimDocument, "string:Title") ?? Array.Empty<string>();
            var docs = Index(TableNames.Element, BimDocumentColumn);
            var r = new string[NumRows(TableNames.Element)];
            for (var i = 0; i < r.Length; ++i)
                r[i] = docs != null && docs[i] >= 0 && docs[i] < titles.Length ? titles[docs[i]] ?? "" : "";
            return r;
        }

        /// <summary>
        /// The host model's elements held more than once (same UniqueId). A full export holds none; a file in which some
        /// are must not be patched.
        /// </summary>
        public int HostElementsHeldTwice()
        {
            var host = HostElements();
            var uids = Strs(TableNames.Element, "string:UniqueId") ?? Array.Empty<string>();
            var seen = new HashSet<string>(StringComparer.Ordinal);
            var twice = 0;
            for (var i = 0; i < uids.Length; ++i)
                if (host[i] && !string.IsNullOrEmpty(uids[i]) && !seen.Add(uids[i]))
                    twice++;
            return twice;
        }
    }
}
