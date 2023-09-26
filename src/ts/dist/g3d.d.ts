/**
 * @module vim-ts
 */
import { AbstractG3d } from './abstractG3d';
import { BFast } from './bfast';
export declare type MeshSection = 'opaque' | 'transparent' | 'all';
/**
 * See https://github.com/vimaec/vim#vim-geometry-attributes
 */
export declare class VimAttributes {
    static positions: string;
    static indices: string;
    static instanceMeshes: string;
    static instanceTransforms: string;
    static instanceNodes: string;
    static instanceFlags: string;
    static meshSubmeshes: string;
    static submeshIndexOffsets: string;
    static submeshMaterials: string;
    static materialColors: string;
    static all: string[];
}
/**
 * G3D is a simple, efficient, generic binary format for storing and transmitting geometry.
 * The G3D format is designed to be used either as a serialization format or as an in-memory data structure.
 * A G3d with specific attributes according to the VIM format specification.
 * See https://github.com/vimaec/vim#vim-geometry-attributes for the vim specification.
 * See https://github.com/vimaec/g3d for the g3d specification.
 */
export declare class G3d {
    positions: Float32Array;
    indices: Uint32Array;
    instanceMeshes: Int32Array;
    instanceTransforms: Float32Array;
    instanceFlags: Uint16Array;
    meshSubmeshes: Int32Array;
    submeshIndexOffset: Int32Array;
    submeshMaterial: Int32Array;
    materialColors: Float32Array;
    instanceNodes: Int32Array;
    meshVertexOffsets: Int32Array;
    meshInstances: Array<Array<number>>;
    meshOpaqueCount: Int32Array;
    submeshVertexStart: Int32Array;
    submeshVertexEnd: Int32Array;
    rawG3d: AbstractG3d;
    static MATRIX_SIZE: number;
    static COLOR_SIZE: number;
    static POSITION_SIZE: number;
    /**
     * Opaque white
     */
    DEFAULT_COLOR: Float32Array;
    constructor(instanceMeshes: Int32Array, instanceFlags: Uint16Array | undefined, instanceTransforms: Float32Array, instanceNodes: Int32Array | undefined, meshSubmeshes: Int32Array, submeshIndexOffsets: Int32Array, submeshMaterials: Int32Array, indices: Int32Array | Uint32Array, positions: Float32Array, materialColors: Float32Array);
    private computeSubmeshVertexRange;
    static createFromAbstract(g3d: AbstractG3d): G3d;
    static createFromPath(path: string): Promise<G3d>;
    static createFromBfast(bfast: BFast): Promise<G3d>;
    /**
     * Computes the index of the first vertex of each mesh
     */
    private computeMeshVertexOffsets;
    /**
     * Computes all instances pointing to each mesh.
     */
    private computeMeshInstances;
    /**
     * Reorders submeshIndexOffset, submeshMaterials and indices
     * such that for each mesh, submeshes are sorted according to material alpha.
     * This enables efficient splitting of arrays into opaque and transparent continuous ranges.
     */
    private sortSubmeshes;
    /**
     * Stores result of getSubmeshIndexEnd for each submesh in an array
     */
    private computeSubmeshEnd;
    /**
     * Stores result of getMeshIndexStart for each mesh in an array
     */
    private computeMeshIndexOffsets;
    /**
     * Reorder submesh arrays and returns size of largest reordered mesh
     */
    private reorderSubmeshes;
    /**
     * Sorts the range from start to end in every array provided in arrays in increasing criterion order.
     * Using a simple bubble sort, there is a limited number of submeshes per mesh.
     */
    private Sort;
    /**
     * Reorders the index buffer to match the new order of the submesh arrays.
     */
    private reorderIndices;
    /**
     * Rebase indices to be relative to its own mesh instead of to the whole g3d
     */
    private rebaseIndices;
    private unbaseIndices;
    /**
     * Computes an array where true if any of the materials used by a mesh has transparency.
     */
    private computeMeshOpaqueCount;
    /**Given VIM instance indices returns the corresponding G3d indices */
    remapInstances(instances: number[]): number[];
    getVertexCount: () => number;
    getIndexCount: () => number;
    getMeshCount: () => number;
    getMeshInstanceCount(mesh: number): number;
    getMeshIndexStart(mesh: number, section?: MeshSection): number;
    getMeshIndexEnd(mesh: number, section?: MeshSection): number;
    getMeshIndexCount(mesh: number, section?: MeshSection): number;
    getMeshVertexStart(mesh: number): number;
    getMeshVertexEnd(mesh: number): number;
    getMeshVertexCount(mesh: number): number;
    getMeshSubmeshStart(mesh: number, section?: MeshSection): number;
    getMeshSubmeshEnd(mesh: number, section?: MeshSection): number;
    getMeshSubmeshCount(mesh: number, section?: MeshSection): number;
    getMeshHasTransparency(mesh: number): boolean;
    getSubmeshIndexStart(submesh: number): number;
    getSubmeshIndexEnd(submesh: number): number;
    getSubmeshIndexCount(submesh: number): number;
    getSubmeshVertexStart(submesh: number): number;
    getSubmeshVertexEnd(submesh: number): number;
    getSubmeshVertexCount(submesh: number): number;
    /**
     * Returns color of given submesh as a 4-number array (RGBA)
     * @param submesh g3d submesh index
     */
    getSubmeshColor(submesh: number): Float32Array;
    /**
     * Returns color of given submesh as a 4-number array (RGBA)
     * @param submesh g3d submesh index
     */
    getSubmeshAlpha(submesh: number): number;
    /**
     * Returns true if submesh is transparent.
     * @param submesh g3d submesh index
     */
    getSubmeshIsTransparent(submesh: number): boolean;
    /**
     * Returns the total number of mesh in the g3d
     */
    getSubmeshCount(): number;
    getInstanceCount: () => number;
    /**
     * Returns true if instance has given flag enabled.
     * @param instance instance to check.
     * @param flag to check against.
     */
    getInstanceHasFlag(instance: number, flag: number): boolean;
    /**
     * Returns mesh index of given instance
     * @param instance g3d instance index
     */
    getInstanceMesh(instance: number): number;
    /**
     * Returns an 16 number array representation of the matrix for given instance
     * @param instance g3d instance index
     */
    getInstanceMatrix(instance: number): Float32Array;
    getMaterialCount: () => number;
    /**
     * Returns color of given material as a 4-number array (RGBA)
     * @param material g3d material index
     */
    getMaterialColor(material: number): Float32Array;
    /**
     * Returns the alpha component of given material
     * @param material
     */
    getMaterialAlpha(material: number): number;
    /**
     * Concatenates two g3ds into a new g3d.
     * @deprecated
     */
    append(other: G3d): G3d;
    validate(): void;
}
