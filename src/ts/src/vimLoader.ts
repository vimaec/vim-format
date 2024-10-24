/**
 * @module vim-ts
 */

import { BFast } from "./bfast"

export class VimLoader {
    static async loadFromBfast(bfast: BFast, ignoreStrings: boolean): Promise<[BFast | undefined, string[] | undefined]> {

      const [entity, strings] = await Promise.all([
        
          VimLoader.requestEntities(bfast),
          ignoreStrings ? Promise.resolve(undefined) : VimLoader.requestStrings(bfast)
      ])
      
      return [entity, strings] as [BFast, string[]]
    }

    private static async requestStrings (bfast: BFast) {
      const buffer = await bfast.getBuffer('strings')
        if (!buffer) {
            console.error('Could not get String Data from VIM file. Bim features will be disabled.')
            return
        }
        const strings = new TextDecoder('utf-8').decode(buffer).split('\0')
        return strings
    }

    private static async requestEntities (bfast: BFast) {
        const entities = await bfast.getBfast('entities')
        if (!entities) {
            console.error('Could not get String Data from VIM file. Bim features will be disabled.')
        }
        return entities
    }
}
