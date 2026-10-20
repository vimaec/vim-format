using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using Vim.Format.ObjectModel;
using Vim.Util;

namespace Vim.Format.Merge
{
    /// <summary>
    /// Patches a base VIM file with a partial VIM file exported from the same source document (only the elements that
    /// changed, plus what they point at): changed elements are replaced, new ones appended, deleted ones removed; every
    /// other row of the base is kept and every relation is re-pointed. This is the second half of a differential update
    /// (the first half, finding what changed and exporting it, belongs to the exporter).
    /// <para>Rules, per table:</para>
    /// <list type="bullet">
    /// <item>Element: identity = document + UniqueId, where the document is "the host model" for every BimDocument that is
    /// not a link (whatever its title, path or project name) and the title for a linked model. A changed element keeps its
    /// row index (its content comes from the partial); new elements are appended; deleted ones are removed.</item>
    /// <item>Tables owned by an element with at most one row per element (FamilyInstance, Level, Room, View, Material,
    /// BimDocument...): the row follows its element, in place.</item>
    /// <item>Tables with several rows per element (Parameter, Node, Shape, ElementInView, MaterialInElement,
    /// ElementInSystem, ElementInWarning): a changed element's base rows are replaced by the partial's (for
    /// ElementInView: only the rows of the views the partial writes).</item>
    /// <item>Other tables (Category, DisplayUnit, ParameterDescriptor, Workset, associations): the base rows stay; a
    /// partial row equal to a base row (by key or by content) is that row, otherwise it is appended; an association row
    /// whose end is gone is removed. Warning rows of the partial are appended; warnings, wall layer structures and cameras
    /// nothing points at any more are removed.</item>
    /// <item>Geometry: Node rows = instances, Shape rows = shapes, Material rows = materials; meshes are the ones the
    /// written instances use.</item>
    /// </list>
    /// </summary>
    public static class VimPatchService
    {
        private enum Role { None = 0, Changed, Context, New, Excluded }

        private const string HostDocumentKey = "\u0001host";
        private const string SharedDocumentKey = "\u0002shared";

        /// <summary>
        /// Tables whose rows are relations between rows of other tables (X in Y), plus the tables whose rows belong to
        /// the row their first relation points at: a row whose end is gone goes with it.
        /// </summary>
        private static readonly HashSet<string> AssociationTables = new HashSet<string>
        {
            TableNames.ShapeInShapeCollection, TableNames.PhaseOrderInBimDocument, TableNames.ElementInView,
            TableNames.ShapeInView, TableNames.AssetInView, TableNames.AssetInViewSheet, TableNames.LevelInView,
            TableNames.DisplayUnitInBimDocument, TableNames.ElementInSystem, TableNames.ElementInWarning,
            TableNames.MaterialInElement, TableNames.ViewSheetInViewSheetSet, TableNames.ViewInViewSheetSet,
            TableNames.ViewInViewSheet,
            TableNames.ScheduleColumn, TableNames.ScheduleCell, TableNames.CompoundStructureLayer,
        };

        /// <summary>Element-owned tables which can hold several rows per element, whatever a given file holds.</summary>
        private static readonly HashSet<string> ManyRowsPerElementTables = new HashSet<string>
        {
            TableNames.Node, TableNames.Shape, TableNames.Parameter, TableNames.ElementInView, TableNames.MaterialInElement,
            TableNames.ElementInSystem, TableNames.ElementInWarning, TableNames.ShapeCollection,
        };

        /// <summary>
        /// Rows the Revit exporter writes once per export whatever the document (system families, system family types,
        /// unassigned systems): they are filed under the first document that needed one, which differs between a full
        /// and a partial export, so their UniqueId alone identifies them.
        /// </summary>
        private static readonly HashSet<string> SharedSyntheticTypes = new HashSet<string>(StringComparer.Ordinal)
        {
            VimConstants.SystemFamilyElementType, VimConstants.SystemFamilyTypeElementType, VimConstants.UnassignedSystem,
        };

        /// <summary>The rows the exporter writes for a document as a whole; their UniqueId is made from the project name.</summary>
        private static readonly HashSet<string> HolderTypes = new HashSet<string>(StringComparer.Ordinal)
        {
            VimConstants.BimDocumentParameterHolderElementType, VimConstants.SiteParameterHolderElementType, VimConstants.BuildingParameterHolderElementType,
        };

        /// <summary>Patches the files and writes the result. Returns the counts and timings.</summary>
        public static VimPatchResult Patch(string baseVimPath, string partialVimPath, string outputVimPath, VimPatchOptions options = null)
        {
            var sw = Stopwatch.StartNew();
            var baseTables = VimTables.Load(baseVimPath);
            var partialTables = VimTables.Load(partialVimPath);
            var loadMs = sw.ElapsedMilliseconds;

            var (db, result) = Patch(baseTables, partialTables, options);
            result.LoadMs = loadMs;

            sw.Restart();
            db.Write(outputVimPath);
            result.WriteMs = sw.ElapsedMilliseconds;
            return result;
        }

        /// <summary>Patches in memory; the returned builder writes the patched file.</summary>
        public static (DocumentBuilder Builder, VimPatchResult Result) Patch(VimTables baseVim, VimTables partialVim, VimPatchOptions options = null)
        {
            options = options ?? new VimPatchOptions();
            var result = new VimPatchResult();
            var sw = Stopwatch.StartNew();

            var baseSchema = baseVim.Header?.Schema;
            var partialSchema = partialVim.Header?.Schema;
            if (baseSchema != null && partialSchema != null && baseSchema.Major != partialSchema.Major)
                throw new HResultException((int)ErrorCode.VimMergeObjectModelMajorVersionMismatch,
                    $"Object model major version mismatch: base {baseSchema}, partial {partialSchema}");

            var sources = new[] { baseVim, partialVim };
            var tableNames = baseVim.Tables.Keys.Concat(partialVim.Tables.Keys)
                .Where(n => !VimConstants.ComputedTableNames.Contains(n))
                .Distinct()
                .OrderBy(n => n, StringComparer.Ordinal)
                .ToArray();
            var plans = tableNames.ToDictionary(n => n, n => new TablePlan(n, sources));

            // 1. Elements.
            var (partialRole, baseChanged) = PlanElements(baseVim, partialVim, options, plans[TableNames.Element], result);

            // 2. Element-owned tables (ElementInView last: its scope needs the View rows).
            var elementOwned = tableNames.Where(n => baseVim.IsElementOwned(n) || partialVim.IsElementOwned(n)).ToList();
            var ordered = elementOwned.Where(n => n != TableNames.ElementInView).ToList();
            if (elementOwned.Contains(TableNames.ElementInView))
                ordered.Add(TableNames.ElementInView);
            foreach (var table in ordered)
                PlanElementOwnedTable(table, sources, plans, partialRole, baseChanged);

            // 3. Other tables, referees before referrers.
            var others = tableNames.Where(n => n != TableNames.Element && !elementOwned.Contains(n)).ToList();
            var (order, cyclic) = TopologicalOrder(others, sources);
            foreach (var table in order)
                PlanDictionaryTable(table, sources, plans, cyclic.Contains(table));

            // 4. Rows nothing points at any more: a full export would not write them (a warning naming no exported
            // element, a replaced type's wall layers, a camera of a view that is gone).
            result.OrphanRowsRemoved += PruneUnreferenced(TableNames.Warning, null, sources, plans);
            result.OrphanRowsRemoved += PruneUnreferenced(TableNames.CompoundStructure, TableNames.CompoundStructureLayer, sources, plans);
            result.OrphanRowsRemoved += PruneLayersOfRemovedStructures(sources, plans);
            result.OrphanRowsRemoved += PruneUnreferenced(TableNames.Camera, null, sources, plans);

            result.PlanMs = sw.ElapsedMilliseconds;
            sw.Restart();

            // 5. Write.
            var schema = baseSchema;
            if (schema == null || (partialSchema != null && partialSchema.Major == schema.Major && partialSchema.Minor > schema.Minor))
                schema = partialSchema ?? SchemaVersion.Current;
            var values = new Dictionary<string, string>
            {
                { "patched", DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture) },
            };
            foreach (var kv in options.HeaderValues ?? new Dictionary<string, string>())
                values[kv.Key] = kv.Value;
            var header = baseVim.Header != null
                ? SerializableHeader.CreateRevisionOf(baseVim.Header, options.Generator, schema, values)
                : new SerializableHeader(options.Generator, schema, "", values);

            var db = RowPlanWriter.Write(sources, plans, header, options.DeduplicateMeshes, result.Geometry);
            foreach (var kv in plans)
                result.TableRows[kv.Key] = (baseVim.NumRows(kv.Key), partialVim.NumRows(kv.Key), kv.Value.Count);
            result.BuildMs = sw.ElapsedMilliseconds;
            return (db, result);
        }

        /// <summary>
        /// Decides, element by element, what the output holds: the base row (kept, or context), the partial's row in its
        /// place (changed), a partial row appended (new), or nothing (deleted, excluded).
        /// </summary>
        private static (Role[] PartialRole, bool[] BaseChanged) PlanElements(VimTables baseVim, VimTables partialVim,
            VimPatchOptions options, TablePlan elementPlan, VimPatchResult result)
        {
            var aliases = options.UniqueIdAliases ?? new Dictionary<string, string>();
            Func<string, string> alias = u => u != null && aliases.TryGetValue(u, out var a) ? a : u;
            var baseKeys = ElementKeys(baseVim, null);
            var partialKeys = ElementKeys(partialVim, alias);
            var forceChanged = new bool[baseKeys.Length];
            result.HoldersMatched = MatchHolders(baseVim, partialVim, baseKeys, partialKeys, forceChanged);
            var baseUids = baseVim.Strs(TableNames.Element, "string:UniqueId") ?? new string[baseKeys.Length];
            var partialUids = partialVim.Strs(TableNames.Element, "string:UniqueId") ?? new string[partialKeys.Length];
            var baseDocTitles = baseVim.ElementDocumentTitles();

            var changedSet = options.ChangedUniqueIds == null ? null : new HashSet<string>(options.ChangedUniqueIds);
            var deletedSet = new HashSet<string>(options.DeletedUniqueIds ?? Array.Empty<string>());
            var excludedSet = new HashSet<string>(options.ExcludedUniqueIds ?? Array.Empty<string>());
            var replacedDocs = new HashSet<string>(options.ReplacedDocumentTitles ?? Array.Empty<string>());
            var aliasTargets = new HashSet<string>(aliases.Values);

            var partialByKey = new Dictionary<string, int>();
            for (var i = 0; i < partialKeys.Length; ++i)
                partialByKey[partialKeys[i]] = i;

            var partialRole = new Role[partialKeys.Length];
            var baseChanged = new bool[baseKeys.Length];
            var seenDeleted = new HashSet<string>();

            for (var b = 0; b < baseKeys.Length; ++b)
            {
                var uid = baseUids[b] ?? "";
                var inReplacedDoc = replacedDocs.Contains(baseDocTitles[b]);
                var hasPartial = partialByKey.TryGetValue(baseKeys[b], out var p);
                if (deletedSet.Contains(uid) || (inReplacedDoc && !hasPartial))
                {
                    if (deletedSet.Contains(uid)) seenDeleted.Add(uid);
                    result.Deleted++;
                    continue;
                }
                if (!hasPartial)
                {
                    if (changedSet != null && changedSet.Contains(uid)) result.ChangedButNotInPartial.Add(uid);
                    elementPlan.Add(0, b);
                    result.Kept++;
                    continue;
                }
                var isAliasTarget = aliasTargets.Contains(uid) && aliases.ContainsKey(partialUids[p] ?? "");
                var changed = !isAliasTarget && !excludedSet.Contains(partialUids[p] ?? "")
                    && (changedSet == null || changedSet.Contains(uid) || inReplacedDoc || forceChanged[b]);
                if (changed)
                {
                    baseChanged[b] = true;
                    partialRole[p] = Role.Changed;
                    elementPlan.Alias(0, b, elementPlan.Add(1, p));
                    result.Changed++;
                }
                else
                {
                    partialRole[p] = Role.Context;
                    elementPlan.Alias(1, p, elementPlan.Add(0, b));
                    result.Context++;
                    result.Kept++;
                }
            }

            var baseOutByKey = new Lazy<Dictionary<string, int>>(() =>
            {
                var d = new Dictionary<string, int>();
                for (var b = 0; b < baseKeys.Length; ++b)
                {
                    var o = elementPlan.Lookup(0, b);
                    if (o >= 0) d[baseKeys[b]] = o;
                }
                return d;
            });
            for (var p = 0; p < partialKeys.Length; ++p)
            {
                if (partialRole[p] != Role.None) continue;
                var uid = partialUids[p] ?? "";
                if (aliasTargets.Contains(uid) && partialKeys[p].Contains("#")
                    && baseOutByKey.Value.TryGetValue(partialKeys[p].Substring(0, partialKeys[p].LastIndexOf('#')), out var twin))
                {
                    // The element an alias stands for, held by the partial as well (the export view, next to the
                    // temporary view aliased onto it): the same element, as context.
                    partialRole[p] = Role.Context;
                    elementPlan.Alias(1, p, twin);
                    result.Context++;
                    continue;
                }
                if (excludedSet.Contains(uid) || aliases.ContainsKey(uid) || deletedSet.Contains(uid))
                {
                    // Excluded, deleted, or an alias whose target the base does not hold: not written.
                    partialRole[p] = Role.Excluded;
                    result.Excluded++;
                    continue;
                }
                partialRole[p] = Role.New;
                elementPlan.Add(1, p);
                result.Added++;
            }

            result.DeletedButNotInBase = deletedSet.Where(u => !seenDeleted.Contains(u)).ToList();
            result.BaseElements = baseKeys.Length;
            result.PartialElements = partialKeys.Length;
            result.OutputElements = elementPlan.Count;
            result.ElementIndexMap = (int[])elementPlan.Map[0].Clone();
            return (partialRole, baseChanged);
        }

        /// <summary>
        /// The document part of the element keys, per BimDocument row: every document that is not a link is the host,
        /// whatever its title, path or project name (the same model is saved as several local files, renamed, moved);
        /// a linked model is its title plus its ordinal among the rows with that title.
        /// </summary>
        private static string[] DocumentKeys(VimTables t)
        {
            var titles = t.Strs(TableNames.BimDocument, "string:Title") ?? new string[t.NumRows(TableNames.BimDocument)];
            var host = t.HostBimDocuments();
            var seen = new Dictionary<string, int>();
            var keys = new string[titles.Length];
            for (var i = 0; i < titles.Length; ++i)
            {
                if (i < host.Length && host[i]) { keys[i] = HostDocumentKey; continue; }
                var title = titles[i] ?? "";
                seen.TryGetValue(title, out var ordinal);
                seen[title] = ordinal + 1;
                keys[i] = title + "|" + ordinal;
            }
            return keys;
        }

        /// <summary>
        /// One identity per Element row: document key + UniqueId (after the aliases). A key repeated in the same file
        /// gets "#n" appended to its later occurrences. A row given another element's UniqueId by an alias takes the plain
        /// key first (pass 0), so the element it stands for, when the partial holds it too, is the duplicate.
        /// </summary>
        private static string[] ElementKeys(VimTables t, Func<string, string> alias)
        {
            var docKeys = DocumentKeys(t);
            var count = t.NumRows(TableNames.Element);
            var keys = new string[count];
            var docs = t.Index(TableNames.Element, VimTables.BimDocumentColumn);
            var uids = t.Strs(TableNames.Element, "string:UniqueId") ?? new string[count];
            var ids = t.Data<long>(TableNames.Element, "long:Id");
            var types = t.Strs(TableNames.Element, "string:Type");
            var dup = new Dictionary<string, int>();
            for (var pass = 0; pass < 2; ++pass)
            {
                for (var i = 0; i < count; ++i)
                {
                    var raw = uids[i] ?? "";
                    var u = alias == null ? raw : alias(raw) ?? raw;
                    var aliased = !string.Equals(u, raw, StringComparison.Ordinal);
                    if ((pass == 0) != aliased) continue;
                    var d = docs != null && docs[i] >= 0 && docs[i] < docKeys.Length ? docKeys[docs[i]] : "";
                    if (ids != null && types != null && ids[i] == VimConstants.SyntheticElementId && SharedSyntheticTypes.Contains(types[i] ?? ""))
                        d = SharedDocumentKey;
                    var k = d + "\u001f" + u;
                    if (dup.TryGetValue(k, out var c)) { dup[k] = c + 1; k = k + "#" + c; } else dup[k] = 1;
                    keys[i] = k;
                }
            }
            return keys;
        }

        /// <summary>
        /// The rows the exporter writes for a whole document - its parameter holder (the Element of BimDocument, which
        /// carries Project Information) and its site and building holders - have a UniqueId made from the project name.
        /// The project name is data, not identity: a partial holder is the base holder of the same kind in the same
        /// document, whatever its name, and it is always written as changed, so the BimDocument row and Project
        /// Information are refreshed in place. Rewrites partialKeys, marks forceChanged (base rows), returns the count.
        /// </summary>
        private static int MatchHolders(VimTables baseVim, VimTables partialVim, string[] baseKeys, string[] partialKeys, bool[] forceChanged)
        {
            var bIds = baseVim.Data<long>(TableNames.Element, "long:Id");
            var pIds = partialVim.Data<long>(TableNames.Element, "long:Id");
            var bTypes = baseVim.Strs(TableNames.Element, "string:Type");
            var pTypes = partialVim.Strs(TableNames.Element, "string:Type");
            var bUids = baseVim.Strs(TableNames.Element, "string:UniqueId");
            var pUids = partialVim.Strs(TableNames.Element, "string:UniqueId");
            var bDocs = baseVim.Index(TableNames.Element, VimTables.BimDocumentColumn);
            var pDocs = partialVim.Index(TableNames.Element, VimTables.BimDocumentColumn);
            if (bIds == null || pIds == null || bTypes == null || pTypes == null || bDocs == null || pDocs == null || bUids == null || pUids == null)
                return 0;
            var bDocKeys = DocumentKeys(baseVim);
            var pDocKeys = DocumentKeys(partialVim);
            string DocOf(string[] dk, int[] docs, int e) => docs[e] >= 0 && docs[e] < dk.Length ? dk[docs[e]] : "";
            bool IsHolder(long[] ids, string[] types, int e) => ids[e] == VimConstants.SyntheticElementId && HolderTypes.Contains(types[e] ?? "");

            var baseByKey = new Dictionary<string, int>();
            for (var b = 0; b < baseKeys.Length; ++b) baseByKey[baseKeys[b]] = b;

            var claimed = new HashSet<int>();
            var matched = 0;
            var pending = new List<int>();
            for (var p = 0; p < partialKeys.Length; ++p)
            {
                if (!IsHolder(pIds, pTypes, p)) continue;
                if (baseByKey.TryGetValue(partialKeys[p], out var b))
                {
                    claimed.Add(b);
                    forceChanged[b] = true;
                    matched++;
                }
                else
                {
                    pending.Add(p);
                }
            }
            if (pending.Count == 0)
                return matched;

            var baseHolders = new Dictionary<string, List<int>>();
            var baseHoldersByUid = new Dictionary<string, int>();
            for (var b = 0; b < baseKeys.Length; ++b)
            {
                if (!IsHolder(bIds, bTypes, b)) continue;
                var k = DocOf(bDocKeys, bDocs, b) + "\u001f" + bTypes[b];
                if (!baseHolders.TryGetValue(k, out var l)) baseHolders[k] = l = new List<int>();
                l.Add(b);
                if (!baseHoldersByUid.ContainsKey(bUids[b] ?? "")) baseHoldersByUid[bUids[b] ?? ""] = b;
            }
            foreach (var p in pending)
            {
                var b = -1;
                if (baseHolders.TryGetValue(DocOf(pDocKeys, pDocs, p) + "\u001f" + pTypes[p], out var l))
                    foreach (var x in l)
                        if (!claimed.Contains(x)) { b = x; break; }
                if (b >= 0)
                {
                    claimed.Add(b);
                    partialKeys[p] = baseKeys[b];
                    forceChanged[b] = true;
                    matched++;
                    continue;
                }
                // A site or building holder shared with another document of the same project name: the base row is it.
                if (pTypes[p] != VimConstants.BimDocumentParameterHolderElementType
                    && baseHoldersByUid.TryGetValue(pUids[p] ?? "", out var shared) && !claimed.Contains(shared))
                {
                    partialKeys[p] = baseKeys[shared];
                    matched++;
                }
            }
            return matched;
        }

        private static void PlanElementOwnedTable(string table, VimTables[] sources, Dictionary<string, TablePlan> plans,
            Role[] partialRole, bool[] baseChanged)
        {
            var baseVim = sources[0];
            var partialVim = sources[1];
            var plan = plans[table];
            var elementPlan = plans[TableNames.Element];
            var baseOwner = baseVim.Index(table, VimTables.ElementColumn) ?? new int[baseVim.NumRows(table)];
            var partialOwner = partialVim.Index(table, VimTables.ElementColumn) ?? new int[partialVim.NumRows(table)];

            // ElementInView: only the rows of the views the partial writes are replaced (rows for other views stay).
            Func<int, int, bool> inScope = (s, r) => true;
            if (table == TableNames.ElementInView)
            {
                var viewPlan = plans.TryGetValue(TableNames.View, out var vp) ? vp : null;
                var viewColumn = "index:" + TableNames.View + ":View";
                var views = sources.Select(s => s.Index(table, viewColumn)).ToArray();
                var scopeViews = new HashSet<int>();
                if (viewPlan != null && views[1] != null)
                    for (var r = 0; r < partialOwner.Length; ++r)
                    {
                        var e = partialOwner[r];
                        if (e < 0 || (partialRole[e] != Role.Changed && partialRole[e] != Role.New)) continue;
                        var ov = viewPlan.Lookup(1, views[1][r]);
                        if (ov >= 0) scopeViews.Add(ov);
                    }
                inScope = (s, r) =>
                {
                    var v = views[s] == null ? -1 : views[s][r];
                    var ov = viewPlan?.Lookup(s, v) ?? -1;
                    return ov >= 0 && scopeViews.Contains(ov);
                };
            }

            var oneRowPerElement = !ManyRowsPerElementTables.Contains(table) && IsOneRowPerElement(baseOwner) && IsOneRowPerElement(partialOwner);
            if (oneRowPerElement)
            {
                // Rows follow their element, in place.
                var partialRowByOutElement = new Dictionary<int, int>();
                for (var r = 0; r < partialOwner.Length; ++r)
                {
                    var e = partialOwner[r];
                    if (e < 0) continue;
                    var oe = elementPlan.Lookup(1, e);
                    if (oe >= 0) partialRowByOutElement[oe] = r;
                }
                for (var r = 0; r < baseOwner.Length; ++r)
                {
                    var e = baseOwner[r];
                    if (e < 0) { plan.Add(0, r); continue; }
                    var oe = elementPlan.Lookup(0, e);
                    if (oe < 0) continue; // element deleted
                    if (!partialRowByOutElement.TryGetValue(oe, out var pr)) { plan.Add(0, r); continue; }
                    if (baseChanged[e]) plan.Alias(0, r, plan.Add(1, pr));
                    else plan.Alias(1, pr, plan.Add(0, r));
                }
                // Two partial elements standing for one output element (the export view and the temporary view aliased
                // onto it) share that element's row.
                var rowOfOutElement = new Dictionary<int, int>();
                for (var i = 0; i < plan.Count; ++i)
                {
                    var src = plan.Source[i];
                    var e = (src == 0 ? baseOwner : partialOwner)[plan.Row[i]];
                    var oe = e < 0 ? -1 : elementPlan.Lookup(src, e);
                    if (oe >= 0 && !rowOfOutElement.ContainsKey(oe)) rowOfOutElement[oe] = i;
                }
                for (var r = 0; r < partialOwner.Length; ++r)
                {
                    if (plan.Map[1][r] >= 0) continue;
                    var e = partialOwner[r];
                    if (e < 0) { plan.Add(1, r); continue; }
                    if (partialRole[e] == Role.Excluded || partialRole[e] == Role.None) continue;
                    var oe = elementPlan.Lookup(1, e);
                    if (oe < 0) continue;
                    if (rowOfOutElement.TryGetValue(oe, out var existing)) plan.Alias(1, r, existing);
                    else rowOfOutElement[oe] = plan.Add(1, r);
                }
                return;
            }

            // Several rows per element: a changed element's base rows (in scope) give way to the partial's.
            for (var r = 0; r < baseOwner.Length; ++r)
            {
                var e = baseOwner[r];
                if (e < 0) { plan.Add(0, r); continue; }
                if (elementPlan.Lookup(0, e) < 0) continue; // element deleted
                if (baseChanged[e] && inScope(0, r)) continue; // replaced by the partial's rows
                plan.Add(0, r);
            }
            for (var r = 0; r < partialOwner.Length; ++r)
            {
                var e = partialOwner[r];
                if (e < 0) continue;
                var role = partialRole[e];
                if (role != Role.New && !(role == Role.Changed && inScope(1, r))) continue;
                // A new element's row for a view the output does not hold (the temporary view, unaliased).
                if (table == TableNames.ElementInView && !inScope(1, r))
                {
                    var views = partialVim.Index(table, "index:" + TableNames.View + ":View");
                    var v = views == null ? -1 : views[r];
                    if ((plans.TryGetValue(TableNames.View, out var vp) ? vp.Lookup(1, v) : -1) < 0) continue;
                }
                plan.Add(1, r);
            }
        }

        private static bool IsOneRowPerElement(int[] owner)
        {
            var seen = new HashSet<int>();
            foreach (var e in owner)
                if (e >= 0 && !seen.Add(e))
                    return false;
            return true;
        }

        private static (List<string> Order, HashSet<string> Cyclic) TopologicalOrder(List<string> tables, VimTables[] sources)
        {
            var set = new HashSet<string>(tables);
            var deps = tables.ToDictionary(t => t, t => new HashSet<string>(
                sources.SelectMany(s => s.IndexRelations(t)).Select(r => r.Related).Where(r => r != t && set.Contains(r))));
            var order = new List<string>();
            var done = new HashSet<string>();
            bool progress;
            do
            {
                progress = false;
                foreach (var t in tables)
                {
                    if (done.Contains(t) || !deps[t].All(done.Contains)) continue;
                    order.Add(t);
                    done.Add(t);
                    progress = true;
                }
            } while (progress);
            var cyclic = new HashSet<string>(tables.Where(t => !done.Contains(t)));
            order.AddRange(tables.Where(cyclic.Contains));
            return (order, cyclic);
        }

        private static bool IsAssociation(string table)
            => AssociationTables.Contains(table);

        /// <summary>Base rows stay (associations lose rows whose ends are gone); partial rows dedupe by key or content.</summary>
        private static void PlanDictionaryTable(string table, VimTables[] sources, Dictionary<string, TablePlan> plans, bool cyclic)
        {
            var plan = plans[table];
            var assoc = IsAssociation(table);
            var relations = sources.SelectMany(s => s.IndexRelations(table)).Distinct().OrderBy(r => r.Column, StringComparer.Ordinal).ToList();

            bool EndsAlive(int s, int r)
            {
                if (!assoc) return true;
                foreach (var (col, related) in relations)
                {
                    var c = sources[s].Index(table, col);
                    if (c == null || c[r] < 0 || related == table) continue;
                    if (!plans.TryGetValue(related, out var rp) || rp.Lookup(s, c[r]) < 0) return false;
                }
                return true;
            }

            var nb = sources[0].NumRows(table);
            for (var r = 0; r < nb; ++r)
                if (EndsAlive(0, r))
                    plan.Add(0, r);

            var np = sources[1].NumRows(table);
            if (np == 0) return;

            // Two warnings with the same text are two warnings (they name different elements): the partial's are
            // appended; the ones no element names any more are pruned afterwards.
            if (cyclic || table == TableNames.Warning)
            {
                for (var r = 0; r < np; ++r)
                    if (EndsAlive(1, r))
                        plan.Add(1, r);
                return;
            }

            var keyOf = KeyFunction(table, sources, plans, relations);
            var baseIndex = new Dictionary<string, int>();
            for (var i = 0; i < plan.Count; ++i)
            {
                var bk = keyOf(0, plan.Row[i]);
                if (!baseIndex.ContainsKey(bk)) baseIndex[bk] = i;
            }
            for (var r = 0; r < np; ++r)
            {
                if (!EndsAlive(1, r)) continue;
                var k = keyOf(1, r);
                if (baseIndex.TryGetValue(k, out var o)) { plan.Alias(1, r, o); continue; }
                baseIndex[k] = plan.Add(1, r);
            }
        }

        /// <summary>
        /// What makes two rows of a dictionary table the same row: a few key columns for the tables that have a natural
        /// key, otherwise every column with relations re-pointed. A parameter descriptor ignores IsReadOnly: the exporter
        /// gives a descriptor the IsReadOnly of the first parameter it meets (Revit's flag is per element), which a
        /// partial export meets in another order - the base's row is the descriptor.
        /// </summary>
        private static Func<int, int, string> KeyFunction(string table, VimTables[] sources, Dictionary<string, TablePlan> plans,
            List<(string Column, string Related)> relations)
        {
            string[] keyStringColumns = null;
            switch (table)
            {
                case TableNames.Category: keyStringColumns = new[] { "string:Name", "string:BuiltInCategory" }; break;
                case TableNames.DisplayUnit: keyStringColumns = new[] { "string:Spec", "string:Type", "string:Label" }; break;
                case TableNames.Asset: keyStringColumns = new[] { "string:BufferName" }; break;
            }
            var ignoredDataColumns = table == TableNames.ParameterDescriptor
                ? new HashSet<string> { "byte:IsReadOnly" }
                : new HashSet<string>();

            var stringCols = keyStringColumns ?? sources.SelectMany(s => s.GetTable(table)?.StringColumns.Select(c => c.Name) ?? Enumerable.Empty<string>())
                .Distinct().OrderBy(x => x, StringComparer.Ordinal).ToArray();
            var dataCols = keyStringColumns != null ? Array.Empty<string>() : sources.SelectMany(s => s.GetTable(table)?.DataColumns.Select(c => c.Name) ?? Enumerable.Empty<string>())
                .Where(c => !ignoredDataColumns.Contains(c))
                .Distinct().OrderBy(x => x, StringComparer.Ordinal).ToArray();
            var indexCols = keyStringColumns != null ? new List<(string Column, string Related)>() : relations.Where(r => r.Related != table).ToList();

            var strs = sources.Select(s => stringCols.Select(c => s.StringIndices(table, c)).ToArray()).ToArray();
            var datas = sources.Select(s => dataCols.Select(c => s.Data(table, c)?.Data).ToArray()).ToArray();
            var idxs = sources.Select(s => indexCols.Select(c => s.Index(table, c.Column)).ToArray()).ToArray();

            return (s, r) =>
            {
                var sb = new StringBuilder();
                for (var i = 0; i < stringCols.Length; ++i)
                {
                    var c = strs[s][i];
                    sb.Append(c == null ? "" : sources[s].Str(c[r])).Append('\u001f');
                }
                for (var i = 0; i < dataCols.Length; ++i)
                {
                    var c = datas[s][i];
                    sb.Append(c == null ? "0" : Convert.ToString(c.GetValue(r), CultureInfo.InvariantCulture)).Append('\u001f');
                }
                for (var i = 0; i < indexCols.Count; ++i)
                {
                    var c = idxs[s][i];
                    var v = c == null ? -1 : c[r];
                    var o = v < 0 ? v : (plans.TryGetValue(indexCols[i].Related, out var rp) ? rp.Lookup(s, v) : -1);
                    sb.Append(o).Append('\u001f');
                }
                return sb.ToString();
            };
        }

        /// <summary>Removes the rows of a table which no written row of another table (except ignoredReferrer) points at.</summary>
        private static int PruneUnreferenced(string table, string ignoredReferrer, VimTables[] sources, Dictionary<string, TablePlan> plans)
        {
            if (!plans.TryGetValue(table, out var plan) || plan.Count == 0) return 0;
            var used = new bool[plan.Count];
            foreach (var referrer in plans.Values)
            {
                if (referrer.Name == table || referrer.Name == ignoredReferrer) continue;
                foreach (var (column, related) in sources.SelectMany(s => s.IndexRelations(referrer.Name)).Distinct())
                {
                    if (related != table) continue;
                    var cols = sources.Select(s => s.Index(referrer.Name, column)).ToArray();
                    for (var i = 0; i < referrer.Count; ++i)
                    {
                        var c = cols[referrer.Source[i]];
                        var o = plan.Lookup(referrer.Source[i], c == null ? -1 : c[referrer.Row[i]]);
                        if (o >= 0) used[o] = true;
                    }
                }
            }
            return plan.RemoveWhere(i => !used[i]);
        }

        /// <summary>The wall layers of a removed wall layer structure go with it.</summary>
        private static int PruneLayersOfRemovedStructures(VimTables[] sources, Dictionary<string, TablePlan> plans)
        {
            if (!plans.TryGetValue(TableNames.CompoundStructureLayer, out var layers) || !plans.TryGetValue(TableNames.CompoundStructure, out var structures))
                return 0;
            var column = sources.Select(s => s.Index(TableNames.CompoundStructureLayer, "index:" + TableNames.CompoundStructure + ":CompoundStructure")).ToArray();
            return layers.RemoveWhere(i =>
            {
                var c = column[layers.Source[i]];
                var v = c == null ? -1 : c[layers.Row[i]];
                return v >= 0 && structures.Lookup(layers.Source[i], v) < 0;
            });
        }
    }
}
