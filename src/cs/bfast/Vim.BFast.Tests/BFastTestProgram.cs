/*
    BFAST - Binary Format for Array Streaming and Transmission
    Copyright 2019, VIMaec LLC
    Copyright 2018, Ara 3D, Inc.
    Usage licensed under terms of MIT License
	https://github.com/vimaec/bfast
*/

using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Vim.BFast.Tests
{
    public static class BFastTests
    {
        public static int Mb = 1000 * 1000;
        public static int Gb = 1000 * Mb;

        static byte[] ByteArray(int numBytes) =>
            Enumerable.Range(0, numBytes).Select(i => (byte)i).ToArray();

        static readonly byte[] Array1MB
            = ByteArray(Mb);

        static readonly double[] Array1GB
            = Enumerable.Range(0, Gb / 8).Select(i => (double)i).ToArray();

        public static (string, byte[])[] ZeroBuffers
            = Enumerable.Empty<(string, byte[])>().ToArray();

        public static (string, byte[])[] Ten1MBBuffers
            = Enumerable.Range(0, 10).Select(i => (i.ToString(), Array1MB)).ToArray();

        public static (string, double[])[] One1GBBuffer
            => Enumerable.Range(0, 1).Select(i => (i.ToString(), Array1GB)).ToArray();

        public static void TestBFastBytes(byte[] bytes)
        {
            Console.WriteLine($"Size of buffer = {bytes.Length}");
            Console.WriteLine($"First 8 bytes = {string.Join(", ", bytes.Take(8))}");
        }

        public class DisposableTimer : IDisposable
        {
            Stopwatch Stopwatch = Stopwatch.StartNew();

            public void Dispose()
                => Console.WriteLine($"Elapsed = {Stopwatch.ElapsedMilliseconds / 1000}s {Stopwatch.ElapsedMilliseconds % 1000}ms");
        }

        public static DisposableTimer CreateTimer(string message = null)
        {
            Console.WriteLine($"Starting timer {message ?? string.Empty}");
            return new DisposableTimer();
        }

        [Test]
        public static void TestStringPacking()
        {
            var noStrings = new string[0];
            var oneStrings = new[] { "" };
            var twoStrings = new[] { "", "ab" };
            var threeStrings = new[] { "a", "b", "" };
            var noPacked = BFast.PackStrings(noStrings);
            var onePacked = BFast.PackStrings(oneStrings);
            var twoPacked = BFast.PackStrings(twoStrings);
            var threePacked = BFast.PackStrings(threeStrings);
            Assert.AreEqual(0, noPacked.Length);
            Assert.AreEqual(1, onePacked.Length);
            Assert.AreEqual(4, twoPacked.Length);
            Assert.AreEqual(5, threePacked.Length);
            Assert.AreEqual(noStrings, BFast.UnpackStrings(noPacked));
            Assert.AreEqual(oneStrings, BFast.UnpackStrings(onePacked));
            Assert.AreEqual(twoStrings, BFast.UnpackStrings(twoPacked));
            Assert.AreEqual(threeStrings, BFast.UnpackStrings(threePacked));
        }

        [Test]
        public static void BasicTests()
        {
            using (CreateTimer("ZeroBuffers"))
            {
                var bytes = BFast.WriteBFastToBytes(ZeroBuffers);
                TestBFastBytes(bytes);
                var tmp = BFast.ReadBFast(bytes).ToArray();
                Assert.AreEqual(0, tmp.Length);
            }
            using (CreateTimer("Ten1MBBuffers"))
            {
                var bytes = BFast.WriteBFastToBytes(Ten1MBBuffers);
                TestBFastBytes(bytes);
                var tmp = BFast.ReadBFast(bytes).ToArray();
                Assert.AreEqual(10, tmp.Length);
                Assert.AreEqual(tmp.Select(x => x.Name).ToArray(), Enumerable.Range(0, 10).Select(x => x.ToString()).ToArray());
                Assert.AreEqual(tmp.Select(x => (int)x.NumBytes()).ToArray(), Enumerable.Repeat(Mb, 10).ToArray());

                for (var i = 0; i < 10; ++i)
                    Assert.AreEqual(Ten1MBBuffers[i].Item2, tmp[i].ToBytes(), $"Buffer {i} are different");
            }
            using (CreateTimer("OneGBBuffer"))
            {
                //Enumerable.Range(0, Gb).Select(i => (double)i).ToArray()
                var bytes = BFast.WriteBFastToBytes(One1GBBuffer);
                TestBFastBytes(bytes);
                var tmp = BFast.ReadBFast(bytes).ToArray();
                Assert.AreEqual(1, tmp.Length);
                Assert.AreEqual(tmp.Select(x => x.Name).ToArray(), new[] { "0" });
                Assert.AreEqual(tmp.Select(x => x.NumBytes()).ToArray(), Enumerable.Repeat((long)Gb, 1).ToArray());
            }
        }

        public static BFastBuilder BFastWithSubs(int numBuffers, int numLevels, Func<int> numBytes)
            => Enumerable.Range(0, numBuffers).Aggregate(new BFastBuilder(),
                (bld, i) => bld.Add(i.ToString(),
                    numLevels > 0
                        ? BFastWithSubs(numBuffers, numLevels - 1, numBytes)
                        : BFastRoot(numBuffers, numBytes))
                );

        public static BFastBuilder BFastRoot(int numBuffers, Func<int> numBytes)
            => Enumerable.Range(0, numBuffers).Aggregate(new BFastBuilder(), (bld, i) => bld.Add(i.ToString(), ByteArray(numBytes()).ToBuffer()));

        public static void ValidateBFast(byte[] buffer, BFastBuilder srcBuilder)
        {
            var bfast = BFast.ReadBFast(buffer).ToArray();

            var names = srcBuilder.BufferNames().ToArray();
            var sizes = srcBuilder.BufferSizes().ToArray();
            var numBuffers = names.Count();
            // We should have the same number of buffers
            AssertEquals(bfast.Length, numBuffers);
            for (var i = 0; i < numBuffers; i++)
            {
                // Of equal size
                AssertEquals(bfast[i].Name, names[i]);
                AssertEquals(bfast[i].Data.Length, sizes[i]);
                // And they might be sub-buffers
                if (srcBuilder.Children[i].Item2 is BFastBuilder childBuilder)
                    ValidateBFast(bfast[i].ToBytes(), childBuilder);
            }
        }

        [Test]
        public static void TestNestedBFast()
        {
            var random = new Random(1234567);
            // Create a nested BFast structure 3 layers deep with randomly-sized buffers between 1 & 256 bytes size
            var builder = BFastWithSubs(3, 3, () => random.Next(1, 256));
            // Create a buffer to recieve this structure;
            var buffer = new byte[builder.GetSize()];
            var stream = new MemoryStream(buffer, true);
            builder.Write(stream);

            // Now, lets try and deserialize these buffers:
            ValidateBFast(buffer, builder);
        }

        public static void AssertEquals<T>(T x, T y)
        {
            if (!x.Equals(y))
                throw new Exception($"Expected value {x} but instead got {y}");
        }

        /// <summary>
        /// This test cannot be run from the test runner, because the <gcAllowVeryLargeObjects enabled="true" /> App.Config option
        /// has to be enabled from within the host program. 
        /// </summary>
        public static void ReallyBigTest()
        {
            var xs = new Vector3[500 * 1000 * 1000];
            for (var i = 0; i < xs.Length; ++i)
                xs[i] = new Vector3(i, i, i);
            var filePath = Path.Combine(Path.GetTempPath(), "really_big_test.bfast");
            using (var stream = File.OpenWrite(filePath))
                stream.WriteBFast(new[] { ("buffer", xs) });

            var name = "";
            Vector3[] ys;
            using (var stream = File.OpenRead(filePath))
            {
                var buffers = BFast.ReadBFast<Vector3>(stream).ToArray();
                if (buffers.Length != 1)
                    throw new Exception($"Expected exactly one buffer, not {buffers.Length}");
                (name, ys) = (buffers[0].Name, buffers[1].AsArray<Vector3>());
            }
            if (name != "buffer")
                throw new Exception($"Expected name of buffer to be buffer not {name}");
            AssertEquals(xs.Length, ys.Length);
            AssertEquals(xs[0], ys[0]);
            AssertEquals(xs[1], ys[1]);
            AssertEquals(xs[xs.Length - 1], ys[ys.Length - 1]);
        }
    }
}
