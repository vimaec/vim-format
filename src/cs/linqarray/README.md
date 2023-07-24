# LinqArray

**LinqArray** is a pure functional .NET library from **[Ara 3D](https://ara3d.com)** that provides LINQ functionality for 
immutable (read only) arrays, rather than streams, while preserving `O(1)` complexity when retrieving the count or items by index. 
It is performant, memory efficient, cross-platform, safe, and easy to use.

## Overview 

LinqArray is a set of extension methods build on the `System.IReadonlyList<T>` interface which effectively is: 

```
interface IReadonlyList<T> {
    int Count { get; }
    T this[int n] { get; }
}
```

Because the interface does not mutate objects it is safe to use in a multithreaded context. Furthermore, as with regular LINQ for `IEnumerable`, 
evaluation of many of the operations can be performed on demand (aka lazily). 

## Motivation

There are two key characteristics of an array data type which are important to maintain in some problem domains: 
1. Retrieving size of collection in `O(1)` 
2. Retrieving any item in collection by index in `O(1)` 

LINQ provides a large library of extremely useful and powerful algorithms that work on any data type that can be enumerated. 
However most LINQ methods return an `IEnumerable` interface which has `O(N)` time for retrieving the size of a collection, or `O(N)` time 
for retrieving  an element in the collection at a given index. 

### Note about Big O Complexity 

The notation `O(1)` is called Big-O notation and describes the average running time of an operation in terms of the size of the input set. 
The `O(1)` means that the running time of the operation is a fixed constant independent of the size of the collection.  

### Why use Interfaces versus Types 

Using concrete types like `List` or `Array` versus an interface leadas to code that is more verbose and harder to maintain because it 
commit library authors and consumers to uses a specific implementation of an abstract data type. Library functions then have to be written 
for each type. This is why interfaces like `IEnumerable` are so prevalent: using extension methods we can easily write libraries that work 
on any conforming type. The closest thing to an array 

## Extension Methods 

LinqArray provides many of the same extension methods for `IReadOnlyList` as LINQ does for objects implementing the `IEnumerable` interface. Some examples include: 

* `Aggregate`
* `Select`
* `Zip`
* `Take`
* `Skip` 
* `Reverse` 
* `All`
* `Any`
* `Count`

## Status 

The project is under heavy development but the core functionality is fixed. 

## Similar Work

This library is based on an article on CodeProject.com called [LINQ for Immutable Arrays](https://www.codeproject.com/Articles/517728/LINQ-for-Immutable-Arrays). While I worked at Autodesk we used a library based on this article in Autodesk 3ds Max 2016 and later, which shows that 

Unlike [LinqFaster](https://github.com/jackmott/LinqFaster) by Jack Mott evaluations of functions happen lazily, and have no side effects. This means that this library can be easily used in a multi-threaded context without inccurring the overhead and complexity of  synchronization. 