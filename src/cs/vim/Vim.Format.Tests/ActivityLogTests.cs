using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Vim.Format.Activity;
using Vim.Format.ObjectModel;
using Vim.Format.SceneBuilder;
using Vim.LinqArray;
using Vim.Util.Tests;

namespace Vim.Format.Tests
{
    /// <summary>
    /// The activity log (object model 5.11.0): the four entities survive a write and a read, the sidecar text says
    /// the same thing as the file, the retention caps drop the oldest and the CSV reads as a person would expect.
    /// </summary>
    [TestFixture]
    public static class ActivityLogTests
    {
        private const string Session = "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee";

        private static ActivityLog Sample()
            => new ActivityLog
            {
                Sessions =
                {
                    new ActivitySession
                    {
                        SessionGuid = Session,
                        MachineName = "DESK-7",
                        RevitVersion = "2027",
                        StartedUtc = "2026-10-12T08:00:00.0000000Z",
                        EndedUtc = "2026-10-12T14:22:00.0000000Z",
                        Contributor = new ActivityContributor("s.arsenault", ContributorSource.Session),
                    },
                },
                Changes =
                {
                    new ActivityChange
                    {
                        ChangedUtc = "2026-10-12T14:22:00.0000000Z",
                        SessionGuid = Session,
                        Contributor = new ActivityContributor("s.arsenault", ContributorSource.Session),
                        Kind = ElementChangeKind.Deleted,
                        Confidence = ElementChangeConfidence.Observed,
                        UniqueId = "uid-door",
                        CategoryName = "Doors",
                        FamilyName = "M_Single-Flush",
                        TypeName = "0915x2134",
                        ElementName = "0915x2134",
                        LevelName = "Level 4",
                        WorksetName = "Shell",
                    },
                    new ActivityChange
                    {
                        ChangedUtc = "2026-10-12T11:40:00.0000000Z",
                        Contributor = new ActivityContributor("j.tremblay", ContributorSource.Central),
                        Kind = ElementChangeKind.Modified,
                        Confidence = ElementChangeConfidence.FromCentral,
                        UniqueId = "uid-wall",
                        CategoryName = "Walls",
                    },
                    new ActivityChange
                    {
                        ChangedUtc = "2026-10-12T11:40:00.0000000Z",
                        Contributor = new ActivityContributor("", ContributorSource.Unknown),
                        Kind = ElementChangeKind.Deleted,
                        Confidence = ElementChangeConfidence.Unknown,
                        UniqueId = "uid-gone",
                        CategoryName = "Pipe Fittings",
                    },
                },
                Notes =
                {
                    new ActivityNote
                    {
                        OccurredUtc = "2026-10-12T11:40:00.0000000Z",
                        Kind = ActivityMarkerKind.HistoryReset,
                        Contributor = new ActivityContributor("", ContributorSource.Unknown),
                        Description = "This model looks like it was re-saved as a new central since the last export.",
                    },
                },
            };

        /// <summary>A document with one BimDocument, so the sessions have something to belong to.</summary>
        private static string WriteVim(string dir, string name, ActivityLog log)
        {
            var store = new ObjectModelStore();
            var omb = store.ObjectModelBuilder;
            var bimDocument = new BimDocument { Name = "Activity", Title = "Activity", PathName = "C:\\activity.rvt" };
            var holder = omb.ElementBuilder.Add(bimDocument.CreateParameterHolderElement()).Entity;
            bimDocument._Element.Index = holder.Index;
            omb.BimDocumentBuilder.Add(bimDocument);

            var db = store.ToDocumentBuilder(nameof(ActivityLogTests), "0.0.0");
            log.WriteInto(db, 0);
            var path = Path.Combine(dir, name);
            db.Write(path);
            return path;
        }

        [Test]
        public static void TestTheFourEntitiesSurviveAWriteAndARead()
        {
            var dir = new CallerTestContext().PrepareDirectory();
            var path = WriteVim(dir, "activity.vim", Sample());

            var vim = VimScene.LoadVim(path);
            vim.Validate();
            var dm = vim.DocumentModel;

            Assert.AreEqual(SchemaVersion.Current.ToString(), vim.Document.Header.Schema.ToString());

            var session = Enumerable.Range(0, dm.NumSyncSession).Select(dm.GetSyncSession).Single();
            Assert.AreEqual(Session, session.SessionGuid);
            Assert.AreEqual("DESK-7", session.MachineName);
            Assert.AreEqual("2027", session.RevitVersion);
            Assert.AreEqual("s.arsenault", session.Contributor.Name);
            Assert.AreEqual(ContributorSource.Session, session.Contributor.GetSource());
            Assert.AreEqual(0, session._BimDocument.Index);

            var changes = Enumerable.Range(0, dm.NumElementChange).Select(dm.GetElementChange)
                .ToDictionary(c => c.UniqueId, c => c);
            Assert.AreEqual(3, changes.Count);

            var door = changes["uid-door"];
            Assert.AreEqual(ElementChangeKind.Deleted, door.GetChangeKind());
            Assert.AreEqual(ElementChangeConfidence.Observed, door.GetConfidence());
            Assert.AreEqual("Doors", door.CategoryName);
            Assert.AreEqual("M_Single-Flush", door.FamilyName);
            Assert.AreEqual("0915x2134", door.TypeName);
            Assert.AreEqual("Level 4", door.LevelName);
            Assert.AreEqual("Shell", door.WorksetName);
            Assert.AreEqual(session.Index, door._SyncSession.Index, "a watched change belongs to its session");
            Assert.AreEqual("s.arsenault", door.Contributor.Name);

            var wall = changes["uid-wall"];
            Assert.AreEqual(ElementChangeConfidence.FromCentral, wall.GetConfidence());
            Assert.AreEqual(ContributorSource.Central, wall.Contributor.GetSource());
            Assert.AreEqual(-1, wall._SyncSession.Index, "a change that arrived from central belongs to no session of ours");

            var gone = changes["uid-gone"];
            Assert.AreEqual(ElementChangeConfidence.Unknown, gone.GetConfidence());
            Assert.AreEqual("", gone.Contributor.Name, "Revit records nobody for a deletion in somebody else's local");

            var marker = Enumerable.Range(0, dm.NumActivityMarker).Select(dm.GetActivityMarker).Single();
            Assert.AreEqual(ActivityMarkerKind.HistoryReset, marker.GetKind());
            StringAssert.Contains("new central", marker.Description);

            // Three names, three sources: the same person seen twice would be two rows on purpose.
            Assert.AreEqual(3, dm.NumContributor);
        }

        [Test]
        public static void TestTheSidecarTextSaysTheSameThing()
        {
            var text = Sample().ToText();
            var back = ActivityLog.FromText(text);

            Assert.IsNotNull(back);
            Assert.AreEqual(1, back.Sessions.Count);
            Assert.AreEqual(3, back.Changes.Count);
            Assert.AreEqual(1, back.Notes.Count);
            Assert.AreEqual(text, back.ToText(), "the text is its own fixed point");

            var door = back.Changes.Single(c => c.UniqueId == "uid-door");
            Assert.AreEqual(ElementChangeKind.Deleted, door.Kind);
            Assert.AreEqual(ElementChangeConfidence.Observed, door.Confidence);
            Assert.AreEqual("Level 4", door.LevelName);
            Assert.AreEqual(ContributorSource.Session, door.Contributor.Source);

            // A sidecar written by a later VIM is not read at all, rather than read wrongly.
            Assert.IsNull(ActivityLog.FromText("vim-activity=99\n"));
        }

        [Test]
        public static void TestMergeAndTrim()
        {
            var log = Sample();
            log.Merge(Sample());
            Assert.AreEqual(3, log.Changes.Count, "merging the same log twice adds nothing");
            Assert.AreEqual(1, log.Sessions.Count);

            var later = new ActivityLog
            {
                Sessions =
                {
                    new ActivitySession
                    {
                        SessionGuid = Session, StartedUtc = "2026-10-12T08:00:00.0000000Z",
                        EndedUtc = "2026-10-12T17:00:00.0000000Z",
                        Contributor = new ActivityContributor("s.arsenault", ContributorSource.Session),
                    },
                },
            };
            log.Merge(later);
            Assert.AreEqual(1, log.Sessions.Count);
            Assert.AreEqual("2026-10-12T17:00:00.0000000Z", log.Sessions[0].EndedUtc, "the session's end moves, it does not fork");

            // 90 days of retention, counted from a day well after these rows.
            var dropped = log.Trim(new DateTime(2027, 10, 12, 0, 0, 0, DateTimeKind.Utc), 90, 0);
            Assert.AreEqual(3, dropped);
            Assert.AreEqual(0, log.Changes.Count);
            Assert.AreEqual(0, log.Sessions.Count, "a session nothing points at goes with its changes");

            // The row cap keeps the newest.
            var capped = Sample();
            Assert.AreEqual(2, capped.Trim(new DateTime(2026, 10, 12, 18, 0, 0, DateTimeKind.Utc), 0, 1));
            Assert.AreEqual("uid-door", capped.Changes.Single().UniqueId);
        }

        [Test]
        public static void TestTheCsvReadsAsAPersonWouldExpect()
        {
            var dir = new CallerTestContext().PrepareDirectory();
            var path = WriteVim(dir, "csv.vim", Sample());
            var files = ActivityReport.WriteCsv(path);

            Assert.AreEqual(3, files.Length);
            foreach (var f in files)
                Assert.IsTrue(File.Exists(f), f);

            var summary = File.ReadAllLines(files[0]);
            StringAssert.StartsWith("Day,Person,NameFrom,Category,Added,Changed,Deleted,SessionsOnThatDay,HoursInModelOnThatDay", summary[0]);
            Assert.AreEqual(4, summary.Length, "a header and one row per person per day per category");
            Assert.IsTrue(summary.Any(l => l.StartsWith("2026-10-12,s.arsenault,VIM watched it,Doors,0,0,1,1,6.37")), string.Join("\n", summary));
            Assert.IsTrue(summary.Any(l => l.Contains("j.tremblay,Revit (last changed by),Walls,0,1,0,0,0")), string.Join("\n", summary));
            Assert.IsTrue(summary.Any(l => l.Contains(",author unknown,Pipe Fittings,0,0,1,0,0")), string.Join("\n", summary));

            var changes = File.ReadAllLines(files[1]);
            Assert.AreEqual(4, changes.Length);
            StringAssert.StartsWith("ChangedUtc,Person,NameFrom,Confidence,Change,Category", changes[0]);

            var notes = File.ReadAllLines(files[2]);
            Assert.AreEqual(2, notes.Length);
            StringAssert.Contains("HistoryReset", notes[1]);
        }
    }
}
