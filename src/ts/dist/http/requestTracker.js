"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RequestTracker = void 0;
const logging_1 = require("./logging");
/**
 * Represents the state of a single web request
 */
class Request {
    constructor(field) {
        this.status = 'active';
        this.loaded = 0;
        this.total = 0;
        this.lengthComputable = true;
        this.field = field;
    }
}
/**
 * Represents a collection of webrequests
 * Will only send update signal at most every delay
 * Provides convenient aggregation of metrics.
 */
class RequestTracker {
    constructor(source, logger = new logging_1.NoLog()) {
        this.all = new Map();
        this.lastUpdate = 0;
        this.delay = 500;
        this.sleeping = false;
        /**
         * callback on update, called at most every delay time.
         */
        this.onUpdate = undefined;
        this.source = source;
        this.logs = logger;
    }
    /**
     * Returns the sum of .loaded across all requests
     */
    get loaded() {
        let result = 0;
        this.all.forEach((request) => {
            result += request.loaded;
        });
        return result;
    }
    /**
     * Returns the sum of .total across all requests
     */
    get total() {
        let result = 0;
        this.all.forEach((request) => {
            result += request.total;
        });
        return result;
    }
    /**
     * Starts tracking a new web request
     */
    start(field) {
        this.all.set(field, new Request(field));
        this.signal();
    }
    /**
     * Update an existing web request
     */
    update(field, progress) {
        const r = this.all.get(field);
        if (!r)
            throw new Error('Updating missing download');
        if (r.status !== 'active')
            return;
        r.loaded = progress.loaded;
        r.total = progress.total;
        r.lengthComputable = progress.lengthComputable;
        this.signal();
    }
    /**
     * Notify a webrequest of failure
     */
    fail(field) {
        this.logs.error(`${field} failed`);
        const download = this.all.get(field);
        if (!download)
            throw new Error('Failing missing download');
        download.status = 'failed';
        this.signal();
    }
    /**
     * Notify a webrequest of success
     */
    end(field) {
        this.logs.log(`${field} completed`);
        const download = this.all.get(field);
        if (!download)
            throw new Error('Failing missing download');
        download.status = 'completed';
        // We don't want to throttle end update.
        this.onUpdate?.(this);
    }
    signal() {
        if (this.sleeping)
            return;
        this.sleeping = true;
        setTimeout(() => (this.sleeping = false), this.delay);
        this.onUpdate?.(this);
    }
}
exports.RequestTracker = RequestTracker;
