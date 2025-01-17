using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vim.Util;
using IndexMap = System.Collections.Generic.Dictionary<int, int>;
using ReadOnlyIndexMap = System.Collections.Generic.IReadOnlyDictionary<int, int>;

namespace Vim.Format.ObjectModel
{
    /// <summary>
    /// A collection of maps from element index to other related entity indices.
    /// </summary>
    public class ElementIndexMaps
    {
        public ReadOnlyIndexMap FamilyInstanceIndexFromElementIndex { get; private set; }
        public ReadOnlyIndexMap FamilyTypeIndexFromElementIndex { get; private set; }
        public ReadOnlyIndexMap FamilyIndexFromElementIndex { get; private set; }
        public ReadOnlyIndexMap ViewIndexFromElementIndex { get; private set; }
        public ReadOnlyIndexMap AssemblyIndexFromElementIndex { get; private set; }
        public ReadOnlyIndexMap DesignOptionIndexFromElementIndex { get; private set; }
        public ReadOnlyIndexMap LevelIndexFromElementIndex { get; private set; }
        public ReadOnlyIndexMap PhaseIndexFromElementIndex { get; private set; }
        public ReadOnlyIndexMap RoomIndexFromElementIndex { get; private set; }
        public ReadOnlyIndexMap SystemIndexFromElementIndex { get; private set; }

        /// <summary>
        /// Maps the element index to its associated parameter indices.
        /// </summary>
        public IReadOnlyDictionary<int, List<int>> ParameterIndicesFromElementIndex { get; private set; }

        public ElementIndexMaps(DocumentModel documentModel, bool inParallel = true)
        {
            var dm = documentModel;
            var actions = new Action[]
            {
                () => FamilyInstanceIndexFromElementIndex = GetElementIndexMap(dm.FamilyInstanceEntityTable),
                () => FamilyTypeIndexFromElementIndex = GetElementIndexMap(dm.FamilyTypeEntityTable),
                () => FamilyIndexFromElementIndex = GetElementIndexMap(dm.FamilyEntityTable),
                () => ViewIndexFromElementIndex = GetElementIndexMap(dm.ViewEntityTable),
                () => AssemblyIndexFromElementIndex = GetElementIndexMap(dm.AssemblyInstanceEntityTable),
                () => DesignOptionIndexFromElementIndex = GetElementIndexMap(dm.DesignOptionEntityTable),
                () => LevelIndexFromElementIndex = GetElementIndexMap(dm.LevelEntityTable),
                () => PhaseIndexFromElementIndex = GetElementIndexMap(dm.PhaseEntityTable),
                () => RoomIndexFromElementIndex = GetElementIndexMap(dm.RoomEntityTable),
                () => ParameterIndicesFromElementIndex = GetElementIndicesMap(dm.ParameterEntityTable),
                () => SystemIndexFromElementIndex = GetElementIndexMap(dm.SystemEntityTable)
            };

            if (inParallel)
            {
                Parallel.Invoke(actions);
            }
            else
            {
                foreach (var action in actions)
                    action.Invoke();
            }
        }

        public ElementIndexMaps(EntityTableSet entityTables, bool inParallel = true)
        {
            var actions = new Action[]
            {
                () => FamilyInstanceIndexFromElementIndex = GetElementIndexMap(entityTables.FamilyInstanceTable),
                () => FamilyTypeIndexFromElementIndex = GetElementIndexMap(entityTables.FamilyTypeTable),
                () => FamilyIndexFromElementIndex = GetElementIndexMap(entityTables.FamilyTable),
                () => ViewIndexFromElementIndex = GetElementIndexMap(entityTables.ViewTable),
                () => AssemblyIndexFromElementIndex = GetElementIndexMap(entityTables.AssemblyInstanceTable),
                () => DesignOptionIndexFromElementIndex = GetElementIndexMap(entityTables.DesignOptionTable),
                () => LevelIndexFromElementIndex = GetElementIndexMap(entityTables.LevelTable),
                () => PhaseIndexFromElementIndex = GetElementIndexMap(entityTables.PhaseTable),
                () => RoomIndexFromElementIndex = GetElementIndexMap(entityTables.RoomTable),
                () => ParameterIndicesFromElementIndex = GetElementIndicesMap(entityTables.ParameterTable),
                () => SystemIndexFromElementIndex = GetElementIndexMap(entityTables.SystemTable)
            };

            if (inParallel)
            {
                Parallel.Invoke(actions);
            }
            else
            {
                foreach (var action in actions)
                    action.Invoke();
            }
        }

        public static readonly string ElementIndexColumnName = ColumnExtensions.GetIndexColumnName(TableNames.Element, nameof(Element));

        public static DictionaryOfLists<int, int> GetElementIndicesMap(EntityTable et)
        {
            var indicesMap = new DictionaryOfLists<int, int>();
            var elementIndices = et?.IndexColumns[ElementIndexColumnName]?.GetTypedData();
            if (elementIndices == null)
                return indicesMap;
            for (var i = 0; i < elementIndices.Length; ++i)
                indicesMap.Add(elementIndices[i], i);
            return indicesMap;
        }

        public static DictionaryOfLists<int, int> GetElementIndicesMap(EntityTable_v2 et)
        {
            var indicesMap = new DictionaryOfLists<int, int>();
            var elementIndices = et?.IndexColumns[ElementIndexColumnName]?.GetTypedData();
            if (elementIndices == null)
                return indicesMap;
            for (var i = 0; i < elementIndices.Length; ++i)
                indicesMap.Add(elementIndices[i], i);
            return indicesMap;
        }

        public static IndexMap GetElementIndexMap(EntityTable et)
        {
            var indexMap = new IndexMap();
            var elementIndices = et?.IndexColumns[ElementIndexColumnName]?.GetTypedData();
            if (elementIndices == null)
                return indexMap;
            for (var i = 0; i < elementIndices.Length; ++i)
                indexMap.TryAdd(elementIndices[i], i);
            return indexMap;
        }

        public static IndexMap GetElementIndexMap(EntityTable_v2 et)
        {
            var indexMap = new IndexMap();
            if (et?.IndexColumns.TryGetValue(ElementIndexColumnName, out var buffer) != true || buffer == null)
                return indexMap;

            var elementIndices = buffer.GetTypedData();
            if (elementIndices == null)
                return indexMap;

            for (var i = 0; i < elementIndices.Length; ++i)
                indexMap.TryAdd(elementIndices[i], i);

            return indexMap;
        }
    }
}
