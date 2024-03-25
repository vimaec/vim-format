"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.Requester = void 0;
const logging_1 = require("./logging");
const retriableRequest_1 = require("./retriableRequest");
const requestTracker_1 = require("./requestTracker");
/**
 * Wrapper to provide tracking for all webrequests via request logger.
 */
class Requester {
    constructor(verbose = false) {
        this.maxConcurency = 10;
        this._queue = [];
        this._active = new Set();
        this._logs = verbose ? new logging_1.DefaultLog() : new logging_1.NoLog();
        this._tracker = new requestTracker_1.RequestTracker(undefined, this._logs);
        this._tracker.onUpdate = (p) => this.onProgress?.(p);
    }
    abort() {
        this._active.forEach(request => {
            request.abort();
        });
        this._active.clear();
        this._queue.length = 0;
    }
    async http(url, label) {
        const request = new retriableRequest_1.RetriableRequest(url, undefined, 'arraybuffer');
        request.msg = url;
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
exports.Requester = Requester;
