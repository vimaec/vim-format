using Vim.Math3d;

namespace Vim.G3dNext.Attributes
{
    [AttributeCollection(
        typeof(VimPositionsAttribute),
        typeof(VimIndicesAttribute),
        typeof(VimInstanceTransformsAttribute),
        typeof(VimInstanceParentsAttribute),
        typeof(VimInstanceFlagsAttribute),
        typeof(VimInstanceMeshesAttribute),
        typeof(VimMeshSubmeshOffsetsAttribute),
        typeof(VimSubmeshIndexOffsetsAttribute),
        typeof(VimSubmeshMaterialsAttribute),
        typeof(VimMaterialColorsAttribute),
        typeof(VimMaterialGlossinessAttribute),
        typeof(VimMaterialSmoothnessAttribute),
        typeof(VimShapeVerticesAttribute),
        typeof(VimShapeVertexOffsetsAttribute),
        typeof(VimShapeColorsAttribute),
        typeof(VimShapeWidthsAttribute)
    )]
    public partial class VimAttributeCollection // : IAttributeCollection
    {

    }

    [AttributeDescriptor("Vim", "g3d:vertex:position:0:float32:3", AttributeType.Data, ArrayType = typeof(Vector3))]
    public partial class VimPositionsAttribute { }

    [AttributeDescriptor("Vim", "g3d:corner:index:0:int32:1", AttributeType.Index, IndexInto = typeof(VimPositionsAttribute))]
    public partial class VimIndicesAttribute { }

    [AttributeDescriptor("Vim", "g3d:instance:transform:0:float32:16", AttributeType.Data, ArrayType = typeof(Matrix4x4))]
    public partial class VimInstanceTransformsAttribute { }

    [AttributeDescriptor("Vim", "g3d:instance:parent:0:int32:1", AttributeType.Index, IndexInto = typeof(VimInstanceTransformsAttribute))]
    public partial class VimInstanceParentsAttribute { }

    [AttributeDescriptor("Vim", "g3d:instance:flags:0:uint16:1", AttributeType.Data)]
    public partial class VimInstanceFlagsAttribute { }

    [AttributeDescriptor("Vim", "g3d:instance:mesh:0:int32:1", AttributeType.Index, IndexInto = typeof(VimMeshSubmeshOffsetsAttribute))]
    public partial class VimInstanceMeshesAttribute { }

    [AttributeDescriptor("Vim", "g3d:mesh:submeshoffset:0:int32:1", AttributeType.Index, IndexInto = typeof(VimSubmeshIndexOffsetsAttribute))]
    public partial class VimMeshSubmeshOffsetsAttribute { }

    [AttributeDescriptor("Vim", "g3d:submesh:indexoffset:0:int32:1", AttributeType.Index, IndexInto = typeof(VimIndicesAttribute))]
    public partial class VimSubmeshIndexOffsetsAttribute { }

    [AttributeDescriptor("Vim", "g3d:submesh:material:0:int32:1", AttributeType.Index, IndexInto = typeof(VimMaterialColorsAttribute))]
    public partial class VimSubmeshMaterialsAttribute { }

    [AttributeDescriptor("Vim", "g3d:material:color:0:float32:4", AttributeType.Data, ArrayType = typeof(Vector4))]
    public partial class VimMaterialColorsAttribute { }

    [AttributeDescriptor("Vim", "g3d:material:glossiness:0:float32:1", AttributeType.Data)]
    public partial class VimMaterialGlossinessAttribute { }

    [AttributeDescriptor("Vim", "g3d:material:smoothness:0:float32:1", AttributeType.Data)]
    public partial class VimMaterialSmoothnessAttribute { }

    [AttributeDescriptor("Vim", "g3d:shapevertex:position:0:float32:3", AttributeType.Data, ArrayType = typeof(Vector3))]
    public partial class VimShapeVerticesAttribute { }

    [AttributeDescriptor("Vim", "g3d:shape:vertexoffset:0:int32:1", AttributeType.Index, IndexInto = typeof(VimShapeVerticesAttribute))]
    public partial class VimShapeVertexOffsetsAttribute { }

    [AttributeDescriptor("Vim", "g3d:shape:color:0:float32:4", AttributeType.Data, ArrayType = typeof(Vector4))]
    public partial class VimShapeColorsAttribute { }

    [AttributeDescriptor("Vim", "g3d:shape:width:0:float32:1", AttributeType.Data)]
    public partial class VimShapeWidthsAttribute { }
}
