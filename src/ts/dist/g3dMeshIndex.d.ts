/**
 * @module vim-ts
 */
import { AbstractG3d } from './abstractG3d';
import { BFast } from './bfast';
import { MeshSection } from './g3d';
export declare type FilterMode = undefined | 'mesh' | 'instance' | 'group' | 'tag';
/**
 * See https://github.com/vimaec/vim#vim-geometry-attributes
 */
export declare class SceneAttributes {
    static instanceFiles: string;
    static instanceIndices: string;
    static instanceNodes: string;
    static instanceGroups: string;
    static instanceTags: string;
    static instanceMins: string;
    static instanceMaxs: string;
    static meshInstanceCounts: string;
    static meshIndexCounts: string;
    static meshVertexCounts: string;
    static meshOpaqueIndexCount: string;
    static meshOpaqueVertexCount: string;
    static all: string[];
}
/**
 * Represents the index, as in book index, of a collection of G3dMesh.
 * Allows to find and download G3dMesh as needed.
 * Allows to preallocate geometry to render G3dMeshes.
 */
export declare class G3dScene {
    rawG3d: AbstractG3d;
    instanceMeshes: Int32Array;
    instanceIndices: Int32Array;
    instanceNodes: Int32Array;
    instanceGroups: Int32Array;
    instanceTags: BigInt64Array;
    instanceMins: Float32Array;
    instanceMaxs: Float32Array;
    meshInstanceCounts: Int32Array;
    meshIndexCounts: Int32Array;
    meshVertexCounts: Int32Array;
    meshOpaqueIndexCounts: Int32Array;
    meshOpaqueVertexCounts: Int32Array;
    private nodeToInstance;
    private meshSceneInstances;
    constructor(rawG3d: AbstractG3d, instanceFiles: Int32Array, instanceIndices: Int32Array, instanceNodes: Int32Array, instanceGroups: Int32Array, instanceTags: BigInt64Array, instanceMins: Float32Array, instanceMaxs: Float32Array, meshInstanceCounts: Int32Array, meshIndexCounts: Int32Array, meshVertexCounts: Int32Array, meshOpaqueIndexCounts: Int32Array, meshOpaqueVertexCounts: Int32Array);
    private createMap;
    static createFromAbstract(g3d: AbstractG3d): G3dScene;
    static createFromPath(path: string): Promise<G3dScene>;
    static createFromBfast(bfast: BFast): Promise<G3dScene>;
    getMeshSceneInstance(mesh: number, meshIndex: number): number;
    getMeshCount(): number;
    getMeshIndexCount(mesh: number, section: MeshSection): number;
    getMeshVertexCount(mesh: number, section: MeshSection): number;
    getMeshInstanceCount(mesh: number): number;
    getNodeMin(node: number): Float32Array;
    getNodeMax(node: number): Float32Array;
    getInstanceMin(instance: number): Float32Array;
    getInstanceMax(instance: number): Float32Array;
}
