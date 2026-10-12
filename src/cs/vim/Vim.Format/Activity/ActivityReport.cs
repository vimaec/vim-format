using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Vim.Format.Merge;
using Vim.Format.ObjectModel;

namespace Vim.Format.Activity
{
    /// <summary>
    /// Reads the activity log of a .vim file (object model 5.11.0: Contributor, SyncSession, ElementChange,
    /// ActivityMarker) and writes it out as CSV, for billing and for anyone who wants to dig.
    /// <para>
    /// Three files, because they are three different shapes (C:\VimFlexLocal\work\exporter\LIVE-SYNC-UX.md, 4.3):
    /// "[name].activity-summary.csv" is the billing deliverable - one row per person per day per category, with the
    /// added / changed / deleted counts and that person's sessions and hours in the model that day;
    /// "[name].activity-changes.csv" is one row per change; "[name].activity-notes.csv" holds the markers, the lines
    /// that say the log itself cannot be read at face value (a model re-saved as a new central, a reloaded link).
    /// </para>
    /// <para>
    /// Read it as the log reads itself: a name with Confidence = Observed is a person VIM watched work; FromCentral is
    /// Revit's word, second-hand, and a re-save as a new central rewrites those names for every element at once;
    /// Unknown is a deletion in somebody else's local, which Revit records nothing about.
    /// </para>
    /// </summary>
    public static class ActivityReport
    {
        public sealed class Change
        {
            public string ChangedUtc = "";
            public string Person = "";
            public ContributorSource Source;
            public ElementChangeKind Kind;
            public ElementChangeConfidence Confidence;
            public string SessionGuid = "", MachineName = "", RevitVersion = "";
            public string VimRevision = "";
            public string UniqueId = "", Category = "", Family = "", Type = "", Name = "", Level = "", Workset = "";

            /// <summary>The calendar day of the change, in UTC ("yyyy-MM-dd"); "" when the time is unreadable.</summary>
            public string Day
                => TryParseUtc(ChangedUtc, out var t) ? t.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) : "";
        }

        public sealed class Session
        {
            public string SessionGuid = "", MachineName = "", RevitVersion = "", StartedUtc = "", EndedUtc = "";
            public string Person = "";
            public ContributorSource Source;

            /// <summary>Start to end, in hours; 0 when either end is unreadable or the end is before the start.</summary>
            public double Hours
            {
                get
                {
                    if (!TryParseUtc(StartedUtc, out var a) || !TryParseUtc(EndedUtc, out var b)) return 0;
                    var h = (b - a).TotalHours;
                    return h > 0 ? h : 0;
                }
            }
        }

        public sealed class Marker
        {
            public string OccurredUtc = "", Description = "", Person = "", SessionGuid = "", VimRevision = "";
            public ActivityMarkerKind Kind;
        }

        public sealed class Log
        {
            public List<Change> Changes = new List<Change>();
            public List<Session> Sessions = new List<Session>();
            public List<Marker> Markers = new List<Marker>();
            public bool IsEmpty => Changes.Count == 0 && Sessions.Count == 0 && Markers.Count == 0;
        }

        /// <summary>One row of the billing CSV: one person, one day, one category.</summary>
        public sealed class SummaryRow
        {
            public string Person = "", Day = "", Category = "";
            public ContributorSource Source;
            public int Added, Changed, Deleted;
            /// <summary>Per person and day, not per category: the same number repeats on that person's rows for the day.</summary>
            public int SessionsOnThatDay;
            /// <summary>Per person and day, not per category (see <see cref="SessionsOnThatDay"/>).</summary>
            public double HoursInModelOnThatDay;
        }

        public static Log Read(string vimFilePath)
            => Read(VimTables.Load(vimFilePath));

        public static Log Read(VimTables vim)
        {
            var log = new Log();

            var contributorNames = vim.Strs(TableNames.Contributor, "string:Name") ?? Array.Empty<string>();
            var contributorSources = vim.Data<int>(TableNames.Contributor, "int:Source") ?? Array.Empty<int>();
            string PersonName(int i) => i >= 0 && i < contributorNames.Length ? contributorNames[i] ?? "" : "";
            ContributorSource PersonSource(int i)
                => (ContributorSource)(i >= 0 && i < contributorSources.Length ? contributorSources[i] : 0);

            var revisions = vim.Strs(TableNames.ExportRevision, "string:VimRevision") ?? Array.Empty<string>();
            // No revision row: the exporter writes -1 for "the export that wrote this file", so answer with this file.
            var ownRevision = vim.Header?.PersistingId ?? "";
            string Revision(int i) => i >= 0 && i < revisions.Length ? revisions[i] ?? "" : ownRevision;

            var nSessions = vim.NumRows(TableNames.SyncSession);
            var sessionGuids = vim.Strs(TableNames.SyncSession, "string:SessionGuid") ?? new string[nSessions];
            var sessionMachines = vim.Strs(TableNames.SyncSession, "string:MachineName") ?? new string[nSessions];
            var sessionVersions = vim.Strs(TableNames.SyncSession, "string:RevitVersion") ?? new string[nSessions];
            var sessionStarts = vim.Strs(TableNames.SyncSession, "string:StartedUtc") ?? new string[nSessions];
            var sessionEnds = vim.Strs(TableNames.SyncSession, "string:EndedUtc") ?? new string[nSessions];
            var sessionContributors = vim.Index(TableNames.SyncSession, "index:" + TableNames.Contributor + ":Contributor");
            for (var i = 0; i < nSessions; ++i)
            {
                var c = sessionContributors == null ? -1 : sessionContributors[i];
                log.Sessions.Add(new Session
                {
                    SessionGuid = sessionGuids[i] ?? "",
                    MachineName = sessionMachines[i] ?? "",
                    RevitVersion = sessionVersions[i] ?? "",
                    StartedUtc = sessionStarts[i] ?? "",
                    EndedUtc = sessionEnds[i] ?? "",
                    Person = PersonName(c),
                    Source = PersonSource(c),
                });
            }

            var nChanges = vim.NumRows(TableNames.ElementChange);
            if (nChanges > 0)
            {
                string[] Col(string name) => vim.Strs(TableNames.ElementChange, name) ?? new string[nChanges];
                var changedUtc = Col("string:ChangedUtc");
                var uniqueIds = Col("string:UniqueId");
                var categories = Col("string:CategoryName");
                var families = Col("string:FamilyName");
                var types = Col("string:TypeName");
                var names = Col("string:ElementName");
                var levels = Col("string:LevelName");
                var worksets = Col("string:WorksetName");
                var kinds = vim.Data<int>(TableNames.ElementChange, "int:ChangeKind") ?? new int[nChanges];
                var confidences = vim.Data<int>(TableNames.ElementChange, "int:Confidence") ?? new int[nChanges];
                var changeContributors = vim.Index(TableNames.ElementChange, "index:" + TableNames.Contributor + ":Contributor");
                var changeSessions = vim.Index(TableNames.ElementChange, "index:" + TableNames.SyncSession + ":SyncSession");
                var changeRevisions = vim.Index(TableNames.ElementChange, "index:" + TableNames.ExportRevision + ":ExportRevision");

                for (var i = 0; i < nChanges; ++i)
                {
                    var c = changeContributors == null ? -1 : changeContributors[i];
                    var s = changeSessions == null ? -1 : changeSessions[i];
                    log.Changes.Add(new Change
                    {
                        ChangedUtc = changedUtc[i] ?? "",
                        Person = PersonName(c),
                        Source = PersonSource(c),
                        Kind = (ElementChangeKind)kinds[i],
                        Confidence = (ElementChangeConfidence)confidences[i],
                        SessionGuid = s >= 0 && s < log.Sessions.Count ? log.Sessions[s].SessionGuid : "",
                        MachineName = s >= 0 && s < log.Sessions.Count ? log.Sessions[s].MachineName : "",
                        RevitVersion = s >= 0 && s < log.Sessions.Count ? log.Sessions[s].RevitVersion : "",
                        VimRevision = Revision(changeRevisions == null ? -1 : changeRevisions[i]),
                        UniqueId = uniqueIds[i] ?? "",
                        Category = categories[i] ?? "",
                        Family = families[i] ?? "",
                        Type = types[i] ?? "",
                        Name = names[i] ?? "",
                        Level = levels[i] ?? "",
                        Workset = worksets[i] ?? "",
                    });
                }
            }

            var nMarkers = vim.NumRows(TableNames.ActivityMarker);
            if (nMarkers > 0)
            {
                var occurred = vim.Strs(TableNames.ActivityMarker, "string:OccurredUtc") ?? new string[nMarkers];
                var descriptions = vim.Strs(TableNames.ActivityMarker, "string:Description") ?? new string[nMarkers];
                var markerKinds = vim.Data<int>(TableNames.ActivityMarker, "int:Kind") ?? new int[nMarkers];
                var markerContributors = vim.Index(TableNames.ActivityMarker, "index:" + TableNames.Contributor + ":Contributor");
                var markerSessions = vim.Index(TableNames.ActivityMarker, "index:" + TableNames.SyncSession + ":SyncSession");
                var markerRevisions = vim.Index(TableNames.ActivityMarker, "index:" + TableNames.ExportRevision + ":ExportRevision");
                for (var i = 0; i < nMarkers; ++i)
                {
                    var s = markerSessions == null ? -1 : markerSessions[i];
                    log.Markers.Add(new Marker
                    {
                        OccurredUtc = occurred[i] ?? "",
                        Description = descriptions[i] ?? "",
                        Kind = (ActivityMarkerKind)markerKinds[i],
                        Person = PersonName(markerContributors == null ? -1 : markerContributors[i]),
                        SessionGuid = s >= 0 && s < log.Sessions.Count ? log.Sessions[s].SessionGuid : "",
                        VimRevision = Revision(markerRevisions == null ? -1 : markerRevisions[i]),
                    });
                }
            }

            return log;
        }

        /// <summary>One row per person per day per category, sorted by day, person then category.</summary>
        public static List<SummaryRow> Summarize(Log log)
        {
            // A person's sessions and hours for a day: a session counts for every day it changed something on.
            var hoursOf = new Dictionary<string, double>(StringComparer.Ordinal);
            var sessionsOf = new Dictionary<string, HashSet<string>>(StringComparer.Ordinal);
            var sessionByGuid = log.Sessions.GroupBy(s => s.SessionGuid ?? "").ToDictionary(g => g.Key, g => g.First(), StringComparer.Ordinal);
            foreach (var g in log.Changes.Where(c => c.SessionGuid.Length > 0).GroupBy(c => c.Person + "\u001f" + c.Day))
            {
                var guids = new HashSet<string>(g.Select(c => c.SessionGuid), StringComparer.Ordinal);
                sessionsOf[g.Key] = guids;
                hoursOf[g.Key] = guids.Sum(guid => sessionByGuid.TryGetValue(guid, out var s) ? s.Hours : 0);
            }

            var rows = new List<SummaryRow>();
            foreach (var g in log.Changes.GroupBy(c => new { c.Person, c.Source, c.Day, c.Category }))
            {
                var dayKey = g.Key.Person + "\u001f" + g.Key.Day;
                rows.Add(new SummaryRow
                {
                    Person = g.Key.Person,
                    Source = g.Key.Source,
                    Day = g.Key.Day,
                    Category = g.Key.Category,
                    Added = g.Count(c => c.Kind == ElementChangeKind.Added),
                    Changed = g.Count(c => c.Kind == ElementChangeKind.Modified),
                    Deleted = g.Count(c => c.Kind == ElementChangeKind.Deleted),
                    SessionsOnThatDay = sessionsOf.TryGetValue(dayKey, out var s) ? s.Count : 0,
                    HoursInModelOnThatDay = hoursOf.TryGetValue(dayKey, out var h) ? Math.Round(h, 2) : 0,
                });
            }
            return rows
                .OrderBy(r => r.Day, StringComparer.Ordinal)
                .ThenBy(r => r.Person, StringComparer.Ordinal)
                .ThenBy(r => r.Category, StringComparer.Ordinal)
                .ToList();
        }

        /// <summary>
        /// Writes the three CSV files for a .vim, next to it or into <paramref name="outputDirectory"/>.
        /// Returns the paths written, in the order summary, changes, notes. The files are written even when the log is
        /// empty (a header line only), so that "nothing was recorded" is a readable answer.
        /// </summary>
        public static string[] WriteCsv(string vimFilePath, string outputDirectory = null)
            => WriteCsv(Read(vimFilePath), vimFilePath, outputDirectory);

        /// <summary>Same, for a log already read (the .vim is only used for the file names).</summary>
        public static string[] WriteCsv(Log log, string vimFilePath, string outputDirectory = null)
        {
            var dir = string.IsNullOrEmpty(outputDirectory) ? Path.GetDirectoryName(Path.GetFullPath(vimFilePath)) : outputDirectory;
            if (!string.IsNullOrEmpty(dir))
                Directory.CreateDirectory(dir);
            var stem = Path.Combine(dir ?? "", Path.GetFileNameWithoutExtension(vimFilePath));

            var summaryPath = stem + ".activity-summary.csv";
            var changesPath = stem + ".activity-changes.csv";
            var notesPath = stem + ".activity-notes.csv";
            File.WriteAllText(summaryPath, SummaryCsv(log), Utf8);
            File.WriteAllText(changesPath, ChangesCsv(log), Utf8);
            File.WriteAllText(notesPath, NotesCsv(log), Utf8);
            return new[] { summaryPath, changesPath, notesPath };
        }

        public static string SummaryCsv(Log log)
        {
            var sb = new StringBuilder();
            Row(sb, "Day", "Person", "NameFrom", "Category", "Added", "Changed", "Deleted", "SessionsOnThatDay", "HoursInModelOnThatDay");
            foreach (var r in Summarize(log))
                Row(sb, r.Day, r.Person, SourceText(r.Source), r.Category,
                    r.Added.ToString(CultureInfo.InvariantCulture),
                    r.Changed.ToString(CultureInfo.InvariantCulture),
                    r.Deleted.ToString(CultureInfo.InvariantCulture),
                    r.SessionsOnThatDay.ToString(CultureInfo.InvariantCulture),
                    r.HoursInModelOnThatDay.ToString("0.##", CultureInfo.InvariantCulture));
            return sb.ToString();
        }

        public static string ChangesCsv(Log log)
        {
            var sb = new StringBuilder();
            Row(sb, "ChangedUtc", "Person", "NameFrom", "Confidence", "Change", "Category", "Family", "Type", "Name",
                "Level", "Workset", "UniqueId", "SessionGuid", "Machine", "RevitVersion", "VimRevision");
            foreach (var c in log.Changes.OrderBy(c => c.ChangedUtc, StringComparer.Ordinal))
                Row(sb, c.ChangedUtc, c.Person, SourceText(c.Source), c.Confidence.ToString(), c.Kind.ToString(),
                    c.Category, c.Family, c.Type, c.Name, c.Level, c.Workset, c.UniqueId, c.SessionGuid,
                    c.MachineName, c.RevitVersion, c.VimRevision);
            return sb.ToString();
        }

        public static string NotesCsv(Log log)
        {
            var sb = new StringBuilder();
            Row(sb, "OccurredUtc", "Kind", "Person", "Description", "SessionGuid", "VimRevision");
            foreach (var m in log.Markers.OrderBy(m => m.OccurredUtc, StringComparer.Ordinal))
                Row(sb, m.OccurredUtc, m.Kind.ToString(), m.Person, m.Description, m.SessionGuid, m.VimRevision);
            return sb.ToString();
        }

        /// <summary>Where the name came from, in words rather than in an enum name.</summary>
        public static string SourceText(ContributorSource source)
        {
            switch (source)
            {
                case ContributorSource.Session: return "VIM watched it";
                case ContributorSource.Central: return "Revit (last changed by)";
                case ContributorSource.Link: return "a linked model";
                default: return "author unknown";
            }
        }

        private static readonly UTF8Encoding Utf8 = new UTF8Encoding(false);

        private static bool TryParseUtc(string s, out DateTime value)
            => DateTime.TryParse(s, CultureInfo.InvariantCulture,
                DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out value);

        private static void Row(StringBuilder sb, params string[] cells)
        {
            for (var i = 0; i < cells.Length; ++i)
            {
                if (i > 0) sb.Append(',');
                sb.Append(Cell(cells[i]));
            }
            sb.Append("\r\n"); // Excel reads a CSV with CRLF on every machine.
        }

        private static string Cell(string s)
        {
            s = s ?? "";
            return s.IndexOfAny(new[] { ',', '"', '\n', '\r' }) < 0 ? s : "\"" + s.Replace("\"", "\"\"") + "\"";
        }
    }
}
