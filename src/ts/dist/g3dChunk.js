"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.G3dChunk = exports.MeshAttributes = void 0;
/**
 * G3D is a simple, efficient, generic binary format for storing and transmitting geometry.
 * The G3D format is designed to be used either as a serialization format or as an in-memory data structure.
 * A G3d with specific attributes according to the VIM format specification.
 * See https://github.com/vimaec/vim#vim-geometry-attributes for the vim specification.
 * See https://github.com/vimaec/g3d for the g3d specification.
 */
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
class G3dChunk {
    constructor(meshSubmeshOffset, meshOpaqueSubmeshCount, submeshIndexOffsets, submeshVertexOffsets, submeshMaterials, indices, positions) {
        this.meshSubmeshOffset = meshSubmeshOffset;
        this.meshOpaqueSubmeshCount = meshOpaqueSubmeshCount;
        this.submeshIndexOffset = submeshIndexOffsets;
        this.submeshVertexOffset = submeshVertexOffsets;
        this.submeshMaterial = submeshMaterials;
        this.indices = indices instanceof Uint32Array ? indices : new Uint32Array(indices.buffer);
        this.positions = positions;
    }
    static async createFromBfast(bfast) {
        const values = await Promise.all([
            bfast.getInt32Array(MeshAttributes.meshSubmeshOffset),
            bfast.getInt32Array(MeshAttributes.meshOpaqueSubmeshCount),
            bfast.getInt32Array(MeshAttributes.submeshIndexOffsets),
            bfast.getInt32Array(MeshAttributes.submeshVertexOffsets),
            bfast.getInt32Array(MeshAttributes.submeshMaterials),
            bfast.getInt32Array(MeshAttributes.indices),
            bfast.getFloat32Array(MeshAttributes.positions)
        ]);
        return new G3dChunk(...values);
    }
}
exports.G3dChunk = G3dChunk;
G3dChunk.COLOR_SIZE = 4;
G3dChunk.POSITION_SIZE = 3;
/**
 * Opaque white
 */
G3dChunk.DEFAULT_COLOR = new Float32Array([1, 1, 1, 1]);
