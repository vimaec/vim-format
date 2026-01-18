using System.Collections.Generic;
using System.IO;
using System.Text;
using Vim.Math3d;
using Vim.Util;

using Sys = System;

namespace Vim.Format.ElementParameterInfo
{
    /// <summary>
    /// Deals with revit links and project base point and survey point alignment.
    /// To get your head wrapped around this topic, see: https://www.youtube.com/watch?v=TjP40wpFF34
    /// </summary>
    public class RevitLinkInfo
    {
        // Summary
        public DVector3 ProjectBasePointDelta { get; set; }
        public DVector3 SurveyPointDelta { get; set; }
        public bool ProjectBasePointsAreAligned { get; set; }
        public bool SurveyPointsAreAligned { get; set; }
        public bool ProjectBasePointDataIsEqual { get; set; }
        public bool SurveyPointDataIsEqual { get; set; }
        public string Summary { get; set; }
        public string SummaryMetric { get; set; }

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

        private const float AlignmentTolerance = 1E-06f;
        private const double AlignmentToleranceDouble = 1E-06d;
        private const int StringDoubleDecimals = 6;
        private static string ToStringRounded(double d) => d.ToString($"F{StringDoubleDecimals}");

        public static List<RevitLinkInfo> GetRevitLinkInfoList(VimEntityTableSet tableSet)
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

                if (pbpLink != null && pbpParent != null)
                {
                    pbpParentSpacePosition = pbpLink.Position.Transform(linkTransform);
                    pbpAligned = pbpParentSpacePosition.AlmostEquals(pbpParent.Position, AlignmentTolerance);
                }

                if (spLink != null && spParent != null)
                {
                    spParentSpacePosition = spLink.Position.Transform(linkTransform);
                    spAligned = spParentSpacePosition.AlmostEquals(spParent.Position, AlignmentTolerance);
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

                var pbpDelta = info.LinkProjectBasePointInParentSpace - info.ParentProjectBasePoint.Position; // the corrective offset to align the link project base point marker to the parent's.
                info.ProjectBasePointDelta = pbpDelta;

                var spDelta = info.LinkSurveyPointInParentSpace - info.ParentSurveyPoint.Position; // the corrective offset to align the link survey point marker to the parent's.
                info.SurveyPointDelta = spDelta;

                var pbpDataIsEqual = BasePointDataIsAlmostEqual(info.ParentProjectBasePoint, info.LinkProjectBasePoint, out var pbpDataSummary, out var pbpDataSummaryMetric);
                info.ProjectBasePointDataIsEqual = pbpDataIsEqual;

                var spDataIsEqual = BasePointDataIsAlmostEqual(info.ParentSurveyPoint, info.LinkSurveyPoint, out var spDataSummary, out var spDataSummaryMetric);
                info.SurveyPointDataIsEqual = spDataIsEqual;

                info.Summary = GetSummary(pbpAligned, pbpDelta, pbpDataIsEqual, pbpDataSummary, spAligned, spDelta, spDataIsEqual, spDataSummary, false);
                info.SummaryMetric = GetSummary(pbpAligned, pbpDelta, pbpDataIsEqual, pbpDataSummaryMetric, spAligned, spDelta, spDataIsEqual, spDataSummaryMetric, true);

                result.Add(info);
            }

            return result;
        }

        private static string GetSummary(
            bool pbpAligned,
            DVector3 pbpDelta,
            bool pbpDataIsEqual,
            string pbpDataSummary,
            bool spAligned,
            DVector3 spDelta,
            bool spDataIsEqual,
            string spDataSummary,
            bool isMetric)
        {
            var pbpDeltaStr = DVector3ToStringRounded(pbpDelta, isMetric);
            var spDeltaStr = DVector3ToStringRounded(spDelta, isMetric);

            return
$@"{(pbpAligned ? "✅" : "❌")} Project Base Point markers {(pbpAligned ? "are" : "are not")} aligned.{(pbpAligned ? "" : $" 🔼 {pbpDeltaStr}")}
{(pbpDataIsEqual ? "✅" : "❌")} Project Base Point data {(pbpDataIsEqual ? "is" : "is not")} equal.{(pbpDataIsEqual ? "" : $"{Sys.Environment.NewLine}{pbpDataSummary}")}
{(spAligned ? "✅" : "❌")} Survey Point markers {(spAligned ? "are" : "are not")} aligned.{(spAligned ? "" : $" 🔼 {spDeltaStr}")}
{(spDataIsEqual ? "✅" : "❌")} Survey Point data {(spDataIsEqual ? "is" : "is not")} equal.{(spDataIsEqual ? "" : $"{Sys.Environment.NewLine}{spDataSummary}")}";
        }

        private static string DVector3ToStringRounded(DVector3 v, bool isMetric)
        {
            var unitSuffix = isMetric ? "m" : "ft";
            var x = isMetric ? Units.FeetToMeters(v.X) ?? 0d : v.X;
            var y = isMetric ? Units.FeetToMeters(v.Y) ?? 0d : v.Y;
            var z = isMetric ? Units.FeetToMeters(v.Z) ?? 0d : v.Z;
            return $"({ToStringRounded(x)}, {ToStringRounded(y)}, {ToStringRounded(z)}) {unitSuffix}";
        }

        private static bool BasePointDataIsAlmostEqual(BasePoint bp_parent, BasePoint bp_link, out string summary, out string summaryMetric)
        {
            var summaryList = new List<string>();
            var summaryListMetric = new List<string>();
            const string tabPrefix = "  - ";

            bool CompareBasePointDataValues(string property, double parentValue, double linkValue, bool canConvertFeetToMeters)
            {
                var almostEqual = parentValue.AlmostEquals(linkValue, AlignmentToleranceDouble);
                if (!almostEqual)
                {
                    // Imperial
                    summaryList.Add($"{tabPrefix}[{property}] Parent: {ToStringRounded(parentValue)} <> Link: {ToStringRounded(linkValue)}");

                    // Metric
                    var parentValueMetric = canConvertFeetToMeters ? Units.FeetToMeters(parentValue) ?? 0d : parentValue;
                    var linkValueMetric = canConvertFeetToMeters ? Units.FeetToMeters(linkValue) ?? 0d : linkValue;
                    var unitSuffix = canConvertFeetToMeters ? "m" : "";
                    summaryListMetric.Add($"{tabPrefix}[{property}] Parent: {ToStringRounded(parentValueMetric)}{unitSuffix} <> Link: {ToStringRounded(linkValueMetric)}{unitSuffix}");
                }

                return almostEqual;
            }

            var nsAlmostEqual = CompareBasePointDataValues("N/S", bp_parent.NorthSouth, bp_link.NorthSouth, true);
            var ewAlmostEqual = CompareBasePointDataValues("E/W", bp_parent.EastWest, bp_link.EastWest, true);
            var elevationAlmostEqual = CompareBasePointDataValues("Elevation", bp_parent.Elevation, bp_link.Elevation, true);
            var angleToTrueNorthAlmostEqual = CompareBasePointDataValues("Angle to True North", bp_parent.AngleToTrueNorth, bp_link.AngleToTrueNorth, false);

            var dataIsAlmostEqual = nsAlmostEqual && ewAlmostEqual && elevationAlmostEqual && angleToTrueNorthAlmostEqual;

            summary = dataIsAlmostEqual ? "" : string.Join(Sys.Environment.NewLine, summaryList);
            summaryMetric = dataIsAlmostEqual ? "" : string.Join(Sys.Environment.NewLine, summaryListMetric);

            return dataIsAlmostEqual;
        }
    }
}
