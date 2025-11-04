using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Vim.BFast;
using Vim.Util;

namespace Vim.Format.api_v2
{
    public class VIM
    {
        public string FilePath { get; set; }

        public VimHeader Header { get; set; }

        public VimGeometry Geometry { get; set; }

        public string[] StringTable { get; set; }

        public List<VimDataTable> DataTables { get; set; }

        public INamedBuffer[] Assets { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public VIM(
            string filePath,
            VimHeader header,
            VimGeometry geometry,
            string[] stringTable,
            List<VimDataTable> dataTables,
            INamedBuffer[] assets
        )
        {
            FilePath = filePath;
            Header = header;
            Geometry = geometry;
            StringTable = stringTable;
            DataTables = dataTables;
            Assets = assets;
        }

        /// <summary>
        /// Opens the VIM file defined at the given file path.
        /// </summary>
        public static VIM Open(string vimFilePath, VimOpenOptions options = null)
        {
            return Open(new FileInfo(vimFilePath), options);
        }

        public static VIM Open(FileInfo vimFileInfo, VimOpenOptions options = null)
        {
            vimFileInfo.ThrowIfNotExists("VIM file not found");

            using (var fileStream = vimFileInfo.OpenRead())
            {
                return Open(fileStream, vimFileInfo.FullName, options);
            }
        }

        /// <summary>
        /// Opens the VIM file defined in the given seekable Stream.
        /// </summary>
        public static VIM Open(Stream stream, string filePath, VimOpenOptions options = null)
        {
            stream.ThrowIfNotSeekable("Could not open VIM file");

            options = options ?? new VimOpenOptions();

            VimHeader header = null;
            VimGeometry geometry = null;
            var stringTable = Array.Empty<string>();
            var dataTables = new List<VimDataTable>();
            var assets = Array.Empty<INamedBuffer>();

            foreach (var bufferReader in stream.GetBFastBufferReaders())
            {
                var (name, numBytes) = bufferReader;
                bufferReader.Seek();

                switch (name)
                {
                    case BufferNames.Header:
                        {
                            var headerString = Encoding.UTF8.GetString(stream.ReadArray<byte>((int)numBytes));
                            header = VimHeader.Parse(headerString);
                            break;
                        }

                    case BufferNames.Assets:
                        {
                            if (options.IncludeAssets)
                            {
                                assets = stream.ReadBFast().ToArray();
                            }
                            break;
                        }

                    case BufferNames.Strings:
                        {
                            if (options.IncludeStringTable)
                            {
                                stringTable = ReadStrings(stream, numBytes);
                            }
                            break;
                        }

                    case BufferNames.Geometry:
                        {
                            if (options.IncludeGeometry)
                            {
                                geometry = VimGeometry.Read(stream);
                            }
                            break;
                        }

                    case BufferNames.Entities:
                        {
                            if (options.IncludeDataTables)
                            {
                                dataTables = VimDataTable.EnumerateDataTables(bufferReader, options.SchemaOnly).ToList();
                            }
                            break;
                        }
                }
            }

            return new VIM(
                filePath,
                header,
                geometry,
                stringTable,
                dataTables,
                assets);
        }


        public static string[] GetStringTable(FileInfo vimFileInfo)
        {
            vimFileInfo.ThrowIfNotExists("VIM file not found. Could not get string table.");
            
            using (var fileStream = vimFileInfo.OpenRead())
            {
                return GetStringTable(fileStream);
            }
        }

        public static string[] GetStringTable(Stream stream)
        {
            stream.ThrowIfNotSeekable("Could not get string table");

            var stringTableReader = stream.GetBFastBufferReader(BufferNames.Strings);
            if (stringTableReader == null)
                return Array.Empty<string>();

            stringTableReader.Seek();

            var (_, numBytes) = stringTableReader;

            return ReadStrings(stream, numBytes);
        }
        
        public static string[] ReadStrings(Stream stream, long numBytes)
        {
            var stringBytes = stream.ReadArray<byte>((int)numBytes);
            var joinedStringTable = Encoding.UTF8.GetString(stringBytes);
            return joinedStringTable.Split('\0');
        }

        public void Write(string filePath)
        {
            IO.Delete(filePath);
            IO.CreateFileDirectory(filePath);

            using (var fileStream = File.OpenWrite(filePath))
            {
                Write(fileStream);
            }
        }

        public void Write(Stream stream)
        {
            // TODO
            throw new NotImplementedException();
        }

        public VIM Merge(VIM other)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}

