using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Vim.DataFormat;
using Vim.Math3d;

namespace Vim.ObjectModel
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

        public DocumentBuilder ToDocumentBuilder(string generator)
        {
            return ObjectModelBuilder.ToDocumentBuilder(generator)
                .AddMeshes(Meshes.Select(g => g.Subdivide()))
                .AddInstances(Instances)
                .AddShapes(Shapes)
                .AddMaterials(CreateMaterialBuilders());
        }

        private IEnumerable<DocumentBuilder.Material> CreateMaterialBuilders()
            => ObjectModelBuilder.GetEntities<Material>().Select(m => new DocumentBuilder.Material()
            {
                Color = new Vector4((float) m.Color.X, (float) m.Color.Y, (float) m.Color.Z, (float)(1 - m.Transparency)),
                Glossiness = (float) m.Glossiness,
                Smoothness = (float) m.Smoothness
            });

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
