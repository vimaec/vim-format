"use strict";
/**
 * @module vim-ts
 */
Object.defineProperty(exports, "__esModule", { value: true });
exports.RemoteBuffer = exports.setRemoteBufferMaxConcurency = void 0;
const remoteValue_1 = require("./remoteValue");
const requestTracker_1 = require("./requestTracker");
const logging_1 = require("./logging");
let RemoteBufferMaxConcurency = 10;
function setRemoteBufferMaxConcurency(value) {
    RemoteBufferMaxConcurency = value;
}
exports.setRemoteBufferMaxConcurency = setRemoteBufferMaxConcurency;
class RetryRequest {
    constructor(url, range, 
    // eslint-disable-next-line no-undef
    responseType) {
        this.url = url;
        this.range = range;
        this.responseType = responseType;
    }
    abort() {
        this.xhr.abort();
    }
    send() {
        this.xhr?.abort();
        const xhr = new XMLHttpRequest();
        xhr.open('GET', this.url);
        xhr.responseType = this.responseType;
        if (this.range) {
            xhr.setRequestHeader('Range', this.range);
        }
        xhr.onprogress = (e) => {
            this.onProgress?.(e);
        };
        xhr.onload = (e) => {
            this.onProgress?.(e);
            this.onLoad?.(xhr.response);
        };
        xhr.onerror = (_) => {
            this.onError?.();
        };
        xhr.send();
        this.xhr = xhr;
    }
}
/**
 * Wrapper to provide tracking for all webrequests via request logger.
 */
class RemoteBuffer {
    constructor(url, verbose = false) {
        this.maxConcurency = RemoteBufferMaxConcurency;
        this._queue = [];
        this._active = new Set();
        this.url = url;
        this._logs = verbose ? new logging_1.DefaultLog() : new logging_1.NoLog();
        this._tracker = new requestTracker_1.RequestTracker(url, this._logs);
        this._tracker.onUpdate = (p) => this.onProgress?.(p);
        this._encoded = new remoteValue_1.RemoteValue(() => this.requestEncoding());
    }
    async requestEncoding() {
        const xhr = new XMLHttpRequest();
        xhr.open('HEAD', this.url);
        xhr.send();
        this._logs.log(`Requesting header for ${this.url}`);
        const promise = new Promise((resolve, reject) => {
            xhr.onload = (_) => {
                let encoding;
                try {
                    encoding = xhr.getResponseHeader('content-encoding');
                }
                catch (e) {
                    this._logs.error(e);
                }
                resolve(encoding ?? undefined);
            };
            xhr.onerror = (_) => resolve(undefined);
        });
        const encoding = await promise;
        const encoded = !!encoding;
        this._logs.log(`Encoding for ${this.url} = ${encoding}`);
        if (encoded) {
            this._logs.log(`Defaulting to download strategy for encoded content at ${this.url}`);
        }
        return encoded;
    }
    abort() {
        this._active.forEach(request => {
            request.abort();
        });
        this._active.clear();
        this._queue.length = 0;
    }
    async http(range, label) {
        const useRange = range && !(await this._encoded.get());
        const rangeStr = useRange
            ? `bytes=${range.start}-${range.end - 1}`
            : undefined;
        const request = new RetryRequest(this.url, rangeStr, 'arraybuffer');
        request.msg = useRange
            ? `${label} : [${range.start}, ${range.end}] of ${this.url}`
            : `${label} of ${this.url}`;
        this.enqueue(request);
        return new Promise((resolve, reject) => {
            this._tracker.start(label);
            request.onProgress = (e) => {
                this._tracker.update(label, e);
            };
            request.onLoad = (result) => {
                this._tracker.end(label);
                resolve(result);
                this.end(request);
            };
            request.onError = () => {
                this._tracker.fail(label);
                this.retry(request);
            };
        });
    }
    enqueue(xhr) {
        this._queue.push(xhr);
        this.next();
    }
    retry(xhr) {
        this._active.delete(xhr);
        this.maxConcurency = Math.max(1, this.maxConcurency - 1);
        setTimeout(() => this.enqueue(xhr), 2000);
    }
    end(xhr) {
        this._active.delete(xhr);
        this.next();
    }
    next() {
        if (this._queue.length === 0) {
            return;
        }
        if (this._active.size >= this.maxConcurency) {
            return;
        }
        const next = this._queue[0];
        this._queue.shift();
        this._active.add(next);
        next.send();
        this._logs.log('Starting ' + next.msg);
    }
}
exports.RemoteBuffer = RemoteBuffer;
