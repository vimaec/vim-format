"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RemoteVimx = void 0;
const g3dMaterials_1 = require("./g3d/g3dMaterials");
const g3dChunk_1 = require("./g3d/g3dChunk");
const g3dScene_1 = require("./g3d/g3dScene");
const remoteValue_1 = require("./http/remoteValue");
const vimHeader_1 = require("./vimHeader");
const g3dMesh_1 = require("./g3d/g3dMesh");
class RemoteVimx {
    constructor(bfast) {
        this.chunkCache = new Map();
        this.bfast = bfast;
        this.scene = new remoteValue_1.RemoteValue(() => this.requestScene());
    }
    /**
     * Aborts all downloads from the underlying BFAST.
     */
    abort() {
        this.bfast.abort();
        this.scene.abort();
        this.chunkCache.forEach(c => c.abort());
    }
    /**
     * Downloads underlying bfast making all subsequent request local.
     */
    async download() {
        this.bfast.forceDownload();
    }
    async requestScene() {
        const index = await this.bfast.getLocalBfast('scene', true);
        return g3dScene_1.G3dScene.createFromBfast(index);
    }
    async getHeader() {
        return (0, vimHeader_1.requestHeader)(this.bfast);
    }
    /**
   * Fetches and returns the vimx G3dScene
   */
    async getScene() {
        return this.scene.get();
    }
    /**
     * Fetches and returns the vimx G3dMaterials
     */
    async getMaterials() {
        const mat = await this.bfast.getLocalBfast('materials', true);
        return g3dMaterials_1.G3dMaterial.createFromBfast(mat);
    }
    /**
     * Fetches and returns the vimx G3dMesh with given index
     */
    async getChunk(chunk) {
        var cached = this.chunkCache.get(chunk);
        if (cached !== undefined) {
            return cached.get();
        }
        var value = new remoteValue_1.RemoteValue(() => this.requestChunk(chunk));
        this.chunkCache.set(chunk, value);
        return value.get();
    }
    async requestChunk(chunk) {
        const chunkBFast = await this.bfast.getLocalBfast(`chunk_${chunk}`, true);
        return g3dChunk_1.G3dChunk.createFromBfast(chunkBFast);
    }
    async getMesh(mesh) {
        var scene = await this.scene.get();
        var meshChunk = scene.meshChunks[mesh];
        if (meshChunk === undefined)
            return undefined;
        var chunk = await this.getChunk(meshChunk);
        if (chunk === undefined)
            return undefined;
        return new g3dMesh_1.G3dMesh(scene, chunk, scene.meshChunkIndices[mesh]);
    }
}
exports.RemoteVimx = RemoteVimx;
