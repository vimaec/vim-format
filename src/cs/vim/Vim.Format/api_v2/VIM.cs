using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Vim.BFast;
using Vim.Util;
using System.Threading;

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
        public VimGeometry Geometry { get; set; } = new VimGeometry();
        public const string GeometryBufferName = "geometry";

        /// <summary>
        /// The string table for the entities defined among the entity tables. Strings are de-duplicated in this table and indexed using string columns to avoid repetition.
        /// </summary>
        public string[] StringTable { get; set; } = Array.Empty<string>();
        public const string StringTableBufferName = "strings";

        /// <summary>
        /// The entity tables which define the various entities in the building model.
        /// </summary>
        public List<VimEntityTable> EntityTables { get; set; } = new List<VimEntityTable>();
        public const string EntityTablesBufferName = "entities";

        // Search & replace:
        // - entity table (case sensitive) -> entity table

        /// <summary>
        /// The binary assets contained in the building model, including renders, textures, etc.
        /// </summary>
        public INamedBuffer[] Assets { get; set; } = Array.Empty<INamedBuffer>();
        public const string AssetsBufferName = "assets";

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

            using (var fileStream = vimFileInfo.OpenRead())
            {
                return Open(fileStream, vimFileInfo.FullName, options, progress, ct);
            }
        }

        /// <summary>
        /// Opens the VIM file defined in the given seekable Stream.
        /// Note: The given filePath is assigned to the VIM's FilePath property.
        /// </summary>
        public static VIM Open(
            Stream stream,
            string filePath,
            VimOpenOptions options = null,
            IProgress<string> progress = null,
            CancellationToken ct = default)
        {
            stream.ThrowIfNotSeekable("Could not open VIM file");

            options = options ?? new VimOpenOptions();

            var vim = new VIM() { FilePath = filePath };

            foreach (var bufferReader in stream.GetBFastBufferReaders())
            {
                ct.ThrowIfCancellationRequested();

                var (name, numBytes) = bufferReader;
                bufferReader.Seek();

                switch (name)
                {
                    case HeaderBufferName:
                        {
                            progress?.Report("Reading VIM header");
                            var headerString = Encoding.UTF8.GetString(stream.ReadArray<byte>((int)numBytes));
                            vim.Header = VimHeader.Parse(headerString);
                            break;
                        }

                    case AssetsBufferName:
                        {
                            if (options.IncludeAssets)
                            {
                                progress?.Report("Reading VIM assets");
                                vim.Assets = stream.ReadBFast().ToArray();
                            }
                            break;
                        }

                    case StringTableBufferName:
                        {
                            if (options.IncludeStringTable)
                            {
                                progress?.Report("Reading VIM string table");
                                vim.StringTable = ReadStringTable(stream, numBytes);
                            }
                            break;
                        }

                    case GeometryBufferName:
                        {
                            if (options.IncludeGeometry)
                            {
                                progress?.Report("Reading VIM geometry");
                                vim.Geometry = VimGeometry.Read(stream);
                            }
                            break;
                        }

                    case EntityTablesBufferName:
                        {
                            if (options.IncludeEntityTables)
                            {
                                progress?.Report("Reading VIM entity tables");
                                vim.EntityTables = VimEntityTable.EnumerateEntityTables(bufferReader, options.SchemaOnly).ToList();
                            }
                            break;
                        }
                }
            }

            return vim;
        }

        /// <summary>
        /// Returns the VimGeometry contained in the VIM at the given file path.
        /// </summary>
        public static VimGeometry GetGeometry(string vimFilePath)
        {
            return GetGeometry(new FileInfo(vimFilePath));
        }

        /// <summary>
        /// Returns the VimGeometry contained in the VIM in the given FileInfo.
        /// </summary>
        public static VimGeometry GetGeometry(FileInfo vimFileInfo)
        {
            vimFileInfo.ThrowIfNotExists("Could not get VIM geometry");
            using (var stream = vimFileInfo.OpenRead())
            {
                return GetGeometry(stream);
            }
        }

        /// <summary>
        /// Returns the VimGeometry contained in the VIM in the given stream.
        /// </summary>
        public static VimGeometry GetGeometry(Stream vimStream)
        {
            vimStream.ThrowIfNotSeekable("Could not get VIM geometry");

            var geometryBufferReader = vimStream.GetBFastBufferReader(GeometryBufferName);
            if (geometryBufferReader == null)
                return new VimGeometry();

            geometryBufferReader.Seek(); // Seek to the correct position in the stream.

            return VimGeometry.Read(vimStream); // read teh stream at the seeked position
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
        /// Returns a new VIM representing the merge of this VIM with the other VIM.
        /// </summary>
        public VIM Merge(VIM other)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}

