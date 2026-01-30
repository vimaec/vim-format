using Vim.Util;

namespace Vim.Format.ObjectModel
{
    public interface IElementKindInfo
    {
        string ElementKind { get; set; }
        int ElementKindEnum { get; set; }
        bool ElementKindIsLeaf { get; set; }
    }

    public static class IElementKindInfoExtensions
    {
        public static void SetElementKindInfo(
            this IElementKindInfo info,
            ElementKind elementKind)
        {
            if (info == null) return;
            info.ElementKind = elementKind.ToDisplayString();
            info.ElementKindEnum = (int) elementKind;
            info.ElementKindIsLeaf = elementKind.IsLeaf();
        }

        public static void SetElementKindInfo(
            this IElementKindInfo info,
            int? elementIndex,
            ElementKind[] elementKinds)
        {
            if (info == null) return;
            var elementKind = elementKinds.ElementAtOrDefault(elementIndex ?? -1, ElementKind.Unknown);
            info.SetElementKindInfo(elementKind);
        }
    }
}
