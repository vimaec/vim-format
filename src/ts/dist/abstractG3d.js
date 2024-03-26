"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AbstractG3d = void 0;
const g3dAttributes_1 = require("./g3dAttributes");
/**
 * G3D is a simple, efficient, generic binary format for storing and transmitting geometry.
 * The G3D format is designed to be used either as a serialization format or as an in-memory data structure.
 * See https://github.com/vimaec/g3d
 */
class AbstractG3d {
    constructor(meta, attributes) {
        this.meta = meta;
        this.attributes = attributes;
    }
    findAttribute(descriptor) {
        const filter = g3dAttributes_1.G3dAttributeDescriptor.fromString(descriptor);
        for (let i = 0; i < this.attributes.length; ++i) {
            const attribute = this.attributes[i];
            if (attribute.descriptor.matches(filter))
                return attribute;
        }
    }
    /**
     * Create g3d from bfast by requesting all necessary buffers individually.
     */
    static async createFromBfast(bfast, names) {
        const attributes = await Promise.all(names.map(async (a) => {
            const bytes = await bfast.getBytes(a);
            if (!bytes)
                return;
            const decriptor = g3dAttributes_1.G3dAttributeDescriptor.fromString(a);
            return new g3dAttributes_1.G3dAttribute(decriptor, bytes);
        }));
        const validAttributes = attributes.filter((a) => a !== undefined);
        const g3d = new AbstractG3d('meta', validAttributes);
        return g3d;
    }
}
exports.AbstractG3d = AbstractG3d;
