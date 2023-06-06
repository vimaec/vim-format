// ignore_for_file: constant_identifier_names

/*
    G3D Data Format
    Copyright 2019, VIMaec LLC
    Copyright 2018, Ara 3D, Inc.
    Usage licensed under terms of MIT Licenese.
*/

import 'bfast.dart';

const G3D_VERSION = VERSION(major: 2, minor: 2, patch: 0, build: '2022.05.02');

/// The different types of data types that can be used as elements.
enum DataType {
  uint8,
  int8,
  uint16,
  int16,
  uint32,
  int32,
  uint64,
  int64,
  uint128,
  int128,
  float16,
  float32,
  float64,
  float128;

  int get size {
    switch (this) {
      case DataType.uint8:
      case DataType.int8:
        return 1;
      case DataType.uint16:
      case DataType.int16:
        return 2;
      case DataType.uint32:
      case DataType.int32:
        return 4;
      case DataType.uint64:
      case DataType.int64:
        return 8;
      case DataType.uint128:
      case DataType.int128:
        return 16;
      case DataType.float16:
        return 2;
      case DataType.float32:
        return 4;
      case DataType.float64:
        return 8;
      case DataType.float128:
        return 16;
      default:
        throw "invalid data type";
    }
  }

  static DataType parse(String value) {
    switch (value) {
      case 'uint8':
        return DataType.uint8;
      case 'int8':
        return DataType.int8;
      case 'uint16':
        return DataType.uint16;
      case 'int16':
        return DataType.int16;
      case 'uint32':
        return DataType.uint32;
      case 'int32':
        return DataType.int32;
      case 'uint64':
        return DataType.uint64;
      case 'int64':
        return DataType.int64;
      case 'uint128':
        return DataType.uint128;
      case 'int128':
        return DataType.int128;
      case 'float16':
        return DataType.float16;
      case 'float32':
        return DataType.float32;
      case 'float64':
        return DataType.float64;
      case 'float128':
        return DataType.float128;
      default:
        throw ArgumentError.value(value, 'dataType', 'unknown data-type');
    }
  }
}

/// What geometric element each attribute is associated with
enum Association {
  vertex,
  face,
  corner,
  edge,
  subgeometry,
  instance,
  shapevertex,
  shape,
  material,
  mesh,
  submesh,
  all,
  none;

  static Association parse(String value) {
    switch (value) {
      case 'vertex':
        return Association.vertex;
      case 'face':
        return Association.face;
      case 'corner':
        return Association.corner;
      case 'edge':
        return Association.edge;
      case 'subgeometry':
        return Association.subgeometry;
      case 'instance':
        return Association.instance;
      case 'shapevertex':
        return Association.shapevertex;
      case 'shape':
        return Association.shape;
      case 'material':
        return Association.material;
      case 'mesh':
        return Association.mesh;
      case 'submesh':
        return Association.submesh;
      case 'all':
        return Association.all;
      case 'none':
        return Association.none;
      default:
        throw ArgumentError.value(value, 'association', 'unknown association');
    }
  }
}

enum InstanceFlags {
  none(0),
  hidden(1);

  final int flag;
  const InstanceFlags(this.flag);
}

// Contains all the information necessary to parse an attribute data channel and associate it with some part of the geometry
class AttributeDescriptor {
  /// The type of individual data values. There are n of these per element where n is the arity.
  final DataType dataType;

  /// The number of primitive values associated with each element
  final int dataArity;

  /// The index of the attribute.
  final int index;

  /// What part of the geometry each tuple of data values is associated with
  final Association association;

  /// The semantic of the attribute (e.g. normals, uv)
  final String semantic;

  const AttributeDescriptor(
    this.dataType,
    this.dataArity,
    this.index,
    this.association,
    this.semantic,
  );

  static AttributeDescriptor parse(String value) {
    final tokens = value.split(':');
    if (tokens.length < 6) throw "Incorrect number of tokens: ${tokens.length}";
    if (tokens[0] != "g3d") throw "Expected g3d, found: ${tokens[0]}";

    final association = Association.parse(tokens[1]);
    final semantic = tokens[2];
    final index = int.parse(tokens[3]);
    final dataType = DataType.parse(tokens[4]);
    final dataArity = int.parse(tokens[5]);

    return AttributeDescriptor(
      dataType,
      dataArity,
      index,
      association,
      semantic,
    );
  }

  @override
  String toString() {
    return "g3d:${association.name}:$semantic:$index:${dataType.name}:$dataArity";
  }
}

/// Manage the data buffer and meta-information of an attribute
class Attribute {
  final AttributeDescriptor descriptor;
  final int begin;
  final int end;

  int get byteSize => end - begin;
  int get elementSize => descriptor.dataType.size * descriptor.dataArity;
  int get count => byteSize ~/ elementSize;

  Attribute(String desc, this.begin, this.end) : descriptor = AttributeDescriptor.parse(desc) {
    assert(byteSize % elementSize == 0, "Data buffer byte size does not divide evenly by size of elements");
  }

  factory Attribute.fromBuffer(Buffer buffer) {
    return Attribute(buffer.name, buffer.range.begin, buffer.range.end);
  }

  Buffer toBuffer() {
    return Buffer(descriptor.toString(), ByteRange(begin, end));
  }
}

// A G3d data structure, is a set of attributes. It is stored internally as a BFast
class G3d {
  static const defaultMeta = "{ \"G3D\": \"1.0.0\" }";

  final String meta;
  final Bfast bfast;
  final List<Attribute> attributes;

  const G3d(this.meta, this.attributes, this.bfast);
  factory G3d.unpack(Bfast bfast) {
    String meta = defaultMeta;
    final attributes = <Attribute>[];
    for (int i = 0; i < bfast.buffers.length; ++i) {
      final b = bfast.buffers[i];
      if (i == 0) {
        meta = bfast.data.sublist(b.range.begin, b.range.end).toString();
      } else {
        attributes.add(Attribute(b.name, b.range.begin, b.range.end));
      }
    }
    return G3d(meta, attributes, bfast);
  }

  static Future<G3d> readFile(String path) async {
    final bfast = await Bfast.readFile(path);
    return G3d.unpack(bfast);
  }
}

class Descriptors {
  static const String position = "g3d:vertex:position:0:float32:3";
  static const String index = "g3d:corner:index:0:int32:1";
  static const String objectFaceSize = "g3d:all:facesize:0:int32:1";

  static const String vertexUv = "g3d:vertex:uv:0:float32:2";
  static const String vertexUvw = "g3d:vertex:uv:0:float32:3";
  static const String vertexNormal = "g3d:vertex:normal:0:float32:3";
  static const String vertexColor = "g3d:vertex:color:0:float32:3";
  static const String vertexColorWithAlpha = "g3d:vertex:color:0:float32:4";
  static const String vertexBitangent = "g3d:vertex:bitangent:0:float32:3";
  static const String vertexTangent = "g3d:vertex:tangent:0:float32:3";
  static const String vertexTangent4 = "g3d:vertex:tangent:0:float32:4";
  static const String vertexSelectionWeight = "g3d:vertex:weight:0:float32:1";

  static const String faceMaterial = "g3d:face:material:0:int32:1";
  static const String faceNormal = "g3d:face:normal:0:float32:3";
  static const String faceSize = "g3d:face:facesize:0:int32:1";
  static const String faceIndexOffset = "g3d:face:indexoffset:0:int32:1";
  static const String faceSelectionWeight = "g3d:face:weight:0:float32:1";

  //VIM 1.0
  // Meshes
  static const String meshSubmeshOffset = "g3d:mesh:submeshoffset:0:int32:1";

  // Instances
  static const String instanceTransform = "g3d:instance:transform:0:float32:16";
  static const String instanceParent = "g3d:instance:parent:0:int32:1";
  static const String instanceMesh = "g3d:instance:mesh:0:int32:1";
  static const String instanceFlags = "g3d:instance:flags:0:uint16:1";

  // Shapes
  static const String shapeVertex = "g3d:shapevertex:position:0:float32:3";
  static const String shapeVertexOffset = "g3d:shape:vertexoffset:0:int32:1";
  static const String shapeColor = "g3d:shape:color:0:float32:4";
  static const String shapeWidth = "g3d:shape:width:0:float32:1";

  // Materials
  static const String materialColor = "g3d:material:color:0:float32:4";
  static const String materialGlossiness = "g3d:material:glossiness:0:float32:1";
  static const String materialSmoothness = "g3d:material:smoothness:0:float32:1";

  // Submeshes
  static const String submeshIndexOffset = "g3d:submesh:indexoffset:0:int32:1";
  static const String submeshMaterial = "g3d:submesh:material:0:int32:1";

  // https://docs.thinkboxsoftware.com/products/krakatoa/2.6/1_Documentation/manual/formats/particle_channels.html
  static const String pointVelocity = "g3d:vertex:velocity:0:float32:3";
  static const String pointNormal = "g3d:vertex:normal:0:float32:3";
  static const String pointAcceleration = "g3d:vertex:acceleration:0:float32:3";
  static const String pointDensity = "g3d:vertex:density:0:float32:1";
  static const String pointEmissionColor = "g3d:vertex:emission:0:float32:3";
  static const String pointAbsorptionColor = "g3d:vertex:absorption:0:float32:3";
  static const String pointSpin = "g3d:vertex:spin:0:float32:4";
  static const String pointOrientation = "g3d:vertex:orientation:0:float32:4";
  static const String pointParticleId = "g3d:vertex:particleid:0:int32:1";
  static const String pointAge = "g3d:vertex:age:0:int32:1";

  // Line specific attributes
  static const String lineTangentIn = "g3d:vertex:tangent:0:float32:3";
  static const String lineTangentOut = "g3d:vertex:tangent:1:float32:3";
}
