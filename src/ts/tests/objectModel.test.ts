import { VimDocument, IElement } from '../src/objectModel'
import { BFast } from '../src/bfast'
import { loadFile } from './helpers'

class TestCase {
    vimFilePath: string
    expectedElementCount: number
    firstTenElementIds: BigInt64Array

    private _arrayBuffer: ArrayBuffer | undefined = undefined

    constructor(vimFilePath: string, expectedElementCount: number, firstTenElementIds: BigInt64Array)
    {
        this.vimFilePath = vimFilePath
        this.expectedElementCount = expectedElementCount
        this.firstTenElementIds = firstTenElementIds
    }

    async loadFile() : Promise<ArrayBuffer> {
        if (this._arrayBuffer === undefined) {
            this._arrayBuffer = await loadFile(this.vimFilePath)
        }
        return this._arrayBuffer!
    }
}

const testCases: TestCase[] = [
    new TestCase(`${__dirname}/../../../data/Wolford_Residence.r2023.om_v4.4.0.vim`, 4464, new BigInt64Array([-1n, 1222722n, 32440n, 118390n, 174750n, 18438n, 355500n, 185913n, 9946n, 182664n])),
    new TestCase(`${__dirname}/../../../data/Wolford_Residence.r2023.om_v5.0.0.vim`, 4473, new BigInt64Array([-1n, 1222722n, 75912n, -1n, 32440n, 118390n, 22793n, 22794n, 22795n, 22796n]))
]

async function getElementById(doc: VimDocument, id: bigint): Promise<IElement>
{
    const elementIndex = (await doc.element!.getAllId())?.indexOf(id)
    const element = await doc.element!.get(elementIndex!)
    return element
}

describe('testing VIM loading file', () => {
    test.each(testCases)('loading VIM file', async (testCase) => {
        const arrayBuffer = await testCase.loadFile()

        const bfast = new BFast({buffer: arrayBuffer})
        const doc = await VimDocument.createFromBfast(bfast, false)

        expect(doc).not.toBe(undefined)
        expect(doc!.element).not.toBe(undefined);
    })
})

describe('testing objectModel.ts file', () => {
    test.each(testCases)('getting one element', async (testCase) => {
        const arrayBuffer = await testCase.loadFile()

        const bfast = new BFast({buffer: arrayBuffer})
        const doc = await VimDocument.createFromBfast(bfast, false)

        expect(doc).not.toBe(undefined)
        expect(doc!.element).not.toBe(undefined)
        expect(await doc!.element!.getCount()).toBe(testCase.expectedElementCount)
        
        const element = await getElementById(doc!, 374011n)

        expect(element).toMatchObject({
            id: 374011n,
            name: 'GWB on Mtl. Stud',
            uniqueId: '3ae43fb5-6797-479b-ac14-3436f35a7178-0005b4fb',
            familyName: 'Compound Ceiling',
            isPinned: false,
            levelIndex: 6,
            phaseCreatedIndex: 1,
            phaseDemolishedIndex: -1,
            categoryIndex: 5,
            worksetIndex: 0,
            designOptionIndex: -1,
            ownerViewIndex: -1,
            groupIndex: -1,
            assemblyInstanceIndex: -1,
            bimDocumentIndex: 0,
            roomIndex: -1,
            location_X: 0.0,
            location_Y: 0.0,
            location_Z: 0.0
        })
    })
})

describe('testing objectModel.ts array getter', () => {
    test.each(testCases)('getting an array of IDs', async (testCase) => {
        const arrayBuffer = await testCase.loadFile()

        const bfast = new BFast({buffer: arrayBuffer})
        const doc = await VimDocument.createFromBfast(bfast, false)
        const ids = await doc?.element?.getAllId()

        expect(doc).not.toBe(undefined)
        expect(doc!.element).not.toBe(undefined);
        expect(ids).not.toBe(undefined)
        expect(ids!.length).toBe(testCase.expectedElementCount)
        expect(ids!.slice(0, 10)).toEqual(testCase.firstTenElementIds)
    })
})

describe('testing objectModel.ts get-all getter', () => {
    test.each(testCases)('getting all levels', async (testCase) => {
        const arrayBuffer = await testCase.loadFile()

        const bfast = new BFast({buffer: arrayBuffer})
        const doc = await VimDocument.createFromBfast(bfast, false)
        const levels = await doc?.level?.getAll()

        expect(levels).not.toBe(undefined)
        expect(levels!.length).toBe(12)
    })
})

describe('testing objectModel.ts ignoreStrings flag', () => {
    test.each(testCases)('getting an element from a document without strings', async (testCase) => {
        const arrayBuffer = await testCase.loadFile()

        const bfast = new BFast({buffer: arrayBuffer})
        const docWithStrings = await VimDocument.createFromBfast(bfast, false)
        const docWithoutStrings = await VimDocument.createFromBfast(bfast, true)

        const elementWithStrings = await getElementById(docWithStrings!, 374011n)
        const elementWithoutStrings = await getElementById(docWithoutStrings!, 374011n)

        expect(docWithStrings).not.toBeUndefined()
        expect(docWithoutStrings).not.toBeUndefined()
        expect(elementWithStrings).not.toBeUndefined()
        expect(elementWithoutStrings).not.toBeUndefined()

        expect(elementWithStrings!.name).toBe("GWB on Mtl. Stud")
        expect(elementWithStrings!.familyName).toBe("Compound Ceiling")

        expect(elementWithoutStrings!.name).toBeUndefined()
        expect(elementWithoutStrings!.familyName).toBeUndefined()

        expect(elementWithStrings!.id).toBe(elementWithoutStrings!.id)
    })
})
