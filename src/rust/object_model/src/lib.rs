use std::str;
use num_traits::Float;
use vim::EntityTable;

#[derive(Debug, PartialEq, Copy, Clone)]
pub struct Vector2<T: Float> {
    pub x: T,
    pub y: T,
}
#[derive(Debug, PartialEq, Copy, Clone)]
pub struct Vector3<T: Float> {
    pub x: T,
    pub y: T,
    pub z: T,
}
#[derive(Debug, PartialEq, Copy, Clone)]
pub struct AABox<T: Float> {
    pub min: Vector3::<T>,
    pub max: Vector3::<T>,
}
#[derive(Debug, PartialEq, Copy, Clone)]
pub struct AABox2D<T: Float> {
    pub min: Vector2::<T>,
    pub max: Vector2::<T>,
}

pub struct Asset<'a> {
    pub index: usize,
    pub buffer_name: &'a str,
}

pub struct AssetTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct DisplayUnit<'a> {
    pub index: usize,
    pub spec: &'a str,
    pub unit_type: &'a str,
    pub label: &'a str,
}

pub struct DisplayUnitTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct ParameterDescriptor<'a> {
    pub index: usize,
    pub name: &'a str,
    pub group: &'a str,
    pub parameter_type: &'a str,
    pub is_instance: bool,
    pub is_shared: bool,
    pub is_read_only: bool,
    pub flags: i32,
    pub guid: &'a str,

    pub display_unit_index: usize,
    pub display_unit: &'a DisplayUnit<'a>,
}

pub struct ParameterDescriptorTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct Parameter<'a> {
    pub index: usize,
    pub value: &'a str,

    pub parameter_descriptor_index: usize,
    pub parameter_descriptor: &'a ParameterDescriptor<'a>,

    pub element_index: usize,
    pub element: &'a Element<'a>,
}

pub struct ParameterTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct Element<'a> {
    pub index: usize,
    pub id: i32,
    pub element_type: &'a str,
    pub name: &'a str,
    pub unique_id: &'a str,
    pub location: Vector3<f32>,
    pub family_name: &'a str,
    pub is_pinned: bool,

    pub level_index: usize,
    pub level: &'a Level<'a>,

    pub phase_created_index: usize,
    pub phase_created: &'a Phase<'a>,

    pub phase_demolished_index: usize,
    pub phase_demolished: &'a Phase<'a>,

    pub category_index: usize,
    pub category: &'a Category<'a>,

    pub workset_index: usize,
    pub workset: &'a Workset<'a>,

    pub design_option_index: usize,
    pub design_option: &'a DesignOption<'a>,

    pub owner_view_index: usize,
    pub owner_view: &'a View<'a>,
    
    pub group_index: usize,
    pub group: &'a Group<'a>,

    pub assembly_instance_index: usize,
    pub assembly_instance: &'a AssemblyInstance<'a>,

    pub bim_document_index: usize,
    pub bim_document: &'a BimDocument<'a>,

    pub room_index: usize,
    pub room: &'a Room<'a>,
}

pub struct ElementTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct Workset<'a> {
    pub index: i32,
    pub id: i32,
    pub name: &'a str,
    pub kind: &'a str,
    pub is_open: bool,
    pub is_editable: bool,
    pub owner: &'a str,
    pub unique_id: &'a str,

    pub bim_document_index: i32,
    pub bim_document: &'a BimDocument<'a>,
}

pub struct WorksetTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct AssemblyInstance<'a> {
    pub index: i32,
    pub assembly_type_name: &'a str,
    pub position: Vector3<f32>,

    pub element_index: i32,
    pub element: &'a Element<'a>,
}

pub struct AssemblyInstanceTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct Group<'a> {
    pub index: i32,
    pub group_type: &'a str,
    pub position: Vector3<f32>,

    pub element_index: i32,
    pub element: &'a Element<'a>,
}

pub struct GroupTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct DesignOption<'a> {
    pub index: i32,
    pub is_primary: bool,

    pub element_index: i32,
    pub element: &'a Element<'a>,
}

pub struct DesignOptionTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct Level<'a> {
    pub index: i32,
    pub elevation: f64,

    pub element_index: i32,
    pub element: &'a Element<'a>,
}

pub struct LevelTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct Phase<'a> {
    pub index: i32,

    pub element_index: i32,
    pub element: &'a Element<'a>,
}

pub struct PhaseTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct Room<'a> {
    pub index: i32,
    pub base_offset: f64,
    pub limit_offset: f64,
    pub unbounded_height: f64,
    pub volume: f64,
    pub perimeter: f64,
    pub area: f64,
    pub number: &'a str,

    pub upper_limit_index: i32,
    pub upper_limit: &'a Level<'a>,
    
    pub element_index: i32,
    pub element: &'a Level<'a>,
}

pub struct RoomTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct BimDocument<'a> {
    pub index: i32,
    pub title: &'a str,
    pub is_metric: bool,
    pub guid: &'a str,
    pub num_saves: i32,
    pub is_linked: bool,
    pub is_detached: bool,
    pub is_workshared: bool,
    pub path_name: &'a str,
    pub latitude: f64,
    pub longitude: f64,
    pub time_zone: f64,
    pub place_name: &'a str,
    pub weather_station_name: &'a str,
    pub elevation: f64,
    pub project_location: &'a str,
    pub issue_date: &'a str,
    pub status: &'a str,
    pub client_name: &'a str,
    pub address: &'a str,
    pub name: &'a str,
    pub number: &'a str,
    pub author: &'a str,
    pub building_name: &'a str,
    pub organization_name: &'a str,
    pub organization_description: &'a str,
    pub product: &'a str,
    pub version: &'a str,
    pub user: &'a str,

    pub active_view_index: i32,
    pub active_view: &'a View<'a>,

    pub owner_family_index: i32,
    pub owner_family: &'a Family<'a>,

    pub parent_index: i32,
    pub parent: &'a BimDocument<'a>,

    pub element_index: i32,
    pub element: &'a Element<'a>,
}

pub struct BimDocumentTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct DisplayUnitInBimDocument<'a> {
    pub index: i32,

    pub display_unit_index: i32,
    pub display_unit: &'a DisplayUnit<'a>, 

    pub bim_document_index: i32,
    pub bim_document: &'a BimDocument<'a>,
}

pub struct DisplayUnitInBimDocumentTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct PhaseOrderInBimDocument<'a> {
    pub index: i32,
    pub order_index: i32,

    pub phase_index: i32,
    pub phase: &'a Phase<'a>,

    pub bim_document_index: i32,
    pub bim_document: &'a BimDocument<'a>,
}

pub struct PhaseOrderInBimDocumentTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct Category<'a> {
    pub index: i32,
    pub name: &'a str,
    pub id: i32,
    pub category_type: &'a str,
    pub line_color: Vector3<f64>,
    pub built_in_category: &'a str,

    pub parent_index: i32,
    pub parent: &'a Category<'a>, 

    pub material_index: i32,
    pub material: &'a Material<'a>, 
}

pub struct CategoryTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct Family<'a> {
    pub index: i32,
    pub structural_material_type: &'a str,
    pub structural_section_shape: &'a str,
    pub is_system_family: bool,
    pub is_in_place: bool,

    pub family_category_index: i32,
    pub family_category: &'a Category<'a>,
    
    pub element_index: i32,
    pub element: &'a Element<'a>,
}

pub struct FamilyTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct FamilyType<'a> {
    pub index: i32,
    pub is_system_family_type: bool,
    
    pub family_index: i32,
    pub family: &'a Family<'a>,
    
    pub compound_structure_index: i32,
    pub compound_structure: &'a CompoundStructure<'a>,
    
    pub element_index: i32,
    pub element: &'a Element<'a>,
}

pub struct FamilyTypeTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct FamilyInstance<'a> {
    pub index: i32,
    pub facing_flipped: bool,
    pub facing_orientation: Vector3<f32>,
    pub hand_flipped: bool,
    pub mirrored: bool,
    pub has_modified_geometry: bool,
    pub scale: f32,
    pub basis_x: Vector3<f32>,
    pub basis_y: Vector3<f32>,
    pub basis_z: Vector3<f32>,
    pub translation: Vector3<f32>,
    pub hand_orientation: Vector3<f32>,

    pub family_type_index: i32,
    pub family_type: &'a FamilyType<'a>,
    
    pub host_index: i32,
    pub host: &'a Element<'a>,
    
    pub from_room_index: i32,
    pub from_room: &'a Room<'a>, 
    
    pub to_room_index: i32,
    pub to_room: &'a Room<'a>, 
    
    pub element_index: i32,
    pub element: &'a Element<'a>,
}

pub struct FamilyInstanceTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct View<'a> {
    pub index: i32,
    pub title: &'a str,
    pub view_type: &'a str,
    pub up: Vector3<f64>,
    pub right: Vector3<f64>,
    pub origin: Vector3<f64>,
    pub view_direction: Vector3<f64>,
    pub view_position: Vector3<f64>,
    pub scale: f64,
    pub outline: AABox2D<f64>,
    pub detail_level: i32,
    
    pub camera_index: i32,
    pub camera: &'a Camera,

    pub element_index: i32,
    pub element: &'a Element<'a>,
}

pub struct ViewTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct ElementInView<'a> {
    pub index: i32,

    pub view_index: i32,
    pub view: &'a View<'a>,
    
    pub element_index: i32,
    pub element: &'a Element<'a>,
}

pub struct ElementInViewTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct ShapeInView<'a> {
    pub index: i32,

    pub shape_index: i32,
    pub shape: &'a Shape<'a>,
    
    pub view_index: i32,
    pub view: &'a View<'a>,
}

pub struct ShapeInViewTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct AssetInView<'a> {
    pub index: i32,

    pub asset_index: i32,
    pub asset: &'a Asset<'a>,

    pub view_index: i32,
    pub view: &'a View<'a>,
}

pub struct AssetInViewTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct LevelInView<'a> {
    pub index: i32,
    pub extents: AABox<f64>,

    pub level_index: i32,
    pub level: &'a Level<'a>,
    
    pub view_index: i32,
    pub view: &'a View<'a>,
}

pub struct LevelInViewTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct Camera {
    pub index: i32,
    pub id: i32,
    pub is_perspective: i32,
    pub vertical_extent: f64,
    pub horizontal_extent: f64,
    pub far_distance: f64,
    pub near_distance: f64,
    pub target_distance: f64,
    pub right_offset: f64,
    pub up_offset: f64,
}

pub struct CameraTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct Material<'a> {
    pub index: i32,
    pub name: &'a str,
    pub material_category: &'a str,
    pub color: Vector3<f64>,
    pub color_uv_scaling: Vector2<f64>,
    pub color_uv_offset: Vector2<f64>,
    pub normal_uv_scaling: Vector2<f64>,
    pub normal_uv_offset: Vector2<f64>,
    pub normal_amount: f64,
    pub glossiness: f64,
    pub smoothness: f64,
    pub transparency: f64,

    pub color_texture_file_index: i32,
    pub color_texture_file: &'a Asset<'a>,

    pub normal_texture_file_index: i32,
    pub normal_texture_file: &'a Asset<'a>,

    pub element_index: i32,
    pub element: &'a Element<'a>,
}

pub struct MaterialTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct MaterialInElement<'a> {
    pub index: i32,
    pub area: f64,
    pub volume: f64,
    pub is_paint: bool,

    pub material_index: i32,
    pub material: &'a Material<'a>,
    
    pub element_index: i32,
    pub element: &'a Element<'a>,
}

pub struct MaterialInElementTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct CompoundStructureLayer<'a> {
    pub index: i32,
    pub order_index: i32,
    pub width: f64,
    pub material_function_assignment: &'a str,
   
    pub material_index: i32,
    pub material: &'a Material<'a>,

    pub compound_structure_index: i32,
    pub compound_structure: &'a CompoundStructure<'a>,
}

pub struct CompoundStructureLayerTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct CompoundStructure<'a> {
    pub index: i32,
    pub width: f64,

    pub structural_layer_index: i32,
    pub structural_layer: &'a CompoundStructureLayer<'a>,
}

pub struct CompoundStructureTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct Node<'a> {
    pub index: i32,

    pub element_index: i32,
    pub element: &'a Element<'a>,
}

pub struct NodeTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct Geometry {
    pub index: i32,
    pub box_geom: AABox<f32>,
    pub vertex_count: i32,
    pub face_count: i32,
}

pub struct GeometryTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct Shape<'a> {
    pub m_index: i32,

    pub m_element_index: i32,
    pub m_element: &'a Element<'a>,
}

pub struct ShapeTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct ShapeCollection<'a> {
    pub index: i32,

    pub element_index: i32,
    pub element: &'a Element<'a>,
}

pub struct ShapeCollectionTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct ShapeInShapeCollection<'a> {
    pub m_index: i32,

    pub m_shape_index: i32,
    pub m_shape: &'a Shape<'a>,

    pub m_shape_collection_index: i32,
    pub m_shape_collection: &'a ShapeCollection<'a>,
}

pub struct ShapeInShapeCollectionTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct System<'a> {
    pub index: i32,
    pub system_type: i32,

    pub element_index: i32,
    pub element: &'a Element<'a>,
}

pub struct SystemTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct ElementInSystem<'a> {
    pub index: i32,
    pub roles: i32,

    pub system_index: i32,
    pub system: &'a System<'a>,

    pub element_index: i32,
    pub element: &'a Element<'a>,
}

pub struct ElementInSystemTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct Warning<'a> {
    pub index: i32,
    pub guid: &'a str,
    pub severity: &'a str,
    pub description: &'a str,

    pub bim_document_index: i32,
    pub bim_document: &'a BimDocument<'a>,
}

pub struct WarningTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct ElementInWarning<'a> {
    pub index: i32,

    pub warning_index: i32,
    pub warning: &'a Warning<'a>,
    
    pub element_index: i32,
    pub element: &'a Element<'a>,
}

pub struct ElementInWarningTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct BasePoint<'a> {
    pub index: i32,
    pub is_survey_point: bool,
    pub position: Vector3<f64>,
    pub shared_position: Vector3<f64>,
    
    pub element_index: i32,
    pub element: &'a Element<'a>,
}

pub struct BasePointTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct PhaseFilter<'a> {
    pub index: i32,
    pub new: i32,
    pub existing: i32,
    pub demolished: i32,
    pub temporary: i32,

    pub element_index: i32,
    pub element: &'a Element<'a>,
}

pub struct PhaseFilterTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct Grid<'a> {
    pub index: i32,
    pub start_point: Vector3<f64>,
    pub end_point: Vector3<f64>,
    pub is_curved: bool,
    pub extents: AABox<f64>,
    pub element_index: i32,
    pub element: &'a Element<'a>,
}

pub struct GridTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct Area<'a> {
    pub index: i32,
    pub value: f64,
    pub perimeter: f64,
    pub number: &'a str,
    pub is_gross_interior: bool,

    pub area_scheme_index: i32,
    pub area_scheme: &'a AreaScheme<'a>,

    pub element_index: i32,
    pub element: &'a Element<'a>,
}

pub struct AreaTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}

pub struct AreaScheme<'a> {
    pub index: i32,
    pub is_gross_building_area: bool,

    pub element_index: i32,
    pub element: &'a Element<'a>,
}

pub struct AreaSchemeTable<'a> {
    pub entity_table: EntityTable<'a>,
    pub strings: Vec<&'a str>,
}




pub trait Converter<T> {
    fn get_size(&self) -> usize;
    fn get_bytes(&self) -> usize;
    fn get_columns(&self) -> Vec<String>;

    fn convert_from_array(&self, bytes: &[u8]) -> T
    where
        T: Sized + Default + Copy,
    {
        let mut result = T::default();
        let size = self.get_size() * self.get_bytes();
        let result_ptr = &mut result as *mut T as *mut u8;
        unsafe {
            std::ptr::copy_nonoverlapping(bytes.as_ptr(), result_ptr, size);
        }
        result
    }

    fn convert_from_columns(&self, dest: &mut T, bytes_arrays: &[&[u8]], index: usize)
    where
        T: Sized + Default + Copy,
    {
        let bytes = self.get_bytes();
        let dest_ptr = dest as *mut T as *mut u8;
        for (i, bytes_array) in bytes_arrays.iter().enumerate() {
            unsafe {
                std::ptr::copy_nonoverlapping(
                    bytes_array.as_ptr().add(index * bytes),
                    dest_ptr.add(bytes * i),
                    bytes,
                );
            }
        }
    }

    fn convert_to_array(&self, value: &T) -> Vec<u8>
    where
        T: Sized + Copy,
    {
        let mut result = vec![0u8; std::mem::size_of::<T>()];
        let value_ptr = value as *const T as *const u8;
        unsafe {
            std::ptr::copy_nonoverlapping(value_ptr, result.as_mut_ptr(), result.len());
        }
        result
    }
}


// impl<'a> DisplayUnit<'a> {
//     fn default() -> Self {
//         DisplayUnit {
//             index: 0,
//             spec: "",
//             unit_type: "",
//             label: "",
//         }
//     }
// }
// impl<'a> AssetTable<'a> {
//     fn new(entity_table: &'a EntityTable, strings: Vec<&'a str>) -> Self {
//         Self { entity_table, strings }
//     }

//     fn get_count(&self) -> usize {
//         self.entity_table.string_columns
//             .get("string:BufferName")
//             .map_or(0, |column| column.len())
//     }

//     fn get(&self, asset_index: usize) -> Option<Asset> {
//         let buffer_name = self.get_buffer_name(asset_index)?;
//         Some(Asset {
//             index: asset_index,
//             buffer_name,
//         })
//     }

//     fn get_all(&mut self) -> Vec<Asset> {
//         let count = self.get_count();
//         let buffer_name_exists = self.entity_table.string_columns.contains_key("string:BufferName");
//         let buffer_name_data = self
//             .entity_table
//             .string_columns
//             .get("string:BufferName")
//             .unwrap_or(&Vec::new());

//         (0..count)
//             .map(|i| Asset {
//                 index: i,
//                 buffer_name: if buffer_name_exists {
//                     Some(self.strings[buffer_name_data[i]].clone())
//                 } else {
//                     None
//                 },
//             })
//             .collect()
//     }

//     fn get_buffer_name(&mut self, asset_index: usize) -> Option<Option<String>> {
//         if asset_index < 0 || asset_index >= self.get_count() {
//             return None;
//         }

//         self.entity_table
//             .string_columns
//             .get("string:BufferName")
//             .map(|column| Some(self.strings[column[asset_index]].clone()))
//     }

//     fn get_all_buffer_names(&mut self) -> Option<Vec<Option<String>>> {
//         let count = self.get_count();
//         let buffer_name_data = self
//             .entity_table
//             .string_columns
//             .get("string:BufferName")
//             .unwrap_or(&Vec::new());

//         Some(
//             (0..count)
//                 .map(|i| Some(self.strings[buffer_name_data[i]].clone()))
//                 .collect(),
//         )
//     }
// }