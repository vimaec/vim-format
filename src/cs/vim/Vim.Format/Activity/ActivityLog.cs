using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Vim.Format.ObjectModel;

namespace Vim.Format.Activity
{
    /// <summary>One person, as one source names them. Two sources naming the same person are two contributors.</summary>
    public sealed class ActivityContributor : IEquatable<ActivityContributor>
    {
        public string Name = "";
        public ContributorSource Source;

        public ActivityContributor() { }

        public ActivityContributor(string name, ContributorSource source)
        {
            Name = name ?? "";
            Source = source;
        }

        public bool Equals(ActivityContributor other)
            => other != null && Source == other.Source && string.Equals(Name, other.Name, StringComparison.Ordinal);

        public override bool Equals(object obj) => Equals(obj as ActivityContributor);

        public override int GetHashCode() => (Name ?? "").GetHashCode() ^ (int) Source;
    }

    /// <summary>One Revit session VIM watched. Written again at every sync it feeds, with a later <see cref="EndedUtc"/>.</summary>
    public sealed class ActivitySession
    {
        public string SessionGuid = "", MachineName = "", RevitVersion = "", StartedUtc = "", EndedUtc = "";
        public ActivityContributor Contributor = new ActivityContributor();
    }

    /// <summary>One element changed once.</summary>
    public sealed class ActivityChange
    {
        public string ChangedUtc = "";
        /// <summary>The session which made it; empty when the change arrived from the central model.</summary>
        public string SessionGuid = "";
        public ActivityContributor Contributor = new ActivityContributor();
        public ElementChangeKind Kind;
        public ElementChangeConfidence Confidence;
        public string UniqueId = "", CategoryName = "", FamilyName = "", TypeName = "", ElementName = "", LevelName = "", WorksetName = "";
    }

    /// <summary>Something that happened to the log itself (see <see cref="ActivityMarkerKind"/>).</summary>
    public sealed class ActivityNote
    {
        public string OccurredUtc = "", Description = "", SessionGuid = "";
        public ActivityContributor Contributor = new ActivityContributor();
        public ActivityMarkerKind Kind;
    }

    /// <summary>
    /// The activity log as plain data: what a writer collects and what it writes into a .vim as the Contributor,
    /// SyncSession, ElementChange and ActivityMarker tables (object model 5.11.0). It also reads and writes itself as
    /// text, which is how the Revit exporter keeps it in a sidecar file next to the .vim: a full export rewrites the
    /// whole .vim, so without that sidecar every full export would start the history again from nothing.
    /// </summary>
    public sealed class ActivityLog
    {
        /// <summary>The text format version of the sidecar file. A later one is not read.</summary>
        public const int TextFormatVersion = 1;

        public List<ActivitySession> Sessions = new List<ActivitySession>();
        public List<ActivityChange> Changes = new List<ActivityChange>();
        public List<ActivityNote> Notes = new List<ActivityNote>();

        public bool IsEmpty => Sessions.Count == 0 && Changes.Count == 0 && Notes.Count == 0;

        /// <summary>
        /// Adds everything of <paramref name="other"/> that this log does not already hold: a session is the same
        /// session when the guid matches (the later reading of its start and end wins), a change and a note are the
        /// same when every field matches.
        /// </summary>
        public void Merge(ActivityLog other)
        {
            if (other == null) return;

            var byGuid = Sessions.Where(s => s.SessionGuid.Length > 0).GroupBy(s => s.SessionGuid)
                .ToDictionary(g => g.Key, g => g.First(), StringComparer.Ordinal);
            foreach (var s in other.Sessions)
            {
                if (s.SessionGuid.Length > 0 && byGuid.TryGetValue(s.SessionGuid, out var mine))
                {
                    if (string.CompareOrdinal(s.StartedUtc, mine.StartedUtc) < 0 && s.StartedUtc.Length > 0) mine.StartedUtc = s.StartedUtc;
                    if (string.CompareOrdinal(s.EndedUtc, mine.EndedUtc) > 0) mine.EndedUtc = s.EndedUtc;
                    if (mine.MachineName.Length == 0) mine.MachineName = s.MachineName;
                    if (mine.RevitVersion.Length == 0) mine.RevitVersion = s.RevitVersion;
                    continue;
                }
                Sessions.Add(s);
                if (s.SessionGuid.Length > 0) byGuid[s.SessionGuid] = s;
            }

            var seenChanges = new HashSet<string>(Changes.Select(ChangeKey), StringComparer.Ordinal);
            foreach (var c in other.Changes)
                if (seenChanges.Add(ChangeKey(c)))
                    Changes.Add(c);

            var seenNotes = new HashSet<string>(Notes.Select(NoteKey), StringComparer.Ordinal);
            foreach (var n in other.Notes)
                if (seenNotes.Add(NoteKey(n)))
                    Notes.Add(n);
        }

        /// <summary>
        /// Drops what is older than <paramref name="retentionDays"/> and, past <paramref name="maxChangeRows"/>, the
        /// oldest changes, so a year of logging does not quietly grow the .vim. A session nothing points at any more
        /// goes with them. Returns how many change rows were dropped (0 = nothing was trimmed).
        /// Either cap is off when it is 0 or less.
        /// </summary>
        public int Trim(DateTime nowUtc, int retentionDays, int maxChangeRows)
        {
            var before = Changes.Count;

            if (retentionDays > 0)
            {
                var oldest = nowUtc.AddDays(-retentionDays);
                Changes = Changes.Where(c => !IsOlderThan(c.ChangedUtc, oldest)).ToList();
                Notes = Notes.Where(n => !IsOlderThan(n.OccurredUtc, oldest)).ToList();
            }

            if (maxChangeRows > 0 && Changes.Count > maxChangeRows)
                Changes = Changes.OrderBy(c => c.ChangedUtc, StringComparer.Ordinal).Skip(Changes.Count - maxChangeRows).ToList();

            var used = new HashSet<string>(Changes.Select(c => c.SessionGuid).Concat(Notes.Select(n => n.SessionGuid)), StringComparer.Ordinal);
            Sessions = Sessions.Where(s => used.Contains(s.SessionGuid)).ToList();

            return before - Changes.Count;
        }

        /// <summary>
        /// Writes the four activity tables into the builder (nothing when the log is empty). The sessions are bound to
        /// <paramref name="bimDocumentIndex"/>; the changes and the markers are left pointing at no ExportRevision
        /// (-1), which reads as "the export that wrote this file" and is resolved to a row the first time the file is
        /// patched (<see cref="Merge.VimTables.EnsureExportRevisions"/>).
        /// </summary>
        public void WriteInto(DocumentBuilder db, int bimDocumentIndex)
        {
            if (db == null || IsEmpty)
                return;

            var contributors = new List<ActivityContributor>();
            var contributorIndex = new Dictionary<ActivityContributor, int>();
            int Person(ActivityContributor c)
            {
                c = c ?? new ActivityContributor();
                if (contributorIndex.TryGetValue(c, out var i)) return i;
                contributors.Add(c);
                return contributorIndex[c] = contributors.Count - 1;
            }

            var sessionIndex = new Dictionary<string, int>(StringComparer.Ordinal);
            for (var i = 0; i < Sessions.Count; ++i)
                if (Sessions[i].SessionGuid.Length > 0 && !sessionIndex.ContainsKey(Sessions[i].SessionGuid))
                    sessionIndex[Sessions[i].SessionGuid] = i;
            int SessionOf(string guid)
                => guid != null && sessionIndex.TryGetValue(guid, out var i) ? i : -1;

            // The contributors have to be numbered before the tables that point at them are written.
            var sessionPeople = Sessions.Select(s => Person(s.Contributor)).ToArray();
            var changePeople = Changes.Select(c => Person(c.Contributor)).ToArray();
            var notePeople = Notes.Select(n => Person(n.Contributor)).ToArray();

            if (contributors.Count > 0)
            {
                var t = Table(db,TableNames.Contributor);
                t.AddStringColumn("string:Name", contributors.Select(c => c.Name ?? ""));
                t.AddDataColumn("int:Source", contributors.Select(c => (int) c.Source));
            }

            if (Sessions.Count > 0)
            {
                var t = Table(db,TableNames.SyncSession);
                t.AddStringColumn("string:SessionGuid", Sessions.Select(s => s.SessionGuid ?? ""));
                t.AddStringColumn("string:MachineName", Sessions.Select(s => s.MachineName ?? ""));
                t.AddStringColumn("string:RevitVersion", Sessions.Select(s => s.RevitVersion ?? ""));
                t.AddStringColumn("string:StartedUtc", Sessions.Select(s => s.StartedUtc ?? ""));
                t.AddStringColumn("string:EndedUtc", Sessions.Select(s => s.EndedUtc ?? ""));
                t.AddIndexColumn("index:" + TableNames.Contributor + ":Contributor", sessionPeople);
                t.AddIndexColumn("index:" + TableNames.BimDocument + ":BimDocument", Sessions.Select(_ => bimDocumentIndex));
            }

            if (Changes.Count > 0)
            {
                var t = Table(db,TableNames.ElementChange);
                t.AddStringColumn("string:ChangedUtc", Changes.Select(c => c.ChangedUtc ?? ""));
                t.AddStringColumn("string:UniqueId", Changes.Select(c => c.UniqueId ?? ""));
                t.AddStringColumn("string:CategoryName", Changes.Select(c => c.CategoryName ?? ""));
                t.AddStringColumn("string:FamilyName", Changes.Select(c => c.FamilyName ?? ""));
                t.AddStringColumn("string:TypeName", Changes.Select(c => c.TypeName ?? ""));
                t.AddStringColumn("string:ElementName", Changes.Select(c => c.ElementName ?? ""));
                t.AddStringColumn("string:LevelName", Changes.Select(c => c.LevelName ?? ""));
                t.AddStringColumn("string:WorksetName", Changes.Select(c => c.WorksetName ?? ""));
                t.AddDataColumn("int:ChangeKind", Changes.Select(c => (int) c.Kind));
                t.AddDataColumn("int:Confidence", Changes.Select(c => (int) c.Confidence));
                t.AddIndexColumn("index:" + TableNames.Contributor + ":Contributor", changePeople);
                t.AddIndexColumn("index:" + TableNames.SyncSession + ":SyncSession", Changes.Select(c => SessionOf(c.SessionGuid)));
                t.AddIndexColumn("index:" + TableNames.ExportRevision + ":ExportRevision", Changes.Select(_ => -1));
            }

            if (Notes.Count > 0)
            {
                var t = Table(db,TableNames.ActivityMarker);
                t.AddStringColumn("string:OccurredUtc", Notes.Select(n => n.OccurredUtc ?? ""));
                t.AddStringColumn("string:Description", Notes.Select(n => n.Description ?? ""));
                t.AddDataColumn("int:Kind", Notes.Select(n => (int) n.Kind));
                t.AddIndexColumn("index:" + TableNames.Contributor + ":Contributor", notePeople);
                t.AddIndexColumn("index:" + TableNames.SyncSession + ":SyncSession", Notes.Select(n => SessionOf(n.SessionGuid)));
                t.AddIndexColumn("index:" + TableNames.ExportRevision + ":ExportRevision", Notes.Select(_ => -1));
            }
        }

        // -------------------------------------------------------------------------------------------------------
        // The sidecar text file
        // -------------------------------------------------------------------------------------------------------

        public string ToText()
        {
            var sb = new StringBuilder();
            sb.Append("vim-activity=").Append(TextFormatVersion).Append('\n');
            sb.Append("#sessions sessionGuid\tmachine\trevitVersion\tstartedUtc\tendedUtc\tcontributor\tsource\n");
            foreach (var s in Sessions)
                Line(sb, s.SessionGuid, s.MachineName, s.RevitVersion, s.StartedUtc, s.EndedUtc,
                    s.Contributor?.Name, Num((int?) s.Contributor?.Source ?? 0));
            sb.Append("#changes changedUtc\tsessionGuid\tcontributor\tsource\tkind\tconfidence\tuniqueId\tcategory\tfamily\ttype\tname\tlevel\tworkset\n");
            foreach (var c in Changes)
                Line(sb, c.ChangedUtc, c.SessionGuid, c.Contributor?.Name, Num((int?) c.Contributor?.Source ?? 0),
                    Num((int) c.Kind), Num((int) c.Confidence), c.UniqueId,
                    c.CategoryName, c.FamilyName, c.TypeName, c.ElementName, c.LevelName, c.WorksetName);
            sb.Append("#notes occurredUtc\tkind\tcontributor\tsource\tsessionGuid\tdescription\n");
            foreach (var n in Notes)
                Line(sb, n.OccurredUtc, Num((int) n.Kind), n.Contributor?.Name, Num((int?) n.Contributor?.Source ?? 0),
                    n.SessionGuid, n.Description);
            return sb.ToString();
        }

        /// <summary>Reads the sidecar text; null when it is unreadable or written by a later version.</summary>
        public static ActivityLog FromText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return null;
            var log = new ActivityLog();
            var section = 0; // 1 sessions, 2 changes, 3 notes
            foreach (var raw in text.Split('\n'))
            {
                var line = raw.TrimEnd('\r');
                if (line.Length == 0) continue;
                if (line.StartsWith("#sessions")) { section = 1; continue; }
                if (line.StartsWith("#changes")) { section = 2; continue; }
                if (line.StartsWith("#notes")) { section = 3; continue; }
                if (section == 0)
                {
                    var eq = line.IndexOf('=');
                    if (eq < 0) continue;
                    if (line.Substring(0, eq) != "vim-activity") continue;
                    if (!int.TryParse(line.Substring(eq + 1), NumberStyles.Integer, CultureInfo.InvariantCulture, out var v) || v < 1 || v > TextFormatVersion)
                        return null;
                    continue;
                }
                var t = line.Split('\t');
                string At(int i) => i < t.Length ? t[i] : "";
                int Int(int i) { int.TryParse(At(i), NumberStyles.Integer, CultureInfo.InvariantCulture, out var n); return n; }
                switch (section)
                {
                    case 1:
                        log.Sessions.Add(new ActivitySession
                        {
                            SessionGuid = At(0), MachineName = At(1), RevitVersion = At(2),
                            StartedUtc = At(3), EndedUtc = At(4),
                            Contributor = new ActivityContributor(At(5), (ContributorSource) Int(6)),
                        });
                        break;
                    case 2:
                        log.Changes.Add(new ActivityChange
                        {
                            ChangedUtc = At(0), SessionGuid = At(1),
                            Contributor = new ActivityContributor(At(2), (ContributorSource) Int(3)),
                            Kind = (ElementChangeKind) Int(4), Confidence = (ElementChangeConfidence) Int(5),
                            UniqueId = At(6), CategoryName = At(7), FamilyName = At(8), TypeName = At(9),
                            ElementName = At(10), LevelName = At(11), WorksetName = At(12),
                        });
                        break;
                    default:
                        log.Notes.Add(new ActivityNote
                        {
                            OccurredUtc = At(0), Kind = (ActivityMarkerKind) Int(1),
                            Contributor = new ActivityContributor(At(2), (ContributorSource) Int(3)),
                            SessionGuid = At(4), Description = At(5),
                        });
                        break;
                }
            }
            return log;
        }

        /// <summary>
        /// The builder's table for this entity, emptied: an exporter's document builder already carries one row-less
        /// table per entity of the object model, and these four are filled here rather than through the entity
        /// builders (there is nothing in the Revit document to build them from).
        /// </summary>
        private static EntityTableBuilder Table(DocumentBuilder db, string name)
        {
            var t = db.GetTableBuilderOrCreate(name);
            t.Clear();
            return t;
        }

        private static string Num(int n)
            => n.ToString(CultureInfo.InvariantCulture);

        private static void Line(StringBuilder sb, params string[] cells)
        {
            for (var i = 0; i < cells.Length; ++i)
            {
                if (i > 0) sb.Append('\t');
                sb.Append((cells[i] ?? "").Replace('\t', ' ').Replace('\n', ' ').Replace('\r', ' '));
            }
            sb.Append('\n');
        }

        private static bool IsOlderThan(string utc, DateTime oldest)
            => DateTime.TryParse(utc, CultureInfo.InvariantCulture,
                   DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out var t) && t < oldest;

        private static string ChangeKey(ActivityChange c)
            => string.Join("\u001f", c.ChangedUtc, c.SessionGuid, c.Contributor?.Name, ((int?) c.Contributor?.Source ?? 0).ToString(),
                ((int) c.Kind).ToString(), ((int) c.Confidence).ToString(), c.UniqueId);

        private static string NoteKey(ActivityNote n)
            => string.Join("\u001f", n.OccurredUtc, ((int) n.Kind).ToString(), n.Contributor?.Name, n.SessionGuid, n.Description);
    }
}
