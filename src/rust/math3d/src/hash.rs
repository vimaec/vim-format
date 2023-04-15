use num_traits::Float;
use std::hash::Hasher;
use std::collections::hash_map::DefaultHasher;

// Discussion: if we want to go deeper into the subject, check out
// https://en.wikipedia.org/wiki/List_of_hash_functions
// https://stackoverflow.com/questions/5889238/why-is-xor-the-default-way-to-combine-hashes
// https://en.wikipedia.org/wiki/Jenkins_hash_function#cite_note-11
// https://referencesource.microsoft.com/#System.Numerics/System/Numerics/HashCodeHelper.cs
// https://github.com/dotnet/corefx/blob/master/src/Common/src/CoreLib/System/Numerics/Hashing/HashHelpers.cs
 
pub fn combine<T: Float>(h1: u64, h2: u64) -> u64 {
    let rol5 = ((h1) << 5) | ((h1) >> 27);
    ((rol5) + h1) ^ h2
}
pub fn combine_list<T: Float>(xs: &[u64]) -> u64 {
    if xs.is_empty() { return 0; }
    let mut r = xs[0];
    for i in 1..xs.len() { r = combine::<T>(r, xs[i]); }
    r
}
pub fn combine_array<T: Float>(xs: &[u64]) -> u64 { combine_list::<T>(xs) }
pub fn combine_multiple<T: Float>(x0: u64, x1: u64, x2: u64) -> u64 {
    combine::<T>(combine::<T>(x0, x1), x2)
}
pub fn combine_quadruple<T: Float>(x0: u64, x1: u64, x2: u64, x3: u64) -> u64 {
    combine::<T>(combine_multiple::<T>(x0, x1, x2), x3)
}
pub fn hash_values<T: Float, I: Iterator<Item = u64>>(values: I) -> u64 {
    values.fold(0, |acc, x| combine::<T>(acc, x))
}
pub fn hash_codes<T: Float + core::hash::Hash, I: Iterator<Item = T>>(values: I) -> u64 {
    let mut hasher = DefaultHasher::new();
    values.fold(0, |acc, x| {
        x.hash(&mut hasher);
        combine::<T>(acc, hasher.finish())
    })
}
 