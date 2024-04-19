"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.G3dSubsetOG = exports.G3dSubset2 = void 0;
const g3d_1 = require("./g3d");
const g3dMeshIndex_1 = require("./g3dMeshIndex");
const g3dMeshOffsets_1 = require("./g3dMeshOffsets");
class G3dSubset2 {
    constructor(source, 
    //source-based indices of included instanced 
    instances) {
        this._source = source;
        // TODO: Remove this
        if (!instances) {
            instances = new Array();
            for (let i = 0; i < source.instanceMeshes.length; i++) {
                if (source.instanceMeshes[i] >= 0) {
                    instances.push(i);
                }
            }
        }
        this._instances = instances;
        this._meshes = new Array();
        const map = new Map();
        instances.forEach(instance => {
            const mesh = source.instanceMeshes[instance];
            const index = source instanceof g3dMeshIndex_1.G3dMeshIndex ? source.instanceIndices[instance] : instance;
            if (!map.has(mesh)) {
                this._meshes.push(mesh);
                map.set(mesh, [index]);
            }
            else {
                map.get(mesh).push(index);
            }
        });
        this._meshInstances = new Array(this._meshes.length);
        this._meshes.forEach((m, i) => this._meshInstances[i] = map.get(m));
    }
    getMesh(index) {
        return this._meshes?.[index] ?? index;
    }
    getMeshCount() {
        return this._meshes?.length ?? this._source.getMeshCount();
    }
    /**
     * Returns index count for given mesh and section.
     */
    getMeshIndexCount(mesh, section) {
        const instances = this.getMeshInstanceCount(mesh);
        const indices = this._source.getMeshIndexCount(this.getMesh(mesh), section);
        return indices * instances;
    }
    /**
     * Returns vertext count for given mesh and section.
     */
    getMeshVertexCount(mesh, section) {
        const instances = this.getMeshInstanceCount(mesh);
        const vertices = this._source.getMeshVertexCount(this.getMesh(mesh), section);
        return vertices * instances;
    }
    /**
     * Returns instance count for given mesh.
     * @param mesh The index of the mesh from the g3dIndex.
     */
    getMeshInstanceCount(mesh) {
        return this._meshInstances
            ? this._meshInstances[mesh].length
            : this._source.getMeshInstanceCount(this.getMesh(mesh));
    }
    /**
     * Returns the list of mesh-based instance indices for given mesh or undefined if all instances are included.
     * @param mesh The index of the mesh from the g3dIndex.
     */
    getMeshInstances(mesh) {
        return this._meshInstances?.[mesh];
    }
    /**
    * Returns index-th mesh-based instance index for given mesh.
    * @param mesh The index of the mesh from the g3dIndex.
    */
    getMeshInstance(mesh, index) {
        const instance = this._meshInstances
            ? this._meshInstances[mesh][index]
            : index;
        return instance;
    }
    /**
     * Returns a new subset that only contains unique meshes.
     */
    filterUniqueMeshes() {
        return this.filterByCount(count => count === 1);
    }
    /**
     * Returns a new subset that only contains non-unique meshes.
     */
    filterNonUniqueMeshes() {
        return this.filterByCount(count => count > 1);
    }
    filterByCount(predicate) {
        const set = new Set();
        this._meshInstances.forEach((instances, i) => {
            if (predicate(instances.length)) {
                set.add(this._meshes[i]);
            }
        });
        const instances = this._instances.filter(instance => set.has(this._source.instanceMeshes[instance]));
        return new G3dSubset2(this._source, instances);
    }
    /**
   * Returns offsets needed to build geometry.
   */
    getOffsets(section) {
        return g3dMeshOffsets_1.G3dMeshOffsets.fromSubset(this, section);
    }
    getAttributeCounts(section = 'all') {
        const result = new g3dMeshOffsets_1.G3dMeshCounts();
        const count = this.getMeshCount();
        for (let i = 0; i < count; i++) {
            result.instances += this.getMeshInstanceCount(i);
            result.indices += this.getMeshIndexCount(i, section);
            result.vertices += this.getMeshVertexCount(i, section);
        }
        result.meshes = count;
        return result;
    }
    getBoudingBox() {
        if (this._instances.length === 0)
            return;
        if (this._source instanceof g3dMeshIndex_1.G3dMeshIndex) {
            // To avoid including (0,0,0)
            let box = new Float32Array(6);
            const min = this._source.getInstanceMin(this._instances[0]);
            const max = this._source.getInstanceMax(this._instances[0]);
            box[0] = min[0];
            box[1] = min[1];
            box[2] = min[2];
            box[3] = max[0];
            box[4] = max[1];
            box[5] = max[2];
            for (let i = 1; i < this._instances.length; i++) {
                const instance = this._instances[i];
                const min = this._source.getInstanceMin(instance);
                const max = this._source.getInstanceMax(instance);
                minBox(box, min);
                maxBox(box, max);
            }
            return box;
        }
    }
    filter(mode, filter) {
        if (filter === undefined || mode === undefined) {
            return new G3dSubset2(this._source, undefined);
        }
        if (mode === 'instance') {
            const instances = this.filterOnArray(filter, this._source.instanceNodes);
            return new G3dSubset2(this._source, instances);
        }
        if (mode === 'mesh') {
            const instances = this.filterOnArray(filter, this._source.instanceMeshes);
            return new G3dSubset2(this._source, instances);
        }
        if (mode === 'tag' || mode === 'group') {
            throw new Error("Filter Mode Not implemented");
        }
    }
    filterOnArray(filter, array) {
        const set = new Set(filter);
        const result = new Array();
        array.forEach((mesh, i) => {
            if (set.has(mesh) && this._source.instanceMeshes[i] >= 0) {
                result.push(i);
            }
        });
        return result;
    }
}
exports.G3dSubset2 = G3dSubset2;
/**
 * Represents a filter applied to a G3dMeshIndex.
 */
class G3dSubsetOG {
    /**
     * @param index G3d source geometry.
     * @param meshes indices of meshes to include or undefined if all meshes.
     * @param meshToInstances indices of instances to include for each mesh or undefined if all meshes.
     */
    constructor(index, instances, meshes, meshToInstances) {
        this._source = index;
        this._instances = instances;
        this._meshes = meshes;
        this._meshInstances = meshToInstances;
    }
    getBoudingBox() {
        if (this._instances.length === 0)
            return;
        if (this._source instanceof g3dMeshIndex_1.G3dMeshIndex) {
            // To avoid including (0,0,0)
            let box = new Float32Array(6);
            const min = this._source.getInstanceMin(this._instances[0]);
            const max = this._source.getInstanceMax(this._instances[0]);
            box[0] = min[0];
            box[1] = min[1];
            box[2] = min[2];
            box[3] = max[0];
            box[4] = max[1];
            box[5] = max[2];
            for (let i = 1; i < this._instances.length; i++) {
                const instance = this._instances[i];
                const min = this._source.getInstanceMin(instance);
                const max = this._source.getInstanceMax(instance);
                minBox(box, min);
                maxBox(box, max);
            }
            return box;
        }
    }
    getMesh(index) {
        return this._meshes?.[index] ?? index;
    }
    getMeshCount() {
        return this._meshes?.length ?? this._source.getMeshCount();
    }
    /**
     * Returns index count for given mesh and section.
     */
    getMeshIndexCount(mesh, section) {
        const instances = this.getMeshInstanceCount(mesh);
        const indices = this._source.getMeshIndexCount(this.getMesh(mesh), section);
        return indices * instances;
    }
    /**
     * Returns vertext count for given mesh and section.
     */
    getMeshVertexCount(mesh, section) {
        const instances = this.getMeshInstanceCount(mesh);
        const vertices = this._source.getMeshVertexCount(this.getMesh(mesh), section);
        return vertices * instances;
    }
    /**
     * Returns instance count for given mesh.
     * @param mesh The index of the mesh from the g3dIndex.
     */
    getMeshInstanceCount(mesh) {
        return this._meshInstances
            ? this._meshInstances[mesh].length
            : this._source.getMeshInstanceCount(this.getMesh(mesh));
    }
    /**
    * Returns index-th mesh-based instance index for given mesh. Returns -1 otherwise.
    * @param mesh The index of the mesh from the g3dIndex.
    */
    getMeshInstance(mesh, index) {
        const instance = this._meshInstances
            ? this._meshInstances[mesh][index]
            : index;
        if (this._source instanceof g3d_1.G3d) {
            // Dereference one more time. Meshes can sometime be unreferenced
            const m = this.getMesh(mesh);
            return this._source.meshInstances[m]?.[instance] ?? -1;
        }
        return instance;
    }
    /**
     * Returns the list of mesh-based instance indices for given mesh or undefined if all instances are included.
     * @param mesh The index of the mesh from the g3dIndex.
     */
    getMeshInstances(mesh) {
        return this._meshInstances?.[mesh];
    }
    /**
     * Returns a new subset that only contains unique meshes.
     */
    filterUniqueMeshes() {
        return this.filterByCount(count => count === 1);
    }
    /**
     * Returns a new subset that only contains non-unique meshes.
     */
    filterNonUniqueMeshes() {
        return this.filterByCount(count => count > 1);
    }
    filterByCount(predicate) {
        const filteredMeshes = new Array();
        const filteredInstances = this._meshInstances ? new Array() : undefined;
        const count = this.getMeshCount();
        for (let m = 0; m < count; m++) {
            if (predicate(this.getMeshInstanceCount(m))) {
                filteredMeshes.push(this.getMesh(m));
                filteredInstances?.push(this.getMeshInstances(m));
            }
        }
        const meshes = new Set(filteredMeshes);
        const instances = this._instances?.filter(i => meshes.has(this._source.instanceMeshes[i]));
        return new G3dSubsetOG(this._source, instances, filteredMeshes, filteredInstances);
    }
    /**
     * Returns offsets needed to build geometry.
     */
    getOffsets(section) {
        return g3dMeshOffsets_1.G3dMeshOffsets.fromSubset(this, section);
    }
    getAttributeCounts(section = 'all') {
        const result = new g3dMeshOffsets_1.G3dMeshCounts();
        const count = this.getMeshCount();
        for (let i = 0; i < count; i++) {
            result.instances += this.getMeshInstanceCount(i);
            result.indices += this.getMeshIndexCount(i, section);
            result.vertices += this.getMeshVertexCount(i, section);
        }
        result.meshes = count;
        return result;
    }
}
exports.G3dSubsetOG = G3dSubsetOG;
function minBox(box, other) {
    box[0] = Math.min(box[0], other[0]);
    box[1] = Math.min(box[1], other[1]);
    box[2] = Math.min(box[2], other[2]);
}
function maxBox(box, other) {
    box[3] = Math.max(box[3], other[0]);
    box[4] = Math.max(box[4], other[1]);
    box[5] = Math.max(box[5], other[2]);
}
