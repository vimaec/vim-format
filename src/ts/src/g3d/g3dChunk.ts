import { BFast } from '../bfast';

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
export class MeshAttributes {
  static meshSubmeshOffset = 'g3d:mesh:submeshoffset:0:int32:1'
  static meshOpaqueSubmeshCount = 'g3d:mesh:opaquesubmeshcount:0:int32:1'
  static submeshIndexOffsets = 'g3d:submesh:indexoffset:0:int32:1'
  static submeshVertexOffsets = 'g3d:submesh:vertexoffset:0:int32:1'
  static submeshMaterials = 'g3d:submesh:material:0:int32:1'
  static positions = 'g3d:vertex:position:0:float32:3'
  static indices = 'g3d:corner:index:0:int32:1'
}

export class G3dChunk {
  readonly meshSubmeshOffset: Int32Array;
  readonly meshOpaqueSubmeshCount: Int32Array;
  readonly submeshIndexOffset: Int32Array;
  readonly submeshVertexOffset: Int32Array;
  readonly submeshMaterial: Int32Array;
  readonly positions: Float32Array;
  readonly indices: Uint32Array;

  static readonly COLOR_SIZE = 4;
  static readonly POSITION_SIZE = 3;
  /**
   * Opaque white
   */
  static readonly DEFAULT_COLOR = new Float32Array([1, 1, 1, 1]);

  constructor(
    meshSubmeshOffset: Int32Array,
    meshOpaqueSubmeshCount: Int32Array,
    submeshIndexOffsets: Int32Array,
    submeshVertexOffsets: Int32Array,
    submeshMaterials: Int32Array,
    indices: Int32Array | Uint32Array,
    positions: Float32Array
  ) {
    this.meshSubmeshOffset = meshSubmeshOffset;
    this.meshOpaqueSubmeshCount = meshOpaqueSubmeshCount;
    this.submeshIndexOffset = submeshIndexOffsets;
    this.submeshVertexOffset = submeshVertexOffsets;
    this.submeshMaterial = submeshMaterials;
    this.indices = indices instanceof Uint32Array ? indices : new Uint32Array(indices.buffer);
    this.positions = positions;
  }

  static async createFromBfast(bfast: BFast) {
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
