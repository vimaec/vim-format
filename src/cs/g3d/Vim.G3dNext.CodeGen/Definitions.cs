using Vim.Math3d;

namespace Vim.G3dNext.CodeGen
{
    internal class Definitions
    {
        public static G3dEntity[] GetEntities()
        {
            return new G3dEntity[] { vim, mesh, materials, scene };
        }

        public static G3dEntity vim = new G3dEntity("G3dVim")
            .Index("Indices", "g3d:corner:index:0:int32:1", "Positions")
            .Data<Vector3>("Positions", "g3d:vertex:position:0:float32:3")
            .Data<Matrix4x4>("InstanceTransforms", "g3d:instance:transform:0:float32:16")
            .Data<ushort>("InstanceFlags", "g3d:instance:flags:0:uint16:1")
            .Index("InstanceMeshes", "g3d:instance:mesh:0:int32:1", "MeshSubmeshOffsets")
            .Index("MeshSubmeshOffsets", "g3d:mesh:submeshoffset:0:int32:1", "SubmeshIndexOffsets")
            .Index("SubmeshIndexOffsets", "g3d:submesh:indexoffset:0:int32:1", "Indices")
            .Index("SubmeshMaterials", "g3d:submesh:material:0:int32:1", "MaterialColors")
            .Data<Vector4>("MaterialColors", "g3d:material:color:0:float32:4")
            .Data<float>("MaterialGlossiness", "g3d:material:glossiness:0:float32:1")
            .Data<float>("MaterialSmoothness", "g3d:material:smoothness:0:float32:1")
            .Data<Vector3>("ShapeVertices", "g3d:shapevertex:position:0:float32:3")
            .Index("ShapeVertexOffsets", "g3d:shape:vertexoffset:0:int32:1", "ShapeVertices")
            .Data<Vector4>("ShapeColors", "g3d:shape:color:0:float32:4")
            .Data<float>("ShapeWidths", "g3d:shape:width:0:float32:1");

        public static G3dEntity scene = new G3dEntity("G3dScene")
            .Data<int>("ChunkCount", "g3d:chunk:count:0:int32:1")
            .Data<int>("InstanceMeshes", "g3d:instance:mesh:0:int32:1")
            .Data<Matrix4x4>("InstanceTransformData", "g3d:instance:transform:0:float32:16")
            .Data<int>("InstanceNodes", "g3d:instance:node:0:int32:1")
            .Data<int>("InstanceGroups", "g3d:instance:group:0:int32:1")
            .Data<long>("InstanceTags", "g3d:instance:tag:0:int64:1")
            .Data<ushort>("InstanceFlags", "g3d:instance:flags:0:uint16:1")
            .Data<Vector3>("InstanceMins", "g3d:instance:min:0:float32:3")
            .Data<Vector3>("InstanceMaxs", "g3d:instance:max:0:float32:3")
            .Data<int>("MeshChunks", "g3d:mesh:chunk:0:int32:1")
            .Data<int>("MeshChunkIndices", "g3d:mesh:chunkindex:0:int32:1")
            .Data<int>("MeshVertexCounts", "g3d:mesh:vertexcount:0:int32:1")
            .Data<int>("MeshIndexCounts", "g3d:mesh:indexcount:0:int32:1")
            .Data<int>("MeshOpaqueVertexCounts", "g3d:mesh:opaquevertexcount:0:int32:1")
            .Data<int>("MeshOpaqueIndexCounts", "g3d:mesh:opaqueindexcount:0:int32:1");

        public static G3dEntity materials = new G3dEntity("G3dMaterials")
            .Data<Vector4>("MaterialColors", "g3d:material:color:0:float32:4")
            .Data<float>("MaterialGlossiness", "g3d:material:glossiness:0:float32:1")
            .Data<float>("MaterialSmoothness", "g3d:material:smoothness:0:float32:1");


        public static G3dEntity mesh = new G3dEntity("G3dChunk")
            .Data<int>("MeshOpaqueSubmeshCounts", "g3d:mesh:opaquesubmeshcount:0:int32:1")
            .Index("MeshSubmeshOffset", "g3d:mesh:submeshoffset:0:int32:1", "Indices")
            .Index("SubmeshIndexOffsets", "g3d:submesh:indexoffset:0:int32:1", "Indices")
            .Index("SubmeshVertexOffsets", "g3d:submesh:vertexoffset:0:int32:1", "Indices")
            .Index("SubmeshMaterials", "g3d:submesh:material:0:int32:1")
            .Data<Vector3>("Positions", "g3d:vertex:position:0:float32:3")
            .Index("Indices", "g3d:corner:index:0:int32:1", "Positions");
    }
}
