using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.BFast;
using Vim.Math3d;
using Vim.Util;

namespace Vim.Format.api_v2
{
    public partial class VimBuilder
    {
        /// <summary>
        /// The underlying VIM file which will be serialized.
        /// </summary>
        private VimHeader VimHeader { get; }

        /// <summary>
        /// The list of subdivided meshes which will be accumulated.
        /// </summary>
        public List<VimSubdividedMesh> Meshes { get; } = new List<VimSubdividedMesh>();

        /// <summary>
        /// The list of instances which will be accumulated.
        /// </summary>
        public List<VimInstance> Instances { get; } = new List<VimInstance>();

        /// <summary>
        /// The list of materials which will be accumulated.
        /// </summary>
        public List<VimMaterial> Materials { get; } = new List<VimMaterial>();

        // NOTE: see other partial class definition for entity set builder definitions

        /// <summary>
        /// The dictionary of all binary assets, keyed by buffer name.
        /// </summary>
        public readonly Dictionary<string, byte[]> Assets = new Dictionary<string, byte[]>();

        /// <summary>
        /// Constructor
        /// </summary>
        public VimBuilder(
            string generator,
            SerializableVersion schema,
            string versionString,
            IReadOnlyDictionary<string, string> optionalHeaderValues = null)
        {
            VimHeader = new VimHeader(
                generator,
                schema,
                versionString,
                optionalHeaderValues
            );
        }

        /// <summary>
        /// Writes the VIM file to the given file path. Overwrites any existing file.
        /// </summary>
        public void Write(string vimFilePath)
        {
            IO.Delete(vimFilePath);
            IO.CreateFileDirectory(vimFilePath);

            using (var fileStream = File.OpenWrite(vimFilePath))
            {
                Write(fileStream);
            }
        }

        /// <summary>
        /// Writes the VIM file to the given stream.
        /// </summary>
        public void Write(Stream vimStream)
        {
            var tableBuilders = GetVimEntityTableBuilders(); // code-generated.

            var stringLookupInfo = new StringLookupInfo(tableBuilders);

            // Instantiate a new VIM object and apply the header we created in the constructor.
            var vim = new VIM()
            {
                Header = VimHeader,
                Assets = Assets.Select(kv => kv.Value.ToNamedBuffer(kv.Key)).ToArray<INamedBuffer>(),
                StringTable = stringLookupInfo.StringTable,
                EntityTableData = GetVimEntityTableData(tableBuilders, stringLookupInfo).ToList()
            };

            // For efficiency, we create a geometryWriter to avoid extra allocations in memory while writing.
            var geometryWriter = new VimGeometryDataWriter(Meshes, Instances, Materials);

            // Write the VIM's buffers using a BFastBuilder.
            var bfastBuilder = new BFastBuilder();

            bfastBuilder.Add(VIM.HeaderBufferName, vim.Header.ToBuffer());
            bfastBuilder.Add(VIM.AssetsBufferName, vim.Assets ?? Array.Empty<INamedBuffer>());
            bfastBuilder.Add(VIM.EntityTableDataBufferName, GetBFastBuilder(vim.EntityTableData));
            bfastBuilder.Add(VIM.StringTableBufferName, vim.StringTable.PackStrings().ToBuffer());
            bfastBuilder.Add(VIM.GeometryDataBufferName, geometryWriter);

            bfastBuilder.Write(vimStream);
        }

        private static BFastBuilder GetBFastBuilder(IEnumerable<VimEntityTableData> entityTables)
        {
            var bldr = new BFastBuilder();
            foreach (var et in entityTables)
            {
                bldr.Add(et.Name, et.GetAllColumns());
            }
            return bldr;
        }

        /// <summary>
        /// A helper class which collects all the strings from the entity tables to create the indexed string lookups.
        /// </summary>
        private class StringLookupInfo
        {
            public readonly IReadOnlyDictionary<string, int> StringLookup;
            public readonly string[] StringTable;

            public StringLookupInfo(IEnumerable<string> allStrings, int indexOffset = 0)
            {
                // NOTE: ensure the empty string is part of the string table.
                var stringTable = allStrings.Prepend("").Distinct().ToList();

                // By construction, the contents of stringTable should not have repeating items.
                var stringLookup = new Dictionary<string, int>();
                for (var i = 0; i < stringTable.Count; ++i)
                    stringLookup[stringTable[i]] = i + indexOffset;

                StringTable = stringTable.ToArray();
                StringLookup = stringLookup;
            }

            public StringLookupInfo(IEnumerable<VimEntityTableBuilder> tableBuilders, int indexOffset = 0)
                : this(tableBuilders.SelectMany(tb => tb.GetAllStrings()), indexOffset)
            { }
        }

        private IEnumerable<VimEntityTableData> GetVimEntityTableData(List<VimEntityTableBuilder> tableBuilders, StringLookupInfo stringLookupInfo)
            => WithGeometryTable(tableBuilders)
                .Select(tb =>
                    // Transfer each table builder's data
                    new VimEntityTableData()
                    {
                        Name = tb.Name,
                        IndexColumns = tb.IndexColumns
                            .Select(kv => kv.Value.ToNamedBuffer(kv.Key))
                            .ToList(),
                        StringColumns = tb.StringColumns
                            .Select(kv => kv.Value
                                .Select(s => stringLookupInfo.StringLookup[s ?? string.Empty])
                                .ToArray()
                                .ToNamedBuffer(kv.Key))
                            .ToList(),
                        DataColumns = tb.DataColumns
                            .Select(kv => kv.Value.ToNamedBuffer(kv.Key) as INamedBuffer)
                            .ToList()
                    }
                );

        private IEnumerable<VimEntityTableBuilder> WithGeometryTable(IEnumerable<VimEntityTableBuilder> tableBuilders)
            => tableBuilders.Where(tb => tb.Name != VimEntityTableNames.Geometry)
                .Append(CreateGeometryTable());

        private VimEntityTableBuilder CreateGeometryTable()
        {
            // At the last moment, we generate the geometry table based on the mesh bounding boxes.

            var tb = new VimEntityTableBuilder(VimEntityTableNames.Geometry);
            tb.Clear();

            // Populate the box
            var boxMinX = new float[Meshes.Count];
            var boxMinY = new float[Meshes.Count];
            var boxMinZ = new float[Meshes.Count];

            var boxMaxX = new float[Meshes.Count];
            var boxMaxY = new float[Meshes.Count];
            var boxMaxZ = new float[Meshes.Count];

            for (var i = 0; i < Meshes.Count; ++i)
            {
                var b = AABox.Create(Meshes[i].Vertices);
                boxMinX[i] = b.Min.X;
                boxMinY[i] = b.Min.Y;
                boxMinZ[i] = b.Min.Z;

                boxMaxX[i] = b.Max.X;
                boxMaxY[i] = b.Max.Y;
                boxMaxZ[i] = b.Max.Z;
            }

            tb.AddDataColumn("float:Box.Min.X", boxMinX);
            tb.AddDataColumn("float:Box.Min.Y", boxMinY);
            tb.AddDataColumn("float:Box.Min.Z", boxMinZ);

            tb.AddDataColumn("float:Box.Max.X", boxMaxX);
            tb.AddDataColumn("float:Box.Max.Y", boxMaxY);
            tb.AddDataColumn("float:Box.Max.Z", boxMaxZ);

            tb.AddDataColumn("int:VertexCount", Meshes.Select(g => g.Vertices.Count));
            tb.AddDataColumn("int:FaceCount", Meshes.Select(g => g.Indices.Count / 3));

            return tb;
        }
    }
}