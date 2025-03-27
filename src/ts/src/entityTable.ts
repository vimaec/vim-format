/**
 * @module vim-ts
 */

import { BFast, NumericArray, Range } from './bfast'
import { Vector3, Matrix4x4, Vector4 } from './structures'

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

    static getTypeSize(colName: string): number {
        if (colName.startsWith('index:') ||
            colName.startsWith('string:') ||
            colName.startsWith('int:') ||
            colName.startsWith('uint:') ||
            colName.startsWith('float:')) {
            return 4 // 4 bytes
        }
        
        if (colName.startsWith('double:') ||
            colName.startsWith('long:') ||
            colName.startsWith('ulong')) {
            return 8 // 8 bytes
        }
        
        if (colName.startsWith('byte:') ||
            colName.startsWith('ubyte:')) {
            return 1 // 1 byte
        }

        if (colName.startsWith('short:') ||
            colName.startsWith('ushort:')) {
            return 2 // 2 bytes
        }

        if (colName.startsWith('vector3:'))
        {
            return 4 * 3 // 4 bytes (float) times 3 (x,y,z)
        }

        if (colName.startsWith('matrix4x4:'))
        {
            return 4 * 16 // 4 bytes (float) times 16 (4x4 matrix)
        }

        return 1 // default to 1 byte
    }

    async getCount(): Promise<number> {
        const ranges = await this.bfast.getRanges()
        if (!ranges || ranges.size === 0)
            return 0

        const [colName, range] = ranges.entries().next().value as [string, Range]

        const rangeSize = range.length
        const typeSize = EntityTable.getTypeSize(colName)

        return rangeSize / typeSize
    }

    getArray(columnName: string): Promise<NumericArray | undefined> {
        return this.bfast.getArray(columnName)
    }

    async getNumberArray(columnName: string): Promise<number[] | undefined> {
        const array = await this.bfast.getArray(columnName)

        if (!array || (array instanceof BigInt64Array) || (array instanceof BigUint64Array))
            return undefined

        return Array.from(array)
    }

    async getNumber(elementIndex: number, columnName: string): Promise<number | undefined> {
        const array = await this.bfast.getArray(columnName)

        if ((array?.length ?? -1) <= elementIndex)
            return undefined

        return Number(array![elementIndex])
    }

    async getBigIntArray(columnName: string): Promise<BigInt64Array | undefined> {
        const array = await this.bfast.getArray(columnName)

        if (!array)
            return undefined

        if (array instanceof BigInt64Array)
            return array;

        const result = new BigInt64Array(array.length)
        for (var i = 0; i < array.length; ++i) {
            result[i] = BigInt(array[i])
        }
        return result
    }

    async getBigInt(elementIndex: number, columnName: string): Promise<bigint | undefined> {
        const array = await this.bfast.getArray(columnName)

        if ((array?.length ?? -1) <= elementIndex)
            return undefined

        const element = array![elementIndex]

        if (element === undefined)
            return undefined

        return BigInt(element)
    }

    async getBoolean(elementIndex: number, columnName: string): Promise<boolean | undefined> {
        const array = await this.bfast.getArray(columnName)

        if ((array?.length ?? -1) <= elementIndex)
            return undefined

        const element = array![elementIndex]

        if (element === undefined)
            return undefined

        return Boolean(element)
    }

    async getBooleanArray(columnName: string): Promise<boolean[] | undefined> {
        const array = await this.bfast.getArray(columnName)

        if (!array)
            return undefined

        const result = new Array(array.length)
        for (let i = 0; i < array.length; ++i) {
            result[i] = Boolean(array[i])
        }
        return result
    }

    toIndex(value: number | bigint): number {
        return typeof value === 'bigint'
            ? Number(BigInt.asIntN(32, value)) // clamp to signed integer value
            : value
    }

    async getString(elementIndex: number, columnName: string): Promise<string | undefined> {
        if (this.strings === undefined)
            return undefined

        const array = await this.bfast.getArray(columnName)

        if ((array?.length ?? -1) <= elementIndex)
            return undefined

        return this.strings[this.toIndex(array![elementIndex])]
    }

    async getStringArray(columnName: string): Promise<string[] | undefined> {
        if (this.strings === undefined)
            return undefined

        const array = await this.bfast.getArray(columnName)

        if (!array)
            return undefined

        const result = new Array(array.length)
        for (let i = 0; i < array.length; ++i) {
            result[i] = this.strings[this.toIndex(array[i])]
        }
        return result
    }

    async getVector3(elementIndex: number, columnName: string): Promise<Vector3>
    {
        const arrayBuffer = await this.bfast.getBuffer(columnName)
        const floatView = new Float32Array(arrayBuffer)

        const startIndex = elementIndex * 3; // (x,y,z) => 3
        return {
          x: floatView[startIndex],
          y: floatView[startIndex + 1],
          z: floatView[startIndex + 2]
        }
    }

    // TODO: Promise<any> is a shortcut... should actually be Promise<ArrayBuffer>
    async getVector3Array(columnName: string): Promise<any> {
        return await this.bfast.getBuffer(columnName)
    }

    async getVector4(elementIndex: number, columnName: string): Promise<Vector4>
    {
        const arrayBuffer = await this.bfast.getBuffer(columnName)
        const floatView = new Float32Array(arrayBuffer)

        const startIndex = elementIndex * 4; // (x,y,z,w) => 4
        return {
          x: floatView[startIndex],
          y: floatView[startIndex + 1],
          z: floatView[startIndex + 2],
          w: floatView[startIndex + 3]
        }
    }

    // TODO: Promise<any> is a shortcut... should actually be Promise<ArrayBuffer>
    async getVector4Array(columnName: string): Promise<any> {
        return await this.bfast.getBuffer(columnName)
    }

    async getMatrix4x4(elementIndex: number, columnName: string): Promise<Matrix4x4>
    {
        const arrayBuffer = await this.bfast.getBuffer(columnName)
        const floatView = new Float32Array(arrayBuffer)

        const startIndex = elementIndex * 16; // (4x4) => 16
        return {
            m11: floatView[startIndex],
            m12: floatView[startIndex + 1],
            m13: floatView[startIndex + 2],
            m14: floatView[startIndex + 3],
            m21: floatView[startIndex + 4],
            m22: floatView[startIndex + 5],
            m23: floatView[startIndex + 6],
            m24: floatView[startIndex + 7],
            m31: floatView[startIndex + 8],
            m32: floatView[startIndex + 9],
            m33: floatView[startIndex + 10],
            m34: floatView[startIndex + 11],
            m41: floatView[startIndex + 12],
            m42: floatView[startIndex + 13],
            m43: floatView[startIndex + 14],
            m44: floatView[startIndex + 15],
        }
    }

    // TODO: Promise<any> is a shortcut... should actually be Promise<ArrayBuffer>
    async getMatrix4x4Array(columnName: string): Promise<any> {
        return await this.bfast.getBuffer(columnName)
    }
}
