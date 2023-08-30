import { BFast } from "./bfast";
import { G3dMaterial as G3dMaterials } from "./g3dMaterials";
import { G3dMesh } from "./g3dMesh";
import { G3dMeshIndex } from "./g3dMeshIndex";
import { RemoteBuffer } from "./remoteBuffer";


export class RemoteGeometry{
  bfast : BFast

  constructor(bfast : BFast){
    this.bfast = bfast
  }



  static async fromPath(path: string){
    const buffer = new RemoteBuffer(path)
    const bfast = new BFast(buffer)
    return new RemoteGeometry(bfast)
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
  async getIndex(){
    const index = await this.bfast.getLocalBfast('index', true)
    return G3dMeshIndex.createFromBfast(index)
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
    return G3dMesh.createFromBfast(m)
  }

}