import { g3dAreEqual, expectG3dAreSame, loadAbstract, loadBoth, loadG3d, loadRemote, getFilterTestFile } from './helpers'
import { VimAttributes } from '../src/g3d'
import * as fs from 'fs';

describe('RemoteG3d', () => {
  
  test('RemoteG3d.getVertexCount', async () => {
      const [g3d, remoteG3d] = await loadBoth()

      const value = await remoteG3d.getVertexCount()
      const expected = g3d.getVertexCount()

      expect(value).toBe(expected)
  })

  test('RemoteG3d.getMeshCount', async () => {
    const [g3d, remoteG3d] = await loadBoth()

    const value = await remoteG3d.getMeshCount()
    const expected = g3d.getMeshCount()

    expect(value).toBe(expected)
  })

  test('RemoteG3d.getSubmeshCount', async () => {
    const [g3d, remoteG3d] = await loadBoth()

    const value = await remoteG3d.getSubmeshCount()
    const expected = g3d.getSubmeshCount()
    expect(value).toBe(expected)
  })

  test('RemoteG3d.meshSubmeshes', async () => {
    
    const [g3d, remoteG3d] = await loadBoth()
    const meshCount = g3d.getMeshCount()
    
    for (let m=0; m < meshCount; m++){
      const value = await remoteG3d.meshSubmeshes.getNumber(m)
      const expected = g3d.meshSubmeshes[m]
      expect(value).toBe(expected)
    }
  })

  test('RemoteG3d.getMeshSubmeshStart', async () => {
    
    const [g3d, remoteG3d] = await loadBoth()
    const meshCount = g3d.getMeshCount()
    
    for (let m=0; m < meshCount; m++){
      const value = await remoteG3d.getMeshSubmeshStart(m)
      const expected = g3d.getMeshSubmeshStart(m)
      expect(value).toBe(expected)
    }
  })

  test('RemoteG3d.getMeshSubmeshEnd', async () => {
    
    const [g3d, remoteG3d] = await loadBoth()
    const meshCount = g3d.getMeshCount()
    
    for (let m=0; m < meshCount; m++){
      const value = await remoteG3d.getMeshSubmeshEnd(m)
      const expected = g3d.getMeshSubmeshEnd(m)
      expect(value).toBe(expected)
    }
  })

  test('RemoteG3d.getMeshIndexStart', async () => {
    
    const [g3d, remoteG3d] = await loadBoth()
    const meshCount = g3d.getMeshCount()
    
    for (let m=0; m < meshCount; m++){
      const value = await remoteG3d.getMeshIndexStart(m)
      const expected = g3d.getMeshIndexStart(m)
      expect(value).toBe(expected)
    }
  })

  test('RemoteG3d.getMeshIndexEnd', async () => {
    
    const [g3d, remoteG3d] = await loadBoth()
    const meshCount = g3d.getMeshCount()
    for (let m=0; m < meshCount; m++){
      const value = await remoteG3d.getMeshIndexEnd(m)
      const expected = g3d.getMeshIndexEnd(m)
      expect(value).toBe(expected)
    }
  })

  test('RemoteG3d.getMeshIndexCount', async () => {
    
    const [g3d, remoteG3d] = await loadBoth()
    const meshCount = g3d.getMeshCount()

    for(let m=0; m < meshCount; m++ ){
      const value = await remoteG3d.getMeshIndexCount(m)
      const expected = g3d.getMeshIndexCount(m)
      expect(value).toBe(expected)
    }
  })

  test('RemoteG3d.getMeshIndices', async () => {
    
    const remote = await loadRemote()
    const abstract = await loadAbstract()
    const g3d = await loadG3d()
    const meshCount = g3d.getMeshCount()

    const compareIndices = async function(mesh: number){
      const start = g3d.getMeshIndexStart(mesh)
      const end = g3d.getMeshIndexEnd(mesh)

      // Compare with original values before reordering.
      const value = await remote.getMeshIndices(mesh)
      const attribute = abstract.findAttribute(VimAttributes.indices)
      const original = new Uint32Array(attribute?.data?.buffer!)
      const expected = original.slice(start, end)
      
      expect(value).toEqual(expected)
    }

    for(let m=0; m < meshCount; m++ ){
      await compareIndices(m)
    }
  })

  test('RemoteG3d.getMeshSubmeshCount', async () => {
    
    const [g3d, remoteG3d] = await loadBoth()
    const meshCount = g3d.getMeshCount()
    for(let m=0; m < meshCount; m++){
      const value = await remoteG3d.getMeshSubmeshCount(m)
      const expected = await g3d.getMeshSubmeshCount(m)
      expect(value).toBe(expected)
    }
  })

  test('RemoteG3d.getSubmeshIndexStart', async () => {
    
    const remoteG3d = await loadRemote()
    const abstract = await loadAbstract()
    const g3d = await loadG3d()
    const submeshCount = g3d.getSubmeshCount()
    
    for(let m=0; m < submeshCount; m++){
      const value = await remoteG3d.getSubmeshIndexStart(m)

      // Compare with original values before reordering.
      const attribute = abstract.findAttribute(VimAttributes.submeshIndexOffsets)
      const original = attribute?.data as Int32Array
      const expected = original[m]

      expect(value).toBe(expected)
    }
  })
  
  test('RemoteG3d.getSubmeshIndexEnd', async () => {
    
    const remoteG3d = await loadRemote()
    const abstract = await loadAbstract()
    const g3d = await loadG3d()
    const submeshCount = g3d.getSubmeshCount()
    
    for(let m=0; m < submeshCount; m++){
      const value = await remoteG3d.getSubmeshIndexEnd(m)

      // Compare with original values before reordering.
      const attribute = abstract.findAttribute(VimAttributes.submeshIndexOffsets)
      const original = attribute?.data as Int32Array
      const expected = m + 1 < original.length ? original[m+1] : g3d.indices.length
      expect(value).toBe(expected)
    }
  })

  test('RemoteG3d.getSubmeshIndexCount', async () => {
    
    const [g3d, remoteG3d] = await loadBoth()
    const submeshCount = g3d.getSubmeshCount()
    for(let m=0; m < submeshCount; m++){
      const value = await remoteG3d.getSubmeshIndexCount(-1)
      const expected = g3d.getSubmeshIndexCount(-1)
      expect(value).toBe(expected)
    }
  })

  test('RemoteG3d.toG3d', async () =>{
    const [g3d, remote] = await loadBoth()
    const value = await remote.toG3d()
    expectG3dAreSame(value, g3d)
  })  

  test('RemoteG3d.slice', async () =>{
    const [g3d, remote] = await loadBoth()
    for(let i = 0; i < g3d.getInstanceCount(); i++ ){
      const value = await remote.slice(i)
      const expected = await g3d.slice(i)
      expect(g3dAreEqual(value, expected)).toBeTruthy()
    }
  }) 

  test('RemoteG3d.filter (all)', async () =>{
    const [g3d, remote] = await loadBoth()
    const instances = Array.from(g3d.instanceMeshes.map((_,i) => i))
      const expected = g3d.filter(instances)
      const value = await remote.filter(instances)
      expectG3dAreSame(value, expected)
  }) 
})