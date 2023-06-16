using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Vim.Format.Tests;

[TestFixture]
public static class ReferenceAppTests
{
    [Test]
    public static void Validate()
    {
        var vim = RepoPaths.GetLatestWolfordResidenceVim();

        Console.WriteLine("Welcome to the VIM Reference application");
        Console.WriteLine("This application demonstrates how to parse a VIM file");

        var fileInfo = new FileInfo(vim);
        Console.WriteLine($"File {fileInfo.Name} exists ({fileInfo.Exists}) and has {fileInfo.Length} bytes");

        Console.WriteLine($"Opening {fileInfo.Name} as a read-only file stream");
        using var stream = File.OpenRead(fileInfo.FullName);

        Console.WriteLine("Creating a binary reader");
        using var reader = new BinaryReader(stream);

        var magic = reader.ReadUInt64();
        Console.WriteLine($"Read magic number = {magic}, expecting {0xBFA5}");
        if (magic != 0xBFA5)
            throw new Exception($"Invalid magic number. Got {magic:X} but expected {0xBFA5:X}");

        var dataStart = reader.ReadUInt64();
        Console.WriteLine($"Read data start = {dataStart}, expecting <= {fileInfo.Length}, and divisible by 64 ({dataStart % 64} == 0)");
        
        if (dataStart > (ulong) fileInfo.Length)
            throw new Exception($"{nameof(dataStart)} ({dataStart}) is greater than the file length ({fileInfo.Length})");

        if (dataStart % 64 != 0)
            throw new Exception($"{nameof(dataStart)} ({dataStart}) is not divisible by 64");

        var dataEnd = reader.ReadUInt64();
        Console.WriteLine($"Read data end = {dataEnd}, expecting <= {fileInfo.Length} and >= {dataStart}");

        if (dataEnd > (ulong)fileInfo.Length)
            throw new Exception($"{nameof(dataEnd)} ({dataEnd}) is greater than the file length ({fileInfo.Length})");

        if (dataEnd < dataStart)
            throw new Exception($"{nameof(dataEnd)} ({dataEnd}) is less than {nameof(dataStart)} ({dataStart})");

        var numArrays = reader.ReadUInt64();
        Console.WriteLine($"# arrays = {numArrays}, expecting >= 1");

        if (numArrays < 1)
            throw new Exception($"{nameof(numArrays)} ({numArrays}) is less than 1");

        const long bfastHeaderSizeInBytes = 32;
        const long bfastRangeSizeInBytes = 16;

        var minDataStart = bfastHeaderSizeInBytes + bfastRangeSizeInBytes * numArrays;

        if (dataStart < minDataStart)
            throw new Exception($"{nameof(dataStart)} ({dataStart}) is less than the expected minimum: {minDataStart}");

        Console.WriteLine($"Reading range structures from position {stream.Position}");
        var ranges = new List<(ulong Begin, ulong End)>();
        for (var i = 0ul; i < numArrays; i++)
        {
            var begin = reader.ReadUInt64();
            var end = reader.ReadUInt64();

            ranges.Add((begin, end));
            Console.WriteLine($"Range {i}, from {begin} to {end}");
        }

        Console.WriteLine("Seeking to data start");
        stream.Seek((long)dataStart, SeekOrigin.Begin);

        var nameRange = ranges[0];
        Console.WriteLine($"Stream position {stream.Position} should already be at the first buffer {nameRange.Begin}");

        if ((ulong)stream.Position != nameRange.Begin)
            throw new Exception($"Stream position {stream.Position} is not at the first buffer {nameRange.Begin}");

        var nameByteCount = nameRange.End - nameRange.Begin;
        Console.WriteLine($"Reading names, total byte count = {nameByteCount}");
        var nameBytes = reader.ReadBytes((int)nameByteCount);

        var names = System.Text.Encoding.UTF8.GetString(nameBytes).Split((char)0, StringSplitOptions.RemoveEmptyEntries).ToArray();
        Console.WriteLine($"Found {names.Length} buffer names, expected {numArrays - 1}");

        if ((ulong) names.Length != numArrays - 1)
            throw new Exception($"{nameof(names)} length is not {numArrays - 1}");

        for (var i = 0ul; i < numArrays - 1; i++)
            Console.WriteLine($"Buffer {i} = {names[i]}");

        Console.WriteLine("Creating lookup of names to ranges");
        var nameToRange = names.Zip(ranges.Skip(1)).ToDictionary(tuple => tuple.First, tuple => tuple.Second);

        var hasHeader = nameToRange.TryGetValue("header", out var headerRange);
        Console.WriteLine($"Has header = {hasHeader}");
        if (!hasHeader)
            throw new Exception("No header found");

        // Update the seek head
        stream.Seek((long)headerRange.Begin, SeekOrigin.Begin);
        var headerByteCount = headerRange.End - headerRange.Begin;
        var headerBytes = reader.ReadBytes((int)headerByteCount);

        var header = System.Text.Encoding.UTF8.GetString(headerBytes);
        Console.WriteLine("---Begin Header Contents---");
        Console.WriteLine();
        Console.WriteLine(header);
        Console.WriteLine();
        Console.WriteLine("---End Header Contents---");

        var hasAssets = nameToRange.TryGetValue("assets", out var assetsRange);
        Console.WriteLine($"Has assets = {hasAssets}");

        var hasGeometry = nameToRange.TryGetValue("geometry", out var geometryRange);
        Console.WriteLine($"Has geometry = {hasGeometry}");

        var hasStrings = nameToRange.TryGetValue("strings", out var stringsRange);
        Console.WriteLine($"Has strings = {hasStrings}");

        var hasEntities = nameToRange.TryGetValue("entities", out var entitiesRange);
        Console.WriteLine($"Has entities = {hasEntities}");
    }
}
