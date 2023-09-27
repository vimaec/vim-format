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
        
        for (let i = 0; i < bufferName!.length; i++) {
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
        
        for (let i = 0; i < spec!.length; i++) {
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
            (async () => { displayUnitIndex = (await localTable.getNumberArray("index:Vim.DisplayUnit:DisplayUnit")) })(),
        ])
        
        let parameterDescriptor: IParameterDescriptor[] = []
        
        for (let i = 0; i < name!.length; i++) {
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
        
        for (let i = 0; i < value!.length; i++) {
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
    familyName?: string
    isPinned?: boolean
    
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
    getFamilyName(elementIndex: number): Promise<string | undefined>
    getAllFamilyName(): Promise<string[] | undefined>
    getIsPinned(elementIndex: number): Promise<boolean | undefined>
    getAllIsPinned(): Promise<boolean[] | undefined>
    
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
    familyName?: string
    isPinned?: boolean
    
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
        let familyName: string[] | undefined
        let isPinned: boolean[] | undefined
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
            (async () => { familyName = (await localTable.getStringArray("string:FamilyName")) })(),
            (async () => { isPinned = (await localTable.getBooleanArray("byte:IsPinned")) })(),
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
        
        for (let i = 0; i < id!.length; i++) {
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
        
        for (let i = 0; i < id!.length; i++) {
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
        
        for (let i = 0; i < assemblyTypeName!.length; i++) {
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
        
        for (let i = 0; i < groupType!.length; i++) {
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
        
        for (let i = 0; i < isPrimary!.length; i++) {
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
        let familyTypeIndex: number[] | undefined
        let buildingIndex: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { elevation = (await localTable.getNumberArray("double:Elevation")) })(),
            (async () => { familyTypeIndex = (await localTable.getNumberArray("index:Vim.FamilyType:FamilyType")) })(),
            (async () => { buildingIndex = (await localTable.getNumberArray("index:Vim.Building:Building")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let level: ILevel[] = []
        
        for (let i = 0; i < elevation!.length; i++) {
            level.push({
                index: i,
                elevation: elevation ? elevation[i] : undefined,
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
        
        for (let i = 0; i < elementIndex!.length; i++) {
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
        
        for (let i = 0; i < baseOffset!.length; i++) {
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
            (async () => { activeViewIndex = (await localTable.getNumberArray("index:Vim.View:ActiveView")) })(),
            (async () => { ownerFamilyIndex = (await localTable.getNumberArray("index:Vim.Family:OwnerFamily")) })(),
            (async () => { parentIndex = (await localTable.getNumberArray("index:Vim.BimDocument:Parent")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let bimDocument: IBimDocument[] = []
        
        for (let i = 0; i < title!.length; i++) {
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
        
        for (let i = 0; i < displayUnitIndex!.length; i++) {
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
        
        for (let i = 0; i < orderIndex!.length; i++) {
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
    
    parentIndex?: number
    parent?: ICategory
    materialIndex?: number
    material?: IMaterial
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
    
    getParentIndex(categoryIndex: number): Promise<number | undefined>
    getAllParentIndex(): Promise<number[] | undefined>
    getParent(categoryIndex: number): Promise<ICategory | undefined>
    getMaterialIndex(categoryIndex: number): Promise<number | undefined>
    getAllMaterialIndex(): Promise<number[] | undefined>
    getMaterial(categoryIndex: number): Promise<IMaterial | undefined>
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
    
    parentIndex?: number
    parent?: ICategory
    materialIndex?: number
    material?: IMaterial
    
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
            table.getParentIndex(index).then(v => result.parentIndex = v),
            table.getMaterialIndex(index).then(v => result.materialIndex = v),
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
        let parentIndex: number[] | undefined
        let materialIndex: number[] | undefined
        
        await Promise.all([
            (async () => { name = (await localTable.getStringArray("string:Name")) })(),
            (async () => { id = (await localTable.getBigIntArray("long:Id")) ?? (await localTable.getBigIntArray("int:Id")) })(),
            (async () => { categoryType = (await localTable.getStringArray("string:CategoryType")) })(),
            (async () => { lineColor_X = (await localTable.getNumberArray("double:LineColor.X")) })(),
            (async () => { lineColor_Y = (await localTable.getNumberArray("double:LineColor.Y")) })(),
            (async () => { lineColor_Z = (await localTable.getNumberArray("double:LineColor.Z")) })(),
            (async () => { builtInCategory = (await localTable.getStringArray("string:BuiltInCategory")) })(),
            (async () => { parentIndex = (await localTable.getNumberArray("index:Vim.Category:Parent")) })(),
            (async () => { materialIndex = (await localTable.getNumberArray("index:Vim.Material:Material")) })(),
        ])
        
        let category: ICategory[] = []
        
        for (let i = 0; i < name!.length; i++) {
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
        
        for (let i = 0; i < structuralMaterialType!.length; i++) {
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
        
        for (let i = 0; i < isSystemFamilyType!.length; i++) {
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
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let familyInstance: IFamilyInstance[] = []
        
        for (let i = 0; i < facingFlipped!.length; i++) {
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
    
    cameraIndex?: number
    camera?: ICamera
    familyTypeIndex?: number
    familyType?: IFamilyType
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
    
    getCameraIndex(viewIndex: number): Promise<number | undefined>
    getAllCameraIndex(): Promise<number[] | undefined>
    getCamera(viewIndex: number): Promise<ICamera | undefined>
    getFamilyTypeIndex(viewIndex: number): Promise<number | undefined>
    getAllFamilyTypeIndex(): Promise<number[] | undefined>
    getFamilyType(viewIndex: number): Promise<IFamilyType | undefined>
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
    
    cameraIndex?: number
    camera?: ICamera
    familyTypeIndex?: number
    familyType?: IFamilyType
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
            table.getCameraIndex(index).then(v => result.cameraIndex = v),
            table.getFamilyTypeIndex(index).then(v => result.familyTypeIndex = v),
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
        let cameraIndex: number[] | undefined
        let familyTypeIndex: number[] | undefined
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
            (async () => { cameraIndex = (await localTable.getNumberArray("index:Vim.Camera:Camera")) })(),
            (async () => { familyTypeIndex = (await localTable.getNumberArray("index:Vim.FamilyType:FamilyType")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let view: IView[] = []
        
        for (let i = 0; i < title!.length; i++) {
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
        
        for (let i = 0; i < viewIndex!.length; i++) {
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
        
        for (let i = 0; i < shapeIndex!.length; i++) {
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
        
        for (let i = 0; i < assetIndex!.length; i++) {
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
        
        for (let i = 0; i < assetIndex!.length; i++) {
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
        
        for (let i = 0; i < extents_Min_X!.length; i++) {
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
        
        for (let i = 0; i < id!.length; i++) {
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
    
    colorTextureFileIndex?: number
    colorTextureFile?: IAsset
    normalTextureFileIndex?: number
    normalTextureFile?: IAsset
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
    
    getColorTextureFileIndex(materialIndex: number): Promise<number | undefined>
    getAllColorTextureFileIndex(): Promise<number[] | undefined>
    getColorTextureFile(materialIndex: number): Promise<IAsset | undefined>
    getNormalTextureFileIndex(materialIndex: number): Promise<number | undefined>
    getAllNormalTextureFileIndex(): Promise<number[] | undefined>
    getNormalTextureFile(materialIndex: number): Promise<IAsset | undefined>
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
    
    colorTextureFileIndex?: number
    colorTextureFile?: IAsset
    normalTextureFileIndex?: number
    normalTextureFile?: IAsset
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
            table.getColorTextureFileIndex(index).then(v => result.colorTextureFileIndex = v),
            table.getNormalTextureFileIndex(index).then(v => result.normalTextureFileIndex = v),
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
        let colorTextureFileIndex: number[] | undefined
        let normalTextureFileIndex: number[] | undefined
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
            (async () => { colorTextureFileIndex = (await localTable.getNumberArray("index:Vim.Asset:ColorTextureFile")) })(),
            (async () => { normalTextureFileIndex = (await localTable.getNumberArray("index:Vim.Asset:NormalTextureFile")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let material: IMaterial[] = []
        
        for (let i = 0; i < name!.length; i++) {
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
        
        for (let i = 0; i < area!.length; i++) {
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
        
        for (let i = 0; i < orderIndex!.length; i++) {
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
        
        for (let i = 0; i < width!.length; i++) {
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
        
        for (let i = 0; i < elementIndex!.length; i++) {
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
        
        for (let i = 0; i < box_Min_X!.length; i++) {
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
    
    elementIndex?: number
    element?: IElement
}

export interface IShapeTable {
    getCount(): Promise<number>
    get(shapeIndex: number): Promise<IShape>
    getAll(): Promise<IShape[]>
    
    getElementIndex(shapeIndex: number): Promise<number | undefined>
    getAllElementIndex(): Promise<number[] | undefined>
    getElement(shapeIndex: number): Promise<IElement | undefined>
}

export class Shape implements IShape {
    index: number
    
    elementIndex?: number
    element?: IElement
    
    static async createFromTable(table: IShapeTable, index: number): Promise<IShape> {
        let result = new Shape()
        result.index = index
        
        await Promise.all([
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
        
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let shape: IShape[] = []
        
        for (let i = 0; i < elementIndex!.length; i++) {
            shape.push({
                index: i,
                elementIndex: elementIndex ? elementIndex[i] : undefined
            })
        }
        
        return shape
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
        
        for (let i = 0; i < elementIndex!.length; i++) {
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
        
        for (let i = 0; i < shapeIndex!.length; i++) {
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
        
        for (let i = 0; i < systemType!.length; i++) {
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
        
        for (let i = 0; i < roles!.length; i++) {
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
    
    getBimDocumentIndex(warningIndex: number): Promise<number | undefined>
    getAllBimDocumentIndex(): Promise<number[] | undefined>
    getBimDocument(warningIndex: number): Promise<IBimDocument | undefined>
}

export class Warning implements IWarning {
    index: number
    guid?: string
    severity?: string
    description?: string
    
    bimDocumentIndex?: number
    bimDocument?: IBimDocument
    
    static async createFromTable(table: IWarningTable, index: number): Promise<IWarning> {
        let result = new Warning()
        result.index = index
        
        await Promise.all([
            table.getGuid(index).then(v => result.guid = v),
            table.getSeverity(index).then(v => result.severity = v),
            table.getDescription(index).then(v => result.description = v),
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
        let bimDocumentIndex: number[] | undefined
        
        await Promise.all([
            (async () => { guid = (await localTable.getStringArray("string:Guid")) })(),
            (async () => { severity = (await localTable.getStringArray("string:Severity")) })(),
            (async () => { description = (await localTable.getStringArray("string:Description")) })(),
            (async () => { bimDocumentIndex = (await localTable.getNumberArray("index:Vim.BimDocument:BimDocument")) })(),
        ])
        
        let warning: IWarning[] = []
        
        for (let i = 0; i < guid!.length; i++) {
            warning.push({
                index: i,
                guid: guid ? guid[i] : undefined,
                severity: severity ? severity[i] : undefined,
                description: description ? description[i] : undefined,
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
        
        for (let i = 0; i < warningIndex!.length; i++) {
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
        let position_X: number[] | undefined
        let position_Y: number[] | undefined
        let position_Z: number[] | undefined
        let sharedPosition_X: number[] | undefined
        let sharedPosition_Y: number[] | undefined
        let sharedPosition_Z: number[] | undefined
        let elementIndex: number[] | undefined
        
        await Promise.all([
            (async () => { isSurveyPoint = (await localTable.getBooleanArray("byte:IsSurveyPoint")) })(),
            (async () => { position_X = (await localTable.getNumberArray("double:Position.X")) })(),
            (async () => { position_Y = (await localTable.getNumberArray("double:Position.Y")) })(),
            (async () => { position_Z = (await localTable.getNumberArray("double:Position.Z")) })(),
            (async () => { sharedPosition_X = (await localTable.getNumberArray("double:SharedPosition.X")) })(),
            (async () => { sharedPosition_Y = (await localTable.getNumberArray("double:SharedPosition.Y")) })(),
            (async () => { sharedPosition_Z = (await localTable.getNumberArray("double:SharedPosition.Z")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let basePoint: IBasePoint[] = []
        
        for (let i = 0; i < isSurveyPoint!.length; i++) {
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
        
        for (let i = 0; i < _new!.length; i++) {
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
    
    familyTypeIndex?: number
    familyType?: IFamilyType
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
    
    getFamilyTypeIndex(gridIndex: number): Promise<number | undefined>
    getAllFamilyTypeIndex(): Promise<number[] | undefined>
    getFamilyType(gridIndex: number): Promise<IFamilyType | undefined>
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
    
    familyTypeIndex?: number
    familyType?: IFamilyType
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
            table.getFamilyTypeIndex(index).then(v => result.familyTypeIndex = v),
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
        let familyTypeIndex: number[] | undefined
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
            (async () => { familyTypeIndex = (await localTable.getNumberArray("index:Vim.FamilyType:FamilyType")) })(),
            (async () => { elementIndex = (await localTable.getNumberArray("index:Vim.Element:Element")) })(),
        ])
        
        let grid: IGrid[] = []
        
        for (let i = 0; i < startPoint_X!.length; i++) {
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
        
        for (let i = 0; i < value!.length; i++) {
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
        
        for (let i = 0; i < isGrossBuildingArea!.length; i++) {
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
        
        for (let i = 0; i < elementIndex!.length; i++) {
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
        
        for (let i = 0; i < name!.length; i++) {
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
        
        for (let i = 0; i < value!.length; i++) {
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
        
        for (let i = 0; i < elementIndex!.length; i++) {
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
        
        for (let i = 0; i < familyTypeIndex!.length; i++) {
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
        
        for (let i = 0; i < viewSheetIndex!.length; i++) {
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
        
        for (let i = 0; i < viewIndex!.length; i++) {
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
        
        for (let i = 0; i < viewIndex!.length; i++) {
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
        
        for (let i = 0; i < latitude!.length; i++) {
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
        
        for (let i = 0; i < elevation!.length; i++) {
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
        
        return doc
    }
}
