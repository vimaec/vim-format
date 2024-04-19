/**
 * @module vim-ts
 */
import { BFast } from './bfast';
import { MeshSection } from './g3d';
export declare type FilterMode = undefined | 'mesh' | 'instance' | 'group' | 'tag';
/**
 * See https://github.com/vimaec/vim#vim-geometry-attributes
 */
export declare class SceneAttributes {
    static readonly chunkCount = "g3d:chunk:count:0:int32:1";
    static readonly instanceMesh = "g3d:instance:mesh:0:int32:1";
    static readonly instanceMatrix = "g3d:instance:transform:0:float32:16";
    static readonly instanceNodes = "g3d:instance:node:0:int32:1";
    static readonly instanceGroups = "g3d:instance:group:0:int32:1";
    static readonly instanceTags = "g3d:instance:tag:0:int64:1";
    static readonly instanceFlags = "g3d:instance:tag:0:uint16:1";
    static readonly instanceMins = "g3d:instance:min:0:float32:3";
    static readonly instanceMaxs = "g3d:instance:max:0:float32:3";
    static readonly meshChunk = "g3d:mesh:chunk:0:int32:1";
    static readonly meshChunkIndices = "g3d:mesh:chunkindex:0:int32:1";
    static readonly meshIndexCounts = "g3d:mesh:indexcount:0:int32:1";
    static readonly meshVertexCounts = "g3d:mesh:vertexcount:0:int32:1";
    static readonly meshOpaqueIndexCount = "g3d:mesh:opaqueindexcount:0:int32:1";
    static readonly meshOpaqueVertexCount = "g3d:mesh:opaquevertexcount:0:int32:1";
}
/**
 * Represents the index, as in book index, of a collection of G3dMesh.
 * Allows to find and download G3dMesh as needed.
 * Allows to preallocate geometry to render G3dMeshes.
 */
export declare class G3dScene {
    chunkCount: number;
    instanceMeshes: Int32Array;
    instanceMatrices: Float32Array;
    instanceNodes: Int32Array;
    instanceGroups: Int32Array;
    instanceTags: BigInt64Array;
    instanceFlags: Uint16Array;
    instanceMins: Float32Array;
    instanceMaxs: Float32Array;
    meshChunks: Int32Array;
    meshChunkIndices: Int32Array;
    meshIndexCounts: Int32Array;
    meshVertexCounts: Int32Array;
    meshOpaqueIndexCounts: Int32Array;
    meshOpaqueVertexCounts: Int32Array;
    private nodeToInstance;
    constructor(chunkCount: Int32Array, instanceMeshes: Int32Array, instanceMatrices: Float32Array, instanceNodes: Int32Array, instanceGroups: Int32Array, instanceTags: BigInt64Array, instanceFlags: Uint16Array, instanceMins: Float32Array, instanceMaxs: Float32Array, meshChunks: Int32Array, meshChunkIndices: Int32Array, meshIndexCounts: Int32Array, meshVertexCounts: Int32Array, meshOpaqueIndexCounts: Int32Array, meshOpaqueVertexCounts: Int32Array);
    static createFromBfast(bfast: BFast): Promise<G3dScene>;
    getMeshCount(): number;
    getMeshIndexCount(mesh: number, section: MeshSection): number;
    getMeshVertexCount(mesh: number, section: MeshSection): number;
    getInstanceMin(instance: number): Float32Array;
    getInstanceMax(instance: number): Float32Array;
    getInstanceMatrix(instance: number): Float32Array;
}
