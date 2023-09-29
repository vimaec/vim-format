using Vim.Math3d;

namespace Vim.G3dNext.Attributes
{
    [AttributeCollection(
        typeof(PositionsAttribute),
        typeof(IndicesAttribute),
        typeof(InstanceTransformsAttribute),
        typeof(InstanceParentsAttribute),
        typeof(InstanceFlagsAttribute),
        typeof(InstanceMeshesAttribute),
        typeof(MeshSubmeshOffsetsAttribute),
        typeof(SubmeshIndexOffsetsAttribute),
        typeof(SubmeshMaterialsAttribute),
        typeof(MaterialColorsAttribute),
        typeof(MaterialGlossinessAttribute),
        typeof(MaterialSmoothnessAttribute),
        typeof(ShapeVerticesAttribute),
        typeof(ShapeVertexOffsetsAttribute),
        typeof(ShapeColorsAttribute),
        typeof(ShapeWidthsAttribute)
    )]
    public partial class VimAttributeCollection // : IAttributeCollection
    {

    }

    [AttributeDescriptor("g3d:vertex:position:0:float32:3", AttributeType.Data, ArrayType = typeof(Vector3))]
    public partial class PositionsAttribute { }

    [AttributeDescriptor("g3d:corner:index:0:int32:1", AttributeType.Index, IndexInto = typeof(PositionsAttribute))]
    public partial class IndicesAttribute { }

    [AttributeDescriptor("g3d:instance:transform:0:float32:16", AttributeType.Data, ArrayType = typeof(Matrix4x4))]
    public partial class InstanceTransformsAttribute { }

    [AttributeDescriptor("g3d:instance:parent:0:int32:1", AttributeType.Index, IndexInto = typeof(InstanceTransformsAttribute))]
    public partial class InstanceParentsAttribute { }

    [AttributeDescriptor("g3d:instance:flags:0:uint16:1", AttributeType.Data)]
    public partial class InstanceFlagsAttribute { }

    [AttributeDescriptor("g3d:instance:mesh:0:int32:1", AttributeType.Index, IndexInto = typeof(MeshSubmeshOffsetsAttribute))]
    public partial class InstanceMeshesAttribute { }

    [AttributeDescriptor("g3d:mesh:submeshoffset:0:int32:1", AttributeType.Index, IndexInto = typeof(SubmeshIndexOffsetsAttribute))]
    public partial class MeshSubmeshOffsetsAttribute { }

    [AttributeDescriptor("g3d:submesh:indexoffset:0:int32:1", AttributeType.Index, IndexInto = typeof(IndicesAttribute))]
    public partial class SubmeshIndexOffsetsAttribute { }

    [AttributeDescriptor("g3d:submesh:material:0:int32:1", AttributeType.Index, IndexInto = typeof(MaterialColorsAttribute))]
    public partial class SubmeshMaterialsAttribute { }

    [AttributeDescriptor("g3d:shapevertex:position:0:float32:3", AttributeType.Data, ArrayType = typeof(Vector3))]
    public partial class ShapeVerticesAttribute { }

    [AttributeDescriptor("g3d:shape:vertexoffset:0:int32:1", AttributeType.Index, IndexInto = typeof(ShapeVerticesAttribute))]
    public partial class ShapeVertexOffsetsAttribute { }

    [AttributeDescriptor("g3d:shape:color:0:float32:4", AttributeType.Data, ArrayType = typeof(Vector4))]
    public partial class ShapeColorsAttribute { }

    [AttributeDescriptor("g3d:shape:width:0:float32:1", AttributeType.Data)]
    public partial class ShapeWidthsAttribute { }
}
