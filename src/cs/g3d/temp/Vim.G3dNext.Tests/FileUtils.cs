using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Vim.BFast;
using Vim.LinqArray;
using Vim.G3d;
using System.IO.Compression;
using Vim.Math3d;
using System;

namespace Vim.G3dNext
{
    public static class FileUtils
    {
        public static void ResetFolder(string path)
        {

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            Directory.CreateDirectory(path);
        }

        public static VimG3d LoadG3d(string path)
        {
            var time = Stopwatch.StartNew();
            var file = new FileStream(path, FileMode.Open);
            var geometry = file.GetBFastBufferReader("geometry");
            geometry.Seek();
            var g3d = VimG3d.FromStream(file);
            Console.WriteLine("LoadG3d " + time.ElapsedMilliseconds / 1000f);
            return g3d;
        }

        public static void SplitVim(string path, string name)
        {
            var time = Stopwatch.StartNew();
            var vim = VimScene.LoadVim(path, new DataFormat.LoadOptions() {
                SkipAssets = true,
                SkipGeometry = true,
                SkipHeader = true}
            );
            Console.WriteLine("LoadVim " + time.ElapsedMilliseconds / 1000f);

            time.Restart();
            var g3d = LoadG3d(path);
            Console.WriteLine("VimG3d " + time.ElapsedMilliseconds / 1000f);

            time.Restart();
            var meshes = g3d.GetMeshes().ToArray();
            PolyStopwatch.instance.Print();
            Console.WriteLine("GetMeshes " + time.ElapsedMilliseconds / 1000f);

            time.Restart();
            var nodeElements = vim.DocumentModel.NodeElementIndex.ToArray();
            var categoryName = vim.DocumentModel.CategoryName;
            var elementCategory = vim.DocumentModel.ElementCategoryIndex;

            // Remove select => long once using latest data model.
            var nodeElementIds = nodeElements.Select(n => n < 0 || n > vim.DocumentModel.ElementId.Count
                ? (long)-1
                : (long)vim.DocumentModel.ElementId[n]
            ).ToArray();
            Console.WriteLine("nodes " + time.ElapsedMilliseconds / 1000f);

            time.Restart();
            var getMeshName = (MeshG3d mesh) =>
            {
                var node = mesh.instanceNodes[0];

                if (node < 0 || node >= nodeElements.Length) return "";
                var element = nodeElements [node];
                
                if (element < 0 || element >= elementCategory.Count) return "";
                var category = elementCategory[element];

                if (category < 0 || category >= categoryName.Count) return "";
                var name = categoryName[category];

                return name;
            };

            meshes = meshes.OrderByDescending((m) => (
                GetPriority(getMeshName(m)),
                m.GetAABB().MaxSide)
            ).ToArray();
            Console.WriteLine("OrderByDescending " + time.ElapsedMilliseconds / 1000f);

            time.Restart();
            var meshBuffers = meshes.Select((m, i) =>
            {
                return m.g3d.ToBytes()
                    .GzipBytes()
                    .ToNamedBuffer($"mesh_{i}");
            });
            Console.WriteLine("meshBuffers " + time.ElapsedMilliseconds / 1000f);

            time.Restart();
            var index = meshes.ToMeshIndex(g3d, nodeElements, nodeElementIds);
            var indexBuffer = index.g3d.ToBytes()
                .GzipBytes()
                .ToNamedBuffer("index");
            Console.WriteLine("indexBuffer " + time.ElapsedMilliseconds / 1000f);

            time.Restart();
            var mat = g3d.getMaterials();
            var matBuffer = mat.g3d.ToBytes()
                .GzipBytes()
                .ToNamedBuffer("materials");
            Console.WriteLine("matBuffer " + time.ElapsedMilliseconds / 1000f);

            time.Restart();
            var bfastBuilder = new BFastBuilder();
            bfastBuilder.Add(indexBuffer);
            bfastBuilder.Add(matBuffer);

            foreach (var mesh in meshBuffers)
            {
                bfastBuilder.Add(mesh);
            }

            bfastBuilder.Write($"./{name}.vimx");
            Console.WriteLine("Write " + time.ElapsedMilliseconds / 1000f);

        }


        static int GetPriority(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return 0;

            if (value.Contains("Topography")) return 110;
            if (value.Contains("Floor")) return 100;
            if (value.Contains("Slab")) return 100;
            if (value.Contains("Ceiling")) return 90;
            if (value.Contains("Roof")) return 90;

            if (value.Contains("Curtain")) return 80;
            if (value.Contains("Wall")) return 80;
            if (value.Contains("Window")) return 70;

            if (value.Contains("Column")) return 60;
            if (value.Contains("Structural")) return 60;

            if (value.Contains("Stair")) return 40;
            if (value.Contains("Doors")) return 30;

            return 1;
        }

        static AABox[] ComputeBoxes(SceneG3d index, MeshG3d[] meshes)
        {
            var boxes = new List<AABox>();
            for (var i = 0; i < index.instanceFiles.Length; i++)
            {
                var m = index.instanceFiles[i];
                var instance = index.instanceIndices[i];
                var mesh = meshes[m];
                boxes.Add(mesh.GetInstanceAABB(instance));
            }
            return boxes.ToArray();
        }

        public static byte[] GzipBytes(this byte[] inputBytes)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var gzipStream = new DeflateStream(memoryStream, CompressionMode.Compress, true))
                {
                    gzipStream.Write(inputBytes, 0, inputBytes.Length);
                }
                return memoryStream.ToArray();
            }
        }
    }

}
