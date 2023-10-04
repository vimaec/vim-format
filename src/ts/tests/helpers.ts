import * as fs from 'fs'
import { BFast } from '../src/bfast'
import { G3d, VimAttributes } from '../src/g3d'
import { AbstractG3d } from '../src/abstractG3d'
import { RemoteG3d } from '../src/remoteG3d'

export const testVimFilePath = `${__dirname}/../../../data/Wolford_Residence.r2023.om_v4.4.0.vim`



export function loadFile(path: string) {
    return new Promise<ArrayBuffer | undefined>((resolve, reject) => {
        fs.readFile(path, (err, data) => {
            if (err)
                reject(err)
            else {
                var arrbuf = new ArrayBuffer(data.length)
                const view = new Uint8Array(arrbuf)
                for (var i = 0; i < data.length; i++) {
                    view[i] = data[i]
                }

                resolve(arrbuf)
            }
        })
    })
}

export async function loadBoth(vimFilePath: string){
  const arrayBuffer = await loadFile(vimFilePath)
  const bfast = new BFast((arrayBuffer as ArrayBuffer)!)
  const g3dBfast = await bfast.getBfast('geometry')
  const g3d = await G3d.createFromBfast(g3dBfast!)
  const remote = RemoteG3d.createFromBfast(g3dBfast!)
  return [g3d, remote] as [G3d, RemoteG3d]
}

export async function loadRemote(vimFilePath: string){
  const arrayBuffer = await loadFile(vimFilePath)
  const bfast = new BFast((arrayBuffer as ArrayBuffer)!)
  const g3dBfast = await bfast.getBfast('geometry')
  const remote = RemoteG3d.createFromBfast(g3dBfast!)
  return remote 
}

export async function loadAbstract(vimFilePath: string){
  const arrayBuffer = await loadFile(vimFilePath)
  const bfast = new BFast((arrayBuffer as ArrayBuffer)!)
  const g3dBfast = await bfast.getBfast('geometry')
  const remote = await AbstractG3d.createFromBfast(g3dBfast!, VimAttributes.all)
  return remote 
}

export async function loadG3d(vimFilePath: string){
  const arrayBuffer = await loadFile(vimFilePath)
  const bfast = new BFast((arrayBuffer as ArrayBuffer)!)
  const g3dBfast = await bfast.getBfast('geometry')
  return await G3d.createFromBfast(g3dBfast!)
}

export function expectG3dAreSame(self: G3d, other: G3d){
  expect(self.instanceFlags).toEqual(other.instanceFlags)
  expect(self.instanceMeshes).toEqual(other.instanceMeshes)
  expect(self.instanceTransforms).toEqual(other.instanceTransforms)
  expect(self.instanceNodes).toEqual(other.instanceNodes)

  expect(self.meshSubmeshes).toEqual(other.meshSubmeshes)
  expect(self.submeshIndexOffset).toEqual(other.submeshIndexOffset)

  expect(self.submeshMaterial).toEqual(other.submeshMaterial)
  expect(self.materialColors).toEqual(other.materialColors)
  expect(self.positions).toEqual(other.positions)
  expect(self.indices).toEqual(other.indices)
}

export function g3dAreEqual(self: G3d, other: G3d){
  if (self.instanceMeshes.length !== other.instanceMeshes.length){
    console.log('instances count !=')
    return false
  }
  for(let i =0; i < self.instanceMeshes.length; i++){
    
    if(!instanceAreEqual(self, i, other, i)){
      console.log('instance != ' + i)
      return false
    }
  }
  return true
}

export function instanceAreEqual(self: G3d, instance: number, other:G3d, otherInstance: number){
  const selfFlag = self.instanceFlags[instance]
  const otherFlag = other.instanceFlags[otherInstance]

  if(selfFlag !== otherFlag){
    console.log('flags !=')
    return false
  }
  for(let i=0; i < 16; i++){
    if(self.instanceTransforms[instance *16 + i] !== other.instanceTransforms[otherInstance *16 + i]){
      console.log('transform !=')
      return false
    }
  }
  const selfMesh = self.instanceMeshes[instance]
  const otherMesh = other.instanceMeshes[otherInstance]
  return meshAreEqual(self, selfMesh, other, otherMesh)
}

export function meshAreEqual(self: G3d, mesh: number, other:G3d, otherMesh: number){
  if(mesh === -1 && otherMesh === -1){
    return true
  }
  if(mesh === -1 || otherMesh === -1){
    return false
  }

  const selfSubStart = self.getMeshSubmeshStart(mesh)
  const selfSubEnd = self.getMeshSubmeshEnd(mesh)
  const selfCount = selfSubEnd - selfSubStart

  const otherSubStart = other.getMeshSubmeshStart(otherMesh)
  const otherSubEnd = other.getMeshSubmeshEnd(otherMesh)
  const otherCount = otherSubEnd - otherSubStart

  if(selfCount !== otherCount){
    console.log([mesh,otherMesh])
    console.log([selfCount,otherCount])
    console.log('SubCount !=')
    return false
  }
  for(let i = 0 ; i < selfCount; i ++){
    const selfSub = selfSubStart + i
    const otherSub = otherSubStart + i
    if(!submeshAreEqual(self, mesh, selfSub, other, otherMesh, otherSub)){
      console.log('Sub !=')
      return false
    }
  }
  return true
}

export function submeshAreEqual(self: G3d, mesh: number, submesh: number, other:G3d, otherMesh: number, otherSubmesh: number){
  if(!submeshMaterialIsEqual(self, submesh, other, otherSubmesh)){
    console.log('mat !=')
    return false
  }
  if(!submeshGeometryIsEqual(self, mesh, submesh, other, otherMesh, otherSubmesh)){
    console.log('sub !=')
    return false
  }
  return true
}

export function submeshMaterialIsEqual(self: G3d, submesh: number, other:G3d, otherSubmesh: number){
  const selfColor = self.getSubmeshColor(submesh)
  const otherColor = other.getSubmeshColor(otherSubmesh)
  if(selfColor.length !== otherColor.length){
    console.log('mat length !=')
    return false
  }
  for(let i =0 ; i < selfColor.length; i ++){
    if(selfColor[i] !== otherColor[i]){
      console.log('color !=')
      console.log(selfColor)
      console.log(otherColor)
      return false
    }
  }
  return true
}

export function submeshGeometryIsEqual(self: G3d, mesh: number, submesh: number, other:G3d, otherMesh: number, otherSubmesh: number){
  const selfIndexStart = self.getSubmeshIndexStart(submesh)
  const selfIndexEnd = self.getSubmeshIndexEnd(submesh)
  const selfIndexCount = selfIndexEnd - selfIndexStart

  const otherIndexStart = other.getSubmeshIndexStart(otherSubmesh)
  const otherIndexEnd = other.getSubmeshIndexEnd(otherSubmesh)
  const otherIndexCount = otherIndexEnd - otherIndexStart

  const selfVertexStart = self.meshVertexOffsets[mesh]
  const otherVertexStart = other.meshVertexOffsets[otherMesh]

  if(selfIndexCount !== otherIndexCount){
    console.log('index count !=')
    return false
  }
  for(let i =0; i < selfIndexCount; i++){
    const selfLocalVertex = self.indices[selfIndexStart + i] 
    const otherLocalVertex = other.indices[otherIndexStart + i]
    
    if (selfLocalVertex !== otherLocalVertex){
      console.log('vertex !=')
      return false
    }
    
    const selfVertex = selfLocalVertex  + selfVertexStart
    const otherVertex = otherLocalVertex  + otherVertexStart
    for(let p=0; p<3; p++){
      
      const selfPosition = self.positions[selfVertex*3 + p]
      const otherPosition = other.positions[otherVertex*3 + p]
      if (selfPosition !== otherPosition){
        console.log([mesh, submesh, selfLocalVertex, selfVertexStart, selfVertex, selfPosition])
        console.log([otherMesh, otherSubmesh, otherLocalVertex, otherVertexStart, otherVertex, otherPosition])
        console.log('position !=')
        return false
      }
    }
  }
  return true
}
