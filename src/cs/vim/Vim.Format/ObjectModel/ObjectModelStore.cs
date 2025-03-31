using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Vim.Math3d;

namespace Vim.Format.ObjectModel
{
    /// <summary>
    /// Stores converted object models encountered during the Export process.
    /// </summary>
    public class ObjectModelStore
    {
        /// <summary>
        /// A Utility for building the ORM (Object Relational Model)
        /// </summary>
        public ObjectModelBuilder ObjectModelBuilder = new ObjectModelBuilder();

        public List<DocumentBuilder.Mesh> Meshes { get; private set; } = new List<DocumentBuilder.Mesh>();
        public List<DocumentBuilder.Instance> Instances { get; } = new List<DocumentBuilder.Instance>();
        public List<DocumentBuilder.Shape> Shapes { get; } = new List<DocumentBuilder.Shape>();

        public DocumentBuilder ToDocumentBuilder(string generator, string versionString)
        {
            var db = ObjectModelBuilder.ToDocumentBuilder(generator, versionString);
            AddEntityTableBuffers(db);

            return db
                .AddMeshes(Meshes.Select(g => g.Subdivide()))
                .AddInstances(Instances)
                .AddShapes(Shapes)
                .AddMaterials(CreateMaterialBuilders());
        }

        public List<int> FaceMeshIndexBuffer { get; } = new List<int>();
        public List<Vector3> FaceMeshVertexBuffer { get; } = new List<Vector3>();

        public List<Vector3> LineShapeVertexBuffer { get; } = new List<Vector3>();

        private void AddEntityTableBuffers(DocumentBuilder documentBuilder)
        {
#if DEBUG
            // Validate the index buffer and the vertex buffer
            foreach (var index in FaceMeshIndexBuffer)
                Debug.Assert(index >= 0 && index < FaceMeshVertexBuffer.Count);
#endif

            // Add the face mesh index buffer.
            {
                var tb = new EntityTableBuilder(TableNames.MeshIndexBuffer);
                tb.AddIndexColumn($"index:{TableNames.MeshVertexBuffer}:{nameof(ObjectModel.MeshIndexList.VertexIndex)}", FaceMeshIndexBuffer);
                documentBuilder.Tables.Add(tb.Name, tb);
            }

            // Add the face mesh vertex buffer.
            {
                var tb = new EntityTableBuilder(TableNames.MeshVertexBuffer);
                tb.AddDataColumn($"vector3:{nameof(ObjectModel.MeshVertexList.Vertex)}", FaceMeshVertexBuffer);
                documentBuilder.Tables.Add(tb.Name, tb);
            }

            // Add the line shape vertex buffer.
            {
                var tb = new EntityTableBuilder(TableNames.LineShapeVertexBuffer);
                tb.AddDataColumn($"vector3:{nameof(ObjectModel.LineShapeVertexBuffer.Vertex)}", LineShapeVertexBuffer);
                documentBuilder.Tables.Add(tb.Name, tb);
            }
        }

        private IEnumerable<DocumentBuilder.Material> CreateMaterialBuilders()
            => ObjectModelBuilder.GetEntities<Material>().Select(ConvertMaterialEntityToRenderableMaterial);

        public static DocumentBuilder.Material ConvertMaterialEntityToRenderableMaterial(Material m)
            => ConvertMaterialEntityFieldsToRenderableMaterial(
                m.Color.X,
                m.Color.Y,
                m.Color.Z,
                m.Transparency,
                m.Glossiness,
                m.Smoothness);

        public static DocumentBuilder.Material ConvertMaterialEntityFieldsToRenderableMaterial(
            double colorX,
            double colorY,
            double colorZ,
            double transparency,
            double glossiness,
            double smoothness)
            => new DocumentBuilder.Material()
            {
                Color = new Vector4(
                    (float) colorX,
                    (float) colorY,
                    (float) colorZ,
                    (float)(1 - transparency)), // TECH DEBT: rendered material value is 1.0f - transparency
                Glossiness = (float) glossiness,
                Smoothness = (float) smoothness
            };

        /// <summary>
        /// Mutates the Meshes and Instances to remove any meshes which are not referenced by at least one instance.
        /// </summary>
        public ObjectModelStore TrimOrphanMeshes()
        {
            // Example:
            //
            // old instance mesh indices:  [0,  2,  4,  2]
            // ---
            // old mesh indices:           [0,  1,  2,  3,  4]
            // orphan mesh indices:        [    1,      3    ]
            // next mesh indices:          [0, -1,  1, -1,  2]
            // ---
            // next instance mesh indices: [0,  1,  2,  1]

            const int nullMeshIndex = -1;

            // Initialize the mesh indices
            var meshIsReferenced = new bool[Meshes.Count];
            for (var i = 0; i < meshIsReferenced.Length; i++)
                meshIsReferenced[i] = false;

            // Mark the mesh indices which are referenced by an instance.
            foreach (var instance in Instances)
            {
                if (instance.MeshIndex <= nullMeshIndex)
                    continue;

                meshIsReferenced[instance.MeshIndex] = true;
            }

            // Early exit if all meshes are referenced.
            if (meshIsReferenced.All(isReferenced => isReferenced))
                return this;

            // Update the new mesh indices.
            var nextMeshIndex = 0;
            var nextMeshIndices = new int[meshIsReferenced.Length];
            for (var i = 0; i < nextMeshIndices.Length; ++i)
            {
                nextMeshIndices[i] = meshIsReferenced[i]
                    ? nextMeshIndex++
                    : nullMeshIndex;
            }

            // Create a new mesh list which excludes the orphaned meshes.
            Meshes = Meshes.Where((m, i) => nextMeshIndices[i] > nullMeshIndex).ToList();

            // Mutate the instances to update their mesh index.
            foreach (var instance in Instances)
            {
                if (instance.MeshIndex <= nullMeshIndex)
                    continue;

                instance.MeshIndex = nextMeshIndices[instance.MeshIndex];
                Debug.Assert(instance.MeshIndex > nullMeshIndex, $"Invalid instance mesh index ({instance.MeshIndex})");
            }

            return this;
        }
    }
}
