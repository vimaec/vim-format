import { BFast } from "./bfast";
import { G3dMaterial as G3dMaterials } from "./g3dMaterials";
import { G3dMesh } from "./g3dMesh";
import { G3dScene } from "./g3dScene";
import { RemoteBuffer } from "./remoteBuffer";
import { RemoteValue } from "./remoteValue";

export class RemoteVimx{
  bfast : BFast
  scene : RemoteValue<G3dScene>

  constructor(bfast : BFast){
    this.bfast = bfast
    this.scene = new RemoteValue(() => this.requestScene())
  }

  static async fromPath(path: string){
    const buffer = new RemoteBuffer(path)
    const bfast = new BFast(buffer)
    return new RemoteVimx(bfast)
  }

  /**
   * Aborts all downloads from the underlying BFAST.
   */
  abort(){
    this.bfast.abort()
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
    const chunkBFast = await this.bfast.getLocalBfast(`chunk_${chunk}`, true)
    var ranges = await chunkBFast.getRanges()
    const keys = [...ranges.keys()]
    var bfasts = await Promise.all(keys.map(k => chunkBFast.getBfast(k)))
    var meshes = await Promise.all(bfasts.map(b => G3dMesh.createFromBfast(b)))
    return meshes
  }
}