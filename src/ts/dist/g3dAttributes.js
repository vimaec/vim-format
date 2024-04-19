"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.G3dAttribute = exports.G3dAttributeDescriptor = void 0;
class G3dAttributeDescriptor {
    constructor(description, association, semantic, attributeTypeIndex, dataType, dataArity) {
        if (!description.startsWith('g3d:')) {
            throw new Error(`${description} must start with 'g3d'`);
        }
        this.description = description;
        this.association = association;
        this.semantic = semantic;
        this.attributeTypeIndex = attributeTypeIndex;
        this.dataType = dataType;
        this.dataArity = parseInt(dataArity);
    }
    static fromString(descriptor) {
        const desc = descriptor.split(':');
        if (desc.length !== 6) {
            throw new Error(`${descriptor}, must have 6 components delimited by ':'`);
        }
        return new this(descriptor, desc[1], desc[2], desc[3], desc[4], desc[5]);
    }
    matches(other) {
        const match = (a, b) => a === '*' || b === '*' || a === b;
        return (match(this.association, other.association) &&
            match(this.semantic, other.semantic) &&
            match(this.attributeTypeIndex, other.attributeTypeIndex) &&
            match(this.dataType, other.dataType));
    }
}
exports.G3dAttributeDescriptor = G3dAttributeDescriptor;
class G3dAttribute {
    constructor(descriptor, bytes) {
        this.descriptor = descriptor;
        this.bytes = bytes;
        this.data = G3dAttribute.castData(bytes, descriptor.dataType);
    }
    static fromString(descriptor, buffer) {
        return new this(G3dAttributeDescriptor.fromString(descriptor), buffer);
    }
    // Converts a VIM attribute into a typed array from its raw data
    static castData(bytes, dataType) {
        switch (dataType) {
            case 'float32':
                return new Float32Array(bytes.buffer, bytes.byteOffset, bytes.byteLength / 4);
            case 'float64':
                throw new Float64Array(bytes.buffer, bytes.byteOffset, bytes.byteLength / 8);
            case 'uint8':
            case 'int8':
                return bytes;
            case 'int16':
                return new Int16Array(bytes.buffer, bytes.byteOffset, bytes.byteLength / 2);
            case 'uint16':
                return new Uint16Array(bytes.buffer, bytes.byteOffset, bytes.byteLength / 2);
            case 'int32':
                return new Int32Array(bytes.buffer, bytes.byteOffset, bytes.byteLength / 4);
            case 'uint32':
                return new Uint32Array(bytes.buffer, bytes.byteOffset, bytes.byteLength / 4);
            case 'int64':
                return new BigInt64Array(bytes.buffer, bytes.byteOffset, bytes.byteLength / 8);
            case 'uint64':
                return new BigUint64Array(bytes.buffer, bytes.byteOffset, bytes.byteLength / 8);
            default:
                console.error('Unrecognized attribute data type ' + dataType);
        }
    }
}
exports.G3dAttribute = G3dAttribute;
