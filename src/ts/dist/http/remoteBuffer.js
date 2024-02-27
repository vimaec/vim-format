"use strict";
/**
 * @module vim-ts
 */
Object.defineProperty(exports, "__esModule", { value: true });
exports.RemoteBuffer = exports.setRemoteBufferMaxConcurency = void 0;
const requestTracker_1 = require("./requestTracker");
const logging_1 = require("./logging");
const retriableRequest_1 = require("./retriableRequest");
let RemoteBufferMaxConcurency = 10;
function setRemoteBufferMaxConcurency(value) {
    RemoteBufferMaxConcurency = value;
}
exports.setRemoteBufferMaxConcurency = setRemoteBufferMaxConcurency;
/**
 * Wrapper to provide tracking for all webrequests via request logger.
 */
class RemoteBuffer {
    constructor(url) {
        this.maxConcurency = RemoteBufferMaxConcurency;
        this._queue = [];
        this._active = new Set();
        this.url = url;
        this.logs = new logging_1.NoLog();
        this._tracker = new requestTracker_1.RequestTracker(url, this.logs);
        this._tracker.onUpdate = (p) => this.onProgress?.(p);
    }
    abort() {
        this._active.forEach(request => {
            request.abort();
        });
        this._active.clear();
        this._queue.length = 0;
    }
    async http(range, label) {
        const rangeStr = range
            ? `bytes=${range.start}-${range.end - 1}`
            : undefined;
        const request = new retriableRequest_1.RetriableRequest(this.url, rangeStr, 'arraybuffer');
        request.msg = range
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
        this.logs.log('Starting ' + next.msg);
    }
}
exports.RemoteBuffer = RemoteBuffer;
