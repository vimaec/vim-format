using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Vim.BFast;
using Vim.LinqArray;
using Vim.Math3d;
using Vim.Util;

namespace Vim.Format.api_v2
{
    public class VimMergeConfigFiles
    {
        /// <summary>
        /// The input VIM file paths and their transforms.
        /// </summary>
        public (string VimFilePath, Matrix4x4 Transform)[] InputVimFilePathsAndTransforms { get; }

        /// <summary>
        /// The input VIM file paths
        /// </summary>
        public string[] InputVimFilePaths
            => InputVimFilePathsAndTransforms.Select(t => t.VimFilePath).ToArray();

        /// <summary>
        /// The input VIM file path transforms
        /// </summary>
        public Matrix4x4[] InputTransforms
            => InputVimFilePathsAndTransforms.Select(t => t.Transform).ToArray();

        /// <summary>
        /// The merged VIM file path.
        /// </summary>
        public string MergedVimFilePath { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public VimMergeConfigFiles(
            (string VimFilePath, Matrix4x4 Transform)[] inputVimFilePathsAndTransforms,
            string mergedVimFilePath)
        {
            InputVimFilePathsAndTransforms = inputVimFilePathsAndTransforms;
            MergedVimFilePath = mergedVimFilePath;
        }

        /// <summary>
        /// Throws an exception if the input configuration is invalid.
        /// </summary>
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(MergedVimFilePath))
                throw new HResultException((int) ErrorCode.VimMergeConfigFilePathIsEmpty, "Merged VIM file path is empty.");

            var emptyFilePaths = InputVimFilePathsAndTransforms.Where(t => string.IsNullOrWhiteSpace(t.VimFilePath)).ToArray();
            if (emptyFilePaths.Length > 0)
            {
                var msg = string.Join(Environment.NewLine, emptyFilePaths.Select((t, i) => $"Input VIM file path at index {i} is empty."));
                throw new HResultException((int)ErrorCode.VimMergeInputFileNotFound, msg);
            }

            var notFoundFilePaths = InputVimFilePathsAndTransforms.Where(t => !File.Exists(t.VimFilePath)).ToArray();
            if (notFoundFilePaths.Length > 0)
            {
                var msg = string.Join(Environment.NewLine, notFoundFilePaths.Select(t => $"Input VIM file not found: {t.VimFilePath}"));
                throw new HResultException((int)ErrorCode.VimMergeInputFileNotFound, msg);
            }
        }
    }

    public class VimMergeConfigOptions
    {
        /// <summary>
        /// The generator string to embed into the VIM file header
        /// </summary>
        public string GeneratorString { get; set; } = "Unknown";

        /// <summary>
        /// The version string to embed into the VIM file header.
        /// </summary>
        public string VersionString { get; set; } = "0.0.0";

        /// <summary>
        /// Preserves the BIM data
        /// </summary>
        public bool KeepBimData { get; set; } = true;

        /// <summary>
        /// Merges the given VIM files as a grid.
        /// </summary>
        public bool MergeAsGrid { get; set; } = false;

        /// <summary>
        /// Applied when merging as a grid.
        /// </summary>
        public float GridPadding { get; set; } = 0f;

        /// <summary>
        /// Deduplicates Elements and EntityWithElements based on their Element's unique ids.
        /// If the unique ID is empty, the entities are not merged.
        /// </summary>
        public bool DeduplicateEntities { get; set; } = true;
    }

    public class VimMergeConfig
    {
        /// <summary>
        /// The input VIMs and their transforms.
        /// </summary>
        public (VIM Vim, Matrix4x4 Transform)[] InputVimsAndTransforms { get; }

        /// <summary>
        /// The input VIMs
        /// </summary>
        public VIM[] InputVimScenes
            => InputVimsAndTransforms.Select(t => t.Vim).ToArray();

        /// <summary>
        /// The input VIM transforms
        /// </summary>
        public Matrix4x4[] InputTransforms
            => InputVimsAndTransforms.Select(t => t.Transform).ToArray();

        /// <summary>
        /// Constructor.
        /// </summary>
        public VimMergeConfig((VIM Vim, Matrix4x4 Transform)[] inputVimScenesAndTransforms)
            => InputVimsAndTransforms = inputVimScenesAndTransforms;

        /// <summary>
        /// Constructor. Applies an identity matrix to the input VIMs
        /// </summary>
        public VimMergeConfig(VIM[] vims)
            : this(vims.Select(v => (v, Matrix4x4.Identity)).ToArray())
        { }
    }

    public class VimMergedTableBuilder
    {
        public readonly string Name;
        public int RowCount;

        public VimMergedTableBuilder(string name)
            => Name = name;

        public Dictionary<string, IBuffer> DataColumns = new Dictionary<string, IBuffer>();
        public DictionaryOfLists<string, int> IndexColumns = new DictionaryOfLists<string, int>();
        public DictionaryOfLists<string, string> StringColumns = new DictionaryOfLists<string, string>();

        public void AddTable(
            VimEntityTable entityTable,
            VimEntityTableSet parentSet,
            Dictionary<VimEntityTable, int> entityIndexOffsets)
        {
            Debug.Assert(entityIndexOffsets[entityTable] == RowCount);

            // Add index columns from the entity table
            foreach (var k in entityTable.IndexColumnMap.Keys)
            {
                var col = entityTable.IndexColumnMap[k];
                var indexColumnFullName = col.Name;

                // back-fill the index column if it doesn't exist.
                if (!IndexColumns.ContainsKey(indexColumnFullName))
                    IndexColumns.Add(indexColumnFullName, Enumerable.Repeat(ObjectModel.EntityRelation.None, RowCount).ToList());

                Debug.Assert(col.Array.Length == entityTable.RowCount);

                var offset = 0;
                if (parentSet.TryGetRelatedEntityTable(col, out var relatedTable) &&
                    entityIndexOffsets.TryGetValue(relatedTable, out var _offset))
                {
                    offset = _offset;
                }

                var vals = IndexColumns[indexColumnFullName];
                foreach (var v in col.Array)
                    vals.Add(v < 0 ? v : v + offset);
            }

            // Add data columns from the entity table 
            foreach (var k in entityTable.DataColumnMap.Keys)
            {
                var col = entityTable.DataColumnMap[k];
                if (!DataColumns.ContainsKey(k))
                {
                    DataColumns[k] = col;
                }
                else
                {
                    var cur = DataColumns[k];
                    DataColumns[k] = VimEntityTableColumnActions.ConcatDataColumnBuffers(cur, col, k.GetTypePrefix());
                }
            }

            var stringTable = parentSet.StringTable;

            // Add string columns from the entity table
            foreach (var k in entityTable.StringColumnMap.Keys)
            {
                if (!StringColumns.ContainsKey(k))
                    StringColumns.Add(k, Enumerable.Repeat("", RowCount).ToList());

                var col = entityTable.StringColumnMap[k];
                Debug.Assert(col.Array.Length == entityTable.RowCount);
                var vals = StringColumns[k];
                foreach (var v in col.Array)
                    vals.Add(stringTable[v]);
            }

            // For each column in the builder but not in the entity table add default values
            foreach (var kv in DataColumns)
            {
                var colName = kv.Key;
                if (!VimEntityTableColumnName.TryParseColumnTypePrefix(colName, out var typePrefix))
                    continue;

                if (!entityTable.DataColumnMap.ContainsKey(colName))
                {
                    var cur = DataColumns[colName];
                    var defaultBuffer = VimEntityTableColumnActions.CreateDefaultDataColumnBuffer(entityTable.RowCount, typePrefix);
                    DataColumns[colName] = VimEntityTableColumnActions.ConcatDataColumnBuffers(cur, defaultBuffer, typePrefix);
                }
            }

            foreach (var kv in IndexColumns)
            {
                if (!entityTable.IndexColumnMap.ContainsKey(kv.Key))
                    IndexColumns[kv.Key].AddRange(Enumerable.Repeat(-1, entityTable.RowCount));
            }

            foreach (var kv in StringColumns)
            {
                if (!entityTable.StringColumnMap.ContainsKey(kv.Key))
                    StringColumns[kv.Key].AddRange(Enumerable.Repeat("", entityTable.RowCount));
            }

            RowCount += entityTable.RowCount;

            foreach (var kv in DataColumns)
                Debug.Assert(kv.Value.Data.Length == RowCount);
            foreach (var kv in IndexColumns)
                Debug.Assert(kv.Value.Count == RowCount);
            foreach (var kv in StringColumns)
                Debug.Assert(kv.Value.Count == RowCount);
        }

        public void UpdateTableBuilder(VimEntityTableBuilder tb, CancellationToken cancellationToken = default)
        {
            foreach (var kv in DataColumns)
            {
                cancellationToken.ThrowIfCancellationRequested();
                tb.AddDataColumn(kv.Key, kv.Value);
            }

            foreach (var kv in StringColumns)
            {
                cancellationToken.ThrowIfCancellationRequested();
                tb.AddStringColumn(kv.Key, kv.Value.ToArray());
            }

            foreach (var kv in IndexColumns)
            {
                cancellationToken.ThrowIfCancellationRequested();
                tb.AddIndexColumn(kv.Key, kv.Value.ToArray());
            }
        }
    }

    public class VimRemappedEntityTableBuilder
    {
        public VimEntityTableBuilder EntityTableBuilder { get; }
        public int[] OldToNewIndexMap { get; }
        public bool IsRemapped => OldToNewIndexMap != null;

        /// <summary>
        /// Constructor
        /// </summary>
        private VimRemappedEntityTableBuilder(
            VimEntityTableBuilder entityTableBuilder,
            int[] oldToNewIndexMap)
        {
            EntityTableBuilder = entityTableBuilder;
            OldToNewIndexMap = oldToNewIndexMap;
        }

        /// <summary>
        /// Returns a default remapped entity table builder in which no remapping has occurred.
        /// </summary>
        private static VimRemappedEntityTableBuilder CreateDefault(VimEntityTableBuilder et)
            => new VimRemappedEntityTableBuilder(et, null);

        /// <summary>
        /// Returns a VimRemappedTableBuilder whose contained VimEntityTableBuilder is duplicated based on the given keyFn.
        /// </summary>
        private static VimRemappedEntityTableBuilder CreateRemapped<T>(
            VimEntityTableBuilder et,
            Func<int, VimEntityTableBuilder, T> keyFn)
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
                if (VimEntityTableColumnName.TryParseColumnTypePrefix(colName, out var typePrefix))
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

            return new VimRemappedEntityTableBuilder(remapped, oldToNewIndexMap);
        }

        private static void UpdateEntityTableBuilderRelations(
            List<VimRemappedEntityTableBuilder> remappedEntityTableBuilders,
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
                    var tableName = VimEntityTableColumnName.GetRelatedTableNameFromColumnName(indexColumnName);
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

        private static List<VimRemappedEntityTableBuilder> GetRemappedEntityTableBuilders(
            List<VimEntityTableBuilder> entityTableBuilders,
            CancellationToken ct = default)
        {
            var remappedEntityTableBuilders = new List<VimRemappedEntityTableBuilder>();

            // Deduplicate the entities.
            foreach (var table in entityTableBuilders)
            {
                ct.ThrowIfCancellationRequested();

                var tableName = table.Name;

                VimRemappedEntityTableBuilder r;
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
        /// Returns a new collection of entity table builders by deduplicating specific entities which would be
        /// meaninglessly duplicated in the resulting VIM file.
        /// </summary>
        public static IEnumerable<VimEntityTableBuilder> DeduplicateEntities(
            List<VimEntityTableBuilder> entityTableBuilders, CancellationToken ct = default)
            => GetRemappedEntityTableBuilders(entityTableBuilders, ct)
                .Select(r => r.EntityTableBuilder);

        // TODO: **I AM HERE; PORT MergeService.cs**
    }
}