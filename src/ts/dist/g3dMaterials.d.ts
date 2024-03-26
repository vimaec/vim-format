/**
 * @module vim-ts
 */
import { BFast } from './bfast';
/**
 * See https://github.com/vimaec/vim#vim-geometry-attributes
 */
export declare class MaterialAttributes {
    static materialColors: string;
}
/**
 * G3D is a simple, efficient, generic binary format for storing and transmitting geometry.
 * The G3D format is designed to be used either as a serialization format or as an in-memory data structure.
 * A G3d with specific attributes according to the VIM format specification.
 * See https://github.com/vimaec/vim#vim-geometry-attributes for the vim specification.
 * See https://github.com/vimaec/g3d for the g3d specification.
 */
export declare class G3dMaterial {
    static readonly COLOR_SIZE = 4;
    static readonly DEFAULT_COLOR: Float32Array;
    materialColors: Float32Array;
    constructor(materialColors: Float32Array);
    static createFromBfast(bfast: BFast): Promise<G3dMaterial>;
    getMaterialCount: () => number;
    /**
     * Returns color of given material as a 4-number array (RGBA)
     * @param material g3d material index
     */
    getMaterialColor(material: number): Float32Array;
    getMaterialAlpha(material: number): number;
}
