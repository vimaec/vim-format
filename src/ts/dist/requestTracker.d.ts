import { Logger } from "./logging";
/**
 * Represents the state of a single web request
 */
declare class Request {
    status: 'active' | 'completed' | 'failed';
    field: string;
    loaded: number;
    total: number;
    lengthComputable: boolean;
    constructor(field: string);
}
export interface IProgressLogs {
    get loaded(): number;
    get total(): number;
    get all(): Map<string, Request>;
}
/**
 * Represents a collection of webrequests
 * Will only send update signal at most every delay
 * Provides convenient aggregation of metrics.
 */
export declare class RequestTracker {
    source: string;
    all: Map<string, Request>;
    lastUpdate: number;
    delay: number;
    sleeping: boolean;
    logs: Logger;
    /**
     * callback on update, called at most every delay time.
     */
    onUpdate: ((self: RequestTracker) => void) | undefined;
    constructor(source: string, logger?: Logger);
    /**
     * Returns the sum of .loaded across all requests
     */
    get loaded(): number;
    /**
     * Returns the sum of .total across all requests
     */
    get total(): number;
    /**
     * Starts tracking a new web request
     */
    start(field: string): void;
    /**
     * Update an existing web request
     */
    update(field: string, progress: ProgressEvent): void;
    /**
     * Notify a webrequest of failure
     */
    fail(field: string): void;
    /**
     * Notify a webrequest of success
     */
    end(field: string): void;
    private signal;
}
export {};
