export declare class G3dAttributeDescriptor {
    description: string;
    association: string;
    semantic: string;
    attributeTypeIndex: string;
    dataType: string;
    dataArity: number;
    constructor(description: string, association: string, semantic: string, attributeTypeIndex: string, dataType: string, dataArity: string);
    static fromString(descriptor: string): G3dAttributeDescriptor;
    matches(other: G3dAttributeDescriptor): boolean;
}
export declare type TypedArray = Uint8Array | Int16Array | Uint16Array | Int32Array | Uint32Array | Float32Array | Uint32Array | BigUint64Array | BigInt64Array | Float64Array;
export declare class G3dAttribute {
    descriptor: G3dAttributeDescriptor;
    bytes: Uint8Array;
    data: TypedArray | undefined;
    constructor(descriptor: G3dAttributeDescriptor, bytes: Uint8Array);
    static fromString(descriptor: string, buffer: Uint8Array): G3dAttribute;
    static castData(bytes: Uint8Array, dataType: string): TypedArray | undefined;
}
