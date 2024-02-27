/**
 * @module vim-ts
 */

import { G3dChunk } from './g3dChunk'
import { G3d, MeshSection } from './g3d'
import { G3dScene } from './g3dScene'

export class G3dMesh
{
  readonly scene : G3dScene
  readonly chunk: G3dChunk
  readonly index : number

  constructor(scene : G3dScene, chunk: G3dChunk, index : number)
  {
    this.scene = scene
    this.chunk = chunk
    this.index = index
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

  getHasTransparency (mesh: number) {
    return this.getSubmeshCount('transparent') > 0
  }

  // ------------- Submeshes -----------------

  getSubmeshStart(section: MeshSection){
    if(section === 'all' || section === 'opaque'){
      return this.chunk.meshSubmeshOffset[this.index]
    }
    return this.chunk.meshSubmeshOffset[this.index] + this.chunk.meshOpaqueSubmeshCount[this.index] 
  }

  getSubmeshEnd(section: MeshSection){
    if(section === 'opaque'){
      return this.chunk.meshSubmeshOffset[this.index] + this.chunk.meshOpaqueSubmeshCount[this.index]
    }
    if(this.index + 1 < this.chunk.meshSubmeshOffset.length){
      return this.chunk.meshSubmeshOffset[this.index + 1]
    }

    return this.chunk.submeshIndexOffset.length
  }

  getSubmeshCount(section: MeshSection){
    return this.getSubmeshEnd(section) - this.getSubmeshStart(section)
  }

  getSubmeshIndexStart (submesh: number): number {
    return submesh < this.chunk.submeshIndexOffset.length
      ? this.chunk.submeshIndexOffset[submesh]
      : this.chunk.indices.length
  }

  getSubmeshIndexEnd (submesh: number): number {
    return submesh < this.chunk.submeshIndexOffset.length - 1
      ? this.chunk.submeshIndexOffset[submesh + 1]
      : this.chunk.indices.length
  }

  getSubmeshIndexCount (submesh: number): number {
    return this.getSubmeshIndexEnd(submesh) - this.getSubmeshIndexStart(submesh)
  }

  getSubmeshVertexStart(submesh: number){
    return submesh < this.chunk.submeshIndexOffset.length
      ? this.chunk.submeshVertexOffset[submesh]
      : this.chunk.positions.length / G3d.POSITION_SIZE
  }

  getSubmeshVertexEnd (submesh: number): number {
    return submesh < this.chunk.submeshVertexOffset.length - 1
      ? this.chunk.submeshVertexOffset[submesh + 1]
      : this.chunk.positions.length / G3d.POSITION_SIZE
  }

  getSubmeshVertexCount (submesh: number): number {
    return this.getSubmeshVertexEnd(submesh) - this.getSubmeshVertexStart(submesh)
  }
}
