using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Vim.Format.ObjectModel;
using Vim.Util;

namespace Vim.Format.Merge
{
    /// <summary>How a BimDocument row of the partial file is recognised in the base file.</summary>
    public enum PatchDocumentMatch
    {
        /// <summary>Title + PathName + ordinal among the rows with the same Title and PathName (the default).</summary>
        TitleAndPath = 0,
        /// <summary>Title + ordinal: for a file whose path differs between the two exports (another local copy).</summary>
        Title = 1,
        /// <summary>
        /// Every document that is not a link (BimDocument.IsLinked = 0) is the host, whatever its title, path or version:
        /// one identity for all of them. Linked documents are matched by Title + ordinal. For "Sync to VIM": the same
        /// central model is worked on through several local files (V4-GOOD.rvt, V4-CENTRAL_sam.rvt...), each with its
        /// own Title and PathName, and a sync from any of them must replace the rows the others wrote, not add a second
        /// copy of every element next to them (measured 2026-10-12).
        /// </summary>
        HostAndLinkTitles = 2,
    }

    public class VimPatchOptions
    {
        /// <summary>
        /// Revit UniqueIds of the elements that changed (added or modified). Null = every element of the partial file
        /// counts as changed. Elements of the partial file not in this list are context (their types, levels, views...):
        /// the base keeps its own rows for them and the partial's rows only serve to re-point relations.
        /// </summary>
        public ICollection<string> ChangedUniqueIds { get; set; }

        /// <summary>Revit UniqueIds of the elements deleted since the base was written.</summary>
        public ICollection<string> DeletedUniqueIds { get; set; } = new HashSet<string>();

        /// <summary>
        /// Titles of BimDocuments (linked models) exported whole in the partial file: every base element of such a
        /// document is replaced by the partial's, and the ones the partial does not have are deleted.
        /// </summary>
        public ICollection<string> ReplacedDocumentTitles { get; set; } = new HashSet<string>();

        /// <summary>
        /// UniqueId of a partial element -> UniqueId of the base element it stands for. Used for the temporary 3D view
        /// of an incremental export: its rows (ElementInView, ShapeInView) land on the base's export view.
        /// </summary>
        public IDictionary<string, string> UniqueIdAliases { get; set; } = new Dictionary<string, string>();

        /// <summary>UniqueIds of partial elements never written (with every row they own).</summary>
        public ICollection<string> ExcludedUniqueIds { get; set; } = new HashSet<string>();

        /// <summary>
        /// The Shape kinds the partial export produces. A changed element's base shapes of these kinds are replaced by
        /// the partial's; its shapes of other kinds (by default the 2D view linework, Kind 1, and pre-5.9 shapes,
        /// Kind 0) are kept from the base, and the partial's shapes of those kinds are not written.
        /// </summary>
        public ICollection<int> ReplacedShapeKinds { get; set; } = new HashSet<int>
        {
            (int) ShapeKind.ModelCurve, (int) ShapeKind.SymbolicCurve, (int) ShapeKind.VoidEdge,
            (int) ShapeKind.LocationCurve, (int) ShapeKind.Grid,
        };

        /// <summary>
        /// UniqueIds of the views whose ElementInView rows the partial export produces. Null = the views the partial's
        /// own ElementInView rows point at (after aliases). A changed element keeps its base rows for other views (2D).
        /// </summary>
        public ICollection<string> ReplacedViewUniqueIds { get; set; }

        public PatchDocumentMatch DocumentMatch { get; set; } = PatchDocumentMatch.TitleAndPath;

        /// <summary>A partial mesh equal to one the base already has is not written twice.</summary>
        public bool DeduplicateMeshes { get; set; } = true;

        /// <summary>
        /// Writes ExportRevision + ElementInExportRevision (object model 5.10.0): which export of which document wrote each
        /// element's current data. A file without them gets one revision per document from its own header (the base as a
        /// full export, the partial as a partial export).
        /// </summary>
        public bool RecordExportRevisions { get; set; } = true;

        /// <summary>
        /// Keeps the activity log (object model 5.11.0: Contributor, SyncSession, ElementChange, ActivityMarker) of
        /// both files. False drops every activity row from the output - "strip activity on export", for a file that
        /// goes to a client, and the way to clear the log of a file.
        /// </summary>
        public bool KeepActivityLog { get; set; } = true;

        /// <summary>
        /// Drops the activity rows older than this (UTC, ISO 8601); null or empty keeps everything. The caller gives
        /// the cut-off rather than a number of days so that a patch of the same two files always gives the same file.
        /// </summary>
        public string ActivityOldestUtc { get; set; }

        /// <summary>Keeps at most this many element-change rows, the newest; 0 = no cap.</summary>
        public int ActivityMaxChangeRows { get; set; }

        public string Generator { get; set; } = "Vim.Format.Patch";
    }

    public class VimPatchResult
    {
        public int BaseElements, PartialElements, OutputElements;
        public int Kept, Changed, Added, Deleted, Context, Excluded;
        /// <summary>UniqueIds listed as changed which the partial file does not hold: their base rows are kept.</summary>
        public List<string> ChangedButNotInPartial = new List<string>();
        /// <summary>UniqueIds listed as deleted which the base file does not hold.</summary>
        public List<string> DeletedButNotInBase = new List<string>();
        /// <summary>Base Element row -> output Element row (-1 = deleted). Element rows never move except for deletions.</summary>
        public int[] ElementIndexMap;
        public Dictionary<string, (int Base, int Partial, int Output)> TableRows = new Dictionary<string, (int, int, int)>();
        public int PartialRowsWithoutElementDropped;
        /// <summary>Wall layer structures and cameras nothing points at any more, removed.</summary>
        public int OrphanRowsRemoved;
        /// <summary>Activity rows dropped by the retention caps.</summary>
        public int ActivityRowsTrimmed;
        public RowPlanWriteStats Geometry = new RowPlanWriteStats();
        public long LoadMs, PlanMs, BuildMs, WriteMs;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Elements: base {BaseElements}, partial {PartialElements}, output {OutputElements}");
            sb.AppendLine($"  kept {Kept}, changed {Changed}, added {Added}, deleted {Deleted}, context {Context}, excluded {Excluded}");
            if (ChangedButNotInPartial.Count > 0) sb.AppendLine($"  changed but not in the partial (base kept): {ChangedButNotInPartial.Count}");
            if (DeletedButNotInBase.Count > 0) sb.AppendLine($"  deleted but not in the base: {DeletedButNotInBase.Count}");
            sb.AppendLine($"Geometry: meshes from base {Geometry.MeshesFromSource0}, from partial {Geometry.MeshesFromOtherSources}, reused {Geometry.MeshesReused}; instances {Geometry.Instances}, shapes {Geometry.Shapes}, materials {Geometry.Materials}");
            sb.AppendLine($"Timings (ms): load {LoadMs}, plan {PlanMs}, build {BuildMs}, write {WriteMs}");
            foreach (var kv in TableRows.OrderBy(k => k.Key, StringComparer.Ordinal))
                sb.AppendLine($"  {kv.Key}: base {kv.Value.Base}, partial {kv.Value.Partial}, output {kv.Value.Output}");
            return sb.ToString();
        }
    }

    /// <summary>
    /// Patches a base VIM file with a partial VIM file exported from the same Revit document (only the changed
    /// elements, for example through a temporary 3D view): changed elements are replaced, new ones appended, deleted
    /// ones removed; every other row of the base (geometry, materials, parameters, shapes and their Kind, lights, 2D
    /// linework, assets) is kept, and every relation is re-pointed. See INCREMENTAL-EXPORT.md in Vim.Revit/Core.
    /// <para>
    /// Rules, per table:
    /// <list type="bullet">
    /// <item>Element: identity = BimDocument (title + path) + UniqueId. A changed element keeps its row index (its content
    /// comes from the partial); new elements are appended; deleted ones are removed (later rows move up).</item>
    /// <item>Tables owned by an element (an "Element" relation) with at most one row per element (FamilyInstance, Level,
    /// Room, View, Material...): the row follows its element, in place.</item>
    /// <item>Tables with many rows per element (Parameter, Node, Shape, ElementInView, MaterialInElement, ElementInSystem,
    /// LightSource...): a changed element's base rows are replaced by the partial's, within the replace scope (Shape kinds,
    /// views).</item>
    /// <item>Other tables (Category, DisplayUnit, ParameterDescriptor, Workset, Asset, associations): the base rows stay;
    /// a partial row equal to a base row (by key or by content) is the same row, otherwise it is appended. An association
    /// row (XInY) whose end is gone is removed.</item>
    /// <item>Geometry: Node rows = instances, Shape rows = shapes, Material rows = materials; meshes are the ones the
    /// written instances use (a base mesh no instance uses any more is dropped).</item>
    /// </list>
    /// </para>
    /// </summary>
    public static class VimPatchService
    {
        private enum Role { None = 0, Changed, Context, New, Excluded }

        private static readonly Regex AssociationTableName = new Regex(@"^Vim\.\w+In\w+$", RegexOptions.Compiled);

        /// <summary>Non-element tables whose rows belong to the row their first relation points at.</summary>
        private static readonly HashSet<string> OwnerChainedTables = new HashSet<string>
        {
            TableNames.ScheduleColumn, TableNames.ScheduleCell, TableNames.CompoundStructureLayer,
        };

        /// <summary>
        /// The activity log tables (object model 5.11.0), in the order they are planned. They are history: a partial
        /// export never replaces what the base already recorded, whichever elements changed. Contributor and
        /// SyncSession rows are merged on the identity below (the partial's content wins: it is the fresher reading of
        /// the same person or the same still-open session); ElementChange and ActivityMarker rows are appended, the
        /// ones about elements that no longer exist included.
        /// </summary>
        private static readonly string[] ActivityTables =
        {
            TableNames.Contributor, TableNames.SyncSession, TableNames.ElementChange, TableNames.ActivityMarker,
        };

        /// <summary>What makes two activity rows the same row. A table absent from this map is append-only.</summary>
        private static readonly Dictionary<string, string[]> ActivityIdentityColumns = new Dictionary<string, string[]>
        {
            { TableNames.Contributor, new[] { "string:Name", "int:Source" } },
            { TableNames.SyncSession, new[] { "string:SessionGuid" } },
        };

        /// <summary>Element-owned tables which can hold several rows per element, whatever a given file holds.</summary>
        private static readonly HashSet<string> ManyRowsPerElementTables = new HashSet<string>
        {
            TableNames.Node, TableNames.Shape, TableNames.Parameter, TableNames.ElementInView, TableNames.MaterialInElement,
            TableNames.ElementInSystem, TableNames.ElementInWarning, TableNames.LightSource, TableNames.ShapeCollection,
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

            var baseMajor = baseVim.Header?.Schema?.Major;
            var partialMajor = partialVim.Header?.Schema?.Major;
            if (baseMajor != null && partialMajor != null && baseMajor != partialMajor)
                throw new HResultException((int)ErrorCode.VimMergeObjectModelMajorVersionMismatch,
                    $"Object model major version mismatch: base {baseVim.Header.Schema}, partial {partialVim.Header.Schema}");

            // Provenance: every element keeps (or takes, when changed) the export revision its data comes from.
            if (options.RecordExportRevisions)
            {
                baseVim.EnsureExportRevisions(ExportRevisionKind.FullExport);
                partialVim.EnsureExportRevisions(ExportRevisionKind.PartialExport);
            }

            var sources = new[] { baseVim, partialVim };
            var activityTables = new HashSet<string>(options.KeepActivityLog ? ActivityTables : Array.Empty<string>());
            var tableNames = baseVim.Tables.Keys.Concat(partialVim.Tables.Keys)
                .Where(n => !VimConstants.ComputedTableNames.Contains(n))
                .Where(n => options.KeepActivityLog || !ActivityTables.Contains(n)) // "strip activity on export"
                .Distinct()
                .OrderBy(n => n, StringComparer.Ordinal)
                .ToArray();
            var plans = tableNames.ToDictionary(n => n, n => new TablePlan(n, sources));

            // ---------------------------------------------------------------------------------------------
            // 1. Elements
            // ---------------------------------------------------------------------------------------------
            var aliases = options.UniqueIdAliases ?? new Dictionary<string, string>();
            Func<string, string> alias = u => u != null && aliases.TryGetValue(u, out var a) ? a : u;
            var baseKeys = ElementKeys(baseVim, null, options.DocumentMatch);
            var partialKeys = ElementKeys(partialVim, alias, options.DocumentMatch);
            var baseUids = baseVim.Strs(TableNames.Element, "string:UniqueId") ?? new string[baseKeys.Length];
            var partialUids = partialVim.Strs(TableNames.Element, "string:UniqueId") ?? new string[partialKeys.Length];
            var baseDocTitles = baseVim.ElementDocumentTitles();
            var partialDocTitles = partialVim.ElementDocumentTitles();

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
            var elementPlan = plans[TableNames.Element];
            var seenDeleted = new HashSet<string>();
            var seenChanged = new HashSet<string>();

            for (var b = 0; b < baseKeys.Length; ++b)
            {
                var uid = baseUids[b] ?? "";
                var inReplacedDoc = replacedDocs.Contains(baseDocTitles[b]);
                var hasPartial = partialByKey.TryGetValue(baseKeys[b], out var p);
                var deleted = deletedSet.Contains(uid) || (inReplacedDoc && !hasPartial);
                if (deleted)
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
                var isAliasTarget = aliasTargets.Contains(uid) && aliases.ContainsKey(partialUids[p] ?? "") ;
                var changed = !isAliasTarget && !excludedSet.Contains(partialUids[p] ?? "")
                    && (changedSet == null || changedSet.Contains(uid) || inReplacedDoc);
                if (changed)
                {
                    seenChanged.Add(uid);
                    baseChanged[b] = true;
                    partialRole[p] = Role.Changed;
                    var o = elementPlan.Add(1, p);
                    elementPlan.Alias(0, b, o);
                    result.Changed++;
                }
                else
                {
                    partialRole[p] = Role.Context;
                    var o = elementPlan.Add(0, b);
                    elementPlan.Alias(1, p, o);
                    result.Context++;
                    result.Kept++;
                }
            }
            for (var p = 0; p < partialKeys.Length; ++p)
            {
                if (partialRole[p] != Role.None) continue;
                var uid = partialUids[p] ?? "";
                if (excludedSet.Contains(uid) || aliases.ContainsKey(uid))
                {
                    // An alias without its target in the base, or an excluded element: not written.
                    partialRole[p] = Role.Excluded;
                    result.Excluded++;
                    continue;
                }
                if (deletedSet.Contains(uid))
                {
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

            // ---------------------------------------------------------------------------------------------
            // 2. Element-owned tables (ElementInView last: its scope needs the View rows).
            // ---------------------------------------------------------------------------------------------
            var elementOwned = tableNames.Where(n => baseVim.IsElementOwned(n) || partialVim.IsElementOwned(n)).ToList();
            var ordered = elementOwned.Where(n => n != TableNames.ElementInView).ToList();
            if (elementOwned.Contains(TableNames.ElementInView)) ordered.Add(TableNames.ElementInView);

            var shapeKinds = new HashSet<int>(options.ReplacedShapeKinds ?? Array.Empty<int>());
            HashSet<int> scopeViews = null;

            foreach (var table in ordered)
            {
                var plan = plans[table];
                var baseOwner = baseVim.Index(table, VimTables.ElementColumn) ?? new int[baseVim.NumRows(table)];
                var partialOwner = partialVim.Index(table, VimTables.ElementColumn) ?? new int[partialVim.NumRows(table)];

                Func<int, int, bool> inScope = (s, r) => true;
                if (table == TableNames.Shape)
                {
                    var kb = baseVim.Data<int>(table, "int:Kind");
                    var kp = partialVim.Data<int>(table, "int:Kind");
                    inScope = (s, r) =>
                    {
                        var k = s == 0 ? kb : kp;
                        return shapeKinds.Contains(k == null ? 0 : k[r]);
                    };
                }
                else if (table == TableNames.ElementInView)
                {
                    var viewPlan = plans.TryGetValue(TableNames.View, out var vp) ? vp : null;
                    var vb = baseVim.Index(table, "index:" + TableNames.View + ":View");
                    var vpCol = partialVim.Index(table, "index:" + TableNames.View + ":View");
                    scopeViews = ComputeScopeViews(options, baseVim, partialVim, plans, partialOwner, partialRole, vpCol);
                    inScope = (s, r) =>
                    {
                        var col = s == 0 ? vb : vpCol;
                        var v = col == null ? -1 : col[r];
                        var outView = viewPlan?.Lookup(s, v) ?? -1;
                        return outView >= 0 && scopeViews.Contains(outView);
                    };
                }

                // At most one row per element in both files -> rows follow their element in place.
                var keyed = !ManyRowsPerElementTables.Contains(table) && IsOneRowPerElement(baseOwner) && IsOneRowPerElement(partialOwner);
                if (keyed)
                {
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
                        if (partialRowByOutElement.TryGetValue(oe, out var pr))
                        {
                            if (baseChanged[e])
                            {
                                var o = plan.Add(1, pr);
                                plan.Alias(0, r, o);
                            }
                            else
                            {
                                var o = plan.Add(0, r);
                                plan.Alias(1, pr, o);
                            }
                        }
                        else
                        {
                            plan.Add(0, r);
                        }
                    }
                    for (var r = 0; r < partialOwner.Length; ++r)
                    {
                        if (plan.Map[1][r] >= 0) continue;
                        var e = partialOwner[r];
                        if (e < 0) { plan.Add(1, r); continue; }
                        var role = partialRole[e];
                        if (role == Role.Excluded || role == Role.None) continue;
                        if (elementPlan.Lookup(1, e) < 0) continue;
                        plan.Add(1, r);
                    }
                }
                else
                {
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
                        if (e < 0) { result.PartialRowsWithoutElementDropped++; continue; }
                        var role = partialRole[e];
                        if (role == Role.New || (role == Role.Changed && inScope(1, r)))
                        {
                            if (table == TableNames.ElementInView && !inScope(1, r) && role == Role.New)
                            {
                                // A new element's row for a view that is not written (excluded, no alias).
                                var vcol = partialVim.Index(table, "index:" + TableNames.View + ":View");
                                var v = vcol == null ? -1 : vcol[r];
                                if ((plans.TryGetValue(TableNames.View, out var vp2) ? vp2.Lookup(1, v) : -1) < 0) continue;
                            }
                            plan.Add(1, r);
                        }
                    }
                }
            }

            // ---------------------------------------------------------------------------------------------
            // 3. Other tables, referees before referrers.
            // ---------------------------------------------------------------------------------------------
            var others = tableNames.Where(n => n != TableNames.Element && !elementOwned.Contains(n) && !activityTables.Contains(n)).ToList();
            var (order, cyclic) = TopologicalOrder(others, sources);
            foreach (var table in order)
                PlanDictionaryTable(table, sources, plans, cyclic.Contains(table));

            // ---------------------------------------------------------------------------------------------
            // 3b. The activity log, before the pruning below: an ExportRevision the history still points at stays.
            // ---------------------------------------------------------------------------------------------
            foreach (var table in ActivityTables)
                if (activityTables.Contains(table) && plans.ContainsKey(table))
                    PlanActivityTable(table, sources, plans);
            result.ActivityRowsTrimmed += TrimActivity(options, sources, plans);

            // Rows nothing points at any more (a replaced type's wall layers, a camera of an excluded view): removed, as
            // the format's validation requires every CompoundStructure to belong to a FamilyType.
            result.OrphanRowsRemoved += PruneUnreferenced(TableNames.CompoundStructure, TableNames.CompoundStructureLayer, sources, plans);
            if (plans.TryGetValue(TableNames.CompoundStructureLayer, out var layerPlan) && plans.TryGetValue(TableNames.CompoundStructure, out var csPlan))
            {
                var csCol = sources.Select(s => s.Index(TableNames.CompoundStructureLayer, "index:" + TableNames.CompoundStructure + ":CompoundStructure")).ToArray();
                result.OrphanRowsRemoved += layerPlan.RemoveWhere(i =>
                {
                    var c = csCol[layerPlan.Source[i]];
                    var v = c == null ? -1 : c[layerPlan.Row[i]];
                    return v >= 0 && csPlan.Lookup(layerPlan.Source[i], v) < 0;
                });
            }
            result.OrphanRowsRemoved += PruneUnreferenced(TableNames.Camera, null, sources, plans);
            // A session and a person nothing in the history mentions any more go with the rows that mentioned them.
            result.OrphanRowsRemoved += PruneUnreferenced(TableNames.SyncSession, null, sources, plans);
            result.OrphanRowsRemoved += PruneUnreferenced(TableNames.Contributor, null, sources, plans);
            result.OrphanRowsRemoved += PruneUnreferenced(TableNames.ExportRevision, null, sources, plans);

            result.PlanMs = sw.ElapsedMilliseconds;
            sw.Restart();

            // ---------------------------------------------------------------------------------------------
            // 4. Write.
            // ---------------------------------------------------------------------------------------------
            var schema = baseVim.Header?.Schema;
            if (schema == null || (partialVim.Header?.Schema != null && partialVim.Header.Schema.Minor > schema.Minor && partialVim.Header.Schema.Major == schema.Major))
                schema = partialVim.Header?.Schema ?? SchemaVersion.Current;
            var header = baseVim.Header != null
                ? SerializableHeader.CreateRevisionOf(baseVim.Header, options.Generator, schema, new Dictionary<string, string>
                {
                    { "patched", DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture) },
                })
                : new SerializableHeader(options.Generator, schema, "");

            var db = RowPlanWriter.Write(sources, plans, header, options.DeduplicateMeshes, result.Geometry);
            foreach (var kv in plans)
                result.TableRows[kv.Key] = (baseVim.NumRows(kv.Key), partialVim.NumRows(kv.Key), kv.Value.Count);
            result.BuildMs = sw.ElapsedMilliseconds;
            return (db, result);
        }

        internal static string[] ElementKeys(VimTables t, Func<string, string> alias, PatchDocumentMatch match)
        {
            var keys = t.ElementKeys(alias);
            if (match == PatchDocumentMatch.TitleAndPath)
                return keys;

            // Title only: rebuild the document part of the key without the path.
            var n = t.NumRows(TableNames.BimDocument);
            var titles = t.Strs(TableNames.BimDocument, "string:Title") ?? new string[n];
            var seen = new Dictionary<string, int>();
            var docKeys = new string[n];
            var hostKeys = match == PatchDocumentMatch.HostAndLinkTitles ? t.HostBimDocuments() : null;
            for (var i = 0; i < n; ++i)
            {
                if (hostKeys != null && hostKeys[i])
                {
                    docKeys[i] = "\u0001host";
                    continue;
                }
                seen.TryGetValue(titles[i] ?? "", out var ordinal);
                seen[titles[i] ?? ""] = ordinal + 1;
                docKeys[i] = (titles[i] ?? "") + "|" + ordinal;
            }
            var docs = t.Index(TableNames.Element, "index:" + TableNames.BimDocument + ":BimDocument");
            var uids = t.Strs(TableNames.Element, "string:UniqueId") ?? new string[keys.Length];
            var dup = new Dictionary<string, int>();
            for (var i = 0; i < keys.Length; ++i)
            {
                var d = docs != null && docs[i] >= 0 && docs[i] < n ? docKeys[docs[i]] : "";
                var u = uids[i] ?? "";
                if (alias != null) u = alias(u) ?? u;
                var k = d + "\u001f" + u;
                if (dup.TryGetValue(k, out var c)) { dup[k] = c + 1; k = k + "#" + c; } else dup[k] = 1;
                keys[i] = k;
            }
            return keys;
        }

        private static HashSet<int> ComputeScopeViews(VimPatchOptions options, VimTables baseVim, VimTables partialVim,
            Dictionary<string, TablePlan> plans, int[] partialOwner, Role[] partialRole, int[] partialViewCol)
        {
            var scope = new HashSet<int>();
            if (!plans.TryGetValue(TableNames.View, out var viewPlan))
                return scope;
            if (options.ReplacedViewUniqueIds != null)
            {
                // Out View row -> its element's UniqueId.
                var wanted = new HashSet<string>(options.ReplacedViewUniqueIds);
                for (var i = 0; i < viewPlan.Count; ++i)
                {
                    var src = viewPlan.Source[i] == 0 ? baseVim : partialVim;
                    var owner = src.Index(TableNames.View, VimTables.ElementColumn);
                    var e = owner == null ? -1 : owner[viewPlan.Row[i]];
                    var uids = src.Strs(TableNames.Element, "string:UniqueId");
                    if (e >= 0 && uids != null && wanted.Contains(uids[e])) scope.Add(i);
                }
                return scope;
            }
            if (partialViewCol == null)
                return scope;
            for (var r = 0; r < partialOwner.Length; ++r)
            {
                var e = partialOwner[r];
                if (e < 0 || (partialRole[e] != Role.Changed && partialRole[e] != Role.New)) continue;
                var ov = viewPlan.Lookup(1, partialViewCol[r]);
                if (ov >= 0) scope.Add(ov);
            }
            return scope;
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
                        var v = c == null ? -1 : c[referrer.Row[i]];
                        var o = plan.Lookup(referrer.Source[i], v);
                        if (o >= 0) used[o] = true;
                    }
                }
            }
            return plan.RemoveWhere(i => !used[i]);
        }

        /// <summary>
        /// An activity log table: the base's rows all stay (history is never rewritten), the partial's are appended,
        /// and a partial row which is the same Contributor or the same SyncSession as a base row takes that row's
        /// place instead of being added next to it - with the partial's content, which is the fresher reading.
        /// </summary>
        private static void PlanActivityTable(string table, VimTables[] sources, Dictionary<string, TablePlan> plans)
        {
            var plan = plans[table];
            var nb = sources[0].NumRows(table);
            var np = sources[1].NumRows(table);

            if (!ActivityIdentityColumns.TryGetValue(table, out var identityColumns))
            {
                // Append-only: one row per change, per marker. Two identical rows are two facts, not one.
                for (var r = 0; r < nb; ++r) plan.Add(0, r);
                for (var r = 0; r < np; ++r) plan.Add(1, r);
                return;
            }

            var key = ActivityKeyFunction(table, sources, identityColumns);
            var firstPartialOfKey = new Dictionary<string, int>();
            for (var r = np - 1; r >= 0; --r)
                firstPartialOfKey[key(1, r)] = r;

            for (var b = 0; b < nb; ++b)
            {
                var k = key(0, b);
                if (firstPartialOfKey.TryGetValue(k, out var p) && plan.Map[1][p] < 0)
                {
                    var o = plan.Add(1, p);
                    plan.Alias(0, b, o);
                }
                else
                {
                    plan.Add(0, b);
                }
            }

            for (var r = 0; r < np; ++r)
            {
                if (plan.Map[1][r] >= 0) continue;
                var first = firstPartialOfKey[key(1, r)];
                if (first != r && plan.Map[1][first] >= 0)
                    plan.Alias(1, r, plan.Map[1][first]); // the partial repeats itself
                else
                    plan.Add(1, r);
            }
        }

        /// <summary>
        /// Applies the retention caps to the planned activity rows: what is older than the cut-off goes, and past the
        /// row cap the oldest changes go. Returns how many rows were dropped.
        /// </summary>
        private static int TrimActivity(VimPatchOptions options, VimTables[] sources, Dictionary<string, TablePlan> plans)
        {
            var hasCutoff = !string.IsNullOrEmpty(options.ActivityOldestUtc);
            if (!hasCutoff && options.ActivityMaxChangeRows <= 0)
                return 0;

            var dropped = 0;
            dropped += TrimActivityTable(TableNames.ElementChange, "string:ChangedUtc", options, sources, plans);
            dropped += TrimActivityTable(TableNames.ActivityMarker, "string:OccurredUtc", options, sources, plans);
            return dropped;
        }

        private static int TrimActivityTable(string table, string timeColumn, VimPatchOptions options,
            VimTables[] sources, Dictionary<string, TablePlan> plans)
        {
            if (!plans.TryGetValue(table, out var plan) || plan.Count == 0)
                return 0;
            var times = sources.Select(s => s.Strs(table, timeColumn)).ToArray();
            string TimeOf(int i)
            {
                var c = times[plan.Source[i]];
                return c == null || plan.Row[i] >= c.Length ? "" : c[plan.Row[i]] ?? "";
            }

            var remove = new bool[plan.Count];
            var kept = plan.Count;
            if (!string.IsNullOrEmpty(options.ActivityOldestUtc))
            {
                for (var i = 0; i < plan.Count; ++i)
                {
                    var t = TimeOf(i);
                    // An unreadable or empty time is kept: dropping a row because we cannot read its date would be
                    // the one way to lose history silently.
                    if (t.Length > 0 && string.CompareOrdinal(t, options.ActivityOldestUtc) < 0)
                    {
                        remove[i] = true;
                        kept--;
                    }
                }
            }

            // The row cap applies to the changes only; the markers are few and each one says something.
            if (table == TableNames.ElementChange && options.ActivityMaxChangeRows > 0 && kept > options.ActivityMaxChangeRows)
            {
                var oldestFirst = Enumerable.Range(0, plan.Count).Where(i => !remove[i])
                    .OrderBy(TimeOf, StringComparer.Ordinal).Take(kept - options.ActivityMaxChangeRows);
                foreach (var i in oldestFirst)
                    remove[i] = true;
            }

            return plan.RemoveWhere(i => remove[i]);
        }

        /// <summary>The identity of an activity row, as the text of the given string and int columns.</summary>
        private static Func<int, int, string> ActivityKeyFunction(string table, VimTables[] sources, string[] columns)
        {
            var values = new string[sources.Length][][];
            for (var s = 0; s < sources.Length; ++s)
            {
                values[s] = new string[columns.Length][];
                for (var c = 0; c < columns.Length; ++c)
                {
                    if (columns[c].StartsWith(VimConstants.StringColumnNameTypePrefix))
                    {
                        values[s][c] = sources[s].Strs(table, columns[c]);
                    }
                    else
                    {
                        var d = sources[s].Data<int>(table, columns[c]);
                        values[s][c] = d?.Select(x => x.ToString(CultureInfo.InvariantCulture)).ToArray();
                    }
                }
            }
            return (s, r) =>
            {
                var sb = new StringBuilder();
                foreach (var column in values[s])
                    sb.Append(column == null || r >= column.Length ? "" : column[r] ?? "").Append('\u001f');
                return sb.ToString();
            };
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
                    if (done.Contains(t)) continue;
                    if (deps[t].All(done.Contains))
                    {
                        order.Add(t);
                        done.Add(t);
                        progress = true;
                    }
                }
            } while (progress);
            var cyclic = new HashSet<string>(tables.Where(t => !done.Contains(t)));
            order.AddRange(tables.Where(cyclic.Contains));
            return (order, cyclic);
        }

        private static bool IsAssociation(string table)
            => AssociationTableName.IsMatch(table) || OwnerChainedTables.Contains(table);

        /// <summary>Base rows stay (associations lose rows whose ends are gone); partial rows dedupe by key/content.</summary>
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
                    if (c == null) continue;
                    var v = c[r];
                    if (v < 0) continue;
                    if (related == table) continue;
                    if (!plans.TryGetValue(related, out var rp) || rp.Lookup(s, v) < 0) return false;
                }
                return true;
            }

            var nb = sources[0].NumRows(table);
            for (var r = 0; r < nb; ++r)
                if (EndsAlive(0, r))
                    plan.Add(0, r);

            var np = sources[1].NumRows(table);
            if (np == 0) return;

            if (cyclic)
            {
                for (var r = 0; r < np; ++r)
                    if (EndsAlive(1, r))
                        plan.Add(1, r);
                return;
            }

            var keyOf = KeyFunction(table, sources, plans, relations);
            Dictionary<string, int> baseIndex = null;
            var partialIndex = new Dictionary<string, int>();
            for (var r = 0; r < np; ++r)
            {
                if (!EndsAlive(1, r)) continue;
                var k = keyOf(1, r);
                if (baseIndex == null)
                {
                    baseIndex = new Dictionary<string, int>();
                    for (var i = 0; i < plan.Count; ++i)
                    {
                        var bk = keyOf(0, plan.Row[i]);
                        if (!baseIndex.ContainsKey(bk)) baseIndex[bk] = i;
                    }
                }
                if (baseIndex.TryGetValue(k, out var o) || partialIndex.TryGetValue(k, out o))
                {
                    plan.Alias(1, r, o);
                    continue;
                }
                partialIndex[k] = plan.Add(1, r);
            }
        }

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

            var stringCols = keyStringColumns ?? sources.SelectMany(s => s.GetTable(table)?.StringColumns.Select(c => c.Name) ?? Enumerable.Empty<string>())
                .Distinct().OrderBy(x => x, StringComparer.Ordinal).ToArray();
            var dataCols = keyStringColumns != null ? Array.Empty<string>() : sources.SelectMany(s => s.GetTable(table)?.DataColumns.Select(c => c.Name) ?? Enumerable.Empty<string>())
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
    }
}
