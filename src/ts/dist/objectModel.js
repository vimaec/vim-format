"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.LevelInViewTable = exports.LevelInView = exports.AssetInViewSheetTable = exports.AssetInViewSheet = exports.AssetInViewTable = exports.AssetInView = exports.ShapeInViewTable = exports.ShapeInView = exports.ElementInViewTable = exports.ElementInView = exports.ViewTable = exports.View = exports.FamilyInstanceTable = exports.FamilyInstance = exports.FamilyTypeTable = exports.FamilyType = exports.FamilyTable = exports.Family = exports.CategoryTable = exports.Category = exports.PhaseOrderInBimDocumentTable = exports.PhaseOrderInBimDocument = exports.DisplayUnitInBimDocumentTable = exports.DisplayUnitInBimDocument = exports.BimDocumentTable = exports.BimDocument = exports.RoomTable = exports.Room = exports.PhaseTable = exports.Phase = exports.LevelTable = exports.Level = exports.DesignOptionTable = exports.DesignOption = exports.GroupTable = exports.Group = exports.AssemblyInstanceTable = exports.AssemblyInstance = exports.WorksetTable = exports.Workset = exports.ElementTable = exports.Element = exports.ParameterTable = exports.Parameter = exports.ParameterDescriptorTable = exports.ParameterDescriptor = exports.DisplayUnitTable = exports.DisplayUnit = exports.AssetTable = exports.Asset = void 0;
exports.ViewSheetInViewSheetSetTable = exports.ViewSheetInViewSheetSet = exports.ViewSheetTable = exports.ViewSheet = exports.ViewSheetSetTable = exports.ViewSheetSet = exports.ScheduleCellTable = exports.ScheduleCell = exports.ScheduleColumnTable = exports.ScheduleColumn = exports.ScheduleTable = exports.Schedule = exports.AreaSchemeTable = exports.AreaScheme = exports.AreaTable = exports.Area = exports.GridTable = exports.Grid = exports.PhaseFilterTable = exports.PhaseFilter = exports.BasePointTable = exports.BasePoint = exports.ElementInWarningTable = exports.ElementInWarning = exports.WarningTable = exports.Warning = exports.ElementInSystemTable = exports.ElementInSystem = exports.SystemTable = exports.System = exports.ShapeInShapeCollectionTable = exports.ShapeInShapeCollection = exports.ShapeCollectionTable = exports.ShapeCollection = exports.ShapeTable = exports.Shape = exports.GeometryTable = exports.Geometry = exports.NodeTable = exports.Node = exports.CompoundStructureTable = exports.CompoundStructure = exports.CompoundStructureLayerTable = exports.CompoundStructureLayer = exports.MaterialInElementTable = exports.MaterialInElement = exports.MaterialTable = exports.Material = exports.CameraTable = exports.Camera = void 0;
exports.VimDocument = exports.BuildingTable = exports.Building = exports.SiteTable = exports.Site = exports.ViewInViewSheetTable = exports.ViewInViewSheet = exports.ViewInViewSheetSetTable = exports.ViewInViewSheetSet = void 0;
const entityTable_1 = require("./entityTable");
const vimLoader_1 = require("./vimLoader");
class Asset {
    static async createFromTable(table, index) {
        let result = new Asset();
        result.index = index;
        await Promise.all([
            table.getBufferName(index).then(v => result.bufferName = v),
        ]);
        return result;
    }
}
exports.Asset = Asset;
class AssetTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.Asset");
        if (!entity) {
            return undefined;
        }
        let table = new AssetTable();
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(assetIndex) {
        return await Asset.createFromTable(this, assetIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let bufferName;
        await Promise.all([
            (async () => { bufferName = (await localTable.getStringArray("string:BufferName")); })(),
        ]);
        let asset = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            asset.push({
                index: i,
                bufferName: bufferName ? bufferName[i] : undefined
            });
        }
        return asset;
    }
    async getBufferName(assetIndex) {
        return (await this.entityTable.getString(assetIndex, "string:BufferName"));
    }
    async getAllBufferName() {
        return (await this.entityTable.getStringArray("string:BufferName"));
    }
}
exports.AssetTable = AssetTable;
class DisplayUnit {
    static async createFromTable(table, index) {
        let result = new DisplayUnit();
        result.index = index;
        await Promise.all([
            table.getSpec(index).then(v => result.spec = v),
            table.getType(index).then(v => result.type = v),
            table.getLabel(index).then(v => result.label = v),
        ]);
        return result;
    }
}
exports.DisplayUnit = DisplayUnit;
class DisplayUnitTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.DisplayUnit");
        if (!entity) {
            return undefined;
        }
        let table = new DisplayUnitTable();
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(displayUnitIndex) {
        return await DisplayUnit.createFromTable(this, displayUnitIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let spec;
        let type;
        let label;
        await Promise.all([
            (async () => { spec = (await localTable.getStringArray("string:Spec")); })(),
            (async () => { type = (await localTable.getStringArray("string:Type")); })(),
            (async () => { label = (await localTable.getStringArray("string:Label")); })(),
        ]);
        let displayUnit = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            displayUnit.push({
                index: i,
                spec: spec ? spec[i] : undefined,
                type: type ? type[i] : undefined,
                label: label ? label[i] : undefined
            });
        }
        return displayUnit;
    }
    async getSpec(displayUnitIndex) {
        return (await this.entityTable.getString(displayUnitIndex, "string:Spec"));
    }
    async getAllSpec() {
        return (await this.entityTable.getStringArray("string:Spec"));
    }
    async getType(displayUnitIndex) {
        return (await this.entityTable.getString(displayUnitIndex, "string:Type"));
    }
    async getAllType() {
        return (await this.entityTable.getStringArray("string:Type"));
    }
    async getLabel(displayUnitIndex) {
        return (await this.entityTable.getString(displayUnitIndex, "string:Label"));
    }
    async getAllLabel() {
        return (await this.entityTable.getStringArray("string:Label"));
    }
}
exports.DisplayUnitTable = DisplayUnitTable;
class ParameterDescriptor {
    static async createFromTable(table, index) {
        let result = new ParameterDescriptor();
        result.index = index;
        await Promise.all([
            table.getName(index).then(v => result.name = v),
            table.getGroup(index).then(v => result.group = v),
            table.getParameterType(index).then(v => result.parameterType = v),
            table.getIsInstance(index).then(v => result.isInstance = v),
            table.getIsShared(index).then(v => result.isShared = v),
            table.getIsReadOnly(index).then(v => result.isReadOnly = v),
            table.getFlags(index).then(v => result.flags = v),
            table.getGuid(index).then(v => result.guid = v),
            table.getDisplayUnitIndex(index).then(v => result.displayUnitIndex = v),
        ]);
        return result;
    }
}
exports.ParameterDescriptor = ParameterDescriptor;
class ParameterDescriptorTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.ParameterDescriptor");
        if (!entity) {
            return undefined;
        }
        let table = new ParameterDescriptorTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(parameterDescriptorIndex) {
        return await ParameterDescriptor.createFromTable(this, parameterDescriptorIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let name;
        let group;
        let parameterType;
        let isInstance;
        let isShared;
        let isReadOnly;
        let flags;
        let guid;
        let displayUnitIndex;
        await Promise.all([
            (async () => { name = (await localTable.getStringArray("string:Name")); })(),
            (async () => { group = (await localTable.getStringArray("string:Group")); })(),
            (async () => { parameterType = (await localTable.getStringArray("string:ParameterType")); })(),
            (async () => { isInstance = (await localTable.getBooleanArray("byte:IsInstance")); })(),
            (async () => { isShared = (await localTable.getBooleanArray("byte:IsShared")); })(),
            (async () => { isReadOnly = (await localTable.getBooleanArray("byte:IsReadOnly")); })(),
            (async () => { flags = (await localTable.getNumberArray("int:Flags")); })(),
            (async () => { guid = (await localTable.getStringArray("string:Guid")); })(),
            (async () => { displayUnitIndex = (await localTable.getNumberArray("index:Vim.DisplayUnit:DisplayUnit")); })(),
        ]);
        let parameterDescriptor = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            parameterDescriptor.push({
                index: i,
                name: name ? name[i] : undefined,
                group: group ? group[i] : undefined,
                parameterType: parameterType ? parameterType[i] : undefined,
                isInstance: isInstance ? isInstance[i] : undefined,
                isShared: isShared ? isShared[i] : undefined,
                isReadOnly: isReadOnly ? isReadOnly[i] : undefined,
                flags: flags ? flags[i] : undefined,
                guid: guid ? guid[i] : undefined,
                displayUnitIndex: displayUnitIndex ? displayUnitIndex[i] : undefined
            });
        }
        return parameterDescriptor;
    }
    async getName(parameterDescriptorIndex) {
        return (await this.entityTable.getString(parameterDescriptorIndex, "string:Name"));
    }
    async getAllName() {
        return (await this.entityTable.getStringArray("string:Name"));
    }
    async getGroup(parameterDescriptorIndex) {
        return (await this.entityTable.getString(parameterDescriptorIndex, "string:Group"));
    }
    async getAllGroup() {
        return (await this.entityTable.getStringArray("string:Group"));
    }
    async getParameterType(parameterDescriptorIndex) {
        return (await this.entityTable.getString(parameterDescriptorIndex, "string:ParameterType"));
    }
    async getAllParameterType() {
        return (await this.entityTable.getStringArray("string:ParameterType"));
    }
    async getIsInstance(parameterDescriptorIndex) {
        return (await this.entityTable.getBoolean(parameterDescriptorIndex, "byte:IsInstance"));
    }
    async getAllIsInstance() {
        return (await this.entityTable.getBooleanArray("byte:IsInstance"));
    }
    async getIsShared(parameterDescriptorIndex) {
        return (await this.entityTable.getBoolean(parameterDescriptorIndex, "byte:IsShared"));
    }
    async getAllIsShared() {
        return (await this.entityTable.getBooleanArray("byte:IsShared"));
    }
    async getIsReadOnly(parameterDescriptorIndex) {
        return (await this.entityTable.getBoolean(parameterDescriptorIndex, "byte:IsReadOnly"));
    }
    async getAllIsReadOnly() {
        return (await this.entityTable.getBooleanArray("byte:IsReadOnly"));
    }
    async getFlags(parameterDescriptorIndex) {
        return (await this.entityTable.getNumber(parameterDescriptorIndex, "int:Flags"));
    }
    async getAllFlags() {
        return (await this.entityTable.getNumberArray("int:Flags"));
    }
    async getGuid(parameterDescriptorIndex) {
        return (await this.entityTable.getString(parameterDescriptorIndex, "string:Guid"));
    }
    async getAllGuid() {
        return (await this.entityTable.getStringArray("string:Guid"));
    }
    async getDisplayUnitIndex(parameterDescriptorIndex) {
        return await this.entityTable.getNumber(parameterDescriptorIndex, "index:Vim.DisplayUnit:DisplayUnit");
    }
    async getAllDisplayUnitIndex() {
        return await this.entityTable.getNumberArray("index:Vim.DisplayUnit:DisplayUnit");
    }
    async getDisplayUnit(parameterDescriptorIndex) {
        const index = await this.getDisplayUnitIndex(parameterDescriptorIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.displayUnit?.get(index);
    }
}
exports.ParameterDescriptorTable = ParameterDescriptorTable;
class Parameter {
    static async createFromTable(table, index) {
        let result = new Parameter();
        result.index = index;
        await Promise.all([
            table.getValue(index).then(v => result.value = v),
            table.getParameterDescriptorIndex(index).then(v => result.parameterDescriptorIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.Parameter = Parameter;
class ParameterTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.Parameter");
        if (!entity) {
            return undefined;
        }
        let table = new ParameterTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(parameterIndex) {
        return await Parameter.createFromTable(this, parameterIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let value;
        let parameterDescriptorIndex;
        let elementIndex;
        await Promise.all([
            (async () => { value = (await localTable.getStringArray("string:Value")); })(),
            (async () => { parameterDescriptorIndex = (await localTable.getNumberArray("index:Vim.ParameterDescriptor:ParameterDescriptor")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let parameter = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            parameter.push({
                index: i,
                value: value ? value[i] : undefined,
                parameterDescriptorIndex: parameterDescriptorIndex ? parameterDescriptorIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return parameter;
    }
    async getValue(parameterIndex) {
        return (await this.entityTable.getString(parameterIndex, "string:Value"));
    }
    async getAllValue() {
        return (await this.entityTable.getStringArray("string:Value"));
    }
    async getParameterDescriptorIndex(parameterIndex) {
        return await this.entityTable.getNumber(parameterIndex, "index:Vim.ParameterDescriptor:ParameterDescriptor");
    }
    async getAllParameterDescriptorIndex() {
        return await this.entityTable.getNumberArray("index:Vim.ParameterDescriptor:ParameterDescriptor");
    }
    async getParameterDescriptor(parameterIndex) {
        const index = await this.getParameterDescriptorIndex(parameterIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.parameterDescriptor?.get(index);
    }
    async getElementIndex(parameterIndex) {
        return await this.entityTable.getNumber(parameterIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(parameterIndex) {
        const index = await this.getElementIndex(parameterIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.ParameterTable = ParameterTable;
class Element {
    static async createFromTable(table, index) {
        let result = new Element();
        result.index = index;
        await Promise.all([
            table.getId(index).then(v => result.id = v),
            table.getType(index).then(v => result.type = v),
            table.getName(index).then(v => result.name = v),
            table.getUniqueId(index).then(v => result.uniqueId = v),
            table.getLocation_X(index).then(v => result.location_X = v),
            table.getLocation_Y(index).then(v => result.location_Y = v),
            table.getLocation_Z(index).then(v => result.location_Z = v),
            table.getFamilyName(index).then(v => result.familyName = v),
            table.getIsPinned(index).then(v => result.isPinned = v),
            table.getLevelIndex(index).then(v => result.levelIndex = v),
            table.getPhaseCreatedIndex(index).then(v => result.phaseCreatedIndex = v),
            table.getPhaseDemolishedIndex(index).then(v => result.phaseDemolishedIndex = v),
            table.getCategoryIndex(index).then(v => result.categoryIndex = v),
            table.getWorksetIndex(index).then(v => result.worksetIndex = v),
            table.getDesignOptionIndex(index).then(v => result.designOptionIndex = v),
            table.getOwnerViewIndex(index).then(v => result.ownerViewIndex = v),
            table.getGroupIndex(index).then(v => result.groupIndex = v),
            table.getAssemblyInstanceIndex(index).then(v => result.assemblyInstanceIndex = v),
            table.getBimDocumentIndex(index).then(v => result.bimDocumentIndex = v),
            table.getRoomIndex(index).then(v => result.roomIndex = v),
        ]);
        return result;
    }
}
exports.Element = Element;
class ElementTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.Element");
        if (!entity) {
            return undefined;
        }
        let table = new ElementTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(elementIndex) {
        return await Element.createFromTable(this, elementIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let id;
        let type;
        let name;
        let uniqueId;
        let location_X;
        let location_Y;
        let location_Z;
        let familyName;
        let isPinned;
        let levelIndex;
        let phaseCreatedIndex;
        let phaseDemolishedIndex;
        let categoryIndex;
        let worksetIndex;
        let designOptionIndex;
        let ownerViewIndex;
        let groupIndex;
        let assemblyInstanceIndex;
        let bimDocumentIndex;
        let roomIndex;
        await Promise.all([
            (async () => { id = (await localTable.getBigIntArray("long:Id")) ?? (await localTable.getBigIntArray("int:Id")); })(),
            (async () => { type = (await localTable.getStringArray("string:Type")); })(),
            (async () => { name = (await localTable.getStringArray("string:Name")); })(),
            (async () => { uniqueId = (await localTable.getStringArray("string:UniqueId")); })(),
            (async () => { location_X = (await localTable.getNumberArray("float:Location.X")); })(),
            (async () => { location_Y = (await localTable.getNumberArray("float:Location.Y")); })(),
            (async () => { location_Z = (await localTable.getNumberArray("float:Location.Z")); })(),
            (async () => { familyName = (await localTable.getStringArray("string:FamilyName")); })(),
            (async () => { isPinned = (await localTable.getBooleanArray("byte:IsPinned")); })(),
            (async () => { levelIndex = (await localTable.getNumberArray("index:Vim.Level:Level")); })(),
            (async () => { phaseCreatedIndex = (await localTable.getNumberArray("index:Vim.Phase:PhaseCreated")); })(),
            (async () => { phaseDemolishedIndex = (await localTable.getNumberArray("index:Vim.Phase:PhaseDemolished")); })(),
            (async () => { categoryIndex = (await localTable.getNumberArray("index:Vim.Category:Category")); })(),
            (async () => { worksetIndex = (await localTable.getNumberArray("index:Vim.Workset:Workset")); })(),
            (async () => { designOptionIndex = (await localTable.getNumberArray("index:Vim.DesignOption:DesignOption")); })(),
            (async () => { ownerViewIndex = (await localTable.getNumberArray("index:Vim.View:OwnerView")); })(),
            (async () => { groupIndex = (await localTable.getNumberArray("index:Vim.Group:Group")); })(),
            (async () => { assemblyInstanceIndex = (await localTable.getNumberArray("index:Vim.AssemblyInstance:AssemblyInstance")); })(),
            (async () => { bimDocumentIndex = (await localTable.getNumberArray("index:Vim.BimDocument:BimDocument")); })(),
            (async () => { roomIndex = (await localTable.getNumberArray("index:Vim.Room:Room")); })(),
        ]);
        let element = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            element.push({
                index: i,
                id: id ? id[i] : undefined,
                type: type ? type[i] : undefined,
                name: name ? name[i] : undefined,
                uniqueId: uniqueId ? uniqueId[i] : undefined,
                location_X: location_X ? location_X[i] : undefined,
                location_Y: location_Y ? location_Y[i] : undefined,
                location_Z: location_Z ? location_Z[i] : undefined,
                familyName: familyName ? familyName[i] : undefined,
                isPinned: isPinned ? isPinned[i] : undefined,
                levelIndex: levelIndex ? levelIndex[i] : undefined,
                phaseCreatedIndex: phaseCreatedIndex ? phaseCreatedIndex[i] : undefined,
                phaseDemolishedIndex: phaseDemolishedIndex ? phaseDemolishedIndex[i] : undefined,
                categoryIndex: categoryIndex ? categoryIndex[i] : undefined,
                worksetIndex: worksetIndex ? worksetIndex[i] : undefined,
                designOptionIndex: designOptionIndex ? designOptionIndex[i] : undefined,
                ownerViewIndex: ownerViewIndex ? ownerViewIndex[i] : undefined,
                groupIndex: groupIndex ? groupIndex[i] : undefined,
                assemblyInstanceIndex: assemblyInstanceIndex ? assemblyInstanceIndex[i] : undefined,
                bimDocumentIndex: bimDocumentIndex ? bimDocumentIndex[i] : undefined,
                roomIndex: roomIndex ? roomIndex[i] : undefined
            });
        }
        return element;
    }
    async getId(elementIndex) {
        return (await this.entityTable.getBigInt(elementIndex, "long:Id")) ?? (await this.entityTable.getBigInt(elementIndex, "int:Id"));
    }
    async getAllId() {
        return (await this.entityTable.getBigIntArray("long:Id")) ?? (await this.entityTable.getBigIntArray("int:Id"));
    }
    async getType(elementIndex) {
        return (await this.entityTable.getString(elementIndex, "string:Type"));
    }
    async getAllType() {
        return (await this.entityTable.getStringArray("string:Type"));
    }
    async getName(elementIndex) {
        return (await this.entityTable.getString(elementIndex, "string:Name"));
    }
    async getAllName() {
        return (await this.entityTable.getStringArray("string:Name"));
    }
    async getUniqueId(elementIndex) {
        return (await this.entityTable.getString(elementIndex, "string:UniqueId"));
    }
    async getAllUniqueId() {
        return (await this.entityTable.getStringArray("string:UniqueId"));
    }
    async getLocation_X(elementIndex) {
        return (await this.entityTable.getNumber(elementIndex, "float:Location.X"));
    }
    async getAllLocation_X() {
        return (await this.entityTable.getNumberArray("float:Location.X"));
    }
    async getLocation_Y(elementIndex) {
        return (await this.entityTable.getNumber(elementIndex, "float:Location.Y"));
    }
    async getAllLocation_Y() {
        return (await this.entityTable.getNumberArray("float:Location.Y"));
    }
    async getLocation_Z(elementIndex) {
        return (await this.entityTable.getNumber(elementIndex, "float:Location.Z"));
    }
    async getAllLocation_Z() {
        return (await this.entityTable.getNumberArray("float:Location.Z"));
    }
    async getFamilyName(elementIndex) {
        return (await this.entityTable.getString(elementIndex, "string:FamilyName"));
    }
    async getAllFamilyName() {
        return (await this.entityTable.getStringArray("string:FamilyName"));
    }
    async getIsPinned(elementIndex) {
        return (await this.entityTable.getBoolean(elementIndex, "byte:IsPinned"));
    }
    async getAllIsPinned() {
        return (await this.entityTable.getBooleanArray("byte:IsPinned"));
    }
    async getLevelIndex(elementIndex) {
        return await this.entityTable.getNumber(elementIndex, "index:Vim.Level:Level");
    }
    async getAllLevelIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Level:Level");
    }
    async getLevel(elementIndex) {
        const index = await this.getLevelIndex(elementIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.level?.get(index);
    }
    async getPhaseCreatedIndex(elementIndex) {
        return await this.entityTable.getNumber(elementIndex, "index:Vim.Phase:PhaseCreated");
    }
    async getAllPhaseCreatedIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Phase:PhaseCreated");
    }
    async getPhaseCreated(elementIndex) {
        const index = await this.getPhaseCreatedIndex(elementIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.phase?.get(index);
    }
    async getPhaseDemolishedIndex(elementIndex) {
        return await this.entityTable.getNumber(elementIndex, "index:Vim.Phase:PhaseDemolished");
    }
    async getAllPhaseDemolishedIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Phase:PhaseDemolished");
    }
    async getPhaseDemolished(elementIndex) {
        const index = await this.getPhaseDemolishedIndex(elementIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.phase?.get(index);
    }
    async getCategoryIndex(elementIndex) {
        return await this.entityTable.getNumber(elementIndex, "index:Vim.Category:Category");
    }
    async getAllCategoryIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Category:Category");
    }
    async getCategory(elementIndex) {
        const index = await this.getCategoryIndex(elementIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.category?.get(index);
    }
    async getWorksetIndex(elementIndex) {
        return await this.entityTable.getNumber(elementIndex, "index:Vim.Workset:Workset");
    }
    async getAllWorksetIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Workset:Workset");
    }
    async getWorkset(elementIndex) {
        const index = await this.getWorksetIndex(elementIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.workset?.get(index);
    }
    async getDesignOptionIndex(elementIndex) {
        return await this.entityTable.getNumber(elementIndex, "index:Vim.DesignOption:DesignOption");
    }
    async getAllDesignOptionIndex() {
        return await this.entityTable.getNumberArray("index:Vim.DesignOption:DesignOption");
    }
    async getDesignOption(elementIndex) {
        const index = await this.getDesignOptionIndex(elementIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.designOption?.get(index);
    }
    async getOwnerViewIndex(elementIndex) {
        return await this.entityTable.getNumber(elementIndex, "index:Vim.View:OwnerView");
    }
    async getAllOwnerViewIndex() {
        return await this.entityTable.getNumberArray("index:Vim.View:OwnerView");
    }
    async getOwnerView(elementIndex) {
        const index = await this.getOwnerViewIndex(elementIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.view?.get(index);
    }
    async getGroupIndex(elementIndex) {
        return await this.entityTable.getNumber(elementIndex, "index:Vim.Group:Group");
    }
    async getAllGroupIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Group:Group");
    }
    async getGroup(elementIndex) {
        const index = await this.getGroupIndex(elementIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.group?.get(index);
    }
    async getAssemblyInstanceIndex(elementIndex) {
        return await this.entityTable.getNumber(elementIndex, "index:Vim.AssemblyInstance:AssemblyInstance");
    }
    async getAllAssemblyInstanceIndex() {
        return await this.entityTable.getNumberArray("index:Vim.AssemblyInstance:AssemblyInstance");
    }
    async getAssemblyInstance(elementIndex) {
        const index = await this.getAssemblyInstanceIndex(elementIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.assemblyInstance?.get(index);
    }
    async getBimDocumentIndex(elementIndex) {
        return await this.entityTable.getNumber(elementIndex, "index:Vim.BimDocument:BimDocument");
    }
    async getAllBimDocumentIndex() {
        return await this.entityTable.getNumberArray("index:Vim.BimDocument:BimDocument");
    }
    async getBimDocument(elementIndex) {
        const index = await this.getBimDocumentIndex(elementIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.bimDocument?.get(index);
    }
    async getRoomIndex(elementIndex) {
        return await this.entityTable.getNumber(elementIndex, "index:Vim.Room:Room");
    }
    async getAllRoomIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Room:Room");
    }
    async getRoom(elementIndex) {
        const index = await this.getRoomIndex(elementIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.room?.get(index);
    }
}
exports.ElementTable = ElementTable;
class Workset {
    static async createFromTable(table, index) {
        let result = new Workset();
        result.index = index;
        await Promise.all([
            table.getId(index).then(v => result.id = v),
            table.getName(index).then(v => result.name = v),
            table.getKind(index).then(v => result.kind = v),
            table.getIsOpen(index).then(v => result.isOpen = v),
            table.getIsEditable(index).then(v => result.isEditable = v),
            table.getOwner(index).then(v => result.owner = v),
            table.getUniqueId(index).then(v => result.uniqueId = v),
            table.getBimDocumentIndex(index).then(v => result.bimDocumentIndex = v),
        ]);
        return result;
    }
}
exports.Workset = Workset;
class WorksetTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.Workset");
        if (!entity) {
            return undefined;
        }
        let table = new WorksetTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(worksetIndex) {
        return await Workset.createFromTable(this, worksetIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let id;
        let name;
        let kind;
        let isOpen;
        let isEditable;
        let owner;
        let uniqueId;
        let bimDocumentIndex;
        await Promise.all([
            (async () => { id = (await localTable.getNumberArray("int:Id")); })(),
            (async () => { name = (await localTable.getStringArray("string:Name")); })(),
            (async () => { kind = (await localTable.getStringArray("string:Kind")); })(),
            (async () => { isOpen = (await localTable.getBooleanArray("byte:IsOpen")); })(),
            (async () => { isEditable = (await localTable.getBooleanArray("byte:IsEditable")); })(),
            (async () => { owner = (await localTable.getStringArray("string:Owner")); })(),
            (async () => { uniqueId = (await localTable.getStringArray("string:UniqueId")); })(),
            (async () => { bimDocumentIndex = (await localTable.getNumberArray("index:Vim.BimDocument:BimDocument")); })(),
        ]);
        let workset = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            workset.push({
                index: i,
                id: id ? id[i] : undefined,
                name: name ? name[i] : undefined,
                kind: kind ? kind[i] : undefined,
                isOpen: isOpen ? isOpen[i] : undefined,
                isEditable: isEditable ? isEditable[i] : undefined,
                owner: owner ? owner[i] : undefined,
                uniqueId: uniqueId ? uniqueId[i] : undefined,
                bimDocumentIndex: bimDocumentIndex ? bimDocumentIndex[i] : undefined
            });
        }
        return workset;
    }
    async getId(worksetIndex) {
        return (await this.entityTable.getNumber(worksetIndex, "int:Id"));
    }
    async getAllId() {
        return (await this.entityTable.getNumberArray("int:Id"));
    }
    async getName(worksetIndex) {
        return (await this.entityTable.getString(worksetIndex, "string:Name"));
    }
    async getAllName() {
        return (await this.entityTable.getStringArray("string:Name"));
    }
    async getKind(worksetIndex) {
        return (await this.entityTable.getString(worksetIndex, "string:Kind"));
    }
    async getAllKind() {
        return (await this.entityTable.getStringArray("string:Kind"));
    }
    async getIsOpen(worksetIndex) {
        return (await this.entityTable.getBoolean(worksetIndex, "byte:IsOpen"));
    }
    async getAllIsOpen() {
        return (await this.entityTable.getBooleanArray("byte:IsOpen"));
    }
    async getIsEditable(worksetIndex) {
        return (await this.entityTable.getBoolean(worksetIndex, "byte:IsEditable"));
    }
    async getAllIsEditable() {
        return (await this.entityTable.getBooleanArray("byte:IsEditable"));
    }
    async getOwner(worksetIndex) {
        return (await this.entityTable.getString(worksetIndex, "string:Owner"));
    }
    async getAllOwner() {
        return (await this.entityTable.getStringArray("string:Owner"));
    }
    async getUniqueId(worksetIndex) {
        return (await this.entityTable.getString(worksetIndex, "string:UniqueId"));
    }
    async getAllUniqueId() {
        return (await this.entityTable.getStringArray("string:UniqueId"));
    }
    async getBimDocumentIndex(worksetIndex) {
        return await this.entityTable.getNumber(worksetIndex, "index:Vim.BimDocument:BimDocument");
    }
    async getAllBimDocumentIndex() {
        return await this.entityTable.getNumberArray("index:Vim.BimDocument:BimDocument");
    }
    async getBimDocument(worksetIndex) {
        const index = await this.getBimDocumentIndex(worksetIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.bimDocument?.get(index);
    }
}
exports.WorksetTable = WorksetTable;
class AssemblyInstance {
    static async createFromTable(table, index) {
        let result = new AssemblyInstance();
        result.index = index;
        await Promise.all([
            table.getAssemblyTypeName(index).then(v => result.assemblyTypeName = v),
            table.getPosition_X(index).then(v => result.position_X = v),
            table.getPosition_Y(index).then(v => result.position_Y = v),
            table.getPosition_Z(index).then(v => result.position_Z = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.AssemblyInstance = AssemblyInstance;
class AssemblyInstanceTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.AssemblyInstance");
        if (!entity) {
            return undefined;
        }
        let table = new AssemblyInstanceTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(assemblyInstanceIndex) {
        return await AssemblyInstance.createFromTable(this, assemblyInstanceIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let assemblyTypeName;
        let position_X;
        let position_Y;
        let position_Z;
        let elementIndex;
        await Promise.all([
            (async () => { assemblyTypeName = (await localTable.getStringArray("string:AssemblyTypeName")); })(),
            (async () => { position_X = (await localTable.getNumberArray("float:Position.X")); })(),
            (async () => { position_Y = (await localTable.getNumberArray("float:Position.Y")); })(),
            (async () => { position_Z = (await localTable.getNumberArray("float:Position.Z")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let assemblyInstance = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            assemblyInstance.push({
                index: i,
                assemblyTypeName: assemblyTypeName ? assemblyTypeName[i] : undefined,
                position_X: position_X ? position_X[i] : undefined,
                position_Y: position_Y ? position_Y[i] : undefined,
                position_Z: position_Z ? position_Z[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return assemblyInstance;
    }
    async getAssemblyTypeName(assemblyInstanceIndex) {
        return (await this.entityTable.getString(assemblyInstanceIndex, "string:AssemblyTypeName"));
    }
    async getAllAssemblyTypeName() {
        return (await this.entityTable.getStringArray("string:AssemblyTypeName"));
    }
    async getPosition_X(assemblyInstanceIndex) {
        return (await this.entityTable.getNumber(assemblyInstanceIndex, "float:Position.X"));
    }
    async getAllPosition_X() {
        return (await this.entityTable.getNumberArray("float:Position.X"));
    }
    async getPosition_Y(assemblyInstanceIndex) {
        return (await this.entityTable.getNumber(assemblyInstanceIndex, "float:Position.Y"));
    }
    async getAllPosition_Y() {
        return (await this.entityTable.getNumberArray("float:Position.Y"));
    }
    async getPosition_Z(assemblyInstanceIndex) {
        return (await this.entityTable.getNumber(assemblyInstanceIndex, "float:Position.Z"));
    }
    async getAllPosition_Z() {
        return (await this.entityTable.getNumberArray("float:Position.Z"));
    }
    async getElementIndex(assemblyInstanceIndex) {
        return await this.entityTable.getNumber(assemblyInstanceIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(assemblyInstanceIndex) {
        const index = await this.getElementIndex(assemblyInstanceIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.AssemblyInstanceTable = AssemblyInstanceTable;
class Group {
    static async createFromTable(table, index) {
        let result = new Group();
        result.index = index;
        await Promise.all([
            table.getGroupType(index).then(v => result.groupType = v),
            table.getPosition_X(index).then(v => result.position_X = v),
            table.getPosition_Y(index).then(v => result.position_Y = v),
            table.getPosition_Z(index).then(v => result.position_Z = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.Group = Group;
class GroupTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.Group");
        if (!entity) {
            return undefined;
        }
        let table = new GroupTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(groupIndex) {
        return await Group.createFromTable(this, groupIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let groupType;
        let position_X;
        let position_Y;
        let position_Z;
        let elementIndex;
        await Promise.all([
            (async () => { groupType = (await localTable.getStringArray("string:GroupType")); })(),
            (async () => { position_X = (await localTable.getNumberArray("float:Position.X")); })(),
            (async () => { position_Y = (await localTable.getNumberArray("float:Position.Y")); })(),
            (async () => { position_Z = (await localTable.getNumberArray("float:Position.Z")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let group = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            group.push({
                index: i,
                groupType: groupType ? groupType[i] : undefined,
                position_X: position_X ? position_X[i] : undefined,
                position_Y: position_Y ? position_Y[i] : undefined,
                position_Z: position_Z ? position_Z[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return group;
    }
    async getGroupType(groupIndex) {
        return (await this.entityTable.getString(groupIndex, "string:GroupType"));
    }
    async getAllGroupType() {
        return (await this.entityTable.getStringArray("string:GroupType"));
    }
    async getPosition_X(groupIndex) {
        return (await this.entityTable.getNumber(groupIndex, "float:Position.X"));
    }
    async getAllPosition_X() {
        return (await this.entityTable.getNumberArray("float:Position.X"));
    }
    async getPosition_Y(groupIndex) {
        return (await this.entityTable.getNumber(groupIndex, "float:Position.Y"));
    }
    async getAllPosition_Y() {
        return (await this.entityTable.getNumberArray("float:Position.Y"));
    }
    async getPosition_Z(groupIndex) {
        return (await this.entityTable.getNumber(groupIndex, "float:Position.Z"));
    }
    async getAllPosition_Z() {
        return (await this.entityTable.getNumberArray("float:Position.Z"));
    }
    async getElementIndex(groupIndex) {
        return await this.entityTable.getNumber(groupIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(groupIndex) {
        const index = await this.getElementIndex(groupIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.GroupTable = GroupTable;
class DesignOption {
    static async createFromTable(table, index) {
        let result = new DesignOption();
        result.index = index;
        await Promise.all([
            table.getIsPrimary(index).then(v => result.isPrimary = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.DesignOption = DesignOption;
class DesignOptionTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.DesignOption");
        if (!entity) {
            return undefined;
        }
        let table = new DesignOptionTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(designOptionIndex) {
        return await DesignOption.createFromTable(this, designOptionIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let isPrimary;
        let elementIndex;
        await Promise.all([
            (async () => { isPrimary = (await localTable.getBooleanArray("byte:IsPrimary")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let designOption = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            designOption.push({
                index: i,
                isPrimary: isPrimary ? isPrimary[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return designOption;
    }
    async getIsPrimary(designOptionIndex) {
        return (await this.entityTable.getBoolean(designOptionIndex, "byte:IsPrimary"));
    }
    async getAllIsPrimary() {
        return (await this.entityTable.getBooleanArray("byte:IsPrimary"));
    }
    async getElementIndex(designOptionIndex) {
        return await this.entityTable.getNumber(designOptionIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(designOptionIndex) {
        const index = await this.getElementIndex(designOptionIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.DesignOptionTable = DesignOptionTable;
class Level {
    static async createFromTable(table, index) {
        let result = new Level();
        result.index = index;
        await Promise.all([
            table.getElevation(index).then(v => result.elevation = v),
            table.getFamilyTypeIndex(index).then(v => result.familyTypeIndex = v),
            table.getBuildingIndex(index).then(v => result.buildingIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.Level = Level;
class LevelTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.Level");
        if (!entity) {
            return undefined;
        }
        let table = new LevelTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(levelIndex) {
        return await Level.createFromTable(this, levelIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let elevation;
        let familyTypeIndex;
        let buildingIndex;
        let elementIndex;
        await Promise.all([
            (async () => { elevation = (await localTable.getNumberArray("double:Elevation")); })(),
            (async () => { familyTypeIndex = (await localTable.getNumberArray("index:Vim.FamilyType:FamilyType")); })(),
            (async () => { buildingIndex = (await localTable.getNumberArray("index:Vim.Building:Building")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let level = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            level.push({
                index: i,
                elevation: elevation ? elevation[i] : undefined,
                familyTypeIndex: familyTypeIndex ? familyTypeIndex[i] : undefined,
                buildingIndex: buildingIndex ? buildingIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return level;
    }
    async getElevation(levelIndex) {
        return (await this.entityTable.getNumber(levelIndex, "double:Elevation"));
    }
    async getAllElevation() {
        return (await this.entityTable.getNumberArray("double:Elevation"));
    }
    async getFamilyTypeIndex(levelIndex) {
        return await this.entityTable.getNumber(levelIndex, "index:Vim.FamilyType:FamilyType");
    }
    async getAllFamilyTypeIndex() {
        return await this.entityTable.getNumberArray("index:Vim.FamilyType:FamilyType");
    }
    async getFamilyType(levelIndex) {
        const index = await this.getFamilyTypeIndex(levelIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.familyType?.get(index);
    }
    async getBuildingIndex(levelIndex) {
        return await this.entityTable.getNumber(levelIndex, "index:Vim.Building:Building");
    }
    async getAllBuildingIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Building:Building");
    }
    async getBuilding(levelIndex) {
        const index = await this.getBuildingIndex(levelIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.building?.get(index);
    }
    async getElementIndex(levelIndex) {
        return await this.entityTable.getNumber(levelIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(levelIndex) {
        const index = await this.getElementIndex(levelIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.LevelTable = LevelTable;
class Phase {
    static async createFromTable(table, index) {
        let result = new Phase();
        result.index = index;
        await Promise.all([
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.Phase = Phase;
class PhaseTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.Phase");
        if (!entity) {
            return undefined;
        }
        let table = new PhaseTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(phaseIndex) {
        return await Phase.createFromTable(this, phaseIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let elementIndex;
        await Promise.all([
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let phase = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            phase.push({
                index: i,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return phase;
    }
    async getElementIndex(phaseIndex) {
        return await this.entityTable.getNumber(phaseIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(phaseIndex) {
        const index = await this.getElementIndex(phaseIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.PhaseTable = PhaseTable;
class Room {
    static async createFromTable(table, index) {
        let result = new Room();
        result.index = index;
        await Promise.all([
            table.getBaseOffset(index).then(v => result.baseOffset = v),
            table.getLimitOffset(index).then(v => result.limitOffset = v),
            table.getUnboundedHeight(index).then(v => result.unboundedHeight = v),
            table.getVolume(index).then(v => result.volume = v),
            table.getPerimeter(index).then(v => result.perimeter = v),
            table.getArea(index).then(v => result.area = v),
            table.getNumber(index).then(v => result.number = v),
            table.getUpperLimitIndex(index).then(v => result.upperLimitIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.Room = Room;
class RoomTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.Room");
        if (!entity) {
            return undefined;
        }
        let table = new RoomTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(roomIndex) {
        return await Room.createFromTable(this, roomIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let baseOffset;
        let limitOffset;
        let unboundedHeight;
        let volume;
        let perimeter;
        let area;
        let number;
        let upperLimitIndex;
        let elementIndex;
        await Promise.all([
            (async () => { baseOffset = (await localTable.getNumberArray("double:BaseOffset")); })(),
            (async () => { limitOffset = (await localTable.getNumberArray("double:LimitOffset")); })(),
            (async () => { unboundedHeight = (await localTable.getNumberArray("double:UnboundedHeight")); })(),
            (async () => { volume = (await localTable.getNumberArray("double:Volume")); })(),
            (async () => { perimeter = (await localTable.getNumberArray("double:Perimeter")); })(),
            (async () => { area = (await localTable.getNumberArray("double:Area")); })(),
            (async () => { number = (await localTable.getStringArray("string:Number")); })(),
            (async () => { upperLimitIndex = (await localTable.getNumberArray("index:Vim.Level:UpperLimit")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let room = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            room.push({
                index: i,
                baseOffset: baseOffset ? baseOffset[i] : undefined,
                limitOffset: limitOffset ? limitOffset[i] : undefined,
                unboundedHeight: unboundedHeight ? unboundedHeight[i] : undefined,
                volume: volume ? volume[i] : undefined,
                perimeter: perimeter ? perimeter[i] : undefined,
                area: area ? area[i] : undefined,
                number: number ? number[i] : undefined,
                upperLimitIndex: upperLimitIndex ? upperLimitIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return room;
    }
    async getBaseOffset(roomIndex) {
        return (await this.entityTable.getNumber(roomIndex, "double:BaseOffset"));
    }
    async getAllBaseOffset() {
        return (await this.entityTable.getNumberArray("double:BaseOffset"));
    }
    async getLimitOffset(roomIndex) {
        return (await this.entityTable.getNumber(roomIndex, "double:LimitOffset"));
    }
    async getAllLimitOffset() {
        return (await this.entityTable.getNumberArray("double:LimitOffset"));
    }
    async getUnboundedHeight(roomIndex) {
        return (await this.entityTable.getNumber(roomIndex, "double:UnboundedHeight"));
    }
    async getAllUnboundedHeight() {
        return (await this.entityTable.getNumberArray("double:UnboundedHeight"));
    }
    async getVolume(roomIndex) {
        return (await this.entityTable.getNumber(roomIndex, "double:Volume"));
    }
    async getAllVolume() {
        return (await this.entityTable.getNumberArray("double:Volume"));
    }
    async getPerimeter(roomIndex) {
        return (await this.entityTable.getNumber(roomIndex, "double:Perimeter"));
    }
    async getAllPerimeter() {
        return (await this.entityTable.getNumberArray("double:Perimeter"));
    }
    async getArea(roomIndex) {
        return (await this.entityTable.getNumber(roomIndex, "double:Area"));
    }
    async getAllArea() {
        return (await this.entityTable.getNumberArray("double:Area"));
    }
    async getNumber(roomIndex) {
        return (await this.entityTable.getString(roomIndex, "string:Number"));
    }
    async getAllNumber() {
        return (await this.entityTable.getStringArray("string:Number"));
    }
    async getUpperLimitIndex(roomIndex) {
        return await this.entityTable.getNumber(roomIndex, "index:Vim.Level:UpperLimit");
    }
    async getAllUpperLimitIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Level:UpperLimit");
    }
    async getUpperLimit(roomIndex) {
        const index = await this.getUpperLimitIndex(roomIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.level?.get(index);
    }
    async getElementIndex(roomIndex) {
        return await this.entityTable.getNumber(roomIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(roomIndex) {
        const index = await this.getElementIndex(roomIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.RoomTable = RoomTable;
class BimDocument {
    static async createFromTable(table, index) {
        let result = new BimDocument();
        result.index = index;
        await Promise.all([
            table.getTitle(index).then(v => result.title = v),
            table.getIsMetric(index).then(v => result.isMetric = v),
            table.getGuid(index).then(v => result.guid = v),
            table.getNumSaves(index).then(v => result.numSaves = v),
            table.getIsLinked(index).then(v => result.isLinked = v),
            table.getIsDetached(index).then(v => result.isDetached = v),
            table.getIsWorkshared(index).then(v => result.isWorkshared = v),
            table.getPathName(index).then(v => result.pathName = v),
            table.getLatitude(index).then(v => result.latitude = v),
            table.getLongitude(index).then(v => result.longitude = v),
            table.getTimeZone(index).then(v => result.timeZone = v),
            table.getPlaceName(index).then(v => result.placeName = v),
            table.getWeatherStationName(index).then(v => result.weatherStationName = v),
            table.getElevation(index).then(v => result.elevation = v),
            table.getProjectLocation(index).then(v => result.projectLocation = v),
            table.getIssueDate(index).then(v => result.issueDate = v),
            table.getStatus(index).then(v => result.status = v),
            table.getClientName(index).then(v => result.clientName = v),
            table.getAddress(index).then(v => result.address = v),
            table.getName(index).then(v => result.name = v),
            table.getNumber(index).then(v => result.number = v),
            table.getAuthor(index).then(v => result.author = v),
            table.getBuildingName(index).then(v => result.buildingName = v),
            table.getOrganizationName(index).then(v => result.organizationName = v),
            table.getOrganizationDescription(index).then(v => result.organizationDescription = v),
            table.getProduct(index).then(v => result.product = v),
            table.getVersion(index).then(v => result.version = v),
            table.getUser(index).then(v => result.user = v),
            table.getActiveViewIndex(index).then(v => result.activeViewIndex = v),
            table.getOwnerFamilyIndex(index).then(v => result.ownerFamilyIndex = v),
            table.getParentIndex(index).then(v => result.parentIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.BimDocument = BimDocument;
class BimDocumentTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.BimDocument");
        if (!entity) {
            return undefined;
        }
        let table = new BimDocumentTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(bimDocumentIndex) {
        return await BimDocument.createFromTable(this, bimDocumentIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let title;
        let isMetric;
        let guid;
        let numSaves;
        let isLinked;
        let isDetached;
        let isWorkshared;
        let pathName;
        let latitude;
        let longitude;
        let timeZone;
        let placeName;
        let weatherStationName;
        let elevation;
        let projectLocation;
        let issueDate;
        let status;
        let clientName;
        let address;
        let name;
        let number;
        let author;
        let buildingName;
        let organizationName;
        let organizationDescription;
        let product;
        let version;
        let user;
        let activeViewIndex;
        let ownerFamilyIndex;
        let parentIndex;
        let elementIndex;
        await Promise.all([
            (async () => { title = (await localTable.getStringArray("string:Title")); })(),
            (async () => { isMetric = (await localTable.getBooleanArray("byte:IsMetric")); })(),
            (async () => { guid = (await localTable.getStringArray("string:Guid")); })(),
            (async () => { numSaves = (await localTable.getNumberArray("int:NumSaves")); })(),
            (async () => { isLinked = (await localTable.getBooleanArray("byte:IsLinked")); })(),
            (async () => { isDetached = (await localTable.getBooleanArray("byte:IsDetached")); })(),
            (async () => { isWorkshared = (await localTable.getBooleanArray("byte:IsWorkshared")); })(),
            (async () => { pathName = (await localTable.getStringArray("string:PathName")); })(),
            (async () => { latitude = (await localTable.getNumberArray("double:Latitude")); })(),
            (async () => { longitude = (await localTable.getNumberArray("double:Longitude")); })(),
            (async () => { timeZone = (await localTable.getNumberArray("double:TimeZone")); })(),
            (async () => { placeName = (await localTable.getStringArray("string:PlaceName")); })(),
            (async () => { weatherStationName = (await localTable.getStringArray("string:WeatherStationName")); })(),
            (async () => { elevation = (await localTable.getNumberArray("double:Elevation")); })(),
            (async () => { projectLocation = (await localTable.getStringArray("string:ProjectLocation")); })(),
            (async () => { issueDate = (await localTable.getStringArray("string:IssueDate")); })(),
            (async () => { status = (await localTable.getStringArray("string:Status")); })(),
            (async () => { clientName = (await localTable.getStringArray("string:ClientName")); })(),
            (async () => { address = (await localTable.getStringArray("string:Address")); })(),
            (async () => { name = (await localTable.getStringArray("string:Name")); })(),
            (async () => { number = (await localTable.getStringArray("string:Number")); })(),
            (async () => { author = (await localTable.getStringArray("string:Author")); })(),
            (async () => { buildingName = (await localTable.getStringArray("string:BuildingName")); })(),
            (async () => { organizationName = (await localTable.getStringArray("string:OrganizationName")); })(),
            (async () => { organizationDescription = (await localTable.getStringArray("string:OrganizationDescription")); })(),
            (async () => { product = (await localTable.getStringArray("string:Product")); })(),
            (async () => { version = (await localTable.getStringArray("string:Version")); })(),
            (async () => { user = (await localTable.getStringArray("string:User")); })(),
            (async () => { activeViewIndex = (await localTable.getNumberArray("index:Vim.View:ActiveView")); })(),
            (async () => { ownerFamilyIndex = (await localTable.getNumberArray("index:Vim.Family:OwnerFamily")); })(),
            (async () => { parentIndex = (await localTable.getNumberArray("index:Vim.BimDocument:Parent")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let bimDocument = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            bimDocument.push({
                index: i,
                title: title ? title[i] : undefined,
                isMetric: isMetric ? isMetric[i] : undefined,
                guid: guid ? guid[i] : undefined,
                numSaves: numSaves ? numSaves[i] : undefined,
                isLinked: isLinked ? isLinked[i] : undefined,
                isDetached: isDetached ? isDetached[i] : undefined,
                isWorkshared: isWorkshared ? isWorkshared[i] : undefined,
                pathName: pathName ? pathName[i] : undefined,
                latitude: latitude ? latitude[i] : undefined,
                longitude: longitude ? longitude[i] : undefined,
                timeZone: timeZone ? timeZone[i] : undefined,
                placeName: placeName ? placeName[i] : undefined,
                weatherStationName: weatherStationName ? weatherStationName[i] : undefined,
                elevation: elevation ? elevation[i] : undefined,
                projectLocation: projectLocation ? projectLocation[i] : undefined,
                issueDate: issueDate ? issueDate[i] : undefined,
                status: status ? status[i] : undefined,
                clientName: clientName ? clientName[i] : undefined,
                address: address ? address[i] : undefined,
                name: name ? name[i] : undefined,
                number: number ? number[i] : undefined,
                author: author ? author[i] : undefined,
                buildingName: buildingName ? buildingName[i] : undefined,
                organizationName: organizationName ? organizationName[i] : undefined,
                organizationDescription: organizationDescription ? organizationDescription[i] : undefined,
                product: product ? product[i] : undefined,
                version: version ? version[i] : undefined,
                user: user ? user[i] : undefined,
                activeViewIndex: activeViewIndex ? activeViewIndex[i] : undefined,
                ownerFamilyIndex: ownerFamilyIndex ? ownerFamilyIndex[i] : undefined,
                parentIndex: parentIndex ? parentIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return bimDocument;
    }
    async getTitle(bimDocumentIndex) {
        return (await this.entityTable.getString(bimDocumentIndex, "string:Title"));
    }
    async getAllTitle() {
        return (await this.entityTable.getStringArray("string:Title"));
    }
    async getIsMetric(bimDocumentIndex) {
        return (await this.entityTable.getBoolean(bimDocumentIndex, "byte:IsMetric"));
    }
    async getAllIsMetric() {
        return (await this.entityTable.getBooleanArray("byte:IsMetric"));
    }
    async getGuid(bimDocumentIndex) {
        return (await this.entityTable.getString(bimDocumentIndex, "string:Guid"));
    }
    async getAllGuid() {
        return (await this.entityTable.getStringArray("string:Guid"));
    }
    async getNumSaves(bimDocumentIndex) {
        return (await this.entityTable.getNumber(bimDocumentIndex, "int:NumSaves"));
    }
    async getAllNumSaves() {
        return (await this.entityTable.getNumberArray("int:NumSaves"));
    }
    async getIsLinked(bimDocumentIndex) {
        return (await this.entityTable.getBoolean(bimDocumentIndex, "byte:IsLinked"));
    }
    async getAllIsLinked() {
        return (await this.entityTable.getBooleanArray("byte:IsLinked"));
    }
    async getIsDetached(bimDocumentIndex) {
        return (await this.entityTable.getBoolean(bimDocumentIndex, "byte:IsDetached"));
    }
    async getAllIsDetached() {
        return (await this.entityTable.getBooleanArray("byte:IsDetached"));
    }
    async getIsWorkshared(bimDocumentIndex) {
        return (await this.entityTable.getBoolean(bimDocumentIndex, "byte:IsWorkshared"));
    }
    async getAllIsWorkshared() {
        return (await this.entityTable.getBooleanArray("byte:IsWorkshared"));
    }
    async getPathName(bimDocumentIndex) {
        return (await this.entityTable.getString(bimDocumentIndex, "string:PathName"));
    }
    async getAllPathName() {
        return (await this.entityTable.getStringArray("string:PathName"));
    }
    async getLatitude(bimDocumentIndex) {
        return (await this.entityTable.getNumber(bimDocumentIndex, "double:Latitude"));
    }
    async getAllLatitude() {
        return (await this.entityTable.getNumberArray("double:Latitude"));
    }
    async getLongitude(bimDocumentIndex) {
        return (await this.entityTable.getNumber(bimDocumentIndex, "double:Longitude"));
    }
    async getAllLongitude() {
        return (await this.entityTable.getNumberArray("double:Longitude"));
    }
    async getTimeZone(bimDocumentIndex) {
        return (await this.entityTable.getNumber(bimDocumentIndex, "double:TimeZone"));
    }
    async getAllTimeZone() {
        return (await this.entityTable.getNumberArray("double:TimeZone"));
    }
    async getPlaceName(bimDocumentIndex) {
        return (await this.entityTable.getString(bimDocumentIndex, "string:PlaceName"));
    }
    async getAllPlaceName() {
        return (await this.entityTable.getStringArray("string:PlaceName"));
    }
    async getWeatherStationName(bimDocumentIndex) {
        return (await this.entityTable.getString(bimDocumentIndex, "string:WeatherStationName"));
    }
    async getAllWeatherStationName() {
        return (await this.entityTable.getStringArray("string:WeatherStationName"));
    }
    async getElevation(bimDocumentIndex) {
        return (await this.entityTable.getNumber(bimDocumentIndex, "double:Elevation"));
    }
    async getAllElevation() {
        return (await this.entityTable.getNumberArray("double:Elevation"));
    }
    async getProjectLocation(bimDocumentIndex) {
        return (await this.entityTable.getString(bimDocumentIndex, "string:ProjectLocation"));
    }
    async getAllProjectLocation() {
        return (await this.entityTable.getStringArray("string:ProjectLocation"));
    }
    async getIssueDate(bimDocumentIndex) {
        return (await this.entityTable.getString(bimDocumentIndex, "string:IssueDate"));
    }
    async getAllIssueDate() {
        return (await this.entityTable.getStringArray("string:IssueDate"));
    }
    async getStatus(bimDocumentIndex) {
        return (await this.entityTable.getString(bimDocumentIndex, "string:Status"));
    }
    async getAllStatus() {
        return (await this.entityTable.getStringArray("string:Status"));
    }
    async getClientName(bimDocumentIndex) {
        return (await this.entityTable.getString(bimDocumentIndex, "string:ClientName"));
    }
    async getAllClientName() {
        return (await this.entityTable.getStringArray("string:ClientName"));
    }
    async getAddress(bimDocumentIndex) {
        return (await this.entityTable.getString(bimDocumentIndex, "string:Address"));
    }
    async getAllAddress() {
        return (await this.entityTable.getStringArray("string:Address"));
    }
    async getName(bimDocumentIndex) {
        return (await this.entityTable.getString(bimDocumentIndex, "string:Name"));
    }
    async getAllName() {
        return (await this.entityTable.getStringArray("string:Name"));
    }
    async getNumber(bimDocumentIndex) {
        return (await this.entityTable.getString(bimDocumentIndex, "string:Number"));
    }
    async getAllNumber() {
        return (await this.entityTable.getStringArray("string:Number"));
    }
    async getAuthor(bimDocumentIndex) {
        return (await this.entityTable.getString(bimDocumentIndex, "string:Author"));
    }
    async getAllAuthor() {
        return (await this.entityTable.getStringArray("string:Author"));
    }
    async getBuildingName(bimDocumentIndex) {
        return (await this.entityTable.getString(bimDocumentIndex, "string:BuildingName"));
    }
    async getAllBuildingName() {
        return (await this.entityTable.getStringArray("string:BuildingName"));
    }
    async getOrganizationName(bimDocumentIndex) {
        return (await this.entityTable.getString(bimDocumentIndex, "string:OrganizationName"));
    }
    async getAllOrganizationName() {
        return (await this.entityTable.getStringArray("string:OrganizationName"));
    }
    async getOrganizationDescription(bimDocumentIndex) {
        return (await this.entityTable.getString(bimDocumentIndex, "string:OrganizationDescription"));
    }
    async getAllOrganizationDescription() {
        return (await this.entityTable.getStringArray("string:OrganizationDescription"));
    }
    async getProduct(bimDocumentIndex) {
        return (await this.entityTable.getString(bimDocumentIndex, "string:Product"));
    }
    async getAllProduct() {
        return (await this.entityTable.getStringArray("string:Product"));
    }
    async getVersion(bimDocumentIndex) {
        return (await this.entityTable.getString(bimDocumentIndex, "string:Version"));
    }
    async getAllVersion() {
        return (await this.entityTable.getStringArray("string:Version"));
    }
    async getUser(bimDocumentIndex) {
        return (await this.entityTable.getString(bimDocumentIndex, "string:User"));
    }
    async getAllUser() {
        return (await this.entityTable.getStringArray("string:User"));
    }
    async getActiveViewIndex(bimDocumentIndex) {
        return await this.entityTable.getNumber(bimDocumentIndex, "index:Vim.View:ActiveView");
    }
    async getAllActiveViewIndex() {
        return await this.entityTable.getNumberArray("index:Vim.View:ActiveView");
    }
    async getActiveView(bimDocumentIndex) {
        const index = await this.getActiveViewIndex(bimDocumentIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.view?.get(index);
    }
    async getOwnerFamilyIndex(bimDocumentIndex) {
        return await this.entityTable.getNumber(bimDocumentIndex, "index:Vim.Family:OwnerFamily");
    }
    async getAllOwnerFamilyIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Family:OwnerFamily");
    }
    async getOwnerFamily(bimDocumentIndex) {
        const index = await this.getOwnerFamilyIndex(bimDocumentIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.family?.get(index);
    }
    async getParentIndex(bimDocumentIndex) {
        return await this.entityTable.getNumber(bimDocumentIndex, "index:Vim.BimDocument:Parent");
    }
    async getAllParentIndex() {
        return await this.entityTable.getNumberArray("index:Vim.BimDocument:Parent");
    }
    async getParent(bimDocumentIndex) {
        const index = await this.getParentIndex(bimDocumentIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.bimDocument?.get(index);
    }
    async getElementIndex(bimDocumentIndex) {
        return await this.entityTable.getNumber(bimDocumentIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(bimDocumentIndex) {
        const index = await this.getElementIndex(bimDocumentIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.BimDocumentTable = BimDocumentTable;
class DisplayUnitInBimDocument {
    static async createFromTable(table, index) {
        let result = new DisplayUnitInBimDocument();
        result.index = index;
        await Promise.all([
            table.getDisplayUnitIndex(index).then(v => result.displayUnitIndex = v),
            table.getBimDocumentIndex(index).then(v => result.bimDocumentIndex = v),
        ]);
        return result;
    }
}
exports.DisplayUnitInBimDocument = DisplayUnitInBimDocument;
class DisplayUnitInBimDocumentTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.DisplayUnitInBimDocument");
        if (!entity) {
            return undefined;
        }
        let table = new DisplayUnitInBimDocumentTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(displayUnitInBimDocumentIndex) {
        return await DisplayUnitInBimDocument.createFromTable(this, displayUnitInBimDocumentIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let displayUnitIndex;
        let bimDocumentIndex;
        await Promise.all([
            (async () => { displayUnitIndex = (await localTable.getNumberArray("index:Vim.DisplayUnit:DisplayUnit")); })(),
            (async () => { bimDocumentIndex = (await localTable.getNumberArray("index:Vim.BimDocument:BimDocument")); })(),
        ]);
        let displayUnitInBimDocument = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            displayUnitInBimDocument.push({
                index: i,
                displayUnitIndex: displayUnitIndex ? displayUnitIndex[i] : undefined,
                bimDocumentIndex: bimDocumentIndex ? bimDocumentIndex[i] : undefined
            });
        }
        return displayUnitInBimDocument;
    }
    async getDisplayUnitIndex(displayUnitInBimDocumentIndex) {
        return await this.entityTable.getNumber(displayUnitInBimDocumentIndex, "index:Vim.DisplayUnit:DisplayUnit");
    }
    async getAllDisplayUnitIndex() {
        return await this.entityTable.getNumberArray("index:Vim.DisplayUnit:DisplayUnit");
    }
    async getDisplayUnit(displayUnitInBimDocumentIndex) {
        const index = await this.getDisplayUnitIndex(displayUnitInBimDocumentIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.displayUnit?.get(index);
    }
    async getBimDocumentIndex(displayUnitInBimDocumentIndex) {
        return await this.entityTable.getNumber(displayUnitInBimDocumentIndex, "index:Vim.BimDocument:BimDocument");
    }
    async getAllBimDocumentIndex() {
        return await this.entityTable.getNumberArray("index:Vim.BimDocument:BimDocument");
    }
    async getBimDocument(displayUnitInBimDocumentIndex) {
        const index = await this.getBimDocumentIndex(displayUnitInBimDocumentIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.bimDocument?.get(index);
    }
}
exports.DisplayUnitInBimDocumentTable = DisplayUnitInBimDocumentTable;
class PhaseOrderInBimDocument {
    static async createFromTable(table, index) {
        let result = new PhaseOrderInBimDocument();
        result.index = index;
        await Promise.all([
            table.getOrderIndex(index).then(v => result.orderIndex = v),
            table.getPhaseIndex(index).then(v => result.phaseIndex = v),
            table.getBimDocumentIndex(index).then(v => result.bimDocumentIndex = v),
        ]);
        return result;
    }
}
exports.PhaseOrderInBimDocument = PhaseOrderInBimDocument;
class PhaseOrderInBimDocumentTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.PhaseOrderInBimDocument");
        if (!entity) {
            return undefined;
        }
        let table = new PhaseOrderInBimDocumentTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(phaseOrderInBimDocumentIndex) {
        return await PhaseOrderInBimDocument.createFromTable(this, phaseOrderInBimDocumentIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let orderIndex;
        let phaseIndex;
        let bimDocumentIndex;
        await Promise.all([
            (async () => { orderIndex = (await localTable.getNumberArray("int:OrderIndex")); })(),
            (async () => { phaseIndex = (await localTable.getNumberArray("index:Vim.Phase:Phase")); })(),
            (async () => { bimDocumentIndex = (await localTable.getNumberArray("index:Vim.BimDocument:BimDocument")); })(),
        ]);
        let phaseOrderInBimDocument = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            phaseOrderInBimDocument.push({
                index: i,
                orderIndex: orderIndex ? orderIndex[i] : undefined,
                phaseIndex: phaseIndex ? phaseIndex[i] : undefined,
                bimDocumentIndex: bimDocumentIndex ? bimDocumentIndex[i] : undefined
            });
        }
        return phaseOrderInBimDocument;
    }
    async getOrderIndex(phaseOrderInBimDocumentIndex) {
        return (await this.entityTable.getNumber(phaseOrderInBimDocumentIndex, "int:OrderIndex"));
    }
    async getAllOrderIndex() {
        return (await this.entityTable.getNumberArray("int:OrderIndex"));
    }
    async getPhaseIndex(phaseOrderInBimDocumentIndex) {
        return await this.entityTable.getNumber(phaseOrderInBimDocumentIndex, "index:Vim.Phase:Phase");
    }
    async getAllPhaseIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Phase:Phase");
    }
    async getPhase(phaseOrderInBimDocumentIndex) {
        const index = await this.getPhaseIndex(phaseOrderInBimDocumentIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.phase?.get(index);
    }
    async getBimDocumentIndex(phaseOrderInBimDocumentIndex) {
        return await this.entityTable.getNumber(phaseOrderInBimDocumentIndex, "index:Vim.BimDocument:BimDocument");
    }
    async getAllBimDocumentIndex() {
        return await this.entityTable.getNumberArray("index:Vim.BimDocument:BimDocument");
    }
    async getBimDocument(phaseOrderInBimDocumentIndex) {
        const index = await this.getBimDocumentIndex(phaseOrderInBimDocumentIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.bimDocument?.get(index);
    }
}
exports.PhaseOrderInBimDocumentTable = PhaseOrderInBimDocumentTable;
class Category {
    static async createFromTable(table, index) {
        let result = new Category();
        result.index = index;
        await Promise.all([
            table.getName(index).then(v => result.name = v),
            table.getId(index).then(v => result.id = v),
            table.getCategoryType(index).then(v => result.categoryType = v),
            table.getLineColor_X(index).then(v => result.lineColor_X = v),
            table.getLineColor_Y(index).then(v => result.lineColor_Y = v),
            table.getLineColor_Z(index).then(v => result.lineColor_Z = v),
            table.getBuiltInCategory(index).then(v => result.builtInCategory = v),
            table.getParentIndex(index).then(v => result.parentIndex = v),
            table.getMaterialIndex(index).then(v => result.materialIndex = v),
        ]);
        return result;
    }
}
exports.Category = Category;
class CategoryTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.Category");
        if (!entity) {
            return undefined;
        }
        let table = new CategoryTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(categoryIndex) {
        return await Category.createFromTable(this, categoryIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let name;
        let id;
        let categoryType;
        let lineColor_X;
        let lineColor_Y;
        let lineColor_Z;
        let builtInCategory;
        let parentIndex;
        let materialIndex;
        await Promise.all([
            (async () => { name = (await localTable.getStringArray("string:Name")); })(),
            (async () => { id = (await localTable.getBigIntArray("long:Id")) ?? (await localTable.getBigIntArray("int:Id")); })(),
            (async () => { categoryType = (await localTable.getStringArray("string:CategoryType")); })(),
            (async () => { lineColor_X = (await localTable.getNumberArray("double:LineColor.X")); })(),
            (async () => { lineColor_Y = (await localTable.getNumberArray("double:LineColor.Y")); })(),
            (async () => { lineColor_Z = (await localTable.getNumberArray("double:LineColor.Z")); })(),
            (async () => { builtInCategory = (await localTable.getStringArray("string:BuiltInCategory")); })(),
            (async () => { parentIndex = (await localTable.getNumberArray("index:Vim.Category:Parent")); })(),
            (async () => { materialIndex = (await localTable.getNumberArray("index:Vim.Material:Material")); })(),
        ]);
        let category = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            category.push({
                index: i,
                name: name ? name[i] : undefined,
                id: id ? id[i] : undefined,
                categoryType: categoryType ? categoryType[i] : undefined,
                lineColor_X: lineColor_X ? lineColor_X[i] : undefined,
                lineColor_Y: lineColor_Y ? lineColor_Y[i] : undefined,
                lineColor_Z: lineColor_Z ? lineColor_Z[i] : undefined,
                builtInCategory: builtInCategory ? builtInCategory[i] : undefined,
                parentIndex: parentIndex ? parentIndex[i] : undefined,
                materialIndex: materialIndex ? materialIndex[i] : undefined
            });
        }
        return category;
    }
    async getName(categoryIndex) {
        return (await this.entityTable.getString(categoryIndex, "string:Name"));
    }
    async getAllName() {
        return (await this.entityTable.getStringArray("string:Name"));
    }
    async getId(categoryIndex) {
        return (await this.entityTable.getBigInt(categoryIndex, "long:Id")) ?? (await this.entityTable.getBigInt(categoryIndex, "int:Id"));
    }
    async getAllId() {
        return (await this.entityTable.getBigIntArray("long:Id")) ?? (await this.entityTable.getBigIntArray("int:Id"));
    }
    async getCategoryType(categoryIndex) {
        return (await this.entityTable.getString(categoryIndex, "string:CategoryType"));
    }
    async getAllCategoryType() {
        return (await this.entityTable.getStringArray("string:CategoryType"));
    }
    async getLineColor_X(categoryIndex) {
        return (await this.entityTable.getNumber(categoryIndex, "double:LineColor.X"));
    }
    async getAllLineColor_X() {
        return (await this.entityTable.getNumberArray("double:LineColor.X"));
    }
    async getLineColor_Y(categoryIndex) {
        return (await this.entityTable.getNumber(categoryIndex, "double:LineColor.Y"));
    }
    async getAllLineColor_Y() {
        return (await this.entityTable.getNumberArray("double:LineColor.Y"));
    }
    async getLineColor_Z(categoryIndex) {
        return (await this.entityTable.getNumber(categoryIndex, "double:LineColor.Z"));
    }
    async getAllLineColor_Z() {
        return (await this.entityTable.getNumberArray("double:LineColor.Z"));
    }
    async getBuiltInCategory(categoryIndex) {
        return (await this.entityTable.getString(categoryIndex, "string:BuiltInCategory"));
    }
    async getAllBuiltInCategory() {
        return (await this.entityTable.getStringArray("string:BuiltInCategory"));
    }
    async getParentIndex(categoryIndex) {
        return await this.entityTable.getNumber(categoryIndex, "index:Vim.Category:Parent");
    }
    async getAllParentIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Category:Parent");
    }
    async getParent(categoryIndex) {
        const index = await this.getParentIndex(categoryIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.category?.get(index);
    }
    async getMaterialIndex(categoryIndex) {
        return await this.entityTable.getNumber(categoryIndex, "index:Vim.Material:Material");
    }
    async getAllMaterialIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Material:Material");
    }
    async getMaterial(categoryIndex) {
        const index = await this.getMaterialIndex(categoryIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.material?.get(index);
    }
}
exports.CategoryTable = CategoryTable;
class Family {
    static async createFromTable(table, index) {
        let result = new Family();
        result.index = index;
        await Promise.all([
            table.getStructuralMaterialType(index).then(v => result.structuralMaterialType = v),
            table.getStructuralSectionShape(index).then(v => result.structuralSectionShape = v),
            table.getIsSystemFamily(index).then(v => result.isSystemFamily = v),
            table.getIsInPlace(index).then(v => result.isInPlace = v),
            table.getFamilyCategoryIndex(index).then(v => result.familyCategoryIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.Family = Family;
class FamilyTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.Family");
        if (!entity) {
            return undefined;
        }
        let table = new FamilyTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(familyIndex) {
        return await Family.createFromTable(this, familyIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let structuralMaterialType;
        let structuralSectionShape;
        let isSystemFamily;
        let isInPlace;
        let familyCategoryIndex;
        let elementIndex;
        await Promise.all([
            (async () => { structuralMaterialType = (await localTable.getStringArray("string:StructuralMaterialType")); })(),
            (async () => { structuralSectionShape = (await localTable.getStringArray("string:StructuralSectionShape")); })(),
            (async () => { isSystemFamily = (await localTable.getBooleanArray("byte:IsSystemFamily")); })(),
            (async () => { isInPlace = (await localTable.getBooleanArray("byte:IsInPlace")); })(),
            (async () => { familyCategoryIndex = (await localTable.getNumberArray("index:Vim.Category:FamilyCategory")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let family = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            family.push({
                index: i,
                structuralMaterialType: structuralMaterialType ? structuralMaterialType[i] : undefined,
                structuralSectionShape: structuralSectionShape ? structuralSectionShape[i] : undefined,
                isSystemFamily: isSystemFamily ? isSystemFamily[i] : undefined,
                isInPlace: isInPlace ? isInPlace[i] : undefined,
                familyCategoryIndex: familyCategoryIndex ? familyCategoryIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return family;
    }
    async getStructuralMaterialType(familyIndex) {
        return (await this.entityTable.getString(familyIndex, "string:StructuralMaterialType"));
    }
    async getAllStructuralMaterialType() {
        return (await this.entityTable.getStringArray("string:StructuralMaterialType"));
    }
    async getStructuralSectionShape(familyIndex) {
        return (await this.entityTable.getString(familyIndex, "string:StructuralSectionShape"));
    }
    async getAllStructuralSectionShape() {
        return (await this.entityTable.getStringArray("string:StructuralSectionShape"));
    }
    async getIsSystemFamily(familyIndex) {
        return (await this.entityTable.getBoolean(familyIndex, "byte:IsSystemFamily"));
    }
    async getAllIsSystemFamily() {
        return (await this.entityTable.getBooleanArray("byte:IsSystemFamily"));
    }
    async getIsInPlace(familyIndex) {
        return (await this.entityTable.getBoolean(familyIndex, "byte:IsInPlace"));
    }
    async getAllIsInPlace() {
        return (await this.entityTable.getBooleanArray("byte:IsInPlace"));
    }
    async getFamilyCategoryIndex(familyIndex) {
        return await this.entityTable.getNumber(familyIndex, "index:Vim.Category:FamilyCategory");
    }
    async getAllFamilyCategoryIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Category:FamilyCategory");
    }
    async getFamilyCategory(familyIndex) {
        const index = await this.getFamilyCategoryIndex(familyIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.category?.get(index);
    }
    async getElementIndex(familyIndex) {
        return await this.entityTable.getNumber(familyIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(familyIndex) {
        const index = await this.getElementIndex(familyIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.FamilyTable = FamilyTable;
class FamilyType {
    static async createFromTable(table, index) {
        let result = new FamilyType();
        result.index = index;
        await Promise.all([
            table.getIsSystemFamilyType(index).then(v => result.isSystemFamilyType = v),
            table.getFamilyIndex(index).then(v => result.familyIndex = v),
            table.getCompoundStructureIndex(index).then(v => result.compoundStructureIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.FamilyType = FamilyType;
class FamilyTypeTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.FamilyType");
        if (!entity) {
            return undefined;
        }
        let table = new FamilyTypeTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(familyTypeIndex) {
        return await FamilyType.createFromTable(this, familyTypeIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let isSystemFamilyType;
        let familyIndex;
        let compoundStructureIndex;
        let elementIndex;
        await Promise.all([
            (async () => { isSystemFamilyType = (await localTable.getBooleanArray("byte:IsSystemFamilyType")); })(),
            (async () => { familyIndex = (await localTable.getNumberArray("index:Vim.Family:Family")); })(),
            (async () => { compoundStructureIndex = (await localTable.getNumberArray("index:Vim.CompoundStructure:CompoundStructure")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let familyType = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            familyType.push({
                index: i,
                isSystemFamilyType: isSystemFamilyType ? isSystemFamilyType[i] : undefined,
                familyIndex: familyIndex ? familyIndex[i] : undefined,
                compoundStructureIndex: compoundStructureIndex ? compoundStructureIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return familyType;
    }
    async getIsSystemFamilyType(familyTypeIndex) {
        return (await this.entityTable.getBoolean(familyTypeIndex, "byte:IsSystemFamilyType"));
    }
    async getAllIsSystemFamilyType() {
        return (await this.entityTable.getBooleanArray("byte:IsSystemFamilyType"));
    }
    async getFamilyIndex(familyTypeIndex) {
        return await this.entityTable.getNumber(familyTypeIndex, "index:Vim.Family:Family");
    }
    async getAllFamilyIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Family:Family");
    }
    async getFamily(familyTypeIndex) {
        const index = await this.getFamilyIndex(familyTypeIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.family?.get(index);
    }
    async getCompoundStructureIndex(familyTypeIndex) {
        return await this.entityTable.getNumber(familyTypeIndex, "index:Vim.CompoundStructure:CompoundStructure");
    }
    async getAllCompoundStructureIndex() {
        return await this.entityTable.getNumberArray("index:Vim.CompoundStructure:CompoundStructure");
    }
    async getCompoundStructure(familyTypeIndex) {
        const index = await this.getCompoundStructureIndex(familyTypeIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.compoundStructure?.get(index);
    }
    async getElementIndex(familyTypeIndex) {
        return await this.entityTable.getNumber(familyTypeIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(familyTypeIndex) {
        const index = await this.getElementIndex(familyTypeIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.FamilyTypeTable = FamilyTypeTable;
class FamilyInstance {
    static async createFromTable(table, index) {
        let result = new FamilyInstance();
        result.index = index;
        await Promise.all([
            table.getFacingFlipped(index).then(v => result.facingFlipped = v),
            table.getFacingOrientation_X(index).then(v => result.facingOrientation_X = v),
            table.getFacingOrientation_Y(index).then(v => result.facingOrientation_Y = v),
            table.getFacingOrientation_Z(index).then(v => result.facingOrientation_Z = v),
            table.getHandFlipped(index).then(v => result.handFlipped = v),
            table.getMirrored(index).then(v => result.mirrored = v),
            table.getHasModifiedGeometry(index).then(v => result.hasModifiedGeometry = v),
            table.getScale(index).then(v => result.scale = v),
            table.getBasisX_X(index).then(v => result.basisX_X = v),
            table.getBasisX_Y(index).then(v => result.basisX_Y = v),
            table.getBasisX_Z(index).then(v => result.basisX_Z = v),
            table.getBasisY_X(index).then(v => result.basisY_X = v),
            table.getBasisY_Y(index).then(v => result.basisY_Y = v),
            table.getBasisY_Z(index).then(v => result.basisY_Z = v),
            table.getBasisZ_X(index).then(v => result.basisZ_X = v),
            table.getBasisZ_Y(index).then(v => result.basisZ_Y = v),
            table.getBasisZ_Z(index).then(v => result.basisZ_Z = v),
            table.getTranslation_X(index).then(v => result.translation_X = v),
            table.getTranslation_Y(index).then(v => result.translation_Y = v),
            table.getTranslation_Z(index).then(v => result.translation_Z = v),
            table.getHandOrientation_X(index).then(v => result.handOrientation_X = v),
            table.getHandOrientation_Y(index).then(v => result.handOrientation_Y = v),
            table.getHandOrientation_Z(index).then(v => result.handOrientation_Z = v),
            table.getFamilyTypeIndex(index).then(v => result.familyTypeIndex = v),
            table.getHostIndex(index).then(v => result.hostIndex = v),
            table.getFromRoomIndex(index).then(v => result.fromRoomIndex = v),
            table.getToRoomIndex(index).then(v => result.toRoomIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.FamilyInstance = FamilyInstance;
class FamilyInstanceTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.FamilyInstance");
        if (!entity) {
            return undefined;
        }
        let table = new FamilyInstanceTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(familyInstanceIndex) {
        return await FamilyInstance.createFromTable(this, familyInstanceIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let facingFlipped;
        let facingOrientation_X;
        let facingOrientation_Y;
        let facingOrientation_Z;
        let handFlipped;
        let mirrored;
        let hasModifiedGeometry;
        let scale;
        let basisX_X;
        let basisX_Y;
        let basisX_Z;
        let basisY_X;
        let basisY_Y;
        let basisY_Z;
        let basisZ_X;
        let basisZ_Y;
        let basisZ_Z;
        let translation_X;
        let translation_Y;
        let translation_Z;
        let handOrientation_X;
        let handOrientation_Y;
        let handOrientation_Z;
        let familyTypeIndex;
        let hostIndex;
        let fromRoomIndex;
        let toRoomIndex;
        let elementIndex;
        await Promise.all([
            (async () => { facingFlipped = (await localTable.getBooleanArray("byte:FacingFlipped")); })(),
            (async () => { facingOrientation_X = (await localTable.getNumberArray("float:FacingOrientation.X")); })(),
            (async () => { facingOrientation_Y = (await localTable.getNumberArray("float:FacingOrientation.Y")); })(),
            (async () => { facingOrientation_Z = (await localTable.getNumberArray("float:FacingOrientation.Z")); })(),
            (async () => { handFlipped = (await localTable.getBooleanArray("byte:HandFlipped")); })(),
            (async () => { mirrored = (await localTable.getBooleanArray("byte:Mirrored")); })(),
            (async () => { hasModifiedGeometry = (await localTable.getBooleanArray("byte:HasModifiedGeometry")); })(),
            (async () => { scale = (await localTable.getNumberArray("float:Scale")); })(),
            (async () => { basisX_X = (await localTable.getNumberArray("float:BasisX.X")); })(),
            (async () => { basisX_Y = (await localTable.getNumberArray("float:BasisX.Y")); })(),
            (async () => { basisX_Z = (await localTable.getNumberArray("float:BasisX.Z")); })(),
            (async () => { basisY_X = (await localTable.getNumberArray("float:BasisY.X")); })(),
            (async () => { basisY_Y = (await localTable.getNumberArray("float:BasisY.Y")); })(),
            (async () => { basisY_Z = (await localTable.getNumberArray("float:BasisY.Z")); })(),
            (async () => { basisZ_X = (await localTable.getNumberArray("float:BasisZ.X")); })(),
            (async () => { basisZ_Y = (await localTable.getNumberArray("float:BasisZ.Y")); })(),
            (async () => { basisZ_Z = (await localTable.getNumberArray("float:BasisZ.Z")); })(),
            (async () => { translation_X = (await localTable.getNumberArray("float:Translation.X")); })(),
            (async () => { translation_Y = (await localTable.getNumberArray("float:Translation.Y")); })(),
            (async () => { translation_Z = (await localTable.getNumberArray("float:Translation.Z")); })(),
            (async () => { handOrientation_X = (await localTable.getNumberArray("float:HandOrientation.X")); })(),
            (async () => { handOrientation_Y = (await localTable.getNumberArray("float:HandOrientation.Y")); })(),
            (async () => { handOrientation_Z = (await localTable.getNumberArray("float:HandOrientation.Z")); })(),
            (async () => { familyTypeIndex = (await localTable.getNumberArray("index:Vim.FamilyType:FamilyType")); })(),
            (async () => { hostIndex = (await localTable.getNumberArray("index:Vim.Element:Host")); })(),
            (async () => { fromRoomIndex = (await localTable.getNumberArray("index:Vim.Room:FromRoom")); })(),
            (async () => { toRoomIndex = (await localTable.getNumberArray("index:Vim.Room:ToRoom")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let familyInstance = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            familyInstance.push({
                index: i,
                facingFlipped: facingFlipped ? facingFlipped[i] : undefined,
                facingOrientation_X: facingOrientation_X ? facingOrientation_X[i] : undefined,
                facingOrientation_Y: facingOrientation_Y ? facingOrientation_Y[i] : undefined,
                facingOrientation_Z: facingOrientation_Z ? facingOrientation_Z[i] : undefined,
                handFlipped: handFlipped ? handFlipped[i] : undefined,
                mirrored: mirrored ? mirrored[i] : undefined,
                hasModifiedGeometry: hasModifiedGeometry ? hasModifiedGeometry[i] : undefined,
                scale: scale ? scale[i] : undefined,
                basisX_X: basisX_X ? basisX_X[i] : undefined,
                basisX_Y: basisX_Y ? basisX_Y[i] : undefined,
                basisX_Z: basisX_Z ? basisX_Z[i] : undefined,
                basisY_X: basisY_X ? basisY_X[i] : undefined,
                basisY_Y: basisY_Y ? basisY_Y[i] : undefined,
                basisY_Z: basisY_Z ? basisY_Z[i] : undefined,
                basisZ_X: basisZ_X ? basisZ_X[i] : undefined,
                basisZ_Y: basisZ_Y ? basisZ_Y[i] : undefined,
                basisZ_Z: basisZ_Z ? basisZ_Z[i] : undefined,
                translation_X: translation_X ? translation_X[i] : undefined,
                translation_Y: translation_Y ? translation_Y[i] : undefined,
                translation_Z: translation_Z ? translation_Z[i] : undefined,
                handOrientation_X: handOrientation_X ? handOrientation_X[i] : undefined,
                handOrientation_Y: handOrientation_Y ? handOrientation_Y[i] : undefined,
                handOrientation_Z: handOrientation_Z ? handOrientation_Z[i] : undefined,
                familyTypeIndex: familyTypeIndex ? familyTypeIndex[i] : undefined,
                hostIndex: hostIndex ? hostIndex[i] : undefined,
                fromRoomIndex: fromRoomIndex ? fromRoomIndex[i] : undefined,
                toRoomIndex: toRoomIndex ? toRoomIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return familyInstance;
    }
    async getFacingFlipped(familyInstanceIndex) {
        return (await this.entityTable.getBoolean(familyInstanceIndex, "byte:FacingFlipped"));
    }
    async getAllFacingFlipped() {
        return (await this.entityTable.getBooleanArray("byte:FacingFlipped"));
    }
    async getFacingOrientation_X(familyInstanceIndex) {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:FacingOrientation.X"));
    }
    async getAllFacingOrientation_X() {
        return (await this.entityTable.getNumberArray("float:FacingOrientation.X"));
    }
    async getFacingOrientation_Y(familyInstanceIndex) {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:FacingOrientation.Y"));
    }
    async getAllFacingOrientation_Y() {
        return (await this.entityTable.getNumberArray("float:FacingOrientation.Y"));
    }
    async getFacingOrientation_Z(familyInstanceIndex) {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:FacingOrientation.Z"));
    }
    async getAllFacingOrientation_Z() {
        return (await this.entityTable.getNumberArray("float:FacingOrientation.Z"));
    }
    async getHandFlipped(familyInstanceIndex) {
        return (await this.entityTable.getBoolean(familyInstanceIndex, "byte:HandFlipped"));
    }
    async getAllHandFlipped() {
        return (await this.entityTable.getBooleanArray("byte:HandFlipped"));
    }
    async getMirrored(familyInstanceIndex) {
        return (await this.entityTable.getBoolean(familyInstanceIndex, "byte:Mirrored"));
    }
    async getAllMirrored() {
        return (await this.entityTable.getBooleanArray("byte:Mirrored"));
    }
    async getHasModifiedGeometry(familyInstanceIndex) {
        return (await this.entityTable.getBoolean(familyInstanceIndex, "byte:HasModifiedGeometry"));
    }
    async getAllHasModifiedGeometry() {
        return (await this.entityTable.getBooleanArray("byte:HasModifiedGeometry"));
    }
    async getScale(familyInstanceIndex) {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:Scale"));
    }
    async getAllScale() {
        return (await this.entityTable.getNumberArray("float:Scale"));
    }
    async getBasisX_X(familyInstanceIndex) {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:BasisX.X"));
    }
    async getAllBasisX_X() {
        return (await this.entityTable.getNumberArray("float:BasisX.X"));
    }
    async getBasisX_Y(familyInstanceIndex) {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:BasisX.Y"));
    }
    async getAllBasisX_Y() {
        return (await this.entityTable.getNumberArray("float:BasisX.Y"));
    }
    async getBasisX_Z(familyInstanceIndex) {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:BasisX.Z"));
    }
    async getAllBasisX_Z() {
        return (await this.entityTable.getNumberArray("float:BasisX.Z"));
    }
    async getBasisY_X(familyInstanceIndex) {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:BasisY.X"));
    }
    async getAllBasisY_X() {
        return (await this.entityTable.getNumberArray("float:BasisY.X"));
    }
    async getBasisY_Y(familyInstanceIndex) {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:BasisY.Y"));
    }
    async getAllBasisY_Y() {
        return (await this.entityTable.getNumberArray("float:BasisY.Y"));
    }
    async getBasisY_Z(familyInstanceIndex) {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:BasisY.Z"));
    }
    async getAllBasisY_Z() {
        return (await this.entityTable.getNumberArray("float:BasisY.Z"));
    }
    async getBasisZ_X(familyInstanceIndex) {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:BasisZ.X"));
    }
    async getAllBasisZ_X() {
        return (await this.entityTable.getNumberArray("float:BasisZ.X"));
    }
    async getBasisZ_Y(familyInstanceIndex) {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:BasisZ.Y"));
    }
    async getAllBasisZ_Y() {
        return (await this.entityTable.getNumberArray("float:BasisZ.Y"));
    }
    async getBasisZ_Z(familyInstanceIndex) {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:BasisZ.Z"));
    }
    async getAllBasisZ_Z() {
        return (await this.entityTable.getNumberArray("float:BasisZ.Z"));
    }
    async getTranslation_X(familyInstanceIndex) {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:Translation.X"));
    }
    async getAllTranslation_X() {
        return (await this.entityTable.getNumberArray("float:Translation.X"));
    }
    async getTranslation_Y(familyInstanceIndex) {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:Translation.Y"));
    }
    async getAllTranslation_Y() {
        return (await this.entityTable.getNumberArray("float:Translation.Y"));
    }
    async getTranslation_Z(familyInstanceIndex) {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:Translation.Z"));
    }
    async getAllTranslation_Z() {
        return (await this.entityTable.getNumberArray("float:Translation.Z"));
    }
    async getHandOrientation_X(familyInstanceIndex) {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:HandOrientation.X"));
    }
    async getAllHandOrientation_X() {
        return (await this.entityTable.getNumberArray("float:HandOrientation.X"));
    }
    async getHandOrientation_Y(familyInstanceIndex) {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:HandOrientation.Y"));
    }
    async getAllHandOrientation_Y() {
        return (await this.entityTable.getNumberArray("float:HandOrientation.Y"));
    }
    async getHandOrientation_Z(familyInstanceIndex) {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:HandOrientation.Z"));
    }
    async getAllHandOrientation_Z() {
        return (await this.entityTable.getNumberArray("float:HandOrientation.Z"));
    }
    async getFamilyTypeIndex(familyInstanceIndex) {
        return await this.entityTable.getNumber(familyInstanceIndex, "index:Vim.FamilyType:FamilyType");
    }
    async getAllFamilyTypeIndex() {
        return await this.entityTable.getNumberArray("index:Vim.FamilyType:FamilyType");
    }
    async getFamilyType(familyInstanceIndex) {
        const index = await this.getFamilyTypeIndex(familyInstanceIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.familyType?.get(index);
    }
    async getHostIndex(familyInstanceIndex) {
        return await this.entityTable.getNumber(familyInstanceIndex, "index:Vim.Element:Host");
    }
    async getAllHostIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Host");
    }
    async getHost(familyInstanceIndex) {
        const index = await this.getHostIndex(familyInstanceIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
    async getFromRoomIndex(familyInstanceIndex) {
        return await this.entityTable.getNumber(familyInstanceIndex, "index:Vim.Room:FromRoom");
    }
    async getAllFromRoomIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Room:FromRoom");
    }
    async getFromRoom(familyInstanceIndex) {
        const index = await this.getFromRoomIndex(familyInstanceIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.room?.get(index);
    }
    async getToRoomIndex(familyInstanceIndex) {
        return await this.entityTable.getNumber(familyInstanceIndex, "index:Vim.Room:ToRoom");
    }
    async getAllToRoomIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Room:ToRoom");
    }
    async getToRoom(familyInstanceIndex) {
        const index = await this.getToRoomIndex(familyInstanceIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.room?.get(index);
    }
    async getElementIndex(familyInstanceIndex) {
        return await this.entityTable.getNumber(familyInstanceIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(familyInstanceIndex) {
        const index = await this.getElementIndex(familyInstanceIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.FamilyInstanceTable = FamilyInstanceTable;
class View {
    static async createFromTable(table, index) {
        let result = new View();
        result.index = index;
        await Promise.all([
            table.getTitle(index).then(v => result.title = v),
            table.getViewType(index).then(v => result.viewType = v),
            table.getUp_X(index).then(v => result.up_X = v),
            table.getUp_Y(index).then(v => result.up_Y = v),
            table.getUp_Z(index).then(v => result.up_Z = v),
            table.getRight_X(index).then(v => result.right_X = v),
            table.getRight_Y(index).then(v => result.right_Y = v),
            table.getRight_Z(index).then(v => result.right_Z = v),
            table.getOrigin_X(index).then(v => result.origin_X = v),
            table.getOrigin_Y(index).then(v => result.origin_Y = v),
            table.getOrigin_Z(index).then(v => result.origin_Z = v),
            table.getViewDirection_X(index).then(v => result.viewDirection_X = v),
            table.getViewDirection_Y(index).then(v => result.viewDirection_Y = v),
            table.getViewDirection_Z(index).then(v => result.viewDirection_Z = v),
            table.getViewPosition_X(index).then(v => result.viewPosition_X = v),
            table.getViewPosition_Y(index).then(v => result.viewPosition_Y = v),
            table.getViewPosition_Z(index).then(v => result.viewPosition_Z = v),
            table.getScale(index).then(v => result.scale = v),
            table.getOutline_Min_X(index).then(v => result.outline_Min_X = v),
            table.getOutline_Min_Y(index).then(v => result.outline_Min_Y = v),
            table.getOutline_Max_X(index).then(v => result.outline_Max_X = v),
            table.getOutline_Max_Y(index).then(v => result.outline_Max_Y = v),
            table.getDetailLevel(index).then(v => result.detailLevel = v),
            table.getCameraIndex(index).then(v => result.cameraIndex = v),
            table.getFamilyTypeIndex(index).then(v => result.familyTypeIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.View = View;
class ViewTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.View");
        if (!entity) {
            return undefined;
        }
        let table = new ViewTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(viewIndex) {
        return await View.createFromTable(this, viewIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let title;
        let viewType;
        let up_X;
        let up_Y;
        let up_Z;
        let right_X;
        let right_Y;
        let right_Z;
        let origin_X;
        let origin_Y;
        let origin_Z;
        let viewDirection_X;
        let viewDirection_Y;
        let viewDirection_Z;
        let viewPosition_X;
        let viewPosition_Y;
        let viewPosition_Z;
        let scale;
        let outline_Min_X;
        let outline_Min_Y;
        let outline_Max_X;
        let outline_Max_Y;
        let detailLevel;
        let cameraIndex;
        let familyTypeIndex;
        let elementIndex;
        await Promise.all([
            (async () => { title = (await localTable.getStringArray("string:Title")); })(),
            (async () => { viewType = (await localTable.getStringArray("string:ViewType")); })(),
            (async () => { up_X = (await localTable.getNumberArray("double:Up.X")); })(),
            (async () => { up_Y = (await localTable.getNumberArray("double:Up.Y")); })(),
            (async () => { up_Z = (await localTable.getNumberArray("double:Up.Z")); })(),
            (async () => { right_X = (await localTable.getNumberArray("double:Right.X")); })(),
            (async () => { right_Y = (await localTable.getNumberArray("double:Right.Y")); })(),
            (async () => { right_Z = (await localTable.getNumberArray("double:Right.Z")); })(),
            (async () => { origin_X = (await localTable.getNumberArray("double:Origin.X")); })(),
            (async () => { origin_Y = (await localTable.getNumberArray("double:Origin.Y")); })(),
            (async () => { origin_Z = (await localTable.getNumberArray("double:Origin.Z")); })(),
            (async () => { viewDirection_X = (await localTable.getNumberArray("double:ViewDirection.X")); })(),
            (async () => { viewDirection_Y = (await localTable.getNumberArray("double:ViewDirection.Y")); })(),
            (async () => { viewDirection_Z = (await localTable.getNumberArray("double:ViewDirection.Z")); })(),
            (async () => { viewPosition_X = (await localTable.getNumberArray("double:ViewPosition.X")); })(),
            (async () => { viewPosition_Y = (await localTable.getNumberArray("double:ViewPosition.Y")); })(),
            (async () => { viewPosition_Z = (await localTable.getNumberArray("double:ViewPosition.Z")); })(),
            (async () => { scale = (await localTable.getNumberArray("double:Scale")); })(),
            (async () => { outline_Min_X = (await localTable.getNumberArray("double:Outline.Min.X")); })(),
            (async () => { outline_Min_Y = (await localTable.getNumberArray("double:Outline.Min.Y")); })(),
            (async () => { outline_Max_X = (await localTable.getNumberArray("double:Outline.Max.X")); })(),
            (async () => { outline_Max_Y = (await localTable.getNumberArray("double:Outline.Max.Y")); })(),
            (async () => { detailLevel = (await localTable.getNumberArray("int:DetailLevel")); })(),
            (async () => { cameraIndex = (await localTable.getNumberArray("index:Vim.Camera:Camera")); })(),
            (async () => { familyTypeIndex = (await localTable.getNumberArray("index:Vim.FamilyType:FamilyType")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let view = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            view.push({
                index: i,
                title: title ? title[i] : undefined,
                viewType: viewType ? viewType[i] : undefined,
                up_X: up_X ? up_X[i] : undefined,
                up_Y: up_Y ? up_Y[i] : undefined,
                up_Z: up_Z ? up_Z[i] : undefined,
                right_X: right_X ? right_X[i] : undefined,
                right_Y: right_Y ? right_Y[i] : undefined,
                right_Z: right_Z ? right_Z[i] : undefined,
                origin_X: origin_X ? origin_X[i] : undefined,
                origin_Y: origin_Y ? origin_Y[i] : undefined,
                origin_Z: origin_Z ? origin_Z[i] : undefined,
                viewDirection_X: viewDirection_X ? viewDirection_X[i] : undefined,
                viewDirection_Y: viewDirection_Y ? viewDirection_Y[i] : undefined,
                viewDirection_Z: viewDirection_Z ? viewDirection_Z[i] : undefined,
                viewPosition_X: viewPosition_X ? viewPosition_X[i] : undefined,
                viewPosition_Y: viewPosition_Y ? viewPosition_Y[i] : undefined,
                viewPosition_Z: viewPosition_Z ? viewPosition_Z[i] : undefined,
                scale: scale ? scale[i] : undefined,
                outline_Min_X: outline_Min_X ? outline_Min_X[i] : undefined,
                outline_Min_Y: outline_Min_Y ? outline_Min_Y[i] : undefined,
                outline_Max_X: outline_Max_X ? outline_Max_X[i] : undefined,
                outline_Max_Y: outline_Max_Y ? outline_Max_Y[i] : undefined,
                detailLevel: detailLevel ? detailLevel[i] : undefined,
                cameraIndex: cameraIndex ? cameraIndex[i] : undefined,
                familyTypeIndex: familyTypeIndex ? familyTypeIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return view;
    }
    async getTitle(viewIndex) {
        return (await this.entityTable.getString(viewIndex, "string:Title"));
    }
    async getAllTitle() {
        return (await this.entityTable.getStringArray("string:Title"));
    }
    async getViewType(viewIndex) {
        return (await this.entityTable.getString(viewIndex, "string:ViewType"));
    }
    async getAllViewType() {
        return (await this.entityTable.getStringArray("string:ViewType"));
    }
    async getUp_X(viewIndex) {
        return (await this.entityTable.getNumber(viewIndex, "double:Up.X"));
    }
    async getAllUp_X() {
        return (await this.entityTable.getNumberArray("double:Up.X"));
    }
    async getUp_Y(viewIndex) {
        return (await this.entityTable.getNumber(viewIndex, "double:Up.Y"));
    }
    async getAllUp_Y() {
        return (await this.entityTable.getNumberArray("double:Up.Y"));
    }
    async getUp_Z(viewIndex) {
        return (await this.entityTable.getNumber(viewIndex, "double:Up.Z"));
    }
    async getAllUp_Z() {
        return (await this.entityTable.getNumberArray("double:Up.Z"));
    }
    async getRight_X(viewIndex) {
        return (await this.entityTable.getNumber(viewIndex, "double:Right.X"));
    }
    async getAllRight_X() {
        return (await this.entityTable.getNumberArray("double:Right.X"));
    }
    async getRight_Y(viewIndex) {
        return (await this.entityTable.getNumber(viewIndex, "double:Right.Y"));
    }
    async getAllRight_Y() {
        return (await this.entityTable.getNumberArray("double:Right.Y"));
    }
    async getRight_Z(viewIndex) {
        return (await this.entityTable.getNumber(viewIndex, "double:Right.Z"));
    }
    async getAllRight_Z() {
        return (await this.entityTable.getNumberArray("double:Right.Z"));
    }
    async getOrigin_X(viewIndex) {
        return (await this.entityTable.getNumber(viewIndex, "double:Origin.X"));
    }
    async getAllOrigin_X() {
        return (await this.entityTable.getNumberArray("double:Origin.X"));
    }
    async getOrigin_Y(viewIndex) {
        return (await this.entityTable.getNumber(viewIndex, "double:Origin.Y"));
    }
    async getAllOrigin_Y() {
        return (await this.entityTable.getNumberArray("double:Origin.Y"));
    }
    async getOrigin_Z(viewIndex) {
        return (await this.entityTable.getNumber(viewIndex, "double:Origin.Z"));
    }
    async getAllOrigin_Z() {
        return (await this.entityTable.getNumberArray("double:Origin.Z"));
    }
    async getViewDirection_X(viewIndex) {
        return (await this.entityTable.getNumber(viewIndex, "double:ViewDirection.X"));
    }
    async getAllViewDirection_X() {
        return (await this.entityTable.getNumberArray("double:ViewDirection.X"));
    }
    async getViewDirection_Y(viewIndex) {
        return (await this.entityTable.getNumber(viewIndex, "double:ViewDirection.Y"));
    }
    async getAllViewDirection_Y() {
        return (await this.entityTable.getNumberArray("double:ViewDirection.Y"));
    }
    async getViewDirection_Z(viewIndex) {
        return (await this.entityTable.getNumber(viewIndex, "double:ViewDirection.Z"));
    }
    async getAllViewDirection_Z() {
        return (await this.entityTable.getNumberArray("double:ViewDirection.Z"));
    }
    async getViewPosition_X(viewIndex) {
        return (await this.entityTable.getNumber(viewIndex, "double:ViewPosition.X"));
    }
    async getAllViewPosition_X() {
        return (await this.entityTable.getNumberArray("double:ViewPosition.X"));
    }
    async getViewPosition_Y(viewIndex) {
        return (await this.entityTable.getNumber(viewIndex, "double:ViewPosition.Y"));
    }
    async getAllViewPosition_Y() {
        return (await this.entityTable.getNumberArray("double:ViewPosition.Y"));
    }
    async getViewPosition_Z(viewIndex) {
        return (await this.entityTable.getNumber(viewIndex, "double:ViewPosition.Z"));
    }
    async getAllViewPosition_Z() {
        return (await this.entityTable.getNumberArray("double:ViewPosition.Z"));
    }
    async getScale(viewIndex) {
        return (await this.entityTable.getNumber(viewIndex, "double:Scale"));
    }
    async getAllScale() {
        return (await this.entityTable.getNumberArray("double:Scale"));
    }
    async getOutline_Min_X(viewIndex) {
        return (await this.entityTable.getNumber(viewIndex, "double:Outline.Min.X"));
    }
    async getAllOutline_Min_X() {
        return (await this.entityTable.getNumberArray("double:Outline.Min.X"));
    }
    async getOutline_Min_Y(viewIndex) {
        return (await this.entityTable.getNumber(viewIndex, "double:Outline.Min.Y"));
    }
    async getAllOutline_Min_Y() {
        return (await this.entityTable.getNumberArray("double:Outline.Min.Y"));
    }
    async getOutline_Max_X(viewIndex) {
        return (await this.entityTable.getNumber(viewIndex, "double:Outline.Max.X"));
    }
    async getAllOutline_Max_X() {
        return (await this.entityTable.getNumberArray("double:Outline.Max.X"));
    }
    async getOutline_Max_Y(viewIndex) {
        return (await this.entityTable.getNumber(viewIndex, "double:Outline.Max.Y"));
    }
    async getAllOutline_Max_Y() {
        return (await this.entityTable.getNumberArray("double:Outline.Max.Y"));
    }
    async getDetailLevel(viewIndex) {
        return (await this.entityTable.getNumber(viewIndex, "int:DetailLevel"));
    }
    async getAllDetailLevel() {
        return (await this.entityTable.getNumberArray("int:DetailLevel"));
    }
    async getCameraIndex(viewIndex) {
        return await this.entityTable.getNumber(viewIndex, "index:Vim.Camera:Camera");
    }
    async getAllCameraIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Camera:Camera");
    }
    async getCamera(viewIndex) {
        const index = await this.getCameraIndex(viewIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.camera?.get(index);
    }
    async getFamilyTypeIndex(viewIndex) {
        return await this.entityTable.getNumber(viewIndex, "index:Vim.FamilyType:FamilyType");
    }
    async getAllFamilyTypeIndex() {
        return await this.entityTable.getNumberArray("index:Vim.FamilyType:FamilyType");
    }
    async getFamilyType(viewIndex) {
        const index = await this.getFamilyTypeIndex(viewIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.familyType?.get(index);
    }
    async getElementIndex(viewIndex) {
        return await this.entityTable.getNumber(viewIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(viewIndex) {
        const index = await this.getElementIndex(viewIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.ViewTable = ViewTable;
class ElementInView {
    static async createFromTable(table, index) {
        let result = new ElementInView();
        result.index = index;
        await Promise.all([
            table.getViewIndex(index).then(v => result.viewIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.ElementInView = ElementInView;
class ElementInViewTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.ElementInView");
        if (!entity) {
            return undefined;
        }
        let table = new ElementInViewTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(elementInViewIndex) {
        return await ElementInView.createFromTable(this, elementInViewIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let viewIndex;
        let elementIndex;
        await Promise.all([
            (async () => { viewIndex = (await localTable.getNumberArray("index:Vim.View:View")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let elementInView = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            elementInView.push({
                index: i,
                viewIndex: viewIndex ? viewIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return elementInView;
    }
    async getViewIndex(elementInViewIndex) {
        return await this.entityTable.getNumber(elementInViewIndex, "index:Vim.View:View");
    }
    async getAllViewIndex() {
        return await this.entityTable.getNumberArray("index:Vim.View:View");
    }
    async getView(elementInViewIndex) {
        const index = await this.getViewIndex(elementInViewIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.view?.get(index);
    }
    async getElementIndex(elementInViewIndex) {
        return await this.entityTable.getNumber(elementInViewIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(elementInViewIndex) {
        const index = await this.getElementIndex(elementInViewIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.ElementInViewTable = ElementInViewTable;
class ShapeInView {
    static async createFromTable(table, index) {
        let result = new ShapeInView();
        result.index = index;
        await Promise.all([
            table.getShapeIndex(index).then(v => result.shapeIndex = v),
            table.getViewIndex(index).then(v => result.viewIndex = v),
        ]);
        return result;
    }
}
exports.ShapeInView = ShapeInView;
class ShapeInViewTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.ShapeInView");
        if (!entity) {
            return undefined;
        }
        let table = new ShapeInViewTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(shapeInViewIndex) {
        return await ShapeInView.createFromTable(this, shapeInViewIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let shapeIndex;
        let viewIndex;
        await Promise.all([
            (async () => { shapeIndex = (await localTable.getNumberArray("index:Vim.Shape:Shape")); })(),
            (async () => { viewIndex = (await localTable.getNumberArray("index:Vim.View:View")); })(),
        ]);
        let shapeInView = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            shapeInView.push({
                index: i,
                shapeIndex: shapeIndex ? shapeIndex[i] : undefined,
                viewIndex: viewIndex ? viewIndex[i] : undefined
            });
        }
        return shapeInView;
    }
    async getShapeIndex(shapeInViewIndex) {
        return await this.entityTable.getNumber(shapeInViewIndex, "index:Vim.Shape:Shape");
    }
    async getAllShapeIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Shape:Shape");
    }
    async getShape(shapeInViewIndex) {
        const index = await this.getShapeIndex(shapeInViewIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.shape?.get(index);
    }
    async getViewIndex(shapeInViewIndex) {
        return await this.entityTable.getNumber(shapeInViewIndex, "index:Vim.View:View");
    }
    async getAllViewIndex() {
        return await this.entityTable.getNumberArray("index:Vim.View:View");
    }
    async getView(shapeInViewIndex) {
        const index = await this.getViewIndex(shapeInViewIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.view?.get(index);
    }
}
exports.ShapeInViewTable = ShapeInViewTable;
class AssetInView {
    static async createFromTable(table, index) {
        let result = new AssetInView();
        result.index = index;
        await Promise.all([
            table.getAssetIndex(index).then(v => result.assetIndex = v),
            table.getViewIndex(index).then(v => result.viewIndex = v),
        ]);
        return result;
    }
}
exports.AssetInView = AssetInView;
class AssetInViewTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.AssetInView");
        if (!entity) {
            return undefined;
        }
        let table = new AssetInViewTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(assetInViewIndex) {
        return await AssetInView.createFromTable(this, assetInViewIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let assetIndex;
        let viewIndex;
        await Promise.all([
            (async () => { assetIndex = (await localTable.getNumberArray("index:Vim.Asset:Asset")); })(),
            (async () => { viewIndex = (await localTable.getNumberArray("index:Vim.View:View")); })(),
        ]);
        let assetInView = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            assetInView.push({
                index: i,
                assetIndex: assetIndex ? assetIndex[i] : undefined,
                viewIndex: viewIndex ? viewIndex[i] : undefined
            });
        }
        return assetInView;
    }
    async getAssetIndex(assetInViewIndex) {
        return await this.entityTable.getNumber(assetInViewIndex, "index:Vim.Asset:Asset");
    }
    async getAllAssetIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Asset:Asset");
    }
    async getAsset(assetInViewIndex) {
        const index = await this.getAssetIndex(assetInViewIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.asset?.get(index);
    }
    async getViewIndex(assetInViewIndex) {
        return await this.entityTable.getNumber(assetInViewIndex, "index:Vim.View:View");
    }
    async getAllViewIndex() {
        return await this.entityTable.getNumberArray("index:Vim.View:View");
    }
    async getView(assetInViewIndex) {
        const index = await this.getViewIndex(assetInViewIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.view?.get(index);
    }
}
exports.AssetInViewTable = AssetInViewTable;
class AssetInViewSheet {
    static async createFromTable(table, index) {
        let result = new AssetInViewSheet();
        result.index = index;
        await Promise.all([
            table.getAssetIndex(index).then(v => result.assetIndex = v),
            table.getViewSheetIndex(index).then(v => result.viewSheetIndex = v),
        ]);
        return result;
    }
}
exports.AssetInViewSheet = AssetInViewSheet;
class AssetInViewSheetTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.AssetInViewSheet");
        if (!entity) {
            return undefined;
        }
        let table = new AssetInViewSheetTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(assetInViewSheetIndex) {
        return await AssetInViewSheet.createFromTable(this, assetInViewSheetIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let assetIndex;
        let viewSheetIndex;
        await Promise.all([
            (async () => { assetIndex = (await localTable.getNumberArray("index:Vim.Asset:Asset")); })(),
            (async () => { viewSheetIndex = (await localTable.getNumberArray("index:Vim.ViewSheet:ViewSheet")); })(),
        ]);
        let assetInViewSheet = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            assetInViewSheet.push({
                index: i,
                assetIndex: assetIndex ? assetIndex[i] : undefined,
                viewSheetIndex: viewSheetIndex ? viewSheetIndex[i] : undefined
            });
        }
        return assetInViewSheet;
    }
    async getAssetIndex(assetInViewSheetIndex) {
        return await this.entityTable.getNumber(assetInViewSheetIndex, "index:Vim.Asset:Asset");
    }
    async getAllAssetIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Asset:Asset");
    }
    async getAsset(assetInViewSheetIndex) {
        const index = await this.getAssetIndex(assetInViewSheetIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.asset?.get(index);
    }
    async getViewSheetIndex(assetInViewSheetIndex) {
        return await this.entityTable.getNumber(assetInViewSheetIndex, "index:Vim.ViewSheet:ViewSheet");
    }
    async getAllViewSheetIndex() {
        return await this.entityTable.getNumberArray("index:Vim.ViewSheet:ViewSheet");
    }
    async getViewSheet(assetInViewSheetIndex) {
        const index = await this.getViewSheetIndex(assetInViewSheetIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.viewSheet?.get(index);
    }
}
exports.AssetInViewSheetTable = AssetInViewSheetTable;
class LevelInView {
    static async createFromTable(table, index) {
        let result = new LevelInView();
        result.index = index;
        await Promise.all([
            table.getExtents_Min_X(index).then(v => result.extents_Min_X = v),
            table.getExtents_Min_Y(index).then(v => result.extents_Min_Y = v),
            table.getExtents_Min_Z(index).then(v => result.extents_Min_Z = v),
            table.getExtents_Max_X(index).then(v => result.extents_Max_X = v),
            table.getExtents_Max_Y(index).then(v => result.extents_Max_Y = v),
            table.getExtents_Max_Z(index).then(v => result.extents_Max_Z = v),
            table.getLevelIndex(index).then(v => result.levelIndex = v),
            table.getViewIndex(index).then(v => result.viewIndex = v),
        ]);
        return result;
    }
}
exports.LevelInView = LevelInView;
class LevelInViewTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.LevelInView");
        if (!entity) {
            return undefined;
        }
        let table = new LevelInViewTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(levelInViewIndex) {
        return await LevelInView.createFromTable(this, levelInViewIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let extents_Min_X;
        let extents_Min_Y;
        let extents_Min_Z;
        let extents_Max_X;
        let extents_Max_Y;
        let extents_Max_Z;
        let levelIndex;
        let viewIndex;
        await Promise.all([
            (async () => { extents_Min_X = (await localTable.getNumberArray("double:Extents.Min.X")); })(),
            (async () => { extents_Min_Y = (await localTable.getNumberArray("double:Extents.Min.Y")); })(),
            (async () => { extents_Min_Z = (await localTable.getNumberArray("double:Extents.Min.Z")); })(),
            (async () => { extents_Max_X = (await localTable.getNumberArray("double:Extents.Max.X")); })(),
            (async () => { extents_Max_Y = (await localTable.getNumberArray("double:Extents.Max.Y")); })(),
            (async () => { extents_Max_Z = (await localTable.getNumberArray("double:Extents.Max.Z")); })(),
            (async () => { levelIndex = (await localTable.getNumberArray("index:Vim.Level:Level")); })(),
            (async () => { viewIndex = (await localTable.getNumberArray("index:Vim.View:View")); })(),
        ]);
        let levelInView = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            levelInView.push({
                index: i,
                extents_Min_X: extents_Min_X ? extents_Min_X[i] : undefined,
                extents_Min_Y: extents_Min_Y ? extents_Min_Y[i] : undefined,
                extents_Min_Z: extents_Min_Z ? extents_Min_Z[i] : undefined,
                extents_Max_X: extents_Max_X ? extents_Max_X[i] : undefined,
                extents_Max_Y: extents_Max_Y ? extents_Max_Y[i] : undefined,
                extents_Max_Z: extents_Max_Z ? extents_Max_Z[i] : undefined,
                levelIndex: levelIndex ? levelIndex[i] : undefined,
                viewIndex: viewIndex ? viewIndex[i] : undefined
            });
        }
        return levelInView;
    }
    async getExtents_Min_X(levelInViewIndex) {
        return (await this.entityTable.getNumber(levelInViewIndex, "double:Extents.Min.X"));
    }
    async getAllExtents_Min_X() {
        return (await this.entityTable.getNumberArray("double:Extents.Min.X"));
    }
    async getExtents_Min_Y(levelInViewIndex) {
        return (await this.entityTable.getNumber(levelInViewIndex, "double:Extents.Min.Y"));
    }
    async getAllExtents_Min_Y() {
        return (await this.entityTable.getNumberArray("double:Extents.Min.Y"));
    }
    async getExtents_Min_Z(levelInViewIndex) {
        return (await this.entityTable.getNumber(levelInViewIndex, "double:Extents.Min.Z"));
    }
    async getAllExtents_Min_Z() {
        return (await this.entityTable.getNumberArray("double:Extents.Min.Z"));
    }
    async getExtents_Max_X(levelInViewIndex) {
        return (await this.entityTable.getNumber(levelInViewIndex, "double:Extents.Max.X"));
    }
    async getAllExtents_Max_X() {
        return (await this.entityTable.getNumberArray("double:Extents.Max.X"));
    }
    async getExtents_Max_Y(levelInViewIndex) {
        return (await this.entityTable.getNumber(levelInViewIndex, "double:Extents.Max.Y"));
    }
    async getAllExtents_Max_Y() {
        return (await this.entityTable.getNumberArray("double:Extents.Max.Y"));
    }
    async getExtents_Max_Z(levelInViewIndex) {
        return (await this.entityTable.getNumber(levelInViewIndex, "double:Extents.Max.Z"));
    }
    async getAllExtents_Max_Z() {
        return (await this.entityTable.getNumberArray("double:Extents.Max.Z"));
    }
    async getLevelIndex(levelInViewIndex) {
        return await this.entityTable.getNumber(levelInViewIndex, "index:Vim.Level:Level");
    }
    async getAllLevelIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Level:Level");
    }
    async getLevel(levelInViewIndex) {
        const index = await this.getLevelIndex(levelInViewIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.level?.get(index);
    }
    async getViewIndex(levelInViewIndex) {
        return await this.entityTable.getNumber(levelInViewIndex, "index:Vim.View:View");
    }
    async getAllViewIndex() {
        return await this.entityTable.getNumberArray("index:Vim.View:View");
    }
    async getView(levelInViewIndex) {
        const index = await this.getViewIndex(levelInViewIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.view?.get(index);
    }
}
exports.LevelInViewTable = LevelInViewTable;
class Camera {
    static async createFromTable(table, index) {
        let result = new Camera();
        result.index = index;
        await Promise.all([
            table.getId(index).then(v => result.id = v),
            table.getIsPerspective(index).then(v => result.isPerspective = v),
            table.getVerticalExtent(index).then(v => result.verticalExtent = v),
            table.getHorizontalExtent(index).then(v => result.horizontalExtent = v),
            table.getFarDistance(index).then(v => result.farDistance = v),
            table.getNearDistance(index).then(v => result.nearDistance = v),
            table.getTargetDistance(index).then(v => result.targetDistance = v),
            table.getRightOffset(index).then(v => result.rightOffset = v),
            table.getUpOffset(index).then(v => result.upOffset = v),
        ]);
        return result;
    }
}
exports.Camera = Camera;
class CameraTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.Camera");
        if (!entity) {
            return undefined;
        }
        let table = new CameraTable();
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(cameraIndex) {
        return await Camera.createFromTable(this, cameraIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let id;
        let isPerspective;
        let verticalExtent;
        let horizontalExtent;
        let farDistance;
        let nearDistance;
        let targetDistance;
        let rightOffset;
        let upOffset;
        await Promise.all([
            (async () => { id = (await localTable.getNumberArray("int:Id")); })(),
            (async () => { isPerspective = (await localTable.getNumberArray("int:IsPerspective")); })(),
            (async () => { verticalExtent = (await localTable.getNumberArray("double:VerticalExtent")); })(),
            (async () => { horizontalExtent = (await localTable.getNumberArray("double:HorizontalExtent")); })(),
            (async () => { farDistance = (await localTable.getNumberArray("double:FarDistance")); })(),
            (async () => { nearDistance = (await localTable.getNumberArray("double:NearDistance")); })(),
            (async () => { targetDistance = (await localTable.getNumberArray("double:TargetDistance")); })(),
            (async () => { rightOffset = (await localTable.getNumberArray("double:RightOffset")); })(),
            (async () => { upOffset = (await localTable.getNumberArray("double:UpOffset")); })(),
        ]);
        let camera = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            camera.push({
                index: i,
                id: id ? id[i] : undefined,
                isPerspective: isPerspective ? isPerspective[i] : undefined,
                verticalExtent: verticalExtent ? verticalExtent[i] : undefined,
                horizontalExtent: horizontalExtent ? horizontalExtent[i] : undefined,
                farDistance: farDistance ? farDistance[i] : undefined,
                nearDistance: nearDistance ? nearDistance[i] : undefined,
                targetDistance: targetDistance ? targetDistance[i] : undefined,
                rightOffset: rightOffset ? rightOffset[i] : undefined,
                upOffset: upOffset ? upOffset[i] : undefined
            });
        }
        return camera;
    }
    async getId(cameraIndex) {
        return (await this.entityTable.getNumber(cameraIndex, "int:Id"));
    }
    async getAllId() {
        return (await this.entityTable.getNumberArray("int:Id"));
    }
    async getIsPerspective(cameraIndex) {
        return (await this.entityTable.getNumber(cameraIndex, "int:IsPerspective"));
    }
    async getAllIsPerspective() {
        return (await this.entityTable.getNumberArray("int:IsPerspective"));
    }
    async getVerticalExtent(cameraIndex) {
        return (await this.entityTable.getNumber(cameraIndex, "double:VerticalExtent"));
    }
    async getAllVerticalExtent() {
        return (await this.entityTable.getNumberArray("double:VerticalExtent"));
    }
    async getHorizontalExtent(cameraIndex) {
        return (await this.entityTable.getNumber(cameraIndex, "double:HorizontalExtent"));
    }
    async getAllHorizontalExtent() {
        return (await this.entityTable.getNumberArray("double:HorizontalExtent"));
    }
    async getFarDistance(cameraIndex) {
        return (await this.entityTable.getNumber(cameraIndex, "double:FarDistance"));
    }
    async getAllFarDistance() {
        return (await this.entityTable.getNumberArray("double:FarDistance"));
    }
    async getNearDistance(cameraIndex) {
        return (await this.entityTable.getNumber(cameraIndex, "double:NearDistance"));
    }
    async getAllNearDistance() {
        return (await this.entityTable.getNumberArray("double:NearDistance"));
    }
    async getTargetDistance(cameraIndex) {
        return (await this.entityTable.getNumber(cameraIndex, "double:TargetDistance"));
    }
    async getAllTargetDistance() {
        return (await this.entityTable.getNumberArray("double:TargetDistance"));
    }
    async getRightOffset(cameraIndex) {
        return (await this.entityTable.getNumber(cameraIndex, "double:RightOffset"));
    }
    async getAllRightOffset() {
        return (await this.entityTable.getNumberArray("double:RightOffset"));
    }
    async getUpOffset(cameraIndex) {
        return (await this.entityTable.getNumber(cameraIndex, "double:UpOffset"));
    }
    async getAllUpOffset() {
        return (await this.entityTable.getNumberArray("double:UpOffset"));
    }
}
exports.CameraTable = CameraTable;
class Material {
    static async createFromTable(table, index) {
        let result = new Material();
        result.index = index;
        await Promise.all([
            table.getName(index).then(v => result.name = v),
            table.getMaterialCategory(index).then(v => result.materialCategory = v),
            table.getColor_X(index).then(v => result.color_X = v),
            table.getColor_Y(index).then(v => result.color_Y = v),
            table.getColor_Z(index).then(v => result.color_Z = v),
            table.getColorUvScaling_X(index).then(v => result.colorUvScaling_X = v),
            table.getColorUvScaling_Y(index).then(v => result.colorUvScaling_Y = v),
            table.getColorUvOffset_X(index).then(v => result.colorUvOffset_X = v),
            table.getColorUvOffset_Y(index).then(v => result.colorUvOffset_Y = v),
            table.getNormalUvScaling_X(index).then(v => result.normalUvScaling_X = v),
            table.getNormalUvScaling_Y(index).then(v => result.normalUvScaling_Y = v),
            table.getNormalUvOffset_X(index).then(v => result.normalUvOffset_X = v),
            table.getNormalUvOffset_Y(index).then(v => result.normalUvOffset_Y = v),
            table.getNormalAmount(index).then(v => result.normalAmount = v),
            table.getGlossiness(index).then(v => result.glossiness = v),
            table.getSmoothness(index).then(v => result.smoothness = v),
            table.getTransparency(index).then(v => result.transparency = v),
            table.getColorTextureFileIndex(index).then(v => result.colorTextureFileIndex = v),
            table.getNormalTextureFileIndex(index).then(v => result.normalTextureFileIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.Material = Material;
class MaterialTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.Material");
        if (!entity) {
            return undefined;
        }
        let table = new MaterialTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(materialIndex) {
        return await Material.createFromTable(this, materialIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let name;
        let materialCategory;
        let color_X;
        let color_Y;
        let color_Z;
        let colorUvScaling_X;
        let colorUvScaling_Y;
        let colorUvOffset_X;
        let colorUvOffset_Y;
        let normalUvScaling_X;
        let normalUvScaling_Y;
        let normalUvOffset_X;
        let normalUvOffset_Y;
        let normalAmount;
        let glossiness;
        let smoothness;
        let transparency;
        let colorTextureFileIndex;
        let normalTextureFileIndex;
        let elementIndex;
        await Promise.all([
            (async () => { name = (await localTable.getStringArray("string:Name")); })(),
            (async () => { materialCategory = (await localTable.getStringArray("string:MaterialCategory")); })(),
            (async () => { color_X = (await localTable.getNumberArray("double:Color.X")); })(),
            (async () => { color_Y = (await localTable.getNumberArray("double:Color.Y")); })(),
            (async () => { color_Z = (await localTable.getNumberArray("double:Color.Z")); })(),
            (async () => { colorUvScaling_X = (await localTable.getNumberArray("double:ColorUvScaling.X")); })(),
            (async () => { colorUvScaling_Y = (await localTable.getNumberArray("double:ColorUvScaling.Y")); })(),
            (async () => { colorUvOffset_X = (await localTable.getNumberArray("double:ColorUvOffset.X")); })(),
            (async () => { colorUvOffset_Y = (await localTable.getNumberArray("double:ColorUvOffset.Y")); })(),
            (async () => { normalUvScaling_X = (await localTable.getNumberArray("double:NormalUvScaling.X")); })(),
            (async () => { normalUvScaling_Y = (await localTable.getNumberArray("double:NormalUvScaling.Y")); })(),
            (async () => { normalUvOffset_X = (await localTable.getNumberArray("double:NormalUvOffset.X")); })(),
            (async () => { normalUvOffset_Y = (await localTable.getNumberArray("double:NormalUvOffset.Y")); })(),
            (async () => { normalAmount = (await localTable.getNumberArray("double:NormalAmount")); })(),
            (async () => { glossiness = (await localTable.getNumberArray("double:Glossiness")); })(),
            (async () => { smoothness = (await localTable.getNumberArray("double:Smoothness")); })(),
            (async () => { transparency = (await localTable.getNumberArray("double:Transparency")); })(),
            (async () => { colorTextureFileIndex = (await localTable.getNumberArray("index:Vim.Asset:ColorTextureFile")); })(),
            (async () => { normalTextureFileIndex = (await localTable.getNumberArray("index:Vim.Asset:NormalTextureFile")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let material = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            material.push({
                index: i,
                name: name ? name[i] : undefined,
                materialCategory: materialCategory ? materialCategory[i] : undefined,
                color_X: color_X ? color_X[i] : undefined,
                color_Y: color_Y ? color_Y[i] : undefined,
                color_Z: color_Z ? color_Z[i] : undefined,
                colorUvScaling_X: colorUvScaling_X ? colorUvScaling_X[i] : undefined,
                colorUvScaling_Y: colorUvScaling_Y ? colorUvScaling_Y[i] : undefined,
                colorUvOffset_X: colorUvOffset_X ? colorUvOffset_X[i] : undefined,
                colorUvOffset_Y: colorUvOffset_Y ? colorUvOffset_Y[i] : undefined,
                normalUvScaling_X: normalUvScaling_X ? normalUvScaling_X[i] : undefined,
                normalUvScaling_Y: normalUvScaling_Y ? normalUvScaling_Y[i] : undefined,
                normalUvOffset_X: normalUvOffset_X ? normalUvOffset_X[i] : undefined,
                normalUvOffset_Y: normalUvOffset_Y ? normalUvOffset_Y[i] : undefined,
                normalAmount: normalAmount ? normalAmount[i] : undefined,
                glossiness: glossiness ? glossiness[i] : undefined,
                smoothness: smoothness ? smoothness[i] : undefined,
                transparency: transparency ? transparency[i] : undefined,
                colorTextureFileIndex: colorTextureFileIndex ? colorTextureFileIndex[i] : undefined,
                normalTextureFileIndex: normalTextureFileIndex ? normalTextureFileIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return material;
    }
    async getName(materialIndex) {
        return (await this.entityTable.getString(materialIndex, "string:Name"));
    }
    async getAllName() {
        return (await this.entityTable.getStringArray("string:Name"));
    }
    async getMaterialCategory(materialIndex) {
        return (await this.entityTable.getString(materialIndex, "string:MaterialCategory"));
    }
    async getAllMaterialCategory() {
        return (await this.entityTable.getStringArray("string:MaterialCategory"));
    }
    async getColor_X(materialIndex) {
        return (await this.entityTable.getNumber(materialIndex, "double:Color.X"));
    }
    async getAllColor_X() {
        return (await this.entityTable.getNumberArray("double:Color.X"));
    }
    async getColor_Y(materialIndex) {
        return (await this.entityTable.getNumber(materialIndex, "double:Color.Y"));
    }
    async getAllColor_Y() {
        return (await this.entityTable.getNumberArray("double:Color.Y"));
    }
    async getColor_Z(materialIndex) {
        return (await this.entityTable.getNumber(materialIndex, "double:Color.Z"));
    }
    async getAllColor_Z() {
        return (await this.entityTable.getNumberArray("double:Color.Z"));
    }
    async getColorUvScaling_X(materialIndex) {
        return (await this.entityTable.getNumber(materialIndex, "double:ColorUvScaling.X"));
    }
    async getAllColorUvScaling_X() {
        return (await this.entityTable.getNumberArray("double:ColorUvScaling.X"));
    }
    async getColorUvScaling_Y(materialIndex) {
        return (await this.entityTable.getNumber(materialIndex, "double:ColorUvScaling.Y"));
    }
    async getAllColorUvScaling_Y() {
        return (await this.entityTable.getNumberArray("double:ColorUvScaling.Y"));
    }
    async getColorUvOffset_X(materialIndex) {
        return (await this.entityTable.getNumber(materialIndex, "double:ColorUvOffset.X"));
    }
    async getAllColorUvOffset_X() {
        return (await this.entityTable.getNumberArray("double:ColorUvOffset.X"));
    }
    async getColorUvOffset_Y(materialIndex) {
        return (await this.entityTable.getNumber(materialIndex, "double:ColorUvOffset.Y"));
    }
    async getAllColorUvOffset_Y() {
        return (await this.entityTable.getNumberArray("double:ColorUvOffset.Y"));
    }
    async getNormalUvScaling_X(materialIndex) {
        return (await this.entityTable.getNumber(materialIndex, "double:NormalUvScaling.X"));
    }
    async getAllNormalUvScaling_X() {
        return (await this.entityTable.getNumberArray("double:NormalUvScaling.X"));
    }
    async getNormalUvScaling_Y(materialIndex) {
        return (await this.entityTable.getNumber(materialIndex, "double:NormalUvScaling.Y"));
    }
    async getAllNormalUvScaling_Y() {
        return (await this.entityTable.getNumberArray("double:NormalUvScaling.Y"));
    }
    async getNormalUvOffset_X(materialIndex) {
        return (await this.entityTable.getNumber(materialIndex, "double:NormalUvOffset.X"));
    }
    async getAllNormalUvOffset_X() {
        return (await this.entityTable.getNumberArray("double:NormalUvOffset.X"));
    }
    async getNormalUvOffset_Y(materialIndex) {
        return (await this.entityTable.getNumber(materialIndex, "double:NormalUvOffset.Y"));
    }
    async getAllNormalUvOffset_Y() {
        return (await this.entityTable.getNumberArray("double:NormalUvOffset.Y"));
    }
    async getNormalAmount(materialIndex) {
        return (await this.entityTable.getNumber(materialIndex, "double:NormalAmount"));
    }
    async getAllNormalAmount() {
        return (await this.entityTable.getNumberArray("double:NormalAmount"));
    }
    async getGlossiness(materialIndex) {
        return (await this.entityTable.getNumber(materialIndex, "double:Glossiness"));
    }
    async getAllGlossiness() {
        return (await this.entityTable.getNumberArray("double:Glossiness"));
    }
    async getSmoothness(materialIndex) {
        return (await this.entityTable.getNumber(materialIndex, "double:Smoothness"));
    }
    async getAllSmoothness() {
        return (await this.entityTable.getNumberArray("double:Smoothness"));
    }
    async getTransparency(materialIndex) {
        return (await this.entityTable.getNumber(materialIndex, "double:Transparency"));
    }
    async getAllTransparency() {
        return (await this.entityTable.getNumberArray("double:Transparency"));
    }
    async getColorTextureFileIndex(materialIndex) {
        return await this.entityTable.getNumber(materialIndex, "index:Vim.Asset:ColorTextureFile");
    }
    async getAllColorTextureFileIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Asset:ColorTextureFile");
    }
    async getColorTextureFile(materialIndex) {
        const index = await this.getColorTextureFileIndex(materialIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.asset?.get(index);
    }
    async getNormalTextureFileIndex(materialIndex) {
        return await this.entityTable.getNumber(materialIndex, "index:Vim.Asset:NormalTextureFile");
    }
    async getAllNormalTextureFileIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Asset:NormalTextureFile");
    }
    async getNormalTextureFile(materialIndex) {
        const index = await this.getNormalTextureFileIndex(materialIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.asset?.get(index);
    }
    async getElementIndex(materialIndex) {
        return await this.entityTable.getNumber(materialIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(materialIndex) {
        const index = await this.getElementIndex(materialIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.MaterialTable = MaterialTable;
class MaterialInElement {
    static async createFromTable(table, index) {
        let result = new MaterialInElement();
        result.index = index;
        await Promise.all([
            table.getArea(index).then(v => result.area = v),
            table.getVolume(index).then(v => result.volume = v),
            table.getIsPaint(index).then(v => result.isPaint = v),
            table.getMaterialIndex(index).then(v => result.materialIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.MaterialInElement = MaterialInElement;
class MaterialInElementTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.MaterialInElement");
        if (!entity) {
            return undefined;
        }
        let table = new MaterialInElementTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(materialInElementIndex) {
        return await MaterialInElement.createFromTable(this, materialInElementIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let area;
        let volume;
        let isPaint;
        let materialIndex;
        let elementIndex;
        await Promise.all([
            (async () => { area = (await localTable.getNumberArray("double:Area")); })(),
            (async () => { volume = (await localTable.getNumberArray("double:Volume")); })(),
            (async () => { isPaint = (await localTable.getBooleanArray("byte:IsPaint")); })(),
            (async () => { materialIndex = (await localTable.getNumberArray("index:Vim.Material:Material")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let materialInElement = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            materialInElement.push({
                index: i,
                area: area ? area[i] : undefined,
                volume: volume ? volume[i] : undefined,
                isPaint: isPaint ? isPaint[i] : undefined,
                materialIndex: materialIndex ? materialIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return materialInElement;
    }
    async getArea(materialInElementIndex) {
        return (await this.entityTable.getNumber(materialInElementIndex, "double:Area"));
    }
    async getAllArea() {
        return (await this.entityTable.getNumberArray("double:Area"));
    }
    async getVolume(materialInElementIndex) {
        return (await this.entityTable.getNumber(materialInElementIndex, "double:Volume"));
    }
    async getAllVolume() {
        return (await this.entityTable.getNumberArray("double:Volume"));
    }
    async getIsPaint(materialInElementIndex) {
        return (await this.entityTable.getBoolean(materialInElementIndex, "byte:IsPaint"));
    }
    async getAllIsPaint() {
        return (await this.entityTable.getBooleanArray("byte:IsPaint"));
    }
    async getMaterialIndex(materialInElementIndex) {
        return await this.entityTable.getNumber(materialInElementIndex, "index:Vim.Material:Material");
    }
    async getAllMaterialIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Material:Material");
    }
    async getMaterial(materialInElementIndex) {
        const index = await this.getMaterialIndex(materialInElementIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.material?.get(index);
    }
    async getElementIndex(materialInElementIndex) {
        return await this.entityTable.getNumber(materialInElementIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(materialInElementIndex) {
        const index = await this.getElementIndex(materialInElementIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.MaterialInElementTable = MaterialInElementTable;
class CompoundStructureLayer {
    static async createFromTable(table, index) {
        let result = new CompoundStructureLayer();
        result.index = index;
        await Promise.all([
            table.getOrderIndex(index).then(v => result.orderIndex = v),
            table.getWidth(index).then(v => result.width = v),
            table.getMaterialFunctionAssignment(index).then(v => result.materialFunctionAssignment = v),
            table.getMaterialIndex(index).then(v => result.materialIndex = v),
            table.getCompoundStructureIndex(index).then(v => result.compoundStructureIndex = v),
        ]);
        return result;
    }
}
exports.CompoundStructureLayer = CompoundStructureLayer;
class CompoundStructureLayerTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.CompoundStructureLayer");
        if (!entity) {
            return undefined;
        }
        let table = new CompoundStructureLayerTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(compoundStructureLayerIndex) {
        return await CompoundStructureLayer.createFromTable(this, compoundStructureLayerIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let orderIndex;
        let width;
        let materialFunctionAssignment;
        let materialIndex;
        let compoundStructureIndex;
        await Promise.all([
            (async () => { orderIndex = (await localTable.getNumberArray("int:OrderIndex")); })(),
            (async () => { width = (await localTable.getNumberArray("double:Width")); })(),
            (async () => { materialFunctionAssignment = (await localTable.getStringArray("string:MaterialFunctionAssignment")); })(),
            (async () => { materialIndex = (await localTable.getNumberArray("index:Vim.Material:Material")); })(),
            (async () => { compoundStructureIndex = (await localTable.getNumberArray("index:Vim.CompoundStructure:CompoundStructure")); })(),
        ]);
        let compoundStructureLayer = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            compoundStructureLayer.push({
                index: i,
                orderIndex: orderIndex ? orderIndex[i] : undefined,
                width: width ? width[i] : undefined,
                materialFunctionAssignment: materialFunctionAssignment ? materialFunctionAssignment[i] : undefined,
                materialIndex: materialIndex ? materialIndex[i] : undefined,
                compoundStructureIndex: compoundStructureIndex ? compoundStructureIndex[i] : undefined
            });
        }
        return compoundStructureLayer;
    }
    async getOrderIndex(compoundStructureLayerIndex) {
        return (await this.entityTable.getNumber(compoundStructureLayerIndex, "int:OrderIndex"));
    }
    async getAllOrderIndex() {
        return (await this.entityTable.getNumberArray("int:OrderIndex"));
    }
    async getWidth(compoundStructureLayerIndex) {
        return (await this.entityTable.getNumber(compoundStructureLayerIndex, "double:Width"));
    }
    async getAllWidth() {
        return (await this.entityTable.getNumberArray("double:Width"));
    }
    async getMaterialFunctionAssignment(compoundStructureLayerIndex) {
        return (await this.entityTable.getString(compoundStructureLayerIndex, "string:MaterialFunctionAssignment"));
    }
    async getAllMaterialFunctionAssignment() {
        return (await this.entityTable.getStringArray("string:MaterialFunctionAssignment"));
    }
    async getMaterialIndex(compoundStructureLayerIndex) {
        return await this.entityTable.getNumber(compoundStructureLayerIndex, "index:Vim.Material:Material");
    }
    async getAllMaterialIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Material:Material");
    }
    async getMaterial(compoundStructureLayerIndex) {
        const index = await this.getMaterialIndex(compoundStructureLayerIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.material?.get(index);
    }
    async getCompoundStructureIndex(compoundStructureLayerIndex) {
        return await this.entityTable.getNumber(compoundStructureLayerIndex, "index:Vim.CompoundStructure:CompoundStructure");
    }
    async getAllCompoundStructureIndex() {
        return await this.entityTable.getNumberArray("index:Vim.CompoundStructure:CompoundStructure");
    }
    async getCompoundStructure(compoundStructureLayerIndex) {
        const index = await this.getCompoundStructureIndex(compoundStructureLayerIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.compoundStructure?.get(index);
    }
}
exports.CompoundStructureLayerTable = CompoundStructureLayerTable;
class CompoundStructure {
    static async createFromTable(table, index) {
        let result = new CompoundStructure();
        result.index = index;
        await Promise.all([
            table.getWidth(index).then(v => result.width = v),
            table.getStructuralLayerIndex(index).then(v => result.structuralLayerIndex = v),
        ]);
        return result;
    }
}
exports.CompoundStructure = CompoundStructure;
class CompoundStructureTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.CompoundStructure");
        if (!entity) {
            return undefined;
        }
        let table = new CompoundStructureTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(compoundStructureIndex) {
        return await CompoundStructure.createFromTable(this, compoundStructureIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let width;
        let structuralLayerIndex;
        await Promise.all([
            (async () => { width = (await localTable.getNumberArray("double:Width")); })(),
            (async () => { structuralLayerIndex = (await localTable.getNumberArray("index:Vim.CompoundStructureLayer:StructuralLayer")); })(),
        ]);
        let compoundStructure = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            compoundStructure.push({
                index: i,
                width: width ? width[i] : undefined,
                structuralLayerIndex: structuralLayerIndex ? structuralLayerIndex[i] : undefined
            });
        }
        return compoundStructure;
    }
    async getWidth(compoundStructureIndex) {
        return (await this.entityTable.getNumber(compoundStructureIndex, "double:Width"));
    }
    async getAllWidth() {
        return (await this.entityTable.getNumberArray("double:Width"));
    }
    async getStructuralLayerIndex(compoundStructureIndex) {
        return await this.entityTable.getNumber(compoundStructureIndex, "index:Vim.CompoundStructureLayer:StructuralLayer");
    }
    async getAllStructuralLayerIndex() {
        return await this.entityTable.getNumberArray("index:Vim.CompoundStructureLayer:StructuralLayer");
    }
    async getStructuralLayer(compoundStructureIndex) {
        const index = await this.getStructuralLayerIndex(compoundStructureIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.compoundStructureLayer?.get(index);
    }
}
exports.CompoundStructureTable = CompoundStructureTable;
class Node {
    static async createFromTable(table, index) {
        let result = new Node();
        result.index = index;
        await Promise.all([
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.Node = Node;
class NodeTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.Node");
        if (!entity) {
            return undefined;
        }
        let table = new NodeTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(nodeIndex) {
        return await Node.createFromTable(this, nodeIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let elementIndex;
        await Promise.all([
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let node = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            node.push({
                index: i,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return node;
    }
    async getElementIndex(nodeIndex) {
        return await this.entityTable.getNumber(nodeIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(nodeIndex) {
        const index = await this.getElementIndex(nodeIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.NodeTable = NodeTable;
class Geometry {
    static async createFromTable(table, index) {
        let result = new Geometry();
        result.index = index;
        await Promise.all([
            table.getBox_Min_X(index).then(v => result.box_Min_X = v),
            table.getBox_Min_Y(index).then(v => result.box_Min_Y = v),
            table.getBox_Min_Z(index).then(v => result.box_Min_Z = v),
            table.getBox_Max_X(index).then(v => result.box_Max_X = v),
            table.getBox_Max_Y(index).then(v => result.box_Max_Y = v),
            table.getBox_Max_Z(index).then(v => result.box_Max_Z = v),
            table.getVertexCount(index).then(v => result.vertexCount = v),
            table.getFaceCount(index).then(v => result.faceCount = v),
        ]);
        return result;
    }
}
exports.Geometry = Geometry;
class GeometryTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.Geometry");
        if (!entity) {
            return undefined;
        }
        let table = new GeometryTable();
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(geometryIndex) {
        return await Geometry.createFromTable(this, geometryIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let box_Min_X;
        let box_Min_Y;
        let box_Min_Z;
        let box_Max_X;
        let box_Max_Y;
        let box_Max_Z;
        let vertexCount;
        let faceCount;
        await Promise.all([
            (async () => { box_Min_X = (await localTable.getNumberArray("float:Box.Min.X")); })(),
            (async () => { box_Min_Y = (await localTable.getNumberArray("float:Box.Min.Y")); })(),
            (async () => { box_Min_Z = (await localTable.getNumberArray("float:Box.Min.Z")); })(),
            (async () => { box_Max_X = (await localTable.getNumberArray("float:Box.Max.X")); })(),
            (async () => { box_Max_Y = (await localTable.getNumberArray("float:Box.Max.Y")); })(),
            (async () => { box_Max_Z = (await localTable.getNumberArray("float:Box.Max.Z")); })(),
            (async () => { vertexCount = (await localTable.getNumberArray("int:VertexCount")); })(),
            (async () => { faceCount = (await localTable.getNumberArray("int:FaceCount")); })(),
        ]);
        let geometry = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            geometry.push({
                index: i,
                box_Min_X: box_Min_X ? box_Min_X[i] : undefined,
                box_Min_Y: box_Min_Y ? box_Min_Y[i] : undefined,
                box_Min_Z: box_Min_Z ? box_Min_Z[i] : undefined,
                box_Max_X: box_Max_X ? box_Max_X[i] : undefined,
                box_Max_Y: box_Max_Y ? box_Max_Y[i] : undefined,
                box_Max_Z: box_Max_Z ? box_Max_Z[i] : undefined,
                vertexCount: vertexCount ? vertexCount[i] : undefined,
                faceCount: faceCount ? faceCount[i] : undefined
            });
        }
        return geometry;
    }
    async getBox_Min_X(geometryIndex) {
        return (await this.entityTable.getNumber(geometryIndex, "float:Box.Min.X"));
    }
    async getAllBox_Min_X() {
        return (await this.entityTable.getNumberArray("float:Box.Min.X"));
    }
    async getBox_Min_Y(geometryIndex) {
        return (await this.entityTable.getNumber(geometryIndex, "float:Box.Min.Y"));
    }
    async getAllBox_Min_Y() {
        return (await this.entityTable.getNumberArray("float:Box.Min.Y"));
    }
    async getBox_Min_Z(geometryIndex) {
        return (await this.entityTable.getNumber(geometryIndex, "float:Box.Min.Z"));
    }
    async getAllBox_Min_Z() {
        return (await this.entityTable.getNumberArray("float:Box.Min.Z"));
    }
    async getBox_Max_X(geometryIndex) {
        return (await this.entityTable.getNumber(geometryIndex, "float:Box.Max.X"));
    }
    async getAllBox_Max_X() {
        return (await this.entityTable.getNumberArray("float:Box.Max.X"));
    }
    async getBox_Max_Y(geometryIndex) {
        return (await this.entityTable.getNumber(geometryIndex, "float:Box.Max.Y"));
    }
    async getAllBox_Max_Y() {
        return (await this.entityTable.getNumberArray("float:Box.Max.Y"));
    }
    async getBox_Max_Z(geometryIndex) {
        return (await this.entityTable.getNumber(geometryIndex, "float:Box.Max.Z"));
    }
    async getAllBox_Max_Z() {
        return (await this.entityTable.getNumberArray("float:Box.Max.Z"));
    }
    async getVertexCount(geometryIndex) {
        return (await this.entityTable.getNumber(geometryIndex, "int:VertexCount"));
    }
    async getAllVertexCount() {
        return (await this.entityTable.getNumberArray("int:VertexCount"));
    }
    async getFaceCount(geometryIndex) {
        return (await this.entityTable.getNumber(geometryIndex, "int:FaceCount"));
    }
    async getAllFaceCount() {
        return (await this.entityTable.getNumberArray("int:FaceCount"));
    }
}
exports.GeometryTable = GeometryTable;
class Shape {
    static async createFromTable(table, index) {
        let result = new Shape();
        result.index = index;
        await Promise.all([
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.Shape = Shape;
class ShapeTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.Shape");
        if (!entity) {
            return undefined;
        }
        let table = new ShapeTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(shapeIndex) {
        return await Shape.createFromTable(this, shapeIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let elementIndex;
        await Promise.all([
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let shape = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            shape.push({
                index: i,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return shape;
    }
    async getElementIndex(shapeIndex) {
        return await this.entityTable.getNumber(shapeIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(shapeIndex) {
        const index = await this.getElementIndex(shapeIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.ShapeTable = ShapeTable;
class ShapeCollection {
    static async createFromTable(table, index) {
        let result = new ShapeCollection();
        result.index = index;
        await Promise.all([
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.ShapeCollection = ShapeCollection;
class ShapeCollectionTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.ShapeCollection");
        if (!entity) {
            return undefined;
        }
        let table = new ShapeCollectionTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(shapeCollectionIndex) {
        return await ShapeCollection.createFromTable(this, shapeCollectionIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let elementIndex;
        await Promise.all([
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let shapeCollection = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            shapeCollection.push({
                index: i,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return shapeCollection;
    }
    async getElementIndex(shapeCollectionIndex) {
        return await this.entityTable.getNumber(shapeCollectionIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(shapeCollectionIndex) {
        const index = await this.getElementIndex(shapeCollectionIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.ShapeCollectionTable = ShapeCollectionTable;
class ShapeInShapeCollection {
    static async createFromTable(table, index) {
        let result = new ShapeInShapeCollection();
        result.index = index;
        await Promise.all([
            table.getShapeIndex(index).then(v => result.shapeIndex = v),
            table.getShapeCollectionIndex(index).then(v => result.shapeCollectionIndex = v),
        ]);
        return result;
    }
}
exports.ShapeInShapeCollection = ShapeInShapeCollection;
class ShapeInShapeCollectionTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.ShapeInShapeCollection");
        if (!entity) {
            return undefined;
        }
        let table = new ShapeInShapeCollectionTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(shapeInShapeCollectionIndex) {
        return await ShapeInShapeCollection.createFromTable(this, shapeInShapeCollectionIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let shapeIndex;
        let shapeCollectionIndex;
        await Promise.all([
            (async () => { shapeIndex = (await localTable.getNumberArray("index:Vim.Shape:Shape")); })(),
            (async () => { shapeCollectionIndex = (await localTable.getNumberArray("index:Vim.ShapeCollection:ShapeCollection")); })(),
        ]);
        let shapeInShapeCollection = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            shapeInShapeCollection.push({
                index: i,
                shapeIndex: shapeIndex ? shapeIndex[i] : undefined,
                shapeCollectionIndex: shapeCollectionIndex ? shapeCollectionIndex[i] : undefined
            });
        }
        return shapeInShapeCollection;
    }
    async getShapeIndex(shapeInShapeCollectionIndex) {
        return await this.entityTable.getNumber(shapeInShapeCollectionIndex, "index:Vim.Shape:Shape");
    }
    async getAllShapeIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Shape:Shape");
    }
    async getShape(shapeInShapeCollectionIndex) {
        const index = await this.getShapeIndex(shapeInShapeCollectionIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.shape?.get(index);
    }
    async getShapeCollectionIndex(shapeInShapeCollectionIndex) {
        return await this.entityTable.getNumber(shapeInShapeCollectionIndex, "index:Vim.ShapeCollection:ShapeCollection");
    }
    async getAllShapeCollectionIndex() {
        return await this.entityTable.getNumberArray("index:Vim.ShapeCollection:ShapeCollection");
    }
    async getShapeCollection(shapeInShapeCollectionIndex) {
        const index = await this.getShapeCollectionIndex(shapeInShapeCollectionIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.shapeCollection?.get(index);
    }
}
exports.ShapeInShapeCollectionTable = ShapeInShapeCollectionTable;
class System {
    static async createFromTable(table, index) {
        let result = new System();
        result.index = index;
        await Promise.all([
            table.getSystemType(index).then(v => result.systemType = v),
            table.getFamilyTypeIndex(index).then(v => result.familyTypeIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.System = System;
class SystemTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.System");
        if (!entity) {
            return undefined;
        }
        let table = new SystemTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(systemIndex) {
        return await System.createFromTable(this, systemIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let systemType;
        let familyTypeIndex;
        let elementIndex;
        await Promise.all([
            (async () => { systemType = (await localTable.getNumberArray("int:SystemType")); })(),
            (async () => { familyTypeIndex = (await localTable.getNumberArray("index:Vim.FamilyType:FamilyType")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let system = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            system.push({
                index: i,
                systemType: systemType ? systemType[i] : undefined,
                familyTypeIndex: familyTypeIndex ? familyTypeIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return system;
    }
    async getSystemType(systemIndex) {
        return (await this.entityTable.getNumber(systemIndex, "int:SystemType"));
    }
    async getAllSystemType() {
        return (await this.entityTable.getNumberArray("int:SystemType"));
    }
    async getFamilyTypeIndex(systemIndex) {
        return await this.entityTable.getNumber(systemIndex, "index:Vim.FamilyType:FamilyType");
    }
    async getAllFamilyTypeIndex() {
        return await this.entityTable.getNumberArray("index:Vim.FamilyType:FamilyType");
    }
    async getFamilyType(systemIndex) {
        const index = await this.getFamilyTypeIndex(systemIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.familyType?.get(index);
    }
    async getElementIndex(systemIndex) {
        return await this.entityTable.getNumber(systemIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(systemIndex) {
        const index = await this.getElementIndex(systemIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.SystemTable = SystemTable;
class ElementInSystem {
    static async createFromTable(table, index) {
        let result = new ElementInSystem();
        result.index = index;
        await Promise.all([
            table.getRoles(index).then(v => result.roles = v),
            table.getSystemIndex(index).then(v => result.systemIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.ElementInSystem = ElementInSystem;
class ElementInSystemTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.ElementInSystem");
        if (!entity) {
            return undefined;
        }
        let table = new ElementInSystemTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(elementInSystemIndex) {
        return await ElementInSystem.createFromTable(this, elementInSystemIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let roles;
        let systemIndex;
        let elementIndex;
        await Promise.all([
            (async () => { roles = (await localTable.getNumberArray("int:Roles")); })(),
            (async () => { systemIndex = (await localTable.getNumberArray("index:Vim.System:System")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let elementInSystem = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            elementInSystem.push({
                index: i,
                roles: roles ? roles[i] : undefined,
                systemIndex: systemIndex ? systemIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return elementInSystem;
    }
    async getRoles(elementInSystemIndex) {
        return (await this.entityTable.getNumber(elementInSystemIndex, "int:Roles"));
    }
    async getAllRoles() {
        return (await this.entityTable.getNumberArray("int:Roles"));
    }
    async getSystemIndex(elementInSystemIndex) {
        return await this.entityTable.getNumber(elementInSystemIndex, "index:Vim.System:System");
    }
    async getAllSystemIndex() {
        return await this.entityTable.getNumberArray("index:Vim.System:System");
    }
    async getSystem(elementInSystemIndex) {
        const index = await this.getSystemIndex(elementInSystemIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.system?.get(index);
    }
    async getElementIndex(elementInSystemIndex) {
        return await this.entityTable.getNumber(elementInSystemIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(elementInSystemIndex) {
        const index = await this.getElementIndex(elementInSystemIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.ElementInSystemTable = ElementInSystemTable;
class Warning {
    static async createFromTable(table, index) {
        let result = new Warning();
        result.index = index;
        await Promise.all([
            table.getGuid(index).then(v => result.guid = v),
            table.getSeverity(index).then(v => result.severity = v),
            table.getDescription(index).then(v => result.description = v),
            table.getBimDocumentIndex(index).then(v => result.bimDocumentIndex = v),
        ]);
        return result;
    }
}
exports.Warning = Warning;
class WarningTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.Warning");
        if (!entity) {
            return undefined;
        }
        let table = new WarningTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(warningIndex) {
        return await Warning.createFromTable(this, warningIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let guid;
        let severity;
        let description;
        let bimDocumentIndex;
        await Promise.all([
            (async () => { guid = (await localTable.getStringArray("string:Guid")); })(),
            (async () => { severity = (await localTable.getStringArray("string:Severity")); })(),
            (async () => { description = (await localTable.getStringArray("string:Description")); })(),
            (async () => { bimDocumentIndex = (await localTable.getNumberArray("index:Vim.BimDocument:BimDocument")); })(),
        ]);
        let warning = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            warning.push({
                index: i,
                guid: guid ? guid[i] : undefined,
                severity: severity ? severity[i] : undefined,
                description: description ? description[i] : undefined,
                bimDocumentIndex: bimDocumentIndex ? bimDocumentIndex[i] : undefined
            });
        }
        return warning;
    }
    async getGuid(warningIndex) {
        return (await this.entityTable.getString(warningIndex, "string:Guid"));
    }
    async getAllGuid() {
        return (await this.entityTable.getStringArray("string:Guid"));
    }
    async getSeverity(warningIndex) {
        return (await this.entityTable.getString(warningIndex, "string:Severity"));
    }
    async getAllSeverity() {
        return (await this.entityTable.getStringArray("string:Severity"));
    }
    async getDescription(warningIndex) {
        return (await this.entityTable.getString(warningIndex, "string:Description"));
    }
    async getAllDescription() {
        return (await this.entityTable.getStringArray("string:Description"));
    }
    async getBimDocumentIndex(warningIndex) {
        return await this.entityTable.getNumber(warningIndex, "index:Vim.BimDocument:BimDocument");
    }
    async getAllBimDocumentIndex() {
        return await this.entityTable.getNumberArray("index:Vim.BimDocument:BimDocument");
    }
    async getBimDocument(warningIndex) {
        const index = await this.getBimDocumentIndex(warningIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.bimDocument?.get(index);
    }
}
exports.WarningTable = WarningTable;
class ElementInWarning {
    static async createFromTable(table, index) {
        let result = new ElementInWarning();
        result.index = index;
        await Promise.all([
            table.getWarningIndex(index).then(v => result.warningIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.ElementInWarning = ElementInWarning;
class ElementInWarningTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.ElementInWarning");
        if (!entity) {
            return undefined;
        }
        let table = new ElementInWarningTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(elementInWarningIndex) {
        return await ElementInWarning.createFromTable(this, elementInWarningIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let warningIndex;
        let elementIndex;
        await Promise.all([
            (async () => { warningIndex = (await localTable.getNumberArray("index:Vim.Warning:Warning")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let elementInWarning = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            elementInWarning.push({
                index: i,
                warningIndex: warningIndex ? warningIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return elementInWarning;
    }
    async getWarningIndex(elementInWarningIndex) {
        return await this.entityTable.getNumber(elementInWarningIndex, "index:Vim.Warning:Warning");
    }
    async getAllWarningIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Warning:Warning");
    }
    async getWarning(elementInWarningIndex) {
        const index = await this.getWarningIndex(elementInWarningIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.warning?.get(index);
    }
    async getElementIndex(elementInWarningIndex) {
        return await this.entityTable.getNumber(elementInWarningIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(elementInWarningIndex) {
        const index = await this.getElementIndex(elementInWarningIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.ElementInWarningTable = ElementInWarningTable;
class BasePoint {
    static async createFromTable(table, index) {
        let result = new BasePoint();
        result.index = index;
        await Promise.all([
            table.getIsSurveyPoint(index).then(v => result.isSurveyPoint = v),
            table.getPosition_X(index).then(v => result.position_X = v),
            table.getPosition_Y(index).then(v => result.position_Y = v),
            table.getPosition_Z(index).then(v => result.position_Z = v),
            table.getSharedPosition_X(index).then(v => result.sharedPosition_X = v),
            table.getSharedPosition_Y(index).then(v => result.sharedPosition_Y = v),
            table.getSharedPosition_Z(index).then(v => result.sharedPosition_Z = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.BasePoint = BasePoint;
class BasePointTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.BasePoint");
        if (!entity) {
            return undefined;
        }
        let table = new BasePointTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(basePointIndex) {
        return await BasePoint.createFromTable(this, basePointIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let isSurveyPoint;
        let position_X;
        let position_Y;
        let position_Z;
        let sharedPosition_X;
        let sharedPosition_Y;
        let sharedPosition_Z;
        let elementIndex;
        await Promise.all([
            (async () => { isSurveyPoint = (await localTable.getBooleanArray("byte:IsSurveyPoint")); })(),
            (async () => { position_X = (await localTable.getNumberArray("double:Position.X")); })(),
            (async () => { position_Y = (await localTable.getNumberArray("double:Position.Y")); })(),
            (async () => { position_Z = (await localTable.getNumberArray("double:Position.Z")); })(),
            (async () => { sharedPosition_X = (await localTable.getNumberArray("double:SharedPosition.X")); })(),
            (async () => { sharedPosition_Y = (await localTable.getNumberArray("double:SharedPosition.Y")); })(),
            (async () => { sharedPosition_Z = (await localTable.getNumberArray("double:SharedPosition.Z")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let basePoint = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            basePoint.push({
                index: i,
                isSurveyPoint: isSurveyPoint ? isSurveyPoint[i] : undefined,
                position_X: position_X ? position_X[i] : undefined,
                position_Y: position_Y ? position_Y[i] : undefined,
                position_Z: position_Z ? position_Z[i] : undefined,
                sharedPosition_X: sharedPosition_X ? sharedPosition_X[i] : undefined,
                sharedPosition_Y: sharedPosition_Y ? sharedPosition_Y[i] : undefined,
                sharedPosition_Z: sharedPosition_Z ? sharedPosition_Z[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return basePoint;
    }
    async getIsSurveyPoint(basePointIndex) {
        return (await this.entityTable.getBoolean(basePointIndex, "byte:IsSurveyPoint"));
    }
    async getAllIsSurveyPoint() {
        return (await this.entityTable.getBooleanArray("byte:IsSurveyPoint"));
    }
    async getPosition_X(basePointIndex) {
        return (await this.entityTable.getNumber(basePointIndex, "double:Position.X"));
    }
    async getAllPosition_X() {
        return (await this.entityTable.getNumberArray("double:Position.X"));
    }
    async getPosition_Y(basePointIndex) {
        return (await this.entityTable.getNumber(basePointIndex, "double:Position.Y"));
    }
    async getAllPosition_Y() {
        return (await this.entityTable.getNumberArray("double:Position.Y"));
    }
    async getPosition_Z(basePointIndex) {
        return (await this.entityTable.getNumber(basePointIndex, "double:Position.Z"));
    }
    async getAllPosition_Z() {
        return (await this.entityTable.getNumberArray("double:Position.Z"));
    }
    async getSharedPosition_X(basePointIndex) {
        return (await this.entityTable.getNumber(basePointIndex, "double:SharedPosition.X"));
    }
    async getAllSharedPosition_X() {
        return (await this.entityTable.getNumberArray("double:SharedPosition.X"));
    }
    async getSharedPosition_Y(basePointIndex) {
        return (await this.entityTable.getNumber(basePointIndex, "double:SharedPosition.Y"));
    }
    async getAllSharedPosition_Y() {
        return (await this.entityTable.getNumberArray("double:SharedPosition.Y"));
    }
    async getSharedPosition_Z(basePointIndex) {
        return (await this.entityTable.getNumber(basePointIndex, "double:SharedPosition.Z"));
    }
    async getAllSharedPosition_Z() {
        return (await this.entityTable.getNumberArray("double:SharedPosition.Z"));
    }
    async getElementIndex(basePointIndex) {
        return await this.entityTable.getNumber(basePointIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(basePointIndex) {
        const index = await this.getElementIndex(basePointIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.BasePointTable = BasePointTable;
class PhaseFilter {
    static async createFromTable(table, index) {
        let result = new PhaseFilter();
        result.index = index;
        await Promise.all([
            table.getNew(index).then(v => result._new = v),
            table.getExisting(index).then(v => result.existing = v),
            table.getDemolished(index).then(v => result.demolished = v),
            table.getTemporary(index).then(v => result.temporary = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.PhaseFilter = PhaseFilter;
class PhaseFilterTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.PhaseFilter");
        if (!entity) {
            return undefined;
        }
        let table = new PhaseFilterTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(phaseFilterIndex) {
        return await PhaseFilter.createFromTable(this, phaseFilterIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let _new;
        let existing;
        let demolished;
        let temporary;
        let elementIndex;
        await Promise.all([
            (async () => { _new = (await localTable.getNumberArray("int:New")); })(),
            (async () => { existing = (await localTable.getNumberArray("int:Existing")); })(),
            (async () => { demolished = (await localTable.getNumberArray("int:Demolished")); })(),
            (async () => { temporary = (await localTable.getNumberArray("int:Temporary")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let phaseFilter = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            phaseFilter.push({
                index: i,
                _new: _new ? _new[i] : undefined,
                existing: existing ? existing[i] : undefined,
                demolished: demolished ? demolished[i] : undefined,
                temporary: temporary ? temporary[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return phaseFilter;
    }
    async getNew(phaseFilterIndex) {
        return (await this.entityTable.getNumber(phaseFilterIndex, "int:New"));
    }
    async getAllNew() {
        return (await this.entityTable.getNumberArray("int:New"));
    }
    async getExisting(phaseFilterIndex) {
        return (await this.entityTable.getNumber(phaseFilterIndex, "int:Existing"));
    }
    async getAllExisting() {
        return (await this.entityTable.getNumberArray("int:Existing"));
    }
    async getDemolished(phaseFilterIndex) {
        return (await this.entityTable.getNumber(phaseFilterIndex, "int:Demolished"));
    }
    async getAllDemolished() {
        return (await this.entityTable.getNumberArray("int:Demolished"));
    }
    async getTemporary(phaseFilterIndex) {
        return (await this.entityTable.getNumber(phaseFilterIndex, "int:Temporary"));
    }
    async getAllTemporary() {
        return (await this.entityTable.getNumberArray("int:Temporary"));
    }
    async getElementIndex(phaseFilterIndex) {
        return await this.entityTable.getNumber(phaseFilterIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(phaseFilterIndex) {
        const index = await this.getElementIndex(phaseFilterIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.PhaseFilterTable = PhaseFilterTable;
class Grid {
    static async createFromTable(table, index) {
        let result = new Grid();
        result.index = index;
        await Promise.all([
            table.getStartPoint_X(index).then(v => result.startPoint_X = v),
            table.getStartPoint_Y(index).then(v => result.startPoint_Y = v),
            table.getStartPoint_Z(index).then(v => result.startPoint_Z = v),
            table.getEndPoint_X(index).then(v => result.endPoint_X = v),
            table.getEndPoint_Y(index).then(v => result.endPoint_Y = v),
            table.getEndPoint_Z(index).then(v => result.endPoint_Z = v),
            table.getIsCurved(index).then(v => result.isCurved = v),
            table.getExtents_Min_X(index).then(v => result.extents_Min_X = v),
            table.getExtents_Min_Y(index).then(v => result.extents_Min_Y = v),
            table.getExtents_Min_Z(index).then(v => result.extents_Min_Z = v),
            table.getExtents_Max_X(index).then(v => result.extents_Max_X = v),
            table.getExtents_Max_Y(index).then(v => result.extents_Max_Y = v),
            table.getExtents_Max_Z(index).then(v => result.extents_Max_Z = v),
            table.getFamilyTypeIndex(index).then(v => result.familyTypeIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.Grid = Grid;
class GridTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.Grid");
        if (!entity) {
            return undefined;
        }
        let table = new GridTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(gridIndex) {
        return await Grid.createFromTable(this, gridIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let startPoint_X;
        let startPoint_Y;
        let startPoint_Z;
        let endPoint_X;
        let endPoint_Y;
        let endPoint_Z;
        let isCurved;
        let extents_Min_X;
        let extents_Min_Y;
        let extents_Min_Z;
        let extents_Max_X;
        let extents_Max_Y;
        let extents_Max_Z;
        let familyTypeIndex;
        let elementIndex;
        await Promise.all([
            (async () => { startPoint_X = (await localTable.getNumberArray("double:StartPoint.X")); })(),
            (async () => { startPoint_Y = (await localTable.getNumberArray("double:StartPoint.Y")); })(),
            (async () => { startPoint_Z = (await localTable.getNumberArray("double:StartPoint.Z")); })(),
            (async () => { endPoint_X = (await localTable.getNumberArray("double:EndPoint.X")); })(),
            (async () => { endPoint_Y = (await localTable.getNumberArray("double:EndPoint.Y")); })(),
            (async () => { endPoint_Z = (await localTable.getNumberArray("double:EndPoint.Z")); })(),
            (async () => { isCurved = (await localTable.getBooleanArray("byte:IsCurved")); })(),
            (async () => { extents_Min_X = (await localTable.getNumberArray("double:Extents.Min.X")); })(),
            (async () => { extents_Min_Y = (await localTable.getNumberArray("double:Extents.Min.Y")); })(),
            (async () => { extents_Min_Z = (await localTable.getNumberArray("double:Extents.Min.Z")); })(),
            (async () => { extents_Max_X = (await localTable.getNumberArray("double:Extents.Max.X")); })(),
            (async () => { extents_Max_Y = (await localTable.getNumberArray("double:Extents.Max.Y")); })(),
            (async () => { extents_Max_Z = (await localTable.getNumberArray("double:Extents.Max.Z")); })(),
            (async () => { familyTypeIndex = (await localTable.getNumberArray("index:Vim.FamilyType:FamilyType")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let grid = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            grid.push({
                index: i,
                startPoint_X: startPoint_X ? startPoint_X[i] : undefined,
                startPoint_Y: startPoint_Y ? startPoint_Y[i] : undefined,
                startPoint_Z: startPoint_Z ? startPoint_Z[i] : undefined,
                endPoint_X: endPoint_X ? endPoint_X[i] : undefined,
                endPoint_Y: endPoint_Y ? endPoint_Y[i] : undefined,
                endPoint_Z: endPoint_Z ? endPoint_Z[i] : undefined,
                isCurved: isCurved ? isCurved[i] : undefined,
                extents_Min_X: extents_Min_X ? extents_Min_X[i] : undefined,
                extents_Min_Y: extents_Min_Y ? extents_Min_Y[i] : undefined,
                extents_Min_Z: extents_Min_Z ? extents_Min_Z[i] : undefined,
                extents_Max_X: extents_Max_X ? extents_Max_X[i] : undefined,
                extents_Max_Y: extents_Max_Y ? extents_Max_Y[i] : undefined,
                extents_Max_Z: extents_Max_Z ? extents_Max_Z[i] : undefined,
                familyTypeIndex: familyTypeIndex ? familyTypeIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return grid;
    }
    async getStartPoint_X(gridIndex) {
        return (await this.entityTable.getNumber(gridIndex, "double:StartPoint.X"));
    }
    async getAllStartPoint_X() {
        return (await this.entityTable.getNumberArray("double:StartPoint.X"));
    }
    async getStartPoint_Y(gridIndex) {
        return (await this.entityTable.getNumber(gridIndex, "double:StartPoint.Y"));
    }
    async getAllStartPoint_Y() {
        return (await this.entityTable.getNumberArray("double:StartPoint.Y"));
    }
    async getStartPoint_Z(gridIndex) {
        return (await this.entityTable.getNumber(gridIndex, "double:StartPoint.Z"));
    }
    async getAllStartPoint_Z() {
        return (await this.entityTable.getNumberArray("double:StartPoint.Z"));
    }
    async getEndPoint_X(gridIndex) {
        return (await this.entityTable.getNumber(gridIndex, "double:EndPoint.X"));
    }
    async getAllEndPoint_X() {
        return (await this.entityTable.getNumberArray("double:EndPoint.X"));
    }
    async getEndPoint_Y(gridIndex) {
        return (await this.entityTable.getNumber(gridIndex, "double:EndPoint.Y"));
    }
    async getAllEndPoint_Y() {
        return (await this.entityTable.getNumberArray("double:EndPoint.Y"));
    }
    async getEndPoint_Z(gridIndex) {
        return (await this.entityTable.getNumber(gridIndex, "double:EndPoint.Z"));
    }
    async getAllEndPoint_Z() {
        return (await this.entityTable.getNumberArray("double:EndPoint.Z"));
    }
    async getIsCurved(gridIndex) {
        return (await this.entityTable.getBoolean(gridIndex, "byte:IsCurved"));
    }
    async getAllIsCurved() {
        return (await this.entityTable.getBooleanArray("byte:IsCurved"));
    }
    async getExtents_Min_X(gridIndex) {
        return (await this.entityTable.getNumber(gridIndex, "double:Extents.Min.X"));
    }
    async getAllExtents_Min_X() {
        return (await this.entityTable.getNumberArray("double:Extents.Min.X"));
    }
    async getExtents_Min_Y(gridIndex) {
        return (await this.entityTable.getNumber(gridIndex, "double:Extents.Min.Y"));
    }
    async getAllExtents_Min_Y() {
        return (await this.entityTable.getNumberArray("double:Extents.Min.Y"));
    }
    async getExtents_Min_Z(gridIndex) {
        return (await this.entityTable.getNumber(gridIndex, "double:Extents.Min.Z"));
    }
    async getAllExtents_Min_Z() {
        return (await this.entityTable.getNumberArray("double:Extents.Min.Z"));
    }
    async getExtents_Max_X(gridIndex) {
        return (await this.entityTable.getNumber(gridIndex, "double:Extents.Max.X"));
    }
    async getAllExtents_Max_X() {
        return (await this.entityTable.getNumberArray("double:Extents.Max.X"));
    }
    async getExtents_Max_Y(gridIndex) {
        return (await this.entityTable.getNumber(gridIndex, "double:Extents.Max.Y"));
    }
    async getAllExtents_Max_Y() {
        return (await this.entityTable.getNumberArray("double:Extents.Max.Y"));
    }
    async getExtents_Max_Z(gridIndex) {
        return (await this.entityTable.getNumber(gridIndex, "double:Extents.Max.Z"));
    }
    async getAllExtents_Max_Z() {
        return (await this.entityTable.getNumberArray("double:Extents.Max.Z"));
    }
    async getFamilyTypeIndex(gridIndex) {
        return await this.entityTable.getNumber(gridIndex, "index:Vim.FamilyType:FamilyType");
    }
    async getAllFamilyTypeIndex() {
        return await this.entityTable.getNumberArray("index:Vim.FamilyType:FamilyType");
    }
    async getFamilyType(gridIndex) {
        const index = await this.getFamilyTypeIndex(gridIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.familyType?.get(index);
    }
    async getElementIndex(gridIndex) {
        return await this.entityTable.getNumber(gridIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(gridIndex) {
        const index = await this.getElementIndex(gridIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.GridTable = GridTable;
class Area {
    static async createFromTable(table, index) {
        let result = new Area();
        result.index = index;
        await Promise.all([
            table.getValue(index).then(v => result.value = v),
            table.getPerimeter(index).then(v => result.perimeter = v),
            table.getNumber(index).then(v => result.number = v),
            table.getIsGrossInterior(index).then(v => result.isGrossInterior = v),
            table.getAreaSchemeIndex(index).then(v => result.areaSchemeIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.Area = Area;
class AreaTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.Area");
        if (!entity) {
            return undefined;
        }
        let table = new AreaTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(areaIndex) {
        return await Area.createFromTable(this, areaIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let value;
        let perimeter;
        let number;
        let isGrossInterior;
        let areaSchemeIndex;
        let elementIndex;
        await Promise.all([
            (async () => { value = (await localTable.getNumberArray("double:Value")); })(),
            (async () => { perimeter = (await localTable.getNumberArray("double:Perimeter")); })(),
            (async () => { number = (await localTable.getStringArray("string:Number")); })(),
            (async () => { isGrossInterior = (await localTable.getBooleanArray("byte:IsGrossInterior")); })(),
            (async () => { areaSchemeIndex = (await localTable.getNumberArray("index:Vim.AreaScheme:AreaScheme")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let area = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            area.push({
                index: i,
                value: value ? value[i] : undefined,
                perimeter: perimeter ? perimeter[i] : undefined,
                number: number ? number[i] : undefined,
                isGrossInterior: isGrossInterior ? isGrossInterior[i] : undefined,
                areaSchemeIndex: areaSchemeIndex ? areaSchemeIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return area;
    }
    async getValue(areaIndex) {
        return (await this.entityTable.getNumber(areaIndex, "double:Value"));
    }
    async getAllValue() {
        return (await this.entityTable.getNumberArray("double:Value"));
    }
    async getPerimeter(areaIndex) {
        return (await this.entityTable.getNumber(areaIndex, "double:Perimeter"));
    }
    async getAllPerimeter() {
        return (await this.entityTable.getNumberArray("double:Perimeter"));
    }
    async getNumber(areaIndex) {
        return (await this.entityTable.getString(areaIndex, "string:Number"));
    }
    async getAllNumber() {
        return (await this.entityTable.getStringArray("string:Number"));
    }
    async getIsGrossInterior(areaIndex) {
        return (await this.entityTable.getBoolean(areaIndex, "byte:IsGrossInterior"));
    }
    async getAllIsGrossInterior() {
        return (await this.entityTable.getBooleanArray("byte:IsGrossInterior"));
    }
    async getAreaSchemeIndex(areaIndex) {
        return await this.entityTable.getNumber(areaIndex, "index:Vim.AreaScheme:AreaScheme");
    }
    async getAllAreaSchemeIndex() {
        return await this.entityTable.getNumberArray("index:Vim.AreaScheme:AreaScheme");
    }
    async getAreaScheme(areaIndex) {
        const index = await this.getAreaSchemeIndex(areaIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.areaScheme?.get(index);
    }
    async getElementIndex(areaIndex) {
        return await this.entityTable.getNumber(areaIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(areaIndex) {
        const index = await this.getElementIndex(areaIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.AreaTable = AreaTable;
class AreaScheme {
    static async createFromTable(table, index) {
        let result = new AreaScheme();
        result.index = index;
        await Promise.all([
            table.getIsGrossBuildingArea(index).then(v => result.isGrossBuildingArea = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.AreaScheme = AreaScheme;
class AreaSchemeTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.AreaScheme");
        if (!entity) {
            return undefined;
        }
        let table = new AreaSchemeTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(areaSchemeIndex) {
        return await AreaScheme.createFromTable(this, areaSchemeIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let isGrossBuildingArea;
        let elementIndex;
        await Promise.all([
            (async () => { isGrossBuildingArea = (await localTable.getBooleanArray("byte:IsGrossBuildingArea")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let areaScheme = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            areaScheme.push({
                index: i,
                isGrossBuildingArea: isGrossBuildingArea ? isGrossBuildingArea[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return areaScheme;
    }
    async getIsGrossBuildingArea(areaSchemeIndex) {
        return (await this.entityTable.getBoolean(areaSchemeIndex, "byte:IsGrossBuildingArea"));
    }
    async getAllIsGrossBuildingArea() {
        return (await this.entityTable.getBooleanArray("byte:IsGrossBuildingArea"));
    }
    async getElementIndex(areaSchemeIndex) {
        return await this.entityTable.getNumber(areaSchemeIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(areaSchemeIndex) {
        const index = await this.getElementIndex(areaSchemeIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.AreaSchemeTable = AreaSchemeTable;
class Schedule {
    static async createFromTable(table, index) {
        let result = new Schedule();
        result.index = index;
        await Promise.all([
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.Schedule = Schedule;
class ScheduleTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.Schedule");
        if (!entity) {
            return undefined;
        }
        let table = new ScheduleTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(scheduleIndex) {
        return await Schedule.createFromTable(this, scheduleIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let elementIndex;
        await Promise.all([
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let schedule = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            schedule.push({
                index: i,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return schedule;
    }
    async getElementIndex(scheduleIndex) {
        return await this.entityTable.getNumber(scheduleIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(scheduleIndex) {
        const index = await this.getElementIndex(scheduleIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.ScheduleTable = ScheduleTable;
class ScheduleColumn {
    static async createFromTable(table, index) {
        let result = new ScheduleColumn();
        result.index = index;
        await Promise.all([
            table.getName(index).then(v => result.name = v),
            table.getColumnIndex(index).then(v => result.columnIndex = v),
            table.getScheduleIndex(index).then(v => result.scheduleIndex = v),
        ]);
        return result;
    }
}
exports.ScheduleColumn = ScheduleColumn;
class ScheduleColumnTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.ScheduleColumn");
        if (!entity) {
            return undefined;
        }
        let table = new ScheduleColumnTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(scheduleColumnIndex) {
        return await ScheduleColumn.createFromTable(this, scheduleColumnIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let name;
        let columnIndex;
        let scheduleIndex;
        await Promise.all([
            (async () => { name = (await localTable.getStringArray("string:Name")); })(),
            (async () => { columnIndex = (await localTable.getNumberArray("int:ColumnIndex")); })(),
            (async () => { scheduleIndex = (await localTable.getNumberArray("index:Vim.Schedule:Schedule")); })(),
        ]);
        let scheduleColumn = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            scheduleColumn.push({
                index: i,
                name: name ? name[i] : undefined,
                columnIndex: columnIndex ? columnIndex[i] : undefined,
                scheduleIndex: scheduleIndex ? scheduleIndex[i] : undefined
            });
        }
        return scheduleColumn;
    }
    async getName(scheduleColumnIndex) {
        return (await this.entityTable.getString(scheduleColumnIndex, "string:Name"));
    }
    async getAllName() {
        return (await this.entityTable.getStringArray("string:Name"));
    }
    async getColumnIndex(scheduleColumnIndex) {
        return (await this.entityTable.getNumber(scheduleColumnIndex, "int:ColumnIndex"));
    }
    async getAllColumnIndex() {
        return (await this.entityTable.getNumberArray("int:ColumnIndex"));
    }
    async getScheduleIndex(scheduleColumnIndex) {
        return await this.entityTable.getNumber(scheduleColumnIndex, "index:Vim.Schedule:Schedule");
    }
    async getAllScheduleIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Schedule:Schedule");
    }
    async getSchedule(scheduleColumnIndex) {
        const index = await this.getScheduleIndex(scheduleColumnIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.schedule?.get(index);
    }
}
exports.ScheduleColumnTable = ScheduleColumnTable;
class ScheduleCell {
    static async createFromTable(table, index) {
        let result = new ScheduleCell();
        result.index = index;
        await Promise.all([
            table.getValue(index).then(v => result.value = v),
            table.getRowIndex(index).then(v => result.rowIndex = v),
            table.getScheduleColumnIndex(index).then(v => result.scheduleColumnIndex = v),
        ]);
        return result;
    }
}
exports.ScheduleCell = ScheduleCell;
class ScheduleCellTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.ScheduleCell");
        if (!entity) {
            return undefined;
        }
        let table = new ScheduleCellTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(scheduleCellIndex) {
        return await ScheduleCell.createFromTable(this, scheduleCellIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let value;
        let rowIndex;
        let scheduleColumnIndex;
        await Promise.all([
            (async () => { value = (await localTable.getStringArray("string:Value")); })(),
            (async () => { rowIndex = (await localTable.getNumberArray("int:RowIndex")); })(),
            (async () => { scheduleColumnIndex = (await localTable.getNumberArray("index:Vim.ScheduleColumn:ScheduleColumn")); })(),
        ]);
        let scheduleCell = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            scheduleCell.push({
                index: i,
                value: value ? value[i] : undefined,
                rowIndex: rowIndex ? rowIndex[i] : undefined,
                scheduleColumnIndex: scheduleColumnIndex ? scheduleColumnIndex[i] : undefined
            });
        }
        return scheduleCell;
    }
    async getValue(scheduleCellIndex) {
        return (await this.entityTable.getString(scheduleCellIndex, "string:Value"));
    }
    async getAllValue() {
        return (await this.entityTable.getStringArray("string:Value"));
    }
    async getRowIndex(scheduleCellIndex) {
        return (await this.entityTable.getNumber(scheduleCellIndex, "int:RowIndex"));
    }
    async getAllRowIndex() {
        return (await this.entityTable.getNumberArray("int:RowIndex"));
    }
    async getScheduleColumnIndex(scheduleCellIndex) {
        return await this.entityTable.getNumber(scheduleCellIndex, "index:Vim.ScheduleColumn:ScheduleColumn");
    }
    async getAllScheduleColumnIndex() {
        return await this.entityTable.getNumberArray("index:Vim.ScheduleColumn:ScheduleColumn");
    }
    async getScheduleColumn(scheduleCellIndex) {
        const index = await this.getScheduleColumnIndex(scheduleCellIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.scheduleColumn?.get(index);
    }
}
exports.ScheduleCellTable = ScheduleCellTable;
class ViewSheetSet {
    static async createFromTable(table, index) {
        let result = new ViewSheetSet();
        result.index = index;
        await Promise.all([
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.ViewSheetSet = ViewSheetSet;
class ViewSheetSetTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.ViewSheetSet");
        if (!entity) {
            return undefined;
        }
        let table = new ViewSheetSetTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(viewSheetSetIndex) {
        return await ViewSheetSet.createFromTable(this, viewSheetSetIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let elementIndex;
        await Promise.all([
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let viewSheetSet = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            viewSheetSet.push({
                index: i,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return viewSheetSet;
    }
    async getElementIndex(viewSheetSetIndex) {
        return await this.entityTable.getNumber(viewSheetSetIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(viewSheetSetIndex) {
        const index = await this.getElementIndex(viewSheetSetIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.ViewSheetSetTable = ViewSheetSetTable;
class ViewSheet {
    static async createFromTable(table, index) {
        let result = new ViewSheet();
        result.index = index;
        await Promise.all([
            table.getFamilyTypeIndex(index).then(v => result.familyTypeIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.ViewSheet = ViewSheet;
class ViewSheetTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.ViewSheet");
        if (!entity) {
            return undefined;
        }
        let table = new ViewSheetTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(viewSheetIndex) {
        return await ViewSheet.createFromTable(this, viewSheetIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let familyTypeIndex;
        let elementIndex;
        await Promise.all([
            (async () => { familyTypeIndex = (await localTable.getNumberArray("index:Vim.FamilyType:FamilyType")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let viewSheet = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            viewSheet.push({
                index: i,
                familyTypeIndex: familyTypeIndex ? familyTypeIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return viewSheet;
    }
    async getFamilyTypeIndex(viewSheetIndex) {
        return await this.entityTable.getNumber(viewSheetIndex, "index:Vim.FamilyType:FamilyType");
    }
    async getAllFamilyTypeIndex() {
        return await this.entityTable.getNumberArray("index:Vim.FamilyType:FamilyType");
    }
    async getFamilyType(viewSheetIndex) {
        const index = await this.getFamilyTypeIndex(viewSheetIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.familyType?.get(index);
    }
    async getElementIndex(viewSheetIndex) {
        return await this.entityTable.getNumber(viewSheetIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(viewSheetIndex) {
        const index = await this.getElementIndex(viewSheetIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.ViewSheetTable = ViewSheetTable;
class ViewSheetInViewSheetSet {
    static async createFromTable(table, index) {
        let result = new ViewSheetInViewSheetSet();
        result.index = index;
        await Promise.all([
            table.getViewSheetIndex(index).then(v => result.viewSheetIndex = v),
            table.getViewSheetSetIndex(index).then(v => result.viewSheetSetIndex = v),
        ]);
        return result;
    }
}
exports.ViewSheetInViewSheetSet = ViewSheetInViewSheetSet;
class ViewSheetInViewSheetSetTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.ViewSheetInViewSheetSet");
        if (!entity) {
            return undefined;
        }
        let table = new ViewSheetInViewSheetSetTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(viewSheetInViewSheetSetIndex) {
        return await ViewSheetInViewSheetSet.createFromTable(this, viewSheetInViewSheetSetIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let viewSheetIndex;
        let viewSheetSetIndex;
        await Promise.all([
            (async () => { viewSheetIndex = (await localTable.getNumberArray("index:Vim.ViewSheet:ViewSheet")); })(),
            (async () => { viewSheetSetIndex = (await localTable.getNumberArray("index:Vim.ViewSheetSet:ViewSheetSet")); })(),
        ]);
        let viewSheetInViewSheetSet = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            viewSheetInViewSheetSet.push({
                index: i,
                viewSheetIndex: viewSheetIndex ? viewSheetIndex[i] : undefined,
                viewSheetSetIndex: viewSheetSetIndex ? viewSheetSetIndex[i] : undefined
            });
        }
        return viewSheetInViewSheetSet;
    }
    async getViewSheetIndex(viewSheetInViewSheetSetIndex) {
        return await this.entityTable.getNumber(viewSheetInViewSheetSetIndex, "index:Vim.ViewSheet:ViewSheet");
    }
    async getAllViewSheetIndex() {
        return await this.entityTable.getNumberArray("index:Vim.ViewSheet:ViewSheet");
    }
    async getViewSheet(viewSheetInViewSheetSetIndex) {
        const index = await this.getViewSheetIndex(viewSheetInViewSheetSetIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.viewSheet?.get(index);
    }
    async getViewSheetSetIndex(viewSheetInViewSheetSetIndex) {
        return await this.entityTable.getNumber(viewSheetInViewSheetSetIndex, "index:Vim.ViewSheetSet:ViewSheetSet");
    }
    async getAllViewSheetSetIndex() {
        return await this.entityTable.getNumberArray("index:Vim.ViewSheetSet:ViewSheetSet");
    }
    async getViewSheetSet(viewSheetInViewSheetSetIndex) {
        const index = await this.getViewSheetSetIndex(viewSheetInViewSheetSetIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.viewSheetSet?.get(index);
    }
}
exports.ViewSheetInViewSheetSetTable = ViewSheetInViewSheetSetTable;
class ViewInViewSheetSet {
    static async createFromTable(table, index) {
        let result = new ViewInViewSheetSet();
        result.index = index;
        await Promise.all([
            table.getViewIndex(index).then(v => result.viewIndex = v),
            table.getViewSheetSetIndex(index).then(v => result.viewSheetSetIndex = v),
        ]);
        return result;
    }
}
exports.ViewInViewSheetSet = ViewInViewSheetSet;
class ViewInViewSheetSetTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.ViewInViewSheetSet");
        if (!entity) {
            return undefined;
        }
        let table = new ViewInViewSheetSetTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(viewInViewSheetSetIndex) {
        return await ViewInViewSheetSet.createFromTable(this, viewInViewSheetSetIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let viewIndex;
        let viewSheetSetIndex;
        await Promise.all([
            (async () => { viewIndex = (await localTable.getNumberArray("index:Vim.View:View")); })(),
            (async () => { viewSheetSetIndex = (await localTable.getNumberArray("index:Vim.ViewSheetSet:ViewSheetSet")); })(),
        ]);
        let viewInViewSheetSet = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            viewInViewSheetSet.push({
                index: i,
                viewIndex: viewIndex ? viewIndex[i] : undefined,
                viewSheetSetIndex: viewSheetSetIndex ? viewSheetSetIndex[i] : undefined
            });
        }
        return viewInViewSheetSet;
    }
    async getViewIndex(viewInViewSheetSetIndex) {
        return await this.entityTable.getNumber(viewInViewSheetSetIndex, "index:Vim.View:View");
    }
    async getAllViewIndex() {
        return await this.entityTable.getNumberArray("index:Vim.View:View");
    }
    async getView(viewInViewSheetSetIndex) {
        const index = await this.getViewIndex(viewInViewSheetSetIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.view?.get(index);
    }
    async getViewSheetSetIndex(viewInViewSheetSetIndex) {
        return await this.entityTable.getNumber(viewInViewSheetSetIndex, "index:Vim.ViewSheetSet:ViewSheetSet");
    }
    async getAllViewSheetSetIndex() {
        return await this.entityTable.getNumberArray("index:Vim.ViewSheetSet:ViewSheetSet");
    }
    async getViewSheetSet(viewInViewSheetSetIndex) {
        const index = await this.getViewSheetSetIndex(viewInViewSheetSetIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.viewSheetSet?.get(index);
    }
}
exports.ViewInViewSheetSetTable = ViewInViewSheetSetTable;
class ViewInViewSheet {
    static async createFromTable(table, index) {
        let result = new ViewInViewSheet();
        result.index = index;
        await Promise.all([
            table.getViewIndex(index).then(v => result.viewIndex = v),
            table.getViewSheetIndex(index).then(v => result.viewSheetIndex = v),
        ]);
        return result;
    }
}
exports.ViewInViewSheet = ViewInViewSheet;
class ViewInViewSheetTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.ViewInViewSheet");
        if (!entity) {
            return undefined;
        }
        let table = new ViewInViewSheetTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(viewInViewSheetIndex) {
        return await ViewInViewSheet.createFromTable(this, viewInViewSheetIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let viewIndex;
        let viewSheetIndex;
        await Promise.all([
            (async () => { viewIndex = (await localTable.getNumberArray("index:Vim.View:View")); })(),
            (async () => { viewSheetIndex = (await localTable.getNumberArray("index:Vim.ViewSheet:ViewSheet")); })(),
        ]);
        let viewInViewSheet = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            viewInViewSheet.push({
                index: i,
                viewIndex: viewIndex ? viewIndex[i] : undefined,
                viewSheetIndex: viewSheetIndex ? viewSheetIndex[i] : undefined
            });
        }
        return viewInViewSheet;
    }
    async getViewIndex(viewInViewSheetIndex) {
        return await this.entityTable.getNumber(viewInViewSheetIndex, "index:Vim.View:View");
    }
    async getAllViewIndex() {
        return await this.entityTable.getNumberArray("index:Vim.View:View");
    }
    async getView(viewInViewSheetIndex) {
        const index = await this.getViewIndex(viewInViewSheetIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.view?.get(index);
    }
    async getViewSheetIndex(viewInViewSheetIndex) {
        return await this.entityTable.getNumber(viewInViewSheetIndex, "index:Vim.ViewSheet:ViewSheet");
    }
    async getAllViewSheetIndex() {
        return await this.entityTable.getNumberArray("index:Vim.ViewSheet:ViewSheet");
    }
    async getViewSheet(viewInViewSheetIndex) {
        const index = await this.getViewSheetIndex(viewInViewSheetIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.viewSheet?.get(index);
    }
}
exports.ViewInViewSheetTable = ViewInViewSheetTable;
class Site {
    static async createFromTable(table, index) {
        let result = new Site();
        result.index = index;
        await Promise.all([
            table.getLatitude(index).then(v => result.latitude = v),
            table.getLongitude(index).then(v => result.longitude = v),
            table.getAddress(index).then(v => result.address = v),
            table.getElevation(index).then(v => result.elevation = v),
            table.getNumber(index).then(v => result.number = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.Site = Site;
class SiteTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.Site");
        if (!entity) {
            return undefined;
        }
        let table = new SiteTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(siteIndex) {
        return await Site.createFromTable(this, siteIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let latitude;
        let longitude;
        let address;
        let elevation;
        let number;
        let elementIndex;
        await Promise.all([
            (async () => { latitude = (await localTable.getNumberArray("double:Latitude")); })(),
            (async () => { longitude = (await localTable.getNumberArray("double:Longitude")); })(),
            (async () => { address = (await localTable.getStringArray("string:Address")); })(),
            (async () => { elevation = (await localTable.getNumberArray("double:Elevation")); })(),
            (async () => { number = (await localTable.getStringArray("string:Number")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let site = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            site.push({
                index: i,
                latitude: latitude ? latitude[i] : undefined,
                longitude: longitude ? longitude[i] : undefined,
                address: address ? address[i] : undefined,
                elevation: elevation ? elevation[i] : undefined,
                number: number ? number[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return site;
    }
    async getLatitude(siteIndex) {
        return (await this.entityTable.getNumber(siteIndex, "double:Latitude"));
    }
    async getAllLatitude() {
        return (await this.entityTable.getNumberArray("double:Latitude"));
    }
    async getLongitude(siteIndex) {
        return (await this.entityTable.getNumber(siteIndex, "double:Longitude"));
    }
    async getAllLongitude() {
        return (await this.entityTable.getNumberArray("double:Longitude"));
    }
    async getAddress(siteIndex) {
        return (await this.entityTable.getString(siteIndex, "string:Address"));
    }
    async getAllAddress() {
        return (await this.entityTable.getStringArray("string:Address"));
    }
    async getElevation(siteIndex) {
        return (await this.entityTable.getNumber(siteIndex, "double:Elevation"));
    }
    async getAllElevation() {
        return (await this.entityTable.getNumberArray("double:Elevation"));
    }
    async getNumber(siteIndex) {
        return (await this.entityTable.getString(siteIndex, "string:Number"));
    }
    async getAllNumber() {
        return (await this.entityTable.getStringArray("string:Number"));
    }
    async getElementIndex(siteIndex) {
        return await this.entityTable.getNumber(siteIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(siteIndex) {
        const index = await this.getElementIndex(siteIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.SiteTable = SiteTable;
class Building {
    static async createFromTable(table, index) {
        let result = new Building();
        result.index = index;
        await Promise.all([
            table.getElevation(index).then(v => result.elevation = v),
            table.getTerrainElevation(index).then(v => result.terrainElevation = v),
            table.getAddress(index).then(v => result.address = v),
            table.getSiteIndex(index).then(v => result.siteIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ]);
        return result;
    }
}
exports.Building = Building;
class BuildingTable {
    static async createFromDocument(document) {
        const entity = await document.entities.getBfast("Vim.Building");
        if (!entity) {
            return undefined;
        }
        let table = new BuildingTable();
        table.document = document;
        table.entityTable = new entityTable_1.EntityTable(entity, document.strings);
        return table;
    }
    getCount() {
        return this.entityTable.getCount();
    }
    async get(buildingIndex) {
        return await Building.createFromTable(this, buildingIndex);
    }
    async getAll() {
        const localTable = await this.entityTable.getLocal();
        let elevation;
        let terrainElevation;
        let address;
        let siteIndex;
        let elementIndex;
        await Promise.all([
            (async () => { elevation = (await localTable.getNumberArray("double:Elevation")); })(),
            (async () => { terrainElevation = (await localTable.getNumberArray("double:TerrainElevation")); })(),
            (async () => { address = (await localTable.getStringArray("string:Address")); })(),
            (async () => { siteIndex = (await localTable.getNumberArray("index:Vim.Site:Site")); })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")); })(),
        ]);
        let building = [];
        const rowCount = await this.getCount();
        for (let i = 0; i < rowCount; i++) {
            building.push({
                index: i,
                elevation: elevation ? elevation[i] : undefined,
                terrainElevation: terrainElevation ? terrainElevation[i] : undefined,
                address: address ? address[i] : undefined,
                siteIndex: siteIndex ? siteIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            });
        }
        return building;
    }
    async getElevation(buildingIndex) {
        return (await this.entityTable.getNumber(buildingIndex, "double:Elevation"));
    }
    async getAllElevation() {
        return (await this.entityTable.getNumberArray("double:Elevation"));
    }
    async getTerrainElevation(buildingIndex) {
        return (await this.entityTable.getNumber(buildingIndex, "double:TerrainElevation"));
    }
    async getAllTerrainElevation() {
        return (await this.entityTable.getNumberArray("double:TerrainElevation"));
    }
    async getAddress(buildingIndex) {
        return (await this.entityTable.getString(buildingIndex, "string:Address"));
    }
    async getAllAddress() {
        return (await this.entityTable.getStringArray("string:Address"));
    }
    async getSiteIndex(buildingIndex) {
        return await this.entityTable.getNumber(buildingIndex, "index:Vim.Site:Site");
    }
    async getAllSiteIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Site:Site");
    }
    async getSite(buildingIndex) {
        const index = await this.getSiteIndex(buildingIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.site?.get(index);
    }
    async getElementIndex(buildingIndex) {
        return await this.entityTable.getNumber(buildingIndex, "index:Vim.Element:Element");
    }
    async getAllElementIndex() {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element");
    }
    async getElement(buildingIndex) {
        const index = await this.getElementIndex(buildingIndex);
        if (index === undefined) {
            return undefined;
        }
        return await this.document.element?.get(index);
    }
}
exports.BuildingTable = BuildingTable;
class VimDocument {
    constructor(entities, strings) {
        this.entities = entities;
        this.strings = strings;
    }
    static async createFromBfast(bfast, download, ignoreStrings = false) {
        const loaded = await vimLoader_1.VimLoader.loadFromBfast(bfast, download, ignoreStrings);
        if (loaded[0] === undefined)
            return undefined;
        let doc = new VimDocument(loaded[0], loaded[1]);
        doc.asset = await AssetTable.createFromDocument(doc);
        doc.displayUnit = await DisplayUnitTable.createFromDocument(doc);
        doc.parameterDescriptor = await ParameterDescriptorTable.createFromDocument(doc);
        doc.parameter = await ParameterTable.createFromDocument(doc);
        doc.element = await ElementTable.createFromDocument(doc);
        doc.workset = await WorksetTable.createFromDocument(doc);
        doc.assemblyInstance = await AssemblyInstanceTable.createFromDocument(doc);
        doc.group = await GroupTable.createFromDocument(doc);
        doc.designOption = await DesignOptionTable.createFromDocument(doc);
        doc.level = await LevelTable.createFromDocument(doc);
        doc.phase = await PhaseTable.createFromDocument(doc);
        doc.room = await RoomTable.createFromDocument(doc);
        doc.bimDocument = await BimDocumentTable.createFromDocument(doc);
        doc.displayUnitInBimDocument = await DisplayUnitInBimDocumentTable.createFromDocument(doc);
        doc.phaseOrderInBimDocument = await PhaseOrderInBimDocumentTable.createFromDocument(doc);
        doc.category = await CategoryTable.createFromDocument(doc);
        doc.family = await FamilyTable.createFromDocument(doc);
        doc.familyType = await FamilyTypeTable.createFromDocument(doc);
        doc.familyInstance = await FamilyInstanceTable.createFromDocument(doc);
        doc.view = await ViewTable.createFromDocument(doc);
        doc.elementInView = await ElementInViewTable.createFromDocument(doc);
        doc.shapeInView = await ShapeInViewTable.createFromDocument(doc);
        doc.assetInView = await AssetInViewTable.createFromDocument(doc);
        doc.assetInViewSheet = await AssetInViewSheetTable.createFromDocument(doc);
        doc.levelInView = await LevelInViewTable.createFromDocument(doc);
        doc.camera = await CameraTable.createFromDocument(doc);
        doc.material = await MaterialTable.createFromDocument(doc);
        doc.materialInElement = await MaterialInElementTable.createFromDocument(doc);
        doc.compoundStructureLayer = await CompoundStructureLayerTable.createFromDocument(doc);
        doc.compoundStructure = await CompoundStructureTable.createFromDocument(doc);
        doc.node = await NodeTable.createFromDocument(doc);
        doc.geometry = await GeometryTable.createFromDocument(doc);
        doc.shape = await ShapeTable.createFromDocument(doc);
        doc.shapeCollection = await ShapeCollectionTable.createFromDocument(doc);
        doc.shapeInShapeCollection = await ShapeInShapeCollectionTable.createFromDocument(doc);
        doc.system = await SystemTable.createFromDocument(doc);
        doc.elementInSystem = await ElementInSystemTable.createFromDocument(doc);
        doc.warning = await WarningTable.createFromDocument(doc);
        doc.elementInWarning = await ElementInWarningTable.createFromDocument(doc);
        doc.basePoint = await BasePointTable.createFromDocument(doc);
        doc.phaseFilter = await PhaseFilterTable.createFromDocument(doc);
        doc.grid = await GridTable.createFromDocument(doc);
        doc.area = await AreaTable.createFromDocument(doc);
        doc.areaScheme = await AreaSchemeTable.createFromDocument(doc);
        doc.schedule = await ScheduleTable.createFromDocument(doc);
        doc.scheduleColumn = await ScheduleColumnTable.createFromDocument(doc);
        doc.scheduleCell = await ScheduleCellTable.createFromDocument(doc);
        doc.viewSheetSet = await ViewSheetSetTable.createFromDocument(doc);
        doc.viewSheet = await ViewSheetTable.createFromDocument(doc);
        doc.viewSheetInViewSheetSet = await ViewSheetInViewSheetSetTable.createFromDocument(doc);
        doc.viewInViewSheetSet = await ViewInViewSheetSetTable.createFromDocument(doc);
        doc.viewInViewSheet = await ViewInViewSheetTable.createFromDocument(doc);
        doc.site = await SiteTable.createFromDocument(doc);
        doc.building = await BuildingTable.createFromDocument(doc);
        return doc;
    }
}
exports.VimDocument = VimDocument;
