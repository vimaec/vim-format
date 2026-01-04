using System.Linq;
using Vim.Util;
using NUnit.Framework;

namespace Vim.Format.Tests
{
    [TestFixture]
    public static class VimQuadMeshStripTests
    {
        [Test]
        public static void QuadMeshStripIndicesTests()
        {
            var emptyStrip00 = VimQuadMeshStrip.QuadMeshStripIndicesFromPointRows(0, 0);
            Assert.AreEqual(0, emptyStrip00.Count);

            var emptyStrip01 = VimQuadMeshStrip.QuadMeshStripIndicesFromPointRows(0, 1);
            Assert.AreEqual(0, emptyStrip01.Count);

            var emptyStrip10 = VimQuadMeshStrip.QuadMeshStripIndicesFromPointRows(1, 0);
            Assert.AreEqual(0, emptyStrip10.Count);

            var emptyStrip11 = VimQuadMeshStrip.QuadMeshStripIndicesFromPointRows(1, 1);
            Assert.AreEqual(0, emptyStrip11.Count);

            var emptyStrip12 = VimQuadMeshStrip.QuadMeshStripIndicesFromPointRows(1, 2);
            Assert.AreEqual(0, emptyStrip12.Count);

            var emptyStrip21 = VimQuadMeshStrip.QuadMeshStripIndicesFromPointRows(2, 1);
            Assert.AreEqual(0, emptyStrip21.Count);

            // COUNTER-CLOCKWISE TEST (DEFAULT)
            //   2------3   <--- row 1: [2,3]
            //   |      |                      => counter-clockwise quad: (0,1,3,2)
            //   |      |
            //   0------1   <--- row 0: [0,1]
            var strip22 = VimQuadMeshStrip.QuadMeshStripIndicesFromPointRows(2, 2);
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
            var clockwiseStrip22 = VimQuadMeshStrip.QuadMeshStripIndicesFromPointRows(2, 2, true);
            Assert.AreEqual(4, clockwiseStrip22.Count);
            Assert.AreEqual(2, clockwiseStrip22[0]);
            Assert.AreEqual(3, clockwiseStrip22[1]);
            Assert.AreEqual(1, clockwiseStrip22[2]);
            Assert.AreEqual(0, clockwiseStrip22[3]);
            var reversed22 = clockwiseStrip22.Reversed().ToArray();
            for (var i = 0; i < strip22.Count; ++i)
            {
                Assert.AreEqual(strip22[i], reversed22[i]);
            }

            //   *------*------*
            //   |      |      |
            //   |      |      |
            //   *------*------*
            var strip23 = VimQuadMeshStrip.QuadMeshStripIndicesFromPointRows(2, 3);
            Assert.AreEqual(4 * 2, strip23.Count);

            //   *------*------*------*
            //   |      |      |      |
            //   |      |      |      |
            //   *------*------*------*
            //   |      |      |      |
            //   |      |      |      |
            //   *------*------*------*
            var strip34 = VimQuadMeshStrip.QuadMeshStripIndicesFromPointRows(3, 4);
            Assert.AreEqual(4 * 6, strip34.Count);
        }
    }
}
