"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.G3dMeshOffsets = exports.G3dMeshCounts = void 0;
class G3dMeshCounts {
    constructor() {
        this.instances = 0;
        this.meshes = 0;
        this.indices = 0;
        this.vertices = 0;
    }
}
exports.G3dMeshCounts = G3dMeshCounts;
/**
 * Holds the offsets needed to preallocate geometry for a given meshIndexSubset
 */
class G3dMeshOffsets {
    /**
     * Computes geometry offsets for given subset and section
     * @param subset subset for which to compute offsets
     * @param section on of 'opaque' | 'transparent' | 'all'
     */
    static fromSubset(subset, section) {
        var result = new G3dMeshOffsets();
        result.subset = subset;
        result.section = section;
        function computeOffsets(getter) {
            const meshCount = subset.getMeshCount();
            const offsets = new Int32Array(meshCount);
            for (let i = 1; i < meshCount; i++) {
                offsets[i] = offsets[i - 1] + getter(i - 1);
            }
            return offsets;
        }
        result.counts = subset.getAttributeCounts(section);
        result.indexOffsets = computeOffsets((m) => subset.getMeshIndexCount(m, section));
        result.vertexOffsets = computeOffsets((m) => subset.getMeshVertexCount(m, section));
        return result;
    }
    getIndexOffset(mesh) {
        return mesh < this.counts.meshes
            ? this.indexOffsets[mesh]
            : this.counts.indices;
    }
    getVertexOffset(mesh) {
        return mesh < this.counts.meshes
            ? this.vertexOffsets[mesh]
            : this.counts.vertices;
    }
    /**
     * Returns how many instances of given meshes are the filtered view.
     */
    getMeshInstanceCount(mesh) {
        return this.subset.getMeshInstanceCount(mesh);
    }
    /**
     * Returns instance for given mesh.
     * @mesh view-relative mesh index
     * @at view-relative instance index for given mesh
     * @returns mesh-relative instance index
     */
    getMeshInstance(mesh, index) {
        return this.subset.getMeshInstance(mesh, index);
    }
    /**
     * Returns the vim-relative mesh index at given index
     */
    getMesh(index) {
        return this.subset.getMesh(index);
    }
}
exports.G3dMeshOffsets = G3dMeshOffsets;
