

export class RetriableRequest {
  url: string;
  range: string | undefined;
  // eslint-disable-next-line no-undef
  responseType: XMLHttpRequestResponseType;
  msg: string | undefined;
  xhr: XMLHttpRequest | undefined;

  constructor(
    url: string,
    range: string | undefined,
    // eslint-disable-next-line no-undef
    responseType: XMLHttpRequestResponseType
  ) {
    this.url = url;
    this.range = range;
    this.responseType = responseType;
  }

  onLoad: ((result: any) => void) | undefined;
  onError: (() => void) | undefined;
  onProgress: ((e: ProgressEvent<EventTarget>) => void) | undefined;

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
