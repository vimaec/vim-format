import { VimDocument } from '../src/objectModel'
import { BFast } from '../src/bfast'
import { loadFile } from './helpers'
import { VimHelpers } from '../src/vimHelpers'

const vimFilePath = `${__dirname}/../data/Wolford_Residence.r2023.vim`

describe('testing vimHelpers.ts getElementParameters', () => {
    test('getting element parameters', async () => {
        const arrayBuffer = await loadFile(vimFilePath)

        const bfast = new BFast((arrayBuffer as ArrayBuffer)!)
        const doc = await VimDocument.createFromBfast(bfast)
        const parameters = await VimHelpers.getElementParameters(doc!, 1)

        expect(parameters).toEqual([
            {
                name: 'View Name',
                value: '-1',
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: '3D VIM Viz',
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: 'Independent',
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: undefined,
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: ` 1/8" = 1'-0" `,
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: '96',
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: 'Medium',
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: 'Show Both',
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: 'No',
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: 'No',
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: 'No',
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: undefined,
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: undefined,
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: 'No',
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: `1000' - 0"`,
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: 'Show All',
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: 'New Construction',
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: 'None',
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: 'Architectural',
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: 'By Discipline',
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: 'None',
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: undefined,
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: 'No',
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: undefined,
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: 'No',
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: 'Orthographic',
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: `55' - 5 31/32"`,
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: `10' - 11 1/16"`,
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: 'Adjusting',
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: '-1',
                group: 'Identity Data',
                isInstance: true
            },
            {
                name: 'View Name',
                value: 'No',
                group: 'Identity Data',
                isInstance: true
            }
        ])
    })
})