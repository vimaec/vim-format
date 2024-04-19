import { BFast } from "./bfast"

/**
 * Representation of VimHeader from the Vim format
 * See https://github.com/vimaec/vim#header-buffer
 */
export type VimHeader = {
  vim: string | undefined
  vimx: string | undefined
  id: string | undefined
  revision: string | undefined
  generator: string | undefined
  created: string | undefined
  schema: string | undefined
}

export async function requestHeader (bfast: BFast): Promise<VimHeader> {
  const header = await bfast.getBuffer('header')
  const pairs = new TextDecoder('utf-8').decode(header).split('\n')
  const map = new Map(pairs.map((p) => p.split('=')).map((p) => [p[0], p[1]]))
  return {
    vim: map.get('vim'),
    vimx: map.get('vimx'),
    id: map.get('id'),
    revision: map.get('revision'),
    generator: map.get('generator'),
    created: map.get('created'),
    schema: map.get('schema')
  }
}