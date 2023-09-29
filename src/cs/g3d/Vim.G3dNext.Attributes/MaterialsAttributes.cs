using Vim.Math3d;

namespace Vim.G3dNext.Attributes
{
    [AttributeCollection(
        typeof(MaterialColorsAttribute),
        typeof(MaterialGlossinessAttribute),
        typeof(MaterialSmoothnessAttribute)
    )]
    public partial class MaterialsAttributeCollection // : IAttributeCollection
    {

    }

    [AttributeDescriptor("g3d:material:color:0:float32:4", AttributeType.Data, ArrayType = typeof(Vector4))]
    public partial class MaterialColorsAttribute { }

    [AttributeDescriptor("g3d:material:glossiness:0:float32:1", AttributeType.Data)]
    public partial class MaterialGlossinessAttribute { }

    [AttributeDescriptor("g3d:material:smoothness:0:float32:1", AttributeType.Data)]
    public partial class MaterialSmoothnessAttribute { }
}
