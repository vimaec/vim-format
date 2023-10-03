using System.Diagnostics;
using System.Runtime.CompilerServices;
using Vim.G3dNext.Attributes;
using Vim.Math3d;
using Vim.Util.Tests;

namespace Vim.G3dNext.Tests
{
    public static class TestUtils
    {

        public static string ResidencePath = VimFormatRepoPaths.GetLatestWolfordResidenceVim();

        /// <summary>
        /// Deletes and/or creates a folder for given test case name.
        /// </summary>
        public static string PrepareTestDir([CallerMemberName] string testName = null)
        {
            if (testName == null)
                throw new ArgumentException(nameof(testName));

            var testDir = Path.Combine(VimFormatRepoPaths.OutDir, testName);

            // Prepare the test directory
            if (Directory.Exists(testDir))
                Directory.Delete(testDir, true);
            Directory.CreateDirectory(testDir);

            return testDir;
        }

        public static (long, long) GetMemoryConsumptionAndMSecElapsed(Action action)
        {
            var time = 0L;
            var mem = GetMemoryConsumption(
                () => time = GetMSecElapsed(action));
            return (mem, time);
        }

        public static long GetMSecElapsed(Action action)
        {
            var sw = Stopwatch.StartNew();
            action();
            return sw.ElapsedMilliseconds;
        }

        /// <summary>
        /// Creates a directory if needed, or clears all of its contents otherwise
        /// </summary>
        public static string CreateAndClearDirectory(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            else
                DeleteFolderContents(dirPath);
            return dirPath;
        }

        /// <summary>
        /// Deletes all contents in a folder
        /// </summary>
        /// <remarks>
        /// https://stackoverflow.com/questions/1288718/how-to-delete-all-files-and-folders-in-a-directory
        /// </remarks>
        private static void DeleteFolderContents(string folderPath)
        {
            var di = new DirectoryInfo(folderPath);
            foreach (var dir in di.EnumerateDirectories().AsParallel())
                DeleteFolderAndAllContents(dir.FullName);
            foreach (var file in di.EnumerateFiles().AsParallel())
                file.Delete();
        }

        /// <summary>
        /// Deletes everything in a folder and then the folder.
        /// </summary>
        private static void DeleteFolderAndAllContents(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                return;

            DeleteFolderContents(folderPath);
            Directory.Delete(folderPath);
        }

        // NOTE: Calling a function generates additional memory
        private static long GetMemoryConsumption(Action action)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            var memBefore = GC.GetTotalMemory(true);
            action();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            return GC.GetTotalMemory(true) - memBefore;
        }

        public static VimAttributeCollection CreateTestG3d()
        {
            var attributes = new VimAttributeCollection();

            attributes.Positions.TypedData = new Vector3[] { Vector3.Zero, Vector3.UnitX, Vector3.UnitY, Vector3.UnitZ };
            attributes.Indices.TypedData = new int[] { 0, 1, 2, 0, 3, 2, 1, 3, 2 };
            attributes.SubmeshIndexOffsets.TypedData = new int[] { 0, 3, 6 };
            attributes.SubmeshMaterials.TypedData = new int[] { 0 };
            attributes.MeshSubmeshOffsets.TypedData = new int[] { 0 };
            attributes.InstanceTransforms.TypedData = new Matrix4x4[] { Matrix4x4.Identity };
            attributes.InstanceMeshes.TypedData = new int[] { 0 };
            attributes.InstanceParents.TypedData = new int[] { -1 };
            attributes.MaterialColors.TypedData = new Vector4[] { new Vector4(0.25f, 0.5f, 0.75f, 1) };
            attributes.MaterialGlossiness.TypedData = new float[] { .95f };
            attributes.MaterialSmoothness.TypedData = new float[] { .5f };

            attributes.Validate();

            return attributes;
        }
    }
}
