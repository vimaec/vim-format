using Vim.Math3d;

namespace Vim.G3dNext.Attributes
{
    [AttributeCollection(
        typeof(MeshInstanceTransformAttribute),
        typeof(MeshOpaqueSubmeshCountAttribute),
        typeof(MeshSubmeshIndexOffsetAttribute),
        typeof(MeshSubmeshVertexOffsetAttribute),
        typeof(MeshSubmeshMaterialAttribute),
        typeof(MeshVertexAttribute),
        typeof(MeshIndexAttribute)
    )]
    public partial class MeshAttributeCollection // : IAttributeCollection
    {

    }

    [AttributeDescriptor("g3d:instance:transform:0:float32:16", AttributeType.Data, ArrayType = typeof(Matrix4x4))]
    public partial class MeshInstanceTransformAttribute { }

    [AttributeDescriptor("g3d:mesh:opaquesubmeshcount:0:int32:1", AttributeType.Data)]
    public partial class MeshOpaqueSubmeshCountAttribute { }

    [AttributeDescriptor("g3d:submesh:indexoffset:0:int32:1", AttributeType.Index, IndexInto = typeof(IndexAttribute))]
    public partial class MeshSubmeshIndexOffsetAttribute { }

    [AttributeDescriptor("g3d:submesh:vertexoffset:0:int32:1", AttributeType.Index, IndexInto = typeof(IndexAttribute))]
    public partial class MeshSubmeshVertexOffsetAttribute { }

    [AttributeDescriptor("g3d:submesh:material:0:int32:1", AttributeType.Index, IndexInto = typeof(MaterialColorAttribute))]
    public partial class MeshSubmeshMaterialAttribute { }

    [AttributeDescriptor("g3d:vertex:position:0:float32:3", AttributeType.Data, ArrayType = typeof(Vector3))]
    public partial class MeshVertexAttribute { }

    [AttributeDescriptor("g3d:corner:index:0:int32:1", AttributeType.Index, IndexInto = typeof(VertexAttribute))]
    public partial class MeshIndexAttribute { }
}
