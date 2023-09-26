/**
 * @module vim-ts
 */

import { AbstractG3d } from './abstractG3d'
import { BFast } from './bfast'
import { G3d, MeshSection } from './g3d'
import { G3dMaterial } from './g3dMaterials'
import { G3dScene } from './g3dScene'

/**
 * See https://github.com/vimaec/vim#vim-geometry-attributes
 */
export class MeshAttributes {

  static instanceTransforms = 'g3d:instance:transform:0:float32:16'
  static meshOpaqueSubmeshCount = 'g3d:mesh:opaquesubmeshcount:0:int32:1'
  static submeshIndexOffsets = 'g3d:submesh:indexoffset:0:int32:1'
  static submeshVertexOffsets = 'g3d:submesh:vertexoffset:0:int32:1'
  static submeshMaterials = 'g3d:submesh:material:0:int32:1'
  static positions = 'g3d:vertex:position:0:float32:3'
  static indices = 'g3d:corner:index:0:int32:1'

  static all = [
    MeshAttributes.instanceTransforms,
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

  rawG3d: AbstractG3d

  instanceTransforms: Float32Array

  meshOpaqueSubmeshCount : number

  submeshIndexOffset: Int32Array
  submeshVertexOffset: Int32Array
  submeshMaterial: Int32Array

  positions: Float32Array
  indices: Uint32Array

  static MATRIX_SIZE = 16
  static COLOR_SIZE = 4
  static POSITION_SIZE = 3
  /**
   * Opaque white
   */
  DEFAULT_COLOR = new Float32Array([1, 1, 1, 1])

  constructor(
    instanceTransforms: Float32Array,
    meshOpaqueSubmeshCount : number,
    submeshIndexOffsets : Int32Array,
    submeshVertexOffsets : Int32Array,
    submeshMaterials : Int32Array,
    indices: Int32Array | Uint32Array,
    positions: Float32Array,
    ){

    this.instanceTransforms = instanceTransforms
    this.meshOpaqueSubmeshCount = meshOpaqueSubmeshCount
    this.submeshIndexOffset = submeshIndexOffsets
    this.submeshVertexOffset = submeshVertexOffsets
    this.submeshMaterial = submeshMaterials
    this.indices = indices instanceof Uint32Array ? indices : new Uint32Array(indices.buffer)
    this.positions = positions
  }

  static createFromAbstract(g3d: AbstractG3d) {

    const instanceTransforms = g3d.findAttribute(
      MeshAttributes.instanceTransforms
    )?.data as Float32Array

    const meshOpaqueSubmeshCountArray = g3d.findAttribute(
        MeshAttributes.meshOpaqueSubmeshCount
      )?.data as Int32Array
    const meshOpaqueSubmeshCount = meshOpaqueSubmeshCountArray[0]

    const submeshIndexOffsets = g3d.findAttribute(
      MeshAttributes.submeshIndexOffsets
    )?.data as Int32Array

    const submeshVertexOffsets = g3d.findAttribute(
      MeshAttributes.submeshVertexOffsets
    )?.data as Int32Array
  
    const submeshMaterial = g3d.findAttribute(MeshAttributes.submeshMaterials)
      ?.data as Int32Array

    const indices = g3d.findAttribute(MeshAttributes.indices)?.data as Int32Array

    const positions = g3d.findAttribute(MeshAttributes.positions)
      ?.data as Float32Array

    const result = new G3dMesh(
      instanceTransforms,
      meshOpaqueSubmeshCount,
      submeshIndexOffsets,
      submeshVertexOffsets,
      submeshMaterial,
      indices,
      positions,
    )
    result.rawG3d = g3d

    return result
  }

  static async createFromPath (path: string) {
    const f = await fetch(path)
    const buffer = await f.arrayBuffer()
    var g3d = this.createFromBuffer(buffer)

    return g3d
  }

  static async createFromBuffer (buffer: ArrayBuffer) {
    const bfast = new BFast(buffer)
    return this.createFromBfast(bfast)
  }

  static async createFromBfast (bfast: BFast) {
    const g3d = await AbstractG3d.createFromBfast(bfast, MeshAttributes.all)
    return G3dMesh.createFromAbstract(g3d)
  }

  // -----------Instances---------------

  getBimInstance(meshInstance: number){
    const sceneInstance = this.scene.getMeshSceneInstance(this.meshIndex, meshInstance)
    return this.scene.instanceNodes[sceneInstance]
  }

  getInstanceMax(meshInstance: number){
    const sceneInstance = this.scene.getMeshSceneInstance(this.meshIndex, meshInstance)
    return this.scene.instanceMaxs.subarray(sceneInstance * 3, (sceneInstance +1) * 3)
  }

  getInstanceMin(meshInstance: number){
    const sceneInstance = this.scene.getMeshSceneInstance(this.meshIndex, meshInstance)
    return this.scene.instanceMins.subarray(sceneInstance * 3, (sceneInstance +1) * 3)
  }

  getInstanceGroup(meshInstance: number){
    const sceneInstance = this.scene.getMeshSceneInstance(this.meshIndex, meshInstance)
    return this.scene.instanceGroups[sceneInstance]
  }

  getInstanceTag(meshInstance: number){
    const sceneInstance = this.scene.getMeshSceneInstance(this.meshIndex, meshInstance)
    return this.scene.instanceTags[sceneInstance]
  }

  getInstanceHasFlag(meshInstance: number, flag: number){
    const sceneInstance = this.scene.getMeshSceneInstance(this.meshIndex, meshInstance)
    return (this.scene.instanceFlags[sceneInstance] & flag) > 0
  }

  getInstanceCount = () => this.instanceTransforms.length

  /**
   * Returns an 16 number array representation of the matrix for given instance
   * @param instance g3d instance index
   */
  getInstanceMatrix (instance: number): Float32Array {
    return this.instanceTransforms.subarray(
      instance * G3dMesh.MATRIX_SIZE,
      (instance + 1) * G3dMesh.MATRIX_SIZE
    )
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

