using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Vim.Format.ObjectModel;
using Vim.LinqArray;

namespace Vim.Format.Merge
{
    public class VimExtractOptions
    {
        /// <summary>Only Shape rows of these kinds are written for the selected elements (null = all kinds).</summary>
        public ICollection<int> ShapeKinds { get; set; }

        /// <summary>Only ElementInView rows for views with these UniqueIds are written (null = all views).</summary>
        public ICollection<string> ElementInViewUniqueIds { get; set; }

        public string Generator { get; set; } = "Vim.Format.Extract";
    }

    /// <summary>
    /// Writes a VIM file holding a subset of the elements of another one, the way a partial export of those elements
    /// would: the selected elements with every row they own, plus every row those rows point at (types, families,
    /// levels, materials, views, documents, hosts...) with the rows those elements own. Used to build patch fixtures and
    /// to test <see cref="VimPatchService"/>.
    /// </summary>
    public static class VimExtractService
    {
        private static readonly Regex AssociationTableName = new Regex(@"^Vim\.\w+In\w+$", RegexOptions.Compiled);

        public static DocumentBuilder Extract(VimTables src, ICollection<string> uniqueIds, VimExtractOptions options = null)
        {
            options = options ?? new VimExtractOptions();
            var sources = new[] { src };
            var tables = src.Tables.Keys.Where(n => !VimConstants.ComputedTableNames.Contains(n)).OrderBy(n => n, StringComparer.Ordinal).ToArray();
            var included = tables.ToDictionary(t => t, t => new bool[src.NumRows(t)]);
            var relations = tables.ToDictionary(t => t, t => src.IndexRelations(t).Select(r => (Col: src.Index(t, r.Column), r.Related)).ToArray());

            // Rows owned by each element, per element-owned table (CSR).
            var owned = new Dictionary<string, int[][]>();
            var numElements = src.NumRows(TableNames.Element);
            foreach (var t in tables.Where(src.IsElementOwned))
            {
                var owner = src.Index(t, VimTables.ElementColumn);
                var lists = new List<int>[numElements];
                for (var r = 0; r < owner.Length; ++r)
                {
                    var e = owner[r];
                    if (e < 0 || e >= numElements) continue;
                    (lists[e] = lists[e] ?? new List<int>()).Add(r);
                }
                owned[t] = lists.Select(l => l?.ToArray()).ToArray();
            }

            var shapeKinds = options.ShapeKinds == null ? null : new HashSet<int>(options.ShapeKinds);
            var kinds = src.Data<int>(TableNames.Shape, "int:Kind");
            var uids = src.Strs(TableNames.Element, "string:UniqueId") ?? new string[numElements];
            HashSet<int> eivViews = null;
            if (options.ElementInViewUniqueIds != null)
            {
                var wanted = new HashSet<string>(options.ElementInViewUniqueIds);
                var viewOwner = src.Index(TableNames.View, VimTables.ElementColumn) ?? Array.Empty<int>();
                eivViews = new HashSet<int>(Enumerable.Range(0, viewOwner.Length).Where(v => viewOwner[v] >= 0 && wanted.Contains(uids[viewOwner[v]])));
            }
            var eivView = src.Index(TableNames.ElementInView, "index:" + TableNames.View + ":View");

            bool OwnedRowWanted(string table, int r)
            {
                if (table == TableNames.Shape && shapeKinds != null)
                    return shapeKinds.Contains(kinds == null ? 0 : kinds[r]);
                if (table == TableNames.ElementInView && eivViews != null)
                    return eivView != null && eivViews.Contains(eivView[r]);
                return true;
            }

            var work = new Stack<(string Table, int Row)>();
            void Include(string t, int r)
            {
                if (r < 0 || !included.TryGetValue(t, out var inc) || r >= inc.Length || inc[r]) return;
                inc[r] = true;
                work.Push((t, r));
            }

            var wantedUids = new HashSet<string>(uniqueIds);
            for (var e = 0; e < numElements; ++e)
                if (wantedUids.Contains(uids[e] ?? ""))
                    Include(TableNames.Element, e);

            // Materials used by the meshes of the written nodes (no table row has to point at them).
            var g = src.Geometry;
            var instanceMeshes = g?.InstanceMeshes?.ToArray() ?? Array.Empty<int>();
            var meshSubmeshOffsets = g?.MeshSubmeshOffset?.ToArray() ?? Array.Empty<int>();
            var submeshMaterials = g?.SubmeshMaterials?.ToArray() ?? Array.Empty<int>();

            while (work.Count > 0)
            {
                var (t, r) = work.Pop();
                if (t == TableNames.Node && r < instanceMeshes.Length)
                {
                    var m = instanceMeshes[r];
                    if (m >= 0 && m < meshSubmeshOffsets.Length)
                    {
                        var end = m + 1 < meshSubmeshOffsets.Length ? meshSubmeshOffsets[m + 1] : submeshMaterials.Length;
                        for (var s = meshSubmeshOffsets[m]; s < end; ++s)
                            Include(TableNames.Material, submeshMaterials[s]);
                    }
                }
                foreach (var (col, related) in relations[t])
                    if (col != null)
                        Include(related, col[r]);
                if (t == TableNames.Element)
                    foreach (var kv in owned)
                    {
                        var rows = kv.Value[r];
                        if (rows == null) continue;
                        foreach (var orow in rows)
                            if (OwnedRowWanted(kv.Key, orow))
                                Include(kv.Key, orow);
                    }
            }

            // Associations (and rows owned through their first relation) whose ends are all written.
            var assoc = tables.Where(t => !src.IsElementOwned(t) && t != TableNames.Element
                && (AssociationTableName.IsMatch(t) || t == TableNames.ScheduleColumn || t == TableNames.ScheduleCell || t == TableNames.CompoundStructureLayer)).ToArray();
            bool changed;
            do
            {
                changed = false;
                foreach (var t in assoc)
                {
                    var inc = included[t];
                    for (var r = 0; r < inc.Length; ++r)
                    {
                        if (inc[r]) continue;
                        var ok = true;
                        var any = false;
                        foreach (var (col, related) in relations[t])
                        {
                            if (col == null || col[r] < 0) continue;
                            any = true;
                            if (!included.TryGetValue(related, out var ri) || col[r] >= ri.Length || !ri[col[r]]) { ok = false; break; }
                        }
                        if (ok && any) { inc[r] = true; changed = true; }
                    }
                }
            } while (changed);

            var plans = new Dictionary<string, TablePlan>();
            foreach (var t in tables)
            {
                var plan = new TablePlan(t, sources);
                var inc = included[t];
                for (var r = 0; r < inc.Length; ++r)
                    if (inc[r])
                        plan.Add(0, r);
                plans[t] = plan;
            }

            var header = new SerializableHeader(options.Generator, src.Header?.Schema ?? SchemaVersion.Current, "");
            return RowPlanWriter.Write(sources, plans, header, false);
        }
    }
}
