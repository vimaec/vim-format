using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Vim.Format.api_v2;
using Vim.Util;

namespace Vim.Format.ObjectModel
{
    public static class ObjectModelExtensions
    {
        public static string GetUrn(this BimDocument bd)
            => Urn.GetBimDocumentUrn(Urn.VimNID, bd);

        public static Element CreateSyntheticElement(string name, string type)
            => new Element
            {
                Id = VimEntityTableConstants.SyntheticElementId,
                Name = name,
                Type = type,
                UniqueId = $"{name}_{type}" // NOTE: we need to assign a UniqueId for merging purposes.
            };

        public static Element CreateParameterHolderElement(string bimDocumentName)
            => CreateSyntheticElement(bimDocumentName, VimEntityTableConstants.BimDocumentParameterHolderElementType);

        public static Element CreateParameterHolderElement(this BimDocument bd)
            => CreateParameterHolderElement(bd.Name);

        public static DictionaryOfLists<int, AssetInView> GetAssetsInViewOrderedByViewIndex(this VIM vim)
            => vim.GetEntityTableSet().AssetInViewTable.GroupBy(aiv => aiv.View.Index).ToDictionaryOfLists();

        public static string GetBimDocumentFileName(this VIM vim, int bimDocumentIndex)
            => Path.GetFileName(vim.GetEntityTableSet().BimDocumentTable.GetPathName(bimDocumentIndex));

        public static IEnumerable<DisplayUnit> GetBimDocumentDisplayUnits(this VIM vim, BimDocument bd)
            => vim.GetEntityTableSet().DisplayUnitInBimDocumentTable
                .Where(item => item.BimDocument.Index == bd.Index)
                .Select(item => item.DisplayUnit);

        public static IEnumerable<Phase> GetBimDocumentPhases(this VIM vim, BimDocument bd)
            => vim.GetEntityTableSet().PhaseOrderInBimDocumentTable
                .Where(item => item.BimDocument.Index == bd.Index)
                .Select(item => item.Phase);

        public const string LengthSpecLegacyPrefix = "UT_Length";
        public const string LengthSpecPrefix = "autodesk.spec.aec:length";

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

        public static DataTable GetScheduleAsDataTable(this VIM vim, int scheduleIndex)
        {
            var tableSet = vim.GetEntityTableSet();

            var ei = tableSet.ScheduleTable.GetElementIndex(scheduleIndex);

            var dataTable = new DataTable(tableSet.ElementTable.GetName(ei));

            var columns = tableSet.ScheduleColumnTable
                .Where(c => c._Schedule.Index == scheduleIndex)
                .OrderBy(c => c.ColumnIndex)
                .ToArray();
            dataTable.Columns.AddRange(columns.Select(c => new DataColumn(c.Name)).ToArray());

            var columnSet = new HashSet<int>(columns.Select(c => c.Index));

            var cellRecords = tableSet.ScheduleCellTable.Column_ScheduleColumnIndex
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
        public static List<int> GetParameterIndicesFromElementIndex(this VimElementIndexMaps elementIndexMaps, int elementIndex)
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
            api_v2.ElementTable elementTable)
            where T : IElementIndex
        {
            entityWithElementCollection = entityWithElementCollection ?? Array.Empty<T>();

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
            api_v2.ElementTable elementTable)
            where T : IElementIndex
        {
            entityWithElementCollection = entityWithElementCollection ?? Array.Empty<T>();

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
