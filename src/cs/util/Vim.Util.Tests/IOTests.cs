using System.IO;
using NUnit.Framework;

namespace Vim.Util.Tests
{
    [TestFixture]
    // ReSharper disable once InconsistentNaming
    public static class IOTests
    {
        private static string PrepareDirectory(string testName)
        {
            var dir = Path.Combine(TestContext.DefaultTestDir, nameof(IOTests), testName);
            if (Directory.Exists(dir))
                Directory.Delete(dir, true);
            return Directory.CreateDirectory(dir).FullName;
        }

        private static void Touch(string filePath)
            => File.WriteAllText(filePath, "");

        [Test]
        public static void GetVersionedFilePath_NoFile_ReturnsSamePath()
        {
            var dir = PrepareDirectory(nameof(GetVersionedFilePath_NoFile_ReturnsSamePath));
            var path = Path.Combine(dir, "a.vim");
            Assert.AreEqual(path, IO.GetVersionedFilePath(path));
        }

        [Test]
        public static void GetVersionedFilePath_FileExists_ReturnsV2()
        {
            var dir = PrepareDirectory(nameof(GetVersionedFilePath_FileExists_ReturnsV2));
            var path = Path.Combine(dir, "a.vim");
            Touch(path);
            Assert.AreEqual(Path.Combine(dir, "a.v2.vim"), IO.GetVersionedFilePath(path));
        }

        [Test]
        public static void GetVersionedFilePath_FileAndV2Exist_ReturnsV3()
        {
            var dir = PrepareDirectory(nameof(GetVersionedFilePath_FileAndV2Exist_ReturnsV3));
            var path = Path.Combine(dir, "a.vim");
            Touch(path);
            Touch(Path.Combine(dir, "a.v2.vim"));
            Assert.AreEqual(Path.Combine(dir, "a.v3.vim"), IO.GetVersionedFilePath(path));
        }

        [Test]
        public static void GetVersionedFilePath_VersionedInputExists_ReplacesSuffix()
        {
            var dir = PrepareDirectory(nameof(GetVersionedFilePath_VersionedInputExists_ReplacesSuffix));
            var path = Path.Combine(dir, "a.v2.vim");
            Touch(path);
            Assert.AreEqual(Path.Combine(dir, "a.v3.vim"), IO.GetVersionedFilePath(path));
        }

        [Test]
        public static void GetVersionedFilePath_DirectoryExists_CountsAsTaken()
        {
            var dir = PrepareDirectory(nameof(GetVersionedFilePath_DirectoryExists_CountsAsTaken));
            var path = Path.Combine(dir, "a.vim");
            Directory.CreateDirectory(path);
            Assert.AreEqual(Path.Combine(dir, "a.v2.vim"), IO.GetVersionedFilePath(path));
        }

        [Test]
        public static void GetVersionedFilePath_DottedVersionTag_IsNotAVersionSuffix()
        {
            var dir = PrepareDirectory(nameof(GetVersionedFilePath_DottedVersionTag_IsNotAVersionSuffix));
            var path = Path.Combine(dir, "a.v1.5.9.3.vim");
            Touch(path);
            Assert.AreEqual(Path.Combine(dir, "a.v1.5.9.3.v2.vim"), IO.GetVersionedFilePath(path));
        }

        [Test]
        public static void GetVersionedFilePath_LockedFile_ReturnsNextVersion()
        {
            var dir = PrepareDirectory(nameof(GetVersionedFilePath_LockedFile_ReturnsNextVersion));
            var path = Path.Combine(dir, "a.vim");
            Touch(path);

            // Another application (for example, VIM Flex) has the file open.
            using (new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var versionedPath = IO.GetVersionedFilePath(path);
                Assert.AreEqual(Path.Combine(dir, "a.v2.vim"), versionedPath);
                Touch(versionedPath);
            }
        }

        [Test]
        public static void IsVersionOfBaseFilePath()
        {
            var dir = PrepareDirectory(nameof(IsVersionOfBaseFilePath));
            var basePath = Path.Combine(dir, "name.vim");

            Assert.IsTrue(IO.IsVersionOfBaseFilePath(basePath, basePath), "Same path");
            Assert.IsTrue(IO.IsVersionOfBaseFilePath(basePath, Path.Combine(dir, "NAME.v12.VIM")), "Versioned path, other case");
            Assert.IsTrue(IO.IsVersionOfBaseFilePath(Path.Combine(dir, "name.v2.vim"), basePath), "Versioned base path");
            Assert.IsTrue(IO.IsVersionOfBaseFilePath(basePath, IO.GetVersionedFilePath(basePath)), "Path from GetVersionedFilePath");

            Assert.IsFalse(IO.IsVersionOfBaseFilePath(basePath, Path.Combine(dir, "name.dry_run.vim")), "Other suffix");
            Assert.IsFalse(IO.IsVersionOfBaseFilePath(basePath, Path.Combine(dir, "name.v2.v3.vim")), "Two version suffixes");
            Assert.IsFalse(IO.IsVersionOfBaseFilePath(basePath, Path.Combine(dir, "name2.vim")), "Other name");
            Assert.IsFalse(IO.IsVersionOfBaseFilePath(basePath, Path.Combine(dir, "name.v2.txt")), "Other extension");
            Assert.IsFalse(IO.IsVersionOfBaseFilePath(basePath, Path.Combine(dir, "sub", "name.v2.vim")), "Other directory");
            Assert.IsFalse(IO.IsVersionOfBaseFilePath(basePath, null), "Null candidate");
            Assert.IsFalse(IO.IsVersionOfBaseFilePath(null, basePath), "Null base");
            Assert.IsFalse(IO.IsVersionOfBaseFilePath(basePath, ""), "Empty candidate");
        }
    }
}
