using System.Collections.Generic;
using System.IO;
using Vim.Format.ObjectModel;
using Vim.Math3d;
using Vim.Util;

namespace Vim.Format.ElementParameterInfo
{
    /// <summary>
    /// Deals with revit links and project base point and survey point alignment.
    /// To get your head wrapped around this topic, see: https://www.youtube.com/watch?v=TjP40wpFF34
    /// </summary>
    public class RevitLinkInfo
    {
        // Summary
        public bool ProjectBasePointsAreAligned { get; set; }
        public bool SurveyPointsAreAligned { get; set; }
        public bool ProjectBasePointDataIsEqual { get; set; }
        public bool SurveyPointDataIsEqual { get; set; }
        public string Summary { get; set; }

        // Link
        public FamilyInstance LinkFamilyInstance { get; set; }
        public Element LinkFamilyInstanceElement { get; set; }
        public BimDocument LinkBimDocument { get; set; }
        public BasePoint LinkProjectBasePoint { get; set; }
        public DVector3 LinkProjectBasePointInParentSpace { get; set; }
        public BasePoint LinkSurveyPoint { get; set; }
        public DVector3 LinkSurveyPointInParentSpace { get; set; }

        // Parent
        public BimDocument ParentBimDocument { get; set; }
        public BasePoint ParentProjectBasePoint { get; set; }
        public BasePoint ParentSurveyPoint { get; set; }

        public static List<RevitLinkInfo> GetRevitLinkInfoList(EntityTableSet tableSet)
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
                var linkElementIndex = familyInstanceTable.GetElementIndex(i);
                var categoryIndex = elementTable.GetCategoryIndex(linkElementIndex);
                var builtInCategory = categoryTable.GetBuiltInCategory(categoryIndex);

                if (builtInCategory != "OST_RvtLinks")
                    continue;

                // The family type of the revit link instance contains the title of the linked bim document.
                var familyTypeIndex = familyInstanceTable.GetFamilyTypeIndex(i);
                if (familyTypeIndex < 0)
                    continue;

                var parentBimDocument = elementTable.GetBimDocument(linkElementIndex);
                if (parentBimDocument == null)
                    continue;

                var familyTypeElementIndex = familyTypeTable.GetElementIndex(familyTypeIndex);
                var bimDocumentTitleNoExtension = Path.GetFileNameWithoutExtension(elementTable.GetName(familyTypeElementIndex));

                if (!bimDocumentLinkMap.TryGetValue((parentBimDocument.Index, bimDocumentTitleNoExtension), out var linkedBimDocument) || linkedBimDocument == null)
                    continue;

                var linkFi = familyInstanceTable.Get(i);
                var linkTransform = DMatrix4x4.CreateFromRows(linkFi.BasisX.ToDVector3(), linkFi.BasisY.ToDVector3(), linkFi.BasisZ.ToDVector3(), linkFi.Translation.ToDVector3());

                var pbpLink = bimDocumentProjectBasePointMap.GetOrDefault(linkedBimDocument.Index);
                var spLink = bimDocumentSurveyPointMap.GetOrDefault(linkedBimDocument.Index);

                var pbpParent = bimDocumentProjectBasePointMap.GetOrDefault(parentBimDocument.Index);
                var spParent = bimDocumentSurveyPointMap.GetOrDefault(parentBimDocument.Index);

                var pbpAligned = false;
                DVector3 pbpParentSpacePosition = default;
                var spAligned = false;
                DVector3 spParentSpacePosition = default;
                const float alignmentTolerance = 1E-06f;

                if (pbpLink != null && pbpParent != null)
                {
                    pbpParentSpacePosition = pbpLink.Position.Transform(linkTransform);
                    pbpAligned = pbpParentSpacePosition.AlmostEquals(pbpParent.Position, alignmentTolerance);
                }

                if (spLink != null && spParent != null)
                {
                    spParentSpacePosition = spLink.Position.Transform(linkTransform);
                    spAligned = spParentSpacePosition.AlmostEquals(spParent.Position, alignmentTolerance);
                }

                var info = new RevitLinkInfo
                {
                    LinkFamilyInstance = linkFi,
                    LinkFamilyInstanceElement = elementTable.Get(linkElementIndex),
                    LinkBimDocument = linkedBimDocument,
                    LinkProjectBasePoint = pbpLink,
                    LinkProjectBasePointInParentSpace = pbpParentSpacePosition,
                    LinkSurveyPoint = spLink,
                    LinkSurveyPointInParentSpace = spParentSpacePosition,
                    ParentBimDocument = parentBimDocument,
                    ParentProjectBasePoint = pbpParent,
                    ParentSurveyPoint = spParent,
                    ProjectBasePointsAreAligned = pbpAligned,
                    SurveyPointsAreAligned = spAligned,
                };

                var pbpDelta = info.ParentProjectBasePoint.Position - info.LinkProjectBasePointInParentSpace;
                var pbpDeltaStr = $"({pbpDelta.X:F5}, {pbpDelta.Y:F5}, {pbpDelta.Z:F5}) ft";
                var spDelta = info.ParentSurveyPoint.Position - info.LinkSurveyPointInParentSpace;
                var spDeltaStr = $"({spDelta.X:F5}, {spDelta.Y:F5}, {spDelta.Z:F5}) ft";

                var pbpDataIsEqual =
                    info.ParentProjectBasePoint.NorthSouth.Equals(info.LinkProjectBasePoint.NorthSouth)
                    && info.ParentProjectBasePoint.EastWest.Equals(info.LinkProjectBasePoint.EastWest)
                    && info.ParentProjectBasePoint.Elevation.Equals(info.LinkProjectBasePoint.Elevation)
                    && info.ParentProjectBasePoint.AngleToTrueNorth.Equals(info.LinkProjectBasePoint.AngleToTrueNorth);

                var spDataIsEqual =
                    info.ParentSurveyPoint.NorthSouth.Equals(info.LinkSurveyPoint.NorthSouth)
                    && info.ParentSurveyPoint.EastWest.Equals(info.LinkSurveyPoint.EastWest)
                    && info.ParentSurveyPoint.Elevation.Equals(info.LinkSurveyPoint.Elevation)
                    && info.ParentSurveyPoint.AngleToTrueNorth.Equals(info.LinkSurveyPoint.AngleToTrueNorth);

                info.ProjectBasePointDataIsEqual= pbpDataIsEqual;
                info.SurveyPointDataIsEqual = spDataIsEqual;
                info.Summary =
$@"{(pbpAligned ? "✅" : "❌")} Project Base Point markers {(pbpAligned ? "are" : "are not")} aligned.{(pbpAligned ? "" : $" 🔼 {pbpDeltaStr}")}
{(pbpDataIsEqual ? "✅" : "❌")} Project Base Point data {(pbpDataIsEqual ? "is" : "is not")} equal.
{(spAligned ? "✅" : "❌")} Survey Point markers {(spAligned ? "are" : "are not")} aligned.{(spAligned ? "" : $" 🔼 {spDeltaStr}")}
{(spDataIsEqual? "✅" : "❌")} Survey Point data {(spDataIsEqual ? "is" : "is not")} equal.";

                result.Add(info);
            }

            return result;
        }
    }
}
