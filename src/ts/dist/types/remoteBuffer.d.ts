/**
 * @module vim-ts
 */
import { Range } from './bfast';
import { IProgressLogs } from './requestTracker';
import { Logger } from './logging';
export declare function setRemoteBufferMaxConcurency(value: number): void;
export declare class RetryRequest {
    url: string;
    range: string | undefined;
    responseType: XMLHttpRequestResponseType;
    msg: string | undefined;
    xhr: XMLHttpRequest | undefined;
    constructor(url: string, range: string | undefined, responseType: XMLHttpRequestResponseType);
    onLoad: ((result: any) => void) | undefined;
    onError: (() => void) | undefined;
    onProgress: ((e: ProgressEvent<EventTarget>) => void) | undefined;
    abort(): void;
    send(): void;
}
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
    private _encoded;
    constructor(url: string);
    private requestEncoding;
    abort(): void;
    http(range: Range | undefined, label: string): Promise<ArrayBuffer>;
    private enqueue;
    private retry;
    private end;
    private next;
}
