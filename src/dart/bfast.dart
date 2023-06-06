// /*
//     BFAST Binary Format for Array Streaming and Transmission
//     Copyright 2019, VIMaec LLC
//     Copyright 2018, Ara 3D, Inc.
//     Usage licensed under terms of MIT Licenese
//     https://github.com/vimaec/bfast
// */
// ignore_for_file: constant_identifier_names

import 'dart:core';
import 'dart:io';
import 'dart:typed_data';
import 'dart:convert';

class VERSION {
  final int major;
  final int minor;
  final int patch;
  final String build;

  const VERSION({
    this.major = 0,
    this.minor = 0,
    this.patch = 0,
    this.build = "",
  });

  factory VERSION.parse(String? version) {
    final versionParts = version?.split(".");
    if (versionParts != null) {
      return VERSION(
        major: versionParts.isNotEmpty ? int.parse(versionParts[0]) : 0,
        minor: versionParts.length > 1 ? int.parse(versionParts[1]) : 0,
        patch: versionParts.length > 2 ? int.parse(versionParts[2]) : 0,
        build: versionParts.length > 3 ? versionParts[3] : "",
      );
    }
    return const VERSION();
  }
}

const BFAST_VERSION = VERSION(major: 1, minor: 0, patch: 1, build: "2019.9.24");

// Magic numbers for identifying a BFAST format
const int MAGIC = 0xBFA5;
const int TMP = 0xA5BF;
const int SWAPPED_MAGIC = TMP << 48;

// The size of the header
const int HEADER_SIZE = 32;
// The size of array offsets
const int ARRAY_OFFSET_SIZE = 16;
// This is the size of the header + padding to bring to alignment
const int ARRAY_OFFSETS_START = 32;
// This is sufficient alignment to fit objects natively into 256-bit (32 byte) registers
const int ALIGNMENT = 64;
final String ZERO_BYTE = String.fromCharCode(0);

// The array offset indicates where in the raw byte array (offset from beginning of BFAST byte stream) that a particular array's data can be found.
class ArrayOffset {
  final int begin;
  final int end;

  const ArrayOffset(this.begin, this.end);
}

// A data structure at the top of the file. This is followed by 32 bytes of padding, then an array of n array_offsets (where n is equal to num_arrays)
class Header {
  // Either MAGIC (same-endian) of SWAPPED_MAGIC (different-endian)
  final int magic;
  // >= desc_end and modulo 64 == 0 and <= file_size
  final int dataStart;
  // >= data_start and <= file_size
  final int dataEnd;
  // number of array_headers
  final int numArrays;

  const Header(
    this.magic,
    this.dataStart,
    this.dataEnd,
    this.numArrays,
  );
}

// A helper struct for representing a range of bytes
class ByteRange {
  final int begin;
  final int end;
  int get size => end - begin;

  const ByteRange(this.begin, this.end);
}

// A Bfast buffer conceptually is a name and a byte-range
class Buffer {
  final String name;
  final ByteRange range;

  const Buffer(this.name, this.range);
}

// A Bfast conceptually is a collection of buffers: named byte arrays.
// It contains the raw data contained within.
class Bfast {
  final Uint8List data;
  final List<Buffer> buffers;

  const Bfast(this.data, this.buffers);
  // Unpacks an array of buffers into a BFastData package
  factory Bfast.unpack(Uint8List data) {
    final range = ByteRange(0, data.length);
    final header = data.buffer.asUint64List(range.begin, 4);
    final h = Header(header[0], header[1], header[2], header[3]);
    if (h.magic != MAGIC) throw "invalid magic number, either not a BFast, or was created on a machine with different endianess";
    if (h.dataEnd < h.dataStart) throw "data ends before it starts";

    final start = range.begin + ARRAY_OFFSETS_START;
    List<ByteRange> ranges = [];
    ArrayOffset? prev;
    for (int i = 0; i < h.numArrays; ++i) {
      final offsetsData = data.buffer.asUint64List(start + i * 2 * 8, 2);
      final offset = ArrayOffset(offsetsData[0], offsetsData[1]);
      if (offset.begin > offset.end) throw "Offset begin is after the offset end";
      if (offset.end > range.size) throw "Offset end is after the end of the data";
      if (prev != null && offset.begin < prev.end) throw "Offset begin is before the end of the previous offset";
      final begin = range.begin + offset.begin;
      final end = range.begin + offset.end;
      ranges.add(ByteRange(begin, end));
      prev = offset;
    }

    if (ranges.isEmpty) throw "Empty ranges list";
    // Removing the trailing '\0' before spliting the names
    final names = data.splitRange(ranges[0], ZERO_BYTE).where((n) => n.isNotEmpty).toList(growable: false);
    if (names.length != ranges.length - 1) throw "The number of names does not match the raw data size";
    final buffers = <Buffer>[];
    for (int i = 0; i < names.length; ++i) {
      buffers.add(Buffer(names[i], ranges[i + 1]));
    }
    return Bfast(data, buffers);
  }

  static Future<Bfast> readFile(String file) async {
    final fstrm = File(file);
    final buffer = await fstrm.readAsBytes();
    return Bfast.unpack(buffer);
  }
}

extension Uint8ListRangeExtensions on Uint8List {
// Splits names separated by null characters
  List<String> splitRange(ByteRange range, [Pattern? pattern]) {
    return split(range.begin, range.end, pattern);
  }

  List<String> split(int start, [int? end, Pattern? pattern]) {
    final strings = utf8.decode(sublist(start, end), allowMalformed: true);
    final p = pattern ?? ZERO_BYTE;
    return strings.split(p).toList(growable: false);
  }
}
