"use strict";
/**
 * @module vim-ts
 */
Object.defineProperty(exports, "__esModule", { value: true });
exports.G3dScene = exports.SceneAttributes = void 0;
const g3d_1 = require("./g3d");
/**
 * See https://github.com/vimaec/vim#vim-geometry-attributes
 */
class SceneAttributes {
}
exports.SceneAttributes = SceneAttributes;
SceneAttributes.chunkCount = 'g3d:chunk:count:0:int32:1';
SceneAttributes.instanceMesh = 'g3d:instance:mesh:0:int32:1';
SceneAttributes.instanceMatrix = 'g3d:instance:transform:0:float32:16';
SceneAttributes.instanceNodes = 'g3d:instance:node:0:int32:1';
SceneAttributes.instanceGroups = 'g3d:instance:group:0:int32:1';
SceneAttributes.instanceTags = 'g3d:instance:tag:0:int64:1';
SceneAttributes.instanceFlags = 'g3d:instance:tag:0:uint16:1';
SceneAttributes.instanceMins = 'g3d:instance:min:0:float32:3';
SceneAttributes.instanceMaxs = 'g3d:instance:max:0:float32:3';
SceneAttributes.meshChunk = 'g3d:mesh:chunk:0:int32:1';
SceneAttributes.meshChunkIndices = 'g3d:mesh:chunkindex:0:int32:1';
SceneAttributes.meshIndexCounts = 'g3d:mesh:indexcount:0:int32:1';
SceneAttributes.meshVertexCounts = 'g3d:mesh:vertexcount:0:int32:1';
SceneAttributes.meshOpaqueIndexCount = "g3d:mesh:opaqueindexcount:0:int32:1";
SceneAttributes.meshOpaqueVertexCount = "g3d:mesh:opaquevertexcount:0:int32:1";
/**
 * Represents the index, as in book index, of a collection of G3dMesh.
 * Allows to find and download G3dMesh as needed.
 * Allows to preallocate geometry to render G3dMeshes.
 */
class G3dScene {
    constructor(chunkCount, instanceMeshes, instanceMatrices, instanceNodes, instanceGroups, instanceTags, instanceFlags, instanceMins, instanceMaxs, meshChunks, meshChunkIndices, meshIndexCounts, meshVertexCounts, meshOpaqueIndexCounts, meshOpaqueVertexCounts) {
        this.chunkCount = chunkCount[0];
        this.instanceMeshes = instanceMeshes;
        this.instanceMatrices = instanceMatrices;
        this.instanceNodes = instanceNodes;
        this.instanceGroups = instanceGroups;
        this.instanceTags = instanceTags;
        this.instanceFlags = instanceFlags;
        this.instanceMins = instanceMins;
        this.instanceMaxs = instanceMaxs;
        this.meshChunks = meshChunks;
        this.meshChunkIndices = meshChunkIndices;
        this.meshIndexCounts = meshIndexCounts;
        this.meshVertexCounts = meshVertexCounts;
        this.meshOpaqueIndexCounts = meshOpaqueIndexCounts;
        this.meshOpaqueVertexCounts = meshOpaqueVertexCounts;
        // Reverse instanceNodes
        this.nodeToInstance = new Map();
        for (let i = 0; i < this.instanceNodes.length; i++) {
            this.nodeToInstance.set(this.instanceNodes[i], i);
        }
    }
    static async createFromBfast(bfast) {
        const values = await Promise.all([
            bfast.getInt32Array(SceneAttributes.chunkCount),
            bfast.getInt32Array(SceneAttributes.instanceMesh),
            bfast.getFloat32Array(SceneAttributes.instanceMatrix),
            bfast.getInt32Array(SceneAttributes.instanceNodes),
            bfast.getInt32Array(SceneAttributes.instanceGroups),
            bfast.getBigInt64Array(SceneAttributes.instanceTags),
            bfast.getUint16Array(SceneAttributes.instanceFlags),
            bfast.getFloat32Array(SceneAttributes.instanceMins),
            bfast.getFloat32Array(SceneAttributes.instanceMaxs),
            bfast.getInt32Array(SceneAttributes.meshChunk),
            bfast.getInt32Array(SceneAttributes.meshChunkIndices),
            bfast.getInt32Array(SceneAttributes.meshIndexCounts),
            bfast.getInt32Array(SceneAttributes.meshVertexCounts),
            bfast.getInt32Array(SceneAttributes.meshOpaqueIndexCount),
            bfast.getInt32Array(SceneAttributes.meshOpaqueVertexCount),
        ]);
        return new G3dScene(...values);
    }
    getMeshCount() {
        return this.meshChunks.length;
    }
    getMeshIndexCount(mesh, section) {
        const all = this.meshIndexCounts[mesh];
        if (section === 'all')
            return all;
        const opaque = this.meshOpaqueIndexCounts[mesh];
        return section === 'opaque' ? opaque : all - opaque;
    }
    getMeshVertexCount(mesh, section) {
        const all = this.meshVertexCounts[mesh];
        if (section === 'all')
            return all;
        const opaque = this.meshOpaqueVertexCounts[mesh];
        return section === 'opaque' ? opaque : all - opaque;
    }
    getInstanceMin(instance) {
        return this.instanceMins.subarray(instance * g3d_1.G3d.POSITION_SIZE);
    }
    getInstanceMax(instance) {
        return this.instanceMaxs.subarray(instance * g3d_1.G3d.POSITION_SIZE);
    }
    getInstanceMatrix(instance) {
        return this.instanceMatrices.subarray(instance * g3d_1.G3d.MATRIX_SIZE, (instance + 1) * g3d_1.G3d.MATRIX_SIZE);
    }
}
exports.G3dScene = G3dScene;
