using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Vim.Util;

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

        public static Element CreateSyntheticElement(string name, string type)
            => new Element { Id = VimConstants.SyntheticElementId, Name = name, Type = type };

        public static Element CreateParameterHolderElement(this BimDocument bd)
            => CreateSyntheticElement(bd.Name, VimConstants.BimDocumentParameterHolderElementType);

        public static string GetBimDocumentFileName(this DocumentModel dm, int bimDocumentIndex)
            => Path.GetFileName(dm.GetBimDocumentPathName(bimDocumentIndex));

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
                .Indices()
                .Where((colIndex, _) => columnSet.Contains(colIndex))
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
    }
}
