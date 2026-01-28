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
            {
                elementDomains[i] = GetElementDomain(i, elementTable, categoryTable, elementKinds, elementIsVisibleIn3dView, categoryDomains);
            }

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
            var categoryDomain = categoryDomains.GetCategoryDomain(builtInCategory);

            switch (categoryDomain)
            {
                case CategoryDomainEnum.Conceptual: return ElementDomainEnum.Conceptual;
                case CategoryDomainEnum.Physical:
                    {
                        var isInstance = elementKind == ElementKind.FamilyInstance;
                        if (isInstance)
                        {
                            var isVisibleIn3dView = elementIsVisibleIn3dView[elementIndex];
                            return isVisibleIn3dView
                                ? ElementDomainEnum.PhysicalVisible
                                : ElementDomainEnum.PhysicalHidden;
                        }
                        return ElementDomainEnum.PhysicalNotInstanced;
                    }
                case CategoryDomainEnum.Topography: return ElementDomainEnum.Topography;
                case CategoryDomainEnum.Group: return ElementDomainEnum.Group;
                case CategoryDomainEnum.Rooms: return ElementDomainEnum.Rooms;
                case CategoryDomainEnum.System: return ElementDomainEnum.System;
                case CategoryDomainEnum.Link:
                    {
                        var isVisibleIn3dView = elementIsVisibleIn3dView[elementIndex];
                        return isVisibleIn3dView
                            ? ElementDomainEnum.LinkVisible
                            : ElementDomainEnum.LinkHidden;
                    }
                case CategoryDomainEnum.Annotation: return ElementDomainEnum.Annotation;
                case CategoryDomainEnum.Symbol: return ElementDomainEnum.Symbol;
                default:
                    Debug.Fail($"Unknown CategoryDomainEnum {categoryDomain:G}");
                    return ElementDomainEnum.Conceptual;
            }
        }
    }
}
