import { DefaultLog, Logger, NoLog } from "./logging"
import { RetriableRequest } from "./retriableRequest"
import { IProgressLogs, RequestTracker } from "./requestTracker"

/**
 * Wrapper to provide tracking for all webrequests via request logger.
 */
export class Requester {
  maxConcurency: number = 10
  onProgress: (progress : IProgressLogs) => void
  private _tracker: RequestTracker
  private _logs : Logger
  private _queue: RetriableRequest[] = []
  private _active: Set<RetriableRequest> = new Set<RetriableRequest>()

  constructor (verbose: boolean = false) {
    this._logs = verbose ? new DefaultLog() : new NoLog()
    this._tracker = new RequestTracker(undefined, this._logs)
    this._tracker.onUpdate = (p) => this.onProgress?.(p)
  }

  abort(){
    this._active.forEach(request => {
      request.abort()
    })
    this._active.clear()
    this._queue.length = 0
  }

  async http (url : string, headers : Record<string, string> = {}, label?: string) {
    const request = new RetriableRequest(url, headers, undefined, 'arraybuffer')
    request.msg = url

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

  private enqueue (xhr: RetriableRequest) {
    this._queue.push(xhr)
    this.next()
  }

  private retry (xhr: RetriableRequest) {
    this._active.delete(xhr)
    this.maxConcurency = Math.max(1, this.maxConcurency - 1)
    setTimeout(() => this.enqueue(xhr), 2000)
  }

  private end (xhr: RetriableRequest) {
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
