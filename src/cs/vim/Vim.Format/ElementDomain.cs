using System.Collections.Generic;
using System.Diagnostics;
using Vim.Format.ObjectModel;

namespace Vim.Format
{
    public enum ElementDomainEnum
    {
        Conceptual = 0,
        PhysicalVisible = 10,
        PhysicalHidden = 11,
        PhysicalNotInstanced = 12,
        Topography = 20,
        Group = 30,
        Rooms = 40,
        System = 50,
        LinkVisible = 60,
        LinkHidden = 61,
        Annotation = 70,
        Symbol = 80
    }

    public static class ElementDomainEnumExtensions
    {
        public static string ToDisplayString(this ElementDomainEnum e)
        {
            switch (e)
            {
                case ElementDomainEnum.PhysicalVisible: return "Physical-Visible";
                case ElementDomainEnum.PhysicalHidden: return "Physical-Hidden";
                case ElementDomainEnum.PhysicalNotInstanced: return "Physical-NotInstanced";
                case ElementDomainEnum.LinkVisible: return "Link-Visible";
                case ElementDomainEnum.LinkHidden: return "Link-Hidden";
                default: return e.ToString("G");
            }
        }
    }

    /// <summary>
    /// The ElementDomain is a labeling of each element which facilitates the filtering of large lists of elements.
    /// </summary>
    public static class ElementDomain
    {
        /// <summary>
        /// Returns an array aligned 1:1 with the element table to identify the ElementDomain.
        /// </summary>
        public static ElementDomainEnum[] GetElementDomains(
            ElementTable elementTable,
            CategoryTable categoryTable,
            ElementKind[] elementKinds,
            bool[] elementIsVisibleIn3dView)
        {
            var categoryDomains = CategoryDomain.GetCategoryDomainMap();

            var elementDomains = new ElementDomainEnum[elementTable.RowCount];

            for (var i = 0; i < elementDomains.Length; ++i)
                elementDomains[i] = GetElementDomain(i, elementTable, categoryTable, elementKinds, elementIsVisibleIn3dView, categoryDomains);

            return elementDomains;
        }

        public static ElementDomainEnum GetElementDomain(
            int elementIndex,
            ElementTable elementTable,
            CategoryTable categoryTable,
            ElementKind[] elementKinds,
            bool[] elementIsVisibleIn3dView,
            Dictionary<string, CategoryDomainEnum> categoryDomains)
        {
            var elementKind = elementKinds[elementIndex];

            // Family and FamilyType elements are relegated to the conceptual element domain.
            if (elementKind == ElementKind.Family || elementKind == ElementKind.FamilyType)
                return ElementDomainEnum.Conceptual;

            // Elements which are neither Family or FamilyType are labeled based on the category domain.
            var categoryIndex = elementTable.GetCategoryIndex(elementIndex);
            var builtInCategory = categoryTable.GetBuiltInCategory(categoryIndex);
            
            var hasCategoryDomain = categoryDomains.TryGetValue(builtInCategory, out var categoryDomain);
            if (!hasCategoryDomain)
            {
                // Special case: if the VIM file was sourced from IFC (or some source other than Revit),
                // it probably won't have category domain. In this case, we use the ElementKind.FamilyInstance
                // to determine whether the element domain is actually conceptual.
                return elementKind is ElementKind.FamilyInstance
                    ? GetPhysicalDomain(elementIndex, elementIsVisibleIn3dView)
                    : ElementDomainEnum.Conceptual;
            }

            switch (categoryDomain)
            {
                case CategoryDomainEnum.Conceptual: return ElementDomainEnum.Conceptual;
                case CategoryDomainEnum.Physical:
                    return elementKind is ElementKind.FamilyInstance
                        ? GetPhysicalDomain(elementIndex, elementIsVisibleIn3dView)
                        : ElementDomainEnum.PhysicalNotInstanced;
                case CategoryDomainEnum.Topography: return ElementDomainEnum.Topography;
                case CategoryDomainEnum.Group: return ElementDomainEnum.Group;
                case CategoryDomainEnum.Rooms: return ElementDomainEnum.Rooms;
                case CategoryDomainEnum.System: return ElementDomainEnum.System;
                case CategoryDomainEnum.Link: return GetLinkDomain(elementIndex, elementIsVisibleIn3dView);
                case CategoryDomainEnum.Annotation: return ElementDomainEnum.Annotation;
                case CategoryDomainEnum.Symbol: return ElementDomainEnum.Symbol;
                default:
                    Debug.Fail($"Unknown CategoryDomainEnum {categoryDomain:G}");
                    return ElementDomainEnum.Conceptual;
            }
        }

        public static ElementDomainEnum GetPhysicalDomain(int elementIndex, bool[] elementIsVisibleIn3dView)
            => elementIsVisibleIn3dView[elementIndex]
                ? ElementDomainEnum.PhysicalVisible
                : ElementDomainEnum.PhysicalHidden;

        public static ElementDomainEnum GetLinkDomain(int elementIndex, bool[] elementIsVisibleIn3dView)
            => elementIsVisibleIn3dView[elementIndex]
                ? ElementDomainEnum.LinkVisible
                : ElementDomainEnum.LinkHidden;
    }
}
