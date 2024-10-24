"use strict";
/**
 * @module vim-ts
 */
Object.defineProperty(exports, "__esModule", { value: true });
exports.EntityTable = void 0;
class EntityTable {
    constructor(bfast, strings) {
        this.bfast = bfast;
        this.strings = strings;
    }
    async getLocal() {
        return new EntityTable(await this.bfast.getSelf(), this.strings);
    }
    static getTypeSize(colName) {
        if (colName.startsWith('index:') ||
            colName.startsWith('string:') ||
            colName.startsWith('int:') ||
            colName.startsWith('uint:') ||
            colName.startsWith('float:')) {
            return 4; // 4 bytes
        }
        if (colName.startsWith('double:') ||
            colName.startsWith('long:') ||
            colName.startsWith('ulong')) {
            return 8; // 8 bytes
        }
        if (colName.startsWith('byte:') ||
            colName.startsWith('ubyte:')) {
            return 1; // 1 byte
        }
        if (colName.startsWith('short:') ||
            colName.startsWith('ushort:')) {
            return 2; // 2 bytes
        }
        return 1; // default to 1 byte
    }
    async getCount() {
        const ranges = await this.bfast.getRanges();
        if (!ranges || ranges.size === 0)
            return 0;
        const [colName, range] = ranges.entries().next().value;
        const rangeSize = range.length;
        const typeSize = EntityTable.getTypeSize(colName);
        return rangeSize / typeSize;
    }
    getArray(columnName) {
        return this.bfast.getArray(columnName);
    }
    async getNumberArray(columnName) {
        const array = await this.bfast.getArray(columnName);
        if (!array || (array instanceof BigInt64Array) || (array instanceof BigUint64Array))
            return undefined;
        return Array.from(array);
    }
    async getNumber(elementIndex, columnName) {
        const array = await this.bfast.getArray(columnName);
        if ((array?.length ?? -1) <= elementIndex)
            return undefined;
        return Number(array[elementIndex]);
    }
    async getBigIntArray(columnName) {
        const array = await this.bfast.getArray(columnName);
        if (!array)
            return undefined;
        if (array instanceof BigInt64Array)
            return array;
        const result = new BigInt64Array(array.length);
        for (var i = 0; i < array.length; ++i) {
            result[i] = BigInt(array[i]);
        }
        return result;
    }
    async getBigInt(elementIndex, columnName) {
        const array = await this.bfast.getArray(columnName);
        if ((array?.length ?? -1) <= elementIndex)
            return undefined;
        const element = array[elementIndex];
        if (element === undefined)
            return undefined;
        return BigInt(element);
    }
    async getBoolean(elementIndex, columnName) {
        const array = await this.bfast.getArray(columnName);
        if ((array?.length ?? -1) <= elementIndex)
            return undefined;
        const element = array[elementIndex];
        if (element === undefined)
            return undefined;
        return Boolean(element);
    }
    async getBooleanArray(columnName) {
        const array = await this.bfast.getArray(columnName);
        if (!array)
            return undefined;
        const result = new Array(array.length);
        for (let i = 0; i < array.length; ++i) {
            result[i] = Boolean(array[i]);
        }
        return result;
    }
    toIndex(value) {
        return typeof value === 'bigint'
            ? Number(BigInt.asIntN(32, value)) // clamp to signed integer value
            : value;
    }
    async getString(elementIndex, columnName) {
        if (this.strings === undefined)
            return undefined;
        const array = await this.bfast.getArray(columnName);
        if ((array?.length ?? -1) <= elementIndex)
            return undefined;
        return this.strings[this.toIndex(array[elementIndex])];
    }
    async getStringArray(columnName) {
        if (this.strings === undefined)
            return undefined;
        const array = await this.bfast.getArray(columnName);
        if (!array)
            return undefined;
        const result = new Array(array.length);
        for (let i = 0; i < array.length; ++i) {
            result[i] = this.strings[this.toIndex(array[i])];
        }
        return result;
    }
}
exports.EntityTable = EntityTable;
