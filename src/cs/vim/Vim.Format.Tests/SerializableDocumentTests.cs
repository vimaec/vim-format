using NUnit.Framework;
using Vim.Util.Tests;
using Vim.Format.SceneBuilder;
using Vim.G3dNext;
using Vim.Format.Geometry;
using Vim.LinqArray;
using Vim.Math3d;

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
        for(var i=0; i < scene.GetMaterialCount(); i++)
        {
            Assert.AreEqual(scene.GetMaterialColor(i),scene.GetMaterialColorNext(i));
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
            MeshesAreSame(mesh, next);
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
            MeshesAreSame(mesh, next);
        }
    }

    private static void MeshesAreSame(IMesh mesh, IMeshCommon next)
    {

        Assert.That(mesh.Indices.SequenceEquals(next.Indices));
        Assert.That(mesh.Vertices.SequenceEquals(next.Vertices));
        Assert.That(mesh.SubmeshIndexOffsets.SequenceEquals(next.SubmeshIndexOffsets));
        Assert.That(mesh.SubmeshMaterials.SequenceEquals(next.SubmeshMaterials));
    }
}

