using NUnit.Framework;
using Vim.Util.Tests;

namespace Vim.BFastNext.Tests
{
    public class BFastNextTests
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
            var bfast = new BFastNext();
            Assert.That(bfast.Entries.Count(), Is.EqualTo(0));
        }

        [Test]
        public void EmptyBFast_GetArray_Returns_Null()
        {
            var bfast = new BFastNext();
            Assert.IsNull(bfast.GetArray<int>("missing"));
        }

        [Test]
        public void EmptyBFast_GetBfast_Returns_Null()
        {
            var bfast = new BFastNext();
            Assert.IsNull(bfast.GetBFast("missing"));
        }

        [Test]
        public void EmptyBFast_Remove_Does_Nothing()
        {
            var bfast = new BFastNext();
            bfast.Remove("missing");
        }

        [Test]
        public void EmptyBFast_GetSize_Return_64()
        {
            var bfast = new BFastNext();
            Assert.That(bfast.GetSize(), Is.EqualTo(64));
        }

        [Test]
        public void EmptyBFast_Writes_Header()
        {
            var bfast = new BFastNext();
            bfast.Write(Path);
            using (var stream = File.OpenRead(Path))
            {
                var bfast2 = new BFastNext(stream);
                Assert.That(bfast2.GetSize(), Is.EqualTo(64));
            }
        }

        [Test]
        public void SetArray_Adds_Entry()
        {
            var bfast = new BFastNext();
            bfast.AddArray("A", new int[3] { 0, 1, 2 });

            Assert.That(bfast.Entries.Count(), Is.EqualTo(1));
        }

        [Test]
        public void SetArray_Then_GetArray()
        {
            var bfast = new BFastNext();
            bfast.AddArray("A", new int[3] { 0, 1, 2 });
            var result = bfast.GetArray<int>("A");

            Assert.That(result, Is.EqualTo(new int[3] { 0, 1, 2 }));
        }

        [Test]
        public void SetArray_Then_GetArray_Bytes()
        {
            var bfast = new BFastNext();
            bfast.AddArray("A", new int[3] { 0, 1, 2 });
            var result = bfast.GetArray<byte>("A");

            var bytes = (new int[3] { 0, 1, 2 }).SelectMany(i => BitConverter.GetBytes(i));
            Assert.That(result, Is.EqualTo(bytes));
        }

        [Test]
        public void SetArray_Then_GetArray_Float()
        {
            var bfast = new BFastNext();
            bfast.AddArray("A", new int[3] { 0, 1, 2 });
            var result = bfast.GetArray<float>("A");

            var floats = (new int[3] { 0, 1, 2 }).Select(i => BitConverter.Int32BitsToSingle(i));
            Assert.That(result, Is.EqualTo(floats));
        }

        [Test]
        public void SetArray_Then_GetBFast_Returns_Null()
        {
            var bfast = new BFastNext();
            bfast.AddArray("A", new int[3] { 0, 1, 2 });
            var result = bfast.GetBFast("A");

            Assert.IsNull(result);
        }

        [Test]
        public void SetArray_Then_SetArray_Replaces()
        {
            var bfast = new BFastNext();
            bfast.AddArray("A", new int[3] { 0, 1, 2 });
            bfast.AddArray("A", new float[3] { 0.1f, 0.2f, 0.3f });
            var result = bfast.GetArray<float>("A");

            Assert.That(result, Is.EqualTo(new float[3] { 0.1f, 0.2f, 0.3f }));
        }

        [Test]
        public void SetArray_Then_SetBFast_Replaces()
        {
            var bfast = new BFastNext();

            bfast.AddArray("A", new int[3] { 0, 1, 2 });
            bfast.AddBFast("A", new BFastNext());
            var result = bfast.GetArray<int>("A");
            Assert.That(result.Length > 3);
        }

        [Test]
        public void SetBFast_Adds_Entry()
        {
            var bfast = new BFastNext();
            bfast.AddBFast("A", new BFastNext());

            Assert.That(bfast.Entries.Count(), Is.EqualTo(1));
        }

        [Test]
        public void SetBFast_Then_GetBFast_Returns_Same()
        {
            var bfast = new BFastNext();
            var b = new BFastNext();
            bfast.AddBFast("A", b);
            var result = bfast.GetBFast("A");

            Assert.That(result, Is.EqualTo(b));
        }

        [Test]
        public void SetBFast_Then_SetBFast_Replaces()
        {
            var bfast = new BFastNext();
            var a = new BFastNext();
            var b = new BFastNext();
            bfast.AddBFast("A", a);
            bfast.AddBFast("A", b);
            var result = bfast.GetBFast("A");
            Assert.That(a != b);
            Assert.That(result == b);
        }

        [Test]
        public void SetBFast_Then_SetArray_Replaces()
        {
            var bfast = new BFastNext();
            bfast.AddBFast("A", new BFastNext());
            bfast.AddArray("A", new int[3] { 0, 1, 2 });
            var result = bfast.GetBFast("A");
            Assert.IsNull(result);
        }

        [Test]
        public void Remove_Missing_DoesNothing()
        {
            var bfast = new BFastNext();
            bfast.Remove("A");
            Assert.That(bfast.Entries.Count() == 0);
        }

        [Test]
        public void Remove_Array()
        {
            var bfast = new BFastNext();
            bfast.AddArray("A", new int[3] { 0, 1, 2 });
            bfast.Remove("A");
            Assert.IsNull(bfast.GetArray<int>("A"));
            Assert.That(bfast.Entries.Count() == 0);
        }

        [Test]
        public void Remove_BFast()
        {
            var bfast = new BFastNext();
            bfast.AddBFast("A", new BFastNext());
            bfast.Remove("A");
            Assert.IsNull(bfast.GetBFast("A"));
            Assert.That(bfast.Entries.Count() == 0);
        }

        [Test]
        public void Removed_BFast_Not_Written()
        {
            var bfast = new BFastNext();
            bfast.AddBFast("A", new BFastNext());
            bfast.Remove("A");

            bfast.Write(Path);

            using (var stream = File.OpenRead(Path))
            {
                var other = new BFastNext(stream);
                Assert.IsNull(other.GetBFast("A"));
                Assert.That(other.Entries.Count() == 0);
            }
        }

        [Test]
        public void Removed_Array_Not_Written()
        {
            var bfast = new BFastNext();
            bfast.AddArray("A", new int[3] { 0, 1, 2 });
            bfast.Remove("A");
            bfast.Write(Path);

            using (var stream = File.OpenRead(Path))
            {
                var other = new BFastNext(stream);
                Assert.IsNull(other.GetArray<int>("A"));
                Assert.That(other.Entries.Count() == 0);
            }
        }

        [Test]
        public void Removed_InChild_Not_Written()
        {
            using (var residence = File.OpenRead(ResidencePath))
            {
                var input = new BFastNext(residence);
                var geometry = input.GetBFast("geometry");
                geometry.Remove("g3d:vertex:position:0:float32:3");
                input.AddBFast("geometry", geometry);
                input.Write(Path);
            }

            using (var stream = File.OpenRead(Path))
            {
                var bfast = new BFastNext(stream);
                var geometry = bfast.GetBFast("geometry");
                Assert.That(bfast.Entries.Count() == 5);
                Assert.That(geometry.Entries.Count() == 16);
                Assert.IsNull(geometry.GetArray<float>("g3d:vertex:position:0:float32:3"));
            }
        }

        [Test]
        public void Write_Then_Read_Array()
        {
            var bfast = new BFastNext();

            bfast.AddArray("A", new int[3] { 0, 1, 2 });
            bfast.Write(Path);

            using (var stream = File.OpenRead(Path))
            {
                var other = new BFastNext(stream);
                var result = other.GetArray<int>("A");
                Assert.That(result, Is.EqualTo(new int[3] { 0, 1, 2 }));
            }
        }

        [Test]
        public void Write_Then_Read_SimpleBFast()
        {
            var bfast = new BFastNext();
            var child = new BFastNext();

            bfast.AddBFast("child", child);
            child.AddArray("A", new int[3] { 0, 1, 2 });
            bfast.Write(Path);

            using (var stream = File.OpenRead(Path))
            {
                var other = new BFastNext(stream);
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
            var bfast = new BFastNext();
            var child = new BFastNext();
            var grandChild = new BFastNext();

            bfast.AddBFast("child", child);
            child.AddBFast("grandChild", grandChild);
            bfast.Write(Path);

            using (var stream = File.OpenRead(Path))
            {
                var other = new BFastNext(stream);
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
            var bfast = new BFastNext();
            var child = new BFastNext();
            var grandChild = new BFastNext();

            bfast.AddBFast("child", child);
            child.AddBFast("grandChild", grandChild);
            grandChild.AddArray("A", new int[3] { 0, 1, 2 });


            bfast.Write(Path);
            using (var stream = File.OpenRead(Path))
            {
                var other = new BFastNext(stream);
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
            var basic = new BFastNext();
            basic.AddArray("ints", new int[1] { 1 });
            basic.AddArray("floats", new float[1] { 2.0f });
            basic.Write(Path);

            using (var stream = File.OpenRead(Path))
            {
                using (var residence = File.OpenRead(ResidencePath))
                {
                    var input = new BFastNext(stream);
                    var inputResidence = new BFastNext(residence);
                    var output = new BFastNext();

                    output.AddBFast("input", input);
                    output.AddBFast("residence", inputResidence);
                    output.Write(OutputPath);
                }
            }

            using (var stream = File.OpenRead(OutputPath))
            {
                var bfast = new BFastNext(stream);
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