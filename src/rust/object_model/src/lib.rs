use std::{collections::HashMap, ops::Index};
use std::fs::File;
use std::io::Read;
use std::str;
use std::string::String;
use bfast::{self, ByteRange, Header, Buffer, Bfast};
use g3d::G3d;
use vim::EntityTable;

pub struct Asset<'a> {
    pub index: usize,
    pub buffer_name: &'a str,
}

pub struct AssetTable<'a> {
    pub entity_table: &'a EntityTable<'a>,
    pub strings: Vec<&'a str>,
}