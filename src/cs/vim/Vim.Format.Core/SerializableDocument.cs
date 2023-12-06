using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Vim.BFastNextNS;
using Vim.G3d;

namespace Vim.Format
{
    /// <summary>
    /// Tracks all of the data for a particular entity type in a conceptual table.
    /// A column maybe a relation to another entity table (IndexColumn)
    /// a data value stored as a double (DataColumn) or else
    /// it is string data, stored as an index into the global lookup table (StringColumn).
    /// </summary>
    public class SerializableEntityTable
    {
        /// <summary>
        /// Name of 
        /// </summary>
        public string Name;

        /// <summary>
        /// Relation to another entity table. For example surface to element. 
        /// </summary>
        public List<NamedBuffer<int>> IndexColumns = new List<NamedBuffer<int>>();

        /// <summary>
        /// Data encoded as strings in the global string table
        /// </summary>
        public List<NamedBuffer<int>> StringColumns = new List<NamedBuffer<int>>();

        /// <summary>
        /// Numeric data encoded as byte, int, float, or doubles 
        /// </summary>
        public List<INamedBuffer> DataColumns = new List<INamedBuffer>();

        public IEnumerable<string> ColumnNames
            => IndexColumns.Select(c => c.Name)
                .Concat(StringColumns.Select(c => c.Name))
                .Concat(DataColumns.Select(c => c.Name));

        public IEnumerable<INamedBuffer> AllColumns
            => IndexColumns
            .Concat(StringColumns)
            .Concat(DataColumns);

        public static SerializableEntityTable FromBfast(string name, BFastNext bfast)
        {
            return null;
        }

        public BFastNext ToBFast()
        {
            var bfast = new BFastNext();
            foreach (var col in AllColumns)
            {
                bfast.SetArray(col.Name, col.AsArray<byte>());
            }
            return bfast;
        }
    }

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
        public BFastNext ToBFast()
        {
            var bfast = new BFastNext();
            //bfast.SetArray(BufferNames.Header, Header.ToBytes());

            var assets = new BFastNext();
            foreach (var asset in Assets)
            {
                assets.SetArray(asset.Name, asset.ToArray<byte>());
            }
            bfast.SetBFast(BufferNames.Assets, assets);

            var entities = new BFastNext();
            foreach (var entity in EntityTables)
            {
                entities.SetBFast(entity.Name, entity.ToBFast());
            }
            bfast.SetBFast(BufferNames.Entities, entities);
            bfast.SetArray(BufferNames.Strings, BFastIO.PackStrings(StringTable));
            bfast.SetArray(BufferNames.Geometry, Geometry.WriteToBytes());
            return bfast;
        }

        public static SerializableDocument FromPath(string path, LoadOptions options = null)
        {
            using (var file = new FileStream(path, FileMode.OpenOrCreate))
            {
                var bfast = new BFastNext(file);
                var doc = FromBFast(bfast);
                doc.FileName = path;
                return doc;
            }
        }

        public static SerializableDocument FromBFast(BFastNext bfast, LoadOptions options = null)
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
                var geo = bfast.GetArray<byte>(BufferNames.Geometry);
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
            BFastNext bfast,
            bool schemaOnly)
        {

            foreach (var entry in bfast.Entries)
            {
                var b = bfast.GetBFast(entry);
                var table = ReadEntityTable2(b, schemaOnly);
                table.Name = entry;
                yield return table;
            }
        }


        /// <summary>
        /// Returns a SerializableEntityTable based on the given buffer reader.
        /// </summary>
        public static SerializableEntityTable ReadEntityTable2(
            BFastNext bfast,
            bool schemaOnly
           )
        {
            var et = new SerializableEntityTable();
            foreach (var entry in bfast.Entries)
            {
                var typePrefix = entry.GetTypePrefix();

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
