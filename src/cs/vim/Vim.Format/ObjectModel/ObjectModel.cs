using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vim.Util;
using Vim.Math3d;

// ReSharper disable InconsistentNaming

namespace Vim.Format.ObjectModel
{
    public static class SchemaVersion
    {
        // ReSharper disable MemberHidesStaticFromOuterClass
        public static class History
        {
            // Schema additions (geometry pieces: each placed geometry split by material, Revit subcategory and compound
            // structure layer, one row per g3d submesh; written by the exporter's experimental "geometry pieces" option)
            //   Vim.GeometryPiece__index:Vim.Element:Element
            //   Vim.GeometryPiece__index:Vim.Node:Node
            //   Vim.GeometryPiece__int:SubmeshIndex
            //   Vim.GeometryPiece__index:Vim.Material:Material
            //   Vim.GeometryPiece__index:Vim.Category:Subcategory
            //   Vim.GeometryPiece__index:Vim.CompoundStructureLayer:CompoundStructureLayer
            public const string v5_14_0 = "5.14.0";

            // Schema additions (section graphics: what Revit uses to draw a plan or a section - Object Styles, the Line
            // Weights table, line and fill patterns, material graphics, each view's Visibility/Graphics overrides and the
            // phase graphic overrides)
            //   Vim.Category__int:ProjectionLineWeight
            //   Vim.Category__int:CutLineWeight
            //   Vim.Category__byte:IsCuttable
            //   Vim.Category__index:Vim.LinePattern:ProjectionLinePattern
            //   Vim.Category__index:Vim.LinePattern:CutLinePattern
            //   Vim.Material__byte:GraphicsRead
            //   Vim.Material__double:ShadingColor.X / .Y / .Z
            //   Vim.Material__byte:UseRenderAppearanceForShading
            //   Vim.Material__index:Vim.FillPattern:SurfaceForegroundPattern (+ SurfaceForegroundPatternColor.X/Y/Z)
            //   Vim.Material__index:Vim.FillPattern:SurfaceBackgroundPattern (+ SurfaceBackgroundPatternColor.X/Y/Z)
            //   Vim.Material__index:Vim.FillPattern:CutForegroundPattern (+ CutForegroundPatternColor.X/Y/Z)
            //   Vim.Material__index:Vim.FillPattern:CutBackgroundPattern (+ CutBackgroundPatternColor.X/Y/Z)
            //   Vim.View__byte:GraphicsRead
            //   Vim.FilterInView__index:Vim.GraphicOverride:GraphicOverride
            //   Vim.LinePattern (Name, ElementId, UniqueId, IsSolid, Segments, BimDocument)
            //   Vim.FillPattern (Name, ElementId, UniqueId, Target, IsSolidFill, HostOrientation, GridCount, BimDocument)
            //   Vim.FillGrid (FillPattern, OrderIndex, Angle, Origin.X/Y, Offset, Shift, Segments)
            //   Vim.LineWeight (Table, Scale, Number, Width, BimDocument)
            //   Vim.GraphicOverride (projection / cut lines, surface / cut patterns, transparency, halftone, detail level)
            //   Vim.CategoryInView (View, Category, IsHidden, GraphicOverride)
            //   Vim.ElementOverrideInView (Element, View, IsHidden, GraphicOverride)
            //   Vim.PhaseGraphicOverride (Status, projection / cut line colour, width, weight, pattern, Scale, BimDocument)
            public const string v5_13_0 = "5.13.0";

            // Schema additions (the view audit: what each view does to the model - template, overrides, filters, hidden
            // elements, placement; the grid bubble ends; a stable key per warning to follow it across versions)
            //   Vim.View__index:Vim.Element:ViewTemplate
            //   Vim.View__index:Vim.View:PrimaryView
            //   Vim.View__index:Vim.Phase:Phase
            //   Vim.View__index:Vim.PhaseFilter:PhaseFilter
            //   Vim.View__index:Vim.Element:ScopeBox
            //   Vim.View__string:Discipline
            //   Vim.View__string:ProjectBrowserPath
            //   Vim.View__byte:IsAudited
            //   Vim.View__byte:IsCropBoxActive
            //   Vim.View__byte:IsTemporaryHideIsolateActive
            //   Vim.View__int:CategoryOverrideCount
            //   Vim.View__int:HiddenCategoryCount
            //   Vim.View__int:FilterCount
            //   Vim.View__int:ElementOverrideCount
            //   Vim.View__int:HiddenElementCount
            //   Vim.View__string:TemplateControlledSettings
            //   Vim.FilterInView__string:Name
            //   Vim.FilterInView__string:Kind
            //   Vim.FilterInView__string:FilterUniqueId
            //   Vim.FilterInView__int:OrderIndex
            //   Vim.FilterInView__byte:IsEnabled
            //   Vim.FilterInView__byte:IsVisible
            //   Vim.FilterInView__byte:HasOverrides
            //   Vim.FilterInView__index:Vim.View:View
            //   Vim.Grid__byte:TypeBubbleAtEnd0
            //   Vim.Grid__byte:TypeBubbleAtEnd1
            //   Vim.Grid__byte:BubbleAtEnd0
            //   Vim.Grid__byte:BubbleAtEnd1
            //   Vim.Grid__index:Vim.View:BubbleView
            //   Vim.GridInView__byte:BubbleAtEnd0
            //   Vim.GridInView__byte:BubbleAtEnd1
            //   Vim.GridInView__index:Vim.Grid:Grid
            //   Vim.GridInView__index:Vim.View:View
            //   Vim.Warning__string:Key
            //   Vim.Warning__int:FailingElementCount
            public const string v5_12_0 = "5.12.0";

            // Schema additions (the activity log: who changed what, written at the moment the change happens)
            //   Vim.Contributor__string:Name
            //   Vim.Contributor__int:Source
            //   Vim.SyncSession__string:SessionGuid
            //   Vim.SyncSession__string:MachineName
            //   Vim.SyncSession__string:RevitVersion
            //   Vim.SyncSession__string:StartedUtc
            //   Vim.SyncSession__string:EndedUtc
            //   Vim.SyncSession__index:Vim.Contributor:Contributor
            //   Vim.SyncSession__index:Vim.BimDocument:BimDocument
            //   Vim.ElementChange__string:ChangedUtc
            //   Vim.ElementChange__string:UniqueId
            //   Vim.ElementChange__string:CategoryName
            //   Vim.ElementChange__string:FamilyName
            //   Vim.ElementChange__string:TypeName
            //   Vim.ElementChange__string:ElementName
            //   Vim.ElementChange__string:LevelName
            //   Vim.ElementChange__string:WorksetName
            //   Vim.ElementChange__int:ChangeKind
            //   Vim.ElementChange__int:Confidence
            //   Vim.ElementChange__index:Vim.ExportRevision:ExportRevision
            //   Vim.ElementChange__index:Vim.SyncSession:SyncSession
            //   Vim.ElementChange__index:Vim.Contributor:Contributor
            //   Vim.ActivityMarker__string:OccurredUtc
            //   Vim.ActivityMarker__string:Description
            //   Vim.ActivityMarker__int:Kind
            //   Vim.ActivityMarker__index:Vim.ExportRevision:ExportRevision
            //   Vim.ActivityMarker__index:Vim.SyncSession:SyncSession
            //   Vim.ActivityMarker__index:Vim.Contributor:Contributor
            public const string v5_11_0 = "5.11.0";

            // Schema additions (where each element's data comes from through incremental exports and patches)
            //   Vim.ExportRevision__string:VimRevision
            //   Vim.ExportRevision__int:Kind
            //   Vim.ExportRevision__string:Generator
            //   Vim.ExportRevision__string:CreatedUtc
            //   Vim.ExportRevision__int:DocumentNumSaves
            //   Vim.ExportRevision__string:DocumentVersionGuid
            //   Vim.ExportRevision__index:Vim.BimDocument:BimDocument
            //   Vim.ElementInExportRevision__index:Vim.ExportRevision:ExportRevision
            //   Vim.ElementInExportRevision__index:Vim.Element:Element
            public const string v5_10_0 = "5.10.0";

            // Schema additions (something drawable for the elements that have no triangles: model lines, symbolic lines,
            // void outlines and location curves as Shapes of a given kind, and where each element's Location comes from)
            //   Vim.Element__int:LocationSource
            //   Vim.Shape__int:Kind
            public const string v5_9_0 = "5.9.0";

            // Schema additions (light sources and the sun, previously written as "light/lights.json" and "light/sun.json" assets)
            //   Vim.LightSource__byte:HasLightSourceTransform
            //   Vim.LightSource__byte:IsOn
            //   Vim.LightSource__double:BasisX.X
            //   Vim.LightSource__double:BasisX.Y
            //   Vim.LightSource__double:BasisX.Z
            //   Vim.LightSource__double:BasisY.X
            //   Vim.LightSource__double:BasisY.Y
            //   Vim.LightSource__double:BasisY.Z
            //   Vim.LightSource__double:BasisZ.X
            //   Vim.LightSource__double:BasisZ.Y
            //   Vim.LightSource__double:BasisZ.Z
            //   Vim.LightSource__double:Origin.X
            //   Vim.LightSource__double:Origin.Y
            //   Vim.LightSource__double:Origin.Z
            //   Vim.LightSource__index:Vim.Element:Element
            //   Vim.LightSource__index:Vim.LightType:LightType
            //   Vim.LightType__double:ColorFilter.X
            //   Vim.LightType__double:ColorFilter.Y
            //   Vim.LightType__double:ColorFilter.Z
            //   Vim.LightType__double:Efficacy
            //   Vim.LightType__double:EmitDiameter
            //   Vim.LightType__double:EmitLength
            //   Vim.LightType__double:EmitWidth
            //   Vim.LightType__double:Flux
            //   Vim.LightType__double:Illuminance
            //   Vim.LightType__double:IlluminanceDistance
            //   Vim.LightType__double:InitialColorTemperature
            //   Vim.LightType__double:LossFactor
            //   Vim.LightType__double:Luminosity
            //   Vim.LightType__double:SpotBeamAngle
            //   Vim.LightType__double:SpotFieldAngle
            //   Vim.LightType__double:TiltAngle
            //   Vim.LightType__double:Wattage
            //   Vim.LightType__index:Vim.Asset:PhotometricWebFile
            //   Vim.LightType__index:Vim.FamilyType:FamilyType
            //   Vim.LightType__int:DimmingColor
            //   Vim.LightType__int:InitialIntensityType
            //   Vim.LightType__int:LightDistributionStyle
            //   Vim.LightType__int:LightShapeStyle
            //   Vim.LightType__string:PhotometricWebFileName
            //   Vim.SunAndShadowSettings__byte:RelativeToView
            //   Vim.SunAndShadowSettings__byte:UsesDST
            //   Vim.SunAndShadowSettings__double:Altitude
            //   Vim.SunAndShadowSettings__double:Azimuth
            //   Vim.SunAndShadowSettings__index:Vim.View:View
            //   Vim.SunAndShadowSettings__int:ActiveFrame
            //   Vim.SunAndShadowSettings__int:NumberOfFrames
            //   Vim.SunAndShadowSettings__string:ActiveFrameTime
            //   Vim.SunAndShadowSettings__string:EndDateAndTime
            //   Vim.SunAndShadowSettings__string:StartDateAndTime
            //   Vim.SunAndShadowSettings__string:SunAndShadowType
            public const string v5_8_0 = "5.8.0";

            // Schema additions
            //   Vim.Element__string:Creator
            //   Vim.Element__string:LastChangedBy
            //   Vim.Element__string:Owner
            //   Vim.BasePoint__byte:IsClipped
            //   Vim.BasePoint__double:NorthSouth
            //   Vim.BasePoint__double:EastWest
            //   Vim.BasePoint__double:Elevation
            //   Vim.BasePoint__double:AngleToTrueNorth
            public const string v5_7_0 = "5.7.0";

            // Schema additions
            //   Vim.BimDocument__long:FileLength
            public const string v5_6_0 = "5.6.0";

            // Schema additions
            //   Vim.Level__double:ProjectElevation
            public const string v5_5_0 = "5.5.0";

            // Schema additions
            //   Vim.FamilyInstance__index:Vim.Element:SuperComponent
            public const string v5_4_0 = "5.4.0";

            // Schema additions
            //   Vim.ParameterDescriptor__int:StorageType
            public const string v5_3_0 = "5.3.0";

            // Schema additions (Added support for IfcSite and IfcBuilding)
            //   Vim.Building__double:Elevation
            //   Vim.Building__double:TerrainElevation
            //   Vim.Building__index:Vim.Element:Element
            //   Vim.Building__index:Vim.Site:Site
            //   Vim.Building__string:Address
            //   Vim.Level__index:Vim.Building:Building
            //   Vim.Site__double:Elevation
            //   Vim.Site__double:Latitude
            //   Vim.Site__double:Longitude
            //   Vim.Site__index:Vim.Element:Element
            //   Vim.Site__string:Address
            //   Vim.Site__string:Number
            public const string v5_2_0 = "5.2.0";

            // Schema additions:
            //   Vim.AssetInViewSheet__index:Vim.Asset:Asset
            //   Vim.AssetInViewSheet__index:Vim.ViewSheet:ViewSheet
            //   Vim.ViewInViewSheet__index:Vim.View:View
            //   Vim.ViewInViewSheet__index:Vim.ViewSheet:ViewSheet
            //   Vim.ViewInViewSheetSet__index:Vim.View:View
            //   Vim.ViewInViewSheetSet__index:Vim.ViewSheetSet:ViewSheetSet
            //   Vim.ViewSheet__index:Vim.Element:Element
            //   Vim.ViewSheet__index:Vim.FamilyType:FamilyType
            //   Vim.ViewSheetInViewSheetSet__index:Vim.ViewSheet:ViewSheet
            //   Vim.ViewSheetInViewSheetSet__index:Vim.ViewSheetSet:ViewSheetSet
            //   Vim.ViewSheetSet__index:Vim.Element:Element
            public const string v5_1_0 = "5.1.0";

            // Schema removals (we still read these as fallbacks to preserve backwards compatibility):
            //   Vim.Category__int:Id
            //   Vim.Element__int:Id
            // Schema additions:
            //   Vim.Category__long:Id
            //   Vim.Element__long:Id
            public const string v5_0_0 = "5.0.0";

            // Schema additions:
            //   Vim.Grid__index:Vim.FamilyType:FamilyType
            //   Vim.Level__index:Vim.FamilyType:FamilyType
            //   Vim.Schedule__index:Vim.Element:Element
            //   Vim.ScheduleCell__index:Vim.ScheduleColumn:ScheduleColumn
            //   Vim.ScheduleCell__int:RowIndex
            //   Vim.ScheduleCell__string:Value
            //   Vim.ScheduleColumn__index:Vim.Schedule:Schedule
            //   Vim.ScheduleColumn__int:ColumnIndex
            //   Vim.ScheduleColumn__string:Name
            //   Vim.System__index:Vim.FamilyType:FamilyType
            //   Vim.View__index:Vim.FamilyType:FamilyType
            public const string v4_6_0 = "4.6.0";

            // Schema additions:
            //   Vim.Area__byte:IsGrossInterior
            //   Vim.Area__double:Perimeter
            //   Vim.Area__double:Value
            //   Vim.Area__index:Vim.AreaScheme:AreaScheme
            //   Vim.Area__index:Vim.Element:Element
            //   Vim.Area__string:Number
            //   Vim.AreaScheme__byte:IsGrossBuildingArea
            //   Vim.AreaScheme__index:Vim.Element:Element
            //   Vim.Grid__byte:IsCurved
            //   Vim.Grid__double:EndPoint.X
            //   Vim.Grid__double:EndPoint.Y
            //   Vim.Grid__double:EndPoint.Z
            //   Vim.Grid__double:Extents.Max.X
            //   Vim.Grid__double:Extents.Max.Y
            //   Vim.Grid__double:Extents.Max.Z
            //   Vim.Grid__double:Extents.Min.X
            //   Vim.Grid__double:Extents.Min.Y
            //   Vim.Grid__double:Extents.Min.Z
            //   Vim.Grid__double:StartPoint.X
            //   Vim.Grid__double:StartPoint.Y
            //   Vim.Grid__double:StartPoint.Z
            //   Vim.Grid__index:Vim.Element:Element
            //   Vim.LevelInView__double:Extents.Max.X
            //   Vim.LevelInView__double:Extents.Max.Y
            //   Vim.LevelInView__double:Extents.Max.Z
            //   Vim.LevelInView__double:Extents.Min.X
            //   Vim.LevelInView__double:Extents.Min.Y
            //   Vim.LevelInView__double:Extents.Min.Z
            //   Vim.LevelInView__index:Vim.Level:Level
            //   Vim.LevelInView__index:Vim.View:View
            //   Vim.PhaseFilter__index:Vim.Element:Element
            //   Vim.PhaseFilter__int:Demolished
            //   Vim.PhaseFilter__int:Existing
            //   Vim.PhaseFilter__int:New
            //   Vim.PhaseFilter__int:Temporary
            public const string v4_5_0 = "4.5.0";

            // Schema additions:
            //   Vim.BasePoint__byte:IsSurveyPoint
            //   Vim.BasePoint__double:Position.X
            //   Vim.BasePoint__double:Position.Y
            //   Vim.BasePoint__double:Position.Z
            //   Vim.BasePoint__double:SharedPosition.X
            //   Vim.BasePoint__double:SharedPosition.Y
            //   Vim.BasePoint__double:SharedPosition.Z
            //   Vim.BasePoint__index:Vim.Element:Element
            //   Vim.Element__byte:IsPinned
            //   Vim.Element__string:UniqueId
            //   Vim.Family__byte:IsInPlace
            public const string v4_4_0 = "4.4.0";

            // G3d Additions:
            //   g3d:instance:flags:0:uint16:1 (to support newly captured room geometry being hidden by default)
            // Schema additions:
            //   Vim.MaterialInElement__byte:IsPaint
            //   Vim.MaterialInElement__double:Area
            //   Vim.MaterialInElement__double:Volume
            //   Vim.MaterialInElement__index:Vim.Element:Element
            //   Vim.MaterialInElement__index:Vim.Material:Material
            public const string v4_3_0 = "4.3.0";

            // Schema additions:
            //   Vim.ElementInSystem__index:Vim.Element:Element
            //   Vim.ElementInSystem__index:Vim.System:System
            //   Vim.ElementInSystem__int:Roles
            //   Vim.ElementInWarning__index:Vim.Element:Element
            //   Vim.ElementInWarning__index:Vim.Warning:Warning
            //   Vim.ParameterDescriptor__int:Flags
            //   Vim.ParameterDescriptor__string:Guid
            //   Vim.System__index:Vim.Element:Element
            //   Vim.System__int:SystemType
            //   Vim.Warning__index:Vim.BimDocument:BimDocument
            //   Vim.Warning__string:Description
            //   Vim.Warning__string:Guid
            //   Vim.Warning__string:Severity
            //   Vim.Workset__index:Vim.BimDocument:BimDocument
            public const string v4_2_0 = "4.2.0";

            // Schema additions:
            //   Vim.BimDocument__index:Vim.BimDocument:Parent
            //   Vim.CompoundStructure__double:Width
            //   Vim.CompoundStructure__index:Vim.CompoundStructureLayer:StructuralLayer
            //   Vim.ShapeCollection__index:Vim.Element:Element
            //   Vim.ShapeInShapeCollection__index:Vim.Shape:Shape
            //   Vim.ShapeInShapeCollection__index:Vim.ShapeCollection:ShapeCollection
            public const string v4_1_0 = "4.1.0";

            // Object model schema version released with VIM 1.0
            public const string v4_0_0 = "4.0.0";
        }
        // ReSharper enable MemberHidesStaticFromOuterClass

        // [MAINTAIN] Add more object model SerializableVersions below and update the current one.
        public static SerializableVersion Current => v5_14_0;
        public static SerializableVersion v5_14_0 => SerializableVersion.Parse(History.v5_14_0);
        public static SerializableVersion v5_13_0 => SerializableVersion.Parse(History.v5_13_0);
        public static SerializableVersion v5_12_0 => SerializableVersion.Parse(History.v5_12_0);
        public static SerializableVersion v5_11_0 => SerializableVersion.Parse(History.v5_11_0);
        public static SerializableVersion v5_10_0 => SerializableVersion.Parse(History.v5_10_0);
        public static SerializableVersion v5_9_0 => SerializableVersion.Parse(History.v5_9_0);
        public static SerializableVersion v5_8_0 => SerializableVersion.Parse(History.v5_8_0);
        public static SerializableVersion v5_7_0 => SerializableVersion.Parse(History.v5_7_0);
        public static SerializableVersion v5_6_0 => SerializableVersion.Parse(History.v5_6_0);
        public static SerializableVersion v5_5_0 => SerializableVersion.Parse(History.v5_5_0);
        public static SerializableVersion v5_4_0 => SerializableVersion.Parse(History.v5_4_0);
        public static SerializableVersion v5_3_0 => SerializableVersion.Parse(History.v5_3_0);
        public static SerializableVersion v5_2_0 => SerializableVersion.Parse(History.v5_2_0);
        public static SerializableVersion v5_1_0 => SerializableVersion.Parse(History.v5_1_0);
        public static SerializableVersion v5_0_0 => SerializableVersion.Parse(History.v5_0_0);
        public static SerializableVersion v4_6_0 => SerializableVersion.Parse(History.v4_6_0);
        public static SerializableVersion v4_5_0 => SerializableVersion.Parse(History.v4_5_0);
        public static SerializableVersion v4_4_0 => SerializableVersion.Parse(History.v4_4_0);
        public static SerializableVersion v4_3_0 => SerializableVersion.Parse(History.v4_3_0);
        public static SerializableVersion v4_2_0 => SerializableVersion.Parse(History.v4_2_0);
        public static SerializableVersion v4_1_0 => SerializableVersion.Parse(History.v4_1_0);
        public static SerializableVersion v4_0_0 => SerializableVersion.Parse(History.v4_0_0);
    }

    public partial class DocumentModel
    {
        public readonly Document Document;
    }

    public enum G3dAttributeReferenceMultiplicity {
        OneToOne,
        OneToMany,
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class G3dAttributeReferenceAttribute : Attribute
    {
        public readonly G3dAttributeReferenceMultiplicity AttributeReferenceMultiplicity;
        public readonly string AttributeName;
        public readonly bool AttributeIsOptional;

        public G3dAttributeReferenceAttribute(
            string attributeName,
            G3dAttributeReferenceMultiplicity attributeReferenceMultiplicity,
            bool attributeIsOptional = false)
        {
            AttributeReferenceMultiplicity = attributeReferenceMultiplicity;
            AttributeName = attributeName;
            AttributeIsOptional = attributeIsOptional;
        }
    }

    public enum ElementKind
    {
        Unknown = 0,
        FamilyInstance = 1,
        FamilyType = 2,
        Family = 3,
        Group = 4,
        System = 5,
        Room = 6,
        Area = 7,
        AreaScheme = 8,
        Level = 9,
        Grid = 10,
        Building = 11,
        Site = 12,
        BasePoint = 13,
        BimDocument = 14,
        Material = 15,
        Phase = 16,
        PhaseFilter = 17,
        View = 18,
        ViewSheet = 19,
        ViewSheetSet = 20,
        Schedule = 21,
        AssemblyInstance = 22,
        DesignOption = 23,
        // [MAINTAIN]
        // - Add more element kinds here if new element entities are added; do not re-order this enum!
        // - Update the ToDisplayString extension function below.
        // - Also create a new SQL vw_Element_v* view with new element kind mapping.
    }

    public static class ElementKindExtensions
    {
        /// <summary>
        /// Returns a display string representation of the element kind.
        /// </summary>
        public static string ToDisplayString(this ElementKind k)
        {
            string str;
            switch (k)
            {
                case ElementKind.FamilyInstance:
                    str = "Family Instance";
                    break;
                case ElementKind.FamilyType:
                    str = "Family Type";
                    break;
                case ElementKind.AreaScheme:
                    str = "Area Scheme";
                    break;
                case ElementKind.BasePoint:
                    str = "Base Point";
                    break;
                case ElementKind.BimDocument:
                    str = "BIM Document";
                    break;
                case ElementKind.PhaseFilter:
                    str = "Phase Filter";
                    break;
                case ElementKind.ViewSheet:
                    str = "View Sheet";
                    break;
                case ElementKind.ViewSheetSet:
                    str = "View Sheet Set";
                    break;
                case ElementKind.AssemblyInstance:
                    str = "Assembly Instance";
                    break;
                case ElementKind.DesignOption:
                    str = "Design Option";
                    break;
                default:
                    str = k.ToString("G");
                    break;
            }
            return str;
        }

        /// <summary>
        /// Returns true if the ElementKind is considered to be a terminal leaf in its hierarchy.
        /// This is used to facilitate the removal of Family and FamilyType elements in
        /// lists which may count elements by category.
        /// 
        /// For example, to accureately count elements with the "Door" category in a building,
        /// you wouldn't want to include Family and FamilyType elements which also have the
        /// "Door" category; this would lead to over-counting since Family and FamilyType elements
        /// should not be considered countable "leaf" nodes (they are conceptual).
        /// </summary>
        public static bool IsLeaf(this ElementKind k)
        {
            switch (k)
            {
                case ElementKind.Family:
                case ElementKind.FamilyType:
                    return false;
                default:
                    return true;
            }
        }
    }

    public interface IElementKindTable : IEntityTable
    {
        int GetElementIndex(int entityIndex);
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ElementKindAttribute : Attribute
    {
        public ElementKind ElementKind;

        public ElementKindAttribute(ElementKind elementKind)
        {
            ElementKind = elementKind;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class IgnoreInEquality : Attribute { }

    public interface IStorageKey
    {
        /// <summary>
        /// Returns an object which uniquely identifies an Entity based on its data contents.
        /// </summary>
        object GetStorageKey();
    }

    public static class StorageKeyExtensions
    {
        public static ulong CombineAsUInt64(this int value1, int value2)
        {
            unchecked
            {
                return ((ulong) value1 << 32) | (uint) value2;
            }
        }

        public static ulong CombineAsStorageKey<T, U>(this Relation<T> relation1, Relation<U> relation2)
            => (relation1?.Index ?? EntityRelation.None).CombineAsUInt64(relation2?.Index ?? EntityRelation.None);
    }

    /// <summary>
    /// Represents a base object in the object model.
    /// </summary>
    public partial class Entity
    {
        public int Index;
        public Document Document;

        public virtual bool FieldsAreEqual(object obj)
            => throw new NotImplementedException();
    }

    /// <summary>
    /// Helper class pertaining to entity relations.
    /// </summary>
    public static class EntityRelation
    {
        public const int None = VimConstants.NoEntityRelation;
        public static int IndexOrDefault(this Entity entity)
            => entity?.Index ?? None;

        public static Func<int, T> GetterOrDefault<T>(this T entity) where T : Entity
        {
            if (entity == null)
                return null;

            return _ => entity;
        }
    }

    /// <summary>
    /// Represents a referenced Entity. Lazily instantiates the referenced Entity when reading/deserializing a VIM file.
    /// </summary>
    public class Relation<T>
    {
        public int Index;
        public Func<int, T> Getter;
        public T Value =>
            Getter != null
                ? Getter.Invoke(Index)
                : default;
        public Relation(int index, Func<int, T> getter)
            => (Index, Getter) = (index, getter);
        public Relation(int index = EntityRelation.None)
            => Index = index;
    }

    public interface IElementIndex
    {
        int GetElementIndexOrNone();
    }

    /// <summary>
    /// Represents an Entity which contains a Relation to an Element.
    /// </summary>
    public partial class EntityWithElement : Entity, IElementIndex
    {
        public Relation<Element> _Element;

        public int GetElementIndexOrNone()
            => _Element?.Index ?? EntityRelation.None;
    }

    /// <summary>
    /// Represents an Asset buffer in the VIM file.
    /// </summary>
    [TableName(TableNames.Asset)]
    public partial class Asset : Entity
    {
        /// <summary>
        /// The name of the Asset buffer in the VIM file.
        /// </summary>
        public string BufferName;
    }

    /// <summary>
    /// Defines the display units of a ParameterDefinition
    /// </summary>
    [TableName(TableNames.DisplayUnit)]
    public partial class DisplayUnit : Entity, IStorageKey
    {
        /// <summary>
        /// Corresponds to an underlying physical unit, for example a length, an area, a volume, etc.
        /// In Revit 2020 and earlier, this is serialized as an internal string, ex: "UT_Length".
        /// In Revit 2021 and beyond, this is serialized as a ForgeTypeId, ex: "autodesk.spec.aec:length-1.0.0"
        /// </summary>
        public string Spec;

        /// <summary>
        /// Corresponds to the display unit type.
        /// In Revit 2020 and earlier, this is serialized as an internal string, ex: "DUT_FEET_FRACTIONAL_INCHES"
        /// In Revit 2021 and beyond, this is serialized as a ForgeTypeId, ex: "autodesk.unit.unit:feetFractionalInches-1.0.0"
        /// </summary>
        public string Type;

        /// <summary>
        /// Corresponds to the localized display unit type, ex: "Feet and Fractional Inches".
        /// </summary>
        public string Label;

        public object GetStorageKey()
            => (Spec, Type, Label);

        public static DisplayUnit Empty
            => new DisplayUnit { Spec = "", Type = "", Label = ""};
    }

    /// <summary>
    /// Qualifies the origin of the parameter descriptor. This enum is defined as a set of bitwise flags in case these values need to be composed in the future.
    /// </summary>
    [Flags]
    public enum ParameterDescriptorFlag
    {
        None = 0,
        IsBuiltIn = 1,
        IsProject = 1 << 1,
        IsGlobal = 1 << 2,
    }

    /// <summary>
    /// Defines the storage type of the parameter descriptor.
    /// </summary>
    public enum ParameterDescriptorStorageType
    {
        Unknown = 0,
        Integer = 1,
        Double = 2,
        String = 3,
        ElementId = 4
    }

    /// <summary>
    /// Represents a parameter descriptor.
    /// </summary>
    [TableName(TableNames.ParameterDescriptor)]
    public partial class ParameterDescriptor : Entity, IStorageKey
    {
        /// <summary>
        /// The name of the parameter.
        /// </summary>
        public string Name;

        /// <summary>
        /// The group in which the parameter belongs.
        /// </summary>
        public string Group;

        /// <summary>
        /// The serialized type of the parameter value.
        /// </summary>
        public string ParameterType;

        // Maintenance note: IsInstance, IsShared, and IsReadOnly were added prior to object model v4.2.0 and are preserved for backwards compatibility.
        // Moving forward, additional flags which qualify the nature of the parameter descriptor should be defined in the Flags field as a combination of ParameterDescriptorFlag enums.

        /// <summary>
        /// If this parameter descriptor defines a project parameter (i.e. Flags & ParameterDescriptorFlags.IsProject == ParameterDescriptorFlags.IsProject),
        /// this value determines whether the parameter descriptor is InstanceBinding (IsInstance = true) or TypeBinding (IsInstance = false) in Revit.
        /// </summary>
        public bool IsInstance;
        
        /// <summary>
        /// Determines whether this parameter is shared.
        /// </summary>
        public bool IsShared;
        
        /// <summary>
        /// Determines whether this parameter is read only.
        /// </summary>
        public bool IsReadOnly;

        /// <summary>
        /// The collection of ParameterDescriptorFlag enum values serialized as an int.
        /// </summary>
        public int Flags;
        
        /// <summary>
        /// If the parameter definition is shared (IsShared==true), represents the GUID if the shared parameter.
        /// If the parameter definition is internal (IsShared==false) represents:
        /// - (In Revit 2021 and earlier) the ID of the parameter.
        /// - (In Revit 2022 and later) the TypeId of the parameter (ForgeTypeId).
        /// </summary>
        public string Guid;

        /// <summary>
        /// The storage type of the parameter serialized as an int (see enum ParameterDescriptorStorageType)
        /// Unknown = 0, Integer = 1, Double = 2, String = 3, ElementId = 4
        /// </summary>
        public int StorageType;

        public Relation<DisplayUnit> _DisplayUnit;

        public object GetStorageKey()
            => (Name,
                Group,
                IsInstance,
                IsShared,
                IsReadOnly,
                ParameterType,
                Flags,
                Guid,
                StorageType,
                _DisplayUnit?.Index ?? -1);

        public bool IsBuiltIn
            => ((ParameterDescriptorFlag) Flags & ParameterDescriptorFlag.IsBuiltIn) == ParameterDescriptorFlag.IsBuiltIn;

        public bool IsProject
            => ((ParameterDescriptorFlag) Flags & ParameterDescriptorFlag.IsProject) == ParameterDescriptorFlag.IsProject;

        /// <summary>
        /// Invoked to set Flags and to assign the IsInstance/IsShared/IsReadOnly values.
        /// </summary>
        public ParameterDescriptor UpdateFlags(
            bool isShared,
            bool isReadOnly,
            bool isBuiltIn,
            bool isProject,
            bool isInstanceBinding,
            bool isGlobal)
        {
            IsInstance = isInstanceBinding;
            IsReadOnly = isReadOnly;
            IsShared = isShared;

            var flags = ParameterDescriptorFlag.None;

            if (isBuiltIn) flags |= ParameterDescriptorFlag.IsBuiltIn;
            if (isProject) flags |= ParameterDescriptorFlag.IsProject;
            if (isGlobal) flags |= ParameterDescriptorFlag.IsGlobal;

            Flags = (int) flags;

            return this;
        }

        public ParameterDescriptorFlag GetParameterDescriptorFlags()
            => (ParameterDescriptorFlag) Flags;

        public ParameterDescriptorStorageType GetParameterDescriptorStorageType()
            => (ParameterDescriptorStorageType) StorageType;

        public static string GetParameterDescriptorFlagString(ParameterDescriptorFlag flag)
        {
            switch (flag)
            {
                case ParameterDescriptorFlag.IsBuiltIn:
                    return "Built In";
                case ParameterDescriptorFlag.IsProject:
                    return "Project";
                case ParameterDescriptorFlag.IsGlobal:
                    return "Global";
                default:
                    return "Standard";
            }
        }

        public static string GetParameterDescriptorStorageTypeString(ParameterDescriptorStorageType storageType)
        {
            switch (storageType)
            {
                case ParameterDescriptorStorageType.Integer:
                    return "Integer";
                case ParameterDescriptorStorageType.Double:
                    return "Double";
                case ParameterDescriptorStorageType.String:
                    return "String";
                case ParameterDescriptorStorageType.ElementId:
                    return "ElementId";
                case ParameterDescriptorStorageType.Unknown:
                default:
                    return "Unknown";
            }
        }
    }

    /// <summary>
    /// Represents a parameter associated to an Element. An Element can contain 0..* Parameters.
    /// </summary>
    [TableName(TableNames.Parameter)]
    [CascadeElementRemap]
    public partial class Parameter : EntityWithElement
    {
        /// <summary>
        /// A pipe-separated "NativeValue|DisplayValue" string.<br/>
        /// Pipe "|" or backslash "\" characters contained inside the NativeValue or DisplayValue parts are escaped with the backslash "\" character.
        /// If the value is not pipe-separated, it is both the NativeValue and the DisplayValue.
        /// </summary>
        public string Value;
        public Relation<ParameterDescriptor> _ParameterDescriptor;

        /// <summary>
        /// Gets and sets the pipe-separated "NativeValue|DisplayValue" strings encoded in the Value field.<br/>
        /// </summary>
        public (string NativeValue, string DisplayValue) Values
        {
            get => SplitValues(Value);
            set => Value = value.NativeValue == value.DisplayValue
                ? PipeSeparatedStrings.Join(value.NativeValue) // NOTE: this call also escapes any pipes contained in the NativeValue.
                : PipeSeparatedStrings.Join(value.NativeValue, value.DisplayValue);
        }

        public static (string NativeValue, string DisplayValue) SplitValues(string value)
        {
            var values = PipeSeparatedStrings.Split(value);

            if (values == null || values.Length == 0)
                return ("", "");

            if (values.Length == 1)
                return (values[0], values[0]); // re-use the value for both the native and display value.

            // values contains more than one item.
            return (values[0], values[1]);
        }

        public static bool TryParseNativeValueAsDouble(string nativeValue, out double result)
            => double.TryParse(nativeValue, out result) && !result.IsInfinity() && !result.IsNaN();

        public bool TryParseNativeValueAsDouble(out double result)
            => TryParseNativeValueAsDouble(Values.NativeValue, out result);

        public static double? ParseNativeValueAsDouble(string nativeValue)
            => TryParseNativeValueAsDouble(nativeValue, out var result) ? result : (double?) null;

        public static bool TryParseNativeValueAsLong(string nativeValue, out long result)
            => long.TryParse(nativeValue, out result);

        public bool TryParseNativeValueAsLong(out long result)
            => TryParseNativeValueAsLong(Values.NativeValue, out result);

        public static long? ParseNativeValueAsLong(string nativeValue)
            => TryParseNativeValueAsLong(nativeValue, out var result) ? result : (long?) null;

        public static bool TryParseNativeValueAsElementId(string nativeValue, out long result)
            => TryParseNativeValueAsLong(nativeValue, out result);

        public bool TryParseNativeValueAsElementId(out long result)
            => TryParseNativeValueAsLong(out result);

        public static long? ParseNativeValueAsElementId(string nativeValue)
            => ParseNativeValueAsLong(nativeValue);

        public static bool TryParseNativeValueAsBoolean(string nativeValue, ParameterDescriptor desc, out bool result)
        {
            result = false;

            switch ((ParameterDescriptorStorageType)desc.StorageType)
            {
                case ParameterDescriptorStorageType.ElementId:
                    if (!long.TryParse(nativeValue, out var parsedElementId))
                        return false;

                    result = parsedElementId != -1L; // -1 Element id indicates an unassigned element ID in Revit
                    return true;

                case ParameterDescriptorStorageType.Integer:
                    if (!long.TryParse(nativeValue, out var parsedLong))
                        return false;

                    result = parsedLong != 0L; // 0 = false in Revit
                    return true;

                case ParameterDescriptorStorageType.Double:
                    if (!double.TryParse(nativeValue, out var parsedDouble))
                        return false;

                    result = parsedDouble != 0d; // 0 = false in Revit
                    return true;

                case ParameterDescriptorStorageType.String:
                case ParameterDescriptorStorageType.Unknown:
                default:
                    return bool.TryParse(nativeValue, out result);
            }
        }

        public bool TryParseNativeValueAsBoolean(ParameterDescriptor desc, out bool result)
            => TryParseNativeValueAsBoolean(Values.NativeValue, desc, out result);

        public static (bool HasValue, bool IsDubious) GetValueInfo(string value)
        {
            var trimmed = (value ?? "").Trim();

            var hasValue = !string.IsNullOrEmpty(trimmed);
            var isDubious = hasValue && (trimmed == "-1" || trimmed == "0");

            return (hasValue, isDubious);
        }
    }

    /// <summary>
    /// Where an Element's Location comes from (Element.LocationSource).
    /// </summary>
    public enum ElementLocationSource
    {
        /// <summary>Not recorded (files written before object model 5.9.0, or an exporter which does not fill it).</summary>
        Unknown = 0,
        /// <summary>The element has no placement (for example a type, a view, or an element without location or extent). Location is (0, 0, 0).</summary>
        None = 1,
        /// <summary>Revit: the element's LocationPoint.Point.</summary>
        LocationPoint = 2,
        /// <summary>Revit: the middle of the element's LocationCurve (Curve.Evaluate(0.5, normalized)).</summary>
        LocationCurve = 3,
        /// <summary>Revit: the origin of the family instance's total transform (FamilyInstance.GetTotalTransform().Origin), for an instance without a location.</summary>
        InstanceOrigin = 4,
        /// <summary>Revit: the centre of the element's model bounding box (Element.get_BoundingBox(null)), for an element with no other placement.</summary>
        BoundingBoxCenter = 5,
    }

    /// <summary>
    /// Represents an object which can be associated to a collection of Parameters.
    /// </summary>
    [TableName(TableNames.Element)]
    public partial class Element : Entity
    {
        [EntityColumnLoader("int:Id", typeof(int), SchemaVersion.History.v4_6_0)]
        public long Id;

        public string Type;
        public string Name;
        /// <summary>
        /// Maps to the Element.UniqueId property in Revit.<br/>
        /// Caution: This ID will not always be unique, for example: the "Existing" Phase element's UniqueId has been observed to be shared between linked models (ex: Skanska).<br/>
        /// We export this value to enable a correlation with other tools which persist the Element.UniqueId from Revit.
        /// </summary>
        public string UniqueId;

        /// <summary>
        /// A point which places the element, in feet. LocationSource says where it comes from.<br/>
        /// Since object model 5.9.0 (when LocationSource is set) the point is in the same world space as the geometry
        /// (a linked document's transform is applied), so an element without triangles can still be found and zoomed to.
        /// </summary>
        public float Location_X;
        public float Location_Y;
        public float Location_Z;
        public Vector3 Location
        {
            get => new Vector3(Location_X, Location_Y, Location_Z);
            set => (Location_X, Location_Y, Location_Z) = value;
        }

        /// <summary>
        /// The integer representation of the ElementLocationSource enum: where Location comes from.
        /// 0 (Unknown) in files written before 5.9.0, where Location is (0, 0, 0) both for "at the origin" and for "no location".
        /// </summary>
        public int LocationSource;

        public ElementLocationSource GetLocationSource()
            => (ElementLocationSource) LocationSource;

        public string FamilyName;
        public bool IsPinned;

        public string Creator;
        public string LastChangedBy;
        public string Owner;

        public Relation<Level> _Level;
        public Relation<Phase> _PhaseCreated;
        public Relation<Phase> _PhaseDemolished;
        public Relation<Category> _Category;
        public Relation<Workset> _Workset;
        public Relation<DesignOption> _DesignOption;
        public Relation<View> _OwnerView;
        public Relation<Group> _Group;
        public Relation<AssemblyInstance> _AssemblyInstance;
        public Relation<BimDocument> _BimDocument;
        public Relation<Room> _Room;
    }

    /// <summary>
    /// Represents a named collection of Elements used for organizational purposes.
    /// </summary>
    [TableName(TableNames.Workset)]
    public partial class Workset : Entity
    {
        public int Id;
        public string Name;
        public string Kind;
        public bool IsOpen;
        public bool IsEditable;
        public string Owner;
        public string UniqueId;
        public Relation<BimDocument> _BimDocument;
    }

    [TableName(TableNames.AssemblyInstance)]
    [ElementKind(ElementKind.AssemblyInstance)]
    public partial class AssemblyInstance : EntityWithElement
    {
        public string AssemblyTypeName;
        public float Position_X;
        public float Position_Y;
        public float Position_Z;
        public Vector3 Position
        {
            get => new Vector3(Position_X, Position_Y, Position_Z);
            set => (Position_X, Position_Y, Position_Z) = value;
        }
    }

    [TableName(TableNames.Group)]
    [CascadeElementRemap] // Groups can be family instances
    [ElementKind(ElementKind.Group)]
    public partial class Group : EntityWithElement
    {
        public string GroupType;
        public float Position_X;
        public float Position_Y;
        public float Position_Z;
        public Vector3 Position
        {
            get => new Vector3(Position_X, Position_Y, Position_Z);
            set => (Position_X, Position_Y, Position_Z) = value;
        }
    }

    [TableName(TableNames.DesignOption)]
    [ElementKind(ElementKind.DesignOption)]
    public partial class DesignOption : EntityWithElement
    {
        public bool IsPrimary;
    }

    /// <summary>
    /// Represents an XY plane at a specific Z coordinate in the model.
    /// </summary>
    [TableName(TableNames.Level)]
    [ElementKind(ElementKind.Level)]
    public partial class Level : EntityWithElement
    {
        /// <summary>
        /// The elevation above or below the ground level.
        /// - Revit: maps to Level.Elevation
        /// - IFC: maps to IfcBuildingStorey.Elevation
        /// </summary>
        public double Elevation;

        /// <summary>
        /// The elevation relative to the project origin.
        /// - Revit: maps to Level.ProjectElevation
        /// - IFC: maps to IfcBuildingStorey.Elevation + IfcBuilding.ElevationOfRefHeight
        /// </summary>
        public double ProjectElevation;

        /// <summary>
        /// The associated Level's FamilyType (in Revit, this maps to its LevelType)
        /// </summary>
        public Relation<FamilyType> _FamilyType;

        /// <summary>
        /// The associated Level's building.
        /// </summary>
        public Relation<Building> _Building;
    }

    /// <summary>
    /// Represents a phase of construction.
    /// </summary>
    [TableName(TableNames.Phase)]
    [ElementKind(ElementKind.Phase)]
    public partial class Phase : EntityWithElement
    { }

    /// <summary>
    /// Represents a room in the model.
    /// </summary>
    [TableName(TableNames.Room)]
    [ElementKind(ElementKind.Room)]
    public partial class Room : EntityWithElement
    {
        public double BaseOffset;
        public double LimitOffset;
        public double UnboundedHeight;
        public double Volume;
        public double Perimeter;
        public double Area;
        public string Number;
        public Relation<Level> _UpperLimit;
    }

    /// <summary>
    /// Represents a source BIM document, for example: a Revit file, or an IFC file.
    /// </summary>
    [TableName(TableNames.BimDocument)]
    [ElementKind(ElementKind.BimDocument)]
    public partial class BimDocument : EntityWithElement
    {
        public string Title;
        public bool IsMetric;

        [IgnoreInEquality]
        //Ignore Guid in equality comparer because it gets exported different by revit when it should be the same.
        public string Guid;

        public int NumSaves;
        public bool IsLinked;
        public bool IsDetached;
        public bool IsWorkshared;
        public string PathName;
        public double Latitude;
        public double Longitude;
        public double TimeZone;
        public string PlaceName;
        public string WeatherStationName;
        public double Elevation;
        public string ProjectLocation;
        public string IssueDate;
        public string Status;
        public string ClientName;
        public string Address;
        public string Name;
        public string Number;
        public string Author;
        public string BuildingName;
        public string OrganizationName;
        public string OrganizationDescription;
        public string Product;
        public string Version;
        public string User;
        /// <summary>
        /// File length in bytes
        /// </summary>
        public long FileLength;
        public Relation<View> _ActiveView;
        public Relation<Family> _OwnerFamily;
        public Relation<BimDocument> _Parent;
    }

    /// <summary>
    /// An associative table used to list the DisplayUnits in a BimDocument.
    /// </summary>
    [TableName(TableNames.DisplayUnitInBimDocument)]
    public partial class DisplayUnitInBimDocument : Entity, IStorageKey
    {
        public Relation<DisplayUnit> _DisplayUnit;
        public Relation<BimDocument> _BimDocument;

        public object GetStorageKey()
            => _DisplayUnit.CombineAsStorageKey(_BimDocument);
    }

    /// <summary>
    /// An associative table used to order the Phases in a BimDocument.
    /// </summary>
    [TableName(TableNames.PhaseOrderInBimDocument)]
    public partial class PhaseOrderInBimDocument : Entity, IStorageKey
    {
        public int OrderIndex;

        public Relation<Phase> _Phase;
        public Relation<BimDocument> _BimDocument;

        public object GetStorageKey()
            => _Phase.CombineAsStorageKey(_BimDocument);
    }

    /// <summary>
    /// Represents the category to which an Element may belong (ex: Door, Floor, Ceiling, etc...).
    /// </summary>
    [TableName(TableNames.Category)]
    public partial class Category : Entity
    {
        public string Name;

        [EntityColumnLoader("int:Id", typeof(int), SchemaVersion.History.v4_6_0)]
        public long Id;
        
        public string CategoryType;

        public double LineColor_X;
        public double LineColor_Y;
        public double LineColor_Z;
        public DVector3 LineColor
        {
            get => new DVector3(LineColor_X, LineColor_Y, LineColor_Z);
            set => (LineColor_X, LineColor_Y, LineColor_Z) = value;
        }

        /// <summary>
        /// Represents the associated built-in category in Revit.
        /// </summary>
        public string BuiltInCategory;

        public Relation<Category> _Parent;
        public Relation<Material> _Material;

        // ---------------------------------------------------------------------------------------------------------------
        // Object Styles (5.13.0): how the category or subcategory draws when no view overrides it (Manage > Object Styles).
        // LineColor above is the Object Styles line colour; Material above is the Object Styles material. Object Styles
        // has no fill pattern column: a cut or surface fill comes from the element's material (Material.CutForegroundPattern
        // ...) or from a view override (GraphicOverride).
        // ---------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Object Styles "Line Weight - Projection": a line weight number 1-16, turned into a width on paper by the Line
        /// Weights table (LineWeight). -1 when not read.
        /// </summary>
        public int ProjectionLineWeight = -1;

        /// <summary>
        /// Object Styles "Line Weight - Cut": a line weight number 1-16. 0 when the category has no cut line weight (it is
        /// not cut by a plan or section, e.g. Furniture), -1 when not read.
        /// </summary>
        public int CutLineWeight = -1;

        /// <summary>Whether the category can be cut by a plan or section view (Revit's Category.IsCuttable).</summary>
        public bool IsCuttable;

        /// <summary>Object Styles "Line Pattern" (projection lines). None when not read.</summary>
        public Relation<LinePattern> _ProjectionLinePattern;

        /// <summary>The line pattern of the cut lines (Revit keeps one per graphics style; Object Styles shows the projection one).</summary>
        public Relation<LinePattern> _CutLinePattern;
    }

    /// <summary>
    /// Represents a collection FamilyTypes, for example an 'I Beam' Family.
    /// </summary>
    [TableName(TableNames.Family)]
    [ElementKind(ElementKind.Family)]
    public partial class Family : EntityWithElement
    {
        public string StructuralMaterialType;
        public string StructuralSectionShape;
        public bool IsSystemFamily;
        public bool IsInPlace;
        public Relation<Category> _FamilyCategory;
    }

    /// <summary>
    /// Represents the template by which a FamilyInstance is created.
    /// For example, a FamilyType of 'W14x32' is defined within the 'I Beam' Family.
    /// In the Revit API, the FamilyType closely correlates to the FamilySymbol class.
    /// </summary>
    [TableName(TableNames.FamilyType)]
    [ElementKind(ElementKind.FamilyType)]
    public partial class FamilyType : EntityWithElement
    {
        public bool IsSystemFamilyType;
        public Relation<Family> _Family;
        public Relation<CompoundStructure> _CompoundStructure;
    }

    /// <summary>
    /// A FamilyInstance represents a physical instance of a FamilyType.
    /// For example, a FamilyInstance of the FamilyType 'W14x32' (which itself is defined in the 'I Beam' Family)
    /// may have a length of 12 feet, whereas another FamilyInstance may have a different length of 8 feet.
    /// </summary>
    [TableName(TableNames.FamilyInstance)]
    [CascadeElementRemap]
    [ElementKind(ElementKind.FamilyInstance)]
    public partial class FamilyInstance : EntityWithElement
    {
        public bool FacingFlipped;

        public float FacingOrientation_X;
        public float FacingOrientation_Y;
        public float FacingOrientation_Z;
        public Vector3 FacingOrientation
        {
            get => new Vector3(FacingOrientation_X, FacingOrientation_Y, FacingOrientation_Z);
            set => (FacingOrientation_X, FacingOrientation_Y, FacingOrientation_Z) = value;
        }

        public bool HandFlipped;
        public bool Mirrored;
        public bool HasModifiedGeometry;
        public float Scale;

        public float BasisX_X;
        public float BasisX_Y;
        public float BasisX_Z;
        public Vector3 BasisX
        {
            get => new Vector3(BasisX_X, BasisX_Y, BasisX_Z);
            set => (BasisX_X, BasisX_Y, BasisX_Z) = value;
        }

        public float BasisY_X;
        public float BasisY_Y;
        public float BasisY_Z;
        public Vector3 BasisY
        {
            get => new Vector3(BasisY_X, BasisY_Y, BasisY_Z);
            set => (BasisY_X, BasisY_Y, BasisY_Z) = value;
        }

        public float BasisZ_X;
        public float BasisZ_Y;
        public float BasisZ_Z;
        public Vector3 BasisZ
        {
            get => new Vector3(BasisZ_X, BasisZ_Y, BasisZ_Z);
            set => (BasisZ_X, BasisZ_Y, BasisZ_Z) = value;
        }


        public float Translation_X;
        public float Translation_Y;
        public float Translation_Z;
        public Vector3 Translation
        {
            get => new Vector3(Translation_X, Translation_Y, Translation_Z);
            set => (Translation_X, Translation_Y, Translation_Z) = value;
        }

        public float HandOrientation_X;
        public float HandOrientation_Y;
        public float HandOrientation_Z;
        public Vector3 HandOrientation
        {
            get => new Vector3(HandOrientation_X, HandOrientation_Y, HandOrientation_Z);
            set => (HandOrientation_X, HandOrientation_Y, HandOrientation_Z) = value;
        }

        public Relation<FamilyType> _FamilyType;
        public Relation<Element> _Host;
        public Relation<Room> _FromRoom;
        public Relation<Room> _ToRoom;
        public Relation<Element> _SuperComponent;
    }

    /// <summary>
    /// Represents a 3D or a 2D view.
    /// </summary>
    [TableName(TableNames.View)]
    [ElementKind(ElementKind.View)]
    public partial class View : EntityWithElement
    {
        public string Title;
        public string ViewType;

        public double Up_X;
        public double Up_Y;
        public double Up_Z;
        public DVector3 Up
        {
            get => new DVector3(Up_X, Up_Y, Up_Z);
            set => (Up_X, Up_Y, Up_Z) = value;
        }

        public double Right_X;
        public double Right_Y;
        public double Right_Z;
        public DVector3 Right
        {
            get => new DVector3(Right_X, Right_Y, Right_Z);
            set => (Right_X, Right_Y, Right_Z) = value;
        }

        public double Origin_X;
        public double Origin_Y;
        public double Origin_Z;
        public DVector3 Origin
        {
            get => new DVector3(Origin_X, Origin_Y, Origin_Z);
            set => (Origin_X, Origin_Y, Origin_Z) = value;
        }

        public double ViewDirection_X;
        public double ViewDirection_Y;
        public double ViewDirection_Z;
        public DVector3 ViewDirection
        {
            get => new DVector3(ViewDirection_X, ViewDirection_Y, ViewDirection_Z);
            set => (ViewDirection_X, ViewDirection_Y, ViewDirection_Z) = value;
        }

        public double ViewPosition_X;
        public double ViewPosition_Y;
        public double ViewPosition_Z;
        public DVector3 ViewPosition
        {
            get => new DVector3(ViewPosition_X, ViewPosition_Y, ViewPosition_Z);
            set => (ViewPosition_X, ViewPosition_Y, ViewPosition_Z) = value;
        }

        public double Scale;

        public double Outline_Min_X;
        public double Outline_Min_Y;
        public double Outline_Max_X;
        public double Outline_Max_Y;
        public DAABox2D Outline
        {
            get => new DAABox2D(new DVector2(Outline_Min_X, Outline_Min_Y), new DVector2(Outline_Max_X, Outline_Max_Y));
            set
            {
                (Outline_Min_X, Outline_Min_Y) = value.Min;
                (Outline_Max_X, Outline_Max_Y) = value.Max;
            }
        }

        public int DetailLevel; // 0 = Undefined, 1 = Coarse, 2 = Medium, 3 = Fine
        public Relation<Camera> _Camera;

        /// <summary>
        /// The View's associated FamilyType. In Revit, this maps to its ViewFamilyType
        /// </summary>
        public Relation<FamilyType> _FamilyType;

        // ---------------------------------------------------------------------------------------------------------------
        // View audit (5.12.0). Written for every view (sheets included) when the exporter's "All View Info" is on; then
        // IsAudited is true. A count which could not be read is -1, never 0. Where the view sits on sheets is in
        // ViewInViewSheet, which filters it applies in FilterInView, who last changed it in its Element row.
        // ---------------------------------------------------------------------------------------------------------------

        /// <summary>True when the view audit fields below were read from the authoring tool.</summary>
        public bool IsAudited;

        /// <summary>The view template applied to the view (the template's Element row), or none.</summary>
        public Relation<Element> _ViewTemplate;

        /// <summary>For a dependent view, its primary (parent) view.</summary>
        public Relation<View> _PrimaryView;

        /// <summary>The phase the view shows.</summary>
        public Relation<Phase> _Phase;

        /// <summary>The phase filter applied to the view.</summary>
        public Relation<PhaseFilter> _PhaseFilter;

        /// <summary>The scope box which sets the view's crop, or none.</summary>
        public Relation<Element> _ScopeBox;

        /// <summary>The view discipline (Architectural, Structural, Mechanical, Electrical, Plumbing, Coordination), or empty when the view has none.</summary>
        public string Discipline;

        /// <summary>The folders of the project browser in which the view is listed, outermost first, separated by " / ".</summary>
        public string ProjectBrowserPath;

        /// <summary>Whether the view is cropped.</summary>
        public bool IsCropBoxActive;

        /// <summary>Whether a temporary hide / isolate is active in the view (not saved as a permanent setting).</summary>
        public bool IsTemporaryHideIsolateActive;

        /// <summary>Categories and subcategories with a graphic override in the view (Visibility/Graphics). -1 when not read.</summary>
        public int CategoryOverrideCount = -1;

        /// <summary>Categories and subcategories switched off in the view (Visibility/Graphics). -1 when not read.</summary>
        public int HiddenCategoryCount = -1;

        /// <summary>View filters applied to the view (see FilterInView). -1 when not read.</summary>
        public int FilterCount = -1;

        /// <summary>Elements with a graphic override of their own in the view (Override Graphics in View > By Element); an element a rule
        /// of the view already hides is not counted. -1 when not read (an optional, slow count).</summary>
        public int ElementOverrideCount = -1;

        /// <summary>Elements hidden one by one in the view (Hide in View > Elements), a permanent setting; an element a rule of the view
        /// already hides (category off, workset hidden, hiding filter) is not counted. -1 when not read (an optional, slow count).</summary>
        public int HiddenElementCount = -1;

        /// <summary>
        /// The settings the view template locks in this view, as Revit names them in the template's properties, separated by
        /// " | " (e.g. "V/G Overrides Model | Phase Filter | Detail Level"); empty without a template. Where the template
        /// controls a setting, the audit fields above hold the template's value: that is what Revit draws.
        /// </summary>
        public string TemplateControlledSettings;

        /// <summary>
        /// True when the view's Visibility/Graphics overrides were read (5.13.0): then a category, filter or element of the
        /// view without a CategoryInView / FilterInView override / ElementOverrideInView row has none, and draws by Object
        /// Styles. Where the view template controls Visibility/Graphics or filters, the template's settings were read.
        /// </summary>
        public bool GraphicsRead;
    }

    /// <summary>
    /// A view filter applied to a View, in the order the view applies them.
    /// </summary>
    [TableName(TableNames.FilterInView)]
    public partial class FilterInView : Entity, IStorageKey
    {
        /// <summary>The filter's name.</summary>
        public string Name;

        /// <summary>"Rule" for a rule-based filter, "Selection" for a selection filter.</summary>
        public string Kind;

        /// <summary>The filter's UniqueId in the authoring tool.</summary>
        public string FilterUniqueId;

        /// <summary>The filter's position in the view's list (0 = first).</summary>
        public int OrderIndex;

        /// <summary>Whether the filter is enabled in the view (always true before Revit 2021, which cannot disable a filter).</summary>
        public bool IsEnabled;

        /// <summary>Whether the elements the filter matches are visible in the view (false = the filter hides them).</summary>
        public bool IsVisible;

        /// <summary>Whether the filter changes how its elements look (colour, line weight, pattern, halftone, transparency...).</summary>
        public bool HasOverrides;

        public Relation<View> _View;

        /// <summary>What the filter overrides in the view (5.13.0); none when HasOverrides is false.</summary>
        public Relation<GraphicOverride> _GraphicOverride;

        public object GetStorageKey()
            => $"{_View?.Index ?? -1}|{FilterUniqueId}";
    }

    /// <summary>
    /// A Grid which can show in a View (in its range, not hidden there), with the ends at which the view draws its bubbles.
    /// End 0 is the start point of the grid line, end 1 its end point.
    /// </summary>
    [TableName(TableNames.GridInView)]
    public partial class GridInView : Entity, IStorageKey
    {
        public bool BubbleAtEnd0;
        public bool BubbleAtEnd1;

        public Relation<Grid> _Grid;
        public Relation<View> _View;

        public object GetStorageKey()
            => _Grid.CombineAsStorageKey(_View);
    }

    // -------------------------------------------------------------------------------------------------------------------
    // Section graphics (5.13.0). What Revit uses to draw a plan or a section: the Line Weights table, line and fill
    // patterns, the view overrides. Resolution order for a line or a fill of an element in a view (what Revit draws):
    // ElementOverrideInView, then the view's filters (FilterInView, in OrderIndex order, first one that sets a field wins),
    // then CategoryInView (subcategory before its parent category), then Object Styles (Category), then the material
    // (Material graphics) for fills. A line weight number becomes a width with LineWeight (Table "Model", the view's Scale).
    // -------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// A line pattern (Manage > Additional Settings > Line Patterns), or Revit's built-in "Solid".
    /// </summary>
    [TableName(TableNames.LinePattern)]
    public partial class LinePattern : Entity, IStorageKey
    {
        /// <summary>The pattern's name ("Solid" for the built-in solid pattern).</summary>
        public string Name;

        /// <summary>The pattern's element id in its document (Revit's solid pattern id for "Solid").</summary>
        public long ElementId;

        /// <summary>The pattern's UniqueId in its document; "solid" for the built-in solid pattern.</summary>
        public string UniqueId;

        /// <summary>True for the built-in solid pattern (a continuous line, Segments empty).</summary>
        public bool IsSolid;

        /// <summary>
        /// The repeating segments, in order, separated by ";": "Dash 0.0104167", "Space 0.0052083", "Dot". Lengths are in
        /// feet ON PAPER (multiply by 304.8 for millimetres on paper; Revit draws them the same length at every view scale).
        /// </summary>
        public string Segments;

        public Relation<BimDocument> _BimDocument;

        public object GetStorageKey()
            => $"{_BimDocument?.Index ?? -1}|{UniqueId}";
    }

    /// <summary>
    /// A fill pattern (Manage > Additional Settings > Fill Patterns): Drafting (fixed on paper) or Model (fixed on the model).
    /// Its hatch lines are in FillGrid.
    /// </summary>
    [TableName(TableNames.FillPattern)]
    public partial class FillPattern : Entity, IStorageKey
    {
        public string Name;

        /// <summary>The pattern's element id in its document.</summary>
        public long ElementId;

        /// <summary>The pattern's UniqueId in its document.</summary>
        public string UniqueId;

        /// <summary>"Drafting" (lengths on paper, same look at every scale) or "Model" (lengths on the model, scale with the view).</summary>
        public string Target;

        /// <summary>True for a solid fill ("Solid fill": no FillGrid rows, the area is filled with the colour).</summary>
        public bool IsSolidFill;

        /// <summary>Revit's FillPatternHostOrientation: "ToView", "AlignWithElement" or "KeepReadable" (drafting patterns).</summary>
        public string HostOrientation;

        /// <summary>How many FillGrid rows (hatch line families) the pattern has.</summary>
        public int GridCount;

        public Relation<BimDocument> _BimDocument;

        public object GetStorageKey()
            => $"{_BimDocument?.Index ?? -1}|{UniqueId}";
    }

    /// <summary>
    /// One family of parallel hatch lines of a FillPattern (Revit's FillGrid). Lengths are in feet: on paper for a Drafting
    /// pattern, on the model for a Model pattern. Angle in radians, counter-clockwise from the pattern's X axis.
    /// </summary>
    [TableName(TableNames.FillGrid)]
    public partial class FillGrid : Entity, IStorageKey
    {
        /// <summary>The grid's position in the pattern (0 = first).</summary>
        public int OrderIndex;

        /// <summary>The direction of the hatch lines, radians.</summary>
        public double Angle;

        /// <summary>A point every line of the family passes through (lines repeat from it every Offset).</summary>
        public double Origin_X;
        public double Origin_Y;
        public DVector2 Origin
        {
            get => new DVector2(Origin_X, Origin_Y);
            set => (Origin_X, Origin_Y) = value;
        }

        /// <summary>The distance between two neighbouring lines of the family.</summary>
        public double Offset;

        /// <summary>How far each next line's dash pattern slides along the line.</summary>
        public double Shift;

        /// <summary>The dash / space lengths along a line, separated by " " (dash, space, dash, space...); empty = continuous.</summary>
        public string Segments;

        public Relation<FillPattern> _FillPattern;

        public object GetStorageKey()
            => $"{_FillPattern?.Index ?? -1}|{OrderIndex}";
    }

    /// <summary>
    /// The Line Weights table (Manage > Additional Settings > Line Weights): the width on paper of each line weight
    /// number 1-16. The Model table has one column per view scale; the Annotation and Perspective tables have none (Scale 0).
    /// The Revit API cannot read this table: the exporter measures it by drawing one line per number in throw-away views
    /// at each scale and reading back the width Revit's own 2D export gives (then undoes everything). Revit uses the column
    /// of the view's scale; the export measures the exported views' own scales plus Revit's usual metric and imperial ones.
    /// </summary>
    [TableName(TableNames.LineWeight)]
    public partial class LineWeight : Entity, IStorageKey
    {
        /// <summary>"Model", "Annotation" or "Perspective".</summary>
        public string Table;

        /// <summary>The view scale this width applies to: 100 means 1:100 (1" = 8'-0" is 96). 0 for the Annotation and Perspective tables.</summary>
        public int Scale;

        /// <summary>The line weight number, 1-16 (Object Styles and overrides hold this number).</summary>
        public int Number;

        /// <summary>The width on paper, in millimetres.</summary>
        public double Width;

        public Relation<BimDocument> _BimDocument;

        public object GetStorageKey()
            => $"{_BimDocument?.Index ?? -1}|{Table}|{Scale}|{Number}";
    }

    /// <summary>
    /// A set of graphic overrides (Revit's OverrideGraphicSettings) as set in Visibility/Graphics, by a view filter or by
    /// Override Graphics in View > By Element. Rows are shared: identical overrides are one row.
    /// A line weight of -1, a pattern of none or a "Has...Color" of false means the field is not overridden (the next rule
    /// applies). A pattern colour or line colour is in the domain [0..1].
    /// </summary>
    [TableName(TableNames.GraphicOverride)]
    public partial class GraphicOverride : Entity, IStorageKey
    {
        /// <summary>Projection/Surface > Lines > Weight (1-16), -1 = not overridden.</summary>
        public int ProjectionLineWeight = -1;
        public bool HasProjectionLineColor;
        public double ProjectionLineColor_X;
        public double ProjectionLineColor_Y;
        public double ProjectionLineColor_Z;
        public DVector3 ProjectionLineColor
        {
            get => new DVector3(ProjectionLineColor_X, ProjectionLineColor_Y, ProjectionLineColor_Z);
            set => (ProjectionLineColor_X, ProjectionLineColor_Y, ProjectionLineColor_Z) = value;
        }
        public Relation<LinePattern> _ProjectionLinePattern;

        /// <summary>Cut > Lines > Weight (1-16), -1 = not overridden.</summary>
        public int CutLineWeight = -1;
        public bool HasCutLineColor;
        public double CutLineColor_X;
        public double CutLineColor_Y;
        public double CutLineColor_Z;
        public DVector3 CutLineColor
        {
            get => new DVector3(CutLineColor_X, CutLineColor_Y, CutLineColor_Z);
            set => (CutLineColor_X, CutLineColor_Y, CutLineColor_Z) = value;
        }
        public Relation<LinePattern> _CutLinePattern;

        /// <summary>Projection/Surface > Patterns > Foreground: visible (false = switched off), pattern, colour.</summary>
        public bool SurfaceForegroundPatternVisible = true;
        public Relation<FillPattern> _SurfaceForegroundPattern;
        public bool HasSurfaceForegroundPatternColor;
        public double SurfaceForegroundPatternColor_X;
        public double SurfaceForegroundPatternColor_Y;
        public double SurfaceForegroundPatternColor_Z;
        public DVector3 SurfaceForegroundPatternColor
        {
            get => new DVector3(SurfaceForegroundPatternColor_X, SurfaceForegroundPatternColor_Y, SurfaceForegroundPatternColor_Z);
            set => (SurfaceForegroundPatternColor_X, SurfaceForegroundPatternColor_Y, SurfaceForegroundPatternColor_Z) = value;
        }

        /// <summary>Projection/Surface > Patterns > Background.</summary>
        public bool SurfaceBackgroundPatternVisible = true;
        public Relation<FillPattern> _SurfaceBackgroundPattern;
        public bool HasSurfaceBackgroundPatternColor;
        public double SurfaceBackgroundPatternColor_X;
        public double SurfaceBackgroundPatternColor_Y;
        public double SurfaceBackgroundPatternColor_Z;
        public DVector3 SurfaceBackgroundPatternColor
        {
            get => new DVector3(SurfaceBackgroundPatternColor_X, SurfaceBackgroundPatternColor_Y, SurfaceBackgroundPatternColor_Z);
            set => (SurfaceBackgroundPatternColor_X, SurfaceBackgroundPatternColor_Y, SurfaceBackgroundPatternColor_Z) = value;
        }

        /// <summary>Cut > Patterns > Foreground.</summary>
        public bool CutForegroundPatternVisible = true;
        public Relation<FillPattern> _CutForegroundPattern;
        public bool HasCutForegroundPatternColor;
        public double CutForegroundPatternColor_X;
        public double CutForegroundPatternColor_Y;
        public double CutForegroundPatternColor_Z;
        public DVector3 CutForegroundPatternColor
        {
            get => new DVector3(CutForegroundPatternColor_X, CutForegroundPatternColor_Y, CutForegroundPatternColor_Z);
            set => (CutForegroundPatternColor_X, CutForegroundPatternColor_Y, CutForegroundPatternColor_Z) = value;
        }

        /// <summary>Cut > Patterns > Background.</summary>
        public bool CutBackgroundPatternVisible = true;
        public Relation<FillPattern> _CutBackgroundPattern;
        public bool HasCutBackgroundPatternColor;
        public double CutBackgroundPatternColor_X;
        public double CutBackgroundPatternColor_Y;
        public double CutBackgroundPatternColor_Z;
        public DVector3 CutBackgroundPatternColor
        {
            get => new DVector3(CutBackgroundPatternColor_X, CutBackgroundPatternColor_Y, CutBackgroundPatternColor_Z);
            set => (CutBackgroundPatternColor_X, CutBackgroundPatternColor_Y, CutBackgroundPatternColor_Z) = value;
        }

        /// <summary>Surface Transparency, 0-100 (0 = opaque / not overridden).</summary>
        public int Transparency;

        /// <summary>Halftone.</summary>
        public bool Halftone;

        /// <summary>Detail Level: 0 = By View (not overridden), 1 = Coarse, 2 = Medium, 3 = Fine.</summary>
        public int DetailLevel;

        /// <summary>A text key of every value above (identical overrides share one row).</summary>
        public string Key;

        public object GetStorageKey()
            => Key;
    }

    /// <summary>
    /// A category or subcategory a View switches off or overrides (Visibility/Graphics, Model and Annotation tabs). Only
    /// categories with a change have a row; the others draw by Object Styles. When the view template controls
    /// Visibility/Graphics, the template's settings are written (that is what Revit draws).
    /// </summary>
    [TableName(TableNames.CategoryInView)]
    public partial class CategoryInView : Entity, IStorageKey
    {
        /// <summary>The Visibility checkbox is off: the category (and its subcategories) does not draw in the view.</summary>
        public bool IsHidden;

        public Relation<View> _View;
        public Relation<Category> _Category;

        /// <summary>The overrides; none when the category is only switched off.</summary>
        public Relation<GraphicOverride> _GraphicOverride;

        public object GetStorageKey()
            => _View.CombineAsStorageKey(_Category);
    }

    /// <summary>
    /// An element overridden (Override Graphics in View > By Element) or hidden (Hide in View > Elements) one by one in a
    /// View. Read for the exported views.
    /// </summary>
    [TableName(TableNames.ElementOverrideInView)]
    [CascadeElementRemap]
    public partial class ElementOverrideInView : EntityWithElement, IStorageKey
    {
        /// <summary>Hidden in the view one by one (a permanent Hide in View, not a temporary hide).</summary>
        public bool IsHidden;

        public Relation<View> _View;

        /// <summary>The overrides; none when the element is only hidden.</summary>
        public Relation<GraphicOverride> _GraphicOverride;

        public object GetStorageKey()
            => _Element.CombineAsStorageKey(_View);
    }

    /// <summary>
    /// Phasing > Graphic Overrides: how an element of each phase status draws where the phase filter says "Overridden".
    /// The Revit API cannot read this tab: the exporter measures it with four throw-away walls (one per status) drawn
    /// in a throw-away plan (cut lines) and a throw-away section in front of them (projection lines), then undoes
    /// everything. Width is millimetres on paper at Scale; the line weight number is the one whose width at that scale is
    /// the measured width (-1 when none matches). The fill patterns of this tab are not measured.
    /// </summary>
    [TableName(TableNames.PhaseGraphicOverride)]
    public partial class PhaseGraphicOverride : Entity, IStorageKey
    {
        /// <summary>"New", "Existing", "Demolished" or "Temporary".</summary>
        public string Status;

        /// <summary>The view scale the lines were measured at.</summary>
        public int Scale;

        /// <summary>Whether projection lines were measured (the throw-away section drew the walls).</summary>
        public bool HasProjectionLine;
        public double ProjectionLineColor_X;
        public double ProjectionLineColor_Y;
        public double ProjectionLineColor_Z;
        public DVector3 ProjectionLineColor
        {
            get => new DVector3(ProjectionLineColor_X, ProjectionLineColor_Y, ProjectionLineColor_Z);
            set => (ProjectionLineColor_X, ProjectionLineColor_Y, ProjectionLineColor_Z) = value;
        }
        public double ProjectionLineWidth;
        public int ProjectionLineWeight = -1;
        public Relation<LinePattern> _ProjectionLinePattern;

        /// <summary>Whether cut lines were measured (the throw-away plan cut the walls).</summary>
        public bool HasCutLine;
        public double CutLineColor_X;
        public double CutLineColor_Y;
        public double CutLineColor_Z;
        public DVector3 CutLineColor
        {
            get => new DVector3(CutLineColor_X, CutLineColor_Y, CutLineColor_Z);
            set => (CutLineColor_X, CutLineColor_Y, CutLineColor_Z) = value;
        }
        public double CutLineWidth;
        public int CutLineWeight = -1;
        public Relation<LinePattern> _CutLinePattern;

        public Relation<BimDocument> _BimDocument;

        public object GetStorageKey()
            => $"{_BimDocument?.Index ?? -1}|{Status}";
    }

    /// <summary>
    /// An associative table binding an Element to a View.
    /// </summary>
    [TableName(TableNames.ElementInView)]
    [CascadeElementRemap]
    public partial class ElementInView : EntityWithElement, IStorageKey
    {
        public Relation<View> _View;

        public object GetStorageKey()
            => _Element.CombineAsStorageKey(_View);
    }

    /// <summary>
    /// An associative table binding a Shape to a View.
    /// </summary>
    [TableName(TableNames.ShapeInView)]
    public partial class ShapeInView : Entity, IStorageKey
    {
        public Relation<Shape> _Shape;
        public Relation<View> _View;

        public object GetStorageKey()
            => _Shape.CombineAsStorageKey(_View);
    }

    /// <summary>
    /// An associative table binding an Asset to a View.
    /// </summary>
    [TableName(TableNames.AssetInView)]
    public partial class AssetInView : Entity, IStorageKey
    {
        public Relation<Asset> _Asset;
        public Relation<View> _View;

        public object GetStorageKey()
            => _Asset.CombineAsStorageKey(_View);
    }

    /// <summary>
    /// An associative table binding an Asset to a ViewSheet.
    /// </summary>
    [TableName(TableNames.AssetInViewSheet)]
    public partial class AssetInViewSheet : Entity, IStorageKey
    {
        public Relation<Asset> _Asset;
        public Relation<ViewSheet> _ViewSheet;

        public object GetStorageKey()
            => _Asset.CombineAsStorageKey(_ViewSheet);
    }

    /// <summary>
    /// An associative table binding a Level to a View.
    /// </summary>
    [TableName(TableNames.LevelInView)]
    public partial class LevelInView : Entity, IStorageKey
    {
        /// <summary>
        /// The graphical extents of the level line, including the circular bubble
        /// </summary>
        public double Extents_Min_X;
        public double Extents_Min_Y;
        public double Extents_Min_Z;
        public double Extents_Max_X;
        public double Extents_Max_Y;
        public double Extents_Max_Z;

        public DAABox Extents
        {
            get => new DAABox(new DVector3(Extents_Min_X, Extents_Min_Y, Extents_Min_Z), new DVector3(Extents_Max_X, Extents_Max_Y, Extents_Max_Z));
            set
            {
                (Extents_Min_X, Extents_Min_Y, Extents_Min_Z) = value.Min;
                (Extents_Max_X, Extents_Max_Y, Extents_Max_Z) = value.Max;
            }
        }

        public Relation<Level> _Level;
        public Relation<View> _View;

        public object GetStorageKey()
            => _Level.CombineAsStorageKey(_View);
    }

    /// <summary>
    /// Represents the orthographic or perspective camera of a 3D view.
    /// </summary>
    [TableName(TableNames.Camera)]
    public partial class Camera : Entity
    {
        public int Id;
        /// <summary>Identifies whether the projection is orthographic 0 or perspective 1</summary>
        public int IsPerspective;

        /// <summary>Distance between top and bottom planes on the target plane.</summary>
        public double VerticalExtent;

        /// <summary>Distance between left and right planes on the target plane.</summary>
        public double HorizontalExtent;

        /// <summary>
        ///    Distance from eye point to far plane of view frustum along the view direction.
        ///    This property together with NearDistance determines the depth restrictions of a view frustum.
        /// </summary>
        public double FarDistance;

        /// <summary>
        ///    Distance from eye point to near plane of view frustum along the view direction.
        ///    This property together with FarDistance determines the depth restrictions of a view frustum.
        /// </summary>
        public double NearDistance;

        /// <summary>Distance from eye point along view direction to target plane.
        ///    This value is appropriate for perspective views only.
        ///    Attempts to get this value for an orthographic view can
        ///    be made, but the obtained value is to be ignored.
        /// </summary>
        public double TargetDistance;

        /// <summary>
        ///    Distance that the target plane is offset towards the right
        ///    where right is normal to both Up direction and View direction.
        ///    This offset shifts both left and right planes.
        /// </summary>
        public double RightOffset;

        /// <summary>
        ///    Distance that the target plane is offset in the direction of
        ///    the Up direction. This offset shifts both top and bottom planes.
        /// </summary>
        public double UpOffset;
    }

    /// <summary>
    /// Represents a colored and textured material.
    /// </summary>
    [TableName(TableNames.Material)]
    [G3dAttributeReference("g3d:material:color:0:float32:4", G3dAttributeReferenceMultiplicity.OneToOne)]
    [G3dAttributeReference("g3d:material:color:0:float32:4", G3dAttributeReferenceMultiplicity.OneToOne)]
    [G3dAttributeReference("g3d:material:glossiness:0:float32:1", G3dAttributeReferenceMultiplicity.OneToOne)]
    [G3dAttributeReference("g3d:material:smoothness:0:float32:1", G3dAttributeReferenceMultiplicity.OneToOne)]
    [ElementKind(ElementKind.Material)]
    public partial class Material : EntityWithElement
    {
        /// <summary>
        /// Material name
        /// </summary>
        public string Name;

        /// <summary>
        /// The type of the category.
        /// </summary>
        public string MaterialCategory;


        /// <summary>
        /// The diffuse (albedo) color.
        /// </summary>
        public double Color_X;
        public double Color_Y;
        public double Color_Z;
        public DVector3 Color
        {
            get => new DVector3(Color_X, Color_Y, Color_Z);
            set => (Color_X, Color_Y, Color_Z) = value;
        }

        /// <summary>
        /// The asset representing the diffuse (albedo) color texture.
        /// </summary>
        public Relation<Asset> _ColorTextureFile;

        /// <summary>
        /// The UV scaling factor of the diffuse (albedo) color texture.
        /// </summary>
        public double ColorUvScaling_X = 1;
        public double ColorUvScaling_Y = 1;
        public DVector2 ColorUvScaling
        {
            get => new DVector2(ColorUvScaling_X, ColorUvScaling_Y);
            set => (ColorUvScaling_X, ColorUvScaling_Y) = value;
        }

        /// <summary>
        /// The UV offset of the diffuse (albedo) color texture.
        /// </summary>
        public double ColorUvOffset_X;
        public double ColorUvOffset_Y;
        public DVector2 ColorUvOffset
        {
            get => new DVector2(ColorUvOffset_X, ColorUvOffset_Y);
            set => (ColorUvOffset_X, ColorUvOffset_Y) = value;
        }

        /// <summary>
        /// The asset representing the normal (bump) texture.
        /// </summary>
        public Relation<Asset> _NormalTextureFile;

        /// <summary>
        /// The UV scaling factor of the normal (bump) texture.
        /// </summary>
        public double NormalUvScaling_X = 1;
        public double NormalUvScaling_Y = 1;
        public DVector2 NormalUvScaling
        {
            get => new DVector2(NormalUvScaling_X, NormalUvScaling_Y);
            set => (NormalUvScaling_X, NormalUvScaling_Y) = value;
        }

        /// <summary>
        /// The UV offset of the normal (bump) texture.
        /// </summary>
        public double NormalUvOffset_X;
        public double NormalUvOffset_Y;
        public DVector2 NormalUvOffset
        {
            get => new DVector2(NormalUvOffset_X, NormalUvOffset_Y);
            set => (NormalUvOffset_X, NormalUvOffset_Y) = value;
        }

        /// <summary>The magnitude of the normal texture effect.</summary>
        public double NormalAmount;

        /// <summary>The glossiness, defined in the domain [0..1].</summary>
        public double Glossiness;

        /// <summary>The smoothness, defined in the domain [0..1]</summary>
        public double Smoothness;

        /// <summary>The transparency, defined in the domain [0..1]</summary>
        public double Transparency;

        // ---------------------------------------------------------------------------------------------------------------
        // Material graphics (5.13.0): Revit's Material Browser > Graphics tab - Shading, Surface Patterns and Cut Patterns.
        // A pattern colour is in the domain [0..1] like Color. A pattern of none means the material draws no such pattern.
        // ---------------------------------------------------------------------------------------------------------------

        /// <summary>True when the graphics fields below were read from the authoring tool.</summary>
        public bool GraphicsRead;

        /// <summary>Graphics > Shading > Color (Revit's Material.Color). Color above may hold the render appearance's colour instead.</summary>
        public double ShadingColor_X;
        public double ShadingColor_Y;
        public double ShadingColor_Z;
        public DVector3 ShadingColor
        {
            get => new DVector3(ShadingColor_X, ShadingColor_Y, ShadingColor_Z);
            set => (ShadingColor_X, ShadingColor_Y, ShadingColor_Z) = value;
        }

        /// <summary>Graphics > Shading > Use Render Appearance.</summary>
        public bool UseRenderAppearanceForShading;

        /// <summary>Graphics > Surface Patterns > Foreground: pattern and colour.</summary>
        public Relation<FillPattern> _SurfaceForegroundPattern;
        public double SurfaceForegroundPatternColor_X;
        public double SurfaceForegroundPatternColor_Y;
        public double SurfaceForegroundPatternColor_Z;
        public DVector3 SurfaceForegroundPatternColor
        {
            get => new DVector3(SurfaceForegroundPatternColor_X, SurfaceForegroundPatternColor_Y, SurfaceForegroundPatternColor_Z);
            set => (SurfaceForegroundPatternColor_X, SurfaceForegroundPatternColor_Y, SurfaceForegroundPatternColor_Z) = value;
        }

        /// <summary>Graphics > Surface Patterns > Background: pattern and colour.</summary>
        public Relation<FillPattern> _SurfaceBackgroundPattern;
        public double SurfaceBackgroundPatternColor_X;
        public double SurfaceBackgroundPatternColor_Y;
        public double SurfaceBackgroundPatternColor_Z;
        public DVector3 SurfaceBackgroundPatternColor
        {
            get => new DVector3(SurfaceBackgroundPatternColor_X, SurfaceBackgroundPatternColor_Y, SurfaceBackgroundPatternColor_Z);
            set => (SurfaceBackgroundPatternColor_X, SurfaceBackgroundPatternColor_Y, SurfaceBackgroundPatternColor_Z) = value;
        }

        /// <summary>Graphics > Cut Patterns > Foreground: pattern and colour (the cut fill of a plan or section).</summary>
        public Relation<FillPattern> _CutForegroundPattern;
        public double CutForegroundPatternColor_X;
        public double CutForegroundPatternColor_Y;
        public double CutForegroundPatternColor_Z;
        public DVector3 CutForegroundPatternColor
        {
            get => new DVector3(CutForegroundPatternColor_X, CutForegroundPatternColor_Y, CutForegroundPatternColor_Z);
            set => (CutForegroundPatternColor_X, CutForegroundPatternColor_Y, CutForegroundPatternColor_Z) = value;
        }

        /// <summary>Graphics > Cut Patterns > Background: pattern and colour (drawn under the foreground).</summary>
        public Relation<FillPattern> _CutBackgroundPattern;
        public double CutBackgroundPatternColor_X;
        public double CutBackgroundPatternColor_Y;
        public double CutBackgroundPatternColor_Z;
        public DVector3 CutBackgroundPatternColor
        {
            get => new DVector3(CutBackgroundPatternColor_X, CutBackgroundPatternColor_Y, CutBackgroundPatternColor_Z);
            set => (CutBackgroundPatternColor_X, CutBackgroundPatternColor_Y, CutBackgroundPatternColor_Z) = value;
        }
    }

    /// <summary>
    /// An associative table binding a Material to an Element.
    /// </summary>
    [TableName(TableNames.MaterialInElement)]
    [CascadeElementRemap]
    public partial class MaterialInElement : EntityWithElement, IStorageKey
    {
        public double Area;
        public double Volume;
        public bool IsPaint;

        public Relation<Material> _Material;

        public object GetStorageKey()
            => (_Material.CombineAsStorageKey(_Element), IsPaint);
    }

    /// <summary>
    /// The possible values of the MaterialFunctionAssignment member in a CompoundStructureLayer
    /// </summary>
    public static class MaterialFunctionAssignment
    {
        public const string None = nameof(None); // Revit enum value: 0
        public const string Structure = nameof(Structure); // Revit enum value: 1
        public const string Substrate = nameof(Substrate); // Revit enum value: 2
        public const string Insulation = nameof(Insulation); // Revit enum value: 3
        public const string Finish1 = nameof(Finish1); // Revit enum value: 4
        public const string Finish2 = nameof(Finish2); // Revit enum value: 5
        public const string Membrane = nameof(Membrane); // Revit enum value: 100, i.e. 0x00000064
        public const string StructuralDeck = nameof(StructuralDeck); // Revit enum value: 200, i.e. 0x000000C8
    }

    /// <summary>
    /// Represents a material layer within a CompoundStructure.
    /// </summary>
    [TableName(TableNames.CompoundStructureLayer)]
    public partial class CompoundStructureLayer : Entity
    {
        public int OrderIndex;
        public double Width;
        public string MaterialFunctionAssignment;

        public Relation<Material> _Material;
        public Relation<CompoundStructure> _CompoundStructure;
    }

    /// <summary>
    /// Represents the collection of material layers which compose walls, ceilings, floors, etc.
    /// </summary>
    [TableName(TableNames.CompoundStructure)]
    public partial class CompoundStructure : Entity
    {
        /// <summary>
        /// If the structure is not vertically compound, then this is simply the sum of all layers' widths.
        /// If the structure is vertically compound, this is the width of the rectangular grid stored in the
        /// vertically compound structure. The presence of a layer with variable width has no effect on the
        /// value returned by this method. The value returned assumes that all layers have their specified
        /// width.<br/>
        /// See: <see href="https://www.revitapidocs.com/2020/dc1a081e-8dab-565f-145d-a429098d353c.htm">Revit API Docs - CompoundStructure.GetWidth()</see>.
        /// </summary>
        public double Width;
        /// <summary>
        /// Indicates the layer whose material defines the structural properties of the type for the purposes of analysis.<br/>
        /// See: <see href="https://www.revitapidocs.com/2020/cf4d771e-6ed2-ec6a-d32d-647fb5b649b3.htm">Revit API Docs - CompoundStructure.StructuralMaterialIndex</see>.
        /// </summary>
        public Relation<CompoundStructureLayer> _StructuralLayer;
    }

    /// <summary>
    /// Represents an instance in the G3D buffer of the VIM file.
    /// The ordering and the number of Nodes matches the ordering and the number of instances in the G3D buffer.
    /// This serves to bridge the gap between the Element entities and their corresponding instance geometry.
    /// </summary>
    [TableName(TableNames.Node)]
    [G3dAttributeReference("g3d:instance:transform:0:float32:16", G3dAttributeReferenceMultiplicity.OneToOne)]
    [G3dAttributeReference("g3d:instance:parent:0:int32:1", G3dAttributeReferenceMultiplicity.OneToOne)]
    [G3dAttributeReference("g3d:instance:mesh:0:int32:1", G3dAttributeReferenceMultiplicity.OneToOne)]
    [G3dAttributeReference("g3d:instance:flags:0:uint16:1", G3dAttributeReferenceMultiplicity.OneToOne, true)]
    public partial class Node : EntityWithElement
    {
    }

    /// <summary>
    /// A geometry piece (5.14.0): one submesh of one placed geometry, i.e. the triangles of a Node's mesh that share one material,
    /// one Revit subcategory and, for walls, floors, roofs and ceilings, one compound structure layer. Written only by the
    /// exporter's experimental "geometry pieces" option (otherwise the table is empty).
    /// <para>
    /// The g3d submesh is <c>g3d:mesh:submeshoffset[g3d:instance:mesh[Node]] + SubmeshIndex</c>. SubmeshIndex is local to the
    /// node's mesh, so it stays valid when files are merged.
    /// </para>
    /// <para>
    /// A layer piece of a split host is a closed solid of that one layer, as Revit computes it after the joins (wrapping,
    /// openings): its section cap is the layer's cut shape, filled with the Material's cut pattern.
    /// </para>
    /// </summary>
    [TableName(TableNames.GeometryPiece)]
    [CascadeElementRemap]
    public partial class GeometryPiece : EntityWithElement
    {
        /// <summary>The submesh of the node's mesh, counted from the mesh's first submesh (0-based).</summary>
        public int SubmeshIndex;

        /// <summary>The placed geometry (same index as the g3d instance) whose mesh holds the piece.</summary>
        public Relation<Node> _Node;

        /// <summary>The material of the piece (the same as g3d:submesh:material).</summary>
        public Relation<Material> _Material;

        /// <summary>The Revit subcategory of the piece's faces (from the faces' GraphicsStyle). None: the element's own category.</summary>
        public Relation<Category> _Subcategory;

        /// <summary>The compound structure layer of a wall, floor, roof or ceiling (OrderIndex, Width, MaterialFunctionAssignment). None: not a layer.</summary>
        public Relation<CompoundStructureLayer> _CompoundStructureLayer;
    }

    /// <summary>
    /// Represents a mesh in the G3D buffer of the VIM file.
    /// </summary>
    [TableName(TableNames.Geometry)]
    [G3dAttributeReference("g3d:mesh:submeshoffset:0:int32:1", G3dAttributeReferenceMultiplicity.OneToOne)]
    public partial class Geometry : Entity
    {
        public float Box_Min_X;
        public float Box_Min_Y;
        public float Box_Min_Z;
        public float Box_Max_X;
        public float Box_Max_Y;
        public float Box_Max_Z;
        public AABox Box
        {
            get => new AABox(new Vector3(Box_Min_X, Box_Min_Y, Box_Min_Z), new Vector3(Box_Max_X, Box_Max_Y, Box_Max_Z));
            set
            {
                (Box_Min_X, Box_Min_Y, Box_Min_Z) = value.Min;
                (Box_Max_X, Box_Max_Y, Box_Max_Z) = value.Max;
            }
        }

        public int VertexCount;
        public int FaceCount;
    }

    /// <summary>
    /// What a Shape draws (Shape.Kind). Kinds other than Unknown are lines of the model itself, in world space and in feet,
    /// meant to be drawn in 3D as lines (never filled, never as a solid) and picked as their Element.
    /// </summary>
    public enum ShapeKind
    {
        /// <summary>Not recorded: files written before object model 5.9.0.</summary>
        Unknown = 0,
        /// <summary>A line of a 2D view's drawing (the linework of the exported view sheet sets) or a face outline; bound to its View by ShapeInView.</summary>
        ViewLinework = 1,
        /// <summary>A model curve: a model line element, or a curve inside a family instance's 3D geometry (Revit draws it in 3D).</summary>
        ModelCurve = 2,
        /// <summary>A symbolic line of a family (for example a door swing): Revit draws it only in plan views. Placed at the instance, in world space.</summary>
        SymbolicCurve = 3,
        /// <summary>An edge loop of a void: a family's void form (cutting geometry), or an opening element (wall, floor, roof or shaft opening). Never a solid.</summary>
        VoidEdge = 4,
        /// <summary>The location curve of an element (for example a beam's location line, or the curve of an element without triangles).</summary>
        LocationCurve = 5,
        /// <summary>A grid line (its full model curve).</summary>
        Grid = 6,
    }

    /// <summary>
    /// Represents a sequence of Vector3 points in world space.
    /// The ordering and number of Shapes matches the ordering and number of shapes in the G3D buffer.
    /// The points live in the G3D shape vertex buffer ("g3d:shapevertex:position:0:float32:3", feet), each Shape being the
    /// range [vertexoffset[i], vertexoffset[i + 1]) of that buffer: an open polyline, or a closed one when its last point equals its first.
    /// </summary>
    [TableName(TableNames.Shape)]
    [G3dAttributeReference("g3d:shape:vertexoffset:0:int32:1", G3dAttributeReferenceMultiplicity.OneToOne, true)]
    [G3dAttributeReference("g3d:shape:color:0:float32:4", G3dAttributeReferenceMultiplicity.OneToOne, true)]
    [G3dAttributeReference("g3d:shape:width:0:float32:1", G3dAttributeReferenceMultiplicity.OneToOne, true)]
    public partial class Shape : EntityWithElement
    {
        /// <summary>
        /// The integer representation of the ShapeKind enum: what the shape draws. 0 (Unknown) in files written before 5.9.0.
        /// </summary>
        public int Kind;

        public ShapeKind GetKind()
            => (ShapeKind) Kind;
    }

    /// <summary>
    /// Represents a collection of shapes associated with an Element.
    /// Currently, these define the shapes representing the curve loops on a face on an element;
    /// faces may have a number of curve loops which may designate the contour of the face and its holes.
    /// </summary>
    [TableName(TableNames.ShapeCollection)]
    [G3dAttributeReference("g3d:shape:vertexoffset:0:int32:1", G3dAttributeReferenceMultiplicity.OneToMany, true)]
    [G3dAttributeReference("g3d:shape:color:0:float32:4", G3dAttributeReferenceMultiplicity.OneToMany, true)]
    [G3dAttributeReference("g3d:shape:width:0:float32:1", G3dAttributeReferenceMultiplicity.OneToMany, true)]
    public partial class ShapeCollection : EntityWithElement
    {
    }

    /// <summary>
    /// An associative table binding a Shape to a ShapeCollection.
    /// </summary>
    [TableName(TableNames.ShapeInShapeCollection)]
    public partial class ShapeInShapeCollection : Entity, IStorageKey
    {
        public Relation<Shape> _Shape;
        public Relation<ShapeCollection> _ShapeCollection;

        public object GetStorageKey()
            => _Shape.CombineAsStorageKey(_ShapeCollection);
    }

    /// <summary>
    /// Designates whether a System is Mechanical, Electrical, Pipe, etc.
    /// </summary>
    public enum SystemType
    {
        Unknown = 0,
        Mechanical,
        Electrical,
        Pipe,
        Stair,
        Wall,
        StackedWall,
        CurtainWall,
        CurtainSystem,
        UnassignedMechanical,
        UnassignedElectrical,
        UnassignedPipe
    }

    public static class SystemTypeExtensions
    {
        public static string ToDisplayString(this SystemType systemType)
        {
            switch (systemType)
            {
                case SystemType.StackedWall:
                    return "Stacked Wall";
                case SystemType.CurtainWall:
                    return "Curtain Wall";
                case SystemType.CurtainSystem:
                    return "Curtain System";
                case SystemType.UnassignedMechanical:
                    return "Unassigned Mechanical";
                case SystemType.UnassignedElectrical:
                    return "Unassigned Electrical";
                case SystemType.UnassignedPipe:
                    return "Unassigned Pipe";
                default:
                    return systemType.ToString("G");
            }
        }
    }

    /// <summary>
    /// Represents a collection of Elements which compose a System. These may be mechanical systems, piping systems, electrical systems, curtain walls, stairs, etc.
    /// </summary>
    [TableName(TableNames.System)]
    [ElementKind(ElementKind.System)]
    public partial class System : EntityWithElement
    {
        /// <summary>
        /// The integer representation of the SystemType enum.
        /// </summary>
        public int SystemType;

        /// <summary>
        /// Returns the system type enum value of the system.
        /// </summary>
        public SystemType GetSystemType()
            => (SystemType) SystemType;

        /// <summary>
        /// Returns the FamilyType information of the System.
        /// </summary>
        public Relation<FamilyType> _FamilyType;
    }

    /// <summary>
    /// Designates the role(s) of the element in a system. This enum is defined as a set of bitwise flags in case the Element has more than one role in the system.
    /// </summary>
    [Flags]
    public enum ElementInSystemRole
    {
        /// <summary>
        /// No specified role
        /// </summary>
        Default = 0,
        /// <summary>
        /// The element acts as a base equipment object (for example an electrical panel object in an electrical system)
        /// </summary>
        BaseEquipment = 1,
        /// <summary>
        /// The element acts as a terminal output in the system.
        /// </summary>
        Terminal = 1 << 1,
    }

    /// <summary>
    /// An associative table binding an Element to a System.
    /// </summary>
    [TableName(TableNames.ElementInSystem)]
    [CascadeElementRemap]
    public partial class ElementInSystem : EntityWithElement, IStorageKey
    {
        /// <summary>
        /// Defines the collection of roles of the element in the system.
        /// </summary>
        public int Roles;

        /// <summary>
        /// The system
        /// </summary>
        public Relation<System> _System;

        /// <summary>
        /// The storage key; an element may only exist once in a system.
        /// </summary>
        public object GetStorageKey()
            => _Element.CombineAsStorageKey(_System);

        public bool IsRoleDefault
            => ((ElementInSystemRole)Roles == ElementInSystemRole.Default);

        public bool IsRoleBaseEquipment
            => ((ElementInSystemRole)Roles & ElementInSystemRole.BaseEquipment) == ElementInSystemRole.BaseEquipment;

        public bool IsRoleTerminal
            => ((ElementInSystemRole)Roles & ElementInSystemRole.Terminal) == ElementInSystemRole.Terminal;

        public ElementInSystemRole GetRoles()
            => (ElementInSystemRole) Roles;
    }

    /// <summary>
    /// Represents a textual Warning in a BimDocument. Warnings designate whether there are any problematic
    /// authoring issues among Elements in a BimDocument.
    /// </summary>
    [TableName(TableNames.Warning)]
    public partial class Warning : Entity
    {
        public string Guid;
        public string Severity;
        public string Description;
        public Relation<BimDocument> _BimDocument;

        /// <summary>
        /// The same warning carries the same key in every export of the model (5.12.0): the kind of warning (Guid) and a
        /// hash of the sorted UniqueIds of ALL the elements it names, whether or not they are in the file. Two exports of
        /// one model list a warning under one key as long as it names the same elements, so a warning can be followed
        /// across versions (appeared, still there, gone).
        /// </summary>
        public string Key;

        /// <summary>How many elements the warning names in the authoring tool (some may not be in the file).</summary>
        public int FailingElementCount;
    }

    /// <summary>
    /// An associative table binding an Element to a Warning.
    /// </summary>
    [TableName(TableNames.ElementInWarning)]
    [CascadeElementRemap]
    public partial class ElementInWarning : EntityWithElement, IStorageKey
    {
        public Relation<Warning> _Warning;

        public object GetStorageKey()
            => _Element.CombineAsStorageKey(_Warning);
    }

    /// <summary>
    /// Represents the project base point or the document's current survey point if IsShared is set to true.
    /// BasePoints are only exported in Revit 2021+
    /// </summary>
    [TableName(TableNames.BasePoint)]
    [ElementKind(ElementKind.BasePoint)]
    public partial class BasePoint : EntityWithElement
    {
        /// <summary>
        /// Returns true if the BasePoint is the associated BimDocument's current survey point. The associated BimDocument is stored in the Element relation.
        /// </summary>
        public bool IsSurveyPoint;

        /// <summary>
        /// The clipped state of the survey point. This is only relevant if IsSurveyPoint is true, false otherwise.
        /// </summary>
        public bool IsClipped;

        /// <summary>
        /// The NorthSouth (N/S) value in Revit.
        /// </summary>
        public double NorthSouth;

        /// <summary>
        /// The EastWest (E/W) value in Revit.
        /// </summary>
        public double EastWest;

        /// <summary>
        /// The Elevation value in Revit.
        /// </summary>
        public double Elevation;

        /// <summary>
        /// The AngleToTrueNorth value in Revit, in radians.
        /// </summary>
        public double AngleToTrueNorth;

        /// <summary>
        /// The position of the BasePoint relative to the BimDocument's internal origin.
        /// </summary>
        public double Position_X;
        public double Position_Y;
        public double Position_Z;
        public DVector3 Position
        {
            get => new DVector3(Position_X, Position_Y, Position_Z);
            set => (Position_X, Position_Y, Position_Z) = value;
        }

        /// <summary>
        /// From the RevitAPI docs (2020 onwards):<br/>
        /// Shared position of the BasePoint based on the active ProjectLocation of its belonging Document.<br/>
        /// To get the shared position under other ProjectLocations, please use ProjectLocation.GetProjectPosition(BasePoint.Position).
        /// </summary>
        public double SharedPosition_X;
        public double SharedPosition_Y;
        public double SharedPosition_Z;
        public DVector3 SharedPosition
        {
            get => new DVector3(SharedPosition_X, SharedPosition_Y, SharedPosition_Z);
            set => (SharedPosition_X, SharedPosition_Y, SharedPosition_Z) = value;
        }
    }

    /// <summary>
    /// Phase status presentation in a phase filter.
    /// See: https://www.revitapidocs.com/2023/84d5855c-fba2-b026-ee60-7f2a24b78129.htm
    /// </summary>
    public enum PhaseStatusPresentation
    {
        /// <summary>
        /// Represents the "Not Displayed" phase status presentation.
        /// </summary>
        DontShow = 0,

        /// <summary>
        /// Represents the "By Category" phase status presentation.
        /// </summary>
        ShowByCategory = 1,

        /// <summary>
        /// Represents the "Overridden" phase status presentation.
        /// </summary>
        ShowOverridden = 2,
    }

    /// <summary>
    /// Represents a row in the Phase Filters view in Revit.
    /// </summary>
    [TableName(TableNames.PhaseFilter)]
    [ElementKind(ElementKind.PhaseFilter)]
    public partial class PhaseFilter : EntityWithElement
    {
        /// <summary>
        /// Corresponds to the "New" column in the Revit Phase Filters view
        /// </summary>
        public int New;

        public PhaseStatusPresentation GetNewPhaseStatusPresentation()
            => (PhaseStatusPresentation) New;

        /// <summary>
        /// Corresponds to the "Existing" column in the Revit Phase Filters view.
        /// </summary>
        public int Existing;

        public PhaseStatusPresentation GetExistingPhaseStatusPresentation()
            => (PhaseStatusPresentation) Existing;

        /// <summary>
        /// Corresponds to the "Demolished" column in the Revit Phase Filters view.
        /// </summary>
        public int Demolished;

        public PhaseStatusPresentation GetDemolishedPhaseStatusPresentation()
            => (PhaseStatusPresentation) Demolished;

        /// <summary>
        /// Corresponds to the "Temporary" column in the Revit Phase Filters view.
        /// </summary>
        public int Temporary;

        public PhaseStatusPresentation GetTemporaryPhaseStatusPresentation()
            => (PhaseStatusPresentation) Temporary;

        public static string PhaseStatusPresentationToString(int phaseStatusPresentation)
        {
            switch (phaseStatusPresentation)
            {
                case ((int)PhaseStatusPresentation.DontShow):
                    return "Not Displayed";
                case ((int)PhaseStatusPresentation.ShowByCategory):
                    return "By Category";
                case ((int)PhaseStatusPresentation.ShowOverridden):
                    return "Overridden";
                default:
                    return "Unknown";
            }
        }
    }

    /// <summary>
    /// Represents a vertical plane (or a vertical cylindrical segment when curved).
    /// </summary>
    [TableName(TableNames.Grid)]
    [ElementKind(ElementKind.Grid)]
    public partial class Grid : EntityWithElement
    {
        /// <summary>
        /// The start point of the grid line
        /// </summary>
        public double StartPoint_X;
        public double StartPoint_Y;
        public double StartPoint_Z;
        public DVector3 StartPoint
        {
            get => new DVector3(StartPoint_X, StartPoint_Y, StartPoint_Z);
            set => (StartPoint_X, StartPoint_Y, StartPoint_Z) = value;
        }

        /// <summary>
        /// The end point of the grid line
        /// </summary>
        public double EndPoint_X;
        public double EndPoint_Y;
        public double EndPoint_Z;
        public DVector3 EndPoint
        {
            get => new DVector3(EndPoint_X, EndPoint_Y, EndPoint_Z);
            set => (EndPoint_X, EndPoint_Y, EndPoint_Z) = value;
        }

        /// <summary>
        /// Whether the grid is curved 
        /// </summary>
        public bool IsCurved;

        /// <summary>
        /// The extents of the grid as a bounding box
        /// </summary>
        public double Extents_Min_X;
        public double Extents_Min_Y;
        public double Extents_Min_Z;
        public double Extents_Max_X;
        public double Extents_Max_Y;
        public double Extents_Max_Z;

        public DAABox Extents
        {
            get => new DAABox(new DVector3(Extents_Min_X, Extents_Min_Y, Extents_Min_Z), new DVector3(Extents_Max_X, Extents_Max_Y, Extents_Max_Z));
            set
            {
                (Extents_Min_X, Extents_Min_Y, Extents_Min_Z) = value.Min;
                (Extents_Max_X, Extents_Max_Y, Extents_Max_Z) = value.Max;
            }
        }

        /// <summary>
        /// The associated FamilyType of the Grid. In Revit, this maps to the Grid's GridType.
        /// </summary>
        public Relation<FamilyType> _FamilyType;

        /// <summary>
        /// The grid type's default bubbles in plan views (5.12.0): at the start point of the grid line (end 0) and at its
        /// end point (end 1). In Revit, the type's "Plan View Symbols End 1" and "End 2".
        /// </summary>
        public bool TypeBubbleAtEnd0;
        public bool TypeBubbleAtEnd1;

        /// <summary>
        /// The bubbles as drawn in BubbleView, the lowest floor plan which shows the grid (5.12.0). Not read when
        /// BubbleView is none. The bubbles of every view which shows the grid are in GridInView.
        /// </summary>
        public bool BubbleAtEnd0;
        public bool BubbleAtEnd1;
        public Relation<View> _BubbleView;
    }

    /// <summary>
    /// Represents a planar region which can be used to represent places like parking spots.
    /// </summary>
    [TableName(TableNames.Area)]
    [ElementKind(ElementKind.Area)]
    public partial class Area : EntityWithElement
    {
        /// <summary>
        /// The area value in square feet.
        /// </summary>
        public double Value; // NOTE: We can't just name this "Area" because member names cannot be the same as their enclosing type in C#.
        
        /// <summary>
        /// The perimeter of the area.
        /// </summary>
        public double Perimeter;

        /// <summary>
        /// The number of the area.
        /// </summary>
        public string Number;

        /// <summary>
        /// Determines whether the area is a gross interior.
        /// </summary>
        public bool IsGrossInterior;

        /// <summary>
        /// The area scheme.
        /// </summary>
        public Relation<AreaScheme> _AreaScheme;
    }

    /// <summary>
    /// Represents an area categorization, for example to differentiate between parking areas and waste/dump areas.
    /// </summary>
    [TableName(TableNames.AreaScheme)]
    [ElementKind(ElementKind.AreaScheme)]
    public partial class AreaScheme : EntityWithElement
    {
        /// <summary>
        /// Determines if the area scheme is the built-in "gross building area" scheme.
        /// </summary>
        public bool IsGrossBuildingArea;
    }

    /// <summary>
    /// Represents tabular data composed of named columns and cells containing string values.
    /// </summary>
    [TableName(TableNames.Schedule)]
    [ElementKind(ElementKind.Schedule)]

    public partial class Schedule : EntityWithElement
    { }

    /// <summary>
    /// Represents a column in a Schedule.
    /// </summary>
    [TableName(TableNames.ScheduleColumn)]
    public partial class ScheduleColumn : Entity
    {
        /// <summary>
        /// The name of the column.
        /// </summary>
        public string Name;

        /// <summary>
        /// The index of the the column in the schedule.
        /// </summary>
        public int ColumnIndex;

        /// <summary>
        /// The Schedule to which the column belongs.
        /// </summary>
        public Relation<Schedule> _Schedule;
    }

    /// <summary>
    /// Represents a cell in a ScheduleColumn.
    /// </summary>
    [TableName(TableNames.ScheduleCell)]
    public partial class ScheduleCell : Entity
    {
        /// <summary>
        /// The string value of the cell.
        /// </summary>
        public string Value;

        /// <summary>
        /// The row index of the cell in the column.
        /// </summary>
        public int RowIndex;

        /// <summary>
        /// The ScheduleColumn to which the cell belongs.
        /// </summary>
        public Relation<ScheduleColumn> _ScheduleColumn;
    }

    /// <summary>
    /// Represents a view sheet set, which is a collection of views and view sheets.
    /// </summary>
    [TableName(TableNames.ViewSheetSet)]
    [ElementKind(ElementKind.ViewSheetSet)]
    public partial class ViewSheetSet : EntityWithElement
    {
    }

    /// <summary>
    /// Represents a view sheet, which can contain multiple views.
    /// </summary>
    [TableName(TableNames.ViewSheet)]
    [ElementKind(ElementKind.ViewSheet)]
    public partial class ViewSheet : EntityWithElement
    {
        /// <summary>
        /// The ViewSheet's associated FamilyType. In Revit, this maps to its ViewFamilyType
        /// </summary>
        public Relation<FamilyType> _FamilyType;
    }

    /// <summary>
    /// An associative table binding a ViewSheet to a ViewSheetSet.
    /// </summary>
    [TableName(TableNames.ViewSheetInViewSheetSet)]
    public partial class ViewSheetInViewSheetSet : Entity, IStorageKey
    {
        public Relation<ViewSheet> _ViewSheet;
        public Relation<ViewSheetSet> _ViewSheetSet;

        public object GetStorageKey()
            => _ViewSheet.CombineAsStorageKey(_ViewSheetSet);
    }

    /// <summary>
    /// An associative table binding a View to a ViewSheetSet.
    /// </summary>
    [TableName(TableNames.ViewInViewSheetSet)]
    public partial class ViewInViewSheetSet : Entity, IStorageKey
    {
        public Relation<View> _View;
        public Relation<ViewSheetSet> _ViewSheetSet;

        public object GetStorageKey()
            => _View.CombineAsStorageKey(_ViewSheetSet);
    }

    /// <summary>
    /// An associative table binding a View to a ViewSheet
    /// </summary>
    [TableName(TableNames.ViewInViewSheet)]
    public partial class ViewInViewSheet : Entity, IStorageKey
    {
        public Relation<View> _View;
        public Relation<ViewSheet> _ViewSheet;

        public object GetStorageKey()
            => _View.CombineAsStorageKey(_ViewSheet);
    }

    [TableName(TableNames.Site)]
    [ElementKind(ElementKind.Site)]
    public partial class Site : EntityWithElement
    {
        public double Latitude;
        public double Longitude;
        public string Address;
        public double Elevation;
        public string Number;
    }

    [TableName(TableNames.Building)]
    [ElementKind(ElementKind.Building)]
    public partial class Building : EntityWithElement
    {
        /// <summary>
        /// Revit: maps to Document.SiteLocation.Elevation
        /// IFC: maps to IfcBuilding.ElevationOfRefHeight
        /// </summary>
        public double Elevation;

        /// <summary>
        /// Revit: maps to Document.SiteLocation.Elevation
        /// IFC: maps to IfcBuilding.ElevationOfTerrain
        /// </summary>
        public double TerrainElevation;

        /// <summary>
        /// Revit: maps to Document.ProjectInformation.Address
        /// IFC: maps to IfcBuilding.BuildingAddress
        /// </summary>
        public string Address;

        public Relation<Site> _Site;
    }

    /// <summary>
    /// The shape of a light's emitter. Maps to Revit's LightShapeStyle (LightType.GetLightShape()).
    /// </summary>
    public enum LightShapeStyle
    {
        Unknown = 0,
        Point = 1,
        Line = 2,
        Rectangle = 3,
        Circle = 4,
    }

    /// <summary>
    /// The distribution of a light. Maps to Revit's LightDistributionStyle (LightType.GetLightDistribution()).
    /// </summary>
    public enum LightDistributionStyle
    {
        Unknown = 0,
        Spherical = 1,
        Hemispherical = 2,
        Spot = 3,
        PhotometricWeb = 4,
    }

    /// <summary>
    /// How the initial intensity of a light is defined. Maps to the subclass of Revit's InitialIntensity (LightType.GetInitialIntensity()).
    /// </summary>
    public enum InitialIntensityType
    {
        Unknown = 0,
        /// <summary>InitialLuminousIntensity: LightType.Luminosity applies.</summary>
        LuminousIntensity = 1,
        /// <summary>InitialFluxIntensity: LightType.Flux applies.</summary>
        Flux = 2,
        /// <summary>InitialWattageIntensity: LightType.Wattage and LightType.Efficacy apply.</summary>
        Wattage = 3,
        /// <summary>InitialIlluminanceIntensity: LightType.Illuminance and LightType.IlluminanceDistance apply.</summary>
        Illuminance = 4,
        /// <summary>
        /// The Revit Lighting API refused the family; the values were read from the family type's FBX_LIGHT_* built-in parameters.
        /// Any of Luminosity, Flux, Wattage, Efficacy and Illuminance may be set (0 when the parameter is absent).
        /// </summary>
        TypeParameters = 5,
    }

    /// <summary>
    /// The dimming behaviour of a light. Maps to Revit's LightDimmingColor (LightType.DimmingColor).
    /// </summary>
    public enum LightDimmingColor
    {
        Unknown = 0,
        None = 1,
        Incandescent = 2,
    }

    /// <summary>
    /// Represents the photometric definition of a lighting FamilyType: its emitter shape, its distribution, its initial
    /// intensity and its color. All the FamilyInstances of the FamilyType share it; each of them has a LightSource.<br/>
    /// Revit: maps to Autodesk.Revit.DB.Lighting.LightType (LightType.GetLightTypeFromInstance).<br/>
    /// Units: the Revit Lighting API returns SI values (cd, lm, W, lm/W, lx, K), with lengths in feet and angles in radians;
    /// they are stored as they are. A value which does not apply to the shape, the distribution or the intensity type is 0.
    /// </summary>
    [TableName(TableNames.LightType)]
    public partial class LightType : Entity
    {
        /// <summary>
        /// The integer representation of the LightShapeStyle enum (LightType.GetLightShape()).
        /// </summary>
        public int LightShapeStyle;

        public LightShapeStyle GetLightShapeStyle()
            => (LightShapeStyle) LightShapeStyle;

        /// <summary>
        /// The emit length in feet, for a Line or a Rectangle shape (LineLightShape.EmitLength, RectangleLightShape.EmitLength).
        /// </summary>
        public double EmitLength;

        /// <summary>
        /// The emit width in feet, for a Rectangle shape (RectangleLightShape.EmitWidth).
        /// </summary>
        public double EmitWidth;

        /// <summary>
        /// The emit diameter in feet, for a Circle shape (CircleLightShape.EmitDiameter).
        /// </summary>
        public double EmitDiameter;

        /// <summary>
        /// The integer representation of the LightDistributionStyle enum (LightType.GetLightDistribution()).
        /// </summary>
        public int LightDistributionStyle;

        public LightDistributionStyle GetLightDistributionStyle()
            => (LightDistributionStyle) LightDistributionStyle;

        /// <summary>
        /// The spot beam angle in radians, for a Spot distribution (SpotLightDistribution.SpotBeamAngle).
        /// </summary>
        public double SpotBeamAngle;

        /// <summary>
        /// The spot field angle in radians, for a Spot distribution (SpotLightDistribution.SpotFieldAngle).
        /// </summary>
        public double SpotFieldAngle;

        /// <summary>
        /// The tilt angle in radians, for a Spot or a PhotometricWeb distribution (SpotLightDistribution.TiltAngle, PhotometricWebLightDistribution.TiltAngle).
        /// Note: RevitAPI.xml says PhotometricWebLightDistribution.TiltAngle is in degrees; measured on Revit 2027, it returns radians.
        /// </summary>
        public double TiltAngle;

        /// <summary>
        /// The photometric web (IES) file name, as Revit names it (PhotometricWebLightDistribution.PhotometricWebFile, or the
        /// FBX_LIGHT_PHOTOMETRIC_FILE parameter). Kept even when the file itself could not be found.
        /// </summary>
        public string PhotometricWebFileName;

        /// <summary>
        /// The asset holding the photometric web file's bytes as they are (IESNA LM-63 text), or none if the file could not be
        /// found. Read from the family type's cache (the FBX_LIGHT_PHOTOMETRIC_FILE_CACHE parameter), else from the disk.
        /// Light types which use the same file share the same asset.
        /// </summary>
        public Relation<Asset> _PhotometricWebFile;

        /// <summary>
        /// The integer representation of the InitialIntensityType enum (LightType.GetInitialIntensity()).
        /// </summary>
        public int InitialIntensityType;

        public InitialIntensityType GetInitialIntensityType()
            => (InitialIntensityType) InitialIntensityType;

        /// <summary>
        /// The luminous intensity in candelas (InitialLuminousIntensity.Luminosity).
        /// </summary>
        public double Luminosity;

        /// <summary>
        /// The luminous flux in lumens (InitialFluxIntensity.Flux).
        /// </summary>
        public double Flux;

        /// <summary>
        /// The wattage in watts (InitialWattageIntensity.Wattage).
        /// </summary>
        public double Wattage;

        /// <summary>
        /// The efficacy in lumens per watt (InitialWattageIntensity.Efficacy).
        /// </summary>
        public double Efficacy;

        /// <summary>
        /// The illuminance in lux (InitialIlluminanceIntensity.Illuminance).
        /// </summary>
        public double Illuminance;

        /// <summary>
        /// The distance in feet at which the Illuminance is measured (InitialIlluminanceIntensity.Distance).
        /// </summary>
        public double IlluminanceDistance;

        /// <summary>
        /// The initial color temperature in kelvins (LightType.GetInitialColor().TemperatureValue). 0 if unknown.
        /// </summary>
        public double InitialColorTemperature;

        /// <summary>
        /// The color filter, each component in [0..1] (LightType.ColorFilter, whose bytes are divided by 255). (0, 0, 0) if unknown.
        /// </summary>
        public double ColorFilter_X;
        public double ColorFilter_Y;
        public double ColorFilter_Z;
        public DVector3 ColorFilter
        {
            get => new DVector3(ColorFilter_X, ColorFilter_Y, ColorFilter_Z);
            set => (ColorFilter_X, ColorFilter_Y, ColorFilter_Z) = value;
        }

        /// <summary>
        /// The total light loss factor, a ratio (LightType.GetLossFactor().LossFactorValue). 0 if unknown.
        /// </summary>
        public double LossFactor;

        /// <summary>
        /// The integer representation of the LightDimmingColor enum (LightType.DimmingColor).
        /// </summary>
        public int DimmingColor;

        public LightDimmingColor GetDimmingColor()
            => (LightDimmingColor) DimmingColor;

        /// <summary>
        /// The FamilyType which carries this light definition (in Revit, the FamilySymbol of the light fixtures).
        /// </summary>
        public Relation<FamilyType> _FamilyType;
    }

    /// <summary>
    /// Represents the light source of a light fixture: where it is, how it is oriented, and whether it is on.
    /// The Element relation is the fixture's Element (a FamilyInstance). Revit allows one light source per family
    /// (LightFamily.GetLightSourceTransform), hence at most one LightSource per Element.
    /// </summary>
    [TableName(TableNames.LightSource)]
    [CascadeElementRemap]
    public partial class LightSource : EntityWithElement
    {
        /// <summary>
        /// The origin of the light source, in the same world space and units (feet) as the geometry: the transform of the
        /// document holding the fixture (identity, or the link's transform) x FamilyInstance.GetTotalTransform() x
        /// LightFamily.GetLightSourceTransform() (read in the light's family document).
        /// </summary>
        public double Origin_X;
        public double Origin_Y;
        public double Origin_Z;
        public DVector3 Origin
        {
            get => new DVector3(Origin_X, Origin_Y, Origin_Z);
            set => (Origin_X, Origin_Y, Origin_Z) = value;
        }

        /// <summary>
        /// The X axis of the light source frame, in world space, as a unit vector.
        /// </summary>
        public double BasisX_X;
        public double BasisX_Y;
        public double BasisX_Z;
        public DVector3 BasisX
        {
            get => new DVector3(BasisX_X, BasisX_Y, BasisX_Z);
            set => (BasisX_X, BasisX_Y, BasisX_Z) = value;
        }

        /// <summary>
        /// The Y axis of the light source frame, in world space, as a unit vector.
        /// </summary>
        public double BasisY_X;
        public double BasisY_Y;
        public double BasisY_Z;
        public DVector3 BasisY
        {
            get => new DVector3(BasisY_X, BasisY_Y, BasisY_Z);
            set => (BasisY_X, BasisY_Y, BasisY_Z) = value;
        }

        /// <summary>
        /// The Z axis of the light source frame, in world space, as a unit vector. The light points along -BasisZ (the
        /// photometric nadir, as in IES files). A mirrored fixture gives a left-handed frame: do not assume BasisX x BasisY = BasisZ.
        /// </summary>
        public double BasisZ_X;
        public double BasisZ_Y;
        public double BasisZ_Z;
        public DVector3 BasisZ
        {
            get => new DVector3(BasisZ_X, BasisZ_Y, BasisZ_Z);
            set => (BasisZ_X, BasisZ_Y, BasisZ_Z) = value;
        }

        /// <summary>
        /// True if the frame includes the family's light source transform (LightFamily.GetLightSourceTransform).
        /// False if the family could not be read (in-place or non-editable family, or Revit refused to open it):
        /// the frame is then the fixture's own frame.
        /// </summary>
        public bool HasLightSourceTransform;

        /// <summary>
        /// Whether the light is on in the exported 3D view's light groups (LightGroupManager.IsLightOn).
        /// Lights in linked documents are always on.
        /// </summary>
        public bool IsOn;

        /// <summary>
        /// The photometric definition shared by the fixtures of the same FamilyType.
        /// </summary>
        public Relation<LightType> _LightType;
    }

    /// <summary>
    /// Represents the sun of a 3D view. Revit: maps to the View's SunAndShadowSettings.<br/>
    /// The place on Earth (latitude, longitude, time zone, elevation) is in the BimDocument, and the angle to true north in the BasePoint.
    /// </summary>
    [TableName(TableNames.SunAndShadowSettings)]
    public partial class SunAndShadowSettings : Entity
    {
        /// <summary>
        /// The name of Revit's SunAndShadowType: "StillImage", "OneDayStudy", "MultiDayStudy" or "Lighting".
        /// "Lighting" is a fixed angle which is not tied to a place or a time.
        /// </summary>
        public string SunAndShadowType;

        /// <summary>
        /// Whether daylight saving time applies (SunAndShadowSettings.UsesDST).
        /// </summary>
        public bool UsesDST;

        /// <summary>
        /// The time of the active frame, on the project's clock, as "yyyy-MM-ddTHH:mm:ss" (SunAndShadowSettings.GetFrameTime(ActiveFrame)).
        /// Empty for the Lighting type.
        /// </summary>
        public string ActiveFrameTime;

        /// <summary>
        /// The start of the study, on the project's clock, as "yyyy-MM-ddTHH:mm:ss" (SunAndShadowSettings.StartDateAndTime). Empty for the Lighting type.
        /// </summary>
        public string StartDateAndTime;

        /// <summary>
        /// The end of the study, on the project's clock, as "yyyy-MM-ddTHH:mm:ss" (SunAndShadowSettings.EndDateAndTime). Empty for the Lighting type.
        /// </summary>
        public string EndDateAndTime;

        /// <summary>
        /// The active frame of the study (SunAndShadowSettings.ActiveFrame). 0 for the Lighting type.
        /// </summary>
        public int ActiveFrame;

        /// <summary>
        /// The number of frames of the study (SunAndShadowSettings.NumberOfFrames). 0 for the Lighting type.
        /// </summary>
        public int NumberOfFrames;

        /// <summary>
        /// The altitude of the sun, in radians: the sun Revit computed for the active frame (SunAndShadowSettings.GetFrameAltitude),
        /// or the fixed altitude for the Lighting type (SunAndShadowSettings.Altitude).
        /// </summary>
        public double Altitude;

        /// <summary>
        /// The azimuth of the sun, in radians: clockwise from true north for the sun Revit computed for the active frame
        /// (SunAndShadowSettings.GetFrameAzimuth), or the fixed azimuth for the Lighting type (SunAndShadowSettings.Azimuth).
        /// </summary>
        public double Azimuth;

        /// <summary>
        /// For the Lighting type, whether the fixed angles are relative to the view rather than to the model (SunAndShadowSettings.RelativeToView).
        /// </summary>
        public bool RelativeToView;

        /// <summary>
        /// The 3D view which owns these settings.
        /// </summary>
        public Relation<View> _View;
    }

    /// <summary>
    /// What wrote an ExportRevision (ExportRevision.Kind).
    /// </summary>
    public enum ExportRevisionKind
    {
        /// <summary>Not recorded.</summary>
        Unknown = 0,
        /// <summary>A full export of the document.</summary>
        FullExport = 1,
        /// <summary>A partial (incremental) export: only the changed elements of the document.</summary>
        PartialExport = 2,
    }

    /// <summary>
    /// One export of one source document (BimDocument) which wrote element data now in this file. A file written by one
    /// export has one row per document; a file patched or merged since keeps the rows of every export whose data is still
    /// in it, and ElementInExportRevision says which export each element's data comes from.
    /// </summary>
    [TableName(TableNames.ExportRevision)]
    public partial class ExportRevision : Entity
    {
        /// <summary>The persisting id (header "id::revision") of the VIM file the export wrote.</summary>
        public string VimRevision;

        /// <summary>The integer representation of the ExportRevisionKind enum.</summary>
        public int Kind;

        /// <summary>The generator of that VIM file (header "generator"), for example "Vim.Revit.Exporter".</summary>
        public string Generator;

        /// <summary>When that VIM file was written (UTC, ISO 8601).</summary>
        public string CreatedUtc;

        /// <summary>The document's number of saves at export time (Revit: DocumentVersion.NumberOfSaves).</summary>
        public int DocumentNumSaves;

        /// <summary>The document's version id at export time (Revit: DocumentVersion.VersionGUID, new after any change).</summary>
        public string DocumentVersionGuid;

        /// <summary>The source document.</summary>
        public Relation<BimDocument> _BimDocument;

        public ExportRevisionKind GetKind()
            => (ExportRevisionKind) Kind;
    }

    /// <summary>
    /// Binds an Element to the ExportRevision which wrote its current data (at most one per Element).
    /// </summary>
    [TableName(TableNames.ElementInExportRevision)]
    [CascadeElementRemap]
    public partial class ElementInExportRevision : EntityWithElement, IStorageKey
    {
        public Relation<ExportRevision> _ExportRevision;

        public object GetStorageKey()
            => _Element.CombineAsStorageKey(_ExportRevision);
    }

    // ---------------------------------------------------------------------------------------------------------------
    // The activity log ("who changed what"): Contributor, SyncSession, ElementChange, ActivityMarker.
    //
    // These rows are VIM's own record, written at the moment a change happens and keyed on the element's Revit
    // UniqueId, because Revit's own record cannot be relied on: it says nothing at all about a deleted element, and
    // re-saving a model as a new central resets "Created by" / "Last changed by" for every element to one person.
    // See C:\VimFlexLocal\work\exporter\LIVE-SYNC-UX.md and Vim.Revit/Core/INCREMENTAL-EXPORT.md.
    // ---------------------------------------------------------------------------------------------------------------

    /// <summary>Where a Contributor's name was read (Contributor.Source).</summary>
    public enum ContributorSource
    {
        /// <summary>Nobody can say: Revit records no author for a deletion made in somebody else's local.</summary>
        Unknown = 0,
        /// <summary>A Revit session VIM watched: Application.Username at the moment of the change.</summary>
        Session = 1,
        /// <summary>Revit's worksharing "last changed by", read once when the change arrived from the central model.</summary>
        Central = 2,
        /// <summary>A linked model: VIM saw the link reload, not the person who changed it.</summary>
        Link = 3,
    }

    /// <summary>
    /// One person the activity log names, per source. The same person can hold two rows - one Session, one Central -
    /// on purpose: a name VIM watched is a different quality of fact from a name Revit handed over.
    /// </summary>
    [TableName(TableNames.Contributor)]
    public partial class Contributor : Entity
    {
        /// <summary>Revit's Application.Username (self-declared off ACC), or the worksharing LastChangedBy.</summary>
        public string Name;

        /// <summary>The integer representation of the ContributorSource enum.</summary>
        public int Source;

        public ContributorSource GetSource()
            => (ContributorSource) Source;
    }

    /// <summary>
    /// One Revit session VIM watched, in one document. A session usually spans several syncs: its row is written again
    /// at each of them, with a later EndedUtc, and a patch merges the two on SessionGuid.
    /// </summary>
    [TableName(TableNames.SyncSession)]
    public partial class SyncSession : Entity
    {
        /// <summary>Identifies the session across the syncs it wrote (a GUID made when VIM first saw the document change).</summary>
        public string SessionGuid;

        public string MachineName;

        /// <summary>The Revit version the session ran in, for example "2027".</summary>
        public string RevitVersion;

        /// <summary>When VIM first saw this session touch the document (UTC, ISO 8601).</summary>
        public string StartedUtc;

        /// <summary>When the session last wrote into this file (UTC, ISO 8601); it may still be open.</summary>
        public string EndedUtc;

        public Relation<Contributor> _Contributor;

        public Relation<BimDocument> _BimDocument;
    }

    /// <summary>What happened to the element (ElementChange.ChangeKind).</summary>
    public enum ElementChangeKind
    {
        Unknown = 0,
        Added = 1,
        Modified = 2,
        Deleted = 3,
    }

    /// <summary>How much the name on an ElementChange row is worth (ElementChange.Confidence).</summary>
    public enum ElementChangeConfidence
    {
        /// <summary>It vanished between two syncs and nobody can say who did it.</summary>
        Unknown = 0,
        /// <summary>VIM watched this person do it. Survives anything, including a re-save as a new central.</summary>
        Observed = 1,
        /// <summary>
        /// It arrived at a sync and the name is Revit's worksharing "last changed by", read once at that moment:
        /// second-hand, possibly stale, and re-writable in bulk and retroactively by a re-save as a new central.
        /// </summary>
        FromCentral = 2,
    }

    /// <summary>
    /// One element changed once, as one sync recorded it. The row carries the element's names itself instead of
    /// pointing at an Element row: a deleted element has no Element row, and this table has to outlive its elements.
    /// Join on UniqueId to reach the element while it still exists.
    /// </summary>
    [TableName(TableNames.ElementChange)]
    public partial class ElementChange : Entity
    {
        /// <summary>
        /// The export which wrote this row into this file - not the export the change happened in: when it happened
        /// is <see cref="ChangedUtc"/>. A full export rewrites the file, so it rewrites its history under one revision.
        /// </summary>
        public Relation<ExportRevision> _ExportRevision;

        /// <summary>The session which made the change; none when the change arrived from the central model.</summary>
        public Relation<SyncSession> _SyncSession;

        /// <summary>Who the log names for this change (see Confidence for what that name is worth).</summary>
        public Relation<Contributor> _Contributor;

        /// <summary>The integer representation of the ElementChangeKind enum.</summary>
        public int ChangeKind;

        /// <summary>The integer representation of the ElementChangeConfidence enum.</summary>
        public int Confidence;

        /// <summary>When the change happened for an observed row, when it arrived for a row from central (UTC, ISO 8601).</summary>
        public string ChangedUtc;

        /// <summary>The Revit UniqueId of the element. The only key that survives the element's deletion.</summary>
        public string UniqueId;

        // The names Revit showed for the element at the moment of the change, so a deletion still reads
        // "Doors / M_Single-Flush / 0915x2134 on Level 4". Empty when VIM never met the element.
        public string CategoryName;
        public string FamilyName;
        public string TypeName;
        public string ElementName;
        public string LevelName;
        public string WorksetName;

        public ElementChangeKind GetChangeKind()
            => (ElementChangeKind) ChangeKind;

        public ElementChangeConfidence GetConfidence()
            => (ElementChangeConfidence) Confidence;
    }

    /// <summary>What an ActivityMarker records (ActivityMarker.Kind).</summary>
    public enum ActivityMarkerKind
    {
        Unknown = 0,
        /// <summary>
        /// This model was re-saved as a new central: Revit's own "created by" / "last changed by" was reset to one
        /// person at this point, so every FromCentral row written after it is unreliable. VIM's own rows are not.
        /// </summary>
        HistoryReset = 1,
        /// <summary>A linked model reloaded: its elements were re-exported whole, and VIM cannot name who changed them.</summary>
        LinkReloaded = 2,
        /// <summary>Activity recording was off for a while, so there is a hole in the log.</summary>
        LoggingGap = 3,
        /// <summary>Older activity was dropped to keep the file within the retention cap.</summary>
        ActivityTrimmed = 4,
        /// <summary>Somebody holds a whole user workset (Sam 2026-10-12: taking a whole workset is a practice to detect).</summary>
        WorksetHeld = 5,
    }

    /// <summary>
    /// Something that happened to the log itself rather than to an element: shown as a banner above the activity view
    /// so nobody reads a wiped history, or a hole in the log, as somebody's work.
    /// </summary>
    [TableName(TableNames.ActivityMarker)]
    public partial class ActivityMarker : Entity
    {
        public Relation<ExportRevision> _ExportRevision;

        public Relation<SyncSession> _SyncSession;

        /// <summary>Who did it, when VIM can say (the person who re-saved the central, for example).</summary>
        public Relation<Contributor> _Contributor;

        /// <summary>The integer representation of the ActivityMarkerKind enum.</summary>
        public int Kind;

        public string OccurredUtc;

        /// <summary>One plain sentence, ready to show.</summary>
        public string Description;

        public ActivityMarkerKind GetKind()
            => (ActivityMarkerKind) Kind;
    }

    /// <summary>
    /// Helper functions for performing reflection over the object model
    /// </summary>
    public static partial class ObjectModelReflection
    {
        public static EntityColumnLoadingInfo[] GetEntityColumnLoadingInfo(this FieldInfo fi)
            => fi.GetOrderedEntityColumnLoaderAttributes(SchemaVersion.Current)
                .Select(a =>
                {
                    var (strategy, typePrefix) = a.SerializedType.GetValueSerializationStrategyAndTypePrefix();
                    return new EntityColumnLoadingInfo(strategy, typePrefix, a);
                })
                .ToArray();

        public static bool IsEntityAndHasTableNameAttribute(this Type t)
            => typeof(Entity).IsAssignableFrom(t) && t.GetCustomAttribute(typeof(TableNameAttribute)) != null;

        public static ElementKind GetElementKind(this Type t)
        {
            if (t.GetCustomAttribute(typeof(ElementKindAttribute)) is ElementKindAttribute attr)
            {
                return attr.ElementKind;
            }
            else
            {
                return ElementKind.Unknown;
            }
        }

        public static IEnumerable<Type> GetEntityTypes<T>() where T : Entity
            => typeof(T).Assembly.GetAllSubclassesOf(typeof(T));

        public static IEnumerable<Type> GetEntityTypes()
            => GetEntityTypes<Entity>().Where(IsEntityAndHasTableNameAttribute);

        public static object GetPropertyValue(this DocumentModel documentModel, string propertyName)
            => documentModel.GetType().GetProperty(propertyName)?.GetValue(documentModel, null);

        public static VimSchema GetCurrentVimSchema()
        {
            var vimSchema = new VimSchema(VimFormatVersion.Current, SchemaVersion.Current);

            foreach (var entityType in GetEntityTypes())
            {
                var entityTableSchema = vimSchema.AddEntityTableSchema(entityType.GetEntityTableName());

                foreach (var fieldInfo in entityType.GetRelationFields())
                {
                    var (indexColumnName, _) = fieldInfo.GetIndexColumnInfo();
                    entityTableSchema.AddColumn(indexColumnName);
                }

                foreach (var fieldInfo in entityType.GetEntityFields())
                {
                    var loadingInfos = fieldInfo.GetEntityColumnLoadingInfo();

                    foreach (var li in loadingInfos)
                        entityTableSchema.AddColumn(li.EntityColumnAttribute.SerializedValueColumnName);
                }
            }
            return vimSchema;
        }
    }
}
