/**
 * @module vim-ts
 */

import { Range } from './bfast'
import { RemoteValue } from './remoteValue'
import {IProgressLogs, RequestTracker} from './requestTracker'
import {DefaultLog, Logger, NoLog} from './logging'

let RemoteBufferMaxConcurency = 10
export function setRemoteBufferMaxConcurency(value: number){
  RemoteBufferMaxConcurency =value
}


class RetryRequest {
  url: string
  range: string | undefined
  // eslint-disable-next-line no-undef
  responseType: XMLHttpRequestResponseType
  msg: string | undefined
  xhr: XMLHttpRequest | undefined

  constructor (
    url: string,
    range: string | undefined,
    // eslint-disable-next-line no-undef
    responseType: XMLHttpRequestResponseType
  ) {
    this.url = url
    this.range = range
    this.responseType = responseType
  }

  onLoad: ((result: any) => void) | undefined
  onError: (() => void) | undefined
  onProgress: ((e: ProgressEvent<EventTarget>) => void) | undefined

  abort(){
    this.xhr.abort()
  }

  send () {
    this.xhr?.abort()
    const xhr = new XMLHttpRequest()
    xhr.open('GET', this.url)
    xhr.responseType = this.responseType

    if (this.range) {
      xhr.setRequestHeader('Range', this.range)
    }

    xhr.onprogress = (e) => {
      this.onProgress?.(e)
    }
    xhr.onload = (e) => {
      this.onProgress?.(e)
      this.onLoad?.(xhr.response)
    }
    xhr.onerror = (_) => {
      this.onError?.()
    }
    xhr.send()
    this.xhr = xhr
  }
}

/**
 * Wrapper to provide tracking for all webrequests via request logger.
 */
export class RemoteBuffer {
  url: string
  maxConcurency: number = RemoteBufferMaxConcurency
  onProgress: (progress : IProgressLogs) => void
  private _tracker: RequestTracker
  private _logs : Logger
  private _queue: RetryRequest[] = []
  private _active: Set<RetryRequest> = new Set<RetryRequest>()
  private _encoded: RemoteValue<boolean>

  constructor (url: string, verbose: boolean = false) {
    this.url = url
    this._logs = verbose ? new DefaultLog() : new NoLog()
    this._tracker = new RequestTracker(url, this._logs)
    this._tracker.onUpdate = (p) => this.onProgress?.(p)
    this._encoded = new RemoteValue(() => this.requestEncoding())
  }

  private async requestEncoding () {
    const xhr = new XMLHttpRequest()
    xhr.open('HEAD', this.url)
    xhr.send()
    this._logs.log(`Requesting header for ${this.url}`)

    const promise = new Promise<string | undefined>((resolve, reject) => {
      xhr.onload = (_) => {
        let encoding: string | null | undefined
        try {
          encoding = xhr.getResponseHeader('content-encoding')
        } catch (e) {
          this._logs.error(e)
        }
        resolve(encoding ?? undefined)
      }
      xhr.onerror = (_) => resolve(undefined)
    })

    const encoding = await promise
    const encoded = !!encoding

    this._logs.log(`Encoding for ${this.url} = ${encoding}`)
    if (encoded) {
      this._logs.log(
        `Defaulting to download strategy for encoded content at ${this.url}`
      )
    }
    return encoded
  }

  abort(){
    this._active.forEach(request => {
      request.abort()
    })
    this._active.clear()
    this._queue.length = 0
  }

  async http (range: Range | undefined, label: string) {
    const useRange = range && !(await this._encoded.get())
    const rangeStr = useRange
      ? `bytes=${range.start}-${range.end - 1}`
      : undefined
    const request = new RetryRequest(this.url, rangeStr, 'arraybuffer')
    request.msg = useRange
      ? `${label} : [${range.start}, ${range.end}] of ${this.url}`
      : `${label} of ${this.url}`

    this.enqueue(request)
    return new Promise<ArrayBuffer | undefined>((resolve, reject) => {
      this._tracker.start(label)

      request.onProgress = (e) => {
        this._tracker.update(label, e)
      }
      request.onLoad = (result) => {
        this._tracker.end(label)
        resolve(result)
        this.end(request)
      }
      request.onError = () => {
        this._tracker.fail(label)
        this.retry(request)
      }
    })
  }

  private enqueue (xhr: RetryRequest) {
    this._queue.push(xhr)
    this.next()
  }

  private retry (xhr: RetryRequest) {
    this._active.delete(xhr)
    this.maxConcurency = Math.max(1, this.maxConcurency - 1)
    setTimeout(() => this.enqueue(xhr), 2000)
  }

  private end (xhr: RetryRequest) {
    this._active.delete(xhr)
    this.next()
  }

  private next () {
    if (this._queue.length === 0) {
      return
    }

    if (this._active.size >= this.maxConcurency) {
      return
    }

    const next = this._queue[0]
    this._queue.shift()
    this._active.add(next)
    next.send()
    this._logs.log('Starting ' + next.msg)
  }
}
