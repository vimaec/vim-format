// AUTO-GENERATED FILE, DO NOT MODIFY.
// ReSharper disable All
using System;
using System.Collections.Generic;
using System.Linq;
using Vim.Math3d;
using Vim.LinqArray;
using Vim.Format.ObjectModel;

namespace Vim.Format.ObjectModel {
    // AUTO-GENERATED
    public partial class Asset
    {
        public Asset()
        {
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Asset other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (BufferName == other.BufferName);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class DisplayUnit
    {
        public DisplayUnit()
        {
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is DisplayUnit other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Spec == other.Spec) &&
                    (Type == other.Type) &&
                    (Label == other.Label);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class ParameterDescriptor
    {
        public DisplayUnit DisplayUnit => _DisplayUnit.Value;
        public ParameterDescriptor()
        {
            _DisplayUnit = new Relation<DisplayUnit>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is ParameterDescriptor other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Name == other.Name) &&
                    (Group == other.Group) &&
                    (ParameterType == other.ParameterType) &&
                    (IsInstance == other.IsInstance) &&
                    (IsShared == other.IsShared) &&
                    (IsReadOnly == other.IsReadOnly) &&
                    (Flags == other.Flags) &&
                    (Guid == other.Guid) &&
                    (_DisplayUnit?.Index == other._DisplayUnit?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class Parameter
    {
        public ParameterDescriptor ParameterDescriptor => _ParameterDescriptor.Value;
        public Element Element => _Element.Value;
        public Parameter()
        {
            _ParameterDescriptor = new Relation<ParameterDescriptor>();
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Parameter other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Value == other.Value) &&
                    (_ParameterDescriptor?.Index == other._ParameterDescriptor?.Index) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class Element
    {
        public Level Level => _Level.Value;
        public Phase PhaseCreated => _PhaseCreated.Value;
        public Phase PhaseDemolished => _PhaseDemolished.Value;
        public Category Category => _Category.Value;
        public Workset Workset => _Workset.Value;
        public DesignOption DesignOption => _DesignOption.Value;
        public View OwnerView => _OwnerView.Value;
        public Group Group => _Group.Value;
        public AssemblyInstance AssemblyInstance => _AssemblyInstance.Value;
        public BimDocument BimDocument => _BimDocument.Value;
        public Room Room => _Room.Value;
        public Element()
        {
            _Level = new Relation<Level>();
            _PhaseCreated = new Relation<Phase>();
            _PhaseDemolished = new Relation<Phase>();
            _Category = new Relation<Category>();
            _Workset = new Relation<Workset>();
            _DesignOption = new Relation<DesignOption>();
            _OwnerView = new Relation<View>();
            _Group = new Relation<Group>();
            _AssemblyInstance = new Relation<AssemblyInstance>();
            _BimDocument = new Relation<BimDocument>();
            _Room = new Relation<Room>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Element other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Id == other.Id) &&
                    (Type == other.Type) &&
                    (Name == other.Name) &&
                    (UniqueId == other.UniqueId) &&
                    (Location == other.Location) &&
                    (FamilyName == other.FamilyName) &&
                    (IsPinned == other.IsPinned) &&
                    (_Level?.Index == other._Level?.Index) &&
                    (_PhaseCreated?.Index == other._PhaseCreated?.Index) &&
                    (_PhaseDemolished?.Index == other._PhaseDemolished?.Index) &&
                    (_Category?.Index == other._Category?.Index) &&
                    (_Workset?.Index == other._Workset?.Index) &&
                    (_DesignOption?.Index == other._DesignOption?.Index) &&
                    (_OwnerView?.Index == other._OwnerView?.Index) &&
                    (_Group?.Index == other._Group?.Index) &&
                    (_AssemblyInstance?.Index == other._AssemblyInstance?.Index) &&
                    (_BimDocument?.Index == other._BimDocument?.Index) &&
                    (_Room?.Index == other._Room?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class Workset
    {
        public BimDocument BimDocument => _BimDocument.Value;
        public Workset()
        {
            _BimDocument = new Relation<BimDocument>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Workset other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Id == other.Id) &&
                    (Name == other.Name) &&
                    (Kind == other.Kind) &&
                    (IsOpen == other.IsOpen) &&
                    (IsEditable == other.IsEditable) &&
                    (Owner == other.Owner) &&
                    (UniqueId == other.UniqueId) &&
                    (_BimDocument?.Index == other._BimDocument?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class AssemblyInstance
    {
        public Element Element => _Element.Value;
        public AssemblyInstance()
        {
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is AssemblyInstance other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (AssemblyTypeName == other.AssemblyTypeName) &&
                    (Position == other.Position) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class Group
    {
        public Element Element => _Element.Value;
        public Group()
        {
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Group other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (GroupType == other.GroupType) &&
                    (Position == other.Position) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class DesignOption
    {
        public Element Element => _Element.Value;
        public DesignOption()
        {
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is DesignOption other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (IsPrimary == other.IsPrimary) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class Level
    {
        public FamilyType FamilyType => _FamilyType.Value;
        public Element Element => _Element.Value;
        public Level()
        {
            _FamilyType = new Relation<FamilyType>();
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Level other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Elevation == other.Elevation) &&
                    (_FamilyType?.Index == other._FamilyType?.Index) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class Phase
    {
        public Element Element => _Element.Value;
        public Phase()
        {
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Phase other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class Room
    {
        public Level UpperLimit => _UpperLimit.Value;
        public Element Element => _Element.Value;
        public Room()
        {
            _UpperLimit = new Relation<Level>();
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Room other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (BaseOffset == other.BaseOffset) &&
                    (LimitOffset == other.LimitOffset) &&
                    (UnboundedHeight == other.UnboundedHeight) &&
                    (Volume == other.Volume) &&
                    (Perimeter == other.Perimeter) &&
                    (Area == other.Area) &&
                    (Number == other.Number) &&
                    (_UpperLimit?.Index == other._UpperLimit?.Index) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class BimDocument
    {
        public View ActiveView => _ActiveView.Value;
        public Family OwnerFamily => _OwnerFamily.Value;
        public BimDocument Parent => _Parent.Value;
        public Element Element => _Element.Value;
        public BimDocument()
        {
            _ActiveView = new Relation<View>();
            _OwnerFamily = new Relation<Family>();
            _Parent = new Relation<BimDocument>();
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is BimDocument other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Title == other.Title) &&
                    (IsMetric == other.IsMetric) &&
                    (NumSaves == other.NumSaves) &&
                    (IsLinked == other.IsLinked) &&
                    (IsDetached == other.IsDetached) &&
                    (IsWorkshared == other.IsWorkshared) &&
                    (PathName == other.PathName) &&
                    (Latitude == other.Latitude) &&
                    (Longitude == other.Longitude) &&
                    (TimeZone == other.TimeZone) &&
                    (PlaceName == other.PlaceName) &&
                    (WeatherStationName == other.WeatherStationName) &&
                    (Elevation == other.Elevation) &&
                    (ProjectLocation == other.ProjectLocation) &&
                    (IssueDate == other.IssueDate) &&
                    (Status == other.Status) &&
                    (ClientName == other.ClientName) &&
                    (Address == other.Address) &&
                    (Name == other.Name) &&
                    (Number == other.Number) &&
                    (Author == other.Author) &&
                    (BuildingName == other.BuildingName) &&
                    (OrganizationName == other.OrganizationName) &&
                    (OrganizationDescription == other.OrganizationDescription) &&
                    (Product == other.Product) &&
                    (Version == other.Version) &&
                    (User == other.User) &&
                    (_ActiveView?.Index == other._ActiveView?.Index) &&
                    (_OwnerFamily?.Index == other._OwnerFamily?.Index) &&
                    (_Parent?.Index == other._Parent?.Index) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class DisplayUnitInBimDocument
    {
        public DisplayUnit DisplayUnit => _DisplayUnit.Value;
        public BimDocument BimDocument => _BimDocument.Value;
        public DisplayUnitInBimDocument()
        {
            _DisplayUnit = new Relation<DisplayUnit>();
            _BimDocument = new Relation<BimDocument>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is DisplayUnitInBimDocument other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (_DisplayUnit?.Index == other._DisplayUnit?.Index) &&
                    (_BimDocument?.Index == other._BimDocument?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class PhaseOrderInBimDocument
    {
        public Phase Phase => _Phase.Value;
        public BimDocument BimDocument => _BimDocument.Value;
        public PhaseOrderInBimDocument()
        {
            _Phase = new Relation<Phase>();
            _BimDocument = new Relation<BimDocument>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is PhaseOrderInBimDocument other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (OrderIndex == other.OrderIndex) &&
                    (_Phase?.Index == other._Phase?.Index) &&
                    (_BimDocument?.Index == other._BimDocument?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class Category
    {
        public Category Parent => _Parent.Value;
        public Material Material => _Material.Value;
        public Category()
        {
            _Parent = new Relation<Category>();
            _Material = new Relation<Material>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Category other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Name == other.Name) &&
                    (Id == other.Id) &&
                    (CategoryType == other.CategoryType) &&
                    (LineColor == other.LineColor) &&
                    (BuiltInCategory == other.BuiltInCategory) &&
                    (_Parent?.Index == other._Parent?.Index) &&
                    (_Material?.Index == other._Material?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class Family
    {
        public Category FamilyCategory => _FamilyCategory.Value;
        public Element Element => _Element.Value;
        public Family()
        {
            _FamilyCategory = new Relation<Category>();
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Family other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (StructuralMaterialType == other.StructuralMaterialType) &&
                    (StructuralSectionShape == other.StructuralSectionShape) &&
                    (IsSystemFamily == other.IsSystemFamily) &&
                    (IsInPlace == other.IsInPlace) &&
                    (_FamilyCategory?.Index == other._FamilyCategory?.Index) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class FamilyType
    {
        public Family Family => _Family.Value;
        public CompoundStructure CompoundStructure => _CompoundStructure.Value;
        public Element Element => _Element.Value;
        public FamilyType()
        {
            _Family = new Relation<Family>();
            _CompoundStructure = new Relation<CompoundStructure>();
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is FamilyType other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (IsSystemFamilyType == other.IsSystemFamilyType) &&
                    (_Family?.Index == other._Family?.Index) &&
                    (_CompoundStructure?.Index == other._CompoundStructure?.Index) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class FamilyInstance
    {
        public FamilyType FamilyType => _FamilyType.Value;
        public Element Host => _Host.Value;
        public Room FromRoom => _FromRoom.Value;
        public Room ToRoom => _ToRoom.Value;
        public Element Element => _Element.Value;
        public FamilyInstance()
        {
            _FamilyType = new Relation<FamilyType>();
            _Host = new Relation<Element>();
            _FromRoom = new Relation<Room>();
            _ToRoom = new Relation<Room>();
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is FamilyInstance other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (FacingFlipped == other.FacingFlipped) &&
                    (FacingOrientation == other.FacingOrientation) &&
                    (HandFlipped == other.HandFlipped) &&
                    (Mirrored == other.Mirrored) &&
                    (HasModifiedGeometry == other.HasModifiedGeometry) &&
                    (Scale == other.Scale) &&
                    (BasisX == other.BasisX) &&
                    (BasisY == other.BasisY) &&
                    (BasisZ == other.BasisZ) &&
                    (Translation == other.Translation) &&
                    (HandOrientation == other.HandOrientation) &&
                    (_FamilyType?.Index == other._FamilyType?.Index) &&
                    (_Host?.Index == other._Host?.Index) &&
                    (_FromRoom?.Index == other._FromRoom?.Index) &&
                    (_ToRoom?.Index == other._ToRoom?.Index) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class View
    {
        public Camera Camera => _Camera.Value;
        public FamilyType FamilyType => _FamilyType.Value;
        public Element Element => _Element.Value;
        public View()
        {
            _Camera = new Relation<Camera>();
            _FamilyType = new Relation<FamilyType>();
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is View other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Title == other.Title) &&
                    (ViewType == other.ViewType) &&
                    (Up == other.Up) &&
                    (Right == other.Right) &&
                    (Origin == other.Origin) &&
                    (ViewDirection == other.ViewDirection) &&
                    (ViewPosition == other.ViewPosition) &&
                    (Scale == other.Scale) &&
                    (Outline == other.Outline) &&
                    (DetailLevel == other.DetailLevel) &&
                    (_Camera?.Index == other._Camera?.Index) &&
                    (_FamilyType?.Index == other._FamilyType?.Index) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class ElementInView
    {
        public View View => _View.Value;
        public Element Element => _Element.Value;
        public ElementInView()
        {
            _View = new Relation<View>();
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is ElementInView other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (_View?.Index == other._View?.Index) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class ShapeInView
    {
        public Shape Shape => _Shape.Value;
        public View View => _View.Value;
        public ShapeInView()
        {
            _Shape = new Relation<Shape>();
            _View = new Relation<View>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is ShapeInView other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (_Shape?.Index == other._Shape?.Index) &&
                    (_View?.Index == other._View?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class AssetInView
    {
        public Asset Asset => _Asset.Value;
        public View View => _View.Value;
        public AssetInView()
        {
            _Asset = new Relation<Asset>();
            _View = new Relation<View>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is AssetInView other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (_Asset?.Index == other._Asset?.Index) &&
                    (_View?.Index == other._View?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class LevelInView
    {
        public Level Level => _Level.Value;
        public View View => _View.Value;
        public LevelInView()
        {
            _Level = new Relation<Level>();
            _View = new Relation<View>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is LevelInView other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Extents == other.Extents) &&
                    (_Level?.Index == other._Level?.Index) &&
                    (_View?.Index == other._View?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class Camera
    {
        public Camera()
        {
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Camera other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Id == other.Id) &&
                    (IsPerspective == other.IsPerspective) &&
                    (VerticalExtent == other.VerticalExtent) &&
                    (HorizontalExtent == other.HorizontalExtent) &&
                    (FarDistance == other.FarDistance) &&
                    (NearDistance == other.NearDistance) &&
                    (TargetDistance == other.TargetDistance) &&
                    (RightOffset == other.RightOffset) &&
                    (UpOffset == other.UpOffset);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class Material
    {
        public Asset ColorTextureFile => _ColorTextureFile.Value;
        public Asset NormalTextureFile => _NormalTextureFile.Value;
        public Element Element => _Element.Value;
        public Material()
        {
            _ColorTextureFile = new Relation<Asset>();
            _NormalTextureFile = new Relation<Asset>();
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Material other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Name == other.Name) &&
                    (MaterialCategory == other.MaterialCategory) &&
                    (Color == other.Color) &&
                    (ColorUvScaling == other.ColorUvScaling) &&
                    (ColorUvOffset == other.ColorUvOffset) &&
                    (NormalUvScaling == other.NormalUvScaling) &&
                    (NormalUvOffset == other.NormalUvOffset) &&
                    (NormalAmount == other.NormalAmount) &&
                    (Glossiness == other.Glossiness) &&
                    (Smoothness == other.Smoothness) &&
                    (Transparency == other.Transparency) &&
                    (_ColorTextureFile?.Index == other._ColorTextureFile?.Index) &&
                    (_NormalTextureFile?.Index == other._NormalTextureFile?.Index) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class MaterialInElement
    {
        public Material Material => _Material.Value;
        public Element Element => _Element.Value;
        public MaterialInElement()
        {
            _Material = new Relation<Material>();
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is MaterialInElement other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Area == other.Area) &&
                    (Volume == other.Volume) &&
                    (IsPaint == other.IsPaint) &&
                    (_Material?.Index == other._Material?.Index) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class CompoundStructureLayer
    {
        public Material Material => _Material.Value;
        public CompoundStructure CompoundStructure => _CompoundStructure.Value;
        public CompoundStructureLayer()
        {
            _Material = new Relation<Material>();
            _CompoundStructure = new Relation<CompoundStructure>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is CompoundStructureLayer other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (OrderIndex == other.OrderIndex) &&
                    (Width == other.Width) &&
                    (MaterialFunctionAssignment == other.MaterialFunctionAssignment) &&
                    (_Material?.Index == other._Material?.Index) &&
                    (_CompoundStructure?.Index == other._CompoundStructure?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class CompoundStructure
    {
        public CompoundStructureLayer StructuralLayer => _StructuralLayer.Value;
        public CompoundStructure()
        {
            _StructuralLayer = new Relation<CompoundStructureLayer>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is CompoundStructure other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Width == other.Width) &&
                    (_StructuralLayer?.Index == other._StructuralLayer?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class Node
    {
        public Element Element => _Element.Value;
        public Node()
        {
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Node other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class Geometry
    {
        public Geometry()
        {
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Geometry other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Box == other.Box) &&
                    (VertexCount == other.VertexCount) &&
                    (FaceCount == other.FaceCount);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class Shape
    {
        public Element Element => _Element.Value;
        public Shape()
        {
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Shape other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class ShapeCollection
    {
        public Element Element => _Element.Value;
        public ShapeCollection()
        {
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is ShapeCollection other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class ShapeInShapeCollection
    {
        public Shape Shape => _Shape.Value;
        public ShapeCollection ShapeCollection => _ShapeCollection.Value;
        public ShapeInShapeCollection()
        {
            _Shape = new Relation<Shape>();
            _ShapeCollection = new Relation<ShapeCollection>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is ShapeInShapeCollection other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (_Shape?.Index == other._Shape?.Index) &&
                    (_ShapeCollection?.Index == other._ShapeCollection?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class System
    {
        public FamilyType FamilyType => _FamilyType.Value;
        public Element Element => _Element.Value;
        public System()
        {
            _FamilyType = new Relation<FamilyType>();
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is System other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (SystemType == other.SystemType) &&
                    (_FamilyType?.Index == other._FamilyType?.Index) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class ElementInSystem
    {
        public System System => _System.Value;
        public Element Element => _Element.Value;
        public ElementInSystem()
        {
            _System = new Relation<System>();
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is ElementInSystem other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Roles == other.Roles) &&
                    (_System?.Index == other._System?.Index) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class Warning
    {
        public BimDocument BimDocument => _BimDocument.Value;
        public Warning()
        {
            _BimDocument = new Relation<BimDocument>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Warning other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Guid == other.Guid) &&
                    (Severity == other.Severity) &&
                    (Description == other.Description) &&
                    (_BimDocument?.Index == other._BimDocument?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class ElementInWarning
    {
        public Warning Warning => _Warning.Value;
        public Element Element => _Element.Value;
        public ElementInWarning()
        {
            _Warning = new Relation<Warning>();
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is ElementInWarning other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (_Warning?.Index == other._Warning?.Index) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class BasePoint
    {
        public Element Element => _Element.Value;
        public BasePoint()
        {
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is BasePoint other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (IsSurveyPoint == other.IsSurveyPoint) &&
                    (Position == other.Position) &&
                    (SharedPosition == other.SharedPosition) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class PhaseFilter
    {
        public Element Element => _Element.Value;
        public PhaseFilter()
        {
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is PhaseFilter other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (New == other.New) &&
                    (Existing == other.Existing) &&
                    (Demolished == other.Demolished) &&
                    (Temporary == other.Temporary) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class Grid
    {
        public FamilyType FamilyType => _FamilyType.Value;
        public Element Element => _Element.Value;
        public Grid()
        {
            _FamilyType = new Relation<FamilyType>();
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Grid other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (StartPoint == other.StartPoint) &&
                    (EndPoint == other.EndPoint) &&
                    (IsCurved == other.IsCurved) &&
                    (Extents == other.Extents) &&
                    (_FamilyType?.Index == other._FamilyType?.Index) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class Area
    {
        public AreaScheme AreaScheme => _AreaScheme.Value;
        public Element Element => _Element.Value;
        public Area()
        {
            _AreaScheme = new Relation<AreaScheme>();
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Area other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Value == other.Value) &&
                    (Perimeter == other.Perimeter) &&
                    (Number == other.Number) &&
                    (IsGrossInterior == other.IsGrossInterior) &&
                    (_AreaScheme?.Index == other._AreaScheme?.Index) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class AreaScheme
    {
        public Element Element => _Element.Value;
        public AreaScheme()
        {
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is AreaScheme other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (IsGrossBuildingArea == other.IsGrossBuildingArea) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class Schedule
    {
        public Element Element => _Element.Value;
        public Schedule()
        {
            _Element = new Relation<Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Schedule other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (_Element?.Index == other._Element?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class ScheduleColumn
    {
        public Schedule Schedule => _Schedule.Value;
        public ScheduleColumn()
        {
            _Schedule = new Relation<Schedule>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is ScheduleColumn other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Name == other.Name) &&
                    (ColumnIndex == other.ColumnIndex) &&
                    (_Schedule?.Index == other._Schedule?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    // AUTO-GENERATED
    public partial class ScheduleCell
    {
        public ScheduleColumn ScheduleColumn => _ScheduleColumn.Value;
        public ScheduleCell()
        {
            _ScheduleColumn = new Relation<ScheduleColumn>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is ScheduleCell other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Value == other.Value) &&
                    (RowIndex == other.RowIndex) &&
                    (_ScheduleColumn?.Index == other._ScheduleColumn?.Index);
                if (!fieldsAreEqual)
                {
                    return false;
                }
                
                return true;
            }
            return false;
        }
        
    } // end of class
    
    public partial class DocumentModel
    {
        public ElementIndexMaps ElementIndexMaps { get; }
        
        // Asset
        
        public EntityTable AssetEntityTable { get; }
        
        public IArray<String> AssetBufferName { get; }
        public String GetAssetBufferName(int index, String defaultValue = "") => AssetBufferName?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public int NumAsset => AssetEntityTable?.NumRows ?? 0;
        public IArray<Asset> AssetList { get; }
        public Asset GetAsset(int n)
        {
            if (n < 0) return null;
            var r = new Asset();
            r.Document = Document;
            r.Index = n;
            r.BufferName = AssetBufferName.ElementAtOrDefault(n);
            return r;
        }
        
        
        // DisplayUnit
        
        public EntityTable DisplayUnitEntityTable { get; }
        
        public IArray<String> DisplayUnitSpec { get; }
        public String GetDisplayUnitSpec(int index, String defaultValue = "") => DisplayUnitSpec?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> DisplayUnitType { get; }
        public String GetDisplayUnitType(int index, String defaultValue = "") => DisplayUnitType?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> DisplayUnitLabel { get; }
        public String GetDisplayUnitLabel(int index, String defaultValue = "") => DisplayUnitLabel?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public int NumDisplayUnit => DisplayUnitEntityTable?.NumRows ?? 0;
        public IArray<DisplayUnit> DisplayUnitList { get; }
        public DisplayUnit GetDisplayUnit(int n)
        {
            if (n < 0) return null;
            var r = new DisplayUnit();
            r.Document = Document;
            r.Index = n;
            r.Spec = DisplayUnitSpec.ElementAtOrDefault(n);
            r.Type = DisplayUnitType.ElementAtOrDefault(n);
            r.Label = DisplayUnitLabel.ElementAtOrDefault(n);
            return r;
        }
        
        
        // ParameterDescriptor
        
        public EntityTable ParameterDescriptorEntityTable { get; }
        
        public IArray<String> ParameterDescriptorName { get; }
        public String GetParameterDescriptorName(int index, String defaultValue = "") => ParameterDescriptorName?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> ParameterDescriptorGroup { get; }
        public String GetParameterDescriptorGroup(int index, String defaultValue = "") => ParameterDescriptorGroup?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> ParameterDescriptorParameterType { get; }
        public String GetParameterDescriptorParameterType(int index, String defaultValue = "") => ParameterDescriptorParameterType?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Boolean> ParameterDescriptorIsInstance { get; }
        public Boolean GetParameterDescriptorIsInstance(int index, Boolean defaultValue = default) => ParameterDescriptorIsInstance?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Boolean> ParameterDescriptorIsShared { get; }
        public Boolean GetParameterDescriptorIsShared(int index, Boolean defaultValue = default) => ParameterDescriptorIsShared?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Boolean> ParameterDescriptorIsReadOnly { get; }
        public Boolean GetParameterDescriptorIsReadOnly(int index, Boolean defaultValue = default) => ParameterDescriptorIsReadOnly?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Int32> ParameterDescriptorFlags { get; }
        public Int32 GetParameterDescriptorFlags(int index, Int32 defaultValue = default) => ParameterDescriptorFlags?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> ParameterDescriptorGuid { get; }
        public String GetParameterDescriptorGuid(int index, String defaultValue = "") => ParameterDescriptorGuid?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> ParameterDescriptorDisplayUnitIndex { get; }
        public int GetParameterDescriptorDisplayUnitIndex(int index) => ParameterDescriptorDisplayUnitIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumParameterDescriptor => ParameterDescriptorEntityTable?.NumRows ?? 0;
        public IArray<ParameterDescriptor> ParameterDescriptorList { get; }
        public ParameterDescriptor GetParameterDescriptor(int n)
        {
            if (n < 0) return null;
            var r = new ParameterDescriptor();
            r.Document = Document;
            r.Index = n;
            r.Name = ParameterDescriptorName.ElementAtOrDefault(n);
            r.Group = ParameterDescriptorGroup.ElementAtOrDefault(n);
            r.ParameterType = ParameterDescriptorParameterType.ElementAtOrDefault(n);
            r.IsInstance = ParameterDescriptorIsInstance.ElementAtOrDefault(n);
            r.IsShared = ParameterDescriptorIsShared.ElementAtOrDefault(n);
            r.IsReadOnly = ParameterDescriptorIsReadOnly.ElementAtOrDefault(n);
            r.Flags = ParameterDescriptorFlags.ElementAtOrDefault(n);
            r.Guid = ParameterDescriptorGuid.ElementAtOrDefault(n);
            r._DisplayUnit = new Relation<DisplayUnit>(GetParameterDescriptorDisplayUnitIndex(n), GetDisplayUnit);
            return r;
        }
        
        
        // Parameter
        
        public EntityTable ParameterEntityTable { get; }
        
        public IArray<String> ParameterValue { get; }
        public String GetParameterValue(int index, String defaultValue = "") => ParameterValue?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> ParameterParameterDescriptorIndex { get; }
        public int GetParameterParameterDescriptorIndex(int index) => ParameterParameterDescriptorIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> ParameterElementIndex { get; }
        public int GetParameterElementIndex(int index) => ParameterElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumParameter => ParameterEntityTable?.NumRows ?? 0;
        public IArray<Parameter> ParameterList { get; }
        public Parameter GetParameter(int n)
        {
            if (n < 0) return null;
            var r = new Parameter();
            r.Document = Document;
            r.Index = n;
            r.Value = ParameterValue.ElementAtOrDefault(n);
            r._ParameterDescriptor = new Relation<ParameterDescriptor>(GetParameterParameterDescriptorIndex(n), GetParameterDescriptor);
            r._Element = new Relation<Element>(GetParameterElementIndex(n), GetElement);
            return r;
        }
        
        
        // Element
        
        public EntityTable ElementEntityTable { get; }
        
        public IArray<Int32> ElementId { get; }
        public Int32 GetElementId(int index, Int32 defaultValue = default) => ElementId?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> ElementType { get; }
        public String GetElementType(int index, String defaultValue = "") => ElementType?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> ElementName { get; }
        public String GetElementName(int index, String defaultValue = "") => ElementName?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> ElementUniqueId { get; }
        public String GetElementUniqueId(int index, String defaultValue = "") => ElementUniqueId?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Vector3> ElementLocation { get; }
        public Vector3 GetElementLocation(int index, Vector3 defaultValue = default) => ElementLocation?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> ElementFamilyName { get; }
        public String GetElementFamilyName(int index, String defaultValue = "") => ElementFamilyName?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Boolean> ElementIsPinned { get; }
        public Boolean GetElementIsPinned(int index, Boolean defaultValue = default) => ElementIsPinned?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> ElementLevelIndex { get; }
        public int GetElementLevelIndex(int index) => ElementLevelIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> ElementPhaseCreatedIndex { get; }
        public int GetElementPhaseCreatedIndex(int index) => ElementPhaseCreatedIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> ElementPhaseDemolishedIndex { get; }
        public int GetElementPhaseDemolishedIndex(int index) => ElementPhaseDemolishedIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> ElementCategoryIndex { get; }
        public int GetElementCategoryIndex(int index) => ElementCategoryIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> ElementWorksetIndex { get; }
        public int GetElementWorksetIndex(int index) => ElementWorksetIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> ElementDesignOptionIndex { get; }
        public int GetElementDesignOptionIndex(int index) => ElementDesignOptionIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> ElementOwnerViewIndex { get; }
        public int GetElementOwnerViewIndex(int index) => ElementOwnerViewIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> ElementGroupIndex { get; }
        public int GetElementGroupIndex(int index) => ElementGroupIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> ElementAssemblyInstanceIndex { get; }
        public int GetElementAssemblyInstanceIndex(int index) => ElementAssemblyInstanceIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> ElementBimDocumentIndex { get; }
        public int GetElementBimDocumentIndex(int index) => ElementBimDocumentIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> ElementRoomIndex { get; }
        public int GetElementRoomIndex(int index) => ElementRoomIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumElement => ElementEntityTable?.NumRows ?? 0;
        public IArray<Element> ElementList { get; }
        public Element GetElement(int n)
        {
            if (n < 0) return null;
            var r = new Element();
            r.Document = Document;
            r.Index = n;
            r.Id = ElementId.ElementAtOrDefault(n);
            r.Type = ElementType.ElementAtOrDefault(n);
            r.Name = ElementName.ElementAtOrDefault(n);
            r.UniqueId = ElementUniqueId.ElementAtOrDefault(n);
            r.Location = ElementLocation.ElementAtOrDefault(n);
            r.FamilyName = ElementFamilyName.ElementAtOrDefault(n);
            r.IsPinned = ElementIsPinned.ElementAtOrDefault(n);
            r._Level = new Relation<Level>(GetElementLevelIndex(n), GetLevel);
            r._PhaseCreated = new Relation<Phase>(GetElementPhaseCreatedIndex(n), GetPhase);
            r._PhaseDemolished = new Relation<Phase>(GetElementPhaseDemolishedIndex(n), GetPhase);
            r._Category = new Relation<Category>(GetElementCategoryIndex(n), GetCategory);
            r._Workset = new Relation<Workset>(GetElementWorksetIndex(n), GetWorkset);
            r._DesignOption = new Relation<DesignOption>(GetElementDesignOptionIndex(n), GetDesignOption);
            r._OwnerView = new Relation<View>(GetElementOwnerViewIndex(n), GetView);
            r._Group = new Relation<Group>(GetElementGroupIndex(n), GetGroup);
            r._AssemblyInstance = new Relation<AssemblyInstance>(GetElementAssemblyInstanceIndex(n), GetAssemblyInstance);
            r._BimDocument = new Relation<BimDocument>(GetElementBimDocumentIndex(n), GetBimDocument);
            r._Room = new Relation<Room>(GetElementRoomIndex(n), GetRoom);
            return r;
        }
        
        
        // Workset
        
        public EntityTable WorksetEntityTable { get; }
        
        public IArray<Int32> WorksetId { get; }
        public Int32 GetWorksetId(int index, Int32 defaultValue = default) => WorksetId?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> WorksetName { get; }
        public String GetWorksetName(int index, String defaultValue = "") => WorksetName?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> WorksetKind { get; }
        public String GetWorksetKind(int index, String defaultValue = "") => WorksetKind?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Boolean> WorksetIsOpen { get; }
        public Boolean GetWorksetIsOpen(int index, Boolean defaultValue = default) => WorksetIsOpen?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Boolean> WorksetIsEditable { get; }
        public Boolean GetWorksetIsEditable(int index, Boolean defaultValue = default) => WorksetIsEditable?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> WorksetOwner { get; }
        public String GetWorksetOwner(int index, String defaultValue = "") => WorksetOwner?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> WorksetUniqueId { get; }
        public String GetWorksetUniqueId(int index, String defaultValue = "") => WorksetUniqueId?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> WorksetBimDocumentIndex { get; }
        public int GetWorksetBimDocumentIndex(int index) => WorksetBimDocumentIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumWorkset => WorksetEntityTable?.NumRows ?? 0;
        public IArray<Workset> WorksetList { get; }
        public Workset GetWorkset(int n)
        {
            if (n < 0) return null;
            var r = new Workset();
            r.Document = Document;
            r.Index = n;
            r.Id = WorksetId.ElementAtOrDefault(n);
            r.Name = WorksetName.ElementAtOrDefault(n);
            r.Kind = WorksetKind.ElementAtOrDefault(n);
            r.IsOpen = WorksetIsOpen.ElementAtOrDefault(n);
            r.IsEditable = WorksetIsEditable.ElementAtOrDefault(n);
            r.Owner = WorksetOwner.ElementAtOrDefault(n);
            r.UniqueId = WorksetUniqueId.ElementAtOrDefault(n);
            r._BimDocument = new Relation<BimDocument>(GetWorksetBimDocumentIndex(n), GetBimDocument);
            return r;
        }
        
        
        // AssemblyInstance
        
        public EntityTable AssemblyInstanceEntityTable { get; }
        
        public IArray<String> AssemblyInstanceAssemblyTypeName { get; }
        public String GetAssemblyInstanceAssemblyTypeName(int index, String defaultValue = "") => AssemblyInstanceAssemblyTypeName?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Vector3> AssemblyInstancePosition { get; }
        public Vector3 GetAssemblyInstancePosition(int index, Vector3 defaultValue = default) => AssemblyInstancePosition?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> AssemblyInstanceElementIndex { get; }
        public int GetAssemblyInstanceElementIndex(int index) => AssemblyInstanceElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumAssemblyInstance => AssemblyInstanceEntityTable?.NumRows ?? 0;
        public IArray<AssemblyInstance> AssemblyInstanceList { get; }
        public AssemblyInstance GetAssemblyInstance(int n)
        {
            if (n < 0) return null;
            var r = new AssemblyInstance();
            r.Document = Document;
            r.Index = n;
            r.AssemblyTypeName = AssemblyInstanceAssemblyTypeName.ElementAtOrDefault(n);
            r.Position = AssemblyInstancePosition.ElementAtOrDefault(n);
            r._Element = new Relation<Element>(GetAssemblyInstanceElementIndex(n), GetElement);
            return r;
        }
        
        
        // Group
        
        public EntityTable GroupEntityTable { get; }
        
        public IArray<String> GroupGroupType { get; }
        public String GetGroupGroupType(int index, String defaultValue = "") => GroupGroupType?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Vector3> GroupPosition { get; }
        public Vector3 GetGroupPosition(int index, Vector3 defaultValue = default) => GroupPosition?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> GroupElementIndex { get; }
        public int GetGroupElementIndex(int index) => GroupElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumGroup => GroupEntityTable?.NumRows ?? 0;
        public IArray<Group> GroupList { get; }
        public Group GetGroup(int n)
        {
            if (n < 0) return null;
            var r = new Group();
            r.Document = Document;
            r.Index = n;
            r.GroupType = GroupGroupType.ElementAtOrDefault(n);
            r.Position = GroupPosition.ElementAtOrDefault(n);
            r._Element = new Relation<Element>(GetGroupElementIndex(n), GetElement);
            return r;
        }
        
        
        // DesignOption
        
        public EntityTable DesignOptionEntityTable { get; }
        
        public IArray<Boolean> DesignOptionIsPrimary { get; }
        public Boolean GetDesignOptionIsPrimary(int index, Boolean defaultValue = default) => DesignOptionIsPrimary?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> DesignOptionElementIndex { get; }
        public int GetDesignOptionElementIndex(int index) => DesignOptionElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumDesignOption => DesignOptionEntityTable?.NumRows ?? 0;
        public IArray<DesignOption> DesignOptionList { get; }
        public DesignOption GetDesignOption(int n)
        {
            if (n < 0) return null;
            var r = new DesignOption();
            r.Document = Document;
            r.Index = n;
            r.IsPrimary = DesignOptionIsPrimary.ElementAtOrDefault(n);
            r._Element = new Relation<Element>(GetDesignOptionElementIndex(n), GetElement);
            return r;
        }
        
        
        // Level
        
        public EntityTable LevelEntityTable { get; }
        
        public IArray<Double> LevelElevation { get; }
        public Double GetLevelElevation(int index, Double defaultValue = default) => LevelElevation?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> LevelFamilyTypeIndex { get; }
        public int GetLevelFamilyTypeIndex(int index) => LevelFamilyTypeIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> LevelElementIndex { get; }
        public int GetLevelElementIndex(int index) => LevelElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumLevel => LevelEntityTable?.NumRows ?? 0;
        public IArray<Level> LevelList { get; }
        public Level GetLevel(int n)
        {
            if (n < 0) return null;
            var r = new Level();
            r.Document = Document;
            r.Index = n;
            r.Elevation = LevelElevation.ElementAtOrDefault(n);
            r._FamilyType = new Relation<FamilyType>(GetLevelFamilyTypeIndex(n), GetFamilyType);
            r._Element = new Relation<Element>(GetLevelElementIndex(n), GetElement);
            return r;
        }
        
        
        // Phase
        
        public EntityTable PhaseEntityTable { get; }
        
        public IArray<int> PhaseElementIndex { get; }
        public int GetPhaseElementIndex(int index) => PhaseElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumPhase => PhaseEntityTable?.NumRows ?? 0;
        public IArray<Phase> PhaseList { get; }
        public Phase GetPhase(int n)
        {
            if (n < 0) return null;
            var r = new Phase();
            r.Document = Document;
            r.Index = n;
            r._Element = new Relation<Element>(GetPhaseElementIndex(n), GetElement);
            return r;
        }
        
        
        // Room
        
        public EntityTable RoomEntityTable { get; }
        
        public IArray<Double> RoomBaseOffset { get; }
        public Double GetRoomBaseOffset(int index, Double defaultValue = default) => RoomBaseOffset?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> RoomLimitOffset { get; }
        public Double GetRoomLimitOffset(int index, Double defaultValue = default) => RoomLimitOffset?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> RoomUnboundedHeight { get; }
        public Double GetRoomUnboundedHeight(int index, Double defaultValue = default) => RoomUnboundedHeight?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> RoomVolume { get; }
        public Double GetRoomVolume(int index, Double defaultValue = default) => RoomVolume?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> RoomPerimeter { get; }
        public Double GetRoomPerimeter(int index, Double defaultValue = default) => RoomPerimeter?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> RoomArea { get; }
        public Double GetRoomArea(int index, Double defaultValue = default) => RoomArea?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> RoomNumber { get; }
        public String GetRoomNumber(int index, String defaultValue = "") => RoomNumber?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> RoomUpperLimitIndex { get; }
        public int GetRoomUpperLimitIndex(int index) => RoomUpperLimitIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> RoomElementIndex { get; }
        public int GetRoomElementIndex(int index) => RoomElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumRoom => RoomEntityTable?.NumRows ?? 0;
        public IArray<Room> RoomList { get; }
        public Room GetRoom(int n)
        {
            if (n < 0) return null;
            var r = new Room();
            r.Document = Document;
            r.Index = n;
            r.BaseOffset = RoomBaseOffset.ElementAtOrDefault(n);
            r.LimitOffset = RoomLimitOffset.ElementAtOrDefault(n);
            r.UnboundedHeight = RoomUnboundedHeight.ElementAtOrDefault(n);
            r.Volume = RoomVolume.ElementAtOrDefault(n);
            r.Perimeter = RoomPerimeter.ElementAtOrDefault(n);
            r.Area = RoomArea.ElementAtOrDefault(n);
            r.Number = RoomNumber.ElementAtOrDefault(n);
            r._UpperLimit = new Relation<Level>(GetRoomUpperLimitIndex(n), GetLevel);
            r._Element = new Relation<Element>(GetRoomElementIndex(n), GetElement);
            return r;
        }
        
        
        // BimDocument
        
        public EntityTable BimDocumentEntityTable { get; }
        
        public IArray<String> BimDocumentTitle { get; }
        public String GetBimDocumentTitle(int index, String defaultValue = "") => BimDocumentTitle?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Boolean> BimDocumentIsMetric { get; }
        public Boolean GetBimDocumentIsMetric(int index, Boolean defaultValue = default) => BimDocumentIsMetric?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> BimDocumentGuid { get; }
        public String GetBimDocumentGuid(int index, String defaultValue = "") => BimDocumentGuid?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Int32> BimDocumentNumSaves { get; }
        public Int32 GetBimDocumentNumSaves(int index, Int32 defaultValue = default) => BimDocumentNumSaves?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Boolean> BimDocumentIsLinked { get; }
        public Boolean GetBimDocumentIsLinked(int index, Boolean defaultValue = default) => BimDocumentIsLinked?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Boolean> BimDocumentIsDetached { get; }
        public Boolean GetBimDocumentIsDetached(int index, Boolean defaultValue = default) => BimDocumentIsDetached?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Boolean> BimDocumentIsWorkshared { get; }
        public Boolean GetBimDocumentIsWorkshared(int index, Boolean defaultValue = default) => BimDocumentIsWorkshared?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> BimDocumentPathName { get; }
        public String GetBimDocumentPathName(int index, String defaultValue = "") => BimDocumentPathName?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> BimDocumentLatitude { get; }
        public Double GetBimDocumentLatitude(int index, Double defaultValue = default) => BimDocumentLatitude?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> BimDocumentLongitude { get; }
        public Double GetBimDocumentLongitude(int index, Double defaultValue = default) => BimDocumentLongitude?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> BimDocumentTimeZone { get; }
        public Double GetBimDocumentTimeZone(int index, Double defaultValue = default) => BimDocumentTimeZone?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> BimDocumentPlaceName { get; }
        public String GetBimDocumentPlaceName(int index, String defaultValue = "") => BimDocumentPlaceName?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> BimDocumentWeatherStationName { get; }
        public String GetBimDocumentWeatherStationName(int index, String defaultValue = "") => BimDocumentWeatherStationName?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> BimDocumentElevation { get; }
        public Double GetBimDocumentElevation(int index, Double defaultValue = default) => BimDocumentElevation?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> BimDocumentProjectLocation { get; }
        public String GetBimDocumentProjectLocation(int index, String defaultValue = "") => BimDocumentProjectLocation?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> BimDocumentIssueDate { get; }
        public String GetBimDocumentIssueDate(int index, String defaultValue = "") => BimDocumentIssueDate?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> BimDocumentStatus { get; }
        public String GetBimDocumentStatus(int index, String defaultValue = "") => BimDocumentStatus?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> BimDocumentClientName { get; }
        public String GetBimDocumentClientName(int index, String defaultValue = "") => BimDocumentClientName?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> BimDocumentAddress { get; }
        public String GetBimDocumentAddress(int index, String defaultValue = "") => BimDocumentAddress?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> BimDocumentName { get; }
        public String GetBimDocumentName(int index, String defaultValue = "") => BimDocumentName?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> BimDocumentNumber { get; }
        public String GetBimDocumentNumber(int index, String defaultValue = "") => BimDocumentNumber?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> BimDocumentAuthor { get; }
        public String GetBimDocumentAuthor(int index, String defaultValue = "") => BimDocumentAuthor?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> BimDocumentBuildingName { get; }
        public String GetBimDocumentBuildingName(int index, String defaultValue = "") => BimDocumentBuildingName?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> BimDocumentOrganizationName { get; }
        public String GetBimDocumentOrganizationName(int index, String defaultValue = "") => BimDocumentOrganizationName?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> BimDocumentOrganizationDescription { get; }
        public String GetBimDocumentOrganizationDescription(int index, String defaultValue = "") => BimDocumentOrganizationDescription?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> BimDocumentProduct { get; }
        public String GetBimDocumentProduct(int index, String defaultValue = "") => BimDocumentProduct?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> BimDocumentVersion { get; }
        public String GetBimDocumentVersion(int index, String defaultValue = "") => BimDocumentVersion?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> BimDocumentUser { get; }
        public String GetBimDocumentUser(int index, String defaultValue = "") => BimDocumentUser?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> BimDocumentActiveViewIndex { get; }
        public int GetBimDocumentActiveViewIndex(int index) => BimDocumentActiveViewIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> BimDocumentOwnerFamilyIndex { get; }
        public int GetBimDocumentOwnerFamilyIndex(int index) => BimDocumentOwnerFamilyIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> BimDocumentParentIndex { get; }
        public int GetBimDocumentParentIndex(int index) => BimDocumentParentIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> BimDocumentElementIndex { get; }
        public int GetBimDocumentElementIndex(int index) => BimDocumentElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumBimDocument => BimDocumentEntityTable?.NumRows ?? 0;
        public IArray<BimDocument> BimDocumentList { get; }
        public BimDocument GetBimDocument(int n)
        {
            if (n < 0) return null;
            var r = new BimDocument();
            r.Document = Document;
            r.Index = n;
            r.Title = BimDocumentTitle.ElementAtOrDefault(n);
            r.IsMetric = BimDocumentIsMetric.ElementAtOrDefault(n);
            r.Guid = BimDocumentGuid.ElementAtOrDefault(n);
            r.NumSaves = BimDocumentNumSaves.ElementAtOrDefault(n);
            r.IsLinked = BimDocumentIsLinked.ElementAtOrDefault(n);
            r.IsDetached = BimDocumentIsDetached.ElementAtOrDefault(n);
            r.IsWorkshared = BimDocumentIsWorkshared.ElementAtOrDefault(n);
            r.PathName = BimDocumentPathName.ElementAtOrDefault(n);
            r.Latitude = BimDocumentLatitude.ElementAtOrDefault(n);
            r.Longitude = BimDocumentLongitude.ElementAtOrDefault(n);
            r.TimeZone = BimDocumentTimeZone.ElementAtOrDefault(n);
            r.PlaceName = BimDocumentPlaceName.ElementAtOrDefault(n);
            r.WeatherStationName = BimDocumentWeatherStationName.ElementAtOrDefault(n);
            r.Elevation = BimDocumentElevation.ElementAtOrDefault(n);
            r.ProjectLocation = BimDocumentProjectLocation.ElementAtOrDefault(n);
            r.IssueDate = BimDocumentIssueDate.ElementAtOrDefault(n);
            r.Status = BimDocumentStatus.ElementAtOrDefault(n);
            r.ClientName = BimDocumentClientName.ElementAtOrDefault(n);
            r.Address = BimDocumentAddress.ElementAtOrDefault(n);
            r.Name = BimDocumentName.ElementAtOrDefault(n);
            r.Number = BimDocumentNumber.ElementAtOrDefault(n);
            r.Author = BimDocumentAuthor.ElementAtOrDefault(n);
            r.BuildingName = BimDocumentBuildingName.ElementAtOrDefault(n);
            r.OrganizationName = BimDocumentOrganizationName.ElementAtOrDefault(n);
            r.OrganizationDescription = BimDocumentOrganizationDescription.ElementAtOrDefault(n);
            r.Product = BimDocumentProduct.ElementAtOrDefault(n);
            r.Version = BimDocumentVersion.ElementAtOrDefault(n);
            r.User = BimDocumentUser.ElementAtOrDefault(n);
            r._ActiveView = new Relation<View>(GetBimDocumentActiveViewIndex(n), GetView);
            r._OwnerFamily = new Relation<Family>(GetBimDocumentOwnerFamilyIndex(n), GetFamily);
            r._Parent = new Relation<BimDocument>(GetBimDocumentParentIndex(n), GetBimDocument);
            r._Element = new Relation<Element>(GetBimDocumentElementIndex(n), GetElement);
            return r;
        }
        
        
        // DisplayUnitInBimDocument
        
        public EntityTable DisplayUnitInBimDocumentEntityTable { get; }
        
        public IArray<int> DisplayUnitInBimDocumentDisplayUnitIndex { get; }
        public int GetDisplayUnitInBimDocumentDisplayUnitIndex(int index) => DisplayUnitInBimDocumentDisplayUnitIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> DisplayUnitInBimDocumentBimDocumentIndex { get; }
        public int GetDisplayUnitInBimDocumentBimDocumentIndex(int index) => DisplayUnitInBimDocumentBimDocumentIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumDisplayUnitInBimDocument => DisplayUnitInBimDocumentEntityTable?.NumRows ?? 0;
        public IArray<DisplayUnitInBimDocument> DisplayUnitInBimDocumentList { get; }
        public DisplayUnitInBimDocument GetDisplayUnitInBimDocument(int n)
        {
            if (n < 0) return null;
            var r = new DisplayUnitInBimDocument();
            r.Document = Document;
            r.Index = n;
            r._DisplayUnit = new Relation<DisplayUnit>(GetDisplayUnitInBimDocumentDisplayUnitIndex(n), GetDisplayUnit);
            r._BimDocument = new Relation<BimDocument>(GetDisplayUnitInBimDocumentBimDocumentIndex(n), GetBimDocument);
            return r;
        }
        
        
        // PhaseOrderInBimDocument
        
        public EntityTable PhaseOrderInBimDocumentEntityTable { get; }
        
        public IArray<Int32> PhaseOrderInBimDocumentOrderIndex { get; }
        public Int32 GetPhaseOrderInBimDocumentOrderIndex(int index, Int32 defaultValue = default) => PhaseOrderInBimDocumentOrderIndex?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> PhaseOrderInBimDocumentPhaseIndex { get; }
        public int GetPhaseOrderInBimDocumentPhaseIndex(int index) => PhaseOrderInBimDocumentPhaseIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> PhaseOrderInBimDocumentBimDocumentIndex { get; }
        public int GetPhaseOrderInBimDocumentBimDocumentIndex(int index) => PhaseOrderInBimDocumentBimDocumentIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumPhaseOrderInBimDocument => PhaseOrderInBimDocumentEntityTable?.NumRows ?? 0;
        public IArray<PhaseOrderInBimDocument> PhaseOrderInBimDocumentList { get; }
        public PhaseOrderInBimDocument GetPhaseOrderInBimDocument(int n)
        {
            if (n < 0) return null;
            var r = new PhaseOrderInBimDocument();
            r.Document = Document;
            r.Index = n;
            r.OrderIndex = PhaseOrderInBimDocumentOrderIndex.ElementAtOrDefault(n);
            r._Phase = new Relation<Phase>(GetPhaseOrderInBimDocumentPhaseIndex(n), GetPhase);
            r._BimDocument = new Relation<BimDocument>(GetPhaseOrderInBimDocumentBimDocumentIndex(n), GetBimDocument);
            return r;
        }
        
        
        // Category
        
        public EntityTable CategoryEntityTable { get; }
        
        public IArray<String> CategoryName { get; }
        public String GetCategoryName(int index, String defaultValue = "") => CategoryName?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Int32> CategoryId { get; }
        public Int32 GetCategoryId(int index, Int32 defaultValue = default) => CategoryId?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> CategoryCategoryType { get; }
        public String GetCategoryCategoryType(int index, String defaultValue = "") => CategoryCategoryType?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<DVector3> CategoryLineColor { get; }
        public DVector3 GetCategoryLineColor(int index, DVector3 defaultValue = default) => CategoryLineColor?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> CategoryBuiltInCategory { get; }
        public String GetCategoryBuiltInCategory(int index, String defaultValue = "") => CategoryBuiltInCategory?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> CategoryParentIndex { get; }
        public int GetCategoryParentIndex(int index) => CategoryParentIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> CategoryMaterialIndex { get; }
        public int GetCategoryMaterialIndex(int index) => CategoryMaterialIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumCategory => CategoryEntityTable?.NumRows ?? 0;
        public IArray<Category> CategoryList { get; }
        public Category GetCategory(int n)
        {
            if (n < 0) return null;
            var r = new Category();
            r.Document = Document;
            r.Index = n;
            r.Name = CategoryName.ElementAtOrDefault(n);
            r.Id = CategoryId.ElementAtOrDefault(n);
            r.CategoryType = CategoryCategoryType.ElementAtOrDefault(n);
            r.LineColor = CategoryLineColor.ElementAtOrDefault(n);
            r.BuiltInCategory = CategoryBuiltInCategory.ElementAtOrDefault(n);
            r._Parent = new Relation<Category>(GetCategoryParentIndex(n), GetCategory);
            r._Material = new Relation<Material>(GetCategoryMaterialIndex(n), GetMaterial);
            return r;
        }
        
        
        // Family
        
        public EntityTable FamilyEntityTable { get; }
        
        public IArray<String> FamilyStructuralMaterialType { get; }
        public String GetFamilyStructuralMaterialType(int index, String defaultValue = "") => FamilyStructuralMaterialType?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> FamilyStructuralSectionShape { get; }
        public String GetFamilyStructuralSectionShape(int index, String defaultValue = "") => FamilyStructuralSectionShape?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Boolean> FamilyIsSystemFamily { get; }
        public Boolean GetFamilyIsSystemFamily(int index, Boolean defaultValue = default) => FamilyIsSystemFamily?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Boolean> FamilyIsInPlace { get; }
        public Boolean GetFamilyIsInPlace(int index, Boolean defaultValue = default) => FamilyIsInPlace?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> FamilyFamilyCategoryIndex { get; }
        public int GetFamilyFamilyCategoryIndex(int index) => FamilyFamilyCategoryIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> FamilyElementIndex { get; }
        public int GetFamilyElementIndex(int index) => FamilyElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumFamily => FamilyEntityTable?.NumRows ?? 0;
        public IArray<Family> FamilyList { get; }
        public Family GetFamily(int n)
        {
            if (n < 0) return null;
            var r = new Family();
            r.Document = Document;
            r.Index = n;
            r.StructuralMaterialType = FamilyStructuralMaterialType.ElementAtOrDefault(n);
            r.StructuralSectionShape = FamilyStructuralSectionShape.ElementAtOrDefault(n);
            r.IsSystemFamily = FamilyIsSystemFamily.ElementAtOrDefault(n);
            r.IsInPlace = FamilyIsInPlace.ElementAtOrDefault(n);
            r._FamilyCategory = new Relation<Category>(GetFamilyFamilyCategoryIndex(n), GetCategory);
            r._Element = new Relation<Element>(GetFamilyElementIndex(n), GetElement);
            return r;
        }
        
        
        // FamilyType
        
        public EntityTable FamilyTypeEntityTable { get; }
        
        public IArray<Boolean> FamilyTypeIsSystemFamilyType { get; }
        public Boolean GetFamilyTypeIsSystemFamilyType(int index, Boolean defaultValue = default) => FamilyTypeIsSystemFamilyType?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> FamilyTypeFamilyIndex { get; }
        public int GetFamilyTypeFamilyIndex(int index) => FamilyTypeFamilyIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> FamilyTypeCompoundStructureIndex { get; }
        public int GetFamilyTypeCompoundStructureIndex(int index) => FamilyTypeCompoundStructureIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> FamilyTypeElementIndex { get; }
        public int GetFamilyTypeElementIndex(int index) => FamilyTypeElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumFamilyType => FamilyTypeEntityTable?.NumRows ?? 0;
        public IArray<FamilyType> FamilyTypeList { get; }
        public FamilyType GetFamilyType(int n)
        {
            if (n < 0) return null;
            var r = new FamilyType();
            r.Document = Document;
            r.Index = n;
            r.IsSystemFamilyType = FamilyTypeIsSystemFamilyType.ElementAtOrDefault(n);
            r._Family = new Relation<Family>(GetFamilyTypeFamilyIndex(n), GetFamily);
            r._CompoundStructure = new Relation<CompoundStructure>(GetFamilyTypeCompoundStructureIndex(n), GetCompoundStructure);
            r._Element = new Relation<Element>(GetFamilyTypeElementIndex(n), GetElement);
            return r;
        }
        
        
        // FamilyInstance
        
        public EntityTable FamilyInstanceEntityTable { get; }
        
        public IArray<Boolean> FamilyInstanceFacingFlipped { get; }
        public Boolean GetFamilyInstanceFacingFlipped(int index, Boolean defaultValue = default) => FamilyInstanceFacingFlipped?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Vector3> FamilyInstanceFacingOrientation { get; }
        public Vector3 GetFamilyInstanceFacingOrientation(int index, Vector3 defaultValue = default) => FamilyInstanceFacingOrientation?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Boolean> FamilyInstanceHandFlipped { get; }
        public Boolean GetFamilyInstanceHandFlipped(int index, Boolean defaultValue = default) => FamilyInstanceHandFlipped?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Boolean> FamilyInstanceMirrored { get; }
        public Boolean GetFamilyInstanceMirrored(int index, Boolean defaultValue = default) => FamilyInstanceMirrored?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Boolean> FamilyInstanceHasModifiedGeometry { get; }
        public Boolean GetFamilyInstanceHasModifiedGeometry(int index, Boolean defaultValue = default) => FamilyInstanceHasModifiedGeometry?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> FamilyInstanceScale { get; }
        public Single GetFamilyInstanceScale(int index, Single defaultValue = default) => FamilyInstanceScale?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Vector3> FamilyInstanceBasisX { get; }
        public Vector3 GetFamilyInstanceBasisX(int index, Vector3 defaultValue = default) => FamilyInstanceBasisX?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Vector3> FamilyInstanceBasisY { get; }
        public Vector3 GetFamilyInstanceBasisY(int index, Vector3 defaultValue = default) => FamilyInstanceBasisY?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Vector3> FamilyInstanceBasisZ { get; }
        public Vector3 GetFamilyInstanceBasisZ(int index, Vector3 defaultValue = default) => FamilyInstanceBasisZ?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Vector3> FamilyInstanceTranslation { get; }
        public Vector3 GetFamilyInstanceTranslation(int index, Vector3 defaultValue = default) => FamilyInstanceTranslation?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Vector3> FamilyInstanceHandOrientation { get; }
        public Vector3 GetFamilyInstanceHandOrientation(int index, Vector3 defaultValue = default) => FamilyInstanceHandOrientation?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> FamilyInstanceFamilyTypeIndex { get; }
        public int GetFamilyInstanceFamilyTypeIndex(int index) => FamilyInstanceFamilyTypeIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> FamilyInstanceHostIndex { get; }
        public int GetFamilyInstanceHostIndex(int index) => FamilyInstanceHostIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> FamilyInstanceFromRoomIndex { get; }
        public int GetFamilyInstanceFromRoomIndex(int index) => FamilyInstanceFromRoomIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> FamilyInstanceToRoomIndex { get; }
        public int GetFamilyInstanceToRoomIndex(int index) => FamilyInstanceToRoomIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> FamilyInstanceElementIndex { get; }
        public int GetFamilyInstanceElementIndex(int index) => FamilyInstanceElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumFamilyInstance => FamilyInstanceEntityTable?.NumRows ?? 0;
        public IArray<FamilyInstance> FamilyInstanceList { get; }
        public FamilyInstance GetFamilyInstance(int n)
        {
            if (n < 0) return null;
            var r = new FamilyInstance();
            r.Document = Document;
            r.Index = n;
            r.FacingFlipped = FamilyInstanceFacingFlipped.ElementAtOrDefault(n);
            r.FacingOrientation = FamilyInstanceFacingOrientation.ElementAtOrDefault(n);
            r.HandFlipped = FamilyInstanceHandFlipped.ElementAtOrDefault(n);
            r.Mirrored = FamilyInstanceMirrored.ElementAtOrDefault(n);
            r.HasModifiedGeometry = FamilyInstanceHasModifiedGeometry.ElementAtOrDefault(n);
            r.Scale = FamilyInstanceScale.ElementAtOrDefault(n);
            r.BasisX = FamilyInstanceBasisX.ElementAtOrDefault(n);
            r.BasisY = FamilyInstanceBasisY.ElementAtOrDefault(n);
            r.BasisZ = FamilyInstanceBasisZ.ElementAtOrDefault(n);
            r.Translation = FamilyInstanceTranslation.ElementAtOrDefault(n);
            r.HandOrientation = FamilyInstanceHandOrientation.ElementAtOrDefault(n);
            r._FamilyType = new Relation<FamilyType>(GetFamilyInstanceFamilyTypeIndex(n), GetFamilyType);
            r._Host = new Relation<Element>(GetFamilyInstanceHostIndex(n), GetElement);
            r._FromRoom = new Relation<Room>(GetFamilyInstanceFromRoomIndex(n), GetRoom);
            r._ToRoom = new Relation<Room>(GetFamilyInstanceToRoomIndex(n), GetRoom);
            r._Element = new Relation<Element>(GetFamilyInstanceElementIndex(n), GetElement);
            return r;
        }
        
        
        // View
        
        public EntityTable ViewEntityTable { get; }
        
        public IArray<String> ViewTitle { get; }
        public String GetViewTitle(int index, String defaultValue = "") => ViewTitle?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> ViewViewType { get; }
        public String GetViewViewType(int index, String defaultValue = "") => ViewViewType?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<DVector3> ViewUp { get; }
        public DVector3 GetViewUp(int index, DVector3 defaultValue = default) => ViewUp?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<DVector3> ViewRight { get; }
        public DVector3 GetViewRight(int index, DVector3 defaultValue = default) => ViewRight?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<DVector3> ViewOrigin { get; }
        public DVector3 GetViewOrigin(int index, DVector3 defaultValue = default) => ViewOrigin?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<DVector3> ViewViewDirection { get; }
        public DVector3 GetViewViewDirection(int index, DVector3 defaultValue = default) => ViewViewDirection?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<DVector3> ViewViewPosition { get; }
        public DVector3 GetViewViewPosition(int index, DVector3 defaultValue = default) => ViewViewPosition?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> ViewScale { get; }
        public Double GetViewScale(int index, Double defaultValue = default) => ViewScale?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<DAABox2D> ViewOutline { get; }
        public DAABox2D GetViewOutline(int index, DAABox2D defaultValue = default) => ViewOutline?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Int32> ViewDetailLevel { get; }
        public Int32 GetViewDetailLevel(int index, Int32 defaultValue = default) => ViewDetailLevel?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> ViewCameraIndex { get; }
        public int GetViewCameraIndex(int index) => ViewCameraIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> ViewFamilyTypeIndex { get; }
        public int GetViewFamilyTypeIndex(int index) => ViewFamilyTypeIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> ViewElementIndex { get; }
        public int GetViewElementIndex(int index) => ViewElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumView => ViewEntityTable?.NumRows ?? 0;
        public IArray<View> ViewList { get; }
        public View GetView(int n)
        {
            if (n < 0) return null;
            var r = new View();
            r.Document = Document;
            r.Index = n;
            r.Title = ViewTitle.ElementAtOrDefault(n);
            r.ViewType = ViewViewType.ElementAtOrDefault(n);
            r.Up = ViewUp.ElementAtOrDefault(n);
            r.Right = ViewRight.ElementAtOrDefault(n);
            r.Origin = ViewOrigin.ElementAtOrDefault(n);
            r.ViewDirection = ViewViewDirection.ElementAtOrDefault(n);
            r.ViewPosition = ViewViewPosition.ElementAtOrDefault(n);
            r.Scale = ViewScale.ElementAtOrDefault(n);
            r.Outline = ViewOutline.ElementAtOrDefault(n);
            r.DetailLevel = ViewDetailLevel.ElementAtOrDefault(n);
            r._Camera = new Relation<Camera>(GetViewCameraIndex(n), GetCamera);
            r._FamilyType = new Relation<FamilyType>(GetViewFamilyTypeIndex(n), GetFamilyType);
            r._Element = new Relation<Element>(GetViewElementIndex(n), GetElement);
            return r;
        }
        
        
        // ElementInView
        
        public EntityTable ElementInViewEntityTable { get; }
        
        public IArray<int> ElementInViewViewIndex { get; }
        public int GetElementInViewViewIndex(int index) => ElementInViewViewIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> ElementInViewElementIndex { get; }
        public int GetElementInViewElementIndex(int index) => ElementInViewElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumElementInView => ElementInViewEntityTable?.NumRows ?? 0;
        public IArray<ElementInView> ElementInViewList { get; }
        public ElementInView GetElementInView(int n)
        {
            if (n < 0) return null;
            var r = new ElementInView();
            r.Document = Document;
            r.Index = n;
            r._View = new Relation<View>(GetElementInViewViewIndex(n), GetView);
            r._Element = new Relation<Element>(GetElementInViewElementIndex(n), GetElement);
            return r;
        }
        
        
        // ShapeInView
        
        public EntityTable ShapeInViewEntityTable { get; }
        
        public IArray<int> ShapeInViewShapeIndex { get; }
        public int GetShapeInViewShapeIndex(int index) => ShapeInViewShapeIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> ShapeInViewViewIndex { get; }
        public int GetShapeInViewViewIndex(int index) => ShapeInViewViewIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumShapeInView => ShapeInViewEntityTable?.NumRows ?? 0;
        public IArray<ShapeInView> ShapeInViewList { get; }
        public ShapeInView GetShapeInView(int n)
        {
            if (n < 0) return null;
            var r = new ShapeInView();
            r.Document = Document;
            r.Index = n;
            r._Shape = new Relation<Shape>(GetShapeInViewShapeIndex(n), GetShape);
            r._View = new Relation<View>(GetShapeInViewViewIndex(n), GetView);
            return r;
        }
        
        
        // AssetInView
        
        public EntityTable AssetInViewEntityTable { get; }
        
        public IArray<int> AssetInViewAssetIndex { get; }
        public int GetAssetInViewAssetIndex(int index) => AssetInViewAssetIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> AssetInViewViewIndex { get; }
        public int GetAssetInViewViewIndex(int index) => AssetInViewViewIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumAssetInView => AssetInViewEntityTable?.NumRows ?? 0;
        public IArray<AssetInView> AssetInViewList { get; }
        public AssetInView GetAssetInView(int n)
        {
            if (n < 0) return null;
            var r = new AssetInView();
            r.Document = Document;
            r.Index = n;
            r._Asset = new Relation<Asset>(GetAssetInViewAssetIndex(n), GetAsset);
            r._View = new Relation<View>(GetAssetInViewViewIndex(n), GetView);
            return r;
        }
        
        
        // LevelInView
        
        public EntityTable LevelInViewEntityTable { get; }
        
        public IArray<DAABox> LevelInViewExtents { get; }
        public DAABox GetLevelInViewExtents(int index, DAABox defaultValue = default) => LevelInViewExtents?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> LevelInViewLevelIndex { get; }
        public int GetLevelInViewLevelIndex(int index) => LevelInViewLevelIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> LevelInViewViewIndex { get; }
        public int GetLevelInViewViewIndex(int index) => LevelInViewViewIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumLevelInView => LevelInViewEntityTable?.NumRows ?? 0;
        public IArray<LevelInView> LevelInViewList { get; }
        public LevelInView GetLevelInView(int n)
        {
            if (n < 0) return null;
            var r = new LevelInView();
            r.Document = Document;
            r.Index = n;
            r.Extents = LevelInViewExtents.ElementAtOrDefault(n);
            r._Level = new Relation<Level>(GetLevelInViewLevelIndex(n), GetLevel);
            r._View = new Relation<View>(GetLevelInViewViewIndex(n), GetView);
            return r;
        }
        
        
        // Camera
        
        public EntityTable CameraEntityTable { get; }
        
        public IArray<Int32> CameraId { get; }
        public Int32 GetCameraId(int index, Int32 defaultValue = default) => CameraId?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Int32> CameraIsPerspective { get; }
        public Int32 GetCameraIsPerspective(int index, Int32 defaultValue = default) => CameraIsPerspective?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> CameraVerticalExtent { get; }
        public Double GetCameraVerticalExtent(int index, Double defaultValue = default) => CameraVerticalExtent?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> CameraHorizontalExtent { get; }
        public Double GetCameraHorizontalExtent(int index, Double defaultValue = default) => CameraHorizontalExtent?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> CameraFarDistance { get; }
        public Double GetCameraFarDistance(int index, Double defaultValue = default) => CameraFarDistance?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> CameraNearDistance { get; }
        public Double GetCameraNearDistance(int index, Double defaultValue = default) => CameraNearDistance?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> CameraTargetDistance { get; }
        public Double GetCameraTargetDistance(int index, Double defaultValue = default) => CameraTargetDistance?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> CameraRightOffset { get; }
        public Double GetCameraRightOffset(int index, Double defaultValue = default) => CameraRightOffset?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> CameraUpOffset { get; }
        public Double GetCameraUpOffset(int index, Double defaultValue = default) => CameraUpOffset?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public int NumCamera => CameraEntityTable?.NumRows ?? 0;
        public IArray<Camera> CameraList { get; }
        public Camera GetCamera(int n)
        {
            if (n < 0) return null;
            var r = new Camera();
            r.Document = Document;
            r.Index = n;
            r.Id = CameraId.ElementAtOrDefault(n);
            r.IsPerspective = CameraIsPerspective.ElementAtOrDefault(n);
            r.VerticalExtent = CameraVerticalExtent.ElementAtOrDefault(n);
            r.HorizontalExtent = CameraHorizontalExtent.ElementAtOrDefault(n);
            r.FarDistance = CameraFarDistance.ElementAtOrDefault(n);
            r.NearDistance = CameraNearDistance.ElementAtOrDefault(n);
            r.TargetDistance = CameraTargetDistance.ElementAtOrDefault(n);
            r.RightOffset = CameraRightOffset.ElementAtOrDefault(n);
            r.UpOffset = CameraUpOffset.ElementAtOrDefault(n);
            return r;
        }
        
        
        // Material
        
        public EntityTable MaterialEntityTable { get; }
        
        public IArray<String> MaterialName { get; }
        public String GetMaterialName(int index, String defaultValue = "") => MaterialName?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> MaterialMaterialCategory { get; }
        public String GetMaterialMaterialCategory(int index, String defaultValue = "") => MaterialMaterialCategory?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<DVector3> MaterialColor { get; }
        public DVector3 GetMaterialColor(int index, DVector3 defaultValue = default) => MaterialColor?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<DVector2> MaterialColorUvScaling { get; }
        public DVector2 GetMaterialColorUvScaling(int index, DVector2 defaultValue = default) => MaterialColorUvScaling?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<DVector2> MaterialColorUvOffset { get; }
        public DVector2 GetMaterialColorUvOffset(int index, DVector2 defaultValue = default) => MaterialColorUvOffset?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<DVector2> MaterialNormalUvScaling { get; }
        public DVector2 GetMaterialNormalUvScaling(int index, DVector2 defaultValue = default) => MaterialNormalUvScaling?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<DVector2> MaterialNormalUvOffset { get; }
        public DVector2 GetMaterialNormalUvOffset(int index, DVector2 defaultValue = default) => MaterialNormalUvOffset?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> MaterialNormalAmount { get; }
        public Double GetMaterialNormalAmount(int index, Double defaultValue = default) => MaterialNormalAmount?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> MaterialGlossiness { get; }
        public Double GetMaterialGlossiness(int index, Double defaultValue = default) => MaterialGlossiness?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> MaterialSmoothness { get; }
        public Double GetMaterialSmoothness(int index, Double defaultValue = default) => MaterialSmoothness?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> MaterialTransparency { get; }
        public Double GetMaterialTransparency(int index, Double defaultValue = default) => MaterialTransparency?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> MaterialColorTextureFileIndex { get; }
        public int GetMaterialColorTextureFileIndex(int index) => MaterialColorTextureFileIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> MaterialNormalTextureFileIndex { get; }
        public int GetMaterialNormalTextureFileIndex(int index) => MaterialNormalTextureFileIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> MaterialElementIndex { get; }
        public int GetMaterialElementIndex(int index) => MaterialElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumMaterial => MaterialEntityTable?.NumRows ?? 0;
        public IArray<Material> MaterialList { get; }
        public Material GetMaterial(int n)
        {
            if (n < 0) return null;
            var r = new Material();
            r.Document = Document;
            r.Index = n;
            r.Name = MaterialName.ElementAtOrDefault(n);
            r.MaterialCategory = MaterialMaterialCategory.ElementAtOrDefault(n);
            r.Color = MaterialColor.ElementAtOrDefault(n);
            r.ColorUvScaling = MaterialColorUvScaling.ElementAtOrDefault(n);
            r.ColorUvOffset = MaterialColorUvOffset.ElementAtOrDefault(n);
            r.NormalUvScaling = MaterialNormalUvScaling.ElementAtOrDefault(n);
            r.NormalUvOffset = MaterialNormalUvOffset.ElementAtOrDefault(n);
            r.NormalAmount = MaterialNormalAmount.ElementAtOrDefault(n);
            r.Glossiness = MaterialGlossiness.ElementAtOrDefault(n);
            r.Smoothness = MaterialSmoothness.ElementAtOrDefault(n);
            r.Transparency = MaterialTransparency.ElementAtOrDefault(n);
            r._ColorTextureFile = new Relation<Asset>(GetMaterialColorTextureFileIndex(n), GetAsset);
            r._NormalTextureFile = new Relation<Asset>(GetMaterialNormalTextureFileIndex(n), GetAsset);
            r._Element = new Relation<Element>(GetMaterialElementIndex(n), GetElement);
            return r;
        }
        
        
        // MaterialInElement
        
        public EntityTable MaterialInElementEntityTable { get; }
        
        public IArray<Double> MaterialInElementArea { get; }
        public Double GetMaterialInElementArea(int index, Double defaultValue = default) => MaterialInElementArea?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> MaterialInElementVolume { get; }
        public Double GetMaterialInElementVolume(int index, Double defaultValue = default) => MaterialInElementVolume?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Boolean> MaterialInElementIsPaint { get; }
        public Boolean GetMaterialInElementIsPaint(int index, Boolean defaultValue = default) => MaterialInElementIsPaint?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> MaterialInElementMaterialIndex { get; }
        public int GetMaterialInElementMaterialIndex(int index) => MaterialInElementMaterialIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> MaterialInElementElementIndex { get; }
        public int GetMaterialInElementElementIndex(int index) => MaterialInElementElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumMaterialInElement => MaterialInElementEntityTable?.NumRows ?? 0;
        public IArray<MaterialInElement> MaterialInElementList { get; }
        public MaterialInElement GetMaterialInElement(int n)
        {
            if (n < 0) return null;
            var r = new MaterialInElement();
            r.Document = Document;
            r.Index = n;
            r.Area = MaterialInElementArea.ElementAtOrDefault(n);
            r.Volume = MaterialInElementVolume.ElementAtOrDefault(n);
            r.IsPaint = MaterialInElementIsPaint.ElementAtOrDefault(n);
            r._Material = new Relation<Material>(GetMaterialInElementMaterialIndex(n), GetMaterial);
            r._Element = new Relation<Element>(GetMaterialInElementElementIndex(n), GetElement);
            return r;
        }
        
        
        // CompoundStructureLayer
        
        public EntityTable CompoundStructureLayerEntityTable { get; }
        
        public IArray<Int32> CompoundStructureLayerOrderIndex { get; }
        public Int32 GetCompoundStructureLayerOrderIndex(int index, Int32 defaultValue = default) => CompoundStructureLayerOrderIndex?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> CompoundStructureLayerWidth { get; }
        public Double GetCompoundStructureLayerWidth(int index, Double defaultValue = default) => CompoundStructureLayerWidth?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> CompoundStructureLayerMaterialFunctionAssignment { get; }
        public String GetCompoundStructureLayerMaterialFunctionAssignment(int index, String defaultValue = "") => CompoundStructureLayerMaterialFunctionAssignment?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> CompoundStructureLayerMaterialIndex { get; }
        public int GetCompoundStructureLayerMaterialIndex(int index) => CompoundStructureLayerMaterialIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> CompoundStructureLayerCompoundStructureIndex { get; }
        public int GetCompoundStructureLayerCompoundStructureIndex(int index) => CompoundStructureLayerCompoundStructureIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumCompoundStructureLayer => CompoundStructureLayerEntityTable?.NumRows ?? 0;
        public IArray<CompoundStructureLayer> CompoundStructureLayerList { get; }
        public CompoundStructureLayer GetCompoundStructureLayer(int n)
        {
            if (n < 0) return null;
            var r = new CompoundStructureLayer();
            r.Document = Document;
            r.Index = n;
            r.OrderIndex = CompoundStructureLayerOrderIndex.ElementAtOrDefault(n);
            r.Width = CompoundStructureLayerWidth.ElementAtOrDefault(n);
            r.MaterialFunctionAssignment = CompoundStructureLayerMaterialFunctionAssignment.ElementAtOrDefault(n);
            r._Material = new Relation<Material>(GetCompoundStructureLayerMaterialIndex(n), GetMaterial);
            r._CompoundStructure = new Relation<CompoundStructure>(GetCompoundStructureLayerCompoundStructureIndex(n), GetCompoundStructure);
            return r;
        }
        
        
        // CompoundStructure
        
        public EntityTable CompoundStructureEntityTable { get; }
        
        public IArray<Double> CompoundStructureWidth { get; }
        public Double GetCompoundStructureWidth(int index, Double defaultValue = default) => CompoundStructureWidth?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> CompoundStructureStructuralLayerIndex { get; }
        public int GetCompoundStructureStructuralLayerIndex(int index) => CompoundStructureStructuralLayerIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumCompoundStructure => CompoundStructureEntityTable?.NumRows ?? 0;
        public IArray<CompoundStructure> CompoundStructureList { get; }
        public CompoundStructure GetCompoundStructure(int n)
        {
            if (n < 0) return null;
            var r = new CompoundStructure();
            r.Document = Document;
            r.Index = n;
            r.Width = CompoundStructureWidth.ElementAtOrDefault(n);
            r._StructuralLayer = new Relation<CompoundStructureLayer>(GetCompoundStructureStructuralLayerIndex(n), GetCompoundStructureLayer);
            return r;
        }
        
        
        // Node
        
        public EntityTable NodeEntityTable { get; }
        
        public IArray<int> NodeElementIndex { get; }
        public int GetNodeElementIndex(int index) => NodeElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumNode => NodeEntityTable?.NumRows ?? 0;
        public IArray<Node> NodeList { get; }
        public Node GetNode(int n)
        {
            if (n < 0) return null;
            var r = new Node();
            r.Document = Document;
            r.Index = n;
            r._Element = new Relation<Element>(GetNodeElementIndex(n), GetElement);
            return r;
        }
        
        
        // Geometry
        
        public EntityTable GeometryEntityTable { get; }
        
        public IArray<AABox> GeometryBox { get; }
        public AABox GetGeometryBox(int index, AABox defaultValue = default) => GeometryBox?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Int32> GeometryVertexCount { get; }
        public Int32 GetGeometryVertexCount(int index, Int32 defaultValue = default) => GeometryVertexCount?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Int32> GeometryFaceCount { get; }
        public Int32 GetGeometryFaceCount(int index, Int32 defaultValue = default) => GeometryFaceCount?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public int NumGeometry => GeometryEntityTable?.NumRows ?? 0;
        public IArray<Geometry> GeometryList { get; }
        public Geometry GetGeometry(int n)
        {
            if (n < 0) return null;
            var r = new Geometry();
            r.Document = Document;
            r.Index = n;
            r.Box = GeometryBox.ElementAtOrDefault(n);
            r.VertexCount = GeometryVertexCount.ElementAtOrDefault(n);
            r.FaceCount = GeometryFaceCount.ElementAtOrDefault(n);
            return r;
        }
        
        
        // Shape
        
        public EntityTable ShapeEntityTable { get; }
        
        public IArray<int> ShapeElementIndex { get; }
        public int GetShapeElementIndex(int index) => ShapeElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumShape => ShapeEntityTable?.NumRows ?? 0;
        public IArray<Shape> ShapeList { get; }
        public Shape GetShape(int n)
        {
            if (n < 0) return null;
            var r = new Shape();
            r.Document = Document;
            r.Index = n;
            r._Element = new Relation<Element>(GetShapeElementIndex(n), GetElement);
            return r;
        }
        
        
        // ShapeCollection
        
        public EntityTable ShapeCollectionEntityTable { get; }
        
        public IArray<int> ShapeCollectionElementIndex { get; }
        public int GetShapeCollectionElementIndex(int index) => ShapeCollectionElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumShapeCollection => ShapeCollectionEntityTable?.NumRows ?? 0;
        public IArray<ShapeCollection> ShapeCollectionList { get; }
        public ShapeCollection GetShapeCollection(int n)
        {
            if (n < 0) return null;
            var r = new ShapeCollection();
            r.Document = Document;
            r.Index = n;
            r._Element = new Relation<Element>(GetShapeCollectionElementIndex(n), GetElement);
            return r;
        }
        
        
        // ShapeInShapeCollection
        
        public EntityTable ShapeInShapeCollectionEntityTable { get; }
        
        public IArray<int> ShapeInShapeCollectionShapeIndex { get; }
        public int GetShapeInShapeCollectionShapeIndex(int index) => ShapeInShapeCollectionShapeIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> ShapeInShapeCollectionShapeCollectionIndex { get; }
        public int GetShapeInShapeCollectionShapeCollectionIndex(int index) => ShapeInShapeCollectionShapeCollectionIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumShapeInShapeCollection => ShapeInShapeCollectionEntityTable?.NumRows ?? 0;
        public IArray<ShapeInShapeCollection> ShapeInShapeCollectionList { get; }
        public ShapeInShapeCollection GetShapeInShapeCollection(int n)
        {
            if (n < 0) return null;
            var r = new ShapeInShapeCollection();
            r.Document = Document;
            r.Index = n;
            r._Shape = new Relation<Shape>(GetShapeInShapeCollectionShapeIndex(n), GetShape);
            r._ShapeCollection = new Relation<ShapeCollection>(GetShapeInShapeCollectionShapeCollectionIndex(n), GetShapeCollection);
            return r;
        }
        
        
        // System
        
        public EntityTable SystemEntityTable { get; }
        
        public IArray<Int32> SystemSystemType { get; }
        public Int32 GetSystemSystemType(int index, Int32 defaultValue = default) => SystemSystemType?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> SystemFamilyTypeIndex { get; }
        public int GetSystemFamilyTypeIndex(int index) => SystemFamilyTypeIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> SystemElementIndex { get; }
        public int GetSystemElementIndex(int index) => SystemElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumSystem => SystemEntityTable?.NumRows ?? 0;
        public IArray<System> SystemList { get; }
        public System GetSystem(int n)
        {
            if (n < 0) return null;
            var r = new System();
            r.Document = Document;
            r.Index = n;
            r.SystemType = SystemSystemType.ElementAtOrDefault(n);
            r._FamilyType = new Relation<FamilyType>(GetSystemFamilyTypeIndex(n), GetFamilyType);
            r._Element = new Relation<Element>(GetSystemElementIndex(n), GetElement);
            return r;
        }
        
        
        // ElementInSystem
        
        public EntityTable ElementInSystemEntityTable { get; }
        
        public IArray<Int32> ElementInSystemRoles { get; }
        public Int32 GetElementInSystemRoles(int index, Int32 defaultValue = default) => ElementInSystemRoles?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> ElementInSystemSystemIndex { get; }
        public int GetElementInSystemSystemIndex(int index) => ElementInSystemSystemIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> ElementInSystemElementIndex { get; }
        public int GetElementInSystemElementIndex(int index) => ElementInSystemElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumElementInSystem => ElementInSystemEntityTable?.NumRows ?? 0;
        public IArray<ElementInSystem> ElementInSystemList { get; }
        public ElementInSystem GetElementInSystem(int n)
        {
            if (n < 0) return null;
            var r = new ElementInSystem();
            r.Document = Document;
            r.Index = n;
            r.Roles = ElementInSystemRoles.ElementAtOrDefault(n);
            r._System = new Relation<System>(GetElementInSystemSystemIndex(n), GetSystem);
            r._Element = new Relation<Element>(GetElementInSystemElementIndex(n), GetElement);
            return r;
        }
        
        
        // Warning
        
        public EntityTable WarningEntityTable { get; }
        
        public IArray<String> WarningGuid { get; }
        public String GetWarningGuid(int index, String defaultValue = "") => WarningGuid?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> WarningSeverity { get; }
        public String GetWarningSeverity(int index, String defaultValue = "") => WarningSeverity?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> WarningDescription { get; }
        public String GetWarningDescription(int index, String defaultValue = "") => WarningDescription?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> WarningBimDocumentIndex { get; }
        public int GetWarningBimDocumentIndex(int index) => WarningBimDocumentIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumWarning => WarningEntityTable?.NumRows ?? 0;
        public IArray<Warning> WarningList { get; }
        public Warning GetWarning(int n)
        {
            if (n < 0) return null;
            var r = new Warning();
            r.Document = Document;
            r.Index = n;
            r.Guid = WarningGuid.ElementAtOrDefault(n);
            r.Severity = WarningSeverity.ElementAtOrDefault(n);
            r.Description = WarningDescription.ElementAtOrDefault(n);
            r._BimDocument = new Relation<BimDocument>(GetWarningBimDocumentIndex(n), GetBimDocument);
            return r;
        }
        
        
        // ElementInWarning
        
        public EntityTable ElementInWarningEntityTable { get; }
        
        public IArray<int> ElementInWarningWarningIndex { get; }
        public int GetElementInWarningWarningIndex(int index) => ElementInWarningWarningIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> ElementInWarningElementIndex { get; }
        public int GetElementInWarningElementIndex(int index) => ElementInWarningElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumElementInWarning => ElementInWarningEntityTable?.NumRows ?? 0;
        public IArray<ElementInWarning> ElementInWarningList { get; }
        public ElementInWarning GetElementInWarning(int n)
        {
            if (n < 0) return null;
            var r = new ElementInWarning();
            r.Document = Document;
            r.Index = n;
            r._Warning = new Relation<Warning>(GetElementInWarningWarningIndex(n), GetWarning);
            r._Element = new Relation<Element>(GetElementInWarningElementIndex(n), GetElement);
            return r;
        }
        
        
        // BasePoint
        
        public EntityTable BasePointEntityTable { get; }
        
        public IArray<Boolean> BasePointIsSurveyPoint { get; }
        public Boolean GetBasePointIsSurveyPoint(int index, Boolean defaultValue = default) => BasePointIsSurveyPoint?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<DVector3> BasePointPosition { get; }
        public DVector3 GetBasePointPosition(int index, DVector3 defaultValue = default) => BasePointPosition?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<DVector3> BasePointSharedPosition { get; }
        public DVector3 GetBasePointSharedPosition(int index, DVector3 defaultValue = default) => BasePointSharedPosition?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> BasePointElementIndex { get; }
        public int GetBasePointElementIndex(int index) => BasePointElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumBasePoint => BasePointEntityTable?.NumRows ?? 0;
        public IArray<BasePoint> BasePointList { get; }
        public BasePoint GetBasePoint(int n)
        {
            if (n < 0) return null;
            var r = new BasePoint();
            r.Document = Document;
            r.Index = n;
            r.IsSurveyPoint = BasePointIsSurveyPoint.ElementAtOrDefault(n);
            r.Position = BasePointPosition.ElementAtOrDefault(n);
            r.SharedPosition = BasePointSharedPosition.ElementAtOrDefault(n);
            r._Element = new Relation<Element>(GetBasePointElementIndex(n), GetElement);
            return r;
        }
        
        
        // PhaseFilter
        
        public EntityTable PhaseFilterEntityTable { get; }
        
        public IArray<Int32> PhaseFilterNew { get; }
        public Int32 GetPhaseFilterNew(int index, Int32 defaultValue = default) => PhaseFilterNew?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Int32> PhaseFilterExisting { get; }
        public Int32 GetPhaseFilterExisting(int index, Int32 defaultValue = default) => PhaseFilterExisting?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Int32> PhaseFilterDemolished { get; }
        public Int32 GetPhaseFilterDemolished(int index, Int32 defaultValue = default) => PhaseFilterDemolished?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Int32> PhaseFilterTemporary { get; }
        public Int32 GetPhaseFilterTemporary(int index, Int32 defaultValue = default) => PhaseFilterTemporary?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> PhaseFilterElementIndex { get; }
        public int GetPhaseFilterElementIndex(int index) => PhaseFilterElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumPhaseFilter => PhaseFilterEntityTable?.NumRows ?? 0;
        public IArray<PhaseFilter> PhaseFilterList { get; }
        public PhaseFilter GetPhaseFilter(int n)
        {
            if (n < 0) return null;
            var r = new PhaseFilter();
            r.Document = Document;
            r.Index = n;
            r.New = PhaseFilterNew.ElementAtOrDefault(n);
            r.Existing = PhaseFilterExisting.ElementAtOrDefault(n);
            r.Demolished = PhaseFilterDemolished.ElementAtOrDefault(n);
            r.Temporary = PhaseFilterTemporary.ElementAtOrDefault(n);
            r._Element = new Relation<Element>(GetPhaseFilterElementIndex(n), GetElement);
            return r;
        }
        
        
        // Grid
        
        public EntityTable GridEntityTable { get; }
        
        public IArray<DVector3> GridStartPoint { get; }
        public DVector3 GetGridStartPoint(int index, DVector3 defaultValue = default) => GridStartPoint?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<DVector3> GridEndPoint { get; }
        public DVector3 GetGridEndPoint(int index, DVector3 defaultValue = default) => GridEndPoint?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Boolean> GridIsCurved { get; }
        public Boolean GetGridIsCurved(int index, Boolean defaultValue = default) => GridIsCurved?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<DAABox> GridExtents { get; }
        public DAABox GetGridExtents(int index, DAABox defaultValue = default) => GridExtents?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> GridFamilyTypeIndex { get; }
        public int GetGridFamilyTypeIndex(int index) => GridFamilyTypeIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> GridElementIndex { get; }
        public int GetGridElementIndex(int index) => GridElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumGrid => GridEntityTable?.NumRows ?? 0;
        public IArray<Grid> GridList { get; }
        public Grid GetGrid(int n)
        {
            if (n < 0) return null;
            var r = new Grid();
            r.Document = Document;
            r.Index = n;
            r.StartPoint = GridStartPoint.ElementAtOrDefault(n);
            r.EndPoint = GridEndPoint.ElementAtOrDefault(n);
            r.IsCurved = GridIsCurved.ElementAtOrDefault(n);
            r.Extents = GridExtents.ElementAtOrDefault(n);
            r._FamilyType = new Relation<FamilyType>(GetGridFamilyTypeIndex(n), GetFamilyType);
            r._Element = new Relation<Element>(GetGridElementIndex(n), GetElement);
            return r;
        }
        
        
        // Area
        
        public EntityTable AreaEntityTable { get; }
        
        public IArray<Double> AreaValue { get; }
        public Double GetAreaValue(int index, Double defaultValue = default) => AreaValue?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> AreaPerimeter { get; }
        public Double GetAreaPerimeter(int index, Double defaultValue = default) => AreaPerimeter?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> AreaNumber { get; }
        public String GetAreaNumber(int index, String defaultValue = "") => AreaNumber?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Boolean> AreaIsGrossInterior { get; }
        public Boolean GetAreaIsGrossInterior(int index, Boolean defaultValue = default) => AreaIsGrossInterior?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> AreaAreaSchemeIndex { get; }
        public int GetAreaAreaSchemeIndex(int index) => AreaAreaSchemeIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> AreaElementIndex { get; }
        public int GetAreaElementIndex(int index) => AreaElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumArea => AreaEntityTable?.NumRows ?? 0;
        public IArray<Area> AreaList { get; }
        public Area GetArea(int n)
        {
            if (n < 0) return null;
            var r = new Area();
            r.Document = Document;
            r.Index = n;
            r.Value = AreaValue.ElementAtOrDefault(n);
            r.Perimeter = AreaPerimeter.ElementAtOrDefault(n);
            r.Number = AreaNumber.ElementAtOrDefault(n);
            r.IsGrossInterior = AreaIsGrossInterior.ElementAtOrDefault(n);
            r._AreaScheme = new Relation<AreaScheme>(GetAreaAreaSchemeIndex(n), GetAreaScheme);
            r._Element = new Relation<Element>(GetAreaElementIndex(n), GetElement);
            return r;
        }
        
        
        // AreaScheme
        
        public EntityTable AreaSchemeEntityTable { get; }
        
        public IArray<Boolean> AreaSchemeIsGrossBuildingArea { get; }
        public Boolean GetAreaSchemeIsGrossBuildingArea(int index, Boolean defaultValue = default) => AreaSchemeIsGrossBuildingArea?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> AreaSchemeElementIndex { get; }
        public int GetAreaSchemeElementIndex(int index) => AreaSchemeElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumAreaScheme => AreaSchemeEntityTable?.NumRows ?? 0;
        public IArray<AreaScheme> AreaSchemeList { get; }
        public AreaScheme GetAreaScheme(int n)
        {
            if (n < 0) return null;
            var r = new AreaScheme();
            r.Document = Document;
            r.Index = n;
            r.IsGrossBuildingArea = AreaSchemeIsGrossBuildingArea.ElementAtOrDefault(n);
            r._Element = new Relation<Element>(GetAreaSchemeElementIndex(n), GetElement);
            return r;
        }
        
        
        // Schedule
        
        public EntityTable ScheduleEntityTable { get; }
        
        public IArray<int> ScheduleElementIndex { get; }
        public int GetScheduleElementIndex(int index) => ScheduleElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumSchedule => ScheduleEntityTable?.NumRows ?? 0;
        public IArray<Schedule> ScheduleList { get; }
        public Schedule GetSchedule(int n)
        {
            if (n < 0) return null;
            var r = new Schedule();
            r.Document = Document;
            r.Index = n;
            r._Element = new Relation<Element>(GetScheduleElementIndex(n), GetElement);
            return r;
        }
        
        
        // ScheduleColumn
        
        public EntityTable ScheduleColumnEntityTable { get; }
        
        public IArray<String> ScheduleColumnName { get; }
        public String GetScheduleColumnName(int index, String defaultValue = "") => ScheduleColumnName?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Int32> ScheduleColumnColumnIndex { get; }
        public Int32 GetScheduleColumnColumnIndex(int index, Int32 defaultValue = default) => ScheduleColumnColumnIndex?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> ScheduleColumnScheduleIndex { get; }
        public int GetScheduleColumnScheduleIndex(int index) => ScheduleColumnScheduleIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumScheduleColumn => ScheduleColumnEntityTable?.NumRows ?? 0;
        public IArray<ScheduleColumn> ScheduleColumnList { get; }
        public ScheduleColumn GetScheduleColumn(int n)
        {
            if (n < 0) return null;
            var r = new ScheduleColumn();
            r.Document = Document;
            r.Index = n;
            r.Name = ScheduleColumnName.ElementAtOrDefault(n);
            r.ColumnIndex = ScheduleColumnColumnIndex.ElementAtOrDefault(n);
            r._Schedule = new Relation<Schedule>(GetScheduleColumnScheduleIndex(n), GetSchedule);
            return r;
        }
        
        
        // ScheduleCell
        
        public EntityTable ScheduleCellEntityTable { get; }
        
        public IArray<String> ScheduleCellValue { get; }
        public String GetScheduleCellValue(int index, String defaultValue = "") => ScheduleCellValue?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Int32> ScheduleCellRowIndex { get; }
        public Int32 GetScheduleCellRowIndex(int index, Int32 defaultValue = default) => ScheduleCellRowIndex?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> ScheduleCellScheduleColumnIndex { get; }
        public int GetScheduleCellScheduleColumnIndex(int index) => ScheduleCellScheduleColumnIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumScheduleCell => ScheduleCellEntityTable?.NumRows ?? 0;
        public IArray<ScheduleCell> ScheduleCellList { get; }
        public ScheduleCell GetScheduleCell(int n)
        {
            if (n < 0) return null;
            var r = new ScheduleCell();
            r.Document = Document;
            r.Index = n;
            r.Value = ScheduleCellValue.ElementAtOrDefault(n);
            r.RowIndex = ScheduleCellRowIndex.ElementAtOrDefault(n);
            r._ScheduleColumn = new Relation<ScheduleColumn>(GetScheduleCellScheduleColumnIndex(n), GetScheduleColumn);
            return r;
        }
        
        // All entity collections
        public Dictionary<string, IEnumerable<Entity>> AllEntities => new Dictionary<string, IEnumerable<Entity>>() {
            {"Vim.Asset", AssetList.ToEnumerable()},
            {"Vim.DisplayUnit", DisplayUnitList.ToEnumerable()},
            {"Vim.ParameterDescriptor", ParameterDescriptorList.ToEnumerable()},
            {"Vim.Parameter", ParameterList.ToEnumerable()},
            {"Vim.Element", ElementList.ToEnumerable()},
            {"Vim.Workset", WorksetList.ToEnumerable()},
            {"Vim.AssemblyInstance", AssemblyInstanceList.ToEnumerable()},
            {"Vim.Group", GroupList.ToEnumerable()},
            {"Vim.DesignOption", DesignOptionList.ToEnumerable()},
            {"Vim.Level", LevelList.ToEnumerable()},
            {"Vim.Phase", PhaseList.ToEnumerable()},
            {"Vim.Room", RoomList.ToEnumerable()},
            {"Vim.BimDocument", BimDocumentList.ToEnumerable()},
            {"Vim.DisplayUnitInBimDocument", DisplayUnitInBimDocumentList.ToEnumerable()},
            {"Vim.PhaseOrderInBimDocument", PhaseOrderInBimDocumentList.ToEnumerable()},
            {"Vim.Category", CategoryList.ToEnumerable()},
            {"Vim.Family", FamilyList.ToEnumerable()},
            {"Vim.FamilyType", FamilyTypeList.ToEnumerable()},
            {"Vim.FamilyInstance", FamilyInstanceList.ToEnumerable()},
            {"Vim.View", ViewList.ToEnumerable()},
            {"Vim.ElementInView", ElementInViewList.ToEnumerable()},
            {"Vim.ShapeInView", ShapeInViewList.ToEnumerable()},
            {"Vim.AssetInView", AssetInViewList.ToEnumerable()},
            {"Vim.LevelInView", LevelInViewList.ToEnumerable()},
            {"Vim.Camera", CameraList.ToEnumerable()},
            {"Vim.Material", MaterialList.ToEnumerable()},
            {"Vim.MaterialInElement", MaterialInElementList.ToEnumerable()},
            {"Vim.CompoundStructureLayer", CompoundStructureLayerList.ToEnumerable()},
            {"Vim.CompoundStructure", CompoundStructureList.ToEnumerable()},
            {"Vim.Node", NodeList.ToEnumerable()},
            {"Vim.Geometry", GeometryList.ToEnumerable()},
            {"Vim.Shape", ShapeList.ToEnumerable()},
            {"Vim.ShapeCollection", ShapeCollectionList.ToEnumerable()},
            {"Vim.ShapeInShapeCollection", ShapeInShapeCollectionList.ToEnumerable()},
            {"Vim.System", SystemList.ToEnumerable()},
            {"Vim.ElementInSystem", ElementInSystemList.ToEnumerable()},
            {"Vim.Warning", WarningList.ToEnumerable()},
            {"Vim.ElementInWarning", ElementInWarningList.ToEnumerable()},
            {"Vim.BasePoint", BasePointList.ToEnumerable()},
            {"Vim.PhaseFilter", PhaseFilterList.ToEnumerable()},
            {"Vim.Grid", GridList.ToEnumerable()},
            {"Vim.Area", AreaList.ToEnumerable()},
            {"Vim.AreaScheme", AreaSchemeList.ToEnumerable()},
            {"Vim.Schedule", ScheduleList.ToEnumerable()},
            {"Vim.ScheduleColumn", ScheduleColumnList.ToEnumerable()},
            {"Vim.ScheduleCell", ScheduleCellList.ToEnumerable()},
        };
        
        // Entity types from table names
        public Dictionary<string, Type> EntityTypes => new Dictionary<string, Type>() {
            {"Vim.Asset", typeof(Asset)},
            {"Vim.DisplayUnit", typeof(DisplayUnit)},
            {"Vim.ParameterDescriptor", typeof(ParameterDescriptor)},
            {"Vim.Parameter", typeof(Parameter)},
            {"Vim.Element", typeof(Element)},
            {"Vim.Workset", typeof(Workset)},
            {"Vim.AssemblyInstance", typeof(AssemblyInstance)},
            {"Vim.Group", typeof(Group)},
            {"Vim.DesignOption", typeof(DesignOption)},
            {"Vim.Level", typeof(Level)},
            {"Vim.Phase", typeof(Phase)},
            {"Vim.Room", typeof(Room)},
            {"Vim.BimDocument", typeof(BimDocument)},
            {"Vim.DisplayUnitInBimDocument", typeof(DisplayUnitInBimDocument)},
            {"Vim.PhaseOrderInBimDocument", typeof(PhaseOrderInBimDocument)},
            {"Vim.Category", typeof(Category)},
            {"Vim.Family", typeof(Family)},
            {"Vim.FamilyType", typeof(FamilyType)},
            {"Vim.FamilyInstance", typeof(FamilyInstance)},
            {"Vim.View", typeof(View)},
            {"Vim.ElementInView", typeof(ElementInView)},
            {"Vim.ShapeInView", typeof(ShapeInView)},
            {"Vim.AssetInView", typeof(AssetInView)},
            {"Vim.LevelInView", typeof(LevelInView)},
            {"Vim.Camera", typeof(Camera)},
            {"Vim.Material", typeof(Material)},
            {"Vim.MaterialInElement", typeof(MaterialInElement)},
            {"Vim.CompoundStructureLayer", typeof(CompoundStructureLayer)},
            {"Vim.CompoundStructure", typeof(CompoundStructure)},
            {"Vim.Node", typeof(Node)},
            {"Vim.Geometry", typeof(Geometry)},
            {"Vim.Shape", typeof(Shape)},
            {"Vim.ShapeCollection", typeof(ShapeCollection)},
            {"Vim.ShapeInShapeCollection", typeof(ShapeInShapeCollection)},
            {"Vim.System", typeof(System)},
            {"Vim.ElementInSystem", typeof(ElementInSystem)},
            {"Vim.Warning", typeof(Warning)},
            {"Vim.ElementInWarning", typeof(ElementInWarning)},
            {"Vim.BasePoint", typeof(BasePoint)},
            {"Vim.PhaseFilter", typeof(PhaseFilter)},
            {"Vim.Grid", typeof(Grid)},
            {"Vim.Area", typeof(Area)},
            {"Vim.AreaScheme", typeof(AreaScheme)},
            {"Vim.Schedule", typeof(Schedule)},
            {"Vim.ScheduleColumn", typeof(ScheduleColumn)},
            {"Vim.ScheduleCell", typeof(ScheduleCell)},
        };
        public DocumentModel(Document d, bool inParallel = true)
        {
            Document = d;
            
            // Initialize entity tables
            AssetEntityTable = Document.GetTable("Vim.Asset");
            DisplayUnitEntityTable = Document.GetTable("Vim.DisplayUnit");
            ParameterDescriptorEntityTable = Document.GetTable("Vim.ParameterDescriptor");
            ParameterEntityTable = Document.GetTable("Vim.Parameter");
            ElementEntityTable = Document.GetTable("Vim.Element");
            WorksetEntityTable = Document.GetTable("Vim.Workset");
            AssemblyInstanceEntityTable = Document.GetTable("Vim.AssemblyInstance");
            GroupEntityTable = Document.GetTable("Vim.Group");
            DesignOptionEntityTable = Document.GetTable("Vim.DesignOption");
            LevelEntityTable = Document.GetTable("Vim.Level");
            PhaseEntityTable = Document.GetTable("Vim.Phase");
            RoomEntityTable = Document.GetTable("Vim.Room");
            BimDocumentEntityTable = Document.GetTable("Vim.BimDocument");
            DisplayUnitInBimDocumentEntityTable = Document.GetTable("Vim.DisplayUnitInBimDocument");
            PhaseOrderInBimDocumentEntityTable = Document.GetTable("Vim.PhaseOrderInBimDocument");
            CategoryEntityTable = Document.GetTable("Vim.Category");
            FamilyEntityTable = Document.GetTable("Vim.Family");
            FamilyTypeEntityTable = Document.GetTable("Vim.FamilyType");
            FamilyInstanceEntityTable = Document.GetTable("Vim.FamilyInstance");
            ViewEntityTable = Document.GetTable("Vim.View");
            ElementInViewEntityTable = Document.GetTable("Vim.ElementInView");
            ShapeInViewEntityTable = Document.GetTable("Vim.ShapeInView");
            AssetInViewEntityTable = Document.GetTable("Vim.AssetInView");
            LevelInViewEntityTable = Document.GetTable("Vim.LevelInView");
            CameraEntityTable = Document.GetTable("Vim.Camera");
            MaterialEntityTable = Document.GetTable("Vim.Material");
            MaterialInElementEntityTable = Document.GetTable("Vim.MaterialInElement");
            CompoundStructureLayerEntityTable = Document.GetTable("Vim.CompoundStructureLayer");
            CompoundStructureEntityTable = Document.GetTable("Vim.CompoundStructure");
            NodeEntityTable = Document.GetTable("Vim.Node");
            GeometryEntityTable = Document.GetTable("Vim.Geometry");
            ShapeEntityTable = Document.GetTable("Vim.Shape");
            ShapeCollectionEntityTable = Document.GetTable("Vim.ShapeCollection");
            ShapeInShapeCollectionEntityTable = Document.GetTable("Vim.ShapeInShapeCollection");
            SystemEntityTable = Document.GetTable("Vim.System");
            ElementInSystemEntityTable = Document.GetTable("Vim.ElementInSystem");
            WarningEntityTable = Document.GetTable("Vim.Warning");
            ElementInWarningEntityTable = Document.GetTable("Vim.ElementInWarning");
            BasePointEntityTable = Document.GetTable("Vim.BasePoint");
            PhaseFilterEntityTable = Document.GetTable("Vim.PhaseFilter");
            GridEntityTable = Document.GetTable("Vim.Grid");
            AreaEntityTable = Document.GetTable("Vim.Area");
            AreaSchemeEntityTable = Document.GetTable("Vim.AreaScheme");
            ScheduleEntityTable = Document.GetTable("Vim.Schedule");
            ScheduleColumnEntityTable = Document.GetTable("Vim.ScheduleColumn");
            ScheduleCellEntityTable = Document.GetTable("Vim.ScheduleCell");
            
            // Initialize entity arrays
            AssetBufferName = AssetEntityTable?.GetStringColumnValues("string:BufferName") ?? Array.Empty<String>().ToIArray();
            DisplayUnitSpec = DisplayUnitEntityTable?.GetStringColumnValues("string:Spec") ?? Array.Empty<String>().ToIArray();
            DisplayUnitType = DisplayUnitEntityTable?.GetStringColumnValues("string:Type") ?? Array.Empty<String>().ToIArray();
            DisplayUnitLabel = DisplayUnitEntityTable?.GetStringColumnValues("string:Label") ?? Array.Empty<String>().ToIArray();
            ParameterDescriptorName = ParameterDescriptorEntityTable?.GetStringColumnValues("string:Name") ?? Array.Empty<String>().ToIArray();
            ParameterDescriptorGroup = ParameterDescriptorEntityTable?.GetStringColumnValues("string:Group") ?? Array.Empty<String>().ToIArray();
            ParameterDescriptorParameterType = ParameterDescriptorEntityTable?.GetStringColumnValues("string:ParameterType") ?? Array.Empty<String>().ToIArray();
            ParameterDescriptorIsInstance = ParameterDescriptorEntityTable?.GetDataColumnValues<Boolean>("byte:IsInstance") ?? Array.Empty<Boolean>().ToIArray();
            ParameterDescriptorIsShared = ParameterDescriptorEntityTable?.GetDataColumnValues<Boolean>("byte:IsShared") ?? Array.Empty<Boolean>().ToIArray();
            ParameterDescriptorIsReadOnly = ParameterDescriptorEntityTable?.GetDataColumnValues<Boolean>("byte:IsReadOnly") ?? Array.Empty<Boolean>().ToIArray();
            ParameterDescriptorFlags = ParameterDescriptorEntityTable?.GetDataColumnValues<Int32>("int:Flags") ?? Array.Empty<Int32>().ToIArray();
            ParameterDescriptorGuid = ParameterDescriptorEntityTable?.GetStringColumnValues("string:Guid") ?? Array.Empty<String>().ToIArray();
            ParameterValue = ParameterEntityTable?.GetStringColumnValues("string:Value") ?? Array.Empty<String>().ToIArray();
            ElementId = ElementEntityTable?.GetDataColumnValues<Int32>("int:Id") ?? Array.Empty<Int32>().ToIArray();
            ElementType = ElementEntityTable?.GetStringColumnValues("string:Type") ?? Array.Empty<String>().ToIArray();
            ElementName = ElementEntityTable?.GetStringColumnValues("string:Name") ?? Array.Empty<String>().ToIArray();
            ElementUniqueId = ElementEntityTable?.GetStringColumnValues("string:UniqueId") ?? Array.Empty<String>().ToIArray();
            ElementLocation = ElementEntityTable?.GetCompositeDataColumnValues<Vector3>("Location") ?? Array.Empty<Vector3>().ToIArray();
            ElementFamilyName = ElementEntityTable?.GetStringColumnValues("string:FamilyName") ?? Array.Empty<String>().ToIArray();
            ElementIsPinned = ElementEntityTable?.GetDataColumnValues<Boolean>("byte:IsPinned") ?? Array.Empty<Boolean>().ToIArray();
            WorksetId = WorksetEntityTable?.GetDataColumnValues<Int32>("int:Id") ?? Array.Empty<Int32>().ToIArray();
            WorksetName = WorksetEntityTable?.GetStringColumnValues("string:Name") ?? Array.Empty<String>().ToIArray();
            WorksetKind = WorksetEntityTable?.GetStringColumnValues("string:Kind") ?? Array.Empty<String>().ToIArray();
            WorksetIsOpen = WorksetEntityTable?.GetDataColumnValues<Boolean>("byte:IsOpen") ?? Array.Empty<Boolean>().ToIArray();
            WorksetIsEditable = WorksetEntityTable?.GetDataColumnValues<Boolean>("byte:IsEditable") ?? Array.Empty<Boolean>().ToIArray();
            WorksetOwner = WorksetEntityTable?.GetStringColumnValues("string:Owner") ?? Array.Empty<String>().ToIArray();
            WorksetUniqueId = WorksetEntityTable?.GetStringColumnValues("string:UniqueId") ?? Array.Empty<String>().ToIArray();
            AssemblyInstanceAssemblyTypeName = AssemblyInstanceEntityTable?.GetStringColumnValues("string:AssemblyTypeName") ?? Array.Empty<String>().ToIArray();
            AssemblyInstancePosition = AssemblyInstanceEntityTable?.GetCompositeDataColumnValues<Vector3>("Position") ?? Array.Empty<Vector3>().ToIArray();
            GroupGroupType = GroupEntityTable?.GetStringColumnValues("string:GroupType") ?? Array.Empty<String>().ToIArray();
            GroupPosition = GroupEntityTable?.GetCompositeDataColumnValues<Vector3>("Position") ?? Array.Empty<Vector3>().ToIArray();
            DesignOptionIsPrimary = DesignOptionEntityTable?.GetDataColumnValues<Boolean>("byte:IsPrimary") ?? Array.Empty<Boolean>().ToIArray();
            LevelElevation = LevelEntityTable?.GetDataColumnValues<Double>("double:Elevation") ?? Array.Empty<Double>().ToIArray();
            RoomBaseOffset = RoomEntityTable?.GetDataColumnValues<Double>("double:BaseOffset") ?? Array.Empty<Double>().ToIArray();
            RoomLimitOffset = RoomEntityTable?.GetDataColumnValues<Double>("double:LimitOffset") ?? Array.Empty<Double>().ToIArray();
            RoomUnboundedHeight = RoomEntityTable?.GetDataColumnValues<Double>("double:UnboundedHeight") ?? Array.Empty<Double>().ToIArray();
            RoomVolume = RoomEntityTable?.GetDataColumnValues<Double>("double:Volume") ?? Array.Empty<Double>().ToIArray();
            RoomPerimeter = RoomEntityTable?.GetDataColumnValues<Double>("double:Perimeter") ?? Array.Empty<Double>().ToIArray();
            RoomArea = RoomEntityTable?.GetDataColumnValues<Double>("double:Area") ?? Array.Empty<Double>().ToIArray();
            RoomNumber = RoomEntityTable?.GetStringColumnValues("string:Number") ?? Array.Empty<String>().ToIArray();
            BimDocumentTitle = BimDocumentEntityTable?.GetStringColumnValues("string:Title") ?? Array.Empty<String>().ToIArray();
            BimDocumentIsMetric = BimDocumentEntityTable?.GetDataColumnValues<Boolean>("byte:IsMetric") ?? Array.Empty<Boolean>().ToIArray();
            BimDocumentGuid = BimDocumentEntityTable?.GetStringColumnValues("string:Guid") ?? Array.Empty<String>().ToIArray();
            BimDocumentNumSaves = BimDocumentEntityTable?.GetDataColumnValues<Int32>("int:NumSaves") ?? Array.Empty<Int32>().ToIArray();
            BimDocumentIsLinked = BimDocumentEntityTable?.GetDataColumnValues<Boolean>("byte:IsLinked") ?? Array.Empty<Boolean>().ToIArray();
            BimDocumentIsDetached = BimDocumentEntityTable?.GetDataColumnValues<Boolean>("byte:IsDetached") ?? Array.Empty<Boolean>().ToIArray();
            BimDocumentIsWorkshared = BimDocumentEntityTable?.GetDataColumnValues<Boolean>("byte:IsWorkshared") ?? Array.Empty<Boolean>().ToIArray();
            BimDocumentPathName = BimDocumentEntityTable?.GetStringColumnValues("string:PathName") ?? Array.Empty<String>().ToIArray();
            BimDocumentLatitude = BimDocumentEntityTable?.GetDataColumnValues<Double>("double:Latitude") ?? Array.Empty<Double>().ToIArray();
            BimDocumentLongitude = BimDocumentEntityTable?.GetDataColumnValues<Double>("double:Longitude") ?? Array.Empty<Double>().ToIArray();
            BimDocumentTimeZone = BimDocumentEntityTable?.GetDataColumnValues<Double>("double:TimeZone") ?? Array.Empty<Double>().ToIArray();
            BimDocumentPlaceName = BimDocumentEntityTable?.GetStringColumnValues("string:PlaceName") ?? Array.Empty<String>().ToIArray();
            BimDocumentWeatherStationName = BimDocumentEntityTable?.GetStringColumnValues("string:WeatherStationName") ?? Array.Empty<String>().ToIArray();
            BimDocumentElevation = BimDocumentEntityTable?.GetDataColumnValues<Double>("double:Elevation") ?? Array.Empty<Double>().ToIArray();
            BimDocumentProjectLocation = BimDocumentEntityTable?.GetStringColumnValues("string:ProjectLocation") ?? Array.Empty<String>().ToIArray();
            BimDocumentIssueDate = BimDocumentEntityTable?.GetStringColumnValues("string:IssueDate") ?? Array.Empty<String>().ToIArray();
            BimDocumentStatus = BimDocumentEntityTable?.GetStringColumnValues("string:Status") ?? Array.Empty<String>().ToIArray();
            BimDocumentClientName = BimDocumentEntityTable?.GetStringColumnValues("string:ClientName") ?? Array.Empty<String>().ToIArray();
            BimDocumentAddress = BimDocumentEntityTable?.GetStringColumnValues("string:Address") ?? Array.Empty<String>().ToIArray();
            BimDocumentName = BimDocumentEntityTable?.GetStringColumnValues("string:Name") ?? Array.Empty<String>().ToIArray();
            BimDocumentNumber = BimDocumentEntityTable?.GetStringColumnValues("string:Number") ?? Array.Empty<String>().ToIArray();
            BimDocumentAuthor = BimDocumentEntityTable?.GetStringColumnValues("string:Author") ?? Array.Empty<String>().ToIArray();
            BimDocumentBuildingName = BimDocumentEntityTable?.GetStringColumnValues("string:BuildingName") ?? Array.Empty<String>().ToIArray();
            BimDocumentOrganizationName = BimDocumentEntityTable?.GetStringColumnValues("string:OrganizationName") ?? Array.Empty<String>().ToIArray();
            BimDocumentOrganizationDescription = BimDocumentEntityTable?.GetStringColumnValues("string:OrganizationDescription") ?? Array.Empty<String>().ToIArray();
            BimDocumentProduct = BimDocumentEntityTable?.GetStringColumnValues("string:Product") ?? Array.Empty<String>().ToIArray();
            BimDocumentVersion = BimDocumentEntityTable?.GetStringColumnValues("string:Version") ?? Array.Empty<String>().ToIArray();
            BimDocumentUser = BimDocumentEntityTable?.GetStringColumnValues("string:User") ?? Array.Empty<String>().ToIArray();
            PhaseOrderInBimDocumentOrderIndex = PhaseOrderInBimDocumentEntityTable?.GetDataColumnValues<Int32>("int:OrderIndex") ?? Array.Empty<Int32>().ToIArray();
            CategoryName = CategoryEntityTable?.GetStringColumnValues("string:Name") ?? Array.Empty<String>().ToIArray();
            CategoryId = CategoryEntityTable?.GetDataColumnValues<Int32>("int:Id") ?? Array.Empty<Int32>().ToIArray();
            CategoryCategoryType = CategoryEntityTable?.GetStringColumnValues("string:CategoryType") ?? Array.Empty<String>().ToIArray();
            CategoryLineColor = CategoryEntityTable?.GetCompositeDataColumnValues<DVector3>("LineColor") ?? Array.Empty<DVector3>().ToIArray();
            CategoryBuiltInCategory = CategoryEntityTable?.GetStringColumnValues("string:BuiltInCategory") ?? Array.Empty<String>().ToIArray();
            FamilyStructuralMaterialType = FamilyEntityTable?.GetStringColumnValues("string:StructuralMaterialType") ?? Array.Empty<String>().ToIArray();
            FamilyStructuralSectionShape = FamilyEntityTable?.GetStringColumnValues("string:StructuralSectionShape") ?? Array.Empty<String>().ToIArray();
            FamilyIsSystemFamily = FamilyEntityTable?.GetDataColumnValues<Boolean>("byte:IsSystemFamily") ?? Array.Empty<Boolean>().ToIArray();
            FamilyIsInPlace = FamilyEntityTable?.GetDataColumnValues<Boolean>("byte:IsInPlace") ?? Array.Empty<Boolean>().ToIArray();
            FamilyTypeIsSystemFamilyType = FamilyTypeEntityTable?.GetDataColumnValues<Boolean>("byte:IsSystemFamilyType") ?? Array.Empty<Boolean>().ToIArray();
            FamilyInstanceFacingFlipped = FamilyInstanceEntityTable?.GetDataColumnValues<Boolean>("byte:FacingFlipped") ?? Array.Empty<Boolean>().ToIArray();
            FamilyInstanceFacingOrientation = FamilyInstanceEntityTable?.GetCompositeDataColumnValues<Vector3>("FacingOrientation") ?? Array.Empty<Vector3>().ToIArray();
            FamilyInstanceHandFlipped = FamilyInstanceEntityTable?.GetDataColumnValues<Boolean>("byte:HandFlipped") ?? Array.Empty<Boolean>().ToIArray();
            FamilyInstanceMirrored = FamilyInstanceEntityTable?.GetDataColumnValues<Boolean>("byte:Mirrored") ?? Array.Empty<Boolean>().ToIArray();
            FamilyInstanceHasModifiedGeometry = FamilyInstanceEntityTable?.GetDataColumnValues<Boolean>("byte:HasModifiedGeometry") ?? Array.Empty<Boolean>().ToIArray();
            FamilyInstanceScale = FamilyInstanceEntityTable?.GetDataColumnValues<Single>("float:Scale") ?? Array.Empty<Single>().ToIArray();
            FamilyInstanceBasisX = FamilyInstanceEntityTable?.GetCompositeDataColumnValues<Vector3>("BasisX") ?? Array.Empty<Vector3>().ToIArray();
            FamilyInstanceBasisY = FamilyInstanceEntityTable?.GetCompositeDataColumnValues<Vector3>("BasisY") ?? Array.Empty<Vector3>().ToIArray();
            FamilyInstanceBasisZ = FamilyInstanceEntityTable?.GetCompositeDataColumnValues<Vector3>("BasisZ") ?? Array.Empty<Vector3>().ToIArray();
            FamilyInstanceTranslation = FamilyInstanceEntityTable?.GetCompositeDataColumnValues<Vector3>("Translation") ?? Array.Empty<Vector3>().ToIArray();
            FamilyInstanceHandOrientation = FamilyInstanceEntityTable?.GetCompositeDataColumnValues<Vector3>("HandOrientation") ?? Array.Empty<Vector3>().ToIArray();
            ViewTitle = ViewEntityTable?.GetStringColumnValues("string:Title") ?? Array.Empty<String>().ToIArray();
            ViewViewType = ViewEntityTable?.GetStringColumnValues("string:ViewType") ?? Array.Empty<String>().ToIArray();
            ViewUp = ViewEntityTable?.GetCompositeDataColumnValues<DVector3>("Up") ?? Array.Empty<DVector3>().ToIArray();
            ViewRight = ViewEntityTable?.GetCompositeDataColumnValues<DVector3>("Right") ?? Array.Empty<DVector3>().ToIArray();
            ViewOrigin = ViewEntityTable?.GetCompositeDataColumnValues<DVector3>("Origin") ?? Array.Empty<DVector3>().ToIArray();
            ViewViewDirection = ViewEntityTable?.GetCompositeDataColumnValues<DVector3>("ViewDirection") ?? Array.Empty<DVector3>().ToIArray();
            ViewViewPosition = ViewEntityTable?.GetCompositeDataColumnValues<DVector3>("ViewPosition") ?? Array.Empty<DVector3>().ToIArray();
            ViewScale = ViewEntityTable?.GetDataColumnValues<Double>("double:Scale") ?? Array.Empty<Double>().ToIArray();
            ViewOutline = ViewEntityTable?.GetCompositeDataColumnValues<DAABox2D>("Outline") ?? Array.Empty<DAABox2D>().ToIArray();
            ViewDetailLevel = ViewEntityTable?.GetDataColumnValues<Int32>("int:DetailLevel") ?? Array.Empty<Int32>().ToIArray();
            LevelInViewExtents = LevelInViewEntityTable?.GetCompositeDataColumnValues<DAABox>("Extents") ?? Array.Empty<DAABox>().ToIArray();
            CameraId = CameraEntityTable?.GetDataColumnValues<Int32>("int:Id") ?? Array.Empty<Int32>().ToIArray();
            CameraIsPerspective = CameraEntityTable?.GetDataColumnValues<Int32>("int:IsPerspective") ?? Array.Empty<Int32>().ToIArray();
            CameraVerticalExtent = CameraEntityTable?.GetDataColumnValues<Double>("double:VerticalExtent") ?? Array.Empty<Double>().ToIArray();
            CameraHorizontalExtent = CameraEntityTable?.GetDataColumnValues<Double>("double:HorizontalExtent") ?? Array.Empty<Double>().ToIArray();
            CameraFarDistance = CameraEntityTable?.GetDataColumnValues<Double>("double:FarDistance") ?? Array.Empty<Double>().ToIArray();
            CameraNearDistance = CameraEntityTable?.GetDataColumnValues<Double>("double:NearDistance") ?? Array.Empty<Double>().ToIArray();
            CameraTargetDistance = CameraEntityTable?.GetDataColumnValues<Double>("double:TargetDistance") ?? Array.Empty<Double>().ToIArray();
            CameraRightOffset = CameraEntityTable?.GetDataColumnValues<Double>("double:RightOffset") ?? Array.Empty<Double>().ToIArray();
            CameraUpOffset = CameraEntityTable?.GetDataColumnValues<Double>("double:UpOffset") ?? Array.Empty<Double>().ToIArray();
            MaterialName = MaterialEntityTable?.GetStringColumnValues("string:Name") ?? Array.Empty<String>().ToIArray();
            MaterialMaterialCategory = MaterialEntityTable?.GetStringColumnValues("string:MaterialCategory") ?? Array.Empty<String>().ToIArray();
            MaterialColor = MaterialEntityTable?.GetCompositeDataColumnValues<DVector3>("Color") ?? Array.Empty<DVector3>().ToIArray();
            MaterialColorUvScaling = MaterialEntityTable?.GetCompositeDataColumnValues<DVector2>("ColorUvScaling") ?? Array.Empty<DVector2>().ToIArray();
            MaterialColorUvOffset = MaterialEntityTable?.GetCompositeDataColumnValues<DVector2>("ColorUvOffset") ?? Array.Empty<DVector2>().ToIArray();
            MaterialNormalUvScaling = MaterialEntityTable?.GetCompositeDataColumnValues<DVector2>("NormalUvScaling") ?? Array.Empty<DVector2>().ToIArray();
            MaterialNormalUvOffset = MaterialEntityTable?.GetCompositeDataColumnValues<DVector2>("NormalUvOffset") ?? Array.Empty<DVector2>().ToIArray();
            MaterialNormalAmount = MaterialEntityTable?.GetDataColumnValues<Double>("double:NormalAmount") ?? Array.Empty<Double>().ToIArray();
            MaterialGlossiness = MaterialEntityTable?.GetDataColumnValues<Double>("double:Glossiness") ?? Array.Empty<Double>().ToIArray();
            MaterialSmoothness = MaterialEntityTable?.GetDataColumnValues<Double>("double:Smoothness") ?? Array.Empty<Double>().ToIArray();
            MaterialTransparency = MaterialEntityTable?.GetDataColumnValues<Double>("double:Transparency") ?? Array.Empty<Double>().ToIArray();
            MaterialInElementArea = MaterialInElementEntityTable?.GetDataColumnValues<Double>("double:Area") ?? Array.Empty<Double>().ToIArray();
            MaterialInElementVolume = MaterialInElementEntityTable?.GetDataColumnValues<Double>("double:Volume") ?? Array.Empty<Double>().ToIArray();
            MaterialInElementIsPaint = MaterialInElementEntityTable?.GetDataColumnValues<Boolean>("byte:IsPaint") ?? Array.Empty<Boolean>().ToIArray();
            CompoundStructureLayerOrderIndex = CompoundStructureLayerEntityTable?.GetDataColumnValues<Int32>("int:OrderIndex") ?? Array.Empty<Int32>().ToIArray();
            CompoundStructureLayerWidth = CompoundStructureLayerEntityTable?.GetDataColumnValues<Double>("double:Width") ?? Array.Empty<Double>().ToIArray();
            CompoundStructureLayerMaterialFunctionAssignment = CompoundStructureLayerEntityTable?.GetStringColumnValues("string:MaterialFunctionAssignment") ?? Array.Empty<String>().ToIArray();
            CompoundStructureWidth = CompoundStructureEntityTable?.GetDataColumnValues<Double>("double:Width") ?? Array.Empty<Double>().ToIArray();
            GeometryBox = GeometryEntityTable?.GetCompositeDataColumnValues<AABox>("Box") ?? Array.Empty<AABox>().ToIArray();
            GeometryVertexCount = GeometryEntityTable?.GetDataColumnValues<Int32>("int:VertexCount") ?? Array.Empty<Int32>().ToIArray();
            GeometryFaceCount = GeometryEntityTable?.GetDataColumnValues<Int32>("int:FaceCount") ?? Array.Empty<Int32>().ToIArray();
            SystemSystemType = SystemEntityTable?.GetDataColumnValues<Int32>("int:SystemType") ?? Array.Empty<Int32>().ToIArray();
            ElementInSystemRoles = ElementInSystemEntityTable?.GetDataColumnValues<Int32>("int:Roles") ?? Array.Empty<Int32>().ToIArray();
            WarningGuid = WarningEntityTable?.GetStringColumnValues("string:Guid") ?? Array.Empty<String>().ToIArray();
            WarningSeverity = WarningEntityTable?.GetStringColumnValues("string:Severity") ?? Array.Empty<String>().ToIArray();
            WarningDescription = WarningEntityTable?.GetStringColumnValues("string:Description") ?? Array.Empty<String>().ToIArray();
            BasePointIsSurveyPoint = BasePointEntityTable?.GetDataColumnValues<Boolean>("byte:IsSurveyPoint") ?? Array.Empty<Boolean>().ToIArray();
            BasePointPosition = BasePointEntityTable?.GetCompositeDataColumnValues<DVector3>("Position") ?? Array.Empty<DVector3>().ToIArray();
            BasePointSharedPosition = BasePointEntityTable?.GetCompositeDataColumnValues<DVector3>("SharedPosition") ?? Array.Empty<DVector3>().ToIArray();
            PhaseFilterNew = PhaseFilterEntityTable?.GetDataColumnValues<Int32>("int:New") ?? Array.Empty<Int32>().ToIArray();
            PhaseFilterExisting = PhaseFilterEntityTable?.GetDataColumnValues<Int32>("int:Existing") ?? Array.Empty<Int32>().ToIArray();
            PhaseFilterDemolished = PhaseFilterEntityTable?.GetDataColumnValues<Int32>("int:Demolished") ?? Array.Empty<Int32>().ToIArray();
            PhaseFilterTemporary = PhaseFilterEntityTable?.GetDataColumnValues<Int32>("int:Temporary") ?? Array.Empty<Int32>().ToIArray();
            GridStartPoint = GridEntityTable?.GetCompositeDataColumnValues<DVector3>("StartPoint") ?? Array.Empty<DVector3>().ToIArray();
            GridEndPoint = GridEntityTable?.GetCompositeDataColumnValues<DVector3>("EndPoint") ?? Array.Empty<DVector3>().ToIArray();
            GridIsCurved = GridEntityTable?.GetDataColumnValues<Boolean>("byte:IsCurved") ?? Array.Empty<Boolean>().ToIArray();
            GridExtents = GridEntityTable?.GetCompositeDataColumnValues<DAABox>("Extents") ?? Array.Empty<DAABox>().ToIArray();
            AreaValue = AreaEntityTable?.GetDataColumnValues<Double>("double:Value") ?? Array.Empty<Double>().ToIArray();
            AreaPerimeter = AreaEntityTable?.GetDataColumnValues<Double>("double:Perimeter") ?? Array.Empty<Double>().ToIArray();
            AreaNumber = AreaEntityTable?.GetStringColumnValues("string:Number") ?? Array.Empty<String>().ToIArray();
            AreaIsGrossInterior = AreaEntityTable?.GetDataColumnValues<Boolean>("byte:IsGrossInterior") ?? Array.Empty<Boolean>().ToIArray();
            AreaSchemeIsGrossBuildingArea = AreaSchemeEntityTable?.GetDataColumnValues<Boolean>("byte:IsGrossBuildingArea") ?? Array.Empty<Boolean>().ToIArray();
            ScheduleColumnName = ScheduleColumnEntityTable?.GetStringColumnValues("string:Name") ?? Array.Empty<String>().ToIArray();
            ScheduleColumnColumnIndex = ScheduleColumnEntityTable?.GetDataColumnValues<Int32>("int:ColumnIndex") ?? Array.Empty<Int32>().ToIArray();
            ScheduleCellValue = ScheduleCellEntityTable?.GetStringColumnValues("string:Value") ?? Array.Empty<String>().ToIArray();
            ScheduleCellRowIndex = ScheduleCellEntityTable?.GetDataColumnValues<Int32>("int:RowIndex") ?? Array.Empty<Int32>().ToIArray();
            
            // Initialize entity relational columns
            ParameterDescriptorDisplayUnitIndex = ParameterDescriptorEntityTable?.GetIndexColumnValues("index:Vim.DisplayUnit:DisplayUnit") ?? Array.Empty<int>().ToIArray();
            ParameterParameterDescriptorIndex = ParameterEntityTable?.GetIndexColumnValues("index:Vim.ParameterDescriptor:ParameterDescriptor") ?? Array.Empty<int>().ToIArray();
            ParameterElementIndex = ParameterEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            ElementLevelIndex = ElementEntityTable?.GetIndexColumnValues("index:Vim.Level:Level") ?? Array.Empty<int>().ToIArray();
            ElementPhaseCreatedIndex = ElementEntityTable?.GetIndexColumnValues("index:Vim.Phase:PhaseCreated") ?? Array.Empty<int>().ToIArray();
            ElementPhaseDemolishedIndex = ElementEntityTable?.GetIndexColumnValues("index:Vim.Phase:PhaseDemolished") ?? Array.Empty<int>().ToIArray();
            ElementCategoryIndex = ElementEntityTable?.GetIndexColumnValues("index:Vim.Category:Category") ?? Array.Empty<int>().ToIArray();
            ElementWorksetIndex = ElementEntityTable?.GetIndexColumnValues("index:Vim.Workset:Workset") ?? Array.Empty<int>().ToIArray();
            ElementDesignOptionIndex = ElementEntityTable?.GetIndexColumnValues("index:Vim.DesignOption:DesignOption") ?? Array.Empty<int>().ToIArray();
            ElementOwnerViewIndex = ElementEntityTable?.GetIndexColumnValues("index:Vim.View:OwnerView") ?? Array.Empty<int>().ToIArray();
            ElementGroupIndex = ElementEntityTable?.GetIndexColumnValues("index:Vim.Group:Group") ?? Array.Empty<int>().ToIArray();
            ElementAssemblyInstanceIndex = ElementEntityTable?.GetIndexColumnValues("index:Vim.AssemblyInstance:AssemblyInstance") ?? Array.Empty<int>().ToIArray();
            ElementBimDocumentIndex = ElementEntityTable?.GetIndexColumnValues("index:Vim.BimDocument:BimDocument") ?? Array.Empty<int>().ToIArray();
            ElementRoomIndex = ElementEntityTable?.GetIndexColumnValues("index:Vim.Room:Room") ?? Array.Empty<int>().ToIArray();
            WorksetBimDocumentIndex = WorksetEntityTable?.GetIndexColumnValues("index:Vim.BimDocument:BimDocument") ?? Array.Empty<int>().ToIArray();
            AssemblyInstanceElementIndex = AssemblyInstanceEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            GroupElementIndex = GroupEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            DesignOptionElementIndex = DesignOptionEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            LevelFamilyTypeIndex = LevelEntityTable?.GetIndexColumnValues("index:Vim.FamilyType:FamilyType") ?? Array.Empty<int>().ToIArray();
            LevelElementIndex = LevelEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            PhaseElementIndex = PhaseEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            RoomUpperLimitIndex = RoomEntityTable?.GetIndexColumnValues("index:Vim.Level:UpperLimit") ?? Array.Empty<int>().ToIArray();
            RoomElementIndex = RoomEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            BimDocumentActiveViewIndex = BimDocumentEntityTable?.GetIndexColumnValues("index:Vim.View:ActiveView") ?? Array.Empty<int>().ToIArray();
            BimDocumentOwnerFamilyIndex = BimDocumentEntityTable?.GetIndexColumnValues("index:Vim.Family:OwnerFamily") ?? Array.Empty<int>().ToIArray();
            BimDocumentParentIndex = BimDocumentEntityTable?.GetIndexColumnValues("index:Vim.BimDocument:Parent") ?? Array.Empty<int>().ToIArray();
            BimDocumentElementIndex = BimDocumentEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            DisplayUnitInBimDocumentDisplayUnitIndex = DisplayUnitInBimDocumentEntityTable?.GetIndexColumnValues("index:Vim.DisplayUnit:DisplayUnit") ?? Array.Empty<int>().ToIArray();
            DisplayUnitInBimDocumentBimDocumentIndex = DisplayUnitInBimDocumentEntityTable?.GetIndexColumnValues("index:Vim.BimDocument:BimDocument") ?? Array.Empty<int>().ToIArray();
            PhaseOrderInBimDocumentPhaseIndex = PhaseOrderInBimDocumentEntityTable?.GetIndexColumnValues("index:Vim.Phase:Phase") ?? Array.Empty<int>().ToIArray();
            PhaseOrderInBimDocumentBimDocumentIndex = PhaseOrderInBimDocumentEntityTable?.GetIndexColumnValues("index:Vim.BimDocument:BimDocument") ?? Array.Empty<int>().ToIArray();
            CategoryParentIndex = CategoryEntityTable?.GetIndexColumnValues("index:Vim.Category:Parent") ?? Array.Empty<int>().ToIArray();
            CategoryMaterialIndex = CategoryEntityTable?.GetIndexColumnValues("index:Vim.Material:Material") ?? Array.Empty<int>().ToIArray();
            FamilyFamilyCategoryIndex = FamilyEntityTable?.GetIndexColumnValues("index:Vim.Category:FamilyCategory") ?? Array.Empty<int>().ToIArray();
            FamilyElementIndex = FamilyEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            FamilyTypeFamilyIndex = FamilyTypeEntityTable?.GetIndexColumnValues("index:Vim.Family:Family") ?? Array.Empty<int>().ToIArray();
            FamilyTypeCompoundStructureIndex = FamilyTypeEntityTable?.GetIndexColumnValues("index:Vim.CompoundStructure:CompoundStructure") ?? Array.Empty<int>().ToIArray();
            FamilyTypeElementIndex = FamilyTypeEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            FamilyInstanceFamilyTypeIndex = FamilyInstanceEntityTable?.GetIndexColumnValues("index:Vim.FamilyType:FamilyType") ?? Array.Empty<int>().ToIArray();
            FamilyInstanceHostIndex = FamilyInstanceEntityTable?.GetIndexColumnValues("index:Vim.Element:Host") ?? Array.Empty<int>().ToIArray();
            FamilyInstanceFromRoomIndex = FamilyInstanceEntityTable?.GetIndexColumnValues("index:Vim.Room:FromRoom") ?? Array.Empty<int>().ToIArray();
            FamilyInstanceToRoomIndex = FamilyInstanceEntityTable?.GetIndexColumnValues("index:Vim.Room:ToRoom") ?? Array.Empty<int>().ToIArray();
            FamilyInstanceElementIndex = FamilyInstanceEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            ViewCameraIndex = ViewEntityTable?.GetIndexColumnValues("index:Vim.Camera:Camera") ?? Array.Empty<int>().ToIArray();
            ViewFamilyTypeIndex = ViewEntityTable?.GetIndexColumnValues("index:Vim.FamilyType:FamilyType") ?? Array.Empty<int>().ToIArray();
            ViewElementIndex = ViewEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            ElementInViewViewIndex = ElementInViewEntityTable?.GetIndexColumnValues("index:Vim.View:View") ?? Array.Empty<int>().ToIArray();
            ElementInViewElementIndex = ElementInViewEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            ShapeInViewShapeIndex = ShapeInViewEntityTable?.GetIndexColumnValues("index:Vim.Shape:Shape") ?? Array.Empty<int>().ToIArray();
            ShapeInViewViewIndex = ShapeInViewEntityTable?.GetIndexColumnValues("index:Vim.View:View") ?? Array.Empty<int>().ToIArray();
            AssetInViewAssetIndex = AssetInViewEntityTable?.GetIndexColumnValues("index:Vim.Asset:Asset") ?? Array.Empty<int>().ToIArray();
            AssetInViewViewIndex = AssetInViewEntityTable?.GetIndexColumnValues("index:Vim.View:View") ?? Array.Empty<int>().ToIArray();
            LevelInViewLevelIndex = LevelInViewEntityTable?.GetIndexColumnValues("index:Vim.Level:Level") ?? Array.Empty<int>().ToIArray();
            LevelInViewViewIndex = LevelInViewEntityTable?.GetIndexColumnValues("index:Vim.View:View") ?? Array.Empty<int>().ToIArray();
            MaterialColorTextureFileIndex = MaterialEntityTable?.GetIndexColumnValues("index:Vim.Asset:ColorTextureFile") ?? Array.Empty<int>().ToIArray();
            MaterialNormalTextureFileIndex = MaterialEntityTable?.GetIndexColumnValues("index:Vim.Asset:NormalTextureFile") ?? Array.Empty<int>().ToIArray();
            MaterialElementIndex = MaterialEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            MaterialInElementMaterialIndex = MaterialInElementEntityTable?.GetIndexColumnValues("index:Vim.Material:Material") ?? Array.Empty<int>().ToIArray();
            MaterialInElementElementIndex = MaterialInElementEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            CompoundStructureLayerMaterialIndex = CompoundStructureLayerEntityTable?.GetIndexColumnValues("index:Vim.Material:Material") ?? Array.Empty<int>().ToIArray();
            CompoundStructureLayerCompoundStructureIndex = CompoundStructureLayerEntityTable?.GetIndexColumnValues("index:Vim.CompoundStructure:CompoundStructure") ?? Array.Empty<int>().ToIArray();
            CompoundStructureStructuralLayerIndex = CompoundStructureEntityTable?.GetIndexColumnValues("index:Vim.CompoundStructureLayer:StructuralLayer") ?? Array.Empty<int>().ToIArray();
            NodeElementIndex = NodeEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            ShapeElementIndex = ShapeEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            ShapeCollectionElementIndex = ShapeCollectionEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            ShapeInShapeCollectionShapeIndex = ShapeInShapeCollectionEntityTable?.GetIndexColumnValues("index:Vim.Shape:Shape") ?? Array.Empty<int>().ToIArray();
            ShapeInShapeCollectionShapeCollectionIndex = ShapeInShapeCollectionEntityTable?.GetIndexColumnValues("index:Vim.ShapeCollection:ShapeCollection") ?? Array.Empty<int>().ToIArray();
            SystemFamilyTypeIndex = SystemEntityTable?.GetIndexColumnValues("index:Vim.FamilyType:FamilyType") ?? Array.Empty<int>().ToIArray();
            SystemElementIndex = SystemEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            ElementInSystemSystemIndex = ElementInSystemEntityTable?.GetIndexColumnValues("index:Vim.System:System") ?? Array.Empty<int>().ToIArray();
            ElementInSystemElementIndex = ElementInSystemEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            WarningBimDocumentIndex = WarningEntityTable?.GetIndexColumnValues("index:Vim.BimDocument:BimDocument") ?? Array.Empty<int>().ToIArray();
            ElementInWarningWarningIndex = ElementInWarningEntityTable?.GetIndexColumnValues("index:Vim.Warning:Warning") ?? Array.Empty<int>().ToIArray();
            ElementInWarningElementIndex = ElementInWarningEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            BasePointElementIndex = BasePointEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            PhaseFilterElementIndex = PhaseFilterEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            GridFamilyTypeIndex = GridEntityTable?.GetIndexColumnValues("index:Vim.FamilyType:FamilyType") ?? Array.Empty<int>().ToIArray();
            GridElementIndex = GridEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            AreaAreaSchemeIndex = AreaEntityTable?.GetIndexColumnValues("index:Vim.AreaScheme:AreaScheme") ?? Array.Empty<int>().ToIArray();
            AreaElementIndex = AreaEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            AreaSchemeElementIndex = AreaSchemeEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            ScheduleElementIndex = ScheduleEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            ScheduleColumnScheduleIndex = ScheduleColumnEntityTable?.GetIndexColumnValues("index:Vim.Schedule:Schedule") ?? Array.Empty<int>().ToIArray();
            ScheduleCellScheduleColumnIndex = ScheduleCellEntityTable?.GetIndexColumnValues("index:Vim.ScheduleColumn:ScheduleColumn") ?? Array.Empty<int>().ToIArray();
            
            // Initialize entity collections
            AssetList = NumAsset.Select(i => GetAsset(i));
            DisplayUnitList = NumDisplayUnit.Select(i => GetDisplayUnit(i));
            ParameterDescriptorList = NumParameterDescriptor.Select(i => GetParameterDescriptor(i));
            ParameterList = NumParameter.Select(i => GetParameter(i));
            ElementList = NumElement.Select(i => GetElement(i));
            WorksetList = NumWorkset.Select(i => GetWorkset(i));
            AssemblyInstanceList = NumAssemblyInstance.Select(i => GetAssemblyInstance(i));
            GroupList = NumGroup.Select(i => GetGroup(i));
            DesignOptionList = NumDesignOption.Select(i => GetDesignOption(i));
            LevelList = NumLevel.Select(i => GetLevel(i));
            PhaseList = NumPhase.Select(i => GetPhase(i));
            RoomList = NumRoom.Select(i => GetRoom(i));
            BimDocumentList = NumBimDocument.Select(i => GetBimDocument(i));
            DisplayUnitInBimDocumentList = NumDisplayUnitInBimDocument.Select(i => GetDisplayUnitInBimDocument(i));
            PhaseOrderInBimDocumentList = NumPhaseOrderInBimDocument.Select(i => GetPhaseOrderInBimDocument(i));
            CategoryList = NumCategory.Select(i => GetCategory(i));
            FamilyList = NumFamily.Select(i => GetFamily(i));
            FamilyTypeList = NumFamilyType.Select(i => GetFamilyType(i));
            FamilyInstanceList = NumFamilyInstance.Select(i => GetFamilyInstance(i));
            ViewList = NumView.Select(i => GetView(i));
            ElementInViewList = NumElementInView.Select(i => GetElementInView(i));
            ShapeInViewList = NumShapeInView.Select(i => GetShapeInView(i));
            AssetInViewList = NumAssetInView.Select(i => GetAssetInView(i));
            LevelInViewList = NumLevelInView.Select(i => GetLevelInView(i));
            CameraList = NumCamera.Select(i => GetCamera(i));
            MaterialList = NumMaterial.Select(i => GetMaterial(i));
            MaterialInElementList = NumMaterialInElement.Select(i => GetMaterialInElement(i));
            CompoundStructureLayerList = NumCompoundStructureLayer.Select(i => GetCompoundStructureLayer(i));
            CompoundStructureList = NumCompoundStructure.Select(i => GetCompoundStructure(i));
            NodeList = NumNode.Select(i => GetNode(i));
            GeometryList = NumGeometry.Select(i => GetGeometry(i));
            ShapeList = NumShape.Select(i => GetShape(i));
            ShapeCollectionList = NumShapeCollection.Select(i => GetShapeCollection(i));
            ShapeInShapeCollectionList = NumShapeInShapeCollection.Select(i => GetShapeInShapeCollection(i));
            SystemList = NumSystem.Select(i => GetSystem(i));
            ElementInSystemList = NumElementInSystem.Select(i => GetElementInSystem(i));
            WarningList = NumWarning.Select(i => GetWarning(i));
            ElementInWarningList = NumElementInWarning.Select(i => GetElementInWarning(i));
            BasePointList = NumBasePoint.Select(i => GetBasePoint(i));
            PhaseFilterList = NumPhaseFilter.Select(i => GetPhaseFilter(i));
            GridList = NumGrid.Select(i => GetGrid(i));
            AreaList = NumArea.Select(i => GetArea(i));
            AreaSchemeList = NumAreaScheme.Select(i => GetAreaScheme(i));
            ScheduleList = NumSchedule.Select(i => GetSchedule(i));
            ScheduleColumnList = NumScheduleColumn.Select(i => GetScheduleColumn(i));
            ScheduleCellList = NumScheduleCell.Select(i => GetScheduleCell(i));
            
            // Initialize element index maps
            ElementIndexMaps = new ElementIndexMaps(this, inParallel);
        }
    } // Document class
    public static class DocumentBuilderExtensions
    {
        public static Func<IEnumerable<Entity>, EntityTableBuilder> GetTableBuilderFunc(this Type type)
        {
            if (type == typeof(Asset)) return ToAssetTableBuilder;
            if (type == typeof(DisplayUnit)) return ToDisplayUnitTableBuilder;
            if (type == typeof(ParameterDescriptor)) return ToParameterDescriptorTableBuilder;
            if (type == typeof(Parameter)) return ToParameterTableBuilder;
            if (type == typeof(Element)) return ToElementTableBuilder;
            if (type == typeof(Workset)) return ToWorksetTableBuilder;
            if (type == typeof(AssemblyInstance)) return ToAssemblyInstanceTableBuilder;
            if (type == typeof(Group)) return ToGroupTableBuilder;
            if (type == typeof(DesignOption)) return ToDesignOptionTableBuilder;
            if (type == typeof(Level)) return ToLevelTableBuilder;
            if (type == typeof(Phase)) return ToPhaseTableBuilder;
            if (type == typeof(Room)) return ToRoomTableBuilder;
            if (type == typeof(BimDocument)) return ToBimDocumentTableBuilder;
            if (type == typeof(DisplayUnitInBimDocument)) return ToDisplayUnitInBimDocumentTableBuilder;
            if (type == typeof(PhaseOrderInBimDocument)) return ToPhaseOrderInBimDocumentTableBuilder;
            if (type == typeof(Category)) return ToCategoryTableBuilder;
            if (type == typeof(Family)) return ToFamilyTableBuilder;
            if (type == typeof(FamilyType)) return ToFamilyTypeTableBuilder;
            if (type == typeof(FamilyInstance)) return ToFamilyInstanceTableBuilder;
            if (type == typeof(View)) return ToViewTableBuilder;
            if (type == typeof(ElementInView)) return ToElementInViewTableBuilder;
            if (type == typeof(ShapeInView)) return ToShapeInViewTableBuilder;
            if (type == typeof(AssetInView)) return ToAssetInViewTableBuilder;
            if (type == typeof(LevelInView)) return ToLevelInViewTableBuilder;
            if (type == typeof(Camera)) return ToCameraTableBuilder;
            if (type == typeof(Material)) return ToMaterialTableBuilder;
            if (type == typeof(MaterialInElement)) return ToMaterialInElementTableBuilder;
            if (type == typeof(CompoundStructureLayer)) return ToCompoundStructureLayerTableBuilder;
            if (type == typeof(CompoundStructure)) return ToCompoundStructureTableBuilder;
            if (type == typeof(Node)) return ToNodeTableBuilder;
            if (type == typeof(Geometry)) return ToGeometryTableBuilder;
            if (type == typeof(Shape)) return ToShapeTableBuilder;
            if (type == typeof(ShapeCollection)) return ToShapeCollectionTableBuilder;
            if (type == typeof(ShapeInShapeCollection)) return ToShapeInShapeCollectionTableBuilder;
            if (type == typeof(System)) return ToSystemTableBuilder;
            if (type == typeof(ElementInSystem)) return ToElementInSystemTableBuilder;
            if (type == typeof(Warning)) return ToWarningTableBuilder;
            if (type == typeof(ElementInWarning)) return ToElementInWarningTableBuilder;
            if (type == typeof(BasePoint)) return ToBasePointTableBuilder;
            if (type == typeof(PhaseFilter)) return ToPhaseFilterTableBuilder;
            if (type == typeof(Grid)) return ToGridTableBuilder;
            if (type == typeof(Area)) return ToAreaTableBuilder;
            if (type == typeof(AreaScheme)) return ToAreaSchemeTableBuilder;
            if (type == typeof(Schedule)) return ToScheduleTableBuilder;
            if (type == typeof(ScheduleColumn)) return ToScheduleColumnTableBuilder;
            if (type == typeof(ScheduleCell)) return ToScheduleCellTableBuilder;
            throw new ArgumentException(nameof(type));
        }
        public static EntityTableBuilder ToAssetTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<Asset>() ?? Enumerable.Empty<Asset>();
            var tb = new EntityTableBuilder("Vim.Asset");
            tb.AddStringColumn("string:BufferName", typedEntities.Select(x => x.BufferName));
            return tb;
        }
        public static EntityTableBuilder ToDisplayUnitTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<DisplayUnit>() ?? Enumerable.Empty<DisplayUnit>();
            var tb = new EntityTableBuilder("Vim.DisplayUnit");
            tb.AddStringColumn("string:Spec", typedEntities.Select(x => x.Spec));
            tb.AddStringColumn("string:Type", typedEntities.Select(x => x.Type));
            tb.AddStringColumn("string:Label", typedEntities.Select(x => x.Label));
            return tb;
        }
        public static EntityTableBuilder ToParameterDescriptorTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<ParameterDescriptor>() ?? Enumerable.Empty<ParameterDescriptor>();
            var tb = new EntityTableBuilder("Vim.ParameterDescriptor");
            tb.AddStringColumn("string:Name", typedEntities.Select(x => x.Name));
            tb.AddStringColumn("string:Group", typedEntities.Select(x => x.Group));
            tb.AddStringColumn("string:ParameterType", typedEntities.Select(x => x.ParameterType));
            tb.AddDataColumn("byte:IsInstance", typedEntities.Select(x => x.IsInstance));
            tb.AddDataColumn("byte:IsShared", typedEntities.Select(x => x.IsShared));
            tb.AddDataColumn("byte:IsReadOnly", typedEntities.Select(x => x.IsReadOnly));
            tb.AddDataColumn("int:Flags", typedEntities.Select(x => x.Flags));
            tb.AddStringColumn("string:Guid", typedEntities.Select(x => x.Guid));
            tb.AddIndexColumn("index:Vim.DisplayUnit:DisplayUnit", typedEntities.Select(x => x._DisplayUnit.Index));
            return tb;
        }
        public static EntityTableBuilder ToParameterTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<Parameter>() ?? Enumerable.Empty<Parameter>();
            var tb = new EntityTableBuilder("Vim.Parameter");
            tb.AddStringColumn("string:Value", typedEntities.Select(x => x.Value));
            tb.AddIndexColumn("index:Vim.ParameterDescriptor:ParameterDescriptor", typedEntities.Select(x => x._ParameterDescriptor.Index));
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToElementTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<Element>() ?? Enumerable.Empty<Element>();
            var tb = new EntityTableBuilder("Vim.Element");
            tb.AddDataColumn("int:Id", typedEntities.Select(x => x.Id));
            tb.AddStringColumn("string:Type", typedEntities.Select(x => x.Type));
            tb.AddStringColumn("string:Name", typedEntities.Select(x => x.Name));
            tb.AddStringColumn("string:UniqueId", typedEntities.Select(x => x.UniqueId));
            tb.AddCompositeDataColumns("Location", typedEntities.Select(x => x.Location));
            tb.AddStringColumn("string:FamilyName", typedEntities.Select(x => x.FamilyName));
            tb.AddDataColumn("byte:IsPinned", typedEntities.Select(x => x.IsPinned));
            tb.AddIndexColumn("index:Vim.Level:Level", typedEntities.Select(x => x._Level.Index));
            tb.AddIndexColumn("index:Vim.Phase:PhaseCreated", typedEntities.Select(x => x._PhaseCreated.Index));
            tb.AddIndexColumn("index:Vim.Phase:PhaseDemolished", typedEntities.Select(x => x._PhaseDemolished.Index));
            tb.AddIndexColumn("index:Vim.Category:Category", typedEntities.Select(x => x._Category.Index));
            tb.AddIndexColumn("index:Vim.Workset:Workset", typedEntities.Select(x => x._Workset.Index));
            tb.AddIndexColumn("index:Vim.DesignOption:DesignOption", typedEntities.Select(x => x._DesignOption.Index));
            tb.AddIndexColumn("index:Vim.View:OwnerView", typedEntities.Select(x => x._OwnerView.Index));
            tb.AddIndexColumn("index:Vim.Group:Group", typedEntities.Select(x => x._Group.Index));
            tb.AddIndexColumn("index:Vim.AssemblyInstance:AssemblyInstance", typedEntities.Select(x => x._AssemblyInstance.Index));
            tb.AddIndexColumn("index:Vim.BimDocument:BimDocument", typedEntities.Select(x => x._BimDocument.Index));
            tb.AddIndexColumn("index:Vim.Room:Room", typedEntities.Select(x => x._Room.Index));
            return tb;
        }
        public static EntityTableBuilder ToWorksetTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<Workset>() ?? Enumerable.Empty<Workset>();
            var tb = new EntityTableBuilder("Vim.Workset");
            tb.AddDataColumn("int:Id", typedEntities.Select(x => x.Id));
            tb.AddStringColumn("string:Name", typedEntities.Select(x => x.Name));
            tb.AddStringColumn("string:Kind", typedEntities.Select(x => x.Kind));
            tb.AddDataColumn("byte:IsOpen", typedEntities.Select(x => x.IsOpen));
            tb.AddDataColumn("byte:IsEditable", typedEntities.Select(x => x.IsEditable));
            tb.AddStringColumn("string:Owner", typedEntities.Select(x => x.Owner));
            tb.AddStringColumn("string:UniqueId", typedEntities.Select(x => x.UniqueId));
            tb.AddIndexColumn("index:Vim.BimDocument:BimDocument", typedEntities.Select(x => x._BimDocument.Index));
            return tb;
        }
        public static EntityTableBuilder ToAssemblyInstanceTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<AssemblyInstance>() ?? Enumerable.Empty<AssemblyInstance>();
            var tb = new EntityTableBuilder("Vim.AssemblyInstance");
            tb.AddStringColumn("string:AssemblyTypeName", typedEntities.Select(x => x.AssemblyTypeName));
            tb.AddCompositeDataColumns("Position", typedEntities.Select(x => x.Position));
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToGroupTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<Group>() ?? Enumerable.Empty<Group>();
            var tb = new EntityTableBuilder("Vim.Group");
            tb.AddStringColumn("string:GroupType", typedEntities.Select(x => x.GroupType));
            tb.AddCompositeDataColumns("Position", typedEntities.Select(x => x.Position));
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToDesignOptionTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<DesignOption>() ?? Enumerable.Empty<DesignOption>();
            var tb = new EntityTableBuilder("Vim.DesignOption");
            tb.AddDataColumn("byte:IsPrimary", typedEntities.Select(x => x.IsPrimary));
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToLevelTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<Level>() ?? Enumerable.Empty<Level>();
            var tb = new EntityTableBuilder("Vim.Level");
            tb.AddDataColumn("double:Elevation", typedEntities.Select(x => x.Elevation));
            tb.AddIndexColumn("index:Vim.FamilyType:FamilyType", typedEntities.Select(x => x._FamilyType.Index));
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToPhaseTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<Phase>() ?? Enumerable.Empty<Phase>();
            var tb = new EntityTableBuilder("Vim.Phase");
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToRoomTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<Room>() ?? Enumerable.Empty<Room>();
            var tb = new EntityTableBuilder("Vim.Room");
            tb.AddDataColumn("double:BaseOffset", typedEntities.Select(x => x.BaseOffset));
            tb.AddDataColumn("double:LimitOffset", typedEntities.Select(x => x.LimitOffset));
            tb.AddDataColumn("double:UnboundedHeight", typedEntities.Select(x => x.UnboundedHeight));
            tb.AddDataColumn("double:Volume", typedEntities.Select(x => x.Volume));
            tb.AddDataColumn("double:Perimeter", typedEntities.Select(x => x.Perimeter));
            tb.AddDataColumn("double:Area", typedEntities.Select(x => x.Area));
            tb.AddStringColumn("string:Number", typedEntities.Select(x => x.Number));
            tb.AddIndexColumn("index:Vim.Level:UpperLimit", typedEntities.Select(x => x._UpperLimit.Index));
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToBimDocumentTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<BimDocument>() ?? Enumerable.Empty<BimDocument>();
            var tb = new EntityTableBuilder("Vim.BimDocument");
            tb.AddStringColumn("string:Title", typedEntities.Select(x => x.Title));
            tb.AddDataColumn("byte:IsMetric", typedEntities.Select(x => x.IsMetric));
            tb.AddStringColumn("string:Guid", typedEntities.Select(x => x.Guid));
            tb.AddDataColumn("int:NumSaves", typedEntities.Select(x => x.NumSaves));
            tb.AddDataColumn("byte:IsLinked", typedEntities.Select(x => x.IsLinked));
            tb.AddDataColumn("byte:IsDetached", typedEntities.Select(x => x.IsDetached));
            tb.AddDataColumn("byte:IsWorkshared", typedEntities.Select(x => x.IsWorkshared));
            tb.AddStringColumn("string:PathName", typedEntities.Select(x => x.PathName));
            tb.AddDataColumn("double:Latitude", typedEntities.Select(x => x.Latitude));
            tb.AddDataColumn("double:Longitude", typedEntities.Select(x => x.Longitude));
            tb.AddDataColumn("double:TimeZone", typedEntities.Select(x => x.TimeZone));
            tb.AddStringColumn("string:PlaceName", typedEntities.Select(x => x.PlaceName));
            tb.AddStringColumn("string:WeatherStationName", typedEntities.Select(x => x.WeatherStationName));
            tb.AddDataColumn("double:Elevation", typedEntities.Select(x => x.Elevation));
            tb.AddStringColumn("string:ProjectLocation", typedEntities.Select(x => x.ProjectLocation));
            tb.AddStringColumn("string:IssueDate", typedEntities.Select(x => x.IssueDate));
            tb.AddStringColumn("string:Status", typedEntities.Select(x => x.Status));
            tb.AddStringColumn("string:ClientName", typedEntities.Select(x => x.ClientName));
            tb.AddStringColumn("string:Address", typedEntities.Select(x => x.Address));
            tb.AddStringColumn("string:Name", typedEntities.Select(x => x.Name));
            tb.AddStringColumn("string:Number", typedEntities.Select(x => x.Number));
            tb.AddStringColumn("string:Author", typedEntities.Select(x => x.Author));
            tb.AddStringColumn("string:BuildingName", typedEntities.Select(x => x.BuildingName));
            tb.AddStringColumn("string:OrganizationName", typedEntities.Select(x => x.OrganizationName));
            tb.AddStringColumn("string:OrganizationDescription", typedEntities.Select(x => x.OrganizationDescription));
            tb.AddStringColumn("string:Product", typedEntities.Select(x => x.Product));
            tb.AddStringColumn("string:Version", typedEntities.Select(x => x.Version));
            tb.AddStringColumn("string:User", typedEntities.Select(x => x.User));
            tb.AddIndexColumn("index:Vim.View:ActiveView", typedEntities.Select(x => x._ActiveView.Index));
            tb.AddIndexColumn("index:Vim.Family:OwnerFamily", typedEntities.Select(x => x._OwnerFamily.Index));
            tb.AddIndexColumn("index:Vim.BimDocument:Parent", typedEntities.Select(x => x._Parent.Index));
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToDisplayUnitInBimDocumentTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<DisplayUnitInBimDocument>() ?? Enumerable.Empty<DisplayUnitInBimDocument>();
            var tb = new EntityTableBuilder("Vim.DisplayUnitInBimDocument");
            tb.AddIndexColumn("index:Vim.DisplayUnit:DisplayUnit", typedEntities.Select(x => x._DisplayUnit.Index));
            tb.AddIndexColumn("index:Vim.BimDocument:BimDocument", typedEntities.Select(x => x._BimDocument.Index));
            return tb;
        }
        public static EntityTableBuilder ToPhaseOrderInBimDocumentTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<PhaseOrderInBimDocument>() ?? Enumerable.Empty<PhaseOrderInBimDocument>();
            var tb = new EntityTableBuilder("Vim.PhaseOrderInBimDocument");
            tb.AddDataColumn("int:OrderIndex", typedEntities.Select(x => x.OrderIndex));
            tb.AddIndexColumn("index:Vim.Phase:Phase", typedEntities.Select(x => x._Phase.Index));
            tb.AddIndexColumn("index:Vim.BimDocument:BimDocument", typedEntities.Select(x => x._BimDocument.Index));
            return tb;
        }
        public static EntityTableBuilder ToCategoryTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<Category>() ?? Enumerable.Empty<Category>();
            var tb = new EntityTableBuilder("Vim.Category");
            tb.AddStringColumn("string:Name", typedEntities.Select(x => x.Name));
            tb.AddDataColumn("int:Id", typedEntities.Select(x => x.Id));
            tb.AddStringColumn("string:CategoryType", typedEntities.Select(x => x.CategoryType));
            tb.AddCompositeDataColumns("LineColor", typedEntities.Select(x => x.LineColor));
            tb.AddStringColumn("string:BuiltInCategory", typedEntities.Select(x => x.BuiltInCategory));
            tb.AddIndexColumn("index:Vim.Category:Parent", typedEntities.Select(x => x._Parent.Index));
            tb.AddIndexColumn("index:Vim.Material:Material", typedEntities.Select(x => x._Material.Index));
            return tb;
        }
        public static EntityTableBuilder ToFamilyTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<Family>() ?? Enumerable.Empty<Family>();
            var tb = new EntityTableBuilder("Vim.Family");
            tb.AddStringColumn("string:StructuralMaterialType", typedEntities.Select(x => x.StructuralMaterialType));
            tb.AddStringColumn("string:StructuralSectionShape", typedEntities.Select(x => x.StructuralSectionShape));
            tb.AddDataColumn("byte:IsSystemFamily", typedEntities.Select(x => x.IsSystemFamily));
            tb.AddDataColumn("byte:IsInPlace", typedEntities.Select(x => x.IsInPlace));
            tb.AddIndexColumn("index:Vim.Category:FamilyCategory", typedEntities.Select(x => x._FamilyCategory.Index));
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToFamilyTypeTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<FamilyType>() ?? Enumerable.Empty<FamilyType>();
            var tb = new EntityTableBuilder("Vim.FamilyType");
            tb.AddDataColumn("byte:IsSystemFamilyType", typedEntities.Select(x => x.IsSystemFamilyType));
            tb.AddIndexColumn("index:Vim.Family:Family", typedEntities.Select(x => x._Family.Index));
            tb.AddIndexColumn("index:Vim.CompoundStructure:CompoundStructure", typedEntities.Select(x => x._CompoundStructure.Index));
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToFamilyInstanceTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<FamilyInstance>() ?? Enumerable.Empty<FamilyInstance>();
            var tb = new EntityTableBuilder("Vim.FamilyInstance");
            tb.AddDataColumn("byte:FacingFlipped", typedEntities.Select(x => x.FacingFlipped));
            tb.AddCompositeDataColumns("FacingOrientation", typedEntities.Select(x => x.FacingOrientation));
            tb.AddDataColumn("byte:HandFlipped", typedEntities.Select(x => x.HandFlipped));
            tb.AddDataColumn("byte:Mirrored", typedEntities.Select(x => x.Mirrored));
            tb.AddDataColumn("byte:HasModifiedGeometry", typedEntities.Select(x => x.HasModifiedGeometry));
            tb.AddDataColumn("float:Scale", typedEntities.Select(x => x.Scale));
            tb.AddCompositeDataColumns("BasisX", typedEntities.Select(x => x.BasisX));
            tb.AddCompositeDataColumns("BasisY", typedEntities.Select(x => x.BasisY));
            tb.AddCompositeDataColumns("BasisZ", typedEntities.Select(x => x.BasisZ));
            tb.AddCompositeDataColumns("Translation", typedEntities.Select(x => x.Translation));
            tb.AddCompositeDataColumns("HandOrientation", typedEntities.Select(x => x.HandOrientation));
            tb.AddIndexColumn("index:Vim.FamilyType:FamilyType", typedEntities.Select(x => x._FamilyType.Index));
            tb.AddIndexColumn("index:Vim.Element:Host", typedEntities.Select(x => x._Host.Index));
            tb.AddIndexColumn("index:Vim.Room:FromRoom", typedEntities.Select(x => x._FromRoom.Index));
            tb.AddIndexColumn("index:Vim.Room:ToRoom", typedEntities.Select(x => x._ToRoom.Index));
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToViewTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<View>() ?? Enumerable.Empty<View>();
            var tb = new EntityTableBuilder("Vim.View");
            tb.AddStringColumn("string:Title", typedEntities.Select(x => x.Title));
            tb.AddStringColumn("string:ViewType", typedEntities.Select(x => x.ViewType));
            tb.AddCompositeDataColumns("Up", typedEntities.Select(x => x.Up));
            tb.AddCompositeDataColumns("Right", typedEntities.Select(x => x.Right));
            tb.AddCompositeDataColumns("Origin", typedEntities.Select(x => x.Origin));
            tb.AddCompositeDataColumns("ViewDirection", typedEntities.Select(x => x.ViewDirection));
            tb.AddCompositeDataColumns("ViewPosition", typedEntities.Select(x => x.ViewPosition));
            tb.AddDataColumn("double:Scale", typedEntities.Select(x => x.Scale));
            tb.AddCompositeDataColumns("Outline", typedEntities.Select(x => x.Outline));
            tb.AddDataColumn("int:DetailLevel", typedEntities.Select(x => x.DetailLevel));
            tb.AddIndexColumn("index:Vim.Camera:Camera", typedEntities.Select(x => x._Camera.Index));
            tb.AddIndexColumn("index:Vim.FamilyType:FamilyType", typedEntities.Select(x => x._FamilyType.Index));
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToElementInViewTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<ElementInView>() ?? Enumerable.Empty<ElementInView>();
            var tb = new EntityTableBuilder("Vim.ElementInView");
            tb.AddIndexColumn("index:Vim.View:View", typedEntities.Select(x => x._View.Index));
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToShapeInViewTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<ShapeInView>() ?? Enumerable.Empty<ShapeInView>();
            var tb = new EntityTableBuilder("Vim.ShapeInView");
            tb.AddIndexColumn("index:Vim.Shape:Shape", typedEntities.Select(x => x._Shape.Index));
            tb.AddIndexColumn("index:Vim.View:View", typedEntities.Select(x => x._View.Index));
            return tb;
        }
        public static EntityTableBuilder ToAssetInViewTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<AssetInView>() ?? Enumerable.Empty<AssetInView>();
            var tb = new EntityTableBuilder("Vim.AssetInView");
            tb.AddIndexColumn("index:Vim.Asset:Asset", typedEntities.Select(x => x._Asset.Index));
            tb.AddIndexColumn("index:Vim.View:View", typedEntities.Select(x => x._View.Index));
            return tb;
        }
        public static EntityTableBuilder ToLevelInViewTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<LevelInView>() ?? Enumerable.Empty<LevelInView>();
            var tb = new EntityTableBuilder("Vim.LevelInView");
            tb.AddCompositeDataColumns("Extents", typedEntities.Select(x => x.Extents));
            tb.AddIndexColumn("index:Vim.Level:Level", typedEntities.Select(x => x._Level.Index));
            tb.AddIndexColumn("index:Vim.View:View", typedEntities.Select(x => x._View.Index));
            return tb;
        }
        public static EntityTableBuilder ToCameraTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<Camera>() ?? Enumerable.Empty<Camera>();
            var tb = new EntityTableBuilder("Vim.Camera");
            tb.AddDataColumn("int:Id", typedEntities.Select(x => x.Id));
            tb.AddDataColumn("int:IsPerspective", typedEntities.Select(x => x.IsPerspective));
            tb.AddDataColumn("double:VerticalExtent", typedEntities.Select(x => x.VerticalExtent));
            tb.AddDataColumn("double:HorizontalExtent", typedEntities.Select(x => x.HorizontalExtent));
            tb.AddDataColumn("double:FarDistance", typedEntities.Select(x => x.FarDistance));
            tb.AddDataColumn("double:NearDistance", typedEntities.Select(x => x.NearDistance));
            tb.AddDataColumn("double:TargetDistance", typedEntities.Select(x => x.TargetDistance));
            tb.AddDataColumn("double:RightOffset", typedEntities.Select(x => x.RightOffset));
            tb.AddDataColumn("double:UpOffset", typedEntities.Select(x => x.UpOffset));
            return tb;
        }
        public static EntityTableBuilder ToMaterialTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<Material>() ?? Enumerable.Empty<Material>();
            var tb = new EntityTableBuilder("Vim.Material");
            tb.AddStringColumn("string:Name", typedEntities.Select(x => x.Name));
            tb.AddStringColumn("string:MaterialCategory", typedEntities.Select(x => x.MaterialCategory));
            tb.AddCompositeDataColumns("Color", typedEntities.Select(x => x.Color));
            tb.AddCompositeDataColumns("ColorUvScaling", typedEntities.Select(x => x.ColorUvScaling));
            tb.AddCompositeDataColumns("ColorUvOffset", typedEntities.Select(x => x.ColorUvOffset));
            tb.AddCompositeDataColumns("NormalUvScaling", typedEntities.Select(x => x.NormalUvScaling));
            tb.AddCompositeDataColumns("NormalUvOffset", typedEntities.Select(x => x.NormalUvOffset));
            tb.AddDataColumn("double:NormalAmount", typedEntities.Select(x => x.NormalAmount));
            tb.AddDataColumn("double:Glossiness", typedEntities.Select(x => x.Glossiness));
            tb.AddDataColumn("double:Smoothness", typedEntities.Select(x => x.Smoothness));
            tb.AddDataColumn("double:Transparency", typedEntities.Select(x => x.Transparency));
            tb.AddIndexColumn("index:Vim.Asset:ColorTextureFile", typedEntities.Select(x => x._ColorTextureFile.Index));
            tb.AddIndexColumn("index:Vim.Asset:NormalTextureFile", typedEntities.Select(x => x._NormalTextureFile.Index));
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToMaterialInElementTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<MaterialInElement>() ?? Enumerable.Empty<MaterialInElement>();
            var tb = new EntityTableBuilder("Vim.MaterialInElement");
            tb.AddDataColumn("double:Area", typedEntities.Select(x => x.Area));
            tb.AddDataColumn("double:Volume", typedEntities.Select(x => x.Volume));
            tb.AddDataColumn("byte:IsPaint", typedEntities.Select(x => x.IsPaint));
            tb.AddIndexColumn("index:Vim.Material:Material", typedEntities.Select(x => x._Material.Index));
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToCompoundStructureLayerTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<CompoundStructureLayer>() ?? Enumerable.Empty<CompoundStructureLayer>();
            var tb = new EntityTableBuilder("Vim.CompoundStructureLayer");
            tb.AddDataColumn("int:OrderIndex", typedEntities.Select(x => x.OrderIndex));
            tb.AddDataColumn("double:Width", typedEntities.Select(x => x.Width));
            tb.AddStringColumn("string:MaterialFunctionAssignment", typedEntities.Select(x => x.MaterialFunctionAssignment));
            tb.AddIndexColumn("index:Vim.Material:Material", typedEntities.Select(x => x._Material.Index));
            tb.AddIndexColumn("index:Vim.CompoundStructure:CompoundStructure", typedEntities.Select(x => x._CompoundStructure.Index));
            return tb;
        }
        public static EntityTableBuilder ToCompoundStructureTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<CompoundStructure>() ?? Enumerable.Empty<CompoundStructure>();
            var tb = new EntityTableBuilder("Vim.CompoundStructure");
            tb.AddDataColumn("double:Width", typedEntities.Select(x => x.Width));
            tb.AddIndexColumn("index:Vim.CompoundStructureLayer:StructuralLayer", typedEntities.Select(x => x._StructuralLayer.Index));
            return tb;
        }
        public static EntityTableBuilder ToNodeTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<Node>() ?? Enumerable.Empty<Node>();
            var tb = new EntityTableBuilder("Vim.Node");
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToGeometryTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<Geometry>() ?? Enumerable.Empty<Geometry>();
            var tb = new EntityTableBuilder("Vim.Geometry");
            tb.AddCompositeDataColumns("Box", typedEntities.Select(x => x.Box));
            tb.AddDataColumn("int:VertexCount", typedEntities.Select(x => x.VertexCount));
            tb.AddDataColumn("int:FaceCount", typedEntities.Select(x => x.FaceCount));
            return tb;
        }
        public static EntityTableBuilder ToShapeTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<Shape>() ?? Enumerable.Empty<Shape>();
            var tb = new EntityTableBuilder("Vim.Shape");
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToShapeCollectionTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<ShapeCollection>() ?? Enumerable.Empty<ShapeCollection>();
            var tb = new EntityTableBuilder("Vim.ShapeCollection");
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToShapeInShapeCollectionTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<ShapeInShapeCollection>() ?? Enumerable.Empty<ShapeInShapeCollection>();
            var tb = new EntityTableBuilder("Vim.ShapeInShapeCollection");
            tb.AddIndexColumn("index:Vim.Shape:Shape", typedEntities.Select(x => x._Shape.Index));
            tb.AddIndexColumn("index:Vim.ShapeCollection:ShapeCollection", typedEntities.Select(x => x._ShapeCollection.Index));
            return tb;
        }
        public static EntityTableBuilder ToSystemTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<System>() ?? Enumerable.Empty<System>();
            var tb = new EntityTableBuilder("Vim.System");
            tb.AddDataColumn("int:SystemType", typedEntities.Select(x => x.SystemType));
            tb.AddIndexColumn("index:Vim.FamilyType:FamilyType", typedEntities.Select(x => x._FamilyType.Index));
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToElementInSystemTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<ElementInSystem>() ?? Enumerable.Empty<ElementInSystem>();
            var tb = new EntityTableBuilder("Vim.ElementInSystem");
            tb.AddDataColumn("int:Roles", typedEntities.Select(x => x.Roles));
            tb.AddIndexColumn("index:Vim.System:System", typedEntities.Select(x => x._System.Index));
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToWarningTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<Warning>() ?? Enumerable.Empty<Warning>();
            var tb = new EntityTableBuilder("Vim.Warning");
            tb.AddStringColumn("string:Guid", typedEntities.Select(x => x.Guid));
            tb.AddStringColumn("string:Severity", typedEntities.Select(x => x.Severity));
            tb.AddStringColumn("string:Description", typedEntities.Select(x => x.Description));
            tb.AddIndexColumn("index:Vim.BimDocument:BimDocument", typedEntities.Select(x => x._BimDocument.Index));
            return tb;
        }
        public static EntityTableBuilder ToElementInWarningTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<ElementInWarning>() ?? Enumerable.Empty<ElementInWarning>();
            var tb = new EntityTableBuilder("Vim.ElementInWarning");
            tb.AddIndexColumn("index:Vim.Warning:Warning", typedEntities.Select(x => x._Warning.Index));
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToBasePointTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<BasePoint>() ?? Enumerable.Empty<BasePoint>();
            var tb = new EntityTableBuilder("Vim.BasePoint");
            tb.AddDataColumn("byte:IsSurveyPoint", typedEntities.Select(x => x.IsSurveyPoint));
            tb.AddCompositeDataColumns("Position", typedEntities.Select(x => x.Position));
            tb.AddCompositeDataColumns("SharedPosition", typedEntities.Select(x => x.SharedPosition));
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToPhaseFilterTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<PhaseFilter>() ?? Enumerable.Empty<PhaseFilter>();
            var tb = new EntityTableBuilder("Vim.PhaseFilter");
            tb.AddDataColumn("int:New", typedEntities.Select(x => x.New));
            tb.AddDataColumn("int:Existing", typedEntities.Select(x => x.Existing));
            tb.AddDataColumn("int:Demolished", typedEntities.Select(x => x.Demolished));
            tb.AddDataColumn("int:Temporary", typedEntities.Select(x => x.Temporary));
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToGridTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<Grid>() ?? Enumerable.Empty<Grid>();
            var tb = new EntityTableBuilder("Vim.Grid");
            tb.AddCompositeDataColumns("StartPoint", typedEntities.Select(x => x.StartPoint));
            tb.AddCompositeDataColumns("EndPoint", typedEntities.Select(x => x.EndPoint));
            tb.AddDataColumn("byte:IsCurved", typedEntities.Select(x => x.IsCurved));
            tb.AddCompositeDataColumns("Extents", typedEntities.Select(x => x.Extents));
            tb.AddIndexColumn("index:Vim.FamilyType:FamilyType", typedEntities.Select(x => x._FamilyType.Index));
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToAreaTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<Area>() ?? Enumerable.Empty<Area>();
            var tb = new EntityTableBuilder("Vim.Area");
            tb.AddDataColumn("double:Value", typedEntities.Select(x => x.Value));
            tb.AddDataColumn("double:Perimeter", typedEntities.Select(x => x.Perimeter));
            tb.AddStringColumn("string:Number", typedEntities.Select(x => x.Number));
            tb.AddDataColumn("byte:IsGrossInterior", typedEntities.Select(x => x.IsGrossInterior));
            tb.AddIndexColumn("index:Vim.AreaScheme:AreaScheme", typedEntities.Select(x => x._AreaScheme.Index));
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToAreaSchemeTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<AreaScheme>() ?? Enumerable.Empty<AreaScheme>();
            var tb = new EntityTableBuilder("Vim.AreaScheme");
            tb.AddDataColumn("byte:IsGrossBuildingArea", typedEntities.Select(x => x.IsGrossBuildingArea));
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToScheduleTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<Schedule>() ?? Enumerable.Empty<Schedule>();
            var tb = new EntityTableBuilder("Vim.Schedule");
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToScheduleColumnTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<ScheduleColumn>() ?? Enumerable.Empty<ScheduleColumn>();
            var tb = new EntityTableBuilder("Vim.ScheduleColumn");
            tb.AddStringColumn("string:Name", typedEntities.Select(x => x.Name));
            tb.AddDataColumn("int:ColumnIndex", typedEntities.Select(x => x.ColumnIndex));
            tb.AddIndexColumn("index:Vim.Schedule:Schedule", typedEntities.Select(x => x._Schedule.Index));
            return tb;
        }
        public static EntityTableBuilder ToScheduleCellTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<ScheduleCell>() ?? Enumerable.Empty<ScheduleCell>();
            var tb = new EntityTableBuilder("Vim.ScheduleCell");
            tb.AddStringColumn("string:Value", typedEntities.Select(x => x.Value));
            tb.AddDataColumn("int:RowIndex", typedEntities.Select(x => x.RowIndex));
            tb.AddIndexColumn("index:Vim.ScheduleColumn:ScheduleColumn", typedEntities.Select(x => x._ScheduleColumn.Index));
            return tb;
        }
    } // DocumentBuilderExtensions
    
    public partial class ObjectModelBuilder
    {
        public readonly Dictionary<Type, EntityTableBuilder> EntityTableBuilders = new Dictionary<Type, EntityTableBuilder>()
        {
            {typeof(Asset), new EntityTableBuilder()},
            {typeof(DisplayUnit), new EntityTableBuilder()},
            {typeof(ParameterDescriptor), new EntityTableBuilder()},
            {typeof(Parameter), new EntityTableBuilder()},
            {typeof(Element), new EntityTableBuilder()},
            {typeof(Workset), new EntityTableBuilder()},
            {typeof(AssemblyInstance), new EntityTableBuilder()},
            {typeof(Group), new EntityTableBuilder()},
            {typeof(DesignOption), new EntityTableBuilder()},
            {typeof(Level), new EntityTableBuilder()},
            {typeof(Phase), new EntityTableBuilder()},
            {typeof(Room), new EntityTableBuilder()},
            {typeof(BimDocument), new EntityTableBuilder()},
            {typeof(DisplayUnitInBimDocument), new EntityTableBuilder()},
            {typeof(PhaseOrderInBimDocument), new EntityTableBuilder()},
            {typeof(Category), new EntityTableBuilder()},
            {typeof(Family), new EntityTableBuilder()},
            {typeof(FamilyType), new EntityTableBuilder()},
            {typeof(FamilyInstance), new EntityTableBuilder()},
            {typeof(View), new EntityTableBuilder()},
            {typeof(ElementInView), new EntityTableBuilder()},
            {typeof(ShapeInView), new EntityTableBuilder()},
            {typeof(AssetInView), new EntityTableBuilder()},
            {typeof(LevelInView), new EntityTableBuilder()},
            {typeof(Camera), new EntityTableBuilder()},
            {typeof(Material), new EntityTableBuilder()},
            {typeof(MaterialInElement), new EntityTableBuilder()},
            {typeof(CompoundStructureLayer), new EntityTableBuilder()},
            {typeof(CompoundStructure), new EntityTableBuilder()},
            {typeof(Node), new EntityTableBuilder()},
            {typeof(Geometry), new EntityTableBuilder()},
            {typeof(Shape), new EntityTableBuilder()},
            {typeof(ShapeCollection), new EntityTableBuilder()},
            {typeof(ShapeInShapeCollection), new EntityTableBuilder()},
            {typeof(System), new EntityTableBuilder()},
            {typeof(ElementInSystem), new EntityTableBuilder()},
            {typeof(Warning), new EntityTableBuilder()},
            {typeof(ElementInWarning), new EntityTableBuilder()},
            {typeof(BasePoint), new EntityTableBuilder()},
            {typeof(PhaseFilter), new EntityTableBuilder()},
            {typeof(Grid), new EntityTableBuilder()},
            {typeof(Area), new EntityTableBuilder()},
            {typeof(AreaScheme), new EntityTableBuilder()},
            {typeof(Schedule), new EntityTableBuilder()},
            {typeof(ScheduleColumn), new EntityTableBuilder()},
            {typeof(ScheduleCell), new EntityTableBuilder()},
        };
    } // ObjectModelBuilder
} // namespace