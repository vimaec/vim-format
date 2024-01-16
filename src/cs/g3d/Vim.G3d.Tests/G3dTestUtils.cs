using Assimp;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Vim.G3d.AssimpWrapper;
using Vim.LinqArray;

namespace Vim.G3d.Tests
{
    public static class G3dTestUtils
    {
        public static void OutputSceneStats(Scene scene)
            => Console.WriteLine(
$@"    #animations = {scene.AnimationCount}
    #cameras = {scene.CameraCount}
    #lights = {scene.LightCount}
    #materials = {scene.MaterialCount}
    #meshes = {scene.MeshCount}
    #nodes = {scene.GetNodes().Count()}
    #textures = {scene.TextureCount}");

        // TODO: merge all of the meshes using the transform. 

        public static void OutputMeshStats(Mesh mesh)
            => Console.WriteLine(
                $@"
    mesh  {mesh.Name}
    #faces = {mesh.FaceCount}
    #vertices = {mesh.VertexCount}
    #normals = {mesh.Normals?.Count ?? 0}
    #texture coordinate chanels = {mesh.TextureCoordinateChannelCount}
    #vertex color chanels = {mesh.VertexColorChannelCount}
    #bones = {mesh.BoneCount}
    #tangents = {mesh.Tangents?.Count}
    #bitangents = {mesh.BiTangents?.Count}");

        public static T TimeLoadingFile<T>(string fileName, Func<string, T> func)
        {
            var sw = new Stopwatch();
            sw.Start();
            try
            {
                return func(fileName);
            }
            finally
            {
                Console.WriteLine($"Time to open {Path.GetFileName(fileName)} is {sw.ElapsedMilliseconds}msec");
            }
        }

        public static void OutputStats(G3D g)
        {
            //Console.WriteLine("Header");

            Console.WriteLine($"# corners per faces {g.NumCornersPerFace} ");
            Console.WriteLine($"# vertices = {g.NumVertices}");
            Console.WriteLine($"# faces = {g.NumFaces}");
            Console.WriteLine($"# subgeos = {g.NumMeshes}");
            Console.WriteLine($"# indices (corners/edges0 = {g.NumCorners}");
            Console.WriteLine($"# instances = {g.NumInstances}");
            Console.WriteLine($"Number of attributes = {g.Attributes.Count}");

            foreach (var attr in g.Attributes.ToEnumerable())
                Console.WriteLine($"{attr.Name} #items={attr.ElementCount}");
        }
    }
}
