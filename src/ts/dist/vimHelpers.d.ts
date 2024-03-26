import { VimDocument } from "./objectModel";
/**
 * Representation of ElementParameter entity from the entity model
 * See https://github.com/vimaec/vim/blob/master/ObjectModel/object-model-schema.json
 */
export declare type ElementParameter = {
    name: string | undefined;
    value: string | undefined;
    group: string | undefined;
    isInstance: boolean;
};
/**
 * Returns all parameters of an element and of its family type and family
 * @param element element index
 * @returns An array of paramters with name, value, group
 */
export declare function getElementParameters(document: VimDocument, element: number): Promise<ElementParameter[]>;
export declare function getFamilyElements(document: VimDocument, element: number): Promise<any[] | [number, number]>;
export declare function getElementsParameters(document: VimDocument, elements: Map<number, boolean>): Promise<ElementParameter[]>;
