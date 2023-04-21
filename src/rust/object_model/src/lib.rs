use std::{str, collections::HashMap};
use object_model_macro_derive::vim_schema;

#[vim_schema("Vim.AssemblyInstance__float:Position.X,Vim.AssemblyInstance__float:Position.Y,Vim.AssemblyInstance__float:Position.Z,Vim.AssemblyInstance__index:Vim.Element:Element,Vim.AssemblyInstance__string:AssemblyTypeName,Vim.Asset__string:BufferName,Vim.AssetInView__index:Vim.Asset:Asset,Vim.AssetInView__index:Vim.View:View,Vim.BasePoint__byte:IsSurveyPoint,Vim.BasePoint__double:Position.X,Vim.BasePoint__double:Position.Y,Vim.BasePoint__double:Position.Z,Vim.BasePoint__double:SharedPosition.X,Vim.BasePoint__double:SharedPosition.Y,Vim.BasePoint__double:SharedPosition.Z,Vim.BasePoint__index:Vim.Element:Element,Vim.BimDocument__byte:IsDetached,Vim.BimDocument__byte:IsLinked,Vim.BimDocument__byte:IsMetric,Vim.BimDocument__byte:IsWorkshared,Vim.BimDocument__double:Elevation,Vim.BimDocument__double:Latitude,Vim.BimDocument__double:Longitude,Vim.BimDocument__double:TimeZone,Vim.BimDocument__index:Vim.BimDocument:Parent,Vim.BimDocument__index:Vim.Element:Element,Vim.BimDocument__index:Vim.Family:OwnerFamily,Vim.BimDocument__index:Vim.View:ActiveView,Vim.BimDocument__int:NumSaves,Vim.BimDocument__string:Address,Vim.BimDocument__string:Author,Vim.BimDocument__string:BuildingName,Vim.BimDocument__string:ClientName,Vim.BimDocument__string:Guid,Vim.BimDocument__string:IssueDate,Vim.BimDocument__string:Name,Vim.BimDocument__string:Number,Vim.BimDocument__string:OrganizationDescription,Vim.BimDocument__string:OrganizationName,Vim.BimDocument__string:PathName,Vim.BimDocument__string:PlaceName,Vim.BimDocument__string:Product,Vim.BimDocument__string:ProjectLocation,Vim.BimDocument__string:Status,Vim.BimDocument__string:Title,Vim.BimDocument__string:User,Vim.BimDocument__string:Version,Vim.BimDocument__string:WeatherStationName,Vim.Camera__double:FarDistance,Vim.Camera__double:HorizontalExtent,Vim.Camera__double:NearDistance,Vim.Camera__double:RightOffset,Vim.Camera__double:TargetDistance,Vim.Camera__double:UpOffset,Vim.Camera__double:VerticalExtent,Vim.Camera__int:Id,Vim.Camera__int:IsPerspective,Vim.Category__double:LineColor.X,Vim.Category__double:LineColor.Y,Vim.Category__double:LineColor.Z,Vim.Category__index:Vim.Category:Parent,Vim.Category__index:Vim.Material:Material,Vim.Category__int:Id,Vim.Category__string:BuiltInCategory,Vim.Category__string:CategoryType,Vim.Category__string:Name,Vim.CompoundStructure__double:Width,Vim.CompoundStructure__index:Vim.CompoundStructureLayer:StructuralLayer,Vim.CompoundStructureLayer__double:Width,Vim.CompoundStructureLayer__index:Vim.CompoundStructure:CompoundStructure,Vim.CompoundStructureLayer__index:Vim.Material:Material,Vim.CompoundStructureLayer__int:OrderIndex,Vim.CompoundStructureLayer__string:MaterialFunctionAssignment,Vim.DesignOption__byte:IsPrimary,Vim.DesignOption__index:Vim.Element:Element,Vim.DisplayUnit__string:Label,Vim.DisplayUnit__string:Spec,Vim.DisplayUnit__string:Type,Vim.DisplayUnitInBimDocument__index:Vim.BimDocument:BimDocument,Vim.DisplayUnitInBimDocument__index:Vim.DisplayUnit:DisplayUnit,Vim.Element__byte:IsPinned,Vim.Element__float:Location.X,Vim.Element__float:Location.Y,Vim.Element__float:Location.Z,Vim.Element__index:Vim.AssemblyInstance:AssemblyInstance,Vim.Element__index:Vim.BimDocument:BimDocument,Vim.Element__index:Vim.Category:Category,Vim.Element__index:Vim.DesignOption:DesignOption,Vim.Element__index:Vim.Group:Group,Vim.Element__index:Vim.Level:Level,Vim.Element__index:Vim.Phase:PhaseCreated,Vim.Element__index:Vim.Phase:PhaseDemolished,Vim.Element__index:Vim.Room:Room,Vim.Element__index:Vim.View:OwnerView,Vim.Element__index:Vim.Workset:Workset,Vim.Element__int:Id,Vim.Element__string:FamilyName,Vim.Element__string:Name,Vim.Element__string:Type,Vim.Element__string:UniqueId,Vim.ElementInSystem__index:Vim.Element:Element,Vim.ElementInSystem__index:Vim.System:System,Vim.ElementInSystem__int:Roles,Vim.ElementInView__index:Vim.Element:Element,Vim.ElementInView__index:Vim.View:View,Vim.ElementInWarning__index:Vim.Element:Element,Vim.ElementInWarning__index:Vim.Warning:Warning,Vim.Family__byte:IsInPlace,Vim.Family__byte:IsSystemFamily,Vim.Family__index:Vim.Category:FamilyCategory,Vim.Family__index:Vim.Element:Element,Vim.Family__string:StructuralMaterialType,Vim.Family__string:StructuralSectionShape,Vim.FamilyInstance__byte:FacingFlipped,Vim.FamilyInstance__byte:HandFlipped,Vim.FamilyInstance__byte:HasModifiedGeometry,Vim.FamilyInstance__byte:Mirrored,Vim.FamilyInstance__float:BasisX.X,Vim.FamilyInstance__float:BasisX.Y,Vim.FamilyInstance__float:BasisX.Z,Vim.FamilyInstance__float:BasisY.X,Vim.FamilyInstance__float:BasisY.Y,Vim.FamilyInstance__float:BasisY.Z,Vim.FamilyInstance__float:BasisZ.X,Vim.FamilyInstance__float:BasisZ.Y,Vim.FamilyInstance__float:BasisZ.Z,Vim.FamilyInstance__float:FacingOrientation.X,Vim.FamilyInstance__float:FacingOrientation.Y,Vim.FamilyInstance__float:FacingOrientation.Z,Vim.FamilyInstance__float:HandOrientation.X,Vim.FamilyInstance__float:HandOrientation.Y,Vim.FamilyInstance__float:HandOrientation.Z,Vim.FamilyInstance__float:Scale,Vim.FamilyInstance__float:Translation.X,Vim.FamilyInstance__float:Translation.Y,Vim.FamilyInstance__float:Translation.Z,Vim.FamilyInstance__index:Vim.Element:Element,Vim.FamilyInstance__index:Vim.Element:Host,Vim.FamilyInstance__index:Vim.FamilyType:FamilyType,Vim.FamilyInstance__index:Vim.Room:FromRoom,Vim.FamilyInstance__index:Vim.Room:ToRoom,Vim.FamilyType__byte:IsSystemFamilyType,Vim.FamilyType__index:Vim.CompoundStructure:CompoundStructure,Vim.FamilyType__index:Vim.Element:Element,Vim.FamilyType__index:Vim.Family:Family,Vim.Geometry__float:Box.Max.X,Vim.Geometry__float:Box.Max.Y,Vim.Geometry__float:Box.Max.Z,Vim.Geometry__float:Box.Min.X,Vim.Geometry__float:Box.Min.Y,Vim.Geometry__float:Box.Min.Z,Vim.Geometry__int:FaceCount,Vim.Geometry__int:VertexCount,Vim.Group__float:Position.X,Vim.Group__float:Position.Y,Vim.Group__float:Position.Z,Vim.Group__index:Vim.Element:Element,Vim.Group__string:GroupType,Vim.Level__double:Elevation,Vim.Level__index:Vim.Element:Element,Vim.Material__double:Color.X,Vim.Material__double:Color.Y,Vim.Material__double:Color.Z,Vim.Material__double:ColorUvOffset.X,Vim.Material__double:ColorUvOffset.Y,Vim.Material__double:ColorUvScaling.X,Vim.Material__double:ColorUvScaling.Y,Vim.Material__double:Glossiness,Vim.Material__double:NormalAmount,Vim.Material__double:NormalUvOffset.X,Vim.Material__double:NormalUvOffset.Y,Vim.Material__double:NormalUvScaling.X,Vim.Material__double:NormalUvScaling.Y,Vim.Material__double:Smoothness,Vim.Material__double:Transparency,Vim.Material__index:Vim.Asset:ColorTextureFile,Vim.Material__index:Vim.Asset:NormalTextureFile,Vim.Material__index:Vim.Element:Element,Vim.Material__string:MaterialCategory,Vim.Material__string:Name,Vim.MaterialInElement__byte:IsPaint,Vim.MaterialInElement__double:Area,Vim.MaterialInElement__double:Volume,Vim.MaterialInElement__index:Vim.Element:Element,Vim.MaterialInElement__index:Vim.Material:Material,Vim.Node__index:Vim.Element:Element,Vim.Parameter__index:Vim.Element:Element,Vim.Parameter__index:Vim.ParameterDescriptor:ParameterDescriptor,Vim.Parameter__string:Value,Vim.ParameterDescriptor__byte:IsInstance,Vim.ParameterDescriptor__byte:IsReadOnly,Vim.ParameterDescriptor__byte:IsShared,Vim.ParameterDescriptor__index:Vim.DisplayUnit:DisplayUnit,Vim.ParameterDescriptor__int:Flags,Vim.ParameterDescriptor__string:Group,Vim.ParameterDescriptor__string:Guid,Vim.ParameterDescriptor__string:Name,Vim.ParameterDescriptor__string:ParameterType,Vim.Phase__index:Vim.Element:Element,Vim.PhaseOrderInBimDocument__index:Vim.BimDocument:BimDocument,Vim.PhaseOrderInBimDocument__index:Vim.Phase:Phase,Vim.PhaseOrderInBimDocument__int:OrderIndex,Vim.Room__double:Area,Vim.Room__double:BaseOffset,Vim.Room__double:LimitOffset,Vim.Room__double:Perimeter,Vim.Room__double:UnboundedHeight,Vim.Room__double:Volume,Vim.Room__index:Vim.Element:Element,Vim.Room__index:Vim.Level:UpperLimit,Vim.Room__string:Number,Vim.Shape__index:Vim.Element:Element,Vim.ShapeCollection__index:Vim.Element:Element,Vim.ShapeInShapeCollection__index:Vim.Shape:Shape,Vim.ShapeInShapeCollection__index:Vim.ShapeCollection:ShapeCollection,Vim.ShapeInView__index:Vim.Shape:Shape,Vim.ShapeInView__index:Vim.View:View,Vim.System__index:Vim.Element:Element,Vim.System__int:SystemType,Vim.View__double:Origin.X,Vim.View__double:Origin.Y,Vim.View__double:Origin.Z,Vim.View__double:Outline.Max.X,Vim.View__double:Outline.Max.Y,Vim.View__double:Outline.Min.X,Vim.View__double:Outline.Min.Y,Vim.View__double:Right.X,Vim.View__double:Right.Y,Vim.View__double:Right.Z,Vim.View__double:Scale,Vim.View__double:Up.X,Vim.View__double:Up.Y,Vim.View__double:Up.Z,Vim.View__double:ViewDirection.X,Vim.View__double:ViewDirection.Y,Vim.View__double:ViewDirection.Z,Vim.View__double:ViewPosition.X,Vim.View__double:ViewPosition.Y,Vim.View__double:ViewPosition.Z,Vim.View__index:Vim.Camera:Camera,Vim.View__index:Vim.Element:Element,Vim.View__int:DetailLevel,Vim.View__string:Title,Vim.View__string:ViewType,Vim.Warning__index:Vim.BimDocument:BimDocument,Vim.Warning__string:Description,Vim.Warning__string:Guid,Vim.Warning__string:Severity,Vim.Workset__byte:IsEditable,Vim.Workset__byte:IsOpen,Vim.Workset__index:Vim.BimDocument:BimDocument,Vim.Workset__int:Id,Vim.Workset__string:Kind,Vim.Workset__string:Name,Vim.Workset__string:Owner,Vim.Workset__string:UniqueId")]
pub struct DocumentModel {}

// pub struct Asset<'a> {
//     pub index: usize,
   
//     pub buffer_name: &'a str,
// }
// impl<'a> std::fmt::Debug for Asset<'a> {
//     fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
//         f.debug_struct("Asset").field("index", &self.index).field("buffer_name", &self.buffer_name).finish()
//     }
// }

// // pub fn get_asset_table(scene: &mut VimScene) -> Option<AssetTable> {
// //     scene
// //         .entity_tables
// //         .get_mut("Vim.Asset")
// //         .map(|entity_table| AssetTable::new(entity_table, &mut scene.strings))
// // }

// pub struct DisplayUnit<'a> {
    
//     pub index: usize,
//     pub spec: &'a str,
//     pub unit_type: &'a str,
//     pub label: &'a str,
// }

// impl<'a> DisplayUnit<'a> {
//     pub const NAME: &'static str = "as";
// }

// pub struct ParameterDescriptor<'a> {
//     pub index: usize,
//     pub name: &'a str,
//     pub group: &'a str,
//     pub parameter_type: &'a str,
//     pub is_instance: bool,
//     pub is_shared: bool,
//     pub is_read_only: bool,
//     pub flags: i32,
//     pub guid: &'a str,

//     pub display_unit_index: usize,
//     pub display_unit: &'a DisplayUnit<'a>,
// }

// pub struct Parameter<'a> {
//     pub index: usize,
//     pub value: &'a str,

//     pub parameter_descriptor_index: usize,
//     pub parameter_descriptor: &'a ParameterDescriptor<'a>,

//     pub element_index: usize,
//     pub element: &'a Element<'a>,
// }

// pub struct Element<'a> {
//     pub index: usize,
//     pub id: i32,
//     pub element_type: &'a str,
//     pub name: &'a str,
//     pub unique_id: &'a str,
//     pub location: Vector3<f32>,
//     pub family_name: &'a str,
//     pub is_pinned: bool,

//     pub level_index: usize,
//     pub level: &'a Level<'a>,

//     pub phase_created_index: usize,
//     pub phase_created: &'a Phase<'a>,

//     pub phase_demolished_index: usize,
//     pub phase_demolished: &'a Phase<'a>,

//     pub category_index: usize,
//     pub category: &'a Category<'a>,

//     pub workset_index: usize,
//     pub workset: &'a Workset<'a>,

//     pub design_option_index: usize,
//     pub design_option: &'a DesignOption<'a>,

//     pub owner_view_index: usize,
//     pub owner_view: &'a View<'a>,
    
//     pub group_index: usize,
//     pub group: &'a Group<'a>,

//     pub assembly_instance_index: usize,
//     pub assembly_instance: &'a AssemblyInstance<'a>,

//     pub bim_document_index: usize,
//     pub bim_document: &'a BimDocument<'a>,

//     pub room_index: usize,
//     pub room: &'a Room<'a>,
// }

// pub struct Workset<'a> {
//     pub index: i32,
//     pub id: i32,
//     pub name: &'a str,
//     pub kind: &'a str,
//     pub is_open: bool,
//     pub is_editable: bool,
//     pub owner: &'a str,
//     pub unique_id: &'a str,

//     pub bim_document_index: i32,
//     pub bim_document: &'a BimDocument<'a>,
// }

// pub struct AssemblyInstance<'a> {
//     pub index: i32,
//     pub assembly_type_name: &'a str,
//     pub position: Vector3<f32>,

//     pub element_index: i32,
//     pub element: Option<&'a Element<'a>>,
// }

// pub struct Group<'a> {
//     pub index: i32,
//     pub group_type: &'a str,
//     pub position: Vector3<f32>,

//     pub element_index: i32,
//     pub element: &'a Element<'a>,
// }

// pub struct DesignOption<'a> {
//     pub index: i32,
//     pub is_primary: bool,

//     pub element_index: i32,
//     pub element: &'a Element<'a>,
// }

// pub struct Level<'a> {
//     pub index: i32,
//     pub elevation: f64,

//     pub element_index: i32,
//     pub element: &'a Element<'a>,
// }

// pub struct Phase<'a> {
//     pub index: i32,

//     pub element_index: i32,
//     pub element: &'a Element<'a>,
// }

// pub struct Room<'a> {
//     pub index: i32,
//     pub base_offset: f64,
//     pub limit_offset: f64,
//     pub unbounded_height: f64,
//     pub volume: f64,
//     pub perimeter: f64,
//     pub area: f64,
//     pub number: &'a str,

//     pub upper_limit_index: i32,
//     pub upper_limit: &'a Level<'a>,
    
//     pub element_index: i32,
//     pub element: &'a Level<'a>,
// }

// pub struct BimDocument<'a> {
//     pub index: i32,
//     pub title: &'a str,
//     pub is_metric: bool,
//     pub guid: &'a str,
//     pub num_saves: i32,
//     pub is_linked: bool,
//     pub is_detached: bool,
//     pub is_workshared: bool,
//     pub path_name: &'a str,
//     pub latitude: f64,
//     pub longitude: f64,
//     pub time_zone: f64,
//     pub place_name: &'a str,
//     pub weather_station_name: &'a str,
//     pub elevation: f64,
//     pub project_location: &'a str,
//     pub issue_date: &'a str,
//     pub status: &'a str,
//     pub client_name: &'a str,
//     pub address: &'a str,
//     pub name: &'a str,
//     pub number: &'a str,
//     pub author: &'a str,
//     pub building_name: &'a str,
//     pub organization_name: &'a str,
//     pub organization_description: &'a str,
//     pub product: &'a str,
//     pub version: &'a str,
//     pub user: &'a str,

//     pub active_view_index: i32,
//     pub active_view: &'a View<'a>,

//     pub owner_family_index: i32,
//     pub owner_family: &'a Family<'a>,

//     pub parent_index: i32,
//     pub parent: &'a BimDocument<'a>,

//     pub element_index: i32,
//     pub element: &'a Element<'a>,
// }

// pub struct DisplayUnitInBimDocument<'a> {
//     pub index: i32,

//     pub display_unit_index: i32,
//     pub display_unit: &'a DisplayUnit<'a>, 

//     pub bim_document_index: i32,
//     pub bim_document: &'a BimDocument<'a>,
// }

// pub struct PhaseOrderInBimDocument<'a> {
//     pub index: i32,
//     pub order_index: i32,

//     pub phase_index: i32,
//     pub phase: &'a Phase<'a>,

//     pub bim_document_index: i32,
//     pub bim_document: &'a BimDocument<'a>,
// }

// pub struct Category<'a> {
//     pub index: i32,
//     pub name: &'a str,
//     pub id: i32,
//     pub category_type: &'a str,
//     pub line_color: Vector3<f64>,
//     pub built_in_category: &'a str,

//     pub parent_index: i32,
//     pub parent: &'a Category<'a>, 

//     pub material_index: i32,
//     pub material: &'a Material<'a>, 
// }

// pub struct Family<'a> {
//     pub index: i32,
//     pub structural_material_type: &'a str,
//     pub structural_section_shape: &'a str,
//     pub is_system_family: bool,
//     pub is_in_place: bool,

//     pub family_category_index: i32,
//     pub family_category: &'a Category<'a>,
    
//     pub element_index: i32,
//     pub element: &'a Element<'a>,
// }

// pub struct FamilyType<'a> {
//     pub index: i32,
//     pub is_system_family_type: bool,
    
//     pub family_index: i32,
//     pub family: &'a Family<'a>,
    
//     pub compound_structure_index: i32,
//     pub compound_structure: &'a CompoundStructure<'a>,
    
//     pub element_index: i32,
//     pub element: &'a Element<'a>,
// }

// pub struct FamilyInstance<'a> {
//     pub index: i32,
//     pub facing_flipped: bool,
//     pub facing_orientation: Vector3<f32>,
//     pub hand_flipped: bool,
//     pub mirrored: bool,
//     pub has_modified_geometry: bool,
//     pub scale: f32,
//     pub basis_x: Vector3<f32>,
//     pub basis_y: Vector3<f32>,
//     pub basis_z: Vector3<f32>,
//     pub translation: Vector3<f32>,
//     pub hand_orientation: Vector3<f32>,

//     pub family_type_index: i32,
//     pub family_type: &'a FamilyType<'a>,
    
//     pub host_index: i32,
//     pub host: &'a Element<'a>,
    
//     pub from_room_index: i32,
//     pub from_room: &'a Room<'a>, 
    
//     pub to_room_index: i32,
//     pub to_room: &'a Room<'a>, 
    
//     pub element_index: i32,
//     pub element: &'a Element<'a>,
// }

// pub struct View<'a> {
//     pub index: i32,
//     pub title: &'a str,
//     pub view_type: &'a str,
//     pub up: Vector3<f64>,
//     pub right: Vector3<f64>,
//     pub origin: Vector3<f64>,
//     pub view_direction: Vector3<f64>,
//     pub view_position: Vector3<f64>,
//     pub scale: f64,
//     pub outline: AABox2D<f64>,
//     pub detail_level: i32,
    
//     pub camera_index: i32,
//     pub camera: &'a Camera<'a>,

//     pub element_index: i32,
//     pub element: &'a Element<'a>,
// }

// pub struct ElementInView<'a> {
//     pub index: i32,

//     pub view_index: i32,
//     pub view: &'a View<'a>,
    
//     pub element_index: i32,
//     pub element: &'a Element<'a>,
// }

// pub struct ShapeInView<'a> {
//     pub index: i32,

//     pub shape_index: i32,
//     pub shape: &'a Shape<'a>,
    
//     pub view_index: i32,
//     pub view: &'a View<'a>,
// }

// pub struct AssetInView<'a> {
//     pub index: i32,

//     pub asset_index: i32,
//     pub asset: &'a Asset<'a>,

//     pub view_index: i32,
//     pub view: &'a View<'a>,
// }

// pub struct LevelInView<'a> {
//     pub index: i32,
//     pub extents: AABox<f64>,

//     pub level_index: i32,
//     pub level: &'a Level<'a>,
    
//     pub view_index: i32,
//     pub view: &'a View<'a>,
// }

// pub struct Camera<'a> {
//     pub index: &'a i32,
//     pub id: i32,
//     pub is_perspective: i32,
//     pub vertical_extent: f64,
//     pub horizontal_extent: f64,
//     pub far_distance: f64,
//     pub near_distance: f64,
//     pub target_distance: f64,
//     pub right_offset: f64,
//     pub up_offset: f64,
// }

// pub struct Material<'a> {
//     pub index: i32,
//     pub name: &'a str,
//     pub material_category: &'a str,
//     pub color: Vector3<f64>,
//     pub color_uv_scaling: Vector2<f64>,
//     pub color_uv_offset: Vector2<f64>,
//     pub normal_uv_scaling: Vector2<f64>,
//     pub normal_uv_offset: Vector2<f64>,
//     pub normal_amount: f64,
//     pub glossiness: f64,
//     pub smoothness: f64,
//     pub transparency: f64,

//     pub color_texture_file_index: i32,
//     pub color_texture_file: &'a Asset<'a>,

//     pub normal_texture_file_index: i32,
//     pub normal_texture_file: &'a Asset<'a>,

//     pub element_index: i32,
//     pub element: &'a Element<'a>,
// }

// pub struct MaterialInElement<'a> {
//     pub index: i32,
//     pub area: f64,
//     pub volume: f64,
//     pub is_paint: bool,

//     pub material_index: i32,
//     pub material: &'a Material<'a>,
    
//     pub element_index: i32,
//     pub element: &'a Element<'a>,
// }

// pub struct CompoundStructureLayer<'a> {
//     pub index: i32,
//     pub order_index: i32,
//     pub width: f64,
//     pub material_function_assignment: &'a str,
   
//     pub material_index: i32,
//     pub material: &'a Material<'a>,

//     pub compound_structure_index: i32,
//     pub compound_structure: &'a CompoundStructure<'a>,
// }

// pub struct CompoundStructure<'a> {
//     pub index: i32,
//     pub width: f64,

//     pub structural_layer_index: i32,
//     pub structural_layer: &'a CompoundStructureLayer<'a>,
// }

// pub struct Node<'a> {
//     pub index: i32,

//     pub element_index: i32,
//     pub element: &'a Element<'a>,
// }

// pub struct Geometry {
//     pub index: i32,
//     pub box_geom: AABox<f32>,
//     pub vertex_count: i32,
//     pub face_count: i32,
// }

// pub struct Shape<'a> {
//     pub m_index: i32,

//     pub m_element_index: i32,
//     pub m_element: &'a Element<'a>,
// }

// pub struct ShapeCollection<'a> {
//     pub index: i32,

//     pub element_index: i32,
//     pub element: &'a Element<'a>,
// }

// pub struct ShapeInShapeCollection<'a> {
//     pub m_index: i32,

//     pub m_shape_index: i32,
//     pub m_shape: &'a Shape<'a>,

//     pub m_shape_collection_index: i32,
//     pub m_shape_collection: &'a ShapeCollection<'a>,
// }

// pub struct System<'a> {
//     pub index: i32,
//     pub system_type: i32,

//     pub element_index: i32,
//     pub element: &'a Element<'a>,
// }

// pub struct ElementInSystem<'a> {
//     pub index: i32,
//     pub roles: i32,

//     pub system_index: i32,
//     pub system: &'a System<'a>,

//     pub element_index: i32,
//     pub element: &'a Element<'a>,
// }

// pub struct Warning<'a> {
//     pub index: i32,
//     pub guid: &'a str,
//     pub severity: &'a str,
//     pub description: &'a str,

//     pub bim_document_index: i32,
//     pub bim_document: &'a BimDocument<'a>,
// }

// pub struct ElementInWarning<'a> {
//     pub index: i32,

//     pub warning_index: i32,
//     pub warning: &'a Warning<'a>,
    
//     pub element_index: i32,
//     pub element: &'a Element<'a>,
// }

// pub struct BasePoint<'a> {
//     pub index: i32,
//     pub is_survey_point: bool,
//     pub position: Vector3<f64>,
//     pub shared_position: Vector3<f64>,
    
//     pub element_index: i32,
//     pub element: &'a Element<'a>,
// }

// pub struct PhaseFilter<'a> {
//     pub index: i32,
//     pub new: i32,
//     pub existing: i32,
//     pub demolished: i32,
//     pub temporary: i32,

//     pub element_index: i32,
//     pub element: &'a Element<'a>,
// }

// pub struct Grid<'a> {
//     pub index: i32,
//     pub start_point: Vector3<f64>,
//     pub end_point: Vector3<f64>,
//     pub is_curved: bool,
//     pub extents: AABox<f64>,
//     pub element_index: i32,
//     pub element: &'a Element<'a>,
// }

// pub struct Area<'a> {
//     pub index: i32,
//     pub value: f64,
//     pub perimeter: f64,
//     pub number: &'a str,
//     pub is_gross_interior: bool,

//     pub area_scheme_index: i32,
//     pub area_scheme: &'a AreaScheme<'a>,

//     pub element_index: i32,
//     pub element: &'a Element<'a>,
// }

// pub struct AreaScheme<'a> {
//     pub index: i32,
//     pub is_gross_building_area: bool,

//     pub element_index: i32,
//     pub element: &'a Element<'a>,
// }

// pub trait Converter<T> {
//     fn get_size(&self) -> usize;
//     fn get_bytes(&self) -> usize;
//     fn get_columns(&self) -> Vec<String>;

//     fn convert_from_array(&self, bytes: &[u8]) -> T
//     where
//         T: Sized + Default + Copy,
//     {
//         let mut result = T::default();
//         let size = self.get_size() * self.get_bytes();
//         let result_ptr = &mut result as *mut T as *mut u8;
//         unsafe {
//             std::ptr::copy_nonoverlapping(bytes.as_ptr(), result_ptr, size);
//         }
//         result
//     }

//     fn convert_from_columns(&self, dest: &mut T, bytes_arrays: &[&[u8]], index: usize)
//     where
//         T: Sized + Default + Copy,
//     {
//         let bytes = self.get_bytes();
//         let dest_ptr = dest as *mut T as *mut u8;
//         for (i, bytes_array) in bytes_arrays.iter().enumerate() {
//             unsafe {
//                 std::ptr::copy_nonoverlapping(
//                     bytes_array.as_ptr().add(index * bytes),
//                     dest_ptr.add(bytes * i),
//                     bytes,
//                 );
//             }
//         }
//     }

//     fn convert_to_array(&self, value: &T) -> Vec<u8>
//     where
//         T: Sized + Copy,
//     {
//         let mut result = vec![0u8; std::mem::size_of::<T>()];
//         let value_ptr = value as *const T as *const u8;
//         unsafe {
//             std::ptr::copy_nonoverlapping(value_ptr, result.as_mut_ptr(), result.len());
//         }
//         result
//     }
// }

// #[derive(Debug, PartialEq, Copy, Clone)]
// pub struct Vector2<T: Float> {
//     pub x: T,
//     pub y: T,
// }
// #[derive(Debug, PartialEq, Copy, Clone)]
// pub struct Vector3<T: Float> {
//     pub x: T,
//     pub y: T,
//     pub z: T,
// }
// #[derive(Debug, PartialEq, Copy, Clone)]
// pub struct AABox<T: Float> {
//     pub min: Vector3::<T>,
//     pub max: Vector3::<T>,
// }
// #[derive(Debug, PartialEq, Copy, Clone)]
// pub struct AABox2D<T: Float> {
//     pub min: Vector2::<T>,
//     pub max: Vector2::<T>,
// }



impl<'a> AssetTable<'a> {
    // pub fn new(entity_table: vim::EntityTable<'a>, strings: &'a Vec<&'a str>) -> Self {
    //     Self { entity_table, strings }
    // }

    // pub fn count(&self) -> usize {
       
    //     self.entity_table.data_columns.get("string:BufferName").map_or(0, |column| column.size() / std::mem::size_of::<i32>())
    // }

    // fn get(&self, asset_index: usize) -> Option<Asset> {
    //     let buffer_name = self.get_buffer_name(asset_index)?;
    //     Some(Asset {
    //         index: asset_index,
    //         buffer_name,
    //     })
    // }

    // fn get_all(&mut self) -> Vec<Asset> {
    //     let count = self.get_count();
    //     let buffer_name_exists = self.entity_table.string_columns.contains_key("string:BufferName");
    //     let buffer_name_data = self
    //         .entity_table
    //         .string_columns
    //         .get("string:BufferName")
    //         .unwrap_or(&Vec::new());

    //     (0..count)
    //         .map(|i| Asset {
    //             index: i,
    //             buffer_name: if buffer_name_exists {
    //                 Some(self.strings[buffer_name_data[i]].clone())
    //             } else {
    //                 None
    //             },
    //         })
    //         .collect()
    // }

    // fn get_buffer_name(&mut self, asset_index: usize) -> Option<Option<String>> {
    //     if asset_index < 0 || asset_index >= self.get_count() {
    //         return None;
    //     }

    //     self.entity_table
    //         .string_columns
    //         .get("string:BufferName")
    //         .map(|column| Some(self.strings[column[asset_index]].clone()))
    // }

    // fn get_all_buffer_names(&mut self) -> Option<Vec<Option<String>>> {
    //     let count = self.get_count();
    //     let buffer_name_data = self
    //         .entity_table
    //         .string_columns
    //         .get("string:BufferName")
    //         .unwrap_or(&Vec::new());

    //     Some(
    //         (0..count)
    //             .map(|i| Some(self.strings[buffer_name_data[i]].clone()))
    //             .collect(),
    //     )
    // }
}