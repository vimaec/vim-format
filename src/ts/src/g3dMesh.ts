/**
 * @module vim-ts
 */

import { BFast } from './bfast'
import { G3d, MeshSection } from './g3d'
import { G3dScene } from './g3dScene'

/**
 * See https://github.com/vimaec/vim#vim-geometry-attributes
 */
export class MeshAttributes {

  static meshOpaqueSubmeshCount = 'g3d:mesh:opaquesubmeshcount:0:int32:1'
  static submeshIndexOffsets = 'g3d:submesh:indexoffset:0:int32:1'
  static submeshVertexOffsets = 'g3d:submesh:vertexoffset:0:int32:1'
  static submeshMaterials = 'g3d:submesh:material:0:int32:1'
  static positions = 'g3d:vertex:position:0:float32:3'
  static indices = 'g3d:corner:index:0:int32:1'

  static all = [
    MeshAttributes.meshOpaqueSubmeshCount,
    MeshAttributes.submeshIndexOffsets,
    MeshAttributes.submeshVertexOffsets,
    MeshAttributes.submeshMaterials,
    MeshAttributes.positions,
    MeshAttributes.indices,
  ]
}

/**
 * G3D is a simple, efficient, generic binary format for storing and transmitting geometry.
 * The G3D format is designed to be used either as a serialization format or as an in-memory data structure.
 * A G3d with specific attributes according to the VIM format specification.
 * See https://github.com/vimaec/vim#vim-geometry-attributes for the vim specification.
 * See https://github.com/vimaec/g3d for the g3d specification.
 */
export class G3dMesh {

  scene: G3dScene
  meshIndex: number 
  readonly meshOpaqueSubmeshCount : number
  readonly submeshIndexOffset: Int32Array
  readonly submeshVertexOffset: Int32Array
  readonly submeshMaterial: Int32Array

  readonly positions: Float32Array
  readonly indices: Uint32Array

  static readonly COLOR_SIZE = 4
  static readonly POSITION_SIZE = 3
  /**
   * Opaque white
   */
  static readonly DEFAULT_COLOR = new Float32Array([1, 1, 1, 1])

  constructor(
    meshOpaqueSubmeshCount : number,
    submeshIndexOffsets : Int32Array,
    submeshVertexOffsets : Int32Array,
    submeshMaterials : Int32Array,
    indices: Int32Array | Uint32Array,
    positions: Float32Array,
    ){

    this.meshOpaqueSubmeshCount = meshOpaqueSubmeshCount
    this.submeshIndexOffset = submeshIndexOffsets
    this.submeshVertexOffset = submeshVertexOffsets
    this.submeshMaterial = submeshMaterials
    this.indices = indices instanceof Uint32Array ? indices : new Uint32Array(indices.buffer)
    this.positions = positions
  }

  static async createFromBfast (bfast: BFast) {
    const values = await Promise.all([
      bfast.getValue(MeshAttributes.meshOpaqueSubmeshCount, 0).then(v => v as number),
      bfast.getInt32Array(MeshAttributes.submeshIndexOffsets),
      bfast.getInt32Array(MeshAttributes.submeshVertexOffsets),
      bfast.getInt32Array(MeshAttributes.submeshMaterials),
      bfast.getInt32Array(MeshAttributes.indices),
      bfast.getFloat32Array(MeshAttributes.positions)
    ])

    return new G3dMesh(...values)
  }

  // ------------- Mesh -----------------

  getVertexStart(section: MeshSection = 'all'){
    const sub = this.getSubmeshStart(section)
    return this.getSubmeshVertexStart(sub)
  }

  getVertexEnd(section: MeshSection = 'all'){
    const sub = this.getSubmeshEnd(section)
    return this.getSubmeshVertexStart(sub)
  }

  getVertexCount(section: MeshSection = 'all'){
    return this.getVertexEnd(section) - this.getVertexStart(section)
  }

  getIndexStart(section: MeshSection = 'all'){
    const sub = this.getSubmeshStart(section)
    return this.getSubmeshIndexStart(sub)
  }

  getIndexEnd(section: MeshSection = 'all'){
    const sub = this.getSubmeshEnd(section)
    return this.getSubmeshIndexStart(sub)
  }

  getIndexCount (section: MeshSection = 'all'): number {
    return this.getIndexEnd(section) - this.getIndexStart(section)
  }

  getHasTransparency () {
    return this.meshOpaqueSubmeshCount < this.submeshIndexOffset.length}

  // ------------- Submeshes -----------------

  getSubmeshStart(section: MeshSection){
    if(section === 'all') return 0
    if(section === 'opaque') return 0
    return this.meshOpaqueSubmeshCount
  }

  getSubmeshEnd(section: MeshSection){
    if(section === 'all') return this.submeshIndexOffset.length
    if(section === 'transparent') return this.submeshIndexOffset.length
    return this.meshOpaqueSubmeshCount
  }

  getSubmeshCount(section: MeshSection){
    return this.getSubmeshEnd(section) - this.getSubmeshStart(section)
  }

  getSubmeshIndexStart (submesh: number): number {
    return submesh < this.submeshIndexOffset.length
      ? this.submeshIndexOffset[submesh]
      : this.indices.length
  }

  getSubmeshIndexEnd (submesh: number): number {
    return submesh < this.submeshIndexOffset.length - 1
      ? this.submeshIndexOffset[submesh + 1]
      : this.indices.length
  }

  getSubmeshIndexCount (submesh: number): number {
    return this.getSubmeshIndexEnd(submesh) - this.getSubmeshIndexStart(submesh)
  }

  getSubmeshVertexStart(submesh: number){
    return submesh < this.submeshIndexOffset.length
      ? this.submeshVertexOffset[submesh]
      : this.positions.length / G3d.POSITION_SIZE
  }

  getSubmeshVertexEnd (submesh: number): number {
    return submesh < this.submeshVertexOffset.length - 1
      ? this.submeshVertexOffset[submesh + 1]
      : this.positions.length / G3d.POSITION_SIZE
  }

  getSubmeshVertexCount (submesh: number): number {
    return this.getSubmeshVertexEnd(submesh) - this.getSubmeshVertexStart(submesh)
  }

  // ------------- Instances -----------------

}

