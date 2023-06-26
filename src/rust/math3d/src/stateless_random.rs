use crate::{hash, Vector2, Vector3, Vector4};
use num_traits::Float;

pub fn random_uint<T: Float>(index: usize, seed: u64) -> u64 {
    hash::combine::<T>(seed, index as u64)
}

pub fn random_int<T: Float>(index: usize, seed: u64) -> u64 {
    hash::combine::<T>(seed, index as u64)
}

pub fn random_float<T: Float+ std::fmt::Debug>(min: T, max: T, index: usize, seed: u64) -> T {
    let random_uint: T = T::from(random_uint::<T>(index, seed)).unwrap();
    let max_value: T = T::from(u64::MAX).unwrap();
    let res:T = random_uint / max_value * (max - min) + min;
    res
}

pub fn random_float_default<T: Float+ std::fmt::Debug>(index: usize, seed: u64) -> T {
    random_float(T::zero(), T::one(), index, seed)
}

pub fn random_vector2<T: Float+ std::fmt::Debug>(index: usize, seed: u64) -> Vector2<T> {
    Vector2::<T>::new(
        random_float_default(index * 2, seed),
        random_float_default(index * 2 + 1, seed),
    )
}

pub fn random_vector3<T: Float+ std::fmt::Debug>(index: usize, seed: u64) -> Vector3<T> {
    Vector3::<T>::new(
        random_float_default(index * 3, seed),
        random_float_default(index * 3 + 1, seed),
        random_float_default(index * 3 + 2, seed),
    )
}

pub fn random_vector4<T: Float+ std::fmt::Debug>(index: usize, seed: u64) -> Vector4<T> {
    Vector4::<T>::new(
        random_float_default(index * 4, seed),
        random_float_default(index * 4 + 1, seed),
        random_float_default(index * 4 + 2, seed),
        random_float_default(index * 4 + 3, seed),
    )
}