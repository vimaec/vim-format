/**
 * @module vim-ts
 */
import { BFast } from "./bfast";
export interface IAsset {
    index: number;
    bufferName?: string;
}
export interface IAssetTable {
    getCount(): Promise<number>;
    get(assetIndex: number): Promise<IAsset>;
    getAll(): Promise<IAsset[]>;
    getBufferName(assetIndex: number): Promise<string | undefined>;
    getAllBufferName(): Promise<string[] | undefined>;
}
export declare class Asset implements IAsset {
    index: number;
    bufferName?: string;
    static createFromTable(table: IAssetTable, index: number): Promise<IAsset>;
}
export declare class AssetTable implements IAssetTable {
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IAssetTable | undefined>;
    getCount(): Promise<number>;
    get(assetIndex: number): Promise<IAsset>;
    getAll(): Promise<IAsset[]>;
    getBufferName(assetIndex: number): Promise<string | undefined>;
    getAllBufferName(): Promise<string[] | undefined>;
}
export interface IDisplayUnit {
    index: number;
    spec?: string;
    type?: string;
    label?: string;
}
export interface IDisplayUnitTable {
    getCount(): Promise<number>;
    get(displayUnitIndex: number): Promise<IDisplayUnit>;
    getAll(): Promise<IDisplayUnit[]>;
    getSpec(displayUnitIndex: number): Promise<string | undefined>;
    getAllSpec(): Promise<string[] | undefined>;
    getType(displayUnitIndex: number): Promise<string | undefined>;
    getAllType(): Promise<string[] | undefined>;
    getLabel(displayUnitIndex: number): Promise<string | undefined>;
    getAllLabel(): Promise<string[] | undefined>;
}
export declare class DisplayUnit implements IDisplayUnit {
    index: number;
    spec?: string;
    type?: string;
    label?: string;
    static createFromTable(table: IDisplayUnitTable, index: number): Promise<IDisplayUnit>;
}
export declare class DisplayUnitTable implements IDisplayUnitTable {
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IDisplayUnitTable | undefined>;
    getCount(): Promise<number>;
    get(displayUnitIndex: number): Promise<IDisplayUnit>;
    getAll(): Promise<IDisplayUnit[]>;
    getSpec(displayUnitIndex: number): Promise<string | undefined>;
    getAllSpec(): Promise<string[] | undefined>;
    getType(displayUnitIndex: number): Promise<string | undefined>;
    getAllType(): Promise<string[] | undefined>;
    getLabel(displayUnitIndex: number): Promise<string | undefined>;
    getAllLabel(): Promise<string[] | undefined>;
}
export interface IParameterDescriptor {
    index: number;
    name?: string;
    group?: string;
    parameterType?: string;
    isInstance?: boolean;
    isShared?: boolean;
    isReadOnly?: boolean;
    flags?: number;
    guid?: string;
    displayUnitIndex?: number;
    displayUnit?: IDisplayUnit;
}
export interface IParameterDescriptorTable {
    getCount(): Promise<number>;
    get(parameterDescriptorIndex: number): Promise<IParameterDescriptor>;
    getAll(): Promise<IParameterDescriptor[]>;
    getName(parameterDescriptorIndex: number): Promise<string | undefined>;
    getAllName(): Promise<string[] | undefined>;
    getGroup(parameterDescriptorIndex: number): Promise<string | undefined>;
    getAllGroup(): Promise<string[] | undefined>;
    getParameterType(parameterDescriptorIndex: number): Promise<string | undefined>;
    getAllParameterType(): Promise<string[] | undefined>;
    getIsInstance(parameterDescriptorIndex: number): Promise<boolean | undefined>;
    getAllIsInstance(): Promise<boolean[] | undefined>;
    getIsShared(parameterDescriptorIndex: number): Promise<boolean | undefined>;
    getAllIsShared(): Promise<boolean[] | undefined>;
    getIsReadOnly(parameterDescriptorIndex: number): Promise<boolean | undefined>;
    getAllIsReadOnly(): Promise<boolean[] | undefined>;
    getFlags(parameterDescriptorIndex: number): Promise<number | undefined>;
    getAllFlags(): Promise<number[] | undefined>;
    getGuid(parameterDescriptorIndex: number): Promise<string | undefined>;
    getAllGuid(): Promise<string[] | undefined>;
    getDisplayUnitIndex(parameterDescriptorIndex: number): Promise<number | undefined>;
    getAllDisplayUnitIndex(): Promise<number[] | undefined>;
    getDisplayUnit(parameterDescriptorIndex: number): Promise<IDisplayUnit | undefined>;
}
export declare class ParameterDescriptor implements IParameterDescriptor {
    index: number;
    name?: string;
    group?: string;
    parameterType?: string;
    isInstance?: boolean;
    isShared?: boolean;
    isReadOnly?: boolean;
    flags?: number;
    guid?: string;
    displayUnitIndex?: number;
    displayUnit?: IDisplayUnit;
    static createFromTable(table: IParameterDescriptorTable, index: number): Promise<IParameterDescriptor>;
}
export declare class ParameterDescriptorTable implements IParameterDescriptorTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IParameterDescriptorTable | undefined>;
    getCount(): Promise<number>;
    get(parameterDescriptorIndex: number): Promise<IParameterDescriptor>;
    getAll(): Promise<IParameterDescriptor[]>;
    getName(parameterDescriptorIndex: number): Promise<string | undefined>;
    getAllName(): Promise<string[] | undefined>;
    getGroup(parameterDescriptorIndex: number): Promise<string | undefined>;
    getAllGroup(): Promise<string[] | undefined>;
    getParameterType(parameterDescriptorIndex: number): Promise<string | undefined>;
    getAllParameterType(): Promise<string[] | undefined>;
    getIsInstance(parameterDescriptorIndex: number): Promise<boolean | undefined>;
    getAllIsInstance(): Promise<boolean[] | undefined>;
    getIsShared(parameterDescriptorIndex: number): Promise<boolean | undefined>;
    getAllIsShared(): Promise<boolean[] | undefined>;
    getIsReadOnly(parameterDescriptorIndex: number): Promise<boolean | undefined>;
    getAllIsReadOnly(): Promise<boolean[] | undefined>;
    getFlags(parameterDescriptorIndex: number): Promise<number | undefined>;
    getAllFlags(): Promise<number[] | undefined>;
    getGuid(parameterDescriptorIndex: number): Promise<string | undefined>;
    getAllGuid(): Promise<string[] | undefined>;
    getDisplayUnitIndex(parameterDescriptorIndex: number): Promise<number | undefined>;
    getAllDisplayUnitIndex(): Promise<number[] | undefined>;
    getDisplayUnit(parameterDescriptorIndex: number): Promise<IDisplayUnit | undefined>;
}
export interface IParameter {
    index: number;
    value?: string;
    parameterDescriptorIndex?: number;
    parameterDescriptor?: IParameterDescriptor;
    elementIndex?: number;
    element?: IElement;
}
export interface IParameterTable {
    getCount(): Promise<number>;
    get(parameterIndex: number): Promise<IParameter>;
    getAll(): Promise<IParameter[]>;
    getValue(parameterIndex: number): Promise<string | undefined>;
    getAllValue(): Promise<string[] | undefined>;
    getParameterDescriptorIndex(parameterIndex: number): Promise<number | undefined>;
    getAllParameterDescriptorIndex(): Promise<number[] | undefined>;
    getParameterDescriptor(parameterIndex: number): Promise<IParameterDescriptor | undefined>;
    getElementIndex(parameterIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(parameterIndex: number): Promise<IElement | undefined>;
}
export declare class Parameter implements IParameter {
    index: number;
    value?: string;
    parameterDescriptorIndex?: number;
    parameterDescriptor?: IParameterDescriptor;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IParameterTable, index: number): Promise<IParameter>;
}
export declare class ParameterTable implements IParameterTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IParameterTable | undefined>;
    getCount(): Promise<number>;
    get(parameterIndex: number): Promise<IParameter>;
    getAll(): Promise<IParameter[]>;
    getValue(parameterIndex: number): Promise<string | undefined>;
    getAllValue(): Promise<string[] | undefined>;
    getParameterDescriptorIndex(parameterIndex: number): Promise<number | undefined>;
    getAllParameterDescriptorIndex(): Promise<number[] | undefined>;
    getParameterDescriptor(parameterIndex: number): Promise<IParameterDescriptor | undefined>;
    getElementIndex(parameterIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(parameterIndex: number): Promise<IElement | undefined>;
}
export interface IElement {
    index: number;
    id?: bigint;
    type?: string;
    name?: string;
    uniqueId?: string;
    location_X?: number;
    location_Y?: number;
    location_Z?: number;
    familyName?: string;
    isPinned?: boolean;
    levelIndex?: number;
    level?: ILevel;
    phaseCreatedIndex?: number;
    phaseCreated?: IPhase;
    phaseDemolishedIndex?: number;
    phaseDemolished?: IPhase;
    categoryIndex?: number;
    category?: ICategory;
    worksetIndex?: number;
    workset?: IWorkset;
    designOptionIndex?: number;
    designOption?: IDesignOption;
    ownerViewIndex?: number;
    ownerView?: IView;
    groupIndex?: number;
    group?: IGroup;
    assemblyInstanceIndex?: number;
    assemblyInstance?: IAssemblyInstance;
    bimDocumentIndex?: number;
    bimDocument?: IBimDocument;
    roomIndex?: number;
    room?: IRoom;
}
export interface IElementTable {
    getCount(): Promise<number>;
    get(elementIndex: number): Promise<IElement>;
    getAll(): Promise<IElement[]>;
    getId(elementIndex: number): Promise<bigint | undefined>;
    getAllId(): Promise<BigInt64Array | undefined>;
    getType(elementIndex: number): Promise<string | undefined>;
    getAllType(): Promise<string[] | undefined>;
    getName(elementIndex: number): Promise<string | undefined>;
    getAllName(): Promise<string[] | undefined>;
    getUniqueId(elementIndex: number): Promise<string | undefined>;
    getAllUniqueId(): Promise<string[] | undefined>;
    getLocation_X(elementIndex: number): Promise<number | undefined>;
    getAllLocation_X(): Promise<number[] | undefined>;
    getLocation_Y(elementIndex: number): Promise<number | undefined>;
    getAllLocation_Y(): Promise<number[] | undefined>;
    getLocation_Z(elementIndex: number): Promise<number | undefined>;
    getAllLocation_Z(): Promise<number[] | undefined>;
    getFamilyName(elementIndex: number): Promise<string | undefined>;
    getAllFamilyName(): Promise<string[] | undefined>;
    getIsPinned(elementIndex: number): Promise<boolean | undefined>;
    getAllIsPinned(): Promise<boolean[] | undefined>;
    getLevelIndex(elementIndex: number): Promise<number | undefined>;
    getAllLevelIndex(): Promise<number[] | undefined>;
    getLevel(elementIndex: number): Promise<ILevel | undefined>;
    getPhaseCreatedIndex(elementIndex: number): Promise<number | undefined>;
    getAllPhaseCreatedIndex(): Promise<number[] | undefined>;
    getPhaseCreated(elementIndex: number): Promise<IPhase | undefined>;
    getPhaseDemolishedIndex(elementIndex: number): Promise<number | undefined>;
    getAllPhaseDemolishedIndex(): Promise<number[] | undefined>;
    getPhaseDemolished(elementIndex: number): Promise<IPhase | undefined>;
    getCategoryIndex(elementIndex: number): Promise<number | undefined>;
    getAllCategoryIndex(): Promise<number[] | undefined>;
    getCategory(elementIndex: number): Promise<ICategory | undefined>;
    getWorksetIndex(elementIndex: number): Promise<number | undefined>;
    getAllWorksetIndex(): Promise<number[] | undefined>;
    getWorkset(elementIndex: number): Promise<IWorkset | undefined>;
    getDesignOptionIndex(elementIndex: number): Promise<number | undefined>;
    getAllDesignOptionIndex(): Promise<number[] | undefined>;
    getDesignOption(elementIndex: number): Promise<IDesignOption | undefined>;
    getOwnerViewIndex(elementIndex: number): Promise<number | undefined>;
    getAllOwnerViewIndex(): Promise<number[] | undefined>;
    getOwnerView(elementIndex: number): Promise<IView | undefined>;
    getGroupIndex(elementIndex: number): Promise<number | undefined>;
    getAllGroupIndex(): Promise<number[] | undefined>;
    getGroup(elementIndex: number): Promise<IGroup | undefined>;
    getAssemblyInstanceIndex(elementIndex: number): Promise<number | undefined>;
    getAllAssemblyInstanceIndex(): Promise<number[] | undefined>;
    getAssemblyInstance(elementIndex: number): Promise<IAssemblyInstance | undefined>;
    getBimDocumentIndex(elementIndex: number): Promise<number | undefined>;
    getAllBimDocumentIndex(): Promise<number[] | undefined>;
    getBimDocument(elementIndex: number): Promise<IBimDocument | undefined>;
    getRoomIndex(elementIndex: number): Promise<number | undefined>;
    getAllRoomIndex(): Promise<number[] | undefined>;
    getRoom(elementIndex: number): Promise<IRoom | undefined>;
}
export declare class Element implements IElement {
    index: number;
    id?: bigint;
    type?: string;
    name?: string;
    uniqueId?: string;
    location_X?: number;
    location_Y?: number;
    location_Z?: number;
    familyName?: string;
    isPinned?: boolean;
    levelIndex?: number;
    level?: ILevel;
    phaseCreatedIndex?: number;
    phaseCreated?: IPhase;
    phaseDemolishedIndex?: number;
    phaseDemolished?: IPhase;
    categoryIndex?: number;
    category?: ICategory;
    worksetIndex?: number;
    workset?: IWorkset;
    designOptionIndex?: number;
    designOption?: IDesignOption;
    ownerViewIndex?: number;
    ownerView?: IView;
    groupIndex?: number;
    group?: IGroup;
    assemblyInstanceIndex?: number;
    assemblyInstance?: IAssemblyInstance;
    bimDocumentIndex?: number;
    bimDocument?: IBimDocument;
    roomIndex?: number;
    room?: IRoom;
    static createFromTable(table: IElementTable, index: number): Promise<IElement>;
}
export declare class ElementTable implements IElementTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IElementTable | undefined>;
    getCount(): Promise<number>;
    get(elementIndex: number): Promise<IElement>;
    getAll(): Promise<IElement[]>;
    getId(elementIndex: number): Promise<bigint | undefined>;
    getAllId(): Promise<BigInt64Array | undefined>;
    getType(elementIndex: number): Promise<string | undefined>;
    getAllType(): Promise<string[] | undefined>;
    getName(elementIndex: number): Promise<string | undefined>;
    getAllName(): Promise<string[] | undefined>;
    getUniqueId(elementIndex: number): Promise<string | undefined>;
    getAllUniqueId(): Promise<string[] | undefined>;
    getLocation_X(elementIndex: number): Promise<number | undefined>;
    getAllLocation_X(): Promise<number[] | undefined>;
    getLocation_Y(elementIndex: number): Promise<number | undefined>;
    getAllLocation_Y(): Promise<number[] | undefined>;
    getLocation_Z(elementIndex: number): Promise<number | undefined>;
    getAllLocation_Z(): Promise<number[] | undefined>;
    getFamilyName(elementIndex: number): Promise<string | undefined>;
    getAllFamilyName(): Promise<string[] | undefined>;
    getIsPinned(elementIndex: number): Promise<boolean | undefined>;
    getAllIsPinned(): Promise<boolean[] | undefined>;
    getLevelIndex(elementIndex: number): Promise<number | undefined>;
    getAllLevelIndex(): Promise<number[] | undefined>;
    getLevel(elementIndex: number): Promise<ILevel | undefined>;
    getPhaseCreatedIndex(elementIndex: number): Promise<number | undefined>;
    getAllPhaseCreatedIndex(): Promise<number[] | undefined>;
    getPhaseCreated(elementIndex: number): Promise<IPhase | undefined>;
    getPhaseDemolishedIndex(elementIndex: number): Promise<number | undefined>;
    getAllPhaseDemolishedIndex(): Promise<number[] | undefined>;
    getPhaseDemolished(elementIndex: number): Promise<IPhase | undefined>;
    getCategoryIndex(elementIndex: number): Promise<number | undefined>;
    getAllCategoryIndex(): Promise<number[] | undefined>;
    getCategory(elementIndex: number): Promise<ICategory | undefined>;
    getWorksetIndex(elementIndex: number): Promise<number | undefined>;
    getAllWorksetIndex(): Promise<number[] | undefined>;
    getWorkset(elementIndex: number): Promise<IWorkset | undefined>;
    getDesignOptionIndex(elementIndex: number): Promise<number | undefined>;
    getAllDesignOptionIndex(): Promise<number[] | undefined>;
    getDesignOption(elementIndex: number): Promise<IDesignOption | undefined>;
    getOwnerViewIndex(elementIndex: number): Promise<number | undefined>;
    getAllOwnerViewIndex(): Promise<number[] | undefined>;
    getOwnerView(elementIndex: number): Promise<IView | undefined>;
    getGroupIndex(elementIndex: number): Promise<number | undefined>;
    getAllGroupIndex(): Promise<number[] | undefined>;
    getGroup(elementIndex: number): Promise<IGroup | undefined>;
    getAssemblyInstanceIndex(elementIndex: number): Promise<number | undefined>;
    getAllAssemblyInstanceIndex(): Promise<number[] | undefined>;
    getAssemblyInstance(elementIndex: number): Promise<IAssemblyInstance | undefined>;
    getBimDocumentIndex(elementIndex: number): Promise<number | undefined>;
    getAllBimDocumentIndex(): Promise<number[] | undefined>;
    getBimDocument(elementIndex: number): Promise<IBimDocument | undefined>;
    getRoomIndex(elementIndex: number): Promise<number | undefined>;
    getAllRoomIndex(): Promise<number[] | undefined>;
    getRoom(elementIndex: number): Promise<IRoom | undefined>;
}
export interface IWorkset {
    index: number;
    id?: number;
    name?: string;
    kind?: string;
    isOpen?: boolean;
    isEditable?: boolean;
    owner?: string;
    uniqueId?: string;
    bimDocumentIndex?: number;
    bimDocument?: IBimDocument;
}
export interface IWorksetTable {
    getCount(): Promise<number>;
    get(worksetIndex: number): Promise<IWorkset>;
    getAll(): Promise<IWorkset[]>;
    getId(worksetIndex: number): Promise<number | undefined>;
    getAllId(): Promise<number[] | undefined>;
    getName(worksetIndex: number): Promise<string | undefined>;
    getAllName(): Promise<string[] | undefined>;
    getKind(worksetIndex: number): Promise<string | undefined>;
    getAllKind(): Promise<string[] | undefined>;
    getIsOpen(worksetIndex: number): Promise<boolean | undefined>;
    getAllIsOpen(): Promise<boolean[] | undefined>;
    getIsEditable(worksetIndex: number): Promise<boolean | undefined>;
    getAllIsEditable(): Promise<boolean[] | undefined>;
    getOwner(worksetIndex: number): Promise<string | undefined>;
    getAllOwner(): Promise<string[] | undefined>;
    getUniqueId(worksetIndex: number): Promise<string | undefined>;
    getAllUniqueId(): Promise<string[] | undefined>;
    getBimDocumentIndex(worksetIndex: number): Promise<number | undefined>;
    getAllBimDocumentIndex(): Promise<number[] | undefined>;
    getBimDocument(worksetIndex: number): Promise<IBimDocument | undefined>;
}
export declare class Workset implements IWorkset {
    index: number;
    id?: number;
    name?: string;
    kind?: string;
    isOpen?: boolean;
    isEditable?: boolean;
    owner?: string;
    uniqueId?: string;
    bimDocumentIndex?: number;
    bimDocument?: IBimDocument;
    static createFromTable(table: IWorksetTable, index: number): Promise<IWorkset>;
}
export declare class WorksetTable implements IWorksetTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IWorksetTable | undefined>;
    getCount(): Promise<number>;
    get(worksetIndex: number): Promise<IWorkset>;
    getAll(): Promise<IWorkset[]>;
    getId(worksetIndex: number): Promise<number | undefined>;
    getAllId(): Promise<number[] | undefined>;
    getName(worksetIndex: number): Promise<string | undefined>;
    getAllName(): Promise<string[] | undefined>;
    getKind(worksetIndex: number): Promise<string | undefined>;
    getAllKind(): Promise<string[] | undefined>;
    getIsOpen(worksetIndex: number): Promise<boolean | undefined>;
    getAllIsOpen(): Promise<boolean[] | undefined>;
    getIsEditable(worksetIndex: number): Promise<boolean | undefined>;
    getAllIsEditable(): Promise<boolean[] | undefined>;
    getOwner(worksetIndex: number): Promise<string | undefined>;
    getAllOwner(): Promise<string[] | undefined>;
    getUniqueId(worksetIndex: number): Promise<string | undefined>;
    getAllUniqueId(): Promise<string[] | undefined>;
    getBimDocumentIndex(worksetIndex: number): Promise<number | undefined>;
    getAllBimDocumentIndex(): Promise<number[] | undefined>;
    getBimDocument(worksetIndex: number): Promise<IBimDocument | undefined>;
}
export interface IAssemblyInstance {
    index: number;
    assemblyTypeName?: string;
    position_X?: number;
    position_Y?: number;
    position_Z?: number;
    elementIndex?: number;
    element?: IElement;
}
export interface IAssemblyInstanceTable {
    getCount(): Promise<number>;
    get(assemblyInstanceIndex: number): Promise<IAssemblyInstance>;
    getAll(): Promise<IAssemblyInstance[]>;
    getAssemblyTypeName(assemblyInstanceIndex: number): Promise<string | undefined>;
    getAllAssemblyTypeName(): Promise<string[] | undefined>;
    getPosition_X(assemblyInstanceIndex: number): Promise<number | undefined>;
    getAllPosition_X(): Promise<number[] | undefined>;
    getPosition_Y(assemblyInstanceIndex: number): Promise<number | undefined>;
    getAllPosition_Y(): Promise<number[] | undefined>;
    getPosition_Z(assemblyInstanceIndex: number): Promise<number | undefined>;
    getAllPosition_Z(): Promise<number[] | undefined>;
    getElementIndex(assemblyInstanceIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(assemblyInstanceIndex: number): Promise<IElement | undefined>;
}
export declare class AssemblyInstance implements IAssemblyInstance {
    index: number;
    assemblyTypeName?: string;
    position_X?: number;
    position_Y?: number;
    position_Z?: number;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IAssemblyInstanceTable, index: number): Promise<IAssemblyInstance>;
}
export declare class AssemblyInstanceTable implements IAssemblyInstanceTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IAssemblyInstanceTable | undefined>;
    getCount(): Promise<number>;
    get(assemblyInstanceIndex: number): Promise<IAssemblyInstance>;
    getAll(): Promise<IAssemblyInstance[]>;
    getAssemblyTypeName(assemblyInstanceIndex: number): Promise<string | undefined>;
    getAllAssemblyTypeName(): Promise<string[] | undefined>;
    getPosition_X(assemblyInstanceIndex: number): Promise<number | undefined>;
    getAllPosition_X(): Promise<number[] | undefined>;
    getPosition_Y(assemblyInstanceIndex: number): Promise<number | undefined>;
    getAllPosition_Y(): Promise<number[] | undefined>;
    getPosition_Z(assemblyInstanceIndex: number): Promise<number | undefined>;
    getAllPosition_Z(): Promise<number[] | undefined>;
    getElementIndex(assemblyInstanceIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(assemblyInstanceIndex: number): Promise<IElement | undefined>;
}
export interface IGroup {
    index: number;
    groupType?: string;
    position_X?: number;
    position_Y?: number;
    position_Z?: number;
    elementIndex?: number;
    element?: IElement;
}
export interface IGroupTable {
    getCount(): Promise<number>;
    get(groupIndex: number): Promise<IGroup>;
    getAll(): Promise<IGroup[]>;
    getGroupType(groupIndex: number): Promise<string | undefined>;
    getAllGroupType(): Promise<string[] | undefined>;
    getPosition_X(groupIndex: number): Promise<number | undefined>;
    getAllPosition_X(): Promise<number[] | undefined>;
    getPosition_Y(groupIndex: number): Promise<number | undefined>;
    getAllPosition_Y(): Promise<number[] | undefined>;
    getPosition_Z(groupIndex: number): Promise<number | undefined>;
    getAllPosition_Z(): Promise<number[] | undefined>;
    getElementIndex(groupIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(groupIndex: number): Promise<IElement | undefined>;
}
export declare class Group implements IGroup {
    index: number;
    groupType?: string;
    position_X?: number;
    position_Y?: number;
    position_Z?: number;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IGroupTable, index: number): Promise<IGroup>;
}
export declare class GroupTable implements IGroupTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IGroupTable | undefined>;
    getCount(): Promise<number>;
    get(groupIndex: number): Promise<IGroup>;
    getAll(): Promise<IGroup[]>;
    getGroupType(groupIndex: number): Promise<string | undefined>;
    getAllGroupType(): Promise<string[] | undefined>;
    getPosition_X(groupIndex: number): Promise<number | undefined>;
    getAllPosition_X(): Promise<number[] | undefined>;
    getPosition_Y(groupIndex: number): Promise<number | undefined>;
    getAllPosition_Y(): Promise<number[] | undefined>;
    getPosition_Z(groupIndex: number): Promise<number | undefined>;
    getAllPosition_Z(): Promise<number[] | undefined>;
    getElementIndex(groupIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(groupIndex: number): Promise<IElement | undefined>;
}
export interface IDesignOption {
    index: number;
    isPrimary?: boolean;
    elementIndex?: number;
    element?: IElement;
}
export interface IDesignOptionTable {
    getCount(): Promise<number>;
    get(designOptionIndex: number): Promise<IDesignOption>;
    getAll(): Promise<IDesignOption[]>;
    getIsPrimary(designOptionIndex: number): Promise<boolean | undefined>;
    getAllIsPrimary(): Promise<boolean[] | undefined>;
    getElementIndex(designOptionIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(designOptionIndex: number): Promise<IElement | undefined>;
}
export declare class DesignOption implements IDesignOption {
    index: number;
    isPrimary?: boolean;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IDesignOptionTable, index: number): Promise<IDesignOption>;
}
export declare class DesignOptionTable implements IDesignOptionTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IDesignOptionTable | undefined>;
    getCount(): Promise<number>;
    get(designOptionIndex: number): Promise<IDesignOption>;
    getAll(): Promise<IDesignOption[]>;
    getIsPrimary(designOptionIndex: number): Promise<boolean | undefined>;
    getAllIsPrimary(): Promise<boolean[] | undefined>;
    getElementIndex(designOptionIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(designOptionIndex: number): Promise<IElement | undefined>;
}
export interface ILevel {
    index: number;
    elevation?: number;
    familyTypeIndex?: number;
    familyType?: IFamilyType;
    buildingIndex?: number;
    building?: IBuilding;
    elementIndex?: number;
    element?: IElement;
}
export interface ILevelTable {
    getCount(): Promise<number>;
    get(levelIndex: number): Promise<ILevel>;
    getAll(): Promise<ILevel[]>;
    getElevation(levelIndex: number): Promise<number | undefined>;
    getAllElevation(): Promise<number[] | undefined>;
    getFamilyTypeIndex(levelIndex: number): Promise<number | undefined>;
    getAllFamilyTypeIndex(): Promise<number[] | undefined>;
    getFamilyType(levelIndex: number): Promise<IFamilyType | undefined>;
    getBuildingIndex(levelIndex: number): Promise<number | undefined>;
    getAllBuildingIndex(): Promise<number[] | undefined>;
    getBuilding(levelIndex: number): Promise<IBuilding | undefined>;
    getElementIndex(levelIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(levelIndex: number): Promise<IElement | undefined>;
}
export declare class Level implements ILevel {
    index: number;
    elevation?: number;
    familyTypeIndex?: number;
    familyType?: IFamilyType;
    buildingIndex?: number;
    building?: IBuilding;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: ILevelTable, index: number): Promise<ILevel>;
}
export declare class LevelTable implements ILevelTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<ILevelTable | undefined>;
    getCount(): Promise<number>;
    get(levelIndex: number): Promise<ILevel>;
    getAll(): Promise<ILevel[]>;
    getElevation(levelIndex: number): Promise<number | undefined>;
    getAllElevation(): Promise<number[] | undefined>;
    getFamilyTypeIndex(levelIndex: number): Promise<number | undefined>;
    getAllFamilyTypeIndex(): Promise<number[] | undefined>;
    getFamilyType(levelIndex: number): Promise<IFamilyType | undefined>;
    getBuildingIndex(levelIndex: number): Promise<number | undefined>;
    getAllBuildingIndex(): Promise<number[] | undefined>;
    getBuilding(levelIndex: number): Promise<IBuilding | undefined>;
    getElementIndex(levelIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(levelIndex: number): Promise<IElement | undefined>;
}
export interface IPhase {
    index: number;
    elementIndex?: number;
    element?: IElement;
}
export interface IPhaseTable {
    getCount(): Promise<number>;
    get(phaseIndex: number): Promise<IPhase>;
    getAll(): Promise<IPhase[]>;
    getElementIndex(phaseIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(phaseIndex: number): Promise<IElement | undefined>;
}
export declare class Phase implements IPhase {
    index: number;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IPhaseTable, index: number): Promise<IPhase>;
}
export declare class PhaseTable implements IPhaseTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IPhaseTable | undefined>;
    getCount(): Promise<number>;
    get(phaseIndex: number): Promise<IPhase>;
    getAll(): Promise<IPhase[]>;
    getElementIndex(phaseIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(phaseIndex: number): Promise<IElement | undefined>;
}
export interface IRoom {
    index: number;
    baseOffset?: number;
    limitOffset?: number;
    unboundedHeight?: number;
    volume?: number;
    perimeter?: number;
    area?: number;
    number?: string;
    upperLimitIndex?: number;
    upperLimit?: ILevel;
    elementIndex?: number;
    element?: IElement;
}
export interface IRoomTable {
    getCount(): Promise<number>;
    get(roomIndex: number): Promise<IRoom>;
    getAll(): Promise<IRoom[]>;
    getBaseOffset(roomIndex: number): Promise<number | undefined>;
    getAllBaseOffset(): Promise<number[] | undefined>;
    getLimitOffset(roomIndex: number): Promise<number | undefined>;
    getAllLimitOffset(): Promise<number[] | undefined>;
    getUnboundedHeight(roomIndex: number): Promise<number | undefined>;
    getAllUnboundedHeight(): Promise<number[] | undefined>;
    getVolume(roomIndex: number): Promise<number | undefined>;
    getAllVolume(): Promise<number[] | undefined>;
    getPerimeter(roomIndex: number): Promise<number | undefined>;
    getAllPerimeter(): Promise<number[] | undefined>;
    getArea(roomIndex: number): Promise<number | undefined>;
    getAllArea(): Promise<number[] | undefined>;
    getNumber(roomIndex: number): Promise<string | undefined>;
    getAllNumber(): Promise<string[] | undefined>;
    getUpperLimitIndex(roomIndex: number): Promise<number | undefined>;
    getAllUpperLimitIndex(): Promise<number[] | undefined>;
    getUpperLimit(roomIndex: number): Promise<ILevel | undefined>;
    getElementIndex(roomIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(roomIndex: number): Promise<IElement | undefined>;
}
export declare class Room implements IRoom {
    index: number;
    baseOffset?: number;
    limitOffset?: number;
    unboundedHeight?: number;
    volume?: number;
    perimeter?: number;
    area?: number;
    number?: string;
    upperLimitIndex?: number;
    upperLimit?: ILevel;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IRoomTable, index: number): Promise<IRoom>;
}
export declare class RoomTable implements IRoomTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IRoomTable | undefined>;
    getCount(): Promise<number>;
    get(roomIndex: number): Promise<IRoom>;
    getAll(): Promise<IRoom[]>;
    getBaseOffset(roomIndex: number): Promise<number | undefined>;
    getAllBaseOffset(): Promise<number[] | undefined>;
    getLimitOffset(roomIndex: number): Promise<number | undefined>;
    getAllLimitOffset(): Promise<number[] | undefined>;
    getUnboundedHeight(roomIndex: number): Promise<number | undefined>;
    getAllUnboundedHeight(): Promise<number[] | undefined>;
    getVolume(roomIndex: number): Promise<number | undefined>;
    getAllVolume(): Promise<number[] | undefined>;
    getPerimeter(roomIndex: number): Promise<number | undefined>;
    getAllPerimeter(): Promise<number[] | undefined>;
    getArea(roomIndex: number): Promise<number | undefined>;
    getAllArea(): Promise<number[] | undefined>;
    getNumber(roomIndex: number): Promise<string | undefined>;
    getAllNumber(): Promise<string[] | undefined>;
    getUpperLimitIndex(roomIndex: number): Promise<number | undefined>;
    getAllUpperLimitIndex(): Promise<number[] | undefined>;
    getUpperLimit(roomIndex: number): Promise<ILevel | undefined>;
    getElementIndex(roomIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(roomIndex: number): Promise<IElement | undefined>;
}
export interface IBimDocument {
    index: number;
    title?: string;
    isMetric?: boolean;
    guid?: string;
    numSaves?: number;
    isLinked?: boolean;
    isDetached?: boolean;
    isWorkshared?: boolean;
    pathName?: string;
    latitude?: number;
    longitude?: number;
    timeZone?: number;
    placeName?: string;
    weatherStationName?: string;
    elevation?: number;
    projectLocation?: string;
    issueDate?: string;
    status?: string;
    clientName?: string;
    address?: string;
    name?: string;
    number?: string;
    author?: string;
    buildingName?: string;
    organizationName?: string;
    organizationDescription?: string;
    product?: string;
    version?: string;
    user?: string;
    activeViewIndex?: number;
    activeView?: IView;
    ownerFamilyIndex?: number;
    ownerFamily?: IFamily;
    parentIndex?: number;
    parent?: IBimDocument;
    elementIndex?: number;
    element?: IElement;
}
export interface IBimDocumentTable {
    getCount(): Promise<number>;
    get(bimDocumentIndex: number): Promise<IBimDocument>;
    getAll(): Promise<IBimDocument[]>;
    getTitle(bimDocumentIndex: number): Promise<string | undefined>;
    getAllTitle(): Promise<string[] | undefined>;
    getIsMetric(bimDocumentIndex: number): Promise<boolean | undefined>;
    getAllIsMetric(): Promise<boolean[] | undefined>;
    getGuid(bimDocumentIndex: number): Promise<string | undefined>;
    getAllGuid(): Promise<string[] | undefined>;
    getNumSaves(bimDocumentIndex: number): Promise<number | undefined>;
    getAllNumSaves(): Promise<number[] | undefined>;
    getIsLinked(bimDocumentIndex: number): Promise<boolean | undefined>;
    getAllIsLinked(): Promise<boolean[] | undefined>;
    getIsDetached(bimDocumentIndex: number): Promise<boolean | undefined>;
    getAllIsDetached(): Promise<boolean[] | undefined>;
    getIsWorkshared(bimDocumentIndex: number): Promise<boolean | undefined>;
    getAllIsWorkshared(): Promise<boolean[] | undefined>;
    getPathName(bimDocumentIndex: number): Promise<string | undefined>;
    getAllPathName(): Promise<string[] | undefined>;
    getLatitude(bimDocumentIndex: number): Promise<number | undefined>;
    getAllLatitude(): Promise<number[] | undefined>;
    getLongitude(bimDocumentIndex: number): Promise<number | undefined>;
    getAllLongitude(): Promise<number[] | undefined>;
    getTimeZone(bimDocumentIndex: number): Promise<number | undefined>;
    getAllTimeZone(): Promise<number[] | undefined>;
    getPlaceName(bimDocumentIndex: number): Promise<string | undefined>;
    getAllPlaceName(): Promise<string[] | undefined>;
    getWeatherStationName(bimDocumentIndex: number): Promise<string | undefined>;
    getAllWeatherStationName(): Promise<string[] | undefined>;
    getElevation(bimDocumentIndex: number): Promise<number | undefined>;
    getAllElevation(): Promise<number[] | undefined>;
    getProjectLocation(bimDocumentIndex: number): Promise<string | undefined>;
    getAllProjectLocation(): Promise<string[] | undefined>;
    getIssueDate(bimDocumentIndex: number): Promise<string | undefined>;
    getAllIssueDate(): Promise<string[] | undefined>;
    getStatus(bimDocumentIndex: number): Promise<string | undefined>;
    getAllStatus(): Promise<string[] | undefined>;
    getClientName(bimDocumentIndex: number): Promise<string | undefined>;
    getAllClientName(): Promise<string[] | undefined>;
    getAddress(bimDocumentIndex: number): Promise<string | undefined>;
    getAllAddress(): Promise<string[] | undefined>;
    getName(bimDocumentIndex: number): Promise<string | undefined>;
    getAllName(): Promise<string[] | undefined>;
    getNumber(bimDocumentIndex: number): Promise<string | undefined>;
    getAllNumber(): Promise<string[] | undefined>;
    getAuthor(bimDocumentIndex: number): Promise<string | undefined>;
    getAllAuthor(): Promise<string[] | undefined>;
    getBuildingName(bimDocumentIndex: number): Promise<string | undefined>;
    getAllBuildingName(): Promise<string[] | undefined>;
    getOrganizationName(bimDocumentIndex: number): Promise<string | undefined>;
    getAllOrganizationName(): Promise<string[] | undefined>;
    getOrganizationDescription(bimDocumentIndex: number): Promise<string | undefined>;
    getAllOrganizationDescription(): Promise<string[] | undefined>;
    getProduct(bimDocumentIndex: number): Promise<string | undefined>;
    getAllProduct(): Promise<string[] | undefined>;
    getVersion(bimDocumentIndex: number): Promise<string | undefined>;
    getAllVersion(): Promise<string[] | undefined>;
    getUser(bimDocumentIndex: number): Promise<string | undefined>;
    getAllUser(): Promise<string[] | undefined>;
    getActiveViewIndex(bimDocumentIndex: number): Promise<number | undefined>;
    getAllActiveViewIndex(): Promise<number[] | undefined>;
    getActiveView(bimDocumentIndex: number): Promise<IView | undefined>;
    getOwnerFamilyIndex(bimDocumentIndex: number): Promise<number | undefined>;
    getAllOwnerFamilyIndex(): Promise<number[] | undefined>;
    getOwnerFamily(bimDocumentIndex: number): Promise<IFamily | undefined>;
    getParentIndex(bimDocumentIndex: number): Promise<number | undefined>;
    getAllParentIndex(): Promise<number[] | undefined>;
    getParent(bimDocumentIndex: number): Promise<IBimDocument | undefined>;
    getElementIndex(bimDocumentIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(bimDocumentIndex: number): Promise<IElement | undefined>;
}
export declare class BimDocument implements IBimDocument {
    index: number;
    title?: string;
    isMetric?: boolean;
    guid?: string;
    numSaves?: number;
    isLinked?: boolean;
    isDetached?: boolean;
    isWorkshared?: boolean;
    pathName?: string;
    latitude?: number;
    longitude?: number;
    timeZone?: number;
    placeName?: string;
    weatherStationName?: string;
    elevation?: number;
    projectLocation?: string;
    issueDate?: string;
    status?: string;
    clientName?: string;
    address?: string;
    name?: string;
    number?: string;
    author?: string;
    buildingName?: string;
    organizationName?: string;
    organizationDescription?: string;
    product?: string;
    version?: string;
    user?: string;
    activeViewIndex?: number;
    activeView?: IView;
    ownerFamilyIndex?: number;
    ownerFamily?: IFamily;
    parentIndex?: number;
    parent?: IBimDocument;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IBimDocumentTable, index: number): Promise<IBimDocument>;
}
export declare class BimDocumentTable implements IBimDocumentTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IBimDocumentTable | undefined>;
    getCount(): Promise<number>;
    get(bimDocumentIndex: number): Promise<IBimDocument>;
    getAll(): Promise<IBimDocument[]>;
    getTitle(bimDocumentIndex: number): Promise<string | undefined>;
    getAllTitle(): Promise<string[] | undefined>;
    getIsMetric(bimDocumentIndex: number): Promise<boolean | undefined>;
    getAllIsMetric(): Promise<boolean[] | undefined>;
    getGuid(bimDocumentIndex: number): Promise<string | undefined>;
    getAllGuid(): Promise<string[] | undefined>;
    getNumSaves(bimDocumentIndex: number): Promise<number | undefined>;
    getAllNumSaves(): Promise<number[] | undefined>;
    getIsLinked(bimDocumentIndex: number): Promise<boolean | undefined>;
    getAllIsLinked(): Promise<boolean[] | undefined>;
    getIsDetached(bimDocumentIndex: number): Promise<boolean | undefined>;
    getAllIsDetached(): Promise<boolean[] | undefined>;
    getIsWorkshared(bimDocumentIndex: number): Promise<boolean | undefined>;
    getAllIsWorkshared(): Promise<boolean[] | undefined>;
    getPathName(bimDocumentIndex: number): Promise<string | undefined>;
    getAllPathName(): Promise<string[] | undefined>;
    getLatitude(bimDocumentIndex: number): Promise<number | undefined>;
    getAllLatitude(): Promise<number[] | undefined>;
    getLongitude(bimDocumentIndex: number): Promise<number | undefined>;
    getAllLongitude(): Promise<number[] | undefined>;
    getTimeZone(bimDocumentIndex: number): Promise<number | undefined>;
    getAllTimeZone(): Promise<number[] | undefined>;
    getPlaceName(bimDocumentIndex: number): Promise<string | undefined>;
    getAllPlaceName(): Promise<string[] | undefined>;
    getWeatherStationName(bimDocumentIndex: number): Promise<string | undefined>;
    getAllWeatherStationName(): Promise<string[] | undefined>;
    getElevation(bimDocumentIndex: number): Promise<number | undefined>;
    getAllElevation(): Promise<number[] | undefined>;
    getProjectLocation(bimDocumentIndex: number): Promise<string | undefined>;
    getAllProjectLocation(): Promise<string[] | undefined>;
    getIssueDate(bimDocumentIndex: number): Promise<string | undefined>;
    getAllIssueDate(): Promise<string[] | undefined>;
    getStatus(bimDocumentIndex: number): Promise<string | undefined>;
    getAllStatus(): Promise<string[] | undefined>;
    getClientName(bimDocumentIndex: number): Promise<string | undefined>;
    getAllClientName(): Promise<string[] | undefined>;
    getAddress(bimDocumentIndex: number): Promise<string | undefined>;
    getAllAddress(): Promise<string[] | undefined>;
    getName(bimDocumentIndex: number): Promise<string | undefined>;
    getAllName(): Promise<string[] | undefined>;
    getNumber(bimDocumentIndex: number): Promise<string | undefined>;
    getAllNumber(): Promise<string[] | undefined>;
    getAuthor(bimDocumentIndex: number): Promise<string | undefined>;
    getAllAuthor(): Promise<string[] | undefined>;
    getBuildingName(bimDocumentIndex: number): Promise<string | undefined>;
    getAllBuildingName(): Promise<string[] | undefined>;
    getOrganizationName(bimDocumentIndex: number): Promise<string | undefined>;
    getAllOrganizationName(): Promise<string[] | undefined>;
    getOrganizationDescription(bimDocumentIndex: number): Promise<string | undefined>;
    getAllOrganizationDescription(): Promise<string[] | undefined>;
    getProduct(bimDocumentIndex: number): Promise<string | undefined>;
    getAllProduct(): Promise<string[] | undefined>;
    getVersion(bimDocumentIndex: number): Promise<string | undefined>;
    getAllVersion(): Promise<string[] | undefined>;
    getUser(bimDocumentIndex: number): Promise<string | undefined>;
    getAllUser(): Promise<string[] | undefined>;
    getActiveViewIndex(bimDocumentIndex: number): Promise<number | undefined>;
    getAllActiveViewIndex(): Promise<number[] | undefined>;
    getActiveView(bimDocumentIndex: number): Promise<IView | undefined>;
    getOwnerFamilyIndex(bimDocumentIndex: number): Promise<number | undefined>;
    getAllOwnerFamilyIndex(): Promise<number[] | undefined>;
    getOwnerFamily(bimDocumentIndex: number): Promise<IFamily | undefined>;
    getParentIndex(bimDocumentIndex: number): Promise<number | undefined>;
    getAllParentIndex(): Promise<number[] | undefined>;
    getParent(bimDocumentIndex: number): Promise<IBimDocument | undefined>;
    getElementIndex(bimDocumentIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(bimDocumentIndex: number): Promise<IElement | undefined>;
}
export interface IDisplayUnitInBimDocument {
    index: number;
    displayUnitIndex?: number;
    displayUnit?: IDisplayUnit;
    bimDocumentIndex?: number;
    bimDocument?: IBimDocument;
}
export interface IDisplayUnitInBimDocumentTable {
    getCount(): Promise<number>;
    get(displayUnitInBimDocumentIndex: number): Promise<IDisplayUnitInBimDocument>;
    getAll(): Promise<IDisplayUnitInBimDocument[]>;
    getDisplayUnitIndex(displayUnitInBimDocumentIndex: number): Promise<number | undefined>;
    getAllDisplayUnitIndex(): Promise<number[] | undefined>;
    getDisplayUnit(displayUnitInBimDocumentIndex: number): Promise<IDisplayUnit | undefined>;
    getBimDocumentIndex(displayUnitInBimDocumentIndex: number): Promise<number | undefined>;
    getAllBimDocumentIndex(): Promise<number[] | undefined>;
    getBimDocument(displayUnitInBimDocumentIndex: number): Promise<IBimDocument | undefined>;
}
export declare class DisplayUnitInBimDocument implements IDisplayUnitInBimDocument {
    index: number;
    displayUnitIndex?: number;
    displayUnit?: IDisplayUnit;
    bimDocumentIndex?: number;
    bimDocument?: IBimDocument;
    static createFromTable(table: IDisplayUnitInBimDocumentTable, index: number): Promise<IDisplayUnitInBimDocument>;
}
export declare class DisplayUnitInBimDocumentTable implements IDisplayUnitInBimDocumentTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IDisplayUnitInBimDocumentTable | undefined>;
    getCount(): Promise<number>;
    get(displayUnitInBimDocumentIndex: number): Promise<IDisplayUnitInBimDocument>;
    getAll(): Promise<IDisplayUnitInBimDocument[]>;
    getDisplayUnitIndex(displayUnitInBimDocumentIndex: number): Promise<number | undefined>;
    getAllDisplayUnitIndex(): Promise<number[] | undefined>;
    getDisplayUnit(displayUnitInBimDocumentIndex: number): Promise<IDisplayUnit | undefined>;
    getBimDocumentIndex(displayUnitInBimDocumentIndex: number): Promise<number | undefined>;
    getAllBimDocumentIndex(): Promise<number[] | undefined>;
    getBimDocument(displayUnitInBimDocumentIndex: number): Promise<IBimDocument | undefined>;
}
export interface IPhaseOrderInBimDocument {
    index: number;
    orderIndex?: number;
    phaseIndex?: number;
    phase?: IPhase;
    bimDocumentIndex?: number;
    bimDocument?: IBimDocument;
}
export interface IPhaseOrderInBimDocumentTable {
    getCount(): Promise<number>;
    get(phaseOrderInBimDocumentIndex: number): Promise<IPhaseOrderInBimDocument>;
    getAll(): Promise<IPhaseOrderInBimDocument[]>;
    getOrderIndex(phaseOrderInBimDocumentIndex: number): Promise<number | undefined>;
    getAllOrderIndex(): Promise<number[] | undefined>;
    getPhaseIndex(phaseOrderInBimDocumentIndex: number): Promise<number | undefined>;
    getAllPhaseIndex(): Promise<number[] | undefined>;
    getPhase(phaseOrderInBimDocumentIndex: number): Promise<IPhase | undefined>;
    getBimDocumentIndex(phaseOrderInBimDocumentIndex: number): Promise<number | undefined>;
    getAllBimDocumentIndex(): Promise<number[] | undefined>;
    getBimDocument(phaseOrderInBimDocumentIndex: number): Promise<IBimDocument | undefined>;
}
export declare class PhaseOrderInBimDocument implements IPhaseOrderInBimDocument {
    index: number;
    orderIndex?: number;
    phaseIndex?: number;
    phase?: IPhase;
    bimDocumentIndex?: number;
    bimDocument?: IBimDocument;
    static createFromTable(table: IPhaseOrderInBimDocumentTable, index: number): Promise<IPhaseOrderInBimDocument>;
}
export declare class PhaseOrderInBimDocumentTable implements IPhaseOrderInBimDocumentTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IPhaseOrderInBimDocumentTable | undefined>;
    getCount(): Promise<number>;
    get(phaseOrderInBimDocumentIndex: number): Promise<IPhaseOrderInBimDocument>;
    getAll(): Promise<IPhaseOrderInBimDocument[]>;
    getOrderIndex(phaseOrderInBimDocumentIndex: number): Promise<number | undefined>;
    getAllOrderIndex(): Promise<number[] | undefined>;
    getPhaseIndex(phaseOrderInBimDocumentIndex: number): Promise<number | undefined>;
    getAllPhaseIndex(): Promise<number[] | undefined>;
    getPhase(phaseOrderInBimDocumentIndex: number): Promise<IPhase | undefined>;
    getBimDocumentIndex(phaseOrderInBimDocumentIndex: number): Promise<number | undefined>;
    getAllBimDocumentIndex(): Promise<number[] | undefined>;
    getBimDocument(phaseOrderInBimDocumentIndex: number): Promise<IBimDocument | undefined>;
}
export interface ICategory {
    index: number;
    name?: string;
    id?: bigint;
    categoryType?: string;
    lineColor_X?: number;
    lineColor_Y?: number;
    lineColor_Z?: number;
    builtInCategory?: string;
    parentIndex?: number;
    parent?: ICategory;
    materialIndex?: number;
    material?: IMaterial;
}
export interface ICategoryTable {
    getCount(): Promise<number>;
    get(categoryIndex: number): Promise<ICategory>;
    getAll(): Promise<ICategory[]>;
    getName(categoryIndex: number): Promise<string | undefined>;
    getAllName(): Promise<string[] | undefined>;
    getId(categoryIndex: number): Promise<bigint | undefined>;
    getAllId(): Promise<BigInt64Array | undefined>;
    getCategoryType(categoryIndex: number): Promise<string | undefined>;
    getAllCategoryType(): Promise<string[] | undefined>;
    getLineColor_X(categoryIndex: number): Promise<number | undefined>;
    getAllLineColor_X(): Promise<number[] | undefined>;
    getLineColor_Y(categoryIndex: number): Promise<number | undefined>;
    getAllLineColor_Y(): Promise<number[] | undefined>;
    getLineColor_Z(categoryIndex: number): Promise<number | undefined>;
    getAllLineColor_Z(): Promise<number[] | undefined>;
    getBuiltInCategory(categoryIndex: number): Promise<string | undefined>;
    getAllBuiltInCategory(): Promise<string[] | undefined>;
    getParentIndex(categoryIndex: number): Promise<number | undefined>;
    getAllParentIndex(): Promise<number[] | undefined>;
    getParent(categoryIndex: number): Promise<ICategory | undefined>;
    getMaterialIndex(categoryIndex: number): Promise<number | undefined>;
    getAllMaterialIndex(): Promise<number[] | undefined>;
    getMaterial(categoryIndex: number): Promise<IMaterial | undefined>;
}
export declare class Category implements ICategory {
    index: number;
    name?: string;
    id?: bigint;
    categoryType?: string;
    lineColor_X?: number;
    lineColor_Y?: number;
    lineColor_Z?: number;
    builtInCategory?: string;
    parentIndex?: number;
    parent?: ICategory;
    materialIndex?: number;
    material?: IMaterial;
    static createFromTable(table: ICategoryTable, index: number): Promise<ICategory>;
}
export declare class CategoryTable implements ICategoryTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<ICategoryTable | undefined>;
    getCount(): Promise<number>;
    get(categoryIndex: number): Promise<ICategory>;
    getAll(): Promise<ICategory[]>;
    getName(categoryIndex: number): Promise<string | undefined>;
    getAllName(): Promise<string[] | undefined>;
    getId(categoryIndex: number): Promise<bigint | undefined>;
    getAllId(): Promise<BigInt64Array | undefined>;
    getCategoryType(categoryIndex: number): Promise<string | undefined>;
    getAllCategoryType(): Promise<string[] | undefined>;
    getLineColor_X(categoryIndex: number): Promise<number | undefined>;
    getAllLineColor_X(): Promise<number[] | undefined>;
    getLineColor_Y(categoryIndex: number): Promise<number | undefined>;
    getAllLineColor_Y(): Promise<number[] | undefined>;
    getLineColor_Z(categoryIndex: number): Promise<number | undefined>;
    getAllLineColor_Z(): Promise<number[] | undefined>;
    getBuiltInCategory(categoryIndex: number): Promise<string | undefined>;
    getAllBuiltInCategory(): Promise<string[] | undefined>;
    getParentIndex(categoryIndex: number): Promise<number | undefined>;
    getAllParentIndex(): Promise<number[] | undefined>;
    getParent(categoryIndex: number): Promise<ICategory | undefined>;
    getMaterialIndex(categoryIndex: number): Promise<number | undefined>;
    getAllMaterialIndex(): Promise<number[] | undefined>;
    getMaterial(categoryIndex: number): Promise<IMaterial | undefined>;
}
export interface IFamily {
    index: number;
    structuralMaterialType?: string;
    structuralSectionShape?: string;
    isSystemFamily?: boolean;
    isInPlace?: boolean;
    familyCategoryIndex?: number;
    familyCategory?: ICategory;
    elementIndex?: number;
    element?: IElement;
}
export interface IFamilyTable {
    getCount(): Promise<number>;
    get(familyIndex: number): Promise<IFamily>;
    getAll(): Promise<IFamily[]>;
    getStructuralMaterialType(familyIndex: number): Promise<string | undefined>;
    getAllStructuralMaterialType(): Promise<string[] | undefined>;
    getStructuralSectionShape(familyIndex: number): Promise<string | undefined>;
    getAllStructuralSectionShape(): Promise<string[] | undefined>;
    getIsSystemFamily(familyIndex: number): Promise<boolean | undefined>;
    getAllIsSystemFamily(): Promise<boolean[] | undefined>;
    getIsInPlace(familyIndex: number): Promise<boolean | undefined>;
    getAllIsInPlace(): Promise<boolean[] | undefined>;
    getFamilyCategoryIndex(familyIndex: number): Promise<number | undefined>;
    getAllFamilyCategoryIndex(): Promise<number[] | undefined>;
    getFamilyCategory(familyIndex: number): Promise<ICategory | undefined>;
    getElementIndex(familyIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(familyIndex: number): Promise<IElement | undefined>;
}
export declare class Family implements IFamily {
    index: number;
    structuralMaterialType?: string;
    structuralSectionShape?: string;
    isSystemFamily?: boolean;
    isInPlace?: boolean;
    familyCategoryIndex?: number;
    familyCategory?: ICategory;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IFamilyTable, index: number): Promise<IFamily>;
}
export declare class FamilyTable implements IFamilyTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IFamilyTable | undefined>;
    getCount(): Promise<number>;
    get(familyIndex: number): Promise<IFamily>;
    getAll(): Promise<IFamily[]>;
    getStructuralMaterialType(familyIndex: number): Promise<string | undefined>;
    getAllStructuralMaterialType(): Promise<string[] | undefined>;
    getStructuralSectionShape(familyIndex: number): Promise<string | undefined>;
    getAllStructuralSectionShape(): Promise<string[] | undefined>;
    getIsSystemFamily(familyIndex: number): Promise<boolean | undefined>;
    getAllIsSystemFamily(): Promise<boolean[] | undefined>;
    getIsInPlace(familyIndex: number): Promise<boolean | undefined>;
    getAllIsInPlace(): Promise<boolean[] | undefined>;
    getFamilyCategoryIndex(familyIndex: number): Promise<number | undefined>;
    getAllFamilyCategoryIndex(): Promise<number[] | undefined>;
    getFamilyCategory(familyIndex: number): Promise<ICategory | undefined>;
    getElementIndex(familyIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(familyIndex: number): Promise<IElement | undefined>;
}
export interface IFamilyType {
    index: number;
    isSystemFamilyType?: boolean;
    familyIndex?: number;
    family?: IFamily;
    compoundStructureIndex?: number;
    compoundStructure?: ICompoundStructure;
    elementIndex?: number;
    element?: IElement;
}
export interface IFamilyTypeTable {
    getCount(): Promise<number>;
    get(familyTypeIndex: number): Promise<IFamilyType>;
    getAll(): Promise<IFamilyType[]>;
    getIsSystemFamilyType(familyTypeIndex: number): Promise<boolean | undefined>;
    getAllIsSystemFamilyType(): Promise<boolean[] | undefined>;
    getFamilyIndex(familyTypeIndex: number): Promise<number | undefined>;
    getAllFamilyIndex(): Promise<number[] | undefined>;
    getFamily(familyTypeIndex: number): Promise<IFamily | undefined>;
    getCompoundStructureIndex(familyTypeIndex: number): Promise<number | undefined>;
    getAllCompoundStructureIndex(): Promise<number[] | undefined>;
    getCompoundStructure(familyTypeIndex: number): Promise<ICompoundStructure | undefined>;
    getElementIndex(familyTypeIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(familyTypeIndex: number): Promise<IElement | undefined>;
}
export declare class FamilyType implements IFamilyType {
    index: number;
    isSystemFamilyType?: boolean;
    familyIndex?: number;
    family?: IFamily;
    compoundStructureIndex?: number;
    compoundStructure?: ICompoundStructure;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IFamilyTypeTable, index: number): Promise<IFamilyType>;
}
export declare class FamilyTypeTable implements IFamilyTypeTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IFamilyTypeTable | undefined>;
    getCount(): Promise<number>;
    get(familyTypeIndex: number): Promise<IFamilyType>;
    getAll(): Promise<IFamilyType[]>;
    getIsSystemFamilyType(familyTypeIndex: number): Promise<boolean | undefined>;
    getAllIsSystemFamilyType(): Promise<boolean[] | undefined>;
    getFamilyIndex(familyTypeIndex: number): Promise<number | undefined>;
    getAllFamilyIndex(): Promise<number[] | undefined>;
    getFamily(familyTypeIndex: number): Promise<IFamily | undefined>;
    getCompoundStructureIndex(familyTypeIndex: number): Promise<number | undefined>;
    getAllCompoundStructureIndex(): Promise<number[] | undefined>;
    getCompoundStructure(familyTypeIndex: number): Promise<ICompoundStructure | undefined>;
    getElementIndex(familyTypeIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(familyTypeIndex: number): Promise<IElement | undefined>;
}
export interface IFamilyInstance {
    index: number;
    facingFlipped?: boolean;
    facingOrientation_X?: number;
    facingOrientation_Y?: number;
    facingOrientation_Z?: number;
    handFlipped?: boolean;
    mirrored?: boolean;
    hasModifiedGeometry?: boolean;
    scale?: number;
    basisX_X?: number;
    basisX_Y?: number;
    basisX_Z?: number;
    basisY_X?: number;
    basisY_Y?: number;
    basisY_Z?: number;
    basisZ_X?: number;
    basisZ_Y?: number;
    basisZ_Z?: number;
    translation_X?: number;
    translation_Y?: number;
    translation_Z?: number;
    handOrientation_X?: number;
    handOrientation_Y?: number;
    handOrientation_Z?: number;
    familyTypeIndex?: number;
    familyType?: IFamilyType;
    hostIndex?: number;
    host?: IElement;
    fromRoomIndex?: number;
    fromRoom?: IRoom;
    toRoomIndex?: number;
    toRoom?: IRoom;
    elementIndex?: number;
    element?: IElement;
}
export interface IFamilyInstanceTable {
    getCount(): Promise<number>;
    get(familyInstanceIndex: number): Promise<IFamilyInstance>;
    getAll(): Promise<IFamilyInstance[]>;
    getFacingFlipped(familyInstanceIndex: number): Promise<boolean | undefined>;
    getAllFacingFlipped(): Promise<boolean[] | undefined>;
    getFacingOrientation_X(familyInstanceIndex: number): Promise<number | undefined>;
    getAllFacingOrientation_X(): Promise<number[] | undefined>;
    getFacingOrientation_Y(familyInstanceIndex: number): Promise<number | undefined>;
    getAllFacingOrientation_Y(): Promise<number[] | undefined>;
    getFacingOrientation_Z(familyInstanceIndex: number): Promise<number | undefined>;
    getAllFacingOrientation_Z(): Promise<number[] | undefined>;
    getHandFlipped(familyInstanceIndex: number): Promise<boolean | undefined>;
    getAllHandFlipped(): Promise<boolean[] | undefined>;
    getMirrored(familyInstanceIndex: number): Promise<boolean | undefined>;
    getAllMirrored(): Promise<boolean[] | undefined>;
    getHasModifiedGeometry(familyInstanceIndex: number): Promise<boolean | undefined>;
    getAllHasModifiedGeometry(): Promise<boolean[] | undefined>;
    getScale(familyInstanceIndex: number): Promise<number | undefined>;
    getAllScale(): Promise<number[] | undefined>;
    getBasisX_X(familyInstanceIndex: number): Promise<number | undefined>;
    getAllBasisX_X(): Promise<number[] | undefined>;
    getBasisX_Y(familyInstanceIndex: number): Promise<number | undefined>;
    getAllBasisX_Y(): Promise<number[] | undefined>;
    getBasisX_Z(familyInstanceIndex: number): Promise<number | undefined>;
    getAllBasisX_Z(): Promise<number[] | undefined>;
    getBasisY_X(familyInstanceIndex: number): Promise<number | undefined>;
    getAllBasisY_X(): Promise<number[] | undefined>;
    getBasisY_Y(familyInstanceIndex: number): Promise<number | undefined>;
    getAllBasisY_Y(): Promise<number[] | undefined>;
    getBasisY_Z(familyInstanceIndex: number): Promise<number | undefined>;
    getAllBasisY_Z(): Promise<number[] | undefined>;
    getBasisZ_X(familyInstanceIndex: number): Promise<number | undefined>;
    getAllBasisZ_X(): Promise<number[] | undefined>;
    getBasisZ_Y(familyInstanceIndex: number): Promise<number | undefined>;
    getAllBasisZ_Y(): Promise<number[] | undefined>;
    getBasisZ_Z(familyInstanceIndex: number): Promise<number | undefined>;
    getAllBasisZ_Z(): Promise<number[] | undefined>;
    getTranslation_X(familyInstanceIndex: number): Promise<number | undefined>;
    getAllTranslation_X(): Promise<number[] | undefined>;
    getTranslation_Y(familyInstanceIndex: number): Promise<number | undefined>;
    getAllTranslation_Y(): Promise<number[] | undefined>;
    getTranslation_Z(familyInstanceIndex: number): Promise<number | undefined>;
    getAllTranslation_Z(): Promise<number[] | undefined>;
    getHandOrientation_X(familyInstanceIndex: number): Promise<number | undefined>;
    getAllHandOrientation_X(): Promise<number[] | undefined>;
    getHandOrientation_Y(familyInstanceIndex: number): Promise<number | undefined>;
    getAllHandOrientation_Y(): Promise<number[] | undefined>;
    getHandOrientation_Z(familyInstanceIndex: number): Promise<number | undefined>;
    getAllHandOrientation_Z(): Promise<number[] | undefined>;
    getFamilyTypeIndex(familyInstanceIndex: number): Promise<number | undefined>;
    getAllFamilyTypeIndex(): Promise<number[] | undefined>;
    getFamilyType(familyInstanceIndex: number): Promise<IFamilyType | undefined>;
    getHostIndex(familyInstanceIndex: number): Promise<number | undefined>;
    getAllHostIndex(): Promise<number[] | undefined>;
    getHost(familyInstanceIndex: number): Promise<IElement | undefined>;
    getFromRoomIndex(familyInstanceIndex: number): Promise<number | undefined>;
    getAllFromRoomIndex(): Promise<number[] | undefined>;
    getFromRoom(familyInstanceIndex: number): Promise<IRoom | undefined>;
    getToRoomIndex(familyInstanceIndex: number): Promise<number | undefined>;
    getAllToRoomIndex(): Promise<number[] | undefined>;
    getToRoom(familyInstanceIndex: number): Promise<IRoom | undefined>;
    getElementIndex(familyInstanceIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(familyInstanceIndex: number): Promise<IElement | undefined>;
}
export declare class FamilyInstance implements IFamilyInstance {
    index: number;
    facingFlipped?: boolean;
    facingOrientation_X?: number;
    facingOrientation_Y?: number;
    facingOrientation_Z?: number;
    handFlipped?: boolean;
    mirrored?: boolean;
    hasModifiedGeometry?: boolean;
    scale?: number;
    basisX_X?: number;
    basisX_Y?: number;
    basisX_Z?: number;
    basisY_X?: number;
    basisY_Y?: number;
    basisY_Z?: number;
    basisZ_X?: number;
    basisZ_Y?: number;
    basisZ_Z?: number;
    translation_X?: number;
    translation_Y?: number;
    translation_Z?: number;
    handOrientation_X?: number;
    handOrientation_Y?: number;
    handOrientation_Z?: number;
    familyTypeIndex?: number;
    familyType?: IFamilyType;
    hostIndex?: number;
    host?: IElement;
    fromRoomIndex?: number;
    fromRoom?: IRoom;
    toRoomIndex?: number;
    toRoom?: IRoom;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IFamilyInstanceTable, index: number): Promise<IFamilyInstance>;
}
export declare class FamilyInstanceTable implements IFamilyInstanceTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IFamilyInstanceTable | undefined>;
    getCount(): Promise<number>;
    get(familyInstanceIndex: number): Promise<IFamilyInstance>;
    getAll(): Promise<IFamilyInstance[]>;
    getFacingFlipped(familyInstanceIndex: number): Promise<boolean | undefined>;
    getAllFacingFlipped(): Promise<boolean[] | undefined>;
    getFacingOrientation_X(familyInstanceIndex: number): Promise<number | undefined>;
    getAllFacingOrientation_X(): Promise<number[] | undefined>;
    getFacingOrientation_Y(familyInstanceIndex: number): Promise<number | undefined>;
    getAllFacingOrientation_Y(): Promise<number[] | undefined>;
    getFacingOrientation_Z(familyInstanceIndex: number): Promise<number | undefined>;
    getAllFacingOrientation_Z(): Promise<number[] | undefined>;
    getHandFlipped(familyInstanceIndex: number): Promise<boolean | undefined>;
    getAllHandFlipped(): Promise<boolean[] | undefined>;
    getMirrored(familyInstanceIndex: number): Promise<boolean | undefined>;
    getAllMirrored(): Promise<boolean[] | undefined>;
    getHasModifiedGeometry(familyInstanceIndex: number): Promise<boolean | undefined>;
    getAllHasModifiedGeometry(): Promise<boolean[] | undefined>;
    getScale(familyInstanceIndex: number): Promise<number | undefined>;
    getAllScale(): Promise<number[] | undefined>;
    getBasisX_X(familyInstanceIndex: number): Promise<number | undefined>;
    getAllBasisX_X(): Promise<number[] | undefined>;
    getBasisX_Y(familyInstanceIndex: number): Promise<number | undefined>;
    getAllBasisX_Y(): Promise<number[] | undefined>;
    getBasisX_Z(familyInstanceIndex: number): Promise<number | undefined>;
    getAllBasisX_Z(): Promise<number[] | undefined>;
    getBasisY_X(familyInstanceIndex: number): Promise<number | undefined>;
    getAllBasisY_X(): Promise<number[] | undefined>;
    getBasisY_Y(familyInstanceIndex: number): Promise<number | undefined>;
    getAllBasisY_Y(): Promise<number[] | undefined>;
    getBasisY_Z(familyInstanceIndex: number): Promise<number | undefined>;
    getAllBasisY_Z(): Promise<number[] | undefined>;
    getBasisZ_X(familyInstanceIndex: number): Promise<number | undefined>;
    getAllBasisZ_X(): Promise<number[] | undefined>;
    getBasisZ_Y(familyInstanceIndex: number): Promise<number | undefined>;
    getAllBasisZ_Y(): Promise<number[] | undefined>;
    getBasisZ_Z(familyInstanceIndex: number): Promise<number | undefined>;
    getAllBasisZ_Z(): Promise<number[] | undefined>;
    getTranslation_X(familyInstanceIndex: number): Promise<number | undefined>;
    getAllTranslation_X(): Promise<number[] | undefined>;
    getTranslation_Y(familyInstanceIndex: number): Promise<number | undefined>;
    getAllTranslation_Y(): Promise<number[] | undefined>;
    getTranslation_Z(familyInstanceIndex: number): Promise<number | undefined>;
    getAllTranslation_Z(): Promise<number[] | undefined>;
    getHandOrientation_X(familyInstanceIndex: number): Promise<number | undefined>;
    getAllHandOrientation_X(): Promise<number[] | undefined>;
    getHandOrientation_Y(familyInstanceIndex: number): Promise<number | undefined>;
    getAllHandOrientation_Y(): Promise<number[] | undefined>;
    getHandOrientation_Z(familyInstanceIndex: number): Promise<number | undefined>;
    getAllHandOrientation_Z(): Promise<number[] | undefined>;
    getFamilyTypeIndex(familyInstanceIndex: number): Promise<number | undefined>;
    getAllFamilyTypeIndex(): Promise<number[] | undefined>;
    getFamilyType(familyInstanceIndex: number): Promise<IFamilyType | undefined>;
    getHostIndex(familyInstanceIndex: number): Promise<number | undefined>;
    getAllHostIndex(): Promise<number[] | undefined>;
    getHost(familyInstanceIndex: number): Promise<IElement | undefined>;
    getFromRoomIndex(familyInstanceIndex: number): Promise<number | undefined>;
    getAllFromRoomIndex(): Promise<number[] | undefined>;
    getFromRoom(familyInstanceIndex: number): Promise<IRoom | undefined>;
    getToRoomIndex(familyInstanceIndex: number): Promise<number | undefined>;
    getAllToRoomIndex(): Promise<number[] | undefined>;
    getToRoom(familyInstanceIndex: number): Promise<IRoom | undefined>;
    getElementIndex(familyInstanceIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(familyInstanceIndex: number): Promise<IElement | undefined>;
}
export interface IView {
    index: number;
    title?: string;
    viewType?: string;
    up_X?: number;
    up_Y?: number;
    up_Z?: number;
    right_X?: number;
    right_Y?: number;
    right_Z?: number;
    origin_X?: number;
    origin_Y?: number;
    origin_Z?: number;
    viewDirection_X?: number;
    viewDirection_Y?: number;
    viewDirection_Z?: number;
    viewPosition_X?: number;
    viewPosition_Y?: number;
    viewPosition_Z?: number;
    scale?: number;
    outline_Min_X?: number;
    outline_Min_Y?: number;
    outline_Max_X?: number;
    outline_Max_Y?: number;
    detailLevel?: number;
    cameraIndex?: number;
    camera?: ICamera;
    familyTypeIndex?: number;
    familyType?: IFamilyType;
    elementIndex?: number;
    element?: IElement;
}
export interface IViewTable {
    getCount(): Promise<number>;
    get(viewIndex: number): Promise<IView>;
    getAll(): Promise<IView[]>;
    getTitle(viewIndex: number): Promise<string | undefined>;
    getAllTitle(): Promise<string[] | undefined>;
    getViewType(viewIndex: number): Promise<string | undefined>;
    getAllViewType(): Promise<string[] | undefined>;
    getUp_X(viewIndex: number): Promise<number | undefined>;
    getAllUp_X(): Promise<number[] | undefined>;
    getUp_Y(viewIndex: number): Promise<number | undefined>;
    getAllUp_Y(): Promise<number[] | undefined>;
    getUp_Z(viewIndex: number): Promise<number | undefined>;
    getAllUp_Z(): Promise<number[] | undefined>;
    getRight_X(viewIndex: number): Promise<number | undefined>;
    getAllRight_X(): Promise<number[] | undefined>;
    getRight_Y(viewIndex: number): Promise<number | undefined>;
    getAllRight_Y(): Promise<number[] | undefined>;
    getRight_Z(viewIndex: number): Promise<number | undefined>;
    getAllRight_Z(): Promise<number[] | undefined>;
    getOrigin_X(viewIndex: number): Promise<number | undefined>;
    getAllOrigin_X(): Promise<number[] | undefined>;
    getOrigin_Y(viewIndex: number): Promise<number | undefined>;
    getAllOrigin_Y(): Promise<number[] | undefined>;
    getOrigin_Z(viewIndex: number): Promise<number | undefined>;
    getAllOrigin_Z(): Promise<number[] | undefined>;
    getViewDirection_X(viewIndex: number): Promise<number | undefined>;
    getAllViewDirection_X(): Promise<number[] | undefined>;
    getViewDirection_Y(viewIndex: number): Promise<number | undefined>;
    getAllViewDirection_Y(): Promise<number[] | undefined>;
    getViewDirection_Z(viewIndex: number): Promise<number | undefined>;
    getAllViewDirection_Z(): Promise<number[] | undefined>;
    getViewPosition_X(viewIndex: number): Promise<number | undefined>;
    getAllViewPosition_X(): Promise<number[] | undefined>;
    getViewPosition_Y(viewIndex: number): Promise<number | undefined>;
    getAllViewPosition_Y(): Promise<number[] | undefined>;
    getViewPosition_Z(viewIndex: number): Promise<number | undefined>;
    getAllViewPosition_Z(): Promise<number[] | undefined>;
    getScale(viewIndex: number): Promise<number | undefined>;
    getAllScale(): Promise<number[] | undefined>;
    getOutline_Min_X(viewIndex: number): Promise<number | undefined>;
    getAllOutline_Min_X(): Promise<number[] | undefined>;
    getOutline_Min_Y(viewIndex: number): Promise<number | undefined>;
    getAllOutline_Min_Y(): Promise<number[] | undefined>;
    getOutline_Max_X(viewIndex: number): Promise<number | undefined>;
    getAllOutline_Max_X(): Promise<number[] | undefined>;
    getOutline_Max_Y(viewIndex: number): Promise<number | undefined>;
    getAllOutline_Max_Y(): Promise<number[] | undefined>;
    getDetailLevel(viewIndex: number): Promise<number | undefined>;
    getAllDetailLevel(): Promise<number[] | undefined>;
    getCameraIndex(viewIndex: number): Promise<number | undefined>;
    getAllCameraIndex(): Promise<number[] | undefined>;
    getCamera(viewIndex: number): Promise<ICamera | undefined>;
    getFamilyTypeIndex(viewIndex: number): Promise<number | undefined>;
    getAllFamilyTypeIndex(): Promise<number[] | undefined>;
    getFamilyType(viewIndex: number): Promise<IFamilyType | undefined>;
    getElementIndex(viewIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(viewIndex: number): Promise<IElement | undefined>;
}
export declare class View implements IView {
    index: number;
    title?: string;
    viewType?: string;
    up_X?: number;
    up_Y?: number;
    up_Z?: number;
    right_X?: number;
    right_Y?: number;
    right_Z?: number;
    origin_X?: number;
    origin_Y?: number;
    origin_Z?: number;
    viewDirection_X?: number;
    viewDirection_Y?: number;
    viewDirection_Z?: number;
    viewPosition_X?: number;
    viewPosition_Y?: number;
    viewPosition_Z?: number;
    scale?: number;
    outline_Min_X?: number;
    outline_Min_Y?: number;
    outline_Max_X?: number;
    outline_Max_Y?: number;
    detailLevel?: number;
    cameraIndex?: number;
    camera?: ICamera;
    familyTypeIndex?: number;
    familyType?: IFamilyType;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IViewTable, index: number): Promise<IView>;
}
export declare class ViewTable implements IViewTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IViewTable | undefined>;
    getCount(): Promise<number>;
    get(viewIndex: number): Promise<IView>;
    getAll(): Promise<IView[]>;
    getTitle(viewIndex: number): Promise<string | undefined>;
    getAllTitle(): Promise<string[] | undefined>;
    getViewType(viewIndex: number): Promise<string | undefined>;
    getAllViewType(): Promise<string[] | undefined>;
    getUp_X(viewIndex: number): Promise<number | undefined>;
    getAllUp_X(): Promise<number[] | undefined>;
    getUp_Y(viewIndex: number): Promise<number | undefined>;
    getAllUp_Y(): Promise<number[] | undefined>;
    getUp_Z(viewIndex: number): Promise<number | undefined>;
    getAllUp_Z(): Promise<number[] | undefined>;
    getRight_X(viewIndex: number): Promise<number | undefined>;
    getAllRight_X(): Promise<number[] | undefined>;
    getRight_Y(viewIndex: number): Promise<number | undefined>;
    getAllRight_Y(): Promise<number[] | undefined>;
    getRight_Z(viewIndex: number): Promise<number | undefined>;
    getAllRight_Z(): Promise<number[] | undefined>;
    getOrigin_X(viewIndex: number): Promise<number | undefined>;
    getAllOrigin_X(): Promise<number[] | undefined>;
    getOrigin_Y(viewIndex: number): Promise<number | undefined>;
    getAllOrigin_Y(): Promise<number[] | undefined>;
    getOrigin_Z(viewIndex: number): Promise<number | undefined>;
    getAllOrigin_Z(): Promise<number[] | undefined>;
    getViewDirection_X(viewIndex: number): Promise<number | undefined>;
    getAllViewDirection_X(): Promise<number[] | undefined>;
    getViewDirection_Y(viewIndex: number): Promise<number | undefined>;
    getAllViewDirection_Y(): Promise<number[] | undefined>;
    getViewDirection_Z(viewIndex: number): Promise<number | undefined>;
    getAllViewDirection_Z(): Promise<number[] | undefined>;
    getViewPosition_X(viewIndex: number): Promise<number | undefined>;
    getAllViewPosition_X(): Promise<number[] | undefined>;
    getViewPosition_Y(viewIndex: number): Promise<number | undefined>;
    getAllViewPosition_Y(): Promise<number[] | undefined>;
    getViewPosition_Z(viewIndex: number): Promise<number | undefined>;
    getAllViewPosition_Z(): Promise<number[] | undefined>;
    getScale(viewIndex: number): Promise<number | undefined>;
    getAllScale(): Promise<number[] | undefined>;
    getOutline_Min_X(viewIndex: number): Promise<number | undefined>;
    getAllOutline_Min_X(): Promise<number[] | undefined>;
    getOutline_Min_Y(viewIndex: number): Promise<number | undefined>;
    getAllOutline_Min_Y(): Promise<number[] | undefined>;
    getOutline_Max_X(viewIndex: number): Promise<number | undefined>;
    getAllOutline_Max_X(): Promise<number[] | undefined>;
    getOutline_Max_Y(viewIndex: number): Promise<number | undefined>;
    getAllOutline_Max_Y(): Promise<number[] | undefined>;
    getDetailLevel(viewIndex: number): Promise<number | undefined>;
    getAllDetailLevel(): Promise<number[] | undefined>;
    getCameraIndex(viewIndex: number): Promise<number | undefined>;
    getAllCameraIndex(): Promise<number[] | undefined>;
    getCamera(viewIndex: number): Promise<ICamera | undefined>;
    getFamilyTypeIndex(viewIndex: number): Promise<number | undefined>;
    getAllFamilyTypeIndex(): Promise<number[] | undefined>;
    getFamilyType(viewIndex: number): Promise<IFamilyType | undefined>;
    getElementIndex(viewIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(viewIndex: number): Promise<IElement | undefined>;
}
export interface IElementInView {
    index: number;
    viewIndex?: number;
    view?: IView;
    elementIndex?: number;
    element?: IElement;
}
export interface IElementInViewTable {
    getCount(): Promise<number>;
    get(elementInViewIndex: number): Promise<IElementInView>;
    getAll(): Promise<IElementInView[]>;
    getViewIndex(elementInViewIndex: number): Promise<number | undefined>;
    getAllViewIndex(): Promise<number[] | undefined>;
    getView(elementInViewIndex: number): Promise<IView | undefined>;
    getElementIndex(elementInViewIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(elementInViewIndex: number): Promise<IElement | undefined>;
}
export declare class ElementInView implements IElementInView {
    index: number;
    viewIndex?: number;
    view?: IView;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IElementInViewTable, index: number): Promise<IElementInView>;
}
export declare class ElementInViewTable implements IElementInViewTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IElementInViewTable | undefined>;
    getCount(): Promise<number>;
    get(elementInViewIndex: number): Promise<IElementInView>;
    getAll(): Promise<IElementInView[]>;
    getViewIndex(elementInViewIndex: number): Promise<number | undefined>;
    getAllViewIndex(): Promise<number[] | undefined>;
    getView(elementInViewIndex: number): Promise<IView | undefined>;
    getElementIndex(elementInViewIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(elementInViewIndex: number): Promise<IElement | undefined>;
}
export interface IShapeInView {
    index: number;
    shapeIndex?: number;
    shape?: IShape;
    viewIndex?: number;
    view?: IView;
}
export interface IShapeInViewTable {
    getCount(): Promise<number>;
    get(shapeInViewIndex: number): Promise<IShapeInView>;
    getAll(): Promise<IShapeInView[]>;
    getShapeIndex(shapeInViewIndex: number): Promise<number | undefined>;
    getAllShapeIndex(): Promise<number[] | undefined>;
    getShape(shapeInViewIndex: number): Promise<IShape | undefined>;
    getViewIndex(shapeInViewIndex: number): Promise<number | undefined>;
    getAllViewIndex(): Promise<number[] | undefined>;
    getView(shapeInViewIndex: number): Promise<IView | undefined>;
}
export declare class ShapeInView implements IShapeInView {
    index: number;
    shapeIndex?: number;
    shape?: IShape;
    viewIndex?: number;
    view?: IView;
    static createFromTable(table: IShapeInViewTable, index: number): Promise<IShapeInView>;
}
export declare class ShapeInViewTable implements IShapeInViewTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IShapeInViewTable | undefined>;
    getCount(): Promise<number>;
    get(shapeInViewIndex: number): Promise<IShapeInView>;
    getAll(): Promise<IShapeInView[]>;
    getShapeIndex(shapeInViewIndex: number): Promise<number | undefined>;
    getAllShapeIndex(): Promise<number[] | undefined>;
    getShape(shapeInViewIndex: number): Promise<IShape | undefined>;
    getViewIndex(shapeInViewIndex: number): Promise<number | undefined>;
    getAllViewIndex(): Promise<number[] | undefined>;
    getView(shapeInViewIndex: number): Promise<IView | undefined>;
}
export interface IAssetInView {
    index: number;
    assetIndex?: number;
    asset?: IAsset;
    viewIndex?: number;
    view?: IView;
}
export interface IAssetInViewTable {
    getCount(): Promise<number>;
    get(assetInViewIndex: number): Promise<IAssetInView>;
    getAll(): Promise<IAssetInView[]>;
    getAssetIndex(assetInViewIndex: number): Promise<number | undefined>;
    getAllAssetIndex(): Promise<number[] | undefined>;
    getAsset(assetInViewIndex: number): Promise<IAsset | undefined>;
    getViewIndex(assetInViewIndex: number): Promise<number | undefined>;
    getAllViewIndex(): Promise<number[] | undefined>;
    getView(assetInViewIndex: number): Promise<IView | undefined>;
}
export declare class AssetInView implements IAssetInView {
    index: number;
    assetIndex?: number;
    asset?: IAsset;
    viewIndex?: number;
    view?: IView;
    static createFromTable(table: IAssetInViewTable, index: number): Promise<IAssetInView>;
}
export declare class AssetInViewTable implements IAssetInViewTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IAssetInViewTable | undefined>;
    getCount(): Promise<number>;
    get(assetInViewIndex: number): Promise<IAssetInView>;
    getAll(): Promise<IAssetInView[]>;
    getAssetIndex(assetInViewIndex: number): Promise<number | undefined>;
    getAllAssetIndex(): Promise<number[] | undefined>;
    getAsset(assetInViewIndex: number): Promise<IAsset | undefined>;
    getViewIndex(assetInViewIndex: number): Promise<number | undefined>;
    getAllViewIndex(): Promise<number[] | undefined>;
    getView(assetInViewIndex: number): Promise<IView | undefined>;
}
export interface IAssetInViewSheet {
    index: number;
    assetIndex?: number;
    asset?: IAsset;
    viewSheetIndex?: number;
    viewSheet?: IViewSheet;
}
export interface IAssetInViewSheetTable {
    getCount(): Promise<number>;
    get(assetInViewSheetIndex: number): Promise<IAssetInViewSheet>;
    getAll(): Promise<IAssetInViewSheet[]>;
    getAssetIndex(assetInViewSheetIndex: number): Promise<number | undefined>;
    getAllAssetIndex(): Promise<number[] | undefined>;
    getAsset(assetInViewSheetIndex: number): Promise<IAsset | undefined>;
    getViewSheetIndex(assetInViewSheetIndex: number): Promise<number | undefined>;
    getAllViewSheetIndex(): Promise<number[] | undefined>;
    getViewSheet(assetInViewSheetIndex: number): Promise<IViewSheet | undefined>;
}
export declare class AssetInViewSheet implements IAssetInViewSheet {
    index: number;
    assetIndex?: number;
    asset?: IAsset;
    viewSheetIndex?: number;
    viewSheet?: IViewSheet;
    static createFromTable(table: IAssetInViewSheetTable, index: number): Promise<IAssetInViewSheet>;
}
export declare class AssetInViewSheetTable implements IAssetInViewSheetTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IAssetInViewSheetTable | undefined>;
    getCount(): Promise<number>;
    get(assetInViewSheetIndex: number): Promise<IAssetInViewSheet>;
    getAll(): Promise<IAssetInViewSheet[]>;
    getAssetIndex(assetInViewSheetIndex: number): Promise<number | undefined>;
    getAllAssetIndex(): Promise<number[] | undefined>;
    getAsset(assetInViewSheetIndex: number): Promise<IAsset | undefined>;
    getViewSheetIndex(assetInViewSheetIndex: number): Promise<number | undefined>;
    getAllViewSheetIndex(): Promise<number[] | undefined>;
    getViewSheet(assetInViewSheetIndex: number): Promise<IViewSheet | undefined>;
}
export interface ILevelInView {
    index: number;
    extents_Min_X?: number;
    extents_Min_Y?: number;
    extents_Min_Z?: number;
    extents_Max_X?: number;
    extents_Max_Y?: number;
    extents_Max_Z?: number;
    levelIndex?: number;
    level?: ILevel;
    viewIndex?: number;
    view?: IView;
}
export interface ILevelInViewTable {
    getCount(): Promise<number>;
    get(levelInViewIndex: number): Promise<ILevelInView>;
    getAll(): Promise<ILevelInView[]>;
    getExtents_Min_X(levelInViewIndex: number): Promise<number | undefined>;
    getAllExtents_Min_X(): Promise<number[] | undefined>;
    getExtents_Min_Y(levelInViewIndex: number): Promise<number | undefined>;
    getAllExtents_Min_Y(): Promise<number[] | undefined>;
    getExtents_Min_Z(levelInViewIndex: number): Promise<number | undefined>;
    getAllExtents_Min_Z(): Promise<number[] | undefined>;
    getExtents_Max_X(levelInViewIndex: number): Promise<number | undefined>;
    getAllExtents_Max_X(): Promise<number[] | undefined>;
    getExtents_Max_Y(levelInViewIndex: number): Promise<number | undefined>;
    getAllExtents_Max_Y(): Promise<number[] | undefined>;
    getExtents_Max_Z(levelInViewIndex: number): Promise<number | undefined>;
    getAllExtents_Max_Z(): Promise<number[] | undefined>;
    getLevelIndex(levelInViewIndex: number): Promise<number | undefined>;
    getAllLevelIndex(): Promise<number[] | undefined>;
    getLevel(levelInViewIndex: number): Promise<ILevel | undefined>;
    getViewIndex(levelInViewIndex: number): Promise<number | undefined>;
    getAllViewIndex(): Promise<number[] | undefined>;
    getView(levelInViewIndex: number): Promise<IView | undefined>;
}
export declare class LevelInView implements ILevelInView {
    index: number;
    extents_Min_X?: number;
    extents_Min_Y?: number;
    extents_Min_Z?: number;
    extents_Max_X?: number;
    extents_Max_Y?: number;
    extents_Max_Z?: number;
    levelIndex?: number;
    level?: ILevel;
    viewIndex?: number;
    view?: IView;
    static createFromTable(table: ILevelInViewTable, index: number): Promise<ILevelInView>;
}
export declare class LevelInViewTable implements ILevelInViewTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<ILevelInViewTable | undefined>;
    getCount(): Promise<number>;
    get(levelInViewIndex: number): Promise<ILevelInView>;
    getAll(): Promise<ILevelInView[]>;
    getExtents_Min_X(levelInViewIndex: number): Promise<number | undefined>;
    getAllExtents_Min_X(): Promise<number[] | undefined>;
    getExtents_Min_Y(levelInViewIndex: number): Promise<number | undefined>;
    getAllExtents_Min_Y(): Promise<number[] | undefined>;
    getExtents_Min_Z(levelInViewIndex: number): Promise<number | undefined>;
    getAllExtents_Min_Z(): Promise<number[] | undefined>;
    getExtents_Max_X(levelInViewIndex: number): Promise<number | undefined>;
    getAllExtents_Max_X(): Promise<number[] | undefined>;
    getExtents_Max_Y(levelInViewIndex: number): Promise<number | undefined>;
    getAllExtents_Max_Y(): Promise<number[] | undefined>;
    getExtents_Max_Z(levelInViewIndex: number): Promise<number | undefined>;
    getAllExtents_Max_Z(): Promise<number[] | undefined>;
    getLevelIndex(levelInViewIndex: number): Promise<number | undefined>;
    getAllLevelIndex(): Promise<number[] | undefined>;
    getLevel(levelInViewIndex: number): Promise<ILevel | undefined>;
    getViewIndex(levelInViewIndex: number): Promise<number | undefined>;
    getAllViewIndex(): Promise<number[] | undefined>;
    getView(levelInViewIndex: number): Promise<IView | undefined>;
}
export interface ICamera {
    index: number;
    id?: number;
    isPerspective?: number;
    verticalExtent?: number;
    horizontalExtent?: number;
    farDistance?: number;
    nearDistance?: number;
    targetDistance?: number;
    rightOffset?: number;
    upOffset?: number;
}
export interface ICameraTable {
    getCount(): Promise<number>;
    get(cameraIndex: number): Promise<ICamera>;
    getAll(): Promise<ICamera[]>;
    getId(cameraIndex: number): Promise<number | undefined>;
    getAllId(): Promise<number[] | undefined>;
    getIsPerspective(cameraIndex: number): Promise<number | undefined>;
    getAllIsPerspective(): Promise<number[] | undefined>;
    getVerticalExtent(cameraIndex: number): Promise<number | undefined>;
    getAllVerticalExtent(): Promise<number[] | undefined>;
    getHorizontalExtent(cameraIndex: number): Promise<number | undefined>;
    getAllHorizontalExtent(): Promise<number[] | undefined>;
    getFarDistance(cameraIndex: number): Promise<number | undefined>;
    getAllFarDistance(): Promise<number[] | undefined>;
    getNearDistance(cameraIndex: number): Promise<number | undefined>;
    getAllNearDistance(): Promise<number[] | undefined>;
    getTargetDistance(cameraIndex: number): Promise<number | undefined>;
    getAllTargetDistance(): Promise<number[] | undefined>;
    getRightOffset(cameraIndex: number): Promise<number | undefined>;
    getAllRightOffset(): Promise<number[] | undefined>;
    getUpOffset(cameraIndex: number): Promise<number | undefined>;
    getAllUpOffset(): Promise<number[] | undefined>;
}
export declare class Camera implements ICamera {
    index: number;
    id?: number;
    isPerspective?: number;
    verticalExtent?: number;
    horizontalExtent?: number;
    farDistance?: number;
    nearDistance?: number;
    targetDistance?: number;
    rightOffset?: number;
    upOffset?: number;
    static createFromTable(table: ICameraTable, index: number): Promise<ICamera>;
}
export declare class CameraTable implements ICameraTable {
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<ICameraTable | undefined>;
    getCount(): Promise<number>;
    get(cameraIndex: number): Promise<ICamera>;
    getAll(): Promise<ICamera[]>;
    getId(cameraIndex: number): Promise<number | undefined>;
    getAllId(): Promise<number[] | undefined>;
    getIsPerspective(cameraIndex: number): Promise<number | undefined>;
    getAllIsPerspective(): Promise<number[] | undefined>;
    getVerticalExtent(cameraIndex: number): Promise<number | undefined>;
    getAllVerticalExtent(): Promise<number[] | undefined>;
    getHorizontalExtent(cameraIndex: number): Promise<number | undefined>;
    getAllHorizontalExtent(): Promise<number[] | undefined>;
    getFarDistance(cameraIndex: number): Promise<number | undefined>;
    getAllFarDistance(): Promise<number[] | undefined>;
    getNearDistance(cameraIndex: number): Promise<number | undefined>;
    getAllNearDistance(): Promise<number[] | undefined>;
    getTargetDistance(cameraIndex: number): Promise<number | undefined>;
    getAllTargetDistance(): Promise<number[] | undefined>;
    getRightOffset(cameraIndex: number): Promise<number | undefined>;
    getAllRightOffset(): Promise<number[] | undefined>;
    getUpOffset(cameraIndex: number): Promise<number | undefined>;
    getAllUpOffset(): Promise<number[] | undefined>;
}
export interface IMaterial {
    index: number;
    name?: string;
    materialCategory?: string;
    color_X?: number;
    color_Y?: number;
    color_Z?: number;
    colorUvScaling_X?: number;
    colorUvScaling_Y?: number;
    colorUvOffset_X?: number;
    colorUvOffset_Y?: number;
    normalUvScaling_X?: number;
    normalUvScaling_Y?: number;
    normalUvOffset_X?: number;
    normalUvOffset_Y?: number;
    normalAmount?: number;
    glossiness?: number;
    smoothness?: number;
    transparency?: number;
    colorTextureFileIndex?: number;
    colorTextureFile?: IAsset;
    normalTextureFileIndex?: number;
    normalTextureFile?: IAsset;
    elementIndex?: number;
    element?: IElement;
}
export interface IMaterialTable {
    getCount(): Promise<number>;
    get(materialIndex: number): Promise<IMaterial>;
    getAll(): Promise<IMaterial[]>;
    getName(materialIndex: number): Promise<string | undefined>;
    getAllName(): Promise<string[] | undefined>;
    getMaterialCategory(materialIndex: number): Promise<string | undefined>;
    getAllMaterialCategory(): Promise<string[] | undefined>;
    getColor_X(materialIndex: number): Promise<number | undefined>;
    getAllColor_X(): Promise<number[] | undefined>;
    getColor_Y(materialIndex: number): Promise<number | undefined>;
    getAllColor_Y(): Promise<number[] | undefined>;
    getColor_Z(materialIndex: number): Promise<number | undefined>;
    getAllColor_Z(): Promise<number[] | undefined>;
    getColorUvScaling_X(materialIndex: number): Promise<number | undefined>;
    getAllColorUvScaling_X(): Promise<number[] | undefined>;
    getColorUvScaling_Y(materialIndex: number): Promise<number | undefined>;
    getAllColorUvScaling_Y(): Promise<number[] | undefined>;
    getColorUvOffset_X(materialIndex: number): Promise<number | undefined>;
    getAllColorUvOffset_X(): Promise<number[] | undefined>;
    getColorUvOffset_Y(materialIndex: number): Promise<number | undefined>;
    getAllColorUvOffset_Y(): Promise<number[] | undefined>;
    getNormalUvScaling_X(materialIndex: number): Promise<number | undefined>;
    getAllNormalUvScaling_X(): Promise<number[] | undefined>;
    getNormalUvScaling_Y(materialIndex: number): Promise<number | undefined>;
    getAllNormalUvScaling_Y(): Promise<number[] | undefined>;
    getNormalUvOffset_X(materialIndex: number): Promise<number | undefined>;
    getAllNormalUvOffset_X(): Promise<number[] | undefined>;
    getNormalUvOffset_Y(materialIndex: number): Promise<number | undefined>;
    getAllNormalUvOffset_Y(): Promise<number[] | undefined>;
    getNormalAmount(materialIndex: number): Promise<number | undefined>;
    getAllNormalAmount(): Promise<number[] | undefined>;
    getGlossiness(materialIndex: number): Promise<number | undefined>;
    getAllGlossiness(): Promise<number[] | undefined>;
    getSmoothness(materialIndex: number): Promise<number | undefined>;
    getAllSmoothness(): Promise<number[] | undefined>;
    getTransparency(materialIndex: number): Promise<number | undefined>;
    getAllTransparency(): Promise<number[] | undefined>;
    getColorTextureFileIndex(materialIndex: number): Promise<number | undefined>;
    getAllColorTextureFileIndex(): Promise<number[] | undefined>;
    getColorTextureFile(materialIndex: number): Promise<IAsset | undefined>;
    getNormalTextureFileIndex(materialIndex: number): Promise<number | undefined>;
    getAllNormalTextureFileIndex(): Promise<number[] | undefined>;
    getNormalTextureFile(materialIndex: number): Promise<IAsset | undefined>;
    getElementIndex(materialIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(materialIndex: number): Promise<IElement | undefined>;
}
export declare class Material implements IMaterial {
    index: number;
    name?: string;
    materialCategory?: string;
    color_X?: number;
    color_Y?: number;
    color_Z?: number;
    colorUvScaling_X?: number;
    colorUvScaling_Y?: number;
    colorUvOffset_X?: number;
    colorUvOffset_Y?: number;
    normalUvScaling_X?: number;
    normalUvScaling_Y?: number;
    normalUvOffset_X?: number;
    normalUvOffset_Y?: number;
    normalAmount?: number;
    glossiness?: number;
    smoothness?: number;
    transparency?: number;
    colorTextureFileIndex?: number;
    colorTextureFile?: IAsset;
    normalTextureFileIndex?: number;
    normalTextureFile?: IAsset;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IMaterialTable, index: number): Promise<IMaterial>;
}
export declare class MaterialTable implements IMaterialTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IMaterialTable | undefined>;
    getCount(): Promise<number>;
    get(materialIndex: number): Promise<IMaterial>;
    getAll(): Promise<IMaterial[]>;
    getName(materialIndex: number): Promise<string | undefined>;
    getAllName(): Promise<string[] | undefined>;
    getMaterialCategory(materialIndex: number): Promise<string | undefined>;
    getAllMaterialCategory(): Promise<string[] | undefined>;
    getColor_X(materialIndex: number): Promise<number | undefined>;
    getAllColor_X(): Promise<number[] | undefined>;
    getColor_Y(materialIndex: number): Promise<number | undefined>;
    getAllColor_Y(): Promise<number[] | undefined>;
    getColor_Z(materialIndex: number): Promise<number | undefined>;
    getAllColor_Z(): Promise<number[] | undefined>;
    getColorUvScaling_X(materialIndex: number): Promise<number | undefined>;
    getAllColorUvScaling_X(): Promise<number[] | undefined>;
    getColorUvScaling_Y(materialIndex: number): Promise<number | undefined>;
    getAllColorUvScaling_Y(): Promise<number[] | undefined>;
    getColorUvOffset_X(materialIndex: number): Promise<number | undefined>;
    getAllColorUvOffset_X(): Promise<number[] | undefined>;
    getColorUvOffset_Y(materialIndex: number): Promise<number | undefined>;
    getAllColorUvOffset_Y(): Promise<number[] | undefined>;
    getNormalUvScaling_X(materialIndex: number): Promise<number | undefined>;
    getAllNormalUvScaling_X(): Promise<number[] | undefined>;
    getNormalUvScaling_Y(materialIndex: number): Promise<number | undefined>;
    getAllNormalUvScaling_Y(): Promise<number[] | undefined>;
    getNormalUvOffset_X(materialIndex: number): Promise<number | undefined>;
    getAllNormalUvOffset_X(): Promise<number[] | undefined>;
    getNormalUvOffset_Y(materialIndex: number): Promise<number | undefined>;
    getAllNormalUvOffset_Y(): Promise<number[] | undefined>;
    getNormalAmount(materialIndex: number): Promise<number | undefined>;
    getAllNormalAmount(): Promise<number[] | undefined>;
    getGlossiness(materialIndex: number): Promise<number | undefined>;
    getAllGlossiness(): Promise<number[] | undefined>;
    getSmoothness(materialIndex: number): Promise<number | undefined>;
    getAllSmoothness(): Promise<number[] | undefined>;
    getTransparency(materialIndex: number): Promise<number | undefined>;
    getAllTransparency(): Promise<number[] | undefined>;
    getColorTextureFileIndex(materialIndex: number): Promise<number | undefined>;
    getAllColorTextureFileIndex(): Promise<number[] | undefined>;
    getColorTextureFile(materialIndex: number): Promise<IAsset | undefined>;
    getNormalTextureFileIndex(materialIndex: number): Promise<number | undefined>;
    getAllNormalTextureFileIndex(): Promise<number[] | undefined>;
    getNormalTextureFile(materialIndex: number): Promise<IAsset | undefined>;
    getElementIndex(materialIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(materialIndex: number): Promise<IElement | undefined>;
}
export interface IMaterialInElement {
    index: number;
    area?: number;
    volume?: number;
    isPaint?: boolean;
    materialIndex?: number;
    material?: IMaterial;
    elementIndex?: number;
    element?: IElement;
}
export interface IMaterialInElementTable {
    getCount(): Promise<number>;
    get(materialInElementIndex: number): Promise<IMaterialInElement>;
    getAll(): Promise<IMaterialInElement[]>;
    getArea(materialInElementIndex: number): Promise<number | undefined>;
    getAllArea(): Promise<number[] | undefined>;
    getVolume(materialInElementIndex: number): Promise<number | undefined>;
    getAllVolume(): Promise<number[] | undefined>;
    getIsPaint(materialInElementIndex: number): Promise<boolean | undefined>;
    getAllIsPaint(): Promise<boolean[] | undefined>;
    getMaterialIndex(materialInElementIndex: number): Promise<number | undefined>;
    getAllMaterialIndex(): Promise<number[] | undefined>;
    getMaterial(materialInElementIndex: number): Promise<IMaterial | undefined>;
    getElementIndex(materialInElementIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(materialInElementIndex: number): Promise<IElement | undefined>;
}
export declare class MaterialInElement implements IMaterialInElement {
    index: number;
    area?: number;
    volume?: number;
    isPaint?: boolean;
    materialIndex?: number;
    material?: IMaterial;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IMaterialInElementTable, index: number): Promise<IMaterialInElement>;
}
export declare class MaterialInElementTable implements IMaterialInElementTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IMaterialInElementTable | undefined>;
    getCount(): Promise<number>;
    get(materialInElementIndex: number): Promise<IMaterialInElement>;
    getAll(): Promise<IMaterialInElement[]>;
    getArea(materialInElementIndex: number): Promise<number | undefined>;
    getAllArea(): Promise<number[] | undefined>;
    getVolume(materialInElementIndex: number): Promise<number | undefined>;
    getAllVolume(): Promise<number[] | undefined>;
    getIsPaint(materialInElementIndex: number): Promise<boolean | undefined>;
    getAllIsPaint(): Promise<boolean[] | undefined>;
    getMaterialIndex(materialInElementIndex: number): Promise<number | undefined>;
    getAllMaterialIndex(): Promise<number[] | undefined>;
    getMaterial(materialInElementIndex: number): Promise<IMaterial | undefined>;
    getElementIndex(materialInElementIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(materialInElementIndex: number): Promise<IElement | undefined>;
}
export interface ICompoundStructureLayer {
    index: number;
    orderIndex?: number;
    width?: number;
    materialFunctionAssignment?: string;
    materialIndex?: number;
    material?: IMaterial;
    compoundStructureIndex?: number;
    compoundStructure?: ICompoundStructure;
}
export interface ICompoundStructureLayerTable {
    getCount(): Promise<number>;
    get(compoundStructureLayerIndex: number): Promise<ICompoundStructureLayer>;
    getAll(): Promise<ICompoundStructureLayer[]>;
    getOrderIndex(compoundStructureLayerIndex: number): Promise<number | undefined>;
    getAllOrderIndex(): Promise<number[] | undefined>;
    getWidth(compoundStructureLayerIndex: number): Promise<number | undefined>;
    getAllWidth(): Promise<number[] | undefined>;
    getMaterialFunctionAssignment(compoundStructureLayerIndex: number): Promise<string | undefined>;
    getAllMaterialFunctionAssignment(): Promise<string[] | undefined>;
    getMaterialIndex(compoundStructureLayerIndex: number): Promise<number | undefined>;
    getAllMaterialIndex(): Promise<number[] | undefined>;
    getMaterial(compoundStructureLayerIndex: number): Promise<IMaterial | undefined>;
    getCompoundStructureIndex(compoundStructureLayerIndex: number): Promise<number | undefined>;
    getAllCompoundStructureIndex(): Promise<number[] | undefined>;
    getCompoundStructure(compoundStructureLayerIndex: number): Promise<ICompoundStructure | undefined>;
}
export declare class CompoundStructureLayer implements ICompoundStructureLayer {
    index: number;
    orderIndex?: number;
    width?: number;
    materialFunctionAssignment?: string;
    materialIndex?: number;
    material?: IMaterial;
    compoundStructureIndex?: number;
    compoundStructure?: ICompoundStructure;
    static createFromTable(table: ICompoundStructureLayerTable, index: number): Promise<ICompoundStructureLayer>;
}
export declare class CompoundStructureLayerTable implements ICompoundStructureLayerTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<ICompoundStructureLayerTable | undefined>;
    getCount(): Promise<number>;
    get(compoundStructureLayerIndex: number): Promise<ICompoundStructureLayer>;
    getAll(): Promise<ICompoundStructureLayer[]>;
    getOrderIndex(compoundStructureLayerIndex: number): Promise<number | undefined>;
    getAllOrderIndex(): Promise<number[] | undefined>;
    getWidth(compoundStructureLayerIndex: number): Promise<number | undefined>;
    getAllWidth(): Promise<number[] | undefined>;
    getMaterialFunctionAssignment(compoundStructureLayerIndex: number): Promise<string | undefined>;
    getAllMaterialFunctionAssignment(): Promise<string[] | undefined>;
    getMaterialIndex(compoundStructureLayerIndex: number): Promise<number | undefined>;
    getAllMaterialIndex(): Promise<number[] | undefined>;
    getMaterial(compoundStructureLayerIndex: number): Promise<IMaterial | undefined>;
    getCompoundStructureIndex(compoundStructureLayerIndex: number): Promise<number | undefined>;
    getAllCompoundStructureIndex(): Promise<number[] | undefined>;
    getCompoundStructure(compoundStructureLayerIndex: number): Promise<ICompoundStructure | undefined>;
}
export interface ICompoundStructure {
    index: number;
    width?: number;
    structuralLayerIndex?: number;
    structuralLayer?: ICompoundStructureLayer;
}
export interface ICompoundStructureTable {
    getCount(): Promise<number>;
    get(compoundStructureIndex: number): Promise<ICompoundStructure>;
    getAll(): Promise<ICompoundStructure[]>;
    getWidth(compoundStructureIndex: number): Promise<number | undefined>;
    getAllWidth(): Promise<number[] | undefined>;
    getStructuralLayerIndex(compoundStructureIndex: number): Promise<number | undefined>;
    getAllStructuralLayerIndex(): Promise<number[] | undefined>;
    getStructuralLayer(compoundStructureIndex: number): Promise<ICompoundStructureLayer | undefined>;
}
export declare class CompoundStructure implements ICompoundStructure {
    index: number;
    width?: number;
    structuralLayerIndex?: number;
    structuralLayer?: ICompoundStructureLayer;
    static createFromTable(table: ICompoundStructureTable, index: number): Promise<ICompoundStructure>;
}
export declare class CompoundStructureTable implements ICompoundStructureTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<ICompoundStructureTable | undefined>;
    getCount(): Promise<number>;
    get(compoundStructureIndex: number): Promise<ICompoundStructure>;
    getAll(): Promise<ICompoundStructure[]>;
    getWidth(compoundStructureIndex: number): Promise<number | undefined>;
    getAllWidth(): Promise<number[] | undefined>;
    getStructuralLayerIndex(compoundStructureIndex: number): Promise<number | undefined>;
    getAllStructuralLayerIndex(): Promise<number[] | undefined>;
    getStructuralLayer(compoundStructureIndex: number): Promise<ICompoundStructureLayer | undefined>;
}
export interface INode {
    index: number;
    elementIndex?: number;
    element?: IElement;
}
export interface INodeTable {
    getCount(): Promise<number>;
    get(nodeIndex: number): Promise<INode>;
    getAll(): Promise<INode[]>;
    getElementIndex(nodeIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(nodeIndex: number): Promise<IElement | undefined>;
}
export declare class Node implements INode {
    index: number;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: INodeTable, index: number): Promise<INode>;
}
export declare class NodeTable implements INodeTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<INodeTable | undefined>;
    getCount(): Promise<number>;
    get(nodeIndex: number): Promise<INode>;
    getAll(): Promise<INode[]>;
    getElementIndex(nodeIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(nodeIndex: number): Promise<IElement | undefined>;
}
export interface IGeometry {
    index: number;
    box_Min_X?: number;
    box_Min_Y?: number;
    box_Min_Z?: number;
    box_Max_X?: number;
    box_Max_Y?: number;
    box_Max_Z?: number;
    vertexCount?: number;
    faceCount?: number;
}
export interface IGeometryTable {
    getCount(): Promise<number>;
    get(geometryIndex: number): Promise<IGeometry>;
    getAll(): Promise<IGeometry[]>;
    getBox_Min_X(geometryIndex: number): Promise<number | undefined>;
    getAllBox_Min_X(): Promise<number[] | undefined>;
    getBox_Min_Y(geometryIndex: number): Promise<number | undefined>;
    getAllBox_Min_Y(): Promise<number[] | undefined>;
    getBox_Min_Z(geometryIndex: number): Promise<number | undefined>;
    getAllBox_Min_Z(): Promise<number[] | undefined>;
    getBox_Max_X(geometryIndex: number): Promise<number | undefined>;
    getAllBox_Max_X(): Promise<number[] | undefined>;
    getBox_Max_Y(geometryIndex: number): Promise<number | undefined>;
    getAllBox_Max_Y(): Promise<number[] | undefined>;
    getBox_Max_Z(geometryIndex: number): Promise<number | undefined>;
    getAllBox_Max_Z(): Promise<number[] | undefined>;
    getVertexCount(geometryIndex: number): Promise<number | undefined>;
    getAllVertexCount(): Promise<number[] | undefined>;
    getFaceCount(geometryIndex: number): Promise<number | undefined>;
    getAllFaceCount(): Promise<number[] | undefined>;
}
export declare class Geometry implements IGeometry {
    index: number;
    box_Min_X?: number;
    box_Min_Y?: number;
    box_Min_Z?: number;
    box_Max_X?: number;
    box_Max_Y?: number;
    box_Max_Z?: number;
    vertexCount?: number;
    faceCount?: number;
    static createFromTable(table: IGeometryTable, index: number): Promise<IGeometry>;
}
export declare class GeometryTable implements IGeometryTable {
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IGeometryTable | undefined>;
    getCount(): Promise<number>;
    get(geometryIndex: number): Promise<IGeometry>;
    getAll(): Promise<IGeometry[]>;
    getBox_Min_X(geometryIndex: number): Promise<number | undefined>;
    getAllBox_Min_X(): Promise<number[] | undefined>;
    getBox_Min_Y(geometryIndex: number): Promise<number | undefined>;
    getAllBox_Min_Y(): Promise<number[] | undefined>;
    getBox_Min_Z(geometryIndex: number): Promise<number | undefined>;
    getAllBox_Min_Z(): Promise<number[] | undefined>;
    getBox_Max_X(geometryIndex: number): Promise<number | undefined>;
    getAllBox_Max_X(): Promise<number[] | undefined>;
    getBox_Max_Y(geometryIndex: number): Promise<number | undefined>;
    getAllBox_Max_Y(): Promise<number[] | undefined>;
    getBox_Max_Z(geometryIndex: number): Promise<number | undefined>;
    getAllBox_Max_Z(): Promise<number[] | undefined>;
    getVertexCount(geometryIndex: number): Promise<number | undefined>;
    getAllVertexCount(): Promise<number[] | undefined>;
    getFaceCount(geometryIndex: number): Promise<number | undefined>;
    getAllFaceCount(): Promise<number[] | undefined>;
}
export interface IShape {
    index: number;
    elementIndex?: number;
    element?: IElement;
}
export interface IShapeTable {
    getCount(): Promise<number>;
    get(shapeIndex: number): Promise<IShape>;
    getAll(): Promise<IShape[]>;
    getElementIndex(shapeIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(shapeIndex: number): Promise<IElement | undefined>;
}
export declare class Shape implements IShape {
    index: number;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IShapeTable, index: number): Promise<IShape>;
}
export declare class ShapeTable implements IShapeTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IShapeTable | undefined>;
    getCount(): Promise<number>;
    get(shapeIndex: number): Promise<IShape>;
    getAll(): Promise<IShape[]>;
    getElementIndex(shapeIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(shapeIndex: number): Promise<IElement | undefined>;
}
export interface IShapeCollection {
    index: number;
    elementIndex?: number;
    element?: IElement;
}
export interface IShapeCollectionTable {
    getCount(): Promise<number>;
    get(shapeCollectionIndex: number): Promise<IShapeCollection>;
    getAll(): Promise<IShapeCollection[]>;
    getElementIndex(shapeCollectionIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(shapeCollectionIndex: number): Promise<IElement | undefined>;
}
export declare class ShapeCollection implements IShapeCollection {
    index: number;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IShapeCollectionTable, index: number): Promise<IShapeCollection>;
}
export declare class ShapeCollectionTable implements IShapeCollectionTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IShapeCollectionTable | undefined>;
    getCount(): Promise<number>;
    get(shapeCollectionIndex: number): Promise<IShapeCollection>;
    getAll(): Promise<IShapeCollection[]>;
    getElementIndex(shapeCollectionIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(shapeCollectionIndex: number): Promise<IElement | undefined>;
}
export interface IShapeInShapeCollection {
    index: number;
    shapeIndex?: number;
    shape?: IShape;
    shapeCollectionIndex?: number;
    shapeCollection?: IShapeCollection;
}
export interface IShapeInShapeCollectionTable {
    getCount(): Promise<number>;
    get(shapeInShapeCollectionIndex: number): Promise<IShapeInShapeCollection>;
    getAll(): Promise<IShapeInShapeCollection[]>;
    getShapeIndex(shapeInShapeCollectionIndex: number): Promise<number | undefined>;
    getAllShapeIndex(): Promise<number[] | undefined>;
    getShape(shapeInShapeCollectionIndex: number): Promise<IShape | undefined>;
    getShapeCollectionIndex(shapeInShapeCollectionIndex: number): Promise<number | undefined>;
    getAllShapeCollectionIndex(): Promise<number[] | undefined>;
    getShapeCollection(shapeInShapeCollectionIndex: number): Promise<IShapeCollection | undefined>;
}
export declare class ShapeInShapeCollection implements IShapeInShapeCollection {
    index: number;
    shapeIndex?: number;
    shape?: IShape;
    shapeCollectionIndex?: number;
    shapeCollection?: IShapeCollection;
    static createFromTable(table: IShapeInShapeCollectionTable, index: number): Promise<IShapeInShapeCollection>;
}
export declare class ShapeInShapeCollectionTable implements IShapeInShapeCollectionTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IShapeInShapeCollectionTable | undefined>;
    getCount(): Promise<number>;
    get(shapeInShapeCollectionIndex: number): Promise<IShapeInShapeCollection>;
    getAll(): Promise<IShapeInShapeCollection[]>;
    getShapeIndex(shapeInShapeCollectionIndex: number): Promise<number | undefined>;
    getAllShapeIndex(): Promise<number[] | undefined>;
    getShape(shapeInShapeCollectionIndex: number): Promise<IShape | undefined>;
    getShapeCollectionIndex(shapeInShapeCollectionIndex: number): Promise<number | undefined>;
    getAllShapeCollectionIndex(): Promise<number[] | undefined>;
    getShapeCollection(shapeInShapeCollectionIndex: number): Promise<IShapeCollection | undefined>;
}
export interface ISystem {
    index: number;
    systemType?: number;
    familyTypeIndex?: number;
    familyType?: IFamilyType;
    elementIndex?: number;
    element?: IElement;
}
export interface ISystemTable {
    getCount(): Promise<number>;
    get(systemIndex: number): Promise<ISystem>;
    getAll(): Promise<ISystem[]>;
    getSystemType(systemIndex: number): Promise<number | undefined>;
    getAllSystemType(): Promise<number[] | undefined>;
    getFamilyTypeIndex(systemIndex: number): Promise<number | undefined>;
    getAllFamilyTypeIndex(): Promise<number[] | undefined>;
    getFamilyType(systemIndex: number): Promise<IFamilyType | undefined>;
    getElementIndex(systemIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(systemIndex: number): Promise<IElement | undefined>;
}
export declare class System implements ISystem {
    index: number;
    systemType?: number;
    familyTypeIndex?: number;
    familyType?: IFamilyType;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: ISystemTable, index: number): Promise<ISystem>;
}
export declare class SystemTable implements ISystemTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<ISystemTable | undefined>;
    getCount(): Promise<number>;
    get(systemIndex: number): Promise<ISystem>;
    getAll(): Promise<ISystem[]>;
    getSystemType(systemIndex: number): Promise<number | undefined>;
    getAllSystemType(): Promise<number[] | undefined>;
    getFamilyTypeIndex(systemIndex: number): Promise<number | undefined>;
    getAllFamilyTypeIndex(): Promise<number[] | undefined>;
    getFamilyType(systemIndex: number): Promise<IFamilyType | undefined>;
    getElementIndex(systemIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(systemIndex: number): Promise<IElement | undefined>;
}
export interface IElementInSystem {
    index: number;
    roles?: number;
    systemIndex?: number;
    system?: ISystem;
    elementIndex?: number;
    element?: IElement;
}
export interface IElementInSystemTable {
    getCount(): Promise<number>;
    get(elementInSystemIndex: number): Promise<IElementInSystem>;
    getAll(): Promise<IElementInSystem[]>;
    getRoles(elementInSystemIndex: number): Promise<number | undefined>;
    getAllRoles(): Promise<number[] | undefined>;
    getSystemIndex(elementInSystemIndex: number): Promise<number | undefined>;
    getAllSystemIndex(): Promise<number[] | undefined>;
    getSystem(elementInSystemIndex: number): Promise<ISystem | undefined>;
    getElementIndex(elementInSystemIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(elementInSystemIndex: number): Promise<IElement | undefined>;
}
export declare class ElementInSystem implements IElementInSystem {
    index: number;
    roles?: number;
    systemIndex?: number;
    system?: ISystem;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IElementInSystemTable, index: number): Promise<IElementInSystem>;
}
export declare class ElementInSystemTable implements IElementInSystemTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IElementInSystemTable | undefined>;
    getCount(): Promise<number>;
    get(elementInSystemIndex: number): Promise<IElementInSystem>;
    getAll(): Promise<IElementInSystem[]>;
    getRoles(elementInSystemIndex: number): Promise<number | undefined>;
    getAllRoles(): Promise<number[] | undefined>;
    getSystemIndex(elementInSystemIndex: number): Promise<number | undefined>;
    getAllSystemIndex(): Promise<number[] | undefined>;
    getSystem(elementInSystemIndex: number): Promise<ISystem | undefined>;
    getElementIndex(elementInSystemIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(elementInSystemIndex: number): Promise<IElement | undefined>;
}
export interface IWarning {
    index: number;
    guid?: string;
    severity?: string;
    description?: string;
    bimDocumentIndex?: number;
    bimDocument?: IBimDocument;
}
export interface IWarningTable {
    getCount(): Promise<number>;
    get(warningIndex: number): Promise<IWarning>;
    getAll(): Promise<IWarning[]>;
    getGuid(warningIndex: number): Promise<string | undefined>;
    getAllGuid(): Promise<string[] | undefined>;
    getSeverity(warningIndex: number): Promise<string | undefined>;
    getAllSeverity(): Promise<string[] | undefined>;
    getDescription(warningIndex: number): Promise<string | undefined>;
    getAllDescription(): Promise<string[] | undefined>;
    getBimDocumentIndex(warningIndex: number): Promise<number | undefined>;
    getAllBimDocumentIndex(): Promise<number[] | undefined>;
    getBimDocument(warningIndex: number): Promise<IBimDocument | undefined>;
}
export declare class Warning implements IWarning {
    index: number;
    guid?: string;
    severity?: string;
    description?: string;
    bimDocumentIndex?: number;
    bimDocument?: IBimDocument;
    static createFromTable(table: IWarningTable, index: number): Promise<IWarning>;
}
export declare class WarningTable implements IWarningTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IWarningTable | undefined>;
    getCount(): Promise<number>;
    get(warningIndex: number): Promise<IWarning>;
    getAll(): Promise<IWarning[]>;
    getGuid(warningIndex: number): Promise<string | undefined>;
    getAllGuid(): Promise<string[] | undefined>;
    getSeverity(warningIndex: number): Promise<string | undefined>;
    getAllSeverity(): Promise<string[] | undefined>;
    getDescription(warningIndex: number): Promise<string | undefined>;
    getAllDescription(): Promise<string[] | undefined>;
    getBimDocumentIndex(warningIndex: number): Promise<number | undefined>;
    getAllBimDocumentIndex(): Promise<number[] | undefined>;
    getBimDocument(warningIndex: number): Promise<IBimDocument | undefined>;
}
export interface IElementInWarning {
    index: number;
    warningIndex?: number;
    warning?: IWarning;
    elementIndex?: number;
    element?: IElement;
}
export interface IElementInWarningTable {
    getCount(): Promise<number>;
    get(elementInWarningIndex: number): Promise<IElementInWarning>;
    getAll(): Promise<IElementInWarning[]>;
    getWarningIndex(elementInWarningIndex: number): Promise<number | undefined>;
    getAllWarningIndex(): Promise<number[] | undefined>;
    getWarning(elementInWarningIndex: number): Promise<IWarning | undefined>;
    getElementIndex(elementInWarningIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(elementInWarningIndex: number): Promise<IElement | undefined>;
}
export declare class ElementInWarning implements IElementInWarning {
    index: number;
    warningIndex?: number;
    warning?: IWarning;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IElementInWarningTable, index: number): Promise<IElementInWarning>;
}
export declare class ElementInWarningTable implements IElementInWarningTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IElementInWarningTable | undefined>;
    getCount(): Promise<number>;
    get(elementInWarningIndex: number): Promise<IElementInWarning>;
    getAll(): Promise<IElementInWarning[]>;
    getWarningIndex(elementInWarningIndex: number): Promise<number | undefined>;
    getAllWarningIndex(): Promise<number[] | undefined>;
    getWarning(elementInWarningIndex: number): Promise<IWarning | undefined>;
    getElementIndex(elementInWarningIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(elementInWarningIndex: number): Promise<IElement | undefined>;
}
export interface IBasePoint {
    index: number;
    isSurveyPoint?: boolean;
    position_X?: number;
    position_Y?: number;
    position_Z?: number;
    sharedPosition_X?: number;
    sharedPosition_Y?: number;
    sharedPosition_Z?: number;
    elementIndex?: number;
    element?: IElement;
}
export interface IBasePointTable {
    getCount(): Promise<number>;
    get(basePointIndex: number): Promise<IBasePoint>;
    getAll(): Promise<IBasePoint[]>;
    getIsSurveyPoint(basePointIndex: number): Promise<boolean | undefined>;
    getAllIsSurveyPoint(): Promise<boolean[] | undefined>;
    getPosition_X(basePointIndex: number): Promise<number | undefined>;
    getAllPosition_X(): Promise<number[] | undefined>;
    getPosition_Y(basePointIndex: number): Promise<number | undefined>;
    getAllPosition_Y(): Promise<number[] | undefined>;
    getPosition_Z(basePointIndex: number): Promise<number | undefined>;
    getAllPosition_Z(): Promise<number[] | undefined>;
    getSharedPosition_X(basePointIndex: number): Promise<number | undefined>;
    getAllSharedPosition_X(): Promise<number[] | undefined>;
    getSharedPosition_Y(basePointIndex: number): Promise<number | undefined>;
    getAllSharedPosition_Y(): Promise<number[] | undefined>;
    getSharedPosition_Z(basePointIndex: number): Promise<number | undefined>;
    getAllSharedPosition_Z(): Promise<number[] | undefined>;
    getElementIndex(basePointIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(basePointIndex: number): Promise<IElement | undefined>;
}
export declare class BasePoint implements IBasePoint {
    index: number;
    isSurveyPoint?: boolean;
    position_X?: number;
    position_Y?: number;
    position_Z?: number;
    sharedPosition_X?: number;
    sharedPosition_Y?: number;
    sharedPosition_Z?: number;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IBasePointTable, index: number): Promise<IBasePoint>;
}
export declare class BasePointTable implements IBasePointTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IBasePointTable | undefined>;
    getCount(): Promise<number>;
    get(basePointIndex: number): Promise<IBasePoint>;
    getAll(): Promise<IBasePoint[]>;
    getIsSurveyPoint(basePointIndex: number): Promise<boolean | undefined>;
    getAllIsSurveyPoint(): Promise<boolean[] | undefined>;
    getPosition_X(basePointIndex: number): Promise<number | undefined>;
    getAllPosition_X(): Promise<number[] | undefined>;
    getPosition_Y(basePointIndex: number): Promise<number | undefined>;
    getAllPosition_Y(): Promise<number[] | undefined>;
    getPosition_Z(basePointIndex: number): Promise<number | undefined>;
    getAllPosition_Z(): Promise<number[] | undefined>;
    getSharedPosition_X(basePointIndex: number): Promise<number | undefined>;
    getAllSharedPosition_X(): Promise<number[] | undefined>;
    getSharedPosition_Y(basePointIndex: number): Promise<number | undefined>;
    getAllSharedPosition_Y(): Promise<number[] | undefined>;
    getSharedPosition_Z(basePointIndex: number): Promise<number | undefined>;
    getAllSharedPosition_Z(): Promise<number[] | undefined>;
    getElementIndex(basePointIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(basePointIndex: number): Promise<IElement | undefined>;
}
export interface IPhaseFilter {
    index: number;
    _new?: number;
    existing?: number;
    demolished?: number;
    temporary?: number;
    elementIndex?: number;
    element?: IElement;
}
export interface IPhaseFilterTable {
    getCount(): Promise<number>;
    get(phaseFilterIndex: number): Promise<IPhaseFilter>;
    getAll(): Promise<IPhaseFilter[]>;
    getNew(phaseFilterIndex: number): Promise<number | undefined>;
    getAllNew(): Promise<number[] | undefined>;
    getExisting(phaseFilterIndex: number): Promise<number | undefined>;
    getAllExisting(): Promise<number[] | undefined>;
    getDemolished(phaseFilterIndex: number): Promise<number | undefined>;
    getAllDemolished(): Promise<number[] | undefined>;
    getTemporary(phaseFilterIndex: number): Promise<number | undefined>;
    getAllTemporary(): Promise<number[] | undefined>;
    getElementIndex(phaseFilterIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(phaseFilterIndex: number): Promise<IElement | undefined>;
}
export declare class PhaseFilter implements IPhaseFilter {
    index: number;
    _new?: number;
    existing?: number;
    demolished?: number;
    temporary?: number;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IPhaseFilterTable, index: number): Promise<IPhaseFilter>;
}
export declare class PhaseFilterTable implements IPhaseFilterTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IPhaseFilterTable | undefined>;
    getCount(): Promise<number>;
    get(phaseFilterIndex: number): Promise<IPhaseFilter>;
    getAll(): Promise<IPhaseFilter[]>;
    getNew(phaseFilterIndex: number): Promise<number | undefined>;
    getAllNew(): Promise<number[] | undefined>;
    getExisting(phaseFilterIndex: number): Promise<number | undefined>;
    getAllExisting(): Promise<number[] | undefined>;
    getDemolished(phaseFilterIndex: number): Promise<number | undefined>;
    getAllDemolished(): Promise<number[] | undefined>;
    getTemporary(phaseFilterIndex: number): Promise<number | undefined>;
    getAllTemporary(): Promise<number[] | undefined>;
    getElementIndex(phaseFilterIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(phaseFilterIndex: number): Promise<IElement | undefined>;
}
export interface IGrid {
    index: number;
    startPoint_X?: number;
    startPoint_Y?: number;
    startPoint_Z?: number;
    endPoint_X?: number;
    endPoint_Y?: number;
    endPoint_Z?: number;
    isCurved?: boolean;
    extents_Min_X?: number;
    extents_Min_Y?: number;
    extents_Min_Z?: number;
    extents_Max_X?: number;
    extents_Max_Y?: number;
    extents_Max_Z?: number;
    familyTypeIndex?: number;
    familyType?: IFamilyType;
    elementIndex?: number;
    element?: IElement;
}
export interface IGridTable {
    getCount(): Promise<number>;
    get(gridIndex: number): Promise<IGrid>;
    getAll(): Promise<IGrid[]>;
    getStartPoint_X(gridIndex: number): Promise<number | undefined>;
    getAllStartPoint_X(): Promise<number[] | undefined>;
    getStartPoint_Y(gridIndex: number): Promise<number | undefined>;
    getAllStartPoint_Y(): Promise<number[] | undefined>;
    getStartPoint_Z(gridIndex: number): Promise<number | undefined>;
    getAllStartPoint_Z(): Promise<number[] | undefined>;
    getEndPoint_X(gridIndex: number): Promise<number | undefined>;
    getAllEndPoint_X(): Promise<number[] | undefined>;
    getEndPoint_Y(gridIndex: number): Promise<number | undefined>;
    getAllEndPoint_Y(): Promise<number[] | undefined>;
    getEndPoint_Z(gridIndex: number): Promise<number | undefined>;
    getAllEndPoint_Z(): Promise<number[] | undefined>;
    getIsCurved(gridIndex: number): Promise<boolean | undefined>;
    getAllIsCurved(): Promise<boolean[] | undefined>;
    getExtents_Min_X(gridIndex: number): Promise<number | undefined>;
    getAllExtents_Min_X(): Promise<number[] | undefined>;
    getExtents_Min_Y(gridIndex: number): Promise<number | undefined>;
    getAllExtents_Min_Y(): Promise<number[] | undefined>;
    getExtents_Min_Z(gridIndex: number): Promise<number | undefined>;
    getAllExtents_Min_Z(): Promise<number[] | undefined>;
    getExtents_Max_X(gridIndex: number): Promise<number | undefined>;
    getAllExtents_Max_X(): Promise<number[] | undefined>;
    getExtents_Max_Y(gridIndex: number): Promise<number | undefined>;
    getAllExtents_Max_Y(): Promise<number[] | undefined>;
    getExtents_Max_Z(gridIndex: number): Promise<number | undefined>;
    getAllExtents_Max_Z(): Promise<number[] | undefined>;
    getFamilyTypeIndex(gridIndex: number): Promise<number | undefined>;
    getAllFamilyTypeIndex(): Promise<number[] | undefined>;
    getFamilyType(gridIndex: number): Promise<IFamilyType | undefined>;
    getElementIndex(gridIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(gridIndex: number): Promise<IElement | undefined>;
}
export declare class Grid implements IGrid {
    index: number;
    startPoint_X?: number;
    startPoint_Y?: number;
    startPoint_Z?: number;
    endPoint_X?: number;
    endPoint_Y?: number;
    endPoint_Z?: number;
    isCurved?: boolean;
    extents_Min_X?: number;
    extents_Min_Y?: number;
    extents_Min_Z?: number;
    extents_Max_X?: number;
    extents_Max_Y?: number;
    extents_Max_Z?: number;
    familyTypeIndex?: number;
    familyType?: IFamilyType;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IGridTable, index: number): Promise<IGrid>;
}
export declare class GridTable implements IGridTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IGridTable | undefined>;
    getCount(): Promise<number>;
    get(gridIndex: number): Promise<IGrid>;
    getAll(): Promise<IGrid[]>;
    getStartPoint_X(gridIndex: number): Promise<number | undefined>;
    getAllStartPoint_X(): Promise<number[] | undefined>;
    getStartPoint_Y(gridIndex: number): Promise<number | undefined>;
    getAllStartPoint_Y(): Promise<number[] | undefined>;
    getStartPoint_Z(gridIndex: number): Promise<number | undefined>;
    getAllStartPoint_Z(): Promise<number[] | undefined>;
    getEndPoint_X(gridIndex: number): Promise<number | undefined>;
    getAllEndPoint_X(): Promise<number[] | undefined>;
    getEndPoint_Y(gridIndex: number): Promise<number | undefined>;
    getAllEndPoint_Y(): Promise<number[] | undefined>;
    getEndPoint_Z(gridIndex: number): Promise<number | undefined>;
    getAllEndPoint_Z(): Promise<number[] | undefined>;
    getIsCurved(gridIndex: number): Promise<boolean | undefined>;
    getAllIsCurved(): Promise<boolean[] | undefined>;
    getExtents_Min_X(gridIndex: number): Promise<number | undefined>;
    getAllExtents_Min_X(): Promise<number[] | undefined>;
    getExtents_Min_Y(gridIndex: number): Promise<number | undefined>;
    getAllExtents_Min_Y(): Promise<number[] | undefined>;
    getExtents_Min_Z(gridIndex: number): Promise<number | undefined>;
    getAllExtents_Min_Z(): Promise<number[] | undefined>;
    getExtents_Max_X(gridIndex: number): Promise<number | undefined>;
    getAllExtents_Max_X(): Promise<number[] | undefined>;
    getExtents_Max_Y(gridIndex: number): Promise<number | undefined>;
    getAllExtents_Max_Y(): Promise<number[] | undefined>;
    getExtents_Max_Z(gridIndex: number): Promise<number | undefined>;
    getAllExtents_Max_Z(): Promise<number[] | undefined>;
    getFamilyTypeIndex(gridIndex: number): Promise<number | undefined>;
    getAllFamilyTypeIndex(): Promise<number[] | undefined>;
    getFamilyType(gridIndex: number): Promise<IFamilyType | undefined>;
    getElementIndex(gridIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(gridIndex: number): Promise<IElement | undefined>;
}
export interface IArea {
    index: number;
    value?: number;
    perimeter?: number;
    number?: string;
    isGrossInterior?: boolean;
    areaSchemeIndex?: number;
    areaScheme?: IAreaScheme;
    elementIndex?: number;
    element?: IElement;
}
export interface IAreaTable {
    getCount(): Promise<number>;
    get(areaIndex: number): Promise<IArea>;
    getAll(): Promise<IArea[]>;
    getValue(areaIndex: number): Promise<number | undefined>;
    getAllValue(): Promise<number[] | undefined>;
    getPerimeter(areaIndex: number): Promise<number | undefined>;
    getAllPerimeter(): Promise<number[] | undefined>;
    getNumber(areaIndex: number): Promise<string | undefined>;
    getAllNumber(): Promise<string[] | undefined>;
    getIsGrossInterior(areaIndex: number): Promise<boolean | undefined>;
    getAllIsGrossInterior(): Promise<boolean[] | undefined>;
    getAreaSchemeIndex(areaIndex: number): Promise<number | undefined>;
    getAllAreaSchemeIndex(): Promise<number[] | undefined>;
    getAreaScheme(areaIndex: number): Promise<IAreaScheme | undefined>;
    getElementIndex(areaIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(areaIndex: number): Promise<IElement | undefined>;
}
export declare class Area implements IArea {
    index: number;
    value?: number;
    perimeter?: number;
    number?: string;
    isGrossInterior?: boolean;
    areaSchemeIndex?: number;
    areaScheme?: IAreaScheme;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IAreaTable, index: number): Promise<IArea>;
}
export declare class AreaTable implements IAreaTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IAreaTable | undefined>;
    getCount(): Promise<number>;
    get(areaIndex: number): Promise<IArea>;
    getAll(): Promise<IArea[]>;
    getValue(areaIndex: number): Promise<number | undefined>;
    getAllValue(): Promise<number[] | undefined>;
    getPerimeter(areaIndex: number): Promise<number | undefined>;
    getAllPerimeter(): Promise<number[] | undefined>;
    getNumber(areaIndex: number): Promise<string | undefined>;
    getAllNumber(): Promise<string[] | undefined>;
    getIsGrossInterior(areaIndex: number): Promise<boolean | undefined>;
    getAllIsGrossInterior(): Promise<boolean[] | undefined>;
    getAreaSchemeIndex(areaIndex: number): Promise<number | undefined>;
    getAllAreaSchemeIndex(): Promise<number[] | undefined>;
    getAreaScheme(areaIndex: number): Promise<IAreaScheme | undefined>;
    getElementIndex(areaIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(areaIndex: number): Promise<IElement | undefined>;
}
export interface IAreaScheme {
    index: number;
    isGrossBuildingArea?: boolean;
    elementIndex?: number;
    element?: IElement;
}
export interface IAreaSchemeTable {
    getCount(): Promise<number>;
    get(areaSchemeIndex: number): Promise<IAreaScheme>;
    getAll(): Promise<IAreaScheme[]>;
    getIsGrossBuildingArea(areaSchemeIndex: number): Promise<boolean | undefined>;
    getAllIsGrossBuildingArea(): Promise<boolean[] | undefined>;
    getElementIndex(areaSchemeIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(areaSchemeIndex: number): Promise<IElement | undefined>;
}
export declare class AreaScheme implements IAreaScheme {
    index: number;
    isGrossBuildingArea?: boolean;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IAreaSchemeTable, index: number): Promise<IAreaScheme>;
}
export declare class AreaSchemeTable implements IAreaSchemeTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IAreaSchemeTable | undefined>;
    getCount(): Promise<number>;
    get(areaSchemeIndex: number): Promise<IAreaScheme>;
    getAll(): Promise<IAreaScheme[]>;
    getIsGrossBuildingArea(areaSchemeIndex: number): Promise<boolean | undefined>;
    getAllIsGrossBuildingArea(): Promise<boolean[] | undefined>;
    getElementIndex(areaSchemeIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(areaSchemeIndex: number): Promise<IElement | undefined>;
}
export interface ISchedule {
    index: number;
    elementIndex?: number;
    element?: IElement;
}
export interface IScheduleTable {
    getCount(): Promise<number>;
    get(scheduleIndex: number): Promise<ISchedule>;
    getAll(): Promise<ISchedule[]>;
    getElementIndex(scheduleIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(scheduleIndex: number): Promise<IElement | undefined>;
}
export declare class Schedule implements ISchedule {
    index: number;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IScheduleTable, index: number): Promise<ISchedule>;
}
export declare class ScheduleTable implements IScheduleTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IScheduleTable | undefined>;
    getCount(): Promise<number>;
    get(scheduleIndex: number): Promise<ISchedule>;
    getAll(): Promise<ISchedule[]>;
    getElementIndex(scheduleIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(scheduleIndex: number): Promise<IElement | undefined>;
}
export interface IScheduleColumn {
    index: number;
    name?: string;
    columnIndex?: number;
    scheduleIndex?: number;
    schedule?: ISchedule;
}
export interface IScheduleColumnTable {
    getCount(): Promise<number>;
    get(scheduleColumnIndex: number): Promise<IScheduleColumn>;
    getAll(): Promise<IScheduleColumn[]>;
    getName(scheduleColumnIndex: number): Promise<string | undefined>;
    getAllName(): Promise<string[] | undefined>;
    getColumnIndex(scheduleColumnIndex: number): Promise<number | undefined>;
    getAllColumnIndex(): Promise<number[] | undefined>;
    getScheduleIndex(scheduleColumnIndex: number): Promise<number | undefined>;
    getAllScheduleIndex(): Promise<number[] | undefined>;
    getSchedule(scheduleColumnIndex: number): Promise<ISchedule | undefined>;
}
export declare class ScheduleColumn implements IScheduleColumn {
    index: number;
    name?: string;
    columnIndex?: number;
    scheduleIndex?: number;
    schedule?: ISchedule;
    static createFromTable(table: IScheduleColumnTable, index: number): Promise<IScheduleColumn>;
}
export declare class ScheduleColumnTable implements IScheduleColumnTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IScheduleColumnTable | undefined>;
    getCount(): Promise<number>;
    get(scheduleColumnIndex: number): Promise<IScheduleColumn>;
    getAll(): Promise<IScheduleColumn[]>;
    getName(scheduleColumnIndex: number): Promise<string | undefined>;
    getAllName(): Promise<string[] | undefined>;
    getColumnIndex(scheduleColumnIndex: number): Promise<number | undefined>;
    getAllColumnIndex(): Promise<number[] | undefined>;
    getScheduleIndex(scheduleColumnIndex: number): Promise<number | undefined>;
    getAllScheduleIndex(): Promise<number[] | undefined>;
    getSchedule(scheduleColumnIndex: number): Promise<ISchedule | undefined>;
}
export interface IScheduleCell {
    index: number;
    value?: string;
    rowIndex?: number;
    scheduleColumnIndex?: number;
    scheduleColumn?: IScheduleColumn;
}
export interface IScheduleCellTable {
    getCount(): Promise<number>;
    get(scheduleCellIndex: number): Promise<IScheduleCell>;
    getAll(): Promise<IScheduleCell[]>;
    getValue(scheduleCellIndex: number): Promise<string | undefined>;
    getAllValue(): Promise<string[] | undefined>;
    getRowIndex(scheduleCellIndex: number): Promise<number | undefined>;
    getAllRowIndex(): Promise<number[] | undefined>;
    getScheduleColumnIndex(scheduleCellIndex: number): Promise<number | undefined>;
    getAllScheduleColumnIndex(): Promise<number[] | undefined>;
    getScheduleColumn(scheduleCellIndex: number): Promise<IScheduleColumn | undefined>;
}
export declare class ScheduleCell implements IScheduleCell {
    index: number;
    value?: string;
    rowIndex?: number;
    scheduleColumnIndex?: number;
    scheduleColumn?: IScheduleColumn;
    static createFromTable(table: IScheduleCellTable, index: number): Promise<IScheduleCell>;
}
export declare class ScheduleCellTable implements IScheduleCellTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IScheduleCellTable | undefined>;
    getCount(): Promise<number>;
    get(scheduleCellIndex: number): Promise<IScheduleCell>;
    getAll(): Promise<IScheduleCell[]>;
    getValue(scheduleCellIndex: number): Promise<string | undefined>;
    getAllValue(): Promise<string[] | undefined>;
    getRowIndex(scheduleCellIndex: number): Promise<number | undefined>;
    getAllRowIndex(): Promise<number[] | undefined>;
    getScheduleColumnIndex(scheduleCellIndex: number): Promise<number | undefined>;
    getAllScheduleColumnIndex(): Promise<number[] | undefined>;
    getScheduleColumn(scheduleCellIndex: number): Promise<IScheduleColumn | undefined>;
}
export interface IViewSheetSet {
    index: number;
    elementIndex?: number;
    element?: IElement;
}
export interface IViewSheetSetTable {
    getCount(): Promise<number>;
    get(viewSheetSetIndex: number): Promise<IViewSheetSet>;
    getAll(): Promise<IViewSheetSet[]>;
    getElementIndex(viewSheetSetIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(viewSheetSetIndex: number): Promise<IElement | undefined>;
}
export declare class ViewSheetSet implements IViewSheetSet {
    index: number;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IViewSheetSetTable, index: number): Promise<IViewSheetSet>;
}
export declare class ViewSheetSetTable implements IViewSheetSetTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IViewSheetSetTable | undefined>;
    getCount(): Promise<number>;
    get(viewSheetSetIndex: number): Promise<IViewSheetSet>;
    getAll(): Promise<IViewSheetSet[]>;
    getElementIndex(viewSheetSetIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(viewSheetSetIndex: number): Promise<IElement | undefined>;
}
export interface IViewSheet {
    index: number;
    familyTypeIndex?: number;
    familyType?: IFamilyType;
    elementIndex?: number;
    element?: IElement;
}
export interface IViewSheetTable {
    getCount(): Promise<number>;
    get(viewSheetIndex: number): Promise<IViewSheet>;
    getAll(): Promise<IViewSheet[]>;
    getFamilyTypeIndex(viewSheetIndex: number): Promise<number | undefined>;
    getAllFamilyTypeIndex(): Promise<number[] | undefined>;
    getFamilyType(viewSheetIndex: number): Promise<IFamilyType | undefined>;
    getElementIndex(viewSheetIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(viewSheetIndex: number): Promise<IElement | undefined>;
}
export declare class ViewSheet implements IViewSheet {
    index: number;
    familyTypeIndex?: number;
    familyType?: IFamilyType;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IViewSheetTable, index: number): Promise<IViewSheet>;
}
export declare class ViewSheetTable implements IViewSheetTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IViewSheetTable | undefined>;
    getCount(): Promise<number>;
    get(viewSheetIndex: number): Promise<IViewSheet>;
    getAll(): Promise<IViewSheet[]>;
    getFamilyTypeIndex(viewSheetIndex: number): Promise<number | undefined>;
    getAllFamilyTypeIndex(): Promise<number[] | undefined>;
    getFamilyType(viewSheetIndex: number): Promise<IFamilyType | undefined>;
    getElementIndex(viewSheetIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(viewSheetIndex: number): Promise<IElement | undefined>;
}
export interface IViewSheetInViewSheetSet {
    index: number;
    viewSheetIndex?: number;
    viewSheet?: IViewSheet;
    viewSheetSetIndex?: number;
    viewSheetSet?: IViewSheetSet;
}
export interface IViewSheetInViewSheetSetTable {
    getCount(): Promise<number>;
    get(viewSheetInViewSheetSetIndex: number): Promise<IViewSheetInViewSheetSet>;
    getAll(): Promise<IViewSheetInViewSheetSet[]>;
    getViewSheetIndex(viewSheetInViewSheetSetIndex: number): Promise<number | undefined>;
    getAllViewSheetIndex(): Promise<number[] | undefined>;
    getViewSheet(viewSheetInViewSheetSetIndex: number): Promise<IViewSheet | undefined>;
    getViewSheetSetIndex(viewSheetInViewSheetSetIndex: number): Promise<number | undefined>;
    getAllViewSheetSetIndex(): Promise<number[] | undefined>;
    getViewSheetSet(viewSheetInViewSheetSetIndex: number): Promise<IViewSheetSet | undefined>;
}
export declare class ViewSheetInViewSheetSet implements IViewSheetInViewSheetSet {
    index: number;
    viewSheetIndex?: number;
    viewSheet?: IViewSheet;
    viewSheetSetIndex?: number;
    viewSheetSet?: IViewSheetSet;
    static createFromTable(table: IViewSheetInViewSheetSetTable, index: number): Promise<IViewSheetInViewSheetSet>;
}
export declare class ViewSheetInViewSheetSetTable implements IViewSheetInViewSheetSetTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IViewSheetInViewSheetSetTable | undefined>;
    getCount(): Promise<number>;
    get(viewSheetInViewSheetSetIndex: number): Promise<IViewSheetInViewSheetSet>;
    getAll(): Promise<IViewSheetInViewSheetSet[]>;
    getViewSheetIndex(viewSheetInViewSheetSetIndex: number): Promise<number | undefined>;
    getAllViewSheetIndex(): Promise<number[] | undefined>;
    getViewSheet(viewSheetInViewSheetSetIndex: number): Promise<IViewSheet | undefined>;
    getViewSheetSetIndex(viewSheetInViewSheetSetIndex: number): Promise<number | undefined>;
    getAllViewSheetSetIndex(): Promise<number[] | undefined>;
    getViewSheetSet(viewSheetInViewSheetSetIndex: number): Promise<IViewSheetSet | undefined>;
}
export interface IViewInViewSheetSet {
    index: number;
    viewIndex?: number;
    view?: IView;
    viewSheetSetIndex?: number;
    viewSheetSet?: IViewSheetSet;
}
export interface IViewInViewSheetSetTable {
    getCount(): Promise<number>;
    get(viewInViewSheetSetIndex: number): Promise<IViewInViewSheetSet>;
    getAll(): Promise<IViewInViewSheetSet[]>;
    getViewIndex(viewInViewSheetSetIndex: number): Promise<number | undefined>;
    getAllViewIndex(): Promise<number[] | undefined>;
    getView(viewInViewSheetSetIndex: number): Promise<IView | undefined>;
    getViewSheetSetIndex(viewInViewSheetSetIndex: number): Promise<number | undefined>;
    getAllViewSheetSetIndex(): Promise<number[] | undefined>;
    getViewSheetSet(viewInViewSheetSetIndex: number): Promise<IViewSheetSet | undefined>;
}
export declare class ViewInViewSheetSet implements IViewInViewSheetSet {
    index: number;
    viewIndex?: number;
    view?: IView;
    viewSheetSetIndex?: number;
    viewSheetSet?: IViewSheetSet;
    static createFromTable(table: IViewInViewSheetSetTable, index: number): Promise<IViewInViewSheetSet>;
}
export declare class ViewInViewSheetSetTable implements IViewInViewSheetSetTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IViewInViewSheetSetTable | undefined>;
    getCount(): Promise<number>;
    get(viewInViewSheetSetIndex: number): Promise<IViewInViewSheetSet>;
    getAll(): Promise<IViewInViewSheetSet[]>;
    getViewIndex(viewInViewSheetSetIndex: number): Promise<number | undefined>;
    getAllViewIndex(): Promise<number[] | undefined>;
    getView(viewInViewSheetSetIndex: number): Promise<IView | undefined>;
    getViewSheetSetIndex(viewInViewSheetSetIndex: number): Promise<number | undefined>;
    getAllViewSheetSetIndex(): Promise<number[] | undefined>;
    getViewSheetSet(viewInViewSheetSetIndex: number): Promise<IViewSheetSet | undefined>;
}
export interface IViewInViewSheet {
    index: number;
    viewIndex?: number;
    view?: IView;
    viewSheetIndex?: number;
    viewSheet?: IViewSheet;
}
export interface IViewInViewSheetTable {
    getCount(): Promise<number>;
    get(viewInViewSheetIndex: number): Promise<IViewInViewSheet>;
    getAll(): Promise<IViewInViewSheet[]>;
    getViewIndex(viewInViewSheetIndex: number): Promise<number | undefined>;
    getAllViewIndex(): Promise<number[] | undefined>;
    getView(viewInViewSheetIndex: number): Promise<IView | undefined>;
    getViewSheetIndex(viewInViewSheetIndex: number): Promise<number | undefined>;
    getAllViewSheetIndex(): Promise<number[] | undefined>;
    getViewSheet(viewInViewSheetIndex: number): Promise<IViewSheet | undefined>;
}
export declare class ViewInViewSheet implements IViewInViewSheet {
    index: number;
    viewIndex?: number;
    view?: IView;
    viewSheetIndex?: number;
    viewSheet?: IViewSheet;
    static createFromTable(table: IViewInViewSheetTable, index: number): Promise<IViewInViewSheet>;
}
export declare class ViewInViewSheetTable implements IViewInViewSheetTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IViewInViewSheetTable | undefined>;
    getCount(): Promise<number>;
    get(viewInViewSheetIndex: number): Promise<IViewInViewSheet>;
    getAll(): Promise<IViewInViewSheet[]>;
    getViewIndex(viewInViewSheetIndex: number): Promise<number | undefined>;
    getAllViewIndex(): Promise<number[] | undefined>;
    getView(viewInViewSheetIndex: number): Promise<IView | undefined>;
    getViewSheetIndex(viewInViewSheetIndex: number): Promise<number | undefined>;
    getAllViewSheetIndex(): Promise<number[] | undefined>;
    getViewSheet(viewInViewSheetIndex: number): Promise<IViewSheet | undefined>;
}
export interface ISite {
    index: number;
    latitude?: number;
    longitude?: number;
    address?: string;
    elevation?: number;
    number?: string;
    elementIndex?: number;
    element?: IElement;
}
export interface ISiteTable {
    getCount(): Promise<number>;
    get(siteIndex: number): Promise<ISite>;
    getAll(): Promise<ISite[]>;
    getLatitude(siteIndex: number): Promise<number | undefined>;
    getAllLatitude(): Promise<number[] | undefined>;
    getLongitude(siteIndex: number): Promise<number | undefined>;
    getAllLongitude(): Promise<number[] | undefined>;
    getAddress(siteIndex: number): Promise<string | undefined>;
    getAllAddress(): Promise<string[] | undefined>;
    getElevation(siteIndex: number): Promise<number | undefined>;
    getAllElevation(): Promise<number[] | undefined>;
    getNumber(siteIndex: number): Promise<string | undefined>;
    getAllNumber(): Promise<string[] | undefined>;
    getElementIndex(siteIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(siteIndex: number): Promise<IElement | undefined>;
}
export declare class Site implements ISite {
    index: number;
    latitude?: number;
    longitude?: number;
    address?: string;
    elevation?: number;
    number?: string;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: ISiteTable, index: number): Promise<ISite>;
}
export declare class SiteTable implements ISiteTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<ISiteTable | undefined>;
    getCount(): Promise<number>;
    get(siteIndex: number): Promise<ISite>;
    getAll(): Promise<ISite[]>;
    getLatitude(siteIndex: number): Promise<number | undefined>;
    getAllLatitude(): Promise<number[] | undefined>;
    getLongitude(siteIndex: number): Promise<number | undefined>;
    getAllLongitude(): Promise<number[] | undefined>;
    getAddress(siteIndex: number): Promise<string | undefined>;
    getAllAddress(): Promise<string[] | undefined>;
    getElevation(siteIndex: number): Promise<number | undefined>;
    getAllElevation(): Promise<number[] | undefined>;
    getNumber(siteIndex: number): Promise<string | undefined>;
    getAllNumber(): Promise<string[] | undefined>;
    getElementIndex(siteIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(siteIndex: number): Promise<IElement | undefined>;
}
export interface IBuilding {
    index: number;
    elevation?: number;
    terrainElevation?: number;
    address?: string;
    siteIndex?: number;
    site?: ISite;
    elementIndex?: number;
    element?: IElement;
}
export interface IBuildingTable {
    getCount(): Promise<number>;
    get(buildingIndex: number): Promise<IBuilding>;
    getAll(): Promise<IBuilding[]>;
    getElevation(buildingIndex: number): Promise<number | undefined>;
    getAllElevation(): Promise<number[] | undefined>;
    getTerrainElevation(buildingIndex: number): Promise<number | undefined>;
    getAllTerrainElevation(): Promise<number[] | undefined>;
    getAddress(buildingIndex: number): Promise<string | undefined>;
    getAllAddress(): Promise<string[] | undefined>;
    getSiteIndex(buildingIndex: number): Promise<number | undefined>;
    getAllSiteIndex(): Promise<number[] | undefined>;
    getSite(buildingIndex: number): Promise<ISite | undefined>;
    getElementIndex(buildingIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(buildingIndex: number): Promise<IElement | undefined>;
}
export declare class Building implements IBuilding {
    index: number;
    elevation?: number;
    terrainElevation?: number;
    address?: string;
    siteIndex?: number;
    site?: ISite;
    elementIndex?: number;
    element?: IElement;
    static createFromTable(table: IBuildingTable, index: number): Promise<IBuilding>;
}
export declare class BuildingTable implements IBuildingTable {
    private document;
    private entityTable;
    static createFromDocument(document: VimDocument): Promise<IBuildingTable | undefined>;
    getCount(): Promise<number>;
    get(buildingIndex: number): Promise<IBuilding>;
    getAll(): Promise<IBuilding[]>;
    getElevation(buildingIndex: number): Promise<number | undefined>;
    getAllElevation(): Promise<number[] | undefined>;
    getTerrainElevation(buildingIndex: number): Promise<number | undefined>;
    getAllTerrainElevation(): Promise<number[] | undefined>;
    getAddress(buildingIndex: number): Promise<string | undefined>;
    getAllAddress(): Promise<string[] | undefined>;
    getSiteIndex(buildingIndex: number): Promise<number | undefined>;
    getAllSiteIndex(): Promise<number[] | undefined>;
    getSite(buildingIndex: number): Promise<ISite | undefined>;
    getElementIndex(buildingIndex: number): Promise<number | undefined>;
    getAllElementIndex(): Promise<number[] | undefined>;
    getElement(buildingIndex: number): Promise<IElement | undefined>;
}
export declare class VimDocument {
    asset: IAssetTable | undefined;
    displayUnit: IDisplayUnitTable | undefined;
    parameterDescriptor: IParameterDescriptorTable | undefined;
    parameter: IParameterTable | undefined;
    element: IElementTable | undefined;
    workset: IWorksetTable | undefined;
    assemblyInstance: IAssemblyInstanceTable | undefined;
    group: IGroupTable | undefined;
    designOption: IDesignOptionTable | undefined;
    level: ILevelTable | undefined;
    phase: IPhaseTable | undefined;
    room: IRoomTable | undefined;
    bimDocument: IBimDocumentTable | undefined;
    displayUnitInBimDocument: IDisplayUnitInBimDocumentTable | undefined;
    phaseOrderInBimDocument: IPhaseOrderInBimDocumentTable | undefined;
    category: ICategoryTable | undefined;
    family: IFamilyTable | undefined;
    familyType: IFamilyTypeTable | undefined;
    familyInstance: IFamilyInstanceTable | undefined;
    view: IViewTable | undefined;
    elementInView: IElementInViewTable | undefined;
    shapeInView: IShapeInViewTable | undefined;
    assetInView: IAssetInViewTable | undefined;
    assetInViewSheet: IAssetInViewSheetTable | undefined;
    levelInView: ILevelInViewTable | undefined;
    camera: ICameraTable | undefined;
    material: IMaterialTable | undefined;
    materialInElement: IMaterialInElementTable | undefined;
    compoundStructureLayer: ICompoundStructureLayerTable | undefined;
    compoundStructure: ICompoundStructureTable | undefined;
    node: INodeTable | undefined;
    geometry: IGeometryTable | undefined;
    shape: IShapeTable | undefined;
    shapeCollection: IShapeCollectionTable | undefined;
    shapeInShapeCollection: IShapeInShapeCollectionTable | undefined;
    system: ISystemTable | undefined;
    elementInSystem: IElementInSystemTable | undefined;
    warning: IWarningTable | undefined;
    elementInWarning: IElementInWarningTable | undefined;
    basePoint: IBasePointTable | undefined;
    phaseFilter: IPhaseFilterTable | undefined;
    grid: IGridTable | undefined;
    area: IAreaTable | undefined;
    areaScheme: IAreaSchemeTable | undefined;
    schedule: IScheduleTable | undefined;
    scheduleColumn: IScheduleColumnTable | undefined;
    scheduleCell: IScheduleCellTable | undefined;
    viewSheetSet: IViewSheetSetTable | undefined;
    viewSheet: IViewSheetTable | undefined;
    viewSheetInViewSheetSet: IViewSheetInViewSheetSetTable | undefined;
    viewInViewSheetSet: IViewInViewSheetSetTable | undefined;
    viewInViewSheet: IViewInViewSheetTable | undefined;
    site: ISiteTable | undefined;
    building: IBuildingTable | undefined;
    entities: BFast;
    strings: string[] | undefined;
    private constructor();
    static createFromBfast(bfast: BFast, download: boolean, ignoreStrings?: boolean): Promise<VimDocument | undefined>;
}
