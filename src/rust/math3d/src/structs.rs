use std::cmp::Ordering;
use std::ops::{Add, Div, Mul, Neg, Sub, AddAssign};
use num_traits::{Float, Zero, One, FloatConst };
use std::hash::Hash;

use math3d_macro_derive::{StructOps, VectorOps, IntervalOps, VectorComponentOps, VectorOperators};
use crate::transformable::{Transformable3D, Points, Mappable, Points2D};
use crate::{math3d_ops, constants};

#[derive(Debug, Clone, Copy, PartialEq, StructOps, IntervalOps)]
pub struct AABox<T: Float = f32> {
    pub min: Vector3::<T>,
    pub max: Vector3::<T>,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps, IntervalOps)]
pub struct AABox2D<T: Float = f32> {
    pub min: Vector2::<T>,
    pub max: Vector2::<T>,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps, IntervalOps)]
pub struct AABox4D<T: Float = f32> {
    pub min: Vector4::<T>,
    pub max: Vector4::<T>,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps)]
pub struct AxisAngle<T: Float = f64> {
    pub axis: Vector3::<T>,
    pub angle: T,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps)]
pub struct Byte2 {
    pub x: u8,
    pub y: u8,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps)]
pub struct Byte3 {
    pub x: u8,
    pub y: u8,
    pub z: u8,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps)]
pub struct Byte4 {
    pub x: u8,
    pub y: u8,
    pub z: u8,
    pub w: u8,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps)]
pub struct ColorHDR {
    pub r: f32,
    pub g: f32,
    pub b: f32,
    pub a: f32,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps)]
pub struct ColorRGB {
    pub r: u8,
    pub g: u8,
    pub b: u8,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps)]
pub struct ColorRGBA {
    pub r: u8,
    pub g: u8,
    pub b: u8,
    pub a: u8,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps, VectorComponentOps, VectorOperators)]
pub struct Complex<T: Float = f64> {
    pub real: T,
    pub imaginary: T,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps)]
pub struct CylindricalCoordinate<T: Float = f64> {
    pub radius: T,
    pub azimuth: T,
    pub height: T,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps, VectorComponentOps, VectorOperators)]
pub struct Euler<T: Float = f32> {
    pub yaw: T,
    pub pitch: T,
    pub roll: T,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps, VectorComponentOps, VectorOperators)]
pub struct GeoCoordinate<T: Float = f64> {
    pub latitude: T,
    pub longitude: T,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps, VectorComponentOps, VectorOperators)]
pub struct HorizontalCoordinate<T: Float = f64> {
    pub azimuth: T,
    pub inclination: T,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps, VectorComponentOps, VectorOperators)]
pub struct Int2 {
    pub x: i32,
    pub y: i32,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps, VectorComponentOps, VectorOperators)]
pub struct Int3 {
    pub x: i32,
    pub y: i32,
    pub z: i32,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps, VectorComponentOps, VectorOperators)]
pub struct Int4 {
    pub x: i32,
    pub y: i32,
    pub z: i32,
    pub w: i32,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps, IntervalOps)]
pub struct Interval<T: Float = f32> {
    pub min: T,
    pub max: T,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps)]
pub struct Line<T: Float = f32> {
    pub a: Vector3::<T>,
    pub b: Vector3::<T>,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps)]
pub struct Line2D<T: Float = f32> {
    pub a: Vector2::<T>,
    pub b: Vector2::<T>,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps)]
pub struct LogPolarCoordinate<T: Float = f64> {
    pub rho: T,
    pub azimuth: T,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps)]
pub struct Plane<T: Float = f32> {
    pub normal: Vector3::<T>,
    pub d: T,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps)]
pub struct PolarCoordinate<T: Float = f64> {
    pub radius: T,
    pub azimuth: T,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps)]
pub struct Quad<T: Float = f32> {
    pub a: Vector3::<T>,
    pub b: Vector3::<T>,
    pub c: Vector3::<T>,
    pub d: Vector3::<T>,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps)]
pub struct Quad2D<T: Float = f32> {
    pub a: Vector2::<T>,
    pub b: Vector2::<T>,
    pub c: Vector2::<T>,
    pub d: Vector2::<T>,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps, VectorComponentOps)]
pub struct Quaternion<T: Float = f32> {
    pub x: T,
    pub y: T,
    pub z: T,
    pub w: T,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps)]
pub struct Ray<T: Float = f32> {
    pub position: Vector3::<T>,
    pub direction: Vector3::<T>,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps)]
pub struct Sphere<T: Float = f32> {
    pub center: Vector3::<T>,
    pub radius: T,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps)]
pub struct SphericalCoordinate<T: Float = f64> {
    pub radius: T,
    pub azimuth: T,
    pub inclination: T,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps)]
pub struct Transform<T: Float = f32> {
    pub position: Vector3::<T>,
    pub orientation: Quaternion::<T>,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps)]
pub struct Triangle<T: Float = f32> {
    pub a: Vector3::<T>,
    pub b: Vector3::<T>,
    pub c: Vector3::<T>,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps)]
pub struct Triangle2D<T: Float = f32> {
    pub a: Vector2::<T>,
    pub b: Vector2::<T>,
    pub c: Vector2::<T>,
}

#[derive(Debug, Clone, Copy, Default, PartialEq, Eq, StructOps, VectorComponentOps, VectorOperators, VectorOps)]
pub struct Vector2<T: Float> {
    pub x: T,
    pub y: T,
}

#[derive(Debug, Clone, Copy, Default, PartialEq, Eq, StructOps, VectorComponentOps, VectorOperators, VectorOps)]
pub struct Vector3<T: Float> {
    pub x: T,
    pub y: T,
    pub z: T,
}

#[derive(Debug, Clone, Copy, Default, PartialEq, Eq, StructOps, VectorComponentOps, VectorOperators, VectorOps)]
pub struct Vector4<T: Float> {
    pub x: T,
    pub y: T,
    pub z: T,
    pub w: T,
}

#[derive(Debug, Clone, Copy, PartialEq, StructOps)]
pub struct Matrix4x4<T: Float> {    
    /// Value at row 1, column 1 of the matrix.
    pub m11: T,
    /// Value at row 1, column 2 of the matrix.
    pub m12: T,
    /// Value at row 1, column 3 of the matrix.
    pub m13: T,
    /// Value at row 1, column 4 of the matrix.
    pub m14: T,

    /// Value at row 2, column 1 of the matrix.
    pub m21: T,
    /// Value at row 2, column 2 of the matrix.
    pub m22: T,
    /// Value at row 2, column 3 of the matrix.
    pub m23: T,
    /// Value at row 2, column 4 of the matrix.
    pub m24: T,

    /// Value at row 3, column 1 of the matrix.
    pub m31: T,
    /// Value at row 3, column 2 of the matrix.
    pub m32: T,
    /// Value at row 3, column 3 of the matrix.
    pub m33: T,
    /// Value at row 3, column 4 of the matrix.
    pub m34: T,

    /// Value at row 4, column 1 of the matrix.
    pub m41: T,
    /// Value at row 4, column 2 of the matrix.
    pub m42: T,
    /// Value at row 4, column 3 of the matrix.
    pub m43: T,
    /// Value at row 4, column 4 of the matrix.
    pub m44: T,
}

#[derive(Debug, Clone, Copy, PartialEq, Eq)]
pub struct ValueDomain<T: Float + PartialOrd> {
    pub lower: T,
    pub upper: T,
}

#[derive(Debug, Clone, Copy, PartialEq, Default)]
pub struct Stats<T> {
    count: usize,
    min: T,
    max: T,
    sum: T,
}


#[derive(Debug, PartialEq, Eq)]
pub enum ContainmentType {
    // Indicates that there is no overlap between two bounding volumes.
    Disjoint,
    // Indicates that one bounding volume completely contains another volume.
    Contains,
    // Indicates that bounding volumes partially overlap one another.
    Intersects,
}

#[derive(Debug, PartialEq, Eq)]
pub enum PlaneIntersectionType {
    /// There is no intersection, the bounding volume is in the negative half space of the plane.
    Front,
    /// There is no intersection, the bounding volume is in the positive half space of the plane.
    Back,
    /// The plane is intersected.
    Intersecting
}

//PrimInt

impl ColorRGBA  {
    pub const LIGHT_RED: ColorRGBA = ColorRGBA { r: 255, g: 128, b: 128, a: 255 };
    pub const DARK_RED: ColorRGBA = ColorRGBA { r: 255, g: 0, b: 0, a: 255 };
    pub const LIGHT_GREEN: ColorRGBA = ColorRGBA { r: 128, g: 255, b: 128, a: 255 };
    pub const DARK_GREEN: ColorRGBA = ColorRGBA { r: 0, g: 255, b: 0, a: 255 };
    pub const LIGHT_BLUE: ColorRGBA = ColorRGBA { r: 128, g: 128, b: 255, a: 255 };
    pub const DARK_BLUE: ColorRGBA = ColorRGBA { r: 0, g: 0, b: 255, a: 255 };
}

impl<T: Float> Vector4<T> {
    pub fn new_from_vector3(v: Vector3<T>, w: T) -> Self { Self {x: v.x, y: v.y, z: v.z, w } }
    pub fn new_from_vector2(v: Vector2<T>, z: T, w: T) -> Self { Self { x: v.x, y: v.y, z, w } }
    pub fn new_from_num(v: T) -> Self { Self::new(v, v, v, v) }

    pub fn xyz(&self) -> Vector3<T> { Vector3::<T> { x: self.x, y: self.y, z: self.z } }
    pub fn xy(&self) -> Vector2<T> { Vector2::<T> { x: self.x, y: self.y } }

    pub fn transform_quaternion(&self, rotation: Quaternion<T>) -> Self {
        let x2 = rotation.x + rotation.x;
        let y2 = rotation.y + rotation.y;
        let z2 = rotation.z + rotation.z;

        let wx2 = rotation.w * x2;
        let wy2 = rotation.w * y2;
        let wz2 = rotation.w * z2;
        let xx2 = rotation.x * x2;
        let xy2 = rotation.x * y2;
        let xz2 = rotation.x * z2;
        let yy2 = rotation.y * y2;
        let yz2 = rotation.y * z2;
        let zz2 = rotation.z * z2;

        Self::new(
            self.x * (T::one() - yy2 - zz2) + self.y * (xy2 - wz2) + self.z * (xz2 + wy2),
            self.x * (xy2 + wz2) + self.y * (T::one() - xx2 - zz2) + self.z * (yz2 - wx2),
            self.x * (xz2 - wy2) + self.y * (yz2 + wx2) + self.z * (T::one() - xx2 - yy2),
            self.w,
        )
    }
    
    pub fn to_vector2(&self) -> Vector2<T> { Vector2::new(self.x, self.y) }
    pub fn to_vector3(&self) -> Vector3<T> { Vector3::<T>::new(self.x, self.y, self.z) }
    pub fn from_vector2(v: Vector2<T>) -> Self { Self::new(v.x, v.y, T::zero(), T::zero()) }
}

impl<T: Float + FloatConst> Transformable3D<T> for Vector4<T> {
    type Output = Self;

    #[inline(always)]
    fn transform(&self, matrix: Matrix4x4<T>) -> Self::Output {
        Self::Output {
            x: self.x * matrix.m11 + self.y * matrix.m21 + self.z * matrix.m31 + self.w * matrix.m41,
            y: self.x * matrix.m12 + self.y * matrix.m22 + self.z * matrix.m32 + self.w * matrix.m42,
            z: self.x * matrix.m13 + self.y * matrix.m23 + self.z * matrix.m33 + self.w * matrix.m43,
            w: self.x * matrix.m14 + self.y * matrix.m24 + self.z * matrix.m34 + self.w * matrix.m44,
        }
    }
}

impl<T: Float + FloatConst + PartialOrd> Vector3<T> {
    #[inline(always)]
    pub fn new_from_xy(x: T, y: T) -> Self { Self { x, y, z: T::zero() } }
    #[inline(always)]
    pub fn new_from_vector2(xy: Vector2<T>, z: T) -> Self { Self { x: xy.x, y: xy.y, z } }

    /// Computes the cross product of two vectors.
    #[inline(always)]
    pub fn cross(self, vector: Self) -> Self {
        Self {
            x: self.y * vector.z - self.z * vector.y,
            y: self.z * vector.x - self.x * vector.z,
            z: self.x * vector.y - self.y * vector.x,
        }
    }
    /// Returns the mixed product
    #[inline(always)]
    pub fn mixed_product(self, v1: Self, v2: Self) -> T { self.cross(v1).dot(v2) }
    /// Returns the reflection of a vector off a surface that has the specified normal.
    #[inline(always)]
    pub fn reflect(self, normal: Self) -> Self { self - normal * self.dot(normal) * T::from(2).unwrap() }
    /// Transforms a vector normal by the given matrix.
    #[inline(always)]
    pub fn transform_normal(self, matrix: Matrix4x4<T>) -> Self {
        Self {
            x: self.x * matrix.m11 + self.y * matrix.m21 + self.z * matrix.m31,
            y: self.x * matrix.m12 + self.y * matrix.m22 + self.z * matrix.m32,
            z: self.x * matrix.m13 + self.y * matrix.m23 + self.z * matrix.m33,
        }
    }

    #[inline(always)]
    pub fn clamp_box(self, aabox: &AABox<T>) -> Self { self.clamp(aabox.min, aabox.max) }

    #[inline(always)]
    pub fn xy(&self) -> Vector2<T> { Vector2::<T> { x: self.x, y: self.y } }
    #[inline(always)]
    pub fn xz(&self) -> Vector2<T> { Vector2::<T> { x: self.x, y: self.z } }
    #[inline(always)]
    pub fn yz(self) -> Vector2<T> { Vector2::<T> { x: self.y, y: self.z } }
    
    #[inline(always)]
    pub fn xzy(self) -> Self { Self { x: self.x, y: self.z, z: self.y } }
    #[inline(always)]
    pub fn zxy(self) -> Self { Self { x: self.z, y: self.x, z: self.y } }
    #[inline(always)]
    pub fn zyx(self) -> Self { Self { x: self.z, y: self.y, z: self.x } }
    #[inline(always)]
    pub fn yxz(self) -> Self { Self { x: self.y, y: self.x, z: self.z } }
    #[inline(always)]
    pub fn yzx(self) -> Self { Self { x: self.y, y: self.z, z: self.x } }

    pub fn to_vector2(&self) -> Vector2<T> { Vector2::<T>::new(self.x, self.y) }
    pub fn to_vector4(&self) -> Vector4<T> { Vector4::<T>::new(self.x, self.y, self.z, T::zero()) }
    pub fn from_num(v: T) -> Self { Self::new(v, v, v) }

    pub fn rotate(&self, axis: Self, angle: T) -> Self { self.transform(Matrix4x4::create_from_axis_angle(axis, angle)) }
    pub fn is_non_zero_and_valid(&self) -> bool {
        let length_squared = self.length_squared();
        !length_squared.is_infinite() && !length_squared.is_nan() && length_squared.abs() > constants::tolerance()
    }
    pub fn is_zero_or_invalid(&self) -> bool { !self.is_non_zero_and_valid() }
    pub fn is_perpendicular(v1: Self, v2: Self, tolerance: T) -> bool { v1 != Vector3::zero() && v2 != Vector3::zero() && math3d_ops::almost_zero(&v1.dot(v2), &tolerance) }
    pub fn projection(&self, v2: Self) -> Self { v2 * (self.dot(v2) / v2.length_squared()) }
    pub fn rejection(&self, v2: Self) -> Self { *self - self.projection(v2) }
    // The smaller of the two possible angles between the two vectors is returned, therefore the result will never be greater than 180 degrees or smaller than -180 degrees.
    // If you imagine the from and to vectors as lines on a piece of paper, both originating from the same point, then the /axis/ vector would point up out of the paper.
    // The measured angle between the two vectors would be positive in a clockwise direction and negative in an anti-clockwise direction.
    pub fn signed_angle(&self, to: Self, axis: Option<Self>) -> T {
        self.angle(to, constants::tolerance()) * T::signum(axis.unwrap_or(Self::unit_z()).dot(self.cross(to))) 
    }
    pub fn angle(&self, v2: Self, tolerance: T) -> T {
        let d = (self.length_squared() * v2.length_squared()).sqrt();
        if d < tolerance { return T::zero(); }
        math3d_ops::clamp(self.dot(v2) / d, -T::one(), T::one()).acos()
    }
    pub fn colinear(&self, v2: Self, tolerance: Option<T>) -> bool {
        let t = tolerance.unwrap_or(constants::tolerance());
        !self.is_nan() && !v2.is_nan() && self.signed_angle(v2, None) <= t
    }
    pub fn is_back_face(&self, line_of_sight: Self) -> bool { self.dot(line_of_sight) < T::zero() }

    pub fn catmull_rom(&self, value2: Self, value3: Self, value4: Self, amount: T) -> Self {
        Self::new(
            math3d_ops::catmull_rom(self.x, value2.x, value3.x, value4.x, amount),
            math3d_ops::catmull_rom(self.y, value2.y, value3.y, value4.y, amount),
            math3d_ops::catmull_rom(self.z, value2.z, value3.z, value4.z, amount),
        )
    }

    pub fn hermite(&self, tangent1: Self, value2: Self, tangent2: Self, amount: T) -> Self {
        Self::new(
            math3d_ops::hermite(self.x, tangent1.x, value2.x, tangent2.x, amount),
            math3d_ops::hermite(self.y, tangent1.y, value2.y, tangent2.y, amount),
            math3d_ops::hermite(self.z, tangent1.z, value2.z, tangent2.z, amount),
        )
    }

    pub fn smooth_step(&self, value2: Self, amount: T) -> Self {
        Self::new(
            math3d_ops::smooth_step(self.x, value2.x, amount),
            math3d_ops::smooth_step(self.y, value2.y, amount),
            math3d_ops::smooth_step(self.z, value2.z, amount),
        )
    }

    pub fn to_line(&self) -> Line<T> { Line::<T>::new(Self::zero(), *self) }
    pub fn along(&self, d: T) -> Self { self.normalize() * d }
    pub fn along_x(value: T) -> Self { Self::unit_x() * value }
    pub fn along_y(value: T) -> Self { Self::unit_y() * value }
    pub fn along_z(value: T) -> Self { Self::unit_z() * value }
    
     /// Transforms a vector by the given Quaternion rotation value.
    pub fn transform_quaternion(&self, rotation: Quaternion<T>) -> Self {
        let x2 = rotation.x + rotation.x;
        let y2 = rotation.y + rotation.y;
        let z2 = rotation.z + rotation.z;

        let wx2 = rotation.w * x2;
        let wy2 = rotation.w * y2;
        let wz2 = rotation.w * z2;
        let xx2 = rotation.x * x2;
        let xy2 = rotation.x * y2;
        let xz2 = rotation.x * z2;
        let yy2 = rotation.y * y2;
        let yz2 = rotation.y * z2;
        let zz2 = rotation.z * z2;

        Self::new(
            self.x * (T::one() - yy2 - zz2) + self.y * (xy2 - wz2) + self.z * (xz2 + wy2),
            self.x * (xy2 + wz2) + self.y * (T::one() - xx2 - zz2) + self.z * (yz2 - wx2),
            self.x * (xz2 - wy2) + self.y * (yz2 + wx2) + self.z * (T::one() - xx2 - yy2),
        )
    }
    /// Returns true if the four points are co-planar. 
    pub fn coplanar(v1: Self, v2: Self, v3: Self, v4: Self, epsilon: Option<T>) -> bool {
        (v3 - v1).dot((v2 - v1).cross(v4 - v1)).abs() < epsilon.unwrap_or(constants::tolerance())
    }
    /// Returns a translation matrix. 
    pub fn to_matrix(&self) -> Matrix4x4<T> { Matrix4x4::<T>::create_translation(*self) }
    /// Transforms a vector by the given matrix.
    pub fn transform_to_vector4(&self, matrix: Matrix4x4<T>) -> Vector4<T> {
        Vector4::<T>::new(
            self.x * matrix.m11 + self.y * matrix.m21 + self.z * matrix.m31 + matrix.m41,
            self.x * matrix.m12 + self.y * matrix.m22 + self.z * matrix.m32 + matrix.m42,
            self.x * matrix.m13 + self.y * matrix.m23 + self.z * matrix.m33 + matrix.m43,
            self.x * matrix.m14 + self.y * matrix.m24 + self.z * matrix.m34 + matrix.m44,
        )
    }
    pub fn transform_to_vector4_quaternion(&self, rotation: Quaternion<T>) -> Vector4<T> {
        let x2 = rotation.x + rotation.x;
        let y2 = rotation.y + rotation.y;
        let z2 = rotation.z + rotation.z;
    
        let wx2 = rotation.w * x2;
        let wy2 = rotation.w * y2;
        let wz2 = rotation.w * z2;
        let xx2 = rotation.x * x2;
        let xy2 = rotation.x * y2;
        let xz2 = rotation.x * z2;
        let yy2 = rotation.y * y2;
        let yz2 = rotation.y * z2;
        let zz2 = rotation.z * z2;
    
        Vector4::<T>::new(
            self.x * (T::one() - yy2 - zz2) + self.y * (xy2 - wz2) + self.z * (xz2 + wy2),
            self.x * (xy2 + wz2) + self.y * (T::one() - xx2 - zz2) + self.z * (yz2 - wx2),
            self.x * (xz2 - wy2) + self.y * (yz2 + wx2) + self.z * (T::one() - xx2 - yy2),
            T::one(),
        )
    }
    
   
}

impl<T: Float + FloatConst> Transformable3D<T> for Vector3<T> {
    type Output = Self;

    /// Transforms a vector by the given matrix.
    fn transform(&self, matrix: Matrix4x4<T>) -> Self::Output {
        Self::Output {
            x: self.x * matrix.m11 + self.y * matrix.m21 + self.z * matrix.m31 + matrix.m41, 
            y: self.x * matrix.m12 + self.y * matrix.m22 + self.z * matrix.m32 + matrix.m42,  
            z: self.x * matrix.m13 + self.y * matrix.m23 + self.z * matrix.m33 + matrix.m43,
        }
    }
}

impl<T: Float> Line<T> {
    pub fn vector(&self) -> Vector3<T> { self.b - self.a }
    pub fn ray(&self) -> Ray<T> { Ray::<T> { position: self.a, direction: self.vector() } }
    pub fn length(&self) -> T { self.a.distance(self.b) }
    pub fn length_squared(&self) -> T { self.a.distance_squared(self.b) }
    pub fn midpoint(&self) -> Vector3<T> { self.a.average(self.b) }
    pub fn normal(self) -> Self { Self { a: self.a, b: self.a + self.vector().normalize() } }
    pub fn inverse(self) -> Self { Self { a: self.b, b: self.a } }
    #[inline(always)]
    pub fn lerp(&self, amount: T) -> Vector3<T> { self.a.lerp(self.b, amount) }
    // #[inline(always)]
    // pub fn set_length(self, length: T) -> Self { Self { a: self.a, b: self.a + self.vector().along(length) } }
}

impl<T: Float> Mappable<Vector3<T>> for Line<T> {
    type Container = Self;

    #[inline(always)]
    fn map<F>(self, f: F) -> Self::Container
    where
        F: Fn(Vector3<T>) -> Vector3<T> { 
            Self {a: f(self.a), b: f(self.b) } 
        }
}

impl<T: Float> Points<T> for Line<T> {
    fn num_points(&self) -> usize { 2 }
    #[inline(always)]
    fn get_point(&self, n: usize) -> Vector3<T> { if n == 0 { self.a } else { self.b } }
}

impl<T: Float + FloatConst> Transformable3D<T> for Line<T> {
    type Output = Self;

    #[inline(always)]
    fn transform(&self, mat: Matrix4x4<T>) -> Self {
        Self { a: self.a.transform(mat), b: self.b.transform(mat) }
    }
}

impl Int2 {
    pub fn to_vector2<T: Float>(&self) -> Vector2<T> {
        Vector2::<T> { x: T::from(self.x).unwrap(), y: T::from(self.y).unwrap() }
    }
}

impl<T: Float> From<Int2> for Vector2<T> {
    fn from(val: Int2) -> Self { val.to_vector2() }
}

impl Int3 {
    pub fn to_vector3<T: Float>(&self) -> Vector3<T> {
        Vector3::<T> { x: T::from(self.x).unwrap(), y: T::from(self.y).unwrap(), z: T::from(self.z).unwrap() }
    }
}

impl<T: Float> From<Int3> for Vector3<T> {
    fn from(val: Int3) -> Self { val.to_vector3() }
}

impl<T: Float> Vector2<T> {
    pub fn to_vector3(&self) -> Vector3<T> { Vector3::<T> { x: self.x, y:self.y, z: T::zero() } }
    pub fn to_vector4(&self) -> Vector4<T> { Vector4::<T>::new(self.x, self.y, T::zero(), T::zero()) }

    pub fn point_cross_product(&self, other: Self) -> T { self.x * other.y - other.x * self.y }
    pub fn cross(&self, v2: Self) -> T { self.x * v2.y - self.y * v2.x }
    pub fn from_num(v: T) -> Self { Self::new(v, v) }
     /// Returns the reflection of a vector off a surface that has the specified normal.
    pub fn reflect(&self, normal: Self) -> Self { *self - ((normal * self.dot(normal)) * T::from(2).unwrap()) }
    /// Transforms a vector normal by the given matrix.
    pub fn transform_normal(&self, matrix: Matrix4x4<T>) -> Self {
        Self::new(
            self.x * matrix.m11 + self.y * matrix.m21,
            self.x * matrix.m12 + self.y * matrix.m22,
        )
    }
    /// Transforms a vector by the given Quaternion rotation value.
    pub fn transform_quaternion(&self, rotation: Quaternion<T>) -> Self {
        let x2 = rotation.x + rotation.x;
        let y2 = rotation.y + rotation.y;
        let z2 = rotation.z + rotation.z;
    
        let wz2 = rotation.w * z2;
        let xx2 = rotation.x * x2;
        let xy2 = rotation.x * y2;
        let yy2 = rotation.y * y2;
        let zz2 = rotation.z * z2;
    
        Self::new(
            self.x * (T::one() - yy2 - zz2) + self.y * (xy2 - wz2),
            self.x * (xy2 + wz2) + self.y * (T::one() - xx2 - zz2),
        )
    }
    /// Transforms a vector by the given Quaternion rotation value.
    pub fn transform_to_vector4_quaternion(&self, rotation: Quaternion<T>) -> Vector4<T> {
        let x2 = rotation.x + rotation.x;
        let y2 = rotation.y + rotation.y;
        let z2 = rotation.z + rotation.z;
    
        let wx2 = rotation.w * x2;
        let wy2 = rotation.w * y2;
        let wz2 = rotation.w * z2;
        let xx2 = rotation.x * x2;
        let xy2 = rotation.x * y2;
        let xz2 = rotation.x * z2;
        let yy2 = rotation.y * y2;
        let yz2 = rotation.y * z2;
        let zz2 = rotation.z * z2;
    
        Vector4::<T>::new(
            self.x * (T::one() - yy2 - zz2) + self.y * (xy2 - wz2),
            self.x * (xy2 + wz2) + self.y * (T::one() - xx2 - zz2),
            self.x * (xz2 - wy2) + self.y * (yz2 + wx2),
            T::one(),
        )
    }
    /// Transforms a vector by the given matrix.
    pub fn transform_to_vector4(&self, matrix: Matrix4x4<T>) -> Vector4<T> {
        Vector4::<T>::new(
            self.x * matrix.m11 + self.y * matrix.m21 + matrix.m41,
            self.x * matrix.m12 + self.y * matrix.m22 + matrix.m42,
            self.x * matrix.m13 + self.y * matrix.m23 + matrix.m43,
            self.x * matrix.m14 + self.y * matrix.m24 + matrix.m44,
        )
    }
    
}

impl<T: Float + FloatConst + AddAssign> Transformable3D<T> for Vector2<T> {
    type Output = Self;
    /// Transforms the box by a 4x4 matrix.
    fn transform(&self, matrix: Matrix4x4<T>) -> Self {
        Self::new(
            self.x * matrix.m11 + self.y * matrix.m21 + matrix.m41,
            self.x * matrix.m12 + self.y * matrix.m22 + matrix.m42,
        )
    }
}

impl<T: Float> From<Vector2<T>> for Vector3<T> {
    fn from(v: Vector2<T>) -> Self { Self { x: v.x, y: v.y, z: T::zero() } }
}

impl<T: Float> Line2D<T> {
    #[inline(always)]
    pub fn bounding_box(&self) -> AABox2D<T> { AABox2D::new(self.a.min(self.b), self.a.max(self.b)) }

    #[inline(always)]
    pub fn line_point_cross_product(&self, point: Vector2<T>) -> T {
        let tmp_line = Self::new(Vector2::zero(), self.b - self.a);
        let tmp_point = point - self.a;
        tmp_line.b.point_cross_product(tmp_point)
    }

    #[inline(always)]
    pub fn is_point_on_line(&self, point: Vector2<T>) -> bool {
        self.line_point_cross_product(point).abs() < constants::tolerance()
    }

    #[inline(always)]
    pub fn is_point_right_of_line(&self, point: Vector2<T>) -> bool {
        self.line_point_cross_product(point) < T::zero()
    }

    #[inline(always)]
    pub fn touches_or_crosses(&self, other: &Line2D<T>) -> bool {
        self.is_point_on_line(other.a)
            || self.is_point_on_line(other.b)
            || (self.is_point_right_of_line(other.a) ^ self.is_point_right_of_line(other.b))
    }

    #[inline(always)]
    pub fn intersects_with_box(&self, this_box: &AABox2D<T>, other_line: &Line2D<T>, other_box: &AABox2D<T>) -> bool {
        this_box.intersects(other_box)
            && self.touches_or_crosses(&other_line)
            && other_line.touches_or_crosses(self)
    }

    #[inline(always)]
    pub fn intersects(&self, other: &Line2D<T>) -> bool {
         // Inspired by: https://martin-thoma.com/how-to-check-if-two-line-segments-intersect/
        self.intersects_with_box(&self.bounding_box(), other, &other.bounding_box())
    }
}

impl<T: Float + FloatConst + PartialOrd> Transform<T> {
    pub fn identity(&self) -> Self {
        Self { position: Vector3::<T>::zero(), orientation: Quaternion::<T>::identity() }
    }
}

impl<T: Float> HorizontalCoordinate<T> {
    pub fn from_vector(vector: Vector2<T>) -> HorizontalCoordinate<T> {
        Self { azimuth: vector.x, inclination: vector.y }
    }
}

impl<T: Float> From<HorizontalCoordinate<T>> for Vector2<T> {
    fn from(coord: HorizontalCoordinate<T>) -> Self {
        Self { x: coord.azimuth, y: coord.inclination }
    }
}

impl<T: Float> From<Vector2<T>> for HorizontalCoordinate<T> {
    fn from(vector: Vector2<T>) -> HorizontalCoordinate<T> {
        HorizontalCoordinate::<T>::from_vector(vector)
    }
}

impl<T: Float + AddAssign> AABox<T> {
    // CCW
    pub const TOP_INDICES: [usize; 4] = [0, 1, 2, 3];
    pub const BOTTOM_INDICES: [usize; 4] = [7, 6, 5, 4];
    pub const FRONT_INDICES: [usize; 4] = [4, 5, 1, 0];
    pub const RIGHT_INDICES: [usize; 4] = [5, 6, 2, 1];
    pub const BACK_INDICES: [usize; 4] = [6, 7, 3, 2];
    pub const LEFT_INDICES: [usize; 4] = [7, 4, 0, 3];

    /// Create a bounding box from the given list of points.
    #[inline(always)]
    pub fn new_from_points(points: &[Vector3<T>]) -> Self {
        let mut min = Vector3::max_value();
        let mut max = Vector3::min_value();
        for pt in points {
            min = min.min(*pt);
            max = max.max(*pt);
        }
        Self { min, max }
    }
    #[inline(always)]
    pub fn new_from_sphere(sphere: Sphere<T>) -> Self { Self { 
            min: sphere.center - Vector3::create_from_value(sphere.radius),
            max: sphere.center + Vector3::create_from_value(sphere.radius)
        }
    }
    /// Creates a new axis-aligned bounding box from its center and extent.
    pub fn new_from_center_and_extent(center: Vector3<T>, extent: Vector3<T>) -> Self {
        let half_extent = extent / (T::from(2).unwrap());
        Self { min: center - half_extent, max: center + half_extent }
    }

    pub fn unit() -> Self { Self { min: Vector3::<T>::zero(), max: Vector3::<T>::one() } }
    /// Sets the center of the box.
    pub fn set_center(&self, v: Vector3<T>) -> Self { Self::new_from_center_and_extent(v, self.extent()) }
    /// Sets the extent of the box.
    pub fn set_extent(&self, v: Vector3<T>) -> Self { Self::new_from_center_and_extent(self.center(), v) }

    pub fn count(&self) -> usize { 2 }
    pub fn center_bottom(&self) -> Vector3<T> { self.center().set_z(self.min.z) }
    /// This is the four front corners followed by the four back corners all as if looking from the front
    /// going in counter-clockwise order from bottom left. 
    #[inline(always)]
    pub fn corners(&self) -> [Vector3<T>; 8] { [
        // Bottom (looking down)
        Vector3::new(self.min.x, self.min.y, self.min.z),
        Vector3::new(self.max.x, self.min.y, self.min.z),
        Vector3::new(self.max.x, self.max.y, self.min.z),
        Vector3::new(self.min.x, self.max.y, self.min.z),
        // Top (looking down)
        Vector3::new(self.min.x, self.min.y, self.max.z),
        Vector3::new(self.max.x, self.min.y, self.max.z),
        Vector3::new(self.max.x, self.max.y, self.max.z),
        Vector3::new(self.min.x, self.max.y, self.max.z), ]
    }
    /// Returns the center of each face.
    #[inline(always)]
    pub fn face_centers(&self) -> [Vector3<T>; 6] {
        let corners = self.corners();
        [
            corners[Self::FRONT_INDICES[0]].average(corners[Self::FRONT_INDICES[2]]),
            corners[Self::RIGHT_INDICES[0]].average(corners[Self::RIGHT_INDICES[2]]),
            corners[Self::BACK_INDICES[0]].average(corners[Self::BACK_INDICES[2]]),
            corners[Self::LEFT_INDICES[0]].average(corners[Self::LEFT_INDICES[2]]),
            corners[Self::TOP_INDICES[0]].average(corners[Self::TOP_INDICES[2]]),
            corners[Self::BOTTOM_INDICES[0]].average(corners[Self::BOTTOM_INDICES[2]]),
        ]
    }
    /// Returns corners and face centers as an iterator.
    #[inline(always)]
    pub fn corners_and_face_centers(&self) -> [Vector3<T>; 14] {
        let corners = self.corners();
        let face_centers = self.face_centers();
        [
            corners[0], corners[1], corners[2], corners[3], corners[4], corners[5], corners[6], corners[7],
            face_centers[0], face_centers[1], face_centers[2], face_centers[3], face_centers[4], face_centers[5],
        ]
    }

    pub fn is_empty(&self) -> bool { !self.is_valid() }
    pub fn is_valid(&self) -> bool { self.min.x <= self.max.x && self.min.y <= self.max.y && self.min.z <= self.max.z }
    
    // Inspired by: https://stackoverflow.com/questions/5254838/calculating-distance-between-a-point-and-a-rectangular-box-nearest-point
    #[inline(always)]
    pub fn distance(&self, point: Vector3<T>) -> T { Vector3::zero().max(self.min - point).max(point - self.max).length() }
    /// Returns the distance of the point to the box center. 
    #[inline(always)]
    pub fn center_distance(&self, point: Vector3<T>) -> T { self.center().distance(point) }
    /// Moves the box by the given vector offset
    #[inline(always)]
    pub fn translate(&self, offset: Vector3<T>) -> Self { Self { min: self.min + offset, max: self.max + offset } }
    pub fn distance_to_origin(&self) -> T { self.distance(Vector3::zero()) }
    pub fn center_distance_to_origin(&self) -> T { self.center_distance(Vector3::zero()) }
    pub fn volume(&self) -> T { if self.is_empty() { T::zero() } else { self.extent().product_components() } }
    pub fn max_side(&self) -> T { self.extent().max_component() }
    pub fn max_face_area(&self) -> T {
        if self.extent().x > self.extent().y { self.extent().x * self.extent().z.max(self.extent().y) } 
        else { self.extent().y * self.extent().z.max(self.extent().x) }
    }
    pub fn min_side(&self) -> T { self.extent().min_component() }
    pub fn diagonal(&self) -> T { self.extent().length() }

    #[inline(always)]
    pub fn contains(&self, other: &Self) -> ContainmentType {
        if other.max.x < self.min.x
            || other.min.x > self.max.x
            || other.max.y < self.min.y
            || other.min.y > self.max.y
            || other.max.z < self.min.z
            || other.min.z > self.max.z
        {
            ContainmentType::Disjoint
        } else if other.min.x >= self.min.x
            && other.max.x <= self.max.x
            && other.min.y >= self.min.y
            && other.max.y <= self.max.y
            && other.min.z >= self.min.z
            && other.max.z <= self.max.z
        {
            ContainmentType::Contains
        } else {
            ContainmentType::Intersects
        }
    }
    pub fn contains_sphere(&self, sphere: &Sphere<T>) -> ContainmentType {
        if sphere.center.x - self.min.x >= sphere.radius
            && sphere.center.y - self.min.y >= sphere.radius
            && sphere.center.z - self.min.z >= sphere.radius
            && self.max.x - sphere.center.x >= sphere.radius
            && self.max.y - sphere.center.y >= sphere.radius
            && self.max.z - sphere.center.z >= sphere.radius
        {
            return ContainmentType::Contains;
        }
    
        let mut dmin = T::zero();
        let mut e = sphere.center.x - self.min.x;
        if e < T::zero() {
            if e < -sphere.radius {
                return ContainmentType::Disjoint;
            }
            dmin += e * e;
        } else {
            e = sphere.center.x - self.max.x;
            if e > T::zero() {
                if e > sphere.radius {
                    return ContainmentType::Disjoint;
                }
                dmin += e * e;
            }
        }
    
        e = sphere.center.y - self.min.y;
        if e < T::zero() {
            if e < -sphere.radius {
                return ContainmentType::Disjoint;
            }
            dmin += e * e;
        } else {
            e = sphere.center.y - self.max.y;
            if e > T::zero() {
                if e > sphere.radius {
                    return ContainmentType::Disjoint;
                }
                dmin += e * e;
            }
        }
    
        e = sphere.center.z - self.min.z;
        if e < T::zero() {
            if e < -sphere.radius {
                return ContainmentType::Disjoint;
            }
            dmin += e * e;
        } else {
            e = sphere.center.z - self.max.z;
            if e > T::zero() {
                if e > sphere.radius {
                    return ContainmentType::Disjoint;
                }
                dmin += e * e;
            }
        }
    
        if dmin <= sphere.radius * sphere.radius {
            return ContainmentType::Intersects;
        }
    
        ContainmentType::Disjoint
    }   
    #[inline(always)]
    pub fn contains_point(&self, point: &Vector3<T>) -> bool {
        !(point.x < self.min.x
            || point.x > self.max.x
            || point.y < self.min.y
            || point.y > self.max.y
            || point.z < self.min.z
            || point.z > self.max.z)
    }

    #[inline(always)]
    pub fn intersects(&self, other: &Self) -> bool {
        if self.max.x >= other.min.x && self.min.x <= other.max.x {
            if self.max.y < other.min.y || self.min.y > other.max.y { return false; }
            return self.max.z >= other.min.z && self.min.z <= other.max.z;
        }
        return false;
    }
    #[inline(always)]
    pub fn intersects_sphere(&self, sphere: &Sphere<T>) -> bool {
        if sphere.center.x - self.min.x > sphere.radius
            && sphere.center.y - self.min.y > sphere.radius
            && sphere.center.z - self.min.z > sphere.radius
            && self.max.x - sphere.center.x > sphere.radius
            && self.max.y - sphere.center.y > sphere.radius
            && self.max.z - sphere.center.z > sphere.radius
        {
            return true;
        }
    
        let mut dmin = T::zero();
        if sphere.center.x - self.min.x <= sphere.radius {
            dmin += (sphere.center.x - self.min.x) * (sphere.center.x - self.min.x);
        } else if self.max.x - sphere.center.x <= sphere.radius {
            dmin += (sphere.center.x - self.max.x) * (sphere.center.x - self.max.x);
        }
    
        if sphere.center.y - self.min.y <= sphere.radius {
            dmin += (sphere.center.y - self.min.y) * (sphere.center.y - self.min.y);
        } else if self.max.y - sphere.center.y <= sphere.radius {
            dmin += (sphere.center.y - self.max.y) * (sphere.center.y - self.max.y);
        }
    
        if sphere.center.z - self.min.z <= sphere.radius {
            dmin += (sphere.center.z - self.min.z) * (sphere.center.z - self.min.z);
        } else if self.max.z - sphere.center.z <= sphere.radius {
            dmin += (sphere.center.z - self.max.z) * (sphere.center.z - self.max.z);
        }
    
        if dmin <= sphere.radius * sphere.radius {
            return true;
        }
    
        false
    }
    #[inline(always)]
    pub fn intersects_plane(&self, plane: &Plane<T>) -> PlaneIntersectionType {
        let ax; let ay; let az; let bx; let by; let bz;

        if plane.normal.x >= T::zero() {
            ax = self.max.x;
            bx = self.min.x;
        } else {
            ax = self.min.x;
            bx = self.max.x;
        }
    
        if plane.normal.y >= T::zero() {
            ay = self.max.y;
            by = self.min.y;
        } else {
            ay = self.min.y;
            by = self.max.y;
        }
    
        if plane.normal.z >= T::zero() {
            az = self.max.z;
            bz = self.min.z;
        } else {
            az = self.min.z;
            bz = self.max.z;
        }
    
        let distance = plane.normal.x * bx + plane.normal.y * by + plane.normal.z * bz + plane.d;
        if distance > T::zero() {
            return PlaneIntersectionType::Front;
        }
    
        let distance = plane.normal.x * ax + plane.normal.y * ay + plane.normal.z * az + plane.d;
        if distance < T::zero() {
            return PlaneIntersectionType::Back;
        }
    
        PlaneIntersectionType::Intersecting
    }

    /// Returns where a point is relative to the bounding box on a scale of 0..1
    #[inline(always)]
    pub fn relative_position(&self, v: Vector3<T>) -> Vector3<T> { v.inverse_lerp(self.min, self.max) }
    /// Moves the box so that it's origin is on the center
    #[inline(always)]
    pub fn recenter(&self) -> Self { self.translate(-self.center()) }
    /// Rescales the box
    #[inline(always)]
    pub fn scale(&self, scale: T) -> Self {
        let rc = self.recenter();
        let aabox = Self { min: rc.min * scale, max: rc.max * scale };
        aabox.translate(self.center())
    }
    /// Given a normalized position in bounding box, returns the actual position.
    #[inline(always)]
    pub fn lerp(&self, v: Vector3<T>) -> Vector3<T> { self.min + self.extent() * v }

    // /// Returns the enclosing bounding sphere.
    #[inline(always)]
    pub fn to_sphere(&self) -> Sphere<T> {
        let center = self.center();
        Sphere::<T> { center, radius: center.distance(self.max) }
    }

    pub fn to_aabox_array(m: &[T]) -> Vec<Self> {
        const NUM_FLOATS: usize = 6;
        debug_assert!(m.len() % NUM_FLOATS == 0);
        let ret_length = m.len() / NUM_FLOATS;
        let mut ret = Vec::with_capacity(ret_length);

        for i in 0..ret_length {
            let i6 = i * NUM_FLOATS;
            let aabox = AABox::new(
                Vector3::new(m[i6 + 0], m[i6 + 1], m[i6 + 2]),
                Vector3::new(m[i6 + 3], m[i6 + 4], m[i6 + 5]),
            );
            ret.push(aabox);
        }
        ret
    }
}

impl<T: Float + FloatConst + AddAssign> Transformable3D<T> for AABox<T> {
    type Output = Self;
    /// Transforms the box by a 4x4 matrix.
    fn transform(&self, mat: Matrix4x4<T>) -> Self {
        AABox::<T>::new_from_points(&self.corners().map(|v| v.transform(mat)))
    }
}

impl<T: Float> AABox2D<T> {
    // CCW
    pub const INDICES: [usize; 4] = [0, 1, 2, 3];

    /// Create a bounding box from the given list of points.
    #[inline(always)]
    pub fn new_from_points(points: &[Vector2<T>]) -> Self {
        let mut min = Vector2::max_value();
        let mut max = Vector2::min_value();
        for pt in points {
            min = min.min(*pt);
            max = max.max(*pt);
        }
        Self { min, max }
    }

    pub fn unit() -> Self { Self { min: Vector2::<T>::zero(), max: Vector2::<T>::one() } }

    pub fn count(&self) -> usize { 2 }
    pub fn center_bottom(&self) -> Vector2<T> { self.center().set_y(self.min.y) }
    #[inline(always)]
    pub fn corners(&self) -> [Vector2<T>; 4] { [
        Vector2::new(self.min.x, self.min.y),
        Vector2::new(self.max.x, self.min.y),
        Vector2::new(self.max.x, self.max.y),
        Vector2::new(self.min.x, self.max.y) ]
    }

    pub fn is_empty(&self) -> bool { !self.is_valid() }
    pub fn is_valid(&self) -> bool { self.min.x <= self.max.x && self.min.y <= self.max.y }

    #[inline(always)]
    pub fn distance(&self, point: Vector2<T>) -> T { Vector2::zero().max(self.min - point).max(point - self.max).length() }
    /// Returns the distance of the point to the box center. 
    #[inline(always)]
    pub fn center_distance(&self, point: Vector2<T>) -> T { self.center().distance(point) }
    /// Moves the box by the given vector offset
    #[inline(always)]
    pub fn translate(&self, offset: Vector2<T>) -> Self { Self { min: self.min + offset, max: self.max + offset } }
    pub fn distance_to_origin(&self) -> T { self.distance(Vector2::zero()) }
    pub fn center_distance_to_origin(&self) -> T { self.center_distance(Vector2::zero()) }
    pub fn area(&self) -> T { if self.is_empty() { T::zero() } else { self.extent().product_components() } }
    pub fn max_side(&self) -> T { self.extent().max_component() }
    pub fn min_side(&self) -> T { self.extent().min_component() }
    pub fn diagonal(&self) -> T { self.extent().length() }

    #[inline(always)]
    pub fn contains(&self, aabox: &Self) -> ContainmentType {
        if aabox.max.x < self.min.x
            || aabox.min.x > self.max.x
            || aabox.max.y < self.min.y
            || aabox.min.y > self.max.y
        {
            ContainmentType::Disjoint
        } else if aabox.min.x >= self.min.x
            && aabox.max.x <= self.max.x
            && aabox.min.y >= self.min.y
            && aabox.max.y <= self.max.y
        {
            ContainmentType::Contains
        } else {
            ContainmentType::Intersects
        }
    }
    #[inline(always)]
    pub fn contains_point(&self, point: &Vector2<T>) -> bool {
        !(point.x < self.min.x || point.x > self.max.x || point.y < self.min.y || point.y > self.max.y)
    }
    #[inline(always)]
    pub fn intersects(&self, other: &Self) -> bool {
        self.min.x <= other.max.x && self.max.x >= other.min.x && self.min.y <= other.max.y && self.max.y >= other.min.y
    }

    /// Returns where a point is relative to the bounding box on a scale of 0..1 
    #[inline(always)]
    pub fn relative_position(&self, v: Vector2<T>) -> Vector2<T> { v.inverse_lerp(self.min, self.max) }
    /// Moves the box so that it's origin is on the center
    #[inline(always)]
    pub fn recenter(&self) -> Self { self.translate(-self.center()) }
    /// Rescales the box
    #[inline(always)]
    pub fn scale(&self, scale: T) -> Self {
        let recentered = self.recenter();
        let aabox =  Self {
            min: recentered.min * scale,
            max: recentered.max * scale,
        };
        aabox.translate(self.center())
    }
}

impl<T: Float + FloatConst + PartialOrd<T>> Quaternion<T> {
    /// Returns a Quaternion representing no rotation. 
    pub fn identity() -> Self { Self { x: T::zero(), y: T::zero(), z: T::zero(), w: T::one() } }
    /// Returns whether the Quaternion is the identity Quaternion.
    pub fn is_identity(&self) -> bool { self.x == T::zero() && self.y == T::zero() && self.z == T::zero() && self.w == T::one() }

    /// Constructs a Quaternion from the given vector and rotation parts.
    #[inline(always)]
    pub fn new_from_vector(vector_part: Vector3<T>, scalar_part: T) -> Self {
        Self { x: vector_part.x, y: vector_part.y, z: vector_part.z, w: scalar_part }
    }
    /// Creates a Quaternion from a normalized vector axis and an angle to rotate about the vector.
    #[inline(always)]
    pub fn new_from_axis_angle(axis: Vector3<T>, angle: T) -> Self {
        let half = T::from(0.5).unwrap();
        Self::new_from_vector(axis * (angle * half).sin(), (angle * half).cos())
    }
    /// Creates a new Quaternion from the given rotation around X, Y, and Z
    #[inline(always)]
    pub fn new_from_euler_angles(v: Vector3<T>) -> Self {
        let two = T::from(2.0).unwrap();
        let (c1, s1) = ((v.x / two).cos(), (v.x / two).sin());
        let (c2, s2) = ((v.y / two).cos(), (v.y / two).sin());
        let (c3, s3) = ((v.z / two).cos(), (v.z / two).sin());

        let qw = c1 * c2 * c3 - s1 * s2 * s3;
        let qx = s1 * c2 * c3 + c1 * s2 * s3;
        let qy = c1 * s2 * c3 - s1 * c2 * s3;
        let qz = c1 * c2 * s3 + s1 * s2 * c3;
        Self { x: qx, y: qy, z: qz, w: qw }
    }

    #[inline(always)]
    /// A structure encapsulating a four-dimensional vector (x,y,z,w), 
    /// which is used to efficiently rotate an object about the (x,y,z) vector by the angle theta, where w = cos(theta/2).
    pub fn new_from_rotation_matrix(matrix: Matrix4x4<T>) -> Quaternion<T> {
        let trace = matrix.m11 + matrix.m22 + matrix.m33;
        let half = T::from(0.5).unwrap();
    
        if trace > T::zero() {
            let s = (trace + T::one()).sqrt();
            let w = s * half;
            let s = half / s;
            return Quaternion::new(
                (matrix.m23 - matrix.m32) * s,
                (matrix.m31 - matrix.m13) * s,
                (matrix.m12 - matrix.m21) * s,
                w);
        }
        if matrix.m11 >= matrix.m22 && matrix.m11 >= matrix.m33 {
            let s = (T::one() + matrix.m11 - matrix.m22 - matrix.m33).sqrt();
            let invs = half / s;
            return Quaternion::new(
                half * s,
                (matrix.m12 + matrix.m21) * invs,
                (matrix.m13 + matrix.m31) * invs,
                (matrix.m23 - matrix.m32) * invs
            );
        }
        if matrix.m22 > matrix.m33 {
            let s = (T::one() + matrix.m22 - matrix.m11 - matrix.m33).sqrt();
            let invs = half / s;
            return Quaternion::new(
                (matrix.m21 + matrix.m12) * invs,
                half * s,
                (matrix.m32 + matrix.m23) * invs,
                (matrix.m31 - matrix.m13) * invs,
            );
        } 
        {
            let s = (T::one() + matrix.m33 - matrix.m11 - matrix.m22).sqrt();
            let invs = half / s;
            return Quaternion::new(
                (matrix.m31 + matrix.m13) * invs,
                (matrix.m32 + matrix.m23) * invs,
                half * s,
                (matrix.m12 - matrix.m21) * invs
            );
        }
    }
    
    /// Calculates the length of the Quaternion.
    #[inline(always)]
    pub fn length(&self) -> T { self.length_squared().sqrt() }

    /// Calculates the length squared of the Quaternion. This operation is cheaper than Length().
    #[inline(always)]
    pub fn length_squared(&self) -> T { self.x * self.x + self.y * self.y + self.z * self.z + self.w * self.w }

    /// Divides each component of the Quaternion by the length of the Quaternion.
    #[inline(always)]
    pub fn normalize(&self) -> Self { *self * self.length().recip() }

    /// Returns the conjugate of the quaternion
    #[inline(always)]
    pub fn conjugate(&self) -> Self { Self { x: -self.x, y: -self.y, z: -self.z, w: self.w } }

    /// Returns the inverse of a Quaternion.
    #[inline(always)]
    pub fn inverse(&self) -> Self { self.conjugate() * self.length_squared().recip() }

    /// Creates a new Quaternion from the given rotation around the X axis
    #[inline(always)]
    pub fn new_x_rotation(theta: T) -> Self {
        let half = T::from(0.5).unwrap();
        Self { x: (theta * half).sin(), y: T::zero(), z: T::zero(), w: (theta * half).cos() }
    }
    /// Creates a new Quaternion from the given rotation around the Y axis
    #[inline(always)]
    pub fn new_y_rotation(theta: T) -> Self {
        let half = T::from(0.5).unwrap();
        Self { x: T::zero(), y: (theta * half).sin(), z: T::zero(), w: (theta * half).cos() }
    }
    /// Creates a new Quaternion from the given rotation around the Z axis
    #[inline(always)]
    pub fn new_z_rotation(theta: T) -> Self {
        let half = T::from(0.5).unwrap();
        Self { x: T::zero(), y: T::zero(), z: (theta * half).sin(), w: (theta * half).cos() }
    }

    /// Creates a new look-at Quaternion
    #[inline(always)]
    pub fn look_at(position: Vector3<T>, target_position: Vector3<T>, up: Vector3<T>, forward: Vector3<T>) -> Self {
        let plane = Plane::new_from_normal_and_point(up, position);

        let projected_target = plane.project_point_onto_plane(target_position);
        let projected_direction = (projected_target - position).normalize();

        let q1 = Self::new_rotation_from_a_to_b(forward, projected_direction, Some(up));
        let q2 = Self::new_rotation_from_a_to_b(projected_direction, (target_position - position).normalize(), Some(up));

        q2 * q1
    }

    /// Creates a new Quaternion rotating vector 'fromA' to 'toB'.<br/>
    /// Precondition: fromA and toB are normalized.
    #[inline(always)]
    pub fn new_rotation_from_a_to_b(from_a: Vector3<T>, to_b: Vector3<T>, up: Option<Vector3<T>>) -> Self {
        let axis = from_a.cross(to_b);
        let length_squared = axis.length_squared();
        if length_squared > T::zero() {
            Self::new_from_axis_angle(
                axis / length_squared.sqrt(), 
                math3d_ops::clamp(from_a.dot(to_b),-T::one(), T::one()).acos())
        } else {
            if (from_a + to_b).almost_zero(constants::tolerance()) {
                Self::new_from_axis_angle(up.unwrap_or(Vector3::unit_z()), T::from(std::f32::consts::PI).unwrap())
            } else {
                Self::identity()
            }
        }
    }

    /// Creates a new Quaternion from the given yaw, pitch, and roll, in radians.
    ///  Roll first, about axis the object is facing, then
    ///  pitch upward, then yaw to face into the new heading
    ///  1. Z(roll), 2. X (pitch), 3. Y (yaw)  
    #[inline(always)]
    pub fn new_from_yaw_pitch_roll(yaw: T, pitch: T, roll: T) -> Self {
        let half = T::from(0.5).unwrap();
        let half_roll = roll * half;
        let sr = half_roll.sin();
        let cr = half_roll.cos();

        let half_pitch = pitch * half;
        let sp = half_pitch.sin();
        let cp = half_pitch.cos();

        let half_yaw = yaw * half;
        let sy = half_yaw.sin();
        let cy = half_yaw.cos();

        Self { 
            x: cy * sp * cr + sy * cp * sr,
            y: sy * cp * cr - cy * sp * sr,
            z: cy * cp * sr - sy * sp * cr,
            w: cy * cp * cr + sy * sp * sr, }
    }

    // Creates a Quaternion from the given rotation matrix.
    // #[inline(always)]
    // fn new_from_rotation_matrix(matrix: Matrix4x4<T>) -> Quaternion<T> {
    //     let trace = matrix[0][0] + matrix[1][1] + matrix[2][2];
    //     if trace > T::zero() {
    //         let s = (trace + T::one()).sqrt();
    //         let w = s * 0.5;
    //         let s = 0.5 / s;
    //         Self { x: (matrix[1][2] - matrix[2][1]) * s,
    //             y: (matrix[2][0] - matrix[0][2]) * s,
    //             z: (matrix[0][1] - matrix[1][0]) * s,
    //             w, }
    //     } else if matrix[0][0] >= matrix[1][1] && matrix[0][0] >= matrix[2][2] {
    //         let s = (1.0 + matrix[0][0] - matrix[1][1] - matrix[2][2]).sqrt();
    //         let inv_s = 0.5 / s;
    //         Quaternion::new(
    //             0.5 * s,
    //             (matrix[0][1] + matrix[1][0]) * inv_s,
    //             (matrix[0][2] + matrix[2][0]) * inv_s,
    //             (matrix[1][2] - matrix[2][1]) * inv_s,
    //         )
    //     } else if matrix[1][1] > matrix[2][2] {
    //         let s = (1.0 + matrix[1][1] - matrix[0][0] - matrix[2][2]).sqrt();
    //         let inv_s = 0.5 / s;
    //         Quaternion::new(
    //             (matrix[1][0] + matrix[0][1]) * inv_s,
    //             0.5 * s,
    //             (matrix[1][2] + matrix[2][1]) * inv_s,
    //             (matrix[2][0] - matrix[0][2]) * inv_s,
    //         )
    //     } else {
    //         let s = (1.0 + matrix[2][2] - matrix[0][0] - matrix[1][1]).sqrt();
    //         let inv_s = 0.5 / s;
    //         Quaternion::new(
    //             (matrix[2][0] + matrix[0][2]) * inv_s,
    //             (matrix[2][1] + matrix[1][2]) * inv_s,
    //             0.5 * s,
    //             (matrix[0][1] - matrix[1][0]) * inv_s,
    //         )
    //     }
    // }

    /// Interpolates between two quaternions, using spherical linear interpolation.
    #[inline(always)]
    pub fn slerp(q1: Self, q2: Self, t: T) -> Self {
        let mut cos_omega = q1.x * q2.x + q1.y * q2.y + q1.z * q2.z + q1.w * q2.w;
        let mut flip = false;
    
        if cos_omega < T::zero() {
            flip = true;
            cos_omega = -cos_omega;
        }
    
        let s1; let s2;
        if cos_omega > (T::one() - constants::tolerance()) {
            // Too close, do straight linear interpolation.
            s1 = T::one() - t;
            s2 = if flip { -t } else { t } 
        } else {
            let omega = cos_omega.acos();
            let inv_sin_omega = T::one() / omega.sin();
            s1 = ((T::one() - t) * omega).sin() * inv_sin_omega;
            s2 = if flip { -(t * omega).sin() * inv_sin_omega } else { (t * omega).sin() * inv_sin_omega };
        };

        q1 * s1 + q2 * s2
    }
    
    ///  Linearly interpolates between two quaternions.
    #[inline(always)]
    pub fn lerp(q1: Self, q2: Self, t: T) -> Self {
        if q1.dot(q2) >= T::zero() {
            (q1 * (T::one() - t) + q2 * t).normalize()
        } else {
            (q1 * (T::one() - t) - q2 * t).normalize()
        }
    }

    /// Concatenates two Quaternions; the result represents the value1 rotation followed by the value2 rotation.
    #[inline(always)]
    pub fn concatenate(value1: Self, value2: Self) -> Self {
        let q1x = value2.x;
        let q1y = value2.y;
        let q1z = value2.z;
        let q1w = value2.w;
    
        let q2x = value1.x;
        let q2y = value1.y;
        let q2z = value1.z;
        let q2w = value1.w;
    
        let cx = q1y * q2z - q1z * q2y;
        let cy = q1z * q2x - q1x * q2z;
        let cz = q1x * q2y - q1y * q2x;
    
        let dot = q1x * q2x + q1y * q2y + q1z * q2z;
    
        Self { x: q1x * q2w + q2x * q1w + cx,
            y: q1y * q2w + q2y * q1w + cy,
            z: q1z * q2w + q2z * q1w + cz,
            w: q1w * q2w - dot, }
    }
    
    /// Returns Euler123 angles (rotate around, X, then Y, then Z).
    #[inline(always)]
    pub fn to_euler_angles(&self) -> Vector3<T> {
        let two = T::from(2).unwrap();
        let x = T::atan2(-two * (self.y * self.z - self.w * self.x), self.w * self.w - self.x * self.x - self.y * self.y + self.z * self.z);
        let y = T::asin(two * (self.x * self.z + self.w * self.y));
        let z = T::atan2(-two * (self.x * self.y - self.w * self.z), self.w * self.w + self.x * self.x - self.y * self.y - self.z * self.z);
        Vector3::<T> { x, y, z }
    }

    pub fn to_spherical_angle(&self) -> HorizontalCoordinate<T> {
        self.to_spherical_angle_with_forward(Vector3::<T>::unit_y())
    }

    pub fn to_spherical_angle_with_forward(&self, forward_vector: Vector3<T>) -> HorizontalCoordinate<T> {
        let new_forward = forward_vector.transform_quaternion(*self);
        let forward_xy = Vector3::new(new_forward.x, new_forward.y, T::zero()).normalize();
        let angle = forward_xy.y.acos();
        let azimuth = if forward_xy.x < T::zero() { angle } else { -angle };
        let inclination = -new_forward.z.acos() + T::from(std::f32::consts::FRAC_PI_2).unwrap();
        HorizontalCoordinate::<T> { azimuth, inclination }
    }

    pub fn new_from_horizontal_coordinate(angle: HorizontalCoordinate<T>) -> Self {
        Self::new_z_rotation(angle.azimuth) * Self::new_x_rotation(angle.inclination)
    }
     /// Returns a rotation matrix. 
    pub fn to_matrix(&self) -> Matrix4x4<T> { Matrix4x4::create_rotation(*self) }

}

impl<T: Float> Neg for Quaternion<T> {
    type Output = Self;
    /// Flips the sign of each component of the quaternion.
    fn neg(self) -> Self::Output {
        Self { x: -self.x, y: -self.y, z: -self.z, w: -self.w }
    }
}

impl<T: Float> Add for Quaternion<T> {
    type Output = Self;
    /// Adds two Quaternions element-by-element.
    fn add(self, rhs: Self) -> Self::Output {
        Self { x: self.x + rhs.x, y: self.y + rhs.y, z: self.z + rhs.z, w: self.w + rhs.w }
    }
}

impl<T: Float> Sub for Quaternion<T> {
    type Output = Self;
    /// Subtracts one Quaternion from another.
    fn sub(self, rhs: Self) -> Self::Output {
        Self { x: self.x - rhs.x, y: self.y - rhs.y, z: self.z - rhs.z, w: self.w - rhs.w }
    }
}

impl<T: Float> Mul for Quaternion<T> {
    type Output = Self;
    /// Multiplies two Quaternions together.
    fn mul(self, rhs: Self) -> Self::Output {
        let tmp_00 = (self.z - self.y) * (rhs.y - rhs.z);
        let tmp_01 = (self.w + self.x) * (rhs.w + rhs.x);
        let tmp_02 = (self.w - self.x) * (rhs.y + rhs.z);
        let tmp_03 = (self.y + self.z) * (rhs.w - rhs.x);
        let tmp_04 = (self.z - self.x) * (rhs.x - rhs.y);
        let tmp_05 = (self.z + self.x) * (rhs.x + rhs.y);
        let tmp_06 = (self.w + self.y) * (rhs.w - rhs.z);
        let tmp_07 = (self.w - self.y) * (rhs.w + rhs.z);
        let tmp_08 = tmp_05 + tmp_06 + tmp_07;
        let tmp_09 = (tmp_04 + tmp_08) * T::from(0.5).unwrap();

        Self { x: tmp_01 + tmp_09 - tmp_08,
            y: tmp_02 + tmp_09 - tmp_07,
            z: tmp_03 + tmp_09 - tmp_06,
            w: tmp_00 + tmp_09 - tmp_05,
        }
    }
}

impl<T: Float> Mul<T> for Quaternion<T> {
    type Output = Self;
    /// Multiplies a Quaternion by a scalar value.
    fn mul(self, rhs: T) -> Self::Output {
        Self { x: self.x * rhs, y: self.y * rhs, z: self.z * rhs, w: self.w * rhs}
    }
}

impl<T: Float + FloatConst> Div for Quaternion<T> {
    type Output = Self;
    /// Divides a Quaternion by another Quaternion.
    fn div(self, rhs: Self) -> Self::Output {
        self * rhs.inverse()
    }
}

impl<T: Float + FloatConst + PartialOrd> From<HorizontalCoordinate<T>> for Quaternion<T> {
    fn from(angle: HorizontalCoordinate<T>) -> Self { Self::new_from_horizontal_coordinate(angle) }
}

impl<T: Float + FloatConst + PartialOrd> From<Quaternion<T>> for HorizontalCoordinate<T> {
    fn from(q: Quaternion<T>) -> Self {  q.to_spherical_angle()  }
}


impl<T: Float + FloatConst + PartialOrd> Plane<T> {
    #[inline(always)]
    pub fn new_from_coordinates(x: T, y: T, z: T, d: T) -> Self {
        Self { normal: Vector3 { x, y, z }, d }
    }

    #[inline(always)]
    pub fn new_from_vector(vector: Vector4<T>) -> Self {
        Self::new_from_coordinates(vector.x, vector.y, vector.z, vector.w)
    }

    /// Creates a Plane that contains the three given points.
    #[inline(always)]
    pub fn new_from_vertices(point1: Vector3<T>, point2: Vector3<T>, point3: Vector3<T>) -> Self {
        let a = point2 - point1;
        let b = point3 - point1;
        let n = a.cross(b);
        let d = -n.normalize().dot(point1);
        Self { normal: n.normalize(), d }
    }

    /// Creates a Plane with the given normal that contains the point
    #[inline(always)]
    pub fn new_from_normal_and_point(normal: Vector3<T>, point: Vector3<T>) -> Self {
        let n = normal.normalize();
        let d = -n.dot(point);
        Self { normal: n, d }
    }

    /// Creates a new Plane whose normal vector is the source Plane's normal vector normalized.
    #[inline]
    pub fn normalize(&self) -> Self {
        let length_squared = self.normal.length_squared();
        if (length_squared - T::one()).abs() < constants::tolerance() { return *self; }
        let length = length_squared.sqrt();
        Self { normal: self.normal / length, d: self.d / length }
    }

    ///  Transforms a normalized Plane by a Quaternion rotation.
    #[inline]
    pub fn transform_quaternion(&self, rotation: Quaternion<T>) -> Self {
        let x2 = rotation.x + rotation.x;
        let y2 = rotation.y + rotation.y;
        let z2 = rotation.z + rotation.z;

        let wx2 = rotation.w * x2;
        let wy2 = rotation.w * y2;
        let wz2 = rotation.w * z2;
        let xx2 = rotation.x * x2;
        let xy2 = rotation.x * y2;
        let xz2 = rotation.x * z2;
        let yy2 = rotation.y * y2;
        let yz2 = rotation.y * z2;
        let zz2 = rotation.z * z2;

        let m11 = T::one() - yy2 - zz2;
        let m21 = xy2 - wz2;
        let m31 = xz2 + wy2;

        let m12 = xy2 + wz2;
        let m22 = T::one() - xx2 - zz2;
        let m32 = yz2 - wx2;

        let m13 = xz2 - wy2;
        let m23 = yz2 + wx2;
        let m33 = T::one() - xx2 - yy2;

        let x = self.normal.x;
        let y = self.normal.y;
        let z = self.normal.z;

        Plane::new_from_coordinates(
            x * m11 + y * m21 + z * m31,
            x * m12 + y * m22 + z * m32,
            x * m13 + y * m23 + z * m33,
            self.d,
        )
    }

    /// Projects a point onto the plane
    #[inline]
    pub fn project_point_onto_plane(&self, point: Vector3<T>) -> Vector3<T> {
        let dist = point.dot(self.normal) - self.d;
        point - self.normal * dist
    }

    /// Calculates the dot product of a Plane and Vector4.
    #[inline]
    pub fn dot_vector(&self, value: Vector4<T>) -> T { self.to_vector4().dot(value) }

    /// Returns the dot product of a specified Vector3 and the normal vector of this Plane plus the distance (D) value of the Plane.
    #[inline]
    pub fn dot_coordinate(&self, value: Vector3<T>) -> T { self.normal.dot(value) + self.d }

    /// Returns the dot product of a specified Vector3 and the Normal vector of this Plane.
    #[inline]
    pub fn dot_normal(&self, value: Vector3<T>) -> T { self.normal.dot(value) }

    /// Returns a value less than zero if the points is below the plane, above zero if above the plane, or zero if coplanar
    #[inline]
    pub fn classify_point(&self, point: Vector3<T>) -> T { point.dot(self.normal) + self.d }

    /// Returns a Vector4 representation of the Plane
    #[inline]
    pub fn to_vector4(&self) -> Vector4<T> { Vector4::<T> {x: self.normal.x, y: self.normal.y, z: self.normal.z, w: self.d } }

}

impl<T: Float + FloatConst> Transformable3D<T> for Plane<T> {
    type Output = Self;

    /// Transforms a vector by the given matrix.
    #[inline(always)]
    fn transform(&self, matrix: Matrix4x4<T>) -> Self::Output {
        let m = matrix.inverse();
        let x = self.normal.x;
        let y = self.normal.y;
        let z = self.normal.z;
        let w = self.d;
        let normal = Vector3::new(
            x * m.m11 + y * m.m12 + z * m.m13 + w * m.m14,
            x * m.m21 + y * m.m22 + z * m.m23 + w * m.m24,
            x * m.m31 + y * m.m32 + z * m.m33 + w * m.m34,
        );
        let d = x * m.m41 + y * m.m42 + z * m.m43 + w * m.m44;
        return Self::new(normal, d);
    }
}

impl<T: Float + FloatConst> Transformable3D<T> for Quad<T> {
    type Output = Self;

    fn transform(&self, mat: Matrix4x4<T>) -> Self::Output {
        self.map(|x| x.transform(mat))
    }
}

impl<T: Float> Mappable<Vector3<T>> for Quad<T> {
    type Container = Self;

    fn map<F>(self, f: F) -> Self::Container where F: Fn(Vector3<T>) -> Vector3<T> {
        Self { a: f(self.a), b: f(self.b), c: f(self.c), d: f(self.d) }
    }
}

impl<T: Float> Points<T> for Quad<T> {
    fn num_points(&self) -> usize { 4 }

    fn get_point(&self, n: usize) -> Vector3<T> {
        match n {
            0 => self.a,
            1 => self.b,
            2 => self.c,
            _ => self.d,
        }
    }
}

impl<T: Float + FloatConst + AddAssign<T> + PartialOrd> Sphere<T> {
    /// Test if a bounding box is fully inside, outside, or just intersecting the sphere.
    pub fn contains_box(&self, aabox: &AABox<T>) -> ContainmentType {
        let mut inside = true;
        for corner in aabox.corners() {
            if self.contains_point(&corner) ==  ContainmentType::Disjoint {
                inside = false;
                break;
            }
        }

        if inside {
            return ContainmentType::Contains;
        }

        let mut dmin = T::zero();

        if self.center.x < aabox.min.x {
            dmin += (self.center.x - aabox.min.x) * (self.center.x - aabox.min.x);
        } else if self.center.x > aabox.max.x {
            dmin += (self.center.x - aabox.max.x) * (self.center.x - aabox.max.x);
        }

        if self.center.y < aabox.min.y {
            dmin += (self.center.y - aabox.min.y) * (self.center.y - aabox.min.y);
        } else if self.center.y > aabox.max.y {
            dmin += (self.center.y - aabox.max.y) * (self.center.y - aabox.max.y);
        }

        if self.center.z < aabox.min.z {
            dmin += (self.center.z - aabox.min.z) * (self.center.z - aabox.min.z);
        } else if self.center.z > aabox.max.z {
            dmin += (self.center.z - aabox.max.z) * (self.center.z - aabox.max.z);
        }

        if dmin <= self.radius * self.radius {
            return ContainmentType::Intersects;
        }

        ContainmentType::Disjoint
    }
    /// Test if a sphere is fully inside, outside, or just intersecting the sphere.
    pub fn contains_sphere(&self, other: &Sphere<T>) -> ContainmentType {
        let sq_distance = other.center.distance_squared(self.center);
        if sq_distance > (other.radius + self.radius) * (other.radius + self.radius) {
            return ContainmentType::Disjoint;
        }
        if sq_distance <= (self.radius - other.radius) * (self.radius - other.radius) {
            return ContainmentType::Contains;
        }
        ContainmentType::Intersects
    }
    /// Test if a point is fully inside, outside, or just intersecting the sphere.
    pub fn contains_point(&self, point: &Vector3<T>) -> ContainmentType {
        let sq_radius = self.radius * self.radius;
        let sq_distance = point.distance_squared(self.center);

        if sq_distance > sq_radius {
            return ContainmentType::Disjoint;
        } else if sq_distance < sq_radius {
            return ContainmentType::Contains;
        } else {
            return ContainmentType::Intersects;
        }
    }

    /// Creates the smallest sphere that contains the box. 
    pub fn new_from_box(box_: &AABox<T>) -> Self {
        let center = box_.center();
        let radius = center.distance(box_.max);
        Self { center, radius }
    }

    pub fn new_from_points(points: &[Vector3<T>]) -> Self {
        let (mut minx, mut maxx, mut miny, mut maxy, mut minz, mut maxz) = (
            Vector3::<T>::new(T::max_value(), T::max_value(), T::max_value()),
            Vector3::<T>::new(T::min_value(), T::min_value(), T::min_value()),
            Vector3::<T>::new(T::max_value(), T::max_value(), T::max_value()),
            Vector3::<T>::new(T::min_value(), T::min_value(), T::min_value()),
            Vector3::<T>::new(T::max_value(), T::max_value(), T::max_value()),
            Vector3::<T>::new(T::min_value(), T::min_value(), T::min_value()),
        );
        let half = T::from(0.5).unwrap();
        for pt in points.iter() {
            if pt.x < minx.x {
                minx = *pt;
            }
            if pt.x > maxx.x {
                maxx = *pt;
            }
            if pt.y < miny.y {
                miny = *pt;
            }
            if pt.y > maxy.y {
                maxy = *pt;
            }
            if pt.z < minz.z {
                minz = *pt;
            }
            if pt.z > maxz.z {
                maxz = *pt;
            }
        }

        let sq_dist_x = maxx.distance_squared(minx);
        let sq_dist_y = maxy.distance_squared(miny);
        let sq_dist_z = maxz.distance_squared(minz);

        let (min, max) = if sq_dist_y > sq_dist_x && sq_dist_y > sq_dist_z {
            (miny, maxy)
        } else if sq_dist_z > sq_dist_x && sq_dist_z > sq_dist_y {
            (minz, maxz)
        } else {
            (minx, maxx)
        };

        let mut center = (min + max) * half;
        let mut radius = max.distance(center);

        let mut sq_radius = radius * radius;
        for pt in points.iter() {
            let diff = *pt - center;
            let sq_dist = diff.length_squared();
            if sq_dist > sq_radius {
                let distance = sq_dist.sqrt();
                let direction = diff / distance;
                let g = center - direction * radius;
                center = (g + *pt) * half;
                radius = pt.distance(center);
                sq_radius = radius * radius;
            }
        }

        Self { center, radius }
    }

    /// Creates a sphere merging it with another 
    pub fn merge(&self, additional: &Self) -> Self {
        let ocenter_to_acenter = additional.center - self.center;
        let distance = ocenter_to_acenter.length();
        if distance <= self.radius + additional.radius {
            if distance <= self.radius - additional.radius { return *self; }
            if distance <= additional.radius - self.radius { return *additional; }
        }
        let two = T::from(2).unwrap();
        let left_radius = <T as Float>::max(self.radius - distance, additional.radius);
        let right_radius = <T as Float>::max(self.radius + distance, additional.radius);
        let ocenter_to_acenter = ocenter_to_acenter + (ocenter_to_acenter * ((left_radius - right_radius) / (two * ocenter_to_acenter.length())));
        Self { center: self.center + ocenter_to_acenter, 
            radius: (left_radius + right_radius) / two }
    }

    /// Gets whether or not a specified <see cref="AABox"/> intersects with this sphere.
    pub fn intersects_box(&self, aabox: &AABox<T>) -> bool { aabox.intersects_sphere(&self) }

    /// Gets whether or not the other <see cref="Sphere"/> intersects with this sphere.
    pub fn intersects_sphere(&self, sphere: &Sphere<T>) -> bool {
        let sq_distance = sphere.center.distance_squared(self.center);
        !(sq_distance > (sphere.radius + self.radius) * (sphere.radius + self.radius))
    }
    
    pub fn intersects_plane(&self, plane: &Plane<T>) -> PlaneIntersectionType {
        let distance = Vector3::dot(&plane.normal, self.center);
        let distance = distance + plane.d;
        if distance > self.radius {
            PlaneIntersectionType::Front
        } else if distance < -self.radius {
            PlaneIntersectionType::Back
        } else {
            PlaneIntersectionType::Intersecting
        }
    }
    /// Gets whether or not a specified <see cref="Ray"/> intersects with this sphere.
    pub fn intersects_ray(&self, ray: &Ray<T>) -> Option<T> {
        ray.intersects_sphere(self)
    }
    pub fn translate(&self, offset: &Vector3<T>) -> Self {
        Self { center: self.center + *offset, radius: self.radius }
    }

    pub fn distance_point(&self, point: Vector3<T>) -> T {
        <T as Float>::max(self.center.distance(point) - self.radius, T::zero())
    }

    pub fn distance_sphere(&self, other: &Self) -> T {
        <T as Float>::max(self.center.distance(other.center) - self.radius - other.radius, T::zero())
    }

}

impl<T: Float + FloatConst> Transformable3D<T> for Sphere<T> {
    type Output = Self;

    fn transform(&self, mat: Matrix4x4<T>) -> Self::Output {
        Self {
            center: self.center.transform(mat),
            radius: self.radius * <T as Float>::max(<T as Float>::max(
                mat.m11 * mat.m11 + mat.m12 * mat.m12 + mat.m13 * mat.m13, 
                mat.m21 * mat.m21 + mat.m22 * mat.m22 + mat.m23 * mat.m23), 
                mat.m31 * mat.m31 + mat.m32 * mat.m32 + mat.m33 * mat.m33).sqrt(),
        }
    }
}

// Assuming necessary struct and function definitions for AABox, Plane, Sphere, Triangle, Ray, Vector3, and Matrix4x4

impl<T: Float + FloatConst + PartialOrd> Ray<T> {
    #[inline(always)]
    pub fn intersects_box(&self, aabox: &AABox<T>) -> Option<T> {
        let mut t_min = None;
        let mut t_max = None;
        let epsilon = constants::tolerance();

        if self.direction.x.abs() < epsilon {
            if self.position.x < aabox.min.x || self.position.x > aabox.max.x { return None; }
        } else {
            t_min = Some((aabox.min.x - self.position.x) / self.direction.x);
            t_max = Some((aabox.max.x - self.position.x) / self.direction.x);
            if t_min.unwrap() > t_max.unwrap() {
                std::mem::swap(&mut t_min, &mut t_max);
            }
        }
        if self.direction.y.abs() < epsilon {
            if self.position.y < aabox.min.y || self.position.y > aabox.max.y {
                return None;
            }
        } else {
            let mut t_min_y = (aabox.min.y - self.position.y) / self.direction.y;
            let mut t_max_y = (aabox.max.y - self.position.y) / self.direction.y;
            if t_min_y > t_max_y {
                std::mem::swap(&mut t_min_y, &mut t_max_y);
            }
            if t_min.is_some() && t_min.unwrap() > t_max_y || t_max.is_some() && t_min_y > t_max.unwrap() {
                return None;
            }
            if t_min.is_none() || t_min_y > t_min.unwrap() {
                t_min = Some(t_min_y);
            }
            if t_max.is_none() || t_max_y < t_max.unwrap() {
                t_max = Some(t_max_y);
            }
        }
        if self.direction.z.abs() < epsilon {
            if self.position.z < aabox.min.z || self.position.z > aabox.max.z { return None; }
        } else {
            let mut t_min_z = (aabox.min.z - self.position.z) / self.direction.z;
            let mut t_max_z = (aabox.max.z - self.position.z) / self.direction.z;
            if t_min_z > t_max_z {
                std::mem::swap(&mut t_min_z, &mut t_max_z);
            }
            if t_min.is_some() && t_min.unwrap() > t_max_z || t_max.is_some() && t_min_z > t_max.unwrap() {
                return None;
            }
            if t_min.is_none() || t_min_z > t_min.unwrap() {
                t_min = Some(t_min_z);
            }
            if t_max.is_none() || t_max_z < t_max.unwrap() {
                t_max = Some(t_max_z);
            }
        }
        if t_min.is_some() && t_min.unwrap() < T::zero() && t_max.unwrap() > T::zero() {
            return Some(T::zero());
        }
        if t_min.unwrap() < T::zero() { return None; }
        t_min
    }

    #[inline(always)]
    pub fn intersects_plane(&self, plane: &Plane<T>, tolerance: T) -> Option<T> {
        let den = self.direction.dot(plane.normal);
        if den.abs() < tolerance { return None; }
        let result = (-plane.d - self.position.dot(plane.normal)) / den;
        if result < T::zero() {
            if result < -tolerance { return None; }
            return Some(T::zero());
        }
        Some(result)
    }

    #[inline(always)]
    pub fn intersects_sphere(&self, sphere: &Sphere<T>) -> Option<T> {
        let difference = sphere.center - self.position;
        let difference_length_squared = difference.length_squared();
        let sphere_radius_squared = sphere.radius * sphere.radius;

        if difference_length_squared < sphere_radius_squared { return Some(T::zero()); }
        let distance_along_ray = self.direction.dot(difference);
        if distance_along_ray < T::zero() { return None; }

        let dist = sphere_radius_squared + distance_along_ray.powi(2) - difference_length_squared;
        if dist < T::zero() { None } 
        else { Some(distance_along_ray - dist.sqrt()) }
    }
    // Adapted from https://en.wikipedia.org/wiki/M%C3%B6ller%E2%80%93Trumbore_intersection_algorithm
    // Does not require or benefit from precomputed normals.
    pub fn intersects_triangle(&self, tri: &Triangle<T>, tolerance: Option<T>) -> Option<T> {
        let tolerance = tolerance.unwrap_or(constants::tolerance());
        let edge1 = tri.b - tri.a;
        let edge2 = tri.c - tri.a;

        let h = self.direction.cross(edge2);
        let a = edge1.dot(h);
        if a > -tolerance && a < tolerance { return None; }

        let f = T::one() / a;
        let s = self.position - tri.a;
        let u = f * s.dot(h);
        if u < T::zero() || u > T::one() { return None; }

        let q = s.cross(edge1);
        let v = f * self.direction.dot(q);
        if v < T::zero() || u + v > T::one() { return None; }

        let t = f * edge2.dot(q);
        if t > tolerance { Some(t) } else { None }
    }    

}

impl<T: Float + FloatConst> Transformable3D<T> for Ray<T> {
    type Output = Self;

    fn transform(&self, mat: Matrix4x4<T>) -> Self::Output {
        Self {
            position: self.position.transform(mat),
            direction: self.direction.transform_normal(mat),
        }
    }
}

impl<T: Float + FloatConst + AddAssign + PartialOrd> Triangle<T> {
    pub fn length_a(&self) -> T { self.a.distance(self.b) }
    pub fn length_b(&self) -> T { self.b.distance(self.c) }
    pub fn length_c(&self) -> T { self.c.distance(self.a)}

    pub fn has_area(&self) -> bool { self.a != self.b && self.b != self.c && self.c != self.a }
    pub fn area(&self) -> T { (self.b - self.a).cross(self.c - self.a).length() * T::from(0.5f32).unwrap() }
    pub fn perimeter(&self) -> T { self.length_a() + self.length_b() + self.length_c() }
    pub fn mid_point(&self) -> Vector3<T> { (self.a + self.b + self.c) / T::from(3).unwrap() }
    pub fn normal_direction(&self) -> Vector3<T> { (self.b - self.a).cross(self.c - self.a) }
    pub fn normal(&self) -> Vector3<T> { self.normal_direction().normalize() }
    pub fn safe_normal(&self) -> Vector3<T> { self.normal_direction().safe_normalize() }

    pub fn bounding_box(&self) -> AABox<T> { AABox::<T>::new_from_points(&[self.a, self.b, self.c]) }
    pub fn bounding_sphere(&self) -> Sphere<T> { Sphere::<T>::new_from_points(&[self.a, self.b, self.c]) }

    #[inline(always)]
    pub fn is_sliver(&self, tolerance: Option<T>) -> bool {
        let tolerance = tolerance.unwrap_or(constants::tolerance());
        self.length_a() <= tolerance || self.length_b() <= tolerance || self.length_c() <= tolerance
    }
    pub fn side(&self, n: usize) -> Line<T> {
        match n {
            0 => Line::<T>::new(self.a, self.b),
            1 => Line::<T>::new(self.b, self.c),
            2 => Line::<T>::new(self.c, self.a),
            _ => panic!("Index out of range!"),
        }
    }
    pub fn binormal(&self) -> Vector3<T> { (self.b - self.a).safe_normalize() }
    pub fn tangent(&self) -> Vector3<T> { (self.c - self.a).safe_normalize() }

    pub fn ab(&self) -> Line<T> { Line::<T> { a: self.a, b: self.b } }
    pub fn bc(&self) -> Line<T> { Line::<T>{ a:self.b, b: self.c} }
    pub fn ca(&self) -> Line<T> { Line::<T>{ a: self.c, b: self.a} }
    pub fn ba(&self) -> Line<T> { self.ab().inverse() }
    pub fn cb(&self) -> Line<T> { self.bc().inverse() }
    pub fn ac(&self) -> Line<T> { self.ca().inverse() }
}

impl<T: Float + FloatConst> Transformable3D<T> for Triangle<T> {
    type Output = Self;

    fn transform(&self, mat: Matrix4x4<T>) -> Self::Output {
        self.map(|x| x.transform(mat))
    }
}

impl<T: Float> Mappable<Vector3<T>> for Triangle<T> {
    type Container = Self;

    fn map<F>(self, f: F) -> Self::Container where F: Fn(Vector3<T>) -> Vector3<T> {
        Self { a: f(self.a), b: f(self.b), c: f(self.c) }
    }
}

impl<T: Float> Points<T> for Triangle<T> {
    fn num_points(&self) -> usize { 3 }

    fn get_point(&self, n: usize) -> Vector3<T> {
        match n {
            0 => self.a,
            1 => self.b,
            _ => self.c,
        }
    }
}

impl<T: Float> Triangle2D<T> {
    // Compute the signed area of a triangle.
    pub fn area(&self) -> T {
        let half = T::from(0.5).unwrap();
        half * (self.a.x * (self.c.y - self.b.y) + self.b.x * (self.a.y - self.c.y) + self.c.x * (self.b.y - self.a.y))
    }

    // Test if a given point p2 is on the left side of the line formed by p0-p1.
    pub fn is_on_left_side_of_line(p0: Vector2<T>, p1: Vector2<T>, p2: Vector2<T>) -> bool {
        Triangle2D::new(p0, p2, p1).area() > T::zero()
    }

    // Test if a given point is inside a given triangle in R2.
    pub fn contains(&self, point: Vector2<T>) -> bool {
        // Point in triangle test using barycentric coordinates
        let v0 = self.b - self.a;
        let v1 = self.c - self.a;
        let v2 = point - self.a;

        let dot00 = v0.dot(v0);
        let dot01 = v0.dot(v1);
        let dot02 = v0.dot(v2);
        let dot11 = v1.dot(v1);
        let dot12 = v1.dot(v2);

        let inv_denom = T::one() / (dot00 * dot11 - dot01 * dot01);
        let dot11 = (dot11 * dot02 - dot01 * dot12) * inv_denom;
        let dot00 = (dot00 * dot12 - dot01 * dot02) * inv_denom;

        dot11 > T::zero() && dot00 > T::zero() && dot11 + dot00 < T::one()
    }
}

impl<T: Float> Points2D<T> for Triangle2D<T> {
    fn num_points(&self) -> usize { 3 }

    fn get_point(&self, n: usize) -> Vector2<T> {
        match n {
            0 => self.a,
            1 => self.b,
            _ => self.c,
        }
    }
}



impl<T: Float + FloatConst + PartialOrd<T>> Transform<T> {
    pub fn to_matrix(&self) -> Matrix4x4<T> { Matrix4x4::create_trs(self.position, self.orientation, Vector3::one()) }
}


impl<T: Float + PartialOrd> ValueDomain<T> {
    pub fn new(lower: T, upper: T) -> Self { Self { lower, upper } }
    pub fn normalize(&self, value: T) -> T { math3d_ops::clamp(value, self.lower, self.upper) / self.upper }
}

impl<T: PartialEq + std::hash::Hash + Default + PartialOrd + Div<Output = T>> Stats<T> {
    pub fn new(count: usize, min: T, max: T, sum: T) -> Self { Self { count, min, max, sum } }

    pub fn default() -> Self {
        Self {
            count: 0,
            min: Default::default(),
            max: Default::default(),
            sum: Default::default(),
        }
    }

    pub fn normalize(&self, value: T) -> T
    where
        T: PartialOrd + Copy,
    {
        math3d_ops::min(math3d_ops::max(value, self.min), self.max) / self.max
    }
}

 

impl<T: PartialEq + Hash> std::hash::Hash for Stats<T> {
    fn hash<H: std::hash::Hasher>(&self, state: &mut H) {
        self.count.hash(state);
        self.min.hash(state);
        self.max.hash(state);
        self.sum.hash(state);
    }
}

impl<T: PartialEq> std::cmp::PartialOrd for Stats<T>
where
    T: PartialOrd,
{
    fn partial_cmp(&self, other: &Stats<T>) -> Option<std::cmp::Ordering> {
        self.sum.partial_cmp(&other.sum)
    }
}

// impl<T: PartialEq> std::cmp::Ord for Stats<T>
// where
//     T: std::cmp::PartialOrd,
// {
//     fn cmp(&self, other: &Stats<T>) -> std::cmp::Ordering {
//         self.sum.cmp(&other.sum)
//     }
// }

impl<T: std::fmt::Debug> std::fmt::Display for Stats<T> {
    fn fmt(&self, f: &mut std::fmt::Formatter) -> std::fmt::Result {
        write!(
            f,
            "Stats<{}>(Count = {}, Min = {:?}, Max = {:?}, Sum = {:?})",
            std::any::type_name::<T>(),
            self.count,
            self.min,
            self.max,
            self.sum
        )
    }
}

impl<T: Float + FloatConst + PartialOrd<T>> Matrix4x4<T> {
    pub fn col0(&self) -> Vector3<T> { Vector3::new(self.m11, self.m21, self.m31) }
    pub fn col1(&self) -> Vector3<T> { Vector3::new(self.m12, self.m22, self.m32) }
    pub fn col2(&self) -> Vector3<T> { Vector3::new(self.m13, self.m23, self.m33) }
    pub fn col3(&self) -> Vector3<T> { Vector3::new(self.m14, self.m24, self.m34) }

    pub fn row0(&self) -> Vector3<T> { Vector3::new(self.m11, self.m12, self.m13) }
    pub fn row1(&self) -> Vector3<T> { Vector3::new(self.m21, self.m22, self.m23) }
    pub fn row2(&self) -> Vector3<T> { Vector3::new(self.m31, self.m32, self.m33) }
    pub fn row3(&self) -> Vector3<T> { Vector3::new(self.m41, self.m42, self.m43) }

    pub fn get_row(&self, row: usize) -> Vector3<T> {
        match row {
            0 => self.row0(),
            1 => self.row1(),
            2 => self.row2(),
            _ => self.row3(),
        }
    }
    pub fn get_col(&self, col: usize) -> Vector3<T> {
        match col {
            0 => self.col0(),
            1 => self.col1(),
            2 => self.col2(),
            _ => self.col3(),
        }
    }

    pub fn identity() -> Self { 
        Self {
            m11: T::one(),
            m12: T::zero(),
            m13: T::zero(),
            m14: T::zero(),
            m21: T::zero(),
            m22: T::one(),
            m23: T::zero(),
            m24: T::zero(),
            m31: T::zero(),
            m32: T::zero(),
            m33: T::one(),
            m34: T::zero(),
            m41: T::zero(),
            m42: T::zero(),
            m43: T::zero(),
            m44: T::one(), 
        }
    }
    pub fn is_identity(&self) -> bool {
        self.m11 == T::one()
            && self.m12 == T::zero()
            && self.m13 == T::zero()
            && self.m14 == T::zero()
            && self.m21 == T::zero()
            && self.m22 == T::one()
            && self.m23 == T::zero()
            && self.m24 == T::zero()
            && self.m31 == T::zero()
            && self.m32 == T::zero()
            && self.m33 == T::one()
            && self.m34 == T::zero()
            && self.m41 == T::zero()
            && self.m42 == T::zero()
            && self.m43 == T::zero()
            && self.m44 == T::one()
    }
    pub fn translation(&self) -> Vector3<T> { Vector3::<T> { x: self.m41, y: self.m42, z: self.m43 } }

    #[inline]
    pub fn set_translation(&self, v: Vector3<T>) -> Self {
        Self::new_from_4th_rows_vector3(self.row0(), self.row1(), self.row2(), v)
    }

    #[inline]
    pub fn new_from_rows_vector3(row0: Vector3<T>, row1: Vector3<T>, row2: Vector3<T>) -> Self {
        Self::new_from_4th_rows_vector4(row0.to_vector4(), row1.to_vector4(), row2.to_vector4(), Vector4::new(T::zero(), T::zero(), T::zero(), T::one()))
    }

    #[inline]
    pub fn new_from_4th_rows_vector3(row0: Vector3<T>, row1: Vector3<T>, row2: Vector3<T>, row3: Vector3<T>) -> Self {
        Self::new_from_4th_rows_vector4(row0.to_vector4(), row1.to_vector4(), row2.to_vector4(), Vector4::new(row3.x, row3.y, row3.z, T::one()))
    }

    #[inline]
    pub fn new_from_rows_vector4(row0: Vector4<T>, row1: Vector4<T>, row2: Vector4<T>) -> Self {
        Self::new_from_4th_rows_vector3(row0.to_vector3(), row1.to_vector3(), row2.to_vector3(), Vector3::zero())
    }

    #[inline]
    pub fn new_from_4th_rows_vector4(row0: Vector4<T>, row1: Vector4<T>, row2: Vector4<T>, row3: Vector4<T>) -> Self {
        Self::new(row0.x, row0.y, row0.z, row0.w,
                row1.x, row1.y, row1.z, row1.w,
                row2.x, row2.y, row2.z, row2.w,
                row3.x, row3.y, row3.z, row3.w)
    }

    /// Creates a spherical billboard that rotates around a specified object position.
    pub fn create_billboard(object_position: Vector3<T>, camera_position: Vector3<T>, camera_up_vector: Vector3<T>, camera_forward_vector: Vector3<T>) -> Self {
        let epsilon = T::from(1e-4).unwrap();

        let mut z_axis = Vector3::new(
            object_position.x - camera_position.x,
            object_position.y - camera_position.y,
            object_position.z - camera_position.z,
        );

        let norm = z_axis.length_squared();
        z_axis = if norm < epsilon { -camera_forward_vector } else { z_axis * norm.sqrt().recip() };

        let x_axis = camera_up_vector.cross(z_axis).normalize();
        let y_axis = z_axis.cross(x_axis);

        Self::new(
            x_axis.x, x_axis.y, x_axis.z, T::zero(),
            y_axis.x, y_axis.y, y_axis.z, T::zero(),
            z_axis.x, z_axis.y, z_axis.z, T::zero(),
            object_position.x, object_position.y, object_position.z, T::one(),
        )
    }
    /// Creates a cylindrical billboard that rotates around a specified axis.
    pub fn create_constrained_billboard(object_position: Vector3<T>, camera_position: Vector3<T>, rotate_axis: Vector3<T>, camera_forward_vector: Vector3<T>, object_forward_vector: Vector3<T>) -> Self {
        let epsilon = T::from(1e-4).unwrap();
        let min_angle = T::one() - T::from(0.1 * (std::f32::consts::PI / 180.0)).unwrap(); // 0.1 degrees

        let mut face_dir = Vector3::new(
            object_position.x - camera_position.x,
            object_position.y - camera_position.y,
            object_position.z - camera_position.z,
        );

        let norm = face_dir.length_squared();
        if norm < epsilon {
            face_dir = -camera_forward_vector;
        } else {
            face_dir = face_dir * norm.sqrt().recip();
        }

        let y_axis = rotate_axis;
        let x_axis;
        let mut z_axis;

        let dot = rotate_axis.dot(face_dir);
        if dot.abs() > min_angle {
            z_axis = object_forward_vector;

            let dot = rotate_axis.dot(z_axis);
            if dot.abs() > min_angle {
                z_axis = if rotate_axis.z.abs() > min_angle { 
                    Vector3::new(T::one(), T::zero(), T::zero()) 
                } else { 
                    Vector3::new(T::zero(), T::zero(), -T::one()) 
                };
            }
            x_axis = rotate_axis.cross(z_axis).normalize();
            z_axis = x_axis.cross(rotate_axis).normalize();
        } else {
            x_axis = rotate_axis.cross(face_dir).normalize();
            z_axis = x_axis.cross(y_axis).normalize();
        }

        Self::new(
            x_axis.x, x_axis.y, x_axis.z, T::zero(),
            y_axis.x, y_axis.y, y_axis.z, T::zero(),
            z_axis.x, z_axis.y, z_axis.z, T::zero(),
            object_position.x, object_position.y, object_position.z, T::one(),
        )
    }
    /// Creates a translation matrix.
    pub fn create_translation(position: Vector3<T>) -> Self {
        Self {
            m11: T::one(),
            m12: T::zero(),
            m13: T::zero(),
            m14: T::zero(),
            m21: T::zero(),
            m22: T::one(),
            m23: T::zero(),
            m24: T::zero(),
            m31: T::zero(),
            m32: T::zero(),
            m33: T::one(),
            m34: T::zero(),
            m41: position.x,
            m42: position.y,
            m43: position.z,
            m44: T::one(),
        }
    }
    /// Creates a translation matrix.
    pub fn create_translation_xyz(x: T, y: T, z: T) -> Self {
        Self::create_translation(Vector3::<T> { x, y, z })
    }
    /// Creates a scaling matrix.
    pub fn create_scale_xyz(x_scale: T, y_scale: T, z_scale: T) -> Self {
        Self {
            m11: x_scale,
            m12: T::zero(),
            m13: T::zero(),
            m14: T::zero(),
            m21: T::zero(),
            m22: y_scale,
            m23: T::zero(),
            m24: T::zero(),
            m31: T::zero(),
            m32: T::zero(),
            m33: z_scale,
            m34: T::zero(),
            m41: T::zero(),
            m42: T::zero(),
            m43: T::zero(),
            m44: T::one(),
        }
    }
    /// Creates a scaling matrix with a center point.
    pub fn create_scale_xyz_center(x_scale: T, y_scale: T, z_scale: T, center_point: Vector3<T>) -> Self {
        let tx = center_point.x * (T::one() - x_scale);
        let ty = center_point.y * (T::one() - y_scale);
        let tz = center_point.z * (T::one() - z_scale);

        Self {
            m11: x_scale,
            m12: T::zero(),
            m13: T::zero(),
            m14: T::zero(),
            m21: T::zero(),
            m22: y_scale,
            m23: T::zero(),
            m24: T::zero(),
            m31: T::zero(),
            m32: T::zero(),
            m33: z_scale,
            m34: T::zero(),
            m41: tx,
            m42: ty,
            m43: tz,
            m44: T::one(),
        }
    }

    /// Creates a scaling matrix.
    pub fn create_scale(scales: Vector3<T>) -> Self {
        Self::create_scale_xyz(scales.x, scales.y, scales.z)
    }
    
    /// Creates a scaling matrix with a center point.
    pub fn create_scale_centered(scales: Vector3<T>, center_point: Vector3<T>) -> Self {
        Self::create_scale_xyz_center(scales.x, scales.y, scales.z, center_point)
    }
    
    /// Creates a uniform scaling matrix that scales equally on each axis.
    pub fn create_scale_uniform(scale: T) -> Self {
        Self::create_scale_xyz(scale, scale, scale)
    }
    
    /// Creates a uniform scaling matrix that scales equally on each axis with a center point.
    pub fn create_scale_uniform_centered(scale: T, center_point: Vector3<T>) -> Self {
        Self::create_scale_xyz_center(scale, scale, scale, center_point)
    }

    /// Creates a matrix for rotating points around the X-axis.
    pub fn create_rotation_x(radians: T) -> Self {
        let c = radians.cos();
        let s = radians.sin();
        
        // [  1  0  0  0 ]
        // [  0  c  s  0 ]
        // [  0 -s  c  0 ]
        // [  0  0  0  1 ]    
        Self {
            m11: T::one(),
            m12: T::zero(),
            m13: T::zero(),
            m14: T::zero(),
            m21: T::zero(),
            m22: c,
            m23: s,
            m24: T::zero(),
            m31: T::zero(),
            m32: -s,
            m33: c,
            m34: T::zero(),
            m41: T::zero(),
            m42: T::zero(),
            m43: T::zero(),
            m44: T::one(),
        }
    }
    
    /// Creates a matrix for rotating points around the X-axis, from a center point.
    pub fn create_rotation_x_centered(radians: T, center_point: Vector3<T>) -> Self {
        let c = radians.cos();
        let s = radians.sin();
        let y = center_point.y * (T::one() - c) + center_point.z * s;
        let z = center_point.z * (T::one() - c) - center_point.y * s;
    
        // [  1  0  0  0 ]
        // [  0  c  s  0 ]
        // [  0 -s  c  0 ]
        // [  0  y  z  1 ]
        Self {
            m11: T::one(),
            m12: T::zero(),
            m13: T::zero(),
            m14: T::zero(),
            m21: T::zero(),
            m22: c,
            m23: s,
            m24: T::zero(),
            m31: T::zero(),
            m32: -s,
            m33: c,
            m34: T::zero(),
            m41: T::zero(),
            m42: y,
            m43: z,
            m44: T::one(),
        }
    }
    
    /// Creates a matrix for rotating points around the Y-axis.
    pub fn create_rotation_y(radians: T) -> Self {
        let c = radians.cos();
        let s = radians.sin();
    
        Self {
            m11: c,
            m12: T::zero(),
            m13: -s,
            m14: T::zero(),
            m21: T::zero(),
            m22: T::one(),
            m23: T::zero(),
            m24: T::zero(),
            m31: s,
            m32: T::zero(),
            m33: c,
            m34: T::zero(),
            m41: T::zero(),
            m42: T::zero(),
            m43: T::zero(),
            m44: T::one(),
        }
    }

    /// Creates a matrix for rotating points around the Y-axis, from a center point.
    pub fn create_rotation_y_centered(radians: T, center_point: Vector3<T>) -> Self {
        let c = radians.cos();
        let s = radians.sin();
    
        let x = center_point.x * (T::one() - c) - center_point.z * s;
        let z = center_point.z * (T::one() - c) + center_point.x * s;
    
        // [  c  0 -s  0 ]
        // [  0  1  0  0 ]
        // [  s  0  c  0 ]
        // [  x  0  z  1 ]
        Self::new(
            c, T::zero(), -s, T::zero(),
            T::zero(), T::one(), T::zero(), T::zero(),
            s, T::zero(), c, T::zero(),
            x, T::zero(), z, T::one(),
        )
    }

    /// Creates a matrix for rotating points around the Z-axis.
    pub fn create_rotation_z(radians: T) -> Self {
        let c = radians.cos();
        let s = radians.sin();
    
        Self::new(
            c, s, T::zero(), T::zero(),
            -s, c, T::zero(), T::zero(),
            T::zero(), T::zero(), T::one(), T::zero(),
            T::zero(), T::zero(), T::zero(), T::one(),
        )
    }
    
    /// Creates a matrix for rotating points around the Z-axis.
    pub fn create_rotation_z_centered(radians: T, center_point: Vector3<T>) -> Self {
        let c = radians.cos();
        let s = radians.sin();
    
        let x = center_point.x * (T::one() - c) + center_point.y * s;
        let y = center_point.y * (T::one() - c) - center_point.x * s;
    
        Self::new(
            c, s, T::zero(), T::zero(),
            -s, c, T::zero(), T::zero(),
            T::zero(), T::zero(), T::one(), T::zero(),
            x, y, T::zero(), T::one(),
        )
    }

    /// Creates a matrix that rotates around an arbitrary vector.
    pub fn create_from_axis_angle(axis: Vector3<T>, angle: T) -> Self {
        let (x, y, z) = (axis.x, axis.y, axis.z);
        let (sa, ca) = (angle.sin(), angle.cos());
        let (xx, yy, zz) = (x * x, y * y, z * z);
        let (xy, xz, yz) = (x * y, x * z, y * z);

        Self::new(
            xx + ca * (T::one() - xx),
            xy - ca * xy + sa * z,
            xz - ca * xz - sa * y,
            T::zero(),
            xy - ca * xy - sa * z,
            yy + ca * (T::one() - yy),
            yz - ca * yz + sa * x,
            T::zero(),
            xz - ca * xz + sa * y,
            yz - ca * yz - sa * x,
            zz + ca * (T::one() - zz),
            T::zero(),
            T::zero(),
            T::zero(),
            T::zero(),
            T::one(),
        )
    }

    /// Creates a perspective projection matrix based on a field of view, aspect ratio, and near and far view plane distances. 
    pub fn create_perspective_field_of_view(field_of_view: T, aspect_ratio: T, near_plane_distance: T, far_plane_distance: T) -> Self {
        if field_of_view <= T::zero() || field_of_view >= T::PI() || near_plane_distance <= T::zero() || far_plane_distance <= T::zero() || near_plane_distance >= far_plane_distance {
            panic!("Invalid arguments provided for create_perspective.");
        }
        let half = T::from(0.5).unwrap();
        let y_scale = (field_of_view * half).tan().recip();
        let x_scale = y_scale / aspect_ratio;
        let neg_far_range = if far_plane_distance.is_infinite() { -T::one() } else { far_plane_distance / (near_plane_distance - far_plane_distance) };

        Self::new(
            x_scale,
            T::zero(),
            T::zero(),
            T::zero(),
            T::zero(),
            y_scale,
            T::zero(),
            T::zero(),
            T::zero(),
            T::zero(),
            neg_far_range,
            -T::one(),
            T::zero(),
            T::zero(),
            near_plane_distance * neg_far_range,
            T::zero(),
        )
    }

    /// Creates a perspective projection matrix from the given view volume dimensions.
    pub fn create_perspective(width: T, height: T, near_plane_distance: T, far_plane_distance: T) -> Self {
        if near_plane_distance <= T::zero() || far_plane_distance <= T::zero() || near_plane_distance >= far_plane_distance {
            panic!("Invalid arguments provided for create_perspective.");
        }
        let two = T::from(2).unwrap();
        let neg_far_range = if far_plane_distance.is_infinite() { -T::one() } else { far_plane_distance / (near_plane_distance - far_plane_distance) };

        Self::new(
            two * near_plane_distance / width,
            T::zero(),
            T::zero(),
            T::zero(),
            T::zero(),
            two * near_plane_distance / height,
            T::zero(),
            T::zero(),
            T::zero(),
            T::zero(),
            neg_far_range,
            -T::one(),
            T::zero(),
            T::zero(),
            near_plane_distance * neg_far_range,
            T::zero(),
        )
    }

    /// Creates a customized, perspective projection matrix.
    pub fn create_perspective_off_center(left: T, right: T, bottom: T, top: T, near_plane_distance: T, far_plane_distance: T) -> Self {
        if near_plane_distance <= T::zero() || far_plane_distance <= T::zero() || near_plane_distance >= far_plane_distance {
            panic!("Invalid arguments provided for create_perspective_off_center.");
        }
        let two = T::from(2).unwrap();
        let neg_far_range = if far_plane_distance.is_infinite() && far_plane_distance.is_sign_positive() { -T::one() } else { far_plane_distance / (near_plane_distance - far_plane_distance) };
    
        Self::new(
            two * near_plane_distance / (right - left),
            T::zero(),
            T::zero(),
            T::zero(),

            T::zero(),
            two * near_plane_distance / (top - bottom),
            T::zero(), 
            T::zero(),

            (left + right) / (right - left), 
            (top + bottom) / (top - bottom),
            neg_far_range,
            -T::one(),

            T::zero(),
            T::zero(),
            near_plane_distance * neg_far_range,
            T::zero(),
        )
    }
    
    /// Creates an orthographic perspective matrix from the given view volume dimensions.
    pub fn create_orthographic(width: T, height: T, z_near_plane: T, z_far_plane: T) -> Self {
        let two = T::from(2).unwrap();
        Self::new(
            two / width,
            T::zero(),
            T::zero(),
            T::zero(),
            T::zero(),
            two / height,
            T::zero(),
            T::zero(),
            T::zero(),
            T::zero(),
            T::one() / (z_near_plane - z_far_plane),
            T::zero(),
            T::zero(),
            T::zero(),
            z_near_plane / (z_near_plane - z_far_plane),
            T::one(),
        )
    }
    
    /// Builds a customized, orthographic projection matrix.
    pub fn create_orthographic_off_center(left: T, right: T, bottom: T, top: T, z_near_plane: T, z_far_plane: T) -> Self {
        let two = T::from(2).unwrap();
        Self::new(
            two / (right - left),
            T::zero(),
            T::zero(),
            T::zero(),
            T::zero(),
            two / (top - bottom),
            T::zero(),
            T::zero(),
            T::zero(),
            T::zero(),
            T::one() / (z_near_plane - z_far_plane),
            T::zero(),
            (left + right) / (left - right),
            (top + bottom) / (bottom - top),
            z_near_plane / (z_near_plane - z_far_plane),
            T::one(),
        )
    }

    /// Creates a view matrix.
    pub fn create_look_at(camera_position: Vector3<T>, camera_target: Vector3<T>, camera_up_vector: Vector3<T>) -> Self {
        let zaxis = (camera_position - camera_target).normalize();
        let xaxis = camera_up_vector.cross(zaxis).normalize();
        let yaxis = zaxis.cross(xaxis);

        Self::new(
            xaxis.x, 
            yaxis.x, 
            zaxis.x, 
            T::zero(),
            xaxis.y, 
            yaxis.y, 
            zaxis.y, 
            T::zero(),
            xaxis.z, 
            yaxis.z, 
            zaxis.z, 
            T::zero(),
            -xaxis.dot(camera_position), 
            -yaxis.dot(camera_position), 
            -zaxis.dot(camera_position), 
            T::one(),
        )
    }
    
    /// Creates a world matrix with the specified parameters.
    pub fn create_world(position: Vector3<T>, forward: Vector3<T>, up: Vector3<T>) -> Self {
        let zaxis = (-forward).normalize();
        let xaxis = up.cross(zaxis).normalize();
        let yaxis = zaxis.cross(xaxis);

        Self::new(
            xaxis.x, xaxis.y, xaxis.z, T::zero(),
            yaxis.x, yaxis.y, yaxis.z, T::zero(),
            zaxis.x, zaxis.y, zaxis.z, T::zero(),
            position.x, position.y, position.z, T::one(),
        )
    }
    
    /// Creates a rotation matrix from the given Quaternion rotation value.
    pub fn create_from_quaternion(quaternion: Quaternion<T>) -> Self {
        Self::create_rotation(quaternion)
    }
    
    /// Creates a rotation matrix from the given Quaternion rotation value.
    pub fn create_rotation(quaternion: Quaternion<T>) -> Self {
        let xx = quaternion.x * quaternion.x;
        let yy = quaternion.y * quaternion.y;
        let zz = quaternion.z * quaternion.z;
    
        let xy = quaternion.x * quaternion.y;
        let wz = quaternion.z * quaternion.w;
        let xz = quaternion.z * quaternion.x;
        let wy = quaternion.y * quaternion.w;
        let yz = quaternion.y * quaternion.z;
        let wx = quaternion.x * quaternion.w;
    
        let two = T::from(2).unwrap();
        Self::new(
            T::one() - two * (yy + zz), 
            two * (xy + wz), 
            two * (xz - wy), 
            T::zero(),
            two * (xy - wz), 
            T::one() - two * (zz + xx), 
            two * (yz + wx), 
            T::zero(),
            two * (xz + wy), 
            two * (yz - wx), 
            T::one() - two * (yy + xx), 
            T::zero(),
            T::zero(), 
            T::zero(), 
            T::zero(), 
            T::one(),
        )
    }
    
    /// Creates a rotation matrix from the specified yaw, pitch, and roll.
    pub fn create_from_yaw_pitch_roll(yaw: T, pitch: T, roll: T) -> Self {
        Self::create_rotation(Quaternion::<T>::new_from_yaw_pitch_roll(yaw, pitch, roll))
    }
    
    /// Creates a Matrix that flattens geometry into a specified Plane as if casting a shadow from a specified light source.
    pub fn create_shadow(light_direction: Vector3<T>, plane: Plane<T>) -> Self {
        let p = plane.normalize();
        let dot = p.normal.x * light_direction.x + 
            p.normal.y * light_direction.y + 
            p.normal.z * light_direction.z;
        let a = -p.normal.x;
        let b = -p.normal.y;
        let c = -p.normal.z;
        let d = -p.d;
    
        Self {
            m11: a * light_direction.x + dot,
            m21: b * light_direction.x,
            m31: c * light_direction.x,
            m41: d * light_direction.x,

            m12: a * light_direction.y,
            m22: b * light_direction.y + dot,
            m32: c * light_direction.y,
            m42: d * light_direction.y,

            m13: a * light_direction.z,
            m23: b * light_direction.z,
            m33: c * light_direction.z + dot,
            m43: d * light_direction.z,

            m14: T::zero(),
            m24: T::zero(),
            m34: T::zero(),
            m44: dot,
        }
    }
    
    /// Creates a Matrix that reflects the coordinate system about a specified Plane.
    pub fn create_reflection(value: Plane<T>) -> Self {
        let two = T::from(2.0f32).unwrap();
        let value = value.normalize();
    
        let a = value.normal.x;
        let b = value.normal.y;
        let c = value.normal.z;
    
        let fa = -two * a;
        let fb = -two * b;
        let fc = -two * c;
    
        Self {
            m11: fa * a + T::one(),
            m12: fb * a,
            m13: fc * a,
            m14: T::zero(),

            m21: fa * b,
            m22: fb * b + T::one(),
            m23: fc * b,
            m24: T::zero(),

            m31: fa * c,
            m32: fb * c,
            m33: fc * c + T::one(),
            m34: T::zero(),

            m41: fa * value.d,
            m42: fb * value.d,
            m43: fc * value.d,
            m44: T::one(),
        }
    }
    
    /// Calculates the determinant of the 3x3 rotational component of the matrix.
    pub fn get_3x3_rotation_determinant(&self) -> T {
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

        let a = self.m11;
        let b = self.m12;
        let c = self.m13;
        let d = self.m21;
        let e = self.m22;
        let f = self.m23;
        let g = self.m31;
        let h = self.m32;
        let i = self.m33;
    
        let ei_fh = e * i - f * h;
        let di_gf = d * i - g * f;
        let dh_eg = d * h - e * g;
    
        a * ei_fh - b * di_gf + c * dh_eg
    }
    
    /// Returns true if the 3x3 rotation determinant of the matrix is less than 0. This assumes the matrix represents
    /// an affine transform.
    pub fn is_reflection(&self) -> bool { self.get_3x3_rotation_determinant() < T::zero() }
    
     /// Calculates the determinant of the matrix.
    pub fn get_determinant(&self) -> T {
        let a = self.m11; let b = self.m12; let c = self.m13; let d = self.m14;
        let e = self.m21; let f = self.m22; let g = self.m23; let h = self.m24;
        let i = self.m31; let j = self.m32; let k = self.m33; let l = self.m34;
        let m = self.m41; let n = self.m42; let o = self.m43; let p = self.m44;
    
        let kp_lo = k * p - l * o;
        let jp_ln = j * p - l * n;
        let jo_kn = j * o - k * n;
        let ip_lm = i * p - l * m;
        let io_km = i * o - k * m;
        let in_jm = i * n - j * m;

        a * (f * kp_lo - g * jp_ln + h * jo_kn) -
        b * (e * kp_lo - g * ip_lm + h * io_km) +
        c * (e * jp_ln - f * ip_lm + h * in_jm) -
        d * (e * jo_kn - f * io_km + g * in_jm)
    }
    
    /// Attempts to calculate the inverse of the given matrix. If successful, result will contain the inverted matrix.
    pub fn invert(&self) -> Option<Self> {
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
        let a = self.m11;
        let b = self.m12;
        let c = self.m13;
        let d = self.m14;
        let e = self.m21;
        let f = self.m22;
        let g = self.m23;
        let h = self.m24;
        let i = self.m31;
        let j = self.m32;
        let k = self.m33;
        let l = self.m34;
        let m = self.m41;
        let n = self.m42;
        let o = self.m43;
        let p = self.m44;
    
        let kp_lo = k.mul(p) - l.mul(o);
        let jp_ln = j.mul(p) - l.mul(n);
        let jo_kn = j.mul(o) - k.mul(n);
        let ip_lm = i.mul(p) - l.mul(m);
        let io_km = i.mul(o) - k.mul(m);
        let in_jm = i.mul(n) - j.mul(m);
    
        let a11 = T::one() * (f.mul(kp_lo) - g.mul(jp_ln) + h.mul(jo_kn));
        let a12 = T::one().neg() * (e.mul(kp_lo) - g.mul(ip_lm) + h.mul(io_km));
        let a13 = T::one() * (e.mul(jp_ln) - f.mul(ip_lm) + h.mul(in_jm));
        let a14 = T::one().neg() * (e.mul(jo_kn) - f.mul(io_km) + g.mul(in_jm));
    
        let det = a.mul(a11) + b.mul(a12) + c.mul(a13) + d.mul(a14);

        if det.abs() < T::epsilon() { return None; }

        let inv_det = T::one() / det;

        let gp_ho = g.mul(p) - h.mul(o);
        let fp_hn = f.mul(p) - h.mul(n);
        let fo_gn = f.mul(o) - g.mul(n);
        let ep_hm = e.mul(p) - h.mul(m);
        let eo_gm = e.mul(o) - g.mul(m);
        let en_fm = e.mul(n) - f.mul(m);

        let gl_hk = g * l - h * k;
        let fl_hj = f * l - h * j;
        let fk_gj = f * k - g * j;
        let el_hi = e * l - h * i;
        let ek_gi = e * k - g * i;
        let ej_fi = e * j - f * i;
    
        Some(Self {
            m11: a11 * inv_det,
            m21: a12 * inv_det,
            m31: a13 * inv_det,
            m41: a14 * inv_det,

            m12: -(b * kp_lo - c * jp_ln + d * jo_kn) * inv_det,
            m22: (a * kp_lo - c * ip_lm + d * io_km) * inv_det,
            m32: -(a * jp_ln - b * ip_lm + d * in_jm) * inv_det,
            m42: (a * jo_kn - b * io_km + c * in_jm) * inv_det,
        
            m13: (b * gp_ho - c * fp_hn + d * fo_gn) * inv_det,
            m23: -(a * gp_ho - c * ep_hm + d * eo_gm) * inv_det,
            m33: (a * fp_hn - b * ep_hm + d * en_fm) * inv_det,
            m43: -(a * fo_gn - b * eo_gm + c * en_fm) * inv_det,

            m14: -(b * gl_hk - c * fl_hj + d * fk_gj) * inv_det,
            m24: (a * gl_hk - c * el_hi + d * ek_gi) * inv_det,
            m34: -(a * fl_hj - b * el_hi + d * ej_fi) * inv_det,
            m44: (a * fk_gj - b * ek_gi + c * ej_fi) * inv_det,
        })
    }

    /// Transforms the given matrix by applying the given Quaternion rotation.
    pub fn transform_quaternion(&self, rotation: Quaternion<T>) -> Self {
        let x2 = rotation.x + rotation.x;
        let y2 = rotation.y + rotation.y;
        let z2 = rotation.z + rotation.z;
    
        let wx2 = rotation.w * x2;
        let wy2 = rotation.w * y2;
        let wz2 = rotation.w * z2;
        let xx2 = rotation.x * x2;
        let xy2 = rotation.x * y2;
        let xz2 = rotation.x * z2;
        let yy2 = rotation.y * y2;
        let yz2 = rotation.y * z2;
        let zz2 = rotation.z * z2;
    
        let q11 = T::one() - yy2 - zz2;
        let q21 = xy2 - wz2;
        let q31 = xz2 + wy2;
    
        let q12 = xy2 + wz2;
        let q22 = T::one() - xx2 - zz2;
        let q32 = yz2 - wx2;
    
        let q13 = xz2 - wy2;
        let q23 = yz2 + wx2;
        let q33 = T::one() - xx2 - yy2;
    
        Self {
            m11: self.m11 * q11 + self.m12 * q21 + self.m13 * q31,
            m12: self.m11 * q12 + self.m12 * q22 + self.m13 * q32,
            m13: self.m11 * q13 + self.m12 * q23 + self.m13 * q33,
            m14: self.m14,
        
            m21: self.m21 * q11 + self.m22 * q21 + self.m23 * q31,
            m22: self.m21 * q12 + self.m22 * q22 + self.m23 * q32,
            m23: self.m21 * q13 + self.m22 * q23 + self.m23 * q33,
            m24: self.m24,
        
            m31: self.m31 * q11 + self.m32 * q21 + self.m33 * q31,
            m32: self.m31 * q12 + self.m32 * q22 + self.m33 * q32,
            m33: self.m31 * q13 + self.m32 * q23 + self.m33 * q33,
            m34: self.m34,
        
            m41: self.m41 * q11 + self.m42 * q21 + self.m43 * q31,
            m42: self.m41 * q12 + self.m42 * q22 + self.m43 * q32,
            m43: self.m41 * q13 + self.m42 * q23 + self.m43 * q33,
            m44: self.m44,
        }
    }

    /// Transposes the rows and columns of a matrix.
    pub fn transpose(&self) -> Self { 
        Self {
            m11: self.m11,
            m12: self.m21,
            m13: self.m31,
            m14: self.m41,
            m21: self.m12,
            m22: self.m22,
            m23: self.m32,
            m24: self.m42,
            m31: self.m13,
            m32: self.m23,
            m33: self.m33,
            m34: self.m43,
            m41: self.m14,
            m42: self.m24,
            m43: self.m34,
            m44: self.m44,
        }
    }

    /// Linearly interpolates between the corresponding values of two matrices.
    pub fn lerp(&self, matrix: Self, amount: T) -> Self {
        Self {
            // First row
            m11: self.m11 + (matrix.m11 - self.m11) * amount,
            m12: self.m12 + (matrix.m12 - self.m12) * amount,
            m13: self.m13 + (matrix.m13 - self.m13) * amount,
            m14: self.m14 + (matrix.m14 - self.m14) * amount,
            // Second row
            m21: self.m21 + (matrix.m21 - self.m21) * amount,
            m22: self.m22 + (matrix.m22 - self.m22) * amount,
            m23: self.m23 + (matrix.m23 - self.m23) * amount,
            m24: self.m24 + (matrix.m24 - self.m24) * amount,
            // Third row
            m31: self.m31 + (matrix.m31 - self.m31) * amount,
            m32: self.m32 + (matrix.m32 - self.m32) * amount,
            m33: self.m33 + (matrix.m33 - self.m33) * amount,
            m34: self.m34 + (matrix.m34 - self.m34) * amount,
            // Fourth row
            m41: self.m41 + (matrix.m41 - self.m41) * amount,
            m42: self.m42 + (matrix.m42 - self.m42) * amount,
            m43: self.m43 + (matrix.m43 - self.m43) * amount,
            m44: self.m44 + (matrix.m44 - self.m44) * amount,
        } 
    }

    pub fn create_trs(translation: Vector3<T>, rotation: Quaternion<T>, scale: Vector3<T>) -> Self {
        Self::create_translation(translation) * 
        Self::create_rotation(rotation) * 
        Self::create_scale(scale)
    }
    
    /// Get's the scale factor of each axis.  This implementation extracts the scale exclusively,
    /// so it attempts to ignore rotation.  This is contrary to most math libraries
    /// that use decompose, so a negation on Y becomes a 90 degree rotation and a negation on X.
    /// We have implemented this extraction to be able to quickly remove scaling from matrices.
    /// Multiplying a matrix by the inverse of it's direct scale will preserve it's current rotation.
    /// It's implemented this way mostly so we can get easy testing on unit tests, and because this
    /// implementation is equally valid.
    /// NOTE: This could probably be improved to handle more generic cases by using
    /// CrossProduct to determine axis flipping: (X Cross Y) Dot Z < 0 == Flip
    pub fn extract_direct_scale(&self) -> Vector3<T> {
        Vector3::<T>::new(
            self.row0().length() * if self.m11 > T::zero() { T::one() } else { -T::one() },
            self.row1().length() * if self.m22 > T::zero() { T::one() } else { -T::one() },
            self.row2().length() * if self.m33 > T::zero() { T::one() } else { -T::one() },
        )
    }
    pub fn scale_translation(&self, amount: T) -> Self { self.set_translation(self.translation() * amount) }

    /// Attempts to extract the scale, translation, and rotation components from the given scale/rotation/translation matrix.
    /// If successful, the out parameters will contained the extracted values.
    /// https://referencesource.microsoft.com/#System.Numerics/System/Numerics/Matrix4x4.cs
    pub fn decompose(&self) -> Option<(Vector3<T>, Quaternion<T>, Vector3<T>)> {
        let epsilon: T = T::from(0.0001).unwrap();
        let p_canonical_basis = [ Vector3::<T>::unit_x(), Vector3::<T>::unit_y(), Vector3::<T>::unit_z() ];
        let mut p_vector_basis = [ self.row0(), self.row1(), self.row2() ];
        let mut pf_scales = [ p_vector_basis[0].length(), p_vector_basis[1].length(), p_vector_basis[2].length() ];
        
        let x = pf_scales[0];
        let y = pf_scales[1];
        let z = pf_scales[2];

        let a; let b; let c;
        if x < y {
            if y < z { a = 2; b = 1; c = 0; }
            else {
                a = 1;
                if x < z { b = 2; c = 0; }
                else { b = 0; c = 2; }
            }
        }
        else {
            if x < z { a = 2; b = 0; c = 1; }
            else { a = 0;
                if y < z { b = 2; c = 1; }
                else { b = 1; c = 2; }
            }
        }
    
        if pf_scales[a] < epsilon {
            p_vector_basis[a] = p_canonical_basis[a];
        }
        p_vector_basis[a] = p_vector_basis[a].normalize();
    
        if pf_scales[b] < epsilon {
            let f_abs_x = p_vector_basis[a].x.abs();
            let f_abs_y = p_vector_basis[a].y.abs();
            let f_abs_z = p_vector_basis[a].z.abs();
            let cc;
            if f_abs_x < f_abs_y {
                if f_abs_y < f_abs_z { cc = 0; }
                else { if f_abs_x < f_abs_z { cc = 0; } else { cc = 2; } }
            }
            else {
                if f_abs_x < f_abs_z { cc = 1; } else {
                    if f_abs_y < f_abs_z { cc = 1; } else { cc = 2; }
                }
            };
            p_vector_basis[b] = p_vector_basis[a].cross(p_canonical_basis[cc]);
        }

        p_vector_basis[b] = p_vector_basis[b].normalize();
        if pf_scales[c] < epsilon {
            p_vector_basis[c] = p_vector_basis[a].cross(p_vector_basis[b]);
        }
        p_vector_basis[c] = p_vector_basis[c].normalize();
    
        let mut det = Self::new_from_rows_vector3(
            p_vector_basis[0],
            p_vector_basis[1],
            p_vector_basis[2],
        ).get_determinant();
    
        // use Kramer's rule to check for handedness of coordinate system
        if det < T::zero() {
            pf_scales[a] = -pf_scales[a];
            p_vector_basis[a] = -p_vector_basis[a];
            det = -det;
        }
    
        det = det - T::one();
        det = det * det;

        if epsilon < det { return None; }

        let rotation = Quaternion::new_from_rotation_matrix(Self::new_from_rows_vector3(
            p_vector_basis[0],
            p_vector_basis[1],
            p_vector_basis[2],
        ));
        let translation = self.translation();
        let scale = Vector3::new(pf_scales[0], pf_scales[1], pf_scales[2]);
        return Some((scale, rotation, translation))
    }
    pub fn transform(&self, matrix: Self) -> Matrix4x4<T> { *self * matrix }

    pub fn to_numbers(&self) -> [T;16] { [
        self.m11, self.m12, self.m13, self.m14,
        self.m21, self.m22, self.m23, self.m24,
        self.m31, self.m32, self.m33, self.m34,
        self.m41, self.m42, self.m43, self.m44 ]
    }
    pub fn to_numbers_array(matrix_array: &[Matrix4x4<T>]) -> Vec<T> {
        let mut ret = vec![T::zero(); matrix_array.len() * 16];
        for (i, matrix) in matrix_array.iter().enumerate() {
            let j = i * 16;
            ret[j + 0] = matrix.m11;
            ret[j + 1] = matrix.m12;
            ret[j + 2] = matrix.m13;
            ret[j + 3] = matrix.m14;
            ret[j + 4] = matrix.m21;
            ret[j + 5] = matrix.m22;
            ret[j + 6] = matrix.m23;
            ret[j + 7] = matrix.m24;
            ret[j + 8] = matrix.m31;
            ret[j + 9] = matrix.m32;
            ret[j + 10] = matrix.m33;
            ret[j + 11] = matrix.m34;
            ret[j + 12] = matrix.m41;
            ret[j + 13] = matrix.m42;
            ret[j + 14] = matrix.m43;
            ret[j + 15] = matrix.m44;
        }
        ret
    }

    pub fn to_matrix(m: &[T]) -> Self {
        Self {
            m11: m[0], m12: m[1], m13: m[2], m14: m[3],
            m21: m[4], m22: m[5], m23: m[6], m24: m[7],
            m31: m[8], m32: m[9], m33: m[10], m34: m[11],
            m41: m[12], m42: m[13], m43: m[14], m44: m[15],
        }
    }
    pub fn to_matrix_array(m: &[T]) -> Vec<Self> {
        assert_eq!(m.len() % 16, 0);
        let mut ret = Vec::with_capacity(m.len() / 16);
        for chunk in m.chunks_exact(16) {
            ret.push(Matrix4x4::to_matrix(chunk));
        }
        ret
    }

    pub fn ray_from_projection_matrix(&self, normalised_screen_coordinates: Vector2<T>) -> Ray<T> {
        let inv_projection = self.inverse();
        let inverted_y = Vector2::new(normalised_screen_coordinates.x, T::one() - normalised_screen_coordinates.y);
        let scales_normalised_screen_coordinates = inverted_y * T::from(2).unwrap() - T::one();

        let mut p0 = Vector4::new(scales_normalised_screen_coordinates.x, scales_normalised_screen_coordinates.y, T::zero(), T::one());
        let mut p1 = Vector4::new(scales_normalised_screen_coordinates.x, scales_normalised_screen_coordinates.y, T::one(), T::one());

        p0 = p0.transform(inv_projection);
        p1 = p1.transform(inv_projection);

        p0 = p0 / p0.w;
        p1 = p1 / p1.w;

        return Ray::new(p0.to_vector3(), (p1 - p0).to_vector3().normalize())
    }

    pub fn inverse(&self) -> Self {
        let mat = self.invert();
        if let Some(m) = mat { return m }
        else { panic!("No inversion of matrix available") }
    }

    pub fn multiply(matrices: &[Matrix4x4<T>]) -> Matrix4x4<T> {
        matrices.iter().fold(Matrix4x4::identity(), |m1, m2| m1 * (*m2))
    }

}

impl<T: Float + Neg<Output = T>> Neg for Matrix4x4<T> {
    type Output = Self;

    fn neg(self) -> Self::Output {
        Self {
            m11: -self.m11, m12: -self.m12, m13: -self.m13, m14: -self.m14,
            m21: -self.m21, m22: -self.m22, m23: -self.m23, m24: -self.m24,
            m31: -self.m31, m32: -self.m32, m33: -self.m33, m34: -self.m34,
            m41: -self.m41, m42: -self.m42, m43: -self.m43, m44: -self.m44,
        }
    }
}

impl<T: Float + Add<Output = T>> Add for Matrix4x4<T> {
    type Output = Self;

    fn add(self, other: Self) -> Self::Output {
        Self {
            m11: self.m11 + other.m11, m12: self.m12 + other.m12, m13: self.m13 + other.m13, m14: self.m14 + other.m14,
            m21: self.m21 + other.m21, m22: self.m22 + other.m22, m23: self.m23 + other.m23, m24: self.m24 + other.m24,
            m31: self.m31 + other.m31, m32: self.m32 + other.m32, m33: self.m33 + other.m33, m34: self.m34 + other.m34,
            m41: self.m41 + other.m41, m42: self.m42 + other.m42, m43: self.m43 + other.m43, m44: self.m44 + other.m44,
        }
    }
}

impl<T: Float + Sub<Output = T>> Sub for Matrix4x4<T> {
    type Output = Self;

    fn sub(self, other: Self) -> Self::Output {
        Self {
            m11: self.m11 - other.m11, m12: self.m12 - other.m12, m13: self.m13 - other.m13, m14: self.m14 - other.m14,
            m21: self.m21 - other.m21, m22: self.m22 - other.m22, m23: self.m23 - other.m23, m24: self.m24 - other.m24,
            m31: self.m31 - other.m31, m32: self.m32 - other.m32, m33: self.m33 - other.m33, m34: self.m34 - other.m34,
            m41: self.m41 - other.m41, m42: self.m42 - other.m42, m43: self.m43 - other.m43, m44: self.m44 - other.m44,
        }
    }
}

impl<T: Float> std::ops::Mul for Matrix4x4<T> {
    type Output = Self;

    fn mul(self, other: Self) -> Self::Output {
        Self {
            m11: self.m11 * other.m11 + self.m12 * other.m21 + self.m13 * other.m31 + self.m14 * other.m41,
            m12: self.m11 * other.m12 + self.m12 * other.m22 + self.m13 * other.m32 + self.m14 * other.m42,
            m13: self.m11 * other.m13 + self.m12 * other.m23 + self.m13 * other.m33 + self.m14 * other.m43,
            m14: self.m11 * other.m14 + self.m12 * other.m24 + self.m13 * other.m34 + self.m14 * other.m44,

            m21: self.m21 * other.m11 + self.m22 * other.m21 + self.m23 * other.m31 + self.m24 * other.m41,
            m22: self.m21 * other.m12 + self.m22 * other.m22 + self.m23 * other.m32 + self.m24 * other.m42,
            m23: self.m21 * other.m13 + self.m22 * other.m23 + self.m23 * other.m33 + self.m24 * other.m43,
            m24: self.m21 * other.m14 + self.m22 * other.m24 + self.m23 * other.m34 + self.m24 * other.m44,

            m31: self.m31 * other.m11 + self.m32 * other.m21 + self.m33 * other.m31 + self.m34 * other.m41,
            m32: self.m31 * other.m12 + self.m32 * other.m22 + self.m33 * other.m32 + self.m34 * other.m42,
            m33: self.m31 * other.m13 + self.m32 * other.m23 + self.m33 * other.m33 + self.m34 * other.m43,
            m34: self.m31 * other.m14 + self.m32 * other.m24 + self.m33 * other.m34 + self.m34 * other.m44,

            m41: self.m41 * other.m11 + self.m42 * other.m21 + self.m43 * other.m31 + self.m44 * other.m41,
            m42: self.m41 * other.m12 + self.m42 * other.m22 + self.m43 * other.m32 + self.m44 * other.m42,
            m43: self.m41 * other.m13 + self.m42 * other.m23 + self.m43 * other.m33 + self.m44 * other.m43,
            m44: self.m41 * other.m14 + self.m42 * other.m24 + self.m43 * other.m34 + self.m44 * other.m44,
        }
    }
}

impl<T: Float> Mul<T> for Matrix4x4<T> {
    type Output = Self;

    fn mul(self, other: T) -> Self::Output {
        Self {
            m11: self.m11 * other,
            m12: self.m12 * other,
            m13: self.m13 * other,
            m14: self.m14 * other,
            m21: self.m21 * other,
            m22: self.m22 * other,
            m23: self.m23 * other,
            m24: self.m24 * other,
            m31: self.m31 * other,
            m32: self.m32 * other,
            m33: self.m33 * other,
            m34: self.m34 * other,
            m41: self.m41 * other,
            m42: self.m42 * other,
            m43: self.m43 * other,
            m44: self.m44 * other,
        }
    }
}

// impl<T: PartialEq + Hash> std::hash::Hash for Matrix4x4<T> {
//     fn hash<H: std::hash::Hasher>(&self, state: &mut H) {
//         self.count.hash(state);
//         self.min.hash(state);
//         self.max.hash(state);
//         self.sum.hash(state);
//     }
// } 

// impl<T: Float> PartialEq for Matrix4x4<T> {
//     fn eq(&self, other: &Self) -> bool {
//         self.m11 == other.m11 && self.m22 == other.m22 && self.m33 == other.m33 && self.m44 == other.m44 && // Check diagonal element first for early out.
//         self.m12 == other.m12 && self.m13 == other.m13 && self.m14 == other.m14 && self.m21 == other.m21 &&
//         self.m23 == other.m23 && self.m24 == other.m24 && self.m31 == other.m31 && self.m32 == other.m32 &&
//         self.m34 == other.m34 && self.m41 == other.m41 && self.m42 == other.m42 && self.m43 == other.m43
//     }
// }

// impl<T: Float> Eq for Matrix4x4<T> {}

// impl<T: Float>  std::fmt::Display for Matrix4x4<T> {
//     fn fmt(&self, f: &mut  std::fmt::Formatter<'_>) -> std::fmt::Result {
//         write!(
//             f,
//             "{{ {{M11:{} M12:{} M13:{} M14:{}}} {{M21:{} M22:{} M23:{} M24:{}}} {{M31:{} M32:{} M33:{} M34:{}}} {{M41:{} M42:{} M43:{} M44:{}}} }}",
//             self.m11, self.m12, self.m13, self.m14,
//             self.m21, self.m22, self.m23, self.m24,
//             self.m31, self.m32, self.m33, self.m34,
//             self.m41, self.m42, self.m43, self.m44,
//         )
//     }
// }
//use std::iter::Iterator;

