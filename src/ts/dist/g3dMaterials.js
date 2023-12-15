"use strict";
/**
 * @module vim-ts
 */
Object.defineProperty(exports, "__esModule", { value: true });
exports.G3dMaterial = exports.MaterialAttributes = void 0;
/**
 * See https://github.com/vimaec/vim#vim-geometry-attributes
 */
class MaterialAttributes {
}
exports.MaterialAttributes = MaterialAttributes;
MaterialAttributes.materialColors = 'g3d:material:color:0:float32:4';
/**
 * G3D is a simple, efficient, generic binary format for storing and transmitting geometry.
 * The G3D format is designed to be used either as a serialization format or as an in-memory data structure.
 * A G3d with specific attributes according to the VIM format specification.
 * See https://github.com/vimaec/vim#vim-geometry-attributes for the vim specification.
 * See https://github.com/vimaec/g3d for the g3d specification.
 */
class G3dMaterial {
    constructor(materialColors) {
        this.getMaterialCount = () => this.materialColors.length / G3dMaterial.COLOR_SIZE;
        this.materialColors = materialColors;
    }
    static async createFromBfast(bfast) {
        const mats = await bfast.getFloat32Array(MaterialAttributes.materialColors);
        return new G3dMaterial(mats);
    }
    /**
     * Returns color of given material as a 4-number array (RGBA)
     * @param material g3d material index
     */
    getMaterialColor(material) {
        if (material < 0)
            return G3dMaterial.DEFAULT_COLOR;
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
G3dMaterial.DEFAULT_COLOR = new Float32Array([1, 1, 1, 1]);
