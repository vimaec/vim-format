"use strict";
/**
 * @module vim-ts
 */
Object.defineProperty(exports, "__esModule", { value: true });
exports.G3dMesh = exports.MeshAttributes = void 0;
const abstractG3d_1 = require("./abstractG3d");
const bfast_1 = require("./bfast");
const g3d_1 = require("./g3d");
/**
 * See https://github.com/vimaec/vim#vim-geometry-attributes
 */
class MeshAttributes {
}
exports.MeshAttributes = MeshAttributes;
MeshAttributes.instanceTransforms = 'g3d:instance:transform:0:float32:16';
MeshAttributes.meshOpaqueSubmeshCount = 'g3d:mesh:opaquesubmeshcount:0:int32:1';
MeshAttributes.submeshIndexOffsets = 'g3d:submesh:indexoffset:0:int32:1';
MeshAttributes.submeshVertexOffsets = 'g3d:submesh:vertexoffset:0:int32:1';
MeshAttributes.submeshMaterials = 'g3d:submesh:material:0:int32:1';
MeshAttributes.positions = 'g3d:vertex:position:0:float32:3';
MeshAttributes.indices = 'g3d:corner:index:0:int32:1';
MeshAttributes.all = [
    MeshAttributes.instanceTransforms,
    MeshAttributes.meshOpaqueSubmeshCount,
    MeshAttributes.submeshIndexOffsets,
    MeshAttributes.submeshVertexOffsets,
    MeshAttributes.submeshMaterials,
    MeshAttributes.positions,
    MeshAttributes.indices,
];
/**
 * G3D is a simple, efficient, generic binary format for storing and transmitting geometry.
 * The G3D format is designed to be used either as a serialization format or as an in-memory data structure.
 * A G3d with specific attributes according to the VIM format specification.
 * See https://github.com/vimaec/vim#vim-geometry-attributes for the vim specification.
 * See https://github.com/vimaec/g3d for the g3d specification.
 */
class G3dMesh {
    constructor(instanceTransforms, meshOpaqueSubmeshCount, submeshIndexOffsets, submeshVertexOffsets, submeshMaterials, indices, positions) {
        /**
         * Opaque white
         */
        this.DEFAULT_COLOR = new Float32Array([1, 1, 1, 1]);
        this.getInstanceCount = () => this.instanceTransforms.length;
        this.instanceTransforms = instanceTransforms;
        this.meshOpaqueSubmeshCount = meshOpaqueSubmeshCount;
        this.submeshIndexOffset = submeshIndexOffsets;
        this.submeshVertexOffset = submeshVertexOffsets;
        this.submeshMaterial = submeshMaterials;
        this.indices = indices instanceof Uint32Array ? indices : new Uint32Array(indices.buffer);
        this.positions = positions;
    }
    static createFromAbstract(g3d) {
        const instanceTransforms = g3d.findAttribute(MeshAttributes.instanceTransforms)?.data;
        const meshOpaqueSubmeshCountArray = g3d.findAttribute(MeshAttributes.meshOpaqueSubmeshCount)?.data;
        const meshOpaqueSubmeshCount = meshOpaqueSubmeshCountArray[0];
        const submeshIndexOffsets = g3d.findAttribute(MeshAttributes.submeshIndexOffsets)?.data;
        const submeshVertexOffsets = g3d.findAttribute(MeshAttributes.submeshVertexOffsets)?.data;
        const submeshMaterial = g3d.findAttribute(MeshAttributes.submeshMaterials)
            ?.data;
        const indices = g3d.findAttribute(MeshAttributes.indices)?.data;
        const positions = g3d.findAttribute(MeshAttributes.positions)
            ?.data;
        const result = new G3dMesh(instanceTransforms, meshOpaqueSubmeshCount, submeshIndexOffsets, submeshVertexOffsets, submeshMaterial, indices, positions);
        result.rawG3d = g3d;
        return result;
    }
    static async createFromPath(path) {
        const f = await fetch(path);
        const buffer = await f.arrayBuffer();
        var g3d = this.createFromBuffer(buffer);
        return g3d;
    }
    static async createFromBuffer(buffer) {
        const bfast = new bfast_1.BFast(buffer);
        return this.createFromBfast(bfast);
    }
    static async createFromBfast(bfast) {
        const g3d = await abstractG3d_1.AbstractG3d.createFromBfast(bfast, MeshAttributes.all);
        return G3dMesh.createFromAbstract(g3d);
    }
    // -----------Instances---------------
    getBimInstance(meshInstance) {
        const sceneInstance = this.scene.getMeshSceneInstance(this.meshIndex, meshInstance);
        return this.scene.instanceNodes[sceneInstance];
    }
    getInstanceMax(meshInstance) {
        const sceneInstance = this.scene.getMeshSceneInstance(this.meshIndex, meshInstance);
        return this.scene.instanceMaxs.subarray(sceneInstance * 3, (sceneInstance + 1) * 3);
    }
    getInstanceMin(meshInstance) {
        const sceneInstance = this.scene.getMeshSceneInstance(this.meshIndex, meshInstance);
        return this.scene.instanceMins.subarray(sceneInstance * 3, (sceneInstance + 1) * 3);
    }
    getInstanceGroup(meshInstance) {
        const sceneInstance = this.scene.getMeshSceneInstance(this.meshIndex, meshInstance);
        return this.scene.instanceGroups[sceneInstance];
    }
    getInstanceTag(meshInstance) {
        const sceneInstance = this.scene.getMeshSceneInstance(this.meshIndex, meshInstance);
        return this.scene.instanceTags[sceneInstance];
    }
    getInstanceHasFlag(meshInstance, flag) {
        const sceneInstance = this.scene.getMeshSceneInstance(this.meshIndex, meshInstance);
        return (this.scene.instanceFlags[sceneInstance] & flag) > 0;
    }
    /**
     * Returns an 16 number array representation of the matrix for given instance
     * @param instance g3d instance index
     */
    getInstanceMatrix(instance) {
        return this.instanceTransforms.subarray(instance * G3dMesh.MATRIX_SIZE, (instance + 1) * G3dMesh.MATRIX_SIZE);
    }
    // ------------- Mesh -----------------
    getVertexStart(section = 'all') {
        const sub = this.getSubmeshStart(section);
        return this.getSubmeshVertexStart(sub);
    }
    getVertexEnd(section = 'all') {
        const sub = this.getSubmeshEnd(section);
        return this.getSubmeshVertexStart(sub);
    }
    getVertexCount(section = 'all') {
        return this.getVertexEnd(section) - this.getVertexStart(section);
    }
    getIndexStart(section = 'all') {
        const sub = this.getSubmeshStart(section);
        return this.getSubmeshIndexStart(sub);
    }
    getIndexEnd(section = 'all') {
        const sub = this.getSubmeshEnd(section);
        return this.getSubmeshIndexStart(sub);
    }
    getIndexCount(section = 'all') {
        return this.getIndexEnd(section) - this.getIndexStart(section);
    }
    getHasTransparency() {
        return this.meshOpaqueSubmeshCount < this.submeshIndexOffset.length;
    }
    // ------------- Submeshes -----------------
    getSubmeshStart(section) {
        if (section === 'all')
            return 0;
        if (section === 'opaque')
            return 0;
        return this.meshOpaqueSubmeshCount;
    }
    getSubmeshEnd(section) {
        if (section === 'all')
            return this.submeshIndexOffset.length;
        if (section === 'transparent')
            return this.submeshIndexOffset.length;
        return this.meshOpaqueSubmeshCount;
    }
    getSubmeshCount(section) {
        return this.getSubmeshEnd(section) - this.getSubmeshStart(section);
    }
    getSubmeshIndexStart(submesh) {
        return submesh < this.submeshIndexOffset.length
            ? this.submeshIndexOffset[submesh]
            : this.indices.length;
    }
    getSubmeshIndexEnd(submesh) {
        return submesh < this.submeshIndexOffset.length - 1
            ? this.submeshIndexOffset[submesh + 1]
            : this.indices.length;
    }
    getSubmeshIndexCount(submesh) {
        return this.getSubmeshIndexEnd(submesh) - this.getSubmeshIndexStart(submesh);
    }
    getSubmeshVertexStart(submesh) {
        return submesh < this.submeshIndexOffset.length
            ? this.submeshVertexOffset[submesh]
            : this.positions.length / g3d_1.G3d.POSITION_SIZE;
    }
    getSubmeshVertexEnd(submesh) {
        return submesh < this.submeshVertexOffset.length - 1
            ? this.submeshVertexOffset[submesh + 1]
            : this.positions.length / g3d_1.G3d.POSITION_SIZE;
    }
    getSubmeshVertexCount(submesh) {
        return this.getSubmeshVertexEnd(submesh) - this.getSubmeshVertexStart(submesh);
    }
}
exports.G3dMesh = G3dMesh;
G3dMesh.MATRIX_SIZE = 16;
G3dMesh.COLOR_SIZE = 4;
G3dMesh.POSITION_SIZE = 3;
