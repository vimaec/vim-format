using NUnit.Framework;
using System.IO;
using Vim.Math3d;
using Vim.Util.Tests;
using System.Collections.Generic;

namespace Vim.Format.Tests;

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

        var mergeFiles = new VimMergeConfigFiles(new[]
        {
            (vim1, Matrix4x4.Identity),
            (vim2, Matrix4x4.Identity)
        }, mergedFilePath);

        var mergeOptions = new VimMergeConfigOptions()
        {
            GeneratorString = nameof(TestRoomService_ComputeElementsInRoom),
            VersionString = "0.0.0",
        };

        VimMergeService.Merge(mergeFiles, mergeOptions);

        var mergedVim = VIM.Open(mergedFilePath);
        Assert.DoesNotThrow(() => mergedVim.Validate());

        var mergedVimTableSet = mergedVim.GetEntityTableSet();
        var elementKinds = mergedVimTableSet.GetElementKinds();

        var wolfordFamilyInstanceElementIndices = new HashSet<int>();
        for (var i = 0; i < elementKinds.Length; ++i)
        {
            if (elementKinds[i] == ElementKind.FamilyInstance)
                wolfordFamilyInstanceElementIndices.Add(i);
        }

        // Compute the element association in each room.
        var familyInstancesInRooms = VimRoomService.ComputeElementsInRoom(
            mergedVim,
            ei => wolfordFamilyInstanceElementIndices.Contains(ei.ElementIndex));

        Assert.IsNotEmpty(familyInstancesInRooms);
    }
}
