/**
 * @module vim-ts
 */
import { AbstractG3d } from './abstractG3d';
import { BFast } from './bfast';
import { MeshSection } from './g3d';
import { G3dScene } from './g3dScene';
/**
 * See https://github.com/vimaec/vim#vim-geometry-attributes
 */
export declare class MeshAttributes {
    static instanceTransforms: string;
    static meshOpaqueSubmeshCount: string;
    static submeshIndexOffsets: string;
    static submeshVertexOffsets: string;
    static submeshMaterials: string;
    static positions: string;
    static indices: string;
    static all: string[];
}
/**
 * G3D is a simple, efficient, generic binary format for storing and transmitting geometry.
 * The G3D format is designed to be used either as a serialization format or as an in-memory data structure.
 * A G3d with specific attributes according to the VIM format specification.
 * See https://github.com/vimaec/vim#vim-geometry-attributes for the vim specification.
 * See https://github.com/vimaec/g3d for the g3d specification.
 */
export declare class G3dMesh {
    scene: G3dScene;
    meshIndex: number;
    rawG3d: AbstractG3d;
    instanceTransforms: Float32Array;
    meshOpaqueSubmeshCount: number;
    submeshIndexOffset: Int32Array;
    submeshVertexOffset: Int32Array;
    submeshMaterial: Int32Array;
    positions: Float32Array;
    indices: Uint32Array;
    static MATRIX_SIZE: number;
    static COLOR_SIZE: number;
    static POSITION_SIZE: number;
    /**
     * Opaque white
     */
    DEFAULT_COLOR: Float32Array;
    constructor(instanceTransforms: Float32Array, meshOpaqueSubmeshCount: number, submeshIndexOffsets: Int32Array, submeshVertexOffsets: Int32Array, submeshMaterials: Int32Array, indices: Int32Array | Uint32Array, positions: Float32Array);
    static createFromAbstract(g3d: AbstractG3d): G3dMesh;
    static createFromPath(path: string): Promise<G3dMesh>;
    static createFromBuffer(buffer: ArrayBuffer): Promise<G3dMesh>;
    static createFromBfast(bfast: BFast): Promise<G3dMesh>;
    getBimInstance(meshInstance: number): number;
    getInstanceMax(meshInstance: number): Float32Array;
    getInstanceMin(meshInstance: number): Float32Array;
    getInstanceGroup(meshInstance: number): number;
    getInstanceTag(meshInstance: number): bigint;
    getInstanceHasFlag(meshInstance: number, flag: number): boolean;
    getInstanceCount: () => number;
    /**
     * Returns an 16 number array representation of the matrix for given instance
     * @param instance g3d instance index
     */
    getInstanceMatrix(instance: number): Float32Array;
    getVertexStart(section?: MeshSection): number;
    getVertexEnd(section?: MeshSection): number;
    getVertexCount(section?: MeshSection): number;
    getIndexStart(section?: MeshSection): number;
    getIndexEnd(section?: MeshSection): number;
    getIndexCount(section?: MeshSection): number;
    getHasTransparency(): boolean;
    getSubmeshStart(section: MeshSection): number;
    getSubmeshEnd(section: MeshSection): number;
    getSubmeshCount(section: MeshSection): number;
    getSubmeshIndexStart(submesh: number): number;
    getSubmeshIndexEnd(submesh: number): number;
    getSubmeshIndexCount(submesh: number): number;
    getSubmeshVertexStart(submesh: number): number;
    getSubmeshVertexEnd(submesh: number): number;
    getSubmeshVertexCount(submesh: number): number;
}
