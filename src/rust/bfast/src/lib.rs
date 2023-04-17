use std::io::{Read, Write};
use std::vec::Vec;

pub const BFAST_VERSION: (u8, u8, u8, &'static str) = (1, 0, 1, "2019.9.24");
/// Magic numbers for identifying a BFAST format
const MAGIC: u64 = 0xBFA5;
const TMP: u64 = 0xA5BF;
const SWAPPED_MAGIC: u64 = TMP << 48;
/// The size of the header
pub const HEADER_SIZE: usize = 32;
/// The size of array offsets 
pub const ARRAY_OFFSET_SIZE: usize = 16;
/// This is the size of the header + padding to bring to alignment 
const ARRAY_OFFSETS_START: isize = 32;
/// This is sufficient alignment to fit objects natively into 256-bit (32 byte) registers 
const ALIGNMENT: usize = 64;
const ZERO_BYTE: u8 = (0 as char) as u8;

/// The array offset indicates where in the raw byte array (offset from beginning of BFAST byte stream) that a particular array's data can be found. 
#[repr(align(8))]
#[derive(Debug, Clone, Copy)]
pub struct ArrayOffset {
    pub begin: u64,
    pub end: u64,
}
impl Default for ArrayOffset {
    fn default() -> Self {
        Self { begin: Default::default(), end: Default::default() }
    }
}

/// A data structure at the top of the file. This is followed by 32 bytes of padding, then an array of n array_offsets (where n is equal to num_arrays)
#[repr(align(8))]
#[derive(Debug, Clone, Copy)]
pub struct Header {
    magic: u64,         // Either MAGIC (same-endian) of SWAPPED_MAGIC (different-endian)
    data_start: u64,    // >= desc_end and modulo 64 == 0 and <= file_size
    data_end: u64,      // >= data_start and <= file_size
    num_arrays: u64,    // number of array_headers
}
impl Default for Header {
    fn default() -> Self {
        Self { 
            magic: Default::default(), 
            data_start: Default::default(), 
            data_end: Default::default(), 
            num_arrays: Default::default() 
        }
    }
}

/// A helper struct for representing a range of bytes 
#[derive(Debug, Clone, Copy)]
pub struct ByteRange {
    begin: *const u8,
    end: *const u8,
}
impl ByteRange {
    pub fn begin(&self) -> *const u8 { return self.begin }
    pub fn end(&self) -> *const u8 { return self.end }
    pub fn size(&self) -> usize { unsafe { self.end.offset_from(self.begin) as usize } }
}
impl ToString for ByteRange {
    fn to_string(&self) -> String {
        let slice = unsafe { std::slice::from_raw_parts(self.begin, self.size()) };
        String::from_utf8_lossy(slice).into_owned()
    }
}

/// A Bfast buffer conceptually is a name and a byte-range
#[derive(Debug, Clone)]
pub struct Buffer {
    pub name: String,
    pub data: ByteRange,
}

/// The Bfast container implementation is a container of date ranges: the first one contains the names 
pub struct RawData {
    // Each data buffer 
    pub ranges: Vec<ByteRange>,
}

impl RawData {
    /// Returns true if the given value is aligned. 
    #[inline(always)]
    fn is_aligned(n: usize) -> bool { n % ALIGNMENT == 0 }
    /// Returns an aligned version of the given value to bring it to alignment 
    #[inline(always)]
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
            let slice = unsafe { std::slice::from_raw_parts(range.begin(), range.end().offset_from(range.begin()) as usize) };
            out.extend_from_slice(slice);
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
    
    // Unpacks a vector of bytes into a RawData
    pub fn unpack(data: &ByteRange) -> Result<RawData, Box<dyn std::error::Error>> {
        let h = unsafe { &*(data.begin() as *const Header) };
        if h.magic != MAGIC { return Err("invalid magic number, either not a BFast, or was created on a machine with different endianess".into()); }
        if h.data_end < h.data_start { return Err("data ends before it starts".into()); }
        
        let size = h.num_arrays as usize;
        let array_offsets = unsafe { std::slice::from_raw_parts((data.begin().offset(ARRAY_OFFSETS_START)) as *const ArrayOffset, size) };
        let mut r = RawData {  ranges: Vec::with_capacity(size) };
    
        for i in 0..size {
            let offset = &array_offsets[i];
            if offset.begin > offset.end { return Err("Offset begin is after the offset end".into()); }
            if offset.end as usize > data.size() { return Err("Offset end is after the end of the data".into()); }
            if i > 0 && offset.begin < array_offsets[i - 1].end { return Err("Offset begin is before the end of the previous offset".into()); }
            
            let begin = unsafe { data.begin().add(offset.begin as usize) };
            let end = unsafe { data.end().add(offset.end as usize) };
            r.ranges.push(ByteRange{ begin, end })
        }
        Ok(r)
    }
}

/// A Bfast conceptually is a collection of buffers: named byte arrays. 
/// It contains the raw data contained within. 
pub struct Bfast {
    //name_data: Vec<Byte>,
    pub data: ByteRange,
    //data_buffer: Vec<Byte>,
    pub buffers: Vec<Buffer>,
}
impl Bfast {
    pub fn pack(&self) -> Vec<u8> {
        let count = self.buffers.len();
        let name_data: Vec<u8> = Vec::with_capacity(count);
        
        let mut ranges = Vec::with_capacity(1 + self.buffers.len());
        ranges.push(ByteRange { begin: name_data.as_ptr(), end: unsafe { name_data.as_ptr().add(name_data.len() as usize) } }); 
        for b in &self.buffers { ranges.push(b.data) }
        let r = RawData { ranges };
        r.pack()
    }

    /// Unpacks an array of buffers into a BFastData package
    pub fn unpack_range(data: &ByteRange) -> Result<Self, Box<dyn std::error::Error>> {
        let raw_data = RawData::unpack(&data)?;
        let b = raw_data.ranges.first().ok_or("Empty ranges list")?;
        let raw_ranges = unsafe { std::slice::from_raw_parts(b.begin(), b.size()) };
        
        let mut buffers: Vec<Buffer> = Vec::with_capacity(raw_data.ranges.len());
        let max_index = raw_data.ranges.len() - 1;
        for (i, v) in raw_ranges.split(|&c| c == ZERO_BYTE).enumerate() {
            let str = std::str::from_utf8(v)?;
            if i >= max_index { return Err("The number of names does not match the raw data size".into()) }
            buffers.push(Buffer { name: str.to_owned(), data: raw_data.ranges[i + 1] })
        }
        Ok(Self { data: *data, buffers })
    }

    fn unpack(data: &[u8]) -> Result<Self, Box<dyn std::error::Error>> {
        let pointer = data.as_ptr();
        let range = ByteRange {
            begin: pointer,
            end: unsafe { pointer.add(data.len()) },
        };
        Self::unpack_range(&range)
    }

    pub fn write_file(&self, file: &str) -> Result<(), Box<dyn std::error::Error>> {
        let data = self.pack();
        let mut fstrm = std::fs::File::create(std::path::Path::new(file))?;
        fstrm.write_all(&data)?;
        Ok(())
    }

    pub fn read_file(file: &str) -> Result<Bfast, Box<dyn std::error::Error>> {
        let mut fstrm = std::fs::File::open(std::path::Path::new(file))?;  
        Self::read_stream(&mut fstrm)
    }
    pub fn read_stream(fstrm: &mut std::fs::File) -> Result<Bfast, Box<dyn std::error::Error>> {
        let filesize = fstrm.metadata()?.len() as usize;
        let mut buffer = vec![0u8; filesize];
        fstrm.read_exact(&mut buffer)?;
        Bfast::unpack(&buffer)
    }
}
