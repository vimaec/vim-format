using NUnit.Framework;
using Vim.Util.Tests;
using Vim.Format.SceneBuilder;
using Vim.G3dNext;
using Vim.Format.Geometry;
using Vim.LinqArray;
using Vim.Math3d;
using Vim.G3d;
using System.Linq;
using System.Diagnostics;
using System;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using static Vim.Format.DocumentBuilder;

namespace Vim.Format.Tests;



[TestFixture]
public static class SerializableDocumentTests
{
    [Test]
    public static void TestEmpty()
    {
        var doc = new SerializableDocument();
        Assert.DoesNotThrow(() => doc.ToBFast());
    }

    [Test]
    public static void CanOpenVim()
    {
        var path = VimFormatRepoPaths.GetLatestWolfordResidenceVim();
        var scene = VimScene.LoadVim(path);
        scene.Validate();
    }

    [Test]
    public static void MaterialColor_IsSame()
    {
        var path = VimFormatRepoPaths.GetLatestWolfordResidenceVim();
        var scene = VimScene.LoadVim(path);
        for (var i = 0; i < scene.GetMaterialCount(); i++)
        {
            Assert.AreEqual(scene.GetMaterialColor(i), scene.GetMaterialColorNext(i));
        }
    }

    [Test]
    public static void GetMesh_IsSameMesh()
    {
        var path = VimFormatRepoPaths.GetLatestWolfordResidenceVim();
        var scene = VimScene.LoadVim(path);
        for (var i = 0; i < scene.GetMeshCount(); i++)
        {
            var mesh = scene.Meshes[i];
            var next = scene.MeshesNext[i];
            var raw = scene.MeshesRaw[i];
            MeshesAreSame(mesh, next);
            MeshesAreSame(mesh, raw);
        }
    }

    [Test]
    public static void GetMesh_Transform_IsSame()
    {
        var path = VimFormatRepoPaths.GetLatestWolfordResidenceVim();
        var scene = VimScene.LoadVim(path);
        for (var i = 0; i < scene.GetMeshCount(); i++)
        {
            var mat = Matrix4x4.CreateWorld(
                new Vector3(1, -2, 3),
                new Vector3(0, 0, 1),
                new Vector3(0, 1, 0)
            );
            var mesh = scene.Meshes[i].Transform(mat);
            var next = scene.MeshesNext[i].Transform(mat);
            var raw = scene.MeshesRaw[i].Transform(mat);
            MeshesAreSame(mesh, next);
            MeshesAreSame(mesh, raw);
        }
    }

    [Test]
    public static void ReverseWindingOrder_IsSame()
    {
        var path = VimFormatRepoPaths.GetLatestWolfordResidenceVim();
        var scene = VimScene.LoadVim(path);
        for (var i = 0; i < scene.GetMeshCount(); i++)
        {

            var mesh = scene.Meshes[i].ReverseWindingOrder().ToIMesh();
            var next = scene.MeshesNext[i].ReverseWindingOrder();
            var raw = scene.MeshesRaw[i].ReverseWindingOrder();
            MeshesAreSame(mesh, next);
            MeshesAreSame(mesh, raw);
        }
    }

    [Test]
    public static void FaceMaterial_IsSame()
    {
        var path = VimFormatRepoPaths.GetLatestWolfordResidenceVim();
        var scene = VimScene.LoadVim(path);
        for (var i = 0; i < scene.GetMeshCount(); i++)
        {
            var mesh = scene.Meshes[i].GetFaceMaterials();
            var next = scene.MeshesNext[i].GetFaceMaterials();
            var raw = scene.MeshesRaw[i].GetFaceMaterials();
            Assert.That(mesh.SequenceEquals(next));
            Assert.That(next.SequenceEquals(raw));
        }
    }

    [Test]
    public static void Merge_ByPair_IsSame()
    {
        var path = VimFormatRepoPaths.GetLatestWolfordResidenceVim();
        var scene = VimScene.LoadVim(path);
        for (var i = 1; i < scene.GetMeshCount(); i++)
        {
            var mesh = scene.Meshes[i].Merge(scene.Meshes[i - 1]);
            var next = scene.MeshesNext[i].Merge2(scene.MeshesNext[i - 1]);
            var raw = scene.MeshesRaw[i].Merge2(scene.MeshesRaw[i - 1]);
            MeshesAreSame(mesh, next);
            MeshesAreSame(mesh, raw);
        }
    }

    [Test]
    public static void Merge_All_IsSame()
    {
        var path = VimFormatRepoPaths.GetLatestWolfordResidenceVim();
        var scene = VimScene.LoadVim(path);

        var mesh = scene.Meshes[0].Merge(scene.Meshes.Skip(1).ToArray());
        var next = scene.MeshesNext[0].Merge2(scene.MeshesNext.Skip(1).ToArray());
        var raw = scene.MeshesRaw[0].Merge2(scene.MeshesRaw.Skip(1).ToArray());
        MeshesAreSame(mesh, next);
        MeshesAreSame(mesh, raw);
    }

    [Test]
    public static void SplitByMaterials_IsSame()
    {
        var path = VimFormatRepoPaths.GetLatestWolfordResidenceVim();
        var scene = VimScene.LoadVim(path);
        for (var i = 0; i < scene.GetMeshCount(); i++)
        {
            var mesh = scene.Meshes[i].SplitByMaterial().ToArray();
            var next = scene.MeshesNext[i].SplitByMaterial();
            var raw = scene.MeshesRaw[i].SplitByMaterial();
            Assert.AreEqual(mesh.Length, next.Length);
            Assert.AreEqual(mesh.Length, raw.Length);

            for (var j = 0; j < mesh.Length; j++)
            {
                MeshesAreSame(mesh[j].Mesh, next[j].mesh);
                MeshesAreSame(mesh[j].Mesh, raw[j].mesh);
            }
        }
    }


    [Test]
    public static void SplitByMaterials_Benchmark()
    {
        var path = VimFormatRepoPaths.GetLatestWolfordResidenceVim();
        var scene = VimScene.LoadVim(path);
        var sw = new Stopwatch();

        long time = 0;
        long consumeTime =0;
        long consumeAgain = 0;
        for (var j=0; j < 100; j++)
        {
            for (var i = 0; i < scene.GetMeshCount(); i++)
            {
                sw.Restart();
                var mesh = scene.Meshes[i].SplitByMaterial().ToArray();
                sw.Stop();
                time += sw.ElapsedTicks;

                sw.Restart();
                foreach (var (mat, m) in mesh)
                {
                    Consume(m);
                }
                sw.Stop();
                consumeTime += sw.ElapsedTicks;

                sw.Restart();
                foreach (var (mat, m) in mesh)
                {
                    Consume(m);
                }
                sw.Stop();
                consumeAgain += sw.ElapsedTicks;
            }
        }

        Console.WriteLine("==Meshes==");
        Console.WriteLine("Split:" + (time * 1.0) / Stopwatch.Frequency);
        Console.WriteLine("Consume:" + (consumeTime * 1.0) / Stopwatch.Frequency);
        Console.WriteLine("Consume Again:" + (consumeAgain * 1.0) / Stopwatch.Frequency);
        Console.WriteLine("Total:" + (time + consumeTime + consumeAgain * 1.0) / Stopwatch.Frequency);

        time = 0;
        consumeTime = 0;
        consumeAgain = 0;
        for (var j = 0; j < 100; j++)
        {
            for (var i = 0; i < scene.GetMeshCount(); i++)
            {
                sw.Restart();
                var next = scene.MeshesNext[i].SplitByMaterial();
                sw.Stop();
                time += sw.ElapsedTicks;

                sw.Restart();
                foreach (var (mat, mesh) in next)
                {
                    Consume(mesh);
                }
                sw.Stop();
                consumeTime += sw.ElapsedTicks;

                sw.Restart();
                foreach (var (mat, m) in next)
                {
                    Consume(m);
                }
                sw.Stop();
                consumeAgain += sw.ElapsedTicks;
            }
        }
        Console.WriteLine("==Next==");
        Console.WriteLine("Split:" + (time * 1.0) / Stopwatch.Frequency);
        Console.WriteLine("Consume:" + (consumeTime * 1.0) / Stopwatch.Frequency);
        Console.WriteLine("Consume Again:" + (consumeAgain * 1.0) / Stopwatch.Frequency);
        Console.WriteLine("Total:" + (time + consumeTime + consumeAgain * 1.0) / Stopwatch.Frequency);

        time =0;
        consumeTime = 0;
        consumeAgain = 0;
        for (var j = 0; j < 100; j++)
        {
            for (var i = 0; i < scene.GetMeshCount(); i++)
            {
                sw.Restart();
                var raw = scene.MeshesRaw[i].SplitByMaterial();
                sw.Stop();
                time += sw.ElapsedTicks;

                sw.Restart();
                foreach (var (mat, mesh) in raw)
                {
                    Consume(mesh);
                }
                sw.Stop();
                consumeTime += sw.ElapsedTicks;

                sw.Restart();
                foreach (var (mat, m) in raw)
                {
                    Consume(m);
                }
                sw.Stop();
                consumeAgain += sw.ElapsedTicks;
            }
        }
        Console.WriteLine("==Raw==");
        Console.WriteLine("Split:" + (time *1.0) / Stopwatch.Frequency);
        Console.WriteLine("Consume:" + (consumeTime * 1.0) / Stopwatch.Frequency);
        Console.WriteLine("Consume Again:" + (consumeAgain * 1.0) / Stopwatch.Frequency);
        Console.WriteLine("Total:" + (time + consumeTime + consumeAgain * 1.0) / Stopwatch.Frequency);

    }

    private static void Consume(IMesh mesh)
    {
        mesh.Indices.Sum();
        mesh.Vertices.Sum(v => v.X);
        mesh.SubmeshIndexOffsets.Sum();
        mesh.SubmeshIndexCount.Sum();
        mesh.SubmeshMaterials.Sum();
    }

    private static void Consume(IMeshCommon mesh)
    {
        mesh.Indices.Sum();
        mesh.Vertices.Sum(v => v.X);
        mesh.SubmeshIndexOffsets.Sum();
        mesh.SubmeshIndexCounts.Sum();
        mesh.SubmeshMaterials.Sum();
    }


    private static void MeshesAreSame(IMesh mesh, IMeshCommon next)
    {
        Assert.That(mesh.Indices.SequenceEquals(next.Indices));
        Assert.That(mesh.Vertices.SequenceEquals(next.Vertices));
        Assert.That(mesh.SubmeshIndexOffsets.SequenceEquals(next.SubmeshIndexOffsets));
        Assert.That(mesh.SubmeshMaterials.SequenceEquals(next.SubmeshMaterials));
        Assert.That(mesh.SubmeshIndexCount.SequenceEquals(next.SubmeshIndexCounts));
    }
    private static void MeshesAreSame(IMeshCommon mesh, IMeshCommon next)
    {
        Assert.That(mesh.Indices.SequenceEquals(next.Indices));
        Assert.That(mesh.Vertices.SequenceEquals(next.Vertices));
        Assert.That(mesh.SubmeshIndexOffsets.SequenceEquals(next.SubmeshIndexOffsets));
        Assert.That(mesh.SubmeshMaterials.SequenceEquals(next.SubmeshMaterials));
        Assert.That(mesh.SubmeshIndexCounts.SequenceEquals(next.SubmeshIndexCounts));
    }




}

