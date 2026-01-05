using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.BFast;
using Vim.Util;

using ReadOnlyIndexMap = System.Collections.Generic.IReadOnlyDictionary<int, int>;

namespace Vim.Format
{
    /// <summary>
    /// A set of entity tables.
    /// </summary>
    public partial class VimEntityTableSet
    {
        /// <summary>
        /// The string table. Can be null (when only loading the schema)
        /// </summary>
        public string[] StringTable { get; }

        /// <summary>
        /// A mapping of entity table data objects keyed by entity table name.
        /// </summary>
        public Dictionary<string, VimEntityTableData> TableData { get; } = new Dictionary<string, VimEntityTableData>();

        /// <summary>
        /// A mapping of entity tables keyed by entity table name.
        /// </summary>
        public Dictionary<string, VimEntityTable> Tables { get; } = new Dictionary<string, VimEntityTable>();

        /// <summary>
        /// Maps elements to table records which are referencing those elements.
        /// </summary>
        public VimElementIndexMaps ElementIndexMaps { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public VimEntityTableSet(VimEntityTableData[] entityTableData, string[] stringTable, bool inParallel = true)
        {
            StringTable = stringTable;

            foreach (var table in entityTableData)
                TableData[table.Name] = table;

            Initialize(); // Code-generated.
        }

        /// <summary>
        /// Returns an entity table data object based on the given entity table name. If the object does not exist, returns a new empty table.
        /// </summary>
        public VimEntityTableData GetEntityTableDataOrEmpty(string tableName)
            => TableData.TryGetValue(tableName, out var result) ? result : new VimEntityTableData { Name = tableName };

        /// <summary>
        /// Returns the entity table based on the given table name.
        /// </summary>
        public bool TryGetEntityTable(string tableName, out VimEntityTable entityTable)
        {
            entityTable = null;
            return string.IsNullOrEmpty(tableName)
                ? false
                : Tables.TryGetValue(tableName, out entityTable);
        }

        /// <summary>
        /// Returns the related entity table based on the given index column's name.
        /// </summary>
        public bool TryGetRelatedEntityTable(INamedBuffer<int> indexColumn, out VimEntityTable entityTable)
        {
            entityTable = null;
            var relatedTableName = VimEntityTableColumnName.GetRelatedTableName(indexColumn);
            return string.IsNullOrEmpty(relatedTableName)
                ? false
                : TryGetEntityTable(relatedTableName, out entityTable);
        }

        /// <summary>
        /// Returns an array aligned with the Element table which defines the kind of each element (ex: FamilyInstance, FamilyType, Family, Level, Room, Material, Phase, etc)
        /// </summary>
        public static Vim.Format.ElementKind[] GetElementKinds(FileInfo vimFileInfo)
        {
            vimFileInfo.ThrowIfNotExists("Could not get the element kinds.");

            var elementTableName = VimEntityTableNames.Element;

            var elementKindTableNames = GetElementKindTableNames();
            elementKindTableNames.Add(elementTableName);

            var entityTableData = VimEntityTableData.EnumerateEntityTables(
                vimFileInfo,
                false,
                entityTableName => elementKindTableNames.Contains(entityTableName),
                (entityTableName, colName) =>
                    // If we're dealing with the element table, load a single column from the element table (i.e. the ID column)
                    (entityTableName == elementTableName && (colName is "long:Id" || colName is "int:Id")) ||
                    // Otherwise, load the element index column.
                    colName == "index:Vim.Element:Element"
            ).ToArray();

            var ets = new VimEntityTableSet(entityTableData, Array.Empty<string>());

            return ets.GetElementKinds();
        }

        /// <summary>
        /// Returns the VimEntityTableSet contained in the given VIM file.
        /// </summary>
        public static VimEntityTableSet GetEntityTableSet(
            string vimFilePath,
            VimEntityTableSetOptions options = null)
        {
            return GetEntityTableSet(new FileInfo(vimFilePath), options);
        }

        /// <summary>
        /// Returns the VimEntityTableSet contained in the given VIM file.
        /// </summary>
        public static VimEntityTableSet GetEntityTableSet(
            FileInfo vimFileInfo,
            VimEntityTableSetOptions options = null)
        {
            vimFileInfo.ThrowIfNotExists("Could not get the entity table set.");

            var stringTable = options.StringTable
                ?? (options.SchemaOnly ? null : VIM.GetStringTable(vimFileInfo));

            var entityTableData = VimEntityTableData.EnumerateEntityTables(
                vimFileInfo,
                options.SchemaOnly,
                options.EntityTableNameFilter,
                options.EntityTableColumnFilter)
                .ToArray();

            return new VimEntityTableSet(entityTableData, stringTable, options.InParallel);
        }

        /// <summary>
        /// Returns an entity table set from the given VIM file containing only the specified tables by name.
        /// If no entity table names are specified, all entity tables are loaded.
        /// Useful for loading specific entity tables without loading the whole file into memory.
        /// </summary>
        public static VimEntityTableSet GetEntityTableSetByTableName(
            FileInfo vimFileInfo,
            params string[] tableNames)
        {
            var tableSet = new HashSet<string>(tableNames);

            return GetEntityTableSet(vimFileInfo, new VimEntityTableSetOptions()
            {
                StringTable = Array.Empty<string>(),
                EntityTableNameFilter = n => tableSet.Count == 0 ? true : tableSet.Contains(n)
            });
        }
    }

    public partial class CategoryTable
    {
        /// <summary>
        /// Returns the full name of the category, ex: "parentCategoryName: categoryName" or just "categoryName" if there is no parent.
        /// </summary>
        public static string GetNameFull(string parentCategoryName, string categoryName)
            => !string.IsNullOrEmpty(parentCategoryName)
                ? $"{parentCategoryName}: {categoryName}"
                : categoryName;

        /// <summary>
        /// Returns the full name of the category, ex: "parentCategoryName: categoryName" or just "categoryName" if there is no parent.
        /// </summary>
        public string GetNameFull(int categoryIndex)
        {
            var categoryName = GetName(categoryIndex);
            var parentIndex = GetParentIndex(categoryIndex);
            var parentCategoryName = parentIndex >= 0 ? GetName(parentIndex) : "";

            return GetNameFull(parentCategoryName, categoryName);
        }
    }

    public partial class ElementTable
    {
        public bool IsFamilyInstance(int elementIndex)
            => ParentTableSet.ElementIndexMaps.FamilyInstanceIndexFromElementIndex.ContainsKey(elementIndex);

        public bool IsFamilyType(int elementIndex)
            => ParentTableSet.ElementIndexMaps.FamilyTypeIndexFromElementIndex.ContainsKey(elementIndex);

        public bool IsFamily(int elementIndex)
            => ParentTableSet.ElementIndexMaps.FamilyIndexFromElementIndex.ContainsKey(elementIndex);

        public bool IsSystem(int elementIndex)
            => ParentTableSet.ElementIndexMaps.SystemIndexFromElementIndex.ContainsKey(elementIndex);

        // Index properties

        private int GetRelatedIndex(int elementIndex, ReadOnlyIndexMap indexMap)
            => indexMap.TryGetValue(elementIndex, out var value) ? value : EntityRelation.None;

        public int GetLevelElementIndex(int elementIndex)
            => ParentTableSet.LevelTable.GetElementIndex(GetLevelIndex(elementIndex));

        public int GetRoomElementIndex(int elementIndex)
            => ParentTableSet.RoomTable.GetElementIndex(GetRoomIndex(elementIndex));

        public int GetSystemElementIndex(int elementIndex)
            => ParentTableSet.SystemTable.GetElementIndex(GetSystemIndex(elementIndex));

        public int GetSystemIndex(int elementIndex)
            => GetRelatedIndex(elementIndex, ParentTableSet.ElementIndexMaps.SystemIndexFromElementIndex);

        public int GetFamilyInstanceIndex(int elementIndex)
            => GetRelatedIndex(elementIndex, ParentTableSet.ElementIndexMaps.FamilyInstanceIndexFromElementIndex);

        public int GetFamilyInstanceElementIndex(int elementIndex)
            => ParentTableSet.FamilyInstanceTable.GetElementIndex(GetFamilyInstanceIndex(elementIndex));

        public int GetFamilyTypeIndex(int elementIndex)
        {
            if (IsFamilyInstance(elementIndex))
            {
                var familyInstanceIndex = GetFamilyInstanceIndex(elementIndex);
                return ParentTableSet.FamilyInstanceTable.GetFamilyTypeIndex(familyInstanceIndex);
            }

            return GetRelatedIndex(elementIndex, ParentTableSet.ElementIndexMaps.FamilyTypeIndexFromElementIndex);
        }

        public int GetFamilyTypeElementIndex(int elementIndex)
            => ParentTableSet.FamilyTypeTable.GetElementIndex(GetFamilyTypeIndex(elementIndex));

        public string GetFamilyTypeName(int elementIndex)
            => ParentTableSet.ElementTable.GetName(GetFamilyTypeElementIndex(elementIndex));

        public int GetFamilyIndex(int elementIndex)
        {
            if (IsFamilyInstance(elementIndex))
            {
                var familyInstanceIndex = GetFamilyInstanceIndex(elementIndex);
                var familyTypeIndex = ParentTableSet.FamilyInstanceTable.GetFamilyTypeIndex(familyInstanceIndex);
                return ParentTableSet.FamilyTypeTable.GetFamilyIndex(familyTypeIndex);
            }

            if (IsFamilyType(elementIndex))
            {
                var familyTypeIndex = GetRelatedIndex(elementIndex, ParentTableSet.ElementIndexMaps.FamilyTypeIndexFromElementIndex);
                return ParentTableSet.FamilyTypeTable.GetFamilyIndex(familyTypeIndex);
            }

            return GetRelatedIndex(elementIndex, ParentTableSet.ElementIndexMaps.FamilyIndexFromElementIndex);
        }

        public int GetFamilyElementIndex(int elementIndex)
            => ParentTableSet.FamilyTable.GetElementIndex(GetFamilyIndex(elementIndex));

        public string GetFamilyNameEx(int elementIndex)
            => ParentTableSet.ElementTable.GetName(GetFamilyElementIndex(elementIndex));

        // Object-generating properties

        public FamilyInstance GetFamilyInstance(int elementIndex)
            => ParentTableSet.FamilyInstanceTable.Get(GetFamilyInstanceIndex(elementIndex));

        public Element GetFamilyInstanceElement(int elementIndex)
            => Get(GetFamilyInstanceElementIndex(elementIndex));

        public FamilyType GetFamilyType(int elementIndex)
            => ParentTableSet.FamilyTypeTable.Get(GetFamilyTypeIndex(elementIndex));

        public Element GetFamilyTypeElement(int elementIndex)
            => Get(GetFamilyTypeElementIndex(elementIndex));

        public Family GetFamily(int elementIndex)
            => ParentTableSet.FamilyTable.Get(GetFamilyIndex(elementIndex));

        public Element GetFamilyElement(int elementIndex)
            => Get(GetFamilyElementIndex(elementIndex));

        public System GetSystem(int elementIndex)
            => ParentTableSet.SystemTable.Get(GetSystemIndex(elementIndex));

        public Element GetSystemElement(int elementIndex)
            => Get(GetSystemElementIndex(elementIndex));

        // Parameters

        public List<int> GetParameterIndices(int elementIndex)
            => ParentTableSet.ElementIndexMaps.ParameterIndicesFromElementIndex
                .TryGetValue(elementIndex, out var pIndices) ? pIndices : new List<int>();

        public List<int> GetFamilyInstanceParameterIndices(int elementIndex)
        {
            if (elementIndex < 0)
                return new List<int>();

            var familyInstanceElementIndex = GetFamilyInstanceElementIndex(elementIndex);
            if (familyInstanceElementIndex == EntityRelation.None)
                return new List<int>();

            return GetParameterIndices(familyInstanceElementIndex);
        }

        public List<int> GetFamilyTypeParameterIndices(int elementIndex)
        {
            if (elementIndex < 0)
                return new List<int>();

            var familyTypeElementIndex = GetFamilyTypeElementIndex(elementIndex);
            if (familyTypeElementIndex == EntityRelation.None)
                return new List<int>();

            return GetParameterIndices(familyTypeElementIndex);
        }

        public List<int> GetFamilyParameterIndices(int elementIndex)
        {
            if (elementIndex < 0)
                return new List<int>();

            var familyElementIndex = GetFamilyElementIndex(elementIndex);
            if (familyElementIndex == EntityRelation.None)
                return new List<int>();

            return GetParameterIndices(familyElementIndex);
        }

        /// <summary>
        /// Returns an array of booleans aligned 1:1 with the element table.
        /// Items are true if the element is visible in at least one Revit 3d view.
        /// </summary>
        public bool[] GetIsVisibleInRevit3dView()
        {
            // Result is 1:1 aligned with the elements.
            var elementVisibility = new bool[RowCount];

            // Initialize with false visibility.
            for (var i = 0; i < elementVisibility.Length; ++i)
            {
                elementVisibility[i] = false;
            }

            var viewTable = ParentTableSet.ViewTable;

            // O(n) traversal of ElementInView table to extract element visibility in 3d views.
            var elementInViewTable = ParentTableSet.ElementInViewTable;
            for (var i = 0; i < elementInViewTable.RowCount; ++i)
            {
                var viewIndex = elementInViewTable.GetViewIndex(i);
                var viewType = viewTable.GetViewType(viewIndex);

                // Only target records whose ViewType is "ThreeD" (applies to Revit only).
                if (!viewType.Equals("ThreeD", StringComparison.InvariantCultureIgnoreCase))
                    continue;

                var elementIndex = elementInViewTable.GetElementIndex(i);
                if (elementIndex == EntityRelation.None)
                    continue;

                elementVisibility[elementIndex] = true;
            }

            return elementVisibility;
        }
    }

    public partial class FamilyInstanceTable
    {
        /// <summary>
        /// Returns the super component distance of the family instance.
        /// </summary>
        public int GetSuperComponentDistance(int familyInstanceIndex, int depth = 0)
        {
            var parentElementIndex = GetSuperComponentIndex(familyInstanceIndex);
            if (parentElementIndex == EntityRelation.None)
                return depth;

            var hasParentFamilyInstanceIndex = ParentTableSet.ElementIndexMaps.FamilyInstanceIndexFromElementIndex
                .TryGetValue(parentElementIndex, out var parentFamilyInstanceIndex);

            return hasParentFamilyInstanceIndex
                ? GetSuperComponentDistance(parentFamilyInstanceIndex, depth + 1)
                : depth;
        }
    }

    public partial class CompoundStructureTable
    {
        /// <summary>
        /// Returns an array aligned 1:1 with the CompoundStructureTable records which defines the FamilyType index of the CompoundStructure.
        /// </summary>
        public int[] GetFamilyTypeIndices()
        {
            // Initialize the result array which is aligned with the CompoundStructureTable records.
            var result = new int[RowCount];
            for (var i = 0; i < result.Length; ++i) { result[i] = EntityRelation.None; }

            // O(n) iteration over the family type records to populate the result.
            var familyTypeTable = ParentTableSet.FamilyTypeTable;
            for (var familyTypeIndex = 0; familyTypeIndex < familyTypeTable.RowCount; ++familyTypeIndex)
            {
                var compoundStructureIndex = familyTypeTable.GetCompoundStructureIndex(familyTypeIndex);
                if (compoundStructureIndex == EntityRelation.None)
                    continue;

                result[compoundStructureIndex] = familyTypeIndex;
            }

            return result;
        }

        /// <summary>
        /// Returns an array aligned 1:1 with the CompoundStructureTable records which defines the number of CompoundStructureLayers associated with the CompoundStructure.
        /// </summary>
        public int[] GetCompoundStructureLayerCounts()
        {
            var result = new int[RowCount];
            for (var i = 0; i < result.Length; ++i) { result[i] = 0; }

            // O(n) iteration over the CompoundStructureLayer records to count the layers in each CompoundStructure.
            var layerTable = ParentTableSet.CompoundStructureLayerTable;
            for (var layerIndex = 0; layerIndex < layerTable.RowCount; ++layerIndex)
            {
                var compoundStructureIndex = layerTable.GetCompoundStructureIndex(layerIndex);
                if (compoundStructureIndex == EntityRelation.None)
                    continue;

                result[compoundStructureIndex] += 1;
            }

            return result;
        }
    }
}
