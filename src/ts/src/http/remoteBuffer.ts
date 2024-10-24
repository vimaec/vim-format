/**
 * @module vim-ts
 */

import { Range } from '../bfast'
import { RemoteValue } from './remoteValue'
import {IProgressLogs, RequestTracker} from './requestTracker'
import { Logger, NoLog} from './logging'
import { RetriableRequest } from './retriableRequest'

let RemoteBufferMaxConcurency = 10
export function setRemoteBufferMaxConcurency(value: number){
  RemoteBufferMaxConcurency =value
}

/**
 * Wrapper to provide tracking for all webrequests via request logger.
 */
export class RemoteBuffer {
  readonly url: string
  readonly headers : Record<string, string> = {}
  maxConcurency: number = RemoteBufferMaxConcurency
  onProgress: (progress : IProgressLogs) => void
  logs : Logger
  
  private _tracker: RequestTracker
  private _queue: RetriableRequest[] = []
  private _active: Set<RetriableRequest> = new Set<RetriableRequest>()

  constructor (url: string, headers? : Record<string, string>) {
    this.url = url
    this.logs = new NoLog()
    this.headers = headers ?? {}
    this._tracker = new RequestTracker(url, this.logs)
    this._tracker.onUpdate = (p) => this.onProgress?.(p)
  }

  abort(){
    this._active.forEach(request => {
      request.abort()
    })
    this._active.clear()
    this._queue.length = 0
  }

  async http (range: Range | undefined, label: string) {
    const rangeStr = range
      ? `bytes=${range.start}-${range.end - 1}`
      : undefined

    const request = new RetriableRequest(this.url, this.headers, rangeStr, 'arraybuffer')

    request.msg = range
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

  private enqueue (request: RetriableRequest) {
    this._queue.push(request)
    this.next()
  }

  private retry (request: RetriableRequest) {
    this._active.delete(request)
    this.maxConcurency = Math.max(1, this.maxConcurency - 1)
    setTimeout(() => this.enqueue(request), 2000)
  }

  private end (request: RetriableRequest) {
    this.logs.log('Finished ' + request.msg)
    this._active.delete(request)
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
    this.logs.log('Started ' + next.msg)
  }
}
