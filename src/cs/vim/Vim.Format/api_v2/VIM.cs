using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Vim.BFast;
using Vim.Util;
using System.Threading;

// TODO
// - Port tests
//  - TransformServiceTests.cs => re-implement transform service
//    - Port mesh and instance filtering
//    - ...See if we can create a simple example to modify node render materials as well.
// - Read/Write implementation guinea pig: gltf converter
// - Adapt all test code to new API & fill in the gaps
// - Adapt the merge service
// - Port ColumnExtensions.cs
// - Port ColumnExtensions.Buffers.cs
// - Port ColumnExtensions.Reflection.cs
// - Test beyond this repository (i.e. with Revit exporter) > check the buildup using VimMesh

namespace Vim.Format.api_v2
{
    /// <summary>
    /// A VIM represents a building design (one or more BIM models). A VIM contains the building element geometry and its associated parameters.
    /// </summary>
    public class VIM
    {
        /// <summary>
        /// The file path of the opened VIM. Can be empty if the VIM is being written.
        /// </summary>
        public string FilePath { get; } = "";

        /// <summary>
        /// The header of the VIM, which contains IDs used to distinguish different VIM files and information about the provenance of the VIM.
        /// </summary>
        public VimHeader Header { get; } = new VimHeader();
        public const string HeaderBufferName = "header";

        /// <summary>
        /// The geometry of the building elements.
        /// </summary>
        public VimGeometryData GeometryData { get; } = new VimGeometryData();
        public const string GeometryDataBufferName = "geometry";

        /// <summary>
        /// The string table for the entities defined among the entity tables. Strings are de-duplicated in this table and indexed using string columns to avoid repetition.
        /// </summary>
        public string[] StringTable { get; } = Array.Empty<string>();
        public const string StringTableBufferName = "strings";

        /// <summary>
        /// The entity tables which define the various entities in the building model.
        /// </summary>
        public List<VimEntityTableData> EntityTableData { get; } = new List<VimEntityTableData>();
        public const string EntityTableDataBufferName = "entities";

        /// <summary>
        /// The binary assets contained in the building model, including renders, textures, etc.
        /// </summary>
        public INamedBuffer[] Assets { get; } = Array.Empty<INamedBuffer>();
        public const string AssetsBufferName = "assets";

        /// <summary>
        /// Serialization constructor
        /// </summary>
        public VIM(
            VimHeader header,
            string[] stringTable,
            List<VimEntityTableData> entityTableData,
            INamedBuffer[] assets)
        {
            Header = header;
            StringTable = stringTable;
            EntityTableData = entityTableData;
            Assets = assets;
            // ... GeometryData is serialized in VimBuilder ...
        }

        /// <summary>
        /// Stream-based constructor.
        /// </summary>
        public VIM(
            Stream vimStream,
            string vimFilePath,
            VimOpenOptions options = null,
            IProgress<string> progress = null,
            CancellationToken ct = default)
        {
            vimStream.ThrowIfNotSeekable("Could not open VIM file");

            options = options ?? new VimOpenOptions();

            FilePath = vimFilePath;

            foreach (var bufferReader in vimStream.GetBFastBufferReaders())
            {
                ct.ThrowIfCancellationRequested();

                var (name, numBytes) = bufferReader;
                bufferReader.Seek();

                switch (name)
                {
                    case HeaderBufferName:
                        {
                            progress?.Report("Reading VIM header");
                            // vim stream has been seeked to header.
                            var headerString = Encoding.UTF8.GetString(vimStream.ReadArray<byte>((int)numBytes));
                            Header = VimHeader.Parse(headerString);
                            break;
                        }

                    case AssetsBufferName:
                        {
                            if (options.IncludeAssets)
                            {
                                progress?.Report("Reading VIM assets");
                                // vim stream has been seeked to asset buffer
                                Assets = vimStream.ReadBFast().ToArray();
                            }
                            break;
                        }

                    case StringTableBufferName:
                        {
                            if (options.IncludeStringTable)
                            {
                                progress?.Report("Reading VIM string table");
                                // vim stream has been seeked to string table
                                StringTable = ReadStringTable(vimStream, numBytes);
                            }
                            break;
                        }

                    case GeometryDataBufferName:
                        {
                            if (options.IncludeGeometry)
                            {
                                progress?.Report("Reading VIM geometry");
                                // vim stream has been seeked to geometry buffer
                                GeometryData = new VimGeometryData(vimStream);
                            }
                            break;
                        }

                    case EntityTableDataBufferName:
                        {
                            if (options.IncludeEntityTables)
                            {
                                progress?.Report("Reading VIM entity tables");
                                // vim stream has been seeked to entities buffer
                                EntityTableData = VimEntityTableData.EnumerateEntityTables(bufferReader, options.SchemaOnly).ToList();
                            }
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// Opens the VIM file defined at the given file path.
        /// </summary>
        public static VIM Open(
            string vimFilePath,
            VimOpenOptions options = null,
            IProgress<string> progress = null,
            CancellationToken ct = default)
        {
            return Open(new FileInfo(vimFilePath), options, progress, ct);
        }

        /// <summary>
        /// Opens the VIM file defined in the given FileInfo
        /// </summary>
        public static VIM Open(
            FileInfo vimFileInfo,
            VimOpenOptions options = null,
            IProgress<string> progress = null,
            CancellationToken ct = default)
        {
            vimFileInfo.ThrowIfNotExists("VIM file not found");

            using (var vimFileStream = vimFileInfo.OpenRead())
            {
                return Open(vimFileStream, vimFileInfo.FullName, options, progress, ct);
            }
        }

        /// <summary>
        /// Opens the VIM file defined in the given seekable Stream.
        /// Note: The given filePath is assigned to the VIM's FilePath property.
        /// </summary>
        public static VIM Open(
            Stream vimStream,
            string vimFilePath,
            VimOpenOptions options = null,
            IProgress<string> progress = null,
            CancellationToken ct = default)
        {
            return new VIM(vimStream, vimFilePath, options, progress, ct);
        }

        /// <summary>
        /// Returns the VimGeometryData contained in the VIM at the given file path.
        /// </summary>
        public static VimGeometryData GetGeometryData(string vimFilePath)
        {
            return GetGeometryData(new FileInfo(vimFilePath));
        }

        /// <summary>
        /// Returns the VimGeometryData contained in the VIM in the given FileInfo.
        /// </summary>
        public static VimGeometryData GetGeometryData(FileInfo vimFileInfo)
        {
            vimFileInfo.ThrowIfNotExists("Could not get VIM geometry");
            using (var stream = vimFileInfo.OpenRead())
            {
                return GetGeometryData(stream);
            }
        }

        /// <summary>
        /// Returns the VimGeometryData contained in the VIM in the given stream.
        /// </summary>
        public static VimGeometryData GetGeometryData(Stream vimStream)
        {
            vimStream.ThrowIfNotSeekable("Could not get VIM geometry");

            var geometryBufferReader = vimStream.GetBFastBufferReader(GeometryDataBufferName);
            if (geometryBufferReader == null)
                return new VimGeometryData();

            geometryBufferReader.Seek(); // Seek to the correct position in the stream.

            // vim stream has been seeked to geometry buffer.
            return new VimGeometryData(vimStream);
        }

        /// <summary>
        /// Returns the string table contained in the VIM file.
        /// </summary>
        public static string[] GetStringTable(FileInfo vimFileInfo)
        {
            vimFileInfo.ThrowIfNotExists("VIM file not found. Could not get string table.");

            using (var fileStream = vimFileInfo.OpenRead())
            {
                return GetStringTable(fileStream);
            }
        }

        /// <summary>
        /// Returns the string table contained in the VIM file contained in the stream.
        /// </summary>
        public static string[] GetStringTable(Stream vimStream)
        {
            vimStream.ThrowIfNotSeekable("Could not get string table");

            var stringTableReader = vimStream.GetBFastBufferReader(StringTableBufferName);
            if (stringTableReader == null)
                return Array.Empty<string>();

            stringTableReader.Seek();

            var (_, numBytes) = stringTableReader;

            return ReadStringTable(vimStream, numBytes);
        }

        private static string[] ReadStringTable(Stream stream, long numBytes)
        {
            var stringBytes = stream.ReadArray<byte>((int)numBytes);
            var joinedStringTable = Encoding.UTF8.GetString(stringBytes);
            return joinedStringTable.Split('\0');
        }

        private VimEntityTableSet _cachedEntityTableSet = null;

        /// <summary>
        /// Returns the VimEntityTableSet contained in this VIM file.
        /// </summary>
        public VimEntityTableSet GetEntityTableSet(bool inParallel = true)
        {
            if (_cachedEntityTableSet == null)
            {
                _cachedEntityTableSet = new VimEntityTableSet(EntityTableData.ToArray(), StringTable, inParallel);
            }

            return _cachedEntityTableSet;
        }

        private VimElementGeometryInfo[] _cachedElementGeometryMap = null;

        /// <summary>
        /// Returns the VimElementGeometryMap contained in this VIM file.
        /// </summary>
        public VimElementGeometryInfo[] GetElementGeometryInfoList()
        {
            if (_cachedElementGeometryMap == null)
            {
                _cachedElementGeometryMap = VimElementGeometryInfo.GetElementGeometryInfoList(GetEntityTableSet(), GeometryData);
            }

            return _cachedElementGeometryMap;
        }

        private VimElementHierarchyService _cachedElementHierarchyService;

        /// <summary>
        /// Returns the VimElementHierarchyService contained in this VIM file.
        /// </summary>
        public VimElementHierarchyService GetElementHierarchyService(bool isElementAndDescendantPrimaryKey = false)
        {
            if (_cachedElementHierarchyService == null)
            {
                _cachedElementHierarchyService = new VimElementHierarchyService(
                    GetEntityTableSet(),
                    GetElementGeometryInfoList(),
                    isElementAndDescendantPrimaryKey);
            }

            return _cachedElementHierarchyService;
        }

        /// <summary>
        /// Returns the asset buffer based on the given name.
        /// </summary>
        public bool TryGetAssetBuffer(string assetBufferName, out INamedBuffer assetBuffer)
        {
            assetBuffer = Assets.FirstOrDefault(buffer => buffer.Name == assetBufferName);
            return assetBuffer != null;
        }

        /// <summary>
        /// Extracts the asset corresponding to the assetBufferName and returns a FileInfo representing the extracted asset on disk.<br/>
        /// Returns null if the asset could not be extracted.
        /// </summary>
        public FileInfo ExtractAsset(string assetBufferName, FileInfo fileInfo)
        {
            if (!TryGetAssetBuffer(assetBufferName, out var assetBuffer))
                return null;

            return VimAssetInfo.ExtractAsset(assetBuffer, fileInfo);
        }

        /// <summary>
        /// Extracts the assets contained in the Document to the given directory.
        /// </summary>
        public IEnumerable<(string assetBufferName, FileInfo assetFileInfo)>
            ExtractAssets(DirectoryInfo directoryInfo)
        {
            var result = new List<(string assetBufferName, FileInfo assetFileInfo)>();
            foreach (var assetBuffer in Assets)
            {
                var assetBufferName = assetBuffer.Name;
                var assetFilePath = assetBuffer.ExtractAsset(directoryInfo);
                result.Add((assetBufferName, assetFilePath));
            }
            return result;
        }

        /// <summary>
        /// Gets the byte array which defines the given asset. Returns false if the asset was not found or if the byte array is empty or null.
        /// </summary>
        public bool TryGetAssetBytes(string assetBufferName, out byte[] bytes)
        {
            bytes = null;

            if (!TryGetAssetBuffer(assetBufferName, out var assetBuffer))
                return false;

            if (!(assetBuffer is NamedBuffer<byte> byteBuffer))
                return false;

            bytes = byteBuffer.Array;

            return bytes != null && bytes.Length > 0;
        }

        /// <summary>
        /// Gets the byte array which defines the given asset. Returns false if the asset was not found or if the byte array is empty or null.
        /// </summary>
        public bool TryGetAssetBytes(VimAssetType assetType, string assetName, out byte[] bytes)
            => TryGetAssetBytes(new VimAssetInfo(assetName, assetType).ToString(), out bytes);

        /// <summary>
        /// Gets the byte array which defines the main image asset. Returns false if the asset was not found or if the byte array is empty or null.
        /// </summary>
        public bool TryGetMainImageBytes(out byte[] bytes)
            => TryGetAssetBytes(VimAssetType.Render, VimAssetInfo.MainPng, out bytes);
    }
}

