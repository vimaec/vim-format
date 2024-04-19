/**
 * @module vim-ts
 */
export declare type Vector2 = {
    x: number;
    y: number;
};
export declare type Vector3 = {
    x: number;
    y: number;
    z: number;
};
export declare type Vector4 = {
    x: number;
    y: number;
    z: number;
    w: number;
};
export declare type AABox = {
    min: Vector3;
    max: Vector3;
};
export declare type AABox2D = {
    min: Vector2;
    max: Vector2;
};
export declare type AABox4D = {
    min: Vector4;
    max: Vector4;
};
export declare type Matrix4x4 = {
    m11: number;
    m12: number;
    m13: number;
    m14: number;
    m21: number;
    m22: number;
    m23: number;
    m24: number;
    m31: number;
    m32: number;
    m33: number;
    m34: number;
    m41: number;
    m42: number;
    m43: number;
    m44: number;
};
