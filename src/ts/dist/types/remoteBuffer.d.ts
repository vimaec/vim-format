/**
 * @module vim-ts
 */
import { Range } from './bfast';
import { IProgressLogs } from './requestTracker';
export declare function setRemoteBufferMaxConcurency(value: number): void;
/**
 * Wrapper to provide tracking for all webrequests via request logger.
 */
export declare class RemoteBuffer {
    url: string;
    maxConcurency: number;
    onProgress: (progress: IProgressLogs) => void;
    private _tracker;
    private _logs;
    private _queue;
    private _active;
    private _encoded;
    constructor(url: string, verbose?: boolean);
    private requestEncoding;
    abort(): void;
    http(range: Range | undefined, label: string): Promise<ArrayBuffer>;
    private enqueue;
    private retry;
    private end;
    private next;
}
