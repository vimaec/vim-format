using NUnit.Framework;
using NUnit.Framework.Internal;
using Vim.BFast;
using Vim.G3dNext.Attributes;
using Vim.Math3d;
using Vim.BFastNextNS;

namespace Vim.G3dNext.Tests
{
    [TestFixture]
    public static class VimG3dTests
    {
        [Test]
        public static void Can_Parse_Attributes()
        {
            var attributeNames = new VimAttributeCollection().GetAttributeNames();
            foreach (var name in attributeNames)
            {
                // Test that the attribute descriptor parsing works as intended.
                var parsed = AttributeDescriptor.TryParse(name, out var desc);
                Assert.IsTrue(parsed);
                Assert.AreEqual(name, desc.Name);
            }
        }

        [Test]
        public static void Can_Read_Write_Triangle()
        {
            // Serialize a triangle g3d as bytes and read it back.
            var vertices = new[]
            {
            new Vector3(0, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 1, 1)
        };

            var indices = new[] { 0, 1, 2 };

            var g3d1 = new VimAttributeCollection();
            g3d1.PositionsAttribute.TypedData = vertices;
            g3d1.IndicesAttribute.TypedData = indices;

            var memoryStream = new MemoryStream();
            g3d1.ToBFast().Write(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var bfast = new BFastNext(memoryStream);

            var g3d2 = new VimAttributeCollection(bfast);
            Assert.IsNotNull(g3d2);

            Assert.AreEqual(3, g3d2.PositionsAttribute.Count);
            Assert.AreEqual(3, g3d2.IndicesAttribute.Count);
            Assert.AreEqual(0, g3d2.MeshSubmeshOffsetsAttribute.Count);
            Assert.AreEqual(0, g3d2.InstanceTransformsAttribute.Count);

            Assert.AreEqual(vertices, g3d2.PositionsAttribute.TypedData);
            Assert.AreEqual(indices, g3d2.IndicesAttribute.TypedData);
        }

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

            var memoryStream = new MemoryStream();
            var g3d = G3dVim.FromVim(TestUtils.ResidencePath);
            g3d.ToBFast().Write(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var bfast = new BFastNext(memoryStream);
            var g3dMats = new G3dMaterials(bfast);

            Assert.IsNotNull(g3dMats);
            Assert.AreEqual(g3d.MaterialColors, g3dMats.MaterialColors);
            Assert.AreEqual(g3d.MaterialGlossiness, g3dMats.MaterialGlossiness);
            Assert.AreEqual(g3d.MaterialSmoothness, g3dMats.MaterialSmoothness);
        }

        [Test]
        public static void Can_Write_And_Read()
        {
            var testDir = TestUtils.PrepareTestDir();
            var g3d = TestUtils.CreateTestG3d();

            var g3dFilePath = Path.Combine(testDir, "test.g3d");
            g3d.ToBFast().Write(g3dFilePath);
            var result = g3dFilePath.ReadBFast((b) => new VimAttributeCollection(b));

            foreach (var attributeName in result.GetAttributeNames())
            {
                var attr0 = g3d.Map[attributeName];
                var attr1 = result.Map[attributeName];
                Assert.AreEqual(attr0.Data, attr1.Data);
            }
        }

        [Test]
        public static void Can_Merge_two_g3d()
        {
            var testDir = TestUtils.PrepareTestDir();
            var g3d = TestUtils.CreateTestG3d();

            var mergedG3d = new[] { g3d, g3d }.Merge();

            var memoryStream = new MemoryStream();
            var g3dFilePath = Path.Combine(testDir!, "merged.g3d");
            mergedG3d.ToBFast().Write(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var bfast = new BFastNext(memoryStream);
            

            var g3dResult = new VimAttributeCollection(bfast);
            Assert.NotNull(g3d);

            {
                var merged = Enumerable.Repeat(g3d.PositionsAttribute.TypedData, 2).SelectMany(v => v).ToArray();
                Assert.AreEqual(merged, g3dResult.PositionsAttribute.TypedData);
            }

            {
                var tmp = g3d.IndicesAttribute.TypedData;
                var merged = new[] { tmp, tmp.Select(i => i + g3d.PositionsAttribute.Count).ToArray() }.SelectMany(v => v).ToArray();
                Assert.AreEqual(merged, g3dResult.IndicesAttribute.TypedData);
            }

            {
                var tmp = g3d.SubmeshIndexOffsetsAttribute.TypedData;
                var merged = new[] { tmp, tmp.Select(i => i + g3d.IndicesAttribute.Count).ToArray() }.SelectMany(v => v).ToArray();
                Assert.AreEqual(merged, g3dResult.SubmeshIndexOffsetsAttribute.TypedData);
            }

            {
                var tmp = g3d.SubmeshMaterialsAttribute.TypedData;
                var merged = new[] { tmp, tmp.Select(i => i + g3d.MaterialColorsAttribute.Count).ToArray() }.SelectMany(v => v).ToArray();
                Assert.AreEqual(merged, g3dResult.SubmeshMaterialsAttribute.TypedData);
            }

            {
                var tmp = g3d.MeshSubmeshOffsetsAttribute.TypedData;
                var merged = new[] { tmp, tmp.Select(i => i + g3d.SubmeshIndexOffsetsAttribute.Count).ToArray() }.SelectMany(v => v).ToArray();
                Assert.AreEqual(merged, g3dResult.MeshSubmeshOffsetsAttribute.TypedData);
            }

            {
                var merged = Enumerable.Repeat(g3d.InstanceTransformsAttribute.TypedData, 2).SelectMany(v => v).ToArray();
                Assert.AreEqual(merged, g3dResult.InstanceTransformsAttribute.TypedData);
            }

            {
                var tmp = g3d.InstanceMeshesAttribute.TypedData;
                var merged = new[] { tmp, tmp.Select(i => i + g3d.MeshSubmeshOffsetsAttribute.Count).ToArray() }.SelectMany(v => v).ToArray();
                Assert.AreEqual(merged, g3dResult.InstanceMeshesAttribute.TypedData);
            }

            {
                var merged = Enumerable.Repeat(g3d.InstanceParentsAttribute.TypedData, 2).SelectMany(v => v).ToArray();
                Assert.AreEqual(merged, g3dResult.InstanceParentsAttribute.TypedData);
            }

            {
                var merged = Enumerable.Repeat(g3d.MaterialColorsAttribute.TypedData, 2).SelectMany(v => v).ToArray();
                Assert.AreEqual(merged, g3dResult.MaterialColorsAttribute.TypedData);
            }

            {
                var merged = Enumerable.Repeat(g3d.MaterialGlossinessAttribute.TypedData, 2).SelectMany(v => v).ToArray();
                Assert.AreEqual(merged, g3dResult.MaterialGlossinessAttribute.TypedData);
            }

            {
                var merged = Enumerable.Repeat(g3d.MaterialSmoothnessAttribute.TypedData, 2).SelectMany(v => v).ToArray();
                Assert.AreEqual(merged, g3dResult.MaterialSmoothnessAttribute.TypedData);
            }
        }
    }

}

