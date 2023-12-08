using NUnit.Framework;
using System;
using Vim.Util;
using Vim.Util.Tests;

namespace Vim.BFastNS.Tests
{
    public class BFastTests
    {
        public static string RootFolder = System.IO.Path.Combine(VimFormatRepoPaths.ProjDir, "..", "..");
        public static string Path = System.IO.Path.Combine(RootFolder, "out/input.bfast");
        public static string OutputPath = System.IO.Path.Combine(RootFolder, "out/output.bfast");
        public static string ResidencePath = VimFormatRepoPaths.GetLatestWolfordResidenceVim();

        [SetUp]
        public void Setup()
        {
            File.Delete(Path);
            File.Delete(OutputPath);
        }

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
            Assert.IsNull(bfast.GetArray<int>("missing"));
        }

        [Test]
        public void EmptyBFast_GetBfast_Returns_Null()
        {
            var bfast = new BFast();
            Assert.IsNull(bfast.GetBFast("missing"));
        }

        [Test]
        public void EmptyBFast_Remove_Does_Nothing()
        {
            var bfast = new BFast();
            bfast.Remove("missing");
        }

        [Test]
            public void EmptyBFast_GetSize_Return_64()
        {
            var bfast = new BFast();
            Assert.That(bfast.GetSize(), Is.EqualTo(64));
        }

        [Test]
        public void EmptyBFast_Writes_Header()
        {
            var bfast = new BFast();
            bfast.Write(Path);
            using (var stream = File.OpenRead(Path))
            {
                var bfast2 = new BFast(stream);
                Assert.That(bfast2.GetSize(), Is.EqualTo(64));
            }
        }

        [Test]
        public void SetEnumerable_Adds_Entry()
        {
            var bfast = new BFast();
            bfast.SetEnumerable("A", () => new int[3] { 0, 1, 2 });
            Assert.That(bfast.Entries.Count(), Is.EqualTo(1));
        }


        [Test]
        public void SetEnumerable_Then_GetArray()
        {
            var bfast = new BFast();
            bfast.SetEnumerable("A", () => new int[3] { 0, 1, 2 });
            var result = bfast.GetArray<int>("A");

            Assert.That(result, Is.EqualTo(new int[3] { 0, 1, 2 }));
        }

        [Test]
        public void SetEnumerable_Then_GetArray_Bytes()
        {
            var bfast = new BFast();
            bfast.SetEnumerable("A", () => new int[3] { 0, 1, 2 });
            var result = bfast.GetArray<byte>("A");

            var bytes = (new int[3] { 0, 1, 2 }).SelectMany(i => BitConverter.GetBytes(i));
            Assert.That(result, Is.EqualTo(bytes));
        }


        [Test]
        public void SetEnumerable_Then_GetEnumerable_Bytes()
        {
            var bfast = new BFast();
            bfast.SetEnumerable("A", () => new int[3] { 0, 1, 2 });
            var result = bfast.GetEnumerable<byte>("A");

            var bytes = (new int[3] { 0, 1, 2 }).SelectMany(i => BitConverter.GetBytes(i));
            Assert.That(result, Is.EqualTo(bytes));
        }

        [Test]
        public void SetEnumerable_Then_GetEnumerable_Float()
        {
            var bfast = new BFast();
            bfast.SetEnumerable("A", () => new int[3] { 0, 1, 2 });
            var result = bfast.GetEnumerable<float>("A");

            var floats = (new int[3] { 0, 1, 2 }).Select(i => BitConverter.Int32BitsToSingle(i));
            Assert.That(result, Is.EqualTo(floats));
        }

        [Test]
        public void SetEnumerable_Then_GetArray_Float()
        {
            var bfast = new BFast();
            bfast.SetEnumerable("A", () => new int[3] { 0, 1, 2 });
            var result = bfast.GetArray<float>("A");

            var floats = (new int[3] { 0, 1, 2 }).Select(i => BitConverter.Int32BitsToSingle(i));
            Assert.That(result, Is.EqualTo(floats));
        }


        [Test]
        public void SetEnumerable_Then_GetEnumerable()
        {
            var bfast = new BFast();
            bfast.SetEnumerable("A", () => new int[3] { 0, 1, 2 });
            var result = bfast.GetEnumerable<int>("A").ToArray();
            Assert.That(result, Is.EqualTo(new int[3] { 0, 1, 2 }));
        }

        [Test]
        public void SetEnumerable_Then_GetBFast()
        {
            var bfast = new BFast();
            bfast.SetEnumerable("A", () => new int[3] { 0, 1, 2 });
            var result = bfast.GetBFast("A");
            Assert.That(result, Is.Null);
        }

        IEnumerable<int> GetLots()
        {
            return Enumerable.Range(0, int.MaxValue).Concat(Enumerable.Range(0, 10));
        }


        [Test, Explicit]
        public void SetEnumerable_Then_GetEnumerable_Lots()
        {
            var bfast = new BFast();
            bfast.SetEnumerable<int>("A", GetLots);

            var result = bfast.GetEnumerable<int>("A");
            Assert.That(result, Is.EqualTo(GetLots()));
        }


        [Test]
        public void SetArray_Adds_Entry()
        {
            var bfast = new BFast();
            bfast.SetArray("A", new int[3] { 0, 1, 2 });

            Assert.That(bfast.Entries.Count(), Is.EqualTo(1));
        }

        [Test]
        public void SetArray_Then_GetArray()
        {
            var bfast = new BFast();
            bfast.SetArray("A", new int[3] { 0, 1, 2 });
            var result = bfast.GetArray<int>("A");

            Assert.That(result, Is.EqualTo(new int[3] { 0, 1, 2 }));
        }

        [Test]
        public void SetArray_Then_GetArray_Bytes()
        {
            var bfast = new BFast();
            bfast.SetArray("A", new int[3] { 0, 1, 2 });
            var result = bfast.GetArray<byte>("A");

            var bytes = (new int[3] { 0, 1, 2 }).SelectMany(i => BitConverter.GetBytes(i));
            Assert.That(result, Is.EqualTo(bytes));
        }

        [Test]
        public void SetArray_Then_GetArray_Float()
        {
            var bfast = new BFast();
            bfast.SetArray("A", new int[3] { 0, 1, 2 });
            var result = bfast.GetArray<float>("A");

            var floats = (new int[3] { 0, 1, 2 }).Select(i => BitConverter.Int32BitsToSingle(i));
            Assert.That(result, Is.EqualTo(floats));
        }

        [Test]
        public void SetArray_Then_GetEnumerable()
        {
            var bfast = new BFast();
            bfast.SetArray("A", new int[3] { 0, 1, 2 });
            var result = bfast.GetEnumerable<int>("A");

            Assert.That(result, Is.EqualTo(new int[3] { 0, 1, 2 }));
        }

        [Test]
        public void SetArray_Then_GetEnumerable_Bytes()
        {
            var bfast = new BFast();
            bfast.SetArray("A", new int[3] { 0, 1, 2 });
            var result = bfast.GetEnumerable<byte>("A");

            var bytes = (new int[3] { 0, 1, 2 }).SelectMany(i => BitConverter.GetBytes(i));
            Assert.That(result, Is.EqualTo(bytes));
        }

        [Test]
        public void SetArray_Then_GetEnumerable_Float()
        {
            var bfast = new BFast();
            bfast.SetArray("A", new int[3] { 0, 1, 2 });
            var result = bfast.GetEnumerable<float>("A");

            var floats = (new int[3] { 0, 1, 2 }).Select(i => BitConverter.Int32BitsToSingle(i));
            Assert.That(result, Is.EqualTo(floats));
        }

        [Test]
        public void SetArray_Then_GetBFast_Returns_Null()
        {
            var bfast = new BFast();
            bfast.SetArray("A", new int[3] { 0, 1, 2 });
            var result = bfast.GetBFast("A");

            Assert.IsNull(result);
        }

        [Test]
        public void SetArray_Then_SetArray_Replaces()
        {
            var bfast = new BFast();
            bfast.SetArray("A", new int[3] { 0, 1, 2 });
            bfast.SetArray("A", new float[3] { 0.1f, 0.2f, 0.3f });
            var result = bfast.GetArray<float>("A");

            Assert.That(result, Is.EqualTo(new float[3] { 0.1f, 0.2f, 0.3f }));
        }

        [Test]
        public void SetArray_Then_SetBFast_Replaces()
        {
            var bfast = new BFast();

            bfast.SetArray("A", new int[3] { 0, 1, 2 });
            bfast.SetBFast("A", new BFast());
            var result = bfast.GetArray<int>("A");
            Assert.That(result.Length > 3);
        }

        [Test]
        public void SetBFast_Adds_Entry()
        {
            var bfast = new BFast();
            bfast.SetBFast("A", new BFast());

            Assert.That(bfast.Entries.Count(), Is.EqualTo(1));
        }

        [Test]
        public void SetBFast_Then_GetBFast_Returns_Same()
        {
            var bfast = new BFast();
            var b = new BFast();
            bfast.SetBFast("A", b);
            var result = bfast.GetBFast("A");

            Assert.That(result, Is.EqualTo(b));
        }

        [Test]
        public void Inflate_NonDeflated_Throws()
        {
            var bfast = new BFast();
            var b = new BFast();
            bfast.SetBFast("A", b);
            Assert.That(() => bfast.GetBFast("A", inflate: true), Throws.Exception);
        }

        [Test]
        public void Deflate_Inflate_Works()
        {
            var bfast = new BFast();
            var b = new BFast();
            b.SetArray("B", new int[3] { 0, 1, 2 });
            bfast.SetBFast("A", b, deflate : true);

            var result = bfast.GetBFast("A", inflate: true);
            var b2 = result.GetArray<int>("B");
            Assert.That(result.Entries.Count(), Is.EqualTo(1));
            Assert.That(b2, Is.EqualTo(new int[3] { 0, 1, 2 }));
        }

        [Test]
        public void SetBFast_Then_SetBFast_Replaces()
        {
            var bfast = new BFast();
            var a = new BFast();
            var b = new BFast();
            bfast.SetBFast("A", a);
            bfast.SetBFast("A", b);
            var result = bfast.GetBFast("A");
            Assert.That(a != b);
            Assert.That(result == b);
        }

        [Test]
        public void SetBFast_Then_SetArray_Replaces()
        {
            var bfast = new BFast();
            bfast.SetBFast("A", new BFast());
            bfast.SetArray("A", new int[3] { 0, 1, 2 });
            var result = bfast.GetBFast("A");
            Assert.IsNull(result);
        }

        [Test]
        public void Remove_Missing_DoesNothing()
        {
            var bfast = new BFast();
            bfast.Remove("A");
            Assert.That(bfast.Entries.Count() == 0);
        }

        [Test]
        public void Remove_Array()
        {
            var bfast = new BFast();
            bfast.SetArray("A", new int[3] { 0, 1, 2 });
            bfast.Remove("A");
            Assert.IsNull(bfast.GetArray<int>("A"));
            Assert.That(bfast.Entries.Count() == 0);
        }

        [Test]
        public void Remove_BFast()
        {
            var bfast = new BFast();
            bfast.SetBFast("A", new BFast());
            bfast.Remove("A");
            Assert.IsNull(bfast.GetBFast("A"));
            Assert.That(bfast.Entries.Count() == 0);
        }

        [Test]
        public void Removed_BFast_Not_Written()
        {
            var bfast = new BFast();
            bfast.SetBFast("A", new BFast());
            bfast.Remove("A");

            bfast.Write(Path);

            using (var stream = File.OpenRead(Path))
            {
                var other = new BFast(stream);
                Assert.IsNull(other.GetBFast("A"));
                Assert.That(other.Entries.Count() == 0);
            }
        }

        [Test]
        public void Removed_Array_Not_Written()
        {
            var bfast = new BFast();
            bfast.SetArray("A", new int[3] { 0, 1, 2 });
            bfast.Remove("A");
            bfast.Write(Path);

            using (var stream = File.OpenRead(Path))
            {
                var other = new BFast(stream);
                Assert.IsNull(other.GetArray<int>("A"));
                Assert.That(other.Entries.Count() == 0);
            }
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
                input.Write(Path);
            }

            using (var stream = File.OpenRead(Path))
            {
                var bfast = new BFast(stream);
                var geometry = bfast.GetBFast("geometry");
                Assert.That(bfast.Entries.Count() == 5);
                Assert.That(geometry.Entries.Count() == 16);
                Assert.IsNull(geometry.GetArray<float>("g3d:vertex:position:0:float32:3"));
            }
        }

        [Test]
        public void Write_Then_Read_Array()
        {
            var bfast = new BFast();

            bfast.SetArray("A", new int[3] { 0, 1, 2 });
            bfast.Write(Path);

            using (var stream = File.OpenRead(Path))
            {
                var other = new BFast(stream);
                var result = other.GetArray<int>("A");
                Assert.That(result, Is.EqualTo(new int[3] { 0, 1, 2 }));
            }
        }

        [Test]
        public void Write_Then_Read_Enumerable()
        {
            var bfast = new BFast();

            bfast.SetEnumerable("A", () => new int[3] { 0, 1, 2 });
            bfast.Write(Path);

            using (var stream = File.OpenRead(Path))
            {
                var other = new BFast(stream);
                var array = other.GetArray<int>("A");
                var enumerable = other.GetEnumerable<int>("A");
                Assert.That(array, Is.EqualTo(new int[3] { 0, 1, 2 }));
                Assert.That(enumerable, Is.EqualTo(new int[3] { 0, 1, 2 }));
            }
        }

        [Test]
        public void Write_Then_Read_SimpleBFast()
        {
            var bfast = new BFast();
            var child = new BFast();

            bfast.SetBFast("child", child);
            child.SetArray("A", new int[3] { 0, 1, 2 });
            bfast.Write(Path);

            using (var stream = File.OpenRead(Path))
            {
                var other = new BFast(stream);
                var child2 = other.GetBFast("child");
                var result = child2.GetArray<int>("A");

                Assert.That(other.Entries.Count() == 1);
                Assert.That(child2.Entries.Count() == 1);
                Assert.That(result, Is.EqualTo(new int[3] { 0, 1, 2 }));
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
            bfast.Write(Path);

            using (var stream = File.OpenRead(Path))
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


            bfast.Write(Path);
            using (var stream = File.OpenRead(Path))
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
            basic.SetArray("ints", new int[1] { 1 });
            basic.SetArray("floats", new float[1] { 2.0f });
            basic.Write(Path);

            using (var stream = File.OpenRead(Path))
            {
                using (var residence = File.OpenRead(ResidencePath))
                {
                    var input = new BFast(stream);
                    var inputResidence = new BFast(residence);
                    var output = new BFast();

                    output.SetBFast("input", input);
                    output.SetBFast("residence", inputResidence);
                    output.Write(OutputPath);
                }
            }

            using (var stream = File.OpenRead(OutputPath))
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
