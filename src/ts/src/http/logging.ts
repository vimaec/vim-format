/**
 * @module vim-ts
 */


export class Logger{
  log : (s) => void
  warn : (s) => void
  error : (s) => void
}

export class DefaultLog implements Logger{
  log = (s) => console.log(s)
  warn = (s) => console.warn(s)
  error = (s) => console.error(s)
}

export class NoLog implements Logger{
  log = (s) => {}
  warn = (s) => {}
  error = (s) => {}
}