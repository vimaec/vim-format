import { MeshSection } from "./g3d";
import { G3dSubset } from "./g3dSubset";
export declare class G3dMeshCounts {
    instances: number;
    meshes: number;
    indices: number;
    vertices: number;
}
/**
 * Holds the offsets needed to preallocate geometry for a given meshIndexSubset
 */
export declare class G3dMeshOffsets {
    subset: G3dSubset;
    section: MeshSection;
    counts: G3dMeshCounts;
    indexOffsets: Int32Array;
    vertexOffsets: Int32Array;
    /**
     * Computes geometry offsets for given subset and section
     * @param subset subset for which to compute offsets
     * @param section on of 'opaque' | 'transparent' | 'all'
     */
    static fromSubset(subset: G3dSubset, section: MeshSection): G3dMeshOffsets;
    getIndexOffset(mesh: number): number;
    getVertexOffset(mesh: number): number;
    /**
     * Returns how many instances of given meshes are the filtered view.
     */
    getMeshInstanceCount(mesh: number): number;
    /**
     * Returns instance for given mesh.
     * @mesh view-relative mesh index
     * @at view-relative instance index for given mesh
     * @returns mesh-relative instance index
     */
    getMeshInstance(mesh: number, index: number): number;
    /**
     * Returns the vim-relative mesh index at given index
     */
    getMesh(index: number): number;
}
