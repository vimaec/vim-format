use std::io::{Read, Write};
use std::vec::Vec;

pub const BFAST_VERSION: (u8, u8, u8, &'static str) = (1, 0, 1, "2019.9.24");
/// Magic numbers for identifying a BFAST format
pub const MAGIC: u64 = 0xBFA5;
pub const TMP: u64 = 0xA5BF;
pub const SWAPPED_MAGIC: u64 = TMP << 48;
/// The size of the header
pub const HEADER_SIZE: usize = 32;
/// The size of array offsets 
pub const ARRAY_OFFSET_SIZE: usize = 16;
/// This is the size of the header + padding to bring to alignment 
const ARRAY_OFFSETS_START: usize = 32;
/// This is sufficient alignment to fit objects natively into 256-bit (32 byte) registers 
const ALIGNMENT: usize = 64;
const ZERO_BYTE: u8 = '\0' as u8;

/// The array offset indicates where in the raw byte array (offset from beginning of BFAST byte stream) that a particular array's data can be found. 
#[repr(align(8))]
#[derive(Debug, PartialEq, Eq, Hash, Clone, Copy)]
pub struct ArrayOffset {
    pub begin: u64,
    pub end: u64,
}
// impl Default for ArrayOffset {
//     fn default() -> Self {
//         Self { begin: Default::default(), end: Default::default() }
//     }
// }

/// A data structure at the top of the file. This is followed by 32 bytes of padding, then an array of n array_offsets (where n is equal to num_arrays)
#[repr(align(8))]
#[derive(Debug, PartialEq, Eq, Hash, Clone, Copy)]
pub struct Header {
    /// Either MAGIC (same-endian) of SWAPPED_MAGIC (different-endian)
    magic: u64,
    /// >= desc_end and modulo 64 == 0 and <= file_size
    data_start: u64,
    /// >= data_start and <= file_size
    data_end: u64,
    /// number of array_headers
    num_arrays: u64,
}
// impl Default for Header {
//     fn default() -> Self {
//         Self { magic: Default::default(), data_start: Default::default(), data_end: Default::default(), num_arrays: Default::default() }
//     }
// }

/// A helper struct for representing a range of bytes 
#[derive(Debug, PartialEq, Eq, Hash, Clone, Copy)]
pub struct ByteRange {
    pub begin: usize,
    pub end: usize
}
impl ByteRange {
    // pub fn begin(&self) -> *const u8 { return self.begin }
    // pub fn end(&self) -> *const u8 { return self.end }
    //pub fn data(&self) -> &[u8] { unsafe { std::slice::from_raw_parts(self.begin, self.size()) } }
    //pub fn size(&self) -> usize { unsafe { self.end.offset_from(self.begin) as usize } }
    pub fn size(&self) -> usize { self.end - self.begin }
}

// impl ToString for ByteRange {
//     fn to_string(&self) -> String {
//         String::from_utf8_lossy(self.data()).into_owned()
//     }
// }

/// A Bfast buffer conceptually is a name and a byte-range
#[derive(Debug, PartialEq, Eq, Hash)] //, Clone, Copy
pub struct Buffer {
    pub name: String,
    pub range: ByteRange,
}
// impl Buffer {
//     pub fn new(name: &str, begin: *const u8, end: *const u8) -> Self {
//         Self { name: String::from(name), data: ByteRange { begin, end } }
//     }
//     pub fn new_data(name: &str, data: &str) -> Self {
//         let begin = data.as_ptr();
//         let end = unsafe { begin.add(data.len()) };
//         Self { name: String::from(name), data: ByteRange { begin, end } }
//     }
// }

/// The Bfast container implementation is a container of date ranges: the first one contains the names 
#[derive(Debug)]
struct RawData {
    // Each data buffer 
    ranges: Vec<ByteRange>,
}

impl RawData {
    /// Returns true if the given value is aligned. 
    fn is_aligned(n: usize) -> bool { n % ALIGNMENT == 0 }
    /// Returns an aligned version of the given value to bring it to alignment 
    fn aligned_value(n: usize) -> usize {
        if Self::is_aligned(n) { n } else {
            let r = n + ALIGNMENT - (n % ALIGNMENT);
            assert!(Self::is_aligned(r));
            r
        }
    }
    /// Computes where the data offsets are relative to the beginning of the BFAST byte stream.
    fn compute_offsets(&self) -> Vec<ArrayOffset> {
        let mut n = self.compute_data_start();
        let mut offsets = Vec::with_capacity(self.ranges.len());
        for range in &self.ranges {
            debug_assert!(Self::is_aligned(n));
            offsets.push(ArrayOffset { begin: n as u64, end: (n + range.size()) as u64 });
            n = Self::aligned_value(n + range.size());
        }
        offsets
    }
    /// Computes where the first array data starts 
    fn compute_data_start(&self) -> usize {
        let mut r = HEADER_SIZE;
        r += ARRAY_OFFSET_SIZE * self.ranges.len();
        Self::aligned_value(r)
    }
    /// Computes how many bytes are needed to store the current BFAST blob
    fn compute_needed_size(&self) -> usize {
        let tmp = self.compute_offsets();
        if tmp.is_empty() {
            self.compute_data_start()
        } else {
            Self::aligned_value(tmp.last().unwrap().end as usize)
        }
    }
    /// Copies the data structure to the bytes stream and update the current index
    fn copy_item<T>(x: &T, out: &mut Vec<u8>) -> usize {
        let begin = x as *const T as *const u8;
        let size = std::mem::size_of::<T>();
        let end = unsafe { begin.add(size) };
        let slice = unsafe { std::slice::from_raw_parts(begin, end.offset_from(begin) as usize) };
        out.extend_from_slice(slice);
        size
    }
    /// Adds zero bytes to the bytes stream for null padding 
    fn output_padding(out: &mut Vec<u8>, current: &usize) -> usize {
        let mut size: usize = 0;
        while !Self::is_aligned(*current + size) {
            out.push(ZERO_BYTE);
            size += 1;
        }
        size
    }
    /// Copies the BFAST data structure to any output iterator
    fn copy_to(&self, out: &mut Vec<u8>) {
         // Initialize and get the data offsets 
        let offsets = self.compute_offsets();
        debug_assert!(offsets.len() == self.ranges.len());
        let n = offsets.len();
        let mut current = 0;
    
        // Fill out the header
        let h = Header {
            magic: MAGIC,
            num_arrays: n as u64,
            data_start: if n == 0 { 0 } else { offsets[0].begin },
            data_end: if n == 0 { 0 } else { offsets[n - 1].end },
        };
        
        // Copy the header 
        current += Self::copy_item(&h, out);
        debug_assert!(current == 32);
        // Early escape if there are no offsets 
        if n == 0 { return; }
        
        // Copy the array offsets and add padding 
        for off in &offsets {
            current += Self::copy_item(off, out);
        }
        debug_assert!(current == 32 + offsets.len() * 16);
        current += Self::output_padding(out, &current);
        debug_assert!(Self::is_aligned(current));
        debug_assert!(current == self.compute_data_start());
    
        // Copy the arrays 
        for i in 0..self.ranges.len() {
            current += Self::output_padding(out, &current);
            let range = &self.ranges[i];
            let offset = &offsets[i];
            debug_assert!(range.size() == (offset.end - offset.begin) as usize);
            debug_assert!(current == offset.begin as usize);          
           // let slice = unsafe { std::slice::from_raw_parts(range.begin, range.end.offset_from(range.begin) as usize) };
           // out.extend_from_slice(range.data);
            current += range.size();
            debug_assert!(current == offset.end as usize);
        }
    }
    
    
    // Converts the BFast into a byte-array.
    fn pack(&self) -> Vec<u8> {
        let needed_size = self.compute_needed_size();
        let mut r = Vec::with_capacity(needed_size); 
        self.copy_to(&mut r);
        r
    }
    
    // // Unpacks a vector of bytes into a RawData
    // pub fn unpack(range: &ByteRange) -> Result<RawData, Box<dyn std::error::Error>> {
    //     let h = unsafe { &*(range.begin as *const Header) };
    //     if h.magic != MAGIC { return Err("invalid magic number, either not a BFast, or was created on a machine with different endianess".into()); }
    //     if h.data_end < h.data_start { return Err("data ends before it starts".into()); }
        
    //     let size = h.num_arrays as usize;
    //     let array_offsets = unsafe { std::slice::from_raw_parts((range.begin.offset(ARRAY_OFFSETS_START)) as *const ArrayOffset, size) };
    //     let mut r = RawData {  ranges: Vec::with_capacity(size) };
    
    //     for i in 0..size {
    //         let offset = &array_offsets[i];
    //         if offset.begin > offset.end { return Err("Offset begin is after the offset end".into()); }
    //         if offset.end as usize > range.size() { return Err("Offset end is after the end of the data".into()); }
    //         if i > 0 && offset.begin < array_offsets[i - 1].end { return Err("Offset begin is before the end of the previous offset".into()); }
            
    //         let begin = unsafe { range.begin.offset(offset.begin as isize) };
    //         let end = unsafe { range.end.offset(offset.end as isize) };
    //         r.ranges.push(ByteRange{ begin, end })
    //     }
    //     Ok(r)
    // }
}

/// A Bfast conceptually is a collection of buffers: named byte arrays. 
/// It contains the raw data contained within. 
#[derive(Debug)]
pub struct Bfast<'a> { 
    pub data: &'a [u8], 
    pub buffers: Vec<Buffer>,
}
impl<'a> Bfast<'a> {
    // pub fn range(&self, range: &ByteRange) -> &[u8] { &self.data[range.begin..range.end] }
    // pub fn buffer(&self, buffer: &Buffer) -> &[u8] { &self.data[buffer.range.begin..buffer.range.end] }
    
    pub fn pack(buffers: Vec<Buffer>) -> Vec<u8> {
        // // Compute the name data
        // name_data.clear();

        // size_t count = 0;
        // for (auto b : buffers)
        // {
        //     for (auto c : b.name)
        //         count++;
        //     count++;
        // }

        // name_data.resize(count);
        // count = 0;
        // for (auto b : buffers)
        // {
        //     for (auto c : b.name)
        //         name_data[count++] = c;
        //     name_data[count++] = 0;
        // }

        // RawData r;
        // size_t index = 0;
        // r.ranges.resize(1 + buffers.size());
        // r.ranges[index++] = ByteRange{ name_data.data(), name_data.data() + name_data.size() };
        // for (auto b : buffers)
        //     r.ranges[index++] = b.data;
        // return r;

        let count = buffers.len();
        let name_data: Vec<u8> = Vec::with_capacity(count);
        
        let mut ranges = Vec::with_capacity(1 + buffers.len());
       //ranges.push(ByteRange { begin: name_data.as_ptr(), end: unsafe { name_data.as_ptr().add(name_data.len() as usize) } }); 
        for b in &buffers { ranges.push(b.range) }
        let r = RawData { ranges };
        r.pack()
    }

    ///Unpacks an array of buffers into a BFastData package    
    pub fn unpack(data: &[u8], range: Option<ByteRange>) -> Result<Vec<Buffer>, Box<dyn std::error::Error>> {
        const HEADER_SIZE: usize = std::mem::size_of::<Header>();
        const ARRAY_OFFSET_SIZE: usize = std::mem::size_of::<ArrayOffset>();

        let range = range.unwrap_or(ByteRange { begin: 0, end: data.len() });
        let header = &data[0..HEADER_SIZE];
        let h = unsafe { &*(header.as_ptr() as *const Header) };
        if h.magic != MAGIC { return Err("invalid magic number, either not a BFast, or was created on a machine with different endianess".into()); }
        if h.data_end < h.data_start { return Err("data ends before it starts".into()); }

        let size = h.num_arrays as usize;
        let mut ranges: Vec<ByteRange> = Vec::with_capacity(size);
        
        let start = range.begin + ARRAY_OFFSETS_START;
        let mut prev: Option<&ArrayOffset> = None;
        for i in 0..size {
            let index = start + i * ARRAY_OFFSET_SIZE;
            let slice = &data[index..(index + ARRAY_OFFSET_SIZE)];
            let offset = unsafe { &*(slice.as_ptr() as *const ArrayOffset) };
            if (*offset).begin > offset.end { return Err("Offset begin is after the offset end".into()); }
            if offset.end as usize > range.size() { return Err("Offset end is after the end of the data".into()); }
            if let Some(p) = prev { if offset.begin < p.end { return Err("Offset begin is before the end of the previous offset".into()); } }
            
            let begin = range.begin + offset.begin as usize;
            let end = range.begin + offset.end as usize;
            ranges.push(ByteRange{ begin, end });
            prev = Some(offset);
        }

        let names_range = ranges.first().ok_or("Empty ranges list")?;
        let names: Vec<&str> = std::str::from_utf8(&data[names_range.begin..names_range.end])?
            .split_terminator('\0')
            .collect();
        if names.len() != ranges.len() - 1 { return Err("The number of names does not match the raw data size".into()) }
        let buffers = names.iter().enumerate()
            .map(|(i, name)| Buffer { name: String::from(*name), range: ranges[i + 1] })
            .collect();
        Ok(buffers)
    }

    pub fn write_file(buffers: Vec<Buffer>, file: &str) -> Result<(), Box<dyn std::error::Error>> {
        let data = Self::pack(buffers);
        let mut fstrm = std::fs::File::create(std::path::Path::new(file))?;
        fstrm.write_all(&data)?;
        Ok(())
    }
    
}



// impl Default for Bfast {
//     fn default() -> Self {
//         Self { buffers: Default::default() }
//     }
// }
impl<'a> From<&'a [u8]> for Bfast<'a> {
    fn from(data: &'a [u8]) -> Self {
        if let Ok(b) = Self::unpack(data, None) { Self { data, buffers : b } } 
        else { Self { data, buffers: Vec::new() } }
    }
}
impl<'a> Into<&'a [u8]> for Bfast<'a> {
    fn into(self) -> &'a [u8] { self.data }
}

pub fn read_bytes(file: &str) -> Result<Vec<u8>, Box<dyn std::error::Error>> {
    Ok(std::fs::read(file)?)
}