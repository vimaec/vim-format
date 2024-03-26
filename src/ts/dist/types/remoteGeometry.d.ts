import { BFast } from "./bfast";
import { G3dMaterial as G3dMaterials } from "./g3dMaterials";
import { G3dMesh } from "./g3dMesh";
import { G3dScene } from "./g3dScene";
import { RemoteValue } from "./remoteValue";
export declare class RemoteGeometry {
    bfast: BFast;
    scene: RemoteValue<G3dScene>;
    sceneRaw: RemoteValue<G3dScene>;
    constructor(bfast: BFast);
    static fromPath(path: string): Promise<RemoteGeometry>;
    /**
     * Aborts all downloads from the underlying BFAST.
     */
    abort(): void;
    /**
     * Downloads underlying bfast making all subsequent request local.
     */
    download(): Promise<void>;
    /**
     * Fetches and returns the vimx G3dMeshIndex
     */
    private requestIndex;
    private requestIndexRaw;
    getIndex(): Promise<G3dScene>;
    getIndexRaw(): Promise<G3dScene>;
    /**
     * Fetches and returns the vimx G3dMaterials
     */
    getMaterials(): Promise<G3dMaterials>;
    /**
     * Fetches and returns the vimx G3dMesh with given index
     */
    getMesh(mesh: number): Promise<G3dMesh>;
    /**
     * Fetches and returns the vimx G3dMaterials
     */
    getMaterialsRaw(): Promise<G3dMaterials>;
    /**
     * Fetches and returns the vimx G3dMesh with given index
     */
    getMeshRaw(mesh: number): Promise<G3dMesh>;
}
