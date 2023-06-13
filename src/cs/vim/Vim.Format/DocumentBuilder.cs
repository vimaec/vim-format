using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Vim.Math3d;
using Vim.BFast;
using System.IO;
using Vim.DotNetUtilities;
using static Vim.DataFormat.Serializer;

namespace Vim.DataFormat
{
    public partial class DocumentBuilder
    {
        public readonly SerializableHeader Header;
        public readonly Dictionary<string, EntityTableBuilder> Tables = new Dictionary<string, EntityTableBuilder>();
        public readonly Dictionary<string, byte[]> Assets = new Dictionary<string, byte[]>();
        public readonly List<SubdividedMesh> Meshes = new List<SubdividedMesh>();
        public readonly List<Instance> Instances = new List<Instance>();
        public readonly List<Shape> Shapes = new List<Shape>();
        public readonly List<Material> Materials = new List<Material>();

        public bool UseColors { get; set; }

        public DocumentBuilder(
            string generator,
            SerializableVersion schema,
            IReadOnlyDictionary<string, string> optionalHeaderValues = null)
        {
            Header = new SerializableHeader(generator, schema, optionalHeaderValues);
        }

        public EntityTableBuilder GetTableBuilderOrCreate(string name)
        {
            if (!Tables.ContainsKey(name))
                Tables.Add(name, new EntityTableBuilder(name));
            return Tables[name];
        }

        public EntityTableBuilder CreateTableBuilder(string name)
        {
            if (Tables.ContainsKey(name))
                throw new Exception($"Table {name} already exists");
            return GetTableBuilderOrCreate(name);
        }

        public DocumentBuilder AddAsset(string name, byte[] asset)
        {
            if (!Assets.ContainsKey(name))
                Assets.Add(name, asset);
            return this;
        }

        public DocumentBuilder AddMesh(SubdividedMesh g)
        {
            Meshes.Add(g);
            return this;
        }

        public DocumentBuilder AddMeshes(IEnumerable<SubdividedMesh> gb)
        {
            Meshes.AddRange(gb);
            return this;
        }

        public DocumentBuilder AddInstances(IEnumerable<Instance> ib)
        {
            Instances.AddRange(ib);
            return this;
        }
        public DocumentBuilder AddMaterials(IEnumerable<Material> mb)
        {
            Materials.AddRange(mb);
            return this;
        }

        public DocumentBuilder AddShapes(IEnumerable<Shape> sb)
        {
            Shapes.AddRange(sb);
            return this;
        }

        public DocumentBuilder AddAsset(INamedBuffer b)
            => AddAsset(b.Name, b.ToBytes());

        public DocumentBuilder AddInstance(Matrix4x4 transform, int meshIndex, int parentIndex = -1)
        {
            Instances.Add(
                new Instance()
                {
                    Transform = transform,
                    MeshIndex = meshIndex,
                    ParentIndex = parentIndex
                }
            );

            return this;
        }

        public class StringLookupInfo
        {
            public readonly IReadOnlyDictionary<string, int> StringLookup;
            public readonly IEnumerable<string> StringTable;

            public StringLookupInfo(IEnumerable<string> allStrings, int indexOffset = 0)
            {
                // NOTE: ensure the empty string is part of the string table.
                var stringTable = allStrings.Prepend("").Distinct().ToList();

                // By construction, the contents of stringTable should not have repeating items.
                var stringLookup = new Dictionary<string, int>();
                for (var i = 0; i < stringTable.Count; ++i)
                    stringLookup[stringTable[i]] = i + indexOffset;

                StringTable = stringTable;
                StringLookup = stringLookup;
            }
        }

        public static StringLookupInfo GetStringLookupInfo(IEnumerable<EntityTableBuilder> tableBuilders, int indexOffset = 0)
            => new StringLookupInfo(tableBuilders.SelectMany(tb => tb.GetAllStrings()), indexOffset);

        public StringLookupInfo GetStringLookupInfo()
            => GetStringLookupInfo(Tables.Values);

        public List<SerializableEntityTable> ComputeEntityTables(IReadOnlyDictionary<string, int> stringLookup)
        {
            // Create the new Entity tables
            var tableList = new List<SerializableEntityTable>();

            // Create the geometry table 
            {
                var tb = GetTableBuilderOrCreate(TableNames.Geometry);
                tb.Clear();

                // We have to force evaluation because as an enumerable the bounding box could be evaluated 6 times more
                tb.AddCompositeDataColumns("Box", Meshes.Select(g => AABox.Create(g.Vertices)));
                tb.AddDataColumn("int:VertexCount", Meshes.Select(g => g.Vertices.Count));
                tb.AddDataColumn("int:FaceCount", Meshes.Select(g => g.Indices.Count / 3));
            }

            // TODO: add bounding box information to the nodes 

            foreach (var tb in Tables.Values)
            {
                var table = tb.ToSerializableEntityTable(stringLookup);
                tableList.Add(table);
            }

            return tableList;
        }

        public void Write(string filePath)
        {
            Util.CreateFileDirectory(filePath);
            using (var stream = File.OpenWrite(filePath))
                Write(stream);
        }

        public void Write(Stream stream)
        {
            var assets = Assets.Select(kv => kv.Value.ToNamedBuffer(kv.Key)) as IEnumerable<INamedBuffer>;
            Debug.Assert(assets != null, "Asset conversion to IEnumerable<INamedBuffer> failed.");

            var stringLookupInfo = GetStringLookupInfo();
            var entityTables = ComputeEntityTables(stringLookupInfo.StringLookup);
            var stringTable = stringLookupInfo.StringTable;

            Serialize(stream, Header, assets, stringTable, entityTables, new BigG3dWriter(Meshes, Instances, Shapes, Materials, null, UseColors));
        }
    }
}
