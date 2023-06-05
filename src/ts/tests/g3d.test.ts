import { g3dAreEqual, getFilterTestFile, instanceAreEqual, loadG3d } from "./helpers"
import * as fs from 'fs';


describe('G3d', () =>{
  
  test('g3d.append', async () =>{
    const g3d = await loadG3d()

    for(let i = 0; i < g3d.getInstanceCount(); i++ ){
      const slice = g3d.slice(i)
      const merge = slice.append(slice)
      instanceAreEqual(merge, 0, g3d, i)
      instanceAreEqual(merge, 1, g3d, i)
    }
  })
  
  test('g3d.equals (all)', async () =>{
    const g3d = await loadG3d()
    expect(g3dAreEqual(g3d, g3d)).toBeTruthy()
  })

  test('g3d.filter (all)', async () => {
    const g3d = await loadG3d()

    const instances = g3d.instanceMeshes.map((_,i) => i)
    const filter = g3d.filter([...instances])
    expect(g3dAreEqual(filter, g3d)).toBeTruthy()
  })

 

  test('G3d.filter (some)', async () =>{
    const g3d = await loadG3d()
    const instances = [0, 1, 4000, 8059]
    for(let i=0; i < instances.length; i++){
      const instance = instances[i]
      const path = getFilterTestFile(instance)
      const data = await fs.promises.readFile(path)
      const expected = data.toString()

      const filter = g3d.filter([instance])
      const value = JSON.stringify(filter)

      expect(value).toEqual(expected)
    }
  })

  /*
  // Run this to regenerate test cases.
  test('G3d.toJson', async () =>{
    const g3d = await loadG3d()
    const instances = [0, 1, 4000, 8059]
    for(let i=0; i < instances.length; i++){
      const instance = instances[i]
      const filter = g3d.filter([instance])
      const str = JSON.stringify(filter)
      await fs.promises.writeFile(getFilterTestFile(instance), str);
    }
  })
  */
})


