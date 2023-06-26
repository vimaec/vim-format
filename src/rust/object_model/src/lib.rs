use std::str;
use object_model_macro_derive::vim_schema;

#[vim_schema("Vim.AssemblyInstance__float:Position.X,Vim.AssemblyInstance__float:Position.Y,Vim.AssemblyInstance__float:Position.Z,Vim.AssemblyInstance__index:Vim.Element:Element,Vim.AssemblyInstance__string:AssemblyTypeName,Vim.Asset__string:BufferName,Vim.AssetInView__index:Vim.Asset:Asset,Vim.AssetInView__index:Vim.View:View,Vim.BasePoint__byte:IsSurveyPoint,Vim.BasePoint__double:Position.X,Vim.BasePoint__double:Position.Y,Vim.BasePoint__double:Position.Z,Vim.BasePoint__double:SharedPosition.X,Vim.BasePoint__double:SharedPosition.Y,Vim.BasePoint__double:SharedPosition.Z,Vim.BasePoint__index:Vim.Element:Element,Vim.BimDocument__byte:IsDetached,Vim.BimDocument__byte:IsLinked,Vim.BimDocument__byte:IsMetric,Vim.BimDocument__byte:IsWorkshared,Vim.BimDocument__double:Elevation,Vim.BimDocument__double:Latitude,Vim.BimDocument__double:Longitude,Vim.BimDocument__double:TimeZone,Vim.BimDocument__index:Vim.BimDocument:Parent,Vim.BimDocument__index:Vim.Element:Element,Vim.BimDocument__index:Vim.Family:OwnerFamily,Vim.BimDocument__index:Vim.View:ActiveView,Vim.BimDocument__int:NumSaves,Vim.BimDocument__string:Address,Vim.BimDocument__string:Author,Vim.BimDocument__string:BuildingName,Vim.BimDocument__string:ClientName,Vim.BimDocument__string:Guid,Vim.BimDocument__string:IssueDate,Vim.BimDocument__string:Name,Vim.BimDocument__string:Number,Vim.BimDocument__string:OrganizationDescription,Vim.BimDocument__string:OrganizationName,Vim.BimDocument__string:PathName,Vim.BimDocument__string:PlaceName,Vim.BimDocument__string:Product,Vim.BimDocument__string:ProjectLocation,Vim.BimDocument__string:Status,Vim.BimDocument__string:Title,Vim.BimDocument__string:User,Vim.BimDocument__string:Version,Vim.BimDocument__string:WeatherStationName,Vim.Camera__double:FarDistance,Vim.Camera__double:HorizontalExtent,Vim.Camera__double:NearDistance,Vim.Camera__double:RightOffset,Vim.Camera__double:TargetDistance,Vim.Camera__double:UpOffset,Vim.Camera__double:VerticalExtent,Vim.Camera__int:Id,Vim.Camera__int:IsPerspective,Vim.Category__double:LineColor.X,Vim.Category__double:LineColor.Y,Vim.Category__double:LineColor.Z,Vim.Category__index:Vim.Category:Parent,Vim.Category__index:Vim.Material:Material,Vim.Category__int:Id,Vim.Category__string:BuiltInCategory,Vim.Category__string:CategoryType,Vim.Category__string:Name,Vim.CompoundStructure__double:Width,Vim.CompoundStructure__index:Vim.CompoundStructureLayer:StructuralLayer,Vim.CompoundStructureLayer__double:Width,Vim.CompoundStructureLayer__index:Vim.CompoundStructure:CompoundStructure,Vim.CompoundStructureLayer__index:Vim.Material:Material,Vim.CompoundStructureLayer__int:OrderIndex,Vim.CompoundStructureLayer__string:MaterialFunctionAssignment,Vim.DesignOption__byte:IsPrimary,Vim.DesignOption__index:Vim.Element:Element,Vim.DisplayUnit__string:Label,Vim.DisplayUnit__string:Spec,Vim.DisplayUnit__string:Type,Vim.DisplayUnitInBimDocument__index:Vim.BimDocument:BimDocument,Vim.DisplayUnitInBimDocument__index:Vim.DisplayUnit:DisplayUnit,Vim.Element__byte:IsPinned,Vim.Element__float:Location.X,Vim.Element__float:Location.Y,Vim.Element__float:Location.Z,Vim.Element__index:Vim.AssemblyInstance:AssemblyInstance,Vim.Element__index:Vim.BimDocument:BimDocument,Vim.Element__index:Vim.Category:Category,Vim.Element__index:Vim.DesignOption:DesignOption,Vim.Element__index:Vim.Group:Group,Vim.Element__index:Vim.Level:Level,Vim.Element__index:Vim.Phase:PhaseCreated,Vim.Element__index:Vim.Phase:PhaseDemolished,Vim.Element__index:Vim.Room:Room,Vim.Element__index:Vim.View:OwnerView,Vim.Element__index:Vim.Workset:Workset,Vim.Element__int:Id,Vim.Element__string:FamilyName,Vim.Element__string:Name,Vim.Element__string:Type,Vim.Element__string:UniqueId,Vim.ElementInSystem__index:Vim.Element:Element,Vim.ElementInSystem__index:Vim.System:System,Vim.ElementInSystem__int:Roles,Vim.ElementInView__index:Vim.Element:Element,Vim.ElementInView__index:Vim.View:View,Vim.ElementInWarning__index:Vim.Element:Element,Vim.ElementInWarning__index:Vim.Warning:Warning,Vim.Family__byte:IsInPlace,Vim.Family__byte:IsSystemFamily,Vim.Family__index:Vim.Category:FamilyCategory,Vim.Family__index:Vim.Element:Element,Vim.Family__string:StructuralMaterialType,Vim.Family__string:StructuralSectionShape,Vim.FamilyInstance__byte:FacingFlipped,Vim.FamilyInstance__byte:HandFlipped,Vim.FamilyInstance__byte:HasModifiedGeometry,Vim.FamilyInstance__byte:Mirrored,Vim.FamilyInstance__float:BasisX.X,Vim.FamilyInstance__float:BasisX.Y,Vim.FamilyInstance__float:BasisX.Z,Vim.FamilyInstance__float:BasisY.X,Vim.FamilyInstance__float:BasisY.Y,Vim.FamilyInstance__float:BasisY.Z,Vim.FamilyInstance__float:BasisZ.X,Vim.FamilyInstance__float:BasisZ.Y,Vim.FamilyInstance__float:BasisZ.Z,Vim.FamilyInstance__float:FacingOrientation.X,Vim.FamilyInstance__float:FacingOrientation.Y,Vim.FamilyInstance__float:FacingOrientation.Z,Vim.FamilyInstance__float:HandOrientation.X,Vim.FamilyInstance__float:HandOrientation.Y,Vim.FamilyInstance__float:HandOrientation.Z,Vim.FamilyInstance__float:Scale,Vim.FamilyInstance__float:Translation.X,Vim.FamilyInstance__float:Translation.Y,Vim.FamilyInstance__float:Translation.Z,Vim.FamilyInstance__index:Vim.Element:Element,Vim.FamilyInstance__index:Vim.Element:Host,Vim.FamilyInstance__index:Vim.FamilyType:FamilyType,Vim.FamilyInstance__index:Vim.Room:FromRoom,Vim.FamilyInstance__index:Vim.Room:ToRoom,Vim.FamilyType__byte:IsSystemFamilyType,Vim.FamilyType__index:Vim.CompoundStructure:CompoundStructure,Vim.FamilyType__index:Vim.Element:Element,Vim.FamilyType__index:Vim.Family:Family,Vim.Geometry__float:Box.Max.X,Vim.Geometry__float:Box.Max.Y,Vim.Geometry__float:Box.Max.Z,Vim.Geometry__float:Box.Min.X,Vim.Geometry__float:Box.Min.Y,Vim.Geometry__float:Box.Min.Z,Vim.Geometry__int:FaceCount,Vim.Geometry__int:VertexCount,Vim.Group__float:Position.X,Vim.Group__float:Position.Y,Vim.Group__float:Position.Z,Vim.Group__index:Vim.Element:Element,Vim.Group__string:GroupType,Vim.Level__double:Elevation,Vim.Level__index:Vim.Element:Element,Vim.Material__double:Color.X,Vim.Material__double:Color.Y,Vim.Material__double:Color.Z,Vim.Material__double:ColorUvOffset.X,Vim.Material__double:ColorUvOffset.Y,Vim.Material__double:ColorUvScaling.X,Vim.Material__double:ColorUvScaling.Y,Vim.Material__double:Glossiness,Vim.Material__double:NormalAmount,Vim.Material__double:NormalUvOffset.X,Vim.Material__double:NormalUvOffset.Y,Vim.Material__double:NormalUvScaling.X,Vim.Material__double:NormalUvScaling.Y,Vim.Material__double:Smoothness,Vim.Material__double:Transparency,Vim.Material__index:Vim.Asset:ColorTextureFile,Vim.Material__index:Vim.Asset:NormalTextureFile,Vim.Material__index:Vim.Element:Element,Vim.Material__string:MaterialCategory,Vim.Material__string:Name,Vim.MaterialInElement__byte:IsPaint,Vim.MaterialInElement__double:Area,Vim.MaterialInElement__double:Volume,Vim.MaterialInElement__index:Vim.Element:Element,Vim.MaterialInElement__index:Vim.Material:Material,Vim.Node__index:Vim.Element:Element,Vim.Parameter__index:Vim.Element:Element,Vim.Parameter__index:Vim.ParameterDescriptor:ParameterDescriptor,Vim.Parameter__string:Value,Vim.ParameterDescriptor__byte:IsInstance,Vim.ParameterDescriptor__byte:IsReadOnly,Vim.ParameterDescriptor__byte:IsShared,Vim.ParameterDescriptor__index:Vim.DisplayUnit:DisplayUnit,Vim.ParameterDescriptor__int:Flags,Vim.ParameterDescriptor__string:Group,Vim.ParameterDescriptor__string:Guid,Vim.ParameterDescriptor__string:Name,Vim.ParameterDescriptor__string:ParameterType,Vim.Phase__index:Vim.Element:Element,Vim.PhaseOrderInBimDocument__index:Vim.BimDocument:BimDocument,Vim.PhaseOrderInBimDocument__index:Vim.Phase:Phase,Vim.PhaseOrderInBimDocument__int:OrderIndex,Vim.Room__double:Area,Vim.Room__double:BaseOffset,Vim.Room__double:LimitOffset,Vim.Room__double:Perimeter,Vim.Room__double:UnboundedHeight,Vim.Room__double:Volume,Vim.Room__index:Vim.Element:Element,Vim.Room__index:Vim.Level:UpperLimit,Vim.Room__string:Number,Vim.Shape__index:Vim.Element:Element,Vim.ShapeCollection__index:Vim.Element:Element,Vim.ShapeInShapeCollection__index:Vim.Shape:Shape,Vim.ShapeInShapeCollection__index:Vim.ShapeCollection:ShapeCollection,Vim.ShapeInView__index:Vim.Shape:Shape,Vim.ShapeInView__index:Vim.View:View,Vim.System__index:Vim.Element:Element,Vim.System__int:SystemType,Vim.View__double:Origin.X,Vim.View__double:Origin.Y,Vim.View__double:Origin.Z,Vim.View__double:Outline.Max.X,Vim.View__double:Outline.Max.Y,Vim.View__double:Outline.Min.X,Vim.View__double:Outline.Min.Y,Vim.View__double:Right.X,Vim.View__double:Right.Y,Vim.View__double:Right.Z,Vim.View__double:Scale,Vim.View__double:Up.X,Vim.View__double:Up.Y,Vim.View__double:Up.Z,Vim.View__double:ViewDirection.X,Vim.View__double:ViewDirection.Y,Vim.View__double:ViewDirection.Z,Vim.View__double:ViewPosition.X,Vim.View__double:ViewPosition.Y,Vim.View__double:ViewPosition.Z,Vim.View__index:Vim.Camera:Camera,Vim.View__index:Vim.Element:Element,Vim.View__int:DetailLevel,Vim.View__string:Title,Vim.View__string:ViewType,Vim.Warning__index:Vim.BimDocument:BimDocument,Vim.Warning__string:Description,Vim.Warning__string:Guid,Vim.Warning__string:Severity,Vim.Workset__byte:IsEditable,Vim.Workset__byte:IsOpen,Vim.Workset__index:Vim.BimDocument:BimDocument,Vim.Workset__int:Id,Vim.Workset__string:Kind,Vim.Workset__string:Name,Vim.Workset__string:Owner,Vim.Workset__string:UniqueId")]
pub struct DocumentModel {}
