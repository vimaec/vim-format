/**
 * @module vim-ts
 */

import { BFast } from '../bfast'
import { G3d, MeshSection } from './g3d'

export type FilterMode = undefined | 'mesh' | 'instance' | 'group' | 'tag'

/**
 * See https://github.com/vimaec/vim#vim-geometry-attributes
 */
export class SceneAttributes {

  static readonly chunkCount = 'g3d:chunk:count:0:int32:1'
  static readonly  instanceMesh = 'g3d:instance:mesh:0:int32:1'
  static readonly instanceMatrix = 'g3d:instance:transform:0:float32:16'
  static readonly instanceNodes = 'g3d:instance:node:0:int32:1'
  static readonly instanceGroups = 'g3d:instance:group:0:int32:1'
  static readonly instanceTags = 'g3d:instance:tag:0:int64:1'
  static readonly instanceFlags = 'g3d:instance:tag:0:uint16:1'
  static readonly instanceMins = 'g3d:instance:min:0:float32:3'
  static readonly instanceMaxs = 'g3d:instance:max:0:float32:3'


  static readonly meshChunk = 'g3d:mesh:chunk:0:int32:1'
  static readonly meshChunkIndices = 'g3d:mesh:chunkindex:0:int32:1';
  static readonly meshIndexCounts = 'g3d:mesh:indexcount:0:int32:1'
  static readonly meshVertexCounts = 'g3d:mesh:vertexcount:0:int32:1'

  static readonly meshOpaqueIndexCount = "g3d:mesh:opaqueindexcount:0:int32:1"
  static readonly meshOpaqueVertexCount = "g3d:mesh:opaquevertexcount:0:int32:1"
}

/**
 * Represents the index, as in book index, of a collection of G3dMesh.
 * Allows to find and download G3dMesh as needed.
 * Allows to preallocate geometry to render G3dMeshes.
 */
export class G3dScene {
  chunkCount: number
  instanceMeshes: Int32Array
  instanceMatrices: Float32Array
  instanceNodes: Int32Array
  instanceGroups: Int32Array
  instanceTags : BigInt64Array
  instanceFlags : Uint16Array
  instanceMins: Float32Array
  instanceMaxs: Float32Array

  meshChunks : Int32Array
  meshChunkIndices : Int32Array
  meshIndexCounts: Int32Array
  meshVertexCounts: Int32Array

  meshOpaqueIndexCounts: Int32Array
  meshOpaqueVertexCounts: Int32Array

  private nodeToInstance : Map<number, number>

  constructor(
    chunkCount: Int32Array,
    instanceMeshes: Int32Array,
    instanceMatrices: Float32Array,
    instanceNodes: Int32Array,
    instanceGroups: Int32Array,
    instanceTags: BigInt64Array,
    instanceFlags: Uint16Array,
    instanceMins: Float32Array,
    instanceMaxs: Float32Array,

    meshChunks: Int32Array,
    meshChunkIndices : Int32Array,
    meshIndexCounts : Int32Array,
    meshVertexCounts: Int32Array, 
    
    meshOpaqueIndexCounts: Int32Array,
    meshOpaqueVertexCounts: Int32Array,
    ){
    this.chunkCount = chunkCount[0]
    this.instanceMeshes = instanceMeshes
    this.instanceMatrices = instanceMatrices
    this.instanceNodes = instanceNodes
    this.instanceGroups = instanceGroups
    this.instanceTags =  instanceTags
    this.instanceFlags = instanceFlags
    this.instanceMins = instanceMins
    this.instanceMaxs = instanceMaxs

    this.meshChunks = meshChunks
    this.meshChunkIndices = meshChunkIndices
    this.meshIndexCounts = meshIndexCounts
    this.meshVertexCounts = meshVertexCounts
    this.meshOpaqueIndexCounts = meshOpaqueIndexCounts
    this.meshOpaqueVertexCounts = meshOpaqueVertexCounts

    // Reverse instanceNodes
    this.nodeToInstance = new Map<number, number>()
    for(let i = 0; i < this.instanceNodes.length; i ++){
      this.nodeToInstance.set(this.instanceNodes[i], i)
    }
  }
    
  static async createFromBfast (bfast: BFast) {

    const values = await Promise.all([
      bfast.getInt32Array(SceneAttributes.chunkCount),
      bfast.getInt32Array(SceneAttributes.instanceMesh),
      bfast.getFloat32Array(SceneAttributes.instanceMatrix),
      bfast.getInt32Array(SceneAttributes.instanceNodes),
      bfast.getInt32Array(SceneAttributes.instanceGroups),
      bfast.getBigInt64Array(SceneAttributes.instanceTags),
      bfast.getUint16Array(SceneAttributes.instanceFlags),
      bfast.getFloat32Array(SceneAttributes.instanceMins),
      bfast.getFloat32Array(SceneAttributes.instanceMaxs),

      bfast.getInt32Array(SceneAttributes.meshChunk),
      bfast.getInt32Array(SceneAttributes.meshChunkIndices),
      bfast.getInt32Array(SceneAttributes.meshIndexCounts),
      bfast.getInt32Array(SceneAttributes.meshVertexCounts),
      bfast.getInt32Array(SceneAttributes.meshOpaqueIndexCount),
      bfast.getInt32Array(SceneAttributes.meshOpaqueVertexCount),
    ])
    return new G3dScene(...values)
  }

  getMeshCount() {
    return this.meshChunks.length
  }

  getMeshIndexCount(mesh: number, section: MeshSection){
    const all = this.meshIndexCounts[mesh]
    if(section === 'all') return all;
    const opaque = this.meshOpaqueIndexCounts[mesh]
    return section === 'opaque' ? opaque : all - opaque
  }

  getMeshVertexCount(mesh:number, section: MeshSection){
    const all = this.meshVertexCounts[mesh]
    if(section === 'all') return all;
    const opaque = this.meshOpaqueVertexCounts[mesh]
      return section === 'opaque' ? opaque : all - opaque
  }

  getInstanceMin(instance: number){
    return this.instanceMins.subarray(instance * G3d.POSITION_SIZE)
  }

  getInstanceMax(instance: number){
    return this.instanceMaxs.subarray(instance * G3d.POSITION_SIZE)
  }

  getInstanceMatrix(instance: number){
    return this.instanceMatrices.subarray(instance * G3d.MATRIX_SIZE, (instance + 1)* G3d.MATRIX_SIZE)
  }

}
