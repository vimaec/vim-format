using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Vim.Math3d;
using Vim.Util;

namespace Vim.Format.api_v2
{
    public class VimTransformService
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
        public VimTransformService(string generatorString, string versionString)
        {
            _generatorString = generatorString;
            _versionString = versionString;
        }

        /// <summary>
        /// Returns a new VIM builder in which the original meshes have been deduplicated.
        /// </summary>
        public VimBuilder DeduplicateGeometry(VIM vim)
            => Transform(
                vim,
                _ => true,
                (_, cur) => cur,
                true);

        /// <summary>
        /// Returns a new VIM builder in which the elements and their geometry have been filtered.
        /// </summary>
        public VimBuilder Filter(VIM vim, ElementFilter elementFilter, bool deduplicateMeshes = false)
            => Transform(
                vim,
                elementFilter,
                (_, cur) => cur,
                deduplicateMeshes);

        /// <summary>
        /// Returns a new VIM builder in which the element transforms have been multiplied by the given matrix.
        /// </summary>
        public VimBuilder Transform(VIM vim, Matrix4x4 matrix, bool deduplicateMeshes = false)
            => Transform(
                vim,
                _ => true,
                (_, cur) => cur * matrix,
                deduplicateMeshes);

        public delegate bool ElementFilter(VimElementGeometryInfo egi);
        public delegate Matrix4x4 InstanceTransform(int instanceIndex, Matrix4x4 currentTransform);

        /// <summary>
        /// Transforms the VIM into a new VIM builder based on the given filters and transformations.
        /// </summary>
        /// <param name="vim">The VIM to transform into a new VIM builder.</param>
        /// <param name="elementFilter">Returns true if the given element should be present in the new VIM builder.</param>
        /// <param name="instanceTransform">Returns the instance's new transform (or its current transform if left unchanged).</param>
        /// <param name="deduplicateMeshes">Determines whether the mesh deduplication process is applied.</param>
        public VimBuilder Transform(
            VIM vim,
            ElementFilter elementFilter = null,
            InstanceTransform instanceTransform = null,
            bool deduplicateMeshes = false)
        {
            var vb = new VimBuilder(_generatorString, ObjectModel.SchemaVersion.Current, _versionString);

            var geometryData = vim.GeometryData;
            var elementGeometryInfo = vim.GetElementGeometryInfoList();

            // Filter the elements and instances we want to keep
            var oldInstanceIndexToNewInstanceIndex = new Dictionary<int, int>();
            var instanceIndicesToKeep = new List<int>();
            var elementIndicesToKeep = new HashSet<int>();
            var oldMeshIndexToNewMeshIndex = new Dictionary<int, int>();
            var meshIndicesToKeep = new List<int>();

            foreach (var egi in elementGeometryInfo) // Reminder: ElementGeometryInfo is 1:1 aligned with the Element table.
            {
                var keep = elementFilter?.Invoke(egi) ?? true;
                if (!keep)
                    continue;

                elementIndicesToKeep.Add(egi.ElementIndex);

                foreach (var (oldInstanceIndex, oldMeshIndex) in egi.InstanceAndMeshIndices)
                {
                    if (oldInstanceIndex != -1 && !oldInstanceIndexToNewInstanceIndex.ContainsKey(oldInstanceIndex))
                    {
                        var newInstanceIndex = instanceIndicesToKeep.Count;
                        instanceIndicesToKeep.Add(oldInstanceIndex);
                        oldInstanceIndexToNewInstanceIndex.Add(oldInstanceIndex, newInstanceIndex);
                    }

                    if (oldMeshIndex != -1 &&
                        geometryData.TryGetVimMeshView(oldMeshIndex, out var meshView) &&
                        meshView.FaceCount != 0 &&
                        !oldMeshIndexToNewMeshIndex.ContainsKey(oldMeshIndex))
                    {
                        var newMeshIndex = meshIndicesToKeep.Count;
                        meshIndicesToKeep.Add(oldMeshIndex);
                        oldMeshIndexToNewMeshIndex.Add(oldMeshIndex, newMeshIndex);
                    }
                }
            }

            var filteredEntityTableBuilders = VimEntityTableBuilderRemapped.FilterElements(
                VimBuilder.GetVimEntityTableBuilders(vim),
                elementIndicesToKeep);

            var meshViewLookup = new Dictionary<int, VimMeshView>();
            if (deduplicateMeshes)
            {
                // Group the mesh views under a common mesh view
                var groupedMeshes = geometryData.GroupMeshViews(meshIndicesToKeep);

                // Create the lookup from old mesh view index to common mesh view
                foreach (var (meshComparer, meshViews) in groupedMeshes)
                {
                    foreach (var meshView in meshViews)
                    {
                        meshViewLookup[meshView.MeshIndex] = meshComparer.MeshView;
                    }
                }
            }
            else
            {
                foreach (var meshIndex in meshIndicesToKeep)
                {
                    var meshView = geometryData.GetMeshView(meshIndex);
                    if (meshView.HasValue)
                    {
                        meshViewLookup[meshIndex] = meshView.Value;
                    }
                }
            }

            // Add the assets
            foreach (var asset in vim.Assets)
                vb.AddAsset(asset);

            // Add the meshes.
            foreach (var oldMeshIndex in meshViewLookup.Keys.OrderBy(i => i))
            {
                vb.Meshes.Add(new VimSubdividedMesh(meshViewLookup[oldMeshIndex]));
            }

            // Add the materials.
            // TODO: Remap the materials from the filtered entity table builders
            // TECH DEBT: this could be improved to remove duplicate materials, but this would require a remapping among the material entities as well.
            var materials = vim.Document.Geometry.Materials.Select(m => m.ToDocumentBuilderMaterial()).ToEnumerable();
            vb.AddMaterials(materials);

            // Remove the associated FamilyInstances and remap the entities.
            var nodeEntityRemap = new EntityRemap(
                TableNames.Node,
                instanceIndicesToKeep.Select(n => n.NodeIndex).ToList(),
                oldInstanceIndexToNewInstanceIndex
            );
            var entityRemaps = EntityRemap.GetEntityRemaps(vim, nodeEntityRemap, elementIndicesToKeep);
            StoreTransformedEntityTables(vim.Document, vb, entityRemaps);

            // Add the nodes
            foreach (var node in instanceIndicesToKeep)
            {
                var g = meshViewLookup.GetOrDefaultAllowNulls(node.GetMesh());
                var meshIndex = meshIndices.GetOrDefaultAllowNulls(g, -1);
                var transform = instanceTransform?.Invoke(node) ?? node.Transform;
                var flags = node.InstanceFlags;
                vb.AddInstance(transform, meshIndex, flags);
            }

            return vb;
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
