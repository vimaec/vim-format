"use strict";
/**
 * @module vim-ts
 */
Object.defineProperty(exports, "__esModule", { value: true });
exports.G3dScene = exports.SceneAttributes = void 0;
const abstractG3d_1 = require("./abstractG3d");
const bfast_1 = require("./bfast");
const g3d_1 = require("./g3d");
/**
 * See https://github.com/vimaec/vim#vim-geometry-attributes
 */
class SceneAttributes {
}
exports.SceneAttributes = SceneAttributes;
SceneAttributes.chunkCount = 'g3d:chunk:count:0:int32:1';
SceneAttributes.instanceMesh = 'g3d:instance:mesh:0:int32:1';
SceneAttributes.instanceTransform = 'g3d:instance:transform:0:int32:1';
SceneAttributes.instanceNodes = 'g3d:instance:node:0:int32:1';
SceneAttributes.instanceGroups = 'g3d:instance:group:0:int32:1';
SceneAttributes.instanceTags = 'g3d:instance:tag:0:int64:1';
SceneAttributes.instanceFlags = 'g3d:instance:tag:0:uint16:1';
SceneAttributes.instanceMins = 'g3d:instance:min:0:float32:3';
SceneAttributes.instanceMaxs = 'g3d:instance:max:0:float32:3';
SceneAttributes.meshChunk = 'g3d:mesh:chunk:0:int32:1';
SceneAttributes.meshChunkIndices = 'g3d:mesh:chunkindex:0:int32:1';
SceneAttributes.meshInstanceCounts = 'g3d:mesh:instancecount:0:int32:1';
SceneAttributes.meshIndexCounts = 'g3d:mesh:indexcount:0:int32:1';
SceneAttributes.meshVertexCounts = 'g3d:mesh:vertexcount:0:int32:1';
SceneAttributes.meshOpaqueIndexCount = "g3d:mesh:opaqueindexcount:0:int32:1";
SceneAttributes.meshOpaqueVertexCount = "g3d:mesh:opaquevertexcount:0:int32:1";
SceneAttributes.all = [
    SceneAttributes.chunkCount,
    SceneAttributes.instanceMesh,
    SceneAttributes.instanceTransform,
    SceneAttributes.instanceNodes,
    SceneAttributes.instanceGroups,
    SceneAttributes.instanceTags,
    SceneAttributes.instanceFlags,
    SceneAttributes.instanceMins,
    SceneAttributes.instanceMaxs,
    SceneAttributes.meshChunk,
    SceneAttributes.meshChunkIndices,
    SceneAttributes.meshInstanceCounts,
    SceneAttributes.meshIndexCounts,
    SceneAttributes.meshVertexCounts,
    SceneAttributes.meshOpaqueIndexCount,
    SceneAttributes.meshOpaqueVertexCount,
];
/**
 * Represents the index, as in book index, of a collection of G3dMesh.
 * Allows to find and download G3dMesh as needed.
 * Allows to preallocate geometry to render G3dMeshes.
 */
class G3dScene {
    constructor(rawG3d, chunkCount, instanceMeshes, instanceTransform, instanceNodes, instanceGroups, instanceTags, instanceFlags, instanceMins, instanceMaxs, meshChunks, meshChunkIndices, meshInstanceCounts, meshIndexCounts, meshVertexCounts, meshOpaqueIndexCounts, meshOpaqueVertexCounts) {
        this.rawG3d = rawG3d;
        this.chunkCount = chunkCount[0];
        this.instanceMeshes = instanceMeshes;
        this.instanceTransforms = instanceTransform;
        this.instanceNodes = instanceNodes;
        this.instanceGroups = instanceGroups;
        this.instanceTags = instanceTags;
        this.instanceFlags = instanceFlags;
        this.instanceMins = instanceMins;
        this.instanceMaxs = instanceMaxs;
        this.meshChunks = meshChunks;
        this.meshChunkIndices = meshChunkIndices;
        this.meshInstanceCounts = meshInstanceCounts;
        this.meshIndexCounts = meshIndexCounts;
        this.meshVertexCounts = meshVertexCounts;
        this.meshOpaqueIndexCounts = meshOpaqueIndexCounts;
        this.meshOpaqueVertexCounts = meshOpaqueVertexCounts;
        // Reverse instanceNodes
        this.nodeToInstance = new Map();
        for (let i = 0; i < this.instanceNodes.length; i++) {
            this.nodeToInstance.set(this.instanceNodes[i], i);
        }
        this.meshSceneInstances = this.createMap();
    }
    createMap() {
        // From : (mesh, scene-index) -> mesh-index 
        // To: (mesh, mesh-index) -> scene-index
        const map = new Map();
        for (let i = 0; i < this.instanceMeshes.length; i++) {
            const mesh = this.instanceMeshes[i];
            const index = this.instanceTransforms[i];
            const indices = map.get(mesh) ?? new Map();
            indices.set(index, i);
            map.set(mesh, indices);
        }
        return map;
    }
    static createFromAbstract(g3d) {
        function getArray(attribute) {
            return g3d.findAttribute(attribute)?.data;
        }
        return new G3dScene(g3d, getArray(SceneAttributes.chunkCount), getArray(SceneAttributes.instanceMesh), getArray(SceneAttributes.instanceTransform), getArray(SceneAttributes.instanceNodes), getArray(SceneAttributes.instanceGroups), getArray(SceneAttributes.instanceTags), getArray(SceneAttributes.instanceFlags), getArray(SceneAttributes.instanceMins), getArray(SceneAttributes.instanceMaxs), getArray(SceneAttributes.meshChunk), getArray(SceneAttributes.meshChunkIndices), getArray(SceneAttributes.meshInstanceCounts), getArray(SceneAttributes.meshIndexCounts), getArray(SceneAttributes.meshVertexCounts), getArray(SceneAttributes.meshOpaqueIndexCount), getArray(SceneAttributes.meshOpaqueVertexCount));
    }
    static async createFromPath(path) {
        const f = await fetch(path);
        const buffer = await f.arrayBuffer();
        const bfast = new bfast_1.BFast(buffer);
        return this.createFromBfast(bfast);
    }
    static async createFromBfast(bfast) {
        const g3d = await abstractG3d_1.AbstractG3d.createFromBfast(bfast, SceneAttributes.all);
        return G3dScene.createFromAbstract(g3d);
    }
    getMeshSceneInstance(mesh, meshIndex) {
        return this.meshSceneInstances.get(mesh)?.get(meshIndex);
    }
    getMeshCount() {
        return this.meshInstanceCounts.length;
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
    getMeshInstanceCount(mesh) {
        return this.meshInstanceCounts[mesh];
    }
    getNodeMin(node) {
        const instance = this.nodeToInstance.get(node);
        if (!instance) {
            return undefined;
        }
        return this.getInstanceMin(instance);
    }
    getNodeMax(node) {
        const instance = this.nodeToInstance.get(node);
        if (!instance) {
            return undefined;
        }
        return this.getInstanceMax(instance);
    }
    getInstanceMin(instance) {
        return this.instanceMins.subarray(instance * g3d_1.G3d.POSITION_SIZE);
    }
    getInstanceMax(instance) {
        return this.instanceMaxs.subarray(instance * g3d_1.G3d.POSITION_SIZE);
    }
}
exports.G3dScene = G3dScene;
