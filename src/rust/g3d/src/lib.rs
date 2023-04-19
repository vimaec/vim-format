pub mod descriptors;
use bfast::{Buffer, Bfast};
pub use descriptors::*;

pub const G3D_VERSION: (u8, u8, u8, &'static str) = (2, 2, 0, "2022.05.02");

/// The different types of data types that can be used as elements.
#[derive(Debug, PartialEq, Eq, Hash, Clone, Copy)]
pub enum DataType {
    Uint8,
    Int8,
    Uint16,
    Int16,
    Uint32,
    Int32,
    Uint64,
    Int64,
    Uint128,
    Int128,
    Float16,
    Float32,
    Float64,
    Float128,
    Undefined = 0xff,
}
impl std::str::FromStr for DataType {
    type Err = ();
    fn from_str(s: &str) -> Result<Self, Self::Err> {
        match s {
            "uint8" =>      Ok(DataType::Uint8),
            "int8" =>       Ok(DataType::Int8),
            "uint16" =>     Ok(DataType::Uint16),
            "int16" =>      Ok(DataType::Int16),
            "uint32" =>     Ok(DataType::Uint32),
            "int32" =>      Ok(DataType::Int32),
            "uint64" =>     Ok(DataType::Uint64),
            "int64" =>      Ok(DataType::Int64),
            "uint128" =>    Ok(DataType::Uint128),
            "int128" =>     Ok(DataType::Int128),
            "float16" =>    Ok(DataType::Float16),
            "float32" =>    Ok(DataType::Float32),
            "float64" =>    Ok(DataType::Float64),
            "float128" =>   Ok(DataType::Float128),
            _ =>            Ok(DataType::Undefined),
        }
    }
}
impl std::string::ToString for DataType {
    fn to_string(&self) -> String {
        match self {
            DataType::Uint8 =>       String::from("uint8"),
            DataType::Int8 =>        String::from("int8"),
            DataType::Uint16 =>      String::from("uint16"),
            DataType::Int16 =>       String::from("int16"),
            DataType::Uint32 =>      String::from("uint32"),
            DataType::Int32 =>       String::from("int32"),
            DataType::Uint64 =>      String::from("uint64"),
            DataType::Int64 =>       String::from("int64"),
            DataType::Uint128 =>     String::from("uint128"),
            DataType::Int128 =>      String::from("int128"),
            DataType::Float16 =>     String::from("float16"),
            DataType::Float32 =>     String::from("float32"),
            DataType::Float64 =>     String::from("float64"),
            DataType::Float128 =>    String::from("float128"),
            DataType::Undefined =>   String::from(""),
        }
    }
}
impl DataType {
    /// Retrieves the size of individual data types
    pub fn size(&self) -> Result<usize, Box<dyn std::error::Error>>  {
        match self {
            DataType::Uint8 | DataType::Int8 => Ok(1),
            DataType::Uint16 | DataType::Int16 => Ok(2),
            DataType::Uint32 | DataType::Int32 => Ok(4),
            DataType::Uint64 | DataType::Int64 => Ok(8),
            DataType::Uint128 | DataType::Int128 => Ok(16),
            DataType::Float16 => Ok(2),
            DataType::Float32 => Ok(4),
            DataType::Float64 => Ok(8),
            DataType::Float128 => Ok(16),
            _ => Err("Invalid data type".into()),
        }
    }
}

/// What geometric element each attribute is associated with 
#[derive(Debug, PartialEq, Eq, Hash, Clone, Copy)]
pub enum Association {
    Vertex,
    Face,
    Corner,
    Edge,
    Subgeometry,
    Instance,
    Shapevertex,
    Shape,
    Material,
    Mesh,
    Submesh,
    All,
    None,
}
impl std::str::FromStr for Association {
    type Err = ();
    fn from_str(s: &str) -> Result<Self, Self::Err> {
        match s {
            "vertex" =>      Ok(Association::Vertex),
            "face" =>        Ok(Association::Face),
            "corner" =>      Ok(Association::Corner),
            "edge" =>        Ok(Association::Edge),
            "subgeometry" => Ok(Association::Subgeometry),
            "instance" =>    Ok(Association::Instance),
            "shapevertex" => Ok(Association::Shapevertex), 
            "shape" =>       Ok(Association::Shape),
            "material" =>    Ok(Association::Material),
            "mesh" =>        Ok(Association::Mesh),
            "submesh" =>     Ok(Association::Submesh),
            "all" =>         Ok(Association::All),
            _ =>             Ok(Association::None),
        }
    }
}
impl std::string::ToString for Association {
    fn to_string(&self) -> String {
        match self {
            Association::Vertex =>      String::from("vertex"),
            Association::Face =>        String::from("face"),
            Association::Corner =>      String::from("corner"),
            Association::Edge =>        String::from("edge"),
            Association::Subgeometry => String::from("subgeometry"),
            Association::Instance =>    String::from("instance"),
            Association::Shapevertex => String::from("shapevertex"),
            Association::Shape =>       String::from("shape"),
            Association::Material =>    String::from("material"),
            Association::Mesh =>        String::from("mesh"),
            Association::Submesh =>     String::from("submesh"),
            Association::All =>         String::from("all"),
            Association::None =>        String::from("none"),
        }
    }
}

#[derive(Debug, PartialEq, Eq, Hash, Clone, Copy)]
pub enum InstanceFlags {
    None = 0,
    Hidden = 1,
}

/// Contains all the information necessary to parse an attribute data channel and associate it with some part of the geometry 
#[derive(Debug, PartialEq, Eq, Hash)]
pub struct AttributeDescriptor {
    /// The type of individual data values. There are n of these per element where n is the arity.
    pub data_type: DataType,
    /// The number of primitive values associated with each element 
    pub data_arity: usize,
    /// The index of the attribute.
    pub index: i32,
    /// What part of the geometry each tuple of data values is associated with 
    pub association: Association,
    /// The semantic of the attribute (e.g. normals, uv)
    pub semantic: String,
}
impl Default for AttributeDescriptor {
    fn default() -> Self {
        Self { 
            data_type: DataType::Undefined, 
            data_arity: Default::default(), 
            index: Default::default(), 
            association: Association::None, 
            semantic: Default::default() 
        }
    }
}
impl ToString for AttributeDescriptor {
    fn to_string(&self) -> String { 
        format!("g3d:{}:{}:{}:{}:{}",
            self.association.to_string(),
            self.semantic,
            self.index,
            self.data_type.to_string(),
            self.data_arity
        )
    }
}
impl std::str::FromStr for AttributeDescriptor {
    type Err = Box<dyn std::error::Error>;
    fn from_str(s: &str) -> Result<Self, Self::Err> {
        let tokens: Vec<&str> = s.split(':').collect(); 
        if tokens.len() != 6 { return Err(format!("Incorrect number of tokens: {}", tokens.len()).into()); }
        if tokens[0] != "g3d" { return Err(format!("Expected g3d, found: {}", tokens[0]).into()); }

        let association = tokens[1].parse::<Association>().unwrap_or(Association::None);
        let semantic = String::from(tokens[2]);
        let index = tokens[3].parse::<i32>().map_err(|e| e.to_string())?;
        let data_type = tokens[4].parse::<DataType>().unwrap_or(DataType::Undefined);
        let data_arity = tokens[5].parse::<usize>().map_err(|e| e.to_string())?;

        Ok(AttributeDescriptor { data_type, data_arity, index, association, semantic })
    }
}
// impl AttributeDescriptor {   
//     /// The size of each data element in bytes (not counting the arity).
//     fn data_type_size(&self) -> Result<usize, String> { self.data_type.size() }
// }

/// Manage the data buffer and meta-information of an attribute 
#[derive(Debug, PartialEq, Eq, Hash)]
pub struct Attribute {
    pub descriptor: AttributeDescriptor,
    pub begin: usize,
    pub end: usize,
}

impl Attribute {
    fn new(desc: &str, begin: usize , end: usize) -> Result<Self, Box<dyn std::error::Error>> {
        if begin > end { return Err("Data begin is after the offset end".into()); }
        let descriptor = desc.parse()?;
        let attribute = Attribute { descriptor, begin, end };
        if attribute.size() % attribute.element_size() != 0 {
            return Err("Data buffer byte size does not divide evenly by size of elements".into());
        }
        Ok(attribute)
    }
    pub fn size(&self) -> usize { self.end - self.begin }
    pub fn element_size(&self) -> usize { self.descriptor.data_type.size().unwrap_or(0) * self.descriptor.data_arity }
    pub fn count(&self) -> usize { self.size() / self.element_size() }
}
// impl From<bfast::Buffer> for Attribute {
//     fn from(buffer: bfast::Buffer) -> Self {
//         Self::new(&buffer.name, buffer.data.begin,  buffer.data.end).unwrap()
//     }
// }
// impl Into<bfast::Buffer> for Attribute {
//     fn into(self) -> bfast::Buffer {
//         bfast::Buffer { name: self.descriptor.to_string(), data: bfast::ByteRange { begin: self.begin, end : self.end } }
//     }
// }

#[derive(Debug)]
pub struct G3d {
    pub meta: String,
   // bfast: bfast::Bfast,
    pub attributes: Vec<Attribute>,
}

impl G3d {
    const DEFAULT_META: &str = "{\"G3D\": \"1.0.0\"}";

    // pub fn recompute_bfast(&self, bfast: &mut bfast::Bfast ) {
    //     bfast.buffers = self.attributes.iter().map(|attr| -> bfast::Buffer {
    //         bfast::Buffer { name: attr.descriptor.to_string(), data: bfast::ByteRange { begin: attr.begin, end : attr.end } }
    //     }).collect();
    // }

    // pub fn write_file(self, path: &str) -> Result<(), Box<dyn std::error::Error>> {
    //     let b: bfast::Bfast = self.into();
    //     b.write_file(path)
    // }
    // pub fn read_file(path: &str) -> Result<Self, Box<dyn std::error::Error>> {
    //     let bfast = bfast::Bfast::read_file(path)?;
    //     let range = bfast.unpack(None)?;
    //     Ok(range.into())
    // }

    // pub fn add_attribute(&mut self, name: &str, begin: *const u8, end: *const u8) {
    //     if let Ok(attr) = Attribute::new(name, begin, end) {
    //         self.attributes.push(attr);
    //     }
    // }
    // pub fn add_attribute_with_size(&mut self, name: &str, begin: *mut u8, size: usize) {
    //     let end = unsafe { begin.add(size) };
    //     self.add_attribute(name, begin, end);
    // }
}

impl Default for G3d {
    fn default() -> Self {
        Self { meta: Default::default(), attributes: Default::default() }
    }
}
impl From<Bfast<'_>> for G3d {
    fn from(bfast: Bfast) -> Self {
        let mut attributes = Vec::new();
        let mut meta = Self::DEFAULT_META.to_string();
        for (i, buffer) in bfast.buffers.iter().enumerate() {
            if i == 0 {
                meta = String::from_utf8_lossy(&bfast.data[buffer.range.begin..buffer.range.end]).into();
            } 
            else {
                if let Ok(attr) = Attribute::new(&buffer.name, buffer.range.begin,  buffer.range.end) {
                    attributes.push(attr);
                }
            }
        }
        G3d { meta, attributes }
    }
}
// impl Into<bfast::Bfast> for G3d {
//     fn into(self) -> bfast::Bfast {
//         let mut buffers: Vec<bfast::Buffer> = Vec::with_capacity(self.attributes.len() + 1);
//         buffers.push(bfast::Buffer::new_data("meta", &self.meta));
//         for attr in self.attributes { buffers.push(attr.into()); }
//         bfast::Bfast{ buffers }
//     }
// }