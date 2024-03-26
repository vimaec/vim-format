import { IProgressLogs } from "./requestTracker";
/**
 * Wrapper to provide tracking for all webrequests via request logger.
 */
export declare class Requester {
    maxConcurency: number;
    onProgress: (progress: IProgressLogs) => void;
    private _tracker;
    private _logs;
    private _queue;
    private _active;
    constructor(verbose?: boolean);
    abort(): void;
    http(url: string, label?: string): Promise<ArrayBuffer>;
    private enqueue;
    private retry;
    private end;
    private next;
}
