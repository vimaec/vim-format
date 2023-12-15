"use strict";
/**
 * @module vim-ts
 */
Object.defineProperty(exports, "__esModule", { value: true });
exports.G3dMesh = exports.MeshAttributes = void 0;
const g3d_1 = require("./g3d");
/**
 * See https://github.com/vimaec/vim#vim-geometry-attributes
 */
class MeshAttributes {
}
exports.MeshAttributes = MeshAttributes;
MeshAttributes.meshOpaqueSubmeshCount = 'g3d:mesh:opaquesubmeshcount:0:int32:1';
MeshAttributes.submeshIndexOffsets = 'g3d:submesh:indexoffset:0:int32:1';
MeshAttributes.submeshVertexOffsets = 'g3d:submesh:vertexoffset:0:int32:1';
MeshAttributes.submeshMaterials = 'g3d:submesh:material:0:int32:1';
MeshAttributes.positions = 'g3d:vertex:position:0:float32:3';
MeshAttributes.indices = 'g3d:corner:index:0:int32:1';
MeshAttributes.all = [
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
    constructor(meshOpaqueSubmeshCount, submeshIndexOffsets, submeshVertexOffsets, submeshMaterials, indices, positions) {
        this.meshOpaqueSubmeshCount = meshOpaqueSubmeshCount;
        this.submeshIndexOffset = submeshIndexOffsets;
        this.submeshVertexOffset = submeshVertexOffsets;
        this.submeshMaterial = submeshMaterials;
        this.indices = indices instanceof Uint32Array ? indices : new Uint32Array(indices.buffer);
        this.positions = positions;
    }
    static async createFromBfast(bfast) {
        const values = await Promise.all([
            bfast.getValue(MeshAttributes.meshOpaqueSubmeshCount, 0).then(v => v),
            bfast.getInt32Array(MeshAttributes.submeshIndexOffsets),
            bfast.getInt32Array(MeshAttributes.submeshVertexOffsets),
            bfast.getInt32Array(MeshAttributes.submeshMaterials),
            bfast.getInt32Array(MeshAttributes.indices),
            bfast.getFloat32Array(MeshAttributes.positions)
        ]);
        return new G3dMesh(...values);
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
G3dMesh.COLOR_SIZE = 4;
G3dMesh.POSITION_SIZE = 3;
/**
 * Opaque white
 */
G3dMesh.DEFAULT_COLOR = new Float32Array([1, 1, 1, 1]);
