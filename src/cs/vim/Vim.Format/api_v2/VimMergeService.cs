using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vim.BFast;
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
        public VIM[] InputVims
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
            foreach (var colName in entityTable.DataColumnMap.Keys)
            {
                var col = entityTable.DataColumnMap[colName];
                if (!VimEntityTableColumnName.TryParseColumnTypePrefix(colName, out var typePrefix))
                    continue;

                if (!DataColumns.ContainsKey(colName))
                {
                    // back-fill the data column with default values.
                    var defaultBuffer = ColumnExtensions.CreateDefaultDataColumnBuffer(RowCount, typePrefix);
                    DataColumns[colName] = defaultBuffer;
                }

                var cur = DataColumns[colName];
                DataColumns[colName] = VimEntityTableColumnActions.ConcatDataColumnBuffers(cur, col, colName.GetTypePrefix());
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

    public static class VimMergeService
    {
        /// <summary>
        /// Merges the VIM files in the specified VimMergeConfigFiles object.
        /// </summary>
        public static void MergeVimFiles(
            VimMergeConfigFiles fileConfig,
            VimMergeConfigOptions optionsConfig,
            IProgress<string> progress = null,
            CancellationToken cancellationToken = default)
        {
            fileConfig.Validate();

            progress?.Report("Loading VIM files");
            cancellationToken.ThrowIfCancellationRequested();
            var inputVimsAndTransforms =
                fileConfig.InputVimFilePathsAndTransforms
                .AsParallel()
                .Select(t => (VIM.Open(t.VimFilePath), t.Transform))
                .ToArray();

            progress?.Report("Merging VIM files");
            cancellationToken.ThrowIfCancellationRequested();
            var (vimBuilder, vimEntityTableBuilders) = MergeVims(
                new VimMergeConfig(inputVimsAndTransforms),
                optionsConfig,
                progress,
                cancellationToken);

            progress?.Report("Writing merged VIM file");
            cancellationToken.ThrowIfCancellationRequested();
            var mergedVimFilePath = fileConfig.MergedVimFilePath;
            vimBuilder.Write(mergedVimFilePath, vimEntityTableBuilders);

            progress?.Report("Completed VIM file merge");
        }

        /// <summary>
        /// Merge the given VIM scenes into a VimBuilder
        /// </summary>
        public static (VimBuilder, VimEntityTableBuilder[]) MergeVims(
            VimMergeConfig vimMergeConfig,
            VimMergeConfigOptions optionsConfig = null,
            IProgress<string> progress = null,
            CancellationToken ct = default)
        {
            optionsConfig = optionsConfig ?? new VimMergeConfigOptions();

            // Validate that all VIMs are in the same object model major version
            var vims = vimMergeConfig.InputVims.ToArray();
            ValidateSameObjectModelSchemaMajorVersion(vims);

            var vimBuilder = new VimBuilder(optionsConfig.GeneratorString, ObjectModel.SchemaVersion.Current, optionsConfig.VersionString);

            // Merge the entity data
            progress?.Report("Merging entities");
            ct.ThrowIfCancellationRequested();
            var entityTableBuilders = MergeEntities(vims, optionsConfig.KeepBimData, ct);

            // Optionally deduplicate the entity data.
            if (optionsConfig.DeduplicateEntities)
            {
                progress?.Report("Deduplicating entities");
                ct.ThrowIfCancellationRequested();
                entityTableBuilders = VimEntityTableBuilderRemapped.DeduplicateEntities(entityTableBuilders, ct);
            }

            // Merge the materials
            //
            // IMPORTANT: there must be a 1:1 aligned relationship between the material entities and the renderable materials.
            // To ensure this constraint, We use the existing material entities in the document builder, whose entities have been
            // previously populated and optionally deduplicated above.
            progress?.Report("Merging materials");
            ct.ThrowIfCancellationRequested();

            var materialTable = entityTableBuilders.FirstOrDefault(et => et.Name == TableNames.Material);
            if (materialTable != null)
            {
                var mdcs = materialTable.DataColumns;

                var colorXColumn = mdcs.TryGetValue("double:Color.X", out var cX)
                    ? cX.AsArray<double>()
                    : Array.Empty<double>();

                var colorYColumn = mdcs.TryGetValue("double:Color.Y", out var cY)
                    ? cY.AsArray<double>()
                    : Array.Empty<double>();

                var colorZColumn = mdcs.TryGetValue("double:Color.Z", out var cZ)
                    ? cZ.AsArray<double>()
                    : Array.Empty<double>();

                var transparencyColumn = mdcs.TryGetValue("double:Transparency", out var t)
                    ? t.AsArray<double>()
                    : Array.Empty<double>();

                var glossinessColumn = mdcs.TryGetValue("double:Glossiness", out var g)
                    ? g.AsArray<double>()
                    : Array.Empty<double>();

                var smoothnessColumn = mdcs.TryGetValue("double:Smoothness", out var s)
                    ? s.AsArray<double>()
                    : Array.Empty<double>();

                for (var i = 0; i < materialTable.RowCount; ++i)
                {
                    vimBuilder.Materials.Add(ObjectModel.Material.ConvertMaterialEntityFieldsToRenderableMaterial(
                        colorX: (float)colorXColumn.ElementAtOrDefault(i),
                        colorY: (float)colorYColumn.ElementAtOrDefault(i),
                        colorZ: (float)colorZColumn.ElementAtOrDefault(i),
                        transparency: (float)transparencyColumn.ElementAtOrDefault(i),
                        glossiness: (float)glossinessColumn.ElementAtOrDefault(i),
                        smoothness: (float)smoothnessColumn.ElementAtOrDefault(i)
                    ));
                }
            }

            // Merge the geometry
            progress?.Report("Merging geometry");
            ct.ThrowIfCancellationRequested();

            var materialCounts = vims.Select(v => v.GeometryData.MaterialCount).ToArray();
            var materialOffsets = PartialSums(materialCounts); 

            vimBuilder.Meshes.AddRange(vims
                .SelectMany((vim, vimIndex) => vim.GeometryData.GetMeshViews().Select(mesh => (mesh, vimIndex)))
                .Select(
                    pair => new VimSubdividedMesh(
                        indices: pair.mesh.GetIndices(),
                        vertices: pair.mesh.GetVertices(),
                        submeshesIndexOffset: pair.mesh.GetSubmeshIndexOffsets(),
                        submeshMaterials: pair.mesh.GetSubmeshMaterials().Select(
                            mat => mat == -1 ? -1 : mat + materialOffsets[pair.vimIndex])?.ToList()
                    )
                )
                .ToList());

            ct.ThrowIfCancellationRequested();

            // Apply the optional grid transforms.
            var vimTransforms = vimMergeConfig.InputTransforms.ToArray(); 
            if (optionsConfig.MergeAsGrid)
            {
                progress?.Report("Calculating merge grid");
                var gridTransforms = GetGridTransforms(vims, optionsConfig.GridPadding);
                vimTransforms = gridTransforms.Zip(vimTransforms, (g, t) => g * t).ToArray();
            }

            var meshCounts = vims.Select(v => v.GeometryData.MeshCount).ToArray();
            var meshOffsets = PartialSums(meshCounts);

            // Merge the instances
            progress?.Report("Merging instances");
            ct.ThrowIfCancellationRequested();

            var isIdentityAll = vimTransforms.All(t => t.IsIdentity);
            for (var vimIndex = 0; vimIndex < vims.Length; ++vimIndex)
            {
                var vim = vims[vimIndex];
                var geometryData = vim.GeometryData;
                for (var instanceIndex = 0; instanceIndex < geometryData.InstanceCount; ++instanceIndex)
                {

                    var meshIndexRaw = geometryData.InstanceMeshes.ElementAtOrDefault(instanceIndex, -1);
                    var meshIndex = meshIndexRaw == -1
                        ? -1
                        : meshIndexRaw + meshOffsets[vimIndex];

                    var instanceTransformRaw = geometryData.InstanceTransforms.ElementAtOrDefault(instanceIndex, Matrix4x4.Identity);
                    var instanceTransform = isIdentityAll
                        ? instanceTransformRaw
                        : instanceTransformRaw * vimTransforms[vimIndex];

                    var vimInstance = new VimInstance()
                    {
                        ParentIndex = -1,
                        InstanceFlags = (InstanceFlags) geometryData.InstanceFlags.ElementAtOrDefault(instanceIndex),
                        MeshIndex = meshIndex,
                        Transform = instanceTransform
                    };

                    vimBuilder.Instances.Add(vimInstance);
                }
            }

            // Merge the assets
            progress?.Report("Merging assets");
            ct.ThrowIfCancellationRequested();
            foreach (var asset in vims.SelectMany(vim => vim.Assets))
                vimBuilder.AddAsset(asset);

            return (vimBuilder, entityTableBuilders);
        }

        private static int[] PartialSums(int[] self, int init = default)
        {
            var count = self.Length;
            var r = new int[count + 1];
            var prev = r[0] = init;
            if (count == 0) return r; 
            for (var i = 0; i < count; ++i)
            {
                prev = r[i + 1] = prev + self[i];
            }
            return r;
        }

        /// <summary>
        /// Throws if the given VIM files do not all have the same object model schema major version.
        /// </summary>
        public static void ValidateSameObjectModelSchemaMajorVersion(VIM[] vims)
        {
            var objectModelMajorVersions = vims
                .Select(v => v.Header.Schema.Major)
                .Distinct()
                .OrderBy(i => i)
                .ToArray();

            // If we are only dealing with one major object model schema version, we are fine.
            if (objectModelMajorVersions.Length == 1)
                return;

            // Throw otherwise with a helpful error message.
            var sb = new StringBuilder();
            sb.AppendLine($"Object model schema major version mismatch ({string.Join(", ", objectModelMajorVersions.Select(v => $"v{v}.*"))})");
            sb.AppendLine();
            foreach (var vim in vims.OrderBy(v => v.Header.Schema.Major))
                sb.AppendLine($"- v{vim.Header.Schema}: '{vim.FilePath}'");
            sb.AppendLine();
            sb.AppendLine("Please ensure the VIM files have all been exported with matching schema major versions.");

            throw new HResultException((int)ErrorCode.VimMergeObjectModelMajorVersionMismatch, sb.ToString());
        }

        /// <summary>
        /// Returns a collection of transforms based on the largest dimension of the largest VIM bounding box.
        /// </summary>
        public static Matrix4x4[] GetGridTransforms(VIM[] vims, float padding)
        {
            var boxes = vims.Select(v => v.GeometryData.GetWorldSpaceBoundingBox()).ToArray();
            var centerBottomTransforms = boxes.Select(b => Matrix4x4.CreateTranslation(-b.CenterBottom));

            var columnSize = boxes.Select(b => b.Extent.X).Max() + padding;
            var rowSize = boxes.Select(b => b.Extent.Y).Max() + padding;

            var numRows = (int)Math.Sqrt(vims.Length).Ceiling();

            var transforms = GetGridOfTransforms(
                vims.Length,
                numRows,
                columnSize,
                rowSize)
                .Zip(centerBottomTransforms, (g, b) => b * g)
                .ToArray();

            return transforms;
        }

        private static Matrix4x4[] GetGridOfTransforms(int count, int numRows, float xSide, float ySide)
        {
            var result = new Matrix4x4[count];
            for (var i = 0; i < result.Length; ++i)
            {
                result[i] = Matrix4x4.CreateTranslation(i % numRows * xSide, i / numRows * ySide, 0);
            }
            return result;
        }

        private static VimEntityTableBuilder[] MergeEntities(
            VIM[] vims,
            bool keepBimData = true,
            CancellationToken cancellationToken = default)
        {
            // Compute the offsets for the new entities
            var offsets = ComputeMergedEntityTableOffsets(vims);

            // Collect the entity tables, grouped by name 
            var mergedTableBuilders = new Dictionary<string, VimMergedTableBuilder>();
            foreach (var vim in vims)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var entityTableSet = vim.GetEntityTableSet();

                foreach (var entityTable in entityTableSet.Tables.Values)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var name = entityTable.Name;
                    if (VimEntityTableNames.ComputedTableNames.Contains(entityTable.Name))
                        continue;

                    if (!keepBimData && !VimEntityTableNames.NonBimNames.Contains(entityTable.Name))
                        continue;

                    var mergedTableBuilder = mergedTableBuilders.GetOrCompute(name, (s) => new VimMergedTableBuilder(s));
                    mergedTableBuilder.AddTable(entityTable, entityTableSet, offsets);
                }
            }

            // Generate the new merged entity table builder
            var entityTableBuilders = mergedTableBuilders.Values.Select(mtb => new VimEntityTableBuilder(mtb.Name)).ToArray();

            Parallel.For(0, entityTableBuilders.Length, i =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                var tb = entityTableBuilders[i];
                var mtb = mergedTableBuilders[tb.Name];
                mtb.UpdateTableBuilder(tb, cancellationToken);
            });

            return entityTableBuilders;
        }

        /// <summary>
        /// For every entity table in each document, computes the offset of that entity table
        /// in a merged document. This is used for remapping entity table relations when merging VIM files.
        /// </summary>
        private static Dictionary<VimEntityTable, int> ComputeMergedEntityTableOffsets(IEnumerable<VIM> vims)
        {
            var aggregateOffsetMap = new Dictionary<string, int>();
            var entityTableOffsetMap = new Dictionary<VimEntityTable, int>();
            foreach (var vim in vims)
            {
                foreach (var entityTable in vim.GetEntityTableSet().Tables.Values)
                {
                    var entityTableName = entityTable.Name;

                    // Add the entity table name to the aggregate offset map
                    aggregateOffsetMap.TryAdd(entityTableName, 0);

                    // Assign the current offset to the given entity table.
                    entityTableOffsetMap.Add(entityTable, aggregateOffsetMap[entityTableName]);

                    // Update the aggregated offsets
                    aggregateOffsetMap[entityTableName] += entityTable.RowCount;
                }
            }
            return entityTableOffsetMap;
        }
    }
}