"use strict";
/**
 * @module vim-ts
 */
Object.defineProperty(exports, "__esModule", { value: true });
exports.convertArray = exports.convert = exports.Matrix4x4Converter = exports.AABox4DConverter = exports.AABoxConverter = exports.AABox2DConverter = exports.Vector4Converter = exports.Vector3Converter = exports.Vector2Converter = void 0;
class Vector2Converter {
    get columns() {
        return [".X", ".Y"];
    }
    convertFromArray(array) {
        return { x: array[0], y: array[1] };
    }
    convertToArray(value) {
        return [value.x, value.y];
    }
}
exports.Vector2Converter = Vector2Converter;
class Vector3Converter {
    get columns() {
        return [".X", ".Y", ".Z"];
    }
    convertFromArray(array) {
        return { x: array[0], y: array[1], z: array[2] };
    }
    convertToArray(value) {
        return [value.x, value.y, value.z];
    }
}
exports.Vector3Converter = Vector3Converter;
class Vector4Converter {
    get columns() {
        return [".X", ".Y", ".Z", ".W"];
    }
    convertFromArray(array) {
        return { x: array[0], y: array[1], z: array[2], w: array[3] };
    }
    convertToArray(value) {
        return [value.x, value.y, value.z, value.w];
    }
}
exports.Vector4Converter = Vector4Converter;
class AABox2DConverter {
    get columns() {
        return [".Min.X", ".Min.Y", ".Max.X", ".Max.Y"];
    }
    convertFromArray(array) {
        return {
            min: { x: array[0], y: array[1] },
            max: { x: array[2], y: array[3] }
        };
    }
    convertToArray(value) {
        return [
            value.min.x, value.min.y,
            value.max.x, value.max.y
        ];
    }
}
exports.AABox2DConverter = AABox2DConverter;
class AABoxConverter {
    get columns() {
        return [".Min.X", ".Min.Y", ".Min.Z", ".Max.X", ".Max.Y", ".Max.Z"];
    }
    convertFromArray(array) {
        return {
            min: { x: array[0], y: array[1], z: array[2] },
            max: { x: array[3], y: array[4], z: array[5] }
        };
    }
    convertToArray(value) {
        return [
            value.min.x, value.min.y, value.min.z,
            value.max.x, value.max.y, value.max.z
        ];
    }
}
exports.AABoxConverter = AABoxConverter;
class AABox4DConverter {
    get columns() {
        return [".Min.X", ".Min.Y", ".Min.Z", ".Min.W", ".Max.X", ".Max.Y", ".Max.Z", ".Max.W"];
    }
    convertFromArray(array) {
        return {
            min: { x: array[0], y: array[1], z: array[2], w: array[3] },
            max: { x: array[4], y: array[5], z: array[6], w: array[7] }
        };
    }
    convertToArray(value) {
        return [
            value.min.x, value.min.y, value.min.z, value.min.w,
            value.max.x, value.max.y, value.max.z, value.max.w
        ];
    }
}
exports.AABox4DConverter = AABox4DConverter;
class Matrix4x4Converter {
    get columns() {
        return [
            ".M11", ".M12", ".M13", ".M14",
            ".M21", ".M22", ".M23", ".M24",
            ".M31", ".M32", ".M33", ".M34",
            ".M41", ".M42", ".M43", ".M44"
        ];
    }
    convertFromArray(array) {
        return {
            m11: array[0], m12: array[1], m13: array[2], m14: array[3],
            m21: array[4], m22: array[5], m23: array[6], m24: array[7],
            m31: array[8], m32: array[9], m33: array[10], m34: array[11],
            m41: array[12], m42: array[13], m43: array[14], m44: array[15]
        };
    }
    convertToArray(value) {
        return [
            value.m11, value.m12, value.m13, value.m14,
            value.m21, value.m22, value.m23, value.m24,
            value.m31, value.m32, value.m33, value.m34,
            value.m41, value.m42, value.m43, value.m44
        ];
    }
}
exports.Matrix4x4Converter = Matrix4x4Converter;
function convert(converter, array) {
    if (array.some(n => n === undefined)) {
        return undefined;
    }
    return converter.convertFromArray(array.map(n => n));
}
exports.convert = convert;
function convertArray(converter, arrays) {
    if (arrays.some(n => n === undefined)) {
        return undefined;
    }
    let result = [];
    for (let i = 0; i < arrays[0].length; i++) {
        let array = [];
        for (let j = 0; j < arrays.length; j++) {
            array.push(arrays[j][i]);
        }
        result.push(converter.convertFromArray(array));
    }
    return result;
}
exports.convertArray = convertArray;
