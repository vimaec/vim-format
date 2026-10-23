using System;
using System.Collections.Generic;
using System.Linq;
using Vim.G3d;
using Vim.BFast;
using System.IO;
using static Vim.Format.DocumentBuilder;

namespace Vim.Format
{
    /// <summary>
    /// This is a helper class for writing the really big G3Ds needed in a VIM
    /// </summary>
    public class BigG3dWriter : IBFastComponent
    {
        public INamedBuffer Meta { get; }
        public string[] Names { get; }
        public long[] Sizes { get; }
        public BFastHeader Header { get; }
        public List<SubdividedMesh> Meshes { get; }
        public List<Instance> Instances { get; }
        public List<Shape> Shapes { get; }
        public List<Material> Materials { get; }

        /// <summary>
        /// The Vim.Category (usually a Revit subcategory) of each submesh, parallel to g3d:submesh:material. Written only when
        /// at least one mesh carries categories (exporter options StoreSubcategoryPerPart or geometry pieces). -1 = unknown /
        /// the element's own category. Readers that do not know this attribute ignore it. Not in the generated
        /// CommonAttributes list (CommonAttributes.tt) yet.
        /// </summary>
        public const string SubmeshCategoryAttribute = "g3d:submesh:category:0:int32:1";

        public bool WriteSubmeshCategories { get; }

        // Computed fields
        public int[] MeshVertexOffsets { get; }
        public int[] MeshIndexOffsets { get; }
        public int[] MeshSubmeshOffset { get; }
        public int[] SubmeshIndexOffsets { get; }
        public int[] ShapeVertexOffsets { get; }
        public bool HasVertexUvs { get; }

        public BigG3dWriter(List<SubdividedMesh> meshes, List<Instance> instances, List<Shape> shapes, List<Material> materials, G3dHeader? header = null, bool useColors = false)
        {
            Meshes = meshes;
            Instances = instances;
            Shapes = shapes;
            Materials = materials;
            var totalSubmeshCount = meshes.Select(s => s.SubmeshesIndexOffset.Count).Sum();

            // Compute the Vertex offsets and index offsets 
            MeshVertexOffsets = new int[meshes.Count];
            MeshIndexOffsets = new int[meshes.Count];
            SubmeshIndexOffsets = new int[totalSubmeshCount];
            MeshSubmeshOffset = new int[meshes.Count];

            var n = meshes.Count;

            for (var i = 1; i < n; ++i)
            {
                MeshVertexOffsets[i] = MeshVertexOffsets[i - 1] + meshes[i - 1].Vertices.Count;
                MeshIndexOffsets[i] = MeshIndexOffsets[i - 1] + meshes[i - 1].Indices.Count;
                MeshSubmeshOffset[i] = MeshSubmeshOffset[i - 1] + meshes[i - 1].SubmeshesIndexOffset.Count;
            }

            var subIndex =0;
            var previousIndexCount = 0;
            foreach(var geo in meshes)
            {
                foreach(var sub in geo.SubmeshesIndexOffset)
                {
                    SubmeshIndexOffsets[subIndex++] = sub + previousIndexCount;
                }
                previousIndexCount += geo.Indices.Count;
            }

            var submeshCount = meshes.Select(s => s.SubmeshesIndexOffset.Count).Sum();

            var totalVertices = n == 0 ? 0 : MeshVertexOffsets[n - 1] + meshes[n - 1].Vertices.Count;
            var totalIndices = n == 0 ? 0 : MeshIndexOffsets[n - 1] + meshes[n - 1].Indices.Count;
            long totalFaces = totalIndices / 3;

            // Compute the shape vertex offsets
            var numShapes = shapes.Count;
            ShapeVertexOffsets = new int[numShapes];
            for (var i = 1; i < numShapes; ++i)
            {
                ShapeVertexOffsets[i] = ShapeVertexOffsets[i - 1] + shapes[i - 1].Vertices.Count;
            }
            var numShapeVertices = numShapes == 0 ? 0 : ShapeVertexOffsets[numShapes - 1] + shapes[numShapes - 1].Vertices.Count;

            Meta = (header ?? G3dHeader.Default).ToBytes().ToNamedBuffer("meta");

            (long size, string name) AttributeSizeAndName(string attributeName, long count)
                => (AttributeDescriptor.Parse(attributeName).DataElementSize * count, attributeName);

            var writers = new List<(long size, string attribute)>()
            {
                (Meta.NumBytes(), Meta.Name),
                AttributeSizeAndName(CommonAttributes.Position, totalVertices),
                AttributeSizeAndName(CommonAttributes.Index, totalIndices),

                AttributeSizeAndName(CommonAttributes.MeshSubmeshOffset, meshes.Count),
                AttributeSizeAndName(CommonAttributes.SubmeshIndexOffset, submeshCount),
                AttributeSizeAndName(CommonAttributes.SubmeshMaterial, submeshCount),

                AttributeSizeAndName(CommonAttributes.InstanceTransform, instances.Count),
                AttributeSizeAndName(CommonAttributes.InstanceParent, instances.Count),
                AttributeSizeAndName(CommonAttributes.InstanceMesh, instances.Count),
                AttributeSizeAndName(CommonAttributes.InstanceFlags, instances.Count),

                AttributeSizeAndName(CommonAttributes.ShapeVertex, numShapeVertices),
                AttributeSizeAndName(CommonAttributes.ShapeVertexOffset, numShapes),
                AttributeSizeAndName(CommonAttributes.ShapeColor, numShapes),
                AttributeSizeAndName(CommonAttributes.ShapeWidth, numShapes),

                AttributeSizeAndName(CommonAttributes.MaterialColor, materials.Count),
                AttributeSizeAndName(CommonAttributes.MaterialGlossiness, materials.Count),
                AttributeSizeAndName(CommonAttributes.MaterialSmoothness, materials.Count),
            };

            WriteSubmeshCategories = meshes.Any(m => m.SubmeshCategories != null);
            if (WriteSubmeshCategories)
                writers.Add(AttributeSizeAndName(SubmeshCategoryAttribute, submeshCount));

            if (useColors)
            {
                writers.Add(AttributeSizeAndName(CommonAttributes.VertexColor, totalVertices));
            }

            // Texture coordinates: written only when at least one mesh carries them (the exporter's experimental
            // "Textures" option). The buffer always spans every vertex; meshes without UVs get (0, 0).
            HasVertexUvs = meshes.Any(m => m.UVs != null);
            if (HasVertexUvs)
            {
                writers.Add(AttributeSizeAndName(CommonAttributes.VertexUv, totalVertices));
            }

            Names = writers.Select(w => w.attribute).ToArray();
            Sizes = writers.Select(w => w.size).ToArray();
            Header = BFast.BFast.CreateBFastHeader(Sizes, Names);
        }

        public long GetSize()
            => BFast.BFast.ComputeNextAlignment(Header.Preamble.DataEnd);

        public void Write(Stream stream)
        {
            // TODO: validate in debug mode that this is producing the current data model. Look at the schema!

            stream.WriteBFastHeader(Header);
            stream.WriteBFastBody(Header, Names, Sizes, (_stream, index, name, size) =>
            {
                switch (name)
                {
                    case "meta":
                        _stream.Write(Meta);
                        break;

                    // Vertices
                    case CommonAttributes.Position:
                        Meshes.ForEach(g => stream.Write(g.Vertices.ToArray()));
                        break;

                    // Texture coordinates (one per vertex, zeros for meshes without UVs)
                    case CommonAttributes.VertexUv:
                        Meshes.ForEach(g => stream.Write(g.UVs != null
                            ? g.UVs.ToArray()
                            : new Math3d.Vector2[g.Vertices.Count]));
                        break;

                    // Indices
                    case CommonAttributes.Index:
                        for (var i = 0; i < Meshes.Count; ++i)
                        {
                            var g = Meshes[i];
                            var offset = MeshVertexOffsets[i];
                            stream.Write(g.Indices.Select(idx => idx + offset).ToArray());
                        }
                        break;

                    // Meshes
                    case CommonAttributes.MeshSubmeshOffset:
                        stream.Write(MeshSubmeshOffset);
                        break;

                    // Instances
                    case CommonAttributes.InstanceMesh:
                        stream.Write(Instances.Select(i => i.MeshIndex).ToArray());
                        break;
                    case CommonAttributes.InstanceTransform:
                        stream.Write(Instances.Select(i => i.Transform).ToArray());
                        break;
                    case CommonAttributes.InstanceParent:
                        stream.Write(Instances.Select(i => i.ParentIndex).ToArray());
                        break;
                    case CommonAttributes.InstanceFlags:
                        stream.Write(Instances.Select(i => (ushort) i.InstanceFlags).ToArray());
                        break;

                    // Shapes
                    case CommonAttributes.ShapeVertex:
                        stream.Write(Shapes.SelectMany(s => s.Vertices).ToArray());
                        break;
                    case CommonAttributes.ShapeVertexOffset:
                        stream.Write(ShapeVertexOffsets);
                        break;
                    case CommonAttributes.ShapeColor:
                        stream.Write(Shapes.Select(s => s.Color).ToArray());
                        break;
                    case CommonAttributes.ShapeWidth:
                        stream.Write(Shapes.Select(s => s.Width).ToArray());
                        break;

                    // Materials
                    case CommonAttributes.MaterialColor:
                        stream.Write(Materials.Select(i => i.Color).ToArray());
                        break;
                    case CommonAttributes.MaterialGlossiness:
                        stream.Write(Materials.Select(i => i.Glossiness).ToArray());
                        break;
                    case CommonAttributes.MaterialSmoothness:
                        stream.Write(Materials.Select(i => i.Smoothness).ToArray());
                        break;

                    // Submeshes
                    case CommonAttributes.SubmeshIndexOffset:
                        stream.Write(SubmeshIndexOffsets);
                        break;
                    case CommonAttributes.SubmeshMaterial:
                        stream.Write(Meshes.SelectMany(s => s.SubmeshMaterials).ToArray());
                        break;
                    case SubmeshCategoryAttribute:
                        stream.Write(Meshes.SelectMany(s => s.SubmeshCategories ?? Enumerable.Repeat(-1, s.SubmeshMaterials.Count)).ToArray());
                        break;
                    default:
                        throw new Exception($"Not a recognized geometry buffer: {name}");
                }
                return size;
            });
        }
    }
}
