using NUnit.Framework;
using System;
using System.Linq;
using Vim.Format.Geometry;

namespace Vim.Format.Tests.Geometry
{
    internal class MeshStripsTest
    {
        [Test]
        public static void Strip_0_0()
        {
            var emptyStrip00 = Primitives.QuadMeshStripIndicesFromPointRows(0, 0);
            Assert.AreEqual(0, emptyStrip00.Length);
        }

        [Test]
        public static void Strip_0_1()
        {
            var emptyStrip01 = Primitives.QuadMeshStripIndicesFromPointRows(0, 1);
            Assert.AreEqual(0, emptyStrip01.Length);
        }

        [Test]
        public static void Strip_1_0()
        {
            var emptyStrip10 = Primitives.QuadMeshStripIndicesFromPointRows(1, 0);
            Assert.AreEqual(0, emptyStrip10.Length);
        }

        [Test]
        public static void Strip_1_1()
        {
            var emptyStrip11 = Primitives.QuadMeshStripIndicesFromPointRows(1, 1);
            Assert.AreEqual(0, emptyStrip11.Length);
        }

        [Test]
        public static void Strip_1_2()
        {
            var emptyStrip12 = Primitives.QuadMeshStripIndicesFromPointRows(1, 2);
            Assert.AreEqual(0, emptyStrip12.Length);
        }

        [Test]
        public static void Strip_2_1()
        {
            var emptyStrip21 = Primitives.QuadMeshStripIndicesFromPointRows(2, 1);
            Assert.AreEqual(0, emptyStrip21.Length);
        }

        [Test]
        public static void Strip_2_2_CounterClockwise()
        {
            // COUNTER-CLOCKWISE TEST (DEFAULT)
            //   2------3   <--- row 1: [2,3]
            //   |      |                      => counter-clockwise quad: (0,1,3,2)
            //   |      |
            //   0------1   <--- row 0: [0,1]
            var strip22 = Primitives.QuadMeshStripIndicesFromPointRows(2, 2);
            Assert.AreEqual(4, strip22.Length);
            Assert.AreEqual(0, strip22[0]);
            Assert.AreEqual(1, strip22[1]);
            Assert.AreEqual(3, strip22[2]);
            Assert.AreEqual(2, strip22[3]);
        }

        [Test]
        public static void Strip_2_2_Clockwise()
        {
            // CLOCKWISE TEST
            //   2------3   <--- row 1: [2,3]
            //   |      |                      => clockwise quad: (2,3,1,0)
            //   |      |
            //   0------1   <--- row 0: [0,1]
            var clockwiseStrip22 = Primitives.QuadMeshStripIndicesFromPointRows(2, 2, true);
            Assert.AreEqual(4, clockwiseStrip22.Length);
            Assert.AreEqual(2, clockwiseStrip22[0]);
            Assert.AreEqual(3, clockwiseStrip22[1]);
            Assert.AreEqual(1, clockwiseStrip22[2]);
            Assert.AreEqual(0, clockwiseStrip22[3]);
            clockwiseStrip22.Reverse();
        }

        [Test]
        public static void Strip_2_2_Reverse()
        {
            var clockwiseStrip22 = Primitives.QuadMeshStripIndicesFromPointRows(2, 2, true);
            var strip22 = Primitives.QuadMeshStripIndicesFromPointRows(2, 2);

            Assert.IsTrue(clockwiseStrip22.Reverse().SequenceEqual(strip22));
        }

        [Test]
        public static void Strip_2_3()
        {
            //   *------*------*
            //   |      |      |
            //   |      |      |
            //   *------*------*
            var strip23 = Primitives.QuadMeshStripIndicesFromPointRows(2, 3);
            Assert.AreEqual(4 * 2, strip23.Length);
        }

        [Test]
        public static void Strip_3_4()
        {
            //   *------*------*------*
            //   |      |      |      |
            //   |      |      |      |
            //   *------*------*------*
            //   |      |      |      |
            //   |      |      |      |
            //   *------*------*------*
            var strip34 = Primitives.QuadMeshStripIndicesFromPointRows(3, 4);
            Assert.AreEqual(4 * 6, strip34.Length);
        }
    }
}
