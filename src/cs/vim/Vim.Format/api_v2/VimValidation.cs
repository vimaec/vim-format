using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Vim.Math3d;
using Vim.Util;

namespace Vim.Format.api_v2
{
    public static class VimValidation
    {
        public static void Validate(this VIM vim, VimValidationOptions options = null)
        {
            options = options ?? new VimValidationOptions();

            ValidateTableRows(vim);
            ValidateIndexColumns(vim);
            ValidateGeometryBuffers(vim);
            ValidateAssetBuffers(vim);
            ValidateEntitiesWithElement(vim);
            ValidateBimDocument(vim, options);
            ValidateCompoundStructures(vim);
            ValidateAssets(vim);
            ValidateParameters(vim);
            ValidatePhases(vim);
            if (options.UniqueStorageKeys) ValidateStorageKeys(vim);
            if (options.ElementInSystemMustNotHaveNullValues) ValidateElementInSystem(vim);
            ValidateMaterials(vim);
            ValidateEntityAndGeometryInvariants(vim);
            ValidateNodes(vim);
        }

        public static void ValidateTableRows(VIM vim)
        {
            foreach (var et in vim.EntityTableData)
            {
                var rowCount = et.GetRowCount();
                foreach (var c in et.IndexColumns)
                {
                    if (c.Data.Length != rowCount)
                        throw new VimValidationException($"Expected array length {c.Data.Length} of column {c.Name} to be the same as number of rows {rowCount}");
                }

                foreach (var c in et.StringColumns)
                {
                    if (c.Data.Length != rowCount)
                        throw new VimValidationException($"Expected array length {c.Data.Length} of column {c.Name} to be the same as number of rows {rowCount}");
                }

                foreach (var c in et.DataColumns)
                {
                    if (c.Data.Length != rowCount)
                        throw new VimValidationException($"Expected array length {c.Data.Length} of column {c.Name} to be the same as number of rows {rowCount}");
                }
            }
        }

        public static void ValidateIndexColumns(VIM vim)
        {
            var tableSet = vim.GetEntityTableSet();

            foreach (var kv in tableSet.Tables)
            {
                var et = kv.Value;
                foreach (var ixKv in et.IndexColumnMap)
                {
                    var indexColumn = ixKv.Value;

                    var relatedTableName = VimEntityTableColumnName.GetRelatedTableName(indexColumn);

                    if (!tableSet.TryGetEntityTable(relatedTableName, out var table))
                        throw new VimValidationException($"Could not find related table for index column {indexColumn.Name}");
                }
            }
        }

        public static void ValidateGeometryBuffers(VIM vim)
        {
            var geometryData = vim.GeometryData;

            if (geometryData.Header == null) throw new VimValidationException("VIM geometry data header is null");
            if (geometryData.Indices == null) throw new VimValidationException("VIM geometry indices is null");
            if (geometryData.Vertices == null) throw new VimValidationException("VIM geometry vertices is null");
            if (geometryData.MeshSubmeshOffsets == null) throw new VimValidationException("VIM geometry mesh submesh offests is null");
            if (geometryData.SubmeshIndexOffsets == null) throw new VimValidationException("VIM geometry submesh index offsets is null");
            if (geometryData.InstanceMeshes == null) throw new VimValidationException("VIM geometry instance meshes is null");
            if (geometryData.InstanceTransforms == null) throw new VimValidationException("VIM geometry instance transforms is null");
        }

        public static void ValidateAssetBuffers(VIM vim)
        {
            foreach (var asset in vim.Assets)
                VimAssetInfo.Parse(asset.Name); // This will throw if it fails to parse.
        }

        public static void ValidateEntitiesWithElement(VIM vim)
        {
            // All entities with element must have a valid element index.

            var entityWithElementTypes = new HashSet<Type>(
                ObjectModel.ObjectModelReflection.GetEntityTypes<ObjectModel.EntityWithElement>()
                .Where(t => t.GetCustomAttributes(typeof(ObjectModel.G3dAttributeReferenceAttribute), true).Count() == 0)
            );

            var entityWithElementTypesAndTableNames = entityWithElementTypes
                .Select(t => (t, (t.GetCustomAttribute(typeof(TableNameAttribute)) as TableNameAttribute)?.Name))
                .Where(tuple => !string.IsNullOrEmpty(tuple.Item2));

            var tableSet = vim.GetEntityTableSet();
            var elementCount = tableSet.ElementTable.RowCount;

            Parallel.ForEach(entityWithElementTypesAndTableNames, entityWithElementTypeAndTableName =>
            {
                var (entityType, tableName) = entityWithElementTypeAndTableName;
                if (!tableSet.TryGetEntityTable(tableName, out var entityTable))
                    return;

                var elementIndices = entityTable.GetType().GetProperty("Column_ElementIndex")?.GetValue(entityTable) as int[];
                for (var i = 0; i < elementIndices.Length; ++i)
                {
                    var elementIndex = elementIndices[i];
                    if (elementIndex < 0)
                        throw new VimValidationException($"{nameof(ObjectModel.EntityWithElement)} {tableName} @{i} has a negative element index: {elementIndex}.");
                    if (elementIndex >= elementCount)
                        throw new VimValidationException($"{nameof(ObjectModel.EntityWithElement)} {tableName} @{i} has an invalid element index: {elementIndex}; element count: {elementCount}");
                }
            });
        }

        public static void ValidateBimDocument(VIM vim, VimValidationOptions validationOptions)
        {
            var tableSet = vim.GetEntityTableSet();

            // There is at least one BimDocument in the document model.
            if (tableSet.BimDocumentTable.RowCount == 0 && validationOptions.BimDocumentMustExist)
                throw new VimValidationException($"No {nameof(ObjectModel.BimDocument)} found.");

            foreach (var bd in tableSet.BimDocumentTable)
            {
                var bdElement = bd.Element;
                if (bdElement == null)
                    throw new VimValidationException($"{nameof(ObjectModel.BimDocument)} @{bd.Index} has null {nameof(ObjectModel.Element)}.");

                var expectedElementId = VimEntityTableConstants.SyntheticElementId;
                if (validationOptions.BimDocumentElementMustBeSynthetic && bdElement.Id != expectedElementId)
                    throw new VimValidationException($"{nameof(ObjectModel.BimDocument)} @{bd.Index} - Related {nameof(ObjectModel.Element)}.{nameof(ObjectModel.Element.Id)} @{bdElement.Index} is not {expectedElementId}");

                var expectedName = bd.Name;
                if (validationOptions.BimDocumentElementNameMustMatchBimDocumentName && bdElement.Name != expectedName)
                    throw new VimValidationException($"{nameof(ObjectModel.BimDocument)} @{bd.Index} - Related {nameof(ObjectModel.Element)}.{nameof(ObjectModel.Element.Name)} @{bdElement.Index} does not match {nameof(ObjectModel.BimDocument)}.{nameof(ObjectModel.BimDocument.Name)} ({expectedName})");

                var expectedElementType = VimConstants.BimDocumentParameterHolderElementType;
                if (validationOptions.BimDocumentElementTypeMustBeParameterHolder && bdElement.Type != expectedElementType)
                    throw new VimValidationException($"{nameof(ObjectModel.BimDocument)} @{bd.Index} - Related {nameof(ObjectModel.Element)}.{nameof(ObjectModel.Element.Type)} @{bdElement.Index} is not '{expectedElementType}'.");
            }
        }

        public static void ValidateCompoundStructureLayer(ObjectModel.CompoundStructureLayer layer)
        {
            // All CompoundLayers have a CompoundStructure
            if (layer.CompoundStructure == null)
                throw new VimValidationException($"{nameof(ObjectModel.CompoundStructureLayer)} {layer.Index} has null {nameof(ObjectModel.CompoundStructure)}");
        }

        public static void ValidateCompoundStructures(VIM vim)
        {
            var tableSet = vim.GetEntityTableSet();

            var cslArray = tableSet.CompoundStructureLayerTable.ToArray();

            // All compound structure layers are valid
            foreach (var csl in cslArray)
                ValidateCompoundStructureLayer(csl);

            // There are no gaps in the order index of compound structure layers.
            foreach (var cslCluster in cslArray.GroupBy(csl => csl._CompoundStructure.Index))
            {
                var ordered = cslCluster.OrderBy(csl => csl.OrderIndex).ToArray();
                for (var i = 0; i < ordered.Length; ++i)
                {
                    var orderIndex = ordered[i].OrderIndex;
                    if (orderIndex != i)
                        throw new VimValidationException($"{nameof(ObjectModel.CompoundStructureLayer.OrderIndex)} {orderIndex} does not match expected value of {i}");
                }

                var csIndex = cslCluster.Key;
                var csStructuralLayerIndex = tableSet.CompoundStructureTable.Column_StructuralLayerIndex.ElementAtOrDefault(csIndex, ObjectModel.EntityRelation.None);
                if (csStructuralLayerIndex != ObjectModel.EntityRelation.None)
                {
                    // If the CompoundStructure.StructuralMaterial exists, it should be a valid relation to a CompoundStructureLayer
                    var maxIndex = cslArray.Length - 1;
                    if (csStructuralLayerIndex < 0 || csStructuralLayerIndex > maxIndex)
                        throw new VimValidationException($"{nameof(ObjectModel.CompoundStructure)} {csIndex} has an invalid {nameof(ObjectModel.CompoundStructure.StructuralLayer)} relation index of {csStructuralLayerIndex}. Expected value between [0..{maxIndex}]");
                }
            }

            // All FamilyTypes with compound structures are system families.
            var ftArray = tableSet.FamilyTypeTable.ToArray();
            var cslRelationIndices = new HashSet<int>(cslArray.Select(l => l.CompoundStructure.Index));
            var ftRelationIndices = new HashSet<int>(ftArray.Where(ft => ft.CompoundStructure != null).Select(ft => ft.CompoundStructure.Index));
            var csArray = tableSet.CompoundStructureTable.ToArray();
            foreach (var cs in csArray)
            {
                // All compound structures are referenced by at least one compound structure layer
                if (!cslRelationIndices.Contains(cs.Index))
                    throw new VimValidationException($"{nameof(ObjectModel.CompoundStructure)} index: {cs.Index} does not have a corresponding {nameof(ObjectModel.CompoundStructureLayer)}");

                // All compound structures are referenced by at least one family type.
                if (!ftRelationIndices.Contains(cs.Index))
                    throw new VimValidationException($"{nameof(ObjectModel.CompoundStructure)} index: {cs.Index} does not have a corresponding {nameof(ObjectModel.FamilyType)}");
            }

            // A compound structure must be referenced by exactly one family type (no re-using compound structures).
            if (ftRelationIndices.Count != ftRelationIndices.Distinct().Count())
                throw new VimValidationException($"A {nameof(ObjectModel.CompoundStructure)} must be referenced by exactly one {nameof(ObjectModel.FamilyType)}.");
        }

        public static void ValidateAssets(VIM vim)
        {
            // Validate that the assets contained in the buffers matches the assets entity table.
            var assetBuffers = vim.Assets;
            var assetEntities = vim.GetEntityTableSet().AssetTable.ToArray();
            foreach (var asset in assetEntities)
            {
                if (assetBuffers.FirstOrDefault(ab => ab.Name == asset.BufferName) == null)
                    throw new VimValidationException($"No matching asset buffer found for asset entity {asset.Index} with {nameof(asset.BufferName)} '{asset.BufferName}'");
            }
        }

        public static void ValidateParameters(VIM vim)
        {
            var tableSet = vim.GetEntityTableSet();
            Parallel.ForEach(tableSet.ParameterTable, p =>
            {
                // Each parameter must be associated to an element.
                if (p._Element.Index == ObjectModel.EntityRelation.None)
                    throw new VimValidationException($"{nameof(ObjectModel.Element)} not found for {nameof(ObjectModel.Parameter)} {p.Index}");

                // Each parameter must have a parameter descriptor.
                if (p.ParameterDescriptor == null)
                    throw new VimValidationException($"{nameof(ObjectModel.ParameterDescriptor)} is null for {nameof(ObjectModel.Parameter)} {p.Index}");
            });

            // Validate the parameter descriptors.
            foreach (var pd in tableSet.ParameterDescriptorTable)
            {
                if (pd.DisplayUnit == null)
                    throw new VimValidationException($"{nameof(ObjectModel.DisplayUnit)} is null for {nameof(ObjectModel.ParameterDescriptor)} {pd.Index}");
            }
        }

        public static void ValidatePhases(VIM vim)
        {
            // Validate the phase order information.
            var tableSet = vim.GetEntityTableSet();
            var poArray = tableSet.PhaseOrderInBimDocumentTable.ToArray();
            foreach (var po in poArray)
            {
                var bd = po.BimDocument;
                if (bd == null)
                    throw new VimValidationException($"{nameof(ObjectModel.BimDocument)} is null for {nameof(ObjectModel.PhaseOrderInBimDocument)} {po.Index}");

                var phase = po.Phase;
                if (phase == null)
                    throw new VimValidationException($"{nameof(ObjectModel.Phase)} is null for {nameof(ObjectModel.PhaseOrderInBimDocument)} {po.Index}");
            }

            // Validate the order indices in the bim documents.
            foreach (var g in poArray.GroupBy(po => po._BimDocument.Index))
            {
                var ordered = g.OrderBy(v => v.OrderIndex).ToArray();
                for (var i = 0; i < ordered.Length; ++i)
                {
                    var po = ordered[i];
                    var orderIndex = po.OrderIndex;
                    if (orderIndex != i)
                        throw new VimValidationException($"Unexpected OrderIndex {orderIndex}; expected {i} in {nameof(ObjectModel.PhaseOrderInBimDocument)} {po.Index}");
                }
            }

            // Validate that the phase order information covers the set of phases.
            var phaseIndexSet = new HashSet<int>(tableSet.PhaseTable.Select(p => p.Index));
            phaseIndexSet.ExceptWith(poArray.Select(po => po.Index));
            if (phaseIndexSet.Count != 0)
                throw new VimValidationException($"{nameof(ObjectModel.Phase)} index coverage is incomplete among {nameof(ObjectModel.PhaseOrderInBimDocument)}");
        }

        /// <summary>
        /// Validate that the entity tables whose entities inherit from IStorageKey all have unique storage key values.
        /// </summary>
        public static void ValidateStorageKeys(VIM vim)
        {
            var tableSet = vim.GetEntityTableSet();
            var storageKeyTables = tableSet.Tables.Values.OfType<IEnumerable<ObjectModel.IStorageKey>>();
            foreach (var storageKeyTable in storageKeyTables)
            {
                ValidateStorageKeyTable(storageKeyTable);
            }
        }

        /// <summary>
        /// Generic storage key check; ensures that the keys only appear once in the keySet.
        /// </summary>
        public static void ValidateStorageKeyTable(IEnumerable<ObjectModel.IStorageKey> storageKeyEntities)
        {
            var keySet = new HashSet<object>();
            foreach (var entity in storageKeyEntities)
            {
                var key = entity.GetStorageKey();
                if (!keySet.Add(key))
                    throw new VimValidationException($"Duplicate storage key ({key}) found for {entity.GetType().Name}");
            }
        }

        public static void ValidateElementInSystem(VIM vim)
        {
            foreach (var eis in vim.GetEntityTableSet().ElementInSystemTable)
            {
                if (eis.System == null)
                    throw new VimValidationException($"{nameof(ObjectModel.ElementInSystem)} @ {eis.Index} has a null {nameof(ObjectModel.ElementInSystem.System)}");

                if (eis.Element == null)
                    throw new VimValidationException($"{nameof(ObjectModel.ElementInSystem)} @ {eis.Index} has a null {nameof(ObjectModel.ElementInSystem.Element)}");
            }
        }

        public static void ValidateMaterials(VIM vim)
        {
            foreach (var material in vim.GetEntityTableSet().MaterialTable)
            {
                var index = material.Index;
                ValidateDVector3Domain(nameof(material.Color), material.Color, DVector3.Zero, DVector3.One, index);
                ValidateDomain(nameof(material.Glossiness), material.Glossiness, 0d, 1d, index);
                ValidateDomain(nameof(material.Smoothness), material.Smoothness, 0d, 1d, index);
                ValidateDomain(nameof(material.Transparency), material.Transparency, 0d, 1d, index);
                ValidateDomain(nameof(material.NormalAmount), material.NormalAmount, 0d, 1d, index);
            }
        }

        public static void ValidateDomain(string label, double value, double lowerInclusive, double upperInclusive, int index)
        {
            if (value < lowerInclusive || value > upperInclusive)
                throw new VimValidationException($"{label} {value} is not in the range [{lowerInclusive}..{upperInclusive}] for material {index}");
        }

        public static void ValidateDVector3Domain(string label, DVector3 value, DVector3 lowerInclusive, DVector3 upperInclusive, int index)
        {
            if (value.X < lowerInclusive.X ||
                value.Y < lowerInclusive.Y ||
                value.Z < lowerInclusive.Z ||
                value.X > upperInclusive.X ||
                value.Y > upperInclusive.Y ||
                value.Z > upperInclusive.Z)
            {
                throw new VimValidationException($"{label} {value} is not in the range [{lowerInclusive}..{upperInclusive}] for material {index}");
            }
        }

        public static void ValidateGeometry(VIM vim)
        {
            var geometryData = vim.GeometryData;

            var indexCount = geometryData.IndexCount;
            var vertexCount = geometryData.VertexCount;
            var meshCount = geometryData.MeshCount;
            var meshSubmeshOffset = geometryData.MeshSubmeshOffsets;
            var meshSubmeshCount = geometryData.MeshSubmeshCount;
            var submeshCount = geometryData.SubmeshCount;
            var submeshMaterialCount = geometryData.SubmeshMaterials.Length;
            var submeshIndexOffsets = geometryData.SubmeshIndexOffsets;
            var submeshIndexOffsetCount = submeshIndexOffsets.Length;
            var submeshIndexCount = geometryData.SubmeshIndexCount;
            var instanceCount = geometryData.InstanceCount;
            var instanceParentCount = geometryData.InstanceParents.Length;
            var instanceMeshesCount = geometryData.InstanceMeshes.Length;
            var instanceTransformsCount = geometryData.InstanceTransforms.Length;
            var instanceFlagsCount = geometryData.InstanceFlags.Length;
            var materialCount = geometryData.MaterialCount;
            var materialColorCount = geometryData.MaterialColors.Length;
            var materialGlossinessCount = geometryData.MaterialGlossiness.Length;
            var materialSmoothnessCount = geometryData.MaterialSmoothness.Length;

            // Indices
            ValidateIndices(geometryData); // Validates the packed geometry.
            foreach (var m in geometryData.GetMeshData()) { ValidateIndices(m.GetIndices(), m.VertexCount); } // Validates the individual meshes.
            if (!(indexCount % 3 == 0)) throw new VimValidationException($"Geometry data index count {indexCount} must be divisible by 3");
            if (!geometryData.Indices.All(i => i >= 0 && i < geometryData.VertexCount)) throw new VimValidationException($"Geometry data indices must all be between 0 and the vertex count {vertexCount}");

            // Submeshes
            if (!(submeshCount >= meshCount)) throw new VimValidationException($"Geometry data submesh count {submeshCount} must be greater than or equal to the mesh count {meshCount}");
            if (!(submeshCount == submeshMaterialCount)) throw new VimValidationException($"Geometry data submesh count {submeshCount} must be equal to the submesh material count {submeshMaterialCount}");
            if (!(submeshCount == submeshIndexOffsetCount)) throw new VimValidationException($"Geometry data submesh count {submeshCount} must be equal to the length of the submesh index offsets {submeshIndexOffsetCount}");
            if (!submeshIndexOffsets.All(i => i % 3 == 0)) throw new VimValidationException("Geometry data submesh index offsets must all be divisible by 3");
            if (!submeshIndexOffsets.All(i => i >= 0 && i < indexCount)) throw new VimValidationException($"Geometry data submesh index offsets must all index into the index buffer");
            if (!submeshIndexCount.All(i => i > 0)) throw new VimValidationException("Geometry data submesh index counts must all be positive");

            // Meshes
            if (!meshSubmeshOffset.All(i => i >= 0 && i < submeshCount)) throw new VimValidationException("Geometry data mesh submesh offsets must all index into the submesh index offsets buffer");
            if (!meshSubmeshCount.All(i => i > 0)) throw new VimValidationException("Geometry data mesh submesh counts must all be positive");

            // Instances
            if (!(instanceCount == instanceParentCount)) throw new VimValidationException($"Geometry data instance count {instanceCount} must be equal to the instance parent count {instanceParentCount}");
            if (!(instanceCount == instanceMeshesCount)) throw new VimValidationException($"Geometry data instance count {instanceCount} must be equal to the instance mesh count {instanceMeshesCount}");
            if (!(instanceCount == instanceTransformsCount)) throw new VimValidationException($"Geometry data instance count {instanceCount} must be equal to the instance transform count {instanceTransformsCount}");
            if (!(instanceCount == instanceFlagsCount)) throw new VimValidationException($"Geometry data instance count {instanceCount} must be equal to the instance flag count {instanceFlagsCount}");
            if (!geometryData.InstanceParents.All(i => i < instanceCount)) throw new VimValidationException($"Geometry data instance parent indices must be less than the instance count {instanceCount}");
            if (!geometryData.InstanceMeshes.All(i => i < meshCount)) throw new VimValidationException($"Geometry data instance mesh indices must be less than the mesh count {meshCount}");

            // Materials
            if (!(materialCount == materialColorCount)) throw new VimValidationException($"Geometry data material count {materialCount} must be equal to the material color count {materialColorCount}");
            if (!(materialCount == materialGlossinessCount)) throw new VimValidationException($"Geometry data material count {materialCount} must be equal to the material glossiness count {materialGlossinessCount}");
            if (!(materialCount == materialSmoothnessCount)) throw new VimValidationException($"Geometry data material count {materialCount} must be equal to the material smoothness count {materialSmoothnessCount}");
        }

        public static void ValidateIndices(VimGeometryData geometryData)
        {
            ValidateIndices(geometryData.Indices, geometryData.VertexCount);
        }

        public static void ValidateIndices(int[] indices, int vertexCount)
        {
            foreach (var index in indices)
            {
                if (index < 0 || index >= vertexCount)
                    throw new VimValidationException($"Invalid mesh index: {index}. Expected a value greater or equal to 0 and less than {vertexCount}");
            }
        }

        public static void ValidateEntityAndGeometryInvariants(VIM vim)
        {
            var geometry = vim.GeometryData;
            var errors = new List<string>();

            var entityTypesWithGeometryReferences = new HashSet<(Type, string, ObjectModel.G3dAttributeReferenceAttribute[])>(
                ObjectModel.ObjectModelReflection.GetEntityTypes<ObjectModel.Entity>()
                .Select(t => (
                    type: t,
                    tableName: t.GetCustomAttribute<TableNameAttribute>()?.Name,
                    attrs: t.GetCustomAttributes(typeof(ObjectModel.G3dAttributeReferenceAttribute))
                        .Select(a => a as ObjectModel.G3dAttributeReferenceAttribute)
                        .ToArray()))
                .Where(tuple => tuple.attrs.Length != 0)
            );

            var tableSet = vim.GetEntityTableSet();

            foreach (var tuple in entityTypesWithGeometryReferences)
            {
                var (type, tableName, attrs) = tuple;

                if (!tableSet.Tables.TryGetValue(tableName, out var table))
                    throw new VimValidationException($"Entity table not found: {tableName}");

                var numEntities = table.RowCount;

                foreach (var attr in attrs)
                {
                    var bufferName = attr.AttributeName;
                    var isOptional = attr.AttributeIsOptional;

                    var geometryItemCount = vim.GeometryData.GetItemCountByBufferName(bufferName);

                    // We don't check the relation if the attribute is optional and absent (null).
                    if (isOptional && geometryItemCount == 0)
                        continue;

                    var mult = attr.AttributeReferenceMultiplicity;

                    // Validate one-to-one relationships
                    if (mult == ObjectModel.G3dAttributeReferenceMultiplicity.OneToOne && numEntities != geometryItemCount)
                    {
                        errors.Add($"Multiplicity Error ({mult}); the number of entities of type \"{type.Name}\" ({numEntities}) is not equal to the number of elements in the geometry buffer \"{bufferName}\" ({geometryItemCount})");
                    }
                }
            }

            if (errors.Count > 0)
            {
                throw new VimValidationException(
                    $"Entity geometry invariant error(s):{Environment.NewLine}{string.Join(Environment.NewLine, errors)}");
            }
        }

        public static void ValidateNodes(VIM vim)
        {
            var nodeEntityCount = vim.GetEntityTableSet().NodeTable.RowCount;
            var instanceCount = vim.GeometryData.InstanceCount;
            if (nodeEntityCount != instanceCount)
                throw new VimValidationException($"The number of Node entities {nodeEntityCount} must match the number of geometry instances ({instanceCount})");
        }
    }
}
