using Vim.Math3d;

namespace Vim.G3dNext.Attributes
{
    [AttributeCollection(
        typeof(InstanceFilesAttribute),
        typeof(InstanceIndicesAttribute),
        typeof(InstanceNodesAttribute),
        typeof(InstanceGroupsAttribute),
        typeof(InstanceTagsAttribute),
        typeof(InstanceFlagsAttribute),
        typeof(InstanceMinsAttribute),
        typeof(InstanceMaxsAttribute),
        typeof(MeshInstanceCountsAttribute),
        typeof(MeshIndexCountsAttribute),
        typeof(MeshVertexCountsAttribute),
        typeof(MeshOpaqueIndexCountsAttribute),
        typeof(MeshOpaqueVertexCountsAttribute)
    )]
    public partial class SceneAttributeCollection // : IAttributeCollection
    {

    }

    [AttributeDescriptor("g3d:instance:file:0:int32:1", AttributeType.Data)]
    public partial class InstanceFilesAttribute { }

    [AttributeDescriptor("g3d:instance:node:0:int32:1", AttributeType.Data)]
    public partial class InstanceNodesAttribute { }

    [AttributeDescriptor("g3d:instance:index:0:int32:1", AttributeType.Data)]
    public partial class InstanceIndicesAttribute { }

    [AttributeDescriptor("g3d:instance:group:0:int32:1", AttributeType.Data)]
    public partial class InstanceGroupsAttribute { }

    [AttributeDescriptor("g3d:instance:tag:0:int64:1", AttributeType.Data)]
    public partial class InstanceTagsAttribute { }

    [AttributeDescriptor("g3d:instance:min:0:float32:3", AttributeType.Data, ArrayType = typeof(Vector3))]
    public partial class InstanceMinsAttribute { }

    [AttributeDescriptor("g3d:instance:max:0:float32:3", AttributeType.Data, ArrayType = typeof(Vector3))]
    public partial class InstanceMaxsAttribute { }

    [AttributeDescriptor("g3d:mesh:instancecount:0:int32:1", AttributeType.Data)]
    public partial class MeshInstanceCountsAttribute { }

    [AttributeDescriptor("g3d:mesh:vertexcount:0:int32:1", AttributeType.Data)]
    public partial class MeshVertexCountsAttribute { }

    [AttributeDescriptor("g3d:mesh:indexcount:0:int32:1", AttributeType.Data)]
    public partial class MeshIndexCountsAttribute { }

    [AttributeDescriptor("g3d:mesh:opaquevertexcount:0:int32:1", AttributeType.Data)]
    public partial class MeshOpaqueVertexCountsAttribute { }

    [AttributeDescriptor("g3d:mesh:opaqueindexcount:0:int32:1", AttributeType.Data)]
    public partial class MeshOpaqueIndexCountsAttribute { }

}
