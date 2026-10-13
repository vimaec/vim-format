using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Vim.Format.Merge;
using Vim.Format.ObjectModel;
using Vim.Format.SceneBuilder;
using Vim.Math3d;
using Vim.Util.Tests;

namespace Vim.Format.Tests
{
    /// <summary>
    /// The four "Sync to VIM" defects found by the TowerS Diff series (2026-10-13), each as a base file patched with the
    /// partial file a sync writes: a renamed Project Information, a linked model re-exported whole, the warnings, and the
    /// rows an incremental export files under its temporary view.
    /// </summary>
    [TestFixture]
    public static class VimPatchSyncFixTests
    {
        private sealed class Doc
        {
            public readonly ObjectModelStore Store = new ObjectModelStore();
            public ObjectModelBuilder Omb => Store.ObjectModelBuilder;
            public int View = -1;
            public readonly int Unit;
            public readonly Dictionary<string, int> Docs = new Dictionary<string, int>();

            public Doc()
            {
                Unit = Omb.DisplayUnitBuilder.Add(new DisplayUnit { Spec = "text", Type = "text", Label = "text" }).Entity.Index;
            }

            /// <summary>A BimDocument with its parameter holder (UniqueId made from the project name, as the exporter does).</summary>
            public int AddDocument(string title, string projectName, bool linked = false, int parent = -1)
            {
                var bd = new BimDocument { Name = projectName, Title = title, PathName = "C:\\" + title + ".rvt", IsLinked = linked, _Parent = { Index = parent } };
                var holder = Omb.ElementBuilder.Add(bd.CreateParameterHolderElement()).Entity;
                var d = Omb.BimDocumentBuilder.Add(bd).Entity.Index;
                bd._Element.Index = holder.Index;
                holder._BimDocument.Index = d;
                Omb.ParameterBuilder.AddUntracked(new Parameter { Value = projectName, _ParameterDescriptor = { Index = Descriptor("Project Name", false) }, _Element = { Index = holder.Index } });
                Docs[title] = d;
                return d;
            }

            public int Descriptor(string name, bool readOnly)
                => Omb.ParameterDescriptorBuilder.GetOrAdd((name, readOnly), () => new ParameterDescriptor { Name = name, Group = "Data", IsReadOnly = readOnly, _DisplayUnit = { Index = Unit } }).Entity.Index;

            public int AddElement(long id, string uid, int doc, string type = "", string name = null)
                => Omb.ElementBuilder.Add(new Element { Id = id, Name = name ?? uid, UniqueId = uid, Type = type, _BimDocument = { Index = doc } }).Entity.Index;

            public int AddView(string uid, string title, int doc)
            {
                var e = AddElement(1000, uid, doc, "View3D", title);
                View = Omb.ViewBuilder.Add(new View { Title = title, _Element = { Index = e } }).Entity.Index;
                return e;
            }

            /// <summary>An element with a node, a parameter (descriptor "Mark", read-only or not), a shape in the view and a row in the view.</summary>
            public int AddBox(long id, string uid, int doc, float x, string mark, bool markReadOnly = false)
            {
                var e = AddElement(id, uid, doc);
                var mesh = Store.Meshes.Count;
                Store.Meshes.Add(new DocumentBuilder.Mesh(
                    new List<Vector3> { new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 1, 0) },
                    new List<int> { 0, 1, 2 }));
                Store.Instances.Add(new DocumentBuilder.Instance { Transform = Matrix4x4.CreateTranslation(x, 0, 0), MeshIndex = mesh, ParentIndex = -1 });
                Omb.NodeBuilder.AddUntracked(new Node { _Element = { Index = e } });
                Omb.ParameterBuilder.AddUntracked(new Parameter { Value = mark, _ParameterDescriptor = { Index = Descriptor("Mark", markReadOnly) }, _Element = { Index = e } });
                if (View >= 0)
                {
                    Omb.ElementInViewBuilder.AddUntracked(new ElementInView { _Element = { Index = e }, _View = { Index = View } });
                    Store.Shapes.Add(new DocumentBuilder.Shape { Vertices = new List<Vector3> { new Vector3(x, 0, 0), new Vector3(x, 1, 0) }, Color = new Vector4(0, 0, 0, 1), Width = 1 });
                    var shape = Omb.ShapeBuilder.AddUntracked(new Shape { _Element = { Index = e }, Kind = (int) ShapeKind.ModelCurve }).Index;
                    Omb.ShapeInViewBuilder.AddUntracked(new ShapeInView { _Shape = { Index = shape }, _View = { Index = View } });
                }
                return e;
            }

            public int AddWarning(int doc, string key, string description, int failing, params int[] elements)
            {
                var w = Omb.WarningBuilder.AddUntracked(new Warning { Key = key, Description = description, Severity = "Warning", FailingElementCount = failing, _BimDocument = { Index = doc } }).Index;
                foreach (var e in elements)
                    Omb.ElementInWarningBuilder.AddUntracked(new ElementInWarning { _Element = { Index = e }, _Warning = { Index = w } });
                return w;
            }

            public string Write(string dir, string name)
            {
                var path = Path.Combine(dir, name);
                Store.ToDocumentBuilder(nameof(VimPatchSyncFixTests), "0.0.0").Write(path);
                return path;
            }
        }

        private static List<string> Uids(DocumentModel dm) => Enumerable.Range(0, dm.NumElement).Select(i => dm.GetElement(i).UniqueId).ToList();

        /// <summary>
        /// Version 5: Project Information renamed. The holder's UniqueId is made from the project name, so the partial's
        /// holder matched nothing and the file got a second host document holding every re-exported element. Now the
        /// holder is matched by document: one document, its Name cell changed, nothing else moved.
        /// Also: the partial holds the real export view as well as its temporary view aliased onto it (an incremental
        /// export writes every view); the alias must win, or the new element loses its view row and every re-exported
        /// shape its ShapeInView (version 3).
        /// </summary>
        [Test]
        public static void TestRenamedProjectInformationChangesOnlyItsCells()
        {
            var dir = new CallerTestContext().PrepareDirectory();

            var b = new Doc();
            var bHost = b.AddDocument("Tower", "USF HEALTH");
            b.AddView("view-all", "VIM-ALL", bHost);
            var wall = b.AddBox(1, "wall", bHost, 0, "A");
            b.AddBox(2, "door", bHost, 5, "D");
            var basePath = b.Write(dir, "base.vim");

            var p = new Doc();
            var pHost = p.AddDocument("Tower", "Skanska Tower 2027");
            p.AddView("view-all", "VIM-ALL", pHost);          // the real export view, as context
            p.AddView("temp-view", "_VIM_EXPORT_1", pHost);    // the temporary view the partial was exported from
            p.AddBox(1, "wall", pHost, 2, "B");
            p.AddBox(4, "chair", pHost, 7, "N");
            var partialPath = p.Write(dir, "partial.vim");

            var outPath = Path.Combine(dir, "patched.vim");
            var result = VimPatchService.Patch(basePath, partialPath, outPath, new VimPatchOptions
            {
                ChangedUniqueIds = new HashSet<string> { "wall", "chair" },
                UniqueIdAliases = new Dictionary<string, string> { { "temp-view", "view-all" } },
                DocumentMatch = PatchDocumentMatch.HostAndLinkTitles,
            });
            Assert.AreEqual(1, result.HoldersMatched);

            var vim = VimScene.LoadVim(outPath);
            vim.Validate();
            var dm = vim.DocumentModel;
            Assert.AreEqual(1, dm.NumBimDocument, "one document, not two");
            Assert.AreEqual("Skanska Tower 2027", dm.GetBimDocument(0).Name, "the name cell is updated in place");
            var uids = Uids(dm);
            Assert.AreEqual("Skanska Tower 2027_BimDocument Parameter Holder", uids[0], "the holder keeps its row, its UniqueId follows the name");
            CollectionAssert.DoesNotContain(uids, "USF HEALTH_BimDocument Parameter Holder");
            Assert.AreEqual(wall, uids.IndexOf("wall"));
            Assert.IsTrue(Enumerable.Range(0, dm.NumElement).All(i => dm.GetElement(i)._BimDocument.Index == 0), "every element in the one document");
            var holderParams = Enumerable.Range(0, dm.NumParameter).Select(dm.GetParameter).Where(x => x._Element.Index == 0).Select(x => x.Value).ToList();
            CollectionAssert.AreEqual(new[] { "Skanska Tower 2027" }, holderParams, "Project Information parameters replaced, not doubled");

            // The temporary view landed on the export view.
            Assert.AreEqual(1, dm.NumView);
            var chair = uids.IndexOf("chair");
            Assert.IsTrue(Enumerable.Range(0, dm.NumElementInView).Select(dm.GetElementInView).Any(x => x._Element.Index == chair && x._View.Index == 0), "the new element is in the export view");
            var shapeViews = Enumerable.Range(0, dm.NumShapeInView).Select(dm.GetShapeInView).Select(x => dm.GetShape(x._Shape.Index)._Element.Index).ToList();
            CollectionAssert.Contains(shapeViews, uids.IndexOf("wall"), "the re-exported shape keeps its ShapeInView");
            CollectionAssert.Contains(shapeViews, chair);
        }

        /// <summary>
        /// Version 1: a linked model re-exported whole. (1) The exporter writes one row per system family ("Basic Wall")
        /// for the whole export, under the first document that needs it - the host in a full export, the link in a
        /// partial; it was appended as a second row. (2) A parameter descriptor's IsReadOnly comes from the first element
        /// met; the partial met another one and the descriptor was appended. (3) The link's levels keep their building.
        /// </summary>
        [Test]
        public static void TestLinkReplacedWholeMatchesSharedRowsAndDescriptors()
        {
            var dir = new CallerTestContext().PrepareDirectory();

            Doc Model(bool partial)
            {
                var d = new Doc();
                var host = d.AddDocument("Tower", "USF HEALTH");
                d.AddView("view-all", "VIM-ALL", host);
                var link = d.AddDocument("Struct", "USF STRUCT", true, host);
                // Full export: the shared row under the host. Partial (host not re-exported): under the link.
                var system = d.AddElement(-1, "Basic Wall_System Family", partial ? link : host, VimConstants.SystemFamilyElementType, "Basic Wall");
                var family = d.Omb.FamilyBuilder.AddUntracked(new Family { _Element = { Index = system } }).Index;
                if (!partial)
                {
                    var hostType = d.AddElement(50, "host-type", host, "WallType");
                    d.Omb.FamilyTypeBuilder.AddUntracked(new FamilyType { _Family = { Index = family }, _Element = { Index = hostType } });
                    d.AddBox(1, "host-wall", host, 0, "H");
                }
                var linkType = d.AddElement(60, "link-type", link, "WallType");
                d.Omb.FamilyTypeBuilder.AddUntracked(new FamilyType { _Family = { Index = family }, _Element = { Index = linkType } });
                d.AddBox(70, "link-wall", link, 3, partial ? "L2" : "L", markReadOnly: partial);
                var buildingHolder = d.AddElement(-1, "USF STRUCT (Building)_Building Parameter Holder", link, VimConstants.BuildingParameterHolderElementType);
                var building = d.Omb.BuildingBuilder.AddUntracked(new Building { _Element = { Index = buildingHolder } }).Index;
                var level = d.AddElement(80, "link-level", link, "Level");
                d.Omb.LevelBuilder.AddUntracked(new Level { Elevation = 10, _Building = { Index = building }, _Element = { Index = level } });
                return d;
            }

            var basePath = Model(false).Write(dir, "base.vim");
            var partialPath = Model(true).Write(dir, "partial.vim");
            var outPath = Path.Combine(dir, "patched.vim");
            VimPatchService.Patch(basePath, partialPath, outPath, new VimPatchOptions
            {
                ChangedUniqueIds = new HashSet<string>(),
                ReplacedDocumentTitles = new HashSet<string> { "Struct" },
                DocumentMatch = PatchDocumentMatch.HostAndLinkTitles,
            });

            var vim = VimScene.LoadVim(outPath);
            vim.Validate();
            var dm = vim.DocumentModel;
            var uids = Uids(dm);
            Assert.AreEqual(1, uids.Count(u => u == "Basic Wall_System Family"), "one shared row");
            var system = uids.IndexOf("Basic Wall_System Family");
            Assert.AreEqual(0, dm.GetElement(system)._BimDocument.Index, "still filed under the host, as a full export does");
            Assert.AreEqual(1, dm.NumFamily);
            var linkType = Enumerable.Range(0, dm.NumFamilyType).Select(dm.GetFamilyType).Single(t => t.Element.UniqueId == "link-type");
            Assert.AreEqual(system, dm.GetFamily(linkType._Family.Index)._Element.Index);

            Assert.AreEqual(2, dm.NumParameterDescriptor, "Project Name + Mark: the partial's read-only twin is not added");
            var linkWallMark = Enumerable.Range(0, dm.NumParameter).Select(dm.GetParameter).Single(x => x.Element.UniqueId == "link-wall");
            Assert.AreEqual("L2", linkWallMark.Value, "the value is the partial's");
            Assert.IsFalse(linkWallMark.ParameterDescriptor.IsReadOnly, "the descriptor is the base's");

            var level = Enumerable.Range(0, dm.NumLevel).Select(dm.GetLevel).Single();
            Assert.GreaterOrEqual(level._Building.Index, 0, "the link's level keeps its building");
            Assert.AreEqual(1, uids.Count(u => u == "USF STRUCT (Building)_Building Parameter Holder"));
        }

        /// <summary>
        /// Warnings. With the partial carrying Revit's whole list for the host: a warning still listed keeps its row (and
        /// the rows of the elements the sync did not touch), one Revit no longer lists goes, a new one comes, one naming
        /// nothing exported is not written. Without that option, a renamed document no longer doubles them (version 5:
        /// 27 extra rows, all under the phantom second document).
        /// </summary>
        [Test]
        public static void TestWarningsAreReplacedFromRevitsList()
        {
            var dir = new CallerTestContext().PrepareDirectory();

            var b = new Doc();
            var bHost = b.AddDocument("Tower", "USF HEALTH");
            b.AddView("view-all", "VIM-ALL", bHost);
            var wall = b.AddBox(1, "wall", bHost, 0, "A");
            var door = b.AddBox(2, "door", bHost, 5, "D");
            var room = b.AddBox(3, "room", bHost, 9, "R");
            b.AddWarning(bHost, "k1", "overlap", 2, wall, door);
            b.AddWarning(bHost, "k2", "resolved since", 1, room);
            var basePath = b.Write(dir, "base.vim");

            var p = new Doc();
            var pHost = p.AddDocument("Tower", "Skanska Tower 2027");
            p.AddView("temp-view", "_VIM_EXPORT_1", pHost);
            var pWall = p.AddBox(1, "wall", pHost, 2, "B");
            p.AddWarning(pHost, "k1", "overlap", 2, pWall);           // still listed; the door is not in the partial
            p.AddWarning(pHost, "k3", "new one", 1, pWall);
            p.AddWarning(pHost, "k4", "room tag outside its room", 1); // names nothing exported
            var partialPath = p.Write(dir, "partial.vim");

            VimPatchOptions Options(bool replace) => new VimPatchOptions
            {
                ChangedUniqueIds = new HashSet<string> { "wall" },
                UniqueIdAliases = new Dictionary<string, string> { { "temp-view", "view-all" } },
                DocumentMatch = PatchDocumentMatch.HostAndLinkTitles,
                ReplaceHostWarnings = replace,
            };

            var replaced = Path.Combine(dir, "replaced.vim");
            VimPatchService.Patch(basePath, partialPath, replaced, Options(true));
            var dm = VimScene.LoadVim(replaced).DocumentModel;
            VimScene.LoadVim(replaced).Validate();
            var keys = Enumerable.Range(0, dm.NumWarning).Select(i => dm.GetWarning(i).Key).ToList();
            CollectionAssert.AreEquivalent(new[] { "k1", "k3" }, keys);
            var named = Enumerable.Range(0, dm.NumElementInWarning).Select(dm.GetElementInWarning)
                .Select(x => (x.Warning.Key, x.Element.UniqueId)).ToList();
            CollectionAssert.AreEquivalent(new[] { ("k1", "wall"), ("k1", "door"), ("k3", "wall") }, named);

            // The old rule, with the document fix: k1 is not doubled (k2 stays, k4 is added as before - the old rule
            // never removes anything; the exporter passes ReplaceHostWarnings).
            var kept = Path.Combine(dir, "kept.vim");
            VimPatchService.Patch(basePath, partialPath, kept, Options(false));
            var dm2 = VimScene.LoadVim(kept).DocumentModel;
            Assert.AreEqual(1, Enumerable.Range(0, dm2.NumWarning).Count(i => dm2.GetWarning(i).Key == "k1"), "one row per warning");
            Assert.AreEqual(1, dm2.NumBimDocument);
        }
    }
}
