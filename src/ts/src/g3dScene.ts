/**
 * @module vim-ts
 */

import { AbstractG3d } from './abstractG3d'
import { BFast } from './bfast'
import { G3d, MeshSection } from './g3d'

export type FilterMode = undefined | 'mesh' | 'instance' | 'group' | 'tag'

/**
 * See https://github.com/vimaec/vim#vim-geometry-attributes
 */
export class SceneAttributes {

  static chunkCount = 'g3d:chunk:count:0:int32:1'
  static instanceMesh = 'g3d:instance:mesh:0:int32:1'
  static instanceTransform = 'g3d:instance:transform:0:int32:1'
  static instanceNodes = 'g3d:instance:node:0:int32:1'
  static instanceGroups = 'g3d:instance:group:0:int32:1'
  static instanceTags = 'g3d:instance:tag:0:int64:1'
  static instanceFlags = 'g3d:instance:tag:0:uint16:1'
  static instanceMins = 'g3d:instance:min:0:float32:3'
  static instanceMaxs = 'g3d:instance:max:0:float32:3'


  static meshChunk = 'g3d:mesh:chunk:0:int32:1'
  static meshInstanceCounts = 'g3d:mesh:instancecount:0:int32:1'
  static meshIndexCounts = 'g3d:mesh:indexcount:0:int32:1'
  static meshVertexCounts = 'g3d:mesh:vertexcount:0:int32:1'

  static meshOpaqueIndexCount = "g3d:mesh:opaqueindexcount:0:int32:1"
  static meshOpaqueVertexCount = "g3d:mesh:opaquevertexcount:0:int32:1"

  static all = [
    SceneAttributes.chunkCount,
    SceneAttributes.instanceMesh,
    SceneAttributes.instanceTransform,
    SceneAttributes.instanceNodes,
    SceneAttributes.instanceGroups,
    SceneAttributes.instanceTags,
    SceneAttributes.instanceFlags,
    SceneAttributes.instanceMins,
    SceneAttributes.instanceMaxs,

    SceneAttributes.meshChunk,
    SceneAttributes.meshInstanceCounts,
    SceneAttributes.meshIndexCounts,
    SceneAttributes.meshVertexCounts,

    SceneAttributes.meshOpaqueIndexCount,
    SceneAttributes.meshOpaqueVertexCount,
  ]
}

/**
 * Represents the index, as in book index, of a collection of G3dMesh.
 * Allows to find and download G3dMesh as needed.
 * Allows to preallocate geometry to render G3dMeshes.
 */
export class G3dScene {
  rawG3d: AbstractG3d

  chunkCount: number
  instanceMeshes: Int32Array
  instanceTransforms: Int32Array
  instanceNodes: Int32Array
  instanceGroups: Int32Array
  instanceTags : BigInt64Array
  instanceFlags : Uint16Array
  instanceMins: Float32Array
  instanceMaxs: Float32Array

  meshChunks : Int32Array
  meshInstanceCounts: Int32Array
  meshIndexCounts: Int32Array
  meshVertexCounts: Int32Array

  meshOpaqueIndexCounts: Int32Array
  meshOpaqueVertexCounts: Int32Array

  private nodeToInstance : Map<number, number>
  private meshSceneInstances: Map<number, Map<number,number>>

  constructor(
    rawG3d: AbstractG3d,

    chunkCount: Int32Array,
    instanceMeshes: Int32Array,
    instanceTransform: Int32Array,
    instanceNodes: Int32Array,
    instanceGroups: Int32Array,
    instanceTags: BigInt64Array,
    instanceFlags: Uint16Array,
    instanceMins: Float32Array,
    instanceMaxs: Float32Array,

    meshChunks: Int32Array,
    meshInstanceCounts: Int32Array,
    meshIndexCounts : Int32Array,
    meshVertexCounts: Int32Array, 
    
    meshOpaqueIndexCounts: Int32Array,
    meshOpaqueVertexCounts: Int32Array,
    ){

    this.rawG3d = rawG3d

    this.chunkCount = chunkCount[0]
    this.instanceMeshes = instanceMeshes
    this.instanceTransforms = instanceTransform
    this.instanceNodes = instanceNodes
    this.instanceGroups = instanceGroups
    this.instanceTags =  instanceTags
    this.instanceFlags = instanceFlags
    this.instanceMins = instanceMins
    this.instanceMaxs = instanceMaxs

    this.meshChunks = meshChunks
    this.meshInstanceCounts = meshInstanceCounts
    this.meshIndexCounts = meshIndexCounts
    this.meshVertexCounts = meshVertexCounts
    this.meshOpaqueIndexCounts = meshOpaqueIndexCounts
    this.meshOpaqueVertexCounts = meshOpaqueVertexCounts

    // Reverse instanceNodes
    this.nodeToInstance = new Map<number, number>()
    for(let i = 0; i < this.instanceNodes.length; i ++){
      this.nodeToInstance.set(this.instanceNodes[i], i)
    }
    this.meshSceneInstances = this.createMap()
  }

  private createMap(){
    // From : (mesh, scene-index) -> mesh-index 
    // To: (mesh, mesh-index) -> scene-index
    const map = new Map<number,Map<number, number>>()
    for(let i =0; i < this.instanceMeshes.length; i++){
      const mesh = this.instanceMeshes[i]
      const index = this.instanceTransforms[i]
      const indices = map.get(mesh) ?? new Map<number, number>()
      indices.set(index, i)
      map.set(mesh, indices)
    }
    return map
  }
    
  static createFromAbstract(g3d: AbstractG3d) {

    function getArray<T>(attribute: string){
      return g3d.findAttribute(
        attribute
        )?.data as T
    }
    
    return new G3dScene(
      g3d,
      getArray<Int32Array>(SceneAttributes.chunkCount),
      getArray<Int32Array>(SceneAttributes.instanceMesh),
      getArray<Int32Array>(SceneAttributes.instanceTransform),
      getArray<Int32Array>(SceneAttributes.instanceNodes),
      getArray<Int32Array>(SceneAttributes.instanceGroups),
      getArray<BigInt64Array>(SceneAttributes.instanceTags),
      getArray<Uint16Array>(SceneAttributes.instanceFlags),
      getArray<Float32Array>(SceneAttributes.instanceMins),
      getArray<Float32Array>(SceneAttributes.instanceMaxs),

      getArray<Int32Array>(SceneAttributes.meshChunk),
      getArray<Int32Array>(SceneAttributes.meshInstanceCounts),
      getArray<Int32Array>(SceneAttributes.meshIndexCounts),
      getArray<Int32Array>(SceneAttributes.meshVertexCounts),
      getArray<Int32Array>(SceneAttributes.meshOpaqueIndexCount),
      getArray<Int32Array>(SceneAttributes.meshOpaqueVertexCount),
    )
  }

  static async createFromPath (path: string) {
    const f = await fetch(path)
    const buffer = await f.arrayBuffer()
    const bfast = new BFast(buffer)
    return this.createFromBfast(bfast)
  }

  static async createFromBfast (bfast: BFast) {
    const g3d = await AbstractG3d.createFromBfast(bfast, SceneAttributes.all)
    return G3dScene.createFromAbstract(g3d)
  }

  getMeshSceneInstance(mesh: number, meshIndex: number){
    return this.meshSceneInstances.get(mesh)?.get(meshIndex)
  }

  getMeshCount() {
    return this.meshInstanceCounts.length
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

  getMeshInstanceCount(mesh:number){
    return this.meshInstanceCounts[mesh]
  }

  getNodeMin(node: number){
    const instance = this.nodeToInstance.get(node)
    if(!instance){
      return undefined
    }
    return this.getInstanceMin(instance)
  }

  getNodeMax(node: number){
    const instance = this.nodeToInstance.get(node)
    if(!instance){
      return undefined
    }
    return this.getInstanceMax(instance)
  }

  getInstanceMin(instance: number){
    return this.instanceMins.subarray(instance * G3d.POSITION_SIZE)
  }

  getInstanceMax(instance: number){
    return this.instanceMaxs.subarray(instance * G3d.POSITION_SIZE)
  }

}
