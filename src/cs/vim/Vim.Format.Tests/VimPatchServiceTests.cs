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

            public Doc(string viewUid, string viewName)
            {
                var bimDocument = new BimDocument { Name = "Patch", Title = "Patch", PathName = "C:\\patch.rvt" };
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
    }
}
