/**
 * @module vim-ts
 */
/**
 * Returns a value from cache or queue up existing request or start a new requests
 */
export declare class RemoteValue<T> {
    label: string;
    private _getter;
    private _value;
    private _request;
    constructor(getter: () => Promise<T>, label?: string);
    /**
     * Returns a value from cache or queue up existing request or start a new requests
     */
    get(): Promise<T>;
}
