using System;
using System.Collections.Generic;
using System.IO;
using Vim.BFast;
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

        /// <summary>
        /// The dictionary of all entity table builders, keyed by entity table name.
        /// </summary>
        public readonly Dictionary<string, EntityTableBuilder> Tables = new Dictionary<string, EntityTableBuilder>();

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
            var (vim, geometryWriter) = CreateVimAndGeometryWriter();

            var bfastBuilder = new BFastBuilder();

            bfastBuilder.Add(VIM.HeaderBufferName, vim.Header.ToBuffer());
            bfastBuilder.Add(VIM.AssetsBufferName, vim.Assets ?? Array.Empty<INamedBuffer>());
            bfastBuilder.Add(VIM.DataTablesBufferName, VimDataTable.GetBFastBuilder(vim.DataTables));
            bfastBuilder.Add(VIM.StringTableBufferName, vim.StringTable.PackStrings().ToBuffer());
            bfastBuilder.Add(VIM.GeometryBufferName, geometryWriter);

            bfastBuilder.Write(vimStream);
        }

        private (VIM, VimGeometryWriter) CreateVimAndGeometryWriter()
        {
            // Instantiate a new VIM object and apply the header we created in the constructor.
            var vim = new VIM()
            {
                Header = VimHeader
            };

            // Convert everything we can into the VIM file.
            // TODO

            // For efficiency, we create a geometryWriter to avoid extra allocations in memory.
            var geometryWriter = new VimGeometryWriter(Meshes, Instances, Materials);

            return (vim, geometryWriter);
        }
    }
}