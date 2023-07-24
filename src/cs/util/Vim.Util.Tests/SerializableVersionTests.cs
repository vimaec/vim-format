using NUnit.Framework;

namespace Vim.Util.Tests
{
    [TestFixture]
    public static class SerializableVersionTests
    {
        [Test]
        public static void TestParse()
        {
            var failures = new[]
            {
                SerializableVersion.Parse(null),
                SerializableVersion.Parse(""),
                SerializableVersion.Parse("aeafef"),
                SerializableVersion.Parse("1.b"),
                SerializableVersion.Parse("1.1.e"),
                SerializableVersion.Parse("a.1.0"),
            };
            foreach (var v in failures)
            {
                Assert.IsNull(v);
            }

            var v1_permutations = new[]
            {
                SerializableVersion.Parse("1"),
                SerializableVersion.Parse("1.0"),
                SerializableVersion.Parse("1.0.0")
            };
            foreach (var v in v1_permutations)
            {
                Assert.IsNotNull(v);
                Assert.AreEqual(1, v.Major);
                Assert.AreEqual(0, v.Minor);
                Assert.AreEqual(0, v.Patch);
                Assert.IsTrue(string.IsNullOrEmpty(v.Qualifier));
                Assert.IsTrue(v.IsEqualTo(v));
                Assert.IsTrue(v.IsGreaterThanOrEqual(v));
            }

            var v1_2_3 = SerializableVersion.Parse("1.2.3");
            Assert.AreEqual(1, v1_2_3.Major);
            Assert.AreEqual(2, v1_2_3.Minor);
            Assert.AreEqual(3, v1_2_3.Patch);
            Assert.IsTrue(string.IsNullOrEmpty(v1_2_3.Qualifier));
            Assert.IsTrue(v1_2_3.IsEqualTo(v1_2_3));

            var v4_5_6_z = SerializableVersion.Parse("4.5.6.z");
            Assert.AreEqual(4, v4_5_6_z.Major);
            Assert.AreEqual(5, v4_5_6_z.Minor);
            Assert.AreEqual(6, v4_5_6_z.Patch);
            Assert.AreEqual("z", v4_5_6_z.Qualifier);
            Assert.IsTrue(v4_5_6_z.IsEqualTo(v4_5_6_z));
        }

        [Test]
        public static void TestVersionComparison()
        {
            var cases = new[]
            {
                (SerializableVersion.Parse("0"), SerializableVersion.Parse("1")),
                (SerializableVersion.Parse("0.0.0"), SerializableVersion.Parse("0.0.1")),
                (SerializableVersion.Parse("0.0.1"), SerializableVersion.Parse("0.1.0")),
                (SerializableVersion.Parse("0.1.0"), SerializableVersion.Parse("1.0.0")),
                (SerializableVersion.Parse("1.0.0"), SerializableVersion.Parse("1.0.0.a")),
                (SerializableVersion.Parse("1.0.0.a"), SerializableVersion.Parse("1.0.0.aa")),
                (SerializableVersion.Parse("1.0.0.a"), SerializableVersion.Parse("1.0.0.b")),
                (SerializableVersion.Parse("1.0.0.aa"), SerializableVersion.Parse("1.0.0.b")),
                (SerializableVersion.Parse("1.0.0.b"), SerializableVersion.Parse("1.0.0.bb")),
            };

            foreach (var (a, b) in cases)
            {
                Assert.IsNotNull(a);
                Assert.IsNotNull(b);
                Assert.IsTrue(a.IsLessThan(b));
                Assert.IsFalse(b.IsLessThan(a));
                Assert.IsTrue(b.IsGreaterThanOrEqual(a));
                Assert.IsFalse(a.IsGreaterThanOrEqual(b));
                Assert.IsFalse(a.Equals(b));
                Assert.IsFalse(b.Equals(a));
            }

            // Special case that tripped up some logic:
            var v4_4_0_a = SerializableVersion.Parse("4.4.0.a");
            Assert.IsTrue(v4_4_0_a.IsGreaterThanOrEqual(v4_4_0_a));
        }
    }
}
