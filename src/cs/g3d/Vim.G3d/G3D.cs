/*
    G3D Geometry Format Library
    Copyright 2019, VIMaec LLC.
    Copyright 2018, Ara 3D Inc.
    Usage licensed under terms of MIT License
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.Math3d;
using Vim.BFastLib;
using System.Diagnostics;

namespace Vim.G3d
{
    /// <summary>
    /// Represents a basic single-precision G3D in memory, with access to common attributes.
    /// The G3D format can be double precision, but this data structure won't provide access to all of the attributes.
    /// In the case of G3D formats that are non-conformant to the expected semantics you can use GeometryAttributes.
    /// This class is inspired heavily by the structure of FBX and Assimp. 
    /// </summary>
    public class G3D : GeometryAttributes
    {
        public new static G3D Empty = Create();

        public G3dHeader Header { get; }

        // These are the values of the most common attributes. Some are retrieved directly from data, others are computed on demand, or coerced. 

        // Vertex buffer. Usually present.
        public IList<Vector3> Vertices { get; }

        // Index buffer (one index per corner, and per half-edge). Computed if absent. 
        public IList<int> Indices { get; }

        // Vertex associated data, provided or null
        public List<IList<Vector2>> AllVertexUvs { get; } = new List<IList<Vector2>>();
        public List<IList<Vector4>> AllVertexColors { get; } = new List<IList<Vector4>>();
        public IList<Vector2> VertexUvs => AllVertexUvs?.ElementAtOrDefault(0);
        public IList<Vector4> VertexColors => AllVertexColors?.ElementAtOrDefault(0);
        public IList<Vector3> VertexNormals { get; }
        public IList<Vector4> VertexTangents { get; }

        // Faces
        public IList<int> FaceMaterials { get; } // Material indices per face, 
        public IList<Vector3> FaceNormals { get; } // If not provided, are computed dynamically as the average of all vertex normals,

        // Meshes
        public IList<int> MeshIndexOffsets { get; } // Offset into the index buffer for each Mesh
        public IList<int> MeshVertexOffsets { get; } // Offset into the vertex buffer for each Mesh
        public IList<int> MeshIndexCounts { get; } // Computed
        public IList<int> MeshVertexCounts { get; } // Computed
        public IList<int> MeshSubmeshOffset { get; }
        public IList<int> MeshSubmeshCount { get; } // Computed
        public IList<G3dMesh> Meshes { get; }

        // Instances
        public IList<int> InstanceParents { get; } // Index of the parent transform 
        public IList<Matrix4x4> InstanceTransforms { get; } // A 4x4 matrix in row-column order defining the transormed
        public IList<int> InstanceMeshes { get; } // The SubGeometry associated with the index
        public IList<ushort> InstanceFlags { get; } // The instance flags associated with the index.

        // Shapes
        public IList<Vector3> ShapeVertices { get; }
        public IList<int> ShapeVertexOffsets { get; }
        public IList<Vector4> ShapeColors { get; }
        public IList<float> ShapeWidths { get; }
        public IList<int> ShapeVertexCounts { get; } // Computed
        public IList<G3dShape> Shapes { get; } // Computed

        // Materials
        public IList<Vector4> MaterialColors { get; } // RGBA with transparency.
        public IList<float> MaterialGlossiness { get; }
        public IList<float> MaterialSmoothness { get; }
        public IList<G3dMaterial> Materials { get; }


        // Submeshes
        public IList<int> SubmeshIndexOffsets { get; }
        public IList<int> SubmeshIndexCount { get; }
        public IList<int> SubmeshMaterials { get; }

        public G3D(IEnumerable<GeometryAttribute> attributes, G3dHeader? header = null, int numCornersPerFaceOverride = -1)
            : base(attributes, numCornersPerFaceOverride)
        {
            Header = header ?? new G3dHeader();

            foreach (var attr in Attributes)
            {
                var desc = attr.Descriptor;
                switch (desc.Semantic)
                {
                    case Semantic.Index:
                        if (attr.IsTypeAndAssociation<int>(Association.assoc_corner))
                            Indices = Indices ?? attr.AsType<int>().Data;
                        if (attr.IsTypeAndAssociation<short>(Association.assoc_corner))
                            Indices = Indices ?? attr.AsType<short>().Data.Select(x => (int)x).ToArray();
                        break;

                    case Semantic.Position:
                        if (attr.IsTypeAndAssociation<Vector3>(Association.assoc_vertex))
                            Vertices = Vertices ?? attr.AsType<Vector3>().Data;
                        if (attr.IsTypeAndAssociation<Vector3>(Association.assoc_corner))
                            Vertices = Vertices ?? attr.AsType<Vector3>().Data; // TODO: is this used?
                        if (attr.IsTypeAndAssociation<Vector3>(Association.assoc_shapevertex))
                            ShapeVertices = ShapeVertices ?? attr.AsType<Vector3>().Data;
                        break;

                    case Semantic.Tangent:
                        if (attr.IsTypeAndAssociation<Vector3>(Association.assoc_vertex))
                            VertexTangents = VertexTangents ?? attr.AsType<Vector3>().Data.Select(v => v.ToVector4()).ToArray();
                        if (attr.IsTypeAndAssociation<Vector4>(Association.assoc_vertex))
                            VertexTangents = VertexTangents ?? attr.AsType<Vector4>().Data;
                        break;

                    case Semantic.Uv:
                        if (attr.IsTypeAndAssociation<Vector3>(Association.assoc_vertex))
                            AllVertexUvs.Add(attr.AsType<Vector3>().Data.Select(uv => uv.ToVector2()).ToArray());
                        if (attr.IsTypeAndAssociation<Vector2>(Association.assoc_vertex))
                            AllVertexUvs.Add(attr.AsType<Vector2>().Data);
                        break;

                    case Semantic.Color:
                        if (desc.Association == Association.assoc_vertex)
                            AllVertexColors.Add(attr.AttributeToColors());
                        if (desc.Association == Association.assoc_shape)
                            ShapeColors = ShapeColors ?? attr.AttributeToColors();
                        if (desc.Association == Association.assoc_material)
                            MaterialColors = MaterialColors ?? attr.AttributeToColors();
                        break;

                    case Semantic.VertexOffset:
                        if (attr.IsTypeAndAssociation<int>(Association.assoc_mesh))
                            MeshVertexOffsets = MeshVertexOffsets ?? attr.AsType<int>().Data;
                        if (attr.IsTypeAndAssociation<int>(Association.assoc_shape))
                            ShapeVertexOffsets = ShapeVertexOffsets ?? attr.AsType<int>().Data;
                        break;

                    case Semantic.IndexOffset:
                        if (attr.IsTypeAndAssociation<int>(Association.assoc_mesh))
                            MeshIndexOffsets = MeshIndexOffsets ?? attr.AsType<int>().Data;
                        if (attr.IsTypeAndAssociation<int>(Association.assoc_submesh))
                            SubmeshIndexOffsets = SubmeshIndexOffsets ?? attr.AsType<int>().Data;
                        break;

                    case Semantic.Normal:
                        if (attr.IsTypeAndAssociation<Vector3>(Association.assoc_face))
                            FaceNormals = FaceNormals ?? attr.AsType<Vector3>().Data;
                        if (attr.IsTypeAndAssociation<Vector3>(Association.assoc_vertex))
                            VertexNormals = VertexNormals ?? attr.AsType<Vector3>().Data;
                        break;

                    case Semantic.Material:
                        if (attr.IsTypeAndAssociation<int>(Association.assoc_face))
                            FaceMaterials = FaceMaterials ?? attr.AsType<int>().Data;
                        if (attr.IsTypeAndAssociation<int>(Association.assoc_submesh))
                            SubmeshMaterials = SubmeshMaterials ?? attr.AsType<int>().Data;
                        break;

                    case Semantic.Parent:
                        if (attr.IsTypeAndAssociation<int>(Association.assoc_instance))
                            InstanceParents = InstanceParents ?? attr.AsType<int>().Data;
                        break;

                    case Semantic.Mesh:
                        if (attr.IsTypeAndAssociation<int>(Association.assoc_instance))
                            InstanceMeshes = InstanceMeshes ?? attr.AsType<int>().Data;
                        break;

                    case Semantic.Transform:
                        if (attr.IsTypeAndAssociation<Matrix4x4>(Association.assoc_instance))
                            InstanceTransforms = InstanceTransforms ?? attr.AsType<Matrix4x4>().Data;
                        break;

                    case Semantic.Width:
                        if (attr.IsTypeAndAssociation<float>(Association.assoc_shape))
                            ShapeWidths = ShapeWidths ?? attr.AsType<float>().Data;
                        break;

                    case Semantic.Glossiness:
                        if (attr.IsTypeAndAssociation<float>(Association.assoc_material))
                            MaterialGlossiness = attr.AsType<float>().Data;
                        break;

                    case Semantic.Smoothness:
                        if (attr.IsTypeAndAssociation<float>(Association.assoc_material))
                            MaterialSmoothness = attr.AsType<float>().Data;
                        break;

                    case Semantic.SubMeshOffset:
                        if (attr.IsTypeAndAssociation<int>(Association.assoc_mesh))
                            MeshSubmeshOffset = attr.AsType<int>().Data;
                        break;

                    case Semantic.Flags:
                        if (attr.IsTypeAndAssociation<ushort>(Association.assoc_instance))
                            InstanceFlags = attr.AsType<ushort>().Data;
                        break;
                }
            }

            // If no vertices are provided, we are going to generate a list of zero vertices.
            if (Vertices == null)
                Vertices = Vector3.Zero.Repeat(0);

            // If no indices are provided then we are going to have to treat the index buffer as indices
            if (Indices == null)
                Indices = Vertices.Indices().ToArray();

            // Compute face normals if possible
            if (FaceNormals == null && VertexNormals != null)
                FaceNormals = NumFaces.Select(ComputeFaceNormal);

            if (NumMeshes > 0)
            {
                // Mesh offset is the same as the offset of its first submesh.
                if(MeshSubmeshOffset != null)
                {
                    MeshIndexOffsets = MeshSubmeshOffset.Select(submesh => SubmeshIndexOffsets[submesh]).ToArray();
                    MeshSubmeshCount = GetSubArrayCounts(MeshSubmeshOffset.Count, MeshSubmeshOffset, NumSubmeshes);
                }

                if(MeshIndexOffsets != null)
                {
                    MeshIndexCounts = GetSubArrayCounts(NumMeshes, MeshIndexOffsets, NumCorners);
                    MeshVertexOffsets = MeshIndexOffsets
                        .Zip(MeshIndexCounts, (start, count) => (start, count))
                        .Select(range => Indices.SubArray(range.start, range.count).Min())
                        .ToArray();
                }
        
                if (MeshVertexOffsets != null)
                    MeshVertexCounts = GetSubArrayCounts(NumMeshes, MeshVertexOffsets, NumVertices);
            }
            else
            {
                MeshSubmeshCount = Array.Empty<int>();
            }

            if (SubmeshIndexOffsets != null)
                SubmeshIndexCount = GetSubArrayCounts(SubmeshIndexOffsets.Count, SubmeshIndexOffsets, NumCorners);

            // Compute all meshes
            Meshes = NumMeshes.Select(i => new G3dMesh(this, i));

            if (MaterialColors != null)
                Materials = MaterialColors.Count.Select(i => new G3dMaterial(this, i));

            // Process the shape data
            if (ShapeVertices == null)
                ShapeVertices = Vector3.Zero.Repeat(0);

            if (ShapeVertexOffsets == null)
                ShapeVertexOffsets = Array.Empty<int>();

            if (ShapeColors == null)
                ShapeColors = Vector4.Zero.Repeat(0);
             
            if (ShapeWidths == null)
                ShapeWidths = Array.Empty<float>();

            // Update the instance options
            if (InstanceFlags == null)
                InstanceFlags = ((ushort) 0).Repeat(NumInstances);

            ShapeVertexCounts = GetSubArrayCounts(NumShapes, ShapeVertexOffsets, ShapeVertices.Count);
            ValidateSubArrayCounts(ShapeVertexCounts, nameof(ShapeVertexCounts));

            Shapes = NumShapes.Select(i => new G3dShape(this, i));
        }

        private static IList<int> GetSubArrayCounts(int numItems, IList<int> offsets, int totalCount)
            => numItems.Select(i => i < (numItems - 1)
                ? offsets[i + 1] - offsets[i]
                : totalCount - offsets[i]);

        private static void ValidateSubArrayCounts(IList<int> subArrayCounts, string memberName)
        {
            for (var i = 0; i < subArrayCounts.Count; ++i)
            {
                if (subArrayCounts[i] < 0)
                    throw new Exception($"{memberName}[{i}] is a negative sub array count.");
            }
        }

        private static Vector3 Average(IList<Vector3> xs)
            => xs.Aggregate(Vector3.Zero, (a, b) => a + b) / xs.Count;

        private Vector3 ComputeFaceNormal(int nFace)
            => Average(NumCornersPerFace.Select(c => VertexNormals[nFace * NumCornersPerFace + c]));

        public static G3D Read(string filePath)
        {
            using (var stream = File.OpenRead(filePath))
            {
                var bfast = new BFast(stream);
                return Read(bfast);
            }
        }


        public static G3D Read(BFast bfast)
        {
            var header = G3dHeader.FromBytesOrDefault(bfast.GetArray<byte>("meta"));
            var attributes = new List<GeometryAttribute>();
            foreach (var name in bfast.Entries)
            {
                if (name == "meta") continue;
                var attribute = GetEmptyAttribute(name);
                if (attribute == null) continue;
                var a = attribute.Read(bfast);
                attributes.Add(a);
            }

            return new G3D(attributes, header);
        }
        private static GeometryAttribute GetEmptyAttribute(string name)
        {
            if (!AttributeDescriptor.TryParse(name, out var attributeDescriptor))
            {
                Debug.WriteLine("G3D Error: Could not parse attribute " + name);
                return null;
            }
            try
            {
                return attributeDescriptor.ToDefaultAttribute(0);
            }
            catch
            {
                Debug.WriteLine("G3D Error: Could not parse attribute " + name);
                return null;
            }
        }


        public static G3D Create(params GeometryAttribute[] attributes)
            => new G3D(attributes);

        public static G3D Create(G3dHeader header, params GeometryAttribute[] attributes)
            => new G3D(attributes, header);

    }
}
