using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Data;
using Vim.BFastNS.Core;
using Vim.Util.Tests;

namespace Vim.BFastNS.Tests
{
    public class BFastTests
    {
        public static string ResultPath = Path.Combine(VimFormatRepoPaths.OutDir, "input.bfast");
        public static string ResultPath2 = Path.Combine(VimFormatRepoPaths.OutDir, "input.bfast");
        public static string ResidencePath = VimFormatRepoPaths.GetLatestWolfordResidenceVim();

        BFast bfast;

        [SetUp]
        public void Setup()
        {
            bfast = new BFast();

            if (!Directory.Exists(VimFormatRepoPaths.OutDir))
            {
                Directory.CreateDirectory(VimFormatRepoPaths.OutDir);
            }
            if (File.Exists(ResultPath))
            {
                File.Delete(ResultPath);
            }
            if (File.Exists(ResultPath2))
            {
                File.Delete(ResultPath2);
            }
        }

        private void TestBeforeAfter(Action<BFast> method)
        {
            method(bfast);

            // Test that it also works after write/read
            var next = new BFast(bfast.ToMemoryStream());
            method(next);
        }

        private void TestBeforeAfter<T>(Func<BFast, T> method, IResolveConstraint constraint)
        {
            Assert.That(method(bfast), constraint);

            // Test that it also works after write/read
            var next = new BFast(bfast.ToMemoryStream());
            Assert.That(method(next), constraint);
        }

        private void TestBeforeAfterFile<T>(Func<BFast, T> method, IResolveConstraint constraint)
        {
            Assert.That(method(bfast), constraint);
            using (var file = File.Open(ResultPath, FileMode.CreateNew))
            {
                bfast.Write(file);
                file.Seek(0, SeekOrigin.Begin);

                // Test that it also works after write/read
                var next = new BFast(file);
                Assert.That(method(next), constraint);
            }
        }

        #region empty

        [Test]
        public void EmptyBFast_Has_No_Entries()
        {
            var bfast = new BFast();
            Assert.That(bfast.Entries.Count(), Is.EqualTo(0));
        }

        [Test]
        public void EmptyBFast_GetArray_Returns_Null()
        {
            var bfast = new BFast();
            Assert.IsNull(bfast.GetArray<byte>("missing"));
        }

        [Test]
        public void EmptyBFast_GetBfast_Returns_Null()
        {
            var bfast = new BFast();
            Assert.IsNull(bfast.GetBFast("missing"));
        }

        [Test]
        public void EmptyBFast_GetEnumerable_Returns_Null()
        {
            var bfast = new BFast();
            Assert.IsNull(bfast.GetEnumerable<byte>("missing"));
        }

        [Test]
        public void EmptyBFast_Remove_Does_Nothing()
        {
            var bfast = new BFast();
            bfast.Remove("missing");
        }


        [Test]
        public void EmptyBFast_Writes_Header()
        {
            var bfast = new BFast();
            var stream = new MemoryStream();
            bfast.Write(stream);

            stream.Seek(0, SeekOrigin.Begin);
            var raw = BFastHeader.FromStream(stream);

            Assert.That(raw.Ranges.Count, Is.EqualTo(0));
        }
        #endregion

        #region enumerable

        [Test]
        public void SetEnumerable_Adds_Entry()
        {
            bfast.SetEnumerable("A", () => new int[3] { 0, 1, 2 });
            TestBeforeAfter(b => b.Entries.Count(), Is.EqualTo(1));
        }

        [Test]
        public void SetEnumerable_Then_GetEnumerable()
        {
            var expected = new int[3] { 0, 1, 2 };
            bfast.SetEnumerable("A", () => expected);
            TestBeforeAfter(b => b.GetEnumerable<int>("A"), Is.EqualTo(expected));
        }

        [Test]
        public void SetEnumerable_Then_GetEnumerable_Bytes()
        {
            bfast.SetEnumerable("A", () => new int[3] { 0, 1, 2 });
            var expected = (new int[3] { 0, 1, 2 }).SelectMany(i => BitConverter.GetBytes(i));
            TestBeforeAfter(b => b.GetEnumerable<byte>("A"), Is.EqualTo(expected));
        }

        [Test]
        public void SetEnumerable_Then_GetEnumerable_Float()
        {
            bfast.SetEnumerable("A", () => new int[3] { 0, 1, 2 });
            var expected = (new int[3] { 0, 1, 2 }).Select(i => BitConverter.Int32BitsToSingle(i));
            TestBeforeAfter(b => b.GetEnumerable<float>("A"), Is.EqualTo(expected));
        }

        [Test]
        public void SetEnumerable_Then_GetArray()
        {
            bfast.SetEnumerable("A", () => new int[3] { 0, 1, 2 });
            var expected = new int[3] { 0, 1, 2 };
            TestBeforeAfter(b => b.GetArray<int>("A"), Is.EqualTo(expected));
        }

        [Test]
        public void SetEnumerable_Then_GetArray_Bytes()
        {
            bfast.SetEnumerable("A", () => new int[3] { 0, 1, 2 });
            var expected = (new int[3] { 0, 1, 2 }).SelectMany(i => BitConverter.GetBytes(i));
            TestBeforeAfter(b => b.GetArray<byte>("A"), Is.EqualTo(expected));
        }

        [Test]
        public void SetEnumerable_Then_GetArray_Float()
        {
            bfast.SetEnumerable("A", () => new int[3] { 0, 1, 2 });
            var expected = (new int[3] { 0, 1, 2 }).Select(i => BitConverter.Int32BitsToSingle(i));
            // MemoryStream can't handle such size.
            TestBeforeAfter(b => b.GetArray<float>("A"), Is.EqualTo(expected));

        }

        [Test]
        public void SetEnumerable_Then_GetBFast()
        {
            bfast.SetEnumerable("A", () => new int[3] { 0, 1, 2 });
            TestBeforeAfter(b => b.GetBFast("A"), Is.Null);
        }

        [Test]
        public void SetEnumerable_Then_GetBFast_ValidBytes()
        {
            var sub = new BFast();
            bfast.SetBFast("A", sub);
            var bytes = bfast.GetArray<byte>("A");
            bfast.SetEnumerable("A",() => bytes);
            
            TestBeforeAfter(b => b.GetBFast("A"), Is.EqualTo(sub));
        }

        [Test, Explicit]
        public void SetEnumerable_Then_GetEnumerable_Lots()
        {
            IEnumerable<int> GetLots()
            {
                return Enumerable.Range(0, int.MaxValue).Concat(Enumerable.Range(0, 10));
            }
            bfast.SetEnumerable<int>("A", GetLots);

            TestBeforeAfterFile(b => b.GetEnumerable<int>("A"), Is.EqualTo(GetLots()));
        }

        #endregion

        #region array
        [Test]
        public void SetArray_Adds_Entry()
        {
            bfast.SetArray("A", new int[3] { 0, 1, 2 });
            TestBeforeAfter(b => b.Entries.Count(), Is.EqualTo(1));
        }

        [Test]
        public void SetArray_Then_GetArray()
        {
            var array = new int[3] { 0, 1, 2 };
            bfast.SetArray("A", array);
            TestBeforeAfter(b => b.GetArray<int>("A"), Is.EqualTo(array));
        }

        [Test]
        public void SetArray_Then_GetArray_Bytes()
        {
            var array = new int[3] { 0, 1, 2 };
            var expected = array.SelectMany(i => BitConverter.GetBytes(i));

            bfast.SetArray("A", array);
            TestBeforeAfter(b => bfast.GetArray<byte>("A"), Is.EqualTo(expected));
        }

        [Test]
        public void SetArray_Then_GetArray_Float()
        {
            var array = new int[3] { 0, 1, 2 };
            var expected = array.Select(i => BitConverter.Int32BitsToSingle(i));
            
            bfast.SetArray("A", array);
            TestBeforeAfter(b => bfast.GetArray<float>("A"), Is.EqualTo(expected));
        }

        [Test]
        public void SetArray_Then_GetEnumerable()
        {
            var array = new int[3] { 0, 1, 2 };
            bfast.SetArray("A", array);
            TestBeforeAfter(b => b.GetEnumerable<int>("A"), Is.EqualTo(array));
        }

        [Test]
        public void SetArray_Then_GetEnumerable_Bytes()
        {
            var array = new int[3] { 0, 1, 2 };
            var expected = array.SelectMany(i => BitConverter.GetBytes(i));
         
            bfast.SetArray("A", array);
            TestBeforeAfter(b => b.GetEnumerable<byte>("A"), Is.EqualTo(expected));
        }

        [Test]
        public void SetArray_Then_GetEnumerable_Float()
        {
            var array = new int[3] { 0, 1, 2 };
            var expected = array.Select(i => BitConverter.Int32BitsToSingle(i));
         
            bfast.SetArray("A", array);
            var result = bfast.GetEnumerable<float>("A");
        }

        [Test]
        public void SetArray_Then_GetBFast_Returns_Null()
        {
            bfast.SetArray("A", new int[3] { 0, 1, 2 });
            TestBeforeAfter(b => b.GetBFast("A"), Is.Null);
        }

        [Test]
        public void SetArray_Then_SetArray_Replaces()
        {
            var ints = new int[3] { 0, 1, 2 };
            var floats = new float[3] { 0.1f, 0.2f, 0.3f };
            bfast.SetArray("A", ints);
            bfast.SetArray("A", floats);
            TestBeforeAfter(b => b.GetArray<float>("A"), Is.EqualTo(floats));
        }

        [Test]
        public void SetArray_Then_SetBFast_Replaces()
        {
            bfast.SetArray("A", new int[3] { 0, 1, 2 });
            bfast.SetBFast("A", new BFast());
            TestBeforeAfter(b => b.GetArray<int>("A").Length, Is.GreaterThan(3));
        }
        #endregion

        [Test]
        public void SetBFast_Adds_Entry()
        {
            bfast.SetBFast("A", new BFast());
            TestBeforeAfter(b => b.Entries.Count(), Is.EqualTo(1));
        }

        [Test]
        public void SetBFast_Then_GetBFast_Returns_Same()
        {
            var expected = new BFast();
            bfast.SetBFast("A", expected);
            TestBeforeAfter(b => b.GetBFast("A"), Is.EqualTo(expected));
        }

        [Test]
        public void SetBFast_Then_GetBFast_Nested()
        {
            using (var file = File.Open(ResidencePath, FileMode.Open))
            {
                var (b1, b2) = (new BFast(), new BFast());
                b1.SetBFast("b2", b2);
                bfast.SetBFast("b1", b1);

                var mem = bfast.ToMemoryStream();
                var r = new BFast(mem);
                var r1 = r.GetBFast("b1");
                var r2 = r1.GetBFast("b2");

                Assert.NotNull(r);
                Assert.NotNull(r1);
                Assert.NotNull(r2);
            }
        }

        #region compress
        [Test]
        public void Compression_Decompress_Uncompressed_Returns_Null()
        {
            var expected = new BFast();
            bfast.SetBFast("A", expected);
            TestBeforeAfter(b => b.GetBFast("A", decompress: true), Is.Null);
        }

        [Test]
        public void Compression_Get_Compressed_Returns_Null()
        {
            var expected = new BFast();
            bfast.SetBFast("A", expected, compress: true);
            TestBeforeAfter(b => b.GetBFast("A"), Is.Null);
        }

        [Test]
        public void Compression_Get_Uncompressed_Works()
        {
            // This is tested by the bfast tests.
        }

        [Test]
        public void Compression_Decompress_Compressed_Works()
        {
            var ints = new int[3] { 0, 1, 2 };

            var bfastA = new BFast();
            bfastA.SetArray("B", ints);
            bfast.SetBFast("A", bfastA, compress: true);

            TestBeforeAfter((b) =>
            {
                var result = b.GetBFast("A", decompress: true);
                var b2 = result.GetArray<int>("B");

                Assert.That(result.Entries.Count(), Is.EqualTo(1));
                Assert.That(b2, Is.EqualTo(ints));
            });
        }
        #endregion

        #region bfast

        [Test]
        public void SetBFast_Then_SetBFast_Replaces()
        {
            var bfastA = new BFast();
            bfast.SetBFast("A", bfastA);

            var bfastB = new BFast();
            bfastB.SetArray("A", new int[] { 1, 2, 3 });
            bfast.SetBFast("A", bfastB);

            TestBeforeAfter((b) =>
            {
                var result = b.GetBFast("A");
                Assert.That(bfastA, Is.Not.EqualTo(bfastB));
                Assert.That(result, Is.Not.EqualTo(bfastA));
                Assert.That(result, Is.EqualTo(bfastB));
            });
        }

        [Test]
        public void SetBFast_Then_SetArray_Replaces()
        {
            bfast.SetBFast("A", new BFast());
            bfast.SetArray("A", new int[3] { 0, 1, 2 });
            TestBeforeAfter((b) =>
            {
                var result = b.GetBFast("A");
                Assert.IsNull(result);
            });
            
        }
        #endregion

        [Test]
        public void Remove_Missing_DoesNothing()
        {
            TestBeforeAfter((b) =>
            {
                b.Remove("A");
                Assert.That(b.Entries.Count() == 0);
            });
        }

        [Test]
        public void Remove_Array()
        {
            bfast.SetArray("A", new int[3] { 0, 1, 2 });
            bfast.Remove("A");
            TestBeforeAfter((b) =>
            {
                Assert.IsNull(b.GetArray<int>("A"));
                Assert.That(b.Entries.Count() == 0);
            });
        }

        [Test]
        public void Remove_BFast()
        {
            bfast.SetBFast("A", new BFast());
            bfast.Remove("A");

            TestBeforeAfter((b) =>
            {
                Assert.IsNull(bfast.GetBFast("A"));
                Assert.That(bfast.Entries.Count() == 0);
            });
        }

        [Test]
        public void Removed_InChild_Not_Written()
        {
            using (var residence = File.OpenRead(ResidencePath))
            {
                var input = new BFast(residence);
                var geometry = input.GetBFast("geometry");
                geometry.Remove("g3d:vertex:position:0:float32:3");
                input.SetBFast("geometry", geometry);
                input.Write(ResultPath);
            }

            using (var stream = File.OpenRead(ResultPath))
            {
                var bfast = new BFast(stream);
                var geometry = bfast.GetBFast("geometry");
                Assert.That(bfast.Entries.Count() == 5);
                Assert.That(geometry.Entries.Count() == 16);
                Assert.IsNull(geometry.GetArray<float>("g3d:vertex:position:0:float32:3"));
            }
        }

        [Test]
        public void Write_Then_Read_NestedBFast()
        {
            var bfast = new BFast();
            var child = new BFast();
            var grandChild = new BFast();

            bfast.SetBFast("child", child);
            child.SetBFast("grandChild", grandChild);
            bfast.Write(ResultPath);

            using (var stream = File.OpenRead(ResultPath))
            {
                var other = new BFast(stream);
                var child2 = other.GetBFast("child");
                var grandChild2 = child2.GetBFast("grandChild");

                Assert.That(other.Entries.Count() == 1);
                Assert.That(child2.Entries.Count() == 1);
                Assert.That(grandChild2.Entries.Count() == 0);
            }
        }

        [Test]
        public void Write_Then_Read_NestedBFast_WithArray()
        {
            var bfast = new BFast();
            var child = new BFast();
            var grandChild = new BFast();

            bfast.SetBFast("child", child);
            child.SetBFast("grandChild", grandChild);
            grandChild.SetArray("A", new int[3] { 0, 1, 2 });


            bfast.Write(ResultPath);
            using (var stream = File.OpenRead(ResultPath))
            {
                var other = new BFast(stream);
                var child2 = other.GetBFast("child");
                var grandChild2 = child2.GetBFast("grandChild");
                var result = grandChild2.GetArray<int>("A");

                Assert.That(other.Entries.Count() == 1);
                Assert.That(child2.Entries.Count() == 1);
                Assert.That(grandChild2.Entries.Count() == 1);
                Assert.That(result, Is.EqualTo(new int[3] { 0, 1, 2 }));
            }
        }

        [Test]
        public void Write_Then_Read_Mixed_Sources()
        {
            var basic = new BFast();
            var dummy = new MemoryStream();
            basic.SetArray("ints", new int[1] { 1 });
            basic.SetArray("floats", new float[1] { 2.0f });
            basic.Write(dummy);

            using (var residence = File.OpenRead(ResidencePath))
            {
                dummy.Seek(0, SeekOrigin.Begin);
                var input = new BFast(dummy);

                var inputResidence = new BFast(residence);
                var output = new BFast();

                output.SetBFast("input", input);
                output.SetBFast("residence", inputResidence);
                output.Write(ResultPath2);
            }

            using (var stream = File.OpenRead(ResultPath2))
            {
                var bfast = new BFast(stream);
                var input = bfast.GetBFast("input");
                var residence = bfast.GetBFast("residence");
                var geometry = residence.GetBFast("geometry");

                Assert.That(bfast.Entries.Count() == 2);
                Assert.That(input.Entries.Count() == 2);
                Assert.That(residence.Entries.Count() == 5);
                Assert.That(geometry.Entries.Count() == 17);
            }
        }
    }
}
