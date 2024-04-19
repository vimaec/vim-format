/**
 * @module vim-ts
 */
import { Vector2, Vector3, Vector4, AABox, AABox2D, AABox4D, Matrix4x4 } from "./structures";
export interface IConverter<T> {
    get columns(): string[];
    convertFromArray(array: number[]): T;
    convertToArray(value: T): number[];
}
export declare class Vector2Converter implements IConverter<Vector2> {
    get columns(): string[];
    convertFromArray(array: number[]): Vector2;
    convertToArray(value: Vector2): number[];
}
export declare class Vector3Converter implements IConverter<Vector3> {
    get columns(): string[];
    convertFromArray(array: number[]): Vector3;
    convertToArray(value: Vector3): number[];
}
export declare class Vector4Converter implements IConverter<Vector4> {
    get columns(): string[];
    convertFromArray(array: number[]): Vector4;
    convertToArray(value: Vector4): number[];
}
export declare class AABox2DConverter implements IConverter<AABox2D> {
    get columns(): string[];
    convertFromArray(array: number[]): AABox2D;
    convertToArray(value: AABox2D): number[];
}
export declare class AABoxConverter implements IConverter<AABox> {
    get columns(): string[];
    convertFromArray(array: number[]): AABox;
    convertToArray(value: AABox): number[];
}
export declare class AABox4DConverter implements IConverter<AABox4D> {
    get columns(): string[];
    convertFromArray(array: number[]): AABox4D;
    convertToArray(value: AABox4D): number[];
}
export declare class Matrix4x4Converter implements IConverter<Matrix4x4> {
    get columns(): string[];
    convertFromArray(array: number[]): Matrix4x4;
    convertToArray(value: Matrix4x4): number[];
}
export declare function convert<T>(converter: IConverter<T>, array: (number | undefined)[]): T;
export declare function convertArray<T>(converter: IConverter<T>, arrays: (number[] | undefined)[]): T[];
