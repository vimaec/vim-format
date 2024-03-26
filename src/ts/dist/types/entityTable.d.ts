/**
 * @module vim-ts
 */
import { BFast, NumericArray } from './bfast';
export declare class EntityTable {
    private readonly bfast;
    private readonly strings;
    constructor(bfast: BFast, strings: string[] | undefined);
    getLocal(): Promise<EntityTable>;
    static getTypeSize(colName: string): number;
    getCount(): Promise<number>;
    getArray(columnName: string): Promise<NumericArray | undefined>;
    getNumberArray(columnName: string): Promise<number[] | undefined>;
    getNumber(elementIndex: number, columnName: string): Promise<number | undefined>;
    getBigIntArray(columnName: string): Promise<BigInt64Array | undefined>;
    getBigInt(elementIndex: number, columnName: string): Promise<bigint | undefined>;
    getBoolean(elementIndex: number, columnName: string): Promise<boolean | undefined>;
    getBooleanArray(columnName: string): Promise<boolean[] | undefined>;
    toIndex(value: number | bigint): number;
    getString(elementIndex: number, columnName: string): Promise<string | undefined>;
    getStringArray(columnName: string): Promise<string[] | undefined>;
}
