"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.getElementsParameters = exports.getFamilyElements = exports.getElementParameters = void 0;
/**
 * Returns all parameters of an element and of its family type and family
 * @param element element index
 * @returns An array of paramters with name, value, group
 */
async function getElementParameters(document, element) {
    const elements = new Map();
    elements.set(element, true);
    const familyElements = await getFamilyElements(document, element);
    familyElements.forEach(element => elements.set(element, false));
    return getElementsParameters(document, elements);
}
exports.getElementParameters = getElementParameters;
async function getFamilyElements(document, element) {
    const familyInstance = await getElementFamilyInstance(document, element);
    const familyType = Number.isInteger(familyInstance)
        ? await document.familyInstance.getFamilyTypeIndex(familyInstance)
        : undefined;
    const getFamilyElement = async (familyType) => {
        const family = await document.familyType.getFamilyIndex(familyType);
        return Number.isInteger(family)
            ? await document.family.getElementIndex(family)
            : undefined;
    };
    return Number.isInteger(familyType)
        ? await Promise.all([
            getFamilyElement(familyType),
            document.familyType.getElementIndex(familyType)
        ])
        : [undefined, undefined];
}
exports.getFamilyElements = getFamilyElements;
async function getElementsParameters(document, elements) {
    const [parameterElements, parameterValues, getParameterDescriptorIndices, parameterDescriptorNames, parameterDescriptorGroups] = await Promise.all([
        document.parameter.getAllElementIndex(),
        document.parameter.getAllValue(),
        document.parameter.getAllParameterDescriptorIndex(),
        document.parameterDescriptor.getAllName(),
        document.parameterDescriptor.getAllGroup()
    ]);
    if (!parameterElements)
        return undefined;
    if (!parameterValues)
        return undefined;
    if (!getParameterDescriptorIndices)
        return undefined;
    const getParameterDisplayValue = (index) => {
        const value = parameterValues[index];
        const split = value.indexOf('|');
        if (split >= 0) {
            return value.substring(split + 1, value.length);
        }
        else
            return value;
    };
    const parameters = new Array();
    parameterElements.forEach((e, i) => {
        if (elements.has(e)) {
            parameters.push([i, elements.get(e)]);
        }
    });
    return parameters.map(([parameter, isInstance]) => {
        const descriptor = getParameterDescriptorIndices[parameter];
        const value = getParameterDisplayValue(parameter);
        const name = Number.isInteger(descriptor)
            ? parameterDescriptorNames?.[descriptor]
            : undefined;
        const group = Number.isInteger(descriptor)
            ? parameterDescriptorGroups?.[descriptor]
            : undefined;
        return { name, value, group, isInstance };
    });
}
exports.getElementsParameters = getElementsParameters;
async function getElementFamilyInstance(document, element) {
    const familyInstanceElement = await document.familyInstance.getAllElementIndex();
    const result = familyInstanceElement.findIndex(e => e === element);
    return result < 0 ? undefined : result;
}
