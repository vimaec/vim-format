// AUTO-GENERATED FILE, DO NOT MODIFY.
// ReSharper disable All
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vim.Math3d;
using Vim.LinqArray;
using Vim.Format.ObjectModel;
using Vim.Util;

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
        public Vim.Format.ObjectModel.DisplayUnit DisplayUnit => _DisplayUnit?.Value;
        public int DisplayUnitIndex => _DisplayUnit?.Index ?? EntityRelation.None;
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
                    (StorageType == other.StorageType) &&
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
        public Vim.Format.ObjectModel.ParameterDescriptor ParameterDescriptor => _ParameterDescriptor?.Value;
        public int ParameterDescriptorIndex => _ParameterDescriptor?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Level Level => _Level?.Value;
        public int LevelIndex => _Level?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Phase PhaseCreated => _PhaseCreated?.Value;
        public int PhaseCreatedIndex => _PhaseCreated?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Phase PhaseDemolished => _PhaseDemolished?.Value;
        public int PhaseDemolishedIndex => _PhaseDemolished?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Category Category => _Category?.Value;
        public int CategoryIndex => _Category?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Workset Workset => _Workset?.Value;
        public int WorksetIndex => _Workset?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.DesignOption DesignOption => _DesignOption?.Value;
        public int DesignOptionIndex => _DesignOption?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.View OwnerView => _OwnerView?.Value;
        public int OwnerViewIndex => _OwnerView?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Group Group => _Group?.Value;
        public int GroupIndex => _Group?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.AssemblyInstance AssemblyInstance => _AssemblyInstance?.Value;
        public int AssemblyInstanceIndex => _AssemblyInstance?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.BimDocument BimDocument => _BimDocument?.Value;
        public int BimDocumentIndex => _BimDocument?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Room Room => _Room?.Value;
        public int RoomIndex => _Room?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.BimDocument BimDocument => _BimDocument?.Value;
        public int BimDocumentIndex => _BimDocument?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.FamilyType FamilyType => _FamilyType?.Value;
        public int FamilyTypeIndex => _FamilyType?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Building Building => _Building?.Value;
        public int BuildingIndex => _Building?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
        public Level()
        {
            _FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>();
            _Building = new Relation<Vim.Format.ObjectModel.Building>();
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Level other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Elevation == other.Elevation) &&
                    (ProjectElevation == other.ProjectElevation) &&
                    (_FamilyType?.Index == other._FamilyType?.Index) &&
                    (_Building?.Index == other._Building?.Index) &&
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
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Level UpperLimit => _UpperLimit?.Value;
        public int UpperLimitIndex => _UpperLimit?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.View ActiveView => _ActiveView?.Value;
        public int ActiveViewIndex => _ActiveView?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Family OwnerFamily => _OwnerFamily?.Value;
        public int OwnerFamilyIndex => _OwnerFamily?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.BimDocument Parent => _Parent?.Value;
        public int ParentIndex => _Parent?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
                    (FileLength == other.FileLength) &&
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
        public Vim.Format.ObjectModel.DisplayUnit DisplayUnit => _DisplayUnit?.Value;
        public int DisplayUnitIndex => _DisplayUnit?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.BimDocument BimDocument => _BimDocument?.Value;
        public int BimDocumentIndex => _BimDocument?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Phase Phase => _Phase?.Value;
        public int PhaseIndex => _Phase?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.BimDocument BimDocument => _BimDocument?.Value;
        public int BimDocumentIndex => _BimDocument?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Category Parent => _Parent?.Value;
        public int ParentIndex => _Parent?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Material Material => _Material?.Value;
        public int MaterialIndex => _Material?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Category FamilyCategory => _FamilyCategory?.Value;
        public int FamilyCategoryIndex => _FamilyCategory?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Family Family => _Family?.Value;
        public int FamilyIndex => _Family?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.CompoundStructure CompoundStructure => _CompoundStructure?.Value;
        public int CompoundStructureIndex => _CompoundStructure?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.FamilyType FamilyType => _FamilyType?.Value;
        public int FamilyTypeIndex => _FamilyType?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Element Host => _Host?.Value;
        public int HostIndex => _Host?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Room FromRoom => _FromRoom?.Value;
        public int FromRoomIndex => _FromRoom?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Room ToRoom => _ToRoom?.Value;
        public int ToRoomIndex => _ToRoom?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Element SuperComponent => _SuperComponent?.Value;
        public int SuperComponentIndex => _SuperComponent?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
        public FamilyInstance()
        {
            _FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>();
            _Host = new Relation<Vim.Format.ObjectModel.Element>();
            _FromRoom = new Relation<Vim.Format.ObjectModel.Room>();
            _ToRoom = new Relation<Vim.Format.ObjectModel.Room>();
            _SuperComponent = new Relation<Vim.Format.ObjectModel.Element>();
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
                    (_SuperComponent?.Index == other._SuperComponent?.Index) &&
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
        public Vim.Format.ObjectModel.Camera Camera => _Camera?.Value;
        public int CameraIndex => _Camera?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.FamilyType FamilyType => _FamilyType?.Value;
        public int FamilyTypeIndex => _FamilyType?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.View View => _View?.Value;
        public int ViewIndex => _View?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Shape Shape => _Shape?.Value;
        public int ShapeIndex => _Shape?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.View View => _View?.Value;
        public int ViewIndex => _View?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Asset Asset => _Asset?.Value;
        public int AssetIndex => _Asset?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.View View => _View?.Value;
        public int ViewIndex => _View?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Asset Asset => _Asset?.Value;
        public int AssetIndex => _Asset?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.ViewSheet ViewSheet => _ViewSheet?.Value;
        public int ViewSheetIndex => _ViewSheet?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Level Level => _Level?.Value;
        public int LevelIndex => _Level?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.View View => _View?.Value;
        public int ViewIndex => _View?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Asset ColorTextureFile => _ColorTextureFile?.Value;
        public int ColorTextureFileIndex => _ColorTextureFile?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Asset NormalTextureFile => _NormalTextureFile?.Value;
        public int NormalTextureFileIndex => _NormalTextureFile?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Material Material => _Material?.Value;
        public int MaterialIndex => _Material?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Material Material => _Material?.Value;
        public int MaterialIndex => _Material?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.CompoundStructure CompoundStructure => _CompoundStructure?.Value;
        public int CompoundStructureIndex => _CompoundStructure?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.CompoundStructureLayer StructuralLayer => _StructuralLayer?.Value;
        public int StructuralLayerIndex => _StructuralLayer?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Shape Shape => _Shape?.Value;
        public int ShapeIndex => _Shape?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.ShapeCollection ShapeCollection => _ShapeCollection?.Value;
        public int ShapeCollectionIndex => _ShapeCollection?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.FamilyType FamilyType => _FamilyType?.Value;
        public int FamilyTypeIndex => _FamilyType?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.System System => _System?.Value;
        public int SystemIndex => _System?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.BimDocument BimDocument => _BimDocument?.Value;
        public int BimDocumentIndex => _BimDocument?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Warning Warning => _Warning?.Value;
        public int WarningIndex => _Warning?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.FamilyType FamilyType => _FamilyType?.Value;
        public int FamilyTypeIndex => _FamilyType?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.AreaScheme AreaScheme => _AreaScheme?.Value;
        public int AreaSchemeIndex => _AreaScheme?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Schedule Schedule => _Schedule?.Value;
        public int ScheduleIndex => _Schedule?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.ScheduleColumn ScheduleColumn => _ScheduleColumn?.Value;
        public int ScheduleColumnIndex => _ScheduleColumn?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.FamilyType FamilyType => _FamilyType?.Value;
        public int FamilyTypeIndex => _FamilyType?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
        public ViewSheet()
        {
            _FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>();
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is ViewSheet other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
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
    public partial class ViewSheetInViewSheetSet
    {
        public Vim.Format.ObjectModel.ViewSheet ViewSheet => _ViewSheet?.Value;
        public int ViewSheetIndex => _ViewSheet?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.ViewSheetSet ViewSheetSet => _ViewSheetSet?.Value;
        public int ViewSheetSetIndex => _ViewSheetSet?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.View View => _View?.Value;
        public int ViewIndex => _View?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.ViewSheetSet ViewSheetSet => _ViewSheetSet?.Value;
        public int ViewSheetSetIndex => _ViewSheetSet?.Index ?? EntityRelation.None;
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
        public Vim.Format.ObjectModel.View View => _View?.Value;
        public int ViewIndex => _View?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.ViewSheet ViewSheet => _ViewSheet?.Value;
        public int ViewSheetIndex => _ViewSheet?.Index ?? EntityRelation.None;
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
    
    // AUTO-GENERATED
    public partial class Site
    {
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
        public Site()
        {
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Site other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Latitude == other.Latitude) &&
                    (Longitude == other.Longitude) &&
                    (Address == other.Address) &&
                    (Elevation == other.Elevation) &&
                    (Number == other.Number) &&
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
    public partial class Building
    {
        public Vim.Format.ObjectModel.Site Site => _Site?.Value;
        public int SiteIndex => _Site?.Index ?? EntityRelation.None;
        public Vim.Format.ObjectModel.Element Element => _Element?.Value;
        public int ElementIndex => _Element?.Index ?? EntityRelation.None;
        public Building()
        {
            _Site = new Relation<Vim.Format.ObjectModel.Site>();
            _Element = new Relation<Vim.Format.ObjectModel.Element>();
        }
        
        public override bool FieldsAreEqual(object obj)
        {
            if ((obj is Building other))
            {
                var fieldsAreEqual =
                    (Index == other.Index) &&
                    (Elevation == other.Elevation) &&
                    (TerrainElevation == other.TerrainElevation) &&
                    (Address == other.Address) &&
                    (_Site?.Index == other._Site?.Index) &&
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
        public IArray<Int32> ParameterDescriptorStorageType { get; }
        public Int32 GetParameterDescriptorStorageType(int index, Int32 defaultValue = default) => ParameterDescriptorStorageType?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
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
            r.StorageType = ParameterDescriptorStorageType.ElementAtOrDefault(n);
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
        public IArray<Double> LevelProjectElevation { get; }
        public Double GetLevelProjectElevation(int index, Double defaultValue = default) => LevelProjectElevation?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> LevelFamilyTypeIndex { get; }
        public int GetLevelFamilyTypeIndex(int index) => LevelFamilyTypeIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> LevelBuildingIndex { get; }
        public int GetLevelBuildingIndex(int index) => LevelBuildingIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
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
            r.ProjectElevation = LevelProjectElevation.ElementAtOrDefault(n);
            r._FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>(GetLevelFamilyTypeIndex(n), GetFamilyType);
            r._Building = new Relation<Vim.Format.ObjectModel.Building>(GetLevelBuildingIndex(n), GetBuilding);
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
        public IArray<Int64> BimDocumentFileLength { get; }
        public Int64 GetBimDocumentFileLength(int index, Int64 defaultValue = default) => BimDocumentFileLength?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
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
            r.FileLength = BimDocumentFileLength.ElementAtOrDefault(n);
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
        public IArray<int> FamilyInstanceSuperComponentIndex { get; }
        public int GetFamilyInstanceSuperComponentIndex(int index) => FamilyInstanceSuperComponentIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
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
            r._SuperComponent = new Relation<Vim.Format.ObjectModel.Element>(GetFamilyInstanceSuperComponentIndex(n), GetElement);
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
        
        public IArray<int> ViewSheetFamilyTypeIndex { get; }
        public int GetViewSheetFamilyTypeIndex(int index) => ViewSheetFamilyTypeIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
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
            r._FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>(GetViewSheetFamilyTypeIndex(n), GetFamilyType);
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
        
        
        // Site
        
        public EntityTable SiteEntityTable { get; }
        
        public IArray<Double> SiteLatitude { get; }
        public Double GetSiteLatitude(int index, Double defaultValue = default) => SiteLatitude?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> SiteLongitude { get; }
        public Double GetSiteLongitude(int index, Double defaultValue = default) => SiteLongitude?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> SiteAddress { get; }
        public String GetSiteAddress(int index, String defaultValue = "") => SiteAddress?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> SiteElevation { get; }
        public Double GetSiteElevation(int index, Double defaultValue = default) => SiteElevation?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> SiteNumber { get; }
        public String GetSiteNumber(int index, String defaultValue = "") => SiteNumber?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> SiteElementIndex { get; }
        public int GetSiteElementIndex(int index) => SiteElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumSite => SiteEntityTable?.NumRows ?? 0;
        public IArray<Site> SiteList { get; }
        public Site GetSite(int n)
        {
            if (n < 0) return null;
            var r = new Site();
            r.Document = Document;
            r.Index = n;
            r.Latitude = SiteLatitude.ElementAtOrDefault(n);
            r.Longitude = SiteLongitude.ElementAtOrDefault(n);
            r.Address = SiteAddress.ElementAtOrDefault(n);
            r.Elevation = SiteElevation.ElementAtOrDefault(n);
            r.Number = SiteNumber.ElementAtOrDefault(n);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetSiteElementIndex(n), GetElement);
            return r;
        }
        
        
        // Building
        
        public EntityTable BuildingEntityTable { get; }
        
        public IArray<Double> BuildingElevation { get; }
        public Double GetBuildingElevation(int index, Double defaultValue = default) => BuildingElevation?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<Double> BuildingTerrainElevation { get; }
        public Double GetBuildingTerrainElevation(int index, Double defaultValue = default) => BuildingTerrainElevation?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<String> BuildingAddress { get; }
        public String GetBuildingAddress(int index, String defaultValue = "") => BuildingAddress?.ElementAtOrDefault(index, defaultValue) ?? defaultValue;
        public IArray<int> BuildingSiteIndex { get; }
        public int GetBuildingSiteIndex(int index) => BuildingSiteIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public IArray<int> BuildingElementIndex { get; }
        public int GetBuildingElementIndex(int index) => BuildingElementIndex?.ElementAtOrDefault(index, EntityRelation.None) ?? EntityRelation.None;
        public int NumBuilding => BuildingEntityTable?.NumRows ?? 0;
        public IArray<Building> BuildingList { get; }
        public Building GetBuilding(int n)
        {
            if (n < 0) return null;
            var r = new Building();
            r.Document = Document;
            r.Index = n;
            r.Elevation = BuildingElevation.ElementAtOrDefault(n);
            r.TerrainElevation = BuildingTerrainElevation.ElementAtOrDefault(n);
            r.Address = BuildingAddress.ElementAtOrDefault(n);
            r._Site = new Relation<Vim.Format.ObjectModel.Site>(GetBuildingSiteIndex(n), GetSite);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetBuildingElementIndex(n), GetElement);
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
            {"Vim.Site", SiteList.ToEnumerable()},
            {"Vim.Building", BuildingList.ToEnumerable()},
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
            {"Vim.Site", typeof(Site)},
            {"Vim.Building", typeof(Building)},
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
            SiteEntityTable = Document.GetTable("Vim.Site");
            BuildingEntityTable = Document.GetTable("Vim.Building");
            
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
            ParameterDescriptorStorageType = ParameterDescriptorEntityTable?.GetDataColumnValues<Int32>("int:StorageType") ?? Array.Empty<Int32>().ToIArray();
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
            LevelProjectElevation = LevelEntityTable?.GetDataColumnValues<Double>("double:ProjectElevation") ?? Array.Empty<Double>().ToIArray();
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
            BimDocumentFileLength = BimDocumentEntityTable?.GetDataColumnValues<Int64>("long:FileLength") ?? Array.Empty<Int64>().ToIArray();
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
            SiteLatitude = SiteEntityTable?.GetDataColumnValues<Double>("double:Latitude") ?? Array.Empty<Double>().ToIArray();
            SiteLongitude = SiteEntityTable?.GetDataColumnValues<Double>("double:Longitude") ?? Array.Empty<Double>().ToIArray();
            SiteAddress = SiteEntityTable?.GetStringColumnValues("string:Address") ?? Array.Empty<String>().ToIArray();
            SiteElevation = SiteEntityTable?.GetDataColumnValues<Double>("double:Elevation") ?? Array.Empty<Double>().ToIArray();
            SiteNumber = SiteEntityTable?.GetStringColumnValues("string:Number") ?? Array.Empty<String>().ToIArray();
            BuildingElevation = BuildingEntityTable?.GetDataColumnValues<Double>("double:Elevation") ?? Array.Empty<Double>().ToIArray();
            BuildingTerrainElevation = BuildingEntityTable?.GetDataColumnValues<Double>("double:TerrainElevation") ?? Array.Empty<Double>().ToIArray();
            BuildingAddress = BuildingEntityTable?.GetStringColumnValues("string:Address") ?? Array.Empty<String>().ToIArray();
            
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
            LevelBuildingIndex = LevelEntityTable?.GetIndexColumnValues("index:Vim.Building:Building") ?? Array.Empty<int>().ToIArray();
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
            FamilyInstanceSuperComponentIndex = FamilyInstanceEntityTable?.GetIndexColumnValues("index:Vim.Element:SuperComponent") ?? Array.Empty<int>().ToIArray();
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
            ViewSheetFamilyTypeIndex = ViewSheetEntityTable?.GetIndexColumnValues("index:Vim.FamilyType:FamilyType") ?? Array.Empty<int>().ToIArray();
            ViewSheetElementIndex = ViewSheetEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            ViewSheetInViewSheetSetViewSheetIndex = ViewSheetInViewSheetSetEntityTable?.GetIndexColumnValues("index:Vim.ViewSheet:ViewSheet") ?? Array.Empty<int>().ToIArray();
            ViewSheetInViewSheetSetViewSheetSetIndex = ViewSheetInViewSheetSetEntityTable?.GetIndexColumnValues("index:Vim.ViewSheetSet:ViewSheetSet") ?? Array.Empty<int>().ToIArray();
            ViewInViewSheetSetViewIndex = ViewInViewSheetSetEntityTable?.GetIndexColumnValues("index:Vim.View:View") ?? Array.Empty<int>().ToIArray();
            ViewInViewSheetSetViewSheetSetIndex = ViewInViewSheetSetEntityTable?.GetIndexColumnValues("index:Vim.ViewSheetSet:ViewSheetSet") ?? Array.Empty<int>().ToIArray();
            ViewInViewSheetViewIndex = ViewInViewSheetEntityTable?.GetIndexColumnValues("index:Vim.View:View") ?? Array.Empty<int>().ToIArray();
            ViewInViewSheetViewSheetIndex = ViewInViewSheetEntityTable?.GetIndexColumnValues("index:Vim.ViewSheet:ViewSheet") ?? Array.Empty<int>().ToIArray();
            SiteElementIndex = SiteEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            BuildingSiteIndex = BuildingEntityTable?.GetIndexColumnValues("index:Vim.Site:Site") ?? Array.Empty<int>().ToIArray();
            BuildingElementIndex = BuildingEntityTable?.GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>().ToIArray();
            
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
            SiteList = NumSite.Select(i => GetSite(i));
            BuildingList = NumBuilding.Select(i => GetBuilding(i));
            
            // Initialize element index maps
            ElementIndexMaps = new ElementIndexMaps(this, inParallel);
        }
    } // Document class
    
    public partial class EntityTableSet
    {
        public string[] StringTable { get; }
        
        public Dictionary<string, SerializableEntityTable> RawTableMap { get; } = new Dictionary<string, SerializableEntityTable>();
        
        public Dictionary<string, EntityTable_v2> Tables { get; } = new Dictionary<string, EntityTable_v2>();
        
        public SerializableEntityTable GetSerializableTableOrEmpty(string tableName)
            => RawTableMap.TryGetValue(tableName, out var result) ? result : new SerializableEntityTable { Name = tableName };
        
        public ElementIndexMaps ElementIndexMaps { get; }
        
        public EntityTableSet(SerializableEntityTable[] rawTables, string[] stringTable, bool inParallel = true)
        {
            StringTable = stringTable;
            
            foreach (var rawTable in rawTables)
                RawTableMap[rawTable.Name] = rawTable;
            
            // Populate the entity tables.
            Tables[TableNames.Asset] = AssetTable = new AssetTable(GetSerializableTableOrEmpty(TableNames.Asset), stringTable, this);
            Tables[TableNames.DisplayUnit] = DisplayUnitTable = new DisplayUnitTable(GetSerializableTableOrEmpty(TableNames.DisplayUnit), stringTable, this);
            Tables[TableNames.ParameterDescriptor] = ParameterDescriptorTable = new ParameterDescriptorTable(GetSerializableTableOrEmpty(TableNames.ParameterDescriptor), stringTable, this);
            Tables[TableNames.Parameter] = ParameterTable = new ParameterTable(GetSerializableTableOrEmpty(TableNames.Parameter), stringTable, this);
            Tables[TableNames.Element] = ElementTable = new ElementTable(GetSerializableTableOrEmpty(TableNames.Element), stringTable, this);
            Tables[TableNames.Workset] = WorksetTable = new WorksetTable(GetSerializableTableOrEmpty(TableNames.Workset), stringTable, this);
            Tables[TableNames.AssemblyInstance] = AssemblyInstanceTable = new AssemblyInstanceTable(GetSerializableTableOrEmpty(TableNames.AssemblyInstance), stringTable, this);
            Tables[TableNames.Group] = GroupTable = new GroupTable(GetSerializableTableOrEmpty(TableNames.Group), stringTable, this);
            Tables[TableNames.DesignOption] = DesignOptionTable = new DesignOptionTable(GetSerializableTableOrEmpty(TableNames.DesignOption), stringTable, this);
            Tables[TableNames.Level] = LevelTable = new LevelTable(GetSerializableTableOrEmpty(TableNames.Level), stringTable, this);
            Tables[TableNames.Phase] = PhaseTable = new PhaseTable(GetSerializableTableOrEmpty(TableNames.Phase), stringTable, this);
            Tables[TableNames.Room] = RoomTable = new RoomTable(GetSerializableTableOrEmpty(TableNames.Room), stringTable, this);
            Tables[TableNames.BimDocument] = BimDocumentTable = new BimDocumentTable(GetSerializableTableOrEmpty(TableNames.BimDocument), stringTable, this);
            Tables[TableNames.DisplayUnitInBimDocument] = DisplayUnitInBimDocumentTable = new DisplayUnitInBimDocumentTable(GetSerializableTableOrEmpty(TableNames.DisplayUnitInBimDocument), stringTable, this);
            Tables[TableNames.PhaseOrderInBimDocument] = PhaseOrderInBimDocumentTable = new PhaseOrderInBimDocumentTable(GetSerializableTableOrEmpty(TableNames.PhaseOrderInBimDocument), stringTable, this);
            Tables[TableNames.Category] = CategoryTable = new CategoryTable(GetSerializableTableOrEmpty(TableNames.Category), stringTable, this);
            Tables[TableNames.Family] = FamilyTable = new FamilyTable(GetSerializableTableOrEmpty(TableNames.Family), stringTable, this);
            Tables[TableNames.FamilyType] = FamilyTypeTable = new FamilyTypeTable(GetSerializableTableOrEmpty(TableNames.FamilyType), stringTable, this);
            Tables[TableNames.FamilyInstance] = FamilyInstanceTable = new FamilyInstanceTable(GetSerializableTableOrEmpty(TableNames.FamilyInstance), stringTable, this);
            Tables[TableNames.View] = ViewTable = new ViewTable(GetSerializableTableOrEmpty(TableNames.View), stringTable, this);
            Tables[TableNames.ElementInView] = ElementInViewTable = new ElementInViewTable(GetSerializableTableOrEmpty(TableNames.ElementInView), stringTable, this);
            Tables[TableNames.ShapeInView] = ShapeInViewTable = new ShapeInViewTable(GetSerializableTableOrEmpty(TableNames.ShapeInView), stringTable, this);
            Tables[TableNames.AssetInView] = AssetInViewTable = new AssetInViewTable(GetSerializableTableOrEmpty(TableNames.AssetInView), stringTable, this);
            Tables[TableNames.AssetInViewSheet] = AssetInViewSheetTable = new AssetInViewSheetTable(GetSerializableTableOrEmpty(TableNames.AssetInViewSheet), stringTable, this);
            Tables[TableNames.LevelInView] = LevelInViewTable = new LevelInViewTable(GetSerializableTableOrEmpty(TableNames.LevelInView), stringTable, this);
            Tables[TableNames.Camera] = CameraTable = new CameraTable(GetSerializableTableOrEmpty(TableNames.Camera), stringTable, this);
            Tables[TableNames.Material] = MaterialTable = new MaterialTable(GetSerializableTableOrEmpty(TableNames.Material), stringTable, this);
            Tables[TableNames.MaterialInElement] = MaterialInElementTable = new MaterialInElementTable(GetSerializableTableOrEmpty(TableNames.MaterialInElement), stringTable, this);
            Tables[TableNames.CompoundStructureLayer] = CompoundStructureLayerTable = new CompoundStructureLayerTable(GetSerializableTableOrEmpty(TableNames.CompoundStructureLayer), stringTable, this);
            Tables[TableNames.CompoundStructure] = CompoundStructureTable = new CompoundStructureTable(GetSerializableTableOrEmpty(TableNames.CompoundStructure), stringTable, this);
            Tables[TableNames.Node] = NodeTable = new NodeTable(GetSerializableTableOrEmpty(TableNames.Node), stringTable, this);
            Tables[TableNames.Geometry] = GeometryTable = new GeometryTable(GetSerializableTableOrEmpty(TableNames.Geometry), stringTable, this);
            Tables[TableNames.Shape] = ShapeTable = new ShapeTable(GetSerializableTableOrEmpty(TableNames.Shape), stringTable, this);
            Tables[TableNames.ShapeCollection] = ShapeCollectionTable = new ShapeCollectionTable(GetSerializableTableOrEmpty(TableNames.ShapeCollection), stringTable, this);
            Tables[TableNames.ShapeInShapeCollection] = ShapeInShapeCollectionTable = new ShapeInShapeCollectionTable(GetSerializableTableOrEmpty(TableNames.ShapeInShapeCollection), stringTable, this);
            Tables[TableNames.System] = SystemTable = new SystemTable(GetSerializableTableOrEmpty(TableNames.System), stringTable, this);
            Tables[TableNames.ElementInSystem] = ElementInSystemTable = new ElementInSystemTable(GetSerializableTableOrEmpty(TableNames.ElementInSystem), stringTable, this);
            Tables[TableNames.Warning] = WarningTable = new WarningTable(GetSerializableTableOrEmpty(TableNames.Warning), stringTable, this);
            Tables[TableNames.ElementInWarning] = ElementInWarningTable = new ElementInWarningTable(GetSerializableTableOrEmpty(TableNames.ElementInWarning), stringTable, this);
            Tables[TableNames.BasePoint] = BasePointTable = new BasePointTable(GetSerializableTableOrEmpty(TableNames.BasePoint), stringTable, this);
            Tables[TableNames.PhaseFilter] = PhaseFilterTable = new PhaseFilterTable(GetSerializableTableOrEmpty(TableNames.PhaseFilter), stringTable, this);
            Tables[TableNames.Grid] = GridTable = new GridTable(GetSerializableTableOrEmpty(TableNames.Grid), stringTable, this);
            Tables[TableNames.Area] = AreaTable = new AreaTable(GetSerializableTableOrEmpty(TableNames.Area), stringTable, this);
            Tables[TableNames.AreaScheme] = AreaSchemeTable = new AreaSchemeTable(GetSerializableTableOrEmpty(TableNames.AreaScheme), stringTable, this);
            Tables[TableNames.Schedule] = ScheduleTable = new ScheduleTable(GetSerializableTableOrEmpty(TableNames.Schedule), stringTable, this);
            Tables[TableNames.ScheduleColumn] = ScheduleColumnTable = new ScheduleColumnTable(GetSerializableTableOrEmpty(TableNames.ScheduleColumn), stringTable, this);
            Tables[TableNames.ScheduleCell] = ScheduleCellTable = new ScheduleCellTable(GetSerializableTableOrEmpty(TableNames.ScheduleCell), stringTable, this);
            Tables[TableNames.ViewSheetSet] = ViewSheetSetTable = new ViewSheetSetTable(GetSerializableTableOrEmpty(TableNames.ViewSheetSet), stringTable, this);
            Tables[TableNames.ViewSheet] = ViewSheetTable = new ViewSheetTable(GetSerializableTableOrEmpty(TableNames.ViewSheet), stringTable, this);
            Tables[TableNames.ViewSheetInViewSheetSet] = ViewSheetInViewSheetSetTable = new ViewSheetInViewSheetSetTable(GetSerializableTableOrEmpty(TableNames.ViewSheetInViewSheetSet), stringTable, this);
            Tables[TableNames.ViewInViewSheetSet] = ViewInViewSheetSetTable = new ViewInViewSheetSetTable(GetSerializableTableOrEmpty(TableNames.ViewInViewSheetSet), stringTable, this);
            Tables[TableNames.ViewInViewSheet] = ViewInViewSheetTable = new ViewInViewSheetTable(GetSerializableTableOrEmpty(TableNames.ViewInViewSheet), stringTable, this);
            Tables[TableNames.Site] = SiteTable = new SiteTable(GetSerializableTableOrEmpty(TableNames.Site), stringTable, this);
            Tables[TableNames.Building] = BuildingTable = new BuildingTable(GetSerializableTableOrEmpty(TableNames.Building), stringTable, this);
            
            // Initialize element index maps
            ElementIndexMaps = new ElementIndexMaps(this, inParallel);
            
        } // EntityTableSet constructor
        
        public AssetTable AssetTable { get; } // can be null
        public Asset GetAsset(int index) => AssetTable?.Get(index);
        public DisplayUnitTable DisplayUnitTable { get; } // can be null
        public DisplayUnit GetDisplayUnit(int index) => DisplayUnitTable?.Get(index);
        public ParameterDescriptorTable ParameterDescriptorTable { get; } // can be null
        public ParameterDescriptor GetParameterDescriptor(int index) => ParameterDescriptorTable?.Get(index);
        public ParameterTable ParameterTable { get; } // can be null
        public Parameter GetParameter(int index) => ParameterTable?.Get(index);
        public ElementTable ElementTable { get; } // can be null
        public Element GetElement(int index) => ElementTable?.Get(index);
        public WorksetTable WorksetTable { get; } // can be null
        public Workset GetWorkset(int index) => WorksetTable?.Get(index);
        public AssemblyInstanceTable AssemblyInstanceTable { get; } // can be null
        public AssemblyInstance GetAssemblyInstance(int index) => AssemblyInstanceTable?.Get(index);
        public GroupTable GroupTable { get; } // can be null
        public Group GetGroup(int index) => GroupTable?.Get(index);
        public DesignOptionTable DesignOptionTable { get; } // can be null
        public DesignOption GetDesignOption(int index) => DesignOptionTable?.Get(index);
        public LevelTable LevelTable { get; } // can be null
        public Level GetLevel(int index) => LevelTable?.Get(index);
        public PhaseTable PhaseTable { get; } // can be null
        public Phase GetPhase(int index) => PhaseTable?.Get(index);
        public RoomTable RoomTable { get; } // can be null
        public Room GetRoom(int index) => RoomTable?.Get(index);
        public BimDocumentTable BimDocumentTable { get; } // can be null
        public BimDocument GetBimDocument(int index) => BimDocumentTable?.Get(index);
        public DisplayUnitInBimDocumentTable DisplayUnitInBimDocumentTable { get; } // can be null
        public DisplayUnitInBimDocument GetDisplayUnitInBimDocument(int index) => DisplayUnitInBimDocumentTable?.Get(index);
        public PhaseOrderInBimDocumentTable PhaseOrderInBimDocumentTable { get; } // can be null
        public PhaseOrderInBimDocument GetPhaseOrderInBimDocument(int index) => PhaseOrderInBimDocumentTable?.Get(index);
        public CategoryTable CategoryTable { get; } // can be null
        public Category GetCategory(int index) => CategoryTable?.Get(index);
        public FamilyTable FamilyTable { get; } // can be null
        public Family GetFamily(int index) => FamilyTable?.Get(index);
        public FamilyTypeTable FamilyTypeTable { get; } // can be null
        public FamilyType GetFamilyType(int index) => FamilyTypeTable?.Get(index);
        public FamilyInstanceTable FamilyInstanceTable { get; } // can be null
        public FamilyInstance GetFamilyInstance(int index) => FamilyInstanceTable?.Get(index);
        public ViewTable ViewTable { get; } // can be null
        public View GetView(int index) => ViewTable?.Get(index);
        public ElementInViewTable ElementInViewTable { get; } // can be null
        public ElementInView GetElementInView(int index) => ElementInViewTable?.Get(index);
        public ShapeInViewTable ShapeInViewTable { get; } // can be null
        public ShapeInView GetShapeInView(int index) => ShapeInViewTable?.Get(index);
        public AssetInViewTable AssetInViewTable { get; } // can be null
        public AssetInView GetAssetInView(int index) => AssetInViewTable?.Get(index);
        public AssetInViewSheetTable AssetInViewSheetTable { get; } // can be null
        public AssetInViewSheet GetAssetInViewSheet(int index) => AssetInViewSheetTable?.Get(index);
        public LevelInViewTable LevelInViewTable { get; } // can be null
        public LevelInView GetLevelInView(int index) => LevelInViewTable?.Get(index);
        public CameraTable CameraTable { get; } // can be null
        public Camera GetCamera(int index) => CameraTable?.Get(index);
        public MaterialTable MaterialTable { get; } // can be null
        public Material GetMaterial(int index) => MaterialTable?.Get(index);
        public MaterialInElementTable MaterialInElementTable { get; } // can be null
        public MaterialInElement GetMaterialInElement(int index) => MaterialInElementTable?.Get(index);
        public CompoundStructureLayerTable CompoundStructureLayerTable { get; } // can be null
        public CompoundStructureLayer GetCompoundStructureLayer(int index) => CompoundStructureLayerTable?.Get(index);
        public CompoundStructureTable CompoundStructureTable { get; } // can be null
        public CompoundStructure GetCompoundStructure(int index) => CompoundStructureTable?.Get(index);
        public NodeTable NodeTable { get; } // can be null
        public Node GetNode(int index) => NodeTable?.Get(index);
        public GeometryTable GeometryTable { get; } // can be null
        public Geometry GetGeometry(int index) => GeometryTable?.Get(index);
        public ShapeTable ShapeTable { get; } // can be null
        public Shape GetShape(int index) => ShapeTable?.Get(index);
        public ShapeCollectionTable ShapeCollectionTable { get; } // can be null
        public ShapeCollection GetShapeCollection(int index) => ShapeCollectionTable?.Get(index);
        public ShapeInShapeCollectionTable ShapeInShapeCollectionTable { get; } // can be null
        public ShapeInShapeCollection GetShapeInShapeCollection(int index) => ShapeInShapeCollectionTable?.Get(index);
        public SystemTable SystemTable { get; } // can be null
        public System GetSystem(int index) => SystemTable?.Get(index);
        public ElementInSystemTable ElementInSystemTable { get; } // can be null
        public ElementInSystem GetElementInSystem(int index) => ElementInSystemTable?.Get(index);
        public WarningTable WarningTable { get; } // can be null
        public Warning GetWarning(int index) => WarningTable?.Get(index);
        public ElementInWarningTable ElementInWarningTable { get; } // can be null
        public ElementInWarning GetElementInWarning(int index) => ElementInWarningTable?.Get(index);
        public BasePointTable BasePointTable { get; } // can be null
        public BasePoint GetBasePoint(int index) => BasePointTable?.Get(index);
        public PhaseFilterTable PhaseFilterTable { get; } // can be null
        public PhaseFilter GetPhaseFilter(int index) => PhaseFilterTable?.Get(index);
        public GridTable GridTable { get; } // can be null
        public Grid GetGrid(int index) => GridTable?.Get(index);
        public AreaTable AreaTable { get; } // can be null
        public Area GetArea(int index) => AreaTable?.Get(index);
        public AreaSchemeTable AreaSchemeTable { get; } // can be null
        public AreaScheme GetAreaScheme(int index) => AreaSchemeTable?.Get(index);
        public ScheduleTable ScheduleTable { get; } // can be null
        public Schedule GetSchedule(int index) => ScheduleTable?.Get(index);
        public ScheduleColumnTable ScheduleColumnTable { get; } // can be null
        public ScheduleColumn GetScheduleColumn(int index) => ScheduleColumnTable?.Get(index);
        public ScheduleCellTable ScheduleCellTable { get; } // can be null
        public ScheduleCell GetScheduleCell(int index) => ScheduleCellTable?.Get(index);
        public ViewSheetSetTable ViewSheetSetTable { get; } // can be null
        public ViewSheetSet GetViewSheetSet(int index) => ViewSheetSetTable?.Get(index);
        public ViewSheetTable ViewSheetTable { get; } // can be null
        public ViewSheet GetViewSheet(int index) => ViewSheetTable?.Get(index);
        public ViewSheetInViewSheetSetTable ViewSheetInViewSheetSetTable { get; } // can be null
        public ViewSheetInViewSheetSet GetViewSheetInViewSheetSet(int index) => ViewSheetInViewSheetSetTable?.Get(index);
        public ViewInViewSheetSetTable ViewInViewSheetSetTable { get; } // can be null
        public ViewInViewSheetSet GetViewInViewSheetSet(int index) => ViewInViewSheetSetTable?.Get(index);
        public ViewInViewSheetTable ViewInViewSheetTable { get; } // can be null
        public ViewInViewSheet GetViewInViewSheet(int index) => ViewInViewSheetTable?.Get(index);
        public SiteTable SiteTable { get; } // can be null
        public Site GetSite(int index) => SiteTable?.Get(index);
        public BuildingTable BuildingTable { get; } // can be null
        public Building GetBuilding(int index) => BuildingTable?.Get(index);
        
        public static HashSet<string> GetElementKindTableNames()
            => new HashSet<string>()
            {
            TableNames.AssemblyInstance,
            TableNames.Group,
            TableNames.DesignOption,
            TableNames.Level,
            TableNames.Phase,
            TableNames.Room,
            TableNames.BimDocument,
            TableNames.Family,
            TableNames.FamilyType,
            TableNames.FamilyInstance,
            TableNames.View,
            TableNames.Material,
            TableNames.System,
            TableNames.BasePoint,
            TableNames.PhaseFilter,
            TableNames.Grid,
            TableNames.Area,
            TableNames.AreaScheme,
            TableNames.Schedule,
            TableNames.ViewSheetSet,
            TableNames.ViewSheet,
            TableNames.Site,
            TableNames.Building,
            };
        
        // Returns an array defining a 1:1 association of Element to its ElementKind
        public ElementKind[] GetElementKinds()
        {
            var elementKinds = new ElementKind[ElementTable?.RowCount ?? 0];
            
            if (elementKinds.Length == 0) return elementKinds;
            
            // Initialize all element kinds to unknown
            for (var i = 0; i < elementKinds.Length; ++i) { elementKinds[i] = ElementKind.Unknown; }
            
            // Populate the element kinds from the relevant entity tables
            for (var i = 0; i < (AssemblyInstanceTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = AssemblyInstanceTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.AssemblyInstance;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (GroupTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = GroupTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.Group;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (DesignOptionTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = DesignOptionTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.DesignOption;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (LevelTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = LevelTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.Level;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (PhaseTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = PhaseTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.Phase;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (RoomTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = RoomTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.Room;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (BimDocumentTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = BimDocumentTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.BimDocument;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (FamilyTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = FamilyTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.Family;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (FamilyTypeTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = FamilyTypeTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.FamilyType;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (FamilyInstanceTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = FamilyInstanceTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.FamilyInstance;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (ViewTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = ViewTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.View;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (MaterialTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = MaterialTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.Material;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (SystemTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = SystemTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.System;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (BasePointTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = BasePointTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.BasePoint;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (PhaseFilterTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = PhaseFilterTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.PhaseFilter;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (GridTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = GridTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.Grid;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (AreaTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = AreaTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.Area;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (AreaSchemeTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = AreaSchemeTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.AreaScheme;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (ScheduleTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = ScheduleTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.Schedule;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (ViewSheetSetTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = ViewSheetSetTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.ViewSheetSet;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (ViewSheetTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = ViewSheetTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.ViewSheet;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (SiteTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = SiteTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.Site;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            for (var i = 0; i < (BuildingTable?.RowCount ?? 0); ++i)
            {
                var elementIndex = BuildingTable?.Column_ElementIndex[i] ?? EntityRelation.None;
                if (elementIndex < 0 || elementIndex >= elementKinds.Length) continue;
                
                var currentElementKind = elementKinds[elementIndex];
                var candidateElementKind = ElementKind.Building;
                
                // Only update the element kind if it is unknown or if it is less than the current kind.
                if (currentElementKind != ElementKind.Unknown && currentElementKind <= candidateElementKind) continue;
                
                elementKinds[elementIndex] = candidateElementKind;
            }
            
            return elementKinds;
        } // GetElementKinds()
    } // class EntityTableSet
    
    public partial class AssetTable : EntityTable_v2, IEnumerable<Asset>
    {
        
        public const string TableName = TableNames.Asset;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public AssetTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_BufferName = GetStringColumnValues("string:BufferName") ?? Array.Empty<String>();
        }
        
        public String[] Column_BufferName { get; }
        public String GetBufferName(int index, String @default = "") => Column_BufferName.ElementAtOrDefault(index, @default);
        // Object Getter
        public Asset Get(int index)
        {
            if (index < 0) return null;
            var r = new Asset();
            r.Index = index;
            r.BufferName = GetBufferName(index);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Asset> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class AssetTable 
    
    public partial class DisplayUnitTable : EntityTable_v2, IEnumerable<DisplayUnit>
    {
        
        public const string TableName = TableNames.DisplayUnit;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public DisplayUnitTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Spec = GetStringColumnValues("string:Spec") ?? Array.Empty<String>();
            Column_Type = GetStringColumnValues("string:Type") ?? Array.Empty<String>();
            Column_Label = GetStringColumnValues("string:Label") ?? Array.Empty<String>();
        }
        
        public String[] Column_Spec { get; }
        public String GetSpec(int index, String @default = "") => Column_Spec.ElementAtOrDefault(index, @default);
        public String[] Column_Type { get; }
        public String GetType(int index, String @default = "") => Column_Type.ElementAtOrDefault(index, @default);
        public String[] Column_Label { get; }
        public String GetLabel(int index, String @default = "") => Column_Label.ElementAtOrDefault(index, @default);
        // Object Getter
        public DisplayUnit Get(int index)
        {
            if (index < 0) return null;
            var r = new DisplayUnit();
            r.Index = index;
            r.Spec = GetSpec(index);
            r.Type = GetType(index);
            r.Label = GetLabel(index);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<DisplayUnit> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class DisplayUnitTable 
    
    public partial class ParameterDescriptorTable : EntityTable_v2, IEnumerable<ParameterDescriptor>
    {
        
        public const string TableName = TableNames.ParameterDescriptor;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public ParameterDescriptorTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Name = GetStringColumnValues("string:Name") ?? Array.Empty<String>();
            Column_Group = GetStringColumnValues("string:Group") ?? Array.Empty<String>();
            Column_ParameterType = GetStringColumnValues("string:ParameterType") ?? Array.Empty<String>();
            Column_IsInstance = GetDataColumnValues<Boolean>("byte:IsInstance") ?? Array.Empty<Boolean>();
            Column_IsShared = GetDataColumnValues<Boolean>("byte:IsShared") ?? Array.Empty<Boolean>();
            Column_IsReadOnly = GetDataColumnValues<Boolean>("byte:IsReadOnly") ?? Array.Empty<Boolean>();
            Column_Flags = GetDataColumnValues<Int32>("int:Flags") ?? Array.Empty<Int32>();
            Column_Guid = GetStringColumnValues("string:Guid") ?? Array.Empty<String>();
            Column_StorageType = GetDataColumnValues<Int32>("int:StorageType") ?? Array.Empty<Int32>();
            Column_DisplayUnitIndex = GetIndexColumnValues("index:Vim.DisplayUnit:DisplayUnit") ?? Array.Empty<int>();
        }
        
        public String[] Column_Name { get; }
        public String GetName(int index, String @default = "") => Column_Name.ElementAtOrDefault(index, @default);
        public String[] Column_Group { get; }
        public String GetGroup(int index, String @default = "") => Column_Group.ElementAtOrDefault(index, @default);
        public String[] Column_ParameterType { get; }
        public String GetParameterType(int index, String @default = "") => Column_ParameterType.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsInstance { get; }
        public Boolean GetIsInstance(int index, Boolean @default = default) => Column_IsInstance.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsShared { get; }
        public Boolean GetIsShared(int index, Boolean @default = default) => Column_IsShared.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsReadOnly { get; }
        public Boolean GetIsReadOnly(int index, Boolean @default = default) => Column_IsReadOnly.ElementAtOrDefault(index, @default);
        public Int32[] Column_Flags { get; }
        public Int32 GetFlags(int index, Int32 @default = default) => Column_Flags.ElementAtOrDefault(index, @default);
        public String[] Column_Guid { get; }
        public String GetGuid(int index, String @default = "") => Column_Guid.ElementAtOrDefault(index, @default);
        public Int32[] Column_StorageType { get; }
        public Int32 GetStorageType(int index, Int32 @default = default) => Column_StorageType.ElementAtOrDefault(index, @default);
        public int[] Column_DisplayUnitIndex { get; }
        public int GetDisplayUnitIndex(int index) => Column_DisplayUnitIndex.ElementAtOrDefault(index, EntityRelation.None);
        public DisplayUnit GetDisplayUnit(int index) => _GetReferencedDisplayUnit(GetDisplayUnitIndex(index));
        private DisplayUnit _GetReferencedDisplayUnit(int referencedIndex) => ParentTableSet.GetDisplayUnit(referencedIndex);
        // Object Getter
        public ParameterDescriptor Get(int index)
        {
            if (index < 0) return null;
            var r = new ParameterDescriptor();
            r.Index = index;
            r.Name = GetName(index);
            r.Group = GetGroup(index);
            r.ParameterType = GetParameterType(index);
            r.IsInstance = GetIsInstance(index);
            r.IsShared = GetIsShared(index);
            r.IsReadOnly = GetIsReadOnly(index);
            r.Flags = GetFlags(index);
            r.Guid = GetGuid(index);
            r.StorageType = GetStorageType(index);
            r._DisplayUnit = new Relation<Vim.Format.ObjectModel.DisplayUnit>(GetDisplayUnitIndex(index), _GetReferencedDisplayUnit);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<ParameterDescriptor> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ParameterDescriptorTable 
    
    public partial class ParameterTable : EntityTable_v2, IEnumerable<Parameter>
    {
        
        public const string TableName = TableNames.Parameter;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public ParameterTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Value = GetStringColumnValues("string:Value") ?? Array.Empty<String>();
            Column_ParameterDescriptorIndex = GetIndexColumnValues("index:Vim.ParameterDescriptor:ParameterDescriptor") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public String[] Column_Value { get; }
        public String GetValue(int index, String @default = "") => Column_Value.ElementAtOrDefault(index, @default);
        public int[] Column_ParameterDescriptorIndex { get; }
        public int GetParameterDescriptorIndex(int index) => Column_ParameterDescriptorIndex.ElementAtOrDefault(index, EntityRelation.None);
        public ParameterDescriptor GetParameterDescriptor(int index) => _GetReferencedParameterDescriptor(GetParameterDescriptorIndex(index));
        private ParameterDescriptor _GetReferencedParameterDescriptor(int referencedIndex) => ParentTableSet.GetParameterDescriptor(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Parameter Get(int index)
        {
            if (index < 0) return null;
            var r = new Parameter();
            r.Index = index;
            r.Value = GetValue(index);
            r._ParameterDescriptor = new Relation<Vim.Format.ObjectModel.ParameterDescriptor>(GetParameterDescriptorIndex(index), _GetReferencedParameterDescriptor);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Parameter> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ParameterTable 
    
    public partial class ElementTable : EntityTable_v2, IEnumerable<Element>
    {
        
        public const string TableName = TableNames.Element;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public ElementTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Id = (GetDataColumnValues<Int64>("long:Id") ?? GetDataColumnValues<Int32>("int:Id")?.Select(v => (Int64) v).ToArray()) ?? Array.Empty<Int64>();
            Column_Type = GetStringColumnValues("string:Type") ?? Array.Empty<String>();
            Column_Name = GetStringColumnValues("string:Name") ?? Array.Empty<String>();
            Column_UniqueId = GetStringColumnValues("string:UniqueId") ?? Array.Empty<String>();
            Column_Location_X = GetDataColumnValues<Single>("float:Location.X") ?? Array.Empty<Single>();
            Column_Location_Y = GetDataColumnValues<Single>("float:Location.Y") ?? Array.Empty<Single>();
            Column_Location_Z = GetDataColumnValues<Single>("float:Location.Z") ?? Array.Empty<Single>();
            Column_FamilyName = GetStringColumnValues("string:FamilyName") ?? Array.Empty<String>();
            Column_IsPinned = GetDataColumnValues<Boolean>("byte:IsPinned") ?? Array.Empty<Boolean>();
            Column_LevelIndex = GetIndexColumnValues("index:Vim.Level:Level") ?? Array.Empty<int>();
            Column_PhaseCreatedIndex = GetIndexColumnValues("index:Vim.Phase:PhaseCreated") ?? Array.Empty<int>();
            Column_PhaseDemolishedIndex = GetIndexColumnValues("index:Vim.Phase:PhaseDemolished") ?? Array.Empty<int>();
            Column_CategoryIndex = GetIndexColumnValues("index:Vim.Category:Category") ?? Array.Empty<int>();
            Column_WorksetIndex = GetIndexColumnValues("index:Vim.Workset:Workset") ?? Array.Empty<int>();
            Column_DesignOptionIndex = GetIndexColumnValues("index:Vim.DesignOption:DesignOption") ?? Array.Empty<int>();
            Column_OwnerViewIndex = GetIndexColumnValues("index:Vim.View:OwnerView") ?? Array.Empty<int>();
            Column_GroupIndex = GetIndexColumnValues("index:Vim.Group:Group") ?? Array.Empty<int>();
            Column_AssemblyInstanceIndex = GetIndexColumnValues("index:Vim.AssemblyInstance:AssemblyInstance") ?? Array.Empty<int>();
            Column_BimDocumentIndex = GetIndexColumnValues("index:Vim.BimDocument:BimDocument") ?? Array.Empty<int>();
            Column_RoomIndex = GetIndexColumnValues("index:Vim.Room:Room") ?? Array.Empty<int>();
        }
        
        public Int64[] Column_Id { get; }
        public Int64 GetId(int index, Int64 @default = default) => Column_Id.ElementAtOrDefault(index, @default);
        public String[] Column_Type { get; }
        public String GetType(int index, String @default = "") => Column_Type.ElementAtOrDefault(index, @default);
        public String[] Column_Name { get; }
        public String GetName(int index, String @default = "") => Column_Name.ElementAtOrDefault(index, @default);
        public String[] Column_UniqueId { get; }
        public String GetUniqueId(int index, String @default = "") => Column_UniqueId.ElementAtOrDefault(index, @default);
        public Single[] Column_Location_X { get; }
        public Single GetLocation_X(int index, Single @default = default) => Column_Location_X.ElementAtOrDefault(index, @default);
        public Single[] Column_Location_Y { get; }
        public Single GetLocation_Y(int index, Single @default = default) => Column_Location_Y.ElementAtOrDefault(index, @default);
        public Single[] Column_Location_Z { get; }
        public Single GetLocation_Z(int index, Single @default = default) => Column_Location_Z.ElementAtOrDefault(index, @default);
        public String[] Column_FamilyName { get; }
        public String GetFamilyName(int index, String @default = "") => Column_FamilyName.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsPinned { get; }
        public Boolean GetIsPinned(int index, Boolean @default = default) => Column_IsPinned.ElementAtOrDefault(index, @default);
        public int[] Column_LevelIndex { get; }
        public int GetLevelIndex(int index) => Column_LevelIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Level GetLevel(int index) => _GetReferencedLevel(GetLevelIndex(index));
        private Level _GetReferencedLevel(int referencedIndex) => ParentTableSet.GetLevel(referencedIndex);
        public int[] Column_PhaseCreatedIndex { get; }
        public int GetPhaseCreatedIndex(int index) => Column_PhaseCreatedIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Phase GetPhaseCreated(int index) => _GetReferencedPhaseCreated(GetPhaseCreatedIndex(index));
        private Phase _GetReferencedPhaseCreated(int referencedIndex) => ParentTableSet.GetPhase(referencedIndex);
        public int[] Column_PhaseDemolishedIndex { get; }
        public int GetPhaseDemolishedIndex(int index) => Column_PhaseDemolishedIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Phase GetPhaseDemolished(int index) => _GetReferencedPhaseDemolished(GetPhaseDemolishedIndex(index));
        private Phase _GetReferencedPhaseDemolished(int referencedIndex) => ParentTableSet.GetPhase(referencedIndex);
        public int[] Column_CategoryIndex { get; }
        public int GetCategoryIndex(int index) => Column_CategoryIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Category GetCategory(int index) => _GetReferencedCategory(GetCategoryIndex(index));
        private Category _GetReferencedCategory(int referencedIndex) => ParentTableSet.GetCategory(referencedIndex);
        public int[] Column_WorksetIndex { get; }
        public int GetWorksetIndex(int index) => Column_WorksetIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Workset GetWorkset(int index) => _GetReferencedWorkset(GetWorksetIndex(index));
        private Workset _GetReferencedWorkset(int referencedIndex) => ParentTableSet.GetWorkset(referencedIndex);
        public int[] Column_DesignOptionIndex { get; }
        public int GetDesignOptionIndex(int index) => Column_DesignOptionIndex.ElementAtOrDefault(index, EntityRelation.None);
        public DesignOption GetDesignOption(int index) => _GetReferencedDesignOption(GetDesignOptionIndex(index));
        private DesignOption _GetReferencedDesignOption(int referencedIndex) => ParentTableSet.GetDesignOption(referencedIndex);
        public int[] Column_OwnerViewIndex { get; }
        public int GetOwnerViewIndex(int index) => Column_OwnerViewIndex.ElementAtOrDefault(index, EntityRelation.None);
        public View GetOwnerView(int index) => _GetReferencedOwnerView(GetOwnerViewIndex(index));
        private View _GetReferencedOwnerView(int referencedIndex) => ParentTableSet.GetView(referencedIndex);
        public int[] Column_GroupIndex { get; }
        public int GetGroupIndex(int index) => Column_GroupIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Group GetGroup(int index) => _GetReferencedGroup(GetGroupIndex(index));
        private Group _GetReferencedGroup(int referencedIndex) => ParentTableSet.GetGroup(referencedIndex);
        public int[] Column_AssemblyInstanceIndex { get; }
        public int GetAssemblyInstanceIndex(int index) => Column_AssemblyInstanceIndex.ElementAtOrDefault(index, EntityRelation.None);
        public AssemblyInstance GetAssemblyInstance(int index) => _GetReferencedAssemblyInstance(GetAssemblyInstanceIndex(index));
        private AssemblyInstance _GetReferencedAssemblyInstance(int referencedIndex) => ParentTableSet.GetAssemblyInstance(referencedIndex);
        public int[] Column_BimDocumentIndex { get; }
        public int GetBimDocumentIndex(int index) => Column_BimDocumentIndex.ElementAtOrDefault(index, EntityRelation.None);
        public BimDocument GetBimDocument(int index) => _GetReferencedBimDocument(GetBimDocumentIndex(index));
        private BimDocument _GetReferencedBimDocument(int referencedIndex) => ParentTableSet.GetBimDocument(referencedIndex);
        public int[] Column_RoomIndex { get; }
        public int GetRoomIndex(int index) => Column_RoomIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Room GetRoom(int index) => _GetReferencedRoom(GetRoomIndex(index));
        private Room _GetReferencedRoom(int referencedIndex) => ParentTableSet.GetRoom(referencedIndex);
        // Object Getter
        public Element Get(int index)
        {
            if (index < 0) return null;
            var r = new Element();
            r.Index = index;
            r.Id = GetId(index);
            r.Type = GetType(index);
            r.Name = GetName(index);
            r.UniqueId = GetUniqueId(index);
            r.Location_X = GetLocation_X(index);
            r.Location_Y = GetLocation_Y(index);
            r.Location_Z = GetLocation_Z(index);
            r.FamilyName = GetFamilyName(index);
            r.IsPinned = GetIsPinned(index);
            r._Level = new Relation<Vim.Format.ObjectModel.Level>(GetLevelIndex(index), _GetReferencedLevel);
            r._PhaseCreated = new Relation<Vim.Format.ObjectModel.Phase>(GetPhaseCreatedIndex(index), _GetReferencedPhaseCreated);
            r._PhaseDemolished = new Relation<Vim.Format.ObjectModel.Phase>(GetPhaseDemolishedIndex(index), _GetReferencedPhaseDemolished);
            r._Category = new Relation<Vim.Format.ObjectModel.Category>(GetCategoryIndex(index), _GetReferencedCategory);
            r._Workset = new Relation<Vim.Format.ObjectModel.Workset>(GetWorksetIndex(index), _GetReferencedWorkset);
            r._DesignOption = new Relation<Vim.Format.ObjectModel.DesignOption>(GetDesignOptionIndex(index), _GetReferencedDesignOption);
            r._OwnerView = new Relation<Vim.Format.ObjectModel.View>(GetOwnerViewIndex(index), _GetReferencedOwnerView);
            r._Group = new Relation<Vim.Format.ObjectModel.Group>(GetGroupIndex(index), _GetReferencedGroup);
            r._AssemblyInstance = new Relation<Vim.Format.ObjectModel.AssemblyInstance>(GetAssemblyInstanceIndex(index), _GetReferencedAssemblyInstance);
            r._BimDocument = new Relation<Vim.Format.ObjectModel.BimDocument>(GetBimDocumentIndex(index), _GetReferencedBimDocument);
            r._Room = new Relation<Vim.Format.ObjectModel.Room>(GetRoomIndex(index), _GetReferencedRoom);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Element> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ElementTable 
    
    public partial class WorksetTable : EntityTable_v2, IEnumerable<Workset>
    {
        
        public const string TableName = TableNames.Workset;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public WorksetTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Id = GetDataColumnValues<Int32>("int:Id") ?? Array.Empty<Int32>();
            Column_Name = GetStringColumnValues("string:Name") ?? Array.Empty<String>();
            Column_Kind = GetStringColumnValues("string:Kind") ?? Array.Empty<String>();
            Column_IsOpen = GetDataColumnValues<Boolean>("byte:IsOpen") ?? Array.Empty<Boolean>();
            Column_IsEditable = GetDataColumnValues<Boolean>("byte:IsEditable") ?? Array.Empty<Boolean>();
            Column_Owner = GetStringColumnValues("string:Owner") ?? Array.Empty<String>();
            Column_UniqueId = GetStringColumnValues("string:UniqueId") ?? Array.Empty<String>();
            Column_BimDocumentIndex = GetIndexColumnValues("index:Vim.BimDocument:BimDocument") ?? Array.Empty<int>();
        }
        
        public Int32[] Column_Id { get; }
        public Int32 GetId(int index, Int32 @default = default) => Column_Id.ElementAtOrDefault(index, @default);
        public String[] Column_Name { get; }
        public String GetName(int index, String @default = "") => Column_Name.ElementAtOrDefault(index, @default);
        public String[] Column_Kind { get; }
        public String GetKind(int index, String @default = "") => Column_Kind.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsOpen { get; }
        public Boolean GetIsOpen(int index, Boolean @default = default) => Column_IsOpen.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsEditable { get; }
        public Boolean GetIsEditable(int index, Boolean @default = default) => Column_IsEditable.ElementAtOrDefault(index, @default);
        public String[] Column_Owner { get; }
        public String GetOwner(int index, String @default = "") => Column_Owner.ElementAtOrDefault(index, @default);
        public String[] Column_UniqueId { get; }
        public String GetUniqueId(int index, String @default = "") => Column_UniqueId.ElementAtOrDefault(index, @default);
        public int[] Column_BimDocumentIndex { get; }
        public int GetBimDocumentIndex(int index) => Column_BimDocumentIndex.ElementAtOrDefault(index, EntityRelation.None);
        public BimDocument GetBimDocument(int index) => _GetReferencedBimDocument(GetBimDocumentIndex(index));
        private BimDocument _GetReferencedBimDocument(int referencedIndex) => ParentTableSet.GetBimDocument(referencedIndex);
        // Object Getter
        public Workset Get(int index)
        {
            if (index < 0) return null;
            var r = new Workset();
            r.Index = index;
            r.Id = GetId(index);
            r.Name = GetName(index);
            r.Kind = GetKind(index);
            r.IsOpen = GetIsOpen(index);
            r.IsEditable = GetIsEditable(index);
            r.Owner = GetOwner(index);
            r.UniqueId = GetUniqueId(index);
            r._BimDocument = new Relation<Vim.Format.ObjectModel.BimDocument>(GetBimDocumentIndex(index), _GetReferencedBimDocument);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Workset> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class WorksetTable 
    
    public partial class AssemblyInstanceTable : EntityTable_v2, IEnumerable<AssemblyInstance> , IElementKindTable
    {
        
        public const string TableName = TableNames.AssemblyInstance;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public AssemblyInstanceTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_AssemblyTypeName = GetStringColumnValues("string:AssemblyTypeName") ?? Array.Empty<String>();
            Column_Position_X = GetDataColumnValues<Single>("float:Position.X") ?? Array.Empty<Single>();
            Column_Position_Y = GetDataColumnValues<Single>("float:Position.Y") ?? Array.Empty<Single>();
            Column_Position_Z = GetDataColumnValues<Single>("float:Position.Z") ?? Array.Empty<Single>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public String[] Column_AssemblyTypeName { get; }
        public String GetAssemblyTypeName(int index, String @default = "") => Column_AssemblyTypeName.ElementAtOrDefault(index, @default);
        public Single[] Column_Position_X { get; }
        public Single GetPosition_X(int index, Single @default = default) => Column_Position_X.ElementAtOrDefault(index, @default);
        public Single[] Column_Position_Y { get; }
        public Single GetPosition_Y(int index, Single @default = default) => Column_Position_Y.ElementAtOrDefault(index, @default);
        public Single[] Column_Position_Z { get; }
        public Single GetPosition_Z(int index, Single @default = default) => Column_Position_Z.ElementAtOrDefault(index, @default);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public AssemblyInstance Get(int index)
        {
            if (index < 0) return null;
            var r = new AssemblyInstance();
            r.Index = index;
            r.AssemblyTypeName = GetAssemblyTypeName(index);
            r.Position_X = GetPosition_X(index);
            r.Position_Y = GetPosition_Y(index);
            r.Position_Z = GetPosition_Z(index);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<AssemblyInstance> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class AssemblyInstanceTable 
    
    public partial class GroupTable : EntityTable_v2, IEnumerable<Group> , IElementKindTable
    {
        
        public const string TableName = TableNames.Group;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public GroupTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_GroupType = GetStringColumnValues("string:GroupType") ?? Array.Empty<String>();
            Column_Position_X = GetDataColumnValues<Single>("float:Position.X") ?? Array.Empty<Single>();
            Column_Position_Y = GetDataColumnValues<Single>("float:Position.Y") ?? Array.Empty<Single>();
            Column_Position_Z = GetDataColumnValues<Single>("float:Position.Z") ?? Array.Empty<Single>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public String[] Column_GroupType { get; }
        public String GetGroupType(int index, String @default = "") => Column_GroupType.ElementAtOrDefault(index, @default);
        public Single[] Column_Position_X { get; }
        public Single GetPosition_X(int index, Single @default = default) => Column_Position_X.ElementAtOrDefault(index, @default);
        public Single[] Column_Position_Y { get; }
        public Single GetPosition_Y(int index, Single @default = default) => Column_Position_Y.ElementAtOrDefault(index, @default);
        public Single[] Column_Position_Z { get; }
        public Single GetPosition_Z(int index, Single @default = default) => Column_Position_Z.ElementAtOrDefault(index, @default);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Group Get(int index)
        {
            if (index < 0) return null;
            var r = new Group();
            r.Index = index;
            r.GroupType = GetGroupType(index);
            r.Position_X = GetPosition_X(index);
            r.Position_Y = GetPosition_Y(index);
            r.Position_Z = GetPosition_Z(index);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Group> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class GroupTable 
    
    public partial class DesignOptionTable : EntityTable_v2, IEnumerable<DesignOption> , IElementKindTable
    {
        
        public const string TableName = TableNames.DesignOption;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public DesignOptionTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_IsPrimary = GetDataColumnValues<Boolean>("byte:IsPrimary") ?? Array.Empty<Boolean>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Boolean[] Column_IsPrimary { get; }
        public Boolean GetIsPrimary(int index, Boolean @default = default) => Column_IsPrimary.ElementAtOrDefault(index, @default);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public DesignOption Get(int index)
        {
            if (index < 0) return null;
            var r = new DesignOption();
            r.Index = index;
            r.IsPrimary = GetIsPrimary(index);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<DesignOption> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class DesignOptionTable 
    
    public partial class LevelTable : EntityTable_v2, IEnumerable<Level> , IElementKindTable
    {
        
        public const string TableName = TableNames.Level;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public LevelTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Elevation = GetDataColumnValues<Double>("double:Elevation") ?? Array.Empty<Double>();
            Column_ProjectElevation = GetDataColumnValues<Double>("double:ProjectElevation") ?? Array.Empty<Double>();
            Column_FamilyTypeIndex = GetIndexColumnValues("index:Vim.FamilyType:FamilyType") ?? Array.Empty<int>();
            Column_BuildingIndex = GetIndexColumnValues("index:Vim.Building:Building") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Double[] Column_Elevation { get; }
        public Double GetElevation(int index, Double @default = default) => Column_Elevation.ElementAtOrDefault(index, @default);
        public Double[] Column_ProjectElevation { get; }
        public Double GetProjectElevation(int index, Double @default = default) => Column_ProjectElevation.ElementAtOrDefault(index, @default);
        public int[] Column_FamilyTypeIndex { get; }
        public int GetFamilyTypeIndex(int index) => Column_FamilyTypeIndex.ElementAtOrDefault(index, EntityRelation.None);
        public FamilyType GetFamilyType(int index) => _GetReferencedFamilyType(GetFamilyTypeIndex(index));
        private FamilyType _GetReferencedFamilyType(int referencedIndex) => ParentTableSet.GetFamilyType(referencedIndex);
        public int[] Column_BuildingIndex { get; }
        public int GetBuildingIndex(int index) => Column_BuildingIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Building GetBuilding(int index) => _GetReferencedBuilding(GetBuildingIndex(index));
        private Building _GetReferencedBuilding(int referencedIndex) => ParentTableSet.GetBuilding(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Level Get(int index)
        {
            if (index < 0) return null;
            var r = new Level();
            r.Index = index;
            r.Elevation = GetElevation(index);
            r.ProjectElevation = GetProjectElevation(index);
            r._FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>(GetFamilyTypeIndex(index), _GetReferencedFamilyType);
            r._Building = new Relation<Vim.Format.ObjectModel.Building>(GetBuildingIndex(index), _GetReferencedBuilding);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Level> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class LevelTable 
    
    public partial class PhaseTable : EntityTable_v2, IEnumerable<Phase> , IElementKindTable
    {
        
        public const string TableName = TableNames.Phase;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public PhaseTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Phase Get(int index)
        {
            if (index < 0) return null;
            var r = new Phase();
            r.Index = index;
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Phase> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class PhaseTable 
    
    public partial class RoomTable : EntityTable_v2, IEnumerable<Room> , IElementKindTable
    {
        
        public const string TableName = TableNames.Room;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public RoomTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_BaseOffset = GetDataColumnValues<Double>("double:BaseOffset") ?? Array.Empty<Double>();
            Column_LimitOffset = GetDataColumnValues<Double>("double:LimitOffset") ?? Array.Empty<Double>();
            Column_UnboundedHeight = GetDataColumnValues<Double>("double:UnboundedHeight") ?? Array.Empty<Double>();
            Column_Volume = GetDataColumnValues<Double>("double:Volume") ?? Array.Empty<Double>();
            Column_Perimeter = GetDataColumnValues<Double>("double:Perimeter") ?? Array.Empty<Double>();
            Column_Area = GetDataColumnValues<Double>("double:Area") ?? Array.Empty<Double>();
            Column_Number = GetStringColumnValues("string:Number") ?? Array.Empty<String>();
            Column_UpperLimitIndex = GetIndexColumnValues("index:Vim.Level:UpperLimit") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Double[] Column_BaseOffset { get; }
        public Double GetBaseOffset(int index, Double @default = default) => Column_BaseOffset.ElementAtOrDefault(index, @default);
        public Double[] Column_LimitOffset { get; }
        public Double GetLimitOffset(int index, Double @default = default) => Column_LimitOffset.ElementAtOrDefault(index, @default);
        public Double[] Column_UnboundedHeight { get; }
        public Double GetUnboundedHeight(int index, Double @default = default) => Column_UnboundedHeight.ElementAtOrDefault(index, @default);
        public Double[] Column_Volume { get; }
        public Double GetVolume(int index, Double @default = default) => Column_Volume.ElementAtOrDefault(index, @default);
        public Double[] Column_Perimeter { get; }
        public Double GetPerimeter(int index, Double @default = default) => Column_Perimeter.ElementAtOrDefault(index, @default);
        public Double[] Column_Area { get; }
        public Double GetArea(int index, Double @default = default) => Column_Area.ElementAtOrDefault(index, @default);
        public String[] Column_Number { get; }
        public String GetNumber(int index, String @default = "") => Column_Number.ElementAtOrDefault(index, @default);
        public int[] Column_UpperLimitIndex { get; }
        public int GetUpperLimitIndex(int index) => Column_UpperLimitIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Level GetUpperLimit(int index) => _GetReferencedUpperLimit(GetUpperLimitIndex(index));
        private Level _GetReferencedUpperLimit(int referencedIndex) => ParentTableSet.GetLevel(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Room Get(int index)
        {
            if (index < 0) return null;
            var r = new Room();
            r.Index = index;
            r.BaseOffset = GetBaseOffset(index);
            r.LimitOffset = GetLimitOffset(index);
            r.UnboundedHeight = GetUnboundedHeight(index);
            r.Volume = GetVolume(index);
            r.Perimeter = GetPerimeter(index);
            r.Area = GetArea(index);
            r.Number = GetNumber(index);
            r._UpperLimit = new Relation<Vim.Format.ObjectModel.Level>(GetUpperLimitIndex(index), _GetReferencedUpperLimit);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Room> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class RoomTable 
    
    public partial class BimDocumentTable : EntityTable_v2, IEnumerable<BimDocument> , IElementKindTable
    {
        
        public const string TableName = TableNames.BimDocument;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public BimDocumentTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Title = GetStringColumnValues("string:Title") ?? Array.Empty<String>();
            Column_IsMetric = GetDataColumnValues<Boolean>("byte:IsMetric") ?? Array.Empty<Boolean>();
            Column_Guid = GetStringColumnValues("string:Guid") ?? Array.Empty<String>();
            Column_NumSaves = GetDataColumnValues<Int32>("int:NumSaves") ?? Array.Empty<Int32>();
            Column_IsLinked = GetDataColumnValues<Boolean>("byte:IsLinked") ?? Array.Empty<Boolean>();
            Column_IsDetached = GetDataColumnValues<Boolean>("byte:IsDetached") ?? Array.Empty<Boolean>();
            Column_IsWorkshared = GetDataColumnValues<Boolean>("byte:IsWorkshared") ?? Array.Empty<Boolean>();
            Column_PathName = GetStringColumnValues("string:PathName") ?? Array.Empty<String>();
            Column_Latitude = GetDataColumnValues<Double>("double:Latitude") ?? Array.Empty<Double>();
            Column_Longitude = GetDataColumnValues<Double>("double:Longitude") ?? Array.Empty<Double>();
            Column_TimeZone = GetDataColumnValues<Double>("double:TimeZone") ?? Array.Empty<Double>();
            Column_PlaceName = GetStringColumnValues("string:PlaceName") ?? Array.Empty<String>();
            Column_WeatherStationName = GetStringColumnValues("string:WeatherStationName") ?? Array.Empty<String>();
            Column_Elevation = GetDataColumnValues<Double>("double:Elevation") ?? Array.Empty<Double>();
            Column_ProjectLocation = GetStringColumnValues("string:ProjectLocation") ?? Array.Empty<String>();
            Column_IssueDate = GetStringColumnValues("string:IssueDate") ?? Array.Empty<String>();
            Column_Status = GetStringColumnValues("string:Status") ?? Array.Empty<String>();
            Column_ClientName = GetStringColumnValues("string:ClientName") ?? Array.Empty<String>();
            Column_Address = GetStringColumnValues("string:Address") ?? Array.Empty<String>();
            Column_Name = GetStringColumnValues("string:Name") ?? Array.Empty<String>();
            Column_Number = GetStringColumnValues("string:Number") ?? Array.Empty<String>();
            Column_Author = GetStringColumnValues("string:Author") ?? Array.Empty<String>();
            Column_BuildingName = GetStringColumnValues("string:BuildingName") ?? Array.Empty<String>();
            Column_OrganizationName = GetStringColumnValues("string:OrganizationName") ?? Array.Empty<String>();
            Column_OrganizationDescription = GetStringColumnValues("string:OrganizationDescription") ?? Array.Empty<String>();
            Column_Product = GetStringColumnValues("string:Product") ?? Array.Empty<String>();
            Column_Version = GetStringColumnValues("string:Version") ?? Array.Empty<String>();
            Column_User = GetStringColumnValues("string:User") ?? Array.Empty<String>();
            Column_FileLength = GetDataColumnValues<Int64>("long:FileLength") ?? Array.Empty<Int64>();
            Column_ActiveViewIndex = GetIndexColumnValues("index:Vim.View:ActiveView") ?? Array.Empty<int>();
            Column_OwnerFamilyIndex = GetIndexColumnValues("index:Vim.Family:OwnerFamily") ?? Array.Empty<int>();
            Column_ParentIndex = GetIndexColumnValues("index:Vim.BimDocument:Parent") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public String[] Column_Title { get; }
        public String GetTitle(int index, String @default = "") => Column_Title.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsMetric { get; }
        public Boolean GetIsMetric(int index, Boolean @default = default) => Column_IsMetric.ElementAtOrDefault(index, @default);
        public String[] Column_Guid { get; }
        public String GetGuid(int index, String @default = "") => Column_Guid.ElementAtOrDefault(index, @default);
        public Int32[] Column_NumSaves { get; }
        public Int32 GetNumSaves(int index, Int32 @default = default) => Column_NumSaves.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsLinked { get; }
        public Boolean GetIsLinked(int index, Boolean @default = default) => Column_IsLinked.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsDetached { get; }
        public Boolean GetIsDetached(int index, Boolean @default = default) => Column_IsDetached.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsWorkshared { get; }
        public Boolean GetIsWorkshared(int index, Boolean @default = default) => Column_IsWorkshared.ElementAtOrDefault(index, @default);
        public String[] Column_PathName { get; }
        public String GetPathName(int index, String @default = "") => Column_PathName.ElementAtOrDefault(index, @default);
        public Double[] Column_Latitude { get; }
        public Double GetLatitude(int index, Double @default = default) => Column_Latitude.ElementAtOrDefault(index, @default);
        public Double[] Column_Longitude { get; }
        public Double GetLongitude(int index, Double @default = default) => Column_Longitude.ElementAtOrDefault(index, @default);
        public Double[] Column_TimeZone { get; }
        public Double GetTimeZone(int index, Double @default = default) => Column_TimeZone.ElementAtOrDefault(index, @default);
        public String[] Column_PlaceName { get; }
        public String GetPlaceName(int index, String @default = "") => Column_PlaceName.ElementAtOrDefault(index, @default);
        public String[] Column_WeatherStationName { get; }
        public String GetWeatherStationName(int index, String @default = "") => Column_WeatherStationName.ElementAtOrDefault(index, @default);
        public Double[] Column_Elevation { get; }
        public Double GetElevation(int index, Double @default = default) => Column_Elevation.ElementAtOrDefault(index, @default);
        public String[] Column_ProjectLocation { get; }
        public String GetProjectLocation(int index, String @default = "") => Column_ProjectLocation.ElementAtOrDefault(index, @default);
        public String[] Column_IssueDate { get; }
        public String GetIssueDate(int index, String @default = "") => Column_IssueDate.ElementAtOrDefault(index, @default);
        public String[] Column_Status { get; }
        public String GetStatus(int index, String @default = "") => Column_Status.ElementAtOrDefault(index, @default);
        public String[] Column_ClientName { get; }
        public String GetClientName(int index, String @default = "") => Column_ClientName.ElementAtOrDefault(index, @default);
        public String[] Column_Address { get; }
        public String GetAddress(int index, String @default = "") => Column_Address.ElementAtOrDefault(index, @default);
        public String[] Column_Name { get; }
        public String GetName(int index, String @default = "") => Column_Name.ElementAtOrDefault(index, @default);
        public String[] Column_Number { get; }
        public String GetNumber(int index, String @default = "") => Column_Number.ElementAtOrDefault(index, @default);
        public String[] Column_Author { get; }
        public String GetAuthor(int index, String @default = "") => Column_Author.ElementAtOrDefault(index, @default);
        public String[] Column_BuildingName { get; }
        public String GetBuildingName(int index, String @default = "") => Column_BuildingName.ElementAtOrDefault(index, @default);
        public String[] Column_OrganizationName { get; }
        public String GetOrganizationName(int index, String @default = "") => Column_OrganizationName.ElementAtOrDefault(index, @default);
        public String[] Column_OrganizationDescription { get; }
        public String GetOrganizationDescription(int index, String @default = "") => Column_OrganizationDescription.ElementAtOrDefault(index, @default);
        public String[] Column_Product { get; }
        public String GetProduct(int index, String @default = "") => Column_Product.ElementAtOrDefault(index, @default);
        public String[] Column_Version { get; }
        public String GetVersion(int index, String @default = "") => Column_Version.ElementAtOrDefault(index, @default);
        public String[] Column_User { get; }
        public String GetUser(int index, String @default = "") => Column_User.ElementAtOrDefault(index, @default);
        public Int64[] Column_FileLength { get; }
        public Int64 GetFileLength(int index, Int64 @default = default) => Column_FileLength.ElementAtOrDefault(index, @default);
        public int[] Column_ActiveViewIndex { get; }
        public int GetActiveViewIndex(int index) => Column_ActiveViewIndex.ElementAtOrDefault(index, EntityRelation.None);
        public View GetActiveView(int index) => _GetReferencedActiveView(GetActiveViewIndex(index));
        private View _GetReferencedActiveView(int referencedIndex) => ParentTableSet.GetView(referencedIndex);
        public int[] Column_OwnerFamilyIndex { get; }
        public int GetOwnerFamilyIndex(int index) => Column_OwnerFamilyIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Family GetOwnerFamily(int index) => _GetReferencedOwnerFamily(GetOwnerFamilyIndex(index));
        private Family _GetReferencedOwnerFamily(int referencedIndex) => ParentTableSet.GetFamily(referencedIndex);
        public int[] Column_ParentIndex { get; }
        public int GetParentIndex(int index) => Column_ParentIndex.ElementAtOrDefault(index, EntityRelation.None);
        public BimDocument GetParent(int index) => _GetReferencedParent(GetParentIndex(index));
        private BimDocument _GetReferencedParent(int referencedIndex) => ParentTableSet.GetBimDocument(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public BimDocument Get(int index)
        {
            if (index < 0) return null;
            var r = new BimDocument();
            r.Index = index;
            r.Title = GetTitle(index);
            r.IsMetric = GetIsMetric(index);
            r.Guid = GetGuid(index);
            r.NumSaves = GetNumSaves(index);
            r.IsLinked = GetIsLinked(index);
            r.IsDetached = GetIsDetached(index);
            r.IsWorkshared = GetIsWorkshared(index);
            r.PathName = GetPathName(index);
            r.Latitude = GetLatitude(index);
            r.Longitude = GetLongitude(index);
            r.TimeZone = GetTimeZone(index);
            r.PlaceName = GetPlaceName(index);
            r.WeatherStationName = GetWeatherStationName(index);
            r.Elevation = GetElevation(index);
            r.ProjectLocation = GetProjectLocation(index);
            r.IssueDate = GetIssueDate(index);
            r.Status = GetStatus(index);
            r.ClientName = GetClientName(index);
            r.Address = GetAddress(index);
            r.Name = GetName(index);
            r.Number = GetNumber(index);
            r.Author = GetAuthor(index);
            r.BuildingName = GetBuildingName(index);
            r.OrganizationName = GetOrganizationName(index);
            r.OrganizationDescription = GetOrganizationDescription(index);
            r.Product = GetProduct(index);
            r.Version = GetVersion(index);
            r.User = GetUser(index);
            r.FileLength = GetFileLength(index);
            r._ActiveView = new Relation<Vim.Format.ObjectModel.View>(GetActiveViewIndex(index), _GetReferencedActiveView);
            r._OwnerFamily = new Relation<Vim.Format.ObjectModel.Family>(GetOwnerFamilyIndex(index), _GetReferencedOwnerFamily);
            r._Parent = new Relation<Vim.Format.ObjectModel.BimDocument>(GetParentIndex(index), _GetReferencedParent);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<BimDocument> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class BimDocumentTable 
    
    public partial class DisplayUnitInBimDocumentTable : EntityTable_v2, IEnumerable<DisplayUnitInBimDocument>
    {
        
        public const string TableName = TableNames.DisplayUnitInBimDocument;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public DisplayUnitInBimDocumentTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_DisplayUnitIndex = GetIndexColumnValues("index:Vim.DisplayUnit:DisplayUnit") ?? Array.Empty<int>();
            Column_BimDocumentIndex = GetIndexColumnValues("index:Vim.BimDocument:BimDocument") ?? Array.Empty<int>();
        }
        
        public int[] Column_DisplayUnitIndex { get; }
        public int GetDisplayUnitIndex(int index) => Column_DisplayUnitIndex.ElementAtOrDefault(index, EntityRelation.None);
        public DisplayUnit GetDisplayUnit(int index) => _GetReferencedDisplayUnit(GetDisplayUnitIndex(index));
        private DisplayUnit _GetReferencedDisplayUnit(int referencedIndex) => ParentTableSet.GetDisplayUnit(referencedIndex);
        public int[] Column_BimDocumentIndex { get; }
        public int GetBimDocumentIndex(int index) => Column_BimDocumentIndex.ElementAtOrDefault(index, EntityRelation.None);
        public BimDocument GetBimDocument(int index) => _GetReferencedBimDocument(GetBimDocumentIndex(index));
        private BimDocument _GetReferencedBimDocument(int referencedIndex) => ParentTableSet.GetBimDocument(referencedIndex);
        // Object Getter
        public DisplayUnitInBimDocument Get(int index)
        {
            if (index < 0) return null;
            var r = new DisplayUnitInBimDocument();
            r.Index = index;
            r._DisplayUnit = new Relation<Vim.Format.ObjectModel.DisplayUnit>(GetDisplayUnitIndex(index), _GetReferencedDisplayUnit);
            r._BimDocument = new Relation<Vim.Format.ObjectModel.BimDocument>(GetBimDocumentIndex(index), _GetReferencedBimDocument);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<DisplayUnitInBimDocument> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class DisplayUnitInBimDocumentTable 
    
    public partial class PhaseOrderInBimDocumentTable : EntityTable_v2, IEnumerable<PhaseOrderInBimDocument>
    {
        
        public const string TableName = TableNames.PhaseOrderInBimDocument;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public PhaseOrderInBimDocumentTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_OrderIndex = GetDataColumnValues<Int32>("int:OrderIndex") ?? Array.Empty<Int32>();
            Column_PhaseIndex = GetIndexColumnValues("index:Vim.Phase:Phase") ?? Array.Empty<int>();
            Column_BimDocumentIndex = GetIndexColumnValues("index:Vim.BimDocument:BimDocument") ?? Array.Empty<int>();
        }
        
        public Int32[] Column_OrderIndex { get; }
        public Int32 GetOrderIndex(int index, Int32 @default = default) => Column_OrderIndex.ElementAtOrDefault(index, @default);
        public int[] Column_PhaseIndex { get; }
        public int GetPhaseIndex(int index) => Column_PhaseIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Phase GetPhase(int index) => _GetReferencedPhase(GetPhaseIndex(index));
        private Phase _GetReferencedPhase(int referencedIndex) => ParentTableSet.GetPhase(referencedIndex);
        public int[] Column_BimDocumentIndex { get; }
        public int GetBimDocumentIndex(int index) => Column_BimDocumentIndex.ElementAtOrDefault(index, EntityRelation.None);
        public BimDocument GetBimDocument(int index) => _GetReferencedBimDocument(GetBimDocumentIndex(index));
        private BimDocument _GetReferencedBimDocument(int referencedIndex) => ParentTableSet.GetBimDocument(referencedIndex);
        // Object Getter
        public PhaseOrderInBimDocument Get(int index)
        {
            if (index < 0) return null;
            var r = new PhaseOrderInBimDocument();
            r.Index = index;
            r.OrderIndex = GetOrderIndex(index);
            r._Phase = new Relation<Vim.Format.ObjectModel.Phase>(GetPhaseIndex(index), _GetReferencedPhase);
            r._BimDocument = new Relation<Vim.Format.ObjectModel.BimDocument>(GetBimDocumentIndex(index), _GetReferencedBimDocument);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<PhaseOrderInBimDocument> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class PhaseOrderInBimDocumentTable 
    
    public partial class CategoryTable : EntityTable_v2, IEnumerable<Category>
    {
        
        public const string TableName = TableNames.Category;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public CategoryTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Name = GetStringColumnValues("string:Name") ?? Array.Empty<String>();
            Column_Id = (GetDataColumnValues<Int64>("long:Id") ?? GetDataColumnValues<Int32>("int:Id")?.Select(v => (Int64) v).ToArray()) ?? Array.Empty<Int64>();
            Column_CategoryType = GetStringColumnValues("string:CategoryType") ?? Array.Empty<String>();
            Column_LineColor_X = GetDataColumnValues<Double>("double:LineColor.X") ?? Array.Empty<Double>();
            Column_LineColor_Y = GetDataColumnValues<Double>("double:LineColor.Y") ?? Array.Empty<Double>();
            Column_LineColor_Z = GetDataColumnValues<Double>("double:LineColor.Z") ?? Array.Empty<Double>();
            Column_BuiltInCategory = GetStringColumnValues("string:BuiltInCategory") ?? Array.Empty<String>();
            Column_ParentIndex = GetIndexColumnValues("index:Vim.Category:Parent") ?? Array.Empty<int>();
            Column_MaterialIndex = GetIndexColumnValues("index:Vim.Material:Material") ?? Array.Empty<int>();
        }
        
        public String[] Column_Name { get; }
        public String GetName(int index, String @default = "") => Column_Name.ElementAtOrDefault(index, @default);
        public Int64[] Column_Id { get; }
        public Int64 GetId(int index, Int64 @default = default) => Column_Id.ElementAtOrDefault(index, @default);
        public String[] Column_CategoryType { get; }
        public String GetCategoryType(int index, String @default = "") => Column_CategoryType.ElementAtOrDefault(index, @default);
        public Double[] Column_LineColor_X { get; }
        public Double GetLineColor_X(int index, Double @default = default) => Column_LineColor_X.ElementAtOrDefault(index, @default);
        public Double[] Column_LineColor_Y { get; }
        public Double GetLineColor_Y(int index, Double @default = default) => Column_LineColor_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_LineColor_Z { get; }
        public Double GetLineColor_Z(int index, Double @default = default) => Column_LineColor_Z.ElementAtOrDefault(index, @default);
        public String[] Column_BuiltInCategory { get; }
        public String GetBuiltInCategory(int index, String @default = "") => Column_BuiltInCategory.ElementAtOrDefault(index, @default);
        public int[] Column_ParentIndex { get; }
        public int GetParentIndex(int index) => Column_ParentIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Category GetParent(int index) => _GetReferencedParent(GetParentIndex(index));
        private Category _GetReferencedParent(int referencedIndex) => ParentTableSet.GetCategory(referencedIndex);
        public int[] Column_MaterialIndex { get; }
        public int GetMaterialIndex(int index) => Column_MaterialIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Material GetMaterial(int index) => _GetReferencedMaterial(GetMaterialIndex(index));
        private Material _GetReferencedMaterial(int referencedIndex) => ParentTableSet.GetMaterial(referencedIndex);
        // Object Getter
        public Category Get(int index)
        {
            if (index < 0) return null;
            var r = new Category();
            r.Index = index;
            r.Name = GetName(index);
            r.Id = GetId(index);
            r.CategoryType = GetCategoryType(index);
            r.LineColor_X = GetLineColor_X(index);
            r.LineColor_Y = GetLineColor_Y(index);
            r.LineColor_Z = GetLineColor_Z(index);
            r.BuiltInCategory = GetBuiltInCategory(index);
            r._Parent = new Relation<Vim.Format.ObjectModel.Category>(GetParentIndex(index), _GetReferencedParent);
            r._Material = new Relation<Vim.Format.ObjectModel.Material>(GetMaterialIndex(index), _GetReferencedMaterial);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Category> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class CategoryTable 
    
    public partial class FamilyTable : EntityTable_v2, IEnumerable<Family> , IElementKindTable
    {
        
        public const string TableName = TableNames.Family;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public FamilyTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_StructuralMaterialType = GetStringColumnValues("string:StructuralMaterialType") ?? Array.Empty<String>();
            Column_StructuralSectionShape = GetStringColumnValues("string:StructuralSectionShape") ?? Array.Empty<String>();
            Column_IsSystemFamily = GetDataColumnValues<Boolean>("byte:IsSystemFamily") ?? Array.Empty<Boolean>();
            Column_IsInPlace = GetDataColumnValues<Boolean>("byte:IsInPlace") ?? Array.Empty<Boolean>();
            Column_FamilyCategoryIndex = GetIndexColumnValues("index:Vim.Category:FamilyCategory") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public String[] Column_StructuralMaterialType { get; }
        public String GetStructuralMaterialType(int index, String @default = "") => Column_StructuralMaterialType.ElementAtOrDefault(index, @default);
        public String[] Column_StructuralSectionShape { get; }
        public String GetStructuralSectionShape(int index, String @default = "") => Column_StructuralSectionShape.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsSystemFamily { get; }
        public Boolean GetIsSystemFamily(int index, Boolean @default = default) => Column_IsSystemFamily.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsInPlace { get; }
        public Boolean GetIsInPlace(int index, Boolean @default = default) => Column_IsInPlace.ElementAtOrDefault(index, @default);
        public int[] Column_FamilyCategoryIndex { get; }
        public int GetFamilyCategoryIndex(int index) => Column_FamilyCategoryIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Category GetFamilyCategory(int index) => _GetReferencedFamilyCategory(GetFamilyCategoryIndex(index));
        private Category _GetReferencedFamilyCategory(int referencedIndex) => ParentTableSet.GetCategory(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Family Get(int index)
        {
            if (index < 0) return null;
            var r = new Family();
            r.Index = index;
            r.StructuralMaterialType = GetStructuralMaterialType(index);
            r.StructuralSectionShape = GetStructuralSectionShape(index);
            r.IsSystemFamily = GetIsSystemFamily(index);
            r.IsInPlace = GetIsInPlace(index);
            r._FamilyCategory = new Relation<Vim.Format.ObjectModel.Category>(GetFamilyCategoryIndex(index), _GetReferencedFamilyCategory);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Family> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class FamilyTable 
    
    public partial class FamilyTypeTable : EntityTable_v2, IEnumerable<FamilyType> , IElementKindTable
    {
        
        public const string TableName = TableNames.FamilyType;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public FamilyTypeTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_IsSystemFamilyType = GetDataColumnValues<Boolean>("byte:IsSystemFamilyType") ?? Array.Empty<Boolean>();
            Column_FamilyIndex = GetIndexColumnValues("index:Vim.Family:Family") ?? Array.Empty<int>();
            Column_CompoundStructureIndex = GetIndexColumnValues("index:Vim.CompoundStructure:CompoundStructure") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Boolean[] Column_IsSystemFamilyType { get; }
        public Boolean GetIsSystemFamilyType(int index, Boolean @default = default) => Column_IsSystemFamilyType.ElementAtOrDefault(index, @default);
        public int[] Column_FamilyIndex { get; }
        public int GetFamilyIndex(int index) => Column_FamilyIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Family GetFamily(int index) => _GetReferencedFamily(GetFamilyIndex(index));
        private Family _GetReferencedFamily(int referencedIndex) => ParentTableSet.GetFamily(referencedIndex);
        public int[] Column_CompoundStructureIndex { get; }
        public int GetCompoundStructureIndex(int index) => Column_CompoundStructureIndex.ElementAtOrDefault(index, EntityRelation.None);
        public CompoundStructure GetCompoundStructure(int index) => _GetReferencedCompoundStructure(GetCompoundStructureIndex(index));
        private CompoundStructure _GetReferencedCompoundStructure(int referencedIndex) => ParentTableSet.GetCompoundStructure(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public FamilyType Get(int index)
        {
            if (index < 0) return null;
            var r = new FamilyType();
            r.Index = index;
            r.IsSystemFamilyType = GetIsSystemFamilyType(index);
            r._Family = new Relation<Vim.Format.ObjectModel.Family>(GetFamilyIndex(index), _GetReferencedFamily);
            r._CompoundStructure = new Relation<Vim.Format.ObjectModel.CompoundStructure>(GetCompoundStructureIndex(index), _GetReferencedCompoundStructure);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<FamilyType> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class FamilyTypeTable 
    
    public partial class FamilyInstanceTable : EntityTable_v2, IEnumerable<FamilyInstance> , IElementKindTable
    {
        
        public const string TableName = TableNames.FamilyInstance;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public FamilyInstanceTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_FacingFlipped = GetDataColumnValues<Boolean>("byte:FacingFlipped") ?? Array.Empty<Boolean>();
            Column_FacingOrientation_X = GetDataColumnValues<Single>("float:FacingOrientation.X") ?? Array.Empty<Single>();
            Column_FacingOrientation_Y = GetDataColumnValues<Single>("float:FacingOrientation.Y") ?? Array.Empty<Single>();
            Column_FacingOrientation_Z = GetDataColumnValues<Single>("float:FacingOrientation.Z") ?? Array.Empty<Single>();
            Column_HandFlipped = GetDataColumnValues<Boolean>("byte:HandFlipped") ?? Array.Empty<Boolean>();
            Column_Mirrored = GetDataColumnValues<Boolean>("byte:Mirrored") ?? Array.Empty<Boolean>();
            Column_HasModifiedGeometry = GetDataColumnValues<Boolean>("byte:HasModifiedGeometry") ?? Array.Empty<Boolean>();
            Column_Scale = GetDataColumnValues<Single>("float:Scale") ?? Array.Empty<Single>();
            Column_BasisX_X = GetDataColumnValues<Single>("float:BasisX.X") ?? Array.Empty<Single>();
            Column_BasisX_Y = GetDataColumnValues<Single>("float:BasisX.Y") ?? Array.Empty<Single>();
            Column_BasisX_Z = GetDataColumnValues<Single>("float:BasisX.Z") ?? Array.Empty<Single>();
            Column_BasisY_X = GetDataColumnValues<Single>("float:BasisY.X") ?? Array.Empty<Single>();
            Column_BasisY_Y = GetDataColumnValues<Single>("float:BasisY.Y") ?? Array.Empty<Single>();
            Column_BasisY_Z = GetDataColumnValues<Single>("float:BasisY.Z") ?? Array.Empty<Single>();
            Column_BasisZ_X = GetDataColumnValues<Single>("float:BasisZ.X") ?? Array.Empty<Single>();
            Column_BasisZ_Y = GetDataColumnValues<Single>("float:BasisZ.Y") ?? Array.Empty<Single>();
            Column_BasisZ_Z = GetDataColumnValues<Single>("float:BasisZ.Z") ?? Array.Empty<Single>();
            Column_Translation_X = GetDataColumnValues<Single>("float:Translation.X") ?? Array.Empty<Single>();
            Column_Translation_Y = GetDataColumnValues<Single>("float:Translation.Y") ?? Array.Empty<Single>();
            Column_Translation_Z = GetDataColumnValues<Single>("float:Translation.Z") ?? Array.Empty<Single>();
            Column_HandOrientation_X = GetDataColumnValues<Single>("float:HandOrientation.X") ?? Array.Empty<Single>();
            Column_HandOrientation_Y = GetDataColumnValues<Single>("float:HandOrientation.Y") ?? Array.Empty<Single>();
            Column_HandOrientation_Z = GetDataColumnValues<Single>("float:HandOrientation.Z") ?? Array.Empty<Single>();
            Column_FamilyTypeIndex = GetIndexColumnValues("index:Vim.FamilyType:FamilyType") ?? Array.Empty<int>();
            Column_HostIndex = GetIndexColumnValues("index:Vim.Element:Host") ?? Array.Empty<int>();
            Column_FromRoomIndex = GetIndexColumnValues("index:Vim.Room:FromRoom") ?? Array.Empty<int>();
            Column_ToRoomIndex = GetIndexColumnValues("index:Vim.Room:ToRoom") ?? Array.Empty<int>();
            Column_SuperComponentIndex = GetIndexColumnValues("index:Vim.Element:SuperComponent") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Boolean[] Column_FacingFlipped { get; }
        public Boolean GetFacingFlipped(int index, Boolean @default = default) => Column_FacingFlipped.ElementAtOrDefault(index, @default);
        public Single[] Column_FacingOrientation_X { get; }
        public Single GetFacingOrientation_X(int index, Single @default = default) => Column_FacingOrientation_X.ElementAtOrDefault(index, @default);
        public Single[] Column_FacingOrientation_Y { get; }
        public Single GetFacingOrientation_Y(int index, Single @default = default) => Column_FacingOrientation_Y.ElementAtOrDefault(index, @default);
        public Single[] Column_FacingOrientation_Z { get; }
        public Single GetFacingOrientation_Z(int index, Single @default = default) => Column_FacingOrientation_Z.ElementAtOrDefault(index, @default);
        public Boolean[] Column_HandFlipped { get; }
        public Boolean GetHandFlipped(int index, Boolean @default = default) => Column_HandFlipped.ElementAtOrDefault(index, @default);
        public Boolean[] Column_Mirrored { get; }
        public Boolean GetMirrored(int index, Boolean @default = default) => Column_Mirrored.ElementAtOrDefault(index, @default);
        public Boolean[] Column_HasModifiedGeometry { get; }
        public Boolean GetHasModifiedGeometry(int index, Boolean @default = default) => Column_HasModifiedGeometry.ElementAtOrDefault(index, @default);
        public Single[] Column_Scale { get; }
        public Single GetScale(int index, Single @default = default) => Column_Scale.ElementAtOrDefault(index, @default);
        public Single[] Column_BasisX_X { get; }
        public Single GetBasisX_X(int index, Single @default = default) => Column_BasisX_X.ElementAtOrDefault(index, @default);
        public Single[] Column_BasisX_Y { get; }
        public Single GetBasisX_Y(int index, Single @default = default) => Column_BasisX_Y.ElementAtOrDefault(index, @default);
        public Single[] Column_BasisX_Z { get; }
        public Single GetBasisX_Z(int index, Single @default = default) => Column_BasisX_Z.ElementAtOrDefault(index, @default);
        public Single[] Column_BasisY_X { get; }
        public Single GetBasisY_X(int index, Single @default = default) => Column_BasisY_X.ElementAtOrDefault(index, @default);
        public Single[] Column_BasisY_Y { get; }
        public Single GetBasisY_Y(int index, Single @default = default) => Column_BasisY_Y.ElementAtOrDefault(index, @default);
        public Single[] Column_BasisY_Z { get; }
        public Single GetBasisY_Z(int index, Single @default = default) => Column_BasisY_Z.ElementAtOrDefault(index, @default);
        public Single[] Column_BasisZ_X { get; }
        public Single GetBasisZ_X(int index, Single @default = default) => Column_BasisZ_X.ElementAtOrDefault(index, @default);
        public Single[] Column_BasisZ_Y { get; }
        public Single GetBasisZ_Y(int index, Single @default = default) => Column_BasisZ_Y.ElementAtOrDefault(index, @default);
        public Single[] Column_BasisZ_Z { get; }
        public Single GetBasisZ_Z(int index, Single @default = default) => Column_BasisZ_Z.ElementAtOrDefault(index, @default);
        public Single[] Column_Translation_X { get; }
        public Single GetTranslation_X(int index, Single @default = default) => Column_Translation_X.ElementAtOrDefault(index, @default);
        public Single[] Column_Translation_Y { get; }
        public Single GetTranslation_Y(int index, Single @default = default) => Column_Translation_Y.ElementAtOrDefault(index, @default);
        public Single[] Column_Translation_Z { get; }
        public Single GetTranslation_Z(int index, Single @default = default) => Column_Translation_Z.ElementAtOrDefault(index, @default);
        public Single[] Column_HandOrientation_X { get; }
        public Single GetHandOrientation_X(int index, Single @default = default) => Column_HandOrientation_X.ElementAtOrDefault(index, @default);
        public Single[] Column_HandOrientation_Y { get; }
        public Single GetHandOrientation_Y(int index, Single @default = default) => Column_HandOrientation_Y.ElementAtOrDefault(index, @default);
        public Single[] Column_HandOrientation_Z { get; }
        public Single GetHandOrientation_Z(int index, Single @default = default) => Column_HandOrientation_Z.ElementAtOrDefault(index, @default);
        public int[] Column_FamilyTypeIndex { get; }
        public int GetFamilyTypeIndex(int index) => Column_FamilyTypeIndex.ElementAtOrDefault(index, EntityRelation.None);
        public FamilyType GetFamilyType(int index) => _GetReferencedFamilyType(GetFamilyTypeIndex(index));
        private FamilyType _GetReferencedFamilyType(int referencedIndex) => ParentTableSet.GetFamilyType(referencedIndex);
        public int[] Column_HostIndex { get; }
        public int GetHostIndex(int index) => Column_HostIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetHost(int index) => _GetReferencedHost(GetHostIndex(index));
        private Element _GetReferencedHost(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        public int[] Column_FromRoomIndex { get; }
        public int GetFromRoomIndex(int index) => Column_FromRoomIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Room GetFromRoom(int index) => _GetReferencedFromRoom(GetFromRoomIndex(index));
        private Room _GetReferencedFromRoom(int referencedIndex) => ParentTableSet.GetRoom(referencedIndex);
        public int[] Column_ToRoomIndex { get; }
        public int GetToRoomIndex(int index) => Column_ToRoomIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Room GetToRoom(int index) => _GetReferencedToRoom(GetToRoomIndex(index));
        private Room _GetReferencedToRoom(int referencedIndex) => ParentTableSet.GetRoom(referencedIndex);
        public int[] Column_SuperComponentIndex { get; }
        public int GetSuperComponentIndex(int index) => Column_SuperComponentIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetSuperComponent(int index) => _GetReferencedSuperComponent(GetSuperComponentIndex(index));
        private Element _GetReferencedSuperComponent(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public FamilyInstance Get(int index)
        {
            if (index < 0) return null;
            var r = new FamilyInstance();
            r.Index = index;
            r.FacingFlipped = GetFacingFlipped(index);
            r.FacingOrientation_X = GetFacingOrientation_X(index);
            r.FacingOrientation_Y = GetFacingOrientation_Y(index);
            r.FacingOrientation_Z = GetFacingOrientation_Z(index);
            r.HandFlipped = GetHandFlipped(index);
            r.Mirrored = GetMirrored(index);
            r.HasModifiedGeometry = GetHasModifiedGeometry(index);
            r.Scale = GetScale(index);
            r.BasisX_X = GetBasisX_X(index);
            r.BasisX_Y = GetBasisX_Y(index);
            r.BasisX_Z = GetBasisX_Z(index);
            r.BasisY_X = GetBasisY_X(index);
            r.BasisY_Y = GetBasisY_Y(index);
            r.BasisY_Z = GetBasisY_Z(index);
            r.BasisZ_X = GetBasisZ_X(index);
            r.BasisZ_Y = GetBasisZ_Y(index);
            r.BasisZ_Z = GetBasisZ_Z(index);
            r.Translation_X = GetTranslation_X(index);
            r.Translation_Y = GetTranslation_Y(index);
            r.Translation_Z = GetTranslation_Z(index);
            r.HandOrientation_X = GetHandOrientation_X(index);
            r.HandOrientation_Y = GetHandOrientation_Y(index);
            r.HandOrientation_Z = GetHandOrientation_Z(index);
            r._FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>(GetFamilyTypeIndex(index), _GetReferencedFamilyType);
            r._Host = new Relation<Vim.Format.ObjectModel.Element>(GetHostIndex(index), _GetReferencedHost);
            r._FromRoom = new Relation<Vim.Format.ObjectModel.Room>(GetFromRoomIndex(index), _GetReferencedFromRoom);
            r._ToRoom = new Relation<Vim.Format.ObjectModel.Room>(GetToRoomIndex(index), _GetReferencedToRoom);
            r._SuperComponent = new Relation<Vim.Format.ObjectModel.Element>(GetSuperComponentIndex(index), _GetReferencedSuperComponent);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<FamilyInstance> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class FamilyInstanceTable 
    
    public partial class ViewTable : EntityTable_v2, IEnumerable<View> , IElementKindTable
    {
        
        public const string TableName = TableNames.View;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public ViewTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Title = GetStringColumnValues("string:Title") ?? Array.Empty<String>();
            Column_ViewType = GetStringColumnValues("string:ViewType") ?? Array.Empty<String>();
            Column_Up_X = GetDataColumnValues<Double>("double:Up.X") ?? Array.Empty<Double>();
            Column_Up_Y = GetDataColumnValues<Double>("double:Up.Y") ?? Array.Empty<Double>();
            Column_Up_Z = GetDataColumnValues<Double>("double:Up.Z") ?? Array.Empty<Double>();
            Column_Right_X = GetDataColumnValues<Double>("double:Right.X") ?? Array.Empty<Double>();
            Column_Right_Y = GetDataColumnValues<Double>("double:Right.Y") ?? Array.Empty<Double>();
            Column_Right_Z = GetDataColumnValues<Double>("double:Right.Z") ?? Array.Empty<Double>();
            Column_Origin_X = GetDataColumnValues<Double>("double:Origin.X") ?? Array.Empty<Double>();
            Column_Origin_Y = GetDataColumnValues<Double>("double:Origin.Y") ?? Array.Empty<Double>();
            Column_Origin_Z = GetDataColumnValues<Double>("double:Origin.Z") ?? Array.Empty<Double>();
            Column_ViewDirection_X = GetDataColumnValues<Double>("double:ViewDirection.X") ?? Array.Empty<Double>();
            Column_ViewDirection_Y = GetDataColumnValues<Double>("double:ViewDirection.Y") ?? Array.Empty<Double>();
            Column_ViewDirection_Z = GetDataColumnValues<Double>("double:ViewDirection.Z") ?? Array.Empty<Double>();
            Column_ViewPosition_X = GetDataColumnValues<Double>("double:ViewPosition.X") ?? Array.Empty<Double>();
            Column_ViewPosition_Y = GetDataColumnValues<Double>("double:ViewPosition.Y") ?? Array.Empty<Double>();
            Column_ViewPosition_Z = GetDataColumnValues<Double>("double:ViewPosition.Z") ?? Array.Empty<Double>();
            Column_Scale = GetDataColumnValues<Double>("double:Scale") ?? Array.Empty<Double>();
            Column_Outline_Min_X = GetDataColumnValues<Double>("double:Outline.Min.X") ?? Array.Empty<Double>();
            Column_Outline_Min_Y = GetDataColumnValues<Double>("double:Outline.Min.Y") ?? Array.Empty<Double>();
            Column_Outline_Max_X = GetDataColumnValues<Double>("double:Outline.Max.X") ?? Array.Empty<Double>();
            Column_Outline_Max_Y = GetDataColumnValues<Double>("double:Outline.Max.Y") ?? Array.Empty<Double>();
            Column_DetailLevel = GetDataColumnValues<Int32>("int:DetailLevel") ?? Array.Empty<Int32>();
            Column_CameraIndex = GetIndexColumnValues("index:Vim.Camera:Camera") ?? Array.Empty<int>();
            Column_FamilyTypeIndex = GetIndexColumnValues("index:Vim.FamilyType:FamilyType") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public String[] Column_Title { get; }
        public String GetTitle(int index, String @default = "") => Column_Title.ElementAtOrDefault(index, @default);
        public String[] Column_ViewType { get; }
        public String GetViewType(int index, String @default = "") => Column_ViewType.ElementAtOrDefault(index, @default);
        public Double[] Column_Up_X { get; }
        public Double GetUp_X(int index, Double @default = default) => Column_Up_X.ElementAtOrDefault(index, @default);
        public Double[] Column_Up_Y { get; }
        public Double GetUp_Y(int index, Double @default = default) => Column_Up_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_Up_Z { get; }
        public Double GetUp_Z(int index, Double @default = default) => Column_Up_Z.ElementAtOrDefault(index, @default);
        public Double[] Column_Right_X { get; }
        public Double GetRight_X(int index, Double @default = default) => Column_Right_X.ElementAtOrDefault(index, @default);
        public Double[] Column_Right_Y { get; }
        public Double GetRight_Y(int index, Double @default = default) => Column_Right_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_Right_Z { get; }
        public Double GetRight_Z(int index, Double @default = default) => Column_Right_Z.ElementAtOrDefault(index, @default);
        public Double[] Column_Origin_X { get; }
        public Double GetOrigin_X(int index, Double @default = default) => Column_Origin_X.ElementAtOrDefault(index, @default);
        public Double[] Column_Origin_Y { get; }
        public Double GetOrigin_Y(int index, Double @default = default) => Column_Origin_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_Origin_Z { get; }
        public Double GetOrigin_Z(int index, Double @default = default) => Column_Origin_Z.ElementAtOrDefault(index, @default);
        public Double[] Column_ViewDirection_X { get; }
        public Double GetViewDirection_X(int index, Double @default = default) => Column_ViewDirection_X.ElementAtOrDefault(index, @default);
        public Double[] Column_ViewDirection_Y { get; }
        public Double GetViewDirection_Y(int index, Double @default = default) => Column_ViewDirection_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_ViewDirection_Z { get; }
        public Double GetViewDirection_Z(int index, Double @default = default) => Column_ViewDirection_Z.ElementAtOrDefault(index, @default);
        public Double[] Column_ViewPosition_X { get; }
        public Double GetViewPosition_X(int index, Double @default = default) => Column_ViewPosition_X.ElementAtOrDefault(index, @default);
        public Double[] Column_ViewPosition_Y { get; }
        public Double GetViewPosition_Y(int index, Double @default = default) => Column_ViewPosition_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_ViewPosition_Z { get; }
        public Double GetViewPosition_Z(int index, Double @default = default) => Column_ViewPosition_Z.ElementAtOrDefault(index, @default);
        public Double[] Column_Scale { get; }
        public Double GetScale(int index, Double @default = default) => Column_Scale.ElementAtOrDefault(index, @default);
        public Double[] Column_Outline_Min_X { get; }
        public Double GetOutline_Min_X(int index, Double @default = default) => Column_Outline_Min_X.ElementAtOrDefault(index, @default);
        public Double[] Column_Outline_Min_Y { get; }
        public Double GetOutline_Min_Y(int index, Double @default = default) => Column_Outline_Min_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_Outline_Max_X { get; }
        public Double GetOutline_Max_X(int index, Double @default = default) => Column_Outline_Max_X.ElementAtOrDefault(index, @default);
        public Double[] Column_Outline_Max_Y { get; }
        public Double GetOutline_Max_Y(int index, Double @default = default) => Column_Outline_Max_Y.ElementAtOrDefault(index, @default);
        public Int32[] Column_DetailLevel { get; }
        public Int32 GetDetailLevel(int index, Int32 @default = default) => Column_DetailLevel.ElementAtOrDefault(index, @default);
        public int[] Column_CameraIndex { get; }
        public int GetCameraIndex(int index) => Column_CameraIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Camera GetCamera(int index) => _GetReferencedCamera(GetCameraIndex(index));
        private Camera _GetReferencedCamera(int referencedIndex) => ParentTableSet.GetCamera(referencedIndex);
        public int[] Column_FamilyTypeIndex { get; }
        public int GetFamilyTypeIndex(int index) => Column_FamilyTypeIndex.ElementAtOrDefault(index, EntityRelation.None);
        public FamilyType GetFamilyType(int index) => _GetReferencedFamilyType(GetFamilyTypeIndex(index));
        private FamilyType _GetReferencedFamilyType(int referencedIndex) => ParentTableSet.GetFamilyType(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public View Get(int index)
        {
            if (index < 0) return null;
            var r = new View();
            r.Index = index;
            r.Title = GetTitle(index);
            r.ViewType = GetViewType(index);
            r.Up_X = GetUp_X(index);
            r.Up_Y = GetUp_Y(index);
            r.Up_Z = GetUp_Z(index);
            r.Right_X = GetRight_X(index);
            r.Right_Y = GetRight_Y(index);
            r.Right_Z = GetRight_Z(index);
            r.Origin_X = GetOrigin_X(index);
            r.Origin_Y = GetOrigin_Y(index);
            r.Origin_Z = GetOrigin_Z(index);
            r.ViewDirection_X = GetViewDirection_X(index);
            r.ViewDirection_Y = GetViewDirection_Y(index);
            r.ViewDirection_Z = GetViewDirection_Z(index);
            r.ViewPosition_X = GetViewPosition_X(index);
            r.ViewPosition_Y = GetViewPosition_Y(index);
            r.ViewPosition_Z = GetViewPosition_Z(index);
            r.Scale = GetScale(index);
            r.Outline_Min_X = GetOutline_Min_X(index);
            r.Outline_Min_Y = GetOutline_Min_Y(index);
            r.Outline_Max_X = GetOutline_Max_X(index);
            r.Outline_Max_Y = GetOutline_Max_Y(index);
            r.DetailLevel = GetDetailLevel(index);
            r._Camera = new Relation<Vim.Format.ObjectModel.Camera>(GetCameraIndex(index), _GetReferencedCamera);
            r._FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>(GetFamilyTypeIndex(index), _GetReferencedFamilyType);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<View> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ViewTable 
    
    public partial class ElementInViewTable : EntityTable_v2, IEnumerable<ElementInView>
    {
        
        public const string TableName = TableNames.ElementInView;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public ElementInViewTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_ViewIndex = GetIndexColumnValues("index:Vim.View:View") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public int[] Column_ViewIndex { get; }
        public int GetViewIndex(int index) => Column_ViewIndex.ElementAtOrDefault(index, EntityRelation.None);
        public View GetView(int index) => _GetReferencedView(GetViewIndex(index));
        private View _GetReferencedView(int referencedIndex) => ParentTableSet.GetView(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public ElementInView Get(int index)
        {
            if (index < 0) return null;
            var r = new ElementInView();
            r.Index = index;
            r._View = new Relation<Vim.Format.ObjectModel.View>(GetViewIndex(index), _GetReferencedView);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<ElementInView> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ElementInViewTable 
    
    public partial class ShapeInViewTable : EntityTable_v2, IEnumerable<ShapeInView>
    {
        
        public const string TableName = TableNames.ShapeInView;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public ShapeInViewTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_ShapeIndex = GetIndexColumnValues("index:Vim.Shape:Shape") ?? Array.Empty<int>();
            Column_ViewIndex = GetIndexColumnValues("index:Vim.View:View") ?? Array.Empty<int>();
        }
        
        public int[] Column_ShapeIndex { get; }
        public int GetShapeIndex(int index) => Column_ShapeIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Shape GetShape(int index) => _GetReferencedShape(GetShapeIndex(index));
        private Shape _GetReferencedShape(int referencedIndex) => ParentTableSet.GetShape(referencedIndex);
        public int[] Column_ViewIndex { get; }
        public int GetViewIndex(int index) => Column_ViewIndex.ElementAtOrDefault(index, EntityRelation.None);
        public View GetView(int index) => _GetReferencedView(GetViewIndex(index));
        private View _GetReferencedView(int referencedIndex) => ParentTableSet.GetView(referencedIndex);
        // Object Getter
        public ShapeInView Get(int index)
        {
            if (index < 0) return null;
            var r = new ShapeInView();
            r.Index = index;
            r._Shape = new Relation<Vim.Format.ObjectModel.Shape>(GetShapeIndex(index), _GetReferencedShape);
            r._View = new Relation<Vim.Format.ObjectModel.View>(GetViewIndex(index), _GetReferencedView);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<ShapeInView> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ShapeInViewTable 
    
    public partial class AssetInViewTable : EntityTable_v2, IEnumerable<AssetInView>
    {
        
        public const string TableName = TableNames.AssetInView;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public AssetInViewTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_AssetIndex = GetIndexColumnValues("index:Vim.Asset:Asset") ?? Array.Empty<int>();
            Column_ViewIndex = GetIndexColumnValues("index:Vim.View:View") ?? Array.Empty<int>();
        }
        
        public int[] Column_AssetIndex { get; }
        public int GetAssetIndex(int index) => Column_AssetIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Asset GetAsset(int index) => _GetReferencedAsset(GetAssetIndex(index));
        private Asset _GetReferencedAsset(int referencedIndex) => ParentTableSet.GetAsset(referencedIndex);
        public int[] Column_ViewIndex { get; }
        public int GetViewIndex(int index) => Column_ViewIndex.ElementAtOrDefault(index, EntityRelation.None);
        public View GetView(int index) => _GetReferencedView(GetViewIndex(index));
        private View _GetReferencedView(int referencedIndex) => ParentTableSet.GetView(referencedIndex);
        // Object Getter
        public AssetInView Get(int index)
        {
            if (index < 0) return null;
            var r = new AssetInView();
            r.Index = index;
            r._Asset = new Relation<Vim.Format.ObjectModel.Asset>(GetAssetIndex(index), _GetReferencedAsset);
            r._View = new Relation<Vim.Format.ObjectModel.View>(GetViewIndex(index), _GetReferencedView);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<AssetInView> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class AssetInViewTable 
    
    public partial class AssetInViewSheetTable : EntityTable_v2, IEnumerable<AssetInViewSheet>
    {
        
        public const string TableName = TableNames.AssetInViewSheet;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public AssetInViewSheetTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_AssetIndex = GetIndexColumnValues("index:Vim.Asset:Asset") ?? Array.Empty<int>();
            Column_ViewSheetIndex = GetIndexColumnValues("index:Vim.ViewSheet:ViewSheet") ?? Array.Empty<int>();
        }
        
        public int[] Column_AssetIndex { get; }
        public int GetAssetIndex(int index) => Column_AssetIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Asset GetAsset(int index) => _GetReferencedAsset(GetAssetIndex(index));
        private Asset _GetReferencedAsset(int referencedIndex) => ParentTableSet.GetAsset(referencedIndex);
        public int[] Column_ViewSheetIndex { get; }
        public int GetViewSheetIndex(int index) => Column_ViewSheetIndex.ElementAtOrDefault(index, EntityRelation.None);
        public ViewSheet GetViewSheet(int index) => _GetReferencedViewSheet(GetViewSheetIndex(index));
        private ViewSheet _GetReferencedViewSheet(int referencedIndex) => ParentTableSet.GetViewSheet(referencedIndex);
        // Object Getter
        public AssetInViewSheet Get(int index)
        {
            if (index < 0) return null;
            var r = new AssetInViewSheet();
            r.Index = index;
            r._Asset = new Relation<Vim.Format.ObjectModel.Asset>(GetAssetIndex(index), _GetReferencedAsset);
            r._ViewSheet = new Relation<Vim.Format.ObjectModel.ViewSheet>(GetViewSheetIndex(index), _GetReferencedViewSheet);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<AssetInViewSheet> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class AssetInViewSheetTable 
    
    public partial class LevelInViewTable : EntityTable_v2, IEnumerable<LevelInView>
    {
        
        public const string TableName = TableNames.LevelInView;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public LevelInViewTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Extents_Min_X = GetDataColumnValues<Double>("double:Extents.Min.X") ?? Array.Empty<Double>();
            Column_Extents_Min_Y = GetDataColumnValues<Double>("double:Extents.Min.Y") ?? Array.Empty<Double>();
            Column_Extents_Min_Z = GetDataColumnValues<Double>("double:Extents.Min.Z") ?? Array.Empty<Double>();
            Column_Extents_Max_X = GetDataColumnValues<Double>("double:Extents.Max.X") ?? Array.Empty<Double>();
            Column_Extents_Max_Y = GetDataColumnValues<Double>("double:Extents.Max.Y") ?? Array.Empty<Double>();
            Column_Extents_Max_Z = GetDataColumnValues<Double>("double:Extents.Max.Z") ?? Array.Empty<Double>();
            Column_LevelIndex = GetIndexColumnValues("index:Vim.Level:Level") ?? Array.Empty<int>();
            Column_ViewIndex = GetIndexColumnValues("index:Vim.View:View") ?? Array.Empty<int>();
        }
        
        public Double[] Column_Extents_Min_X { get; }
        public Double GetExtents_Min_X(int index, Double @default = default) => Column_Extents_Min_X.ElementAtOrDefault(index, @default);
        public Double[] Column_Extents_Min_Y { get; }
        public Double GetExtents_Min_Y(int index, Double @default = default) => Column_Extents_Min_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_Extents_Min_Z { get; }
        public Double GetExtents_Min_Z(int index, Double @default = default) => Column_Extents_Min_Z.ElementAtOrDefault(index, @default);
        public Double[] Column_Extents_Max_X { get; }
        public Double GetExtents_Max_X(int index, Double @default = default) => Column_Extents_Max_X.ElementAtOrDefault(index, @default);
        public Double[] Column_Extents_Max_Y { get; }
        public Double GetExtents_Max_Y(int index, Double @default = default) => Column_Extents_Max_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_Extents_Max_Z { get; }
        public Double GetExtents_Max_Z(int index, Double @default = default) => Column_Extents_Max_Z.ElementAtOrDefault(index, @default);
        public int[] Column_LevelIndex { get; }
        public int GetLevelIndex(int index) => Column_LevelIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Level GetLevel(int index) => _GetReferencedLevel(GetLevelIndex(index));
        private Level _GetReferencedLevel(int referencedIndex) => ParentTableSet.GetLevel(referencedIndex);
        public int[] Column_ViewIndex { get; }
        public int GetViewIndex(int index) => Column_ViewIndex.ElementAtOrDefault(index, EntityRelation.None);
        public View GetView(int index) => _GetReferencedView(GetViewIndex(index));
        private View _GetReferencedView(int referencedIndex) => ParentTableSet.GetView(referencedIndex);
        // Object Getter
        public LevelInView Get(int index)
        {
            if (index < 0) return null;
            var r = new LevelInView();
            r.Index = index;
            r.Extents_Min_X = GetExtents_Min_X(index);
            r.Extents_Min_Y = GetExtents_Min_Y(index);
            r.Extents_Min_Z = GetExtents_Min_Z(index);
            r.Extents_Max_X = GetExtents_Max_X(index);
            r.Extents_Max_Y = GetExtents_Max_Y(index);
            r.Extents_Max_Z = GetExtents_Max_Z(index);
            r._Level = new Relation<Vim.Format.ObjectModel.Level>(GetLevelIndex(index), _GetReferencedLevel);
            r._View = new Relation<Vim.Format.ObjectModel.View>(GetViewIndex(index), _GetReferencedView);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<LevelInView> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class LevelInViewTable 
    
    public partial class CameraTable : EntityTable_v2, IEnumerable<Camera>
    {
        
        public const string TableName = TableNames.Camera;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public CameraTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Id = GetDataColumnValues<Int32>("int:Id") ?? Array.Empty<Int32>();
            Column_IsPerspective = GetDataColumnValues<Int32>("int:IsPerspective") ?? Array.Empty<Int32>();
            Column_VerticalExtent = GetDataColumnValues<Double>("double:VerticalExtent") ?? Array.Empty<Double>();
            Column_HorizontalExtent = GetDataColumnValues<Double>("double:HorizontalExtent") ?? Array.Empty<Double>();
            Column_FarDistance = GetDataColumnValues<Double>("double:FarDistance") ?? Array.Empty<Double>();
            Column_NearDistance = GetDataColumnValues<Double>("double:NearDistance") ?? Array.Empty<Double>();
            Column_TargetDistance = GetDataColumnValues<Double>("double:TargetDistance") ?? Array.Empty<Double>();
            Column_RightOffset = GetDataColumnValues<Double>("double:RightOffset") ?? Array.Empty<Double>();
            Column_UpOffset = GetDataColumnValues<Double>("double:UpOffset") ?? Array.Empty<Double>();
        }
        
        public Int32[] Column_Id { get; }
        public Int32 GetId(int index, Int32 @default = default) => Column_Id.ElementAtOrDefault(index, @default);
        public Int32[] Column_IsPerspective { get; }
        public Int32 GetIsPerspective(int index, Int32 @default = default) => Column_IsPerspective.ElementAtOrDefault(index, @default);
        public Double[] Column_VerticalExtent { get; }
        public Double GetVerticalExtent(int index, Double @default = default) => Column_VerticalExtent.ElementAtOrDefault(index, @default);
        public Double[] Column_HorizontalExtent { get; }
        public Double GetHorizontalExtent(int index, Double @default = default) => Column_HorizontalExtent.ElementAtOrDefault(index, @default);
        public Double[] Column_FarDistance { get; }
        public Double GetFarDistance(int index, Double @default = default) => Column_FarDistance.ElementAtOrDefault(index, @default);
        public Double[] Column_NearDistance { get; }
        public Double GetNearDistance(int index, Double @default = default) => Column_NearDistance.ElementAtOrDefault(index, @default);
        public Double[] Column_TargetDistance { get; }
        public Double GetTargetDistance(int index, Double @default = default) => Column_TargetDistance.ElementAtOrDefault(index, @default);
        public Double[] Column_RightOffset { get; }
        public Double GetRightOffset(int index, Double @default = default) => Column_RightOffset.ElementAtOrDefault(index, @default);
        public Double[] Column_UpOffset { get; }
        public Double GetUpOffset(int index, Double @default = default) => Column_UpOffset.ElementAtOrDefault(index, @default);
        // Object Getter
        public Camera Get(int index)
        {
            if (index < 0) return null;
            var r = new Camera();
            r.Index = index;
            r.Id = GetId(index);
            r.IsPerspective = GetIsPerspective(index);
            r.VerticalExtent = GetVerticalExtent(index);
            r.HorizontalExtent = GetHorizontalExtent(index);
            r.FarDistance = GetFarDistance(index);
            r.NearDistance = GetNearDistance(index);
            r.TargetDistance = GetTargetDistance(index);
            r.RightOffset = GetRightOffset(index);
            r.UpOffset = GetUpOffset(index);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Camera> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class CameraTable 
    
    public partial class MaterialTable : EntityTable_v2, IEnumerable<Material> , IElementKindTable
    {
        
        public const string TableName = TableNames.Material;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public MaterialTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Name = GetStringColumnValues("string:Name") ?? Array.Empty<String>();
            Column_MaterialCategory = GetStringColumnValues("string:MaterialCategory") ?? Array.Empty<String>();
            Column_Color_X = GetDataColumnValues<Double>("double:Color.X") ?? Array.Empty<Double>();
            Column_Color_Y = GetDataColumnValues<Double>("double:Color.Y") ?? Array.Empty<Double>();
            Column_Color_Z = GetDataColumnValues<Double>("double:Color.Z") ?? Array.Empty<Double>();
            Column_ColorUvScaling_X = GetDataColumnValues<Double>("double:ColorUvScaling.X") ?? Array.Empty<Double>();
            Column_ColorUvScaling_Y = GetDataColumnValues<Double>("double:ColorUvScaling.Y") ?? Array.Empty<Double>();
            Column_ColorUvOffset_X = GetDataColumnValues<Double>("double:ColorUvOffset.X") ?? Array.Empty<Double>();
            Column_ColorUvOffset_Y = GetDataColumnValues<Double>("double:ColorUvOffset.Y") ?? Array.Empty<Double>();
            Column_NormalUvScaling_X = GetDataColumnValues<Double>("double:NormalUvScaling.X") ?? Array.Empty<Double>();
            Column_NormalUvScaling_Y = GetDataColumnValues<Double>("double:NormalUvScaling.Y") ?? Array.Empty<Double>();
            Column_NormalUvOffset_X = GetDataColumnValues<Double>("double:NormalUvOffset.X") ?? Array.Empty<Double>();
            Column_NormalUvOffset_Y = GetDataColumnValues<Double>("double:NormalUvOffset.Y") ?? Array.Empty<Double>();
            Column_NormalAmount = GetDataColumnValues<Double>("double:NormalAmount") ?? Array.Empty<Double>();
            Column_Glossiness = GetDataColumnValues<Double>("double:Glossiness") ?? Array.Empty<Double>();
            Column_Smoothness = GetDataColumnValues<Double>("double:Smoothness") ?? Array.Empty<Double>();
            Column_Transparency = GetDataColumnValues<Double>("double:Transparency") ?? Array.Empty<Double>();
            Column_ColorTextureFileIndex = GetIndexColumnValues("index:Vim.Asset:ColorTextureFile") ?? Array.Empty<int>();
            Column_NormalTextureFileIndex = GetIndexColumnValues("index:Vim.Asset:NormalTextureFile") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public String[] Column_Name { get; }
        public String GetName(int index, String @default = "") => Column_Name.ElementAtOrDefault(index, @default);
        public String[] Column_MaterialCategory { get; }
        public String GetMaterialCategory(int index, String @default = "") => Column_MaterialCategory.ElementAtOrDefault(index, @default);
        public Double[] Column_Color_X { get; }
        public Double GetColor_X(int index, Double @default = default) => Column_Color_X.ElementAtOrDefault(index, @default);
        public Double[] Column_Color_Y { get; }
        public Double GetColor_Y(int index, Double @default = default) => Column_Color_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_Color_Z { get; }
        public Double GetColor_Z(int index, Double @default = default) => Column_Color_Z.ElementAtOrDefault(index, @default);
        public Double[] Column_ColorUvScaling_X { get; }
        public Double GetColorUvScaling_X(int index, Double @default = default) => Column_ColorUvScaling_X.ElementAtOrDefault(index, @default);
        public Double[] Column_ColorUvScaling_Y { get; }
        public Double GetColorUvScaling_Y(int index, Double @default = default) => Column_ColorUvScaling_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_ColorUvOffset_X { get; }
        public Double GetColorUvOffset_X(int index, Double @default = default) => Column_ColorUvOffset_X.ElementAtOrDefault(index, @default);
        public Double[] Column_ColorUvOffset_Y { get; }
        public Double GetColorUvOffset_Y(int index, Double @default = default) => Column_ColorUvOffset_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_NormalUvScaling_X { get; }
        public Double GetNormalUvScaling_X(int index, Double @default = default) => Column_NormalUvScaling_X.ElementAtOrDefault(index, @default);
        public Double[] Column_NormalUvScaling_Y { get; }
        public Double GetNormalUvScaling_Y(int index, Double @default = default) => Column_NormalUvScaling_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_NormalUvOffset_X { get; }
        public Double GetNormalUvOffset_X(int index, Double @default = default) => Column_NormalUvOffset_X.ElementAtOrDefault(index, @default);
        public Double[] Column_NormalUvOffset_Y { get; }
        public Double GetNormalUvOffset_Y(int index, Double @default = default) => Column_NormalUvOffset_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_NormalAmount { get; }
        public Double GetNormalAmount(int index, Double @default = default) => Column_NormalAmount.ElementAtOrDefault(index, @default);
        public Double[] Column_Glossiness { get; }
        public Double GetGlossiness(int index, Double @default = default) => Column_Glossiness.ElementAtOrDefault(index, @default);
        public Double[] Column_Smoothness { get; }
        public Double GetSmoothness(int index, Double @default = default) => Column_Smoothness.ElementAtOrDefault(index, @default);
        public Double[] Column_Transparency { get; }
        public Double GetTransparency(int index, Double @default = default) => Column_Transparency.ElementAtOrDefault(index, @default);
        public int[] Column_ColorTextureFileIndex { get; }
        public int GetColorTextureFileIndex(int index) => Column_ColorTextureFileIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Asset GetColorTextureFile(int index) => _GetReferencedColorTextureFile(GetColorTextureFileIndex(index));
        private Asset _GetReferencedColorTextureFile(int referencedIndex) => ParentTableSet.GetAsset(referencedIndex);
        public int[] Column_NormalTextureFileIndex { get; }
        public int GetNormalTextureFileIndex(int index) => Column_NormalTextureFileIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Asset GetNormalTextureFile(int index) => _GetReferencedNormalTextureFile(GetNormalTextureFileIndex(index));
        private Asset _GetReferencedNormalTextureFile(int referencedIndex) => ParentTableSet.GetAsset(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Material Get(int index)
        {
            if (index < 0) return null;
            var r = new Material();
            r.Index = index;
            r.Name = GetName(index);
            r.MaterialCategory = GetMaterialCategory(index);
            r.Color_X = GetColor_X(index);
            r.Color_Y = GetColor_Y(index);
            r.Color_Z = GetColor_Z(index);
            r.ColorUvScaling_X = GetColorUvScaling_X(index);
            r.ColorUvScaling_Y = GetColorUvScaling_Y(index);
            r.ColorUvOffset_X = GetColorUvOffset_X(index);
            r.ColorUvOffset_Y = GetColorUvOffset_Y(index);
            r.NormalUvScaling_X = GetNormalUvScaling_X(index);
            r.NormalUvScaling_Y = GetNormalUvScaling_Y(index);
            r.NormalUvOffset_X = GetNormalUvOffset_X(index);
            r.NormalUvOffset_Y = GetNormalUvOffset_Y(index);
            r.NormalAmount = GetNormalAmount(index);
            r.Glossiness = GetGlossiness(index);
            r.Smoothness = GetSmoothness(index);
            r.Transparency = GetTransparency(index);
            r._ColorTextureFile = new Relation<Vim.Format.ObjectModel.Asset>(GetColorTextureFileIndex(index), _GetReferencedColorTextureFile);
            r._NormalTextureFile = new Relation<Vim.Format.ObjectModel.Asset>(GetNormalTextureFileIndex(index), _GetReferencedNormalTextureFile);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Material> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class MaterialTable 
    
    public partial class MaterialInElementTable : EntityTable_v2, IEnumerable<MaterialInElement>
    {
        
        public const string TableName = TableNames.MaterialInElement;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public MaterialInElementTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Area = GetDataColumnValues<Double>("double:Area") ?? Array.Empty<Double>();
            Column_Volume = GetDataColumnValues<Double>("double:Volume") ?? Array.Empty<Double>();
            Column_IsPaint = GetDataColumnValues<Boolean>("byte:IsPaint") ?? Array.Empty<Boolean>();
            Column_MaterialIndex = GetIndexColumnValues("index:Vim.Material:Material") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Double[] Column_Area { get; }
        public Double GetArea(int index, Double @default = default) => Column_Area.ElementAtOrDefault(index, @default);
        public Double[] Column_Volume { get; }
        public Double GetVolume(int index, Double @default = default) => Column_Volume.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsPaint { get; }
        public Boolean GetIsPaint(int index, Boolean @default = default) => Column_IsPaint.ElementAtOrDefault(index, @default);
        public int[] Column_MaterialIndex { get; }
        public int GetMaterialIndex(int index) => Column_MaterialIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Material GetMaterial(int index) => _GetReferencedMaterial(GetMaterialIndex(index));
        private Material _GetReferencedMaterial(int referencedIndex) => ParentTableSet.GetMaterial(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public MaterialInElement Get(int index)
        {
            if (index < 0) return null;
            var r = new MaterialInElement();
            r.Index = index;
            r.Area = GetArea(index);
            r.Volume = GetVolume(index);
            r.IsPaint = GetIsPaint(index);
            r._Material = new Relation<Vim.Format.ObjectModel.Material>(GetMaterialIndex(index), _GetReferencedMaterial);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<MaterialInElement> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class MaterialInElementTable 
    
    public partial class CompoundStructureLayerTable : EntityTable_v2, IEnumerable<CompoundStructureLayer>
    {
        
        public const string TableName = TableNames.CompoundStructureLayer;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public CompoundStructureLayerTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_OrderIndex = GetDataColumnValues<Int32>("int:OrderIndex") ?? Array.Empty<Int32>();
            Column_Width = GetDataColumnValues<Double>("double:Width") ?? Array.Empty<Double>();
            Column_MaterialFunctionAssignment = GetStringColumnValues("string:MaterialFunctionAssignment") ?? Array.Empty<String>();
            Column_MaterialIndex = GetIndexColumnValues("index:Vim.Material:Material") ?? Array.Empty<int>();
            Column_CompoundStructureIndex = GetIndexColumnValues("index:Vim.CompoundStructure:CompoundStructure") ?? Array.Empty<int>();
        }
        
        public Int32[] Column_OrderIndex { get; }
        public Int32 GetOrderIndex(int index, Int32 @default = default) => Column_OrderIndex.ElementAtOrDefault(index, @default);
        public Double[] Column_Width { get; }
        public Double GetWidth(int index, Double @default = default) => Column_Width.ElementAtOrDefault(index, @default);
        public String[] Column_MaterialFunctionAssignment { get; }
        public String GetMaterialFunctionAssignment(int index, String @default = "") => Column_MaterialFunctionAssignment.ElementAtOrDefault(index, @default);
        public int[] Column_MaterialIndex { get; }
        public int GetMaterialIndex(int index) => Column_MaterialIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Material GetMaterial(int index) => _GetReferencedMaterial(GetMaterialIndex(index));
        private Material _GetReferencedMaterial(int referencedIndex) => ParentTableSet.GetMaterial(referencedIndex);
        public int[] Column_CompoundStructureIndex { get; }
        public int GetCompoundStructureIndex(int index) => Column_CompoundStructureIndex.ElementAtOrDefault(index, EntityRelation.None);
        public CompoundStructure GetCompoundStructure(int index) => _GetReferencedCompoundStructure(GetCompoundStructureIndex(index));
        private CompoundStructure _GetReferencedCompoundStructure(int referencedIndex) => ParentTableSet.GetCompoundStructure(referencedIndex);
        // Object Getter
        public CompoundStructureLayer Get(int index)
        {
            if (index < 0) return null;
            var r = new CompoundStructureLayer();
            r.Index = index;
            r.OrderIndex = GetOrderIndex(index);
            r.Width = GetWidth(index);
            r.MaterialFunctionAssignment = GetMaterialFunctionAssignment(index);
            r._Material = new Relation<Vim.Format.ObjectModel.Material>(GetMaterialIndex(index), _GetReferencedMaterial);
            r._CompoundStructure = new Relation<Vim.Format.ObjectModel.CompoundStructure>(GetCompoundStructureIndex(index), _GetReferencedCompoundStructure);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<CompoundStructureLayer> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class CompoundStructureLayerTable 
    
    public partial class CompoundStructureTable : EntityTable_v2, IEnumerable<CompoundStructure>
    {
        
        public const string TableName = TableNames.CompoundStructure;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public CompoundStructureTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Width = GetDataColumnValues<Double>("double:Width") ?? Array.Empty<Double>();
            Column_StructuralLayerIndex = GetIndexColumnValues("index:Vim.CompoundStructureLayer:StructuralLayer") ?? Array.Empty<int>();
        }
        
        public Double[] Column_Width { get; }
        public Double GetWidth(int index, Double @default = default) => Column_Width.ElementAtOrDefault(index, @default);
        public int[] Column_StructuralLayerIndex { get; }
        public int GetStructuralLayerIndex(int index) => Column_StructuralLayerIndex.ElementAtOrDefault(index, EntityRelation.None);
        public CompoundStructureLayer GetStructuralLayer(int index) => _GetReferencedStructuralLayer(GetStructuralLayerIndex(index));
        private CompoundStructureLayer _GetReferencedStructuralLayer(int referencedIndex) => ParentTableSet.GetCompoundStructureLayer(referencedIndex);
        // Object Getter
        public CompoundStructure Get(int index)
        {
            if (index < 0) return null;
            var r = new CompoundStructure();
            r.Index = index;
            r.Width = GetWidth(index);
            r._StructuralLayer = new Relation<Vim.Format.ObjectModel.CompoundStructureLayer>(GetStructuralLayerIndex(index), _GetReferencedStructuralLayer);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<CompoundStructure> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class CompoundStructureTable 
    
    public partial class NodeTable : EntityTable_v2, IEnumerable<Node>
    {
        
        public const string TableName = TableNames.Node;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public NodeTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Node Get(int index)
        {
            if (index < 0) return null;
            var r = new Node();
            r.Index = index;
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Node> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class NodeTable 
    
    public partial class GeometryTable : EntityTable_v2, IEnumerable<Geometry>
    {
        
        public const string TableName = TableNames.Geometry;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public GeometryTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Box_Min_X = GetDataColumnValues<Single>("float:Box.Min.X") ?? Array.Empty<Single>();
            Column_Box_Min_Y = GetDataColumnValues<Single>("float:Box.Min.Y") ?? Array.Empty<Single>();
            Column_Box_Min_Z = GetDataColumnValues<Single>("float:Box.Min.Z") ?? Array.Empty<Single>();
            Column_Box_Max_X = GetDataColumnValues<Single>("float:Box.Max.X") ?? Array.Empty<Single>();
            Column_Box_Max_Y = GetDataColumnValues<Single>("float:Box.Max.Y") ?? Array.Empty<Single>();
            Column_Box_Max_Z = GetDataColumnValues<Single>("float:Box.Max.Z") ?? Array.Empty<Single>();
            Column_VertexCount = GetDataColumnValues<Int32>("int:VertexCount") ?? Array.Empty<Int32>();
            Column_FaceCount = GetDataColumnValues<Int32>("int:FaceCount") ?? Array.Empty<Int32>();
        }
        
        public Single[] Column_Box_Min_X { get; }
        public Single GetBox_Min_X(int index, Single @default = default) => Column_Box_Min_X.ElementAtOrDefault(index, @default);
        public Single[] Column_Box_Min_Y { get; }
        public Single GetBox_Min_Y(int index, Single @default = default) => Column_Box_Min_Y.ElementAtOrDefault(index, @default);
        public Single[] Column_Box_Min_Z { get; }
        public Single GetBox_Min_Z(int index, Single @default = default) => Column_Box_Min_Z.ElementAtOrDefault(index, @default);
        public Single[] Column_Box_Max_X { get; }
        public Single GetBox_Max_X(int index, Single @default = default) => Column_Box_Max_X.ElementAtOrDefault(index, @default);
        public Single[] Column_Box_Max_Y { get; }
        public Single GetBox_Max_Y(int index, Single @default = default) => Column_Box_Max_Y.ElementAtOrDefault(index, @default);
        public Single[] Column_Box_Max_Z { get; }
        public Single GetBox_Max_Z(int index, Single @default = default) => Column_Box_Max_Z.ElementAtOrDefault(index, @default);
        public Int32[] Column_VertexCount { get; }
        public Int32 GetVertexCount(int index, Int32 @default = default) => Column_VertexCount.ElementAtOrDefault(index, @default);
        public Int32[] Column_FaceCount { get; }
        public Int32 GetFaceCount(int index, Int32 @default = default) => Column_FaceCount.ElementAtOrDefault(index, @default);
        // Object Getter
        public Geometry Get(int index)
        {
            if (index < 0) return null;
            var r = new Geometry();
            r.Index = index;
            r.Box_Min_X = GetBox_Min_X(index);
            r.Box_Min_Y = GetBox_Min_Y(index);
            r.Box_Min_Z = GetBox_Min_Z(index);
            r.Box_Max_X = GetBox_Max_X(index);
            r.Box_Max_Y = GetBox_Max_Y(index);
            r.Box_Max_Z = GetBox_Max_Z(index);
            r.VertexCount = GetVertexCount(index);
            r.FaceCount = GetFaceCount(index);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Geometry> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class GeometryTable 
    
    public partial class ShapeTable : EntityTable_v2, IEnumerable<Shape>
    {
        
        public const string TableName = TableNames.Shape;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public ShapeTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Shape Get(int index)
        {
            if (index < 0) return null;
            var r = new Shape();
            r.Index = index;
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Shape> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ShapeTable 
    
    public partial class ShapeCollectionTable : EntityTable_v2, IEnumerable<ShapeCollection>
    {
        
        public const string TableName = TableNames.ShapeCollection;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public ShapeCollectionTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public ShapeCollection Get(int index)
        {
            if (index < 0) return null;
            var r = new ShapeCollection();
            r.Index = index;
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<ShapeCollection> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ShapeCollectionTable 
    
    public partial class ShapeInShapeCollectionTable : EntityTable_v2, IEnumerable<ShapeInShapeCollection>
    {
        
        public const string TableName = TableNames.ShapeInShapeCollection;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public ShapeInShapeCollectionTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_ShapeIndex = GetIndexColumnValues("index:Vim.Shape:Shape") ?? Array.Empty<int>();
            Column_ShapeCollectionIndex = GetIndexColumnValues("index:Vim.ShapeCollection:ShapeCollection") ?? Array.Empty<int>();
        }
        
        public int[] Column_ShapeIndex { get; }
        public int GetShapeIndex(int index) => Column_ShapeIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Shape GetShape(int index) => _GetReferencedShape(GetShapeIndex(index));
        private Shape _GetReferencedShape(int referencedIndex) => ParentTableSet.GetShape(referencedIndex);
        public int[] Column_ShapeCollectionIndex { get; }
        public int GetShapeCollectionIndex(int index) => Column_ShapeCollectionIndex.ElementAtOrDefault(index, EntityRelation.None);
        public ShapeCollection GetShapeCollection(int index) => _GetReferencedShapeCollection(GetShapeCollectionIndex(index));
        private ShapeCollection _GetReferencedShapeCollection(int referencedIndex) => ParentTableSet.GetShapeCollection(referencedIndex);
        // Object Getter
        public ShapeInShapeCollection Get(int index)
        {
            if (index < 0) return null;
            var r = new ShapeInShapeCollection();
            r.Index = index;
            r._Shape = new Relation<Vim.Format.ObjectModel.Shape>(GetShapeIndex(index), _GetReferencedShape);
            r._ShapeCollection = new Relation<Vim.Format.ObjectModel.ShapeCollection>(GetShapeCollectionIndex(index), _GetReferencedShapeCollection);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<ShapeInShapeCollection> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ShapeInShapeCollectionTable 
    
    public partial class SystemTable : EntityTable_v2, IEnumerable<System> , IElementKindTable
    {
        
        public const string TableName = TableNames.System;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public SystemTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_SystemType = GetDataColumnValues<Int32>("int:SystemType") ?? Array.Empty<Int32>();
            Column_FamilyTypeIndex = GetIndexColumnValues("index:Vim.FamilyType:FamilyType") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Int32[] Column_SystemType { get; }
        public Int32 GetSystemType(int index, Int32 @default = default) => Column_SystemType.ElementAtOrDefault(index, @default);
        public int[] Column_FamilyTypeIndex { get; }
        public int GetFamilyTypeIndex(int index) => Column_FamilyTypeIndex.ElementAtOrDefault(index, EntityRelation.None);
        public FamilyType GetFamilyType(int index) => _GetReferencedFamilyType(GetFamilyTypeIndex(index));
        private FamilyType _GetReferencedFamilyType(int referencedIndex) => ParentTableSet.GetFamilyType(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public System Get(int index)
        {
            if (index < 0) return null;
            var r = new System();
            r.Index = index;
            r.SystemType = GetSystemType(index);
            r._FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>(GetFamilyTypeIndex(index), _GetReferencedFamilyType);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<System> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class SystemTable 
    
    public partial class ElementInSystemTable : EntityTable_v2, IEnumerable<ElementInSystem>
    {
        
        public const string TableName = TableNames.ElementInSystem;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public ElementInSystemTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Roles = GetDataColumnValues<Int32>("int:Roles") ?? Array.Empty<Int32>();
            Column_SystemIndex = GetIndexColumnValues("index:Vim.System:System") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Int32[] Column_Roles { get; }
        public Int32 GetRoles(int index, Int32 @default = default) => Column_Roles.ElementAtOrDefault(index, @default);
        public int[] Column_SystemIndex { get; }
        public int GetSystemIndex(int index) => Column_SystemIndex.ElementAtOrDefault(index, EntityRelation.None);
        public System GetSystem(int index) => _GetReferencedSystem(GetSystemIndex(index));
        private System _GetReferencedSystem(int referencedIndex) => ParentTableSet.GetSystem(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public ElementInSystem Get(int index)
        {
            if (index < 0) return null;
            var r = new ElementInSystem();
            r.Index = index;
            r.Roles = GetRoles(index);
            r._System = new Relation<Vim.Format.ObjectModel.System>(GetSystemIndex(index), _GetReferencedSystem);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<ElementInSystem> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ElementInSystemTable 
    
    public partial class WarningTable : EntityTable_v2, IEnumerable<Warning>
    {
        
        public const string TableName = TableNames.Warning;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public WarningTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Guid = GetStringColumnValues("string:Guid") ?? Array.Empty<String>();
            Column_Severity = GetStringColumnValues("string:Severity") ?? Array.Empty<String>();
            Column_Description = GetStringColumnValues("string:Description") ?? Array.Empty<String>();
            Column_BimDocumentIndex = GetIndexColumnValues("index:Vim.BimDocument:BimDocument") ?? Array.Empty<int>();
        }
        
        public String[] Column_Guid { get; }
        public String GetGuid(int index, String @default = "") => Column_Guid.ElementAtOrDefault(index, @default);
        public String[] Column_Severity { get; }
        public String GetSeverity(int index, String @default = "") => Column_Severity.ElementAtOrDefault(index, @default);
        public String[] Column_Description { get; }
        public String GetDescription(int index, String @default = "") => Column_Description.ElementAtOrDefault(index, @default);
        public int[] Column_BimDocumentIndex { get; }
        public int GetBimDocumentIndex(int index) => Column_BimDocumentIndex.ElementAtOrDefault(index, EntityRelation.None);
        public BimDocument GetBimDocument(int index) => _GetReferencedBimDocument(GetBimDocumentIndex(index));
        private BimDocument _GetReferencedBimDocument(int referencedIndex) => ParentTableSet.GetBimDocument(referencedIndex);
        // Object Getter
        public Warning Get(int index)
        {
            if (index < 0) return null;
            var r = new Warning();
            r.Index = index;
            r.Guid = GetGuid(index);
            r.Severity = GetSeverity(index);
            r.Description = GetDescription(index);
            r._BimDocument = new Relation<Vim.Format.ObjectModel.BimDocument>(GetBimDocumentIndex(index), _GetReferencedBimDocument);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Warning> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class WarningTable 
    
    public partial class ElementInWarningTable : EntityTable_v2, IEnumerable<ElementInWarning>
    {
        
        public const string TableName = TableNames.ElementInWarning;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public ElementInWarningTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_WarningIndex = GetIndexColumnValues("index:Vim.Warning:Warning") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public int[] Column_WarningIndex { get; }
        public int GetWarningIndex(int index) => Column_WarningIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Warning GetWarning(int index) => _GetReferencedWarning(GetWarningIndex(index));
        private Warning _GetReferencedWarning(int referencedIndex) => ParentTableSet.GetWarning(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public ElementInWarning Get(int index)
        {
            if (index < 0) return null;
            var r = new ElementInWarning();
            r.Index = index;
            r._Warning = new Relation<Vim.Format.ObjectModel.Warning>(GetWarningIndex(index), _GetReferencedWarning);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<ElementInWarning> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ElementInWarningTable 
    
    public partial class BasePointTable : EntityTable_v2, IEnumerable<BasePoint> , IElementKindTable
    {
        
        public const string TableName = TableNames.BasePoint;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public BasePointTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_IsSurveyPoint = GetDataColumnValues<Boolean>("byte:IsSurveyPoint") ?? Array.Empty<Boolean>();
            Column_Position_X = GetDataColumnValues<Double>("double:Position.X") ?? Array.Empty<Double>();
            Column_Position_Y = GetDataColumnValues<Double>("double:Position.Y") ?? Array.Empty<Double>();
            Column_Position_Z = GetDataColumnValues<Double>("double:Position.Z") ?? Array.Empty<Double>();
            Column_SharedPosition_X = GetDataColumnValues<Double>("double:SharedPosition.X") ?? Array.Empty<Double>();
            Column_SharedPosition_Y = GetDataColumnValues<Double>("double:SharedPosition.Y") ?? Array.Empty<Double>();
            Column_SharedPosition_Z = GetDataColumnValues<Double>("double:SharedPosition.Z") ?? Array.Empty<Double>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Boolean[] Column_IsSurveyPoint { get; }
        public Boolean GetIsSurveyPoint(int index, Boolean @default = default) => Column_IsSurveyPoint.ElementAtOrDefault(index, @default);
        public Double[] Column_Position_X { get; }
        public Double GetPosition_X(int index, Double @default = default) => Column_Position_X.ElementAtOrDefault(index, @default);
        public Double[] Column_Position_Y { get; }
        public Double GetPosition_Y(int index, Double @default = default) => Column_Position_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_Position_Z { get; }
        public Double GetPosition_Z(int index, Double @default = default) => Column_Position_Z.ElementAtOrDefault(index, @default);
        public Double[] Column_SharedPosition_X { get; }
        public Double GetSharedPosition_X(int index, Double @default = default) => Column_SharedPosition_X.ElementAtOrDefault(index, @default);
        public Double[] Column_SharedPosition_Y { get; }
        public Double GetSharedPosition_Y(int index, Double @default = default) => Column_SharedPosition_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_SharedPosition_Z { get; }
        public Double GetSharedPosition_Z(int index, Double @default = default) => Column_SharedPosition_Z.ElementAtOrDefault(index, @default);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public BasePoint Get(int index)
        {
            if (index < 0) return null;
            var r = new BasePoint();
            r.Index = index;
            r.IsSurveyPoint = GetIsSurveyPoint(index);
            r.Position_X = GetPosition_X(index);
            r.Position_Y = GetPosition_Y(index);
            r.Position_Z = GetPosition_Z(index);
            r.SharedPosition_X = GetSharedPosition_X(index);
            r.SharedPosition_Y = GetSharedPosition_Y(index);
            r.SharedPosition_Z = GetSharedPosition_Z(index);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<BasePoint> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class BasePointTable 
    
    public partial class PhaseFilterTable : EntityTable_v2, IEnumerable<PhaseFilter> , IElementKindTable
    {
        
        public const string TableName = TableNames.PhaseFilter;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public PhaseFilterTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_New = GetDataColumnValues<Int32>("int:New") ?? Array.Empty<Int32>();
            Column_Existing = GetDataColumnValues<Int32>("int:Existing") ?? Array.Empty<Int32>();
            Column_Demolished = GetDataColumnValues<Int32>("int:Demolished") ?? Array.Empty<Int32>();
            Column_Temporary = GetDataColumnValues<Int32>("int:Temporary") ?? Array.Empty<Int32>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Int32[] Column_New { get; }
        public Int32 GetNew(int index, Int32 @default = default) => Column_New.ElementAtOrDefault(index, @default);
        public Int32[] Column_Existing { get; }
        public Int32 GetExisting(int index, Int32 @default = default) => Column_Existing.ElementAtOrDefault(index, @default);
        public Int32[] Column_Demolished { get; }
        public Int32 GetDemolished(int index, Int32 @default = default) => Column_Demolished.ElementAtOrDefault(index, @default);
        public Int32[] Column_Temporary { get; }
        public Int32 GetTemporary(int index, Int32 @default = default) => Column_Temporary.ElementAtOrDefault(index, @default);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public PhaseFilter Get(int index)
        {
            if (index < 0) return null;
            var r = new PhaseFilter();
            r.Index = index;
            r.New = GetNew(index);
            r.Existing = GetExisting(index);
            r.Demolished = GetDemolished(index);
            r.Temporary = GetTemporary(index);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<PhaseFilter> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class PhaseFilterTable 
    
    public partial class GridTable : EntityTable_v2, IEnumerable<Grid> , IElementKindTable
    {
        
        public const string TableName = TableNames.Grid;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public GridTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_StartPoint_X = GetDataColumnValues<Double>("double:StartPoint.X") ?? Array.Empty<Double>();
            Column_StartPoint_Y = GetDataColumnValues<Double>("double:StartPoint.Y") ?? Array.Empty<Double>();
            Column_StartPoint_Z = GetDataColumnValues<Double>("double:StartPoint.Z") ?? Array.Empty<Double>();
            Column_EndPoint_X = GetDataColumnValues<Double>("double:EndPoint.X") ?? Array.Empty<Double>();
            Column_EndPoint_Y = GetDataColumnValues<Double>("double:EndPoint.Y") ?? Array.Empty<Double>();
            Column_EndPoint_Z = GetDataColumnValues<Double>("double:EndPoint.Z") ?? Array.Empty<Double>();
            Column_IsCurved = GetDataColumnValues<Boolean>("byte:IsCurved") ?? Array.Empty<Boolean>();
            Column_Extents_Min_X = GetDataColumnValues<Double>("double:Extents.Min.X") ?? Array.Empty<Double>();
            Column_Extents_Min_Y = GetDataColumnValues<Double>("double:Extents.Min.Y") ?? Array.Empty<Double>();
            Column_Extents_Min_Z = GetDataColumnValues<Double>("double:Extents.Min.Z") ?? Array.Empty<Double>();
            Column_Extents_Max_X = GetDataColumnValues<Double>("double:Extents.Max.X") ?? Array.Empty<Double>();
            Column_Extents_Max_Y = GetDataColumnValues<Double>("double:Extents.Max.Y") ?? Array.Empty<Double>();
            Column_Extents_Max_Z = GetDataColumnValues<Double>("double:Extents.Max.Z") ?? Array.Empty<Double>();
            Column_FamilyTypeIndex = GetIndexColumnValues("index:Vim.FamilyType:FamilyType") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Double[] Column_StartPoint_X { get; }
        public Double GetStartPoint_X(int index, Double @default = default) => Column_StartPoint_X.ElementAtOrDefault(index, @default);
        public Double[] Column_StartPoint_Y { get; }
        public Double GetStartPoint_Y(int index, Double @default = default) => Column_StartPoint_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_StartPoint_Z { get; }
        public Double GetStartPoint_Z(int index, Double @default = default) => Column_StartPoint_Z.ElementAtOrDefault(index, @default);
        public Double[] Column_EndPoint_X { get; }
        public Double GetEndPoint_X(int index, Double @default = default) => Column_EndPoint_X.ElementAtOrDefault(index, @default);
        public Double[] Column_EndPoint_Y { get; }
        public Double GetEndPoint_Y(int index, Double @default = default) => Column_EndPoint_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_EndPoint_Z { get; }
        public Double GetEndPoint_Z(int index, Double @default = default) => Column_EndPoint_Z.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsCurved { get; }
        public Boolean GetIsCurved(int index, Boolean @default = default) => Column_IsCurved.ElementAtOrDefault(index, @default);
        public Double[] Column_Extents_Min_X { get; }
        public Double GetExtents_Min_X(int index, Double @default = default) => Column_Extents_Min_X.ElementAtOrDefault(index, @default);
        public Double[] Column_Extents_Min_Y { get; }
        public Double GetExtents_Min_Y(int index, Double @default = default) => Column_Extents_Min_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_Extents_Min_Z { get; }
        public Double GetExtents_Min_Z(int index, Double @default = default) => Column_Extents_Min_Z.ElementAtOrDefault(index, @default);
        public Double[] Column_Extents_Max_X { get; }
        public Double GetExtents_Max_X(int index, Double @default = default) => Column_Extents_Max_X.ElementAtOrDefault(index, @default);
        public Double[] Column_Extents_Max_Y { get; }
        public Double GetExtents_Max_Y(int index, Double @default = default) => Column_Extents_Max_Y.ElementAtOrDefault(index, @default);
        public Double[] Column_Extents_Max_Z { get; }
        public Double GetExtents_Max_Z(int index, Double @default = default) => Column_Extents_Max_Z.ElementAtOrDefault(index, @default);
        public int[] Column_FamilyTypeIndex { get; }
        public int GetFamilyTypeIndex(int index) => Column_FamilyTypeIndex.ElementAtOrDefault(index, EntityRelation.None);
        public FamilyType GetFamilyType(int index) => _GetReferencedFamilyType(GetFamilyTypeIndex(index));
        private FamilyType _GetReferencedFamilyType(int referencedIndex) => ParentTableSet.GetFamilyType(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Grid Get(int index)
        {
            if (index < 0) return null;
            var r = new Grid();
            r.Index = index;
            r.StartPoint_X = GetStartPoint_X(index);
            r.StartPoint_Y = GetStartPoint_Y(index);
            r.StartPoint_Z = GetStartPoint_Z(index);
            r.EndPoint_X = GetEndPoint_X(index);
            r.EndPoint_Y = GetEndPoint_Y(index);
            r.EndPoint_Z = GetEndPoint_Z(index);
            r.IsCurved = GetIsCurved(index);
            r.Extents_Min_X = GetExtents_Min_X(index);
            r.Extents_Min_Y = GetExtents_Min_Y(index);
            r.Extents_Min_Z = GetExtents_Min_Z(index);
            r.Extents_Max_X = GetExtents_Max_X(index);
            r.Extents_Max_Y = GetExtents_Max_Y(index);
            r.Extents_Max_Z = GetExtents_Max_Z(index);
            r._FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>(GetFamilyTypeIndex(index), _GetReferencedFamilyType);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Grid> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class GridTable 
    
    public partial class AreaTable : EntityTable_v2, IEnumerable<Area> , IElementKindTable
    {
        
        public const string TableName = TableNames.Area;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public AreaTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Value = GetDataColumnValues<Double>("double:Value") ?? Array.Empty<Double>();
            Column_Perimeter = GetDataColumnValues<Double>("double:Perimeter") ?? Array.Empty<Double>();
            Column_Number = GetStringColumnValues("string:Number") ?? Array.Empty<String>();
            Column_IsGrossInterior = GetDataColumnValues<Boolean>("byte:IsGrossInterior") ?? Array.Empty<Boolean>();
            Column_AreaSchemeIndex = GetIndexColumnValues("index:Vim.AreaScheme:AreaScheme") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Double[] Column_Value { get; }
        public Double GetValue(int index, Double @default = default) => Column_Value.ElementAtOrDefault(index, @default);
        public Double[] Column_Perimeter { get; }
        public Double GetPerimeter(int index, Double @default = default) => Column_Perimeter.ElementAtOrDefault(index, @default);
        public String[] Column_Number { get; }
        public String GetNumber(int index, String @default = "") => Column_Number.ElementAtOrDefault(index, @default);
        public Boolean[] Column_IsGrossInterior { get; }
        public Boolean GetIsGrossInterior(int index, Boolean @default = default) => Column_IsGrossInterior.ElementAtOrDefault(index, @default);
        public int[] Column_AreaSchemeIndex { get; }
        public int GetAreaSchemeIndex(int index) => Column_AreaSchemeIndex.ElementAtOrDefault(index, EntityRelation.None);
        public AreaScheme GetAreaScheme(int index) => _GetReferencedAreaScheme(GetAreaSchemeIndex(index));
        private AreaScheme _GetReferencedAreaScheme(int referencedIndex) => ParentTableSet.GetAreaScheme(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Area Get(int index)
        {
            if (index < 0) return null;
            var r = new Area();
            r.Index = index;
            r.Value = GetValue(index);
            r.Perimeter = GetPerimeter(index);
            r.Number = GetNumber(index);
            r.IsGrossInterior = GetIsGrossInterior(index);
            r._AreaScheme = new Relation<Vim.Format.ObjectModel.AreaScheme>(GetAreaSchemeIndex(index), _GetReferencedAreaScheme);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Area> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class AreaTable 
    
    public partial class AreaSchemeTable : EntityTable_v2, IEnumerable<AreaScheme> , IElementKindTable
    {
        
        public const string TableName = TableNames.AreaScheme;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public AreaSchemeTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_IsGrossBuildingArea = GetDataColumnValues<Boolean>("byte:IsGrossBuildingArea") ?? Array.Empty<Boolean>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Boolean[] Column_IsGrossBuildingArea { get; }
        public Boolean GetIsGrossBuildingArea(int index, Boolean @default = default) => Column_IsGrossBuildingArea.ElementAtOrDefault(index, @default);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public AreaScheme Get(int index)
        {
            if (index < 0) return null;
            var r = new AreaScheme();
            r.Index = index;
            r.IsGrossBuildingArea = GetIsGrossBuildingArea(index);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<AreaScheme> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class AreaSchemeTable 
    
    public partial class ScheduleTable : EntityTable_v2, IEnumerable<Schedule> , IElementKindTable
    {
        
        public const string TableName = TableNames.Schedule;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public ScheduleTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Schedule Get(int index)
        {
            if (index < 0) return null;
            var r = new Schedule();
            r.Index = index;
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Schedule> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ScheduleTable 
    
    public partial class ScheduleColumnTable : EntityTable_v2, IEnumerable<ScheduleColumn>
    {
        
        public const string TableName = TableNames.ScheduleColumn;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public ScheduleColumnTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Name = GetStringColumnValues("string:Name") ?? Array.Empty<String>();
            Column_ColumnIndex = GetDataColumnValues<Int32>("int:ColumnIndex") ?? Array.Empty<Int32>();
            Column_ScheduleIndex = GetIndexColumnValues("index:Vim.Schedule:Schedule") ?? Array.Empty<int>();
        }
        
        public String[] Column_Name { get; }
        public String GetName(int index, String @default = "") => Column_Name.ElementAtOrDefault(index, @default);
        public Int32[] Column_ColumnIndex { get; }
        public Int32 GetColumnIndex(int index, Int32 @default = default) => Column_ColumnIndex.ElementAtOrDefault(index, @default);
        public int[] Column_ScheduleIndex { get; }
        public int GetScheduleIndex(int index) => Column_ScheduleIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Schedule GetSchedule(int index) => _GetReferencedSchedule(GetScheduleIndex(index));
        private Schedule _GetReferencedSchedule(int referencedIndex) => ParentTableSet.GetSchedule(referencedIndex);
        // Object Getter
        public ScheduleColumn Get(int index)
        {
            if (index < 0) return null;
            var r = new ScheduleColumn();
            r.Index = index;
            r.Name = GetName(index);
            r.ColumnIndex = GetColumnIndex(index);
            r._Schedule = new Relation<Vim.Format.ObjectModel.Schedule>(GetScheduleIndex(index), _GetReferencedSchedule);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<ScheduleColumn> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ScheduleColumnTable 
    
    public partial class ScheduleCellTable : EntityTable_v2, IEnumerable<ScheduleCell>
    {
        
        public const string TableName = TableNames.ScheduleCell;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public ScheduleCellTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Value = GetStringColumnValues("string:Value") ?? Array.Empty<String>();
            Column_RowIndex = GetDataColumnValues<Int32>("int:RowIndex") ?? Array.Empty<Int32>();
            Column_ScheduleColumnIndex = GetIndexColumnValues("index:Vim.ScheduleColumn:ScheduleColumn") ?? Array.Empty<int>();
        }
        
        public String[] Column_Value { get; }
        public String GetValue(int index, String @default = "") => Column_Value.ElementAtOrDefault(index, @default);
        public Int32[] Column_RowIndex { get; }
        public Int32 GetRowIndex(int index, Int32 @default = default) => Column_RowIndex.ElementAtOrDefault(index, @default);
        public int[] Column_ScheduleColumnIndex { get; }
        public int GetScheduleColumnIndex(int index) => Column_ScheduleColumnIndex.ElementAtOrDefault(index, EntityRelation.None);
        public ScheduleColumn GetScheduleColumn(int index) => _GetReferencedScheduleColumn(GetScheduleColumnIndex(index));
        private ScheduleColumn _GetReferencedScheduleColumn(int referencedIndex) => ParentTableSet.GetScheduleColumn(referencedIndex);
        // Object Getter
        public ScheduleCell Get(int index)
        {
            if (index < 0) return null;
            var r = new ScheduleCell();
            r.Index = index;
            r.Value = GetValue(index);
            r.RowIndex = GetRowIndex(index);
            r._ScheduleColumn = new Relation<Vim.Format.ObjectModel.ScheduleColumn>(GetScheduleColumnIndex(index), _GetReferencedScheduleColumn);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<ScheduleCell> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ScheduleCellTable 
    
    public partial class ViewSheetSetTable : EntityTable_v2, IEnumerable<ViewSheetSet> , IElementKindTable
    {
        
        public const string TableName = TableNames.ViewSheetSet;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public ViewSheetSetTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public ViewSheetSet Get(int index)
        {
            if (index < 0) return null;
            var r = new ViewSheetSet();
            r.Index = index;
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<ViewSheetSet> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ViewSheetSetTable 
    
    public partial class ViewSheetTable : EntityTable_v2, IEnumerable<ViewSheet> , IElementKindTable
    {
        
        public const string TableName = TableNames.ViewSheet;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public ViewSheetTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_FamilyTypeIndex = GetIndexColumnValues("index:Vim.FamilyType:FamilyType") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public int[] Column_FamilyTypeIndex { get; }
        public int GetFamilyTypeIndex(int index) => Column_FamilyTypeIndex.ElementAtOrDefault(index, EntityRelation.None);
        public FamilyType GetFamilyType(int index) => _GetReferencedFamilyType(GetFamilyTypeIndex(index));
        private FamilyType _GetReferencedFamilyType(int referencedIndex) => ParentTableSet.GetFamilyType(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public ViewSheet Get(int index)
        {
            if (index < 0) return null;
            var r = new ViewSheet();
            r.Index = index;
            r._FamilyType = new Relation<Vim.Format.ObjectModel.FamilyType>(GetFamilyTypeIndex(index), _GetReferencedFamilyType);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<ViewSheet> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ViewSheetTable 
    
    public partial class ViewSheetInViewSheetSetTable : EntityTable_v2, IEnumerable<ViewSheetInViewSheetSet>
    {
        
        public const string TableName = TableNames.ViewSheetInViewSheetSet;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public ViewSheetInViewSheetSetTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_ViewSheetIndex = GetIndexColumnValues("index:Vim.ViewSheet:ViewSheet") ?? Array.Empty<int>();
            Column_ViewSheetSetIndex = GetIndexColumnValues("index:Vim.ViewSheetSet:ViewSheetSet") ?? Array.Empty<int>();
        }
        
        public int[] Column_ViewSheetIndex { get; }
        public int GetViewSheetIndex(int index) => Column_ViewSheetIndex.ElementAtOrDefault(index, EntityRelation.None);
        public ViewSheet GetViewSheet(int index) => _GetReferencedViewSheet(GetViewSheetIndex(index));
        private ViewSheet _GetReferencedViewSheet(int referencedIndex) => ParentTableSet.GetViewSheet(referencedIndex);
        public int[] Column_ViewSheetSetIndex { get; }
        public int GetViewSheetSetIndex(int index) => Column_ViewSheetSetIndex.ElementAtOrDefault(index, EntityRelation.None);
        public ViewSheetSet GetViewSheetSet(int index) => _GetReferencedViewSheetSet(GetViewSheetSetIndex(index));
        private ViewSheetSet _GetReferencedViewSheetSet(int referencedIndex) => ParentTableSet.GetViewSheetSet(referencedIndex);
        // Object Getter
        public ViewSheetInViewSheetSet Get(int index)
        {
            if (index < 0) return null;
            var r = new ViewSheetInViewSheetSet();
            r.Index = index;
            r._ViewSheet = new Relation<Vim.Format.ObjectModel.ViewSheet>(GetViewSheetIndex(index), _GetReferencedViewSheet);
            r._ViewSheetSet = new Relation<Vim.Format.ObjectModel.ViewSheetSet>(GetViewSheetSetIndex(index), _GetReferencedViewSheetSet);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<ViewSheetInViewSheetSet> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ViewSheetInViewSheetSetTable 
    
    public partial class ViewInViewSheetSetTable : EntityTable_v2, IEnumerable<ViewInViewSheetSet>
    {
        
        public const string TableName = TableNames.ViewInViewSheetSet;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public ViewInViewSheetSetTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_ViewIndex = GetIndexColumnValues("index:Vim.View:View") ?? Array.Empty<int>();
            Column_ViewSheetSetIndex = GetIndexColumnValues("index:Vim.ViewSheetSet:ViewSheetSet") ?? Array.Empty<int>();
        }
        
        public int[] Column_ViewIndex { get; }
        public int GetViewIndex(int index) => Column_ViewIndex.ElementAtOrDefault(index, EntityRelation.None);
        public View GetView(int index) => _GetReferencedView(GetViewIndex(index));
        private View _GetReferencedView(int referencedIndex) => ParentTableSet.GetView(referencedIndex);
        public int[] Column_ViewSheetSetIndex { get; }
        public int GetViewSheetSetIndex(int index) => Column_ViewSheetSetIndex.ElementAtOrDefault(index, EntityRelation.None);
        public ViewSheetSet GetViewSheetSet(int index) => _GetReferencedViewSheetSet(GetViewSheetSetIndex(index));
        private ViewSheetSet _GetReferencedViewSheetSet(int referencedIndex) => ParentTableSet.GetViewSheetSet(referencedIndex);
        // Object Getter
        public ViewInViewSheetSet Get(int index)
        {
            if (index < 0) return null;
            var r = new ViewInViewSheetSet();
            r.Index = index;
            r._View = new Relation<Vim.Format.ObjectModel.View>(GetViewIndex(index), _GetReferencedView);
            r._ViewSheetSet = new Relation<Vim.Format.ObjectModel.ViewSheetSet>(GetViewSheetSetIndex(index), _GetReferencedViewSheetSet);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<ViewInViewSheetSet> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ViewInViewSheetSetTable 
    
    public partial class ViewInViewSheetTable : EntityTable_v2, IEnumerable<ViewInViewSheet>
    {
        
        public const string TableName = TableNames.ViewInViewSheet;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public ViewInViewSheetTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_ViewIndex = GetIndexColumnValues("index:Vim.View:View") ?? Array.Empty<int>();
            Column_ViewSheetIndex = GetIndexColumnValues("index:Vim.ViewSheet:ViewSheet") ?? Array.Empty<int>();
        }
        
        public int[] Column_ViewIndex { get; }
        public int GetViewIndex(int index) => Column_ViewIndex.ElementAtOrDefault(index, EntityRelation.None);
        public View GetView(int index) => _GetReferencedView(GetViewIndex(index));
        private View _GetReferencedView(int referencedIndex) => ParentTableSet.GetView(referencedIndex);
        public int[] Column_ViewSheetIndex { get; }
        public int GetViewSheetIndex(int index) => Column_ViewSheetIndex.ElementAtOrDefault(index, EntityRelation.None);
        public ViewSheet GetViewSheet(int index) => _GetReferencedViewSheet(GetViewSheetIndex(index));
        private ViewSheet _GetReferencedViewSheet(int referencedIndex) => ParentTableSet.GetViewSheet(referencedIndex);
        // Object Getter
        public ViewInViewSheet Get(int index)
        {
            if (index < 0) return null;
            var r = new ViewInViewSheet();
            r.Index = index;
            r._View = new Relation<Vim.Format.ObjectModel.View>(GetViewIndex(index), _GetReferencedView);
            r._ViewSheet = new Relation<Vim.Format.ObjectModel.ViewSheet>(GetViewSheetIndex(index), _GetReferencedViewSheet);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<ViewInViewSheet> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class ViewInViewSheetTable 
    
    public partial class SiteTable : EntityTable_v2, IEnumerable<Site> , IElementKindTable
    {
        
        public const string TableName = TableNames.Site;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public SiteTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Latitude = GetDataColumnValues<Double>("double:Latitude") ?? Array.Empty<Double>();
            Column_Longitude = GetDataColumnValues<Double>("double:Longitude") ?? Array.Empty<Double>();
            Column_Address = GetStringColumnValues("string:Address") ?? Array.Empty<String>();
            Column_Elevation = GetDataColumnValues<Double>("double:Elevation") ?? Array.Empty<Double>();
            Column_Number = GetStringColumnValues("string:Number") ?? Array.Empty<String>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Double[] Column_Latitude { get; }
        public Double GetLatitude(int index, Double @default = default) => Column_Latitude.ElementAtOrDefault(index, @default);
        public Double[] Column_Longitude { get; }
        public Double GetLongitude(int index, Double @default = default) => Column_Longitude.ElementAtOrDefault(index, @default);
        public String[] Column_Address { get; }
        public String GetAddress(int index, String @default = "") => Column_Address.ElementAtOrDefault(index, @default);
        public Double[] Column_Elevation { get; }
        public Double GetElevation(int index, Double @default = default) => Column_Elevation.ElementAtOrDefault(index, @default);
        public String[] Column_Number { get; }
        public String GetNumber(int index, String @default = "") => Column_Number.ElementAtOrDefault(index, @default);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Site Get(int index)
        {
            if (index < 0) return null;
            var r = new Site();
            r.Index = index;
            r.Latitude = GetLatitude(index);
            r.Longitude = GetLongitude(index);
            r.Address = GetAddress(index);
            r.Elevation = GetElevation(index);
            r.Number = GetNumber(index);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Site> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class SiteTable 
    
    public partial class BuildingTable : EntityTable_v2, IEnumerable<Building> , IElementKindTable
    {
        
        public const string TableName = TableNames.Building;
        
        public EntityTableSet ParentTableSet { get; } // can be null
        
        public BuildingTable(SerializableEntityTable rawTable, string[] stringTable, EntityTableSet parentTableSet = null) : base(rawTable, stringTable)
        {
            ParentTableSet = parentTableSet;
            Column_Elevation = GetDataColumnValues<Double>("double:Elevation") ?? Array.Empty<Double>();
            Column_TerrainElevation = GetDataColumnValues<Double>("double:TerrainElevation") ?? Array.Empty<Double>();
            Column_Address = GetStringColumnValues("string:Address") ?? Array.Empty<String>();
            Column_SiteIndex = GetIndexColumnValues("index:Vim.Site:Site") ?? Array.Empty<int>();
            Column_ElementIndex = GetIndexColumnValues("index:Vim.Element:Element") ?? Array.Empty<int>();
        }
        
        public Double[] Column_Elevation { get; }
        public Double GetElevation(int index, Double @default = default) => Column_Elevation.ElementAtOrDefault(index, @default);
        public Double[] Column_TerrainElevation { get; }
        public Double GetTerrainElevation(int index, Double @default = default) => Column_TerrainElevation.ElementAtOrDefault(index, @default);
        public String[] Column_Address { get; }
        public String GetAddress(int index, String @default = "") => Column_Address.ElementAtOrDefault(index, @default);
        public int[] Column_SiteIndex { get; }
        public int GetSiteIndex(int index) => Column_SiteIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Site GetSite(int index) => _GetReferencedSite(GetSiteIndex(index));
        private Site _GetReferencedSite(int referencedIndex) => ParentTableSet.GetSite(referencedIndex);
        public int[] Column_ElementIndex { get; }
        public int GetElementIndex(int index) => Column_ElementIndex.ElementAtOrDefault(index, EntityRelation.None);
        public Element GetElement(int index) => _GetReferencedElement(GetElementIndex(index));
        private Element _GetReferencedElement(int referencedIndex) => ParentTableSet.GetElement(referencedIndex);
        // Object Getter
        public Building Get(int index)
        {
            if (index < 0) return null;
            var r = new Building();
            r.Index = index;
            r.Elevation = GetElevation(index);
            r.TerrainElevation = GetTerrainElevation(index);
            r.Address = GetAddress(index);
            r._Site = new Relation<Vim.Format.ObjectModel.Site>(GetSiteIndex(index), _GetReferencedSite);
            r._Element = new Relation<Vim.Format.ObjectModel.Element>(GetElementIndex(index), _GetReferencedElement);
            return r;
        }
        // Enumerator
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Building> GetEnumerator()
        {
            for (var i = 0; i < RowCount; ++i)
                yield return Get(i);
        }
    } // class BuildingTable 
    
    public static class DocumentBuilderExtensions
    {
        public static EntityTableBuilder ToAssetTableBuilder(this EntitySetBuilder<Asset> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.Asset);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BufferName; }
                tb.AddStringColumn("string:BufferName", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToDisplayUnitTableBuilder(this EntitySetBuilder<DisplayUnit> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.DisplayUnit);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Spec; }
                tb.AddStringColumn("string:Spec", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Type; }
                tb.AddStringColumn("string:Type", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Label; }
                tb.AddStringColumn("string:Label", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToParameterDescriptorTableBuilder(this EntitySetBuilder<ParameterDescriptor> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.ParameterDescriptor);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Name; }
                tb.AddStringColumn("string:Name", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Group; }
                tb.AddStringColumn("string:Group", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ParameterType; }
                tb.AddStringColumn("string:ParameterType", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsInstance; }
                tb.AddDataColumn("byte:IsInstance", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsShared; }
                tb.AddDataColumn("byte:IsShared", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsReadOnly; }
                tb.AddDataColumn("byte:IsReadOnly", columnData);
            }
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Flags; }
                tb.AddDataColumn("int:Flags", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Guid; }
                tb.AddStringColumn("string:Guid", columnData);
            }
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].StorageType; }
                tb.AddDataColumn("int:StorageType", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._DisplayUnit?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.DisplayUnit:DisplayUnit", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToParameterTableBuilder(this EntitySetBuilder<Parameter> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.Parameter);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Value; }
                tb.AddStringColumn("string:Value", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._ParameterDescriptor?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.ParameterDescriptor:ParameterDescriptor", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToElementTableBuilder(this EntitySetBuilder<Element> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.Element);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Int64[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Id; }
                tb.AddDataColumn("long:Id", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Type; }
                tb.AddStringColumn("string:Type", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Name; }
                tb.AddStringColumn("string:Name", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].UniqueId; }
                tb.AddStringColumn("string:UniqueId", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Location_X; }
                tb.AddDataColumn("float:Location.X", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Location_Y; }
                tb.AddDataColumn("float:Location.Y", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Location_Z; }
                tb.AddDataColumn("float:Location.Z", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].FamilyName; }
                tb.AddStringColumn("string:FamilyName", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsPinned; }
                tb.AddDataColumn("byte:IsPinned", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Level?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Level:Level", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._PhaseCreated?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Phase:PhaseCreated", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._PhaseDemolished?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Phase:PhaseDemolished", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Category?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Category:Category", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Workset?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Workset:Workset", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._DesignOption?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.DesignOption:DesignOption", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._OwnerView?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.View:OwnerView", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Group?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Group:Group", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._AssemblyInstance?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.AssemblyInstance:AssemblyInstance", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._BimDocument?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.BimDocument:BimDocument", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Room?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Room:Room", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToWorksetTableBuilder(this EntitySetBuilder<Workset> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.Workset);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Id; }
                tb.AddDataColumn("int:Id", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Name; }
                tb.AddStringColumn("string:Name", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Kind; }
                tb.AddStringColumn("string:Kind", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsOpen; }
                tb.AddDataColumn("byte:IsOpen", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsEditable; }
                tb.AddDataColumn("byte:IsEditable", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Owner; }
                tb.AddStringColumn("string:Owner", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].UniqueId; }
                tb.AddStringColumn("string:UniqueId", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._BimDocument?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.BimDocument:BimDocument", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToAssemblyInstanceTableBuilder(this EntitySetBuilder<AssemblyInstance> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.AssemblyInstance);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].AssemblyTypeName; }
                tb.AddStringColumn("string:AssemblyTypeName", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Position_X; }
                tb.AddDataColumn("float:Position.X", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Position_Y; }
                tb.AddDataColumn("float:Position.Y", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Position_Z; }
                tb.AddDataColumn("float:Position.Z", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToGroupTableBuilder(this EntitySetBuilder<Group> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.Group);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].GroupType; }
                tb.AddStringColumn("string:GroupType", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Position_X; }
                tb.AddDataColumn("float:Position.X", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Position_Y; }
                tb.AddDataColumn("float:Position.Y", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Position_Z; }
                tb.AddDataColumn("float:Position.Z", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToDesignOptionTableBuilder(this EntitySetBuilder<DesignOption> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.DesignOption);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsPrimary; }
                tb.AddDataColumn("byte:IsPrimary", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToLevelTableBuilder(this EntitySetBuilder<Level> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.Level);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Elevation; }
                tb.AddDataColumn("double:Elevation", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ProjectElevation; }
                tb.AddDataColumn("double:ProjectElevation", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._FamilyType?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.FamilyType:FamilyType", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Building?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Building:Building", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToPhaseTableBuilder(this EntitySetBuilder<Phase> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.Phase);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToRoomTableBuilder(this EntitySetBuilder<Room> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.Room);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BaseOffset; }
                tb.AddDataColumn("double:BaseOffset", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].LimitOffset; }
                tb.AddDataColumn("double:LimitOffset", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].UnboundedHeight; }
                tb.AddDataColumn("double:UnboundedHeight", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Volume; }
                tb.AddDataColumn("double:Volume", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Perimeter; }
                tb.AddDataColumn("double:Perimeter", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Area; }
                tb.AddDataColumn("double:Area", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Number; }
                tb.AddStringColumn("string:Number", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._UpperLimit?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Level:UpperLimit", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToBimDocumentTableBuilder(this EntitySetBuilder<BimDocument> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.BimDocument);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Title; }
                tb.AddStringColumn("string:Title", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsMetric; }
                tb.AddDataColumn("byte:IsMetric", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Guid; }
                tb.AddStringColumn("string:Guid", columnData);
            }
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].NumSaves; }
                tb.AddDataColumn("int:NumSaves", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsLinked; }
                tb.AddDataColumn("byte:IsLinked", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsDetached; }
                tb.AddDataColumn("byte:IsDetached", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsWorkshared; }
                tb.AddDataColumn("byte:IsWorkshared", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].PathName; }
                tb.AddStringColumn("string:PathName", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Latitude; }
                tb.AddDataColumn("double:Latitude", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Longitude; }
                tb.AddDataColumn("double:Longitude", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].TimeZone; }
                tb.AddDataColumn("double:TimeZone", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].PlaceName; }
                tb.AddStringColumn("string:PlaceName", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].WeatherStationName; }
                tb.AddStringColumn("string:WeatherStationName", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Elevation; }
                tb.AddDataColumn("double:Elevation", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ProjectLocation; }
                tb.AddStringColumn("string:ProjectLocation", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IssueDate; }
                tb.AddStringColumn("string:IssueDate", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Status; }
                tb.AddStringColumn("string:Status", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ClientName; }
                tb.AddStringColumn("string:ClientName", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Address; }
                tb.AddStringColumn("string:Address", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Name; }
                tb.AddStringColumn("string:Name", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Number; }
                tb.AddStringColumn("string:Number", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Author; }
                tb.AddStringColumn("string:Author", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BuildingName; }
                tb.AddStringColumn("string:BuildingName", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].OrganizationName; }
                tb.AddStringColumn("string:OrganizationName", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].OrganizationDescription; }
                tb.AddStringColumn("string:OrganizationDescription", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Product; }
                tb.AddStringColumn("string:Product", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Version; }
                tb.AddStringColumn("string:Version", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].User; }
                tb.AddStringColumn("string:User", columnData);
            }
            {
                var columnData = new Int64[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].FileLength; }
                tb.AddDataColumn("long:FileLength", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._ActiveView?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.View:ActiveView", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._OwnerFamily?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Family:OwnerFamily", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Parent?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.BimDocument:Parent", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToDisplayUnitInBimDocumentTableBuilder(this EntitySetBuilder<DisplayUnitInBimDocument> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.DisplayUnitInBimDocument);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._DisplayUnit?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.DisplayUnit:DisplayUnit", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._BimDocument?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.BimDocument:BimDocument", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToPhaseOrderInBimDocumentTableBuilder(this EntitySetBuilder<PhaseOrderInBimDocument> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.PhaseOrderInBimDocument);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].OrderIndex; }
                tb.AddDataColumn("int:OrderIndex", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Phase?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Phase:Phase", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._BimDocument?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.BimDocument:BimDocument", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToCategoryTableBuilder(this EntitySetBuilder<Category> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.Category);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Name; }
                tb.AddStringColumn("string:Name", columnData);
            }
            {
                var columnData = new Int64[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Id; }
                tb.AddDataColumn("long:Id", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].CategoryType; }
                tb.AddStringColumn("string:CategoryType", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].LineColor_X; }
                tb.AddDataColumn("double:LineColor.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].LineColor_Y; }
                tb.AddDataColumn("double:LineColor.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].LineColor_Z; }
                tb.AddDataColumn("double:LineColor.Z", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BuiltInCategory; }
                tb.AddStringColumn("string:BuiltInCategory", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Parent?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Category:Parent", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Material?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Material:Material", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToFamilyTableBuilder(this EntitySetBuilder<Family> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.Family);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].StructuralMaterialType; }
                tb.AddStringColumn("string:StructuralMaterialType", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].StructuralSectionShape; }
                tb.AddStringColumn("string:StructuralSectionShape", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsSystemFamily; }
                tb.AddDataColumn("byte:IsSystemFamily", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsInPlace; }
                tb.AddDataColumn("byte:IsInPlace", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._FamilyCategory?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Category:FamilyCategory", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToFamilyTypeTableBuilder(this EntitySetBuilder<FamilyType> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.FamilyType);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsSystemFamilyType; }
                tb.AddDataColumn("byte:IsSystemFamilyType", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Family?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Family:Family", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._CompoundStructure?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.CompoundStructure:CompoundStructure", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToFamilyInstanceTableBuilder(this EntitySetBuilder<FamilyInstance> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.FamilyInstance);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].FacingFlipped; }
                tb.AddDataColumn("byte:FacingFlipped", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].FacingOrientation_X; }
                tb.AddDataColumn("float:FacingOrientation.X", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].FacingOrientation_Y; }
                tb.AddDataColumn("float:FacingOrientation.Y", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].FacingOrientation_Z; }
                tb.AddDataColumn("float:FacingOrientation.Z", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].HandFlipped; }
                tb.AddDataColumn("byte:HandFlipped", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Mirrored; }
                tb.AddDataColumn("byte:Mirrored", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].HasModifiedGeometry; }
                tb.AddDataColumn("byte:HasModifiedGeometry", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Scale; }
                tb.AddDataColumn("float:Scale", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BasisX_X; }
                tb.AddDataColumn("float:BasisX.X", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BasisX_Y; }
                tb.AddDataColumn("float:BasisX.Y", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BasisX_Z; }
                tb.AddDataColumn("float:BasisX.Z", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BasisY_X; }
                tb.AddDataColumn("float:BasisY.X", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BasisY_Y; }
                tb.AddDataColumn("float:BasisY.Y", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BasisY_Z; }
                tb.AddDataColumn("float:BasisY.Z", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BasisZ_X; }
                tb.AddDataColumn("float:BasisZ.X", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BasisZ_Y; }
                tb.AddDataColumn("float:BasisZ.Y", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].BasisZ_Z; }
                tb.AddDataColumn("float:BasisZ.Z", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Translation_X; }
                tb.AddDataColumn("float:Translation.X", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Translation_Y; }
                tb.AddDataColumn("float:Translation.Y", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Translation_Z; }
                tb.AddDataColumn("float:Translation.Z", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].HandOrientation_X; }
                tb.AddDataColumn("float:HandOrientation.X", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].HandOrientation_Y; }
                tb.AddDataColumn("float:HandOrientation.Y", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].HandOrientation_Z; }
                tb.AddDataColumn("float:HandOrientation.Z", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._FamilyType?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.FamilyType:FamilyType", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Host?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Host", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._FromRoom?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Room:FromRoom", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._ToRoom?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Room:ToRoom", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._SuperComponent?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:SuperComponent", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToViewTableBuilder(this EntitySetBuilder<View> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.View);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Title; }
                tb.AddStringColumn("string:Title", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ViewType; }
                tb.AddStringColumn("string:ViewType", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Up_X; }
                tb.AddDataColumn("double:Up.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Up_Y; }
                tb.AddDataColumn("double:Up.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Up_Z; }
                tb.AddDataColumn("double:Up.Z", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Right_X; }
                tb.AddDataColumn("double:Right.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Right_Y; }
                tb.AddDataColumn("double:Right.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Right_Z; }
                tb.AddDataColumn("double:Right.Z", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Origin_X; }
                tb.AddDataColumn("double:Origin.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Origin_Y; }
                tb.AddDataColumn("double:Origin.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Origin_Z; }
                tb.AddDataColumn("double:Origin.Z", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ViewDirection_X; }
                tb.AddDataColumn("double:ViewDirection.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ViewDirection_Y; }
                tb.AddDataColumn("double:ViewDirection.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ViewDirection_Z; }
                tb.AddDataColumn("double:ViewDirection.Z", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ViewPosition_X; }
                tb.AddDataColumn("double:ViewPosition.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ViewPosition_Y; }
                tb.AddDataColumn("double:ViewPosition.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ViewPosition_Z; }
                tb.AddDataColumn("double:ViewPosition.Z", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Scale; }
                tb.AddDataColumn("double:Scale", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Outline_Min_X; }
                tb.AddDataColumn("double:Outline.Min.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Outline_Min_Y; }
                tb.AddDataColumn("double:Outline.Min.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Outline_Max_X; }
                tb.AddDataColumn("double:Outline.Max.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Outline_Max_Y; }
                tb.AddDataColumn("double:Outline.Max.Y", columnData);
            }
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].DetailLevel; }
                tb.AddDataColumn("int:DetailLevel", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Camera?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Camera:Camera", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._FamilyType?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.FamilyType:FamilyType", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToElementInViewTableBuilder(this EntitySetBuilder<ElementInView> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.ElementInView);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._View?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.View:View", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToShapeInViewTableBuilder(this EntitySetBuilder<ShapeInView> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.ShapeInView);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Shape?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Shape:Shape", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._View?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.View:View", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToAssetInViewTableBuilder(this EntitySetBuilder<AssetInView> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.AssetInView);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Asset?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Asset:Asset", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._View?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.View:View", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToAssetInViewSheetTableBuilder(this EntitySetBuilder<AssetInViewSheet> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.AssetInViewSheet);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Asset?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Asset:Asset", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._ViewSheet?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.ViewSheet:ViewSheet", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToLevelInViewTableBuilder(this EntitySetBuilder<LevelInView> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.LevelInView);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Extents_Min_X; }
                tb.AddDataColumn("double:Extents.Min.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Extents_Min_Y; }
                tb.AddDataColumn("double:Extents.Min.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Extents_Min_Z; }
                tb.AddDataColumn("double:Extents.Min.Z", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Extents_Max_X; }
                tb.AddDataColumn("double:Extents.Max.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Extents_Max_Y; }
                tb.AddDataColumn("double:Extents.Max.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Extents_Max_Z; }
                tb.AddDataColumn("double:Extents.Max.Z", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Level?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Level:Level", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._View?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.View:View", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToCameraTableBuilder(this EntitySetBuilder<Camera> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.Camera);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Id; }
                tb.AddDataColumn("int:Id", columnData);
            }
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsPerspective; }
                tb.AddDataColumn("int:IsPerspective", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].VerticalExtent; }
                tb.AddDataColumn("double:VerticalExtent", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].HorizontalExtent; }
                tb.AddDataColumn("double:HorizontalExtent", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].FarDistance; }
                tb.AddDataColumn("double:FarDistance", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].NearDistance; }
                tb.AddDataColumn("double:NearDistance", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].TargetDistance; }
                tb.AddDataColumn("double:TargetDistance", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].RightOffset; }
                tb.AddDataColumn("double:RightOffset", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].UpOffset; }
                tb.AddDataColumn("double:UpOffset", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToMaterialTableBuilder(this EntitySetBuilder<Material> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.Material);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Name; }
                tb.AddStringColumn("string:Name", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].MaterialCategory; }
                tb.AddStringColumn("string:MaterialCategory", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Color_X; }
                tb.AddDataColumn("double:Color.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Color_Y; }
                tb.AddDataColumn("double:Color.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Color_Z; }
                tb.AddDataColumn("double:Color.Z", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ColorUvScaling_X; }
                tb.AddDataColumn("double:ColorUvScaling.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ColorUvScaling_Y; }
                tb.AddDataColumn("double:ColorUvScaling.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ColorUvOffset_X; }
                tb.AddDataColumn("double:ColorUvOffset.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ColorUvOffset_Y; }
                tb.AddDataColumn("double:ColorUvOffset.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].NormalUvScaling_X; }
                tb.AddDataColumn("double:NormalUvScaling.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].NormalUvScaling_Y; }
                tb.AddDataColumn("double:NormalUvScaling.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].NormalUvOffset_X; }
                tb.AddDataColumn("double:NormalUvOffset.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].NormalUvOffset_Y; }
                tb.AddDataColumn("double:NormalUvOffset.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].NormalAmount; }
                tb.AddDataColumn("double:NormalAmount", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Glossiness; }
                tb.AddDataColumn("double:Glossiness", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Smoothness; }
                tb.AddDataColumn("double:Smoothness", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Transparency; }
                tb.AddDataColumn("double:Transparency", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._ColorTextureFile?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Asset:ColorTextureFile", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._NormalTextureFile?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Asset:NormalTextureFile", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToMaterialInElementTableBuilder(this EntitySetBuilder<MaterialInElement> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.MaterialInElement);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Area; }
                tb.AddDataColumn("double:Area", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Volume; }
                tb.AddDataColumn("double:Volume", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsPaint; }
                tb.AddDataColumn("byte:IsPaint", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Material?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Material:Material", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToCompoundStructureLayerTableBuilder(this EntitySetBuilder<CompoundStructureLayer> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.CompoundStructureLayer);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].OrderIndex; }
                tb.AddDataColumn("int:OrderIndex", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Width; }
                tb.AddDataColumn("double:Width", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].MaterialFunctionAssignment; }
                tb.AddStringColumn("string:MaterialFunctionAssignment", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Material?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Material:Material", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._CompoundStructure?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.CompoundStructure:CompoundStructure", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToCompoundStructureTableBuilder(this EntitySetBuilder<CompoundStructure> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.CompoundStructure);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Width; }
                tb.AddDataColumn("double:Width", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._StructuralLayer?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.CompoundStructureLayer:StructuralLayer", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToNodeTableBuilder(this EntitySetBuilder<Node> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.Node);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToGeometryTableBuilder(this EntitySetBuilder<Geometry> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.Geometry);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Box_Min_X; }
                tb.AddDataColumn("float:Box.Min.X", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Box_Min_Y; }
                tb.AddDataColumn("float:Box.Min.Y", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Box_Min_Z; }
                tb.AddDataColumn("float:Box.Min.Z", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Box_Max_X; }
                tb.AddDataColumn("float:Box.Max.X", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Box_Max_Y; }
                tb.AddDataColumn("float:Box.Max.Y", columnData);
            }
            {
                var columnData = new Single[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Box_Max_Z; }
                tb.AddDataColumn("float:Box.Max.Z", columnData);
            }
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].VertexCount; }
                tb.AddDataColumn("int:VertexCount", columnData);
            }
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].FaceCount; }
                tb.AddDataColumn("int:FaceCount", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToShapeTableBuilder(this EntitySetBuilder<Shape> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.Shape);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToShapeCollectionTableBuilder(this EntitySetBuilder<ShapeCollection> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.ShapeCollection);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToShapeInShapeCollectionTableBuilder(this EntitySetBuilder<ShapeInShapeCollection> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.ShapeInShapeCollection);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Shape?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Shape:Shape", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._ShapeCollection?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.ShapeCollection:ShapeCollection", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToSystemTableBuilder(this EntitySetBuilder<System> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.System);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].SystemType; }
                tb.AddDataColumn("int:SystemType", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._FamilyType?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.FamilyType:FamilyType", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToElementInSystemTableBuilder(this EntitySetBuilder<ElementInSystem> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.ElementInSystem);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Roles; }
                tb.AddDataColumn("int:Roles", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._System?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.System:System", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToWarningTableBuilder(this EntitySetBuilder<Warning> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.Warning);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Guid; }
                tb.AddStringColumn("string:Guid", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Severity; }
                tb.AddStringColumn("string:Severity", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Description; }
                tb.AddStringColumn("string:Description", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._BimDocument?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.BimDocument:BimDocument", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToElementInWarningTableBuilder(this EntitySetBuilder<ElementInWarning> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.ElementInWarning);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Warning?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Warning:Warning", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToBasePointTableBuilder(this EntitySetBuilder<BasePoint> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.BasePoint);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsSurveyPoint; }
                tb.AddDataColumn("byte:IsSurveyPoint", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Position_X; }
                tb.AddDataColumn("double:Position.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Position_Y; }
                tb.AddDataColumn("double:Position.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Position_Z; }
                tb.AddDataColumn("double:Position.Z", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].SharedPosition_X; }
                tb.AddDataColumn("double:SharedPosition.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].SharedPosition_Y; }
                tb.AddDataColumn("double:SharedPosition.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].SharedPosition_Z; }
                tb.AddDataColumn("double:SharedPosition.Z", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToPhaseFilterTableBuilder(this EntitySetBuilder<PhaseFilter> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.PhaseFilter);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].New; }
                tb.AddDataColumn("int:New", columnData);
            }
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Existing; }
                tb.AddDataColumn("int:Existing", columnData);
            }
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Demolished; }
                tb.AddDataColumn("int:Demolished", columnData);
            }
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Temporary; }
                tb.AddDataColumn("int:Temporary", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToGridTableBuilder(this EntitySetBuilder<Grid> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.Grid);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].StartPoint_X; }
                tb.AddDataColumn("double:StartPoint.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].StartPoint_Y; }
                tb.AddDataColumn("double:StartPoint.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].StartPoint_Z; }
                tb.AddDataColumn("double:StartPoint.Z", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].EndPoint_X; }
                tb.AddDataColumn("double:EndPoint.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].EndPoint_Y; }
                tb.AddDataColumn("double:EndPoint.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].EndPoint_Z; }
                tb.AddDataColumn("double:EndPoint.Z", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsCurved; }
                tb.AddDataColumn("byte:IsCurved", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Extents_Min_X; }
                tb.AddDataColumn("double:Extents.Min.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Extents_Min_Y; }
                tb.AddDataColumn("double:Extents.Min.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Extents_Min_Z; }
                tb.AddDataColumn("double:Extents.Min.Z", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Extents_Max_X; }
                tb.AddDataColumn("double:Extents.Max.X", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Extents_Max_Y; }
                tb.AddDataColumn("double:Extents.Max.Y", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Extents_Max_Z; }
                tb.AddDataColumn("double:Extents.Max.Z", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._FamilyType?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.FamilyType:FamilyType", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToAreaTableBuilder(this EntitySetBuilder<Area> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.Area);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Value; }
                tb.AddDataColumn("double:Value", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Perimeter; }
                tb.AddDataColumn("double:Perimeter", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Number; }
                tb.AddStringColumn("string:Number", columnData);
            }
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsGrossInterior; }
                tb.AddDataColumn("byte:IsGrossInterior", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._AreaScheme?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.AreaScheme:AreaScheme", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToAreaSchemeTableBuilder(this EntitySetBuilder<AreaScheme> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.AreaScheme);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Boolean[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].IsGrossBuildingArea; }
                tb.AddDataColumn("byte:IsGrossBuildingArea", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToScheduleTableBuilder(this EntitySetBuilder<Schedule> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.Schedule);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToScheduleColumnTableBuilder(this EntitySetBuilder<ScheduleColumn> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.ScheduleColumn);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Name; }
                tb.AddStringColumn("string:Name", columnData);
            }
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].ColumnIndex; }
                tb.AddDataColumn("int:ColumnIndex", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Schedule?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Schedule:Schedule", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToScheduleCellTableBuilder(this EntitySetBuilder<ScheduleCell> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.ScheduleCell);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Value; }
                tb.AddStringColumn("string:Value", columnData);
            }
            {
                var columnData = new Int32[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].RowIndex; }
                tb.AddDataColumn("int:RowIndex", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._ScheduleColumn?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.ScheduleColumn:ScheduleColumn", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToViewSheetSetTableBuilder(this EntitySetBuilder<ViewSheetSet> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.ViewSheetSet);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToViewSheetTableBuilder(this EntitySetBuilder<ViewSheet> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.ViewSheet);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._FamilyType?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.FamilyType:FamilyType", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToViewSheetInViewSheetSetTableBuilder(this EntitySetBuilder<ViewSheetInViewSheetSet> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.ViewSheetInViewSheetSet);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._ViewSheet?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.ViewSheet:ViewSheet", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._ViewSheetSet?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.ViewSheetSet:ViewSheetSet", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToViewInViewSheetSetTableBuilder(this EntitySetBuilder<ViewInViewSheetSet> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.ViewInViewSheetSet);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._View?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.View:View", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._ViewSheetSet?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.ViewSheetSet:ViewSheetSet", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToViewInViewSheetTableBuilder(this EntitySetBuilder<ViewInViewSheet> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.ViewInViewSheet);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._View?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.View:View", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._ViewSheet?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.ViewSheet:ViewSheet", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToSiteTableBuilder(this EntitySetBuilder<Site> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.Site);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Latitude; }
                tb.AddDataColumn("double:Latitude", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Longitude; }
                tb.AddDataColumn("double:Longitude", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Address; }
                tb.AddStringColumn("string:Address", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Elevation; }
                tb.AddDataColumn("double:Elevation", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Number; }
                tb.AddStringColumn("string:Number", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
        public static EntityTableBuilder ToBuildingTableBuilder(this EntitySetBuilder<Building> entitySet)
        {
            var tb = new EntityTableBuilder(TableNames.Building);
            var entities = entitySet.Entities;
            var entityCount = entities.Count;
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Elevation; }
                tb.AddDataColumn("double:Elevation", columnData);
            }
            {
                var columnData = new Double[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].TerrainElevation; }
                tb.AddDataColumn("double:TerrainElevation", columnData);
            }
            {
                var columnData = new String[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i].Address; }
                tb.AddStringColumn("string:Address", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Site?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Site:Site", columnData);
            }
            {
                var columnData = new int[entityCount];
                for (var i = 0; i < columnData.Length; ++i) { columnData[i] = entities[i]._Element?.Index ?? EntityRelation.None; }
                tb.AddIndexColumn("index:Vim.Element:Element", columnData);
            }
            return tb;
        }
    } // DocumentBuilderExtensions
    
    public partial class ObjectModelBuilder
    {
        public readonly EntitySetBuilder<Asset> AssetBuilder = new EntitySetBuilder<Asset>(TableNames.Asset);
        public readonly EntitySetBuilder<DisplayUnit> DisplayUnitBuilder = new EntitySetBuilder<DisplayUnit>(TableNames.DisplayUnit);
        public readonly EntitySetBuilder<ParameterDescriptor> ParameterDescriptorBuilder = new EntitySetBuilder<ParameterDescriptor>(TableNames.ParameterDescriptor);
        public readonly EntitySetBuilder<Parameter> ParameterBuilder = new EntitySetBuilder<Parameter>(TableNames.Parameter);
        public readonly EntitySetBuilder<Element> ElementBuilder = new EntitySetBuilder<Element>(TableNames.Element);
        public readonly EntitySetBuilder<Workset> WorksetBuilder = new EntitySetBuilder<Workset>(TableNames.Workset);
        public readonly EntitySetBuilder<AssemblyInstance> AssemblyInstanceBuilder = new EntitySetBuilder<AssemblyInstance>(TableNames.AssemblyInstance);
        public readonly EntitySetBuilder<Group> GroupBuilder = new EntitySetBuilder<Group>(TableNames.Group);
        public readonly EntitySetBuilder<DesignOption> DesignOptionBuilder = new EntitySetBuilder<DesignOption>(TableNames.DesignOption);
        public readonly EntitySetBuilder<Level> LevelBuilder = new EntitySetBuilder<Level>(TableNames.Level);
        public readonly EntitySetBuilder<Phase> PhaseBuilder = new EntitySetBuilder<Phase>(TableNames.Phase);
        public readonly EntitySetBuilder<Room> RoomBuilder = new EntitySetBuilder<Room>(TableNames.Room);
        public readonly EntitySetBuilder<BimDocument> BimDocumentBuilder = new EntitySetBuilder<BimDocument>(TableNames.BimDocument);
        public readonly EntitySetBuilder<DisplayUnitInBimDocument> DisplayUnitInBimDocumentBuilder = new EntitySetBuilder<DisplayUnitInBimDocument>(TableNames.DisplayUnitInBimDocument);
        public readonly EntitySetBuilder<PhaseOrderInBimDocument> PhaseOrderInBimDocumentBuilder = new EntitySetBuilder<PhaseOrderInBimDocument>(TableNames.PhaseOrderInBimDocument);
        public readonly EntitySetBuilder<Category> CategoryBuilder = new EntitySetBuilder<Category>(TableNames.Category);
        public readonly EntitySetBuilder<Family> FamilyBuilder = new EntitySetBuilder<Family>(TableNames.Family);
        public readonly EntitySetBuilder<FamilyType> FamilyTypeBuilder = new EntitySetBuilder<FamilyType>(TableNames.FamilyType);
        public readonly EntitySetBuilder<FamilyInstance> FamilyInstanceBuilder = new EntitySetBuilder<FamilyInstance>(TableNames.FamilyInstance);
        public readonly EntitySetBuilder<View> ViewBuilder = new EntitySetBuilder<View>(TableNames.View);
        public readonly EntitySetBuilder<ElementInView> ElementInViewBuilder = new EntitySetBuilder<ElementInView>(TableNames.ElementInView);
        public readonly EntitySetBuilder<ShapeInView> ShapeInViewBuilder = new EntitySetBuilder<ShapeInView>(TableNames.ShapeInView);
        public readonly EntitySetBuilder<AssetInView> AssetInViewBuilder = new EntitySetBuilder<AssetInView>(TableNames.AssetInView);
        public readonly EntitySetBuilder<AssetInViewSheet> AssetInViewSheetBuilder = new EntitySetBuilder<AssetInViewSheet>(TableNames.AssetInViewSheet);
        public readonly EntitySetBuilder<LevelInView> LevelInViewBuilder = new EntitySetBuilder<LevelInView>(TableNames.LevelInView);
        public readonly EntitySetBuilder<Camera> CameraBuilder = new EntitySetBuilder<Camera>(TableNames.Camera);
        public readonly EntitySetBuilder<Material> MaterialBuilder = new EntitySetBuilder<Material>(TableNames.Material);
        public readonly EntitySetBuilder<MaterialInElement> MaterialInElementBuilder = new EntitySetBuilder<MaterialInElement>(TableNames.MaterialInElement);
        public readonly EntitySetBuilder<CompoundStructureLayer> CompoundStructureLayerBuilder = new EntitySetBuilder<CompoundStructureLayer>(TableNames.CompoundStructureLayer);
        public readonly EntitySetBuilder<CompoundStructure> CompoundStructureBuilder = new EntitySetBuilder<CompoundStructure>(TableNames.CompoundStructure);
        public readonly EntitySetBuilder<Node> NodeBuilder = new EntitySetBuilder<Node>(TableNames.Node);
        public readonly EntitySetBuilder<Geometry> GeometryBuilder = new EntitySetBuilder<Geometry>(TableNames.Geometry);
        public readonly EntitySetBuilder<Shape> ShapeBuilder = new EntitySetBuilder<Shape>(TableNames.Shape);
        public readonly EntitySetBuilder<ShapeCollection> ShapeCollectionBuilder = new EntitySetBuilder<ShapeCollection>(TableNames.ShapeCollection);
        public readonly EntitySetBuilder<ShapeInShapeCollection> ShapeInShapeCollectionBuilder = new EntitySetBuilder<ShapeInShapeCollection>(TableNames.ShapeInShapeCollection);
        public readonly EntitySetBuilder<System> SystemBuilder = new EntitySetBuilder<System>(TableNames.System);
        public readonly EntitySetBuilder<ElementInSystem> ElementInSystemBuilder = new EntitySetBuilder<ElementInSystem>(TableNames.ElementInSystem);
        public readonly EntitySetBuilder<Warning> WarningBuilder = new EntitySetBuilder<Warning>(TableNames.Warning);
        public readonly EntitySetBuilder<ElementInWarning> ElementInWarningBuilder = new EntitySetBuilder<ElementInWarning>(TableNames.ElementInWarning);
        public readonly EntitySetBuilder<BasePoint> BasePointBuilder = new EntitySetBuilder<BasePoint>(TableNames.BasePoint);
        public readonly EntitySetBuilder<PhaseFilter> PhaseFilterBuilder = new EntitySetBuilder<PhaseFilter>(TableNames.PhaseFilter);
        public readonly EntitySetBuilder<Grid> GridBuilder = new EntitySetBuilder<Grid>(TableNames.Grid);
        public readonly EntitySetBuilder<Area> AreaBuilder = new EntitySetBuilder<Area>(TableNames.Area);
        public readonly EntitySetBuilder<AreaScheme> AreaSchemeBuilder = new EntitySetBuilder<AreaScheme>(TableNames.AreaScheme);
        public readonly EntitySetBuilder<Schedule> ScheduleBuilder = new EntitySetBuilder<Schedule>(TableNames.Schedule);
        public readonly EntitySetBuilder<ScheduleColumn> ScheduleColumnBuilder = new EntitySetBuilder<ScheduleColumn>(TableNames.ScheduleColumn);
        public readonly EntitySetBuilder<ScheduleCell> ScheduleCellBuilder = new EntitySetBuilder<ScheduleCell>(TableNames.ScheduleCell);
        public readonly EntitySetBuilder<ViewSheetSet> ViewSheetSetBuilder = new EntitySetBuilder<ViewSheetSet>(TableNames.ViewSheetSet);
        public readonly EntitySetBuilder<ViewSheet> ViewSheetBuilder = new EntitySetBuilder<ViewSheet>(TableNames.ViewSheet);
        public readonly EntitySetBuilder<ViewSheetInViewSheetSet> ViewSheetInViewSheetSetBuilder = new EntitySetBuilder<ViewSheetInViewSheetSet>(TableNames.ViewSheetInViewSheetSet);
        public readonly EntitySetBuilder<ViewInViewSheetSet> ViewInViewSheetSetBuilder = new EntitySetBuilder<ViewInViewSheetSet>(TableNames.ViewInViewSheetSet);
        public readonly EntitySetBuilder<ViewInViewSheet> ViewInViewSheetBuilder = new EntitySetBuilder<ViewInViewSheet>(TableNames.ViewInViewSheet);
        public readonly EntitySetBuilder<Site> SiteBuilder = new EntitySetBuilder<Site>(TableNames.Site);
        public readonly EntitySetBuilder<Building> BuildingBuilder = new EntitySetBuilder<Building>(TableNames.Building);
        
        public DocumentBuilder AddEntityTableSets(DocumentBuilder db)
        {
            db.Tables.Add(AssetBuilder.EntityTableName, AssetBuilder.ToAssetTableBuilder());
            db.Tables.Add(DisplayUnitBuilder.EntityTableName, DisplayUnitBuilder.ToDisplayUnitTableBuilder());
            db.Tables.Add(ParameterDescriptorBuilder.EntityTableName, ParameterDescriptorBuilder.ToParameterDescriptorTableBuilder());
            db.Tables.Add(ParameterBuilder.EntityTableName, ParameterBuilder.ToParameterTableBuilder());
            db.Tables.Add(ElementBuilder.EntityTableName, ElementBuilder.ToElementTableBuilder());
            db.Tables.Add(WorksetBuilder.EntityTableName, WorksetBuilder.ToWorksetTableBuilder());
            db.Tables.Add(AssemblyInstanceBuilder.EntityTableName, AssemblyInstanceBuilder.ToAssemblyInstanceTableBuilder());
            db.Tables.Add(GroupBuilder.EntityTableName, GroupBuilder.ToGroupTableBuilder());
            db.Tables.Add(DesignOptionBuilder.EntityTableName, DesignOptionBuilder.ToDesignOptionTableBuilder());
            db.Tables.Add(LevelBuilder.EntityTableName, LevelBuilder.ToLevelTableBuilder());
            db.Tables.Add(PhaseBuilder.EntityTableName, PhaseBuilder.ToPhaseTableBuilder());
            db.Tables.Add(RoomBuilder.EntityTableName, RoomBuilder.ToRoomTableBuilder());
            db.Tables.Add(BimDocumentBuilder.EntityTableName, BimDocumentBuilder.ToBimDocumentTableBuilder());
            db.Tables.Add(DisplayUnitInBimDocumentBuilder.EntityTableName, DisplayUnitInBimDocumentBuilder.ToDisplayUnitInBimDocumentTableBuilder());
            db.Tables.Add(PhaseOrderInBimDocumentBuilder.EntityTableName, PhaseOrderInBimDocumentBuilder.ToPhaseOrderInBimDocumentTableBuilder());
            db.Tables.Add(CategoryBuilder.EntityTableName, CategoryBuilder.ToCategoryTableBuilder());
            db.Tables.Add(FamilyBuilder.EntityTableName, FamilyBuilder.ToFamilyTableBuilder());
            db.Tables.Add(FamilyTypeBuilder.EntityTableName, FamilyTypeBuilder.ToFamilyTypeTableBuilder());
            db.Tables.Add(FamilyInstanceBuilder.EntityTableName, FamilyInstanceBuilder.ToFamilyInstanceTableBuilder());
            db.Tables.Add(ViewBuilder.EntityTableName, ViewBuilder.ToViewTableBuilder());
            db.Tables.Add(ElementInViewBuilder.EntityTableName, ElementInViewBuilder.ToElementInViewTableBuilder());
            db.Tables.Add(ShapeInViewBuilder.EntityTableName, ShapeInViewBuilder.ToShapeInViewTableBuilder());
            db.Tables.Add(AssetInViewBuilder.EntityTableName, AssetInViewBuilder.ToAssetInViewTableBuilder());
            db.Tables.Add(AssetInViewSheetBuilder.EntityTableName, AssetInViewSheetBuilder.ToAssetInViewSheetTableBuilder());
            db.Tables.Add(LevelInViewBuilder.EntityTableName, LevelInViewBuilder.ToLevelInViewTableBuilder());
            db.Tables.Add(CameraBuilder.EntityTableName, CameraBuilder.ToCameraTableBuilder());
            db.Tables.Add(MaterialBuilder.EntityTableName, MaterialBuilder.ToMaterialTableBuilder());
            db.Tables.Add(MaterialInElementBuilder.EntityTableName, MaterialInElementBuilder.ToMaterialInElementTableBuilder());
            db.Tables.Add(CompoundStructureLayerBuilder.EntityTableName, CompoundStructureLayerBuilder.ToCompoundStructureLayerTableBuilder());
            db.Tables.Add(CompoundStructureBuilder.EntityTableName, CompoundStructureBuilder.ToCompoundStructureTableBuilder());
            db.Tables.Add(NodeBuilder.EntityTableName, NodeBuilder.ToNodeTableBuilder());
            db.Tables.Add(GeometryBuilder.EntityTableName, GeometryBuilder.ToGeometryTableBuilder());
            db.Tables.Add(ShapeBuilder.EntityTableName, ShapeBuilder.ToShapeTableBuilder());
            db.Tables.Add(ShapeCollectionBuilder.EntityTableName, ShapeCollectionBuilder.ToShapeCollectionTableBuilder());
            db.Tables.Add(ShapeInShapeCollectionBuilder.EntityTableName, ShapeInShapeCollectionBuilder.ToShapeInShapeCollectionTableBuilder());
            db.Tables.Add(SystemBuilder.EntityTableName, SystemBuilder.ToSystemTableBuilder());
            db.Tables.Add(ElementInSystemBuilder.EntityTableName, ElementInSystemBuilder.ToElementInSystemTableBuilder());
            db.Tables.Add(WarningBuilder.EntityTableName, WarningBuilder.ToWarningTableBuilder());
            db.Tables.Add(ElementInWarningBuilder.EntityTableName, ElementInWarningBuilder.ToElementInWarningTableBuilder());
            db.Tables.Add(BasePointBuilder.EntityTableName, BasePointBuilder.ToBasePointTableBuilder());
            db.Tables.Add(PhaseFilterBuilder.EntityTableName, PhaseFilterBuilder.ToPhaseFilterTableBuilder());
            db.Tables.Add(GridBuilder.EntityTableName, GridBuilder.ToGridTableBuilder());
            db.Tables.Add(AreaBuilder.EntityTableName, AreaBuilder.ToAreaTableBuilder());
            db.Tables.Add(AreaSchemeBuilder.EntityTableName, AreaSchemeBuilder.ToAreaSchemeTableBuilder());
            db.Tables.Add(ScheduleBuilder.EntityTableName, ScheduleBuilder.ToScheduleTableBuilder());
            db.Tables.Add(ScheduleColumnBuilder.EntityTableName, ScheduleColumnBuilder.ToScheduleColumnTableBuilder());
            db.Tables.Add(ScheduleCellBuilder.EntityTableName, ScheduleCellBuilder.ToScheduleCellTableBuilder());
            db.Tables.Add(ViewSheetSetBuilder.EntityTableName, ViewSheetSetBuilder.ToViewSheetSetTableBuilder());
            db.Tables.Add(ViewSheetBuilder.EntityTableName, ViewSheetBuilder.ToViewSheetTableBuilder());
            db.Tables.Add(ViewSheetInViewSheetSetBuilder.EntityTableName, ViewSheetInViewSheetSetBuilder.ToViewSheetInViewSheetSetTableBuilder());
            db.Tables.Add(ViewInViewSheetSetBuilder.EntityTableName, ViewInViewSheetSetBuilder.ToViewInViewSheetSetTableBuilder());
            db.Tables.Add(ViewInViewSheetBuilder.EntityTableName, ViewInViewSheetBuilder.ToViewInViewSheetTableBuilder());
            db.Tables.Add(SiteBuilder.EntityTableName, SiteBuilder.ToSiteTableBuilder());
            db.Tables.Add(BuildingBuilder.EntityTableName, BuildingBuilder.ToBuildingTableBuilder());
            
            return db;
        } // AddEntityTableSets
        
        public void Clear()
        {
            AssetBuilder.Clear();
            DisplayUnitBuilder.Clear();
            ParameterDescriptorBuilder.Clear();
            ParameterBuilder.Clear();
            ElementBuilder.Clear();
            WorksetBuilder.Clear();
            AssemblyInstanceBuilder.Clear();
            GroupBuilder.Clear();
            DesignOptionBuilder.Clear();
            LevelBuilder.Clear();
            PhaseBuilder.Clear();
            RoomBuilder.Clear();
            BimDocumentBuilder.Clear();
            DisplayUnitInBimDocumentBuilder.Clear();
            PhaseOrderInBimDocumentBuilder.Clear();
            CategoryBuilder.Clear();
            FamilyBuilder.Clear();
            FamilyTypeBuilder.Clear();
            FamilyInstanceBuilder.Clear();
            ViewBuilder.Clear();
            ElementInViewBuilder.Clear();
            ShapeInViewBuilder.Clear();
            AssetInViewBuilder.Clear();
            AssetInViewSheetBuilder.Clear();
            LevelInViewBuilder.Clear();
            CameraBuilder.Clear();
            MaterialBuilder.Clear();
            MaterialInElementBuilder.Clear();
            CompoundStructureLayerBuilder.Clear();
            CompoundStructureBuilder.Clear();
            NodeBuilder.Clear();
            GeometryBuilder.Clear();
            ShapeBuilder.Clear();
            ShapeCollectionBuilder.Clear();
            ShapeInShapeCollectionBuilder.Clear();
            SystemBuilder.Clear();
            ElementInSystemBuilder.Clear();
            WarningBuilder.Clear();
            ElementInWarningBuilder.Clear();
            BasePointBuilder.Clear();
            PhaseFilterBuilder.Clear();
            GridBuilder.Clear();
            AreaBuilder.Clear();
            AreaSchemeBuilder.Clear();
            ScheduleBuilder.Clear();
            ScheduleColumnBuilder.Clear();
            ScheduleCellBuilder.Clear();
            ViewSheetSetBuilder.Clear();
            ViewSheetBuilder.Clear();
            ViewSheetInViewSheetSetBuilder.Clear();
            ViewInViewSheetSetBuilder.Clear();
            ViewInViewSheetBuilder.Clear();
            SiteBuilder.Clear();
            BuildingBuilder.Clear();
        } // Clear
    } // ObjectModelBuilder
} // namespace
