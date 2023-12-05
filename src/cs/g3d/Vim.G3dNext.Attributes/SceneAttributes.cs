using Vim.Math3d;

namespace Vim.G3dNext.Attributes
{
    [AttributeCollection(
        typeof(SceneChunkCountAttribute),
        typeof(SceneInstanceMeshesAttribute),
        typeof(SceneInstanceTransformsAttribute),
        typeof(SceneInstanceTransformDataAttribute),
        typeof(SceneInstanceNodesAttribute),
        typeof(SceneInstanceGroupsAttribute),
        typeof(SceneInstanceTagsAttribute),
        typeof(SceneInstanceFlagsAttribute),
        typeof(SceneInstanceMinsAttribute),
        typeof(SceneInstanceMaxsAttribute),
        typeof(SceneMeshChunksAttribute),
        typeof(SceneMeshChunkIndicesAttribute),
        typeof(SceneMeshInstanceCountsAttribute),
        typeof(SceneMeshIndexCountsAttribute),
        typeof(SceneMeshVertexCountsAttribute),
        typeof(SceneMeshOpaqueIndexCountsAttribute),
        typeof(SceneMeshOpaqueVertexCountsAttribute)
    )]
    public partial class SceneAttributeCollection // : IAttributeCollection
    {

    }

    [AttributeDescriptor("Scene", "g3d:chunk:count:0:int32:1", AttributeType.Data)]
    public partial class SceneChunkCountAttribute { }

    [AttributeDescriptor("Scene", "g3d:instance:mesh:0:int32:1", AttributeType.Data)]
    public partial class SceneInstanceMeshesAttribute { }

    [AttributeDescriptor("Scene", "g3d:instance:transform:0:int32:1", AttributeType.Data)]
    public partial class SceneInstanceTransformsAttribute { }

    [AttributeDescriptor("Scene", "g3d:instance:transform:0:float32:16", AttributeType.Data, ArrayType = typeof(Matrix4x4))]
    public partial class SceneInstanceTransformDataAttribute { }

    [AttributeDescriptor("Scene", "g3d:instance:node:0:int32:1", AttributeType.Data)]
    public partial class SceneInstanceNodesAttribute { }

    [AttributeDescriptor("Scene", "g3d:instance:group:0:int32:1", AttributeType.Data)]
    public partial class SceneInstanceGroupsAttribute { }

    [AttributeDescriptor("Scene", "g3d:instance:tag:0:int64:1", AttributeType.Data)]
    public partial class SceneInstanceTagsAttribute { }

    [AttributeDescriptor("Scene", "g3d:instance:flags:0:uint16:1", AttributeType.Data)]
    public partial class SceneInstanceFlagsAttribute { }

    [AttributeDescriptor("Scene", "g3d:instance:min:0:float32:3", AttributeType.Data, ArrayType = typeof(Vector3))]
    public partial class SceneInstanceMinsAttribute { }

    [AttributeDescriptor("Scene", "g3d:instance:max:0:float32:3", AttributeType.Data, ArrayType = typeof(Vector3))]
    public partial class SceneInstanceMaxsAttribute { }

    [AttributeDescriptor("Scene", "g3d:mesh:chunk:0:int32:1", AttributeType.Data)]
    public partial class SceneMeshChunksAttribute { }

    [AttributeDescriptor("Scene", "g3d:mesh:chunkindex:0:int32:1", AttributeType.Data)]
    public partial class SceneMeshChunkIndicesAttribute { }

    [AttributeDescriptor("Scene", "g3d:mesh:instancecount:0:int32:1", AttributeType.Data)]
    public partial class SceneMeshInstanceCountsAttribute { }

    [AttributeDescriptor("Scene", "g3d:mesh:vertexcount:0:int32:1", AttributeType.Data)]
    public partial class SceneMeshVertexCountsAttribute { }

    [AttributeDescriptor("Scene", "g3d:mesh:indexcount:0:int32:1", AttributeType.Data)]
    public partial class SceneMeshIndexCountsAttribute { }

    [AttributeDescriptor("Scene", "g3d:mesh:opaquevertexcount:0:int32:1", AttributeType.Data)]
    public partial class SceneMeshOpaqueVertexCountsAttribute { }

    [AttributeDescriptor("Scene", "g3d:mesh:opaqueindexcount:0:int32:1", AttributeType.Data)]
    public partial class SceneMeshOpaqueIndexCountsAttribute { }
}
