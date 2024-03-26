/**
 * @module vim-ts
 */
export declare class Logger {
    log: (s: any) => void;
    warn: (s: any) => void;
    error: (s: any) => void;
}
export declare class DefaultLog implements Logger {
    log: (s: any) => void;
    warn: (s: any) => void;
    error: (s: any) => void;
}
export declare class NoLog implements Logger {
    log: (s: any) => void;
    warn: (s: any) => void;
    error: (s: any) => void;
}
