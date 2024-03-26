import { G3dAttribute } from './g3dAttributes';
import { BFast } from './bfast';
/**
 * G3D is a simple, efficient, generic binary format for storing and transmitting geometry.
 * The G3D format is designed to be used either as a serialization format or as an in-memory data structure.
 * See https://github.com/vimaec/g3d
 */
export declare class AbstractG3d {
    meta: string;
    attributes: G3dAttribute[];
    constructor(meta: string, attributes: G3dAttribute[]);
    findAttribute(descriptor: string): G3dAttribute | undefined;
    /**
     * Create g3d from bfast by requesting all necessary buffers individually.
     */
    static createFromBfast(bfast: BFast, names: string[]): Promise<AbstractG3d>;
}
