using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Vim.LinqArray;
using Vim.Util;
using Vim.Util.Tests;
using Vim.Format.api_v2;

namespace Vim.Format.Tests.api_v2;

[TestFixture]
public static class VimElementHierarchyTests
{
    [Test]
    public static void TestFlattenVimElementHierarchy()
    {
        var ctx = new CallerTestContext();
        var _ = ctx.PrepareDirectory();
        var logger = ctx.CreateLogger();
        // Visits the tree and flattens it to a list of records representing the hierarchy.
        //
        // Sample tree:
        //
        //   root -> A -> B -> C -> D
        //            \-> E
        //
        // Resulting data records (Element | Descendant | Distance | RootDistance):
        //   
        //   A | A | 0 | 0
        //   A | B | 1 | 1
        //   A | E | 1 | 1
        //   E | E | 0 | 1
        //   A | C | 2 | 2
        //   A | D | 3 | 3
        //   B | B | 0 | 1
        //   B | C | 1 | 2
        //   B | D | 2 | 3
        //   C | C | 0 | 2
        //   C | D | 1 | 3
        //   D | D | 0 | 3
        //
        // Observations:
        // - RootDistance is always the distance of the terminal object from the root.

        var dummyA = new VimElementGeometryInfo(0);
        var dummyB = new VimElementGeometryInfo(1);
        var dummyC = new VimElementGeometryInfo(2);
        var dummyD = new VimElementGeometryInfo(3);
        dummyD.NodeAndGeometryIndices.Add((33, 33));
        var dummyE = new VimElementGeometryInfo(4);
        dummyE.NodeAndGeometryIndices.Add((44, 44));

        var dummyMap = new VimElementGeometryMap(new[]
        {
            dummyA,
            dummyB,
            dummyC,
            dummyD,
            dummyE
        });

        var A = new Tree<int> { Value = dummyA.ElementIndex };
        var B = new Tree<int> { Value = dummyB.ElementIndex };
        var C = new Tree<int> { Value = dummyC.ElementIndex };
        var D = new Tree<int> { Value = dummyD.ElementIndex };
        var E = new Tree<int> { Value = dummyE.ElementIndex };

        A.AddChild(B);
        A.AddChild(E);
        B.AddChild(C);
        C.AddChild(D);

        var treeRoot = new Tree<int> { Value = -1 }; // Add a dummy root.
        treeRoot.AddChild(A);

        var flattened = VimElementHierarchyService.FlattenElementHierarchy(
            treeRoot,
            false,
            dummyMap)
            .OrderBy(r => (Element: r.Element, Descendant: r.Descendant, r.Distance, r.RootDistance))
            .ToList();

        var expected = new List<VimElementHierarchy>()
        {
            // A | A | 0 | 0
            new ()
            {
                Element = A.Value,
                Descendant = A.Value,
                Distance = 0,
                RootDistance = 0,
                DescendantNodeIndex = null,
                DescendantGeometryIndex = null,
            },
            // A | B | 1 | 1
            new ()
            {
                Element = A.Value,
                Descendant = B.Value,
                Distance = 1,
                RootDistance = 1,
                DescendantNodeIndex = null,
                DescendantGeometryIndex = null,
            },
            // A | E | 1 | 1
            new ()
            {
                Element = A.Value,
                Descendant = E.Value,
                Distance = 1,
                RootDistance = 1,
                DescendantNodeIndex = 44,
                DescendantGeometryIndex = 44,
            },
            // E | E | 0 | 1
            new ()
            {
                Element = E.Value,
                Descendant = E.Value,
                Distance = 0,
                RootDistance = 1,
                DescendantNodeIndex = 44,
                DescendantGeometryIndex = 44,
            },
            //   A | C | 2 | 2
            new ()
            {
                Element = A.Value,
                Descendant = C.Value,
                Distance = 2,
                RootDistance = 2,
                DescendantNodeIndex = null,
                DescendantGeometryIndex = null,
            },
            //   A | D | 3 | 3
            new ()
            {
                Element = A.Value,
                Descendant = D.Value,
                Distance = 3,
                RootDistance = 3,
                DescendantNodeIndex = 33,
                DescendantGeometryIndex = 33,
            },
            //   B | B | 0 | 1
            new ()
            {
                Element = B.Value,
                Descendant = B.Value,
                Distance = 0,
                RootDistance = 1,
                DescendantNodeIndex = null,
                DescendantGeometryIndex = null,
            },
            //   B | C | 1 | 2
            new ()
            {
                Element = B.Value,
                Descendant = C.Value,
                Distance = 1,
                RootDistance = 2,
                DescendantNodeIndex = null,
                DescendantGeometryIndex = null,
            },
            //   B | D | 2 | 3
            new ()
            {
                Element = B.Value,
                Descendant = D.Value,
                Distance = 2,
                RootDistance = 3,
                DescendantNodeIndex = 33,
                DescendantGeometryIndex = 33,
            },
            //   C | C | 0 | 2
            new ()
            {
                Element = C.Value,
                Descendant = C.Value,
                Distance = 0,
                RootDistance = 2,
                DescendantNodeIndex = null,
                DescendantGeometryIndex = null,
            },
            //   C | D | 1 | 3
            new ()
            {
                Element = C.Value,
                Descendant = D.Value,
                Distance = 1,
                RootDistance = 3,
                DescendantNodeIndex = 33,
                DescendantGeometryIndex = 33,
            },
            //   D | D | 0 | 3
            new ()
            {
                Element = D.Value,
                Descendant = D.Value,
                Distance = 0,
                RootDistance = 3,
                DescendantNodeIndex = 33,
                DescendantGeometryIndex = 33,
            },
        }
        .OrderBy(r => (Element: r.Element, Descendant: r.Descendant, r.Distance, r.RootDistance))
        .ToList();

        void logRecords(IReadOnlyCollection<VimElementHierarchy> records, string title)
        {
            logger.Log($"~~~ {title} ({records.Count}) ~~~");
            foreach (var item in records)
            {
                logger.Log($"Element: {item.Element}, Descendant: {item.Descendant}, Distance: {item.Distance}, RootDistance: {item.RootDistance}, NodeIndex: {item.DescendantNodeIndex}, GeometryIndex: {item.DescendantGeometryIndex}");
            }

            logger.Log("~~~");
        }
        logRecords(flattened, "Flattened");
        logRecords(expected, "Expected");

        Assert.AreEqual(expected.Count, flattened.Count);

        foreach (var e in expected)
        {
            var found = flattened.FirstOrDefault(r => r.Element == e.Element && r.Descendant == e.Descendant);
            Assert.IsNotNull(found);
            Assert.AreEqual(e.Element, found.Element);
            Assert.AreEqual(e.Descendant, found.Descendant);
            Assert.AreEqual(e.Distance, found.Distance);
            Assert.AreEqual(e.RootDistance, found.RootDistance);
            Assert.AreEqual(e.DescendantNodeIndex, found.DescendantNodeIndex);
            Assert.AreEqual(e.DescendantGeometryIndex, found.DescendantGeometryIndex);
        }
    }

    [TestCase(true)]
    [TestCase(false)]
    public static void TestVimElementHierarchy(bool isElementAndDescendantPrimaryKey)
    {
        var ctx = new CallerTestContext();
        ctx.PrepareDirectory();

        var vimFilePath = VimFormatRepoPaths.GetDataFilePath("Wolford_Residence.r2025.om_v5.6.0.vim", false);

        var vim = VIM.Open(vimFilePath, new VimOpenOptions { IncludeAssets = false });
        
        var service = vim.GetElementHierarchyService(isElementAndDescendantPrimaryKey);
        var root = service.GetElementIndexHierarchy();
        Assert.IsNotEmpty(root.Children);

        var flatHierarchy = service.GetElementHierarchy();
        Assert.IsNotEmpty(flatHierarchy);

        if (!isElementAndDescendantPrimaryKey)
        {
            // Validate that the descendant element's node correctly references the element.
            var nodes = vim.GetEntityTableSet().NodeTable.ToArray();
            foreach (var item in flatHierarchy)
            {
                var descendantElementIndex = item.Descendant;
                var descendantNodeIndex = item.DescendantNodeIndex;
                if (!descendantNodeIndex.HasValue)
                    continue;

                Assert.AreEqual(descendantElementIndex, nodes[descendantNodeIndex.Value]._Element.Index);
            }
        }
    }
}
