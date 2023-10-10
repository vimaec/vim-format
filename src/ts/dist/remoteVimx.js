"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RemoteVimx = void 0;
const bfast_1 = require("./bfast");
const g3dMaterials_1 = require("./g3dMaterials");
const g3dMesh_1 = require("./g3dMesh");
const g3dScene_1 = require("./g3dScene");
const remoteBuffer_1 = require("./remoteBuffer");
const remoteValue_1 = require("./remoteValue");
class RemoteVimx {
    constructor(bfast) {
        this.chunkCache = new Map();
        this.bfast = bfast;
        this.scene = new remoteValue_1.RemoteValue(() => this.requestScene());
    }
    static async fromPath(path) {
        const buffer = new remoteBuffer_1.RemoteBuffer(path);
        const bfast = new bfast_1.BFast(buffer);
        return new RemoteVimx(bfast);
    }
    /**
     * Aborts all downloads from the underlying BFAST.
     */
    abort() {
        this.bfast.abort();
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
        var ranges = await chunkBFast.getRanges();
        const keys = [...ranges.keys()];
        var bfasts = await Promise.all(keys.map(k => chunkBFast.getBfast(k)));
        var meshes = await Promise.all(bfasts.map(b => g3dMesh_1.G3dMesh.createFromBfast(b)));
        const scene = await this.scene.get();
        meshes.forEach(m => m.scene = scene);
        return meshes;
    }
    async getMesh(mesh) {
        var scene = await this.scene.get();
        var chunk = scene.meshChunks[mesh];
        var meshes = await this.getChunk(chunk);
        var index = scene.meshChunkIndices[mesh];
        return meshes[index];
    }
}
exports.RemoteVimx = RemoteVimx;
