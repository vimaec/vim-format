using System.IO;
using System.Linq;
using NUnit.Framework;
using Vim.Format.ObjectModel;
using Vim.Format.SceneBuilder;
using Vim.LinqArray;
using Vim.Math3d;
using Vim.Util.Tests;

namespace Vim.Format.Tests
{
    /// <summary>
    /// Object model 5.9.0: Shape.Kind (what a polyline draws) and Element.LocationSource (where Element.Location comes from)
    /// round-trip through a VIM file, and the shape polylines stay in the G3D shape buffers in Shape row order.
    /// </summary>
    [TestFixture]
    public static class DrawingCurveEntityTests
    {
        [Test]
        public static void TestShapeKindAndLocationSourceRoundTrip()
        {
            var ctx = new CallerTestContext();
            var dir = ctx.PrepareDirectory();

            var store = new ObjectModelStore();
            var omb = store.ObjectModelBuilder;

            var bimDocument = new BimDocument { Name = "Drawing curves", Title = "Drawing curves" };
            var bimDocumentElement = omb.ElementBuilder.Add(bimDocument.CreateParameterHolderElement()).Entity;
            bimDocument._Element.Index = bimDocumentElement.Index;
            omb.BimDocumentBuilder.Add(bimDocument);

            var door = omb.ElementBuilder.Add(new Element
            {
                Id = 30, Name = "Door", UniqueId = "door",
                Location = new Vector3(3, -26, 0),
                LocationSource = (int) ElementLocationSource.LocationPoint,
            }).Entity;
            var empty = omb.ElementBuilder.Add(new Element
            {
                Id = 31, Name = "Empty family", UniqueId = "empty",
                Location = new Vector3(19, -26, 0),
                LocationSource = (int) ElementLocationSource.InstanceOrigin,
            }).Entity;
            var type = omb.ElementBuilder.Add(new Element { Id = 32, Name = "A type", UniqueId = "type", LocationSource = (int) ElementLocationSource.None }).Entity;

            // A door swing (symbolic arc, 3 points) and a void edge (2 points) on the door.
            store.Shapes.Add(new DocumentBuilder.Shape { Vertices = new[] { new Vector3(2, -27, 0), new Vector3(2.7f, -26.7f, 0), new Vector3(3, -26, 0) }.ToList(), Color = new Vector4(0, 0, 0, 1), Width = 1 });
            omb.ShapeBuilder.AddUntracked(new Shape { _Element = { Index = door.Index }, Kind = (int) ShapeKind.SymbolicCurve });
            store.Shapes.Add(new DocumentBuilder.Shape { Vertices = new[] { new Vector3(2, -26, 0), new Vector3(2, -26, 2) }.ToList(), Color = new Vector4(0, 0, 0, 1), Width = 1 });
            omb.ShapeBuilder.AddUntracked(new Shape { _Element = { Index = door.Index }, Kind = (int) ShapeKind.VoidEdge });

            var path = Path.Combine(dir, "drawing-curves.vim");
            store.ToDocumentBuilder(nameof(DrawingCurveEntityTests), "0.0.0").Write(path);

            var vim = VimScene.LoadVim(path);
            vim.Validate();
            var dm = vim.DocumentModel;

            Assert.AreEqual(SchemaVersion.Current.ToString(), vim.Document.Header.Schema.ToString());
            Assert.AreEqual(ElementLocationSource.LocationPoint, dm.GetElement(door.Index).GetLocationSource());
            Assert.AreEqual(ElementLocationSource.InstanceOrigin, dm.GetElement(empty.Index).GetLocationSource());
            Assert.AreEqual(new Vector3(19, -26, 0), dm.GetElement(empty.Index).Location);
            Assert.AreEqual(ElementLocationSource.None, dm.GetElement(type.Index).GetLocationSource());

            Assert.AreEqual(2, dm.NumShape);
            Assert.AreEqual(ShapeKind.SymbolicCurve, dm.GetShape(0).GetKind());
            Assert.AreEqual(ShapeKind.VoidEdge, dm.GetShape(1).GetKind());
            Assert.AreEqual("door", dm.GetShape(1).Element.UniqueId);

            var g = vim.Document.Geometry;
            Assert.AreEqual(2, g.ShapeVertexOffsets.Count);
            Assert.AreEqual(5, g.ShapeVertices.Count);
            Assert.AreEqual(3, g.ShapeVertexOffsets[1]);
            Assert.AreEqual(new Vector3(2, -26, 2), g.ShapeVertices[4]);
        }
    }
}
