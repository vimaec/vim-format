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
        public Vim.Format.ObjectModel.DisplayUnit DisplayUnit => _DisplayUnit.Value;
        public ParameterDescriptor()
        {
            _DisplayUnit = new Relation<Vim.Format.ObjectModel.DisplayUnit>();
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
        public Vim.Format.ObjectModel.ParameterDescriptor ParameterDescriptor => _ParameterDescriptor.Value;
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public Parameter()
        {
            _ParameterDescriptor = new Relation<Vim.Format.ObjectModel.ParameterDescriptor>();
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
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
        public Vim.Format.ObjectModel.Level Level => _Level.Value;
        public Vim.Format.ObjectModel.Phase PhaseCreated => _PhaseCreated.Value;
        public Vim.Format.ObjectModel.Phase PhaseDemolished => _PhaseDemolished.Value;
        public Vim.Format.ObjectModel.Category Category => _Category.Value;
        public Vim.Format.ObjectModel.Workset Workset => _Workset.Value;
        public Vim.Format.ObjectModel.DesignOption DesignOption => _DesignOption.Value;
        public Vim.Format.ObjectModel.View OwnerView => _OwnerView.Value;
        public Vim.Format.ObjectModel.Group Group => _Group.Value;
        public Vim.Format.ObjectModel.AssemblyInstance AssemblyInstance => _AssemblyInstance.Value;
        public Vim.Format.ObjectModel.BimDocument BimDocument => _BimDocument.Value;
        public Vim.Format.ObjectModel.Room Room => _Room.Value;
        public Element()
        {
            _Level = new Relation<Vim.Format.ObjectModel.Level>();
            _PhaseCreated = new Relation<Vim.Format.ObjectModel.Phase>();
            _PhaseDemolished = new Relation<Vim.Format.ObjectModel.Phase>();
            _Category = new Relation<Vim.Format.ObjectModel.Category>();
            _Workset = new Relation<Vim.Format.ObjectModel.Workset>();
            _DesignOption = new Relation<Vim.Format.ObjectModel.DesignOption>();
            _OwnerView = new Relation<Vim.Format.ObjectModel.View>();
            _Group = new Relation<Vim.Format.ObjectModel.Group>();
            _AssemblyInstance = new Relation<Vim.Format.ObjectModel.AssemblyInstance>();
            _BimDocument = new Relation<Vim.Format.ObjectModel.BimDocument>();
            _Room = new Relation<Vim.Format.ObjectModel.Room>();
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
                    (Location_X == other.Location_X) &&
                    (Location_Y == other.Location_Y) &&
                    (Location_Z == other.Location_Z) &&
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
        public Vim.Format.ObjectModel.BimDocument BimDocument => _BimDocument.Value;
        public Workset()
        {
            _BimDocument = new Relation<Vim.Format.ObjectModel.BimDocument>();
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
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public AssemblyInstance()
        {
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is AssemblyInstance other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (AssemblyTypeName == other.AssemblyTypeName) &&
                    (Position_X == other.Position_X) &&
                    (Position_Y == other.Position_Y) &&
                    (Position_Z == other.Position_Z) &&
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
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public Group()
        {
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Group other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (GroupType == other.GroupType) &&
                    (Position_X == other.Position_X) &&
                    (Position_Y == other.Position_Y) &&
                    (Position_Z == other.Position_Z) &&
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
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public DesignOption()
        {
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
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
        public Vim.Format.ObjectModel.FamilyType FamilyType => _FamilyType.Value;
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public Level()
        {
            _FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>();
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
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
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public Phase()
        {
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
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
        public Vim.Format.ObjectModel.Level UpperLimit => _UpperLimit.Value;
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public Room()
        {
            _UpperLimit = new Relation<Vim.Format.ObjectModel.Level>();
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
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
        public Vim.Format.ObjectModel.View ActiveView => _ActiveView.Value;
        public Vim.Format.ObjectModel.Family OwnerFamily => _OwnerFamily.Value;
        public Vim.Format.ObjectModel.BimDocument Parent => _Parent.Value;
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public BimDocument()
        {
            _ActiveView = new Relation<Vim.Format.ObjectModel.View>();
            _OwnerFamily = new Relation<Vim.Format.ObjectModel.Family>();
            _Parent = new Relation<Vim.Format.ObjectModel.BimDocument>();
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
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
        public Vim.Format.ObjectModel.DisplayUnit DisplayUnit => _DisplayUnit.Value;
        public Vim.Format.ObjectModel.BimDocument BimDocument => _BimDocument.Value;
        public DisplayUnitInBimDocument()
        {
            _DisplayUnit = new Relation<Vim.Format.ObjectModel.DisplayUnit>();
            _BimDocument = new Relation<Vim.Format.ObjectModel.BimDocument>();
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
        public Vim.Format.ObjectModel.Phase Phase => _Phase.Value;
        public Vim.Format.ObjectModel.BimDocument BimDocument => _BimDocument.Value;
        public PhaseOrderInBimDocument()
        {
            _Phase = new Relation<Vim.Format.ObjectModel.Phase>();
            _BimDocument = new Relation<Vim.Format.ObjectModel.BimDocument>();
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
        public Vim.Format.ObjectModel.Category Parent => _Parent.Value;
        public Vim.Format.ObjectModel.Material Material => _Material.Value;
        public Category()
        {
            _Parent = new Relation<Vim.Format.ObjectModel.Category>();
            _Material = new Relation<Vim.Format.ObjectModel.Material>();
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
                    (LineColor_X == other.LineColor_X) &&
                    (LineColor_Y == other.LineColor_Y) &&
                    (LineColor_Z == other.LineColor_Z) &&
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
        public Vim.Format.ObjectModel.Category FamilyCategory => _FamilyCategory.Value;
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public Family()
        {
            _FamilyCategory = new Relation<Vim.Format.ObjectModel.Category>();
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
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
        public Vim.Format.ObjectModel.Family Family => _Family.Value;
        public Vim.Format.ObjectModel.CompoundStructure CompoundStructure => _CompoundStructure.Value;
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public FamilyType()
        {
            _Family = new Relation<Vim.Format.ObjectModel.Family>();
            _CompoundStructure = new Relation<Vim.Format.ObjectModel.CompoundStructure>();
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
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
        public Vim.Format.ObjectModel.FamilyType FamilyType => _FamilyType.Value;
        public Vim.Format.ObjectModel.Element Host => _Host.Value;
        public Vim.Format.ObjectModel.Room FromRoom => _FromRoom.Value;
        public Vim.Format.ObjectModel.Room ToRoom => _ToRoom.Value;
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public FamilyInstance()
        {
            _FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>();
            _Host = new Relation<Vim.Format.ObjectModel.Element>();
            _FromRoom = new Relation<Vim.Format.ObjectModel.Room>();
            _ToRoom = new Relation<Vim.Format.ObjectModel.Room>();
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is FamilyInstance other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (FacingFlipped == other.FacingFlipped) &&
                    (FacingOrientation_X == other.FacingOrientation_X) &&
                    (FacingOrientation_Y == other.FacingOrientation_Y) &&
                    (FacingOrientation_Z == other.FacingOrientation_Z) &&
                    (HandFlipped == other.HandFlipped) &&
                    (Mirrored == other.Mirrored) &&
                    (HasModifiedGeometry == other.HasModifiedGeometry) &&
                    (Scale == other.Scale) &&
                    (BasisX_X == other.BasisX_X) &&
                    (BasisX_Y == other.BasisX_Y) &&
                    (BasisX_Z == other.BasisX_Z) &&
                    (BasisY_X == other.BasisY_X) &&
                    (BasisY_Y == other.BasisY_Y) &&
                    (BasisY_Z == other.BasisY_Z) &&
                    (BasisZ_X == other.BasisZ_X) &&
                    (BasisZ_Y == other.BasisZ_Y) &&
                    (BasisZ_Z == other.BasisZ_Z) &&
                    (Translation_X == other.Translation_X) &&
                    (Translation_Y == other.Translation_Y) &&
                    (Translation_Z == other.Translation_Z) &&
                    (HandOrientation_X == other.HandOrientation_X) &&
                    (HandOrientation_Y == other.HandOrientation_Y) &&
                    (HandOrientation_Z == other.HandOrientation_Z) &&
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
        public Vim.Format.ObjectModel.Camera Camera => _Camera.Value;
        public Vim.Format.ObjectModel.FamilyType FamilyType => _FamilyType.Value;
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public View()
        {
            _Camera = new Relation<Vim.Format.ObjectModel.Camera>();
            _FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>();
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is View other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Title == other.Title) &&
                    (ViewType == other.ViewType) &&
                    (Up_X == other.Up_X) &&
                    (Up_Y == other.Up_Y) &&
                    (Up_Z == other.Up_Z) &&
                    (Right_X == other.Right_X) &&
                    (Right_Y == other.Right_Y) &&
                    (Right_Z == other.Right_Z) &&
                    (Origin_X == other.Origin_X) &&
                    (Origin_Y == other.Origin_Y) &&
                    (Origin_Z == other.Origin_Z) &&
                    (ViewDirection_X == other.ViewDirection_X) &&
                    (ViewDirection_Y == other.ViewDirection_Y) &&
                    (ViewDirection_Z == other.ViewDirection_Z) &&
                    (ViewPosition_X == other.ViewPosition_X) &&
                    (ViewPosition_Y == other.ViewPosition_Y) &&
                    (ViewPosition_Z == other.ViewPosition_Z) &&
                    (Scale == other.Scale) &&
                    (Outline_Min_X == other.Outline_Min_X) &&
                    (Outline_Min_Y == other.Outline_Min_Y) &&
                    (Outline_Max_X == other.Outline_Max_X) &&
                    (Outline_Max_Y == other.Outline_Max_Y) &&
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
        public Vim.Format.ObjectModel.View View => _View.Value;
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public ElementInView()
        {
            _View = new Relation<Vim.Format.ObjectModel.View>();
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
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
        public Vim.Format.ObjectModel.Shape Shape => _Shape.Value;
        public Vim.Format.ObjectModel.View View => _View.Value;
        public ShapeInView()
        {
            _Shape = new Relation<Vim.Format.ObjectModel.Shape>();
            _View = new Relation<Vim.Format.ObjectModel.View>();
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
        public Vim.Format.ObjectModel.Asset Asset => _Asset.Value;
        public Vim.Format.ObjectModel.View View => _View.Value;
        public AssetInView()
        {
            _Asset = new Relation<Vim.Format.ObjectModel.Asset>();
            _View = new Relation<Vim.Format.ObjectModel.View>();
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
    public partial class AssetInViewSheet
    {
        public Vim.Format.ObjectModel.Asset Asset => _Asset.Value;
        public Vim.Format.ObjectModel.ViewSheet ViewSheet => _ViewSheet.Value;
        public AssetInViewSheet()
        {
            _Asset = new Relation<Vim.Format.ObjectModel.Asset>();
            _ViewSheet = new Relation<Vim.Format.ObjectModel.ViewSheet>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is AssetInViewSheet other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (_Asset?.Index == other._Asset?.Index) &&
                    (_ViewSheet?.Index == other._ViewSheet?.Index);
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
        public Vim.Format.ObjectModel.Level Level => _Level.Value;
        public Vim.Format.ObjectModel.View View => _View.Value;
        public LevelInView()
        {
            _Level = new Relation<Vim.Format.ObjectModel.Level>();
            _View = new Relation<Vim.Format.ObjectModel.View>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is LevelInView other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Extents_Min_X == other.Extents_Min_X) &&
                    (Extents_Min_Y == other.Extents_Min_Y) &&
                    (Extents_Min_Z == other.Extents_Min_Z) &&
                    (Extents_Max_X == other.Extents_Max_X) &&
                    (Extents_Max_Y == other.Extents_Max_Y) &&
                    (Extents_Max_Z == other.Extents_Max_Z) &&
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
        public Vim.Format.ObjectModel.Asset ColorTextureFile => _ColorTextureFile.Value;
        public Vim.Format.ObjectModel.Asset NormalTextureFile => _NormalTextureFile.Value;
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public Material()
        {
            _ColorTextureFile = new Relation<Vim.Format.ObjectModel.Asset>();
            _NormalTextureFile = new Relation<Vim.Format.ObjectModel.Asset>();
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Material other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Name == other.Name) &&
                    (MaterialCategory == other.MaterialCategory) &&
                    (Color_X == other.Color_X) &&
                    (Color_Y == other.Color_Y) &&
                    (Color_Z == other.Color_Z) &&
                    (ColorUvScaling_X == other.ColorUvScaling_X) &&
                    (ColorUvScaling_Y == other.ColorUvScaling_Y) &&
                    (ColorUvOffset_X == other.ColorUvOffset_X) &&
                    (ColorUvOffset_Y == other.ColorUvOffset_Y) &&
                    (NormalUvScaling_X == other.NormalUvScaling_X) &&
                    (NormalUvScaling_Y == other.NormalUvScaling_Y) &&
                    (NormalUvOffset_X == other.NormalUvOffset_X) &&
                    (NormalUvOffset_Y == other.NormalUvOffset_Y) &&
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
        public Vim.Format.ObjectModel.Material Material => _Material.Value;
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public MaterialInElement()
        {
            _Material = new Relation<Vim.Format.ObjectModel.Material>();
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
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
        public Vim.Format.ObjectModel.Material Material => _Material.Value;
        public Vim.Format.ObjectModel.CompoundStructure CompoundStructure => _CompoundStructure.Value;
        public CompoundStructureLayer()
        {
            _Material = new Relation<Vim.Format.ObjectModel.Material>();
            _CompoundStructure = new Relation<Vim.Format.ObjectModel.CompoundStructure>();
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
        public Vim.Format.ObjectModel.CompoundStructureLayer StructuralLayer => _StructuralLayer.Value;
        public CompoundStructure()
        {
            _StructuralLayer = new Relation<Vim.Format.ObjectModel.CompoundStructureLayer>();
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
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public Node()
        {
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
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
                    (Box_Min_X == other.Box_Min_X) &&
                    (Box_Min_Y == other.Box_Min_Y) &&
                    (Box_Min_Z == other.Box_Min_Z) &&
                    (Box_Max_X == other.Box_Max_X) &&
                    (Box_Max_Y == other.Box_Max_Y) &&
                    (Box_Max_Z == other.Box_Max_Z) &&
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
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public Shape()
        {
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
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
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public ShapeCollection()
        {
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
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
        public Vim.Format.ObjectModel.Shape Shape => _Shape.Value;
        public Vim.Format.ObjectModel.ShapeCollection ShapeCollection => _ShapeCollection.Value;
        public ShapeInShapeCollection()
        {
            _Shape = new Relation<Vim.Format.ObjectModel.Shape>();
            _ShapeCollection = new Relation<Vim.Format.ObjectModel.ShapeCollection>();
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
        public Vim.Format.ObjectModel.FamilyType FamilyType => _FamilyType.Value;
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public System()
        {
            _FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>();
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
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
        public Vim.Format.ObjectModel.System System => _System.Value;
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public ElementInSystem()
        {
            _System = new Relation<Vim.Format.ObjectModel.System>();
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
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
        public Vim.Format.ObjectModel.BimDocument BimDocument => _BimDocument.Value;
        public Warning()
        {
            _BimDocument = new Relation<Vim.Format.ObjectModel.BimDocument>();
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
        public Vim.Format.ObjectModel.Warning Warning => _Warning.Value;
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public ElementInWarning()
        {
            _Warning = new Relation<Vim.Format.ObjectModel.Warning>();
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
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
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public BasePoint()
        {
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is BasePoint other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (IsSurveyPoint == other.IsSurveyPoint) &&
                    (Position_X == other.Position_X) &&
                    (Position_Y == other.Position_Y) &&
                    (Position_Z == other.Position_Z) &&
                    (SharedPosition_X == other.SharedPosition_X) &&
                    (SharedPosition_Y == other.SharedPosition_Y) &&
                    (SharedPosition_Z == other.SharedPosition_Z) &&
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
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public PhaseFilter()
        {
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
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
        public Vim.Format.ObjectModel.FamilyType FamilyType => _FamilyType.Value;
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public Grid()
        {
            _FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>();
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Grid other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (StartPoint_X == other.StartPoint_X) &&
                    (StartPoint_Y == other.StartPoint_Y) &&
                    (StartPoint_Z == other.StartPoint_Z) &&
                    (EndPoint_X == other.EndPoint_X) &&
                    (EndPoint_Y == other.EndPoint_Y) &&
                    (EndPoint_Z == other.EndPoint_Z) &&
                    (IsCurved == other.IsCurved) &&
                    (Extents_Min_X == other.Extents_Min_X) &&
                    (Extents_Min_Y == other.Extents_Min_Y) &&
                    (Extents_Min_Z == other.Extents_Min_Z) &&
                    (Extents_Max_X == other.Extents_Max_X) &&
                    (Extents_Max_Y == other.Extents_Max_Y) &&
                    (Extents_Max_Z == other.Extents_Max_Z) &&
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
        public Vim.Format.ObjectModel.AreaScheme AreaScheme => _AreaScheme.Value;
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public Area()
        {
            _AreaScheme = new Relation<Vim.Format.ObjectModel.AreaScheme>();
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
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
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public AreaScheme()
        {
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
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
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public Schedule()
        {
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
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
        public Vim.Format.ObjectModel.Schedule Schedule => _Schedule.Value;
        public ScheduleColumn()
        {
            _Schedule = new Relation<Vim.Format.ObjectModel.Schedule>();
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
        public Vim.Format.ObjectModel.ScheduleColumn ScheduleColumn => _ScheduleColumn.Value;
        public ScheduleCell()
        {
            _ScheduleColumn = new Relation<Vim.Format.ObjectModel.ScheduleColumn>();
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
    
    // AUTO-GENERATED
    public partial class ViewSheetSet
    {
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public ViewSheetSet()
        {
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is ViewSheetSet other))
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
    public partial class ViewSheet
    {
        public Vim.Format.ObjectModel.Element Element => _Element.Value;
        public ViewSheet()
        {
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is ViewSheet other))
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
    public partial class ViewSheetInViewSheetSet
    {
        public Vim.Format.ObjectModel.ViewSheet ViewSheet => _ViewSheet.Value;
        public Vim.Format.ObjectModel.ViewSheetSet ViewSheetSet => _ViewSheetSet.Value;
        public ViewSheetInViewSheetSet()
        {
            _ViewSheet = new Relation<Vim.Format.ObjectModel.ViewSheet>();
            _ViewSheetSet = new Relation<Vim.Format.ObjectModel.ViewSheetSet>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is ViewSheetInViewSheetSet other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (_ViewSheet?.Index == other._ViewSheet?.Index) &&
                    (_ViewSheetSet?.Index == other._ViewSheetSet?.Index);
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
    public partial class ViewInViewSheetSet
    {
        public Vim.Format.ObjectModel.View View => _View.Value;
        public Vim.Format.ObjectModel.ViewSheetSet ViewSheetSet => _ViewSheetSet.Value;
        public ViewInViewSheetSet()
        {
            _View = new Relation<Vim.Format.ObjectModel.View>();
            _ViewSheetSet = new Relation<Vim.Format.ObjectModel.ViewSheetSet>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is ViewInViewSheetSet other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (_View?.Index == other._View?.Index) &&
                    (_ViewSheetSet?.Index == other._ViewSheetSet?.Index);
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
    public partial class ViewInViewSheet
    {
        public Vim.Format.ObjectModel.View View => _View.Value;
        public Vim.Format.ObjectModel.ViewSheet ViewSheet => _ViewSheet.Value;
        public ViewInViewSheet()
        {
            _View = new Relation<Vim.Format.ObjectModel.View>();
            _ViewSheet = new Relation<Vim.Format.ObjectModel.ViewSheet>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is ViewInViewSheet other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (_View?.Index == other._View?.Index) &&
                    (_ViewSheet?.Index == other._ViewSheet?.Index);
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
            r._DisplayUnit = new Relation<Vim.Format.ObjectModel.DisplayUnit>(GetParameterDescriptorDisplayUnitIndex(n), GetDisplayUnit);
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
            r._ParameterDescriptor = new Relation<Vim.Format.ObjectModel.ParameterDescriptor>(GetParameterParameterDescriptorIndex(n), GetParameterDescriptor);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetParameterElementIndex(n), GetElement);
            return r;
        }
        
        
        // Element
        
        public EntityTable ElementEntityTable { get; }
        
        public IArray<Int64> ElementId { get; }
        public Int64 GetElementId(int index, Int64 defaultValue = default) => ElementId?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> ElementType { get; }
        public String GetElementType(int index, String defaultValue = "") => ElementType?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> ElementName { get; }
        public String GetElementName(int index, String defaultValue = "") => ElementName?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> ElementUniqueId { get; }
        public String GetElementUniqueId(int index, String defaultValue = "") => ElementUniqueId?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> ElementLocation_X { get; }
        public Single GetElementLocation_X(int index, Single defaultValue = default) => ElementLocation_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> ElementLocation_Y { get; }
        public Single GetElementLocation_Y(int index, Single defaultValue = default) => ElementLocation_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> ElementLocation_Z { get; }
        public Single GetElementLocation_Z(int index, Single defaultValue = default) => ElementLocation_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
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
            r.Location_X = ElementLocation_X.ElementAtOrDefault(n);
            r.Location_Y = ElementLocation_Y.ElementAtOrDefault(n);
            r.Location_Z = ElementLocation_Z.ElementAtOrDefault(n);
            r.FamilyName = ElementFamilyName.ElementAtOrDefault(n);
            r.IsPinned = ElementIsPinned.ElementAtOrDefault(n);
            r._Level = new Relation<Vim.Format.ObjectModel.Level>(GetElementLevelIndex(n), GetLevel);
            r._PhaseCreated = new Relation<Vim.Format.ObjectModel.Phase>(GetElementPhaseCreatedIndex(n), GetPhase);
            r._PhaseDemolished = new Relation<Vim.Format.ObjectModel.Phase>(GetElementPhaseDemolishedIndex(n), GetPhase);
            r._Category = new Relation<Vim.Format.ObjectModel.Category>(GetElementCategoryIndex(n), GetCategory);
            r._Workset = new Relation<Vim.Format.ObjectModel.Workset>(GetElementWorksetIndex(n), GetWorkset);
            r._DesignOption = new Relation<Vim.Format.ObjectModel.DesignOption>(GetElementDesignOptionIndex(n), GetDesignOption);
            r._OwnerView = new Relation<Vim.Format.ObjectModel.View>(GetElementOwnerViewIndex(n), GetView);
            r._Group = new Relation<Vim.Format.ObjectModel.Group>(GetElementGroupIndex(n), GetGroup);
            r._AssemblyInstance = new Relation<Vim.Format.ObjectModel.AssemblyInstance>(GetElementAssemblyInstanceIndex(n), GetAssemblyInstance);
            r._BimDocument = new Relation<Vim.Format.ObjectModel.BimDocument>(GetElementBimDocumentIndex(n), GetBimDocument);
            r._Room = new Relation<Vim.Format.ObjectModel.Room>(GetElementRoomIndex(n), GetRoom);
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
            r._BimDocument = new Relation<Vim.Format.ObjectModel.BimDocument>(GetWorksetBimDocumentIndex(n), GetBimDocument);
            return r;
        }
        
        
        // AssemblyInstance
        
        public EntityTable AssemblyInstanceEntityTable { get; }
        
        public IArray<String> AssemblyInstanceAssemblyTypeName { get; }
        public String GetAssemblyInstanceAssemblyTypeName(int index, String defaultValue = "") => AssemblyInstanceAssemblyTypeName?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> AssemblyInstancePosition_X { get; }
        public Single GetAssemblyInstancePosition_X(int index, Single defaultValue = default) => AssemblyInstancePosition_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> AssemblyInstancePosition_Y { get; }
        public Single GetAssemblyInstancePosition_Y(int index, Single defaultValue = default) => AssemblyInstancePosition_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> AssemblyInstancePosition_Z { get; }
        public Single GetAssemblyInstancePosition_Z(int index, Single defaultValue = default) => AssemblyInstancePosition_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
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
            r.Position_X = AssemblyInstancePosition_X.ElementAtOrDefault(n);
            r.Position_Y = AssemblyInstancePosition_Y.ElementAtOrDefault(n);
            r.Position_Z = AssemblyInstancePosition_Z.ElementAtOrDefault(n);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetAssemblyInstanceElementIndex(n), GetElement);
            return r;
        }
        
        
        // Group
        
        public EntityTable GroupEntityTable { get; }
        
        public IArray<String> GroupGroupType { get; }
        public String GetGroupGroupType(int index, String defaultValue = "") => GroupGroupType?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> GroupPosition_X { get; }
        public Single GetGroupPosition_X(int index, Single defaultValue = default) => GroupPosition_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> GroupPosition_Y { get; }
        public Single GetGroupPosition_Y(int index, Single defaultValue = default) => GroupPosition_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> GroupPosition_Z { get; }
        public Single GetGroupPosition_Z(int index, Single defaultValue = default) => GroupPosition_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
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
            r.Position_X = GroupPosition_X.ElementAtOrDefault(n);
            r.Position_Y = GroupPosition_Y.ElementAtOrDefault(n);
            r.Position_Z = GroupPosition_Z.ElementAtOrDefault(n);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetGroupElementIndex(n), GetElement);
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
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetDesignOptionElementIndex(n), GetElement);
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
            r._FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>(GetLevelFamilyTypeIndex(n), GetFamilyType);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetLevelElementIndex(n), GetElement);
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
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetPhaseElementIndex(n), GetElement);
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
            r._UpperLimit = new Relation<Vim.Format.ObjectModel.Level>(GetRoomUpperLimitIndex(n), GetLevel);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetRoomElementIndex(n), GetElement);
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
            r._ActiveView = new Relation<Vim.Format.ObjectModel.View>(GetBimDocumentActiveViewIndex(n), GetView);
            r._OwnerFamily = new Relation<Vim.Format.ObjectModel.Family>(GetBimDocumentOwnerFamilyIndex(n), GetFamily);
            r._Parent = new Relation<Vim.Format.ObjectModel.BimDocument>(GetBimDocumentParentIndex(n), GetBimDocument);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetBimDocumentElementIndex(n), GetElement);
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
            r._DisplayUnit = new Relation<Vim.Format.ObjectModel.DisplayUnit>(GetDisplayUnitInBimDocumentDisplayUnitIndex(n), GetDisplayUnit);
            r._BimDocument = new Relation<Vim.Format.ObjectModel.BimDocument>(GetDisplayUnitInBimDocumentBimDocumentIndex(n), GetBimDocument);
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
            r._Phase = new Relation<Vim.Format.ObjectModel.Phase>(GetPhaseOrderInBimDocumentPhaseIndex(n), GetPhase);
            r._BimDocument = new Relation<Vim.Format.ObjectModel.BimDocument>(GetPhaseOrderInBimDocumentBimDocumentIndex(n), GetBimDocument);
            return r;
        }
        
        
        // Category
        
        public EntityTable CategoryEntityTable { get; }
        
        public IArray<String> CategoryName { get; }
        public String GetCategoryName(int index, String defaultValue = "") => CategoryName?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Int64> CategoryId { get; }
        public Int64 GetCategoryId(int index, Int64 defaultValue = default) => CategoryId?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> CategoryCategoryType { get; }
        public String GetCategoryCategoryType(int index, String defaultValue = "") => CategoryCategoryType?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> CategoryLineColor_X { get; }
        public Double GetCategoryLineColor_X(int index, Double defaultValue = default) => CategoryLineColor_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> CategoryLineColor_Y { get; }
        public Double GetCategoryLineColor_Y(int index, Double defaultValue = default) => CategoryLineColor_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> CategoryLineColor_Z { get; }
        public Double GetCategoryLineColor_Z(int index, Double defaultValue = default) => CategoryLineColor_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
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
            r.LineColor_X = CategoryLineColor_X.ElementAtOrDefault(n);
            r.LineColor_Y = CategoryLineColor_Y.ElementAtOrDefault(n);
            r.LineColor_Z = CategoryLineColor_Z.ElementAtOrDefault(n);
            r.BuiltInCategory = CategoryBuiltInCategory.ElementAtOrDefault(n);
            r._Parent = new Relation<Vim.Format.ObjectModel.Category>(GetCategoryParentIndex(n), GetCategory);
            r._Material = new Relation<Vim.Format.ObjectModel.Material>(GetCategoryMaterialIndex(n), GetMaterial);
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
            r._FamilyCategory = new Relation<Vim.Format.ObjectModel.Category>(GetFamilyFamilyCategoryIndex(n), GetCategory);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetFamilyElementIndex(n), GetElement);
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
            r._Family = new Relation<Vim.Format.ObjectModel.Family>(GetFamilyTypeFamilyIndex(n), GetFamily);
            r._CompoundStructure = new Relation<Vim.Format.ObjectModel.CompoundStructure>(GetFamilyTypeCompoundStructureIndex(n), GetCompoundStructure);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetFamilyTypeElementIndex(n), GetElement);
            return r;
        }
        
        
        // FamilyInstance
        
        public EntityTable FamilyInstanceEntityTable { get; }
        
        public IArray<Boolean> FamilyInstanceFacingFlipped { get; }
        public Boolean GetFamilyInstanceFacingFlipped(int index, Boolean defaultValue = default) => FamilyInstanceFacingFlipped?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> FamilyInstanceFacingOrientation_X { get; }
        public Single GetFamilyInstanceFacingOrientation_X(int index, Single defaultValue = default) => FamilyInstanceFacingOrientation_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> FamilyInstanceFacingOrientation_Y { get; }
        public Single GetFamilyInstanceFacingOrientation_Y(int index, Single defaultValue = default) => FamilyInstanceFacingOrientation_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> FamilyInstanceFacingOrientation_Z { get; }
        public Single GetFamilyInstanceFacingOrientation_Z(int index, Single defaultValue = default) => FamilyInstanceFacingOrientation_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Boolean> FamilyInstanceHandFlipped { get; }
        public Boolean GetFamilyInstanceHandFlipped(int index, Boolean defaultValue = default) => FamilyInstanceHandFlipped?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Boolean> FamilyInstanceMirrored { get; }
        public Boolean GetFamilyInstanceMirrored(int index, Boolean defaultValue = default) => FamilyInstanceMirrored?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Boolean> FamilyInstanceHasModifiedGeometry { get; }
        public Boolean GetFamilyInstanceHasModifiedGeometry(int index, Boolean defaultValue = default) => FamilyInstanceHasModifiedGeometry?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> FamilyInstanceScale { get; }
        public Single GetFamilyInstanceScale(int index, Single defaultValue = default) => FamilyInstanceScale?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> FamilyInstanceBasisX_X { get; }
        public Single GetFamilyInstanceBasisX_X(int index, Single defaultValue = default) => FamilyInstanceBasisX_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> FamilyInstanceBasisX_Y { get; }
        public Single GetFamilyInstanceBasisX_Y(int index, Single defaultValue = default) => FamilyInstanceBasisX_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> FamilyInstanceBasisX_Z { get; }
        public Single GetFamilyInstanceBasisX_Z(int index, Single defaultValue = default) => FamilyInstanceBasisX_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> FamilyInstanceBasisY_X { get; }
        public Single GetFamilyInstanceBasisY_X(int index, Single defaultValue = default) => FamilyInstanceBasisY_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> FamilyInstanceBasisY_Y { get; }
        public Single GetFamilyInstanceBasisY_Y(int index, Single defaultValue = default) => FamilyInstanceBasisY_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> FamilyInstanceBasisY_Z { get; }
        public Single GetFamilyInstanceBasisY_Z(int index, Single defaultValue = default) => FamilyInstanceBasisY_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> FamilyInstanceBasisZ_X { get; }
        public Single GetFamilyInstanceBasisZ_X(int index, Single defaultValue = default) => FamilyInstanceBasisZ_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> FamilyInstanceBasisZ_Y { get; }
        public Single GetFamilyInstanceBasisZ_Y(int index, Single defaultValue = default) => FamilyInstanceBasisZ_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> FamilyInstanceBasisZ_Z { get; }
        public Single GetFamilyInstanceBasisZ_Z(int index, Single defaultValue = default) => FamilyInstanceBasisZ_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> FamilyInstanceTranslation_X { get; }
        public Single GetFamilyInstanceTranslation_X(int index, Single defaultValue = default) => FamilyInstanceTranslation_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> FamilyInstanceTranslation_Y { get; }
        public Single GetFamilyInstanceTranslation_Y(int index, Single defaultValue = default) => FamilyInstanceTranslation_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> FamilyInstanceTranslation_Z { get; }
        public Single GetFamilyInstanceTranslation_Z(int index, Single defaultValue = default) => FamilyInstanceTranslation_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> FamilyInstanceHandOrientation_X { get; }
        public Single GetFamilyInstanceHandOrientation_X(int index, Single defaultValue = default) => FamilyInstanceHandOrientation_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> FamilyInstanceHandOrientation_Y { get; }
        public Single GetFamilyInstanceHandOrientation_Y(int index, Single defaultValue = default) => FamilyInstanceHandOrientation_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> FamilyInstanceHandOrientation_Z { get; }
        public Single GetFamilyInstanceHandOrientation_Z(int index, Single defaultValue = default) => FamilyInstanceHandOrientation_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
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
            r.FacingOrientation_X = FamilyInstanceFacingOrientation_X.ElementAtOrDefault(n);
            r.FacingOrientation_Y = FamilyInstanceFacingOrientation_Y.ElementAtOrDefault(n);
            r.FacingOrientation_Z = FamilyInstanceFacingOrientation_Z.ElementAtOrDefault(n);
            r.HandFlipped = FamilyInstanceHandFlipped.ElementAtOrDefault(n);
            r.Mirrored = FamilyInstanceMirrored.ElementAtOrDefault(n);
            r.HasModifiedGeometry = FamilyInstanceHasModifiedGeometry.ElementAtOrDefault(n);
            r.Scale = FamilyInstanceScale.ElementAtOrDefault(n);
            r.BasisX_X = FamilyInstanceBasisX_X.ElementAtOrDefault(n);
            r.BasisX_Y = FamilyInstanceBasisX_Y.ElementAtOrDefault(n);
            r.BasisX_Z = FamilyInstanceBasisX_Z.ElementAtOrDefault(n);
            r.BasisY_X = FamilyInstanceBasisY_X.ElementAtOrDefault(n);
            r.BasisY_Y = FamilyInstanceBasisY_Y.ElementAtOrDefault(n);
            r.BasisY_Z = FamilyInstanceBasisY_Z.ElementAtOrDefault(n);
            r.BasisZ_X = FamilyInstanceBasisZ_X.ElementAtOrDefault(n);
            r.BasisZ_Y = FamilyInstanceBasisZ_Y.ElementAtOrDefault(n);
            r.BasisZ_Z = FamilyInstanceBasisZ_Z.ElementAtOrDefault(n);
            r.Translation_X = FamilyInstanceTranslation_X.ElementAtOrDefault(n);
            r.Translation_Y = FamilyInstanceTranslation_Y.ElementAtOrDefault(n);
            r.Translation_Z = FamilyInstanceTranslation_Z.ElementAtOrDefault(n);
            r.HandOrientation_X = FamilyInstanceHandOrientation_X.ElementAtOrDefault(n);
            r.HandOrientation_Y = FamilyInstanceHandOrientation_Y.ElementAtOrDefault(n);
            r.HandOrientation_Z = FamilyInstanceHandOrientation_Z.ElementAtOrDefault(n);
            r._FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>(GetFamilyInstanceFamilyTypeIndex(n), GetFamilyType);
            r._Host = new Relation<Vim.Format.ObjectModel.Element>(GetFamilyInstanceHostIndex(n), GetElement);
            r._FromRoom = new Relation<Vim.Format.ObjectModel.Room>(GetFamilyInstanceFromRoomIndex(n), GetRoom);
            r._ToRoom = new Relation<Vim.Format.ObjectModel.Room>(GetFamilyInstanceToRoomIndex(n), GetRoom);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetFamilyInstanceElementIndex(n), GetElement);
            return r;
        }
        
        
        // View
        
        public EntityTable ViewEntityTable { get; }
        
        public IArray<String> ViewTitle { get; }
        public String GetViewTitle(int index, String defaultValue = "") => ViewTitle?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> ViewViewType { get; }
        public String GetViewViewType(int index, String defaultValue = "") => ViewViewType?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> ViewUp_X { get; }
        public Double GetViewUp_X(int index, Double defaultValue = default) => ViewUp_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> ViewUp_Y { get; }
        public Double GetViewUp_Y(int index, Double defaultValue = default) => ViewUp_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> ViewUp_Z { get; }
        public Double GetViewUp_Z(int index, Double defaultValue = default) => ViewUp_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> ViewRight_X { get; }
        public Double GetViewRight_X(int index, Double defaultValue = default) => ViewRight_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> ViewRight_Y { get; }
        public Double GetViewRight_Y(int index, Double defaultValue = default) => ViewRight_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> ViewRight_Z { get; }
        public Double GetViewRight_Z(int index, Double defaultValue = default) => ViewRight_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> ViewOrigin_X { get; }
        public Double GetViewOrigin_X(int index, Double defaultValue = default) => ViewOrigin_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> ViewOrigin_Y { get; }
        public Double GetViewOrigin_Y(int index, Double defaultValue = default) => ViewOrigin_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> ViewOrigin_Z { get; }
        public Double GetViewOrigin_Z(int index, Double defaultValue = default) => ViewOrigin_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> ViewViewDirection_X { get; }
        public Double GetViewViewDirection_X(int index, Double defaultValue = default) => ViewViewDirection_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> ViewViewDirection_Y { get; }
        public Double GetViewViewDirection_Y(int index, Double defaultValue = default) => ViewViewDirection_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> ViewViewDirection_Z { get; }
        public Double GetViewViewDirection_Z(int index, Double defaultValue = default) => ViewViewDirection_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> ViewViewPosition_X { get; }
        public Double GetViewViewPosition_X(int index, Double defaultValue = default) => ViewViewPosition_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> ViewViewPosition_Y { get; }
        public Double GetViewViewPosition_Y(int index, Double defaultValue = default) => ViewViewPosition_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> ViewViewPosition_Z { get; }
        public Double GetViewViewPosition_Z(int index, Double defaultValue = default) => ViewViewPosition_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> ViewScale { get; }
        public Double GetViewScale(int index, Double defaultValue = default) => ViewScale?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> ViewOutline_Min_X { get; }
        public Double GetViewOutline_Min_X(int index, Double defaultValue = default) => ViewOutline_Min_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> ViewOutline_Min_Y { get; }
        public Double GetViewOutline_Min_Y(int index, Double defaultValue = default) => ViewOutline_Min_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> ViewOutline_Max_X { get; }
        public Double GetViewOutline_Max_X(int index, Double defaultValue = default) => ViewOutline_Max_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> ViewOutline_Max_Y { get; }
        public Double GetViewOutline_Max_Y(int index, Double defaultValue = default) => ViewOutline_Max_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
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
            r.Up_X = ViewUp_X.ElementAtOrDefault(n);
            r.Up_Y = ViewUp_Y.ElementAtOrDefault(n);
            r.Up_Z = ViewUp_Z.ElementAtOrDefault(n);
            r.Right_X = ViewRight_X.ElementAtOrDefault(n);
            r.Right_Y = ViewRight_Y.ElementAtOrDefault(n);
            r.Right_Z = ViewRight_Z.ElementAtOrDefault(n);
            r.Origin_X = ViewOrigin_X.ElementAtOrDefault(n);
            r.Origin_Y = ViewOrigin_Y.ElementAtOrDefault(n);
            r.Origin_Z = ViewOrigin_Z.ElementAtOrDefault(n);
            r.ViewDirection_X = ViewViewDirection_X.ElementAtOrDefault(n);
            r.ViewDirection_Y = ViewViewDirection_Y.ElementAtOrDefault(n);
            r.ViewDirection_Z = ViewViewDirection_Z.ElementAtOrDefault(n);
            r.ViewPosition_X = ViewViewPosition_X.ElementAtOrDefault(n);
            r.ViewPosition_Y = ViewViewPosition_Y.ElementAtOrDefault(n);
            r.ViewPosition_Z = ViewViewPosition_Z.ElementAtOrDefault(n);
            r.Scale = ViewScale.ElementAtOrDefault(n);
            r.Outline_Min_X = ViewOutline_Min_X.ElementAtOrDefault(n);
            r.Outline_Min_Y = ViewOutline_Min_Y.ElementAtOrDefault(n);
            r.Outline_Max_X = ViewOutline_Max_X.ElementAtOrDefault(n);
            r.Outline_Max_Y = ViewOutline_Max_Y.ElementAtOrDefault(n);
            r.DetailLevel = ViewDetailLevel.ElementAtOrDefault(n);
            r._Camera = new Relation<Vim.Format.ObjectModel.Camera>(GetViewCameraIndex(n), GetCamera);
            r._FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>(GetViewFamilyTypeIndex(n), GetFamilyType);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetViewElementIndex(n), GetElement);
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
            r._View = new Relation<Vim.Format.ObjectModel.View>(GetElementInViewViewIndex(n), GetView);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementInViewElementIndex(n), GetElement);
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
            r._Shape = new Relation<Vim.Format.ObjectModel.Shape>(GetShapeInViewShapeIndex(n), GetShape);
            r._View = new Relation<Vim.Format.ObjectModel.View>(GetShapeInViewViewIndex(n), GetView);
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
            r._Asset = new Relation<Vim.Format.ObjectModel.Asset>(GetAssetInViewAssetIndex(n), GetAsset);
            r._View = new Relation<Vim.Format.ObjectModel.View>(GetAssetInViewViewIndex(n), GetView);
            return r;
        }
        
        
        // AssetInViewSheet
        
        public EntityTable AssetInViewSheetEntityTable { get; }
        
        public IArray<int> AssetInViewSheetAssetIndex { get; }
        public int GetAssetInViewSheetAssetIndex(int index) => AssetInViewSheetAssetIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> AssetInViewSheetViewSheetIndex { get; }
        public int GetAssetInViewSheetViewSheetIndex(int index) => AssetInViewSheetViewSheetIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumAssetInViewSheet => AssetInViewSheetEntityTable?.NumRows ?? 0;
        public IArray<AssetInViewSheet> AssetInViewSheetList { get; }
        public AssetInViewSheet GetAssetInViewSheet(int n)
        {
            if (n < 0) return null;
            var r = new AssetInViewSheet();
            r.Document = Document;
            r.Index = n;
            r._Asset = new Relation<Vim.Format.ObjectModel.Asset>(GetAssetInViewSheetAssetIndex(n), GetAsset);
            r._ViewSheet = new Relation<Vim.Format.ObjectModel.ViewSheet>(GetAssetInViewSheetViewSheetIndex(n), GetViewSheet);
            return r;
        }
        
        
        // LevelInView
        
        public EntityTable LevelInViewEntityTable { get; }
        
        public IArray<Double> LevelInViewExtents_Min_X { get; }
        public Double GetLevelInViewExtents_Min_X(int index, Double defaultValue = default) => LevelInViewExtents_Min_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> LevelInViewExtents_Min_Y { get; }
        public Double GetLevelInViewExtents_Min_Y(int index, Double defaultValue = default) => LevelInViewExtents_Min_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> LevelInViewExtents_Min_Z { get; }
        public Double GetLevelInViewExtents_Min_Z(int index, Double defaultValue = default) => LevelInViewExtents_Min_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> LevelInViewExtents_Max_X { get; }
        public Double GetLevelInViewExtents_Max_X(int index, Double defaultValue = default) => LevelInViewExtents_Max_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> LevelInViewExtents_Max_Y { get; }
        public Double GetLevelInViewExtents_Max_Y(int index, Double defaultValue = default) => LevelInViewExtents_Max_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> LevelInViewExtents_Max_Z { get; }
        public Double GetLevelInViewExtents_Max_Z(int index, Double defaultValue = default) => LevelInViewExtents_Max_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
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
            r.Extents_Min_X = LevelInViewExtents_Min_X.ElementAtOrDefault(n);
            r.Extents_Min_Y = LevelInViewExtents_Min_Y.ElementAtOrDefault(n);
            r.Extents_Min_Z = LevelInViewExtents_Min_Z.ElementAtOrDefault(n);
            r.Extents_Max_X = LevelInViewExtents_Max_X.ElementAtOrDefault(n);
            r.Extents_Max_Y = LevelInViewExtents_Max_Y.ElementAtOrDefault(n);
            r.Extents_Max_Z = LevelInViewExtents_Max_Z.ElementAtOrDefault(n);
            r._Level = new Relation<Vim.Format.ObjectModel.Level>(GetLevelInViewLevelIndex(n), GetLevel);
            r._View = new Relation<Vim.Format.ObjectModel.View>(GetLevelInViewViewIndex(n), GetView);
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
        public IArray<Double> MaterialColor_X { get; }
        public Double GetMaterialColor_X(int index, Double defaultValue = default) => MaterialColor_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> MaterialColor_Y { get; }
        public Double GetMaterialColor_Y(int index, Double defaultValue = default) => MaterialColor_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> MaterialColor_Z { get; }
        public Double GetMaterialColor_Z(int index, Double defaultValue = default) => MaterialColor_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> MaterialColorUvScaling_X { get; }
        public Double GetMaterialColorUvScaling_X(int index, Double defaultValue = default) => MaterialColorUvScaling_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> MaterialColorUvScaling_Y { get; }
        public Double GetMaterialColorUvScaling_Y(int index, Double defaultValue = default) => MaterialColorUvScaling_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> MaterialColorUvOffset_X { get; }
        public Double GetMaterialColorUvOffset_X(int index, Double defaultValue = default) => MaterialColorUvOffset_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> MaterialColorUvOffset_Y { get; }
        public Double GetMaterialColorUvOffset_Y(int index, Double defaultValue = default) => MaterialColorUvOffset_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> MaterialNormalUvScaling_X { get; }
        public Double GetMaterialNormalUvScaling_X(int index, Double defaultValue = default) => MaterialNormalUvScaling_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> MaterialNormalUvScaling_Y { get; }
        public Double GetMaterialNormalUvScaling_Y(int index, Double defaultValue = default) => MaterialNormalUvScaling_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> MaterialNormalUvOffset_X { get; }
        public Double GetMaterialNormalUvOffset_X(int index, Double defaultValue = default) => MaterialNormalUvOffset_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> MaterialNormalUvOffset_Y { get; }
        public Double GetMaterialNormalUvOffset_Y(int index, Double defaultValue = default) => MaterialNormalUvOffset_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
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
            r.Color_X = MaterialColor_X.ElementAtOrDefault(n);
            r.Color_Y = MaterialColor_Y.ElementAtOrDefault(n);
            r.Color_Z = MaterialColor_Z.ElementAtOrDefault(n);
            r.ColorUvScaling_X = MaterialColorUvScaling_X.ElementAtOrDefault(n);
            r.ColorUvScaling_Y = MaterialColorUvScaling_Y.ElementAtOrDefault(n);
            r.ColorUvOffset_X = MaterialColorUvOffset_X.ElementAtOrDefault(n);
            r.ColorUvOffset_Y = MaterialColorUvOffset_Y.ElementAtOrDefault(n);
            r.NormalUvScaling_X = MaterialNormalUvScaling_X.ElementAtOrDefault(n);
            r.NormalUvScaling_Y = MaterialNormalUvScaling_Y.ElementAtOrDefault(n);
            r.NormalUvOffset_X = MaterialNormalUvOffset_X.ElementAtOrDefault(n);
            r.NormalUvOffset_Y = MaterialNormalUvOffset_Y.ElementAtOrDefault(n);
            r.NormalAmount = MaterialNormalAmount.ElementAtOrDefault(n);
            r.Glossiness = MaterialGlossiness.ElementAtOrDefault(n);
            r.Smoothness = MaterialSmoothness.ElementAtOrDefault(n);
            r.Transparency = MaterialTransparency.ElementAtOrDefault(n);
            r._ColorTextureFile = new Relation<Vim.Format.ObjectModel.Asset>(GetMaterialColorTextureFileIndex(n), GetAsset);
            r._NormalTextureFile = new Relation<Vim.Format.ObjectModel.Asset>(GetMaterialNormalTextureFileIndex(n), GetAsset);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetMaterialElementIndex(n), GetElement);
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
            r._Material = new Relation<Vim.Format.ObjectModel.Material>(GetMaterialInElementMaterialIndex(n), GetMaterial);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetMaterialInElementElementIndex(n), GetElement);
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
            r._Material = new Relation<Vim.Format.ObjectModel.Material>(GetCompoundStructureLayerMaterialIndex(n), GetMaterial);
            r._CompoundStructure = new Relation<Vim.Format.ObjectModel.CompoundStructure>(GetCompoundStructureLayerCompoundStructureIndex(n), GetCompoundStructure);
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
            r._StructuralLayer = new Relation<Vim.Format.ObjectModel.CompoundStructureLayer>(GetCompoundStructureStructuralLayerIndex(n), GetCompoundStructureLayer);
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
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetNodeElementIndex(n), GetElement);
            return r;
        }
        
        
        // Geometry
        
        public EntityTable GeometryEntityTable { get; }
        
        public IArray<Single> GeometryBox_Min_X { get; }
        public Single GetGeometryBox_Min_X(int index, Single defaultValue = default) => GeometryBox_Min_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> GeometryBox_Min_Y { get; }
        public Single GetGeometryBox_Min_Y(int index, Single defaultValue = default) => GeometryBox_Min_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> GeometryBox_Min_Z { get; }
        public Single GetGeometryBox_Min_Z(int index, Single defaultValue = default) => GeometryBox_Min_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> GeometryBox_Max_X { get; }
        public Single GetGeometryBox_Max_X(int index, Single defaultValue = default) => GeometryBox_Max_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> GeometryBox_Max_Y { get; }
        public Single GetGeometryBox_Max_Y(int index, Single defaultValue = default) => GeometryBox_Max_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Single> GeometryBox_Max_Z { get; }
        public Single GetGeometryBox_Max_Z(int index, Single defaultValue = default) => GeometryBox_Max_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
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
            r.Box_Min_X = GeometryBox_Min_X.ElementAtOrDefault(n);
            r.Box_Min_Y = GeometryBox_Min_Y.ElementAtOrDefault(n);
            r.Box_Min_Z = GeometryBox_Min_Z.ElementAtOrDefault(n);
            r.Box_Max_X = GeometryBox_Max_X.ElementAtOrDefault(n);
            r.Box_Max_Y = GeometryBox_Max_Y.ElementAtOrDefault(n);
            r.Box_Max_Z = GeometryBox_Max_Z.ElementAtOrDefault(n);
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
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetShapeElementIndex(n), GetElement);
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
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetShapeCollectionElementIndex(n), GetElement);
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
            r._Shape = new Relation<Vim.Format.ObjectModel.Shape>(GetShapeInShapeCollectionShapeIndex(n), GetShape);
            r._ShapeCollection = new Relation<Vim.Format.ObjectModel.ShapeCollection>(GetShapeInShapeCollectionShapeCollectionIndex(n), GetShapeCollection);
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
            r._FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>(GetSystemFamilyTypeIndex(n), GetFamilyType);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetSystemElementIndex(n), GetElement);
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
            r._System = new Relation<Vim.Format.ObjectModel.System>(GetElementInSystemSystemIndex(n), GetSystem);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementInSystemElementIndex(n), GetElement);
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
            r._BimDocument = new Relation<Vim.Format.ObjectModel.BimDocument>(GetWarningBimDocumentIndex(n), GetBimDocument);
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
            r._Warning = new Relation<Vim.Format.ObjectModel.Warning>(GetElementInWarningWarningIndex(n), GetWarning);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementInWarningElementIndex(n), GetElement);
            return r;
        }
        
        
        // BasePoint
        
        public EntityTable BasePointEntityTable { get; }
        
        public IArray<Boolean> BasePointIsSurveyPoint { get; }
        public Boolean GetBasePointIsSurveyPoint(int index, Boolean defaultValue = default) => BasePointIsSurveyPoint?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> BasePointPosition_X { get; }
        public Double GetBasePointPosition_X(int index, Double defaultValue = default) => BasePointPosition_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> BasePointPosition_Y { get; }
        public Double GetBasePointPosition_Y(int index, Double defaultValue = default) => BasePointPosition_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> BasePointPosition_Z { get; }
        public Double GetBasePointPosition_Z(int index, Double defaultValue = default) => BasePointPosition_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> BasePointSharedPosition_X { get; }
        public Double GetBasePointSharedPosition_X(int index, Double defaultValue = default) => BasePointSharedPosition_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> BasePointSharedPosition_Y { get; }
        public Double GetBasePointSharedPosition_Y(int index, Double defaultValue = default) => BasePointSharedPosition_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> BasePointSharedPosition_Z { get; }
        public Double GetBasePointSharedPosition_Z(int index, Double defaultValue = default) => BasePointSharedPosition_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
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
            r.Position_X = BasePointPosition_X.ElementAtOrDefault(n);
            r.Position_Y = BasePointPosition_Y.ElementAtOrDefault(n);
            r.Position_Z = BasePointPosition_Z.ElementAtOrDefault(n);
            r.SharedPosition_X = BasePointSharedPosition_X.ElementAtOrDefault(n);
            r.SharedPosition_Y = BasePointSharedPosition_Y.ElementAtOrDefault(n);
            r.SharedPosition_Z = BasePointSharedPosition_Z.ElementAtOrDefault(n);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetBasePointElementIndex(n), GetElement);
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
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetPhaseFilterElementIndex(n), GetElement);
            return r;
        }
        
        
        // Grid
        
        public EntityTable GridEntityTable { get; }
        
        public IArray<Double> GridStartPoint_X { get; }
        public Double GetGridStartPoint_X(int index, Double defaultValue = default) => GridStartPoint_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> GridStartPoint_Y { get; }
        public Double GetGridStartPoint_Y(int index, Double defaultValue = default) => GridStartPoint_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> GridStartPoint_Z { get; }
        public Double GetGridStartPoint_Z(int index, Double defaultValue = default) => GridStartPoint_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> GridEndPoint_X { get; }
        public Double GetGridEndPoint_X(int index, Double defaultValue = default) => GridEndPoint_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> GridEndPoint_Y { get; }
        public Double GetGridEndPoint_Y(int index, Double defaultValue = default) => GridEndPoint_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> GridEndPoint_Z { get; }
        public Double GetGridEndPoint_Z(int index, Double defaultValue = default) => GridEndPoint_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Boolean> GridIsCurved { get; }
        public Boolean GetGridIsCurved(int index, Boolean defaultValue = default) => GridIsCurved?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> GridExtents_Min_X { get; }
        public Double GetGridExtents_Min_X(int index, Double defaultValue = default) => GridExtents_Min_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> GridExtents_Min_Y { get; }
        public Double GetGridExtents_Min_Y(int index, Double defaultValue = default) => GridExtents_Min_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> GridExtents_Min_Z { get; }
        public Double GetGridExtents_Min_Z(int index, Double defaultValue = default) => GridExtents_Min_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> GridExtents_Max_X { get; }
        public Double GetGridExtents_Max_X(int index, Double defaultValue = default) => GridExtents_Max_X?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> GridExtents_Max_Y { get; }
        public Double GetGridExtents_Max_Y(int index, Double defaultValue = default) => GridExtents_Max_Y?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> GridExtents_Max_Z { get; }
        public Double GetGridExtents_Max_Z(int index, Double defaultValue = default) => GridExtents_Max_Z?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
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
            r.StartPoint_X = GridStartPoint_X.ElementAtOrDefault(n);
            r.StartPoint_Y = GridStartPoint_Y.ElementAtOrDefault(n);
            r.StartPoint_Z = GridStartPoint_Z.ElementAtOrDefault(n);
            r.EndPoint_X = GridEndPoint_X.ElementAtOrDefault(n);
            r.EndPoint_Y = GridEndPoint_Y.ElementAtOrDefault(n);
            r.EndPoint_Z = GridEndPoint_Z.ElementAtOrDefault(n);
            r.IsCurved = GridIsCurved.ElementAtOrDefault(n);
            r.Extents_Min_X = GridExtents_Min_X.ElementAtOrDefault(n);
            r.Extents_Min_Y = GridExtents_Min_Y.ElementAtOrDefault(n);
            r.Extents_Min_Z = GridExtents_Min_Z.ElementAtOrDefault(n);
            r.Extents_Max_X = GridExtents_Max_X.ElementAtOrDefault(n);
            r.Extents_Max_Y = GridExtents_Max_Y.ElementAtOrDefault(n);
            r.Extents_Max_Z = GridExtents_Max_Z.ElementAtOrDefault(n);
            r._FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>(GetGridFamilyTypeIndex(n), GetFamilyType);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetGridElementIndex(n), GetElement);
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
            r._AreaScheme = new Relation<Vim.Format.ObjectModel.AreaScheme>(GetAreaAreaSchemeIndex(n), GetAreaScheme);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetAreaElementIndex(n), GetElement);
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
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetAreaSchemeElementIndex(n), GetElement);
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
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetScheduleElementIndex(n), GetElement);
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
            r._Schedule = new Relation<Vim.Format.ObjectModel.Schedule>(GetScheduleColumnScheduleIndex(n), GetSchedule);
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
            r._ScheduleColumn = new Relation<Vim.Format.ObjectModel.ScheduleColumn>(GetScheduleCellScheduleColumnIndex(n), GetScheduleColumn);
            return r;
        }
        
        
        // ViewSheetSet
        
        public EntityTable ViewSheetSetEntityTable { get; }
        
        public IArray<int> ViewSheetSetElementIndex { get; }
        public int GetViewSheetSetElementIndex(int index) => ViewSheetSetElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumViewSheetSet => ViewSheetSetEntityTable?.NumRows ?? 0;
        public IArray<ViewSheetSet> ViewSheetSetList { get; }
        public ViewSheetSet GetViewSheetSet(int n)
        {
            if (n < 0) return null;
            var r = new ViewSheetSet();
            r.Document = Document;
            r.Index = n;
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetViewSheetSetElementIndex(n), GetElement);
            return r;
        }
        
        
        // ViewSheet
        
        public EntityTable ViewSheetEntityTable { get; }
        
        public IArray<int> ViewSheetElementIndex { get; }
        public int GetViewSheetElementIndex(int index) => ViewSheetElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumViewSheet => ViewSheetEntityTable?.NumRows ?? 0;
        public IArray<ViewSheet> ViewSheetList { get; }
        public ViewSheet GetViewSheet(int n)
        {
            if (n < 0) return null;
            var r = new ViewSheet();
            r.Document = Document;
            r.Index = n;
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetViewSheetElementIndex(n), GetElement);
            return r;
        }
        
        
        // ViewSheetInViewSheetSet
        
        public EntityTable ViewSheetInViewSheetSetEntityTable { get; }
        
        public IArray<int> ViewSheetInViewSheetSetViewSheetIndex { get; }
        public int GetViewSheetInViewSheetSetViewSheetIndex(int index) => ViewSheetInViewSheetSetViewSheetIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> ViewSheetInViewSheetSetViewSheetSetIndex { get; }
        public int GetViewSheetInViewSheetSetViewSheetSetIndex(int index) => ViewSheetInViewSheetSetViewSheetSetIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumViewSheetInViewSheetSet => ViewSheetInViewSheetSetEntityTable?.NumRows ?? 0;
        public IArray<ViewSheetInViewSheetSet> ViewSheetInViewSheetSetList { get; }
        public ViewSheetInViewSheetSet GetViewSheetInViewSheetSet(int n)
        {
            if (n < 0) return null;
            var r = new ViewSheetInViewSheetSet();
            r.Document = Document;
            r.Index = n;
            r._ViewSheet = new Relation<Vim.Format.ObjectModel.ViewSheet>(GetViewSheetInViewSheetSetViewSheetIndex(n), GetViewSheet);
            r._ViewSheetSet = new Relation<Vim.Format.ObjectModel.ViewSheetSet>(GetViewSheetInViewSheetSetViewSheetSetIndex(n), GetViewSheetSet);
            return r;
        }
        
        
        // ViewInViewSheetSet
        
        public EntityTable ViewInViewSheetSetEntityTable { get; }
        
        public IArray<int> ViewInViewSheetSetViewIndex { get; }
        public int GetViewInViewSheetSetViewIndex(int index) => ViewInViewSheetSetViewIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> ViewInViewSheetSetViewSheetSetIndex { get; }
        public int GetViewInViewSheetSetViewSheetSetIndex(int index) => ViewInViewSheetSetViewSheetSetIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumViewInViewSheetSet => ViewInViewSheetSetEntityTable?.NumRows ?? 0;
        public IArray<ViewInViewSheetSet> ViewInViewSheetSetList { get; }
        public ViewInViewSheetSet GetViewInViewSheetSet(int n)
        {
            if (n < 0) return null;
            var r = new ViewInViewSheetSet();
            r.Document = Document;
            r.Index = n;
            r._View = new Relation<Vim.Format.ObjectModel.View>(GetViewInViewSheetSetViewIndex(n), GetView);
            r._ViewSheetSet = new Relation<Vim.Format.ObjectModel.ViewSheetSet>(GetViewInViewSheetSetViewSheetSetIndex(n), GetViewSheetSet);
            return r;
        }
        
        
        // ViewInViewSheet
        
        public EntityTable ViewInViewSheetEntityTable { get; }
        
        public IArray<int> ViewInViewSheetViewIndex { get; }
        public int GetViewInViewSheetViewIndex(int index) => ViewInViewSheetViewIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> ViewInViewSheetViewSheetIndex { get; }
        public int GetViewInViewSheetViewSheetIndex(int index) => ViewInViewSheetViewSheetIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumViewInViewSheet => ViewInViewSheetEntityTable?.NumRows ?? 0;
        public IArray<ViewInViewSheet> ViewInViewSheetList { get; }
        public ViewInViewSheet GetViewInViewSheet(int n)
        {
            if (n < 0) return null;
            var r = new ViewInViewSheet();
            r.Document = Document;
            r.Index = n;
            r._View = new Relation<Vim.Format.ObjectModel.View>(GetViewInViewSheetViewIndex(n), GetView);
            r._ViewSheet = new Relation<Vim.Format.ObjectModel.ViewSheet>(GetViewInViewSheetViewSheetIndex(n), GetViewSheet);
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
            {"Vim.AssetInViewSheet", AssetInViewSheetList.ToEnumerable()},
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
            {"Vim.ViewSheetSet", ViewSheetSetList.ToEnumerable()},
            {"Vim.ViewSheet", ViewSheetList.ToEnumerable()},
            {"Vim.ViewSheetInViewSheetSet", ViewSheetInViewSheetSetList.ToEnumerable()},
            {"Vim.ViewInViewSheetSet", ViewInViewSheetSetList.ToEnumerable()},
            {"Vim.ViewInViewSheet", ViewInViewSheetList.ToEnumerable()},
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
            {"Vim.AssetInViewSheet", typeof(AssetInViewSheet)},
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
            {"Vim.ViewSheetSet", typeof(ViewSheetSet)},
            {"Vim.ViewSheet", typeof(ViewSheet)},
            {"Vim.ViewSheetInViewSheetSet", typeof(ViewSheetInViewSheetSet)},
            {"Vim.ViewInViewSheetSet", typeof(ViewInViewSheetSet)},
            {"Vim.ViewInViewSheet", typeof(ViewInViewSheet)},
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
            AssetInViewSheetEntityTable = Document.GetTable("Vim.AssetInViewSheet");
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
            ViewSheetSetEntityTable = Document.GetTable("Vim.ViewSheetSet");
            ViewSheetEntityTable = Document.GetTable("Vim.ViewSheet");
            ViewSheetInViewSheetSetEntityTable = Document.GetTable("Vim.ViewSheetInViewSheetSet");
            ViewInViewSheetSetEntityTable = Document.GetTable("Vim.ViewInViewSheetSet");
            ViewInViewSheetEntityTable = Document.GetTable("Vim.ViewInViewSheet");
            
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
            ElementId = (ElementEntityTable?.GetDataColumnValues<Int64>("long:Id") ?? ElementEntityTable?.GetDataColumnValues<Int32>("int:Id")?.Select(v => (Int64) v)) ?? Array.Empty<Int64>().ToIArray();
            ElementType = ElementEntityTable?.GetStringColumnValues("string:Type") ?? Array.Empty<String>().ToIArray();
            ElementName = ElementEntityTable?.GetStringColumnValues("string:Name") ?? Array.Empty<String>().ToIArray();
            ElementUniqueId = ElementEntityTable?.GetStringColumnValues("string:UniqueId") ?? Array.Empty<String>().ToIArray();
            ElementLocation_X = ElementEntityTable?.GetDataColumnValues<Single>("float:Location.X") ?? Array.Empty<Single>().ToIArray();
            ElementLocation_Y = ElementEntityTable?.GetDataColumnValues<Single>("float:Location.Y") ?? Array.Empty<Single>().ToIArray();
            ElementLocation_Z = ElementEntityTable?.GetDataColumnValues<Single>("float:Location.Z") ?? Array.Empty<Single>().ToIArray();
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
            AssemblyInstancePosition_X = AssemblyInstanceEntityTable?.GetDataColumnValues<Single>("float:Position.X") ?? Array.Empty<Single>().ToIArray();
            AssemblyInstancePosition_Y = AssemblyInstanceEntityTable?.GetDataColumnValues<Single>("float:Position.Y") ?? Array.Empty<Single>().ToIArray();
            AssemblyInstancePosition_Z = AssemblyInstanceEntityTable?.GetDataColumnValues<Single>("float:Position.Z") ?? Array.Empty<Single>().ToIArray();
            GroupGroupType = GroupEntityTable?.GetStringColumnValues("string:GroupType") ?? Array.Empty<String>().ToIArray();
            GroupPosition_X = GroupEntityTable?.GetDataColumnValues<Single>("float:Position.X") ?? Array.Empty<Single>().ToIArray();
            GroupPosition_Y = GroupEntityTable?.GetDataColumnValues<Single>("float:Position.Y") ?? Array.Empty<Single>().ToIArray();
            GroupPosition_Z = GroupEntityTable?.GetDataColumnValues<Single>("float:Position.Z") ?? Array.Empty<Single>().ToIArray();
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
            CategoryId = (CategoryEntityTable?.GetDataColumnValues<Int64>("long:Id") ?? CategoryEntityTable?.GetDataColumnValues<Int32>("int:Id")?.Select(v => (Int64) v)) ?? Array.Empty<Int64>().ToIArray();
            CategoryCategoryType = CategoryEntityTable?.GetStringColumnValues("string:CategoryType") ?? Array.Empty<String>().ToIArray();
            CategoryLineColor_X = CategoryEntityTable?.GetDataColumnValues<Double>("double:LineColor.X") ?? Array.Empty<Double>().ToIArray();
            CategoryLineColor_Y = CategoryEntityTable?.GetDataColumnValues<Double>("double:LineColor.Y") ?? Array.Empty<Double>().ToIArray();
            CategoryLineColor_Z = CategoryEntityTable?.GetDataColumnValues<Double>("double:LineColor.Z") ?? Array.Empty<Double>().ToIArray();
            CategoryBuiltInCategory = CategoryEntityTable?.GetStringColumnValues("string:BuiltInCategory") ?? Array.Empty<String>().ToIArray();
            FamilyStructuralMaterialType = FamilyEntityTable?.GetStringColumnValues("string:StructuralMaterialType") ?? Array.Empty<String>().ToIArray();
            FamilyStructuralSectionShape = FamilyEntityTable?.GetStringColumnValues("string:StructuralSectionShape") ?? Array.Empty<String>().ToIArray();
            FamilyIsSystemFamily = FamilyEntityTable?.GetDataColumnValues<Boolean>("byte:IsSystemFamily") ?? Array.Empty<Boolean>().ToIArray();
            FamilyIsInPlace = FamilyEntityTable?.GetDataColumnValues<Boolean>("byte:IsInPlace") ?? Array.Empty<Boolean>().ToIArray();
            FamilyTypeIsSystemFamilyType = FamilyTypeEntityTable?.GetDataColumnValues<Boolean>("byte:IsSystemFamilyType") ?? Array.Empty<Boolean>().ToIArray();
            FamilyInstanceFacingFlipped = FamilyInstanceEntityTable?.GetDataColumnValues<Boolean>("byte:FacingFlipped") ?? Array.Empty<Boolean>().ToIArray();
            FamilyInstanceFacingOrientation_X = FamilyInstanceEntityTable?.GetDataColumnValues<Single>("float:FacingOrientation.X") ?? Array.Empty<Single>().ToIArray();
            FamilyInstanceFacingOrientation_Y = FamilyInstanceEntityTable?.GetDataColumnValues<Single>("float:FacingOrientation.Y") ?? Array.Empty<Single>().ToIArray();
            FamilyInstanceFacingOrientation_Z = FamilyInstanceEntityTable?.GetDataColumnValues<Single>("float:FacingOrientation.Z") ?? Array.Empty<Single>().ToIArray();
            FamilyInstanceHandFlipped = FamilyInstanceEntityTable?.GetDataColumnValues<Boolean>("byte:HandFlipped") ?? Array.Empty<Boolean>().ToIArray();
            FamilyInstanceMirrored = FamilyInstanceEntityTable?.GetDataColumnValues<Boolean>("byte:Mirrored") ?? Array.Empty<Boolean>().ToIArray();
            FamilyInstanceHasModifiedGeometry = FamilyInstanceEntityTable?.GetDataColumnValues<Boolean>("byte:HasModifiedGeometry") ?? Array.Empty<Boolean>().ToIArray();
            FamilyInstanceScale = FamilyInstanceEntityTable?.GetDataColumnValues<Single>("float:Scale") ?? Array.Empty<Single>().ToIArray();
            FamilyInstanceBasisX_X = FamilyInstanceEntityTable?.GetDataColumnValues<Single>("float:BasisX.X") ?? Array.Empty<Single>().ToIArray();
            FamilyInstanceBasisX_Y = FamilyInstanceEntityTable?.GetDataColumnValues<Single>("float:BasisX.Y") ?? Array.Empty<Single>().ToIArray();
            FamilyInstanceBasisX_Z = FamilyInstanceEntityTable?.GetDataColumnValues<Single>("float:BasisX.Z") ?? Array.Empty<Single>().ToIArray();
            FamilyInstanceBasisY_X = FamilyInstanceEntityTable?.GetDataColumnValues<Single>("float:BasisY.X") ?? Array.Empty<Single>().ToIArray();
            FamilyInstanceBasisY_Y = FamilyInstanceEntityTable?.GetDataColumnValues<Single>("float:BasisY.Y") ?? Array.Empty<Single>().ToIArray();
            FamilyInstanceBasisY_Z = FamilyInstanceEntityTable?.GetDataColumnValues<Single>("float:BasisY.Z") ?? Array.Empty<Single>().ToIArray();
            FamilyInstanceBasisZ_X = FamilyInstanceEntityTable?.GetDataColumnValues<Single>("float:BasisZ.X") ?? Array.Empty<Single>().ToIArray();
            FamilyInstanceBasisZ_Y = FamilyInstanceEntityTable?.GetDataColumnValues<Single>("float:BasisZ.Y") ?? Array.Empty<Single>().ToIArray();
            FamilyInstanceBasisZ_Z = FamilyInstanceEntityTable?.GetDataColumnValues<Single>("float:BasisZ.Z") ?? Array.Empty<Single>().ToIArray();
            FamilyInstanceTranslation_X = FamilyInstanceEntityTable?.GetDataColumnValues<Single>("float:Translation.X") ?? Array.Empty<Single>().ToIArray();
            FamilyInstanceTranslation_Y = FamilyInstanceEntityTable?.GetDataColumnValues<Single>("float:Translation.Y") ?? Array.Empty<Single>().ToIArray();
            FamilyInstanceTranslation_Z = FamilyInstanceEntityTable?.GetDataColumnValues<Single>("float:Translation.Z") ?? Array.Empty<Single>().ToIArray();
            FamilyInstanceHandOrientation_X = FamilyInstanceEntityTable?.GetDataColumnValues<Single>("float:HandOrientation.X") ?? Array.Empty<Single>().ToIArray();
            FamilyInstanceHandOrientation_Y = FamilyInstanceEntityTable?.GetDataColumnValues<Single>("float:HandOrientation.Y") ?? Array.Empty<Single>().ToIArray();
            FamilyInstanceHandOrientation_Z = FamilyInstanceEntityTable?.GetDataColumnValues<Single>("float:HandOrientation.Z") ?? Array.Empty<Single>().ToIArray();
            ViewTitle = ViewEntityTable?.GetStringColumnValues("string:Title") ?? Array.Empty<String>().ToIArray();
            ViewViewType = ViewEntityTable?.GetStringColumnValues("string:ViewType") ?? Array.Empty<String>().ToIArray();
            ViewUp_X = ViewEntityTable?.GetDataColumnValues<Double>("double:Up.X") ?? Array.Empty<Double>().ToIArray();
            ViewUp_Y = ViewEntityTable?.GetDataColumnValues<Double>("double:Up.Y") ?? Array.Empty<Double>().ToIArray();
            ViewUp_Z = ViewEntityTable?.GetDataColumnValues<Double>("double:Up.Z") ?? Array.Empty<Double>().ToIArray();
            ViewRight_X = ViewEntityTable?.GetDataColumnValues<Double>("double:Right.X") ?? Array.Empty<Double>().ToIArray();
            ViewRight_Y = ViewEntityTable?.GetDataColumnValues<Double>("double:Right.Y") ?? Array.Empty<Double>().ToIArray();
            ViewRight_Z = ViewEntityTable?.GetDataColumnValues<Double>("double:Right.Z") ?? Array.Empty<Double>().ToIArray();
            ViewOrigin_X = ViewEntityTable?.GetDataColumnValues<Double>("double:Origin.X") ?? Array.Empty<Double>().ToIArray();
            ViewOrigin_Y = ViewEntityTable?.GetDataColumnValues<Double>("double:Origin.Y") ?? Array.Empty<Double>().ToIArray();
            ViewOrigin_Z = ViewEntityTable?.GetDataColumnValues<Double>("double:Origin.Z") ?? Array.Empty<Double>().ToIArray();
            ViewViewDirection_X = ViewEntityTable?.GetDataColumnValues<Double>("double:ViewDirection.X") ?? Array.Empty<Double>().ToIArray();
            ViewViewDirection_Y = ViewEntityTable?.GetDataColumnValues<Double>("double:ViewDirection.Y") ?? Array.Empty<Double>().ToIArray();
            ViewViewDirection_Z = ViewEntityTable?.GetDataColumnValues<Double>("double:ViewDirection.Z") ?? Array.Empty<Double>().ToIArray();
            ViewViewPosition_X = ViewEntityTable?.GetDataColumnValues<Double>("double:ViewPosition.X") ?? Array.Empty<Double>().ToIArray();
            ViewViewPosition_Y = ViewEntityTable?.GetDataColumnValues<Double>("double:ViewPosition.Y") ?? Array.Empty<Double>().ToIArray();
            ViewViewPosition_Z = ViewEntityTable?.GetDataColumnValues<Double>("double:ViewPosition.Z") ?? Array.Empty<Double>().ToIArray();
            ViewScale = ViewEntityTable?.GetDataColumnValues<Double>("double:Scale") ?? Array.Empty<Double>().ToIArray();
            ViewOutline_Min_X = ViewEntityTable?.GetDataColumnValues<Double>("double:Outline.Min.X") ?? Array.Empty<Double>().ToIArray();
            ViewOutline_Min_Y = ViewEntityTable?.GetDataColumnValues<Double>("double:Outline.Min.Y") ?? Array.Empty<Double>().ToIArray();
            ViewOutline_Max_X = ViewEntityTable?.GetDataColumnValues<Double>("double:Outline.Max.X") ?? Array.Empty<Double>().ToIArray();
            ViewOutline_Max_Y = ViewEntityTable?.GetDataColumnValues<Double>("double:Outline.Max.Y") ?? Array.Empty<Double>().ToIArray();
            ViewDetailLevel = ViewEntityTable?.GetDataColumnValues<Int32>("int:DetailLevel") ?? Array.Empty<Int32>().ToIArray();
            LevelInViewExtents_Min_X = LevelInViewEntityTable?.GetDataColumnValues<Double>("double:Extents.Min.X") ?? Array.Empty<Double>().ToIArray();
            LevelInViewExtents_Min_Y = LevelInViewEntityTable?.GetDataColumnValues<Double>("double:Extents.Min.Y") ?? Array.Empty<Double>().ToIArray();
            LevelInViewExtents_Min_Z = LevelInViewEntityTable?.GetDataColumnValues<Double>("double:Extents.Min.Z") ?? Array.Empty<Double>().ToIArray();
            LevelInViewExtents_Max_X = LevelInViewEntityTable?.GetDataColumnValues<Double>("double:Extents.Max.X") ?? Array.Empty<Double>().ToIArray();
            LevelInViewExtents_Max_Y = LevelInViewEntityTable?.GetDataColumnValues<Double>("double:Extents.Max.Y") ?? Array.Empty<Double>().ToIArray();
            LevelInViewExtents_Max_Z = LevelInViewEntityTable?.GetDataColumnValues<Double>("double:Extents.Max.Z") ?? Array.Empty<Double>().ToIArray();
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
            MaterialColor_X = MaterialEntityTable?.GetDataColumnValues<Double>("double:Color.X") ?? Array.Empty<Double>().ToIArray();
            MaterialColor_Y = MaterialEntityTable?.GetDataColumnValues<Double>("double:Color.Y") ?? Array.Empty<Double>().ToIArray();
            MaterialColor_Z = MaterialEntityTable?.GetDataColumnValues<Double>("double:Color.Z") ?? Array.Empty<Double>().ToIArray();
            MaterialColorUvScaling_X = MaterialEntityTable?.GetDataColumnValues<Double>("double:ColorUvScaling.X") ?? Array.Empty<Double>().ToIArray();
            MaterialColorUvScaling_Y = MaterialEntityTable?.GetDataColumnValues<Double>("double:ColorUvScaling.Y") ?? Array.Empty<Double>().ToIArray();
            MaterialColorUvOffset_X = MaterialEntityTable?.GetDataColumnValues<Double>("double:ColorUvOffset.X") ?? Array.Empty<Double>().ToIArray();
            MaterialColorUvOffset_Y = MaterialEntityTable?.GetDataColumnValues<Double>("double:ColorUvOffset.Y") ?? Array.Empty<Double>().ToIArray();
            MaterialNormalUvScaling_X = MaterialEntityTable?.GetDataColumnValues<Double>("double:NormalUvScaling.X") ?? Array.Empty<Double>().ToIArray();
            MaterialNormalUvScaling_Y = MaterialEntityTable?.GetDataColumnValues<Double>("double:NormalUvScaling.Y") ?? Array.Empty<Double>().ToIArray();
            MaterialNormalUvOffset_X = MaterialEntityTable?.GetDataColumnValues<Double>("double:NormalUvOffset.X") ?? Array.Empty<Double>().ToIArray();
            MaterialNormalUvOffset_Y = MaterialEntityTable?.GetDataColumnValues<Double>("double:NormalUvOffset.Y") ?? Array.Empty<Double>().ToIArray();
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
            GeometryBox_Min_X = GeometryEntityTable?.GetDataColumnValues<Single>("float:Box.Min.X") ?? Array.Empty<Single>().ToIArray();
            GeometryBox_Min_Y = GeometryEntityTable?.GetDataColumnValues<Single>("float:Box.Min.Y") ?? Array.Empty<Single>().ToIArray();
            GeometryBox_Min_Z = GeometryEntityTable?.GetDataColumnValues<Single>("float:Box.Min.Z") ?? Array.Empty<Single>().ToIArray();
            GeometryBox_Max_X = GeometryEntityTable?.GetDataColumnValues<Single>("float:Box.Max.X") ?? Array.Empty<Single>().ToIArray();
            GeometryBox_Max_Y = GeometryEntityTable?.GetDataColumnValues<Single>("float:Box.Max.Y") ?? Array.Empty<Single>().ToIArray();
            GeometryBox_Max_Z = GeometryEntityTable?.GetDataColumnValues<Single>("float:Box.Max.Z") ?? Array.Empty<Single>().ToIArray();
            GeometryVertexCount = GeometryEntityTable?.GetDataColumnValues<Int32>("int:VertexCount") ?? Array.Empty<Int32>().ToIArray();
            GeometryFaceCount = GeometryEntityTable?.GetDataColumnValues<Int32>("int:FaceCount") ?? Array.Empty<Int32>().ToIArray();
            SystemSystemType = SystemEntityTable?.GetDataColumnValues<Int32>("int:SystemType") ?? Array.Empty<Int32>().ToIArray();
            ElementInSystemRoles = ElementInSystemEntityTable?.GetDataColumnValues<Int32>("int:Roles") ?? Array.Empty<Int32>().ToIArray();
            WarningGuid = WarningEntityTable?.GetStringColumnValues("string:Guid") ?? Array.Empty<String>().ToIArray();
            WarningSeverity = WarningEntityTable?.GetStringColumnValues("string:Severity") ?? Array.Empty<String>().ToIArray();
            WarningDescription = WarningEntityTable?.GetStringColumnValues("string:Description") ?? Array.Empty<String>().ToIArray();
            BasePointIsSurveyPoint = BasePointEntityTable?.GetDataColumnValues<Boolean>("byte:IsSurveyPoint") ?? Array.Empty<Boolean>().ToIArray();
            BasePointPosition_X = BasePointEntityTable?.GetDataColumnValues<Double>("double:Position.X") ?? Array.Empty<Double>().ToIArray();
            BasePointPosition_Y = BasePointEntityTable?.GetDataColumnValues<Double>("double:Position.Y") ?? Array.Empty<Double>().ToIArray();
            BasePointPosition_Z = BasePointEntityTable?.GetDataColumnValues<Double>("double:Position.Z") ?? Array.Empty<Double>().ToIArray();
            BasePointSharedPosition_X = BasePointEntityTable?.GetDataColumnValues<Double>("double:SharedPosition.X") ?? Array.Empty<Double>().ToIArray();
            BasePointSharedPosition_Y = BasePointEntityTable?.GetDataColumnValues<Double>("double:SharedPosition.Y") ?? Array.Empty<Double>().ToIArray();
            BasePointSharedPosition_Z = BasePointEntityTable?.GetDataColumnValues<Double>("double:SharedPosition.Z") ?? Array.Empty<Double>().ToIArray();
            PhaseFilterNew = PhaseFilterEntityTable?.GetDataColumnValues<Int32>("int:New") ?? Array.Empty<Int32>().ToIArray();
            PhaseFilterExisting = PhaseFilterEntityTable?.GetDataColumnValues<Int32>("int:Existing") ?? Array.Empty<Int32>().ToIArray();
            PhaseFilterDemolished = PhaseFilterEntityTable?.GetDataColumnValues<Int32>("int:Demolished") ?? Array.Empty<Int32>().ToIArray();
            PhaseFilterTemporary = PhaseFilterEntityTable?.GetDataColumnValues<Int32>("int:Temporary") ?? Array.Empty<Int32>().ToIArray();
            GridStartPoint_X = GridEntityTable?.GetDataColumnValues<Double>("double:StartPoint.X") ?? Array.Empty<Double>().ToIArray();
            GridStartPoint_Y = GridEntityTable?.GetDataColumnValues<Double>("double:StartPoint.Y") ?? Array.Empty<Double>().ToIArray();
            GridStartPoint_Z = GridEntityTable?.GetDataColumnValues<Double>("double:StartPoint.Z") ?? Array.Empty<Double>().ToIArray();
            GridEndPoint_X = GridEntityTable?.GetDataColumnValues<Double>("double:EndPoint.X") ?? Array.Empty<Double>().ToIArray();
            GridEndPoint_Y = GridEntityTable?.GetDataColumnValues<Double>("double:EndPoint.Y") ?? Array.Empty<Double>().ToIArray();
            GridEndPoint_Z = GridEntityTable?.GetDataColumnValues<Double>("double:EndPoint.Z") ?? Array.Empty<Double>().ToIArray();
            GridIsCurved = GridEntityTable?.GetDataColumnValues<Boolean>("byte:IsCurved") ?? Array.Empty<Boolean>().ToIArray();
            GridExtents_Min_X = GridEntityTable?.GetDataColumnValues<Double>("double:Extents.Min.X") ?? Array.Empty<Double>().ToIArray();
            GridExtents_Min_Y = GridEntityTable?.GetDataColumnValues<Double>("double:Extents.Min.Y") ?? Array.Empty<Double>().ToIArray();
            GridExtents_Min_Z = GridEntityTable?.GetDataColumnValues<Double>("double:Extents.Min.Z") ?? Array.Empty<Double>().ToIArray();
            GridExtents_Max_X = GridEntityTable?.GetDataColumnValues<Double>("double:Extents.Max.X") ?? Array.Empty<Double>().ToIArray();
            GridExtents_Max_Y = GridEntityTable?.GetDataColumnValues<Double>("double:Extents.Max.Y") ?? Array.Empty<Double>().ToIArray();
            GridExtents_Max_Z = GridEntityTable?.GetDataColumnValues<Double>("double:Extents.Max.Z") ?? Array.Empty<Double>().ToIArray();
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
            AssetInViewSheetAssetIndex = AssetInViewSheetEntityTable?.GetIndexColumnValues("index:Vim.Asset:Asset") ?? Array.Empty<int>().ToIArray();
            AssetInViewSheetViewSheetIndex = AssetInViewSheetEntityTable?.GetIndexColumnValues("index:Vim.ViewSheet:ViewSheet") ?? Array.Empty<int>().ToIArray();
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
            ViewSheetSetElementIndex = ViewSheetSetEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            ViewSheetElementIndex = ViewSheetEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            ViewSheetInViewSheetSetViewSheetIndex = ViewSheetInViewSheetSetEntityTable?.GetIndexColumnValues("index:Vim.ViewSheet:ViewSheet") ?? Array.Empty<int>().ToIArray();
            ViewSheetInViewSheetSetViewSheetSetIndex = ViewSheetInViewSheetSetEntityTable?.GetIndexColumnValues("index:Vim.ViewSheetSet:ViewSheetSet") ?? Array.Empty<int>().ToIArray();
            ViewInViewSheetSetViewIndex = ViewInViewSheetSetEntityTable?.GetIndexColumnValues("index:Vim.View:View") ?? Array.Empty<int>().ToIArray();
            ViewInViewSheetSetViewSheetSetIndex = ViewInViewSheetSetEntityTable?.GetIndexColumnValues("index:Vim.ViewSheetSet:ViewSheetSet") ?? Array.Empty<int>().ToIArray();
            ViewInViewSheetViewIndex = ViewInViewSheetEntityTable?.GetIndexColumnValues("index:Vim.View:View") ?? Array.Empty<int>().ToIArray();
            ViewInViewSheetViewSheetIndex = ViewInViewSheetEntityTable?.GetIndexColumnValues("index:Vim.ViewSheet:ViewSheet") ?? Array.Empty<int>().ToIArray();
            
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
            AssetInViewSheetList = NumAssetInViewSheet.Select(i => GetAssetInViewSheet(i));
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
            ViewSheetSetList = NumViewSheetSet.Select(i => GetViewSheetSet(i));
            ViewSheetList = NumViewSheet.Select(i => GetViewSheet(i));
            ViewSheetInViewSheetSetList = NumViewSheetInViewSheetSet.Select(i => GetViewSheetInViewSheetSet(i));
            ViewInViewSheetSetList = NumViewInViewSheetSet.Select(i => GetViewInViewSheetSet(i));
            ViewInViewSheetList = NumViewInViewSheet.Select(i => GetViewInViewSheet(i));
            
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
            if (type == typeof(AssetInViewSheet)) return ToAssetInViewSheetTableBuilder;
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
            if (type == typeof(ViewSheetSet)) return ToViewSheetSetTableBuilder;
            if (type == typeof(ViewSheet)) return ToViewSheetTableBuilder;
            if (type == typeof(ViewSheetInViewSheetSet)) return ToViewSheetInViewSheetSetTableBuilder;
            if (type == typeof(ViewInViewSheetSet)) return ToViewInViewSheetSetTableBuilder;
            if (type == typeof(ViewInViewSheet)) return ToViewInViewSheetTableBuilder;
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
            tb.AddDataColumn("long:Id", typedEntities.Select(x => x.Id));
            tb.AddStringColumn("string:Type", typedEntities.Select(x => x.Type));
            tb.AddStringColumn("string:Name", typedEntities.Select(x => x.Name));
            tb.AddStringColumn("string:UniqueId", typedEntities.Select(x => x.UniqueId));
            tb.AddDataColumn("float:Location.X", typedEntities.Select(x => x.Location_X));
            tb.AddDataColumn("float:Location.Y", typedEntities.Select(x => x.Location_Y));
            tb.AddDataColumn("float:Location.Z", typedEntities.Select(x => x.Location_Z));
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
            tb.AddDataColumn("float:Position.X", typedEntities.Select(x => x.Position_X));
            tb.AddDataColumn("float:Position.Y", typedEntities.Select(x => x.Position_Y));
            tb.AddDataColumn("float:Position.Z", typedEntities.Select(x => x.Position_Z));
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToGroupTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<Group>() ?? Enumerable.Empty<Group>();
            var tb = new EntityTableBuilder("Vim.Group");
            tb.AddStringColumn("string:GroupType", typedEntities.Select(x => x.GroupType));
            tb.AddDataColumn("float:Position.X", typedEntities.Select(x => x.Position_X));
            tb.AddDataColumn("float:Position.Y", typedEntities.Select(x => x.Position_Y));
            tb.AddDataColumn("float:Position.Z", typedEntities.Select(x => x.Position_Z));
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
            tb.AddDataColumn("long:Id", typedEntities.Select(x => x.Id));
            tb.AddStringColumn("string:CategoryType", typedEntities.Select(x => x.CategoryType));
            tb.AddDataColumn("double:LineColor.X", typedEntities.Select(x => x.LineColor_X));
            tb.AddDataColumn("double:LineColor.Y", typedEntities.Select(x => x.LineColor_Y));
            tb.AddDataColumn("double:LineColor.Z", typedEntities.Select(x => x.LineColor_Z));
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
            tb.AddDataColumn("float:FacingOrientation.X", typedEntities.Select(x => x.FacingOrientation_X));
            tb.AddDataColumn("float:FacingOrientation.Y", typedEntities.Select(x => x.FacingOrientation_Y));
            tb.AddDataColumn("float:FacingOrientation.Z", typedEntities.Select(x => x.FacingOrientation_Z));
            tb.AddDataColumn("byte:HandFlipped", typedEntities.Select(x => x.HandFlipped));
            tb.AddDataColumn("byte:Mirrored", typedEntities.Select(x => x.Mirrored));
            tb.AddDataColumn("byte:HasModifiedGeometry", typedEntities.Select(x => x.HasModifiedGeometry));
            tb.AddDataColumn("float:Scale", typedEntities.Select(x => x.Scale));
            tb.AddDataColumn("float:BasisX.X", typedEntities.Select(x => x.BasisX_X));
            tb.AddDataColumn("float:BasisX.Y", typedEntities.Select(x => x.BasisX_Y));
            tb.AddDataColumn("float:BasisX.Z", typedEntities.Select(x => x.BasisX_Z));
            tb.AddDataColumn("float:BasisY.X", typedEntities.Select(x => x.BasisY_X));
            tb.AddDataColumn("float:BasisY.Y", typedEntities.Select(x => x.BasisY_Y));
            tb.AddDataColumn("float:BasisY.Z", typedEntities.Select(x => x.BasisY_Z));
            tb.AddDataColumn("float:BasisZ.X", typedEntities.Select(x => x.BasisZ_X));
            tb.AddDataColumn("float:BasisZ.Y", typedEntities.Select(x => x.BasisZ_Y));
            tb.AddDataColumn("float:BasisZ.Z", typedEntities.Select(x => x.BasisZ_Z));
            tb.AddDataColumn("float:Translation.X", typedEntities.Select(x => x.Translation_X));
            tb.AddDataColumn("float:Translation.Y", typedEntities.Select(x => x.Translation_Y));
            tb.AddDataColumn("float:Translation.Z", typedEntities.Select(x => x.Translation_Z));
            tb.AddDataColumn("float:HandOrientation.X", typedEntities.Select(x => x.HandOrientation_X));
            tb.AddDataColumn("float:HandOrientation.Y", typedEntities.Select(x => x.HandOrientation_Y));
            tb.AddDataColumn("float:HandOrientation.Z", typedEntities.Select(x => x.HandOrientation_Z));
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
            tb.AddDataColumn("double:Up.X", typedEntities.Select(x => x.Up_X));
            tb.AddDataColumn("double:Up.Y", typedEntities.Select(x => x.Up_Y));
            tb.AddDataColumn("double:Up.Z", typedEntities.Select(x => x.Up_Z));
            tb.AddDataColumn("double:Right.X", typedEntities.Select(x => x.Right_X));
            tb.AddDataColumn("double:Right.Y", typedEntities.Select(x => x.Right_Y));
            tb.AddDataColumn("double:Right.Z", typedEntities.Select(x => x.Right_Z));
            tb.AddDataColumn("double:Origin.X", typedEntities.Select(x => x.Origin_X));
            tb.AddDataColumn("double:Origin.Y", typedEntities.Select(x => x.Origin_Y));
            tb.AddDataColumn("double:Origin.Z", typedEntities.Select(x => x.Origin_Z));
            tb.AddDataColumn("double:ViewDirection.X", typedEntities.Select(x => x.ViewDirection_X));
            tb.AddDataColumn("double:ViewDirection.Y", typedEntities.Select(x => x.ViewDirection_Y));
            tb.AddDataColumn("double:ViewDirection.Z", typedEntities.Select(x => x.ViewDirection_Z));
            tb.AddDataColumn("double:ViewPosition.X", typedEntities.Select(x => x.ViewPosition_X));
            tb.AddDataColumn("double:ViewPosition.Y", typedEntities.Select(x => x.ViewPosition_Y));
            tb.AddDataColumn("double:ViewPosition.Z", typedEntities.Select(x => x.ViewPosition_Z));
            tb.AddDataColumn("double:Scale", typedEntities.Select(x => x.Scale));
            tb.AddDataColumn("double:Outline.Min.X", typedEntities.Select(x => x.Outline_Min_X));
            tb.AddDataColumn("double:Outline.Min.Y", typedEntities.Select(x => x.Outline_Min_Y));
            tb.AddDataColumn("double:Outline.Max.X", typedEntities.Select(x => x.Outline_Max_X));
            tb.AddDataColumn("double:Outline.Max.Y", typedEntities.Select(x => x.Outline_Max_Y));
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
        public static EntityTableBuilder ToAssetInViewSheetTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<AssetInViewSheet>() ?? Enumerable.Empty<AssetInViewSheet>();
            var tb = new EntityTableBuilder("Vim.AssetInViewSheet");
            tb.AddIndexColumn("index:Vim.Asset:Asset", typedEntities.Select(x => x._Asset.Index));
            tb.AddIndexColumn("index:Vim.ViewSheet:ViewSheet", typedEntities.Select(x => x._ViewSheet.Index));
            return tb;
        }
        public static EntityTableBuilder ToLevelInViewTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<LevelInView>() ?? Enumerable.Empty<LevelInView>();
            var tb = new EntityTableBuilder("Vim.LevelInView");
            tb.AddDataColumn("double:Extents.Min.X", typedEntities.Select(x => x.Extents_Min_X));
            tb.AddDataColumn("double:Extents.Min.Y", typedEntities.Select(x => x.Extents_Min_Y));
            tb.AddDataColumn("double:Extents.Min.Z", typedEntities.Select(x => x.Extents_Min_Z));
            tb.AddDataColumn("double:Extents.Max.X", typedEntities.Select(x => x.Extents_Max_X));
            tb.AddDataColumn("double:Extents.Max.Y", typedEntities.Select(x => x.Extents_Max_Y));
            tb.AddDataColumn("double:Extents.Max.Z", typedEntities.Select(x => x.Extents_Max_Z));
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
            tb.AddDataColumn("double:Color.X", typedEntities.Select(x => x.Color_X));
            tb.AddDataColumn("double:Color.Y", typedEntities.Select(x => x.Color_Y));
            tb.AddDataColumn("double:Color.Z", typedEntities.Select(x => x.Color_Z));
            tb.AddDataColumn("double:ColorUvScaling.X", typedEntities.Select(x => x.ColorUvScaling_X));
            tb.AddDataColumn("double:ColorUvScaling.Y", typedEntities.Select(x => x.ColorUvScaling_Y));
            tb.AddDataColumn("double:ColorUvOffset.X", typedEntities.Select(x => x.ColorUvOffset_X));
            tb.AddDataColumn("double:ColorUvOffset.Y", typedEntities.Select(x => x.ColorUvOffset_Y));
            tb.AddDataColumn("double:NormalUvScaling.X", typedEntities.Select(x => x.NormalUvScaling_X));
            tb.AddDataColumn("double:NormalUvScaling.Y", typedEntities.Select(x => x.NormalUvScaling_Y));
            tb.AddDataColumn("double:NormalUvOffset.X", typedEntities.Select(x => x.NormalUvOffset_X));
            tb.AddDataColumn("double:NormalUvOffset.Y", typedEntities.Select(x => x.NormalUvOffset_Y));
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
            tb.AddDataColumn("float:Box.Min.X", typedEntities.Select(x => x.Box_Min_X));
            tb.AddDataColumn("float:Box.Min.Y", typedEntities.Select(x => x.Box_Min_Y));
            tb.AddDataColumn("float:Box.Min.Z", typedEntities.Select(x => x.Box_Min_Z));
            tb.AddDataColumn("float:Box.Max.X", typedEntities.Select(x => x.Box_Max_X));
            tb.AddDataColumn("float:Box.Max.Y", typedEntities.Select(x => x.Box_Max_Y));
            tb.AddDataColumn("float:Box.Max.Z", typedEntities.Select(x => x.Box_Max_Z));
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
            tb.AddDataColumn("double:Position.X", typedEntities.Select(x => x.Position_X));
            tb.AddDataColumn("double:Position.Y", typedEntities.Select(x => x.Position_Y));
            tb.AddDataColumn("double:Position.Z", typedEntities.Select(x => x.Position_Z));
            tb.AddDataColumn("double:SharedPosition.X", typedEntities.Select(x => x.SharedPosition_X));
            tb.AddDataColumn("double:SharedPosition.Y", typedEntities.Select(x => x.SharedPosition_Y));
            tb.AddDataColumn("double:SharedPosition.Z", typedEntities.Select(x => x.SharedPosition_Z));
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
            tb.AddDataColumn("double:StartPoint.X", typedEntities.Select(x => x.StartPoint_X));
            tb.AddDataColumn("double:StartPoint.Y", typedEntities.Select(x => x.StartPoint_Y));
            tb.AddDataColumn("double:StartPoint.Z", typedEntities.Select(x => x.StartPoint_Z));
            tb.AddDataColumn("double:EndPoint.X", typedEntities.Select(x => x.EndPoint_X));
            tb.AddDataColumn("double:EndPoint.Y", typedEntities.Select(x => x.EndPoint_Y));
            tb.AddDataColumn("double:EndPoint.Z", typedEntities.Select(x => x.EndPoint_Z));
            tb.AddDataColumn("byte:IsCurved", typedEntities.Select(x => x.IsCurved));
            tb.AddDataColumn("double:Extents.Min.X", typedEntities.Select(x => x.Extents_Min_X));
            tb.AddDataColumn("double:Extents.Min.Y", typedEntities.Select(x => x.Extents_Min_Y));
            tb.AddDataColumn("double:Extents.Min.Z", typedEntities.Select(x => x.Extents_Min_Z));
            tb.AddDataColumn("double:Extents.Max.X", typedEntities.Select(x => x.Extents_Max_X));
            tb.AddDataColumn("double:Extents.Max.Y", typedEntities.Select(x => x.Extents_Max_Y));
            tb.AddDataColumn("double:Extents.Max.Z", typedEntities.Select(x => x.Extents_Max_Z));
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
        public static EntityTableBuilder ToViewSheetSetTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<ViewSheetSet>() ?? Enumerable.Empty<ViewSheetSet>();
            var tb = new EntityTableBuilder("Vim.ViewSheetSet");
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToViewSheetTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<ViewSheet>() ?? Enumerable.Empty<ViewSheet>();
            var tb = new EntityTableBuilder("Vim.ViewSheet");
            tb.AddIndexColumn("index:Vim.Element:Element", typedEntities.Select(x => x._Element.Index));
            return tb;
        }
        public static EntityTableBuilder ToViewSheetInViewSheetSetTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<ViewSheetInViewSheetSet>() ?? Enumerable.Empty<ViewSheetInViewSheetSet>();
            var tb = new EntityTableBuilder("Vim.ViewSheetInViewSheetSet");
            tb.AddIndexColumn("index:Vim.ViewSheet:ViewSheet", typedEntities.Select(x => x._ViewSheet.Index));
            tb.AddIndexColumn("index:Vim.ViewSheetSet:ViewSheetSet", typedEntities.Select(x => x._ViewSheetSet.Index));
            return tb;
        }
        public static EntityTableBuilder ToViewInViewSheetSetTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<ViewInViewSheetSet>() ?? Enumerable.Empty<ViewInViewSheetSet>();
            var tb = new EntityTableBuilder("Vim.ViewInViewSheetSet");
            tb.AddIndexColumn("index:Vim.View:View", typedEntities.Select(x => x._View.Index));
            tb.AddIndexColumn("index:Vim.ViewSheetSet:ViewSheetSet", typedEntities.Select(x => x._ViewSheetSet.Index));
            return tb;
        }
        public static EntityTableBuilder ToViewInViewSheetTableBuilder(this IEnumerable<Entity> entities)
        {
            var typedEntities = entities?.Cast<ViewInViewSheet>() ?? Enumerable.Empty<ViewInViewSheet>();
            var tb = new EntityTableBuilder("Vim.ViewInViewSheet");
            tb.AddIndexColumn("index:Vim.View:View", typedEntities.Select(x => x._View.Index));
            tb.AddIndexColumn("index:Vim.ViewSheet:ViewSheet", typedEntities.Select(x => x._ViewSheet.Index));
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
            {typeof(AssetInViewSheet), new EntityTableBuilder()},
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
            {typeof(ViewSheetSet), new EntityTableBuilder()},
            {typeof(ViewSheet), new EntityTableBuilder()},
            {typeof(ViewSheetInViewSheetSet), new EntityTableBuilder()},
            {typeof(ViewInViewSheetSet), new EntityTableBuilder()},
            {typeof(ViewInViewSheet), new EntityTableBuilder()},
        };
    } // ObjectModelBuilder
} // namespace
