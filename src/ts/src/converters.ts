/**
 * @module vim-ts
 */

import { Vector2, Vector3, Vector4, AABox, AABox2D, AABox4D, Matrix4x4 } from "./structures"

export interface IConverter<T> {
    get columns(): string[]
    convertFromArray(array: number[]): T
    convertToArray(value: T): number[]
}

export class Vector2Converter implements IConverter<Vector2> {
    get columns(): string[] {
        return [ ".X", ".Y" ]
    }
    convertFromArray(array: number[]): Vector2 {
        return { x: array[0], y: array[1] }
    }
    convertToArray(value: Vector2): number[] {
        return [ value.x, value.y ]
    }
}

export class Vector3Converter implements IConverter<Vector3> {
    get columns(): string[] {
        return [ ".X", ".Y", ".Z" ]
    }
    convertFromArray(array: number[]): Vector3 {
        return { x: array[0], y: array[1], z: array[2] }
    }
    convertToArray(value: Vector3): number[] {
        return [ value.x, value.y, value.z ]
    }
}

export class Vector4Converter implements IConverter<Vector4> {
    get columns(): string[] {
        return [ ".X", ".Y", ".Z", ".W" ]
    }
    convertFromArray(array: number[]): Vector4 {
        return { x: array[0], y: array[1], z: array[2], w: array[3] }
    }
    convertToArray(value: Vector4): number[] {
        return [ value.x, value.y, value.z, value.w ]
    }
}

export class AABox2DConverter implements IConverter<AABox2D> {
    get columns(): string[] {
        return [ ".Min.X", ".Min.Y", ".Max.X", ".Max.Y" ]
    }
    convertFromArray(array: number[]): AABox2D {
        return {
            min: { x: array[0], y: array[1] },
            max: { x: array[2], y: array[3] }
        }
    }
    convertToArray(value: AABox2D): number[] {
        return [
            value.min.x, value.min.y,
            value.max.x, value.max.y
        ]
    }
}

export class AABoxConverter implements IConverter<AABox> {
    get columns(): string[] {
        return [ ".Min.X", ".Min.Y", ".Min.Z", ".Max.X", ".Max.Y", ".Max.Z" ]
    }
    convertFromArray(array: number[]): AABox {
        return {
            min: { x: array[0], y: array[1], z: array[2] },
            max: { x: array[3], y: array[4], z: array[5] }
        }
    }
    convertToArray(value: AABox): number[] {
        return [
            value.min.x, value.min.y, value.min.z,
            value.max.x, value.max.y, value.max.z
        ]
    }
}

export class AABox4DConverter implements IConverter<AABox4D> {
    get columns(): string[] {
        return [ ".Min.X", ".Min.Y", ".Min.Z", ".Min.W", ".Max.X", ".Max.Y", ".Max.Z", ".Max.W" ]
    }
    convertFromArray(array: number[]): AABox4D {
        return {
            min: { x: array[0], y: array[1], z: array[2], w: array[3] },
            max: { x: array[4], y: array[5], z: array[6], w: array[7] }
        }
    }
    convertToArray(value: AABox4D): number[] {
        return [
            value.min.x, value.min.y, value.min.z, value.min.w,
            value.max.x, value.max.y, value.max.z, value.max.w
        ]
    }
}

export class Matrix4x4Converter implements IConverter<Matrix4x4> {
    get columns(): string[] {
        return [
            ".M11", ".M12", ".M13", ".M14",
            ".M21", ".M22", ".M23", ".M24",
            ".M31", ".M32", ".M33", ".M34",
            ".M41", ".M42", ".M43", ".M44"
        ]
    }
    convertFromArray(array: number[]): Matrix4x4 {
        return {
            m11: array[0], m12: array[1], m13: array[2], m14: array[3],
            m21: array[4], m22: array[5], m23: array[6], m24: array[7],
            m31: array[8], m32: array[9], m33: array[10], m34: array[11],
            m41: array[12], m42: array[13], m43: array[14], m44: array[15]
        }
    }
    convertToArray(value: Matrix4x4): number[] {
        return [
            value.m11, value.m12, value.m13, value.m14,
            value.m21, value.m22, value.m23, value.m24,
            value.m31, value.m32, value.m33, value.m34,
            value.m41, value.m42, value.m43, value.m44
        ]
    }
}

export function convert<T>(converter: IConverter<T>, array: (number | undefined)[]) {
    if (array.some(n => n === undefined)) {
        return undefined
    }
    
    return converter.convertFromArray(array.map(n => n!))
}

export function convertArray<T>(converter: IConverter<T>, arrays: (number[] | undefined)[]) {
    if (arrays.some(n => n === undefined)) {
        return undefined
    }

    let result: T[] = []

    for (let i = 0; i < arrays[0]!.length; i++) {
        let array: number[] = []

        for (let j = 0; j < arrays.length; j++) {
            array.push(arrays[j]![i])
        }

        result.push(converter.convertFromArray(array))
    }
    
    return result
}