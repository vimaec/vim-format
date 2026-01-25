using System;
using System.Collections.Generic;
using System.Text;
using Vim.Format.ObjectModel;
using Vim.Util;

namespace Vim.Format
{
    public class FamilyDescendants
    {
        public DictionaryOfLists<int, int> FamilyElementsToFamilyInstanceElements { get; } = new DictionaryOfLists<int, int>();
        public DictionaryOfLists<int, int> FamilyTypeElementsToFamilyInstanceElements { get; } = new DictionaryOfLists<int, int>();

        /// <summary>
        /// Constructor
        /// </summary>
        public FamilyDescendants(EntityTableSet tableSet)
        {
            var familyInstanceTable = tableSet.FamilyInstanceTable;
            var familyTypeTable = tableSet.FamilyTypeTable;
            var familyTable = tableSet.FamilyTable;

            // Build a mapping of FamilyTypeElement -> FamilyInstanceElement by traversing the FamilyInstance table.

            for (var i = 0; i < familyInstanceTable.RowCount; ++i)
            {
                var familyInstanceElementIndex = familyInstanceTable.GetElementIndex(i);
                if (familyInstanceElementIndex == EntityRelation.None)
                    continue;

                var familyTypeIndex = familyInstanceTable.GetFamilyTypeIndex(i);
                if (familyTypeIndex == EntityRelation.None)
                    continue;

                var familyTypeElementIndex = familyTypeTable.GetElementIndex(familyTypeIndex);
                if (familyTypeElementIndex == EntityRelation.None)
                    continue;

                FamilyTypeElementsToFamilyInstanceElements.Add(familyTypeElementIndex, familyInstanceElementIndex);
            }

            // Build a mapping of FamilyElement -> FamilyInstanceElement by traversing the FamilyType table.

            for (var i = 0; i < familyTypeTable.RowCount; ++i)
            {
                var familyTypeElementIndex = familyTypeTable.GetElementIndex(i);
                if (familyTypeElementIndex == EntityRelation.None)
                    continue;

                var familyIndex = familyTypeTable.GetFamilyIndex(i);
                if (familyIndex == EntityRelation.None)
                    continue;

                var familyElementIndex = familyTable.GetElementIndex(familyIndex);
                if (familyElementIndex == EntityRelation.None)
                    continue;

                if (!FamilyTypeElementsToFamilyInstanceElements.TryGetValue(familyTypeElementIndex, out var familyInstanceElementIndices))
                    continue;

                foreach (var familyInstanceElementIndex in familyInstanceElementIndices)
                {
                    FamilyElementsToFamilyInstanceElements.Add(familyElementIndex, familyInstanceElementIndex);
                }
            }
        }

        public bool TryGetFamilyInstanceElementsFromFamilyElement(int familyElementIndex, out List<int> familyInstanceElementIndices)
            => FamilyElementsToFamilyInstanceElements.TryGetValue(familyElementIndex, out familyInstanceElementIndices);

        public bool TryGetFamilyInstanceElementsFromFamilyTypeElement(int familyTypeElementIndex, out List<int> familyInstanceElementIndices)
            => FamilyTypeElementsToFamilyInstanceElements.TryGetValue(familyTypeElementIndex, out familyInstanceElementIndices);
    }
}
