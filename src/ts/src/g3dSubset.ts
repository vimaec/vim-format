import { G3d, MeshSection } from './g3d'
import { G3dMeshIndex } from './g3dMeshIndex'
import { G3dMeshCounts, G3dMeshOffsets } from './g3dMeshOffsets'

/**
 * Represents a filter applied to a G3dMeshIndex.
 */
export class G3dSubset
{
  private _source: G3dMeshIndex | G3d
  private _meshes: number[] | undefined
  private _meshInstances : (number[] | undefined)[] | undefined

/**
 * @param index G3d source geometry.
 * @param meshes indices of meshes to include or undefined if all meshes.
 * @param meshToInstances indices of instances to include for each mesh or undefined if all meshes.
 */
  constructor(index: G3dMeshIndex | G3d, meshes?: number[], meshToInstances? : number[][]){
    this._source = index
    this._meshes = meshes
    this._meshInstances = meshToInstances
  }

  getMesh(index: number){
    return this._meshes?.[index] ?? index
  }

  getMeshCount(){
    return this._meshes?.length ?? this._source.getMeshCount()
  }

  /**
   * Returns index count for given mesh and section.
   */
  getMeshIndexCount(mesh: number, section: MeshSection){
    const instances = this.getMeshInstanceCount(mesh)
    const indices = this._source.getMeshIndexCount(this.getMesh(mesh), section)
    return indices * instances
  }

  /**
   * Returns vertext count for given mesh and section.
   */
  getMeshVertexCount(mesh: number, section: MeshSection){
    const instances = this.getMeshInstanceCount(mesh)
    const vertices =  this._source.getMeshVertexCount(this.getMesh(mesh), section)
    return vertices * instances
  }

  /**
   * Returns instance count for given mesh.
   * @param mesh The index of the mesh from the g3dIndex.
   */
  getMeshInstanceCount(mesh: number){
    return this._meshInstances
      ? this._meshInstances[mesh].length
      : this._source.getMeshInstanceCount(this.getMesh(mesh))
  }

   /**
   * Returns index-th mesh-based instance index for given mesh.
   * @param mesh The index of the mesh from the g3dIndex.
   */
  getMeshInstance(mesh: number, index:number){
    const instance = this._meshInstances
      ? this._meshInstances[mesh][index]
      : index 

    if(this._source instanceof G3d){
      // Dereference one more time.
      return this._source.meshInstances[mesh][instance]
    }
    
    return instance
  }

  /**
   * Returns the list of mesh-based instance indices for given mesh or undefined if all instances are included.
   * @param mesh The index of the mesh from the g3dIndex.
   */
  getMeshInstances(mesh: number){
    return this._meshInstances?.[mesh]
  }
  
  /**
   * Returns a new subset that only contains unique meshes.
   */
  filterUniqueMeshes(){
    return this.filterByCount(count => count === 1)
  }

  /**
   * Returns a new subset that only contains non-unique meshes.
   */
  filterNonUniqueMeshes(){
    return this.filterByCount(count =>count > 1)
  }

  private filterByCount(predicate : (i: number) => boolean){
    const filteredMeshes = new Array<number>()
    const filteredInstances = this._meshInstances ? new Array<Array<number>>() : undefined
    const count = this.getMeshCount()
    for(let m =0; m < count; m++){
      if(predicate(this.getMeshInstanceCount(m))){
        filteredMeshes.push(this.getMesh(m))
        filteredInstances?.push(this.getMeshInstances(m))
      }
    }
    return new G3dSubset(this._source, filteredMeshes, filteredInstances)
  }

  /**
   * Returns offsets needed to build geometry.
   */
  getOffsets(section: MeshSection){
    return G3dMeshOffsets.fromSubset(this, section)
  }

  getAttributeCounts(section: MeshSection = 'all'){

    const result = new G3dMeshCounts()
    const count = this.getMeshCount()
    for(let i = 0; i < count; i ++){
      result.instances += this.getMeshInstanceCount(i)
      result.indices += this.getMeshIndexCount(i, section)
      result.vertices += this.getMeshVertexCount(i, section)
    }
    result.meshes = count

    return result
  }
}
