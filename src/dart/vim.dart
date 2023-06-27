import 'dart:core';
import 'dart:io';
import 'dart:typed_data';

import 'bfast.dart';
import 'g3d.dart';

enum VimLoadFlags {
  geometry(1),
  assets(2),
  strings(4),
  entities(8),
  all(1 | 2 | 4 | 8);

  final int value;
  const VimLoadFlags(this.value);

  @override
  String toString() => 'VimLoadFlags.$name';
}

class EntityTable {
  final String name;
  final Uint8List data;
  final Map<String, ByteRange> columns;

  const EntityTable(this.name, this.data, this.columns);
}

extension EntityTableColumnData on EntityTable {
  int columnDataCount(String name) {
    final column = columns[name];
    if (column != null) {
      final type = name.substring(0, name.indexOf(':'));
      switch (type) {
        case "index":
          return column.size ~/ Int32List.bytesPerElement;
        case "int":
          return column.size ~/ Int32List.bytesPerElement;
        case "string":
          return column.size ~/ Int32List.bytesPerElement;
        case "byte":
          return column.size ~/ Uint8List.bytesPerElement;
        case "float":
          return column.size ~/ Float32List.bytesPerElement;
        case "double":
          return column.size ~/ Float64List.bytesPerElement;
      }
    }
    return 0;
  }

  List<T>? columnData<T extends num>(String name, {int? index}) {
    final column = columns[name];
    if (column != null) {
      final type = name.substring(0, name.indexOf(':'));
      switch (type) {
        case "index":
        case "int":
        case "string":
          if (index != null && (index < 0 || index >= column.size ~/ Int32List.bytesPerElement)) return [];
          return intColumnData(column, elementIndex: index) as List<T>;
        case "byte":
          if (index != null && (index < 0 || index >= column.size ~/ Uint8List.bytesPerElement)) return [];
          return byteColumnData(column, elementIndex: index) as List<T>;
        case "float":
          if (index != null && (index < 0 || index >= column.size ~/ Float32List.bytesPerElement)) return [];
          return floatColumnData(column, elementIndex: index) as List<T>;
        case "double":
          if (index != null && (index < 0 || index >= column.size ~/ Float64List.bytesPerElement)) return [];
          return doubleColumnData(column, elementIndex: index) as List<T>;
      }
    }
    return null;
  }

  Int32List intColumnData(ByteRange range, {int? elementIndex}) {
    return elementIndex != null
        ? data.buffer.asInt32List(range.begin + elementIndex * Int32List.bytesPerElement, 1)
        : data.sublist(range.begin, range.end).buffer.asInt32List();
  }

  Uint8List byteColumnData(ByteRange range, {int? elementIndex}) {
    return elementIndex != null ? data.buffer.asUint8List(range.begin + elementIndex * Uint8List.bytesPerElement, 1) : data.sublist(range.begin, range.end);
  }

  Float32List floatColumnData(ByteRange range, {int? elementIndex}) {
    return elementIndex != null
        ? data.buffer.asFloat32List(range.begin + elementIndex * Float32List.bytesPerElement, 1)
        : data.sublist(range.begin, range.end).buffer.asFloat32List();
  }

  Float64List doubleColumnData(ByteRange range, {int? elementIndex}) {
    return elementIndex != null
        ? data.buffer.asFloat64List(range.begin + elementIndex * Float64List.bytesPerElement, 1)
        : data.sublist(range.begin, range.end).buffer.asFloat64List();
  }

  //final el = _entityTable.data.buffer.asInt32List(indx.begin + elementIndex * Int32List.bytesPerElement, 1);

  //   switch (type) {

  //     case "float":
  //       floatColumns[column] = bytes.buffer.asFloat32List();
  //       break;
  //     case "double":
  //     case "numeric":
  //       doubleColumns[column] = bytes.buffer.asFloat64List();
  //       break;
}

// Int32List intColumnData(ByteRange range) {
//   return data.sublist(range.begin, range.end).buffer.asInt32List();
// }

// Int32List intColumnDataAt(ByteRange range) {
//   final ints = data.sublist(range.begin, range.end).buffer.asInt32List();
//   return ints;
// }

class VimHeader {
  final String vim;
  final String id;
  final String revision;
  final String generator;
  final String created;
  final String schema;

  const VimHeader(
    this.vim,
    this.id,
    this.revision,
    this.generator,
    this.created,
    this.schema,
  );
}

class VimScene {
  final VimHeader header;
  final Bfast? assets;
  final Bfast? geometry;
  final List<String> strings;
  final Map<String, EntityTable> entities;

  G3d? get g3d => geometry == null ? null : G3d.unpack(geometry!);

  static VimHeader _getHeader(Bfast bfast, Buffer? buffer) {
    if (buffer == null) throw "Invalid Header";
    Map<String, String> map = {};
    final tokens = bfast.data.splitRange(buffer.range, "\n");
    for (int i = 0; i < tokens.length; i++) {
      final keyValue = tokens[i].split("=");
      if (keyValue.length == 2) {
        map[keyValue[0]] = keyValue[1];
      }
    }
    return VimHeader(
      map['vim'] ?? '',
      map['id'] ?? '',
      map['revision'] ?? '',
      map['generator'] ?? '',
      map['created'] ?? '',
      map['schema'] ?? '',
    );
  }

  static Bfast? _getGeometry(Bfast bfast, Buffer? buffer, [VimLoadFlags flags = VimLoadFlags.all]) {
    if ((flags.value & VimLoadFlags.geometry.value) != 0) {
      if (buffer == null) throw "Invalid Geometry";
      final buff = bfast.data.sublist(buffer.range.begin, buffer.range.end);
      return Bfast.unpack(buff);
    }
    return null;
  }

  static Bfast? _getAssets(Bfast bfast, Buffer? buffer, [VimLoadFlags flags = VimLoadFlags.all]) {
    if ((flags.value & VimLoadFlags.assets.value) != 0) {
      if (buffer == null) throw "Invalid Assets";
      final buff = bfast.data.sublist(buffer.range.begin, buffer.range.end);
      return Bfast.unpack(buff);
    }
    return null;
  }

  static List<String> _getStrings(Bfast bfast, Buffer? buffer, [VimLoadFlags flags = VimLoadFlags.all]) {
    if ((flags.value & VimLoadFlags.strings.value) != 0) {
      if (buffer == null) throw "Invalid Strings";
      return bfast.data.splitRange(buffer.range, ZERO_BYTE);
    }
    return const [];
  }

  static Map<String, EntityTable> _getEntities(Bfast bfast, Buffer? buffer, [VimLoadFlags flags = VimLoadFlags.all]) {
    if ((flags.value & VimLoadFlags.entities.value) != 0) {
      if (buffer == null) throw "Invalid Entities";
      final buff = bfast.data.sublist(buffer.range.begin, buffer.range.end);
      final entities = Bfast.unpack(buff);
      Map<String, EntityTable> entityTables = {};
      for (final eb in entities.buffers) {
        final buff = entities.data.sublist(eb.range.begin, eb.range.end);
        final table = Bfast.unpack(buff);
        final columns = {for (final b in table.buffers) b.name: b.range};
        entityTables[eb.name] = EntityTable(eb.name, buff, columns);
      }
      return entityTables;
    }
    return const {};
  }

  const VimScene(this.header, this.assets, this.geometry, this.strings, this.entities);
  factory VimScene.unpack(Uint8List data, [VimLoadFlags flags = VimLoadFlags.all]) {
    final bfast = Bfast.unpack(data);
    final dataMap = {for (final b in bfast.buffers) b.name: b};
    final header = _getHeader(bfast, dataMap["header"]);

    final assets = _getAssets(bfast, dataMap["assets"], flags);
    final geometry = _getGeometry(bfast, dataMap["geometry"], flags);
    final strings = _getStrings(bfast, dataMap["strings"], flags);
    final entities = _getEntities(bfast, dataMap["entities"], flags);

    return VimScene(header, assets, geometry, strings, entities);
  }

  static Future<VimScene> readFile(
    String fileName, [
    VimLoadFlags loadFlags = VimLoadFlags.all,
  ]) async {
    final fstrm = File(fileName);
    final buffer = await fstrm.readAsBytes();
    return VimScene.unpack(buffer, loadFlags);
  }
}
