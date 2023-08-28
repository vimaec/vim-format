
export class G3dAttributeDescriptor {
  // original descriptor string
  description: string
  // Indicates the part of the geometry that this attribute is associated with
  association: string
  // the role of the attribute
  semantic: string
  // each attribute type should have it's own index ( you can have uv0, uv1, etc. )
  attributeTypeIndex: string
  // the type of individual values (e.g. int32, float64)
  dataType: string
  // how many values associated with each element (e.g. UVs might be 2, geometry might be 3, quaternions 4, matrices 9 or 16)
  dataArity: number

  constructor (
    description: string,
    association: string,
    semantic: string,
    attributeTypeIndex: string,
    dataType: string,
    dataArity: string
  ) {
    if (!description.startsWith('g3d:')) {
      throw new Error(`${description} must start with 'g3d'`)
    }

    this.description = description
    this.association = association
    this.semantic = semantic
    this.attributeTypeIndex = attributeTypeIndex
    this.dataType = dataType
    this.dataArity = parseInt(dataArity)
  }

  static fromString (descriptor: string): G3dAttributeDescriptor {
    const desc = descriptor.split(':')

    if (desc.length !== 6) {
      throw new Error(`${descriptor}, must have 6 components delimited by ':'`)
    }

    return new this(descriptor, desc[1], desc[2], desc[3], desc[4], desc[5])
  }

  matches (other: G3dAttributeDescriptor) {
    const match = (a: string, b: string) => a === '*' || b === '*' || a === b

    return (
      match(this.association, other.association) &&
      match(this.semantic, other.semantic) &&
      match(this.attributeTypeIndex, other.attributeTypeIndex) &&
      match(this.dataType, other.dataType)
    )
  }
}


export type TypedArray =
  | Uint8Array
  | Int16Array
  | Uint16Array
  | Int32Array
  | Uint32Array
  | Float32Array
  | Uint32Array
  | BigUint64Array
  | BigInt64Array
  | Float64Array

export class G3dAttribute {
  descriptor: G3dAttributeDescriptor
  bytes: Uint8Array
  data: TypedArray | undefined

  constructor (descriptor: G3dAttributeDescriptor, bytes: Uint8Array) {
    this.descriptor = descriptor
    this.bytes = bytes
    this.data = G3dAttribute.castData(bytes, descriptor.dataType)
  }

  static fromString (descriptor: string, buffer: Uint8Array): G3dAttribute {
    return new this(G3dAttributeDescriptor.fromString(descriptor), buffer)
  }

  // Converts a VIM attribute into a typed array from its raw data
  static castData (bytes: Uint8Array, dataType: string): TypedArray | undefined {
    switch (dataType) {
      case 'float32':
        return new Float32Array(
          bytes.buffer,
          bytes.byteOffset,
          bytes.byteLength / 4
        )
      case 'float64':
        throw new Float64Array(
          bytes.buffer,
          bytes.byteOffset,
          bytes.byteLength / 8
        )
      case 'uint8':
      case 'int8':
        return bytes
      case 'int16':
        return new Int16Array(
          bytes.buffer,
          bytes.byteOffset,
          bytes.byteLength / 2
        )
      case 'uint16':
        return new Uint16Array(
          bytes.buffer,
          bytes.byteOffset,
          bytes.byteLength / 2
        )
      case 'int32':
        return new Int32Array(
          bytes.buffer,
          bytes.byteOffset,
          bytes.byteLength / 4
        )
      case 'uint32':
        return new Uint32Array(
          bytes.buffer,
          bytes.byteOffset,
          bytes.byteLength / 4
        )
      case 'int64':
        return new BigInt64Array(
          bytes.buffer,
          bytes.byteOffset,
          bytes.byteLength / 8
        )
      case 'uint64':
        return new BigUint64Array(
          bytes.buffer,
          bytes.byteOffset,
          bytes.byteLength / 8
        )
      default:
        console.error('Unrecognized attribute data type ' + dataType)
    }
  }
}
