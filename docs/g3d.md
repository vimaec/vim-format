# G3D

[<img src="https://img.shields.io/nuget/v/Vim.G3d.svg">](https://www.nuget.org/packages/Vim.G3d) 

G3D is a simple, efficient, generic binary format for storing and transmitting geometry. The G3D format
is designed to be used either as a serialization format or as an in-memory data structure.

G3D can represent triangular meshes, quadrilateral meshes, polygonal meshes, point clouds, and line segments.  
It can be easily and efficiently deserialized and rendered in different languages and on different platforms.

The G3D format can contain a superset of geometry attributes found in most common geometry formats, 
including formats such as FBX, glTF, OBJ, PLY, and in memory data structures used in popular 3D APIs, like 
Unity, Three.JS, Assimp, and 3dsMax.

G3D is maintained by [VIMaec LLC](https://vimaec.com) and is licensed under the terms of the MIT License.

# Repository Structure and Projects

On this Github repository we have the following projects:

* `src/cs/g3d/Vim.G3d` - C# .NET Standard 2.0 Library for reading/writing G3D buffers
* `src/cs/g3d/Vim.G3d.AssimpAdapter` - C# library for converting from Assimp meshes to G3D data structures
* `src/cs/g3d/Vim.G3d.Tests` - C# project with NUnit tests

# Format 

## BFAST Container

The underlying binary layout of a G3D file conforms to the [BFAST serialization format](./bfast.md), which is a simple and efficient binary format for serializing collections of byte arrays. BFAST provides an interface that allows named arrays of binary data to be serialized and deserialized quickly and easily.

## Meta-Information

The first buffer in a g3d, named "meta", is an 8 byte header composed of the following data:

```
byte1=0x63 // magic number part "A"
byte2=0xD0 // magic number part "B"
byte3=0x66 // first character in the unit (0x66 is the character code for 'f' in 'ft' for feet)
byte4=0x74 // second character in the unit (0x77 is the character code for 't' in 'ft' for feet)
byte5=0x02 // up axis (0x00: x axis, 0x01: y axis, 0x02: z axis)
byte6=0x00 // forward vector (0x00: x axis, 0x01: y axis, 0x02: z axis, 0x03: -x axis, 0x04: -y axis, 0x05: -z axis)
byte7=0x00 // axis handedness (0x00: left-handed, 0x01: right-handed)
byte8=0x00 // zero-padding
```

## Attributes
 
### Attribute Descriptor String

Every attribute descriptor has a one to one mapping to a string representation similar to a URN: 
    
    `g3d:<association>:<semantic>:<index>:<data_type>:<data_arity>`

This attribute descriptor string is the name of the buffer. 

### Association

G3D is organized as a collection of attribute buffers. Each attributes describe what part of the incoming geometry they are associated with:

* vertex     // vertex data
* corner     // face-vertex data
* face       // per polygon data
* edge       // per half-edge data 
* mesh       // A continuous group of submeshes
* submesh    // polygonal group - assumes a contiguous sequence of indices in the index buffer
* instance   // objects which may have a related mesh, matrix and more.
* all        // whole object data - for example face-size of 4 with whole object indicates a quad mesh

### Semantic

Attributes also have a "semantic" which is used to identify what role the attribute has when parsing. These map roughly to FBX layer elements, or Three.JS buffer attributes. There are a number of predefined semantic values with reserved names, but applications are free to define custom semantic values. The only required semantic in a G3D file is "position". Here is a list of some of the predefined semantics: 

* unknown,       // no known attribute type
* position,      // vertex buffer 
* index,         // index buffer
* indexoffset,   // an offset into the index buffer (used with groups and with faces)
* vertexoffset,  // the offset into the vertex buffer (used only with groups, and must have offset.)
* normal,        // computed normal information (per face, group, corner, or vertex)
* binormal,      // computed binormal information 
* tangent,       // computed tangent information 
* materialid,    // material id
* visibility,    // visibility data
* size,          // number of indices per face or group
* uv,            // UV (sometimes more than 1, e.g. Unity supports up to 8)
* color,         // usually vertex color, but could be edge color as well
* smoothing,     // identifies smoothing groups (e.g. ala 3ds Max and OBJ files)
* weight,        // in 3ds Max this is called selection 
* mapchannel,    // 3ds Max map channel (assoc of none => map verts, assoc of corner => map faces)
* id,            // used to identify what object each face part came from 
* joint,         // used to identify what a joint a skin is associated with 
* boxes,         // used to identify bounding boxes
* spheres,       // used to identify bounding spheres
* user,          // identifies user specific data (in 3ds Max this could be "per-vertex-data")

### Index

Attributes use indices to distinguish when multiple attributes share the same name (e.g. uv:0 ... uv:8)

### Data Type

Attributes are stored in 512-byte aligned data-buffers arranged as arrays of scalars or fixed width vectors. The individual data values can be integers, or floating point values of various widths from 1 to 8 bytes. The data-types are:

* int8
* int16
* int32
* int64
* uint8
* uint16
* uint32
* uint64
* float32
* float64

### Arity

The number of primitives per data element is called the "arity" and can be any integer value greater than zero. 

## Encoding Strings

While there is no explicit string type, one could encode string data by using a data-type uint8 with an arity of a fixed value (say 255) to store short strings. 

# Recommended reading:

* [VIM AEC blog post about using G3D with Unity](https://www.vimaec.com/the-g3d-geometry-exchange-format/)
* [Hackernoon article about BFast](https://hackernoon.com/bfast-a-data-format-for-serializing-named-binary-buffers-243p130uw)
* http://assimp.sourceforge.net/lib_html/structai_mesh.html
* http://help.autodesk.com/view/FBX/2017/ENU/?guid=__files_GUID_5EDC0280_E000_4B0B_88DF_5D215A589D5E_htm
* https://help.autodesk.com/cloudhelp/2017/ENU/Max-SDK/cpp_ref/class_mesh.html
* https://help.autodesk.com/view/3DSMAX/2016/ENU/?guid=__files_GUID_CBBA20AD_F7D5_46BC_9F5E_5EDA109F9CF4_htm
* http://paulbourke.net/dataformats/
* http://paulbourke.net/dataformats/obj/
* http://paulbourke.net/dataformats/ply/
* http://paulbourke.net/dataformats/3ds/
* https://github.com/KhronosGroup/gltf
* http://help.autodesk.com/view/FBX/2017/ENU/?guid=__cpp_ref_class_fbx_layer_element_html
