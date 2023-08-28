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

  async getIndex(){
    const index = await this.bfast.getLocalBfast('index', true)
    return G3dMeshIndex.createFromBfast(index)
  }

  async getMaterials(){
    const mat = await this.bfast.getLocalBfast('materials', true)
    return G3dMaterials.createFromBfast(mat)
  }

  async getMesh(mesh: number){
    const m = await this.bfast.getLocalBfast(`mesh_${mesh}`, true)
    return G3dMesh.createFromBfast(m)
  }

}