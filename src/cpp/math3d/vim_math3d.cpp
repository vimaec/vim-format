
#include <iostream>
#include <sstream>
#include <vector>
#include <array>
#include <numeric>
#include <functional>
#include <initializer_list>
#include <optional>
#include <cassert>
#include <cstdlib>
#include <ctime>
#include <chrono> 

#include "hash.h"
#include "vim_math3d.h"

using namespace vim::math3d; 

void test(char const* const name, const std::function<void(void)>& func) {
    try { func();  std::cout << "Test " << name << " is passed." << std::endl; }
    catch (const std::exception& ex) { std::cout << "!!! Test " << name << " is failed !!!" << ex.what() << std::endl; }
};
template<typename T>
void testException(char const* const name, const std::function<void(void)>& func) {
    try { func(); std::cout << "!!! Test " << name << " is failed !!!" << std::endl; }
    catch (const T& ex) { std::cout << "Test " << name << " is passed." << std::endl; }
};

struct Assert {
    inline static void IsFalse(const bool& value) { if (value) { throw std::exception("Assertion is failed"); } }
    inline static void IsTrue(const bool& value) { if (!value) { throw std::exception("Assertion is failed"); } }
    template<typename T> inline static void AreEqual(const T& should, const T& is) { if (should != is) { throw std::exception("Assertion is failed"); } }
    template<typename T> inline static void AreNotEqual(const T& should, const T& is) { if (should == is) { throw std::exception("Assertion is failed"); } }
    inline static void True(const bool& value, char const* const message = "Assertion is failed") { if (!value) { throw std::exception(message); } }
    inline static void False(const bool& value, char const* const message = "Assertion is failed") { if (value) { throw std::exception(message); } }
};

struct MathHelper
{
    static constexpr float pi = 3.14159265358979323846;
    static constexpr float piOver2 = pi / 2;
    static constexpr float piOver4 = pi / 4;

    template<typename T> static bool Equal(T a, T b) { return (std::fabs(a - b) < 1e-5); }

    static float ToRadians(float degrees) { return degrees * pi / 180.0; }
    
    //static bool Equal(double a, double b) { return (std::fabs(a - b) < 1e-5);}
    template<typename T> static bool Equal(const Vector2<T>& a, const Vector2<T>& b) { return Equal(a.X, b.X) && Equal(a.Y, b.Y); }
    template<typename T> static bool Equal(const Vector3<T>& a, const Vector3<T>& b) { return Equal(a.X, b.X) && Equal(a.Y, b.Y) && Equal(a.Z, b.Z); }
    template<typename T> static bool Equal(const Vector4<T>& a, const Vector4<T>& b) { return Equal(a.X, b.X) && Equal(a.Y, b.Y) && Equal(a.Z, b.Z) && Equal(a.W, b.W); }
    template<typename T> static bool Equal(const Matrix4x4<T>& a, const Matrix4x4<T>& b) {
        return Equal(a.M11, b.M11) && Equal(a.M12, b.M12) && Equal(a.M13, b.M13) && Equal(a.M14, b.M14) &&
            Equal(a.M21, b.M21) && Equal(a.M22, b.M22) && Equal(a.M23, b.M23) && Equal(a.M24, b.M24) &&
            Equal(a.M31, b.M31) && Equal(a.M32, b.M32) && Equal(a.M33, b.M33) && Equal(a.M34, b.M34) &&
            Equal(a.M41, b.M41) && Equal(a.M42, b.M42) && Equal(a.M43, b.M43) && Equal(a.M44, b.M44);
    }
    template<typename T> static bool Equal(const Plane<T>& a, const Plane<T>& b) { return Equal(a.Normal, b.Normal) && Equal(a.D, b.D); }
    template<typename T>  static bool Equal(const Quaternion<T>& a, const Quaternion<T>& b) { return Equal(a.X, b.X) && Equal(a.Y, b.Y) && Equal(a.Z, b.Z) && Equal(a.W, b.W); }
    template<typename T> static bool EqualRotation(const Quaternion<T>& a, const Quaternion<T>& b) { return Equal(a, b) || Equal(a, -b); }
};

FMatrix4x4 GenerateMatrixNumberFrom1To16() {
    auto a = FMatrix4x4();
    a.M11 = 1.0f;
    a.M12 = 2.0f;
    a.M13 = 3.0f;
    a.M14 = 4.0f;
    a.M21 = 5.0f;
    a.M22 = 6.0f;
    a.M23 = 7.0f;
    a.M24 = 8.0f;
    a.M31 = 9.0f;
    a.M32 = 10.0f;
    a.M33 = 11.0f;
    a.M34 = 12.0f;
    a.M41 = 13.0f;
    a.M42 = 14.0f;
    a.M43 = 15.0f;
    a.M44 = 16.0f;
    return a;
};

FMatrix4x4 GenerateTestMatrix() {
    auto m =
        FMatrix4x4::rotationX(MathHelper::ToRadians(30.0f)) *
        FMatrix4x4::rotationY(MathHelper::ToRadians(30.0f)) *
        FMatrix4x4::rotationZ(MathHelper::ToRadians(30.0f));
    return m.setTranslation(FVector3(111.0f, 222.0f, 333.0f));
};

void DecomposeTest(float yaw, float pitch, float roll, const FVector3& expectedTranslation, const FVector3& expectedScales)
{
    auto expectedRotation = FQuaternion::fromYawPitchRoll(MathHelper::ToRadians(yaw),
        MathHelper::ToRadians(pitch),
        MathHelper::ToRadians(roll));

    auto m = FMatrix4x4::scale(expectedScales) *
        FMatrix4x4::fromQuaternion(expectedRotation) *
        FMatrix4x4::translation(expectedTranslation);

    FVector3 scales;
    FQuaternion rotation;
    FVector3 translation;

    auto actualResult = m.decompose(scales, rotation, translation);
    Assert::True(actualResult, "FMatrix4x4::Decompose did not return expected value.0");

    auto scaleIsZeroOrNegative = expectedScales.X <= 0 ||
        expectedScales.Y <= 0 ||
        expectedScales.Z <= 0;

    if (scaleIsZeroOrNegative)
    {
        Assert::True(MathHelper::Equal(std::abs(expectedScales.X), std::abs(scales.X)), "FMatrix4x4::Decompose did not return expected value. 1");
        Assert::True(MathHelper::Equal(std::abs(expectedScales.Y), std::abs(scales.Y)), "FMatrix4x4::Decompose did not return expected value. 2");
        Assert::True(MathHelper::Equal(std::abs(expectedScales.Z), std::abs(scales.Z)), "FMatrix4x4::Decompose did not return expected value. 3");
    }
    else
    {
        Assert::True(MathHelper::Equal(expectedScales, scales), "FMatrix4x4::Decompose did not return expected value. 1");
        Assert::True(MathHelper::EqualRotation(expectedRotation, rotation), "FMatrix4x4::Decompose did not return expected value. 2");
    }

    Assert::True(MathHelper::Equal(expectedTranslation, translation), "FMatrix4x4::Decompose did not return expected value.");
};

void ExtractScaleTest(const FVector3& s, const FVector3& r)
{
    auto m = FMatrix4x4::scale(s) * FMatrix4x4::rotation(FQuaternion::fromEulerAngles(r));
    Assert::True(m.extractDirectScale().almostEquals(s),
        "Failed to extract similar scale to input: {m.ExtractDirectScale()} != {s}");
};

void DecomposeScaleTest(float sx, float sy, float sz)
{
    auto m = FMatrix4x4::scale(sx, sy, sz);

    auto expectedScales = FVector3(sx, sy, sz);
    FVector3 scales;
    FQuaternion rotation;
    FVector3 translation;

    auto actualResult = FMatrix4x4::decompose(m, scales, rotation, translation);
    Assert::True(actualResult, "FMatrix4x4::Decompose did not return expected value.1");
    Assert::True(MathHelper::Equal(expectedScales, scales), "FMatrix4x4::Decompose did not return expected value.2");
    Assert::True(MathHelper::EqualRotation(FQuaternion::identity(), rotation), "FMatrix4x4::Decompose did not return expected value.3");
    Assert::True(MathHelper::Equal(FVector3::zero(), translation), "FMatrix4x4::Decompose did not return expected value.4");
};

void CreateReflectionTest(const FPlane& plane, const FMatrix4x4& expected)
{
    auto actual = FMatrix4x4::reflection(plane);
    Assert::True(MathHelper::Equal(actual, expected), "FMatrix4x4::reflection did not return expected value.");
    Assert::True(actual.isReflection(), "FMatrix4x4::IsReflection did not return expected value.");
};

void CreateConstrainedBillboardFact(const FVector3& placeDirection, const FVector3& rotateAxis, const FMatrix4x4& expectedRotation)
{
    FVector3 cameraPosition = FVector3(3.0f, 4.0f, 5.0f);
    auto objectPosition = cameraPosition + placeDirection * 10.0f;
    auto expected = expectedRotation * FMatrix4x4::translation(objectPosition);
    auto actual = FMatrix4x4::constrainedBillboard(objectPosition, cameraPosition, rotateAxis, FVector3(0, 0, -1), FVector3(0, 0, -1));
    Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::CreateConstrainedBillboard did not return the expected value.");

    // When you move camera along rotateAxis, result must be same.
    cameraPosition += rotateAxis * 10.0f;
    actual = FMatrix4x4::constrainedBillboard(objectPosition, cameraPosition, rotateAxis, FVector3(0, 0, -1), FVector3(0, 0, -1));
    Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::CreateConstrainedBillboard did not return the expected value.");

    cameraPosition -= rotateAxis * 30.0f;
    actual = FMatrix4x4::constrainedBillboard(objectPosition, cameraPosition, rotateAxis, FVector3(0, 0, -1), FVector3(0, 0, -1));
    Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::CreateConstrainedBillboard did not return the expected value.");
};

void CreateBillboardFact(const FVector3& placeDirection, const FVector3& cameraUpVector, const FMatrix4x4& expectedRotation)
{
    auto cameraPosition = FVector3(3.0f, 4.0f, 5.0f);
    auto objectPosition = cameraPosition + placeDirection * 10.0f;
    auto expected = expectedRotation * FMatrix4x4::translation(objectPosition);
    auto actual = FMatrix4x4::billboard(objectPosition, cameraPosition, cameraUpVector, FVector3(0, 0, -1));
    Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::CreateBillboard did not return the expected value.");
}

int main()
{
#pragma region AABox2DTests
    std::cout << "AABox2D Tests" << std::endl;

    test("TestNoIntersection1", []() {
        auto box1 = AABox2D(FVector2(0, 0), FVector2(3, 3));
        auto box2 = AABox2D(FVector2(4, 4), FVector2(5, 5));
        Assert::IsFalse(box1.intersects(box2));
        Assert::IsFalse(box2.intersects(box1));
     });

    test("TestNoIntersection2", []() {
        auto box1 = AABox2D(FVector2(0, 0), FVector2(3, 3));
        auto box2 = AABox2D(FVector2(-5, -5), FVector2(-4, -4));
        Assert::IsFalse(box1.intersects(box2));
        Assert::IsFalse(box2.intersects(box1));
    });

    // Intersection tests

    test("TestIntersects1", []() {
        auto box1 = AABox2D(FVector2(0, 0), FVector2(5, 5));
        auto box2 = AABox2D(FVector2(1, 1), FVector2(2, 2));
        Assert::IsTrue(box1.intersects(box2));
        Assert::IsTrue(box2.intersects(box1));
    });

    test("TestIntersects2", []() {
        auto box1 = AABox2D(FVector2(0, 0), FVector2(3, 3));
        auto box2 = AABox2D(FVector2(1, -1), FVector2(2, 7));
        Assert::IsTrue(box1.intersects(box2));
        Assert::IsTrue(box2.intersects(box1));
    });

    test("TestIntersects3", []() {
        auto  box1 = AABox2D(FVector2(0, 0), FVector2(3, 3));
        auto  box2 = AABox2D(FVector2(1, -1), FVector2(2, 2));
        Assert::IsTrue(box1.intersects(box2));
        Assert::IsTrue(box2.intersects(box1));
    });

    test("TestIntersects4", []() {
        auto box1 = AABox2D(FVector2(0, 0), FVector2(3, 3));
        auto box2 = AABox2D(FVector2(3, 3), FVector2(5, 5));
        Assert::IsTrue(box1.intersects(box2));
        Assert::IsTrue(box2.intersects(box1));
    });

#pragma endregion

#pragma region DVector3Tests
    std::cout << "DVector3 Tests" << std::endl;

    test("DVector3CrossTest", []() {
        auto a = DVector3(1.0, 0.0, 0.0);
        auto b = DVector3(0.0, 1.0, 0.0);

        auto expected = DVector3(0.0, 0.0, 1.0);
        
        auto actual = a.cross(b);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Cross did not return the expected value.");
     });
#pragma endregion

#pragma region Line2DTests
    std::cout << "Line2D Tests" << std::endl;

    test("TestNoIntersection1", []()
    {
        auto a =  Line2D( FVector2(0, 0),  FVector2(7, 7));
        auto b =  Line2D( FVector2(3, 4),  FVector2(4, 5));
        Assert::IsFalse(a.intersects(b));
        Assert::IsFalse(b.intersects(a));
    });


    test("TestNoIntersection2", []()
    {
        auto a =  Line2D( FVector2(-4, 4),  FVector2(-2, 1));
        auto b =  Line2D( FVector2(-2, 3),  FVector2(0, 0));
        Assert::IsFalse(a.intersects(b));
        Assert::IsFalse(b.intersects(a));
    });


    test("TestNoIntersection3", []()
    {
        auto a =  Line2D( FVector2(0, 0),  FVector2(0, 1));
        auto b =  Line2D( FVector2(2, 2),  FVector2(2, 3));
        Assert::IsFalse(a.intersects(b));
        Assert::IsFalse(b.intersects(a));
    });


    test("TestNoIntersection4", []()
    {
        auto a =  Line2D( FVector2(0, 0),  FVector2(0, 1));
        auto b =  Line2D( FVector2(2, 2),  FVector2(3, 2));
        Assert::IsFalse(a.intersects(b));
        Assert::IsFalse(b.intersects(a));
    });


    test("TestNoIntersection5", []()
    {
        auto a =  Line2D( FVector2(-1, -1),  FVector2(2, 2));
        auto b =  Line2D( FVector2(3, 3),  FVector2(5, 5));
        Assert::IsFalse(a.intersects(b));
        Assert::IsFalse(b.intersects(a));
    });


    test("TestNoIntersection6", []()
    {
        auto a =  Line2D( FVector2(0, 0),  FVector2(1, 1));
        auto b =  Line2D( FVector2(2, 0),  FVector2(0.5f, 2));
        Assert::IsFalse(a.intersects(b));
        Assert::IsFalse(b.intersects(a));
    });


    test("TestNoIntersection7", []()
    {
        auto a =  Line2D( FVector2(1, 1),  FVector2(4, 1));
        auto b =  Line2D( FVector2(2, 2),  FVector2(3, 2));
        Assert::IsFalse(a.intersects(b));
        Assert::IsFalse(b.intersects(a));
    });


    test("TestNoIntersection8", []()
    {
        auto a =  Line2D( FVector2(0, 5),  FVector2(6, 0));
        auto b =  Line2D( FVector2(2, 1),  FVector2(2, 2));
        Assert::IsFalse(a.intersects(b));
        Assert::IsFalse(b.intersects(a));
    });

    // Intersection tests


    test("TestIntersects1", []()
    {
        auto a =  Line2D( FVector2(0, -2),  FVector2(0, 2));
        auto b =  Line2D( FVector2(-2, 0),  FVector2(2, 0));
        Assert::IsTrue(a.intersects(b));
        Assert::IsTrue(b.intersects(a));
    });


    test("TestIntersects2", []()
    {
        auto a =  Line2D( FVector2(5, 5),  FVector2(0, 0));
        auto b =  Line2D( FVector2(1, 1),  FVector2(8, 2));
        Assert::IsTrue(a.intersects(b));
        Assert::IsTrue(b.intersects(a));
    });


    test("TestIntersects3", []()
    {
        auto a =  Line2D( FVector2(-1, 0),  FVector2(0, 0));
        auto b =  Line2D( FVector2(-1, -1),  FVector2(-1, 1));
        Assert::IsTrue(a.intersects(b));
        Assert::IsTrue(b.intersects(a));
    });


    test("TestIntersects4", []()
    {
        auto a =  Line2D( FVector2(0, 2),  FVector2(2, 2));
        auto b =  Line2D( FVector2(2, 0),  FVector2(2, 4));
        Assert::IsTrue(a.intersects(b));
        Assert::IsTrue(b.intersects(a));
    });


    test("TestIntersects5", []()
    {
        auto a =  Line2D( FVector2(0, 0),  FVector2(5, 5));
        auto b =  Line2D( FVector2(1, 1),  FVector2(3, 3));
        Assert::IsTrue(a.intersects(b));
        Assert::IsTrue(b.intersects(a));
    });


    test("TestIntersects6", []()
    {
        srand(time(nullptr)); // Seed the random number generator with the current time
    
        for (auto i = 0; i < 50; i++)
        {
            auto ax = static_cast<double>(rand()) / RAND_MAX;
            auto ay = static_cast<double>(rand()) / RAND_MAX;
            auto bx = static_cast<double>(rand()) / RAND_MAX;
            auto by = static_cast<double>(rand()) / RAND_MAX;
            auto a =  Line2D( FVector2(ax, ay),  FVector2(bx, by));
            auto b =  Line2D( FVector2(ax, ay),  FVector2(bx, by));
            Assert::IsTrue(a.intersects(b));
            Assert::IsTrue(b.intersects(a));
        }
    });
#pragma endregion

#pragma region PlaneTests
    std::cout << "FPlane Tests" << std::endl;

    test("PlaneEqualsTest1", []()
    {
        auto a = FPlane(1.0f, 2.0f, 3.0f, 4.0f);
        auto b = FPlane(1.0f, 2.0f, 3.0f, 4.0f);

        // case 1: compare between same values
        Assert::AreEqual(true, a == b);
        //Assert::AreEqual(true, ((object)a).Equals(b));

        // case 2: compare between different values
        auto c = FPlane(FVector3(10.0f, b.Normal.Y, b.Normal.Z), b.D);
        Assert::AreEqual(false, b == c);
        Assert::AreEqual(false, b == c);
    });

    // A test for operator == (FPlane, FPlane)
        
    test("PlaneEqualityTest", []()
    {
        auto a = FPlane(1.0f, 2.0f, 3.0f, 4.0f);
        auto b = FPlane(1.0f, 2.0f, 3.0f, 4.0f);

        // case 1: compare between same values
        auto expected = true;
        auto actual = a == b;
        Assert::AreEqual(expected, actual);

        // case 2: compare between different values
        b = FPlane(FVector3(10.0f, b.Normal.Y, b.Normal.Z), b.D);
        expected = false;
        actual = a == b;
        Assert::AreEqual(expected, actual);
    });

    // A test for FPlane (float, float, float, float)
        
    test("PlaneConstructorTest1", []()
    {
        float a = 1.0f, b = 2.0f, c = 3.0f, d = 4.0f;
        auto target = FPlane(a, b, c, d);

        Assert::True(
            target.Normal.X == a && target.Normal.Y == b && target.Normal.Z == c && target.D == d,
            "FPlane.cstor did not return the expected value.");
    });

    // A test for FPlane.CreateFromVertices
        
    test("PlaneCreateFromVerticesTest", []()
    {
        auto point1 = FVector3(0.0f, 1.0f, 1.0f);
        auto point2 = FVector3(0.0f, 0.0f, 1.0f);
        auto point3 = FVector3(1.0f, 0.0f, 1.0f);

        auto target = FPlane(point1, point2, point3);
        auto expected = FPlane(FVector3(0, 0, 1), -1.0f);
        Assert::True(target == expected, "FPlane operator== did not return the expected value.");
    });

        // A test for FPlane.CreateFromVertices
        
        test("PlaneCreateFromVerticesTest2", []()
        {
            auto point1 = FVector3(0.0f, 0.0f, 1.0f);
            auto point2 = FVector3(1.0f, 0.0f, 0.0f);
            auto point3 = FVector3(1.0f, 1.0f, 0.0f);

            auto target = FPlane(point1, point2, point3);
            auto invRoot2 = (float)(1 / std::sqrt(2));

            auto expected = FPlane(FVector3(invRoot2, 0, invRoot2), -invRoot2);
            Assert::True(MathHelper::Equal(target, expected), "FPlane.cstor did not return the expected value.");
        });

        // A test for FPlane (Vector3f, float)
        
        test("PlaneConstructorTest3", []()
        {
            auto normal = FVector3(1, 2, 3);
            float d = 4;

            auto target = FPlane(normal, d);
            Assert::True(
                target.Normal == normal && target.D == d,
                "FPlane.cstor did not return the expected value.");
        });

        // A test for FPlane (Vector4f)
        
        test("PlaneConstructorTest", []()
        {
            auto value = FVector4(1.0f, 2.0f, 3.0f, 4.0f);
            auto target = FPlane(value);

            Assert::True(
                target.Normal.X == value.X && target.Normal.Y == value.Y && target.Normal.Z == value.Z && target.D == value.W,
                "FPlane.cstor did not return the expected value.");
        });

        
        test("PlaneDotTest", []()
        {
            auto target = FPlane(2, 3, 4, 5);
            auto value = FVector4(5, 4, 3, 2);

            float expected = 10 + 12 + 12 + 10;
            auto actual = FPlane::dot(target, value);
            Assert::True(MathHelper::Equal(expected, actual), "FPlane.Dot returns unexpected value.");
        });

        
        test("PlaneDotCoordinateTest", []()
        {
            auto target = FPlane(2, 3, 4, 5);
            auto value = FVector3(5, 4, 3);

            float expected = 10 + 12 + 12 + 5;
            auto actual = FPlane::dotCoordinate(target, value);
            Assert::True(MathHelper::Equal(expected, actual), "FPlane.DotCoordinate returns unexpected value.");
        });

        
        test("PlaneDotNormalTest", []()
        {
            auto target = FPlane(2, 3, 4, 5);
            auto value = FVector3(5, 4, 3);

            float expected = 10 + 12 + 12;
            auto actual = FPlane::dotNormal(target, value);
            Assert::True(MathHelper::Equal(expected, actual), "FPlane.DotCoordinate returns unexpected value.");
        });

        
        test("PlaneNormalizeTest", []()
        {
            auto target = FPlane(1, 2, 3, 4);

            auto f = target.Normal.lengthSquared();
            auto invF = 1.0f / mathOps::sqrt(f); // f.Sqrt();
            auto expected = FPlane(target.Normal * invF, target.D * invF);

            auto actual = FPlane::normalize(target);
            Assert::True(MathHelper::Equal(expected, actual), "FPlane.Normalize returns unexpected value.");

            // normalize, normalized normal.
            actual = FPlane::normalize(actual);
            Assert::True(MathHelper::Equal(expected, actual), "FPlane.Normalize returns unexpected value.");
        });

        
        // Transform by matrix
        test("PlaneTransformTest1", []()
        {
            auto target = FPlane(1, 2, 3, 4);
            target = FPlane::normalize(target);

            auto m =
                FMatrix4x4::rotationX(MathHelper::ToRadians(30.0)) *
                FMatrix4x4::rotationY(MathHelper::ToRadians(30.0)) *
                FMatrix4x4::rotationZ(MathHelper::ToRadians(30.0));
            m.M41 = 10.0f;
            m.M42 = 20.0f;
            m.M43 = 30.0f;

            FMatrix4x4 inv = m.invert().value_or(FMatrix4x4(std::numeric_limits<float>::quiet_NaN()));
            auto itm = FMatrix4x4::transpose(inv);
            float x = target.Normal.X, y = target.Normal.Y, z = target.Normal.Z, w = target.D;
            auto Normal = FVector3(
                x * itm.M11 + y * itm.M21 + z * itm.M31 + w * itm.M41,
                x * itm.M12 + y * itm.M22 + z * itm.M32 + w * itm.M42,
                x * itm.M13 + y * itm.M23 + z * itm.M33 + w * itm.M43);
            auto D = x * itm.M14 + y * itm.M24 + z * itm.M34 + w * itm.M44;
            auto expected = FPlane(Normal, D);
            auto actual = FMatrix4x4::transform(target, m);
            Assert::True(MathHelper::Equal(expected, actual), "FPlane.Transform did not return the expected value.");
        });

        
        // Transform by quaternion
        test("PlaneTransformTest2", []()
        {
            auto target = FPlane(1, 2, 3, 4);
            target = FPlane::normalize(target);

            auto m =
                FMatrix4x4::rotationX(MathHelper::ToRadians(30.0f)) *
                FMatrix4x4::rotationY(MathHelper::ToRadians(30.0f)) *
                FMatrix4x4::rotationZ(MathHelper::ToRadians(30.0f));
            auto q = m.quaternion();

            float x = target.Normal.X, y = target.Normal.Y, z = target.Normal.Z, w = target.D;
            auto Normal = FVector3(
                x * m.M11 + y * m.M21 + z * m.M31 + w * m.M41,
                x * m.M12 + y * m.M22 + z * m.M32 + w * m.M42,
                x * m.M13 + y * m.M23 + z * m.M33 + w * m.M43);
            auto D = x * m.M14 + y * m.M24 + z * m.M34 + w * m.M44;
            auto expected = FPlane(Normal, D);
            auto actual = FMatrix4x4::transform(target, m);
            Assert::True(MathHelper::Equal(expected, actual), "FPlane.Transform did not return the expected value.");
        });

        // A test for FPlane comparison involving NaN values
        
        test("PlaneEqualsNanTest", []() {
            const auto nan = std::numeric_limits<float>::quiet_NaN();
            auto a = FPlane(nan, 0, 0, 0);
            auto b = FPlane(0, nan, 0, 0);
            auto c = FPlane(0, 0, nan, 0);
            auto d = FPlane(0, 0, 0, nan);

            Assert::False(a == FPlane(0, 0, 0, 0));
            Assert::False(b == FPlane(0, 0, 0, 0));
            Assert::False(c == FPlane(0, 0, 0, 0));
            Assert::False(d == FPlane(0, 0, 0, 0));

            Assert::True(a != FPlane(0, 0, 0, 0));
            Assert::True(b != FPlane(0, 0, 0, 0));
            Assert::True(c != FPlane(0, 0, 0, 0));
            Assert::True(d != FPlane(0, 0, 0, 0));

            Assert::False(a == (FPlane(0, 0, 0, 0)));
            Assert::False(b == (FPlane(0, 0, 0, 0)));
            Assert::False(c == (FPlane(0, 0, 0, 0)));
            Assert::False(d == (FPlane(0, 0, 0, 0)));

            // Counterintuitive result - IEEE rules for NaN comparison are weird!
            Assert::False(a == a);
            Assert::False(b == b);
            Assert::False(c == c);
            Assert::False(d == d);
        });


#pragma endregion

#pragma region QuaternionTests
    std::cout << "FQuaternion Tests" << std::endl;
    // A test for Dot (FQuaternion, FQuaternion)

    test("QuaternionDotTest", []()
            {
        auto a = FQuaternion(1.0f, 2.0f, 3.0f, 4.0f);
        auto b = FQuaternion(5.0f, 6.0f, 7.0f, 8.0f);

        auto expected = 70.0f;

        auto actual = FQuaternion::dot(a, b);
        Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::Dot did not return the expected value: expected {expected} actual {actual}");
    });

    // A test for Length ()

    test("QuaternionLengthTest", []()  {
        auto v = FVector3(1.0f, 2.0f, 3.0f);

        auto w = 4.0f;

        auto target = FQuaternion(v, w);

        auto expected = 5.477226f;

        auto actual = target.length();

        Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::Length did not return the expected value: expected {expected} actual {actual}");
    });

    // A test for LengthSquared ()

    test("QuaternionLengthSquaredTest", []()
            {
        auto v = FVector3(1.0f, 2.0f, 3.0f);
        auto w = 4.0f;

        auto target = FQuaternion(v, w);

        auto expected = 30.0f;

        auto actual = target.lengthSquared();

        Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::LengthSquared did not return the expected value: expected {expected} actual {actual}");
    });

    // A test for Lerp (FQuaternion, FQuaternion, float)

    test("QuaternionLerpTest", []()
            {
        auto axis = FVector3(1.0f, 2.0f, 3.0f).normalize();
        auto a = FQuaternion::fromAxisAngle(axis, MathHelper::ToRadians(10.0f));
        auto b = FQuaternion::fromAxisAngle(axis, MathHelper::ToRadians(30.0f));

        auto t = 0.5f;

        auto expected = FQuaternion::fromAxisAngle(axis, MathHelper::ToRadians(20.0f));

        auto actual = FQuaternion::lerp(a, b, t);
        Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::lerp did not return the expected value: expected {expected} actual {actual}");

        // Case a and b are same.
        expected = a;
        actual = FQuaternion::lerp(a, a, t);
        Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::lerp did not return the expected value: expected {expected} actual {actual}");
    });

    // A test for Lerp (FQuaternion, FQuaternion, float)
    // Lerp test when t = 0

    test("QuaternionLerpTest1", []()
            {
        auto axis = FVector3(1.0f, 2.0f, 3.0f).normalize();
        auto a = FQuaternion::fromAxisAngle(axis, MathHelper::ToRadians(10.0f));
        auto b = FQuaternion::fromAxisAngle(axis, MathHelper::ToRadians(30.0f));

        auto t = 0.0f;

        auto expected = FQuaternion(a.X, a.Y, a.Z, a.W);
        auto actual = FQuaternion::lerp(a, b, t);
        Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::lerp did not return the expected value: expected {expected} actual {actual}");
    });

    // A test for Lerp (FQuaternion, FQuaternion, float)
    // Lerp test when t = 1

    test("QuaternionLerpTest2", []()
            {
        auto axis = FVector3(1.0f, 2.0f, 3.0f).normalize();
        auto a = FQuaternion::fromAxisAngle(axis, MathHelper::ToRadians(10.0f));
        auto b = FQuaternion::fromAxisAngle(axis, MathHelper::ToRadians(30.0f));

        auto t = 1.0f;

        auto expected = FQuaternion(b.X, b.Y, b.Z, b.W);
        auto actual = FQuaternion::lerp(a, b, t);
        Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::lerp did not return the expected value: expected {expected} actual {actual}");
    });

    // A test for Lerp (FQuaternion, FQuaternion, float)
    // Lerp test when the two quaternions are more than 90 degree (dot product <0)

    test("QuaternionLerpTest3", []()
            {
        auto axis = FVector3(1.0f, 2.0f, 3.0f).normalize();
        auto a = FQuaternion::fromAxisAngle(axis, MathHelper::ToRadians(10.0f));
        auto b = -a;

        auto t = 1.0f;

        auto actual = FQuaternion::lerp(a, b, t);
        // Note that in quaternion world, Q == -Q. In the case of quaternions dot product is zero, 
        // one of the quaternion will be flipped to compute the shortest distance. When t = 1, we
        // expect the result to be the same as quaternion b but flipped.
        Assert::True(actual == a, "FQuaternion::lerp did not return the expected value: expected {a} actual {actual}");
    });

    // A test for Conjugate(FQuaternion)

    test("QuaternionConjugateTest1", []()
            {
        auto a = FQuaternion(1, 2, 3, 4);

        auto expected = FQuaternion(-1, -2, -3, 4);

        auto actual = a.conjugate();
        Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::Conjugate did not return the expected value: expected {expected} actual {actual}");
    });

    // A test for Normalize (FQuaternion)

    test("QuaternionNormalizeTest", []()
            {
        auto a = FQuaternion(1.0f, 2.0f, 3.0f, 4.0f);

        auto expected = FQuaternion(0.182574168f, 0.365148336f, 0.5477225f, 0.7302967f);

        auto actual = a.normalize();
        Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::Normalize did not return the expected value: expected {expected} actual {actual}");
    });

    // A test for Normalize (FQuaternion)
    // Normalize zero length quaternion

    test("QuaternionNormalizeTest1", []()  {
        auto a = FQuaternion(0.0f, 0.0f, -0.0f, 0.0f);

        auto actual = a.normalize();
        Assert::True(std::isnan(actual.X) && std::isnan(actual.Y) && std::isnan(actual.Z) && std::isnan(actual.W)
            , "FQuaternion::Normalize did not return the expected value: expected {FQuaternion(std::numeric_limits<float>::quiet_NaN(), std::numeric_limits<float>::quiet_NaN(), std::numeric_limits<float>::quiet_NaN(), std::numeric_limits<float>::quiet_NaN())} actual {actual}");
    });

    // A test for Concatenate(FQuaternion, FQuaternion)

    test("QuaternionConcatenateTest1", []()
            {
        auto b = FQuaternion(1.0f, 2.0f, 3.0f, 4.0f);
        auto a = FQuaternion(5.0f, 6.0f, 7.0f, 8.0f);

        auto expected = FQuaternion(24.0f, 48.0f, 48.0f, -6.0f);

        auto actual = FQuaternion::concatenate(a, b);
        Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::Concatenate did not return the expected value: expected {expected} actual {actual}");
    });

    // A test for operator - (FQuaternion, FQuaternion)

    test("QuaternionSubtractionTest", []()
            {
        auto a = FQuaternion(1.0f, 6.0f, 7.0f, 4.0f);
        auto b = FQuaternion(5.0f, 2.0f, 3.0f, 8.0f);

        auto expected = FQuaternion(-4.0f, 4.0f, 4.0f, -4.0f);

        auto actual = a - b;

        Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::operator - did not return the expected value: expected {expected} actual {actual}");
    });

    // A test for operator * (FQuaternion, float)

    test("QuaternionMultiplyTest", []()
            {
        auto a = FQuaternion(1.0f, 2.0f, 3.0f, 4.0f);
        auto factor = 0.5f;

        auto expected = FQuaternion(0.5f, 1.0f, 1.5f, 2.0f);

        auto actual = a * factor;

        Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::operator * did not return the expected value: expected {expected} actual {actual}");
    });

            // A test for operator * (FQuaternion, FQuaternion)
        
    test("QuaternionMultiplyTest1", []()
    {
        auto a = FQuaternion(1.0f, 2.0f, 3.0f, 4.0f);
        auto b = FQuaternion(5.0f, 6.0f, 7.0f, 8.0f);

        auto expected = FQuaternion(24.0f, 48.0f, 48.0f, -6.0f);

        auto actual = a * b;

        Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::operator * did not return the expected value: expected {expected} actual {actual}");
    });

    // A test for operator / (FQuaternion, FQuaternion)

    test("QuaternionDivisionTest1", []()
            {
        auto a = FQuaternion(1.0f, 2.0f, 3.0f, 4.0f);
        auto b = FQuaternion(5.0f, 6.0f, 7.0f, 8.0f);

        auto expected = FQuaternion(-0.045977015f, -0.09195402f, -7.450581E-9f, 0.402298868f);

        auto actual = a / b;

        Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::operator / did not return the expected value: expected {expected} actual {actual}");
    });

    // A test for operator + (FQuaternion, FQuaternion)

    test("QuaternionAdditionTest", []()
            {
        auto a = FQuaternion(1.0f, 2.0f, 3.0f, 4.0f);
        auto b = FQuaternion(5.0f, 6.0f, 7.0f, 8.0f);

        auto expected = FQuaternion(6.0f, 8.0f, 10.0f, 12.0f);

        auto actual = a + b;

        Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::operator + did not return the expected value: expected {expected} actual {actual}");
    });

    // A test for FQuaternion (float, float, float, float)

    test("QuaternionConstructorTest", []()
            {
        auto x = 1.0f;
        auto y = 2.0f;
        auto z = 3.0f;
        auto w = 4.0f;

        auto target = FQuaternion(x, y, z, w);

        Assert::True(MathHelper::Equal(target.X, x) && MathHelper::Equal(target.Y, y) && MathHelper::Equal(target.Z, z) && MathHelper::Equal(target.W, w),
            "FQuaternion::constructor (x,y,z,w) did not return the expected value.");
    });

    // A test for FQuaternion (Vector3f, float)

    test("QuaternionConstructorTest1", []()
            {
        auto v = FVector3(1.0f, 2.0f, 3.0f);
        auto w = 4.0f;

        auto target = FQuaternion(v, w);
        Assert::True(MathHelper::Equal(target.X, v.X) && MathHelper::Equal(target.Y, v.Y) && MathHelper::Equal(target.Z, v.Z) && MathHelper::Equal(target.W, w),
            "FQuaternion::constructor (Vector3f,w) did not return the expected value.");
    });

    // A test for CreateFromAxisAngle (Vector3f, float)

    test("QuaternionCreateFromAxisAngleTest", []()
            {
        auto axis = FVector3(1.0f, 2.0f, 3.0f).normalize();
        auto angle = MathHelper::ToRadians(30.0f);

        auto expected = FQuaternion(0.0691723f, 0.1383446f, 0.207516879f, 0.9659258f);

        auto actual = FQuaternion::fromAxisAngle(axis, angle);
        Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::fromAxisAngle did not return the expected value: expected {expected} actual {actual}");
    });

    // A test for CreateFromAxisAngle (Vector3f, float)
    // CreateFromAxisAngle of zero vector

    test("QuaternionCreateFromAxisAngleTest1", []() {
        auto axis = FVector3();
        auto angle = MathHelper::ToRadians(-30.0f);

        auto cos = (float)std::cos(angle / 2.0f);
        auto actual = FQuaternion::fromAxisAngle(axis, angle);

        Assert::True(actual.X == 0.0f && actual.Y == 0.0f && actual.Z == 0.0f && MathHelper::Equal(cos, actual.W)
            , "FQuaternion::fromAxisAngle did not return the expected value.");
    });

    // A test for CreateFromAxisAngle (Vector3f, float)
    // CreateFromAxisAngle of angle = 30 && 750

    test("QuaternionCreateFromAxisAngleTest2", []() {
        auto axis = FVector3(1, 0, 0);
        auto angle1 = MathHelper::ToRadians(30.0f);
        auto angle2 = MathHelper::ToRadians(750.0f);

        auto actual1 = FQuaternion::fromAxisAngle(axis, angle1);
        auto actual2 = FQuaternion::fromAxisAngle(axis, angle2);
        Assert::True(MathHelper::Equal(actual1, actual2), "FQuaternion::fromAxisAngle did not return the expected value: actual1 {actual1} actual2 {actual2}");
    });

    // A test for CreateFromAxisAngle (Vector3f, float)
    // CreateFromAxisAngle of angle = 30 && 390

    test("QuaternionCreateFromAxisAngleTest3", []() {
        auto axis = FVector3(1, 0, 0);
        auto angle1 = MathHelper::ToRadians(30.0f);
        auto angle2 = MathHelper::ToRadians(390.0f);

        auto actual1 = FQuaternion::fromAxisAngle(axis, angle1);
        auto actual2 = FQuaternion::fromAxisAngle(axis, angle2);
        actual1 = actual1.setX(-actual1.X);
        actual1 = actual1.setW(-actual1.W);

        Assert::True(MathHelper::Equal(actual1, actual2), "FQuaternion::fromAxisAngle did not return the expected value: actual1 {actual1} actual2 {actual2}");
    });


    test("QuaternionCreateFromYawPitchRollTest1", []()
            {
        auto yawAngle = MathHelper::ToRadians(30.0f);
        auto pitchAngle = MathHelper::ToRadians(40.0f);
        auto rollAngle = MathHelper::ToRadians(50.0f);

        auto yaw = FQuaternion::fromAxisAngle(FVector3::unitY(), yawAngle);
        auto pitch = FQuaternion::fromAxisAngle(FVector3::unitX(), pitchAngle);
        auto roll = FQuaternion::fromAxisAngle(FVector3::unitZ(), rollAngle);

        auto expected = yaw * pitch * roll;
        auto actual = FQuaternion::fromYawPitchRoll(yawAngle, pitchAngle, rollAngle);
        Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::QuaternionCreateFromYawPitchRollTest1 did not return the expected value: expected {expected} actual {actual}");
    });

    // Covers more numeric rigions

    test("QuaternionCreateFromYawPitchRollTest2", []()
            {
        const float step = 35.0f;

        for (auto yawAngle = -720.0f; yawAngle <= 720.0f; yawAngle += step)
        {
            for (auto pitchAngle = -720.0f; pitchAngle <= 720.0f; pitchAngle += step)
            {
                for (auto rollAngle = -720.0f; rollAngle <= 720.0f; rollAngle += step)
                {
                    auto yawRad = MathHelper::ToRadians(yawAngle);
                    auto pitchRad = MathHelper::ToRadians(pitchAngle);
                    auto rollRad = MathHelper::ToRadians(rollAngle);

                    auto yaw = FQuaternion::fromAxisAngle(FVector3::unitY(), yawRad);
                    auto pitch = FQuaternion::fromAxisAngle(FVector3::unitX(), pitchRad);
                    auto roll = FQuaternion::fromAxisAngle(FVector3::unitZ(), rollRad);

                    auto expected = yaw * pitch * roll;
                    auto actual = FQuaternion::fromYawPitchRoll(yawRad, pitchRad, rollRad);
                    Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::QuaternionCreateFromYawPitchRollTest2 Yaw:{yawAngle} Pitch:{pitchAngle} Roll:{rollAngle} did not return the expected value: expected {expected} actual {actual}");
                }
            }
        }
    });

    // A test for Slerp (FQuaternion, FQuaternion, float)

    test("QuaternionSlerpTest", []()
            {
        auto axis = FVector3(1.0f, 2.0f, 3.0f).normalize();
        auto a = FQuaternion::fromAxisAngle(axis, MathHelper::ToRadians(10.0f));
        auto b = FQuaternion::fromAxisAngle(axis, MathHelper::ToRadians(30.0f));

        auto t = 0.5f;

        auto expected = FQuaternion::fromAxisAngle(axis, MathHelper::ToRadians(20.0f));

        auto actual = FQuaternion::slerp(a, b, t);
        Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::slerp did not return the expected value: expected {expected} actual {actual}");

        // Case a and b are same.
        expected = a;
        actual = FQuaternion::slerp(a, a, t);
        Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::slerp did not return the expected value: expected {expected} actual {actual}");
    });

    // A test for Slerp (FQuaternion, FQuaternion, float)
    // Slerp test where t = 0

    test("QuaternionSlerpTest1", []()
            {
        auto axis = FVector3(1.0f, 2.0f, 3.0f).normalize();
        auto a = FQuaternion::fromAxisAngle(axis, MathHelper::ToRadians(10.0f));
        auto b = FQuaternion::fromAxisAngle(axis, MathHelper::ToRadians(30.0f));

        auto t = 0.0f;

        auto expected = FQuaternion(a.X, a.Y, a.Z, a.W);
        auto actual = FQuaternion::slerp(a, b, t);
        Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::slerp did not return the expected value: expected {expected} actual {actual}");
    });

    // A test for Slerp (FQuaternion, FQuaternion, float)
    // Slerp test where t = 1

    test("QuaternionSlerpTest2", []()
            {
        auto axis = FVector3(1.0f, 2.0f, 3.0f).normalize();
        auto a = FQuaternion::fromAxisAngle(axis, MathHelper::ToRadians(10.0f));
        auto b = FQuaternion::fromAxisAngle(axis, MathHelper::ToRadians(30.0f));

        auto t = 1.0f;

        auto expected = FQuaternion(b.X, b.Y, b.Z, b.W);
        auto actual = FQuaternion::slerp(a, b, t);
        Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::slerp did not return the expected value: expected {expected} actual {actual}");
    });

    // A test for Slerp (FQuaternion, FQuaternion, float)
    // Slerp test where dot product is < 0

    test("QuaternionSlerpTest3", []()
            {
        auto axis = FVector3(1.0f, 2.0f, 3.0f).normalize();
        auto a = FQuaternion::fromAxisAngle(axis, MathHelper::ToRadians(10.0f));
        auto b = -a;

        auto t = 1.0f;

        auto expected = a;
        auto actual = FQuaternion::slerp(a, b, t);
        // Note that in quaternion world, Q == -Q. In the case of quaternions dot product is zero, 
        // one of the quaternion will be flipped to compute the shortest distance. When t = 1, we
        // expect the result to be the same as quaternion b but flipped.
        Assert::True(actual == expected, "FQuaternion::slerp did not return the expected value: expected {expected} actual {actual}");
    });

    // A test for Slerp (FQuaternion, FQuaternion, float)
    // Slerp test where the quaternion is flipped

    test("QuaternionSlerpTest4", []()
            {
        auto axis = FVector3(1.0f, 2.0f, 3.0f).normalize();
        auto a = FQuaternion::fromAxisAngle(axis, MathHelper::ToRadians(10.0f));
        auto b = -FQuaternion::fromAxisAngle(axis, MathHelper::ToRadians(30.0f));

        auto t = 0.0f;

        auto expected = FQuaternion(a.X, a.Y, a.Z, a.W);
        auto actual = FQuaternion::slerp(a, b, t);
        Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::slerp did not return the expected value: expected {expected} actual {actual}");
    });

    // A test for operator - (FQuaternion)

    test("QuaternionUnaryNegationTest", []()
            {
        auto a = FQuaternion(1.0f, 2.0f, 3.0f, 4.0f);

        auto expected = FQuaternion(-1.0f, -2.0f, -3.0f, -4.0f);

        auto actual = -a;

        Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::operator - did not return the expected value: expected {expected} actual {actual}");
    });

    // A test for Inverse (FQuaternion)

    test("QuaternionInverseTest", []()
            {
        auto a = FQuaternion(5.0f, 6.0f, 7.0f, 8.0f);

        auto expected = FQuaternion(-0.0287356321f, -0.03448276f, -0.0402298868f, 0.04597701f);

        auto actual = a.inverse();
        Assert::True(expected == actual);
    });

    // A test for Inverse (FQuaternion)
    // Invert zero length quaternion

    test("QuaternionInverseTest1", []()
            {
        auto a = FQuaternion();
        auto actual = a.inverse();

        Assert::True(std::isnan(actual.X) && std::isnan(actual.Y) && std::isnan(actual.Z) && std::isnan(actual.W)
            , "FQuaternion::Inverse - did not return the expected value: expected {FQuaternion(std::numeric_limits<float>::quiet_NaN(), std::numeric_limits<float>::quiet_NaN(), std::numeric_limits<float>::quiet_NaN(), std::numeric_limits<float>::quiet_NaN())} actual {actual}");
    });

    // A test for Add (FQuaternion, FQuaternion)

    test("QuaternionAddTest", []()
            {
        auto a = FQuaternion(1.0f, 2.0f, 3.0f, 4.0f);
        auto b = FQuaternion(5.0f, 6.0f, 7.0f, 8.0f);
        auto expected = FQuaternion(6.0f, 8.0f, 10.0f, 12.0f);
        Assert::True(expected == (a + b));
    });

    // A test for Divide (FQuaternion, FQuaternion)

    test("QuaternionDivideTest", []()
            {
        auto a = FQuaternion(1.0f, 2.0f, 3.0f, 4.0f);
        auto b = FQuaternion(5.0f, 6.0f, 7.0f, 8.0f);
        auto expected = FQuaternion(-0.045977015f, -0.09195402f, -7.450581E-9f, 0.402298868f);
        Assert::IsTrue(MathHelper::Equal(expected, a / b));
    });

    // A test for Multiply (FQuaternion, float)

    test("QuaternionMultiplyTest2", []() {
        auto a = FQuaternion(1.0f, 2.0f, 3.0f, 4.0f);
        auto factor = 0.5f;
        auto expected = FQuaternion(0.5f, 1.0f, 1.5f, 2.0f);
        Assert::True(expected == (a * factor));
    });

    // A test for Multiply (FQuaternion, FQuaternion)

    test("QuaternionMultiplyTest3", []()
            {
        auto a = FQuaternion(1.0f, 2.0f, 3.0f, 4.0f);
        auto b = FQuaternion(5.0f, 6.0f, 7.0f, 8.0f);
        auto expected = FQuaternion(24.0f, 48.0f, 48.0f, -6.0f);
        Assert::True(expected == (a * b));
    });

    // A test for Negate (FQuaternion)

    test("QuaternionNegateTest", []()
            {
        auto a = FQuaternion(1.0f, 2.0f, 3.0f, 4.0f);
        auto expected = FQuaternion(-1.0f, -2.0f, -3.0f, -4.0f);
        Assert::True(expected == (- a));
    });

    // A test for Subtract (FQuaternion, FQuaternion)

    test("QuaternionSubtractTest", []()
            {
        auto a = FQuaternion(1.0f, 6.0f, 7.0f, 4.0f);
        auto b = FQuaternion(5.0f, 2.0f, 3.0f, 8.0f);

        auto expected = FQuaternion(-4.0f, 4.0f, 4.0f, -4.0f);
        Assert::True(expected == (a - b));
    });

    // A test for operator != (FQuaternion, FQuaternion)

    test("QuaternionInequalityTest", []()
            {
        auto a = FQuaternion(1.0f, 2.0f, 3.0f, 4.0f);
        auto b = FQuaternion(1.0f, 2.0f, 3.0f, 4.0f);

        // case 1: compare between same values
        auto expected = false;
        auto actual = a != b;
        Assert::True(expected == actual);

        // case 2: compare between different values
        expected = true;
        actual = a != b.setX(10.0f);
        Assert::True(expected == actual);
    });

    // A test for operator == (FQuaternion, FQuaternion)

    test("QuaternionEqualityTest", []()
            {
        auto a = FQuaternion(1.0f, 2.0f, 3.0f, 4.0f);
        auto b = FQuaternion(1.0f, 2.0f, 3.0f, 4.0f);

        // case 1: compare between same values
        auto expected = true;
        auto actual = a == b;
        Assert::AreEqual(expected, actual);

        // case 2: compare between different values
        b = b.setX(10.0f);
        expected = false;
        actual = a == b;
        Assert::True(expected == actual);
    });

    // A test for CreateFromRotationMatrix (FMatrix4x4)
    // Convert Identity matrix test

    test("QuaternionFromRotationMatrixTest1", []()
            {
        auto matrix = FMatrix4x4::identity();

        auto expected = FQuaternion(0.0f, 0.0f, 0.0f, 1.0f);
        auto actual = matrix.quaternion();
        Assert::True(MathHelper::Equal(expected, actual),
            "FQuaternion::fromRotationMatrix did not return the expected value: expected {expected} actual {actual}");

        // make sure convert back to matrix is same as we passed matrix.
        auto m2 = FMatrix4x4::fromQuaternion(actual);
        Assert::True(MathHelper::Equal(matrix, m2),
            "FQuaternion::fromQuaternion did not return the expected value: matrix {matrix} m2 {m2}");
    });

    // A test for CreateFromRotationMatrix (FMatrix4x4)
    // Convert X axis rotation matrix

    test("QuaternionFromRotationMatrixTest2", []()
            {
        for (auto angle = 0.0f; angle < 720.0f; angle += 10.0f)
        {
            auto matrix = FMatrix4x4::rotationX(angle);

            auto expected = FQuaternion::fromAxisAngle(FVector3::unitX(), angle);
            auto actual = matrix.quaternion();
            Assert::True(MathHelper::EqualRotation(expected, actual),
                "FQuaternion::fromRotationMatrix angle:{angle} did not return the expected value: expected {expected} actual {actual}");

            // make sure convert back to matrix is same as we passed matrix.
            auto m2 = FMatrix4x4::fromQuaternion(actual); 
            Assert::True(MathHelper::Equal(matrix, m2),
                "FQuaternion::fromQuaternion angle:{angle} did not return the expected value: matrix {matrix} m2 {m2}");
        }
    });

    // A test for CreateFromRotationMatrix (FMatrix4x4)
    // Convert Y axis rotation matrix

    test("QuaternionFromRotationMatrixTest3", []()
            {
        for (auto angle = 0.0f; angle < 720.0f; angle += 10.0f)
        {
            auto matrix = FMatrix4x4::rotationY(angle);

            auto expected = FQuaternion::fromAxisAngle(FVector3::unitY(), angle);
            auto actual = matrix.quaternion();
            Assert::True(MathHelper::EqualRotation(expected, actual),
                "FQuaternion::fromRotationMatrix angle:{angle} did not return the expected value: expected {expected} actual {actual}");

            // make sure convert back to matrix is same as we passed matrix.
            auto m2 = FMatrix4x4::fromQuaternion(actual);
            Assert::True(MathHelper::Equal(matrix, m2),
                "FQuaternion::fromQuaternion angle:{angle} did not return the expected value: matrix {matrix} m2 {m2}");
        }
    });

    // A test for CreateFromRotationMatrix (FMatrix4x4)
    // Convert Z axis rotation matrix

    test("QuaternionFromRotationMatrixTest4", []()
            {
        for (auto angle = 0.0f; angle < 720.0f; angle += 10.0f)
        {
            auto matrix = FMatrix4x4::rotationZ(angle);

            auto expected = FQuaternion::fromAxisAngle(FVector3::unitZ(), angle);
            auto actual = matrix.quaternion();
            Assert::True(MathHelper::EqualRotation(expected, actual),
                "FQuaternion::fromRotationMatrix angle:{angle} did not return the expected value: expected {expected} actual {actual}");

            // make sure convert back to matrix is same as we passed matrix.
            auto m2 = FMatrix4x4::fromQuaternion(actual);
            Assert::True(MathHelper::Equal(matrix, m2),
                "FQuaternion::fromQuaternion angle:{angle} did not return the expected value: matrix {matrix} m2 {m2}");
        }
    });

    // A test for CreateFromRotationMatrix (FMatrix4x4)
    // Convert XYZ axis rotation matrix

    test("QuaternionFromRotationMatrixTest5", []() {
        for (auto angle = 0.0f; angle < 720.0f; angle += 10.0f)
        {
            auto matrix = FMatrix4x4::rotationX(angle) * FMatrix4x4::rotationY(angle) * FMatrix4x4::rotationZ(angle);

            auto expected =
                FQuaternion::fromAxisAngle(FVector3::unitZ(), angle) *
                FQuaternion::fromAxisAngle(FVector3::unitY(), angle) *
                FQuaternion::fromAxisAngle(FVector3::unitX(), angle);

            auto actual = matrix.quaternion();
            Assert::True(MathHelper::EqualRotation(expected, actual),
                "FQuaternion::fromRotationMatrix angle:{angle} did not return the expected value: expected {expected} actual {actual}");

            // make sure convert back to matrix is same as we passed matrix.
            auto m2 = FMatrix4x4::fromQuaternion(actual);
            Assert::True(MathHelper::Equal(matrix, m2),
                "FQuaternion::fromQuaternion angle:{angle} did not return the expected value: matrix {matrix} m2 {m2}");
        }
    });

    // A test for CreateFromRotationMatrix (FMatrix4x4)
    // X axis is most large axis case

    test("QuaternionFromRotationMatrixWithScaledMatrixTest1", []() {
        auto angle = MathHelper::ToRadians(180.0f);
        auto matrix = FMatrix4x4::rotationY(angle) * FMatrix4x4::rotationZ(angle);

        auto expected = FQuaternion::fromAxisAngle(FVector3::unitZ(), angle) * FQuaternion::fromAxisAngle(FVector3::unitY(), angle);
        auto actual = matrix.quaternion();
        Assert::True(MathHelper::EqualRotation(expected, actual),
            "FQuaternion::fromRotationMatrix did not return the expected value: expected {expected} actual {actual}");

        // make sure convert back to matrix is same as we passed matrix.
        auto m2 = FMatrix4x4::fromQuaternion(actual);
        Assert::True(MathHelper::Equal(matrix, m2),
            "FQuaternion::fromQuaternion did not return the expected value: matrix {matrix} m2 {m2}");
    });

    // A test for CreateFromRotationMatrix (FMatrix4x4)
    // Y axis is most large axis case

    test("QuaternionFromRotationMatrixWithScaledMatrixTest2", []() {
        auto angle = MathHelper::ToRadians(180.0f);
        auto matrix = FMatrix4x4::rotationX(angle) * FMatrix4x4::rotationZ(angle);

        auto expected = FQuaternion::fromAxisAngle(FVector3::unitZ(), angle) * FQuaternion::fromAxisAngle(FVector3::unitX(), angle);
        auto actual = matrix.quaternion();
        Assert::True(MathHelper::EqualRotation(expected, actual),
            "FQuaternion::fromRotationMatrix did not return the expected value: expected {expected} actual {actual}");

        // make sure convert back to matrix is same as we passed matrix.
        auto m2 = FMatrix4x4::fromQuaternion(actual);
        Assert::True(MathHelper::Equal(matrix, m2),
            "FQuaternion::fromQuaternion did not return the expected value: matrix {matrix} m2 {m2}");
    });

    // A test for CreateFromRotationMatrix (FMatrix4x4)
    // Z axis is most large axis case

    test("QuaternionFromRotationMatrixWithScaledMatrixTest3", []() {
        auto angle = MathHelper::ToRadians(180.0f);
        auto matrix = FMatrix4x4::rotationX(angle) * FMatrix4x4::rotationY(angle);

        auto expected = FQuaternion::fromAxisAngle(FVector3::unitY(), angle) * FQuaternion::fromAxisAngle(FVector3::unitX(), angle);
        auto actual = matrix.quaternion();
        Assert::True(MathHelper::EqualRotation(expected, actual),
            "FQuaternion::fromRotationMatrix did not return the expected value: expected {expected} actual {actual}");

        // make sure convert back to matrix is same as we passed matrix.
        auto m2 = FMatrix4x4::fromQuaternion(actual);
        Assert::True(MathHelper::Equal(matrix, m2),
            "FQuaternion::fromQuaternion did not return the expected value: matrix {matrix} m2 {m2}");
    });

    // A test for Equals (FQuaternion)

    test("QuaternionEqualsTest1", []()
            {
        auto a = FQuaternion(1.0f, 2.0f, 3.0f, 4.0f);
        auto b = FQuaternion(1.0f, 2.0f, 3.0f, 4.0f);

        // case 1: compare between same values
        auto expected = true;
        auto actual = a == b;
        Assert::AreEqual(expected, actual);

        // case 2: compare between different values
        b = b.setX(10.0f);
        expected = false;
        actual = a == b;
        Assert::AreEqual(expected, actual);
    });

    // A test for Identity

    test("QuaternionIdentityTest", []()
            {
        auto val = FQuaternion(0, 0, 0, 1);
        Assert::True(val == FQuaternion::identity());
    });

    // A test for isIdentity()

    test("QuaternionIsIdentityTest", [](){
        Assert::True(FQuaternion::identity().isIdentity());
        Assert::True(FQuaternion(0, 0, 0, 1).isIdentity());
        Assert::False(FQuaternion(1, 0, 0, 1).isIdentity());
        Assert::False(FQuaternion(0, 1, 0, 1).isIdentity());
        Assert::False(FQuaternion(0, 0, 1, 1).isIdentity());
        Assert::False(FQuaternion(0, 0, 0, 0).isIdentity());
    });

    // A test for FQuaternion comparison involving NaN values

    test("QuaternionEqualsNanTest", [](){
        const auto nan = std::numeric_limits<float>::quiet_NaN();
        auto a = FQuaternion(nan, 0, 0, 0);
        auto b = FQuaternion(0, nan, 0, 0);
        auto c = FQuaternion(0, 0, nan, 0);
        auto d = FQuaternion(0, 0, 0, nan);

        Assert::False(a == FQuaternion(0, 0, 0, 0));
        Assert::False(b == FQuaternion(0, 0, 0, 0));
        Assert::False(c == FQuaternion(0, 0, 0, 0));
        Assert::False(d == FQuaternion(0, 0, 0, 0));

        Assert::True(a != FQuaternion(0, 0, 0, 0));
        Assert::True(b != FQuaternion(0, 0, 0, 0));
        Assert::True(c != FQuaternion(0, 0, 0, 0));
        Assert::True(d != FQuaternion(0, 0, 0, 0));

        Assert::False(a == (FQuaternion(0, 0, 0, 0)));
        Assert::False(b == (FQuaternion(0, 0, 0, 0)));
        Assert::False(c == (FQuaternion(0, 0, 0, 0)));
        Assert::False(d == (FQuaternion(0, 0, 0, 0)));

        Assert::False(a.isIdentity());
        Assert::False(b.isIdentity());
        Assert::False(c.isIdentity());
        Assert::False(d.isIdentity());

        // Counterintuitive result - IEEE rules for NaN comparison are weird!
        Assert::False(a == (a));
        Assert::False(b == (b));
        Assert::False(c == (c));
        Assert::False(d == (d));
    });

    test("ToEulerAndBack", []() {
        auto x = vim::math3d::constants::pi / 5;
        auto y = vim::math3d::constants::pi * 2 / 7;
        auto z = vim::math3d::constants::pi / 3;
        auto euler = FVector3(x, y, z);
        auto quat = FQuaternion::fromEulerAngles(euler);
        auto euler2 = quat.toEulerAngles();
        Assert::True(std::fabs(euler2.Y - euler.Y) < 0.001f);
        Assert::True(std::fabs(euler2.Z - euler.Z) < 0.001f);
    });

    // A test to avoid a floating point NaN precision issue
    // when two input normalized vectors are almost parallel
    // and pointing in the same direction.

    test("CreateRotationFromAtoBTest", []() {
        auto a = FVector3(0.57731324f, 0.57728577f, 0.5774519f);
        auto b = FVector3(0.57738256f, 0.57728577f, 0.57738256f);

        // Assert precondition that a and b are normalized.
        Assert::IsTrue(vim::math3d::mathOps::almostEquals(a.normalize().length(), a.length()));
        Assert::IsTrue(vim::math3d::mathOps::almostEquals(b.normalize().length(), b.length()));

        // Validate that the returned quaternion does not contain NaN due to precision issues
        auto quat = FQuaternion::rotationFromAToB(a, b);
        Assert::IsTrue(quat == (FQuaternion::identity()));
    });
#pragma endregion

#pragma region RayTests
    std::cout << "Ray Tests" << std::endl;

    test("Ray_IntersectBox_IsFalse_OutsideBox", []()
    {
        auto ray = Ray(FVector3(-2, 0, -2), FVector3(0, 0, 1));
        auto box = AABox(FVector3(-1, -1, -1), FVector3(1, 1, 1));

        Assert::True(ray.intersects(box) == std::nullopt);
    });
    test("Ray_IntersectBox_IsTrue_Through", []()
    {
        auto front = Ray(FVector3(0, 0, -2), FVector3(0, 0, 1));
        auto back = Ray(FVector3(0, 0, 2), FVector3(0, 0, -1));
        auto left = Ray(FVector3(-2, 0, 0), FVector3(1, 0, 0));
        auto right = Ray(FVector3(2, 0, 0), FVector3(-1, 0, 0));
        auto top = Ray(FVector3(0, 2, 0), FVector3(0, -1, 0));
        auto under = Ray(FVector3(0, -2, 0), FVector3(0, 1, 0));

        auto box = AABox(FVector3(-1, -1, -1), FVector3(1, 1, 1));

        Assert::True(front.intersects(box) != std::nullopt, "front is null");
        Assert::True(back.intersects(box) != std::nullopt, "back is null");
        Assert::True(left.intersects(box) != std::nullopt, "left is null");
        Assert::True(right.intersects(box) != std::nullopt, "right is null");
        Assert::True(top.intersects(box) != std::nullopt, "top is null");
        Assert::True(under.intersects(box) != std::nullopt, "under is null");
        
    });
    test("Ray_IntersectBox_IsTrue_ThroughDiagonals", []()
    {
        auto XYnZ = Ray(FVector3(2, 2, -2), FVector3(-1, -1, 1));
        auto nXYnZ = Ray(FVector3(-2, 2, -2), FVector3(1, -1, 1));
        auto nXnYnZ = Ray(FVector3(-2, -2, -2), FVector3(1, 1, 1));
        auto XnYnZ = Ray(FVector3(2, -2, -2), FVector3(-1, 1, 1));

        auto box = AABox(FVector3(-1, -1, -1), FVector3(1, 1, 1));

        Assert::True(XYnZ.intersects(box) != std::nullopt, "XYnZ is null");
        Assert::True(nXYnZ.intersects(box) != std::nullopt, "nXYnZ is null");
        Assert::True(nXnYnZ.intersects(box) != std::nullopt, "nXnYnZ is null");
        Assert::True(XnYnZ.intersects(box) != std::nullopt, "XnYnZ is null");

    });
    test("Ray_IntersectBox_IsFalse_AwayFromBox", []()
    {
        auto front = Ray(FVector3(0, 0, -2), FVector3(0, 0, -1));
        auto back = Ray(FVector3(0, 0, 2), FVector3(0, 0, 1));
        auto left = Ray(FVector3(-2, 0, 0), FVector3(-1, 0, 0));
        auto right = Ray(FVector3(2, 0, 0), FVector3(1, 0, 0));
        auto top = Ray(FVector3(0, 2, 0), FVector3(0, 1, 0));
        auto under = Ray(FVector3(0, -2, 0), FVector3(0, -1, 0));

        auto box = AABox(FVector3(-1, -1, -1), FVector3(1, 1, 1));
        
        Assert::True(front.intersects(box) == std::nullopt, "front is not null");
        Assert::True(back.intersects(box) == std::nullopt, "back is not null");
        Assert::True(left.intersects(box) == std::nullopt, "left is not null");
        Assert::True(right.intersects(box) == std::nullopt, "right is not null");
        Assert::True(top.intersects(box) == std::nullopt, "top is not null");
        Assert::True(under.intersects(box) == std::nullopt, "under is not null");
    });
    test("Ray_IntersectBox_IsTrue_OnEdge", []()
    {
        auto front = Ray(FVector3(0, 2, -1), FVector3(0, -1, 0));
        auto back = Ray(FVector3(0, 2, 1), FVector3(0, -1, 0));
        auto left = Ray(FVector3(-1, 0, -2), FVector3(0, 0, 1));
        auto right = Ray(FVector3(1, 0, -2), FVector3(0, 0, 1));
        auto top = Ray(FVector3(0, 1, -2), FVector3(0, 0, 1));
        auto under = Ray(FVector3(0, -1, -2), FVector3(0, 0, 1));

        auto box = AABox(FVector3(-1, -1, -1), FVector3(1, 1, 1));

        Assert::True(front.intersects(box) != std::nullopt, "front is null");
        Assert::True(back.intersects(box) != std::nullopt, "back is null");
        Assert::True(left.intersects(box) != std::nullopt, "left is null");
        Assert::True(right.intersects(box) != std::nullopt, "right is null");
        Assert::True(top.intersects(box) != std::nullopt, "top is null");
        Assert::True(under.intersects(box) != std::nullopt, "under is null");
    });
    test("Ray_IntersectBox_IsFalse_NearEdge", []()
    {
        auto ray = Ray(FVector3(0, 0, -2), FVector3(0, 1.1f, 1));
        auto box = AABox(FVector3(-1, -1, -1), FVector3(1, 1, 1));

        Assert::True(ray.intersects(box) == std::nullopt, "ray is not null");
    });
    test("Ray_IntersectBox_IsTrue_FlatBox", []()
    {
        auto box = AABox(FVector3(-1, -1, 0), FVector3(1, 1, 0));
        auto ray = Ray(FVector3(0, 0, -1), FVector3(0, 0, 1));

        Assert::True(ray.intersects(box) != std::nullopt, "ray is null");
    });
    test("Ray_IntersectTriangle_IsTrue_Inside", []()
    {
        auto ray = Ray(FVector3(0, 0, -1), FVector3(0, 0, 1));

        auto triangle = Triangle(
            FVector3(0, 1, 0),
            FVector3(1, -1, 0),
            FVector3(-1, -1, 0)
        );

        Assert::True(ray.intersects(triangle) != std::nullopt, "ray is null");
    });
    test("Ray_IntersectTriangle_IsFalse_Parralel", []()
    {
        auto ray = Ray(FVector3(0, 0, -1), FVector3(0, 0, 1));

        auto triangle = Triangle(
            FVector3(1, 0, 0),
            FVector3(-1, 0, 0),
            FVector3(0, 0, 1)
        );

        Assert::True(ray.intersects(triangle) == std::nullopt, "ray is not null");
    });
    test("Ray_IntersectTriangle_IsTrue_OnCorner", []()
    {
        auto ray = Ray(FVector3(0, 1, -1), FVector3(0, 0, 1));

        auto triangle = Triangle(
            FVector3(0, 1, 0),
            FVector3(1, -1, 0),
            FVector3(-1, -1, 0)
        );

        Assert::True(ray.intersects(triangle) != std::nullopt, "ray is null");
    });
    test("Ray_IntersectTriangle_IsFalse_InTrickyCorner", []()
    {
        const auto ray = Ray(FVector3(-0.1f, 0, -1), FVector3(0, 0, 1));
        const auto triangle = Triangle(FVector3(0, 0, 0), FVector3(-1, 1, 0), FVector3(1, 0, 0));

        Assert::True(ray.intersects(triangle) == std::nullopt, "ray is not null");
    });
    test("Ray_IntersectTriangle_PerfTest", []()
    {
        //IsFalse_InTrickyCorner
        const auto ray1 = Ray(FVector3(-0.1f, 0, -1), FVector3(0, 0, 1));
        const auto triangle1 = Triangle(FVector3(0, 0, 0), FVector3(-1, 1, 0), FVector3(1, 0, 0));

        //IsTrue_OnCorner
        const auto ray2 = Ray(FVector3(0, 1, -1), FVector3(0, 0, 1));
        const auto triangle2 = Triangle(FVector3(0, 1, 0), FVector3(1, -1, 0), FVector3(-1, -1, 0));

        //IsTrue_OnCorner
        const auto ray3 = Ray(FVector3(0, 0, -1), FVector3(0, 0, 1));
        const auto triangle3 = Triangle(FVector3(1, 0, 0), FVector3(-1, 0, 0), FVector3(0, 0, 1));

        // IsFalse_Parralel
        const auto ray4 = Ray(FVector3(0, 0, -1), FVector3(0, 0, 1));
        const auto triangle4 = Triangle(FVector3(1, 0, 0), FVector3(-1, 0, 0), FVector3(0, 0, 1));

        for (auto j = 0; j < 10; j++)
        {
            auto watch = std::chrono::high_resolution_clock::now();
            for (auto i = 0; i < 1000000; i++)
            {
                intersects(ray1, triangle1);
                intersects(ray2, triangle2);
                intersects(ray3, triangle3);
                intersects(ray4, triangle4);
            }

            auto thombore = std::chrono::duration_cast<std::chrono::milliseconds>(std::chrono::high_resolution_clock::now() - watch).count();
            std::cout << "TomboreMoller " << thombore << " ms" << std::endl;
        }
    });

#pragma endregion

#pragma region Vector2Tests
    std::cout << "FVector2 Tests" << std::endl;
    
    test("Vector2MarshalSizeTest", []()
    {
        auto v = FVector2();

        Assert::True(8 == sizeof(FVector2));
        Assert::True(8 == sizeof(struct FVector2));
        Assert::True(8 == sizeof(v));
    });

    test("Vector2hashTest", [] ()
    {
        auto v1 = FVector2(2.0f, 3.0f);
        auto v2 = FVector2(2.0f, 3.0f);
        auto v3 = FVector2(3.0f, 2.0f);
        Assert::AreEqual(v1.hash(), v1.hash());
        Assert::AreEqual(v1.hash(), v2.hash());
        Assert::AreNotEqual(v1.hash(), v3.hash());
        auto v4 = FVector2(0.0f, 0.0f);
        auto v6 = FVector2(1.0f, 0.0f);
        auto v7 = FVector2(0.0f, 1.0f);
        auto v8 = FVector2(1.0f, 1.0f);
        Assert::AreNotEqual(v4.hash(), v6.hash());
        Assert::AreNotEqual(v4.hash(), v7.hash());
        Assert::AreNotEqual(v4.hash(), v8.hash());
        Assert::AreNotEqual(v7.hash(), v6.hash());
        Assert::AreNotEqual(v8.hash(), v6.hash());
        Assert::AreNotEqual(v8.hash(), v7.hash());
    });

    // A test for Distance (Vector2f, Vector2f)

    test("Vector2DistanceTest", [] ()
    {
        auto a = FVector2(1.0f, 2.0f);
        auto b = FVector2(3.0f, 4.0f);

        auto expected = (float)std::sqrt(8);
        float actual;

        actual = FVector2::distance(a, b);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Distance did not return the expected value.");
    });

    // A test for Distance (Vector2f, Vector2f)
    // Distance from the same point

    test("Vector2DistanceTest2", [] ()
    {
        auto a = FVector2(1.051f, 2.05f);
        auto b = FVector2(1.051f, 2.05f);

        auto actual = FVector2::distance(a, b);
        Assert::AreEqual(0.0f, actual);
    });

    // A test for DistanceSquared (Vector2f, Vector2f)

    test("Vector2DistanceSquaredTest", [] ()
    {
        auto a = FVector2(1.0f, 2.0f);
        auto b = FVector2(3.0f, 4.0f);

        auto expected = 8.0f;
        float actual;

        actual = FVector2::distanceSquared(a, b);
        Assert::True(MathHelper::Equal(expected, actual),
            "Vector2f.DistanceSquared did not return the expected value.");
    });

    // A test for Dot (Vector2f, Vector2f)

    test("Vector2DotTest", [] ()
    {
        auto a = FVector2(1.0f, 2.0f);
        auto b = FVector2(3.0f, 4.0f);

        auto expected = 11.0f;
        float actual;

        actual = FVector2::dot(a, b);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Dot did not return the expected value.");
    });

    // A test for Dot (Vector2f, Vector2f)
    // Dot test for perpendicular vector

    test("Vector2DotTest1", [] ()
    {
        auto a = FVector2(1.55f, 1.55f);
        auto b = FVector2(-1.55f, 1.55f);

        auto expected = 0.0f;
        auto actual = FVector2::dot(a, b);
        Assert::AreEqual(expected, actual);
    });

    // A test for Dot (Vector2f, Vector2f)
    // Dot test with specail float values

    test("Vector2DotTest2", [] ()
    {
        float min_val = -std::numeric_limits<float>::infinity();
        float max_val = std::numeric_limits<float>::infinity();

        auto a = FVector2(min_val, min_val);
        auto b = FVector2(max_val, max_val);

        auto actual = FVector2::dot(a, b);
        Assert::True(std::isinf(actual) && actual < 0, "Vector2f.Dot did not return the expected value.");
    });

    // A test for Length ()

    test("Vector2LengthTest", [] ()
    {
        auto a = FVector2(2.0f, 4.0f);

        auto target = a;

        auto expected = (float)std::sqrt(20);
        float actual;

        actual = target.length();

        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Length did not return the expected value.");
    });

    // A test for Length ()
    // Length test where length is zero

    test("Vector2LengthTest1", [] ()
    {
        auto target = FVector2(0.0f, 0.0f);

        auto expected = 0.0f;
        float actual;

        actual = target.length();

        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Length did not return the expected value.");
    });

    // A test for LengthSquared ()

    test("Vector2LengthSquaredTest", [] ()
    {
        auto a = FVector2(2.0f, 4.0f);

        auto target = a;

        auto expected = 20.0f;
        float actual;

        actual = target.lengthSquared();

        Assert::True(MathHelper::Equal(expected, actual),
            "Vector2f.LengthSquared did not return the expected value.");
    });

    // A test for LengthSquared ()
    // LengthSquared test where the result is zero

    test("Vector2LengthSquaredTest1", [] ()
    {
        auto a = FVector2(0.0f, 0.0f);

        auto expected = 0.0f;
        auto actual = a.lengthSquared();

        Assert::AreEqual(expected, actual);
    });

    // A test for Min (Vector2f, Vector2f)

    test("Vector2MinTest", [] ()
    {
        auto a = FVector2(-1.0f, 4.0f);
        auto b = FVector2(2.0f, 1.0f);

        auto expected = FVector2(-1.0f, 1.0f);
        FVector2 actual;
        actual = a.min(b);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Min did not return the expected value.");
    });


    test("Vector2MinMaxCodeCoverageTest", []()
    {
        auto min = FVector2(0, 0);
        auto max = FVector2(1, 1);
        FVector2 actual;

        // Min.
        actual = min.min(max);
        Assert::AreEqual(actual, min);

        actual = max.min(min);
        Assert::AreEqual(actual, min);

        // Max.
        actual = min.max(max);
        Assert::AreEqual(actual, max);

        actual = max.max(min);
        Assert::AreEqual(actual, max);
    });

    // A test for Max (Vector2f, Vector2f)

    test("Vector2MaxTest", []()
    {
        auto a = FVector2(-1.0f, 4.0f);
        auto b = FVector2(2.0f, 1.0f);

        auto expected = FVector2(2.0f, 4.0f);
        FVector2 actual;
        actual = a.max(b);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Max did not return the expected value.");
    });

    // A test for Clamp (Vector2f, Vector2f, Vector2f)

    test("Vector2ClampTest", [] ()
    {
        auto a = FVector2(0.5f, 0.3f);
        auto min = FVector2(0.0f, 0.1f);
        auto max = FVector2(1.0f, 1.1f);

        // Normal case.
        // Case N1: specified value is in the range.
        auto expected = FVector2(0.5f, 0.3f);
        auto actual = a.clamp(min, max);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Clamp did not return the expected value.");
        // Normal case.
        // Case N2: specified value is bigger than max value.
        a = FVector2(2.0f, 3.0f);
        expected = max;
        actual = a.clamp(min, max);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Clamp did not return the expected value.");
        // Case N3: specified value is smaller than max value.
        a = FVector2(-1.0f, -2.0f);
        expected = min;
        actual = a.clamp(min, max);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Clamp did not return the expected value.");
        // Case N4: combination case.
        a = FVector2(-2.0f, 4.0f);
        expected = FVector2(min.X, max.Y);
        actual = a.clamp(min, max);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Clamp did not return the expected value.");
        // User specified min value is bigger than max value.
        max = FVector2(0.0f, 0.1f);
        min = FVector2(1.0f, 1.1f);

        // Case W1: specified value is in the range.
        a = FVector2(0.5f, 0.3f);
        expected = min;
        actual = a.clamp(min, max);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Clamp did not return the expected value.");

        // Normal case.
        // Case W2: specified value is bigger than max and min value.
        a = FVector2(2.0f, 3.0f);
        expected = min;
        actual = a.clamp(min, max);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Clamp did not return the expected value.");

        // Case W3: specified value is smaller than min and max value.
        a = FVector2(-1.0f, -2.0f);
        expected = min;
        actual = a.clamp(min, max);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Clamp did not return the expected value.");
    });

    // A test for Lerp (Vector2f, Vector2f, float)

    test("Vector2LerpTest", [] ()
    {
        auto a = FVector2(1.0f, 2.0f);
        auto b = FVector2(3.0f, 4.0f);

        auto t = 0.5f;

        auto expected = FVector2(2.0f, 3.0f);
        FVector2 actual;
        actual = a.lerp(b, t);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Lerp did not return the expected value.");
    });

    // A test for Lerp (Vector2f, Vector2f, float)
    // Lerp test with factor zero

    test("Vector2LerpTest1", [] ()
    {
        auto a = FVector2(0.0f, 0.0f);
        auto b = FVector2(3.18f, 4.25f);

        auto t = 0.0f;
        auto expected = FVector2::zero();
        auto actual = a.lerp(b, t);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Lerp did not return the expected value.");
    });

    // A test for Lerp (Vector2f, Vector2f, float)
    // Lerp test with factor one

    test("Vector2LerpTest2", [] ()
    {
        auto a = FVector2(0.0f, 0.0f);
        auto b = FVector2(3.18f, 4.25f);

        auto t = 1.0f;
        auto expected = FVector2(3.18f, 4.25f);
        auto actual = a.lerp(b, t);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Lerp did not return the expected value.");
    });

    // A test for Lerp (Vector2f, Vector2f, float)
    // Lerp test with factor > 1

    test("Vector2LerpTest3", [] ()
    {
        auto a = FVector2(0.0f, 0.0f);
        auto b = FVector2(3.18f, 4.25f);

        auto t = 2.0f;
        auto expected = b * 2.0f;
        auto actual = a.lerp(b, t);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Lerp did not return the expected value.");
    });

    // A test for Lerp (Vector2f, Vector2f, float)
    // Lerp test with factor < 0

    test("Vector2LerpTest4", []()
    {
        auto a = FVector2(0.0f, 0.0f);
        auto b = FVector2(3.18f, 4.25f);

        auto t = -2.0f;
        auto expected = -(b * 2.0f);
        auto actual = a.lerp(b, t);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Lerp did not return the expected value.");
    });

    // A test for Lerp (Vector2f, Vector2f, float)
    // Lerp test with special float value

    test("Vector2LerpTest5", [] ()
    {
        auto inf = std::numeric_limits<float>::infinity();
        auto neg_inf = -std::numeric_limits<float>::infinity();
        auto a = FVector2(45.67f, 90.0f);
        auto b = FVector2(inf, neg_inf);

        auto t = 0.408f;
        auto actual = a.lerp(b, t);
        Assert::True(std::isinf(actual.X) && actual.X > 0, "Vector2f.Lerp did not return the expected value.");
        Assert::True(std::isinf(actual.Y) && actual.Y < 0, "Vector2f.Lerp did not return the expected value.");
    });

    // A test for Lerp (Vector2f, Vector2f, float)
    // Lerp test from the same point

    test("Vector2LerpTest6", [] ()
    {
        auto a = FVector2(1.0f, 2.0f);
        auto b = FVector2(1.0f, 2.0f);

        auto t = 0.5f;

        auto expected = FVector2(1.0f, 2.0f);
        auto actual = a.lerp(b, t);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Lerp did not return the expected value.");
    });

    // A test for Transform(Vector2f, FMatrix4x4)

    test("Vector2TransformTest", [] ()
    {
        auto v = FVector2(1.0f, 2.0f);
        auto m =
            FMatrix4x4::rotationX(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationY(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationZ(MathHelper::ToRadians(30.0f));
        m.M41 = 10.0f;
        m.M42 = 20.0f;
        m.M43 = 30.0f;

        auto expected = FVector2(10.316987f, 22.183012f);
        FVector2 actual;

        actual = FMatrix4x4::transform(v, m);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Transform did not return the expected value.");
    });

    // A test for TransformNormal (Vector2f, FMatrix4x4)

    test("Vector2TransformNormalTest", [] ()
    {
        auto v = FVector2(1.0f, 2.0f);
        auto m =
            FMatrix4x4::rotationX(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationY(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationZ(MathHelper::ToRadians(30.0f));
        m.M41 = 10.0f;
        m.M42 = 20.0f;
        m.M43 = 30.0f;

        auto expected = FVector2(0.3169873f, 2.18301272f);
        FVector2 actual;

        actual = FMatrix4x4::transformNormal(v, m);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Tranform did not return the expected value.");
    });

    // A test for Transform (Vector2f, FQuaternion)

    test("Vector2TransformByQuaternionTest", [] ()
    {
        auto v = FVector2(1.0f, 2.0f);

        auto m =
            FMatrix4x4::rotationX(mathOps::toRadians(30)) *
            FMatrix4x4::rotationY(mathOps::toRadians(30)) *
            FMatrix4x4::rotationZ(mathOps::toRadians(30));
        auto q = m.quaternion();

        auto expected = FMatrix4x4::transform(v, m);
        auto actual = FQuaternion::transform(v, q);
        Assert::True(expected.almostEquals(actual));
    });

    // A test for Transform (Vector2f, FQuaternion)
    // Transform Vector2f with zero quaternion

    test("Vector2TransformByQuaternionTest1", [] ()
    {
        auto v = FVector2(1.0f, 2.0f);
        auto q = FQuaternion();
        auto expected = v;

        auto actual = FQuaternion::transform(v, q);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Transform did not return the expected value.");
    });

    // A test for Transform (Vector2f, FQuaternion)
    // Transform Vector2f with identity quaternion

    test("Vector2TransformByQuaternionTest2", [] ()
    {
        auto v = FVector2(1.0f, 2.0f);
        auto q = FQuaternion::identity();
        auto expected = v;

        auto actual = FQuaternion::transform(v, q);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Transform did not return the expected value.");
    });

    // A test for Normalize (Vector2f)

    test("Vector2NormalizeTest", [] ()
    {
        auto a = FVector2(2.0f, 3.0f);
        auto expected = FVector2(0.554700196225229122018341733457f, 0.8320502943378436830275126001855f);
        Assert::AreEqual(a.normalize(), expected);
    });

    // A test for Normalize (Vector2f)
    // Normalize zero length vector

    test("Vector2NormalizeTest1", [] ()
    {
        auto a = FVector2(); // no parameter, default to 0.0f
        auto actual = a.normalize();
        Assert::True(std::isnan(actual.X) && std::isnan(actual.Y), "Vector2f.Normalize did not return the expected value.");
    });

    // A test for Normalize (Vector2f)
    // Normalize infinite length vector

    test("Vector2NormalizeTest2", [] ()
    {
        auto max = std::numeric_limits<float>::max();
        auto a = FVector2(max, max);
        auto actual = a.normalize();
        auto expected = FVector2(0, 0);
        Assert::True(expected == actual);
    });

    // A test for operator - (Vector2f)

    test("Vector2UnaryNegationTest", [] ()
    {
        auto a = FVector2(1.0f, 2.0f);

        auto expected = FVector2(-1.0f, -2.0f);
        FVector2 actual;

        actual = -a;

        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.operator - did not return the expected value.");
    });



    // A test for operator - (Vector2f)
    // Negate test with special float value

    test("Vector2UnaryNegationTest1", [] ()
    {
        auto inf = std::numeric_limits<float>::infinity();
        auto neg_inf = -std::numeric_limits<float>::infinity();
        auto a = FVector2(inf, neg_inf);

        auto actual = -a;

        Assert::True(std::isinf(actual.X) && actual.X < 0, "Vector2f.operator - did not return the expected value.");
        Assert::True(std::isinf(actual.Y) && actual.Y > 0, "Vector2f.operator - did not return the expected value.");
    });

    // A test for operator - (Vector2f)
    // Negate test with special float value

    test("Vector2UnaryNegationTest2", [] ()
    {
        const auto nan = std::numeric_limits<float>::quiet_NaN();
        auto a = FVector2(nan, 0.0f);
        auto actual = -a;

        Assert::True(std::isnan(actual.X), "Vector2f.operator - did not return the expected value.");
        Assert::True(0.0f == actual.Y, "Vector2f.operator - did not return the expected value.");
    });

    // A test for operator - (Vector2f, Vector2f)

    test("Vector2SubtractionTest", [] ()
    {
        auto a = FVector2(1.0f, 3.0f);
        auto b = FVector2(2.0f, 1.5f);

        auto expected = FVector2(-1.0f, 1.5f);
        FVector2 actual;

        actual = a - b;

        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.operator - did not return the expected value.");
    });

    // A test for operator * (Vector2f, float)

    test("Vector2MultiplyOperatorTest", [] ()
    {
        auto a = FVector2(2.0f, 3.0f);
        const float factor = 2.0f;

        auto expected = FVector2(4.0f, 6.0f);
        FVector2 actual;

        actual = a * factor;
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.operator * did not return the expected value.");
    });

    // A test for operator * (float, Vector2f)

    test("Vector2MultiplyOperatorTest2", [] ()
    {
        auto a = FVector2(2.0f, 3.0f);
        const float factor = 2.0f;

        auto expected = FVector2(4.0f, 6.0f);
        FVector2 actual;

        actual = factor * a;
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.operator * did not return the expected value.");
    });

    // A test for operator * (Vector2f, Vector2f)

    test("Vector2MultiplyOperatorTest3", [] ()
    {
        auto a = FVector2(2.0f, 3.0f);
        auto b = FVector2(4.0f, 5.0f);

        auto expected = FVector2(8.0f, 15.0f);
        FVector2 actual;

        actual = a * b;

        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.operator * did not return the expected value.");
    });

    // A test for operator / (Vector2f, float)

    test("Vector2DivisionTest", [] ()
    {
        auto a = FVector2(2.0f, 3.0f);

        auto div = 2.0f;

        auto expected = FVector2(1.0f, 1.5f);
        FVector2 actual;

        actual = a / div;

        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.operator / did not return the expected value.");
    });

    // A test for operator / (Vector2f, Vector2f)

    test("Vector2DivisionTest1", [] ()
    {
        auto a = FVector2(2.0f, 3.0f);
        auto b = FVector2(4.0f, 5.0f);

        auto expected = FVector2(2.0f / 4.0f, 3.0f / 5.0f);
        FVector2 actual;

        actual = a / b;

        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.operator / did not return the expected value.");
    });

    // A test for operator / (Vector2f, float)
    // Divide by zero

    test("Vector2DivisionTest2", [] ()
    {
        auto a = FVector2(-2.0f, 3.0f);

        auto div = 0.0f;

        auto actual = a / div;

        Assert::True(std::isinf(actual.X) && actual.X < 0, "Vector2f.operator / did not return the expected value.");
        Assert::True(std::isinf(actual.Y) && actual.Y > 0, "Vector2f.operator / did not return the expected value.");
    });

    // A test for operator / (Vector2f, Vector2f)
    // Divide by zero

    test("Vector2DivisionTest3", [] ()
    {
        auto a = FVector2(0.047f, -3.0f);
        auto b = FVector2();

        auto actual = a / b;

        Assert::True(std::isinf(actual.X), "Vector2f.operator / did not return the expected value.");
        Assert::True(std::isinf(actual.Y), "Vector2f.operator / did not return the expected value.");
    });

    // A test for operator + (Vector2f, Vector2f)

    test("Vector2AdditionTest", [] ()
    {
        auto a = FVector2(1.0f, 2.0f);
        auto b = FVector2(3.0f, 4.0f);

        auto expected = FVector2(4.0f, 6.0f);
        FVector2 actual;

        actual = a + b;

        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.operator + did not return the expected value.");
    });

    // A test for Vector2f (float, float)

    test("Vector2ConstructorTest", [] ()
    {
        auto x = 1.0f;
        auto y = 2.0f;

        auto target = FVector2(x, y);
        Assert::True(MathHelper::Equal(target.X, x) && MathHelper::Equal(target.Y, y), "Vector2f(x,y) constructor did not return the expected value.");
    });

    // A test for Vector2f ()
    // Constructor with no parameter

    test("Vector2ConstructorTest2", [] ()
    {
        auto target = FVector2();
        Assert::AreEqual(target.X, 0.0f);
        Assert::AreEqual(target.Y, 0.0f);
    });

    // A test for Vector2f (float, float)
    // Constructor with special floating values

    test("Vector2ConstructorTest3", [] ()
    {
        const auto nan = std::numeric_limits<float>::quiet_NaN();
        auto max = std::numeric_limits<float>::max();
        auto target = FVector2(nan, max);
        Assert::True(std::isnan(target.X));
        Assert::True(target.Y == max);
    });

    // A test for Vector2f (float)

    test("Vector2ConstructorTest4", [] ()
    {
        auto value = 1.0f;
        auto target = FVector2(value);

        auto expected = FVector2(value, value);
        Assert::AreEqual(expected, target);

        value = 2.0f;
        target = FVector2(value);
        expected = FVector2(value, value);
        Assert::AreEqual(expected, target);
    });

    // A test for Add (Vector2f, Vector2f)

    test("Vector2AddTest", [] ()
    {
        auto a = FVector2(1.0f, 2.0f);
        auto b = FVector2(5.0f, 6.0f);

        auto expected = FVector2(6.0f, 8.0f);
        FVector2 actual;

        actual = a.add(b);
        Assert::AreEqual(expected, actual);
    });

    // A test for Divide (Vector2f, float)

    test("Vector2DivideTest", [] ()
    {
        auto a = FVector2(1.0f, 2.0f);
        auto div = 2.0f;
        auto expected = FVector2(0.5f, 1.0f);
        Assert::AreEqual(expected, a / div);
    });

    // A test for Divide (Vector2f, Vector2f)

    test("Vector2DivideTest1", [] ()
    {
        auto a = FVector2(1.0f, 6.0f);
        auto b = FVector2(5.0f, 2.0f);

        auto expected = FVector2(1.0f / 5.0f, 6.0f / 2.0f);
        FVector2 actual;

        actual = mathOps::divide(a, b);
        Assert::AreEqual(expected, actual);
    });

    // A test for Equals (object)

    test("Vector2EqualsTest", [] ()
    {
        auto a = FVector2(1.0f, 2.0f);
        auto b = FVector2(1.0f, 2.0f);

        // case 1: compare between same values
        auto obj = b;

        auto expected = true;
        bool actual = a == obj;
        Assert::AreEqual(expected, actual);

        // case 2: compare between different values
        b = b.setX(10.0f);
        obj = b;
        expected = false;
        actual = a == obj;
        Assert::AreEqual(expected, actual);

        //// case 3: compare between different types.
        //auto obj = FQuaternion();
        //expected = false;
        //actual = a == obj;
        //Assert::AreEqual(expected, actual);

        //// case 3: compare against null.
        ////obj = null;
        //expected = false;
        //actual = a == obj;
        Assert::AreEqual(expected, actual);
    });

    // A test for Multiply (Vector2f, float)

    test("Vector2MultiplyTest", [] ()
    {
        auto a = FVector2(1.0f, 2.0f);
        const float factor = 2.0f;
        auto expected = FVector2(2.0f, 4.0f);
        Assert::AreEqual(expected, a * factor);
    });

    // A test for Multiply (float, Vector2f)

    test("Vector2MultiplyTest2", [] ()
    {
        auto a = FVector2(1.0f, 2.0f);
        const float factor = 2.0f;
        auto expected = FVector2(2.0f, 4.0f);
        Assert::AreEqual(expected, factor * a);
    });

    // A test for Multiply (Vector2f, Vector2f)

    test("Vector2MultiplyTest3", [] ()
    {
        auto a = FVector2(1.0f, 2.0f);
        auto b = FVector2(5.0f, 6.0f);

        auto expected = FVector2(5.0f, 12.0f);
        FVector2 actual;

        actual = a.multiply(b);
        Assert::AreEqual(expected, actual);
    });

    // A test for Negate (Vector2f)

    test("Vector2NegateTest", [] ()
    {
        auto a = FVector2(1.0f, 2.0f);

        auto expected = FVector2(-1.0f, -2.0f);
        FVector2 actual;

        actual = a.negate();
        Assert::AreEqual(expected, actual);
    });

    // A test for operator != (Vector2f, Vector2f)

    test("Vector2InequalityTest", [] ()
    {
        auto a = FVector2(1.0f, 2.0f);
        auto b = FVector2(1.0f, 2.0f);

        // case 1: compare between same values
        auto expected = false;
        auto actual = a != b;
        Assert::AreEqual(expected, actual);

        // case 2: compare between different values
        b = b.setX(10);
        expected = true;
        actual = a != b;
        Assert::AreEqual(expected, actual);
    });

    // A test for operator == (Vector2f, Vector2f)

    test("Vector2EqualityTest", [] ()
    {
        auto a = FVector2(1.0f, 2.0f);
        auto b = FVector2(1.0f, 2.0f);

        // case 1: compare between same values
        auto expected = true;
        auto actual = a == b;
        Assert::AreEqual(expected, actual);

        // case 2: compare between different values
        b = b.setX(10);
        expected = false;
        actual = a == b;
        Assert::AreEqual(expected, actual);
    });

    // A test for Subtract (Vector2f, Vector2f)

    test("Vector2SubtractTest", [] ()
    {
        auto a = FVector2(1.0f, 6.0f);
        auto b = FVector2(5.0f, 2.0f);

        auto expected = FVector2(-4.0f, 4.0f);
        FVector2 actual;

        actual = a.subtract(b);
        Assert::AreEqual(expected, actual);
    });

    // A test for UnitX

    test("Vector2UnitXTest", [] ()
    {
        auto val = FVector2(1.0f, 0.0f);
        Assert::AreEqual(val, FVector2::unitX());
    });

    // A test for UnitY

    test("Vector2UnitYTest", [] ()
    {
        auto val = FVector2(0.0f, 1.0f);
        Assert::AreEqual(val, FVector2::unitY());
    });

    // A test for One

    test("Vector2OneTest", [] ()
    {
        auto val = FVector2(1.0f, 1.0f);
        Assert::AreEqual(val, FVector2::one());
    });

    // A test for Zero

    test("Vector2ZeroTest", [] ()
    {
        auto val = FVector2(0.0f, 0.0f);
        Assert::AreEqual(val, FVector2::zero());
    });

    // A test for Equals (Vector2f)

    test("Vector2EqualsTest1", [] ()
    {
        auto a = FVector2(1.0f, 2.0f);
        auto b = FVector2(1.0f, 2.0f);

        // case 1: compare between same values
        auto expected = true;
        auto actual = a == (b);
        Assert::AreEqual(expected, actual);

        // case 2: compare between different values
        b = b.setX(10);
        expected = false;
        actual = a == (b);
        Assert::AreEqual(expected, actual);
    });

    // A test for Vector2f comparison involving NaN values

    test("Vector2EqualsNanTest", [] ()
    {
        const auto nan = std::numeric_limits<float>::quiet_NaN();
        auto a = FVector2(nan, 0);
        auto b = FVector2(0, nan);

        Assert::False(a == FVector2::zero());
        Assert::False(b == FVector2::zero());

        Assert::True(a != FVector2::zero());
        Assert::True(b != FVector2::zero());

        Assert::False(a == (FVector2::zero()));
        Assert::False(b == (FVector2::zero()));

        // Counterintuitive result - IEEE rules for NaN comparison are weird!
        Assert::False(a == (a));
        Assert::False(b == (b));
    });

    // A test for Reflect (Vector2f, Vector2f)

    test("Vector2ReflectTest", [] ()
    {
        auto a = FVector2(1.0f, 1.0f).normalize();

        // Reflect on XZ plane.
        auto n = FVector2(0.0f, 1.0f);
        auto expected = FVector2(a.X, -a.Y);
        auto actual = a.reflect(n);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Reflect did not return the expected value.");

        // Reflect on XY plane.
        n = FVector2(0.0f, 0.0f);
        expected = FVector2(a.X, a.Y);
        actual = a.reflect(n);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Reflect did not return the expected value.");

        // Reflect on YZ plane.
        n = FVector2(1.0f, 0.0f);
        expected = FVector2(-a.X, a.Y);
        actual = a.reflect(n);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Reflect did not return the expected value.");
    });

    // A test for Reflect (Vector2f, Vector2f)
    // Reflection when normal and source are the same

    test("Vector2ReflectTest1", [] ()
    {
        auto n = FVector2(0.45f, 1.28f);
        n = n.normalize();
        auto a = n;

        auto expected = -n;
        auto actual = a.reflect(n);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Reflect did not return the expected value.");
    });

    // A test for Reflect (Vector2f, Vector2f)
    // Reflection when normal and source are negation

    test("Vector2ReflectTest2", [] ()
    {
        auto n = FVector2(0.45f, 1.28f);
        n = n.normalize();
        auto a = -n;

        auto expected = n;
        auto actual = a.reflect(n);
        Assert::True(MathHelper::Equal(expected, actual), "Vector2f.Reflect did not return the expected value.");
    });


    test("Vector2AbsTest", [] () {
        auto neg_inf = -std::numeric_limits<float>::infinity();
        auto v1 = FVector2(-2.5f, 2.0f);
        auto v3 = FVector2(0.0f, neg_inf).abs();
        auto v = v1.abs();
        Assert::True(2.5f == v.X);
        Assert::True(2.0f == v.Y);
        Assert::True(0.0f == v3.X);
        Assert::True(std::isinf(v3.Y) && v3.Y > 0);
    });


    test("Vector2SqrtTest", [] ()
    {
        auto nan_val = std::numeric_limits<float>::quiet_NaN();
        auto v1 = FVector2(-2.5f, 2.0f);
        auto v2 = FVector2(5.5f, 4.5f);
        Assert::True(2 == (int)v2.squareRoot().X);
        Assert::True(2 == (int)v2.squareRoot().Y);
        Assert::True(std::isnan(v1.squareRoot().X));
    });

#pragma endregion

#pragma region Vector3Tests
    std::cout << "FVector3 Tests" << std::endl;

    test("Vector3MarshalSizeTest", []()
    {
        auto v = FVector3();

        Assert::True(12 == sizeof(FVector3));
        Assert::True(12 == sizeof(struct FVector3));
        Assert::True(12 == sizeof(v));
    });

    test("Vector3hashTest", [] ()
    {
        auto v1 = FVector3(2.0f, 3.0f, 3.3f);
        auto v2 = FVector3(2.0f, 3.0f, 3.3f);
        auto v3 = FVector3(2.0f, 3.0f, 3.3f);
        auto v5 = FVector3(3.0f, 2.0f, 3.3f);
        Assert::AreEqual(v1.hash(), v1.hash());
        Assert::AreEqual(v1.hash(), v2.hash());
        Assert::AreNotEqual(v1.hash(), v5.hash());
        Assert::AreEqual(v1.hash(), v3.hash());
        auto v4 = FVector3(0.0f, 0.0f, 0.0f);
        auto v6 = FVector3(1.0f, 0.0f, 0.0f);
        auto v7 = FVector3(0.0f, 1.0f, 0.0f);
        auto v8 = FVector3(1.0f, 1.0f, 1.0f);
        auto v9 = FVector3(1.0f, 1.0f, 0.0f);
        Assert::AreNotEqual(v4.hash(), v6.hash());
        Assert::AreNotEqual(v4.hash(), v7.hash());
        Assert::AreNotEqual(v4.hash(), v8.hash());
        Assert::AreNotEqual(v7.hash(), v6.hash());
        Assert::AreNotEqual(v8.hash(), v6.hash());
        Assert::AreNotEqual(v8.hash(), v9.hash());
        Assert::AreNotEqual(v7.hash(), v9.hash());
    });

    // A test for Cross (Vector3f, Vector3f)
        
    test("Vector3CrossTest", [] ()
    {
        auto a = FVector3(1.0f, 0.0f, 0.0f);
        auto b = FVector3(0.0f, 1.0f, 0.0f);

        auto expected = FVector3(0.0f, 0.0f, 1.0f);
        FVector3 actual;

        actual = a.cross(b);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Cross did not return the expected value.");
    });

    // A test for Cross (Vector3f, Vector3f)
    // Cross test of the same vector
        
    test("Vector3CrossTest1", [] ()
    {
        auto a = FVector3(0.0f, 1.0f, 0.0f);
        auto b = FVector3(0.0f, 1.0f, 0.0f);

        auto expected = FVector3(0.0f, 0.0f, 0.0f);
        auto actual = a.cross(b);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Cross did not return the expected value.");
    });

    // A test for Distance (Vector3f, Vector3f)
        
    test("Vector3DistanceTest", [] ()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);
        auto b = FVector3(4.0f, 5.0f, 6.0f);

        auto expected = (float)std::sqrt(27);
        float actual;

        actual = a.distance(b);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Distance did not return the expected value.");
    });

    // A test for Distance (Vector3f, Vector3f)
    // Distance from the same point
        
    test("Vector3DistanceTest1", [] ()
    {
        auto a = FVector3(1.051f, 2.05f, 3.478f);
        auto b = FVector3(1.051f, 2.05f, 3.478f);

        auto actual = a.distance(b);
        Assert::AreEqual(0.0f, actual);
    });

    // A test for DistanceSquared (Vector3f, Vector3f)
        
    test("Vector3DistanceSquaredTest", [] ()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);
        auto b = FVector3(4.0f, 5.0f, 6.0f);

        auto expected = 27.0f;
        float actual;

        actual = a.distanceSquared(b);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.DistanceSquared did not return the expected value.");
    });

    // A test for Dot (Vector3f, Vector3f)
        
    test("Vector3DotTest", [] ()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);
        auto b = FVector3(4.0f, 5.0f, 6.0f);

        auto expected = 32.0f;
        float actual;

        actual = a.dot(b);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Dot did not return the expected value.");
    });

    // A test for Dot (Vector3f, Vector3f)
    // Dot test for perpendicular vector
        
    test("Vector3DotTest1", [] ()
    {
        auto a = FVector3(1.55f, 1.55f, 1);
        auto b = FVector3(2.5f, 3, 1.5f);
        auto c = a.cross(b);

        auto expected = 0.0f;
        auto actual1 = a.dot(c);
        auto actual2 = b.dot(c);
        Assert::True(MathHelper::Equal(expected, actual1), "Vector3f.Dot did not return the expected value.");
        Assert::True(MathHelper::Equal(expected, actual2), "Vector3f.Dot did not return the expected value.");
    });

    // A test for Length ()
        
    test("Vector3LengthTest", [] ()
    {
        auto a = FVector2(1.0f, 2.0f);

        auto z = 3.0f;

        auto target = FVector3(a, z);

        auto expected = (float)std::sqrt(14.0f);
        float actual;

        actual = target.length();
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Length did not return the expected value.");
    });

    // A test for Length ()
    // Length test where length is zero
        
    test("Vector3LengthTest1", [] ()
    {
        auto target = FVector3();

        auto expected = 0.0f;
        auto actual = target.length();
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Length did not return the expected value.");
    });

    // A test for LengthSquared ()
        
    test("Vector3LengthSquaredTest", [] ()
    {
        auto a = FVector2(1.0f, 2.0f);

        auto z = 3.0f;

        auto target = FVector3(a, z);

        auto expected = 14.0f;
        float actual;

        actual = target.lengthSquared();
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.LengthSquared did not return the expected value.");
    });

    // A test for Min (Vector3f, Vector3f)
        
    test("Vector3MinTest", [] ()
    {
        auto a = FVector3(-1.0f, 4.0f, -3.0f);
        auto b = FVector3(2.0f, 1.0f, -1.0f);

        auto expected = FVector3(-1.0f, 1.0f, -3.0f);
        FVector3 actual;
        actual = a.min(b);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Min did not return the expected value.");
    });

    // A test for Max (Vector3f, Vector3f)
        
    test("Vector3MaxTest", [] ()
    {
        auto a = FVector3(-1.0f, 4.0f, -3.0f);
        auto b = FVector3(2.0f, 1.0f, -1.0f);

        auto expected = FVector3(2.0f, 4.0f, -1.0f);
        FVector3 actual;
        actual = a.max(b);
        Assert::True(MathHelper::Equal(expected, actual), "mathOps::Max did not return the expected value.");
    });

        
    test("Vector3MinMaxCodeCoverageTest", [] ()
    {
        auto min = FVector3::zero();
        auto max = FVector3::one();
        FVector3 actual;

        // Min.
        actual = min.min(max);
        Assert::AreEqual(actual, min);

        actual = max.min(min);
        Assert::AreEqual(actual, min);

        // Max.
        actual = min.max(max);
        Assert::AreEqual(actual, max);

        actual = max.max(min);
        Assert::AreEqual(actual, max);
    });

    // A test for Lerp (Vector3f, Vector3f, float)
        
    test("Vector3LerpTest", [] ()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);
        auto b = FVector3(4.0f, 5.0f, 6.0f);

        auto t = 0.5f;

        auto expected = FVector3(2.5f, 3.5f, 4.5f);
        FVector3 actual;

        actual = a.lerp(b, t);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Lerp did not return the expected value.");
    });

    // A test for Lerp (Vector3f, Vector3f, float)
    // Lerp test with factor zero
        
    test("Vector3LerpTest1", [] ()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);
        auto b = FVector3(4.0f, 5.0f, 6.0f);

        auto t = 0.0f;
        auto expected = FVector3(1.0f, 2.0f, 3.0f);
        auto actual = a.lerp(b, t);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Lerp did not return the expected value.");
    });

    // A test for Lerp (Vector3f, Vector3f, float)
    // Lerp test with factor one
        
    test("Vector3LerpTest2", [] ()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);
        auto b = FVector3(4.0f, 5.0f, 6.0f);

        auto t = 1.0f;
        auto expected = FVector3(4.0f, 5.0f, 6.0f);
        auto actual = a.lerp(b, t);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Lerp did not return the expected value.");
    });

    // A test for Lerp (Vector3f, Vector3f, float)
    // Lerp test with factor > 1
        
    test("Vector3LerpTest3", [] ()
    {
        auto a = FVector3(0.0f, 0.0f, 0.0f);
        auto b = FVector3(4.0f, 5.0f, 6.0f);

        auto t = 2.0f;
        auto expected = FVector3(8.0f, 10.0f, 12.0f);
        auto actual = a.lerp(b, t);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Lerp did not return the expected value.");
    });

    // A test for Lerp (Vector3f, Vector3f, float)
    // Lerp test with factor < 0
        
    test("Vector3LerpTest4", [] ()
    {
        auto a = FVector3(0.0f, 0.0f, 0.0f);
        auto b = FVector3(4.0f, 5.0f, 6.0f);

        auto t = -2.0f;
        auto expected = FVector3(-8.0f, -10.0f, -12.0f);
        auto actual = a.lerp(b, t);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Lerp did not return the expected value.");
    });

    // A test for Lerp (Vector3f, Vector3f, float)
    // Lerp test from the same point
        
    test("Vector3LerpTest5", [] ()
    {
        auto a = FVector3(1.68f, 2.34f, 5.43f);
        auto b = a;

        auto t = 0.18f;
        auto expected = FVector3(1.68f, 2.34f, 5.43f);
        auto actual = a.lerp(b, t);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Lerp did not return the expected value.");
    });

    // A test for Reflect (Vector3f, Vector3f)
        
    test("Vector3ReflectTest", [] ()
    {
        auto a = FVector3(1.0f, 1.0f, 1.0f).normalize();

        // Reflect on XZ plane.
        auto n = FVector3(0.0f, 1.0f, 0.0f);
        auto expected = FVector3(a.X, -a.Y, a.Z);
        auto actual = a.reflect(n);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Reflect did not return the expected value.");

        // Reflect on XY plane.
        n = FVector3(0.0f, 0.0f, 1.0f);
        expected = FVector3(a.X, a.Y, -a.Z);
        actual = a.reflect(n);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Reflect did not return the expected value.");

        // Reflect on YZ plane.
        n = FVector3(1.0f, 0.0f, 0.0f);
        expected = FVector3(-a.X, a.Y, a.Z);
        actual = a.reflect(n);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Reflect did not return the expected value.");
    });

    // A test for Reflect (Vector3f, Vector3f)
    // Reflection when normal and source are the same
        
    test("Vector3ReflectTest1", [] ()
    {
        auto n = FVector3(0.45f, 1.28f, 0.86f);
        n = n.normalize();
        auto a = n;

        auto expected = -n;
        auto actual = a.reflect(n);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Reflect did not return the expected value.");
    });

    // A test for Reflect (Vector3f, Vector3f)
    // Reflection when normal and source are negation
        
    test("Vector3ReflectTest2", [] ()
    {
        auto n = FVector3(0.45f, 1.28f, 0.86f);
        n = n.normalize();
        auto a = -n;

        auto expected = n;
        auto actual = a.reflect(n);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Reflect did not return the expected value.");
    });

    // A test for Reflect (Vector3f, Vector3f)
    // Reflection when normal and source are perpendicular (a dot n = 0)
        
    test("Vector3ReflectTest3", [] ()
    {
        auto n = FVector3(0.45f, 1.28f, 0.86f);
        auto temp = FVector3(1.28f, 0.45f, 0.01f);
        // find a perpendicular vector of n
        auto a = temp.cross(n);

        auto expected = a;
        auto actual = a.reflect(n);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Reflect did not return the expected value.");
    });

    // A test for Transform(Vector3f, FMatrix4x4)
        
    test("Vector3TransformTest", [] ()
    {
        auto v = FVector3(1.0f, 2.0f, 3.0f);
        auto m =
            FMatrix4x4::rotationX(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationY(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationZ(MathHelper::ToRadians(30.0f));
        m.M41 = 10.0f;
        m.M42 = 20.0f;
        m.M43 = 30.0f;

        auto expected = FVector3(12.191987f, 21.533493f, 32.616024f);
        FVector3 actual;

        actual = FMatrix4x4::transform(v, m);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Transform did not return the expected value.");
    });

    // A test for Clamp (Vector3f, Vector3f, Vector3f)
        
    test("Vector3ClampTest", [] ()
    {
        auto a = FVector3(0.5f, 0.3f, 0.33f);
        auto min = FVector3(0.0f, 0.1f, 0.13f);
        auto max = FVector3(1.0f, 1.1f, 1.13f);

        // Normal case.
        // Case N1: specified value is in the range.
        auto expected = FVector3(0.5f, 0.3f, 0.33f);
        auto actual = a.clamp(min, max);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Clamp did not return the expected value.");

        // Normal case.
        // Case N2: specified value is bigger than max value.
        a = FVector3(2.0f, 3.0f, 4.0f);
        expected = max;
        actual = a.clamp(min, max);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Clamp did not return the expected value.");

        // Case N3: specified value is smaller than max value.
        a = FVector3(-2.0f, -3.0f, -4.0f);
        expected = min;
        actual = a.clamp(min, max);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Clamp did not return the expected value.");

        // Case N4: combination case.
        a = FVector3(-2.0f, 0.5f, 4.0f);
        expected = FVector3(min.X, a.Y, max.Z);
        actual = a.clamp(min, max);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Clamp did not return the expected value.");

        // User specified min value is bigger than max value.
        max = FVector3(0.0f, 0.1f, 0.13f);
        min = FVector3(1.0f, 1.1f, 1.13f);

        // Case W1: specified value is in the range.
        a = FVector3(0.5f, 0.3f, 0.33f);
        expected = min;
        actual = a.clamp(min, max);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Clamp did not return the expected value.");

        // Normal case.
        // Case W2: specified value is bigger than max and min value.
        a = FVector3(2.0f, 3.0f, 4.0f);
        expected = min;
        actual = a.clamp(min, max);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Clamp did not return the expected value.");

        // Case W3: specified value is smaller than min and max value.
        a = FVector3(-2.0f, -3.0f, -4.0f);
        expected = min;
        actual = a.clamp(min, max);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Clamp did not return the expected value.");
    });

    // A test for TransformNormal (Vector3f, FMatrix4x4)
        
    test("Vector3TransformNormalTest", [] ()
    {
        auto v = FVector3(1.0f, 2.0f, 3.0f);
        auto m =
            FMatrix4x4::rotationX(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationY(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationZ(MathHelper::ToRadians(30.0f));
        m.M41 = 10.0f;
        m.M42 = 20.0f;
        m.M43 = 30.0f;

        auto expected = FVector3(2.19198728f, 1.53349364f, 2.61602545f);
        FVector3 actual;

        actual = FMatrix4x4::transformNormal(v, m);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.TransformNormal did not return the expected value.");
    });

    // A test for Transform (Vector3f, FQuaternion)
        
    test("Vector3TransformByQuaternionTest", [] ()
    {
        auto v = FVector3(1.0f, 2.0f, 3.0f);

        auto m =
            FMatrix4x4::rotationX(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationY(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationZ(MathHelper::ToRadians(30.0f));
        auto q = m.quaternion();

        auto expected = FMatrix4x4::transform(v, m);
        auto actual = FQuaternion::transform(v, q);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Transform did not return the expected value.");
    });

    // A test for Transform (Vector3f, FQuaternion)
    // Transform vector3 with zero quaternion
        
    test("Vector3TransformByQuaternionTest1", [] ()
    {
        auto v = FVector3(1.0f, 2.0f, 3.0f);
        auto q = FQuaternion();
        auto expected = v;

        auto actual = FQuaternion::transform(v, q);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Transform did not return the expected value.");
    });

    // A test for Transform (Vector3f, FQuaternion)
    // Transform vector3 with identity quaternion
        
    test("Vector3TransformByQuaternionTest2", [] ()
    {
        auto v = FVector3(1.0f, 2.0f, 3.0f);
        auto q = FQuaternion::identity();
        auto expected = v;

        auto actual = FQuaternion::transform(v, q);
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Transform did not return the expected value.");
    });

    // A test for Normalize (Vector3f)
        
    test("Vector3NormalizeTest", [] ()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);

        auto expected = FVector3(
            0.26726124191242438468455348087975f,
            0.53452248382484876936910696175951f,
            0.80178372573727315405366044263926f);
        FVector3 actual;

        actual = a.normalize();
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Normalize did not return the expected value.");
    });

    // A test for Normalize (Vector3f)
    // Normalize vector of length one
        
    test("Vector3NormalizeTest1", [] ()
    {
        auto a = FVector3(1.0f, 0.0f, 0.0f);

        auto expected = FVector3(1.0f, 0.0f, 0.0f);
        auto actual = a.normalize();
        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.Normalize did not return the expected value.");
    });

    // A test for Normalize (Vector3f)
    // Normalize vector of length zero
        
    test("Vector3NormalizeTest2", [] ()
    {
        auto a = FVector3(0.0f, 0.0f, 0.0f);

        auto expected = FVector3(0.0f, 0.0f, 0.0f);
        auto actual = a.normalize();
        Assert::True(std::isnan(actual.X) && std::isnan(actual.Y) && std::isnan(actual.Z), "Vector3f.Normalize did not return the expected value.");
    });

    // A test for operator - (Vector3f)
        
    test("Vector3UnaryNegationTest", [] ()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);

        auto expected = FVector3(-1.0f, -2.0f, -3.0f);
        FVector3 actual;

        actual = -a;

        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.operator - did not return the expected value.");
    });

        
    test("Vector3UnaryNegationTest1", []() 
    {
        auto a = -FVector3(std::numeric_limits<float>::quiet_NaN(), std::numeric_limits<float>::infinity(), -std::numeric_limits<float>::infinity());
        auto b = -FVector3(0.0f, 0.0f, 0.0f);
        Assert::True(std::isnan(a.X));
        Assert::AreEqual(-std::numeric_limits<float>::infinity(), a.Y);
        Assert::AreEqual(std::numeric_limits<float>::infinity(), a.Z);
        Assert::AreEqual(0.0f, b.X);
        Assert::AreEqual(0.0f, b.Y);
        Assert::AreEqual(0.0f, b.Z);
    });

    // A test for operator - (Vector3f, Vector3f)
        
    test("Vector3SubtractionTest", [] ()
    {
        auto a = FVector3(4.0f, 2.0f, 3.0f);

        auto b = FVector3(1.0f, 5.0f, 7.0f);

        auto expected = FVector3(3.0f, -3.0f, -4.0f);
        FVector3 actual;

        actual = a - b;

        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.operator - did not return the expected value.");
    });

    // A test for operator * (Vector3f, float)
        
    test("Vector3MultiplyOperatorTest", [] ()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);

        auto factor = 2.0f;

        auto expected = FVector3(2.0f, 4.0f, 6.0f);
        FVector3 actual;

        actual = a * factor;

        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.operator * did not return the expected value.");
    });

    // A test for operator * (float, Vector3f)
        
    test("Vector3MultiplyOperatorTest2", [] ()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);

        const float factor = 2.0f;

        auto expected = FVector3(2.0f, 4.0f, 6.0f);
        FVector3 actual;

        actual = factor * a;

        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.operator * did not return the expected value.");
    });

    // A test for operator * (Vector3f, Vector3f)
        
    test("Vector3MultiplyOperatorTest3", [] ()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);

        auto b = FVector3(4.0f, 5.0f, 6.0f);

        auto expected = FVector3(4.0f, 10.0f, 18.0f);
        FVector3 actual;

        actual = a * b;

        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.operator * did not return the expected value.");
    });

    // A test for operator / (Vector3f, float)
        
    test("Vector3DivisionTest", [] ()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);

        auto div = 2.0f;

        auto expected = FVector3(0.5f, 1.0f, 1.5f);
        FVector3 actual;

        actual = a / div;

        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.operator / did not return the expected value.");
    });

    // A test for operator / (Vector3f, Vector3f)
        
    test("Vector3DivisionTest1", [] ()
    {
        auto a = FVector3(4.0f, 2.0f, 3.0f);

        auto b = FVector3(1.0f, 5.0f, 6.0f);

        auto expected = FVector3(4.0f, 0.4f, 0.5f);
        FVector3 actual;

        actual = a / b;

        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.operator / did not return the expected value.");
    });

    // A test for operator / (Vector3f, Vector3f)
    // Divide by zero
        
    test("Vector3DivisionTest2", [] ()
    {
        auto a = FVector3(-2.0f, 3.0f, std::numeric_limits<float>::max());

        auto div = 0.0f;

        auto actual = a / div;

        Assert::True(std::isinf(actual.X) && actual.X < 0, "Vector3f.operator / did not return the expected value.");
        Assert::True(std::isinf(actual.Y) && actual.Y > 0, "Vector3f.operator / did not return the expected value.");
        Assert::True(std::isinf(actual.Z) && actual.Z > 0, "Vector3f.operator / did not return the expected value.");
    });

    // A test for operator / (Vector3f, Vector3f)
    // Divide by zero
        
    test("Vector3DivisionTest3", [] ()
    {
        auto a = FVector3(0.047f, -3.0f, -std::numeric_limits<float>::infinity());
        auto b = FVector3();

        auto actual = a / b;

        Assert::True(std::isinf(actual.X) && actual.X > 0, "Vector3f.operator / did not return the expected value.");
        Assert::True(std::isinf(actual.Y) && actual.Y < 0, "Vector3f.operator / did not return the expected value.");
        Assert::True(std::isinf(actual.Z) && actual.Z < 0, "Vector3f.operator / did not return the expected value.");
    });

    // A test for operator + (Vector3f, Vector3f)
        
    test("Vector3AdditionTest", [] ()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);
        auto b = FVector3(4.0f, 5.0f, 6.0f);

        auto expected = FVector3(5.0f, 7.0f, 9.0f);
        FVector3 actual;

        actual = a + b;

        Assert::True(MathHelper::Equal(expected, actual), "Vector3f.operator + did not return the expected value.");
    });

    // A test for Vector3f (float, float, float)
        
    test("Vector3ConstructorTest", [] ()
    {
        auto x = 1.0f;
        auto y = 2.0f;
        auto z = 3.0f;

        auto target = FVector3(x, y, z);
        Assert::True(MathHelper::Equal(target.X, x) && MathHelper::Equal(target.Y, y) && MathHelper::Equal(target.Z, z), "Vector3f.constructor (x,y,z) did not return the expected value.");
    });

    // A test for Vector3f (Vector2f, float)
        
    test("Vector3ConstructorTest1", [] ()
    {
        auto a = FVector2(1.0f, 2.0f);

        auto z = 3.0f;

        auto target = FVector3(a, z);
        Assert::True(MathHelper::Equal(target.X, a.X) && MathHelper::Equal(target.Y, a.Y) && MathHelper::Equal(target.Z, z), "Vector3f.constructor (Vector2f,z) did not return the expected value.");
    });

    // A test for Vector3f ()
    // Constructor with no parameter
        
    test("Vector3ConstructorTest3", [] ()
    {
        auto a = FVector3();

        Assert::AreEqual(a.X, 0.0f);
        Assert::AreEqual(a.Y, 0.0f);
        Assert::AreEqual(a.Z, 0.0f);
    });

    // A test for Vector2f (float, float)
    // Constructor with special floating values
        
    test("Vector3ConstructorTest4", [] ()
    {
        auto target = FVector3(std::numeric_limits<float>::quiet_NaN(), std::numeric_limits<float>::max(), std::numeric_limits<float>::infinity());

        Assert::True(std::isnan(target.X), "Vector3f.constructor (Vector3f) did not return the expected value.");
        Assert::True((std::numeric_limits<float>::max() == target.Y), "Vector3f.constructor (Vector3f) did not return the expected value.");
        Assert::True(std::isinf(target.Z) && target.Z > 0, "Vector3f.constructor (Vector3f) did not return the expected value.");
    });

    // A test for Add (Vector3f, Vector3f)
        
    test("Vector3AddTest", [] ()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);
        auto b = FVector3(5.0f, 6.0f, 7.0f);

        auto expected = FVector3(6.0f, 8.0f, 10.0f);
        FVector3 actual;

        actual = a.add(b);
        Assert::AreEqual(expected, actual);
    });

    // A test for Divide (Vector3f, float)
        
    test("Vector3DivideTest", [] ()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);
        auto div = 2.0f;
        auto expected = FVector3(0.5f, 1.0f, 1.5f);
        Assert::AreEqual(expected, a / div);
    });

    // A test for Divide (Vector3f, Vector3f)
        
    test("Vector3DivideTest1", [] ()
    {
        auto a = FVector3(1.0f, 6.0f, 7.0f);
        auto b = FVector3(5.0f, 2.0f, 3.0f);

        auto expected = FVector3(1.0f / 5.0f, 6.0f / 2.0f, 7.0f / 3.0f);
        FVector3 actual;

        actual = a.divide(b);
        Assert::AreEqual(expected, actual);
    });

    // A test for Equals (object)
        
    test("Vector3EqualsTest", [] ()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);
        auto b = FVector3(1.0f, 2.0f, 3.0f);

        // case 1: compare between same values
        auto obj = b;

        bool expected = true;
        bool actual = a == obj;
        Assert::AreEqual(expected, actual);

        // case 2: compare between different values
        b = b.setX(10);
        obj = b;
        expected = false;
        actual = a == obj;
        Assert::AreEqual(expected, actual);

        //// case 3: compare between different types.
        //obj = FQuaternion();
        //expected = false;
        //actual = a == obj;
        //Assert::AreEqual(expected, actual);

        //// case 3: compare against null.
        ////obj = null;
        //expected = false;
        //actual = a == obj;
        Assert::AreEqual(expected, actual);
    });

    // A test for Multiply (Vector3f, float)
        
    test("Vector3MultiplyTest", [] ()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);
        const float factor = 2.0f;
        auto expected = FVector3(2.0f, 4.0f, 6.0f);
        Assert::AreEqual(expected, a * factor);
    });

    // A test for Multiply (float, Vector3f)
        
    test("Vector3MultiplyTest2", []()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);
        const float factor = 2.0f;
        auto expected = FVector3(2.0f, 4.0f, 6.0f);
        Assert::AreEqual(expected, factor * a);
    });

    // A test for Multiply (Vector3f, Vector3f)
        
    test("Vector3MultiplyTest3", [] ()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);
        auto b = FVector3(5.0f, 6.0f, 7.0f);

        auto expected = FVector3(5.0f, 12.0f, 21.0f);
        FVector3 actual;

        actual = a.multiply(b);
        Assert::AreEqual(expected, actual);
    });

    // A test for Negate (Vector3f)
        
    test("Vector3NegateTest", [] ()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);

        auto expected = FVector3(-1.0f, -2.0f, -3.0f);
        FVector3 actual;

        actual = a.negate();
        Assert::AreEqual(expected, actual);
    });

    // A test for operator != (Vector3f, Vector3f)
        
    test("Vector3InequalityTest", [] ()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);
        auto b = FVector3(1.0f, 2.0f, 3.0f);

        // case 1: compare between same values
        auto expected = false;
        auto actual = a != b;
        Assert::AreEqual(expected, actual);

        // case 2: compare between different values
        b = b.setX(10);
        expected = true;
        actual = a != b;
        Assert::AreEqual(expected, actual);
    });

    // A test for operator == (Vector3f, Vector3f)
        
    test("Vector3EqualityTest", [] ()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);
        auto b = FVector3(1.0f, 2.0f, 3.0f);

        // case 1: compare between same values
        auto expected = true;
        auto actual = a == b;
        Assert::AreEqual(expected, actual);

        // case 2: compare between different values
        b = b.setX(10);
        expected = false;
        actual = a == b;
        Assert::AreEqual(expected, actual);
    });

    // A test for Subtract (Vector3f, Vector3f)
        
    test("Vector3SubtractTest", [] ()
    {
        auto a = FVector3(1.0f, 6.0f, 3.0f);
        auto b = FVector3(5.0f, 2.0f, 3.0f);

        auto expected = FVector3(-4.0f, 4.0f, 0.0f);
        FVector3 actual;

        actual = a.subtract(b);
        Assert::AreEqual(expected, actual);
    });

    // A test for One
        
    test("Vector3OneTest", [] ()
    {
        auto val = FVector3(1.0f, 1.0f, 1.0f);
        Assert::AreEqual(val, FVector3::one());
    });

    // A test for UnitX
        
    test("Vector3UnitXTest", [] ()
    {
        auto val = FVector3(1.0f, 0.0f, 0.0f);
        Assert::AreEqual(val, FVector3::unitX());
    });

    // A test for UnitY
        
    test("Vector3UnitYTest", [] ()
    {
        auto val = FVector3(0.0f, 1.0f, 0.0f);
        Assert::AreEqual(val, FVector3::unitY());
    });

    // A test for UnitZ
        
    test("Vector3UnitZTest", [] ()
    {
        auto val = FVector3(0.0f, 0.0f, 1.0f);
        Assert::AreEqual(val, FVector3::unitZ());
    });

    // A test for Zero
        
    test("Vector3ZeroTest", [] ()
    {
        auto val = FVector3(0.0f, 0.0f, 0.0f);
        Assert::AreEqual(val, FVector3::zero());
    });

    // A test for Equals (Vector3f)
        
    test("Vector3EqualsTest1", [] ()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);
        auto b = FVector3(1.0f, 2.0f, 3.0f);

        // case 1: compare between same values
        bool expected = true;
        bool actual = a == b;
        Assert::AreEqual(expected, actual);

        // case 2: compare between different values
        b = b.setX(10);
        expected = false;
        actual = a == b;
        Assert::AreEqual(expected, actual);
    });

    // A test for Vector3f (float)
        
    test("Vector3ConstructorTest5", [] ()
    {
        auto value = 1.0f;
        auto target = FVector3(value);

        auto expected = FVector3(value, value, value);
        Assert::AreEqual(expected, target);

        value = 2.0f;
        target = FVector3(value);
        expected = FVector3(value, value, value);
        Assert::AreEqual(expected, target);
    });

    // A test for Vector3f comparison involving NaN values
        
    test("Vector3EqualsNanTest", [] ()
    {
        auto a = FVector3(std::numeric_limits<float>::quiet_NaN(), 0, 0);
        auto b = FVector3(0, std::numeric_limits<float>::quiet_NaN(), 0);
        auto c = FVector3(0, 0, std::numeric_limits<float>::quiet_NaN());

        Assert::False(a == FVector3::zero());
        Assert::False(b == FVector3::zero());
        Assert::False(c == FVector3::zero());

        Assert::True(a != FVector3::zero());
        Assert::True(b != FVector3::zero());
        Assert::True(c != FVector3::zero());

        Assert::False(a == FVector3::zero());
        Assert::False(b == FVector3::zero());
        Assert::False(c == FVector3::zero());

        // Counterintuitive result - IEEE rules for NaN comparison are weird!
        Assert::False(a == a);
        Assert::False(b == b);
        Assert::False(c == c);
    });

        
    test("Vector3AbsTest", [] ()
    {
        auto v1 = FVector3(-2.5f, 2.0f, 0.5f);
        auto v3 = FVector3(0.0f, -std::numeric_limits<float>::infinity(), std::numeric_limits<float>::quiet_NaN()).abs();
        auto v = v1.abs();
        Assert::True(2.5f == v.X);
        Assert::True(2.0f == v.Y);
        Assert::True(0.5f == v.Z);
        Assert::True(0.0f == v3.X);
        Assert::True(std::isinf(v3.Y));
        Assert::True(std::isnan(v3.Z));
    });

        
    test("Vector3SqrtTest", [] ()
    {
        auto a = FVector3(-2.5f, 2.0f, 0.5f);
        auto b = FVector3(5.5f, 4.5f, 16.5f);
        Assert::True(2 == (int)b.squareRoot().X);
        Assert::True(2 == (int)b.squareRoot().Y);
        Assert::True(4 == (int)b.squareRoot().Z);
        Assert::True(std::isnan(a.squareRoot().X));
    });


#pragma endregion

#pragma region Vector4Tests
    std::cout << "FVector4 Tests" << std::endl;
    
    test("Vector4MarshalSizeTest", []()
    {
        auto v = FVector4();

        Assert::True(16 == sizeof(FVector4));
        Assert::True(16 == sizeof(struct FVector4));
        Assert::True(16 == sizeof(v));
    });

    test("Vector4hashTest", []()
    {
        auto v1 = FVector4(2.5f, 2.0f, 3.0f, 3.3f);
        auto v2 = FVector4(2.5f, 2.0f, 3.0f, 3.3f);
        auto v3 = FVector4(2.5f, 2.0f, 3.0f, 3.3f);
        auto v5 = FVector4(3.3f, 3.0f, 2.0f, 2.5f);
        Assert::AreEqual(v1.hash(), v1.hash());
        Assert::AreEqual(v1.hash(), v2.hash());
        Assert::AreNotEqual(v1.hash(), v5.hash());
        Assert::AreEqual(v1.hash(), v3.hash());
        auto v4 = FVector4(0.0f, 0.0f, 0.0f, 0.0f);
        auto v6 = FVector4(1.0f, 0.0f, 0.0f, 0.0f);
        auto v7 = FVector4(0.0f, 1.0f, 0.0f, 0.0f);
        auto v8 = FVector4(1.0f, 1.0f, 1.0f, 1.0f);
        auto v9 = FVector4(1.0f, 1.0f, 0.0f, 0.0f);
        Assert::AreNotEqual(v4.hash(), v6.hash());
        Assert::AreNotEqual(v4.hash(), v7.hash());
        Assert::AreNotEqual(v4.hash(), v8.hash());
        Assert::AreNotEqual(v7.hash(), v6.hash());
        Assert::AreNotEqual(v8.hash(), v6.hash());
        Assert::AreNotEqual(v8.hash(), v7.hash());
        Assert::AreNotEqual(v9.hash(), v7.hash());
    });

    // A test for DistanceSquared (Vector4f, Vector4f)
        
    test("Vector4DistanceSquaredTest", []()
    {
        auto a = FVector4(1.0f, 2.0f, 3.0f, 4.0f);
        auto b = FVector4(5.0f, 6.0f, 7.0f, 8.0f);

        auto expected = 64.0f;
        float actual;

        actual = a.distanceSquared(b);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.DistanceSquared did not return the expected value.");
    });

    // A test for Distance (Vector4f, Vector4f)

    test("Vector4DistanceTest", []()
    {
        auto a = FVector4(1.0f, 2.0f, 3.0f, 4.0f);
        auto b = FVector4(5.0f, 6.0f, 7.0f, 8.0f);

        auto expected = 8.0f;
        float actual;

        actual = a.distance(b);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Distance did not return the expected value.");
    });

    // A test for Distance (Vector4f, Vector4f)
    // Distance from the same point

    test("Vector4DistanceTest1", []()
    {
        auto a = FVector4(FVector2(1.051f, 2.05f), 3.478f, 1.0f);
        auto b = FVector4(FVector3(1.051f, 2.05f, 3.478f), 0.0f);
        b = b.setW(1);

        auto actual = a.distance(b);
        Assert::AreEqual(0.0f, actual);
    });

    // A test for Dot (Vector4f, Vector4f)

    test("Vector4DotTest", []()
    {
        auto a = FVector4(1.0f, 2.0f, 3.0f, 4.0f);
        auto b = FVector4(5.0f, 6.0f, 7.0f, 8.0f);

        auto expected = 70.0f;
        float actual;

        actual = a.dot(b);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Dot did not return the expected value.");
    });

    // A test for Dot (Vector4f, Vector4f)
    // Dot test for perpendicular vector

    test("Vector4DotTest1", []()
    {
        auto a = FVector3(1.55f, 1.55f, 1);
        auto b = FVector3(2.5f, 3, 1.5f);
        auto c = a.cross(b);

        auto d = FVector4(a, 0);
        auto e = FVector4(c, 0);

        auto actual = d.dot(e);
        Assert::True(MathHelper::Equal(0.0f, actual), "Vector4f.Dot did not return the expected value.");
    });

    // A test for Length ()

    test("Vector4LengthTest", []()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);
        auto w = 4.0f;

        auto target = FVector4(a, w);

        auto expected = (float)std::sqrt(30.0f);
        float actual;

        actual = target.length();

        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Length did not return the expected value.");
    });

    // A test for Length ()
    // Length test where length is zero

    test("Vector4LengthTest1", []()
    {
        auto target = FVector4();

        auto expected = 0.0f;
        auto actual = target.length();

        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Length did not return the expected value.");
    });

    // A test for LengthSquared ()

    test("Vector4LengthSquaredTest", []()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);
        auto w = 4.0f;

        auto target = FVector4(a, w);

        float expected = 30;
        float actual;

        actual = target.lengthSquared();

        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.LengthSquared did not return the expected value.");
    });

    // A test for Min (Vector4f, Vector4f)

    test("Vector4MinTest", []()
    {
        auto a = FVector4(-1.0f, 4.0f, -3.0f, 1000.0f);
        auto b = FVector4(2.0f, 1.0f, -1.0f, 0.0f);

        auto expected = FVector4(-1.0f, 1.0f, -3.0f, 0.0f);
        FVector4 actual;
        actual = a.min(b);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Min did not return the expected value.");
    });

    // A test for Max (Vector4f, Vector4f)

    test("Vector4MaxTest", []()
    {
        auto a = FVector4(-1.0f, 4.0f, -3.0f, 1000.0f);
        auto b = FVector4(2.0f, 1.0f, -1.0f, 0.0f);

        auto expected = FVector4(2.0f, 4.0f, -1.0f, 1000.0f);
        FVector4 actual;
        actual = a.max(b);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Max did not return the expected value.");
    });


    test("Vector4MinMaxCodeCoverageTest", []()
    {
        auto min = FVector4::zero();
        auto max = FVector4::one();
        FVector4 actual;

        // Min.
        actual = min.min(max);
        Assert::AreEqual(actual, min);

        actual = max.min(min);
        Assert::AreEqual(actual, min);

        // Max.
        actual = min.max(max);
        Assert::AreEqual(actual, max);

        actual = max.max(min);
        Assert::AreEqual(actual, max);
    });

    // A test for Clamp (Vector4f, Vector4f, Vector4f)

    test("Vector4ClampTest", []()
    {
        auto a = FVector4(0.5f, 0.3f, 0.33f, 0.44f);
        auto min = FVector4(0.0f, 0.1f, 0.13f, 0.14f);
        auto max = FVector4(1.0f, 1.1f, 1.13f, 1.14f);

        // Normal case.
        // Case N1: specified value is in the range.
        auto expected = FVector4(0.5f, 0.3f, 0.33f, 0.44f);
        auto actual = a.clamp(min, max);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Clamp did not return the expected value.");

        // Normal case.
        // Case N2: specified value is bigger than max value.
        a = FVector4(2.0f, 3.0f, 4.0f, 5.0f);
        expected = max;
        actual =  a.clamp(min, max);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Clamp did not return the expected value.");

        // Case N3: specified value is smaller than max value.
        a = FVector4(-2.0f, -3.0f, -4.0f, -5.0f);
        expected = min;
        actual =  a.clamp(min, max);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Clamp did not return the expected value.");

        // Case N4: combination case.
        a = FVector4(-2.0f, 0.5f, 4.0f, -5.0f);
        expected = FVector4(min.X, a.Y, max.Z, min.W);
        actual =  a.clamp(min, max);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Clamp did not return the expected value.");

        // User specified min value is bigger than max value.
        max = FVector4(0.0f, 0.1f, 0.13f, 0.14f);
        min = FVector4(1.0f, 1.1f, 1.13f, 1.14f);

        // Case W1: specified value is in the range.
        a = FVector4(0.5f, 0.3f, 0.33f, 0.44f);
        expected = min;
        actual =  a.clamp(min, max);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Clamp did not return the expected value.");

        // Normal case.
        // Case W2: specified value is bigger than max and min value.
        a = FVector4(2.0f, 3.0f, 4.0f, 5.0f);
        expected = min;
        actual =  a.clamp(min, max);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Clamp did not return the expected value.");

        // Case W3: specified value is smaller than min and max value.
        a = FVector4(-2.0f, -3.0f, -4.0f, -5.0f);
        expected = min;
        actual =  a.clamp(min, max);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Clamp did not return the expected value.");
    });

    // A test for Lerp (Vector4f, Vector4f, float)

    test("Vector4LerpTest", []()
    {
        auto a = FVector4(1.0f, 2.0f, 3.0f, 4.0f);
        auto b = FVector4(5.0f, 6.0f, 7.0f, 8.0f);

        auto t = 0.5f;

        auto expected = FVector4(3.0f, 4.0f, 5.0f, 6.0f);
        FVector4 actual;

        actual = a.lerp(b, t);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Lerp did not return the expected value.");
    });

    // A test for Lerp (Vector4f, Vector4f, float)
    // Lerp test with factor zero

    test("Vector4LerpTest1", []()
    {
        auto a = FVector4(FVector3(1.0f, 2.0f, 3.0f), 4.0f);
        auto b = FVector4(4.0f, 5.0f, 6.0f, 7.0f);

        auto t = 0.0f;
        auto expected = FVector4(1.0f, 2.0f, 3.0f, 4.0f);
        auto actual = a.lerp(b, t);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Lerp did not return the expected value.");
    });

    // A test for Lerp (Vector4f, Vector4f, float)
    // Lerp test with factor one

    test("Vector4LerpTest2", []()
    {
        auto a = FVector4(FVector3(1.0f, 2.0f, 3.0f), 4.0f);
        auto b = FVector4(4.0f, 5.0f, 6.0f, 7.0f);

        auto t = 1.0f;
        auto expected = FVector4(4.0f, 5.0f, 6.0f, 7.0f);
        auto actual = a.lerp(b, t);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Lerp did not return the expected value.");
    });

    // A test for Lerp (Vector4f, Vector4f, float)
    // Lerp test with factor > 1

    test("Vector4LerpTest3", []()
    {
        auto a = FVector4(FVector3(0.0f, 0.0f, 0.0f), 0.0f);
        auto b = FVector4(4.0f, 5.0f, 6.0f, 7.0f);

        auto t = 2.0f;
        auto expected = FVector4(8.0f, 10.0f, 12.0f, 14.0f);
        auto actual = a.lerp(b, t);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Lerp did not return the expected value.");
    });

    // A test for Lerp (Vector4f, Vector4f, float)
    // Lerp test with factor < 0

    test("Vector4LerpTest4", []()
    {
        auto a = FVector4(FVector3(0.0f, 0.0f, 0.0f), 0.0f);
        auto b = FVector4(4.0f, 5.0f, 6.0f, 7.0f);

        auto t = -2.0f;
        auto expected = -(b * 2);
        auto actual = a.lerp(b, t);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Lerp did not return the expected value.");
    });

    // A test for Lerp (Vector4f, Vector4f, float)
    // Lerp test from the same point

    test("Vector4LerpTest5", []()
    {
        auto a = FVector4(4.0f, 5.0f, 6.0f, 7.0f);
        auto b = FVector4(4.0f, 5.0f, 6.0f, 7.0f);

        auto t = 0.85f;
        auto expected = a;
        auto actual = a.lerp(b, t);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Lerp did not return the expected value.");
    });

    // A test for Transform (Vector2f, FMatrix4x4)

    test("Vector4TransformTest1", []()
    {
        auto v = FVector2(1.0f, 2.0f);

        auto m =
            FMatrix4x4::rotationX(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationY(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationZ(MathHelper::ToRadians(30.0f));
        m.M41 = 10.0f;
        m.M42 = 20.0f;
        m.M43 = 30.0f;

        auto expected = FVector4(10.316987f, 22.183012f, 30.3660259f, 1.0f);
        FVector4 actual;

        actual = FMatrix4x4::transformToVector4(v, m);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
    });

    // A test for Transform (Vector3f, FMatrix4x4)

    test("Vector4TransformTest2", []()
    {
        auto v = FVector3(1.0f, 2.0f, 3.0f);

        auto m =
            FMatrix4x4::rotationX(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationY(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationZ(MathHelper::ToRadians(30.0f));
        m.M41 = 10.0f;
        m.M42 = 20.0f;
        m.M43 = 30.0f;

        auto expected = FVector4(12.19198728f, 21.53349376f, 32.61602545f, 1.0f);
        FVector4 actual;

        actual = FMatrix4x4::transformToVector4(v, m);
        Assert::True(MathHelper::Equal(expected, actual), "mathOps::Transform did not return the expected value.");
    });

    // A test for Transform (Vector4f, FMatrix4x4)

    test("Vector4TransformVector4Test", []()
    {
        auto v = FVector4(1.0f, 2.0f, 3.0f, 0.0f);

        auto m =
            FMatrix4x4::rotationX(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationY(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationZ(MathHelper::ToRadians(30.0f));
        m.M41 = 10.0f;
        m.M42 = 20.0f;
        m.M43 = 30.0f;

        auto expected = FVector4(2.19198728f, 1.53349376f, 2.61602545f, 0.0f);
        FVector4 actual;

        actual = FMatrix4x4::transform(v, m);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Transform did not return the expected value.");

        // 
        v = v.setW(1);

        expected = FVector4(12.19198728f, 21.53349376f, 32.61602545f, 1.0f);
        actual = FMatrix4x4::transform(v, m);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
    });

    // A test for Transform (Vector4f, FMatrix4x4)
    // Transform vector4 with zero matrix

    test("Vector4TransformVector4Test1", []()
    {
        auto v = FVector4(1.0f, 2.0f, 3.0f, 0.0f);
        auto m = FMatrix4x4();
        auto expected = FVector4(0, 0, 0, 0);

        auto actual = FMatrix4x4::transform(v, m);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
    });

    // A test for Transform (Vector4f, FMatrix4x4)
    // Transform vector4 with identity matrix

    test("Vector4TransformVector4Test2", []()
    {
        auto v = FVector4(1.0f, 2.0f, 3.0f, 0.0f);
        auto m = FMatrix4x4::identity();
        auto expected = FVector4(1.0f, 2.0f, 3.0f, 0.0f);

        auto actual = FMatrix4x4::transform(v, m);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
    });

    // A test for Transform (Vector3f, FMatrix4x4)
    // Transform Vector3f test

    test("Vector4TransformVector3Test", []()
    {
        auto v = FVector3(1.0f, 2.0f, 3.0f);

        auto m =
            FMatrix4x4::rotationX(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationY(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationZ(MathHelper::ToRadians(30.0f));
        m.M41 = 10.0f;
        m.M42 = 20.0f;
        m.M43 = 30.0f;

        auto expected = FMatrix4x4::transform(FVector4(v, 1.0f), m);
        auto actual = FMatrix4x4::transformToVector4(v, m);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
    });

    // A test for Transform (Vector3f, FMatrix4x4)
    // Transform vector3 with zero matrix

    test("Vector4TransformVector3Test1", []()
    {
        auto v = FVector3(1.0f, 2.0f, 3.0f);
        auto m = FMatrix4x4();
        auto expected = FVector4(0, 0, 0, 0);

        auto actual = FMatrix4x4::transformToVector4(v, m);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
    });

    // A test for Transform (Vector3f, FMatrix4x4)
    // Transform vector3 with identity matrix

    test("Vector4TransformVector3Test2", []()
    {
        auto v = FVector3(1.0f, 2.0f, 3.0f);
        auto m = FMatrix4x4::identity();
        auto expected = FVector4(1.0f, 2.0f, 3.0f, 1.0f);

        auto actual = FMatrix4x4::transformToVector4(v, m);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
    });

    // A test for Transform (Vector2f, FMatrix4x4)
    // Transform Vector2f test

    test("Vector4TransformVector2Test", []()
    {
        auto v = FVector2(1.0f, 2.0f);

        auto m =
            FMatrix4x4::rotationX(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationY(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationZ(MathHelper::ToRadians(30.0f));
        m.M41 = 10.0f;
        m.M42 = 20.0f;
        m.M43 = 30.0f;

        auto expected = FMatrix4x4::transform(FVector4(v, 0.0f, 1.0f), m);
        auto actual = FMatrix4x4::transformToVector4(v, m);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
    });

    // A test for Transform (Vector2f, FMatrix4x4)
    // Transform Vector2f with zero matrix

    test("Vector4TransformVector2Test1", []()
    {
        auto v = FVector2(1.0f, 2.0f);
        auto m = FMatrix4x4();
        auto expected = FVector4(0, 0, 0, 0);

        auto actual = FMatrix4x4::transformToVector4(v, m);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
    });

    // A test for Transform (Vector2f, FMatrix4x4)
    // Transform vector2 with identity matrix

    test("Vector4TransformVector2Test2", []()
    {
        auto v = FVector2(1.0f, 2.0f);
        auto m = FMatrix4x4::identity();
        auto expected = FVector4(1.0f, 2.0f, 0, 1.0f);

        auto actual = FMatrix4x4::transformToVector4(v, m);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
    });

    // A test for Transform (Vector2f, FQuaternion)

    test("Vector4TransformVector2QuatanionTest", []()
    {
        auto v = FVector2(1.0f, 2.0f);

        auto m =
            FMatrix4x4::rotationX(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationY(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationZ(MathHelper::ToRadians(30.0f));

        auto q = m.quaternion(); 

        auto expected = FMatrix4x4::transformToVector4(v, m);
        FVector4 actual;

        actual = FQuaternion::transformToVector4(v, q);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
    });

    // A test for Transform (Vector3f, FQuaternion)

    test("Vector4TransformVector3Quaternion", []()
    {
        auto v = FVector3(1.0f, 2.0f, 3.0f);

        auto m =
            FMatrix4x4::rotationX(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationY(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationZ(MathHelper::ToRadians(30.0f));
        auto q =  m.quaternion();

        auto expected = FMatrix4x4::transformToVector4(v, m);
        FVector4 actual;

        actual = FQuaternion::transformToVector4(v, q);
        Assert::True(MathHelper::Equal(expected, actual), "mathOps::Transform did not return the expected value.");
    });

    // A test for Transform (Vector4f, FQuaternion)

    test("Vector4TransformVector4QuaternionTest", []()
    {
        auto v = FVector4(1.0f, 2.0f, 3.0f, 0.0f);

        auto m =
            FMatrix4x4::rotationX(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationY(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationZ(MathHelper::ToRadians(30.0f));
        auto q =  m.quaternion();

        auto expected = FMatrix4x4::transform(v, m);
        FVector4 actual;

        actual = FQuaternion::transform(v, q);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Transform did not return the expected value.");

        // 
        v = v.setW(1);
        expected = expected.setW(1);
        actual = FQuaternion::transform(v, q);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
    });

    // A test for Transform (Vector4f, FQuaternion)
    // Transform vector4 with zero quaternion

    test("Vector4TransformVector4QuaternionTest1", []()
    {
        auto v = FVector4(1.0f, 2.0f, 3.0f, 0.0f);
        auto q = FQuaternion();
        auto expected = v;

        auto actual = FQuaternion::transform(v, q);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
    });

    // A test for Transform (Vector4f, FQuaternion)
    // Transform vector4 with identity matrix

    test("Vector4TransformVector4QuaternionTest2", []()
    {
        auto v = FVector4(1.0f, 2.0f, 3.0f, 0.0f);
        auto q = FQuaternion::identity();
        auto expected = FVector4(1.0f, 2.0f, 3.0f, 0.0f);

        auto actual = FQuaternion::transform(v, q);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
    });

    // A test for Transform (Vector3f, FQuaternion)
    // Transform Vector3f test

    test("Vector4TransformVector3QuaternionTest", []()
    {
        auto v = FVector3(1.0f, 2.0f, 3.0f);

        auto m =
            FMatrix4x4::rotationX(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationY(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationZ(MathHelper::ToRadians(30.0f));
        auto q =  m.quaternion();

        auto expected = FMatrix4x4::transformToVector4(v, m);
        auto actual = FQuaternion::transformToVector4(v, q);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
    });

    // A test for Transform (Vector3f, FQuaternion)
    // Transform vector3 with zero quaternion

    test("Vector4TransformVector3QuaternionTest1", []()
    {
        auto v = FVector3(1.0f, 2.0f, 3.0f);
        auto q = FQuaternion();
        auto expected = FVector4(v, 1.0f);

        auto actual = FQuaternion::transformToVector4(v, q);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
    });

    // A test for Transform (Vector3f, FQuaternion)
    // Transform vector3 with identity quaternion

    test("Vector4TransformVector3QuaternionTest2", []()
    {
        auto v = FVector3(1.0f, 2.0f, 3.0f);
        auto q = FQuaternion::identity();
        auto expected = FVector4(1.0f, 2.0f, 3.0f, 1.0f);

        auto actual = FQuaternion::transformToVector4(v, q);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
    });

    // A test for Transform (Vector2f, FQuaternion)
    // Transform Vector2f by quaternion test

    test("Vector4TransformVector2QuaternionTest", []()
    {
        auto v = FVector2(1.0f, 2.0f);

        auto m =
            FMatrix4x4::rotationX(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationY(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationZ(MathHelper::ToRadians(30.0f));
        auto q =  m.quaternion();

        auto expected = FMatrix4x4::transformToVector4(v, m);
        auto actual = FQuaternion::transformToVector4(v, q);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
    });

    // A test for Transform (Vector2f, FQuaternion)
    // Transform Vector2f with zero quaternion

    test("Vector4TransformVector2QuaternionTest1", []()
    {
        auto v = FVector2(1.0f, 2.0f);
        auto q = FQuaternion();
        auto expected = FVector4(1.0f, 2.0f, 0, 1.0f);

        auto actual = FQuaternion::transformToVector4(v, q);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
    });

    // A test for Transform (Vector2f, FMatrix4x4)
    // Transform vector2 with identity FQuaternion

    test("Vector4TransformVector2QuaternionTest2", []()
    {
        auto v = FVector2(1.0f, 2.0f);
        auto q = FQuaternion::identity();
        auto expected = FVector4(1.0f, 2.0f, 0, 1.0f);

        auto actual = FQuaternion::transformToVector4(v, q);
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Transform did not return the expected value.");
    });

    // A test for Normalize (Vector4f)

    test("Vector4NormalizeTest", []()
    {
        auto a = FVector4(1.0f, 2.0f, 3.0f, 4.0f);

        auto expected = FVector4(
            0.1825741858350553711523232609336f,
            0.3651483716701107423046465218672f,
            0.5477225575051661134569697828008f,
            0.7302967433402214846092930437344f);
        FVector4 actual;

        actual = a.normalize();
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Normalize did not return the expected value.");
    });

    // A test for Normalize (Vector4f)
    // Normalize vector of length one

    test("Vector4NormalizeTest1", []()
    {
        auto a = FVector4(1.0f, 0.0f, 0.0f, 0.0f);

        auto expected = FVector4(1.0f, 0.0f, 0.0f, 0.0f);
        auto actual = a.normalize();
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.Normalize did not return the expected value.");
    });

    // A test for Normalize (Vector4f)
    // Normalize vector of length zero

    test("Vector4NormalizeTest2", []()
    {
        auto a = FVector4(0.0f, 0.0f, 0.0f, 0.0f);

        auto expected = FVector4(0.0f, 0.0f, 0.0f, 0.0f);
        auto actual = a.normalize();
        Assert::True(std::isnan(actual.X) && std::isnan(actual.Y) && std::isnan(actual.Z) && std::isnan(actual.W), "Vector4f.Normalize did not return the expected value.");
    });

    // A test for operator - (Vector4f)

    test("Vector4UnaryNegationTest", []()
    {
        auto a = FVector4(1.0f, 2.0f, 3.0f, 4.0f);

        auto expected = FVector4(-1.0f, -2.0f, -3.0f, -4.0f);
        FVector4 actual;

        actual = -a;

        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.operator - did not return the expected value.");
    });

    // A test for operator - (Vector4f, Vector4f)

    test("Vector4SubtractionTest", []()
    {
        auto a = FVector4(1.0f, 6.0f, 3.0f, 4.0f);
        auto b = FVector4(5.0f, 2.0f, 3.0f, 9.0f);

        auto expected = FVector4(-4.0f, 4.0f, 0.0f, -5.0f);
        FVector4 actual;

        actual = a - b;

        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.operator - did not return the expected value.");
    });

    // A test for operator * (Vector4f, float)

    test("Vector4MultiplyOperatorTest", []()
    {
        auto a = FVector4(1.0f, 2.0f, 3.0f, 4.0f);

        const float factor = 2.0f;

        auto expected = FVector4(2.0f, 4.0f, 6.0f, 8.0f);
        FVector4 actual;

        actual = a * factor;
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.operator * did not return the expected value.");
    });

    // A test for operator * (float, Vector4f)

    test("Vector4MultiplyOperatorTest2", []()
    {
        auto a = FVector4(1.0f, 2.0f, 3.0f, 4.0f);

        const float factor = 2.0f;
        auto expected = FVector4(2.0f, 4.0f, 6.0f, 8.0f);
        FVector4 actual;

        actual = factor * a;
        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.operator * did not return the expected value.");
    });

    // A test for operator * (Vector4f, Vector4f)

    test("Vector4MultiplyOperatorTest3", []()
    {
        auto a = FVector4(1.0f, 2.0f, 3.0f, 4.0f);
        auto b = FVector4(5.0f, 6.0f, 7.0f, 8.0f);

        auto expected = FVector4(5.0f, 12.0f, 21.0f, 32.0f);
        FVector4 actual;

        actual = a * b;

        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.operator * did not return the expected value.");
    });

    // A test for operator / (Vector4f, float)

    test("Vector4DivisionTest", []()
    {
        auto a = FVector4(1.0f, 2.0f, 3.0f, 4.0f);

        auto div = 2.0f;

        auto expected = FVector4(0.5f, 1.0f, 1.5f, 2.0f);
        FVector4 actual;

        actual = a / div;

        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.operator / did not return the expected value.");
    });

    // A test for operator / (Vector4f, Vector4f)

    test("Vector4DivisionTest1", []()
    {
        auto a = FVector4(1.0f, 6.0f, 7.0f, 4.0f);
        auto b = FVector4(5.0f, 2.0f, 3.0f, 8.0f);

        auto expected = FVector4(1.0f / 5.0f, 6.0f / 2.0f, 7.0f / 3.0f, 4.0f / 8.0f);
        FVector4 actual;

        actual = a / b;

        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.operator / did not return the expected value.");
    });

    // A test for operator / (Vector4f, Vector4f)
    // Divide by zero

    test("Vector4DivisionTest2", []()
    {
        auto a = FVector4(-2.0f, 3.0f, std::numeric_limits<float>::max(), std::numeric_limits<float>::quiet_NaN());

        auto div = 0.0f;

        auto actual = a / div;

        Assert::True(std::isinf(actual.X) && actual.X < 0, "Vector4f.operator / did not return the expected value.");
        Assert::True(std::isinf(actual.Y) && actual.Y > 0, "Vector4f.operator / did not return the expected value.");
        Assert::True(std::isinf(actual.Z) && actual.Z > 0, "Vector4f.operator / did not return the expected value.");
        Assert::True(std::isnan(actual.W), "Vector4f.operator / did not return the expected value.");
    });

    // A test for operator / (Vector4f, Vector4f)
    // Divide by zero

    test("Vector4DivisionTest3", []()
    {
        auto a = FVector4(0.047f, -3.0f, -std::numeric_limits<float>::infinity(), std::numeric_limits<float>::lowest());
        auto b = FVector4();

        auto actual = a / b;

        Assert::True(std::isinf(actual.X) && actual.X > 0, "Vector4f.operator / did not return the expected value.");
        Assert::True(std::isinf(actual.Y) && actual.Y < 0, "Vector4f.operator / did not return the expected value.");
        Assert::True(std::isinf(actual.Z) && actual.Z < 0, "Vector4f.operator / did not return the expected value.");
        Assert::True(std::isinf(actual.W) && actual.W < 0, "Vector4f.operator / did not return the expected value.");
        //    Assert::True(float.IsNegativeInfinity(actual.W), "Vector4f.operator / did not return the expected value.");
    });

    // A test for operator + (Vector4f, Vector4f)

    test("Vector4AdditionTest", []()
    {
        auto a = FVector4(1.0f, 2.0f, 3.0f, 4.0f);
        auto b = FVector4(5.0f, 6.0f, 7.0f, 8.0f);

        auto expected = FVector4(6.0f, 8.0f, 10.0f, 12.0f);
        FVector4 actual;

        actual = a + b;

        Assert::True(MathHelper::Equal(expected, actual), "Vector4f.operator + did not return the expected value.");
    });


    test("OperatorAddTest", []()
    {
        auto v1 = FVector4(2.5f, 2.0f, 3.0f, 3.3f);
        auto v2 = FVector4(5.5f, 4.5f, 6.5f, 7.5f);

        auto v3 = v1 + v2;
        auto v5 = FVector4(-1.0f, 0.0f, 0.0f, std::numeric_limits<float>::quiet_NaN());
        auto v4 = v1 + v5;
        Assert::AreEqual(8.0f, v3.X);
        Assert::AreEqual(6.5f, v3.Y);
        Assert::AreEqual(9.5f, v3.Z);
        Assert::AreEqual(10.8f, v3.W);
        Assert::AreEqual(1.5f, v4.X);
        Assert::AreEqual(2.0f, v4.Y);
        Assert::AreEqual(3.0f, v4.Z);
        Assert::True(std::isnan(v4.W));
    });

    // A test for Vector4f (float, float, float, float)

    test("Vector4ConstructorTest", []()
    {
        auto x = 1.0f;
        auto y = 2.0f;
        auto z = 3.0f;
        auto w = 4.0f;

        auto target = FVector4(x, y, z, w);

        Assert::True(MathHelper::Equal(target.X, x) && MathHelper::Equal(target.Y, y) && MathHelper::Equal(target.Z, z) && MathHelper::Equal(target.W, w),
            "Vector4f constructor(x,y,z,w) did not return the expected value.");
    });

    // A test for Vector4f (Vector2f, float, float)

    test("Vector4ConstructorTest1", []()
    {
        auto a = FVector2(1.0f, 2.0f);
        auto z = 3.0f;
        auto w = 4.0f;

        auto target = FVector4(a, z, w);
        Assert::True(MathHelper::Equal(target.X, a.X) && MathHelper::Equal(target.Y, a.Y) && MathHelper::Equal(target.Z, z) && MathHelper::Equal(target.W, w),
            "Vector4f constructor(Vector2f,z,w) did not return the expected value.");
    });

    // A test for Vector4f (Vector3f, float)

    test("Vector4ConstructorTest2", []()
    {
        auto a = FVector3(1.0f, 2.0f, 3.0f);
        auto w = 4.0f;

        auto target = FVector4(a, w);

        Assert::True(MathHelper::Equal(target.X, a.X) && MathHelper::Equal(target.Y, a.Y) && MathHelper::Equal(target.Z, a.Z) && MathHelper::Equal(target.W, w),
            "Vector4f constructor(Vector3f,w) did not return the expected value.");
    });

    // A test for Vector4f ()
    // Constructor with no parameter

    test("Vector4ConstructorTest4", []()
    {
        auto a = FVector4();

        Assert::AreEqual(a.X, 0.0f);
        Assert::AreEqual(a.Y, 0.0f);
        Assert::AreEqual(a.Z, 0.0f);
        Assert::AreEqual(a.W, 0.0f);
    });

    // A test for Vector4f ()
    // Constructor with special floating values

    test("Vector4ConstructorTest5", []()
    {
        auto target = FVector4(std::numeric_limits<float>::quiet_NaN(), std::numeric_limits<float>::max(), std::numeric_limits<float>::infinity(), std::numeric_limits<float>::epsilon());

        Assert::True(std::isnan(target.X), "Vector4f.constructor (float, float, float, float) did not return the expected value.");
        Assert::True((std::numeric_limits<float>::max() == target.Y), "Vector4f.constructor (float, float, float, float) did not return the expected value.");
        Assert::True(std::isinf(target.Z) && target.Z > 0, "Vector4f.constructor (float, float, float, float) did not return the expected value.");
        Assert::True((std::numeric_limits<float>::epsilon() == target.W), "Vector4f.constructor (float, float, float, float) did not return the expected value.");
    });

    // A test for Add (Vector4f, Vector4f)

    test("Vector4AddTest", []()
    {
        auto a = FVector4(1.0f, 2.0f, 3.0f, 4.0f);
        auto b = FVector4(5.0f, 6.0f, 7.0f, 8.0f);

        auto expected = FVector4(6.0f, 8.0f, 10.0f, 12.0f);
        FVector4 actual;

        actual = a.add(b);
        Assert::AreEqual(expected, actual);
    });

    // A test for Divide (Vector4f, float)

    test("Vector4DivideTest", []()
    {
        auto a = FVector4(1.0f, 2.0f, 3.0f, 4.0f);
        auto div = 2.0f;
        auto expected = FVector4(0.5f, 1.0f, 1.5f, 2.0f);
        Assert::AreEqual(expected, a / div);
    });

    // A test for Divide (Vector4f, Vector4f)

    test("Vector4DivideTest1", []()
    {
        auto a = FVector4(1.0f, 6.0f, 7.0f, 4.0f);
        auto b = FVector4(5.0f, 2.0f, 3.0f, 8.0f);

        auto expected = FVector4(1.0f / 5.0f, 6.0f / 2.0f, 7.0f / 3.0f, 4.0f / 8.0f);
        FVector4 actual;

        actual = a.divide(b);
        Assert::AreEqual(expected, actual);
    });

    // A test for Equals (object)

    test("Vector4EqualsTest", []()
    {
        auto a = FVector4(1.0f, 2.0f, 3.0f, 4.0f);
        auto b = FVector4(1.0f, 2.0f, 3.0f, 4.0f);

        // case 1: compare between same values
        auto obj = b;

        bool expected = true;
        bool actual = a == obj;
        Assert::AreEqual(expected, actual);

        // case 2: compare between different values
        b = b.setX(10);
        obj = b;
        expected = false;
        actual = a == obj;
        Assert::AreEqual(expected, actual);

        //// case 3: compare between different types.
        //obj = FQuaternion();
        //expected = false;
        //actual = a == obj;
        //Assert::AreEqual(expected, actual);

        //// case 3: compare against null.
        ////obj = null;
        //expected = false;
        //actual = a == obj;
        Assert::AreEqual(expected, actual);
    });

    // A test for Multiply (float, Vector4f)

    test("Vector4MultiplyTest", []()
    {
        auto a = FVector4(1.0f, 2.0f, 3.0f, 4.0f);
        const float factor = 2.0f;
        auto expected = FVector4(2.0f, 4.0f, 6.0f, 8.0f);
        Assert::AreEqual(expected, factor * a);
    });

    // A test for Multiply (Vector4f, float)

    test("Vector4MultiplyTest2", []()
    {
        auto a = FVector4(1.0f, 2.0f, 3.0f, 4.0f);
        const float factor = 2.0f;
        auto expected = FVector4(2.0f, 4.0f, 6.0f, 8.0f);
        Assert::AreEqual(expected, a * factor);
    });

    // A test for Multiply (Vector4f, Vector4f)

    test("Vector4MultiplyTest3", []()
    {
        auto a = FVector4(1.0f, 2.0f, 3.0f, 4.0f);
        auto b = FVector4(5.0f, 6.0f, 7.0f, 8.0f);

        auto expected = FVector4(5.0f, 12.0f, 21.0f, 32.0f);
        FVector4 actual;

        actual = a.multiply(b);
        Assert::AreEqual(expected, actual);
    });

    // A test for Negate (Vector4f)

    test("Vector4NegateTest", []()
    {
        auto a = FVector4(1.0f, 2.0f, 3.0f, 4.0f);

        auto expected = FVector4(-1.0f, -2.0f, -3.0f, -4.0f);
        FVector4 actual;

        actual = a.negate();
        Assert::AreEqual(expected, actual);
    });

    // A test for operator != (Vector4f, Vector4f)

    test("Vector4InequalityTest", []()
    {
        auto a = FVector4(1.0f, 2.0f, 3.0f, 4.0f);
        auto b = FVector4(1.0f, 2.0f, 3.0f, 4.0f);

        // case 1: compare between same values
        auto expected = false;
        auto actual = a != b;
        Assert::AreEqual(expected, actual);

        // case 2: compare between different values
        b = b.setX(10);
        expected = true;
        actual = a != b;
        Assert::AreEqual(expected, actual);
    });

    // A test for operator == (Vector4f, Vector4f)

    test("Vector4EqualityTest", []()
    {
        auto a = FVector4(1.0f, 2.0f, 3.0f, 4.0f);
        auto b = FVector4(1.0f, 2.0f, 3.0f, 4.0f);

        // case 1: compare between same values
        auto expected = true;
        auto actual = a == b;
        Assert::AreEqual(expected, actual);

        // case 2: compare between different values
        b = b.setX(10);
        expected = false;
        actual = a == b;
        Assert::AreEqual(expected, actual);
    });

    // A test for Subtract (Vector4f, Vector4f)

    test("Vector4SubtractTest", []()
    {
        auto a = FVector4(1.0f, 6.0f, 3.0f, 4.0f);
        auto b = FVector4(5.0f, 2.0f, 3.0f, 9.0f);

        auto expected = FVector4(-4.0f, 4.0f, 0.0f, -5.0f);
        FVector4 actual;

        actual = a.subtract(b);

        Assert::AreEqual(expected, actual);
    });

    // A test for UnitW

    test("Vector4UnitWTest", []()
    {
        auto val = FVector4(0.0f, 0.0f, 0.0f, 1.0f);
        Assert::AreEqual(val, FVector4::unitW());
    });

    // A test for UnitX

    test("Vector4UnitXTest", []()
    {
        auto val = FVector4(1.0f, 0.0f, 0.0f, 0.0f);
        Assert::AreEqual(val, FVector4::unitX());
    });

    // A test for UnitY

    test("Vector4UnitYTest", []()
    {
        auto val = FVector4(0.0f, 1.0f, 0.0f, 0.0f);
        Assert::AreEqual(val, FVector4::unitY());
    });

    // A test for UnitZ

    test("Vector4UnitZTest", []()
    {
        auto val = FVector4(0.0f, 0.0f, 1.0f, 0.0f);
        Assert::AreEqual(val, FVector4::unitZ());
    });

    // A test for One

    test("Vector4OneTest", []()
    {
        auto val = FVector4(1.0f, 1.0f, 1.0f, 1.0f);
        Assert::AreEqual(val, FVector4::one());
    });

    // A test for Zero

    test("Vector4ZeroTest", []()
    {
        auto val = FVector4(0.0f, 0.0f, 0.0f, 0.0f);
        Assert::AreEqual(val, FVector4::zero());
    });

    // A test for Equals (Vector4f)

    test("Vector4EqualsTest1", []()
    {
        auto a = FVector4(1.0f, 2.0f, 3.0f, 4.0f);
        auto b = FVector4(1.0f, 2.0f, 3.0f, 4.0f);

        // case 1: compare between same values
        Assert::True(a == b);

        // case 2: compare between different values
        b = b.setX(10);
        Assert::False(a == b);
    });

    // A test for Vector4f (float)

    test("Vector4ConstructorTest6", []()
    {
        auto value = 1.0f;
        auto target = FVector4(value);

        auto expected = FVector4(value, value, value, value);
        Assert::AreEqual(expected, target);

        value = 2.0f;
        target = FVector4(value);
        expected = FVector4(value, value, value, value);
        Assert::AreEqual(expected, target);
    });

    // A test for Vector4f comparison involving NaN values

    test("Vector4EqualsNanTest", []()
    {
        auto a = FVector4(std::numeric_limits<float>::quiet_NaN(), 0, 0, 0);
        auto b = FVector4(0, std::numeric_limits<float>::quiet_NaN(), 0, 0);
        auto c = FVector4(0, 0, std::numeric_limits<float>::quiet_NaN(), 0);
        auto d = FVector4(0, 0, 0, std::numeric_limits<float>::quiet_NaN());

        Assert::False(a == FVector4::zero());
        Assert::False(b == FVector4::zero());
        Assert::False(c == FVector4::zero());
        Assert::False(d == FVector4::zero());

        Assert::True(a != FVector4::zero());
        Assert::True(b != FVector4::zero());
        Assert::True(c != FVector4::zero());
        Assert::True(d != FVector4::zero());

        Assert::False(a == FVector4::zero());
        Assert::False(b == FVector4::zero());
        Assert::False(c == FVector4::zero());
        Assert::False(d == FVector4::zero());

        // Counterintuitive result - IEEE rules for NaN comparison are weird!
        Assert::False(a == (a));
        Assert::False(b == (b));
        Assert::False(c == (c));
        Assert::False(d == (d));
    });


    test("Vector4AbsTest", []()
    {
        auto v1 = FVector4(-2.5f, 2.0f, 3.0f, 3.3f);
        auto v3 = FVector4(std::numeric_limits<float>::infinity(), 0.0f, -std::numeric_limits<float>::infinity(), std::numeric_limits<float>::quiet_NaN()).abs();
        auto v = v1.abs();
        Assert::AreEqual(2.5f, v.X);
        Assert::AreEqual(2.0f, v.Y);
        Assert::AreEqual(3.0f, v.Z);
        Assert::AreEqual(3.3f, v.W);
        Assert::True(std::isinf(v3.X));
        Assert::AreEqual(0.0f, v3.Y);
        Assert::True(std::isinf(v3.Z));
        Assert::True(std::isnan(v3.W));
    });


    test("Vector4SqrtTest", []()
    {
        auto v1 = FVector4(-2.5f, 2.0f, 3.0f, 3.3f);
        auto v2 = FVector4(5.5f, 4.5f, 6.5f, 7.5f);
        Assert::AreEqual(2, (int)v2.squareRoot().X);
        Assert::AreEqual(2, (int)v2.squareRoot().Y);
        Assert::AreEqual(2, (int)v2.squareRoot().Z);
        Assert::AreEqual(2, (int)v2.squareRoot().W);
        Assert::True(std::isnan(v1.squareRoot().X));
    });

#pragma endregion

#pragma region Matrix4x4Tests
    std::cout << "Matrix4x4 Tests" << std::endl;

    // A test for Identity
    test("Matrix4x4IdentityTest", [] ()
    {
        auto val = FMatrix4x4();
        val.M11 = val.M22 = val.M33 = val.M44 = 1.0f;

        Assert::True(MathHelper::Equal(val, FMatrix4x4::identity()), "FMatrix4x4::Indentity was not set correctly.");
    });

    // A test for Determinant   
    test("Matrix4x4DeterminantTest", [] ()
    {
        auto target =
                FMatrix4x4::rotationX(MathHelper::ToRadians(30.0f)) *
                FMatrix4x4::rotationY(MathHelper::ToRadians(30.0f)) *
                FMatrix4x4::rotationZ(MathHelper::ToRadians(30.0f));

        auto val = 1.0f;
        auto det = target.getDeterminant();

        Assert::True(MathHelper::Equal(val, det), "FMatrix4x4::Determinant was not set correctly.");
    });

    // A test for Determinant
    // Determinant test |A| = 1 / |A'|  
    test("Matrix4x4DeterminantTest1", [] ()
    {
        auto a = FMatrix4x4();
        a.M11 = 5.0f;
        a.M12 = 2.0f;
        a.M13 = 8.25f;
        a.M14 = 1.0f;
        a.M21 = 12.0f;
        a.M22 = 6.8f;
        a.M23 = 2.14f;
        a.M24 = 9.6f;
        a.M31 = 6.5f;
        a.M32 = 1.0f;
        a.M33 = 3.14f;
        a.M34 = 2.22f;
        a.M41 = 0;
        a.M42 = 0.86f;
        a.M43 = 4.0f;
        a.M44 = 1.0f;

        auto inv = a.invert();
        Assert::True(inv.has_value());

        auto detA = a.getDeterminant();
        auto detI = inv.value().getDeterminant();
        auto t = 1.0f / detI;

        // only accurate to 3 precision
        Assert::True(std::fabs(detA - t) < 1e-3, "FMatrix4x4::Determinant was not set correctly.");
    });

    // A test for Invert (FMatrix4x4)
    test("Matrix4x4InvertTest", [] ()
    {
        auto mtx =
            FMatrix4x4::rotationX(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationY(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationZ(MathHelper::ToRadians(30.0f));

        auto expected = FMatrix4x4();
        expected.M11 = 0.74999994f;
        expected.M12 = -0.216506317f;
        expected.M13 = 0.62499994f;
        expected.M14 = 0.0f;

        expected.M21 = 0.433012635f;
        expected.M22 = 0.87499994f;
        expected.M23 = -0.216506317f;
        expected.M24 = 0.0f;

        expected.M31 = -0.49999997f;
        expected.M32 = 0.433012635f;
        expected.M33 = 0.74999994f;
        expected.M34 = 0.0f;

        expected.M41 = 0.0f;
        expected.M42 = 0.0f;
        expected.M43 = 0.0f;
        expected.M44 = 0.99999994f;

        auto inv = mtx.invert();
        auto actual = inv.value();

        Assert::True(inv.has_value());
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::Invert did not return the expected value.");

        // Make sure M*M is identity matrix
        auto i = mtx * actual;
        Assert::True(MathHelper::Equal(i, FMatrix4x4::identity()), "FMatrix4x4::Invert did not return the expected value.");
    });

    // A test for Invert (FMatrix4x4)
    test("Matrix4x4InvertIdentityTest", [] ()
    {
        auto mtx = FMatrix4x4::identity();
        auto inv = mtx.invert();
        
        Assert::True(inv.has_value());
        Assert::True(MathHelper::Equal(inv.value(), FMatrix4x4::identity()));
    });

    // A test for Invert (FMatrix4x4)
    test("Matrix4x4InvertTranslationTest", [] ()
    {
        auto mtx = FMatrix4x4::translation(23, 42, 666);
        auto inv = mtx.invert();
        
        Assert::True(inv.has_value());

        auto i = mtx * inv.value();
        Assert::True(MathHelper::Equal(i, FMatrix4x4::identity()));
    });

    // A test for Invert (FMatrix4x4)
    test("Matrix4x4InvertRotationTest", [] ()
    {
        auto mtx = FMatrix4x4::fromYawPitchRoll(3, 4, 5);
        auto inv = mtx.invert();

        Assert::True(inv.has_value());

        auto i = mtx * inv.value();
        Assert::True(MathHelper::Equal(i, FMatrix4x4::identity()));
    });

    // A test for Invert (FMatrix4x4)
    test("Matrix4x4InvertScaleTest", [] ()
    {
        auto mtx = FMatrix4x4::scale(23, 42, -666);
        auto inv = mtx.invert();

        Assert::True(inv.has_value());

        auto i = mtx * inv.value();
        Assert::True(MathHelper::Equal(i, FMatrix4x4::identity()));
    });

    // A test for Invert (FMatrix4x4)
    test("Matrix4x4InvertProjectionTest", [] ()
    {
        auto mtx = FMatrix4x4::perspectiveFieldOfView(1, 1.333f, 0.1f, 666);
        auto inv = mtx.invert();

        Assert::True(inv.has_value());

        auto i = mtx * inv.value();
        Assert::True(MathHelper::Equal(i, FMatrix4x4::identity()));
    });

    // A test for Invert (FMatrix4x4)
    test("Matrix4x4InvertAffineTest", [] ()
    {
        auto mtx = FMatrix4x4::fromYawPitchRoll(3, 4, 5) *
                        FMatrix4x4::scale(23, 42, -666) *
                        FMatrix4x4::translation(17, 53, 89);
        auto inv = mtx.invert();

        Assert::True(inv.has_value());

        auto i = mtx * inv.value();
        Assert::True(MathHelper::Equal(i, FMatrix4x4::identity()));
    });

    // Various rotation decompose test.
    test("Matrix4x4DecomposeTest01", [] ()
    {
        DecomposeTest(10.0f, 20.0f, 30.0f, FVector3(10, 20, 30), FVector3(2, 3, 4));

        const float step = 35.0f;

        for (auto yawAngle = -720.0f; yawAngle <= 720.0f; yawAngle += step)
        {
            for (auto pitchAngle = -720.0f; pitchAngle <= 720.0f; pitchAngle += step)
            {
                for (auto rollAngle = -720.0f; rollAngle <= 720.0f; rollAngle += step)
                {
                    DecomposeTest(yawAngle, pitchAngle, rollAngle, FVector3(10, 20, 30), FVector3(2, 3, 4));
                }
            }
        }
    });

    // Various scaled matrix decompose test.
    test("Matrix4x4DecomposeTest02", [] ()
    {
        DecomposeTest(10.0f, 20.0f, 30.0f, FVector3(10, 20, 30), FVector3(2, 3, 4));

        // Various scales.
        DecomposeTest(0, 0, 0, FVector3::zero(), FVector3(1, 2, 3));
        DecomposeTest(0, 0, 0, FVector3::zero(), FVector3(1, 3, 2));
        DecomposeTest(0, 0, 0, FVector3::zero(), FVector3(2, 1, 3));
        DecomposeTest(0, 0, 0, FVector3::zero(), FVector3(2, 3, 1));
        DecomposeTest(0, 0, 0, FVector3::zero(), FVector3(3, 1, 2));
        DecomposeTest(0, 0, 0, FVector3::zero(), FVector3(3, 2, 1));

        DecomposeTest(0, 0, 0, FVector3::zero(), FVector3(-2, 1, 1));

        // Small scales.
        DecomposeTest(0, 0, 0, FVector3::zero(), FVector3(1e-4f, 2e-4f, 3e-4f));
        DecomposeTest(0, 0, 0, FVector3::zero(), FVector3(1e-4f, 3e-4f, 2e-4f));
        DecomposeTest(0, 0, 0, FVector3::zero(), FVector3(2e-4f, 1e-4f, 3e-4f));
        DecomposeTest(0, 0, 0, FVector3::zero(), FVector3(2e-4f, 3e-4f, 1e-4f));
        DecomposeTest(0, 0, 0, FVector3::zero(), FVector3(3e-4f, 1e-4f, 2e-4f));
        DecomposeTest(0, 0, 0, FVector3::zero(), FVector3(3e-4f, 2e-4f, 1e-4f));

        // Zero scales.
        DecomposeTest(0, 0, 0, FVector3(10, 20, 30), FVector3(0, 0, 0));
        DecomposeTest(0, 0, 0, FVector3(10, 20, 30), FVector3(1, 0, 0));
        DecomposeTest(0, 0, 0, FVector3(10, 20, 30), FVector3(0, 1, 0));
        DecomposeTest(0, 0, 0, FVector3(10, 20, 30), FVector3(0, 0, 1));
        DecomposeTest(0, 0, 0, FVector3(10, 20, 30), FVector3(0, 1, 1));
        DecomposeTest(0, 0, 0, FVector3(10, 20, 30), FVector3(1, 0, 1));
        DecomposeTest(0, 0, 0, FVector3(10, 20, 30), FVector3(1, 1, 0));

        // Negative scales.
        DecomposeTest(0, 0, 0, FVector3(10, 20, 30), FVector3(-1, -1, -1));
        DecomposeTest(0, 0, 0, FVector3(10, 20, 30), FVector3(1, -1, -1));
        DecomposeTest(0, 0, 0, FVector3(10, 20, 30), FVector3(-1, 1, -1));
        DecomposeTest(0, 0, 0, FVector3(10, 20, 30), FVector3(-1, -1, 1));
        DecomposeTest(0, 0, 0, FVector3(10, 20, 30), FVector3(-1, 1, 1));
        DecomposeTest(0, 0, 0, FVector3(10, 20, 30), FVector3(1, -1, 1));
        DecomposeTest(0, 0, 0, FVector3(10, 20, 30), FVector3(1, 1, -1));
    });

    // Tiny scale decompose test.
    test("Matrix4x4DecomposeTest03", [] ()
    {
        DecomposeScaleTest(1, 2e-4f, 3e-4f);
        DecomposeScaleTest(1, 3e-4f, 2e-4f);
        DecomposeScaleTest(2e-4f, 1, 3e-4f);
        DecomposeScaleTest(2e-4f, 3e-4f, 1);
        DecomposeScaleTest(3e-4f, 1, 2e-4f);
        DecomposeScaleTest(3e-4f, 2e-4f, 1);
    });

    // Simple scale extraction test.
    test("Matrix4x4ExtractScaleTest", [] ()
    {
        ExtractScaleTest(FVector3(1, 2, 1), FVector3::zero());
        ExtractScaleTest(FVector3(-1, 2, 1), FVector3::zero());
        ExtractScaleTest(FVector3(-1, 2, -1), FVector3::zero());

        ExtractScaleTest(FVector3(1, 2, 0.75f), FVector3::unitX());
        ExtractScaleTest(FVector3(1, 2, 0.75f), FVector3::unitY());
        ExtractScaleTest(FVector3(1, 2, 0.75f), FVector3::unitZ());

        ExtractScaleTest(FVector3(1, 2, 0.75f), -FVector3::unitX());
        ExtractScaleTest(FVector3(1, 2, 0.75f), -FVector3::unitY());
        ExtractScaleTest(FVector3(1, 2, 0.75f), -FVector3::unitZ());

        ExtractScaleTest(FVector3(-1, 2, 0.75f), -FVector3::unitX());
        ExtractScaleTest(FVector3(1, -2, -0.75f), -FVector3::unitY());
        ExtractScaleTest(FVector3(1, 2, -0.75f), -FVector3::unitZ());

        // Note, for more complex rotations the extraction will not return the same scale
        // These scenarios could potentially be handled by figuring out which of the
        // axis are still in the same RH configuration, but that is a bit beyond the current scope
        // and would be better handled by a full decomposition.
        //ExtractScaleTest(FVector3(1, 2, 0.75f), FVector3(.5f, 0.3f, 1.75f));
    });
 
    test("Matrix4x4DecomposeTest04", [] ()
    {
        FVector3 scales;
        FQuaternion rotation;
        FVector3 translation;

        Assert::False(FMatrix4x4::decompose(GenerateMatrixNumberFrom1To16(), scales, rotation, translation), "decompose should have failed.");
    });

    // Transform by quaternion test
    test("Matrix4x4TransformTest", [] ()
    {
        auto target = GenerateMatrixNumberFrom1To16();
        auto m =
            FMatrix4x4::rotationX(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationY(MathHelper::ToRadians(30.0f)) *
            FMatrix4x4::rotationZ(MathHelper::ToRadians(30.0f));

        auto q = m.quaternion();

        auto expected = target * m;
        FMatrix4x4 actual;
        actual = target.transform(q);
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::Transform did not return the expected value.");
    });

    // A test for CreateRotationX (float) 
    test("Matrix4x4CreateRotationXTest", [] ()
    {
        auto radians = MathHelper::ToRadians(30.0f);
        auto expected = FMatrix4x4();

        expected.M11 = 1.0f;
        expected.M22 = 0.8660254f;
        expected.M23 = 0.5f;
        expected.M32 = -0.5f;
        expected.M33 = 0.8660254f;
        expected.M44 = 1.0f;

        FMatrix4x4 actual;

        actual = FMatrix4x4::rotationX(radians);
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::rotationX did not return the expected value.");
    });

    // A test for CreateRotationX (float)
    // CreateRotationX of zero degree 
    test("Matrix4x4CreateRotationXTest1", [] ()
    {
        float radians = 0;

        auto expected = FMatrix4x4::identity();
        auto actual = FMatrix4x4::rotationX(radians);
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::rotationX did not return the expected value.");
    });

    // A test for CreateRotationX (float, Vector3f) 
    test("Matrix4x4CreateRotationXCenterTest", [] ()
    {
        auto radians = MathHelper::ToRadians(30.0f);
        auto center = FVector3(23, 42, 66);

        auto rotateAroundZero = FMatrix4x4::rotationX(radians, FVector3::zero());
        auto rotateAroundZeroExpected = FMatrix4x4::rotationX(radians);
        Assert::True(MathHelper::Equal(rotateAroundZero, rotateAroundZeroExpected));

        auto rotateAroundCenter = FMatrix4x4::rotationX(radians, center);
        auto rotateAroundCenterExpected = FMatrix4x4::translation(-center) * FMatrix4x4::rotationX(radians) * FMatrix4x4::translation(center);
        Assert::True(MathHelper::Equal(rotateAroundCenter, rotateAroundCenterExpected));
    });

    // A test for CreateRotationY (float)
    test("Matrix4x4CreateRotationYTest", [] ()
    {
        auto radians = MathHelper::ToRadians(60.0f);

        auto expected = FMatrix4x4();

        expected.M11 = 0.49999997f;
        expected.M13 = -0.866025448f;
        expected.M22 = 1.0f;
        expected.M31 = 0.866025448f;
        expected.M33 = 0.49999997f;
        expected.M44 = 1.0f;

        FMatrix4x4 actual;
        actual = FMatrix4x4::rotationY(radians);
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::rotationY did not return the expected value.");
    });

    // A test for RotationY (float)
    // CreateRotationY test for negative angle
    test("Matrix4x4CreateRotationYTest1", [] ()
    {
        auto radians = MathHelper::ToRadians(-300.0f);

        auto expected = FMatrix4x4();

        expected.M11 = 0.49999997f;
        expected.M13 = -0.866025448f;
        expected.M22 = 1.0f;
        expected.M31 = 0.866025448f;
        expected.M33 = 0.49999997f;
        expected.M44 = 1.0f;

        auto actual = FMatrix4x4::rotationY(radians);
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::rotationY did not return the expected value.");
    });

    // A test for CreateRotationY (float, Vector3f)
    test("Matrix4x4CreateRotationYCenterTest", [] ()
    {
        auto radians = MathHelper::ToRadians(30.0f);
        auto center = FVector3(23, 42, 66);

        auto rotateAroundZero = FMatrix4x4::rotationY(radians, FVector3::zero());
        auto rotateAroundZeroExpected = FMatrix4x4::rotationY(radians);
        Assert::True(MathHelper::Equal(rotateAroundZero, rotateAroundZeroExpected));

        auto rotateAroundCenter = FMatrix4x4::rotationY(radians, center);
        auto rotateAroundCenterExpected = FMatrix4x4::translation(-center) * FMatrix4x4::rotationY(radians) * FMatrix4x4::translation(center);
        Assert::True(MathHelper::Equal(rotateAroundCenter, rotateAroundCenterExpected));
    });

    // A test for CreateFromAxisAngle(Vector3f,float)
    test("Matrix4x4CreateFromAxisAngleTest", [] ()
    {
        auto radians = MathHelper::ToRadians(-30.0f);

        auto expected = FMatrix4x4::rotationX(radians);
        auto actual = FMatrix4x4::fromAxisAngle(FVector3::unitX(), radians);
        Assert::True(MathHelper::Equal(expected, actual));

        expected = FMatrix4x4::rotationY(radians);
        actual = FMatrix4x4::fromAxisAngle(FVector3::unitY(), radians);
        Assert::True(MathHelper::Equal(expected, actual));

        expected = FMatrix4x4::rotationZ(radians);
        actual = FMatrix4x4::fromAxisAngle(FVector3::unitZ(), radians);
        Assert::True(MathHelper::Equal(expected, actual));

        expected = FMatrix4x4::fromQuaternion(FQuaternion::fromAxisAngle(FVector3::one().normalize(), radians));
        actual = FMatrix4x4::fromAxisAngle(FVector3::one().normalize(), radians);
        Assert::True(MathHelper::Equal(expected, actual));

        const int rotCount = 16;
        for (auto i = 0; i < rotCount; ++i)
        {
            auto latitude = (2.0f * MathHelper::pi) * (i / (float)rotCount);
            for (auto j = 0; j < rotCount; ++j)
            {
                auto longitude = -MathHelper::piOver2 + MathHelper::pi * (j / (float)rotCount);

                auto m = FMatrix4x4::rotationZ(longitude) * FMatrix4x4::rotationY(latitude);
                auto axis = FVector3(m.M11, m.M12, m.M13);
                for (auto k = 0; k < rotCount; ++k)
                {
                    auto rot = (2.0f * MathHelper::pi) * (k / (float)rotCount);
                    expected = FMatrix4x4::fromQuaternion(FQuaternion::fromAxisAngle(axis, rot));
                    actual = FMatrix4x4::fromAxisAngle(axis, rot);
                    Assert::True(MathHelper::Equal(expected, actual));
                }
            }
        }
    });

    test("Matrix4x4CreateFromYawPitchRollTest1", [] ()
    {
        auto yawAngle = MathHelper::ToRadians(30.0f);
        auto pitchAngle = MathHelper::ToRadians(40.0f);
        auto rollAngle = MathHelper::ToRadians(50.0f);

        auto yaw = FMatrix4x4::fromAxisAngle(FVector3::unitY(), yawAngle);
        auto pitch = FMatrix4x4::fromAxisAngle(FVector3::unitX(), pitchAngle);
        auto roll = FMatrix4x4::fromAxisAngle(FVector3::unitZ(), rollAngle);

        auto expected = roll * pitch * yaw;
        auto actual = FMatrix4x4::fromYawPitchRoll(yawAngle, pitchAngle, rollAngle);
        Assert::True(MathHelper::Equal(expected, actual));
    });

    // Covers more numeric rigions
    test("Matrix4x4CreateFromYawPitchRollTest2", [] ()
    {
        const float step = 35.0f;

        for (auto yawAngle = -720.0f; yawAngle <= 720.0f; yawAngle += step)
        {
            for (auto pitchAngle = -720.0f; pitchAngle <= 720.0f; pitchAngle += step)
            {
                for (auto rollAngle = -720.0f; rollAngle <= 720.0f; rollAngle += step)
                {
                    auto yawRad = MathHelper::ToRadians(yawAngle);
                    auto pitchRad = MathHelper::ToRadians(pitchAngle);
                    auto rollRad = MathHelper::ToRadians(rollAngle);
                    auto yaw = FMatrix4x4::fromAxisAngle(FVector3::unitY(), yawRad);
                    auto pitch = FMatrix4x4::fromAxisAngle(FVector3::unitX(), pitchRad);
                    auto roll = FMatrix4x4::fromAxisAngle(FVector3::unitZ(), rollRad);

                    auto expected = roll * pitch * yaw;
                    auto actual = FMatrix4x4::fromYawPitchRoll(yawRad, pitchRad, rollRad);
                    Assert::True(MathHelper::Equal(expected, actual));
                    //Assert::True(MathHelper::Equal(expected, actual), string.Format("Yaw:{0} Pitch:{1} Roll:{2}", yawAngle, pitchAngle, rollAngle));
                }
            }
        }
    });

    // Simple shadow test.
    test("Matrix4x4CreateShadowTest01", [] ()
    {
        auto lightDir = FVector3::unitY();
        auto plane = FPlane(FVector3::unitY(), 0);

        auto expected = FMatrix4x4::scale(1, 0, 1);

        auto actual = FMatrix4x4::shadow(lightDir, plane);
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::shadow did not returned expected value.");
    });

    // Various plane projections.
    test("Matrix4x4CreateShadowTest02", [] ()
    {
        // Complex cases.
        FPlane planes[5] = {
            FPlane( 0, 1, 0, 0 ),
            FPlane( 1, 2, 3, 4 ),
            FPlane( 5, 6, 7, 8 ),
            FPlane(-1,-2,-3,-4 ),
            FPlane(-5,-6,-7,-8 ),
        };

        FVector3 points[6] = {
            FVector3( 1, 2, 3),
            FVector3( 5, 6, 7),
            FVector3( 8, 9, 10),
            FVector3(-1,-2,-3),
            FVector3(-5,-6,-7),
            FVector3(-8,-9,-10),
        };
        
        for (const FPlane& p : planes) {
            auto plane = FPlane::normalize(p);

            // Try various direction of light directions.
            FVector3 testDirections[27] =
            {
                FVector3( -1.0f, 1.0f, 1.0f ),
                FVector3(  0.0f, 1.0f, 1.0f ),
                FVector3(  1.0f, 1.0f, 1.0f ),
                FVector3( -1.0f, 0.0f, 1.0f ),
                FVector3(  0.0f, 0.0f, 1.0f ),
                FVector3(  1.0f, 0.0f, 1.0f ),
                FVector3( -1.0f,-1.0f, 1.0f ),
                FVector3(  0.0f,-1.0f, 1.0f ),
                FVector3(  1.0f,-1.0f, 1.0f ),

                FVector3( -1.0f, 1.0f, 0.0f ),
                FVector3(  0.0f, 1.0f, 0.0f ),
                FVector3(  1.0f, 1.0f, 0.0f ),
                FVector3( -1.0f, 0.0f, 0.0f ),
                FVector3(  0.0f, 0.0f, 0.0f ),
                FVector3(  1.0f, 0.0f, 0.0f ),
                FVector3( -1.0f,-1.0f, 0.0f ),
                FVector3(  0.0f,-1.0f, 0.0f ),
                FVector3(  1.0f,-1.0f, 0.0f ),

                FVector3( -1.0f, 1.0f,-1.0f ),
                FVector3(  0.0f, 1.0f,-1.0f ),
                FVector3(  1.0f, 1.0f,-1.0f ),
                FVector3( -1.0f, 0.0f,-1.0f ),
                FVector3(  0.0f, 0.0f,-1.0f ),
                FVector3(  1.0f, 0.0f,-1.0f ),
                FVector3( -1.0f,-1.0f,-1.0f ),
                FVector3(  0.0f,-1.0f,-1.0f ),
                FVector3(  1.0f,-1.0f,-1.0f ),
            };

            for (const FVector3& lightDirInfo : testDirections)
            {
                if (lightDirInfo.length() < 0.1f)
                    continue;
                auto lightDir = lightDirInfo.normalize();

                if (FPlane::dotNormal(plane, lightDir) < 0.1f)
                    continue;

                auto m = FMatrix4x4::shadow(lightDir, plane);
                auto pp = -plane.D * plane.Normal; // origin of the plane.

                //
                for (const auto& point : points)
                {
                    auto v4 = FMatrix4x4::transformToVector4(point, m);

                    auto sp = FVector3(v4.X, v4.Y, v4.Z) / v4.W;

                    // Make sure transformed position is on the plane.
                    auto v = sp - pp;
                    auto d = FVector3::dot(v, plane.Normal);
                    Assert::True(mathOps::almostZero(d, 0.0001f), "FMatrix4x4::shadow did not provide expected value.1");

                    // make sure direction between transformed position and original position are same as light direction.
                    if ((point - pp).dot(plane.Normal) > 0.0001f)
                    {
                        auto dir = (point - sp).normalize();
                        Assert::True(MathHelper::Equal(dir, lightDir), "FMatrix4x4::shadow did not provide expected value.2");
                    }
                }
            }
        }
    }); 
        
    test("Matrix4x4CreateReflectionTest01", [] ()
    {
        // XY plane.
        CreateReflectionTest(FPlane(FVector3::unitZ(), 0), FMatrix4x4::scale(1, 1, -1));
        // XZ plane.
        CreateReflectionTest(FPlane(FVector3::unitY(), 0), FMatrix4x4::scale(1, -1, 1));
        // YZ plane.
        CreateReflectionTest(FPlane(FVector3::unitX(), 0), FMatrix4x4::scale(-1, 1, 1));

        // Complex cases.
        FPlane planes[5] = {
            FPlane( 0, 1, 0, 0 ),
            FPlane( 1, 2, 3, 4 ),
            FPlane( 5, 6, 7, 8 ),
            FPlane(-1,-2,-3,-4 ),
            FPlane(-5,-6,-7,-8 )
        };

        FVector3 points[4] = {
            FVector3( 1, 2, 3),
            FVector3( 5, 6, 7),
            FVector3(-1,-2,-3),
            FVector3(-5,-6,-7)
        };

        for (const auto& p : planes)
        {
            auto plane = FPlane::normalize(p);
            auto m = FMatrix4x4::reflection(plane);
            auto pp = -plane.D * plane.Normal; // Position on the plane.

            //
            for (const auto& point : points)
            {
                auto rp = FMatrix4x4::transform(point, m);

                // Manually compute reflection point and compare results.
                auto v = point - pp;
                auto d = v.dot(plane.Normal);
                auto vp = point - 2.0f * d * plane.Normal;
                Assert::True(MathHelper::Equal(rp, vp), "FMatrix4x4::Reflection did not provide expected value.");
            }
        }
    });

    // A test for CreateRotationZ (float)
    test("Matrix4x4CreateRotationZTest", [] ()
    {
        auto radians = MathHelper::ToRadians(50.0f);

        auto expected = FMatrix4x4();
        expected.M11 = 0.642787635f;
        expected.M12 = 0.766044438f;
        expected.M21 = -0.766044438f;
        expected.M22 = 0.642787635f;
        expected.M33 = 1.0f;
        expected.M44 = 1.0f;

        FMatrix4x4 actual;
        actual = FMatrix4x4::rotationZ(radians);
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::rotationZ did not return the expected value.");
    });

    // A test for CreateRotationZ (float, Vector3f)
    test("Matrix4x4CreateRotationZCenterTest", [] ()
    {
        auto radians = MathHelper::ToRadians(30.0f);
        auto center = FVector3(23, 42, 66);

        auto rotateAroundZero = FMatrix4x4::rotationZ(radians, FVector3::zero());
        auto rotateAroundZeroExpected = FMatrix4x4::rotationZ(radians);
        Assert::True(MathHelper::Equal(rotateAroundZero, rotateAroundZeroExpected));

        auto rotateAroundCenter = FMatrix4x4::rotationZ(radians, center);
        auto rotateAroundCenterExpected = FMatrix4x4::translation(-center) * FMatrix4x4::rotationZ(radians) * FMatrix4x4::translation(center);
        Assert::True(MathHelper::Equal(rotateAroundCenter, rotateAroundCenterExpected));
    });

    // A test for CrateLookAt (Vector3f, Vector3f, Vector3f)
    test("Matrix4x4CreateLookAtTest", [] ()
    {
        auto cameraPosition = FVector3(10.0f, 20.0f, 30.0f);
        auto cameraTarget = FVector3(3.0f, 2.0f, -4.0f);
        auto cameraUpVector = FVector3(0.0f, 1.0f, 0.0f);

        auto expected = FMatrix4x4();
        expected.M11 = 0.979457f;
        expected.M12 = -0.0928267762f;
        expected.M13 = 0.179017f;

        expected.M21 = 0.0f;
        expected.M22 = 0.8877481f;
        expected.M23 = 0.460329473f;

        expected.M31 = -0.201652914f;
        expected.M32 = -0.450872928f;
        expected.M33 = 0.8695112f;

        expected.M41 = -3.74498272f;
        expected.M42 = -3.30050683f;
        expected.M43 = -37.0820961f;
        expected.M44 = 1.0f;

        auto actual = FMatrix4x4::lookAt(cameraPosition, cameraTarget, cameraUpVector);
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::CreateLookAt did not return the expected value.");
    });

    // A test for CreateWorld (Vector3f, Vector3f, Vector3f)
    test("Matrix4x4CreateWorldTest", [] ()
    {
        auto objectPosition = FVector3(10.0f, 20.0f, 30.0f);
        auto objectForwardDirection = FVector3(3.0f, 2.0f, -4.0f);
        auto objectUpVector = FVector3(0.0f, 1.0f, 0.0f);

        auto expected = FMatrix4x4();
        expected.M11 = 0.799999952f;
        expected.M12 = 0;
        expected.M13 = 0.599999964f;
        expected.M14 = 0;

        expected.M21 = -0.2228344f;
        expected.M22 = 0.928476632f;
        expected.M23 = 0.297112525f;
        expected.M24 = 0;

        expected.M31 = -0.557086f;
        expected.M32 = -0.371390671f;
        expected.M33 = 0.742781341f;
        expected.M34 = 0;

        expected.M41 = 10;
        expected.M42 = 20;
        expected.M43 = 30;
        expected.M44 = 1.0f;

        auto actual = FMatrix4x4::world(objectPosition, objectForwardDirection, objectUpVector);
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::CreateWorld did not return the expected value.");

        Assert::True(objectPosition == actual.translation());
        Assert::True(FVector3::dot(objectUpVector.normalize(), FVector3(actual.M21, actual.M22, actual.M23)) > 0);
        Assert::True(FVector3::dot(objectForwardDirection.normalize(), FVector3(-actual.M31, -actual.M32, -actual.M33)) > 0.999f);
    });

    // A test for CreateOrtho (float, float, float, float)
    test("Matrix4x4CreateOrthoTest", [] ()
    {
        auto width = 100.0f;
        auto height = 200.0f;
        auto zNearPlane = 1.5f;
        auto zFarPlane = 1000.0f;

        auto expected = FMatrix4x4();
        expected.M11 = 0.02f;
        expected.M22 = 0.01f;
        expected.M33 = -0.00100150227f;
        expected.M43 = -0.00150225335f;
        expected.M44 = 1.0f;

        FMatrix4x4 actual;
        actual = FMatrix4x4::orthographic(width, height, zNearPlane, zFarPlane);
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::CreateOrtho did not return the expected value.");
    });

    // A test for CreateOrthoOffCenter (float, float, float, float, float, float)
    test("Matrix4x4CreateOrthoOffCenterTest", [] ()
    {
        auto left = 10.0f;
        auto right = 90.0f;
        auto bottom = 20.0f;
        auto top = 180.0f;
        auto zNearPlane = 1.5f;
        auto zFarPlane = 1000.0f;

        auto expected = FMatrix4x4();
        expected.M11 = 0.025f;
        expected.M22 = 0.0125f;
        expected.M33 = -0.00100150227f;
        expected.M41 = -1.25f;
        expected.M42 = -1.25f;
        expected.M43 = -0.00150225335f;
        expected.M44 = 1.0f;

        FMatrix4x4 actual;
        actual = FMatrix4x4::orthographicOffCenter(left, right, bottom, top, zNearPlane, zFarPlane);
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::CreateOrthoOffCenter did not return the expected value.");
    });

    // A test for CreatePerspective (float, float, float, float)
    test("Matrix4x4CreatePerspectiveTest", [] ()
    {
        auto width = 100.0f;
        auto height = 200.0f;
        auto zNearPlane = 1.5f;
        auto zFarPlane = 1000.0f;

        auto expected = FMatrix4x4();
        expected.M11 = 0.03f;
        expected.M22 = 0.015f;
        expected.M33 = -1.00150228f;
        expected.M34 = -1.0f;
        expected.M43 = -1.50225341f;

        FMatrix4x4 actual;
        actual = FMatrix4x4::perspective(width, height, zNearPlane, zFarPlane);
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::perspective did not return the expected value.");
    });

    // A test for CreatePerspective (float, float, float, float)
    // CreatePerspective test where znear = zfar
    testException<std::out_of_range>("Matrix4x4CreatePerspectiveTest", []() {
        auto width = 100.0f;
        auto height = 200.0f;
        auto zNearPlane = 0.0f;
        auto zFarPlane = 0.0f;

        auto actual = FMatrix4x4::perspective(width, height, zNearPlane, zFarPlane);
    });

    // A test for CreatePerspective (float, float, float, float)
// CreatePerspective test where near plane is negative value
    testException<std::out_of_range>("Matrix4x4CreatePerspectiveTest2", []()
    {
        auto actual = FMatrix4x4::perspective(10, 10, -10, 10);
    });

    // A test for CreatePerspective (float, float, float, float)
    // CreatePerspective test where far plane is negative value
    testException<std::out_of_range>("Matrix4x4CreatePerspectiveTest3", []()
    {
        auto actual = FMatrix4x4::perspective(10, 10, 10, -10);
    });

    // A test for CreatePerspective (float, float, float, float)
    // CreatePerspective test where near plane is beyond far plane
    testException<std::out_of_range>("Matrix4x4CreatePerspectiveTest4", []()
    {
        auto actual = FMatrix4x4::perspective(10, 10, 10, 1);
    });

    // A test for CreatePerspectiveFieldOfView (float, float, float, float)
    test("Matrix4x4CreatePerspectiveFieldOfViewTest", [] ()
    {
        auto fieldOfView = MathHelper::ToRadians(30.0f);
        auto aspectRatio = 1280.0f / 720.0f;
        auto zNearPlane = 1.5f;
        auto zFarPlane = 1000.0f;

        auto expected = FMatrix4x4();
        expected.M11 = 2.09927845f;
        expected.M22 = 3.73205066f;
        expected.M33 = -1.00150228f;
        expected.M34 = -1.0f;
        expected.M43 = -1.50225341f;
        FMatrix4x4 actual;

        actual = FMatrix4x4::perspectiveFieldOfView(fieldOfView, aspectRatio, zNearPlane, zFarPlane);
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::CreatePerspectiveFieldOfView did not return the expected value.");
    });

    // A test for CreatePerspectiveFieldOfView (float, float, float, float)
    // CreatePerspectiveFieldOfView test where filedOfView is negative value.
    testException<std::out_of_range>("Matrix4x4CreatePerspectiveFieldOfViewTest1", []()
    {
        auto mtx = FMatrix4x4::perspectiveFieldOfView(-1, 1, 1, 10);
    });

    // A test for CreatePerspectiveFieldOfView (float, float, float, float)
    // CreatePerspectiveFieldOfView test where filedOfView is more than pi.
    testException<std::out_of_range>("Matrix4x4CreatePerspectiveFieldOfViewTest2", []()
    {
        auto mtx = FMatrix4x4::perspectiveFieldOfView(MathHelper::pi + 0.01f, 1, 1, 10);
    });

    // A test for CreatePerspectiveFieldOfView (float, float, float, float)
    // CreatePerspectiveFieldOfView test where nearPlaneDistance is negative value.
    testException<std::out_of_range>("Matrix4x4CreatePerspectiveFieldOfViewTest3", []()
    {
        auto mtx = FMatrix4x4::perspectiveFieldOfView(MathHelper::piOver4, 1, -1, 10);
    });

    // A test for CreatePerspectiveFieldOfView (float, float, float, float)
    // CreatePerspectiveFieldOfView test where farPlaneDistance is negative value.
    testException<std::out_of_range>("Matrix4x4CreatePerspectiveFieldOfViewTest4", []()
    {
        auto mtx = FMatrix4x4::perspectiveFieldOfView(MathHelper::piOver4, 1, 1, -10);
    });

    // A test for CreatePerspectiveFieldOfView (float, float, float, float)
    // CreatePerspectiveFieldOfView test where nearPlaneDistance is larger than farPlaneDistance.
    testException<std::out_of_range>("Matrix4x4CreatePerspectiveFieldOfViewTest5", []()
    {
        auto mtx = FMatrix4x4::perspectiveFieldOfView(MathHelper::piOver4, 1, 10, 1);
    });

    // A test for CreatePerspectiveOffCenter (float, float, float, float, float, float)
    test("Matrix4x4CreatePerspectiveOffCenterTest", [] ()
    {
        auto left = 10.0f;
        auto right = 90.0f;
        auto bottom = 20.0f;
        auto top = 180.0f;
        auto zNearPlane = 1.5f;
        auto zFarPlane = 1000.0f;

        auto expected = FMatrix4x4();
        expected.M11 = 0.0375f;
        expected.M22 = 0.01875f;
        expected.M31 = 1.25f;
        expected.M32 = 1.25f;
        expected.M33 = -1.00150228f;
        expected.M34 = -1.0f;
        expected.M43 = -1.50225341f;

        FMatrix4x4 actual;
        actual = FMatrix4x4::perspectiveOffCenter(left, right, bottom, top, zNearPlane, zFarPlane);
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::CreatePerspectiveOffCenter did not return the expected value.");
    });

    // A test for CreatePerspectiveOffCenter (float, float, float, float, float, float)
    // CreatePerspectiveOffCenter test where nearPlaneDistance is negative.
    testException<std::out_of_range>("Matrix4x4CreatePerspectiveOffCenterTest1", []()
    {
        float left = 10.0f, right = 90.0f, bottom = 20.0f, top = 180.0f;
        auto actual = FMatrix4x4::perspectiveOffCenter(left, right, bottom, top, -1, 10);
    });

    // A test for CreatePerspectiveOffCenter (float, float, float, float, float, float)
    // CreatePerspectiveOffCenter test where farPlaneDistance is negative.
    testException<std::out_of_range>("Matrix4x4CreatePerspectiveOffCenterTest2", []()
    {
        float left = 10.0f, right = 90.0f, bottom = 20.0f, top = 180.0f;
        auto actual = FMatrix4x4::perspectiveOffCenter(left, right, bottom, top, 1, -10);
    });

    // A test for CreatePerspectiveOffCenter (float, float, float, float, float, float)
    // CreatePerspectiveOffCenter test where test where nearPlaneDistance is larger than farPlaneDistance.
    testException<std::out_of_range>("Matrix4x4CreatePerspectiveOffCenterTest3", []()
    {
        float left = 10.0f, right = 90.0f, bottom = 20.0f, top = 180.0f;
        auto actual = FMatrix4x4::perspectiveOffCenter(left, right, bottom, top, 10, 1);
    });

    // A test for Invert (FMatrix4x4)
    // Non invertible matrix - determinant is zero - singular matrix
    test("Matrix4x4InvertTest1", [] ()
    {
        auto a = FMatrix4x4();
        a.M11 = 1.0f;
        a.M12 = 2.0f;
        a.M13 = 3.0f;
        a.M14 = 4.0f;
        a.M21 = 5.0f;
        a.M22 = 6.0f;
        a.M23 = 7.0f;
        a.M24 = 8.0f;
        a.M31 = 9.0f;
        a.M32 = 10.0f;
        a.M33 = 11.0f;
        a.M34 = 12.0f;
        a.M41 = 13.0f;
        a.M42 = 14.0f;
        a.M43 = 15.0f;
        a.M44 = 16.0f;

        auto detA = a.getDeterminant();
        Assert::True(MathHelper::Equal(detA, 0.0f), "FMatrix4x4::Invert did not return the expected value.");

        auto inv = a.invert();
        Assert::False(inv.has_value());
        FMatrix4x4 actual = inv.value_or(FMatrix4x4(std::numeric_limits<float>::quiet_NaN()));

        // all the elements in Actual is NaN
        Assert::True(
            std::isnan(actual.M11) && std::isnan(actual.M12) && std::isnan(actual.M13) && std::isnan(actual.M14) &&
            std::isnan(actual.M21) && std::isnan(actual.M22) && std::isnan(actual.M23) && std::isnan(actual.M24) &&
            std::isnan(actual.M31) && std::isnan(actual.M32) && std::isnan(actual.M33) && std::isnan(actual.M34) &&
            std::isnan(actual.M41) && std::isnan(actual.M42) && std::isnan(actual.M43) && std::isnan(actual.M44)
            , "FMatrix4x4::Invert did not return the expected value.");
    });

    // A test for Lerp (FMatrix4x4, FMatrix4x4, float)
    test("Matrix4x4LerpTest", [] ()
    {
        auto a = FMatrix4x4();
        a.M11 = 11.0f;
        a.M12 = 12.0f;
        a.M13 = 13.0f;
        a.M14 = 14.0f;
        a.M21 = 21.0f;
        a.M22 = 22.0f;
        a.M23 = 23.0f;
        a.M24 = 24.0f;
        a.M31 = 31.0f;
        a.M32 = 32.0f;
        a.M33 = 33.0f;
        a.M34 = 34.0f;
        a.M41 = 41.0f;
        a.M42 = 42.0f;
        a.M43 = 43.0f;
        a.M44 = 44.0f;

        auto b = GenerateMatrixNumberFrom1To16();

        auto t = 0.5f;

        auto expected = FMatrix4x4();
        expected.M11 = a.M11 + (b.M11 - a.M11) * t;
        expected.M12 = a.M12 + (b.M12 - a.M12) * t;
        expected.M13 = a.M13 + (b.M13 - a.M13) * t;
        expected.M14 = a.M14 + (b.M14 - a.M14) * t;

        expected.M21 = a.M21 + (b.M21 - a.M21) * t;
        expected.M22 = a.M22 + (b.M22 - a.M22) * t;
        expected.M23 = a.M23 + (b.M23 - a.M23) * t;
        expected.M24 = a.M24 + (b.M24 - a.M24) * t;

        expected.M31 = a.M31 + (b.M31 - a.M31) * t;
        expected.M32 = a.M32 + (b.M32 - a.M32) * t;
        expected.M33 = a.M33 + (b.M33 - a.M33) * t;
        expected.M34 = a.M34 + (b.M34 - a.M34) * t;

        expected.M41 = a.M41 + (b.M41 - a.M41) * t;
        expected.M42 = a.M42 + (b.M42 - a.M42) * t;
        expected.M43 = a.M43 + (b.M43 - a.M43) * t;
        expected.M44 = a.M44 + (b.M44 - a.M44) * t;

        FMatrix4x4 actual;
        actual = FMatrix4x4::lerp(a, b, t);
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::Lerp did not return the expected value.");
    });

    // A test for operator - (FMatrix4x4)
    test("Matrix4x4UnaryNegationTest", [] ()
    {
        auto a = GenerateMatrixNumberFrom1To16();

        auto expected = FMatrix4x4();
        expected.M11 = -1.0f;
        expected.M12 = -2.0f;
        expected.M13 = -3.0f;
        expected.M14 = -4.0f;
        expected.M21 = -5.0f;
        expected.M22 = -6.0f;
        expected.M23 = -7.0f;
        expected.M24 = -8.0f;
        expected.M31 = -9.0f;
        expected.M32 = -10.0f;
        expected.M33 = -11.0f;
        expected.M34 = -12.0f;
        expected.M41 = -13.0f;
        expected.M42 = -14.0f;
        expected.M43 = -15.0f;
        expected.M44 = -16.0f;

        auto actual = -a;
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::operator - did not return the expected value.");
    });

    // A test for operator - (FMatrix4x4, FMatrix4x4)
    test("Matrix4x4SubtractionTest", [] ()
    {
        auto a = GenerateMatrixNumberFrom1To16();
        auto b = GenerateMatrixNumberFrom1To16();
        auto expected = FMatrix4x4();

        auto actual = a - b;
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::operator - did not return the expected value.");
    });

    // A test for operator * (FMatrix4x4, FMatrix4x4)
    test("Matrix4x4MultiplyTest1", [] ()
    {
        auto a = GenerateMatrixNumberFrom1To16();
        auto b = GenerateMatrixNumberFrom1To16();

        auto expected = FMatrix4x4();
        expected.M11 = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31 + a.M14 * b.M41;
        expected.M12 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32 + a.M14 * b.M42;
        expected.M13 = a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33 + a.M14 * b.M43;
        expected.M14 = a.M11 * b.M14 + a.M12 * b.M24 + a.M13 * b.M34 + a.M14 * b.M44;

        expected.M21 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31 + a.M24 * b.M41;
        expected.M22 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32 + a.M24 * b.M42;
        expected.M23 = a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33 + a.M24 * b.M43;
        expected.M24 = a.M21 * b.M14 + a.M22 * b.M24 + a.M23 * b.M34 + a.M24 * b.M44;

        expected.M31 = a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31 + a.M34 * b.M41;
        expected.M32 = a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32 + a.M34 * b.M42;
        expected.M33 = a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33 + a.M34 * b.M43;
        expected.M34 = a.M31 * b.M14 + a.M32 * b.M24 + a.M33 * b.M34 + a.M34 * b.M44;

        expected.M41 = a.M41 * b.M11 + a.M42 * b.M21 + a.M43 * b.M31 + a.M44 * b.M41;
        expected.M42 = a.M41 * b.M12 + a.M42 * b.M22 + a.M43 * b.M32 + a.M44 * b.M42;
        expected.M43 = a.M41 * b.M13 + a.M42 * b.M23 + a.M43 * b.M33 + a.M44 * b.M43;
        expected.M44 = a.M41 * b.M14 + a.M42 * b.M24 + a.M43 * b.M34 + a.M44 * b.M44;

        auto actual = a * b;
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::operator * did not return the expected value.");
    });

    // A test for operator * (FMatrix4x4, FMatrix4x4)
    // Multiply with identity matrix 
    test("Matrix4x4MultiplyTest4", [] ()
    {
        auto a = FMatrix4x4();
        a.M11 = 1.0f;
        a.M12 = 2.0f;
        a.M13 = 3.0f;
        a.M14 = 4.0f;
        a.M21 = 5.0f;
        a.M22 = -6.0f;
        a.M23 = 7.0f;
        a.M24 = -8.0f;
        a.M31 = 9.0f;
        a.M32 = 10.0f;
        a.M33 = 11.0f;
        a.M34 = 12.0f;
        a.M41 = 13.0f;
        a.M42 = -14.0f;
        a.M43 = 15.0f;
        a.M44 = -16.0f;

        auto b = FMatrix4x4();
        b = FMatrix4x4::identity();

        auto expected = a;
        auto actual = a * b;

        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::operator * did not return the expected value.");
    });

    // A test for operator + (FMatrix4x4, FMatrix4x4)
    test("Matrix4x4AdditionTest", [] ()
    {
        auto a = GenerateMatrixNumberFrom1To16();
        auto b = GenerateMatrixNumberFrom1To16();

        auto expected = FMatrix4x4();
        expected.M11 = a.M11 + b.M11;
        expected.M12 = a.M12 + b.M12;
        expected.M13 = a.M13 + b.M13;
        expected.M14 = a.M14 + b.M14;
        expected.M21 = a.M21 + b.M21;
        expected.M22 = a.M22 + b.M22;
        expected.M23 = a.M23 + b.M23;
        expected.M24 = a.M24 + b.M24;
        expected.M31 = a.M31 + b.M31;
        expected.M32 = a.M32 + b.M32;
        expected.M33 = a.M33 + b.M33;
        expected.M34 = a.M34 + b.M34;
        expected.M41 = a.M41 + b.M41;
        expected.M42 = a.M42 + b.M42;
        expected.M43 = a.M43 + b.M43;
        expected.M44 = a.M44 + b.M44;

        FMatrix4x4 actual;

        actual = a + b;

        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::operator + did not return the expected value.");
    });

    // A test for Transpose (FMatrix4x4)
    test("Matrix4x4TransposeTest", [] ()
    {
        auto a = GenerateMatrixNumberFrom1To16();

        auto expected = FMatrix4x4();
        expected.M11 = a.M11;
        expected.M12 = a.M21;
        expected.M13 = a.M31;
        expected.M14 = a.M41;
        expected.M21 = a.M12;
        expected.M22 = a.M22;
        expected.M23 = a.M32;
        expected.M24 = a.M42;
        expected.M31 = a.M13;
        expected.M32 = a.M23;
        expected.M33 = a.M33;
        expected.M34 = a.M43;
        expected.M41 = a.M14;
        expected.M42 = a.M24;
        expected.M43 = a.M34;
        expected.M44 = a.M44;

        auto actual = FMatrix4x4::transpose(a);
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::Transpose did not return the expected value.");
    });

    // A test for Transpose (FMatrix4x4)
    // Transpose Identity matrix
    test("Matrix4x4TransposeTest1", [] ()
    {
        auto a = FMatrix4x4::identity();
        auto expected = FMatrix4x4::identity();

        auto actual = FMatrix4x4::transpose(a);
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::Transpose did not return the expected value.");
    });

    // A test for FMatrix4x4 (FQuaternion) 
    test("Matrix4x4FromQuaternionTest1", [] ()
    {
        auto axis = FVector3(1.0f, 2.0f, 3.0f).normalize();
        auto q = FQuaternion::fromAxisAngle(axis, MathHelper::ToRadians(30.0f));

        auto expected = FMatrix4x4();
        expected.M11 = 0.875595033f;
        expected.M12 = 0.420031041f;
        expected.M13 = -0.2385524f;
        expected.M14 = 0.0f;

        expected.M21 = -0.38175258f;
        expected.M22 = 0.904303849f;
        expected.M23 = 0.1910483f;
        expected.M24 = 0.0f;

        expected.M31 = 0.295970082f;
        expected.M32 = -0.07621294f;
        expected.M33 = 0.952151954f;
        expected.M34 = 0.0f;

        expected.M41 = 0.0f;
        expected.M42 = 0.0f;
        expected.M43 = 0.0f;
        expected.M44 = 1.0f;

        auto target = FMatrix4x4::fromQuaternion(q);
        Assert::True(MathHelper::Equal(expected, target), "FMatrix4x4::FMatrix4x4(FQuaternion) did not return the expected value.");
    });

    // A test for FromQuaternion (FMatrix4x4)
    // Convert X axis rotation matrix 
    test("Matrix4x4FromQuaternionTest2", [] ()
    {
        for (auto angle = 0.0f; angle < 720.0f; angle += 10.0f)
        {
            auto quat = FQuaternion::fromAxisAngle(FVector3::unitX(), angle);

            auto expected = FMatrix4x4::rotationX(angle);
            auto actual = FMatrix4x4::fromQuaternion(quat);
            Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::FromQuaternion did not return the expected value.1");

            // make sure convert back to quaternion is same as we passed FQuaternion::
            auto q2 = actual.quaternion();
            Assert::True(MathHelper::EqualRotation(quat, q2), "FQuaternion::FromQuaternion did not return the expected value.2");
        }
    });

    // A test for FromQuaternion (FMatrix4x4)
    // Convert Y axis rotation matrix  
    test("Matrix4x4FromQuaternionTest3", [] ()
    {
        for (auto angle = 0.0f; angle < 720.0f; angle += 10.0f)
        {
            auto quat = FQuaternion::fromAxisAngle(FVector3::unitY(), angle);

            auto expected = FMatrix4x4::rotationY(angle);
            auto actual = FMatrix4x4::fromQuaternion(quat);
            Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::FromQuaternion did not return the expected value.1");

            // make sure convert back to quaternion is same as we passed FQuaternion::
            auto q2 = actual.quaternion();
            Assert::True(MathHelper::EqualRotation(quat, q2), "FQuaternion::FromQuaternion did not return the expected value.2");
        }
    });

    // A test for FromQuaternion (FMatrix4x4)
    // Convert Z axis rotation matrix   
    test("Matrix4x4FromQuaternionTest4", [] ()
    {
        for (auto angle = 0.0f; angle < 720.0f; angle += 10.0f)
        {
            auto quat = FQuaternion::fromAxisAngle(FVector3::unitZ(), angle);

            auto expected = FMatrix4x4::rotationZ(angle);
            auto actual = FMatrix4x4::fromQuaternion(quat);
            Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::FromQuaternion did not return the expected value.1");

            // make sure convert back to quaternion is same as we passed FQuaternion::
            auto q2 = actual.quaternion();
            Assert::True(MathHelper::EqualRotation(quat, q2), "FQuaternion::FromQuaternion did not return the expected value.2");
        }
    });

    // A test for FromQuaternion (FMatrix4x4)
    // Convert XYZ axis rotation matrix 
    test("Matrix4x4FromQuaternionTest5", [] ()
    {
        for (auto angle = 0.0f; angle < 720.0f; angle += 10.0f)
        {
            auto quat =
                FQuaternion::fromAxisAngle(FVector3::unitZ(), angle) *
                FQuaternion::fromAxisAngle(FVector3::unitY(), angle) *
                FQuaternion::fromAxisAngle(FVector3::unitX(), angle);

            auto expected =
                FMatrix4x4::rotationX(angle) *
                FMatrix4x4::rotationY(angle) *
                FMatrix4x4::rotationZ(angle);
            auto actual = FMatrix4x4::fromQuaternion(quat);
            Assert::True(MathHelper::Equal(expected, actual), "FQuaternion::FromQuaternion did not return the expected value.1");

            // make sure convert back to quaternion is same as we passed FQuaternion::
            auto q2 = actual.quaternion();
            Assert::True(MathHelper::EqualRotation(quat, q2), "FQuaternion::FromQuaternion did not return the expected value.2");
        }
    });

    // A test for ToString () 
    test("Matrix4x4ToStringTest", [] ()
    {
        auto a = FMatrix4x4();
        a.M11 = 11.0f;
        a.M12 = -12.0f;
        a.M13 = -13.3f;
        a.M14 = 14.4f;
        a.M21 = 21.0f;
        a.M22 = 22.0f;
        a.M23 = 23.0f;
        a.M24 = 24.0f;
        a.M31 = 31.0f;
        a.M32 = 32.0f;
        a.M33 = 33.0f;
        a.M34 = 34.0f;
        a.M41 = 41.0f;
        a.M42 = 42.0f;
        a.M43 = 43.0f;
        a.M44 = 44.0f;

        std::stringstream actualOss;
        actualOss << a;
        std::string actual = actualOss.str();

        std::ostringstream expectedlOss;
        expectedlOss << "{{ {{M11:" << a.M11 << " M12:" << a.M12 << " M13:" << a.M13 << " M14:" << a.M14 << "}} {{M21:" << a.M21 << " M22:" << a.M22 << " M23:" << a.M23 << " M24:" << a.M24 << "}} {{M31:" << a.M31 << " M32:" << a.M32 << " M33:" << a.M33 << " M34:" << a.M34 << "}} {{M41:" << a.M41 << " M42:" << a.M42 << " M43:" << a.M43 << " M44:" << a.M44 << "}}} }}";
        std::string expected = expectedlOss.str();

        Assert::True(expected == actual);
    });

    // A test for Add (FMatrix4x4, FMatrix4x4)
    test("Matrix4x4AddTest", [] ()
    {
        auto a = GenerateMatrixNumberFrom1To16();
        auto b = GenerateMatrixNumberFrom1To16();

        auto expected = FMatrix4x4();
        expected.M11 = a.M11 + b.M11;
        expected.M12 = a.M12 + b.M12;
        expected.M13 = a.M13 + b.M13;
        expected.M14 = a.M14 + b.M14;
        expected.M21 = a.M21 + b.M21;
        expected.M22 = a.M22 + b.M22;
        expected.M23 = a.M23 + b.M23;
        expected.M24 = a.M24 + b.M24;
        expected.M31 = a.M31 + b.M31;
        expected.M32 = a.M32 + b.M32;
        expected.M33 = a.M33 + b.M33;
        expected.M34 = a.M34 + b.M34;
        expected.M41 = a.M41 + b.M41;
        expected.M42 = a.M42 + b.M42;
        expected.M43 = a.M43 + b.M43;
        expected.M44 = a.M44 + b.M44;

        FMatrix4x4 actual = a.add(b);
        Assert::AreEqual(expected, actual);
    });

    // A test for Equals (object) 
    test("Matrix4x4EqualsTest", [] ()
    {
        auto a = GenerateMatrixNumberFrom1To16();
        auto b = GenerateMatrixNumberFrom1To16();

        // case 1: compare between same values
        auto obj = b;

        bool expected = true;
        bool actual = a == obj;
        Assert::AreEqual(expected, actual);

        // case 2: compare between different values
        b.M11 = 11.0f;
        obj = b;
        expected = false;
        actual = a == obj;
        Assert::AreEqual(expected, actual);

        //// case 3: compare between different types.
        //obj = FVector4();
        //expected = false;
        //actual = a == obj;
        //Assert::AreEqual(expected, actual);

        //// case 3: compare against null.
        //obj = null;
        //expected = false;
        //actual = a.Equals(obj);
        //Assert::AreEqual(expected, actual);
    });

    // A test for hash ()
    test("Matrix4x4hashTest", [] ()
    {
        FMatrix4x4 target = GenerateMatrixNumberFrom1To16();

        auto expected = hash::combine({ target.M11, target.M12, target.M13, target.M14, target.M21, target.M22, target.M23, target.M24, target.M31, target.M32, target.M33, target.M34, target.M41, target.M42, target.M43, target.M44 });
        auto actual = target.hash();

        Assert::True(expected == actual);
    });

    // A test for Multiply (FMatrix4x4, FMatrix4x4)
    test("Matrix4x4MultiplyTest3", [] ()
    {
        auto a = GenerateMatrixNumberFrom1To16();
        auto b = GenerateMatrixNumberFrom1To16();

        auto expected = FMatrix4x4();
        expected.M11 = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31 + a.M14 * b.M41;
        expected.M12 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32 + a.M14 * b.M42;
        expected.M13 = a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33 + a.M14 * b.M43;
        expected.M14 = a.M11 * b.M14 + a.M12 * b.M24 + a.M13 * b.M34 + a.M14 * b.M44;

        expected.M21 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31 + a.M24 * b.M41;
        expected.M22 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32 + a.M24 * b.M42;
        expected.M23 = a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33 + a.M24 * b.M43;
        expected.M24 = a.M21 * b.M14 + a.M22 * b.M24 + a.M23 * b.M34 + a.M24 * b.M44;

        expected.M31 = a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31 + a.M34 * b.M41;
        expected.M32 = a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32 + a.M34 * b.M42;
        expected.M33 = a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33 + a.M34 * b.M43;
        expected.M34 = a.M31 * b.M14 + a.M32 * b.M24 + a.M33 * b.M34 + a.M34 * b.M44;

        expected.M41 = a.M41 * b.M11 + a.M42 * b.M21 + a.M43 * b.M31 + a.M44 * b.M41;
        expected.M42 = a.M41 * b.M12 + a.M42 * b.M22 + a.M43 * b.M32 + a.M44 * b.M42;
        expected.M43 = a.M41 * b.M13 + a.M42 * b.M23 + a.M43 * b.M33 + a.M44 * b.M43;
        expected.M44 = a.M41 * b.M14 + a.M42 * b.M24 + a.M43 * b.M34 + a.M44 * b.M44;
        FMatrix4x4 actual = a.multiply(b);

        Assert::AreEqual(expected, actual);
    });

    // A test for Multiply (FMatrix4x4, float)
    test("Matrix4x4MultiplyTest5", [] ()
    {
        auto a = GenerateMatrixNumberFrom1To16();
        auto expected = FMatrix4x4(3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36, 39, 42, 45, 48);
        auto actual = a.multiply(3);

        Assert::AreEqual(expected, actual);
    });

    // A test for Multiply (FMatrix4x4, float)
    test("Matrix4x4MultiplyTest6", [] ()
    {
        auto a = GenerateMatrixNumberFrom1To16();
        auto expected = FMatrix4x4(3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36, 39, 42, 45, 48);
        auto actual = a * 3;

        Assert::AreEqual(expected, actual);
    });

    // A test for Negate (FMatrix4x4)
    test("Matrix4x4NegateTest", [] ()
    {
        auto m = GenerateMatrixNumberFrom1To16();

        auto expected = FMatrix4x4();
        expected.M11 = -1.0f;
        expected.M12 = -2.0f;
        expected.M13 = -3.0f;
        expected.M14 = -4.0f;
        expected.M21 = -5.0f;
        expected.M22 = -6.0f;
        expected.M23 = -7.0f;
        expected.M24 = -8.0f;
        expected.M31 = -9.0f;
        expected.M32 = -10.0f;
        expected.M33 = -11.0f;
        expected.M34 = -12.0f;
        expected.M41 = -13.0f;
        expected.M42 = -14.0f;
        expected.M43 = -15.0f;
        expected.M44 = -16.0f;
        FMatrix4x4 actual;

        actual = m.negate();
        Assert::AreEqual(expected, actual);
    });

    // A test for operator != (FMatrix4x4, FMatrix4x4)
    test("Matrix4x4InequalityTest", [] ()
    {
        auto a = GenerateMatrixNumberFrom1To16();
        auto b = GenerateMatrixNumberFrom1To16();

        // case 1: compare between same values
        auto expected = false;
        auto actual = a != b;
        Assert::AreEqual(expected, actual);

        // case 2: compare between different values
        b.M11 = 11.0f;
        expected = true;
        actual = a != b;
        Assert::AreEqual(expected, actual);
    });

    // A test for operator == (FMatrix4x4, FMatrix4x4)
    test("Matrix4x4EqualityTest", [] ()
    {
        auto a = GenerateMatrixNumberFrom1To16();
        auto b = GenerateMatrixNumberFrom1To16();

        // case 1: compare between same values
        auto expected = true;
        auto actual = a == b;
        Assert::AreEqual(expected, actual);

        // case 2: compare between different values
        b.M11 = 11.0f;
        expected = false;
        actual = a == b;
        Assert::AreEqual(expected, actual);
    });

    // A test for Subtract (FMatrix4x4, FMatrix4x4)
    test("Matrix4x4SubtractTest", [] ()
    {
        auto a = GenerateMatrixNumberFrom1To16();
        auto b = GenerateMatrixNumberFrom1To16();
        auto expected = FMatrix4x4();
        FMatrix4x4 actual;

        actual = a.subtract(b);
        Assert::AreEqual(expected, actual);
    });

    // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Place object at Forward side of camera on XZ-plane
    test("Matrix4x4CreateBillboardTest01", [] () {
        // Object placed at Forward of camera. result must be same as 180 degrees rotate along y-axis.
        CreateBillboardFact(FVector3(0, 0, -1), FVector3(0, 1, 0), FMatrix4x4::rotationY(MathHelper::ToRadians(180.0f)));
    });

    // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Place object at Backward side of camera on XZ-plane
    test("Matrix4x4CreateBillboardTest02", [] () {
        // Object placed at Backward of camera. This result must be same as 0 degrees rotate along y-axis.
        CreateBillboardFact(FVector3(0, 0, 1), FVector3(0, 1, 0), FMatrix4x4::rotationY(MathHelper::ToRadians(0)));
    });

    // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Place object at Right side of camera on XZ-plane
    test("Matrix4x4CreateBillboardTest03", [] () {
        // Place object at Right side of camera. This result must be same as 90 degrees rotate along y-axis.
        CreateBillboardFact(FVector3(1, 0, 0), FVector3(0, 1, 0), FMatrix4x4::rotationY(MathHelper::ToRadians(90)));
    });

    // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Place object at Left side of camera on XZ-plane
    test("Matrix4x4CreateBillboardTest04", [] () {
        // Place object at Left side of camera. This result must be same as -90 degrees rotate along y-axis.
        CreateBillboardFact(FVector3(-1, 0, 0), FVector3(0, 1, 0), FMatrix4x4::rotationY(MathHelper::ToRadians(-90)));
    });

    // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Place object at Up side of camera on XY-plane
    test("Matrix4x4CreateBillboardTest05", [] () {
       // Place object at Up side of camera. result must be same as 180 degrees rotate along z-axis after 90 degrees rotate along x-axis.
       CreateBillboardFact(FVector3(0, 1, 0), FVector3(0, 0, 1),
            FMatrix4x4::rotationX(MathHelper::ToRadians(90.0f)) * FMatrix4x4::rotationZ(MathHelper::ToRadians(180)));
    });

    // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Place object at Down side of camera on XY-plane
    test("Matrix4x4CreateBillboardTest06", [] () {
       // Place object at Down side of camera. result must be same as 0 degrees rotate along z-axis after 90 degrees rotate along x-axis.
       CreateBillboardFact(FVector3(0, -1, 0), FVector3(0, 0, 1),
            FMatrix4x4::rotationX(MathHelper::ToRadians(90.0f)) * FMatrix4x4::rotationZ(MathHelper::ToRadians(0)));
    });

    // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Place object at Right side of camera on XY-plane
    test("Matrix4x4CreateBillboardTest07", [] () {
        // Place object at Right side of camera. result must be same as 90 degrees rotate along z-axis after 90 degrees rotate along x-axis.
        CreateBillboardFact(FVector3(1, 0, 0), FVector3(0, 0, 1),
            FMatrix4x4::rotationX(MathHelper::ToRadians(90.0f)) * FMatrix4x4::rotationZ(MathHelper::ToRadians(90.0f)));
    });

    // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Place object at Left side of camera on XY-plane
    test("Matrix4x4CreateBillboardTest08", [] () {
        // Place object at Left side of camera. result must be same as -90 degrees rotate along z-axis after 90 degrees rotate along x-axis.
        CreateBillboardFact(FVector3(-1, 0, 0), FVector3(0, 0, 1),
            FMatrix4x4::rotationX(MathHelper::ToRadians(90.0f)) * FMatrix4x4::rotationZ(MathHelper::ToRadians(-90.0f)));
    });

    // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Place object at Up side of camera on YZ-plane
    test("Matrix4x4CreateBillboardTest09", [] () {
        // Place object at Up side of camera. result must be same as -90 degrees rotate along x-axis after 90 degrees rotate along z-axis.
        CreateBillboardFact(FVector3(0, 1, 0), FVector3(-1, 0, 0),
            FMatrix4x4::rotationZ(MathHelper::ToRadians(90.0f)) * FMatrix4x4::rotationX(MathHelper::ToRadians(-90.0f)));
    });

    // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Place object at Down side of camera on YZ-plane
    test("Matrix4x4CreateBillboardTest10", [] () {
        // Place object at Down side of camera. result must be same as 90 degrees rotate along x-axis after 90 degrees rotate along z-axis.
        CreateBillboardFact(FVector3(0, -1, 0), FVector3(-1, 0, 0),
            FMatrix4x4::rotationZ(MathHelper::ToRadians(90.0f)) * FMatrix4x4::rotationX(MathHelper::ToRadians(90.0f)));
    });

    // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Place object at Forward side of camera on YZ-plane
    test("Matrix4x4CreateBillboardTest11", [] () {
        // Place object at Forward side of camera. result must be same as 180 degrees rotate along x-axis after 90 degrees rotate along z-axis.
        CreateBillboardFact(FVector3(0, 0, -1), FVector3(-1, 0, 0),
            FMatrix4x4::rotationZ(MathHelper::ToRadians(90.0f)) * FMatrix4x4::rotationX(MathHelper::ToRadians(180.0f)));
    });

    // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Place object at Backward side of camera on YZ-plane
    test("Matrix4x4CreateBillboardTest12", [] () {
        // Place object at Backward side of camera. result must be same as 0 degrees rotate along x-axis after 90 degrees rotate along z-axis.
        CreateBillboardFact(FVector3(0, 0, 1), FVector3(-1, 0, 0),
            FMatrix4x4::rotationZ(MathHelper::ToRadians(90.0f)) * FMatrix4x4::rotationX(MathHelper::ToRadians(0.0f)));
    });

    // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Object and camera positions are too close and doesn't pass cameraForwardVector.
    test("Matrix4x4CreateBillboardTooCloseTest1", [] ()
    {
        auto objectPosition = FVector3(3.0f, 4.0f, 5.0f);
        auto cameraPosition = objectPosition;
        auto cameraUpVector = FVector3(0, 1, 0);

        // Doesn't pass camera face direction. CreateBillboard uses Vector3f(0, 0, -1) direction. Result must be same as 180 degrees rotate along y-axis.
        auto expected = FMatrix4x4::rotationY(MathHelper::ToRadians(180.0f)) * FMatrix4x4::translation(objectPosition);
        auto actual = FMatrix4x4::billboard(objectPosition, cameraPosition, cameraUpVector, FVector3(0, 0, 1));
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::CreateBillboard did not return the expected value.");
    });

    // A test for CreateBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Object and camera positions are too close and passed cameraForwardVector.
    test("Matrix4x4CreateBillboardTooCloseTest2", [] ()
    {
        auto objectPosition = FVector3(3.0f, 4.0f, 5.0f);
        auto cameraPosition = objectPosition;
        auto cameraUpVector = FVector3(0, 1, 0);

        // Passes Vector3f.Right as camera face direction. Result must be same as -90 degrees rotate along y-axis.
        auto expected = FMatrix4x4::rotationY(MathHelper::ToRadians(-90.0f)) * FMatrix4x4::translation(objectPosition);
        auto actual = FMatrix4x4::billboard(objectPosition, cameraPosition, cameraUpVector, FVector3(1, 0, 0));
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::CreateBillboard did not return the expected value.");
    });
 
    // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Place object at Forward side of camera on XZ-plane
    test("Matrix4x4CreateConstrainedBillboardTest01", [] () {
        // Object placed at Forward of camera. result must be same as 180 degrees rotate along y-axis.
        CreateConstrainedBillboardFact(FVector3(0, 0, -1), FVector3(0, 1, 0), FMatrix4x4::rotationY(MathHelper::ToRadians(180.0f)));
    });

    // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Place object at Backward side of camera on XZ-plane
    test("Matrix4x4CreateConstrainedBillboardTest02", [] () {
        // Object placed at Backward of camera. This result must be same as 0 degrees rotate along y-axis.
        CreateConstrainedBillboardFact(FVector3(0, 0, 1), FVector3(0, 1, 0), FMatrix4x4::rotationY(MathHelper::ToRadians(0)));
    });

    // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Place object at Right side of camera on XZ-plane
    test("Matrix4x4CreateConstrainedBillboardTest03", [] () {
        // Place object at Right side of camera. This result must be same as 90 degrees rotate along y-axis.
        CreateConstrainedBillboardFact(FVector3(1, 0, 0), FVector3(0, 1, 0), FMatrix4x4::rotationY(MathHelper::ToRadians(90)));
    });

    // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Place object at Left side of camera on XZ-plane
    test("Matrix4x4CreateConstrainedBillboardTest04", [] () {
        // Place object at Left side of camera. This result must be same as -90 degrees rotate along y-axis.
        CreateConstrainedBillboardFact(FVector3(-1, 0, 0), FVector3(0, 1, 0), FMatrix4x4::rotationY(MathHelper::ToRadians(-90)));
    });

    // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Place object at Up side of camera on XY-plane
    test("Matrix4x4CreateConstrainedBillboardTest05", [] () {
        // Place object at Up side of camera. result must be same as 180 degrees rotate along z-axis after 90 degrees rotate along x-axis.
        CreateConstrainedBillboardFact(FVector3(0, 1, 0), FVector3(0, 0, 1),
            FMatrix4x4::rotationX(MathHelper::ToRadians(90.0f)) * FMatrix4x4::rotationZ(MathHelper::ToRadians(180)));
    });

    // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Place object at Down side of camera on XY-plane
    test("Matrix4x4CreateConstrainedBillboardTest06", [] () {
        // Place object at Down side of camera. result must be same as 0 degrees rotate along z-axis after 90 degrees rotate along x-axis.
        CreateConstrainedBillboardFact(FVector3(0, -1, 0), FVector3(0, 0, 1),
            FMatrix4x4::rotationX(MathHelper::ToRadians(90.0f)) * FMatrix4x4::rotationZ(MathHelper::ToRadians(0)));
    });

    // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Place object at Right side of camera on XY-plane
    test("Matrix4x4CreateConstrainedBillboardTest07", [] () {
        // Place object at Right side of camera. result must be same as 90 degrees rotate along z-axis after 90 degrees rotate along x-axis.
        CreateConstrainedBillboardFact(FVector3(1, 0, 0), FVector3(0, 0, 1),
            FMatrix4x4::rotationX(MathHelper::ToRadians(90.0f)) * FMatrix4x4::rotationZ(MathHelper::ToRadians(90.0f)));
    });

    // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Place object at Left side of camera on XY-plane
    test("Matrix4x4CreateConstrainedBillboardTest08", [] () {
        // Place object at Left side of camera. result must be same as -90 degrees rotate along z-axis after 90 degrees rotate along x-axis.
        CreateConstrainedBillboardFact(FVector3(-1, 0, 0), FVector3(0, 0, 1),
            FMatrix4x4::rotationX(MathHelper::ToRadians(90.0f)) * FMatrix4x4::rotationZ(MathHelper::ToRadians(-90.0f)));
    });

    // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Place object at Up side of camera on YZ-plane
    test("Matrix4x4CreateConstrainedBillboardTest09", [] () {
        // Place object at Up side of camera. result must be same as -90 degrees rotate along x-axis after 90 degrees rotate along z-axis.
        CreateConstrainedBillboardFact(FVector3(0, 1, 0), FVector3(-1, 0, 0),
            FMatrix4x4::rotationZ(MathHelper::ToRadians(90.0f)) * FMatrix4x4::rotationX(MathHelper::ToRadians(-90.0f)));
    });

    // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Place object at Down side of camera on YZ-plane
    test("Matrix4x4CreateConstrainedBillboardTest10", [] () {
        // Place object at Down side of camera. result must be same as 90 degrees rotate along x-axis after 90 degrees rotate along z-axis.
        CreateConstrainedBillboardFact(FVector3(0, -1, 0), FVector3(-1, 0, 0),
            FMatrix4x4::rotationZ(MathHelper::ToRadians(90.0f)) * FMatrix4x4::rotationX(MathHelper::ToRadians(90.0f)));
    });

    // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Place object at Forward side of camera on YZ-plane
    test("Matrix4x4CreateConstrainedBillboardTest11", [] () {
        // Place object at Forward side of camera. result must be same as 180 degrees rotate along x-axis after 90 degrees rotate along z-axis.
        CreateConstrainedBillboardFact(FVector3(0, 0, -1), FVector3(-1, 0, 0),
            FMatrix4x4::rotationZ(MathHelper::ToRadians(90.0f)) * FMatrix4x4::rotationX(MathHelper::ToRadians(180.0f)));
    });

    // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Place object at Backward side of camera on YZ-plane
    test("Matrix4x4CreateConstrainedBillboardTest12", [] () {
        // Place object at Backward side of camera. result must be same as 0 degrees rotate along x-axis after 90 degrees rotate along z-axis.
        CreateConstrainedBillboardFact(FVector3(0, 0, 1), FVector3(-1, 0, 0),
            FMatrix4x4::rotationZ(MathHelper::ToRadians(90.0f)) * FMatrix4x4::rotationX(MathHelper::ToRadians(0.0f)));
    });

    // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Object and camera positions are too close and doesn't pass cameraForwardVector.
    test("Matrix4x4CreateConstrainedBillboardTooCloseTest1", [] ()
    {
        auto objectPosition = FVector3(3.0f, 4.0f, 5.0f);
        auto cameraPosition = objectPosition;
        auto cameraUpVector = FVector3(0, 1, 0);

        // Doesn't pass camera face direction. CreateConstrainedBillboard uses Vector3f(0, 0, -1) direction. Result must be same as 180 degrees rotate along y-axis.
        auto expected = FMatrix4x4::rotationY(MathHelper::ToRadians(180.0f)) * FMatrix4x4::translation(objectPosition);
        auto actual = FMatrix4x4::constrainedBillboard(objectPosition, cameraPosition, cameraUpVector, FVector3(0, 0, 1), FVector3(0, 0, -1));
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::CreateConstrainedBillboard did not return the expected value.");
    });

    // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Object and camera positions are too close and passed cameraForwardVector.
    test("Matrix4x4CreateConstrainedBillboardTooCloseTest2", [] ()
    {
        auto objectPosition = FVector3(3.0f, 4.0f, 5.0f);
        auto cameraPosition = objectPosition;
        auto cameraUpVector = FVector3(0, 1, 0);

        // Passes Vector3f.Right as camera face direction. Result must be same as -90 degrees rotate along y-axis.
        auto expected = FMatrix4x4::rotationY(MathHelper::ToRadians(-90.0f)) * FMatrix4x4::translation(objectPosition);
        auto actual = FMatrix4x4::constrainedBillboard(objectPosition, cameraPosition, cameraUpVector, FVector3(1, 0, 0), FVector3(0, 0, -1));
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::CreateConstrainedBillboard did not return the expected value.");
    });

    // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Angle between rotateAxis and camera to object vector is too small. And use doesn't passed objectForwardVector parameter.
    test("Matrix4x4CreateConstrainedBillboardAlongAxisTest1", [] ()
    {
        // Place camera at up side of object.
        auto objectPosition = FVector3(3.0f, 4.0f, 5.0f);
        auto rotateAxis = FVector3(0, 1, 0);
        auto cameraPosition = objectPosition + rotateAxis * 10.0f;

        // In this case, CreateConstrainedBillboard picks Vector3f(0, 0, -1) as object forward vector.
        auto expected = FMatrix4x4::rotationY(MathHelper::ToRadians(180.0f)) * FMatrix4x4::translation(objectPosition);
        auto actual = FMatrix4x4::constrainedBillboard(objectPosition, cameraPosition, rotateAxis, FVector3(0, 0, -1), FVector3(0, 0, -1));
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::CreateConstrainedBillboard did not return the expected value.");
    });

    // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Angle between rotateAxis and camera to object vector is too small. And user doesn't passed objectForwardVector parameter.
    test("Matrix4x4CreateConstrainedBillboardAlongAxisTest2", [] ()
    {
        // Place camera at up side of object.
        auto objectPosition = FVector3(3.0f, 4.0f, 5.0f);
        auto rotateAxis = FVector3(0, 0, -1);
        auto cameraPosition = objectPosition + rotateAxis * 10.0f;

        // In this case, CreateConstrainedBillboard picks Vector3f(1, 0, 0) as object forward vector.
        auto expected = FMatrix4x4::rotationX(MathHelper::ToRadians(-90.0f)) * FMatrix4x4::rotationZ(MathHelper::ToRadians(-90.0f)) * FMatrix4x4::translation(objectPosition);
        auto actual = FMatrix4x4::constrainedBillboard(objectPosition, cameraPosition, rotateAxis, FVector3(0, 0, -1), FVector3(0, 0, -1));
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::CreateConstrainedBillboard did not return the expected value.");
    });

    // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Angle between rotateAxis and camera to object vector is too small. And user passed correct objectForwardVector parameter.
    test("Matrix4x4CreateConstrainedBillboardAlongAxisTest3", [] ()
    {
        // Place camera at up side of object.
        auto objectPosition = FVector3(3.0f, 4.0f, 5.0f);
        auto rotateAxis = FVector3(0, 1, 0);
        auto cameraPosition = objectPosition + rotateAxis * 10.0f;

        // User passes correct objectForwardVector.
        auto expected = FMatrix4x4::rotationY(MathHelper::ToRadians(180.0f)) * FMatrix4x4::translation(objectPosition);
        auto actual = FMatrix4x4::constrainedBillboard(objectPosition, cameraPosition, rotateAxis, FVector3(0, 0, -1), FVector3(0, 0, -1));
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::CreateConstrainedBillboard did not return the expected value.");
    });

    // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Angle between rotateAxis and camera to object vector is too small. And user passed incorrect objectForwardVector parameter.
    test("Matrix4x4CreateConstrainedBillboardAlongAxisTest4", [] ()
    {
        // Place camera at up side of object.
        auto objectPosition = FVector3(3.0f, 4.0f, 5.0f);
        auto rotateAxis = FVector3(0, 1, 0);
        auto cameraPosition = objectPosition + rotateAxis * 10.0f;

        // User passes correct objectForwardVector.
        auto expected = FMatrix4x4::rotationY(MathHelper::ToRadians(180.0f)) * FMatrix4x4::translation(objectPosition);
        auto actual = FMatrix4x4::constrainedBillboard(objectPosition, cameraPosition, rotateAxis, FVector3(0, 0, -1), FVector3(0, 1, 0));
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::CreateConstrainedBillboard did not return the expected value.");
    });

    // A test for CreateConstrainedBillboard (Vector3f, Vector3f, Vector3f, Vector3f?)
    // Angle between rotateAxis and camera to object vector is too small. And user passed incorrect objectForwardVector parameter.
    test("Matrix4x4CreateConstrainedBillboardAlongAxisTest5", [] ()
    {
        // Place camera at up side of object.
        auto objectPosition = FVector3(3.0f, 4.0f, 5.0f);
        auto rotateAxis = FVector3(0, 0, -1);
        auto cameraPosition = objectPosition + rotateAxis * 10.0f;

        // In this case, CreateConstrainedBillboard picks Vector3f.Right as object forward vector.
        auto expected = FMatrix4x4::rotationX(MathHelper::ToRadians(-90.0f)) * FMatrix4x4::rotationZ(MathHelper::ToRadians(-90.0f)) * FMatrix4x4::translation(objectPosition);
        auto actual = FMatrix4x4::constrainedBillboard(objectPosition, cameraPosition, rotateAxis, FVector3(0, 0, -1), FVector3(0, 0, -1));
        Assert::True(MathHelper::Equal(expected, actual), "FMatrix4x4::CreateConstrainedBillboard did not return the expected value.");
    });

    // A test for CreateScale (Vector3f)
    test("Matrix4x4CreateScaleTest1", [] ()
    {
        auto scales = FVector3(2.0f, 3.0f, 4.0f);
        auto expected = FMatrix4x4(
            2.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 3.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 4.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 1.0f);
        auto actual = FMatrix4x4::scale(scales);
        Assert::AreEqual(expected, actual);
    });

    // A test for CreateScale (Vector3f, Vector3f)
    test("Matrix4x4CreateScaleCenterTest1", [] ()
    {
        auto scale = FVector3(3, 4, 5);
        auto center = FVector3(23, 42, 666);

        auto scaleAroundZero = FMatrix4x4::scale(scale, FVector3::zero());
        auto scaleAroundZeroExpected = FMatrix4x4::scale(scale);
        Assert::True(MathHelper::Equal(scaleAroundZero, scaleAroundZeroExpected));

        auto scaleAroundCenter = FMatrix4x4::scale(scale, center);
        auto scaleAroundCenterExpected = FMatrix4x4::translation(-center) * FMatrix4x4::scale(scale) * FMatrix4x4::translation(center);
        Assert::True(MathHelper::Equal(scaleAroundCenter, scaleAroundCenterExpected));
    });

    // A test for CreateScale (float)  
    test("Matrix4x4CreateScaleTest2", [] ()
    {
        auto scale = 2.0f;
        auto expected = FMatrix4x4(
            2.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 2.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 2.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 1.0f);
        auto actual = FMatrix4x4::scale(scale);
        Assert::AreEqual(expected, actual);
    });

    // A test for CreateScale (float, Vector3f)  
    test("Matrix4x4CreateScaleCenterTest2", [] ()
    {
        float scale = 5;
        auto center = FVector3(23, 42, 666);

        auto scaleAroundZero = FMatrix4x4::scale(scale, FVector3::zero());
        auto scaleAroundZeroExpected = FMatrix4x4::scale(scale);
        Assert::True(MathHelper::Equal(scaleAroundZero, scaleAroundZeroExpected));

        auto scaleAroundCenter = FMatrix4x4::scale(scale, center);
        auto scaleAroundCenterExpected = FMatrix4x4::translation(-center) * FMatrix4x4::scale(scale) * FMatrix4x4::translation(center);
        Assert::True(MathHelper::Equal(scaleAroundCenter, scaleAroundCenterExpected));
    });

    // A test for CreateScale (float, float, float)
    test("Matrix4x4CreateScaleTest3", [] ()
    {
        auto xScale = 2.0f;
        auto yScale = 3.0f;
        auto zScale = 4.0f;
        auto expected = FMatrix4x4(
            2.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 3.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 4.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 1.0f);
        auto actual = FMatrix4x4::scale(xScale, yScale, zScale);
        Assert::AreEqual(expected, actual);
    });

    // A test for CreateScale (float, float, float, Vector3f)
    test("Matrix4x4CreateScaleCenterTest3", [] ()
    {
        auto scale = FVector3(3, 4, 5);
        auto center = FVector3(23, 42, 666);

        auto scaleAroundZero = FMatrix4x4::scale(scale.X, scale.Y, scale.Z, FVector3::zero());
        auto scaleAroundZeroExpected = FMatrix4x4::scale(scale.X, scale.Y, scale.Z);
        Assert::True(MathHelper::Equal(scaleAroundZero, scaleAroundZeroExpected));

        auto scaleAroundCenter = FMatrix4x4::scale(scale.X, scale.Y, scale.Z, center);
        auto scaleAroundCenterExpected = FMatrix4x4::translation(-center) * FMatrix4x4::scale(scale.X, scale.Y, scale.Z) * FMatrix4x4::translation(center);
        Assert::True(MathHelper::Equal(scaleAroundCenter, scaleAroundCenterExpected));
    });

    // A test for CreateTranslation (Vector3f)
    test("Matrix4x4CreateTranslationTest1", [] ()
    {
        auto position = FVector3(2.0f, 3.0f, 4.0f);
        auto expected = FMatrix4x4(
            1.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 1.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 1.0f, 0.0f,
            2.0f, 3.0f, 4.0f, 1.0f);

        auto actual = FMatrix4x4::translation(position);
        Assert::AreEqual(expected, actual);
    });

    // A test for CreateTranslation (float, float, float)
    test("Matrix4x4CreateTranslationTest2", [] ()
    {
        auto xPosition = 2.0f;
        auto yPosition = 3.0f;
        auto zPosition = 4.0f;

        auto expected = FMatrix4x4(
            1.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 1.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 1.0f, 0.0f,
            2.0f, 3.0f, 4.0f, 1.0f);

        auto actual = FMatrix4x4::translation(xPosition, yPosition, zPosition);
        Assert::AreEqual(expected, actual);
    });

    // A test for Translation 
    test("Matrix4x4TranslationTest", [] ()
    {
        auto a = GenerateTestMatrix();
        auto b = a;

        // Transformed vector that has same semantics of property must be same.
        auto val = FVector3(a.M41, a.M42, a.M43);
        Assert::AreEqual(val, a.translation());

        // Set value and get value must be same.
        val = FVector3(1.0f, 2.0f, 3.0f);
        a = a.setTranslation(val);
        Assert::AreEqual(val, a.translation());

        // Make sure it only modifies expected value of matrix.
        Assert::True(
            a.M11 == b.M11 && a.M12 == b.M12 && a.M13 == b.M13 && a.M14 == b.M14 &&
            a.M21 == b.M21 && a.M22 == b.M22 && a.M23 == b.M23 && a.M24 == b.M24 &&
            a.M31 == b.M31 && a.M32 == b.M32 && a.M33 == b.M33 && a.M34 == b.M34 &&
            a.M41 != b.M41 && a.M42 != b.M42 && a.M43 != b.M43 && a.M44 == b.M44);
    });

    // A test for Equals (FMatrix4x4)  
    test("Matrix4x4EqualsTest1", [] ()
    {
        auto a = GenerateMatrixNumberFrom1To16();
        auto b = GenerateMatrixNumberFrom1To16();

        // case 1: compare between same values
        bool expected = true;
        bool actual = a == b;
        Assert::AreEqual(expected, actual);

        // case 2: compare between different values
        b.M11 = 11.0f;
        expected = false;
        actual = a == b;
        Assert::AreEqual(expected, actual);
    });

    // A test for isIdentity()  
    test("Matrix4x4isIdentityTest", [] ()
    {
        Assert::True(FMatrix4x4::identity().isIdentity());
        Assert::True(FMatrix4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1).isIdentity());
        Assert::False(FMatrix4x4(0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1).isIdentity());
        Assert::False(FMatrix4x4(1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1).isIdentity());
        Assert::False(FMatrix4x4(1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1).isIdentity());
        Assert::False(FMatrix4x4(1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1).isIdentity());
        Assert::False(FMatrix4x4(1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1).isIdentity());
        Assert::False(FMatrix4x4(1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1).isIdentity());
        Assert::False(FMatrix4x4(1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1).isIdentity());
        Assert::False(FMatrix4x4(1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 1).isIdentity());
        Assert::False(FMatrix4x4(1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1).isIdentity());
        Assert::False(FMatrix4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1).isIdentity());
        Assert::False(FMatrix4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1).isIdentity());
        Assert::False(FMatrix4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1).isIdentity());
        Assert::False(FMatrix4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1).isIdentity());
        Assert::False(FMatrix4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1).isIdentity());
        Assert::False(FMatrix4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1).isIdentity());
        Assert::False(FMatrix4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0).isIdentity());
    });

    // A test for FMatrix4x4 comparison involving NaN values
    test("Matrix4x4EqualsNanTest", [] ()
    {
        auto a = FMatrix4x4(std::numeric_limits<float>::quiet_NaN(), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        auto b = FMatrix4x4(0, std::numeric_limits<float>::quiet_NaN(), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        auto c = FMatrix4x4(0, 0, std::numeric_limits<float>::quiet_NaN(), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        auto d = FMatrix4x4(0, 0, 0, std::numeric_limits<float>::quiet_NaN(), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        auto e = FMatrix4x4(0, 0, 0, 0, std::numeric_limits<float>::quiet_NaN(), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        auto f = FMatrix4x4(0, 0, 0, 0, 0, std::numeric_limits<float>::quiet_NaN(), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        auto g = FMatrix4x4(0, 0, 0, 0, 0, 0, std::numeric_limits<float>::quiet_NaN(), 0, 0, 0, 0, 0, 0, 0, 0, 0);
        auto h = FMatrix4x4(0, 0, 0, 0, 0, 0, 0, std::numeric_limits<float>::quiet_NaN(), 0, 0, 0, 0, 0, 0, 0, 0);
        auto i = FMatrix4x4(0, 0, 0, 0, 0, 0, 0, 0, std::numeric_limits<float>::quiet_NaN(), 0, 0, 0, 0, 0, 0, 0);
        auto j = FMatrix4x4(0, 0, 0, 0, 0, 0, 0, 0, 0, std::numeric_limits<float>::quiet_NaN(), 0, 0, 0, 0, 0, 0);
        auto k = FMatrix4x4(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, std::numeric_limits<float>::quiet_NaN(), 0, 0, 0, 0, 0);
        auto l = FMatrix4x4(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, std::numeric_limits<float>::quiet_NaN(), 0, 0, 0, 0);
        auto m = FMatrix4x4(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, std::numeric_limits<float>::quiet_NaN(), 0, 0, 0);
        auto n = FMatrix4x4(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, std::numeric_limits<float>::quiet_NaN(), 0, 0);
        auto o = FMatrix4x4(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, std::numeric_limits<float>::quiet_NaN(), 0);
        auto p = FMatrix4x4(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, std::numeric_limits<float>::quiet_NaN());

        Assert::False(a == FMatrix4x4());
        Assert::False(b == FMatrix4x4());
        Assert::False(c == FMatrix4x4());
        Assert::False(d == FMatrix4x4());
        Assert::False(e == FMatrix4x4());
        Assert::False(f == FMatrix4x4());
        Assert::False(g == FMatrix4x4());
        Assert::False(h == FMatrix4x4());
        Assert::False(i == FMatrix4x4());
        Assert::False(j == FMatrix4x4());
        Assert::False(k == FMatrix4x4());
        Assert::False(l == FMatrix4x4());
        Assert::False(m == FMatrix4x4());
        Assert::False(n == FMatrix4x4());
        Assert::False(o == FMatrix4x4());
        Assert::False(p == FMatrix4x4());

        Assert::True(a != FMatrix4x4());
        Assert::True(b != FMatrix4x4());
        Assert::True(c != FMatrix4x4());
        Assert::True(d != FMatrix4x4());
        Assert::True(e != FMatrix4x4());
        Assert::True(f != FMatrix4x4());
        Assert::True(g != FMatrix4x4());
        Assert::True(h != FMatrix4x4());
        Assert::True(i != FMatrix4x4());
        Assert::True(j != FMatrix4x4());
        Assert::True(k != FMatrix4x4());
        Assert::True(l != FMatrix4x4());
        Assert::True(m != FMatrix4x4());
        Assert::True(n != FMatrix4x4());
        Assert::True(o != FMatrix4x4());
        Assert::True(p != FMatrix4x4());

        Assert::False(a == (FMatrix4x4()));
        Assert::False(b == (FMatrix4x4()));
        Assert::False(c == (FMatrix4x4()));
        Assert::False(d == (FMatrix4x4()));
        Assert::False(e == (FMatrix4x4()));
        Assert::False(f == (FMatrix4x4()));
        Assert::False(g == (FMatrix4x4()));
        Assert::False(h == (FMatrix4x4()));
        Assert::False(i == (FMatrix4x4()));
        Assert::False(j == (FMatrix4x4()));
        Assert::False(k == (FMatrix4x4()));
        Assert::False(l == (FMatrix4x4()));
        Assert::False(m == (FMatrix4x4()));
        Assert::False(n == (FMatrix4x4()));
        Assert::False(o == (FMatrix4x4()));
        Assert::False(p == (FMatrix4x4()));

        Assert::False(a.isIdentity());
        Assert::False(b.isIdentity());
        Assert::False(c.isIdentity());
        Assert::False(d.isIdentity());
        Assert::False(e.isIdentity());
        Assert::False(f.isIdentity());
        Assert::False(g.isIdentity());
        Assert::False(h.isIdentity());
        Assert::False(i.isIdentity());
        Assert::False(j.isIdentity());
        Assert::False(k.isIdentity());
        Assert::False(l.isIdentity());
        Assert::False(m.isIdentity());
        Assert::False(n.isIdentity());
        Assert::False(o.isIdentity());
        Assert::False(p.isIdentity());

        // Counterintuitive result - IEEE rules for NaN comparison are weird!
        Assert::False(a == (a));
        Assert::False(b == (b));
        Assert::False(c == (c));
        Assert::False(d == (d));
        Assert::False(e == (e));
        Assert::False(f == (f));
        Assert::False(g == (g));
        Assert::False(h == (h));
        Assert::False(i == (i));
        Assert::False(j == (j));
        Assert::False(k == (k));
        Assert::False(l == (l));
        Assert::False(m == (m));
        Assert::False(n == (n));
        Assert::False(o == (o));
        Assert::False(p == (p));
    });

    test("PerspectiveFarPlaneAtInfinityTest", [] ()
    {
        auto nearPlaneDistance = 0.125f;
        auto m = FMatrix4x4::perspective(1.0f, 1.0f, nearPlaneDistance, std::numeric_limits<float>::infinity());
        Assert::AreEqual(-1.0f, m.M33);
        Assert::AreEqual(-nearPlaneDistance, m.M43);
    });

    test("PerspectiveFieldOfViewFarPlaneAtInfinityTest", [] ()
    {
        auto nearPlaneDistance = 0.125f;
        auto m = FMatrix4x4::perspectiveFieldOfView(MathHelper::ToRadians(60.0f), 1.5f, nearPlaneDistance, std::numeric_limits<float>::infinity());
        Assert::AreEqual(-1.0f, m.M33);
        Assert::AreEqual(-nearPlaneDistance, m.M43);
    });

    test("PerspectiveOffCenterFarPlaneAtInfinityTest", [] ()
    {
        auto nearPlaneDistance = 0.125f;
        auto m = FMatrix4x4::perspectiveOffCenter(0.0f, 0.0f, 1.0f, 1.0f, nearPlaneDistance, std::numeric_limits<float>::infinity());
        Assert::AreEqual(-1.0f, m.M33);
        Assert::AreEqual(-nearPlaneDistance, m.M43);
    });

#pragma endregion

    return 0;
}

