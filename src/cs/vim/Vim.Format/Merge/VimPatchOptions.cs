using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vim.Format.Merge
{
    /// <summary>What <see cref="VimPatchService"/> replaces, adds and removes.</summary>
    public class VimPatchOptions
    {
        /// <summary>
        /// UniqueIds of the elements that changed (added or modified). Null = every element of the partial file counts
        /// as changed. An element of the partial file which is not in this list is context (a type, a level, a view the
        /// export writes along with the changed elements): the base keeps its own rows for it, and the partial's rows
        /// only serve to re-point relations.
        /// </summary>
        public ICollection<string> ChangedUniqueIds { get; set; }

        /// <summary>UniqueIds of the elements deleted since the base was written.</summary>
        public ICollection<string> DeletedUniqueIds { get; set; } = new HashSet<string>();

        /// <summary>
        /// Titles of the linked models the partial file holds whole: every base element of such a document is replaced
        /// by the partial's, and the ones the partial does not hold are deleted.
        /// </summary>
        public ICollection<string> ReplacedDocumentTitles { get; set; } = new HashSet<string>();

        /// <summary>
        /// UniqueId of a partial element -> UniqueId of the base element it stands for. A differential export draws the
        /// changed elements through a temporary 3D view: its rows (ElementInView...) land on the base's export view.
        /// </summary>
        public IDictionary<string, string> UniqueIdAliases { get; set; } = new Dictionary<string, string>();

        /// <summary>UniqueIds of partial elements which are never written, with every row they own.</summary>
        public ICollection<string> ExcludedUniqueIds { get; set; } = new HashSet<string>();

        /// <summary>A partial mesh equal to one the base already has is not written twice.</summary>
        public bool DeduplicateMeshes { get; set; } = true;

        /// <summary>Header values written in the patched file (added to, or replacing, the base header's).</summary>
        public IReadOnlyDictionary<string, string> HeaderValues { get; set; }

        public string Generator { get; set; } = "Vim.Format.Patch";
    }

    /// <summary>What <see cref="VimPatchService"/> did.</summary>
    public class VimPatchResult
    {
        public int BaseElements, PartialElements, OutputElements;
        public int Kept, Changed, Added, Deleted, Context, Excluded;

        /// <summary>UniqueIds listed as changed which the partial file does not hold: their base rows were kept.</summary>
        public List<string> ChangedButNotInPartial = new List<string>();

        /// <summary>UniqueIds listed as deleted which the base file does not hold.</summary>
        public List<string> DeletedButNotInBase = new List<string>();

        /// <summary>Base Element row -> output Element row (-1 = deleted). Element rows only move for deletions.</summary>
        public int[] ElementIndexMap;

        /// <summary>Per table: rows in the base, in the partial, in the output.</summary>
        public Dictionary<string, (int Base, int Partial, int Output)> TableRows = new Dictionary<string, (int, int, int)>();

        /// <summary>Document, site and building holders of the partial matched to the base's by document and kind.</summary>
        public int HoldersMatched;

        /// <summary>Rows nothing points at any more (warnings, wall layer structures, cameras), removed.</summary>
        public int OrphanRowsRemoved;

        public RowPlanWriteStats Geometry = new RowPlanWriteStats();
        public long LoadMs, PlanMs, BuildMs, WriteMs;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Elements: base {BaseElements}, partial {PartialElements}, output {OutputElements}");
            sb.AppendLine($"  kept {Kept}, changed {Changed}, added {Added}, deleted {Deleted}, context {Context}, excluded {Excluded}");
            if (ChangedButNotInPartial.Count > 0) sb.AppendLine($"  changed but not in the partial (base kept): {ChangedButNotInPartial.Count}");
            if (DeletedButNotInBase.Count > 0) sb.AppendLine($"  deleted but not in the base: {DeletedButNotInBase.Count}");
            if (HoldersMatched > 0) sb.AppendLine($"  document / site / building holders matched: {HoldersMatched}");
            if (OrphanRowsRemoved > 0) sb.AppendLine($"  rows nothing points at any more, removed: {OrphanRowsRemoved}");
            sb.AppendLine($"Geometry: meshes from base {Geometry.MeshesFromSource0}, from partial {Geometry.MeshesFromOtherSources}, reused {Geometry.MeshesReused}; instances {Geometry.Instances}, shapes {Geometry.Shapes}, materials {Geometry.Materials}");
            sb.Append($"Timings (ms): load {LoadMs}, plan {PlanMs}, build {BuildMs}, write {WriteMs}");
            foreach (var kv in TableRows.Where(kv => kv.Value.Base != kv.Value.Output).OrderBy(k => k.Key, StringComparer.Ordinal))
                sb.AppendLine().Append($"  {kv.Key}: base {kv.Value.Base}, partial {kv.Value.Partial}, output {kv.Value.Output}");
            return sb.ToString();
        }
    }
}
