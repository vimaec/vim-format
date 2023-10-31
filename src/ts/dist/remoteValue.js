"use strict";
/**
 * @module vim-ts
 */
Object.defineProperty(exports, "__esModule", { value: true });
exports.RemoteValue = void 0;
/**
 * Returns a value from cache or queue up existing request or start a new requests
 */
class RemoteValue {
    constructor(getter, label) {
        this._getter = getter;
        this.label = label ?? '';
    }
    abort() {
        this._request = undefined;
    }
    /**
     * Returns a value from cache or queue up existing request or start a new requests
     */
    get() {
        if (this._value !== undefined) {
            // console.log(this.label + ' returning cached value ')
            return Promise.resolve(this._value);
        }
        if (this._request) {
            // console.log(this.label + ' returning existing request')
            return this._request;
        }
        // console.log(this.label + ' creating new request')
        this._request = this._getter().then((value) => {
            this._value = value;
            this._request = undefined;
            return this._value;
        });
        return this._request;
    }
}
exports.RemoteValue = RemoteValue;
