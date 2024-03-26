"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RemoteGeometry = void 0;
const bfast_1 = require("./bfast");
const g3dMaterials_1 = require("./g3dMaterials");
const g3dMesh_1 = require("./g3dMesh");
const g3dScene_1 = require("./g3dScene");
const remoteBuffer_1 = require("./remoteBuffer");
const remoteValue_1 = require("./remoteValue");
class RemoteGeometry {
    constructor(bfast) {
        this.bfast = bfast;
        this.scene = new remoteValue_1.RemoteValue(() => this.requestIndex());
        this.sceneRaw = new remoteValue_1.RemoteValue(() => this.requestIndexRaw());
    }
    static async fromPath(path) {
        const buffer = new remoteBuffer_1.RemoteBuffer(path);
        const bfast = new bfast_1.BFast(buffer);
        return new RemoteGeometry(bfast);
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
    /**
     * Fetches and returns the vimx G3dMeshIndex
     */
    async requestIndex() {
        const index = await this.bfast.getLocalBfast('index', true);
        return g3dScene_1.G3dScene.createFromBfast(index);
    }
    async requestIndexRaw() {
        const index = await this.bfast.getLocalBfastRaw('index', true);
        return g3dScene_1.G3dScene.createFromBfast(index);
    }
    async getIndex() {
        return this.scene.get();
    }
    async getIndexRaw() {
        return this.sceneRaw.get();
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
    async getMesh(mesh) {
        const m = await this.bfast.getLocalBfast(`mesh_${mesh}`, true);
        const result = await g3dMesh_1.G3dMesh.createFromBfast(m);
        const scene = await this.scene.get();
        result.scene = scene;
        result.meshIndex = mesh;
        return result;
    }
    /**
     * Fetches and returns the vimx G3dMaterials
     */
    async getMaterialsRaw() {
        const mat = await this.bfast.getLocalBfastRaw('materials', true);
        return g3dMaterials_1.G3dMaterial.createFromBfast(mat);
    }
    /**
     * Fetches and returns the vimx G3dMesh with given index
     */
    async getMeshRaw(mesh) {
        const m = await this.bfast.getLocalBfastRaw(`mesh_${mesh}`, true);
        const result = await g3dMesh_1.G3dMesh.createFromBfast(m);
        const scene = await this.sceneRaw.get();
        result.scene = scene;
        result.meshIndex = mesh;
        return result;
    }
}
exports.RemoteGeometry = RemoteGeometry;
