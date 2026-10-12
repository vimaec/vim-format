using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Vim.Format.Activity;
using Vim.Format.Merge;
using Vim.Format.ObjectModel;
using Vim.Format.SceneBuilder;
using Vim.LinqArray;
using Vim.Math3d;
using Vim.Util.Tests;

namespace Vim.Format.Tests
{
    /// <summary>
    /// VimPatchService: a base file patched with a partial file of the same document. The partial holds a changed wall,
    /// a new element, its own temporary 3D view (aliased onto the base's export view) and the door as context; the base
    /// loses one deleted element. End to end on TowerS: C:\VimFlexLocal\work\incremental\check (PatchCheck run).
    /// </summary>
    [TestFixture]
    public static class VimPatchServiceTests
    {
        private sealed class Doc
        {
            public ObjectModelStore Store = new ObjectModelStore();
            public ObjectModelBuilder Omb => Store.ObjectModelBuilder;
            public int View;
            public ParameterDescriptor Mark;

            public Doc(string viewUid, string viewName, string title = "Patch", string pathName = "C:\\patch.rvt")
            {
                // Name is the project name (the same in every local file of a model); Title and PathName are the file's.
                var bimDocument = new BimDocument { Name = "Patch", Title = title, PathName = pathName };
                var holder = Omb.ElementBuilder.Add(bimDocument.CreateParameterHolderElement()).Entity;
                bimDocument._Element.Index = holder.Index;
                Omb.BimDocumentBuilder.Add(bimDocument);
                var viewElement = AddElement(1000, viewName, viewUid);
                View = Omb.ViewBuilder.Add(new View { Title = viewName, _Element = { Index = viewElement } }).Entity.Index;
                var unit = Omb.DisplayUnitBuilder.Add(new DisplayUnit { Spec = "text", Type = "text", Label = "text" }).Entity;
                Mark = Omb.ParameterDescriptorBuilder.Add(new ParameterDescriptor { Name = "Mark", Group = "Identity Data", _DisplayUnit = { Index = unit.Index } }).Entity;
            }

            public int AddElement(long id, string name, string uid)
                => Omb.ElementBuilder.Add(new Element { Id = id, Name = name, UniqueId = uid, _BimDocument = { Index = 0 } }).Entity.Index;

            /// <summary>An element with a 1 ft box at x, a Mark parameter and an ElementInView row.</summary>
            public int AddBox(long id, string uid, float x, string mark)
            {
                var e = AddElement(id, uid, uid);
                var mesh = Store.Meshes.Count;
                Store.Meshes.Add(new DocumentBuilder.Mesh(
                    new List<Vector3> { new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 1, 0) },
                    new List<int> { 0, 1, 2 }));
                Store.Instances.Add(new DocumentBuilder.Instance { Transform = Matrix4x4.CreateTranslation(x, 0, 0), MeshIndex = mesh, ParentIndex = -1 });
                Omb.NodeBuilder.AddUntracked(new Node { _Element = { Index = e } });
                Omb.ParameterBuilder.AddUntracked(new Parameter { Value = mark, _ParameterDescriptor = { Index = Mark.Index }, _Element = { Index = e } });
                Omb.ElementInViewBuilder.AddUntracked(new ElementInView { _Element = { Index = e }, _View = { Index = View } });
                return e;
            }

            public void AddShape(int element, ShapeKind kind, float x)
            {
                Store.Shapes.Add(new DocumentBuilder.Shape { Vertices = new List<Vector3> { new Vector3(x, 0, 0), new Vector3(x, 1, 0) }, Color = new Vector4(0, 0, 0, 1), Width = 1 });
                Omb.ShapeBuilder.AddUntracked(new Shape { _Element = { Index = element }, Kind = (int)kind });
            }

            public string Write(string dir, string name, ActivityLog activity = null)
            {
                var path = Path.Combine(dir, name);
                var db = Store.ToDocumentBuilder(nameof(VimPatchServiceTests), "0.0.0");
                activity?.WriteInto(db, 0);
                db.Write(path);
                return path;
            }
        }

        /// <summary>One watched change, as the Revit exporter records it.</summary>
        private static ActivityChange Watched(string person, string session, string utc, ElementChangeKind kind, string uid, string category)
            => new ActivityChange
            {
                ChangedUtc = utc,
                SessionGuid = session,
                Contributor = new ActivityContributor(person, ContributorSource.Session),
                Kind = kind,
                Confidence = ElementChangeConfidence.Observed,
                UniqueId = uid,
                CategoryName = category,
            };

        private static ActivitySession SessionOf(string person, string guid, string started, string ended)
            => new ActivitySession
            {
                SessionGuid = guid,
                MachineName = "DESK-1",
                RevitVersion = "2027",
                StartedUtc = started,
                EndedUtc = ended,
                Contributor = new ActivityContributor(person, ContributorSource.Session),
            };

        [Test]
        public static void TestPatchReplacesAddsDeletesAndKeepsTheRest()
        {
            var ctx = new CallerTestContext();
            var dir = ctx.PrepareDirectory();

            // Base: wall (with 2D linework), door hosted by the wall (with a door swing), an element deleted later.
            var b = new Doc("view-all", "VIM-ALL");
            var wall = b.AddBox(1, "wall", 0, "A");
            b.AddShape(wall, ShapeKind.ViewLinework, 0);
            var door = b.AddBox(2, "door", 5, "D");
            b.AddShape(door, ShapeKind.SymbolicCurve, 5);
            b.Omb.FamilyInstanceBuilder.AddUntracked(new FamilyInstance { _Element = { Index = door }, _Host = { Index = wall } });
            b.AddBox(3, "old", 9, "O");
            var basePath = b.Write(dir, "base.vim");

            // Partial (exported from a temporary 3D view): the wall moved to x = 2 with Mark B, a new element, the door as context.
            var p = new Doc("temp-view", "_VIM_EXPORT_1234");
            var pWall = p.AddBox(1, "wall", 2, "B");
            p.AddShape(pWall, ShapeKind.ViewLinework, 99); // 2D linework of a 3D-only partial is not trusted
            var pDoor = p.AddBox(2, "door", 5, "D");
            p.Omb.FamilyInstanceBuilder.AddUntracked(new FamilyInstance { _Element = { Index = pDoor }, _Host = { Index = pWall } });
            p.AddBox(4, "new", 7, "N");
            var partialPath = p.Write(dir, "partial.vim");

            var outPath = Path.Combine(dir, "patched.vim");
            var result = VimPatchService.Patch(basePath, partialPath, outPath, new VimPatchOptions
            {
                ChangedUniqueIds = new HashSet<string> { "wall", "new" },
                DeletedUniqueIds = new HashSet<string> { "old" },
                UniqueIdAliases = new Dictionary<string, string> { { "temp-view", "view-all" } },
            });

            Assert.AreEqual(1, result.Changed);
            Assert.AreEqual(1, result.Added);
            Assert.AreEqual(1, result.Deleted);

            var vim = VimScene.LoadVim(outPath);
            vim.Validate();
            var dm = vim.DocumentModel;
            var uids = Enumerable.Range(0, dm.NumElement).Select(i => dm.GetElement(i).UniqueId).ToList();
            CollectionAssert.DoesNotContain(uids, "old");
            CollectionAssert.DoesNotContain(uids, "temp-view");
            Assert.AreEqual(1, uids.Count(u => u == "view-all"));
            var wallIndex = uids.IndexOf("wall");
            var doorIndex = uids.IndexOf("door");
            var newIndex = uids.IndexOf("new");
            Assert.AreEqual(wall, wallIndex, "a changed element keeps its index");

            // Parameters: the wall's replaced, the door's kept.
            var marks = Enumerable.Range(0, dm.NumParameter).Select(i => dm.GetParameter(i)).GroupBy(x => x.Element.UniqueId, x => x.Value).ToDictionary(x => x.Key, x => x.ToArray());
            CollectionAssert.AreEqual(new[] { "B" }, marks["wall"]);
            CollectionAssert.AreEqual(new[] { "D" }, marks["door"]);
            CollectionAssert.AreEqual(new[] { "N" }, marks["new"]);

            // Geometry: one node per element, the wall at its new place, aligned with the instances.
            var g = vim.Document.Geometry;
            Assert.AreEqual(dm.NumNode, g.InstanceTransforms.Count);
            var nodesOf = Enumerable.Range(0, dm.NumNode).GroupBy(i => dm.GetNode(i).Element.UniqueId).ToDictionary(x => x.Key, x => x.ToArray());
            Assert.AreEqual(1, nodesOf["wall"].Count());
            Assert.AreEqual(2f, g.InstanceTransforms[nodesOf["wall"].Single()].Translation.X);
            Assert.AreEqual(5f, g.InstanceTransforms[nodesOf["door"].Single()].Translation.X);

            // Shapes: the wall keeps its base 2D linework (x = 0), the partial's is not written; the door keeps its swing.
            var shapes = Enumerable.Range(0, dm.NumShape).Select(i => (Uid: dm.GetShape(i).Element.UniqueId, Kind: dm.GetShape(i).GetKind(), X: g.ShapeVertices[g.ShapeVertexOffsets[i]].X)).ToList();
            CollectionAssert.AreEquivalent(new[] { ("wall", ShapeKind.ViewLinework, 0f), ("door", ShapeKind.SymbolicCurve, 5f) }, shapes);

            // The door still hosted by the wall; the new element visible in the base's export view.
            var fi = Enumerable.Range(0, dm.NumFamilyInstance).Select(dm.GetFamilyInstance).Single();
            Assert.AreEqual(doorIndex, fi._Element.Index);
            Assert.AreEqual(wallIndex, fi._Host.Index);
            var eiv = Enumerable.Range(0, dm.NumElementInView).Select(dm.GetElementInView).ToList();
            Assert.AreEqual(1, dm.NumView);
            Assert.IsTrue(eiv.Any(x => x._Element.Index == newIndex && x.View.Title == "VIM-ALL"));
            Assert.AreEqual(1, eiv.Count(x => x._Element.Index == wallIndex));

            // Provenance: the wall and the new element come from the partial export, the door from the base export.
            var revisionOf = Enumerable.Range(0, dm.NumElementInExportRevision).Select(dm.GetElementInExportRevision)
                .ToDictionary(x => x.Element.UniqueId, x => x.ExportRevision.GetKind());
            Assert.AreEqual(ExportRevisionKind.PartialExport, revisionOf["wall"]);
            Assert.AreEqual(ExportRevisionKind.PartialExport, revisionOf["new"]);
            Assert.AreEqual(ExportRevisionKind.FullExport, revisionOf["door"]);
        }

        /// <summary>
        /// A sync from another local file of the same central: its host document has another Title and PathName.
        /// Matched by title and path, every element of the partial was a new element next to the old one, which stayed
        /// stale (Sync to VIM, 2026-10-12); with <see cref="PatchDocumentMatch.HostAndLinkTitles"/> the host is the host.
        /// </summary>
        [Test]
        public static void TestPatchFromAnotherLocalReplacesTheHostRows()
        {
            var ctx = new CallerTestContext();
            var dir = ctx.PrepareDirectory();

            var b = new Doc("view-all", "VIM-ALL", "Model", "C:\\locals\\Model_sam.rvt");
            b.AddBox(1, "wall", 0, "A");
            b.AddBox(2, "door", 5, "D");
            var basePath = b.Write(dir, "base.vim");

            var p = new Doc("temp-view", "_VIM_EXPORT_1234", "Model_jo", "C:\\locals\\Model_jo.rvt");
            p.AddBox(1, "wall", 2, "B");
            var partialPath = p.Write(dir, "partial.vim");

            VimPatchOptions Options(PatchDocumentMatch match) => new VimPatchOptions
            {
                ChangedUniqueIds = new HashSet<string> { "wall" },
                UniqueIdAliases = new Dictionary<string, string> { { "temp-view", "view-all" } },
                DocumentMatch = match,
            };

            // The old matching: the wall is "added" and the base's wall reported as not re-exported.
            var old = VimPatchService.Patch(basePath, partialPath, Path.Combine(dir, "patched-old.vim"), Options(PatchDocumentMatch.TitleAndPath));
            Assert.AreEqual(1, old.ChangedButNotInPartial.Count);

            var outPath = Path.Combine(dir, "patched.vim");
            var result = VimPatchService.Patch(basePath, partialPath, outPath, Options(PatchDocumentMatch.HostAndLinkTitles));
            Assert.AreEqual(1, result.Changed);
            Assert.AreEqual(0, result.Added);
            Assert.AreEqual(0, result.ChangedButNotInPartial.Count);

            var vim = VimScene.LoadVim(outPath);
            vim.Validate();
            var dm = vim.DocumentModel;
            Assert.AreEqual(1, dm.NumBimDocument, "one host document");
            Assert.AreEqual("Model", dm.GetBimDocument(0).Title, "the base's document row is kept");
            var uids = Enumerable.Range(0, dm.NumElement).Select(i => dm.GetElement(i).UniqueId).ToList();
            Assert.AreEqual(1, uids.Count(u => u == "wall"));
            var marks = Enumerable.Range(0, dm.NumParameter).Select(i => dm.GetParameter(i)).ToDictionary(x => x.Element.UniqueId, x => x.Value);
            Assert.AreEqual("B", marks["wall"]);
            Assert.AreEqual("D", marks["door"]);
        }

        /// <summary>
        /// The activity log is history: two syncs in a row add their rows to the ones already in the file, the open
        /// session is one row with a moving end rather than one row per sync, and a change about an element that has
        /// just been deleted survives - that row is the whole point of the feature.
        /// </summary>
        [Test]
        public static void TestPatchAccumulatesTheActivityLog()
        {
            var ctx = new CallerTestContext();
            var dir = ctx.PrepareDirectory();
            const string s1 = "11111111-1111-1111-1111-111111111111";
            const string s2 = "22222222-2222-2222-2222-222222222222";

            // The base: a full export which already recorded two of Sam's changes.
            var b = new Doc("view-all", "VIM-ALL");
            b.AddBox(1, "wall", 0, "A");
            b.AddBox(2, "door", 5, "D");
            b.AddBox(3, "old", 9, "O");
            var baseLog = new ActivityLog
            {
                Sessions = { SessionOf("s.arsenault", s1, "2026-10-12T08:00:00.0000000Z", "2026-10-12T09:00:00.0000000Z") },
                Changes =
                {
                    Watched("s.arsenault", s1, "2026-10-12T08:10:00.0000000Z", ElementChangeKind.Added, "wall", "Walls"),
                    Watched("s.arsenault", s1, "2026-10-12T08:20:00.0000000Z", ElementChangeKind.Added, "door", "Doors"),
                },
            };
            var basePath = b.Write(dir, "base.vim", baseLog);

            // The first sync: the same session, still open (a later end), one modified wall and one deleted element -
            // the deleted one has no Element row to point at, in this file or any other.
            var p = new Doc("temp-view", "_VIM_EXPORT_1234");
            p.AddBox(1, "wall", 2, "B");
            var delta1 = new ActivityLog
            {
                Sessions = { SessionOf("s.arsenault", s1, "2026-10-12T08:00:00.0000000Z", "2026-10-12T10:30:00.0000000Z") },
                Changes =
                {
                    Watched("s.arsenault", s1, "2026-10-12T10:05:00.0000000Z", ElementChangeKind.Modified, "wall", "Walls"),
                    Watched("s.arsenault", s1, "2026-10-12T10:20:00.0000000Z", ElementChangeKind.Deleted, "old", "Generic Models"),
                },
                Notes =
                {
                    new ActivityNote
                    {
                        OccurredUtc = "2026-10-12T10:30:00.0000000Z",
                        Kind = ActivityMarkerKind.HistoryReset,
                        Description = "This model looks like it was re-saved as a new central.",
                    },
                },
            };
            var once = Path.Combine(dir, "patched-1.vim");
            VimPatchService.Patch(basePath, p.Write(dir, "partial-1.vim", delta1), once, new VimPatchOptions
            {
                ChangedUniqueIds = new HashSet<string> { "wall" },
                DeletedUniqueIds = new HashSet<string> { "old" },
                UniqueIdAliases = new Dictionary<string, string> { { "temp-view", "view-all" } },
            });

            var afterOne = ActivityReport.Read(once);
            Assert.AreEqual(4, afterOne.Changes.Count, "the base's two rows plus this sync's two");
            Assert.AreEqual(1, afterOne.Sessions.Count, "one session, not one per sync");
            Assert.AreEqual("2026-10-12T10:30:00.0000000Z", afterOne.Sessions[0].EndedUtc, "the fresher reading of the session wins");
            var deletedRow = afterOne.Changes.Single(c => c.Kind == ElementChangeKind.Deleted);
            Assert.AreEqual("old", deletedRow.UniqueId);
            Assert.AreEqual("Generic Models", deletedRow.Category, "a deletion still says what it was");
            CollectionAssert.DoesNotContain(
                Enumerable.Range(0, VimScene.LoadVim(once).DocumentModel.NumElement)
                    .Select(i => VimScene.LoadVim(once).DocumentModel.GetElement(i).UniqueId).ToList(),
                "old");
            Assert.AreEqual(1, afterOne.Markers.Count);
            Assert.AreEqual(ActivityMarkerKind.HistoryReset, afterOne.Markers[0].Kind);

            // A second sync, by somebody else, in a second session.
            var p2 = new Doc("temp-view-2", "_VIM_EXPORT_5678");
            p2.AddBox(2, "door", 6, "D2");
            var delta2 = new ActivityLog
            {
                Sessions = { SessionOf("j.tremblay", s2, "2026-10-12T11:00:00.0000000Z", "2026-10-12T11:40:00.0000000Z") },
                Changes = { Watched("j.tremblay", s2, "2026-10-12T11:30:00.0000000Z", ElementChangeKind.Modified, "door", "Doors") },
            };
            var twice = Path.Combine(dir, "patched-2.vim");
            VimPatchService.Patch(once, p2.Write(dir, "partial-2.vim", delta2), twice, new VimPatchOptions
            {
                ChangedUniqueIds = new HashSet<string> { "door" },
                UniqueIdAliases = new Dictionary<string, string> { { "temp-view-2", "view-all" } },
            });

            VimScene.LoadVim(twice).Validate();
            var afterTwo = ActivityReport.Read(twice);
            Assert.AreEqual(5, afterTwo.Changes.Count);
            Assert.AreEqual(2, afterTwo.Sessions.Count);
            CollectionAssert.AreEquivalent(new[] { "s.arsenault", "j.tremblay" }, afterTwo.Sessions.Select(x => x.Person).ToList());
            Assert.AreEqual(1, afterTwo.Markers.Count, "the marker written by the first sync is still there");
            Assert.IsTrue(afterTwo.Changes.All(c => c.VimRevision.Length > 0), "every row names the export that wrote it");

            // The billing summary: one row per person per day per category.
            var summary = ActivityReport.Summarize(afterTwo);
            Assert.AreEqual(1, summary.Count(r => r.Person == "j.tremblay"));
            var sam = summary.Where(r => r.Person == "s.arsenault").ToList();
            Assert.AreEqual(2, sam.Sum(r => r.Added));
            Assert.AreEqual(1, sam.Sum(r => r.Changed));
            Assert.AreEqual(1, sam.Sum(r => r.Deleted));
            Assert.AreEqual(2.5, sam.First().HoursInModelOnThatDay, 0.01, "08:00 to 10:30");

            // Retention, and "strip activity on export".
            var trimmed = Path.Combine(dir, "patched-trimmed.vim");
            var trimResult = VimPatchService.Patch(once, p2.Write(dir, "partial-3.vim", delta2), trimmed, new VimPatchOptions
            {
                ChangedUniqueIds = new HashSet<string> { "door" },
                UniqueIdAliases = new Dictionary<string, string> { { "temp-view-2", "view-all" } },
                ActivityOldestUtc = "2026-10-12T10:00:00.0000000Z",
            });
            Assert.AreEqual(2, trimResult.ActivityRowsTrimmed, "the two 08:xx rows are older than the cut-off");
            Assert.AreEqual(3, ActivityReport.Read(trimmed).Changes.Count);

            var stripped = Path.Combine(dir, "patched-stripped.vim");
            VimPatchService.Patch(once, p2.Write(dir, "partial-4.vim", delta2), stripped, new VimPatchOptions
            {
                ChangedUniqueIds = new HashSet<string> { "door" },
                UniqueIdAliases = new Dictionary<string, string> { { "temp-view-2", "view-all" } },
                KeepActivityLog = false,
            });
            Assert.IsTrue(ActivityReport.Read(stripped).IsEmpty, "nothing of the log is left in a stripped file");
            VimScene.LoadVim(stripped).Validate();
        }
    }
}
