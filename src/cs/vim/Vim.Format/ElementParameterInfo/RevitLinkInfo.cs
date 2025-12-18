using System.Collections.Generic;
using System.IO;
using Vim.Format.ObjectModel;
using Vim.Math3d;
using Vim.Util;

namespace Vim.Format.ElementParameterInfo
{
    public class RevitLinkInfo
    {
        // Summary
        public bool ProjectBasePointsAreAligned { get; set; }
        public bool SurveyPointsAreAligned { get; set; }

        // Link
        public FamilyInstance LinkFamilyInstance { get; set; }
        public Element LinkFamilyInstanceElement { get; set; }
        public BimDocument LinkBimDocument { get; set; }
        public BasePoint LinkProjectBasePoint { get; set; }
        public DVector3 LinkProjectBasePointInParentSpace { get; set; }
        // ...Always empty in links...
        //public string LinkProjectBasePointNS { get; set; }
        //public string LinkProjectBasePointEW { get; set; }
        //public string LinkProjectBasePointElevation { get; set; }
        //public string LinkProjectBasePointAngleToTrueNorth { get; set; }
        public BasePoint LinkSurveyPoint { get; set; }
        public DVector3 LinkSurveyPointInParentSpace { get; set; }
        // ...Always empty in links...
        //public string LinkSurveyPointNS { get; set; }
        //public string LinkSurveyPointEW { get; set; }
        //public string LinkSurveyPointElevation { get; set; }
        //public string LinkSurveyPointAngleToTrueNorth { get; set; }

        // Parent
        public BimDocument ParentBimDocument { get; set; }
        public BasePoint ParentProjectBasePoint { get; set; }
        //public string ParentProjectBasePointNS { get; set; }
        //public string ParentProjectBasePointEW { get; set; }
        //public string ParentProjectBasePointElevation { get; set; }
        //public string ParentProjectBasePointAngleToTrueNorth { get; set; }
        public BasePoint ParentSurveyPoint { get; set; }
        //public string ParentSurveyPointNS { get; set; }
        //public string ParentSurveyPointEW { get; set; }
        //public string ParentSurveyPointElevation { get; set; }
        //public string ParentSurveyPointAngleToTrueNorth { get; set; }

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

                // vvv always empty in links vvv
                //var pbpLinkNS = "";
                //var pbpLinkEW = "";
                //var pbpLinkElev = "";
                //var pbpLinkAngleToTrueNorth = "";
                //if (pbpLink != null)
                //{
                //    GetBasePointParameters(pbpLink.ElementIndex, tableSet, out pbpLinkNS, out pbpLinkEW, out pbpLinkElev, out pbpLinkAngleToTrueNorth);
                //}

                //var spLinkNS = "";
                //var spLinkEW = "";
                //var spLinkElev = "";
                //var spLinkAngleToTrueNorth = "";
                //if (spLink != null)
                //{
                //    GetBasePointParameters(spLink.ElementIndex, tableSet, out spLinkNS, out spLinkEW, out spLinkElev, out spLinkAngleToTrueNorth);
                //}

                //var pbpParentNS = "";
                //var pbpParentEW = "";
                //var pbpParentElev = "";
                //var pbpParentAngleToTrueNorth = "";
                //if (pbpParent != null)
                //{
                //    GetBasePointParameters(pbpParent.ElementIndex, tableSet, out pbpParentNS, out pbpParentEW, out pbpParentElev, out pbpParentAngleToTrueNorth);
                //}

                //var spParentNS = "";
                //var spParentEW = "";
                //var spParentElev = "";
                //var spParentAngleToTrueNorth = "";
                //if (spParent != null)
                //{
                //    GetBasePointParameters(spParent.ElementIndex, tableSet, out spParentNS, out spParentEW, out spParentElev, out spParentAngleToTrueNorth);
                //}

                var revitLinkInfo = new RevitLinkInfo
                {
                    LinkFamilyInstance = linkFi,
                    LinkFamilyInstanceElement = elementTable.Get(linkElementIndex),
                    LinkBimDocument = linkedBimDocument,
                    LinkProjectBasePoint = pbpLink,
                    LinkProjectBasePointInParentSpace = pbpParentSpacePosition,
                    //LinkProjectBasePointNS = pbpLinkNS,
                    //LinkProjectBasePointEW = pbpLinkEW,
                    //LinkProjectBasePointElevation = pbpLinkElev,
                    //LinkProjectBasePointAngleToTrueNorth = pbpLinkAngleToTrueNorth,
                    LinkSurveyPoint = spLink,
                    LinkSurveyPointInParentSpace = spParentSpacePosition,
                    //LinkSurveyPointNS = spLinkNS,
                    //LinkSurveyPointEW = spLinkEW,
                    //LinkSurveyPointElevation = spLinkElev,
                    //LinkSurveyPointAngleToTrueNorth = spLinkAngleToTrueNorth,
                    ParentBimDocument = parentBimDocument,
                    ParentProjectBasePoint = pbpParent,
                    //ParentProjectBasePointNS = pbpParentNS,
                    //ParentProjectBasePointEW = pbpParentEW,
                    //ParentProjectBasePointElevation = pbpParentElev,
                    //ParentProjectBasePointAngleToTrueNorth = pbpParentAngleToTrueNorth,
                    ParentSurveyPoint = spParent,
                    //ParentSurveyPointNS = spParentNS,
                    //ParentSurveyPointEW = spParentEW,
                    //ParentSurveyPointElevation = spParentElev,
                    //ParentSurveyPointAngleToTrueNorth = spParentAngleToTrueNorth,
                    ProjectBasePointsAreAligned = pbpAligned,
                    SurveyPointsAreAligned = spAligned,
                };

                result.Add(revitLinkInfo);
            }

            return result;
        }

        // vvv keeping this code around in case it's ever useful later. Turns out that this parameter data remains empty for Revit links vvv

        //// BASEPOINT_NORTHSOUTH_PARAM
        //public const string paramNameNS = "N/S"; 
        //public const string paramIdNS = "-1150191";

        //// BASEPOINT_EASTWEST_PARAM
        //public const string paramNameEW = "E/W";
        //public const string paramIdEW = "-1150192";

        //// BASEPOINT_ELEVATION_PARAM
        //public const string paramNameElev = "Elev";
        //public const string paramIdElev = "-1150193";

        //// BASEPOINT_ELEVATION_PARAM
        //public const string paramNameAngleToTrueNorth = "Angle to True North";
        //public const string paramIdAngleToTrueNorth = "-1150194";

        //public static void GetBasePointParameters(
        //    int basePointElementIndex,
        //    EntityTableSet tableSet,
        //    out string ns,
        //    out string ew,
        //    out string elev,
        //    out string angleToTrueNorth)
        //{
        //    ns = "";
        //    ew = "";
        //    elev = "";
        //    angleToTrueNorth = "";

        //    if (!tableSet.ElementIndexMaps.ParameterIndicesFromElementIndex.TryGetValue(basePointElementIndex, out var parameterIndices))
        //        return;

        //    var parameterTable = tableSet.ParameterTable;

        //    foreach (var i in parameterIndices)
        //    {
        //        var parameter = parameterTable.Get(i);
        //        var pd = parameter.ParameterDescriptor;

        //        var id = pd.Guid;
        //        var name = pd.Name;

        //        if (string.IsNullOrEmpty(ns) && (id == paramIdNS || name == paramNameNS))
        //        {
        //            (_, ns) = parameter.Values;
        //        }
        //        else if (string.IsNullOrEmpty(ew) && (id == paramIdEW || name == paramNameEW))
        //        {
        //            (_, ew) = parameter.Values;
        //        }
        //        else if (string.IsNullOrEmpty(elev) && (id == paramIdElev || name == paramNameElev))
        //        {
        //            (_, elev) = parameter.Values;
        //        }
        //        else if (string.IsNullOrEmpty(angleToTrueNorth) && (id == paramIdAngleToTrueNorth || name == paramNameAngleToTrueNorth))
        //        {
        //            (_, angleToTrueNorth) = parameter.Values;
        //        }
        //    }
        //}
    }
}
