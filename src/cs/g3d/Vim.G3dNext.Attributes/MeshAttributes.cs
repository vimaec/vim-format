using Vim.Math3d;

namespace Vim.G3dNext.Attributes
{
    [AttributeCollection(
        typeof(ChunkMeshSubmeshOffsetAttribute),
        typeof(ChunkMeshOpaqueSubmeshCountsAttribute),
        typeof(ChunkSubmeshIndexOffsetsAttribute),
        typeof(ChunkSubmeshVertexOffsetsAttribute),
        typeof(ChunkSubmeshMaterialsAttribute),
        typeof(ChunkPositionsAttribute),
        typeof(ChunkIndicesAttribute)
    )]
    public partial class ChunkAttributeCollection // : IAttributeCollection
    {

    }

    [AttributeDescriptor("Chunk", "g3d:mesh:opaquesubmeshcount:0:int32:1", AttributeType.Data)]
    public partial class ChunkMeshOpaqueSubmeshCountsAttribute { }

    [AttributeDescriptor("Chunk", "g3d:mesh:submeshOffset:0:int32:1", AttributeType.Index, IndexInto = typeof(ChunkIndicesAttribute))]
    public partial class ChunkMeshSubmeshOffsetAttribute { }

    [AttributeDescriptor("Chunk", "g3d:submesh:indexoffset:0:int32:1", AttributeType.Index, IndexInto = typeof(ChunkIndicesAttribute))]
    public partial class ChunkSubmeshIndexOffsetsAttribute { }

    [AttributeDescriptor("Chunk", "g3d:submesh:vertexoffset:0:int32:1", AttributeType.Index, IndexInto = typeof(ChunkIndicesAttribute))]
    public partial class ChunkSubmeshVertexOffsetsAttribute { }

    [AttributeDescriptor("Chunk", "g3d:submesh:material:0:int32:1", AttributeType.Index)]
    public partial class ChunkSubmeshMaterialsAttribute { }

    [AttributeDescriptor("Chunk", "g3d:vertex:position:0:float32:3", AttributeType.Data, ArrayType = typeof(Vector3))]
    public partial class ChunkPositionsAttribute { }

    [AttributeDescriptor("Chunk", "g3d:corner:index:0:int32:1", AttributeType.Index, IndexInto = typeof(ChunkPositionsAttribute))]
    public partial class ChunkIndicesAttribute { }
}


public enum MeshSection
{
    All, 
    Opaque,
    Transparent
}
