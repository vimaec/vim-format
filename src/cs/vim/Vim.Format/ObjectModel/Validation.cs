using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Vim.Math3d;
using Vim.Util;

namespace Vim.Format.ObjectModel
{
    public class ObjectModelValidationOptions
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

    public class ObjectModelValidationException : Exception
    {
        public ObjectModelValidationException(string message) : base(message) { }
    }

    public static class Validation
    {
        public static void ValidateBimDocument(this DocumentModel dm, ObjectModelValidationOptions validationOptions)
        {
            // There is at least one BimDocument in the document model.
            if (dm.NumBimDocument == 0 && validationOptions.BimDocumentMustExist)
                throw new ObjectModelValidationException($"No {nameof(BimDocument)} found.");

            foreach (var bd in dm.BimDocumentList)
            {
                var bdElement = bd.Element;
                if (bdElement == null)
                    throw new ObjectModelValidationException($"{nameof(BimDocument)} @{bd.Index} has null {nameof(Element)}.");

                var expectedElementId = VimConstants.SyntheticElementId;
                if (validationOptions.BimDocumentElementMustBeSynthetic && bdElement.Id != expectedElementId)
                    throw new ObjectModelValidationException($"{nameof(BimDocument)} @{bd.Index} - Related {nameof(Element)}.{nameof(Element.Id)} @{bdElement.Index} is not {expectedElementId}");

                var expectedName = bd.Name;
                if (validationOptions.BimDocumentElementNameMustMatchBimDocumentName && bdElement.Name != expectedName)
                    throw new ObjectModelValidationException($"{nameof(BimDocument)} @{bd.Index} - Related {nameof(Element)}.{nameof(Element.Name)} @{bdElement.Index} does not match {nameof(BimDocument)}.{nameof(BimDocument.Name)} ({expectedName})");

                var expectedElementType = VimConstants.BimDocumentParameterHolderElementType;
                if (validationOptions.BimDocumentElementTypeMustBeParameterHolder && bdElement.Type != expectedElementType)
                    throw new ObjectModelValidationException($"{nameof(BimDocument)} @{bd.Index} - Related {nameof(Element)}.{nameof(Element.Type)} @{bdElement.Index} is not '{expectedElementType}'.");
            }
        }

        public static void ValidateCompoundStructureLayer(this CompoundStructureLayer layer)
        {
            // All CompoundLayers have a CompoundStructure
            if (layer.CompoundStructure == null)
                throw new ObjectModelValidationException($"{nameof(CompoundStructureLayer)} {layer.Index} has null {nameof(CompoundStructure)}");
        }

        public static void ValidateCompoundStructures(this DocumentModel dm)
        {
            var cslArray = dm.CompoundStructureLayerList.ToArray();

            // All compound structure layers are valid
            foreach (var csl in cslArray)
                csl.ValidateCompoundStructureLayer();

            // There are no gaps in the order index of compound structure layers.
            foreach (var cslCluster in cslArray.GroupBy(csl => csl._CompoundStructure.Index))
            {
                var ordered = cslCluster.OrderBy(csl => csl.OrderIndex).ToArray();
                for (var i = 0; i < ordered.Length; ++i)
                {
                    var orderIndex = ordered[i].OrderIndex;
                    if (orderIndex != i)
                        throw new ObjectModelValidationException($"{nameof(CompoundStructureLayer.OrderIndex)} {orderIndex} does not match expected value of {i}");
                }

                var csIndex = cslCluster.Key;
                var csStructuralLayerIndex = dm.CompoundStructureStructuralLayerIndex.ElementAtOrDefault(csIndex, EntityRelation.None);
                if (csStructuralLayerIndex != EntityRelation.None)
                {
                    // If the CompoundStructure.StructuralMaterial exists, it should be a valid relation to a CompoundStructureLayer
                    var maxIndex = cslArray.Length - 1;
                    if (csStructuralLayerIndex < 0 || csStructuralLayerIndex > maxIndex)
                        throw new ObjectModelValidationException($"{nameof(CompoundStructure)} {csIndex} has an invalid {nameof(CompoundStructure.StructuralLayer)} relation index of {csStructuralLayerIndex}. Expected value between [0..{maxIndex}]");
                }
            }

            // All FamilyTypes with compound structures are system families.
            var ftArray = dm.FamilyTypeList.ToArray();
            var cslRelationIndices = new HashSet<int>(cslArray.Select(l => l.CompoundStructure.Index));
            var ftRelationIndices = new HashSet<int>(ftArray.Where(ft => ft.CompoundStructure != null).Select(ft => ft.CompoundStructure.Index));
            var csArray = dm.CompoundStructureList.ToArray();
            foreach (var cs in csArray)
            {
                // All compound structures are referenced by at least one compound structure layer
                if (!cslRelationIndices.Contains(cs.Index))
                    throw new ObjectModelValidationException($"{nameof(CompoundStructure)} index: {cs.Index} does not have a corresponding {nameof(CompoundStructureLayer)}");

                // All compound structures are referenced by at least one family type.
                if (!ftRelationIndices.Contains(cs.Index))
                    throw new ObjectModelValidationException($"{nameof(CompoundStructure)} index: {cs.Index} does not have a corresponding {nameof(FamilyType)}");
            }

            // A compound structure must be referenced by exactly one family type (no re-using compound structures).
            if (ftRelationIndices.Count != ftRelationIndices.Distinct().Count())
                throw new ObjectModelValidationException($"A {nameof(CompoundStructure)} must be referenced by exactly one {nameof(FamilyType)}.");
        }

        public static void ValidateAssets(this DocumentModel dm)
        {
            // Validate that the assets contained in the buffers matches the assets entity table.
            var assetBuffers = dm.Document.Assets;
            var assetEntities = dm.AssetList.ToArray();
            foreach (var asset in assetEntities)
            {
                if (!assetBuffers.Contains(asset.BufferName))
                    throw new ObjectModelValidationException($"No matching asset buffer found for asset entity {asset.Index} with {nameof(asset.BufferName)} '{asset.BufferName}'");
            }
        }

        public static void ValidateParameters(this DocumentModel dm)
        {
            Parallel.ForEach(dm.ParameterList, p =>
            {
                // Each parameter must be associated to an element.
                if (p._Element.Index == EntityRelation.None)
                    throw new ObjectModelValidationException($"{nameof(Element)} not found for {nameof(Parameter)} {p.Index}");

                // Each parameter must have a parameter descriptor.
                if (p.ParameterDescriptor == null)
                    throw new ObjectModelValidationException($"{nameof(ParameterDescriptor)} is null for {nameof(Parameter)} {p.Index}");
            });

            // Validate the parameter descriptors.
            foreach (var pd in dm.ParameterDescriptorList)
            {
                if (pd.DisplayUnit == null)
                    throw new ObjectModelValidationException($"{nameof(DisplayUnit)} is null for {nameof(ParameterDescriptor)} {pd.Index}");
            }
        }

        public static void ValidatePhases(this DocumentModel dm)
        {
            // Validate the phase order information.
            var poArray = dm.PhaseOrderInBimDocumentList.ToArray();
            foreach (var po in poArray)
            {
                var bd = po.BimDocument;
                if (bd == null)
                    throw new ObjectModelValidationException($"{nameof(BimDocument)} is null for {nameof(PhaseOrderInBimDocument)} {po.Index}");

                var phase = po.Phase;
                if (phase == null)
                    throw new ObjectModelValidationException($"{nameof(Phase)} is null for {nameof(PhaseOrderInBimDocument)} {po.Index}");
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
                        throw new ObjectModelValidationException($"Unexpected OrderIndex {orderIndex}; expected {i} in {nameof(PhaseOrderInBimDocument)} {po.Index}");
                }
            }

            // Validate that the phase order information covers the set of phases.
            var phaseIndexSet = new HashSet<int>(dm.PhaseList.Select(p => p.Index));
            phaseIndexSet.ExceptWith(poArray.Select(po => po.Index));
            if (phaseIndexSet.Count != 0)
                throw new ObjectModelValidationException($"{nameof(Phase)} index coverage is incomplete among {nameof(PhaseOrderInBimDocument)}");
        }
        
        /// <summary>
        /// Generic storage key check; ensures that the keys only appear once in the keySet.
        /// </summary>
        public static void ValidateStorageKeys(IEnumerable<IStorageKey> storageKeyEntities)
        {
            var keySet = new HashSet<object>();
            foreach (var entity in storageKeyEntities)
            {
                var key = entity.GetStorageKey();
                if (!keySet.Add(key))
                    throw new ObjectModelValidationException($"Duplicate storage key ({key}) found for {entity.GetType().Name}");
            }
        }

        /// <summary>
        /// Validate that the entities which inherit from IStorageKey all have unique storage key values.
        /// </summary>
        public static void ValidateStorageKeys(this DocumentModel dm)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            var storageKeyEntityLists = dm.AllEntities
                .OfType<IEnumerable<IStorageKey>>();
            foreach (var storageKeyEntities in storageKeyEntityLists)
            {
                ValidateStorageKeys(storageKeyEntities);
            }
        }

        public static void ValidateMaterials(this DocumentModel dm)
        {
            void ValidateDomain(string label, double value, double lowerInclusive, double upperInclusive, int index)
            {
                if (value < lowerInclusive || value > upperInclusive)
                    throw new ObjectModelValidationException($"{label} {value} is not in the range [{lowerInclusive}..{upperInclusive}] for material {index}");
            }

            void ValidateDVector3Domain(string label, DVector3 value, DVector3 lowerInclusive, DVector3 upperInclusive, int index)
            {
                if (value.X < lowerInclusive.X ||
                    value.Y < lowerInclusive.Y ||
                    value.Z < lowerInclusive.Z ||
                    value.X > upperInclusive.X ||
                    value.Y > upperInclusive.Y ||
                    value.Z > upperInclusive.Z)
                {
                    throw new ObjectModelValidationException($"{label} {value} is not in the range [{lowerInclusive}..{upperInclusive}] for material {index}");
                }
            }

            foreach (var material in dm.MaterialList)
            {
                var index = material.Index;
                ValidateDVector3Domain(nameof(material.Color), material.Color, DVector3.Zero, DVector3.One, index);
                ValidateDomain(nameof(material.Glossiness), material.Glossiness, 0d, 1d, index);
                ValidateDomain(nameof(material.Smoothness), material.Smoothness, 0d, 1d, index);
                ValidateDomain(nameof(material.Transparency), material.Transparency, 0d, 1d, index);
                ValidateDomain(nameof(material.NormalAmount), material.NormalAmount, 0d, 1d, index);
            }
        }

        public static Dictionary<int, HashSet<int>> GetViewToElementsMap(
            this ElementInView[] elementInViewList)
            => elementInViewList
                .Select(eiv => (viewIndex: eiv._View?.Index ?? -1, elementIndex: eiv._Element?.Index ?? -1))
                .GroupBy(t => t.viewIndex)
                .ToDictionary(
                    g => g.Key,
                    g => new HashSet<int>(g.Select(t => t.elementIndex)));

        public static void ValidateShapesInView(this DocumentModel dm)
        {
            var viewToElementsMap = dm.ElementInViewList.ToArray().GetViewToElementsMap();

            // Validate that the shapes in view have an element which is also in the same view.
            foreach (var item in dm.ShapeInViewList)
            {
                var viewIndex = item._View.Index;
                var shape = item.Shape;
                var shapeIndex = shape.Index;
                var shapeElementIndex = shape._Element.Index;

                if (!viewToElementsMap.TryGetValue(viewIndex, out var elementsInView))
                    throw new ObjectModelValidationException($"{nameof(Shape)} ({shapeIndex}) {nameof(View)} ({viewIndex}) not found in {nameof(viewToElementsMap)}");

                if (!elementsInView.Contains(shapeElementIndex))
                    throw new ObjectModelValidationException($"{nameof(Shape)} ({shapeIndex}) in {nameof(View)} ({viewIndex}) does not have a matching {nameof(ElementInView)} ({nameof(Element)} index: {shapeElementIndex})");
            }
        }

        public static void ValidateEntitiesWithElement(this DocumentModel dm)
        {
            var entityWithElementTypes = new HashSet<Type>(ObjectModelReflection.GetEntityTypes<EntityWithElement>()
                .Where(t => t.GetCustomAttributes(typeof(G3dAttributeReferenceAttribute)).Count() == 0));

            var numElements = dm.NumElement;

            Parallel.ForEach(entityWithElementTypes, entityWithElementType =>
            {
                var elementIndices = ((int[])dm.GetPropertyValue(entityWithElementType.Name + "ElementIndex")).ToArray();
                for (var i = 0; i < elementIndices.Length; ++i)
                {
                    var elementIndex = elementIndices[i];
                    if (elementIndex < 0)
                        throw new ObjectModelValidationException($"{nameof(EntityWithElement)} {entityWithElementType} @{i} has a negative element index: {elementIndex}.");
                    if (elementIndex >= numElements)
                        throw new ObjectModelValidationException($"{nameof(EntityWithElement)} {entityWithElementType} @{i} has an invalid element index: {elementIndex}; NumElements: {numElements}");
                }
            });
        }

        public static void ValidateElementInSystem(this DocumentModel dm)
        {
            foreach (var eis in dm.ElementInSystemList)
            {
                if (eis.System == null)
                    throw new ObjectModelValidationException($"{nameof(ElementInSystem)} @ {eis.Index} has a null {nameof(ElementInSystem.System)}");

                if (eis.Element == null)
                    throw new ObjectModelValidationException($"{nameof(ElementInSystem)} @ {eis.Index} has a null {nameof(ElementInSystem.Element)}");
            }
        }

        public static void Validate(this DocumentModel dm, ObjectModelValidationOptions options = null)
        {
            options = options ?? new ObjectModelValidationOptions();

            dm.ValidateEntitiesWithElement();
            dm.ValidateBimDocument(options);
            dm.ValidateCompoundStructures();
            dm.ValidateAssets();
            dm.ValidateParameters();
            dm.ValidatePhases();
            if (options.UniqueStorageKeys)
                dm.ValidateStorageKeys();
            if (options.ElementInSystemMustNotHaveNullValues)
                dm.ValidateElementInSystem();
            dm.ValidateMaterials();
            dm.ValidateShapesInView();
        }
    }
}
