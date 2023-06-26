//descriptors

pub const POSITION: &'static str = "g3d:vertex:position:0:float32:3";
pub const INDEX: &'static str = "g3d:corner:index:0:int32:1";
pub const OBJECT_FACE_SIZE: &'static str = "g3d:all:facesize:0:int32:1";

pub const VERTEX_UV: &'static str = "g3d:vertex:uv:0:float32:2";
pub const VERTEX_UVW: &'static str = "g3d:vertex:uv:0:float32:3";
pub const VERTEX_NORMAL: &'static str = "g3d:vertex:normal:0:float32:3";
pub const VERTEX_COLOR: &'static str = "g3d:vertex:color:0:float32:3";
pub const VERTEX_COLOR_WITH_ALPHA: &'static str = "g3d:vertex:color:0:float32:4";
pub const VERTEX_BITANGENT: &'static str = "g3d:vertex:bitangent:0:float32:3";
pub const VERTEX_TANGENT: &'static str = "g3d:vertex:tangent:0:float32:3";
pub const VERTEX_TANGENT4: &'static str = "g3d:vertex:tangent:0:float32:4";
pub const VERTEX_SELECTION_WEIGHT: &'static str = "g3d:vertex:weight:0:float32:1";

pub const FACE_MATERIAL: &'static str = "g3d:face:material:0:int32:1";
pub const FACE_NORMAL: &'static str = "g3d:face:normal:0:float32:3";
pub const FACE_SIZE: &'static str = "g3d:face:facesize:0:int32:1";
pub const FACE_INDEX_OFFSET: &'static str = "g3d:face:indexoffset:0:int32:1";
pub const FACE_SELECTION_WEIGHT: &'static str = "g3d:face:weight:0:float32:1";

// VIM 1.0
// Meshes
pub const MESH_SUBMESH_OFFSET: &'static str = "g3d:mesh:submeshoffset:0:int32:1";

// Instances
pub const INSTANCE_TRANSFORM: &'static str = "g3d:instance:transform:0:float32:16";
pub const INSTANCE_PARENT: &'static str = "g3d:instance:parent:0:int32:1";
pub const INSTANCE_MESH: &'static str = "g3d:instance:mesh:0:int32:1";
pub const INSTANCE_FLAGS: &'static str = "g3d:instance:flags:0:uint16:1";

// Shapes
pub const SHAPE_VERTEX: &'static str = "g3d:shapevertex:position:0:float32:3";
pub const SHAPE_VERTEX_OFFSET: &'static str = "g3d:shape:vertexoffset:0:int32:1";
pub const SHAPE_COLOR: &'static str = "g3d:shape:color:0:float32:4";
pub const SHAPE_WIDTH: &'static str = "g3d:shape:width:0:float32:1";

// Materials
pub const MATERIAL_COLOR: &'static str = "g3d:material:color:0:float32:4";
pub const MATERIAL_GLOSSINESS: &'static str = "g3d:material:glossiness:0:float32:1";
pub const MATERIAL_SMOOTHNESS: &'static str = "g3d:material:smoothness:0:float32:1";

// Submeshes
pub const SUBMESH_INDEX_OFFSET: &'static str = "g3d:submesh:indexoffset:0:int32:1";
pub const SUBMESH_MATERIAL: &'static str = "g3d:submesh:material:0:int32:1";

// https://docs.thinkboxsoftware.com/products/krakatoa/2.6/1_Documentation/manual/formats/particle_channels.html
pub const POINT_VELOCITY: &'static str = "g3d:vertex:velocity:0:float32:3";
pub const POINT_NORMAL: &'static str = "g3d:vertex:normal:0:float32:3";
pub const POINT_ACCELERATION: &'static str = "g3d:vertex:acceleration:0:float32:3";
pub const POINT_DENSITY: &'static str = "g3d:vertex:density:0:float32:1";
pub const POINT_EMISSION_COLOR: &'static str = "g3d:vertex:emission:0:float32:3";
pub const POINT_ABSORPTION_COLOR: &'static str = "g3d:vertex:absorption:0:float32:3";
pub const POINT_SPIN: &'static str = "g3d:vertex:spin:0:float32:4";
pub const POINT_ORIENTATION: &'static str = "g3d:vertex:orientation:0:float32:4";
pub const POINT_PARTICLE_ID: &'static str = "g3d:vertex:particleid:0:int32:1";
pub const POINT_AGE: &'static str = "g3d:vertex:age:0:int32:1";

// Line specific attributes
pub const LINE_TANGENT_IN: &'static str = "g3d:vertex:tangent:0:float32:3";
pub const LINE_TANGENT_OUT: &'static str = "g3d:vertex:tangent:1:float32:3";
