/**
 * @module vim-ts
 */

import { AbstractG3d } from './abstractG3d'
import { BFast } from './bfast'
import { MeshSection } from './g3d'
import { G3dSubset } from './g3dSubset'


export type FilterMode = undefined | 'mesh' | 'instance' | 'group' | 'tag'

/**
 * See https://github.com/vimaec/vim#vim-geometry-attributes
 */
export class MeshIndexAttributes {

  static instanceFiles = 'g3d:instance:file:0:int32:1'
  static instanceIndices = 'g3d:instance:index:0:int32:1'
  static instanceNodes = 'g3d:instance:node:0:int32:1'
  static instanceGroups = 'g3d:instance:group:0:int32:1'
  static instanceTags = 'g3d:instance:tag:0:int64:1'

  static meshInstanceCounts = 'g3d:mesh:instancecount:0:int32:1'
  static meshIndexCounts = 'g3d:mesh:indexcount:0:int32:1'
  static meshVertexCounts = 'g3d:mesh:vertexcount:0:int32:1'

  static meshOpaqueIndexCount = "g3d:mesh:opaqueindexcount:0:int32:1"
  static meshOpaqueVertexCount = "g3d:mesh:opaquevertexcount:0:int32:1"

  static all = [
    MeshIndexAttributes.instanceFiles,
    MeshIndexAttributes.instanceIndices,
    MeshIndexAttributes.instanceNodes,
    MeshIndexAttributes.instanceGroups,
    MeshIndexAttributes.instanceTags,

    MeshIndexAttributes.meshInstanceCounts,
    MeshIndexAttributes.meshIndexCounts,
    MeshIndexAttributes.meshVertexCounts,

    MeshIndexAttributes.meshOpaqueIndexCount,
    MeshIndexAttributes.meshOpaqueVertexCount,
  ]
}

/**
 * Represents the index, as in book index, of a collection of G3dMesh.
 * Allows to find and download G3dMesh as needed.
 * Allows to preallocate geometry to render G3dMeshes.
 */
export class G3dMeshIndex {
  rawG3d: AbstractG3d

  instanceFiles: Int32Array
  instanceIndices: Int32Array
  instanceNodes: Int32Array
  instanceGroups: Int32Array
  instanceTags : BigInt64Array

  meshInstanceCounts: Int32Array
  meshIndexCounts: Int32Array
  meshVertexCounts: Int32Array

  meshOpaqueIndexCounts: Int32Array
  meshOpaqueVertexCounts: Int32Array

  constructor(
    rawG3d: AbstractG3d,

    instanceFiles: Int32Array,
    instanceIndices: Int32Array,
    instanceNodes: Int32Array,
    instanceGroups: Int32Array,
    instanceTags: BigInt64Array,

    meshInstanceCounts: Int32Array,
    meshIndexCounts : Int32Array,
    meshVertexCounts: Int32Array, 
    
    meshOpaqueIndexCounts: Int32Array,
    meshOpaqueVertexCounts: Int32Array,
    ){

    this.rawG3d = rawG3d

    this.instanceFiles = instanceFiles
    this.instanceIndices = instanceIndices
    this.instanceNodes = instanceNodes
    this.instanceGroups = instanceGroups
    this.instanceTags =  instanceTags

    this.meshInstanceCounts = meshInstanceCounts

    this.meshIndexCounts = meshIndexCounts
    this.meshVertexCounts = meshVertexCounts

    this.meshOpaqueIndexCounts = meshOpaqueIndexCounts
    this.meshOpaqueVertexCounts = meshOpaqueVertexCounts
  }

  static createFromAbstract(g3d: AbstractG3d) {

    function getArray<T>(attribute: string){
      return g3d.findAttribute(
        attribute
        )?.data as T
    }

    return new G3dMeshIndex(
      g3d,
      getArray<Int32Array>(MeshIndexAttributes.instanceFiles),
      getArray<Int32Array>(MeshIndexAttributes.instanceIndices),
      getArray<Int32Array>(MeshIndexAttributes.instanceNodes),
      getArray<Int32Array>(MeshIndexAttributes.instanceGroups),
      getArray<BigInt64Array>(MeshIndexAttributes.instanceTags),

      getArray<Int32Array>(MeshIndexAttributes.meshInstanceCounts),

      getArray<Int32Array>(MeshIndexAttributes.meshIndexCounts),
      getArray<Int32Array>(MeshIndexAttributes.meshVertexCounts),

      getArray<Int32Array>(MeshIndexAttributes.meshOpaqueIndexCount),
      getArray<Int32Array>(MeshIndexAttributes.meshOpaqueVertexCount),
    )
  }

  static async createFromPath (path: string) {
    const f = await fetch(path)
    const buffer = await f.arrayBuffer()
    const bfast = new BFast(buffer)
    return this.createFromBfast(bfast)
  }

  static async createFromBfast (bfast: BFast) {
    const g3d = await AbstractG3d.createFromBfast(bfast, MeshIndexAttributes.all)
    return G3dMeshIndex.createFromAbstract(g3d)
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

  filter(mode: FilterMode, filter: number[]){
    if(filter === undefined || mode === undefined){
      return new G3dSubset(this, undefined)
    } 
    if(mode === 'instance'){
      return this.getMeshesFromInstances(filter)
    }

    if(mode === 'mesh'){
      return new G3dSubset(this, filter)
    }
    if(mode === 'tag' || mode === 'group'){
      throw new Error("Filter Mode Not implemented")
    }
  }

  private getMeshesFromInstances(instances: number[]){
    const set = new Set(instances)
    const meshes = new Array<number>()
    const map = new Map<number, number[]>()
    for(let i=0; i < this.instanceNodes.length; i ++){
      const node = this.instanceNodes[i]
      if(set.has(node)){
        const mesh = this.instanceFiles[i]
        const index = this.instanceIndices[i]

        if(!map.has(mesh)){
          meshes.push(mesh)
          map.set(mesh, [index])
        }
        else{
          map.get(mesh).push(index)
        }
      }
    }
    const meshInstances = new Array<Array<number>>(meshes.length)
    meshes.forEach((m, i) => meshInstances[i] = map.get(m))
    return new G3dSubset(this, meshes, meshInstances)
  }
}
