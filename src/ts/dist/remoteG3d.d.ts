import { BFast } from "./bfast";
import { G3d, G3dAttributeDescriptor, TypedArray } from "./g3d";
declare class G3dRemoteAttribute {
    descriptor: G3dAttributeDescriptor;
    bfast: BFast;
    constructor(descriptor: G3dAttributeDescriptor, bfast: BFast);
    getAll<T extends TypedArray>(): Promise<T>;
    getByte(index: number): Promise<number | BigInt>;
    getNumber(index: number): Promise<number>;
    getValue<T extends TypedArray>(index: number): Promise<T>;
    getValues<T extends TypedArray>(index: number, count: number): Promise<T>;
    getCount(): Promise<number>;
    static fromString(description: string, bfast: BFast): G3dRemoteAttribute;
}
/**
 * G3D is a simple, efficient, generic binary format for storing and transmitting geometry.
 * The G3D format is designed to be used either as a serialization format or as an in-memory data structure.
 * See https://github.com/vimaec/g3d
 */
export declare class RemoteAbstractG3d {
    meta: string;
    attributes: G3dRemoteAttribute[];
    constructor(meta: string, attributes: G3dRemoteAttribute[]);
    findAttribute(descriptor: string): G3dRemoteAttribute | undefined;
    /**
     * Create g3d from bfast by requesting all necessary buffers individually.
     */
    static createFromBfast(bfast: BFast): RemoteAbstractG3d;
}
export declare class RemoteG3d {
    rawG3d: RemoteAbstractG3d;
    positions: G3dRemoteAttribute;
    indices: G3dRemoteAttribute;
    instanceMeshes: G3dRemoteAttribute;
    instanceTransforms: G3dRemoteAttribute;
    instanceFlags: G3dRemoteAttribute;
    meshSubmeshes: G3dRemoteAttribute;
    submeshIndexOffsets: G3dRemoteAttribute;
    submeshMaterials: G3dRemoteAttribute;
    materialColors: G3dRemoteAttribute;
    constructor(g3d: RemoteAbstractG3d);
    static createFromBfast(bfast: BFast): RemoteG3d;
    getVertexCount: () => Promise<number>;
    getMeshCount: () => Promise<number>;
    getSubmeshCount: () => Promise<number>;
    getMeshIndexStart(mesh: number): Promise<number>;
    getMeshIndexEnd(mesh: number): Promise<number>;
    getMeshIndexCount(mesh: number): Promise<number>;
    getMeshIndices(mesh: number): Promise<Uint32Array>;
    getMeshSubmeshEnd(mesh: number): Promise<number>;
    getMeshSubmeshStart(mesh: number): Promise<number>;
    getMeshSubmeshCount(mesh: number): Promise<number>;
    getSubmeshIndexStart(submesh: number): Promise<number>;
    getSubmeshIndexEnd(submesh: number): Promise<number>;
    getSubmeshIndexCount(submesh: number): Promise<number>;
    toG3d(): Promise<G3d>;
    slice(instance: number): Promise<G3d>;
    filter(instances: number[]): Promise<G3d>;
    private filterInstances;
    private filterMesh;
    private filterSubmeshes;
    private filterIndices;
    private filterPositions;
    private filterMaterials;
}
export {};
