using NUnit.Framework;
using NUnit.Framework.Internal;
using Vim.BFast;
using Vim.Format.Vimx;
using Vim.G3dNext.Attributes;
using Vim.Math3d;

namespace Vim.G3dNext.Tests
{
    [TestFixture]
    public static class VimG3dTests
    {
        [Test]
        public static void Can_Parse_Attributes()
        {
            var attributeNames = new VimAttributeCollection().AttributeNames;
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

            var g3d1 = new G3DNext<VimAttributeCollection>();
            var ac1 = g3d1.AttributeCollection;
            ac1.CornersPerFaceAttribute.TypedData = new[] { 3 };
            ac1.VertexAttribute.TypedData = vertices;
            ac1.IndexAttribute.TypedData = indices;

            var memoryStream = new MemoryStream();
            g3d1.ToBFast().Write(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var bfast = new BFastNext.BFastNext(memoryStream);

            var g3d2 = G3DNext<VimAttributeCollection>.ReadBFast(bfast);
            Assert.IsNotNull(g3d2);

            var ac2 = g3d2.AttributeCollection;
            Assert.AreEqual(3, ac2.VertexAttribute.Count);
            Assert.AreEqual(3, ac2.IndexAttribute.Count);
            Assert.AreEqual(0, ac2.MeshSubmeshOffsetAttribute.Count);
            Assert.AreEqual(0, ac2.InstanceTransformAttribute.Count);

            Assert.AreEqual(vertices, ac2.VertexAttribute.TypedData);
            Assert.AreEqual(indices, ac2.IndexAttribute.TypedData);
        }

        [Test]
        public static void Can_Read_G3d_From_Vim()
        {
            var g3d = G3dVim.ReadFromVim(TestUtils.ResidencePath);
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
            var g3d = G3dVim.ReadFromVim(TestUtils.ResidencePath);
            g3d.source.ToBFast().Write(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var bfast = new BFastNext.BFastNext(memoryStream);
            var g3dMats = G3dMaterials.FromBFast(bfast);

            Assert.IsNotNull(g3dMats);
            Assert.AreEqual(g3d.materialColors, g3dMats.materialColors);
            Assert.AreEqual(g3d.materialGlossiness, g3dMats.materialGlossiness);
            Assert.AreEqual(g3d.materialSmoothness, g3dMats.materialSmoothness);
        }

        [Test]
        public static void Can_Write_And_Read()
        {
            var testDir = TestUtils.PrepareTestDir();
            var g3d = TestUtils.CreateTestG3d();

            var g3dFilePath = Path.Combine(testDir, "test.g3d");
            g3d.ToBFast().Write(g3dFilePath);
            var result = G3DNext<VimAttributeCollection>.ReadBFast(g3dFilePath);

            foreach (var attributeName in result.AttributeCollection.AttributeNames)
            {
                var attr0 = g3d.AttributeCollection.Attributes[attributeName];
                var attr1 = result.AttributeCollection.Attributes[attributeName];
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
            var bfast = new BFastNext.BFastNext(memoryStream);
            

            var g3dResult = G3DNext<VimAttributeCollection>.ReadBFast(bfast);
            Assert.NotNull(g3d);

            var mac = g3dResult.AttributeCollection;

            {
                var merged = g3d.AttributeCollection.CornersPerFaceAttribute.TypedData;
                Assert.AreEqual(merged, mac.CornersPerFaceAttribute.TypedData);
            }

            {
                var merged = Enumerable.Repeat(g3d.AttributeCollection.VertexAttribute.TypedData, 2).SelectMany(v => v).ToArray();
                Assert.AreEqual(merged, mac.VertexAttribute.TypedData);
            }

            {
                var tmp = g3d.AttributeCollection.IndexAttribute.TypedData;
                var merged = new[] { tmp, tmp.Select(i => i + g3d.AttributeCollection.VertexAttribute.Count).ToArray() }.SelectMany(v => v).ToArray();
                Assert.AreEqual(merged, mac.IndexAttribute.TypedData);
            }

            {
                var tmp = g3d.AttributeCollection.SubmeshIndexOffsetAttribute.TypedData;
                var merged = new[] { tmp, tmp.Select(i => i + g3d.AttributeCollection.IndexAttribute.Count).ToArray() }.SelectMany(v => v).ToArray();
                Assert.AreEqual(merged, mac.SubmeshIndexOffsetAttribute.TypedData);
            }

            {
                var tmp = g3d.AttributeCollection.SubmeshMaterialAttribute.TypedData;
                var merged = new[] { tmp, tmp.Select(i => i + g3d.AttributeCollection.MaterialColorAttribute.Count).ToArray() }.SelectMany(v => v).ToArray();
                Assert.AreEqual(merged, mac.SubmeshMaterialAttribute.TypedData);
            }

            {
                var tmp = g3d.AttributeCollection.MeshSubmeshOffsetAttribute.TypedData;
                var merged = new[] { tmp, tmp.Select(i => i + g3d.AttributeCollection.SubmeshIndexOffsetAttribute.Count).ToArray() }.SelectMany(v => v).ToArray();
                Assert.AreEqual(merged, mac.MeshSubmeshOffsetAttribute.TypedData);
            }

            {
                var merged = Enumerable.Repeat(g3d.AttributeCollection.InstanceTransformAttribute.TypedData, 2).SelectMany(v => v).ToArray();
                Assert.AreEqual(merged, mac.InstanceTransformAttribute.TypedData);
            }

            {
                var tmp = g3d.AttributeCollection.InstanceMeshAttribute.TypedData;
                var merged = new[] { tmp, tmp.Select(i => i + g3d.AttributeCollection.MeshSubmeshOffsetAttribute.Count).ToArray() }.SelectMany(v => v).ToArray();
                Assert.AreEqual(merged, mac.InstanceMeshAttribute.TypedData);
            }

            {
                var merged = Enumerable.Repeat(g3d.AttributeCollection.InstanceParentAttribute.TypedData, 2).SelectMany(v => v).ToArray();
                Assert.AreEqual(merged, mac.InstanceParentAttribute.TypedData);
            }

            {
                var merged = Enumerable.Repeat(g3d.AttributeCollection.MaterialColorAttribute.TypedData, 2).SelectMany(v => v).ToArray();
                Assert.AreEqual(merged, mac.MaterialColorAttribute.TypedData);
            }

            {
                var merged = Enumerable.Repeat(g3d.AttributeCollection.MaterialGlossinessAttribute.TypedData, 2).SelectMany(v => v).ToArray();
                Assert.AreEqual(merged, mac.MaterialGlossinessAttribute.TypedData);
            }

            {
                var merged = Enumerable.Repeat(g3d.AttributeCollection.MaterialSmoothnessAttribute.TypedData, 2).SelectMany(v => v).ToArray();
                Assert.AreEqual(merged, mac.MaterialSmoothnessAttribute.TypedData);
            }
        }
    }

}

