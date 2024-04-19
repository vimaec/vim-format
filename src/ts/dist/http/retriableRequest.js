"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RetriableRequest = void 0;
class RetriableRequest {
    constructor(url, range, 
    // eslint-disable-next-line no-undef
    responseType) {
        this.url = url;
        this.range = range;
        this.responseType = responseType;
    }
    abort() {
        this.xhr?.abort();
    }
    send() {
        this.xhr?.abort();
        const xhr = new XMLHttpRequest();
        xhr.open('GET', this.url);
        xhr.responseType = this.responseType;
        if (this.range) {
            xhr.setRequestHeader('Range', this.range);
        }
        xhr.onprogress = (e) => {
            this.onProgress?.(e);
        };
        xhr.onload = (e) => {
            this.onProgress?.(e);
            this.onLoad?.(xhr.response);
        };
        xhr.onerror = (_) => {
            this.onError?.();
        };
        xhr.send();
        this.xhr = xhr;
    }
}
exports.RetriableRequest = RetriableRequest;
