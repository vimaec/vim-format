/**
 * @module vim-ts
 */
import { BFast } from './bfast';
import { MeshSection } from './g3d';
import { G3dScene } from './g3dScene';
/**
 * See https://github.com/vimaec/vim#vim-geometry-attributes
 */
export declare class MeshAttributes {
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
    readonly meshOpaqueSubmeshCount: number;
    readonly submeshIndexOffset: Int32Array;
    readonly submeshVertexOffset: Int32Array;
    readonly submeshMaterial: Int32Array;
    readonly positions: Float32Array;
    readonly indices: Uint32Array;
    static readonly COLOR_SIZE = 4;
    static readonly POSITION_SIZE = 3;
    /**
     * Opaque white
     */
    static readonly DEFAULT_COLOR: Float32Array;
    constructor(meshOpaqueSubmeshCount: number, submeshIndexOffsets: Int32Array, submeshVertexOffsets: Int32Array, submeshMaterials: Int32Array, indices: Int32Array | Uint32Array, positions: Float32Array);
    static createFromBfast(bfast: BFast): Promise<G3dMesh>;
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
