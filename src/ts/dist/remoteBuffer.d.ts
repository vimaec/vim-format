/**
 * @module vim-ts
 */
import { Range } from './bfast';
import { IProgressLogs } from './requestTracker';
import { Logger } from './logging';
export declare function setRemoteBufferMaxConcurency(value: number): void;
/**
* Wrapper to provide tracking for all webrequests via request logger.
*/
export declare class RemoteBuffer {
    url: string;
    maxConcurency: number;
    onProgress: (progress: IProgressLogs) => void;
    logs: Logger;
    private _tracker;
    private _queue;
    private _active;
    constructor(url: string);
    abort(): void;
    http(range: Range | undefined, label: string): Promise<ArrayBuffer>;
    private enqueue;
    private retry;
    private end;
    private next;
}
