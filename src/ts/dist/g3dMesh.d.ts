/**
 * @module vim-ts
 */
import { G3dChunk } from './g3dChunk';
import { MeshSection } from './g3d';
import { G3dScene } from './g3dScene';
export declare class G3dMesh {
    readonly scene: G3dScene;
    readonly chunk: G3dChunk;
    readonly index: number;
    constructor(scene: G3dScene, chunk: G3dChunk, index: number);
    getVertexStart(section?: MeshSection): number;
    getVertexEnd(section?: MeshSection): number;
    getVertexCount(section?: MeshSection): number;
    getIndexStart(section?: MeshSection): number;
    getIndexEnd(section?: MeshSection): number;
    getIndexCount(section?: MeshSection): number;
    getHasTransparency(mesh: number): boolean;
    getSubmeshStart(section: MeshSection): number;
    getSubmeshEnd(section: MeshSection): number;
    getSubmeshCount(section: MeshSection): number;
    getSubmeshIndexStart(submesh: number): number;
    getSubmeshIndexEnd(submesh: number): number;
    getSubmeshIndexCount(submesh: number): number;
    getSubmeshVertexStart(submesh: number): number;
    getSubmeshVertexEnd(submesh: number): number;
    getSubmeshVertexCount(submesh: number): number;
}
