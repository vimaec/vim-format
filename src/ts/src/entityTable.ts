/**
 * @module vim-ts
 */

import { BFast } from './bfast'

export class EntityTable {
    private readonly bfast: BFast
    private readonly strings: string[] | undefined

    constructor(bfast: BFast, strings: string[] | undefined) {
        this.bfast = bfast
        this.strings = strings
    }

    async getLocal() {
        return new EntityTable(await this.bfast.getSelf(), this.strings)
    }

    getArray(columnName: string): Promise<number[] | undefined> {
        return this.bfast.getArray(columnName)
    }

    async getNumber(elementIndex: number, columnName: string): Promise<number | undefined> {
        const array = await this.bfast.getArray(columnName)

        if ((array?.length ?? -1) <= elementIndex)
            return undefined

        return array![elementIndex]
    }

    async getBoolean(elementIndex: number, columnName: string): Promise<boolean | undefined> {
        const array = await this.bfast.getArray(columnName)

        if ((array?.length ?? -1) <= elementIndex)
            return undefined

        const element = array![elementIndex]

        if (element === undefined)
            return undefined

        return element === 1
    }

    async getBooleanArray(columnName: string): Promise<boolean[] | undefined> {
        const array = await this.bfast.getArray(columnName)

        if (!array)
            return undefined

        return array.map(n => n === 1)
    }

    async getString(elementIndex: number, columnName: string): Promise<string | undefined> {
        if (this.strings === undefined)
            return undefined

        const array = await this.bfast.getArray(columnName)

        if ((array?.length ?? -1) <= elementIndex)
            return undefined

        return this.strings[array![elementIndex]]
    }

    async getStringArray(columnName: string): Promise<string[] | undefined> {
        if (this.strings === undefined)
            return undefined

        const array = await this.bfast.getArray(columnName)

        if (!array)
            return undefined

        return array.map(n => this.strings[n])
    }
}
