"use strict";
/**
 * @module vim-ts
 */
Object.defineProperty(exports, "__esModule", { value: true });
exports.VimLoader = void 0;
class VimLoader {
    static async loadFromBfast(bfast, ignoreStrings) {
        const [entity, strings] = await Promise.all([
            VimLoader.requestEntities(bfast),
            ignoreStrings ? Promise.resolve(undefined) : VimLoader.requestStrings(bfast)
        ]);
        return [entity, strings];
    }
    static async requestStrings(bfast) {
        const buffer = await bfast.getBuffer('strings');
        if (!buffer) {
            console.error('Could not get String Data from VIM file. Bim features will be disabled.');
            return;
        }
        const strings = new TextDecoder('utf-8').decode(buffer).split('\0');
        return strings;
    }
    static async requestEntities(bfast) {
        const entities = await bfast.getBfast('entities');
        if (!entities) {
            console.error('Could not get String Data from VIM file. Bim features will be disabled.');
        }
        return entities;
    }
}
exports.VimLoader = VimLoader;
