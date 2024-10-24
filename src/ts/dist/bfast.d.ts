/**
 * @module vim-ts
 */
import { RemoteBuffer } from './http/remoteBuffer';
export declare type BFastSource = {
    url?: string;
    headers?: Record<string, string>;
    buffer?: RemoteBuffer | ArrayBuffer;
};
declare type NumericArrayConstructor = Int8ArrayConstructor | Uint8ArrayConstructor | Int16ArrayConstructor | Uint16ArrayConstructor | Int32ArrayConstructor | Uint32ArrayConstructor | BigInt64ArrayConstructor | BigUint64ArrayConstructor | Float32ArrayConstructor | Float64ArrayConstructor;
export declare type NumericArray = Int8Array | Uint8Array | Int16Array | Uint16Array | Int32Array | Uint32Array | Float32Array | Float64Array | BigInt64Array | BigUint64Array;
export declare class Range {
    start: number;
    end: number;
    get length(): number;
    constructor(start: number, end: number);
    offset(offset: number): Range;
}
/**
 * Returns -1 size for undefined attributes.
 */
export declare function parseName(name: string): [number, NumericArrayConstructor];
export declare function typeSize(type: string): 1 | 2 | 4 | 8;
/**
 * Bfast header, mainly for validation.
 * See https://github.com/vimaec/bfast
 */
export declare class BFastHeader {
    magic: number;
    dataStart: number;
    dataEnd: number;
    numArrays: number;
    constructor(magic: number, dataStart: number, dataEnd: number, numArrays: number);
    static createFromArray(array: Uint32Array): BFastHeader;
    static createFromBuffer(array: ArrayBuffer): BFastHeader;
}
/**
 * See https://github.com/vimaec/bfast for bfast format spec
 * This implementation can either lazily request content as needed from http
 * Or it can serve the data directly from an ArrayBuffer
 * Remote mode can transition to buffer mode if server doesnt support partial http request
 */
export declare class BFast {
    source: RemoteBuffer | ArrayBuffer;
    offset: number;
    name: string;
    private _header;
    private _ranges;
    private _children;
    constructor(source: BFastSource, offset?: number, name?: string);
    /**
     * @returns url of the underlying RemoteBuffer if available
     */
    get url(): string;
    /**
     * Aborts all downloads from the underlying RemoteBuffer
     */
    abort(): void;
    /**
     * @returns Bfast Header
     */
    getHeader(): Promise<BFastHeader>;
    /**
     * @returns a map of all buffers by names
     */
    getRanges(): Promise<Map<string, Range>>;
    /**
     * Returns the buffer associated with name as a new bfast.
     * This value is cached for future requests.
     * @param name buffer name
     */
    getBfast(name: string): Promise<BFast>;
    getLocalBfast(name: string, inflate?: boolean): Promise<BFast | undefined>;
    /**
     * Returns the raw buffer associated with a name
     * This value is not cached.
     * @param name buffer name
     */
    getBuffer(name: string): Promise<ArrayBuffer | undefined>;
    /**
     * Returns a number array from the buffer associated with name
     * @param name buffer name
     */
    getArray(name: string): Promise<NumericArray | undefined>;
    getInt32Array(name: string): Promise<Int32Array | undefined>;
    getFloat32Array(name: string): Promise<Float32Array | undefined>;
    getBigInt64Array(name: string): Promise<BigInt64Array | undefined>;
    getUint16Array(name: string): Promise<Uint16Array | undefined>;
    /**
     * Returns a single value from given buffer name
     * @param name buffer name
     * @param index row index
     */
    getValue(name: string, index: number): Promise<number | BigInt | undefined>;
    getRange(name: string): Promise<Range>;
    /**
     * Returns count subsequent values from given buffer name.
     * @param name buffer name
     * @param index row index
     * @param count count of values to return
     */
    getValues(name: string, index: number, count: number): Promise<NumericArray | undefined>;
    /**
     
      * Returns the buffer with given name as a byte array
      * @param name buffer name
      */
    getBytes(name: string): Promise<Uint8Array | undefined>;
    /**
     * Returns a map of name-values with the same index from all buffers.
     * @param name buffer name
     */
    getRow(index: number): Promise<Map<string, number | BigInt | undefined> | undefined>;
    /**
     * Forces download of the full underlying buffer, from now on all calls will be local.
     */
    forceDownload(): Promise<void>;
    /**
     * Downloads the appropriate range and cast it as a new sub bfast.
     */
    private requestBfast;
    /**
     * Downloads and parses ranges as a map of name->range
     */
    private requestRanges;
    /**
     * Downloads and parse names from remote.
     */
    private requestNames;
    /**
     * Downloads and parse header from remote.
     */
    private requestHeader;
    /**
     * Gets array buffer from from cache, or partial http, fallback to full http
     * @param range range to get, or get full resource if undefined
     * @param label label for logs
     */
    private request;
    /**
     * returns requested range from cache.
     */
    private local;
    /**
     * returns requested range from remote.
     */
    private remote;
    /**
     * Returns a new local bfast equivalent to this bfast.
     */
    getSelf(): Promise<BFast>;
}
export {};
