﻿using System;
using System.IO;

namespace Vim.BFast
{
    /// <summary>
    /// Provides an interface to an object that manages a potentially large array of elements all of the same unmanaged type.
    /// </summary>
    public interface IBuffer
    {
        Array Data { get; }
        int ElementSize { get; }
        void Write(Stream stream);
    }

    /// <summary>
    /// A version of the IBuffer interface when the element types are known
    /// </summary>
    public interface IBuffer<T> : IBuffer
    {
        T[] GetTypedData();
    }

    /// <summary>
    /// Represents a buffer associated with a string name. 
    /// </summary>
    public interface INamedBuffer : IBuffer
    {
        string Name { get; }
    }

    /// <summary>
    /// A version of the INamedBuffer interface when the element types are known
    /// </summary>
    public interface INamedBuffer<T> : INamedBuffer, IBuffer<T>
    {
    }

    /// <summary>
    /// A concrete implementation of IBuffer
    /// </summary>
    public unsafe class Buffer<T> : IBuffer<T> where T : unmanaged
    {
        public Buffer(T[] data) => Data = data;
        public int ElementSize => sizeof(T);
        public Array Data { get; }
        public T[] GetTypedData() => Data as T[];
        public void Write(Stream stream) => stream.Write(GetTypedData());
    }

    /// <summary>
    /// A concrete implementation of INamedBuffer
    /// </summary>
    public class NamedBuffer : INamedBuffer
    {
        public NamedBuffer(IBuffer buffer, string name) => (Buffer, Name) = (buffer, name);
        public IBuffer Buffer { get; }
        public string Name { get; }
        public int ElementSize => Buffer.ElementSize;
        public Array Data => Buffer.Data;
        public void Write(Stream stream) => Buffer.Write(stream);
    }

    /// <summary>
    /// A concrete implementation of INamedBuffer with a specific type.
    /// </summary>
    public class NamedBuffer<T> : INamedBuffer<T> where T : unmanaged
    {
        public NamedBuffer(T[] data, string name) => (Array, Name) = (data, name);
        public string Name { get; }
        public unsafe int ElementSize => sizeof(T);
        public readonly T[] Array;
        public Array Data => Array;
        public T[] GetTypedData() => Array;
        public void Write(Stream stream) => stream.Write(Array);
    }
}
