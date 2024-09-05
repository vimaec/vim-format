using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vim.BFast;
using Vim.Format.Geometry;
using Vim.LinqArray;
using Vim.Math3d;
using Vim.Format.ObjectModel;

using Vim.Util;

namespace Vim.Format.Merge
{
    public static class MergeService
    {
        /// <summary>
        /// Merges the VIM files in the specified MergeConfiguration object.
        /// </summary>
        public static void MergeVimFiles(
            MergeConfigFiles fileConfig,
            MergeConfigOptions optionsConfig,
            IProgress<string> progress = null,
            CancellationToken cancellationToken = default)
        {
            fileConfig.Validate();

            progress?.Report("Loading VIM files");
            cancellationToken.ThrowIfCancellationRequested();
            var inputVimScenesAndTransforms =
                fileConfig.InputVimFilePathsAndTransforms
                .AsParallel()
                .Select(t => (VimScene.LoadVim(t.VimFilePath), t.Transform))
                .ToArray();

            progress?.Report("Merging VIM files");
            cancellationToken.ThrowIfCancellationRequested();
            var documentBuilder = MergeVimScenes(
                new MergeConfigVimScenes(inputVimScenesAndTransforms),
                optionsConfig,
                progress,
                cancellationToken);

            progress?.Report("Writing merged VIM file");
            cancellationToken.ThrowIfCancellationRequested();
            var mergedVimFilePath = fileConfig.MergedVimFilePath;
            documentBuilder.Write(mergedVimFilePath);

            progress?.Report("Completed VIM file merge");
        }

        /// <summary>
        /// Merge the given VIM scenes into a DocumentBuilder
        /// </summary>
        public static DocumentBuilder MergeVimScenes(
            MergeConfigVimScenes vimSceneConfig,
            MergeConfigOptions optionsConfig = null,
            IProgress<string> progress = null,
            CancellationToken ct = default)
        {
            optionsConfig = optionsConfig ?? new MergeConfigOptions();

            // Validate that all VIMs are in the same object model major version
            var vims = vimSceneConfig.InputVimScenes.ToArray();
            ValidateSameObjectModelSchemaMajorVersion(vims);

            var db = new DocumentBuilder(optionsConfig.GeneratorString, SchemaVersion.Current, optionsConfig.VersionString);

            // Merge the entity data
            progress?.Report("Merging entities");
            ct.ThrowIfCancellationRequested();
            MergeEntities(db, vims, optionsConfig.KeepBimData, ct);

            // Optionally deduplicate the entity data.
            if (optionsConfig.DeduplicateEntities)
            {
                progress?.Report("Deduplicating entities");
                ct.ThrowIfCancellationRequested();
                RemappedEntityTableBuilder.DeduplicateEntities(db, ct);
            }

            // Merge the materials
            //
            // IMPORTANT: there must be a 1:1 aligned relationship between the material entities and the renderable materials.
            // To ensure this constraint, We use the existing material entities in the document builder, whose entities have been
            // previously populated and optionally deduplicated above.
            progress?.Report("Merging materials");
            ct.ThrowIfCancellationRequested();

            var materialTable = db.Tables.Values.FirstOrDefault(et => et.Name == TableNames.Material);
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

                for (var i = 0; i < materialTable.NumRows; ++i)
                {
                    db.Materials.Add(ObjectModelStore.ConvertMaterialEntityFieldsToRenderableMaterial(
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

            var materialCounts = vims.Select(v => v.Materials.Count);
            var materialOffsets = materialCounts.ToIArray().PartialSums().DropLast();

            db.Meshes.AddRange(vims
                .SelectMany((vim, vimIndex) => vim.Meshes.Select(mesh => (mesh, vimIndex)).ToEnumerable())
                .Select(
                    pair => new DocumentBuilder.SubdividedMesh(
                        indices: pair.mesh.Indices?.ToList(),
                        vertices: pair.mesh.Vertices?.ToList(),
                        submeshesIndexOffset: pair.mesh.SubmeshIndexOffsets?.ToList(),
                        submeshMaterials: pair.mesh.SubmeshMaterials.Select(
                            mat => mat == -1 ? -1 : mat + materialOffsets[pair.vimIndex])?.ToList()
                    )
                )
                .ToList());

            ct.ThrowIfCancellationRequested();

            // Apply the optional grid transforms.
            var vimTransforms = vimSceneConfig.InputTransforms.ToArray(); 
            if (optionsConfig.MergeAsGrid)
            {
                progress?.Report("Calculating merge grid");
                var gridTransforms = GetGridTransforms(vims, optionsConfig.GridPadding);
                vimTransforms = gridTransforms.Zip(vimTransforms, (g, t) => g * t).ToArray();
            }

            var meshCounts = vims.Select(v => v.Meshes.Count);
            var meshOffsets = meshCounts.ToIArray().PartialSums().DropLast();

            // Merge the instances
            progress?.Report("Merging instances");
            ct.ThrowIfCancellationRequested();

            var allIdentity = vimTransforms.All(t => t.IsIdentity);
            db.Instances.AddRange(
                vims
                .SelectMany((vim, vimIndex) => vim.VimNodes.Select(node => (node, vimIndex)).ToEnumerable())
                .Select(pair => new DocumentBuilder.Instance()
                {
                    ParentIndex = -1,
                    InstanceFlags = pair.node.InstanceFlags,
                    MeshIndex = pair.node.MeshIndex == -1 ? -1 : pair.node.MeshIndex + meshOffsets[pair.vimIndex],
                    Transform = allIdentity
                        ? pair.node.Transform
                        : pair.node.Transform * vimTransforms[pair.vimIndex]
                }).ToList());

            // Merge the assets
            progress?.Report("Merging assets");
            ct.ThrowIfCancellationRequested();
            foreach (var asset in vims.SelectMany(vim => vim.Document.Assets.Values.ToEnumerable()))
                db.AddAsset(asset);

            return db;
        }

        /// <summary>
        /// Throws if the given VIM files do not all have the same object model schema major version.
        /// </summary>
        public static void ValidateSameObjectModelSchemaMajorVersion(VimScene[] vims)
        {
            var objectModelMajorVersions = vims
                .Select(v => v.Document.Header.Schema.Major)
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
            foreach (var vim in vims.OrderBy(v => v.Document.Header.Schema.Major))
                sb.AppendLine($"- v{vim.Document.Header.Schema.ToString()}: '{vim.FileName}'");
            sb.AppendLine();
            sb.AppendLine("Please ensure the VIM files have all been exported with matching schema major versions.");

            throw new HResultException((int)ErrorCode.VimMergeObjectModelMajorVersionMismatch, sb.ToString());
        }

        /// <summary>
        /// Returns a collection of transforms based on the largest dimension of the largest VIM bounding box.
        /// </summary>
        public static Matrix4x4[] GetGridTransforms(VimScene[] vims, float padding)
        {
            var boxes = vims.Select(v => v.BoundingBox()).ToArray();
            var centerBottomTransforms = boxes.Select(b => Matrix4x4.CreateTranslation(-b.CenterBottom)).ToIArray();

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

        private static IArray<Matrix4x4> GetGridOfTransforms(int count, int numRows, float xSide, float ySide)
            => count.Select(i => Matrix4x4.CreateTranslation(i % numRows * xSide, i / numRows * ySide, 0));

        private static void MergeEntities(
            DocumentBuilder db,
            VimScene[] vims,
            bool keepBimData = true,
            CancellationToken cancellationToken = default)
        {
            // Compute the offsets for the new entities
            var offsets = ComputeMergedEntityTableOffsets(vims.Select(v => v.Document));

            // Collect the entity tables, grouped by name 
            var mergedTableBuilders = new Dictionary<string, MergedTableBuilder>();
            foreach (var vim in vims)
            {
                cancellationToken.ThrowIfCancellationRequested();

                foreach (var entityTable in vim.Document.EntityTables.Values.ToEnumerable())
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var name = entityTable.Name;
                    if (VimConstants.ComputedTableNames.Contains(entityTable.Name))
                        continue;

                    if (!keepBimData && !VimConstants.NonBimNames.Contains(entityTable.Name))
                        continue;

                    var mergedTableBuilder = mergedTableBuilders.GetOrCompute(name, (s) => new MergedTableBuilder(s));
                    mergedTableBuilder.AddTable(entityTable, offsets);
                }
            }

            // Add the new merged table builder 
            var tableBuilders = mergedTableBuilders.Values.Select(mtb => db.GetTableBuilderOrCreate(mtb.Name)).ToArray();
            Parallel.For(0, tableBuilders.Length, i =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                var tb = tableBuilders[i];
                var mtb = mergedTableBuilders[tb.Name];
                mtb.UpdateTableBuilder(tb, cancellationToken);
            });
        }

        /// <summary>
        /// For every entity table in each document, computes the offset of that entity table
        /// in a merged document. This is used for remapping entity table relations when merging VIM files.
        /// </summary>
        private static Dictionary<EntityTable, int> ComputeMergedEntityTableOffsets(IEnumerable<Document> documents)
        {
            var aggregateOffsetMap = new Dictionary<string, int>();
            var entityTableOffsetMap = new Dictionary<EntityTable, int>();
            foreach (var doc in documents)
            {
                foreach (var entityTable in doc.EntityTables.Values.ToEnumerable())
                {
                    var entityTableName = entityTable.Name;

                    // Add the entity table name to the aggregate offset map
                    aggregateOffsetMap.TryAdd(entityTableName, 0);

                    // Assign the current offset to the given entity table.
                    entityTableOffsetMap.Add(entityTable, aggregateOffsetMap[entityTableName]);

                    // Update the aggregated offsets
                    aggregateOffsetMap[entityTableName] += entityTable.NumRows;
                }
            }
            return entityTableOffsetMap;
        }
    }
}
