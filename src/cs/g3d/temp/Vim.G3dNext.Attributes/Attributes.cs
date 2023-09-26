using Vim.Math3d;

namespace Vim.G3dNext.Attributes
{
    #region vim
    [AttributeDescriptor("g3d:all:facesize:0:int32:1", AttributeType.Singleton)]
    public partial class CornersPerFaceAttribute { }

    [AttributeDescriptor("g3d:vertex:position:0:float32:3", AttributeType.Data, ArrayType = typeof(Vector3))]
    public partial class VertexAttribute { }

    [AttributeDescriptor("g3d:corner:index:0:int32:1", AttributeType.Index, IndexInto = typeof(VertexAttribute))]
    public partial class IndexAttribute { }

    [AttributeDescriptor("g3d:instance:transform:0:float32:16", AttributeType.Data, ArrayType = typeof(Matrix4x4))]
    public partial class InstanceTransformAttribute { }

    [AttributeDescriptor("g3d:instance:parent:0:int32:1", AttributeType.Index, IndexInto = typeof(InstanceTransformAttribute))]
    public partial class InstanceParentAttribute { }

    [AttributeDescriptor("g3d:instance:flags:0:uint16:1", AttributeType.Data)]
    public partial class InstanceFlagsAttribute { }

    [AttributeDescriptor("g3d:instance:mesh:0:int32:1", AttributeType.Index, IndexInto = typeof(MeshSubmeshOffsetAttribute))]
    public partial class InstanceMeshAttribute { }

    [AttributeDescriptor("g3d:mesh:submeshoffset:0:int32:1", AttributeType.Index, IndexInto = typeof(SubmeshIndexOffsetAttribute))]
    public partial class MeshSubmeshOffsetAttribute { }

    [AttributeDescriptor("g3d:submesh:indexoffset:0:int32:1", AttributeType.Index, IndexInto = typeof(IndexAttribute))]
    public partial class SubmeshIndexOffsetAttribute { }

    [AttributeDescriptor("g3d:submesh:material:0:int32:1", AttributeType.Index, IndexInto = typeof(MaterialColorAttribute))]
    public partial class SubmeshMaterialAttribute { }



    [AttributeDescriptor("g3d:shapevertex:position:0:float32:3", AttributeType.Data, ArrayType = typeof(Vector3))]
    public partial class ShapeVertexAttribute { }

    [AttributeDescriptor("g3d:shape:vertexoffset:0:int32:1", AttributeType.Index, IndexInto = typeof(ShapeVertexAttribute))]
    public partial class ShapeVertexOffsetAttribute { }

    [AttributeDescriptor("g3d:shape:color:0:float32:4", AttributeType.Data, ArrayType = typeof(Vector4))]
    public partial class ShapeColorAttribute { }

    [AttributeDescriptor("g3d:shape:width:0:float32:1", AttributeType.Data)]
    public partial class ShapeWidthAttribute { }

    [AttributeCollection(
        typeof(CornersPerFaceAttribute),
        typeof(VertexAttribute),
        typeof(IndexAttribute),
        typeof(InstanceTransformAttribute),
        typeof(InstanceParentAttribute),
        typeof(InstanceFlagsAttribute),
        typeof(InstanceMeshAttribute),
        typeof(MeshSubmeshOffsetAttribute),
        typeof(SubmeshIndexOffsetAttribute),
        typeof(SubmeshMaterialAttribute),
        typeof(MaterialColorAttribute),
        typeof(MaterialGlossinessAttribute),
        typeof(MaterialSmoothnessAttribute),
        typeof(ShapeVertexAttribute),
        typeof(ShapeVertexOffsetAttribute),
        typeof(ShapeColorAttribute),
        typeof(ShapeWidthAttribute))]
    public partial class VimAttributeCollection // : IAttributeCollection
    {

    }

    #endregion

    #region mesh

    [AttributeDescriptor("g3d:submesh:vertexoffset:0:int32:1", AttributeType.Index, IndexInto = typeof(IndexAttribute))]
    public partial class SubmeshVertexOffsetAttribute { }

    [AttributeDescriptor("g3d:mesh:opaquesubmeshcount:0:int32:1", AttributeType.Data)]
    public partial class MeshOpaqueSubmeshCountAttribute { }

    [AttributeCollection(
        typeof(InstanceTransformAttribute),
        typeof(MeshOpaqueSubmeshCountAttribute),
        typeof(SubmeshIndexOffsetAttribute),
        typeof(SubmeshVertexOffsetAttribute),
        typeof(SubmeshMaterialAttribute),
        typeof(VertexAttribute),
        typeof(IndexAttribute)
    )]
    public partial class MeshAttributeCollection // : IAttributeCollection
    {

    }
    #endregion

    #region scene
    [AttributeDescriptor("g3d:instance:file:0:int32:1", AttributeType.Data)]
    public partial class InstanceFileAttribute { }

    [AttributeDescriptor("g3d:instance:node:0:int32:1", AttributeType.Data)]
    public partial class InstanceNodeAttribute { }


    [AttributeDescriptor("g3d:instance:index:0:int32:1", AttributeType.Data)]
    public partial class InstanceIndexAttribute { }

    [AttributeDescriptor("g3d:instance:group:0:int32:1", AttributeType.Data)]
    public partial class InstanceGroupAttribute { }

    [AttributeDescriptor("g3d:instance:tag:0:int64:1", AttributeType.Data)]
    public partial class InstanceTagAttribute { }

    [AttributeDescriptor("g3d:instance:min:0:float32:3", AttributeType.Data, ArrayType = typeof(Vector3))]
    public partial class InstanceMinAttribute { }

    [AttributeDescriptor("g3d:instance:max:0:float32:3", AttributeType.Data, ArrayType = typeof(Vector3))]
    public partial class InstanceMaxAttribute { }

    [AttributeDescriptor("g3d:mesh:instancecount:0:int32:1", AttributeType.Data)]
    public partial class MeshInstanceCountAttribute { }

    [AttributeDescriptor("g3d:mesh:vertexcount:0:int32:1", AttributeType.Data)]
    public partial class MeshVertexCountAttribute { }

    [AttributeDescriptor("g3d:mesh:indexcount:0:int32:1", AttributeType.Data)]
    public partial class MeshIndexCountAttribute { }

    [AttributeDescriptor("g3d:mesh:opaquevertexcount:0:int32:1", AttributeType.Data)]
    public partial class MeshOpaqueVertexCountAttribute { }

    [AttributeDescriptor("g3d:mesh:opaqueindexcount:0:int32:1", AttributeType.Data)]
    public partial class MeshOpaqueIndexCountAttribute { }

    [AttributeCollection(
        typeof(InstanceFileAttribute),
        typeof(InstanceIndexAttribute),
        typeof(InstanceNodeAttribute),
        typeof(InstanceGroupAttribute),
        typeof(InstanceTagAttribute),
        typeof(InstanceFlagsAttribute),
        typeof(InstanceMinAttribute),
        typeof(InstanceMaxAttribute),
        typeof(MeshInstanceCountAttribute),
        typeof(MeshIndexCountAttribute),
        typeof(MeshVertexCountAttribute),
        typeof(MeshOpaqueIndexCountAttribute),
        typeof(MeshOpaqueVertexCountAttribute)
    )]
    public partial class SceneAttributeCollection // : IAttributeCollection
    {

    }
    #endregion

    #region materials
    // Material
    [AttributeDescriptor("g3d:material:color:0:float32:4", AttributeType.Data, ArrayType = typeof(Vector4))]
    public partial class MaterialColorAttribute { }

    [AttributeDescriptor("g3d:material:glossiness:0:float32:1", AttributeType.Data)]
    public partial class MaterialGlossinessAttribute { }

    [AttributeDescriptor("g3d:material:smoothness:0:float32:1", AttributeType.Data)]
    public partial class MaterialSmoothnessAttribute { }

    [AttributeCollection(
        typeof(MaterialColorAttribute),
        typeof(MaterialGlossinessAttribute),
        typeof(MaterialSmoothnessAttribute)
    )]
    public partial class MaterialAttributeCollection // : IAttributeCollection
    {

    }
    #endregion
}
