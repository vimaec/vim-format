using System;
using System.Linq;
using NUnit.Framework;
using Vim.Format.Geometry;
using Vim.G3d;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim.Format.Tests.Geometry
{
    public static class GeometryTests
    {
        public static IMesh XYTriangle = new[] { new Vector3(0f, 0f, 0f), new Vector3(0f, 1f, 0f), new Vector3(1f, 0f, 0f) }.ToIArray().TriMesh(3.Range());
        public static IMesh XYQuad = new[] { new Vector3(0f, 0f, 0f), new Vector3(0f, 1f, 0f), new Vector3(1f, 1f, 0f), new Vector3(1f, 0f, 0f) }.ToIArray().QuadMesh(4.Range());
        public static IMesh XYQuadFromFunc = Primitives.QuadMesh(uv => uv.ToVector3(), 1, 1);
        public static IMesh XYQuad2x2 = Primitives.QuadMesh(uv => uv.ToVector3(), 2, 2);
        public static IMesh XYTriangleTwice = XYTriangle.Merge(XYTriangle.Translate(new Vector3(1, 0, 0)));

        public static readonly Vector3[] TestTetrahedronVertices = { Vector3.Zero, Vector3.UnitX, Vector3.UnitY, Vector3.UnitZ };
        public static readonly int[] TestTetrahedronIndices = { 0, 1, 2, 0, 3, 1, 1, 3, 2, 2, 3, 0 };

        public static IMesh Tetrahedron =
            TestTetrahedronVertices.ToIArray().TriMesh(TestTetrahedronIndices.ToIArray());

        public static IMesh Torus = Primitives.Torus(10, 0.2f, 10, 24);

        static IMesh RevolvedVerticalCylinder(float height, float radius, int verticalSegments, int radialSegments)
            => (Vector3.UnitZ * height).ToLine().Interpolate(verticalSegments).Add(-radius.AlongX()).RevolveAroundAxis(Vector3.UnitZ, radialSegments);

        public static IMesh Cylinder = RevolvedVerticalCylinder(5, 1, 4, 12);

        public static IMesh[] AllMeshes = {
            XYTriangle, // 0
            XYQuad, // 1
            XYQuadFromFunc, // 2
            XYQuad2x2, // 3
            Tetrahedron, // 4
            Torus, // 5
            Cylinder, // 6
            XYTriangleTwice, // 7
        };

        public static double SmallTolerance = 0.0001;

        public static void AssertEquals(IMesh g1, IMesh g2)
        {
            Assert.AreEqual(g1.NumFaces, g2.NumFaces);
            Assert.AreEqual(g1.NumCorners, g2.NumCorners);
            Assert.AreEqual(g1.NumVertices, g2.NumVertices);
            for (var i = 0; i < g1.Indices.Count; i++)
            {
                var v1 = g1.Vertices[g1.Indices[i]];
                var v2 = g2.Vertices[g2.Indices[i]];
                Assert.IsTrue(v1.AlmostEquals(v2, (float)SmallTolerance));
            }
        }

        public static void AssertEqualsWithAttributes(IMesh g1, IMesh g2)
        {
            AssertEquals(g1, g2);

            Assert.AreEqual(
                g1.Attributes.Select(attr => attr.Name).ToArray(),
                g2.Attributes.Select(attr => attr.Name).ToArray());

            Assert.AreEqual(
                g1.Attributes.Select(attr => attr.ElementCount).ToArray(),
                g2.Attributes.Select(attr => attr.ElementCount).ToArray());
        }

        public static void OutputTriangleStats(Triangle t)
        {
            Console.WriteLine($"Vertices: {t.A} {t.B} {t.C}");
            Console.WriteLine($"Area: {t.Area} Perimeter: {t.Perimeter} Midpoint: {t.MidPoint}");
            Console.WriteLine($"Bounding box: {t.BoundingBox}");
            Console.WriteLine($"Bounding sphere: {t.BoundingSphere}");
            Console.WriteLine($"Normal: {t.Normal}, normal direction {t.NormalDirection}");
            Console.WriteLine($"Lengths: {t.LengthA} {t.LengthB} {t.LengthC}");
        }

        public static void OutputTriangleStatsSummary(IMesh g)
        {
            var triangles = g.Triangles();
            for (var i = 0; i < Math.Min(3, triangles.Count); ++i)
            {
                Console.WriteLine($"Triangle {i}");
                OutputTriangleStats(triangles[i]);
            }

            if (triangles.Count > 3)
            {
                Console.WriteLine("...");
                Console.WriteLine($"Triangle {triangles.Count - 1}");
                OutputTriangleStats(triangles.Last());
            }
        }

        public static void OutputIMeshStats(IMesh g)
        {
            g.Validate();
            foreach (var attr in g.Attributes.ToEnumerable())
                Console.WriteLine($"{attr.Descriptor} elementCount={attr.ElementCount}");
        }

        public static void GeometryNullOps(IMesh g)
        {
            AssertEqualsWithAttributes(g, g);
            AssertEqualsWithAttributes(g, g.Attributes.ToIMesh());
            AssertEqualsWithAttributes(g, g.Translate(Vector3.Zero));
            AssertEqualsWithAttributes(g, g.Scale(1.0f));
            AssertEqualsWithAttributes(g, g.Transform(Matrix4x4.Identity));

            AssertEquals(g, g.CopyFaces(0, g.NumFaces).ToIMesh());
        }


        [Test]
        public static void BasicTests()
        {
            var nMesh = 0;
            foreach (var g in AllMeshes)
            {
                Console.WriteLine($"Testing mesh {nMesh++}");
                g.Validate();
                //ValidateGeometry(g.ToTriMesh());
            }

            Assert.AreEqual(3, XYTriangle.NumCornersPerFace);
            Assert.AreEqual(1, XYTriangle.NumFaces);
            Assert.AreEqual(3, XYTriangle.Vertices.Count);
            Assert.AreEqual(3, XYTriangle.Indices.Count);
            Assert.AreEqual(1, XYTriangle.Triangles().Count);
            Assert.AreEqual(0.5, XYTriangle.Area(), SmallTolerance);
            Assert.IsTrue(XYTriangle.Planar());
            Assert.AreEqual(new[] { 0, 1, 2 }, XYTriangle.Indices.ToArray());

            Assert.AreEqual(3, XYQuad.NumCornersPerFace);
            Assert.AreEqual(2, XYQuad.NumFaces);
            Assert.AreEqual(4, XYQuad.Vertices.Count);
            Assert.AreEqual(6, XYQuad.Indices.Count);

            Assert.IsTrue(XYQuad.Planar());
            Assert.AreEqual(new[] { 0, 1, 2, 0, 2, 3 }, XYQuad.Indices.ToArray());

            Assert.AreEqual(3, XYQuadFromFunc.NumCornersPerFace);
            Assert.AreEqual(2, XYQuadFromFunc.NumFaces);
            Assert.AreEqual(4, XYQuadFromFunc.Vertices.Count);
            Assert.AreEqual(6, XYQuadFromFunc.Indices.Count);

            Assert.AreEqual(3, XYQuad2x2.NumCornersPerFace);
            Assert.AreEqual(8, XYQuad2x2.NumFaces);
            Assert.AreEqual(9, XYQuad2x2.Vertices.Count);
            Assert.AreEqual(24, XYQuad2x2.Indices.Count);

            Assert.AreEqual(3, Tetrahedron.NumCornersPerFace);
            Assert.AreEqual(4, Tetrahedron.NumFaces);
            Assert.AreEqual(4, Tetrahedron.Vertices.Count);
            Assert.AreEqual(12, Tetrahedron.Indices.Count);
            Assert.AreEqual(TestTetrahedronIndices, Tetrahedron.Indices.ToArray());

            Assert.AreEqual(3, XYTriangleTwice.NumCornersPerFace);
            Assert.AreEqual(2, XYTriangleTwice.NumFaces);
            Assert.AreEqual(6, XYTriangleTwice.Vertices.Count);
            Assert.AreEqual(6, XYTriangleTwice.Indices.Count);
            Assert.AreEqual(2, XYTriangleTwice.Triangles().Count);
            Assert.AreEqual(1.0, XYTriangleTwice.Area(), SmallTolerance);
            Assert.IsTrue(XYTriangleTwice.Planar());
            Assert.AreEqual(new[] { 0, 1, 2, 3, 4, 5 }, XYTriangleTwice.Indices.ToArray());
        }

        [Test]
        public static void BasicManipulationTests()
        {
            foreach (var g in AllMeshes)
                GeometryNullOps(g);
        }

        [Test]
        public static void OutputGeometryData()
        {
            var n = 0;
            foreach (var g in AllMeshes)
            {
                Console.WriteLine($"Geometry {n++}");
                for (var i = 0; i < g.Vertices.Count && i < 10; ++i)
                {
                    Console.WriteLine($"Vertex {i} {g.Vertices[i]}");
                }

                if (g.Vertices.Count > 10)
                {
                    var last = g.Vertices.Count - 1;
                    Console.WriteLine("...");
                    Console.WriteLine($"Vertex {last} {g.Vertices[last]}");
                }

                for (var i = 0; i < g.NumFaces && i < 10; ++i)
                {
                    Console.WriteLine($"Face {i}: {g.Triangle(i)}");
                }

                if (g.Vertices.Count > 10)
                {
                    var last = g.NumFaces - 1;
                    Console.WriteLine("...");
                    Console.WriteLine($"Face {last}: {g.Triangle(last)}");
                }
            }
        }

        [Test]
        public static void StripIndicesTests()
        {
            var emptyStrip00 = Primitives.QuadMeshStripIndicesFromPointRows(0, 0);
            Assert.AreEqual(0, emptyStrip00.Count);

            var emptyStrip01 = Primitives.QuadMeshStripIndicesFromPointRows(0, 1);
            Assert.AreEqual(0, emptyStrip01.Count);

            var emptyStrip10 = Primitives.QuadMeshStripIndicesFromPointRows(1, 0);
            Assert.AreEqual(0, emptyStrip10.Count);

            var emptyStrip11 = Primitives.QuadMeshStripIndicesFromPointRows(1, 1);
            Assert.AreEqual(0, emptyStrip11.Count);

            var emptyStrip12 = Primitives.QuadMeshStripIndicesFromPointRows(1, 2);
            Assert.AreEqual(0, emptyStrip12.Count);

            var emptyStrip21 = Primitives.QuadMeshStripIndicesFromPointRows(2, 1);
            Assert.AreEqual(0, emptyStrip21.Count);

            // COUNTER-CLOCKWISE TEST (DEFAULT)
            //   2------3   <--- row 1: [2,3]
            //   |      |                      => counter-clockwise quad: (0,1,3,2)
            //   |      |
            //   0------1   <--- row 0: [0,1]
            var strip22 = Primitives.QuadMeshStripIndicesFromPointRows(2, 2);
            Assert.AreEqual(4, strip22.Count);
            Assert.AreEqual(0, strip22[0]);
            Assert.AreEqual(1, strip22[1]);
            Assert.AreEqual(3, strip22[2]);
            Assert.AreEqual(2, strip22[3]);

            // CLOCKWISE TEST
            //   2------3   <--- row 1: [2,3]
            //   |      |                      => clockwise quad: (2,3,1,0)
            //   |      |
            //   0------1   <--- row 0: [0,1]
            var clockwiseStrip22 = Primitives.QuadMeshStripIndicesFromPointRows(2, 2, true);
            Assert.AreEqual(4, clockwiseStrip22.Count);
            Assert.AreEqual(2, clockwiseStrip22[0]);
            Assert.AreEqual(3, clockwiseStrip22[1]);
            Assert.AreEqual(1, clockwiseStrip22[2]);
            Assert.AreEqual(0, clockwiseStrip22[3]);
            var reversed22 = clockwiseStrip22.Reverse();
            for (var i = 0; i < strip22.Count; ++i)
            {
                Assert.AreEqual(strip22[i], reversed22[i]);
            }

            //   *------*------*
            //   |      |      |
            //   |      |      |
            //   *------*------*
            var strip23 = Primitives.QuadMeshStripIndicesFromPointRows(2, 3);
            Assert.AreEqual(4 * 2, strip23.Count);

            //   *------*------*------*
            //   |      |      |      |
            //   |      |      |      |
            //   *------*------*------*
            //   |      |      |      |
            //   |      |      |      |
            //   *------*------*------*
            var strip34 = Primitives.QuadMeshStripIndicesFromPointRows(3, 4);
            Assert.AreEqual(4 * 6, strip34.Count);
        }

        [Test]
        public static void TriangleSerializationTest()
        {
            // Serialize a triangle g3d to a bfast as bytes and read it back.
            var vertices = new[]
            {
                new Vector3(0, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 1, 1)
            };

            var indices = new[] { 0, 1, 2 };
            var submeshIndexOffsets = new[] { 0 };
            var submeshMaterials = new[] { 0 };

            var g3d = new G3DBuilder()
                .AddVertices(vertices.ToIArray())
                .AddIndices(indices.ToIArray())
                .Add(submeshIndexOffsets.ToIArray().ToSubmeshIndexOffsetAttribute())
                .Add(submeshMaterials.ToIArray().ToSubmeshMaterialAttribute())
                .ToG3D();

            var bfastBytes = g3d.WriteToBytes();
            var readG3d = G3D.Read(bfastBytes);

            Assert.IsNotNull(readG3d);
            var mesh = readG3d.ToIMesh();
            mesh.Validate();

            Assert.AreEqual(3, mesh.NumVertices);
            Assert.AreEqual(new Vector3(0, 0, 0), mesh.Vertices[0]);
            Assert.AreEqual(new Vector3(0, 1, 0), mesh.Vertices[1]);
            Assert.AreEqual(new Vector3(0, 1, 1), mesh.Vertices[2]);
            Assert.AreEqual(1, mesh.NumFaces);
            Assert.AreEqual(0, mesh.SubmeshIndexOffsets.ToEnumerable().Single());
            Assert.AreEqual(0, mesh.SubmeshMaterials.ToEnumerable().Single());
            Assert.AreEqual(0, mesh.GetFaceMaterials().First());
        }
    }
}
