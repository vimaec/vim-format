/**
 * @module vim-ts
 */

import { BFast } from '../bfast'

/**
 * See https://github.com/vimaec/vim#vim-geometry-attributes
 */
export class MaterialAttributes {
  static materialColors = 'g3d:material:color:0:float32:4'
}

/**
 * G3D is a simple, efficient, generic binary format for storing and transmitting geometry.
 * The G3D format is designed to be used either as a serialization format or as an in-memory data structure.
 * A G3d with specific attributes according to the VIM format specification.
 * See https://github.com/vimaec/vim#vim-geometry-attributes for the vim specification.
 * See https://github.com/vimaec/g3d for the g3d specification.
 */
export class G3dMaterial {
  static readonly COLOR_SIZE = 4
  static readonly DEFAULT_COLOR = new Float32Array([1, 1, 1, 1])

  materialColors: Float32Array

  constructor(materialColors: Float32Array){
    this.materialColors = materialColors
  }

  static async createFromBfast (bfast: BFast) {
    const mats = await bfast.getFloat32Array(MaterialAttributes.materialColors)
    return new G3dMaterial(mats)
  }

  getMaterialCount = () => this.materialColors.length / G3dMaterial.COLOR_SIZE

  /**
   * Returns color of given material as a 4-number array (RGBA)
   * @param material g3d material index
   */
  getMaterialColor (material: number): Float32Array {
    if (material < 0) return G3dMaterial.DEFAULT_COLOR
    return this.materialColors.subarray(
      material * G3dMaterial.COLOR_SIZE,
      (material + 1) * G3dMaterial.COLOR_SIZE
    )
  }

  getMaterialAlpha (material: number): number {
    if (material < 0) return 1
    const index = material * G3dMaterial.COLOR_SIZE + G3dMaterial.COLOR_SIZE - 1
    const result = this.materialColors[index]
    return result
  }
}

