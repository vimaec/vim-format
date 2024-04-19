import { Logger, NoLog } from "./logging"

/**
 * Represents the state of a single web request
 */
class Request {
  status: 'active' | 'completed' | 'failed' = 'active'
  field: string
  loaded: number = 0
  total: number = 0
  lengthComputable: boolean = true

  constructor (field: string) {
    this.field = field
  }
}


export interface IProgressLogs {
  get loaded(): number
  get total(): number
  get all(): Map<string, Request>
}

/**
 * Represents a collection of webrequests
 * Will only send update signal at most every delay
 * Provides convenient aggregation of metrics.
 */
export class RequestTracker {
  source: string
  all: Map<string, Request> = new Map<string, Request>()
  lastUpdate: number = 0
  delay: number = 500
  sleeping: boolean = false
  logs : Logger

  /**
   * callback on update, called at most every delay time.
   */
  onUpdate: ((self: RequestTracker) => void) | undefined = undefined

  constructor (source?: string, logger : Logger = new NoLog()) {
    this.source = source
    this.logs = logger
  }

  /**
   * Returns the sum of .loaded across all requests
   */
  get loaded () {
    let result = 0
    this.all.forEach((request) => {
      result += request.loaded
    })
    return result
  }

  /**
   * Returns the sum of .total across all requests
   */
  get total () {
    let result = 0
    this.all.forEach((request) => {
      result += request.total
    })
    return result
  }

  /**
   * Starts tracking a new web request
   */
  start (field: string) {
    this.all.set(field, new Request(field))
    this.signal()
  }

  /**
   * Update an existing web request
   */
  update (field: string, progress: ProgressEvent) {
    const r = this.all.get(field)
    if (!r) throw new Error('Updating missing download')
    if (r.status !== 'active') return
    r.loaded = progress.loaded
    r.total = progress.total
    r.lengthComputable = progress.lengthComputable
    this.signal()
  }

  /**
   * Notify a webrequest of failure
   */
  fail (field: string) {
    this.logs.error(`${field} failed`)
    const download = this.all.get(field)
    if (!download) throw new Error('Failing missing download')
    download.status = 'failed'
    this.signal()
  }

  /**
   * Notify a webrequest of success
   */
  end (field: string) {
    this.logs.log(`${field} completed`)
    const download = this.all.get(field)
    if (!download) throw new Error('Failing missing download')
    download.status = 'completed'
    // We don't want to throttle end update.
    this.onUpdate?.(this)
  }

  private signal () {
    if (this.sleeping) return
    this.sleeping = true
    setTimeout(() => (this.sleeping = false), this.delay)
    this.onUpdate?.(this)
  }
}