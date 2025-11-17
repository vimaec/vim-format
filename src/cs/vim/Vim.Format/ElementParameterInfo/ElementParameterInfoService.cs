using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vim.Format.api_v2;
using Vim.Util;

using static Vim.Format.ObjectModel.ObjectModelExtensions;

namespace Vim.Format.ElementParameterInfo
{
    /// <summary>
    /// Contains data derived from element parameters.
    /// </summary>
    public class ElementParameterInfo
    {
        /// <summary>
        /// An array of LevelInfo objects representing harmonized information about the levels in the given VIM Scene (see comment in LevelInfo.cs).
        /// Items in this array are aligned to the Level table.
        /// </summary>
        public LevelInfo[] LevelInfos { get; set; }

        /// <summary>
        /// An array of ElementLevelInfo objects representing detailed information of element level associations.
        /// Items in this array are aligned to the Element table.
        /// </summary>
        public ElementLevelInfo[] ElementLevelInfos { get; set; }

        /// <summary>
        /// An array of ElementMeasureInfo objects representing information about element measures (angle/slope, length, width, height, area, volume).
        /// Items in this array are aligned to the Element table.
        /// </summary>
        public ElementMeasureInfo[] ElementMeasureInfos { get; set; }

        /// <summary>
        /// An array of ElementIfcInfo objects representing information about element IFC data.
        /// Items in this array are aligned to the Element table.
        /// </summary>
        public ElementIfcInfo[] ElementIfcInfos { get; set; }

        /// <summary>
        /// An array of FamilyOmniClassInfo objects representing OmniClass information about the Family.
        /// Items in this array are aligned to the Family table.
        /// </summary>
        public FamilyOmniClassInfo[] FamilyOmniClassInfos { get; set; }

        /// <summary>
        /// An array of FamilyTypeUniformatInfo objects representing Uniformat information about the FamilyType.
        /// Items in this array are aligned to the FamilyType table.
        /// </summary>
        public FamilyTypeUniformatInfo[] FamilyTypeUniformatInfos { get; set; }

        /// <summary>
        /// An array of MeasureType values representing the measure types of each Parameter.
        /// Items in this array are aligned to the Parameter table.
        /// </summary>
        public MeasureType[] ParameterMeasureTypes { get; set; }
    }

    public static class VimElementParameterInfoService
    {
        /// <summary>
        /// Returns the element parameter information from the given VIM file.
        /// </summary>
        public static ElementParameterInfo GetElementParameterInfos(
            FileInfo vimFileInfo,
            string[] stringTable = null,
            VimElementGeometryInfo[] elementGeometryMap = null)
        {
            elementGeometryMap = elementGeometryMap ?? VimElementGeometryInfo.GetElementGeometryInfoList(vimFileInfo);

            var tableSet = VimEntityTableSet.GetEntityTableSet(vimFileInfo, new VimEntityTableSetOptions()
            {
                StringTable = stringTable,
                EntityTableNameFilter = n =>
                    n is TableNames.Element ||
                    n is TableNames.Family ||
                    n is TableNames.FamilyInstance ||
                    n is TableNames.FamilyType ||
                    n is TableNames.Parameter ||
                    n is TableNames.ParameterDescriptor ||
                    n is TableNames.DisplayUnit ||
                    n is TableNames.Level ||
                    n is TableNames.BasePoint
            });

            return GetElementParameterInfos(tableSet, elementGeometryMap);
        }

        /// <summary>
        /// Returns the element parameter information from the given entity table set and element geometry map.
        /// </summary>
        public static ElementParameterInfo GetElementParameterInfos(
            VimEntityTableSet tableSet,
            VimElementGeometryInfo[] elementGeometryMap)
        {
            var elementIndexMaps = tableSet.ElementIndexMaps;
            var elementTable = tableSet.ElementTable;
            var parameterTable = tableSet.ParameterTable;
            var descriptorTable = tableSet.ParameterDescriptorTable;
            var familyInstanceTable = tableSet.FamilyInstanceTable;
            var familyTypeTable = tableSet.FamilyTypeTable;
            var familyTable = tableSet.FamilyTable;
            var basePointTable = tableSet.BasePointTable;
            var levelTable = tableSet.LevelTable;

            var levels = levelTable.ToArray();
            var levelsByBimDocumentIndexAndElementId = levels.GroupByBimDocumentIndexAndElementId(elementTable);
            var basePointsByBimDocumentIndexAndElementId = basePointTable.GroupByBimDocumentIndexAndElementId(elementTable);

            var levelInfos = CreateLevelInfos(
                levels,
                elementTable,
                familyTypeTable,
                parameterTable,
                elementIndexMaps,
                levelsByBimDocumentIndexAndElementId,
                basePointsByBimDocumentIndexAndElementId);

            var levelInfoByBimDocumentIndex = levelInfos.GroupByBimDocumentIndexAndElementId(elementTable);

            var levelInfoMap = levelInfos.ToDictionaryIgnoreDuplicates(li => li.Level.Index, li => li);

            PatchBuildingStoryAbove(levelInfoByBimDocumentIndex);

            var elementLevelInfos = CreateElementLevelInfos(
                elementTable,
                familyInstanceTable,
                parameterTable,
                levelTable,
                elementIndexMaps,
                elementGeometryMap,
                levelInfoMap,
                levelInfoByBimDocumentIndex);

            var parameterMeasureTypes = CreateParameterMeasureTypes(parameterTable, descriptorTable);

            var elementMeasureInfos = CreateElementMeasureInfos(elementTable, parameterTable, parameterMeasureTypes, elementIndexMaps);

            var elementIfcInfos = CreateElementIfcInfos(elementTable, parameterTable, elementIndexMaps);

            var familyOmniClassInfos = CreateFamilyOmniClassInfos(familyTable, parameterTable, elementIndexMaps);

            var familyTypeUniformatInfos = CreateFamilyTypeUniformatInfos(familyTypeTable, parameterTable, elementIndexMaps);

            return new ElementParameterInfo
            {
                LevelInfos = levelInfos,
                ElementLevelInfos = elementLevelInfos,
                ElementMeasureInfos = elementMeasureInfos,
                ParameterMeasureTypes = parameterMeasureTypes,
                ElementIfcInfos = elementIfcInfos,
                FamilyOmniClassInfos = familyOmniClassInfos,
                FamilyTypeUniformatInfos = familyTypeUniformatInfos,
            };
        }

        /// <summary>
        /// Instantiates the level infos in parallel based on the given list of levels.
        /// </summary>
        private static LevelInfo[] CreateLevelInfos(
            IReadOnlyList<ObjectModel.Level> levels,
            ElementTable elementTable,
            FamilyTypeTable familyTypeTable,
            ParameterTable parameterTable,
            VimElementIndexMaps elementIndexMaps,
            IReadOnlyDictionary<int, Dictionary<long, ObjectModel.Level>> levelsByBimDocumentIndexAndElementId,
            IReadOnlyDictionary<int, Dictionary<long, ObjectModel.BasePoint>> basePointsByBimDocumentIndexAndElementId)
            => levels
                .AsParallel()
                .Select(level =>
                {
                    var bimDocumentIndex = elementTable.GetBimDocumentIndex(level.GetElementIndexOrNone());

                    if (!levelsByBimDocumentIndexAndElementId.TryGetValue(bimDocumentIndex, out var elementIdToLevelMap))
                        elementIdToLevelMap = new Dictionary<long, ObjectModel.Level>();

                    if (!basePointsByBimDocumentIndexAndElementId.TryGetValue(bimDocumentIndex, out var elementIdToBasePointMap))
                        elementIdToBasePointMap = new Dictionary<long, ObjectModel.BasePoint>();

                    return new LevelInfo(
                        level,
                        familyTypeTable,
                        parameterTable,
                        elementIndexMaps,
                        elementIdToLevelMap,
                        elementIdToBasePointMap);
                })
                .ToArray();

        /// <summary>
        /// Populates the LevelInfo.BuildingStoryAbove property if it is null.
        /// </summary>
        private static void PatchBuildingStoryAbove(IReadOnlyDictionary<int, Dictionary<long, LevelInfo>> levelInfoByBimDocumentIndex)
        {
            foreach (var (_, levelInfosInBimDocument) in levelInfoByBimDocumentIndex)
            {
                var levelInfosOrderedByProjectElevation = levelInfosInBimDocument.Values.OrderBy(l => l.Level.ProjectElevation).ToArray();

                foreach (var levelInfo in levelInfosOrderedByProjectElevation)
                {
                    if (levelInfo.HasBuildingStoryAbove)
                        continue;

                    levelInfo.BuildingStoryAbove = levelInfosOrderedByProjectElevation
                        .FirstOrDefault(other => other.IsBuildingStory && other.Level.ProjectElevation > levelInfo.Level.ProjectElevation)
                        ?.Level;
                }
            }
        }

        /// <summary>
        /// Returns an array of element level infos based on the given list of elements.
        /// </summary>
        private static ElementLevelInfo[] CreateElementLevelInfos(
            ElementTable elementTable,
            FamilyInstanceTable familyInstanceTable,
            ParameterTable parameterTable,
            LevelTable levelTable,
            VimElementIndexMaps elementIndexMaps,
            VimElementGeometryInfo[] elementGeometryMap,
            IReadOnlyDictionary<int, LevelInfo> levelInfoMap,
            IReadOnlyDictionary<int, Dictionary<long, LevelInfo>> levelInfoByBimDocumentIndex)
        {
            var levelInfoByBimDocumentIndexOrdered = levelInfoByBimDocumentIndex.ToDictionary(
                kv => kv.Key,
                kv => kv.Value.Values.OrderBy(li => li.Level.ProjectElevation).ToArray());

            return elementTable
                .AsParallel()
                .Select(e =>
                {
                    var bimDocumentIndex = elementTable.GetBimDocumentIndex(e.Index);

                    if (!levelInfoByBimDocumentIndexOrdered.TryGetValue(bimDocumentIndex, out var orderedLevelInfosByProjectElevation))
                        orderedLevelInfosByProjectElevation = Array.Empty<LevelInfo>();

                    if (!levelInfoByBimDocumentIndex.TryGetValue(bimDocumentIndex, out var elementIdToLevelInfoMap))
                        elementIdToLevelInfoMap = new Dictionary<long, LevelInfo>();

                    return new ElementLevelInfo(
                        e,
                        elementTable,
                        familyInstanceTable,
                        levelTable,
                        parameterTable,
                        elementIndexMaps,
                        elementGeometryMap,
                        levelInfoMap,
                        orderedLevelInfosByProjectElevation,
                        elementIdToLevelInfoMap);
                })
                .ToArray();
        }

        public static MeasureType[] CreateParameterMeasureTypes(
            ParameterTable parameterTable,
            ParameterDescriptorTable parameterDescriptorTable)
            => parameterTable.Column_ParameterDescriptorIndex
                .AsParallel()
                .Select(pdi =>
                {
                    var parameterDescriptorNameLowerInvariant = parameterDescriptorTable.GetName(pdi).ToLowerInvariant();
                    var parameterDescriptorGuid = parameterDescriptorTable.GetGuid(pdi);
                    return ParameterMeasureInfo.ParseMeasureType(parameterDescriptorNameLowerInvariant, parameterDescriptorGuid);
                })
                .ToArray();

        public static ElementMeasureInfo[] CreateElementMeasureInfos(
            ElementTable elementTable,
            ParameterTable parameterTable,
            MeasureType[] parameterMeasureTypes,
            VimElementIndexMaps elementIndexMaps)
            => elementTable
                .AsParallel()
                .Select(e => new ElementMeasureInfo(e, parameterTable, parameterMeasureTypes, elementIndexMaps))
                .ToArray();

        public static ElementIfcInfo[] CreateElementIfcInfos(
            ElementTable elementTable,
            ParameterTable parameterTable,
            VimElementIndexMaps elementIndexMaps)
            => elementTable
                .AsParallel()
                .Select(e => new ElementIfcInfo(e, parameterTable, elementIndexMaps))
                .ToArray();

        public static FamilyOmniClassInfo[] CreateFamilyOmniClassInfos(
            FamilyTable familyTable,
            ParameterTable parameterTable,
            VimElementIndexMaps elementIndexMaps)
            => familyTable.AsParallel()
                .Select(f => new FamilyOmniClassInfo(f, parameterTable, elementIndexMaps))
                .ToArray();

        public static FamilyTypeUniformatInfo[] CreateFamilyTypeUniformatInfos(
            FamilyTypeTable familyTypeTable,
            ParameterTable parameterTable,
            VimElementIndexMaps elementIndexMaps)
            => familyTypeTable.AsParallel()
                .Select(ft => new FamilyTypeUniformatInfo(ft, parameterTable, elementIndexMaps))
                .ToArray();
    }
}
