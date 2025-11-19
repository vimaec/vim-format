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
        public int NumRows;

        public VimMergedTableBuilder(string name)
            => Name = name;

        public Dictionary<string, IBuffer> DataColumns = new Dictionary<string, IBuffer>();
        public DictionaryOfLists<string, int> IndexColumns = new DictionaryOfLists<string, int>();
        public DictionaryOfLists<string, string> StringColumns = new DictionaryOfLists<string, string>();

        public void AddTable(VimEntityTable entityTable, VimEntityTableSet parentSet, Dictionary<VimEntityTable, int> entityIndexOffsets)
        {
            Debug.Assert(entityIndexOffsets[entityTable] == NumRows);

            // Add index columns from the entity table
            foreach (var k in entityTable.IndexColumnMap.Keys)
            {
                var col = entityTable.IndexColumnMap[k];
                var indexColumnFullName = col.Name;

                // back-fill the index column if it doesn't exist.
                if (!IndexColumns.ContainsKey(indexColumnFullName))
                    IndexColumns.Add(indexColumnFullName, Enumerable.Repeat(ObjectModel.EntityRelation.None, NumRows).ToList());

                Debug.Assert(col.Array.Length == entityTable.RowCount);

                var offset = 0;
                if (parentSet.TryGetRelatedEntityTable(col, out var relatedTable) && entityIndexOffsets.TryGetValue(relatedTable, out var _offset))
                {
                    offset = _offset;
                }

                // TODO: I AM HERE

                var vals = IndexColumns[indexColumnFullName];
                foreach (var v in col.Array)
                    vals.Add(v < 0 ? v : v + offset);
            }

            // Add data columns from the entity table 
            foreach (var colName in entityTable.DataColumns.Keys.ToEnumerable())
            {
                var col = entityTable.DataColumns[colName];
                if (!DataColumns.ContainsKey(colName))
                {
                    DataColumns[colName] = col;
                }
                else
                {
                    var cur = DataColumns[colName];
                    DataColumns[colName] = cur.ConcatDataColumnBuffers(col, colName.GetTypePrefix());
                }
            }

            // Add string columns from the entity table 
            foreach (var k in entityTable.StringColumns.Keys.ToEnumerable())
            {
                if (!StringColumns.ContainsKey(k))
                    StringColumns.Add(k, Enumerable.Repeat("", NumRows).ToList());

                var col = entityTable.StringColumns[k];
                Debug.Assert(col.Array.Length == entityTable.NumRows);
                var vals = StringColumns[k];
                foreach (var v in col.Array)
                    vals.Add(entityTable.Document.GetString(v));
            }

            // For each column in the builder but not in the entity table add default values
            foreach (var kv in DataColumns)
            {
                var colName = kv.Key;
                var typePrefix = colName.GetTypePrefix();
                if (!entityTable.DataColumns.Contains(colName))
                {
                    var cur = DataColumns[colName];
                    var defaultBuffer = ColumnExtensions.CreateDefaultDataColumnBuffer(entityTable.NumRows, typePrefix);
                    DataColumns[colName] = cur.ConcatDataColumnBuffers(defaultBuffer, typePrefix);
                }
            }

            foreach (var kv in IndexColumns)
            {
                if (!entityTable.IndexColumns.Contains(kv.Key))
                    IndexColumns[kv.Key].AddRange(Enumerable.Repeat(-1, entityTable.NumRows));
            }

            foreach (var kv in StringColumns)
            {
                if (!entityTable.StringColumns.Contains(kv.Key))
                    StringColumns[kv.Key].AddRange(Enumerable.Repeat("", entityTable.NumRows));
            }

            NumRows += entityTable.NumRows;

            foreach (var kv in DataColumns)
                Debug.Assert(kv.Value.Data.Length == NumRows);
            foreach (var kv in IndexColumns)
                Debug.Assert(kv.Value.Count == NumRows);
            foreach (var kv in StringColumns)
                Debug.Assert(kv.Value.Count == NumRows);
        }

        public void UpdateTableBuilder(EntityTableBuilder tb, CancellationToken cancellationToken = default)
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
}