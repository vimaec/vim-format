using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Vim.Format.Logging;

namespace Vim.Format.Tests
{
    public class PublishedVersionTests
    {
        public static Regex VersionRegex
            => new (@"<Version>([0-9.]*)</Version>");

        public static Regex BFastVersionRegex
            => new(@"<BFastVersion>([0-9.]*)</BFastVersion>");

        public static Regex LinqArrayVersionRegex
            => new(@"<LinqArrayVersion>([0-9.]*)</LinqArrayVersion>");

        public static Regex Math3DVersionRegex
            => new(@"<Math3DVersion>([0-9.]*)</Math3DVersion>");

        [Test, Ignore("Ignoring until the new package is published")]
        public static void TestPublishedVersions()
        {
            var ctx = new CallerTestContext();
            ctx.PrepareDirectory();
            var logger = ctx.CreateLogger();

            // Parse BFast version

            var bfastProj = Path.Combine(RepoPaths.SrcDir, "cs", "bfast", "Vim.BFast", "Vim.BFast.csproj");
            var bfastProjContent = File.ReadAllText(bfastProj);
            var bfastVersionMatch = VersionRegex.Match(bfastProjContent);
            Assert.IsTrue(bfastVersionMatch.Success);
            var bfastVersion = bfastVersionMatch.Groups[1].ToString();
            logger.LogInformation($"{Path.GetFileName(bfastProj)} Version: {bfastVersion}");

            // Parse LinqArray version

            var linqArrayProj = Path.Combine(RepoPaths.SrcDir, "cs", "linqarray", "Vim.LinqArray", "Vim.LinqArray.csproj");
            var linqArrayProjContent = File.ReadAllText(linqArrayProj);
            var linqArrayVersionMatch = VersionRegex.Match(linqArrayProjContent);
            Assert.IsTrue(linqArrayVersionMatch.Success);
            var linqArrayVersion = linqArrayVersionMatch.Groups[1].ToString();
            logger.LogInformation($"{Path.GetFileName(linqArrayProj)} Version: {linqArrayVersion}");

            // Parse Math3D version

            var math3dProj = Path.Combine(RepoPaths.SrcDir, "cs", "math3d", "Vim.Math3D", "Vim.Math3D.csproj");
            var math3dProjContent = File.ReadAllText(math3dProj);
            var math3dVersionMatch = VersionRegex.Match(math3dProjContent);
            Assert.IsTrue(math3dVersionMatch.Success);
            var math3dVersion = math3dVersionMatch.Groups[1].ToString();
            logger.LogInformation($"{Path.GetFileName(math3dProj)} Version: {math3dVersion}");

            logger.LogInformation("---");

            // -- Check for matches in the g3d project

            var g3dProj = Path.Combine(RepoPaths.SrcDir, "cs", "g3d", "Vim.G3d", "Vim.G3d.csproj");
            var g3dProjContent = File.ReadAllText(g3dProj);
            logger.LogInformation($"{Path.GetFileName(g3dProj)}:");

            var g3dBfastVersionMatch = BFastVersionRegex.Match(g3dProjContent);
            Assert.IsTrue(g3dBfastVersionMatch.Success);
            var g3dBfastVersion = g3dBfastVersionMatch.Groups[1].ToString();
            logger.LogInformation($"    BFast Version: {g3dBfastVersion}");

            var g3dLinqArrayVersionMatch = LinqArrayVersionRegex.Match(g3dProjContent);
            Assert.IsTrue(g3dLinqArrayVersionMatch.Success);
            var g3dLinqArrayVersion = g3dLinqArrayVersionMatch.Groups[1].ToString();
            logger.LogInformation($"    LinqArray Version: {g3dLinqArrayVersion}");

            var g3dMath3dVersionMatch = Math3DVersionRegex.Match(g3dProjContent);
            Assert.IsTrue(g3dMath3dVersionMatch.Success);
            var g3dMath3dVersion = g3dMath3dVersionMatch.Groups[1].ToString();
            logger.LogInformation($"    Math3D Version: {g3dMath3dVersion}");

            Assert.AreEqual(bfastVersion, g3dBfastVersion, $"BFast version ({bfastVersion}) does not match G3d referenced BFast version ({g3dBfastVersion})");
            Assert.AreEqual(linqArrayVersion, g3dLinqArrayVersion, $"LinqArray version ({linqArrayVersion}) does not match G3d referenced LinqArray version ({g3dLinqArrayVersion})");
            Assert.AreEqual(math3dVersion, g3dMath3dVersion, $"Math3D version ({math3dVersion}) does not match G3d referenced Math3D version ({g3dMath3dVersion})");
        }
    }
}
