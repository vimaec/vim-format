
import { G3dAttribute, G3dAttributeDescriptor } from './g3dAttributes'
import { BFast } from './bfast'

/**
 * G3D is a simple, efficient, generic binary format for storing and transmitting geometry.
 * The G3D format is designed to be used either as a serialization format or as an in-memory data structure.
 * See https://github.com/vimaec/g3d
 */
export class AbstractG3d {
  meta: string
  attributes: G3dAttribute[]

  constructor (meta: string, attributes: G3dAttribute[]) {
    this.meta = meta
    this.attributes = attributes
  }

  findAttribute (descriptor: string): G3dAttribute | undefined {
    const filter = G3dAttributeDescriptor.fromString(descriptor)
    for (let i = 0; i < this.attributes.length; ++i) {
      const attribute = this.attributes[i]
      if (attribute.descriptor.matches(filter)) return attribute
    }
  }

  /**
   * Create g3d from bfast by requesting all necessary buffers individually.
   */
  static async createFromBfast (bfast: BFast, names: string[]) {
    
    const attributes = await Promise.all(names.map(async (a) => {
      const bytes = await bfast.getBytes(a)
      if(!bytes) return
      const decriptor = G3dAttributeDescriptor.fromString(a)
      return new G3dAttribute(decriptor, bytes)
    }))

    const validAttributes = attributes.filter((a): a is G3dAttribute => a !== undefined)
    const g3d = new AbstractG3d('meta', validAttributes)
    return g3d
  }
}
