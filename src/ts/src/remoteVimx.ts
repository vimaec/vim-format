import { BFast } from "./bfast";
import { G3dMaterial as G3dMaterials } from "./g3dMaterials";
import { G3dMesh } from "./g3dMesh";
import { G3dScene } from "./g3dScene";
import { RemoteBuffer } from "./remoteBuffer";
import { RemoteValue } from "./remoteValue";

export class RemoteVimx{
  bfast : BFast
  scene : RemoteValue<G3dScene>
  sceneRaw : RemoteValue<G3dScene>

  constructor(bfast : BFast){
    this.bfast = bfast
    this.scene = new RemoteValue(() => this.requestIndex())
    this.sceneRaw = new RemoteValue(() => this.requestIndexRaw())
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

  /**
   * Fetches and returns the vimx G3dMeshIndex
   */
  private async requestIndex(){
    const index = await this.bfast.getLocalBfast('scene', true)
    return G3dScene.createFromBfast(index)
  }

  private async requestIndexRaw(){
    const index = await this.bfast.getLocalBfastRaw('scene', true)
    return G3dScene.createFromBfast(index)
  }

  async getIndex(){
    return this.scene.get()
  }

  async getIndexRaw(){
    return this.sceneRaw.get()
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
  async getMesh(mesh: number){
    const m = await this.bfast.getLocalBfast(`mesh_${mesh}`, true)
    const result = await G3dMesh.createFromBfast(m)
    const scene = await this.scene.get()
    result.scene = scene
    result.meshIndex = mesh
    return result
  }


  /**
   * Fetches and returns the vimx G3dMaterials 
   */
  async getMaterialsRaw(){
    const mat = await this.bfast.getLocalBfastRaw('materials', true)
    return G3dMaterials.createFromBfast(mat)
  }

  /**
   * Fetches and returns the vimx G3dMesh with given index 
   */
  async getMeshRaw(mesh: number){
    const m = await this.bfast.getLocalBfastRaw(`mesh_${mesh}`, true)
    const result = await G3dMesh.createFromBfast(m)
    const scene = await this.sceneRaw.get()
    result.scene = scene
    result.meshIndex = mesh
    return result
  }

}