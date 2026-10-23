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

        /// <summary>
        /// Linked model title in the partial file -> its title in the base file, for a linked model renamed between the
        /// two exports (the caller knows it is the same model by its stable identity: CreationGUID, plus the cloud model
        /// guid). Used by <see cref="PatchDocumentMatch.Title"/> and <see cref="PatchDocumentMatch.HostAndLinkTitles"/>.
        /// </summary>
        public IDictionary<string, string> DocumentTitleAliases { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// The partial file holds every warning Revit lists for the host model (the rows naming no element of the
        /// partial included): the host's Warning rows are replaced by the partial's, matched by Warning.Key, and a base
        /// ElementInWarning row follows its warning to the partial's row of the same key. A base warning Revit no longer
        /// lists goes, and so does every warning no element of the file names any more (a full export writes none).
        /// The linked models of <see cref="ReplacedDocumentTitles"/> are treated the same way. False = the old rule (a
        /// partial warning is added unless an identical row is in the base; nothing is ever removed).
        /// </summary>
        public bool ReplaceHostWarnings { get; set; }

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

        /// <summary>Header values written in the patched file (added to, or replacing, the base header's), for example
        /// the export fingerprint a later differential update checks. Null for none.</summary>
        public IReadOnlyDictionary<string, string> HeaderValues { get; set; }

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
        /// <summary>Document parameter holders (and site / building holders) of the partial matched to the base's, whatever their name-made UniqueId.</summary>
        public int HoldersMatched;
        /// <summary>Warnings of the base dropped because Revit no longer lists them or no element names them any more.</summary>
        public int WarningsRemoved;
        public RowPlanWriteStats Geometry = new RowPlanWriteStats();
        public long LoadMs, PlanMs, BuildMs, WriteMs;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Elements: base {BaseElements}, partial {PartialElements}, output {OutputElements}");
            sb.AppendLine($"  kept {Kept}, changed {Changed}, added {Added}, deleted {Deleted}, context {Context}, excluded {Excluded}");
            if (ChangedButNotInPartial.Count > 0) sb.AppendLine($"  changed but not in the partial (base kept): {ChangedButNotInPartial.Count}");
            if (DeletedButNotInBase.Count > 0) sb.AppendLine($"  deleted but not in the base: {DeletedButNotInBase.Count}");
            if (HoldersMatched > 0) sb.AppendLine($"  document / site / building holders matched by document: {HoldersMatched}");
            if (WarningsRemoved > 0) sb.AppendLine($"  warnings removed (no longer listed by Revit, or naming no element): {WarningsRemoved}");
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
            var partialKeys = ElementKeys(partialVim, alias, options.DocumentMatch, options.DocumentTitleAliases);
            var forceChanged = new bool[baseKeys.Length];
            if (options.DocumentMatch != PatchDocumentMatch.TitleAndPath)
                result.HoldersMatched = MatchHolders(baseVim, partialVim, baseKeys, partialKeys, options, forceChanged);
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
                    && (changedSet == null || changedSet.Contains(uid) || inReplacedDoc || forceChanged[b]);
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
                    // The element an alias stands for, held by the partial as well (the export view, next to the temporary
                    // view aliased onto it): the same element, as context.
                    partialRole[p] = Role.Context;
                    elementPlan.Alias(1, p, twin);
                    result.Context++;
                    continue;
                }
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

            var warningScope = options.ReplaceHostWarnings ? WarningScope(sources, options, replacedDocs) : null;
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
                    // Output element -> its row of this table so far: two partial elements standing for one output element
                    // (the export view and the temporary view aliased onto it) share that row.
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
                        var role = partialRole[e];
                        if (role == Role.Excluded || role == Role.None) continue;
                        var oe = elementPlan.Lookup(1, e);
                        if (oe < 0) continue;
                        if (rowOfOutElement.TryGetValue(oe, out var existing)) { plan.Alias(1, r, existing); continue; }
                        rowOfOutElement[oe] = plan.Add(1, r);
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
                        // With the whole warning list in the partial, a context element's ElementInWarning rows are the
                        // ones Revit lists now (a new warning naming only untouched elements); duplicates of the base's
                        // rows are merged by ReconcileElementsInWarning.
                        var warningContext = table == TableNames.ElementInWarning && role == Role.Context && warningScope != null;
                        if (role == Role.New || (role == Role.Changed && inScope(1, r)) || warningContext)
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
            {
                if (table == TableNames.Warning && warningScope != null)
                    result.WarningsRemoved += PlanWarnings(sources, plans, warningScope);
                else
                    PlanDictionaryTable(table, sources, plans, cyclic.Contains(table));
            }
            if (warningScope != null)
                result.WarningsRemoved += ReconcileElementsInWarning(sources, plans, warningScope);

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
            var headerValues = new Dictionary<string, string>
            {
                { "patched", DateTime.UtcNow.ToString("o", CultureInfo.InvariantCulture) },
            };
            foreach (var kv in options.HeaderValues ?? new Dictionary<string, string>())
                headerValues[kv.Key] = kv.Value;
            var header = baseVim.Header != null
                ? SerializableHeader.CreateRevisionOf(baseVim.Header, options.Generator, schema, headerValues)
                : new SerializableHeader(options.Generator, schema, "", headerValues);

            var db = RowPlanWriter.Write(sources, plans, header, options.DeduplicateMeshes, result.Geometry);
            foreach (var kv in plans)
                result.TableRows[kv.Key] = (baseVim.NumRows(kv.Key), partialVim.NumRows(kv.Key), kv.Value.Count);
            result.BuildMs = sw.ElapsedMilliseconds;
            return (db, result);
        }

        /// <summary>
        /// The exporter's own rows that stand for something Revit has no element for and that it writes ONCE per export
        /// whatever the document (StoreSyntheticSystemFamily keys them by name alone: "FAMILY:Basic Wall"): system families,
        /// system family types, unassigned systems. Their row is filed under the first document that needed it - the host in
        /// a full export, a linked model in a partial export that does not re-export the host's walls. Their UniqueId
        /// ("Basic Wall_System Family") is their only key, and it is stable; the document they are filed under is not.
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

        /// <summary>The document part of the element keys, per BimDocument row (Title / HostAndLinkTitles modes).</summary>
        internal static string[] DocumentKeys(VimTables t, PatchDocumentMatch match, IDictionary<string, string> titleAliases = null)
        {
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
                var title = titles[i] ?? "";
                if (titleAliases != null && titleAliases.TryGetValue(title, out var baseTitle) && baseTitle != null)
                    title = baseTitle;
                seen.TryGetValue(title, out var ordinal);
                seen[title] = ordinal + 1;
                docKeys[i] = title + "|" + ordinal;
            }
            return docKeys;
        }

        internal static string[] ElementKeys(VimTables t, Func<string, string> alias, PatchDocumentMatch match, IDictionary<string, string> titleAliases = null)
        {
            if (match == PatchDocumentMatch.TitleAndPath)
                return t.ElementKeys(alias);

            // Title only: rebuild the document part of the key without the path.
            var n = t.NumRows(TableNames.BimDocument);
            var docKeys = DocumentKeys(t, match, titleAliases);
            var count = t.NumRows(TableNames.Element);
            var keys = new string[count];
            var docs = t.Index(TableNames.Element, "index:" + TableNames.BimDocument + ":BimDocument");
            var uids = t.Strs(TableNames.Element, "string:UniqueId") ?? new string[count];
            var ids = t.Data<long>(TableNames.Element, "long:Id");
            var types = t.Strs(TableNames.Element, "string:Type");
            var dup = new Dictionary<string, int>();
            // Two passes: a row given another element's UniqueId by an alias (the temporary view of an incremental export,
            // standing for the export view) takes the plain key; the element itself, when the partial holds it too (an
            // incremental export writes every view), takes the "#1". The other way round, the temporary view was the
            // duplicate and was dropped, and with it every row an incremental export files under the export view
            // (ElementInView of a new element, ShapeInView of every re-exported shape - Diff series 2026-10-13).
            for (var pass = 0; pass < 2; ++pass)
            {
                for (var i = 0; i < count; ++i)
                {
                    var raw = uids[i] ?? "";
                    var u = alias == null ? raw : alias(raw) ?? raw;
                    var aliased = !string.Equals(u, raw, StringComparison.Ordinal);
                    if ((pass == 0) != aliased) continue;
                    var d = docs != null && docs[i] >= 0 && docs[i] < n ? docKeys[docs[i]] : "";
                    if (ids != null && types != null && ids[i] == VimConstants.SyntheticElementId && SharedSyntheticTypes.Contains(types[i] ?? ""))
                        d = "\u0002shared";
                    var k = d + "\u001f" + u;
                    if (dup.TryGetValue(k, out var c)) { dup[k] = c + 1; k = k + "#" + c; } else dup[k] = 1;
                    keys[i] = k;
                }
            }
            return keys;
        }

        /// <summary>
        /// The rows the exporter writes for a whole document - its parameter holder (the Element of BimDocument, which
        /// carries Project Information) and its site and building holders - take a UniqueId made from the project name
        /// ("[Project Name]_BimDocument Parameter Holder"). Renaming Project Information therefore gave the partial a
        /// holder no base row matched: it was appended with a second BimDocument row, and every re-exported element went
        /// to that second document (Diff series version 5). The project name is data, not identity: a partial holder is
        /// the base holder of the same type in the same document (same host, or same linked model), whatever its name,
        /// and it is always written as changed - so the BimDocument row and the holder's cells (name, number, client,
        /// saves...) are updated in place, and the row keeps its index.
        /// A site or building holder with no counterpart in the same document but the same UniqueId elsewhere in the base
        /// (two models with the same project name share one: the exporter keys them by name) is that row.
        /// Returns how many holders were matched; rewrites partialKeys and marks forceChanged (base rows).
        /// </summary>
        private static int MatchHolders(VimTables baseVim, VimTables partialVim, string[] baseKeys, string[] partialKeys,
            VimPatchOptions options, bool[] forceChanged)
        {
            var bIds = baseVim.Data<long>(TableNames.Element, "long:Id");
            var pIds = partialVim.Data<long>(TableNames.Element, "long:Id");
            var bTypes = baseVim.Strs(TableNames.Element, "string:Type");
            var pTypes = partialVim.Strs(TableNames.Element, "string:Type");
            var bUids = baseVim.Strs(TableNames.Element, "string:UniqueId");
            var pUids = partialVim.Strs(TableNames.Element, "string:UniqueId");
            var bDocs = baseVim.Index(TableNames.Element, "index:" + TableNames.BimDocument + ":BimDocument");
            var pDocs = partialVim.Index(TableNames.Element, "index:" + TableNames.BimDocument + ":BimDocument");
            if (bIds == null || pIds == null || bTypes == null || pTypes == null || bDocs == null || pDocs == null)
                return 0;
            var bDocKeys = DocumentKeys(baseVim, options.DocumentMatch);
            var pDocKeys = DocumentKeys(partialVim, options.DocumentMatch, options.DocumentTitleAliases);
            string DocOf(string[] dk, int[] docs, int e) => docs[e] >= 0 && docs[e] < dk.Length ? dk[docs[e]] : "";
            bool IsHolder(long[] ids, string[] types, int e) => ids[e] == VimConstants.SyntheticElementId && HolderTypes.Contains(types[e] ?? "");

            var baseByKey = new Dictionary<string, int>();
            for (var b = 0; b < baseKeys.Length; ++b) baseByKey[baseKeys[b]] = b;

            // Base holders by (document, type), in row order; the ones a partial holder already matches by key are taken.
            var claimed = new HashSet<int>();
            var exact = new List<(int P, int B)>();
            var pending = new List<int>();
            for (var p = 0; p < partialKeys.Length; ++p)
            {
                if (!IsHolder(pIds, pTypes, p)) continue;
                if (baseByKey.TryGetValue(partialKeys[p], out var b)) { claimed.Add(b); exact.Add((p, b)); }
                else pending.Add(p);
            }
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

            var matched = 0;
            foreach (var (_, b) in exact)
            {
                forceChanged[b] = true;
                matched++;
            }
            foreach (var p in pending)
            {
                var k = DocOf(pDocKeys, pDocs, p) + "\u001f" + pTypes[p];
                var b = -1;
                if (baseHolders.TryGetValue(k, out var l))
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
                // Site / building holder shared with another document of the same project name: the base row is it.
                if (pTypes[p] != VimConstants.BimDocumentParameterHolderElementType
                    && baseHoldersByUid.TryGetValue(pUids[p] ?? "", out var shared) && !claimed.Contains(shared))
                {
                    partialKeys[p] = baseKeys[shared];
                    matched++;
                }
            }
            return matched;
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

        /// <summary>
        /// Per source, the document key of each Warning row whose warnings the partial replaces (the host's, and those
        /// of the linked models re-exported whole), null for the others. Null when a file has no Warning.Key (written
        /// before the key existed): the old rule applies then.
        /// </summary>
        private static string[][] WarningScope(VimTables[] sources, VimPatchOptions options, HashSet<string> replacedDocs)
        {
            var scope = new string[sources.Length][];
            for (var s = 0; s < sources.Length; ++s)
            {
                var t = sources[s];
                var n = t.NumRows(TableNames.Warning);
                if (n > 0 && t.Strs(TableNames.Warning, "string:Key") == null)
                    return null;
                var docs = t.Index(TableNames.Warning, "index:" + TableNames.BimDocument + ":BimDocument");
                var host = t.HostBimDocuments();
                var titles = t.Strs(TableNames.BimDocument, "string:Title") ?? new string[host.Length];
                var aliases = s == 1 ? options.DocumentTitleAliases : null;
                scope[s] = new string[n];
                for (var r = 0; r < n; ++r)
                {
                    var d = docs == null ? -1 : docs[r];
                    if (d < 0 || d >= host.Length) continue;
                    if (host[d]) { scope[s][r] = "\u0001host"; continue; }
                    var title = titles[d] ?? "";
                    if (aliases != null && aliases.TryGetValue(title, out var bt) && bt != null) title = bt;
                    if (replacedDocs.Contains(title)) scope[s][r] = title;
                }
            }
            // A partial that carries no warning of the host while the base has some did not carry Revit's list (an
            // exporter that predates it, or an export that skipped warnings): replacing would empty the host's
            // warnings, so the old rule applies.
            if (scope[1].All(x => x != "\u0001host") && scope[0].Any(x => x == "\u0001host"))
                return null;
            return scope;
        }

        /// <summary>
        /// Warning rows when the partial carries Revit's whole warning list for the documents in scope: those documents'
        /// rows are the partial's (one per Warning.Key), and a base row is aliased to the partial row of the same key -
        /// its ElementInWarning rows follow - or dropped when Revit no longer lists it. Other documents: the old rule.
        /// Returns how many base warnings were dropped.
        /// </summary>
        private static int PlanWarnings(VimTables[] sources, Dictionary<string, TablePlan> plans, string[][] scope)
        {
            const string table = TableNames.Warning;
            var plan = plans[table];
            var keys = sources.Select(s => s.Strs(table, "string:Key") ?? new string[s.NumRows(table)]).ToArray();
            var nb = sources[0].NumRows(table);
            var np = sources[1].NumRows(table);

            // Out of scope: the rule of every dictionary table (base rows stay, a partial row equal to one is that row).
            var relations = sources.SelectMany(s => s.IndexRelations(table)).Distinct().OrderBy(r => r.Column, StringComparer.Ordinal).ToList();
            var keyOf = KeyFunction(table, sources, plans, relations);
            var baseIndex = new Dictionary<string, int>();
            for (var r = 0; r < nb; ++r)
            {
                if (scope[0][r] != null) continue;
                var o = plan.Add(0, r);
                var k = keyOf(0, r);
                if (!baseIndex.ContainsKey(k)) baseIndex[k] = o;
            }

            var inScope = new Dictionary<string, int>();
            for (var r = 0; r < np; ++r)
            {
                if (scope[1][r] == null)
                {
                    var k = keyOf(1, r);
                    if (baseIndex.TryGetValue(k, out var o)) plan.Alias(1, r, o);
                    else baseIndex[k] = plan.Add(1, r);
                    continue;
                }
                var sk = scope[1][r] + "\u001f" + (keys[1][r] ?? "");
                if (inScope.TryGetValue(sk, out var same)) plan.Alias(1, r, same); // Revit lists the same warning twice
                else inScope[sk] = plan.Add(1, r);
            }

            var dropped = 0;
            for (var r = 0; r < nb; ++r)
            {
                if (scope[0][r] == null) continue;
                var sk = scope[0][r] + "\u001f" + (keys[0][r] ?? "");
                if (inScope.TryGetValue(sk, out var o)) plan.Alias(0, r, o);
                else dropped++;
            }
            return dropped;
        }

        /// <summary>
        /// After <see cref="PlanWarnings"/>: an ElementInWarning row whose warning is gone goes too, two rows naming the
        /// same element in the same warning become one, and a warning of the documents in scope that no element of the
        /// file names any more goes - a full export writes a warning only when it names an exported element.
        /// Returns how many warnings were removed that way.
        /// </summary>
        private static int ReconcileElementsInWarning(VimTables[] sources, Dictionary<string, TablePlan> plans, string[][] scope)
        {
            if (!plans.TryGetValue(TableNames.ElementInWarning, out var eiw) || !plans.TryGetValue(TableNames.Warning, out var warnings))
                return 0;
            var elements = plans[TableNames.Element];
            var wCol = sources.Select(s => s.Index(TableNames.ElementInWarning, "index:" + TableNames.Warning + ":Warning")).ToArray();
            var eCol = sources.Select(s => s.Index(TableNames.ElementInWarning, VimTables.ElementColumn)).ToArray();
            int OutWarning(int i) { var c = wCol[eiw.Source[i]]; var v = c == null ? -1 : c[eiw.Row[i]]; return warnings.Lookup(eiw.Source[i], v); }
            int OutElement(int i) { var c = eCol[eiw.Source[i]]; var v = c == null ? -1 : c[eiw.Row[i]]; return elements.Lookup(eiw.Source[i], v); }

            var seen = new HashSet<(int, int)>();
            eiw.RemoveWhere(i =>
            {
                var w = OutWarning(i);
                if (w < 0) return true;
                return !seen.Add((OutElement(i), w));
            });

            var named = new bool[warnings.Count];
            for (var i = 0; i < eiw.Count; ++i)
            {
                var w = OutWarning(i);
                if (w >= 0) named[w] = true;
            }
            // The partial's rows naming nothing exported (room tags, separation lines...) are dropped as a full export
            // never writes them; only base rows are counted as removed.
            var removedFromBase = 0;
            warnings.RemoveWhere(i =>
            {
                var gone = !named[i] && scope[warnings.Source[i]][warnings.Row[i]] != null;
                if (gone && warnings.Source[i] == 0) removedFromBase++;
                return gone;
            });
            return removedFromBase;
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
