using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.Format.ObjectModel;
using Vim.Util;

namespace Vim.Format.ElementParameterInfo
{
    public class RevitLinkInfo
    {
        public FamilyInstance RevitLinkFamilyInstance { get; set; }

        public Element RevitLinkFamilyInstanceElement { get; set; }

        public BimDocument RevitLinkBimDocument { get; set; }

        public BasePoint RevitLinkProjectBasePoint { get; set; }

        public BasePoint RevitLinkSurveyPoint { get; set; }

        public BimDocument ParentBimDocument { get; set; }

        public BasePoint ParentProjectBasePoint { get; set; }

        public BasePoint ParentSurveyPoint { get; set; }

        public static List<RevitLinkInfo> GetRevitLinkInfoCollection(EntityTableSet tableSet)
        {
            var result = new List<RevitLinkInfo>();

            var bimDocumentTable = tableSet.BimDocumentTable;
            var basePointTable = tableSet.BasePointTable;
            var categoryTable = tableSet.CategoryTable;
            var elementTable = tableSet.ElementTable;
            var familyInstanceTable = tableSet.FamilyInstanceTable;
            var familyTypeTable = tableSet.FamilyTypeTable;
            //var parameterTable = tableSet.ParameterTable;
            //var parameterLookup = tableSet.ElementIndexMaps.ParameterIndicesFromElementIndex;

            var bimDocumentLinkMap = new Dictionary<(int parentIndex, string titleNoExtension), BimDocument>();
            for (var i = 0; i < bimDocumentTable.RowCount; ++i)
            {
                var bd = bimDocumentTable.Get(i);
                var titleNoExtension = Path.GetFileNameWithoutExtension(bd.Title); // remove the extension; it is not present in the link's family type name.
                if (string.IsNullOrEmpty(titleNoExtension))
                    continue;

                var parentIndex = bd.ParentIndex;
                var key = (parentIndex, titleNoExtension);
                bimDocumentLinkMap[key] = bd;
            }

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

                // The family type of the revit link instance contains the title of the linked bim document.
                var familyTypeIndex = familyInstanceTable.GetFamilyTypeIndex(i);
                if (familyTypeIndex < 0)
                    continue;

                var parentBimDocument = elementTable.GetBimDocument(elementIndex);
                if (parentBimDocument == null)
                    continue;

                var familyTypeElementIndex = familyTypeTable.GetElementIndex(familyTypeIndex);
                var bimDocumentTitleNoExtension = Path.GetFileNameWithoutExtension(elementTable.GetName(familyTypeElementIndex));

                if (!bimDocumentLinkMap.TryGetValue((parentBimDocument.Index, bimDocumentTitleNoExtension), out var linkedBimDocument) || linkedBimDocument == null)
                    continue;

                var revitLinkInfo = new RevitLinkInfo
                {
                    RevitLinkFamilyInstance = familyInstanceTable.Get(i),
                    RevitLinkFamilyInstanceElement = elementTable.Get(elementIndex),
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
