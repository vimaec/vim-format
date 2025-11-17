using NUnit.Framework;
using System.IO;
using System.Linq;
using Vim.Format.Merge;
using Vim.Format.ObjectModel;
using Vim.Math3d;
using Vim.Format.api_v2;
using Vim.Util.Tests;

namespace Vim.Format.Tests.api_v2;

[TestFixture]
public static class VimRoomServiceTests
{
    /// <summary>
    /// Merges two unrelated VIM files and then calculates the inclusion of elements
    /// coming from the first VIM file (containing no rooms) in the rooms of the second VIM file.
    /// </summary>
    [Test]
    public static void TestRoomService_ComputeElementsInRoom()
    {
        var ctx = new CallerTestContext();
        var dir = ctx.PrepareDirectory();

        // Merge the VIM files.
        var vim1 = Path.Combine(VimFormatRepoPaths.DataDir, "Wolford_Residence.r2023.om_v5.0.0.vim");
        var vim2 = Path.Combine(VimFormatRepoPaths.DataDir, "RoomTest.vim");
        var mergedFilePath = Path.Combine(dir, "merged.vim");

        var mergeFiles = new MergeConfigFiles(new[]
        {
            (vim1, Matrix4x4.Identity),
            (vim2, Matrix4x4.Identity)
        }, mergedFilePath);

        var mergeOptions = new MergeConfigOptions()
        {
            GeneratorString = nameof(TestRoomService_ComputeElementsInRoom),
            VersionString = "0.0.0",
        };

        MergeService.MergeVimFiles(mergeFiles, mergeOptions);

        var mergedVim = VimScene.LoadVim(mergedFilePath);
        Assert.DoesNotThrow(() => mergedVim.Validate());

        var dm = mergedVim.DocumentModel;

        var elementInfos =
            Enumerable.Range(0, mergedVim.DocumentModel.NumElement)
                .AsParallel()
                .Select(elementIndex => dm.GetElementInfo(elementIndex))
                .ToArray();

        var wolfordFamilyInstances = elementInfos
            .Where(ei => ei.BimDocumentFileName.Contains("Wolford") && ei.IsFamilyInstance)
            .ToArray();
        var wolfordFamilyInstanceElementIndices = wolfordFamilyInstances.Select(ei => ei.ElementIndex).ToHashSet();

        // Compute the element association in each room.
        var familyInstancesInRooms = VimRoomService.ComputeElementsInRoom(
            mergedVim,
            ei => wolfordFamilyInstanceElementIndices.Contains(ei.ElementIndex));

        Assert.IsNotEmpty(familyInstancesInRooms);
    }
}
