"use strict";
/**
 * @module vim-ts
 */
Object.defineProperty(exports, "__esModule", { value: true });
exports.NoLog = exports.DefaultLog = exports.Logger = void 0;
class Logger {
}
exports.Logger = Logger;
class DefaultLog {
    constructor() {
        this.log = (s) => console.log(s);
        this.warn = (s) => console.warn(s);
        this.error = (s) => console.error(s);
    }
}
exports.DefaultLog = DefaultLog;
class NoLog {
    constructor() {
        this.log = (s) => { };
        this.warn = (s) => { };
        this.error = (s) => { };
    }
}
exports.NoLog = NoLog;
