using NUnit.Framework;
using NUnit.Framework.Internal;
using Vim.BFastLib;
using Vim.Util.Tests;

namespace Vim.G3dNext.Tests
{
    [TestFixture]
    public static class VimG3dNextTests
    {
        [Test]
        public static void Can_Read_G3d_From_Vim()
        {
            var g3d = G3dVim.FromVim(TestUtils.ResidencePath);
            Assert.IsNotNull(g3d);
        }

        [Test]
        public static void Can_Ignore_Extra_Attributes()
        {
            // Both G3dVim and G3dMaterial share 3 attributes
            // G3dVim contains many more attributes
            // We create a g3dMaterial from the bytes of a g3dVim
            // Shows that extra attributes are ignored as they should.

            var g3d = G3dNextTestUtils.CreateTestG3d();
            var g3dMats = new G3dMaterials(g3d.ToBFast());

            Assert.IsNotNull(g3dMats);
            Assert.AreEqual(g3d.MaterialColors, g3dMats.MaterialColors);
            Assert.AreEqual(g3d.MaterialGlossiness, g3dMats.MaterialGlossiness);
            Assert.AreEqual(g3d.MaterialSmoothness, g3dMats.MaterialSmoothness);
        }

        [Test]
        public static void Can_Write_And_Read()
        {
            var expected = G3dNextTestUtils.CreateTestG3d();
            var g3d = new G3dVim(expected.ToBFast());
            Assert.IsTrue(g3d.Equals(expected));
        }

        [Test]
        public static void Can_Merge_two_g3d()
        {
            var g3d = G3dNextTestUtils.CreateTestG3d();
            var merged = g3d.Merge(g3d);

            var expected = new G3dVim(
                instanceTransforms: g3d.InstanceTransforms.Concat(g3d.InstanceTransforms).ToArray(),
                instanceMeshes: g3d.InstanceMeshes.Concat(g3d.InstanceMeshes.Select(i => i + g3d.GetMeshCount())).ToArray(),
                instanceParents: g3d.InstanceParents.Concat(g3d.InstanceParents).ToArray(),
                instanceFlags: null,
                meshSubmeshOffsets: g3d.MeshSubmeshOffsets.Concat(g3d.MeshSubmeshOffsets.Select(i => g3d.GetSubmeshCount())).ToArray(),
                submeshIndexOffsets: g3d.SubmeshIndexOffsets.Concat(g3d.SubmeshIndexOffsets.Select(i => i + g3d.GetIndexCount())).ToArray(),
                submeshMaterials: g3d.SubmeshMaterials.Concat(g3d.SubmeshMaterials.Select(i => i + g3d.GetMaterialCount())).ToArray(),
                indices: g3d.Indices.Concat(g3d.Indices.Select(i => i + g3d.Positions.Length)).ToArray(),
                positions: g3d.Positions.Concat(g3d.Positions).ToArray(),
                materialColors: g3d.MaterialColors.Concat(g3d.MaterialColors).ToArray(),
                materialGlossiness: g3d.MaterialGlossiness.Concat(g3d.MaterialGlossiness).ToArray(),
                materialSmoothness: g3d.MaterialSmoothness.Concat(g3d.MaterialSmoothness).ToArray(),
                shapeColors: null,
                shapeWidths: null,
                shapeVertices: null,
                shapeVertexOffsets: null
            );
            Assert.IsTrue(merged.Equals(expected));
        }
    }
}

