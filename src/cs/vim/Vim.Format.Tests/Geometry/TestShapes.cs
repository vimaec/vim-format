using NUnit.Framework;
using System.Linq;
using System;
using Vim.Format.Geometry;
using Vim.Math3d;

namespace Vim.Format.Tests.Geometry
{
    public class TestShapes
    {
        public static VimMesh XYTriangle => new VimMesh(
            new[] { 0, 1, 2 }, new[] {
                new Vector3(0f, 0f, 0f),
                new Vector3(0f, 1f, 0f),
                new Vector3(1f, 0f, 0f)
            });

        public static VimMesh XYQuad => VimMesh.FromQuad(
            new int[] { 0, 1, 2, 3 }, new[]{
                new Vector3(0f, 0f, 0f),
                new Vector3(0f, 1f, 0f),
                new Vector3(1f, 1f, 0f),
                new Vector3(1f, 0f, 0f)
            });

        public static VimMesh XYQuadFromFunc => Primitives.QuadMesh(uv => uv.ToVector3(), 1, 1);
        public static VimMesh XYQuad2x2 => Primitives.QuadMesh(uv => uv.ToVector3(), 2, 2);
        public static VimMesh XYTriangleTwice => XYTriangle.Merge(XYTriangle.Translate(new Vector3(1, 0, 0)));
        public static VimMesh Tetrahedron => new VimMesh(
            new[] { 0, 1, 2, 0, 3, 1, 1, 3, 2, 2, 3, 0 }, new[] {
                Vector3.Zero,
                Vector3.UnitX,
                Vector3.UnitY,
                Vector3.UnitZ
            });

        public static VimMesh Torus => Primitives.Torus(10, 0.2f, 10, 24);

        public static VimMesh[] AllMeshes = {
            XYTriangle, // 0
            XYQuad, // 1
            XYQuadFromFunc, // 2
            XYQuad2x2, // 3
            Tetrahedron, // 4
            Torus, // 5
            XYTriangleTwice, // 7
        };

        public static float SmallTolerance = 0.0001f;

        [Test]
        public void Test_Triangle()
        {
            Assert.AreEqual(3, XYTriangle.NumCornersPerFace);
            Assert.AreEqual(1, XYTriangle.NumFaces);
            Assert.AreEqual(3, XYTriangle.vertices.Length);
            Assert.AreEqual(3, XYTriangle.indices.Length);
            Assert.AreEqual(1, XYTriangle.Triangles().Length);
            Assert.IsTrue(XYTriangle.Planar(SmallTolerance));
            Assert.AreEqual(new[] { 0, 1, 2 }, XYTriangle.indices);
        }

        [Test]
        public void Test_Quad()
        {
            Assert.AreEqual(3, XYQuad.NumCornersPerFace);
            Assert.AreEqual(2, XYQuad.NumFaces);
            Assert.AreEqual(4, XYQuad.vertices.Length);
            Assert.AreEqual(6, XYQuad.indices.Length);
            Assert.IsTrue(TestShapes.XYQuad.Planar(SmallTolerance));
            Assert.AreEqual(new[] { 0, 1, 2, 0, 2, 3 }, XYQuad.indices);
        }

        [Test]
        public void Test_QuadFromFunc()
        {
            Assert.AreEqual(3, XYQuadFromFunc.NumCornersPerFace);
            Assert.AreEqual(2, XYQuadFromFunc.NumFaces);
            Assert.AreEqual(4, XYQuadFromFunc.vertices.Length);
            Assert.AreEqual(6, XYQuadFromFunc.indices.Length);
        }

        [Test]
        public void Test_Quad2x2()
        {
            Assert.AreEqual(3, XYQuad2x2.NumCornersPerFace);
            Assert.AreEqual(8, XYQuad2x2.NumFaces);
            Assert.AreEqual(9, XYQuad2x2.vertices.Length);
            Assert.AreEqual(24, XYQuad2x2.indices.Length);
        }


        [Test]
        public void Test_Tetrahedron()
        {
            Assert.AreEqual(3, Tetrahedron.NumCornersPerFace);
            Assert.AreEqual(4, Tetrahedron.NumFaces);
            Assert.AreEqual(4, Tetrahedron.vertices.Length);
            Assert.AreEqual(12, Tetrahedron.indices.Length);
        }

        [Test]
        public void Test_TriangleTwice()
        {
            Assert.AreEqual(3, XYTriangleTwice.NumCornersPerFace);
            Assert.AreEqual(2, XYTriangleTwice.NumFaces);
            Assert.AreEqual(6, XYTriangleTwice.vertices.Length);
            Assert.AreEqual(6, XYTriangleTwice.indices.Length);
            Assert.AreEqual(2, XYTriangleTwice.Triangles().Length);
            Assert.IsTrue(XYTriangleTwice.Planar());
            Assert.AreEqual(new[] { 0, 1, 2, 3, 4, 5 }, XYTriangleTwice.indices);
        }
    }
}
