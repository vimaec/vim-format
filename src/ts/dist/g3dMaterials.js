"use strict";
/**
 * @module vim-ts
 */
Object.defineProperty(exports, "__esModule", { value: true });
exports.G3dMaterial = exports.MaterialAttributes = void 0;
const abstractG3d_1 = require("./abstractG3d");
const bfast_1 = require("./bfast");
/**
 * See https://github.com/vimaec/vim#vim-geometry-attributes
 */
class MaterialAttributes {
}
exports.MaterialAttributes = MaterialAttributes;
MaterialAttributes.materialColors = 'g3d:material:color:0:float32:4';
MaterialAttributes.all = [
    MaterialAttributes.materialColors
];
/**
 * G3D is a simple, efficient, generic binary format for storing and transmitting geometry.
 * The G3D format is designed to be used either as a serialization format or as an in-memory data structure.
 * A G3d with specific attributes according to the VIM format specification.
 * See https://github.com/vimaec/vim#vim-geometry-attributes for the vim specification.
 * See https://github.com/vimaec/g3d for the g3d specification.
 */
class G3dMaterial {
    constructor(materialColors) {
        this.DEFAULT_COLOR = new Float32Array([1, 1, 1, 1]);
        this.getMaterialCount = () => this.materialColors.length / G3dMaterial.COLOR_SIZE;
        this.materialColors = materialColors;
    }
    static createFromAbstract(g3d) {
        const materialColors = g3d.findAttribute(MaterialAttributes.materialColors)
            ?.data;
        const result = new G3dMaterial(materialColors);
        result.rawG3d = g3d;
        return result;
    }
    static async createFromPath(path) {
        const f = await fetch(path);
        const buffer = await f.arrayBuffer();
        var g3d = this.createFromBuffer(buffer);
        return g3d;
    }
    static async createFromBuffer(buffer) {
        const bfast = new bfast_1.BFast(buffer);
        return this.createFromBfast(bfast);
    }
    static async createFromBfast(bfast) {
        const g3d = await abstractG3d_1.AbstractG3d.createFromBfast(bfast, MaterialAttributes.all);
        return G3dMaterial.createFromAbstract(g3d);
    }
    toG3d() {
        return new G3dMaterial(this.materialColors);
    }
    /**
     * Returns color of given material as a 4-number array (RGBA)
     * @param material g3d material index
     */
    getMaterialColor(material) {
        if (material < 0)
            return this.DEFAULT_COLOR;
        return this.materialColors.subarray(material * G3dMaterial.COLOR_SIZE, (material + 1) * G3dMaterial.COLOR_SIZE);
    }
    getMaterialAlpha(material) {
        if (material < 0)
            return 1;
        const index = material * G3dMaterial.COLOR_SIZE + G3dMaterial.COLOR_SIZE - 1;
        const result = this.materialColors[index];
        return result;
    }
}
exports.G3dMaterial = G3dMaterial;
G3dMaterial.COLOR_SIZE = 4;
