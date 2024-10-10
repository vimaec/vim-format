using NUnit.Framework;
using System;
using System.Linq;
using Vim.Format.Geometry;
using Vim.G3d;
using Vim.Math3d;

namespace Vim.Format.Tests.Geometry
{
    public static class TestTransformations
    {
        [Test]
        public static void IdentityOperations()
        {
            foreach (var g in TestShapes.AllMeshes)
            {
                g.GeometryEquals(g);
                g.Translate(Vector3.Zero).GeometryEquals(g);
                g.Scale(Vector3.Zero).GeometryEquals(g);
                g.Transform(Matrix4x4.Identity).GeometryEquals(g);
            }
        }

        [Test]
        public static void ReverseWindingOrder_InvertsTriangle()
        {
            var t1 = TestShapes.XYTriangle;
            var t2 = TestShapes.XYTriangle;
            t1.ReverseWindingOrder();
            Assert.That(t1.indices.SequenceEqual(t2.indices.Reverse()));
        }

        [Test]
        public static void SaveLoad_AllMeshes()
        {
            foreach (var g in TestShapes.AllMeshes)
            {
                var g3d = g.ToG3d();
                var bfast = g3d.ToBFast();
                var g3d2 = new G3dVim(bfast);
                var result = VimMesh.FromG3d(g3d2);
                Assert.IsTrue(g.GeometryEquals(result));
            }
        }


    
    }
}
