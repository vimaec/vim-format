using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Vim.Format.Merge;
using Vim.Format.ObjectModel;
using Vim.Format.SceneBuilder;
using Vim.LinqArray;
using Vim.Math3d;
using Vim.Util.Tests;

namespace Vim.Format.Tests
{
    /// <summary>
    /// VimPatchService, the file half of a differential update: a base file patched with a partial file of the same
    /// document. The partial holds a changed wall, a new element, its own temporary 3D view (aliased onto the base's export
    /// view) and the door as context; the base loses one deleted element.
    /// </summary>
    [TestFixture]
    public static class VimPatchServiceTests
    {
        private sealed class Doc
        {
            public readonly ObjectModelStore Store = new ObjectModelStore();
            public ObjectModelBuilder Omb => Store.ObjectModelBuilder;
            public readonly int View;
            public readonly ParameterDescriptor Mark;

            public Doc(string viewUid, string viewName, string projectName = "Patch", string path = "C:\\patch.rvt")
            {
                var bimDocument = new BimDocument { Name = projectName, Title = Path.GetFileNameWithoutExtension(path), PathName = path, Identity = "creation-guid" };
                var holder = Omb.ElementBuilder.Add(bimDocument.CreateParameterHolderElement()).Entity;
                bimDocument._Element.Index = holder.Index;
                holder._BimDocument.Index = Omb.BimDocumentBuilder.Add(bimDocument).Entity.Index;
                var viewElement = AddElement(1000, viewName, viewUid);
                View = Omb.ViewBuilder.Add(new View { Title = viewName, _Element = { Index = viewElement } }).Entity.Index;
                var unit = Omb.DisplayUnitBuilder.Add(new DisplayUnit { Spec = "text", Type = "text", Label = "text" }).Entity;
                Mark = Omb.ParameterDescriptorBuilder.Add(new ParameterDescriptor { Name = "Mark", Group = "Identity Data", _DisplayUnit = { Index = unit.Index } }).Entity;
            }

            public int AddElement(long id, string name, string uid)
                => Omb.ElementBuilder.Add(new Element { Id = id, Name = name, UniqueId = uid, ChangeToken = uid + "-v1", _BimDocument = { Index = 0 } }).Entity.Index;

            /// <summary>An element with a 1 ft triangle at x, a Mark parameter and an ElementInView row.</summary>
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

            public string Write(string dir, string name)
            {
                var path = Path.Combine(dir, name);
                Store.ToDocumentBuilder(nameof(VimPatchServiceTests), "0.0.0").Write(path);
                return path;
            }
        }

        [Test]
        public static void TestPatchReplacesAddsDeletesAndKeepsTheRest()
        {
            var ctx = new CallerTestContext();
            var dir = ctx.PrepareDirectory();

            // Base: a wall, a door hosted by the wall, an element deleted later.
            var b = new Doc("view-all", "VIM-ALL");
            var wall = b.AddBox(1, "wall", 0, "A");
            var door = b.AddBox(2, "door", 5, "D");
            b.Omb.FamilyInstanceBuilder.AddUntracked(new FamilyInstance { _Element = { Index = door }, _Host = { Index = wall } });
            b.AddBox(3, "old", 9, "O");
            var basePath = b.Write(dir, "base.vim");

            // Partial (exported through a temporary 3D view): the wall moved to x = 2 with Mark B, a new element, the door
            // as context.
            var p = new Doc("temp-view", "_VIM_EXPORT_1234");
            var pWall = p.AddBox(1, "wall", 2, "B");
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
                HeaderValues = new Dictionary<string, string> { { "test.value", "42" } },
            });

            Assert.AreEqual(2, result.Changed, "the wall, and the document's holder (Project Information is always refreshed)");
            Assert.AreEqual(1, result.Added);
            Assert.AreEqual(1, result.Deleted);
            Assert.IsEmpty(result.ChangedButNotInPartial);

            var vim = VimScene.LoadVim(outPath);
            vim.Validate();
            Assert.AreEqual("42", vim.Document.Header.Values["test.value"]);
            var dm = vim.DocumentModel;
            var uids = Enumerable.Range(0, dm.NumElement).Select(i => dm.GetElement(i).UniqueId).ToList();
            CollectionAssert.DoesNotContain(uids, "old");
            CollectionAssert.DoesNotContain(uids, "temp-view");
            Assert.AreEqual(1, uids.Count(u => u == "view-all"));
            var wallIndex = uids.IndexOf("wall");
            var doorIndex = uids.IndexOf("door");
            var newIndex = uids.IndexOf("new");
            Assert.AreEqual(wall, wallIndex, "a changed element keeps its index");
            Assert.AreEqual(1, dm.NumBimDocument, "one document, not one per file");

            // Parameters: the wall's replaced, the door's kept; one descriptor.
            var marks = Enumerable.Range(0, dm.NumParameter).Select(i => dm.GetParameter(i)).GroupBy(x => x.Element.UniqueId, x => x.Value).ToDictionary(x => x.Key, x => x.ToArray());
            CollectionAssert.AreEqual(new[] { "B" }, marks["wall"]);
            CollectionAssert.AreEqual(new[] { "D" }, marks["door"]);
            CollectionAssert.AreEqual(new[] { "N" }, marks["new"]);
            Assert.AreEqual(1, dm.NumParameterDescriptor);

            // Geometry: one node per element, the wall at its new place, aligned with the instances.
            var g = vim.Document.Geometry;
            Assert.AreEqual(dm.NumNode, g.InstanceTransforms.Count);
            var nodesOf = Enumerable.Range(0, dm.NumNode).GroupBy(i => dm.GetNode(i).Element.UniqueId).ToDictionary(x => x.Key, x => x.ToArray());
            Assert.AreEqual(2f, g.InstanceTransforms[nodesOf["wall"].Single()].Translation.X);
            Assert.AreEqual(5f, g.InstanceTransforms[nodesOf["door"].Single()].Translation.X);
            Assert.AreEqual(7f, g.InstanceTransforms[nodesOf["new"].Single()].Translation.X);

            // The door still hosted by the wall; the new element shown in the base's export view.
            var fi = Enumerable.Range(0, dm.NumFamilyInstance).Select(dm.GetFamilyInstance).Single();
            Assert.AreEqual(doorIndex, fi._Element.Index);
            Assert.AreEqual(wallIndex, fi._Host.Index);
            var eiv = Enumerable.Range(0, dm.NumElementInView).Select(dm.GetElementInView).ToList();
            Assert.AreEqual(1, dm.NumView);
            Assert.IsTrue(eiv.Any(x => x._Element.Index == newIndex && x.View.Title == "VIM-ALL"));
            Assert.AreEqual(1, eiv.Count(x => x._Element.Index == wallIndex));
        }

        [Test]
        public static void TestPatchKeysTheHostByDocumentNotByProjectNameOrPath()
        {
            var ctx = new CallerTestContext();
            var dir = ctx.PrepareDirectory();

            var b = new Doc("view-all", "VIM-ALL", "Tower", "C:\\a\\TowerS.rvt");
            b.AddBox(1, "wall", 0, "A");
            var basePath = b.Write(dir, "base.vim");

            // Exported from another local copy (other path, other title) after Project Information was renamed: the holder's
            // UniqueId (made from the project name) differs, the element is the same.
            var p = new Doc("temp-view", "_VIM_EXPORT_1", "Tower renamed", "D:\\b\\TowerS_sam.rvt");
            p.AddBox(1, "wall", 3, "B");
            var partialPath = p.Write(dir, "partial.vim");

            var outPath = Path.Combine(dir, "patched.vim");
            var result = VimPatchService.Patch(basePath, partialPath, outPath, new VimPatchOptions
            {
                ChangedUniqueIds = new HashSet<string> { "wall" },
                UniqueIdAliases = new Dictionary<string, string> { { "temp-view", "view-all" } },
            });

            Assert.AreEqual(1, result.HoldersMatched);
            var vim = VimScene.LoadVim(outPath);
            vim.Validate();
            var dm = vim.DocumentModel;
            Assert.AreEqual(1, dm.NumBimDocument);
            Assert.AreEqual("Tower renamed", dm.GetBimDocument(0).Name, "Project Information refreshed in place");
            Assert.AreEqual(1, Enumerable.Range(0, dm.NumElement).Count(i => dm.GetElement(i).UniqueId == "wall"));
            Assert.AreEqual(3, dm.NumElement, "the holder, the view and the wall: no element added");
        }
    }
}
