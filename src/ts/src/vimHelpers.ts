/**
 * @module vim-ts
 */

import { VimDocument } from "./objectModel"

export class VimHelpers {
  /**
   * Returns all parameters of an element and of its family type and family
   * @param element element index
   * @returns An array of paramters with name, value, group
   */
  static async getElementParameters (document: VimDocument, element: number) {
    const result: ElementParameter[] = []
    
    await Promise.all([
      this.getElementsParameters(document, [element], true).then(p => p?.forEach(i => result.push(i))),
      this.getAllFamilyParameters(document, element).then(p => p?.forEach(i => result.push(i)))
    ])

    return result
  }

  private static async getAllFamilyParameters (document: VimDocument, element: number) {
    const familyInstance = await this.getElementFamilyInstance(document, element)

    const familyType = familyInstance
      ? await document.familyInstance?.getFamilyTypeIndex(familyInstance)
      : undefined

    let family: number | undefined
    let familyTypeElement: number | undefined

    if (familyType)
      await Promise.all([
        document.familyType?.getFamilyIndex(familyType).then((f) => family = f),
        document.familyType?.getElementIndex(familyType).then((fte) => familyTypeElement = fte)
      ])

    const familyElement = family
      ? await document.family?.getElementIndex(family)
      : undefined

    const elements: number[] = []

    if (familyTypeElement)
      elements.push(familyTypeElement)

    if (familyElement)
      elements.push(familyElement)

    return await this.getElementsParameters(document, elements, false)
  }

  private static async getElementsParameters (document: VimDocument, elements: number[], isInstance: boolean) {
    const set = new Set(elements)

    const getParameterDisplayValue = async (index: number) => {
      const value = (await document.parameter?.getValue(index))
        ?.split('|')
        .filter((s) => s.length > 0)
      const displayValue = value?.[value.length - 1] ?? value?.[0]
      return displayValue
    }

    const getParameterName = async (descriptor: number | undefined) => {
      if (descriptor === undefined) return
      return await document.parameterDescriptor?.getName(descriptor)
    }

    const getParameterGroup = async (descriptor: number | undefined) => {
      if (!descriptor) return
      return await document.parameterDescriptor?.getGroup(descriptor)
    }

    const elementIndices = await document.parameter?.getAllElementIndex()

    if (!elementIndices)
      return undefined

    const result: ElementParameter[] = []

    await Promise.all(elementIndices.map(async (e, i) => {
      if (!set.has(e))
        return

      let name: string | undefined
      let value: string | undefined
      let group: string | undefined

      await Promise.all([
        getParameterName(e).then((n) => name = n),
        getParameterDisplayValue(i).then((v) => value = v),
        getParameterGroup(e).then((g) => group = g)
      ])

      result.push({ name, value, group, isInstance })
    }))

    return result
  }

  private static async getElementFamilyInstance (document: VimDocument, element: number) {
    if (!document.familyInstance)
      return undefined

    const elementIndices = await document.familyInstance.getAllElementIndex()

    if (!elementIndices)
      return undefined

    let result: number | undefined

    await Promise.all(elementIndices.map(async (e, i) => {
      if (e === element) {
        result = i
      }
    }))

    return result
  }
}

/**
 * Representation of ElementParamter entity from the entity model
 * See https://github.com/vimaec/vim/blob/master/ObjectModel/object-model-schema.json
 */
export type ElementParameter = {
  name: string | undefined
  value: string | undefined
  group: string | undefined
  isInstance: boolean
}