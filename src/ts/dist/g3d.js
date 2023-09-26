"use strict";
/**
 * @module vim-ts
 */
Object.defineProperty(exports, "__esModule", { value: true });
exports.G3d = exports.VimAttributes = void 0;
const abstractG3d_1 = require("./abstractG3d");
const bfast_1 = require("./bfast");
/**
 * See https://github.com/vimaec/vim#vim-geometry-attributes
 */
class VimAttributes {
}
exports.VimAttributes = VimAttributes;
VimAttributes.positions = 'g3d:vertex:position:0:float32:3';
VimAttributes.indices = 'g3d:corner:index:0:int32:1';
VimAttributes.instanceMeshes = 'g3d:instance:mesh:0:int32:1';
VimAttributes.instanceTransforms = 'g3d:instance:transform:0:float32:16';
VimAttributes.instanceNodes = 'g3d:instance:element:0:int32:1';
VimAttributes.instanceFlags = 'g3d:instance:flags:0:uint16:1';
VimAttributes.meshSubmeshes = 'g3d:mesh:submeshoffset:0:int32:1';
VimAttributes.submeshIndexOffsets = 'g3d:submesh:indexoffset:0:int32:1';
VimAttributes.submeshMaterials = 'g3d:submesh:material:0:int32:1';
VimAttributes.materialColors = 'g3d:material:color:0:float32:4';
VimAttributes.all = [
    VimAttributes.positions,
    VimAttributes.indices,
    VimAttributes.instanceMeshes,
    VimAttributes.instanceTransforms,
    VimAttributes.instanceFlags,
    VimAttributes.meshSubmeshes,
    VimAttributes.submeshIndexOffsets,
    VimAttributes.submeshMaterials,
    VimAttributes.materialColors
];
/**
 * G3D is a simple, efficient, generic binary format for storing and transmitting geometry.
 * The G3D format is designed to be used either as a serialization format or as an in-memory data structure.
 * A G3d with specific attributes according to the VIM format specification.
 * See https://github.com/vimaec/vim#vim-geometry-attributes for the vim specification.
 * See https://github.com/vimaec/g3d for the g3d specification.
 */
class G3d {
    constructor(instanceMeshes, instanceFlags, instanceTransforms, instanceNodes, meshSubmeshes, submeshIndexOffsets, submeshMaterials, indices, positions, materialColors) {
        /**
         * Opaque white
         */
        this.DEFAULT_COLOR = new Float32Array([1, 1, 1, 1]);
        this.getVertexCount = () => this.positions.length / G3d.POSITION_SIZE;
        this.getIndexCount = () => this.indices.length;
        // ------------- Meshes -----------------
        this.getMeshCount = () => this.meshSubmeshes.length;
        // ------------- Instances -----------------
        this.getInstanceCount = () => this.instanceMeshes.length;
        // ------------- Material -----------------
        this.getMaterialCount = () => this.materialColors.length / G3d.COLOR_SIZE;
        this.instanceMeshes = instanceMeshes;
        this.instanceFlags = instanceFlags;
        this.instanceTransforms = instanceTransforms;
        this.instanceNodes = instanceNodes;
        this.meshSubmeshes = meshSubmeshes;
        this.submeshIndexOffset = submeshIndexOffsets;
        this.submeshMaterial = submeshMaterials;
        this.indices = indices instanceof Uint32Array ? indices : new Uint32Array(indices.buffer);
        this.positions = positions;
        this.materialColors = materialColors;
        if (this.instanceFlags === undefined) {
            this.instanceFlags = new Uint16Array(this.instanceMeshes.length);
        }
        if (this.instanceNodes === undefined) {
            this.instanceNodes = new Int32Array(instanceMeshes.length);
            for (let i = 0; i < this.instanceNodes.length; i++) {
                this.instanceNodes[i] = i;
            }
        }
        this.meshVertexOffsets = this.computeMeshVertexOffsets();
        this.rebaseIndices();
        this.meshInstances = this.computeMeshInstances();
        this.meshOpaqueCount = this.computeMeshOpaqueCount();
        this.sortSubmeshes();
        const range = this.computeSubmeshVertexRange();
        this.submeshVertexStart = range.start;
        this.submeshVertexEnd = range.end;
    }
    computeSubmeshVertexRange() {
        const submeshCount = this.getSubmeshCount();
        const start = new Int32Array(submeshCount);
        const end = new Int32Array(submeshCount);
        for (let sub = 0; sub < submeshCount; sub++) {
            let min = Number.MAX_SAFE_INTEGER;
            let max = Number.MIN_SAFE_INTEGER;
            const subStart = this.getSubmeshIndexStart(sub);
            const subEnd = this.getSubmeshIndexEnd(sub);
            for (let i = subStart; i < subEnd; i++) {
                const index = this.indices[i];
                min = Math.min(min, index);
                max = Math.max(min, index);
            }
            start[sub] = min;
            end[sub] = max;
        }
        return { start, end };
    }
    static createFromAbstract(g3d) {
        const instanceMeshes = g3d.findAttribute(VimAttributes.instanceMeshes)
            ?.data;
        const instanceTransforms = g3d.findAttribute(VimAttributes.instanceTransforms)?.data;
        const instanceFlags = g3d.findAttribute(VimAttributes.instanceFlags)?.data ??
            new Uint16Array(instanceMeshes.length);
        const instanceNodes = g3d.findAttribute(VimAttributes.instanceNodes)?.data;
        const meshSubmeshes = g3d.findAttribute(VimAttributes.meshSubmeshes)
            ?.data;
        const submeshIndexOffset = g3d.findAttribute(VimAttributes.submeshIndexOffsets)?.data;
        const submeshMaterial = g3d.findAttribute(VimAttributes.submeshMaterials)
            ?.data;
        const indices = g3d.findAttribute(VimAttributes.indices)?.data;
        const positions = g3d.findAttribute(VimAttributes.positions)
            ?.data;
        const materialColors = g3d.findAttribute(VimAttributes.materialColors)
            ?.data;
        const result = new G3d(instanceMeshes, instanceFlags, instanceTransforms, instanceNodes, meshSubmeshes, submeshIndexOffset, submeshMaterial, indices, positions, materialColors);
        result.rawG3d = g3d;
        return result;
    }
    static async createFromPath(path) {
        const f = await fetch(path);
        const buffer = await f.arrayBuffer();
        const bfast = new bfast_1.BFast(buffer);
        return this.createFromBfast(bfast);
    }
    static async createFromBfast(bfast) {
        const g3d = await abstractG3d_1.AbstractG3d.createFromBfast(bfast, VimAttributes.all);
        return G3d.createFromAbstract(g3d);
    }
    /**
     * Computes the index of the first vertex of each mesh
     */
    computeMeshVertexOffsets() {
        const result = new Int32Array(this.getMeshCount());
        for (let m = 0; m < result.length; m++) {
            let min = Number.MAX_SAFE_INTEGER;
            const start = this.getMeshIndexStart(m, 'all');
            const end = this.getMeshIndexEnd(m, 'all');
            for (let i = start; i < end; i++) {
                min = Math.min(min, this.indices[i]);
            }
            result[m] = min;
        }
        return result;
    }
    /**
     * Computes all instances pointing to each mesh.
     */
    computeMeshInstances() {
        const result = new Array(this.getMeshCount());
        for (let i = 0; i < this.instanceMeshes.length; i++) {
            const mesh = this.instanceMeshes[i];
            if (mesh < 0)
                continue;
            const instanceIndices = result[mesh];
            if (instanceIndices)
                instanceIndices.push(i);
            else
                result[mesh] = [i];
        }
        return result;
    }
    /**
     * Reorders submeshIndexOffset, submeshMaterials and indices
     * such that for each mesh, submeshes are sorted according to material alpha.
     * This enables efficient splitting of arrays into opaque and transparent continuous ranges.
     */
    sortSubmeshes() {
        // We need to compute where submeshes end before we can reorder them.
        const submeshEnd = this.computeSubmeshEnd();
        // We need to compute mesh index offsets from before we swap thins around to recompute new submesh offsets.
        const meshIndexOffsets = this.computeMeshIndexOffsets();
        const meshCount = this.getMeshCount();
        const meshReordered = new Array(meshCount);
        const submeshArrays = [
            this.submeshIndexOffset,
            this.submeshMaterial,
            submeshEnd
        ];
        // Largest mesh size thus minimum buffer size to use to reorder indices.
        const largestMesh = this.reorderSubmeshes(submeshArrays, meshReordered);
        this.reorderIndices(meshIndexOffsets, submeshEnd, meshReordered, largestMesh);
    }
    /**
     * Stores result of getSubmeshIndexEnd for each submesh in an array
     */
    computeSubmeshEnd() {
        const submeshCount = this.getSubmeshCount();
        const result = new Int32Array(submeshCount);
        for (let s = 0; s < submeshCount; s++) {
            result[s] = this.getSubmeshIndexEnd(s);
        }
        return result;
    }
    /**
     * Stores result of getMeshIndexStart for each mesh in an array
     */
    computeMeshIndexOffsets() {
        const meshCount = this.getMeshCount();
        const result = new Int32Array(meshCount);
        for (let m = 0; m < meshCount; m++) {
            result[m] = this.getMeshIndexStart(m, 'all');
        }
        return result;
    }
    /**
     * Reorder submesh arrays and returns size of largest reordered mesh
     */
    reorderSubmeshes(submeshArrays, reordered) {
        const meshCount = this.getMeshCount();
        let largestMesh = 0;
        for (let m = 0; m < meshCount; m++) {
            const subStart = this.getMeshSubmeshStart(m, 'all');
            const subEnd = this.getMeshSubmeshEnd(m, 'all');
            if (subEnd - subStart <= 1) {
                continue;
            }
            largestMesh = Math.max(largestMesh, this.getMeshIndexCount(m, 'all'));
            reordered[m] = this.Sort(subStart, subEnd, (i) => this.getSubmeshAlpha(i), submeshArrays);
        }
        return largestMesh;
    }
    /**
     * Sorts the range from start to end in every array provided in arrays in increasing criterion order.
     * Using a simple bubble sort, there is a limited number of submeshes per mesh.
     */
    Sort(start, end, criterion, arrays) {
        let swapped = false;
        while (true) {
            let loop = false;
            for (let i = start; i < end - 1; i++) {
                if (criterion(i) < criterion(i + 1)) {
                    loop = true;
                    swapped = true;
                    for (let j = 0; j < arrays.length; j++) {
                        const array = arrays[j];
                        const t = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = t;
                    }
                }
            }
            if (!loop) {
                break;
            }
        }
        return swapped;
    }
    /**
     * Reorders the index buffer to match the new order of the submesh arrays.
     */
    reorderIndices(meshIndexOffsets, submeshEnd, meshReordered, bufferSize) {
        const meshCount = this.getMeshCount();
        const buffer = new Float32Array(bufferSize);
        for (let m = 0; m < meshCount; m++) {
            if (!meshReordered[m])
                continue;
            const meshOffset = meshIndexOffsets[m];
            const subStart = this.getMeshSubmeshStart(m, 'all');
            const subEnd = this.getMeshSubmeshEnd(m, 'all');
            let index = 0;
            // Copy indices -> buffer, in sorted order.
            for (let s = subStart; s < subEnd; s++) {
                const start = this.submeshIndexOffset[s];
                const end = submeshEnd[s];
                // Change submesh offset to match new ordering
                this.submeshIndexOffset[s] = meshOffset + index;
                for (let i = start; i < end; i++) {
                    buffer[index++] = this.indices[i];
                }
            }
            // Copy buffer -> indices
            for (let i = 0; i < index; i++) {
                this.indices[meshOffset + i] = buffer[i];
            }
        }
    }
    /**
     * Rebase indices to be relative to its own mesh instead of to the whole g3d
     */
    rebaseIndices() {
        const count = this.getMeshCount();
        for (let m = 0; m < count; m++) {
            const offset = this.meshVertexOffsets[m];
            const start = this.getMeshIndexStart(m, 'all');
            const end = this.getMeshIndexEnd(m, 'all');
            for (let i = start; i < end; i++) {
                this.indices[i] -= offset;
            }
        }
    }
    unbaseIndices() {
        const count = this.getMeshCount();
        for (let m = 0; m < count; m++) {
            const offset = this.meshVertexOffsets[m];
            const start = this.getMeshIndexStart(m, 'all');
            const end = this.getMeshIndexEnd(m, 'all');
            for (let i = start; i < end; i++) {
                this.indices[i] += offset;
            }
        }
    }
    /**
     * Computes an array where true if any of the materials used by a mesh has transparency.
     */
    computeMeshOpaqueCount() {
        const result = new Int32Array(this.getMeshCount());
        for (let m = 0; m < result.length; m++) {
            const subStart = this.getMeshSubmeshStart(m, 'all');
            const subEnd = this.getMeshSubmeshEnd(m, 'all');
            for (let s = subStart; s < subEnd; s++) {
                const alpha = this.getSubmeshAlpha(s);
                result[m] += alpha === 1 ? 1 : 0;
            }
        }
        return result;
    }
    // ------------- All -----------------
    /**Given VIM instance indices returns the corresponding G3d indices */
    remapInstances(instances) {
        const map = new Map();
        for (let i = 0; i < instances.length; i++) {
            map.set(this.instanceNodes[i], i);
        }
        return instances.map((i) => map.get(i));
    }
    getMeshInstanceCount(mesh) {
        return this.meshInstances[mesh]?.length ?? 0;
    }
    getMeshIndexStart(mesh, section = 'all') {
        const sub = this.getMeshSubmeshStart(mesh, section);
        return this.getSubmeshIndexStart(sub);
    }
    getMeshIndexEnd(mesh, section = 'all') {
        const sub = this.getMeshSubmeshEnd(mesh, section);
        return this.getSubmeshIndexEnd(sub - 1);
    }
    getMeshIndexCount(mesh, section = 'all') {
        return (this.getMeshIndexEnd(mesh, section) -
            this.getMeshIndexStart(mesh, section));
    }
    getMeshVertexStart(mesh) {
        return this.meshVertexOffsets[mesh];
    }
    getMeshVertexEnd(mesh) {
        return mesh < this.meshVertexOffsets.length - 1
            ? this.meshVertexOffsets[mesh + 1]
            : this.getVertexCount();
    }
    getMeshVertexCount(mesh) {
        return this.getMeshVertexEnd(mesh) - this.getMeshVertexStart(mesh);
    }
    getMeshSubmeshStart(mesh, section = 'all') {
        if (section === 'transparent') {
            return this.getMeshSubmeshEnd(mesh, 'opaque');
        }
        return this.meshSubmeshes[mesh];
    }
    getMeshSubmeshEnd(mesh, section = 'all') {
        if (section === 'opaque') {
            return this.meshSubmeshes[mesh] + this.meshOpaqueCount[mesh];
        }
        return mesh < this.meshSubmeshes.length - 1
            ? this.meshSubmeshes[mesh + 1]
            : this.getSubmeshCount();
    }
    getMeshSubmeshCount(mesh, section = 'all') {
        const end = this.getMeshSubmeshEnd(mesh, section);
        const start = this.getMeshSubmeshStart(mesh, section);
        return end - start;
    }
    getMeshHasTransparency(mesh) {
        return this.getMeshSubmeshCount(mesh, 'transparent') > 0;
    }
    // ------------- Submeshes -----------------
    getSubmeshIndexStart(submesh) {
        return submesh < this.submeshIndexOffset.length
            ? this.submeshIndexOffset[submesh]
            : this.indices.length;
    }
    getSubmeshIndexEnd(submesh) {
        return submesh < this.submeshIndexOffset.length - 1
            ? this.submeshIndexOffset[submesh + 1]
            : this.indices.length;
    }
    getSubmeshIndexCount(submesh) {
        return this.getSubmeshIndexEnd(submesh) - this.getSubmeshIndexStart(submesh);
    }
    getSubmeshVertexStart(submesh) {
        return this.submeshVertexStart[submesh];
    }
    getSubmeshVertexEnd(submesh) {
        return this.submeshVertexEnd[submesh];
    }
    getSubmeshVertexCount(submesh) {
        return this.getSubmeshVertexEnd(submesh) - this.getSubmeshVertexStart(submesh);
    }
    /**
     * Returns color of given submesh as a 4-number array (RGBA)
     * @param submesh g3d submesh index
     */
    getSubmeshColor(submesh) {
        return this.getMaterialColor(this.submeshMaterial[submesh]);
    }
    /**
     * Returns color of given submesh as a 4-number array (RGBA)
     * @param submesh g3d submesh index
     */
    getSubmeshAlpha(submesh) {
        return this.getMaterialAlpha(this.submeshMaterial[submesh]);
    }
    /**
     * Returns true if submesh is transparent.
     * @param submesh g3d submesh index
     */
    getSubmeshIsTransparent(submesh) {
        return this.getSubmeshAlpha(submesh) < 1;
    }
    /**
     * Returns the total number of mesh in the g3d
     */
    getSubmeshCount() {
        return this.submeshIndexOffset.length;
    }
    /**
     * Returns true if instance has given flag enabled.
     * @param instance instance to check.
     * @param flag to check against.
     */
    getInstanceHasFlag(instance, flag) {
        return (this.instanceFlags[instance] & flag) > 0;
    }
    /**
     * Returns mesh index of given instance
     * @param instance g3d instance index
     */
    getInstanceMesh(instance) {
        return this.instanceMeshes[instance];
    }
    /**
     * Returns an 16 number array representation of the matrix for given instance
     * @param instance g3d instance index
     */
    getInstanceMatrix(instance) {
        return this.instanceTransforms.subarray(instance * G3d.MATRIX_SIZE, (instance + 1) * G3d.MATRIX_SIZE);
    }
    /**
     * Returns color of given material as a 4-number array (RGBA)
     * @param material g3d material index
     */
    getMaterialColor(material) {
        if (material < 0)
            return this.DEFAULT_COLOR;
        return this.materialColors.subarray(material * G3d.COLOR_SIZE, (material + 1) * G3d.COLOR_SIZE);
    }
    /**
     * Returns the alpha component of given material
     * @param material
     */
    getMaterialAlpha(material) {
        if (material < 0)
            return 1;
        const index = material * G3d.COLOR_SIZE + G3d.COLOR_SIZE - 1;
        const result = this.materialColors[index];
        return result;
    }
    /**
     * Concatenates two g3ds into a new g3d.
     * @deprecated
     */
    append(other) {
        const _instanceFlags = new Uint16Array(this.instanceFlags.length + other.instanceFlags.length);
        _instanceFlags.set(this.instanceFlags);
        _instanceFlags.set(other.instanceFlags, this.instanceFlags.length);
        const _instanceMeshes = new Int32Array(this.instanceMeshes.length + other.instanceMeshes.length);
        _instanceMeshes.set(this.instanceMeshes);
        _instanceMeshes.set(other.instanceMeshes.map(m => m >= 0 ? (m + this.meshSubmeshes.length) : -1), this.instanceMeshes.length);
        const _instanceTransforms = new Float32Array(this.instanceTransforms.length + other.instanceTransforms.length);
        _instanceTransforms.set(this.instanceTransforms);
        _instanceTransforms.set(other.instanceTransforms, this.instanceTransforms.length);
        const _positions = new Float32Array(this.positions.length + other.positions.length);
        _positions.set(this.positions);
        _positions.set(other.positions, this.positions.length);
        this.unbaseIndices();
        other.unbaseIndices();
        const _indices = new Uint32Array(this.indices.length + other.indices.length);
        _indices.set(this.indices);
        _indices.set(other.indices.map(i => i + this.positions.length / 3), this.indices.length);
        this.rebaseIndices();
        other.rebaseIndices();
        const _meshSubmeshes = new Int32Array(this.meshSubmeshes.length + other.meshSubmeshes.length);
        _meshSubmeshes.set(this.meshSubmeshes);
        _meshSubmeshes.set(other.meshSubmeshes.map(s => s + this.submeshIndexOffset.length), this.meshSubmeshes.length);
        const _submeshIndexOffsets = new Int32Array(this.submeshIndexOffset.length + other.submeshIndexOffset.length);
        _submeshIndexOffsets.set(this.submeshIndexOffset);
        _submeshIndexOffsets.set(other.submeshIndexOffset.map(s => s + this.indices.length), this.submeshIndexOffset.length);
        const _submeshMaterials = new Int32Array(this.submeshMaterial.length + other.submeshMaterial.length);
        _submeshMaterials.set(this.submeshMaterial);
        _submeshMaterials.set(other.submeshMaterial.map(s => s >= 0 ? (s + this.materialColors.length / 4) : -1), this.submeshMaterial.length);
        const _materialColors = new Float32Array(this.materialColors.length + other.materialColors.length);
        _materialColors.set(this.materialColors);
        _materialColors.set(other.materialColors, this.materialColors.length);
        const g3d = new G3d(_instanceMeshes, _instanceFlags, _instanceTransforms, undefined, _meshSubmeshes, _submeshIndexOffsets, _submeshMaterials, _indices, _positions, _materialColors);
        return g3d;
    }
    validate() {
        const isPresent = (attribute, label) => {
            if (!attribute) {
                throw new Error(`Missing Attribute Buffer: ${label}`);
            }
        };
        isPresent(this.positions, 'position');
        isPresent(this.indices, 'indices');
        isPresent(this.instanceMeshes, 'instanceMeshes');
        isPresent(this.instanceTransforms, 'instanceTransforms');
        isPresent(this.meshSubmeshes, 'meshSubmeshes');
        isPresent(this.submeshIndexOffset, 'submeshIndexOffset');
        isPresent(this.submeshMaterial, 'submeshMaterial');
        isPresent(this.materialColors, 'materialColors');
        // Basic
        if (this.positions.length % G3d.POSITION_SIZE !== 0) {
            throw new Error('Invalid position buffer, must be divisible by ' + G3d.POSITION_SIZE);
        }
        if (this.indices.length % 3 !== 0) {
            throw new Error('Invalid Index Count, must be divisible by 3');
        }
        for (let i = 0; i < this.indices.length; i++) {
            if (this.indices[i] < 0 || this.indices[i] >= this.positions.length) {
                throw new Error('Vertex index out of bound');
            }
        }
        // Instances
        if (this.instanceMeshes.length !==
            this.instanceTransforms.length / G3d.MATRIX_SIZE) {
            throw new Error('Instance buffers mismatched');
        }
        if (this.instanceTransforms.length % G3d.MATRIX_SIZE !== 0) {
            throw new Error('Invalid InstanceTransform buffer, must respect arity ' +
                G3d.MATRIX_SIZE);
        }
        for (let i = 0; i < this.instanceMeshes.length; i++) {
            if (this.instanceMeshes[i] >= this.meshSubmeshes.length) {
                throw new Error('Instance Mesh Out of range.');
            }
        }
        // Meshes
        for (let i = 0; i < this.meshSubmeshes.length; i++) {
            if (this.meshSubmeshes[i] < 0 ||
                this.meshSubmeshes[i] >= this.submeshIndexOffset.length) {
                throw new Error('MeshSubmeshOffset out of bound at');
            }
        }
        for (let i = 0; i < this.meshSubmeshes.length - 1; i++) {
            if (this.meshSubmeshes[i] >= this.meshSubmeshes[i + 1]) {
                throw new Error('MeshSubmesh out of sequence.');
            }
        }
        // Submeshes
        if (this.submeshIndexOffset.length !== this.submeshMaterial.length) {
            throw new Error('Mismatched submesh buffers');
        }
        for (let i = 0; i < this.submeshIndexOffset.length; i++) {
            if (this.submeshIndexOffset[i] < 0 ||
                this.submeshIndexOffset[i] >= this.indices.length) {
                throw new Error('SubmeshIndexOffset out of bound');
            }
        }
        for (let i = 0; i < this.submeshIndexOffset.length; i++) {
            if (this.submeshIndexOffset[i] % 3 !== 0) {
                throw new Error('Invalid SubmeshIndexOffset, must be divisible by 3');
            }
        }
        for (let i = 0; i < this.submeshIndexOffset.length - 1; i++) {
            if (this.submeshIndexOffset[i] >= this.submeshIndexOffset[i + 1]) {
                throw new Error('SubmeshIndexOffset out of sequence.');
            }
        }
        for (let i = 0; i < this.submeshMaterial.length; i++) {
            if (this.submeshMaterial[i] >= this.materialColors.length) {
                throw new Error('submeshMaterial out of bound');
            }
        }
        // Materials
        if (this.materialColors.length % G3d.COLOR_SIZE !== 0) {
            throw new Error('Invalid material color buffer, must be divisible by ' + G3d.COLOR_SIZE);
        }
        console.assert(this.meshInstances.length === this.getMeshCount());
        console.assert(this.meshOpaqueCount.length === this.getMeshCount());
        console.assert(this.meshSubmeshes.length === this.getMeshCount());
        console.assert(this.meshVertexOffsets.length === this.getMeshCount());
        for (let m = 0; m < this.getMeshCount(); m++) {
            console.assert(this.getMeshSubmeshCount(m, 'opaque') +
                this.getMeshSubmeshCount(m, 'transparent') ===
                this.getMeshSubmeshCount(m, 'all'));
            console.assert(this.getMeshIndexCount(m, 'opaque') +
                this.getMeshIndexCount(m, 'transparent') ===
                this.getMeshIndexCount(m, 'all'));
        }
    }
}
exports.G3d = G3d;
G3d.MATRIX_SIZE = 16;
G3d.COLOR_SIZE = 4;
G3d.POSITION_SIZE = 3;
