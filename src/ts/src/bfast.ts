/**
 * @module vim-ts
 */

 import { RemoteValue } from './http/remoteValue'
 import { RemoteBuffer } from './http/remoteBuffer'
 import * as pako from 'pako'
 
 export type BFastSource = {
  url?: string
  headers?: Record<string, string>
  buffer?: RemoteBuffer | ArrayBuffer
}

 type NumericArrayConstructor =
   | Int8ArrayConstructor
   | Uint8ArrayConstructor
   | Int16ArrayConstructor
   | Uint16ArrayConstructor
   | Int32ArrayConstructor
   | Uint32ArrayConstructor
   | BigInt64ArrayConstructor
   | BigUint64ArrayConstructor
   | Float32ArrayConstructor
   | Float64ArrayConstructor

export type NumericArray =
   | Int8Array
   | Uint8Array
   | Int16Array
   | Uint16Array
   | Int32Array
   | Uint32Array
   | Float32Array
   | Float64Array
   | BigInt64Array
   | BigUint64Array
 
 export class Range {
   start: number
   end: number
   get length () {
     return this.end - this.start
   }
 
   constructor (start: number, end: number) {
     this.start = start
     this.end = end
   }
 
   offset (offset: number) {
     return new Range(this.start + offset, this.end + offset)
   }
 }

 /**
  * Returns -1 size for undefined attributes.
  */
export function parseName(name: string): [number, NumericArrayConstructor]{
  if(name.startsWith('g3d')){
    const result =
        name.includes(':int8:')    ? [1, Int8Array]
      : name.includes(':uint8:')   ? [1, Uint8Array]
      : name.includes(':int16:')   ? [2, Int16Array]
      : name.includes(':uint16:')  ? [2, Uint16Array]
      : name.includes(':int32:')   ? [4, Int32Array]
      : name.includes(':uint32:')  ? [4, Uint32Array]
      : name.includes(':int64:')   ? [8, BigInt64Array]
      : name.includes(':uint64:')  ? [8, BigUint64Array]
      : name.includes(':float32:') ? [4, Float32Array]
      : name.includes(':float64:') ? [8, Float64Array]
      : [-1, undefined] 
    return result as [number, NumericArrayConstructor]
  }
  else{
    const result =
        name.startsWith('byte:')   ? [1, Int8Array]
      : name.startsWith('ubyte:')  ? [1, Uint8Array]
      : name.startsWith('short:')  ? [2, Int16Array]
      : name.startsWith('ushort:') ? [2, Uint16Array]
      : name.startsWith('int:')    ? [4, Int32Array]
      : name.startsWith('uint:')   ? [4, Uint32Array]
      : name.startsWith('long:')   ? [8, BigInt64Array]
      : name.startsWith('ulong:')  ? [8, BigUint64Array]
      : name.startsWith('float:')  ? [4, Float32Array]
      : name.startsWith('double:') ? [8, Float64Array]
      : [-1, undefined] 
     return result as [number, NumericArrayConstructor]
  }
}

 export function typeSize (type: string) {
   switch (type) {
     case 'byte':
     case 'ubyte':
       return 1
     case 'short':
     case 'ushort':
       return 2
     case 'int':
     case 'uint':
     case 'float':
       return 4
     case 'long':
     case 'ulong':
     case 'double':
       return 8
     default:
       return 4
   }
 }
 
 function typeConstructor (type: string): NumericArrayConstructor {
   switch (type) {
     case 'byte':
       return Int8Array
     case 'ubyte':
       return Uint8Array
     case 'short':
       return Int16Array
     case 'ushort':
       return Uint16Array
     case 'int':
       return Int32Array
     case 'uint':
       return Uint32Array
     case 'long':
       return BigInt64Array
     case 'ulong':
       return BigUint64Array
     case 'float':
       return Float32Array
     case 'double':
       return Float64Array
     default:
       return Int32Array
   }
 }
 
 /**
  * Bfast header, mainly for validation.
  * See https://github.com/vimaec/bfast
  */
 export class BFastHeader {
   magic: number
   dataStart: number
   dataEnd: number
   numArrays: number
 
   constructor (
     magic: number,
     dataStart: number,
     dataEnd: number,
     numArrays: number
   ) {
     if (magic !== 0xbfa5) {
       throw new Error('Invalid Bfast. Invalid Magic number')
     }
     if (dataStart <= 32 || dataStart > Number.MAX_SAFE_INTEGER) {
       throw new Error('Invalid Bfast. Data start is out of valid range')
     }
     if (dataEnd < dataStart || dataEnd > Number.MAX_SAFE_INTEGER) {
       throw new Error('Invalid Bfast. Data end is out of valid range')
     }
     if (numArrays < 0 || numArrays > dataEnd) {
       throw new Error('Invalid Bfast. Number of arrays is invalid')
     }
 
     this.magic = magic
     this.dataStart = dataStart
     this.dataEnd = dataEnd
     this.numArrays = numArrays
   }
 
   static createFromArray (array: Uint32Array): BFastHeader {
     // Check validity of data
     // TODO: check endianness
 
     if (array[1] !== 0) {
       throw new Error('Invalid Bfast. Expected 0 in byte position 0')
     }
     if (array[3] !== 0) {
       throw new Error('Invalid Bfast. Expected 0 in byte position 8')
     }
     if (array[5] !== 0) {
       throw new Error('Invalid Bfast. Expected 0 in position 16')
     }
     if (array[7] !== 0) {
       throw new Error('Invalid Bfast. Expected 0 in position 24')
     }
 
     return new this(array[0], array[2], array[4], array[6])
   }
 
   static createFromBuffer (array: ArrayBuffer): BFastHeader {
     return BFastHeader.createFromArray(new Uint32Array(array))
   }
 }
 


 /**
  * See https://github.com/vimaec/bfast for bfast format spec
  * This implementation can either lazily request content as needed from http
  * Or it can serve the data directly from an ArrayBuffer
  * Remote mode can transition to buffer mode if server doesnt support partial http request
  */
 export class BFast {
   source: RemoteBuffer | ArrayBuffer
   offset: number
   name: string
   
   private _header: RemoteValue<BFastHeader>
   private _ranges: RemoteValue<Map<string, Range>>
   private _children: Map<string, RemoteValue<BFast | undefined>>
 
   constructor (
     source: BFastSource,
     offset: number = 0,
     name: string = '' 
   ) {
    
     this.source = source.buffer
      ? source.buffer
      : new RemoteBuffer(source.url, source.headers)

     this.offset = offset
     this.name = name ?? "root"
 
     this._header = new RemoteValue(() => this.requestHeader(), name + '.header')
     this._children = new Map<string, RemoteValue<BFast>>()
     this._ranges = new RemoteValue(() => this.requestRanges(), name + '.ranges')
   }

   /**
    * @returns url of the underlying RemoteBuffer if available
    */
   get url(){
      return this.source instanceof RemoteBuffer ? this.source.url : undefined
   }

   /**
    * Aborts all downloads from the underlying RemoteBuffer
    */
   abort(){
      if(this.source instanceof RemoteBuffer){
        this.source.abort()
      }
      
      this._header.abort()
      this._ranges.abort()
      this._children.forEach(c => c.abort())
   }
 
   /**
    * @returns Bfast Header
    */
   async getHeader () {
     return this._header.get()
   }
 
   /**
    * @returns a map of all buffers by names
    */
   async getRanges () {
     return this._ranges.get()
   }
 
   /**
    * Returns the buffer associated with name as a new bfast.
    * This value is cached for future requests.
    * @param name buffer name
    */
   async getBfast (name: string) {
     let request = this._children.get(name)
     if (!request) {
       request = new RemoteValue(() => this.requestBfast(name))
       this._children.set(name, request)
     }
     return request.get()
   }
 
   async getLocalBfast (name: string, inflate: boolean = false): Promise<BFast | undefined> {
    let buffer = await this.getBuffer(name)
    if (!buffer) return undefined
    if(inflate){
     buffer = pako.inflateRaw(buffer).buffer
    }
    return new BFast({buffer}, 0, name)
  }
 
   /**
    * Returns the raw buffer associated with a name
    * This value is not cached.
    * @param name buffer name
    */
   async getBuffer (name: string): Promise<ArrayBuffer | undefined> {
     const ranges = await this.getRanges()
     const range = ranges.get(name)
     if (!range) return undefined
     const buffer = await this.request(range, name)
     return buffer
   }
 
   /**
    * Returns a number array from the buffer associated with name
    * @param name buffer name
    */
   async getArray (name: string): Promise<NumericArray | undefined> {
     const buffer = await this.getBuffer(name)
     if (!buffer) return undefined
     const type = name.split(':')[0]
     const Ctor = typeConstructor(type)
     const array = new Ctor(buffer)
     return array
   }

    async getInt32Array(name: string): Promise<Int32Array | undefined> {
      const buffer = await this.getBuffer(name)
      if(!buffer) return
      return new Int32Array(buffer)
    }

    async getFloat32Array(name: string): Promise<Float32Array | undefined> {
      const buffer = await this.getBuffer(name)
      if(!buffer) return
      return new Float32Array(buffer)
    }

    async getBigInt64Array(name: string): Promise<BigInt64Array | undefined> {
      const buffer = await this.getBuffer(name)
      if(!buffer) return
      return new BigInt64Array(buffer)
    }

    async getUint16Array(name: string): Promise<Uint16Array | undefined> {
      const buffer = await this.getBuffer(name)
      if(!buffer) return
      return new Uint16Array(buffer)
    }

   /**
    * Returns a single value from given buffer name
    * @param name buffer name
    * @param index row index
    */
   async getValue (name: string, index: number): Promise<number | BigInt | undefined> {
    const array = await this.getValues(name, index, 1)
    return array?.[0]
   }
 
   async getRange(name:string){
    const ranges = await this.getRanges()
    return ranges.get(name)
   }
 
   /**
    * Returns count subsequent values from given buffer name.
    * @param name buffer name
    * @param index row index
    * @param count count of values to return
    */
   async getValues (name: string, index: number, count: number): Promise<NumericArray | undefined> {
    if (index < 0 || count < 1) return undefined
 
    const range = await this.getRange(name)
    if (!range) return undefined
 
    const [size, ctor] = parseName(name)
    if(size < 0) return undefined
 
    const start = Math.min(range.start + index * size, range.end)
    const end = Math.min(start + size * count, range.end)
    
    const dataRange = new Range(start, end)
    if(dataRange.length <= 0) return undefined
 
    const buffer = await this.request(
      dataRange,
      `${name}[${index.toString()}]`
    )
    if (!buffer) return undefined
 
    const array = new ctor(buffer)
    return array
  }
  /**
   
    * Returns the buffer with given name as a byte array
    * @param name buffer name
    */
   async getBytes (name: string): Promise<Uint8Array | undefined> {
     const buffer = await this.getBuffer(name)
     if (!buffer) return undefined
     const array = new Uint8Array(buffer)
     return array
   }
 
   /**
    * Returns a map of name-values with the same index from all buffers.
    * @param name buffer name
    */
   async getRow (index: number): Promise<Map<string, number | BigInt | undefined> | undefined> {
     const ranges = await this.getRanges()
     if (!ranges) return undefined
     const result = new Map<string, number | BigInt | undefined>()
     const promises = []
     for (const name of ranges.keys()) {
       const p = this.getValue(name, index).then((v) => result.set(name, v))
       promises.push(p)
     }
 
     await Promise.all(promises)
     return result
   }
 
   /**
    * Forces download of the full underlying buffer, from now on all calls will be local.
    */
   async forceDownload () {
     if (this.source instanceof ArrayBuffer) {
       console.log('Ignoring forceDownload on local buffer.')
       return
     }
     const buffer = await this.remote(undefined, this.name)
     if (!buffer) throw new Error('Failed to download BFAST.')
     this.source = buffer
   }
 
   /**
    * Downloads the appropriate range and cast it as a new sub bfast.
    */
   private async requestBfast (name: string): Promise<BFast | undefined> {
     const ranges = await this.getRanges()
 
     const range = ranges.get(name)
     if (!range) return undefined
 
     const result = new BFast(
       {buffer: this.source},
       this.offset + range.start,
       this.name + '.' + name
     )
 
     return result
   }
 
   /**
    * Downloads and parses ranges as a map of name->range
    */
   private async requestRanges () {
     const header = await this.getHeader()
     const buffer = await this.request(
       new Range(32, 32 + header.numArrays * 16),
       'Ranges'
     )
     if (!buffer) throw new Error('Could not get BFAST Ranges.')
 
     // Parse range
     const array = new Uint32Array(buffer)
     const ranges: Range[] = []
     for (let i = 0; i < array.length; i += 4) {
       if (array[i + 1] !== 0 || array[i + 3] !== 0) {
         throw new Error('Invalid Bfast. 64 bit ranges not supported')
       }
       ranges.push(new Range(array[i], array[i + 2]))
     }
 
     const names = await this.requestNames(ranges[0])
     if (ranges.length !== names.length + 1) {
       throw new Error('Mismatched ranges and names count')
     }
 
     // Map ranges and names
     const map = new Map<string, Range>()
     for (let i = 0; i < names.length; i++) {
       map.set(names[i], ranges[i + 1])
     }
 
     return map
   }
 
   /**
    * Downloads and parse names from remote.
    */
   private async requestNames (range: Range): Promise<string[]> {
     const buffer = await this.request(range, 'Names')
     const names = new TextDecoder('utf-8').decode(buffer)
     const result = names.slice(0, -1).split('\0')
     return result
   }
 
   /**
    * Downloads and parse header from remote.
    */
   private async requestHeader (): Promise<BFastHeader> {
     const buffer = await this.request(new Range(0, 32), 'Header')
     if (!buffer) throw new Error('Could not get BFAST Header')
     const result = BFastHeader.createFromBuffer(buffer)
     return result
   }
 
   /**
    * Gets array buffer from from cache, or partial http, fallback to full http
    * @param range range to get, or get full resource if undefined
    * @param label label for logs
    */
   private async request (range: Range, label: string): Promise<ArrayBuffer> {
     const buffer =
       this.local(range, label) ??
       (await this.remote(range, label)) ??
       (await this.remote(undefined, label))
 
     if (!buffer) {
       throw new Error(`Could not load vim at ${this.source}`)
     }
 
     if (buffer.byteLength > range.length) {
       this.source = buffer
       return this.local(range, label)
     }
     return buffer
   }
 
   /**
    * returns requested range from cache.
    */
   private local (range: Range, label: string) : ArrayBuffer | undefined {
     if (!(this.source instanceof ArrayBuffer)) return undefined
     //console.log(`Returning local ${this.name}.${label}`)
     const r = range.offset(this.offset)
     return this.source.slice(r.start, r.end)
   }
 
   /**
    * returns requested range from remote.
    */
   private async remote (range: Range | undefined, label: string): Promise<ArrayBuffer | undefined> {
     if (!(this.source instanceof RemoteBuffer)) return undefined
     const r = range?.offset(this.offset)
     const buffer = await this.source.http(r, `${this.name}.${label}`)
     if (range && (buffer?.byteLength ?? 0) < range.length) {
       console.log('Range request request failed.')
       return undefined
     }
     return buffer
   }
 
  /**
   * Returns a new local bfast equivalent to this bfast.
   */
  async getSelf () {
    const header = await this._header.get()
    const range = new Range(0, header.dataEnd)
    const buffer = await this.request(range, this.name)
    const result = new BFast({buffer}, 0, this.name)
    return result
  }
 }
