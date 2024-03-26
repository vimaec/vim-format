import { G3d, MeshSection } from './g3d';
import { FilterMode, G3dMeshIndex } from './g3dMeshIndex';
import { G3dMeshCounts, G3dMeshOffsets } from './g3dMeshOffsets';
export declare type G3dSubset = G3dSubsetOG | G3dSubset2;
export declare class G3dSubset2 {
    private _source;
    private _instances;
    private _meshes;
    private _meshInstances;
    constructor(source: G3dMeshIndex | G3d, instances: number[]);
    getMesh(index: number): number;
    getMeshCount(): number;
    /**
     * Returns index count for given mesh and section.
     */
    getMeshIndexCount(mesh: number, section: MeshSection): number;
    /**
     * Returns vertext count for given mesh and section.
     */
    getMeshVertexCount(mesh: number, section: MeshSection): number;
    /**
     * Returns instance count for given mesh.
     * @param mesh The index of the mesh from the g3dIndex.
     */
    getMeshInstanceCount(mesh: number): number;
    /**
     * Returns the list of mesh-based instance indices for given mesh or undefined if all instances are included.
     * @param mesh The index of the mesh from the g3dIndex.
     */
    getMeshInstances(mesh: number): number[];
    /**
    * Returns index-th mesh-based instance index for given mesh.
    * @param mesh The index of the mesh from the g3dIndex.
    */
    getMeshInstance(mesh: number, index: number): number;
    /**
     * Returns a new subset that only contains unique meshes.
     */
    filterUniqueMeshes(): G3dSubset2;
    /**
     * Returns a new subset that only contains non-unique meshes.
     */
    filterNonUniqueMeshes(): G3dSubset2;
    private filterByCount;
    /**
   * Returns offsets needed to build geometry.
   */
    getOffsets(section: MeshSection): G3dMeshOffsets;
    getAttributeCounts(section?: MeshSection): G3dMeshCounts;
    getBoudingBox(): Float32Array;
    filter(mode: FilterMode, filter: number[]): G3dSubset2;
    private filterOnArray;
}
/**
 * Represents a filter applied to a G3dMeshIndex.
 */
export declare class G3dSubsetOG {
    private _source;
    private _instances;
    private _meshes;
    private _meshInstances;
    /**
     * @param index G3d source geometry.
     * @param meshes indices of meshes to include or undefined if all meshes.
     * @param meshToInstances indices of instances to include for each mesh or undefined if all meshes.
     */
    constructor(index: G3dMeshIndex | G3d, instances: number[], meshes: number[], meshToInstances: number[][]);
    getBoudingBox(): Float32Array;
    getMesh(index: number): number;
    getMeshCount(): number;
    /**
     * Returns index count for given mesh and section.
     */
    getMeshIndexCount(mesh: number, section: MeshSection): number;
    /**
     * Returns vertext count for given mesh and section.
     */
    getMeshVertexCount(mesh: number, section: MeshSection): number;
    /**
     * Returns instance count for given mesh.
     * @param mesh The index of the mesh from the g3dIndex.
     */
    getMeshInstanceCount(mesh: number): number;
    /**
    * Returns index-th mesh-based instance index for given mesh. Returns -1 otherwise.
    * @param mesh The index of the mesh from the g3dIndex.
    */
    getMeshInstance(mesh: number, index: number): number;
    /**
     * Returns the list of mesh-based instance indices for given mesh or undefined if all instances are included.
     * @param mesh The index of the mesh from the g3dIndex.
     */
    getMeshInstances(mesh: number): number[];
    /**
     * Returns a new subset that only contains unique meshes.
     */
    filterUniqueMeshes(): G3dSubsetOG;
    /**
     * Returns a new subset that only contains non-unique meshes.
     */
    filterNonUniqueMeshes(): G3dSubsetOG;
    private filterByCount;
    /**
     * Returns offsets needed to build geometry.
     */
    getOffsets(section: MeshSection): G3dMeshOffsets;
    getAttributeCounts(section?: MeshSection): G3dMeshCounts;
}
