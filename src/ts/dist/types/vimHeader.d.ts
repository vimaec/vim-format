import { BFast } from "./bfast";
/**
 * Representation of VimHeader from the Vim format
 * See https://github.com/vimaec/vim#header-buffer
 */
export declare type VimHeader = {
    vim: string | undefined;
    vimx: string | undefined;
    id: string | undefined;
    revision: string | undefined;
    generator: string | undefined;
    created: string | undefined;
    schema: string | undefined;
};
export declare function requestHeader(bfast: BFast): Promise<VimHeader>;
