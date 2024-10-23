"use strict";
/**
 * @module vim-ts
 */
var __createBinding = (this && this.__createBinding) || (Object.create ? (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    var desc = Object.getOwnPropertyDescriptor(m, k);
    if (!desc || ("get" in desc ? !m.__esModule : desc.writable || desc.configurable)) {
      desc = { enumerable: true, get: function() { return m[k]; } };
    }
    Object.defineProperty(o, k2, desc);
}) : (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    o[k2] = m[k];
}));
var __setModuleDefault = (this && this.__setModuleDefault) || (Object.create ? (function(o, v) {
    Object.defineProperty(o, "default", { enumerable: true, value: v });
}) : function(o, v) {
    o["default"] = v;
});
var __importStar = (this && this.__importStar) || function (mod) {
    if (mod && mod.__esModule) return mod;
    var result = {};
    if (mod != null) for (var k in mod) if (k !== "default" && Object.prototype.hasOwnProperty.call(mod, k)) __createBinding(result, mod, k);
    __setModuleDefault(result, mod);
    return result;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.BFast = exports.BFastHeader = exports.typeSize = exports.parseName = exports.Range = void 0;
const remoteValue_1 = require("./http/remoteValue");
const remoteBuffer_1 = require("./http/remoteBuffer");
const pako = __importStar(require("pako"));
class Range {
    constructor(start, end) {
        this.start = start;
        this.end = end;
    }
    get length() {
        return this.end - this.start;
    }
    offset(offset) {
        return new Range(this.start + offset, this.end + offset);
    }
}
exports.Range = Range;
/**
 * Returns -1 size for undefined attributes.
 */
function parseName(name) {
    if (name.startsWith('g3d')) {
        const result = name.includes(':int8:') ? [1, Int8Array]
            : name.includes(':uint8:') ? [1, Uint8Array]
                : name.includes(':int16:') ? [2, Int16Array]
                    : name.includes(':uint16:') ? [2, Uint16Array]
                        : name.includes(':int32:') ? [4, Int32Array]
                            : name.includes(':uint32:') ? [4, Uint32Array]
                                : name.includes(':int64:') ? [8, BigInt64Array]
                                    : name.includes(':uint64:') ? [8, BigUint64Array]
                                        : name.includes(':float32:') ? [4, Float32Array]
                                            : name.includes(':float64:') ? [8, Float64Array]
                                                : [-1, undefined];
        return result;
    }
    else {
        const result = name.startsWith('byte:') ? [1, Int8Array]
            : name.startsWith('ubyte:') ? [1, Uint8Array]
                : name.startsWith('short:') ? [2, Int16Array]
                    : name.startsWith('ushort:') ? [2, Uint16Array]
                        : name.startsWith('int:') ? [4, Int32Array]
                            : name.startsWith('uint:') ? [4, Uint32Array]
                                : name.startsWith('long:') ? [8, BigInt64Array]
                                    : name.startsWith('ulong:') ? [8, BigUint64Array]
                                        : name.startsWith('float:') ? [4, Float32Array]
                                            : name.startsWith('double:') ? [8, Float64Array]
                                                : [-1, undefined];
        return result;
    }
}
exports.parseName = parseName;
function typeSize(type) {
    switch (type) {
        case 'byte':
        case 'ubyte':
            return 1;
        case 'short':
        case 'ushort':
            return 2;
        case 'int':
        case 'uint':
        case 'float':
            return 4;
        case 'long':
        case 'ulong':
        case 'double':
            return 8;
        default:
            return 4;
    }
}
exports.typeSize = typeSize;
function typeConstructor(type) {
    switch (type) {
        case 'byte':
            return Int8Array;
        case 'ubyte':
            return Uint8Array;
        case 'short':
            return Int16Array;
        case 'ushort':
            return Uint16Array;
        case 'int':
            return Int32Array;
        case 'uint':
            return Uint32Array;
        case 'long':
            return BigInt64Array;
        case 'ulong':
            return BigUint64Array;
        case 'float':
            return Float32Array;
        case 'double':
            return Float64Array;
        default:
            return Int32Array;
    }
}
/**
 * Bfast header, mainly for validation.
 * See https://github.com/vimaec/bfast
 */
class BFastHeader {
    constructor(magic, dataStart, dataEnd, numArrays) {
        if (magic !== 0xbfa5) {
            throw new Error('Invalid Bfast. Invalid Magic number');
        }
        if (dataStart <= 32 || dataStart > Number.MAX_SAFE_INTEGER) {
            throw new Error('Invalid Bfast. Data start is out of valid range');
        }
        if (dataEnd < dataStart || dataEnd > Number.MAX_SAFE_INTEGER) {
            throw new Error('Invalid Bfast. Data end is out of valid range');
        }
        if (numArrays < 0 || numArrays > dataEnd) {
            throw new Error('Invalid Bfast. Number of arrays is invalid');
        }
        this.magic = magic;
        this.dataStart = dataStart;
        this.dataEnd = dataEnd;
        this.numArrays = numArrays;
    }
    static createFromArray(array) {
        // Check validity of data
        // TODO: check endianness
        if (array[1] !== 0) {
            throw new Error('Invalid Bfast. Expected 0 in byte position 0');
        }
        if (array[3] !== 0) {
            throw new Error('Invalid Bfast. Expected 0 in byte position 8');
        }
        if (array[5] !== 0) {
            throw new Error('Invalid Bfast. Expected 0 in position 16');
        }
        if (array[7] !== 0) {
            throw new Error('Invalid Bfast. Expected 0 in position 24');
        }
        return new this(array[0], array[2], array[4], array[6]);
    }
    static createFromBuffer(array) {
        return BFastHeader.createFromArray(new Uint32Array(array));
    }
}
exports.BFastHeader = BFastHeader;
/**
 * See https://github.com/vimaec/bfast for bfast format spec
 * This implementation can either lazily request content as needed from http
 * Or it can serve the data directly from an ArrayBuffer
 * Remote mode can transition to buffer mode if server doesnt support partial http request
 */
class BFast {
    constructor(source, offset = 0, name = '') {
        this.source = source.buffer
            ? source.buffer
            : new remoteBuffer_1.RemoteBuffer(source.url, source.headers);
        this.offset = offset;
        this.name = name ?? "root";
        this._header = new remoteValue_1.RemoteValue(() => this.requestHeader(), name + '.header');
        this._children = new Map();
        this._ranges = new remoteValue_1.RemoteValue(() => this.requestRanges(), name + '.ranges');
    }
    /**
     * @returns url of the underlying RemoteBuffer if available
     */
    get url() {
        return this.source instanceof remoteBuffer_1.RemoteBuffer ? this.source.url : undefined;
    }
    /**
     * Aborts all downloads from the underlying RemoteBuffer
     */
    abort() {
        if (this.source instanceof remoteBuffer_1.RemoteBuffer) {
            this.source.abort();
        }
        this._header.abort();
        this._ranges.abort();
        this._children.forEach(c => c.abort());
    }
    /**
     * @returns Bfast Header
     */
    async getHeader() {
        return this._header.get();
    }
    /**
     * @returns a map of all buffers by names
     */
    async getRanges() {
        return this._ranges.get();
    }
    /**
     * Returns the buffer associated with name as a new bfast.
     * This value is cached for future requests.
     * @param name buffer name
     */
    async getBfast(name) {
        let request = this._children.get(name);
        if (!request) {
            request = new remoteValue_1.RemoteValue(() => this.requestBfast(name));
            this._children.set(name, request);
        }
        return request.get();
    }
    async getLocalBfast(name, inflate = false) {
        let buffer = await this.getBuffer(name);
        if (!buffer)
            return undefined;
        if (inflate) {
            buffer = pako.inflateRaw(buffer).buffer;
        }
        return new BFast({ buffer }, 0, name);
    }
    /**
     * Returns the raw buffer associated with a name
     * This value is not cached.
     * @param name buffer name
     */
    async getBuffer(name) {
        const ranges = await this.getRanges();
        const range = ranges.get(name);
        if (!range)
            return undefined;
        const buffer = await this.request(range, name);
        return buffer;
    }
    /**
     * Returns a number array from the buffer associated with name
     * @param name buffer name
     */
    async getArray(name) {
        const buffer = await this.getBuffer(name);
        if (!buffer)
            return undefined;
        const type = name.split(':')[0];
        const Ctor = typeConstructor(type);
        const array = new Ctor(buffer);
        return array;
    }
    async getInt32Array(name) {
        const buffer = await this.getBuffer(name);
        if (!buffer)
            return;
        return new Int32Array(buffer);
    }
    async getFloat32Array(name) {
        const buffer = await this.getBuffer(name);
        if (!buffer)
            return;
        return new Float32Array(buffer);
    }
    async getBigInt64Array(name) {
        const buffer = await this.getBuffer(name);
        if (!buffer)
            return;
        return new BigInt64Array(buffer);
    }
    async getUint16Array(name) {
        const buffer = await this.getBuffer(name);
        if (!buffer)
            return;
        return new Uint16Array(buffer);
    }
    /**
     * Returns a single value from given buffer name
     * @param name buffer name
     * @param index row index
     */
    async getValue(name, index) {
        const array = await this.getValues(name, index, 1);
        return array?.[0];
    }
    async getRange(name) {
        const ranges = await this.getRanges();
        return ranges.get(name);
    }
    /**
     * Returns count subsequent values from given buffer name.
     * @param name buffer name
     * @param index row index
     * @param count count of values to return
     */
    async getValues(name, index, count) {
        if (index < 0 || count < 1)
            return undefined;
        const range = await this.getRange(name);
        if (!range)
            return undefined;
        const [size, ctor] = parseName(name);
        if (size < 0)
            return undefined;
        const start = Math.min(range.start + index * size, range.end);
        const end = Math.min(start + size * count, range.end);
        const dataRange = new Range(start, end);
        if (dataRange.length <= 0)
            return undefined;
        const buffer = await this.request(dataRange, `${name}[${index.toString()}]`);
        if (!buffer)
            return undefined;
        const array = new ctor(buffer);
        return array;
    }
    /**
     
      * Returns the buffer with given name as a byte array
      * @param name buffer name
      */
    async getBytes(name) {
        const buffer = await this.getBuffer(name);
        if (!buffer)
            return undefined;
        const array = new Uint8Array(buffer);
        return array;
    }
    /**
     * Returns a map of name-values with the same index from all buffers.
     * @param name buffer name
     */
    async getRow(index) {
        const ranges = await this.getRanges();
        if (!ranges)
            return undefined;
        const result = new Map();
        const promises = [];
        for (const name of ranges.keys()) {
            const p = this.getValue(name, index).then((v) => result.set(name, v));
            promises.push(p);
        }
        await Promise.all(promises);
        return result;
    }
    /**
     * Forces download of the full underlying buffer, from now on all calls will be local.
     */
    async forceDownload() {
        if (this.source instanceof ArrayBuffer) {
            console.log('Ignoring forceDownload on local buffer.');
            return;
        }
        const buffer = await this.remote(undefined, this.name);
        if (!buffer)
            throw new Error('Failed to download BFAST.');
        this.source = buffer;
    }
    /**
     * Downloads the appropriate range and cast it as a new sub bfast.
     */
    async requestBfast(name) {
        const ranges = await this.getRanges();
        const range = ranges.get(name);
        if (!range)
            return undefined;
        const result = new BFast({ buffer: this.source }, this.offset + range.start, this.name + '.' + name);
        return result;
    }
    /**
     * Downloads and parses ranges as a map of name->range
     */
    async requestRanges() {
        const header = await this.getHeader();
        const buffer = await this.request(new Range(32, 32 + header.numArrays * 16), 'Ranges');
        if (!buffer)
            throw new Error('Could not get BFAST Ranges.');
        // Parse range
        const array = new Uint32Array(buffer);
        const ranges = [];
        for (let i = 0; i < array.length; i += 4) {
            if (array[i + 1] !== 0 || array[i + 3] !== 0) {
                throw new Error('Invalid Bfast. 64 bit ranges not supported');
            }
            ranges.push(new Range(array[i], array[i + 2]));
        }
        const names = await this.requestNames(ranges[0]);
        if (ranges.length !== names.length + 1) {
            throw new Error('Mismatched ranges and names count');
        }
        // Map ranges and names
        const map = new Map();
        for (let i = 0; i < names.length; i++) {
            map.set(names[i], ranges[i + 1]);
        }
        return map;
    }
    /**
     * Downloads and parse names from remote.
     */
    async requestNames(range) {
        const buffer = await this.request(range, 'Names');
        const names = new TextDecoder('utf-8').decode(buffer);
        const result = names.slice(0, -1).split('\0');
        return result;
    }
    /**
     * Downloads and parse header from remote.
     */
    async requestHeader() {
        const buffer = await this.request(new Range(0, 32), 'Header');
        if (!buffer)
            throw new Error('Could not get BFAST Header');
        const result = BFastHeader.createFromBuffer(buffer);
        return result;
    }
    /**
     * Gets array buffer from from cache, or partial http, fallback to full http
     * @param range range to get, or get full resource if undefined
     * @param label label for logs
     */
    async request(range, label) {
        const buffer = this.local(range, label) ??
            (await this.remote(range, label)) ??
            (await this.remote(undefined, label));
        if (!buffer) {
            throw new Error(`Could not load vim at ${this.source}`);
        }
        if (buffer.byteLength > range.length) {
            this.source = buffer;
            return this.local(range, label);
        }
        return buffer;
    }
    /**
     * returns requested range from cache.
     */
    local(range, label) {
        if (!(this.source instanceof ArrayBuffer))
            return undefined;
        //console.log(`Returning local ${this.name}.${label}`)
        const r = range.offset(this.offset);
        return this.source.slice(r.start, r.end);
    }
    /**
     * returns requested range from remote.
     */
    async remote(range, label) {
        if (!(this.source instanceof remoteBuffer_1.RemoteBuffer))
            return undefined;
        const r = range?.offset(this.offset);
        const buffer = await this.source.http(r, `${this.name}.${label}`);
        if (range && (buffer?.byteLength ?? 0) < range.length) {
            console.log('Range request request failed.');
            return undefined;
        }
        return buffer;
    }
    /**
     * Returns a new local bfast equivalent to this bfast.
     */
    async getSelf() {
        const header = await this._header.get();
        const range = new Range(0, header.dataEnd);
        const buffer = await this.request(range, this.name);
        const result = new BFast({ buffer }, 0, this.name);
        return result;
    }
}
exports.BFast = BFast;
