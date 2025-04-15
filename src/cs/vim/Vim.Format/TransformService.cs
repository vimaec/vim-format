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
            var filteredNodeElementIndices = new HashSet<int>();
            var filteredNodes = new List<VimSceneNode>();
            var vimNodes = vim.VimNodes;
            for (var i = 0; i < vimNodes.Count; ++i)
            {
                var n = vimNodes[i];
                if (nodeFilter?.Invoke(n) ?? true)
                {
                    filteredNodes.Add(n);
                    filteredNodeElementIndices.Add(n.ElementIndex);
                }
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
                nodeIndexRemapping.Add(node.NodeIndex);
                db.AddInstance(transform, meshIndex);
            }



            // Calculate the entity remapping
            var entityRemaps = EntityRemap.GetEntityRemaps(vim, filteredNodeElementIndices);
            
            // Copy the tables
            StoreTransformedEntityTables(vim.Document, db, entityRemaps);

            // Construct the document 
            return db;
        }

        public class EntityRemap
        {
            public string EntityTableName { get; }
            public List<int> RemappedIndices { get; }
            public Dictionary<int, int> OldToNewIndexMap { get; }

            /// <summary>
            /// Constructor
            /// </summary>
            private EntityRemap(EntityTable entityTable, HashSet<int> filteredIndices)
            {
                EntityTableName = entityTable.Name;
                RemappedIndices = new List<int>();
                OldToNewIndexMap = new Dictionary<int, int>();

                for (var entityIndex = 0; entityIndex < entityTable.NumRows; ++entityIndex)
                {
                    if (!filteredIndices.Contains(entityIndex))
                        continue;

                    var newEntityIndex = RemappedIndices.Count;
                    RemappedIndices.Add(entityIndex);
                    OldToNewIndexMap.Add(entityIndex, newEntityIndex);
                }
            }

            public static Dictionary<string, EntityRemap> GetEntityRemaps(VimScene vim, HashSet<int> filteredNodeElementIndices)
            {
                // Start by presuming that all element indices will be preserved
                var dm = vim.DocumentModel;

                var filteredElementIndices = new HashSet<int>();
                for (var elementIndex = 0; elementIndex < dm.NumElement; ++elementIndex)
                {
                    filteredElementIndices.Add(elementIndex);
                }

                // Next, discard any family instance elements which are not preserved.
                for (var i = 0; i < dm.NumFamilyInstance; ++i)
                {
                    var familyInstanceElementIndex = dm.FamilyInstanceElementIndex[i];
                    if (!filteredNodeElementIndices.Contains(familyInstanceElementIndex))
                    {
                        // Discard elements associated to family instances which will not appear in the remapping.
                        filteredElementIndices.Remove(familyInstanceElementIndex);
                    }
                }

                var entityRemaps = new Dictionary<string, EntityRemap>();

                // Add the element table to the remapped set.
                if (TryGet(vim, TableNames.Element, filteredElementIndices, out var elementRemap))
                {
                    entityRemaps.Add(TableNames.Element, elementRemap);
                }

                var cascadeElementRemapTableNames = ObjectModelReflection.GetEntityTypes()
                    .Where(t => t.HasCascadeElementRemap())
                    .Select(t => t.GetEntityTableName());

                // Node, FamilyInstance, Parameter, ElementInSystem, ...
                foreach (var entityTableName in cascadeElementRemapTableNames)
                {
                    // Add the cascading entity table to the remapped set.
                    if (TryGetCascadeElementRemap(vim, entityTableName, filteredElementIndices, out var remap))
                    {
                        entityRemaps.Add(entityTableName, remap);
                    }
                }

                return entityRemaps;
            }

            private static bool TryGet(VimScene vim, string entityTableName, HashSet<int> filteredIndices, out EntityRemap remap)
            {
                remap = null;

                var entityTables = vim.Document.EntityTables;

                if (!entityTables.Contains(entityTableName))
                    return false;

                remap = new EntityRemap(entityTables[entityTableName], filteredIndices);

                return true;
            }

            private static bool TryGetCascadeElementRemap(VimScene vim, string entityTableName, HashSet<int> filteredElementIndices, out EntityRemap remap)
            {
                remap = null;

                var entityTables = vim.Document.EntityTables;

                if (!entityTables.Contains(entityTableName))
                    return false;

                var entityTable = entityTables[entityTableName];
                var indexColumns = entityTable.IndexColumns;
                if (indexColumns.Keys.Count == 0)
                    return false;

                var elementIndexColumnName = indexColumns.Keys
                    .FirstOrDefault(columnName => DocumentExtensions.GetRelatedTableNameFromColumnName(columnName) == TableNames.Element);

                if (string.IsNullOrWhiteSpace(elementIndexColumnName))
                    return false;

                var filteredEntityIndices = new HashSet<int>();
                var elementIndexColumn = indexColumns[elementIndexColumnName].Array;
                for (var i = 0; i < elementIndexColumn.Length; ++i)
                {
                    var elementIndex = elementIndexColumn[i];

                    if (filteredElementIndices.Contains(elementIndex))
                    {
                        filteredEntityIndices.Add(i);
                    }
                }

                remap = new EntityRemap(entityTable, filteredEntityIndices);

                return true;
            }
        }

        private static void StoreTransformedEntityTables(
            Document sourceDocument,
            DocumentBuilder db,
            Dictionary<string, EntityRemap> entityRemaps = null)
        {
            // Iterate over all the source tables.
            foreach (var sourceEntityTable in sourceDocument.EntityTables.Values.ToEnumerable())
            {
                var entityTableName = sourceEntityTable.Name;

                // Don't copy tables that are computed automatically
                if (VimConstants.ComputedTableNames.Contains(entityTableName))
                    continue;

                var remappedIndices = entityRemaps != null && entityRemaps.TryGetValue(entityTableName, out var entityRemap)
                    ? entityRemap.RemappedIndices
                    : null;

                StoreTransformedTable(sourceEntityTable, db, remappedIndices, entityRemaps);
            }
        }

        public static EntityTableBuilder StoreTransformedTable(
            EntityTable sourceTable,
            DocumentBuilder db,
            List<int> remapping = null,
            Dictionary<string, EntityRemap> entityRemaps = null)
        {
            var name = sourceTable.Name;
            var tb = db.CreateTableBuilder(name);

            foreach (var col in sourceTable.IndexColumns.Values.ToEnumerable())
            {
                var remappedIndexRelations = col.GetTypedData().RemapData(remapping);

                // Remap the referenced indices.
                var relatedTableName = col.GetRelatedTableName();
                if (entityRemaps?.TryGetValue(relatedTableName, out var entityRemap) ?? false)
                {
                    for (var i = 0; i < remappedIndexRelations.Length; ++i)
                    {
                        var oldIndex = remappedIndexRelations[i];
                        var newIndex = entityRemap.OldToNewIndexMap.TryGetValue(oldIndex, out var ni)
                            ? ni
                            : EntityRelation.None;

                        remappedIndexRelations[i] = newIndex;
                    }
                }

                tb.AddIndexColumn(col.Name, remappedIndexRelations);
            }

            foreach (var col in sourceTable.DataColumns.Values.ToEnumerable())
            {
                tb.AddDataColumn(col.Name, col.CopyDataColumn(remapping));
            }

            foreach (var col in sourceTable.StringColumns.Values.ToEnumerable())
            {
                var strings = col.GetTypedData().Select(i => sourceTable.Document.StringTable.ElementAtOrDefault(i, null));
                tb.AddStringColumn(col.Name, strings.ToArray().RemapData(remapping));
            }

            return tb;
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
        public DocumentBuilder Filter(VimScene vim, NodeFilter nodeFilter, bool deduplicateMeshes = false)
            => Transform(
                vim,
                nodeFilter,
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
