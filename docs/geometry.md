# Vim.Geometry

Vim.Geometry is an efficient open-source cross-platform .NET library of pure functional 3D geometry data structures and algorithms inspired by LINQ.

## Why Functional Programming? 

There are many advantages to functional programming, and in particular the usage of immutable data structures and lazy evaluation. Some of these are: 

* Easier parallelization
* Simpler algorithms
* Fewer memory allocations  
* Less defects
* Easier refactoring
* Faster prototyping

## Advantages of using Interfaces

Most Vim.Geometry algorithms operate on abstract interfaces such as `IMesh`. This is designed to make it easier to adapt algorithms 
to different types of geometry representations from different libraries (e.g. Geometry3Sharp, Unity, 3ds Max, Maya).

## Related Work

Another popular library for 3D Geomtry manipulation is the excellent [Geometry3Sharp](https://github.com/gradientspace/geometry3Sharp) library by 
Ryan Schmidt from [GradientSpace](https://github.com/gradientspace). Geometry3Sharp contains a large number of useful and interesting algorithms,
but is designed more for the 3D printing market, so it makes some performance trade-offs for increased accuracy. 

The [MonoGame library](https://github.com/MonoGame/MonoGame) has a number of useful low-level 3D algorithms and data structures. 
We use some modified MonoGame code in the Math3D library. It is interesting to note that System.Numerics and MonoGame have some common 
ancestry in the now discontinued XNA library.  

## Status

This library is under active development for use in products and services developed by VIMaec LLC. 