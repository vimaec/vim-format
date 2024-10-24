import { BFast } from "./bfast";
import { G3dMaterial as G3dMaterials } from "./g3d/g3dMaterials";
import { G3dChunk } from './g3d/g3dChunk';
import { G3dScene } from "./g3d/g3dScene";
import { RemoteValue } from "./http/remoteValue";
import { G3dMesh } from "./g3d/g3dMesh";
export declare class RemoteVimx {
    bfast: BFast;
    scene: RemoteValue<G3dScene>;
    chunkCache: Map<number, RemoteValue<G3dChunk>>;
    constructor(bfast: BFast);
    /**
     * Aborts all downloads from the underlying BFAST.
     */
    abort(): void;
    /**
     * Downloads underlying bfast making all subsequent request local.
     */
    download(): Promise<void>;
    private requestScene;
    getHeader(): Promise<import("./vimHeader").VimHeader>;
    /**
   * Fetches and returns the vimx G3dScene
   */
    getScene(): Promise<G3dScene>;
    /**
     * Fetches and returns the vimx G3dMaterials
     */
    getMaterials(): Promise<G3dMaterials>;
    /**
     * Fetches and returns the vimx G3dMesh with given index
     */
    getChunk(chunk: number): Promise<G3dChunk>;
    private requestChunk;
    getMesh(mesh: number): Promise<G3dMesh>;
}
