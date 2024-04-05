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
            var mesh = scene.MeshesOld[i];
            var next = scene.MeshesNext[i];
            var raw = scene.Meshes[i];
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
            var mesh = scene.MeshesOld[i].Transform(mat);
            var next = scene.MeshesNext[i].Transform(mat);
            var raw = scene.Meshes[i].Transform(mat);
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

            var mesh = scene.MeshesOld[i].ReverseWindingOrder().ToIMesh();
            var next = scene.MeshesNext[i].ReverseWindingOrder();
            var raw = scene.Meshes[i].ReverseWindingOrder();
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
            var mesh = scene.MeshesOld[i].GetFaceMaterials();
            var next = scene.MeshesNext[i].GetFaceMaterials();
            var raw = scene.Meshes[i].GetFaceMaterials();
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
            var mesh = scene.MeshesOld[i].Merge(scene.MeshesOld[i - 1]);
            var next = scene.MeshesNext[i].Merge2(scene.MeshesNext[i - 1]);
            var raw = scene.Meshes[i].Merge2(scene.Meshes[i - 1]);
            MeshesAreSame(mesh, next);
            MeshesAreSame(mesh, raw);
        }
    }

    [Test]
    public static void QuadToTri_IsSame()
    {
        var indices = new int[] { 0, 1, 4, 3, 1, 2, 5, 4 };
        var vertices = new Vector3[]
        {
            new Vector3(1,0,0),
            new Vector3(1,1,0),
            new Vector3(1,2,0),
            new Vector3(0,0,0),
            new Vector3(0,1,0),
            new Vector3(0,2,0),
        };

        var mesh = Primitives.QuadMesh(vertices.ToIArray(), indices.ToIArray());
        var next = VimMesh.FromQuad(indices, vertices);

        MeshesAreSame(mesh, next);
    }

    [Test]
    public static void FromG3d_Equals_ToIMesh()
    {
        var path = VimFormatRepoPaths.GetLatestWolfordResidenceVim();
        var scene = VimScene.LoadVim(path);
        var mesh = scene.Document.Geometry.ToIMesh();
        var next = VimMesh.FromG3d(scene.Document.GeometryNext);
        MeshesAreSame(mesh, next);

    }

    [Test]
    public static void Merge_All_IsSame()
    {
        var path = VimFormatRepoPaths.GetLatestWolfordResidenceVim();
        var scene = VimScene.LoadVim(path);

        var mesh = scene.MeshesOld[0].Merge(scene.MeshesOld.Skip(1).ToArray());
        var next = scene.MeshesNext[0].Merge2(scene.MeshesNext.Skip(1).ToArray());
        var raw = scene.Meshes[0].Merge2(scene.Meshes.Skip(1).ToArray());
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
            var mesh = scene.MeshesOld[i].SplitByMaterial().ToArray();
            var next = scene.MeshesNext[i].SplitByMaterial();
            var raw = scene.Meshes[i].SplitByMaterial();
            Assert.AreEqual(mesh.Length, next.Length);
            Assert.AreEqual(mesh.Length, raw.Length);

            for (var j = 0; j < mesh.Length; j++)
            {
                MeshesAreSame(mesh[j].Mesh, next[j].mesh);
                MeshesAreSame(mesh[j].Mesh, raw[j].mesh);
            }
        }
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

