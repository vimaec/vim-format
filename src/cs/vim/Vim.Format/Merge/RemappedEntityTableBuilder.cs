using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Vim.Format.ObjectModel;

namespace Vim.Format.Merge
{
    public class RemappedEntityTableBuilder
    {
        public EntityTableBuilder EntityTableBuilder { get; }
        public int[] OldToNewIndexMap { get; }
        public bool IsRemapped => OldToNewIndexMap != null;

        /// <summary>
        /// Constructor
        /// </summary>
        private RemappedEntityTableBuilder(
            EntityTableBuilder entityTableBuilder,
            int[] oldToNewIndexMap)
        {
            EntityTableBuilder = entityTableBuilder;
            OldToNewIndexMap = oldToNewIndexMap;
        }

        /// <summary>
        /// Returns a default remapped entity table builder in which no remapping has occurred.
        /// </summary>
        private static RemappedEntityTableBuilder CreateDefault(EntityTableBuilder et)
            => new RemappedEntityTableBuilder(et, null);

        /// <summary>
        /// Returns a RemappedTableBuilder whose contained EntityTableBuilder is duplicated based on the given keyFn.
        /// </summary>
        private static RemappedEntityTableBuilder CreateRemapped<T>(EntityTableBuilder et, Func<int, EntityTableBuilder, T> keyFn)
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
            var oldToNewIndexMap = new int[et.NumRows];

            // Iterate over the rows and build the maps
            for (var i = 0; i < et.NumRows; ++i)
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
            var remapped = new EntityTableBuilder(et.Name);

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
                var typePrefix = colName.GetTypePrefix();
                var newCol = col.CopyDataColumn(typePrefix, retainedIndices);
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

            return new RemappedEntityTableBuilder(remapped, oldToNewIndexMap);
        }

        private static void UpdateEntityTableBuilderRelations(
            List<RemappedEntityTableBuilder> remappedEntityTableBuilders,
            CancellationToken ct = default)
        {
            var remappedTableIndices = remappedEntityTableBuilders
                .Where(r => r.IsRemapped)
                .ToDictionary(
                    r => r.EntityTableBuilder.Name,
                    r => r.OldToNewIndexMap);

            if (remappedTableIndices.Count == 0)
                return; // nothing to do.

            // Update the index relationships using the remapped entity table builders' indices.
            foreach (var et in remappedEntityTableBuilders.Select(r => r.EntityTableBuilder))
            {
                ct.ThrowIfCancellationRequested();

                foreach (var kv in et.IndexColumns)
                {
                    var indexColumnName = kv.Key;
                    var indexColumn = kv.Value;

                    // Get the related index remapping.
                    var tableName = DocumentExtensions.GetRelatedTableNameFromColumnName(indexColumnName);
                    if (!remappedTableIndices.TryGetValue(tableName, out var oldToNewIndexMap))
                        continue;

                    // Update the indices
                    for (var i = 0; i < indexColumn.Length; ++i)
                    {
                        var oldIndex = indexColumn[i];
                        indexColumn[i] = oldIndex == VimConstants.NoEntityRelation
                            ? oldIndex
                            : oldToNewIndexMap[oldIndex];
                    }
                }
            }
        }

        private static List<RemappedEntityTableBuilder> GetRemappedEntityTableBuilders(
            DocumentBuilder db,
            CancellationToken ct = default)
        {
            var remappedEntityTableBuilders = new List<RemappedEntityTableBuilder>();

            // Deduplicate the entities.
            foreach (var kv in db.Tables)
            {
                ct.ThrowIfCancellationRequested();

                var tableName = kv.Key;
                var table = kv.Value;

                RemappedEntityTableBuilder r;
                switch (tableName)
                {
                    ////////////////////////////////////
                    // FUTURE MAINTENANCE NOTES:
                    // - if we ever remap materials, we must also propagate this remapping to the submeshMaterials in the geometry buffer.
                    // - if we ever remap assets, we must also propagate this remapping to the merged assets in the asset buffer.
                    ////////////////////////////////////

                    // Merge all the categories by name and by built-in category.
                    case TableNames.Category:
                        {
                            var hasNameCol = table.StringColumns.TryGetValue("string:Name", out var nameCol);
                            var hasBuiltInCol = table.StringColumns.TryGetValue("string:BuiltInCategory", out var builtInCol);
                            if (!hasNameCol || !hasBuiltInCol)
                            {
                                r = CreateDefault(table);
                                break;
                            }

                            r = CreateRemapped(table, (i, _) => (nameCol[i], builtInCol[i]));
                            break;
                        }
                    // Merge all the display units
                    case TableNames.DisplayUnit:
                        {
                            var hasSpecCol = table.StringColumns.TryGetValue("string:Spec", out var specCol);
                            var hasTypeCol = table.StringColumns.TryGetValue("string:Type", out var typeCol);
                            var hasLabelCol = table.StringColumns.TryGetValue("string:Label", out var labelCol);
                            if (!hasSpecCol || !hasTypeCol || !hasLabelCol)
                            {
                                r = CreateDefault(table);
                                break;
                            }

                            r = CreateRemapped(table, (i, _) => (specCol[i], typeCol[i], labelCol[i]));
                            break;
                        }

                    // Default case.
                    default:
                        r = CreateDefault(table);
                        break;
                }

                remappedEntityTableBuilders.Add(r);
            }

            // Update the entity index relations.
            UpdateEntityTableBuilderRelations(remappedEntityTableBuilders, ct);

            return remappedEntityTableBuilders;
        }

        /// <summary>
        /// Mutates the document builder by deduplicating specific entities which would be meaninglessly duplicated in the same VIM file.
        /// </summary>
        public static void DeduplicateEntities(DocumentBuilder db, CancellationToken ct = default)
        {
            var remappedEntityTableBuilders = GetRemappedEntityTableBuilders(db, ct);

            db.Tables.Clear();

            foreach (var table in remappedEntityTableBuilders.Select(r => r.EntityTableBuilder))
            {
                db.Tables.Add(table.Name, table);
            }
        }
    }
}
