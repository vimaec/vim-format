// AUTO-GENERATED FILE, DO NOT MODIFY.
/**
 * @module vim-ts
 */
import { BFast } from "./bfast"
import { EntityTable } from "./entityTable"
import { VimLoader } from "./vimLoader"
import { Vector2, Vector3, Vector4, AABox, AABox2D, AABox4D, Matrix4x4 } from "./structures"
import * as Converters from "./converters"

export interface IAsset {
    index: number
    bufferName?: string
}

export interface IAssetTable {
    getCount(): Promise<number>
    get(assetIndex: number): Promise<IAsset>
    getAll(): Promise<IAsset[]>
    
    getBufferName(assetIndex: number): Promise<string | undefined>
    getAllBufferName(): Promise<string[] | undefined>
}

export class Asset implements IAsset {
    index: number
    bufferName?: string
    
    static async createFromTable(table: IAssetTable, index: number): Promise<IAsset> {
        let result = new Asset()
        result.index = index
        
        await Promise.all([
            table.getBufferName(index).then(v => result.bufferName = v),
        ])
        
        return result
    }
}

export class AssetTable implements IAssetTable {
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IAssetTable | undefined> {
        const entity = await document.entities.getBfast("Vim.Asset")
        
        if (!entity) {
            return undefined
        }
        
        let table = new AssetTable()
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(assetIndex: number): Promise<IAsset> {
        return await Asset.createFromTable(this, assetIndex)
    }
    
    async getAll(): Promise<IAsset[]> {
        const localTable = await this.entityTable.getLocal()
        
        let bufferName: string[] | undefined
        
        await Promise.all([
            (async () => { bufferName = (await localTable.getStringArray("string:BufferName")) })(),
        ])
        
        let asset: IAsset[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            asset.push({
                index: i,
                bufferName: bufferName ? bufferName[i] : undefined
            })
        }
        
        return asset
    }
    
    async getBufferName(assetIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(assetIndex, "string:BufferName"))
    }
    
    async getAllBufferName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:BufferName"))
    }
    
}

export interface IDisplayUnit {
    index: number
    spec?: string
    type?: string
    label?: string
}

export interface IDisplayUnitTable {
    getCount(): Promise<number>
    get(displayUnitIndex: number): Promise<IDisplayUnit>
    getAll(): Promise<IDisplayUnit[]>
    
    getSpec(displayUnitIndex: number): Promise<string | undefined>
    getAllSpec(): Promise<string[] | undefined>
    getType(displayUnitIndex: number): Promise<string | undefined>
    getAllType(): Promise<string[] | undefined>
    getLabel(displayUnitIndex: number): Promise<string | undefined>
    getAllLabel(): Promise<string[] | undefined>
}

export class DisplayUnit implements IDisplayUnit {
    index: number
    spec?: string
    type?: string
    label?: string
    
    static async createFromTable(table: IDisplayUnitTable, index: number): Promise<IDisplayUnit> {
        let result = new DisplayUnit()
        result.index = index
        
        await Promise.all([
            table.getSpec(index).then(v => result.spec = v),
            table.getType(index).then(v => result.type = v),
            table.getLabel(index).then(v => result.label = v),
        ])
        
        return result
    }
}

export class DisplayUnitTable implements IDisplayUnitTable {
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IDisplayUnitTable | undefined> {
        const entity = await document.entities.getBfast("Vim.DisplayUnit")
        
        if (!entity) {
            return undefined
        }
        
        let table = new DisplayUnitTable()
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(displayUnitIndex: number): Promise<IDisplayUnit> {
        return await DisplayUnit.createFromTable(this, displayUnitIndex)
    }
    
    async getAll(): Promise<IDisplayUnit[]> {
        const localTable = await this.entityTable.getLocal()
        
        let spec: string[] | undefined
        let type: string[] | undefined
        let label: string[] | undefined
        
        await Promise.all([
            (async () => { spec = (await localTable.getStringArray("string:Spec")) })(),
            (async () => { type = (await localTable.getStringArray("string:Type")) })(),
            (async () => { label = (await localTable.getStringArray("string:Label")) })(),
        ])
        
        let displayUnit: IDisplayUnit[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            displayUnit.push({
                index: i,
                spec: spec ? spec[i] : undefined,
                type: type ? type[i] : undefined,
                label: label ? label[i] : undefined
            })
        }
        
        return displayUnit
    }
    
    async getSpec(displayUnitIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(displayUnitIndex, "string:Spec"))
    }
    
    async getAllSpec(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Spec"))
    }
    
    async getType(displayUnitIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(displayUnitIndex, "string:Type"))
    }
    
    async getAllType(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Type"))
    }
    
    async getLabel(displayUnitIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(displayUnitIndex, "string:Label"))
    }
    
    async getAllLabel(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Label"))
    }
    
}

export interface IParameterDescriptor {
    index: number
    name?: string
    group?: string
    parameterType?: string
    isInstance?: boolean
    isShared?: boolean
    isReadOnly?: boolean
    flags?: number
    guid?: string
    storageType?: number
    
    displayUnitIndex?: number
    displayUnit?: IDisplayUnit
}

export interface IParameterDescriptorTable {
    getCount(): Promise<number>
    get(parameterDescriptorIndex: number): Promise<IParameterDescriptor>
    getAll(): Promise<IParameterDescriptor[]>
    
    getName(parameterDescriptorIndex: number): Promise<string | undefined>
    getAllName(): Promise<string[] | undefined>
    getGroup(parameterDescriptorIndex: number): Promise<string | undefined>
    getAllGroup(): Promise<string[] | undefined>
    getParameterType(parameterDescriptorIndex: number): Promise<string | undefined>
    getAllParameterType(): Promise<string[] | undefined>
    getIsInstance(parameterDescriptorIndex: number): Promise<boolean | undefined>
    getAllIsInstance(): Promise<boolean[] | undefined>
    getIsShared(parameterDescriptorIndex: number): Promise<boolean | undefined>
    getAllIsShared(): Promise<boolean[] | undefined>
    getIsReadOnly(parameterDescriptorIndex: number): Promise<boolean | undefined>
    getAllIsReadOnly(): Promise<boolean[] | undefined>
    getFlags(parameterDescriptorIndex: number): Promise<number | undefined>
    getAllFlags(): Promise<number[] | undefined>
    getGuid(parameterDescriptorIndex: number): Promise<string | undefined>
    getAllGuid(): Promise<string[] | undefined>
    getStorageType(parameterDescriptorIndex: number): Promise<number | undefined>
    getAllStorageType(): Promise<number[] | undefined>
    
    getDisplayUnitIndex(parameterDescriptorIndex: number): Promise<number | undefined>
    getAllDisplayUnitIndex(): Promise<number[] | undefined>
    getDisplayUnit(parameterDescriptorIndex: number): Promise<IDisplayUnit | undefined>
}

export class ParameterDescriptor implements IParameterDescriptor {
    index: number
    name?: string
    group?: string
    parameterType?: string
    isInstance?: boolean
    isShared?: boolean
    isReadOnly?: boolean
    flags?: number
    guid?: string
    storageType?: number
    
    displayUnitIndex?: number
    displayUnit?: IDisplayUnit
    
    static async createFromTable(table: IParameterDescriptorTable, index: number): Promise<IParameterDescriptor> {
        let result = new ParameterDescriptor()
        result.index = index
        
        await Promise.all([
            table.getName(index).then(v => result.name = v),
            table.getGroup(index).then(v => result.group = v),
            table.getParameterType(index).then(v => result.parameterType = v),
            table.getIsInstance(index).then(v => result.isInstance = v),
            table.getIsShared(index).then(v => result.isShared = v),
            table.getIsReadOnly(index).then(v => result.isReadOnly = v),
            table.getFlags(index).then(v => result.flags = v),
            table.getGuid(index).then(v => result.guid = v),
            table.getStorageType(index).then(v => result.storageType = v),
            table.getDisplayUnitIndex(index).then(v => result.displayUnitIndex = v),
        ])
        
        return result
    }
}

export class ParameterDescriptorTable implements IParameterDescriptorTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IParameterDescriptorTable | undefined> {
        const entity = await document.entities.getBfast("Vim.ParameterDescriptor")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ParameterDescriptorTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(parameterDescriptorIndex: number): Promise<IParameterDescriptor> {
        return await ParameterDescriptor.createFromTable(this, parameterDescriptorIndex)
    }
    
    async getAll(): Promise<IParameterDescriptor[]> {
        const localTable = await this.entityTable.getLocal()
        
        let name: string[] | undefined
        let group: string[] | undefined
        let parameterType: string[] | undefined
        let isInstance: boolean[] | undefined
        let isShared: boolean[] | undefined
        let isReadOnly: boolean[] | undefined
        let flags: number[] | undefined
        let guid: string[] | undefined
        let storageType: number[] | undefined
        let displayUnitIndex: number[] | undefined
        
        await Promise.all([
            (async () => { name = (await localTable.getStringArray("string:Name")) })(),
            (async () => { group = (await localTable.getStringArray("string:Group")) })(),
            (async () => { parameterType = (await localTable.getStringArray("string:ParameterType")) })(),
            (async () => { isInstance = (await localTable.getBooleanArray("byte:IsInstance")) })(),
            (async () => { isShared = (await localTable.getBooleanArray("byte:IsShared")) })(),
            (async () => { isReadOnly = (await localTable.getBooleanArray("byte:IsReadOnly")) })(),
            (async () => { flags = (await localTable.getNumberArray("int:Flags")) })(),
            (async () => { guid = (await localTable.getStringArray("string:Guid")) })(),
            (async () => { storageType = (await localTable.getNumberArray("int:StorageType")) })(),
            (async () => { displayUnitIndex = (await localTable.getNumberArray("index:Vim.DisplayUnit:DisplayUnit")) })(),
        ])
        
        let parameterDescriptor: IParameterDescriptor[] = []
        
        const rowCount = await this.getCount()
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
                storageType: storageType ? storageType[i] : undefined,
                displayUnitIndex: displayUnitIndex ? displayUnitIndex[i] : undefined
            })
        }
        
        return parameterDescriptor
    }
    
    async getName(parameterDescriptorIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(parameterDescriptorIndex, "string:Name"))
    }
    
    async getAllName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Name"))
    }
    
    async getGroup(parameterDescriptorIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(parameterDescriptorIndex, "string:Group"))
    }
    
    async getAllGroup(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Group"))
    }
    
    async getParameterType(parameterDescriptorIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(parameterDescriptorIndex, "string:ParameterType"))
    }
    
    async getAllParameterType(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:ParameterType"))
    }
    
    async getIsInstance(parameterDescriptorIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(parameterDescriptorIndex, "byte:IsInstance"))
    }
    
    async getAllIsInstance(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsInstance"))
    }
    
    async getIsShared(parameterDescriptorIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(parameterDescriptorIndex, "byte:IsShared"))
    }
    
    async getAllIsShared(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsShared"))
    }
    
    async getIsReadOnly(parameterDescriptorIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(parameterDescriptorIndex, "byte:IsReadOnly"))
    }
    
    async getAllIsReadOnly(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsReadOnly"))
    }
    
    async getFlags(parameterDescriptorIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(parameterDescriptorIndex, "int:Flags"))
    }
    
    async getAllFlags(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Flags"))
    }
    
    async getGuid(parameterDescriptorIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(parameterDescriptorIndex, "string:Guid"))
    }
    
    async getAllGuid(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Guid"))
    }
    
    async getStorageType(parameterDescriptorIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(parameterDescriptorIndex, "int:StorageType"))
    }
    
    async getAllStorageType(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:StorageType"))
    }
    
    async getDisplayUnitIndex(parameterDescriptorIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(parameterDescriptorIndex, "index:Vim.DisplayUnit:DisplayUnit")
    }
    
    async getAllDisplayUnitIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.DisplayUnit:DisplayUnit")
    }
    
    async getDisplayUnit(parameterDescriptorIndex: number): Promise<IDisplayUnit | undefined> {
        const index = await this.getDisplayUnitIndex(parameterDescriptorIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.displayUnit?.get(index)
    }
    
}

export interface IParameter {
    index: number
    value?: string
    
    parameterDescriptorIndex?: number
    parameterDescriptor?: IParameterDescriptor
    elementIndex?: number
    element?: IElement
}

export interface IParameterTable {
    getCount(): Promise<number>
    get(parameterIndex: number): Promise<IParameter>
    getAll(): Promise<IParameter[]>
    
    getValue(parameterIndex: number): Promise<string | undefined>
    getAllValue(): Promise<string[] | undefined>
    
    getParameterDescriptorIndex(parameterIndex: number): Promise<number | undefined>
    getAllParameterDescriptorIndex(): Promise<number[] | undefined>
    getParameterDescriptor(parameterIndex: number): Promise<IParameterDescriptor | undefined>
    getElementIndex(parameterIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(parameterIndex: number): Promise<IElement | undefined>
}

export class Parameter implements IParameter {
    index: number
    value?: string
    
    parameterDescriptorIndex?: number
    parameterDescriptor?: IParameterDescriptor
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IParameterTable, index: number): Promise<IParameter> {
        let result = new Parameter()
        result.index = index
        
        await Promise.all([
            table.getValue(index).then(v => result.value = v),
            table.getParameterDescriptorIndex(index).then(v => result.parameterDescriptorIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class ParameterTable implements IParameterTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IParameterTable | undefined> {
        const entity = await document.entities.getBfast("Vim.Parameter")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ParameterTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(parameterIndex: number): Promise<IParameter> {
        return await Parameter.createFromTable(this, parameterIndex)
    }
    
    async getAll(): Promise<IParameter[]> {
        const localTable = await this.entityTable.getLocal()
        
        let value: string[] | undefined
        let parameterDescriptorIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { value = (await localTable.getStringArray("string:Value")) })(),
            (async () => { parameterDescriptorIndex = (await localTable.getNumberArray("index:Vim.ParameterDescriptor:ParameterDescriptor")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let parameter: IParameter[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            parameter.push({
                index: i,
                value: value ? value[i] : undefined,
                parameterDescriptorIndex: parameterDescriptorIndex ? parameterDescriptorIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return parameter
    }
    
    async getValue(parameterIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(parameterIndex, "string:Value"))
    }
    
    async getAllValue(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Value"))
    }
    
    async getParameterDescriptorIndex(parameterIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(parameterIndex, "index:Vim.ParameterDescriptor:ParameterDescriptor")
    }
    
    async getAllParameterDescriptorIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.ParameterDescriptor:ParameterDescriptor")
    }
    
    async getParameterDescriptor(parameterIndex: number): Promise<IParameterDescriptor | undefined> {
        const index = await this.getParameterDescriptorIndex(parameterIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.parameterDescriptor?.get(index)
    }
    
    async getElementIndex(parameterIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(parameterIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(parameterIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(parameterIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IElement {
    index: number
    id?: bigint
    type?: string
    name?: string
    uniqueId?: string
    location_X?: number
    location_Y?: number
    location_Z?: number
    locationSource?: number
    familyName?: string
    isPinned?: boolean
    creator?: string
    lastChangedBy?: string
    owner?: string
    
    levelIndex?: number
    level?: ILevel
    phaseCreatedIndex?: number
    phaseCreated?: IPhase
    phaseDemolishedIndex?: number
    phaseDemolished?: IPhase
    categoryIndex?: number
    category?: ICategory
    worksetIndex?: number
    workset?: IWorkset
    designOptionIndex?: number
    designOption?: IDesignOption
    ownerViewIndex?: number
    ownerView?: IView
    groupIndex?: number
    group?: IGroup
    assemblyInstanceIndex?: number
    assemblyInstance?: IAssemblyInstance
    bimDocumentIndex?: number
    bimDocument?: IBimDocument
    roomIndex?: number
    room?: IRoom
}

export interface IElementTable {
    getCount(): Promise<number>
    get(elementIndex: number): Promise<IElement>
    getAll(): Promise<IElement[]>
    
    getId(elementIndex: number): Promise<bigint | undefined>
    getAllId(): Promise<BigInt64Array | undefined>
    getType(elementIndex: number): Promise<string | undefined>
    getAllType(): Promise<string[] | undefined>
    getName(elementIndex: number): Promise<string | undefined>
    getAllName(): Promise<string[] | undefined>
    getUniqueId(elementIndex: number): Promise<string | undefined>
    getAllUniqueId(): Promise<string[] | undefined>
    getLocation_X(elementIndex: number): Promise<number | undefined>
    getAllLocation_X(): Promise<number[] | undefined>
    getLocation_Y(elementIndex: number): Promise<number | undefined>
    getAllLocation_Y(): Promise<number[] | undefined>
    getLocation_Z(elementIndex: number): Promise<number | undefined>
    getAllLocation_Z(): Promise<number[] | undefined>
    getLocationSource(elementIndex: number): Promise<number | undefined>
    getAllLocationSource(): Promise<number[] | undefined>
    getFamilyName(elementIndex: number): Promise<string | undefined>
    getAllFamilyName(): Promise<string[] | undefined>
    getIsPinned(elementIndex: number): Promise<boolean | undefined>
    getAllIsPinned(): Promise<boolean[] | undefined>
    getCreator(elementIndex: number): Promise<string | undefined>
    getAllCreator(): Promise<string[] | undefined>
    getLastChangedBy(elementIndex: number): Promise<string | undefined>
    getAllLastChangedBy(): Promise<string[] | undefined>
    getOwner(elementIndex: number): Promise<string | undefined>
    getAllOwner(): Promise<string[] | undefined>
    
    getLevelIndex(elementIndex: number): Promise<number | undefined>
    getAllLevelIndex(): Promise<number[] | undefined>
    getLevel(elementIndex: number): Promise<ILevel | undefined>
    getPhaseCreatedIndex(elementIndex: number): Promise<number | undefined>
    getAllPhaseCreatedIndex(): Promise<number[] | undefined>
    getPhaseCreated(elementIndex: number): Promise<IPhase | undefined>
    getPhaseDemolishedIndex(elementIndex: number): Promise<number | undefined>
    getAllPhaseDemolishedIndex(): Promise<number[] | undefined>
    getPhaseDemolished(elementIndex: number): Promise<IPhase | undefined>
    getCategoryIndex(elementIndex: number): Promise<number | undefined>
    getAllCategoryIndex(): Promise<number[] | undefined>
    getCategory(elementIndex: number): Promise<ICategory | undefined>
    getWorksetIndex(elementIndex: number): Promise<number | undefined>
    getAllWorksetIndex(): Promise<number[] | undefined>
    getWorkset(elementIndex: number): Promise<IWorkset | undefined>
    getDesignOptionIndex(elementIndex: number): Promise<number | undefined>
    getAllDesignOptionIndex(): Promise<number[] | undefined>
    getDesignOption(elementIndex: number): Promise<IDesignOption | undefined>
    getOwnerViewIndex(elementIndex: number): Promise<number | undefined>
    getAllOwnerViewIndex(): Promise<number[] | undefined>
    getOwnerView(elementIndex: number): Promise<IView | undefined>
    getGroupIndex(elementIndex: number): Promise<number | undefined>
    getAllGroupIndex(): Promise<number[] | undefined>
    getGroup(elementIndex: number): Promise<IGroup | undefined>
    getAssemblyInstanceIndex(elementIndex: number): Promise<number | undefined>
    getAllAssemblyInstanceIndex(): Promise<number[] | undefined>
    getAssemblyInstance(elementIndex: number): Promise<IAssemblyInstance | undefined>
    getBimDocumentIndex(elementIndex: number): Promise<number | undefined>
    getAllBimDocumentIndex(): Promise<number[] | undefined>
    getBimDocument(elementIndex: number): Promise<IBimDocument | undefined>
    getRoomIndex(elementIndex: number): Promise<number | undefined>
    getAllRoomIndex(): Promise<number[] | undefined>
    getRoom(elementIndex: number): Promise<IRoom | undefined>
}

export class Element implements IElement {
    index: number
    id?: bigint
    type?: string
    name?: string
    uniqueId?: string
    location_X?: number
    location_Y?: number
    location_Z?: number
    locationSource?: number
    familyName?: string
    isPinned?: boolean
    creator?: string
    lastChangedBy?: string
    owner?: string
    
    levelIndex?: number
    level?: ILevel
    phaseCreatedIndex?: number
    phaseCreated?: IPhase
    phaseDemolishedIndex?: number
    phaseDemolished?: IPhase
    categoryIndex?: number
    category?: ICategory
    worksetIndex?: number
    workset?: IWorkset
    designOptionIndex?: number
    designOption?: IDesignOption
    ownerViewIndex?: number
    ownerView?: IView
    groupIndex?: number
    group?: IGroup
    assemblyInstanceIndex?: number
    assemblyInstance?: IAssemblyInstance
    bimDocumentIndex?: number
    bimDocument?: IBimDocument
    roomIndex?: number
    room?: IRoom
    
    static async createFromTable(table: IElementTable, index: number): Promise<IElement> {
        let result = new Element()
        result.index = index
        
        await Promise.all([
            table.getId(index).then(v => result.id = v),
            table.getType(index).then(v => result.type = v),
            table.getName(index).then(v => result.name = v),
            table.getUniqueId(index).then(v => result.uniqueId = v),
            table.getLocation_X(index).then(v => result.location_X = v),
            table.getLocation_Y(index).then(v => result.location_Y = v),
            table.getLocation_Z(index).then(v => result.location_Z = v),
            table.getLocationSource(index).then(v => result.locationSource = v),
            table.getFamilyName(index).then(v => result.familyName = v),
            table.getIsPinned(index).then(v => result.isPinned = v),
            table.getCreator(index).then(v => result.creator = v),
            table.getLastChangedBy(index).then(v => result.lastChangedBy = v),
            table.getOwner(index).then(v => result.owner = v),
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
        ])
        
        return result
    }
}

export class ElementTable implements IElementTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IElementTable | undefined> {
        const entity = await document.entities.getBfast("Vim.Element")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ElementTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(elementIndex: number): Promise<IElement> {
        return await Element.createFromTable(this, elementIndex)
    }
    
    async getAll(): Promise<IElement[]> {
        const localTable = await this.entityTable.getLocal()
        
        let id: BigInt64Array | undefined
        let type: string[] | undefined
        let name: string[] | undefined
        let uniqueId: string[] | undefined
        let location_X: number[] | undefined
        let location_Y: number[] | undefined
        let location_Z: number[] | undefined
        let locationSource: number[] | undefined
        let familyName: string[] | undefined
        let isPinned: boolean[] | undefined
        let creator: string[] | undefined
        let lastChangedBy: string[] | undefined
        let owner: string[] | undefined
        let levelIndex: number[] | undefined
        let phaseCreatedIndex: number[] | undefined
        let phaseDemolishedIndex: number[] | undefined
        let categoryIndex: number[] | undefined
        let worksetIndex: number[] | undefined
        let designOptionIndex: number[] | undefined
        let ownerViewIndex: number[] | undefined
        let groupIndex: number[] | undefined
        let assemblyInstanceIndex: number[] | undefined
        let bimDocumentIndex: number[] | undefined
        let roomIndex: number[] | undefined
        
        await Promise.all([
            (async () => { id = (await localTable.getBigIntArray("long:Id")) ?? (await localTable.getBigIntArray("int:Id")) })(),
            (async () => { type = (await localTable.getStringArray("string:Type")) })(),
            (async () => { name = (await localTable.getStringArray("string:Name")) })(),
            (async () => { uniqueId = (await localTable.getStringArray("string:UniqueId")) })(),
            (async () => { location_X = (await localTable.getNumberArray("float:Location.X")) })(),
            (async () => { location_Y = (await localTable.getNumberArray("float:Location.Y")) })(),
            (async () => { location_Z = (await localTable.getNumberArray("float:Location.Z")) })(),
            (async () => { locationSource = (await localTable.getNumberArray("int:LocationSource")) })(),
            (async () => { familyName = (await localTable.getStringArray("string:FamilyName")) })(),
            (async () => { isPinned = (await localTable.getBooleanArray("byte:IsPinned")) })(),
            (async () => { creator = (await localTable.getStringArray("string:Creator")) })(),
            (async () => { lastChangedBy = (await localTable.getStringArray("string:LastChangedBy")) })(),
            (async () => { owner = (await localTable.getStringArray("string:Owner")) })(),
            (async () => { levelIndex = (await localTable.getNumberArray("index:Vim.Level:Level")) })(),
            (async () => { phaseCreatedIndex = (await localTable.getNumberArray("index:Vim.Phase:PhaseCreated")) })(),
            (async () => { phaseDemolishedIndex = (await localTable.getNumberArray("index:Vim.Phase:PhaseDemolished")) })(),
            (async () => { categoryIndex = (await localTable.getNumberArray("index:Vim.Category:Category")) })(),
            (async () => { worksetIndex = (await localTable.getNumberArray("index:Vim.Workset:Workset")) })(),
            (async () => { designOptionIndex = (await localTable.getNumberArray("index:Vim.DesignOption:DesignOption")) })(),
            (async () => { ownerViewIndex = (await localTable.getNumberArray("index:Vim.View:OwnerView")) })(),
            (async () => { groupIndex = (await localTable.getNumberArray("index:Vim.Group:Group")) })(),
            (async () => { assemblyInstanceIndex = (await localTable.getNumberArray("index:Vim.AssemblyInstance:AssemblyInstance")) })(),
            (async () => { bimDocumentIndex = (await localTable.getNumberArray("index:Vim.BimDocument:BimDocument")) })(),
            (async () => { roomIndex = (await localTable.getNumberArray("index:Vim.Room:Room")) })(),
        ])
        
        let element: IElement[] = []
        
        const rowCount = await this.getCount()
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
                locationSource: locationSource ? locationSource[i] : undefined,
                familyName: familyName ? familyName[i] : undefined,
                isPinned: isPinned ? isPinned[i] : undefined,
                creator: creator ? creator[i] : undefined,
                lastChangedBy: lastChangedBy ? lastChangedBy[i] : undefined,
                owner: owner ? owner[i] : undefined,
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
            })
        }
        
        return element
    }
    
    async getId(elementIndex: number): Promise<bigint | undefined> {
        return (await this.entityTable.getBigInt(elementIndex, "long:Id")) ?? (await this.entityTable.getBigInt(elementIndex, "int:Id"))
    }
    
    async getAllId(): Promise<BigInt64Array | undefined> {
        return (await this.entityTable.getBigIntArray("long:Id")) ?? (await this.entityTable.getBigIntArray("int:Id"))
    }
    
    async getType(elementIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(elementIndex, "string:Type"))
    }
    
    async getAllType(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Type"))
    }
    
    async getName(elementIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(elementIndex, "string:Name"))
    }
    
    async getAllName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Name"))
    }
    
    async getUniqueId(elementIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(elementIndex, "string:UniqueId"))
    }
    
    async getAllUniqueId(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:UniqueId"))
    }
    
    async getLocation_X(elementIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(elementIndex, "float:Location.X"))
    }
    
    async getAllLocation_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:Location.X"))
    }
    
    async getLocation_Y(elementIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(elementIndex, "float:Location.Y"))
    }
    
    async getAllLocation_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:Location.Y"))
    }
    
    async getLocation_Z(elementIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(elementIndex, "float:Location.Z"))
    }
    
    async getAllLocation_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:Location.Z"))
    }
    
    async getLocationSource(elementIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(elementIndex, "int:LocationSource"))
    }
    
    async getAllLocationSource(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:LocationSource"))
    }
    
    async getFamilyName(elementIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(elementIndex, "string:FamilyName"))
    }
    
    async getAllFamilyName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:FamilyName"))
    }
    
    async getIsPinned(elementIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(elementIndex, "byte:IsPinned"))
    }
    
    async getAllIsPinned(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsPinned"))
    }
    
    async getCreator(elementIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(elementIndex, "string:Creator"))
    }
    
    async getAllCreator(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Creator"))
    }
    
    async getLastChangedBy(elementIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(elementIndex, "string:LastChangedBy"))
    }
    
    async getAllLastChangedBy(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:LastChangedBy"))
    }
    
    async getOwner(elementIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(elementIndex, "string:Owner"))
    }
    
    async getAllOwner(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Owner"))
    }
    
    async getLevelIndex(elementIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementIndex, "index:Vim.Level:Level")
    }
    
    async getAllLevelIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Level:Level")
    }
    
    async getLevel(elementIndex: number): Promise<ILevel | undefined> {
        const index = await this.getLevelIndex(elementIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.level?.get(index)
    }
    
    async getPhaseCreatedIndex(elementIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementIndex, "index:Vim.Phase:PhaseCreated")
    }
    
    async getAllPhaseCreatedIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Phase:PhaseCreated")
    }
    
    async getPhaseCreated(elementIndex: number): Promise<IPhase | undefined> {
        const index = await this.getPhaseCreatedIndex(elementIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.phase?.get(index)
    }
    
    async getPhaseDemolishedIndex(elementIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementIndex, "index:Vim.Phase:PhaseDemolished")
    }
    
    async getAllPhaseDemolishedIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Phase:PhaseDemolished")
    }
    
    async getPhaseDemolished(elementIndex: number): Promise<IPhase | undefined> {
        const index = await this.getPhaseDemolishedIndex(elementIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.phase?.get(index)
    }
    
    async getCategoryIndex(elementIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementIndex, "index:Vim.Category:Category")
    }
    
    async getAllCategoryIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Category:Category")
    }
    
    async getCategory(elementIndex: number): Promise<ICategory | undefined> {
        const index = await this.getCategoryIndex(elementIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.category?.get(index)
    }
    
    async getWorksetIndex(elementIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementIndex, "index:Vim.Workset:Workset")
    }
    
    async getAllWorksetIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Workset:Workset")
    }
    
    async getWorkset(elementIndex: number): Promise<IWorkset | undefined> {
        const index = await this.getWorksetIndex(elementIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.workset?.get(index)
    }
    
    async getDesignOptionIndex(elementIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementIndex, "index:Vim.DesignOption:DesignOption")
    }
    
    async getAllDesignOptionIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.DesignOption:DesignOption")
    }
    
    async getDesignOption(elementIndex: number): Promise<IDesignOption | undefined> {
        const index = await this.getDesignOptionIndex(elementIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.designOption?.get(index)
    }
    
    async getOwnerViewIndex(elementIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementIndex, "index:Vim.View:OwnerView")
    }
    
    async getAllOwnerViewIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.View:OwnerView")
    }
    
    async getOwnerView(elementIndex: number): Promise<IView | undefined> {
        const index = await this.getOwnerViewIndex(elementIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.view?.get(index)
    }
    
    async getGroupIndex(elementIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementIndex, "index:Vim.Group:Group")
    }
    
    async getAllGroupIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Group:Group")
    }
    
    async getGroup(elementIndex: number): Promise<IGroup | undefined> {
        const index = await this.getGroupIndex(elementIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.group?.get(index)
    }
    
    async getAssemblyInstanceIndex(elementIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementIndex, "index:Vim.AssemblyInstance:AssemblyInstance")
    }
    
    async getAllAssemblyInstanceIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.AssemblyInstance:AssemblyInstance")
    }
    
    async getAssemblyInstance(elementIndex: number): Promise<IAssemblyInstance | undefined> {
        const index = await this.getAssemblyInstanceIndex(elementIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.assemblyInstance?.get(index)
    }
    
    async getBimDocumentIndex(elementIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementIndex, "index:Vim.BimDocument:BimDocument")
    }
    
    async getAllBimDocumentIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.BimDocument:BimDocument")
    }
    
    async getBimDocument(elementIndex: number): Promise<IBimDocument | undefined> {
        const index = await this.getBimDocumentIndex(elementIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.bimDocument?.get(index)
    }
    
    async getRoomIndex(elementIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementIndex, "index:Vim.Room:Room")
    }
    
    async getAllRoomIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Room:Room")
    }
    
    async getRoom(elementIndex: number): Promise<IRoom | undefined> {
        const index = await this.getRoomIndex(elementIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.room?.get(index)
    }
    
}

export interface IWorkset {
    index: number
    id?: number
    name?: string
    kind?: string
    isOpen?: boolean
    isEditable?: boolean
    owner?: string
    uniqueId?: string
    
    bimDocumentIndex?: number
    bimDocument?: IBimDocument
}

export interface IWorksetTable {
    getCount(): Promise<number>
    get(worksetIndex: number): Promise<IWorkset>
    getAll(): Promise<IWorkset[]>
    
    getId(worksetIndex: number): Promise<number | undefined>
    getAllId(): Promise<number[] | undefined>
    getName(worksetIndex: number): Promise<string | undefined>
    getAllName(): Promise<string[] | undefined>
    getKind(worksetIndex: number): Promise<string | undefined>
    getAllKind(): Promise<string[] | undefined>
    getIsOpen(worksetIndex: number): Promise<boolean | undefined>
    getAllIsOpen(): Promise<boolean[] | undefined>
    getIsEditable(worksetIndex: number): Promise<boolean | undefined>
    getAllIsEditable(): Promise<boolean[] | undefined>
    getOwner(worksetIndex: number): Promise<string | undefined>
    getAllOwner(): Promise<string[] | undefined>
    getUniqueId(worksetIndex: number): Promise<string | undefined>
    getAllUniqueId(): Promise<string[] | undefined>
    
    getBimDocumentIndex(worksetIndex: number): Promise<number | undefined>
    getAllBimDocumentIndex(): Promise<number[] | undefined>
    getBimDocument(worksetIndex: number): Promise<IBimDocument | undefined>
}

export class Workset implements IWorkset {
    index: number
    id?: number
    name?: string
    kind?: string
    isOpen?: boolean
    isEditable?: boolean
    owner?: string
    uniqueId?: string
    
    bimDocumentIndex?: number
    bimDocument?: IBimDocument
    
    static async createFromTable(table: IWorksetTable, index: number): Promise<IWorkset> {
        let result = new Workset()
        result.index = index
        
        await Promise.all([
            table.getId(index).then(v => result.id = v),
            table.getName(index).then(v => result.name = v),
            table.getKind(index).then(v => result.kind = v),
            table.getIsOpen(index).then(v => result.isOpen = v),
            table.getIsEditable(index).then(v => result.isEditable = v),
            table.getOwner(index).then(v => result.owner = v),
            table.getUniqueId(index).then(v => result.uniqueId = v),
            table.getBimDocumentIndex(index).then(v => result.bimDocumentIndex = v),
        ])
        
        return result
    }
}

export class WorksetTable implements IWorksetTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IWorksetTable | undefined> {
        const entity = await document.entities.getBfast("Vim.Workset")
        
        if (!entity) {
            return undefined
        }
        
        let table = new WorksetTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(worksetIndex: number): Promise<IWorkset> {
        return await Workset.createFromTable(this, worksetIndex)
    }
    
    async getAll(): Promise<IWorkset[]> {
        const localTable = await this.entityTable.getLocal()
        
        let id: number[] | undefined
        let name: string[] | undefined
        let kind: string[] | undefined
        let isOpen: boolean[] | undefined
        let isEditable: boolean[] | undefined
        let owner: string[] | undefined
        let uniqueId: string[] | undefined
        let bimDocumentIndex: number[] | undefined
        
        await Promise.all([
            (async () => { id = (await localTable.getNumberArray("int:Id")) })(),
            (async () => { name = (await localTable.getStringArray("string:Name")) })(),
            (async () => { kind = (await localTable.getStringArray("string:Kind")) })(),
            (async () => { isOpen = (await localTable.getBooleanArray("byte:IsOpen")) })(),
            (async () => { isEditable = (await localTable.getBooleanArray("byte:IsEditable")) })(),
            (async () => { owner = (await localTable.getStringArray("string:Owner")) })(),
            (async () => { uniqueId = (await localTable.getStringArray("string:UniqueId")) })(),
            (async () => { bimDocumentIndex = (await localTable.getNumberArray("index:Vim.BimDocument:BimDocument")) })(),
        ])
        
        let workset: IWorkset[] = []
        
        const rowCount = await this.getCount()
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
            })
        }
        
        return workset
    }
    
    async getId(worksetIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(worksetIndex, "int:Id"))
    }
    
    async getAllId(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Id"))
    }
    
    async getName(worksetIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(worksetIndex, "string:Name"))
    }
    
    async getAllName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Name"))
    }
    
    async getKind(worksetIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(worksetIndex, "string:Kind"))
    }
    
    async getAllKind(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Kind"))
    }
    
    async getIsOpen(worksetIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(worksetIndex, "byte:IsOpen"))
    }
    
    async getAllIsOpen(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsOpen"))
    }
    
    async getIsEditable(worksetIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(worksetIndex, "byte:IsEditable"))
    }
    
    async getAllIsEditable(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsEditable"))
    }
    
    async getOwner(worksetIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(worksetIndex, "string:Owner"))
    }
    
    async getAllOwner(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Owner"))
    }
    
    async getUniqueId(worksetIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(worksetIndex, "string:UniqueId"))
    }
    
    async getAllUniqueId(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:UniqueId"))
    }
    
    async getBimDocumentIndex(worksetIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(worksetIndex, "index:Vim.BimDocument:BimDocument")
    }
    
    async getAllBimDocumentIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.BimDocument:BimDocument")
    }
    
    async getBimDocument(worksetIndex: number): Promise<IBimDocument | undefined> {
        const index = await this.getBimDocumentIndex(worksetIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.bimDocument?.get(index)
    }
    
}

export interface IAssemblyInstance {
    index: number
    assemblyTypeName?: string
    position_X?: number
    position_Y?: number
    position_Z?: number
    
    elementIndex?: number
    element?: IElement
}

export interface IAssemblyInstanceTable {
    getCount(): Promise<number>
    get(assemblyInstanceIndex: number): Promise<IAssemblyInstance>
    getAll(): Promise<IAssemblyInstance[]>
    
    getAssemblyTypeName(assemblyInstanceIndex: number): Promise<string | undefined>
    getAllAssemblyTypeName(): Promise<string[] | undefined>
    getPosition_X(assemblyInstanceIndex: number): Promise<number | undefined>
    getAllPosition_X(): Promise<number[] | undefined>
    getPosition_Y(assemblyInstanceIndex: number): Promise<number | undefined>
    getAllPosition_Y(): Promise<number[] | undefined>
    getPosition_Z(assemblyInstanceIndex: number): Promise<number | undefined>
    getAllPosition_Z(): Promise<number[] | undefined>
    
    getElementIndex(assemblyInstanceIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(assemblyInstanceIndex: number): Promise<IElement | undefined>
}

export class AssemblyInstance implements IAssemblyInstance {
    index: number
    assemblyTypeName?: string
    position_X?: number
    position_Y?: number
    position_Z?: number
    
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IAssemblyInstanceTable, index: number): Promise<IAssemblyInstance> {
        let result = new AssemblyInstance()
        result.index = index
        
        await Promise.all([
            table.getAssemblyTypeName(index).then(v => result.assemblyTypeName = v),
            table.getPosition_X(index).then(v => result.position_X = v),
            table.getPosition_Y(index).then(v => result.position_Y = v),
            table.getPosition_Z(index).then(v => result.position_Z = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class AssemblyInstanceTable implements IAssemblyInstanceTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IAssemblyInstanceTable | undefined> {
        const entity = await document.entities.getBfast("Vim.AssemblyInstance")
        
        if (!entity) {
            return undefined
        }
        
        let table = new AssemblyInstanceTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(assemblyInstanceIndex: number): Promise<IAssemblyInstance> {
        return await AssemblyInstance.createFromTable(this, assemblyInstanceIndex)
    }
    
    async getAll(): Promise<IAssemblyInstance[]> {
        const localTable = await this.entityTable.getLocal()
        
        let assemblyTypeName: string[] | undefined
        let position_X: number[] | undefined
        let position_Y: number[] | undefined
        let position_Z: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { assemblyTypeName = (await localTable.getStringArray("string:AssemblyTypeName")) })(),
            (async () => { position_X = (await localTable.getNumberArray("float:Position.X")) })(),
            (async () => { position_Y = (await localTable.getNumberArray("float:Position.Y")) })(),
            (async () => { position_Z = (await localTable.getNumberArray("float:Position.Z")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let assemblyInstance: IAssemblyInstance[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            assemblyInstance.push({
                index: i,
                assemblyTypeName: assemblyTypeName ? assemblyTypeName[i] : undefined,
                position_X: position_X ? position_X[i] : undefined,
                position_Y: position_Y ? position_Y[i] : undefined,
                position_Z: position_Z ? position_Z[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return assemblyInstance
    }
    
    async getAssemblyTypeName(assemblyInstanceIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(assemblyInstanceIndex, "string:AssemblyTypeName"))
    }
    
    async getAllAssemblyTypeName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:AssemblyTypeName"))
    }
    
    async getPosition_X(assemblyInstanceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(assemblyInstanceIndex, "float:Position.X"))
    }
    
    async getAllPosition_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:Position.X"))
    }
    
    async getPosition_Y(assemblyInstanceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(assemblyInstanceIndex, "float:Position.Y"))
    }
    
    async getAllPosition_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:Position.Y"))
    }
    
    async getPosition_Z(assemblyInstanceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(assemblyInstanceIndex, "float:Position.Z"))
    }
    
    async getAllPosition_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:Position.Z"))
    }
    
    async getElementIndex(assemblyInstanceIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(assemblyInstanceIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(assemblyInstanceIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(assemblyInstanceIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IGroup {
    index: number
    groupType?: string
    position_X?: number
    position_Y?: number
    position_Z?: number
    
    elementIndex?: number
    element?: IElement
}

export interface IGroupTable {
    getCount(): Promise<number>
    get(groupIndex: number): Promise<IGroup>
    getAll(): Promise<IGroup[]>
    
    getGroupType(groupIndex: number): Promise<string | undefined>
    getAllGroupType(): Promise<string[] | undefined>
    getPosition_X(groupIndex: number): Promise<number | undefined>
    getAllPosition_X(): Promise<number[] | undefined>
    getPosition_Y(groupIndex: number): Promise<number | undefined>
    getAllPosition_Y(): Promise<number[] | undefined>
    getPosition_Z(groupIndex: number): Promise<number | undefined>
    getAllPosition_Z(): Promise<number[] | undefined>
    
    getElementIndex(groupIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(groupIndex: number): Promise<IElement | undefined>
}

export class Group implements IGroup {
    index: number
    groupType?: string
    position_X?: number
    position_Y?: number
    position_Z?: number
    
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IGroupTable, index: number): Promise<IGroup> {
        let result = new Group()
        result.index = index
        
        await Promise.all([
            table.getGroupType(index).then(v => result.groupType = v),
            table.getPosition_X(index).then(v => result.position_X = v),
            table.getPosition_Y(index).then(v => result.position_Y = v),
            table.getPosition_Z(index).then(v => result.position_Z = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class GroupTable implements IGroupTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IGroupTable | undefined> {
        const entity = await document.entities.getBfast("Vim.Group")
        
        if (!entity) {
            return undefined
        }
        
        let table = new GroupTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(groupIndex: number): Promise<IGroup> {
        return await Group.createFromTable(this, groupIndex)
    }
    
    async getAll(): Promise<IGroup[]> {
        const localTable = await this.entityTable.getLocal()
        
        let groupType: string[] | undefined
        let position_X: number[] | undefined
        let position_Y: number[] | undefined
        let position_Z: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { groupType = (await localTable.getStringArray("string:GroupType")) })(),
            (async () => { position_X = (await localTable.getNumberArray("float:Position.X")) })(),
            (async () => { position_Y = (await localTable.getNumberArray("float:Position.Y")) })(),
            (async () => { position_Z = (await localTable.getNumberArray("float:Position.Z")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let group: IGroup[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            group.push({
                index: i,
                groupType: groupType ? groupType[i] : undefined,
                position_X: position_X ? position_X[i] : undefined,
                position_Y: position_Y ? position_Y[i] : undefined,
                position_Z: position_Z ? position_Z[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return group
    }
    
    async getGroupType(groupIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(groupIndex, "string:GroupType"))
    }
    
    async getAllGroupType(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:GroupType"))
    }
    
    async getPosition_X(groupIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(groupIndex, "float:Position.X"))
    }
    
    async getAllPosition_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:Position.X"))
    }
    
    async getPosition_Y(groupIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(groupIndex, "float:Position.Y"))
    }
    
    async getAllPosition_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:Position.Y"))
    }
    
    async getPosition_Z(groupIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(groupIndex, "float:Position.Z"))
    }
    
    async getAllPosition_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:Position.Z"))
    }
    
    async getElementIndex(groupIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(groupIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(groupIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(groupIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IDesignOption {
    index: number
    isPrimary?: boolean
    
    elementIndex?: number
    element?: IElement
}

export interface IDesignOptionTable {
    getCount(): Promise<number>
    get(designOptionIndex: number): Promise<IDesignOption>
    getAll(): Promise<IDesignOption[]>
    
    getIsPrimary(designOptionIndex: number): Promise<boolean | undefined>
    getAllIsPrimary(): Promise<boolean[] | undefined>
    
    getElementIndex(designOptionIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(designOptionIndex: number): Promise<IElement | undefined>
}

export class DesignOption implements IDesignOption {
    index: number
    isPrimary?: boolean
    
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IDesignOptionTable, index: number): Promise<IDesignOption> {
        let result = new DesignOption()
        result.index = index
        
        await Promise.all([
            table.getIsPrimary(index).then(v => result.isPrimary = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class DesignOptionTable implements IDesignOptionTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IDesignOptionTable | undefined> {
        const entity = await document.entities.getBfast("Vim.DesignOption")
        
        if (!entity) {
            return undefined
        }
        
        let table = new DesignOptionTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(designOptionIndex: number): Promise<IDesignOption> {
        return await DesignOption.createFromTable(this, designOptionIndex)
    }
    
    async getAll(): Promise<IDesignOption[]> {
        const localTable = await this.entityTable.getLocal()
        
        let isPrimary: boolean[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { isPrimary = (await localTable.getBooleanArray("byte:IsPrimary")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let designOption: IDesignOption[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            designOption.push({
                index: i,
                isPrimary: isPrimary ? isPrimary[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return designOption
    }
    
    async getIsPrimary(designOptionIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(designOptionIndex, "byte:IsPrimary"))
    }
    
    async getAllIsPrimary(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsPrimary"))
    }
    
    async getElementIndex(designOptionIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(designOptionIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(designOptionIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(designOptionIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface ILevel {
    index: number
    elevation?: number
    projectElevation?: number
    
    familyTypeIndex?: number
    familyType?: IFamilyType
    buildingIndex?: number
    building?: IBuilding
    elementIndex?: number
    element?: IElement
}

export interface ILevelTable {
    getCount(): Promise<number>
    get(levelIndex: number): Promise<ILevel>
    getAll(): Promise<ILevel[]>
    
    getElevation(levelIndex: number): Promise<number | undefined>
    getAllElevation(): Promise<number[] | undefined>
    getProjectElevation(levelIndex: number): Promise<number | undefined>
    getAllProjectElevation(): Promise<number[] | undefined>
    
    getFamilyTypeIndex(levelIndex: number): Promise<number | undefined>
    getAllFamilyTypeIndex(): Promise<number[] | undefined>
    getFamilyType(levelIndex: number): Promise<IFamilyType | undefined>
    getBuildingIndex(levelIndex: number): Promise<number | undefined>
    getAllBuildingIndex(): Promise<number[] | undefined>
    getBuilding(levelIndex: number): Promise<IBuilding | undefined>
    getElementIndex(levelIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(levelIndex: number): Promise<IElement | undefined>
}

export class Level implements ILevel {
    index: number
    elevation?: number
    projectElevation?: number
    
    familyTypeIndex?: number
    familyType?: IFamilyType
    buildingIndex?: number
    building?: IBuilding
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: ILevelTable, index: number): Promise<ILevel> {
        let result = new Level()
        result.index = index
        
        await Promise.all([
            table.getElevation(index).then(v => result.elevation = v),
            table.getProjectElevation(index).then(v => result.projectElevation = v),
            table.getFamilyTypeIndex(index).then(v => result.familyTypeIndex = v),
            table.getBuildingIndex(index).then(v => result.buildingIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class LevelTable implements ILevelTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<ILevelTable | undefined> {
        const entity = await document.entities.getBfast("Vim.Level")
        
        if (!entity) {
            return undefined
        }
        
        let table = new LevelTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(levelIndex: number): Promise<ILevel> {
        return await Level.createFromTable(this, levelIndex)
    }
    
    async getAll(): Promise<ILevel[]> {
        const localTable = await this.entityTable.getLocal()
        
        let elevation: number[] | undefined
        let projectElevation: number[] | undefined
        let familyTypeIndex: number[] | undefined
        let buildingIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { elevation = (await localTable.getNumberArray("double:Elevation")) })(),
            (async () => { projectElevation = (await localTable.getNumberArray("double:ProjectElevation")) })(),
            (async () => { familyTypeIndex = (await localTable.getNumberArray("index:Vim.FamilyType:FamilyType")) })(),
            (async () => { buildingIndex = (await localTable.getNumberArray("index:Vim.Building:Building")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let level: ILevel[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            level.push({
                index: i,
                elevation: elevation ? elevation[i] : undefined,
                projectElevation: projectElevation ? projectElevation[i] : undefined,
                familyTypeIndex: familyTypeIndex ? familyTypeIndex[i] : undefined,
                buildingIndex: buildingIndex ? buildingIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return level
    }
    
    async getElevation(levelIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(levelIndex, "double:Elevation"))
    }
    
    async getAllElevation(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Elevation"))
    }
    
    async getProjectElevation(levelIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(levelIndex, "double:ProjectElevation"))
    }
    
    async getAllProjectElevation(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:ProjectElevation"))
    }
    
    async getFamilyTypeIndex(levelIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(levelIndex, "index:Vim.FamilyType:FamilyType")
    }
    
    async getAllFamilyTypeIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.FamilyType:FamilyType")
    }
    
    async getFamilyType(levelIndex: number): Promise<IFamilyType | undefined> {
        const index = await this.getFamilyTypeIndex(levelIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.familyType?.get(index)
    }
    
    async getBuildingIndex(levelIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(levelIndex, "index:Vim.Building:Building")
    }
    
    async getAllBuildingIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Building:Building")
    }
    
    async getBuilding(levelIndex: number): Promise<IBuilding | undefined> {
        const index = await this.getBuildingIndex(levelIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.building?.get(index)
    }
    
    async getElementIndex(levelIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(levelIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(levelIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(levelIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IPhase {
    index: number
    
    elementIndex?: number
    element?: IElement
}

export interface IPhaseTable {
    getCount(): Promise<number>
    get(phaseIndex: number): Promise<IPhase>
    getAll(): Promise<IPhase[]>
    
    getElementIndex(phaseIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(phaseIndex: number): Promise<IElement | undefined>
}

export class Phase implements IPhase {
    index: number
    
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IPhaseTable, index: number): Promise<IPhase> {
        let result = new Phase()
        result.index = index
        
        await Promise.all([
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class PhaseTable implements IPhaseTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IPhaseTable | undefined> {
        const entity = await document.entities.getBfast("Vim.Phase")
        
        if (!entity) {
            return undefined
        }
        
        let table = new PhaseTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(phaseIndex: number): Promise<IPhase> {
        return await Phase.createFromTable(this, phaseIndex)
    }
    
    async getAll(): Promise<IPhase[]> {
        const localTable = await this.entityTable.getLocal()
        
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let phase: IPhase[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            phase.push({
                index: i,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return phase
    }
    
    async getElementIndex(phaseIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(phaseIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(phaseIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(phaseIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IRoom {
    index: number
    baseOffset?: number
    limitOffset?: number
    unboundedHeight?: number
    volume?: number
    perimeter?: number
    area?: number
    number?: string
    
    upperLimitIndex?: number
    upperLimit?: ILevel
    elementIndex?: number
    element?: IElement
}

export interface IRoomTable {
    getCount(): Promise<number>
    get(roomIndex: number): Promise<IRoom>
    getAll(): Promise<IRoom[]>
    
    getBaseOffset(roomIndex: number): Promise<number | undefined>
    getAllBaseOffset(): Promise<number[] | undefined>
    getLimitOffset(roomIndex: number): Promise<number | undefined>
    getAllLimitOffset(): Promise<number[] | undefined>
    getUnboundedHeight(roomIndex: number): Promise<number | undefined>
    getAllUnboundedHeight(): Promise<number[] | undefined>
    getVolume(roomIndex: number): Promise<number | undefined>
    getAllVolume(): Promise<number[] | undefined>
    getPerimeter(roomIndex: number): Promise<number | undefined>
    getAllPerimeter(): Promise<number[] | undefined>
    getArea(roomIndex: number): Promise<number | undefined>
    getAllArea(): Promise<number[] | undefined>
    getNumber(roomIndex: number): Promise<string | undefined>
    getAllNumber(): Promise<string[] | undefined>
    
    getUpperLimitIndex(roomIndex: number): Promise<number | undefined>
    getAllUpperLimitIndex(): Promise<number[] | undefined>
    getUpperLimit(roomIndex: number): Promise<ILevel | undefined>
    getElementIndex(roomIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(roomIndex: number): Promise<IElement | undefined>
}

export class Room implements IRoom {
    index: number
    baseOffset?: number
    limitOffset?: number
    unboundedHeight?: number
    volume?: number
    perimeter?: number
    area?: number
    number?: string
    
    upperLimitIndex?: number
    upperLimit?: ILevel
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IRoomTable, index: number): Promise<IRoom> {
        let result = new Room()
        result.index = index
        
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
        ])
        
        return result
    }
}

export class RoomTable implements IRoomTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IRoomTable | undefined> {
        const entity = await document.entities.getBfast("Vim.Room")
        
        if (!entity) {
            return undefined
        }
        
        let table = new RoomTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(roomIndex: number): Promise<IRoom> {
        return await Room.createFromTable(this, roomIndex)
    }
    
    async getAll(): Promise<IRoom[]> {
        const localTable = await this.entityTable.getLocal()
        
        let baseOffset: number[] | undefined
        let limitOffset: number[] | undefined
        let unboundedHeight: number[] | undefined
        let volume: number[] | undefined
        let perimeter: number[] | undefined
        let area: number[] | undefined
        let number: string[] | undefined
        let upperLimitIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { baseOffset = (await localTable.getNumberArray("double:BaseOffset")) })(),
            (async () => { limitOffset = (await localTable.getNumberArray("double:LimitOffset")) })(),
            (async () => { unboundedHeight = (await localTable.getNumberArray("double:UnboundedHeight")) })(),
            (async () => { volume = (await localTable.getNumberArray("double:Volume")) })(),
            (async () => { perimeter = (await localTable.getNumberArray("double:Perimeter")) })(),
            (async () => { area = (await localTable.getNumberArray("double:Area")) })(),
            (async () => { number = (await localTable.getStringArray("string:Number")) })(),
            (async () => { upperLimitIndex = (await localTable.getNumberArray("index:Vim.Level:UpperLimit")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let room: IRoom[] = []
        
        const rowCount = await this.getCount()
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
            })
        }
        
        return room
    }
    
    async getBaseOffset(roomIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(roomIndex, "double:BaseOffset"))
    }
    
    async getAllBaseOffset(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:BaseOffset"))
    }
    
    async getLimitOffset(roomIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(roomIndex, "double:LimitOffset"))
    }
    
    async getAllLimitOffset(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:LimitOffset"))
    }
    
    async getUnboundedHeight(roomIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(roomIndex, "double:UnboundedHeight"))
    }
    
    async getAllUnboundedHeight(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:UnboundedHeight"))
    }
    
    async getVolume(roomIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(roomIndex, "double:Volume"))
    }
    
    async getAllVolume(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Volume"))
    }
    
    async getPerimeter(roomIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(roomIndex, "double:Perimeter"))
    }
    
    async getAllPerimeter(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Perimeter"))
    }
    
    async getArea(roomIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(roomIndex, "double:Area"))
    }
    
    async getAllArea(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Area"))
    }
    
    async getNumber(roomIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(roomIndex, "string:Number"))
    }
    
    async getAllNumber(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Number"))
    }
    
    async getUpperLimitIndex(roomIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(roomIndex, "index:Vim.Level:UpperLimit")
    }
    
    async getAllUpperLimitIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Level:UpperLimit")
    }
    
    async getUpperLimit(roomIndex: number): Promise<ILevel | undefined> {
        const index = await this.getUpperLimitIndex(roomIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.level?.get(index)
    }
    
    async getElementIndex(roomIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(roomIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(roomIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(roomIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IBimDocument {
    index: number
    title?: string
    isMetric?: boolean
    guid?: string
    numSaves?: number
    isLinked?: boolean
    isDetached?: boolean
    isWorkshared?: boolean
    pathName?: string
    latitude?: number
    longitude?: number
    timeZone?: number
    placeName?: string
    weatherStationName?: string
    elevation?: number
    projectLocation?: string
    issueDate?: string
    status?: string
    clientName?: string
    address?: string
    name?: string
    number?: string
    author?: string
    buildingName?: string
    organizationName?: string
    organizationDescription?: string
    product?: string
    version?: string
    user?: string
    fileLength?: bigint
    
    activeViewIndex?: number
    activeView?: IView
    ownerFamilyIndex?: number
    ownerFamily?: IFamily
    parentIndex?: number
    parent?: IBimDocument
    elementIndex?: number
    element?: IElement
}

export interface IBimDocumentTable {
    getCount(): Promise<number>
    get(bimDocumentIndex: number): Promise<IBimDocument>
    getAll(): Promise<IBimDocument[]>
    
    getTitle(bimDocumentIndex: number): Promise<string | undefined>
    getAllTitle(): Promise<string[] | undefined>
    getIsMetric(bimDocumentIndex: number): Promise<boolean | undefined>
    getAllIsMetric(): Promise<boolean[] | undefined>
    getGuid(bimDocumentIndex: number): Promise<string | undefined>
    getAllGuid(): Promise<string[] | undefined>
    getNumSaves(bimDocumentIndex: number): Promise<number | undefined>
    getAllNumSaves(): Promise<number[] | undefined>
    getIsLinked(bimDocumentIndex: number): Promise<boolean | undefined>
    getAllIsLinked(): Promise<boolean[] | undefined>
    getIsDetached(bimDocumentIndex: number): Promise<boolean | undefined>
    getAllIsDetached(): Promise<boolean[] | undefined>
    getIsWorkshared(bimDocumentIndex: number): Promise<boolean | undefined>
    getAllIsWorkshared(): Promise<boolean[] | undefined>
    getPathName(bimDocumentIndex: number): Promise<string | undefined>
    getAllPathName(): Promise<string[] | undefined>
    getLatitude(bimDocumentIndex: number): Promise<number | undefined>
    getAllLatitude(): Promise<number[] | undefined>
    getLongitude(bimDocumentIndex: number): Promise<number | undefined>
    getAllLongitude(): Promise<number[] | undefined>
    getTimeZone(bimDocumentIndex: number): Promise<number | undefined>
    getAllTimeZone(): Promise<number[] | undefined>
    getPlaceName(bimDocumentIndex: number): Promise<string | undefined>
    getAllPlaceName(): Promise<string[] | undefined>
    getWeatherStationName(bimDocumentIndex: number): Promise<string | undefined>
    getAllWeatherStationName(): Promise<string[] | undefined>
    getElevation(bimDocumentIndex: number): Promise<number | undefined>
    getAllElevation(): Promise<number[] | undefined>
    getProjectLocation(bimDocumentIndex: number): Promise<string | undefined>
    getAllProjectLocation(): Promise<string[] | undefined>
    getIssueDate(bimDocumentIndex: number): Promise<string | undefined>
    getAllIssueDate(): Promise<string[] | undefined>
    getStatus(bimDocumentIndex: number): Promise<string | undefined>
    getAllStatus(): Promise<string[] | undefined>
    getClientName(bimDocumentIndex: number): Promise<string | undefined>
    getAllClientName(): Promise<string[] | undefined>
    getAddress(bimDocumentIndex: number): Promise<string | undefined>
    getAllAddress(): Promise<string[] | undefined>
    getName(bimDocumentIndex: number): Promise<string | undefined>
    getAllName(): Promise<string[] | undefined>
    getNumber(bimDocumentIndex: number): Promise<string | undefined>
    getAllNumber(): Promise<string[] | undefined>
    getAuthor(bimDocumentIndex: number): Promise<string | undefined>
    getAllAuthor(): Promise<string[] | undefined>
    getBuildingName(bimDocumentIndex: number): Promise<string | undefined>
    getAllBuildingName(): Promise<string[] | undefined>
    getOrganizationName(bimDocumentIndex: number): Promise<string | undefined>
    getAllOrganizationName(): Promise<string[] | undefined>
    getOrganizationDescription(bimDocumentIndex: number): Promise<string | undefined>
    getAllOrganizationDescription(): Promise<string[] | undefined>
    getProduct(bimDocumentIndex: number): Promise<string | undefined>
    getAllProduct(): Promise<string[] | undefined>
    getVersion(bimDocumentIndex: number): Promise<string | undefined>
    getAllVersion(): Promise<string[] | undefined>
    getUser(bimDocumentIndex: number): Promise<string | undefined>
    getAllUser(): Promise<string[] | undefined>
    getFileLength(bimDocumentIndex: number): Promise<bigint | undefined>
    getAllFileLength(): Promise<BigInt64Array | undefined>
    
    getActiveViewIndex(bimDocumentIndex: number): Promise<number | undefined>
    getAllActiveViewIndex(): Promise<number[] | undefined>
    getActiveView(bimDocumentIndex: number): Promise<IView | undefined>
    getOwnerFamilyIndex(bimDocumentIndex: number): Promise<number | undefined>
    getAllOwnerFamilyIndex(): Promise<number[] | undefined>
    getOwnerFamily(bimDocumentIndex: number): Promise<IFamily | undefined>
    getParentIndex(bimDocumentIndex: number): Promise<number | undefined>
    getAllParentIndex(): Promise<number[] | undefined>
    getParent(bimDocumentIndex: number): Promise<IBimDocument | undefined>
    getElementIndex(bimDocumentIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(bimDocumentIndex: number): Promise<IElement | undefined>
}

export class BimDocument implements IBimDocument {
    index: number
    title?: string
    isMetric?: boolean
    guid?: string
    numSaves?: number
    isLinked?: boolean
    isDetached?: boolean
    isWorkshared?: boolean
    pathName?: string
    latitude?: number
    longitude?: number
    timeZone?: number
    placeName?: string
    weatherStationName?: string
    elevation?: number
    projectLocation?: string
    issueDate?: string
    status?: string
    clientName?: string
    address?: string
    name?: string
    number?: string
    author?: string
    buildingName?: string
    organizationName?: string
    organizationDescription?: string
    product?: string
    version?: string
    user?: string
    fileLength?: bigint
    
    activeViewIndex?: number
    activeView?: IView
    ownerFamilyIndex?: number
    ownerFamily?: IFamily
    parentIndex?: number
    parent?: IBimDocument
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IBimDocumentTable, index: number): Promise<IBimDocument> {
        let result = new BimDocument()
        result.index = index
        
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
            table.getFileLength(index).then(v => result.fileLength = v),
            table.getActiveViewIndex(index).then(v => result.activeViewIndex = v),
            table.getOwnerFamilyIndex(index).then(v => result.ownerFamilyIndex = v),
            table.getParentIndex(index).then(v => result.parentIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class BimDocumentTable implements IBimDocumentTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IBimDocumentTable | undefined> {
        const entity = await document.entities.getBfast("Vim.BimDocument")
        
        if (!entity) {
            return undefined
        }
        
        let table = new BimDocumentTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(bimDocumentIndex: number): Promise<IBimDocument> {
        return await BimDocument.createFromTable(this, bimDocumentIndex)
    }
    
    async getAll(): Promise<IBimDocument[]> {
        const localTable = await this.entityTable.getLocal()
        
        let title: string[] | undefined
        let isMetric: boolean[] | undefined
        let guid: string[] | undefined
        let numSaves: number[] | undefined
        let isLinked: boolean[] | undefined
        let isDetached: boolean[] | undefined
        let isWorkshared: boolean[] | undefined
        let pathName: string[] | undefined
        let latitude: number[] | undefined
        let longitude: number[] | undefined
        let timeZone: number[] | undefined
        let placeName: string[] | undefined
        let weatherStationName: string[] | undefined
        let elevation: number[] | undefined
        let projectLocation: string[] | undefined
        let issueDate: string[] | undefined
        let status: string[] | undefined
        let clientName: string[] | undefined
        let address: string[] | undefined
        let name: string[] | undefined
        let number: string[] | undefined
        let author: string[] | undefined
        let buildingName: string[] | undefined
        let organizationName: string[] | undefined
        let organizationDescription: string[] | undefined
        let product: string[] | undefined
        let version: string[] | undefined
        let user: string[] | undefined
        let fileLength: BigInt64Array | undefined
        let activeViewIndex: number[] | undefined
        let ownerFamilyIndex: number[] | undefined
        let parentIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { title = (await localTable.getStringArray("string:Title")) })(),
            (async () => { isMetric = (await localTable.getBooleanArray("byte:IsMetric")) })(),
            (async () => { guid = (await localTable.getStringArray("string:Guid")) })(),
            (async () => { numSaves = (await localTable.getNumberArray("int:NumSaves")) })(),
            (async () => { isLinked = (await localTable.getBooleanArray("byte:IsLinked")) })(),
            (async () => { isDetached = (await localTable.getBooleanArray("byte:IsDetached")) })(),
            (async () => { isWorkshared = (await localTable.getBooleanArray("byte:IsWorkshared")) })(),
            (async () => { pathName = (await localTable.getStringArray("string:PathName")) })(),
            (async () => { latitude = (await localTable.getNumberArray("double:Latitude")) })(),
            (async () => { longitude = (await localTable.getNumberArray("double:Longitude")) })(),
            (async () => { timeZone = (await localTable.getNumberArray("double:TimeZone")) })(),
            (async () => { placeName = (await localTable.getStringArray("string:PlaceName")) })(),
            (async () => { weatherStationName = (await localTable.getStringArray("string:WeatherStationName")) })(),
            (async () => { elevation = (await localTable.getNumberArray("double:Elevation")) })(),
            (async () => { projectLocation = (await localTable.getStringArray("string:ProjectLocation")) })(),
            (async () => { issueDate = (await localTable.getStringArray("string:IssueDate")) })(),
            (async () => { status = (await localTable.getStringArray("string:Status")) })(),
            (async () => { clientName = (await localTable.getStringArray("string:ClientName")) })(),
            (async () => { address = (await localTable.getStringArray("string:Address")) })(),
            (async () => { name = (await localTable.getStringArray("string:Name")) })(),
            (async () => { number = (await localTable.getStringArray("string:Number")) })(),
            (async () => { author = (await localTable.getStringArray("string:Author")) })(),
            (async () => { buildingName = (await localTable.getStringArray("string:BuildingName")) })(),
            (async () => { organizationName = (await localTable.getStringArray("string:OrganizationName")) })(),
            (async () => { organizationDescription = (await localTable.getStringArray("string:OrganizationDescription")) })(),
            (async () => { product = (await localTable.getStringArray("string:Product")) })(),
            (async () => { version = (await localTable.getStringArray("string:Version")) })(),
            (async () => { user = (await localTable.getStringArray("string:User")) })(),
            (async () => { fileLength = (await localTable.getBigIntArray("long:FileLength")) })(),
            (async () => { activeViewIndex = (await localTable.getNumberArray("index:Vim.View:ActiveView")) })(),
            (async () => { ownerFamilyIndex = (await localTable.getNumberArray("index:Vim.Family:OwnerFamily")) })(),
            (async () => { parentIndex = (await localTable.getNumberArray("index:Vim.BimDocument:Parent")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let bimDocument: IBimDocument[] = []
        
        const rowCount = await this.getCount()
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
                fileLength: fileLength ? fileLength[i] : undefined,
                activeViewIndex: activeViewIndex ? activeViewIndex[i] : undefined,
                ownerFamilyIndex: ownerFamilyIndex ? ownerFamilyIndex[i] : undefined,
                parentIndex: parentIndex ? parentIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return bimDocument
    }
    
    async getTitle(bimDocumentIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(bimDocumentIndex, "string:Title"))
    }
    
    async getAllTitle(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Title"))
    }
    
    async getIsMetric(bimDocumentIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(bimDocumentIndex, "byte:IsMetric"))
    }
    
    async getAllIsMetric(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsMetric"))
    }
    
    async getGuid(bimDocumentIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(bimDocumentIndex, "string:Guid"))
    }
    
    async getAllGuid(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Guid"))
    }
    
    async getNumSaves(bimDocumentIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(bimDocumentIndex, "int:NumSaves"))
    }
    
    async getAllNumSaves(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:NumSaves"))
    }
    
    async getIsLinked(bimDocumentIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(bimDocumentIndex, "byte:IsLinked"))
    }
    
    async getAllIsLinked(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsLinked"))
    }
    
    async getIsDetached(bimDocumentIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(bimDocumentIndex, "byte:IsDetached"))
    }
    
    async getAllIsDetached(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsDetached"))
    }
    
    async getIsWorkshared(bimDocumentIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(bimDocumentIndex, "byte:IsWorkshared"))
    }
    
    async getAllIsWorkshared(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsWorkshared"))
    }
    
    async getPathName(bimDocumentIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(bimDocumentIndex, "string:PathName"))
    }
    
    async getAllPathName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:PathName"))
    }
    
    async getLatitude(bimDocumentIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(bimDocumentIndex, "double:Latitude"))
    }
    
    async getAllLatitude(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Latitude"))
    }
    
    async getLongitude(bimDocumentIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(bimDocumentIndex, "double:Longitude"))
    }
    
    async getAllLongitude(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Longitude"))
    }
    
    async getTimeZone(bimDocumentIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(bimDocumentIndex, "double:TimeZone"))
    }
    
    async getAllTimeZone(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:TimeZone"))
    }
    
    async getPlaceName(bimDocumentIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(bimDocumentIndex, "string:PlaceName"))
    }
    
    async getAllPlaceName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:PlaceName"))
    }
    
    async getWeatherStationName(bimDocumentIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(bimDocumentIndex, "string:WeatherStationName"))
    }
    
    async getAllWeatherStationName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:WeatherStationName"))
    }
    
    async getElevation(bimDocumentIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(bimDocumentIndex, "double:Elevation"))
    }
    
    async getAllElevation(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Elevation"))
    }
    
    async getProjectLocation(bimDocumentIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(bimDocumentIndex, "string:ProjectLocation"))
    }
    
    async getAllProjectLocation(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:ProjectLocation"))
    }
    
    async getIssueDate(bimDocumentIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(bimDocumentIndex, "string:IssueDate"))
    }
    
    async getAllIssueDate(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:IssueDate"))
    }
    
    async getStatus(bimDocumentIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(bimDocumentIndex, "string:Status"))
    }
    
    async getAllStatus(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Status"))
    }
    
    async getClientName(bimDocumentIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(bimDocumentIndex, "string:ClientName"))
    }
    
    async getAllClientName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:ClientName"))
    }
    
    async getAddress(bimDocumentIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(bimDocumentIndex, "string:Address"))
    }
    
    async getAllAddress(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Address"))
    }
    
    async getName(bimDocumentIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(bimDocumentIndex, "string:Name"))
    }
    
    async getAllName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Name"))
    }
    
    async getNumber(bimDocumentIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(bimDocumentIndex, "string:Number"))
    }
    
    async getAllNumber(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Number"))
    }
    
    async getAuthor(bimDocumentIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(bimDocumentIndex, "string:Author"))
    }
    
    async getAllAuthor(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Author"))
    }
    
    async getBuildingName(bimDocumentIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(bimDocumentIndex, "string:BuildingName"))
    }
    
    async getAllBuildingName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:BuildingName"))
    }
    
    async getOrganizationName(bimDocumentIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(bimDocumentIndex, "string:OrganizationName"))
    }
    
    async getAllOrganizationName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:OrganizationName"))
    }
    
    async getOrganizationDescription(bimDocumentIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(bimDocumentIndex, "string:OrganizationDescription"))
    }
    
    async getAllOrganizationDescription(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:OrganizationDescription"))
    }
    
    async getProduct(bimDocumentIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(bimDocumentIndex, "string:Product"))
    }
    
    async getAllProduct(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Product"))
    }
    
    async getVersion(bimDocumentIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(bimDocumentIndex, "string:Version"))
    }
    
    async getAllVersion(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Version"))
    }
    
    async getUser(bimDocumentIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(bimDocumentIndex, "string:User"))
    }
    
    async getAllUser(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:User"))
    }
    
    async getFileLength(bimDocumentIndex: number): Promise<bigint | undefined> {
        return (await this.entityTable.getBigInt(bimDocumentIndex, "long:FileLength"))
    }
    
    async getAllFileLength(): Promise<BigInt64Array | undefined> {
        return (await this.entityTable.getBigIntArray("long:FileLength"))
    }
    
    async getActiveViewIndex(bimDocumentIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(bimDocumentIndex, "index:Vim.View:ActiveView")
    }
    
    async getAllActiveViewIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.View:ActiveView")
    }
    
    async getActiveView(bimDocumentIndex: number): Promise<IView | undefined> {
        const index = await this.getActiveViewIndex(bimDocumentIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.view?.get(index)
    }
    
    async getOwnerFamilyIndex(bimDocumentIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(bimDocumentIndex, "index:Vim.Family:OwnerFamily")
    }
    
    async getAllOwnerFamilyIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Family:OwnerFamily")
    }
    
    async getOwnerFamily(bimDocumentIndex: number): Promise<IFamily | undefined> {
        const index = await this.getOwnerFamilyIndex(bimDocumentIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.family?.get(index)
    }
    
    async getParentIndex(bimDocumentIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(bimDocumentIndex, "index:Vim.BimDocument:Parent")
    }
    
    async getAllParentIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.BimDocument:Parent")
    }
    
    async getParent(bimDocumentIndex: number): Promise<IBimDocument | undefined> {
        const index = await this.getParentIndex(bimDocumentIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.bimDocument?.get(index)
    }
    
    async getElementIndex(bimDocumentIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(bimDocumentIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(bimDocumentIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(bimDocumentIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IDisplayUnitInBimDocument {
    index: number
    
    displayUnitIndex?: number
    displayUnit?: IDisplayUnit
    bimDocumentIndex?: number
    bimDocument?: IBimDocument
}

export interface IDisplayUnitInBimDocumentTable {
    getCount(): Promise<number>
    get(displayUnitInBimDocumentIndex: number): Promise<IDisplayUnitInBimDocument>
    getAll(): Promise<IDisplayUnitInBimDocument[]>
    
    getDisplayUnitIndex(displayUnitInBimDocumentIndex: number): Promise<number | undefined>
    getAllDisplayUnitIndex(): Promise<number[] | undefined>
    getDisplayUnit(displayUnitInBimDocumentIndex: number): Promise<IDisplayUnit | undefined>
    getBimDocumentIndex(displayUnitInBimDocumentIndex: number): Promise<number | undefined>
    getAllBimDocumentIndex(): Promise<number[] | undefined>
    getBimDocument(displayUnitInBimDocumentIndex: number): Promise<IBimDocument | undefined>
}

export class DisplayUnitInBimDocument implements IDisplayUnitInBimDocument {
    index: number
    
    displayUnitIndex?: number
    displayUnit?: IDisplayUnit
    bimDocumentIndex?: number
    bimDocument?: IBimDocument
    
    static async createFromTable(table: IDisplayUnitInBimDocumentTable, index: number): Promise<IDisplayUnitInBimDocument> {
        let result = new DisplayUnitInBimDocument()
        result.index = index
        
        await Promise.all([
            table.getDisplayUnitIndex(index).then(v => result.displayUnitIndex = v),
            table.getBimDocumentIndex(index).then(v => result.bimDocumentIndex = v),
        ])
        
        return result
    }
}

export class DisplayUnitInBimDocumentTable implements IDisplayUnitInBimDocumentTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IDisplayUnitInBimDocumentTable | undefined> {
        const entity = await document.entities.getBfast("Vim.DisplayUnitInBimDocument")
        
        if (!entity) {
            return undefined
        }
        
        let table = new DisplayUnitInBimDocumentTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(displayUnitInBimDocumentIndex: number): Promise<IDisplayUnitInBimDocument> {
        return await DisplayUnitInBimDocument.createFromTable(this, displayUnitInBimDocumentIndex)
    }
    
    async getAll(): Promise<IDisplayUnitInBimDocument[]> {
        const localTable = await this.entityTable.getLocal()
        
        let displayUnitIndex: number[] | undefined
        let bimDocumentIndex: number[] | undefined
        
        await Promise.all([
            (async () => { displayUnitIndex = (await localTable.getNumberArray("index:Vim.DisplayUnit:DisplayUnit")) })(),
            (async () => { bimDocumentIndex = (await localTable.getNumberArray("index:Vim.BimDocument:BimDocument")) })(),
        ])
        
        let displayUnitInBimDocument: IDisplayUnitInBimDocument[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            displayUnitInBimDocument.push({
                index: i,
                displayUnitIndex: displayUnitIndex ? displayUnitIndex[i] : undefined,
                bimDocumentIndex: bimDocumentIndex ? bimDocumentIndex[i] : undefined
            })
        }
        
        return displayUnitInBimDocument
    }
    
    async getDisplayUnitIndex(displayUnitInBimDocumentIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(displayUnitInBimDocumentIndex, "index:Vim.DisplayUnit:DisplayUnit")
    }
    
    async getAllDisplayUnitIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.DisplayUnit:DisplayUnit")
    }
    
    async getDisplayUnit(displayUnitInBimDocumentIndex: number): Promise<IDisplayUnit | undefined> {
        const index = await this.getDisplayUnitIndex(displayUnitInBimDocumentIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.displayUnit?.get(index)
    }
    
    async getBimDocumentIndex(displayUnitInBimDocumentIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(displayUnitInBimDocumentIndex, "index:Vim.BimDocument:BimDocument")
    }
    
    async getAllBimDocumentIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.BimDocument:BimDocument")
    }
    
    async getBimDocument(displayUnitInBimDocumentIndex: number): Promise<IBimDocument | undefined> {
        const index = await this.getBimDocumentIndex(displayUnitInBimDocumentIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.bimDocument?.get(index)
    }
    
}

export interface IPhaseOrderInBimDocument {
    index: number
    orderIndex?: number
    
    phaseIndex?: number
    phase?: IPhase
    bimDocumentIndex?: number
    bimDocument?: IBimDocument
}

export interface IPhaseOrderInBimDocumentTable {
    getCount(): Promise<number>
    get(phaseOrderInBimDocumentIndex: number): Promise<IPhaseOrderInBimDocument>
    getAll(): Promise<IPhaseOrderInBimDocument[]>
    
    getOrderIndex(phaseOrderInBimDocumentIndex: number): Promise<number | undefined>
    getAllOrderIndex(): Promise<number[] | undefined>
    
    getPhaseIndex(phaseOrderInBimDocumentIndex: number): Promise<number | undefined>
    getAllPhaseIndex(): Promise<number[] | undefined>
    getPhase(phaseOrderInBimDocumentIndex: number): Promise<IPhase | undefined>
    getBimDocumentIndex(phaseOrderInBimDocumentIndex: number): Promise<number | undefined>
    getAllBimDocumentIndex(): Promise<number[] | undefined>
    getBimDocument(phaseOrderInBimDocumentIndex: number): Promise<IBimDocument | undefined>
}

export class PhaseOrderInBimDocument implements IPhaseOrderInBimDocument {
    index: number
    orderIndex?: number
    
    phaseIndex?: number
    phase?: IPhase
    bimDocumentIndex?: number
    bimDocument?: IBimDocument
    
    static async createFromTable(table: IPhaseOrderInBimDocumentTable, index: number): Promise<IPhaseOrderInBimDocument> {
        let result = new PhaseOrderInBimDocument()
        result.index = index
        
        await Promise.all([
            table.getOrderIndex(index).then(v => result.orderIndex = v),
            table.getPhaseIndex(index).then(v => result.phaseIndex = v),
            table.getBimDocumentIndex(index).then(v => result.bimDocumentIndex = v),
        ])
        
        return result
    }
}

export class PhaseOrderInBimDocumentTable implements IPhaseOrderInBimDocumentTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IPhaseOrderInBimDocumentTable | undefined> {
        const entity = await document.entities.getBfast("Vim.PhaseOrderInBimDocument")
        
        if (!entity) {
            return undefined
        }
        
        let table = new PhaseOrderInBimDocumentTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(phaseOrderInBimDocumentIndex: number): Promise<IPhaseOrderInBimDocument> {
        return await PhaseOrderInBimDocument.createFromTable(this, phaseOrderInBimDocumentIndex)
    }
    
    async getAll(): Promise<IPhaseOrderInBimDocument[]> {
        const localTable = await this.entityTable.getLocal()
        
        let orderIndex: number[] | undefined
        let phaseIndex: number[] | undefined
        let bimDocumentIndex: number[] | undefined
        
        await Promise.all([
            (async () => { orderIndex = (await localTable.getNumberArray("int:OrderIndex")) })(),
            (async () => { phaseIndex = (await localTable.getNumberArray("index:Vim.Phase:Phase")) })(),
            (async () => { bimDocumentIndex = (await localTable.getNumberArray("index:Vim.BimDocument:BimDocument")) })(),
        ])
        
        let phaseOrderInBimDocument: IPhaseOrderInBimDocument[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            phaseOrderInBimDocument.push({
                index: i,
                orderIndex: orderIndex ? orderIndex[i] : undefined,
                phaseIndex: phaseIndex ? phaseIndex[i] : undefined,
                bimDocumentIndex: bimDocumentIndex ? bimDocumentIndex[i] : undefined
            })
        }
        
        return phaseOrderInBimDocument
    }
    
    async getOrderIndex(phaseOrderInBimDocumentIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(phaseOrderInBimDocumentIndex, "int:OrderIndex"))
    }
    
    async getAllOrderIndex(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:OrderIndex"))
    }
    
    async getPhaseIndex(phaseOrderInBimDocumentIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(phaseOrderInBimDocumentIndex, "index:Vim.Phase:Phase")
    }
    
    async getAllPhaseIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Phase:Phase")
    }
    
    async getPhase(phaseOrderInBimDocumentIndex: number): Promise<IPhase | undefined> {
        const index = await this.getPhaseIndex(phaseOrderInBimDocumentIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.phase?.get(index)
    }
    
    async getBimDocumentIndex(phaseOrderInBimDocumentIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(phaseOrderInBimDocumentIndex, "index:Vim.BimDocument:BimDocument")
    }
    
    async getAllBimDocumentIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.BimDocument:BimDocument")
    }
    
    async getBimDocument(phaseOrderInBimDocumentIndex: number): Promise<IBimDocument | undefined> {
        const index = await this.getBimDocumentIndex(phaseOrderInBimDocumentIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.bimDocument?.get(index)
    }
    
}

export interface ICategory {
    index: number
    name?: string
    id?: bigint
    categoryType?: string
    lineColor_X?: number
    lineColor_Y?: number
    lineColor_Z?: number
    builtInCategory?: string
    projectionLineWeight?: number
    cutLineWeight?: number
    isCuttable?: boolean
    
    parentIndex?: number
    parent?: ICategory
    materialIndex?: number
    material?: IMaterial
    projectionLinePatternIndex?: number
    projectionLinePattern?: ILinePattern
    cutLinePatternIndex?: number
    cutLinePattern?: ILinePattern
}

export interface ICategoryTable {
    getCount(): Promise<number>
    get(categoryIndex: number): Promise<ICategory>
    getAll(): Promise<ICategory[]>
    
    getName(categoryIndex: number): Promise<string | undefined>
    getAllName(): Promise<string[] | undefined>
    getId(categoryIndex: number): Promise<bigint | undefined>
    getAllId(): Promise<BigInt64Array | undefined>
    getCategoryType(categoryIndex: number): Promise<string | undefined>
    getAllCategoryType(): Promise<string[] | undefined>
    getLineColor_X(categoryIndex: number): Promise<number | undefined>
    getAllLineColor_X(): Promise<number[] | undefined>
    getLineColor_Y(categoryIndex: number): Promise<number | undefined>
    getAllLineColor_Y(): Promise<number[] | undefined>
    getLineColor_Z(categoryIndex: number): Promise<number | undefined>
    getAllLineColor_Z(): Promise<number[] | undefined>
    getBuiltInCategory(categoryIndex: number): Promise<string | undefined>
    getAllBuiltInCategory(): Promise<string[] | undefined>
    getProjectionLineWeight(categoryIndex: number): Promise<number | undefined>
    getAllProjectionLineWeight(): Promise<number[] | undefined>
    getCutLineWeight(categoryIndex: number): Promise<number | undefined>
    getAllCutLineWeight(): Promise<number[] | undefined>
    getIsCuttable(categoryIndex: number): Promise<boolean | undefined>
    getAllIsCuttable(): Promise<boolean[] | undefined>
    
    getParentIndex(categoryIndex: number): Promise<number | undefined>
    getAllParentIndex(): Promise<number[] | undefined>
    getParent(categoryIndex: number): Promise<ICategory | undefined>
    getMaterialIndex(categoryIndex: number): Promise<number | undefined>
    getAllMaterialIndex(): Promise<number[] | undefined>
    getMaterial(categoryIndex: number): Promise<IMaterial | undefined>
    getProjectionLinePatternIndex(categoryIndex: number): Promise<number | undefined>
    getAllProjectionLinePatternIndex(): Promise<number[] | undefined>
    getProjectionLinePattern(categoryIndex: number): Promise<ILinePattern | undefined>
    getCutLinePatternIndex(categoryIndex: number): Promise<number | undefined>
    getAllCutLinePatternIndex(): Promise<number[] | undefined>
    getCutLinePattern(categoryIndex: number): Promise<ILinePattern | undefined>
}

export class Category implements ICategory {
    index: number
    name?: string
    id?: bigint
    categoryType?: string
    lineColor_X?: number
    lineColor_Y?: number
    lineColor_Z?: number
    builtInCategory?: string
    projectionLineWeight?: number
    cutLineWeight?: number
    isCuttable?: boolean
    
    parentIndex?: number
    parent?: ICategory
    materialIndex?: number
    material?: IMaterial
    projectionLinePatternIndex?: number
    projectionLinePattern?: ILinePattern
    cutLinePatternIndex?: number
    cutLinePattern?: ILinePattern
    
    static async createFromTable(table: ICategoryTable, index: number): Promise<ICategory> {
        let result = new Category()
        result.index = index
        
        await Promise.all([
            table.getName(index).then(v => result.name = v),
            table.getId(index).then(v => result.id = v),
            table.getCategoryType(index).then(v => result.categoryType = v),
            table.getLineColor_X(index).then(v => result.lineColor_X = v),
            table.getLineColor_Y(index).then(v => result.lineColor_Y = v),
            table.getLineColor_Z(index).then(v => result.lineColor_Z = v),
            table.getBuiltInCategory(index).then(v => result.builtInCategory = v),
            table.getProjectionLineWeight(index).then(v => result.projectionLineWeight = v),
            table.getCutLineWeight(index).then(v => result.cutLineWeight = v),
            table.getIsCuttable(index).then(v => result.isCuttable = v),
            table.getParentIndex(index).then(v => result.parentIndex = v),
            table.getMaterialIndex(index).then(v => result.materialIndex = v),
            table.getProjectionLinePatternIndex(index).then(v => result.projectionLinePatternIndex = v),
            table.getCutLinePatternIndex(index).then(v => result.cutLinePatternIndex = v),
        ])
        
        return result
    }
}

export class CategoryTable implements ICategoryTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<ICategoryTable | undefined> {
        const entity = await document.entities.getBfast("Vim.Category")
        
        if (!entity) {
            return undefined
        }
        
        let table = new CategoryTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(categoryIndex: number): Promise<ICategory> {
        return await Category.createFromTable(this, categoryIndex)
    }
    
    async getAll(): Promise<ICategory[]> {
        const localTable = await this.entityTable.getLocal()
        
        let name: string[] | undefined
        let id: BigInt64Array | undefined
        let categoryType: string[] | undefined
        let lineColor_X: number[] | undefined
        let lineColor_Y: number[] | undefined
        let lineColor_Z: number[] | undefined
        let builtInCategory: string[] | undefined
        let projectionLineWeight: number[] | undefined
        let cutLineWeight: number[] | undefined
        let isCuttable: boolean[] | undefined
        let parentIndex: number[] | undefined
        let materialIndex: number[] | undefined
        let projectionLinePatternIndex: number[] | undefined
        let cutLinePatternIndex: number[] | undefined
        
        await Promise.all([
            (async () => { name = (await localTable.getStringArray("string:Name")) })(),
            (async () => { id = (await localTable.getBigIntArray("long:Id")) ?? (await localTable.getBigIntArray("int:Id")) })(),
            (async () => { categoryType = (await localTable.getStringArray("string:CategoryType")) })(),
            (async () => { lineColor_X = (await localTable.getNumberArray("double:LineColor.X")) })(),
            (async () => { lineColor_Y = (await localTable.getNumberArray("double:LineColor.Y")) })(),
            (async () => { lineColor_Z = (await localTable.getNumberArray("double:LineColor.Z")) })(),
            (async () => { builtInCategory = (await localTable.getStringArray("string:BuiltInCategory")) })(),
            (async () => { projectionLineWeight = (await localTable.getNumberArray("int:ProjectionLineWeight")) })(),
            (async () => { cutLineWeight = (await localTable.getNumberArray("int:CutLineWeight")) })(),
            (async () => { isCuttable = (await localTable.getBooleanArray("byte:IsCuttable")) })(),
            (async () => { parentIndex = (await localTable.getNumberArray("index:Vim.Category:Parent")) })(),
            (async () => { materialIndex = (await localTable.getNumberArray("index:Vim.Material:Material")) })(),
            (async () => { projectionLinePatternIndex = (await localTable.getNumberArray("index:Vim.LinePattern:ProjectionLinePattern")) })(),
            (async () => { cutLinePatternIndex = (await localTable.getNumberArray("index:Vim.LinePattern:CutLinePattern")) })(),
        ])
        
        let category: ICategory[] = []
        
        const rowCount = await this.getCount()
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
                projectionLineWeight: projectionLineWeight ? projectionLineWeight[i] : undefined,
                cutLineWeight: cutLineWeight ? cutLineWeight[i] : undefined,
                isCuttable: isCuttable ? isCuttable[i] : undefined,
                parentIndex: parentIndex ? parentIndex[i] : undefined,
                materialIndex: materialIndex ? materialIndex[i] : undefined,
                projectionLinePatternIndex: projectionLinePatternIndex ? projectionLinePatternIndex[i] : undefined,
                cutLinePatternIndex: cutLinePatternIndex ? cutLinePatternIndex[i] : undefined
            })
        }
        
        return category
    }
    
    async getName(categoryIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(categoryIndex, "string:Name"))
    }
    
    async getAllName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Name"))
    }
    
    async getId(categoryIndex: number): Promise<bigint | undefined> {
        return (await this.entityTable.getBigInt(categoryIndex, "long:Id")) ?? (await this.entityTable.getBigInt(categoryIndex, "int:Id"))
    }
    
    async getAllId(): Promise<BigInt64Array | undefined> {
        return (await this.entityTable.getBigIntArray("long:Id")) ?? (await this.entityTable.getBigIntArray("int:Id"))
    }
    
    async getCategoryType(categoryIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(categoryIndex, "string:CategoryType"))
    }
    
    async getAllCategoryType(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:CategoryType"))
    }
    
    async getLineColor_X(categoryIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(categoryIndex, "double:LineColor.X"))
    }
    
    async getAllLineColor_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:LineColor.X"))
    }
    
    async getLineColor_Y(categoryIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(categoryIndex, "double:LineColor.Y"))
    }
    
    async getAllLineColor_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:LineColor.Y"))
    }
    
    async getLineColor_Z(categoryIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(categoryIndex, "double:LineColor.Z"))
    }
    
    async getAllLineColor_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:LineColor.Z"))
    }
    
    async getBuiltInCategory(categoryIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(categoryIndex, "string:BuiltInCategory"))
    }
    
    async getAllBuiltInCategory(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:BuiltInCategory"))
    }
    
    async getProjectionLineWeight(categoryIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(categoryIndex, "int:ProjectionLineWeight"))
    }
    
    async getAllProjectionLineWeight(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:ProjectionLineWeight"))
    }
    
    async getCutLineWeight(categoryIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(categoryIndex, "int:CutLineWeight"))
    }
    
    async getAllCutLineWeight(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:CutLineWeight"))
    }
    
    async getIsCuttable(categoryIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(categoryIndex, "byte:IsCuttable"))
    }
    
    async getAllIsCuttable(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsCuttable"))
    }
    
    async getParentIndex(categoryIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(categoryIndex, "index:Vim.Category:Parent")
    }
    
    async getAllParentIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Category:Parent")
    }
    
    async getParent(categoryIndex: number): Promise<ICategory | undefined> {
        const index = await this.getParentIndex(categoryIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.category?.get(index)
    }
    
    async getMaterialIndex(categoryIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(categoryIndex, "index:Vim.Material:Material")
    }
    
    async getAllMaterialIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Material:Material")
    }
    
    async getMaterial(categoryIndex: number): Promise<IMaterial | undefined> {
        const index = await this.getMaterialIndex(categoryIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.material?.get(index)
    }
    
    async getProjectionLinePatternIndex(categoryIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(categoryIndex, "index:Vim.LinePattern:ProjectionLinePattern")
    }
    
    async getAllProjectionLinePatternIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.LinePattern:ProjectionLinePattern")
    }
    
    async getProjectionLinePattern(categoryIndex: number): Promise<ILinePattern | undefined> {
        const index = await this.getProjectionLinePatternIndex(categoryIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.linePattern?.get(index)
    }
    
    async getCutLinePatternIndex(categoryIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(categoryIndex, "index:Vim.LinePattern:CutLinePattern")
    }
    
    async getAllCutLinePatternIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.LinePattern:CutLinePattern")
    }
    
    async getCutLinePattern(categoryIndex: number): Promise<ILinePattern | undefined> {
        const index = await this.getCutLinePatternIndex(categoryIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.linePattern?.get(index)
    }
    
}

export interface IFamily {
    index: number
    structuralMaterialType?: string
    structuralSectionShape?: string
    isSystemFamily?: boolean
    isInPlace?: boolean
    
    familyCategoryIndex?: number
    familyCategory?: ICategory
    elementIndex?: number
    element?: IElement
}

export interface IFamilyTable {
    getCount(): Promise<number>
    get(familyIndex: number): Promise<IFamily>
    getAll(): Promise<IFamily[]>
    
    getStructuralMaterialType(familyIndex: number): Promise<string | undefined>
    getAllStructuralMaterialType(): Promise<string[] | undefined>
    getStructuralSectionShape(familyIndex: number): Promise<string | undefined>
    getAllStructuralSectionShape(): Promise<string[] | undefined>
    getIsSystemFamily(familyIndex: number): Promise<boolean | undefined>
    getAllIsSystemFamily(): Promise<boolean[] | undefined>
    getIsInPlace(familyIndex: number): Promise<boolean | undefined>
    getAllIsInPlace(): Promise<boolean[] | undefined>
    
    getFamilyCategoryIndex(familyIndex: number): Promise<number | undefined>
    getAllFamilyCategoryIndex(): Promise<number[] | undefined>
    getFamilyCategory(familyIndex: number): Promise<ICategory | undefined>
    getElementIndex(familyIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(familyIndex: number): Promise<IElement | undefined>
}

export class Family implements IFamily {
    index: number
    structuralMaterialType?: string
    structuralSectionShape?: string
    isSystemFamily?: boolean
    isInPlace?: boolean
    
    familyCategoryIndex?: number
    familyCategory?: ICategory
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IFamilyTable, index: number): Promise<IFamily> {
        let result = new Family()
        result.index = index
        
        await Promise.all([
            table.getStructuralMaterialType(index).then(v => result.structuralMaterialType = v),
            table.getStructuralSectionShape(index).then(v => result.structuralSectionShape = v),
            table.getIsSystemFamily(index).then(v => result.isSystemFamily = v),
            table.getIsInPlace(index).then(v => result.isInPlace = v),
            table.getFamilyCategoryIndex(index).then(v => result.familyCategoryIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class FamilyTable implements IFamilyTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IFamilyTable | undefined> {
        const entity = await document.entities.getBfast("Vim.Family")
        
        if (!entity) {
            return undefined
        }
        
        let table = new FamilyTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(familyIndex: number): Promise<IFamily> {
        return await Family.createFromTable(this, familyIndex)
    }
    
    async getAll(): Promise<IFamily[]> {
        const localTable = await this.entityTable.getLocal()
        
        let structuralMaterialType: string[] | undefined
        let structuralSectionShape: string[] | undefined
        let isSystemFamily: boolean[] | undefined
        let isInPlace: boolean[] | undefined
        let familyCategoryIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { structuralMaterialType = (await localTable.getStringArray("string:StructuralMaterialType")) })(),
            (async () => { structuralSectionShape = (await localTable.getStringArray("string:StructuralSectionShape")) })(),
            (async () => { isSystemFamily = (await localTable.getBooleanArray("byte:IsSystemFamily")) })(),
            (async () => { isInPlace = (await localTable.getBooleanArray("byte:IsInPlace")) })(),
            (async () => { familyCategoryIndex = (await localTable.getNumberArray("index:Vim.Category:FamilyCategory")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let family: IFamily[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            family.push({
                index: i,
                structuralMaterialType: structuralMaterialType ? structuralMaterialType[i] : undefined,
                structuralSectionShape: structuralSectionShape ? structuralSectionShape[i] : undefined,
                isSystemFamily: isSystemFamily ? isSystemFamily[i] : undefined,
                isInPlace: isInPlace ? isInPlace[i] : undefined,
                familyCategoryIndex: familyCategoryIndex ? familyCategoryIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return family
    }
    
    async getStructuralMaterialType(familyIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(familyIndex, "string:StructuralMaterialType"))
    }
    
    async getAllStructuralMaterialType(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:StructuralMaterialType"))
    }
    
    async getStructuralSectionShape(familyIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(familyIndex, "string:StructuralSectionShape"))
    }
    
    async getAllStructuralSectionShape(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:StructuralSectionShape"))
    }
    
    async getIsSystemFamily(familyIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(familyIndex, "byte:IsSystemFamily"))
    }
    
    async getAllIsSystemFamily(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsSystemFamily"))
    }
    
    async getIsInPlace(familyIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(familyIndex, "byte:IsInPlace"))
    }
    
    async getAllIsInPlace(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsInPlace"))
    }
    
    async getFamilyCategoryIndex(familyIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(familyIndex, "index:Vim.Category:FamilyCategory")
    }
    
    async getAllFamilyCategoryIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Category:FamilyCategory")
    }
    
    async getFamilyCategory(familyIndex: number): Promise<ICategory | undefined> {
        const index = await this.getFamilyCategoryIndex(familyIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.category?.get(index)
    }
    
    async getElementIndex(familyIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(familyIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(familyIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(familyIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IFamilyType {
    index: number
    isSystemFamilyType?: boolean
    
    familyIndex?: number
    family?: IFamily
    compoundStructureIndex?: number
    compoundStructure?: ICompoundStructure
    elementIndex?: number
    element?: IElement
}

export interface IFamilyTypeTable {
    getCount(): Promise<number>
    get(familyTypeIndex: number): Promise<IFamilyType>
    getAll(): Promise<IFamilyType[]>
    
    getIsSystemFamilyType(familyTypeIndex: number): Promise<boolean | undefined>
    getAllIsSystemFamilyType(): Promise<boolean[] | undefined>
    
    getFamilyIndex(familyTypeIndex: number): Promise<number | undefined>
    getAllFamilyIndex(): Promise<number[] | undefined>
    getFamily(familyTypeIndex: number): Promise<IFamily | undefined>
    getCompoundStructureIndex(familyTypeIndex: number): Promise<number | undefined>
    getAllCompoundStructureIndex(): Promise<number[] | undefined>
    getCompoundStructure(familyTypeIndex: number): Promise<ICompoundStructure | undefined>
    getElementIndex(familyTypeIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(familyTypeIndex: number): Promise<IElement | undefined>
}

export class FamilyType implements IFamilyType {
    index: number
    isSystemFamilyType?: boolean
    
    familyIndex?: number
    family?: IFamily
    compoundStructureIndex?: number
    compoundStructure?: ICompoundStructure
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IFamilyTypeTable, index: number): Promise<IFamilyType> {
        let result = new FamilyType()
        result.index = index
        
        await Promise.all([
            table.getIsSystemFamilyType(index).then(v => result.isSystemFamilyType = v),
            table.getFamilyIndex(index).then(v => result.familyIndex = v),
            table.getCompoundStructureIndex(index).then(v => result.compoundStructureIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class FamilyTypeTable implements IFamilyTypeTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IFamilyTypeTable | undefined> {
        const entity = await document.entities.getBfast("Vim.FamilyType")
        
        if (!entity) {
            return undefined
        }
        
        let table = new FamilyTypeTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(familyTypeIndex: number): Promise<IFamilyType> {
        return await FamilyType.createFromTable(this, familyTypeIndex)
    }
    
    async getAll(): Promise<IFamilyType[]> {
        const localTable = await this.entityTable.getLocal()
        
        let isSystemFamilyType: boolean[] | undefined
        let familyIndex: number[] | undefined
        let compoundStructureIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { isSystemFamilyType = (await localTable.getBooleanArray("byte:IsSystemFamilyType")) })(),
            (async () => { familyIndex = (await localTable.getNumberArray("index:Vim.Family:Family")) })(),
            (async () => { compoundStructureIndex = (await localTable.getNumberArray("index:Vim.CompoundStructure:CompoundStructure")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let familyType: IFamilyType[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            familyType.push({
                index: i,
                isSystemFamilyType: isSystemFamilyType ? isSystemFamilyType[i] : undefined,
                familyIndex: familyIndex ? familyIndex[i] : undefined,
                compoundStructureIndex: compoundStructureIndex ? compoundStructureIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return familyType
    }
    
    async getIsSystemFamilyType(familyTypeIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(familyTypeIndex, "byte:IsSystemFamilyType"))
    }
    
    async getAllIsSystemFamilyType(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsSystemFamilyType"))
    }
    
    async getFamilyIndex(familyTypeIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(familyTypeIndex, "index:Vim.Family:Family")
    }
    
    async getAllFamilyIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Family:Family")
    }
    
    async getFamily(familyTypeIndex: number): Promise<IFamily | undefined> {
        const index = await this.getFamilyIndex(familyTypeIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.family?.get(index)
    }
    
    async getCompoundStructureIndex(familyTypeIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(familyTypeIndex, "index:Vim.CompoundStructure:CompoundStructure")
    }
    
    async getAllCompoundStructureIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.CompoundStructure:CompoundStructure")
    }
    
    async getCompoundStructure(familyTypeIndex: number): Promise<ICompoundStructure | undefined> {
        const index = await this.getCompoundStructureIndex(familyTypeIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.compoundStructure?.get(index)
    }
    
    async getElementIndex(familyTypeIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(familyTypeIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(familyTypeIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(familyTypeIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IFamilyInstance {
    index: number
    facingFlipped?: boolean
    facingOrientation_X?: number
    facingOrientation_Y?: number
    facingOrientation_Z?: number
    handFlipped?: boolean
    mirrored?: boolean
    hasModifiedGeometry?: boolean
    scale?: number
    basisX_X?: number
    basisX_Y?: number
    basisX_Z?: number
    basisY_X?: number
    basisY_Y?: number
    basisY_Z?: number
    basisZ_X?: number
    basisZ_Y?: number
    basisZ_Z?: number
    translation_X?: number
    translation_Y?: number
    translation_Z?: number
    handOrientation_X?: number
    handOrientation_Y?: number
    handOrientation_Z?: number
    
    familyTypeIndex?: number
    familyType?: IFamilyType
    hostIndex?: number
    host?: IElement
    fromRoomIndex?: number
    fromRoom?: IRoom
    toRoomIndex?: number
    toRoom?: IRoom
    superComponentIndex?: number
    superComponent?: IElement
    elementIndex?: number
    element?: IElement
}

export interface IFamilyInstanceTable {
    getCount(): Promise<number>
    get(familyInstanceIndex: number): Promise<IFamilyInstance>
    getAll(): Promise<IFamilyInstance[]>
    
    getFacingFlipped(familyInstanceIndex: number): Promise<boolean | undefined>
    getAllFacingFlipped(): Promise<boolean[] | undefined>
    getFacingOrientation_X(familyInstanceIndex: number): Promise<number | undefined>
    getAllFacingOrientation_X(): Promise<number[] | undefined>
    getFacingOrientation_Y(familyInstanceIndex: number): Promise<number | undefined>
    getAllFacingOrientation_Y(): Promise<number[] | undefined>
    getFacingOrientation_Z(familyInstanceIndex: number): Promise<number | undefined>
    getAllFacingOrientation_Z(): Promise<number[] | undefined>
    getHandFlipped(familyInstanceIndex: number): Promise<boolean | undefined>
    getAllHandFlipped(): Promise<boolean[] | undefined>
    getMirrored(familyInstanceIndex: number): Promise<boolean | undefined>
    getAllMirrored(): Promise<boolean[] | undefined>
    getHasModifiedGeometry(familyInstanceIndex: number): Promise<boolean | undefined>
    getAllHasModifiedGeometry(): Promise<boolean[] | undefined>
    getScale(familyInstanceIndex: number): Promise<number | undefined>
    getAllScale(): Promise<number[] | undefined>
    getBasisX_X(familyInstanceIndex: number): Promise<number | undefined>
    getAllBasisX_X(): Promise<number[] | undefined>
    getBasisX_Y(familyInstanceIndex: number): Promise<number | undefined>
    getAllBasisX_Y(): Promise<number[] | undefined>
    getBasisX_Z(familyInstanceIndex: number): Promise<number | undefined>
    getAllBasisX_Z(): Promise<number[] | undefined>
    getBasisY_X(familyInstanceIndex: number): Promise<number | undefined>
    getAllBasisY_X(): Promise<number[] | undefined>
    getBasisY_Y(familyInstanceIndex: number): Promise<number | undefined>
    getAllBasisY_Y(): Promise<number[] | undefined>
    getBasisY_Z(familyInstanceIndex: number): Promise<number | undefined>
    getAllBasisY_Z(): Promise<number[] | undefined>
    getBasisZ_X(familyInstanceIndex: number): Promise<number | undefined>
    getAllBasisZ_X(): Promise<number[] | undefined>
    getBasisZ_Y(familyInstanceIndex: number): Promise<number | undefined>
    getAllBasisZ_Y(): Promise<number[] | undefined>
    getBasisZ_Z(familyInstanceIndex: number): Promise<number | undefined>
    getAllBasisZ_Z(): Promise<number[] | undefined>
    getTranslation_X(familyInstanceIndex: number): Promise<number | undefined>
    getAllTranslation_X(): Promise<number[] | undefined>
    getTranslation_Y(familyInstanceIndex: number): Promise<number | undefined>
    getAllTranslation_Y(): Promise<number[] | undefined>
    getTranslation_Z(familyInstanceIndex: number): Promise<number | undefined>
    getAllTranslation_Z(): Promise<number[] | undefined>
    getHandOrientation_X(familyInstanceIndex: number): Promise<number | undefined>
    getAllHandOrientation_X(): Promise<number[] | undefined>
    getHandOrientation_Y(familyInstanceIndex: number): Promise<number | undefined>
    getAllHandOrientation_Y(): Promise<number[] | undefined>
    getHandOrientation_Z(familyInstanceIndex: number): Promise<number | undefined>
    getAllHandOrientation_Z(): Promise<number[] | undefined>
    
    getFamilyTypeIndex(familyInstanceIndex: number): Promise<number | undefined>
    getAllFamilyTypeIndex(): Promise<number[] | undefined>
    getFamilyType(familyInstanceIndex: number): Promise<IFamilyType | undefined>
    getHostIndex(familyInstanceIndex: number): Promise<number | undefined>
    getAllHostIndex(): Promise<number[] | undefined>
    getHost(familyInstanceIndex: number): Promise<IElement | undefined>
    getFromRoomIndex(familyInstanceIndex: number): Promise<number | undefined>
    getAllFromRoomIndex(): Promise<number[] | undefined>
    getFromRoom(familyInstanceIndex: number): Promise<IRoom | undefined>
    getToRoomIndex(familyInstanceIndex: number): Promise<number | undefined>
    getAllToRoomIndex(): Promise<number[] | undefined>
    getToRoom(familyInstanceIndex: number): Promise<IRoom | undefined>
    getSuperComponentIndex(familyInstanceIndex: number): Promise<number | undefined>
    getAllSuperComponentIndex(): Promise<number[] | undefined>
    getSuperComponent(familyInstanceIndex: number): Promise<IElement | undefined>
    getElementIndex(familyInstanceIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(familyInstanceIndex: number): Promise<IElement | undefined>
}

export class FamilyInstance implements IFamilyInstance {
    index: number
    facingFlipped?: boolean
    facingOrientation_X?: number
    facingOrientation_Y?: number
    facingOrientation_Z?: number
    handFlipped?: boolean
    mirrored?: boolean
    hasModifiedGeometry?: boolean
    scale?: number
    basisX_X?: number
    basisX_Y?: number
    basisX_Z?: number
    basisY_X?: number
    basisY_Y?: number
    basisY_Z?: number
    basisZ_X?: number
    basisZ_Y?: number
    basisZ_Z?: number
    translation_X?: number
    translation_Y?: number
    translation_Z?: number
    handOrientation_X?: number
    handOrientation_Y?: number
    handOrientation_Z?: number
    
    familyTypeIndex?: number
    familyType?: IFamilyType
    hostIndex?: number
    host?: IElement
    fromRoomIndex?: number
    fromRoom?: IRoom
    toRoomIndex?: number
    toRoom?: IRoom
    superComponentIndex?: number
    superComponent?: IElement
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IFamilyInstanceTable, index: number): Promise<IFamilyInstance> {
        let result = new FamilyInstance()
        result.index = index
        
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
            table.getSuperComponentIndex(index).then(v => result.superComponentIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class FamilyInstanceTable implements IFamilyInstanceTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IFamilyInstanceTable | undefined> {
        const entity = await document.entities.getBfast("Vim.FamilyInstance")
        
        if (!entity) {
            return undefined
        }
        
        let table = new FamilyInstanceTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(familyInstanceIndex: number): Promise<IFamilyInstance> {
        return await FamilyInstance.createFromTable(this, familyInstanceIndex)
    }
    
    async getAll(): Promise<IFamilyInstance[]> {
        const localTable = await this.entityTable.getLocal()
        
        let facingFlipped: boolean[] | undefined
        let facingOrientation_X: number[] | undefined
        let facingOrientation_Y: number[] | undefined
        let facingOrientation_Z: number[] | undefined
        let handFlipped: boolean[] | undefined
        let mirrored: boolean[] | undefined
        let hasModifiedGeometry: boolean[] | undefined
        let scale: number[] | undefined
        let basisX_X: number[] | undefined
        let basisX_Y: number[] | undefined
        let basisX_Z: number[] | undefined
        let basisY_X: number[] | undefined
        let basisY_Y: number[] | undefined
        let basisY_Z: number[] | undefined
        let basisZ_X: number[] | undefined
        let basisZ_Y: number[] | undefined
        let basisZ_Z: number[] | undefined
        let translation_X: number[] | undefined
        let translation_Y: number[] | undefined
        let translation_Z: number[] | undefined
        let handOrientation_X: number[] | undefined
        let handOrientation_Y: number[] | undefined
        let handOrientation_Z: number[] | undefined
        let familyTypeIndex: number[] | undefined
        let hostIndex: number[] | undefined
        let fromRoomIndex: number[] | undefined
        let toRoomIndex: number[] | undefined
        let superComponentIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { facingFlipped = (await localTable.getBooleanArray("byte:FacingFlipped")) })(),
            (async () => { facingOrientation_X = (await localTable.getNumberArray("float:FacingOrientation.X")) })(),
            (async () => { facingOrientation_Y = (await localTable.getNumberArray("float:FacingOrientation.Y")) })(),
            (async () => { facingOrientation_Z = (await localTable.getNumberArray("float:FacingOrientation.Z")) })(),
            (async () => { handFlipped = (await localTable.getBooleanArray("byte:HandFlipped")) })(),
            (async () => { mirrored = (await localTable.getBooleanArray("byte:Mirrored")) })(),
            (async () => { hasModifiedGeometry = (await localTable.getBooleanArray("byte:HasModifiedGeometry")) })(),
            (async () => { scale = (await localTable.getNumberArray("float:Scale")) })(),
            (async () => { basisX_X = (await localTable.getNumberArray("float:BasisX.X")) })(),
            (async () => { basisX_Y = (await localTable.getNumberArray("float:BasisX.Y")) })(),
            (async () => { basisX_Z = (await localTable.getNumberArray("float:BasisX.Z")) })(),
            (async () => { basisY_X = (await localTable.getNumberArray("float:BasisY.X")) })(),
            (async () => { basisY_Y = (await localTable.getNumberArray("float:BasisY.Y")) })(),
            (async () => { basisY_Z = (await localTable.getNumberArray("float:BasisY.Z")) })(),
            (async () => { basisZ_X = (await localTable.getNumberArray("float:BasisZ.X")) })(),
            (async () => { basisZ_Y = (await localTable.getNumberArray("float:BasisZ.Y")) })(),
            (async () => { basisZ_Z = (await localTable.getNumberArray("float:BasisZ.Z")) })(),
            (async () => { translation_X = (await localTable.getNumberArray("float:Translation.X")) })(),
            (async () => { translation_Y = (await localTable.getNumberArray("float:Translation.Y")) })(),
            (async () => { translation_Z = (await localTable.getNumberArray("float:Translation.Z")) })(),
            (async () => { handOrientation_X = (await localTable.getNumberArray("float:HandOrientation.X")) })(),
            (async () => { handOrientation_Y = (await localTable.getNumberArray("float:HandOrientation.Y")) })(),
            (async () => { handOrientation_Z = (await localTable.getNumberArray("float:HandOrientation.Z")) })(),
            (async () => { familyTypeIndex = (await localTable.getNumberArray("index:Vim.FamilyType:FamilyType")) })(),
            (async () => { hostIndex = (await localTable.getNumberArray("index:Vim.Element:Host")) })(),
            (async () => { fromRoomIndex = (await localTable.getNumberArray("index:Vim.Room:FromRoom")) })(),
            (async () => { toRoomIndex = (await localTable.getNumberArray("index:Vim.Room:ToRoom")) })(),
            (async () => { superComponentIndex = (await localTable.getNumberArray("index:Vim.Element:SuperComponent")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let familyInstance: IFamilyInstance[] = []
        
        const rowCount = await this.getCount()
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
                superComponentIndex: superComponentIndex ? superComponentIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return familyInstance
    }
    
    async getFacingFlipped(familyInstanceIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(familyInstanceIndex, "byte:FacingFlipped"))
    }
    
    async getAllFacingFlipped(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:FacingFlipped"))
    }
    
    async getFacingOrientation_X(familyInstanceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:FacingOrientation.X"))
    }
    
    async getAllFacingOrientation_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:FacingOrientation.X"))
    }
    
    async getFacingOrientation_Y(familyInstanceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:FacingOrientation.Y"))
    }
    
    async getAllFacingOrientation_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:FacingOrientation.Y"))
    }
    
    async getFacingOrientation_Z(familyInstanceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:FacingOrientation.Z"))
    }
    
    async getAllFacingOrientation_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:FacingOrientation.Z"))
    }
    
    async getHandFlipped(familyInstanceIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(familyInstanceIndex, "byte:HandFlipped"))
    }
    
    async getAllHandFlipped(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:HandFlipped"))
    }
    
    async getMirrored(familyInstanceIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(familyInstanceIndex, "byte:Mirrored"))
    }
    
    async getAllMirrored(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:Mirrored"))
    }
    
    async getHasModifiedGeometry(familyInstanceIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(familyInstanceIndex, "byte:HasModifiedGeometry"))
    }
    
    async getAllHasModifiedGeometry(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:HasModifiedGeometry"))
    }
    
    async getScale(familyInstanceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:Scale"))
    }
    
    async getAllScale(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:Scale"))
    }
    
    async getBasisX_X(familyInstanceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:BasisX.X"))
    }
    
    async getAllBasisX_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:BasisX.X"))
    }
    
    async getBasisX_Y(familyInstanceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:BasisX.Y"))
    }
    
    async getAllBasisX_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:BasisX.Y"))
    }
    
    async getBasisX_Z(familyInstanceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:BasisX.Z"))
    }
    
    async getAllBasisX_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:BasisX.Z"))
    }
    
    async getBasisY_X(familyInstanceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:BasisY.X"))
    }
    
    async getAllBasisY_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:BasisY.X"))
    }
    
    async getBasisY_Y(familyInstanceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:BasisY.Y"))
    }
    
    async getAllBasisY_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:BasisY.Y"))
    }
    
    async getBasisY_Z(familyInstanceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:BasisY.Z"))
    }
    
    async getAllBasisY_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:BasisY.Z"))
    }
    
    async getBasisZ_X(familyInstanceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:BasisZ.X"))
    }
    
    async getAllBasisZ_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:BasisZ.X"))
    }
    
    async getBasisZ_Y(familyInstanceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:BasisZ.Y"))
    }
    
    async getAllBasisZ_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:BasisZ.Y"))
    }
    
    async getBasisZ_Z(familyInstanceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:BasisZ.Z"))
    }
    
    async getAllBasisZ_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:BasisZ.Z"))
    }
    
    async getTranslation_X(familyInstanceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:Translation.X"))
    }
    
    async getAllTranslation_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:Translation.X"))
    }
    
    async getTranslation_Y(familyInstanceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:Translation.Y"))
    }
    
    async getAllTranslation_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:Translation.Y"))
    }
    
    async getTranslation_Z(familyInstanceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:Translation.Z"))
    }
    
    async getAllTranslation_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:Translation.Z"))
    }
    
    async getHandOrientation_X(familyInstanceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:HandOrientation.X"))
    }
    
    async getAllHandOrientation_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:HandOrientation.X"))
    }
    
    async getHandOrientation_Y(familyInstanceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:HandOrientation.Y"))
    }
    
    async getAllHandOrientation_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:HandOrientation.Y"))
    }
    
    async getHandOrientation_Z(familyInstanceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyInstanceIndex, "float:HandOrientation.Z"))
    }
    
    async getAllHandOrientation_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:HandOrientation.Z"))
    }
    
    async getFamilyTypeIndex(familyInstanceIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(familyInstanceIndex, "index:Vim.FamilyType:FamilyType")
    }
    
    async getAllFamilyTypeIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.FamilyType:FamilyType")
    }
    
    async getFamilyType(familyInstanceIndex: number): Promise<IFamilyType | undefined> {
        const index = await this.getFamilyTypeIndex(familyInstanceIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.familyType?.get(index)
    }
    
    async getHostIndex(familyInstanceIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(familyInstanceIndex, "index:Vim.Element:Host")
    }
    
    async getAllHostIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Host")
    }
    
    async getHost(familyInstanceIndex: number): Promise<IElement | undefined> {
        const index = await this.getHostIndex(familyInstanceIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
    async getFromRoomIndex(familyInstanceIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(familyInstanceIndex, "index:Vim.Room:FromRoom")
    }
    
    async getAllFromRoomIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Room:FromRoom")
    }
    
    async getFromRoom(familyInstanceIndex: number): Promise<IRoom | undefined> {
        const index = await this.getFromRoomIndex(familyInstanceIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.room?.get(index)
    }
    
    async getToRoomIndex(familyInstanceIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(familyInstanceIndex, "index:Vim.Room:ToRoom")
    }
    
    async getAllToRoomIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Room:ToRoom")
    }
    
    async getToRoom(familyInstanceIndex: number): Promise<IRoom | undefined> {
        const index = await this.getToRoomIndex(familyInstanceIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.room?.get(index)
    }
    
    async getSuperComponentIndex(familyInstanceIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(familyInstanceIndex, "index:Vim.Element:SuperComponent")
    }
    
    async getAllSuperComponentIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:SuperComponent")
    }
    
    async getSuperComponent(familyInstanceIndex: number): Promise<IElement | undefined> {
        const index = await this.getSuperComponentIndex(familyInstanceIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
    async getElementIndex(familyInstanceIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(familyInstanceIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(familyInstanceIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(familyInstanceIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IView {
    index: number
    title?: string
    viewType?: string
    up_X?: number
    up_Y?: number
    up_Z?: number
    right_X?: number
    right_Y?: number
    right_Z?: number
    origin_X?: number
    origin_Y?: number
    origin_Z?: number
    viewDirection_X?: number
    viewDirection_Y?: number
    viewDirection_Z?: number
    viewPosition_X?: number
    viewPosition_Y?: number
    viewPosition_Z?: number
    scale?: number
    outline_Min_X?: number
    outline_Min_Y?: number
    outline_Max_X?: number
    outline_Max_Y?: number
    detailLevel?: number
    isAudited?: boolean
    discipline?: string
    projectBrowserPath?: string
    isCropBoxActive?: boolean
    isTemporaryHideIsolateActive?: boolean
    categoryOverrideCount?: number
    hiddenCategoryCount?: number
    filterCount?: number
    elementOverrideCount?: number
    hiddenElementCount?: number
    templateControlledSettings?: string
    graphicsRead?: boolean
    
    cameraIndex?: number
    camera?: ICamera
    familyTypeIndex?: number
    familyType?: IFamilyType
    viewTemplateIndex?: number
    viewTemplate?: IElement
    primaryViewIndex?: number
    primaryView?: IView
    phaseIndex?: number
    phase?: IPhase
    phaseFilterIndex?: number
    phaseFilter?: IPhaseFilter
    scopeBoxIndex?: number
    scopeBox?: IElement
    elementIndex?: number
    element?: IElement
}

export interface IViewTable {
    getCount(): Promise<number>
    get(viewIndex: number): Promise<IView>
    getAll(): Promise<IView[]>
    
    getTitle(viewIndex: number): Promise<string | undefined>
    getAllTitle(): Promise<string[] | undefined>
    getViewType(viewIndex: number): Promise<string | undefined>
    getAllViewType(): Promise<string[] | undefined>
    getUp_X(viewIndex: number): Promise<number | undefined>
    getAllUp_X(): Promise<number[] | undefined>
    getUp_Y(viewIndex: number): Promise<number | undefined>
    getAllUp_Y(): Promise<number[] | undefined>
    getUp_Z(viewIndex: number): Promise<number | undefined>
    getAllUp_Z(): Promise<number[] | undefined>
    getRight_X(viewIndex: number): Promise<number | undefined>
    getAllRight_X(): Promise<number[] | undefined>
    getRight_Y(viewIndex: number): Promise<number | undefined>
    getAllRight_Y(): Promise<number[] | undefined>
    getRight_Z(viewIndex: number): Promise<number | undefined>
    getAllRight_Z(): Promise<number[] | undefined>
    getOrigin_X(viewIndex: number): Promise<number | undefined>
    getAllOrigin_X(): Promise<number[] | undefined>
    getOrigin_Y(viewIndex: number): Promise<number | undefined>
    getAllOrigin_Y(): Promise<number[] | undefined>
    getOrigin_Z(viewIndex: number): Promise<number | undefined>
    getAllOrigin_Z(): Promise<number[] | undefined>
    getViewDirection_X(viewIndex: number): Promise<number | undefined>
    getAllViewDirection_X(): Promise<number[] | undefined>
    getViewDirection_Y(viewIndex: number): Promise<number | undefined>
    getAllViewDirection_Y(): Promise<number[] | undefined>
    getViewDirection_Z(viewIndex: number): Promise<number | undefined>
    getAllViewDirection_Z(): Promise<number[] | undefined>
    getViewPosition_X(viewIndex: number): Promise<number | undefined>
    getAllViewPosition_X(): Promise<number[] | undefined>
    getViewPosition_Y(viewIndex: number): Promise<number | undefined>
    getAllViewPosition_Y(): Promise<number[] | undefined>
    getViewPosition_Z(viewIndex: number): Promise<number | undefined>
    getAllViewPosition_Z(): Promise<number[] | undefined>
    getScale(viewIndex: number): Promise<number | undefined>
    getAllScale(): Promise<number[] | undefined>
    getOutline_Min_X(viewIndex: number): Promise<number | undefined>
    getAllOutline_Min_X(): Promise<number[] | undefined>
    getOutline_Min_Y(viewIndex: number): Promise<number | undefined>
    getAllOutline_Min_Y(): Promise<number[] | undefined>
    getOutline_Max_X(viewIndex: number): Promise<number | undefined>
    getAllOutline_Max_X(): Promise<number[] | undefined>
    getOutline_Max_Y(viewIndex: number): Promise<number | undefined>
    getAllOutline_Max_Y(): Promise<number[] | undefined>
    getDetailLevel(viewIndex: number): Promise<number | undefined>
    getAllDetailLevel(): Promise<number[] | undefined>
    getIsAudited(viewIndex: number): Promise<boolean | undefined>
    getAllIsAudited(): Promise<boolean[] | undefined>
    getDiscipline(viewIndex: number): Promise<string | undefined>
    getAllDiscipline(): Promise<string[] | undefined>
    getProjectBrowserPath(viewIndex: number): Promise<string | undefined>
    getAllProjectBrowserPath(): Promise<string[] | undefined>
    getIsCropBoxActive(viewIndex: number): Promise<boolean | undefined>
    getAllIsCropBoxActive(): Promise<boolean[] | undefined>
    getIsTemporaryHideIsolateActive(viewIndex: number): Promise<boolean | undefined>
    getAllIsTemporaryHideIsolateActive(): Promise<boolean[] | undefined>
    getCategoryOverrideCount(viewIndex: number): Promise<number | undefined>
    getAllCategoryOverrideCount(): Promise<number[] | undefined>
    getHiddenCategoryCount(viewIndex: number): Promise<number | undefined>
    getAllHiddenCategoryCount(): Promise<number[] | undefined>
    getFilterCount(viewIndex: number): Promise<number | undefined>
    getAllFilterCount(): Promise<number[] | undefined>
    getElementOverrideCount(viewIndex: number): Promise<number | undefined>
    getAllElementOverrideCount(): Promise<number[] | undefined>
    getHiddenElementCount(viewIndex: number): Promise<number | undefined>
    getAllHiddenElementCount(): Promise<number[] | undefined>
    getTemplateControlledSettings(viewIndex: number): Promise<string | undefined>
    getAllTemplateControlledSettings(): Promise<string[] | undefined>
    getGraphicsRead(viewIndex: number): Promise<boolean | undefined>
    getAllGraphicsRead(): Promise<boolean[] | undefined>
    
    getCameraIndex(viewIndex: number): Promise<number | undefined>
    getAllCameraIndex(): Promise<number[] | undefined>
    getCamera(viewIndex: number): Promise<ICamera | undefined>
    getFamilyTypeIndex(viewIndex: number): Promise<number | undefined>
    getAllFamilyTypeIndex(): Promise<number[] | undefined>
    getFamilyType(viewIndex: number): Promise<IFamilyType | undefined>
    getViewTemplateIndex(viewIndex: number): Promise<number | undefined>
    getAllViewTemplateIndex(): Promise<number[] | undefined>
    getViewTemplate(viewIndex: number): Promise<IElement | undefined>
    getPrimaryViewIndex(viewIndex: number): Promise<number | undefined>
    getAllPrimaryViewIndex(): Promise<number[] | undefined>
    getPrimaryView(viewIndex: number): Promise<IView | undefined>
    getPhaseIndex(viewIndex: number): Promise<number | undefined>
    getAllPhaseIndex(): Promise<number[] | undefined>
    getPhase(viewIndex: number): Promise<IPhase | undefined>
    getPhaseFilterIndex(viewIndex: number): Promise<number | undefined>
    getAllPhaseFilterIndex(): Promise<number[] | undefined>
    getPhaseFilter(viewIndex: number): Promise<IPhaseFilter | undefined>
    getScopeBoxIndex(viewIndex: number): Promise<number | undefined>
    getAllScopeBoxIndex(): Promise<number[] | undefined>
    getScopeBox(viewIndex: number): Promise<IElement | undefined>
    getElementIndex(viewIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(viewIndex: number): Promise<IElement | undefined>
}

export class View implements IView {
    index: number
    title?: string
    viewType?: string
    up_X?: number
    up_Y?: number
    up_Z?: number
    right_X?: number
    right_Y?: number
    right_Z?: number
    origin_X?: number
    origin_Y?: number
    origin_Z?: number
    viewDirection_X?: number
    viewDirection_Y?: number
    viewDirection_Z?: number
    viewPosition_X?: number
    viewPosition_Y?: number
    viewPosition_Z?: number
    scale?: number
    outline_Min_X?: number
    outline_Min_Y?: number
    outline_Max_X?: number
    outline_Max_Y?: number
    detailLevel?: number
    isAudited?: boolean
    discipline?: string
    projectBrowserPath?: string
    isCropBoxActive?: boolean
    isTemporaryHideIsolateActive?: boolean
    categoryOverrideCount?: number
    hiddenCategoryCount?: number
    filterCount?: number
    elementOverrideCount?: number
    hiddenElementCount?: number
    templateControlledSettings?: string
    graphicsRead?: boolean
    
    cameraIndex?: number
    camera?: ICamera
    familyTypeIndex?: number
    familyType?: IFamilyType
    viewTemplateIndex?: number
    viewTemplate?: IElement
    primaryViewIndex?: number
    primaryView?: IView
    phaseIndex?: number
    phase?: IPhase
    phaseFilterIndex?: number
    phaseFilter?: IPhaseFilter
    scopeBoxIndex?: number
    scopeBox?: IElement
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IViewTable, index: number): Promise<IView> {
        let result = new View()
        result.index = index
        
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
            table.getIsAudited(index).then(v => result.isAudited = v),
            table.getDiscipline(index).then(v => result.discipline = v),
            table.getProjectBrowserPath(index).then(v => result.projectBrowserPath = v),
            table.getIsCropBoxActive(index).then(v => result.isCropBoxActive = v),
            table.getIsTemporaryHideIsolateActive(index).then(v => result.isTemporaryHideIsolateActive = v),
            table.getCategoryOverrideCount(index).then(v => result.categoryOverrideCount = v),
            table.getHiddenCategoryCount(index).then(v => result.hiddenCategoryCount = v),
            table.getFilterCount(index).then(v => result.filterCount = v),
            table.getElementOverrideCount(index).then(v => result.elementOverrideCount = v),
            table.getHiddenElementCount(index).then(v => result.hiddenElementCount = v),
            table.getTemplateControlledSettings(index).then(v => result.templateControlledSettings = v),
            table.getGraphicsRead(index).then(v => result.graphicsRead = v),
            table.getCameraIndex(index).then(v => result.cameraIndex = v),
            table.getFamilyTypeIndex(index).then(v => result.familyTypeIndex = v),
            table.getViewTemplateIndex(index).then(v => result.viewTemplateIndex = v),
            table.getPrimaryViewIndex(index).then(v => result.primaryViewIndex = v),
            table.getPhaseIndex(index).then(v => result.phaseIndex = v),
            table.getPhaseFilterIndex(index).then(v => result.phaseFilterIndex = v),
            table.getScopeBoxIndex(index).then(v => result.scopeBoxIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class ViewTable implements IViewTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IViewTable | undefined> {
        const entity = await document.entities.getBfast("Vim.View")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ViewTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(viewIndex: number): Promise<IView> {
        return await View.createFromTable(this, viewIndex)
    }
    
    async getAll(): Promise<IView[]> {
        const localTable = await this.entityTable.getLocal()
        
        let title: string[] | undefined
        let viewType: string[] | undefined
        let up_X: number[] | undefined
        let up_Y: number[] | undefined
        let up_Z: number[] | undefined
        let right_X: number[] | undefined
        let right_Y: number[] | undefined
        let right_Z: number[] | undefined
        let origin_X: number[] | undefined
        let origin_Y: number[] | undefined
        let origin_Z: number[] | undefined
        let viewDirection_X: number[] | undefined
        let viewDirection_Y: number[] | undefined
        let viewDirection_Z: number[] | undefined
        let viewPosition_X: number[] | undefined
        let viewPosition_Y: number[] | undefined
        let viewPosition_Z: number[] | undefined
        let scale: number[] | undefined
        let outline_Min_X: number[] | undefined
        let outline_Min_Y: number[] | undefined
        let outline_Max_X: number[] | undefined
        let outline_Max_Y: number[] | undefined
        let detailLevel: number[] | undefined
        let isAudited: boolean[] | undefined
        let discipline: string[] | undefined
        let projectBrowserPath: string[] | undefined
        let isCropBoxActive: boolean[] | undefined
        let isTemporaryHideIsolateActive: boolean[] | undefined
        let categoryOverrideCount: number[] | undefined
        let hiddenCategoryCount: number[] | undefined
        let filterCount: number[] | undefined
        let elementOverrideCount: number[] | undefined
        let hiddenElementCount: number[] | undefined
        let templateControlledSettings: string[] | undefined
        let graphicsRead: boolean[] | undefined
        let cameraIndex: number[] | undefined
        let familyTypeIndex: number[] | undefined
        let viewTemplateIndex: number[] | undefined
        let primaryViewIndex: number[] | undefined
        let phaseIndex: number[] | undefined
        let phaseFilterIndex: number[] | undefined
        let scopeBoxIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { title = (await localTable.getStringArray("string:Title")) })(),
            (async () => { viewType = (await localTable.getStringArray("string:ViewType")) })(),
            (async () => { up_X = (await localTable.getNumberArray("double:Up.X")) })(),
            (async () => { up_Y = (await localTable.getNumberArray("double:Up.Y")) })(),
            (async () => { up_Z = (await localTable.getNumberArray("double:Up.Z")) })(),
            (async () => { right_X = (await localTable.getNumberArray("double:Right.X")) })(),
            (async () => { right_Y = (await localTable.getNumberArray("double:Right.Y")) })(),
            (async () => { right_Z = (await localTable.getNumberArray("double:Right.Z")) })(),
            (async () => { origin_X = (await localTable.getNumberArray("double:Origin.X")) })(),
            (async () => { origin_Y = (await localTable.getNumberArray("double:Origin.Y")) })(),
            (async () => { origin_Z = (await localTable.getNumberArray("double:Origin.Z")) })(),
            (async () => { viewDirection_X = (await localTable.getNumberArray("double:ViewDirection.X")) })(),
            (async () => { viewDirection_Y = (await localTable.getNumberArray("double:ViewDirection.Y")) })(),
            (async () => { viewDirection_Z = (await localTable.getNumberArray("double:ViewDirection.Z")) })(),
            (async () => { viewPosition_X = (await localTable.getNumberArray("double:ViewPosition.X")) })(),
            (async () => { viewPosition_Y = (await localTable.getNumberArray("double:ViewPosition.Y")) })(),
            (async () => { viewPosition_Z = (await localTable.getNumberArray("double:ViewPosition.Z")) })(),
            (async () => { scale = (await localTable.getNumberArray("double:Scale")) })(),
            (async () => { outline_Min_X = (await localTable.getNumberArray("double:Outline.Min.X")) })(),
            (async () => { outline_Min_Y = (await localTable.getNumberArray("double:Outline.Min.Y")) })(),
            (async () => { outline_Max_X = (await localTable.getNumberArray("double:Outline.Max.X")) })(),
            (async () => { outline_Max_Y = (await localTable.getNumberArray("double:Outline.Max.Y")) })(),
            (async () => { detailLevel = (await localTable.getNumberArray("int:DetailLevel")) })(),
            (async () => { isAudited = (await localTable.getBooleanArray("byte:IsAudited")) })(),
            (async () => { discipline = (await localTable.getStringArray("string:Discipline")) })(),
            (async () => { projectBrowserPath = (await localTable.getStringArray("string:ProjectBrowserPath")) })(),
            (async () => { isCropBoxActive = (await localTable.getBooleanArray("byte:IsCropBoxActive")) })(),
            (async () => { isTemporaryHideIsolateActive = (await localTable.getBooleanArray("byte:IsTemporaryHideIsolateActive")) })(),
            (async () => { categoryOverrideCount = (await localTable.getNumberArray("int:CategoryOverrideCount")) })(),
            (async () => { hiddenCategoryCount = (await localTable.getNumberArray("int:HiddenCategoryCount")) })(),
            (async () => { filterCount = (await localTable.getNumberArray("int:FilterCount")) })(),
            (async () => { elementOverrideCount = (await localTable.getNumberArray("int:ElementOverrideCount")) })(),
            (async () => { hiddenElementCount = (await localTable.getNumberArray("int:HiddenElementCount")) })(),
            (async () => { templateControlledSettings = (await localTable.getStringArray("string:TemplateControlledSettings")) })(),
            (async () => { graphicsRead = (await localTable.getBooleanArray("byte:GraphicsRead")) })(),
            (async () => { cameraIndex = (await localTable.getNumberArray("index:Vim.Camera:Camera")) })(),
            (async () => { familyTypeIndex = (await localTable.getNumberArray("index:Vim.FamilyType:FamilyType")) })(),
            (async () => { viewTemplateIndex = (await localTable.getNumberArray("index:Vim.Element:ViewTemplate")) })(),
            (async () => { primaryViewIndex = (await localTable.getNumberArray("index:Vim.View:PrimaryView")) })(),
            (async () => { phaseIndex = (await localTable.getNumberArray("index:Vim.Phase:Phase")) })(),
            (async () => { phaseFilterIndex = (await localTable.getNumberArray("index:Vim.PhaseFilter:PhaseFilter")) })(),
            (async () => { scopeBoxIndex = (await localTable.getNumberArray("index:Vim.Element:ScopeBox")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let view: IView[] = []
        
        const rowCount = await this.getCount()
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
                isAudited: isAudited ? isAudited[i] : undefined,
                discipline: discipline ? discipline[i] : undefined,
                projectBrowserPath: projectBrowserPath ? projectBrowserPath[i] : undefined,
                isCropBoxActive: isCropBoxActive ? isCropBoxActive[i] : undefined,
                isTemporaryHideIsolateActive: isTemporaryHideIsolateActive ? isTemporaryHideIsolateActive[i] : undefined,
                categoryOverrideCount: categoryOverrideCount ? categoryOverrideCount[i] : undefined,
                hiddenCategoryCount: hiddenCategoryCount ? hiddenCategoryCount[i] : undefined,
                filterCount: filterCount ? filterCount[i] : undefined,
                elementOverrideCount: elementOverrideCount ? elementOverrideCount[i] : undefined,
                hiddenElementCount: hiddenElementCount ? hiddenElementCount[i] : undefined,
                templateControlledSettings: templateControlledSettings ? templateControlledSettings[i] : undefined,
                graphicsRead: graphicsRead ? graphicsRead[i] : undefined,
                cameraIndex: cameraIndex ? cameraIndex[i] : undefined,
                familyTypeIndex: familyTypeIndex ? familyTypeIndex[i] : undefined,
                viewTemplateIndex: viewTemplateIndex ? viewTemplateIndex[i] : undefined,
                primaryViewIndex: primaryViewIndex ? primaryViewIndex[i] : undefined,
                phaseIndex: phaseIndex ? phaseIndex[i] : undefined,
                phaseFilterIndex: phaseFilterIndex ? phaseFilterIndex[i] : undefined,
                scopeBoxIndex: scopeBoxIndex ? scopeBoxIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return view
    }
    
    async getTitle(viewIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(viewIndex, "string:Title"))
    }
    
    async getAllTitle(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Title"))
    }
    
    async getViewType(viewIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(viewIndex, "string:ViewType"))
    }
    
    async getAllViewType(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:ViewType"))
    }
    
    async getUp_X(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "double:Up.X"))
    }
    
    async getAllUp_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Up.X"))
    }
    
    async getUp_Y(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "double:Up.Y"))
    }
    
    async getAllUp_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Up.Y"))
    }
    
    async getUp_Z(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "double:Up.Z"))
    }
    
    async getAllUp_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Up.Z"))
    }
    
    async getRight_X(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "double:Right.X"))
    }
    
    async getAllRight_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Right.X"))
    }
    
    async getRight_Y(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "double:Right.Y"))
    }
    
    async getAllRight_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Right.Y"))
    }
    
    async getRight_Z(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "double:Right.Z"))
    }
    
    async getAllRight_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Right.Z"))
    }
    
    async getOrigin_X(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "double:Origin.X"))
    }
    
    async getAllOrigin_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Origin.X"))
    }
    
    async getOrigin_Y(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "double:Origin.Y"))
    }
    
    async getAllOrigin_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Origin.Y"))
    }
    
    async getOrigin_Z(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "double:Origin.Z"))
    }
    
    async getAllOrigin_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Origin.Z"))
    }
    
    async getViewDirection_X(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "double:ViewDirection.X"))
    }
    
    async getAllViewDirection_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:ViewDirection.X"))
    }
    
    async getViewDirection_Y(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "double:ViewDirection.Y"))
    }
    
    async getAllViewDirection_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:ViewDirection.Y"))
    }
    
    async getViewDirection_Z(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "double:ViewDirection.Z"))
    }
    
    async getAllViewDirection_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:ViewDirection.Z"))
    }
    
    async getViewPosition_X(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "double:ViewPosition.X"))
    }
    
    async getAllViewPosition_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:ViewPosition.X"))
    }
    
    async getViewPosition_Y(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "double:ViewPosition.Y"))
    }
    
    async getAllViewPosition_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:ViewPosition.Y"))
    }
    
    async getViewPosition_Z(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "double:ViewPosition.Z"))
    }
    
    async getAllViewPosition_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:ViewPosition.Z"))
    }
    
    async getScale(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "double:Scale"))
    }
    
    async getAllScale(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Scale"))
    }
    
    async getOutline_Min_X(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "double:Outline.Min.X"))
    }
    
    async getAllOutline_Min_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Outline.Min.X"))
    }
    
    async getOutline_Min_Y(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "double:Outline.Min.Y"))
    }
    
    async getAllOutline_Min_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Outline.Min.Y"))
    }
    
    async getOutline_Max_X(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "double:Outline.Max.X"))
    }
    
    async getAllOutline_Max_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Outline.Max.X"))
    }
    
    async getOutline_Max_Y(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "double:Outline.Max.Y"))
    }
    
    async getAllOutline_Max_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Outline.Max.Y"))
    }
    
    async getDetailLevel(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "int:DetailLevel"))
    }
    
    async getAllDetailLevel(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:DetailLevel"))
    }
    
    async getIsAudited(viewIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(viewIndex, "byte:IsAudited"))
    }
    
    async getAllIsAudited(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsAudited"))
    }
    
    async getDiscipline(viewIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(viewIndex, "string:Discipline"))
    }
    
    async getAllDiscipline(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Discipline"))
    }
    
    async getProjectBrowserPath(viewIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(viewIndex, "string:ProjectBrowserPath"))
    }
    
    async getAllProjectBrowserPath(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:ProjectBrowserPath"))
    }
    
    async getIsCropBoxActive(viewIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(viewIndex, "byte:IsCropBoxActive"))
    }
    
    async getAllIsCropBoxActive(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsCropBoxActive"))
    }
    
    async getIsTemporaryHideIsolateActive(viewIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(viewIndex, "byte:IsTemporaryHideIsolateActive"))
    }
    
    async getAllIsTemporaryHideIsolateActive(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsTemporaryHideIsolateActive"))
    }
    
    async getCategoryOverrideCount(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "int:CategoryOverrideCount"))
    }
    
    async getAllCategoryOverrideCount(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:CategoryOverrideCount"))
    }
    
    async getHiddenCategoryCount(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "int:HiddenCategoryCount"))
    }
    
    async getAllHiddenCategoryCount(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:HiddenCategoryCount"))
    }
    
    async getFilterCount(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "int:FilterCount"))
    }
    
    async getAllFilterCount(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:FilterCount"))
    }
    
    async getElementOverrideCount(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "int:ElementOverrideCount"))
    }
    
    async getAllElementOverrideCount(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:ElementOverrideCount"))
    }
    
    async getHiddenElementCount(viewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(viewIndex, "int:HiddenElementCount"))
    }
    
    async getAllHiddenElementCount(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:HiddenElementCount"))
    }
    
    async getTemplateControlledSettings(viewIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(viewIndex, "string:TemplateControlledSettings"))
    }
    
    async getAllTemplateControlledSettings(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:TemplateControlledSettings"))
    }
    
    async getGraphicsRead(viewIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(viewIndex, "byte:GraphicsRead"))
    }
    
    async getAllGraphicsRead(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:GraphicsRead"))
    }
    
    async getCameraIndex(viewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(viewIndex, "index:Vim.Camera:Camera")
    }
    
    async getAllCameraIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Camera:Camera")
    }
    
    async getCamera(viewIndex: number): Promise<ICamera | undefined> {
        const index = await this.getCameraIndex(viewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.camera?.get(index)
    }
    
    async getFamilyTypeIndex(viewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(viewIndex, "index:Vim.FamilyType:FamilyType")
    }
    
    async getAllFamilyTypeIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.FamilyType:FamilyType")
    }
    
    async getFamilyType(viewIndex: number): Promise<IFamilyType | undefined> {
        const index = await this.getFamilyTypeIndex(viewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.familyType?.get(index)
    }
    
    async getViewTemplateIndex(viewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(viewIndex, "index:Vim.Element:ViewTemplate")
    }
    
    async getAllViewTemplateIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:ViewTemplate")
    }
    
    async getViewTemplate(viewIndex: number): Promise<IElement | undefined> {
        const index = await this.getViewTemplateIndex(viewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
    async getPrimaryViewIndex(viewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(viewIndex, "index:Vim.View:PrimaryView")
    }
    
    async getAllPrimaryViewIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.View:PrimaryView")
    }
    
    async getPrimaryView(viewIndex: number): Promise<IView | undefined> {
        const index = await this.getPrimaryViewIndex(viewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.view?.get(index)
    }
    
    async getPhaseIndex(viewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(viewIndex, "index:Vim.Phase:Phase")
    }
    
    async getAllPhaseIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Phase:Phase")
    }
    
    async getPhase(viewIndex: number): Promise<IPhase | undefined> {
        const index = await this.getPhaseIndex(viewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.phase?.get(index)
    }
    
    async getPhaseFilterIndex(viewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(viewIndex, "index:Vim.PhaseFilter:PhaseFilter")
    }
    
    async getAllPhaseFilterIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.PhaseFilter:PhaseFilter")
    }
    
    async getPhaseFilter(viewIndex: number): Promise<IPhaseFilter | undefined> {
        const index = await this.getPhaseFilterIndex(viewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.phaseFilter?.get(index)
    }
    
    async getScopeBoxIndex(viewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(viewIndex, "index:Vim.Element:ScopeBox")
    }
    
    async getAllScopeBoxIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:ScopeBox")
    }
    
    async getScopeBox(viewIndex: number): Promise<IElement | undefined> {
        const index = await this.getScopeBoxIndex(viewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
    async getElementIndex(viewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(viewIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(viewIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(viewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IFilterInView {
    index: number
    name?: string
    kind?: string
    filterUniqueId?: string
    orderIndex?: number
    isEnabled?: boolean
    isVisible?: boolean
    hasOverrides?: boolean
    
    viewIndex?: number
    view?: IView
    graphicOverrideIndex?: number
    graphicOverride?: IGraphicOverride
}

export interface IFilterInViewTable {
    getCount(): Promise<number>
    get(filterInViewIndex: number): Promise<IFilterInView>
    getAll(): Promise<IFilterInView[]>
    
    getName(filterInViewIndex: number): Promise<string | undefined>
    getAllName(): Promise<string[] | undefined>
    getKind(filterInViewIndex: number): Promise<string | undefined>
    getAllKind(): Promise<string[] | undefined>
    getFilterUniqueId(filterInViewIndex: number): Promise<string | undefined>
    getAllFilterUniqueId(): Promise<string[] | undefined>
    getOrderIndex(filterInViewIndex: number): Promise<number | undefined>
    getAllOrderIndex(): Promise<number[] | undefined>
    getIsEnabled(filterInViewIndex: number): Promise<boolean | undefined>
    getAllIsEnabled(): Promise<boolean[] | undefined>
    getIsVisible(filterInViewIndex: number): Promise<boolean | undefined>
    getAllIsVisible(): Promise<boolean[] | undefined>
    getHasOverrides(filterInViewIndex: number): Promise<boolean | undefined>
    getAllHasOverrides(): Promise<boolean[] | undefined>
    
    getViewIndex(filterInViewIndex: number): Promise<number | undefined>
    getAllViewIndex(): Promise<number[] | undefined>
    getView(filterInViewIndex: number): Promise<IView | undefined>
    getGraphicOverrideIndex(filterInViewIndex: number): Promise<number | undefined>
    getAllGraphicOverrideIndex(): Promise<number[] | undefined>
    getGraphicOverride(filterInViewIndex: number): Promise<IGraphicOverride | undefined>
}

export class FilterInView implements IFilterInView {
    index: number
    name?: string
    kind?: string
    filterUniqueId?: string
    orderIndex?: number
    isEnabled?: boolean
    isVisible?: boolean
    hasOverrides?: boolean
    
    viewIndex?: number
    view?: IView
    graphicOverrideIndex?: number
    graphicOverride?: IGraphicOverride
    
    static async createFromTable(table: IFilterInViewTable, index: number): Promise<IFilterInView> {
        let result = new FilterInView()
        result.index = index
        
        await Promise.all([
            table.getName(index).then(v => result.name = v),
            table.getKind(index).then(v => result.kind = v),
            table.getFilterUniqueId(index).then(v => result.filterUniqueId = v),
            table.getOrderIndex(index).then(v => result.orderIndex = v),
            table.getIsEnabled(index).then(v => result.isEnabled = v),
            table.getIsVisible(index).then(v => result.isVisible = v),
            table.getHasOverrides(index).then(v => result.hasOverrides = v),
            table.getViewIndex(index).then(v => result.viewIndex = v),
            table.getGraphicOverrideIndex(index).then(v => result.graphicOverrideIndex = v),
        ])
        
        return result
    }
}

export class FilterInViewTable implements IFilterInViewTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IFilterInViewTable | undefined> {
        const entity = await document.entities.getBfast("Vim.FilterInView")
        
        if (!entity) {
            return undefined
        }
        
        let table = new FilterInViewTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(filterInViewIndex: number): Promise<IFilterInView> {
        return await FilterInView.createFromTable(this, filterInViewIndex)
    }
    
    async getAll(): Promise<IFilterInView[]> {
        const localTable = await this.entityTable.getLocal()
        
        let name: string[] | undefined
        let kind: string[] | undefined
        let filterUniqueId: string[] | undefined
        let orderIndex: number[] | undefined
        let isEnabled: boolean[] | undefined
        let isVisible: boolean[] | undefined
        let hasOverrides: boolean[] | undefined
        let viewIndex: number[] | undefined
        let graphicOverrideIndex: number[] | undefined
        
        await Promise.all([
            (async () => { name = (await localTable.getStringArray("string:Name")) })(),
            (async () => { kind = (await localTable.getStringArray("string:Kind")) })(),
            (async () => { filterUniqueId = (await localTable.getStringArray("string:FilterUniqueId")) })(),
            (async () => { orderIndex = (await localTable.getNumberArray("int:OrderIndex")) })(),
            (async () => { isEnabled = (await localTable.getBooleanArray("byte:IsEnabled")) })(),
            (async () => { isVisible = (await localTable.getBooleanArray("byte:IsVisible")) })(),
            (async () => { hasOverrides = (await localTable.getBooleanArray("byte:HasOverrides")) })(),
            (async () => { viewIndex = (await localTable.getNumberArray("index:Vim.View:View")) })(),
            (async () => { graphicOverrideIndex = (await localTable.getNumberArray("index:Vim.GraphicOverride:GraphicOverride")) })(),
        ])
        
        let filterInView: IFilterInView[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            filterInView.push({
                index: i,
                name: name ? name[i] : undefined,
                kind: kind ? kind[i] : undefined,
                filterUniqueId: filterUniqueId ? filterUniqueId[i] : undefined,
                orderIndex: orderIndex ? orderIndex[i] : undefined,
                isEnabled: isEnabled ? isEnabled[i] : undefined,
                isVisible: isVisible ? isVisible[i] : undefined,
                hasOverrides: hasOverrides ? hasOverrides[i] : undefined,
                viewIndex: viewIndex ? viewIndex[i] : undefined,
                graphicOverrideIndex: graphicOverrideIndex ? graphicOverrideIndex[i] : undefined
            })
        }
        
        return filterInView
    }
    
    async getName(filterInViewIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(filterInViewIndex, "string:Name"))
    }
    
    async getAllName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Name"))
    }
    
    async getKind(filterInViewIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(filterInViewIndex, "string:Kind"))
    }
    
    async getAllKind(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Kind"))
    }
    
    async getFilterUniqueId(filterInViewIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(filterInViewIndex, "string:FilterUniqueId"))
    }
    
    async getAllFilterUniqueId(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:FilterUniqueId"))
    }
    
    async getOrderIndex(filterInViewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(filterInViewIndex, "int:OrderIndex"))
    }
    
    async getAllOrderIndex(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:OrderIndex"))
    }
    
    async getIsEnabled(filterInViewIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(filterInViewIndex, "byte:IsEnabled"))
    }
    
    async getAllIsEnabled(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsEnabled"))
    }
    
    async getIsVisible(filterInViewIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(filterInViewIndex, "byte:IsVisible"))
    }
    
    async getAllIsVisible(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsVisible"))
    }
    
    async getHasOverrides(filterInViewIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(filterInViewIndex, "byte:HasOverrides"))
    }
    
    async getAllHasOverrides(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:HasOverrides"))
    }
    
    async getViewIndex(filterInViewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(filterInViewIndex, "index:Vim.View:View")
    }
    
    async getAllViewIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.View:View")
    }
    
    async getView(filterInViewIndex: number): Promise<IView | undefined> {
        const index = await this.getViewIndex(filterInViewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.view?.get(index)
    }
    
    async getGraphicOverrideIndex(filterInViewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(filterInViewIndex, "index:Vim.GraphicOverride:GraphicOverride")
    }
    
    async getAllGraphicOverrideIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.GraphicOverride:GraphicOverride")
    }
    
    async getGraphicOverride(filterInViewIndex: number): Promise<IGraphicOverride | undefined> {
        const index = await this.getGraphicOverrideIndex(filterInViewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.graphicOverride?.get(index)
    }
    
}

export interface IGridInView {
    index: number
    bubbleAtEnd0?: boolean
    bubbleAtEnd1?: boolean
    
    gridIndex?: number
    grid?: IGrid
    viewIndex?: number
    view?: IView
}

export interface IGridInViewTable {
    getCount(): Promise<number>
    get(gridInViewIndex: number): Promise<IGridInView>
    getAll(): Promise<IGridInView[]>
    
    getBubbleAtEnd0(gridInViewIndex: number): Promise<boolean | undefined>
    getAllBubbleAtEnd0(): Promise<boolean[] | undefined>
    getBubbleAtEnd1(gridInViewIndex: number): Promise<boolean | undefined>
    getAllBubbleAtEnd1(): Promise<boolean[] | undefined>
    
    getGridIndex(gridInViewIndex: number): Promise<number | undefined>
    getAllGridIndex(): Promise<number[] | undefined>
    getGrid(gridInViewIndex: number): Promise<IGrid | undefined>
    getViewIndex(gridInViewIndex: number): Promise<number | undefined>
    getAllViewIndex(): Promise<number[] | undefined>
    getView(gridInViewIndex: number): Promise<IView | undefined>
}

export class GridInView implements IGridInView {
    index: number
    bubbleAtEnd0?: boolean
    bubbleAtEnd1?: boolean
    
    gridIndex?: number
    grid?: IGrid
    viewIndex?: number
    view?: IView
    
    static async createFromTable(table: IGridInViewTable, index: number): Promise<IGridInView> {
        let result = new GridInView()
        result.index = index
        
        await Promise.all([
            table.getBubbleAtEnd0(index).then(v => result.bubbleAtEnd0 = v),
            table.getBubbleAtEnd1(index).then(v => result.bubbleAtEnd1 = v),
            table.getGridIndex(index).then(v => result.gridIndex = v),
            table.getViewIndex(index).then(v => result.viewIndex = v),
        ])
        
        return result
    }
}

export class GridInViewTable implements IGridInViewTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IGridInViewTable | undefined> {
        const entity = await document.entities.getBfast("Vim.GridInView")
        
        if (!entity) {
            return undefined
        }
        
        let table = new GridInViewTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(gridInViewIndex: number): Promise<IGridInView> {
        return await GridInView.createFromTable(this, gridInViewIndex)
    }
    
    async getAll(): Promise<IGridInView[]> {
        const localTable = await this.entityTable.getLocal()
        
        let bubbleAtEnd0: boolean[] | undefined
        let bubbleAtEnd1: boolean[] | undefined
        let gridIndex: number[] | undefined
        let viewIndex: number[] | undefined
        
        await Promise.all([
            (async () => { bubbleAtEnd0 = (await localTable.getBooleanArray("byte:BubbleAtEnd0")) })(),
            (async () => { bubbleAtEnd1 = (await localTable.getBooleanArray("byte:BubbleAtEnd1")) })(),
            (async () => { gridIndex = (await localTable.getNumberArray("index:Vim.Grid:Grid")) })(),
            (async () => { viewIndex = (await localTable.getNumberArray("index:Vim.View:View")) })(),
        ])
        
        let gridInView: IGridInView[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            gridInView.push({
                index: i,
                bubbleAtEnd0: bubbleAtEnd0 ? bubbleAtEnd0[i] : undefined,
                bubbleAtEnd1: bubbleAtEnd1 ? bubbleAtEnd1[i] : undefined,
                gridIndex: gridIndex ? gridIndex[i] : undefined,
                viewIndex: viewIndex ? viewIndex[i] : undefined
            })
        }
        
        return gridInView
    }
    
    async getBubbleAtEnd0(gridInViewIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(gridInViewIndex, "byte:BubbleAtEnd0"))
    }
    
    async getAllBubbleAtEnd0(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:BubbleAtEnd0"))
    }
    
    async getBubbleAtEnd1(gridInViewIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(gridInViewIndex, "byte:BubbleAtEnd1"))
    }
    
    async getAllBubbleAtEnd1(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:BubbleAtEnd1"))
    }
    
    async getGridIndex(gridInViewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(gridInViewIndex, "index:Vim.Grid:Grid")
    }
    
    async getAllGridIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Grid:Grid")
    }
    
    async getGrid(gridInViewIndex: number): Promise<IGrid | undefined> {
        const index = await this.getGridIndex(gridInViewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.grid?.get(index)
    }
    
    async getViewIndex(gridInViewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(gridInViewIndex, "index:Vim.View:View")
    }
    
    async getAllViewIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.View:View")
    }
    
    async getView(gridInViewIndex: number): Promise<IView | undefined> {
        const index = await this.getViewIndex(gridInViewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.view?.get(index)
    }
    
}

export interface ILinePattern {
    index: number
    name?: string
    elementId?: bigint
    uniqueId?: string
    isSolid?: boolean
    segments?: string
    
    bimDocumentIndex?: number
    bimDocument?: IBimDocument
}

export interface ILinePatternTable {
    getCount(): Promise<number>
    get(linePatternIndex: number): Promise<ILinePattern>
    getAll(): Promise<ILinePattern[]>
    
    getName(linePatternIndex: number): Promise<string | undefined>
    getAllName(): Promise<string[] | undefined>
    getElementId(linePatternIndex: number): Promise<bigint | undefined>
    getAllElementId(): Promise<BigInt64Array | undefined>
    getUniqueId(linePatternIndex: number): Promise<string | undefined>
    getAllUniqueId(): Promise<string[] | undefined>
    getIsSolid(linePatternIndex: number): Promise<boolean | undefined>
    getAllIsSolid(): Promise<boolean[] | undefined>
    getSegments(linePatternIndex: number): Promise<string | undefined>
    getAllSegments(): Promise<string[] | undefined>
    
    getBimDocumentIndex(linePatternIndex: number): Promise<number | undefined>
    getAllBimDocumentIndex(): Promise<number[] | undefined>
    getBimDocument(linePatternIndex: number): Promise<IBimDocument | undefined>
}

export class LinePattern implements ILinePattern {
    index: number
    name?: string
    elementId?: bigint
    uniqueId?: string
    isSolid?: boolean
    segments?: string
    
    bimDocumentIndex?: number
    bimDocument?: IBimDocument
    
    static async createFromTable(table: ILinePatternTable, index: number): Promise<ILinePattern> {
        let result = new LinePattern()
        result.index = index
        
        await Promise.all([
            table.getName(index).then(v => result.name = v),
            table.getElementId(index).then(v => result.elementId = v),
            table.getUniqueId(index).then(v => result.uniqueId = v),
            table.getIsSolid(index).then(v => result.isSolid = v),
            table.getSegments(index).then(v => result.segments = v),
            table.getBimDocumentIndex(index).then(v => result.bimDocumentIndex = v),
        ])
        
        return result
    }
}

export class LinePatternTable implements ILinePatternTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<ILinePatternTable | undefined> {
        const entity = await document.entities.getBfast("Vim.LinePattern")
        
        if (!entity) {
            return undefined
        }
        
        let table = new LinePatternTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(linePatternIndex: number): Promise<ILinePattern> {
        return await LinePattern.createFromTable(this, linePatternIndex)
    }
    
    async getAll(): Promise<ILinePattern[]> {
        const localTable = await this.entityTable.getLocal()
        
        let name: string[] | undefined
        let elementId: BigInt64Array | undefined
        let uniqueId: string[] | undefined
        let isSolid: boolean[] | undefined
        let segments: string[] | undefined
        let bimDocumentIndex: number[] | undefined
        
        await Promise.all([
            (async () => { name = (await localTable.getStringArray("string:Name")) })(),
            (async () => { elementId = (await localTable.getBigIntArray("long:ElementId")) })(),
            (async () => { uniqueId = (await localTable.getStringArray("string:UniqueId")) })(),
            (async () => { isSolid = (await localTable.getBooleanArray("byte:IsSolid")) })(),
            (async () => { segments = (await localTable.getStringArray("string:Segments")) })(),
            (async () => { bimDocumentIndex = (await localTable.getNumberArray("index:Vim.BimDocument:BimDocument")) })(),
        ])
        
        let linePattern: ILinePattern[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            linePattern.push({
                index: i,
                name: name ? name[i] : undefined,
                elementId: elementId ? elementId[i] : undefined,
                uniqueId: uniqueId ? uniqueId[i] : undefined,
                isSolid: isSolid ? isSolid[i] : undefined,
                segments: segments ? segments[i] : undefined,
                bimDocumentIndex: bimDocumentIndex ? bimDocumentIndex[i] : undefined
            })
        }
        
        return linePattern
    }
    
    async getName(linePatternIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(linePatternIndex, "string:Name"))
    }
    
    async getAllName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Name"))
    }
    
    async getElementId(linePatternIndex: number): Promise<bigint | undefined> {
        return (await this.entityTable.getBigInt(linePatternIndex, "long:ElementId"))
    }
    
    async getAllElementId(): Promise<BigInt64Array | undefined> {
        return (await this.entityTable.getBigIntArray("long:ElementId"))
    }
    
    async getUniqueId(linePatternIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(linePatternIndex, "string:UniqueId"))
    }
    
    async getAllUniqueId(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:UniqueId"))
    }
    
    async getIsSolid(linePatternIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(linePatternIndex, "byte:IsSolid"))
    }
    
    async getAllIsSolid(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsSolid"))
    }
    
    async getSegments(linePatternIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(linePatternIndex, "string:Segments"))
    }
    
    async getAllSegments(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Segments"))
    }
    
    async getBimDocumentIndex(linePatternIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(linePatternIndex, "index:Vim.BimDocument:BimDocument")
    }
    
    async getAllBimDocumentIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.BimDocument:BimDocument")
    }
    
    async getBimDocument(linePatternIndex: number): Promise<IBimDocument | undefined> {
        const index = await this.getBimDocumentIndex(linePatternIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.bimDocument?.get(index)
    }
    
}

export interface IFillPattern {
    index: number
    name?: string
    elementId?: bigint
    uniqueId?: string
    target?: string
    isSolidFill?: boolean
    hostOrientation?: string
    gridCount?: number
    
    bimDocumentIndex?: number
    bimDocument?: IBimDocument
}

export interface IFillPatternTable {
    getCount(): Promise<number>
    get(fillPatternIndex: number): Promise<IFillPattern>
    getAll(): Promise<IFillPattern[]>
    
    getName(fillPatternIndex: number): Promise<string | undefined>
    getAllName(): Promise<string[] | undefined>
    getElementId(fillPatternIndex: number): Promise<bigint | undefined>
    getAllElementId(): Promise<BigInt64Array | undefined>
    getUniqueId(fillPatternIndex: number): Promise<string | undefined>
    getAllUniqueId(): Promise<string[] | undefined>
    getTarget(fillPatternIndex: number): Promise<string | undefined>
    getAllTarget(): Promise<string[] | undefined>
    getIsSolidFill(fillPatternIndex: number): Promise<boolean | undefined>
    getAllIsSolidFill(): Promise<boolean[] | undefined>
    getHostOrientation(fillPatternIndex: number): Promise<string | undefined>
    getAllHostOrientation(): Promise<string[] | undefined>
    getGridCount(fillPatternIndex: number): Promise<number | undefined>
    getAllGridCount(): Promise<number[] | undefined>
    
    getBimDocumentIndex(fillPatternIndex: number): Promise<number | undefined>
    getAllBimDocumentIndex(): Promise<number[] | undefined>
    getBimDocument(fillPatternIndex: number): Promise<IBimDocument | undefined>
}

export class FillPattern implements IFillPattern {
    index: number
    name?: string
    elementId?: bigint
    uniqueId?: string
    target?: string
    isSolidFill?: boolean
    hostOrientation?: string
    gridCount?: number
    
    bimDocumentIndex?: number
    bimDocument?: IBimDocument
    
    static async createFromTable(table: IFillPatternTable, index: number): Promise<IFillPattern> {
        let result = new FillPattern()
        result.index = index
        
        await Promise.all([
            table.getName(index).then(v => result.name = v),
            table.getElementId(index).then(v => result.elementId = v),
            table.getUniqueId(index).then(v => result.uniqueId = v),
            table.getTarget(index).then(v => result.target = v),
            table.getIsSolidFill(index).then(v => result.isSolidFill = v),
            table.getHostOrientation(index).then(v => result.hostOrientation = v),
            table.getGridCount(index).then(v => result.gridCount = v),
            table.getBimDocumentIndex(index).then(v => result.bimDocumentIndex = v),
        ])
        
        return result
    }
}

export class FillPatternTable implements IFillPatternTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IFillPatternTable | undefined> {
        const entity = await document.entities.getBfast("Vim.FillPattern")
        
        if (!entity) {
            return undefined
        }
        
        let table = new FillPatternTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(fillPatternIndex: number): Promise<IFillPattern> {
        return await FillPattern.createFromTable(this, fillPatternIndex)
    }
    
    async getAll(): Promise<IFillPattern[]> {
        const localTable = await this.entityTable.getLocal()
        
        let name: string[] | undefined
        let elementId: BigInt64Array | undefined
        let uniqueId: string[] | undefined
        let target: string[] | undefined
        let isSolidFill: boolean[] | undefined
        let hostOrientation: string[] | undefined
        let gridCount: number[] | undefined
        let bimDocumentIndex: number[] | undefined
        
        await Promise.all([
            (async () => { name = (await localTable.getStringArray("string:Name")) })(),
            (async () => { elementId = (await localTable.getBigIntArray("long:ElementId")) })(),
            (async () => { uniqueId = (await localTable.getStringArray("string:UniqueId")) })(),
            (async () => { target = (await localTable.getStringArray("string:Target")) })(),
            (async () => { isSolidFill = (await localTable.getBooleanArray("byte:IsSolidFill")) })(),
            (async () => { hostOrientation = (await localTable.getStringArray("string:HostOrientation")) })(),
            (async () => { gridCount = (await localTable.getNumberArray("int:GridCount")) })(),
            (async () => { bimDocumentIndex = (await localTable.getNumberArray("index:Vim.BimDocument:BimDocument")) })(),
        ])
        
        let fillPattern: IFillPattern[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            fillPattern.push({
                index: i,
                name: name ? name[i] : undefined,
                elementId: elementId ? elementId[i] : undefined,
                uniqueId: uniqueId ? uniqueId[i] : undefined,
                target: target ? target[i] : undefined,
                isSolidFill: isSolidFill ? isSolidFill[i] : undefined,
                hostOrientation: hostOrientation ? hostOrientation[i] : undefined,
                gridCount: gridCount ? gridCount[i] : undefined,
                bimDocumentIndex: bimDocumentIndex ? bimDocumentIndex[i] : undefined
            })
        }
        
        return fillPattern
    }
    
    async getName(fillPatternIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(fillPatternIndex, "string:Name"))
    }
    
    async getAllName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Name"))
    }
    
    async getElementId(fillPatternIndex: number): Promise<bigint | undefined> {
        return (await this.entityTable.getBigInt(fillPatternIndex, "long:ElementId"))
    }
    
    async getAllElementId(): Promise<BigInt64Array | undefined> {
        return (await this.entityTable.getBigIntArray("long:ElementId"))
    }
    
    async getUniqueId(fillPatternIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(fillPatternIndex, "string:UniqueId"))
    }
    
    async getAllUniqueId(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:UniqueId"))
    }
    
    async getTarget(fillPatternIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(fillPatternIndex, "string:Target"))
    }
    
    async getAllTarget(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Target"))
    }
    
    async getIsSolidFill(fillPatternIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(fillPatternIndex, "byte:IsSolidFill"))
    }
    
    async getAllIsSolidFill(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsSolidFill"))
    }
    
    async getHostOrientation(fillPatternIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(fillPatternIndex, "string:HostOrientation"))
    }
    
    async getAllHostOrientation(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:HostOrientation"))
    }
    
    async getGridCount(fillPatternIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(fillPatternIndex, "int:GridCount"))
    }
    
    async getAllGridCount(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:GridCount"))
    }
    
    async getBimDocumentIndex(fillPatternIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(fillPatternIndex, "index:Vim.BimDocument:BimDocument")
    }
    
    async getAllBimDocumentIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.BimDocument:BimDocument")
    }
    
    async getBimDocument(fillPatternIndex: number): Promise<IBimDocument | undefined> {
        const index = await this.getBimDocumentIndex(fillPatternIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.bimDocument?.get(index)
    }
    
}

export interface IFillGrid {
    index: number
    orderIndex?: number
    angle?: number
    origin_X?: number
    origin_Y?: number
    offset?: number
    shift?: number
    segments?: string
    
    fillPatternIndex?: number
    fillPattern?: IFillPattern
}

export interface IFillGridTable {
    getCount(): Promise<number>
    get(fillGridIndex: number): Promise<IFillGrid>
    getAll(): Promise<IFillGrid[]>
    
    getOrderIndex(fillGridIndex: number): Promise<number | undefined>
    getAllOrderIndex(): Promise<number[] | undefined>
    getAngle(fillGridIndex: number): Promise<number | undefined>
    getAllAngle(): Promise<number[] | undefined>
    getOrigin_X(fillGridIndex: number): Promise<number | undefined>
    getAllOrigin_X(): Promise<number[] | undefined>
    getOrigin_Y(fillGridIndex: number): Promise<number | undefined>
    getAllOrigin_Y(): Promise<number[] | undefined>
    getOffset(fillGridIndex: number): Promise<number | undefined>
    getAllOffset(): Promise<number[] | undefined>
    getShift(fillGridIndex: number): Promise<number | undefined>
    getAllShift(): Promise<number[] | undefined>
    getSegments(fillGridIndex: number): Promise<string | undefined>
    getAllSegments(): Promise<string[] | undefined>
    
    getFillPatternIndex(fillGridIndex: number): Promise<number | undefined>
    getAllFillPatternIndex(): Promise<number[] | undefined>
    getFillPattern(fillGridIndex: number): Promise<IFillPattern | undefined>
}

export class FillGrid implements IFillGrid {
    index: number
    orderIndex?: number
    angle?: number
    origin_X?: number
    origin_Y?: number
    offset?: number
    shift?: number
    segments?: string
    
    fillPatternIndex?: number
    fillPattern?: IFillPattern
    
    static async createFromTable(table: IFillGridTable, index: number): Promise<IFillGrid> {
        let result = new FillGrid()
        result.index = index
        
        await Promise.all([
            table.getOrderIndex(index).then(v => result.orderIndex = v),
            table.getAngle(index).then(v => result.angle = v),
            table.getOrigin_X(index).then(v => result.origin_X = v),
            table.getOrigin_Y(index).then(v => result.origin_Y = v),
            table.getOffset(index).then(v => result.offset = v),
            table.getShift(index).then(v => result.shift = v),
            table.getSegments(index).then(v => result.segments = v),
            table.getFillPatternIndex(index).then(v => result.fillPatternIndex = v),
        ])
        
        return result
    }
}

export class FillGridTable implements IFillGridTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IFillGridTable | undefined> {
        const entity = await document.entities.getBfast("Vim.FillGrid")
        
        if (!entity) {
            return undefined
        }
        
        let table = new FillGridTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(fillGridIndex: number): Promise<IFillGrid> {
        return await FillGrid.createFromTable(this, fillGridIndex)
    }
    
    async getAll(): Promise<IFillGrid[]> {
        const localTable = await this.entityTable.getLocal()
        
        let orderIndex: number[] | undefined
        let angle: number[] | undefined
        let origin_X: number[] | undefined
        let origin_Y: number[] | undefined
        let offset: number[] | undefined
        let shift: number[] | undefined
        let segments: string[] | undefined
        let fillPatternIndex: number[] | undefined
        
        await Promise.all([
            (async () => { orderIndex = (await localTable.getNumberArray("int:OrderIndex")) })(),
            (async () => { angle = (await localTable.getNumberArray("double:Angle")) })(),
            (async () => { origin_X = (await localTable.getNumberArray("double:Origin.X")) })(),
            (async () => { origin_Y = (await localTable.getNumberArray("double:Origin.Y")) })(),
            (async () => { offset = (await localTable.getNumberArray("double:Offset")) })(),
            (async () => { shift = (await localTable.getNumberArray("double:Shift")) })(),
            (async () => { segments = (await localTable.getStringArray("string:Segments")) })(),
            (async () => { fillPatternIndex = (await localTable.getNumberArray("index:Vim.FillPattern:FillPattern")) })(),
        ])
        
        let fillGrid: IFillGrid[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            fillGrid.push({
                index: i,
                orderIndex: orderIndex ? orderIndex[i] : undefined,
                angle: angle ? angle[i] : undefined,
                origin_X: origin_X ? origin_X[i] : undefined,
                origin_Y: origin_Y ? origin_Y[i] : undefined,
                offset: offset ? offset[i] : undefined,
                shift: shift ? shift[i] : undefined,
                segments: segments ? segments[i] : undefined,
                fillPatternIndex: fillPatternIndex ? fillPatternIndex[i] : undefined
            })
        }
        
        return fillGrid
    }
    
    async getOrderIndex(fillGridIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(fillGridIndex, "int:OrderIndex"))
    }
    
    async getAllOrderIndex(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:OrderIndex"))
    }
    
    async getAngle(fillGridIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(fillGridIndex, "double:Angle"))
    }
    
    async getAllAngle(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Angle"))
    }
    
    async getOrigin_X(fillGridIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(fillGridIndex, "double:Origin.X"))
    }
    
    async getAllOrigin_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Origin.X"))
    }
    
    async getOrigin_Y(fillGridIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(fillGridIndex, "double:Origin.Y"))
    }
    
    async getAllOrigin_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Origin.Y"))
    }
    
    async getOffset(fillGridIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(fillGridIndex, "double:Offset"))
    }
    
    async getAllOffset(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Offset"))
    }
    
    async getShift(fillGridIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(fillGridIndex, "double:Shift"))
    }
    
    async getAllShift(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Shift"))
    }
    
    async getSegments(fillGridIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(fillGridIndex, "string:Segments"))
    }
    
    async getAllSegments(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Segments"))
    }
    
    async getFillPatternIndex(fillGridIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(fillGridIndex, "index:Vim.FillPattern:FillPattern")
    }
    
    async getAllFillPatternIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.FillPattern:FillPattern")
    }
    
    async getFillPattern(fillGridIndex: number): Promise<IFillPattern | undefined> {
        const index = await this.getFillPatternIndex(fillGridIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.fillPattern?.get(index)
    }
    
}

export interface ILineWeight {
    index: number
    table?: string
    scale?: number
    number?: number
    width?: number
    
    bimDocumentIndex?: number
    bimDocument?: IBimDocument
}

export interface ILineWeightTable {
    getCount(): Promise<number>
    get(lineWeightIndex: number): Promise<ILineWeight>
    getAll(): Promise<ILineWeight[]>
    
    getTable(lineWeightIndex: number): Promise<string | undefined>
    getAllTable(): Promise<string[] | undefined>
    getScale(lineWeightIndex: number): Promise<number | undefined>
    getAllScale(): Promise<number[] | undefined>
    getNumber(lineWeightIndex: number): Promise<number | undefined>
    getAllNumber(): Promise<number[] | undefined>
    getWidth(lineWeightIndex: number): Promise<number | undefined>
    getAllWidth(): Promise<number[] | undefined>
    
    getBimDocumentIndex(lineWeightIndex: number): Promise<number | undefined>
    getAllBimDocumentIndex(): Promise<number[] | undefined>
    getBimDocument(lineWeightIndex: number): Promise<IBimDocument | undefined>
}

export class LineWeight implements ILineWeight {
    index: number
    table?: string
    scale?: number
    number?: number
    width?: number
    
    bimDocumentIndex?: number
    bimDocument?: IBimDocument
    
    static async createFromTable(table: ILineWeightTable, index: number): Promise<ILineWeight> {
        let result = new LineWeight()
        result.index = index
        
        await Promise.all([
            table.getTable(index).then(v => result.table = v),
            table.getScale(index).then(v => result.scale = v),
            table.getNumber(index).then(v => result.number = v),
            table.getWidth(index).then(v => result.width = v),
            table.getBimDocumentIndex(index).then(v => result.bimDocumentIndex = v),
        ])
        
        return result
    }
}

export class LineWeightTable implements ILineWeightTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<ILineWeightTable | undefined> {
        const entity = await document.entities.getBfast("Vim.LineWeight")
        
        if (!entity) {
            return undefined
        }
        
        let table = new LineWeightTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(lineWeightIndex: number): Promise<ILineWeight> {
        return await LineWeight.createFromTable(this, lineWeightIndex)
    }
    
    async getAll(): Promise<ILineWeight[]> {
        const localTable = await this.entityTable.getLocal()
        
        let table: string[] | undefined
        let scale: number[] | undefined
        let number: number[] | undefined
        let width: number[] | undefined
        let bimDocumentIndex: number[] | undefined
        
        await Promise.all([
            (async () => { table = (await localTable.getStringArray("string:Table")) })(),
            (async () => { scale = (await localTable.getNumberArray("int:Scale")) })(),
            (async () => { number = (await localTable.getNumberArray("int:Number")) })(),
            (async () => { width = (await localTable.getNumberArray("double:Width")) })(),
            (async () => { bimDocumentIndex = (await localTable.getNumberArray("index:Vim.BimDocument:BimDocument")) })(),
        ])
        
        let lineWeight: ILineWeight[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            lineWeight.push({
                index: i,
                table: table ? table[i] : undefined,
                scale: scale ? scale[i] : undefined,
                number: number ? number[i] : undefined,
                width: width ? width[i] : undefined,
                bimDocumentIndex: bimDocumentIndex ? bimDocumentIndex[i] : undefined
            })
        }
        
        return lineWeight
    }
    
    async getTable(lineWeightIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(lineWeightIndex, "string:Table"))
    }
    
    async getAllTable(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Table"))
    }
    
    async getScale(lineWeightIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lineWeightIndex, "int:Scale"))
    }
    
    async getAllScale(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Scale"))
    }
    
    async getNumber(lineWeightIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lineWeightIndex, "int:Number"))
    }
    
    async getAllNumber(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Number"))
    }
    
    async getWidth(lineWeightIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lineWeightIndex, "double:Width"))
    }
    
    async getAllWidth(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Width"))
    }
    
    async getBimDocumentIndex(lineWeightIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(lineWeightIndex, "index:Vim.BimDocument:BimDocument")
    }
    
    async getAllBimDocumentIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.BimDocument:BimDocument")
    }
    
    async getBimDocument(lineWeightIndex: number): Promise<IBimDocument | undefined> {
        const index = await this.getBimDocumentIndex(lineWeightIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.bimDocument?.get(index)
    }
    
}

export interface IGraphicOverride {
    index: number
    projectionLineWeight?: number
    hasProjectionLineColor?: boolean
    projectionLineColor_X?: number
    projectionLineColor_Y?: number
    projectionLineColor_Z?: number
    cutLineWeight?: number
    hasCutLineColor?: boolean
    cutLineColor_X?: number
    cutLineColor_Y?: number
    cutLineColor_Z?: number
    surfaceForegroundPatternVisible?: boolean
    hasSurfaceForegroundPatternColor?: boolean
    surfaceForegroundPatternColor_X?: number
    surfaceForegroundPatternColor_Y?: number
    surfaceForegroundPatternColor_Z?: number
    surfaceBackgroundPatternVisible?: boolean
    hasSurfaceBackgroundPatternColor?: boolean
    surfaceBackgroundPatternColor_X?: number
    surfaceBackgroundPatternColor_Y?: number
    surfaceBackgroundPatternColor_Z?: number
    cutForegroundPatternVisible?: boolean
    hasCutForegroundPatternColor?: boolean
    cutForegroundPatternColor_X?: number
    cutForegroundPatternColor_Y?: number
    cutForegroundPatternColor_Z?: number
    cutBackgroundPatternVisible?: boolean
    hasCutBackgroundPatternColor?: boolean
    cutBackgroundPatternColor_X?: number
    cutBackgroundPatternColor_Y?: number
    cutBackgroundPatternColor_Z?: number
    transparency?: number
    halftone?: boolean
    detailLevel?: number
    key?: string
    
    projectionLinePatternIndex?: number
    projectionLinePattern?: ILinePattern
    cutLinePatternIndex?: number
    cutLinePattern?: ILinePattern
    surfaceForegroundPatternIndex?: number
    surfaceForegroundPattern?: IFillPattern
    surfaceBackgroundPatternIndex?: number
    surfaceBackgroundPattern?: IFillPattern
    cutForegroundPatternIndex?: number
    cutForegroundPattern?: IFillPattern
    cutBackgroundPatternIndex?: number
    cutBackgroundPattern?: IFillPattern
}

export interface IGraphicOverrideTable {
    getCount(): Promise<number>
    get(graphicOverrideIndex: number): Promise<IGraphicOverride>
    getAll(): Promise<IGraphicOverride[]>
    
    getProjectionLineWeight(graphicOverrideIndex: number): Promise<number | undefined>
    getAllProjectionLineWeight(): Promise<number[] | undefined>
    getHasProjectionLineColor(graphicOverrideIndex: number): Promise<boolean | undefined>
    getAllHasProjectionLineColor(): Promise<boolean[] | undefined>
    getProjectionLineColor_X(graphicOverrideIndex: number): Promise<number | undefined>
    getAllProjectionLineColor_X(): Promise<number[] | undefined>
    getProjectionLineColor_Y(graphicOverrideIndex: number): Promise<number | undefined>
    getAllProjectionLineColor_Y(): Promise<number[] | undefined>
    getProjectionLineColor_Z(graphicOverrideIndex: number): Promise<number | undefined>
    getAllProjectionLineColor_Z(): Promise<number[] | undefined>
    getCutLineWeight(graphicOverrideIndex: number): Promise<number | undefined>
    getAllCutLineWeight(): Promise<number[] | undefined>
    getHasCutLineColor(graphicOverrideIndex: number): Promise<boolean | undefined>
    getAllHasCutLineColor(): Promise<boolean[] | undefined>
    getCutLineColor_X(graphicOverrideIndex: number): Promise<number | undefined>
    getAllCutLineColor_X(): Promise<number[] | undefined>
    getCutLineColor_Y(graphicOverrideIndex: number): Promise<number | undefined>
    getAllCutLineColor_Y(): Promise<number[] | undefined>
    getCutLineColor_Z(graphicOverrideIndex: number): Promise<number | undefined>
    getAllCutLineColor_Z(): Promise<number[] | undefined>
    getSurfaceForegroundPatternVisible(graphicOverrideIndex: number): Promise<boolean | undefined>
    getAllSurfaceForegroundPatternVisible(): Promise<boolean[] | undefined>
    getHasSurfaceForegroundPatternColor(graphicOverrideIndex: number): Promise<boolean | undefined>
    getAllHasSurfaceForegroundPatternColor(): Promise<boolean[] | undefined>
    getSurfaceForegroundPatternColor_X(graphicOverrideIndex: number): Promise<number | undefined>
    getAllSurfaceForegroundPatternColor_X(): Promise<number[] | undefined>
    getSurfaceForegroundPatternColor_Y(graphicOverrideIndex: number): Promise<number | undefined>
    getAllSurfaceForegroundPatternColor_Y(): Promise<number[] | undefined>
    getSurfaceForegroundPatternColor_Z(graphicOverrideIndex: number): Promise<number | undefined>
    getAllSurfaceForegroundPatternColor_Z(): Promise<number[] | undefined>
    getSurfaceBackgroundPatternVisible(graphicOverrideIndex: number): Promise<boolean | undefined>
    getAllSurfaceBackgroundPatternVisible(): Promise<boolean[] | undefined>
    getHasSurfaceBackgroundPatternColor(graphicOverrideIndex: number): Promise<boolean | undefined>
    getAllHasSurfaceBackgroundPatternColor(): Promise<boolean[] | undefined>
    getSurfaceBackgroundPatternColor_X(graphicOverrideIndex: number): Promise<number | undefined>
    getAllSurfaceBackgroundPatternColor_X(): Promise<number[] | undefined>
    getSurfaceBackgroundPatternColor_Y(graphicOverrideIndex: number): Promise<number | undefined>
    getAllSurfaceBackgroundPatternColor_Y(): Promise<number[] | undefined>
    getSurfaceBackgroundPatternColor_Z(graphicOverrideIndex: number): Promise<number | undefined>
    getAllSurfaceBackgroundPatternColor_Z(): Promise<number[] | undefined>
    getCutForegroundPatternVisible(graphicOverrideIndex: number): Promise<boolean | undefined>
    getAllCutForegroundPatternVisible(): Promise<boolean[] | undefined>
    getHasCutForegroundPatternColor(graphicOverrideIndex: number): Promise<boolean | undefined>
    getAllHasCutForegroundPatternColor(): Promise<boolean[] | undefined>
    getCutForegroundPatternColor_X(graphicOverrideIndex: number): Promise<number | undefined>
    getAllCutForegroundPatternColor_X(): Promise<number[] | undefined>
    getCutForegroundPatternColor_Y(graphicOverrideIndex: number): Promise<number | undefined>
    getAllCutForegroundPatternColor_Y(): Promise<number[] | undefined>
    getCutForegroundPatternColor_Z(graphicOverrideIndex: number): Promise<number | undefined>
    getAllCutForegroundPatternColor_Z(): Promise<number[] | undefined>
    getCutBackgroundPatternVisible(graphicOverrideIndex: number): Promise<boolean | undefined>
    getAllCutBackgroundPatternVisible(): Promise<boolean[] | undefined>
    getHasCutBackgroundPatternColor(graphicOverrideIndex: number): Promise<boolean | undefined>
    getAllHasCutBackgroundPatternColor(): Promise<boolean[] | undefined>
    getCutBackgroundPatternColor_X(graphicOverrideIndex: number): Promise<number | undefined>
    getAllCutBackgroundPatternColor_X(): Promise<number[] | undefined>
    getCutBackgroundPatternColor_Y(graphicOverrideIndex: number): Promise<number | undefined>
    getAllCutBackgroundPatternColor_Y(): Promise<number[] | undefined>
    getCutBackgroundPatternColor_Z(graphicOverrideIndex: number): Promise<number | undefined>
    getAllCutBackgroundPatternColor_Z(): Promise<number[] | undefined>
    getTransparency(graphicOverrideIndex: number): Promise<number | undefined>
    getAllTransparency(): Promise<number[] | undefined>
    getHalftone(graphicOverrideIndex: number): Promise<boolean | undefined>
    getAllHalftone(): Promise<boolean[] | undefined>
    getDetailLevel(graphicOverrideIndex: number): Promise<number | undefined>
    getAllDetailLevel(): Promise<number[] | undefined>
    getKey(graphicOverrideIndex: number): Promise<string | undefined>
    getAllKey(): Promise<string[] | undefined>
    
    getProjectionLinePatternIndex(graphicOverrideIndex: number): Promise<number | undefined>
    getAllProjectionLinePatternIndex(): Promise<number[] | undefined>
    getProjectionLinePattern(graphicOverrideIndex: number): Promise<ILinePattern | undefined>
    getCutLinePatternIndex(graphicOverrideIndex: number): Promise<number | undefined>
    getAllCutLinePatternIndex(): Promise<number[] | undefined>
    getCutLinePattern(graphicOverrideIndex: number): Promise<ILinePattern | undefined>
    getSurfaceForegroundPatternIndex(graphicOverrideIndex: number): Promise<number | undefined>
    getAllSurfaceForegroundPatternIndex(): Promise<number[] | undefined>
    getSurfaceForegroundPattern(graphicOverrideIndex: number): Promise<IFillPattern | undefined>
    getSurfaceBackgroundPatternIndex(graphicOverrideIndex: number): Promise<number | undefined>
    getAllSurfaceBackgroundPatternIndex(): Promise<number[] | undefined>
    getSurfaceBackgroundPattern(graphicOverrideIndex: number): Promise<IFillPattern | undefined>
    getCutForegroundPatternIndex(graphicOverrideIndex: number): Promise<number | undefined>
    getAllCutForegroundPatternIndex(): Promise<number[] | undefined>
    getCutForegroundPattern(graphicOverrideIndex: number): Promise<IFillPattern | undefined>
    getCutBackgroundPatternIndex(graphicOverrideIndex: number): Promise<number | undefined>
    getAllCutBackgroundPatternIndex(): Promise<number[] | undefined>
    getCutBackgroundPattern(graphicOverrideIndex: number): Promise<IFillPattern | undefined>
}

export class GraphicOverride implements IGraphicOverride {
    index: number
    projectionLineWeight?: number
    hasProjectionLineColor?: boolean
    projectionLineColor_X?: number
    projectionLineColor_Y?: number
    projectionLineColor_Z?: number
    cutLineWeight?: number
    hasCutLineColor?: boolean
    cutLineColor_X?: number
    cutLineColor_Y?: number
    cutLineColor_Z?: number
    surfaceForegroundPatternVisible?: boolean
    hasSurfaceForegroundPatternColor?: boolean
    surfaceForegroundPatternColor_X?: number
    surfaceForegroundPatternColor_Y?: number
    surfaceForegroundPatternColor_Z?: number
    surfaceBackgroundPatternVisible?: boolean
    hasSurfaceBackgroundPatternColor?: boolean
    surfaceBackgroundPatternColor_X?: number
    surfaceBackgroundPatternColor_Y?: number
    surfaceBackgroundPatternColor_Z?: number
    cutForegroundPatternVisible?: boolean
    hasCutForegroundPatternColor?: boolean
    cutForegroundPatternColor_X?: number
    cutForegroundPatternColor_Y?: number
    cutForegroundPatternColor_Z?: number
    cutBackgroundPatternVisible?: boolean
    hasCutBackgroundPatternColor?: boolean
    cutBackgroundPatternColor_X?: number
    cutBackgroundPatternColor_Y?: number
    cutBackgroundPatternColor_Z?: number
    transparency?: number
    halftone?: boolean
    detailLevel?: number
    key?: string
    
    projectionLinePatternIndex?: number
    projectionLinePattern?: ILinePattern
    cutLinePatternIndex?: number
    cutLinePattern?: ILinePattern
    surfaceForegroundPatternIndex?: number
    surfaceForegroundPattern?: IFillPattern
    surfaceBackgroundPatternIndex?: number
    surfaceBackgroundPattern?: IFillPattern
    cutForegroundPatternIndex?: number
    cutForegroundPattern?: IFillPattern
    cutBackgroundPatternIndex?: number
    cutBackgroundPattern?: IFillPattern
    
    static async createFromTable(table: IGraphicOverrideTable, index: number): Promise<IGraphicOverride> {
        let result = new GraphicOverride()
        result.index = index
        
        await Promise.all([
            table.getProjectionLineWeight(index).then(v => result.projectionLineWeight = v),
            table.getHasProjectionLineColor(index).then(v => result.hasProjectionLineColor = v),
            table.getProjectionLineColor_X(index).then(v => result.projectionLineColor_X = v),
            table.getProjectionLineColor_Y(index).then(v => result.projectionLineColor_Y = v),
            table.getProjectionLineColor_Z(index).then(v => result.projectionLineColor_Z = v),
            table.getCutLineWeight(index).then(v => result.cutLineWeight = v),
            table.getHasCutLineColor(index).then(v => result.hasCutLineColor = v),
            table.getCutLineColor_X(index).then(v => result.cutLineColor_X = v),
            table.getCutLineColor_Y(index).then(v => result.cutLineColor_Y = v),
            table.getCutLineColor_Z(index).then(v => result.cutLineColor_Z = v),
            table.getSurfaceForegroundPatternVisible(index).then(v => result.surfaceForegroundPatternVisible = v),
            table.getHasSurfaceForegroundPatternColor(index).then(v => result.hasSurfaceForegroundPatternColor = v),
            table.getSurfaceForegroundPatternColor_X(index).then(v => result.surfaceForegroundPatternColor_X = v),
            table.getSurfaceForegroundPatternColor_Y(index).then(v => result.surfaceForegroundPatternColor_Y = v),
            table.getSurfaceForegroundPatternColor_Z(index).then(v => result.surfaceForegroundPatternColor_Z = v),
            table.getSurfaceBackgroundPatternVisible(index).then(v => result.surfaceBackgroundPatternVisible = v),
            table.getHasSurfaceBackgroundPatternColor(index).then(v => result.hasSurfaceBackgroundPatternColor = v),
            table.getSurfaceBackgroundPatternColor_X(index).then(v => result.surfaceBackgroundPatternColor_X = v),
            table.getSurfaceBackgroundPatternColor_Y(index).then(v => result.surfaceBackgroundPatternColor_Y = v),
            table.getSurfaceBackgroundPatternColor_Z(index).then(v => result.surfaceBackgroundPatternColor_Z = v),
            table.getCutForegroundPatternVisible(index).then(v => result.cutForegroundPatternVisible = v),
            table.getHasCutForegroundPatternColor(index).then(v => result.hasCutForegroundPatternColor = v),
            table.getCutForegroundPatternColor_X(index).then(v => result.cutForegroundPatternColor_X = v),
            table.getCutForegroundPatternColor_Y(index).then(v => result.cutForegroundPatternColor_Y = v),
            table.getCutForegroundPatternColor_Z(index).then(v => result.cutForegroundPatternColor_Z = v),
            table.getCutBackgroundPatternVisible(index).then(v => result.cutBackgroundPatternVisible = v),
            table.getHasCutBackgroundPatternColor(index).then(v => result.hasCutBackgroundPatternColor = v),
            table.getCutBackgroundPatternColor_X(index).then(v => result.cutBackgroundPatternColor_X = v),
            table.getCutBackgroundPatternColor_Y(index).then(v => result.cutBackgroundPatternColor_Y = v),
            table.getCutBackgroundPatternColor_Z(index).then(v => result.cutBackgroundPatternColor_Z = v),
            table.getTransparency(index).then(v => result.transparency = v),
            table.getHalftone(index).then(v => result.halftone = v),
            table.getDetailLevel(index).then(v => result.detailLevel = v),
            table.getKey(index).then(v => result.key = v),
            table.getProjectionLinePatternIndex(index).then(v => result.projectionLinePatternIndex = v),
            table.getCutLinePatternIndex(index).then(v => result.cutLinePatternIndex = v),
            table.getSurfaceForegroundPatternIndex(index).then(v => result.surfaceForegroundPatternIndex = v),
            table.getSurfaceBackgroundPatternIndex(index).then(v => result.surfaceBackgroundPatternIndex = v),
            table.getCutForegroundPatternIndex(index).then(v => result.cutForegroundPatternIndex = v),
            table.getCutBackgroundPatternIndex(index).then(v => result.cutBackgroundPatternIndex = v),
        ])
        
        return result
    }
}

export class GraphicOverrideTable implements IGraphicOverrideTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IGraphicOverrideTable | undefined> {
        const entity = await document.entities.getBfast("Vim.GraphicOverride")
        
        if (!entity) {
            return undefined
        }
        
        let table = new GraphicOverrideTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(graphicOverrideIndex: number): Promise<IGraphicOverride> {
        return await GraphicOverride.createFromTable(this, graphicOverrideIndex)
    }
    
    async getAll(): Promise<IGraphicOverride[]> {
        const localTable = await this.entityTable.getLocal()
        
        let projectionLineWeight: number[] | undefined
        let hasProjectionLineColor: boolean[] | undefined
        let projectionLineColor_X: number[] | undefined
        let projectionLineColor_Y: number[] | undefined
        let projectionLineColor_Z: number[] | undefined
        let cutLineWeight: number[] | undefined
        let hasCutLineColor: boolean[] | undefined
        let cutLineColor_X: number[] | undefined
        let cutLineColor_Y: number[] | undefined
        let cutLineColor_Z: number[] | undefined
        let surfaceForegroundPatternVisible: boolean[] | undefined
        let hasSurfaceForegroundPatternColor: boolean[] | undefined
        let surfaceForegroundPatternColor_X: number[] | undefined
        let surfaceForegroundPatternColor_Y: number[] | undefined
        let surfaceForegroundPatternColor_Z: number[] | undefined
        let surfaceBackgroundPatternVisible: boolean[] | undefined
        let hasSurfaceBackgroundPatternColor: boolean[] | undefined
        let surfaceBackgroundPatternColor_X: number[] | undefined
        let surfaceBackgroundPatternColor_Y: number[] | undefined
        let surfaceBackgroundPatternColor_Z: number[] | undefined
        let cutForegroundPatternVisible: boolean[] | undefined
        let hasCutForegroundPatternColor: boolean[] | undefined
        let cutForegroundPatternColor_X: number[] | undefined
        let cutForegroundPatternColor_Y: number[] | undefined
        let cutForegroundPatternColor_Z: number[] | undefined
        let cutBackgroundPatternVisible: boolean[] | undefined
        let hasCutBackgroundPatternColor: boolean[] | undefined
        let cutBackgroundPatternColor_X: number[] | undefined
        let cutBackgroundPatternColor_Y: number[] | undefined
        let cutBackgroundPatternColor_Z: number[] | undefined
        let transparency: number[] | undefined
        let halftone: boolean[] | undefined
        let detailLevel: number[] | undefined
        let key: string[] | undefined
        let projectionLinePatternIndex: number[] | undefined
        let cutLinePatternIndex: number[] | undefined
        let surfaceForegroundPatternIndex: number[] | undefined
        let surfaceBackgroundPatternIndex: number[] | undefined
        let cutForegroundPatternIndex: number[] | undefined
        let cutBackgroundPatternIndex: number[] | undefined
        
        await Promise.all([
            (async () => { projectionLineWeight = (await localTable.getNumberArray("int:ProjectionLineWeight")) })(),
            (async () => { hasProjectionLineColor = (await localTable.getBooleanArray("byte:HasProjectionLineColor")) })(),
            (async () => { projectionLineColor_X = (await localTable.getNumberArray("double:ProjectionLineColor.X")) })(),
            (async () => { projectionLineColor_Y = (await localTable.getNumberArray("double:ProjectionLineColor.Y")) })(),
            (async () => { projectionLineColor_Z = (await localTable.getNumberArray("double:ProjectionLineColor.Z")) })(),
            (async () => { cutLineWeight = (await localTable.getNumberArray("int:CutLineWeight")) })(),
            (async () => { hasCutLineColor = (await localTable.getBooleanArray("byte:HasCutLineColor")) })(),
            (async () => { cutLineColor_X = (await localTable.getNumberArray("double:CutLineColor.X")) })(),
            (async () => { cutLineColor_Y = (await localTable.getNumberArray("double:CutLineColor.Y")) })(),
            (async () => { cutLineColor_Z = (await localTable.getNumberArray("double:CutLineColor.Z")) })(),
            (async () => { surfaceForegroundPatternVisible = (await localTable.getBooleanArray("byte:SurfaceForegroundPatternVisible")) })(),
            (async () => { hasSurfaceForegroundPatternColor = (await localTable.getBooleanArray("byte:HasSurfaceForegroundPatternColor")) })(),
            (async () => { surfaceForegroundPatternColor_X = (await localTable.getNumberArray("double:SurfaceForegroundPatternColor.X")) })(),
            (async () => { surfaceForegroundPatternColor_Y = (await localTable.getNumberArray("double:SurfaceForegroundPatternColor.Y")) })(),
            (async () => { surfaceForegroundPatternColor_Z = (await localTable.getNumberArray("double:SurfaceForegroundPatternColor.Z")) })(),
            (async () => { surfaceBackgroundPatternVisible = (await localTable.getBooleanArray("byte:SurfaceBackgroundPatternVisible")) })(),
            (async () => { hasSurfaceBackgroundPatternColor = (await localTable.getBooleanArray("byte:HasSurfaceBackgroundPatternColor")) })(),
            (async () => { surfaceBackgroundPatternColor_X = (await localTable.getNumberArray("double:SurfaceBackgroundPatternColor.X")) })(),
            (async () => { surfaceBackgroundPatternColor_Y = (await localTable.getNumberArray("double:SurfaceBackgroundPatternColor.Y")) })(),
            (async () => { surfaceBackgroundPatternColor_Z = (await localTable.getNumberArray("double:SurfaceBackgroundPatternColor.Z")) })(),
            (async () => { cutForegroundPatternVisible = (await localTable.getBooleanArray("byte:CutForegroundPatternVisible")) })(),
            (async () => { hasCutForegroundPatternColor = (await localTable.getBooleanArray("byte:HasCutForegroundPatternColor")) })(),
            (async () => { cutForegroundPatternColor_X = (await localTable.getNumberArray("double:CutForegroundPatternColor.X")) })(),
            (async () => { cutForegroundPatternColor_Y = (await localTable.getNumberArray("double:CutForegroundPatternColor.Y")) })(),
            (async () => { cutForegroundPatternColor_Z = (await localTable.getNumberArray("double:CutForegroundPatternColor.Z")) })(),
            (async () => { cutBackgroundPatternVisible = (await localTable.getBooleanArray("byte:CutBackgroundPatternVisible")) })(),
            (async () => { hasCutBackgroundPatternColor = (await localTable.getBooleanArray("byte:HasCutBackgroundPatternColor")) })(),
            (async () => { cutBackgroundPatternColor_X = (await localTable.getNumberArray("double:CutBackgroundPatternColor.X")) })(),
            (async () => { cutBackgroundPatternColor_Y = (await localTable.getNumberArray("double:CutBackgroundPatternColor.Y")) })(),
            (async () => { cutBackgroundPatternColor_Z = (await localTable.getNumberArray("double:CutBackgroundPatternColor.Z")) })(),
            (async () => { transparency = (await localTable.getNumberArray("int:Transparency")) })(),
            (async () => { halftone = (await localTable.getBooleanArray("byte:Halftone")) })(),
            (async () => { detailLevel = (await localTable.getNumberArray("int:DetailLevel")) })(),
            (async () => { key = (await localTable.getStringArray("string:Key")) })(),
            (async () => { projectionLinePatternIndex = (await localTable.getNumberArray("index:Vim.LinePattern:ProjectionLinePattern")) })(),
            (async () => { cutLinePatternIndex = (await localTable.getNumberArray("index:Vim.LinePattern:CutLinePattern")) })(),
            (async () => { surfaceForegroundPatternIndex = (await localTable.getNumberArray("index:Vim.FillPattern:SurfaceForegroundPattern")) })(),
            (async () => { surfaceBackgroundPatternIndex = (await localTable.getNumberArray("index:Vim.FillPattern:SurfaceBackgroundPattern")) })(),
            (async () => { cutForegroundPatternIndex = (await localTable.getNumberArray("index:Vim.FillPattern:CutForegroundPattern")) })(),
            (async () => { cutBackgroundPatternIndex = (await localTable.getNumberArray("index:Vim.FillPattern:CutBackgroundPattern")) })(),
        ])
        
        let graphicOverride: IGraphicOverride[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            graphicOverride.push({
                index: i,
                projectionLineWeight: projectionLineWeight ? projectionLineWeight[i] : undefined,
                hasProjectionLineColor: hasProjectionLineColor ? hasProjectionLineColor[i] : undefined,
                projectionLineColor_X: projectionLineColor_X ? projectionLineColor_X[i] : undefined,
                projectionLineColor_Y: projectionLineColor_Y ? projectionLineColor_Y[i] : undefined,
                projectionLineColor_Z: projectionLineColor_Z ? projectionLineColor_Z[i] : undefined,
                cutLineWeight: cutLineWeight ? cutLineWeight[i] : undefined,
                hasCutLineColor: hasCutLineColor ? hasCutLineColor[i] : undefined,
                cutLineColor_X: cutLineColor_X ? cutLineColor_X[i] : undefined,
                cutLineColor_Y: cutLineColor_Y ? cutLineColor_Y[i] : undefined,
                cutLineColor_Z: cutLineColor_Z ? cutLineColor_Z[i] : undefined,
                surfaceForegroundPatternVisible: surfaceForegroundPatternVisible ? surfaceForegroundPatternVisible[i] : undefined,
                hasSurfaceForegroundPatternColor: hasSurfaceForegroundPatternColor ? hasSurfaceForegroundPatternColor[i] : undefined,
                surfaceForegroundPatternColor_X: surfaceForegroundPatternColor_X ? surfaceForegroundPatternColor_X[i] : undefined,
                surfaceForegroundPatternColor_Y: surfaceForegroundPatternColor_Y ? surfaceForegroundPatternColor_Y[i] : undefined,
                surfaceForegroundPatternColor_Z: surfaceForegroundPatternColor_Z ? surfaceForegroundPatternColor_Z[i] : undefined,
                surfaceBackgroundPatternVisible: surfaceBackgroundPatternVisible ? surfaceBackgroundPatternVisible[i] : undefined,
                hasSurfaceBackgroundPatternColor: hasSurfaceBackgroundPatternColor ? hasSurfaceBackgroundPatternColor[i] : undefined,
                surfaceBackgroundPatternColor_X: surfaceBackgroundPatternColor_X ? surfaceBackgroundPatternColor_X[i] : undefined,
                surfaceBackgroundPatternColor_Y: surfaceBackgroundPatternColor_Y ? surfaceBackgroundPatternColor_Y[i] : undefined,
                surfaceBackgroundPatternColor_Z: surfaceBackgroundPatternColor_Z ? surfaceBackgroundPatternColor_Z[i] : undefined,
                cutForegroundPatternVisible: cutForegroundPatternVisible ? cutForegroundPatternVisible[i] : undefined,
                hasCutForegroundPatternColor: hasCutForegroundPatternColor ? hasCutForegroundPatternColor[i] : undefined,
                cutForegroundPatternColor_X: cutForegroundPatternColor_X ? cutForegroundPatternColor_X[i] : undefined,
                cutForegroundPatternColor_Y: cutForegroundPatternColor_Y ? cutForegroundPatternColor_Y[i] : undefined,
                cutForegroundPatternColor_Z: cutForegroundPatternColor_Z ? cutForegroundPatternColor_Z[i] : undefined,
                cutBackgroundPatternVisible: cutBackgroundPatternVisible ? cutBackgroundPatternVisible[i] : undefined,
                hasCutBackgroundPatternColor: hasCutBackgroundPatternColor ? hasCutBackgroundPatternColor[i] : undefined,
                cutBackgroundPatternColor_X: cutBackgroundPatternColor_X ? cutBackgroundPatternColor_X[i] : undefined,
                cutBackgroundPatternColor_Y: cutBackgroundPatternColor_Y ? cutBackgroundPatternColor_Y[i] : undefined,
                cutBackgroundPatternColor_Z: cutBackgroundPatternColor_Z ? cutBackgroundPatternColor_Z[i] : undefined,
                transparency: transparency ? transparency[i] : undefined,
                halftone: halftone ? halftone[i] : undefined,
                detailLevel: detailLevel ? detailLevel[i] : undefined,
                key: key ? key[i] : undefined,
                projectionLinePatternIndex: projectionLinePatternIndex ? projectionLinePatternIndex[i] : undefined,
                cutLinePatternIndex: cutLinePatternIndex ? cutLinePatternIndex[i] : undefined,
                surfaceForegroundPatternIndex: surfaceForegroundPatternIndex ? surfaceForegroundPatternIndex[i] : undefined,
                surfaceBackgroundPatternIndex: surfaceBackgroundPatternIndex ? surfaceBackgroundPatternIndex[i] : undefined,
                cutForegroundPatternIndex: cutForegroundPatternIndex ? cutForegroundPatternIndex[i] : undefined,
                cutBackgroundPatternIndex: cutBackgroundPatternIndex ? cutBackgroundPatternIndex[i] : undefined
            })
        }
        
        return graphicOverride
    }
    
    async getProjectionLineWeight(graphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(graphicOverrideIndex, "int:ProjectionLineWeight"))
    }
    
    async getAllProjectionLineWeight(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:ProjectionLineWeight"))
    }
    
    async getHasProjectionLineColor(graphicOverrideIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(graphicOverrideIndex, "byte:HasProjectionLineColor"))
    }
    
    async getAllHasProjectionLineColor(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:HasProjectionLineColor"))
    }
    
    async getProjectionLineColor_X(graphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(graphicOverrideIndex, "double:ProjectionLineColor.X"))
    }
    
    async getAllProjectionLineColor_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:ProjectionLineColor.X"))
    }
    
    async getProjectionLineColor_Y(graphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(graphicOverrideIndex, "double:ProjectionLineColor.Y"))
    }
    
    async getAllProjectionLineColor_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:ProjectionLineColor.Y"))
    }
    
    async getProjectionLineColor_Z(graphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(graphicOverrideIndex, "double:ProjectionLineColor.Z"))
    }
    
    async getAllProjectionLineColor_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:ProjectionLineColor.Z"))
    }
    
    async getCutLineWeight(graphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(graphicOverrideIndex, "int:CutLineWeight"))
    }
    
    async getAllCutLineWeight(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:CutLineWeight"))
    }
    
    async getHasCutLineColor(graphicOverrideIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(graphicOverrideIndex, "byte:HasCutLineColor"))
    }
    
    async getAllHasCutLineColor(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:HasCutLineColor"))
    }
    
    async getCutLineColor_X(graphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(graphicOverrideIndex, "double:CutLineColor.X"))
    }
    
    async getAllCutLineColor_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:CutLineColor.X"))
    }
    
    async getCutLineColor_Y(graphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(graphicOverrideIndex, "double:CutLineColor.Y"))
    }
    
    async getAllCutLineColor_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:CutLineColor.Y"))
    }
    
    async getCutLineColor_Z(graphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(graphicOverrideIndex, "double:CutLineColor.Z"))
    }
    
    async getAllCutLineColor_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:CutLineColor.Z"))
    }
    
    async getSurfaceForegroundPatternVisible(graphicOverrideIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(graphicOverrideIndex, "byte:SurfaceForegroundPatternVisible"))
    }
    
    async getAllSurfaceForegroundPatternVisible(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:SurfaceForegroundPatternVisible"))
    }
    
    async getHasSurfaceForegroundPatternColor(graphicOverrideIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(graphicOverrideIndex, "byte:HasSurfaceForegroundPatternColor"))
    }
    
    async getAllHasSurfaceForegroundPatternColor(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:HasSurfaceForegroundPatternColor"))
    }
    
    async getSurfaceForegroundPatternColor_X(graphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(graphicOverrideIndex, "double:SurfaceForegroundPatternColor.X"))
    }
    
    async getAllSurfaceForegroundPatternColor_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:SurfaceForegroundPatternColor.X"))
    }
    
    async getSurfaceForegroundPatternColor_Y(graphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(graphicOverrideIndex, "double:SurfaceForegroundPatternColor.Y"))
    }
    
    async getAllSurfaceForegroundPatternColor_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:SurfaceForegroundPatternColor.Y"))
    }
    
    async getSurfaceForegroundPatternColor_Z(graphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(graphicOverrideIndex, "double:SurfaceForegroundPatternColor.Z"))
    }
    
    async getAllSurfaceForegroundPatternColor_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:SurfaceForegroundPatternColor.Z"))
    }
    
    async getSurfaceBackgroundPatternVisible(graphicOverrideIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(graphicOverrideIndex, "byte:SurfaceBackgroundPatternVisible"))
    }
    
    async getAllSurfaceBackgroundPatternVisible(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:SurfaceBackgroundPatternVisible"))
    }
    
    async getHasSurfaceBackgroundPatternColor(graphicOverrideIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(graphicOverrideIndex, "byte:HasSurfaceBackgroundPatternColor"))
    }
    
    async getAllHasSurfaceBackgroundPatternColor(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:HasSurfaceBackgroundPatternColor"))
    }
    
    async getSurfaceBackgroundPatternColor_X(graphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(graphicOverrideIndex, "double:SurfaceBackgroundPatternColor.X"))
    }
    
    async getAllSurfaceBackgroundPatternColor_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:SurfaceBackgroundPatternColor.X"))
    }
    
    async getSurfaceBackgroundPatternColor_Y(graphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(graphicOverrideIndex, "double:SurfaceBackgroundPatternColor.Y"))
    }
    
    async getAllSurfaceBackgroundPatternColor_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:SurfaceBackgroundPatternColor.Y"))
    }
    
    async getSurfaceBackgroundPatternColor_Z(graphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(graphicOverrideIndex, "double:SurfaceBackgroundPatternColor.Z"))
    }
    
    async getAllSurfaceBackgroundPatternColor_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:SurfaceBackgroundPatternColor.Z"))
    }
    
    async getCutForegroundPatternVisible(graphicOverrideIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(graphicOverrideIndex, "byte:CutForegroundPatternVisible"))
    }
    
    async getAllCutForegroundPatternVisible(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:CutForegroundPatternVisible"))
    }
    
    async getHasCutForegroundPatternColor(graphicOverrideIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(graphicOverrideIndex, "byte:HasCutForegroundPatternColor"))
    }
    
    async getAllHasCutForegroundPatternColor(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:HasCutForegroundPatternColor"))
    }
    
    async getCutForegroundPatternColor_X(graphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(graphicOverrideIndex, "double:CutForegroundPatternColor.X"))
    }
    
    async getAllCutForegroundPatternColor_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:CutForegroundPatternColor.X"))
    }
    
    async getCutForegroundPatternColor_Y(graphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(graphicOverrideIndex, "double:CutForegroundPatternColor.Y"))
    }
    
    async getAllCutForegroundPatternColor_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:CutForegroundPatternColor.Y"))
    }
    
    async getCutForegroundPatternColor_Z(graphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(graphicOverrideIndex, "double:CutForegroundPatternColor.Z"))
    }
    
    async getAllCutForegroundPatternColor_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:CutForegroundPatternColor.Z"))
    }
    
    async getCutBackgroundPatternVisible(graphicOverrideIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(graphicOverrideIndex, "byte:CutBackgroundPatternVisible"))
    }
    
    async getAllCutBackgroundPatternVisible(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:CutBackgroundPatternVisible"))
    }
    
    async getHasCutBackgroundPatternColor(graphicOverrideIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(graphicOverrideIndex, "byte:HasCutBackgroundPatternColor"))
    }
    
    async getAllHasCutBackgroundPatternColor(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:HasCutBackgroundPatternColor"))
    }
    
    async getCutBackgroundPatternColor_X(graphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(graphicOverrideIndex, "double:CutBackgroundPatternColor.X"))
    }
    
    async getAllCutBackgroundPatternColor_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:CutBackgroundPatternColor.X"))
    }
    
    async getCutBackgroundPatternColor_Y(graphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(graphicOverrideIndex, "double:CutBackgroundPatternColor.Y"))
    }
    
    async getAllCutBackgroundPatternColor_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:CutBackgroundPatternColor.Y"))
    }
    
    async getCutBackgroundPatternColor_Z(graphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(graphicOverrideIndex, "double:CutBackgroundPatternColor.Z"))
    }
    
    async getAllCutBackgroundPatternColor_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:CutBackgroundPatternColor.Z"))
    }
    
    async getTransparency(graphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(graphicOverrideIndex, "int:Transparency"))
    }
    
    async getAllTransparency(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Transparency"))
    }
    
    async getHalftone(graphicOverrideIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(graphicOverrideIndex, "byte:Halftone"))
    }
    
    async getAllHalftone(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:Halftone"))
    }
    
    async getDetailLevel(graphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(graphicOverrideIndex, "int:DetailLevel"))
    }
    
    async getAllDetailLevel(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:DetailLevel"))
    }
    
    async getKey(graphicOverrideIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(graphicOverrideIndex, "string:Key"))
    }
    
    async getAllKey(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Key"))
    }
    
    async getProjectionLinePatternIndex(graphicOverrideIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(graphicOverrideIndex, "index:Vim.LinePattern:ProjectionLinePattern")
    }
    
    async getAllProjectionLinePatternIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.LinePattern:ProjectionLinePattern")
    }
    
    async getProjectionLinePattern(graphicOverrideIndex: number): Promise<ILinePattern | undefined> {
        const index = await this.getProjectionLinePatternIndex(graphicOverrideIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.linePattern?.get(index)
    }
    
    async getCutLinePatternIndex(graphicOverrideIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(graphicOverrideIndex, "index:Vim.LinePattern:CutLinePattern")
    }
    
    async getAllCutLinePatternIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.LinePattern:CutLinePattern")
    }
    
    async getCutLinePattern(graphicOverrideIndex: number): Promise<ILinePattern | undefined> {
        const index = await this.getCutLinePatternIndex(graphicOverrideIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.linePattern?.get(index)
    }
    
    async getSurfaceForegroundPatternIndex(graphicOverrideIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(graphicOverrideIndex, "index:Vim.FillPattern:SurfaceForegroundPattern")
    }
    
    async getAllSurfaceForegroundPatternIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.FillPattern:SurfaceForegroundPattern")
    }
    
    async getSurfaceForegroundPattern(graphicOverrideIndex: number): Promise<IFillPattern | undefined> {
        const index = await this.getSurfaceForegroundPatternIndex(graphicOverrideIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.fillPattern?.get(index)
    }
    
    async getSurfaceBackgroundPatternIndex(graphicOverrideIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(graphicOverrideIndex, "index:Vim.FillPattern:SurfaceBackgroundPattern")
    }
    
    async getAllSurfaceBackgroundPatternIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.FillPattern:SurfaceBackgroundPattern")
    }
    
    async getSurfaceBackgroundPattern(graphicOverrideIndex: number): Promise<IFillPattern | undefined> {
        const index = await this.getSurfaceBackgroundPatternIndex(graphicOverrideIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.fillPattern?.get(index)
    }
    
    async getCutForegroundPatternIndex(graphicOverrideIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(graphicOverrideIndex, "index:Vim.FillPattern:CutForegroundPattern")
    }
    
    async getAllCutForegroundPatternIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.FillPattern:CutForegroundPattern")
    }
    
    async getCutForegroundPattern(graphicOverrideIndex: number): Promise<IFillPattern | undefined> {
        const index = await this.getCutForegroundPatternIndex(graphicOverrideIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.fillPattern?.get(index)
    }
    
    async getCutBackgroundPatternIndex(graphicOverrideIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(graphicOverrideIndex, "index:Vim.FillPattern:CutBackgroundPattern")
    }
    
    async getAllCutBackgroundPatternIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.FillPattern:CutBackgroundPattern")
    }
    
    async getCutBackgroundPattern(graphicOverrideIndex: number): Promise<IFillPattern | undefined> {
        const index = await this.getCutBackgroundPatternIndex(graphicOverrideIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.fillPattern?.get(index)
    }
    
}

export interface ICategoryInView {
    index: number
    isHidden?: boolean
    
    viewIndex?: number
    view?: IView
    categoryIndex?: number
    category?: ICategory
    graphicOverrideIndex?: number
    graphicOverride?: IGraphicOverride
}

export interface ICategoryInViewTable {
    getCount(): Promise<number>
    get(categoryInViewIndex: number): Promise<ICategoryInView>
    getAll(): Promise<ICategoryInView[]>
    
    getIsHidden(categoryInViewIndex: number): Promise<boolean | undefined>
    getAllIsHidden(): Promise<boolean[] | undefined>
    
    getViewIndex(categoryInViewIndex: number): Promise<number | undefined>
    getAllViewIndex(): Promise<number[] | undefined>
    getView(categoryInViewIndex: number): Promise<IView | undefined>
    getCategoryIndex(categoryInViewIndex: number): Promise<number | undefined>
    getAllCategoryIndex(): Promise<number[] | undefined>
    getCategory(categoryInViewIndex: number): Promise<ICategory | undefined>
    getGraphicOverrideIndex(categoryInViewIndex: number): Promise<number | undefined>
    getAllGraphicOverrideIndex(): Promise<number[] | undefined>
    getGraphicOverride(categoryInViewIndex: number): Promise<IGraphicOverride | undefined>
}

export class CategoryInView implements ICategoryInView {
    index: number
    isHidden?: boolean
    
    viewIndex?: number
    view?: IView
    categoryIndex?: number
    category?: ICategory
    graphicOverrideIndex?: number
    graphicOverride?: IGraphicOverride
    
    static async createFromTable(table: ICategoryInViewTable, index: number): Promise<ICategoryInView> {
        let result = new CategoryInView()
        result.index = index
        
        await Promise.all([
            table.getIsHidden(index).then(v => result.isHidden = v),
            table.getViewIndex(index).then(v => result.viewIndex = v),
            table.getCategoryIndex(index).then(v => result.categoryIndex = v),
            table.getGraphicOverrideIndex(index).then(v => result.graphicOverrideIndex = v),
        ])
        
        return result
    }
}

export class CategoryInViewTable implements ICategoryInViewTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<ICategoryInViewTable | undefined> {
        const entity = await document.entities.getBfast("Vim.CategoryInView")
        
        if (!entity) {
            return undefined
        }
        
        let table = new CategoryInViewTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(categoryInViewIndex: number): Promise<ICategoryInView> {
        return await CategoryInView.createFromTable(this, categoryInViewIndex)
    }
    
    async getAll(): Promise<ICategoryInView[]> {
        const localTable = await this.entityTable.getLocal()
        
        let isHidden: boolean[] | undefined
        let viewIndex: number[] | undefined
        let categoryIndex: number[] | undefined
        let graphicOverrideIndex: number[] | undefined
        
        await Promise.all([
            (async () => { isHidden = (await localTable.getBooleanArray("byte:IsHidden")) })(),
            (async () => { viewIndex = (await localTable.getNumberArray("index:Vim.View:View")) })(),
            (async () => { categoryIndex = (await localTable.getNumberArray("index:Vim.Category:Category")) })(),
            (async () => { graphicOverrideIndex = (await localTable.getNumberArray("index:Vim.GraphicOverride:GraphicOverride")) })(),
        ])
        
        let categoryInView: ICategoryInView[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            categoryInView.push({
                index: i,
                isHidden: isHidden ? isHidden[i] : undefined,
                viewIndex: viewIndex ? viewIndex[i] : undefined,
                categoryIndex: categoryIndex ? categoryIndex[i] : undefined,
                graphicOverrideIndex: graphicOverrideIndex ? graphicOverrideIndex[i] : undefined
            })
        }
        
        return categoryInView
    }
    
    async getIsHidden(categoryInViewIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(categoryInViewIndex, "byte:IsHidden"))
    }
    
    async getAllIsHidden(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsHidden"))
    }
    
    async getViewIndex(categoryInViewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(categoryInViewIndex, "index:Vim.View:View")
    }
    
    async getAllViewIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.View:View")
    }
    
    async getView(categoryInViewIndex: number): Promise<IView | undefined> {
        const index = await this.getViewIndex(categoryInViewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.view?.get(index)
    }
    
    async getCategoryIndex(categoryInViewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(categoryInViewIndex, "index:Vim.Category:Category")
    }
    
    async getAllCategoryIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Category:Category")
    }
    
    async getCategory(categoryInViewIndex: number): Promise<ICategory | undefined> {
        const index = await this.getCategoryIndex(categoryInViewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.category?.get(index)
    }
    
    async getGraphicOverrideIndex(categoryInViewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(categoryInViewIndex, "index:Vim.GraphicOverride:GraphicOverride")
    }
    
    async getAllGraphicOverrideIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.GraphicOverride:GraphicOverride")
    }
    
    async getGraphicOverride(categoryInViewIndex: number): Promise<IGraphicOverride | undefined> {
        const index = await this.getGraphicOverrideIndex(categoryInViewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.graphicOverride?.get(index)
    }
    
}

export interface IElementOverrideInView {
    index: number
    isHidden?: boolean
    
    viewIndex?: number
    view?: IView
    graphicOverrideIndex?: number
    graphicOverride?: IGraphicOverride
    elementIndex?: number
    element?: IElement
}

export interface IElementOverrideInViewTable {
    getCount(): Promise<number>
    get(elementOverrideInViewIndex: number): Promise<IElementOverrideInView>
    getAll(): Promise<IElementOverrideInView[]>
    
    getIsHidden(elementOverrideInViewIndex: number): Promise<boolean | undefined>
    getAllIsHidden(): Promise<boolean[] | undefined>
    
    getViewIndex(elementOverrideInViewIndex: number): Promise<number | undefined>
    getAllViewIndex(): Promise<number[] | undefined>
    getView(elementOverrideInViewIndex: number): Promise<IView | undefined>
    getGraphicOverrideIndex(elementOverrideInViewIndex: number): Promise<number | undefined>
    getAllGraphicOverrideIndex(): Promise<number[] | undefined>
    getGraphicOverride(elementOverrideInViewIndex: number): Promise<IGraphicOverride | undefined>
    getElementIndex(elementOverrideInViewIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(elementOverrideInViewIndex: number): Promise<IElement | undefined>
}

export class ElementOverrideInView implements IElementOverrideInView {
    index: number
    isHidden?: boolean
    
    viewIndex?: number
    view?: IView
    graphicOverrideIndex?: number
    graphicOverride?: IGraphicOverride
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IElementOverrideInViewTable, index: number): Promise<IElementOverrideInView> {
        let result = new ElementOverrideInView()
        result.index = index
        
        await Promise.all([
            table.getIsHidden(index).then(v => result.isHidden = v),
            table.getViewIndex(index).then(v => result.viewIndex = v),
            table.getGraphicOverrideIndex(index).then(v => result.graphicOverrideIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class ElementOverrideInViewTable implements IElementOverrideInViewTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IElementOverrideInViewTable | undefined> {
        const entity = await document.entities.getBfast("Vim.ElementOverrideInView")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ElementOverrideInViewTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(elementOverrideInViewIndex: number): Promise<IElementOverrideInView> {
        return await ElementOverrideInView.createFromTable(this, elementOverrideInViewIndex)
    }
    
    async getAll(): Promise<IElementOverrideInView[]> {
        const localTable = await this.entityTable.getLocal()
        
        let isHidden: boolean[] | undefined
        let viewIndex: number[] | undefined
        let graphicOverrideIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { isHidden = (await localTable.getBooleanArray("byte:IsHidden")) })(),
            (async () => { viewIndex = (await localTable.getNumberArray("index:Vim.View:View")) })(),
            (async () => { graphicOverrideIndex = (await localTable.getNumberArray("index:Vim.GraphicOverride:GraphicOverride")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let elementOverrideInView: IElementOverrideInView[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            elementOverrideInView.push({
                index: i,
                isHidden: isHidden ? isHidden[i] : undefined,
                viewIndex: viewIndex ? viewIndex[i] : undefined,
                graphicOverrideIndex: graphicOverrideIndex ? graphicOverrideIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return elementOverrideInView
    }
    
    async getIsHidden(elementOverrideInViewIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(elementOverrideInViewIndex, "byte:IsHidden"))
    }
    
    async getAllIsHidden(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsHidden"))
    }
    
    async getViewIndex(elementOverrideInViewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementOverrideInViewIndex, "index:Vim.View:View")
    }
    
    async getAllViewIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.View:View")
    }
    
    async getView(elementOverrideInViewIndex: number): Promise<IView | undefined> {
        const index = await this.getViewIndex(elementOverrideInViewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.view?.get(index)
    }
    
    async getGraphicOverrideIndex(elementOverrideInViewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementOverrideInViewIndex, "index:Vim.GraphicOverride:GraphicOverride")
    }
    
    async getAllGraphicOverrideIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.GraphicOverride:GraphicOverride")
    }
    
    async getGraphicOverride(elementOverrideInViewIndex: number): Promise<IGraphicOverride | undefined> {
        const index = await this.getGraphicOverrideIndex(elementOverrideInViewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.graphicOverride?.get(index)
    }
    
    async getElementIndex(elementOverrideInViewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementOverrideInViewIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(elementOverrideInViewIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(elementOverrideInViewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IPhaseGraphicOverride {
    index: number
    status?: string
    scale?: number
    hasProjectionLine?: boolean
    projectionLineColor_X?: number
    projectionLineColor_Y?: number
    projectionLineColor_Z?: number
    projectionLineWidth?: number
    projectionLineWeight?: number
    hasCutLine?: boolean
    cutLineColor_X?: number
    cutLineColor_Y?: number
    cutLineColor_Z?: number
    cutLineWidth?: number
    cutLineWeight?: number
    
    projectionLinePatternIndex?: number
    projectionLinePattern?: ILinePattern
    cutLinePatternIndex?: number
    cutLinePattern?: ILinePattern
    bimDocumentIndex?: number
    bimDocument?: IBimDocument
}

export interface IPhaseGraphicOverrideTable {
    getCount(): Promise<number>
    get(phaseGraphicOverrideIndex: number): Promise<IPhaseGraphicOverride>
    getAll(): Promise<IPhaseGraphicOverride[]>
    
    getStatus(phaseGraphicOverrideIndex: number): Promise<string | undefined>
    getAllStatus(): Promise<string[] | undefined>
    getScale(phaseGraphicOverrideIndex: number): Promise<number | undefined>
    getAllScale(): Promise<number[] | undefined>
    getHasProjectionLine(phaseGraphicOverrideIndex: number): Promise<boolean | undefined>
    getAllHasProjectionLine(): Promise<boolean[] | undefined>
    getProjectionLineColor_X(phaseGraphicOverrideIndex: number): Promise<number | undefined>
    getAllProjectionLineColor_X(): Promise<number[] | undefined>
    getProjectionLineColor_Y(phaseGraphicOverrideIndex: number): Promise<number | undefined>
    getAllProjectionLineColor_Y(): Promise<number[] | undefined>
    getProjectionLineColor_Z(phaseGraphicOverrideIndex: number): Promise<number | undefined>
    getAllProjectionLineColor_Z(): Promise<number[] | undefined>
    getProjectionLineWidth(phaseGraphicOverrideIndex: number): Promise<number | undefined>
    getAllProjectionLineWidth(): Promise<number[] | undefined>
    getProjectionLineWeight(phaseGraphicOverrideIndex: number): Promise<number | undefined>
    getAllProjectionLineWeight(): Promise<number[] | undefined>
    getHasCutLine(phaseGraphicOverrideIndex: number): Promise<boolean | undefined>
    getAllHasCutLine(): Promise<boolean[] | undefined>
    getCutLineColor_X(phaseGraphicOverrideIndex: number): Promise<number | undefined>
    getAllCutLineColor_X(): Promise<number[] | undefined>
    getCutLineColor_Y(phaseGraphicOverrideIndex: number): Promise<number | undefined>
    getAllCutLineColor_Y(): Promise<number[] | undefined>
    getCutLineColor_Z(phaseGraphicOverrideIndex: number): Promise<number | undefined>
    getAllCutLineColor_Z(): Promise<number[] | undefined>
    getCutLineWidth(phaseGraphicOverrideIndex: number): Promise<number | undefined>
    getAllCutLineWidth(): Promise<number[] | undefined>
    getCutLineWeight(phaseGraphicOverrideIndex: number): Promise<number | undefined>
    getAllCutLineWeight(): Promise<number[] | undefined>
    
    getProjectionLinePatternIndex(phaseGraphicOverrideIndex: number): Promise<number | undefined>
    getAllProjectionLinePatternIndex(): Promise<number[] | undefined>
    getProjectionLinePattern(phaseGraphicOverrideIndex: number): Promise<ILinePattern | undefined>
    getCutLinePatternIndex(phaseGraphicOverrideIndex: number): Promise<number | undefined>
    getAllCutLinePatternIndex(): Promise<number[] | undefined>
    getCutLinePattern(phaseGraphicOverrideIndex: number): Promise<ILinePattern | undefined>
    getBimDocumentIndex(phaseGraphicOverrideIndex: number): Promise<number | undefined>
    getAllBimDocumentIndex(): Promise<number[] | undefined>
    getBimDocument(phaseGraphicOverrideIndex: number): Promise<IBimDocument | undefined>
}

export class PhaseGraphicOverride implements IPhaseGraphicOverride {
    index: number
    status?: string
    scale?: number
    hasProjectionLine?: boolean
    projectionLineColor_X?: number
    projectionLineColor_Y?: number
    projectionLineColor_Z?: number
    projectionLineWidth?: number
    projectionLineWeight?: number
    hasCutLine?: boolean
    cutLineColor_X?: number
    cutLineColor_Y?: number
    cutLineColor_Z?: number
    cutLineWidth?: number
    cutLineWeight?: number
    
    projectionLinePatternIndex?: number
    projectionLinePattern?: ILinePattern
    cutLinePatternIndex?: number
    cutLinePattern?: ILinePattern
    bimDocumentIndex?: number
    bimDocument?: IBimDocument
    
    static async createFromTable(table: IPhaseGraphicOverrideTable, index: number): Promise<IPhaseGraphicOverride> {
        let result = new PhaseGraphicOverride()
        result.index = index
        
        await Promise.all([
            table.getStatus(index).then(v => result.status = v),
            table.getScale(index).then(v => result.scale = v),
            table.getHasProjectionLine(index).then(v => result.hasProjectionLine = v),
            table.getProjectionLineColor_X(index).then(v => result.projectionLineColor_X = v),
            table.getProjectionLineColor_Y(index).then(v => result.projectionLineColor_Y = v),
            table.getProjectionLineColor_Z(index).then(v => result.projectionLineColor_Z = v),
            table.getProjectionLineWidth(index).then(v => result.projectionLineWidth = v),
            table.getProjectionLineWeight(index).then(v => result.projectionLineWeight = v),
            table.getHasCutLine(index).then(v => result.hasCutLine = v),
            table.getCutLineColor_X(index).then(v => result.cutLineColor_X = v),
            table.getCutLineColor_Y(index).then(v => result.cutLineColor_Y = v),
            table.getCutLineColor_Z(index).then(v => result.cutLineColor_Z = v),
            table.getCutLineWidth(index).then(v => result.cutLineWidth = v),
            table.getCutLineWeight(index).then(v => result.cutLineWeight = v),
            table.getProjectionLinePatternIndex(index).then(v => result.projectionLinePatternIndex = v),
            table.getCutLinePatternIndex(index).then(v => result.cutLinePatternIndex = v),
            table.getBimDocumentIndex(index).then(v => result.bimDocumentIndex = v),
        ])
        
        return result
    }
}

export class PhaseGraphicOverrideTable implements IPhaseGraphicOverrideTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IPhaseGraphicOverrideTable | undefined> {
        const entity = await document.entities.getBfast("Vim.PhaseGraphicOverride")
        
        if (!entity) {
            return undefined
        }
        
        let table = new PhaseGraphicOverrideTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(phaseGraphicOverrideIndex: number): Promise<IPhaseGraphicOverride> {
        return await PhaseGraphicOverride.createFromTable(this, phaseGraphicOverrideIndex)
    }
    
    async getAll(): Promise<IPhaseGraphicOverride[]> {
        const localTable = await this.entityTable.getLocal()
        
        let status: string[] | undefined
        let scale: number[] | undefined
        let hasProjectionLine: boolean[] | undefined
        let projectionLineColor_X: number[] | undefined
        let projectionLineColor_Y: number[] | undefined
        let projectionLineColor_Z: number[] | undefined
        let projectionLineWidth: number[] | undefined
        let projectionLineWeight: number[] | undefined
        let hasCutLine: boolean[] | undefined
        let cutLineColor_X: number[] | undefined
        let cutLineColor_Y: number[] | undefined
        let cutLineColor_Z: number[] | undefined
        let cutLineWidth: number[] | undefined
        let cutLineWeight: number[] | undefined
        let projectionLinePatternIndex: number[] | undefined
        let cutLinePatternIndex: number[] | undefined
        let bimDocumentIndex: number[] | undefined
        
        await Promise.all([
            (async () => { status = (await localTable.getStringArray("string:Status")) })(),
            (async () => { scale = (await localTable.getNumberArray("int:Scale")) })(),
            (async () => { hasProjectionLine = (await localTable.getBooleanArray("byte:HasProjectionLine")) })(),
            (async () => { projectionLineColor_X = (await localTable.getNumberArray("double:ProjectionLineColor.X")) })(),
            (async () => { projectionLineColor_Y = (await localTable.getNumberArray("double:ProjectionLineColor.Y")) })(),
            (async () => { projectionLineColor_Z = (await localTable.getNumberArray("double:ProjectionLineColor.Z")) })(),
            (async () => { projectionLineWidth = (await localTable.getNumberArray("double:ProjectionLineWidth")) })(),
            (async () => { projectionLineWeight = (await localTable.getNumberArray("int:ProjectionLineWeight")) })(),
            (async () => { hasCutLine = (await localTable.getBooleanArray("byte:HasCutLine")) })(),
            (async () => { cutLineColor_X = (await localTable.getNumberArray("double:CutLineColor.X")) })(),
            (async () => { cutLineColor_Y = (await localTable.getNumberArray("double:CutLineColor.Y")) })(),
            (async () => { cutLineColor_Z = (await localTable.getNumberArray("double:CutLineColor.Z")) })(),
            (async () => { cutLineWidth = (await localTable.getNumberArray("double:CutLineWidth")) })(),
            (async () => { cutLineWeight = (await localTable.getNumberArray("int:CutLineWeight")) })(),
            (async () => { projectionLinePatternIndex = (await localTable.getNumberArray("index:Vim.LinePattern:ProjectionLinePattern")) })(),
            (async () => { cutLinePatternIndex = (await localTable.getNumberArray("index:Vim.LinePattern:CutLinePattern")) })(),
            (async () => { bimDocumentIndex = (await localTable.getNumberArray("index:Vim.BimDocument:BimDocument")) })(),
        ])
        
        let phaseGraphicOverride: IPhaseGraphicOverride[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            phaseGraphicOverride.push({
                index: i,
                status: status ? status[i] : undefined,
                scale: scale ? scale[i] : undefined,
                hasProjectionLine: hasProjectionLine ? hasProjectionLine[i] : undefined,
                projectionLineColor_X: projectionLineColor_X ? projectionLineColor_X[i] : undefined,
                projectionLineColor_Y: projectionLineColor_Y ? projectionLineColor_Y[i] : undefined,
                projectionLineColor_Z: projectionLineColor_Z ? projectionLineColor_Z[i] : undefined,
                projectionLineWidth: projectionLineWidth ? projectionLineWidth[i] : undefined,
                projectionLineWeight: projectionLineWeight ? projectionLineWeight[i] : undefined,
                hasCutLine: hasCutLine ? hasCutLine[i] : undefined,
                cutLineColor_X: cutLineColor_X ? cutLineColor_X[i] : undefined,
                cutLineColor_Y: cutLineColor_Y ? cutLineColor_Y[i] : undefined,
                cutLineColor_Z: cutLineColor_Z ? cutLineColor_Z[i] : undefined,
                cutLineWidth: cutLineWidth ? cutLineWidth[i] : undefined,
                cutLineWeight: cutLineWeight ? cutLineWeight[i] : undefined,
                projectionLinePatternIndex: projectionLinePatternIndex ? projectionLinePatternIndex[i] : undefined,
                cutLinePatternIndex: cutLinePatternIndex ? cutLinePatternIndex[i] : undefined,
                bimDocumentIndex: bimDocumentIndex ? bimDocumentIndex[i] : undefined
            })
        }
        
        return phaseGraphicOverride
    }
    
    async getStatus(phaseGraphicOverrideIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(phaseGraphicOverrideIndex, "string:Status"))
    }
    
    async getAllStatus(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Status"))
    }
    
    async getScale(phaseGraphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(phaseGraphicOverrideIndex, "int:Scale"))
    }
    
    async getAllScale(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Scale"))
    }
    
    async getHasProjectionLine(phaseGraphicOverrideIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(phaseGraphicOverrideIndex, "byte:HasProjectionLine"))
    }
    
    async getAllHasProjectionLine(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:HasProjectionLine"))
    }
    
    async getProjectionLineColor_X(phaseGraphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(phaseGraphicOverrideIndex, "double:ProjectionLineColor.X"))
    }
    
    async getAllProjectionLineColor_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:ProjectionLineColor.X"))
    }
    
    async getProjectionLineColor_Y(phaseGraphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(phaseGraphicOverrideIndex, "double:ProjectionLineColor.Y"))
    }
    
    async getAllProjectionLineColor_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:ProjectionLineColor.Y"))
    }
    
    async getProjectionLineColor_Z(phaseGraphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(phaseGraphicOverrideIndex, "double:ProjectionLineColor.Z"))
    }
    
    async getAllProjectionLineColor_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:ProjectionLineColor.Z"))
    }
    
    async getProjectionLineWidth(phaseGraphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(phaseGraphicOverrideIndex, "double:ProjectionLineWidth"))
    }
    
    async getAllProjectionLineWidth(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:ProjectionLineWidth"))
    }
    
    async getProjectionLineWeight(phaseGraphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(phaseGraphicOverrideIndex, "int:ProjectionLineWeight"))
    }
    
    async getAllProjectionLineWeight(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:ProjectionLineWeight"))
    }
    
    async getHasCutLine(phaseGraphicOverrideIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(phaseGraphicOverrideIndex, "byte:HasCutLine"))
    }
    
    async getAllHasCutLine(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:HasCutLine"))
    }
    
    async getCutLineColor_X(phaseGraphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(phaseGraphicOverrideIndex, "double:CutLineColor.X"))
    }
    
    async getAllCutLineColor_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:CutLineColor.X"))
    }
    
    async getCutLineColor_Y(phaseGraphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(phaseGraphicOverrideIndex, "double:CutLineColor.Y"))
    }
    
    async getAllCutLineColor_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:CutLineColor.Y"))
    }
    
    async getCutLineColor_Z(phaseGraphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(phaseGraphicOverrideIndex, "double:CutLineColor.Z"))
    }
    
    async getAllCutLineColor_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:CutLineColor.Z"))
    }
    
    async getCutLineWidth(phaseGraphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(phaseGraphicOverrideIndex, "double:CutLineWidth"))
    }
    
    async getAllCutLineWidth(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:CutLineWidth"))
    }
    
    async getCutLineWeight(phaseGraphicOverrideIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(phaseGraphicOverrideIndex, "int:CutLineWeight"))
    }
    
    async getAllCutLineWeight(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:CutLineWeight"))
    }
    
    async getProjectionLinePatternIndex(phaseGraphicOverrideIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(phaseGraphicOverrideIndex, "index:Vim.LinePattern:ProjectionLinePattern")
    }
    
    async getAllProjectionLinePatternIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.LinePattern:ProjectionLinePattern")
    }
    
    async getProjectionLinePattern(phaseGraphicOverrideIndex: number): Promise<ILinePattern | undefined> {
        const index = await this.getProjectionLinePatternIndex(phaseGraphicOverrideIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.linePattern?.get(index)
    }
    
    async getCutLinePatternIndex(phaseGraphicOverrideIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(phaseGraphicOverrideIndex, "index:Vim.LinePattern:CutLinePattern")
    }
    
    async getAllCutLinePatternIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.LinePattern:CutLinePattern")
    }
    
    async getCutLinePattern(phaseGraphicOverrideIndex: number): Promise<ILinePattern | undefined> {
        const index = await this.getCutLinePatternIndex(phaseGraphicOverrideIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.linePattern?.get(index)
    }
    
    async getBimDocumentIndex(phaseGraphicOverrideIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(phaseGraphicOverrideIndex, "index:Vim.BimDocument:BimDocument")
    }
    
    async getAllBimDocumentIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.BimDocument:BimDocument")
    }
    
    async getBimDocument(phaseGraphicOverrideIndex: number): Promise<IBimDocument | undefined> {
        const index = await this.getBimDocumentIndex(phaseGraphicOverrideIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.bimDocument?.get(index)
    }
    
}

export interface IElementInView {
    index: number
    
    viewIndex?: number
    view?: IView
    elementIndex?: number
    element?: IElement
}

export interface IElementInViewTable {
    getCount(): Promise<number>
    get(elementInViewIndex: number): Promise<IElementInView>
    getAll(): Promise<IElementInView[]>
    
    getViewIndex(elementInViewIndex: number): Promise<number | undefined>
    getAllViewIndex(): Promise<number[] | undefined>
    getView(elementInViewIndex: number): Promise<IView | undefined>
    getElementIndex(elementInViewIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(elementInViewIndex: number): Promise<IElement | undefined>
}

export class ElementInView implements IElementInView {
    index: number
    
    viewIndex?: number
    view?: IView
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IElementInViewTable, index: number): Promise<IElementInView> {
        let result = new ElementInView()
        result.index = index
        
        await Promise.all([
            table.getViewIndex(index).then(v => result.viewIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class ElementInViewTable implements IElementInViewTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IElementInViewTable | undefined> {
        const entity = await document.entities.getBfast("Vim.ElementInView")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ElementInViewTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(elementInViewIndex: number): Promise<IElementInView> {
        return await ElementInView.createFromTable(this, elementInViewIndex)
    }
    
    async getAll(): Promise<IElementInView[]> {
        const localTable = await this.entityTable.getLocal()
        
        let viewIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { viewIndex = (await localTable.getNumberArray("index:Vim.View:View")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let elementInView: IElementInView[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            elementInView.push({
                index: i,
                viewIndex: viewIndex ? viewIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return elementInView
    }
    
    async getViewIndex(elementInViewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementInViewIndex, "index:Vim.View:View")
    }
    
    async getAllViewIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.View:View")
    }
    
    async getView(elementInViewIndex: number): Promise<IView | undefined> {
        const index = await this.getViewIndex(elementInViewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.view?.get(index)
    }
    
    async getElementIndex(elementInViewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementInViewIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(elementInViewIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(elementInViewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IShapeInView {
    index: number
    
    shapeIndex?: number
    shape?: IShape
    viewIndex?: number
    view?: IView
}

export interface IShapeInViewTable {
    getCount(): Promise<number>
    get(shapeInViewIndex: number): Promise<IShapeInView>
    getAll(): Promise<IShapeInView[]>
    
    getShapeIndex(shapeInViewIndex: number): Promise<number | undefined>
    getAllShapeIndex(): Promise<number[] | undefined>
    getShape(shapeInViewIndex: number): Promise<IShape | undefined>
    getViewIndex(shapeInViewIndex: number): Promise<number | undefined>
    getAllViewIndex(): Promise<number[] | undefined>
    getView(shapeInViewIndex: number): Promise<IView | undefined>
}

export class ShapeInView implements IShapeInView {
    index: number
    
    shapeIndex?: number
    shape?: IShape
    viewIndex?: number
    view?: IView
    
    static async createFromTable(table: IShapeInViewTable, index: number): Promise<IShapeInView> {
        let result = new ShapeInView()
        result.index = index
        
        await Promise.all([
            table.getShapeIndex(index).then(v => result.shapeIndex = v),
            table.getViewIndex(index).then(v => result.viewIndex = v),
        ])
        
        return result
    }
}

export class ShapeInViewTable implements IShapeInViewTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IShapeInViewTable | undefined> {
        const entity = await document.entities.getBfast("Vim.ShapeInView")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ShapeInViewTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(shapeInViewIndex: number): Promise<IShapeInView> {
        return await ShapeInView.createFromTable(this, shapeInViewIndex)
    }
    
    async getAll(): Promise<IShapeInView[]> {
        const localTable = await this.entityTable.getLocal()
        
        let shapeIndex: number[] | undefined
        let viewIndex: number[] | undefined
        
        await Promise.all([
            (async () => { shapeIndex = (await localTable.getNumberArray("index:Vim.Shape:Shape")) })(),
            (async () => { viewIndex = (await localTable.getNumberArray("index:Vim.View:View")) })(),
        ])
        
        let shapeInView: IShapeInView[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            shapeInView.push({
                index: i,
                shapeIndex: shapeIndex ? shapeIndex[i] : undefined,
                viewIndex: viewIndex ? viewIndex[i] : undefined
            })
        }
        
        return shapeInView
    }
    
    async getShapeIndex(shapeInViewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(shapeInViewIndex, "index:Vim.Shape:Shape")
    }
    
    async getAllShapeIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Shape:Shape")
    }
    
    async getShape(shapeInViewIndex: number): Promise<IShape | undefined> {
        const index = await this.getShapeIndex(shapeInViewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.shape?.get(index)
    }
    
    async getViewIndex(shapeInViewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(shapeInViewIndex, "index:Vim.View:View")
    }
    
    async getAllViewIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.View:View")
    }
    
    async getView(shapeInViewIndex: number): Promise<IView | undefined> {
        const index = await this.getViewIndex(shapeInViewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.view?.get(index)
    }
    
}

export interface IAssetInView {
    index: number
    
    assetIndex?: number
    asset?: IAsset
    viewIndex?: number
    view?: IView
}

export interface IAssetInViewTable {
    getCount(): Promise<number>
    get(assetInViewIndex: number): Promise<IAssetInView>
    getAll(): Promise<IAssetInView[]>
    
    getAssetIndex(assetInViewIndex: number): Promise<number | undefined>
    getAllAssetIndex(): Promise<number[] | undefined>
    getAsset(assetInViewIndex: number): Promise<IAsset | undefined>
    getViewIndex(assetInViewIndex: number): Promise<number | undefined>
    getAllViewIndex(): Promise<number[] | undefined>
    getView(assetInViewIndex: number): Promise<IView | undefined>
}

export class AssetInView implements IAssetInView {
    index: number
    
    assetIndex?: number
    asset?: IAsset
    viewIndex?: number
    view?: IView
    
    static async createFromTable(table: IAssetInViewTable, index: number): Promise<IAssetInView> {
        let result = new AssetInView()
        result.index = index
        
        await Promise.all([
            table.getAssetIndex(index).then(v => result.assetIndex = v),
            table.getViewIndex(index).then(v => result.viewIndex = v),
        ])
        
        return result
    }
}

export class AssetInViewTable implements IAssetInViewTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IAssetInViewTable | undefined> {
        const entity = await document.entities.getBfast("Vim.AssetInView")
        
        if (!entity) {
            return undefined
        }
        
        let table = new AssetInViewTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(assetInViewIndex: number): Promise<IAssetInView> {
        return await AssetInView.createFromTable(this, assetInViewIndex)
    }
    
    async getAll(): Promise<IAssetInView[]> {
        const localTable = await this.entityTable.getLocal()
        
        let assetIndex: number[] | undefined
        let viewIndex: number[] | undefined
        
        await Promise.all([
            (async () => { assetIndex = (await localTable.getNumberArray("index:Vim.Asset:Asset")) })(),
            (async () => { viewIndex = (await localTable.getNumberArray("index:Vim.View:View")) })(),
        ])
        
        let assetInView: IAssetInView[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            assetInView.push({
                index: i,
                assetIndex: assetIndex ? assetIndex[i] : undefined,
                viewIndex: viewIndex ? viewIndex[i] : undefined
            })
        }
        
        return assetInView
    }
    
    async getAssetIndex(assetInViewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(assetInViewIndex, "index:Vim.Asset:Asset")
    }
    
    async getAllAssetIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Asset:Asset")
    }
    
    async getAsset(assetInViewIndex: number): Promise<IAsset | undefined> {
        const index = await this.getAssetIndex(assetInViewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.asset?.get(index)
    }
    
    async getViewIndex(assetInViewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(assetInViewIndex, "index:Vim.View:View")
    }
    
    async getAllViewIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.View:View")
    }
    
    async getView(assetInViewIndex: number): Promise<IView | undefined> {
        const index = await this.getViewIndex(assetInViewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.view?.get(index)
    }
    
}

export interface IAssetInViewSheet {
    index: number
    
    assetIndex?: number
    asset?: IAsset
    viewSheetIndex?: number
    viewSheet?: IViewSheet
}

export interface IAssetInViewSheetTable {
    getCount(): Promise<number>
    get(assetInViewSheetIndex: number): Promise<IAssetInViewSheet>
    getAll(): Promise<IAssetInViewSheet[]>
    
    getAssetIndex(assetInViewSheetIndex: number): Promise<number | undefined>
    getAllAssetIndex(): Promise<number[] | undefined>
    getAsset(assetInViewSheetIndex: number): Promise<IAsset | undefined>
    getViewSheetIndex(assetInViewSheetIndex: number): Promise<number | undefined>
    getAllViewSheetIndex(): Promise<number[] | undefined>
    getViewSheet(assetInViewSheetIndex: number): Promise<IViewSheet | undefined>
}

export class AssetInViewSheet implements IAssetInViewSheet {
    index: number
    
    assetIndex?: number
    asset?: IAsset
    viewSheetIndex?: number
    viewSheet?: IViewSheet
    
    static async createFromTable(table: IAssetInViewSheetTable, index: number): Promise<IAssetInViewSheet> {
        let result = new AssetInViewSheet()
        result.index = index
        
        await Promise.all([
            table.getAssetIndex(index).then(v => result.assetIndex = v),
            table.getViewSheetIndex(index).then(v => result.viewSheetIndex = v),
        ])
        
        return result
    }
}

export class AssetInViewSheetTable implements IAssetInViewSheetTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IAssetInViewSheetTable | undefined> {
        const entity = await document.entities.getBfast("Vim.AssetInViewSheet")
        
        if (!entity) {
            return undefined
        }
        
        let table = new AssetInViewSheetTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(assetInViewSheetIndex: number): Promise<IAssetInViewSheet> {
        return await AssetInViewSheet.createFromTable(this, assetInViewSheetIndex)
    }
    
    async getAll(): Promise<IAssetInViewSheet[]> {
        const localTable = await this.entityTable.getLocal()
        
        let assetIndex: number[] | undefined
        let viewSheetIndex: number[] | undefined
        
        await Promise.all([
            (async () => { assetIndex = (await localTable.getNumberArray("index:Vim.Asset:Asset")) })(),
            (async () => { viewSheetIndex = (await localTable.getNumberArray("index:Vim.ViewSheet:ViewSheet")) })(),
        ])
        
        let assetInViewSheet: IAssetInViewSheet[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            assetInViewSheet.push({
                index: i,
                assetIndex: assetIndex ? assetIndex[i] : undefined,
                viewSheetIndex: viewSheetIndex ? viewSheetIndex[i] : undefined
            })
        }
        
        return assetInViewSheet
    }
    
    async getAssetIndex(assetInViewSheetIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(assetInViewSheetIndex, "index:Vim.Asset:Asset")
    }
    
    async getAllAssetIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Asset:Asset")
    }
    
    async getAsset(assetInViewSheetIndex: number): Promise<IAsset | undefined> {
        const index = await this.getAssetIndex(assetInViewSheetIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.asset?.get(index)
    }
    
    async getViewSheetIndex(assetInViewSheetIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(assetInViewSheetIndex, "index:Vim.ViewSheet:ViewSheet")
    }
    
    async getAllViewSheetIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.ViewSheet:ViewSheet")
    }
    
    async getViewSheet(assetInViewSheetIndex: number): Promise<IViewSheet | undefined> {
        const index = await this.getViewSheetIndex(assetInViewSheetIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.viewSheet?.get(index)
    }
    
}

export interface ILevelInView {
    index: number
    extents_Min_X?: number
    extents_Min_Y?: number
    extents_Min_Z?: number
    extents_Max_X?: number
    extents_Max_Y?: number
    extents_Max_Z?: number
    
    levelIndex?: number
    level?: ILevel
    viewIndex?: number
    view?: IView
}

export interface ILevelInViewTable {
    getCount(): Promise<number>
    get(levelInViewIndex: number): Promise<ILevelInView>
    getAll(): Promise<ILevelInView[]>
    
    getExtents_Min_X(levelInViewIndex: number): Promise<number | undefined>
    getAllExtents_Min_X(): Promise<number[] | undefined>
    getExtents_Min_Y(levelInViewIndex: number): Promise<number | undefined>
    getAllExtents_Min_Y(): Promise<number[] | undefined>
    getExtents_Min_Z(levelInViewIndex: number): Promise<number | undefined>
    getAllExtents_Min_Z(): Promise<number[] | undefined>
    getExtents_Max_X(levelInViewIndex: number): Promise<number | undefined>
    getAllExtents_Max_X(): Promise<number[] | undefined>
    getExtents_Max_Y(levelInViewIndex: number): Promise<number | undefined>
    getAllExtents_Max_Y(): Promise<number[] | undefined>
    getExtents_Max_Z(levelInViewIndex: number): Promise<number | undefined>
    getAllExtents_Max_Z(): Promise<number[] | undefined>
    
    getLevelIndex(levelInViewIndex: number): Promise<number | undefined>
    getAllLevelIndex(): Promise<number[] | undefined>
    getLevel(levelInViewIndex: number): Promise<ILevel | undefined>
    getViewIndex(levelInViewIndex: number): Promise<number | undefined>
    getAllViewIndex(): Promise<number[] | undefined>
    getView(levelInViewIndex: number): Promise<IView | undefined>
}

export class LevelInView implements ILevelInView {
    index: number
    extents_Min_X?: number
    extents_Min_Y?: number
    extents_Min_Z?: number
    extents_Max_X?: number
    extents_Max_Y?: number
    extents_Max_Z?: number
    
    levelIndex?: number
    level?: ILevel
    viewIndex?: number
    view?: IView
    
    static async createFromTable(table: ILevelInViewTable, index: number): Promise<ILevelInView> {
        let result = new LevelInView()
        result.index = index
        
        await Promise.all([
            table.getExtents_Min_X(index).then(v => result.extents_Min_X = v),
            table.getExtents_Min_Y(index).then(v => result.extents_Min_Y = v),
            table.getExtents_Min_Z(index).then(v => result.extents_Min_Z = v),
            table.getExtents_Max_X(index).then(v => result.extents_Max_X = v),
            table.getExtents_Max_Y(index).then(v => result.extents_Max_Y = v),
            table.getExtents_Max_Z(index).then(v => result.extents_Max_Z = v),
            table.getLevelIndex(index).then(v => result.levelIndex = v),
            table.getViewIndex(index).then(v => result.viewIndex = v),
        ])
        
        return result
    }
}

export class LevelInViewTable implements ILevelInViewTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<ILevelInViewTable | undefined> {
        const entity = await document.entities.getBfast("Vim.LevelInView")
        
        if (!entity) {
            return undefined
        }
        
        let table = new LevelInViewTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(levelInViewIndex: number): Promise<ILevelInView> {
        return await LevelInView.createFromTable(this, levelInViewIndex)
    }
    
    async getAll(): Promise<ILevelInView[]> {
        const localTable = await this.entityTable.getLocal()
        
        let extents_Min_X: number[] | undefined
        let extents_Min_Y: number[] | undefined
        let extents_Min_Z: number[] | undefined
        let extents_Max_X: number[] | undefined
        let extents_Max_Y: number[] | undefined
        let extents_Max_Z: number[] | undefined
        let levelIndex: number[] | undefined
        let viewIndex: number[] | undefined
        
        await Promise.all([
            (async () => { extents_Min_X = (await localTable.getNumberArray("double:Extents.Min.X")) })(),
            (async () => { extents_Min_Y = (await localTable.getNumberArray("double:Extents.Min.Y")) })(),
            (async () => { extents_Min_Z = (await localTable.getNumberArray("double:Extents.Min.Z")) })(),
            (async () => { extents_Max_X = (await localTable.getNumberArray("double:Extents.Max.X")) })(),
            (async () => { extents_Max_Y = (await localTable.getNumberArray("double:Extents.Max.Y")) })(),
            (async () => { extents_Max_Z = (await localTable.getNumberArray("double:Extents.Max.Z")) })(),
            (async () => { levelIndex = (await localTable.getNumberArray("index:Vim.Level:Level")) })(),
            (async () => { viewIndex = (await localTable.getNumberArray("index:Vim.View:View")) })(),
        ])
        
        let levelInView: ILevelInView[] = []
        
        const rowCount = await this.getCount()
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
            })
        }
        
        return levelInView
    }
    
    async getExtents_Min_X(levelInViewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(levelInViewIndex, "double:Extents.Min.X"))
    }
    
    async getAllExtents_Min_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Extents.Min.X"))
    }
    
    async getExtents_Min_Y(levelInViewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(levelInViewIndex, "double:Extents.Min.Y"))
    }
    
    async getAllExtents_Min_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Extents.Min.Y"))
    }
    
    async getExtents_Min_Z(levelInViewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(levelInViewIndex, "double:Extents.Min.Z"))
    }
    
    async getAllExtents_Min_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Extents.Min.Z"))
    }
    
    async getExtents_Max_X(levelInViewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(levelInViewIndex, "double:Extents.Max.X"))
    }
    
    async getAllExtents_Max_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Extents.Max.X"))
    }
    
    async getExtents_Max_Y(levelInViewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(levelInViewIndex, "double:Extents.Max.Y"))
    }
    
    async getAllExtents_Max_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Extents.Max.Y"))
    }
    
    async getExtents_Max_Z(levelInViewIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(levelInViewIndex, "double:Extents.Max.Z"))
    }
    
    async getAllExtents_Max_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Extents.Max.Z"))
    }
    
    async getLevelIndex(levelInViewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(levelInViewIndex, "index:Vim.Level:Level")
    }
    
    async getAllLevelIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Level:Level")
    }
    
    async getLevel(levelInViewIndex: number): Promise<ILevel | undefined> {
        const index = await this.getLevelIndex(levelInViewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.level?.get(index)
    }
    
    async getViewIndex(levelInViewIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(levelInViewIndex, "index:Vim.View:View")
    }
    
    async getAllViewIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.View:View")
    }
    
    async getView(levelInViewIndex: number): Promise<IView | undefined> {
        const index = await this.getViewIndex(levelInViewIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.view?.get(index)
    }
    
}

export interface ICamera {
    index: number
    id?: number
    isPerspective?: number
    verticalExtent?: number
    horizontalExtent?: number
    farDistance?: number
    nearDistance?: number
    targetDistance?: number
    rightOffset?: number
    upOffset?: number
}

export interface ICameraTable {
    getCount(): Promise<number>
    get(cameraIndex: number): Promise<ICamera>
    getAll(): Promise<ICamera[]>
    
    getId(cameraIndex: number): Promise<number | undefined>
    getAllId(): Promise<number[] | undefined>
    getIsPerspective(cameraIndex: number): Promise<number | undefined>
    getAllIsPerspective(): Promise<number[] | undefined>
    getVerticalExtent(cameraIndex: number): Promise<number | undefined>
    getAllVerticalExtent(): Promise<number[] | undefined>
    getHorizontalExtent(cameraIndex: number): Promise<number | undefined>
    getAllHorizontalExtent(): Promise<number[] | undefined>
    getFarDistance(cameraIndex: number): Promise<number | undefined>
    getAllFarDistance(): Promise<number[] | undefined>
    getNearDistance(cameraIndex: number): Promise<number | undefined>
    getAllNearDistance(): Promise<number[] | undefined>
    getTargetDistance(cameraIndex: number): Promise<number | undefined>
    getAllTargetDistance(): Promise<number[] | undefined>
    getRightOffset(cameraIndex: number): Promise<number | undefined>
    getAllRightOffset(): Promise<number[] | undefined>
    getUpOffset(cameraIndex: number): Promise<number | undefined>
    getAllUpOffset(): Promise<number[] | undefined>
}

export class Camera implements ICamera {
    index: number
    id?: number
    isPerspective?: number
    verticalExtent?: number
    horizontalExtent?: number
    farDistance?: number
    nearDistance?: number
    targetDistance?: number
    rightOffset?: number
    upOffset?: number
    
    static async createFromTable(table: ICameraTable, index: number): Promise<ICamera> {
        let result = new Camera()
        result.index = index
        
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
        ])
        
        return result
    }
}

export class CameraTable implements ICameraTable {
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<ICameraTable | undefined> {
        const entity = await document.entities.getBfast("Vim.Camera")
        
        if (!entity) {
            return undefined
        }
        
        let table = new CameraTable()
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(cameraIndex: number): Promise<ICamera> {
        return await Camera.createFromTable(this, cameraIndex)
    }
    
    async getAll(): Promise<ICamera[]> {
        const localTable = await this.entityTable.getLocal()
        
        let id: number[] | undefined
        let isPerspective: number[] | undefined
        let verticalExtent: number[] | undefined
        let horizontalExtent: number[] | undefined
        let farDistance: number[] | undefined
        let nearDistance: number[] | undefined
        let targetDistance: number[] | undefined
        let rightOffset: number[] | undefined
        let upOffset: number[] | undefined
        
        await Promise.all([
            (async () => { id = (await localTable.getNumberArray("int:Id")) })(),
            (async () => { isPerspective = (await localTable.getNumberArray("int:IsPerspective")) })(),
            (async () => { verticalExtent = (await localTable.getNumberArray("double:VerticalExtent")) })(),
            (async () => { horizontalExtent = (await localTable.getNumberArray("double:HorizontalExtent")) })(),
            (async () => { farDistance = (await localTable.getNumberArray("double:FarDistance")) })(),
            (async () => { nearDistance = (await localTable.getNumberArray("double:NearDistance")) })(),
            (async () => { targetDistance = (await localTable.getNumberArray("double:TargetDistance")) })(),
            (async () => { rightOffset = (await localTable.getNumberArray("double:RightOffset")) })(),
            (async () => { upOffset = (await localTable.getNumberArray("double:UpOffset")) })(),
        ])
        
        let camera: ICamera[] = []
        
        const rowCount = await this.getCount()
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
            })
        }
        
        return camera
    }
    
    async getId(cameraIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(cameraIndex, "int:Id"))
    }
    
    async getAllId(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Id"))
    }
    
    async getIsPerspective(cameraIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(cameraIndex, "int:IsPerspective"))
    }
    
    async getAllIsPerspective(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:IsPerspective"))
    }
    
    async getVerticalExtent(cameraIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(cameraIndex, "double:VerticalExtent"))
    }
    
    async getAllVerticalExtent(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:VerticalExtent"))
    }
    
    async getHorizontalExtent(cameraIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(cameraIndex, "double:HorizontalExtent"))
    }
    
    async getAllHorizontalExtent(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:HorizontalExtent"))
    }
    
    async getFarDistance(cameraIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(cameraIndex, "double:FarDistance"))
    }
    
    async getAllFarDistance(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:FarDistance"))
    }
    
    async getNearDistance(cameraIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(cameraIndex, "double:NearDistance"))
    }
    
    async getAllNearDistance(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:NearDistance"))
    }
    
    async getTargetDistance(cameraIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(cameraIndex, "double:TargetDistance"))
    }
    
    async getAllTargetDistance(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:TargetDistance"))
    }
    
    async getRightOffset(cameraIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(cameraIndex, "double:RightOffset"))
    }
    
    async getAllRightOffset(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:RightOffset"))
    }
    
    async getUpOffset(cameraIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(cameraIndex, "double:UpOffset"))
    }
    
    async getAllUpOffset(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:UpOffset"))
    }
    
}

export interface IMaterial {
    index: number
    name?: string
    materialCategory?: string
    color_X?: number
    color_Y?: number
    color_Z?: number
    colorUvScaling_X?: number
    colorUvScaling_Y?: number
    colorUvOffset_X?: number
    colorUvOffset_Y?: number
    normalUvScaling_X?: number
    normalUvScaling_Y?: number
    normalUvOffset_X?: number
    normalUvOffset_Y?: number
    normalAmount?: number
    glossiness?: number
    smoothness?: number
    transparency?: number
    graphicsRead?: boolean
    shadingColor_X?: number
    shadingColor_Y?: number
    shadingColor_Z?: number
    useRenderAppearanceForShading?: boolean
    surfaceForegroundPatternColor_X?: number
    surfaceForegroundPatternColor_Y?: number
    surfaceForegroundPatternColor_Z?: number
    surfaceBackgroundPatternColor_X?: number
    surfaceBackgroundPatternColor_Y?: number
    surfaceBackgroundPatternColor_Z?: number
    cutForegroundPatternColor_X?: number
    cutForegroundPatternColor_Y?: number
    cutForegroundPatternColor_Z?: number
    cutBackgroundPatternColor_X?: number
    cutBackgroundPatternColor_Y?: number
    cutBackgroundPatternColor_Z?: number
    
    colorTextureFileIndex?: number
    colorTextureFile?: IAsset
    normalTextureFileIndex?: number
    normalTextureFile?: IAsset
    surfaceForegroundPatternIndex?: number
    surfaceForegroundPattern?: IFillPattern
    surfaceBackgroundPatternIndex?: number
    surfaceBackgroundPattern?: IFillPattern
    cutForegroundPatternIndex?: number
    cutForegroundPattern?: IFillPattern
    cutBackgroundPatternIndex?: number
    cutBackgroundPattern?: IFillPattern
    elementIndex?: number
    element?: IElement
}

export interface IMaterialTable {
    getCount(): Promise<number>
    get(materialIndex: number): Promise<IMaterial>
    getAll(): Promise<IMaterial[]>
    
    getName(materialIndex: number): Promise<string | undefined>
    getAllName(): Promise<string[] | undefined>
    getMaterialCategory(materialIndex: number): Promise<string | undefined>
    getAllMaterialCategory(): Promise<string[] | undefined>
    getColor_X(materialIndex: number): Promise<number | undefined>
    getAllColor_X(): Promise<number[] | undefined>
    getColor_Y(materialIndex: number): Promise<number | undefined>
    getAllColor_Y(): Promise<number[] | undefined>
    getColor_Z(materialIndex: number): Promise<number | undefined>
    getAllColor_Z(): Promise<number[] | undefined>
    getColorUvScaling_X(materialIndex: number): Promise<number | undefined>
    getAllColorUvScaling_X(): Promise<number[] | undefined>
    getColorUvScaling_Y(materialIndex: number): Promise<number | undefined>
    getAllColorUvScaling_Y(): Promise<number[] | undefined>
    getColorUvOffset_X(materialIndex: number): Promise<number | undefined>
    getAllColorUvOffset_X(): Promise<number[] | undefined>
    getColorUvOffset_Y(materialIndex: number): Promise<number | undefined>
    getAllColorUvOffset_Y(): Promise<number[] | undefined>
    getNormalUvScaling_X(materialIndex: number): Promise<number | undefined>
    getAllNormalUvScaling_X(): Promise<number[] | undefined>
    getNormalUvScaling_Y(materialIndex: number): Promise<number | undefined>
    getAllNormalUvScaling_Y(): Promise<number[] | undefined>
    getNormalUvOffset_X(materialIndex: number): Promise<number | undefined>
    getAllNormalUvOffset_X(): Promise<number[] | undefined>
    getNormalUvOffset_Y(materialIndex: number): Promise<number | undefined>
    getAllNormalUvOffset_Y(): Promise<number[] | undefined>
    getNormalAmount(materialIndex: number): Promise<number | undefined>
    getAllNormalAmount(): Promise<number[] | undefined>
    getGlossiness(materialIndex: number): Promise<number | undefined>
    getAllGlossiness(): Promise<number[] | undefined>
    getSmoothness(materialIndex: number): Promise<number | undefined>
    getAllSmoothness(): Promise<number[] | undefined>
    getTransparency(materialIndex: number): Promise<number | undefined>
    getAllTransparency(): Promise<number[] | undefined>
    getGraphicsRead(materialIndex: number): Promise<boolean | undefined>
    getAllGraphicsRead(): Promise<boolean[] | undefined>
    getShadingColor_X(materialIndex: number): Promise<number | undefined>
    getAllShadingColor_X(): Promise<number[] | undefined>
    getShadingColor_Y(materialIndex: number): Promise<number | undefined>
    getAllShadingColor_Y(): Promise<number[] | undefined>
    getShadingColor_Z(materialIndex: number): Promise<number | undefined>
    getAllShadingColor_Z(): Promise<number[] | undefined>
    getUseRenderAppearanceForShading(materialIndex: number): Promise<boolean | undefined>
    getAllUseRenderAppearanceForShading(): Promise<boolean[] | undefined>
    getSurfaceForegroundPatternColor_X(materialIndex: number): Promise<number | undefined>
    getAllSurfaceForegroundPatternColor_X(): Promise<number[] | undefined>
    getSurfaceForegroundPatternColor_Y(materialIndex: number): Promise<number | undefined>
    getAllSurfaceForegroundPatternColor_Y(): Promise<number[] | undefined>
    getSurfaceForegroundPatternColor_Z(materialIndex: number): Promise<number | undefined>
    getAllSurfaceForegroundPatternColor_Z(): Promise<number[] | undefined>
    getSurfaceBackgroundPatternColor_X(materialIndex: number): Promise<number | undefined>
    getAllSurfaceBackgroundPatternColor_X(): Promise<number[] | undefined>
    getSurfaceBackgroundPatternColor_Y(materialIndex: number): Promise<number | undefined>
    getAllSurfaceBackgroundPatternColor_Y(): Promise<number[] | undefined>
    getSurfaceBackgroundPatternColor_Z(materialIndex: number): Promise<number | undefined>
    getAllSurfaceBackgroundPatternColor_Z(): Promise<number[] | undefined>
    getCutForegroundPatternColor_X(materialIndex: number): Promise<number | undefined>
    getAllCutForegroundPatternColor_X(): Promise<number[] | undefined>
    getCutForegroundPatternColor_Y(materialIndex: number): Promise<number | undefined>
    getAllCutForegroundPatternColor_Y(): Promise<number[] | undefined>
    getCutForegroundPatternColor_Z(materialIndex: number): Promise<number | undefined>
    getAllCutForegroundPatternColor_Z(): Promise<number[] | undefined>
    getCutBackgroundPatternColor_X(materialIndex: number): Promise<number | undefined>
    getAllCutBackgroundPatternColor_X(): Promise<number[] | undefined>
    getCutBackgroundPatternColor_Y(materialIndex: number): Promise<number | undefined>
    getAllCutBackgroundPatternColor_Y(): Promise<number[] | undefined>
    getCutBackgroundPatternColor_Z(materialIndex: number): Promise<number | undefined>
    getAllCutBackgroundPatternColor_Z(): Promise<number[] | undefined>
    
    getColorTextureFileIndex(materialIndex: number): Promise<number | undefined>
    getAllColorTextureFileIndex(): Promise<number[] | undefined>
    getColorTextureFile(materialIndex: number): Promise<IAsset | undefined>
    getNormalTextureFileIndex(materialIndex: number): Promise<number | undefined>
    getAllNormalTextureFileIndex(): Promise<number[] | undefined>
    getNormalTextureFile(materialIndex: number): Promise<IAsset | undefined>
    getSurfaceForegroundPatternIndex(materialIndex: number): Promise<number | undefined>
    getAllSurfaceForegroundPatternIndex(): Promise<number[] | undefined>
    getSurfaceForegroundPattern(materialIndex: number): Promise<IFillPattern | undefined>
    getSurfaceBackgroundPatternIndex(materialIndex: number): Promise<number | undefined>
    getAllSurfaceBackgroundPatternIndex(): Promise<number[] | undefined>
    getSurfaceBackgroundPattern(materialIndex: number): Promise<IFillPattern | undefined>
    getCutForegroundPatternIndex(materialIndex: number): Promise<number | undefined>
    getAllCutForegroundPatternIndex(): Promise<number[] | undefined>
    getCutForegroundPattern(materialIndex: number): Promise<IFillPattern | undefined>
    getCutBackgroundPatternIndex(materialIndex: number): Promise<number | undefined>
    getAllCutBackgroundPatternIndex(): Promise<number[] | undefined>
    getCutBackgroundPattern(materialIndex: number): Promise<IFillPattern | undefined>
    getElementIndex(materialIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(materialIndex: number): Promise<IElement | undefined>
}

export class Material implements IMaterial {
    index: number
    name?: string
    materialCategory?: string
    color_X?: number
    color_Y?: number
    color_Z?: number
    colorUvScaling_X?: number
    colorUvScaling_Y?: number
    colorUvOffset_X?: number
    colorUvOffset_Y?: number
    normalUvScaling_X?: number
    normalUvScaling_Y?: number
    normalUvOffset_X?: number
    normalUvOffset_Y?: number
    normalAmount?: number
    glossiness?: number
    smoothness?: number
    transparency?: number
    graphicsRead?: boolean
    shadingColor_X?: number
    shadingColor_Y?: number
    shadingColor_Z?: number
    useRenderAppearanceForShading?: boolean
    surfaceForegroundPatternColor_X?: number
    surfaceForegroundPatternColor_Y?: number
    surfaceForegroundPatternColor_Z?: number
    surfaceBackgroundPatternColor_X?: number
    surfaceBackgroundPatternColor_Y?: number
    surfaceBackgroundPatternColor_Z?: number
    cutForegroundPatternColor_X?: number
    cutForegroundPatternColor_Y?: number
    cutForegroundPatternColor_Z?: number
    cutBackgroundPatternColor_X?: number
    cutBackgroundPatternColor_Y?: number
    cutBackgroundPatternColor_Z?: number
    
    colorTextureFileIndex?: number
    colorTextureFile?: IAsset
    normalTextureFileIndex?: number
    normalTextureFile?: IAsset
    surfaceForegroundPatternIndex?: number
    surfaceForegroundPattern?: IFillPattern
    surfaceBackgroundPatternIndex?: number
    surfaceBackgroundPattern?: IFillPattern
    cutForegroundPatternIndex?: number
    cutForegroundPattern?: IFillPattern
    cutBackgroundPatternIndex?: number
    cutBackgroundPattern?: IFillPattern
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IMaterialTable, index: number): Promise<IMaterial> {
        let result = new Material()
        result.index = index
        
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
            table.getGraphicsRead(index).then(v => result.graphicsRead = v),
            table.getShadingColor_X(index).then(v => result.shadingColor_X = v),
            table.getShadingColor_Y(index).then(v => result.shadingColor_Y = v),
            table.getShadingColor_Z(index).then(v => result.shadingColor_Z = v),
            table.getUseRenderAppearanceForShading(index).then(v => result.useRenderAppearanceForShading = v),
            table.getSurfaceForegroundPatternColor_X(index).then(v => result.surfaceForegroundPatternColor_X = v),
            table.getSurfaceForegroundPatternColor_Y(index).then(v => result.surfaceForegroundPatternColor_Y = v),
            table.getSurfaceForegroundPatternColor_Z(index).then(v => result.surfaceForegroundPatternColor_Z = v),
            table.getSurfaceBackgroundPatternColor_X(index).then(v => result.surfaceBackgroundPatternColor_X = v),
            table.getSurfaceBackgroundPatternColor_Y(index).then(v => result.surfaceBackgroundPatternColor_Y = v),
            table.getSurfaceBackgroundPatternColor_Z(index).then(v => result.surfaceBackgroundPatternColor_Z = v),
            table.getCutForegroundPatternColor_X(index).then(v => result.cutForegroundPatternColor_X = v),
            table.getCutForegroundPatternColor_Y(index).then(v => result.cutForegroundPatternColor_Y = v),
            table.getCutForegroundPatternColor_Z(index).then(v => result.cutForegroundPatternColor_Z = v),
            table.getCutBackgroundPatternColor_X(index).then(v => result.cutBackgroundPatternColor_X = v),
            table.getCutBackgroundPatternColor_Y(index).then(v => result.cutBackgroundPatternColor_Y = v),
            table.getCutBackgroundPatternColor_Z(index).then(v => result.cutBackgroundPatternColor_Z = v),
            table.getColorTextureFileIndex(index).then(v => result.colorTextureFileIndex = v),
            table.getNormalTextureFileIndex(index).then(v => result.normalTextureFileIndex = v),
            table.getSurfaceForegroundPatternIndex(index).then(v => result.surfaceForegroundPatternIndex = v),
            table.getSurfaceBackgroundPatternIndex(index).then(v => result.surfaceBackgroundPatternIndex = v),
            table.getCutForegroundPatternIndex(index).then(v => result.cutForegroundPatternIndex = v),
            table.getCutBackgroundPatternIndex(index).then(v => result.cutBackgroundPatternIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class MaterialTable implements IMaterialTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IMaterialTable | undefined> {
        const entity = await document.entities.getBfast("Vim.Material")
        
        if (!entity) {
            return undefined
        }
        
        let table = new MaterialTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(materialIndex: number): Promise<IMaterial> {
        return await Material.createFromTable(this, materialIndex)
    }
    
    async getAll(): Promise<IMaterial[]> {
        const localTable = await this.entityTable.getLocal()
        
        let name: string[] | undefined
        let materialCategory: string[] | undefined
        let color_X: number[] | undefined
        let color_Y: number[] | undefined
        let color_Z: number[] | undefined
        let colorUvScaling_X: number[] | undefined
        let colorUvScaling_Y: number[] | undefined
        let colorUvOffset_X: number[] | undefined
        let colorUvOffset_Y: number[] | undefined
        let normalUvScaling_X: number[] | undefined
        let normalUvScaling_Y: number[] | undefined
        let normalUvOffset_X: number[] | undefined
        let normalUvOffset_Y: number[] | undefined
        let normalAmount: number[] | undefined
        let glossiness: number[] | undefined
        let smoothness: number[] | undefined
        let transparency: number[] | undefined
        let graphicsRead: boolean[] | undefined
        let shadingColor_X: number[] | undefined
        let shadingColor_Y: number[] | undefined
        let shadingColor_Z: number[] | undefined
        let useRenderAppearanceForShading: boolean[] | undefined
        let surfaceForegroundPatternColor_X: number[] | undefined
        let surfaceForegroundPatternColor_Y: number[] | undefined
        let surfaceForegroundPatternColor_Z: number[] | undefined
        let surfaceBackgroundPatternColor_X: number[] | undefined
        let surfaceBackgroundPatternColor_Y: number[] | undefined
        let surfaceBackgroundPatternColor_Z: number[] | undefined
        let cutForegroundPatternColor_X: number[] | undefined
        let cutForegroundPatternColor_Y: number[] | undefined
        let cutForegroundPatternColor_Z: number[] | undefined
        let cutBackgroundPatternColor_X: number[] | undefined
        let cutBackgroundPatternColor_Y: number[] | undefined
        let cutBackgroundPatternColor_Z: number[] | undefined
        let colorTextureFileIndex: number[] | undefined
        let normalTextureFileIndex: number[] | undefined
        let surfaceForegroundPatternIndex: number[] | undefined
        let surfaceBackgroundPatternIndex: number[] | undefined
        let cutForegroundPatternIndex: number[] | undefined
        let cutBackgroundPatternIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { name = (await localTable.getStringArray("string:Name")) })(),
            (async () => { materialCategory = (await localTable.getStringArray("string:MaterialCategory")) })(),
            (async () => { color_X = (await localTable.getNumberArray("double:Color.X")) })(),
            (async () => { color_Y = (await localTable.getNumberArray("double:Color.Y")) })(),
            (async () => { color_Z = (await localTable.getNumberArray("double:Color.Z")) })(),
            (async () => { colorUvScaling_X = (await localTable.getNumberArray("double:ColorUvScaling.X")) })(),
            (async () => { colorUvScaling_Y = (await localTable.getNumberArray("double:ColorUvScaling.Y")) })(),
            (async () => { colorUvOffset_X = (await localTable.getNumberArray("double:ColorUvOffset.X")) })(),
            (async () => { colorUvOffset_Y = (await localTable.getNumberArray("double:ColorUvOffset.Y")) })(),
            (async () => { normalUvScaling_X = (await localTable.getNumberArray("double:NormalUvScaling.X")) })(),
            (async () => { normalUvScaling_Y = (await localTable.getNumberArray("double:NormalUvScaling.Y")) })(),
            (async () => { normalUvOffset_X = (await localTable.getNumberArray("double:NormalUvOffset.X")) })(),
            (async () => { normalUvOffset_Y = (await localTable.getNumberArray("double:NormalUvOffset.Y")) })(),
            (async () => { normalAmount = (await localTable.getNumberArray("double:NormalAmount")) })(),
            (async () => { glossiness = (await localTable.getNumberArray("double:Glossiness")) })(),
            (async () => { smoothness = (await localTable.getNumberArray("double:Smoothness")) })(),
            (async () => { transparency = (await localTable.getNumberArray("double:Transparency")) })(),
            (async () => { graphicsRead = (await localTable.getBooleanArray("byte:GraphicsRead")) })(),
            (async () => { shadingColor_X = (await localTable.getNumberArray("double:ShadingColor.X")) })(),
            (async () => { shadingColor_Y = (await localTable.getNumberArray("double:ShadingColor.Y")) })(),
            (async () => { shadingColor_Z = (await localTable.getNumberArray("double:ShadingColor.Z")) })(),
            (async () => { useRenderAppearanceForShading = (await localTable.getBooleanArray("byte:UseRenderAppearanceForShading")) })(),
            (async () => { surfaceForegroundPatternColor_X = (await localTable.getNumberArray("double:SurfaceForegroundPatternColor.X")) })(),
            (async () => { surfaceForegroundPatternColor_Y = (await localTable.getNumberArray("double:SurfaceForegroundPatternColor.Y")) })(),
            (async () => { surfaceForegroundPatternColor_Z = (await localTable.getNumberArray("double:SurfaceForegroundPatternColor.Z")) })(),
            (async () => { surfaceBackgroundPatternColor_X = (await localTable.getNumberArray("double:SurfaceBackgroundPatternColor.X")) })(),
            (async () => { surfaceBackgroundPatternColor_Y = (await localTable.getNumberArray("double:SurfaceBackgroundPatternColor.Y")) })(),
            (async () => { surfaceBackgroundPatternColor_Z = (await localTable.getNumberArray("double:SurfaceBackgroundPatternColor.Z")) })(),
            (async () => { cutForegroundPatternColor_X = (await localTable.getNumberArray("double:CutForegroundPatternColor.X")) })(),
            (async () => { cutForegroundPatternColor_Y = (await localTable.getNumberArray("double:CutForegroundPatternColor.Y")) })(),
            (async () => { cutForegroundPatternColor_Z = (await localTable.getNumberArray("double:CutForegroundPatternColor.Z")) })(),
            (async () => { cutBackgroundPatternColor_X = (await localTable.getNumberArray("double:CutBackgroundPatternColor.X")) })(),
            (async () => { cutBackgroundPatternColor_Y = (await localTable.getNumberArray("double:CutBackgroundPatternColor.Y")) })(),
            (async () => { cutBackgroundPatternColor_Z = (await localTable.getNumberArray("double:CutBackgroundPatternColor.Z")) })(),
            (async () => { colorTextureFileIndex = (await localTable.getNumberArray("index:Vim.Asset:ColorTextureFile")) })(),
            (async () => { normalTextureFileIndex = (await localTable.getNumberArray("index:Vim.Asset:NormalTextureFile")) })(),
            (async () => { surfaceForegroundPatternIndex = (await localTable.getNumberArray("index:Vim.FillPattern:SurfaceForegroundPattern")) })(),
            (async () => { surfaceBackgroundPatternIndex = (await localTable.getNumberArray("index:Vim.FillPattern:SurfaceBackgroundPattern")) })(),
            (async () => { cutForegroundPatternIndex = (await localTable.getNumberArray("index:Vim.FillPattern:CutForegroundPattern")) })(),
            (async () => { cutBackgroundPatternIndex = (await localTable.getNumberArray("index:Vim.FillPattern:CutBackgroundPattern")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let material: IMaterial[] = []
        
        const rowCount = await this.getCount()
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
                graphicsRead: graphicsRead ? graphicsRead[i] : undefined,
                shadingColor_X: shadingColor_X ? shadingColor_X[i] : undefined,
                shadingColor_Y: shadingColor_Y ? shadingColor_Y[i] : undefined,
                shadingColor_Z: shadingColor_Z ? shadingColor_Z[i] : undefined,
                useRenderAppearanceForShading: useRenderAppearanceForShading ? useRenderAppearanceForShading[i] : undefined,
                surfaceForegroundPatternColor_X: surfaceForegroundPatternColor_X ? surfaceForegroundPatternColor_X[i] : undefined,
                surfaceForegroundPatternColor_Y: surfaceForegroundPatternColor_Y ? surfaceForegroundPatternColor_Y[i] : undefined,
                surfaceForegroundPatternColor_Z: surfaceForegroundPatternColor_Z ? surfaceForegroundPatternColor_Z[i] : undefined,
                surfaceBackgroundPatternColor_X: surfaceBackgroundPatternColor_X ? surfaceBackgroundPatternColor_X[i] : undefined,
                surfaceBackgroundPatternColor_Y: surfaceBackgroundPatternColor_Y ? surfaceBackgroundPatternColor_Y[i] : undefined,
                surfaceBackgroundPatternColor_Z: surfaceBackgroundPatternColor_Z ? surfaceBackgroundPatternColor_Z[i] : undefined,
                cutForegroundPatternColor_X: cutForegroundPatternColor_X ? cutForegroundPatternColor_X[i] : undefined,
                cutForegroundPatternColor_Y: cutForegroundPatternColor_Y ? cutForegroundPatternColor_Y[i] : undefined,
                cutForegroundPatternColor_Z: cutForegroundPatternColor_Z ? cutForegroundPatternColor_Z[i] : undefined,
                cutBackgroundPatternColor_X: cutBackgroundPatternColor_X ? cutBackgroundPatternColor_X[i] : undefined,
                cutBackgroundPatternColor_Y: cutBackgroundPatternColor_Y ? cutBackgroundPatternColor_Y[i] : undefined,
                cutBackgroundPatternColor_Z: cutBackgroundPatternColor_Z ? cutBackgroundPatternColor_Z[i] : undefined,
                colorTextureFileIndex: colorTextureFileIndex ? colorTextureFileIndex[i] : undefined,
                normalTextureFileIndex: normalTextureFileIndex ? normalTextureFileIndex[i] : undefined,
                surfaceForegroundPatternIndex: surfaceForegroundPatternIndex ? surfaceForegroundPatternIndex[i] : undefined,
                surfaceBackgroundPatternIndex: surfaceBackgroundPatternIndex ? surfaceBackgroundPatternIndex[i] : undefined,
                cutForegroundPatternIndex: cutForegroundPatternIndex ? cutForegroundPatternIndex[i] : undefined,
                cutBackgroundPatternIndex: cutBackgroundPatternIndex ? cutBackgroundPatternIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return material
    }
    
    async getName(materialIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(materialIndex, "string:Name"))
    }
    
    async getAllName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Name"))
    }
    
    async getMaterialCategory(materialIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(materialIndex, "string:MaterialCategory"))
    }
    
    async getAllMaterialCategory(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:MaterialCategory"))
    }
    
    async getColor_X(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:Color.X"))
    }
    
    async getAllColor_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Color.X"))
    }
    
    async getColor_Y(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:Color.Y"))
    }
    
    async getAllColor_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Color.Y"))
    }
    
    async getColor_Z(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:Color.Z"))
    }
    
    async getAllColor_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Color.Z"))
    }
    
    async getColorUvScaling_X(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:ColorUvScaling.X"))
    }
    
    async getAllColorUvScaling_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:ColorUvScaling.X"))
    }
    
    async getColorUvScaling_Y(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:ColorUvScaling.Y"))
    }
    
    async getAllColorUvScaling_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:ColorUvScaling.Y"))
    }
    
    async getColorUvOffset_X(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:ColorUvOffset.X"))
    }
    
    async getAllColorUvOffset_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:ColorUvOffset.X"))
    }
    
    async getColorUvOffset_Y(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:ColorUvOffset.Y"))
    }
    
    async getAllColorUvOffset_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:ColorUvOffset.Y"))
    }
    
    async getNormalUvScaling_X(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:NormalUvScaling.X"))
    }
    
    async getAllNormalUvScaling_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:NormalUvScaling.X"))
    }
    
    async getNormalUvScaling_Y(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:NormalUvScaling.Y"))
    }
    
    async getAllNormalUvScaling_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:NormalUvScaling.Y"))
    }
    
    async getNormalUvOffset_X(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:NormalUvOffset.X"))
    }
    
    async getAllNormalUvOffset_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:NormalUvOffset.X"))
    }
    
    async getNormalUvOffset_Y(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:NormalUvOffset.Y"))
    }
    
    async getAllNormalUvOffset_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:NormalUvOffset.Y"))
    }
    
    async getNormalAmount(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:NormalAmount"))
    }
    
    async getAllNormalAmount(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:NormalAmount"))
    }
    
    async getGlossiness(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:Glossiness"))
    }
    
    async getAllGlossiness(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Glossiness"))
    }
    
    async getSmoothness(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:Smoothness"))
    }
    
    async getAllSmoothness(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Smoothness"))
    }
    
    async getTransparency(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:Transparency"))
    }
    
    async getAllTransparency(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Transparency"))
    }
    
    async getGraphicsRead(materialIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(materialIndex, "byte:GraphicsRead"))
    }
    
    async getAllGraphicsRead(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:GraphicsRead"))
    }
    
    async getShadingColor_X(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:ShadingColor.X"))
    }
    
    async getAllShadingColor_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:ShadingColor.X"))
    }
    
    async getShadingColor_Y(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:ShadingColor.Y"))
    }
    
    async getAllShadingColor_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:ShadingColor.Y"))
    }
    
    async getShadingColor_Z(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:ShadingColor.Z"))
    }
    
    async getAllShadingColor_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:ShadingColor.Z"))
    }
    
    async getUseRenderAppearanceForShading(materialIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(materialIndex, "byte:UseRenderAppearanceForShading"))
    }
    
    async getAllUseRenderAppearanceForShading(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:UseRenderAppearanceForShading"))
    }
    
    async getSurfaceForegroundPatternColor_X(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:SurfaceForegroundPatternColor.X"))
    }
    
    async getAllSurfaceForegroundPatternColor_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:SurfaceForegroundPatternColor.X"))
    }
    
    async getSurfaceForegroundPatternColor_Y(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:SurfaceForegroundPatternColor.Y"))
    }
    
    async getAllSurfaceForegroundPatternColor_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:SurfaceForegroundPatternColor.Y"))
    }
    
    async getSurfaceForegroundPatternColor_Z(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:SurfaceForegroundPatternColor.Z"))
    }
    
    async getAllSurfaceForegroundPatternColor_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:SurfaceForegroundPatternColor.Z"))
    }
    
    async getSurfaceBackgroundPatternColor_X(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:SurfaceBackgroundPatternColor.X"))
    }
    
    async getAllSurfaceBackgroundPatternColor_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:SurfaceBackgroundPatternColor.X"))
    }
    
    async getSurfaceBackgroundPatternColor_Y(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:SurfaceBackgroundPatternColor.Y"))
    }
    
    async getAllSurfaceBackgroundPatternColor_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:SurfaceBackgroundPatternColor.Y"))
    }
    
    async getSurfaceBackgroundPatternColor_Z(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:SurfaceBackgroundPatternColor.Z"))
    }
    
    async getAllSurfaceBackgroundPatternColor_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:SurfaceBackgroundPatternColor.Z"))
    }
    
    async getCutForegroundPatternColor_X(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:CutForegroundPatternColor.X"))
    }
    
    async getAllCutForegroundPatternColor_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:CutForegroundPatternColor.X"))
    }
    
    async getCutForegroundPatternColor_Y(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:CutForegroundPatternColor.Y"))
    }
    
    async getAllCutForegroundPatternColor_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:CutForegroundPatternColor.Y"))
    }
    
    async getCutForegroundPatternColor_Z(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:CutForegroundPatternColor.Z"))
    }
    
    async getAllCutForegroundPatternColor_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:CutForegroundPatternColor.Z"))
    }
    
    async getCutBackgroundPatternColor_X(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:CutBackgroundPatternColor.X"))
    }
    
    async getAllCutBackgroundPatternColor_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:CutBackgroundPatternColor.X"))
    }
    
    async getCutBackgroundPatternColor_Y(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:CutBackgroundPatternColor.Y"))
    }
    
    async getAllCutBackgroundPatternColor_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:CutBackgroundPatternColor.Y"))
    }
    
    async getCutBackgroundPatternColor_Z(materialIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialIndex, "double:CutBackgroundPatternColor.Z"))
    }
    
    async getAllCutBackgroundPatternColor_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:CutBackgroundPatternColor.Z"))
    }
    
    async getColorTextureFileIndex(materialIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(materialIndex, "index:Vim.Asset:ColorTextureFile")
    }
    
    async getAllColorTextureFileIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Asset:ColorTextureFile")
    }
    
    async getColorTextureFile(materialIndex: number): Promise<IAsset | undefined> {
        const index = await this.getColorTextureFileIndex(materialIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.asset?.get(index)
    }
    
    async getNormalTextureFileIndex(materialIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(materialIndex, "index:Vim.Asset:NormalTextureFile")
    }
    
    async getAllNormalTextureFileIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Asset:NormalTextureFile")
    }
    
    async getNormalTextureFile(materialIndex: number): Promise<IAsset | undefined> {
        const index = await this.getNormalTextureFileIndex(materialIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.asset?.get(index)
    }
    
    async getSurfaceForegroundPatternIndex(materialIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(materialIndex, "index:Vim.FillPattern:SurfaceForegroundPattern")
    }
    
    async getAllSurfaceForegroundPatternIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.FillPattern:SurfaceForegroundPattern")
    }
    
    async getSurfaceForegroundPattern(materialIndex: number): Promise<IFillPattern | undefined> {
        const index = await this.getSurfaceForegroundPatternIndex(materialIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.fillPattern?.get(index)
    }
    
    async getSurfaceBackgroundPatternIndex(materialIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(materialIndex, "index:Vim.FillPattern:SurfaceBackgroundPattern")
    }
    
    async getAllSurfaceBackgroundPatternIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.FillPattern:SurfaceBackgroundPattern")
    }
    
    async getSurfaceBackgroundPattern(materialIndex: number): Promise<IFillPattern | undefined> {
        const index = await this.getSurfaceBackgroundPatternIndex(materialIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.fillPattern?.get(index)
    }
    
    async getCutForegroundPatternIndex(materialIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(materialIndex, "index:Vim.FillPattern:CutForegroundPattern")
    }
    
    async getAllCutForegroundPatternIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.FillPattern:CutForegroundPattern")
    }
    
    async getCutForegroundPattern(materialIndex: number): Promise<IFillPattern | undefined> {
        const index = await this.getCutForegroundPatternIndex(materialIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.fillPattern?.get(index)
    }
    
    async getCutBackgroundPatternIndex(materialIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(materialIndex, "index:Vim.FillPattern:CutBackgroundPattern")
    }
    
    async getAllCutBackgroundPatternIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.FillPattern:CutBackgroundPattern")
    }
    
    async getCutBackgroundPattern(materialIndex: number): Promise<IFillPattern | undefined> {
        const index = await this.getCutBackgroundPatternIndex(materialIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.fillPattern?.get(index)
    }
    
    async getElementIndex(materialIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(materialIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(materialIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(materialIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IMaterialInElement {
    index: number
    area?: number
    volume?: number
    isPaint?: boolean
    
    materialIndex?: number
    material?: IMaterial
    elementIndex?: number
    element?: IElement
}

export interface IMaterialInElementTable {
    getCount(): Promise<number>
    get(materialInElementIndex: number): Promise<IMaterialInElement>
    getAll(): Promise<IMaterialInElement[]>
    
    getArea(materialInElementIndex: number): Promise<number | undefined>
    getAllArea(): Promise<number[] | undefined>
    getVolume(materialInElementIndex: number): Promise<number | undefined>
    getAllVolume(): Promise<number[] | undefined>
    getIsPaint(materialInElementIndex: number): Promise<boolean | undefined>
    getAllIsPaint(): Promise<boolean[] | undefined>
    
    getMaterialIndex(materialInElementIndex: number): Promise<number | undefined>
    getAllMaterialIndex(): Promise<number[] | undefined>
    getMaterial(materialInElementIndex: number): Promise<IMaterial | undefined>
    getElementIndex(materialInElementIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(materialInElementIndex: number): Promise<IElement | undefined>
}

export class MaterialInElement implements IMaterialInElement {
    index: number
    area?: number
    volume?: number
    isPaint?: boolean
    
    materialIndex?: number
    material?: IMaterial
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IMaterialInElementTable, index: number): Promise<IMaterialInElement> {
        let result = new MaterialInElement()
        result.index = index
        
        await Promise.all([
            table.getArea(index).then(v => result.area = v),
            table.getVolume(index).then(v => result.volume = v),
            table.getIsPaint(index).then(v => result.isPaint = v),
            table.getMaterialIndex(index).then(v => result.materialIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class MaterialInElementTable implements IMaterialInElementTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IMaterialInElementTable | undefined> {
        const entity = await document.entities.getBfast("Vim.MaterialInElement")
        
        if (!entity) {
            return undefined
        }
        
        let table = new MaterialInElementTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(materialInElementIndex: number): Promise<IMaterialInElement> {
        return await MaterialInElement.createFromTable(this, materialInElementIndex)
    }
    
    async getAll(): Promise<IMaterialInElement[]> {
        const localTable = await this.entityTable.getLocal()
        
        let area: number[] | undefined
        let volume: number[] | undefined
        let isPaint: boolean[] | undefined
        let materialIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { area = (await localTable.getNumberArray("double:Area")) })(),
            (async () => { volume = (await localTable.getNumberArray("double:Volume")) })(),
            (async () => { isPaint = (await localTable.getBooleanArray("byte:IsPaint")) })(),
            (async () => { materialIndex = (await localTable.getNumberArray("index:Vim.Material:Material")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let materialInElement: IMaterialInElement[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            materialInElement.push({
                index: i,
                area: area ? area[i] : undefined,
                volume: volume ? volume[i] : undefined,
                isPaint: isPaint ? isPaint[i] : undefined,
                materialIndex: materialIndex ? materialIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return materialInElement
    }
    
    async getArea(materialInElementIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialInElementIndex, "double:Area"))
    }
    
    async getAllArea(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Area"))
    }
    
    async getVolume(materialInElementIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(materialInElementIndex, "double:Volume"))
    }
    
    async getAllVolume(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Volume"))
    }
    
    async getIsPaint(materialInElementIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(materialInElementIndex, "byte:IsPaint"))
    }
    
    async getAllIsPaint(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsPaint"))
    }
    
    async getMaterialIndex(materialInElementIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(materialInElementIndex, "index:Vim.Material:Material")
    }
    
    async getAllMaterialIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Material:Material")
    }
    
    async getMaterial(materialInElementIndex: number): Promise<IMaterial | undefined> {
        const index = await this.getMaterialIndex(materialInElementIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.material?.get(index)
    }
    
    async getElementIndex(materialInElementIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(materialInElementIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(materialInElementIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(materialInElementIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface ICompoundStructureLayer {
    index: number
    orderIndex?: number
    width?: number
    materialFunctionAssignment?: string
    
    materialIndex?: number
    material?: IMaterial
    compoundStructureIndex?: number
    compoundStructure?: ICompoundStructure
}

export interface ICompoundStructureLayerTable {
    getCount(): Promise<number>
    get(compoundStructureLayerIndex: number): Promise<ICompoundStructureLayer>
    getAll(): Promise<ICompoundStructureLayer[]>
    
    getOrderIndex(compoundStructureLayerIndex: number): Promise<number | undefined>
    getAllOrderIndex(): Promise<number[] | undefined>
    getWidth(compoundStructureLayerIndex: number): Promise<number | undefined>
    getAllWidth(): Promise<number[] | undefined>
    getMaterialFunctionAssignment(compoundStructureLayerIndex: number): Promise<string | undefined>
    getAllMaterialFunctionAssignment(): Promise<string[] | undefined>
    
    getMaterialIndex(compoundStructureLayerIndex: number): Promise<number | undefined>
    getAllMaterialIndex(): Promise<number[] | undefined>
    getMaterial(compoundStructureLayerIndex: number): Promise<IMaterial | undefined>
    getCompoundStructureIndex(compoundStructureLayerIndex: number): Promise<number | undefined>
    getAllCompoundStructureIndex(): Promise<number[] | undefined>
    getCompoundStructure(compoundStructureLayerIndex: number): Promise<ICompoundStructure | undefined>
}

export class CompoundStructureLayer implements ICompoundStructureLayer {
    index: number
    orderIndex?: number
    width?: number
    materialFunctionAssignment?: string
    
    materialIndex?: number
    material?: IMaterial
    compoundStructureIndex?: number
    compoundStructure?: ICompoundStructure
    
    static async createFromTable(table: ICompoundStructureLayerTable, index: number): Promise<ICompoundStructureLayer> {
        let result = new CompoundStructureLayer()
        result.index = index
        
        await Promise.all([
            table.getOrderIndex(index).then(v => result.orderIndex = v),
            table.getWidth(index).then(v => result.width = v),
            table.getMaterialFunctionAssignment(index).then(v => result.materialFunctionAssignment = v),
            table.getMaterialIndex(index).then(v => result.materialIndex = v),
            table.getCompoundStructureIndex(index).then(v => result.compoundStructureIndex = v),
        ])
        
        return result
    }
}

export class CompoundStructureLayerTable implements ICompoundStructureLayerTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<ICompoundStructureLayerTable | undefined> {
        const entity = await document.entities.getBfast("Vim.CompoundStructureLayer")
        
        if (!entity) {
            return undefined
        }
        
        let table = new CompoundStructureLayerTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(compoundStructureLayerIndex: number): Promise<ICompoundStructureLayer> {
        return await CompoundStructureLayer.createFromTable(this, compoundStructureLayerIndex)
    }
    
    async getAll(): Promise<ICompoundStructureLayer[]> {
        const localTable = await this.entityTable.getLocal()
        
        let orderIndex: number[] | undefined
        let width: number[] | undefined
        let materialFunctionAssignment: string[] | undefined
        let materialIndex: number[] | undefined
        let compoundStructureIndex: number[] | undefined
        
        await Promise.all([
            (async () => { orderIndex = (await localTable.getNumberArray("int:OrderIndex")) })(),
            (async () => { width = (await localTable.getNumberArray("double:Width")) })(),
            (async () => { materialFunctionAssignment = (await localTable.getStringArray("string:MaterialFunctionAssignment")) })(),
            (async () => { materialIndex = (await localTable.getNumberArray("index:Vim.Material:Material")) })(),
            (async () => { compoundStructureIndex = (await localTable.getNumberArray("index:Vim.CompoundStructure:CompoundStructure")) })(),
        ])
        
        let compoundStructureLayer: ICompoundStructureLayer[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            compoundStructureLayer.push({
                index: i,
                orderIndex: orderIndex ? orderIndex[i] : undefined,
                width: width ? width[i] : undefined,
                materialFunctionAssignment: materialFunctionAssignment ? materialFunctionAssignment[i] : undefined,
                materialIndex: materialIndex ? materialIndex[i] : undefined,
                compoundStructureIndex: compoundStructureIndex ? compoundStructureIndex[i] : undefined
            })
        }
        
        return compoundStructureLayer
    }
    
    async getOrderIndex(compoundStructureLayerIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(compoundStructureLayerIndex, "int:OrderIndex"))
    }
    
    async getAllOrderIndex(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:OrderIndex"))
    }
    
    async getWidth(compoundStructureLayerIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(compoundStructureLayerIndex, "double:Width"))
    }
    
    async getAllWidth(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Width"))
    }
    
    async getMaterialFunctionAssignment(compoundStructureLayerIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(compoundStructureLayerIndex, "string:MaterialFunctionAssignment"))
    }
    
    async getAllMaterialFunctionAssignment(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:MaterialFunctionAssignment"))
    }
    
    async getMaterialIndex(compoundStructureLayerIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(compoundStructureLayerIndex, "index:Vim.Material:Material")
    }
    
    async getAllMaterialIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Material:Material")
    }
    
    async getMaterial(compoundStructureLayerIndex: number): Promise<IMaterial | undefined> {
        const index = await this.getMaterialIndex(compoundStructureLayerIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.material?.get(index)
    }
    
    async getCompoundStructureIndex(compoundStructureLayerIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(compoundStructureLayerIndex, "index:Vim.CompoundStructure:CompoundStructure")
    }
    
    async getAllCompoundStructureIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.CompoundStructure:CompoundStructure")
    }
    
    async getCompoundStructure(compoundStructureLayerIndex: number): Promise<ICompoundStructure | undefined> {
        const index = await this.getCompoundStructureIndex(compoundStructureLayerIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.compoundStructure?.get(index)
    }
    
}

export interface ICompoundStructure {
    index: number
    width?: number
    
    structuralLayerIndex?: number
    structuralLayer?: ICompoundStructureLayer
}

export interface ICompoundStructureTable {
    getCount(): Promise<number>
    get(compoundStructureIndex: number): Promise<ICompoundStructure>
    getAll(): Promise<ICompoundStructure[]>
    
    getWidth(compoundStructureIndex: number): Promise<number | undefined>
    getAllWidth(): Promise<number[] | undefined>
    
    getStructuralLayerIndex(compoundStructureIndex: number): Promise<number | undefined>
    getAllStructuralLayerIndex(): Promise<number[] | undefined>
    getStructuralLayer(compoundStructureIndex: number): Promise<ICompoundStructureLayer | undefined>
}

export class CompoundStructure implements ICompoundStructure {
    index: number
    width?: number
    
    structuralLayerIndex?: number
    structuralLayer?: ICompoundStructureLayer
    
    static async createFromTable(table: ICompoundStructureTable, index: number): Promise<ICompoundStructure> {
        let result = new CompoundStructure()
        result.index = index
        
        await Promise.all([
            table.getWidth(index).then(v => result.width = v),
            table.getStructuralLayerIndex(index).then(v => result.structuralLayerIndex = v),
        ])
        
        return result
    }
}

export class CompoundStructureTable implements ICompoundStructureTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<ICompoundStructureTable | undefined> {
        const entity = await document.entities.getBfast("Vim.CompoundStructure")
        
        if (!entity) {
            return undefined
        }
        
        let table = new CompoundStructureTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(compoundStructureIndex: number): Promise<ICompoundStructure> {
        return await CompoundStructure.createFromTable(this, compoundStructureIndex)
    }
    
    async getAll(): Promise<ICompoundStructure[]> {
        const localTable = await this.entityTable.getLocal()
        
        let width: number[] | undefined
        let structuralLayerIndex: number[] | undefined
        
        await Promise.all([
            (async () => { width = (await localTable.getNumberArray("double:Width")) })(),
            (async () => { structuralLayerIndex = (await localTable.getNumberArray("index:Vim.CompoundStructureLayer:StructuralLayer")) })(),
        ])
        
        let compoundStructure: ICompoundStructure[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            compoundStructure.push({
                index: i,
                width: width ? width[i] : undefined,
                structuralLayerIndex: structuralLayerIndex ? structuralLayerIndex[i] : undefined
            })
        }
        
        return compoundStructure
    }
    
    async getWidth(compoundStructureIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(compoundStructureIndex, "double:Width"))
    }
    
    async getAllWidth(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Width"))
    }
    
    async getStructuralLayerIndex(compoundStructureIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(compoundStructureIndex, "index:Vim.CompoundStructureLayer:StructuralLayer")
    }
    
    async getAllStructuralLayerIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.CompoundStructureLayer:StructuralLayer")
    }
    
    async getStructuralLayer(compoundStructureIndex: number): Promise<ICompoundStructureLayer | undefined> {
        const index = await this.getStructuralLayerIndex(compoundStructureIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.compoundStructureLayer?.get(index)
    }
    
}

export interface INode {
    index: number
    
    elementIndex?: number
    element?: IElement
}

export interface INodeTable {
    getCount(): Promise<number>
    get(nodeIndex: number): Promise<INode>
    getAll(): Promise<INode[]>
    
    getElementIndex(nodeIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(nodeIndex: number): Promise<IElement | undefined>
}

export class Node implements INode {
    index: number
    
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: INodeTable, index: number): Promise<INode> {
        let result = new Node()
        result.index = index
        
        await Promise.all([
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class NodeTable implements INodeTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<INodeTable | undefined> {
        const entity = await document.entities.getBfast("Vim.Node")
        
        if (!entity) {
            return undefined
        }
        
        let table = new NodeTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(nodeIndex: number): Promise<INode> {
        return await Node.createFromTable(this, nodeIndex)
    }
    
    async getAll(): Promise<INode[]> {
        const localTable = await this.entityTable.getLocal()
        
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let node: INode[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            node.push({
                index: i,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return node
    }
    
    async getElementIndex(nodeIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(nodeIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(nodeIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(nodeIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IGeometryPiece {
    index: number
    submeshIndex?: number
    
    nodeIndex?: number
    node?: INode
    materialIndex?: number
    material?: IMaterial
    subcategoryIndex?: number
    subcategory?: ICategory
    compoundStructureLayerIndex?: number
    compoundStructureLayer?: ICompoundStructureLayer
    elementIndex?: number
    element?: IElement
}

export interface IGeometryPieceTable {
    getCount(): Promise<number>
    get(geometryPieceIndex: number): Promise<IGeometryPiece>
    getAll(): Promise<IGeometryPiece[]>
    
    getSubmeshIndex(geometryPieceIndex: number): Promise<number | undefined>
    getAllSubmeshIndex(): Promise<number[] | undefined>
    
    getNodeIndex(geometryPieceIndex: number): Promise<number | undefined>
    getAllNodeIndex(): Promise<number[] | undefined>
    getNode(geometryPieceIndex: number): Promise<INode | undefined>
    getMaterialIndex(geometryPieceIndex: number): Promise<number | undefined>
    getAllMaterialIndex(): Promise<number[] | undefined>
    getMaterial(geometryPieceIndex: number): Promise<IMaterial | undefined>
    getSubcategoryIndex(geometryPieceIndex: number): Promise<number | undefined>
    getAllSubcategoryIndex(): Promise<number[] | undefined>
    getSubcategory(geometryPieceIndex: number): Promise<ICategory | undefined>
    getCompoundStructureLayerIndex(geometryPieceIndex: number): Promise<number | undefined>
    getAllCompoundStructureLayerIndex(): Promise<number[] | undefined>
    getCompoundStructureLayer(geometryPieceIndex: number): Promise<ICompoundStructureLayer | undefined>
    getElementIndex(geometryPieceIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(geometryPieceIndex: number): Promise<IElement | undefined>
}

export class GeometryPiece implements IGeometryPiece {
    index: number
    submeshIndex?: number
    
    nodeIndex?: number
    node?: INode
    materialIndex?: number
    material?: IMaterial
    subcategoryIndex?: number
    subcategory?: ICategory
    compoundStructureLayerIndex?: number
    compoundStructureLayer?: ICompoundStructureLayer
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IGeometryPieceTable, index: number): Promise<IGeometryPiece> {
        let result = new GeometryPiece()
        result.index = index
        
        await Promise.all([
            table.getSubmeshIndex(index).then(v => result.submeshIndex = v),
            table.getNodeIndex(index).then(v => result.nodeIndex = v),
            table.getMaterialIndex(index).then(v => result.materialIndex = v),
            table.getSubcategoryIndex(index).then(v => result.subcategoryIndex = v),
            table.getCompoundStructureLayerIndex(index).then(v => result.compoundStructureLayerIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class GeometryPieceTable implements IGeometryPieceTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IGeometryPieceTable | undefined> {
        const entity = await document.entities.getBfast("Vim.GeometryPiece")
        
        if (!entity) {
            return undefined
        }
        
        let table = new GeometryPieceTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(geometryPieceIndex: number): Promise<IGeometryPiece> {
        return await GeometryPiece.createFromTable(this, geometryPieceIndex)
    }
    
    async getAll(): Promise<IGeometryPiece[]> {
        const localTable = await this.entityTable.getLocal()
        
        let submeshIndex: number[] | undefined
        let nodeIndex: number[] | undefined
        let materialIndex: number[] | undefined
        let subcategoryIndex: number[] | undefined
        let compoundStructureLayerIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { submeshIndex = (await localTable.getNumberArray("int:SubmeshIndex")) })(),
            (async () => { nodeIndex = (await localTable.getNumberArray("index:Vim.Node:Node")) })(),
            (async () => { materialIndex = (await localTable.getNumberArray("index:Vim.Material:Material")) })(),
            (async () => { subcategoryIndex = (await localTable.getNumberArray("index:Vim.Category:Subcategory")) })(),
            (async () => { compoundStructureLayerIndex = (await localTable.getNumberArray("index:Vim.CompoundStructureLayer:CompoundStructureLayer")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let geometryPiece: IGeometryPiece[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            geometryPiece.push({
                index: i,
                submeshIndex: submeshIndex ? submeshIndex[i] : undefined,
                nodeIndex: nodeIndex ? nodeIndex[i] : undefined,
                materialIndex: materialIndex ? materialIndex[i] : undefined,
                subcategoryIndex: subcategoryIndex ? subcategoryIndex[i] : undefined,
                compoundStructureLayerIndex: compoundStructureLayerIndex ? compoundStructureLayerIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return geometryPiece
    }
    
    async getSubmeshIndex(geometryPieceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(geometryPieceIndex, "int:SubmeshIndex"))
    }
    
    async getAllSubmeshIndex(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:SubmeshIndex"))
    }
    
    async getNodeIndex(geometryPieceIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(geometryPieceIndex, "index:Vim.Node:Node")
    }
    
    async getAllNodeIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Node:Node")
    }
    
    async getNode(geometryPieceIndex: number): Promise<INode | undefined> {
        const index = await this.getNodeIndex(geometryPieceIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.node?.get(index)
    }
    
    async getMaterialIndex(geometryPieceIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(geometryPieceIndex, "index:Vim.Material:Material")
    }
    
    async getAllMaterialIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Material:Material")
    }
    
    async getMaterial(geometryPieceIndex: number): Promise<IMaterial | undefined> {
        const index = await this.getMaterialIndex(geometryPieceIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.material?.get(index)
    }
    
    async getSubcategoryIndex(geometryPieceIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(geometryPieceIndex, "index:Vim.Category:Subcategory")
    }
    
    async getAllSubcategoryIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Category:Subcategory")
    }
    
    async getSubcategory(geometryPieceIndex: number): Promise<ICategory | undefined> {
        const index = await this.getSubcategoryIndex(geometryPieceIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.category?.get(index)
    }
    
    async getCompoundStructureLayerIndex(geometryPieceIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(geometryPieceIndex, "index:Vim.CompoundStructureLayer:CompoundStructureLayer")
    }
    
    async getAllCompoundStructureLayerIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.CompoundStructureLayer:CompoundStructureLayer")
    }
    
    async getCompoundStructureLayer(geometryPieceIndex: number): Promise<ICompoundStructureLayer | undefined> {
        const index = await this.getCompoundStructureLayerIndex(geometryPieceIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.compoundStructureLayer?.get(index)
    }
    
    async getElementIndex(geometryPieceIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(geometryPieceIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(geometryPieceIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(geometryPieceIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IHostPath {
    index: number
    curveKind?: number
    p0_X?: number
    p0_Y?: number
    p0_Z?: number
    p1_X?: number
    p1_Y?: number
    p1_Z?: number
    mid_X?: number
    mid_Y?: number
    mid_Z?: number
    center_X?: number
    center_Y?: number
    center_Z?: number
    radius?: number
    normal_X?: number
    normal_Y?: number
    normal_Z?: number
    length?: number
    width?: number
    orientation_X?: number
    orientation_Y?: number
    orientation_Z?: number
    flipped?: boolean
    locationLine?: number
    locationLineOffset?: number
    coreOffsetExterior?: number
    baseOffset?: number
    baseElevation?: number
    topOffset?: number
    height?: number
    hasEditedProfile?: boolean
    topAttachedCount?: number
    baseAttachedCount?: number
    isStacked?: boolean
    isCurtain?: boolean
    
    baseLevelIndex?: number
    baseLevel?: ILevel
    topLevelIndex?: number
    topLevel?: ILevel
    compoundStructureIndex?: number
    compoundStructure?: ICompoundStructure
    elementIndex?: number
    element?: IElement
}

export interface IHostPathTable {
    getCount(): Promise<number>
    get(hostPathIndex: number): Promise<IHostPath>
    getAll(): Promise<IHostPath[]>
    
    getCurveKind(hostPathIndex: number): Promise<number | undefined>
    getAllCurveKind(): Promise<number[] | undefined>
    getP0_X(hostPathIndex: number): Promise<number | undefined>
    getAllP0_X(): Promise<number[] | undefined>
    getP0_Y(hostPathIndex: number): Promise<number | undefined>
    getAllP0_Y(): Promise<number[] | undefined>
    getP0_Z(hostPathIndex: number): Promise<number | undefined>
    getAllP0_Z(): Promise<number[] | undefined>
    getP1_X(hostPathIndex: number): Promise<number | undefined>
    getAllP1_X(): Promise<number[] | undefined>
    getP1_Y(hostPathIndex: number): Promise<number | undefined>
    getAllP1_Y(): Promise<number[] | undefined>
    getP1_Z(hostPathIndex: number): Promise<number | undefined>
    getAllP1_Z(): Promise<number[] | undefined>
    getMid_X(hostPathIndex: number): Promise<number | undefined>
    getAllMid_X(): Promise<number[] | undefined>
    getMid_Y(hostPathIndex: number): Promise<number | undefined>
    getAllMid_Y(): Promise<number[] | undefined>
    getMid_Z(hostPathIndex: number): Promise<number | undefined>
    getAllMid_Z(): Promise<number[] | undefined>
    getCenter_X(hostPathIndex: number): Promise<number | undefined>
    getAllCenter_X(): Promise<number[] | undefined>
    getCenter_Y(hostPathIndex: number): Promise<number | undefined>
    getAllCenter_Y(): Promise<number[] | undefined>
    getCenter_Z(hostPathIndex: number): Promise<number | undefined>
    getAllCenter_Z(): Promise<number[] | undefined>
    getRadius(hostPathIndex: number): Promise<number | undefined>
    getAllRadius(): Promise<number[] | undefined>
    getNormal_X(hostPathIndex: number): Promise<number | undefined>
    getAllNormal_X(): Promise<number[] | undefined>
    getNormal_Y(hostPathIndex: number): Promise<number | undefined>
    getAllNormal_Y(): Promise<number[] | undefined>
    getNormal_Z(hostPathIndex: number): Promise<number | undefined>
    getAllNormal_Z(): Promise<number[] | undefined>
    getLength(hostPathIndex: number): Promise<number | undefined>
    getAllLength(): Promise<number[] | undefined>
    getWidth(hostPathIndex: number): Promise<number | undefined>
    getAllWidth(): Promise<number[] | undefined>
    getOrientation_X(hostPathIndex: number): Promise<number | undefined>
    getAllOrientation_X(): Promise<number[] | undefined>
    getOrientation_Y(hostPathIndex: number): Promise<number | undefined>
    getAllOrientation_Y(): Promise<number[] | undefined>
    getOrientation_Z(hostPathIndex: number): Promise<number | undefined>
    getAllOrientation_Z(): Promise<number[] | undefined>
    getFlipped(hostPathIndex: number): Promise<boolean | undefined>
    getAllFlipped(): Promise<boolean[] | undefined>
    getLocationLine(hostPathIndex: number): Promise<number | undefined>
    getAllLocationLine(): Promise<number[] | undefined>
    getLocationLineOffset(hostPathIndex: number): Promise<number | undefined>
    getAllLocationLineOffset(): Promise<number[] | undefined>
    getCoreOffsetExterior(hostPathIndex: number): Promise<number | undefined>
    getAllCoreOffsetExterior(): Promise<number[] | undefined>
    getBaseOffset(hostPathIndex: number): Promise<number | undefined>
    getAllBaseOffset(): Promise<number[] | undefined>
    getBaseElevation(hostPathIndex: number): Promise<number | undefined>
    getAllBaseElevation(): Promise<number[] | undefined>
    getTopOffset(hostPathIndex: number): Promise<number | undefined>
    getAllTopOffset(): Promise<number[] | undefined>
    getHeight(hostPathIndex: number): Promise<number | undefined>
    getAllHeight(): Promise<number[] | undefined>
    getHasEditedProfile(hostPathIndex: number): Promise<boolean | undefined>
    getAllHasEditedProfile(): Promise<boolean[] | undefined>
    getTopAttachedCount(hostPathIndex: number): Promise<number | undefined>
    getAllTopAttachedCount(): Promise<number[] | undefined>
    getBaseAttachedCount(hostPathIndex: number): Promise<number | undefined>
    getAllBaseAttachedCount(): Promise<number[] | undefined>
    getIsStacked(hostPathIndex: number): Promise<boolean | undefined>
    getAllIsStacked(): Promise<boolean[] | undefined>
    getIsCurtain(hostPathIndex: number): Promise<boolean | undefined>
    getAllIsCurtain(): Promise<boolean[] | undefined>
    
    getBaseLevelIndex(hostPathIndex: number): Promise<number | undefined>
    getAllBaseLevelIndex(): Promise<number[] | undefined>
    getBaseLevel(hostPathIndex: number): Promise<ILevel | undefined>
    getTopLevelIndex(hostPathIndex: number): Promise<number | undefined>
    getAllTopLevelIndex(): Promise<number[] | undefined>
    getTopLevel(hostPathIndex: number): Promise<ILevel | undefined>
    getCompoundStructureIndex(hostPathIndex: number): Promise<number | undefined>
    getAllCompoundStructureIndex(): Promise<number[] | undefined>
    getCompoundStructure(hostPathIndex: number): Promise<ICompoundStructure | undefined>
    getElementIndex(hostPathIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(hostPathIndex: number): Promise<IElement | undefined>
}

export class HostPath implements IHostPath {
    index: number
    curveKind?: number
    p0_X?: number
    p0_Y?: number
    p0_Z?: number
    p1_X?: number
    p1_Y?: number
    p1_Z?: number
    mid_X?: number
    mid_Y?: number
    mid_Z?: number
    center_X?: number
    center_Y?: number
    center_Z?: number
    radius?: number
    normal_X?: number
    normal_Y?: number
    normal_Z?: number
    length?: number
    width?: number
    orientation_X?: number
    orientation_Y?: number
    orientation_Z?: number
    flipped?: boolean
    locationLine?: number
    locationLineOffset?: number
    coreOffsetExterior?: number
    baseOffset?: number
    baseElevation?: number
    topOffset?: number
    height?: number
    hasEditedProfile?: boolean
    topAttachedCount?: number
    baseAttachedCount?: number
    isStacked?: boolean
    isCurtain?: boolean
    
    baseLevelIndex?: number
    baseLevel?: ILevel
    topLevelIndex?: number
    topLevel?: ILevel
    compoundStructureIndex?: number
    compoundStructure?: ICompoundStructure
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IHostPathTable, index: number): Promise<IHostPath> {
        let result = new HostPath()
        result.index = index
        
        await Promise.all([
            table.getCurveKind(index).then(v => result.curveKind = v),
            table.getP0_X(index).then(v => result.p0_X = v),
            table.getP0_Y(index).then(v => result.p0_Y = v),
            table.getP0_Z(index).then(v => result.p0_Z = v),
            table.getP1_X(index).then(v => result.p1_X = v),
            table.getP1_Y(index).then(v => result.p1_Y = v),
            table.getP1_Z(index).then(v => result.p1_Z = v),
            table.getMid_X(index).then(v => result.mid_X = v),
            table.getMid_Y(index).then(v => result.mid_Y = v),
            table.getMid_Z(index).then(v => result.mid_Z = v),
            table.getCenter_X(index).then(v => result.center_X = v),
            table.getCenter_Y(index).then(v => result.center_Y = v),
            table.getCenter_Z(index).then(v => result.center_Z = v),
            table.getRadius(index).then(v => result.radius = v),
            table.getNormal_X(index).then(v => result.normal_X = v),
            table.getNormal_Y(index).then(v => result.normal_Y = v),
            table.getNormal_Z(index).then(v => result.normal_Z = v),
            table.getLength(index).then(v => result.length = v),
            table.getWidth(index).then(v => result.width = v),
            table.getOrientation_X(index).then(v => result.orientation_X = v),
            table.getOrientation_Y(index).then(v => result.orientation_Y = v),
            table.getOrientation_Z(index).then(v => result.orientation_Z = v),
            table.getFlipped(index).then(v => result.flipped = v),
            table.getLocationLine(index).then(v => result.locationLine = v),
            table.getLocationLineOffset(index).then(v => result.locationLineOffset = v),
            table.getCoreOffsetExterior(index).then(v => result.coreOffsetExterior = v),
            table.getBaseOffset(index).then(v => result.baseOffset = v),
            table.getBaseElevation(index).then(v => result.baseElevation = v),
            table.getTopOffset(index).then(v => result.topOffset = v),
            table.getHeight(index).then(v => result.height = v),
            table.getHasEditedProfile(index).then(v => result.hasEditedProfile = v),
            table.getTopAttachedCount(index).then(v => result.topAttachedCount = v),
            table.getBaseAttachedCount(index).then(v => result.baseAttachedCount = v),
            table.getIsStacked(index).then(v => result.isStacked = v),
            table.getIsCurtain(index).then(v => result.isCurtain = v),
            table.getBaseLevelIndex(index).then(v => result.baseLevelIndex = v),
            table.getTopLevelIndex(index).then(v => result.topLevelIndex = v),
            table.getCompoundStructureIndex(index).then(v => result.compoundStructureIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class HostPathTable implements IHostPathTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IHostPathTable | undefined> {
        const entity = await document.entities.getBfast("Vim.HostPath")
        
        if (!entity) {
            return undefined
        }
        
        let table = new HostPathTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(hostPathIndex: number): Promise<IHostPath> {
        return await HostPath.createFromTable(this, hostPathIndex)
    }
    
    async getAll(): Promise<IHostPath[]> {
        const localTable = await this.entityTable.getLocal()
        
        let curveKind: number[] | undefined
        let p0_X: number[] | undefined
        let p0_Y: number[] | undefined
        let p0_Z: number[] | undefined
        let p1_X: number[] | undefined
        let p1_Y: number[] | undefined
        let p1_Z: number[] | undefined
        let mid_X: number[] | undefined
        let mid_Y: number[] | undefined
        let mid_Z: number[] | undefined
        let center_X: number[] | undefined
        let center_Y: number[] | undefined
        let center_Z: number[] | undefined
        let radius: number[] | undefined
        let normal_X: number[] | undefined
        let normal_Y: number[] | undefined
        let normal_Z: number[] | undefined
        let length: number[] | undefined
        let width: number[] | undefined
        let orientation_X: number[] | undefined
        let orientation_Y: number[] | undefined
        let orientation_Z: number[] | undefined
        let flipped: boolean[] | undefined
        let locationLine: number[] | undefined
        let locationLineOffset: number[] | undefined
        let coreOffsetExterior: number[] | undefined
        let baseOffset: number[] | undefined
        let baseElevation: number[] | undefined
        let topOffset: number[] | undefined
        let height: number[] | undefined
        let hasEditedProfile: boolean[] | undefined
        let topAttachedCount: number[] | undefined
        let baseAttachedCount: number[] | undefined
        let isStacked: boolean[] | undefined
        let isCurtain: boolean[] | undefined
        let baseLevelIndex: number[] | undefined
        let topLevelIndex: number[] | undefined
        let compoundStructureIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { curveKind = (await localTable.getNumberArray("int:CurveKind")) })(),
            (async () => { p0_X = (await localTable.getNumberArray("double:P0.X")) })(),
            (async () => { p0_Y = (await localTable.getNumberArray("double:P0.Y")) })(),
            (async () => { p0_Z = (await localTable.getNumberArray("double:P0.Z")) })(),
            (async () => { p1_X = (await localTable.getNumberArray("double:P1.X")) })(),
            (async () => { p1_Y = (await localTable.getNumberArray("double:P1.Y")) })(),
            (async () => { p1_Z = (await localTable.getNumberArray("double:P1.Z")) })(),
            (async () => { mid_X = (await localTable.getNumberArray("double:Mid.X")) })(),
            (async () => { mid_Y = (await localTable.getNumberArray("double:Mid.Y")) })(),
            (async () => { mid_Z = (await localTable.getNumberArray("double:Mid.Z")) })(),
            (async () => { center_X = (await localTable.getNumberArray("double:Center.X")) })(),
            (async () => { center_Y = (await localTable.getNumberArray("double:Center.Y")) })(),
            (async () => { center_Z = (await localTable.getNumberArray("double:Center.Z")) })(),
            (async () => { radius = (await localTable.getNumberArray("double:Radius")) })(),
            (async () => { normal_X = (await localTable.getNumberArray("double:Normal.X")) })(),
            (async () => { normal_Y = (await localTable.getNumberArray("double:Normal.Y")) })(),
            (async () => { normal_Z = (await localTable.getNumberArray("double:Normal.Z")) })(),
            (async () => { length = (await localTable.getNumberArray("double:Length")) })(),
            (async () => { width = (await localTable.getNumberArray("double:Width")) })(),
            (async () => { orientation_X = (await localTable.getNumberArray("double:Orientation.X")) })(),
            (async () => { orientation_Y = (await localTable.getNumberArray("double:Orientation.Y")) })(),
            (async () => { orientation_Z = (await localTable.getNumberArray("double:Orientation.Z")) })(),
            (async () => { flipped = (await localTable.getBooleanArray("byte:Flipped")) })(),
            (async () => { locationLine = (await localTable.getNumberArray("int:LocationLine")) })(),
            (async () => { locationLineOffset = (await localTable.getNumberArray("double:LocationLineOffset")) })(),
            (async () => { coreOffsetExterior = (await localTable.getNumberArray("double:CoreOffsetExterior")) })(),
            (async () => { baseOffset = (await localTable.getNumberArray("double:BaseOffset")) })(),
            (async () => { baseElevation = (await localTable.getNumberArray("double:BaseElevation")) })(),
            (async () => { topOffset = (await localTable.getNumberArray("double:TopOffset")) })(),
            (async () => { height = (await localTable.getNumberArray("double:Height")) })(),
            (async () => { hasEditedProfile = (await localTable.getBooleanArray("byte:HasEditedProfile")) })(),
            (async () => { topAttachedCount = (await localTable.getNumberArray("int:TopAttachedCount")) })(),
            (async () => { baseAttachedCount = (await localTable.getNumberArray("int:BaseAttachedCount")) })(),
            (async () => { isStacked = (await localTable.getBooleanArray("byte:IsStacked")) })(),
            (async () => { isCurtain = (await localTable.getBooleanArray("byte:IsCurtain")) })(),
            (async () => { baseLevelIndex = (await localTable.getNumberArray("index:Vim.Level:BaseLevel")) })(),
            (async () => { topLevelIndex = (await localTable.getNumberArray("index:Vim.Level:TopLevel")) })(),
            (async () => { compoundStructureIndex = (await localTable.getNumberArray("index:Vim.CompoundStructure:CompoundStructure")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let hostPath: IHostPath[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            hostPath.push({
                index: i,
                curveKind: curveKind ? curveKind[i] : undefined,
                p0_X: p0_X ? p0_X[i] : undefined,
                p0_Y: p0_Y ? p0_Y[i] : undefined,
                p0_Z: p0_Z ? p0_Z[i] : undefined,
                p1_X: p1_X ? p1_X[i] : undefined,
                p1_Y: p1_Y ? p1_Y[i] : undefined,
                p1_Z: p1_Z ? p1_Z[i] : undefined,
                mid_X: mid_X ? mid_X[i] : undefined,
                mid_Y: mid_Y ? mid_Y[i] : undefined,
                mid_Z: mid_Z ? mid_Z[i] : undefined,
                center_X: center_X ? center_X[i] : undefined,
                center_Y: center_Y ? center_Y[i] : undefined,
                center_Z: center_Z ? center_Z[i] : undefined,
                radius: radius ? radius[i] : undefined,
                normal_X: normal_X ? normal_X[i] : undefined,
                normal_Y: normal_Y ? normal_Y[i] : undefined,
                normal_Z: normal_Z ? normal_Z[i] : undefined,
                length: length ? length[i] : undefined,
                width: width ? width[i] : undefined,
                orientation_X: orientation_X ? orientation_X[i] : undefined,
                orientation_Y: orientation_Y ? orientation_Y[i] : undefined,
                orientation_Z: orientation_Z ? orientation_Z[i] : undefined,
                flipped: flipped ? flipped[i] : undefined,
                locationLine: locationLine ? locationLine[i] : undefined,
                locationLineOffset: locationLineOffset ? locationLineOffset[i] : undefined,
                coreOffsetExterior: coreOffsetExterior ? coreOffsetExterior[i] : undefined,
                baseOffset: baseOffset ? baseOffset[i] : undefined,
                baseElevation: baseElevation ? baseElevation[i] : undefined,
                topOffset: topOffset ? topOffset[i] : undefined,
                height: height ? height[i] : undefined,
                hasEditedProfile: hasEditedProfile ? hasEditedProfile[i] : undefined,
                topAttachedCount: topAttachedCount ? topAttachedCount[i] : undefined,
                baseAttachedCount: baseAttachedCount ? baseAttachedCount[i] : undefined,
                isStacked: isStacked ? isStacked[i] : undefined,
                isCurtain: isCurtain ? isCurtain[i] : undefined,
                baseLevelIndex: baseLevelIndex ? baseLevelIndex[i] : undefined,
                topLevelIndex: topLevelIndex ? topLevelIndex[i] : undefined,
                compoundStructureIndex: compoundStructureIndex ? compoundStructureIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return hostPath
    }
    
    async getCurveKind(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "int:CurveKind"))
    }
    
    async getAllCurveKind(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:CurveKind"))
    }
    
    async getP0_X(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:P0.X"))
    }
    
    async getAllP0_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:P0.X"))
    }
    
    async getP0_Y(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:P0.Y"))
    }
    
    async getAllP0_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:P0.Y"))
    }
    
    async getP0_Z(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:P0.Z"))
    }
    
    async getAllP0_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:P0.Z"))
    }
    
    async getP1_X(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:P1.X"))
    }
    
    async getAllP1_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:P1.X"))
    }
    
    async getP1_Y(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:P1.Y"))
    }
    
    async getAllP1_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:P1.Y"))
    }
    
    async getP1_Z(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:P1.Z"))
    }
    
    async getAllP1_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:P1.Z"))
    }
    
    async getMid_X(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:Mid.X"))
    }
    
    async getAllMid_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Mid.X"))
    }
    
    async getMid_Y(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:Mid.Y"))
    }
    
    async getAllMid_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Mid.Y"))
    }
    
    async getMid_Z(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:Mid.Z"))
    }
    
    async getAllMid_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Mid.Z"))
    }
    
    async getCenter_X(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:Center.X"))
    }
    
    async getAllCenter_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Center.X"))
    }
    
    async getCenter_Y(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:Center.Y"))
    }
    
    async getAllCenter_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Center.Y"))
    }
    
    async getCenter_Z(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:Center.Z"))
    }
    
    async getAllCenter_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Center.Z"))
    }
    
    async getRadius(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:Radius"))
    }
    
    async getAllRadius(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Radius"))
    }
    
    async getNormal_X(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:Normal.X"))
    }
    
    async getAllNormal_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Normal.X"))
    }
    
    async getNormal_Y(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:Normal.Y"))
    }
    
    async getAllNormal_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Normal.Y"))
    }
    
    async getNormal_Z(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:Normal.Z"))
    }
    
    async getAllNormal_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Normal.Z"))
    }
    
    async getLength(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:Length"))
    }
    
    async getAllLength(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Length"))
    }
    
    async getWidth(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:Width"))
    }
    
    async getAllWidth(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Width"))
    }
    
    async getOrientation_X(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:Orientation.X"))
    }
    
    async getAllOrientation_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Orientation.X"))
    }
    
    async getOrientation_Y(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:Orientation.Y"))
    }
    
    async getAllOrientation_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Orientation.Y"))
    }
    
    async getOrientation_Z(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:Orientation.Z"))
    }
    
    async getAllOrientation_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Orientation.Z"))
    }
    
    async getFlipped(hostPathIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(hostPathIndex, "byte:Flipped"))
    }
    
    async getAllFlipped(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:Flipped"))
    }
    
    async getLocationLine(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "int:LocationLine"))
    }
    
    async getAllLocationLine(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:LocationLine"))
    }
    
    async getLocationLineOffset(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:LocationLineOffset"))
    }
    
    async getAllLocationLineOffset(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:LocationLineOffset"))
    }
    
    async getCoreOffsetExterior(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:CoreOffsetExterior"))
    }
    
    async getAllCoreOffsetExterior(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:CoreOffsetExterior"))
    }
    
    async getBaseOffset(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:BaseOffset"))
    }
    
    async getAllBaseOffset(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:BaseOffset"))
    }
    
    async getBaseElevation(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:BaseElevation"))
    }
    
    async getAllBaseElevation(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:BaseElevation"))
    }
    
    async getTopOffset(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:TopOffset"))
    }
    
    async getAllTopOffset(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:TopOffset"))
    }
    
    async getHeight(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "double:Height"))
    }
    
    async getAllHeight(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Height"))
    }
    
    async getHasEditedProfile(hostPathIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(hostPathIndex, "byte:HasEditedProfile"))
    }
    
    async getAllHasEditedProfile(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:HasEditedProfile"))
    }
    
    async getTopAttachedCount(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "int:TopAttachedCount"))
    }
    
    async getAllTopAttachedCount(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:TopAttachedCount"))
    }
    
    async getBaseAttachedCount(hostPathIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostPathIndex, "int:BaseAttachedCount"))
    }
    
    async getAllBaseAttachedCount(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:BaseAttachedCount"))
    }
    
    async getIsStacked(hostPathIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(hostPathIndex, "byte:IsStacked"))
    }
    
    async getAllIsStacked(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsStacked"))
    }
    
    async getIsCurtain(hostPathIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(hostPathIndex, "byte:IsCurtain"))
    }
    
    async getAllIsCurtain(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsCurtain"))
    }
    
    async getBaseLevelIndex(hostPathIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(hostPathIndex, "index:Vim.Level:BaseLevel")
    }
    
    async getAllBaseLevelIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Level:BaseLevel")
    }
    
    async getBaseLevel(hostPathIndex: number): Promise<ILevel | undefined> {
        const index = await this.getBaseLevelIndex(hostPathIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.level?.get(index)
    }
    
    async getTopLevelIndex(hostPathIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(hostPathIndex, "index:Vim.Level:TopLevel")
    }
    
    async getAllTopLevelIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Level:TopLevel")
    }
    
    async getTopLevel(hostPathIndex: number): Promise<ILevel | undefined> {
        const index = await this.getTopLevelIndex(hostPathIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.level?.get(index)
    }
    
    async getCompoundStructureIndex(hostPathIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(hostPathIndex, "index:Vim.CompoundStructure:CompoundStructure")
    }
    
    async getAllCompoundStructureIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.CompoundStructure:CompoundStructure")
    }
    
    async getCompoundStructure(hostPathIndex: number): Promise<ICompoundStructure | undefined> {
        const index = await this.getCompoundStructureIndex(hostPathIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.compoundStructure?.get(index)
    }
    
    async getElementIndex(hostPathIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(hostPathIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(hostPathIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(hostPathIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IHostedInsert {
    index: number
    kind?: number
    distanceAlongPath?: number
    normalizedParameter?: number
    offsetFromPath?: number
    elevation?: number
    sillHeight?: number
    headHeight?: number
    facingFlipped?: boolean
    handFlipped?: boolean
    origin_X?: number
    origin_Y?: number
    origin_Z?: number
    openingMin_X?: number
    openingMin_Y?: number
    openingMin_Z?: number
    openingMax_X?: number
    openingMax_Y?: number
    openingMax_Z?: number
    
    hostIndex?: number
    host?: IElement
    hostPathIndex?: number
    hostPath?: IHostPath
    elementIndex?: number
    element?: IElement
}

export interface IHostedInsertTable {
    getCount(): Promise<number>
    get(hostedInsertIndex: number): Promise<IHostedInsert>
    getAll(): Promise<IHostedInsert[]>
    
    getKind(hostedInsertIndex: number): Promise<number | undefined>
    getAllKind(): Promise<number[] | undefined>
    getDistanceAlongPath(hostedInsertIndex: number): Promise<number | undefined>
    getAllDistanceAlongPath(): Promise<number[] | undefined>
    getNormalizedParameter(hostedInsertIndex: number): Promise<number | undefined>
    getAllNormalizedParameter(): Promise<number[] | undefined>
    getOffsetFromPath(hostedInsertIndex: number): Promise<number | undefined>
    getAllOffsetFromPath(): Promise<number[] | undefined>
    getElevation(hostedInsertIndex: number): Promise<number | undefined>
    getAllElevation(): Promise<number[] | undefined>
    getSillHeight(hostedInsertIndex: number): Promise<number | undefined>
    getAllSillHeight(): Promise<number[] | undefined>
    getHeadHeight(hostedInsertIndex: number): Promise<number | undefined>
    getAllHeadHeight(): Promise<number[] | undefined>
    getFacingFlipped(hostedInsertIndex: number): Promise<boolean | undefined>
    getAllFacingFlipped(): Promise<boolean[] | undefined>
    getHandFlipped(hostedInsertIndex: number): Promise<boolean | undefined>
    getAllHandFlipped(): Promise<boolean[] | undefined>
    getOrigin_X(hostedInsertIndex: number): Promise<number | undefined>
    getAllOrigin_X(): Promise<number[] | undefined>
    getOrigin_Y(hostedInsertIndex: number): Promise<number | undefined>
    getAllOrigin_Y(): Promise<number[] | undefined>
    getOrigin_Z(hostedInsertIndex: number): Promise<number | undefined>
    getAllOrigin_Z(): Promise<number[] | undefined>
    getOpeningMin_X(hostedInsertIndex: number): Promise<number | undefined>
    getAllOpeningMin_X(): Promise<number[] | undefined>
    getOpeningMin_Y(hostedInsertIndex: number): Promise<number | undefined>
    getAllOpeningMin_Y(): Promise<number[] | undefined>
    getOpeningMin_Z(hostedInsertIndex: number): Promise<number | undefined>
    getAllOpeningMin_Z(): Promise<number[] | undefined>
    getOpeningMax_X(hostedInsertIndex: number): Promise<number | undefined>
    getAllOpeningMax_X(): Promise<number[] | undefined>
    getOpeningMax_Y(hostedInsertIndex: number): Promise<number | undefined>
    getAllOpeningMax_Y(): Promise<number[] | undefined>
    getOpeningMax_Z(hostedInsertIndex: number): Promise<number | undefined>
    getAllOpeningMax_Z(): Promise<number[] | undefined>
    
    getHostIndex(hostedInsertIndex: number): Promise<number | undefined>
    getAllHostIndex(): Promise<number[] | undefined>
    getHost(hostedInsertIndex: number): Promise<IElement | undefined>
    getHostPathIndex(hostedInsertIndex: number): Promise<number | undefined>
    getAllHostPathIndex(): Promise<number[] | undefined>
    getHostPath(hostedInsertIndex: number): Promise<IHostPath | undefined>
    getElementIndex(hostedInsertIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(hostedInsertIndex: number): Promise<IElement | undefined>
}

export class HostedInsert implements IHostedInsert {
    index: number
    kind?: number
    distanceAlongPath?: number
    normalizedParameter?: number
    offsetFromPath?: number
    elevation?: number
    sillHeight?: number
    headHeight?: number
    facingFlipped?: boolean
    handFlipped?: boolean
    origin_X?: number
    origin_Y?: number
    origin_Z?: number
    openingMin_X?: number
    openingMin_Y?: number
    openingMin_Z?: number
    openingMax_X?: number
    openingMax_Y?: number
    openingMax_Z?: number
    
    hostIndex?: number
    host?: IElement
    hostPathIndex?: number
    hostPath?: IHostPath
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IHostedInsertTable, index: number): Promise<IHostedInsert> {
        let result = new HostedInsert()
        result.index = index
        
        await Promise.all([
            table.getKind(index).then(v => result.kind = v),
            table.getDistanceAlongPath(index).then(v => result.distanceAlongPath = v),
            table.getNormalizedParameter(index).then(v => result.normalizedParameter = v),
            table.getOffsetFromPath(index).then(v => result.offsetFromPath = v),
            table.getElevation(index).then(v => result.elevation = v),
            table.getSillHeight(index).then(v => result.sillHeight = v),
            table.getHeadHeight(index).then(v => result.headHeight = v),
            table.getFacingFlipped(index).then(v => result.facingFlipped = v),
            table.getHandFlipped(index).then(v => result.handFlipped = v),
            table.getOrigin_X(index).then(v => result.origin_X = v),
            table.getOrigin_Y(index).then(v => result.origin_Y = v),
            table.getOrigin_Z(index).then(v => result.origin_Z = v),
            table.getOpeningMin_X(index).then(v => result.openingMin_X = v),
            table.getOpeningMin_Y(index).then(v => result.openingMin_Y = v),
            table.getOpeningMin_Z(index).then(v => result.openingMin_Z = v),
            table.getOpeningMax_X(index).then(v => result.openingMax_X = v),
            table.getOpeningMax_Y(index).then(v => result.openingMax_Y = v),
            table.getOpeningMax_Z(index).then(v => result.openingMax_Z = v),
            table.getHostIndex(index).then(v => result.hostIndex = v),
            table.getHostPathIndex(index).then(v => result.hostPathIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class HostedInsertTable implements IHostedInsertTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IHostedInsertTable | undefined> {
        const entity = await document.entities.getBfast("Vim.HostedInsert")
        
        if (!entity) {
            return undefined
        }
        
        let table = new HostedInsertTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(hostedInsertIndex: number): Promise<IHostedInsert> {
        return await HostedInsert.createFromTable(this, hostedInsertIndex)
    }
    
    async getAll(): Promise<IHostedInsert[]> {
        const localTable = await this.entityTable.getLocal()
        
        let kind: number[] | undefined
        let distanceAlongPath: number[] | undefined
        let normalizedParameter: number[] | undefined
        let offsetFromPath: number[] | undefined
        let elevation: number[] | undefined
        let sillHeight: number[] | undefined
        let headHeight: number[] | undefined
        let facingFlipped: boolean[] | undefined
        let handFlipped: boolean[] | undefined
        let origin_X: number[] | undefined
        let origin_Y: number[] | undefined
        let origin_Z: number[] | undefined
        let openingMin_X: number[] | undefined
        let openingMin_Y: number[] | undefined
        let openingMin_Z: number[] | undefined
        let openingMax_X: number[] | undefined
        let openingMax_Y: number[] | undefined
        let openingMax_Z: number[] | undefined
        let hostIndex: number[] | undefined
        let hostPathIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { kind = (await localTable.getNumberArray("int:Kind")) })(),
            (async () => { distanceAlongPath = (await localTable.getNumberArray("double:DistanceAlongPath")) })(),
            (async () => { normalizedParameter = (await localTable.getNumberArray("double:NormalizedParameter")) })(),
            (async () => { offsetFromPath = (await localTable.getNumberArray("double:OffsetFromPath")) })(),
            (async () => { elevation = (await localTable.getNumberArray("double:Elevation")) })(),
            (async () => { sillHeight = (await localTable.getNumberArray("double:SillHeight")) })(),
            (async () => { headHeight = (await localTable.getNumberArray("double:HeadHeight")) })(),
            (async () => { facingFlipped = (await localTable.getBooleanArray("byte:FacingFlipped")) })(),
            (async () => { handFlipped = (await localTable.getBooleanArray("byte:HandFlipped")) })(),
            (async () => { origin_X = (await localTable.getNumberArray("double:Origin.X")) })(),
            (async () => { origin_Y = (await localTable.getNumberArray("double:Origin.Y")) })(),
            (async () => { origin_Z = (await localTable.getNumberArray("double:Origin.Z")) })(),
            (async () => { openingMin_X = (await localTable.getNumberArray("double:OpeningMin.X")) })(),
            (async () => { openingMin_Y = (await localTable.getNumberArray("double:OpeningMin.Y")) })(),
            (async () => { openingMin_Z = (await localTable.getNumberArray("double:OpeningMin.Z")) })(),
            (async () => { openingMax_X = (await localTable.getNumberArray("double:OpeningMax.X")) })(),
            (async () => { openingMax_Y = (await localTable.getNumberArray("double:OpeningMax.Y")) })(),
            (async () => { openingMax_Z = (await localTable.getNumberArray("double:OpeningMax.Z")) })(),
            (async () => { hostIndex = (await localTable.getNumberArray("index:Vim.Element:Host")) })(),
            (async () => { hostPathIndex = (await localTable.getNumberArray("index:Vim.HostPath:HostPath")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let hostedInsert: IHostedInsert[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            hostedInsert.push({
                index: i,
                kind: kind ? kind[i] : undefined,
                distanceAlongPath: distanceAlongPath ? distanceAlongPath[i] : undefined,
                normalizedParameter: normalizedParameter ? normalizedParameter[i] : undefined,
                offsetFromPath: offsetFromPath ? offsetFromPath[i] : undefined,
                elevation: elevation ? elevation[i] : undefined,
                sillHeight: sillHeight ? sillHeight[i] : undefined,
                headHeight: headHeight ? headHeight[i] : undefined,
                facingFlipped: facingFlipped ? facingFlipped[i] : undefined,
                handFlipped: handFlipped ? handFlipped[i] : undefined,
                origin_X: origin_X ? origin_X[i] : undefined,
                origin_Y: origin_Y ? origin_Y[i] : undefined,
                origin_Z: origin_Z ? origin_Z[i] : undefined,
                openingMin_X: openingMin_X ? openingMin_X[i] : undefined,
                openingMin_Y: openingMin_Y ? openingMin_Y[i] : undefined,
                openingMin_Z: openingMin_Z ? openingMin_Z[i] : undefined,
                openingMax_X: openingMax_X ? openingMax_X[i] : undefined,
                openingMax_Y: openingMax_Y ? openingMax_Y[i] : undefined,
                openingMax_Z: openingMax_Z ? openingMax_Z[i] : undefined,
                hostIndex: hostIndex ? hostIndex[i] : undefined,
                hostPathIndex: hostPathIndex ? hostPathIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return hostedInsert
    }
    
    async getKind(hostedInsertIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostedInsertIndex, "int:Kind"))
    }
    
    async getAllKind(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Kind"))
    }
    
    async getDistanceAlongPath(hostedInsertIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostedInsertIndex, "double:DistanceAlongPath"))
    }
    
    async getAllDistanceAlongPath(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:DistanceAlongPath"))
    }
    
    async getNormalizedParameter(hostedInsertIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostedInsertIndex, "double:NormalizedParameter"))
    }
    
    async getAllNormalizedParameter(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:NormalizedParameter"))
    }
    
    async getOffsetFromPath(hostedInsertIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostedInsertIndex, "double:OffsetFromPath"))
    }
    
    async getAllOffsetFromPath(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:OffsetFromPath"))
    }
    
    async getElevation(hostedInsertIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostedInsertIndex, "double:Elevation"))
    }
    
    async getAllElevation(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Elevation"))
    }
    
    async getSillHeight(hostedInsertIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostedInsertIndex, "double:SillHeight"))
    }
    
    async getAllSillHeight(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:SillHeight"))
    }
    
    async getHeadHeight(hostedInsertIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostedInsertIndex, "double:HeadHeight"))
    }
    
    async getAllHeadHeight(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:HeadHeight"))
    }
    
    async getFacingFlipped(hostedInsertIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(hostedInsertIndex, "byte:FacingFlipped"))
    }
    
    async getAllFacingFlipped(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:FacingFlipped"))
    }
    
    async getHandFlipped(hostedInsertIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(hostedInsertIndex, "byte:HandFlipped"))
    }
    
    async getAllHandFlipped(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:HandFlipped"))
    }
    
    async getOrigin_X(hostedInsertIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostedInsertIndex, "double:Origin.X"))
    }
    
    async getAllOrigin_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Origin.X"))
    }
    
    async getOrigin_Y(hostedInsertIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostedInsertIndex, "double:Origin.Y"))
    }
    
    async getAllOrigin_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Origin.Y"))
    }
    
    async getOrigin_Z(hostedInsertIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostedInsertIndex, "double:Origin.Z"))
    }
    
    async getAllOrigin_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Origin.Z"))
    }
    
    async getOpeningMin_X(hostedInsertIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostedInsertIndex, "double:OpeningMin.X"))
    }
    
    async getAllOpeningMin_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:OpeningMin.X"))
    }
    
    async getOpeningMin_Y(hostedInsertIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostedInsertIndex, "double:OpeningMin.Y"))
    }
    
    async getAllOpeningMin_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:OpeningMin.Y"))
    }
    
    async getOpeningMin_Z(hostedInsertIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostedInsertIndex, "double:OpeningMin.Z"))
    }
    
    async getAllOpeningMin_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:OpeningMin.Z"))
    }
    
    async getOpeningMax_X(hostedInsertIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostedInsertIndex, "double:OpeningMax.X"))
    }
    
    async getAllOpeningMax_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:OpeningMax.X"))
    }
    
    async getOpeningMax_Y(hostedInsertIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostedInsertIndex, "double:OpeningMax.Y"))
    }
    
    async getAllOpeningMax_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:OpeningMax.Y"))
    }
    
    async getOpeningMax_Z(hostedInsertIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(hostedInsertIndex, "double:OpeningMax.Z"))
    }
    
    async getAllOpeningMax_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:OpeningMax.Z"))
    }
    
    async getHostIndex(hostedInsertIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(hostedInsertIndex, "index:Vim.Element:Host")
    }
    
    async getAllHostIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Host")
    }
    
    async getHost(hostedInsertIndex: number): Promise<IElement | undefined> {
        const index = await this.getHostIndex(hostedInsertIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
    async getHostPathIndex(hostedInsertIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(hostedInsertIndex, "index:Vim.HostPath:HostPath")
    }
    
    async getAllHostPathIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.HostPath:HostPath")
    }
    
    async getHostPath(hostedInsertIndex: number): Promise<IHostPath | undefined> {
        const index = await this.getHostPathIndex(hostedInsertIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.hostPath?.get(index)
    }
    
    async getElementIndex(hostedInsertIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(hostedInsertIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(hostedInsertIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(hostedInsertIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IFamilyRecipe {
    index: number
    readStatus?: number
    refusal?: string
    flexibility?: number
    reading?: string
    placementType?: string
    isParametric?: boolean
    currentTypeName?: string
    labelledDimensionCount?: number
    drivingDimensionCount?: number
    lockCount?: number
    formCount?: number
    openingCount?: number
    nestedInstanceCount?: number
    openingTied?: number
    readMs?: number
    
    familyIndex?: number
    family?: IFamily
}

export interface IFamilyRecipeTable {
    getCount(): Promise<number>
    get(familyRecipeIndex: number): Promise<IFamilyRecipe>
    getAll(): Promise<IFamilyRecipe[]>
    
    getReadStatus(familyRecipeIndex: number): Promise<number | undefined>
    getAllReadStatus(): Promise<number[] | undefined>
    getRefusal(familyRecipeIndex: number): Promise<string | undefined>
    getAllRefusal(): Promise<string[] | undefined>
    getFlexibility(familyRecipeIndex: number): Promise<number | undefined>
    getAllFlexibility(): Promise<number[] | undefined>
    getReading(familyRecipeIndex: number): Promise<string | undefined>
    getAllReading(): Promise<string[] | undefined>
    getPlacementType(familyRecipeIndex: number): Promise<string | undefined>
    getAllPlacementType(): Promise<string[] | undefined>
    getIsParametric(familyRecipeIndex: number): Promise<boolean | undefined>
    getAllIsParametric(): Promise<boolean[] | undefined>
    getCurrentTypeName(familyRecipeIndex: number): Promise<string | undefined>
    getAllCurrentTypeName(): Promise<string[] | undefined>
    getLabelledDimensionCount(familyRecipeIndex: number): Promise<number | undefined>
    getAllLabelledDimensionCount(): Promise<number[] | undefined>
    getDrivingDimensionCount(familyRecipeIndex: number): Promise<number | undefined>
    getAllDrivingDimensionCount(): Promise<number[] | undefined>
    getLockCount(familyRecipeIndex: number): Promise<number | undefined>
    getAllLockCount(): Promise<number[] | undefined>
    getFormCount(familyRecipeIndex: number): Promise<number | undefined>
    getAllFormCount(): Promise<number[] | undefined>
    getOpeningCount(familyRecipeIndex: number): Promise<number | undefined>
    getAllOpeningCount(): Promise<number[] | undefined>
    getNestedInstanceCount(familyRecipeIndex: number): Promise<number | undefined>
    getAllNestedInstanceCount(): Promise<number[] | undefined>
    getOpeningTied(familyRecipeIndex: number): Promise<number | undefined>
    getAllOpeningTied(): Promise<number[] | undefined>
    getReadMs(familyRecipeIndex: number): Promise<number | undefined>
    getAllReadMs(): Promise<number[] | undefined>
    
    getFamilyIndex(familyRecipeIndex: number): Promise<number | undefined>
    getAllFamilyIndex(): Promise<number[] | undefined>
    getFamily(familyRecipeIndex: number): Promise<IFamily | undefined>
}

export class FamilyRecipe implements IFamilyRecipe {
    index: number
    readStatus?: number
    refusal?: string
    flexibility?: number
    reading?: string
    placementType?: string
    isParametric?: boolean
    currentTypeName?: string
    labelledDimensionCount?: number
    drivingDimensionCount?: number
    lockCount?: number
    formCount?: number
    openingCount?: number
    nestedInstanceCount?: number
    openingTied?: number
    readMs?: number
    
    familyIndex?: number
    family?: IFamily
    
    static async createFromTable(table: IFamilyRecipeTable, index: number): Promise<IFamilyRecipe> {
        let result = new FamilyRecipe()
        result.index = index
        
        await Promise.all([
            table.getReadStatus(index).then(v => result.readStatus = v),
            table.getRefusal(index).then(v => result.refusal = v),
            table.getFlexibility(index).then(v => result.flexibility = v),
            table.getReading(index).then(v => result.reading = v),
            table.getPlacementType(index).then(v => result.placementType = v),
            table.getIsParametric(index).then(v => result.isParametric = v),
            table.getCurrentTypeName(index).then(v => result.currentTypeName = v),
            table.getLabelledDimensionCount(index).then(v => result.labelledDimensionCount = v),
            table.getDrivingDimensionCount(index).then(v => result.drivingDimensionCount = v),
            table.getLockCount(index).then(v => result.lockCount = v),
            table.getFormCount(index).then(v => result.formCount = v),
            table.getOpeningCount(index).then(v => result.openingCount = v),
            table.getNestedInstanceCount(index).then(v => result.nestedInstanceCount = v),
            table.getOpeningTied(index).then(v => result.openingTied = v),
            table.getReadMs(index).then(v => result.readMs = v),
            table.getFamilyIndex(index).then(v => result.familyIndex = v),
        ])
        
        return result
    }
}

export class FamilyRecipeTable implements IFamilyRecipeTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IFamilyRecipeTable | undefined> {
        const entity = await document.entities.getBfast("Vim.FamilyRecipe")
        
        if (!entity) {
            return undefined
        }
        
        let table = new FamilyRecipeTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(familyRecipeIndex: number): Promise<IFamilyRecipe> {
        return await FamilyRecipe.createFromTable(this, familyRecipeIndex)
    }
    
    async getAll(): Promise<IFamilyRecipe[]> {
        const localTable = await this.entityTable.getLocal()
        
        let readStatus: number[] | undefined
        let refusal: string[] | undefined
        let flexibility: number[] | undefined
        let reading: string[] | undefined
        let placementType: string[] | undefined
        let isParametric: boolean[] | undefined
        let currentTypeName: string[] | undefined
        let labelledDimensionCount: number[] | undefined
        let drivingDimensionCount: number[] | undefined
        let lockCount: number[] | undefined
        let formCount: number[] | undefined
        let openingCount: number[] | undefined
        let nestedInstanceCount: number[] | undefined
        let openingTied: number[] | undefined
        let readMs: number[] | undefined
        let familyIndex: number[] | undefined
        
        await Promise.all([
            (async () => { readStatus = (await localTable.getNumberArray("int:ReadStatus")) })(),
            (async () => { refusal = (await localTable.getStringArray("string:Refusal")) })(),
            (async () => { flexibility = (await localTable.getNumberArray("int:Flexibility")) })(),
            (async () => { reading = (await localTable.getStringArray("string:Reading")) })(),
            (async () => { placementType = (await localTable.getStringArray("string:PlacementType")) })(),
            (async () => { isParametric = (await localTable.getBooleanArray("byte:IsParametric")) })(),
            (async () => { currentTypeName = (await localTable.getStringArray("string:CurrentTypeName")) })(),
            (async () => { labelledDimensionCount = (await localTable.getNumberArray("int:LabelledDimensionCount")) })(),
            (async () => { drivingDimensionCount = (await localTable.getNumberArray("int:DrivingDimensionCount")) })(),
            (async () => { lockCount = (await localTable.getNumberArray("int:LockCount")) })(),
            (async () => { formCount = (await localTable.getNumberArray("int:FormCount")) })(),
            (async () => { openingCount = (await localTable.getNumberArray("int:OpeningCount")) })(),
            (async () => { nestedInstanceCount = (await localTable.getNumberArray("int:NestedInstanceCount")) })(),
            (async () => { openingTied = (await localTable.getNumberArray("int:OpeningTied")) })(),
            (async () => { readMs = (await localTable.getNumberArray("int:ReadMs")) })(),
            (async () => { familyIndex = (await localTable.getNumberArray("index:Vim.Family:Family")) })(),
        ])
        
        let familyRecipe: IFamilyRecipe[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            familyRecipe.push({
                index: i,
                readStatus: readStatus ? readStatus[i] : undefined,
                refusal: refusal ? refusal[i] : undefined,
                flexibility: flexibility ? flexibility[i] : undefined,
                reading: reading ? reading[i] : undefined,
                placementType: placementType ? placementType[i] : undefined,
                isParametric: isParametric ? isParametric[i] : undefined,
                currentTypeName: currentTypeName ? currentTypeName[i] : undefined,
                labelledDimensionCount: labelledDimensionCount ? labelledDimensionCount[i] : undefined,
                drivingDimensionCount: drivingDimensionCount ? drivingDimensionCount[i] : undefined,
                lockCount: lockCount ? lockCount[i] : undefined,
                formCount: formCount ? formCount[i] : undefined,
                openingCount: openingCount ? openingCount[i] : undefined,
                nestedInstanceCount: nestedInstanceCount ? nestedInstanceCount[i] : undefined,
                openingTied: openingTied ? openingTied[i] : undefined,
                readMs: readMs ? readMs[i] : undefined,
                familyIndex: familyIndex ? familyIndex[i] : undefined
            })
        }
        
        return familyRecipe
    }
    
    async getReadStatus(familyRecipeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyRecipeIndex, "int:ReadStatus"))
    }
    
    async getAllReadStatus(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:ReadStatus"))
    }
    
    async getRefusal(familyRecipeIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(familyRecipeIndex, "string:Refusal"))
    }
    
    async getAllRefusal(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Refusal"))
    }
    
    async getFlexibility(familyRecipeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyRecipeIndex, "int:Flexibility"))
    }
    
    async getAllFlexibility(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Flexibility"))
    }
    
    async getReading(familyRecipeIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(familyRecipeIndex, "string:Reading"))
    }
    
    async getAllReading(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Reading"))
    }
    
    async getPlacementType(familyRecipeIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(familyRecipeIndex, "string:PlacementType"))
    }
    
    async getAllPlacementType(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:PlacementType"))
    }
    
    async getIsParametric(familyRecipeIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(familyRecipeIndex, "byte:IsParametric"))
    }
    
    async getAllIsParametric(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsParametric"))
    }
    
    async getCurrentTypeName(familyRecipeIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(familyRecipeIndex, "string:CurrentTypeName"))
    }
    
    async getAllCurrentTypeName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:CurrentTypeName"))
    }
    
    async getLabelledDimensionCount(familyRecipeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyRecipeIndex, "int:LabelledDimensionCount"))
    }
    
    async getAllLabelledDimensionCount(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:LabelledDimensionCount"))
    }
    
    async getDrivingDimensionCount(familyRecipeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyRecipeIndex, "int:DrivingDimensionCount"))
    }
    
    async getAllDrivingDimensionCount(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:DrivingDimensionCount"))
    }
    
    async getLockCount(familyRecipeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyRecipeIndex, "int:LockCount"))
    }
    
    async getAllLockCount(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:LockCount"))
    }
    
    async getFormCount(familyRecipeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyRecipeIndex, "int:FormCount"))
    }
    
    async getAllFormCount(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:FormCount"))
    }
    
    async getOpeningCount(familyRecipeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyRecipeIndex, "int:OpeningCount"))
    }
    
    async getAllOpeningCount(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:OpeningCount"))
    }
    
    async getNestedInstanceCount(familyRecipeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyRecipeIndex, "int:NestedInstanceCount"))
    }
    
    async getAllNestedInstanceCount(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:NestedInstanceCount"))
    }
    
    async getOpeningTied(familyRecipeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyRecipeIndex, "int:OpeningTied"))
    }
    
    async getAllOpeningTied(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:OpeningTied"))
    }
    
    async getReadMs(familyRecipeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyRecipeIndex, "int:ReadMs"))
    }
    
    async getAllReadMs(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:ReadMs"))
    }
    
    async getFamilyIndex(familyRecipeIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(familyRecipeIndex, "index:Vim.Family:Family")
    }
    
    async getAllFamilyIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Family:Family")
    }
    
    async getFamily(familyRecipeIndex: number): Promise<IFamily | undefined> {
        const index = await this.getFamilyIndex(familyRecipeIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.family?.get(index)
    }
    
}

export interface IFamilyRecipeParameter {
    index: number
    name?: string
    familyElementId?: bigint
    guid?: string
    dataType?: string
    storageType?: string
    isInstance?: boolean
    isReporting?: boolean
    isShared?: boolean
    isDeterminedByFormula?: boolean
    formula?: string
    drives?: number
    
    familyIndex?: number
    family?: IFamily
}

export interface IFamilyRecipeParameterTable {
    getCount(): Promise<number>
    get(familyRecipeParameterIndex: number): Promise<IFamilyRecipeParameter>
    getAll(): Promise<IFamilyRecipeParameter[]>
    
    getName(familyRecipeParameterIndex: number): Promise<string | undefined>
    getAllName(): Promise<string[] | undefined>
    getFamilyElementId(familyRecipeParameterIndex: number): Promise<bigint | undefined>
    getAllFamilyElementId(): Promise<BigInt64Array | undefined>
    getGuid(familyRecipeParameterIndex: number): Promise<string | undefined>
    getAllGuid(): Promise<string[] | undefined>
    getDataType(familyRecipeParameterIndex: number): Promise<string | undefined>
    getAllDataType(): Promise<string[] | undefined>
    getStorageType(familyRecipeParameterIndex: number): Promise<string | undefined>
    getAllStorageType(): Promise<string[] | undefined>
    getIsInstance(familyRecipeParameterIndex: number): Promise<boolean | undefined>
    getAllIsInstance(): Promise<boolean[] | undefined>
    getIsReporting(familyRecipeParameterIndex: number): Promise<boolean | undefined>
    getAllIsReporting(): Promise<boolean[] | undefined>
    getIsShared(familyRecipeParameterIndex: number): Promise<boolean | undefined>
    getAllIsShared(): Promise<boolean[] | undefined>
    getIsDeterminedByFormula(familyRecipeParameterIndex: number): Promise<boolean | undefined>
    getAllIsDeterminedByFormula(): Promise<boolean[] | undefined>
    getFormula(familyRecipeParameterIndex: number): Promise<string | undefined>
    getAllFormula(): Promise<string[] | undefined>
    getDrives(familyRecipeParameterIndex: number): Promise<number | undefined>
    getAllDrives(): Promise<number[] | undefined>
    
    getFamilyIndex(familyRecipeParameterIndex: number): Promise<number | undefined>
    getAllFamilyIndex(): Promise<number[] | undefined>
    getFamily(familyRecipeParameterIndex: number): Promise<IFamily | undefined>
}

export class FamilyRecipeParameter implements IFamilyRecipeParameter {
    index: number
    name?: string
    familyElementId?: bigint
    guid?: string
    dataType?: string
    storageType?: string
    isInstance?: boolean
    isReporting?: boolean
    isShared?: boolean
    isDeterminedByFormula?: boolean
    formula?: string
    drives?: number
    
    familyIndex?: number
    family?: IFamily
    
    static async createFromTable(table: IFamilyRecipeParameterTable, index: number): Promise<IFamilyRecipeParameter> {
        let result = new FamilyRecipeParameter()
        result.index = index
        
        await Promise.all([
            table.getName(index).then(v => result.name = v),
            table.getFamilyElementId(index).then(v => result.familyElementId = v),
            table.getGuid(index).then(v => result.guid = v),
            table.getDataType(index).then(v => result.dataType = v),
            table.getStorageType(index).then(v => result.storageType = v),
            table.getIsInstance(index).then(v => result.isInstance = v),
            table.getIsReporting(index).then(v => result.isReporting = v),
            table.getIsShared(index).then(v => result.isShared = v),
            table.getIsDeterminedByFormula(index).then(v => result.isDeterminedByFormula = v),
            table.getFormula(index).then(v => result.formula = v),
            table.getDrives(index).then(v => result.drives = v),
            table.getFamilyIndex(index).then(v => result.familyIndex = v),
        ])
        
        return result
    }
}

export class FamilyRecipeParameterTable implements IFamilyRecipeParameterTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IFamilyRecipeParameterTable | undefined> {
        const entity = await document.entities.getBfast("Vim.FamilyRecipeParameter")
        
        if (!entity) {
            return undefined
        }
        
        let table = new FamilyRecipeParameterTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(familyRecipeParameterIndex: number): Promise<IFamilyRecipeParameter> {
        return await FamilyRecipeParameter.createFromTable(this, familyRecipeParameterIndex)
    }
    
    async getAll(): Promise<IFamilyRecipeParameter[]> {
        const localTable = await this.entityTable.getLocal()
        
        let name: string[] | undefined
        let familyElementId: BigInt64Array | undefined
        let guid: string[] | undefined
        let dataType: string[] | undefined
        let storageType: string[] | undefined
        let isInstance: boolean[] | undefined
        let isReporting: boolean[] | undefined
        let isShared: boolean[] | undefined
        let isDeterminedByFormula: boolean[] | undefined
        let formula: string[] | undefined
        let drives: number[] | undefined
        let familyIndex: number[] | undefined
        
        await Promise.all([
            (async () => { name = (await localTable.getStringArray("string:Name")) })(),
            (async () => { familyElementId = (await localTable.getBigIntArray("long:FamilyElementId")) })(),
            (async () => { guid = (await localTable.getStringArray("string:Guid")) })(),
            (async () => { dataType = (await localTable.getStringArray("string:DataType")) })(),
            (async () => { storageType = (await localTable.getStringArray("string:StorageType")) })(),
            (async () => { isInstance = (await localTable.getBooleanArray("byte:IsInstance")) })(),
            (async () => { isReporting = (await localTable.getBooleanArray("byte:IsReporting")) })(),
            (async () => { isShared = (await localTable.getBooleanArray("byte:IsShared")) })(),
            (async () => { isDeterminedByFormula = (await localTable.getBooleanArray("byte:IsDeterminedByFormula")) })(),
            (async () => { formula = (await localTable.getStringArray("string:Formula")) })(),
            (async () => { drives = (await localTable.getNumberArray("int:Drives")) })(),
            (async () => { familyIndex = (await localTable.getNumberArray("index:Vim.Family:Family")) })(),
        ])
        
        let familyRecipeParameter: IFamilyRecipeParameter[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            familyRecipeParameter.push({
                index: i,
                name: name ? name[i] : undefined,
                familyElementId: familyElementId ? familyElementId[i] : undefined,
                guid: guid ? guid[i] : undefined,
                dataType: dataType ? dataType[i] : undefined,
                storageType: storageType ? storageType[i] : undefined,
                isInstance: isInstance ? isInstance[i] : undefined,
                isReporting: isReporting ? isReporting[i] : undefined,
                isShared: isShared ? isShared[i] : undefined,
                isDeterminedByFormula: isDeterminedByFormula ? isDeterminedByFormula[i] : undefined,
                formula: formula ? formula[i] : undefined,
                drives: drives ? drives[i] : undefined,
                familyIndex: familyIndex ? familyIndex[i] : undefined
            })
        }
        
        return familyRecipeParameter
    }
    
    async getName(familyRecipeParameterIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(familyRecipeParameterIndex, "string:Name"))
    }
    
    async getAllName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Name"))
    }
    
    async getFamilyElementId(familyRecipeParameterIndex: number): Promise<bigint | undefined> {
        return (await this.entityTable.getBigInt(familyRecipeParameterIndex, "long:FamilyElementId"))
    }
    
    async getAllFamilyElementId(): Promise<BigInt64Array | undefined> {
        return (await this.entityTable.getBigIntArray("long:FamilyElementId"))
    }
    
    async getGuid(familyRecipeParameterIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(familyRecipeParameterIndex, "string:Guid"))
    }
    
    async getAllGuid(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Guid"))
    }
    
    async getDataType(familyRecipeParameterIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(familyRecipeParameterIndex, "string:DataType"))
    }
    
    async getAllDataType(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:DataType"))
    }
    
    async getStorageType(familyRecipeParameterIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(familyRecipeParameterIndex, "string:StorageType"))
    }
    
    async getAllStorageType(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:StorageType"))
    }
    
    async getIsInstance(familyRecipeParameterIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(familyRecipeParameterIndex, "byte:IsInstance"))
    }
    
    async getAllIsInstance(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsInstance"))
    }
    
    async getIsReporting(familyRecipeParameterIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(familyRecipeParameterIndex, "byte:IsReporting"))
    }
    
    async getAllIsReporting(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsReporting"))
    }
    
    async getIsShared(familyRecipeParameterIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(familyRecipeParameterIndex, "byte:IsShared"))
    }
    
    async getAllIsShared(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsShared"))
    }
    
    async getIsDeterminedByFormula(familyRecipeParameterIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(familyRecipeParameterIndex, "byte:IsDeterminedByFormula"))
    }
    
    async getAllIsDeterminedByFormula(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsDeterminedByFormula"))
    }
    
    async getFormula(familyRecipeParameterIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(familyRecipeParameterIndex, "string:Formula"))
    }
    
    async getAllFormula(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Formula"))
    }
    
    async getDrives(familyRecipeParameterIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyRecipeParameterIndex, "int:Drives"))
    }
    
    async getAllDrives(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Drives"))
    }
    
    async getFamilyIndex(familyRecipeParameterIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(familyRecipeParameterIndex, "index:Vim.Family:Family")
    }
    
    async getAllFamilyIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Family:Family")
    }
    
    async getFamily(familyRecipeParameterIndex: number): Promise<IFamily | undefined> {
        const index = await this.getFamilyIndex(familyRecipeParameterIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.family?.get(index)
    }
    
}

export interface IFamilyTypeValue {
    index: number
    typeName?: string
    value?: string
    doubleValue?: number
    integerValue?: number
    elementIdValue?: bigint
    
    familyParameterIndex?: number
    familyParameter?: IFamilyRecipeParameter
    familyTypeIndex?: number
    familyType?: IFamilyType
}

export interface IFamilyTypeValueTable {
    getCount(): Promise<number>
    get(familyTypeValueIndex: number): Promise<IFamilyTypeValue>
    getAll(): Promise<IFamilyTypeValue[]>
    
    getTypeName(familyTypeValueIndex: number): Promise<string | undefined>
    getAllTypeName(): Promise<string[] | undefined>
    getValue(familyTypeValueIndex: number): Promise<string | undefined>
    getAllValue(): Promise<string[] | undefined>
    getDoubleValue(familyTypeValueIndex: number): Promise<number | undefined>
    getAllDoubleValue(): Promise<number[] | undefined>
    getIntegerValue(familyTypeValueIndex: number): Promise<number | undefined>
    getAllIntegerValue(): Promise<number[] | undefined>
    getElementIdValue(familyTypeValueIndex: number): Promise<bigint | undefined>
    getAllElementIdValue(): Promise<BigInt64Array | undefined>
    
    getFamilyParameterIndex(familyTypeValueIndex: number): Promise<number | undefined>
    getAllFamilyParameterIndex(): Promise<number[] | undefined>
    getFamilyParameter(familyTypeValueIndex: number): Promise<IFamilyRecipeParameter | undefined>
    getFamilyTypeIndex(familyTypeValueIndex: number): Promise<number | undefined>
    getAllFamilyTypeIndex(): Promise<number[] | undefined>
    getFamilyType(familyTypeValueIndex: number): Promise<IFamilyType | undefined>
}

export class FamilyTypeValue implements IFamilyTypeValue {
    index: number
    typeName?: string
    value?: string
    doubleValue?: number
    integerValue?: number
    elementIdValue?: bigint
    
    familyParameterIndex?: number
    familyParameter?: IFamilyRecipeParameter
    familyTypeIndex?: number
    familyType?: IFamilyType
    
    static async createFromTable(table: IFamilyTypeValueTable, index: number): Promise<IFamilyTypeValue> {
        let result = new FamilyTypeValue()
        result.index = index
        
        await Promise.all([
            table.getTypeName(index).then(v => result.typeName = v),
            table.getValue(index).then(v => result.value = v),
            table.getDoubleValue(index).then(v => result.doubleValue = v),
            table.getIntegerValue(index).then(v => result.integerValue = v),
            table.getElementIdValue(index).then(v => result.elementIdValue = v),
            table.getFamilyParameterIndex(index).then(v => result.familyParameterIndex = v),
            table.getFamilyTypeIndex(index).then(v => result.familyTypeIndex = v),
        ])
        
        return result
    }
}

export class FamilyTypeValueTable implements IFamilyTypeValueTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IFamilyTypeValueTable | undefined> {
        const entity = await document.entities.getBfast("Vim.FamilyTypeValue")
        
        if (!entity) {
            return undefined
        }
        
        let table = new FamilyTypeValueTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(familyTypeValueIndex: number): Promise<IFamilyTypeValue> {
        return await FamilyTypeValue.createFromTable(this, familyTypeValueIndex)
    }
    
    async getAll(): Promise<IFamilyTypeValue[]> {
        const localTable = await this.entityTable.getLocal()
        
        let typeName: string[] | undefined
        let value: string[] | undefined
        let doubleValue: number[] | undefined
        let integerValue: number[] | undefined
        let elementIdValue: BigInt64Array | undefined
        let familyParameterIndex: number[] | undefined
        let familyTypeIndex: number[] | undefined
        
        await Promise.all([
            (async () => { typeName = (await localTable.getStringArray("string:TypeName")) })(),
            (async () => { value = (await localTable.getStringArray("string:Value")) })(),
            (async () => { doubleValue = (await localTable.getNumberArray("double:DoubleValue")) })(),
            (async () => { integerValue = (await localTable.getNumberArray("int:IntegerValue")) })(),
            (async () => { elementIdValue = (await localTable.getBigIntArray("long:ElementIdValue")) })(),
            (async () => { familyParameterIndex = (await localTable.getNumberArray("index:Vim.FamilyRecipeParameter:FamilyParameter")) })(),
            (async () => { familyTypeIndex = (await localTable.getNumberArray("index:Vim.FamilyType:FamilyType")) })(),
        ])
        
        let familyTypeValue: IFamilyTypeValue[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            familyTypeValue.push({
                index: i,
                typeName: typeName ? typeName[i] : undefined,
                value: value ? value[i] : undefined,
                doubleValue: doubleValue ? doubleValue[i] : undefined,
                integerValue: integerValue ? integerValue[i] : undefined,
                elementIdValue: elementIdValue ? elementIdValue[i] : undefined,
                familyParameterIndex: familyParameterIndex ? familyParameterIndex[i] : undefined,
                familyTypeIndex: familyTypeIndex ? familyTypeIndex[i] : undefined
            })
        }
        
        return familyTypeValue
    }
    
    async getTypeName(familyTypeValueIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(familyTypeValueIndex, "string:TypeName"))
    }
    
    async getAllTypeName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:TypeName"))
    }
    
    async getValue(familyTypeValueIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(familyTypeValueIndex, "string:Value"))
    }
    
    async getAllValue(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Value"))
    }
    
    async getDoubleValue(familyTypeValueIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyTypeValueIndex, "double:DoubleValue"))
    }
    
    async getAllDoubleValue(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:DoubleValue"))
    }
    
    async getIntegerValue(familyTypeValueIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyTypeValueIndex, "int:IntegerValue"))
    }
    
    async getAllIntegerValue(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:IntegerValue"))
    }
    
    async getElementIdValue(familyTypeValueIndex: number): Promise<bigint | undefined> {
        return (await this.entityTable.getBigInt(familyTypeValueIndex, "long:ElementIdValue"))
    }
    
    async getAllElementIdValue(): Promise<BigInt64Array | undefined> {
        return (await this.entityTable.getBigIntArray("long:ElementIdValue"))
    }
    
    async getFamilyParameterIndex(familyTypeValueIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(familyTypeValueIndex, "index:Vim.FamilyRecipeParameter:FamilyParameter")
    }
    
    async getAllFamilyParameterIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.FamilyRecipeParameter:FamilyParameter")
    }
    
    async getFamilyParameter(familyTypeValueIndex: number): Promise<IFamilyRecipeParameter | undefined> {
        const index = await this.getFamilyParameterIndex(familyTypeValueIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.familyRecipeParameter?.get(index)
    }
    
    async getFamilyTypeIndex(familyTypeValueIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(familyTypeValueIndex, "index:Vim.FamilyType:FamilyType")
    }
    
    async getAllFamilyTypeIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.FamilyType:FamilyType")
    }
    
    async getFamilyType(familyTypeValueIndex: number): Promise<IFamilyType | undefined> {
        const index = await this.getFamilyTypeIndex(familyTypeValueIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.familyType?.get(index)
    }
    
}

export interface IFamilyReferencePlane {
    index: number
    familyElementId?: bigint
    name?: string
    origin_X?: number
    origin_Y?: number
    origin_Z?: number
    normal_X?: number
    normal_Y?: number
    normal_Z?: number
    isReference?: string
    definesOrigin?: boolean
    isDriven?: boolean
    
    familyIndex?: number
    family?: IFamily
}

export interface IFamilyReferencePlaneTable {
    getCount(): Promise<number>
    get(familyReferencePlaneIndex: number): Promise<IFamilyReferencePlane>
    getAll(): Promise<IFamilyReferencePlane[]>
    
    getFamilyElementId(familyReferencePlaneIndex: number): Promise<bigint | undefined>
    getAllFamilyElementId(): Promise<BigInt64Array | undefined>
    getName(familyReferencePlaneIndex: number): Promise<string | undefined>
    getAllName(): Promise<string[] | undefined>
    getOrigin_X(familyReferencePlaneIndex: number): Promise<number | undefined>
    getAllOrigin_X(): Promise<number[] | undefined>
    getOrigin_Y(familyReferencePlaneIndex: number): Promise<number | undefined>
    getAllOrigin_Y(): Promise<number[] | undefined>
    getOrigin_Z(familyReferencePlaneIndex: number): Promise<number | undefined>
    getAllOrigin_Z(): Promise<number[] | undefined>
    getNormal_X(familyReferencePlaneIndex: number): Promise<number | undefined>
    getAllNormal_X(): Promise<number[] | undefined>
    getNormal_Y(familyReferencePlaneIndex: number): Promise<number | undefined>
    getAllNormal_Y(): Promise<number[] | undefined>
    getNormal_Z(familyReferencePlaneIndex: number): Promise<number | undefined>
    getAllNormal_Z(): Promise<number[] | undefined>
    getIsReference(familyReferencePlaneIndex: number): Promise<string | undefined>
    getAllIsReference(): Promise<string[] | undefined>
    getDefinesOrigin(familyReferencePlaneIndex: number): Promise<boolean | undefined>
    getAllDefinesOrigin(): Promise<boolean[] | undefined>
    getIsDriven(familyReferencePlaneIndex: number): Promise<boolean | undefined>
    getAllIsDriven(): Promise<boolean[] | undefined>
    
    getFamilyIndex(familyReferencePlaneIndex: number): Promise<number | undefined>
    getAllFamilyIndex(): Promise<number[] | undefined>
    getFamily(familyReferencePlaneIndex: number): Promise<IFamily | undefined>
}

export class FamilyReferencePlane implements IFamilyReferencePlane {
    index: number
    familyElementId?: bigint
    name?: string
    origin_X?: number
    origin_Y?: number
    origin_Z?: number
    normal_X?: number
    normal_Y?: number
    normal_Z?: number
    isReference?: string
    definesOrigin?: boolean
    isDriven?: boolean
    
    familyIndex?: number
    family?: IFamily
    
    static async createFromTable(table: IFamilyReferencePlaneTable, index: number): Promise<IFamilyReferencePlane> {
        let result = new FamilyReferencePlane()
        result.index = index
        
        await Promise.all([
            table.getFamilyElementId(index).then(v => result.familyElementId = v),
            table.getName(index).then(v => result.name = v),
            table.getOrigin_X(index).then(v => result.origin_X = v),
            table.getOrigin_Y(index).then(v => result.origin_Y = v),
            table.getOrigin_Z(index).then(v => result.origin_Z = v),
            table.getNormal_X(index).then(v => result.normal_X = v),
            table.getNormal_Y(index).then(v => result.normal_Y = v),
            table.getNormal_Z(index).then(v => result.normal_Z = v),
            table.getIsReference(index).then(v => result.isReference = v),
            table.getDefinesOrigin(index).then(v => result.definesOrigin = v),
            table.getIsDriven(index).then(v => result.isDriven = v),
            table.getFamilyIndex(index).then(v => result.familyIndex = v),
        ])
        
        return result
    }
}

export class FamilyReferencePlaneTable implements IFamilyReferencePlaneTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IFamilyReferencePlaneTable | undefined> {
        const entity = await document.entities.getBfast("Vim.FamilyReferencePlane")
        
        if (!entity) {
            return undefined
        }
        
        let table = new FamilyReferencePlaneTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(familyReferencePlaneIndex: number): Promise<IFamilyReferencePlane> {
        return await FamilyReferencePlane.createFromTable(this, familyReferencePlaneIndex)
    }
    
    async getAll(): Promise<IFamilyReferencePlane[]> {
        const localTable = await this.entityTable.getLocal()
        
        let familyElementId: BigInt64Array | undefined
        let name: string[] | undefined
        let origin_X: number[] | undefined
        let origin_Y: number[] | undefined
        let origin_Z: number[] | undefined
        let normal_X: number[] | undefined
        let normal_Y: number[] | undefined
        let normal_Z: number[] | undefined
        let isReference: string[] | undefined
        let definesOrigin: boolean[] | undefined
        let isDriven: boolean[] | undefined
        let familyIndex: number[] | undefined
        
        await Promise.all([
            (async () => { familyElementId = (await localTable.getBigIntArray("long:FamilyElementId")) })(),
            (async () => { name = (await localTable.getStringArray("string:Name")) })(),
            (async () => { origin_X = (await localTable.getNumberArray("double:Origin.X")) })(),
            (async () => { origin_Y = (await localTable.getNumberArray("double:Origin.Y")) })(),
            (async () => { origin_Z = (await localTable.getNumberArray("double:Origin.Z")) })(),
            (async () => { normal_X = (await localTable.getNumberArray("double:Normal.X")) })(),
            (async () => { normal_Y = (await localTable.getNumberArray("double:Normal.Y")) })(),
            (async () => { normal_Z = (await localTable.getNumberArray("double:Normal.Z")) })(),
            (async () => { isReference = (await localTable.getStringArray("string:IsReference")) })(),
            (async () => { definesOrigin = (await localTable.getBooleanArray("byte:DefinesOrigin")) })(),
            (async () => { isDriven = (await localTable.getBooleanArray("byte:IsDriven")) })(),
            (async () => { familyIndex = (await localTable.getNumberArray("index:Vim.Family:Family")) })(),
        ])
        
        let familyReferencePlane: IFamilyReferencePlane[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            familyReferencePlane.push({
                index: i,
                familyElementId: familyElementId ? familyElementId[i] : undefined,
                name: name ? name[i] : undefined,
                origin_X: origin_X ? origin_X[i] : undefined,
                origin_Y: origin_Y ? origin_Y[i] : undefined,
                origin_Z: origin_Z ? origin_Z[i] : undefined,
                normal_X: normal_X ? normal_X[i] : undefined,
                normal_Y: normal_Y ? normal_Y[i] : undefined,
                normal_Z: normal_Z ? normal_Z[i] : undefined,
                isReference: isReference ? isReference[i] : undefined,
                definesOrigin: definesOrigin ? definesOrigin[i] : undefined,
                isDriven: isDriven ? isDriven[i] : undefined,
                familyIndex: familyIndex ? familyIndex[i] : undefined
            })
        }
        
        return familyReferencePlane
    }
    
    async getFamilyElementId(familyReferencePlaneIndex: number): Promise<bigint | undefined> {
        return (await this.entityTable.getBigInt(familyReferencePlaneIndex, "long:FamilyElementId"))
    }
    
    async getAllFamilyElementId(): Promise<BigInt64Array | undefined> {
        return (await this.entityTable.getBigIntArray("long:FamilyElementId"))
    }
    
    async getName(familyReferencePlaneIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(familyReferencePlaneIndex, "string:Name"))
    }
    
    async getAllName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Name"))
    }
    
    async getOrigin_X(familyReferencePlaneIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyReferencePlaneIndex, "double:Origin.X"))
    }
    
    async getAllOrigin_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Origin.X"))
    }
    
    async getOrigin_Y(familyReferencePlaneIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyReferencePlaneIndex, "double:Origin.Y"))
    }
    
    async getAllOrigin_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Origin.Y"))
    }
    
    async getOrigin_Z(familyReferencePlaneIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyReferencePlaneIndex, "double:Origin.Z"))
    }
    
    async getAllOrigin_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Origin.Z"))
    }
    
    async getNormal_X(familyReferencePlaneIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyReferencePlaneIndex, "double:Normal.X"))
    }
    
    async getAllNormal_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Normal.X"))
    }
    
    async getNormal_Y(familyReferencePlaneIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyReferencePlaneIndex, "double:Normal.Y"))
    }
    
    async getAllNormal_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Normal.Y"))
    }
    
    async getNormal_Z(familyReferencePlaneIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyReferencePlaneIndex, "double:Normal.Z"))
    }
    
    async getAllNormal_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Normal.Z"))
    }
    
    async getIsReference(familyReferencePlaneIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(familyReferencePlaneIndex, "string:IsReference"))
    }
    
    async getAllIsReference(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:IsReference"))
    }
    
    async getDefinesOrigin(familyReferencePlaneIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(familyReferencePlaneIndex, "byte:DefinesOrigin"))
    }
    
    async getAllDefinesOrigin(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:DefinesOrigin"))
    }
    
    async getIsDriven(familyReferencePlaneIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(familyReferencePlaneIndex, "byte:IsDriven"))
    }
    
    async getAllIsDriven(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsDriven"))
    }
    
    async getFamilyIndex(familyReferencePlaneIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(familyReferencePlaneIndex, "index:Vim.Family:Family")
    }
    
    async getAllFamilyIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Family:Family")
    }
    
    async getFamily(familyReferencePlaneIndex: number): Promise<IFamily | undefined> {
        const index = await this.getFamilyIndex(familyReferencePlaneIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.family?.get(index)
    }
    
}

export interface IFamilyForm {
    index: number
    familyElementId?: bigint
    kind?: string
    isSolid?: boolean
    isVisible?: boolean
    subcategory?: string
    material?: string
    planeOrigin_X?: number
    planeOrigin_Y?: number
    planeOrigin_Z?: number
    planeNormal_X?: number
    planeNormal_Y?: number
    planeNormal_Z?: number
    planeXDir_X?: number
    planeXDir_Y?: number
    planeXDir_Z?: number
    startOffset?: number
    endOffset?: number
    startAngle?: number
    endAngle?: number
    isDriven?: boolean
    isRebuildable?: boolean
    
    familyIndex?: number
    family?: IFamily
}

export interface IFamilyFormTable {
    getCount(): Promise<number>
    get(familyFormIndex: number): Promise<IFamilyForm>
    getAll(): Promise<IFamilyForm[]>
    
    getFamilyElementId(familyFormIndex: number): Promise<bigint | undefined>
    getAllFamilyElementId(): Promise<BigInt64Array | undefined>
    getKind(familyFormIndex: number): Promise<string | undefined>
    getAllKind(): Promise<string[] | undefined>
    getIsSolid(familyFormIndex: number): Promise<boolean | undefined>
    getAllIsSolid(): Promise<boolean[] | undefined>
    getIsVisible(familyFormIndex: number): Promise<boolean | undefined>
    getAllIsVisible(): Promise<boolean[] | undefined>
    getSubcategory(familyFormIndex: number): Promise<string | undefined>
    getAllSubcategory(): Promise<string[] | undefined>
    getMaterial(familyFormIndex: number): Promise<string | undefined>
    getAllMaterial(): Promise<string[] | undefined>
    getPlaneOrigin_X(familyFormIndex: number): Promise<number | undefined>
    getAllPlaneOrigin_X(): Promise<number[] | undefined>
    getPlaneOrigin_Y(familyFormIndex: number): Promise<number | undefined>
    getAllPlaneOrigin_Y(): Promise<number[] | undefined>
    getPlaneOrigin_Z(familyFormIndex: number): Promise<number | undefined>
    getAllPlaneOrigin_Z(): Promise<number[] | undefined>
    getPlaneNormal_X(familyFormIndex: number): Promise<number | undefined>
    getAllPlaneNormal_X(): Promise<number[] | undefined>
    getPlaneNormal_Y(familyFormIndex: number): Promise<number | undefined>
    getAllPlaneNormal_Y(): Promise<number[] | undefined>
    getPlaneNormal_Z(familyFormIndex: number): Promise<number | undefined>
    getAllPlaneNormal_Z(): Promise<number[] | undefined>
    getPlaneXDir_X(familyFormIndex: number): Promise<number | undefined>
    getAllPlaneXDir_X(): Promise<number[] | undefined>
    getPlaneXDir_Y(familyFormIndex: number): Promise<number | undefined>
    getAllPlaneXDir_Y(): Promise<number[] | undefined>
    getPlaneXDir_Z(familyFormIndex: number): Promise<number | undefined>
    getAllPlaneXDir_Z(): Promise<number[] | undefined>
    getStartOffset(familyFormIndex: number): Promise<number | undefined>
    getAllStartOffset(): Promise<number[] | undefined>
    getEndOffset(familyFormIndex: number): Promise<number | undefined>
    getAllEndOffset(): Promise<number[] | undefined>
    getStartAngle(familyFormIndex: number): Promise<number | undefined>
    getAllStartAngle(): Promise<number[] | undefined>
    getEndAngle(familyFormIndex: number): Promise<number | undefined>
    getAllEndAngle(): Promise<number[] | undefined>
    getIsDriven(familyFormIndex: number): Promise<boolean | undefined>
    getAllIsDriven(): Promise<boolean[] | undefined>
    getIsRebuildable(familyFormIndex: number): Promise<boolean | undefined>
    getAllIsRebuildable(): Promise<boolean[] | undefined>
    
    getFamilyIndex(familyFormIndex: number): Promise<number | undefined>
    getAllFamilyIndex(): Promise<number[] | undefined>
    getFamily(familyFormIndex: number): Promise<IFamily | undefined>
}

export class FamilyForm implements IFamilyForm {
    index: number
    familyElementId?: bigint
    kind?: string
    isSolid?: boolean
    isVisible?: boolean
    subcategory?: string
    material?: string
    planeOrigin_X?: number
    planeOrigin_Y?: number
    planeOrigin_Z?: number
    planeNormal_X?: number
    planeNormal_Y?: number
    planeNormal_Z?: number
    planeXDir_X?: number
    planeXDir_Y?: number
    planeXDir_Z?: number
    startOffset?: number
    endOffset?: number
    startAngle?: number
    endAngle?: number
    isDriven?: boolean
    isRebuildable?: boolean
    
    familyIndex?: number
    family?: IFamily
    
    static async createFromTable(table: IFamilyFormTable, index: number): Promise<IFamilyForm> {
        let result = new FamilyForm()
        result.index = index
        
        await Promise.all([
            table.getFamilyElementId(index).then(v => result.familyElementId = v),
            table.getKind(index).then(v => result.kind = v),
            table.getIsSolid(index).then(v => result.isSolid = v),
            table.getIsVisible(index).then(v => result.isVisible = v),
            table.getSubcategory(index).then(v => result.subcategory = v),
            table.getMaterial(index).then(v => result.material = v),
            table.getPlaneOrigin_X(index).then(v => result.planeOrigin_X = v),
            table.getPlaneOrigin_Y(index).then(v => result.planeOrigin_Y = v),
            table.getPlaneOrigin_Z(index).then(v => result.planeOrigin_Z = v),
            table.getPlaneNormal_X(index).then(v => result.planeNormal_X = v),
            table.getPlaneNormal_Y(index).then(v => result.planeNormal_Y = v),
            table.getPlaneNormal_Z(index).then(v => result.planeNormal_Z = v),
            table.getPlaneXDir_X(index).then(v => result.planeXDir_X = v),
            table.getPlaneXDir_Y(index).then(v => result.planeXDir_Y = v),
            table.getPlaneXDir_Z(index).then(v => result.planeXDir_Z = v),
            table.getStartOffset(index).then(v => result.startOffset = v),
            table.getEndOffset(index).then(v => result.endOffset = v),
            table.getStartAngle(index).then(v => result.startAngle = v),
            table.getEndAngle(index).then(v => result.endAngle = v),
            table.getIsDriven(index).then(v => result.isDriven = v),
            table.getIsRebuildable(index).then(v => result.isRebuildable = v),
            table.getFamilyIndex(index).then(v => result.familyIndex = v),
        ])
        
        return result
    }
}

export class FamilyFormTable implements IFamilyFormTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IFamilyFormTable | undefined> {
        const entity = await document.entities.getBfast("Vim.FamilyForm")
        
        if (!entity) {
            return undefined
        }
        
        let table = new FamilyFormTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(familyFormIndex: number): Promise<IFamilyForm> {
        return await FamilyForm.createFromTable(this, familyFormIndex)
    }
    
    async getAll(): Promise<IFamilyForm[]> {
        const localTable = await this.entityTable.getLocal()
        
        let familyElementId: BigInt64Array | undefined
        let kind: string[] | undefined
        let isSolid: boolean[] | undefined
        let isVisible: boolean[] | undefined
        let subcategory: string[] | undefined
        let material: string[] | undefined
        let planeOrigin_X: number[] | undefined
        let planeOrigin_Y: number[] | undefined
        let planeOrigin_Z: number[] | undefined
        let planeNormal_X: number[] | undefined
        let planeNormal_Y: number[] | undefined
        let planeNormal_Z: number[] | undefined
        let planeXDir_X: number[] | undefined
        let planeXDir_Y: number[] | undefined
        let planeXDir_Z: number[] | undefined
        let startOffset: number[] | undefined
        let endOffset: number[] | undefined
        let startAngle: number[] | undefined
        let endAngle: number[] | undefined
        let isDriven: boolean[] | undefined
        let isRebuildable: boolean[] | undefined
        let familyIndex: number[] | undefined
        
        await Promise.all([
            (async () => { familyElementId = (await localTable.getBigIntArray("long:FamilyElementId")) })(),
            (async () => { kind = (await localTable.getStringArray("string:Kind")) })(),
            (async () => { isSolid = (await localTable.getBooleanArray("byte:IsSolid")) })(),
            (async () => { isVisible = (await localTable.getBooleanArray("byte:IsVisible")) })(),
            (async () => { subcategory = (await localTable.getStringArray("string:Subcategory")) })(),
            (async () => { material = (await localTable.getStringArray("string:Material")) })(),
            (async () => { planeOrigin_X = (await localTable.getNumberArray("double:PlaneOrigin.X")) })(),
            (async () => { planeOrigin_Y = (await localTable.getNumberArray("double:PlaneOrigin.Y")) })(),
            (async () => { planeOrigin_Z = (await localTable.getNumberArray("double:PlaneOrigin.Z")) })(),
            (async () => { planeNormal_X = (await localTable.getNumberArray("double:PlaneNormal.X")) })(),
            (async () => { planeNormal_Y = (await localTable.getNumberArray("double:PlaneNormal.Y")) })(),
            (async () => { planeNormal_Z = (await localTable.getNumberArray("double:PlaneNormal.Z")) })(),
            (async () => { planeXDir_X = (await localTable.getNumberArray("double:PlaneXDir.X")) })(),
            (async () => { planeXDir_Y = (await localTable.getNumberArray("double:PlaneXDir.Y")) })(),
            (async () => { planeXDir_Z = (await localTable.getNumberArray("double:PlaneXDir.Z")) })(),
            (async () => { startOffset = (await localTable.getNumberArray("double:StartOffset")) })(),
            (async () => { endOffset = (await localTable.getNumberArray("double:EndOffset")) })(),
            (async () => { startAngle = (await localTable.getNumberArray("double:StartAngle")) })(),
            (async () => { endAngle = (await localTable.getNumberArray("double:EndAngle")) })(),
            (async () => { isDriven = (await localTable.getBooleanArray("byte:IsDriven")) })(),
            (async () => { isRebuildable = (await localTable.getBooleanArray("byte:IsRebuildable")) })(),
            (async () => { familyIndex = (await localTable.getNumberArray("index:Vim.Family:Family")) })(),
        ])
        
        let familyForm: IFamilyForm[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            familyForm.push({
                index: i,
                familyElementId: familyElementId ? familyElementId[i] : undefined,
                kind: kind ? kind[i] : undefined,
                isSolid: isSolid ? isSolid[i] : undefined,
                isVisible: isVisible ? isVisible[i] : undefined,
                subcategory: subcategory ? subcategory[i] : undefined,
                material: material ? material[i] : undefined,
                planeOrigin_X: planeOrigin_X ? planeOrigin_X[i] : undefined,
                planeOrigin_Y: planeOrigin_Y ? planeOrigin_Y[i] : undefined,
                planeOrigin_Z: planeOrigin_Z ? planeOrigin_Z[i] : undefined,
                planeNormal_X: planeNormal_X ? planeNormal_X[i] : undefined,
                planeNormal_Y: planeNormal_Y ? planeNormal_Y[i] : undefined,
                planeNormal_Z: planeNormal_Z ? planeNormal_Z[i] : undefined,
                planeXDir_X: planeXDir_X ? planeXDir_X[i] : undefined,
                planeXDir_Y: planeXDir_Y ? planeXDir_Y[i] : undefined,
                planeXDir_Z: planeXDir_Z ? planeXDir_Z[i] : undefined,
                startOffset: startOffset ? startOffset[i] : undefined,
                endOffset: endOffset ? endOffset[i] : undefined,
                startAngle: startAngle ? startAngle[i] : undefined,
                endAngle: endAngle ? endAngle[i] : undefined,
                isDriven: isDriven ? isDriven[i] : undefined,
                isRebuildable: isRebuildable ? isRebuildable[i] : undefined,
                familyIndex: familyIndex ? familyIndex[i] : undefined
            })
        }
        
        return familyForm
    }
    
    async getFamilyElementId(familyFormIndex: number): Promise<bigint | undefined> {
        return (await this.entityTable.getBigInt(familyFormIndex, "long:FamilyElementId"))
    }
    
    async getAllFamilyElementId(): Promise<BigInt64Array | undefined> {
        return (await this.entityTable.getBigIntArray("long:FamilyElementId"))
    }
    
    async getKind(familyFormIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(familyFormIndex, "string:Kind"))
    }
    
    async getAllKind(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Kind"))
    }
    
    async getIsSolid(familyFormIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(familyFormIndex, "byte:IsSolid"))
    }
    
    async getAllIsSolid(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsSolid"))
    }
    
    async getIsVisible(familyFormIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(familyFormIndex, "byte:IsVisible"))
    }
    
    async getAllIsVisible(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsVisible"))
    }
    
    async getSubcategory(familyFormIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(familyFormIndex, "string:Subcategory"))
    }
    
    async getAllSubcategory(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Subcategory"))
    }
    
    async getMaterial(familyFormIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(familyFormIndex, "string:Material"))
    }
    
    async getAllMaterial(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Material"))
    }
    
    async getPlaneOrigin_X(familyFormIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyFormIndex, "double:PlaneOrigin.X"))
    }
    
    async getAllPlaneOrigin_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:PlaneOrigin.X"))
    }
    
    async getPlaneOrigin_Y(familyFormIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyFormIndex, "double:PlaneOrigin.Y"))
    }
    
    async getAllPlaneOrigin_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:PlaneOrigin.Y"))
    }
    
    async getPlaneOrigin_Z(familyFormIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyFormIndex, "double:PlaneOrigin.Z"))
    }
    
    async getAllPlaneOrigin_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:PlaneOrigin.Z"))
    }
    
    async getPlaneNormal_X(familyFormIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyFormIndex, "double:PlaneNormal.X"))
    }
    
    async getAllPlaneNormal_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:PlaneNormal.X"))
    }
    
    async getPlaneNormal_Y(familyFormIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyFormIndex, "double:PlaneNormal.Y"))
    }
    
    async getAllPlaneNormal_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:PlaneNormal.Y"))
    }
    
    async getPlaneNormal_Z(familyFormIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyFormIndex, "double:PlaneNormal.Z"))
    }
    
    async getAllPlaneNormal_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:PlaneNormal.Z"))
    }
    
    async getPlaneXDir_X(familyFormIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyFormIndex, "double:PlaneXDir.X"))
    }
    
    async getAllPlaneXDir_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:PlaneXDir.X"))
    }
    
    async getPlaneXDir_Y(familyFormIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyFormIndex, "double:PlaneXDir.Y"))
    }
    
    async getAllPlaneXDir_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:PlaneXDir.Y"))
    }
    
    async getPlaneXDir_Z(familyFormIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyFormIndex, "double:PlaneXDir.Z"))
    }
    
    async getAllPlaneXDir_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:PlaneXDir.Z"))
    }
    
    async getStartOffset(familyFormIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyFormIndex, "double:StartOffset"))
    }
    
    async getAllStartOffset(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:StartOffset"))
    }
    
    async getEndOffset(familyFormIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyFormIndex, "double:EndOffset"))
    }
    
    async getAllEndOffset(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:EndOffset"))
    }
    
    async getStartAngle(familyFormIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyFormIndex, "double:StartAngle"))
    }
    
    async getAllStartAngle(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:StartAngle"))
    }
    
    async getEndAngle(familyFormIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyFormIndex, "double:EndAngle"))
    }
    
    async getAllEndAngle(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:EndAngle"))
    }
    
    async getIsDriven(familyFormIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(familyFormIndex, "byte:IsDriven"))
    }
    
    async getAllIsDriven(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsDriven"))
    }
    
    async getIsRebuildable(familyFormIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(familyFormIndex, "byte:IsRebuildable"))
    }
    
    async getAllIsRebuildable(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsRebuildable"))
    }
    
    async getFamilyIndex(familyFormIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(familyFormIndex, "index:Vim.Family:Family")
    }
    
    async getAllFamilyIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Family:Family")
    }
    
    async getFamily(familyFormIndex: number): Promise<IFamily | undefined> {
        const index = await this.getFamilyIndex(familyFormIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.family?.get(index)
    }
    
}

export interface IFamilySketchCurve {
    index: number
    role?: number
    loop?: number
    order?: number
    curveElementId?: bigint
    curveKind?: number
    start_X?: number
    start_Y?: number
    start_Z?: number
    end_X?: number
    end_Y?: number
    end_Z?: number
    mid_X?: number
    mid_Y?: number
    mid_Z?: number
    center_X?: number
    center_Y?: number
    center_Z?: number
    radius?: number
    lockCount?: number
    
    familyFormIndex?: number
    familyForm?: IFamilyForm
}

export interface IFamilySketchCurveTable {
    getCount(): Promise<number>
    get(familySketchCurveIndex: number): Promise<IFamilySketchCurve>
    getAll(): Promise<IFamilySketchCurve[]>
    
    getRole(familySketchCurveIndex: number): Promise<number | undefined>
    getAllRole(): Promise<number[] | undefined>
    getLoop(familySketchCurveIndex: number): Promise<number | undefined>
    getAllLoop(): Promise<number[] | undefined>
    getOrder(familySketchCurveIndex: number): Promise<number | undefined>
    getAllOrder(): Promise<number[] | undefined>
    getCurveElementId(familySketchCurveIndex: number): Promise<bigint | undefined>
    getAllCurveElementId(): Promise<BigInt64Array | undefined>
    getCurveKind(familySketchCurveIndex: number): Promise<number | undefined>
    getAllCurveKind(): Promise<number[] | undefined>
    getStart_X(familySketchCurveIndex: number): Promise<number | undefined>
    getAllStart_X(): Promise<number[] | undefined>
    getStart_Y(familySketchCurveIndex: number): Promise<number | undefined>
    getAllStart_Y(): Promise<number[] | undefined>
    getStart_Z(familySketchCurveIndex: number): Promise<number | undefined>
    getAllStart_Z(): Promise<number[] | undefined>
    getEnd_X(familySketchCurveIndex: number): Promise<number | undefined>
    getAllEnd_X(): Promise<number[] | undefined>
    getEnd_Y(familySketchCurveIndex: number): Promise<number | undefined>
    getAllEnd_Y(): Promise<number[] | undefined>
    getEnd_Z(familySketchCurveIndex: number): Promise<number | undefined>
    getAllEnd_Z(): Promise<number[] | undefined>
    getMid_X(familySketchCurveIndex: number): Promise<number | undefined>
    getAllMid_X(): Promise<number[] | undefined>
    getMid_Y(familySketchCurveIndex: number): Promise<number | undefined>
    getAllMid_Y(): Promise<number[] | undefined>
    getMid_Z(familySketchCurveIndex: number): Promise<number | undefined>
    getAllMid_Z(): Promise<number[] | undefined>
    getCenter_X(familySketchCurveIndex: number): Promise<number | undefined>
    getAllCenter_X(): Promise<number[] | undefined>
    getCenter_Y(familySketchCurveIndex: number): Promise<number | undefined>
    getAllCenter_Y(): Promise<number[] | undefined>
    getCenter_Z(familySketchCurveIndex: number): Promise<number | undefined>
    getAllCenter_Z(): Promise<number[] | undefined>
    getRadius(familySketchCurveIndex: number): Promise<number | undefined>
    getAllRadius(): Promise<number[] | undefined>
    getLockCount(familySketchCurveIndex: number): Promise<number | undefined>
    getAllLockCount(): Promise<number[] | undefined>
    
    getFamilyFormIndex(familySketchCurveIndex: number): Promise<number | undefined>
    getAllFamilyFormIndex(): Promise<number[] | undefined>
    getFamilyForm(familySketchCurveIndex: number): Promise<IFamilyForm | undefined>
}

export class FamilySketchCurve implements IFamilySketchCurve {
    index: number
    role?: number
    loop?: number
    order?: number
    curveElementId?: bigint
    curveKind?: number
    start_X?: number
    start_Y?: number
    start_Z?: number
    end_X?: number
    end_Y?: number
    end_Z?: number
    mid_X?: number
    mid_Y?: number
    mid_Z?: number
    center_X?: number
    center_Y?: number
    center_Z?: number
    radius?: number
    lockCount?: number
    
    familyFormIndex?: number
    familyForm?: IFamilyForm
    
    static async createFromTable(table: IFamilySketchCurveTable, index: number): Promise<IFamilySketchCurve> {
        let result = new FamilySketchCurve()
        result.index = index
        
        await Promise.all([
            table.getRole(index).then(v => result.role = v),
            table.getLoop(index).then(v => result.loop = v),
            table.getOrder(index).then(v => result.order = v),
            table.getCurveElementId(index).then(v => result.curveElementId = v),
            table.getCurveKind(index).then(v => result.curveKind = v),
            table.getStart_X(index).then(v => result.start_X = v),
            table.getStart_Y(index).then(v => result.start_Y = v),
            table.getStart_Z(index).then(v => result.start_Z = v),
            table.getEnd_X(index).then(v => result.end_X = v),
            table.getEnd_Y(index).then(v => result.end_Y = v),
            table.getEnd_Z(index).then(v => result.end_Z = v),
            table.getMid_X(index).then(v => result.mid_X = v),
            table.getMid_Y(index).then(v => result.mid_Y = v),
            table.getMid_Z(index).then(v => result.mid_Z = v),
            table.getCenter_X(index).then(v => result.center_X = v),
            table.getCenter_Y(index).then(v => result.center_Y = v),
            table.getCenter_Z(index).then(v => result.center_Z = v),
            table.getRadius(index).then(v => result.radius = v),
            table.getLockCount(index).then(v => result.lockCount = v),
            table.getFamilyFormIndex(index).then(v => result.familyFormIndex = v),
        ])
        
        return result
    }
}

export class FamilySketchCurveTable implements IFamilySketchCurveTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IFamilySketchCurveTable | undefined> {
        const entity = await document.entities.getBfast("Vim.FamilySketchCurve")
        
        if (!entity) {
            return undefined
        }
        
        let table = new FamilySketchCurveTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(familySketchCurveIndex: number): Promise<IFamilySketchCurve> {
        return await FamilySketchCurve.createFromTable(this, familySketchCurveIndex)
    }
    
    async getAll(): Promise<IFamilySketchCurve[]> {
        const localTable = await this.entityTable.getLocal()
        
        let role: number[] | undefined
        let loop: number[] | undefined
        let order: number[] | undefined
        let curveElementId: BigInt64Array | undefined
        let curveKind: number[] | undefined
        let start_X: number[] | undefined
        let start_Y: number[] | undefined
        let start_Z: number[] | undefined
        let end_X: number[] | undefined
        let end_Y: number[] | undefined
        let end_Z: number[] | undefined
        let mid_X: number[] | undefined
        let mid_Y: number[] | undefined
        let mid_Z: number[] | undefined
        let center_X: number[] | undefined
        let center_Y: number[] | undefined
        let center_Z: number[] | undefined
        let radius: number[] | undefined
        let lockCount: number[] | undefined
        let familyFormIndex: number[] | undefined
        
        await Promise.all([
            (async () => { role = (await localTable.getNumberArray("int:Role")) })(),
            (async () => { loop = (await localTable.getNumberArray("int:Loop")) })(),
            (async () => { order = (await localTable.getNumberArray("int:Order")) })(),
            (async () => { curveElementId = (await localTable.getBigIntArray("long:CurveElementId")) })(),
            (async () => { curveKind = (await localTable.getNumberArray("int:CurveKind")) })(),
            (async () => { start_X = (await localTable.getNumberArray("double:Start.X")) })(),
            (async () => { start_Y = (await localTable.getNumberArray("double:Start.Y")) })(),
            (async () => { start_Z = (await localTable.getNumberArray("double:Start.Z")) })(),
            (async () => { end_X = (await localTable.getNumberArray("double:End.X")) })(),
            (async () => { end_Y = (await localTable.getNumberArray("double:End.Y")) })(),
            (async () => { end_Z = (await localTable.getNumberArray("double:End.Z")) })(),
            (async () => { mid_X = (await localTable.getNumberArray("double:Mid.X")) })(),
            (async () => { mid_Y = (await localTable.getNumberArray("double:Mid.Y")) })(),
            (async () => { mid_Z = (await localTable.getNumberArray("double:Mid.Z")) })(),
            (async () => { center_X = (await localTable.getNumberArray("double:Center.X")) })(),
            (async () => { center_Y = (await localTable.getNumberArray("double:Center.Y")) })(),
            (async () => { center_Z = (await localTable.getNumberArray("double:Center.Z")) })(),
            (async () => { radius = (await localTable.getNumberArray("double:Radius")) })(),
            (async () => { lockCount = (await localTable.getNumberArray("int:LockCount")) })(),
            (async () => { familyFormIndex = (await localTable.getNumberArray("index:Vim.FamilyForm:FamilyForm")) })(),
        ])
        
        let familySketchCurve: IFamilySketchCurve[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            familySketchCurve.push({
                index: i,
                role: role ? role[i] : undefined,
                loop: loop ? loop[i] : undefined,
                order: order ? order[i] : undefined,
                curveElementId: curveElementId ? curveElementId[i] : undefined,
                curveKind: curveKind ? curveKind[i] : undefined,
                start_X: start_X ? start_X[i] : undefined,
                start_Y: start_Y ? start_Y[i] : undefined,
                start_Z: start_Z ? start_Z[i] : undefined,
                end_X: end_X ? end_X[i] : undefined,
                end_Y: end_Y ? end_Y[i] : undefined,
                end_Z: end_Z ? end_Z[i] : undefined,
                mid_X: mid_X ? mid_X[i] : undefined,
                mid_Y: mid_Y ? mid_Y[i] : undefined,
                mid_Z: mid_Z ? mid_Z[i] : undefined,
                center_X: center_X ? center_X[i] : undefined,
                center_Y: center_Y ? center_Y[i] : undefined,
                center_Z: center_Z ? center_Z[i] : undefined,
                radius: radius ? radius[i] : undefined,
                lockCount: lockCount ? lockCount[i] : undefined,
                familyFormIndex: familyFormIndex ? familyFormIndex[i] : undefined
            })
        }
        
        return familySketchCurve
    }
    
    async getRole(familySketchCurveIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familySketchCurveIndex, "int:Role"))
    }
    
    async getAllRole(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Role"))
    }
    
    async getLoop(familySketchCurveIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familySketchCurveIndex, "int:Loop"))
    }
    
    async getAllLoop(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Loop"))
    }
    
    async getOrder(familySketchCurveIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familySketchCurveIndex, "int:Order"))
    }
    
    async getAllOrder(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Order"))
    }
    
    async getCurveElementId(familySketchCurveIndex: number): Promise<bigint | undefined> {
        return (await this.entityTable.getBigInt(familySketchCurveIndex, "long:CurveElementId"))
    }
    
    async getAllCurveElementId(): Promise<BigInt64Array | undefined> {
        return (await this.entityTable.getBigIntArray("long:CurveElementId"))
    }
    
    async getCurveKind(familySketchCurveIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familySketchCurveIndex, "int:CurveKind"))
    }
    
    async getAllCurveKind(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:CurveKind"))
    }
    
    async getStart_X(familySketchCurveIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familySketchCurveIndex, "double:Start.X"))
    }
    
    async getAllStart_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Start.X"))
    }
    
    async getStart_Y(familySketchCurveIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familySketchCurveIndex, "double:Start.Y"))
    }
    
    async getAllStart_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Start.Y"))
    }
    
    async getStart_Z(familySketchCurveIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familySketchCurveIndex, "double:Start.Z"))
    }
    
    async getAllStart_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Start.Z"))
    }
    
    async getEnd_X(familySketchCurveIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familySketchCurveIndex, "double:End.X"))
    }
    
    async getAllEnd_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:End.X"))
    }
    
    async getEnd_Y(familySketchCurveIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familySketchCurveIndex, "double:End.Y"))
    }
    
    async getAllEnd_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:End.Y"))
    }
    
    async getEnd_Z(familySketchCurveIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familySketchCurveIndex, "double:End.Z"))
    }
    
    async getAllEnd_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:End.Z"))
    }
    
    async getMid_X(familySketchCurveIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familySketchCurveIndex, "double:Mid.X"))
    }
    
    async getAllMid_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Mid.X"))
    }
    
    async getMid_Y(familySketchCurveIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familySketchCurveIndex, "double:Mid.Y"))
    }
    
    async getAllMid_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Mid.Y"))
    }
    
    async getMid_Z(familySketchCurveIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familySketchCurveIndex, "double:Mid.Z"))
    }
    
    async getAllMid_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Mid.Z"))
    }
    
    async getCenter_X(familySketchCurveIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familySketchCurveIndex, "double:Center.X"))
    }
    
    async getAllCenter_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Center.X"))
    }
    
    async getCenter_Y(familySketchCurveIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familySketchCurveIndex, "double:Center.Y"))
    }
    
    async getAllCenter_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Center.Y"))
    }
    
    async getCenter_Z(familySketchCurveIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familySketchCurveIndex, "double:Center.Z"))
    }
    
    async getAllCenter_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Center.Z"))
    }
    
    async getRadius(familySketchCurveIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familySketchCurveIndex, "double:Radius"))
    }
    
    async getAllRadius(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Radius"))
    }
    
    async getLockCount(familySketchCurveIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familySketchCurveIndex, "int:LockCount"))
    }
    
    async getAllLockCount(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:LockCount"))
    }
    
    async getFamilyFormIndex(familySketchCurveIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(familySketchCurveIndex, "index:Vim.FamilyForm:FamilyForm")
    }
    
    async getAllFamilyFormIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.FamilyForm:FamilyForm")
    }
    
    async getFamilyForm(familySketchCurveIndex: number): Promise<IFamilyForm | undefined> {
        const index = await this.getFamilyFormIndex(familySketchCurveIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.familyForm?.get(index)
    }
    
}

export interface IFamilyConstraint {
    index: number
    familyElementId?: bigint
    kind?: number
    isLocked?: boolean
    hasView?: boolean
    value?: number
    segmentCount?: number
    reachesGeometry?: boolean
    referenceCount?: number
    
    familyIndex?: number
    family?: IFamily
    familyParameterIndex?: number
    familyParameter?: IFamilyRecipeParameter
}

export interface IFamilyConstraintTable {
    getCount(): Promise<number>
    get(familyConstraintIndex: number): Promise<IFamilyConstraint>
    getAll(): Promise<IFamilyConstraint[]>
    
    getFamilyElementId(familyConstraintIndex: number): Promise<bigint | undefined>
    getAllFamilyElementId(): Promise<BigInt64Array | undefined>
    getKind(familyConstraintIndex: number): Promise<number | undefined>
    getAllKind(): Promise<number[] | undefined>
    getIsLocked(familyConstraintIndex: number): Promise<boolean | undefined>
    getAllIsLocked(): Promise<boolean[] | undefined>
    getHasView(familyConstraintIndex: number): Promise<boolean | undefined>
    getAllHasView(): Promise<boolean[] | undefined>
    getValue(familyConstraintIndex: number): Promise<number | undefined>
    getAllValue(): Promise<number[] | undefined>
    getSegmentCount(familyConstraintIndex: number): Promise<number | undefined>
    getAllSegmentCount(): Promise<number[] | undefined>
    getReachesGeometry(familyConstraintIndex: number): Promise<boolean | undefined>
    getAllReachesGeometry(): Promise<boolean[] | undefined>
    getReferenceCount(familyConstraintIndex: number): Promise<number | undefined>
    getAllReferenceCount(): Promise<number[] | undefined>
    
    getFamilyIndex(familyConstraintIndex: number): Promise<number | undefined>
    getAllFamilyIndex(): Promise<number[] | undefined>
    getFamily(familyConstraintIndex: number): Promise<IFamily | undefined>
    getFamilyParameterIndex(familyConstraintIndex: number): Promise<number | undefined>
    getAllFamilyParameterIndex(): Promise<number[] | undefined>
    getFamilyParameter(familyConstraintIndex: number): Promise<IFamilyRecipeParameter | undefined>
}

export class FamilyConstraint implements IFamilyConstraint {
    index: number
    familyElementId?: bigint
    kind?: number
    isLocked?: boolean
    hasView?: boolean
    value?: number
    segmentCount?: number
    reachesGeometry?: boolean
    referenceCount?: number
    
    familyIndex?: number
    family?: IFamily
    familyParameterIndex?: number
    familyParameter?: IFamilyRecipeParameter
    
    static async createFromTable(table: IFamilyConstraintTable, index: number): Promise<IFamilyConstraint> {
        let result = new FamilyConstraint()
        result.index = index
        
        await Promise.all([
            table.getFamilyElementId(index).then(v => result.familyElementId = v),
            table.getKind(index).then(v => result.kind = v),
            table.getIsLocked(index).then(v => result.isLocked = v),
            table.getHasView(index).then(v => result.hasView = v),
            table.getValue(index).then(v => result.value = v),
            table.getSegmentCount(index).then(v => result.segmentCount = v),
            table.getReachesGeometry(index).then(v => result.reachesGeometry = v),
            table.getReferenceCount(index).then(v => result.referenceCount = v),
            table.getFamilyIndex(index).then(v => result.familyIndex = v),
            table.getFamilyParameterIndex(index).then(v => result.familyParameterIndex = v),
        ])
        
        return result
    }
}

export class FamilyConstraintTable implements IFamilyConstraintTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IFamilyConstraintTable | undefined> {
        const entity = await document.entities.getBfast("Vim.FamilyConstraint")
        
        if (!entity) {
            return undefined
        }
        
        let table = new FamilyConstraintTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(familyConstraintIndex: number): Promise<IFamilyConstraint> {
        return await FamilyConstraint.createFromTable(this, familyConstraintIndex)
    }
    
    async getAll(): Promise<IFamilyConstraint[]> {
        const localTable = await this.entityTable.getLocal()
        
        let familyElementId: BigInt64Array | undefined
        let kind: number[] | undefined
        let isLocked: boolean[] | undefined
        let hasView: boolean[] | undefined
        let value: number[] | undefined
        let segmentCount: number[] | undefined
        let reachesGeometry: boolean[] | undefined
        let referenceCount: number[] | undefined
        let familyIndex: number[] | undefined
        let familyParameterIndex: number[] | undefined
        
        await Promise.all([
            (async () => { familyElementId = (await localTable.getBigIntArray("long:FamilyElementId")) })(),
            (async () => { kind = (await localTable.getNumberArray("int:Kind")) })(),
            (async () => { isLocked = (await localTable.getBooleanArray("byte:IsLocked")) })(),
            (async () => { hasView = (await localTable.getBooleanArray("byte:HasView")) })(),
            (async () => { value = (await localTable.getNumberArray("double:Value")) })(),
            (async () => { segmentCount = (await localTable.getNumberArray("int:SegmentCount")) })(),
            (async () => { reachesGeometry = (await localTable.getBooleanArray("byte:ReachesGeometry")) })(),
            (async () => { referenceCount = (await localTable.getNumberArray("int:ReferenceCount")) })(),
            (async () => { familyIndex = (await localTable.getNumberArray("index:Vim.Family:Family")) })(),
            (async () => { familyParameterIndex = (await localTable.getNumberArray("index:Vim.FamilyRecipeParameter:FamilyParameter")) })(),
        ])
        
        let familyConstraint: IFamilyConstraint[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            familyConstraint.push({
                index: i,
                familyElementId: familyElementId ? familyElementId[i] : undefined,
                kind: kind ? kind[i] : undefined,
                isLocked: isLocked ? isLocked[i] : undefined,
                hasView: hasView ? hasView[i] : undefined,
                value: value ? value[i] : undefined,
                segmentCount: segmentCount ? segmentCount[i] : undefined,
                reachesGeometry: reachesGeometry ? reachesGeometry[i] : undefined,
                referenceCount: referenceCount ? referenceCount[i] : undefined,
                familyIndex: familyIndex ? familyIndex[i] : undefined,
                familyParameterIndex: familyParameterIndex ? familyParameterIndex[i] : undefined
            })
        }
        
        return familyConstraint
    }
    
    async getFamilyElementId(familyConstraintIndex: number): Promise<bigint | undefined> {
        return (await this.entityTable.getBigInt(familyConstraintIndex, "long:FamilyElementId"))
    }
    
    async getAllFamilyElementId(): Promise<BigInt64Array | undefined> {
        return (await this.entityTable.getBigIntArray("long:FamilyElementId"))
    }
    
    async getKind(familyConstraintIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyConstraintIndex, "int:Kind"))
    }
    
    async getAllKind(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Kind"))
    }
    
    async getIsLocked(familyConstraintIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(familyConstraintIndex, "byte:IsLocked"))
    }
    
    async getAllIsLocked(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsLocked"))
    }
    
    async getHasView(familyConstraintIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(familyConstraintIndex, "byte:HasView"))
    }
    
    async getAllHasView(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:HasView"))
    }
    
    async getValue(familyConstraintIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyConstraintIndex, "double:Value"))
    }
    
    async getAllValue(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Value"))
    }
    
    async getSegmentCount(familyConstraintIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyConstraintIndex, "int:SegmentCount"))
    }
    
    async getAllSegmentCount(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:SegmentCount"))
    }
    
    async getReachesGeometry(familyConstraintIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(familyConstraintIndex, "byte:ReachesGeometry"))
    }
    
    async getAllReachesGeometry(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:ReachesGeometry"))
    }
    
    async getReferenceCount(familyConstraintIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyConstraintIndex, "int:ReferenceCount"))
    }
    
    async getAllReferenceCount(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:ReferenceCount"))
    }
    
    async getFamilyIndex(familyConstraintIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(familyConstraintIndex, "index:Vim.Family:Family")
    }
    
    async getAllFamilyIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Family:Family")
    }
    
    async getFamily(familyConstraintIndex: number): Promise<IFamily | undefined> {
        const index = await this.getFamilyIndex(familyConstraintIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.family?.get(index)
    }
    
    async getFamilyParameterIndex(familyConstraintIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(familyConstraintIndex, "index:Vim.FamilyRecipeParameter:FamilyParameter")
    }
    
    async getAllFamilyParameterIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.FamilyRecipeParameter:FamilyParameter")
    }
    
    async getFamilyParameter(familyConstraintIndex: number): Promise<IFamilyRecipeParameter | undefined> {
        const index = await this.getFamilyParameterIndex(familyConstraintIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.familyRecipeParameter?.get(index)
    }
    
}

export interface IFamilyConstraintReference {
    index: number
    order?: number
    refKind?: number
    refElementId?: bigint
    refName?: string
    referenceType?: string
    stable?: string
    
    familyConstraintIndex?: number
    familyConstraint?: IFamilyConstraint
}

export interface IFamilyConstraintReferenceTable {
    getCount(): Promise<number>
    get(familyConstraintReferenceIndex: number): Promise<IFamilyConstraintReference>
    getAll(): Promise<IFamilyConstraintReference[]>
    
    getOrder(familyConstraintReferenceIndex: number): Promise<number | undefined>
    getAllOrder(): Promise<number[] | undefined>
    getRefKind(familyConstraintReferenceIndex: number): Promise<number | undefined>
    getAllRefKind(): Promise<number[] | undefined>
    getRefElementId(familyConstraintReferenceIndex: number): Promise<bigint | undefined>
    getAllRefElementId(): Promise<BigInt64Array | undefined>
    getRefName(familyConstraintReferenceIndex: number): Promise<string | undefined>
    getAllRefName(): Promise<string[] | undefined>
    getReferenceType(familyConstraintReferenceIndex: number): Promise<string | undefined>
    getAllReferenceType(): Promise<string[] | undefined>
    getStable(familyConstraintReferenceIndex: number): Promise<string | undefined>
    getAllStable(): Promise<string[] | undefined>
    
    getFamilyConstraintIndex(familyConstraintReferenceIndex: number): Promise<number | undefined>
    getAllFamilyConstraintIndex(): Promise<number[] | undefined>
    getFamilyConstraint(familyConstraintReferenceIndex: number): Promise<IFamilyConstraint | undefined>
}

export class FamilyConstraintReference implements IFamilyConstraintReference {
    index: number
    order?: number
    refKind?: number
    refElementId?: bigint
    refName?: string
    referenceType?: string
    stable?: string
    
    familyConstraintIndex?: number
    familyConstraint?: IFamilyConstraint
    
    static async createFromTable(table: IFamilyConstraintReferenceTable, index: number): Promise<IFamilyConstraintReference> {
        let result = new FamilyConstraintReference()
        result.index = index
        
        await Promise.all([
            table.getOrder(index).then(v => result.order = v),
            table.getRefKind(index).then(v => result.refKind = v),
            table.getRefElementId(index).then(v => result.refElementId = v),
            table.getRefName(index).then(v => result.refName = v),
            table.getReferenceType(index).then(v => result.referenceType = v),
            table.getStable(index).then(v => result.stable = v),
            table.getFamilyConstraintIndex(index).then(v => result.familyConstraintIndex = v),
        ])
        
        return result
    }
}

export class FamilyConstraintReferenceTable implements IFamilyConstraintReferenceTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IFamilyConstraintReferenceTable | undefined> {
        const entity = await document.entities.getBfast("Vim.FamilyConstraintReference")
        
        if (!entity) {
            return undefined
        }
        
        let table = new FamilyConstraintReferenceTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(familyConstraintReferenceIndex: number): Promise<IFamilyConstraintReference> {
        return await FamilyConstraintReference.createFromTable(this, familyConstraintReferenceIndex)
    }
    
    async getAll(): Promise<IFamilyConstraintReference[]> {
        const localTable = await this.entityTable.getLocal()
        
        let order: number[] | undefined
        let refKind: number[] | undefined
        let refElementId: BigInt64Array | undefined
        let refName: string[] | undefined
        let referenceType: string[] | undefined
        let stable: string[] | undefined
        let familyConstraintIndex: number[] | undefined
        
        await Promise.all([
            (async () => { order = (await localTable.getNumberArray("int:Order")) })(),
            (async () => { refKind = (await localTable.getNumberArray("int:RefKind")) })(),
            (async () => { refElementId = (await localTable.getBigIntArray("long:RefElementId")) })(),
            (async () => { refName = (await localTable.getStringArray("string:RefName")) })(),
            (async () => { referenceType = (await localTable.getStringArray("string:ReferenceType")) })(),
            (async () => { stable = (await localTable.getStringArray("string:Stable")) })(),
            (async () => { familyConstraintIndex = (await localTable.getNumberArray("index:Vim.FamilyConstraint:FamilyConstraint")) })(),
        ])
        
        let familyConstraintReference: IFamilyConstraintReference[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            familyConstraintReference.push({
                index: i,
                order: order ? order[i] : undefined,
                refKind: refKind ? refKind[i] : undefined,
                refElementId: refElementId ? refElementId[i] : undefined,
                refName: refName ? refName[i] : undefined,
                referenceType: referenceType ? referenceType[i] : undefined,
                stable: stable ? stable[i] : undefined,
                familyConstraintIndex: familyConstraintIndex ? familyConstraintIndex[i] : undefined
            })
        }
        
        return familyConstraintReference
    }
    
    async getOrder(familyConstraintReferenceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyConstraintReferenceIndex, "int:Order"))
    }
    
    async getAllOrder(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Order"))
    }
    
    async getRefKind(familyConstraintReferenceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyConstraintReferenceIndex, "int:RefKind"))
    }
    
    async getAllRefKind(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:RefKind"))
    }
    
    async getRefElementId(familyConstraintReferenceIndex: number): Promise<bigint | undefined> {
        return (await this.entityTable.getBigInt(familyConstraintReferenceIndex, "long:RefElementId"))
    }
    
    async getAllRefElementId(): Promise<BigInt64Array | undefined> {
        return (await this.entityTable.getBigIntArray("long:RefElementId"))
    }
    
    async getRefName(familyConstraintReferenceIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(familyConstraintReferenceIndex, "string:RefName"))
    }
    
    async getAllRefName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:RefName"))
    }
    
    async getReferenceType(familyConstraintReferenceIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(familyConstraintReferenceIndex, "string:ReferenceType"))
    }
    
    async getAllReferenceType(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:ReferenceType"))
    }
    
    async getStable(familyConstraintReferenceIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(familyConstraintReferenceIndex, "string:Stable"))
    }
    
    async getAllStable(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Stable"))
    }
    
    async getFamilyConstraintIndex(familyConstraintReferenceIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(familyConstraintReferenceIndex, "index:Vim.FamilyConstraint:FamilyConstraint")
    }
    
    async getAllFamilyConstraintIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.FamilyConstraint:FamilyConstraint")
    }
    
    async getFamilyConstraint(familyConstraintReferenceIndex: number): Promise<IFamilyConstraint | undefined> {
        const index = await this.getFamilyConstraintIndex(familyConstraintReferenceIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.familyConstraint?.get(index)
    }
    
}

export interface IFamilyFormDriver {
    index: number
    via?: number
    
    familyFormIndex?: number
    familyForm?: IFamilyForm
    familyParameterIndex?: number
    familyParameter?: IFamilyRecipeParameter
}

export interface IFamilyFormDriverTable {
    getCount(): Promise<number>
    get(familyFormDriverIndex: number): Promise<IFamilyFormDriver>
    getAll(): Promise<IFamilyFormDriver[]>
    
    getVia(familyFormDriverIndex: number): Promise<number | undefined>
    getAllVia(): Promise<number[] | undefined>
    
    getFamilyFormIndex(familyFormDriverIndex: number): Promise<number | undefined>
    getAllFamilyFormIndex(): Promise<number[] | undefined>
    getFamilyForm(familyFormDriverIndex: number): Promise<IFamilyForm | undefined>
    getFamilyParameterIndex(familyFormDriverIndex: number): Promise<number | undefined>
    getAllFamilyParameterIndex(): Promise<number[] | undefined>
    getFamilyParameter(familyFormDriverIndex: number): Promise<IFamilyRecipeParameter | undefined>
}

export class FamilyFormDriver implements IFamilyFormDriver {
    index: number
    via?: number
    
    familyFormIndex?: number
    familyForm?: IFamilyForm
    familyParameterIndex?: number
    familyParameter?: IFamilyRecipeParameter
    
    static async createFromTable(table: IFamilyFormDriverTable, index: number): Promise<IFamilyFormDriver> {
        let result = new FamilyFormDriver()
        result.index = index
        
        await Promise.all([
            table.getVia(index).then(v => result.via = v),
            table.getFamilyFormIndex(index).then(v => result.familyFormIndex = v),
            table.getFamilyParameterIndex(index).then(v => result.familyParameterIndex = v),
        ])
        
        return result
    }
}

export class FamilyFormDriverTable implements IFamilyFormDriverTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IFamilyFormDriverTable | undefined> {
        const entity = await document.entities.getBfast("Vim.FamilyFormDriver")
        
        if (!entity) {
            return undefined
        }
        
        let table = new FamilyFormDriverTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(familyFormDriverIndex: number): Promise<IFamilyFormDriver> {
        return await FamilyFormDriver.createFromTable(this, familyFormDriverIndex)
    }
    
    async getAll(): Promise<IFamilyFormDriver[]> {
        const localTable = await this.entityTable.getLocal()
        
        let via: number[] | undefined
        let familyFormIndex: number[] | undefined
        let familyParameterIndex: number[] | undefined
        
        await Promise.all([
            (async () => { via = (await localTable.getNumberArray("int:Via")) })(),
            (async () => { familyFormIndex = (await localTable.getNumberArray("index:Vim.FamilyForm:FamilyForm")) })(),
            (async () => { familyParameterIndex = (await localTable.getNumberArray("index:Vim.FamilyRecipeParameter:FamilyParameter")) })(),
        ])
        
        let familyFormDriver: IFamilyFormDriver[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            familyFormDriver.push({
                index: i,
                via: via ? via[i] : undefined,
                familyFormIndex: familyFormIndex ? familyFormIndex[i] : undefined,
                familyParameterIndex: familyParameterIndex ? familyParameterIndex[i] : undefined
            })
        }
        
        return familyFormDriver
    }
    
    async getVia(familyFormDriverIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(familyFormDriverIndex, "int:Via"))
    }
    
    async getAllVia(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Via"))
    }
    
    async getFamilyFormIndex(familyFormDriverIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(familyFormDriverIndex, "index:Vim.FamilyForm:FamilyForm")
    }
    
    async getAllFamilyFormIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.FamilyForm:FamilyForm")
    }
    
    async getFamilyForm(familyFormDriverIndex: number): Promise<IFamilyForm | undefined> {
        const index = await this.getFamilyFormIndex(familyFormDriverIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.familyForm?.get(index)
    }
    
    async getFamilyParameterIndex(familyFormDriverIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(familyFormDriverIndex, "index:Vim.FamilyRecipeParameter:FamilyParameter")
    }
    
    async getAllFamilyParameterIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.FamilyRecipeParameter:FamilyParameter")
    }
    
    async getFamilyParameter(familyFormDriverIndex: number): Promise<IFamilyRecipeParameter | undefined> {
        const index = await this.getFamilyParameterIndex(familyFormDriverIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.familyRecipeParameter?.get(index)
    }
    
}

export interface IGeometry {
    index: number
    box_Min_X?: number
    box_Min_Y?: number
    box_Min_Z?: number
    box_Max_X?: number
    box_Max_Y?: number
    box_Max_Z?: number
    vertexCount?: number
    faceCount?: number
}

export interface IGeometryTable {
    getCount(): Promise<number>
    get(geometryIndex: number): Promise<IGeometry>
    getAll(): Promise<IGeometry[]>
    
    getBox_Min_X(geometryIndex: number): Promise<number | undefined>
    getAllBox_Min_X(): Promise<number[] | undefined>
    getBox_Min_Y(geometryIndex: number): Promise<number | undefined>
    getAllBox_Min_Y(): Promise<number[] | undefined>
    getBox_Min_Z(geometryIndex: number): Promise<number | undefined>
    getAllBox_Min_Z(): Promise<number[] | undefined>
    getBox_Max_X(geometryIndex: number): Promise<number | undefined>
    getAllBox_Max_X(): Promise<number[] | undefined>
    getBox_Max_Y(geometryIndex: number): Promise<number | undefined>
    getAllBox_Max_Y(): Promise<number[] | undefined>
    getBox_Max_Z(geometryIndex: number): Promise<number | undefined>
    getAllBox_Max_Z(): Promise<number[] | undefined>
    getVertexCount(geometryIndex: number): Promise<number | undefined>
    getAllVertexCount(): Promise<number[] | undefined>
    getFaceCount(geometryIndex: number): Promise<number | undefined>
    getAllFaceCount(): Promise<number[] | undefined>
}

export class Geometry implements IGeometry {
    index: number
    box_Min_X?: number
    box_Min_Y?: number
    box_Min_Z?: number
    box_Max_X?: number
    box_Max_Y?: number
    box_Max_Z?: number
    vertexCount?: number
    faceCount?: number
    
    static async createFromTable(table: IGeometryTable, index: number): Promise<IGeometry> {
        let result = new Geometry()
        result.index = index
        
        await Promise.all([
            table.getBox_Min_X(index).then(v => result.box_Min_X = v),
            table.getBox_Min_Y(index).then(v => result.box_Min_Y = v),
            table.getBox_Min_Z(index).then(v => result.box_Min_Z = v),
            table.getBox_Max_X(index).then(v => result.box_Max_X = v),
            table.getBox_Max_Y(index).then(v => result.box_Max_Y = v),
            table.getBox_Max_Z(index).then(v => result.box_Max_Z = v),
            table.getVertexCount(index).then(v => result.vertexCount = v),
            table.getFaceCount(index).then(v => result.faceCount = v),
        ])
        
        return result
    }
}

export class GeometryTable implements IGeometryTable {
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IGeometryTable | undefined> {
        const entity = await document.entities.getBfast("Vim.Geometry")
        
        if (!entity) {
            return undefined
        }
        
        let table = new GeometryTable()
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(geometryIndex: number): Promise<IGeometry> {
        return await Geometry.createFromTable(this, geometryIndex)
    }
    
    async getAll(): Promise<IGeometry[]> {
        const localTable = await this.entityTable.getLocal()
        
        let box_Min_X: number[] | undefined
        let box_Min_Y: number[] | undefined
        let box_Min_Z: number[] | undefined
        let box_Max_X: number[] | undefined
        let box_Max_Y: number[] | undefined
        let box_Max_Z: number[] | undefined
        let vertexCount: number[] | undefined
        let faceCount: number[] | undefined
        
        await Promise.all([
            (async () => { box_Min_X = (await localTable.getNumberArray("float:Box.Min.X")) })(),
            (async () => { box_Min_Y = (await localTable.getNumberArray("float:Box.Min.Y")) })(),
            (async () => { box_Min_Z = (await localTable.getNumberArray("float:Box.Min.Z")) })(),
            (async () => { box_Max_X = (await localTable.getNumberArray("float:Box.Max.X")) })(),
            (async () => { box_Max_Y = (await localTable.getNumberArray("float:Box.Max.Y")) })(),
            (async () => { box_Max_Z = (await localTable.getNumberArray("float:Box.Max.Z")) })(),
            (async () => { vertexCount = (await localTable.getNumberArray("int:VertexCount")) })(),
            (async () => { faceCount = (await localTable.getNumberArray("int:FaceCount")) })(),
        ])
        
        let geometry: IGeometry[] = []
        
        const rowCount = await this.getCount()
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
            })
        }
        
        return geometry
    }
    
    async getBox_Min_X(geometryIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(geometryIndex, "float:Box.Min.X"))
    }
    
    async getAllBox_Min_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:Box.Min.X"))
    }
    
    async getBox_Min_Y(geometryIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(geometryIndex, "float:Box.Min.Y"))
    }
    
    async getAllBox_Min_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:Box.Min.Y"))
    }
    
    async getBox_Min_Z(geometryIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(geometryIndex, "float:Box.Min.Z"))
    }
    
    async getAllBox_Min_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:Box.Min.Z"))
    }
    
    async getBox_Max_X(geometryIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(geometryIndex, "float:Box.Max.X"))
    }
    
    async getAllBox_Max_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:Box.Max.X"))
    }
    
    async getBox_Max_Y(geometryIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(geometryIndex, "float:Box.Max.Y"))
    }
    
    async getAllBox_Max_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:Box.Max.Y"))
    }
    
    async getBox_Max_Z(geometryIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(geometryIndex, "float:Box.Max.Z"))
    }
    
    async getAllBox_Max_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("float:Box.Max.Z"))
    }
    
    async getVertexCount(geometryIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(geometryIndex, "int:VertexCount"))
    }
    
    async getAllVertexCount(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:VertexCount"))
    }
    
    async getFaceCount(geometryIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(geometryIndex, "int:FaceCount"))
    }
    
    async getAllFaceCount(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:FaceCount"))
    }
    
}

export interface IShape {
    index: number
    kind?: number
    
    elementIndex?: number
    element?: IElement
}

export interface IShapeTable {
    getCount(): Promise<number>
    get(shapeIndex: number): Promise<IShape>
    getAll(): Promise<IShape[]>
    
    getKind(shapeIndex: number): Promise<number | undefined>
    getAllKind(): Promise<number[] | undefined>
    
    getElementIndex(shapeIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(shapeIndex: number): Promise<IElement | undefined>
}

export class Shape implements IShape {
    index: number
    kind?: number
    
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IShapeTable, index: number): Promise<IShape> {
        let result = new Shape()
        result.index = index
        
        await Promise.all([
            table.getKind(index).then(v => result.kind = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class ShapeTable implements IShapeTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IShapeTable | undefined> {
        const entity = await document.entities.getBfast("Vim.Shape")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ShapeTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(shapeIndex: number): Promise<IShape> {
        return await Shape.createFromTable(this, shapeIndex)
    }
    
    async getAll(): Promise<IShape[]> {
        const localTable = await this.entityTable.getLocal()
        
        let kind: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { kind = (await localTable.getNumberArray("int:Kind")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let shape: IShape[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            shape.push({
                index: i,
                kind: kind ? kind[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return shape
    }
    
    async getKind(shapeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(shapeIndex, "int:Kind"))
    }
    
    async getAllKind(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Kind"))
    }
    
    async getElementIndex(shapeIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(shapeIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(shapeIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(shapeIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IShapeCollection {
    index: number
    
    elementIndex?: number
    element?: IElement
}

export interface IShapeCollectionTable {
    getCount(): Promise<number>
    get(shapeCollectionIndex: number): Promise<IShapeCollection>
    getAll(): Promise<IShapeCollection[]>
    
    getElementIndex(shapeCollectionIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(shapeCollectionIndex: number): Promise<IElement | undefined>
}

export class ShapeCollection implements IShapeCollection {
    index: number
    
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IShapeCollectionTable, index: number): Promise<IShapeCollection> {
        let result = new ShapeCollection()
        result.index = index
        
        await Promise.all([
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class ShapeCollectionTable implements IShapeCollectionTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IShapeCollectionTable | undefined> {
        const entity = await document.entities.getBfast("Vim.ShapeCollection")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ShapeCollectionTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(shapeCollectionIndex: number): Promise<IShapeCollection> {
        return await ShapeCollection.createFromTable(this, shapeCollectionIndex)
    }
    
    async getAll(): Promise<IShapeCollection[]> {
        const localTable = await this.entityTable.getLocal()
        
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let shapeCollection: IShapeCollection[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            shapeCollection.push({
                index: i,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return shapeCollection
    }
    
    async getElementIndex(shapeCollectionIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(shapeCollectionIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(shapeCollectionIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(shapeCollectionIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IShapeInShapeCollection {
    index: number
    
    shapeIndex?: number
    shape?: IShape
    shapeCollectionIndex?: number
    shapeCollection?: IShapeCollection
}

export interface IShapeInShapeCollectionTable {
    getCount(): Promise<number>
    get(shapeInShapeCollectionIndex: number): Promise<IShapeInShapeCollection>
    getAll(): Promise<IShapeInShapeCollection[]>
    
    getShapeIndex(shapeInShapeCollectionIndex: number): Promise<number | undefined>
    getAllShapeIndex(): Promise<number[] | undefined>
    getShape(shapeInShapeCollectionIndex: number): Promise<IShape | undefined>
    getShapeCollectionIndex(shapeInShapeCollectionIndex: number): Promise<number | undefined>
    getAllShapeCollectionIndex(): Promise<number[] | undefined>
    getShapeCollection(shapeInShapeCollectionIndex: number): Promise<IShapeCollection | undefined>
}

export class ShapeInShapeCollection implements IShapeInShapeCollection {
    index: number
    
    shapeIndex?: number
    shape?: IShape
    shapeCollectionIndex?: number
    shapeCollection?: IShapeCollection
    
    static async createFromTable(table: IShapeInShapeCollectionTable, index: number): Promise<IShapeInShapeCollection> {
        let result = new ShapeInShapeCollection()
        result.index = index
        
        await Promise.all([
            table.getShapeIndex(index).then(v => result.shapeIndex = v),
            table.getShapeCollectionIndex(index).then(v => result.shapeCollectionIndex = v),
        ])
        
        return result
    }
}

export class ShapeInShapeCollectionTable implements IShapeInShapeCollectionTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IShapeInShapeCollectionTable | undefined> {
        const entity = await document.entities.getBfast("Vim.ShapeInShapeCollection")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ShapeInShapeCollectionTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(shapeInShapeCollectionIndex: number): Promise<IShapeInShapeCollection> {
        return await ShapeInShapeCollection.createFromTable(this, shapeInShapeCollectionIndex)
    }
    
    async getAll(): Promise<IShapeInShapeCollection[]> {
        const localTable = await this.entityTable.getLocal()
        
        let shapeIndex: number[] | undefined
        let shapeCollectionIndex: number[] | undefined
        
        await Promise.all([
            (async () => { shapeIndex = (await localTable.getNumberArray("index:Vim.Shape:Shape")) })(),
            (async () => { shapeCollectionIndex = (await localTable.getNumberArray("index:Vim.ShapeCollection:ShapeCollection")) })(),
        ])
        
        let shapeInShapeCollection: IShapeInShapeCollection[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            shapeInShapeCollection.push({
                index: i,
                shapeIndex: shapeIndex ? shapeIndex[i] : undefined,
                shapeCollectionIndex: shapeCollectionIndex ? shapeCollectionIndex[i] : undefined
            })
        }
        
        return shapeInShapeCollection
    }
    
    async getShapeIndex(shapeInShapeCollectionIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(shapeInShapeCollectionIndex, "index:Vim.Shape:Shape")
    }
    
    async getAllShapeIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Shape:Shape")
    }
    
    async getShape(shapeInShapeCollectionIndex: number): Promise<IShape | undefined> {
        const index = await this.getShapeIndex(shapeInShapeCollectionIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.shape?.get(index)
    }
    
    async getShapeCollectionIndex(shapeInShapeCollectionIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(shapeInShapeCollectionIndex, "index:Vim.ShapeCollection:ShapeCollection")
    }
    
    async getAllShapeCollectionIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.ShapeCollection:ShapeCollection")
    }
    
    async getShapeCollection(shapeInShapeCollectionIndex: number): Promise<IShapeCollection | undefined> {
        const index = await this.getShapeCollectionIndex(shapeInShapeCollectionIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.shapeCollection?.get(index)
    }
    
}

export interface ISystem {
    index: number
    systemType?: number
    
    familyTypeIndex?: number
    familyType?: IFamilyType
    elementIndex?: number
    element?: IElement
}

export interface ISystemTable {
    getCount(): Promise<number>
    get(systemIndex: number): Promise<ISystem>
    getAll(): Promise<ISystem[]>
    
    getSystemType(systemIndex: number): Promise<number | undefined>
    getAllSystemType(): Promise<number[] | undefined>
    
    getFamilyTypeIndex(systemIndex: number): Promise<number | undefined>
    getAllFamilyTypeIndex(): Promise<number[] | undefined>
    getFamilyType(systemIndex: number): Promise<IFamilyType | undefined>
    getElementIndex(systemIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(systemIndex: number): Promise<IElement | undefined>
}

export class System implements ISystem {
    index: number
    systemType?: number
    
    familyTypeIndex?: number
    familyType?: IFamilyType
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: ISystemTable, index: number): Promise<ISystem> {
        let result = new System()
        result.index = index
        
        await Promise.all([
            table.getSystemType(index).then(v => result.systemType = v),
            table.getFamilyTypeIndex(index).then(v => result.familyTypeIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class SystemTable implements ISystemTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<ISystemTable | undefined> {
        const entity = await document.entities.getBfast("Vim.System")
        
        if (!entity) {
            return undefined
        }
        
        let table = new SystemTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(systemIndex: number): Promise<ISystem> {
        return await System.createFromTable(this, systemIndex)
    }
    
    async getAll(): Promise<ISystem[]> {
        const localTable = await this.entityTable.getLocal()
        
        let systemType: number[] | undefined
        let familyTypeIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { systemType = (await localTable.getNumberArray("int:SystemType")) })(),
            (async () => { familyTypeIndex = (await localTable.getNumberArray("index:Vim.FamilyType:FamilyType")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let system: ISystem[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            system.push({
                index: i,
                systemType: systemType ? systemType[i] : undefined,
                familyTypeIndex: familyTypeIndex ? familyTypeIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return system
    }
    
    async getSystemType(systemIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(systemIndex, "int:SystemType"))
    }
    
    async getAllSystemType(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:SystemType"))
    }
    
    async getFamilyTypeIndex(systemIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(systemIndex, "index:Vim.FamilyType:FamilyType")
    }
    
    async getAllFamilyTypeIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.FamilyType:FamilyType")
    }
    
    async getFamilyType(systemIndex: number): Promise<IFamilyType | undefined> {
        const index = await this.getFamilyTypeIndex(systemIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.familyType?.get(index)
    }
    
    async getElementIndex(systemIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(systemIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(systemIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(systemIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IElementInSystem {
    index: number
    roles?: number
    
    systemIndex?: number
    system?: ISystem
    elementIndex?: number
    element?: IElement
}

export interface IElementInSystemTable {
    getCount(): Promise<number>
    get(elementInSystemIndex: number): Promise<IElementInSystem>
    getAll(): Promise<IElementInSystem[]>
    
    getRoles(elementInSystemIndex: number): Promise<number | undefined>
    getAllRoles(): Promise<number[] | undefined>
    
    getSystemIndex(elementInSystemIndex: number): Promise<number | undefined>
    getAllSystemIndex(): Promise<number[] | undefined>
    getSystem(elementInSystemIndex: number): Promise<ISystem | undefined>
    getElementIndex(elementInSystemIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(elementInSystemIndex: number): Promise<IElement | undefined>
}

export class ElementInSystem implements IElementInSystem {
    index: number
    roles?: number
    
    systemIndex?: number
    system?: ISystem
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IElementInSystemTable, index: number): Promise<IElementInSystem> {
        let result = new ElementInSystem()
        result.index = index
        
        await Promise.all([
            table.getRoles(index).then(v => result.roles = v),
            table.getSystemIndex(index).then(v => result.systemIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class ElementInSystemTable implements IElementInSystemTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IElementInSystemTable | undefined> {
        const entity = await document.entities.getBfast("Vim.ElementInSystem")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ElementInSystemTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(elementInSystemIndex: number): Promise<IElementInSystem> {
        return await ElementInSystem.createFromTable(this, elementInSystemIndex)
    }
    
    async getAll(): Promise<IElementInSystem[]> {
        const localTable = await this.entityTable.getLocal()
        
        let roles: number[] | undefined
        let systemIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { roles = (await localTable.getNumberArray("int:Roles")) })(),
            (async () => { systemIndex = (await localTable.getNumberArray("index:Vim.System:System")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let elementInSystem: IElementInSystem[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            elementInSystem.push({
                index: i,
                roles: roles ? roles[i] : undefined,
                systemIndex: systemIndex ? systemIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return elementInSystem
    }
    
    async getRoles(elementInSystemIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(elementInSystemIndex, "int:Roles"))
    }
    
    async getAllRoles(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Roles"))
    }
    
    async getSystemIndex(elementInSystemIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementInSystemIndex, "index:Vim.System:System")
    }
    
    async getAllSystemIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.System:System")
    }
    
    async getSystem(elementInSystemIndex: number): Promise<ISystem | undefined> {
        const index = await this.getSystemIndex(elementInSystemIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.system?.get(index)
    }
    
    async getElementIndex(elementInSystemIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementInSystemIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(elementInSystemIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(elementInSystemIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IWarning {
    index: number
    guid?: string
    severity?: string
    description?: string
    key?: string
    failingElementCount?: number
    
    bimDocumentIndex?: number
    bimDocument?: IBimDocument
}

export interface IWarningTable {
    getCount(): Promise<number>
    get(warningIndex: number): Promise<IWarning>
    getAll(): Promise<IWarning[]>
    
    getGuid(warningIndex: number): Promise<string | undefined>
    getAllGuid(): Promise<string[] | undefined>
    getSeverity(warningIndex: number): Promise<string | undefined>
    getAllSeverity(): Promise<string[] | undefined>
    getDescription(warningIndex: number): Promise<string | undefined>
    getAllDescription(): Promise<string[] | undefined>
    getKey(warningIndex: number): Promise<string | undefined>
    getAllKey(): Promise<string[] | undefined>
    getFailingElementCount(warningIndex: number): Promise<number | undefined>
    getAllFailingElementCount(): Promise<number[] | undefined>
    
    getBimDocumentIndex(warningIndex: number): Promise<number | undefined>
    getAllBimDocumentIndex(): Promise<number[] | undefined>
    getBimDocument(warningIndex: number): Promise<IBimDocument | undefined>
}

export class Warning implements IWarning {
    index: number
    guid?: string
    severity?: string
    description?: string
    key?: string
    failingElementCount?: number
    
    bimDocumentIndex?: number
    bimDocument?: IBimDocument
    
    static async createFromTable(table: IWarningTable, index: number): Promise<IWarning> {
        let result = new Warning()
        result.index = index
        
        await Promise.all([
            table.getGuid(index).then(v => result.guid = v),
            table.getSeverity(index).then(v => result.severity = v),
            table.getDescription(index).then(v => result.description = v),
            table.getKey(index).then(v => result.key = v),
            table.getFailingElementCount(index).then(v => result.failingElementCount = v),
            table.getBimDocumentIndex(index).then(v => result.bimDocumentIndex = v),
        ])
        
        return result
    }
}

export class WarningTable implements IWarningTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IWarningTable | undefined> {
        const entity = await document.entities.getBfast("Vim.Warning")
        
        if (!entity) {
            return undefined
        }
        
        let table = new WarningTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(warningIndex: number): Promise<IWarning> {
        return await Warning.createFromTable(this, warningIndex)
    }
    
    async getAll(): Promise<IWarning[]> {
        const localTable = await this.entityTable.getLocal()
        
        let guid: string[] | undefined
        let severity: string[] | undefined
        let description: string[] | undefined
        let key: string[] | undefined
        let failingElementCount: number[] | undefined
        let bimDocumentIndex: number[] | undefined
        
        await Promise.all([
            (async () => { guid = (await localTable.getStringArray("string:Guid")) })(),
            (async () => { severity = (await localTable.getStringArray("string:Severity")) })(),
            (async () => { description = (await localTable.getStringArray("string:Description")) })(),
            (async () => { key = (await localTable.getStringArray("string:Key")) })(),
            (async () => { failingElementCount = (await localTable.getNumberArray("int:FailingElementCount")) })(),
            (async () => { bimDocumentIndex = (await localTable.getNumberArray("index:Vim.BimDocument:BimDocument")) })(),
        ])
        
        let warning: IWarning[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            warning.push({
                index: i,
                guid: guid ? guid[i] : undefined,
                severity: severity ? severity[i] : undefined,
                description: description ? description[i] : undefined,
                key: key ? key[i] : undefined,
                failingElementCount: failingElementCount ? failingElementCount[i] : undefined,
                bimDocumentIndex: bimDocumentIndex ? bimDocumentIndex[i] : undefined
            })
        }
        
        return warning
    }
    
    async getGuid(warningIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(warningIndex, "string:Guid"))
    }
    
    async getAllGuid(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Guid"))
    }
    
    async getSeverity(warningIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(warningIndex, "string:Severity"))
    }
    
    async getAllSeverity(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Severity"))
    }
    
    async getDescription(warningIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(warningIndex, "string:Description"))
    }
    
    async getAllDescription(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Description"))
    }
    
    async getKey(warningIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(warningIndex, "string:Key"))
    }
    
    async getAllKey(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Key"))
    }
    
    async getFailingElementCount(warningIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(warningIndex, "int:FailingElementCount"))
    }
    
    async getAllFailingElementCount(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:FailingElementCount"))
    }
    
    async getBimDocumentIndex(warningIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(warningIndex, "index:Vim.BimDocument:BimDocument")
    }
    
    async getAllBimDocumentIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.BimDocument:BimDocument")
    }
    
    async getBimDocument(warningIndex: number): Promise<IBimDocument | undefined> {
        const index = await this.getBimDocumentIndex(warningIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.bimDocument?.get(index)
    }
    
}

export interface IElementInWarning {
    index: number
    
    warningIndex?: number
    warning?: IWarning
    elementIndex?: number
    element?: IElement
}

export interface IElementInWarningTable {
    getCount(): Promise<number>
    get(elementInWarningIndex: number): Promise<IElementInWarning>
    getAll(): Promise<IElementInWarning[]>
    
    getWarningIndex(elementInWarningIndex: number): Promise<number | undefined>
    getAllWarningIndex(): Promise<number[] | undefined>
    getWarning(elementInWarningIndex: number): Promise<IWarning | undefined>
    getElementIndex(elementInWarningIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(elementInWarningIndex: number): Promise<IElement | undefined>
}

export class ElementInWarning implements IElementInWarning {
    index: number
    
    warningIndex?: number
    warning?: IWarning
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IElementInWarningTable, index: number): Promise<IElementInWarning> {
        let result = new ElementInWarning()
        result.index = index
        
        await Promise.all([
            table.getWarningIndex(index).then(v => result.warningIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class ElementInWarningTable implements IElementInWarningTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IElementInWarningTable | undefined> {
        const entity = await document.entities.getBfast("Vim.ElementInWarning")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ElementInWarningTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(elementInWarningIndex: number): Promise<IElementInWarning> {
        return await ElementInWarning.createFromTable(this, elementInWarningIndex)
    }
    
    async getAll(): Promise<IElementInWarning[]> {
        const localTable = await this.entityTable.getLocal()
        
        let warningIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { warningIndex = (await localTable.getNumberArray("index:Vim.Warning:Warning")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let elementInWarning: IElementInWarning[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            elementInWarning.push({
                index: i,
                warningIndex: warningIndex ? warningIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return elementInWarning
    }
    
    async getWarningIndex(elementInWarningIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementInWarningIndex, "index:Vim.Warning:Warning")
    }
    
    async getAllWarningIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Warning:Warning")
    }
    
    async getWarning(elementInWarningIndex: number): Promise<IWarning | undefined> {
        const index = await this.getWarningIndex(elementInWarningIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.warning?.get(index)
    }
    
    async getElementIndex(elementInWarningIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementInWarningIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(elementInWarningIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(elementInWarningIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IBasePoint {
    index: number
    isSurveyPoint?: boolean
    isClipped?: boolean
    northSouth?: number
    eastWest?: number
    elevation?: number
    angleToTrueNorth?: number
    position_X?: number
    position_Y?: number
    position_Z?: number
    sharedPosition_X?: number
    sharedPosition_Y?: number
    sharedPosition_Z?: number
    
    elementIndex?: number
    element?: IElement
}

export interface IBasePointTable {
    getCount(): Promise<number>
    get(basePointIndex: number): Promise<IBasePoint>
    getAll(): Promise<IBasePoint[]>
    
    getIsSurveyPoint(basePointIndex: number): Promise<boolean | undefined>
    getAllIsSurveyPoint(): Promise<boolean[] | undefined>
    getIsClipped(basePointIndex: number): Promise<boolean | undefined>
    getAllIsClipped(): Promise<boolean[] | undefined>
    getNorthSouth(basePointIndex: number): Promise<number | undefined>
    getAllNorthSouth(): Promise<number[] | undefined>
    getEastWest(basePointIndex: number): Promise<number | undefined>
    getAllEastWest(): Promise<number[] | undefined>
    getElevation(basePointIndex: number): Promise<number | undefined>
    getAllElevation(): Promise<number[] | undefined>
    getAngleToTrueNorth(basePointIndex: number): Promise<number | undefined>
    getAllAngleToTrueNorth(): Promise<number[] | undefined>
    getPosition_X(basePointIndex: number): Promise<number | undefined>
    getAllPosition_X(): Promise<number[] | undefined>
    getPosition_Y(basePointIndex: number): Promise<number | undefined>
    getAllPosition_Y(): Promise<number[] | undefined>
    getPosition_Z(basePointIndex: number): Promise<number | undefined>
    getAllPosition_Z(): Promise<number[] | undefined>
    getSharedPosition_X(basePointIndex: number): Promise<number | undefined>
    getAllSharedPosition_X(): Promise<number[] | undefined>
    getSharedPosition_Y(basePointIndex: number): Promise<number | undefined>
    getAllSharedPosition_Y(): Promise<number[] | undefined>
    getSharedPosition_Z(basePointIndex: number): Promise<number | undefined>
    getAllSharedPosition_Z(): Promise<number[] | undefined>
    
    getElementIndex(basePointIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(basePointIndex: number): Promise<IElement | undefined>
}

export class BasePoint implements IBasePoint {
    index: number
    isSurveyPoint?: boolean
    isClipped?: boolean
    northSouth?: number
    eastWest?: number
    elevation?: number
    angleToTrueNorth?: number
    position_X?: number
    position_Y?: number
    position_Z?: number
    sharedPosition_X?: number
    sharedPosition_Y?: number
    sharedPosition_Z?: number
    
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IBasePointTable, index: number): Promise<IBasePoint> {
        let result = new BasePoint()
        result.index = index
        
        await Promise.all([
            table.getIsSurveyPoint(index).then(v => result.isSurveyPoint = v),
            table.getIsClipped(index).then(v => result.isClipped = v),
            table.getNorthSouth(index).then(v => result.northSouth = v),
            table.getEastWest(index).then(v => result.eastWest = v),
            table.getElevation(index).then(v => result.elevation = v),
            table.getAngleToTrueNorth(index).then(v => result.angleToTrueNorth = v),
            table.getPosition_X(index).then(v => result.position_X = v),
            table.getPosition_Y(index).then(v => result.position_Y = v),
            table.getPosition_Z(index).then(v => result.position_Z = v),
            table.getSharedPosition_X(index).then(v => result.sharedPosition_X = v),
            table.getSharedPosition_Y(index).then(v => result.sharedPosition_Y = v),
            table.getSharedPosition_Z(index).then(v => result.sharedPosition_Z = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class BasePointTable implements IBasePointTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IBasePointTable | undefined> {
        const entity = await document.entities.getBfast("Vim.BasePoint")
        
        if (!entity) {
            return undefined
        }
        
        let table = new BasePointTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(basePointIndex: number): Promise<IBasePoint> {
        return await BasePoint.createFromTable(this, basePointIndex)
    }
    
    async getAll(): Promise<IBasePoint[]> {
        const localTable = await this.entityTable.getLocal()
        
        let isSurveyPoint: boolean[] | undefined
        let isClipped: boolean[] | undefined
        let northSouth: number[] | undefined
        let eastWest: number[] | undefined
        let elevation: number[] | undefined
        let angleToTrueNorth: number[] | undefined
        let position_X: number[] | undefined
        let position_Y: number[] | undefined
        let position_Z: number[] | undefined
        let sharedPosition_X: number[] | undefined
        let sharedPosition_Y: number[] | undefined
        let sharedPosition_Z: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { isSurveyPoint = (await localTable.getBooleanArray("byte:IsSurveyPoint")) })(),
            (async () => { isClipped = (await localTable.getBooleanArray("byte:IsClipped")) })(),
            (async () => { northSouth = (await localTable.getNumberArray("double:NorthSouth")) })(),
            (async () => { eastWest = (await localTable.getNumberArray("double:EastWest")) })(),
            (async () => { elevation = (await localTable.getNumberArray("double:Elevation")) })(),
            (async () => { angleToTrueNorth = (await localTable.getNumberArray("double:AngleToTrueNorth")) })(),
            (async () => { position_X = (await localTable.getNumberArray("double:Position.X")) })(),
            (async () => { position_Y = (await localTable.getNumberArray("double:Position.Y")) })(),
            (async () => { position_Z = (await localTable.getNumberArray("double:Position.Z")) })(),
            (async () => { sharedPosition_X = (await localTable.getNumberArray("double:SharedPosition.X")) })(),
            (async () => { sharedPosition_Y = (await localTable.getNumberArray("double:SharedPosition.Y")) })(),
            (async () => { sharedPosition_Z = (await localTable.getNumberArray("double:SharedPosition.Z")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let basePoint: IBasePoint[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            basePoint.push({
                index: i,
                isSurveyPoint: isSurveyPoint ? isSurveyPoint[i] : undefined,
                isClipped: isClipped ? isClipped[i] : undefined,
                northSouth: northSouth ? northSouth[i] : undefined,
                eastWest: eastWest ? eastWest[i] : undefined,
                elevation: elevation ? elevation[i] : undefined,
                angleToTrueNorth: angleToTrueNorth ? angleToTrueNorth[i] : undefined,
                position_X: position_X ? position_X[i] : undefined,
                position_Y: position_Y ? position_Y[i] : undefined,
                position_Z: position_Z ? position_Z[i] : undefined,
                sharedPosition_X: sharedPosition_X ? sharedPosition_X[i] : undefined,
                sharedPosition_Y: sharedPosition_Y ? sharedPosition_Y[i] : undefined,
                sharedPosition_Z: sharedPosition_Z ? sharedPosition_Z[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return basePoint
    }
    
    async getIsSurveyPoint(basePointIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(basePointIndex, "byte:IsSurveyPoint"))
    }
    
    async getAllIsSurveyPoint(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsSurveyPoint"))
    }
    
    async getIsClipped(basePointIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(basePointIndex, "byte:IsClipped"))
    }
    
    async getAllIsClipped(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsClipped"))
    }
    
    async getNorthSouth(basePointIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(basePointIndex, "double:NorthSouth"))
    }
    
    async getAllNorthSouth(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:NorthSouth"))
    }
    
    async getEastWest(basePointIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(basePointIndex, "double:EastWest"))
    }
    
    async getAllEastWest(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:EastWest"))
    }
    
    async getElevation(basePointIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(basePointIndex, "double:Elevation"))
    }
    
    async getAllElevation(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Elevation"))
    }
    
    async getAngleToTrueNorth(basePointIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(basePointIndex, "double:AngleToTrueNorth"))
    }
    
    async getAllAngleToTrueNorth(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:AngleToTrueNorth"))
    }
    
    async getPosition_X(basePointIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(basePointIndex, "double:Position.X"))
    }
    
    async getAllPosition_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Position.X"))
    }
    
    async getPosition_Y(basePointIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(basePointIndex, "double:Position.Y"))
    }
    
    async getAllPosition_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Position.Y"))
    }
    
    async getPosition_Z(basePointIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(basePointIndex, "double:Position.Z"))
    }
    
    async getAllPosition_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Position.Z"))
    }
    
    async getSharedPosition_X(basePointIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(basePointIndex, "double:SharedPosition.X"))
    }
    
    async getAllSharedPosition_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:SharedPosition.X"))
    }
    
    async getSharedPosition_Y(basePointIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(basePointIndex, "double:SharedPosition.Y"))
    }
    
    async getAllSharedPosition_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:SharedPosition.Y"))
    }
    
    async getSharedPosition_Z(basePointIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(basePointIndex, "double:SharedPosition.Z"))
    }
    
    async getAllSharedPosition_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:SharedPosition.Z"))
    }
    
    async getElementIndex(basePointIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(basePointIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(basePointIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(basePointIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IPhaseFilter {
    index: number
    _new?: number
    existing?: number
    demolished?: number
    temporary?: number
    
    elementIndex?: number
    element?: IElement
}

export interface IPhaseFilterTable {
    getCount(): Promise<number>
    get(phaseFilterIndex: number): Promise<IPhaseFilter>
    getAll(): Promise<IPhaseFilter[]>
    
    getNew(phaseFilterIndex: number): Promise<number | undefined>
    getAllNew(): Promise<number[] | undefined>
    getExisting(phaseFilterIndex: number): Promise<number | undefined>
    getAllExisting(): Promise<number[] | undefined>
    getDemolished(phaseFilterIndex: number): Promise<number | undefined>
    getAllDemolished(): Promise<number[] | undefined>
    getTemporary(phaseFilterIndex: number): Promise<number | undefined>
    getAllTemporary(): Promise<number[] | undefined>
    
    getElementIndex(phaseFilterIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(phaseFilterIndex: number): Promise<IElement | undefined>
}

export class PhaseFilter implements IPhaseFilter {
    index: number
    _new?: number
    existing?: number
    demolished?: number
    temporary?: number
    
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IPhaseFilterTable, index: number): Promise<IPhaseFilter> {
        let result = new PhaseFilter()
        result.index = index
        
        await Promise.all([
            table.getNew(index).then(v => result._new = v),
            table.getExisting(index).then(v => result.existing = v),
            table.getDemolished(index).then(v => result.demolished = v),
            table.getTemporary(index).then(v => result.temporary = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class PhaseFilterTable implements IPhaseFilterTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IPhaseFilterTable | undefined> {
        const entity = await document.entities.getBfast("Vim.PhaseFilter")
        
        if (!entity) {
            return undefined
        }
        
        let table = new PhaseFilterTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(phaseFilterIndex: number): Promise<IPhaseFilter> {
        return await PhaseFilter.createFromTable(this, phaseFilterIndex)
    }
    
    async getAll(): Promise<IPhaseFilter[]> {
        const localTable = await this.entityTable.getLocal()
        
        let _new: number[] | undefined
        let existing: number[] | undefined
        let demolished: number[] | undefined
        let temporary: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { _new = (await localTable.getNumberArray("int:New")) })(),
            (async () => { existing = (await localTable.getNumberArray("int:Existing")) })(),
            (async () => { demolished = (await localTable.getNumberArray("int:Demolished")) })(),
            (async () => { temporary = (await localTable.getNumberArray("int:Temporary")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let phaseFilter: IPhaseFilter[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            phaseFilter.push({
                index: i,
                _new: _new ? _new[i] : undefined,
                existing: existing ? existing[i] : undefined,
                demolished: demolished ? demolished[i] : undefined,
                temporary: temporary ? temporary[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return phaseFilter
    }
    
    async getNew(phaseFilterIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(phaseFilterIndex, "int:New"))
    }
    
    async getAllNew(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:New"))
    }
    
    async getExisting(phaseFilterIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(phaseFilterIndex, "int:Existing"))
    }
    
    async getAllExisting(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Existing"))
    }
    
    async getDemolished(phaseFilterIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(phaseFilterIndex, "int:Demolished"))
    }
    
    async getAllDemolished(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Demolished"))
    }
    
    async getTemporary(phaseFilterIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(phaseFilterIndex, "int:Temporary"))
    }
    
    async getAllTemporary(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Temporary"))
    }
    
    async getElementIndex(phaseFilterIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(phaseFilterIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(phaseFilterIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(phaseFilterIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IGrid {
    index: number
    startPoint_X?: number
    startPoint_Y?: number
    startPoint_Z?: number
    endPoint_X?: number
    endPoint_Y?: number
    endPoint_Z?: number
    isCurved?: boolean
    extents_Min_X?: number
    extents_Min_Y?: number
    extents_Min_Z?: number
    extents_Max_X?: number
    extents_Max_Y?: number
    extents_Max_Z?: number
    typeBubbleAtEnd0?: boolean
    typeBubbleAtEnd1?: boolean
    bubbleAtEnd0?: boolean
    bubbleAtEnd1?: boolean
    
    familyTypeIndex?: number
    familyType?: IFamilyType
    bubbleViewIndex?: number
    bubbleView?: IView
    elementIndex?: number
    element?: IElement
}

export interface IGridTable {
    getCount(): Promise<number>
    get(gridIndex: number): Promise<IGrid>
    getAll(): Promise<IGrid[]>
    
    getStartPoint_X(gridIndex: number): Promise<number | undefined>
    getAllStartPoint_X(): Promise<number[] | undefined>
    getStartPoint_Y(gridIndex: number): Promise<number | undefined>
    getAllStartPoint_Y(): Promise<number[] | undefined>
    getStartPoint_Z(gridIndex: number): Promise<number | undefined>
    getAllStartPoint_Z(): Promise<number[] | undefined>
    getEndPoint_X(gridIndex: number): Promise<number | undefined>
    getAllEndPoint_X(): Promise<number[] | undefined>
    getEndPoint_Y(gridIndex: number): Promise<number | undefined>
    getAllEndPoint_Y(): Promise<number[] | undefined>
    getEndPoint_Z(gridIndex: number): Promise<number | undefined>
    getAllEndPoint_Z(): Promise<number[] | undefined>
    getIsCurved(gridIndex: number): Promise<boolean | undefined>
    getAllIsCurved(): Promise<boolean[] | undefined>
    getExtents_Min_X(gridIndex: number): Promise<number | undefined>
    getAllExtents_Min_X(): Promise<number[] | undefined>
    getExtents_Min_Y(gridIndex: number): Promise<number | undefined>
    getAllExtents_Min_Y(): Promise<number[] | undefined>
    getExtents_Min_Z(gridIndex: number): Promise<number | undefined>
    getAllExtents_Min_Z(): Promise<number[] | undefined>
    getExtents_Max_X(gridIndex: number): Promise<number | undefined>
    getAllExtents_Max_X(): Promise<number[] | undefined>
    getExtents_Max_Y(gridIndex: number): Promise<number | undefined>
    getAllExtents_Max_Y(): Promise<number[] | undefined>
    getExtents_Max_Z(gridIndex: number): Promise<number | undefined>
    getAllExtents_Max_Z(): Promise<number[] | undefined>
    getTypeBubbleAtEnd0(gridIndex: number): Promise<boolean | undefined>
    getAllTypeBubbleAtEnd0(): Promise<boolean[] | undefined>
    getTypeBubbleAtEnd1(gridIndex: number): Promise<boolean | undefined>
    getAllTypeBubbleAtEnd1(): Promise<boolean[] | undefined>
    getBubbleAtEnd0(gridIndex: number): Promise<boolean | undefined>
    getAllBubbleAtEnd0(): Promise<boolean[] | undefined>
    getBubbleAtEnd1(gridIndex: number): Promise<boolean | undefined>
    getAllBubbleAtEnd1(): Promise<boolean[] | undefined>
    
    getFamilyTypeIndex(gridIndex: number): Promise<number | undefined>
    getAllFamilyTypeIndex(): Promise<number[] | undefined>
    getFamilyType(gridIndex: number): Promise<IFamilyType | undefined>
    getBubbleViewIndex(gridIndex: number): Promise<number | undefined>
    getAllBubbleViewIndex(): Promise<number[] | undefined>
    getBubbleView(gridIndex: number): Promise<IView | undefined>
    getElementIndex(gridIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(gridIndex: number): Promise<IElement | undefined>
}

export class Grid implements IGrid {
    index: number
    startPoint_X?: number
    startPoint_Y?: number
    startPoint_Z?: number
    endPoint_X?: number
    endPoint_Y?: number
    endPoint_Z?: number
    isCurved?: boolean
    extents_Min_X?: number
    extents_Min_Y?: number
    extents_Min_Z?: number
    extents_Max_X?: number
    extents_Max_Y?: number
    extents_Max_Z?: number
    typeBubbleAtEnd0?: boolean
    typeBubbleAtEnd1?: boolean
    bubbleAtEnd0?: boolean
    bubbleAtEnd1?: boolean
    
    familyTypeIndex?: number
    familyType?: IFamilyType
    bubbleViewIndex?: number
    bubbleView?: IView
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IGridTable, index: number): Promise<IGrid> {
        let result = new Grid()
        result.index = index
        
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
            table.getTypeBubbleAtEnd0(index).then(v => result.typeBubbleAtEnd0 = v),
            table.getTypeBubbleAtEnd1(index).then(v => result.typeBubbleAtEnd1 = v),
            table.getBubbleAtEnd0(index).then(v => result.bubbleAtEnd0 = v),
            table.getBubbleAtEnd1(index).then(v => result.bubbleAtEnd1 = v),
            table.getFamilyTypeIndex(index).then(v => result.familyTypeIndex = v),
            table.getBubbleViewIndex(index).then(v => result.bubbleViewIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class GridTable implements IGridTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IGridTable | undefined> {
        const entity = await document.entities.getBfast("Vim.Grid")
        
        if (!entity) {
            return undefined
        }
        
        let table = new GridTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(gridIndex: number): Promise<IGrid> {
        return await Grid.createFromTable(this, gridIndex)
    }
    
    async getAll(): Promise<IGrid[]> {
        const localTable = await this.entityTable.getLocal()
        
        let startPoint_X: number[] | undefined
        let startPoint_Y: number[] | undefined
        let startPoint_Z: number[] | undefined
        let endPoint_X: number[] | undefined
        let endPoint_Y: number[] | undefined
        let endPoint_Z: number[] | undefined
        let isCurved: boolean[] | undefined
        let extents_Min_X: number[] | undefined
        let extents_Min_Y: number[] | undefined
        let extents_Min_Z: number[] | undefined
        let extents_Max_X: number[] | undefined
        let extents_Max_Y: number[] | undefined
        let extents_Max_Z: number[] | undefined
        let typeBubbleAtEnd0: boolean[] | undefined
        let typeBubbleAtEnd1: boolean[] | undefined
        let bubbleAtEnd0: boolean[] | undefined
        let bubbleAtEnd1: boolean[] | undefined
        let familyTypeIndex: number[] | undefined
        let bubbleViewIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { startPoint_X = (await localTable.getNumberArray("double:StartPoint.X")) })(),
            (async () => { startPoint_Y = (await localTable.getNumberArray("double:StartPoint.Y")) })(),
            (async () => { startPoint_Z = (await localTable.getNumberArray("double:StartPoint.Z")) })(),
            (async () => { endPoint_X = (await localTable.getNumberArray("double:EndPoint.X")) })(),
            (async () => { endPoint_Y = (await localTable.getNumberArray("double:EndPoint.Y")) })(),
            (async () => { endPoint_Z = (await localTable.getNumberArray("double:EndPoint.Z")) })(),
            (async () => { isCurved = (await localTable.getBooleanArray("byte:IsCurved")) })(),
            (async () => { extents_Min_X = (await localTable.getNumberArray("double:Extents.Min.X")) })(),
            (async () => { extents_Min_Y = (await localTable.getNumberArray("double:Extents.Min.Y")) })(),
            (async () => { extents_Min_Z = (await localTable.getNumberArray("double:Extents.Min.Z")) })(),
            (async () => { extents_Max_X = (await localTable.getNumberArray("double:Extents.Max.X")) })(),
            (async () => { extents_Max_Y = (await localTable.getNumberArray("double:Extents.Max.Y")) })(),
            (async () => { extents_Max_Z = (await localTable.getNumberArray("double:Extents.Max.Z")) })(),
            (async () => { typeBubbleAtEnd0 = (await localTable.getBooleanArray("byte:TypeBubbleAtEnd0")) })(),
            (async () => { typeBubbleAtEnd1 = (await localTable.getBooleanArray("byte:TypeBubbleAtEnd1")) })(),
            (async () => { bubbleAtEnd0 = (await localTable.getBooleanArray("byte:BubbleAtEnd0")) })(),
            (async () => { bubbleAtEnd1 = (await localTable.getBooleanArray("byte:BubbleAtEnd1")) })(),
            (async () => { familyTypeIndex = (await localTable.getNumberArray("index:Vim.FamilyType:FamilyType")) })(),
            (async () => { bubbleViewIndex = (await localTable.getNumberArray("index:Vim.View:BubbleView")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let grid: IGrid[] = []
        
        const rowCount = await this.getCount()
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
                typeBubbleAtEnd0: typeBubbleAtEnd0 ? typeBubbleAtEnd0[i] : undefined,
                typeBubbleAtEnd1: typeBubbleAtEnd1 ? typeBubbleAtEnd1[i] : undefined,
                bubbleAtEnd0: bubbleAtEnd0 ? bubbleAtEnd0[i] : undefined,
                bubbleAtEnd1: bubbleAtEnd1 ? bubbleAtEnd1[i] : undefined,
                familyTypeIndex: familyTypeIndex ? familyTypeIndex[i] : undefined,
                bubbleViewIndex: bubbleViewIndex ? bubbleViewIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return grid
    }
    
    async getStartPoint_X(gridIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(gridIndex, "double:StartPoint.X"))
    }
    
    async getAllStartPoint_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:StartPoint.X"))
    }
    
    async getStartPoint_Y(gridIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(gridIndex, "double:StartPoint.Y"))
    }
    
    async getAllStartPoint_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:StartPoint.Y"))
    }
    
    async getStartPoint_Z(gridIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(gridIndex, "double:StartPoint.Z"))
    }
    
    async getAllStartPoint_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:StartPoint.Z"))
    }
    
    async getEndPoint_X(gridIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(gridIndex, "double:EndPoint.X"))
    }
    
    async getAllEndPoint_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:EndPoint.X"))
    }
    
    async getEndPoint_Y(gridIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(gridIndex, "double:EndPoint.Y"))
    }
    
    async getAllEndPoint_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:EndPoint.Y"))
    }
    
    async getEndPoint_Z(gridIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(gridIndex, "double:EndPoint.Z"))
    }
    
    async getAllEndPoint_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:EndPoint.Z"))
    }
    
    async getIsCurved(gridIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(gridIndex, "byte:IsCurved"))
    }
    
    async getAllIsCurved(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsCurved"))
    }
    
    async getExtents_Min_X(gridIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(gridIndex, "double:Extents.Min.X"))
    }
    
    async getAllExtents_Min_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Extents.Min.X"))
    }
    
    async getExtents_Min_Y(gridIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(gridIndex, "double:Extents.Min.Y"))
    }
    
    async getAllExtents_Min_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Extents.Min.Y"))
    }
    
    async getExtents_Min_Z(gridIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(gridIndex, "double:Extents.Min.Z"))
    }
    
    async getAllExtents_Min_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Extents.Min.Z"))
    }
    
    async getExtents_Max_X(gridIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(gridIndex, "double:Extents.Max.X"))
    }
    
    async getAllExtents_Max_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Extents.Max.X"))
    }
    
    async getExtents_Max_Y(gridIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(gridIndex, "double:Extents.Max.Y"))
    }
    
    async getAllExtents_Max_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Extents.Max.Y"))
    }
    
    async getExtents_Max_Z(gridIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(gridIndex, "double:Extents.Max.Z"))
    }
    
    async getAllExtents_Max_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Extents.Max.Z"))
    }
    
    async getTypeBubbleAtEnd0(gridIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(gridIndex, "byte:TypeBubbleAtEnd0"))
    }
    
    async getAllTypeBubbleAtEnd0(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:TypeBubbleAtEnd0"))
    }
    
    async getTypeBubbleAtEnd1(gridIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(gridIndex, "byte:TypeBubbleAtEnd1"))
    }
    
    async getAllTypeBubbleAtEnd1(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:TypeBubbleAtEnd1"))
    }
    
    async getBubbleAtEnd0(gridIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(gridIndex, "byte:BubbleAtEnd0"))
    }
    
    async getAllBubbleAtEnd0(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:BubbleAtEnd0"))
    }
    
    async getBubbleAtEnd1(gridIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(gridIndex, "byte:BubbleAtEnd1"))
    }
    
    async getAllBubbleAtEnd1(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:BubbleAtEnd1"))
    }
    
    async getFamilyTypeIndex(gridIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(gridIndex, "index:Vim.FamilyType:FamilyType")
    }
    
    async getAllFamilyTypeIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.FamilyType:FamilyType")
    }
    
    async getFamilyType(gridIndex: number): Promise<IFamilyType | undefined> {
        const index = await this.getFamilyTypeIndex(gridIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.familyType?.get(index)
    }
    
    async getBubbleViewIndex(gridIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(gridIndex, "index:Vim.View:BubbleView")
    }
    
    async getAllBubbleViewIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.View:BubbleView")
    }
    
    async getBubbleView(gridIndex: number): Promise<IView | undefined> {
        const index = await this.getBubbleViewIndex(gridIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.view?.get(index)
    }
    
    async getElementIndex(gridIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(gridIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(gridIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(gridIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IArea {
    index: number
    value?: number
    perimeter?: number
    number?: string
    isGrossInterior?: boolean
    
    areaSchemeIndex?: number
    areaScheme?: IAreaScheme
    elementIndex?: number
    element?: IElement
}

export interface IAreaTable {
    getCount(): Promise<number>
    get(areaIndex: number): Promise<IArea>
    getAll(): Promise<IArea[]>
    
    getValue(areaIndex: number): Promise<number | undefined>
    getAllValue(): Promise<number[] | undefined>
    getPerimeter(areaIndex: number): Promise<number | undefined>
    getAllPerimeter(): Promise<number[] | undefined>
    getNumber(areaIndex: number): Promise<string | undefined>
    getAllNumber(): Promise<string[] | undefined>
    getIsGrossInterior(areaIndex: number): Promise<boolean | undefined>
    getAllIsGrossInterior(): Promise<boolean[] | undefined>
    
    getAreaSchemeIndex(areaIndex: number): Promise<number | undefined>
    getAllAreaSchemeIndex(): Promise<number[] | undefined>
    getAreaScheme(areaIndex: number): Promise<IAreaScheme | undefined>
    getElementIndex(areaIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(areaIndex: number): Promise<IElement | undefined>
}

export class Area implements IArea {
    index: number
    value?: number
    perimeter?: number
    number?: string
    isGrossInterior?: boolean
    
    areaSchemeIndex?: number
    areaScheme?: IAreaScheme
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IAreaTable, index: number): Promise<IArea> {
        let result = new Area()
        result.index = index
        
        await Promise.all([
            table.getValue(index).then(v => result.value = v),
            table.getPerimeter(index).then(v => result.perimeter = v),
            table.getNumber(index).then(v => result.number = v),
            table.getIsGrossInterior(index).then(v => result.isGrossInterior = v),
            table.getAreaSchemeIndex(index).then(v => result.areaSchemeIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class AreaTable implements IAreaTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IAreaTable | undefined> {
        const entity = await document.entities.getBfast("Vim.Area")
        
        if (!entity) {
            return undefined
        }
        
        let table = new AreaTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(areaIndex: number): Promise<IArea> {
        return await Area.createFromTable(this, areaIndex)
    }
    
    async getAll(): Promise<IArea[]> {
        const localTable = await this.entityTable.getLocal()
        
        let value: number[] | undefined
        let perimeter: number[] | undefined
        let number: string[] | undefined
        let isGrossInterior: boolean[] | undefined
        let areaSchemeIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { value = (await localTable.getNumberArray("double:Value")) })(),
            (async () => { perimeter = (await localTable.getNumberArray("double:Perimeter")) })(),
            (async () => { number = (await localTable.getStringArray("string:Number")) })(),
            (async () => { isGrossInterior = (await localTable.getBooleanArray("byte:IsGrossInterior")) })(),
            (async () => { areaSchemeIndex = (await localTable.getNumberArray("index:Vim.AreaScheme:AreaScheme")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let area: IArea[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            area.push({
                index: i,
                value: value ? value[i] : undefined,
                perimeter: perimeter ? perimeter[i] : undefined,
                number: number ? number[i] : undefined,
                isGrossInterior: isGrossInterior ? isGrossInterior[i] : undefined,
                areaSchemeIndex: areaSchemeIndex ? areaSchemeIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return area
    }
    
    async getValue(areaIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(areaIndex, "double:Value"))
    }
    
    async getAllValue(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Value"))
    }
    
    async getPerimeter(areaIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(areaIndex, "double:Perimeter"))
    }
    
    async getAllPerimeter(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Perimeter"))
    }
    
    async getNumber(areaIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(areaIndex, "string:Number"))
    }
    
    async getAllNumber(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Number"))
    }
    
    async getIsGrossInterior(areaIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(areaIndex, "byte:IsGrossInterior"))
    }
    
    async getAllIsGrossInterior(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsGrossInterior"))
    }
    
    async getAreaSchemeIndex(areaIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(areaIndex, "index:Vim.AreaScheme:AreaScheme")
    }
    
    async getAllAreaSchemeIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.AreaScheme:AreaScheme")
    }
    
    async getAreaScheme(areaIndex: number): Promise<IAreaScheme | undefined> {
        const index = await this.getAreaSchemeIndex(areaIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.areaScheme?.get(index)
    }
    
    async getElementIndex(areaIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(areaIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(areaIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(areaIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IAreaScheme {
    index: number
    isGrossBuildingArea?: boolean
    
    elementIndex?: number
    element?: IElement
}

export interface IAreaSchemeTable {
    getCount(): Promise<number>
    get(areaSchemeIndex: number): Promise<IAreaScheme>
    getAll(): Promise<IAreaScheme[]>
    
    getIsGrossBuildingArea(areaSchemeIndex: number): Promise<boolean | undefined>
    getAllIsGrossBuildingArea(): Promise<boolean[] | undefined>
    
    getElementIndex(areaSchemeIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(areaSchemeIndex: number): Promise<IElement | undefined>
}

export class AreaScheme implements IAreaScheme {
    index: number
    isGrossBuildingArea?: boolean
    
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IAreaSchemeTable, index: number): Promise<IAreaScheme> {
        let result = new AreaScheme()
        result.index = index
        
        await Promise.all([
            table.getIsGrossBuildingArea(index).then(v => result.isGrossBuildingArea = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class AreaSchemeTable implements IAreaSchemeTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IAreaSchemeTable | undefined> {
        const entity = await document.entities.getBfast("Vim.AreaScheme")
        
        if (!entity) {
            return undefined
        }
        
        let table = new AreaSchemeTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(areaSchemeIndex: number): Promise<IAreaScheme> {
        return await AreaScheme.createFromTable(this, areaSchemeIndex)
    }
    
    async getAll(): Promise<IAreaScheme[]> {
        const localTable = await this.entityTable.getLocal()
        
        let isGrossBuildingArea: boolean[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { isGrossBuildingArea = (await localTable.getBooleanArray("byte:IsGrossBuildingArea")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let areaScheme: IAreaScheme[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            areaScheme.push({
                index: i,
                isGrossBuildingArea: isGrossBuildingArea ? isGrossBuildingArea[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return areaScheme
    }
    
    async getIsGrossBuildingArea(areaSchemeIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(areaSchemeIndex, "byte:IsGrossBuildingArea"))
    }
    
    async getAllIsGrossBuildingArea(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsGrossBuildingArea"))
    }
    
    async getElementIndex(areaSchemeIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(areaSchemeIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(areaSchemeIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(areaSchemeIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface ISchedule {
    index: number
    
    elementIndex?: number
    element?: IElement
}

export interface IScheduleTable {
    getCount(): Promise<number>
    get(scheduleIndex: number): Promise<ISchedule>
    getAll(): Promise<ISchedule[]>
    
    getElementIndex(scheduleIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(scheduleIndex: number): Promise<IElement | undefined>
}

export class Schedule implements ISchedule {
    index: number
    
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IScheduleTable, index: number): Promise<ISchedule> {
        let result = new Schedule()
        result.index = index
        
        await Promise.all([
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class ScheduleTable implements IScheduleTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IScheduleTable | undefined> {
        const entity = await document.entities.getBfast("Vim.Schedule")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ScheduleTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(scheduleIndex: number): Promise<ISchedule> {
        return await Schedule.createFromTable(this, scheduleIndex)
    }
    
    async getAll(): Promise<ISchedule[]> {
        const localTable = await this.entityTable.getLocal()
        
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let schedule: ISchedule[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            schedule.push({
                index: i,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return schedule
    }
    
    async getElementIndex(scheduleIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(scheduleIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(scheduleIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(scheduleIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IScheduleColumn {
    index: number
    name?: string
    columnIndex?: number
    
    scheduleIndex?: number
    schedule?: ISchedule
}

export interface IScheduleColumnTable {
    getCount(): Promise<number>
    get(scheduleColumnIndex: number): Promise<IScheduleColumn>
    getAll(): Promise<IScheduleColumn[]>
    
    getName(scheduleColumnIndex: number): Promise<string | undefined>
    getAllName(): Promise<string[] | undefined>
    getColumnIndex(scheduleColumnIndex: number): Promise<number | undefined>
    getAllColumnIndex(): Promise<number[] | undefined>
    
    getScheduleIndex(scheduleColumnIndex: number): Promise<number | undefined>
    getAllScheduleIndex(): Promise<number[] | undefined>
    getSchedule(scheduleColumnIndex: number): Promise<ISchedule | undefined>
}

export class ScheduleColumn implements IScheduleColumn {
    index: number
    name?: string
    columnIndex?: number
    
    scheduleIndex?: number
    schedule?: ISchedule
    
    static async createFromTable(table: IScheduleColumnTable, index: number): Promise<IScheduleColumn> {
        let result = new ScheduleColumn()
        result.index = index
        
        await Promise.all([
            table.getName(index).then(v => result.name = v),
            table.getColumnIndex(index).then(v => result.columnIndex = v),
            table.getScheduleIndex(index).then(v => result.scheduleIndex = v),
        ])
        
        return result
    }
}

export class ScheduleColumnTable implements IScheduleColumnTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IScheduleColumnTable | undefined> {
        const entity = await document.entities.getBfast("Vim.ScheduleColumn")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ScheduleColumnTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(scheduleColumnIndex: number): Promise<IScheduleColumn> {
        return await ScheduleColumn.createFromTable(this, scheduleColumnIndex)
    }
    
    async getAll(): Promise<IScheduleColumn[]> {
        const localTable = await this.entityTable.getLocal()
        
        let name: string[] | undefined
        let columnIndex: number[] | undefined
        let scheduleIndex: number[] | undefined
        
        await Promise.all([
            (async () => { name = (await localTable.getStringArray("string:Name")) })(),
            (async () => { columnIndex = (await localTable.getNumberArray("int:ColumnIndex")) })(),
            (async () => { scheduleIndex = (await localTable.getNumberArray("index:Vim.Schedule:Schedule")) })(),
        ])
        
        let scheduleColumn: IScheduleColumn[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            scheduleColumn.push({
                index: i,
                name: name ? name[i] : undefined,
                columnIndex: columnIndex ? columnIndex[i] : undefined,
                scheduleIndex: scheduleIndex ? scheduleIndex[i] : undefined
            })
        }
        
        return scheduleColumn
    }
    
    async getName(scheduleColumnIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(scheduleColumnIndex, "string:Name"))
    }
    
    async getAllName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Name"))
    }
    
    async getColumnIndex(scheduleColumnIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(scheduleColumnIndex, "int:ColumnIndex"))
    }
    
    async getAllColumnIndex(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:ColumnIndex"))
    }
    
    async getScheduleIndex(scheduleColumnIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(scheduleColumnIndex, "index:Vim.Schedule:Schedule")
    }
    
    async getAllScheduleIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Schedule:Schedule")
    }
    
    async getSchedule(scheduleColumnIndex: number): Promise<ISchedule | undefined> {
        const index = await this.getScheduleIndex(scheduleColumnIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.schedule?.get(index)
    }
    
}

export interface IScheduleCell {
    index: number
    value?: string
    rowIndex?: number
    
    scheduleColumnIndex?: number
    scheduleColumn?: IScheduleColumn
}

export interface IScheduleCellTable {
    getCount(): Promise<number>
    get(scheduleCellIndex: number): Promise<IScheduleCell>
    getAll(): Promise<IScheduleCell[]>
    
    getValue(scheduleCellIndex: number): Promise<string | undefined>
    getAllValue(): Promise<string[] | undefined>
    getRowIndex(scheduleCellIndex: number): Promise<number | undefined>
    getAllRowIndex(): Promise<number[] | undefined>
    
    getScheduleColumnIndex(scheduleCellIndex: number): Promise<number | undefined>
    getAllScheduleColumnIndex(): Promise<number[] | undefined>
    getScheduleColumn(scheduleCellIndex: number): Promise<IScheduleColumn | undefined>
}

export class ScheduleCell implements IScheduleCell {
    index: number
    value?: string
    rowIndex?: number
    
    scheduleColumnIndex?: number
    scheduleColumn?: IScheduleColumn
    
    static async createFromTable(table: IScheduleCellTable, index: number): Promise<IScheduleCell> {
        let result = new ScheduleCell()
        result.index = index
        
        await Promise.all([
            table.getValue(index).then(v => result.value = v),
            table.getRowIndex(index).then(v => result.rowIndex = v),
            table.getScheduleColumnIndex(index).then(v => result.scheduleColumnIndex = v),
        ])
        
        return result
    }
}

export class ScheduleCellTable implements IScheduleCellTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IScheduleCellTable | undefined> {
        const entity = await document.entities.getBfast("Vim.ScheduleCell")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ScheduleCellTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(scheduleCellIndex: number): Promise<IScheduleCell> {
        return await ScheduleCell.createFromTable(this, scheduleCellIndex)
    }
    
    async getAll(): Promise<IScheduleCell[]> {
        const localTable = await this.entityTable.getLocal()
        
        let value: string[] | undefined
        let rowIndex: number[] | undefined
        let scheduleColumnIndex: number[] | undefined
        
        await Promise.all([
            (async () => { value = (await localTable.getStringArray("string:Value")) })(),
            (async () => { rowIndex = (await localTable.getNumberArray("int:RowIndex")) })(),
            (async () => { scheduleColumnIndex = (await localTable.getNumberArray("index:Vim.ScheduleColumn:ScheduleColumn")) })(),
        ])
        
        let scheduleCell: IScheduleCell[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            scheduleCell.push({
                index: i,
                value: value ? value[i] : undefined,
                rowIndex: rowIndex ? rowIndex[i] : undefined,
                scheduleColumnIndex: scheduleColumnIndex ? scheduleColumnIndex[i] : undefined
            })
        }
        
        return scheduleCell
    }
    
    async getValue(scheduleCellIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(scheduleCellIndex, "string:Value"))
    }
    
    async getAllValue(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Value"))
    }
    
    async getRowIndex(scheduleCellIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(scheduleCellIndex, "int:RowIndex"))
    }
    
    async getAllRowIndex(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:RowIndex"))
    }
    
    async getScheduleColumnIndex(scheduleCellIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(scheduleCellIndex, "index:Vim.ScheduleColumn:ScheduleColumn")
    }
    
    async getAllScheduleColumnIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.ScheduleColumn:ScheduleColumn")
    }
    
    async getScheduleColumn(scheduleCellIndex: number): Promise<IScheduleColumn | undefined> {
        const index = await this.getScheduleColumnIndex(scheduleCellIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.scheduleColumn?.get(index)
    }
    
}

export interface IViewSheetSet {
    index: number
    
    elementIndex?: number
    element?: IElement
}

export interface IViewSheetSetTable {
    getCount(): Promise<number>
    get(viewSheetSetIndex: number): Promise<IViewSheetSet>
    getAll(): Promise<IViewSheetSet[]>
    
    getElementIndex(viewSheetSetIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(viewSheetSetIndex: number): Promise<IElement | undefined>
}

export class ViewSheetSet implements IViewSheetSet {
    index: number
    
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IViewSheetSetTable, index: number): Promise<IViewSheetSet> {
        let result = new ViewSheetSet()
        result.index = index
        
        await Promise.all([
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class ViewSheetSetTable implements IViewSheetSetTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IViewSheetSetTable | undefined> {
        const entity = await document.entities.getBfast("Vim.ViewSheetSet")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ViewSheetSetTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(viewSheetSetIndex: number): Promise<IViewSheetSet> {
        return await ViewSheetSet.createFromTable(this, viewSheetSetIndex)
    }
    
    async getAll(): Promise<IViewSheetSet[]> {
        const localTable = await this.entityTable.getLocal()
        
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let viewSheetSet: IViewSheetSet[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            viewSheetSet.push({
                index: i,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return viewSheetSet
    }
    
    async getElementIndex(viewSheetSetIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(viewSheetSetIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(viewSheetSetIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(viewSheetSetIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IViewSheet {
    index: number
    
    familyTypeIndex?: number
    familyType?: IFamilyType
    elementIndex?: number
    element?: IElement
}

export interface IViewSheetTable {
    getCount(): Promise<number>
    get(viewSheetIndex: number): Promise<IViewSheet>
    getAll(): Promise<IViewSheet[]>
    
    getFamilyTypeIndex(viewSheetIndex: number): Promise<number | undefined>
    getAllFamilyTypeIndex(): Promise<number[] | undefined>
    getFamilyType(viewSheetIndex: number): Promise<IFamilyType | undefined>
    getElementIndex(viewSheetIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(viewSheetIndex: number): Promise<IElement | undefined>
}

export class ViewSheet implements IViewSheet {
    index: number
    
    familyTypeIndex?: number
    familyType?: IFamilyType
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IViewSheetTable, index: number): Promise<IViewSheet> {
        let result = new ViewSheet()
        result.index = index
        
        await Promise.all([
            table.getFamilyTypeIndex(index).then(v => result.familyTypeIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class ViewSheetTable implements IViewSheetTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IViewSheetTable | undefined> {
        const entity = await document.entities.getBfast("Vim.ViewSheet")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ViewSheetTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(viewSheetIndex: number): Promise<IViewSheet> {
        return await ViewSheet.createFromTable(this, viewSheetIndex)
    }
    
    async getAll(): Promise<IViewSheet[]> {
        const localTable = await this.entityTable.getLocal()
        
        let familyTypeIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { familyTypeIndex = (await localTable.getNumberArray("index:Vim.FamilyType:FamilyType")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let viewSheet: IViewSheet[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            viewSheet.push({
                index: i,
                familyTypeIndex: familyTypeIndex ? familyTypeIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return viewSheet
    }
    
    async getFamilyTypeIndex(viewSheetIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(viewSheetIndex, "index:Vim.FamilyType:FamilyType")
    }
    
    async getAllFamilyTypeIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.FamilyType:FamilyType")
    }
    
    async getFamilyType(viewSheetIndex: number): Promise<IFamilyType | undefined> {
        const index = await this.getFamilyTypeIndex(viewSheetIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.familyType?.get(index)
    }
    
    async getElementIndex(viewSheetIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(viewSheetIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(viewSheetIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(viewSheetIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IViewSheetInViewSheetSet {
    index: number
    
    viewSheetIndex?: number
    viewSheet?: IViewSheet
    viewSheetSetIndex?: number
    viewSheetSet?: IViewSheetSet
}

export interface IViewSheetInViewSheetSetTable {
    getCount(): Promise<number>
    get(viewSheetInViewSheetSetIndex: number): Promise<IViewSheetInViewSheetSet>
    getAll(): Promise<IViewSheetInViewSheetSet[]>
    
    getViewSheetIndex(viewSheetInViewSheetSetIndex: number): Promise<number | undefined>
    getAllViewSheetIndex(): Promise<number[] | undefined>
    getViewSheet(viewSheetInViewSheetSetIndex: number): Promise<IViewSheet | undefined>
    getViewSheetSetIndex(viewSheetInViewSheetSetIndex: number): Promise<number | undefined>
    getAllViewSheetSetIndex(): Promise<number[] | undefined>
    getViewSheetSet(viewSheetInViewSheetSetIndex: number): Promise<IViewSheetSet | undefined>
}

export class ViewSheetInViewSheetSet implements IViewSheetInViewSheetSet {
    index: number
    
    viewSheetIndex?: number
    viewSheet?: IViewSheet
    viewSheetSetIndex?: number
    viewSheetSet?: IViewSheetSet
    
    static async createFromTable(table: IViewSheetInViewSheetSetTable, index: number): Promise<IViewSheetInViewSheetSet> {
        let result = new ViewSheetInViewSheetSet()
        result.index = index
        
        await Promise.all([
            table.getViewSheetIndex(index).then(v => result.viewSheetIndex = v),
            table.getViewSheetSetIndex(index).then(v => result.viewSheetSetIndex = v),
        ])
        
        return result
    }
}

export class ViewSheetInViewSheetSetTable implements IViewSheetInViewSheetSetTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IViewSheetInViewSheetSetTable | undefined> {
        const entity = await document.entities.getBfast("Vim.ViewSheetInViewSheetSet")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ViewSheetInViewSheetSetTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(viewSheetInViewSheetSetIndex: number): Promise<IViewSheetInViewSheetSet> {
        return await ViewSheetInViewSheetSet.createFromTable(this, viewSheetInViewSheetSetIndex)
    }
    
    async getAll(): Promise<IViewSheetInViewSheetSet[]> {
        const localTable = await this.entityTable.getLocal()
        
        let viewSheetIndex: number[] | undefined
        let viewSheetSetIndex: number[] | undefined
        
        await Promise.all([
            (async () => { viewSheetIndex = (await localTable.getNumberArray("index:Vim.ViewSheet:ViewSheet")) })(),
            (async () => { viewSheetSetIndex = (await localTable.getNumberArray("index:Vim.ViewSheetSet:ViewSheetSet")) })(),
        ])
        
        let viewSheetInViewSheetSet: IViewSheetInViewSheetSet[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            viewSheetInViewSheetSet.push({
                index: i,
                viewSheetIndex: viewSheetIndex ? viewSheetIndex[i] : undefined,
                viewSheetSetIndex: viewSheetSetIndex ? viewSheetSetIndex[i] : undefined
            })
        }
        
        return viewSheetInViewSheetSet
    }
    
    async getViewSheetIndex(viewSheetInViewSheetSetIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(viewSheetInViewSheetSetIndex, "index:Vim.ViewSheet:ViewSheet")
    }
    
    async getAllViewSheetIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.ViewSheet:ViewSheet")
    }
    
    async getViewSheet(viewSheetInViewSheetSetIndex: number): Promise<IViewSheet | undefined> {
        const index = await this.getViewSheetIndex(viewSheetInViewSheetSetIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.viewSheet?.get(index)
    }
    
    async getViewSheetSetIndex(viewSheetInViewSheetSetIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(viewSheetInViewSheetSetIndex, "index:Vim.ViewSheetSet:ViewSheetSet")
    }
    
    async getAllViewSheetSetIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.ViewSheetSet:ViewSheetSet")
    }
    
    async getViewSheetSet(viewSheetInViewSheetSetIndex: number): Promise<IViewSheetSet | undefined> {
        const index = await this.getViewSheetSetIndex(viewSheetInViewSheetSetIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.viewSheetSet?.get(index)
    }
    
}

export interface IViewInViewSheetSet {
    index: number
    
    viewIndex?: number
    view?: IView
    viewSheetSetIndex?: number
    viewSheetSet?: IViewSheetSet
}

export interface IViewInViewSheetSetTable {
    getCount(): Promise<number>
    get(viewInViewSheetSetIndex: number): Promise<IViewInViewSheetSet>
    getAll(): Promise<IViewInViewSheetSet[]>
    
    getViewIndex(viewInViewSheetSetIndex: number): Promise<number | undefined>
    getAllViewIndex(): Promise<number[] | undefined>
    getView(viewInViewSheetSetIndex: number): Promise<IView | undefined>
    getViewSheetSetIndex(viewInViewSheetSetIndex: number): Promise<number | undefined>
    getAllViewSheetSetIndex(): Promise<number[] | undefined>
    getViewSheetSet(viewInViewSheetSetIndex: number): Promise<IViewSheetSet | undefined>
}

export class ViewInViewSheetSet implements IViewInViewSheetSet {
    index: number
    
    viewIndex?: number
    view?: IView
    viewSheetSetIndex?: number
    viewSheetSet?: IViewSheetSet
    
    static async createFromTable(table: IViewInViewSheetSetTable, index: number): Promise<IViewInViewSheetSet> {
        let result = new ViewInViewSheetSet()
        result.index = index
        
        await Promise.all([
            table.getViewIndex(index).then(v => result.viewIndex = v),
            table.getViewSheetSetIndex(index).then(v => result.viewSheetSetIndex = v),
        ])
        
        return result
    }
}

export class ViewInViewSheetSetTable implements IViewInViewSheetSetTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IViewInViewSheetSetTable | undefined> {
        const entity = await document.entities.getBfast("Vim.ViewInViewSheetSet")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ViewInViewSheetSetTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(viewInViewSheetSetIndex: number): Promise<IViewInViewSheetSet> {
        return await ViewInViewSheetSet.createFromTable(this, viewInViewSheetSetIndex)
    }
    
    async getAll(): Promise<IViewInViewSheetSet[]> {
        const localTable = await this.entityTable.getLocal()
        
        let viewIndex: number[] | undefined
        let viewSheetSetIndex: number[] | undefined
        
        await Promise.all([
            (async () => { viewIndex = (await localTable.getNumberArray("index:Vim.View:View")) })(),
            (async () => { viewSheetSetIndex = (await localTable.getNumberArray("index:Vim.ViewSheetSet:ViewSheetSet")) })(),
        ])
        
        let viewInViewSheetSet: IViewInViewSheetSet[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            viewInViewSheetSet.push({
                index: i,
                viewIndex: viewIndex ? viewIndex[i] : undefined,
                viewSheetSetIndex: viewSheetSetIndex ? viewSheetSetIndex[i] : undefined
            })
        }
        
        return viewInViewSheetSet
    }
    
    async getViewIndex(viewInViewSheetSetIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(viewInViewSheetSetIndex, "index:Vim.View:View")
    }
    
    async getAllViewIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.View:View")
    }
    
    async getView(viewInViewSheetSetIndex: number): Promise<IView | undefined> {
        const index = await this.getViewIndex(viewInViewSheetSetIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.view?.get(index)
    }
    
    async getViewSheetSetIndex(viewInViewSheetSetIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(viewInViewSheetSetIndex, "index:Vim.ViewSheetSet:ViewSheetSet")
    }
    
    async getAllViewSheetSetIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.ViewSheetSet:ViewSheetSet")
    }
    
    async getViewSheetSet(viewInViewSheetSetIndex: number): Promise<IViewSheetSet | undefined> {
        const index = await this.getViewSheetSetIndex(viewInViewSheetSetIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.viewSheetSet?.get(index)
    }
    
}

export interface IViewInViewSheet {
    index: number
    
    viewIndex?: number
    view?: IView
    viewSheetIndex?: number
    viewSheet?: IViewSheet
}

export interface IViewInViewSheetTable {
    getCount(): Promise<number>
    get(viewInViewSheetIndex: number): Promise<IViewInViewSheet>
    getAll(): Promise<IViewInViewSheet[]>
    
    getViewIndex(viewInViewSheetIndex: number): Promise<number | undefined>
    getAllViewIndex(): Promise<number[] | undefined>
    getView(viewInViewSheetIndex: number): Promise<IView | undefined>
    getViewSheetIndex(viewInViewSheetIndex: number): Promise<number | undefined>
    getAllViewSheetIndex(): Promise<number[] | undefined>
    getViewSheet(viewInViewSheetIndex: number): Promise<IViewSheet | undefined>
}

export class ViewInViewSheet implements IViewInViewSheet {
    index: number
    
    viewIndex?: number
    view?: IView
    viewSheetIndex?: number
    viewSheet?: IViewSheet
    
    static async createFromTable(table: IViewInViewSheetTable, index: number): Promise<IViewInViewSheet> {
        let result = new ViewInViewSheet()
        result.index = index
        
        await Promise.all([
            table.getViewIndex(index).then(v => result.viewIndex = v),
            table.getViewSheetIndex(index).then(v => result.viewSheetIndex = v),
        ])
        
        return result
    }
}

export class ViewInViewSheetTable implements IViewInViewSheetTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IViewInViewSheetTable | undefined> {
        const entity = await document.entities.getBfast("Vim.ViewInViewSheet")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ViewInViewSheetTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(viewInViewSheetIndex: number): Promise<IViewInViewSheet> {
        return await ViewInViewSheet.createFromTable(this, viewInViewSheetIndex)
    }
    
    async getAll(): Promise<IViewInViewSheet[]> {
        const localTable = await this.entityTable.getLocal()
        
        let viewIndex: number[] | undefined
        let viewSheetIndex: number[] | undefined
        
        await Promise.all([
            (async () => { viewIndex = (await localTable.getNumberArray("index:Vim.View:View")) })(),
            (async () => { viewSheetIndex = (await localTable.getNumberArray("index:Vim.ViewSheet:ViewSheet")) })(),
        ])
        
        let viewInViewSheet: IViewInViewSheet[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            viewInViewSheet.push({
                index: i,
                viewIndex: viewIndex ? viewIndex[i] : undefined,
                viewSheetIndex: viewSheetIndex ? viewSheetIndex[i] : undefined
            })
        }
        
        return viewInViewSheet
    }
    
    async getViewIndex(viewInViewSheetIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(viewInViewSheetIndex, "index:Vim.View:View")
    }
    
    async getAllViewIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.View:View")
    }
    
    async getView(viewInViewSheetIndex: number): Promise<IView | undefined> {
        const index = await this.getViewIndex(viewInViewSheetIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.view?.get(index)
    }
    
    async getViewSheetIndex(viewInViewSheetIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(viewInViewSheetIndex, "index:Vim.ViewSheet:ViewSheet")
    }
    
    async getAllViewSheetIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.ViewSheet:ViewSheet")
    }
    
    async getViewSheet(viewInViewSheetIndex: number): Promise<IViewSheet | undefined> {
        const index = await this.getViewSheetIndex(viewInViewSheetIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.viewSheet?.get(index)
    }
    
}

export interface ISite {
    index: number
    latitude?: number
    longitude?: number
    address?: string
    elevation?: number
    number?: string
    
    elementIndex?: number
    element?: IElement
}

export interface ISiteTable {
    getCount(): Promise<number>
    get(siteIndex: number): Promise<ISite>
    getAll(): Promise<ISite[]>
    
    getLatitude(siteIndex: number): Promise<number | undefined>
    getAllLatitude(): Promise<number[] | undefined>
    getLongitude(siteIndex: number): Promise<number | undefined>
    getAllLongitude(): Promise<number[] | undefined>
    getAddress(siteIndex: number): Promise<string | undefined>
    getAllAddress(): Promise<string[] | undefined>
    getElevation(siteIndex: number): Promise<number | undefined>
    getAllElevation(): Promise<number[] | undefined>
    getNumber(siteIndex: number): Promise<string | undefined>
    getAllNumber(): Promise<string[] | undefined>
    
    getElementIndex(siteIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(siteIndex: number): Promise<IElement | undefined>
}

export class Site implements ISite {
    index: number
    latitude?: number
    longitude?: number
    address?: string
    elevation?: number
    number?: string
    
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: ISiteTable, index: number): Promise<ISite> {
        let result = new Site()
        result.index = index
        
        await Promise.all([
            table.getLatitude(index).then(v => result.latitude = v),
            table.getLongitude(index).then(v => result.longitude = v),
            table.getAddress(index).then(v => result.address = v),
            table.getElevation(index).then(v => result.elevation = v),
            table.getNumber(index).then(v => result.number = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class SiteTable implements ISiteTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<ISiteTable | undefined> {
        const entity = await document.entities.getBfast("Vim.Site")
        
        if (!entity) {
            return undefined
        }
        
        let table = new SiteTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(siteIndex: number): Promise<ISite> {
        return await Site.createFromTable(this, siteIndex)
    }
    
    async getAll(): Promise<ISite[]> {
        const localTable = await this.entityTable.getLocal()
        
        let latitude: number[] | undefined
        let longitude: number[] | undefined
        let address: string[] | undefined
        let elevation: number[] | undefined
        let number: string[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { latitude = (await localTable.getNumberArray("double:Latitude")) })(),
            (async () => { longitude = (await localTable.getNumberArray("double:Longitude")) })(),
            (async () => { address = (await localTable.getStringArray("string:Address")) })(),
            (async () => { elevation = (await localTable.getNumberArray("double:Elevation")) })(),
            (async () => { number = (await localTable.getStringArray("string:Number")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let site: ISite[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            site.push({
                index: i,
                latitude: latitude ? latitude[i] : undefined,
                longitude: longitude ? longitude[i] : undefined,
                address: address ? address[i] : undefined,
                elevation: elevation ? elevation[i] : undefined,
                number: number ? number[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return site
    }
    
    async getLatitude(siteIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(siteIndex, "double:Latitude"))
    }
    
    async getAllLatitude(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Latitude"))
    }
    
    async getLongitude(siteIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(siteIndex, "double:Longitude"))
    }
    
    async getAllLongitude(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Longitude"))
    }
    
    async getAddress(siteIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(siteIndex, "string:Address"))
    }
    
    async getAllAddress(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Address"))
    }
    
    async getElevation(siteIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(siteIndex, "double:Elevation"))
    }
    
    async getAllElevation(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Elevation"))
    }
    
    async getNumber(siteIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(siteIndex, "string:Number"))
    }
    
    async getAllNumber(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Number"))
    }
    
    async getElementIndex(siteIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(siteIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(siteIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(siteIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IBuilding {
    index: number
    elevation?: number
    terrainElevation?: number
    address?: string
    
    siteIndex?: number
    site?: ISite
    elementIndex?: number
    element?: IElement
}

export interface IBuildingTable {
    getCount(): Promise<number>
    get(buildingIndex: number): Promise<IBuilding>
    getAll(): Promise<IBuilding[]>
    
    getElevation(buildingIndex: number): Promise<number | undefined>
    getAllElevation(): Promise<number[] | undefined>
    getTerrainElevation(buildingIndex: number): Promise<number | undefined>
    getAllTerrainElevation(): Promise<number[] | undefined>
    getAddress(buildingIndex: number): Promise<string | undefined>
    getAllAddress(): Promise<string[] | undefined>
    
    getSiteIndex(buildingIndex: number): Promise<number | undefined>
    getAllSiteIndex(): Promise<number[] | undefined>
    getSite(buildingIndex: number): Promise<ISite | undefined>
    getElementIndex(buildingIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(buildingIndex: number): Promise<IElement | undefined>
}

export class Building implements IBuilding {
    index: number
    elevation?: number
    terrainElevation?: number
    address?: string
    
    siteIndex?: number
    site?: ISite
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IBuildingTable, index: number): Promise<IBuilding> {
        let result = new Building()
        result.index = index
        
        await Promise.all([
            table.getElevation(index).then(v => result.elevation = v),
            table.getTerrainElevation(index).then(v => result.terrainElevation = v),
            table.getAddress(index).then(v => result.address = v),
            table.getSiteIndex(index).then(v => result.siteIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class BuildingTable implements IBuildingTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IBuildingTable | undefined> {
        const entity = await document.entities.getBfast("Vim.Building")
        
        if (!entity) {
            return undefined
        }
        
        let table = new BuildingTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(buildingIndex: number): Promise<IBuilding> {
        return await Building.createFromTable(this, buildingIndex)
    }
    
    async getAll(): Promise<IBuilding[]> {
        const localTable = await this.entityTable.getLocal()
        
        let elevation: number[] | undefined
        let terrainElevation: number[] | undefined
        let address: string[] | undefined
        let siteIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { elevation = (await localTable.getNumberArray("double:Elevation")) })(),
            (async () => { terrainElevation = (await localTable.getNumberArray("double:TerrainElevation")) })(),
            (async () => { address = (await localTable.getStringArray("string:Address")) })(),
            (async () => { siteIndex = (await localTable.getNumberArray("index:Vim.Site:Site")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let building: IBuilding[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            building.push({
                index: i,
                elevation: elevation ? elevation[i] : undefined,
                terrainElevation: terrainElevation ? terrainElevation[i] : undefined,
                address: address ? address[i] : undefined,
                siteIndex: siteIndex ? siteIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return building
    }
    
    async getElevation(buildingIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(buildingIndex, "double:Elevation"))
    }
    
    async getAllElevation(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Elevation"))
    }
    
    async getTerrainElevation(buildingIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(buildingIndex, "double:TerrainElevation"))
    }
    
    async getAllTerrainElevation(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:TerrainElevation"))
    }
    
    async getAddress(buildingIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(buildingIndex, "string:Address"))
    }
    
    async getAllAddress(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Address"))
    }
    
    async getSiteIndex(buildingIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(buildingIndex, "index:Vim.Site:Site")
    }
    
    async getAllSiteIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Site:Site")
    }
    
    async getSite(buildingIndex: number): Promise<ISite | undefined> {
        const index = await this.getSiteIndex(buildingIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.site?.get(index)
    }
    
    async getElementIndex(buildingIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(buildingIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(buildingIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(buildingIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface ILightType {
    index: number
    lightShapeStyle?: number
    emitLength?: number
    emitWidth?: number
    emitDiameter?: number
    lightDistributionStyle?: number
    spotBeamAngle?: number
    spotFieldAngle?: number
    tiltAngle?: number
    photometricWebFileName?: string
    initialIntensityType?: number
    luminosity?: number
    flux?: number
    wattage?: number
    efficacy?: number
    illuminance?: number
    illuminanceDistance?: number
    initialColorTemperature?: number
    colorFilter_X?: number
    colorFilter_Y?: number
    colorFilter_Z?: number
    lossFactor?: number
    dimmingColor?: number
    
    photometricWebFileIndex?: number
    photometricWebFile?: IAsset
    familyTypeIndex?: number
    familyType?: IFamilyType
}

export interface ILightTypeTable {
    getCount(): Promise<number>
    get(lightTypeIndex: number): Promise<ILightType>
    getAll(): Promise<ILightType[]>
    
    getLightShapeStyle(lightTypeIndex: number): Promise<number | undefined>
    getAllLightShapeStyle(): Promise<number[] | undefined>
    getEmitLength(lightTypeIndex: number): Promise<number | undefined>
    getAllEmitLength(): Promise<number[] | undefined>
    getEmitWidth(lightTypeIndex: number): Promise<number | undefined>
    getAllEmitWidth(): Promise<number[] | undefined>
    getEmitDiameter(lightTypeIndex: number): Promise<number | undefined>
    getAllEmitDiameter(): Promise<number[] | undefined>
    getLightDistributionStyle(lightTypeIndex: number): Promise<number | undefined>
    getAllLightDistributionStyle(): Promise<number[] | undefined>
    getSpotBeamAngle(lightTypeIndex: number): Promise<number | undefined>
    getAllSpotBeamAngle(): Promise<number[] | undefined>
    getSpotFieldAngle(lightTypeIndex: number): Promise<number | undefined>
    getAllSpotFieldAngle(): Promise<number[] | undefined>
    getTiltAngle(lightTypeIndex: number): Promise<number | undefined>
    getAllTiltAngle(): Promise<number[] | undefined>
    getPhotometricWebFileName(lightTypeIndex: number): Promise<string | undefined>
    getAllPhotometricWebFileName(): Promise<string[] | undefined>
    getInitialIntensityType(lightTypeIndex: number): Promise<number | undefined>
    getAllInitialIntensityType(): Promise<number[] | undefined>
    getLuminosity(lightTypeIndex: number): Promise<number | undefined>
    getAllLuminosity(): Promise<number[] | undefined>
    getFlux(lightTypeIndex: number): Promise<number | undefined>
    getAllFlux(): Promise<number[] | undefined>
    getWattage(lightTypeIndex: number): Promise<number | undefined>
    getAllWattage(): Promise<number[] | undefined>
    getEfficacy(lightTypeIndex: number): Promise<number | undefined>
    getAllEfficacy(): Promise<number[] | undefined>
    getIlluminance(lightTypeIndex: number): Promise<number | undefined>
    getAllIlluminance(): Promise<number[] | undefined>
    getIlluminanceDistance(lightTypeIndex: number): Promise<number | undefined>
    getAllIlluminanceDistance(): Promise<number[] | undefined>
    getInitialColorTemperature(lightTypeIndex: number): Promise<number | undefined>
    getAllInitialColorTemperature(): Promise<number[] | undefined>
    getColorFilter_X(lightTypeIndex: number): Promise<number | undefined>
    getAllColorFilter_X(): Promise<number[] | undefined>
    getColorFilter_Y(lightTypeIndex: number): Promise<number | undefined>
    getAllColorFilter_Y(): Promise<number[] | undefined>
    getColorFilter_Z(lightTypeIndex: number): Promise<number | undefined>
    getAllColorFilter_Z(): Promise<number[] | undefined>
    getLossFactor(lightTypeIndex: number): Promise<number | undefined>
    getAllLossFactor(): Promise<number[] | undefined>
    getDimmingColor(lightTypeIndex: number): Promise<number | undefined>
    getAllDimmingColor(): Promise<number[] | undefined>
    
    getPhotometricWebFileIndex(lightTypeIndex: number): Promise<number | undefined>
    getAllPhotometricWebFileIndex(): Promise<number[] | undefined>
    getPhotometricWebFile(lightTypeIndex: number): Promise<IAsset | undefined>
    getFamilyTypeIndex(lightTypeIndex: number): Promise<number | undefined>
    getAllFamilyTypeIndex(): Promise<number[] | undefined>
    getFamilyType(lightTypeIndex: number): Promise<IFamilyType | undefined>
}

export class LightType implements ILightType {
    index: number
    lightShapeStyle?: number
    emitLength?: number
    emitWidth?: number
    emitDiameter?: number
    lightDistributionStyle?: number
    spotBeamAngle?: number
    spotFieldAngle?: number
    tiltAngle?: number
    photometricWebFileName?: string
    initialIntensityType?: number
    luminosity?: number
    flux?: number
    wattage?: number
    efficacy?: number
    illuminance?: number
    illuminanceDistance?: number
    initialColorTemperature?: number
    colorFilter_X?: number
    colorFilter_Y?: number
    colorFilter_Z?: number
    lossFactor?: number
    dimmingColor?: number
    
    photometricWebFileIndex?: number
    photometricWebFile?: IAsset
    familyTypeIndex?: number
    familyType?: IFamilyType
    
    static async createFromTable(table: ILightTypeTable, index: number): Promise<ILightType> {
        let result = new LightType()
        result.index = index
        
        await Promise.all([
            table.getLightShapeStyle(index).then(v => result.lightShapeStyle = v),
            table.getEmitLength(index).then(v => result.emitLength = v),
            table.getEmitWidth(index).then(v => result.emitWidth = v),
            table.getEmitDiameter(index).then(v => result.emitDiameter = v),
            table.getLightDistributionStyle(index).then(v => result.lightDistributionStyle = v),
            table.getSpotBeamAngle(index).then(v => result.spotBeamAngle = v),
            table.getSpotFieldAngle(index).then(v => result.spotFieldAngle = v),
            table.getTiltAngle(index).then(v => result.tiltAngle = v),
            table.getPhotometricWebFileName(index).then(v => result.photometricWebFileName = v),
            table.getInitialIntensityType(index).then(v => result.initialIntensityType = v),
            table.getLuminosity(index).then(v => result.luminosity = v),
            table.getFlux(index).then(v => result.flux = v),
            table.getWattage(index).then(v => result.wattage = v),
            table.getEfficacy(index).then(v => result.efficacy = v),
            table.getIlluminance(index).then(v => result.illuminance = v),
            table.getIlluminanceDistance(index).then(v => result.illuminanceDistance = v),
            table.getInitialColorTemperature(index).then(v => result.initialColorTemperature = v),
            table.getColorFilter_X(index).then(v => result.colorFilter_X = v),
            table.getColorFilter_Y(index).then(v => result.colorFilter_Y = v),
            table.getColorFilter_Z(index).then(v => result.colorFilter_Z = v),
            table.getLossFactor(index).then(v => result.lossFactor = v),
            table.getDimmingColor(index).then(v => result.dimmingColor = v),
            table.getPhotometricWebFileIndex(index).then(v => result.photometricWebFileIndex = v),
            table.getFamilyTypeIndex(index).then(v => result.familyTypeIndex = v),
        ])
        
        return result
    }
}

export class LightTypeTable implements ILightTypeTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<ILightTypeTable | undefined> {
        const entity = await document.entities.getBfast("Vim.LightType")
        
        if (!entity) {
            return undefined
        }
        
        let table = new LightTypeTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(lightTypeIndex: number): Promise<ILightType> {
        return await LightType.createFromTable(this, lightTypeIndex)
    }
    
    async getAll(): Promise<ILightType[]> {
        const localTable = await this.entityTable.getLocal()
        
        let lightShapeStyle: number[] | undefined
        let emitLength: number[] | undefined
        let emitWidth: number[] | undefined
        let emitDiameter: number[] | undefined
        let lightDistributionStyle: number[] | undefined
        let spotBeamAngle: number[] | undefined
        let spotFieldAngle: number[] | undefined
        let tiltAngle: number[] | undefined
        let photometricWebFileName: string[] | undefined
        let initialIntensityType: number[] | undefined
        let luminosity: number[] | undefined
        let flux: number[] | undefined
        let wattage: number[] | undefined
        let efficacy: number[] | undefined
        let illuminance: number[] | undefined
        let illuminanceDistance: number[] | undefined
        let initialColorTemperature: number[] | undefined
        let colorFilter_X: number[] | undefined
        let colorFilter_Y: number[] | undefined
        let colorFilter_Z: number[] | undefined
        let lossFactor: number[] | undefined
        let dimmingColor: number[] | undefined
        let photometricWebFileIndex: number[] | undefined
        let familyTypeIndex: number[] | undefined
        
        await Promise.all([
            (async () => { lightShapeStyle = (await localTable.getNumberArray("int:LightShapeStyle")) })(),
            (async () => { emitLength = (await localTable.getNumberArray("double:EmitLength")) })(),
            (async () => { emitWidth = (await localTable.getNumberArray("double:EmitWidth")) })(),
            (async () => { emitDiameter = (await localTable.getNumberArray("double:EmitDiameter")) })(),
            (async () => { lightDistributionStyle = (await localTable.getNumberArray("int:LightDistributionStyle")) })(),
            (async () => { spotBeamAngle = (await localTable.getNumberArray("double:SpotBeamAngle")) })(),
            (async () => { spotFieldAngle = (await localTable.getNumberArray("double:SpotFieldAngle")) })(),
            (async () => { tiltAngle = (await localTable.getNumberArray("double:TiltAngle")) })(),
            (async () => { photometricWebFileName = (await localTable.getStringArray("string:PhotometricWebFileName")) })(),
            (async () => { initialIntensityType = (await localTable.getNumberArray("int:InitialIntensityType")) })(),
            (async () => { luminosity = (await localTable.getNumberArray("double:Luminosity")) })(),
            (async () => { flux = (await localTable.getNumberArray("double:Flux")) })(),
            (async () => { wattage = (await localTable.getNumberArray("double:Wattage")) })(),
            (async () => { efficacy = (await localTable.getNumberArray("double:Efficacy")) })(),
            (async () => { illuminance = (await localTable.getNumberArray("double:Illuminance")) })(),
            (async () => { illuminanceDistance = (await localTable.getNumberArray("double:IlluminanceDistance")) })(),
            (async () => { initialColorTemperature = (await localTable.getNumberArray("double:InitialColorTemperature")) })(),
            (async () => { colorFilter_X = (await localTable.getNumberArray("double:ColorFilter.X")) })(),
            (async () => { colorFilter_Y = (await localTable.getNumberArray("double:ColorFilter.Y")) })(),
            (async () => { colorFilter_Z = (await localTable.getNumberArray("double:ColorFilter.Z")) })(),
            (async () => { lossFactor = (await localTable.getNumberArray("double:LossFactor")) })(),
            (async () => { dimmingColor = (await localTable.getNumberArray("int:DimmingColor")) })(),
            (async () => { photometricWebFileIndex = (await localTable.getNumberArray("index:Vim.Asset:PhotometricWebFile")) })(),
            (async () => { familyTypeIndex = (await localTable.getNumberArray("index:Vim.FamilyType:FamilyType")) })(),
        ])
        
        let lightType: ILightType[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            lightType.push({
                index: i,
                lightShapeStyle: lightShapeStyle ? lightShapeStyle[i] : undefined,
                emitLength: emitLength ? emitLength[i] : undefined,
                emitWidth: emitWidth ? emitWidth[i] : undefined,
                emitDiameter: emitDiameter ? emitDiameter[i] : undefined,
                lightDistributionStyle: lightDistributionStyle ? lightDistributionStyle[i] : undefined,
                spotBeamAngle: spotBeamAngle ? spotBeamAngle[i] : undefined,
                spotFieldAngle: spotFieldAngle ? spotFieldAngle[i] : undefined,
                tiltAngle: tiltAngle ? tiltAngle[i] : undefined,
                photometricWebFileName: photometricWebFileName ? photometricWebFileName[i] : undefined,
                initialIntensityType: initialIntensityType ? initialIntensityType[i] : undefined,
                luminosity: luminosity ? luminosity[i] : undefined,
                flux: flux ? flux[i] : undefined,
                wattage: wattage ? wattage[i] : undefined,
                efficacy: efficacy ? efficacy[i] : undefined,
                illuminance: illuminance ? illuminance[i] : undefined,
                illuminanceDistance: illuminanceDistance ? illuminanceDistance[i] : undefined,
                initialColorTemperature: initialColorTemperature ? initialColorTemperature[i] : undefined,
                colorFilter_X: colorFilter_X ? colorFilter_X[i] : undefined,
                colorFilter_Y: colorFilter_Y ? colorFilter_Y[i] : undefined,
                colorFilter_Z: colorFilter_Z ? colorFilter_Z[i] : undefined,
                lossFactor: lossFactor ? lossFactor[i] : undefined,
                dimmingColor: dimmingColor ? dimmingColor[i] : undefined,
                photometricWebFileIndex: photometricWebFileIndex ? photometricWebFileIndex[i] : undefined,
                familyTypeIndex: familyTypeIndex ? familyTypeIndex[i] : undefined
            })
        }
        
        return lightType
    }
    
    async getLightShapeStyle(lightTypeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightTypeIndex, "int:LightShapeStyle"))
    }
    
    async getAllLightShapeStyle(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:LightShapeStyle"))
    }
    
    async getEmitLength(lightTypeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightTypeIndex, "double:EmitLength"))
    }
    
    async getAllEmitLength(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:EmitLength"))
    }
    
    async getEmitWidth(lightTypeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightTypeIndex, "double:EmitWidth"))
    }
    
    async getAllEmitWidth(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:EmitWidth"))
    }
    
    async getEmitDiameter(lightTypeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightTypeIndex, "double:EmitDiameter"))
    }
    
    async getAllEmitDiameter(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:EmitDiameter"))
    }
    
    async getLightDistributionStyle(lightTypeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightTypeIndex, "int:LightDistributionStyle"))
    }
    
    async getAllLightDistributionStyle(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:LightDistributionStyle"))
    }
    
    async getSpotBeamAngle(lightTypeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightTypeIndex, "double:SpotBeamAngle"))
    }
    
    async getAllSpotBeamAngle(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:SpotBeamAngle"))
    }
    
    async getSpotFieldAngle(lightTypeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightTypeIndex, "double:SpotFieldAngle"))
    }
    
    async getAllSpotFieldAngle(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:SpotFieldAngle"))
    }
    
    async getTiltAngle(lightTypeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightTypeIndex, "double:TiltAngle"))
    }
    
    async getAllTiltAngle(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:TiltAngle"))
    }
    
    async getPhotometricWebFileName(lightTypeIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(lightTypeIndex, "string:PhotometricWebFileName"))
    }
    
    async getAllPhotometricWebFileName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:PhotometricWebFileName"))
    }
    
    async getInitialIntensityType(lightTypeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightTypeIndex, "int:InitialIntensityType"))
    }
    
    async getAllInitialIntensityType(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:InitialIntensityType"))
    }
    
    async getLuminosity(lightTypeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightTypeIndex, "double:Luminosity"))
    }
    
    async getAllLuminosity(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Luminosity"))
    }
    
    async getFlux(lightTypeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightTypeIndex, "double:Flux"))
    }
    
    async getAllFlux(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Flux"))
    }
    
    async getWattage(lightTypeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightTypeIndex, "double:Wattage"))
    }
    
    async getAllWattage(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Wattage"))
    }
    
    async getEfficacy(lightTypeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightTypeIndex, "double:Efficacy"))
    }
    
    async getAllEfficacy(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Efficacy"))
    }
    
    async getIlluminance(lightTypeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightTypeIndex, "double:Illuminance"))
    }
    
    async getAllIlluminance(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Illuminance"))
    }
    
    async getIlluminanceDistance(lightTypeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightTypeIndex, "double:IlluminanceDistance"))
    }
    
    async getAllIlluminanceDistance(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:IlluminanceDistance"))
    }
    
    async getInitialColorTemperature(lightTypeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightTypeIndex, "double:InitialColorTemperature"))
    }
    
    async getAllInitialColorTemperature(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:InitialColorTemperature"))
    }
    
    async getColorFilter_X(lightTypeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightTypeIndex, "double:ColorFilter.X"))
    }
    
    async getAllColorFilter_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:ColorFilter.X"))
    }
    
    async getColorFilter_Y(lightTypeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightTypeIndex, "double:ColorFilter.Y"))
    }
    
    async getAllColorFilter_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:ColorFilter.Y"))
    }
    
    async getColorFilter_Z(lightTypeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightTypeIndex, "double:ColorFilter.Z"))
    }
    
    async getAllColorFilter_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:ColorFilter.Z"))
    }
    
    async getLossFactor(lightTypeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightTypeIndex, "double:LossFactor"))
    }
    
    async getAllLossFactor(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:LossFactor"))
    }
    
    async getDimmingColor(lightTypeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightTypeIndex, "int:DimmingColor"))
    }
    
    async getAllDimmingColor(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:DimmingColor"))
    }
    
    async getPhotometricWebFileIndex(lightTypeIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(lightTypeIndex, "index:Vim.Asset:PhotometricWebFile")
    }
    
    async getAllPhotometricWebFileIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Asset:PhotometricWebFile")
    }
    
    async getPhotometricWebFile(lightTypeIndex: number): Promise<IAsset | undefined> {
        const index = await this.getPhotometricWebFileIndex(lightTypeIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.asset?.get(index)
    }
    
    async getFamilyTypeIndex(lightTypeIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(lightTypeIndex, "index:Vim.FamilyType:FamilyType")
    }
    
    async getAllFamilyTypeIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.FamilyType:FamilyType")
    }
    
    async getFamilyType(lightTypeIndex: number): Promise<IFamilyType | undefined> {
        const index = await this.getFamilyTypeIndex(lightTypeIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.familyType?.get(index)
    }
    
}

export interface ILightSource {
    index: number
    origin_X?: number
    origin_Y?: number
    origin_Z?: number
    basisX_X?: number
    basisX_Y?: number
    basisX_Z?: number
    basisY_X?: number
    basisY_Y?: number
    basisY_Z?: number
    basisZ_X?: number
    basisZ_Y?: number
    basisZ_Z?: number
    hasLightSourceTransform?: boolean
    isOn?: boolean
    
    lightTypeIndex?: number
    lightType?: ILightType
    elementIndex?: number
    element?: IElement
}

export interface ILightSourceTable {
    getCount(): Promise<number>
    get(lightSourceIndex: number): Promise<ILightSource>
    getAll(): Promise<ILightSource[]>
    
    getOrigin_X(lightSourceIndex: number): Promise<number | undefined>
    getAllOrigin_X(): Promise<number[] | undefined>
    getOrigin_Y(lightSourceIndex: number): Promise<number | undefined>
    getAllOrigin_Y(): Promise<number[] | undefined>
    getOrigin_Z(lightSourceIndex: number): Promise<number | undefined>
    getAllOrigin_Z(): Promise<number[] | undefined>
    getBasisX_X(lightSourceIndex: number): Promise<number | undefined>
    getAllBasisX_X(): Promise<number[] | undefined>
    getBasisX_Y(lightSourceIndex: number): Promise<number | undefined>
    getAllBasisX_Y(): Promise<number[] | undefined>
    getBasisX_Z(lightSourceIndex: number): Promise<number | undefined>
    getAllBasisX_Z(): Promise<number[] | undefined>
    getBasisY_X(lightSourceIndex: number): Promise<number | undefined>
    getAllBasisY_X(): Promise<number[] | undefined>
    getBasisY_Y(lightSourceIndex: number): Promise<number | undefined>
    getAllBasisY_Y(): Promise<number[] | undefined>
    getBasisY_Z(lightSourceIndex: number): Promise<number | undefined>
    getAllBasisY_Z(): Promise<number[] | undefined>
    getBasisZ_X(lightSourceIndex: number): Promise<number | undefined>
    getAllBasisZ_X(): Promise<number[] | undefined>
    getBasisZ_Y(lightSourceIndex: number): Promise<number | undefined>
    getAllBasisZ_Y(): Promise<number[] | undefined>
    getBasisZ_Z(lightSourceIndex: number): Promise<number | undefined>
    getAllBasisZ_Z(): Promise<number[] | undefined>
    getHasLightSourceTransform(lightSourceIndex: number): Promise<boolean | undefined>
    getAllHasLightSourceTransform(): Promise<boolean[] | undefined>
    getIsOn(lightSourceIndex: number): Promise<boolean | undefined>
    getAllIsOn(): Promise<boolean[] | undefined>
    
    getLightTypeIndex(lightSourceIndex: number): Promise<number | undefined>
    getAllLightTypeIndex(): Promise<number[] | undefined>
    getLightType(lightSourceIndex: number): Promise<ILightType | undefined>
    getElementIndex(lightSourceIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(lightSourceIndex: number): Promise<IElement | undefined>
}

export class LightSource implements ILightSource {
    index: number
    origin_X?: number
    origin_Y?: number
    origin_Z?: number
    basisX_X?: number
    basisX_Y?: number
    basisX_Z?: number
    basisY_X?: number
    basisY_Y?: number
    basisY_Z?: number
    basisZ_X?: number
    basisZ_Y?: number
    basisZ_Z?: number
    hasLightSourceTransform?: boolean
    isOn?: boolean
    
    lightTypeIndex?: number
    lightType?: ILightType
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: ILightSourceTable, index: number): Promise<ILightSource> {
        let result = new LightSource()
        result.index = index
        
        await Promise.all([
            table.getOrigin_X(index).then(v => result.origin_X = v),
            table.getOrigin_Y(index).then(v => result.origin_Y = v),
            table.getOrigin_Z(index).then(v => result.origin_Z = v),
            table.getBasisX_X(index).then(v => result.basisX_X = v),
            table.getBasisX_Y(index).then(v => result.basisX_Y = v),
            table.getBasisX_Z(index).then(v => result.basisX_Z = v),
            table.getBasisY_X(index).then(v => result.basisY_X = v),
            table.getBasisY_Y(index).then(v => result.basisY_Y = v),
            table.getBasisY_Z(index).then(v => result.basisY_Z = v),
            table.getBasisZ_X(index).then(v => result.basisZ_X = v),
            table.getBasisZ_Y(index).then(v => result.basisZ_Y = v),
            table.getBasisZ_Z(index).then(v => result.basisZ_Z = v),
            table.getHasLightSourceTransform(index).then(v => result.hasLightSourceTransform = v),
            table.getIsOn(index).then(v => result.isOn = v),
            table.getLightTypeIndex(index).then(v => result.lightTypeIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class LightSourceTable implements ILightSourceTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<ILightSourceTable | undefined> {
        const entity = await document.entities.getBfast("Vim.LightSource")
        
        if (!entity) {
            return undefined
        }
        
        let table = new LightSourceTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(lightSourceIndex: number): Promise<ILightSource> {
        return await LightSource.createFromTable(this, lightSourceIndex)
    }
    
    async getAll(): Promise<ILightSource[]> {
        const localTable = await this.entityTable.getLocal()
        
        let origin_X: number[] | undefined
        let origin_Y: number[] | undefined
        let origin_Z: number[] | undefined
        let basisX_X: number[] | undefined
        let basisX_Y: number[] | undefined
        let basisX_Z: number[] | undefined
        let basisY_X: number[] | undefined
        let basisY_Y: number[] | undefined
        let basisY_Z: number[] | undefined
        let basisZ_X: number[] | undefined
        let basisZ_Y: number[] | undefined
        let basisZ_Z: number[] | undefined
        let hasLightSourceTransform: boolean[] | undefined
        let isOn: boolean[] | undefined
        let lightTypeIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { origin_X = (await localTable.getNumberArray("double:Origin.X")) })(),
            (async () => { origin_Y = (await localTable.getNumberArray("double:Origin.Y")) })(),
            (async () => { origin_Z = (await localTable.getNumberArray("double:Origin.Z")) })(),
            (async () => { basisX_X = (await localTable.getNumberArray("double:BasisX.X")) })(),
            (async () => { basisX_Y = (await localTable.getNumberArray("double:BasisX.Y")) })(),
            (async () => { basisX_Z = (await localTable.getNumberArray("double:BasisX.Z")) })(),
            (async () => { basisY_X = (await localTable.getNumberArray("double:BasisY.X")) })(),
            (async () => { basisY_Y = (await localTable.getNumberArray("double:BasisY.Y")) })(),
            (async () => { basisY_Z = (await localTable.getNumberArray("double:BasisY.Z")) })(),
            (async () => { basisZ_X = (await localTable.getNumberArray("double:BasisZ.X")) })(),
            (async () => { basisZ_Y = (await localTable.getNumberArray("double:BasisZ.Y")) })(),
            (async () => { basisZ_Z = (await localTable.getNumberArray("double:BasisZ.Z")) })(),
            (async () => { hasLightSourceTransform = (await localTable.getBooleanArray("byte:HasLightSourceTransform")) })(),
            (async () => { isOn = (await localTable.getBooleanArray("byte:IsOn")) })(),
            (async () => { lightTypeIndex = (await localTable.getNumberArray("index:Vim.LightType:LightType")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let lightSource: ILightSource[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            lightSource.push({
                index: i,
                origin_X: origin_X ? origin_X[i] : undefined,
                origin_Y: origin_Y ? origin_Y[i] : undefined,
                origin_Z: origin_Z ? origin_Z[i] : undefined,
                basisX_X: basisX_X ? basisX_X[i] : undefined,
                basisX_Y: basisX_Y ? basisX_Y[i] : undefined,
                basisX_Z: basisX_Z ? basisX_Z[i] : undefined,
                basisY_X: basisY_X ? basisY_X[i] : undefined,
                basisY_Y: basisY_Y ? basisY_Y[i] : undefined,
                basisY_Z: basisY_Z ? basisY_Z[i] : undefined,
                basisZ_X: basisZ_X ? basisZ_X[i] : undefined,
                basisZ_Y: basisZ_Y ? basisZ_Y[i] : undefined,
                basisZ_Z: basisZ_Z ? basisZ_Z[i] : undefined,
                hasLightSourceTransform: hasLightSourceTransform ? hasLightSourceTransform[i] : undefined,
                isOn: isOn ? isOn[i] : undefined,
                lightTypeIndex: lightTypeIndex ? lightTypeIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return lightSource
    }
    
    async getOrigin_X(lightSourceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightSourceIndex, "double:Origin.X"))
    }
    
    async getAllOrigin_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Origin.X"))
    }
    
    async getOrigin_Y(lightSourceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightSourceIndex, "double:Origin.Y"))
    }
    
    async getAllOrigin_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Origin.Y"))
    }
    
    async getOrigin_Z(lightSourceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightSourceIndex, "double:Origin.Z"))
    }
    
    async getAllOrigin_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Origin.Z"))
    }
    
    async getBasisX_X(lightSourceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightSourceIndex, "double:BasisX.X"))
    }
    
    async getAllBasisX_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:BasisX.X"))
    }
    
    async getBasisX_Y(lightSourceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightSourceIndex, "double:BasisX.Y"))
    }
    
    async getAllBasisX_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:BasisX.Y"))
    }
    
    async getBasisX_Z(lightSourceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightSourceIndex, "double:BasisX.Z"))
    }
    
    async getAllBasisX_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:BasisX.Z"))
    }
    
    async getBasisY_X(lightSourceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightSourceIndex, "double:BasisY.X"))
    }
    
    async getAllBasisY_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:BasisY.X"))
    }
    
    async getBasisY_Y(lightSourceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightSourceIndex, "double:BasisY.Y"))
    }
    
    async getAllBasisY_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:BasisY.Y"))
    }
    
    async getBasisY_Z(lightSourceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightSourceIndex, "double:BasisY.Z"))
    }
    
    async getAllBasisY_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:BasisY.Z"))
    }
    
    async getBasisZ_X(lightSourceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightSourceIndex, "double:BasisZ.X"))
    }
    
    async getAllBasisZ_X(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:BasisZ.X"))
    }
    
    async getBasisZ_Y(lightSourceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightSourceIndex, "double:BasisZ.Y"))
    }
    
    async getAllBasisZ_Y(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:BasisZ.Y"))
    }
    
    async getBasisZ_Z(lightSourceIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(lightSourceIndex, "double:BasisZ.Z"))
    }
    
    async getAllBasisZ_Z(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:BasisZ.Z"))
    }
    
    async getHasLightSourceTransform(lightSourceIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(lightSourceIndex, "byte:HasLightSourceTransform"))
    }
    
    async getAllHasLightSourceTransform(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:HasLightSourceTransform"))
    }
    
    async getIsOn(lightSourceIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(lightSourceIndex, "byte:IsOn"))
    }
    
    async getAllIsOn(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:IsOn"))
    }
    
    async getLightTypeIndex(lightSourceIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(lightSourceIndex, "index:Vim.LightType:LightType")
    }
    
    async getAllLightTypeIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.LightType:LightType")
    }
    
    async getLightType(lightSourceIndex: number): Promise<ILightType | undefined> {
        const index = await this.getLightTypeIndex(lightSourceIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.lightType?.get(index)
    }
    
    async getElementIndex(lightSourceIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(lightSourceIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(lightSourceIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(lightSourceIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface ISunAndShadowSettings {
    index: number
    sunAndShadowType?: string
    usesDST?: boolean
    activeFrameTime?: string
    startDateAndTime?: string
    endDateAndTime?: string
    activeFrame?: number
    numberOfFrames?: number
    altitude?: number
    azimuth?: number
    relativeToView?: boolean
    
    viewIndex?: number
    view?: IView
}

export interface ISunAndShadowSettingsTable {
    getCount(): Promise<number>
    get(sunAndShadowSettingsIndex: number): Promise<ISunAndShadowSettings>
    getAll(): Promise<ISunAndShadowSettings[]>
    
    getSunAndShadowType(sunAndShadowSettingsIndex: number): Promise<string | undefined>
    getAllSunAndShadowType(): Promise<string[] | undefined>
    getUsesDST(sunAndShadowSettingsIndex: number): Promise<boolean | undefined>
    getAllUsesDST(): Promise<boolean[] | undefined>
    getActiveFrameTime(sunAndShadowSettingsIndex: number): Promise<string | undefined>
    getAllActiveFrameTime(): Promise<string[] | undefined>
    getStartDateAndTime(sunAndShadowSettingsIndex: number): Promise<string | undefined>
    getAllStartDateAndTime(): Promise<string[] | undefined>
    getEndDateAndTime(sunAndShadowSettingsIndex: number): Promise<string | undefined>
    getAllEndDateAndTime(): Promise<string[] | undefined>
    getActiveFrame(sunAndShadowSettingsIndex: number): Promise<number | undefined>
    getAllActiveFrame(): Promise<number[] | undefined>
    getNumberOfFrames(sunAndShadowSettingsIndex: number): Promise<number | undefined>
    getAllNumberOfFrames(): Promise<number[] | undefined>
    getAltitude(sunAndShadowSettingsIndex: number): Promise<number | undefined>
    getAllAltitude(): Promise<number[] | undefined>
    getAzimuth(sunAndShadowSettingsIndex: number): Promise<number | undefined>
    getAllAzimuth(): Promise<number[] | undefined>
    getRelativeToView(sunAndShadowSettingsIndex: number): Promise<boolean | undefined>
    getAllRelativeToView(): Promise<boolean[] | undefined>
    
    getViewIndex(sunAndShadowSettingsIndex: number): Promise<number | undefined>
    getAllViewIndex(): Promise<number[] | undefined>
    getView(sunAndShadowSettingsIndex: number): Promise<IView | undefined>
}

export class SunAndShadowSettings implements ISunAndShadowSettings {
    index: number
    sunAndShadowType?: string
    usesDST?: boolean
    activeFrameTime?: string
    startDateAndTime?: string
    endDateAndTime?: string
    activeFrame?: number
    numberOfFrames?: number
    altitude?: number
    azimuth?: number
    relativeToView?: boolean
    
    viewIndex?: number
    view?: IView
    
    static async createFromTable(table: ISunAndShadowSettingsTable, index: number): Promise<ISunAndShadowSettings> {
        let result = new SunAndShadowSettings()
        result.index = index
        
        await Promise.all([
            table.getSunAndShadowType(index).then(v => result.sunAndShadowType = v),
            table.getUsesDST(index).then(v => result.usesDST = v),
            table.getActiveFrameTime(index).then(v => result.activeFrameTime = v),
            table.getStartDateAndTime(index).then(v => result.startDateAndTime = v),
            table.getEndDateAndTime(index).then(v => result.endDateAndTime = v),
            table.getActiveFrame(index).then(v => result.activeFrame = v),
            table.getNumberOfFrames(index).then(v => result.numberOfFrames = v),
            table.getAltitude(index).then(v => result.altitude = v),
            table.getAzimuth(index).then(v => result.azimuth = v),
            table.getRelativeToView(index).then(v => result.relativeToView = v),
            table.getViewIndex(index).then(v => result.viewIndex = v),
        ])
        
        return result
    }
}

export class SunAndShadowSettingsTable implements ISunAndShadowSettingsTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<ISunAndShadowSettingsTable | undefined> {
        const entity = await document.entities.getBfast("Vim.SunAndShadowSettings")
        
        if (!entity) {
            return undefined
        }
        
        let table = new SunAndShadowSettingsTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(sunAndShadowSettingsIndex: number): Promise<ISunAndShadowSettings> {
        return await SunAndShadowSettings.createFromTable(this, sunAndShadowSettingsIndex)
    }
    
    async getAll(): Promise<ISunAndShadowSettings[]> {
        const localTable = await this.entityTable.getLocal()
        
        let sunAndShadowType: string[] | undefined
        let usesDST: boolean[] | undefined
        let activeFrameTime: string[] | undefined
        let startDateAndTime: string[] | undefined
        let endDateAndTime: string[] | undefined
        let activeFrame: number[] | undefined
        let numberOfFrames: number[] | undefined
        let altitude: number[] | undefined
        let azimuth: number[] | undefined
        let relativeToView: boolean[] | undefined
        let viewIndex: number[] | undefined
        
        await Promise.all([
            (async () => { sunAndShadowType = (await localTable.getStringArray("string:SunAndShadowType")) })(),
            (async () => { usesDST = (await localTable.getBooleanArray("byte:UsesDST")) })(),
            (async () => { activeFrameTime = (await localTable.getStringArray("string:ActiveFrameTime")) })(),
            (async () => { startDateAndTime = (await localTable.getStringArray("string:StartDateAndTime")) })(),
            (async () => { endDateAndTime = (await localTable.getStringArray("string:EndDateAndTime")) })(),
            (async () => { activeFrame = (await localTable.getNumberArray("int:ActiveFrame")) })(),
            (async () => { numberOfFrames = (await localTable.getNumberArray("int:NumberOfFrames")) })(),
            (async () => { altitude = (await localTable.getNumberArray("double:Altitude")) })(),
            (async () => { azimuth = (await localTable.getNumberArray("double:Azimuth")) })(),
            (async () => { relativeToView = (await localTable.getBooleanArray("byte:RelativeToView")) })(),
            (async () => { viewIndex = (await localTable.getNumberArray("index:Vim.View:View")) })(),
        ])
        
        let sunAndShadowSettings: ISunAndShadowSettings[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            sunAndShadowSettings.push({
                index: i,
                sunAndShadowType: sunAndShadowType ? sunAndShadowType[i] : undefined,
                usesDST: usesDST ? usesDST[i] : undefined,
                activeFrameTime: activeFrameTime ? activeFrameTime[i] : undefined,
                startDateAndTime: startDateAndTime ? startDateAndTime[i] : undefined,
                endDateAndTime: endDateAndTime ? endDateAndTime[i] : undefined,
                activeFrame: activeFrame ? activeFrame[i] : undefined,
                numberOfFrames: numberOfFrames ? numberOfFrames[i] : undefined,
                altitude: altitude ? altitude[i] : undefined,
                azimuth: azimuth ? azimuth[i] : undefined,
                relativeToView: relativeToView ? relativeToView[i] : undefined,
                viewIndex: viewIndex ? viewIndex[i] : undefined
            })
        }
        
        return sunAndShadowSettings
    }
    
    async getSunAndShadowType(sunAndShadowSettingsIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(sunAndShadowSettingsIndex, "string:SunAndShadowType"))
    }
    
    async getAllSunAndShadowType(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:SunAndShadowType"))
    }
    
    async getUsesDST(sunAndShadowSettingsIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(sunAndShadowSettingsIndex, "byte:UsesDST"))
    }
    
    async getAllUsesDST(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:UsesDST"))
    }
    
    async getActiveFrameTime(sunAndShadowSettingsIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(sunAndShadowSettingsIndex, "string:ActiveFrameTime"))
    }
    
    async getAllActiveFrameTime(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:ActiveFrameTime"))
    }
    
    async getStartDateAndTime(sunAndShadowSettingsIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(sunAndShadowSettingsIndex, "string:StartDateAndTime"))
    }
    
    async getAllStartDateAndTime(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:StartDateAndTime"))
    }
    
    async getEndDateAndTime(sunAndShadowSettingsIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(sunAndShadowSettingsIndex, "string:EndDateAndTime"))
    }
    
    async getAllEndDateAndTime(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:EndDateAndTime"))
    }
    
    async getActiveFrame(sunAndShadowSettingsIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(sunAndShadowSettingsIndex, "int:ActiveFrame"))
    }
    
    async getAllActiveFrame(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:ActiveFrame"))
    }
    
    async getNumberOfFrames(sunAndShadowSettingsIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(sunAndShadowSettingsIndex, "int:NumberOfFrames"))
    }
    
    async getAllNumberOfFrames(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:NumberOfFrames"))
    }
    
    async getAltitude(sunAndShadowSettingsIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(sunAndShadowSettingsIndex, "double:Altitude"))
    }
    
    async getAllAltitude(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Altitude"))
    }
    
    async getAzimuth(sunAndShadowSettingsIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(sunAndShadowSettingsIndex, "double:Azimuth"))
    }
    
    async getAllAzimuth(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("double:Azimuth"))
    }
    
    async getRelativeToView(sunAndShadowSettingsIndex: number): Promise<boolean | undefined> {
        return (await this.entityTable.getBoolean(sunAndShadowSettingsIndex, "byte:RelativeToView"))
    }
    
    async getAllRelativeToView(): Promise<boolean[] | undefined> {
        return (await this.entityTable.getBooleanArray("byte:RelativeToView"))
    }
    
    async getViewIndex(sunAndShadowSettingsIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(sunAndShadowSettingsIndex, "index:Vim.View:View")
    }
    
    async getAllViewIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.View:View")
    }
    
    async getView(sunAndShadowSettingsIndex: number): Promise<IView | undefined> {
        const index = await this.getViewIndex(sunAndShadowSettingsIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.view?.get(index)
    }
    
}

export interface IExportRevision {
    index: number
    vimRevision?: string
    kind?: number
    generator?: string
    createdUtc?: string
    documentNumSaves?: number
    documentVersionGuid?: string
    
    bimDocumentIndex?: number
    bimDocument?: IBimDocument
}

export interface IExportRevisionTable {
    getCount(): Promise<number>
    get(exportRevisionIndex: number): Promise<IExportRevision>
    getAll(): Promise<IExportRevision[]>
    
    getVimRevision(exportRevisionIndex: number): Promise<string | undefined>
    getAllVimRevision(): Promise<string[] | undefined>
    getKind(exportRevisionIndex: number): Promise<number | undefined>
    getAllKind(): Promise<number[] | undefined>
    getGenerator(exportRevisionIndex: number): Promise<string | undefined>
    getAllGenerator(): Promise<string[] | undefined>
    getCreatedUtc(exportRevisionIndex: number): Promise<string | undefined>
    getAllCreatedUtc(): Promise<string[] | undefined>
    getDocumentNumSaves(exportRevisionIndex: number): Promise<number | undefined>
    getAllDocumentNumSaves(): Promise<number[] | undefined>
    getDocumentVersionGuid(exportRevisionIndex: number): Promise<string | undefined>
    getAllDocumentVersionGuid(): Promise<string[] | undefined>
    
    getBimDocumentIndex(exportRevisionIndex: number): Promise<number | undefined>
    getAllBimDocumentIndex(): Promise<number[] | undefined>
    getBimDocument(exportRevisionIndex: number): Promise<IBimDocument | undefined>
}

export class ExportRevision implements IExportRevision {
    index: number
    vimRevision?: string
    kind?: number
    generator?: string
    createdUtc?: string
    documentNumSaves?: number
    documentVersionGuid?: string
    
    bimDocumentIndex?: number
    bimDocument?: IBimDocument
    
    static async createFromTable(table: IExportRevisionTable, index: number): Promise<IExportRevision> {
        let result = new ExportRevision()
        result.index = index
        
        await Promise.all([
            table.getVimRevision(index).then(v => result.vimRevision = v),
            table.getKind(index).then(v => result.kind = v),
            table.getGenerator(index).then(v => result.generator = v),
            table.getCreatedUtc(index).then(v => result.createdUtc = v),
            table.getDocumentNumSaves(index).then(v => result.documentNumSaves = v),
            table.getDocumentVersionGuid(index).then(v => result.documentVersionGuid = v),
            table.getBimDocumentIndex(index).then(v => result.bimDocumentIndex = v),
        ])
        
        return result
    }
}

export class ExportRevisionTable implements IExportRevisionTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IExportRevisionTable | undefined> {
        const entity = await document.entities.getBfast("Vim.ExportRevision")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ExportRevisionTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(exportRevisionIndex: number): Promise<IExportRevision> {
        return await ExportRevision.createFromTable(this, exportRevisionIndex)
    }
    
    async getAll(): Promise<IExportRevision[]> {
        const localTable = await this.entityTable.getLocal()
        
        let vimRevision: string[] | undefined
        let kind: number[] | undefined
        let generator: string[] | undefined
        let createdUtc: string[] | undefined
        let documentNumSaves: number[] | undefined
        let documentVersionGuid: string[] | undefined
        let bimDocumentIndex: number[] | undefined
        
        await Promise.all([
            (async () => { vimRevision = (await localTable.getStringArray("string:VimRevision")) })(),
            (async () => { kind = (await localTable.getNumberArray("int:Kind")) })(),
            (async () => { generator = (await localTable.getStringArray("string:Generator")) })(),
            (async () => { createdUtc = (await localTable.getStringArray("string:CreatedUtc")) })(),
            (async () => { documentNumSaves = (await localTable.getNumberArray("int:DocumentNumSaves")) })(),
            (async () => { documentVersionGuid = (await localTable.getStringArray("string:DocumentVersionGuid")) })(),
            (async () => { bimDocumentIndex = (await localTable.getNumberArray("index:Vim.BimDocument:BimDocument")) })(),
        ])
        
        let exportRevision: IExportRevision[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            exportRevision.push({
                index: i,
                vimRevision: vimRevision ? vimRevision[i] : undefined,
                kind: kind ? kind[i] : undefined,
                generator: generator ? generator[i] : undefined,
                createdUtc: createdUtc ? createdUtc[i] : undefined,
                documentNumSaves: documentNumSaves ? documentNumSaves[i] : undefined,
                documentVersionGuid: documentVersionGuid ? documentVersionGuid[i] : undefined,
                bimDocumentIndex: bimDocumentIndex ? bimDocumentIndex[i] : undefined
            })
        }
        
        return exportRevision
    }
    
    async getVimRevision(exportRevisionIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(exportRevisionIndex, "string:VimRevision"))
    }
    
    async getAllVimRevision(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:VimRevision"))
    }
    
    async getKind(exportRevisionIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(exportRevisionIndex, "int:Kind"))
    }
    
    async getAllKind(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Kind"))
    }
    
    async getGenerator(exportRevisionIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(exportRevisionIndex, "string:Generator"))
    }
    
    async getAllGenerator(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Generator"))
    }
    
    async getCreatedUtc(exportRevisionIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(exportRevisionIndex, "string:CreatedUtc"))
    }
    
    async getAllCreatedUtc(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:CreatedUtc"))
    }
    
    async getDocumentNumSaves(exportRevisionIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(exportRevisionIndex, "int:DocumentNumSaves"))
    }
    
    async getAllDocumentNumSaves(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:DocumentNumSaves"))
    }
    
    async getDocumentVersionGuid(exportRevisionIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(exportRevisionIndex, "string:DocumentVersionGuid"))
    }
    
    async getAllDocumentVersionGuid(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:DocumentVersionGuid"))
    }
    
    async getBimDocumentIndex(exportRevisionIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(exportRevisionIndex, "index:Vim.BimDocument:BimDocument")
    }
    
    async getAllBimDocumentIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.BimDocument:BimDocument")
    }
    
    async getBimDocument(exportRevisionIndex: number): Promise<IBimDocument | undefined> {
        const index = await this.getBimDocumentIndex(exportRevisionIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.bimDocument?.get(index)
    }
    
}

export interface IElementInExportRevision {
    index: number
    
    exportRevisionIndex?: number
    exportRevision?: IExportRevision
    elementIndex?: number
    element?: IElement
}

export interface IElementInExportRevisionTable {
    getCount(): Promise<number>
    get(elementInExportRevisionIndex: number): Promise<IElementInExportRevision>
    getAll(): Promise<IElementInExportRevision[]>
    
    getExportRevisionIndex(elementInExportRevisionIndex: number): Promise<number | undefined>
    getAllExportRevisionIndex(): Promise<number[] | undefined>
    getExportRevision(elementInExportRevisionIndex: number): Promise<IExportRevision | undefined>
    getElementIndex(elementInExportRevisionIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(elementInExportRevisionIndex: number): Promise<IElement | undefined>
}

export class ElementInExportRevision implements IElementInExportRevision {
    index: number
    
    exportRevisionIndex?: number
    exportRevision?: IExportRevision
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IElementInExportRevisionTable, index: number): Promise<IElementInExportRevision> {
        let result = new ElementInExportRevision()
        result.index = index
        
        await Promise.all([
            table.getExportRevisionIndex(index).then(v => result.exportRevisionIndex = v),
            table.getElementIndex(index).then(v => result.elementIndex = v),
        ])
        
        return result
    }
}

export class ElementInExportRevisionTable implements IElementInExportRevisionTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IElementInExportRevisionTable | undefined> {
        const entity = await document.entities.getBfast("Vim.ElementInExportRevision")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ElementInExportRevisionTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(elementInExportRevisionIndex: number): Promise<IElementInExportRevision> {
        return await ElementInExportRevision.createFromTable(this, elementInExportRevisionIndex)
    }
    
    async getAll(): Promise<IElementInExportRevision[]> {
        const localTable = await this.entityTable.getLocal()
        
        let exportRevisionIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { exportRevisionIndex = (await localTable.getNumberArray("index:Vim.ExportRevision:ExportRevision")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let elementInExportRevision: IElementInExportRevision[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            elementInExportRevision.push({
                index: i,
                exportRevisionIndex: exportRevisionIndex ? exportRevisionIndex[i] : undefined,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return elementInExportRevision
    }
    
    async getExportRevisionIndex(elementInExportRevisionIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementInExportRevisionIndex, "index:Vim.ExportRevision:ExportRevision")
    }
    
    async getAllExportRevisionIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.ExportRevision:ExportRevision")
    }
    
    async getExportRevision(elementInExportRevisionIndex: number): Promise<IExportRevision | undefined> {
        const index = await this.getExportRevisionIndex(elementInExportRevisionIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.exportRevision?.get(index)
    }
    
    async getElementIndex(elementInExportRevisionIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementInExportRevisionIndex, "index:Vim.Element:Element")
    }
    
    async getAllElementIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Element:Element")
    }
    
    async getElement(elementInExportRevisionIndex: number): Promise<IElement | undefined> {
        const index = await this.getElementIndex(elementInExportRevisionIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.element?.get(index)
    }
    
}

export interface IContributor {
    index: number
    name?: string
    source?: number
}

export interface IContributorTable {
    getCount(): Promise<number>
    get(contributorIndex: number): Promise<IContributor>
    getAll(): Promise<IContributor[]>
    
    getName(contributorIndex: number): Promise<string | undefined>
    getAllName(): Promise<string[] | undefined>
    getSource(contributorIndex: number): Promise<number | undefined>
    getAllSource(): Promise<number[] | undefined>
}

export class Contributor implements IContributor {
    index: number
    name?: string
    source?: number
    
    static async createFromTable(table: IContributorTable, index: number): Promise<IContributor> {
        let result = new Contributor()
        result.index = index
        
        await Promise.all([
            table.getName(index).then(v => result.name = v),
            table.getSource(index).then(v => result.source = v),
        ])
        
        return result
    }
}

export class ContributorTable implements IContributorTable {
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IContributorTable | undefined> {
        const entity = await document.entities.getBfast("Vim.Contributor")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ContributorTable()
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(contributorIndex: number): Promise<IContributor> {
        return await Contributor.createFromTable(this, contributorIndex)
    }
    
    async getAll(): Promise<IContributor[]> {
        const localTable = await this.entityTable.getLocal()
        
        let name: string[] | undefined
        let source: number[] | undefined
        
        await Promise.all([
            (async () => { name = (await localTable.getStringArray("string:Name")) })(),
            (async () => { source = (await localTable.getNumberArray("int:Source")) })(),
        ])
        
        let contributor: IContributor[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            contributor.push({
                index: i,
                name: name ? name[i] : undefined,
                source: source ? source[i] : undefined
            })
        }
        
        return contributor
    }
    
    async getName(contributorIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(contributorIndex, "string:Name"))
    }
    
    async getAllName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Name"))
    }
    
    async getSource(contributorIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(contributorIndex, "int:Source"))
    }
    
    async getAllSource(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Source"))
    }
    
}

export interface ISyncSession {
    index: number
    sessionGuid?: string
    machineName?: string
    revitVersion?: string
    startedUtc?: string
    endedUtc?: string
    
    contributorIndex?: number
    contributor?: IContributor
    bimDocumentIndex?: number
    bimDocument?: IBimDocument
}

export interface ISyncSessionTable {
    getCount(): Promise<number>
    get(syncSessionIndex: number): Promise<ISyncSession>
    getAll(): Promise<ISyncSession[]>
    
    getSessionGuid(syncSessionIndex: number): Promise<string | undefined>
    getAllSessionGuid(): Promise<string[] | undefined>
    getMachineName(syncSessionIndex: number): Promise<string | undefined>
    getAllMachineName(): Promise<string[] | undefined>
    getRevitVersion(syncSessionIndex: number): Promise<string | undefined>
    getAllRevitVersion(): Promise<string[] | undefined>
    getStartedUtc(syncSessionIndex: number): Promise<string | undefined>
    getAllStartedUtc(): Promise<string[] | undefined>
    getEndedUtc(syncSessionIndex: number): Promise<string | undefined>
    getAllEndedUtc(): Promise<string[] | undefined>
    
    getContributorIndex(syncSessionIndex: number): Promise<number | undefined>
    getAllContributorIndex(): Promise<number[] | undefined>
    getContributor(syncSessionIndex: number): Promise<IContributor | undefined>
    getBimDocumentIndex(syncSessionIndex: number): Promise<number | undefined>
    getAllBimDocumentIndex(): Promise<number[] | undefined>
    getBimDocument(syncSessionIndex: number): Promise<IBimDocument | undefined>
}

export class SyncSession implements ISyncSession {
    index: number
    sessionGuid?: string
    machineName?: string
    revitVersion?: string
    startedUtc?: string
    endedUtc?: string
    
    contributorIndex?: number
    contributor?: IContributor
    bimDocumentIndex?: number
    bimDocument?: IBimDocument
    
    static async createFromTable(table: ISyncSessionTable, index: number): Promise<ISyncSession> {
        let result = new SyncSession()
        result.index = index
        
        await Promise.all([
            table.getSessionGuid(index).then(v => result.sessionGuid = v),
            table.getMachineName(index).then(v => result.machineName = v),
            table.getRevitVersion(index).then(v => result.revitVersion = v),
            table.getStartedUtc(index).then(v => result.startedUtc = v),
            table.getEndedUtc(index).then(v => result.endedUtc = v),
            table.getContributorIndex(index).then(v => result.contributorIndex = v),
            table.getBimDocumentIndex(index).then(v => result.bimDocumentIndex = v),
        ])
        
        return result
    }
}

export class SyncSessionTable implements ISyncSessionTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<ISyncSessionTable | undefined> {
        const entity = await document.entities.getBfast("Vim.SyncSession")
        
        if (!entity) {
            return undefined
        }
        
        let table = new SyncSessionTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(syncSessionIndex: number): Promise<ISyncSession> {
        return await SyncSession.createFromTable(this, syncSessionIndex)
    }
    
    async getAll(): Promise<ISyncSession[]> {
        const localTable = await this.entityTable.getLocal()
        
        let sessionGuid: string[] | undefined
        let machineName: string[] | undefined
        let revitVersion: string[] | undefined
        let startedUtc: string[] | undefined
        let endedUtc: string[] | undefined
        let contributorIndex: number[] | undefined
        let bimDocumentIndex: number[] | undefined
        
        await Promise.all([
            (async () => { sessionGuid = (await localTable.getStringArray("string:SessionGuid")) })(),
            (async () => { machineName = (await localTable.getStringArray("string:MachineName")) })(),
            (async () => { revitVersion = (await localTable.getStringArray("string:RevitVersion")) })(),
            (async () => { startedUtc = (await localTable.getStringArray("string:StartedUtc")) })(),
            (async () => { endedUtc = (await localTable.getStringArray("string:EndedUtc")) })(),
            (async () => { contributorIndex = (await localTable.getNumberArray("index:Vim.Contributor:Contributor")) })(),
            (async () => { bimDocumentIndex = (await localTable.getNumberArray("index:Vim.BimDocument:BimDocument")) })(),
        ])
        
        let syncSession: ISyncSession[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            syncSession.push({
                index: i,
                sessionGuid: sessionGuid ? sessionGuid[i] : undefined,
                machineName: machineName ? machineName[i] : undefined,
                revitVersion: revitVersion ? revitVersion[i] : undefined,
                startedUtc: startedUtc ? startedUtc[i] : undefined,
                endedUtc: endedUtc ? endedUtc[i] : undefined,
                contributorIndex: contributorIndex ? contributorIndex[i] : undefined,
                bimDocumentIndex: bimDocumentIndex ? bimDocumentIndex[i] : undefined
            })
        }
        
        return syncSession
    }
    
    async getSessionGuid(syncSessionIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(syncSessionIndex, "string:SessionGuid"))
    }
    
    async getAllSessionGuid(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:SessionGuid"))
    }
    
    async getMachineName(syncSessionIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(syncSessionIndex, "string:MachineName"))
    }
    
    async getAllMachineName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:MachineName"))
    }
    
    async getRevitVersion(syncSessionIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(syncSessionIndex, "string:RevitVersion"))
    }
    
    async getAllRevitVersion(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:RevitVersion"))
    }
    
    async getStartedUtc(syncSessionIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(syncSessionIndex, "string:StartedUtc"))
    }
    
    async getAllStartedUtc(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:StartedUtc"))
    }
    
    async getEndedUtc(syncSessionIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(syncSessionIndex, "string:EndedUtc"))
    }
    
    async getAllEndedUtc(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:EndedUtc"))
    }
    
    async getContributorIndex(syncSessionIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(syncSessionIndex, "index:Vim.Contributor:Contributor")
    }
    
    async getAllContributorIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Contributor:Contributor")
    }
    
    async getContributor(syncSessionIndex: number): Promise<IContributor | undefined> {
        const index = await this.getContributorIndex(syncSessionIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.contributor?.get(index)
    }
    
    async getBimDocumentIndex(syncSessionIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(syncSessionIndex, "index:Vim.BimDocument:BimDocument")
    }
    
    async getAllBimDocumentIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.BimDocument:BimDocument")
    }
    
    async getBimDocument(syncSessionIndex: number): Promise<IBimDocument | undefined> {
        const index = await this.getBimDocumentIndex(syncSessionIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.bimDocument?.get(index)
    }
    
}

export interface IElementChange {
    index: number
    changeKind?: number
    confidence?: number
    changedUtc?: string
    uniqueId?: string
    categoryName?: string
    familyName?: string
    typeName?: string
    elementName?: string
    levelName?: string
    worksetName?: string
    
    exportRevisionIndex?: number
    exportRevision?: IExportRevision
    syncSessionIndex?: number
    syncSession?: ISyncSession
    contributorIndex?: number
    contributor?: IContributor
}

export interface IElementChangeTable {
    getCount(): Promise<number>
    get(elementChangeIndex: number): Promise<IElementChange>
    getAll(): Promise<IElementChange[]>
    
    getChangeKind(elementChangeIndex: number): Promise<number | undefined>
    getAllChangeKind(): Promise<number[] | undefined>
    getConfidence(elementChangeIndex: number): Promise<number | undefined>
    getAllConfidence(): Promise<number[] | undefined>
    getChangedUtc(elementChangeIndex: number): Promise<string | undefined>
    getAllChangedUtc(): Promise<string[] | undefined>
    getUniqueId(elementChangeIndex: number): Promise<string | undefined>
    getAllUniqueId(): Promise<string[] | undefined>
    getCategoryName(elementChangeIndex: number): Promise<string | undefined>
    getAllCategoryName(): Promise<string[] | undefined>
    getFamilyName(elementChangeIndex: number): Promise<string | undefined>
    getAllFamilyName(): Promise<string[] | undefined>
    getTypeName(elementChangeIndex: number): Promise<string | undefined>
    getAllTypeName(): Promise<string[] | undefined>
    getElementName(elementChangeIndex: number): Promise<string | undefined>
    getAllElementName(): Promise<string[] | undefined>
    getLevelName(elementChangeIndex: number): Promise<string | undefined>
    getAllLevelName(): Promise<string[] | undefined>
    getWorksetName(elementChangeIndex: number): Promise<string | undefined>
    getAllWorksetName(): Promise<string[] | undefined>
    
    getExportRevisionIndex(elementChangeIndex: number): Promise<number | undefined>
    getAllExportRevisionIndex(): Promise<number[] | undefined>
    getExportRevision(elementChangeIndex: number): Promise<IExportRevision | undefined>
    getSyncSessionIndex(elementChangeIndex: number): Promise<number | undefined>
    getAllSyncSessionIndex(): Promise<number[] | undefined>
    getSyncSession(elementChangeIndex: number): Promise<ISyncSession | undefined>
    getContributorIndex(elementChangeIndex: number): Promise<number | undefined>
    getAllContributorIndex(): Promise<number[] | undefined>
    getContributor(elementChangeIndex: number): Promise<IContributor | undefined>
}

export class ElementChange implements IElementChange {
    index: number
    changeKind?: number
    confidence?: number
    changedUtc?: string
    uniqueId?: string
    categoryName?: string
    familyName?: string
    typeName?: string
    elementName?: string
    levelName?: string
    worksetName?: string
    
    exportRevisionIndex?: number
    exportRevision?: IExportRevision
    syncSessionIndex?: number
    syncSession?: ISyncSession
    contributorIndex?: number
    contributor?: IContributor
    
    static async createFromTable(table: IElementChangeTable, index: number): Promise<IElementChange> {
        let result = new ElementChange()
        result.index = index
        
        await Promise.all([
            table.getChangeKind(index).then(v => result.changeKind = v),
            table.getConfidence(index).then(v => result.confidence = v),
            table.getChangedUtc(index).then(v => result.changedUtc = v),
            table.getUniqueId(index).then(v => result.uniqueId = v),
            table.getCategoryName(index).then(v => result.categoryName = v),
            table.getFamilyName(index).then(v => result.familyName = v),
            table.getTypeName(index).then(v => result.typeName = v),
            table.getElementName(index).then(v => result.elementName = v),
            table.getLevelName(index).then(v => result.levelName = v),
            table.getWorksetName(index).then(v => result.worksetName = v),
            table.getExportRevisionIndex(index).then(v => result.exportRevisionIndex = v),
            table.getSyncSessionIndex(index).then(v => result.syncSessionIndex = v),
            table.getContributorIndex(index).then(v => result.contributorIndex = v),
        ])
        
        return result
    }
}

export class ElementChangeTable implements IElementChangeTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IElementChangeTable | undefined> {
        const entity = await document.entities.getBfast("Vim.ElementChange")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ElementChangeTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(elementChangeIndex: number): Promise<IElementChange> {
        return await ElementChange.createFromTable(this, elementChangeIndex)
    }
    
    async getAll(): Promise<IElementChange[]> {
        const localTable = await this.entityTable.getLocal()
        
        let changeKind: number[] | undefined
        let confidence: number[] | undefined
        let changedUtc: string[] | undefined
        let uniqueId: string[] | undefined
        let categoryName: string[] | undefined
        let familyName: string[] | undefined
        let typeName: string[] | undefined
        let elementName: string[] | undefined
        let levelName: string[] | undefined
        let worksetName: string[] | undefined
        let exportRevisionIndex: number[] | undefined
        let syncSessionIndex: number[] | undefined
        let contributorIndex: number[] | undefined
        
        await Promise.all([
            (async () => { changeKind = (await localTable.getNumberArray("int:ChangeKind")) })(),
            (async () => { confidence = (await localTable.getNumberArray("int:Confidence")) })(),
            (async () => { changedUtc = (await localTable.getStringArray("string:ChangedUtc")) })(),
            (async () => { uniqueId = (await localTable.getStringArray("string:UniqueId")) })(),
            (async () => { categoryName = (await localTable.getStringArray("string:CategoryName")) })(),
            (async () => { familyName = (await localTable.getStringArray("string:FamilyName")) })(),
            (async () => { typeName = (await localTable.getStringArray("string:TypeName")) })(),
            (async () => { elementName = (await localTable.getStringArray("string:ElementName")) })(),
            (async () => { levelName = (await localTable.getStringArray("string:LevelName")) })(),
            (async () => { worksetName = (await localTable.getStringArray("string:WorksetName")) })(),
            (async () => { exportRevisionIndex = (await localTable.getNumberArray("index:Vim.ExportRevision:ExportRevision")) })(),
            (async () => { syncSessionIndex = (await localTable.getNumberArray("index:Vim.SyncSession:SyncSession")) })(),
            (async () => { contributorIndex = (await localTable.getNumberArray("index:Vim.Contributor:Contributor")) })(),
        ])
        
        let elementChange: IElementChange[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            elementChange.push({
                index: i,
                changeKind: changeKind ? changeKind[i] : undefined,
                confidence: confidence ? confidence[i] : undefined,
                changedUtc: changedUtc ? changedUtc[i] : undefined,
                uniqueId: uniqueId ? uniqueId[i] : undefined,
                categoryName: categoryName ? categoryName[i] : undefined,
                familyName: familyName ? familyName[i] : undefined,
                typeName: typeName ? typeName[i] : undefined,
                elementName: elementName ? elementName[i] : undefined,
                levelName: levelName ? levelName[i] : undefined,
                worksetName: worksetName ? worksetName[i] : undefined,
                exportRevisionIndex: exportRevisionIndex ? exportRevisionIndex[i] : undefined,
                syncSessionIndex: syncSessionIndex ? syncSessionIndex[i] : undefined,
                contributorIndex: contributorIndex ? contributorIndex[i] : undefined
            })
        }
        
        return elementChange
    }
    
    async getChangeKind(elementChangeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(elementChangeIndex, "int:ChangeKind"))
    }
    
    async getAllChangeKind(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:ChangeKind"))
    }
    
    async getConfidence(elementChangeIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(elementChangeIndex, "int:Confidence"))
    }
    
    async getAllConfidence(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Confidence"))
    }
    
    async getChangedUtc(elementChangeIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(elementChangeIndex, "string:ChangedUtc"))
    }
    
    async getAllChangedUtc(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:ChangedUtc"))
    }
    
    async getUniqueId(elementChangeIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(elementChangeIndex, "string:UniqueId"))
    }
    
    async getAllUniqueId(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:UniqueId"))
    }
    
    async getCategoryName(elementChangeIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(elementChangeIndex, "string:CategoryName"))
    }
    
    async getAllCategoryName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:CategoryName"))
    }
    
    async getFamilyName(elementChangeIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(elementChangeIndex, "string:FamilyName"))
    }
    
    async getAllFamilyName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:FamilyName"))
    }
    
    async getTypeName(elementChangeIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(elementChangeIndex, "string:TypeName"))
    }
    
    async getAllTypeName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:TypeName"))
    }
    
    async getElementName(elementChangeIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(elementChangeIndex, "string:ElementName"))
    }
    
    async getAllElementName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:ElementName"))
    }
    
    async getLevelName(elementChangeIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(elementChangeIndex, "string:LevelName"))
    }
    
    async getAllLevelName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:LevelName"))
    }
    
    async getWorksetName(elementChangeIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(elementChangeIndex, "string:WorksetName"))
    }
    
    async getAllWorksetName(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:WorksetName"))
    }
    
    async getExportRevisionIndex(elementChangeIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementChangeIndex, "index:Vim.ExportRevision:ExportRevision")
    }
    
    async getAllExportRevisionIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.ExportRevision:ExportRevision")
    }
    
    async getExportRevision(elementChangeIndex: number): Promise<IExportRevision | undefined> {
        const index = await this.getExportRevisionIndex(elementChangeIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.exportRevision?.get(index)
    }
    
    async getSyncSessionIndex(elementChangeIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementChangeIndex, "index:Vim.SyncSession:SyncSession")
    }
    
    async getAllSyncSessionIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.SyncSession:SyncSession")
    }
    
    async getSyncSession(elementChangeIndex: number): Promise<ISyncSession | undefined> {
        const index = await this.getSyncSessionIndex(elementChangeIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.syncSession?.get(index)
    }
    
    async getContributorIndex(elementChangeIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(elementChangeIndex, "index:Vim.Contributor:Contributor")
    }
    
    async getAllContributorIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Contributor:Contributor")
    }
    
    async getContributor(elementChangeIndex: number): Promise<IContributor | undefined> {
        const index = await this.getContributorIndex(elementChangeIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.contributor?.get(index)
    }
    
}

export interface IActivityMarker {
    index: number
    kind?: number
    occurredUtc?: string
    description?: string
    
    exportRevisionIndex?: number
    exportRevision?: IExportRevision
    syncSessionIndex?: number
    syncSession?: ISyncSession
    contributorIndex?: number
    contributor?: IContributor
}

export interface IActivityMarkerTable {
    getCount(): Promise<number>
    get(activityMarkerIndex: number): Promise<IActivityMarker>
    getAll(): Promise<IActivityMarker[]>
    
    getKind(activityMarkerIndex: number): Promise<number | undefined>
    getAllKind(): Promise<number[] | undefined>
    getOccurredUtc(activityMarkerIndex: number): Promise<string | undefined>
    getAllOccurredUtc(): Promise<string[] | undefined>
    getDescription(activityMarkerIndex: number): Promise<string | undefined>
    getAllDescription(): Promise<string[] | undefined>
    
    getExportRevisionIndex(activityMarkerIndex: number): Promise<number | undefined>
    getAllExportRevisionIndex(): Promise<number[] | undefined>
    getExportRevision(activityMarkerIndex: number): Promise<IExportRevision | undefined>
    getSyncSessionIndex(activityMarkerIndex: number): Promise<number | undefined>
    getAllSyncSessionIndex(): Promise<number[] | undefined>
    getSyncSession(activityMarkerIndex: number): Promise<ISyncSession | undefined>
    getContributorIndex(activityMarkerIndex: number): Promise<number | undefined>
    getAllContributorIndex(): Promise<number[] | undefined>
    getContributor(activityMarkerIndex: number): Promise<IContributor | undefined>
}

export class ActivityMarker implements IActivityMarker {
    index: number
    kind?: number
    occurredUtc?: string
    description?: string
    
    exportRevisionIndex?: number
    exportRevision?: IExportRevision
    syncSessionIndex?: number
    syncSession?: ISyncSession
    contributorIndex?: number
    contributor?: IContributor
    
    static async createFromTable(table: IActivityMarkerTable, index: number): Promise<IActivityMarker> {
        let result = new ActivityMarker()
        result.index = index
        
        await Promise.all([
            table.getKind(index).then(v => result.kind = v),
            table.getOccurredUtc(index).then(v => result.occurredUtc = v),
            table.getDescription(index).then(v => result.description = v),
            table.getExportRevisionIndex(index).then(v => result.exportRevisionIndex = v),
            table.getSyncSessionIndex(index).then(v => result.syncSessionIndex = v),
            table.getContributorIndex(index).then(v => result.contributorIndex = v),
        ])
        
        return result
    }
}

export class ActivityMarkerTable implements IActivityMarkerTable {
    private document: VimDocument
    private entityTable: EntityTable
    
    static async createFromDocument(document: VimDocument): Promise<IActivityMarkerTable | undefined> {
        const entity = await document.entities.getBfast("Vim.ActivityMarker")
        
        if (!entity) {
            return undefined
        }
        
        let table = new ActivityMarkerTable()
        table.document = document
        table.entityTable = new EntityTable(entity, document.strings)
        
        return table
    }
    
    getCount(): Promise<number> {
        return this.entityTable.getCount()
    }
    
    async get(activityMarkerIndex: number): Promise<IActivityMarker> {
        return await ActivityMarker.createFromTable(this, activityMarkerIndex)
    }
    
    async getAll(): Promise<IActivityMarker[]> {
        const localTable = await this.entityTable.getLocal()
        
        let kind: number[] | undefined
        let occurredUtc: string[] | undefined
        let description: string[] | undefined
        let exportRevisionIndex: number[] | undefined
        let syncSessionIndex: number[] | undefined
        let contributorIndex: number[] | undefined
        
        await Promise.all([
            (async () => { kind = (await localTable.getNumberArray("int:Kind")) })(),
            (async () => { occurredUtc = (await localTable.getStringArray("string:OccurredUtc")) })(),
            (async () => { description = (await localTable.getStringArray("string:Description")) })(),
            (async () => { exportRevisionIndex = (await localTable.getNumberArray("index:Vim.ExportRevision:ExportRevision")) })(),
            (async () => { syncSessionIndex = (await localTable.getNumberArray("index:Vim.SyncSession:SyncSession")) })(),
            (async () => { contributorIndex = (await localTable.getNumberArray("index:Vim.Contributor:Contributor")) })(),
        ])
        
        let activityMarker: IActivityMarker[] = []
        
        const rowCount = await this.getCount()
        for (let i = 0; i < rowCount; i++) {
            activityMarker.push({
                index: i,
                kind: kind ? kind[i] : undefined,
                occurredUtc: occurredUtc ? occurredUtc[i] : undefined,
                description: description ? description[i] : undefined,
                exportRevisionIndex: exportRevisionIndex ? exportRevisionIndex[i] : undefined,
                syncSessionIndex: syncSessionIndex ? syncSessionIndex[i] : undefined,
                contributorIndex: contributorIndex ? contributorIndex[i] : undefined
            })
        }
        
        return activityMarker
    }
    
    async getKind(activityMarkerIndex: number): Promise<number | undefined> {
        return (await this.entityTable.getNumber(activityMarkerIndex, "int:Kind"))
    }
    
    async getAllKind(): Promise<number[] | undefined> {
        return (await this.entityTable.getNumberArray("int:Kind"))
    }
    
    async getOccurredUtc(activityMarkerIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(activityMarkerIndex, "string:OccurredUtc"))
    }
    
    async getAllOccurredUtc(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:OccurredUtc"))
    }
    
    async getDescription(activityMarkerIndex: number): Promise<string | undefined> {
        return (await this.entityTable.getString(activityMarkerIndex, "string:Description"))
    }
    
    async getAllDescription(): Promise<string[] | undefined> {
        return (await this.entityTable.getStringArray("string:Description"))
    }
    
    async getExportRevisionIndex(activityMarkerIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(activityMarkerIndex, "index:Vim.ExportRevision:ExportRevision")
    }
    
    async getAllExportRevisionIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.ExportRevision:ExportRevision")
    }
    
    async getExportRevision(activityMarkerIndex: number): Promise<IExportRevision | undefined> {
        const index = await this.getExportRevisionIndex(activityMarkerIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.exportRevision?.get(index)
    }
    
    async getSyncSessionIndex(activityMarkerIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(activityMarkerIndex, "index:Vim.SyncSession:SyncSession")
    }
    
    async getAllSyncSessionIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.SyncSession:SyncSession")
    }
    
    async getSyncSession(activityMarkerIndex: number): Promise<ISyncSession | undefined> {
        const index = await this.getSyncSessionIndex(activityMarkerIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.syncSession?.get(index)
    }
    
    async getContributorIndex(activityMarkerIndex: number): Promise<number | undefined> {
        return await this.entityTable.getNumber(activityMarkerIndex, "index:Vim.Contributor:Contributor")
    }
    
    async getAllContributorIndex(): Promise<number[] | undefined> {
        return await this.entityTable.getNumberArray("index:Vim.Contributor:Contributor")
    }
    
    async getContributor(activityMarkerIndex: number): Promise<IContributor | undefined> {
        const index = await this.getContributorIndex(activityMarkerIndex)
        
        if (index === undefined) {
            return undefined
        }
        
        return await this.document.contributor?.get(index)
    }
    
}

export class VimDocument {
    asset: IAssetTable | undefined
    displayUnit: IDisplayUnitTable | undefined
    parameterDescriptor: IParameterDescriptorTable | undefined
    parameter: IParameterTable | undefined
    element: IElementTable | undefined
    workset: IWorksetTable | undefined
    assemblyInstance: IAssemblyInstanceTable | undefined
    group: IGroupTable | undefined
    designOption: IDesignOptionTable | undefined
    level: ILevelTable | undefined
    phase: IPhaseTable | undefined
    room: IRoomTable | undefined
    bimDocument: IBimDocumentTable | undefined
    displayUnitInBimDocument: IDisplayUnitInBimDocumentTable | undefined
    phaseOrderInBimDocument: IPhaseOrderInBimDocumentTable | undefined
    category: ICategoryTable | undefined
    family: IFamilyTable | undefined
    familyType: IFamilyTypeTable | undefined
    familyInstance: IFamilyInstanceTable | undefined
    view: IViewTable | undefined
    filterInView: IFilterInViewTable | undefined
    gridInView: IGridInViewTable | undefined
    linePattern: ILinePatternTable | undefined
    fillPattern: IFillPatternTable | undefined
    fillGrid: IFillGridTable | undefined
    lineWeight: ILineWeightTable | undefined
    graphicOverride: IGraphicOverrideTable | undefined
    categoryInView: ICategoryInViewTable | undefined
    elementOverrideInView: IElementOverrideInViewTable | undefined
    phaseGraphicOverride: IPhaseGraphicOverrideTable | undefined
    elementInView: IElementInViewTable | undefined
    shapeInView: IShapeInViewTable | undefined
    assetInView: IAssetInViewTable | undefined
    assetInViewSheet: IAssetInViewSheetTable | undefined
    levelInView: ILevelInViewTable | undefined
    camera: ICameraTable | undefined
    material: IMaterialTable | undefined
    materialInElement: IMaterialInElementTable | undefined
    compoundStructureLayer: ICompoundStructureLayerTable | undefined
    compoundStructure: ICompoundStructureTable | undefined
    node: INodeTable | undefined
    geometryPiece: IGeometryPieceTable | undefined
    hostPath: IHostPathTable | undefined
    hostedInsert: IHostedInsertTable | undefined
    familyRecipe: IFamilyRecipeTable | undefined
    familyRecipeParameter: IFamilyRecipeParameterTable | undefined
    familyTypeValue: IFamilyTypeValueTable | undefined
    familyReferencePlane: IFamilyReferencePlaneTable | undefined
    familyForm: IFamilyFormTable | undefined
    familySketchCurve: IFamilySketchCurveTable | undefined
    familyConstraint: IFamilyConstraintTable | undefined
    familyConstraintReference: IFamilyConstraintReferenceTable | undefined
    familyFormDriver: IFamilyFormDriverTable | undefined
    geometry: IGeometryTable | undefined
    shape: IShapeTable | undefined
    shapeCollection: IShapeCollectionTable | undefined
    shapeInShapeCollection: IShapeInShapeCollectionTable | undefined
    system: ISystemTable | undefined
    elementInSystem: IElementInSystemTable | undefined
    warning: IWarningTable | undefined
    elementInWarning: IElementInWarningTable | undefined
    basePoint: IBasePointTable | undefined
    phaseFilter: IPhaseFilterTable | undefined
    grid: IGridTable | undefined
    area: IAreaTable | undefined
    areaScheme: IAreaSchemeTable | undefined
    schedule: IScheduleTable | undefined
    scheduleColumn: IScheduleColumnTable | undefined
    scheduleCell: IScheduleCellTable | undefined
    viewSheetSet: IViewSheetSetTable | undefined
    viewSheet: IViewSheetTable | undefined
    viewSheetInViewSheetSet: IViewSheetInViewSheetSetTable | undefined
    viewInViewSheetSet: IViewInViewSheetSetTable | undefined
    viewInViewSheet: IViewInViewSheetTable | undefined
    site: ISiteTable | undefined
    building: IBuildingTable | undefined
    lightType: ILightTypeTable | undefined
    lightSource: ILightSourceTable | undefined
    sunAndShadowSettings: ISunAndShadowSettingsTable | undefined
    exportRevision: IExportRevisionTable | undefined
    elementInExportRevision: IElementInExportRevisionTable | undefined
    contributor: IContributorTable | undefined
    syncSession: ISyncSessionTable | undefined
    elementChange: IElementChangeTable | undefined
    activityMarker: IActivityMarkerTable | undefined
    
    entities: BFast
    strings: string[] | undefined
    
    private constructor(entities: BFast, strings: string[] | undefined) {
        this.entities = entities
        this.strings = strings
    }
    
    static async createFromBfast(bfast: BFast, ignoreStrings: boolean = false): Promise<VimDocument | undefined> {
        const loaded = await VimLoader.loadFromBfast(bfast, ignoreStrings)
        
        if (loaded[0] === undefined)
            return undefined
        
        let doc = new VimDocument(loaded[0]!, loaded[1])
        
        doc.asset = await AssetTable.createFromDocument(doc)
        doc.displayUnit = await DisplayUnitTable.createFromDocument(doc)
        doc.parameterDescriptor = await ParameterDescriptorTable.createFromDocument(doc)
        doc.parameter = await ParameterTable.createFromDocument(doc)
        doc.element = await ElementTable.createFromDocument(doc)
        doc.workset = await WorksetTable.createFromDocument(doc)
        doc.assemblyInstance = await AssemblyInstanceTable.createFromDocument(doc)
        doc.group = await GroupTable.createFromDocument(doc)
        doc.designOption = await DesignOptionTable.createFromDocument(doc)
        doc.level = await LevelTable.createFromDocument(doc)
        doc.phase = await PhaseTable.createFromDocument(doc)
        doc.room = await RoomTable.createFromDocument(doc)
        doc.bimDocument = await BimDocumentTable.createFromDocument(doc)
        doc.displayUnitInBimDocument = await DisplayUnitInBimDocumentTable.createFromDocument(doc)
        doc.phaseOrderInBimDocument = await PhaseOrderInBimDocumentTable.createFromDocument(doc)
        doc.category = await CategoryTable.createFromDocument(doc)
        doc.family = await FamilyTable.createFromDocument(doc)
        doc.familyType = await FamilyTypeTable.createFromDocument(doc)
        doc.familyInstance = await FamilyInstanceTable.createFromDocument(doc)
        doc.view = await ViewTable.createFromDocument(doc)
        doc.filterInView = await FilterInViewTable.createFromDocument(doc)
        doc.gridInView = await GridInViewTable.createFromDocument(doc)
        doc.linePattern = await LinePatternTable.createFromDocument(doc)
        doc.fillPattern = await FillPatternTable.createFromDocument(doc)
        doc.fillGrid = await FillGridTable.createFromDocument(doc)
        doc.lineWeight = await LineWeightTable.createFromDocument(doc)
        doc.graphicOverride = await GraphicOverrideTable.createFromDocument(doc)
        doc.categoryInView = await CategoryInViewTable.createFromDocument(doc)
        doc.elementOverrideInView = await ElementOverrideInViewTable.createFromDocument(doc)
        doc.phaseGraphicOverride = await PhaseGraphicOverrideTable.createFromDocument(doc)
        doc.elementInView = await ElementInViewTable.createFromDocument(doc)
        doc.shapeInView = await ShapeInViewTable.createFromDocument(doc)
        doc.assetInView = await AssetInViewTable.createFromDocument(doc)
        doc.assetInViewSheet = await AssetInViewSheetTable.createFromDocument(doc)
        doc.levelInView = await LevelInViewTable.createFromDocument(doc)
        doc.camera = await CameraTable.createFromDocument(doc)
        doc.material = await MaterialTable.createFromDocument(doc)
        doc.materialInElement = await MaterialInElementTable.createFromDocument(doc)
        doc.compoundStructureLayer = await CompoundStructureLayerTable.createFromDocument(doc)
        doc.compoundStructure = await CompoundStructureTable.createFromDocument(doc)
        doc.node = await NodeTable.createFromDocument(doc)
        doc.geometryPiece = await GeometryPieceTable.createFromDocument(doc)
        doc.hostPath = await HostPathTable.createFromDocument(doc)
        doc.hostedInsert = await HostedInsertTable.createFromDocument(doc)
        doc.familyRecipe = await FamilyRecipeTable.createFromDocument(doc)
        doc.familyRecipeParameter = await FamilyRecipeParameterTable.createFromDocument(doc)
        doc.familyTypeValue = await FamilyTypeValueTable.createFromDocument(doc)
        doc.familyReferencePlane = await FamilyReferencePlaneTable.createFromDocument(doc)
        doc.familyForm = await FamilyFormTable.createFromDocument(doc)
        doc.familySketchCurve = await FamilySketchCurveTable.createFromDocument(doc)
        doc.familyConstraint = await FamilyConstraintTable.createFromDocument(doc)
        doc.familyConstraintReference = await FamilyConstraintReferenceTable.createFromDocument(doc)
        doc.familyFormDriver = await FamilyFormDriverTable.createFromDocument(doc)
        doc.geometry = await GeometryTable.createFromDocument(doc)
        doc.shape = await ShapeTable.createFromDocument(doc)
        doc.shapeCollection = await ShapeCollectionTable.createFromDocument(doc)
        doc.shapeInShapeCollection = await ShapeInShapeCollectionTable.createFromDocument(doc)
        doc.system = await SystemTable.createFromDocument(doc)
        doc.elementInSystem = await ElementInSystemTable.createFromDocument(doc)
        doc.warning = await WarningTable.createFromDocument(doc)
        doc.elementInWarning = await ElementInWarningTable.createFromDocument(doc)
        doc.basePoint = await BasePointTable.createFromDocument(doc)
        doc.phaseFilter = await PhaseFilterTable.createFromDocument(doc)
        doc.grid = await GridTable.createFromDocument(doc)
        doc.area = await AreaTable.createFromDocument(doc)
        doc.areaScheme = await AreaSchemeTable.createFromDocument(doc)
        doc.schedule = await ScheduleTable.createFromDocument(doc)
        doc.scheduleColumn = await ScheduleColumnTable.createFromDocument(doc)
        doc.scheduleCell = await ScheduleCellTable.createFromDocument(doc)
        doc.viewSheetSet = await ViewSheetSetTable.createFromDocument(doc)
        doc.viewSheet = await ViewSheetTable.createFromDocument(doc)
        doc.viewSheetInViewSheetSet = await ViewSheetInViewSheetSetTable.createFromDocument(doc)
        doc.viewInViewSheetSet = await ViewInViewSheetSetTable.createFromDocument(doc)
        doc.viewInViewSheet = await ViewInViewSheetTable.createFromDocument(doc)
        doc.site = await SiteTable.createFromDocument(doc)
        doc.building = await BuildingTable.createFromDocument(doc)
        doc.lightType = await LightTypeTable.createFromDocument(doc)
        doc.lightSource = await LightSourceTable.createFromDocument(doc)
        doc.sunAndShadowSettings = await SunAndShadowSettingsTable.createFromDocument(doc)
        doc.exportRevision = await ExportRevisionTable.createFromDocument(doc)
        doc.elementInExportRevision = await ElementInExportRevisionTable.createFromDocument(doc)
        doc.contributor = await ContributorTable.createFromDocument(doc)
        doc.syncSession = await SyncSessionTable.createFromDocument(doc)
        doc.elementChange = await ElementChangeTable.createFromDocument(doc)
        doc.activityMarker = await ActivityMarkerTable.createFromDocument(doc)
        
        return doc
    }
}
