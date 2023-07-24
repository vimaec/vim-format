# BFAST

[<img src="https://img.shields.io/nuget/v/Vim.Bfast.svg">](https://www.nuget.org/packages/Vim.Bfast) 

BFAST stands for the **B**inary **F**ormat for **A**rray **S**erialization and **T**ransmission. 

BFAST is a data format for simple, efficient, and reliable serialization and deserialization of 
collections of binary data with optional names as a single block of data. It is designed so
that efficient and correct readers and writers of the format can be quickly written in different 
languages.

BFAST is intended to be a high-performance implementation that is fast enough to use as a purely 
in-memory low-level data format, for representing arbitrary data such as meshes, point-clouds, image data, 
etc. and to scale to data that must be processed out of core. One of the design goals was to assure 
that the format could be easily and efficiently decoded using JavaScript on most modern web-browsers
with very little code. 

BFAST is maintained by [VIMaec LLC](https://www.vimaec.com) and is licensed under the terms of 
the MIT License.
	
## Use Case

You would use the BFAST structure if you have a binary data to serialize that is mostly in the form of 
long arrays. For example a set of files that you want to bundle together without wanting to bring in 
the overhead of a compression library or re-implementing TAR. We use BFAST to encode mesh data and as 
containers for other data. 

## Features

* Very small implementation overhead 
* Easy to implement efficient and conformant encoders and decoders in different languages 
* Fast random access to any point in the data format with a minimum of disk accesses
* Format and endianess easily identified through a magic number at the front of the file
* Data arrays are 64 byte aligned to facilitate casting to SIMD data types (eg. AVX-512)
* Array offsets are encoded using 64-bit integers to supports large data sets
* Positions of data buffers are encoded in the beginning of the file
* Quick and easy to validate that a block is a valid BFAST encoding of data

## Rationale

Encoding containers of binary data is a deceptively simple problem that is easy to solve
in ways that are overly complex, inefficient, or dependent on a particular platform. 
We are proposing a standardized solution to the problem in the form of a specification and sample 
implementation that can allow software to easily encode low level binary data in a manner 
that is both efficient and cross-platform. 

## Related Libraries 

The following is a partial list of commonly used binary data serialization formats:

* [Protcol Buffers](https://developers.google.com/protocol-buffers/)
* [FlatBuffers](https://github.com/google/flatbuffers)
* [BINN](https://github.com/liteserver/binn/)
* [BSON](http://bsonspec.org/)
* [UBJSON](http://ubjson.org/)
* [MessagePack](https://msgpack.org/)
* [CBOR](https://cbor.io/)
* [TAR](https://www.gnu.org/software/tar/manual/html_node/Standard.html)

For a more comprehensive list see:

* https://en.wikipedia.org/wiki/Comparison_of_data-serialization_formats
* https://en.wikipedia.org/wiki/List_of_archive_formats

# Specification

The file format consists of three sections:

* Header - Fixed size descriptor (32 bytes) describing the file contents   
* Ranges - An array of offset pairs indicating the begin and end of each buffer (relative to file begin) 
* Data   - 64-byte aligned data buffers 

## Header Section

The header is a 32-byte struct with the following layout:  

```
    [StructLayout(LayoutKind.Explicit, Pack = 8, Size = 32)]
    public struct Header
    {
        [FieldOffset(0)]    public long Magic;         // 0xBFA5
        [FieldOffset(8)]    public long DataStart;     // <= File size and >= 32 + Sizeof(Range) * NumArrays 
        [FieldOffset(16)]   public long DataEnd;       // >= DataStart and <= file size
        [FieldOffset(24)]   public long NumArrays;     // Number of all buffers, including name buffer
    }
```

## Ranges Section

The ranges start at byte 32. There are `NumArrays` of them and they have the following format. 
`NumArrays` is the total count of all buffers, including the first buffer that contains the names.
`NumArrays` should always be equal to or greater than one. Each `Begin` and `End` values are byte 
offsets relative to the beginning of the file.

```
    [StructLayout(LayoutKind.Explicit, Pack = 8, Size = 16)]
    public struct Range
    {
        [FieldOffset(0)] public long Begin;
        [FieldOffset(8)] public long End;
    }		
```

## Data Section

The data section starts at the first 64 byte aligned address immediately following the last `Range` value.
This value is stored for validation purposes in the header as `DataStart`. 

### Names Buffer

The first data buffer contain the names of the subsequent buffers as a concatenated list of Utf-8 encoded 
strings separated by null characters. Names may be zero-length and are not guaranteed to be unique. 
A name may contain any Utf-8 encoded character except the null character. 

There must be N-1 names where N is the number of ranges (i.e. the `NumArrays` value in header). 

# Implementations

The official reference implementation of BFAST is written in C# and targets .NET Standard 2.0. The C#
test suite uses NUnit and targets .NET Core 2.1. At VIM AEC we are using BFAST in production code that 
targets Unity 2019.1 and .NET Framework 4.7.1.

There is currently a C++ encoder and a JavaScript decoder implementation under development, but they
are not tested and supported yet. 
