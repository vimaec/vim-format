using System.Collections.Generic;
using System.Globalization;

namespace Vim.Format.Merge
{
    /// <summary>
    /// The IsReadOnly a file's parameter descriptors carry, per document. The Revit exporter writes one descriptor per
    /// parameter definition (RevitToVimStore.StoreParameterDescriptor keys them by definition id) and gives it the
    /// IsReadOnly of the first parameter of that definition the export meets - Revit's Parameter.IsReadOnly being per
    /// element. A partial export ("Sync to VIM") meets other elements first and so wrote the same definition with the
    /// other IsReadOnly: a descriptor the full export does not have, and every re-exported value pointed at it (Diff
    /// series: 19 descriptors and 4,948 STRUCTURE elements in version 1, "Elevation from Level" on 63 elements in
    /// version 3). With this map the partial export writes the descriptor exactly as the file has it, and the patch
    /// matches it to the file's row by its storage key, IsReadOnly included.
    /// The map is per document because the definition id of a project or shared parameter is the document's own, so a
    /// full export holds one descriptor per document for it - and they can differ by IsReadOnly alone.
    /// </summary>
    public static class DescriptorReadOnly
    {
        public const string HostDocumentKey = "\u0001host";

        /// <summary>The key of a descriptor in a document: every field of its storage key but IsReadOnly and its display unit.</summary>
        public static string Key(string documentKey, string name, string group, string parameterType, int storageType,
            bool isInstance, bool isShared, int flags, string guid)
            => string.Join("\u001f", documentKey ?? "", name ?? "", group ?? "", parameterType ?? "",
                storageType.ToString(CultureInfo.InvariantCulture), isInstance ? "1" : "0", isShared ? "1" : "0",
                flags.ToString(CultureInfo.InvariantCulture), guid ?? "");

        /// <summary>
        /// Reads, for every document of the file (the host as <see cref="HostDocumentKey"/>, a linked model by its title),
        /// the IsReadOnly of the descriptor its parameter values point at. A key seen with both values keeps the one most
        /// values use.
        /// </summary>
        public static Dictionary<string, bool> FromFile(VimTables t)
        {
            var result = new Dictionary<string, bool>();
            var nd = t.NumRows(TableNames.ParameterDescriptor);
            if (nd == 0) return result;
            const string pd = TableNames.ParameterDescriptor;
            var names = t.Strs(pd, "string:Name");
            var groups = t.Strs(pd, "string:Group");
            var types = t.Strs(pd, "string:ParameterType");
            var guids = t.Strs(pd, "string:Guid");
            var storage = t.Data<int>(pd, "int:StorageType");
            var flags = t.Data<int>(pd, "int:Flags");
            var inst = t.Data<byte>(pd, "byte:IsInstance");
            var shared = t.Data<byte>(pd, "byte:IsShared");
            var ro = t.Data<byte>(pd, "byte:IsReadOnly");
            if (ro == null) return result;

            var host = t.HostBimDocuments();
            var titles = t.Strs(TableNames.BimDocument, "string:Title") ?? new string[host.Length];
            var elementDocs = t.Index(TableNames.Element, "index:" + TableNames.BimDocument + ":BimDocument");
            var paramElement = t.Index(TableNames.Parameter, VimTables.ElementColumn);
            var paramDesc = t.Index(TableNames.Parameter, "index:" + TableNames.ParameterDescriptor + ":ParameterDescriptor");
            if (elementDocs == null || paramElement == null || paramDesc == null) return result;

            // (document, descriptor row) pairs in use, counted.
            var used = new Dictionary<(int Doc, int Desc), int>();
            for (var r = 0; r < paramDesc.Length; ++r)
            {
                var d = paramDesc[r];
                var e = paramElement[r];
                if (d < 0 || d >= nd || e < 0 || e >= elementDocs.Length) continue;
                var k = (elementDocs[e], d);
                used.TryGetValue(k, out var n);
                used[k] = n + 1;
            }

            var votes = new Dictionary<string, (int ReadOnly, int Writable)>();
            foreach (var kv in used)
            {
                var (doc, d) = kv.Key;
                var docKey = doc < 0 || doc >= host.Length ? "" : host[doc] ? HostDocumentKey : titles[doc] ?? "";
                var key = Key(docKey, names?[d], groups?[d], types?[d], storage?[d] ?? 0, (inst?[d] ?? 0) != 0,
                    (shared?[d] ?? 0) != 0, flags?[d] ?? 0, guids?[d]);
                votes.TryGetValue(key, out var v);
                votes[key] = ro[d] != 0 ? (v.ReadOnly + kv.Value, v.Writable) : (v.ReadOnly, v.Writable + kv.Value);
            }
            foreach (var kv in votes)
                result[kv.Key] = kv.Value.ReadOnly > kv.Value.Writable;
            return result;
        }
    }
}
