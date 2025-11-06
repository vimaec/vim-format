using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vim.Util;
using IndexMap = System.Collections.Generic.Dictionary<int, int>;
using ReadOnlyIndexMap = System.Collections.Generic.IReadOnlyDictionary<int, int>;

namespace Vim.Format.api_v2
{
    public class VimElementIndexMaps
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
        /// Constructor
        /// </summary>
        public VimElementIndexMaps(VimEntityTableSet entityTables, bool inParallel = true)
        {
            var actions = new Action[]
            {
                // TODO
                // () => FamilyInstanceIndexFromElementIndex = GetElementIndexMap(entityTables.FamilyInstanceTable),
                // () => FamilyTypeIndexFromElementIndex = GetElementIndexMap(entityTables.FamilyTypeTable),
                // () => FamilyIndexFromElementIndex = GetElementIndexMap(entityTables.FamilyTable),
                // () => ViewIndexFromElementIndex = GetElementIndexMap(entityTables.ViewTable),
                // () => AssemblyIndexFromElementIndex = GetElementIndexMap(entityTables.AssemblyInstanceTable),
                // () => DesignOptionIndexFromElementIndex = GetElementIndexMap(entityTables.DesignOptionTable),
                // () => LevelIndexFromElementIndex = GetElementIndexMap(entityTables.LevelTable),
                // () => PhaseIndexFromElementIndex = GetElementIndexMap(entityTables.PhaseTable),
                // () => RoomIndexFromElementIndex = GetElementIndexMap(entityTables.RoomTable),
                // () => ParameterIndicesFromElementIndex = GetElementIndicesMap(entityTables.ParameterTable),
                // () => SystemIndexFromElementIndex = GetElementIndexMap(entityTables.SystemTable)
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

        public static readonly string ElementIndexColumnName
            = ColumnExtensions.GetIndexColumnName(VimEntityTableNames.Element, "Element");

        public static DictionaryOfLists<int, int> GetElementIndicesMap(VimEntityTable et)
        {
            var indicesMap = new DictionaryOfLists<int, int>();

            if (et?.IndexColumnMap?.TryGetValue(ElementIndexColumnName, out var buffer) != true || buffer == null)
                return indicesMap;

            var elementIndices = buffer.GetTypedData();
            if (elementIndices == null)
                return indicesMap;

            for (var i = 0; i < elementIndices.Length; ++i)
                indicesMap.Add(elementIndices[i], i);
            return indicesMap;
        }

        public static IndexMap GetElementIndexMap(VimEntityTable et)
        {
            var indexMap = new IndexMap();

            if (et?.IndexColumnMap?.TryGetValue(ElementIndexColumnName, out var buffer) != true || buffer == null)
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
