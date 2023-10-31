import { BFast } from "./bfast";
import { G3dMaterial as G3dMaterials } from "./g3dMaterials";
import { G3dMesh } from "./g3dMesh";
import { G3dScene } from "./g3dScene";
import { RemoteValue } from "./remoteValue";
import { requestHeader } from "./vimHeader";

export class RemoteVimx{
  bfast : BFast
  scene : RemoteValue<G3dScene>
  chunkCache = new Map<number, RemoteValue<G3dMesh[]>>()

  constructor(source : BFast | ArrayBuffer | string){
    this.bfast = source instanceof BFast ? source : new BFast(source)
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
    var value = new RemoteValue<G3dMesh[]>(() => this.requestChunk(chunk))
    this.chunkCache.set(chunk, value)
    return value.get()
  }

  private async requestChunk(chunk : number){
    const chunkBFast = await this.bfast.getLocalBfast(`chunk_${chunk}`, true)
    var ranges = await chunkBFast.getRanges()
    const keys = [...ranges.keys()]
    var bfasts = await Promise.all(keys.map(k => chunkBFast.getBfast(k)))
    var meshes = await Promise.all(bfasts.map(b => G3dMesh.createFromBfast(b)))
    const scene = await this.scene.get()
    meshes.forEach(m => m.scene = scene)
    return meshes
  }

  async getMesh(mesh: number){
    var scene = await this.scene.get()
    var chunk = scene.meshChunks[mesh]
    if(chunk === undefined) return undefined

    var meshes = await this.getChunk(chunk)
    if(meshes === undefined) return undefined
    
    var index = scene.meshChunkIndices[mesh]
    var result = meshes[index]
    if(result === undefined) return undefined

    result.meshIndex = mesh
    return result
  }
}