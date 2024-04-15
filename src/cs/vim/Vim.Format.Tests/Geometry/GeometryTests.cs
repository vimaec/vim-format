using NUnit.Framework;
using System;
using System.Linq;
using Vim.Format.Geometry;
using Vim.G3dNext;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim.Format.Tests.Geometry
{
    public static class GeometryTests
    {
        public static VimMesh XYTriangle = new VimMesh(
            new[] { 0, 1, 2 }, new[] {
                new Vector3(0f, 0f, 0f),
                new Vector3(0f, 1f, 0f),
                new Vector3(1f, 0f, 0f)
            });

        public static VimMesh XYQuad = VimMesh.FromQuad(
            new int[] { 0, 1, 2, 3 }, new[]{
                new Vector3(0f, 0f, 0f),
                new Vector3(0f, 1f, 0f),
                new Vector3(1f, 1f, 0f),
                new Vector3(1f, 0f, 0f)
            });

        public static VimMesh XYQuadFromFunc = Primitives.QuadMesh(uv => uv.ToVector3(), 1, 1);
        public static VimMesh XYQuad2x2 = Primitives.QuadMesh(uv => uv.ToVector3(), 2, 2);
        public static VimMesh XYTriangleTwice = XYTriangle.Merge2(XYTriangle.Translate(new Vector3(1, 0, 0)));

        public static VimMesh Tetrahedron = new VimMesh(
            new[] { 0, 1, 2, 0, 3, 1, 1, 3, 2, 2, 3, 0 }, new[] {
                Vector3.Zero,
                Vector3.UnitX,
                Vector3.UnitY,
                Vector3.UnitZ
            });

        public static VimMesh Torus = Primitives.Torus(10, 0.2f, 10, 24);

        public static VimMesh[] AllMeshes = {
            XYTriangle, // 0
            XYQuad, // 1
            XYQuadFromFunc, // 2
            XYQuad2x2, // 3
            Tetrahedron, // 4
            Torus, // 5
            XYTriangleTwice, // 7
        };

        public static double SmallTolerance = 0.0001;

        public static void GeometryNullOps(VimMesh g)
        {
            g.GeometryEquals(g);
            g.Translate(Vector3.Zero).GeometryEquals(g);
            g.Scale(Vector3.Zero).GeometryEquals(g);
            g.Transform(Matrix4x4.Identity).GeometryEquals(g);
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

            Assert.AreEqual(3, XYTriangleTwice.NumCornersPerFace);
            Assert.AreEqual(2, XYTriangleTwice.NumFaces);
            Assert.AreEqual(6, XYTriangleTwice.Vertices.Count);
            Assert.AreEqual(6, XYTriangleTwice.Indices.Count);
            Assert.AreEqual(2, XYTriangleTwice.Triangles().Count);
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
            //TODO: Check the need for this test.
            // Serializing a triangle is that a use case ?

            var mesh = new VimMesh(new[] { 0, 1, 2 }, new[]
            {
                new Vector3(0, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 1, 1)
            });

            var bfast = mesh.ToG3d().ToBFast();
            var result = VimMesh.FromG3d(new G3dVim(bfast));

            Assert.IsNotNull(mesh);
            result.Validate();

            Assert.AreEqual(3, result.NumVertices);
            Assert.AreEqual(new Vector3(0, 0, 0), result.Vertices[0]);
            Assert.AreEqual(new Vector3(0, 1, 0), result.Vertices[1]);
            Assert.AreEqual(new Vector3(0, 1, 1), result.Vertices[2]);
            Assert.AreEqual(1, result.NumFaces);
            Assert.AreEqual(0, result.SubmeshIndexOffsets.ToEnumerable().Single());
            Assert.AreEqual(-1, result.SubmeshMaterials.ToEnumerable().Single());
            Assert.AreEqual(-1, result.GetFaceMaterials().First());
        }
    }
}
