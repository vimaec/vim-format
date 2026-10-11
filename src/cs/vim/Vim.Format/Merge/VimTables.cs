using System;
using System.Collections.Generic;
using System.Linq;
using Vim.BFast;
using Vim.Format.ObjectModel;
using Vim.G3d;
using Vim.LinqArray;

namespace Vim.Format.Merge
{
    /// <summary>
    /// A VIM file held as its raw entity tables, string table, assets and G3D geometry - no DocumentModel is built,
    /// so loading a large file costs only the deserialization. Used by <see cref="VimPatchService"/> and
    /// <see cref="VimExtractService"/>.
    /// </summary>
    public sealed class VimTables
    {
        public const string ElementColumn = "index:" + TableNames.Element + ":Element";

        public SerializableDocument Document { get; }
        public string[] Strings { get; private set; }
        public Dictionary<string, SerializableEntityTable> Tables { get; }
        public G3D Geometry { get; }

        private readonly Dictionary<string, int> _numRows = new Dictionary<string, int>();

        public VimTables(SerializableDocument document)
        {
            Document = document ?? throw new ArgumentNullException(nameof(document));
            Strings = document.StringTable ?? Array.Empty<string>();
            Tables = new Dictionary<string, SerializableEntityTable>();
            foreach (var t in document.EntityTables)
                Tables[t.Name] = t;
            Geometry = document.Geometry;
        }

        public static VimTables Load(string vimFilePath)
            => new VimTables(Serializer.Deserialize(vimFilePath));

        public SerializableHeader Header => Document.Header;

        public bool HasTable(string table) => Tables.ContainsKey(table);

        public SerializableEntityTable GetTable(string table)
            => Tables.TryGetValue(table, out var t) ? t : null;

        public int NumRows(string table)
        {
            if (_numRows.TryGetValue(table, out var n))
                return n;
            var t = GetTable(table);
            n = 0;
            if (t != null)
            {
                var first = t.IndexColumns.Cast<INamedBuffer>()
                    .Concat(t.StringColumns)
                    .Concat(t.DataColumns)
                    .FirstOrDefault();
                n = first?.NumElements() ?? 0;
            }
            _numRows[table] = n;
            return n;
        }

        /// <summary>The values of an index column, or null when the table or the column is absent.</summary>
        public int[] Index(string table, string column)
            => GetTable(table)?.IndexColumns.FirstOrDefault(c => c.Name == column)?.Array;

        /// <summary>The string indices of a string column, or null.</summary>
        public int[] StringIndices(string table, string column)
            => GetTable(table)?.StringColumns.FirstOrDefault(c => c.Name == column)?.Array;

        /// <summary>The resolved values of a string column, or null.</summary>
        public string[] Strs(string table, string column)
        {
            var idx = StringIndices(table, column);
            if (idx == null)
                return null;
            var r = new string[idx.Length];
            for (var i = 0; i < idx.Length; ++i)
                r[i] = Str(idx[i]);
            return r;
        }

        public string Str(int stringIndex)
            => stringIndex >= 0 && stringIndex < Strings.Length ? Strings[stringIndex] ?? "" : "";

        public INamedBuffer Data(string table, string column)
            => GetTable(table)?.DataColumns.FirstOrDefault(c => c.Name == column);

        public T[] Data<T>(string table, string column) where T : unmanaged
            => Data(table, column)?.Data as T[];

        /// <summary>Element-owned tables are the ones with an "index:Vim.Element:Element" column (Element itself excluded).</summary>
        public bool IsElementOwned(string table)
            => table != TableNames.Element && GetTable(table)?.IndexColumns.Any(c => c.Name == ElementColumn) == true;

        /// <summary>
        /// One key per BimDocument row: Title | PathName | n, n being the ordinal of that row among the rows with the
        /// same Title and PathName. The Guid is not used: Revit exports different Guids for the same document (the
        /// 2D export even writes a second BimDocument row of the same file with another Guid).
        /// </summary>
        public string[] BimDocumentKeys()
        {
            var n = NumRows(TableNames.BimDocument);
            var titles = Strs(TableNames.BimDocument, "string:Title") ?? new string[n];
            var paths = Strs(TableNames.BimDocument, "string:PathName") ?? new string[n];
            var seen = new Dictionary<string, int>();
            var r = new string[n];
            for (var i = 0; i < n; ++i)
            {
                var k = (titles[i] ?? "") + "|" + (paths[i] ?? "");
                seen.TryGetValue(k, out var ordinal);
                seen[k] = ordinal + 1;
                r[i] = k + "|" + ordinal;
            }
            return r;
        }

        /// <summary>
        /// One identity per Element row: BimDocument key + UniqueId (after <paramref name="uniqueIdAlias"/>).
        /// A key repeated in the same file gets "#n" appended to its later occurrences.
        /// </summary>
        public string[] ElementKeys(Func<string, string> uniqueIdAlias = null)
        {
            var n = NumRows(TableNames.Element);
            var docKeys = BimDocumentKeys();
            var docs = Index(TableNames.Element, "index:" + TableNames.BimDocument + ":BimDocument");
            var uids = Strs(TableNames.Element, "string:UniqueId") ?? new string[n];
            var seen = new Dictionary<string, int>();
            var r = new string[n];
            for (var i = 0; i < n; ++i)
            {
                var d = docs != null && docs[i] >= 0 && docs[i] < docKeys.Length ? docKeys[docs[i]] : "";
                var u = uids[i] ?? "";
                if (uniqueIdAlias != null)
                    u = uniqueIdAlias(u) ?? u;
                var k = d + "\u001f" + u;
                if (seen.TryGetValue(k, out var count))
                {
                    seen[k] = count + 1;
                    k = k + "#" + count;
                }
                else
                {
                    seen[k] = 1;
                }
                r[i] = k;
            }
            return r;
        }

        /// <summary>The BimDocument title of each Element row ("" when none).</summary>
        public string[] ElementDocumentTitles()
        {
            var n = NumRows(TableNames.Element);
            var titles = Strs(TableNames.BimDocument, "string:Title") ?? Array.Empty<string>();
            var docs = Index(TableNames.Element, "index:" + TableNames.BimDocument + ":BimDocument");
            var r = new string[n];
            for (var i = 0; i < n; ++i)
                r[i] = docs != null && docs[i] >= 0 && docs[i] < titles.Length ? titles[docs[i]] ?? "" : "";
            return r;
        }

        /// <summary>
        /// Files written before object model 5.10.0 (or by an exporter which does not record it) have no ExportRevision:
        /// adds one row per BimDocument, built from this file's header and the document's NumSaves / Guid, and binds every
        /// element of a document to it. Does nothing when the file already binds elements to export revisions.
        /// </summary>
        public void EnsureExportRevisions(ExportRevisionKind kind)
        {
            if (NumRows(TableNames.ElementInExportRevision) > 0)
                return;

            var nDocs = NumRows(TableNames.BimDocument);
            var strings = new List<string>(Strings);
            int S(string s) { strings.Add(s ?? ""); return strings.Count - 1; }

            var persistingId = Header?.PersistingId ?? "";
            var generator = Header?.Generator ?? "";
            var created = Header == null ? "" : Header.CreationDate.ToUniversalTime().ToString("o", System.Globalization.CultureInfo.InvariantCulture);
            var numSaves = Data<int>(TableNames.BimDocument, "int:NumSaves");
            var guids = Strs(TableNames.BimDocument, "string:Guid");

            var revision = new SerializableEntityTable { Name = TableNames.ExportRevision };
            revision.StringColumns.Add(Enumerable.Range(0, nDocs).Select(_ => S(persistingId)).ToArray().ToNamedBuffer("string:VimRevision"));
            revision.StringColumns.Add(Enumerable.Range(0, nDocs).Select(_ => S(generator)).ToArray().ToNamedBuffer("string:Generator"));
            revision.StringColumns.Add(Enumerable.Range(0, nDocs).Select(_ => S(created)).ToArray().ToNamedBuffer("string:CreatedUtc"));
            revision.StringColumns.Add(Enumerable.Range(0, nDocs).Select(d => S(guids?[d])).ToArray().ToNamedBuffer("string:DocumentVersionGuid"));
            revision.DataColumns.Add(Enumerable.Repeat((int)kind, nDocs).ToArray().ToNamedBuffer("int:Kind"));
            revision.DataColumns.Add(Enumerable.Range(0, nDocs).Select(d => numSaves?[d] ?? 0).ToArray().ToNamedBuffer("int:DocumentNumSaves"));
            revision.IndexColumns.Add(Enumerable.Range(0, nDocs).ToArray().ToNamedBuffer("index:" + TableNames.BimDocument + ":BimDocument"));

            var docs = Index(TableNames.Element, "index:" + TableNames.BimDocument + ":BimDocument") ?? Array.Empty<int>();
            var elements = Enumerable.Range(0, docs.Length).Where(e => docs[e] >= 0 && docs[e] < nDocs).ToArray();
            var binding = new SerializableEntityTable { Name = TableNames.ElementInExportRevision };
            binding.IndexColumns.Add(elements.Select(e => docs[e]).ToArray().ToNamedBuffer("index:" + TableNames.ExportRevision + ":ExportRevision"));
            binding.IndexColumns.Add(elements.ToArray().ToNamedBuffer(ElementColumn));

            Strings = strings.ToArray();
            Tables[TableNames.ExportRevision] = revision;
            Tables[TableNames.ElementInExportRevision] = binding;
            _numRows.Remove(TableNames.ExportRevision);
            _numRows.Remove(TableNames.ElementInExportRevision);
        }

        /// <summary>The related table name of each index column of the table.</summary>
        public IEnumerable<(string Column, string Related)> IndexRelations(string table)
            => (GetTable(table)?.IndexColumns ?? new List<NamedBuffer<int>>())
                .Select(c => (c.Name, ColumnExtensions.GetRelatedTableNameFromColumnName(c.Name)));
    }
}
