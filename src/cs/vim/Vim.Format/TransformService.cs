using System.Collections.Generic;
using System.Diagnostics;
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

            // Filter the nodes we want to keep
            var nodesToKeep = new List<VimSceneNode>();
            var nodeElementIndicesToKeep = new HashSet<int>();
            var oldNodeIndexToNewNodeIndex = new Dictionary<int, int>();
            var vimNodes = vim.VimNodes;
            for (var i = 0; i < vimNodes.Count; ++i)
            {
                var node = vimNodes[i];
                if (nodeFilter?.Invoke(node) ?? true)
                {
                    var oldNodeIndex = i;
                    var newNodeIndex = nodesToKeep.Count;
                    nodesToKeep.Add(node);
                    oldNodeIndexToNewNodeIndex.Add(oldNodeIndex, newNodeIndex);
                    nodeElementIndicesToKeep.Add(node.ElementIndex);
                }
            }

            // Filter the meshes we want to keep
            var meshesToKeep = new List<IMesh>();
            foreach (var node in nodesToKeep)
            {
                var mesh = node.GetMesh();
                if (mesh == null || mesh.NumFaces == 0)
                    continue;

                if (meshFilter?.Invoke(mesh) ?? true)
                    meshesToKeep.Add(mesh);
            }

            var meshLookup = new Dictionary<IMesh, IMesh>();
            var meshIndices = new IndexedSet<IMesh>();

            if (deduplicateMeshes)
            {
                // Group meshes according to a hash function
                const float tolerance = 1f / 12f / 8f;
                var groupedMeshes = meshesToKeep.GroupMeshesByHash(tolerance);

                // Create the lookup from old mesh to new mesh 
                meshLookup = groupedMeshes
                    .SelectMany(grp => grp.Value.Select(m => (m, grp.Key.Mesh)))
                    .ToDictionaryIgnoreDuplicates(pair => pair.m, pair => pair.Mesh);
            }
            else
            {
                // Create a dummy mapping from mesh to mesh  
                foreach (var m in meshesToKeep)
                    meshLookup.AddIfNotPresent(m, m);
            }

            // Collect the mesh indices in an indexed set.
            foreach (var m in meshLookup.Values)
                meshIndices.Add(m);

            // Add the assets
            foreach (var asset in vim.Document.Assets.Values.ToEnumerable())
                db.AddAsset(asset);

            // Add the transformed meshes.
            var subdividedMeshes = meshIndices.OrderedKeys.Select(m => (meshTransform?.Invoke(m) ?? m).ToDocumentBuilderSubdividedMesh());
            db.AddMeshes(subdividedMeshes);

            // Add the materials.
            // TECH DEBT: this could be improved to remove duplicate materials, but this would require a remapping among the material entities as well.
            var materials = vim.Document.Geometry.Materials.Select(m => m.ToDocumentBuilderMaterial()).ToEnumerable();
            db.AddMaterials(materials);

            // Remove the associated FamilyInstances and remap the entities.
            var nodeEntityRemap = new EntityRemap(
                TableNames.Node,
                nodesToKeep.Select(n => n.NodeIndex).ToList(),
                oldNodeIndexToNewNodeIndex
            );
            var entityRemaps = EntityRemap.GetEntityRemaps(vim, nodeEntityRemap, nodeElementIndicesToKeep);
            StoreTransformedEntityTables(vim.Document, db, entityRemaps);

            // Add the nodes
            foreach (var node in nodesToKeep)
            {
                var g = meshLookup.GetOrDefaultAllowNulls(node.GetMesh());
                var meshIndex = meshIndices.GetOrDefaultAllowNulls(g, -1);
                var transform = nodeTransform?.Invoke(node) ?? node.Transform;
                var flags = node.InstanceFlags;
                db.AddInstance(transform, meshIndex, flags);
            }

            return db;
        }

        public class EntityRemap
        {
            public string EntityTableName { get; }
            public List<int> RemappedIndices { get; }
            public Dictionary<int, int> OldToNewIndexMap { get; }

            public EntityRemap(string entityTableName, List<int> remappedIndices, Dictionary<int, int> oldToNewIndexMap)
            {
                EntityTableName = entityTableName;
                RemappedIndices = remappedIndices;
                OldToNewIndexMap = oldToNewIndexMap;
            }

            private EntityRemap(EntityTable entityTable, HashSet<int> entityIndicesToKeep)
            {
                EntityTableName = entityTable.Name;
                RemappedIndices = new List<int>();
                OldToNewIndexMap = new Dictionary<int, int>();

                for (var entityIndex = 0; entityIndex < entityTable.NumRows; ++entityIndex)
                {
                    if (!entityIndicesToKeep.Contains(entityIndex))
                        continue;

                    var newEntityIndex = RemappedIndices.Count;
                    RemappedIndices.Add(entityIndex);
                    OldToNewIndexMap.Add(entityIndex, newEntityIndex);
                }
            }

            public static Dictionary<string, EntityRemap> GetEntityRemaps(
                VimScene vim,
                EntityRemap nodeRemap,
                HashSet<int> nodeElementIndicesToKeep)
            {
                var dm = vim.DocumentModel;
                
                // Start by presuming we will keep all the element indices.
                var elementIndicesToKeep = new HashSet<int>();
                for (var elementIndex = 0; elementIndex < dm.NumElement; ++elementIndex)
                {
                    var elementInfo = dm.GetElementInfo(elementIndex);

                    var keep = true;

                    if (elementInfo.IsSystem)
                    {
                        // TECH DEBT: for simplicity, we keep all elements which represent systems.
                        // Specifically, in the case of curtain walls in RoomTest.vim, an element
                        // can be -BOTH- a System and a FamilyInstance, which complicates things
                        // if we were to remove that element (we would also need to remove other
                        // ElementInSystem records referencing that removed system)
                        keep = true;
                    }
                    else if (elementInfo.IsFamilyInstance && !nodeElementIndicesToKeep.Contains(elementIndex))
                    {
                        // Here, the element represents a FamilyInstance, which we want to remove from the
                        // transformed collection of elements.
                        keep = false;
                    }

                    if (keep)
                    {
                        elementIndicesToKeep.Add(elementIndex);
                    }
                }

                var entityRemaps = new Dictionary<string, EntityRemap>()
                {
                    { TableNames.Node, nodeRemap },
                };

                // Add the element table to the remapped set.
                if (TryGet(vim, TableNames.Element, elementIndicesToKeep, out var elementRemap))
                {
                    entityRemaps.Add(TableNames.Element, elementRemap);
                }

                var cascadeElementRemapTableNames = ObjectModelReflection.GetEntityTypes()
                    .Where(t => t.HasCascadeElementRemap())
                    .Select(t => t.GetEntityTableName());

                // Compute the remapping for the following entity tables: Node, FamilyInstance, Parameter, ElementInSystem, ...
                foreach (var entityTableName in cascadeElementRemapTableNames)
                {
                    if (TryGetCascadeElementRemap(vim, entityTableName, elementIndicesToKeep, out var remap))
                    {
                        // Add the cascading entity table to the remapped set.
                        entityRemaps.Add(entityTableName, remap);
                    }
                }

                return entityRemaps;
            }

            private static bool TryGet(
                VimScene vim,
                string entityTableName,
                HashSet<int> filteredIndices,
                out EntityRemap remap)
            {
                remap = null;

                var entityTables = vim.Document.EntityTables;

                if (!entityTables.Contains(entityTableName))
                    return false;

                remap = new EntityRemap(entityTables[entityTableName], filteredIndices);

                return true;
            }

            private static bool TryGetCascadeElementRemap(
                VimScene vim,
                string entityTableName,
                HashSet<int> elementIndicesToKeep,
                out EntityRemap remap)
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
                    .FirstOrDefault(columnName => columnName == $"index:{TableNames.Element}:Element");

                if (string.IsNullOrWhiteSpace(elementIndexColumnName))
                    return false;

                var entityIndicesToKeep = new HashSet<int>();
                var elementIndices = indexColumns[elementIndexColumnName].Array;
                for (var i = 0; i < elementIndices.Length; ++i)
                {
                    var elementIndex = elementIndices[i];

                    if (elementIndicesToKeep.Contains(elementIndex))
                    {
                        entityIndicesToKeep.Add(i);
                    }
                }

                remap = new EntityRemap(entityTable, entityIndicesToKeep);

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
            var sourceTableName = sourceTable.Name;
            var tb = db.CreateTableBuilder(sourceTableName);

            foreach (var col in sourceTable.IndexColumns.Values.ToEnumerable())
            {
                var remappedIndexRelations = col.GetTypedData().Copy(remapping);

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

                        if (newIndex == EntityRelation.None &&
                            col.Name == $"index:{TableNames.Element}:Element" &&
                            sourceTableName != TableNames.Node) // NOTE: special exception for the Node table which must always be 1:1 aligned with the g3d instances (and may not always have a backing element)
                        {
                            Debug.Fail($"Remapped index is {EntityRelation.None} in {sourceTableName} > {col.Name}");
                        }

                        remappedIndexRelations[i] = newIndex;
                    }
                }

                tb.AddIndexColumn(col.Name, remappedIndexRelations);
            }

            foreach (var col in sourceTable.DataColumns.Values.ToEnumerable())
            {
                tb.AddDataColumn(col.Name, col.RemapOrSelfDataColumn(remapping));
            }

            foreach (var col in sourceTable.StringColumns.Values.ToEnumerable())
            {
                var strings = col.GetTypedData().Select(i => sourceTable.Document.StringTable.ElementAtOrDefault(i, null));
                tb.AddStringColumn(col.Name, strings.ToArray().RemapOrSelf(remapping));
            }

            return tb;
        }
    }
}
