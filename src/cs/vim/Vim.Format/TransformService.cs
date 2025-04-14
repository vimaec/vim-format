using System.Collections.Generic;
using System.Linq;
using Vim.Format.Geometry;
using Vim.LinqArray;
using Vim.Math3d;
using Vim.Format.ObjectModel;
using Vim.Util;

namespace Vim.Format
{
    public class TransformService
    {
        // TODO - TECH DEBT: refactor generatorString + versionString into a common service pattern and apply to MergeService as well.

        /// <summary>
        /// A string representing the application which is emitting the new VIM document.
        /// </summary>
        private readonly string _generatorString;

        /// <summary>
        /// The version of the application which is emitting the new VIM document.
        /// </summary>
        private readonly string _versionString;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="generatorString">A string representing the application which is emitting the new VIM document.</param>
        /// <param name="versionString">The version of the application which is emitting the new VIM document.</param>
        public TransformService(string generatorString, string versionString)
        {
            _generatorString = generatorString;
            _versionString = versionString;
        }

        public delegate bool NodeFilter(VimSceneNode node);
        public delegate Matrix4x4 NodeTransform(VimSceneNode node);
        public delegate bool MeshFilter(IMesh mesh);
        public delegate IMesh MeshTransform(IMesh mesh);

        /// <summary>
        /// Transforms the VIM into a new VIM document builder based on the given filters and transformations.
        /// </summary>
        /// <param name="vim">The VIM to transform into a new VIM document builder.</param>
        /// <param name="nodeFilter">Returns true if the given node should be present in the new VIM document builder.</param>
        /// <param name="meshFilter">Returns true if the given mesh should be present in the new VIM document builder.</param>
        /// <param name="nodeTransform">Returns the node's new transform (or its current transform if left unchanged).</param>
        /// <param name="meshTransform">Returns the mesh's new mesh (or its current mesh if left unchanged).</param>        
        /// <param name="deduplicateMeshes">Determines whether the mesh deduplication process is applied.</param>
        public DocumentBuilder Transform(
            VimScene vim,
            NodeFilter nodeFilter,
            MeshFilter meshFilter,
            NodeTransform nodeTransform,
            MeshTransform meshTransform,
            bool deduplicateMeshes)
        {
            var db = new DocumentBuilder(_generatorString, SchemaVersion.Current, _versionString);

            // Filter the nodes.
            var filteredNodes = new List<VimSceneNode>();
            var vimNodes = vim.VimNodes;
            for (var i = 0; i < vimNodes.Count; ++i)
            {
                var n = vimNodes[i];
                if (nodeFilter?.Invoke(n) ?? true)
                    filteredNodes.Add(n);
            }

            // Filter the meshes.
            var filteredMeshes = new List<IMesh>();
            foreach (var n in filteredNodes)
            {
                var mesh = n.GetMesh();
                if (mesh == null || mesh.NumFaces == 0)
                    continue;

                if (meshFilter?.Invoke(mesh) ?? true)
                    filteredMeshes.Add(mesh);
            }

            var meshLookup = new Dictionary<IMesh, IMesh>();
            var meshIndices = new IndexedSet<IMesh>();

            if (deduplicateMeshes)
            {
                // Group meshes according to a hash function
                const float tolerance = 1f / 12f / 8f;
                var groupedMeshes = filteredMeshes.GroupMeshesByHash(tolerance);

                // Create the lookup from old mesh to new mesh 
                meshLookup = groupedMeshes
                    .SelectMany(grp => grp.Value.Select(m => (m, grp.Key.Mesh)))
                    .ToDictionaryIgnoreDuplicates(pair => pair.m, pair => pair.Mesh);
            }
            else
            {
                // Create a mapping from mesh to mesh  
                foreach (var m in filteredMeshes)
                    meshLookup.AddIfNotPresent(m, m);
            }

            // The indexed set of meshes, is only the values
            foreach (var m in meshLookup.Values)
                meshIndices.Add(m);

            // Add assets
            foreach (var asset in vim.Document.Assets.Values.ToEnumerable())
                db.AddAsset(asset);

            // Add the transformed meshes.
            var subdividedMeshes = meshIndices.OrderedKeys.Select(m => (meshTransform?.Invoke(m) ?? m).ToDocumentBuilderSubdividedMesh());
            db.AddMeshes(subdividedMeshes);

            // Add the materials. TODO: this could be improved to remove duplicate materials, but this would require a remapping among the material entities as well.
            var materials = vim.Document.Geometry.Materials.Select(m => m.ToDocumentBuilderMaterial()).ToEnumerable();
            db.AddMaterials(materials);

            // Add nodes
            var nodeIndexRemapping = new List<int>();
            foreach (var node in filteredNodes)
            {
                var g = meshLookup.GetOrDefaultAllowNulls(node.GetMesh());
                var meshIndex = meshIndices.GetOrDefaultAllowNulls(g, -1);
                var transform = nodeTransform?.Invoke(node) ?? node.Transform;
                nodeIndexRemapping.Add(node.Id);
                db.AddInstance(transform, meshIndex);
            }

            // Copy the tables
            db.CopyTablesFrom(vim.Document, nodeIndexRemapping);

            // Construct the document 
            return db;
        }

        /// <summary>
        /// Returns a new VIM document builder in which the original meshes have been deduplicated.
        /// </summary>
        public DocumentBuilder DeduplicateGeometry(VimScene vim)
            => Transform(
                vim,
                _ => true,
                _ => true,
                n => n.Transform,
                m => m,
                true);

        /// <summary>
        /// Returns a new VIM document builder in which the nodes have been filtered.
        /// </summary>
        public DocumentBuilder Filter(VimScene vim, NodeFilter filter, bool deduplicateMeshes = false)
            => Transform(
                vim, 
                filter,
                _ => true,
                n => n.Transform,
                m => m,
                deduplicateMeshes);

        /// <summary>
        /// Returns a new VIM document builder in which the instance transforms have been multiplied by the given matrix.
        /// </summary>
        public DocumentBuilder TransformInstances(VimScene vim, Matrix4x4 matrix, bool deduplicateMeshes = false)
            => Transform(
                vim,
                _ => true,
                _ => true,
                n => n.Transform * matrix,
                m => m,
                deduplicateMeshes);
    }
}
