using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Vim.BFastLib;
using Vim.BFastLib.Core;
using Vim.G3d;

namespace Vim.Format
{
    /// <summary>
    /// Controls what parts of the VIM file are loaded
    /// </summary>
    public class LoadOptions
    {
        public bool SkipGeometry = false;
        public bool SkipAssets = false;
        public bool SchemaOnly = false;
    }

    /// <summary>
    /// The low-level representation of a VIM data file.
    /// </summary>
    public class SerializableDocument
    {
        /// <summary>
        /// Controls how the file is read and loaded into memory
        /// </summary>
        public LoadOptions Options = new LoadOptions();

        /// <summary>
        /// A collection of endline terminated <key>=<value> pairs information about the file
        /// </summary>
        public SerializableHeader Header;

        /// <summary>
        /// A an array of tables, one for each entity 
        /// </summary>
        public List<SerializableEntityTable> EntityTables = new List<SerializableEntityTable>();

        /// <summary>
        /// Used for looking up property strings and entity string fields by Id
        /// </summary>
        public string[] StringTable = Array.Empty<string>();

        /// <summary>
        /// A list of named buffers each representing a different asset in the file 
        /// </summary>
        public INamedBuffer[] Assets = Array.Empty<INamedBuffer>();

        /// <summary>
        /// The uninstanced / untransformed geometry
        /// </summary>
        public G3d.G3D Geometry;

        /// <summary>
        /// The originating file name (if provided)
        /// </summary>
        public string FileName;
        public BFast ToBFast()
        {
            var bfast = new BFast();
            if(Header != null)
            {
                bfast.SetArray(BufferNames.Header, Header.ToBytes());
            }

            if(Assets != null)
            {
                var assets = new BFast();
                foreach (var asset in Assets)
                {
                    assets.SetArray(asset.Name, asset.ToArray<byte>());
                }
                bfast.SetBFast(BufferNames.Assets, assets);
            }
                
            if(EntityTables != null)
            {
                var entities = new BFast();
                foreach (var entity in EntityTables)
                {
                    entities.SetBFast(entity.Name, entity.ToBFast());
                }
                bfast.SetBFast(BufferNames.Entities, entities);
            }

            if (StringTable != null)
            {
                bfast.SetArray(BufferNames.Strings, BFastStrings.Pack(StringTable));
            }

            if(Geometry != null)
            {
                bfast.SetBFast(BufferNames.Geometry, Geometry?.ToBFast());
            }
            
            return bfast;
        }

        public static SerializableDocument FromPath(string path, LoadOptions options = null)
        {
            var doc = BFastHelpers.Read(path, b => FromBFast(b));
            doc.FileName = path;
            return doc;
        }

        public static SerializableDocument FromBFast(BFast bfast, LoadOptions options = null)
        {
            var doc = new SerializableDocument();
            doc.Options = options ?? new LoadOptions();
            doc.Header = SerializableHeader.FromBytes(bfast.GetArray<byte>(BufferNames.Header));
            if (!doc.Options.SkipAssets)
            {
                var asset = bfast.GetBFast(BufferNames.Assets);
                doc.Assets = asset.ToNamedBuffers().ToArray();
            }
            var strs = bfast.GetArray<byte>(BufferNames.Strings);
            doc.StringTable = Encoding.UTF8.GetString(strs).Split('\0');

            if (!doc.Options.SkipGeometry)
            {
                var geo = bfast.GetBFast(BufferNames.Geometry);
                doc.Geometry = G3D.Read(geo);
            }

            var entities = bfast.GetBFast(BufferNames.Entities);
            doc.EntityTables = GetEntityTables(entities, doc.Options.SchemaOnly).ToList();
            return doc;
        }

        /// <summary>
        /// Enumerates the SerializableEntityTables contained in the given entities buffer.
        /// </summary>
        private static IEnumerable<SerializableEntityTable> GetEntityTables(
            BFast bfast,
            bool schemaOnly)
        {

            foreach (var entry in bfast.Entries)
            {
                var b = bfast.GetBFast(entry);
                var table = ReadEntityTable(b, schemaOnly);
                table.Name = entry;
                yield return table;
            }
        }

        /// <summary>
        /// Returns a SerializableEntityTable based on the given buffer reader.
        /// </summary>
        public static SerializableEntityTable ReadEntityTable(
            BFast bfast,
            bool schemaOnly
           )
        {
            var et = new SerializableEntityTable();
            foreach (var entry in bfast.Entries)
            {
                var typePrefix = SerializableEntityTable.GetTypeFromName(entry);

                switch (typePrefix)
                {
                    case VimConstants.IndexColumnNameTypePrefix:
                        {
                            //TODO: replace named buffer with arrays
                            var col = schemaOnly ? new int[0] : bfast.GetArray<int>(entry);
                            et.IndexColumns.Add(col.ToNamedBuffer(entry));
                            break;
                        }
                    case VimConstants.StringColumnNameTypePrefix:
                        {
                            var col = schemaOnly ? new int[0] : bfast.GetArray<int>(entry);
                            et.StringColumns.Add(col.ToNamedBuffer(entry));
                            break;
                        }
                    case VimConstants.IntColumnNameTypePrefix:
                        {
                            var col = schemaOnly ? new int[0] : bfast.GetArray<int>(entry);
                            et.DataColumns.Add(col.ToNamedBuffer(entry));
                            break;
                        }
                    case VimConstants.LongColumnNameTypePrefix:
                        {
                            var col = schemaOnly ? new long[0] : bfast.GetArray<long>(entry);
                            et.DataColumns.Add(col.ToNamedBuffer(entry));
                            break;
                        }
                    case VimConstants.DoubleColumnNameTypePrefix:
                        {
                            var col = schemaOnly ? new double[0] : bfast.GetArray<double>(entry);
                            et.DataColumns.Add(col.ToNamedBuffer(entry));
                            break;
                        }
                    case VimConstants.FloatColumnNameTypePrefix:
                        {
                            var col = schemaOnly ? new float[0] : bfast.GetArray<float>(entry);
                            et.DataColumns.Add(col.ToNamedBuffer(entry));
                            break;
                        }
                    case VimConstants.ByteColumnNameTypePrefix:
                        {
                            var col = schemaOnly ? new byte[0] : bfast.GetArray<byte>(entry);
                            et.DataColumns.Add(col.ToNamedBuffer(entry));
                            break;
                        }
                        // For flexibility, we ignore the columns which do not contain a recognized prefix.
                }
            }

            return et;
        }
    }
}
