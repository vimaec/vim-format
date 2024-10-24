"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RemoteG3d = exports.RemoteAbstractG3d = void 0;
const bfast_1 = require("./bfast");
const g3d_1 = require("./g3d");
class G3dRemoteAttribute {
    constructor(descriptor, bfast) {
        this.descriptor = descriptor;
        this.bfast = bfast;
    }
    async getAll() {
        const bytes = await this.bfast.getBytes(this.descriptor.description);
        if (!bytes)
            return;
        const data = g3d_1.G3dAttribute.castData(bytes, this.descriptor.dataType);
        return data;
    }
    async getByte(index) {
        return await this.bfast.getValue(this.descriptor.description, index);
    }
    async getNumber(index) {
        return Number(await this.bfast.getValue(this.descriptor.description, index));
    }
    async getValue(index) {
        const value = await this.bfast.getValues(this.descriptor.description, index * this.descriptor.dataArity, this.descriptor.dataArity);
        return value;
    }
    async getValues(index, count) {
        const value = await this.bfast.getValues(this.descriptor.description, index * this.descriptor.dataArity, count * this.descriptor.dataArity);
        return value;
    }
    async getCount() {
        const range = await this.bfast.getRange(this.descriptor.description);
        const count = range.length / (this.descriptor.dataArity * (0, bfast_1.typeSize)(this.descriptor.dataType));
        return count;
    }
    static fromString(description, bfast) {
        return new G3dRemoteAttribute(g3d_1.G3dAttributeDescriptor.fromString(description), bfast);
    }
}
/**
 * G3D is a simple, efficient, generic binary format for storing and transmitting geometry.
 * The G3D format is designed to be used either as a serialization format or as an in-memory data structure.
 * See https://github.com/vimaec/g3d
 */
class RemoteAbstractG3d {
    constructor(meta, attributes) {
        this.meta = meta;
        this.attributes = attributes;
    }
    findAttribute(descriptor) {
        const filter = g3d_1.G3dAttributeDescriptor.fromString(descriptor);
        for (let i = 0; i < this.attributes.length; ++i) {
            const attribute = this.attributes[i];
            if (attribute.descriptor.matches(filter))
                return attribute;
        }
    }
    /**
     * Create g3d from bfast by requesting all necessary buffers individually.
     */
    static createFromBfast(bfast) {
        const attributes = g3d_1.VimAttributes.all.map((a) => G3dRemoteAttribute.fromString(a, bfast));
        return new RemoteAbstractG3d('meta', attributes);
    }
}
exports.RemoteAbstractG3d = RemoteAbstractG3d;
class RemoteG3d {
    constructor(g3d) {
        // ------------- All -----------------
        this.getVertexCount = () => this.positions.getCount();
        // ------------- Meshes -----------------
        this.getMeshCount = () => this.meshSubmeshes.getCount();
        this.getSubmeshCount = () => this.submeshIndexOffsets.getCount();
        this.rawG3d = g3d;
        this.positions = g3d.findAttribute(g3d_1.VimAttributes.positions);
        this.indices = g3d.findAttribute(g3d_1.VimAttributes.indices);
        this.meshSubmeshes = g3d.findAttribute(g3d_1.VimAttributes.meshSubmeshes);
        this.submeshIndexOffsets = g3d.findAttribute(g3d_1.VimAttributes.submeshIndexOffsets);
        this.submeshMaterials = g3d.findAttribute(g3d_1.VimAttributes.submeshMaterials);
        this.materialColors = g3d.findAttribute(g3d_1.VimAttributes.materialColors);
        this.instanceMeshes = g3d.findAttribute(g3d_1.VimAttributes.instanceMeshes);
        this.instanceTransforms = g3d.findAttribute(g3d_1.VimAttributes.instanceTransforms);
        this.instanceFlags =
            g3d.findAttribute(g3d_1.VimAttributes.instanceFlags);
    }
    static createFromBfast(bfast) {
        const abstract = RemoteAbstractG3d.createFromBfast(bfast);
        return new RemoteG3d(abstract);
    }
    async getMeshIndexStart(mesh) {
        const sub = await this.getMeshSubmeshStart(mesh);
        return this.getSubmeshIndexStart(sub);
    }
    async getMeshIndexEnd(mesh) {
        const sub = await this.getMeshSubmeshEnd(mesh);
        return this.getSubmeshIndexEnd(sub - 1);
    }
    async getMeshIndexCount(mesh) {
        const start = await this.getMeshIndexStart(mesh);
        const end = await this.getMeshIndexEnd(mesh);
        return end - start;
    }
    async getMeshIndices(mesh) {
        const start = await this.getMeshIndexStart(mesh);
        const end = await this.getMeshIndexEnd(mesh);
        const indices = await this.indices.getValues(start, end - start);
        return new Uint32Array(indices.buffer);
    }
    async getMeshSubmeshEnd(mesh) {
        const meshCount = await this.getMeshCount();
        const submeshCount = await this.getSubmeshCount();
        return mesh + 1 < meshCount
            ? await this.meshSubmeshes.getNumber(mesh + 1)
            : submeshCount;
    }
    async getMeshSubmeshStart(mesh) {
        return this.meshSubmeshes.getNumber(mesh);
    }
    async getMeshSubmeshCount(mesh) {
        const end = await this.getMeshSubmeshEnd(mesh);
        const start = await this.getMeshSubmeshStart(mesh);
        return end - start;
    }
    // // ------------- Submeshes -----------------
    async getSubmeshIndexStart(submesh) {
        const submeshCount = await this.submeshIndexOffsets.getCount();
        return submesh < submeshCount
            ? this.submeshIndexOffsets.getNumber(submesh)
            : await this.indices.getCount();
    }
    async getSubmeshIndexEnd(submesh) {
        const submeshCount = await this.submeshIndexOffsets.getCount();
        return submesh < submeshCount - 1
            ? this.submeshIndexOffsets.getNumber(submesh + 1)
            : await this.indices.getCount();
    }
    async getSubmeshIndexCount(submesh) {
        const start = await this.getSubmeshIndexStart(submesh);
        const end = await this.getSubmeshIndexEnd(submesh);
        return end - start;
    }
    async toG3d() {
        const attributes = await Promise.all([
            this.instanceMeshes.getAll(),
            this.instanceFlags.getAll(),
            this.instanceTransforms.getAll(),
            Promise.resolve(undefined),
            this.meshSubmeshes.getAll(),
            this.submeshIndexOffsets.getAll(),
            this.submeshMaterials.getAll(),
            this.indices.getAll(),
            this.positions.getAll(),
            this.materialColors.getAll(),
        ]);
        return new g3d_1.G3d(...attributes);
    }
    async slice(instance) {
        return this.filter([instance]);
    }
    async filter(instances) {
        // Instances
        const instanceData = await this.filterInstances(instances);
        // Meshes
        const meshes = await this.filterMesh(instanceData.meshes);
        if (!meshes.hasMeshes)
            return instanceData.toG3d();
        instanceData.remapMeshes(meshes.map);
        const [indiceCount, submeshCount] = await meshes.getAttributeCounts(this);
        let submeshes;
        let materials;
        const A = async () => {
            submeshes = await this.filterSubmeshes(meshes, submeshCount);
            materials = await this.filterMaterials(submeshes.materials);
        };
        let vertices;
        let positions;
        const B = async () => {
            vertices = await this.filterIndices(meshes, indiceCount);
            positions = await this.filterPositions(vertices, meshes);
        };
        await Promise.all([A(), B()]);
        submeshes.remapMaterials(materials.map);
        return new g3d_1.G3d(instanceData.meshes, instanceData.flags, instanceData.transforms, instanceData.nodes, meshes.submeshes, submeshes.indexOffsets, submeshes.materials, vertices.indices, positions, materials.colors);
    }
    async filterInstances(instances) {
        const instanceSet = new Set(instances);
        const attributes = new InstanceData(instanceSet.size);
        let instance_i = 0;
        const instanceCount = await this.instanceMeshes.getCount();
        const promises = [];
        for (let i = 0; i < instanceCount; i++) {
            if (!instanceSet.has(i))
                continue;
            const current = instance_i;
            promises.push(this.instanceFlags.getNumber(i).then(v => attributes.flags[current] = v));
            promises.push(this.instanceMeshes.getNumber(i).then(v => attributes.meshes[current] = v));
            promises.push(this.instanceTransforms.getValue(i).then(v => attributes.transforms.set(v, current * 16)));
            attributes.nodes[current] = i;
            instance_i++;
        }
        await Promise.all(promises);
        return attributes;
    }
    async filterMesh(instanceMeshes) {
        const meshes = new MeshData(instanceMeshes);
        if (meshes.hasMeshes) {
            meshes.originalCount = await this.meshSubmeshes.getCount();
            let last = -1;
            let mesh_i = 0;
            for (let i = 0; i < meshes.originalCount; i++) {
                if (!meshes.set.has(i))
                    continue;
                const offset = mesh_i > 0 ? meshes.submeshes[mesh_i - 1] : 0;
                const lastCount = last < 0 ? 0 : await this.getMeshSubmeshCount(last);
                meshes.submeshes[mesh_i] = lastCount + offset;
                meshes.map.set(i, mesh_i);
                last = i;
                mesh_i++;
            }
        }
        return meshes;
    }
    async filterSubmeshes(meshes, submeshCount) {
        let submesh_i = 0;
        let submeshOffset = 0;
        const submeshes = new SubmeshData(submeshCount);
        for (let mesh = 0; mesh < meshes.originalCount; mesh++) {
            if (!meshes.set.has(mesh))
                continue;
            const subStart = await this.getMeshSubmeshStart(mesh);
            const subEnd = await this.getMeshSubmeshEnd(mesh);
            const promises = [];
            for (let j = subStart; j < subEnd; j++) {
                const current = submesh_i;
                promises.push(this.submeshIndexOffsets.getNumber(subStart)
                    .then(start => this.submeshIndexOffsets.getNumber(j)
                    .then(v => submeshes.indexOffsets[current] = v - start + submeshOffset)));
                promises.push(this.submeshMaterials.getNumber(j).then(v => submeshes.materials[current] = v));
                submesh_i++;
            }
            await Promise.all(promises);
            submeshOffset += await this.getMeshIndexCount(mesh);
        }
        return submeshes;
    }
    async filterIndices(meshes, indicesCount) {
        let indices_i = 0;
        let mesh_i = 0;
        const result = new VertexData(meshes, indicesCount);
        for (let mesh = 0; mesh < meshes.originalCount; mesh++) {
            if (!meshes.set.has(mesh))
                continue;
            const [indexStart, indexEnd] = await Promise.all([
                this.getMeshIndexStart(mesh),
                this.getMeshIndexEnd(mesh)
            ]);
            const indices = await this.indices.getValues(indexStart, indexEnd - indexStart);
            result.indices.set(indices, indices_i);
            let min = Number.MAX_SAFE_INTEGER;
            let max = Number.MIN_SAFE_INTEGER;
            for (let i = 0; i < indices.length; i++) {
                min = Math.min(indices[i], min);
                max = Math.max(indices[i] + 1, max);
            }
            for (let i = 0; i < indices.length; i++) {
                result.indices[indices_i + i] = result.indices[indices_i + i] - min + result.positionCount;
            }
            result.meshVertexStart[mesh_i] = min;
            result.meshVertexEnd[mesh_i] = max;
            result.positionCount += max - min;
            if (mesh_i > 0) {
                const previous = result.vertexOffsets[mesh_i - 1];
                const previousLength = result.meshVertexEnd[mesh_i - 1] - result.meshVertexStart[mesh_i - 1];
                result.vertexOffsets[mesh_i] = previous + previousLength;
            }
            mesh_i++;
            indices_i += indices.length;
        }
        return result;
    }
    async filterPositions(indices, meshes) {
        const _positions = new Float32Array(indices.positionCount * g3d_1.G3d.POSITION_SIZE);
        const promises = [];
        let mesh_i = 0;
        for (let mesh = 0; mesh < meshes.originalCount; mesh++) {
            if (!meshes.set.has(mesh))
                continue;
            const vertexStart = indices.meshVertexStart[mesh_i];
            const vertexEnd = indices.meshVertexEnd[mesh_i];
            const current = mesh_i;
            promises.push(this.positions.getValues(vertexStart, vertexEnd - vertexStart)
                .then(v => _positions.set(v, indices.vertexOffsets[current] * g3d_1.G3d.POSITION_SIZE)));
            mesh_i++;
        }
        await Promise.all(promises);
        return _positions;
    }
    async filterMaterials(submeshMaterials) {
        // Material Colors
        const materialCount = await this.materialColors.getCount();
        let color_i = 0;
        const materials = new MaterialData(submeshMaterials);
        const promises = [];
        for (let i = 0; i < materialCount; i++) {
            if (materials.set.has(i)) {
                materials.map.set(i, color_i);
                const current = color_i;
                promises.push(this.materialColors.getValue(i)
                    .then(c => materials.colors.set(c, current * g3d_1.G3d.COLOR_SIZE)));
                color_i++;
            }
        }
        await Promise.all(promises);
        return materials;
    }
}
exports.RemoteG3d = RemoteG3d;
class InstanceData {
    constructor(count) {
        this.meshes = new Int32Array(count);
        this.flags = new Uint16Array(count);
        this.transforms = new Float32Array(count * 16);
        this.nodes = new Int32Array(count);
    }
    remapMeshes(map) {
        for (let i = 0; i < this.meshes.length; i++) {
            this.meshes[i] = map.get(this.meshes[i]) ?? -1;
        }
    }
    toG3d() {
        return new g3d_1.G3d(this.meshes, this.flags, this.transforms, this.nodes, new Int32Array(), new Int32Array(), new Int32Array(), new Uint32Array(), new Float32Array(), new Float32Array());
    }
}
class MeshData {
    constructor(instanceMeshes) {
        this.set = new Set(instanceMeshes);
        this.set.delete(-1);
        this.hasMeshes = this.set.size > 0;
        this.submeshes = this.hasMeshes ? new Int32Array(this.set.size) : undefined;
        this.map = this.hasMeshes ? new Map() : undefined;
    }
    async getAttributeCounts(g3d) {
        let submeshCount = 0;
        let indiceCount = 0;
        const promises = [];
        for (let mesh = 0; mesh < this.originalCount; mesh++) {
            if (!this.set.has(mesh))
                continue;
            promises.push(g3d.getMeshIndexCount(mesh).then(v => indiceCount += v));
            promises.push(g3d.getMeshSubmeshCount(mesh).then(v => submeshCount += v));
        }
        await Promise.all(promises);
        return [indiceCount, submeshCount];
    }
}
class SubmeshData {
    constructor(count) {
        this.indexOffsets = new Int32Array(count);
        this.materials = new Int32Array(count);
    }
    remapMaterials(map) {
        for (let i = 0; i < this.materials.length; i++) {
            this.materials[i] = this.materials[i] < 0 ? -1 : map.get(this.materials[i]);
        }
    }
}
class VertexData {
    constructor(meshes, indicesCount) {
        this.positionCount = 0;
        this.indices = new Uint32Array(indicesCount);
        this.meshVertexStart = new Int32Array(meshes.set.size);
        this.meshVertexEnd = new Int32Array(meshes.set.size);
        this.vertexOffsets = new Int32Array(meshes.set.size);
    }
}
class MaterialData {
    constructor(submeshMaterials) {
        this.set = new Set(submeshMaterials);
        this.map = new Map();
        this.colors = new Float32Array(this.set.size * g3d_1.G3d.COLOR_SIZE);
    }
}
