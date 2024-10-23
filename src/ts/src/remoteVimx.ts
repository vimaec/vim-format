import { BFast, BFastSource } from "./bfast";
import { G3dMaterial as G3dMaterials } from "./g3d/g3dMaterials";
import { G3dChunk } from './g3d/g3dChunk';
import { G3dScene } from "./g3d/g3dScene";
import { RemoteValue } from "./http/remoteValue";
import { requestHeader } from "./vimHeader";
import { G3dMesh } from "./g3d/g3dMesh";

export class RemoteVimx{
  bfast : BFast
  scene : RemoteValue<G3dScene>
  chunkCache = new Map<number, RemoteValue<G3dChunk>>()

  constructor(bfast : BFast){
    this.bfast = bfast
    this.scene = new RemoteValue(() => this.requestScene())
  }

  /**
   * Aborts all downloads from the underlying BFAST.
   */
  abort(){
    this.bfast.abort()
    this.scene.abort()
    this.chunkCache.forEach(c => c.abort())
  }

  /**
   * Downloads underlying bfast making all subsequent request local.
   */
  async download(){
    this.bfast.forceDownload()
  }

  private async requestScene(){
    const index = await this.bfast.getLocalBfast('scene', true)
    return G3dScene.createFromBfast(index)
  }

  async getHeader(){
    return requestHeader(this.bfast)
  }

    /**
   * Fetches and returns the vimx G3dScene 
   */
  async getScene(){
    return this.scene.get()
  }

  /**
   * Fetches and returns the vimx G3dMaterials 
   */
  async getMaterials(){
    const mat = await this.bfast.getLocalBfast('materials', true)
    return G3dMaterials.createFromBfast(mat)
  }

  /**
   * Fetches and returns the vimx G3dMesh with given index 
   */
  async getChunk(chunk: number){
    var cached = this.chunkCache.get(chunk)
    if(cached !== undefined){
      return cached.get()
    }
    var value = new RemoteValue<G3dChunk>(() => this.requestChunk(chunk))
    this.chunkCache.set(chunk, value)
    return value.get()
  }

  private async requestChunk(chunk : number){
    const chunkBFast = await this.bfast.getLocalBfast(`chunk_${chunk}`, true)
    return G3dChunk.createFromBfast(chunkBFast)
  }

  async getMesh(mesh: number){
    var scene = await this.scene.get()
    var meshChunk = scene.meshChunks[mesh]
    if(meshChunk === undefined) return undefined

    var chunk = await this.getChunk(meshChunk)
    if(chunk === undefined) return undefined

    return new G3dMesh(scene, chunk, scene.meshChunkIndices[mesh])
  }
}