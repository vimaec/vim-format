using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Vim.Format.Geometry;
using Vim.G3d;
using Vim.Util;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim.Format.ObjectModel
{
    public static class ObjectModelExtensions
    {
        public static ElementInfo GetElementInfo(this DocumentModel documentModel, int elementIndex)
            => new ElementInfo(documentModel, elementIndex);

        public static ElementInfo GetElementInfo(this DocumentModel documentModel, Element element)
            => documentModel.GetElementInfo(element.Index);

        public static ElementInfo GetElementInfo(this DocumentModel documentModel, EntityWithElement entityWithElement)
            => documentModel.GetElementInfo(entityWithElement._Element.Index);

        public static string GetUrn(this ElementInfo elementInfo)
            => Urn.GetElementUrn(Urn.VimNID, elementInfo.Element);

        public static string GetUrn(this BimDocument bd)
            => Urn.GetBimDocumentUrn(Urn.VimNID, bd);

        public static Element CreateSyntheticElement(string name, string type)
            => new Element
            {
                Id = VimConstants.SyntheticElementId,
                Name = name,
                Type = type,
                UniqueId = $"{name}_{type}" // NOTE: we need to assign a UniqueId for merging purposes.
            };

        public static Element CreateParameterHolderElement(string bimDocumentName)
            => CreateSyntheticElement(bimDocumentName, VimConstants.BimDocumentParameterHolderElementType);

        public static Element CreateParameterHolderElement(this BimDocument bd)
            => CreateParameterHolderElement(bd.Name);

        public static DictionaryOfLists<int, AssetInView> GetAssetsInViewOrderedByViewIndex(this DocumentModel dm)
            => dm.AssetInViewList.GroupBy(aiv => aiv.View.Index).ToDictionaryOfLists();

        public static string GetBimDocumentFileName(this DocumentModel dm, int bimDocumentIndex)
            => Path.GetFileName(dm.GetBimDocumentPathName(bimDocumentIndex));

        public static IArray<DisplayUnit> GetBimDocumentDisplayUnits(this DocumentModel dm, BimDocument bd)
            => dm.DisplayUnitInBimDocumentList
                .Where(item => item.BimDocument.Index == bd.Index)
                .Select(item => item.DisplayUnit)
                .ToIArray();

        public static IArray<Phase> GetBimDocumentPhases(this DocumentModel dm, BimDocument bd)
            => dm.PhaseOrderInBimDocumentList
                .Where(item => item.BimDocument.Index == bd.Index)
                .Select(item => item.Phase)
                .ToIArray();

        public const string LengthSpecLegacyPrefix = "UT_Length";
        public const string LengthSpecPrefix = "autodesk.spec.aec:length";

        public static DisplayUnit GetLengthDisplayUnit(this IArray<DisplayUnit> displayUnits)
            => displayUnits.FirstOrDefault(du =>
            {
                var spec = du.Spec;
                return spec.StartsWith(LengthSpecPrefix, StringComparison.InvariantCultureIgnoreCase) ||
                       spec.StartsWith(LengthSpecLegacyPrefix, StringComparison.InvariantCultureIgnoreCase);
            });

        public static FamilyType GetFamilyType(this FamilyInstance fi)
            => fi?.FamilyType;

        public static string GetFamilyTypeName(this FamilyInstance fi)
            => fi?.FamilyType?.Element?.Name ?? "";

        public static Family GetFamily(this FamilyType ft)
            => ft?.Family;

        public static Family GetFamily(this FamilyInstance fi)
            => fi?.GetFamilyType()?.GetFamily();

        public static string GetFamilyName(this FamilyInstance fi)
            => fi?.GetFamily()?.Element?.Name ?? "";

        /// <summary>
        /// Extension method using pre-allocated parser for improved performance.
        /// </summary>
        public static string GetParameterDisplayValueWithNativeValueFallback(this PipeSeparatedStrings.Parser parser, string parameterValues)
        {
            // Update the parser
            parser.Parse(parameterValues);

            switch (parser.GetCount())
            {
                case 0:
                    // no values found.
                    return null;
                case 1:
                    // return the only value, which designates both the native value and the display value.
                    return parser.GetValue(0); 
                default:
                    {
                        // Attempt to return the display value. If it is null or empty, fall back to the native value.
                        var displayValue = parser.GetValue(1);
                        return string.IsNullOrEmpty(displayValue)
                            ? parser.GetValue(0) // native value.
                            : displayValue;
                    }
            }
        }

        public static ElementInSystem[] GetElementsInSystem(this DocumentModel dm, System system)
        {
            if (system == null)
                return Array.Empty<ElementInSystem>();

            return dm.ElementInSystemList.Where(eis => eis._System.Index == system.Index)
                .ToArray();
        }

        public static Element[] GetElementsInWarning(this DocumentModel dm, Warning warning)
        {
            if (warning == null)
                return Array.Empty<Element>();

            return dm.ElementInWarningList.Where(eiw => eiw._Warning.Index == warning.Index)
                .Select(eiw => eiw.Element)
                .ToArray();
        }

        // A helper class which defines cell data to be stored in a DataTable.
        private class CellData
        {
            public readonly string Value;
            public readonly int ColIndex;
            public readonly int RowIndex;

            public CellData(string value, int colIndex, int rowIndex)
            {
                Value = value;
                ColIndex = colIndex;
                RowIndex = rowIndex;
            }
        }

        public static DataTable GetScheduleAsDataTable(this DocumentModel dm, int scheduleIndex)
        {
            var ei = dm.ScheduleElementIndex[scheduleIndex];

            var dataTable = new DataTable(dm.GetElementName(ei));

            var columns = dm.ScheduleColumnList
                .Where(c => c._Schedule.Index == scheduleIndex)
                .OrderBy(c => c.ColumnIndex)
                .ToArray();
            dataTable.Columns.AddRange(columns.Select(c => new DataColumn(c.Name)).ToArray());

            var columnSet = new HashSet<int>(columns.Select(c => c.Index));

            var cellRecords = dm.ScheduleCellScheduleColumnIndex
                .IndicesWhere((colIndex, _) => columnSet.Contains(colIndex))
                .Select(cellIndex => new CellData(
                    dm.GetScheduleCellValue(cellIndex),
                    dm.GetScheduleCellScheduleColumnIndex(cellIndex),
                    dm.GetScheduleCellRowIndex(cellIndex)))
                .GroupBy(c => c.RowIndex)
                .OrderBy(g => g.Key)
                .Select(g => g.OrderBy(t => t.ColIndex).Select(t => t.Value));

            foreach (var row in cellRecords)
                dataTable.Rows.Add(row.ToArray() as string[]);

            return dataTable;
        }

        /// <summary>
        /// Returns the list of parameter indices associated with the given element index.
        /// </summary>
        public static List<int> GetParameterIndicesFromElementIndex(this ElementIndexMaps elementIndexMaps, int elementIndex)
        {
            return elementIndexMaps.ParameterIndicesFromElementIndex.TryGetValue(elementIndex, out var parameterIndices)
                ? parameterIndices
                : new List<int>();
        }

        /// <summary>
        /// Returns a grouping of entities representing elements by bim document index.
        /// </summary>
        public static IEnumerable<IGrouping<int, T>> GroupByBimDocumentIndex<T>(
            this IEnumerable<T> entityWithElementCollection,
            ElementTable elementTable)
            where T : IElementIndex
        {
            var elementBimDocumentIndices = elementTable.Column_BimDocumentIndex;

            return entityWithElementCollection.GroupBy(e =>
                elementBimDocumentIndices.ElementAtOrDefault(e.GetElementIndexOrNone(), EntityRelation.None));
        }

        /// <summary>
        /// Returns a dictionary mapping a bim document index to a dictionary of entities whose keys are those entities' element IDs.
        /// This is useful when you want to scope elements in a BIM document and when you are referring to elements by their IDs.
        /// Note 1: Element IDs are only unique within their respective BIM documents.
        /// Note 2: Entities which do not have an element association will not appear in the returned value.
        /// </summary>
        public static Dictionary<int, Dictionary<long, T>> GroupByBimDocumentIndexAndElementId<T>(
            this IEnumerable<T> entityWithElementCollection,
            ElementTable elementTable)
            where T : IElementIndex
        {
            var elementIds = elementTable.Column_Id;

            var result = entityWithElementCollection
                .GroupByBimDocumentIndex(elementTable)
                // For each bim document group, create a mapping from elementID to each entity.
                .ToDictionary(
                    groupByBimDocumentIndex => groupByBimDocumentIndex.Key,
                    groupByBimDocumentIndex =>
                    {
                        var elementIdToEntityWithElementMap = new Dictionary<long, T>();

                        foreach (var entityWithElement in groupByBimDocumentIndex)
                        {
                            var elementIndex = entityWithElement.GetElementIndexOrNone();
                            if (elementIndex == EntityRelation.None)
                                continue; // Skip entities which do not have an element index.

                            var elementId = elementIds.ElementAtOrDefault(elementIndex);
                            elementIdToEntityWithElementMap[elementId] = entityWithElement;
                        }
                        return elementIdToEntityWithElementMap;
                    });

            return result;
        }

        /// <summary>
        /// Returns the entity corresponding to the given element ID.
        /// IMPORTANT: if the given element ID is -1 or is not present in the given map, returns false and the item will be null.
        /// </summary>
        public static bool TryGetEntityFromElementId<T>(
            this IReadOnlyDictionary<long, T> elementIdMap,
            long elementId,
            out T item)
            where T: class
        {
            item = null;
            return elementId != -1L && elementIdMap.TryGetValue(elementId, out item);
        }
    }
}
