use std::collections::HashMap;
use std::str;
use std::string::String;
use bfast::{self, ByteRange, Buffer, Bfast};
use g3d::G3d;

#[derive(Debug, PartialEq, Eq, Hash, Clone, Copy)]
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
pub struct EntityTable<'a> {
    pub data: &'a [u8], 
    pub name: &'a str,
    pub columns: HashMap<&'a str, ByteRange>,
}

#[derive(Debug)]
pub struct VimHeader {
    pub vim: Version,
    pub id: String,
    pub revision: String,
    pub generator: String,
    pub created: String,
    pub schema: String,
}
impl Default for VimHeader {
    fn default() -> Self { Self { vim: Default::default(), id: Default::default(), revision: Default::default(), generator: Default::default(), created: Default::default(), schema: Default::default() } }
}

#[derive(Debug)]
pub struct VimScene<'a> {
    pub header: VimHeader,
    pub assets: bfast::Bfast<'a>,
    pub geometry: G3d<'a>,
    pub strings: Vec<&'a str>,
    pub entities: HashMap<&'a str, EntityTable<'a>>,
}

impl<'a> VimScene<'a> {
    pub fn unpack(data: &'a [u8], flags: VimLoadFlags) -> Result<Self, Box<dyn std::error::Error>>  {
        let bfast = &Bfast::unpack(data)?;
        let ui_load_flags = flags as isize;
        let data: HashMap<&str, &Buffer> = HashMap::from_iter(bfast.buffers.iter().map(|b| (b.name, b)));
        
        Ok(VimScene::<'a> {
            header: {
                if let Some(b) = data.get("header") {
                    let h = &bfast.data[b.range.begin..b.range.end];
                    let map: HashMap<&str, &str> = HashMap::from_iter(std::str::from_utf8(h)?.split_terminator('\n').map(|token| { 
                        let kv: Vec<&str> = token.split("=").collect();
                        (kv[0], kv[1])
                    }));
                    VimHeader { 
                        vim : map.get("vim").unwrap_or(&"").parse::<Version>().map_err(|_| VimErrorCodes::NoVersionInfo)?, 
                        id: (*map.get("id").unwrap_or(&"")).to_owned(),
                        revision: (*map.get("revision").unwrap_or(&"")).to_owned(),
                        generator: (*map.get("generator").unwrap_or(&"")).to_owned(),
                        created: (*map.get("created").unwrap_or(&"")).to_owned(),
                        schema: (*map.get("schema").unwrap_or(&"")).to_owned(),
                    }
                }
                else { return Err("Invalid Header".into()); }
            },
            geometry: {
                if (ui_load_flags & VimLoadFlags::Geometry as isize) != 0 {
                    if let Some(b) = data.get("geometry") {
                        let g = &bfast.data[b.range.begin..b.range.end];
                        G3d::unpack(Bfast::unpack(g)?)?
                    }
                    else { return Err("Invalid Geometry".into()); }
                } else { G3d::default() }
            },
            assets: {
                if (ui_load_flags & VimLoadFlags::Assets as isize) != 0 {
                    if let Some(b) = data.get("assets") {
                        let a = &bfast.data[b.range.begin..b.range.end];
                        Bfast::unpack(a)?
                    }
                    else { return Err("Invalid Assets".into()); }
                } else { Bfast::default() }
            },
            strings: {
                if (ui_load_flags & VimLoadFlags::Strings as isize) != 0 {
                    if let Some(b) = data.get("strings") {
                        let s = &bfast.data[b.range.begin..b.range.end];
                        std::str::from_utf8(s)?.split_terminator('\0').collect()
                    }
                    else { return Err("Invalid Strings".into()); }
                } else { Vec::default() }
            },
            entities: {
                if (ui_load_flags & VimLoadFlags::Entities as isize) != 0 {
                    if let Some(b) = data.get("entities") {
                        let et = &bfast.data[b.range.begin..b.range.end];
                        let entities_bfast = Bfast::unpack(et)?;
                        let mut entity_tables: HashMap<&str, EntityTable> = HashMap::new();
                        for eb in entities_bfast.buffers.iter() {
                            let data = &entities_bfast.data[eb.range.begin..eb.range.end];
                            let table = Bfast::unpack(data)?;
                            let columns = HashMap::from_iter(table.buffers.iter().map(|tb| (tb.name, tb.range)));
                            entity_tables.insert(eb.name, EntityTable { name: eb.name, columns, data });
                        }
                        entity_tables
                    }
                    else { return Err("Invalid Entities".into()); }
                } else { HashMap::default() }
            },
        })
    }
}
impl<'a> Default for VimScene<'a> {
    fn default() -> Self { Self { header: Default::default(), assets: Default::default(), geometry: Default::default(), strings: Default::default(), entities: Default::default()  } }
}
impl<'a> From<&'a [u8]> for VimScene<'a> {
    fn from(slice: &'a [u8]) -> Self { Self::from((slice, VimLoadFlags::All)) }
}
impl<'a> From<(&'a [u8], VimLoadFlags)> for VimScene<'a> {
    fn from((slice, flags): (&'a [u8], VimLoadFlags)) -> Self {
        if let Ok(scene) = Self::unpack(slice, flags) { scene } 
        else { Default::default() }
    }
}


#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn it_works() {
        let buffer:&[u8] = &std::fs::read("D:\\wolford.vim").unwrap();
        let res = VimScene::unpack(buffer, VimLoadFlags::All).unwrap();
        let asset = res.entities.get("Vim.Asset").unwrap();
        
        print!("{} {} {}", asset.columns.len(), asset.columns.len(), asset.columns.len());
       // print!("{:?}", res);
         
    }
}