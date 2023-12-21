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
MeshAttributes.meshSubmeshOffset = 'g3d:mesh:submeshOffset:0:int32:1';
MeshAttributes.meshOpaqueSubmeshCount = 'g3d:mesh:opaquesubmeshcount:0:int32:1';
MeshAttributes.submeshIndexOffsets = 'g3d:submesh:indexoffset:0:int32:1';
MeshAttributes.submeshVertexOffsets = 'g3d:submesh:vertexoffset:0:int32:1';
MeshAttributes.submeshMaterials = 'g3d:submesh:material:0:int32:1';
MeshAttributes.positions = 'g3d:vertex:position:0:float32:3';
MeshAttributes.indices = 'g3d:corner:index:0:int32:1';
class G3dMesh {
    constructor(scene, chunk, index) {
        this.scene = scene;
        this.chunk = chunk;
        this.index = index;
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
    getHasTransparency(mesh) {
        return this.getSubmeshCount('transparent') > 0;
    }
    // ------------- Submeshes -----------------
    getSubmeshStart(section) {
        if (section === 'all' || section === 'opaque') {
            return this.chunk.meshSubmeshOffset[this.index];
        }
        return this.chunk.meshSubmeshOffset[this.index] + this.chunk.meshOpaqueSubmeshCount[this.index];
    }
    getSubmeshEnd(section) {
        if (section === 'opaque') {
            return this.chunk.meshSubmeshOffset[this.index] + this.chunk.meshOpaqueSubmeshCount[this.index];
        }
        if (this.index + 1 < this.chunk.meshSubmeshOffset.length) {
            return this.chunk.meshSubmeshOffset[this.index + 1];
        }
        return this.chunk.submeshIndexOffset.length;
    }
    getSubmeshCount(section) {
        return this.getSubmeshEnd(section) - this.getSubmeshStart(section);
    }
    getSubmeshIndexStart(submesh) {
        return submesh < this.chunk.submeshIndexOffset.length
            ? this.chunk.submeshIndexOffset[submesh]
            : this.chunk.indices.length;
    }
    getSubmeshIndexEnd(submesh) {
        return submesh < this.chunk.submeshIndexOffset.length - 1
            ? this.chunk.submeshIndexOffset[submesh + 1]
            : this.chunk.indices.length;
    }
    getSubmeshIndexCount(submesh) {
        return this.getSubmeshIndexEnd(submesh) - this.getSubmeshIndexStart(submesh);
    }
    getSubmeshVertexStart(submesh) {
        return submesh < this.chunk.submeshIndexOffset.length
            ? this.chunk.submeshVertexOffset[submesh]
            : this.chunk.positions.length / g3d_1.G3d.POSITION_SIZE;
    }
    getSubmeshVertexEnd(submesh) {
        return submesh < this.chunk.submeshVertexOffset.length - 1
            ? this.chunk.submeshVertexOffset[submesh + 1]
            : this.chunk.positions.length / g3d_1.G3d.POSITION_SIZE;
    }
    getSubmeshVertexCount(submesh) {
        return this.getSubmeshVertexEnd(submesh) - this.getSubmeshVertexStart(submesh);
    }
}
exports.G3dMesh = G3dMesh;
