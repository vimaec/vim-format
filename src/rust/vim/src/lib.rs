use std::{collections::HashMap, ops::Index};
use std::fs::File;
use std::io::Read;
use std::str;
use std::string::String;
use bfast::{self, ByteRange, Header, Buffer, Bfast};
use g3d::G3d;

#[derive(Debug, PartialEq, Eq, Hash, Clone, Copy)]
pub enum VimErrorCodes {
    Success = 0,
    Failed = -1,
    NoVersionInfo = -2,
    FileNotRecognized = -3,
    GeometryLoadingException = -4,
    AssetLoadingException = -5,
    EntityLoadingException = -6,
}
impl std::fmt::Display for VimErrorCodes {
    fn fmt(&self, f: &mut std::fmt::Formatter) -> std::fmt::Result { write!(f, "{:?}", self) }
}
impl std::error::Error for VimErrorCodes {}

#[derive(Debug, PartialEq, Eq, Hash, Clone, Copy)]
pub enum VimLoadFlags {
    Geometry = 1,
    Assets = 2,
    Strings = 4,
    Entities = 8,
    All = VimLoadFlags::Geometry as isize | VimLoadFlags::Assets as isize | VimLoadFlags::Strings as isize | VimLoadFlags::Entities as isize,
}

#[derive(Debug)]
pub struct EntityTable {
    pub name: String,
    pub index_columns: HashMap<String, Vec<i32>>,
    pub string_columns: HashMap<String, Vec<i32>>,
    pub data_columns: HashMap<String, ByteRange>,
}

// fn split(str: &str, delim: &str) -> Vec<String> {
//     let mut tokens = Vec::new();
//     let mut prev = 0;
//     let mut pos = 0;
//     while pos < str.len() && prev < str.len() {
//         pos = str[prev..].find(delim).unwrap_or(str.len());
//         let token = str[prev..prev + pos].to_string();
//         if !token.is_empty() {
//             tokens.push(token);
//         }
//         prev += pos + delim.len();
//     }
//     tokens
// }

#[derive(Debug)]
pub struct VimScene<'a> {
   // bfast: bfast::Bfast,

    //geometry_bfast: bfast::Bfast,
    assets_bfast: bfast::Bfast<'a>,
    entities_bfast: bfast::Bfast<'a>,
    strings: Vec<String>,
    pub geometry: G3d,
    pub entity_tables: HashMap<String, EntityTable>,
    header: HashMap<String, String>,
    version_major: u32,
    version_minor: u32,
    version_patch: u32,
}

#[derive(Debug)]
pub struct Version {
    pub major: u32,
    pub minor: u32,
    pub patch: u32,
}
impl Default for Version {
    fn default() -> Self { Self { major: 0xffffffff, minor: 0xffffffff, patch: 0xffffffff } }
}
impl ToString for Version {
    fn to_string(&self) -> String {
        format!("{}.{}.{}", self.major, self.minor, self.patch)
    }
}
impl std::str::FromStr for Version {
    type Err = Box<dyn std::error::Error>;
    fn from_str(s: &str) -> Result<Self, Self::Err> {
        let mut v = Version::default();
        for (i, part)  in s.split('.').enumerate() {
            match i {
                0 => v.major = part.parse()?,
                1 => v.minor = part.parse()?,
                2 => v.patch = part.parse()?,
                _ => break
            }
        }
        Ok(v)
    }
}

 
impl<'a> VimScene<'a> {
    fn version(data: &[u8], b: &Buffer) -> Result<Version, Box<dyn std::error::Error>> {
        let h = &data[b.range.begin..b.range.end];
        let header = std::str::from_utf8(h)?;
        for token in header.split('\n') {
            let kv: Vec<&str> = token.split("=").collect();
            if kv.len() == 2 {
                if kv[0] == "vim" { 
                    return Ok(kv[1].parse::<Version>()?); 
                }
            }
        }
        Err(Box::new(VimErrorCodes::NoVersionInfo))
    }

    pub fn read_file(path: &str, load_flags: VimLoadFlags) -> Result<Self, Box<dyn std::error::Error>>  {
        let mut mVersionMajor: u32 = 0xffffffff;
        let mut mVersionMinor: u32 = 0xffffffff;
        let mut mVersionPatch: u32 = 0xffffffff;

        let mut mHeader: HashMap<String, String> = HashMap::new();
        let mut mGeometryBFast: bfast::Bfast = bfast::Bfast { data: &Vec::new(), buffers: Vec::new() };
        let mut mGeometry: G3d = G3d::default();
        let mut mAssetsBFast: bfast::Bfast = bfast::Bfast { data: &Vec::new(), buffers: Vec::new() };
        let mut version = Version::default();
        let mut strings: Vec<String> = Vec::new();

        let mut mEntityTables: HashMap<String, EntityTable> = HashMap::new();

        let buffer = std::fs::read(path)?;
        let slice: &[u8] = &buffer;
        let bfast: Bfast = slice.into();

        let ui_load_flags = load_flags as isize;
        for b in &bfast.buffers {
            match b.name.as_str() {
                "header" => version = Self::version(bfast.data, b).unwrap_or(Version::default()),
                "geometry" if (ui_load_flags & VimLoadFlags::Geometry as isize) != 0 => {
                    let m_geometry_bfast: Bfast = (&slice[b.range.begin..b.range.end]).into();
                    mGeometry = g3d::G3d::from(m_geometry_bfast);
                },
                "assets" if (ui_load_flags & VimLoadFlags::Assets as isize) != 0 => {
                    mAssetsBFast = (&slice[b.range.begin..b.range.end]).into();
                    // match bfast::Bfast::unpack_range(&b.data, |b| bfast::Bfast { buffers: b }) {
                    //     Ok(b) => mAssetsBFast = b,
                    //     Err(_e) => return Err(Box::new(VimErrorCodes::AssetLoadingException)),
                    // }
                },
                "strings" if (ui_load_flags & VimLoadFlags::Strings as isize) != 0 => {
                    let strs: Vec<&str> = std::str::from_utf8(&bfast.data[b.range.begin..b.range.end])?
                        .split_terminator('\0')
                        .collect();
                    strings = strs.iter().map(|s| String::from(*s)).collect();
                //    let str = b.data.to_string();
                //     let size = b.data.data();
                //     for (i, v) in size.split(|&c| c == ZERO_BYTE).enumerate() {
                //         let name = String::from_utf8_lossy(v).to_string(); 
                //         strings.push(name);
                //     }
                    print!("") 
                },
                "entities" if (ui_load_flags & VimLoadFlags::Entities as isize) != 0  => {
                    let entities_bfast: Bfast = (&slice[b.range.begin..b.range.end]).into();
                    for (j, eb) in entities_bfast.buffers.iter().enumerate() {
                        let table_bfast: Bfast = (&entities_bfast.data[eb.range.begin..eb.range.end]).into();

                        let mut index_columns: HashMap<String, Vec<i32>> = HashMap::new();
                        let mut string_columns: HashMap<String, Vec<i32>> = HashMap::new();
                        let mut data_columns: HashMap<String, ByteRange> = HashMap::new();

                        for (k, tb) in table_bfast.buffers.iter().enumerate() {
                            let tbname: &str = &tb.name;
                            if let Some(index) = tbname.find(':') {
                                let type_str = &tbname[0..index];
                                let column_str = &tbname[(index + 1)..tbname.len()];

                                if ["int", "byte", "double", "float"].contains(&type_str) {
                                    data_columns.insert(column_str.to_owned(), tb.range);
                                } else if "index" == type_str {
                                    let b = &table_bfast.data[tb.range.begin..tb.range.end];
                                    let ints: Vec<i32> = b.chunks_exact(4).map(|bytes| i32::from_ne_bytes([bytes[0], bytes[1], bytes[2], bytes[3]])).collect();
                                    index_columns.insert(column_str.to_owned(), ints);
                                } else if "string" == type_str {
                                    let b = &table_bfast.data[tb.range.begin..tb.range.end];
                                    let ints: Vec<i32> = b.chunks_exact(4).map(|bytes| i32::from_ne_bytes([bytes[0], bytes[1], bytes[2], bytes[3]])).collect();
                                    string_columns.insert(column_str.to_owned(), ints);
                                }
                            }

                        }
                        mEntityTables.insert(eb.name.to_string(), EntityTable {
                            name: eb.name.to_string(),
                            index_columns,
                            string_columns,
                            data_columns,
                        });
                    }
                }
                &_ => return Err(Box::new(VimErrorCodes::Failed))
            }
        }
        return Err(Box::new(VimErrorCodes::Failed))
        // Ok(VimScene {
        //     //bfast,
        //    // geometry_bfast: mGeometryBFast,
        //     assets_bfast: mAssetsBFast,
        //     entities_bfast: bfast::Bfast { data: &Vec::new()  },
        //     strings: strings,
        //     geometry: mGeometry,
        //     entity_tables: HashMap::new(),
        //     header: mHeader,
        //     version_major: mVersionMajor,
        //     version_minor: mVersionMinor,
        //     version_patch: mVersionPatch,
        // }) 
        
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn it_works() {
        let res = VimScene::read_file("D:\\wolford.vim", VimLoadFlags::All);
        print!("{:?}", res);
        assert!(res.is_ok());
        let scene = res.unwrap();
        print!("{:?}", scene);
    }
}