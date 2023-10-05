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
        public static SerializableVersion Current => v5_1_0;
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

    /// <summary>
    /// Represents an Entity which contains a Relation to an Element.
    /// </summary>
    public partial class EntityWithElement : Entity
    {
        public Relation<Element> _Element;
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
    /// Defines how a parameter is displayed by indicating whether the value's type is a length, a volume, etc,
    /// and how that value should be shown via its spec (ex: as fractional inches, etc).
    /// </summary>
    [TableName(TableNames.DisplayUnit)]
    public partial class DisplayUnit : Entity, IStorageKey
    {
        public string Spec; // ex: "UT_Length" in Revit 2020 and prior, or "autodesk.spec.aec:length-1.0.0" in Revit 2021 and up.
        public string Type; // ex: "DUT_FEET_FRACTIONAL_INCHES" in Revit 2020 and prior, or "autodesk.unit.unit:feetFractionalInches-1.0.0" in Revit 2021 and up.
        public string Label; // The localized label, ex: "Feet and fractional inches"

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
    /// Represents a parameter descriptor.
    /// </summary>
    [TableName(TableNames.ParameterDescriptor)]
    public partial class ParameterDescriptor : Entity, IStorageKey
    {
        public string Name;
        public string Group;
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
        /// The string representation of the parameter GUID. In Revit, this GUID only exists if IsShared is true.
        /// </summary>
        public string Guid;

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
    }

    /// <summary>
    /// Represents a parameter associated to an Element. An Element can contain 0..* Parameters.
    /// </summary>
    [TableName(TableNames.Parameter)]
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

        public float Location_X;
        public float Location_Y;
        public float Location_Z;
        public Vector3 Location
        {
            get => new Vector3(Location_X, Location_Y, Location_Z);
            set => (Location_X, Location_Y, Location_Z) = value;
        }

        public string FamilyName;
        public bool IsPinned;

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
    public partial class DesignOption : EntityWithElement
    {
        public bool IsPrimary;
    }

    /// <summary>
    /// Represents an XY plane at a specific Z coordinate in the model.
    /// </summary>
    [TableName(TableNames.Level)]
    public partial class Level : EntityWithElement
    {
        /// <summary>
        /// The elevation above or below the ground level.
        /// </summary>
        public double Elevation;

        /// <summary>
        /// The associated Level's FamilyType (in Revit, this maps to its LevelType)
        /// </summary>
        public Relation<FamilyType> _FamilyType;
    }

    /// <summary>
    /// Represents a phase of construction.
    /// </summary>
    [TableName(TableNames.Phase)]
    public partial class Phase : EntityWithElement
    {
    }

    /// <summary>
    /// Represents a room in the model.
    /// </summary>
    [TableName(TableNames.Room)]
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
    }

    /// <summary>
    /// Represents a collection FamilyTypes, for example an 'I Beam' Family.
    /// </summary>
    [TableName(TableNames.Family)]
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
    }

    /// <summary>
    /// Represents a 3D or a 2D view.
    /// </summary>
    [TableName(TableNames.View)]
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
    }

    /// <summary>
    /// An associative table binding an Element to a View.
    /// </summary>
    [TableName(TableNames.ElementInView)]
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
    }

    /// <summary>
    /// An associative table binding a Material to an Element.
    /// </summary>
    [TableName(TableNames.MaterialInElement)]
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
    /// Represents a mesh in the G3D buffer of the VIM file.
    /// </summary>
    [TableName(TableNames.Geometry)]
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
    /// Represents a sequence of Vector3 points in world space.
    /// The ordering and number of Shapes matches the ordering and number of shapes in the G3D buffer.
    /// </summary>
    [TableName(TableNames.Shape)]
    [G3dAttributeReference("g3d:shape:vertexoffset:0:int32:1", G3dAttributeReferenceMultiplicity.OneToOne, true)]
    [G3dAttributeReference("g3d:shape:color:0:float32:4", G3dAttributeReferenceMultiplicity.OneToOne, true)]
    [G3dAttributeReference("g3d:shape:width:0:float32:1", G3dAttributeReferenceMultiplicity.OneToOne, true)]
    public partial class Shape : EntityWithElement
    {
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

    /// <summary>
    /// Represents a collection of Elements which compose a System. These may be mechanical systems, piping systems, electrical systems, curtain walls, stairs, etc.
    /// </summary>
    [TableName(TableNames.System)]
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
    }

    /// <summary>
    /// An associative table binding an Element to a Warning.
    /// </summary>
    [TableName(TableNames.ElementInWarning)]
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
    public partial class BasePoint : EntityWithElement
    {
        /// <summary>
        /// Returns true if the BasePoint is the associated BimDocument's current survey point. The associated BimDocument is stored in the Element relation.
        /// </summary>
        public bool IsSurveyPoint;

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
    }

    /// <summary>
    /// Represents a vertical plane (or a vertical cylindrical segment when curved).
    /// </summary>
    [TableName(TableNames.Grid)]
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
    }

    /// <summary>
    /// Represents a planar region which can be used to represent places like parking spots.
    /// </summary>
    [TableName(TableNames.Area)]
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
    public partial class ViewSheetSet : EntityWithElement
    { }

    /// <summary>
    /// Represents a view sheet, which can contain multiple views.
    /// </summary>
    [TableName(TableNames.ViewSheet)]
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
