using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vim.BFastLib;
using Vim.Math3d;
using Vim.Format.ObjectModel;
using Vim.Util;

namespace Vim.Format.Merge
{
    public static class MergeService2
    {
        public static IVimModel MergeVimModels(
            IVimModel[] vims,
            MergeConfigOptions optionsConfig = null,
            IProgress<string> progress = null,
            CancellationToken ct = default)
        {
            optionsConfig = optionsConfig ?? new MergeConfigOptions();

            // Validate that all VIMs are in the same object model major version
            ValidateSameObjectModelSchemaMajorVersion(vims);

            var db = new DocumentBuilder(optionsConfig.GeneratorString, SchemaVersion.Current, optionsConfig.VersionString);

            // Merge the entity data
            progress?.Report("Merging entities");
            ct.ThrowIfCancellationRequested();
            MergeEntities(db, vims.Select(v => v.Entities).ToArray(), optionsConfig.KeepBimData, ct);

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
                    db.Geometry.AddMaterial(ObjectModelStore.ConvertMaterialEntityFieldsToRenderableMaterial(
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

            var materialCounts = vims.SelectMany(v => v.Instances.Select(i => i.Mesh.Submeshes.Max(sm => sm.Material.Index))).ToArray();
            var materialOffsets = materialCounts.PostAccumulate((x, y) => x + y).DropLast();

            var meshes = vims
                .SelectMany((vim, vimIndex) => vim.Instances.Select(i => (i.Mesh, vimIndex)))
                .Select(
                    pair => new VimMesh2(
                        pair.Mesh.Index,
                        pair.Mesh.Submeshes.Select(
                            sm => (IVimSubmesh) new VimSubmesh2(
                                sm.Index,
                                new VimRenderMaterial(
                                    sm.Material.Index == -1
                                        ? -1
                                        : sm.Material.Index +
                                    materialOffsets[pair.vimIndex],
                                    sm.Material.Color,
                                    sm.Material.Glossiness,
                                    sm.Material.Smoothness
                                ),
                                sm.Vertices,
                                sm.Indices
                            )
                        ).ToArray())).ToArray();

            // TODO: implement AddMeshes for IVimMesh[]
            // db.Geometry.AddMeshes(meshes);

            ct.ThrowIfCancellationRequested();

            // Apply the optional grid transforms.
            var vimTransforms = new Matrix4x4[vims.Length]; 
            if (optionsConfig.MergeAsGrid)
            {
                progress?.Report("Calculating merge grid");
                vimTransforms = GetGridTransforms(vims, optionsConfig.GridPadding);
            }

            var meshCounts = vims.Select(v => v.Instances.Length).ToArray();
            var meshOffsets = meshCounts.PostAccumulate((x, y) => x + y).DropLast();

            // Merge the instances
            progress?.Report("Merging instances");
            ct.ThrowIfCancellationRequested();

            var allIdentity = vimTransforms.All(t => t.IsIdentity);
            var instances = vims
                .SelectMany((vim, vimIndex) => vim.Instances.Select(instance => (instance, vimIndex)))
                .Select(pair => new DocumentBuilder.Instance
                {
                    ParentIndex = -1,
                    InstanceFlags = pair.instance.InstanceFlags,
                    MeshIndex = pair.instance.Mesh == null ? -1 : pair.instance.Mesh.Index + meshOffsets[pair.vimIndex],
                    Transform = allIdentity
                        ? pair.instance.WorldTransform
                        : pair.instance.WorldTransform * vimTransforms[pair.vimIndex]
                }).ToArray();

            db.Geometry.AddInstances(instances);

            // Merge the assets
            progress?.Report("Merging assets");
            ct.ThrowIfCancellationRequested();
            foreach (var asset in vims.SelectMany(vim => vim.SerializableDocument.Assets))
                db.AddAsset(asset);

            // TODO: implement conversion from DocumentBuilder to IVimModel (or use an alternative approach)
            return null;
        }

        /// <summary>
        /// Throws if the given VIM files do not all have the same object model schema major version.
        /// </summary>
        private static void ValidateSameObjectModelSchemaMajorVersion(IVimModel[] vims)
        {
            var objectModelMajorVersions = vims
                .Select(v => v.SerializableDocument.Header.Schema.Major)
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
            foreach (var vim in vims.OrderBy(v => v.SerializableDocument.Header.Schema.Major))
                sb.AppendLine($"- v{vim.SerializableDocument.Header.Schema}: '{vim.FilePath}'");
            sb.AppendLine();
            sb.AppendLine("Please ensure the VIM files have all been exported with matching schema major versions.");

            throw new HResultException((int)ErrorCode.VimMergeObjectModelMajorVersionMismatch, sb.ToString());
        }

        /// <summary>
        /// Returns a collection of transforms based on the largest dimension of the largest VIM bounding box.
        /// </summary>
        private static Matrix4x4[] GetGridTransforms(IVimModel[] vims, float padding)
        {
            var boxes = vims.Select(v => v.BoundingBox).ToArray();
            var centerBottomTransforms = boxes.Select(b => Matrix4x4.CreateTranslation(-b.CenterBottom)).ToArray();

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

        private static IList<Matrix4x4> GetGridOfTransforms(int count, int numRows, float xSide, float ySide)
            => count.Select(i => Matrix4x4.CreateTranslation(i % numRows * xSide, i / numRows * ySide, 0));

        private static void MergeEntities(DocumentBuilder db, EntityTableSet[] entities, bool keepBimData, CancellationToken cancellationToken)
        {
            // Compute the offsets for the new entities
            var offsets = ComputeMergedEntityTableOffsets(entities);

            // Collect the entity tables, grouped by name 
            var mergedTableBuilders = new Dictionary<string, MergedTableBuilder>();
            foreach (var set in entities)
            {
                cancellationToken.ThrowIfCancellationRequested();

                foreach (var entityTable in set.RawTableMap.Values)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var name = entityTable.Name;
                    if (VimConstants.ComputedTableNames.Contains(entityTable.Name))
                        continue;

                    if (!keepBimData && !VimConstants.NonBimNames.Contains(entityTable.Name))
                        continue;

                    var mergedTableBuilder = mergedTableBuilders.GetOrCompute(name, (s) => new MergedTableBuilder(s));
                    // TODO: implement AddTableSet for EntityTableSet
                    // mergedTableBuilder.AddTable(entityTable, offsets);
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
        private static Dictionary<string, int> ComputeMergedEntityTableOffsets(EntityTableSet[] entities)
        {
            var aggregateOffsetMap = new Dictionary<string, int>();
            var entityTableOffsetMap = new Dictionary<string, int>();
            foreach (var set in entities)
            {
                foreach ((string name, var table) in set.RawTableMap)
                {
                    // Add the entity table name to the aggregate offset map
                    aggregateOffsetMap.TryAdd(name, 0);

                    // Assign the current offset to the given entity table.
                    entityTableOffsetMap.Add(name, aggregateOffsetMap[name]);

                    // Update the aggregated offsets
                    aggregateOffsetMap[name] += table.AllColumns.First().Data.Length;
                }
            }
            return entityTableOffsetMap;
        }
    }
}
