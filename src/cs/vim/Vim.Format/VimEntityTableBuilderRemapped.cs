using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Vim.BFast;
using Vim.Util;

namespace Vim.Format
{
    public class VimEntityTableBuilderRemapped
    {
        public VimEntityTableBuilder EntityTableBuilder { get; private set; }
        public int[] OldToNewIndexMap { get; private set; }
        public bool IsRemapped => OldToNewIndexMap != null;

        /// <summary>
        /// Constructor
        /// </summary>
        private VimEntityTableBuilderRemapped(
            VimEntityTableBuilder entityTableBuilder,
            int[] oldToNewIndexMap)
        {
            EntityTableBuilder = entityTableBuilder;
            OldToNewIndexMap = oldToNewIndexMap;
        }

        private void UpdateFrom(VimEntityTableBuilderRemapped other)
        {
            EntityTableBuilder = other.EntityTableBuilder;
            OldToNewIndexMap = other.OldToNewIndexMap;
        }

        /// <summary>
        /// Returns a default remapped entity table builder in which no remapping has occurred.
        /// </summary>
        private static VimEntityTableBuilderRemapped CreateDefault(VimEntityTableBuilder et)
            => new VimEntityTableBuilderRemapped(et, null);

        /// <summary>
        /// Returns a VimTableBuilderRemapped whose contained VimEntityTableBuilder is deduplicated based on the given keyFn.
        /// </summary>
        private static VimEntityTableBuilderRemapped CreateDeduplicated<T>(
            IReadOnlyVimEntityTableBuilder et,
            Func<int, IReadOnlyVimEntityTableBuilder, T> keyFn)
        {
            // We maintain a mapping of the keys to their new indices in this dictionary.
            //
            // ex: input keyFn sequence: [ "a", "b", "c", "b", "a", "c", "d" ]
            //            keyToIndexMap: {
            //                             "a" -> 0,
            //                             "b" -> 1,
            //                             "c" -> 2,
            //                             "d" -> 3,
            //                           }
            var keyToNewIndexMap = new Dictionary<T, int>();

            // We retain the non-duplicate indices in this array.
            //
            // ex: input keyFn sequence: [ "a", "b", "c", "b", "a", "c", "d" ]
            //          retainedIndices: [  0 ,  1 ,  2 ,                 6  ]
            var retainedIndices = new List<int>();

            // We map the old index to the new index in this array.
            //
            // ex: input keyFn sequence: ["a", "b", "c", "b", "a", "c", "d" ]
            //                old index: [ 0 ,  1 ,  2 ,  3 ,  4 ,  5 ,  6  ]
            //         oldToNewIndexMap: [ 0 ,  1 ,  2 ,  1 ,  0 ,  2 ,  3  ]
            var oldToNewIndexMap = new int[et.RowCount];

            // Iterate over the rows and build the maps
            for (var i = 0; i < et.RowCount; ++i)
            {
                var key = keyFn(i, et);

                if (keyToNewIndexMap.TryGetValue(key, out var newIndex))
                {
                    // The key was already found, so store the remapping.
                    oldToNewIndexMap[i] = newIndex;
                }
                else
                {
                    // This is the first time we encounter this key.
                    var nextIndex = keyToNewIndexMap.Count;
                    keyToNewIndexMap.Add(key, nextIndex);
                    retainedIndices.Add(i);
                    oldToNewIndexMap[i] = nextIndex;
                }
            }

            // ex: input keyFn sequence: [ "a", "b", "c", "b", "a", "c", "d" ]
            //  desired output sequence: [ "a", "b", "c", "d" ]
            var remapped = new VimEntityTableBuilder(et.Name);

            // Remap Index columns directly/naively now. In a second pass, the indices are adjusted based OldToNewIndexMap.
            foreach (var kv in et.IndexColumns)
            {
                var colName = kv.Key;
                var col = kv.Value;
                var newCol = retainedIndices.Select(retainedIndex => col[retainedIndex]);
                remapped.AddIndexColumn(colName, newCol);
            }

            // Remap the data columns.
            foreach (var kv in et.DataColumns)
            {
                var colName = kv.Key;
                var col = kv.Value;
                if (!VimEntityTableColumnName.TryParseColumnTypePrefix(colName, out var typePrefix))
                    continue;

                var newCol = VimEntityTableColumnActions.RemapOrSelfDataColumn(col, typePrefix, retainedIndices);
                remapped.AddDataColumn(colName, newCol);
            }

            // Remap the string columns.
            foreach (var kv in et.StringColumns)
            {
                var colName = kv.Key;
                var col = kv.Value;
                var newCol = retainedIndices.Select(retainedIndex => col[retainedIndex]);
                remapped.AddStringColumn(colName, newCol);
            }

            return new VimEntityTableBuilderRemapped(remapped, oldToNewIndexMap);
        }

        public delegate bool EntityFilter(int entityIndex, VimEntityTableBuilder et);

        /// <summary>
        /// Returns a VimTableBuilderRemapped with filtered entities.
        /// </summary>
        private static VimEntityTableBuilderRemapped CreateFiltered(
            VimEntityTableBuilder et,
            EntityFilter entityFilter)
        {
            var retainedIndices = new List<int>();
            var oldToNewIndexMap = new int[et.RowCount];

            // Iterate over the rows and build the maps
            for (var i = 0; i < et.RowCount; ++i)
            {
                if (entityFilter(i, et))
                {
                    var newIndex = retainedIndices.Count;
                    retainedIndices.Add(i);
                    oldToNewIndexMap[i] = newIndex;
                }
                else
                {
                    oldToNewIndexMap[i] = -1;
                }
            }

            var remapped = Remap(et, retainedIndices);

            return new VimEntityTableBuilderRemapped(remapped, oldToNewIndexMap);
        }

        private static VimEntityTableBuilder Remap(IReadOnlyVimEntityTableBuilder et, IReadOnlyList<int> retainedIndices)
        {
            var remapped = new VimEntityTableBuilder(et.Name);

            // Remap Index columns directly/naively now. In a second pass, the indices are adjusted based OldToNewIndexMap.
            foreach (var kv in et.IndexColumns)
            {
                var colName = kv.Key;
                var col = kv.Value;
                var newCol = retainedIndices.Select(retainedIndex => col[retainedIndex]);
                remapped.AddIndexColumn(colName, newCol);
            }

            // Remap the data columns.
            foreach (var kv in et.DataColumns)
            {
                var colName = kv.Key;
                var col = kv.Value;
                if (!VimEntityTableColumnName.TryParseColumnTypePrefix(colName, out var typePrefix))
                    continue;

                var newCol = VimEntityTableColumnActions.RemapOrSelfDataColumn(col, typePrefix, retainedIndices);
                remapped.AddDataColumn(colName, newCol);
            }

            // Remap the string columns.
            foreach (var kv in et.StringColumns)
            {
                var colName = kv.Key;
                var col = kv.Value;
                var newCol = retainedIndices.Select(retainedIndex => col[retainedIndex]);
                remapped.AddStringColumn(colName, newCol);
            }

            return remapped;
        }

        private static void UpdateRelations(
            List<VimEntityTableBuilderRemapped> remappedEntityTableBuilders,
            CancellationToken ct = default)
        {
            MutateIndexRelations(remappedEntityTableBuilders, ct);

            RemoveOrphanedJoiningTableEntities(remappedEntityTableBuilders, ct);
        }

        private static void MutateIndexRelations(
            List<VimEntityTableBuilderRemapped> remappedEntityTableBuilders,
            CancellationToken ct = default)
        {
            var remappedTableIndices = remappedEntityTableBuilders
                .Where(r => r.IsRemapped)
                .ToDictionary(
                    r => r.EntityTableBuilder.Name,
                    r => r.OldToNewIndexMap);

            if (remappedTableIndices.Count == 0)
                return; // nothing to do.

            // Mutate the index relationships using the remapped entity table builders' indices.
            foreach (var et in remappedEntityTableBuilders.Select(r => r.EntityTableBuilder))
            {
                ct.ThrowIfCancellationRequested();

                foreach (var kv in et.IndexColumns)
                {
                    var indexColumnName = kv.Key;
                    var indexColumn = kv.Value;

                    // Get the related index remapping.
                    var tableName = VimEntityTableColumnName.GetRelatedTableNameFromColumnName(indexColumnName);
                    if (!remappedTableIndices.TryGetValue(tableName, out var oldToNewIndexMap))
                        continue;

                    // Mutate the indices
                    for (var i = 0; i < indexColumn.Length; ++i)
                    {
                        var oldIndex = indexColumn[i];
                        if (oldIndex != VimEntityTableConstants.NoEntityRelation)
                        {
                            indexColumn[i] = oldToNewIndexMap[oldIndex];
                        }
                    }
                }
            }
        }

        private static bool RemoveOrphanedJoiningTableEntities(
            List<VimEntityTableBuilderRemapped> remappedEntityTableBuilders,
            CancellationToken ct = default)
        {
            var orphansRemoved = false;
            var joiningTableNames = VimEntityTableSet.GetJoiningTableNames();

            foreach (var rtb in remappedEntityTableBuilders)
            {
                var tb = rtb.EntityTableBuilder;

                if (!joiningTableNames.Contains(tb.Name))
                    continue;

                var orphanIndices = new HashSet<int>();

                // Find the orphaned entities
                foreach (var (tablName, indexColumn) in tb.IndexColumns)
                {
                    for (var i = 0; i < tb.RowCount; ++i)
                    {
                        if (indexColumn[i] == EntityRelation.None)
                        {
                            orphanIndices.Add(i);
                        }
                    }
                }

                if (orphanIndices.Count == 0)
                    continue;

                // Remove the orphans.
                // NOTE: these orphans can be safely/naively removed because nobody else should be referencing them.
                var swept = CreateFiltered(tb, (i, e) => !orphanIndices.Contains(i));

                rtb.UpdateFrom(swept);

                orphansRemoved = true;
            }

            return orphansRemoved;
        }

        private static List<VimEntityTableBuilderRemapped> DeduplicateEntityTableBuilders(
            IReadOnlyList<VimEntityTableBuilder> entityTableBuilders,
            CancellationToken ct = default)
        {
            var result = new List<VimEntityTableBuilderRemapped>();

            var specialTableNames = new HashSet<string>();

            ////////////////////////////////////
            // FUTURE MAINTENANCE NOTES:
            // - if we ever remap materials, we must also propagate this remapping to the submeshMaterials in the geometry buffer.
            // - if we ever remap assets, we must also propagate this remapping to the merged assets in the asset buffer.
            ////////////////////////////////////

            // Merge the same categories
            var categoryTable = entityTableBuilders.FirstOrDefault(t => t.Name == VimEntityTableNames.Category);
            VimEntityTableBuilderRemapped categoryTableRemapped = null;
            if (categoryTable != null)
            {
                var table = categoryTable;
                var nameCol = table.StringColumns.GetOrDefault("string:Name");
                var builtInCol = table.StringColumns.GetOrDefault("string:BuiltInCategory");
                categoryTableRemapped = CreateDeduplicated<object>(table, (i, _) => (
                    nameCol?.ElementAtOrDefault(i, "") ?? "",
                    builtInCol?.ElementAtOrDefault(i, "") ?? ""
                ));
                result.Add(categoryTableRemapped);
                specialTableNames.Add(VimEntityTableNames.Category);
            }

            ct.ThrowIfCancellationRequested();

            // Merge the same display units
            var displayUnitTable = entityTableBuilders.FirstOrDefault(t => t.Name == VimEntityTableNames.DisplayUnit);
            VimEntityTableBuilderRemapped displayUnitTableRemapped = null;
            if (displayUnitTable != null)
            {
                var table = displayUnitTable;
                var specCol = table.StringColumns.GetOrDefault("string:Spec");
                var typeCol = table.StringColumns.GetOrDefault("string:Type");
                var labelCol = table.StringColumns.GetOrDefault("string:Label");
                displayUnitTableRemapped = CreateDeduplicated(table, (i, _) => (
                    specCol?.ElementAtOrDefault(i, "") ?? "",
                    typeCol?.ElementAtOrDefault(i, "") ?? "",
                    labelCol?.ElementAtOrDefault(i, "") ?? ""
                )); // same tuple values as DisplayUnit.GetStorageKey()
                result.Add(displayUnitTableRemapped);
                specialTableNames.Add(VimEntityTableNames.DisplayUnit);
            }

            ct.ThrowIfCancellationRequested();

            // Merge the same parameter descriptors
            var pdTable = entityTableBuilders.FirstOrDefault(t => t.Name == VimEntityTableNames.ParameterDescriptor);
            VimEntityTableBuilderRemapped pdTableRemapped = null;
            {
                var table = pdTable;
                var nameArray = table.StringColumns.GetOrDefault("string:Name");
                var groupArray = table.StringColumns.GetOrDefault("string:Group");
                var isInstanceArray = VimEntityTableColumnTypeInfo.GetDataColumnAsTypedArray<bool>(table.DataColumns.GetOrDefault("byte:IsInstance"));
                var isSharedArray = VimEntityTableColumnTypeInfo.GetDataColumnAsTypedArray<bool>(table.DataColumns.GetOrDefault("byte:IsShared"));
                var isReadOnlyArray = VimEntityTableColumnTypeInfo.GetDataColumnAsTypedArray<bool>(table.DataColumns.GetOrDefault("byte:IsReadOnly"));
                var parameterTypeArray = table.StringColumns.GetOrDefault("string:ParameterType");
                var flagsArray = VimEntityTableColumnTypeInfo.GetDataColumnAsTypedArray<int>(table.DataColumns.GetOrDefault("int:Flags"));
                var guidArray = table.StringColumns.GetOrDefault("string:Guid");
                var storageTypeArray = VimEntityTableColumnTypeInfo.GetDataColumnAsTypedArray<int>(table.DataColumns.GetOrDefault("int:StorageType"));
                var displayUnitIndexArray = table.IndexColumns.GetOrDefault("index:Vim.DisplayUnit:DisplayUnit");

                pdTableRemapped = CreateDeduplicated<object>(table, (i, _) => {
                    var name = nameArray?.ElementAtOrDefault(i, "") ?? "";
                    var group = groupArray?.ElementAtOrDefault(i, "") ?? "";
                    var isInstance = isInstanceArray?.ElementAtOrDefault(i, false) ?? false;
                    var isShared = isSharedArray?.ElementAtOrDefault(i, false) ?? false;
                    var isReadOnly = isReadOnlyArray?.ElementAtOrDefault(i, false) ?? false;
                    var parameterType = parameterTypeArray?.ElementAtOrDefault(i, "") ?? "";
                    var flags = flagsArray?.ElementAtOrDefault(i, 0) ?? 0;
                    var guid = guidArray?.ElementAtOrDefault(i, "") ?? "";
                    var storageType = storageTypeArray?.ElementAtOrDefault(i, 0) ?? 0;
                    var displayUnitIndex = displayUnitIndexArray?.ElementAtOrDefault(i, -1) ?? -1;

                    var remappedDisplayUnitIndex = displayUnitIndex == -1
                        ? -1
                        : displayUnitTableRemapped?.OldToNewIndexMap.ElementAtOrDefault(displayUnitIndex, displayUnitIndex) ?? displayUnitIndex;

                    return (
                        name,
                        group,
                        isInstance,
                        isShared,
                        isReadOnly,
                        parameterType,
                        flags,
                        guid,
                        storageType,
                        remappedDisplayUnitIndex
                    );
                }); // same value tuples as ParameterDescriptor.GetStorageKey()
                result.Add(pdTableRemapped);
                specialTableNames.Add(VimEntityTableNames.ParameterDescriptor);
            }

            ct.ThrowIfCancellationRequested();

            // Process the remaining entity tables.
            foreach (var table in entityTableBuilders)
            {
                ct.ThrowIfCancellationRequested();

                var tableName = table.Name;

                if (specialTableNames.Contains(tableName))
                    continue;

                result.Add(CreateDefault(table));
            }

            // Mutate all the entity index relations to adapt to the filtered entities.
            UpdateRelations(result, ct);

            return result;
        }

        /// <summary>
        /// Returns a new collection of entity table builders by deduplicating specific entities which would be
        /// meaninglessly duplicated in the resulting VIM file.
        /// </summary>
        public static VimEntityTableBuilder[] DeduplicateEntities(
            IReadOnlyList<VimEntityTableBuilder> entityTableBuilders, CancellationToken ct = default)
            => DeduplicateEntityTableBuilders(entityTableBuilders, ct)
                .Select(r => r.EntityTableBuilder)
                .ToArray();

        /// <summary>
        /// Returns a filtered collection of entity tables.
        /// </summary>
        public static List<VimEntityTableBuilderRemapped> FilterEntities(
            IReadOnlyList<VimEntityTableBuilder> entityTableBuilders,
            Dictionary<string, EntityFilter> entityTableFilters,
            CancellationToken ct = default)
        {
            var result = new List<VimEntityTableBuilderRemapped>();

            foreach (var et in entityTableBuilders)
            {
                ct.ThrowIfCancellationRequested();

                if (entityTableFilters.TryGetValue(et.Name, out var filter))
                {
                    result.Add(CreateFiltered(et, filter));
                }
                else
                {
                    result.Add(CreateDefault(et));
                }
            }

            // Update all the entity index relations.
            UpdateRelations(result, ct);

            return result;
        }

        public static VimEntityTableBuilder[] FilterElements(
            IReadOnlyList<VimEntityTableBuilder> entityTableBuilders,
            HashSet<int> filteredElementIndices,
            IReadOnlyList<int> nodesToKeep = null,
            CancellationToken ct = default)
        {
            // Go through each entity table corresponding to an element kind and remove those entities.
            var entityTableFilters = new Dictionary<string, EntityFilter>()
            {
                { VimEntityTableNames.Element, (i, _) => filteredElementIndices.Contains(i) }, 
            };

            foreach (var entityTableBuilder in entityTableBuilders)
            {
                var tableName = entityTableBuilder.Name;

                ct.ThrowIfCancellationRequested();

                if (tableName == VimEntityTableNames.Node && nodesToKeep != null)
                    continue; // We explicitly remap the node table below.

                entityTableFilters[tableName] = (i, et) =>
                {
                    if (!et.IndexColumns.TryGetValue("index:Vim.Element:Element", out var elementIndexCol))
                        return true;

                    var elementIndex = elementIndexCol.ElementAtOrDefault(i, -1);
                    return elementIndex == -1
                        ? false
                        : filteredElementIndices.Contains(elementIndex);
                };
            }

            var result = FilterEntities(entityTableBuilders, entityTableFilters, ct).Select(r => r.EntityTableBuilder).ToList();

            // mutate the result's node table if the nodesToKeep is defined.
            if (nodesToKeep != null)
            {
                var nodeTableIndex = result.FindIndex(t => t.Name == VimEntityTableNames.Node);
                if (nodeTableIndex >= 0)
                {
                    var nodeTable = result[nodeTableIndex];
                    var remappedNodeTable = Remap(nodeTable, nodesToKeep);
                    result[nodeTableIndex] = remappedNodeTable;
                } 
            }

            return result.ToArray();
        }
    }
}
