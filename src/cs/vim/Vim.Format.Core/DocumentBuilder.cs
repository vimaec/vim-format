using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Vim.Math3d;
using Vim.BFastNextNS;
using System.IO;
using Vim.Util;
using Vim.Buffers;
using System.Runtime.InteropServices.ComTypes;
using static Vim.Format.DocumentBuilder;
using Vim.Format.Geometry;

namespace Vim.Format
{
    public partial class DocumentBuilder
    {
        public readonly SerializableHeader Header;
        public readonly Dictionary<string, EntityTableBuilder> Tables = new Dictionary<string, EntityTableBuilder>();
        public readonly Dictionary<string, byte[]> Assets = new Dictionary<string, byte[]>();
        public readonly G3dBuilder Geometry = new G3dBuilder();

        public bool UseColors { get; set; }

        public DocumentBuilder(
            string generator,
            SerializableVersion schema,
            string versionString,
            IReadOnlyDictionary<string, string> optionalHeaderValues = null)
        {
            Header = new SerializableHeader(generator, schema, versionString, optionalHeaderValues);
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

        public DocumentBuilder AddMesh(SubdividedMesh mesh)
        {
            Geometry.AddMesh(mesh);
            return this;
        }

        public DocumentBuilder AddMeshes(IEnumerable<SubdividedMesh> meshes)
        {
            foreach (var m in meshes)
            {
                AddMesh(m);
            }
            return this;
        }

        public DocumentBuilder AddInstances(IEnumerable<Instance> instances)
        {
            foreach (var m in instances)
            {
                Geometry.AddInstance(m);
            }
            return this;
        }

        public DocumentBuilder AddInstance(Matrix4x4 transform, int meshIndex, int parentIndex = -1)
        {
            var instance = new Instance()
            {
                Transform = transform,
                MeshIndex = meshIndex,
                ParentIndex = parentIndex
            };
            Geometry.AddInstance(instance);
            return this;
        }

        public DocumentBuilder AddMaterials(IEnumerable<Material> materials)
        {
            foreach (var material in materials)
            {
                Geometry.AddMaterial(material);
            }
            return this;
        }

        public DocumentBuilder AddShapes(IEnumerable<Shape> shapes)
        {
            foreach (var shape in shapes)
            {
                Geometry.AddShape(shape);
            }
            return this;
        }

        public DocumentBuilder AddAsset(INamedBuffer b)
            => AddAsset(b.Name, b.ToBytes());



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

                // Populate the box
                var boxMinX = new float[Geometry.MeshCount];
                var boxMinY = new float[Geometry.MeshCount];
                var boxMinZ = new float[Geometry.MeshCount];

                var boxMaxX = new float[Geometry.MeshCount];
                var boxMaxY = new float[Geometry.MeshCount];
                var boxMaxZ = new float[Geometry.MeshCount];

                for (var i = 0; i < Geometry.MeshCount; ++i)
                {
                    var b = Geometry.GetBox(i);
                    boxMinX[i] = b.Min.X;
                    boxMinY[i] = b.Min.Y;
                    boxMinZ[i] = b.Min.Z;

                    boxMaxX[i] = b.Max.X;
                    boxMaxY[i] = b.Max.Y;
                    boxMaxZ[i] = b.Max.Z;
                }

                // TODO: TECH DEBT - this couples the object model to the data format.
                tb.AddDataColumn("float:Box.Min.X", boxMinX);
                tb.AddDataColumn("float:Box.Min.Y", boxMinY);
                tb.AddDataColumn("float:Box.Min.Z", boxMinZ);

                tb.AddDataColumn("float:Box.Max.X", boxMaxX);
                tb.AddDataColumn("float:Box.Max.Y", boxMaxY);
                tb.AddDataColumn("float:Box.Max.Z", boxMaxZ);

                tb.AddDataColumn("int:VertexCount", Geometry.GetVertexCounts());
                tb.AddDataColumn("int:FaceCount", Geometry.GetFaceCounts());
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
            ToBFast().Write(filePath);
        }

        public void Write(Stream stream)
        {
            ToBFast().Write(stream);
        }

        public BFastNext ToBFast()
        {
            var assets = Assets.Select(kv => kv.Value.ToNamedBuffer(kv.Key)) as IEnumerable<INamedBuffer>;
            Debug.Assert(assets != null, "Asset conversion to IEnumerable<INamedBuffer> failed.");

            var stringLookupInfo = GetStringLookupInfo();
            var entityTables = ComputeEntityTables(stringLookupInfo.StringLookup);
            var stringTable = stringLookupInfo.StringTable;

            var doc = new SerializableDocument()
            {
                Header = Header,
                Assets = assets.ToArray(),
                StringTable = stringTable.ToArray(),
                EntityTables = entityTables
            };
            var bfast = doc.ToBFast();

            bfast.SetBFast(BufferNames.Geometry, Geometry.ToBFast());

            return bfast;
        }
    }
}
