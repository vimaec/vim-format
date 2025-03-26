using NUnit.Framework;
using System.IO;
using Vim.BFast;
using Vim.Math3d;
using Vim.Util.Tests;

namespace Vim.Format.Tests;

[TestFixture]
public static class GeometricEntitiesTest
{
    [Test]
    public static void TestRawReadWrite()
    {
        var ctx = new CallerTestContext();
        var dir = ctx.PrepareDirectory();

        // Test Vector3 raw read/write
        {
            var vectorData = new Vector3[20];
            for (var i = 0; i < vectorData.Length; i++)
                vectorData[i] = new Vector3(i, i * i, i * i * i);

            var vectorBuffer = vectorData.ToNamedBuffer("vectors");

            var vectorFile = Path.Combine(dir, "vectors.buf");
            using (var fs = File.Open(vectorFile, FileMode.Create))
            {
                vectorBuffer.Write(fs);
            }

            using (var fs = File.OpenRead(vectorFile))
            {
                var vectorsRead = fs.ReadArray<Vector3>(vectorData.Length);
                Assert.Greater(vectorsRead.Length, 0);

                for (var i = 0; i < vectorsRead.Length; i++)
                {
                    var vectorExpected = vectorData[i];
                    var vectorActual = vectorsRead[i];
                    Assert.IsTrue(vectorExpected.X == vectorActual.X);
                    Assert.IsTrue(vectorExpected.Y == vectorActual.Y);
                    Assert.IsTrue(vectorExpected.Z == vectorActual.Z);
                }
            }
        }
        
        // Test Matrix4x4 raw read/write
        {
            var matrixData = new Matrix4x4[20];
            for (var i = 0; i < matrixData.Length; i++)
            {
                matrixData[i] = new Matrix4x4(
                    i,
                    i + 1,
                    i + 2,
                    i + 3,
                    i + 4,
                    i + 5,
                    i + 6,
                    i + 7,
                    i + 8,
                    i + 9,
                    i + 10,
                    i + 11,
                    i + 12,
                    i + 13,
                    i + 14,
                    i + 15);
            }

            var matrixBuffer = matrixData.ToNamedBuffer("matrices");

            var matrixFile = Path.Combine(dir, "matrices.buf");
            using (var fs = File.Open(matrixFile, FileMode.Create))
            {
                matrixBuffer.Write(fs);
            }

            using (var fs = File.OpenRead(matrixFile))
            {
                var matricesRead = fs.ReadArray<Matrix4x4>(matrixData.Length);
                Assert.Greater(matricesRead.Length, 0);
                for (var i = 0; i < matricesRead.Length; i++)
                {
                    var matrixExpectedFloats = matrixData[i].ToFloats();
                    Assert.Greater(matrixExpectedFloats.Length, 0);
                    var matrixActualFloats = matricesRead[i].ToFloats();

                    for (var j = 0; j < matrixExpectedFloats.Length; j++)
                    {
                        Assert.AreEqual(matrixExpectedFloats[j], matrixActualFloats[j]);
                    }
                }
            }
        }
    }
}
