#pragma once

#include <type_traits>

#include "constants.h"

namespace vim::math3d::mathOps {
    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        abs(T x) { return std::abs(x); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        acos(T x) { return std::acos(x); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        asin(T x) { return std::asin(x); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        atan(T x) { return std::atan(x); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        cos(T x) { return std::cos(x); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        cosh(T x) { return std::cosh(x); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        exp(T x) { return std::exp(x); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        log(T x) { return std::log(x); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        log10(T x) { return std::log10(x); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        sin(T x) { return std::sin(x); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        sinh(T x) { return std::sinh(x); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        sqrt(T x) { return std::sqrt(x); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        tan(T x) { return std::tan(x); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        tanh(T x) { return std::tanh(x); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, std::int8_t>::type
        sign(T x) { return x > 0 ? 1 : x < 0 ? -1 : 0; }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        magnitude(T x) { return x; }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        magnitudeSquared(T x) { return x * x; }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        inverse(T x) { return (T)1 / x; }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        truncate(T x) { return std::trunc(x); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        ceiling(T x) { return std::ceil(x); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        floor(T x) { return std::floor(x); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        round(T x) { return std::round(x); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        toRadians(T x) { return x * constants::degreesToRadians; }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        toDegrees(T x) { return x * constants::radiansToDegrees; }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        distance(T v1, T v2) { return std::abs(v2 - v1); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, bool>::type
        isInfinity(T v) { return std::isinf(v); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, bool>::type
        isNaN(T v) { return std::isnan(v); }
    
    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, bool>::type
        almostZero(T v, float tolerance = constants::tolerance)  { return std::fabs(v) < tolerance; }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, bool>::type
        almostEquals(T v1, T v2, float tolerance = constants::tolerance)  { return almostZero(v2 - v1, tolerance); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        smoothstep(T v) { return v * v * (3 - 2 * v); }

    template <typename T>
    inline T add(T v1, T v2) { return v1 + v2; }

    template <typename T>
    inline T subtract(T v1, T v2) { return v1 - v2; }

    template <typename T>
    inline T multiply(T v1, T v2) { return v1 * v2; }

    template <typename T>
    inline T divide(T v1, T v2) { return v1 / v2; }

    template <typename T>
    inline T negate(T v) { return -v; }


    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, bool>::type
        within(T v, T min, T max) { return v >= min && v < max; }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        sqr(T x) { return x * x; }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        cube(T x) { return x * x * x; }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        min(T v1, T v2) { return std::min(v1, v2); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        max(T v1, T v2) { return std::max(v1, v2); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, bool>::type
        gt(T v1, T v2) { return v1 > v2; }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, bool>::type
        lt(T v1, T v2) { return v1 < v2; }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, bool>::type
        gt_eq(T v1, T v2) { return v1 >= v2; }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, bool>::type
        lt_eq(T v1, T v2) { return v1 <= v2; }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, bool>::type
        eq(T v1, T v2) { return v1 == v2; }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, bool>::type
        neq(T v1, T v2)  { return v1 != v2; }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        and_op(T a, T b)  { return a & b; }
    inline bool and_op(bool a, bool b)  { return a && b; }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        or_op(T a, T b)  { return a | b; }
    inline bool or_op(bool a, bool b)  { return a || b; }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        nand_op(T a, T b)  { return ~(a & b); }
    inline bool nand_op(bool a, bool b)  { return !(a && b); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        xor_op(T a, T b)  { return a ^ b; }
    inline bool xor_op(bool a, bool b)  { return a != b; }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        nor_op(T a, T b)  { return ~(a | b); }
    inline bool nor_op(bool a, bool b)  { return !(a || b); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        not_op(T a)  { return ~a; }
    inline bool not_op(bool a)  { return !a; }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        divideRoundUp(T a, T b)  { return (a + b - 1) / b; }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, bool>::type
        isEven(T n)  { return n % 2 == 0; }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, bool>::type
        isOdd(T n)  { return n % 2 == 1; }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, bool>::type
        isPowerOfTwo(T v)  { return v > 0 && !(v & (v - 1)); }

    template <typename T>
    inline T lerp(T v1, T v2, float t)  { return v1 + (v2 - v1) * t; }

    template <typename T>
    inline T inverseLerp(T v, T a, T b)  { return (v - a) / (b - a); }

    template <typename T>
    inline T lerpPrecise(T v1, T v2, float t)  { return ((1 - t) * v1) + (v2 * t); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        clampLower(T v, T min)  { return std::max(v, min); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        clampUpper(T v, T max)  { return std::min(v, max); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        clamp(T v, T min, T max)  { return std::max(std::min(v, max), min); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        average(T v1, T v2)  { return lerp(v1, v2, 0.5f); }

    template <typename T>
    inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
        barycentric(T v1, T v2, T v3, T u, T v)  { return v1 + (v2 - v1) * u + (v3 - v1) * v; }
}