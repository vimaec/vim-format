#pragma once

#include <iostream>
#include <vector>
#include <array>
#include <numeric>
#include <functional>
#include <initializer_list>
#include <optional>
#include <cassert>

#include "constants.h"
#include "hash.h"

namespace vim::math3d {
    namespace mathOps {
        template <typename T = double>
        inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
            percentage(T denominator, T numerator) { return (numerator / denominator) * 100; }

        /// <summary>
        /// Calculate the nearest power of 2 from the input number
        /// </summary>
        template <typename T = std::int32_t>
        inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
            toNearestPowOf2(T x) { return (T)(std::pow((T)2, std::round(std::log(x) / std::log(2)))); }

        /// <summary>
        /// Performs a Catmull-Rom interpolation using the specified positions.
        /// </summary>
        template <typename T = float>
        inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
            catmullRom(T value1, T value2, T value3, T value4, T amount) {
            // Using formula from http://www.mvps.org/directx/articles/catmull/
            // Internally using doubles not to lose precision
            auto amountSquared = amount * amount;
            auto amountCubed = amountSquared * amount;
            return (T)(0.5 * (2.0 * value2 +
                (value3 - value1) * amount +
                (2.0 * value1 - 5.0 * value2 + 4.0 * value3 - value4) * amountSquared +
                (3.0 * value2 - value1 - 3.0 * value3 + value4) * amountCubed));
        }

        /// <summary>
        /// Performs a Hermite spline interpolation.
        /// </summary>
        template <typename T = float>
        inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
            hermite(T value1, T tangent1, T value2, T tangent2, T amount) {
            // All transformed to double not to lose precision
            // Otherwise, for high numbers of param:amount the result is NaN instead of Infinity
            T v1 = value1, v2 = value2, t1 = tangent1, t2 = tangent2, s = amount, result;
            auto sCubed = s * s * s;
            auto sSquared = s * s;

            if (amount == 0) { return value1; }
            if (amount == 1) { return value2; }
            return (2 * v1 - 2 * v2 + t2 + t1) * sCubed + (3 * v2 - 3 * v1 - 2 * t1 - t2) * sSquared + t1 * s + v1;
        }

        /// <summary>
        /// Interpolates between two values using a cubic equation (Hermite),
        /// clamping the amount to 0 to 1
        /// </summary>
        template <typename T = double>
        inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
            smoothStep(T value1, T value2, T amount) {
            return hermite(value1, (T)0, value2, (T)0, std::max(std::min(amount, (T)1), (T)0));
        }

        /// <summary>
        /// Reduces a given angle to a value between π and -π.
        /// </summary>
        /// <param name="angle">The angle to reduce, in radians.</param>
        /// <returns>The new angle, in radians.</returns>
        template <typename T = float>
        inline typename std::enable_if<std::is_arithmetic<T>::value, T>::type
            wrapAngle(T angle) {
            if ((angle > -constants::pi) && (angle <= constants::pi))
                return angle;
            angle %= constants::twoPi;
            if (angle <= -constants::pi)
                return angle + constants::twoPi;
            if (angle > constants::pi)
                return angle - constants::twoPi;
            return angle;
        }

        template <typename T = double>
        inline typename std::enable_if<std::is_arithmetic<T>::value, bool>::type
            isNonZeroAndValid(T self, float tolerance = constants::tolerance) { return !std::isinf(self) && !std::isnan(self) && std::fabs(self) > tolerance; }
    }

    template<typename TContainer, typename TPart>
    struct Mappable {
        virtual TContainer map(std::function<TPart(const TPart&)> f) const = 0;
    };

    template<typename TVector>
    struct Points {
        virtual std::size_t numPoints() const = 0;
        virtual TVector getPoint(std::size_t n) const = 0;
    };

    //template<typename TSelf, typename TMatrix4x4>
    //struct Transformable3D {
    //    virtual TSelf transform(const TMatrix4x4& mat) = 0;
    //};

    template <typename T = float>
    struct Stats final {
        std::size_t Count;
        T Min;
        T Max;
        T Sum;

        const Stats(std::size_t count, T min, T max, T sum) : Count(count), Min(min), Max(max), Sum(sum) {}

        inline T average() const { return Sum / Count; }
        inline T extents() const { return Max - Min; }
        inline T middle() const { return extents() / 2 + Min; }

        inline static Stats<T> stats(const std::initializer_list<T>& self) {
            Stats<T> result = Stats<T>(0, T(), T(), T());
            for (const auto& elem : self) {
                result.Count += 1;
                result.Min = min(result.Min, elem);
                result.Max = max(result.Max, elem);
                result.Sum += elem;
            }
            return result;
        }
        inline static T sum(const std::initializer_list<T>& self) { return stats<T>(self).Sum; }
        inline static T average(const std::initializer_list<T>& self) { return stats<T>(self).average(); }
        inline static T extents(const std::initializer_list<T>& self) { return stats<T>(self).extents(); }
        inline static T middle(const std::initializer_list<T>& self) { return stats<T>(self).middle(); }

        inline std::size_t hash() const { return hash::combine(Count, Min, Max, Sum); }

        inline friend bool operator==(const Stats<T>& s, const Stats<T>& other) { return s.Count == other.Count && s.Min == other.Min && s.Max == other.Max && s.Sum == other.Sum; }
        inline friend bool operator!=(const Stats<T>& s, const Stats<T>& other) { return !(s == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const Stats<T>& v) { return (out << "Stats(Count = " << v.Count << ", Min = " << v.Min << ", Max = " << v.Max << ", Sum = " << v.Sum << ")"); }
    };

    enum class ContainmentType { disjoint, contains, intersects };
    enum class PlaneIntersectionType { front, back, intersecting };

    struct Byte2 final {
        std::uint8_t X;
        std::uint8_t Y;

        const Byte2(std::uint8_t x, std::uint8_t y) : X(x), Y(y) {}
        const Byte2(std::uint8_t value) : X(value), Y(value) {}

        inline static const Byte2 zero() { return Byte2(0); }
        inline static const Byte2 minValue() { return Byte2(std::numeric_limits<std::uint8_t>::min()); }
        inline static const Byte2 maxValue() { return Byte2(std::numeric_limits<std::uint8_t>::max()); }

        inline Byte2 setX(std::uint8_t x) const { return Byte2(x, Y); }
        inline Byte2 setY(std::uint8_t y) const { return Byte2(X, y); }

        inline std::size_t hash() const { return hash::combine(X, Y); } //(Y)
        inline bool almostEquals(const Byte2& x, float tolerance = constants::tolerance) const {
            return std::fabs(X - x.X) < tolerance && std::fabs(Y - x.Y) < tolerance;
        }

        inline friend bool operator==(const Byte2& b, const Byte2& other) { return b.X == other.X && b.Y == other.Y; }
        inline friend bool operator!=(const Byte2& b, const Byte2& other) { return !(b == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const Byte2& v) { return (out << "Byte2(X = " << v.X << ", Y = " << v.Y << ")"); }
    };

    struct Byte3 final {
        std::uint8_t X;
        std::uint8_t Y;
        std::uint8_t Z;

        const Byte3(std::uint8_t x, std::uint8_t y, std::uint8_t z) : X(x), Y(y), Z(z) {}
        const Byte3(std::uint8_t value) : X(value), Y(value), Z(value) {}

        inline static const Byte3 zero() { return Byte3(0); }
        inline static const Byte3 minValue() { return Byte3(std::numeric_limits<std::uint8_t>::min()); }
        inline static const Byte3 maxValue() { return Byte3(std::numeric_limits<std::uint8_t>::max()); }

        inline Byte3 setX(std::uint8_t x) const { return Byte3(x, Y, Z); }
        inline Byte3 setY(std::uint8_t y) const { return Byte3(X, y, Z); }
        inline Byte3 setZ(std::uint8_t z) const { return Byte3(X, Y, z); }

        inline std::size_t hash() const { return hash::combine(X, Y, Z); }
        inline bool almostEquals(const Byte3& x, float tolerance = constants::tolerance) const {
            return std::fabs(X - x.X) < tolerance && std::fabs(Y - x.Y) < tolerance && std::fabs(Z - x.Z) < tolerance;
        }

        inline friend bool operator==(const Byte3& b, const Byte3& other) { return b.X == other.X && b.Y == other.Y && b.Z == other.Z; }
        inline friend bool operator!=(const Byte3& b, const Byte3& other) { return !(b == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const Byte3& v) { return (out << "Byte3(X = " << v.X << ", Y = " << v.Y << ", Z = " << v.Z << ")"); }
    };

    struct Byte4 final {
        std::uint8_t X;
        std::uint8_t Y;
        std::uint8_t Z;
        std::uint8_t W;

        const Byte4(std::uint8_t x, std::uint8_t y, std::uint8_t z, std::uint8_t w) : X(x), Y(y), Z(z), W(w) {}
        const Byte4(std::uint8_t value) : X(value), Y(value), Z(value), W(value) {}

        inline static const Byte4 zero() { return Byte4(0); }
        inline static const Byte4 minValue() { return Byte4(std::numeric_limits<std::uint8_t>::min()); }
        inline static const Byte4 maxValue() { return Byte4(std::numeric_limits<std::uint8_t>::max()); }

        inline Byte4 setX(std::uint8_t x) const { return Byte4(x, Y, Z, W); }
        inline Byte4 setY(std::uint8_t y) const { return Byte4(X, y, Z, W); }
        inline Byte4 setZ(std::uint8_t z) const { return Byte4(X, Y, z, W); }
        inline Byte4 setW(std::uint8_t w) const { return Byte4(X, Y, Z, w); }

        inline std::size_t hash() const { return hash::combine(X, Y, Z, W); }
        inline bool almostEquals(const Byte4& x, float tolerance = constants::tolerance) const {
            return std::fabs(X - x.X) < tolerance && std::fabs(Y - x.Y) < tolerance && std::fabs(Z - x.Z) < tolerance && std::fabs(W - x.W) < tolerance;
        }

        inline friend bool operator==(const Byte4& b, const Byte4& other) { return b.X == other.X && b.Y == other.Y && b.Z == other.Z && b.W == other.W; }
        inline friend bool operator!=(const Byte4& b, const Byte4& other) { return !(b == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const Byte4& v) { return (out << "Byte4(X = " << v.X << ", Y = " << v.Y << ", Z = " << v.Z << ", W = " << v.W << ")"); }
    };

    struct ColorHDR final {
        float R;
        float G;
        float B;
        float A;

        const ColorHDR(float r, float g, float b, float a) : R(r), G(g), B(b), A(a) {}
        const ColorHDR(float value) : R(value), G(value), B(value), A(value) {}

        inline static const ColorHDR zero() { return  ColorHDR(0); }
        inline static const ColorHDR minValue() { return ColorHDR(std::numeric_limits<float>::min()); }
        inline static const ColorHDR maxValue() { return ColorHDR(std::numeric_limits<float>::max()); }

        inline ColorHDR setR(float r) const { return ColorHDR(r, G, B, A); }
        inline ColorHDR setG(float g) const { return ColorHDR(R, g, B, A); }
        inline ColorHDR setB(float b) const { return ColorHDR(R, G, b, A); }
        inline ColorHDR setA(float a) const { return ColorHDR(R, G, B, a); }

        inline std::size_t hash() const { return hash::combine(R, G, B, A); }
        inline bool almostEquals(const ColorHDR& x, float tolerance = constants::tolerance) const {
            return std::fabs(R - x.R) < tolerance && std::fabs(G - x.G) < tolerance
                && std::fabs(B - x.B) < tolerance && std::fabs(A - x.A) < tolerance;
        }

        inline friend bool operator==(const ColorHDR& o, const ColorHDR& other) { return o.R == other.R && o.G == other.G && o.B == other.B && o.A == other.A; }
        inline friend bool operator!=(const ColorHDR& o, const ColorHDR& other) { return !(o == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const ColorHDR& v) { return (out << "ColorHDR(R = " << v.R << ", G = " << v.G << ", B = " << v.B << ", A = " << v.A << ")"); }
    };

    struct ColorRGB final {
        std::uint8_t R;
        std::uint8_t G;
        std::uint8_t B;

        const ColorRGB(std::uint8_t r, std::uint8_t g, std::uint8_t b) : R(r), G(g), B(b) {}
        const ColorRGB(std::uint8_t value) : R(value), G(value), B(value) {}

        inline static const ColorRGB zero() { return ColorRGB(0); }
        inline static const ColorRGB minValue() { return ColorRGB(std::numeric_limits<std::uint8_t>::min()); }
        inline static const ColorRGB maxValue() { return ColorRGB(std::numeric_limits<std::uint8_t>::max()); }

        inline ColorRGB setR(std::uint8_t r) const { return ColorRGB(r, G, B); }
        inline ColorRGB setG(std::uint8_t g) const { return ColorRGB(R, g, B); }
        inline ColorRGB setB(std::uint8_t b) const { return ColorRGB(R, G, b); }

        inline std::size_t hash() const { return hash::combine(R, G, B); }
        inline bool almostEquals(const ColorRGB& x, float tolerance = constants::tolerance) const {
            return std::fabs(R - x.R) < tolerance  && std::fabs(G - x.G) < tolerance && std::fabs(B - x.B) < tolerance;
        }

        inline friend bool operator==(const ColorRGB& o, const ColorRGB& other) { return o.R == other.R && o.G == other.G && o.B == other.B; }
        inline friend bool operator!=(const ColorRGB& o, const ColorRGB& other) { return !(o == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const ColorRGB& v) { return (out << "ColorRGB(R = " << v.R << ", G = " << v.G << ", B = " << v.B << ")"); }
    };

    struct ColorRGBA final {
        std::uint8_t R;
        std::uint8_t G;
        std::uint8_t B;
        std::uint8_t A;

        const ColorRGBA(std::uint8_t r, std::uint8_t g, std::uint8_t b, std::uint8_t a) : R(r), G(g), B(b), A(a) {}
        const ColorRGBA(std::uint8_t value = 0) : R(value), G(value), B(value), A(value) {}

        inline static const ColorRGBA zero() { return ColorRGBA(0); }
        inline static const ColorRGBA minValue() { return ColorRGBA(std::numeric_limits<std::uint8_t>::min()); }
        inline static const ColorRGBA maxValue() { return ColorRGBA(std::numeric_limits<std::uint8_t>::max()); }
        inline static const ColorRGBA lightRed() { return ColorRGBA(255, 128, 128, 255); }
        inline static const ColorRGBA darkRed() { return ColorRGBA(255, 0, 0, 255); }
        inline static const ColorRGBA lightGreen() { return ColorRGBA(128, 255, 128, 255); }
        inline static const ColorRGBA darkGreen() { return ColorRGBA(0, 255, 0, 255); }
        inline static const ColorRGBA lightBlue() { return ColorRGBA(128, 128, 255, 255); }
        inline static const ColorRGBA darkBlue() { return ColorRGBA(0, 0, 255, 255); }

        inline ColorRGBA setR(std::uint8_t r) const { return ColorRGBA(r, G, B, A); }
        inline ColorRGBA setG(std::uint8_t g) const { return ColorRGBA(R, g, B, A); }
        inline ColorRGBA setB(std::uint8_t b) const { return ColorRGBA(R, G, b, A); }
        inline ColorRGBA setA(std::uint8_t a) const { return ColorRGBA(R, G, B, a); }

        inline std::size_t hash() const { return hash::combine(R, G, B, A); }
        inline bool almostEquals(const ColorRGBA& x, float tolerance = constants::tolerance) const {
            return std::fabs(R - x.R) < tolerance && std::fabs(G - x.G) < tolerance
                && std::fabs(B - x.B) < tolerance && std::fabs(A - x.A) < tolerance;
        }

        inline friend bool operator==(const ColorRGBA& c, const ColorRGBA& other) { return c.R == other.R && c.G == other.G && c.B == other.B && c.A == other.A; }
        inline friend bool operator!=(const ColorRGBA& c, const ColorRGBA& other) { return !(c == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const ColorRGBA& v) { return (out << "ColorRGBA(R = " << v.R << ", G = " << v.G << ", B = " << v.B << ", A = " << v.A << ")"); }
    };

    struct Complex final {
        double Real;
        double Imaginary;

        const Complex(double real, double imaginary) : Real(real), Imaginary(imaginary) {}
        const Complex(double value) : Real(value), Imaginary(value) {}

        inline static const Complex zero() { return Complex(0.0); }
        inline static const Complex minValue() { return Complex(std::numeric_limits<double>::min()); }
        inline static const Complex maxValue() { return Complex(std::numeric_limits<double>::max()); }
        inline static const Complex one() { return Complex(1); }
        inline static const Complex unitReal() { return Complex(1.0, 0.0); }
        inline static const Complex unitImaginary() { return Complex(0.0, 1.0); }

        inline Complex setReal(double real) const { return Complex(real, Imaginary); }
        inline Complex setImaginary(double imaginary) const { return Complex(Real, imaginary); }

        inline std::size_t hash() const { return hash::combine(Real, Imaginary); }
        inline bool almostEquals(const Complex& x, float tolerance = constants::tolerance) const {
            return std::fabs(Real - x.Real) <= tolerance && std::fabs(Imaginary - x.Imaginary) <= tolerance;
        }
        inline static double dot(const Complex& value1, const Complex& value2) { return value1.Real * value2.Real + value1.Imaginary * value2.Imaginary; }
        inline double dot(const Complex& value) const { return dot(*this, value); }
        inline bool almostZero(float tolerance = constants::tolerance) const {
            return std::fabs(Real) < tolerance
                && std::fabs(Imaginary) < tolerance;
        }

        inline double minComponent() const { return std::min(Real, Imaginary); }
        inline double maxComponent() const { return std::max(Real, Imaginary); }
        inline double sumComponents() const { return Real + Imaginary; }
        inline double sumSqrComponents() const { return Real * Real + Imaginary * Imaginary; }
        inline double productComponents() const { return Real * Imaginary; }
        inline double getComponent(int n) const { return n == 0 ? Real : Imaginary; }
        inline bool anyComponentNegative() const { return minComponent() < 0; }
        inline double magnitudeSquared() const { return sumSqrComponents(); }
        inline double magnitude() const { return std::sqrt(magnitudeSquared()); }
        inline bool isnan() const { return std::isnan(Real) || std::isnan(Imaginary); }
        inline bool isinf() const { return std::isinf(Real) || std::isinf(Imaginary); }
        inline int compare(const Complex& x) const { return std::signbit(magnitudeSquared() - x.magnitudeSquared()); }

        inline friend Complex operator-(const Complex& o) { return { -o.Real, -o.Imaginary }; }

        inline friend bool operator==(const Complex& o, const Complex& other) { return o.Real == other.Real && o.Imaginary == other.Imaginary; }
        inline friend bool operator!=(const Complex& o, const Complex& other) { return !(o == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const Complex& v) { return (out << "Complex(Real = " << v.Real << ", Imaginary = " << v.Imaginary << ")"); }
        
        inline friend Complex operator+(const Complex& lhs, const Complex& rhs) { return Complex(lhs.Real + rhs.Real, lhs.Imaginary + rhs.Imaginary); }
        inline friend Complex operator+(const Complex& lhs, float rhs) { return Complex(lhs.Real + rhs, lhs.Imaginary + rhs); }
        inline friend Complex operator+(float lhs, const Complex& rhs) { return Complex(lhs + rhs.Real, lhs + rhs.Imaginary); }

        inline friend Complex operator-(const Complex& lhs, const Complex& rhs) { return Complex(lhs.Real - rhs.Real, lhs.Imaginary - rhs.Imaginary); }
        inline friend Complex operator-(const Complex& lhs, float rhs) { return Complex(lhs.Real - rhs, lhs.Imaginary - rhs); }
        inline friend Complex operator-(float lhs, const Complex& rhs) { return Complex(lhs - rhs.Real, lhs - rhs.Imaginary); }

        inline friend Complex operator*(const Complex& lhs, const Complex& rhs) { return Complex(lhs.Real * rhs.Real, lhs.Imaginary * rhs.Imaginary); }
        inline friend Complex operator*(const Complex& lhs, float rhs) { return Complex(lhs.Real * rhs, lhs.Imaginary * rhs); }
        inline friend Complex operator*(float lhs, const Complex& rhs) { return Complex(lhs * rhs.Real, lhs * rhs.Imaginary); }

        inline friend Complex operator/(const Complex& lhs, const Complex& rhs) { return Complex(lhs.Real / rhs.Real, lhs.Imaginary / rhs.Imaginary); }
        inline friend Complex operator/(const Complex& lhs, float rhs) { return Complex(lhs.Real / rhs, lhs.Imaginary / rhs); }
        inline friend Complex operator/(float lhs, const Complex& rhs) { return Complex(lhs / rhs.Real, lhs / rhs.Imaginary); }

        inline friend bool operator<(const Complex& x0, const Complex& x1) { return x0.compare(x1) < 0; }
        inline friend bool operator<=(const Complex& x0, const Complex& x1) { return x0.compare(x1) <= 0; }
        inline friend bool operator>(const Complex& x0, const Complex& x1) { return x0.compare(x1) > 0; }
        inline friend bool operator>=(const Complex& x0, const Complex& x1) { return x0.compare(x1) >= 0; }
    };

    template <typename T = float>
    struct Euler final {
        T Yaw;
        T Pitch;
        T Roll;

        const Euler(T yaw, T pitch, T roll) : Yaw(yaw), Pitch(pitch), Roll(roll) {}
        const Euler(T value) : Yaw(value), Pitch(value), Roll(value) {}

        inline static const Euler zero() { return Euler(0); }
        inline static const Euler minValue() { return Euler(std::numeric_limits<T>::min()); }
        inline static const Euler maxValue() { return Euler(std::numeric_limits<T>::max()); }
        inline static const Euler one() { return Euler(1); }
        inline static const Euler unitYaw() { return Euler(1, 0, 0); }
        inline static const Euler unitPitch() { return Euler(0, 1, 0); }
        inline static const Euler unitRoll() { return Euler(0, 0, 1); }

        inline Euler setYaw(T x) const { return Euler(x, Pitch, Roll); }
        inline Euler setPitch(T y) const { return Euler(Yaw, y, Roll); }
        inline Euler setRoll(T z) const { return Euler(Yaw, Pitch, z); }

        inline std::size_t hash() const { return hash::combine((Yaw), (Pitch), (Roll)); }
        inline bool almostEquals(const Euler& x, float tolerance = constants::tolerance) const {
            return std::fabs(Yaw - x.Yaw) <= tolerance
                && std::fabs(Pitch - x.Pitch) <= tolerance
                && std::fabs(Roll - x.Roll) <= tolerance;
        }
        inline static T dot(const Euler& value1, const Euler& value2) {
            return value1.Yaw * value2.Yaw + value1.Pitch * value2.Pitch + value1.Roll * value2.Roll;
        }
        inline T dot(const Euler& value) const { return dot(*this, value); }
        inline bool almostZero(float tolerance = constants::tolerance) const {
            return std::fabs(Yaw) < tolerance
                && std::fabs(Pitch) < tolerance
                && std::fabs(Roll) < tolerance;
        }
        inline T minComponent() const { return std::min(std::min(Yaw, Pitch), Roll); }
        inline T maxComponent() const { return std::max(std::max(Yaw, Pitch), Roll); }
        inline T sumComponents() const { return Yaw + Pitch + Roll; }
        inline T sumSqrComponents() const { return Yaw * Yaw + Pitch * Pitch + Roll * Roll; }
        inline T productComponents() const { return Yaw * Pitch * Roll; }
        inline T getComponent(int n) const { return n == 0 ? Yaw : n == 1 ? Pitch : Roll; }
        inline bool anyComponentNegative() const { return minComponent() < 0.0; }
        inline T magnitudeSquared() const { return sumSqrComponents(); }
        inline T magnitude() const { return std::sqrt(magnitudeSquared()); }
        inline bool isnan() const { return std::isnan(Yaw) || std::isnan(Pitch) || std::isnan(Roll); }
        inline bool isinf() const { return std::isinf(Yaw) || std::isinf(Pitch) || std::isinf(Roll); }
        inline int compare(const Euler& x) const { return std::signbit(magnitudeSquared() - x.magnitudeSquared()); }

        inline friend Euler operator-(const Euler& o) { return { -o.Yaw, -o.Pitch, -o.Roll }; }
        inline friend bool operator==(const Euler& o, const Euler& other) { return o.Yaw == other.Yaw && o.Pitch == other.Pitch && o.Roll == other.Roll; }
        inline friend bool operator!=(const Euler& o, const Euler& other) { return !(o == other); }

        inline friend std::ostream& operator<<(std::ostream& out, const Euler& v) { return (out << "Euler(Yaw = " << v.Yaw << ", Pitch = " << v.Pitch << ", Roll = " << v.Roll << ")"); }
        inline friend Euler operator+(const Euler& lhs, const Euler& rhs) { return Euler(lhs.Yaw + rhs.Yaw, lhs.Pitch + rhs.Pitch, lhs.Roll + rhs.Roll); }
        inline friend Euler operator+(const Euler& lhs, T rhs) { return Euler(lhs.Yaw + rhs, lhs.Pitch + rhs, lhs.Roll + rhs); }
        inline friend Euler operator+(T lhs, const Euler& rhs) { return Euler(lhs + rhs.Yaw, lhs + rhs.Pitch, lhs + rhs.Roll); }

        //inline friend Euler operator-(const Vector3<T>& rhs) { return Euler(lhs.Yaw - rhs.X, lhs.Pitch - rhs.Y, lhs.Roll - rhs.Z); }
        inline Euler operator-(T rhs) { return Euler(Yaw - rhs, Pitch - rhs, Roll - rhs); }
        inline friend Euler operator-(T lhs, const Euler& rhs) { return Euler(lhs - rhs.Yaw, lhs - rhs.Pitch, lhs - rhs.Roll); }

        inline friend Euler operator *(const Euler& value1, const Euler& value2) { return Euler(value1.Yaw * value2.Yaw, value1.Pitch * value2.Pitch, value1.Roll * value2.Roll); }
        inline friend Euler operator *(const Euler& value1, T value2) { return Euler(value1.Yaw * value2, value1.Pitch * value2, value1.Roll * value2); }
        inline friend Euler operator *(T value1, Euler& value2) { return Euler(value1 * value2.Yaw, value1 * value2.Pitch, value1 * value2.Roll); }

        inline friend Euler operator/(const Euler& lhs, const Euler& rhs) { return Euler(lhs.Yaw / rhs.Yaw, lhs.Pitch / rhs.Pitch, lhs.Roll / rhs.Roll); }
        inline friend Euler operator/(const Euler& lhs, T rhs) { return Euler(lhs.Yaw / rhs, lhs.Pitch / rhs, lhs.Roll / rhs); }
        inline friend Euler operator/(T lhs, const Euler& rhs) { return Euler(lhs / rhs.Yaw, lhs / rhs.Pitch, lhs / rhs.Roll); }

        inline friend bool operator<(const Euler& x0, const Euler& x1) { return x0.compare(x1) < 0; }
        inline friend bool operator<=(const Euler& x0, const Euler& x1) { return x0.compare(x1) <= 0; }
        inline friend bool operator>(const Euler& x0, const Euler& x1) { return x0.compare(x1) > 0; }
        inline friend bool operator>=(const Euler& x0, const Euler& x1) { return x0.compare(x1) >= 0; }
    };
    
#define FEuler Euler<float>
#define DEuler Euler<double>

    struct Int2 final {
        std::int32_t X;
        std::int32_t Y;

        const Int2(std::int32_t x, std::int32_t y) : X(x), Y(y) {}
        const Int2(std::int32_t value = 0) : X(value), Y(value) {}

        inline static const Int2 zero() { return Int2(0); }
        inline static const Int2 minValue() { return Int2(std::numeric_limits<std::int32_t>::min()); }
        inline static const Int2 maxValue() { return Int2(std::numeric_limits<std::int32_t>::max()); }
        inline static const Int2 one() { return Int2(1); }
        inline static const Int2 unitX() { return Int2(1, 0); }
        inline static const Int2 unitY() { return Int2(0, 1); }

        inline Int2 setX(std::int32_t x) const { return Int2(x, Y); }
        inline Int2 setY(std::int32_t y) const { return Int2(X, y); }

        inline std::size_t hash() const { return hash::combine(X, Y); }
        inline bool almostEquals(const Int2& x, float tolerance = constants::tolerance) const {
            return std::fabs(X - x.X) <= tolerance && std::fabs(Y - x.Y) <= tolerance;
        }
        inline static std::int32_t dot(const Int2& value1, const Int2& value2) { return value1.X * value2.X + value1.Y * value2.Y; }
        inline std::int32_t dot(const Int2& value) const { return dot(*this, value); }
        inline bool almostZero(float tolerance = constants::tolerance) const {
            return std::fabs(X) < tolerance && std::fabs(Y) < tolerance;
        }
        inline std::int32_t minComponent() const { return std::min(X, Y); }
        inline std::int32_t maxComponent() const { return std::max(X, Y); }
        inline std::int32_t sumComponents() const { return X + Y; }
        inline std::int32_t sumSqrComponents() const { return X * X + Y * Y; }
        inline std::int32_t productComponents() const { return X * Y; }
        inline std::int32_t getComponent(int n) const { return n == 0 ? X : Y; }
        inline double magnitudeSquared() const { return sumSqrComponents(); }
        inline double magnitude() const { return std::sqrt(magnitudeSquared()); }
        inline bool anyComponentNegative() const { return minComponent() < 0.0; }
        /* inline bool isnan() const { return std::isnan(X) || std::isnan(Y); }
         inline bool isinf() const { return std::isinf(X) || std::isinf(Y); }*/
        inline int compare(const Int2& x) const { return std::signbit(magnitudeSquared() - x.magnitudeSquared()); }

        inline friend Int2 operator-(const Int2& l) { return Int2(- l.X, -l.Y); }
        inline friend bool operator==(const Int2& o, const Int2& other) { return o.X == other.X && o.Y == other.Y; }
        inline friend bool operator!=(const Int2& o, const Int2& other) { return !(o == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const Int2& v) { return (out << "Int2(X = " << v.X << ", Y = " << v.Y << ")"); }
        
        inline friend Int2 operator+(const Int2& lhs, const Int2& rhs) { return Int2(lhs.X + rhs.X, lhs.Y + rhs.Y); }
        inline friend Int2 operator+(const Int2& lhs, std::int32_t rhs) { return Int2(lhs.X + rhs, lhs.Y + rhs); }
        inline friend Int2 operator+(std::int32_t lhs, const Int2& rhs) { return Int2(lhs + rhs.X, lhs + rhs.Y); }

        inline friend Int2 operator-(const Int2& lhs, const Int2& rhs) { return Int2(lhs.X - rhs.X, lhs.Y - rhs.Y); }
        inline friend Int2 operator-(const Int2& lhs, std::int32_t rhs) { return Int2(lhs.X - rhs, lhs.Y - rhs); }
        inline friend Int2 operator-(std::int32_t lhs, const Int2& rhs) { return Int2(lhs - rhs.X, lhs - rhs.Y); }

        inline friend Int2 operator*(const Int2& lhs, const Int2& rhs) { return Int2(lhs.X * rhs.X, lhs.Y * rhs.Y); }
        inline friend Int2 operator*(const Int2& lhs, std::int32_t rhs) { return Int2(lhs.X * rhs, lhs.Y * rhs); }
        inline friend Int2 operator*(std::int32_t lhs, const Int2& rhs) { return Int2(lhs * rhs.X, lhs * rhs.Y); }

        inline friend Int2 operator/(const Int2& lhs, const Int2& rhs) { return Int2(lhs.X / rhs.X, lhs.Y / rhs.Y); }
        inline friend Int2 operator/(const Int2& lhs, std::int32_t rhs) { return Int2(lhs.X / rhs, lhs.Y / rhs); }
        inline friend Int2 operator/(std::int32_t lhs, const Int2& rhs) { return Int2(lhs / rhs.X, lhs / rhs.Y); }

        inline friend bool operator<(const Int2& x0, const Int2& x1) { return x0.compare(x1) < 0; }
        inline friend bool operator<=(const Int2& x0, const Int2& x1) { return x0.compare(x1) <= 0; }
        inline friend bool operator>(const Int2& x0, const Int2& x1) { return x0.compare(x1) > 0; }
        inline friend bool operator>=(const Int2& x0, const Int2& x1) { return x0.compare(x1) >= 0; }
    };

    struct Int3 final {
        std::int32_t X;
        std::int32_t Y;
        std::int32_t Z;

        const Int3(std::int32_t x, std::int32_t y, std::int32_t z) : X(x), Y(y), Z(z) {}
        const Int3(std::int32_t value) : X(value), Y(value), Z(value) {}

        inline static const Int3 zero() { return Int3(0); }
        inline static const Int3 minValue() { return Int3(std::numeric_limits<std::int32_t>::min()); }
        inline static const Int3 maxValue() { return Int3(std::numeric_limits<std::int32_t>::max()); }
        inline static const Int3 one() { return Int3(1); }
        inline static const Int3 unitX() { return Int3(1, 0, 0); }
        inline static const Int3 unitY() { return Int3(0, 1, 0); }
        inline static const Int3 unitZ() { return Int3(0, 0, 1); }

        inline Int3 setX(std::int32_t x) const { return Int3(x, Y, Z); }
        inline Int3 setY(std::int32_t y) const { return Int3(X, y, Z); }
        inline Int3 setZ(std::int32_t z) const { return Int3(X, Y, z); }

        inline std::size_t hash() const { return hash::combine(X, Y, Z); }
        inline bool almostEquals(const Int3& x, float tolerance = constants::tolerance) const {
            return std::fabs(X - x.X) <= tolerance
                && std::fabs(Y - x.Y) <= tolerance
                && std::fabs(Z - x.Z) <= tolerance;
        }
        inline static std::int32_t dot(const Int3& value1, const Int3& value2) {
            return value1.X * value2.X + value1.Y * value2.Y + value1.Z * value2.Z;
        }
        inline std::int32_t dot(const Int3& value) const { return dot(*this, value); }
        inline bool almostZero(float tolerance = constants::tolerance) const {
            return std::fabs(X) < tolerance
                && std::fabs(Y) < tolerance
                && std::fabs(Z) < tolerance;
        }
        inline std::int32_t minComponent() const { return std::min(std::min(X, Y), Z); }
        inline std::int32_t maxComponent() const { return std::max(std::max(X, Y), Z); }
        inline std::int32_t sumComponents() const { return X + Y + Z; }
        inline std::int32_t sumSqrComponents() const { return X * X + Y * Y + Z * Z; }
        inline std::int32_t productComponents() const { return X * Y * Z; }
        inline std::int32_t getComponent(int n) const { return n == 0 ? X : n == 1 ? Y : Z; }
        inline bool anyComponentNegative() const { return minComponent() < 0.0; }
        inline double magnitudeSquared() const { return sumSqrComponents(); }
        inline double magnitude() const { return std::sqrt(magnitudeSquared()); }
        /* inline bool isnan() const { return std::isnan(X) || std::isnan(Y) || std::isnan(Z); }
         inline bool isinf() const { return std::isinf(X) || std::isinf(Y) || std::isinf(Z); }*/
        inline int compare(const Int3& x) const { return std::signbit(magnitudeSquared() - x.magnitudeSquared()); }

        inline friend Int3 operator-(const Int3& l) { return { -l.X, -l.Y, -l.Z }; }
        inline friend bool operator==(const Int3& l, const Int3& other) { return l.X == other.X && l.Y == other.Y && l.Z == other.Z; }
        inline friend bool operator!=(const Int3& l, const Int3& other) { return !(l == other); }

        inline friend std::ostream& operator<<(std::ostream& out, const Int3& v) { return (out << "Int3(X = " << v.X << ", Y = " << v.Y << ", Z = " << v.Z << ")"); }
        inline friend Int3 operator+(const Int3& lhs, const Int3& rhs) { return Int3(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z); }
        inline friend Int3 operator+(const Int3& lhs, std::int32_t rhs) { return Int3(lhs.X + rhs, lhs.Y + rhs, lhs.Z + rhs); }
        inline friend Int3 operator+(std::int32_t lhs, const Int3& rhs) { return Int3(lhs + rhs.X, lhs + rhs.Y, lhs + rhs.Z); }

        inline friend Int3 operator-(const Int3& lhs, const Int3& rhs) { return Int3(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z); }
        inline friend Int3 operator-(const Int3& lhs, std::int32_t rhs) { return Int3(lhs.X - rhs, lhs.Y - rhs, lhs.Z - rhs); }
        inline friend Int3 operator-(std::int32_t lhs, const Int3& rhs) { return Int3(lhs - rhs.X, lhs - rhs.Y, lhs - rhs.Z); }

        inline friend Int3 operator*(const Int3& lhs, const Int3& rhs) { return Int3(lhs.X * rhs.X, lhs.Y * rhs.Y, lhs.Z * rhs.Z); }
        inline friend Int3 operator*(const Int3& lhs, std::int32_t rhs) { return Int3(lhs.X * rhs, lhs.Y * rhs, lhs.Z * rhs); }
        inline friend Int3 operator*(std::int32_t lhs, const Int3& rhs) { return Int3(lhs * rhs.X, lhs * rhs.Y, lhs * rhs.Z); }

        inline friend Int3 operator/(const Int3& lhs, const Int3& rhs) { return Int3(lhs.X / rhs.X, lhs.Y / rhs.Y, lhs.Z / rhs.Z); }
        inline friend Int3 operator/(const Int3& lhs, std::int32_t rhs) { return Int3(lhs.X / rhs, lhs.Y / rhs, lhs.Z / rhs); }
        inline friend Int3 operator/(std::int32_t lhs, const Int3& rhs) { return Int3(lhs / rhs.X, lhs / rhs.Y, lhs / rhs.Z); }

        inline friend bool operator<(const Int3& x0, const Int3& x1) { return x0.compare(x1) < 0; }
        inline friend bool operator<=(const Int3& x0, const Int3& x1) { return x0.compare(x1) <= 0; }
        inline friend bool operator>(const Int3& x0, const Int3& x1) { return x0.compare(x1) > 0; }
        inline friend bool operator>=(const Int3& x0, const Int3& x1) { return x0.compare(x1) >= 0; }
    };

    struct Int4 final {
        std::int32_t X;
        std::int32_t Y;
        std::int32_t Z;
        std::int32_t W;

        const Int4(std::int32_t x, std::int32_t y, std::int32_t z, std::int32_t w) : X(x), Y(y), Z(z), W(w) {}
        const Int4(std::int32_t value) : X(value), Y(value), Z(value), W(value) {}

        inline static const Int4 zero() { return Int4(0); }
        inline static const Int4 minValue() { return Int4(std::numeric_limits<std::int32_t>::min()); }
        inline static const Int4 maxValue() { return Int4(std::numeric_limits<std::int32_t>::max()); }
        inline static const Int4 one() { return Int4(1); }
        inline static const Int4 unitX() { return Int4(1, 0, 0, 0); }
        inline static const Int4 unitY() { return Int4(0, 1, 0, 0); }
        inline static const Int4 unitZ() { return Int4(0, 0, 1, 0); }
        inline static const Int4 unitW() { return Int4(0, 0, 0, 1); }

        inline Int4 setX(std::int32_t x) const { return Int4(x, Y, Z, W); }
        inline Int4 setY(std::int32_t y) const { return Int4(X, y, Z, W); }
        inline Int4 setZ(std::int32_t z) const { return Int4(X, Y, z, W); }
        inline Int4 setW(std::int32_t w) const { return Int4(X, Y, Z, w); }

        inline std::size_t hash() const { return hash::combine(X, Y, Z, W); }
        inline bool almostEquals(const Int4& x, float tolerance = constants::tolerance) const {
            return std::fabs(X - x.X) <= tolerance
                && std::fabs(Y - x.Y) <= tolerance
                && std::fabs(Z - x.Z) <= tolerance
                && std::fabs(W - x.W) <= tolerance;
        }
        inline static std::int32_t dot(const Int4& value1, const Int4& value2) {
            return value1.X * value2.X + value1.Y * value2.Y + value1.Z * value2.Z + value1.W * value2.W;
        }
        inline std::int32_t dot(const Int4& value) const { return dot(*this, value); }
        inline bool almostZero(float tolerance = constants::tolerance) const {
            return std::fabs(X) < tolerance
                && std::fabs(Y) < tolerance
                && std::fabs(Z) < tolerance
                && std::fabs(W) < tolerance;
        }
        inline std::int32_t minComponent() const { return std::min(std::min(std::min(X, Y), Z), W); }
        inline std::int32_t maxComponent() const { return  std::max(std::max(std::max(X, Y), Z), W); }
        inline std::int32_t sumComponents() const { return X + Y + Z + W; }
        inline std::int32_t sumSqrComponents() const { return X * X + Y * Y + Z * Z + W * W; }
        inline std::int32_t productComponents() const { return X * Y * Z * W; }
        inline std::int32_t getComponent(int n) const { return n == 0 ? X : n == 1 ? Y : n == 2 ? Z : W; }
        inline bool anyComponentNegative() const { return minComponent() < 0.0; }
        inline double magnitudeSquared() const { return sumSqrComponents(); }
        inline double magnitude() const { return std::sqrt(magnitudeSquared()); }
        inline int compare(const Int4& x) const { return std::signbit(magnitudeSquared() - x.magnitudeSquared()); }

        inline friend Int4 operator-(const Int4& l) { return { -l.X, -l.Y, -l.Z, -l.W }; }

        inline friend bool operator==(const Int4& l, const Int4& other) { return l.X == other.X && l.Y == other.Y && l.Z == other.Z && l.W == other.W; }
        inline friend bool operator!=(const Int4& l, const Int4& other) { return !(l == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const Int4& v) { return (out << "Int4(X = " << v.X << ", Y = " << v.Y << ", Z = " << v.Z << ", W = " << v.W << ")"); }

        inline friend Int4 operator+(const Int4& lhs, const Int4& rhs) { return Int4(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z, lhs.W + rhs.W); }
        inline friend Int4 operator+(const Int4& lhs, std::int32_t rhs) { return Int4(lhs.X + rhs, lhs.Y + rhs, lhs.Z + rhs, lhs.W + rhs); }
        inline friend Int4 operator+(std::int32_t lhs, const Int4& rhs) { return Int4(lhs + rhs.X, lhs + rhs.Y, lhs + rhs.Z, lhs + rhs.W); }

        inline friend Int4 operator-(const Int4& lhs, const Int4& rhs) { return Int4(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z, lhs.W - rhs.W); }
        inline friend Int4 operator-(const Int4& lhs, std::int32_t rhs) { return Int4(lhs.X - rhs, lhs.Y - rhs, lhs.Z - rhs, lhs.W - rhs); }
        inline friend Int4 operator-(std::int32_t lhs, const Int4& rhs) { return Int4(lhs - rhs.X, lhs - rhs.Y, lhs - rhs.Z, lhs - rhs.W); }

        inline friend Int4 operator*(const Int4& lhs, const Int4& rhs) { return Int4(lhs.X * rhs.X, lhs.Y * rhs.Y, lhs.Z * rhs.Z, lhs.W * rhs.W); }
        inline friend Int4 operator*(const Int4& lhs, std::int32_t rhs) { return Int4(lhs.X * rhs, lhs.Y * rhs, lhs.Z * rhs, lhs.W * rhs); }
        inline friend Int4 operator*(std::int32_t lhs, const Int4& rhs) { return Int4(lhs * rhs.X, lhs * rhs.Y, lhs * rhs.Z, lhs * rhs.W); }

        inline friend Int4 operator/(const Int4& lhs, const Int4& rhs) { return Int4(lhs.X / rhs.X, lhs.Y / rhs.Y, lhs.Z / rhs.Z, lhs.W / rhs.W); }
        inline friend Int4 operator/(const Int4& lhs, std::int32_t rhs) { return Int4(lhs.X / rhs, lhs.Y / rhs, lhs.Z / rhs, lhs.W / rhs); }
        inline friend Int4 operator/(std::int32_t lhs, const Int4& rhs) { return Int4(lhs / rhs.X, lhs / rhs.Y, lhs / rhs.Z, lhs / rhs.W); }

        inline friend bool operator<(const Int4& x0, const Int4& x1) { return x0.compare(x1) < 0; }
        inline friend bool operator<=(const Int4& x0, const Int4& x1) { return x0.compare(x1) <= 0; }
        inline friend bool operator>(const Int4& x0, const Int4& x1) { return x0.compare(x1) > 0; }
        inline friend bool operator>=(const Int4& x0, const Int4& x1) { return x0.compare(x1) >= 0; }
    };

    template <typename T = float>
    struct Interval final {
        T Min;
        T Max;

        const Interval(T min, T max) : Min(min), Max(max) {}
        const Interval(T value) : Min(value), Max(value) {}

        inline static const Interval zero() { return Interval(0); }
        inline static const Interval minValue() { return Interval(std::numeric_limits<T>::min()); }
        inline static const Interval maxValue() { return Interval(std::numeric_limits<T>::max()); }
        inline static const Interval empty() { return Interval(std::numeric_limits<T>::max(), std::numeric_limits<T>::min()); }

        inline Interval setMin(T x) const { return Interval(x, Max); }
        inline Interval setMax(T x) const { return Interval(Min, x); }

        inline std::size_t hash() const { return hash::combine((Min), (Max)); }
        inline bool almostEquals(const Interval& x, float tolerance = constants::tolerance) const {
            return std::fabs(Min - x.Min) <= tolerance
                && std::fabs(Max - x.Max) <= tolerance;
        }
        inline T extent() const { return Max - Min; };
        inline T center() const { return (Max + Min) * 0.5; };
        inline T magnitudeSquared() const { T ext = extent(); return ext * ext; }
        inline T magnitude() const { return std::sqrt(magnitudeSquared()); }
        inline bool isnan() const { return std::isnan(Min) || std::isnan(Max); }
        inline bool isinf() const { return std::isinf(Min) || std::isinf(Max); }
        inline int compare(const Interval& x) const { return std::signbit(magnitudeSquared() - x.magnitudeSquared()); }
        inline Interval merge(const Interval& other) const { return { std::min(Min, other.Min), std::max(Max, other.Max) }; }
        inline Interval merge(T other) const { return { std::min(Min, other), std::max(Max, other) }; }
        inline Interval intersection(const Interval& other) const { return { std::max(Min, other.Min), std::min(Max, other.Max) }; }

        inline friend bool operator==(const Interval& o, const Interval& other) { return o.Min == other.Min && o.Max == other.Max; }
        inline friend bool operator!=(const Interval& o, const Interval& other) { return !(o == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const Interval& v) { return (out << "Interval(Min = " << v.Min << ", Max = " << v.Max << ")"); }

        inline friend Interval operator+(const Interval& value1, const Interval& value2) { return value1.merge(value2); }
        inline friend Interval operator+(const Interval& value1, const T value2) { return value1.merge(value2); }
        inline friend Interval operator-(const Interval& value1, const T value2) { return value1.intersection(value2); }

        inline friend bool operator<(const Interval& x0, const Interval& x1) { return x0.compare(x1) < 0; }
        inline friend bool operator<=(const Interval& x0, const Interval& x1) { return x0.compare(x1) <= 0; }
        inline friend bool operator>(const Interval& x0, const Interval& x1) { return x0.compare(x1) > 0; }
        inline friend bool operator>=(const Interval& x0, const Interval& x1) { return x0.compare(x1) >= 0; }
    };

#define FInterval Interval<float>
#define DInterval Interval<double>

    template <typename T = float>
    struct Vector2 final {
        T X;
        T Y;

        const Vector2(T x, T y) : X(x), Y(y) {}
        const Vector2(T value = 0) : X(value), Y(value) {}

        inline static const Vector2<T> minValue() { return Vector2<T>(std::numeric_limits<T>::min()); }
        inline static const Vector2<T> maxValue() { return Vector2<T>(std::numeric_limits<T>::max()); }
        inline static const Vector2<T> zero() { return Vector2<T>(0); }
        inline static const Vector2<T> one() { return Vector2<T>(1); }
        inline static const Vector2<T> unitX() { return Vector2<T>(1, 0); }
        inline static const Vector2<T> unitY() { return Vector2<T>(0, 1); }

        inline T minComponent() const { return std::min(X, Y); }
        inline T maxComponent() const { return std::max(X, Y); }
        inline T sumComponents() const { return X + Y; }
        inline T sumSqrComponents() const { return X * X + Y * Y; }
        inline T productComponents() const { return X * Y; }
        inline T getComponent(int n) const { return n == 0 ? X : Y; }
        inline T magnitudeSquared() const { return sumSqrComponents(); }
        inline T magnitude() const { return std::sqrt(magnitudeSquared()); }
        inline bool anyComponentNegative() const { return minComponent() < 0.0; }
        inline bool isnan() const { return std::isnan(X) || std::isnan(Y); }
        inline bool isinf() const { return std::isinf(X) || std::isinf(Y); }

        inline Vector2<T> abs() const { return Vector2<T>(std::abs(X), std::abs(Y)); }
        inline Vector2<T> acos() const { return Vector2<T>(std::acos(X), std::acos(Y)); }
        inline Vector2<T> asin() const { return Vector2<T>(std::asin(X), std::asin(Y)); }
        inline Vector2<T> atan() const { return Vector2<T>(std::atan(X), std::atan(Y)); }
        inline Vector2<T> cos() const { return Vector2<T>(std::cos(X), std::cos(Y)); }
        inline Vector2<T> cosh() const { return Vector2<T>(std::cosh(X), std::cosh(Y)); }
        inline Vector2<T> exp() const { return Vector2<T>(std::exp(X), std::exp(Y)); }
        inline Vector2<T> log() const { return Vector2<T>(std::log(X), std::log(Y)); }
        inline Vector2<T> log10() const { return Vector2<T>(std::log10(X), std::log10(Y)); }
        inline Vector2<T> sin() const { return Vector2<T>(std::sin(X), std::sin(Y)); }
        inline Vector2<T> sinh() const { return Vector2<T>(std::sinh(X), std::sinh(Y)); }
        inline Vector2<T> sqrt() const { return Vector2<T>(std::sqrt(X), std::sqrt(Y)); }
        inline Vector2<T> tan() const { return Vector2<T>(std::tan(X), std::tan(Y)); }
        inline Vector2<T> tanh() const { return Vector2<T>(std::tanh(X), std::tanh(Y)); }
        inline Vector2<T> inverse() const { return Vector2<T>((1 / X), (1 / Y)); }
        inline Vector2<T> ceiling() const { return Vector2<T>(std::ceil(X), std::ceil(Y)); }
        inline Vector2<T> floor() const { return Vector2<T>(std::floor(X), std::floor(Y)); }
        inline Vector2<T> round() const { return Vector2<T>(std::round(X), std::round(Y)); }
        inline Vector2<T> truncate() const { return Vector2<T>(std::trunc(X), std::trunc(Y)); }
        inline Vector2<T> sqr() const { return Vector2<T>(X * X, Y * Y); }
        inline Vector2<T> cube() const { return Vector2<T>(X * X * X, Y * Y * Y); }
        inline Vector2<T> toRadians() const { return Vector2<T>(X * constants::degreesToRadians, Y * constants::degreesToRadians); }
        inline Vector2<T> toDegrees() const { return Vector2<T>(X * constants::radiansToDegrees, Y * constants::radiansToDegrees); }

        inline T lengthSquared() const { return sumSqrComponents(); }
        inline T length() const { return std::sqrt(lengthSquared()); }
        inline T distance(const Vector2<T>& v2) const { return (*this - v2).length(); }
        inline static T distance(const Vector2<T>& v1, const Vector2<T>& v2) { return v1.distance(v2); }
        inline T distanceSquared(const Vector2<T>& v2) const { return (*this - v2).lengthSquared(); }
        inline static T distanceSquared(const Vector2<T>& v1, const Vector2<T>& v2) { return v1.distanceSquared(v2); }
        inline Vector2<T> normalize() const { return *this / length(); }
        inline Vector2<T> safeNormalize() const { auto l = length(); return (l != 0) ? (*this / l) : (*this); }
        inline T dot(const Vector2<T>& v2) const { return X * v2.X + Y * v2.Y; }
        inline static T dot(const Vector2<T>& v1, const Vector2<T>& v2) { return v1.dot(v2); }
        inline Vector2<T> min(const Vector2<T>& v2) const { return Vector2<T>(std::min(X, v2.X), std::min(Y, v2.Y)); }
        inline Vector2<T> max(const Vector2<T>& v2) const { return Vector2<T>(std::max(X, v2.X), std::max(Y, v2.Y)); }
        inline Vector2<T> squareRoot() const { return sqrt(); }

        inline Vector2<T> lerp(const Vector2<T>& v2, T t) const { return (*this) + (v2 - (*this)) * t; }
        inline Vector2<T> inverseLerp(const Vector2<T>& a, const Vector2<T>& b) const { return ((*this) - a) / (b - a); }
        inline Vector2<T> lerpPrecise(const Vector2<T>& v2, T t) const { return ((1 - t) * (*this)) + (v2 * t); }
        inline Vector2<T> clampLower(const Vector2<T>& m) const { return max(m); }
        inline Vector2<T> clampUpper(const Vector2<T>& m) const { return min(m); }
        inline Vector2<T> clamp(const Vector2<T>& mn, const Vector2<T>& mx) const { return min(mx).max(mn); }
        inline Vector2<T> average(const Vector2<T>& v2) const { return lerp(v2, (T)0.5); }
        inline Vector2<T> barycentric(const Vector2<T>& v2, const Vector2<T>& v3, T u, T v) const { return (*this) + (v2 - (*this)) * u + (v3 - (*this)) * v; }

        inline Vector2<T> add(const Vector2<T>& v2) { return (*this) + v2; }
        inline Vector2<T> subtract(const Vector2<T>& v2) { return (*this) - v2; }
        inline Vector2<T> multiply(const Vector2<T>& v2) { return (*this) * v2; }
        inline Vector2<T> divide(const Vector2<T>& v2) { return (*this) / v2; }
        inline Vector2<T> negate() { return -(*this); }

        inline T pointCrossProduct(const Vector2<T>& other) { return X * other.Y - other.X * Y; }
        inline T cross(const Vector2<T>& v2) { return X * v2.Y - Y * v2.X; }
        inline Vector2<T> reflect(const Vector2<T>& normal) { return (*this) - (2 * (dot(normal) * normal)); }

        inline Vector2<T> setX(T x) const { return Vector2<T>(x, Y); }
        inline Vector2<T> setY(T y) const { return Vector2<T>(X, y); }

        inline std::size_t hash() const { return hash::combine(X, Y); }
        inline bool almostEquals(const Vector2<T>& x, float tolerance = constants::tolerance) const {
            return std::fabs(X - x.X) <= tolerance && std::fabs(Y - x.Y) <= tolerance;
        }
        inline bool almostZero(float tolerance = constants::tolerance) const {
            return std::fabs(X) < tolerance && std::fabs(Y) < tolerance;
        }
        inline int compare(const Vector2<T>& x) const { return std::signbit(magnitudeSquared() - x.magnitudeSquared()); }

        inline friend Vector2<T> operator-(const Vector2<T>& l) { return Vector2<T>(-l.X, -l.Y); }
        inline friend bool operator==(const Vector2<T>& l, const Vector2<T>& other) { return l.X == other.X && l.Y == other.Y; }
        inline friend bool operator!=(const Vector2<T>& l, const Vector2<T>& other) { return !(l == other); }

        inline friend std::ostream& operator<<(std::ostream& out, const Vector2<T>& v) { return (out << "Vector2<T>(X = " << v.X << ", Y = " << v.Y << ")"); }
        inline friend Vector2<T> operator+(const Vector2<T>& lhs, const Vector2<T>& rhs) { return Vector2<T>(lhs.X + rhs.X, lhs.Y + rhs.Y); }
        inline friend Vector2<T> operator+(const Vector2<T>& lhs, T rhs) { return Vector2<T>(lhs.X + rhs, lhs.Y + rhs); }
        inline friend Vector2<T> operator+(T lhs, const Vector2<T>& rhs) { return Vector2<T>(lhs + rhs.X, lhs + rhs.Y); }

        inline friend Vector2<T> operator-(const Vector2<T>& lhs, const Vector2<T>& rhs) { return Vector2<T>(lhs.X - rhs.X, lhs.Y - rhs.Y); }
        inline friend Vector2<T> operator-(const Vector2<T>& lhs, T rhs) { return Vector2<T>(lhs.X - rhs, lhs.Y - rhs); }
        inline friend Vector2<T> operator-(T lhs, const Vector2<T>& rhs) { return Vector2<T>(lhs - rhs.X, lhs - rhs.Y); }

        inline friend Vector2<T> operator*(const Vector2<T>& lhs, const Vector2<T>& rhs) { return Vector2<T>(lhs.X * rhs.X, lhs.Y * rhs.Y); }
        inline friend Vector2<T> operator*(const Vector2<T>& lhs, T rhs) { return Vector2<T>(lhs.X * rhs, lhs.Y * rhs); }
        inline friend Vector2<T> operator*(T lhs, const Vector2<T>& rhs) { return Vector2<T>(lhs * rhs.X, lhs * rhs.Y); }

        inline friend Vector2<T> operator/(const Vector2<T>& lhs, const Vector2<T>& rhs) { return Vector2<T>(lhs.X / rhs.X, lhs.Y / rhs.Y); }
        inline friend Vector2<T> operator/(const Vector2<T>& lhs, T rhs) { return Vector2<T>(lhs.X / rhs, lhs.Y / rhs); }
        inline friend Vector2<T> operator/(T lhs, const Vector2<T>& rhs) { return Vector2<T>(lhs / rhs.X, lhs / rhs.Y); }

        inline friend bool operator<(const Vector2<T>& x0, const Vector2<T>& x1) { return x0.compare(x1) < 0; }
        inline friend bool operator<=(const Vector2<T>& x0, const Vector2<T>& x1) { return x0.compare(x1) <= 0; }
        inline friend bool operator>(const Vector2<T>& x0, const Vector2<T>& x1) { return x0.compare(x1) > 0; }
        inline friend bool operator>=(const Vector2<T>& x0, const Vector2<T>& x1) { return x0.compare(x1) >= 0; }
    };

#define FVector2 Vector2<float>
#define DVector2 Vector2<double>

    template <typename T = float>
    struct Vector3 final {
        T X;
        T Y;
        T Z;

        const Vector3(T x, T y, T z = 0) : X(x), Y(y), Z(z) {}
        const Vector3(T value = 0) : X(value), Y(value), Z(value) {}
        const Vector3(const Vector2<T>& v, T z = 0) : X(v.X), Y(v.Y), Z(z) {}

        inline static const Vector3<T> minValue() { return Vector3<T>(std::numeric_limits<T>::min()); }
        inline static const Vector3<T> maxValue() { return Vector3<T>(std::numeric_limits<T>::max()); }
        inline static const Vector3<T> zero() { return Vector3<T>(0); }
        inline static const Vector3<T> one() { return Vector3<T>(1); }
        inline static const Vector3<T> unitX() { return Vector3<T>(1, 0, 0); }
        inline static const Vector3<T> unitY() { return Vector3<T>(0, 1, 0); }
        inline static const Vector3<T> unitZ() { return Vector3<T>(0, 0, 1); }

        inline static Vector3<T> alongX(T self) { return unitX() * self; }
        inline static Vector3<T> alongY(T self) { return unitY() * self; }
        inline static Vector3<T> alongZ(T self) { return unitX() * self; }

        inline T minComponent() const { return std::min(std::min(X, Y), Z); }
        inline T maxComponent() const { return std::max(std::max(X, Y), Z); }
        inline T sumComponents() const { return X + Y + Z; }
        inline T sumSqrComponents() const { return X * X + Y * Y + Z * Z; }
        inline T productComponents() const { return X * Y * Z; }
        inline T getComponent(int n) const { return n == 0 ? X : n == 1 ? Y : Z; }
        inline bool anyComponentNegative() const { return minComponent() < 0.0; }
        inline T magnitudeSquared() const { return sumSqrComponents(); }
        inline T magnitude() const { return std::sqrt(magnitudeSquared()); }
        inline bool isnan() const { return std::isnan(X) || std::isnan(Y) || std::isnan(Z); }
        inline bool isinf() const { return std::isinf(X) || std::isinf(Y) || std::isinf(Z); }

        inline Vector3<T> abs() const { return Vector3<T>(std::abs(X), std::abs(Y), std::abs(Z)); }
        inline Vector3<T> acos() const { return Vector3<T>(std::acos(X), std::acos(Y), std::acos(Z)); }
        inline Vector3<T> asin() const { return Vector3<T>(std::asin(X), std::asin(Y), std::asin(Z)); }
        inline Vector3<T> atan() const { return Vector3<T>(std::atan(X), std::atan(Y), std::atan(Z)); }
        inline Vector3<T> cos() const { return Vector3<T>(std::cos(X), std::cos(Y), std::cos(Z)); }
        inline Vector3<T> cosh() const { return Vector3<T>(std::cosh(X), std::cosh(Y), std::cosh(Z)); }
        inline Vector3<T> exp() const { return Vector3<T>(std::exp(X), std::exp(Y), std::exp(Z)); }
        inline Vector3<T> log() const { return Vector3<T>(std::log(X), std::log(Y), std::log(Z)); }
        inline Vector3<T> log10() const { return Vector3<T>(std::log10(X), std::log10(Y), std::log10(Z)); }
        inline Vector3<T> sin() const { return Vector3<T>(std::sin(X), std::sin(Y), std::sin(Z)); }
        inline Vector3<T> sinh() const { return Vector3<T>(std::sinh(X), std::sinh(Y), std::sinh(Z)); }
        inline Vector3<T> sqrt() const { return Vector3<T>(std::sqrt(X), std::sqrt(Y), std::sqrt(Z)); }
        inline Vector3<T> tan() const { return Vector3<T>(std::tan(X), std::tan(Y), std::tan(Z)); }
        inline Vector3<T> tanh() const { return Vector3<T>(std::tanh(X), std::tanh(Y), std::tanh(Z)); }
        inline Vector3<T> inverse() const { return Vector3<T>((1 / X), (1 / Y), (1 / Z)); }
        inline Vector3<T> ceiling() const { return Vector3<T>(std::ceil(X), std::ceil(Y), std::ceil(Z)); }
        inline Vector3<T> floor() const { return Vector3<T>(std::floor(X), std::floor(Y), std::floor(Z)); }
        inline Vector3<T> round() const { return Vector3<T>(std::round(X), std::round(Y), std::round(Z)); }
        inline Vector3<T> truncate() const { return Vector3<T>(std::trunc(X), std::trunc(Y), std::trunc(Z)); }
        inline Vector3<T> sqr() const { return Vector3<T>(X * X, Y * Y, Z * Z); }
        inline Vector3<T> cube() const { return Vector3<T>(X * X * X, Y * Y * Y, Z * Z * Z); }
        inline Vector3<T> toRadians() const { return Vector3<T>(X * constants::degreesToRadians, Y * constants::degreesToRadians, Z * constants::degreesToRadians); }
        inline Vector3<T> toDegrees() const { return Vector3<T>(X * constants::radiansToDegrees, Y * constants::radiansToDegrees, Z * constants::radiansToDegrees); }

        inline T lengthSquared() const { return sumSqrComponents(); }
        inline T length() const { return std::sqrt(lengthSquared()); }
        inline T distance(const Vector3<T>& v2) const { return (*this - v2).length(); }
        inline T distanceSquared(const Vector3<T>& v2) const { return (*this - v2).lengthSquared(); }
        inline Vector3<T> normalize() const { return *this / length(); }
        inline Vector3<T> safeNormalize() const { auto l = length(); return (l != 0) ? (*this / l) : (*this); }
        inline T dot(const Vector3<T>& v2) const { return X * v2.X + Y * v2.Y + Z * v2.Z; }
        inline static T dot(const Vector3<T>& v1, const Vector3<T>& v2) { return v1.dot(v2); }
        inline Vector3<T> min(const Vector3<T>& v2) const { return Vector3<T>(std::min(X, v2.X), std::min(Y, v2.Y), std::min(Z, v2.Z)); }
        inline Vector3<T> max(const Vector3<T>& v2) const { return Vector3<T>(std::max(X, v2.X), std::max(Y, v2.Y), std::max(Z, v2.Z)); }
        inline Vector3<T> squareRoot() const { return sqrt(); }

        inline Vector3<T> lerp(const Vector3<T>& v2, T t) const { return (*this) + (v2 - (*this)) * t; }
        inline Vector3<T> inverseLerp(const Vector3<T>& a, const Vector3<T>& b) const { return ((*this) - a) / (b - a); }
        inline Vector3<T> lerpPrecise(const Vector3<T>& v2, T t) const { return ((1 - t) * (*this)) + (v2 * t); }
        inline Vector3<T> clampLower(const Vector3<T>& m) const { return max(m); }
        inline Vector3<T> clampUpper(const Vector3<T>& m) const { return min(m); }
        inline Vector3<T> clamp(const Vector3<T>& mn, const Vector3<T>& mx) const { return min(mx).max(mn); }
        inline Vector3<T> average(const Vector3<T>& v2) const { return lerp(v2, (T)0.5); }
        inline Vector3<T> barycentric(const Vector3<T>& v2, const Vector3<T>& v3, T u, T v) const { return (*this) + (v2 - (*this)) * u + (v3 - (*this)) * v; }

        inline Vector3<T> add(const Vector3<T>& v2) { return (*this) + v2; }
        inline Vector3<T> subtract(const Vector3<T>& v2) { return (*this) - v2; }
        inline Vector3<T> multiply(const Vector3<T>& v2) { return (*this) * v2; }
        inline Vector3<T> divide(const Vector3<T>& v2) { return (*this) / v2; }
        inline Vector3<T> negate() { return -(*this); }

        inline Vector3<T> cross(const Vector3<T>& x) const { return Vector3<T>(Y * x.Z - Z * x.Y, Z * x.X - X * x.Z, X * x.Y - Y * x.X); }
        inline T mixedProduct(const Vector3<T>& v1, const Vector3<T>& v2) { return cross(v1).dot(v2); }
        inline Vector3<T> reflect(const Vector3<T>& normal) { return (*this) - (2 * (dot(normal) * normal)); }
        inline bool isNonZeroAndValid(float tolerance = constants::tolerance) const { T self = lengthSquared(); return !std::isinf(self) && !std::isnan(self) && std::fabs(self) > tolerance; }
        inline bool isZeroOrInvalid(float tolerance = constants::tolerance) const { return !isNonZeroAndValid(tolerance); }
        inline bool isPerpendicular(const Vector3<T>& v1, const Vector3<T>& v2, float tolerance = constants::tolerance) const { auto z = zero(); return v1 != z && v2 != z && (std::fabs(v1.dot(v2)) < tolerance); }
        inline Vector3<T> projection(const Vector3<T>& v2) const { return v2 * (dot(v2) / v2.lengthSquared()); }
        inline Vector3<T> rejection(const Vector3<T>& v2) const { return *this - projection(v2); }
        inline T angle(const Vector3<T>& v2, float tolerance = constants::tolerance) const
        {
            auto d = lengthSquared() * std::sqrt(v2.lengthSquared());
            //std::clamp(dot(v2) / d, (T)-1, (T)1);
            return d < tolerance ? 0 : std::acos(std::max(std::min(dot(v2) / d, (T)1), (T)-1));
        }
        inline T signedAngle(const Vector3<T>& to, const Vector3<T>& axis) const { return angle(to) * (std::int8_t)std::signbit(axis.dot(cross(to))); }
        inline T signedAngle(const Vector3<T>& to) const { return signedAngle(to, unitZ()); }
        inline bool colinear(const Vector3<T>& v2, float tolerance = constants::tolerance) const { return !isnan() && !v2.isnan() && signedAngle(v2) <= tolerance; }
        inline bool isBackFace(const Vector3<T>& lineOfSight) const { return dot(lineOfSight) < 0; }
        inline Vector3<T> along(T d) const { return normalize() * d; }

        inline Vector3<T> catmullRom(const Vector3<T>& value2, const Vector3<T>& value3, const Vector3<T>& value4, T amount) const {
            return Vector3<T>(
                mathOps::catmullRom(X, value2.X, value3.X, value4.X, amount),
                mathOps::catmullRom(Y, value2.Y, value3.Y, value4.Y, amount),
                mathOps::catmullRom(Z, value2.Z, value3.Z, value4.Z, amount));
        }
        inline Vector3<T> hermite(const Vector3<T>& tangent1, const Vector3<T>& value2, const Vector3<T>& tangent2, T amount) const {
            return Vector3<T>(
                mathOps::hermite(X, tangent1.X, value2.X, tangent2.X, amount),
                mathOps::hermite(Y, tangent1.Y, value2.Y, tangent2.Y, amount),
                mathOps::hermite(Z, tangent1.Z, value2.Z, tangent2.Z, amount));
        }
        inline Vector3<T> smoothStep(const Vector3<T>& value2, T amount) const {
            return Vector3<T>(
                mathOps::smoothStep(X, value2.X, amount),
                mathOps::smoothStep(Y, value2.Y, amount),
                mathOps::smoothStep(Z, value2.Z, amount));
        }
        inline bool coplanar(const Vector3<T>& v2, const Vector3<T>& v3, const Vector3<T>& v4, float epsilon = constants::tolerance) const { return std::fabs((v3 - (*this)).dot((v2 - (*this)).cross(v4 - (*this)))) < epsilon; }

        inline Vector3<T> xzy() const { return Vector3<T>(X, Z, Y); }
        inline Vector3<T> zxy() const { return Vector3<T>(Z, X, Y); }
        inline Vector3<T> zyx() const { return Vector3<T>(Z, Y, Z); }
        inline Vector3<T> yxz() const { return Vector3<T>(Y, X, Z); }
        inline Vector3<T> yzx() const { return Vector3<T>(Y, Z, X); }
        inline Vector2<T> xy() const { return Vector2<T>(X, Y); }
        inline Vector2<T> xz() const { return Vector2<T>(X, Z); }
        inline Vector2<T> yz() const { return Vector2<T>(Y, Z); }

        inline Vector3<T> setX(T x) const { return Vector3<T>(x, Y, Z); }
        inline Vector3<T> setY(T y) const { return Vector3<T>(X, y, Z); }
        inline Vector3<T> setZ(T z) const { return Vector3<T>(X, Y, z); }

        inline std::size_t hash() const { return hash::combine(X, Y, Z); }
        inline bool almostEquals(const Vector3<T>& x, float tolerance = constants::tolerance) const { return std::fabs(X - x.X) <= tolerance && std::fabs(Y - x.Y) <= tolerance && std::fabs(Z - x.Z) <= tolerance; }
        inline bool almostZero(float tolerance = constants::tolerance) const { return std::fabs(X) < tolerance && std::fabs(Y) < tolerance && std::fabs(Z) < tolerance; }
        inline int compare(const Vector3<T>& x) const { return std::signbit(magnitudeSquared() - x.magnitudeSquared()); }

        inline Vector3<T>& operator+=(const Vector3<T>& rhs) { X += rhs.X; Y += rhs.Y, Z += rhs.Z; return *this; }
        inline Vector3<T>& operator-=(const Vector3<T>& rhs) { X -= rhs.X; Y -= rhs.Y, Z -= rhs.Z; return *this; }
        inline friend Vector3<T> operator-(const Vector3<T>& v) { return Vector3<T>(-v.X, -v.Y, -v.Z); }
        inline friend bool operator==(const Vector3<T>& lhs, const Vector3<T>& other) { return lhs.X == other.X && lhs.Y == other.Y && lhs.Z == other.Z; }
        inline friend bool operator!=(const Vector3<T>& lhs, const Vector3<T>& other) { return !(lhs == other); }

        inline friend std::ostream& operator<<(std::ostream& out, const Vector3<T>& v) { return (out << "Vector3<T>(X = " << v.X << ", Y = " << v.Y << ", Z = " << v.Z << ")"); }
        inline friend Vector3<T> operator+(const Vector3<T>& lhs, const Vector3<T>& rhs) { return Vector3<T>(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z); }
        inline friend Vector3<T> operator+(const Vector3<T>& lhs, T rhs) { return Vector3<T>(lhs.X + rhs, lhs.Y + rhs, lhs.Z + rhs); }
        inline friend Vector3<T> operator+(T lhs, const Vector3<T>& rhs) { return Vector3<T>(lhs + rhs.X, lhs + rhs.Y, lhs + rhs.Z); }

        inline friend Vector3<T> operator-(const Vector3<T>& lhs, const Vector3<T>& rhs) { return Vector3<T>(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z); }
        inline friend Vector3<T> operator-(const Vector3<T>& lhs, T rhs) { return Vector3<T>(lhs.X - rhs, lhs.Y - rhs, lhs.Z - rhs); }
        inline friend Vector3<T> operator-(T lhs, const Vector3<T>& rhs) { return Vector3<T>(lhs - rhs.X, lhs - rhs.Y, lhs - rhs.Z); }

        inline friend Vector3<T> operator*(const Vector3<T>& lhs, const Vector3<T>& rhs) { return Vector3<T>(lhs.X * rhs.X, lhs.Y * rhs.Y, lhs.Z * rhs.Z); }
        inline friend Vector3<T> operator*(const Vector3<T>& lhs, T rhs) { return Vector3<T>(lhs.X * rhs, lhs.Y * rhs, lhs.Z * rhs); }
        inline friend Vector3<T> operator*(T lhs, const Vector3<T>& rhs) { return Vector3<T>(lhs * rhs.X, lhs * rhs.Y, lhs * rhs.Z); }

        inline friend Vector3<T> operator/(const Vector3<T>& lhs, const Vector3<T>& rhs) { return Vector3<T>(lhs.X / rhs.X, lhs.Y / rhs.Y, lhs.Z / rhs.Z); }
        inline friend Vector3<T> operator/(const Vector3<T>& lhs, T rhs) { return Vector3<T>(lhs.X / rhs, lhs.Y / rhs, lhs.Z / rhs); }
        inline friend Vector3<T> operator/(T lhs, const Vector3<T>& rhs) { return Vector3<T>(lhs / rhs.X, lhs / rhs.Y, lhs / rhs.Z); }

        inline friend bool operator<(const Vector3<T>& x0, const Vector3<T>& x1) { return x0.compare(x1) < 0; }
        inline friend bool operator<=(const Vector3<T>& x0, const Vector3<T>& x1) { return x0.compare(x1) <= 0; }
        inline friend bool operator>(const Vector3<T>& x0, const Vector3<T>& x1) { return x0.compare(x1) > 0; }
        inline friend bool operator>=(const Vector3<T>& x0, const Vector3<T>& x1) { return x0.compare(x1) >= 0; }
    };

#define FVector3 Vector3<float>
#define DVector3 Vector3<double>

    template <typename T = float>
    struct Vector4 final {
        T X;
        T Y;
        T Z;
        T W;

        const Vector4(T x, T y, T z = 0, T w = 0) : X(x), Y(y), Z(z), W(w) {}
        const Vector4(T value = 0) : X(value), Y(value), Z(value), W(value) {}
        const Vector4(const Vector3<T>& v, T w = 0) : X(v.X), Y(v.Y), Z(v.Z), W(w) {}
        const Vector4(const Vector2<T>& v, T z = 0, T w = 0) : X(v.X), Y(v.Y), Z(z), W(w) {}

        inline static const Vector4<T> minValue() { return Vector4<T>(std::numeric_limits<T>::min()); }
        inline static const Vector4<T> maxValue() { return Vector4<T>(std::numeric_limits<T>::max()); }
        inline static const Vector4<T> zero() { return Vector4<T>(0); }
        inline static const Vector4<T> one() { return Vector4<T>(1); }
        inline static const Vector4<T> unitX() { return Vector4<T>(1, 0, 0, 0);; }
        inline static const Vector4<T> unitY() { return Vector4<T>(0, 1, 0, 0); }
        inline static const Vector4<T> unitZ() { return Vector4<T>(0, 0, 1, 0); }
        inline static const Vector4<T> unitW() { return Vector4<T>(0, 0, 0, 1); }

        inline T minComponent() const { return std::min(std::min(std::min(X, Y), Z), W); }
        inline T maxComponent() const { return  std::max(std::max(std::max(X, Y), Z), W); }
        inline T sumComponents() const { return X + Y + Z + W; }
        inline T sumSqrComponents() const { return X * X + Y * Y + Z * Z + W * W; }
        inline T productComponents() const { return X * Y * Z * W; }
        inline T getComponent(int n) const { return n == 0 ? X : n == 1 ? Y : n == 2 ? Z : W; }
        inline bool anyComponentNegative() const { return minComponent() < 0.0; }
        inline T magnitudeSquared() const { return sumSqrComponents(); }
        inline T magnitude() const { return std::sqrt(magnitudeSquared()); }
        inline bool isnan() const { return std::isnan(X) || std::isnan(Y) || std::isnan(Z) || std::isnan(W); }
        inline bool isinf() const { return std::isinf(X) || std::isinf(Y) || std::isinf(Z) || std::isinf(W); }

        inline Vector4<T> abs() const { return Vector4<T>(std::abs(X), std::abs(Y), std::abs(Z), std::abs(W)); }
        inline Vector4<T> acos() const { return Vector4<T>(std::acos(X), std::acos(Y), std::acos(Z), std::acos(W)); }
        inline Vector4<T> asin() const { return Vector4<T>(std::asin(X), std::asin(Y), std::asin(Z), std::asin(W)); }
        inline Vector4<T> atan() const { return Vector4<T>(std::atan(X), std::atan(Y), std::atan(Z), std::atan(W)); }
        inline Vector4<T> cos() const { return Vector4<T>(std::cos(X), std::cos(Y), std::cos(Z), std::cos(W)); }
        inline Vector4<T> cosh() const { return Vector4<T>(std::cosh(X), std::cosh(Y), std::cosh(Z), std::cosh(W)); }
        inline Vector4<T> exp() const { return Vector4<T>(std::exp(X), std::exp(Y), std::exp(Z), std::exp(W)); }
        inline Vector4<T> log() const { return Vector4<T>(std::log(X), std::log(Y), std::log(Z), std::log(W)); }
        inline Vector4<T> log10() const { return Vector4<T>(std::log10(X), std::log10(Y), std::log10(Z), std::log10(W)); }
        inline Vector4<T> sin() const { return Vector4<T>(std::sin(X), std::sin(Y), std::sin(Z), std::sin(W)); }
        inline Vector4<T> sinh() const { return Vector4<T>(std::sinh(X), std::sinh(Y), std::sinh(Z), std::sinh(W)); }
        inline Vector4<T> sqrt() const { return Vector4<T>(std::sqrt(X), std::sqrt(Y), std::sqrt(Z), std::sqrt(W)); }
        inline Vector4<T> tan() const { return Vector4<T>(std::tan(X), std::tan(Y), std::tan(Z), std::tan(W)); }
        inline Vector4<T> tanh() const { return Vector4<T>(std::tanh(X), std::tanh(Y), std::tanh(Z), std::tanh(W)); }
        inline Vector4<T> inverse() const { return Vector4<T>((1 / X), (1 / Y), (1 / Z), (1 / W)); }
        inline Vector4<T> ceiling() const { return Vector4<T>(std::ceil(X), std::ceil(Y), std::ceil(Z), std::ceil(W)); }
        inline Vector4<T> floor() const { return Vector4<T>(std::floor(X), std::floor(Y), std::floor(Z), std::floor(W)); }
        inline Vector4<T> round() const { return Vector4<T>(std::round(X), std::round(Y), std::round(Z), std::round(Z)); }
        inline Vector4<T> truncate() const { return Vector4<T>(std::trunc(X), std::trunc(Y), std::trunc(Z), std::trunc(W)); }
        inline Vector4<T> sqr() const { return Vector4<T>(X * X, Y * Y, Z * Z, W * W); }
        inline Vector4<T> cube() const { return Vector4<T>(X * X * X, Y * Y * Y, Z * Z * Z, W * W * W); }
        inline Vector4<T> toRadians() const { return Vector4<T>(X * constants::degreesToRadians, Y * constants::degreesToRadians, Z * constants::degreesToRadians, W * constants::degreesToRadians); }
        inline Vector4<T> toDegrees() const { return Vector4<T>(X * constants::radiansToDegrees, Y * constants::radiansToDegrees, Z * constants::radiansToDegrees, W * constants::radiansToDegrees); }

        inline T lengthSquared() const { return sumSqrComponents(); }
        inline T length() const { return std::sqrt(lengthSquared()); }
        inline T distance(const Vector4<T>& v2) const { return (*this - v2).length(); }
        inline T distanceSquared(const Vector4<T>& v2) const { return (*this - v2).lengthSquared(); }
        inline Vector4<T> normalize() const { return (*this) / length(); }
        inline Vector4<T> safeNormalize() const { auto l = length(); return (l != 0) ? (*this / l) : (*this); }
        inline T dot(const Vector4<T>& v2) const { return X * v2.X + Y * v2.Y + Z * v2.Z + W * v2.W; }
        inline Vector4<T> min(const Vector4<T>& v2) const { return Vector4<T>(std::min(X, v2.X), std::min(Y, v2.Y), std::min(Z, v2.Z), std::min(W, v2.W)); }
        inline Vector4<T> max(const Vector4<T>& v2) const { return Vector4<T>(std::max(X, v2.X), std::max(Y, v2.Y), std::max(Z, v2.Z), std::max(W, v2.W)); }
        inline Vector4<T> squareRoot() const { return sqrt(); }

        inline Vector4<T> lerp(const Vector4<T>& v2, T t) const { return (*this) + (v2 - (*this)) * t; }
        inline Vector4<T> inverseLerp(const Vector4<T>& a, const Vector4<T>& b) const { return ((*this) - a) / (b - a); }
        inline Vector4<T> lerpPrecise(const Vector4<T>& v2, T t) const { return ((1 - t) * (*this)) + (v2 * t); }
        inline Vector4<T> clampLower(const Vector4<T>& m) const { return max(m); }
        inline Vector4<T> clampUpper(const Vector4<T>& m) const { return min(m); }
        inline Vector4<T> clamp(const Vector4<T>& mn, const Vector4<T>& mx) const { return min(mx).max(mn); }
        inline Vector4<T> average(const Vector4<T>& v2) const { return lerp(v2, (T)0.5); }
        inline Vector4<T> barycentric(const Vector4<T>& v2, const Vector4<T>& v3, T u, T v) const { return (*this) + (v2 - (*this)) * u + (v3 - (*this)) * v; }

        inline Vector4<T> add(const Vector4<T>& v2) const { return (*this) + v2; }
        inline Vector4<T> subtract(const Vector4<T>& v2) const { return (*this) - v2; }
        inline Vector4<T> multiply(const Vector4<T>& v2) const { return (*this) * v2; }
        inline Vector4<T> divide(const Vector4<T>& v2) const { return (*this) / v2; }
        inline Vector4<T> negate() const { return -(*this); }

        inline Vector4<T> setX(T x) const { return Vector4<T>(x, Y, Z, W); }
        inline Vector4<T> setY(T y) const { return Vector4<T>(X, y, Z, W); }
        inline Vector4<T> setZ(T z) const { return Vector4<T>(X, Y, z, W); }
        inline Vector4<T> setW(T w) const { return Vector4<T>(X, Y, Z, w); }

        inline Vector3<T> xyz() const { return Vector3<T>(X, Y, Z); }
        inline Vector2<T> xy() const { return Vector2<T>(X, Y); }

        inline std::size_t hash() const { return hash::combine(X, Y, Z, W); }
        inline bool almostEquals(const Vector4<T>& x, float tolerance = constants::tolerance) const {
            return std::fabs(X - x.X) <= tolerance && std::fabs(Y - x.Y) <= tolerance && std::fabs(Z - x.Z) <= tolerance && std::fabs(W - x.W) <= tolerance;
        }
        inline bool almostZero(float tolerance = constants::tolerance) const {
            return std::fabs(X) < tolerance && std::fabs(Y) < tolerance && std::fabs(Z) < tolerance && std::fabs(W) < tolerance;
        }
        inline int compare(const Vector4<T>& x) const { return std::signbit(magnitudeSquared() - x.magnitudeSquared()); }

        inline friend Vector4<T> operator-(const Vector4<T>& v) { return Vector4<T>(-v.X, -v.Y, -v.Z, -v.W); }

        inline friend bool operator==(const Vector4<T>& lhs, const Vector4<T>& other) { return lhs.X == other.X && lhs.Y == other.Y && lhs.Z == other.Z && lhs.W == other.W; }
        inline friend bool operator!=(const Vector4<T>& lhs, const Vector4<T>& other) { return !(lhs == other); }

        inline friend std::ostream& operator<<(std::ostream& out, const Vector4<T>& v) { return (out << "Vector4<T>(X = " << v.X << ", Y = " << v.Y << ", Z = " << v.Z << ", W = " << v.W << ")"); }
        inline friend Vector4<T> operator+(const Vector4<T>& lhs, const Vector4<T>& rhs) { return Vector4<T>(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z, lhs.W + rhs.W); }
        inline friend Vector4<T> operator+(const Vector4<T>& lhs, T rhs) { return Vector4<T>(lhs.X + rhs, lhs.Y + rhs, lhs.Z + rhs, lhs.W + rhs); }
        inline friend Vector4<T> operator+(T lhs, const Vector4<T>& rhs) { return Vector4<T>(lhs + rhs.X, lhs + rhs.Y, lhs + rhs.Z, lhs + rhs.W); }

        inline friend Vector4<T> operator-(const Vector4<T>& lhs, const Vector4<T>& rhs) { return Vector4<T>(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z, lhs.W - rhs.W); }
        inline friend Vector4<T> operator-(const Vector4<T>& lhs, T rhs) { return Vector4<T>(lhs.X - rhs, lhs.Y - rhs, lhs.Z - rhs, lhs.W - rhs); }
        inline friend Vector4<T> operator-(T lhs, const Vector4<T>& rhs) { return Vector4<T>(lhs - rhs.X, lhs - rhs.Y, lhs - rhs.Z, lhs - rhs.W); }

        inline friend Vector4<T> operator*(const Vector4<T>& lhs, const Vector4<T>& rhs) { return Vector4<T>(lhs.X * rhs.X, lhs.Y * rhs.Y, lhs.Z * rhs.Z, lhs.W * rhs.W); }
        inline friend Vector4<T> operator*(const Vector4<T>& lhs, T rhs) { return Vector4<T>(lhs.X * rhs, lhs.Y * rhs, lhs.Z * rhs, lhs.W * rhs); }
        inline friend Vector4<T> operator*(T lhs, const Vector4<T>& rhs) { return Vector4<T>(lhs * rhs.X, lhs * rhs.Y, lhs * rhs.Z, lhs * rhs.W); }

        inline friend Vector4<T> operator/(const Vector4<T>& lhs, const Vector4<T>& rhs) { return Vector4<T>(lhs.X / rhs.X, lhs.Y / rhs.Y, lhs.Z / rhs.Z, lhs.W / rhs.W); }
        inline friend Vector4<T> operator/(const Vector4<T>& lhs, T rhs) { return Vector4<T>(lhs.X / rhs, lhs.Y / rhs, lhs.Z / rhs, lhs.W / rhs); }
        inline friend Vector4<T> operator/(T lhs, const Vector4<T>& rhs) { return Vector4<T>(lhs / rhs.X, lhs / rhs.Y, lhs / rhs.Z, lhs / rhs.W); }

        inline friend bool operator<(const Vector4<T>& x0, const Vector4<T>& x1) { return x0.compare(x1) < 0; }
        inline friend bool operator<=(const Vector4<T>& x0, const Vector4<T>& x1) { return x0.compare(x1) <= 0; }
        inline friend bool operator>(const Vector4<T>& x0, const Vector4<T>& x1) { return x0.compare(x1) > 0; }
        inline friend bool operator>=(const Vector4<T>& x0, const Vector4<T>& x1) { return x0.compare(x1) >= 0; }
    };

#define FVector4 Vector4<float>
#define DVector4 Vector4<double>

    template <typename T = float>
    struct CylindricalCoordinate final {
        T Radius;
        T Azimuth;
        T Height;

        const CylindricalCoordinate(T radius, T azimuth, T height) : Radius(radius), Azimuth(azimuth), Height(height) {}
        const CylindricalCoordinate(T value) : Radius(value), Azimuth(value), Height(value) {}

        inline static const CylindricalCoordinate zero() { return CylindricalCoordinate(0); }
        inline static const CylindricalCoordinate minValue() { return CylindricalCoordinate(std::numeric_limits<T>::min()); }
        inline static const CylindricalCoordinate maxValue() { return CylindricalCoordinate(std::numeric_limits<T>::max()); }

        inline CylindricalCoordinate setRadius(T r) const { return CylindricalCoordinate(r, Azimuth, Height); }
        inline CylindricalCoordinate setAzimuth(T a) const { return CylindricalCoordinate(Radius, a, Height); }
        inline CylindricalCoordinate setHeight(T h) const { return CylindricalCoordinate(Radius, Azimuth, h); }

        inline std::size_t hash() const { return hash::combine(Radius, Azimuth, Height); }
        inline bool almostEquals(const CylindricalCoordinate& x, float tolerance = constants::tolerance) const {
            return std::fabs(Radius - x.Radius) < tolerance
                && std::fabs(Azimuth - x.Azimuth) < tolerance
                && std::fabs(Height - x.Height) < tolerance;
        }

        inline friend bool operator==(const CylindricalCoordinate& o, const CylindricalCoordinate& other) { return o.Radius == other.Radius && o.Azimuth == other.Azimuth && o.Height == other.Height; }
        inline friend bool operator!=(const CylindricalCoordinate& o, const CylindricalCoordinate& other) { return !(o == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const CylindricalCoordinate& v) { return (out << "CylindricalCoordinate(Radius = " << v.Radius << ", Azimuth = " << v.Azimuth << ", Height = " << v.Height << ")"); }
    };

#define FCylindricalCoordinate CylindricalCoordinate<float>
#define DCylindricalCoordinate CylindricalCoordinate<double>

    template <typename T = float>
    struct GeoCoordinate final {
        T Latitude;
        T Longitude;

        const GeoCoordinate(T x, T y) : Latitude(x), Longitude(y) {}
        const GeoCoordinate(T value = 0) : Latitude(value), Longitude(value) {}

        inline static const GeoCoordinate zero() { return GeoCoordinate(0); }
        inline static const GeoCoordinate minValue() { return GeoCoordinate(std::numeric_limits<T>::min()); }
        inline static const GeoCoordinate maxValue() { return GeoCoordinate(std::numeric_limits<T>::max()); }
        inline static const GeoCoordinate one() { return GeoCoordinate(1); }
        inline static const GeoCoordinate unitX() { return GeoCoordinate(1, 0); }
        inline static const GeoCoordinate unitY() { return GeoCoordinate(0, 1); }

        inline GeoCoordinate setLatitude(T x) const { return GeoCoordinate(x, Longitude); }
        inline GeoCoordinate setLongitude(T y) const { return GeoCoordinate(Latitude, y); }

        inline std::size_t hash() const { return hash::combine(Latitude, Longitude); }
        inline bool almostEquals(const GeoCoordinate& x, float tolerance = constants::tolerance) const {
            return std::fabs(Latitude - x.Latitude) <= tolerance
                && std::fabs(Longitude - x.Longitude) <= tolerance;
        }
        inline static T dot(const GeoCoordinate& value1, const GeoCoordinate& value2) {
            return value1.Latitude * value2.Latitude + value1.Longitude * value2.Longitude;
        }
        inline T dot(const GeoCoordinate& value) const { return dot(*this, value); }
        inline bool almostZero(float tolerance = constants::tolerance) const {
            return std::fabs(Latitude) < tolerance
                && std::fabs(Longitude) < tolerance;
        }

        inline T minComponent() const { return std::min(Latitude, Longitude); }
        inline T maxComponent() const { return std::max(Latitude, Longitude); }
        inline T sumComponents() const { return Latitude + Longitude; }
        inline T sumSqrComponents() const { return Latitude * Latitude + Longitude * Longitude; }
        inline T productComponents() const { return Latitude * Longitude; }
        inline T getComponent(int n) const { return n == 0 ? Latitude : Longitude; }
        inline T magnitudeSquared() const { return sumSqrComponents(); }
        inline T magnitude() const { return std::sqrt(magnitudeSquared()); }
        inline bool anyComponentNegative() const { return minComponent() < 0.0; }
        inline bool isnan() const { return std::isnan(Latitude) || std::isnan(Longitude); }
        inline bool isinf() const { return std::isinf(Latitude) || std::isinf(Longitude); }
        inline int compare(const GeoCoordinate& x) const { return std::signbit(magnitudeSquared() - x.magnitudeSquared()); }

        inline friend GeoCoordinate operator-(const GeoCoordinate& l) { return { -l.Latitude, -l.Longitude }; }

        inline friend bool operator==(const GeoCoordinate& l, const GeoCoordinate& other) { return l.Latitude == other.Latitude && l.Longitude == other.Longitude; }
        inline friend bool operator!=(const GeoCoordinate& l, const GeoCoordinate& other) { return !(l == other); }

        inline friend std::ostream& operator<<(std::ostream& out, const GeoCoordinate& v) { return (out << "GeoCoordinate(Latitude = " << v.Latitude << ", Longitude = " << v.Longitude << ")"); }
        inline friend GeoCoordinate operator+(const GeoCoordinate& lhs, const GeoCoordinate& rhs) { return GeoCoordinate(lhs.Latitude + rhs.Latitude, lhs.Longitude + rhs.Longitude); }
        inline friend GeoCoordinate operator+(const GeoCoordinate& lhs, T rhs) { return GeoCoordinate(lhs.Latitude + rhs, lhs.Longitude + rhs); }
        inline friend GeoCoordinate operator+(T lhs, const GeoCoordinate& rhs) { return GeoCoordinate(lhs + rhs.Latitude, lhs + rhs.Longitude); }

        inline friend GeoCoordinate operator-(const GeoCoordinate& lhs, const GeoCoordinate& rhs) { return GeoCoordinate(lhs.Latitude - rhs.Latitude, lhs.Longitude - rhs.Longitude); }
        inline friend GeoCoordinate operator-(const GeoCoordinate& lhs, T rhs) { return GeoCoordinate(lhs.Latitude - rhs, lhs.Longitude - rhs); }
        inline friend GeoCoordinate operator-(T lhs, const GeoCoordinate& rhs) { return GeoCoordinate(lhs - rhs.Latitude, lhs - rhs.Longitude); }

        inline friend GeoCoordinate operator*(const GeoCoordinate& lhs, const GeoCoordinate& rhs) { return GeoCoordinate(lhs.Latitude * rhs.Latitude, lhs.Longitude * rhs.Longitude); }
        inline friend GeoCoordinate operator*(const GeoCoordinate& lhs, T rhs) { return GeoCoordinate(lhs.Latitude * rhs, lhs.Longitude * rhs); }
        inline friend GeoCoordinate operator*(T lhs, const GeoCoordinate& rhs) { return GeoCoordinate(lhs * rhs.Latitude, lhs * rhs.Longitude); }

        inline friend GeoCoordinate operator/(const GeoCoordinate& lhs, const GeoCoordinate& rhs) { return GeoCoordinate(lhs.Latitude / rhs.Latitude, lhs.Longitude / rhs.Longitude); }
        inline friend GeoCoordinate operator/(const GeoCoordinate& lhs, T rhs) { return GeoCoordinate(lhs.Latitude / rhs, lhs.Longitude / rhs); }
        inline friend GeoCoordinate operator/(T lhs, const GeoCoordinate& rhs) { return GeoCoordinate(lhs / rhs.Latitude, lhs / rhs.Longitude); }

        inline friend bool operator<(const GeoCoordinate& x0, const GeoCoordinate& x1) { return x0.compare(x1) < 0; }
        inline friend bool operator<=(const GeoCoordinate& x0, const GeoCoordinate& x1) { return x0.compare(x1) <= 0; }
        inline friend bool operator>(const GeoCoordinate& x0, const GeoCoordinate& x1) { return x0.compare(x1) > 0; }
        inline friend bool operator>=(const GeoCoordinate& x0, const GeoCoordinate& x1) { return x0.compare(x1) >= 0; }
    };

#define FGeoCoordinate GeoCoordinate<float>
#define DGeoCoordinate GeoCoordinate<double>

    template <typename T = float>
    struct LogPolarCoordinate final {
        T Rho;
        T Azimuth;

        const LogPolarCoordinate(T rho, T azimuth) : Rho(rho), Azimuth(azimuth) {}
        const LogPolarCoordinate(T value) : Rho(value), Azimuth(value) {}

        inline static const LogPolarCoordinate zero() { return LogPolarCoordinate(0); }
        inline static const LogPolarCoordinate minValue() { return LogPolarCoordinate(std::numeric_limits<T>::min()); }
        inline static const LogPolarCoordinate maxValue() { return LogPolarCoordinate(std::numeric_limits<T>::max()); }

        inline LogPolarCoordinate setRho(T r) const { return LogPolarCoordinate(r, Azimuth); }
        inline LogPolarCoordinate setAzimuth(T a) const { return LogPolarCoordinate(Rho, a); }

        inline std::size_t hash() const { return hash::combine(Rho, Azimuth); }
        inline bool almostEquals(const LogPolarCoordinate& x, float tolerance = constants::tolerance) const {
            return std::fabs(Rho - x.Rho) < tolerance && std::fabs(Azimuth - x.Azimuth) < tolerance;
        }

        inline friend bool operator==(const LogPolarCoordinate& o, const LogPolarCoordinate& other) { return o.Rho == other.Rho && o.Azimuth == other.Azimuth; }
        inline friend bool operator!=(const LogPolarCoordinate& o, const LogPolarCoordinate& other) { return !(o == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const LogPolarCoordinate& v) { return (out << "LogPolarCoordinate(Rho = " << v.Rho << ", Azimuth = " << v.Azimuth << ")"); }
    };

#define FLogPolarCoordinate LogPolarCoordinate<float>
#define DLogPolarCoordinate LogPolarCoordinate<double>

    template <typename T = float>
    struct PolarCoordinate final {
        T Radius;
        T Azimuth;

        const PolarCoordinate(T radius, T azimuth) : Radius(radius), Azimuth(azimuth) {}
        const PolarCoordinate(T value) : Radius(value), Azimuth(value) {}

        inline static const PolarCoordinate zero() { return PolarCoordinate(0); }
        inline static const PolarCoordinate minValue() { return PolarCoordinate(std::numeric_limits<T>::min()); }
        inline static const PolarCoordinate maxValue() { return PolarCoordinate(std::numeric_limits<T>::max()); }

        inline PolarCoordinate setRadius(T r) const { return PolarCoordinate(r, Azimuth); }
        inline PolarCoordinate setAzimuth(T a) const { return PolarCoordinate(Radius, a); }

        inline std::size_t hash() const { return hash::combine(Radius, Azimuth); }
        inline bool almostEquals(const PolarCoordinate& x, float tolerance = constants::tolerance) const {
            return std::fabs(Radius - x.Radius) < tolerance && std::fabs(Azimuth - x.Azimuth) < tolerance;
        }

        inline friend bool operator==(const PolarCoordinate& o, const PolarCoordinate& other) { return o.Radius == other.Radius && o.Azimuth == other.Azimuth; }
        inline friend bool operator!=(const PolarCoordinate& o, const PolarCoordinate& other) { return !(o == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const PolarCoordinate& v) { return (out << "PolarCoordinate(Radius = " << v.Radius << ", Azimuth = " << v.Azimuth << ")"); }
    };

#define FPolarCoordinate PolarCoordinate<float>
#define DPolarCoordinate PolarCoordinate<double>

    template <typename T = float>
    struct SphericalCoordinate final {
        T Radius;
        T Azimuth;
        T Inclination;

        const SphericalCoordinate(T radius, T azimuth, T inclination) : Radius(radius), Azimuth(azimuth), Inclination(inclination) {}
        const SphericalCoordinate(T value) : Radius(value), Azimuth(value), Inclination(value) {}

        inline static const SphericalCoordinate zero() { return SphericalCoordinate(0); }
        inline static const SphericalCoordinate minValue() { return SphericalCoordinate(std::numeric_limits<T>::min()); }
        inline static const SphericalCoordinate maxValue() { return SphericalCoordinate(std::numeric_limits<T>::max()); }

        inline SphericalCoordinate setRadius(T r) const { return SphericalCoordinate(r, Azimuth, Inclination); }
        inline SphericalCoordinate setAzimuth(T a) const { return SphericalCoordinate(Radius, a, Inclination); }
        inline SphericalCoordinate setInclination(T h) const { return SphericalCoordinate(Radius, Azimuth, h); }

        inline std::size_t hash() const { return hash::combine((Radius), (Azimuth), (Inclination)); }
        inline bool almostEquals(const SphericalCoordinate& x, float tolerance = constants::tolerance) const {
            return std::fabs(Radius - x.Radius) < tolerance
                && std::fabs(Azimuth - x.Azimuth) < tolerance
                && std::fabs(Inclination - x.Inclination) < tolerance;
        }

        inline friend bool operator==(const SphericalCoordinate& o, const SphericalCoordinate& other) { return o.Radius == other.Radius && o.Azimuth == other.Azimuth && o.Inclination == other.Inclination; }
        inline friend bool operator!=(const SphericalCoordinate& o, const SphericalCoordinate& other) { return !(o == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const SphericalCoordinate& v) { return (out << "SphericalCoordinate(Radius = " << v.Radius << ", Azimuth = " << v.Azimuth << ", Inclination = " << v.Inclination << ")"); }
    };

#define FSphericalCoordinate SphericalCoordinate<float>
#define DSphericalCoordinate SphericalCoordinate<double>

    template <typename T = float>
    struct HorizontalCoordinate final {
        T Azimuth;
        T Inclination;

        const HorizontalCoordinate(T x, T y) : Azimuth(x), Inclination(y) {}
        const HorizontalCoordinate(const Vector2<T>& v) : Azimuth(v.X), Inclination(v.Y) {}
        const HorizontalCoordinate(T value = 0) : Azimuth(value), Inclination(value) {}

        inline static const HorizontalCoordinate<T> zero() { return HorizontalCoordinate<T>(0); }
        inline static const HorizontalCoordinate<T> minValue() { return HorizontalCoordinate<T>(FLT_MIN); }
        inline static const HorizontalCoordinate<T> maxValue() { return HorizontalCoordinate<T>(FLT_MAX); }
        inline static const HorizontalCoordinate<T> one() { return HorizontalCoordinate<T>(1); }
        inline static const HorizontalCoordinate<T> unitAzimuth() { return HorizontalCoordinate<T>(1, 0); }
        inline static const HorizontalCoordinate<T> unitInclination() { return HorizontalCoordinate<T>(0, 1); }

        inline HorizontalCoordinate<T> setAzimuth(T x) const { return HorizontalCoordinate<T>(x, Inclination); }
        inline HorizontalCoordinate<T> setInclination(T y) const { return HorizontalCoordinate<T>(Azimuth, y); }

        inline std::size_t hash() const { return hash::combine(Azimuth, Inclination); }
        inline bool almostEquals(const HorizontalCoordinate<T>& x, float tolerance = constants::tolerance) const {
            return std::fabs(Azimuth - x.Azimuth) <= tolerance && std::fabs(Inclination - x.Inclination) <= tolerance;
        }
        inline static T dot(const HorizontalCoordinate<T>& value1, const HorizontalCoordinate<T>& value2) {
            return value1.Azimuth * value2.Azimuth + value1.Inclination * value2.Inclination;
        }
        inline T dot(const HorizontalCoordinate<T>& value) const { return dot(*this, value); }
        inline bool almostZero(float tolerance = constants::tolerance) const {
            return std::fabs(Azimuth) < tolerance && std::fabs(Inclination) < tolerance;
        }

        inline T minComponent() const { return std::min(Azimuth, Inclination); }
        inline T maxComponent() const { return std::max(Azimuth, Inclination); }
        inline T sumComponents() const { return Azimuth + Inclination; }
        inline T sumSqrComponents() const { return Azimuth * Azimuth + Inclination * Inclination; }
        inline T productComponents() const { return Azimuth * Inclination; }
        inline T getComponent(int n) const { return n == 0 ? Azimuth : Inclination; }
        inline T magnitudeSquared() const { return sumSqrComponents(); }
        inline T magnitude() const { return std::sqrt(magnitudeSquared()); }
        inline bool anyComponentNegative() const { return minComponent() < 0.0; }
        inline bool isnan() const { return std::isnan(Azimuth) || std::isnan(Inclination); }
        inline bool isinf() const { return std::isinf(Azimuth) || std::isinf(Inclination); }
        inline int compare(const HorizontalCoordinate<T>& x) const { return std::signbit(magnitudeSquared() - x.magnitudeSquared()); }

        inline friend HorizontalCoordinate<T> operator-(const HorizontalCoordinate<T>& l) { return { -l.Azimuth, -l.Inclination }; }
        inline friend bool operator==(const HorizontalCoordinate<T>& l, const HorizontalCoordinate<T>& other) { return l.Azimuth == other.Azimuth && l.Inclination == other.Inclination; }
        inline friend bool operator!=(const HorizontalCoordinate<T>& l, const HorizontalCoordinate<T>& other) { return !(l == other); }

        inline friend std::ostream& operator<<(std::ostream& out, const HorizontalCoordinate<T>& v) { return (out << "HorizontalCoordinate<T>(Azimuth = " << v.Azimuth << ", Inclination = " << v.Inclination << ")"); }
        inline friend HorizontalCoordinate<T> operator+(const HorizontalCoordinate<T>& lhs, const HorizontalCoordinate<T>& rhs) { return HorizontalCoordinate<T>(lhs.Azimuth + rhs.Azimuth, lhs.Inclination + rhs.Inclination); }
        inline friend HorizontalCoordinate<T> operator+(const HorizontalCoordinate<T>& lhs, T rhs) { return HorizontalCoordinate<T>(lhs.Azimuth + rhs, lhs.Inclination + rhs); }
        inline friend HorizontalCoordinate<T> operator+(T lhs, const HorizontalCoordinate<T>& rhs) { return HorizontalCoordinate<T>(lhs + rhs.Azimuth, lhs + rhs.Inclination); }

        inline friend HorizontalCoordinate<T> operator-(const HorizontalCoordinate<T>& lhs, const HorizontalCoordinate<T>& rhs) { return HorizontalCoordinate<T>(lhs.Azimuth - rhs.Azimuth, lhs.Inclination - rhs.Inclination); }
        inline friend HorizontalCoordinate<T> operator-(const HorizontalCoordinate<T>& lhs, T rhs) { return HorizontalCoordinate<T>(lhs.Azimuth - rhs, lhs.Inclination - rhs); }
        inline friend HorizontalCoordinate<T> operator-(T lhs, const HorizontalCoordinate<T>& rhs) { return HorizontalCoordinate<T>(lhs - rhs.Azimuth, lhs - rhs.Inclination); }

        inline friend HorizontalCoordinate<T> operator*(const HorizontalCoordinate<T>& lhs, const HorizontalCoordinate<T>& rhs) { return HorizontalCoordinate<T>(lhs.Azimuth * rhs.Azimuth, lhs.Inclination * rhs.Inclination); }
        inline friend HorizontalCoordinate<T> operator*(const HorizontalCoordinate<T>& lhs, T rhs) { return HorizontalCoordinate<T>(lhs.Azimuth * rhs, lhs.Inclination * rhs); }
        inline friend HorizontalCoordinate<T> operator*(T lhs, const HorizontalCoordinate<T>& rhs) { return HorizontalCoordinate<T>(lhs * rhs.Azimuth, lhs * rhs.Inclination); }

        inline friend HorizontalCoordinate<T> operator/(const HorizontalCoordinate<T>& lhs, const HorizontalCoordinate<T>& rhs) { return HorizontalCoordinate<T>(lhs.Azimuth / rhs.Azimuth, lhs.Inclination / rhs.Inclination); }
        inline friend HorizontalCoordinate<T> operator/(const HorizontalCoordinate<T>& lhs, T rhs) { return HorizontalCoordinate<T>(lhs.Azimuth / rhs, lhs.Inclination / rhs); }
        inline friend HorizontalCoordinate<T> operator/(T lhs, const HorizontalCoordinate<T>& rhs) { return HorizontalCoordinate<T>(lhs / rhs.Azimuth, lhs / rhs.Inclination); }

        inline friend bool operator<(const HorizontalCoordinate<T>& x0, const HorizontalCoordinate<T>& x1) { return x0.compare(x1) < 0; }
        inline friend bool operator<=(const HorizontalCoordinate<T>& x0, const HorizontalCoordinate<T>& x1) { return x0.compare(x1) <= 0; }
        inline friend bool operator>(const HorizontalCoordinate<T>& x0, const HorizontalCoordinate<T>& x1) { return x0.compare(x1) > 0; }
        inline friend bool operator>=(const HorizontalCoordinate<T>& x0, const HorizontalCoordinate<T>& x1) { return x0.compare(x1) >= 0; }
    };

#define FHorizontalCoordinate HorizontalCoordinate<float>
#define DHorizontalCoordinate HorizontalCoordinate<double>

    template <typename T = float>
    struct AxisAngle final {
        Vector3<T> Axis;
        T Angle;

        const AxisAngle(const Vector3<T>& axis, T angle) : Axis(axis), Angle(angle) {}

        inline static const AxisAngle zero() { return AxisAngle(Vector3<T>::zero(), 0); }
        inline static const AxisAngle minValue() { return AxisAngle(Vector3<T>::minValue(), std::numeric_limits<T>::min()); }
        inline static const AxisAngle maxValue() { return AxisAngle(Vector3<T>::maxValue(), std::numeric_limits<T>::max()); }

        inline AxisAngle setAxis(const Vector3<T>& x) const { return AxisAngle(x, Angle); }
        inline AxisAngle setAngle(T x) const { return AxisAngle(Axis, x); }

        inline std::size_t hash() const { return hash::combine((T)Axis.hash(), (Angle)); }
        inline bool almostEquals(const AxisAngle& x, float tolerance = constants::tolerance) const {
            return std::fabs(Axis.X - x.Axis.X) < tolerance && std::fabs(Axis.Y - x.Axis.Y) < tolerance && std::fabs(Axis.Z - x.Axis.Z) < tolerance && std::fabs(Angle - x.Angle) < tolerance;
        }

        inline friend bool operator==(const AxisAngle& a, const AxisAngle& x) { return a.Axis == x.Axis && a.Angle == x.Angle; }
        inline friend bool operator!=(const AxisAngle& a, const AxisAngle& other) { return !(a == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const AxisAngle& v) { return (out << "AxisAngle(Axis = " << v.Axis << ", Angle = " << v.Angle << ")"); }
    };

#define FAxisAngle AxisAngle<float>
#define DAxisAngle AxisAngle<double>

    template <typename T = float>
    struct Plane final {
        Vector3<T> Normal;
        T D;

        const Plane(T x, T y, T z, T d) : Normal(Vector3<T>(x, y, z)), D(d) {}
        const Plane(const Vector4<T>& v) : Normal(Vector3<T>(v.X, v.Y, v.Z)), D(v.W) {}
        const Plane(const Vector3<T>& normal, T d) : Normal(Vector3<T>(normal.X, normal.Y, normal.Z)), D(d) {}
        const Plane(const Vector3<T>& normal, const Vector3<T>& point) {
            auto n = normal.normalize();
            D = n.dot(point);
            Normal = n;
        }
        const Plane(const Vector3<T>& point1, const Vector3<T>& point2, const Vector3<T>& point3) {
            auto a = point2 - point1;
            auto b = point3 - point1;
            auto n = a.cross(b);
            D = -n.normalize().dot(point1);
            Normal = n.normalize();
        }

        inline static const Plane<T> zero() { return Plane<T>(Vector3<T>::zero(), 0); }
        inline static const Plane<T> minValue() { return Plane<T>(Vector3<T>::minValue(), std::numeric_limits<T>::min()); }
        inline static const Plane<T> maxValue() { return Plane<T>(Vector3<T>::maxValue(), std::numeric_limits<T>::max()); }
        inline static const Plane<T> xyPlane() { return Plane<T>(Vector3<T>::unitZ(), 0); }
        inline static const Plane<T> xzPlane() { return Plane<T>(Vector3<T>::unitY(), 0); }
        inline static const Plane<T> yzPlane() { return Plane<T>(Vector3<T>::unitX(), 0); }

        inline Plane<T> normalize() const {
            T epsilon = std::numeric_limits<T>::epsilon(); // 1.192092896e-07f; // smallest such that 1.0+FLT_EPSILON != 1.0
            auto normalLengthSquared = Normal.lengthSquared();
            if (std::fabs(normalLengthSquared - 1) < epsilon) {
                // It already normalized, so we don't need to farther process.
                return *this;
            }
            T normalLength = std::sqrt(normalLengthSquared);
            return Plane<T>(Normal / normalLength, D / normalLength);
        }
        inline static Plane<T> normalize(const Plane<T>& plane) { return plane.normalize(); }
        inline Vector3<T> projectPointOntoPlane(const Vector3<T>& point) {
            auto dist = point.dot(Normal) - D;
            return point - Normal * dist;
        }
        inline T dot(const Vector4<T>& value) const { auto v = Vector4<T>(Normal.X, Normal.Y, Normal.Z, D); return v.dot(value); }
        inline static T dot(const Plane<T>& plane, const Vector4<T>& value) { return plane.dot(value); }
        inline static T dotCoordinate(const Plane<T>& plane, const Vector3<T>& value) { return plane.Normal.dot(value) + plane.D; }
        inline static T dotNormal(const Plane<T>& plane, const Vector3<T>& value) { return plane.Normal.dot(value); }
        inline T classifyPoint(const Vector3<T>& point) { return point.dot(Normal) + D; }

        inline Plane<T> setNormal(Vector3<T> x) const { return Plane<T>(x, D); }
        inline Plane<T> setD(T x) const { return Plane<T>(Normal, x); }

        inline std::size_t hash() const { return hash::combine((T)Normal.hash(), (D)); }
        inline bool almostEquals(const Plane<T>& x, float tolerance = constants::tolerance) const {
            return Normal.almostEquals(x.Normal, tolerance) && std::fabs(D - x.D) < tolerance;
        }

        inline friend bool operator==(const Plane<T>& o, const Plane<T>& x) { return o.Normal == x.Normal && o.D == x.D; }
        inline friend bool operator!=(const Plane<T>& o, const Plane<T>& other) { return !(o == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const Plane<T>& v) { return (out << "Plane<T>(Normal = " << v.Normal << ", D = " << v.D << ")"); }
    };

#define FPlane Plane<float>
#define DPlane Plane<double>

    template <typename T = float>
    struct Quaternion final {
        T X;
        T Y;
        T Z;
        T W;

        const Quaternion(T x, T y, T z, T w) : X(x), Y(y), Z(z), W(w) {}
        const Quaternion(T value = 0) : X(value), Y(value), Z(value), W(value) {}
        const Quaternion(const Vector3<T>& vectorPart, T scalarPart) : X(vectorPart.X), Y(vectorPart.Y), Z(vectorPart.Z), W(scalarPart) {}

        inline static const Quaternion<T> identity() { return Quaternion<T>(0, 0, 0, 1); }
        inline static const Quaternion<T> zero() { return Quaternion<T>(0); }
        inline static const Quaternion<T> minValue() { return Quaternion<T>(std::numeric_limits<T>::min()); }
        inline static const Quaternion<T> maxValue() { return Quaternion<T>(std::numeric_limits<T>::max()); }

        inline static Quaternion<T> fromCoordinate(const HorizontalCoordinate<T>& angle) { return zRotation(angle.Azimuth) * xRotation(angle.Inclination); }
        inline static Quaternion<T> fromAxisAngle(const Vector3<T>& axis, T angle) {
            return Quaternion<T>(axis * std::sin(angle * 0.5), std::cos(angle * 0.5));
        }
        inline static Quaternion<T> fromEulerAngles(const Vector3<T>& v) {
            auto c1 = std::cos(v.X / 2);
            auto s1 = std::sin(v.X / 2);
            auto c2 = std::cos(v.Y / 2);
            auto s2 = std::sin(v.Y / 2);
            auto c3 = std::cos(v.Z / 2);
            auto s3 = std::sin(v.Z / 2);

            T qw = c1 * c2 * c3 - s1 * s2 * s3;
            T qx = s1 * c2 * c3 + c1 * s2 * s3;
            T qy = c1 * s2 * c3 - s1 * c2 * s3;
            T qz = c1 * c2 * s3 + s1 * s2 * c3;
            return Quaternion<T>(qx, qy, qz, qw);
        }
        inline static Quaternion<T> xRotation(T theta) { return Quaternion<T>(std::sin(theta * 0.5), 0, 0, std::cos(theta * 0.5)); }
        inline static Quaternion<T> yRotation(T theta) { return Quaternion<T>(0, std::sin(theta * 0.5), 0, std::cos(theta * 0.5)); }
        inline static Quaternion<T> zRotation(T theta) { return Quaternion<T>(0, 0, std::sin(theta * 0.5), std::cos(theta * 0.5)); }
        inline static Quaternion<T> rotationFromAToB(const Vector3<T>& fromA, const Vector3<T>& toB, const Vector3<T>& up) {
            Vector3<T> axis = fromA.cross(toB);
            T lengthSquared = axis.lengthSquared();
            if (lengthSquared > 0) {
                T clamp = std::max(std::min(fromA.dot(toB), (T)1), (T)-1);
                return fromAxisAngle(axis / std::sqrt(lengthSquared), std::acos(clamp));
            }
            else {
                // The vectors are parallel to each other
                if ((fromA + toB).almostZero()) {
                    // The vectors are in opposite directions so rotate by half a circle.
                    return fromAxisAngle(up, constants::pi);
                }
                else {
                    // The vectors are in the same direction so no rotation is required.
                    return Quaternion<T>::identity();
                }
            }
        }
        inline static Quaternion<T> rotationFromAToB(const Vector3<T>& fromA, const Vector3<T>& toB) {
            return rotationFromAToB(fromA, toB, Vector3<T>::unitZ());
        }
        inline static Quaternion<T> lookAt(const Vector3<T>& position, const Vector3<T>& targetPosition, const Vector3<T>& up, const Vector3<T>& forward) {
            auto plane = Plane<T>(up, position);
            auto projectedTarget = plane.projectPointOntoPlane(targetPosition);
            auto projectedDirection = (projectedTarget - position).normalize();
            auto q1 = rotationFromAToB(forward, projectedDirection, up);
            auto q2 = rotationFromAToB(projectedDirection, (targetPosition - position).normalize(), up);
            return q2 * q1;
        }
        inline static Quaternion<T> fromYawPitchRoll(T yaw, T pitch, T roll) {
            auto halfRoll = roll * 0.5;
            auto sr = std::sin(halfRoll);
            auto cr = std::cos(halfRoll);

            auto halfPitch = pitch * 0.5;
            auto sp = std::sin(halfPitch);
            auto cp = std::cos(halfPitch);

            auto halfYaw = yaw * 0.5;
            auto sy = std::sin(halfYaw);
            auto cy = std::cos(halfYaw);

            return Quaternion<T>(
                cy * sp * cr + sy * cp * sr,
                sy * cp * cr - cy * sp * sr,
                cy * cp * sr - sy * sp * cr,
                cy * cp * cr + sy * sp * sr);
        }

        inline static Vector2<T> transform(const Vector2<T>& value, const Quaternion<T>& rotation) {
            auto x2 = rotation.X + rotation.X;
            auto y2 = rotation.Y + rotation.Y;
            auto z2 = rotation.Z + rotation.Z;

            auto wz2 = rotation.W * z2;
            auto xx2 = rotation.X * x2;
            auto xy2 = rotation.X * y2;
            auto yy2 = rotation.Y * y2;
            auto zz2 = rotation.Z * z2;

            return Vector2<T>(
                value.X * (1 - yy2 - zz2) + value.Y * (xy2 - wz2),
                value.X * (xy2 + wz2) + value.Y * (1 - xx2 - zz2));
        }
        inline static Vector4<T> transformToVector4(const Vector2<T>& value, const Quaternion<T>& rotation) {
            auto x2 = rotation.X + rotation.X;
            auto y2 = rotation.Y + rotation.Y;
            auto z2 = rotation.Z + rotation.Z;

            auto wx2 = rotation.W * x2;
            auto wy2 = rotation.W * y2;
            auto wz2 = rotation.W * z2;
            auto xx2 = rotation.X * x2;
            auto xy2 = rotation.X * y2;
            auto xz2 = rotation.X * z2;
            auto yy2 = rotation.Y * y2;
            auto yz2 = rotation.Y * z2;
            auto zz2 = rotation.Z * z2;

            return Vector4<T>(
                value.X * (1 - yy2 - zz2) + value.Y * (xy2 - wz2),
                value.X * (xy2 + wz2) + value.Y * (1 - xx2 - zz2),
                value.X * (xz2 - wy2) + value.Y * (yz2 + wx2),
                1);
        }
        inline static Vector3<T> transform(const Vector3<T>& value, const Quaternion<T>& rotation) {
            auto x2 = rotation.X + rotation.X;
            auto y2 = rotation.Y + rotation.Y;
            auto z2 = rotation.Z + rotation.Z;

            auto wx2 = rotation.W * x2;
            auto wy2 = rotation.W * y2;
            auto wz2 = rotation.W * z2;
            auto xx2 = rotation.X * x2;
            auto xy2 = rotation.X * y2;
            auto xz2 = rotation.X * z2;
            auto yy2 = rotation.Y * y2;
            auto yz2 = rotation.Y * z2;
            auto zz2 = rotation.Z * z2;

            return Vector3<T>(
                value.X * (1 - yy2 - zz2) + value.Y * (xy2 - wz2) + value.Z * (xz2 + wy2),
                value.X * (xy2 + wz2) + value.Y * (1 - xx2 - zz2) + value.Z * (yz2 - wx2),
                value.X * (xz2 - wy2) + value.Y * (yz2 + wx2) + value.Z * (1 - xx2 - yy2));
        }
        inline static Vector4<T> transformToVector4(const Vector3<T>& value, const Quaternion<T>& rotation) {
            auto x2 = rotation.X + rotation.X;
            auto y2 = rotation.Y + rotation.Y;
            auto z2 = rotation.Z + rotation.Z;

            auto wx2 = rotation.W * x2;
            auto wy2 = rotation.W * y2;
            auto wz2 = rotation.W * z2;
            auto xx2 = rotation.X * x2;
            auto xy2 = rotation.X * y2;
            auto xz2 = rotation.X * z2;
            auto yy2 = rotation.Y * y2;
            auto yz2 = rotation.Y * z2;
            auto zz2 = rotation.Z * z2;

            return Vector4<T>(
                value.X * (1 - yy2 - zz2) + value.Y * (xy2 - wz2) + value.Z * (xz2 + wy2),
                value.X * (xy2 + wz2) + value.Y * (1 - xx2 - zz2) + value.Z * (yz2 - wx2),
                value.X * (xz2 - wy2) + value.Y * (yz2 + wx2) + value.Z * (1 - xx2 - yy2),
                1);
        }
        inline static Vector4<T> transform(const Vector4<T>& value, const Quaternion<T>& rotation) {
            auto x2 = rotation.X + rotation.X;
            auto y2 = rotation.Y + rotation.Y;
            auto z2 = rotation.Z + rotation.Z;

            auto wx2 = rotation.W * x2;
            auto wy2 = rotation.W * y2;
            auto wz2 = rotation.W * z2;
            auto xx2 = rotation.X * x2;
            auto xy2 = rotation.X * y2;
            auto xz2 = rotation.X * z2;
            auto yy2 = rotation.Y * y2;
            auto yz2 = rotation.Y * z2;
            auto zz2 = rotation.Z * z2;

            return Vector4<T>(
                value.X * (1 - yy2 - zz2) + value.Y * (xy2 - wz2) + value.Z * (xz2 + wy2),
                value.X * (xy2 + wz2) + value.Y * (1 - xx2 - zz2) + value.Z * (yz2 - wx2),
                value.X * (xz2 - wy2) + value.Y * (yz2 + wx2) + value.Z * (1 - xx2 - yy2),
                value.W);
        }
        inline static Plane<T> transform(const Plane<T>& p, const Quaternion<T>& rotation) {
            // Compute rotation matrix.
            auto x2 = rotation.X + rotation.X;
            auto y2 = rotation.Y + rotation.Y;
            auto z2 = rotation.Z + rotation.Z;

            auto wx2 = rotation.W * x2;
            auto wy2 = rotation.W * y2;
            auto wz2 = rotation.W * z2;
            auto xx2 = rotation.X * x2;
            auto xy2 = rotation.X * y2;
            auto xz2 = rotation.X * z2;
            auto yy2 = rotation.Y * y2;
            auto yz2 = rotation.Y * z2;
            auto zz2 = rotation.Z * z2;

            auto m11 = 1 - yy2 - zz2;
            auto m21 = xy2 - wz2;
            auto m31 = xz2 + wy2;

            auto m12 = xy2 + wz2;
            auto m22 = 1 - xx2 - zz2;
            auto m32 = yz2 - wx2;

            auto m13 = xz2 - wy2;
            auto m23 = yz2 + wx2;
            auto m33 = 1 - xx2 - yy2;

            auto x = p.Normal.X, y = p.Normal.Y, z = p.Normal.Z;

            return Plane<T>(
                x * m11 + y * m21 + z * m31,
                x * m12 + y * m22 + z * m32,
                x * m13 + y * m23 + z * m33,
                p.D);
        };

        inline T dot(const Quaternion<T>& q) const { return X * q.X + Y * q.Y + Z * q.Z + W * q.W; }
        inline static T dot(const Quaternion<T>& q1, const Quaternion<T>& q2) { return q1.dot(q2); }
        inline Quaternion<T> slerp(const Quaternion<T>& q2, T t) const {
            const T epsilon = 1e-6f;
            auto cosOmega = X * q2.X + Y * q2.Y + Z * q2.Z + W * q2.W;
            bool flip = false;
            if (cosOmega < 0) {
                flip = true;
                cosOmega = -cosOmega;
            }
            T s1, s2;
            if (cosOmega > (1 - epsilon)) {
                // Too close, do straight linear interpolation.
                s1 = 1 - t;
                s2 = (flip) ? -t : t;
            }
            else {
                auto omega = std::acos(cosOmega);
                auto invSinOmega = 1 / std::sin(omega);

                s1 = std::sin((1 - t) * omega) * invSinOmega;
                s2 = (flip)
                    ? -std::sin(t * omega) * invSinOmega
                    : std::sin(t * omega) * invSinOmega;
            }
            return (*this) * s1 + q2 * s2;
        }
        inline static Quaternion<T> slerp(const Quaternion<T>& q1, const Quaternion<T>& q2, T t) { return q1.slerp(q2, t); }
        inline Quaternion<T> lerp(const Quaternion<T>& q2, T t) const {
            return (dot(q2) >= 0
                ? ((*this) * (1 - t) + q2 * t)
                : ((*this) * (1 - t) - q2 * t)).normalize();
        }
        inline static Quaternion<T> lerp(const Quaternion<T>& q1, const Quaternion<T>& q2, T t) { return q1.lerp(q2, t); }
        inline Quaternion<T> concatenate(const Quaternion<T>& q) const {
            // Concatenate rotation is actually q2 * q1 instead of q1 * q2.
            // So that's why value2 goes q1 and value1 goes q2.
            auto q1x = q.X;
            auto q1y = q.Y;
            auto q1z = q.Z;
            auto q1w = q.W;

            auto q2x = X;
            auto q2y = Y;
            auto q2z = Z;
            auto q2w = W;

            // cross(av, bv)
            auto cx = q1y * q2z - q1z * q2y;
            auto cy = q1z * q2x - q1x * q2z;
            auto cz = q1x * q2y - q1y * q2x;

            auto dot = q1x * q2x + q1y * q2y + q1z * q2z;

            return Quaternion<T>(
                q1x * q2w + q2x * q1w + cx,
                q1y * q2w + q2y * q1w + cy,
                q1z * q2w + q2z * q1w + cz,
                q1w * q2w - dot);
        }
        inline static Quaternion<T> concatenate(const Quaternion<T>& q1, const Quaternion<T>& q2) { return q1.concatenate(q2); }
        inline bool isIdentity() const { return X == 0 && Y == 0 && Z == 0 && W == 1; }
        inline T lengthSquared() const { return X * X + Y * Y + Z * Z + W * W; }
        inline T length() const { return std::sqrt(lengthSquared()); }
        inline Quaternion<T> normalize() const { auto l = (1 / length()); return Quaternion<T>(X * l, Y * l, Z * l, W * l); }
        inline Quaternion<T> conjugate() const { return Quaternion<T>(-X, -Y, -Z, W); }
        inline Quaternion<T> inverse() const { return conjugate() * (1 / lengthSquared()); }
        inline Vector3<T> toEulerAngles() const {
            auto x = std::atan2(-2 * (Y * Z - W * X), W * W - X * X - Y * Y + Z * Z);
            auto y = std::asin(2 * (X * Z + W * Y));
            auto z = std::atan2(-2 * (X * Y - W * Z), W * W + X * X - Y * Y - Z * Z);
            return Vector3<T>(x, y, z);
        }
        inline HorizontalCoordinate<T> sphericalAngle(const Vector3<T>& forwardVector) const {
            auto newForward = transform(forwardVector, *this);
            auto forwardXY = Vector3<T>(newForward.X, newForward.Y, 0).normalize();
            auto angle = std::acos(forwardXY.Y);
            auto azimuth = forwardXY.X < 0 ? angle : -angle;
            auto inclination = -std::acos(newForward.Z) + constants::halfPi;
            return HorizontalCoordinate<T>(azimuth, inclination);
        }
        inline HorizontalCoordinate<T> sphericalAngle() const { return sphericalAngle(Vector3<T>::unitY()); }

        inline Quaternion<T> setX(T x) const { return Quaternion<T>(x, Y, Z, W); }
        inline Quaternion<T> setY(T y) const { return Quaternion<T>(X, y, Z, W); }
        inline Quaternion<T> setZ(T z) const { return Quaternion<T>(X, Y, z, W); }
        inline Quaternion<T> setW(T w) const { return Quaternion<T>(X, Y, Z, w); }

        inline std::size_t hash() const { return hash::combine(X, Y, Z, W); }
        inline bool almostEquals(const Quaternion<T>& x, float tolerance = constants::tolerance) const {
            return std::fabs(X - x.X) < tolerance && std::fabs(Y - x.Y) < tolerance && std::fabs(Z - x.Z) < tolerance && std::fabs(W - x.W) < tolerance;
        }

        inline friend Quaternion<T> operator -(const Quaternion<T>& q) { return Quaternion<T>(-q.X, -q.Y, -q.Z, -q.W); }
        inline friend Quaternion<T> operator -(const Quaternion<T>& value1, const Quaternion<T>& value2) { return Quaternion<T>(value1.X - value2.X, value1.Y - value2.Y, value1.Z - value2.Z, value1.W - value2.W); }

        inline friend Quaternion<T> operator *(const Quaternion<T>& value1, const Quaternion<T>& value2) {
            // 9 muls, 27 adds
            auto tmp_00 = (value1.Z - value1.Y) * (value2.Y - value2.Z);
            auto tmp_01 = (value1.W + value1.X) * (value2.W + value2.X);
            auto tmp_02 = (value1.W - value1.X) * (value2.Y + value2.Z);
            auto tmp_03 = (value1.Y + value1.Z) * (value2.W - value2.X);
            auto tmp_04 = (value1.Z - value1.X) * (value2.X - value2.Y);
            auto tmp_05 = (value1.Z + value1.X) * (value2.X + value2.Y);
            auto tmp_06 = (value1.W + value1.Y) * (value2.W - value2.Z);
            auto tmp_07 = (value1.W - value1.Y) * (value2.W + value2.Z);
            auto tmp_08 = tmp_05 + tmp_06 + tmp_07;
            auto tmp_09 = (tmp_04 + tmp_08) * 0.5;

            return Quaternion<T>(
                tmp_01 + tmp_09 - tmp_08,
                tmp_02 + tmp_09 - tmp_07,
                tmp_03 + tmp_09 - tmp_06,
                tmp_00 + tmp_09 - tmp_05);
        }
        inline friend Quaternion<T> operator *(const Quaternion<T>& q, T v) { return Quaternion<T>(q.X * v, q.Y * v, q.Z * v, q.W * v); }

        inline friend Quaternion<T> operator +(const Quaternion<T>& value1, const Quaternion<T>& value2) { return Quaternion<T>(value1.X + value2.X, value1.Y + value2.Y, value1.Z + value2.Z, value1.W + value2.W); }
        inline friend Quaternion<T> operator /(const Quaternion<T>& value1, const Quaternion<T>& value2) { return value1 * value2.inverse(); }

        inline friend bool operator==(const Quaternion<T>& o, const Quaternion<T>& other) { return o.X == other.X && o.Y == other.Y && o.Z == other.Z && o.W == other.W; }
        inline friend bool operator!=(const Quaternion<T>& o, const Quaternion<T>& other) { return !(o == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const Quaternion<T>& v) { return (out << "Quaternion<T>(X = " << v.X << ", Y = " << v.Y << ", Z = " << v.Z << ", W = " << v.W << ")"); }
    };

#define FQuaternion Quaternion<float>
#define DQuaternion Quaternion<double>

    template <typename T = float>
    struct Transform final {
        Vector3<T> Position;
        Quaternion<T> Orientation;

        const Transform(const Vector3<T>& position, const Quaternion<T>& orientation) : Position(position), Orientation(orientation) {}

        inline static const Transform<T> zero() { return Transform<T>(Vector3<T>::zero(), Quaternion<T>::zero()); }
        inline static const Transform<T> minValue() { return Transform<T>(Vector3<T>::minValue(), Quaternion<T>::minValue()); }
        inline static const Transform<T> maxValue() { return Transform<T>(Vector3<T>::maxValue(), Quaternion<T>::maxValue()); }
        inline static const Transform<T> identity() { return Transform<T>(Vector3<T>::zero(), Quaternion<T>::identity()); }

        inline Transform<T> setPosition(const Vector3<T>& x) const { return Transform<T>(x, Orientation); }
        inline Transform<T> seOrientation(const Quaternion<T>& x) const { return Transform<T>(Position, x); }

        inline std::size_t hash() const { return hash::combineValues(Position.hash(), Orientation.hash()); }
        inline bool almostEquals(const Transform<T>& x, float tolerance = constants::tolerance) const {
            return Position.almostEquals(x.Position, tolerance) && Orientation.almostEquals(x.Orientation, tolerance);
        }

        inline friend bool operator==(const Transform<T>& v, const Transform<T>& other) { return v.Position == other.Position && v.Orientation == other.Orientation; }
        inline friend bool operator!=(const Transform<T>& v, const Transform<T>& other) { return !(v == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const Transform<T>& v) { return (out << "Transform<T>(Position = " << v.Position << ", Orientation = " << v.Orientation << ")"); }
    };

#define FTransform Transform<float>
#define DTransform Transform<double>

    template <typename T = float>
    struct Quad final : Points<Vector3<T>>, Mappable<Quad<T>, Vector3<T>> {
        Vector3<T> A;
        Vector3<T> B;
        Vector3<T> C;
        Vector3<T> D;

        const Quad(const Vector3<T>& a, const Vector3<T>& b, const Vector3<T>& c, const Vector3<T>& d) : A(a), B(b), C(c), D(d) {}

        inline static const Quad<T> zero() { return Quad<T>(Vector3<T>::zero(), Vector3<T>::zero(), Vector3<T>::zero(), Vector3<T>::zero()); }
        inline static const Quad<T> minValue() { return Quad<T>(Vector3<T>::minValue(), Vector3<T>::minValue(), Vector3<T>::minValue(), Vector3<T>::minValue()); }
        inline static const Quad<T> maxValue() { return Quad<T>(Vector3<T>::maxValue(), Vector3<T>::maxValue(), Vector3<T>::maxValue(), Vector3<T>::maxValue()); }

        inline Quad<T> setA(Vector3<T> x) const { return Quad<T>(x, B, C, D); }
        inline Quad<T> setB(Vector3<T> x) const { return Quad<T>(A, x, C, D); }
        inline Quad<T> setC(Vector3<T> x) const { return Quad<T>(A, B, x, D); }
        inline Quad<T> setD(Vector3<T> x) const { return Quad<T>(A, B, C, x); }

        std::size_t numPoints() const override { return 4; }
        Vector3<T> getPoint(std::size_t n) const override { return  n == 0 ? A : n == 1 ? B : n == 2 ? C : D; }
        Quad<T> map(std::function<Vector3<T>(const Vector3<T>&)> f) const override { return Quad<T>(f(A), f(B), f(C), f(D)); }

        inline std::size_t hash() const { return hash::combineValues(A.hash(), B.hash(), C.hash(), D.hash()); }
        inline bool almostEquals(const Quad<T>& x, float tolerance = constants::tolerance) const {
            return A.almostEquals(x.A, tolerance)
                && B.almostEquals(x.B, tolerance)
                && C.almostEquals(x.C, tolerance)
                && D.almostEquals(x.D, tolerance);
        }

        inline friend bool operator==(const Quad<T>& q, const Quad<T>& x) { return q.A == x.A && q.B == x.B && q.C == x.C && q.D == x.D; }
        inline friend bool operator!=(const Quad<T>& q, const Quad<T>& other) { return !(q == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const Quad<T>& v) {
            return (out << "Quad<T>(A = " << v.A << ", B = " << v.B << ", C = " << v.C << ", D = " << v.D << ")");
        }
    };

#define FQuad Quad<float>
#define DQuad Quad<double>

    template <typename T = float>
    struct Quad2D final {
        Vector2<T> A;
        Vector2<T> B;
        Vector2<T> C;
        Vector2<T> D;

        const Quad2D(const Vector2<T>& a, const Vector2<T>& b, const Vector2<T>& c, const Vector2<T>& d) : A(a), B(b), C(c), D(d) {}

        inline static const Quad2D zero() { return Quad2D(Vector2<T>::zero(), Vector2<T>::zero(), Vector2<T>::zero(), Vector2<T>::zero()); }
        inline static const Quad2D minValue() { return Quad2D(Vector2<T>::minValue(), Vector2<T>::minValue(), Vector2<T>::minValue(), Vector2<T>::minValue()); }
        inline static const Quad2D maxValue() { return Quad2D(Vector2<T>::maxValue(), Vector2<T>::maxValue(), Vector2<T>::maxValue(), Vector2<T>::maxValue()); }

        inline Quad2D setA(Vector2<T> x) const { return Quad2D(x, B, C, D); }
        inline Quad2D setB(Vector2<T> x) const { return Quad2D(A, x, C, D); }
        inline Quad2D setC(Vector2<T> x) const { return Quad2D(A, B, x, D); }
        inline Quad2D setD(Vector2<T> x) const { return Quad2D(A, B, C, x); }

        inline std::size_t hash() const { return hash::combine(A.hash(), B.hash(), C.hash(), D.hash()); }
        inline bool almostEquals(const Quad2D& x, float tolerance = constants::tolerance) const {
            return A.almostEquals(x.A, tolerance)
                && B.almostEquals(x.B, tolerance)
                && C.almostEquals(x.C, tolerance)
                && D.almostEquals(x.D, tolerance);
        }

        inline friend bool operator==(const Quad2D& o, const Quad2D& x) { return o.A == x.A && o.B == x.B && o.C == x.C && o.D == x.D; }
        inline friend bool operator!=(const Quad2D& o, const Quad2D& other) { return !(o == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const Quad2D& v) {
            return (out << "Quad2D(A = " << v.A << ", B = " << v.B << ", C = " << v.C << ", D = " << v.D << ")");
        }
    };

#define FQuad2D Quad2D<float>
#define DQuad2D Quad2D<double>

    template <typename T = float>
    struct Sphere final {
        Vector3<T> Center;
        T Radius;

        const Sphere(const Vector3<T>& center, T radius) : Center(center), Radius(radius) {}
        const Sphere(const std::vector<Vector3<T>>& p) {
            if (p.empty()) { throw std::exception("Points Array is empty"); }
            // From "Real-Time Collision Detection" (Page 89)
            auto minx = Vector3<T>(std::numeric_limits<T>::max());
            auto maxx = -minx;
            auto miny = minx;
            auto maxy = -minx;
            auto minz = minx;
            auto maxz = -minx;
            // Find the most extreme points along the principle axis.
            auto numPoints = 0;
            for (const auto& pt : p) {
                ++numPoints;
                if (pt.X < minx.X)
                    minx = pt;
                if (pt.X > maxx.X)
                    maxx = pt;
                if (pt.Y < miny.Y)
                    miny = pt;
                if (pt.Y > maxy.Y)
                    maxy = pt;
                if (pt.Z < minz.Z)
                    minz = pt;
                if (pt.Z > maxz.Z)
                    maxz = pt;
            }
            if (numPoints == 0) { throw std::exception("You should have at least one point in points."); }
            auto sqDistX = maxx.distanceSquared(minx);
            auto sqDistY = maxy.distanceSquared(miny);
            auto sqDistZ = maxz.distanceSquared(minz);
            // Pick the pair of most distant points.
            auto min = minx;
            auto max = maxx;
            if (sqDistY > sqDistX && sqDistY > sqDistZ) {
                max = maxy;
                min = miny;
            }
            if (sqDistZ > sqDistX && sqDistZ > sqDistY) {
                max = maxz;
                min = minz;
            }
            auto center = (min + max) * 0.5;
            auto radius = max.distance(center);
            // Test every point and expand the sphere.
            // The current bounding sphere is just a good approximation and may not enclose all points.            
            // From: Mathematics for 3D Game Programming and Computer Graphics, Eric Lengyel, Third Edition.
            // Page 218
            auto sqRadius = radius * radius;
            for (const auto& pt : p) {
                auto diff = pt - center;
                auto sqDist = diff.lengthSquared();
                if (sqDist > sqRadius) {
                    auto distance = std::sqrt(sqDist); // equal to diff.Length();
                    auto direction = diff / distance;
                    auto G = center - radius * direction;
                    center = (G + pt) / 2;
                    radius = pt.distance(center);
                    sqRadius = radius * radius;
                }
            }
            Center = center;
            Radius = radius;
        };
        const Sphere(const std::initializer_list<Vector3<T>>& points) : Sphere<T>(std::vector<Vector3<T>>(points.begin(), points.end())) {}

        inline static const Sphere<T> zero() { return Sphere<T>(Vector3<T>::zero(), 0); }
        inline static const Sphere<T> minValue() { return Sphere<T>(Vector3<T>::minValue(), std::numeric_limits<T>::min()); }
        inline static const Sphere<T> maxValue() { return Sphere<T>(Vector3<T>::maxValue(), std::numeric_limits<T>::max()); }

        inline Sphere<T> merge(const Sphere<T>& additional) const {
            auto ocenterToaCenter = additional.Center - Center;
            auto distance = ocenterToaCenter.length();
            if (distance <= Radius + additional.Radius) {
                if (distance <= Radius - additional.Radius) { return *this; }
                if (distance <= additional.Radius - Radius) { return additional; }
            }
            //else find center of new sphere and radius
            auto leftRadius = std::max(Radius - distance, additional.Radius);
            auto Rightradius = std::max(Radius + distance, additional.Radius);
            ocenterToaCenter = ocenterToaCenter + ((leftRadius - Rightradius) / (2 * ocenterToaCenter.length()) * ocenterToaCenter);
            return Sphere<T>(Center + ocenterToaCenter, (leftRadius + Rightradius) / 2);
        }
        inline PlaneIntersectionType intersects(const Plane<T>& plane) const {
            auto distance = plane.Normal.dot(Center);
            distance += plane.D;
            if (distance > Radius) { return PlaneIntersectionType::front; }
            if (distance < -Radius) { return PlaneIntersectionType::back; }
            return PlaneIntersectionType::intersecting;
        };
        inline ContainmentType contains(const Sphere<T>& sphere) const {
            auto sqDistance = sphere.Center.distanceSquared(Center);
            if (sqDistance > (sphere.Radius + Radius) * (sphere.Radius + Radius)) { return ContainmentType::disjoint; }
            if (sqDistance <= (Radius - sphere.Radius) * (Radius - sphere.Radius)) { return ContainmentType::contains; }
            return ContainmentType::intersects;
        }
        inline ContainmentType contains(const Vector3<T>& point) const {
            auto sqRadius = Radius * Radius;
            auto sqDistance = point.distanceSquared(Center);
            if (sqDistance > sqRadius) { return ContainmentType::disjoint; }
            else if (sqDistance < sqRadius) { return ContainmentType::contains; }
            else { return ContainmentType::intersects; }
        }
        inline bool intersects(const Sphere<T>& sphere) const {
            auto sqDistance = sphere.Center.distanceSquared(Center);
            return !(sqDistance > (sphere.Radius + Radius) * (sphere.Radius + Radius));
        }
        inline Sphere<T> translate(const Vector3<T>& offset) const { return Sphere<T>(Center + offset, Radius); }
        inline T distance(const Vector3<T>& point) const { return std::max(Center.distance(point) - Radius, (T)0); }
        inline T distance(const Sphere<T>& other) const { return std::max(Center.distance(other.Center) - Radius - other.Radius, (T)0); }

        inline Sphere<T> setCenter(Vector3<T> x) const { return Sphere<T>(x, Radius); }
        inline Sphere<T> setRadius(T x) const { return Sphere<T>(Center, x); }

        inline std::size_t hash() const { return hash::combine((T)Center.hash(), Radius); }
        inline bool almostEquals(const Sphere<T>& x, float tolerance = constants::tolerance) const {
            return Center.almostEquals(x.Center, tolerance) && std::fabs(Radius - x.Radius) < tolerance;
        }

        inline friend bool operator==(const Sphere<T>& s, const Sphere<T>& other) { return s.Center == other.Center && s.Radius == other.Radius; }
        inline friend bool operator!=(const Sphere<T>& s, const Sphere<T>& other) { return !(s == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const Sphere<T>& v) { return (out << "Sphere<T>(Center = " << v.Center << ", Radius = " << v.Radius << ")"); }
    };

#define FSphere Sphere<float>
#define DSphere Sphere<double>

    template <typename T = float>
    struct AABox final {
        // CCW
        static constexpr std::array<int, 4> topIndices = { 0, 1, 2, 3, };
        static constexpr std::array<int, 4> bottomIndices = { 7, 6, 5, 4 };
        static constexpr std::array<int, 4> frontIndices = { 4, 5, 1, 0 };
        static constexpr std::array<int, 4> rightIndices = { 5, 6, 2, 1 };
        static constexpr std::array<int, 4> backIndices = { 6, 7, 3, 2 };
        static constexpr std::array<int, 4> leftIndices = { 7, 4, 0, 3 };
        const std::size_t count = 2;

        Vector3<T> Min;
        Vector3<T> Max;

        const AABox(const Vector3<T>& min, const Vector3<T>& max) : Min(min), Max(max) {}
        const AABox(const Vector3<T>& v) : Min(v), Max(v) {}
        const AABox(const Sphere<T>& sphere) : AABox<T>(sphere.Center - Vector3<T>(sphere.Radius), sphere.Center + Vector3<T>(sphere.Radius)) {}
        const AABox(const std::vector<Vector3<T>>& p) {
            Vector3<T> minVec = Vector3<T>::maxValue();
            Vector3<T> maxVec = Vector3<T>::minValue();
            if (!p.empty()) {
                for (const auto& pt : p) {
                    minVec = minVec.min(pt);
                    maxVec = maxVec.max(pt);
                }
            }
            Min = minVec;
            Max = maxVec;
        }
        const AABox(const std::initializer_list<Vector3<T>>& points) : AABox<T>(std::vector<Vector3<T>>(points.begin(), points.end())) {}

        inline static const AABox<T> zero() { return AABox<T>(Vector3<T>::zero()); }
        inline static const AABox<T> minValue() { return AABox<T>(Vector3<T>::minValue()); }
        inline static const AABox<T> maxValue() { return AABox<T>(Vector3<T>::maxValue()); }
        inline static const AABox<T> empty() { return AABox<T>(Vector3<T>::maxValue()); }
        inline static const AABox<T> unitA() { return AABox<T>(Vector3<T>::zero(), Vector3<T>::one()); }

        inline static AABox<T> fromCenterAndExtent(const Vector3<T>& center, const Vector3<T>& extent) { return AABox<T>((center - extent / 2.0), (center + extent / 2.0)); }
        inline static std::vector<AABox<T>> aaBoxes(const std::vector<T>& m) {
            const int numFloats = 6;
            assert(m.size() % numFloats == 0);
            std::vector<AABox<T>> ret; // (m.size() / numFloats);
            for (int i = 0; i < ret.size(); i++) {
                int i6 = i * numFloats;
                ret.push_back(AABox<T>(
                    Vector3<T>(m[i6 + 0], m[i6 + 1], m[i6 + 2]),
                    Vector3<T>(m[i6 + 3], m[i6 + 4], m[i6 + 5])
                ));
            }
            return ret;
        }

        inline Vector3<T> extent() const { return Max - Min; };
        inline Vector3<T> center() const { return (Max + Min) * 0.5; };
        inline T magnitudeSquared() const { return extent().magnitudeSquared(); }
        inline T magnitude() const { return std::sqrt(magnitudeSquared()); }
        inline bool isNaN() const { return Min.isnan() || Max.isnan(); }
        inline bool isInfinity() const { return Min.isinf() || Max.isinf(); }
        inline AABox<T> merge(const AABox<T>& other) const { return AABox<T>(Min.min(other.Min), Max.max(other.Max)); }
        inline AABox<T> intersection(const AABox<T>& other) const { return { Min.max(other.Min), Max.min(other.Max) }; }
        inline Vector3<T> centerBottom() const { return center().setZ(Min.Z); }
        inline std::array<Vector3<T>, 8> corners() const {
            return std::array<Vector3<T>, 8>{
                // Bottom (looking down)
                Vector3<T>(Min.X, Min.Y, Min.Z),
                    Vector3<T>(Max.X, Min.Y, Min.Z),
                    Vector3<T>(Max.X, Max.Y, Min.Z),
                    Vector3<T>(Min.X, Max.Y, Min.Z),
                    // Top (looking down)
                    Vector3<T>(Min.X, Min.Y, Max.Z),
                    Vector3<T>(Max.X, Min.Y, Max.Z),
                    Vector3<T>(Max.X, Max.Y, Max.Z),
                    Vector3<T>(Min.X, Max.Y, Max.Z)};
        }
        inline bool isValid() const { return Min.X <= Max.X && Min.Y <= Max.Y && Min.Z <= Max.Z; }
        inline bool isEmpty() const { return !isValid(); }
        inline T distance(const Vector3<T>& point) const { return Vector3<T>(0).max(Min - point).max(point - Max).length(); }
        inline T centerDistance(const Vector3<T>& point) const { return center().distance(point); }
        inline AABox<T> translate(const Vector3<T>& offset) const { return AABox<T>(Min + offset, Max + offset); }
        inline T distanceToOrigin() const { return distance(Vector3<T>(0)); }
        inline T centerDistanceToOrigin() const { return centerDistance(Vector3<T>(0)); }
        inline T volume() const { return isEmpty() ? 0 : extent().productComponents(); }
        inline T maxSide() const { return extent().maxComponent(); }
        inline T maxFaceArea() const { Vector3<T> ext = extent(); return ext.X > ext.Y ? ext.X * std::max(ext.Z, ext.Y) : ext.Y * std::max(ext.Z, ext.X); }
        inline T minSide() const { return extent().minComponent(); }
        inline T diagonal() const { return extent().length(); }
        inline ContainmentType contains(const AABox<T>& box) const {
            //test if all corner is in the same side of a face by just checking min and max
            if (box.Max.X < Min.X
                || box.Min.X > Max.X
                || box.Max.Y < Min.Y
                || box.Min.Y > Max.Y
                || box.Max.Z < Min.Z
                || box.Min.Z > Max.Z) {
                return ContainmentType::disjoint;
            }
            if (box.Min.X >= Min.X
                && box.Max.X <= Max.X
                && box.Min.Y >= Min.Y
                && box.Max.Y <= Max.Y
                && box.Min.Z >= Min.Z
                && box.Max.Z <= Max.Z) {
                return ContainmentType::contains;
            }
            return ContainmentType::intersects;
        }
        inline static ContainmentType contains(const Sphere<T>& s, const AABox<T>& box) {
            bool inside = true;
            for (const auto& corner : box.corners()) {
                if (contains(s, corner) == ContainmentType::disjoint) {
                    inside = false;
                    break;
                }
            }
            if (inside) { return ContainmentType::contains; }
            //check if the distance from sphere center to cube face < radius
            double dmin = 0;
            if (s.Center.X < box.Min.X)
                dmin += (s.Center.X - box.Min.X) * (s.Center.X - box.Min.X);
            else if (s.Center.X > box.Max.X)
                dmin += (s.Center.X - box.Max.X) * (s.Center.X - box.Max.X);
            if (s.Center.Y < box.Min.Y)
                dmin += (s.Center.Y - box.Min.Y) * (s.Center.Y - box.Min.Y);
            else if (s.Center.Y > box.Max.Y)
                dmin += (s.Center.Y - box.Max.Y) * (s.Center.Y - box.Max.Y);
            if (s.Center.Z < box.Min.Z)
                dmin += (s.Center.Z - box.Min.Z) * (s.Center.Z - box.Min.Z);
            else if (s.Center.Z > box.Max.Z)
                dmin += (s.Center.Z - box.Max.Z) * (s.Center.Z - box.Max.Z);
            if (dmin <= s.Radius * s.Radius)
                return ContainmentType::intersects;
            return ContainmentType::disjoint;
        }
        inline ContainmentType contains(const Sphere<T>& sphere) const {
            if (sphere.Center.X - Min.X >= sphere.Radius
                && sphere.Center.Y - Min.Y >= sphere.Radius
                && sphere.Center.Z - Min.Z >= sphere.Radius
                && Max.X - sphere.Center.X >= sphere.Radius
                && Max.Y - sphere.Center.Y >= sphere.Radius
                && Max.Z - sphere.Center.Z >= sphere.Radius) {
                return ContainmentType::contains;
            }
            T dmin = 0;
            T e = sphere.Center.X - Min.X;
            if (e < 0)
            {
                if (e < -sphere.Radius) {
                    return ContainmentType::disjoint;
                }
                dmin += e * e;
            }
            else
            {
                e = sphere.Center.X - Max.X;
                if (e > 0) {
                    if (e > sphere.Radius) {
                        return ContainmentType::disjoint;
                    }
                    dmin += e * e;
                }
            }
            e = sphere.Center.Y - Min.Y;
            if (e < 0) {
                if (e < -sphere.Radius)
                    return ContainmentType::disjoint;
                dmin += e * e;
            }
            else
            {
                e = sphere.Center.Y - Max.Y;
                if (e > 0) {
                    if (e > sphere.Radius)
                        return ContainmentType::disjoint;
                    dmin += e * e;
                }
            }
            e = sphere.Center.Z - Min.Z;
            if (e < 0) {
                if (e < -sphere.Radius)
                    return ContainmentType::disjoint;
                dmin += e * e;
            }
            else {
                e = sphere.Center.Z - Max.Z;
                if (e > 0) {
                    if (e > sphere.Radius)
                        return ContainmentType::disjoint;
                    dmin += e * e;
                }
            }
            if (dmin <= sphere.Radius * sphere.Radius)
                return ContainmentType::intersects;
            return ContainmentType::disjoint;
        }
        inline bool contains(const Vector3<T>& point) const { return !(point.X < Min.X || point.X > Max.X || point.Y < Min.Y || point.Y > Max.Y || point.Z < Min.Z || point.Z > Max.Z); }
        inline bool intersects(const AABox<T>& box) const {
            if ((Max.X >= box.Min.X) && (Min.X <= box.Max.X)) {
                if ((Max.Y < box.Min.Y) || (Min.Y > box.Max.Y)) { return false; }
                return ((Max.Z >= box.Min.Z) && (Min.Z <= box.Max.Z));
            }
            return false;
        }
        inline bool intersects(const Sphere<T>& sphere) const {
            if (sphere.Center.X - Min.X > sphere.Radius
                && sphere.Center.Y - Min.Y > sphere.Radius
                && sphere.Center.Z - Min.Z > sphere.Radius
                && Max.X - sphere.Center.X > sphere.Radius
                && Max.Y - sphere.Center.Y > sphere.Radius
                && Max.Z - sphere.Center.Z > sphere.Radius) {
                return true;
            }
            T dmin = 0;
            if (sphere.Center.X - Min.X <= sphere.Radius)
                dmin += (sphere.Center.X - Min.X) * (sphere.Center.X - Min.X);
            else if (Max.X - sphere.Center.X <= sphere.Radius)
                dmin += (sphere.Center.X - Max.X) * (sphere.Center.X - Max.X);

            if (sphere.Center.Y - Min.Y <= sphere.Radius)
                dmin += (sphere.Center.Y - Min.Y) * (sphere.Center.Y - Min.Y);
            else if (Max.Y - sphere.Center.Y <= sphere.Radius)
                dmin += (sphere.Center.Y - Max.Y) * (sphere.Center.Y - Max.Y);

            if (sphere.Center.Z - Min.Z <= sphere.Radius)
                dmin += (sphere.Center.Z - Min.Z) * (sphere.Center.Z - Min.Z);
            else if (Max.Z - sphere.Center.Z <= sphere.Radius)
                dmin += (sphere.Center.Z - Max.Z) * (sphere.Center.Z - Max.Z);

            if (dmin <= sphere.Radius * sphere.Radius)
                return true;
            return false;
        }
        inline PlaneIntersectionType intersects(const Plane<T>& plane) const {
            // See http://zach.in.tu-clausthal.de/teaching/cg_literatur/lighthouse3d_view_frustum_culling/index.html
            T ax, ay, az, bx, by, bz;
            if (plane.Normal.X >= 0) { ax = Max.X; bx = Min.X; }
            else { ax = Min.X; bx = Max.X; }

            if (plane.Normal.Y >= 0) { ay = Max.Y; by = Min.Y; }
            else { ay = Min.Y; by = Max.Y; }

            if (plane.Normal.Z >= 0) { az = Max.Z; bz = Min.Z; }
            else { az = Min.Z; bz = Max.Z; }

            // Inline Vector3<T>.Dot(plane.Normal, negativeVertex) + plane.D;
            T distance = plane.Normal.X * bx + plane.Normal.Y * by + plane.Normal.Z * bz + plane.D;
            if (distance > 0)
                return PlaneIntersectionType::front;
            // Inline Vector3<T>.Dot(plane.Normal, positiveVertex) + plane.D;
            distance = plane.Normal.X * ax + plane.Normal.Y * ay + plane.Normal.Z * az + plane.D;
            if (distance < 0)
                return PlaneIntersectionType::back;
            return PlaneIntersectionType::intersecting;
        }
        inline Vector3<T> relativePosition(const Vector3<T>& v) const { return (v - Min) / (Max - Min); }
        inline AABox<T> recenter() const { return translate(-center()); }
        inline AABox<T> scale(T scale) const { auto rec = recenter(); return AABox<T>(rec.Min * scale, rec.Max * scale).translate(center()); }
        inline std::array<Vector3<T>, 6> faceCenters() const {
            auto c = corners();
            return std::array<Vector3<T>, 6>
            {
                c[frontIndices[0]].average(c[frontIndices[2]]),
                    c[rightIndices[0]].average(c[rightIndices[2]]),
                    c[backIndices[0]].average(c[backIndices[2]]),
                    c[leftIndices[0]].average(c[leftIndices[2]]),
                    c[topIndices[0]].average(c[topIndices[2]]),
                    c[bottomIndices[0]].average(c[bottomIndices[2]]),
            };
        }
        inline std::array<Vector3<T>, 14> getCornersAndFaceCenters() const {
            std::array<Vector3<T>, 8> c = corners();
            std::array<Vector3<T>, 6> f = faceCenters();

            std::array<Vector3<T>, 14> result;
            std::copy(c.begin(), c.end(), result.begin());
            std::copy(f.begin(), f.end(), result.begin() + 8);
            return result;
        }
        inline Vector3<T> lerp(const Vector3<T>& v) const { return Min + extent() * v; }
        inline Sphere<T> sphere() const { auto c = center(); return Sphere<T>(c, c.distance(Max)); }

        inline AABox<T> setCenter(const Vector3<T>& c) { auto ext = extent(); return AABox<T>((c - ext / 2.0), (c + ext / 2.0)); }
        inline AABox<T> setExtent(const Vector3<T>& ext) { auto c = center(); return AABox<T>((c - ext / 2.0), (c + ext / 2.0)); }
        inline AABox<T> setMin(Vector3<T> x) const { return AABox<T>(x, Max); }
        inline AABox<T> setMax(Vector3<T> x) const { return AABox<T>(Min, x); }

        inline std::size_t hash() const { return hash::combineValues(Min.hash(), Max.hash()); }
        inline bool almostEquals(const AABox<T>& x, float tolerance = constants::tolerance) const {
            return Min.almostEquals(x.Min, tolerance) && Max.almostEquals(x.Max, tolerance);
        }
        inline int compare(const AABox<T>& x) const { return std::signbit(magnitudeSquared() - x.magnitudeSquared()); }

        inline Vector3<T> operator[](int n) const { return (n == 0) ? Min : Max; }
        inline friend bool operator==(const AABox<T>& box, const AABox<T>& other) { return box.Min == other.Min && box.Max == other.Max; }
        inline friend bool operator!=(const AABox<T>& box, const AABox<T>& other) { return !(box == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const AABox<T>& v) { return (out << "AABox<T>(Min = " << v.Min << ", Max = " << v.Max << ")"); }

        inline friend AABox<T> operator+(const AABox<T>& value1, const AABox<T>& value2) { return value1.merge(value2); }
        inline friend AABox<T> operator-(const AABox<T>& value1, const AABox<T>& value2) { return value1.intersection(value2); }

        inline friend bool operator<(const AABox<T>& x0, const AABox<T>& x1) { return x0.compare(x1) < 0; }
        inline friend bool operator<=(const AABox<T>& x0, const AABox<T>& x1) { return x0.compare(x1) <= 0; }
        inline friend bool operator>(const AABox<T>& x0, const AABox<T>& x1) { return x0.compare(x1) > 0; }
        inline friend bool operator>=(const AABox<T>& x0, const AABox<T>& x1) { return x0.compare(x1) >= 0; }
    };

#define FAABox AABox<float>
#define DAABox AABox<double>

    template <typename T = float>
    struct AABox2D final {
        // CCW
        static constexpr std::array<int, 4> indices = { 0, 1, 2, 3, };

        Vector2<T> Min;
        Vector2<T> Max;

        const AABox2D(const Vector2<T>& min, const Vector2<T>& max) : Min(min), Max(max) {}
        const AABox2D(const Vector2<T>& v) : Min(v), Max(v) {}
        const AABox2D(const std::vector<Vector2<T>>& p) {
            Vector2<T> minVec = Vector2<T>::maxValue();
            Vector2<T> maxVec = Vector2<T>::minValue();
            if (!p.empty()) {
                for (const auto& pt : p) {
                    minVec = minVec.min(pt);
                    maxVec = maxVec.max(pt);
                }
            }
            Min = minVec;
            Max = maxVec;
        }
        const AABox2D(const std::initializer_list<Vector2<T>>& points) : AABox2D<T>(std::vector<Vector2<T>>(points.begin(), points.end())) {}

        inline static const AABox2D<T> zero() { return AABox2D<T>(Vector2<T>::zero(), Vector2<T>::zero()); }
        inline static const AABox2D<T> minValue() { return  AABox2D<T>(Vector2<T>::minValue(), Vector2<T>::minValue()); }
        inline static const AABox2D<T> maxValue() { return  AABox2D<T>(Vector2<T>::maxValue(), Vector2<T>::maxValue()); }
        inline static const AABox2D<T> empty() { return  AABox2D<T>(Vector2<T>::maxValue(), Vector2<T>::minValue()); }
        inline static const AABox2D<T> unit() { return  AABox2D<T>(Vector2<T>::zero(), Vector2<T>::one()); }

        inline Vector2<T> extent() const { return Max - Min; };
        inline Vector2<T> center() const { return (Max + Min) * 0.5; };
        inline double magnitudeSquared() const { return extent().magnitudeSquared(); }
        inline double magnitude() const { return std::sqrt(magnitudeSquared()); }
        inline bool isnan() const { return Min.isnan() || Max.isnan(); }
        inline bool isinf() const { return Min.isinf() || Max.isinf(); }
        inline AABox2D<T> merge(const AABox2D<T>& other) const { return { Min.min(other.Min), Max.max(other.Max) }; }
        inline AABox2D<T> intersection(const AABox2D<T>& other) const { return { Min.max(other.Min), Max.min(other.Max) }; }
        inline Vector2<T> centerBottom() const { return center().setY(Min.Y); }
        inline std::array<Vector2<T>, 4> corners() const {
            return std::array<Vector2<T>, 4>{
                Vector2<T>(Min.X, Min.Y),
                    Vector2<T>(Max.X, Min.Y),
                    Vector2<T>(Max.X, Max.Y),
                    Vector2<T>(Min.X, Max.Y)};
        }
        inline bool isValid() const { return Min.X <= Max.X && Min.Y <= Max.Y; }
        inline bool isEmpty() const { return !isValid(); }
        inline T distance(const Vector2<T>& point) const { return Vector2<T>(0).max(Min - point).max(point - Max).length(); }
        inline T centerDistance(const Vector2<T>& point) const { return center().distance(point); }
        inline AABox2D<T> translate(const Vector2<T>& offset) const { return AABox2D<T>(Min + offset, Max + offset); }
        inline T distanceToOrigin() const { return distance(Vector2<T>(0)); }
        inline T centerDistanceToOrigin() const { return centerDistance(Vector2<T>(0)); }
        inline T area() const { return isEmpty() ? 0 : extent().productComponents(); }
        inline T maxSide() const { return extent().maxComponent(); }
        inline T minSide() const { return extent().minComponent(); }
        inline T diagonal() const { return extent().length(); }
        inline ContainmentType contains(const AABox2D<T>& box) const {
            //test if all corner is in the same side of a face by just checking min and max
            if (box.Max.X < Min.X
                || box.Min.X > Max.X
                || box.Max.Y < Min.Y
                || box.Min.Y > Max.Y) {
                return ContainmentType::disjoint;
            }
            if (box.Min.X >= Min.X
                && box.Max.X <= Max.X
                && box.Min.Y >= Min.Y
                && box.Max.Y <= Max.Y) {
                return ContainmentType::contains;
            }
            return ContainmentType::intersects;
        }
        inline bool contains(const Vector2<T>& point) const { return !(point.X < Min.X || point.X > Max.X || point.Y < Min.Y || point.Y > Max.Y); }
        inline bool intersects(const AABox2D<T>& box) const { return Min.X <= box.Max.X && Max.X >= box.Min.X && Min.Y <= box.Max.Y && Max.Y >= box.Min.Y; }
        inline Vector2<T> relativePosition(const Vector2<T>& v) const { return (v - Min) / (Max - Min); }
        inline AABox2D<T> recenter() const { return translate(-center()); }
        inline AABox2D<T> scale(T scale) const { auto rec = recenter(); return AABox2D<T>(rec.Min * scale, rec.Max * scale).translate(center()); }

        inline AABox2D<T> setMin(Vector2<T> x) const { return AABox2D<T>(x, Max); }
        inline AABox2D<T> setMax(Vector2<T> x) const { return AABox2D<T>(Min, x); }

        inline std::size_t hash() const { return hash::combineValues(Min.hash(), Max.hash()); }
        inline bool almostEquals(const AABox2D<T>& x, float tolerance = constants::tolerance) const { return Min.almostEquals(x.Min, tolerance) && Max.almostEquals(x.Max, tolerance); }
        inline int compare(const AABox2D<T>& x) const { return std::signbit(magnitudeSquared() - x.magnitudeSquared()); }

        inline Vector2<T> operator[](int n) const { return (n == 0) ? Min : Max; }
        inline friend bool operator==(const AABox2D<T>& value, const AABox2D<T>& other) { return value.Min == other.Min && value.Max == other.Max; }
        inline friend bool operator!=(const AABox2D<T>& value, const AABox2D<T>& other) { return !(value == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const AABox2D<T>& v) { return (out << "AABox2D<T>(Min = " << v.Min << ", Max = " << v.Max << ")"); }

        inline friend AABox2D<T> operator+(const AABox2D<T>& value1, const AABox2D<T>& value2) { return value1.merge(value2); }
        inline friend AABox2D<T> operator-(const AABox2D<T>& value1, const AABox2D<T>& value2) { return value1.intersection(value2); }

        inline friend bool operator<(const AABox2D<T>& x0, const AABox2D<T>& x1) { return x0.compare(x1) < 0; }
        inline friend bool operator<=(const AABox2D<T>& x0, const AABox2D<T>& x1) { return x0.compare(x1) <= 0; }
        inline friend bool operator>(const AABox2D<T>& x0, const AABox2D<T>& x1) { return x0.compare(x1) > 0; }
        inline friend bool operator>=(const AABox2D<T>& x0, const AABox2D<T>& x1) { return x0.compare(x1) >= 0; }
    };

#define FAABox2D AABox2D<float>
#define DAABox2D AABox2D<double>

    template <typename T = float>
    struct AABox4D final {
        Vector4<T> Min;
        Vector4<T> Max;

        const AABox4D(const Vector4<T>& min, const Vector4<T>& max) : Min(min), Max(max) {}

        inline static const AABox4D zero() { return AABox4D(Vector4<T>::zero(), Vector4<T>::zero()); }
        inline static const AABox4D minValue() { return AABox4D(Vector4<T>::minValue(), Vector4<T>::minValue()); }
        inline static const AABox4D maxValue() { return AABox4D(Vector4<T>::maxValue(), Vector4<T>::maxValue()); }
        inline static const AABox4D empty() { return AABox4D(Vector4<T>::maxValue(), Vector4<T>::minValue()); }

        inline AABox4D setMin(Vector4<T> x) const { return AABox4D(x, Max); }
        inline AABox4D setMax(Vector4<T> x) const { return AABox4D(Min, x); }

        inline std::size_t hash() const { return hash::combineValues(Min.hash(), Max.hash()); }
        inline bool almostEquals(const AABox4D& x, float tolerance = constants::tolerance) const {
            return Min.almostEquals(x.Min, tolerance)
                && Max.almostEquals(x.Max, tolerance);
        }
        inline Vector4<T> extent() const { return Max - Min; };
        inline Vector4<T> center() { return Min.average(Max); };
        inline double magnitudeSquared() const { return extent().magnitudeSquared(); }
        inline double magnitude() const { return std::sqrt(magnitudeSquared()); }
        inline bool isnan() const { return Min.isnan() || Max.isnan(); }
        inline bool isinf() const { return Min.isinf() || Max.isinf(); }
        inline int compare(const AABox4D& x) const { return std::signbit(magnitudeSquared() - x.magnitudeSquared()); }
        inline AABox4D merge(const AABox4D& other) const { return { Min.min(other.Min), Max.max(other.Max) }; }
        inline AABox4D intersection(const AABox4D& other) const { return { Min.max(other.Min), Max.min(other.Max) }; }

        inline friend bool operator==(const AABox4D& o, const AABox4D& other) { return o.Min == other.Min && o.Max == other.Max; }
        inline friend bool operator!=(const AABox4D& o, const AABox4D& other) { return !(o == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const AABox4D& v) { return (out << "AABox4D(Min = " << v.Min << ", Max = " << v.Max << ")"); }

        inline friend AABox4D operator+(const AABox4D& value1, const AABox4D& value2) { return value1.merge(value2); }
        inline friend AABox4D operator-(const AABox4D& value1, const AABox4D& value2) { return value1.intersection(value2); }

        inline friend bool operator<(const AABox4D& x0, const AABox4D& x1) { return x0.compare(x1) < 0; }
        inline friend bool operator<=(const AABox4D& x0, const AABox4D& x1) { return x0.compare(x1) <= 0; }
        inline friend bool operator>(const AABox4D& x0, const AABox4D& x1) { return x0.compare(x1) > 0; }
        inline friend bool operator>=(const AABox4D& x0, const AABox4D& x1) { return x0.compare(x1) >= 0; }
    };

#define FAABox4D AABox4D<float>
#define DAABox4D AABox4D<double>

    template <typename T = float>
    struct Line final : Points<Vector3<T>>, Mappable<Line<T>, Vector3<T>> {
        Vector3<T> A;
        Vector3<T> B;

        const Line(const Vector3<T>& a, const Vector3<T>& b) : A(a), B(b) {}

        inline static const Line<T> zero() { return Line<T>(Vector2<T>::zero(), Vector2<T>::zero()); }
        inline static const Line<T> minValue() { return Line<T>(Vector2<T>::minValue(), Vector2<T>::minValue()); }
        inline static const Line<T> maxValue() { return Line<T>(Vector2<T>::maxValue(), Vector2<T>::maxValue()); }

        inline Vector3<T> vector() const { return B - A; }
        inline T length() const { return A.distance(B); }
        inline T lengthSquared() const { return A.distanceSquared(B); }
        inline Vector3<T> midPoint() const { return A.average(B); }
        inline Line<T> normal() const { return Line<T>(A, A + vector().normalize()); }
        inline Line<T> inverse() const { return Line<T>(B, A); }
        inline Vector3<T> lerp(T amount) const { return A + (B - A) * amount; }
        inline Line<T> setLength(T length) const { return Line<T>(A, A + vector().along(length)); }

        std::size_t numPoints() const override { return 2; }
        Vector3<T> getPoint(std::size_t n) const override { return n == 0 ? A : B; }
        Line<T> map(std::function<Vector3<T>(const Vector3<T>&)> f) const override { return Line<T>(f(A), f(B)); }

        inline Line<T> setA(Vector3<T> x) const { return Line<T>(x, B); }
        inline Line<T> setB(Vector3<T> x) const { return Line<T>(A, x); }

        inline std::size_t hash() const { return hash::combineValues(A.hash(), B.hash()); }
        inline bool almostEquals(const Line<T>& x, float tolerance = constants::tolerance) const {
            return A.almostEquals(x.A, tolerance) && B.almostEquals(x.B, tolerance);
        }

        inline friend bool operator==(const Line<T>& v, const Line<T>& other) { return v.A == other.A && v.B == other.B; }
        inline friend bool operator!=(const Line<T>& v, const Line<T>& other) { return !(v == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const Line<T>& v) { return (out << "Line<T>(A = " << v.A << ", B = " << v.B << ")"); }
    };

#define FLine Line<float>
#define DLine Line<double>

    template <typename T = float>
    struct Line2D final {
        Vector2<T> A;
        Vector2<T> B;

        const Line2D(const Vector2<T>& min, const Vector2<T>& max) : A(min), B(max) {}

        inline static const Line2D<T> zero() { return Line2D<T>(Vector2<T>::zero(), Vector2<T>::zero()); }
        inline static const Line2D<T> minValue() { return Line2D<T>(Vector2<T>::minValue(), Vector2<T>::minValue()); }
        inline static const Line2D<T> maxValue() { return Line2D<T>(Vector2<T>::maxValue(), Vector2<T>::maxValue()); }

        inline T linePointCrossProduct(const Vector2<T>& point) const {
            auto tmpLine = Line2D<T>(Vector2<T>::zero(), B - A);
            auto tmpPoint = point - A;
            return tmpLine.B.pointCrossProduct(tmpPoint);
        }
        inline bool isPointOnLine(const Vector2<T>& point) const { return std::fabs(linePointCrossProduct(point)) < constants::tolerance; }
        inline bool isPointRightOfLine(const Vector2<T>& point) const { return linePointCrossProduct(point) < 0; }
        inline bool touchesOrCrosses(const Line2D<T>& other) const {
            return isPointOnLine(other.A) || isPointOnLine(other.B) || (isPointRightOfLine(other.A) ^ isPointRightOfLine(other.B));
        }
        inline AABox2D<T> boundingBox() const { return AABox2D<T>(A.min(B), A.max(B)); }
        inline bool intersects(const AABox2D<T>& thisBox, const Line2D<T>& otherLine, const AABox2D<T>& otherBox) const {
            return thisBox.intersects(otherBox) && touchesOrCrosses(otherLine) && otherLine.touchesOrCrosses(*this);
        }
        inline bool intersects(const Line2D<T>& other) { return intersects(boundingBox(), other, other.boundingBox()); }

        inline Line2D<T> setA(Vector2<T> x) const { return Line2D<T>(x, B); }
        inline Line2D<T> setB(Vector2<T> x) const { return Line2D<T>(A, x); }

        inline std::size_t hash() const { return hash::combineValues(A.hash(), B.hash()); }
        inline bool almostEquals(const Line2D<T>& x, float tolerance = constants::tolerance) const { return A.almostEquals(x.A, tolerance) && B.almostEquals(x.B, tolerance); }

        inline friend bool operator==(const Line2D<T>& o, const Line2D<T>& other) { return o.A == other.A && o.B == other.B; }
        inline friend bool operator!=(const Line2D<T>& o, const Line2D<T>& other) { return !(o == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const Line2D<T>& v) { return (out << "Line2D<T>(A = " << v.A << ", B = " << v.B << ")"); }
    };

#define FLine2D Line2D<float>
#define DLine2D Line2D<double>

    template <typename T = float>
    struct Triangle final : Points<Vector3<T>>, Mappable<Triangle<T>, Vector3<T>> {
        Vector3<T> A;
        Vector3<T> B;
        Vector3<T> C;

        const Triangle(const Vector3<T>& a, const Vector3<T>& b, const Vector3<T>& c) : A(a), B(b), C(c) {}

        inline static const Triangle<T> zero() { return Triangle<T>(Vector3<T>::zero(), Vector3<T>::zero(), Vector3<T>::zero()); }
        inline static const Triangle<T> minValue() { return Triangle<T>(Vector3<T>::minValue(), Vector3<T>::minValue(), Vector3<T>::minValue()); }
        inline static const Triangle<T> maxValue() { return Triangle<T>(Vector3<T>::maxValue(), Vector3<T>::maxValue(), Vector3<T>::maxValue()); }

        inline Triangle<T> setA(Vector3<T> x) const { return Triangle<T>(x, B, C); }
        inline Triangle<T> setB(Vector3<T> x) const { return Triangle<T>(A, x, C); }
        inline Triangle<T> setC(Vector3<T> x) const { return Triangle<T>(A, B, x); }

        inline T lengthA() const { return A.distance(B); }
        inline T lengthB() const { return B.distance(C); }
        inline T lengthC() const { return C.distance(A); }
        inline bool hasArea() const { return A != B && B != C && C != A; }
        inline T area() const { return (B - A).cross(C - A).length() * 0.5; }
        inline T perimeter() const { return lengthA() + lengthB() + lengthC(); }
        inline Vector3<T> midPoint() const { return (A + B + C) / 3; }
        inline Vector3<T> normalDirection() const { return (B - A).cross(C - A); }
        inline Vector3<T> normal() const { return normalDirection().normalize(); }
        inline Vector3<T> safeNormal() const { return normalDirection().safeNormalize(); }
        inline AABox<T> boundingBox() const { return AABox<T>({ A, B, C }); }
        inline Sphere<T> boundingSphere() const { return Sphere<T>({ A, B, C }); }
        inline bool isSliver(float tolerance = constants::tolerance) const { return lengthA() <= tolerance || lengthB() <= tolerance || lengthC() <= tolerance; }
        inline Vector3<T> binormal() const { return (B - A).safeNormalize(); }
        inline Vector3<T> tangent() const { return (C - A).safeNormalize(); }
        inline Line<T> ab() const { return Line<T>(A, B); }
        inline Line<T> bc() const { return Line<T>(B, C); }
        inline Line<T> ca() const { return Line<T>(C, A); }
        inline Line<T> ba() const { return ab().inverse(); }
        inline Line<T> cb() const { return bc().inverse(); }
        inline Line<T> ac() const { return ca().inverse(); }
        inline Line<T> side(int n) { return n == 0 ? ab() : n == 1 ? bc() : ca(); }

        std::size_t numPoints() const override { return 3; }
        Vector3<T> getPoint(std::size_t n) const override { return  n == 0 ? A : n == 1 ? B : C; }
        Triangle<T> map(std::function<Vector3<T>(const Vector3<T>&)> f) const override { return Triangle<T>(f(A), f(B), f(C)); }

        inline std::size_t hash() const { return hash::combine(A.hash(), B.hash(), C.hash()); }
        inline bool almostEquals(const Triangle<T>& x, float tolerance = constants::tolerance) const {
            return A.almostEquals(x.A, tolerance) && B.almostEquals(x.B, tolerance) && C.almostEquals(x.C, tolerance);
        }

        inline friend bool operator==(const Triangle<T>& v1, const Triangle<T>& x) { return v1.A == x.A && v1.B == x.B && v1.C == x.C; }
        inline friend bool operator!=(const Triangle<T>& v1, const Triangle<T>& other) { return !(v1 == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const Triangle<T>& v) { return (out << "Triangle<T>(A = " << v.A << ", B = " << v.B << ", C = " << v.C << ")"); }
    };

#define FTriangle Triangle<float>
#define DTriangle Triangle<double>

    template <typename T = float>
    struct Triangle2D final {
        const std::size_t count = 3;

        Vector2<T> A;
        Vector2<T> B;
        Vector2<T> C;

        const Triangle2D(const Vector2<T>& a, const Vector2<T>& b, const Vector2<T>& c) : A(a), B(b), C(c) {}

        inline static const Triangle2D<T> zero() { return Triangle2D<T>(Vector2<T>::zero(), Vector2<T>::zero(), Vector2<T>::zero()); }
        inline static const Triangle2D<T> minValue() { return Triangle2D<T>(Vector2<T>::minValue(), Vector2<T>::minValue(), Vector2<T>::minValue()); }
        inline static const Triangle2D<T> maxValue() { return Triangle2D<T>(Vector2<T>::maxValue(), Vector2<T>::maxValue(), Vector2<T>::maxValue()); }

        inline Triangle2D<T> setA(Vector2<T> x) const { return Triangle2D<T>(x, B, C); }
        inline Triangle2D<T> setB(Vector2<T> x) const { return Triangle2D<T>(A, x, C); }
        inline Triangle2D<T> setC(Vector2<T> x) const { return Triangle2D<T>(A, B, x); }

        inline std::size_t hash() const { return hash::combine(A.hash(), B.hash(), C.hash()); }
        inline bool almostEquals(const Triangle2D<T>& x, float tolerance = constants::tolerance) const {
            return A.almostEquals(x.A, tolerance) && B.almostEquals(x.B, tolerance) && C.almostEquals(x.C, tolerance);
        }

        // Compute the signed area of a triangle.
        inline static T area(const Vector2<T>& p0, const Vector2<T>& p1, const Vector2<T>& p2) {
            return 0.5 * (p0.X * (p2.Y - p1.Y) + p1.X * (p0.Y - p2.Y) + p2.X * (p1.Y - p0.Y));
        }
        inline T area() const { return area(A, B, C); }
        // Test if a given point p2 is on the left side of the line formed by p0-p1.
        inline static bool onLeftSideOfLine(const Vector2<T>& p0, const Vector2<T>& p1, const Vector2<T>& p2) { return area(p0, p1, p2) > 0; }
        // Test if a given point is inside a given triangle in R2.
        inline bool contains(const Vector2<T>& pp) const {
            // Point in triangle test using barycentric coordinates
            Vector2<T> v0 = B - A;
            Vector2<T> v1 = C - A;
            Vector2<T> v2 = pp - A;

            T dot00 = v0.dot(v0);
            T dot01 = v0.dot(v1);
            T dot02 = v0.dot(v2);
            T dot11 = v1.dot(v1);
            T dot12 = v1.dot(v2);

            T invDenom = (1 / (dot00 * dot11 - dot01 * dot01));
            dot11 = (dot11 * dot02 - dot01 * dot12) * invDenom;
            dot00 = (dot00 * dot12 - dot01 * dot02) * invDenom;

            return (dot11 > 0) && (dot00 > 0) && (dot11 + dot00 < 1);
        }

        inline const Vector2<T>& operator[](std::size_t n) const { if (n == 0) return A; else if (n == 1) return B; else return C; }
        inline friend bool operator==(const Triangle2D<T>& v1, const Triangle2D<T>& v2) { return v1.A == v2.A && v1.B == v2.B && v1.C == v2.C; }
        inline friend bool operator!=(const Triangle2D<T>& v1, const Triangle2D<T>& v2) { return !(v1 == v2); }
        inline friend std::ostream& operator<<(std::ostream& out, const Triangle2D<T>& v) { return (out << "Triangle2D<T>(A = " << v.A << ", B = " << v.B << ", C = " << v.C << ")"); }
    };

#define FTriangle2D Triangle2D<float>
#define DTriangle2D Triangle2D<double>

    template <typename T = float>
    struct Ray final {
        Vector3<T> Position;
        Vector3<T> Direction;

        const Ray(const Vector3<T>& position, const Vector3<T>& direction) : Position(position), Direction(direction) {}
        const Ray(const Line<T>& line) : Ray<T>(line.A, line.vector()) {}
        const Ray(const std::pair<Vector3<T>, Vector3<T>>& pair) : Ray<T>(pair.first, pair.second) {}

        inline static const Ray<T> zero() { return Ray<T>(Vector3<T>::zero(), Vector3<T>::zero()); }
        inline static const Ray<T> minValue() { return Ray<T>(Vector3<T>::minValue(), Vector3<T>::minValue()); }
        inline static const Ray<T> maxValue() { return Ray<T>(Vector3<T>::maxValue(), Vector3<T>::maxValue()); }

        inline Ray<T> setPosition(Vector3<T> x) const { return Ray<T>(x, Direction); }
        inline Ray<T> setDirection(Vector3<T> x) const { return Ray<T>(Position, x); }

        inline std::size_t hash() const { return hash::combineValues(Position.hash(), Direction.hash()); }
        inline bool almostEquals(const Ray<T>& x, float tolerance = constants::tolerance) const {
            return Position.almostEquals(x.Position, tolerance)
                && Direction.almostEquals(x.Direction, tolerance);
        }
        inline std::optional<T> intersects(const AABox<T>& box) const {
            const T Epsilon = 1e-6f;
            std::optional<T> tMin, tMax;

            if (std::fabs(Direction.X) < Epsilon) {
                if (Position.X < box.Min.X || Position.X > box.Max.X) { return std::nullopt; }
            }
            else {
                tMin = (box.Min.X - Position.X) / Direction.X;
                tMax = (box.Max.X - Position.X) / Direction.X;
                if (tMin > tMax) { std::swap(*tMin, *tMax); }
            }

            if (std::fabs(Direction.Y) < Epsilon) {
                if (Position.Y < box.Min.Y || Position.Y > box.Max.Y) { return std::nullopt; }
            }
            else {
                auto tMinY = (box.Min.Y - Position.Y) / Direction.Y;
                auto tMaxY = (box.Max.Y - Position.Y) / Direction.Y;

                if (tMinY > tMaxY) { std::swap(tMinY, tMaxY); }
                if ((tMin && *tMin > tMaxY) || (tMax && tMinY > *tMax)) { return std::nullopt; }

                if (!tMin || tMinY > *tMin) tMin = tMinY;
                if (!tMax || tMaxY < *tMax) tMax = tMaxY;
            }

            if (std::fabs(Direction.Z) < Epsilon) {
                if (Position.Z < box.Min.Z || Position.Z > box.Max.Z) { return std::nullopt; }
            }
            else {
                auto tMinZ = (box.Min.Z - Position.Z) / Direction.Z;
                auto tMaxZ = (box.Max.Z - Position.Z) / Direction.Z;

                if (tMinZ > tMaxZ) { std::swap(tMinZ, tMaxZ); }
                if ((tMin && *tMin > tMaxZ) || (tMax && tMinZ > *tMax)) { return std::nullopt; }

                if (!tMin || tMinZ > *tMin) tMin = tMinZ;
                if (!tMax || tMaxZ < *tMax) tMax = tMaxZ;
            }

            if (tMin && *tMin < 0 && *tMax > 0) { return 0; }
            if (tMin < 0) { return std::nullopt; }
            return tMin;
        }
        inline std::optional<T> intersects(const Plane<T>& plane, float tolerance = constants::tolerance) const {
            T den = Direction.dot(plane.Normal);
            if (std::fabs(den) < tolerance) { return std::nullopt; }

            T result = (-plane.D - plane.Normal.dot(Position)) / den;
            if (result < 0) {
                if (result < -tolerance) { return std::nullopt; }
                result = 0;
            }
            return result;
        }
        inline std::optional<T> intersects(const Sphere<T>& sphere) const {
            // Find the vector between where the ray starts the the sphere's centre
            auto difference = sphere.Center - Position;
            auto differenceLengthSquared = difference.lengthSquared();
            auto sphereRadiusSquared = sphere.Radius * sphere.Radius;
            // If the distance between the ray start and the sphere's centre is less than
            // the radius of the sphere, it means we've intersected. N.B. checking the LengthSquared is faster.
            if (differenceLengthSquared < sphereRadiusSquared) { return 0; }
            auto distanceAlongRay = Direction.dot(difference);
            // If the ray is pointing away from the sphere then we don't ever intersect
            if (distanceAlongRay < 0) { return std::nullopt; }

            // Next we kinda use Pythagoras to check if we are within the bounds of the sphere
            // if x = radius of sphere
            // if y = distance between ray position and sphere centre
            // if z = the distance we've travelled along the ray
            // if x^2 + z^2 - y^2 < 0, we do not intersect
            auto dist = sphereRadiusSquared + (distanceAlongRay * distanceAlongRay) - differenceLengthSquared;
            return (dist < 0) ? std::nullopt : std::optional<T>(distanceAlongRay - std::sqrt(dist));
        }
        inline std::optional<T> intersects(const Triangle<T>& tri, float tolerance = constants::tolerance) const {
            auto edge1 = tri.B - tri.A;
            auto edge2 = tri.C - tri.A;

            auto h = Direction.cross(edge2);
            auto a = edge1.dot(h);
            // This ray is parallel to this triangle.
            if (a > -tolerance && a < tolerance) { return std::nullopt; }

            auto f = 1.0 / a;
            auto s = Position - tri.A;
            auto u = f * s.dot(h);
            if (u < 0 || u > 1) { return std::nullopt; }

            auto q = s.cross(edge1);
            auto v = f * Direction.dot(q);
            if (v < 0 || u + v > 1) { return std::nullopt; }

            // At this stage we can compute t to find out where the intersection point is on the line.
            auto t = f * edge2.dot(q);
            if (t > tolerance) { return t; }
            // This means that there is a line intersection but not a ray intersection.
            return std::nullopt;
        }

        inline friend bool operator==(const Ray<T>& r, const Ray<T>& other) { return r.Position == other.Position && r.Direction == other.Direction; }
        inline friend bool operator!=(const Ray<T>& r, const Ray<T>& other) { return !(r == other); }
        inline friend std::ostream& operator<<(std::ostream& out, const Ray<T>& v) { return (out << "Ray<T>(Position = " << v.Position << ", Direction = " << v.Direction << ")"); }
    };

#define FRay Ray<float>
#define DRay Ray<double>

    template <typename T = float>
    struct Matrix4x4 final {
        T M11;
        T M12;
        T M13;
        T M14;

        T M21;
        T M22;
        T M23;
        T M24;

        T M31;
        T M32;
        T M33;
        T M34;

        T M41;
        T M42;
        T M43;
        T M44;

        const Matrix4x4(T m11, T m12, T m13, T m14, T m21, T m22, T m23, T m24, T m31, T m32, T m33, T m34, T m41, T m42, T m43, T m44) : M11(m11), M12(m12), M13(m13), M14(m14), M21(m21), M22(m22), M23(m23), M24(m24), M31(m31), M32(m32), M33(m33), M34(m34), M41(m41), M42(m42), M43(m43), M44(m44) {}
        const Matrix4x4(T value = 0) : M11(value), M12(value), M13(value), M14(value), M21(value), M22(value), M23(value), M24(value), M31(value), M32(value), M33(value), M34(value), M41(value), M42(value), M43(value), M44(value) {}
        const Matrix4x4(const std::array<T, 16>& m) : M11(m[0]), M12(m[1]), M13(m[2]), M14(m[3]), M21(m[4]), M22(m[5]), M23(m[6]), M24(m[7]), M31(m[8]), M32(m[9]), M33(m[10]), M34(m[11]), M41(m[12]), M42(m[13]), M43(m[14]), M44(m[15]) {}

        const Matrix4x4(const Vector4<T>& row0, const Vector4<T>& row1, const Vector4<T>& row2, const Vector4<T>& row3) : Matrix4x4<T>(row0.X, row0.Y, row0.Z, row0.W, row1.X, row1.Y, row1.Z, row1.W, row2.X, row2.Y, row2.Z, row2.W, row3.X, row3.Y, row3.Z, row3.W) {}
        const Matrix4x4(const Vector3<T>& row0, const Vector3<T>& row1, const Vector3<T>& row2) : Matrix4x4<T>(Vector4<T>(row0), Vector4<T>(row1), Vector4<T>(row2), Vector4<T>(0, 0, 0, 1)) {}
        const Matrix4x4(const Vector3<T>& row0, const Vector3<T>& row1, const Vector3<T>& row2, const Vector3<T>& row3) : Matrix4x4<T>(Vector4<T>(row0), Vector4<T>(row1), Vector4<T>(row2), Vector4<T>(row3.X, row3.Y, row3.Z, 1)) {}
        const Matrix4x4(const Vector4<T>& row0, const Vector4<T>& row1, const Vector4<T>& row2) : Matrix4x4<T>(row0.xyz(), row1.xyz(), row2.xyz(), Vector3<T>(0)) {}

        Vector3<T> col0() const { return Vector3<T>(M11, M21, M31); }
        Vector3<T> col1() const { return Vector3<T>(M12, M22, M32); }
        Vector3<T> col2() const { return Vector3<T>(M13, M23, M33); }
        Vector3<T> col3() const { return Vector3<T>(M14, M24, M34); }

        Vector3<T> row0() const { return Vector3<T>(M11, M12, M13); }
        Vector3<T> row1() const { return Vector3<T>(M21, M22, M23); }
        Vector3<T> row2() const { return Vector3<T>(M31, M32, M33); }
        Vector3<T> row3() const { return Vector3<T>(M41, M42, M43); }

        Vector3<T> getRow(int row) const { return row == 0 ? row0() : row == 1 ? row1() : row == 2 ? row2() : row3(); }
        Vector3<T> getCol(int col) const { return col == 0 ? col0() : col == 1 ? col1() : col == 2 ? col2() : col3(); }

        static const Matrix4x4<T> identity() {
            return Matrix4x4<T>(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1);
        }

        inline static Matrix4x4<T> billboard(const Vector3<T>& objectPosition, const Vector3<T>& cameraPosition, const Vector3<T>& cameraUpVector, const Vector3<T>& cameraForwardVector) {
            const T epsilon = 1e-4;
            auto zaxis = Vector3<T>(
                objectPosition.X - cameraPosition.X,
                objectPosition.Y - cameraPosition.Y,
                objectPosition.Z - cameraPosition.Z);

            auto norm = zaxis.lengthSquared();
            if (norm < epsilon) { zaxis = -cameraForwardVector; }
            else { zaxis = zaxis * (1.0 / std::sqrt(norm)); }

            auto xaxis = cameraUpVector.cross(zaxis).normalize();
            auto yaxis = zaxis.cross(xaxis);

            Matrix4x4<T> result;

            result.M11 = xaxis.X;
            result.M12 = xaxis.Y;
            result.M13 = xaxis.Z;
            result.M14 = 0;
            result.M21 = yaxis.X;
            result.M22 = yaxis.Y;
            result.M23 = yaxis.Z;
            result.M24 = 0;
            result.M31 = zaxis.X;
            result.M32 = zaxis.Y;
            result.M33 = zaxis.Z;
            result.M34 = 0;

            result.M41 = objectPosition.X;
            result.M42 = objectPosition.Y;
            result.M43 = objectPosition.Z;
            result.M44 = 1;

            return result;
        }
        inline static Matrix4x4<T> constrainedBillboard(const Vector3<T>& objectPosition, const Vector3<T>& cameraPosition, const Vector3<T>& rotateAxis, const Vector3<T>& cameraForwardVector, const Vector3<T>& objectForwardVector) {
            const float epsilon = 1e-4f;
            const float minAngle = 1.0 - (0.1 * (constants::pi / 180.0)); // 0.1 degrees

            // Treat the case when object and camera positions are too close.
            auto faceDir = Vector3<T>(
                objectPosition.X - cameraPosition.X,
                objectPosition.Y - cameraPosition.Y,
                objectPosition.Z - cameraPosition.Z);
            auto norm = faceDir.lengthSquared();
            if (norm < epsilon) { faceDir = -cameraForwardVector; }
            else { faceDir = faceDir * 1.0 / sqrt(norm); }

            Vector3<T> yaxis = rotateAxis;
            Vector3<T> xaxis;
            Vector3<T> zaxis;

            // Treat the case when angle between faceDir and rotateAxis is too close to 0.
            auto d = rotateAxis.dot(faceDir);
            if (std::fabs(d) > minAngle) {
                zaxis = objectForwardVector;
                // Make sure passed values are useful for compute.
                d = rotateAxis.dot(zaxis);
                if (std::fabs(d) > minAngle) {
                    zaxis = (std::fabs(rotateAxis.Z) > minAngle) ? Vector3<T>(1, 0, 0) : Vector3<T>(0, 0, -1);
                }
                xaxis = rotateAxis.cross(zaxis).normalize();
                zaxis = xaxis.cross(rotateAxis).normalize();
            }
            else
            {
                xaxis = rotateAxis.cross(faceDir).normalize();
                zaxis = xaxis.cross(yaxis).normalize();
            }

            Matrix4x4<T> result;

            result.M11 = xaxis.X;
            result.M12 = xaxis.Y;
            result.M13 = xaxis.Z;
            result.M14 = 0;
            result.M21 = yaxis.X;
            result.M22 = yaxis.Y;
            result.M23 = yaxis.Z;
            result.M24 = 0;
            result.M31 = zaxis.X;
            result.M32 = zaxis.Y;
            result.M33 = zaxis.Z;
            result.M34 = 0;

            result.M41 = objectPosition.X;
            result.M42 = objectPosition.Y;
            result.M43 = objectPosition.Z;
            result.M44 = 1;

            return result;
        }
        inline static Matrix4x4<T> translation(const Vector3<T>& position) {
            Matrix4x4<T> result;

            result.M11 = 1;
            result.M12 = 0;
            result.M13 = 0;
            result.M14 = 0;
            result.M21 = 0;
            result.M22 = 1;
            result.M23 = 0;
            result.M24 = 0;
            result.M31 = 0;
            result.M32 = 0;
            result.M33 = 1;
            result.M34 = 0;

            result.M41 = position.X;
            result.M42 = position.Y;
            result.M43 = position.Z;
            result.M44 = 1;

            return result;
        }
        inline static Matrix4x4<T> translation(T x, T y, T z) { return translation(Vector3<T>(x, y, z)); }
        inline static Matrix4x4<T> scale(T xScale, T yScale, T zScale) {
            Matrix4x4<T> result;

            result.M11 = xScale;
            result.M12 = 0;
            result.M13 = 0;
            result.M14 = 0;
            result.M21 = 0;
            result.M22 = yScale;
            result.M23 = 0;
            result.M24 = 0;
            result.M31 = 0;
            result.M32 = 0;
            result.M33 = zScale;
            result.M34 = 0;
            result.M41 = 0;
            result.M42 = 0;
            result.M43 = 0;
            result.M44 = 1;

            return result;
        }
        inline static Matrix4x4<T> scale(T xScale, T yScale, T zScale, const Vector3<T>& centerPoint)
        {
            Matrix4x4<T> result;

            auto tx = centerPoint.X * (1 - xScale);
            auto ty = centerPoint.Y * (1 - yScale);
            auto tz = centerPoint.Z * (1 - zScale);

            result.M11 = xScale;
            result.M12 = 0;
            result.M13 = 0;
            result.M14 = 0;
            result.M21 = 0;
            result.M22 = yScale;
            result.M23 = 0;
            result.M24 = 0;
            result.M31 = 0;
            result.M32 = 0;
            result.M33 = zScale;
            result.M34 = 0;
            result.M41 = tx;
            result.M42 = ty;
            result.M43 = tz;
            result.M44 = 1;

            return result;
        }
        inline static Matrix4x4<T> scale(const Vector3<T>& scales) { return scale(scales.X, scales.Y, scales.Z); }
        inline static Matrix4x4<T> scale(const Vector3<T>& scales, const Vector3<T>& centerPoint) { return scale(scales.X, scales.Y, scales.Z, centerPoint); }
        inline static Matrix4x4<T> scale(T s) { return scale(s, s, s); }
        inline static Matrix4x4<T> scale(T s, const Vector3<T>& centerPoint) { return scale(s, s, s, centerPoint); }
        inline static Matrix4x4<T> rotationX(T radians) {
            Matrix4x4<T> result;

            auto c = std::cos(radians);
            auto s = std::sin(radians);

            // [  1  0  0  0 ]
            // [  0  c  s  0 ]
            // [  0 -s  c  0 ]
            // [  0  0  0  1 ]
            result.M11 = 1;
            result.M12 = 0;
            result.M13 = 0;
            result.M14 = 0;
            result.M21 = 0;
            result.M22 = c;
            result.M23 = s;
            result.M24 = 0;
            result.M31 = 0;
            result.M32 = -s;
            result.M33 = c;
            result.M34 = 0;
            result.M41 = 0;
            result.M42 = 0;
            result.M43 = 0;
            result.M44 = 1;

            return result;
        }
        inline static Matrix4x4<T> rotationX(T radians, const Vector3<T>& centerPoint) {
            Matrix4x4<T> result;

            auto c = std::cos(radians);
            auto s = std::sin(radians);

            auto y = centerPoint.Y * (1 - c) + centerPoint.Z * s;
            auto z = centerPoint.Z * (1 - c) - centerPoint.Y * s;

            // [  1  0  0  0 ]
            // [  0  c  s  0 ]
            // [  0 -s  c  0 ]
            // [  0  y  z  1 ]
            result.M11 = 1;
            result.M12 = 0;
            result.M13 = 0;
            result.M14 = 0;
            result.M21 = 0;
            result.M22 = c;
            result.M23 = s;
            result.M24 = 0;
            result.M31 = 0;
            result.M32 = -s;
            result.M33 = c;
            result.M34 = 0;
            result.M41 = 0;
            result.M42 = y;
            result.M43 = z;
            result.M44 = 1;

            return result;
        }
        inline static Matrix4x4<T> rotationY(T radians) {
            Matrix4x4<T> result;

            auto c = std::cos(radians);
            auto s = std::sin(radians);

            // [  c  0 -s  0 ]
            // [  0  1  0  0 ]
            // [  s  0  c  0 ]
            // [  0  0  0  1 ]
            result.M11 = c;
            result.M12 = 0;
            result.M13 = -s;
            result.M14 = 0;
            result.M21 = 0;
            result.M22 = 1;
            result.M23 = 0;
            result.M24 = 0;
            result.M31 = s;
            result.M32 = 0;
            result.M33 = c;
            result.M34 = 0;
            result.M41 = 0;
            result.M42 = 0;
            result.M43 = 0;
            result.M44 = 1;

            return result;
        }
        inline static Matrix4x4<T> rotationY(T radians, const Vector3<T>& centerPoint) {
            Matrix4x4<T> result;

            auto c = std::cos(radians);
            auto s = std::sin(radians);

            auto x = centerPoint.X * (1 - c) - centerPoint.Z * s;
            auto z = centerPoint.Z * (1 - c) + centerPoint.X * s;

            // [  c  0 -s  0 ]
            // [  0  1  0  0 ]
            // [  s  0  c  0 ]
            // [  x  0  z  1 ]
            result.M11 = c;
            result.M12 = 0;
            result.M13 = -s;
            result.M14 = 0;
            result.M21 = 0;
            result.M22 = 1;
            result.M23 = 0;
            result.M24 = 0;
            result.M31 = s;
            result.M32 = 0;
            result.M33 = c;
            result.M34 = 0;
            result.M41 = x;
            result.M42 = 0;
            result.M43 = z;
            result.M44 = 1;

            return result;
        }
        inline static Matrix4x4<T> rotationZ(T radians) {
            Matrix4x4<T> result;

            auto c = std::cos(radians);
            auto s = std::sin(radians);

            // [  c  s  0  0 ]
            // [ -s  c  0  0 ]
            // [  0  0  1  0 ]
            // [  0  0  0  1 ]
            result.M11 = c;
            result.M12 = s;
            result.M13 = 0;
            result.M14 = 0;
            result.M21 = -s;
            result.M22 = c;
            result.M23 = 0;
            result.M24 = 0;
            result.M31 = 0;
            result.M32 = 0;
            result.M33 = 1;
            result.M34 = 0;
            result.M41 = 0;
            result.M42 = 0;
            result.M43 = 0;
            result.M44 = 1;

            return result;
        }
        inline static Matrix4x4<T> rotationZ(T radians, const Vector3<T>& centerPoint) {
            Matrix4x4<T> result;

            auto c = std::cos(radians);
            auto s = std::sin(radians);

            auto x = centerPoint.X * (1 - c) + centerPoint.Y * s;
            auto y = centerPoint.Y * (1 - c) - centerPoint.X * s;

            // [  c  s  0  0 ]
            // [ -s  c  0  0 ]
            // [  0  0  1  0 ]
            // [  x  y  0  1 ]
            result.M11 = c;
            result.M12 = s;
            result.M13 = 0;
            result.M14 = 0;
            result.M21 = -s;
            result.M22 = c;
            result.M23 = 0;
            result.M24 = 0;
            result.M31 = 0;
            result.M32 = 0;
            result.M33 = 1;
            result.M34 = 0;
            result.M41 = x;
            result.M42 = y;
            result.M43 = 0;
            result.M44 = 1;

            return result;
        }
        inline static Matrix4x4<T> fromAxisAngle(const Vector3<T>& axis, T angle) {
            // a: angle
            // x, y, z: unit vector for axis.
            //
            // Rotation matrix M can compute by using below equation.
            //
            //        T               T
            //  M = uu + (cos a)( I-uu ) + (sin a)S
            //
            // Where:
            //
            //  u = ( x, y, z )
            //
            //      [  0 -z  y ]
            //  S = [  z  0 -x ]
            //      [ -y  x  0 ]
            //
            //      [ 1 0 0 ]
            //  I = [ 0 1 0 ]
            //      [ 0 0 1 ]
            //
            //
            //     [  xx+cosa*(1-xx)   yx-cosa*yx-sina*z zx-cosa*xz+sina*y ]
            // M = [ xy-cosa*yx+sina*z    yy+cosa(1-yy)  yz-cosa*yz-sina*x ]
            //     [ zx-cosa*zx-sina*y zy-cosa*zy+sina*x   zz+cosa*(1-zz)  ]
            //
            auto x = axis.X, y = axis.Y, z = axis.Z;
            auto sa = std::sin(angle), ca = std::cos(angle);
            auto xx = x * x, yy = y * y, zz = z * z;
            auto xy = x * y, xz = x * z, yz = y * z;

            Matrix4x4<T> result;

            result.M11 = xx + ca * (1 - xx);
            result.M12 = xy - ca * xy + sa * z;
            result.M13 = xz - ca * xz - sa * y;
            result.M14 = 0;
            result.M21 = xy - ca * xy - sa * z;
            result.M22 = yy + ca * (1 - yy);
            result.M23 = yz - ca * yz + sa * x;
            result.M24 = 0;
            result.M31 = xz - ca * xz + sa * y;
            result.M32 = yz - ca * yz - sa * x;
            result.M33 = zz + ca * (1 - zz);
            result.M34 = 0;
            result.M41 = 0;
            result.M42 = 0;
            result.M43 = 0;
            result.M44 = 1;

            return result;
        }
        inline static Matrix4x4<T> perspectiveFieldOfView(T fieldOfView, T aspectRatio, T nearPlaneDistance, T farPlaneDistance) {
            if (fieldOfView < 0 || fieldOfView >= constants::pi) { throw std::out_of_range("fieldOfView is out of range"); }
            if (nearPlaneDistance <= 0) { throw std::out_of_range("nearPlaneDistance is out of range"); }
            if (farPlaneDistance <= 0) { throw std::out_of_range("farPlaneDistance is out of range"); }
            if (nearPlaneDistance >= farPlaneDistance) { throw std::out_of_range("nearPlaneDistance is out of range"); }
 
            auto yScale = 1 / tan(fieldOfView * 0.5);
            auto xScale = yScale / aspectRatio;

            Matrix4x4<T> result;

            result.M11 = xScale;
            result.M12 = result.M13 = result.M14 = 0;

            result.M22 = yScale;
            result.M21 = result.M23 = result.M24 = 0;

            result.M31 = result.M32 = 0;
            auto negFarRange = (std::isinf(farPlaneDistance) && farPlaneDistance > 0) ? -1 : farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            result.M33 = negFarRange;
            result.M34 = -1;

            result.M41 = result.M42 = result.M44 = 0;
            result.M43 = nearPlaneDistance * negFarRange;

            return result;
        }
        inline static Matrix4x4<T> perspective(T width, T height, T nearPlaneDistance, T farPlaneDistance) {
            if (nearPlaneDistance <= 0) { throw std::out_of_range("nearPlaneDistance is out of range"); }
            if (farPlaneDistance <= 0) { throw std::out_of_range("farPlaneDistance is out of range"); }
            if (nearPlaneDistance >= farPlaneDistance) { throw std::out_of_range("nearPlaneDistance is out of range"); }

            Matrix4x4<T> result;

            result.M11 = 2 * nearPlaneDistance / width;
            result.M12 = result.M13 = result.M14 = 0;

            result.M22 = 2 * nearPlaneDistance / height;
            result.M21 = result.M23 = result.M24 = 0;

            auto negFarRange = (std::isinf(farPlaneDistance) && farPlaneDistance > 0) ? -1 : farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            result.M33 = negFarRange;
            result.M31 = result.M32 = 0;
            result.M34 = -1;

            result.M41 = result.M42 = result.M44 = 0;
            result.M43 = nearPlaneDistance * negFarRange;

            return result;
        }
        inline static Matrix4x4<T> perspectiveOffCenter(T left, T right, T bottom, T top, T nearPlaneDistance, T farPlaneDistance) {
            if (nearPlaneDistance <= 0) { throw std::out_of_range("nearPlaneDistance is out of range"); }
            if (farPlaneDistance <= 0) { throw std::out_of_range("farPlaneDistance is out of range"); }
            if (nearPlaneDistance >= farPlaneDistance) { throw std::out_of_range("nearPlaneDistance is out of range"); }

            Matrix4x4<T> result;

            result.M11 = 2 * nearPlaneDistance / (right - left);
            result.M12 = result.M13 = result.M14 = 0;

            result.M22 = 2 * nearPlaneDistance / (top - bottom);
            result.M21 = result.M23 = result.M24 = 0;

            result.M31 = (left + right) / (right - left);
            result.M32 = (top + bottom) / (top - bottom);
            auto negFarRange = (std::isinf(farPlaneDistance) && farPlaneDistance > 0) ? -1 : farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            result.M33 = negFarRange;
            result.M34 = -1;

            result.M43 = nearPlaneDistance * negFarRange;
            result.M41 = result.M42 = result.M44 = 0;

            return result;
        }
        inline static Matrix4x4<T> orthographic(T width, T height, T zNearPlane, T zFarPlane)
        {
            Matrix4x4<T> result;

            result.M11 = 2 / width;
            result.M12 = result.M13 = result.M14 = 0;

            result.M22 = 2 / height;
            result.M21 = result.M23 = result.M24 = 0;

            result.M33 = 1 / (zNearPlane - zFarPlane);
            result.M31 = result.M32 = result.M34 = 0;

            result.M41 = result.M42 = 0;
            result.M43 = zNearPlane / (zNearPlane - zFarPlane);
            result.M44 = 1;

            return result;
        }
        inline static Matrix4x4<T> orthographicOffCenter(T left, T right, T bottom, T top, T zNearPlane, T zFarPlane)
        {
            Matrix4x4<T> result;

            result.M11 = 2 / (right - left);
            result.M12 = result.M13 = result.M14 = 0;

            result.M22 = 2 / (top - bottom);
            result.M21 = result.M23 = result.M24 = 0;

            result.M33 = 1 / (zNearPlane - zFarPlane);
            result.M31 = result.M32 = result.M34 = 0;

            result.M41 = (left + right) / (left - right);
            result.M42 = (top + bottom) / (bottom - top);
            result.M43 = zNearPlane / (zNearPlane - zFarPlane);
            result.M44 = 1;

            return result;
        }
        inline static Matrix4x4<T> lookAt(const Vector3<T>& cameraPosition, const Vector3<T>& cameraTarget, const Vector3<T>& cameraUpVector)
        {
            auto zaxis = (cameraPosition - cameraTarget).normalize();
            auto xaxis = cameraUpVector.cross(zaxis).normalize();
            auto yaxis = zaxis.cross(xaxis);

            Matrix4x4<T> result;

            result.M11 = xaxis.X;
            result.M12 = yaxis.X;
            result.M13 = zaxis.X;
            result.M14 = 0;
            result.M21 = xaxis.Y;
            result.M22 = yaxis.Y;
            result.M23 = zaxis.Y;
            result.M24 = 0;
            result.M31 = xaxis.Z;
            result.M32 = yaxis.Z;
            result.M33 = zaxis.Z;
            result.M34 = 0;
            result.M41 = -xaxis.dot(cameraPosition);
            result.M42 = -yaxis.dot(cameraPosition);
            result.M43 = -zaxis.dot(cameraPosition);
            result.M44 = 1;

            return result;
        }
        inline static Matrix4x4<T> world(const Vector3<T>& position, const Vector3<T>& forward, const Vector3<T>& up)
        {
            auto zaxis = (-forward).normalize();
            auto xaxis = (up.cross(zaxis)).normalize();
            auto yaxis = zaxis.cross(xaxis);

            Matrix4x4<T> result;

            result.M11 = xaxis.X;
            result.M12 = xaxis.Y;
            result.M13 = xaxis.Z;
            result.M14 = 0;
            result.M21 = yaxis.X;
            result.M22 = yaxis.Y;
            result.M23 = yaxis.Z;
            result.M24 = 0;
            result.M31 = zaxis.X;
            result.M32 = zaxis.Y;
            result.M33 = zaxis.Z;
            result.M34 = 0;
            result.M41 = position.X;
            result.M42 = position.Y;
            result.M43 = position.Z;
            result.M44 = 1;

            return result;
        }
        inline static Matrix4x4<T> rotation(const Quaternion<T>& quaternion) {
            Matrix4x4<T> result;

            auto xx = quaternion.X * quaternion.X;
            auto yy = quaternion.Y * quaternion.Y;
            auto zz = quaternion.Z * quaternion.Z;

            auto xy = quaternion.X * quaternion.Y;
            auto wz = quaternion.Z * quaternion.W;
            auto xz = quaternion.Z * quaternion.X;
            auto wy = quaternion.Y * quaternion.W;
            auto yz = quaternion.Y * quaternion.Z;
            auto wx = quaternion.X * quaternion.W;

            result.M11 = 1 - 2 * (yy + zz);
            result.M12 = 2 * (xy + wz);
            result.M13 = 2 * (xz - wy);
            result.M14 = 0;
            result.M21 = 2 * (xy - wz);
            result.M22 = 1 - 2 * (zz + xx);
            result.M23 = 2 * (yz + wx);
            result.M24 = 0;
            result.M31 = 2 * (xz + wy);
            result.M32 = 2 * (yz - wx);
            result.M33 = 1 - 2 * (yy + xx);
            result.M34 = 0;
            result.M41 = 0;
            result.M42 = 0;
            result.M43 = 0;
            result.M44 = 1;

            return result;
        }
        inline static Matrix4x4<T> fromQuaternion(const Quaternion<T>& quaternion) { return rotation(quaternion); }
        inline static Matrix4x4<T> fromYawPitchRoll(T yaw, T pitch, T roll) { return rotation(Quaternion<T>::fromYawPitchRoll(yaw, pitch, roll)); }
        inline static Matrix4x4<T> shadow(const Vector3<T>& lightDirection, const Plane<T>& plane) {
            auto p = plane.normalize();

            auto dot = p.Normal.X * lightDirection.X + p.Normal.Y * lightDirection.Y + p.Normal.Z * lightDirection.Z;
            auto a = -p.Normal.X;
            auto b = -p.Normal.Y;
            auto c = -p.Normal.Z;
            auto d = -p.D;

            Matrix4x4<T> result;

            result.M11 = a * lightDirection.X + dot;
            result.M21 = b * lightDirection.X;
            result.M31 = c * lightDirection.X;
            result.M41 = d * lightDirection.X;

            result.M12 = a * lightDirection.Y;
            result.M22 = b * lightDirection.Y + dot;
            result.M32 = c * lightDirection.Y;
            result.M42 = d * lightDirection.Y;

            result.M13 = a * lightDirection.Z;
            result.M23 = b * lightDirection.Z;
            result.M33 = c * lightDirection.Z + dot;
            result.M43 = d * lightDirection.Z;

            result.M14 = 0;
            result.M24 = 0;
            result.M34 = 0;
            result.M44 = dot;

            return result;
        }
        inline static Matrix4x4<T> reflection(const Plane<T>& plane) {
            auto value = plane.normalize();

            auto a = value.Normal.X;
            auto b = value.Normal.Y;
            auto c = value.Normal.Z;

            auto fa = -2 * a;
            auto fb = -2 * b;
            auto fc = -2 * c;

            Matrix4x4<T> result;

            result.M11 = fa * a + 1;
            result.M12 = fb * a;
            result.M13 = fc * a;
            result.M14 = 0;

            result.M21 = fa * b;
            result.M22 = fb * b + 1;
            result.M23 = fc * b;
            result.M24 = 0;

            result.M31 = fa * c;
            result.M32 = fb * c;
            result.M33 = fc * c + 1;
            result.M34 = 0;

            result.M41 = fa * value.D;
            result.M42 = fb * value.D;
            result.M43 = fc * value.D;
            result.M44 = 1;

            return result;
        }
        inline static Matrix4x4<T> trs(const Vector3<T>& t, const Quaternion<T>& r, const Vector3<T>& s) { return translation(t) * rotation(r) * scale(s); }
        inline static Matrix4x4<T> trs(const Transform<T>& self) { return trs(self.Position, self.Orientation, Vector3<T>::one()); }
        
        inline static std::vector<Matrix4x4<T>> matrises(const std::vector<T>& m) {
            assert(m.size() % 16 == 0);
            std::vector<Matrix4x4<T>> ret(m.size() / 16);
            for (int i = 0; i < ret.size(); i++) {
                int i16 = i * 16;
                ret[i] = Matrix4x4<T>(
                    m[i16 + 0], m[i16 + 1], m[i16 + 2], m[i16 + 3],
                    m[i16 + 4], m[i16 + 5], m[i16 + 6], m[i16 + 7],
                    m[i16 + 8], m[i16 + 9], m[i16 + 10], m[i16 + 11],
                    m[i16 + 12], m[i16 + 13], m[i16 + 14], m[i16 + 15]);
            }
            return ret;
        }
        inline static Vector2<T> transform(const Vector2<T>& position, const Matrix4x4<T>& m) {
            return Vector2<T>(
                position.X * m.M11 + position.Y * m.M21 + m.M41,
                position.X * m.M12 + position.Y * m.M22 + m.M42);
        }
        inline static Vector2<T> transformNormal(const Vector2<T>& normal, const Matrix4x4<T>& matrix) {
            return Vector2<T>(
                normal.X * matrix.M11 + normal.Y * matrix.M21,
                normal.X * matrix.M12 + normal.Y * matrix.M22);
        }
        inline static Vector4<T> transformToVector4(const Vector2<T>& position, const Matrix4x4<T>& matrix) {
            return Vector4<T>(
                position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41,
                position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42,
                position.X * matrix.M13 + position.Y * matrix.M23 + matrix.M43,
                position.X * matrix.M14 + position.Y * matrix.M24 + matrix.M44);
        }
        inline static Vector3<T> transform(const Vector3<T>& v, const Matrix4x4<T>& matrix) {
            return Vector3<T>(
                v.X * matrix.M11 + v.Y * matrix.M21 + v.Z * matrix.M31 + matrix.M41,
                v.X * matrix.M12 + v.Y * matrix.M22 + v.Z * matrix.M32 + matrix.M42,
                v.X * matrix.M13 + v.Y * matrix.M23 + v.Z * matrix.M33 + matrix.M43);
        }
        inline static Vector3<T> transformNormal(const Vector3<T>& normal, const Matrix4x4<T>& matrix) {
            return Vector3<T>(
                normal.X * matrix.M11 + normal.Y * matrix.M21 + normal.Z * matrix.M31,
                normal.X * matrix.M12 + normal.Y * matrix.M22 + normal.Z * matrix.M32,
                normal.X * matrix.M13 + normal.Y * matrix.M23 + normal.Z * matrix.M33
            );
        }
        inline static Vector4<T> transformToVector4(const Vector3<T>& position, const Matrix4x4<T>& matrix) {
            return Vector4<T>(
                position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41,
                position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42,
                position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43,
                position.X * matrix.M14 + position.Y * matrix.M24 + position.Z * matrix.M34 + matrix.M44);
        }
        inline static Vector4<T> transform(const Vector4<T>& value, const Matrix4x4<T>& matrix) {
            return Vector4<T>(
                value.X * matrix.M11 + value.Y * matrix.M21 + value.Z * matrix.M31 + value.W * matrix.M41,
                value.X * matrix.M12 + value.Y * matrix.M22 + value.Z * matrix.M32 + value.W * matrix.M42,
                value.X * matrix.M13 + value.Y * matrix.M23 + value.Z * matrix.M33 + value.W * matrix.M43,
                value.X * matrix.M14 + value.Y * matrix.M24 + value.Z * matrix.M34 + value.W * matrix.M44);
        }
        inline static Vector4<T> transformNormal(const Vector4<T>& normal, const Matrix4x4<T>& matrix) {
            return Vector4<T>(
                normal.X * matrix.M11 + normal.Y * matrix.M21 + normal.Z * matrix.M31 + normal.W * matrix.M41,
                normal.X * matrix.M12 + normal.Y * matrix.M22 + normal.Z * matrix.M32 + normal.W * matrix.M42,
                normal.X * matrix.M13 + normal.Y * matrix.M23 + normal.Z * matrix.M33 + normal.W * matrix.M43,
                normal.X * matrix.M14 + normal.Y * matrix.M24 + normal.Z * matrix.M34 + normal.W * matrix.M44
            );
        }
        inline static Quad<T> transform(const Quad<T>& q, const Matrix4x4<T>& mat) {
            return q.map([&mat](const Vector3<T>& x) { return transform(x, mat); });
        }
        inline static Sphere<T> transform(const Sphere<T>& s, const Matrix4x4<T>& m) {
            auto center = transform(s.Center, m);
            auto radius = s.Radius * (std::sqrt(
                std::max((m.M11 * m.M11) + (m.M12 * m.M12) + (m.M13 * m.M13),
                    std::max((m.M21 * m.M21) + (m.M22 * m.M22) + (m.M23 * m.M23),
                        (m.M31 * m.M31) + (m.M32 * m.M32) + (m.M33 * m.M33)))));
            return Sphere<T>(center, radius);
        }
        inline static Plane<T> transform(const Plane<T>& p, const Matrix4x4<T>& matrix) {
            auto m = matrix.invert().value_or(Matrix4x4<T>(std::numeric_limits<T>::quiet_NaN()));
            auto x = p.Normal.X, y = p.Normal.Y, z = p.Normal.Z, w = p.D;
            return Plane<T>(
                x * m.M11 + y * m.M12 + z * m.M13 + w * m.M14,
                x * m.M21 + y * m.M22 + z * m.M23 + w * m.M24,
                x * m.M31 + y * m.M32 + z * m.M33 + w * m.M34,
                x * m.M41 + y * m.M42 + z * m.M43 + w * m.M44);
        }
        inline static AABox<T> transform(const AABox<T>& box, const Matrix4x4<T>& m) {
            std::vector<Vector3<T>> tCorners;
            for (const Vector3<T>& corner : box.corners()) {
                tCorners.push_back(transform(corner, m));
            }
            return AABox<T>(tCorners);
        }
        inline static Line<T> transform(const Line<T>& line, const Matrix4x4<T>& mat) {
            return Line<T>(
                transform(line.A, mat),
                transform(line.B, mat));
        }
        inline static Triangle<T> transform(const Triangle<T>& tr, const Matrix4x4<T>& mat) {
            return tr.map([&mat](const Vector3<T>& x) { return transform(x, mat); });
        };
        inline static Ray<T> transform(const Ray<T>& x, const Matrix4x4<T>& mat) {
            return Ray<T>(
                transform(x.Position, mat),
                transformNormal(x.Direction, mat));
        };

        bool isIdentity() const {
            return // Check diagonal element first for early out.
                M11 == 1 && M22 == 1 && M33 == 1 && M44 == 1 &&
                M12 == 0 && M13 == 0 && M14 == 0 &&
                M21 == 0 && M23 == 0 && M24 == 0 &&
                M31 == 0 && M32 == 0 && M34 == 0 &&
                M41 == 0 && M42 == 0 && M43 == 0;
        }
        inline Vector3<T> translation() const { return Vector3<T>(M41, M42, M43); }
        inline Matrix4x4<T> setTranslation(const Vector3<T>& v) const { return Matrix4x4<T>(row0(), row1(), row2(), v); }
        inline Matrix4x4<T> scaleTranslation(T amount) const { return setTranslation(translation() * amount); }
        inline T get3x3RotationDeterminant() const {
            // | a b c |
            // | d e f | = a | e f | - b | d f | + c | d e |
            // | g h i |     | h i |     | g i |     | g h |
            //
            // a | e f | = a ( ei - fh )
            //   | h i | 
            //
            // b | d f | = b ( di - gf )
            //   | g i |
            //
            // c | d e | = c ( dh - eg )
            //   | g h |

            T a = M11, b = M12, c = M13;
            T d = M21, e = M22, f = M23;
            T g = M31, h = M32, i = M33;

            auto ei_fh = e * i - f * h;
            auto di_gf = d * i - g * f;
            auto dh_eg = d * h - e * g;

            return a * ei_fh -
                b * di_gf +
                c * dh_eg;
        }
        inline bool isReflection() const { return get3x3RotationDeterminant() < 0; }
        inline T getDeterminant() const {
            // | a b c d |     | f g h |     | e g h |     | e f h |     | e f g |
            // | e f g h | = a | j k l | - b | i k l | + c | i j l | - d | i j k |
            // | i j k l |     | n o p |     | m o p |     | m n p |     | m n o |
            // | m n o p |
            //
            //   | f g h |
            // a | j k l | = a ( f ( kp - lo ) - g ( jp - ln ) + h ( jo - kn ) )
            //   | n o p |
            //
            //   | e g h |     
            // b | i k l | = b ( e ( kp - lo ) - g ( ip - lm ) + h ( io - km ) )
            //   | m o p |     
            //
            //   | e f h |
            // c | i j l | = c ( e ( jp - ln ) - f ( ip - lm ) + h ( in - jm ) )
            //   | m n p |
            //
            //   | e f g |
            // d | i j k | = d ( e ( jo - kn ) - f ( io - km ) + g ( in - jm ) )
            //   | m n o |
            //
            // Cost of operation
            // 17 adds and 28 muls.
            //
            // add: 6 + 8 + 3 = 17
            // mul: 12 + 16 = 28

            T a = M11, b = M12, c = M13, d = M14;
            T e = M21, f = M22, g = M23, h = M24;
            T i = M31, j = M32, k = M33, l = M34;
            T m = M41, n = M42, o = M43, p = M44;

            auto kp_lo = k * p - l * o;
            auto jp_ln = j * p - l * n;
            auto jo_kn = j * o - k * n;
            auto ip_lm = i * p - l * m;
            auto io_km = i * o - k * m;
            auto in_jm = i * n - j * m;

            return a * (f * kp_lo - g * jp_ln + h * jo_kn) -
                b * (e * kp_lo - g * ip_lm + h * io_km) +
                c * (e * jp_ln - f * ip_lm + h * in_jm) -
                d * (e * jo_kn - f * io_km + g * in_jm);
        }
        inline std::optional<Matrix4x4<T>> invert() const {
            //                                       -1
            // If you have matrix M, inverse Matrix M   can compute
            //
            //     -1       1      
            //    M   = --------- A
            //            det(M)
            //
            // A is adjugate (adjoint) of M, where,
            //
            //      T
            // A = C
            //
            // C is Cofactor matrix of M, where,
            //           i + j
            // C   = (-1)      * det(M  )
            //  ij                    ij
            //
            //     [ a b c d ]
            // M = [ e f g h ]
            //     [ i j k l ]
            //     [ m n o p ]
            //
            // First Row
            //           2 | f g h |
            // C   = (-1)  | j k l | = + ( f ( kp - lo ) - g ( jp - ln ) + h ( jo - kn ) )
            //  11         | n o p |
            //
            //           3 | e g h |
            // C   = (-1)  | i k l | = - ( e ( kp - lo ) - g ( ip - lm ) + h ( io - km ) )
            //  12         | m o p |
            //
            //           4 | e f h |
            // C   = (-1)  | i j l | = + ( e ( jp - ln ) - f ( ip - lm ) + h ( in - jm ) )
            //  13         | m n p |
            //
            //           5 | e f g |
            // C   = (-1)  | i j k | = - ( e ( jo - kn ) - f ( io - km ) + g ( in - jm ) )
            //  14         | m n o |
            //
            // Second Row
            //           3 | b c d |
            // C   = (-1)  | j k l | = - ( b ( kp - lo ) - c ( jp - ln ) + d ( jo - kn ) )
            //  21         | n o p |
            //
            //           4 | a c d |
            // C   = (-1)  | i k l | = + ( a ( kp - lo ) - c ( ip - lm ) + d ( io - km ) )
            //  22         | m o p |
            //
            //           5 | a b d |
            // C   = (-1)  | i j l | = - ( a ( jp - ln ) - b ( ip - lm ) + d ( in - jm ) )
            //  23         | m n p |
            //
            //           6 | a b c |
            // C   = (-1)  | i j k | = + ( a ( jo - kn ) - b ( io - km ) + c ( in - jm ) )
            //  24         | m n o |
            //
            // Third Row
            //           4 | b c d |
            // C   = (-1)  | f g h | = + ( b ( gp - ho ) - c ( fp - hn ) + d ( fo - gn ) )
            //  31         | n o p |
            //
            //           5 | a c d |
            // C   = (-1)  | e g h | = - ( a ( gp - ho ) - c ( ep - hm ) + d ( eo - gm ) )
            //  32         | m o p |
            //
            //           6 | a b d |
            // C   = (-1)  | e f h | = + ( a ( fp - hn ) - b ( ep - hm ) + d ( en - fm ) )
            //  33         | m n p |
            //
            //           7 | a b c |
            // C   = (-1)  | e f g | = - ( a ( fo - gn ) - b ( eo - gm ) + c ( en - fm ) )
            //  34         | m n o |
            //
            // Fourth Row
            //           5 | b c d |
            // C   = (-1)  | f g h | = - ( b ( gl - hk ) - c ( fl - hj ) + d ( fk - gj ) )
            //  41         | j k l |
            //
            //           6 | a c d |
            // C   = (-1)  | e g h | = + ( a ( gl - hk ) - c ( el - hi ) + d ( ek - gi ) )
            //  42         | i k l |
            //
            //           7 | a b d |
            // C   = (-1)  | e f h | = - ( a ( fl - hj ) - b ( el - hi ) + d ( ej - fi ) )
            //  43         | i j l |
            //
            //           8 | a b c |
            // C   = (-1)  | e f g | = + ( a ( fk - gj ) - b ( ek - gi ) + c ( ej - fi ) )
            //  44         | i j k |
            //
            // Cost of operation
            // 53 adds, 104 muls, and 1 div.
            auto a = M11, b = M12, c = M13, d = M14;
            auto e = M21, f = M22, g = M23, h = M24;
            auto i = M31, j = M32, k = M33, l = M34;
            auto m = M41, n = M42, o = M43, p = M44;

            auto kp_lo = k * p - l * o;
            auto jp_ln = j * p - l * n;
            auto jo_kn = j * o - k * n;
            auto ip_lm = i * p - l * m;
            auto io_km = i * o - k * m;
            auto in_jm = i * n - j * m;

            auto a11 = +(f * kp_lo - g * jp_ln + h * jo_kn);
            auto a12 = -(e * kp_lo - g * ip_lm + h * io_km);
            auto a13 = +(e * jp_ln - f * ip_lm + h * in_jm);
            auto a14 = -(e * jo_kn - f * io_km + g * in_jm);

            auto det = a * a11 + b * a12 + c * a13 + d * a14;

            if (std::fabs(det) < std::numeric_limits<T>::epsilon()) { return std::nullopt; }

            auto invDet = 1 / det;

            Matrix4x4<T> result;

            result.M11 = a11 * invDet;
            result.M21 = a12 * invDet;
            result.M31 = a13 * invDet;
            result.M41 = a14 * invDet;

            result.M12 = -(b * kp_lo - c * jp_ln + d * jo_kn) * invDet;
            result.M22 = +(a * kp_lo - c * ip_lm + d * io_km) * invDet;
            result.M32 = -(a * jp_ln - b * ip_lm + d * in_jm) * invDet;
            result.M42 = +(a * jo_kn - b * io_km + c * in_jm) * invDet;

            auto gp_ho = g * p - h * o;
            auto fp_hn = f * p - h * n;
            auto fo_gn = f * o - g * n;
            auto ep_hm = e * p - h * m;
            auto eo_gm = e * o - g * m;
            auto en_fm = e * n - f * m;

            result.M13 = +(b * gp_ho - c * fp_hn + d * fo_gn) * invDet;
            result.M23 = -(a * gp_ho - c * ep_hm + d * eo_gm) * invDet;
            result.M33 = +(a * fp_hn - b * ep_hm + d * en_fm) * invDet;
            result.M43 = -(a * fo_gn - b * eo_gm + c * en_fm) * invDet;

            auto gl_hk = g * l - h * k;
            auto fl_hj = f * l - h * j;
            auto fk_gj = f * k - g * j;
            auto el_hi = e * l - h * i;
            auto ek_gi = e * k - g * i;
            auto ej_fi = e * j - f * i;

            result.M14 = -(b * gl_hk - c * fl_hj + d * fk_gj) * invDet;
            result.M24 = +(a * gl_hk - c * el_hi + d * ek_gi) * invDet;
            result.M34 = -(a * fl_hj - b * el_hi + d * ej_fi) * invDet;
            result.M44 = +(a * fk_gj - b * ek_gi + c * ej_fi) * invDet;

            return result;
        }
        inline Vector3<T> extractDirectScale() const {
            return Vector3<T>(
                row0().length() * (M11 > 0 ? 1 : -1),
                row1().length() * (M22 > 0 ? 1 : -1),
                row2().length() * (M33 > 0 ? 1 : -1));
        }
        inline std::array<T, 16> values() const {
            return
            {
                M11, M12, M13, M14,
                M21, M22, M23, M24,
                M31, M32, M33, M34,
                M41, M42, M43, M44
            };
        }
        inline static std::vector<T> values(const std::vector<Matrix4x4<T>>& matrixArray) {
            std::vector<T> ret(matrixArray.size() * 16);
            for (std::size_t i = 0; i < matrixArray.size(); i++) {
                std::size_t j = i * 16;
                ret[j + 0] = matrixArray[i].M11;
                ret[j + 1] = matrixArray[i].M12;
                ret[j + 2] = matrixArray[i].M13;
                ret[j + 3] = matrixArray[i].M14;
                ret[j + 4] = matrixArray[i].M21;
                ret[j + 5] = matrixArray[i].M22;
                ret[j + 6] = matrixArray[i].M23;
                ret[j + 7] = matrixArray[i].M24;
                ret[j + 8] = matrixArray[i].M31;
                ret[j + 9] = matrixArray[i].M32;
                ret[j + 10] = matrixArray[i].M33;
                ret[j + 11] = matrixArray[i].M34;
                ret[j + 12] = matrixArray[i].M41;
                ret[j + 13] = matrixArray[i].M42;
                ret[j + 14] = matrixArray[i].M43;
                ret[j + 15] = matrixArray[i].M44;
            }
            return ret;
        }
        inline Ray<T> rayFromProjectionMatrix(const Vector2<T>& normalisedScreenCoordinates) {
            auto invProjection = inverse();

            auto invertedY = Vector2<T>(normalisedScreenCoordinates.X, 1.0f - normalisedScreenCoordinates.Y);
            auto scalesNormalisedScreenCoordinates = invertedY * 2.0f - 1.0f;

            auto p0 = Vector4<T>(scalesNormalisedScreenCoordinates, 0.0f, 1.0f);
            auto p1 = Vector4<T>(scalesNormalisedScreenCoordinates, 1.0f, 1.0f);

            p0 = transform(p0, invProjection);
            p1 = transform(p1, invProjection);

            p0 = p0 / p0.W;
            p1 = p1 / p1.W;

            auto ret = Ray<T>(p0.xyz(), (p1 - p0).xyz().normalize());
            return ret;
        }
        inline Matrix4x4<T> inverse() const {
            auto inv = invert();
            if (!inv.has_value()) { throw std::exception("No inversion of matrix available"); }
            return inv.value();
        }
        inline Matrix4x4<T> transform(const Quaternion<T>& rotation) const {
            // Compute rotation matrix.
            auto x2 = rotation.X + rotation.X;
            auto y2 = rotation.Y + rotation.Y;
            auto z2 = rotation.Z + rotation.Z;

            auto wx2 = rotation.W * x2;
            auto wy2 = rotation.W * y2;
            auto wz2 = rotation.W * z2;
            auto xx2 = rotation.X * x2;
            auto xy2 = rotation.X * y2;
            auto xz2 = rotation.X * z2;
            auto yy2 = rotation.Y * y2;
            auto yz2 = rotation.Y * z2;
            auto zz2 = rotation.Z * z2;

            auto q11 = 1 - yy2 - zz2;
            auto q21 = xy2 - wz2;
            auto q31 = xz2 + wy2;

            auto q12 = xy2 + wz2;
            auto q22 = 1 - xx2 - zz2;
            auto q32 = yz2 - wx2;

            auto q13 = xz2 - wy2;
            auto q23 = yz2 + wx2;
            auto q33 = 1 - xx2 - yy2;

            Matrix4x4<T> result;

            // First row
            result.M11 = M11 * q11 + M12 * q21 + M13 * q31;
            result.M12 = M11 * q12 + M12 * q22 + M13 * q32;
            result.M13 = M11 * q13 + M12 * q23 + M13 * q33;
            result.M14 = M14;

            // Second row
            result.M21 = M21 * q11 + M22 * q21 + M23 * q31;
            result.M22 = M21 * q12 + M22 * q22 + M23 * q32;
            result.M23 = M21 * q13 + M22 * q23 + M23 * q33;
            result.M24 = M24;

            // Third row
            result.M31 = M31 * q11 + M32 * q21 + M33 * q31;
            result.M32 = M31 * q12 + M32 * q22 + M33 * q32;
            result.M33 = M31 * q13 + M32 * q23 + M33 * q33;
            result.M34 = M34;

            // Fourth row
            result.M41 = M41 * q11 + M42 * q21 + M43 * q31;
            result.M42 = M41 * q12 + M42 * q22 + M43 * q32;
            result.M43 = M41 * q13 + M42 * q23 + M43 * q33;
            result.M44 = M44;

            return result;
        }
        inline Matrix4x4<T> transform(const Matrix4x4<T>& mat) const { return (*this) * mat; }
        inline Quaternion<T> quaternion() const {
            auto trace = M11 + M22 + M33;
            if (trace > 0) {
                auto s = std::sqrt(trace + 1);
                auto w = s * 0.5;
                s = 0.5 / s;
                return Quaternion<T>(
                    (M23 - M32) * s,
                    (M31 - M13) * s,
                    (M12 - M21) * s,
                    w);
            }
            if (M11 >= M22 && M11 >= M33) {
                auto s = std::sqrt(1 + M11 - M22 - M33);
                auto invS = 0.5 / s;
                return Quaternion<T>(0.5 * s,
                    (M12 + M21) * invS,
                    (M13 + M31) * invS,
                    (M23 - M32) * invS);
            }
            if (M22 > M33) {
                auto s = std::sqrt(1 + M22 - M11 - M33);
                auto invS = 0.5 / s;
                return Quaternion<T>(
                    (M21 + M12) * invS,
                    0.5 * s,
                    (M32 + M23) * invS,
                    (M31 - M13) * invS);
            }
            {
                auto s = std::sqrt(1 + M33 - M11 - M22);
                auto invS = 0.5 / s;
                return Quaternion<T>(
                    (M31 + M13) * invS,
                    (M32 + M23) * invS,
                    0.5 * s,
                    (M12 - M21) * invS);
            }
        }
        inline bool decompose(Vector3<T>& scale, Quaternion<T>& rotation, Vector3<T>& trans) const {
            bool result = true;
            const T EPSILON = 0.0001f;
            Vector3<T> pCanonicalBasis[3]{ Vector3<T>::unitX(), Vector3<T>::unitY(), Vector3<T>::unitZ() };
            Vector3<T> pVectorBasis[3]{ row0(), row1(), row2() };
            T pfScales[3]{ pVectorBasis[0].length(), pVectorBasis[1].length(), pVectorBasis[2].length() };

            unsigned int a, b, c;
            T x = pfScales[0], y = pfScales[1], z = pfScales[2];
            if (x < y) {
                if (y < z) { a = 2; b = 1; c = 0; }
                else {
                    a = 1;
                    if (x < z) { b = 2; c = 0; }
                    else { b = 0; c = 2; }
                }
            }
            else {
                if (x < z) { a = 2; b = 0; c = 1; }
                else {
                    a = 0;
                    if (y < z) { b = 2; c = 1; }
                    else { b = 1; c = 2; }
                }
            }
            if (pfScales[a] < EPSILON) { pVectorBasis[a] = pCanonicalBasis[a]; }
            pVectorBasis[a] = pVectorBasis[a].normalize();

            if (pfScales[b] < EPSILON) {
                auto fAbsX = std::fabs(pVectorBasis[a].X);
                auto fAbsY = std::fabs(pVectorBasis[a].Y);
                auto fAbsZ = std::fabs(pVectorBasis[a].Z);

                auto cc = 0;
                if (fAbsX < fAbsY) {
                    if (fAbsY < fAbsZ) { cc = 0; }
                    else {
                        if (fAbsX < fAbsZ) { cc = 0; }
                        else { cc = 2; }
                    }
                }
                else {
                    if (fAbsX < fAbsZ) { cc = 1; }
                    else {
                        if (fAbsY < fAbsZ) { cc = 1; }
                        else { cc = 2; }
                    }
                }
                pVectorBasis[b] = pVectorBasis[a].cross(pCanonicalBasis[cc]);
            }

            pVectorBasis[b] = pVectorBasis[b].normalize();
            if (pfScales[c] < EPSILON) { pVectorBasis[c] = pVectorBasis[a].cross(pVectorBasis[b]); }
            pVectorBasis[c] = pVectorBasis[c].normalize();

            // Update mat tmp;
            auto det = Matrix4x4<T>(pVectorBasis[0], pVectorBasis[1], pVectorBasis[2]).getDeterminant();
            // use Kramer's rule to check for handedness of coordinate system
            if (det < 0) {
                // switch coordinate system by negating the scale and inverting the basis vector on the x-axis
                pfScales[a] = -pfScales[a];
                pVectorBasis[a] = -pVectorBasis[a];
                det = -det;
            }

            det -= 1;
            det *= det;

            if (EPSILON < det) {
                // Non-SRT matrix encountered
                rotation = Quaternion<T>::identity();
                result = false;
            }
            else {
                // generate the quaternion from the matrix
                rotation = Matrix4x4<T>(pVectorBasis[0], pVectorBasis[1], pVectorBasis[2]).quaternion();
            }
            trans = translation();
            scale = Vector3<T>(pfScales[0], pfScales[1], pfScales[2]);
            return result;
        }
        inline static bool decompose(const Matrix4x4<T>& m, Vector3<T>& scale, Quaternion<T>& rotation, Vector3<T>& trans) { return m.decompose(scale, rotation, trans); }
        inline Matrix4x4<T> transpose() const {
            Matrix4x4<T> result;

            result.M11 = M11;
            result.M12 = M21;
            result.M13 = M31;
            result.M14 = M41;
            result.M21 = M12;
            result.M22 = M22;
            result.M23 = M32;
            result.M24 = M42;
            result.M31 = M13;
            result.M32 = M23;
            result.M33 = M33;
            result.M34 = M43;
            result.M41 = M14;
            result.M42 = M24;
            result.M43 = M34;
            result.M44 = M44;

            return result;
        }
        inline static Matrix4x4<T> transpose(const Matrix4x4<T>& m) { return m.transpose(); }
        inline Matrix4x4<T> scaleTranslation(T amount) { return setTranslation(translation() * amount); }
        inline Matrix4x4<T> lerp(const Matrix4x4<T>& matrix2, T amount) const {
            Matrix4x4<T> result;

            // First row
            result.M11 = M11 + (matrix2.M11 - M11) * amount;
            result.M12 = M12 + (matrix2.M12 - M12) * amount;
            result.M13 = M13 + (matrix2.M13 - M13) * amount;
            result.M14 = M14 + (matrix2.M14 - M14) * amount;

            // Second row
            result.M21 = M21 + (matrix2.M21 - M21) * amount;
            result.M22 = M22 + (matrix2.M22 - M22) * amount;
            result.M23 = M23 + (matrix2.M23 - M23) * amount;
            result.M24 = M24 + (matrix2.M24 - M24) * amount;

            // Third row
            result.M31 = M31 + (matrix2.M31 - M31) * amount;
            result.M32 = M32 + (matrix2.M32 - M32) * amount;
            result.M33 = M33 + (matrix2.M33 - M33) * amount;
            result.M34 = M34 + (matrix2.M34 - M34) * amount;

            // Fourth row
            result.M41 = M41 + (matrix2.M41 - M41) * amount;
            result.M42 = M42 + (matrix2.M42 - M42) * amount;
            result.M43 = M43 + (matrix2.M43 - M43) * amount;
            result.M44 = M44 + (matrix2.M44 - M44) * amount;

            return result;
        }
        inline static Matrix4x4<T> lerp(const Matrix4x4<T>& matrix1, const Matrix4x4<T>& matrix2, T amount) { return matrix1.lerp(matrix2, amount); }
        inline Matrix4x4<T> negate() const { return -(*this); }
        inline Matrix4x4<T> add(const Matrix4x4<T>& value2) const { return (*this) + value2; }
        inline Matrix4x4<T> subtract(const Matrix4x4<T>& value2) const { return (*this) - value2; }
        inline Matrix4x4<T> multiply(const Matrix4x4<T>& value2) { return (*this) * value2; }
        inline Matrix4x4<T> multiply(T value2) { return (*this) * value2; }
        inline static Matrix4x4<T> multiply(const std::initializer_list<Matrix4x4<T>>& matrices) {
            auto result = Matrix4x4<T>::identity();
            for (const auto& m : matrices) { result = result * m; }
            return result;
        }

        inline std::size_t hash() { return hash::combine({ M11, M12, M13, M14, M21, M22, M23, M24, M31, M32, M33, M34, M41, M42, M43, M44 }); }

        inline friend Matrix4x4<T> operator -(const Matrix4x4<T>& value) {
            Matrix4x4<T> m;

            m.M11 = -value.M11;
            m.M12 = -value.M12;
            m.M13 = -value.M13;
            m.M14 = -value.M14;
            m.M21 = -value.M21;
            m.M22 = -value.M22;
            m.M23 = -value.M23;
            m.M24 = -value.M24;
            m.M31 = -value.M31;
            m.M32 = -value.M32;
            m.M33 = -value.M33;
            m.M34 = -value.M34;
            m.M41 = -value.M41;
            m.M42 = -value.M42;
            m.M43 = -value.M43;
            m.M44 = -value.M44;

            return m;
        }
        inline friend Matrix4x4<T> operator+(const Matrix4x4<T>& value1, const Matrix4x4<T>& value2) {
            Matrix4x4<T> m;

            m.M11 = value1.M11 + value2.M11;
            m.M12 = value1.M12 + value2.M12;
            m.M13 = value1.M13 + value2.M13;
            m.M14 = value1.M14 + value2.M14;
            m.M21 = value1.M21 + value2.M21;
            m.M22 = value1.M22 + value2.M22;
            m.M23 = value1.M23 + value2.M23;
            m.M24 = value1.M24 + value2.M24;
            m.M31 = value1.M31 + value2.M31;
            m.M32 = value1.M32 + value2.M32;
            m.M33 = value1.M33 + value2.M33;
            m.M34 = value1.M34 + value2.M34;
            m.M41 = value1.M41 + value2.M41;
            m.M42 = value1.M42 + value2.M42;
            m.M43 = value1.M43 + value2.M43;
            m.M44 = value1.M44 + value2.M44;

            return m;
        }
        inline friend Matrix4x4<T> operator-(const Matrix4x4<T>& value1, const Matrix4x4<T>& value2) {
            Matrix4x4<T> m;

            m.M11 = value1.M11 - value2.M11;
            m.M12 = value1.M12 - value2.M12;
            m.M13 = value1.M13 - value2.M13;
            m.M14 = value1.M14 - value2.M14;
            m.M21 = value1.M21 - value2.M21;
            m.M22 = value1.M22 - value2.M22;
            m.M23 = value1.M23 - value2.M23;
            m.M24 = value1.M24 - value2.M24;
            m.M31 = value1.M31 - value2.M31;
            m.M32 = value1.M32 - value2.M32;
            m.M33 = value1.M33 - value2.M33;
            m.M34 = value1.M34 - value2.M34;
            m.M41 = value1.M41 - value2.M41;
            m.M42 = value1.M42 - value2.M42;
            m.M43 = value1.M43 - value2.M43;
            m.M44 = value1.M44 - value2.M44;

            return m;
        }
        inline friend Matrix4x4<T> operator *(const Matrix4x4<T>& value1, const Matrix4x4<T>& value2) {
            Matrix4x4<T> m;

            // First row
            m.M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21 + value1.M13 * value2.M31 + value1.M14 * value2.M41;
            m.M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22 + value1.M13 * value2.M32 + value1.M14 * value2.M42;
            m.M13 = value1.M11 * value2.M13 + value1.M12 * value2.M23 + value1.M13 * value2.M33 + value1.M14 * value2.M43;
            m.M14 = value1.M11 * value2.M14 + value1.M12 * value2.M24 + value1.M13 * value2.M34 + value1.M14 * value2.M44;

            // Second row
            m.M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21 + value1.M23 * value2.M31 + value1.M24 * value2.M41;
            m.M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22 + value1.M23 * value2.M32 + value1.M24 * value2.M42;
            m.M23 = value1.M21 * value2.M13 + value1.M22 * value2.M23 + value1.M23 * value2.M33 + value1.M24 * value2.M43;
            m.M24 = value1.M21 * value2.M14 + value1.M22 * value2.M24 + value1.M23 * value2.M34 + value1.M24 * value2.M44;

            // Third row
            m.M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value1.M33 * value2.M31 + value1.M34 * value2.M41;
            m.M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value1.M33 * value2.M32 + value1.M34 * value2.M42;
            m.M33 = value1.M31 * value2.M13 + value1.M32 * value2.M23 + value1.M33 * value2.M33 + value1.M34 * value2.M43;
            m.M34 = value1.M31 * value2.M14 + value1.M32 * value2.M24 + value1.M33 * value2.M34 + value1.M34 * value2.M44;

            // Fourth row
            m.M41 = value1.M41 * value2.M11 + value1.M42 * value2.M21 + value1.M43 * value2.M31 + value1.M44 * value2.M41;
            m.M42 = value1.M41 * value2.M12 + value1.M42 * value2.M22 + value1.M43 * value2.M32 + value1.M44 * value2.M42;
            m.M43 = value1.M41 * value2.M13 + value1.M42 * value2.M23 + value1.M43 * value2.M33 + value1.M44 * value2.M43;
            m.M44 = value1.M41 * value2.M14 + value1.M42 * value2.M24 + value1.M43 * value2.M34 + value1.M44 * value2.M44;

            return m;
        }
        inline friend Matrix4x4<T> operator *(const Matrix4x4<T>& value1, T value2) {
            Matrix4x4<T> m;

            m.M11 = value1.M11 * value2;
            m.M12 = value1.M12 * value2;
            m.M13 = value1.M13 * value2;
            m.M14 = value1.M14 * value2;
            m.M21 = value1.M21 * value2;
            m.M22 = value1.M22 * value2;
            m.M23 = value1.M23 * value2;
            m.M24 = value1.M24 * value2;
            m.M31 = value1.M31 * value2;
            m.M32 = value1.M32 * value2;
            m.M33 = value1.M33 * value2;
            m.M34 = value1.M34 * value2;
            m.M41 = value1.M41 * value2;
            m.M42 = value1.M42 * value2;
            m.M43 = value1.M43 * value2;
            m.M44 = value1.M44 * value2;
            return m;
        }

        inline friend bool operator ==(const Matrix4x4<T>& value1, const Matrix4x4<T>& value2) {
            return (value1.M11 == value2.M11 && value1.M22 == value2.M22 && value1.M33 == value2.M33 && value1.M44 == value2.M44 && // Check diagonal element first for early out.
                value1.M12 == value2.M12 && value1.M13 == value2.M13 && value1.M14 == value2.M14 && value1.M21 == value2.M21 &&
                value1.M23 == value2.M23 && value1.M24 == value2.M24 && value1.M31 == value2.M31 && value1.M32 == value2.M32 &&
                value1.M34 == value2.M34 && value1.M41 == value2.M41 && value1.M42 == value2.M42 && value1.M43 == value2.M43);
        }
        inline friend bool operator !=(const Matrix4x4<T>& value1, const Matrix4x4<T>& value2) {
            return (value1.M11 != value2.M11 || value1.M12 != value2.M12 || value1.M13 != value2.M13 || value1.M14 != value2.M14 ||
                value1.M21 != value2.M21 || value1.M22 != value2.M22 || value1.M23 != value2.M23 || value1.M24 != value2.M24 ||
                value1.M31 != value2.M31 || value1.M32 != value2.M32 || value1.M33 != value2.M33 || value1.M34 != value2.M34 ||
                value1.M41 != value2.M41 || value1.M42 != value2.M42 || value1.M43 != value2.M43 || value1.M44 != value2.M44);
        }
        inline friend std::ostream& operator<<(std::ostream& out, const Matrix4x4<T>& v) { return (out << "{{ {{M11:" << v.M11 << " M12:" << v.M12 << " M13:" << v.M13 << " M14:" << v.M14 << "}} {{M21:" << v.M21 << " M22:" << v.M22 << " M23:" << v.M23 << " M24:" << v.M24 << "}} {{M31:" << v.M31 << " M32:" << v.M32 << " M33:" << v.M33 << " M34:" << v.M34 << "}} {{M41:" << v.M41 << " M42:" << v.M42 << " M43:" << v.M43 << " M44:" << v.M44 << "}}} }}"); }
    };

#define FMatrix4x4 Matrix4x4<float>
#define DMatrix4x4 Matrix4x4<double>

    template <typename T = float> inline static Vector3<T> rotate(const Vector3<T>& self, const Vector3<T>& axis, T angle) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::fromAxisAngle(axis, angle)); }

    template <typename T = float> inline static ContainmentType contains(const Sphere<T>& s, const Vector3<T>& point) { return s.contains(point); }
    template <typename T = float> inline static ContainmentType contains(const Sphere<T>& s, const AABox<T>& box) { return AABox<T>::contains(s, box); }
    template <typename T = float> inline static ContainmentType contains(const Sphere<T>& s, const Sphere<T>& sphere) { return s.contains(sphere); }
    template <typename T = float> inline static ContainmentType contains(const AABox<T>& a, const AABox<T>& box) { return a.contains(box); }
    template <typename T = float> inline static ContainmentType contains(const AABox<T>& a, const Sphere<T>& sphere) { return a.contains(sphere); }
    template <typename T = float> inline static bool contains(const AABox<T>& a, const Vector3<T>& point) { return a.contains(point); }
    template <typename T = float> inline static ContainmentType contains(const AABox2D<T>& a, const AABox2D<T>& box) { return a.contains(box); }
    template <typename T = float> inline static bool contains(const AABox2D<T>& a, const Vector2<T>& point) { return a.contains(point); }
    template <typename T = float> inline static bool contains(const Triangle2D<T>& a, const Vector2<T>& point) { return a.contains(point); }

    template <typename T = float> inline static bool intersects(const Sphere<T>& s, const Sphere<T>& sphere) { return s.intersects(sphere); }
    template <typename T = float> inline static bool intersects(const Sphere<T>& sphere, const AABox<T>& box) { return box.intersects(sphere); }
    template <typename T = float> inline static PlaneIntersectionType intersects(const Sphere<T>& s, const Plane<T>& plane) { return s.intersects(plane); };
    template <typename T = float> inline static std::optional<T> intersects(const Sphere<T>& s, const Ray<T>& ray) { return ray.intersects(s); }
    template <typename T = float> inline static bool intersects(const AABox<T>& a, const AABox<T>& box) { return a.intersects(box); }
    template <typename T = float> inline static bool intersects(const AABox<T>& a, const Sphere<T>& sphere) { return intersects(a, sphere); }
    template <typename T = float> inline static PlaneIntersectionType intersects(const AABox<T>& a, const Plane<T>& plane) { return a.intersects(plane); }
    template <typename T = float> inline static bool intersects(const AABox2D<T>& b, const AABox2D<T>& box) { return b.intersects(box); }
    template <typename T = float> inline bool intersects(const Line2D<T>& l, const AABox2D<T>& thisBox, const Line2D<T>& otherLine, const AABox2D<T>& otherBox) { return l.intersects(thisBox, otherLine, otherBox); }
    template <typename T = float> inline static std::optional<T> intersects(const Ray<T>& ray, const AABox<T>& box) { return ray.intersects(box); }
    template <typename T = float> inline static std::optional<T> intersects(const Ray<T>& ray, const Plane<T>& plane, float tolerance = constants::tolerance) { return ray.intersects(plane, tolerance); }
    template <typename T = float> inline static std::optional<T> intersects(const Ray<T>& ray, const Sphere<T>& sphere) { return ray.intersects(sphere); }
    template <typename T = float> inline static std::optional<T> intersects(const Ray<T>& ray, const Triangle<T>& tri, float tolerance = constants::tolerance) { return ray.intersects(tri, tolerance); }

    template <typename T = float> inline static AABox<T> transform(const AABox<T>& self, const std::initializer_list<Matrix4x4<T>>& matrices) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::multiply(matrices)); }
    template <typename T = float> inline static Vector4<T> transform(const Vector4<T>& self, const std::initializer_list<Matrix4x4<T>>& matrices) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::multiply(matrices)); }
    template <typename T = float> inline static Vector3<T> transform(const Vector3<T>& self, const std::initializer_list<Matrix4x4<T>>& matrices) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::multiply(matrices)); }
    template <typename T = float> inline static Line<T> transform(const Line<T>& self, const std::initializer_list<Matrix4x4<T>>& matrices) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::multiply(matrices)); }
    template <typename T = float> inline static Triangle<T> transform(const Triangle<T>& self, const std::initializer_list<Matrix4x4<T>>& matrices) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::multiply(matrices)); }
    template <typename T = float> inline static Sphere<T> transform(const Sphere<T>& self, const std::initializer_list<Matrix4x4<T>>& matrices) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::multiply(matrices)); }
    template <typename T = float> inline static Ray<T> transform(const Ray<T>& self, const std::initializer_list<Matrix4x4<T>>& matrices) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::multiply(matrices)); }
    template <typename T = float> inline static Quad<T> transform(const Quad<T>& self, const std::initializer_list<Matrix4x4<T>>& matrices) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::multiply(matrices)); }
    template <typename T = float> inline static Plane<T> transform(const Plane<T>& self, const std::initializer_list<Matrix4x4<T>>& matrices) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::multiply(matrices)); }
    template <typename T = float> inline static Matrix4x4<T> transform(const Matrix4x4<T>& self, const std::initializer_list<Matrix4x4<T>>& matrices) { return self.transform(Matrix4x4<T>::multiply(matrices)); }

    template <typename T = float> inline static AABox<T> translate(const AABox<T>& self, const Vector3<T>& offset) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::translation(offset)); }
    template <typename T = float> inline static Vector4<T> translate(const Vector4<T>& self, const Vector3<T>& offset) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::translation(offset)); }
    template <typename T = float> inline static Vector3<T> translate(const Vector3<T>& self, const Vector3<T>& offset) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::translation(offset)); }
    template <typename T = float> inline static Line<T> translate(const Line<T>& self, const Vector3<T>& offset) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::translation(offset)); }
    template <typename T = float> inline static Triangle<T> translate(const Triangle<T>& self, const Vector3<T>& offset) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::translation(offset)); }
    template <typename T = float> inline static Sphere<T> translate(const Sphere<T>& self, const Vector3<T>& offset) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::translation(offset)); }
    template <typename T = float> inline static Ray<T> translate(const Ray<T>& self, const Vector3<T>& offset) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::translation(offset)); }
    template <typename T = float> inline static Quad<T> translate(const Quad<T>& self, const Vector3<T>& offset) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::translation(offset)); }
    template <typename T = float> inline static Plane<T> translate(const Plane<T>& self, const Vector3<T>& offset) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::translation(offset)); }
    template <typename T = float> inline static Matrix4x4<T> translate(const Matrix4x4<T>& self, const Vector3<T>& offset) { return self.transform(Matrix4x4<T>::translation(offset)); }

    template <typename T = float> inline static AABox<T> translate(const AABox<T>& self, T x, T y, T z) { return translate(self, Vector3<T>(x, y, z)); }
    template <typename T = float> inline static Vector4<T> translate(const Vector4<T>& self, T x, T y, T z) { return translate(self, Vector3<T>(x, y, z)); }
    template <typename T = float> inline static Vector3<T> translate(const Vector3<T>& self, T x, T y, T z) { return translate(self, Vector3<T>(x, y, z)); }
    template <typename T = float> inline static Line<T> translate(const Line<T>& self, T x, T y, T z) { return translate(self, Vector3<T>(x, y, z)); }
    template <typename T = float> inline static Triangle<T> translate(const Triangle<T>& self, T x, T y, T z) { return translate(self, Vector3<T>(x, y, z)); }
    template <typename T = float> inline static Sphere<T> translate(const Sphere<T>& self, T x, T y, T z) { return translate(self, Vector3<T>(x, y, z)); }
    template <typename T = float> inline static Ray<T> translate(const Ray<T>& self, T x, T y, T z) { return translate(self, Vector3<T>(x, y, z)); }
    template <typename T = float> inline static Quad<T> translate(const Quad<T>& self, T x, T y, T z) { return translate(self, Vector3<T>(x, y, z)); }
    template <typename T = float> inline static Plane<T> translate(const Plane<T>& self, T x, T y, T z) { return translate(self, Vector3<T>(x, y, z)); }
    template <typename T = float> inline static Matrix4x4<T> translate(const Matrix4x4<T>& self, T x, T y, T z) { return translate(self, Vector3<T>(x, y, z)); }

    template <typename T = float> inline static AABox<T> rotate(const AABox<T>& self, const Quaternion<T>& q) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::rotation(q)); }
    template <typename T = float> inline static Vector4<T> rotate(const Vector4<T>& self, const Quaternion<T>& q) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::rotation(q)); }
    template <typename T = float> inline static Vector3<T> rotate(const Vector3<T>& self, const Quaternion<T>& q) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::rotation(q)); }
    template <typename T = float> inline static Line<T> rotate(const Line<T>& self, const Quaternion<T>& q) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::rotation(q)); }
    template <typename T = float> inline static Triangle<T> rotate(const Triangle<T>& self, const Quaternion<T>& q) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::rotation(q)); }
    template <typename T = float> inline static Sphere<T> rotate(const Sphere<T>& self, const Quaternion<T>& q) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::rotation(q)); }
    template <typename T = float> inline static Ray<T> rotate(const Ray<T>& self, const Quaternion<T>& q) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::rotation(q)); }
    template <typename T = float> inline static Quad<T> rotate(const Quad<T>& self, const Quaternion<T>& q) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::rotation(q)); }
    template <typename T = float> inline static Plane<T> rotate(const Plane<T>& self, const Quaternion<T>& q) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::rotation(q)); }
    template <typename T = float> inline static Matrix4x4<T> rotate(const Matrix4x4<T>& self, const Quaternion<T>& q) { return self.transform(Matrix4x4<T>::rotation(q)); }

    template <typename T = float> inline static AABox<T> scale(const AABox<T>& self, const Vector3<T>& scales) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::scale(scales)); }
    template <typename T = float> inline static Vector4<T> scale(const Vector4<T>& self, const Vector3<T>& scales) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::scale(scales)); }
    template <typename T = float> inline static Vector3<T> scale(const Vector3<T>& self, const Vector3<T>& scales) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::scale(scales)); }
    template <typename T = float> inline static Line<T> scale(const Line<T>& self, const Vector3<T>& scales) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::scale(scales)); }
    template <typename T = float> inline static Triangle<T> scale(const Triangle<T>& self, const Vector3<T>& scales) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::scale(scales)); }
    template <typename T = float> inline static Sphere<T> scale(const Sphere<T>& self, const Vector3<T>& scales) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::scale(scales)); }
    template <typename T = float> inline static Ray<T> scale(const Ray<T>& self, const Vector3<T>& scales) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::scale(scales)); }
    template <typename T = float> inline static Quad<T> scale(const Quad<T>& self, const Vector3<T>& scales) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::scale(scales)); }
    template <typename T = float> inline static Plane<T> scale(const Plane<T>& self, const Vector3<T>& scales) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::scale(scales)); }
    template <typename T = float> inline static Matrix4x4<T> scale(const Matrix4x4<T>& self, const Vector3<T>& scales) { return self.transform(Matrix4x4<T>::scale(scales)); }

    template <typename T = float> inline static AABox<T> scale(const AABox<T>& self, T s) { return scale(self, Vector3<T>(s, s, s)); }
    template <typename T = float> inline static Vector4<T> scale(const Vector4<T>& self, T s) { return scale(self, Vector3<T>(s, s, s)); }
    template <typename T = float> inline static Vector3<T> scale(const Vector3<T>& self, T s) { return scale(self, Vector3<T>(s, s, s)); }
    template <typename T = float> inline static Line<T> scale(const Line<T>& self, T s) { return scale(self, Vector3<T>(s, s, s)); }
    template <typename T = float> inline static Triangle<T> scale(const Triangle<T>& self, T s) { return scale(self, Vector3<T>(s, s, s)); }
    template <typename T = float> inline static Sphere<T> scale(const Sphere<T>& self, T s) { return scale(self, Vector3<T>(s, s, s)); }
    template <typename T = float> inline static Ray<T> scale(const Ray<T>& self, T s) { return scale(self, Vector3<T>(s, s, s)); }
    template <typename T = float> inline static Quad<T> scale(const Quad<T>& self, T s) { return scale(self, Vector3<T>(s, s, s)); }
    template <typename T = float> inline static Plane<T> scale(const Plane<T>& self, T s) { return scale(self, Vector3<T>(s, s, s)); }
    template <typename T = float> inline static Matrix4x4<T> scale(const Matrix4x4<T>& self, T s) { return scale(self, Vector3<T>(s, s, s)); }

    template <typename T = float> inline static AABox<T> scale(const AABox<T>& self, T x, T y, T z) { return scale(self, Vector3<T>(x, y, z)); }
    template <typename T = float> inline static Vector4<T> scale(const Vector4<T>& self, T x, T y, T z) { return scale(self, Vector3<T>(x, y, z)); }
    template <typename T = float> inline static Vector3<T> scale(const Vector3<T>& self, T x, T y, T z) { return scale(self, Vector3<T>(x, y, z)); }
    template <typename T = float> inline static Line<T> scale(const Line<T>& self, T x, T y, T z) { return scale(self, Vector3<T>(x, y, z)); }
    template <typename T = float> inline static Triangle<T> scale(const Triangle<T>& self, T x, T y, T z) { return scale(self, Vector3<T>(x, y, z)); }
    template <typename T = float> inline static Sphere<T> scale(const Sphere<T>& self, T x, T y, T z) { return scale(self, Vector3<T>(x, y, z)); }
    template <typename T = float> inline static Ray<T> scale(const Ray<T>& self, T x, T y, T z) { return scale(self, Vector3<T>(x, y, z)); }
    template <typename T = float> inline static Quad<T> scale(const Quad<T>& self, T x, T y, T z) { return scale(self, Vector3<T>(x, y, z)); }
    template <typename T = float> inline static Plane<T> scale(const Plane<T>& self, T x, T y, T z) { return scale(self, Vector3<T>(x, y, z)); }
    template <typename T = float> inline static Matrix4x4<T> scale(const Matrix4x4<T>& self, T x, T y, T z) { return scale(self, Vector3<T>(x, y, z)); }

    template <typename T = float> inline static AABox<T> scaleX(const AABox<T>& self, T x) { return scale(self, x, 0, 0); }
    template <typename T = float> inline static Vector4<T> scaleX(const Vector4<T>& self, T x) { return scale(self, x, 0, 0); }
    template <typename T = float> inline static Vector3<T> scaleX(const Vector3<T>& self, T x) { return scale(self, x, 0, 0); }
    template <typename T = float> inline static Line<T> scaleX(const Line<T>& self, T x) { return scale(self, x, 0, 0); }
    template <typename T = float> inline static Triangle<T> scaleX(const Triangle<T>& self, T x) { return scale(self, x, 0, 0); }
    template <typename T = float> inline static Sphere<T> scaleX(const Sphere<T>& self, T x) { return scale(self, x, 0, 0); }
    template <typename T = float> inline static Ray<T> scaleX(const Ray<T>& self, T x) { return scale(self, x, 0, 0); }
    template <typename T = float> inline static Quad<T> scaleX(const Quad<T>& self, T x) { return scale(self, x, 0, 0); }
    template <typename T = float> inline static Plane<T> scaleX(const Plane<T>& self, T x) { return scale(self, x, 0, 0); }
    template <typename T = float> inline static Matrix4x4<T> scaleX(const Matrix4x4<T>& self, T x) { return scale(self, x, 0, 0); }

    template <typename T = float> inline static AABox<T> scaleY(const AABox<T>& self, T y) { return scale(self, 0, y, 0); }
    template <typename T = float> inline static Vector4<T> scaleY(const Vector4<T>& self, T y) { return scale(self, 0, y, 0); }
    template <typename T = float> inline static Vector3<T> scaleY(const Vector3<T>& self, T y) { return scale(self, 0, y, 0); }
    template <typename T = float> inline static Line<T> scaleY(const Line<T>& self, T y) { return scale(self, 0, y, 0); }
    template <typename T = float> inline static Triangle<T> scaleY(const Triangle<T>& self, T y) { return scale(self, 0, y, 0); }
    template <typename T = float> inline static Sphere<T> scaleY(const Sphere<T>& self, T y) { return scale(self, 0, y, 0); }
    template <typename T = float> inline static Ray<T> scaleY(const Ray<T>& self, T y) { return scale(self, 0, y, 0); }
    template <typename T = float> inline static Quad<T> scaleY(const Quad<T>& self, T y) { return scale(self, 0, y, 0); }
    template <typename T = float> inline static Plane<T> scaleY(const Plane<T>& self, T y) { return scale(self, 0, y, 0); }
    template <typename T = float> inline static Matrix4x4<T> scaleY(const Matrix4x4<T>& self, T y) { return scale(self, 0, y, 0); }

    template <typename T = float> inline static AABox<T> scaleZ(const AABox<T>& self, T z) { return scale(self, 0, 0, z); }
    template <typename T = float> inline static Vector4<T> scaleZ(const Vector4<T>& self, T z) { return scale(self, 0, 0, z); }
    template <typename T = float> inline static Vector3<T> scaleZ(const Vector3<T>& self, T z) { return scale(self, 0, 0, z); }
    template <typename T = float> inline static Line<T> scaleZ(const Line<T>& self, T z) { return scale(self, 0, 0, z); }
    template <typename T = float> inline static Triangle<T> scaleZ(const Triangle<T>& self, T z) { return scale(self, 0, 0, z); }
    template <typename T = float> inline static Sphere<T> scaleZ(const Sphere<T>& self, T z) { return scale(self, 0, 0, z); }
    template <typename T = float> inline static Ray<T> scaleZ(const Ray<T>& self, T z) { return scale(self, 0, 0, z); }
    template <typename T = float> inline static Quad<T> scaleZ(const Quad<T>& self, T z) { return scale(self, 0, 0, z); }
    template <typename T = float> inline static Plane<T> scaleZ(const Plane<T>& self, T z) { return scale(self, 0, 0, z); }
    template <typename T = float> inline static Matrix4x4<T> scaleZ(const Matrix4x4<T>& self, T z) { return scale(self, 0, 0, z); }

    template <typename T = float> inline static AABox<T> lookAt(const AABox<T>& self, const Vector3<T>& cameraPosition, const Vector3<T>& cameraTarget, const Vector3<T>& cameraUpVector) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::lookAt(cameraPosition, cameraTarget, cameraUpVector)); }
    template <typename T = float> inline static Vector4<T> lookAt(const Vector4<T>& self, const Vector3<T>& cameraPosition, const Vector3<T>& cameraTarget, const Vector3<T>& cameraUpVector) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::lookAt(cameraPosition, cameraTarget, cameraUpVector)); }
    template <typename T = float> inline static Vector3<T> lookAt(const Vector3<T>& self, const Vector3<T>& cameraPosition, const Vector3<T>& cameraTarget, const Vector3<T>& cameraUpVector) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::lookAt(cameraPosition, cameraTarget, cameraUpVector)); }
    template <typename T = float> inline static Line<T> lookAt(const Line<T>& self, const Vector3<T>& cameraPosition, const Vector3<T>& cameraTarget, const Vector3<T>& cameraUpVector) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::lookAt(cameraPosition, cameraTarget, cameraUpVector)); }
    template <typename T = float> inline static Triangle<T> lookAt(const Triangle<T>& self, const Vector3<T>& cameraPosition, const Vector3<T>& cameraTarget, const Vector3<T>& cameraUpVector) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::lookAt(cameraPosition, cameraTarget, cameraUpVector)); }
    template <typename T = float> inline static Sphere<T> lookAt(const Sphere<T>& self, const Vector3<T>& cameraPosition, const Vector3<T>& cameraTarget, const Vector3<T>& cameraUpVector) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::lookAt(cameraPosition, cameraTarget, cameraUpVector)); }
    template <typename T = float> inline static Ray<T> lookAt(const Ray<T>& self, const Vector3<T>& cameraPosition, const Vector3<T>& cameraTarget, const Vector3<T>& cameraUpVector) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::lookAt(cameraPosition, cameraTarget, cameraUpVector)); }
    template <typename T = float> inline static Quad<T> lookAt(const Quad<T>& self, const Vector3<T>& cameraPosition, const Vector3<T>& cameraTarget, const Vector3<T>& cameraUpVector) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::lookAt(cameraPosition, cameraTarget, cameraUpVector)); }
    template <typename T = float> inline static Plane<T> lookAt(const Plane<T>& self, const Vector3<T>& cameraPosition, const Vector3<T>& cameraTarget, const Vector3<T>& cameraUpVector) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::lookAt(cameraPosition, cameraTarget, cameraUpVector)); }
    template <typename T = float> inline static Matrix4x4<T> lookAt(const Matrix4x4<T>& self, const Vector3<T>& cameraPosition, const Vector3<T>& cameraTarget, const Vector3<T>& cameraUpVector) { return self.transform(Matrix4x4<T>::lookAt(cameraPosition, cameraTarget, cameraUpVector)); }

    template <typename T = float> inline static AABox<T> rotateAround(const AABox<T>& self, const Vector3<T>& axis, T angle) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::fromAxisAngle(axis, angle)); }
    template <typename T = float> inline static Vector4<T> rotateAround(const Vector4<T>& self, const Vector3<T>& axis, T angle) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::fromAxisAngle(axis, angle)); }
    template <typename T = float> inline static Vector3<T> rotateAround(const Vector3<T>& self, const Vector3<T>& axis, T angle) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::fromAxisAngle(axis, angle)); }
    template <typename T = float> inline static Line<T> rotateAround(const Line<T>& self, const Vector3<T>& axis, T angle) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::fromAxisAngle(axis, angle)); }
    template <typename T = float> inline static Triangle<T> rotateAround(const Triangle<T>& self, const Vector3<T>& axis, T angle) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::fromAxisAngle(axis, angle)); }
    template <typename T = float> inline static Sphere<T> rotateAround(const Sphere<T>& self, const Vector3<T>& axis, T angle) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::fromAxisAngle(axis, angle)); }
    template <typename T = float> inline static Ray<T> rotateAround(const Ray<T>& self, const Vector3<T>& axis, T angle) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::fromAxisAngle(axis, angle)); }
    template <typename T = float> inline static Quad<T> rotateAround(const Quad<T>& self, const Vector3<T>& axis, T angle) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::fromAxisAngle(axis, angle)); }
    template <typename T = float> inline static Plane<T> rotateAround(const Plane<T>& self, const Vector3<T>& axis, T angle) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::fromAxisAngle(axis, angle)); }
    template <typename T = float> inline static Matrix4x4<T> rotateAround(const Matrix4x4<T>& self, const Vector3<T>& axis, T angle) { return self.transform(Matrix4x4<T>::fromAxisAngle(axis, angle)); }

    template <typename T = float> inline static AABox<T> rotate(const AABox<T>& self, T yaw, T pitch, T roll) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::fromYawPitchRoll(yaw, pitch, roll)); }
    template <typename T = float> inline static Vector4<T> rotate(const Vector4<T>& self, T yaw, T pitch, T roll) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::fromYawPitchRoll(yaw, pitch, roll)); }
    template <typename T = float> inline static Vector3<T> rotate(const Vector3<T>& self, T yaw, T pitch, T roll) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::fromYawPitchRoll(yaw, pitch, roll)); }
    template <typename T = float> inline static Line<T> rotate(const Line<T>& self, T yaw, T pitch, T roll) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::fromYawPitchRoll(yaw, pitch, roll)); }
    template <typename T = float> inline static Triangle<T> rotate(const Triangle<T>& self, T yaw, T pitch, T roll) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::fromYawPitchRoll(yaw, pitch, roll)); }
    template <typename T = float> inline static Sphere<T> rotate(const Sphere<T>& self, T yaw, T pitch, T roll) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::fromYawPitchRoll(yaw, pitch, roll)); }
    template <typename T = float> inline static Ray<T> rotate(const Ray<T>& self, T yaw, T pitch, T roll) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::fromYawPitchRoll(yaw, pitch, roll)); }
    template <typename T = float> inline static Quad<T> rotate(const Quad<T>& self, T yaw, T pitch, T roll) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::fromYawPitchRoll(yaw, pitch, roll)); }
    template <typename T = float> inline static Plane<T> rotate(const Plane<T>& self, T yaw, T pitch, T roll) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::fromYawPitchRoll(yaw, pitch, roll)); }
    template <typename T = float> inline static Matrix4x4<T> rotate(const Matrix4x4<T>& self, T yaw, T pitch, T roll) { return self.transform(Matrix4x4<T>::fromYawPitchRoll(yaw, pitch, roll)); }

    template <typename T = float> inline static AABox<T> reflect(const AABox<T>& self, const Plane<T>& plane) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::reflection(plane)); }
    template <typename T = float> inline static Vector4<T> reflect(const Vector4<T>& self, const Plane<T>& plane) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::reflection(plane)); }
    template <typename T = float> inline static Vector3<T> reflect(const Vector3<T>& self, const Plane<T>& plane) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::reflection(plane)); }
    template <typename T = float> inline static Line<T> reflect(const Line<T>& self, const Plane<T>& plane) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::reflection(plane)); }
    template <typename T = float> inline static Triangle<T> reflect(const Triangle<T>& self, const Plane<T>& plane) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::reflection(plane)); }
    template <typename T = float> inline static Sphere<T> reflect(const Sphere<T>& self, const Plane<T>& plane) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::reflection(plane)); }
    template <typename T = float> inline static Ray<T> reflect(const Ray<T>& self, const Plane<T>& plane) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::reflection(plane)); }
    template <typename T = float> inline static Quad<T> reflect(const Quad<T>& self, const Plane<T>& plane) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::reflection(plane)); }
    template <typename T = float> inline static Plane<T> reflect(const Plane<T>& self, const Plane<T>& plane) { return Matrix4x4<T>::transform(self, Matrix4x4<T>::reflection(plane)); }
    template <typename T = float> inline static Matrix4x4<T> reflect(const Matrix4x4<T>& self, const Plane<T>& plane) { return self.transform(Matrix4x4<T>::reflection(plane)); }

    template <typename T = float> inline static AABox<T> rotateX(const AABox<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitX(), angle); }
    template <typename T = float> inline static Vector4<T> rotateX(const Vector4<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitX(), angle); }
    template <typename T = float> inline static Vector3<T> rotateX(const Vector3<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitX(), angle); }
    template <typename T = float> inline static Line<T> rotateX(const Line<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitX(), angle); }
    template <typename T = float> inline static Triangle<T> rotateX(const Triangle<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitX(), angle); }
    template <typename T = float> inline static Sphere<T> rotateX(const Sphere<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitX(), angle); }
    template <typename T = float> inline static Ray<T> rotateX(const Ray<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitX(), angle); }
    template <typename T = float> inline static Quad<T> rotateX(const Quad<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitX(), angle); }
    template <typename T = float> inline static Plane<T> rotateX(const Plane<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitX(), angle); }
    template <typename T = float> inline static Matrix4x4<T> rotateX(const Matrix4x4<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitX(), angle); }

    template <typename T = float> inline static AABox<T> rotateY(const AABox<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitY(), angle); }
    template <typename T = float> inline static Vector4<T> rotateY(const Vector4<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitY(), angle); }
    template <typename T = float> inline static Vector3<T> rotateY(const Vector3<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitY(), angle); }
    template <typename T = float> inline static Line<T> rotateY(const Line<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitY(), angle); }
    template <typename T = float> inline static Triangle<T> rotateY(const Triangle<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitY(), angle); }
    template <typename T = float> inline static Sphere<T> rotateY(const Sphere<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitY(), angle); }
    template <typename T = float> inline static Ray<T> rotateY(const Ray<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitY(), angle); }
    template <typename T = float> inline static Quad<T> rotateY(const Quad<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitY(), angle); }
    template <typename T = float> inline static Plane<T> rotateY(const Plane<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitY(), angle); }
    template <typename T = float> inline static Matrix4x4<T> rotateY(const Matrix4x4<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitY(), angle); }

    template <typename T = float> inline static AABox<T> rotateZ(const AABox<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitZ(), angle); }
    template <typename T = float> inline static Vector4<T> rotateZ(const Vector4<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitZ(), angle); }
    template <typename T = float> inline static Vector3<T> rotateZ(const Vector3<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitZ(), angle); }
    template <typename T = float> inline static Line<T> rotateZ(const Line<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitZ(), angle); }
    template <typename T = float> inline static Triangle<T> rotateZ(const Triangle<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitZ(), angle); }
    template <typename T = float> inline static Sphere<T> rotateZ(const Sphere<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitZ(), angle); }
    template <typename T = float> inline static Ray<T> rotateZ(const Ray<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitZ(), angle); }
    template <typename T = float> inline static Quad<T> rotateZ(const Quad<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitZ(), angle); }
    template <typename T = float> inline static Plane<T> rotateZ(const Plane<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitZ(), angle); }
    template <typename T = float> inline static Matrix4x4<T> rotateZ(const Matrix4x4<T>& self, T angle) { return rotateAround(self, Vector3<T>::unitZ(), angle); }

    template <typename T = float> inline static AABox<T> translateRotateScale(const AABox<T>& self, const Vector3<T>& pos, const Quaternion<T>& rot, const Vector3<T>& s) { return scale(rotate(translate(self, pos), rot), s); }
    template <typename T = float> inline static Vector4<T> translateRotateScale(const Vector4<T>& self, const Vector3<T>& pos, const Quaternion<T>& rot, const Vector3<T>& s) { return scale(rotate(translate(self, pos), rot), s); }
    template <typename T = float> inline static Vector3<T> translateRotateScale(const Vector3<T>& self, const Vector3<T>& pos, const Quaternion<T>& rot, const Vector3<T>& s) { return scale(rotate(translate(self, pos), rot), s); }
    template <typename T = float> inline static Line<T> translateRotateScale(const Line<T>& self, const Vector3<T>& pos, const Quaternion<T>& rot, const Vector3<T>& s) { return scale(rotate(translate(self, pos), rot), s); }
    template <typename T = float> inline static Triangle<T> translateRotateScale(const Triangle<T>& self, const Vector3<T>& pos, const Quaternion<T>& rot, const Vector3<T>& s) { return scale(rotate(translate(self, pos), rot), s); }
    template <typename T = float> inline static Sphere<T> translateRotateScale(const Sphere<T>& self, const Vector3<T>& pos, const Quaternion<T>& rot, const Vector3<T>& s) { return scale(rotate(translate(self, pos), rot), s); }
    template <typename T = float> inline static Ray<T> translateRotateScale(const Ray<T>& self, const Vector3<T>& pos, const Quaternion<T>& rot, const Vector3<T>& s) { return scale(rotate(translate(self, pos), rot), s); }
    template <typename T = float> inline static Quad<T> translateRotateScale(const Quad<T>& self, const Vector3<T>& pos, const Quaternion<T>& rot, const Vector3<T>& s) { return scale(rotate(translate(self, pos), rot), s); }
    template <typename T = float> inline static Plane<T> translateRotateScale(const Plane<T>& self, const Vector3<T>& pos, const Quaternion<T>& rot, const Vector3<T>& s) { return scale(rotate(translate(self, pos), rot), s); }
    template <typename T = float> inline static Matrix4x4<T> translateRotateScale(const Matrix4x4<T>& self, const Vector3<T>& pos, const Quaternion<T>& rot, const Vector3<T>& s) { return scale(rotate(translate(self, pos), rot), s); }

    template <typename T = float> inline static AABox<T> toBox(const Stats<T>& s) { return AABox<T>(s.Min, s.Max); }
    template <typename T = float> inline static Vector2<T> toVector2(const T& v) { return Vector2<T>(v); }
    template <typename T = float> inline static Vector2<T> toVector2(const Vector3<T>& v) { return Vector2<T>(v.X, v.Y); }
    template <typename T = float> inline static Vector2<T> toVector2(const Vector4<T>& v) { return Vector2<T>(v.X, v.Y); }
    template <typename T = float> inline static Vector2<T> toVector2(const Int2& i) { return Vector2<T>(i.X, i.Y); }
    template <typename T = float> inline static Vector2<T> toVector2(const HorizontalCoordinate<T>& angle) { return Vector2<T>(angle.Azimuth, angle.Inclination); }
    template <typename T = float> inline static Vector3<T> toVector3(const T v) { return Vector3<T>(v); }
    template <typename T = float> inline static Vector3<T> toVector3(const Vector2<T>& v) { return Vector3<T>(v); }
    template <typename T = float> inline static Vector3<T> toVector3(const Vector4<T>& v) { return Vector3<T>(v.X, v.Y, v.Z); }
    template <typename T = float> inline static Vector3<T> toVector3(const Int3& i) { return Vector3<T>(i.X, i.Y, i.Z); }
    template <typename T = float> inline static Vector4<T> toVector4(const T& v) { return Vector4<T>(v); }
    template <typename T = float> inline static Vector4<T> toVector4(const Vector2<T>& v) { return Vector4<T>(v); }
    template <typename T = float> inline static Vector4<T> toVector4(const Vector3<T>& v) { return Vector4<T>(v); }
    template <typename T = float> inline static Vector4<T> toVector4(const Int4& i) { return Vector4<T>(i.X, i.Y, i.Z, i.W); }
    template <typename T = float> inline static Vector4<T> toVector4(const Plane<T>& p) { return Vector4<T>(p.Normal.X, p.Normal.Y, p.Normal.Z, p.D); }
    template <typename T = float> inline static Vector4<T> toVector4(const Quaternion<T>& q) { return Vector4<T>(q.X, q.Y, q.Z, q.W); }
    template <typename T = float> inline static Matrix4x4<T> toMatrix(const Vector3<T>& self) { return Matrix4x4<T>::translation(self); }
    template <typename T = float> inline static Matrix4x4<T> toMatrix(const Quaternion<T>& self) { return Matrix4x4<T>::rotation(self); }
    template <typename T = float> inline static Matrix4x4<T> toMatrix(const Transform<T>& self) { return Matrix4x4<T>::trs(self); }
    template <typename T = float> inline static Quaternion<T> toQuaternion(const Matrix4x4<T>& matrix) { return matrix.quaternion(); }
    template <typename T = float> inline static HorizontalCoordinate<T> toHorizontalCoordinate(const Quaternion<T>& q) { return q.sphericalAngle(); }
    template <typename T = float> inline static HorizontalCoordinate<T> toHorizontalCoordinate(const Vector2<T>& vector) { return HorizontalCoordinate<T>(vector.X, vector.Y); }
    template <typename T = float> inline static AABox<T> toBox(const Sphere<T>& sphere) { return AABox<T>(sphere); }
    template <typename T = float> inline static Line<T> toLine(const Vector3<T>& v) { return Line<T>(Vector3<T>::zero(), v); }
    template <typename T = float> inline static Sphere<T> toSphere(const AABox<T>& box) { return box.sphere(); }
    template <typename T = float> inline static Ray<T> toRay(const Line<T>& line) { return Ray<T>(line); }
};
