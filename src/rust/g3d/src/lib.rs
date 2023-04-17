pub mod descriptors;
pub use descriptors::*;

use std::collections::HashMap;
use std::fmt;
use std::str::FromStr;


const G3D_VERSION: (u8, u8, u8, &'static str) = (2, 2, 0, "2022.05.02");

#[derive(Debug, PartialEq, Eq, Hash, Clone, Copy)]
pub enum DataType {
    DtUint8,
    DtInt8,
    DtUint16,
    DtInt16,
    DtUint32,
    DtInt32,
    DtUint64,
    DtInt64,
    DtUint128,
    DtInt128,
    DtFloat16,
    DtFloat32,
    DtFloat64,
    DtFloat128,
    DtUndefined = 0xff,
}

#[derive(Debug, PartialEq, Eq, Hash, Clone, Copy)]
pub enum Association {
    AssocVertex,
    AssocFace,
    AssocCorner,
    AssocEdge,
    AssocSubgeometry,
    AssocInstance,
    AssocShapevertex,
    AssocShape,
    AssocMaterial,
    AssocMesh,
    AssocSubmesh,
    AssocAll,
    AssocNone,
}

#[derive(Debug, PartialEq, Eq, Hash, Clone, Copy)]
pub enum InstanceFlags {
    None = 0,
    Hidden = 1,
}

pub struct AttributeDescriptor {
    pub data_type: DataType,
    pub data_arity: i32,
    pub index: i32,
    pub association: Association,
    pub semantic: String,
}
impl Default for AttributeDescriptor {
    fn default() -> Self {
        Self { 
            data_type: DataType::DtUndefined, 
            data_arity: Default::default(), 
            index: Default::default(), 
            association: Association::AssocNone, 
            semantic: Default::default() 
        }
    }
}
impl ToString for AttributeDescriptor {
    fn to_string(&self) -> String { 
        format!("g3d:{}:{}:{}:{}:{}",
            Self::associations_to_strings().get(&self.association).unwrap(),
            self.semantic,
            self.index,
            Self::data_types_to_strings().get(&self.data_type).unwrap(),
            self.data_arity
        )
    }
}

impl AttributeDescriptor {
    /// The size of each data element in bytes (not counting the arity).
    pub fn data_type_size(&self) -> usize {
        Self::data_type_size_of(self.data_type)
    }
    /// Retrieves the size of individual data types
    pub fn data_type_size_of(dt: DataType) -> usize {
        match dt {
            DataType::DtUint8 | DataType::DtInt8 => 1,
            DataType::DtUint16 | DataType::DtInt16 => 2,
            DataType::DtUint32 | DataType::DtInt32 => 4,
            DataType::DtUint64 | DataType::DtInt64 => 8,
            DataType::DtUint128 | DataType::DtInt128 => 16,
            DataType::DtFloat16 => 2,
            DataType::DtFloat32 => 4,
            DataType::DtFloat64 => 8,
            DataType::DtFloat128 => 16,
            _ => panic!("invalid data type"),
        }
    }

    pub fn data_types_to_strings() -> HashMap<DataType, String> {
        let mut names = HashMap::with_capacity(14);
        names.insert(DataType::DtUint8, "uint8".to_string());
        names.insert(DataType::DtInt8, "int8".to_string());
        names.insert(DataType::DtUint16, "uint16".to_string());
        names.insert(DataType::DtInt16, "int16".to_string());
        names.insert(DataType::DtUint32, "uint32".to_string());
        names.insert(DataType::DtInt32, "int32".to_string());
        names.insert(DataType::DtUint64, "uint64".to_string());
        names.insert(DataType::DtInt64, "int64".to_string());
        names.insert(DataType::DtUint128, "uint128".to_string());
        names.insert(DataType::DtInt128, "int128".to_string());
        names.insert(DataType::DtFloat16, "float16".to_string());
        names.insert(DataType::DtFloat32, "float32".to_string());
        names.insert(DataType::DtFloat64, "float64".to_string());
        names.insert(DataType::DtFloat128, "float128".to_string());
        names
    }
    pub fn data_types_from_strings() -> HashMap<String, DataType> {
        Self::data_types_to_strings()
            .into_iter()
            .map(|(k, v)| (v, k))
            .collect()
    }

    pub fn associations_to_strings() -> HashMap<Association, String> {
        let mut names = HashMap::with_capacity(13);
        names.insert(Association::AssocVertex, "vertex".to_string());
        names.insert(Association::AssocFace, "face".to_string());
        names.insert(Association::AssocCorner, "corner".to_string());
        names.insert(Association::AssocEdge, "edge".to_string());
        names.insert(Association::AssocSubgeometry, "subgeometry".to_string());
        names.insert(Association::AssocInstance, "instance".to_string());
        names.insert(Association::AssocShapevertex, "shapevertex".to_string());
        names.insert(Association::AssocShape, "shape".to_string());
        names.insert(Association::AssocMaterial, "material".to_string());
        names.insert(Association::AssocMesh, "mesh".to_string());
        names.insert(Association::AssocSubmesh, "submesh".to_string());
        names.insert(Association::AssocAll, "all".to_string());
        names.insert(Association::AssocNone, "none".to_string());
        names
    }
    pub fn associations_from_strings() -> HashMap<String, Association> {
        Self::associations_to_strings()
            .into_iter()
            .map(|(k, v)| (v, k))
            .collect()
    }

    fn split(s: &str, delim: char) -> Vec<String> {
        s.split(delim).map(|s| s.to_string()).collect()
    }

    pub fn association_from_string(s: &str) -> Result<Association, String> {
        Self::associations_from_strings()
            .get(s)
            .cloned()
            .ok_or(format!("unknown association: {}", s))
    }

    pub fn data_type_from_string(s: &str) -> Result<DataType, String> {
        Self::data_types_from_strings()
            .get(s)
            .cloned()
            .ok_or(format!("unknown data-type: {}", s))
    }

    pub fn from_string(s: &str) -> Result<AttributeDescriptor, String> {
        let tokens = Self::split(s, ':');
        if tokens.len() != 6 {
            return Err(format!("Incorrect number of tokens: {}", tokens.len()));
        }

        if tokens[0] != "g3d" {
            return Err(format!("Expected g3d, found: {}", tokens[0]));
        }

        let association = Self::association_from_string(&tokens[1])?;
        let semantic = tokens[2].clone();
        let index = tokens[3].parse::<i32>().map_err(|e| e.to_string())?;
        let data_type = Self::data_type_from_string(&tokens[4])?;
        let data_arity = tokens[5].parse::<i32>().map_err(|e| e.to_string())?;

        Ok(AttributeDescriptor {
            data_type,
            data_arity,
            index,
            association,
            semantic,
        })
    }
}

pub struct Attribute {
    pub descriptor: AttributeDescriptor,
    begin: *const u8,
    end: *const u8,
}

impl Attribute {
    pub fn new(desc: &str, begin: *const u8, end: *const u8) -> Result<Self, String> {
        if begin.is_null() || end.is_null() {
            return Err("Null parameters".to_string());
        }

        let descriptor = AttributeDescriptor::from_string(desc)?;
        let attribute = Attribute {
            descriptor,
            begin,
            end,
        };

        if attribute.byte_size() % attribute.data_element_size() != 0 {
            return Err("Data buffer byte size does not divide evenly by size of elements".to_string());
        }

        Ok(attribute)
    }

    pub fn byte_size(&self) -> usize {
        (self.end as usize) - (self.begin as usize)
    }

    pub fn data_element_size(&self) -> usize {
        self.descriptor.data_type_size() * self.descriptor.data_arity as usize
    }

    pub fn num_elements(&self) -> usize {
        self.byte_size() / self.data_element_size()
    }

    pub fn to_buffer(&self) -> bfast::Buffer {
        bfast::Buffer {
            name: self.descriptor.to_string(),
            data: bfast::ByteRange { begin: self.begin, end : self.end },
        }
    }

    pub fn from_buffer(buffer: bfast::Buffer) -> Result<Self, String> {
        Self::new(&buffer.name, buffer.data.begin,  buffer.data.end)
    }
}

pub struct G3d {
    pub meta: String,
    bfast: bfast::Bfast,
    pub attributes: Vec<Attribute>,
}

impl G3d {
    // pub fn new() -> Self {
    //     G3d {
    //         meta: Self::default_meta(),
    //         bfast: bfast::Bfast::default(),
    //         attributes: Vec::new(),
    //     }
    // }

    pub fn from_bfast(input_bfast: bfast::Bfast) -> Self {
        let mut attributes = Vec::new();
        let mut meta = String::new();

        for (i, buffer) in input_bfast.buffers.iter().enumerate() {
            if i == 0 {
                meta = buffer.data.to_string()// String::from_utf8_lossy(&buffer.data).to_string();
            } else {
                if let Ok(attr) = Attribute::from_buffer(*buffer) {
                    attributes.push(attr);
                }
            }
        }

        G3d {
            meta,
            bfast: input_bfast,
            attributes,
        }
    }

    fn default_meta() -> String {
        "{\"G3D\": \"1.0.0\"}".to_string()
    }

    pub fn recompute_bfast(&mut self) {
        self.bfast.buffers = self
            .attributes
            .iter()
            .map(|attr| attr.to_buffer())
            .collect();
    }

    pub fn write_file(&self, path: &str) -> Result<(), Box<dyn std::error::Error>> {
        let mut b = bfast::Bfast::default();
        b.add("meta", self.meta.as_bytes());

        for attr in &self.attributes {
            b.buffers.push(attr.to_buffer());
        }

        b.write_file(path)
    }

    pub fn read_file(path: &str) -> Result<Self, Box<dyn std::error::Error>> {
        let bfast = bfast::Bfast::read_file(path)?;
        Ok(Self::from_bfast(bfast))
    }

    pub fn add_attribute(&mut self, name: &str, begin: *const u8, end: *const u8) {
        if let Ok(attr) = Attribute::new(name, begin, end) {
            self.attributes.push(attr);
        }
    }

    pub fn add_attribute_with_size(&mut self, name: &str, begin: *mut u8, size: usize) {
        let end = unsafe { begin.add(size) };
        self.add_attribute(name, begin, end);
    }
}

