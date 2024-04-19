"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.requestHeader = void 0;
async function requestHeader(bfast) {
    const header = await bfast.getBuffer('header');
    const pairs = new TextDecoder('utf-8').decode(header).split('\n');
    const map = new Map(pairs.map((p) => p.split('=')).map((p) => [p[0], p[1]]));
    return {
        vim: map.get('vim'),
        vimx: map.get('vimx'),
        id: map.get('id'),
        revision: map.get('revision'),
        generator: map.get('generator'),
        created: map.get('created'),
        schema: map.get('schema')
    };
}
exports.requestHeader = requestHeader;
