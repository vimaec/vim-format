import { VimDocument } from '../src/objectModel'
import { BFast } from '../src/bfast'
import { loadFile, testVimFilePath } from './helpers'
import * as VimHelpers from '../src/vimHelpers'
import * as fs from 'fs';

const testFilePath = `${__dirname}/../tests/parameters_119.txt`

describe('testing vimHelpers.ts getElementParameters', () => {
  test('getting element parameters', async () => {
    const arrayBuffer = await loadFile(testVimFilePath)

    const bfast = new BFast({buffer: arrayBuffer})
    const doc = await VimDocument.createFromBfast(bfast, false)
    const parameters = await VimHelpers.getElementParameters(doc!, 119)

    //fs.writeFileSync(testFilePath, JSON.stringify(parameters));
    
    const rawData = fs.readFileSync(testFilePath);
    const data = JSON.parse(rawData.toString());
    expect(parameters).toEqual(data)
    
  })
})