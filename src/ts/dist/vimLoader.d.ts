/**
 * @module vim-ts
 */
import { BFast } from "./bfast";
export declare class VimLoader {
    static loadFromBfast(bfast: BFast, download: boolean, ignoreStrings: boolean): Promise<[BFast | undefined, string[] | undefined]>;
    private static requestStrings;
    private static requestEntities;
}
