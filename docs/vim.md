# VIM v1.0.0
The VIM format is a modern and efficient open 3D data interchange format designed for BIM and manufacturing data optimized for efficient loading and rendering on low-power devices.

Characteristics of the VIM format:
* Minimal memory and parsing overhead are required before loading into GPU memory
* Contains instancing information
* Represents structured relational data (e.g., BIM data) efficiently
* Deduplicated string data
* Can include arbitrary nested structures (e.g., assets)
* Extensible object format and geometry format

Unlike other 3D data formats, VIM is designed to carry substantial amounts of complex relational data and instanced geometry in an efficient and standardized manner with minimal pre-processing.

<!--
* Unlike Revit and FBX, VIM is an open and cross platform format and does not require an SDK to read or write
* Unlike IFC and STEP, VIM geometry is already triangulated and is in a GPU-friendly format
* Unlike glTF and USD, VIM supports structured BIM data in a compact relational form
* Unlike 3DXML and Collada, VIM is a binary format and requires minimal parsing 
* Unlike glTF, VIM is an opinionated format, that has fixed access patterns for data buffers, and can be extended in very precise ways to simplify parsing.
* Unlike FBX, VIM is easy to extend with new buffers. 
* Unlike glTF and FBX, VIM is not designed to specify animated assets  
-->

# Sample Implementation
A sample validating VIM reader can be found at: https://github.com/vimaec/vim/blob/master/VIMReference/Program.cs

# About this Specification

This is the specification for version 1.0.0 of the VIM data format. It is divided up into the following parts:
1. VIM Format Binary Specification 
2. VIM Versioning
3. VIM Object Model
4. Extending VIM
5. FAQ

# 1. VIM Format Binary Specification
At the top level, a VIM document conforms to the [BFAST (Binary Format for Array Serialization and Transmission) binary data format](https://github.com/vimaec/bfast).
A BFAST is conceptually similar to a ZIP or TAR archive without compression, just an array of named data buffers. Physically it is laid out as a header, an array of range structures (each containing offsets to the beginning and end of a buffer), and then the data section. The following structures assume 64-bit or smaller alignment.

## Header
The first structure in the VIM file (or any BFAST) is a 32-byte header.

```
// 32 Bytes 
struct Header 
{
    uint64_t Magic;         // 0xBFA5
    uint64_t DataStart;     // <= File size and >= 32 + Sizeof(Range) * NumArrays 
    uint64_t DataEnd;       // >= DataStart and <= file size
    uint64_t NumArrays;     // Number of all buffers, including names buffer
}
```

After the header is 32 bytes of padding.

## Buffer Ranges

At byte number 64 starts an array of N range struct (where N = NumArrays in the header). 
Each range struct specifies where the data begins and ends for a particular buffer as an offset from the beginning of the file.

```
// 16 bytes 
struct Range
{
    uint64_t Begin;
    uint64_t End;
}
```

The data section starts at the first 64-byte aligned address immediately following the last Range value.

The range structs are expected to satisfy the following criteria:
* to be ordered by the Begin value
* to represent non-overlapping buffers (the beginning of the following buffer is strictly greater than the * end of the preceding buffer)
* each buffer begins at a 64 byte aligned address
* There are no more than 64 bytes of padding between each buffer.

## Names Buffer
The first data buffer in a VIM file contains the names of the others buffers as NUL character delimited UTF-8 encoded strings. The number of strings should always be equal to the number of data buffers specified in the header minus one.

## VIM Data Buffers
There are five expected top-level buffers in the VIM file with the following names. Their order is not essential, and only the header is optional. Additional buffers are allowed for custom purposes but will not be parsed by most readers.
* `header`
* `assets`
* `entities`
* `strings`
* `geometry`

## Header Buffer
The header section contains the VIM file version and additional meta data as a sequence of newline (`\n`) terminated sets of key/value pairs denoted by `<key>=<value>`.

The following is an example:
```
vim=1.0.0
id=03280421-595d-4a35-802e-83bb6739e7ae
revision=f7eaf6b2-55b2-4f55-87f4-cdc9a01aa406
generator=Vim.Revit.Exporter:v1.46:Revit2020
created=2020-05-20T16:31:19Z
schema=4.1.0
```

The field names are case insensitive. The only required field is `vim` which must have the value in the format `<major>.<minor>.<patch>` representing the file format version. 

The `id` field should be a uniquely generated GUID created the first time a VIM is created but is not changed when the file is modified or saved.  

The `revision` field if present should be a new GUID created anytime a VIM is modified or updated.

The `created` field, should contain a date in ISO_8601 format. 

The `generator` field contains the name of the program used to generate or edit the VIM.

The `schema` field contains the version of the VIM Object Model.

## Assets Buffer
The assets section of a BIM is also a BFAST container. It may contain any number of buffers with any names. Buffers prefixed with the name `texture/` are assumed to be texture files. By convention buffers prefixed with the name `render/` contain image files. The asset buffer `render/main.png` is used as the PNG thumbnail of the VIM file.

## Geometry Buffer
The geometry section of a VIM contains the merged geometry and basic scene graph information for an entire VIM document using the [G3D format](https://github.com/vimaec/g3d).

### About G3D
The [G3D format](https://github.com/vimaec/g3d) is a binary format for 3D geometry that encodes data in an array of attribute buffers. 

G3D is based on the BFAST binary layout and uses a naming convention to identify the layout of each attribute buffer and how it is used.

Each attribute buffer is associated with a component of a geometry:
* vertex
* corner
* submesh
* mesh
* instance
* shape

G3D attributes have names to identify them (e.g., position or UV) and uses indices to distinguish when multiple attributes share the same name (e.g., uv:0 ... uv:8). 

They can be of one of the following core data datatypes: float32, float64, int8, int16, int32, int64.

More information on G3D is available on its Github repo at [https://github.com/vimaec/g3d](https://github.com/vimaec/g3d).

### VIM Geometry Attributes
The geometry in a VIM contains the following attributes:

* `g3d:vertex:position:0:float32:3`
  An array of 32-bit single-precision floating point values, arranged in slices of 3 to represent the (X, Y, Z) vertices of all the meshes in the VIM. We refer to this as the "vertex buffer".

* `g3d:vertex:uv:0:float32:2`
  (Optional) An array of 32-bit single-precision floating point values, arranged in slices of 2 to represent the (U,V) values associated with each vertex.

* `g3d:corner:index:0:int32:1`
  An array of 32-bit integers representing the combined index buffer of all the meshes in the VIM. The values in this index buffer are relative to the beginning of the vertex buffer. Meshes in a VIM are composed of triangular faces, whose corners are defined by 3 indices.

* `g3d:submesh:indexoffset:0:int32:1`
  An array of 32-bit integers representing the index offset of the index buffer of a given submesh.

* `g3d:submesh:material:0:int32:1`
  An array of 32-bit integers representing the index of the material associated with a given submesh.

* `g3d:mesh:submeshoffset:0:int32:1`
  An array of 32-bit integers representing the index offset of a submesh in a given mesh.

* `g3d:material:color:0:float32:4`
  An array of 32-bit single-precision floating point values in the domain [0.0f, 1.0f], arranged in slices of 4 to represent the (R, G, B, A) diffuse color of a given material.

* `g3d:material:glossiness:0:float32:1`
  An array of 32-bit single-precision floating point values in the domain [0.0f, 1.0f] representing the glossiness of a given material.

* `g3d:material:smoothness:0:float32:1`
  An array of 32-bit single-precision floating point values in the domain [0.0f, 1.0f] representing the smoothness of a given material.

* `g3d:instance:transform:0:float32:16`
  An array of 32-bit single-precision floating point values, arranged in slices of 16 to represent the 4x4 row-major transformation matrix associated with a given instance.

* `g3d:instance:flags:0:uint16:1`
  (Optional) An array of 16-bit unsigned integers representing the flags of a given instance. The first bit of each flag designates whether the instance should be initially hidden (1) or not (0) when rendered.

* `g3d:instance:parent:0:int32:1`
  An array of 32-bit integers representing the index of the parent instance associated with a given instance.

* `g3d:instance:mesh:0:int32:1`
  An array of 32-bit integers representing the index of a mesh associated with a given instance.

* `g3d:shapevertex:position:0:float32:3`
  (Optional) An array of 32-bit single-precision floating point values, arranged in slices of 3 to represent the (X, Y, Z) positions of all the world-space shapes in the VIM. We refer to this as the "shape vertex buffer"

* `g3d:shape:vertexoffset:0:int32:1`
  (Optional) An array of 32-bit integers representing the index offset of the vertices in a given shape.

* `g3d:shape:color:0:float32:4`
  (Optional) An array of 32-bit single-precision floating point values, arranged in slices of 4 to represent the (R, G, B, A) color of a given shape.

* `g3d:shape:width:0:float32:1`
  (Optional) An array of 32-bit single-precision floating-point values represents a given shape's width.

Additional attributes are possible but are ignored and may or may not be written out by any tool that inputs and outputs VIM files.

Conceptually, the geometric objects in a VIM file are related in the following manner:

- **Instance**:
  - Has a 4x4 row-major matrix representing its world-space transform.
  - Has a set of **Flag**s.
  - May have a parent **Instance**.
  - May have a **Mesh**.
- **Mesh**
  - Is composed of 0 or more **Submesh**es.
- **Submesh**
  - Has a **Material**
  - References a slice of values in the index buffer to define the geometry of its triangular faces in local space.
- **Material**
  - Has a glossiness value in the domain [0f, 1f].
  - Has a smoothness value in the domain [0f, 1f].
  - Has an RGBA diffuse color whose components are in the domain [0f, 1f].
- **Shape**
  - Has an RGBA color whose components are in the domain [0f, 1f].
  - Has a width.
  - References a slice of vertices in the shape vertex buffer to define the sequence of world-space vertices which compose its linear segments.

## Entities Buffer

The entities section of a VIM file contains a collection of entity tables which store a wide range of data, including the BIM parameters associated to the elements in the file.

This buffer is encoded as a BFAST, with each buffer containing an entity table. The columns of each entity table are encoded as BFAST buffers and contained within their parent entity table buffers.

The "VIM Object Model" refers to the schema of the entity tables. This constitutes the name of each table, the name and type of each column in each table, and the relationship between tables, as specified by index columns.

- [Conceptual Overview](./ObjectModel/VIM_Object_Model.md)

- [Schema](./ObjectModel/object-model-schema.json)

  - The "Tables" object describes the entity table names and their contained columns.

  - Each column name is prefixed with the type of value it stores. Columns are classified in one of three ways:

    - **Data** columns, which are prefixed with either:
      - `byte:` for 8-bit values, typically used to contain booleans. For example, the column `byte:IsPinned` in the `Vim.Element` table contains boolean values which designate whether the Element on that row has been pinned (a concept in Revit).

      - `int:` for 32-bit signed integer values. For example, the column `int:Id` in the `Vim.Workset` table contains the identifier associated to each Workset.

      - `long:` for 64-bit signed integer values. For example, the column `long:Id` in the `Vim.Element` table contains the identifier associated to each Element.

      - `float:` for 32-bit single-precision floating point values. For example, the column `float:Location.X` in the `Vim.Element` table contains the X location coordinate of each Element. Compound types which would form a Vector3 are represented as multiple columns in the table (e.g., `float:Location.X`, `float:Location.Y`, and `float:Location.Z`).

      - `double:` for 64-bit double-precision floating point values. For example, the column `double.Color.X` in the `Vim.Material` table contains the X color component, or the red channel, of each Material. Compound types which would form a Color are represented as multiple columns in the table (e.g. `double:Color.X`, `double:Color.Y`, `double:Color.Z`).

    - **String** columns, which are prefixed with `string:` and contain signed 32-bit integer index values into the VIM file's string buffer (see below). For example, the column `string:Name` in the `Vim.Element` table contains the string index values representing each Element's name. The VIM file stores deduplicated strings in a common buffer to improve memory usage and to reduce its overall file size. If there is no string for a given row, an index value of `-1` is used.

    - **Index** columns are prefixed with `index:(TABLE_NAME):` and describe a relation to a row in another table. For example, the column `index:Vim.BimDocument:BimDocument` in the `Vim.Element` table contains 32-bit signed integer values representing indexes into the `Vim.BimDocument` table. This index encodes the relation between an Element and its originating BimDocument. If there is no relation, an index value of `-1` is used.

- [Schema Evolution](./ObjectModel/schema-diff.json)

  - This file summarizes the difference between schema versions using a flat representation of the entity table columns, prefixed by their entity table name, in the form: `(TABLE_NAME)__(COLUMN_NAME)`.

  - For example, in the schema update from v4.3.0 to v4.4.0, the entry `Vim.Element__byte:IsPinned` indicates that the entity table `Vim.Element` now contains the column `byte:IsPinned`.

**Important invariant**: the number of rows in the following entity tables must be equal to the number of items items in their corresponding VIM geometry attributes to ensure referential integrity between rendered geometry and its associated entity table data.

```
rowCount(Vim.Node) == itemCount(g3d:instance:transform:0:float32:16)
rowCount(Vim.Node) == itemCount(g3d:instance:flags:0:uint16:1)
rowCount(Vim.Node) == itemCount(g3d:instance:parent:0:int32:1)
rowCount(Vim.Node) == itemCount(g3d:instance:mesh:0:int32:1)

rowCount(Vim.Material) == itemCount(g3d:material:color:0:float32:4)
rowCount(Vim.Material) == itemCount(g3d:material:glossiness:0:float32:1)
rowCount(Vim.Material) == itemCount(g3d:material:smoothness:0:float32:1)

rowCount(Vim.Shape) == itemCount(g3d:shape:vertexoffset:0:int32:1)
rowCount(Vim.Shape) == itemCount(g3d:shape:color:0:float32:4)
rowCount(Vim.Shape) == itemCount(g3d:shape:width:0:float32:1)
```


## Strings Buffer
The strings buffer contains a sequence of strings of zero or more length, with no duplicates, delimited by the "NUL" character. There may or may not be a trailing "NUL" character. The zero-based index of each string is used by the string columns of entity tables.

# 2. VIM Version

The VIM format version is defined by the `vim` key in the VIM file header and designates the version of the format's data layout. This version number uses the [Semantic Versioning](https://semver.org/) scheme: `MAJOR.MINOR.PATCH`

* A `MAJOR` number increment indicates a breaking change. Older VIM tools are not expected to be capable of loading VIM files whose `MAJOR` version has been incremented.

* A `MINOR` number increment indicates a backwards-compatible change, for example: the addition of a new top-level buffer. Older VIM tools are expected to be capable of loading VIM files whose `MINOR` version has been incremented within the same `MAJOR` version.

* A `PATCH` number increment indicates a backwards-compatible change which does not affect the format's data layout, for example: a bug fix. Older VIM tools are expected to be capable of loading VIM files whose `PATCH` version has been incremented within the same `MAJOR` and `MINOR` version.

The VIM Object Model version is defined by the `schema` key in the VIM file header and designates the version of the entity table layout. This version number also uses semantic versioning.

# 3. Extending and Modifying VIM

The VIM format allows for additional geometry attribute buffers beyond the minimum required. Tools should be able to read and write additional buffers, even if there is no more profound understanding of the buffer.

Software loading VIM files should be robust in the absence of tables and columns, never assuming any table or column is present.

Additional tables and columns can be added as desired, and all software supporting VIM files should be able to read and write that data without loss.

# 4. FAQ

1. Why 64 Byte Alignment?

- Many Intel and AMD processors have 64-byte L1 cache lines. When data is aligned on cache lines, it can benefit performance.

2. Why isn't a VIM compressed?

- In HTTP server/client scenarios, the web client (e.g., a browser) will automatically decompress compressed content (e.g., using gzip) and serve with the appropriate content-encoding header. This decompression is optimized and happens in native code, and as a result is faster and simpler than requiring the client code to decompress content.

# Copyright

This documentation is [Copyright 2023 VIMaec LLC.](https://www.vimaec.com/copyright).
