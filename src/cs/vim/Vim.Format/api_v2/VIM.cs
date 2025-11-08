using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Vim.BFast;
using Vim.Util;
using System.Threading;

// TODO
// - Test file open
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
        /// The file path of the VIM. Can be empty if the VIM was created in memory.
        /// </summary>
        public string FilePath { get; set; } = "";

        /// <summary>
        /// The header of the VIM, which contains IDs used to distinguish different VIM files and information about the provenance of the VIM.
        /// </summary>
        public VimHeader Header { get; set; } = new VimHeader();
        public const string HeaderBufferName = "header";

        /// <summary>
        /// The geometry of the building elements.
        /// </summary>
        public VimGeometryData GeometryData { get; set; } = new VimGeometryData();
        public const string GeometryDataBufferName = "geometry";

        /// <summary>
        /// The string table for the entities defined among the entity tables. Strings are de-duplicated in this table and indexed using string columns to avoid repetition.
        /// </summary>
        public string[] StringTable { get; set; } = Array.Empty<string>();
        public const string StringTableBufferName = "strings";

        /// <summary>
        /// The entity tables which define the various entities in the building model.
        /// </summary>
        public List<VimEntityTableData> EntityTableData { get; set; } = new List<VimEntityTableData>();
        public const string EntityTableDataBufferName = "entities";

        /// <summary>
        /// The binary assets contained in the building model, including renders, textures, etc.
        /// </summary>
        public INamedBuffer[] Assets { get; set; } = Array.Empty<INamedBuffer>();
        public const string AssetsBufferName = "assets";

        /// <summary>
        /// Default constructor
        /// </summary>
        public VIM()
        { }

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
        public static VimGeometryData GetVimGeometryData(string vimFilePath)
        {
            return GetVimGeometryData(new FileInfo(vimFilePath));
        }

        /// <summary>
        /// Returns the VimGeometryData contained in the VIM in the given FileInfo.
        /// </summary>
        public static VimGeometryData GetVimGeometryData(FileInfo vimFileInfo)
        {
            vimFileInfo.ThrowIfNotExists("Could not get VIM geometry");
            using (var stream = vimFileInfo.OpenRead())
            {
                return GetVimGeometryData(stream);
            }
        }

        /// <summary>
        /// Returns the VimGeometryData contained in the VIM in the given stream.
        /// </summary>
        public static VimGeometryData GetVimGeometryData(Stream vimStream)
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

        /// <summary>
        /// Returns the VimEntityTableSet contained in the given VIM file.
        /// </summary>
        public static VimEntityTableSet GetVimEntityTableSet(string vimFilePath, VimEntityTableSetOptions options = null)
        {
            return GetVimEntityTableSet(new FileInfo(vimFilePath), options);
        }

        /// <summary>
        /// Returns the VimEntityTableSet contained in the given VIM file.
        /// </summary>
        public static VimEntityTableSet GetVimEntityTableSet(FileInfo vimFileInfo, VimEntityTableSetOptions options = null)
        {
            vimFileInfo.ThrowIfNotExists("Could not get the entity table set.");

            var stringTable = options.StringTable
                ?? (options.SchemaOnly ? null : GetStringTable(vimFileInfo));

            var entityTableData = VimEntityTableData.EnumerateEntityTables(
                vimFileInfo,
                options.SchemaOnly,
                options.EntityTableNameFilter,
                options.EntityTableColumnFilter)
                .ToArray();

            return new VimEntityTableSet(entityTableData, stringTable, options.InParallel);
        }

        /// <summary>
        /// Returns the VimEntityTableSet contained in this VIM file.
        /// </summary>
        public VimEntityTableSet GetVimEntityTableSet(bool inParallel = true)
        {
            return new VimEntityTableSet(EntityTableData.ToArray(), StringTable, inParallel);
        }

        /// <summary>
        /// Returns an array aligned with the Element table designating the ElementKind of each element in the VIM file.
        /// </summary>
        public static ObjectModel.ElementKind[] GetElementKinds(string vimFilePath)
        {
            return GetElementKinds(new FileInfo(vimFilePath));
        }

        /// <summary>
        /// Returns an array aligned with the Element table designating the ElementKind of each element in the VIM file.
        /// </summary>
        public static ObjectModel.ElementKind[] GetElementKinds(FileInfo vimFileInfo)
        {
            vimFileInfo.ThrowIfNotExists("Could not get the element kinds.");

            return VimEntityTableSet.GetElementKinds(vimFileInfo);
        }

        /// <summary>
        /// Returns an array aligned with the Element table designating the ElementKind of each element in the VIM file.
        /// </summary>
        public ObjectModel.ElementKind[] GetElementKinds(VimEntityTableSet tableSet)
        {
            return tableSet.GetElementKinds();
        }

        /// <summary>
        /// Returns a VimElementGeometryMap based on the givem VIM file path.
        /// </summary>
        public static VimElementGeometryMap GetVimElementGeometryMap(string vimFilePath)
        {
            return GetVimElementGeometryMap(new FileInfo(vimFilePath));
        }

        /// <summary>
        /// Returns a VimElementGeometryMap based on the givem VIM file.
        /// </summary>
        public static VimElementGeometryMap GetVimElementGeometryMap(FileInfo vimFileInfo, VimEntityTableSet tableSetWithNodeAndElement = null)
        {
            vimFileInfo.ThrowIfNotExists("Could not get VIM element geometry map");

            var tableSet = tableSetWithNodeAndElement ?? GetVimEntityTableSet(vimFileInfo, new VimEntityTableSetOptions()
            {
                StringTable = Array.Empty<string>(),
                EntityTableNameFilter = n => n is VimEntityTableNames.Node || n is VimEntityTableNames.Element
            });

            var vimGeometryData = GetVimGeometryData(vimFileInfo);

            return new VimElementGeometryMap(tableSet, vimGeometryData);
        }
    }
}

