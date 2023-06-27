use num_traits::{Float, FloatConst};
use crate::{Plane, Vector3};

pub fn xy_plane<T: Float>() -> Plane<T> { Plane::<T> { normal: Vector3::new(T::zero(), T::zero(), T::one()), d: T::zero() } }
pub fn xz_plane<T: Float>() -> Plane<T> { Plane::<T> { normal: Vector3::new(T::zero(), T::one(), T::zero()), d: T::zero() } }
pub fn yz_plane<T: Float>() -> Plane<T> { Plane::<T> { normal: Vector3::new(T::one(), T::zero(), T::zero()), d: T::zero() } }

pub fn tolerance<T: Float>() -> T { T::from(0.0000001f32).unwrap() }
pub fn pi<T: Float + FloatConst>() -> T { T::PI() }
pub fn half_pi<T: Float + FloatConst>() -> T { T::PI() / T::from(2).unwrap() }
pub fn two_pi<T: Float + FloatConst>() -> T { T::PI() * T::from(2).unwrap() }

pub fn log2_e<T: Float + FloatConst>() -> T { T::LOG2_E() }
pub fn log10_e<T: Float + FloatConst>() -> T { T::LOG10_E() }
pub fn e<T: Float + FloatConst>() -> T { T::E() }

pub const RADIANS_TO_DEGREES: f64 = 57.295779513082320876798154814105;
pub const DEGREES_TO_RADIANS: f64 = 0.017453292519943295769236907684886;
pub const ONE_TENTH_OF_A_DEGREE: f64 = DEGREES_TO_RADIANS / 10.0;

pub fn mm_to_feet<T: Float>() -> T { T::from(0.00328084).unwrap() }
pub fn feet_to_mm<T: Float>() -> T { T::one() / mm_to_feet() }
