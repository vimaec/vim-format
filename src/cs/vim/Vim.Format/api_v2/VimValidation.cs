using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Vim.Util;

namespace Vim.Format.api_v2
{
    public class VimValidationOptions
    {
        /// <summary>
        /// Validation will fail if duplicate storage keys are detected. Setting this to false simplifies merged VIM validation.
        /// </summary>
        public bool UniqueStorageKeys { get; set; } = true;

        /// <summary>
        /// By default, we expect at least one BimDocument entity. Setting this to false suppresses this requirement. This is typically
        /// useful when stripping the bim data from the VIM file to keep it as light as possible.
        /// </summary>
        public bool BimDocumentMustExist { get; set; } = true;

        /// <summary>
        /// By default, it is recommended that the bim document element has an element id of -1.
        /// Note: this may not always be the case, particularly for IFC to VIM conversions.
        /// </summary>
        public bool BimDocumentElementMustBeSynthetic { get; set; } = true;

        /// <summary>
        /// By default, it is recommended that the bim document element has an element whose name is equal to the bim document's name.
        /// Note: this may not always be the case, particularly for IFC to VIM conversions.
        /// </summary>
        public bool BimDocumentElementNameMustMatchBimDocumentName { get; set; } = true;

        /// <summary>
        /// By default, it is recommended that the bim document element's type is set to "BimDocument Parameter Holder"
        /// Note: this may not always be the case, particularly for IFC to VIM conversions.
        /// </summary>
        public bool BimDocumentElementTypeMustBeParameterHolder { get; set; } = true;

        /// <summary>
        /// By default, it is recommended to avoid elements in systems having null values. This has been fixed as of object model v4.5.0
        /// </summary>
        public bool ElementInSystemMustNotHaveNullValues { get; set; } = true;
    }

    public class VimValidationException : Exception
    {
        public VimValidationException(string message) : base(message) { }
    }

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
            // TODO: Port Vim.Format.ObjectModel.ValidateAssets

            // vim.DocumentModel.Validate(options.ObjectModelValidationOptions);
            // vim.ValidateGeometry();
            // vim.ValidateDocumentModelToG3dInvariants();
            // vim.ValidateNodes();
            // vim.ValidateShapes();
        }

        public static void ValidateTableRows(VIM vim)
        {
            foreach (var et in vim.EntityTableData)
            {
                var rowCount = et.GetRowCount();
                foreach (var c in et.IndexColumns)
                {
                    if (c.Data.Length != rowCount)
                        throw new Exception($"Expected array length {c.Data.Length} of column {c.Name} to be the same as number of rows {rowCount}");
                }

                foreach (var c in et.StringColumns)
                {
                    if (c.Data.Length != rowCount)
                        throw new Exception($"Expected array length {c.Data.Length} of column {c.Name} to be the same as number of rows {rowCount}");
                }

                foreach (var c in et.DataColumns)
                {
                    if (c.Data.Length != rowCount)
                        throw new Exception($"Expected array length {c.Data.Length} of column {c.Name} to be the same as number of rows {rowCount}");
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
                        throw new Exception($"Could not find related table for index column {indexColumn.Name}");
                }
            }
        }

        public static void ValidateGeometryBuffers(VIM vim)
        {
            var geometryData = vim.GeometryData;

            if (geometryData.Header == null) throw new Exception("VIM geometry data header is null");
            if (geometryData.Indices == null) throw new Exception("VIM geometry indices is null");
            if (geometryData.Vertices == null) throw new Exception("VIM geometry vertices is null");
            if (geometryData.MeshSubmeshOffsets == null) throw new Exception("VIM geometry mesh submesh offests is null");
            if (geometryData.SubmeshIndexOffsets == null) throw new Exception("VIM geometry submesh index offsets is null");
            if (geometryData.InstanceMeshes == null) throw new Exception("VIM geometry instance meshes is null");
            if (geometryData.InstanceTransforms == null) throw new Exception("VIM geometry instance transforms is null");
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

        public static void ValidateGeometry(VIM vim)
        {
            // Validate the packed geometry.
            ValidateIndices(vim.GeometryData);

            // Validate the individual meshes.
            foreach (var m in vim.GeometryData.GetMeshData())
            {
                ValidateIndices(m.GetIndices(), m.VertexCount);
            }
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
                    throw new Exception($"Invalid mesh index: {index}. Expected a value greater or equal to 0 and less than {vertexCount}");
            }
        }

        public static void ValidateDocumentModelToG3dInvariants(this VimScene vim)
        {
            var g3d = vim._SerializableDocument.Geometry;
            var errors = new List<string>();

            errors.AddRange(Vim.G3d.Validation.Validate(g3d).Select(e => e.ToString("G")));

            var entityTypesWithG3dReferences = new HashSet<(Type, G3dAttributeReferenceAttribute[])>(
                ObjectModelReflection.GetEntityTypes<Entity>()
                    .Select(t => (
                        type: t,
                        attrs: t.GetCustomAttributes(typeof(G3dAttributeReferenceAttribute))
                            .Select(a => a as G3dAttributeReferenceAttribute)
                            .ToArray()))
                    .Where(tuple => tuple.attrs.Length != 0));

            var dm = vim.DocumentModel;

            foreach (var tuple in entityTypesWithG3dReferences)
            {
                var (type, attrs) = tuple;
                var propertyName = type.Name + "List";
                if (dm.GetPropertyValue(propertyName) is IArray arr)
                {
                    var numEntities = arr.Count;

                    foreach (var attr in attrs)
                    {
                        var attributeName = attr.AttributeName;
                        var isOptional = attr.AttributeIsOptional;

                        var g3dAttribute = g3d.GetAttribute(attributeName);

                        // We don't check the relation if the attribute is optional and absent (null).
                        if (isOptional && g3dAttribute == null)
                            continue;

                        var g3dElementCount = g3dAttribute?.ElementCount ?? 0;
                        var mult = attr.AttributeReferenceMultiplicity;

                        // Validate one-to-one relationships
                        if (mult == G3dAttributeReferenceMultiplicity.OneToOne && numEntities != g3dElementCount)
                        {
                            errors.Add($"Multiplicity Error ({mult}); the number of entities of type \"{type.Name}\" ({numEntities}) is not equal to the number of elements in the g3d attribute \"{attributeName}\" ({g3dElementCount})");
                        }
                    }
                }
                else
                {
                    throw new VimValidationException($"DocumentModel.{propertyName} not found");
                }
            }

            if (errors.Count > 0)
            {
                throw new VimValidationException(
                    $"DocumentModel to G3d invariant error(s):{Environment.NewLine}{string.Join(Environment.NewLine, errors)}");
            }
        }

        public static void ValidateNodes(this VimScene vim)
        {
            if (vim.VimNodes.Count != vim.DocumentModel.NumNode)
                throw new VimValidationException($"The number of {nameof(VimSceneNode)} ({vim.VimNodes.Count}) does not match the number of node entities ({vim.DocumentModel.NumNode})");
        }

        public static void ValidateShapes(this VimScene vim)
        {
            var shapes = vim.VimShapes;
            var numShapes  = vim.DocumentModel.NumShape;
            if (shapes.Count != numShapes)
                throw new VimValidationException($"The number of {nameof(VimShape)} ({shapes.Count}) does not match the number of shape entities ({numShapes})");

            void ValidateColorDomain(string label, Vector4 value, Vector4 lowerInclusive, Vector4 upperInclusive, int index)
            {
                if (value.X < lowerInclusive.X ||
                    value.Y < lowerInclusive.Y ||
                    value.Z < lowerInclusive.Z ||
                    value.W < lowerInclusive.W ||
                    value.X > upperInclusive.X ||
                    value.Y > upperInclusive.Y ||
                    value.Z > upperInclusive.Z ||
                    value.W > upperInclusive.W)
                {
                    throw new Exception($"{label} {value} is not in the range [{lowerInclusive}..{upperInclusive}] for {index}");
                }
            }

            Parallel.For(0, numShapes, shapeIndex =>
            {
                var shape = shapes[shapeIndex];
                if (shape.ElementIndex < 0)
                    throw new VimValidationException($"{nameof(Element)} is null for {nameof(VimShape)} {shape.ShapeIndex}");
                ValidateColorDomain($"{nameof(VimShape)} color", shape.Color, Vector4.Zero, Vector4.One, shape.ShapeIndex);
            });
        }


}