using System.Collections.Generic;
using System.Linq;
using System;
using Vim.BFast;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Vim.G3d;
using Vim.Util;

namespace Vim.Format
{
    public static class Serializer
    {
        public static List<INamedBuffer> ToBuffers(this SerializableEntityTable table)
        {
            var r = new List<INamedBuffer>();

            r.AddRange(table.DataColumns);
            r.AddRange(table.IndexColumns);
            r.AddRange(table.StringColumns);

            return r;
        }

        public static readonly Regex TypePrefixRegex = new Regex(@"(\w+:).*");

        public static string GetTypePrefix(this string name)
        {
            var match = TypePrefixRegex.Match(name);
            return match.Success ? match.Groups[1].Value : "";
        }

        /// <summary>
        /// Returns the named buffer prefix, or null if no prefix was found.
        /// </summary>
        public static string GetTypePrefix(this INamedBuffer namedBuffer)
            => namedBuffer.Name.GetTypePrefix();

        /// <summary>
        /// Returns a NamedBuffer representing to an entity table column.
        /// If schemaOnly is enabled, the column is returned without any of its contained data;
        /// this is useful for rapidly querying the schema of the entity table.
        /// </summary>
        public static NamedBuffer<T> ReadEntityTableColumn<T>(
            this BFastBufferReader columnBufferReader,
            bool schemaOnly) where T : unmanaged
        {
            var (name, size) = columnBufferReader;

            if (schemaOnly)
                return new Buffer<T>(Array.Empty<T>()).ToNamedBuffer(name);

            return columnBufferReader
                .Seek()
                .ReadBufferFromNumberOfBytes<T>(size)
                .ToNamedBuffer(name);
        }

        /// <summary>
        /// Returns a SerializableEntityTable based on the given buffer reader.
        /// </summary>
        public static SerializableEntityTable ReadEntityTable(
            this BFastBufferReader entityTableBufferReader,
            bool schemaOnly)
        {
            var et = new SerializableEntityTable { Name = entityTableBufferReader.Name };

            foreach (var colBr in entityTableBufferReader.Seek().GetBFastBufferReaders())
            {
                var name = colBr.Name;
                var typePrefix = name.GetTypePrefix();

                switch (typePrefix)
                {
                    case VimConstants.IndexColumnNameTypePrefix:
                        {
                            et.IndexColumns.Add(colBr.ReadEntityTableColumn<int>(schemaOnly));
                            break;
                        }
                    case VimConstants.StringColumnNameTypePrefix:
                        {
                            et.StringColumns.Add(colBr.ReadEntityTableColumn<int>(schemaOnly));
                            break;
                        }
                    case VimConstants.IntColumnNameTypePrefix:
                        {
                            et.DataColumns.Add(colBr.ReadEntityTableColumn<int>(schemaOnly));
                            break;
                        }
                    case VimConstants.LongColumnNameTypePrefix:
                        {
                            et.DataColumns.Add(colBr.ReadEntityTableColumn<long>(schemaOnly));
                            break;
                        }
                    case VimConstants.DoubleColumnNameTypePrefix:
                        {
                            et.DataColumns.Add(colBr.ReadEntityTableColumn<double>(schemaOnly));
                            break;
                        }
                    case VimConstants.FloatColumnNameTypePrefix:
                        {
                            et.DataColumns.Add(colBr.ReadEntityTableColumn<float>(schemaOnly));
                            break;
                        }
                    case VimConstants.ByteColumnNameTypePrefix:
                        {
                            et.DataColumns.Add(colBr.ReadEntityTableColumn<byte>(schemaOnly));
                            break;
                        }
                    // For flexibility, we ignore the columns which do not contain a recognized prefix.
                }
            }

            return et;
        }

        /// <summary>
        /// Enumerates the SerializableEntityTables contained in the given entities buffer.
        /// </summary>
        public static IEnumerable<SerializableEntityTable> EnumerateEntityTables(
            this BFastBufferReader entitiesBufferReader,
            bool schemaOnly)
        {
            foreach (var entityTableBufferReader in entitiesBufferReader.Seek().GetBFastBufferReaders())
            {
                yield return entityTableBufferReader.ReadEntityTable(schemaOnly);
            }
        }

        /// <summary>
        /// Enumerates the SerializableEntityTables contained in the given VIM file.
        /// </summary>
        public static IEnumerable<SerializableEntityTable> EnumerateEntityTables(this FileInfo vimFileInfo, bool schemaOnly)
        {
            using (var stream = vimFileInfo.OpenRead())
            {
                var entitiesBufferReader = stream.GetBFastBufferReaders(b => b.Name == BufferNames.Entities).FirstOrDefault();
                if (entitiesBufferReader == null)
                    yield break;

                foreach (var entityTable in entitiesBufferReader.EnumerateEntityTables(schemaOnly))
                {
                    yield return entityTable;
                }
            }
        }

        public static BFastBuilder ToBFastBuilder(this IEnumerable<SerializableEntityTable> entityTables)
        {
            var bldr = new BFastBuilder();
            foreach (var et in entityTables)
            {
                bldr.Add(et.Name, et.ToBuffers());
            }
            return bldr;
        }

        public static BFastBuilder ToBFastBuilder(this SerializableDocument doc)
            => CreateBFastBuilder(
                doc.Header,
                doc.Assets,
                doc.StringTable,
                doc.EntityTables,
                doc.Geometry.ToG3DWriter());

        public static BFastBuilder CreateBFastBuilder(
            SerializableHeader header,
            IEnumerable<INamedBuffer> assets,
            IEnumerable<string> stringTable,
            IEnumerable<SerializableEntityTable> entityTables,
            IBFastComponent geometry)
        {
            var bfastBuilder = new BFastBuilder();
            bfastBuilder.Add(BufferNames.Header, header.ToString().ToBytesUtf8().ToBuffer());
            bfastBuilder.Add(BufferNames.Assets, assets ?? Array.Empty<INamedBuffer>());
            bfastBuilder.Add(BufferNames.Entities, entityTables.ToBFastBuilder());
            bfastBuilder.Add(BufferNames.Strings, stringTable.PackStrings().ToBuffer());
            bfastBuilder.Add(BufferNames.Geometry, geometry);
            return bfastBuilder;
        }

        public static void Serialize(
            Stream stream,
            SerializableHeader header,
            IEnumerable<INamedBuffer> assets,
            IEnumerable<string> stringTable,
            IEnumerable<SerializableEntityTable> entityTables,
            IBFastComponent geometry)
        {
            CreateBFastBuilder(header, assets, stringTable, entityTables, geometry).Write(stream);
        }

        public static void Serialize(this SerializableDocument doc, Stream stream)
            => doc.ToBFastBuilder().Write(stream);

        public static void Serialize(this SerializableDocument document, string filePath)
        {
            using (var stream = File.OpenWrite(filePath))
                document.Serialize(stream);
        }

        public static SerializableHeader ToSerializableHeader(this byte[] bytes)
            => SerializableHeader.Parse(Encoding.UTF8.GetString(bytes));

        /// <summary>
        /// Returns true if the SerializableHeader in the stream is successfully parsed.
        /// </summary>
        public static bool TryParseSerializableHeader(this Stream stream, out SerializableHeader header)
        {
            using (new SeekContext(stream))
            {
                try
                {
                    header = stream.ReadBFastBuffer<byte>(BufferNames.Header)?.Array.ToSerializableHeader();
                }
                catch
                {
                    header = null;
                }
                return header != null;
            }
        }

        /// <summary>
        /// Returns true if the SerializableHeader in the given VIM file is successfully parsed.
        /// </summary>
        public static bool TryParseSerializableHeader(this FileInfo fileInfo, out SerializableHeader header)
        {
            using (var fs = fileInfo.OpenRead())
            {
                return fs.TryParseSerializableHeader(out header);
            }
        }

        /// <summary>
        /// Returns the VIM file's header schema version. Returns null if the header schema is not found.
        /// </summary>
        public static string GetSchemaVersion(this FileInfo fileInfo)
            => fileInfo.TryParseSerializableHeader(out var header)
                ? header.Schema?.ToString()
                : null;

        public static void ReadBuffer(this SerializableDocument doc, BFastBufferReader bufferReader)
        {
            var (name, numBytes) = bufferReader;
            var stream = bufferReader.Seek();

            switch (name)
            {
                case BufferNames.Header:
                    {
                        doc.Header = stream.ReadArray<byte>((int)numBytes).ToSerializableHeader();
                        break;
                    }

                case BufferNames.Assets:
                    {
                        if (doc.Options?.SkipAssets == true)
                            break;

                        doc.Assets = stream.ReadBFast().ToArray();
                        break;
                    }

                case BufferNames.Strings:
                    {
                        doc.StringTable = ReadStrings(stream, numBytes);
                        break;
                    }

                case BufferNames.Geometry:
                    {
                        if (doc.Options?.SkipGeometry == true)
                            break;

                        doc.Geometry = G3D.Read(stream);
                        break;
                    }

                case BufferNames.Entities:
                    {
                        doc.EntityTables =
                            bufferReader
                            .EnumerateEntityTables(doc.Options?.SchemaOnly ?? false)
                            .ToList();
                        break;
                    }
            }
        }

        public static string[] ReadStrings(Stream stream, long numBytes)
        {
            var stringBytes = stream.ReadArray<byte>((int)numBytes);
            var joinedStringTable = Encoding.UTF8.GetString(stringBytes);
            return joinedStringTable.Split('\0');
        }

        public static SerializableDocument Deserialize(Stream stream, LoadOptions loadOptions = null)
        {
            var doc = new SerializableDocument { Options = loadOptions };

            foreach (var buffer in stream.GetBFastBufferReaders())
            {
                doc.ReadBuffer(buffer);
            }

            return doc;
        }

        public static SerializableDocument Deserialize(string filePath, LoadOptions loadOptions = null)
        {
            using (var stream = File.OpenRead(filePath))
            {
                var doc = Deserialize(stream, loadOptions);
                doc.SetFileName(filePath);
                return doc;
            }
        }
    }
}
