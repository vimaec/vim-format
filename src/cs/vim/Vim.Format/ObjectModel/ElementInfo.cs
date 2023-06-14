using System;
using System.Collections.Generic;
using Vim.LinqArray;

namespace Vim.ObjectModel
{
    /// <summary>
    /// Represents common element information
    /// </summary>
    public class ElementInfo
    {
        public readonly DocumentModel DocumentModel;
        public readonly int ElementIndex;

        // Lazy properties

        private int? _familyInstanceIndex;
        public int FamilyInstanceIndex
        {
            get
            {
                if (_familyInstanceIndex != null)
                    return _familyInstanceIndex.Value;

                _familyInstanceIndex = DocumentModel.ElementIndexMaps.FamilyInstanceIndexFromElementIndex
                    .TryGetValue(ElementIndex, out var value) ? value : EntityRelation.None;

                return _familyInstanceIndex.Value;
            }
        }

        private IArray<int> _parameterIndices;
        public IArray<int> ParameterIndices
        {
            get
            {
                if (_parameterIndices != null)
                    return _parameterIndices;

                _parameterIndices = (DocumentModel.ElementIndexMaps.ParameterIndicesFromElementIndex
                    .TryGetValue(ElementIndex, out var parameterIndices) ? parameterIndices : new List<int>())
                    .ToIArray();

                return _parameterIndices;
            }
        }

        private int? _systemIndex;
        public int SystemIndex
        {
            get
            {
                if (_systemIndex != null)
                    return _systemIndex.Value;

                _systemIndex = DocumentModel.ElementIndexMaps.SystemIndexFromElementIndex
                    .TryGetValue(ElementIndex, out var value) ? value : EntityRelation.None;

                return _systemIndex.Value;
            }
        }

        public void EvaluateLazyProperties()
        {
            _ = FamilyInstanceIndex;
            _ = ParameterIndices;
            _ = SystemIndex;
        }

        // Boolean queries

        public bool IsFamilyInstance
            => DocumentModel.ElementIndexMaps.FamilyInstanceIndexFromElementIndex.ContainsKey(ElementIndex);

        public bool IsFamilyType
            => DocumentModel.ElementIndexMaps.FamilyTypeIndexFromElementIndex.ContainsKey(ElementIndex);

        public bool IsFamily
            => DocumentModel.ElementIndexMaps.FamilyIndexFromElementIndex.ContainsKey(ElementIndex);

        public bool IsSystem
            => DocumentModel.ElementIndexMaps.SystemIndexFromElementIndex.ContainsKey(ElementIndex);

        // Derived Index Properties

        public int CategoryIndex => DocumentModel.GetElementCategoryIndex(ElementIndex);
        public int LevelIndex => DocumentModel.GetElementLevelIndex(ElementIndex);
        public int LevelElementIndex => DocumentModel.GetLevelElementIndex(LevelIndex);
        public int BimDocumentIndex => DocumentModel.GetElementBimDocumentIndex(ElementIndex);
        public int WorksetIndex => DocumentModel.GetElementWorksetIndex(ElementIndex);
        public int FamilyInstanceElementIndex => DocumentModel.GetFamilyInstanceElementIndex(FamilyInstanceIndex);
        public int FamilyTypeIndex => DocumentModel.GetFamilyInstanceFamilyTypeIndex(FamilyInstanceIndex);
        public int FamilyTypeElementIndex => DocumentModel.GetFamilyTypeElementIndex(FamilyTypeIndex);
        public int FamilyIndex => DocumentModel.GetFamilyTypeFamilyIndex(FamilyTypeIndex);
        public int FamilyElementIndex => DocumentModel.GetFamilyElementIndex(FamilyIndex);
        public int SystemElementIndex => DocumentModel.GetSystemElementIndex(SystemIndex);

        // Derived Object-Generating Properties

        public Element Element => DocumentModel.ElementList.ElementAtOrDefault(ElementIndex);
        public Category Category => DocumentModel.CategoryList.ElementAtOrDefault(CategoryIndex);
        public Level Level => DocumentModel.LevelList.ElementAtOrDefault(LevelIndex);
        public BimDocument BimDocument => DocumentModel.BimDocumentList.ElementAtOrDefault(BimDocumentIndex);
        public Workset Workset => DocumentModel.WorksetList.ElementAtOrDefault(WorksetIndex);
        public FamilyInstance FamilyInstance => DocumentModel.FamilyInstanceList.ElementAtOrDefault(FamilyInstanceIndex);
        public Element FamilyInstanceElement => DocumentModel.ElementList.ElementAtOrDefault(FamilyInstanceElementIndex);
        public FamilyType FamilyType => DocumentModel.FamilyTypeList.ElementAtOrDefault(FamilyTypeIndex);
        public Element FamilyTypeElement => DocumentModel.ElementList.ElementAtOrDefault(FamilyTypeElementIndex);
        public Family Family => DocumentModel.FamilyList.ElementAtOrDefault(FamilyIndex);
        public Element FamilyElement => DocumentModel.ElementList.ElementAtOrDefault(FamilyElementIndex);
        public System System => DocumentModel.SystemList.ElementAtOrDefault(SystemIndex);
        public Element SystemElement => DocumentModel.ElementList.ElementAtOrDefault(SystemElementIndex);
        public IEnumerable<Parameter> Parameters => DocumentModel.ParameterList.SelectByIndex(ParameterIndices).ToEnumerable();

        [Flags]
        public enum ParameterScope
        {
            None = 0,
            FamilyInstance = 1,
            FamilyType = 1 << 1,
            Family = 1 << 2,
            All = FamilyInstance | FamilyType | Family,
        }

        public Dictionary<ParameterScope, IEnumerable<Parameter>> GetScopedParameters(
            ParameterScope scope = ParameterScope.All)
        {
            var result = new Dictionary<ParameterScope, IEnumerable<Parameter>>();

            if ((scope & ParameterScope.FamilyInstance) == ParameterScope.FamilyInstance &&
                FamilyInstanceElementIndex != EntityRelation.None)
            {
                result[ParameterScope.FamilyInstance] = DocumentModel.GetElementInfo(FamilyInstanceElementIndex).Parameters;
            }

            if ((scope & ParameterScope.FamilyType) == ParameterScope.FamilyType &&
                FamilyTypeElementIndex != EntityRelation.None)
            {
                result[ParameterScope.FamilyType] = DocumentModel.GetElementInfo(FamilyTypeElementIndex).Parameters;
            }

            if ((scope & ParameterScope.Family) == ParameterScope.Family &&
                FamilyElementIndex != EntityRelation.None)
            {
                result[ParameterScope.Family] = DocumentModel.GetElementInfo(FamilyElementIndex).Parameters;
            }

            return result;
        }

        // Commonly Accessed Properties

        public int ElementId => DocumentModel.GetElementId(ElementIndex, -1);
        public string ElementUniqueId => DocumentModel.GetElementUniqueId(ElementIndex);

        public string ElementUniqueIdWithBimScopedElementIdFallback
        {
            get
            {
                var elementUniqueId = ElementUniqueId;
                return !string.IsNullOrWhiteSpace(elementUniqueId)
                    ? elementUniqueId
                    : $"{BimDocumentFileName}::{ElementId}";
            }
        }

        public string ElementName => DocumentModel.GetElementName(ElementIndex);
        public string CategoryName => DocumentModel.GetCategoryName(CategoryIndex);
        public string LevelName => DocumentModel.GetElementName(LevelElementIndex);
        public string FamilyName => DocumentModel.GetElementName(FamilyElementIndex, DocumentModel.GetElementFamilyName(ElementIndex));
        public string FamilyTypeName => DocumentModel.GetElementName(FamilyTypeElementIndex);
        public string WorksetName => DocumentModel.GetWorksetName(WorksetIndex);
        public string BimDocumentPathName => DocumentModel.GetBimDocumentPathName(BimDocumentIndex);
        public string BimDocumentFileName => DocumentModel.GetBimDocumentFileName(BimDocumentIndex);

        /// <summary>
        /// Constructor.
        /// </summary>
        public ElementInfo(DocumentModel documentModel, int elementIndex)
        {
            DocumentModel = documentModel;
            ElementIndex = elementIndex;
        }
    }
}
