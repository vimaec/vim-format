/**
 * @module vim-ts
 */

import { AbstractG3d } from './abstractG3d'
import { BFast } from './bfast'

/**
 * See https://github.com/vimaec/vim#vim-geometry-attributes
 */
export class MaterialAttributes {
  static materialColors = 'g3d:material:color:0:float32:4'

  static all = [
    MaterialAttributes.materialColors
  ]
}

/**
 * G3D is a simple, efficient, generic binary format for storing and transmitting geometry.
 * The G3D format is designed to be used either as a serialization format or as an in-memory data structure.
 * A G3d with specific attributes according to the VIM format specification.
 * See https://github.com/vimaec/vim#vim-geometry-attributes for the vim specification.
 * See https://github.com/vimaec/g3d for the g3d specification.
 */
export class G3dMaterial {

  static COLOR_SIZE = 4

  rawG3d: AbstractG3d

  materialColors: Float32Array
  DEFAULT_COLOR = new Float32Array([1, 1, 1, 1])

  constructor(

    materialColors: Float32Array){
    this.materialColors = materialColors
  }

  static createFromAbstract(g3d: AbstractG3d) {

    const materialColors = g3d.findAttribute(MaterialAttributes.materialColors)
      ?.data as Float32Array

    const result = new G3dMaterial(

      materialColors
    )
    result.rawG3d = g3d

    return result
  }

  static async createFromPath (path: string) {
    const f = await fetch(path)
    const buffer = await f.arrayBuffer()
    var g3d = this.createFromBuffer(buffer)

    return g3d
  }

  static async createFromBuffer (buffer: ArrayBuffer) {
    const bfast = new BFast(buffer)
    return this.createFromBfast(bfast)
  }

  static async createFromBfast (bfast: BFast) {
    const g3d = await AbstractG3d.createFromBfast(bfast, MaterialAttributes.all)
    return G3dMaterial.createFromAbstract(g3d)
  }

  toG3d(){
    return new G3dMaterial(
      this.materialColors
    )
  }

  getMaterialCount = () => this.materialColors.length / G3dMaterial.COLOR_SIZE

  /**
   * Returns color of given material as a 4-number array (RGBA)
   * @param material g3d material index
   */
  getMaterialColor (material: number): Float32Array {
    if (material < 0) return this.DEFAULT_COLOR
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

