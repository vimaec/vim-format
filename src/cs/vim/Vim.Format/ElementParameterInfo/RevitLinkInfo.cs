using System.Collections.Generic;
using Vim.Format.ObjectModel;
using Vim.Util;

namespace Vim.Format.ElementParameterInfo
{
    public class RevitLinkInfo
    {
        public FamilyInstance RevitLinkInstance { get; set; }

        public Element RevitLinkInstanceElement { get; set; }

        public BimDocument RevitLinkBimDocument { get; set; }

        public BasePoint RevitLinkProjectBasePoint { get; set; }

        public BasePoint RevitLinkSurveyPoint { get; set; }

        public BimDocument ParentBimDocument { get; set; }

        public BasePoint ParentProjectBasePoint { get; set; }

        public BasePoint ParentSurveyPoint { get; set; }

        // TODO: add transform logic (including mirrored) to place the base point info in the parent coordinate system.

        public static List<RevitLinkInfo> GetRevitLinkInfoCollection(EntityTableSet tableSet)
        {
            var result = new List<RevitLinkInfo>();

            var bimDocumentTable = tableSet.BimDocumentTable;
            var basePointTable = tableSet.BasePointTable;
            var categoryTable = tableSet.CategoryTable;
            var elementTable = tableSet.ElementTable;
            var familyInstanceTable = tableSet.FamilyInstanceTable;
            var familyTypeTable = tableSet.FamilyTypeTable;
            var parameterTable = tableSet.ParameterTable;
            var parameterLookup = tableSet.ElementIndexMaps.ParameterIndicesFromElementIndex;

            // Create a mapping of { BimDocument Element Name -> BimDocument } to look up linked BimDocuments by name (this name is stored in the Revit link instance's family type name)
            var bimDocumentNameMap = new Dictionary<string, BimDocument>();
            for (var i = 0; i < bimDocumentTable.RowCount; ++i)
            {
                var elementIndex = bimDocumentTable.GetElementIndex(i);
                if (elementIndex < 0)
                    continue;

                var name = elementTable.GetName(elementIndex);
                if (string.IsNullOrEmpty(name))
                    continue;

                bimDocumentNameMap[name] = bimDocumentTable.Get(i);
            }

            if (bimDocumentNameMap.Count == 0)
                return result; // nothing to do.

            // Create a mapping of:
            //  - { BimDocument Index -> ProjectBasePoint }
            //  - { BimDocument Index -> SurveyPoint }
            var bimDocumentProjectBasePointMap = new Dictionary<int, BasePoint>();
            var bimDocumentSurveyPointMap = new Dictionary<int, BasePoint>();
            for (var i = 0; i < basePointTable.RowCount; ++i)
            {
                var elementIndex = basePointTable.GetElementIndex(i);
                var bimDocumentIndex = elementTable.GetBimDocumentIndex(elementIndex);
                if (bimDocumentIndex < 0)
                    continue;

                var basePoint = basePointTable.Get(i);
                if (basePoint.IsSurveyPoint)
                {
                    bimDocumentSurveyPointMap[bimDocumentIndex] = basePoint;
                }
                else
                {
                    bimDocumentProjectBasePointMap[bimDocumentIndex] = basePoint;
                }
            }

            // Iterate over the family instance table and collect all family instances whose elements are in the built-in category OST_RvtLinks
            for (var i = 0; i < familyInstanceTable.RowCount; ++i)
            {
                var elementIndex = familyInstanceTable.GetElementIndex(i);
                var categoryIndex = elementTable.GetCategoryIndex(elementIndex);
                var builtInCategory = categoryTable.GetBuiltInCategory(categoryIndex);

                if (builtInCategory != "OST_RvtLinks")
                    continue;

                // The family type of the revit link instance contains the name of the linked bim document.
                var familyTypeIndex = familyInstanceTable.GetFamilyTypeIndex(i);
                if (familyTypeIndex < 0)
                    continue;

                var familyTypeElementIndex = familyTypeTable.GetElementIndex(familyTypeIndex);
                var linkedBimDocumentName = elementTable.GetName(familyTypeElementIndex);

                if (!bimDocumentNameMap.TryGetValue(linkedBimDocumentName, out var linkedBimDocument) || linkedBimDocument == null)
                    continue;

                var parentBimDocument = elementTable.GetBimDocument(elementIndex);

                var revitLinkInfo = new RevitLinkInfo()
                {
                    RevitLinkInstance = familyInstanceTable.Get(i),
                    RevitLinkInstanceElement = elementTable.Get(elementIndex),
                    RevitLinkBimDocument = linkedBimDocument,
                    RevitLinkProjectBasePoint = bimDocumentProjectBasePointMap.GetOrDefault(linkedBimDocument.Index),
                    RevitLinkSurveyPoint = bimDocumentSurveyPointMap.GetOrDefault(linkedBimDocument.Index),
                    ParentBimDocument = parentBimDocument,
                    ParentProjectBasePoint = bimDocumentProjectBasePointMap.GetOrDefault(parentBimDocument.Index),
                    ParentSurveyPoint = bimDocumentSurveyPointMap.GetOrDefault(parentBimDocument.Index)
                };

                result.Add(revitLinkInfo);
            }

            return result;
        }
    }
}
