using Vim.Math3d;

namespace Vim.G3dNext.Attributes
{
    [AttributeCollection(
        typeof(MeshInstanceTransformsAttribute),
        typeof(MeshSubmeshOffsetAttribute),
        typeof(MeshOpaqueSubmeshCountsAttribute),
        typeof(MeshSubmeshIndexOffsetsAttribute),
        typeof(MeshSubmeshVertexOffsetsAttribute),
        typeof(MeshSubmeshMaterialsAttribute),
        typeof(MeshPositionsAttribute),
        typeof(MeshIndicesAttribute)
    )]
    public partial class MeshAttributeCollection // : IAttributeCollection
    {

    }

    [AttributeDescriptor("Mesh", "g3d:instance:transform:0:float32:16", AttributeType.Data, ArrayType = typeof(Matrix4x4))]
    public partial class MeshInstanceTransformsAttribute { }

    [AttributeDescriptor("Mesh", "g3d:mesh:opaquesubmeshcount:0:int32:1", AttributeType.Data)]
    public partial class MeshOpaqueSubmeshCountsAttribute { }

    [AttributeDescriptor("Mesh", "g3d:mesh:submeshOffset:0:int32:1", AttributeType.Index, IndexInto = typeof(MeshIndicesAttribute))]
    public partial class MeshSubmeshOffsetAttribute { }

    [AttributeDescriptor("Mesh", "g3d:submesh:indexoffset:0:int32:1", AttributeType.Index, IndexInto = typeof(MeshIndicesAttribute))]
    public partial class MeshSubmeshIndexOffsetsAttribute { }

    [AttributeDescriptor("Mesh", "g3d:submesh:vertexoffset:0:int32:1", AttributeType.Index, IndexInto = typeof(MeshIndicesAttribute))]
    public partial class MeshSubmeshVertexOffsetsAttribute { }

    [AttributeDescriptor("Mesh", "g3d:submesh:material:0:int32:1", AttributeType.Index)]
    public partial class MeshSubmeshMaterialsAttribute { }

    [AttributeDescriptor("Mesh", "g3d:vertex:position:0:float32:3", AttributeType.Data, ArrayType = typeof(Vector3))]
    public partial class MeshPositionsAttribute { }

    [AttributeDescriptor("Mesh", "g3d:corner:index:0:int32:1", AttributeType.Index, IndexInto = typeof(MeshPositionsAttribute))]
    public partial class MeshIndicesAttribute { }
}


public enum MeshSection
{
    Opaque,
    Transparent
}
