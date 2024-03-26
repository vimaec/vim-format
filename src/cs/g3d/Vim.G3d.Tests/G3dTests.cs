using Assimp;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.BFastLib;
using Vim.G3d.AssimpWrapper;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim.G3d.Tests
{
    [TestFixture]
    public static class G3dTests
    {
        public class FileLoadData
        {
            public FileLoadData(string filePath)
                => SourceFile = new FileInfo(filePath);

            public string ShortName => Path.GetFileName(SourceFile.FullName);
            public int NumMeshes => Scene?.MeshCount ?? 0;
            public FileInfo SourceFile;
            public FileInfo G3DFile;
            public long MSecToOpen;
            public long MSecToSaveG3d;
            public long MSecToOpenG3d;
            public long MSecToConvert;
            public long MemoryConsumption;
            public long MemoryConsumptionG3d;
            public Exception Error;
            public Scene Scene;
            public G3D G3d;
        }

        public static readonly string ProjectFolder = new DirectoryInfo(Properties.Resources.ProjDir.Trim()).FullName;
        public static string RootFolder = Path.Combine(ProjectFolder, "..", "..", "..", "..");
        public static string TestInputFolder = Path.Combine(RootFolder, "data", "g3d-test-data", "models");
        public static string TestOutputFolder = Path.Combine(RootFolder, "data", "g3d-test-data", "output");
        
        [SetUp]
        public static void Setup()
        {
            if (!Directory.Exists(RootFolder))
            {
                Directory.CreateDirectory(RootFolder);
            }
            if (!Directory.Exists(TestInputFolder))
            {
                Directory.CreateDirectory(TestInputFolder);
            }
            if (!Directory.Exists(TestOutputFolder))
            {
                Directory.CreateDirectory(TestOutputFolder);
            }
        }

        public static IEnumerable<string> GetInputFiles()
            => Directory.GetFiles(TestInputFolder, "*.*", SearchOption.AllDirectories);

        public static void ValidateSame(Object a, Object b, string name = "")
        {
            if (!a.Equals(b))
                throw new Exception($"Values {a} and {b} are different {name}");
        }

        public static void ValidateSameG3D(G3D g1, G3D g2)
        {
            ValidateSame(g1.NumCornersPerFace, g2.NumCornersPerFace, "NumCornersPerFace");
            ValidateSame(g1.NumFaces, g2.NumFaces, "NumFaces");
            ValidateSame(g1.NumCorners, g2.NumCorners, "NumCorners");
            ValidateSame(g1.NumVertices, g2.NumVertices, "NumVertices");
            ValidateSame(g1.NumInstances, g2.NumInstances, "NumInstances");
            ValidateSame(g1.NumMeshes, g2.NumMeshes, "NumMeshes");
            ValidateSame(g1.Attributes.Count, g2.Attributes.Count, "NumAttributes");
            for (var i = 0; i < g1.Attributes.Count; ++i)
            {
                var attr1 = g1.Attributes[i];
                var attr2 = g2.Attributes[i];
                ValidateSame(attr1.Name, attr2.Name, $"Attribute[{i}].Name");
                ValidateSame(attr1.GetByteSize(), attr2.GetByteSize(), $"Attribute[{i}].ByteSize");
                ValidateSame(attr1.ElementCount, attr2.ElementCount, $"Attribute[{i}].ElementCount");
            }
        }

        [Test]
        [Platform(Exclude = "Linux,Unix", Reason = "AssimpNet is failing to load its dependency on 'libdl.so'.")]
        public static void OpenAndConvertAssimpFiles()
        {
            var files = GetInputFiles()
                .Where(AssimpLoader.CanLoad)
                .Select(f => new FileLoadData(f))
                .ToArray();

            // Load all the files 
            foreach (var f in files)
            {
                try
                {
                    (f.MemoryConsumption, f.MSecToOpen) =
                        Util.GetMemoryConsumptionAndMSecElapsed(() =>
                            f.Scene = AssimpLoader.Load(f.SourceFile.FullName));
                }
                catch (Exception e)
                {
                    f.Error = e;
                }
            }

            // Convert all the Assimp scenes to G3D
            foreach (var f in files)
            {
                if (f.Scene == null) continue;

                try
                {
                    f.MSecToConvert = Util.GetMSecElapsed(() =>
                        f.G3d = f.Scene.ToG3d());
                }
                catch (Exception e)
                {
                    f.Error = e;
                }
            }

            // Save all the G3D scenes
            Util.CreateAndClearDirectory(TestOutputFolder);
            foreach (var f in files)
            {
                if (f.G3d == null) continue;

                try
                {
                    var outputFilePath = Path.Combine(TestOutputFolder, f.ShortName + ".g3d");
                    f.G3DFile = new FileInfo(outputFilePath);

                    f.MSecToSaveG3d = Util.GetMSecElapsed(() =>
                        f.G3d.ToBFast().Write(outputFilePath));
                }
                catch (Exception e)
                {
                    f.Error = e;
                }
            }

            // Try reading back in all of the G3D scenes, measure load times and the memory consumption
            foreach (var f in files)
            {
                if (f.G3DFile == null) continue;

                try
                {
                    G3D localG3d = null;

                    (f.MemoryConsumptionG3d, f.MSecToOpenG3d) =
                       Util.GetMemoryConsumptionAndMSecElapsed(() =>
                            localG3d = G3D.Read(f.G3DFile.FullName));

                    ValidateSameG3D(f.G3d, localG3d);
                }
                catch (Exception e)
                {
                    f.Error = e;
                }
            }

            // Output the header for data
            Console.WriteLine(
                "Importer," +
                "Extension," +
                "File Name," +
                "File Size(KB)," +
                "Load Time(s)," +
                "Memory(KB)," +
                "# Meshes," +
                "Time to Convert," +
                "Time to Write G3D," +
                "G3D File Size(KB)," +
                "G3D Memory(KB)",
                "G3D Load Time(s)",
                "Error");

            // Output the data rows
            foreach (var f in files)
            {
                Console.WriteLine(
                    "Assimp," +
                    $"{Path.GetExtension(f.ShortName)}," +
                    $"{f.ShortName}," +
                    $"{f.SourceFile?.Length / 1000}," +
                    $"{f.MSecToOpen / 100f}," +
                    $"{f.MemoryConsumption / 1000}," +
                    $"{f.NumMeshes}," +
                    $"{f.MSecToConvert / 100f}," +
                    $"{f.MSecToSaveG3d / 100f}," +
                    $"{f.G3DFile?.Length / 1000}," +
                    $"{f.MemoryConsumptionG3d / 1000}," +
                    $"{f.MSecToOpenG3d / 100f}," +
                    $"{f.Error}");
            }

            Assert.AreEqual(0, files.Count(f => f.Error != null), "Errors occurred");
        }

        [Test]
        public static void TriangleTest()
        {
            // Serialize a triangle g3d as bytes and read it back.
            var vertices = new[]
            {
                new Vector3(0, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 1, 1)
            };

            var indices = new[] { 0, 1, 2 };
            var materialIndices = new[] { 5 };

            var g3d = new G3DBuilder()
                .AddVertices(vertices.ToIArray())
                .AddIndices(indices.ToIArray())
                .Add(materialIndices.ToIArray().ToFaceMaterialAttribute())
                .ToG3D();

            var bfast = g3d.ToBFast();
            var g = G3D.Read(bfast);

            Assert.IsNotNull(g);

            Assert.AreEqual(3, g.NumVertices);
            Assert.AreEqual(3, g.NumCorners);
            Assert.AreEqual(1, g.NumFaces);
            Assert.AreEqual(3, g.NumCornersPerFace);
            Assert.AreEqual(0, g.NumMeshes);
            Assert.AreEqual(0, g.NumInstances);

            Assert.AreEqual(vertices, g.Vertices.ToArray());
            Assert.AreEqual(indices, g.Indices.ToArray());
            Assert.AreEqual(materialIndices, g.FaceMaterials.ToArray());
        }

        [Test]
        public static void UnexpectedAttributes_Are_Ignored()
        {
            var bfast = new BFast();
            bfast.SetArray("g3d:instance:potato:0:int32:1", new int[] { 5 });

            var g = G3D.Read(bfast);
            var parsedInstanceAttrs = g.Attributes.Where(ga => ga.Descriptor.Association == Association.assoc_instance).ToArray();
            var parsedPotatoAttr = parsedInstanceAttrs.Single(ga => ga.Descriptor.Semantic == "potato");
            Assert.AreEqual(new [] { 5 }, parsedPotatoAttr.AsType<int>().Data.ToArray());
        }

        public static G3D LoadAssimpFile(string filePath)
        {
            using (var context = new AssimpContext())
            {
                var scene = context.ImportFile(filePath);
                Assert.AreEqual(1, scene.Meshes.Count);
                return scene.Meshes[0].ToG3D();
            }
        }

        // NOTE: can't be run as part of NUnit because it requires the GC
        public static void BigFileTest()
        {
            var nVerts = (300 * 1000 * 1000); // 300 * 12 = 3.6 GB
            var vertices = nVerts.Select(i => new Vector3(i, i, i));
            var bldr = new G3DBuilder();
            bldr.AddVertices(vertices);

            var expectedG3d = bldr.ToG3D();
            Assert.AreEqual(nVerts, expectedG3d.NumVertices);
            var bfast = expectedG3d.ToBFast();
            var resultG3d = G3D.Read(bfast);

            ValidateSameG3D(expectedG3d, resultG3d);
        }
    }
}
