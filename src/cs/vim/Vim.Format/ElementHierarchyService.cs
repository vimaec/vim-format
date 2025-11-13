using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.Format.ObjectModel;
using Vim.Util;

namespace Vim.Format
{
    public class ElementHierarchy
    {
        /// <summary>
        /// The index of the ancestor Element.
        /// </summary>
        public int Element { get; set; }

        /// <summary>
        /// The index of the descendant Element.
        /// </summary>
        public int Descendant { get; set; }

        public bool IsSelf => Element == Descendant;

        /// <summary>
        /// The Descendant Element's Node index in the VIM file.
        /// Value is denormalized here to avoid unnecessary joins.
        /// </summary>
        public int? DescendantNodeIndex { get; set; }

        /// <summary>
        /// The Descendant Element's Geometry index in the VIM file.
        /// Value is denormalized here to avoid unnecessary joins.
        /// </summary>
        public int? DescendantGeometryIndex { get; set; }

        /// <summary>
        /// The distance (in "hops") from the Element to its Descendant.
        /// - A distance of 0 denotes that the Element and the Descendant are the same element.
        /// - A distance of 1 denotes that the Descendant is a direct child of the Element.
        /// - A distance of 2 denotes that the Descendant is a grand-child of the Element.
        /// </summary>
        public int Distance { get; set; }

        /// <summary>
        /// The distance (in "hops") from the root level.
        /// - Elements with Distance = 0 and RootDistance = 0 are considered root-level elements.
        /// - Elements with RootDistance != 0 are considered children.
        /// </summary>
        public int RootDistance { get; set; }
    }

    public class ElementHierarchyService
    {
        private ElementGeometryMap ElementGeometryMap { get; }
        private EntityTableSet TableSet { get; }
        private bool IsElementAndDescendantPrimaryKey { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ElementHierarchyService(
            EntityTableSet tableSet,
            ElementGeometryMap elementGeometryMap,
            bool isElementAndDescendantPrimaryKey = false)
        {
            TableSet = tableSet;
            ElementGeometryMap = elementGeometryMap;
            IsElementAndDescendantPrimaryKey = isElementAndDescendantPrimaryKey;
        }

        /// <summary>
        /// Constructor. Loads the required entity table set.
        /// </summary>
        public ElementHierarchyService(
            FileInfo vimFileInfo,
            ElementGeometryMap elementGeometryMap,
            bool isElementAndDescendantPrimaryKey = false)
            : this(
                new EntityTableSet(
                    vimFileInfo,
                    Array.Empty<string>(), // an empty string table indicates that the strings will not be loaded.
                    n =>
                        n is TableNames.Element ||
                        n is TableNames.Group ||
                        n is TableNames.FamilyInstance ||
                        n is TableNames.System ||
                        n is TableNames.ElementInSystem,
                    inParallel: false
                ),
                elementGeometryMap,
                isElementAndDescendantPrimaryKey)
        { }

        public HashSet<ElementHierarchy> GetElementHierarchy()
        {
            var root = GetElementIndexHierarchy();
            var flattened = FlattenElementHierarchy(root, IsElementAndDescendantPrimaryKey);
            return flattened;
        }

        public Tree<int> GetElementIndexHierarchy()
        {
            var result = new Tree<int> { Value = -1 };

            // Early return if there is no element table
            var elementTable = TableSet.ElementTable;
            if (elementTable == null)
                return result;

            // Initialize the tree of element indices (1 element: 1 tree node, aligned by element index)
            var elementTreeMap = new Tree<int>[elementTable.RowCount];
            for (var elementIndex = 0; elementIndex < elementTreeMap.Length; ++elementIndex)
            {
                var treeNode = new Tree<int> { Value = elementIndex };
                elementTreeMap[elementIndex] = treeNode;
            }

            bool TryGetElementTreeNode(int elementIndex, out Tree<int> treeNode)
            {
                treeNode = null;

                if (elementIndex < 0 || elementIndex >= elementTreeMap.Length)
                    return false;

                treeNode = elementTreeMap[elementIndex];

                return treeNode != null;
            }

            void AddElementParentChildRelationship(int parentElementIndex, int childElementIndex)
            {
                if (!TryGetElementTreeNode(parentElementIndex, out var parent))
                    return;

                if (!TryGetElementTreeNode(childElementIndex, out var child))
                    return;

                parent.AddChild(child);
            }

            // Process the group hierarchy (Element.Group)
            var groupTable = TableSet.GroupTable;
            if (groupTable != null)
            {
                var groupElementIndices = groupTable.Column_ElementIndex;

                // Add all the elements associated to a group.
                var elementGroupIndices = elementTable.Column_GroupIndex;
                for (var elementIndex = 0; elementIndex < elementGroupIndices.Length; ++elementIndex)
                {
                    var elementGroupIndex = elementGroupIndices[elementIndex];
                    if (elementGroupIndex == EntityRelation.None)
                        continue;

                    var groupElementIndex = groupElementIndices[elementGroupIndex]; // parent

                    AddElementParentChildRelationship(groupElementIndex, elementIndex);
                }
            }

            // Process the family instance hierarchy using the SuperComponent relationship.
            var familyInstanceTable = TableSet.FamilyInstanceTable;
            if (familyInstanceTable != null)
            {
                var fiElementIndices = familyInstanceTable.Column_ElementIndex;
                var fiSuperComponentIndices = familyInstanceTable.Column_SuperComponentIndex;
                if (fiElementIndices.Length == fiSuperComponentIndices.Length) // guarding against VIM files which may not have the SuperComponent
                {
                    for (var i = 0; i < fiSuperComponentIndices.Length; ++i)
                    {
                        var fiElementIndex = fiElementIndices[i];
                        var superComponentElementIndex = fiSuperComponentIndices[i];

                        if (superComponentElementIndex == EntityRelation.None)
                            continue;

                        AddElementParentChildRelationship(superComponentElementIndex, fiElementIndex);
                    }
                }
            }

            // Process the elements in curtain systems or curtain walls.
            var systemTable = TableSet.SystemTable;
            var elementInSystemTable = TableSet.ElementInSystemTable;
            if (systemTable != null && elementInSystemTable != null)
            {
                var systems = systemTable.ToArray();

                foreach (var item in elementInSystemTable)
                {
                    var elementIndex = item._Element.Index; // child.
                    var systemIndex = item._System.Index;
                    if (systemIndex == EntityRelation.None)
                        continue;

                    var system = systems[systemIndex];
                    var systemType = system.GetSystemType();
                    if (!(systemType is SystemType.CurtainSystem || systemType is SystemType.CurtainWall))
                        continue;

                    var systemElementIndex = system._Element.Index; // parent

                    AddElementParentChildRelationship(systemElementIndex, elementIndex);
                }
            }

            // Attach all tree items which have no parent to the root/result
            foreach (var node in elementTreeMap.Where(n => n.Parent == null))
                result.AddChild(node);

            return result;
        }

        public class ElementHierarchyComparer : IEqualityComparer<ElementHierarchy>
        {
            private readonly bool _isElementAndDescendantPrimaryKey; // The element and descendant fields are primary keys in v5.4.0.a and previous.

            /// <summary>
            /// Constructor
            /// </summary>
            public ElementHierarchyComparer(bool isElementAndDescendantPrimaryKey)
            {
                _isElementAndDescendantPrimaryKey = isElementAndDescendantPrimaryKey;
            }

            public bool Equals(ElementHierarchy x, ElementHierarchy y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (x is null) return false;
                if (y is null) return false;
                if (x.GetType() != y.GetType()) return false;

                if (_isElementAndDescendantPrimaryKey)
                {
                    // v5.4.0.a and prior
                    return x.Element == y.Element && x.Descendant == y.Descendant;
                }
                else
                {
                    // v5.4.0.b and later
                    return x.Element == y.Element &&
                           x.Descendant == y.Descendant &&
                           x.DescendantNodeIndex == y.DescendantNodeIndex &&
                           x.DescendantGeometryIndex == y.DescendantGeometryIndex &&
                           x.Distance == y.Distance;
                }
            }

            public int GetHashCode(ElementHierarchy obj)
            {
                return HashCodeStd2.Combine(obj.Element, obj.Descendant, obj.Distance);
            }
        }

        public static HashSet<ElementHierarchy> FlattenElementHierarchy(
            Tree<int> root,
            bool isElementAndDescendantPrimaryKey,
            ElementGeometryMap elementGeometryMap)
        {
            // Visits the tree and flattens it to a list of records representing the hierarchy.
            //
            // Sample tree traversal:
            //
            //   root -> A -> B -> C -> D
            //            \-> E
            //
            // Resulting data records (Element | Descendant | Distance | RootDistance):
            //   
            //   A | A | 0 | 0
            //   A | B | 1 | 1
            //   A | E | 1 | 1
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

            var result = new HashSet<ElementHierarchy>(new ElementHierarchyComparer(isElementAndDescendantPrimaryKey));

            // NOTE: this function is only invoked on each leaf; the last item in ancestorElements is the leaf.
            void ProcessLeafAncestors(IReadOnlyList<Tree<int>> ancestorElements, int rootToLeafDistance)
            {
                if (ancestorElements == null || ancestorElements.Count == 0)
                    return;

                var leafIndex = ancestorElements.Count - 1;

                for (var i = 0; i < ancestorElements.Count; ++i)
                {
                    var ancestorElementIndex = ancestorElements[i].Value;
                    for (var j = i; j < ancestorElements.Count; ++j)
                    {
                        var descendantElementIndex = ancestorElements[j].Value;
                        var distance = j - i;

                        var distanceFromLeaf = leafIndex - j;

                        var nodeAndGeometryIndices = elementGeometryMap
                            .ElementAtOrDefault(descendantElementIndex)
                            ?.NodeAndGeometryIndices;

                        if ((nodeAndGeometryIndices?.Count ?? 0) > 0)
                        {
                            // Node and geometry info is present.
                            foreach (var (descendantNodeIndex, descendantGeometryIndex) in nodeAndGeometryIndices)
                            {
                                result.Add(new ElementHierarchy
                                {
                                    Element = ancestorElementIndex,
                                    Descendant = descendantElementIndex,
                                    DescendantNodeIndex = descendantNodeIndex,
                                    DescendantGeometryIndex = descendantGeometryIndex,
                                    Distance = distance,
                                    RootDistance = rootToLeafDistance - distanceFromLeaf
                                });
                            }
                        }
                        else
                        {
                            // Node and geometry info is absent.
                            result.Add(new ElementHierarchy
                            {
                                Element = ancestorElementIndex,
                                Descendant = descendantElementIndex,
                                Distance = distance,
                                RootDistance = rootToLeafDistance - distanceFromLeaf
                            });
                        }
                    }
                }
            }

            void VisitElementTreeNodes(Tree<int> node, IReadOnlyList<Tree<int>> ancestors, int rootDistance)
            {
                if (node.Children.Count != 0)
                    return; // Only process the ancestor list of leaf nodes.

                // No children; this is a terminal node.
                ProcessLeafAncestors(ancestors, rootDistance);
            }

            // Skip the root
            var rootChildren = root.Children;
            foreach (var childList in rootChildren)
                childList.VisitDepthFirst(VisitElementTreeNodes, rootDistance: 0);

            return result;
        }

        /// <summary>
        /// Flattens the element index hierarchy represented by the root. The root is skipped during the traversal.
        /// </summary>
        private HashSet<ElementHierarchy> FlattenElementHierarchy(Tree<int> root, bool isElementAndDescendantPrimaryKey)
            => FlattenElementHierarchy(root, isElementAndDescendantPrimaryKey, ElementGeometryMap);
    }
}
