// AUTO-GENERATED FILE, DO NOT MODIFY WACKO3.
// ReSharper disable All
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Vim.BFast;
using Vim.BFastNextNS;

namespace Vim.G3dNext.Attributes
{
    
    public partial class VimPositionsAttribute : IAttribute<Vim.Math3d.Vector3>
    {
        public const string AttributeName = "g3d:vertex:position:0:float32:3";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<Vim.Math3d.Vector3>("g3d:vertex:position:0:float32:3");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public Vim.Math3d.Vector3[] TypedData { get; set; }
            = Array.Empty<Vim.Math3d.Vector3>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class VimIndicesAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:corner:index:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:corner:index:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Index;

        public Type IndexInto { get; }
            = typeof(Vim.G3dNext.Attributes.VimPositionsAttribute);

        public System.Int32[] TypedData { get; set; }
            = Array.Empty<System.Int32>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class VimInstanceTransformsAttribute : IAttribute<Vim.Math3d.Matrix4x4>
    {
        public const string AttributeName = "g3d:instance:transform:0:float32:16";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<Vim.Math3d.Matrix4x4>("g3d:instance:transform:0:float32:16");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public Vim.Math3d.Matrix4x4[] TypedData { get; set; }
            = Array.Empty<Vim.Math3d.Matrix4x4>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class VimInstanceParentsAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:instance:parent:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:instance:parent:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Index;

        public Type IndexInto { get; }
            = typeof(Vim.G3dNext.Attributes.VimInstanceTransformsAttribute);

        public System.Int32[] TypedData { get; set; }
            = Array.Empty<System.Int32>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class VimInstanceFlagsAttribute : IAttribute<System.UInt16>
    {
        public const string AttributeName = "g3d:instance:flags:0:uint16:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.UInt16>("g3d:instance:flags:0:uint16:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public System.UInt16[] TypedData { get; set; }
            = Array.Empty<System.UInt16>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class VimInstanceMeshesAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:instance:mesh:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:instance:mesh:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Index;

        public Type IndexInto { get; }
            = typeof(Vim.G3dNext.Attributes.VimMeshSubmeshOffsetsAttribute);

        public System.Int32[] TypedData { get; set; }
            = Array.Empty<System.Int32>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class VimMeshSubmeshOffsetsAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:mesh:submeshoffset:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:mesh:submeshoffset:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Index;

        public Type IndexInto { get; }
            = typeof(Vim.G3dNext.Attributes.VimSubmeshIndexOffsetsAttribute);

        public System.Int32[] TypedData { get; set; }
            = Array.Empty<System.Int32>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class VimSubmeshIndexOffsetsAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:submesh:indexoffset:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:submesh:indexoffset:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Index;

        public Type IndexInto { get; }
            = typeof(Vim.G3dNext.Attributes.VimIndicesAttribute);

        public System.Int32[] TypedData { get; set; }
            = Array.Empty<System.Int32>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class VimSubmeshMaterialsAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:submesh:material:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:submesh:material:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Index;

        public Type IndexInto { get; }
            = typeof(Vim.G3dNext.Attributes.VimMaterialColorsAttribute);

        public System.Int32[] TypedData { get; set; }
            = Array.Empty<System.Int32>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class VimMaterialColorsAttribute : IAttribute<Vim.Math3d.Vector4>
    {
        public const string AttributeName = "g3d:material:color:0:float32:4";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<Vim.Math3d.Vector4>("g3d:material:color:0:float32:4");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public Vim.Math3d.Vector4[] TypedData { get; set; }
            = Array.Empty<Vim.Math3d.Vector4>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class VimMaterialGlossinessAttribute : IAttribute<System.Single>
    {
        public const string AttributeName = "g3d:material:glossiness:0:float32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Single>("g3d:material:glossiness:0:float32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public System.Single[] TypedData { get; set; }
            = Array.Empty<System.Single>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class VimMaterialSmoothnessAttribute : IAttribute<System.Single>
    {
        public const string AttributeName = "g3d:material:smoothness:0:float32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Single>("g3d:material:smoothness:0:float32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public System.Single[] TypedData { get; set; }
            = Array.Empty<System.Single>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class VimShapeVerticesAttribute : IAttribute<Vim.Math3d.Vector3>
    {
        public const string AttributeName = "g3d:shapevertex:position:0:float32:3";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<Vim.Math3d.Vector3>("g3d:shapevertex:position:0:float32:3");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public Vim.Math3d.Vector3[] TypedData { get; set; }
            = Array.Empty<Vim.Math3d.Vector3>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class VimShapeVertexOffsetsAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:shape:vertexoffset:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:shape:vertexoffset:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Index;

        public Type IndexInto { get; }
            = typeof(Vim.G3dNext.Attributes.VimShapeVerticesAttribute);

        public System.Int32[] TypedData { get; set; }
            = Array.Empty<System.Int32>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class VimShapeColorsAttribute : IAttribute<Vim.Math3d.Vector4>
    {
        public const string AttributeName = "g3d:shape:color:0:float32:4";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<Vim.Math3d.Vector4>("g3d:shape:color:0:float32:4");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public Vim.Math3d.Vector4[] TypedData { get; set; }
            = Array.Empty<Vim.Math3d.Vector4>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class VimShapeWidthsAttribute : IAttribute<System.Single>
    {
        public const string AttributeName = "g3d:shape:width:0:float32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Single>("g3d:shape:width:0:float32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public System.Single[] TypedData { get; set; }
            = Array.Empty<System.Single>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class MaterialColorsAttribute : IAttribute<Vim.Math3d.Vector4>
    {
        public const string AttributeName = "g3d:material:color:0:float32:4";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<Vim.Math3d.Vector4>("g3d:material:color:0:float32:4");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public Vim.Math3d.Vector4[] TypedData { get; set; }
            = Array.Empty<Vim.Math3d.Vector4>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class MaterialGlossinessAttribute : IAttribute<System.Single>
    {
        public const string AttributeName = "g3d:material:glossiness:0:float32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Single>("g3d:material:glossiness:0:float32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public System.Single[] TypedData { get; set; }
            = Array.Empty<System.Single>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class MaterialSmoothnessAttribute : IAttribute<System.Single>
    {
        public const string AttributeName = "g3d:material:smoothness:0:float32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Single>("g3d:material:smoothness:0:float32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public System.Single[] TypedData { get; set; }
            = Array.Empty<System.Single>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class SceneChunkCountAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:chunk:count:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:chunk:count:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public System.Int32[] TypedData { get; set; }
            = Array.Empty<System.Int32>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class SceneInstanceMeshesAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:instance:mesh:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:instance:mesh:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public System.Int32[] TypedData { get; set; }
            = Array.Empty<System.Int32>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class SceneInstanceTransformsAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:instance:transform:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:instance:transform:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public System.Int32[] TypedData { get; set; }
            = Array.Empty<System.Int32>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class SceneInstanceNodesAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:instance:node:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:instance:node:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public System.Int32[] TypedData { get; set; }
            = Array.Empty<System.Int32>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class SceneInstanceGroupsAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:instance:group:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:instance:group:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public System.Int32[] TypedData { get; set; }
            = Array.Empty<System.Int32>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class SceneInstanceTagsAttribute : IAttribute<System.Int64>
    {
        public const string AttributeName = "g3d:instance:tag:0:int64:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int64>("g3d:instance:tag:0:int64:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public System.Int64[] TypedData { get; set; }
            = Array.Empty<System.Int64>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class SceneInstanceFlagsAttribute : IAttribute<System.UInt16>
    {
        public const string AttributeName = "g3d:instance:flags:0:uint16:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.UInt16>("g3d:instance:flags:0:uint16:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public System.UInt16[] TypedData { get; set; }
            = Array.Empty<System.UInt16>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class SceneInstanceMinsAttribute : IAttribute<Vim.Math3d.Vector3>
    {
        public const string AttributeName = "g3d:instance:min:0:float32:3";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<Vim.Math3d.Vector3>("g3d:instance:min:0:float32:3");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public Vim.Math3d.Vector3[] TypedData { get; set; }
            = Array.Empty<Vim.Math3d.Vector3>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class SceneInstanceMaxsAttribute : IAttribute<Vim.Math3d.Vector3>
    {
        public const string AttributeName = "g3d:instance:max:0:float32:3";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<Vim.Math3d.Vector3>("g3d:instance:max:0:float32:3");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public Vim.Math3d.Vector3[] TypedData { get; set; }
            = Array.Empty<Vim.Math3d.Vector3>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class SceneMeshChunksAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:mesh:chunk:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:mesh:chunk:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public System.Int32[] TypedData { get; set; }
            = Array.Empty<System.Int32>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class SceneMeshInstanceCountsAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:mesh:instancecount:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:mesh:instancecount:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public System.Int32[] TypedData { get; set; }
            = Array.Empty<System.Int32>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class SceneMeshVertexCountsAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:mesh:vertexcount:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:mesh:vertexcount:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public System.Int32[] TypedData { get; set; }
            = Array.Empty<System.Int32>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class SceneMeshIndexCountsAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:mesh:indexcount:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:mesh:indexcount:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public System.Int32[] TypedData { get; set; }
            = Array.Empty<System.Int32>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class SceneMeshOpaqueVertexCountsAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:mesh:opaquevertexcount:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:mesh:opaquevertexcount:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public System.Int32[] TypedData { get; set; }
            = Array.Empty<System.Int32>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class SceneMeshOpaqueIndexCountsAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:mesh:opaqueindexcount:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:mesh:opaqueindexcount:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public System.Int32[] TypedData { get; set; }
            = Array.Empty<System.Int32>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class MeshInstanceTransformsAttribute : IAttribute<Vim.Math3d.Matrix4x4>
    {
        public const string AttributeName = "g3d:instance:transform:0:float32:16";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<Vim.Math3d.Matrix4x4>("g3d:instance:transform:0:float32:16");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public Vim.Math3d.Matrix4x4[] TypedData { get; set; }
            = Array.Empty<Vim.Math3d.Matrix4x4>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class MeshOpaqueSubmeshCountsAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:mesh:opaquesubmeshcount:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:mesh:opaquesubmeshcount:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public System.Int32[] TypedData { get; set; }
            = Array.Empty<System.Int32>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class MeshSubmeshIndexOffsetsAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:submesh:indexoffset:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:submesh:indexoffset:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Index;

        public Type IndexInto { get; }
            = typeof(Vim.G3dNext.Attributes.MeshIndicesAttribute);

        public System.Int32[] TypedData { get; set; }
            = Array.Empty<System.Int32>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class MeshSubmeshVertexOffsetsAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:submesh:vertexoffset:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:submesh:vertexoffset:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Index;

        public Type IndexInto { get; }
            = typeof(Vim.G3dNext.Attributes.MeshIndicesAttribute);

        public System.Int32[] TypedData { get; set; }
            = Array.Empty<System.Int32>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class MeshSubmeshMaterialsAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:submesh:material:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:submesh:material:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Index;

        public Type IndexInto { get; }
            = null;

        public System.Int32[] TypedData { get; set; }
            = Array.Empty<System.Int32>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class MeshPositionsAttribute : IAttribute<Vim.Math3d.Vector3>
    {
        public const string AttributeName = "g3d:vertex:position:0:float32:3";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<Vim.Math3d.Vector3>("g3d:vertex:position:0:float32:3");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Data;

        public Type IndexInto { get; }
            = null;

        public Vim.Math3d.Vector3[] TypedData { get; set; }
            = Array.Empty<Vim.Math3d.Vector3>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    public partial class MeshIndicesAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:corner:index:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.SetArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:corner:index:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Index;

        public Type IndexInto { get; }
            = typeof(Vim.G3dNext.Attributes.MeshPositionsAttribute);

        public System.Int32[] TypedData { get; set; }
            = Array.Empty<System.Int32>();

        public Array Data
            => TypedData;

        public void Write(Stream stream)
        {
            if (TypedData == null || TypedData.Length == 0)
                return;
            stream.Write(TypedData);
        }
    }
    
    // Please provide an explicit implementation in another partial class file.
    public partial class G3dVim : ISetup
    {
        public VimAttributeCollection Attributes;

        public G3dVim() : this(new VimAttributeCollection())
        {
            // empty
        }

        public G3dVim(BFastNext bfast) : this(new VimAttributeCollection(bfast))
        {
            // empty
        }

        public G3dVim(VimAttributeCollection attributes)
        {
            Attributes = attributes;

            // Method to implement in another partial class
            (this as ISetup).Setup();
        }

        public BFastNext ToBFast()
            => Attributes.ToBFast();

        
        public Vim.Math3d.Vector3[] Positions
        {
            get => Attributes.Positions.TypedData;
            set => Attributes.Positions.TypedData = value;
        }

        public System.Int32[] Indices
        {
            get => Attributes.Indices.TypedData;
            set => Attributes.Indices.TypedData = value;
        }

        public Vim.Math3d.Matrix4x4[] InstanceTransforms
        {
            get => Attributes.InstanceTransforms.TypedData;
            set => Attributes.InstanceTransforms.TypedData = value;
        }

        public System.Int32[] InstanceParents
        {
            get => Attributes.InstanceParents.TypedData;
            set => Attributes.InstanceParents.TypedData = value;
        }

        public System.UInt16[] InstanceFlags
        {
            get => Attributes.InstanceFlags.TypedData;
            set => Attributes.InstanceFlags.TypedData = value;
        }

        public System.Int32[] InstanceMeshes
        {
            get => Attributes.InstanceMeshes.TypedData;
            set => Attributes.InstanceMeshes.TypedData = value;
        }

        public System.Int32[] MeshSubmeshOffsets
        {
            get => Attributes.MeshSubmeshOffsets.TypedData;
            set => Attributes.MeshSubmeshOffsets.TypedData = value;
        }

        public System.Int32[] SubmeshIndexOffsets
        {
            get => Attributes.SubmeshIndexOffsets.TypedData;
            set => Attributes.SubmeshIndexOffsets.TypedData = value;
        }

        public System.Int32[] SubmeshMaterials
        {
            get => Attributes.SubmeshMaterials.TypedData;
            set => Attributes.SubmeshMaterials.TypedData = value;
        }

        public Vim.Math3d.Vector4[] MaterialColors
        {
            get => Attributes.MaterialColors.TypedData;
            set => Attributes.MaterialColors.TypedData = value;
        }

        public System.Single[] MaterialGlossiness
        {
            get => Attributes.MaterialGlossiness.TypedData;
            set => Attributes.MaterialGlossiness.TypedData = value;
        }

        public System.Single[] MaterialSmoothness
        {
            get => Attributes.MaterialSmoothness.TypedData;
            set => Attributes.MaterialSmoothness.TypedData = value;
        }

        public Vim.Math3d.Vector3[] ShapeVertices
        {
            get => Attributes.ShapeVertices.TypedData;
            set => Attributes.ShapeVertices.TypedData = value;
        }

        public System.Int32[] ShapeVertexOffsets
        {
            get => Attributes.ShapeVertexOffsets.TypedData;
            set => Attributes.ShapeVertexOffsets.TypedData = value;
        }

        public Vim.Math3d.Vector4[] ShapeColors
        {
            get => Attributes.ShapeColors.TypedData;
            set => Attributes.ShapeColors.TypedData = value;
        }

        public System.Single[] ShapeWidths
        {
            get => Attributes.ShapeWidths.TypedData;
            set => Attributes.ShapeWidths.TypedData = value;
        }
    }

    
    public partial class VimAttributeCollection : IAttributeCollection
    {
        public VimAttributeCollection()
        {
            // empty
        }

        public VimAttributeCollection(BFastNext bfast)
        {
            this.ReadAttributes(bfast);
        }

        public void ReadAttributes(BFastNext bfast)
        {
            foreach (var attribute in Map.Values)
            {
                attribute.ReadBFast(bfast);
            }
        }

        public IDictionary<string, IAttribute> Map { get; }
            = new Dictionary<string, IAttribute>
            {
                [Vim.G3dNext.Attributes.VimPositionsAttribute.AttributeName] = new Vim.G3dNext.Attributes.VimPositionsAttribute(),
                [Vim.G3dNext.Attributes.VimIndicesAttribute.AttributeName] = new Vim.G3dNext.Attributes.VimIndicesAttribute(),
                [Vim.G3dNext.Attributes.VimInstanceTransformsAttribute.AttributeName] = new Vim.G3dNext.Attributes.VimInstanceTransformsAttribute(),
                [Vim.G3dNext.Attributes.VimInstanceParentsAttribute.AttributeName] = new Vim.G3dNext.Attributes.VimInstanceParentsAttribute(),
                [Vim.G3dNext.Attributes.VimInstanceFlagsAttribute.AttributeName] = new Vim.G3dNext.Attributes.VimInstanceFlagsAttribute(),
                [Vim.G3dNext.Attributes.VimInstanceMeshesAttribute.AttributeName] = new Vim.G3dNext.Attributes.VimInstanceMeshesAttribute(),
                [Vim.G3dNext.Attributes.VimMeshSubmeshOffsetsAttribute.AttributeName] = new Vim.G3dNext.Attributes.VimMeshSubmeshOffsetsAttribute(),
                [Vim.G3dNext.Attributes.VimSubmeshIndexOffsetsAttribute.AttributeName] = new Vim.G3dNext.Attributes.VimSubmeshIndexOffsetsAttribute(),
                [Vim.G3dNext.Attributes.VimSubmeshMaterialsAttribute.AttributeName] = new Vim.G3dNext.Attributes.VimSubmeshMaterialsAttribute(),
                [Vim.G3dNext.Attributes.VimMaterialColorsAttribute.AttributeName] = new Vim.G3dNext.Attributes.VimMaterialColorsAttribute(),
                [Vim.G3dNext.Attributes.VimMaterialGlossinessAttribute.AttributeName] = new Vim.G3dNext.Attributes.VimMaterialGlossinessAttribute(),
                [Vim.G3dNext.Attributes.VimMaterialSmoothnessAttribute.AttributeName] = new Vim.G3dNext.Attributes.VimMaterialSmoothnessAttribute(),
                [Vim.G3dNext.Attributes.VimShapeVerticesAttribute.AttributeName] = new Vim.G3dNext.Attributes.VimShapeVerticesAttribute(),
                [Vim.G3dNext.Attributes.VimShapeVertexOffsetsAttribute.AttributeName] = new Vim.G3dNext.Attributes.VimShapeVertexOffsetsAttribute(),
                [Vim.G3dNext.Attributes.VimShapeColorsAttribute.AttributeName] = new Vim.G3dNext.Attributes.VimShapeColorsAttribute(),
                [Vim.G3dNext.Attributes.VimShapeWidthsAttribute.AttributeName] = new Vim.G3dNext.Attributes.VimShapeWidthsAttribute(),
            };

        // Named Attributes.

        public Vim.G3dNext.Attributes.VimPositionsAttribute Positions
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.VimPositionsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.VimPositionsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.VimPositionsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.VimIndicesAttribute Indices
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.VimIndicesAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.VimIndicesAttribute : default;
            set => Map[Vim.G3dNext.Attributes.VimIndicesAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.VimInstanceTransformsAttribute InstanceTransforms
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.VimInstanceTransformsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.VimInstanceTransformsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.VimInstanceTransformsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.VimInstanceParentsAttribute InstanceParents
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.VimInstanceParentsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.VimInstanceParentsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.VimInstanceParentsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.VimInstanceFlagsAttribute InstanceFlags
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.VimInstanceFlagsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.VimInstanceFlagsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.VimInstanceFlagsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.VimInstanceMeshesAttribute InstanceMeshes
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.VimInstanceMeshesAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.VimInstanceMeshesAttribute : default;
            set => Map[Vim.G3dNext.Attributes.VimInstanceMeshesAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.VimMeshSubmeshOffsetsAttribute MeshSubmeshOffsets
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.VimMeshSubmeshOffsetsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.VimMeshSubmeshOffsetsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.VimMeshSubmeshOffsetsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.VimSubmeshIndexOffsetsAttribute SubmeshIndexOffsets
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.VimSubmeshIndexOffsetsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.VimSubmeshIndexOffsetsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.VimSubmeshIndexOffsetsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.VimSubmeshMaterialsAttribute SubmeshMaterials
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.VimSubmeshMaterialsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.VimSubmeshMaterialsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.VimSubmeshMaterialsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.VimMaterialColorsAttribute MaterialColors
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.VimMaterialColorsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.VimMaterialColorsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.VimMaterialColorsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.VimMaterialGlossinessAttribute MaterialGlossiness
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.VimMaterialGlossinessAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.VimMaterialGlossinessAttribute : default;
            set => Map[Vim.G3dNext.Attributes.VimMaterialGlossinessAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.VimMaterialSmoothnessAttribute MaterialSmoothness
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.VimMaterialSmoothnessAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.VimMaterialSmoothnessAttribute : default;
            set => Map[Vim.G3dNext.Attributes.VimMaterialSmoothnessAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.VimShapeVerticesAttribute ShapeVertices
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.VimShapeVerticesAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.VimShapeVerticesAttribute : default;
            set => Map[Vim.G3dNext.Attributes.VimShapeVerticesAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.VimShapeVertexOffsetsAttribute ShapeVertexOffsets
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.VimShapeVertexOffsetsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.VimShapeVertexOffsetsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.VimShapeVertexOffsetsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.VimShapeColorsAttribute ShapeColors
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.VimShapeColorsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.VimShapeColorsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.VimShapeColorsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.VimShapeWidthsAttribute ShapeWidths
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.VimShapeWidthsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.VimShapeWidthsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.VimShapeWidthsAttribute.AttributeName] = value as IAttribute;
        }

        /// <inheritdoc/>
        public IAttribute GetAttribute(Type attributeType)
        {


                if (attributeType == typeof(Vim.G3dNext.Attributes.VimPositionsAttribute))
                    return Positions;


                if (attributeType == typeof(Vim.G3dNext.Attributes.VimIndicesAttribute))
                    return Indices;


                if (attributeType == typeof(Vim.G3dNext.Attributes.VimInstanceTransformsAttribute))
                    return InstanceTransforms;


                if (attributeType == typeof(Vim.G3dNext.Attributes.VimInstanceParentsAttribute))
                    return InstanceParents;


                if (attributeType == typeof(Vim.G3dNext.Attributes.VimInstanceFlagsAttribute))
                    return InstanceFlags;


                if (attributeType == typeof(Vim.G3dNext.Attributes.VimInstanceMeshesAttribute))
                    return InstanceMeshes;


                if (attributeType == typeof(Vim.G3dNext.Attributes.VimMeshSubmeshOffsetsAttribute))
                    return MeshSubmeshOffsets;


                if (attributeType == typeof(Vim.G3dNext.Attributes.VimSubmeshIndexOffsetsAttribute))
                    return SubmeshIndexOffsets;


                if (attributeType == typeof(Vim.G3dNext.Attributes.VimSubmeshMaterialsAttribute))
                    return SubmeshMaterials;


                if (attributeType == typeof(Vim.G3dNext.Attributes.VimMaterialColorsAttribute))
                    return MaterialColors;


                if (attributeType == typeof(Vim.G3dNext.Attributes.VimMaterialGlossinessAttribute))
                    return MaterialGlossiness;


                if (attributeType == typeof(Vim.G3dNext.Attributes.VimMaterialSmoothnessAttribute))
                    return MaterialSmoothness;


                if (attributeType == typeof(Vim.G3dNext.Attributes.VimShapeVerticesAttribute))
                    return ShapeVertices;


                if (attributeType == typeof(Vim.G3dNext.Attributes.VimShapeVertexOffsetsAttribute))
                    return ShapeVertexOffsets;


                if (attributeType == typeof(Vim.G3dNext.Attributes.VimShapeColorsAttribute))
                    return ShapeColors;


                if (attributeType == typeof(Vim.G3dNext.Attributes.VimShapeWidthsAttribute))
                    return ShapeWidths;

            throw new ArgumentException("Type {attributeType.ToString()} is not supported.");
        }

        public IAttribute MergeAttribute(string attributeName, IReadOnlyList<IAttributeCollection> otherCollections)
        {
            var collections = otherCollections.Prepend(this).ToArray();
            switch (attributeName)
            {

                case Vim.G3dNext.Attributes.VimPositionsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.VimPositionsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.VimPositionsAttribute, Vim.Math3d.Vector3>();
                }

                case Vim.G3dNext.Attributes.VimIndicesAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.VimIndicesAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.VimInstanceTransformsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.VimInstanceTransformsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.VimInstanceTransformsAttribute, Vim.Math3d.Matrix4x4>();
                }

                case Vim.G3dNext.Attributes.VimInstanceParentsAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.VimInstanceParentsAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.VimInstanceFlagsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.VimInstanceFlagsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.VimInstanceFlagsAttribute, System.UInt16>();
                }

                case Vim.G3dNext.Attributes.VimInstanceMeshesAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.VimInstanceMeshesAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.VimMeshSubmeshOffsetsAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.VimMeshSubmeshOffsetsAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.VimSubmeshIndexOffsetsAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.VimSubmeshIndexOffsetsAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.VimSubmeshMaterialsAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.VimSubmeshMaterialsAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.VimMaterialColorsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.VimMaterialColorsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.VimMaterialColorsAttribute, Vim.Math3d.Vector4>();
                }

                case Vim.G3dNext.Attributes.VimMaterialGlossinessAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.VimMaterialGlossinessAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.VimMaterialGlossinessAttribute, System.Single>();
                }

                case Vim.G3dNext.Attributes.VimMaterialSmoothnessAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.VimMaterialSmoothnessAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.VimMaterialSmoothnessAttribute, System.Single>();
                }

                case Vim.G3dNext.Attributes.VimShapeVerticesAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.VimShapeVerticesAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.VimShapeVerticesAttribute, Vim.Math3d.Vector3>();
                }

                case Vim.G3dNext.Attributes.VimShapeVertexOffsetsAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.VimShapeVertexOffsetsAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.VimShapeColorsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.VimShapeColorsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.VimShapeColorsAttribute, Vim.Math3d.Vector4>();
                }

                case Vim.G3dNext.Attributes.VimShapeWidthsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.VimShapeWidthsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.VimShapeWidthsAttribute, System.Single>();
                }

                default:
                    throw new ArgumentException(nameof(attributeName));
            }
        }

        public void Validate() 
        {
            // Ensure all the indices are either -1 or within the bounds of the attributes they are indexing into.

            {
                var maxIndex = GetAttribute(Indices.IndexInto).Data.Length - 1;
                for (var i = 0; i < Indices.TypedData.Length; ++i)
                {
                    var index = Indices.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.VimIndicesAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(InstanceParents.IndexInto).Data.Length - 1;
                for (var i = 0; i < InstanceParents.TypedData.Length; ++i)
                {
                    var index = InstanceParents.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.VimInstanceParentsAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(InstanceMeshes.IndexInto).Data.Length - 1;
                for (var i = 0; i < InstanceMeshes.TypedData.Length; ++i)
                {
                    var index = InstanceMeshes.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.VimInstanceMeshesAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(MeshSubmeshOffsets.IndexInto).Data.Length - 1;
                for (var i = 0; i < MeshSubmeshOffsets.TypedData.Length; ++i)
                {
                    var index = MeshSubmeshOffsets.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.VimMeshSubmeshOffsetsAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(SubmeshIndexOffsets.IndexInto).Data.Length - 1;
                for (var i = 0; i < SubmeshIndexOffsets.TypedData.Length; ++i)
                {
                    var index = SubmeshIndexOffsets.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.VimSubmeshIndexOffsetsAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(SubmeshMaterials.IndexInto).Data.Length - 1;
                for (var i = 0; i < SubmeshMaterials.TypedData.Length; ++i)
                {
                    var index = SubmeshMaterials.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.VimSubmeshMaterialsAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(ShapeVertexOffsets.IndexInto).Data.Length - 1;
                for (var i = 0; i < ShapeVertexOffsets.TypedData.Length; ++i)
                {
                    var index = ShapeVertexOffsets.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.VimShapeVertexOffsetsAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }
        }
    }
    
    // Please provide an explicit implementation in another partial class file.
    public partial class G3dMaterials : ISetup
    {
        public MaterialsAttributeCollection Attributes;

        public G3dMaterials() : this(new MaterialsAttributeCollection())
        {
            // empty
        }

        public G3dMaterials(BFastNext bfast) : this(new MaterialsAttributeCollection(bfast))
        {
            // empty
        }

        public G3dMaterials(MaterialsAttributeCollection attributes)
        {
            Attributes = attributes;

            // Method to implement in another partial class
            (this as ISetup).Setup();
        }

        public BFastNext ToBFast()
            => Attributes.ToBFast();

        
        public Vim.Math3d.Vector4[] MaterialColors
        {
            get => Attributes.MaterialColors.TypedData;
            set => Attributes.MaterialColors.TypedData = value;
        }

        public System.Single[] MaterialGlossiness
        {
            get => Attributes.MaterialGlossiness.TypedData;
            set => Attributes.MaterialGlossiness.TypedData = value;
        }

        public System.Single[] MaterialSmoothness
        {
            get => Attributes.MaterialSmoothness.TypedData;
            set => Attributes.MaterialSmoothness.TypedData = value;
        }
    }

    
    public partial class MaterialsAttributeCollection : IAttributeCollection
    {
        public MaterialsAttributeCollection()
        {
            // empty
        }

        public MaterialsAttributeCollection(BFastNext bfast)
        {
            this.ReadAttributes(bfast);
        }

        public void ReadAttributes(BFastNext bfast)
        {
            foreach (var attribute in Map.Values)
            {
                attribute.ReadBFast(bfast);
            }
        }

        public IDictionary<string, IAttribute> Map { get; }
            = new Dictionary<string, IAttribute>
            {
                [Vim.G3dNext.Attributes.MaterialColorsAttribute.AttributeName] = new Vim.G3dNext.Attributes.MaterialColorsAttribute(),
                [Vim.G3dNext.Attributes.MaterialGlossinessAttribute.AttributeName] = new Vim.G3dNext.Attributes.MaterialGlossinessAttribute(),
                [Vim.G3dNext.Attributes.MaterialSmoothnessAttribute.AttributeName] = new Vim.G3dNext.Attributes.MaterialSmoothnessAttribute(),
            };

        // Named Attributes.

        public Vim.G3dNext.Attributes.MaterialColorsAttribute MaterialColors
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MaterialColorsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MaterialColorsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MaterialColorsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MaterialGlossinessAttribute MaterialGlossiness
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MaterialGlossinessAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MaterialGlossinessAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MaterialGlossinessAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MaterialSmoothnessAttribute MaterialSmoothness
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MaterialSmoothnessAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MaterialSmoothnessAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MaterialSmoothnessAttribute.AttributeName] = value as IAttribute;
        }

        /// <inheritdoc/>
        public IAttribute GetAttribute(Type attributeType)
        {


                if (attributeType == typeof(Vim.G3dNext.Attributes.MaterialColorsAttribute))
                    return MaterialColors;


                if (attributeType == typeof(Vim.G3dNext.Attributes.MaterialGlossinessAttribute))
                    return MaterialGlossiness;


                if (attributeType == typeof(Vim.G3dNext.Attributes.MaterialSmoothnessAttribute))
                    return MaterialSmoothness;

            throw new ArgumentException("Type {attributeType.ToString()} is not supported.");
        }

        public IAttribute MergeAttribute(string attributeName, IReadOnlyList<IAttributeCollection> otherCollections)
        {
            var collections = otherCollections.Prepend(this).ToArray();
            switch (attributeName)
            {

                case Vim.G3dNext.Attributes.MaterialColorsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.MaterialColorsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.MaterialColorsAttribute, Vim.Math3d.Vector4>();
                }

                case Vim.G3dNext.Attributes.MaterialGlossinessAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.MaterialGlossinessAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.MaterialGlossinessAttribute, System.Single>();
                }

                case Vim.G3dNext.Attributes.MaterialSmoothnessAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.MaterialSmoothnessAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.MaterialSmoothnessAttribute, System.Single>();
                }

                default:
                    throw new ArgumentException(nameof(attributeName));
            }
        }

        public void Validate() 
        {
            // Ensure all the indices are either -1 or within the bounds of the attributes they are indexing into.

        }
    }
    
    // Please provide an explicit implementation in another partial class file.
    public partial class G3dScene : ISetup
    {
        public SceneAttributeCollection Attributes;

        public G3dScene() : this(new SceneAttributeCollection())
        {
            // empty
        }

        public G3dScene(BFastNext bfast) : this(new SceneAttributeCollection(bfast))
        {
            // empty
        }

        public G3dScene(SceneAttributeCollection attributes)
        {
            Attributes = attributes;

            // Method to implement in another partial class
            (this as ISetup).Setup();
        }

        public BFastNext ToBFast()
            => Attributes.ToBFast();

        
        public System.Int32[] ChunkCount
        {
            get => Attributes.ChunkCount.TypedData;
            set => Attributes.ChunkCount.TypedData = value;
        }

        public System.Int32[] InstanceMeshes
        {
            get => Attributes.InstanceMeshes.TypedData;
            set => Attributes.InstanceMeshes.TypedData = value;
        }

        public System.Int32[] InstanceTransforms
        {
            get => Attributes.InstanceTransforms.TypedData;
            set => Attributes.InstanceTransforms.TypedData = value;
        }

        public System.Int32[] InstanceNodes
        {
            get => Attributes.InstanceNodes.TypedData;
            set => Attributes.InstanceNodes.TypedData = value;
        }

        public System.Int32[] InstanceGroups
        {
            get => Attributes.InstanceGroups.TypedData;
            set => Attributes.InstanceGroups.TypedData = value;
        }

        public System.Int64[] InstanceTags
        {
            get => Attributes.InstanceTags.TypedData;
            set => Attributes.InstanceTags.TypedData = value;
        }

        public System.UInt16[] InstanceFlags
        {
            get => Attributes.InstanceFlags.TypedData;
            set => Attributes.InstanceFlags.TypedData = value;
        }

        public Vim.Math3d.Vector3[] InstanceMins
        {
            get => Attributes.InstanceMins.TypedData;
            set => Attributes.InstanceMins.TypedData = value;
        }

        public Vim.Math3d.Vector3[] InstanceMaxs
        {
            get => Attributes.InstanceMaxs.TypedData;
            set => Attributes.InstanceMaxs.TypedData = value;
        }

        public System.Int32[] MeshChunks
        {
            get => Attributes.MeshChunks.TypedData;
            set => Attributes.MeshChunks.TypedData = value;
        }

        public System.Int32[] MeshInstanceCounts
        {
            get => Attributes.MeshInstanceCounts.TypedData;
            set => Attributes.MeshInstanceCounts.TypedData = value;
        }

        public System.Int32[] MeshIndexCounts
        {
            get => Attributes.MeshIndexCounts.TypedData;
            set => Attributes.MeshIndexCounts.TypedData = value;
        }

        public System.Int32[] MeshVertexCounts
        {
            get => Attributes.MeshVertexCounts.TypedData;
            set => Attributes.MeshVertexCounts.TypedData = value;
        }

        public System.Int32[] MeshOpaqueIndexCounts
        {
            get => Attributes.MeshOpaqueIndexCounts.TypedData;
            set => Attributes.MeshOpaqueIndexCounts.TypedData = value;
        }

        public System.Int32[] MeshOpaqueVertexCounts
        {
            get => Attributes.MeshOpaqueVertexCounts.TypedData;
            set => Attributes.MeshOpaqueVertexCounts.TypedData = value;
        }
    }

    
    public partial class SceneAttributeCollection : IAttributeCollection
    {
        public SceneAttributeCollection()
        {
            // empty
        }

        public SceneAttributeCollection(BFastNext bfast)
        {
            this.ReadAttributes(bfast);
        }

        public void ReadAttributes(BFastNext bfast)
        {
            foreach (var attribute in Map.Values)
            {
                attribute.ReadBFast(bfast);
            }
        }

        public IDictionary<string, IAttribute> Map { get; }
            = new Dictionary<string, IAttribute>
            {
                [Vim.G3dNext.Attributes.SceneChunkCountAttribute.AttributeName] = new Vim.G3dNext.Attributes.SceneChunkCountAttribute(),
                [Vim.G3dNext.Attributes.SceneInstanceMeshesAttribute.AttributeName] = new Vim.G3dNext.Attributes.SceneInstanceMeshesAttribute(),
                [Vim.G3dNext.Attributes.SceneInstanceTransformsAttribute.AttributeName] = new Vim.G3dNext.Attributes.SceneInstanceTransformsAttribute(),
                [Vim.G3dNext.Attributes.SceneInstanceNodesAttribute.AttributeName] = new Vim.G3dNext.Attributes.SceneInstanceNodesAttribute(),
                [Vim.G3dNext.Attributes.SceneInstanceGroupsAttribute.AttributeName] = new Vim.G3dNext.Attributes.SceneInstanceGroupsAttribute(),
                [Vim.G3dNext.Attributes.SceneInstanceTagsAttribute.AttributeName] = new Vim.G3dNext.Attributes.SceneInstanceTagsAttribute(),
                [Vim.G3dNext.Attributes.SceneInstanceFlagsAttribute.AttributeName] = new Vim.G3dNext.Attributes.SceneInstanceFlagsAttribute(),
                [Vim.G3dNext.Attributes.SceneInstanceMinsAttribute.AttributeName] = new Vim.G3dNext.Attributes.SceneInstanceMinsAttribute(),
                [Vim.G3dNext.Attributes.SceneInstanceMaxsAttribute.AttributeName] = new Vim.G3dNext.Attributes.SceneInstanceMaxsAttribute(),
                [Vim.G3dNext.Attributes.SceneMeshChunksAttribute.AttributeName] = new Vim.G3dNext.Attributes.SceneMeshChunksAttribute(),
                [Vim.G3dNext.Attributes.SceneMeshInstanceCountsAttribute.AttributeName] = new Vim.G3dNext.Attributes.SceneMeshInstanceCountsAttribute(),
                [Vim.G3dNext.Attributes.SceneMeshIndexCountsAttribute.AttributeName] = new Vim.G3dNext.Attributes.SceneMeshIndexCountsAttribute(),
                [Vim.G3dNext.Attributes.SceneMeshVertexCountsAttribute.AttributeName] = new Vim.G3dNext.Attributes.SceneMeshVertexCountsAttribute(),
                [Vim.G3dNext.Attributes.SceneMeshOpaqueIndexCountsAttribute.AttributeName] = new Vim.G3dNext.Attributes.SceneMeshOpaqueIndexCountsAttribute(),
                [Vim.G3dNext.Attributes.SceneMeshOpaqueVertexCountsAttribute.AttributeName] = new Vim.G3dNext.Attributes.SceneMeshOpaqueVertexCountsAttribute(),
            };

        // Named Attributes.

        public Vim.G3dNext.Attributes.SceneChunkCountAttribute ChunkCount
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.SceneChunkCountAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.SceneChunkCountAttribute : default;
            set => Map[Vim.G3dNext.Attributes.SceneChunkCountAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.SceneInstanceMeshesAttribute InstanceMeshes
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.SceneInstanceMeshesAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.SceneInstanceMeshesAttribute : default;
            set => Map[Vim.G3dNext.Attributes.SceneInstanceMeshesAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.SceneInstanceTransformsAttribute InstanceTransforms
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.SceneInstanceTransformsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.SceneInstanceTransformsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.SceneInstanceTransformsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.SceneInstanceNodesAttribute InstanceNodes
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.SceneInstanceNodesAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.SceneInstanceNodesAttribute : default;
            set => Map[Vim.G3dNext.Attributes.SceneInstanceNodesAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.SceneInstanceGroupsAttribute InstanceGroups
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.SceneInstanceGroupsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.SceneInstanceGroupsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.SceneInstanceGroupsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.SceneInstanceTagsAttribute InstanceTags
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.SceneInstanceTagsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.SceneInstanceTagsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.SceneInstanceTagsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.SceneInstanceFlagsAttribute InstanceFlags
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.SceneInstanceFlagsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.SceneInstanceFlagsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.SceneInstanceFlagsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.SceneInstanceMinsAttribute InstanceMins
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.SceneInstanceMinsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.SceneInstanceMinsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.SceneInstanceMinsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.SceneInstanceMaxsAttribute InstanceMaxs
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.SceneInstanceMaxsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.SceneInstanceMaxsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.SceneInstanceMaxsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.SceneMeshChunksAttribute MeshChunks
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.SceneMeshChunksAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.SceneMeshChunksAttribute : default;
            set => Map[Vim.G3dNext.Attributes.SceneMeshChunksAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.SceneMeshInstanceCountsAttribute MeshInstanceCounts
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.SceneMeshInstanceCountsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.SceneMeshInstanceCountsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.SceneMeshInstanceCountsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.SceneMeshIndexCountsAttribute MeshIndexCounts
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.SceneMeshIndexCountsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.SceneMeshIndexCountsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.SceneMeshIndexCountsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.SceneMeshVertexCountsAttribute MeshVertexCounts
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.SceneMeshVertexCountsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.SceneMeshVertexCountsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.SceneMeshVertexCountsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.SceneMeshOpaqueIndexCountsAttribute MeshOpaqueIndexCounts
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.SceneMeshOpaqueIndexCountsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.SceneMeshOpaqueIndexCountsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.SceneMeshOpaqueIndexCountsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.SceneMeshOpaqueVertexCountsAttribute MeshOpaqueVertexCounts
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.SceneMeshOpaqueVertexCountsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.SceneMeshOpaqueVertexCountsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.SceneMeshOpaqueVertexCountsAttribute.AttributeName] = value as IAttribute;
        }

        /// <inheritdoc/>
        public IAttribute GetAttribute(Type attributeType)
        {


                if (attributeType == typeof(Vim.G3dNext.Attributes.SceneChunkCountAttribute))
                    return ChunkCount;


                if (attributeType == typeof(Vim.G3dNext.Attributes.SceneInstanceMeshesAttribute))
                    return InstanceMeshes;


                if (attributeType == typeof(Vim.G3dNext.Attributes.SceneInstanceTransformsAttribute))
                    return InstanceTransforms;


                if (attributeType == typeof(Vim.G3dNext.Attributes.SceneInstanceNodesAttribute))
                    return InstanceNodes;


                if (attributeType == typeof(Vim.G3dNext.Attributes.SceneInstanceGroupsAttribute))
                    return InstanceGroups;


                if (attributeType == typeof(Vim.G3dNext.Attributes.SceneInstanceTagsAttribute))
                    return InstanceTags;


                if (attributeType == typeof(Vim.G3dNext.Attributes.SceneInstanceFlagsAttribute))
                    return InstanceFlags;


                if (attributeType == typeof(Vim.G3dNext.Attributes.SceneInstanceMinsAttribute))
                    return InstanceMins;


                if (attributeType == typeof(Vim.G3dNext.Attributes.SceneInstanceMaxsAttribute))
                    return InstanceMaxs;


                if (attributeType == typeof(Vim.G3dNext.Attributes.SceneMeshChunksAttribute))
                    return MeshChunks;


                if (attributeType == typeof(Vim.G3dNext.Attributes.SceneMeshInstanceCountsAttribute))
                    return MeshInstanceCounts;


                if (attributeType == typeof(Vim.G3dNext.Attributes.SceneMeshIndexCountsAttribute))
                    return MeshIndexCounts;


                if (attributeType == typeof(Vim.G3dNext.Attributes.SceneMeshVertexCountsAttribute))
                    return MeshVertexCounts;


                if (attributeType == typeof(Vim.G3dNext.Attributes.SceneMeshOpaqueIndexCountsAttribute))
                    return MeshOpaqueIndexCounts;


                if (attributeType == typeof(Vim.G3dNext.Attributes.SceneMeshOpaqueVertexCountsAttribute))
                    return MeshOpaqueVertexCounts;

            throw new ArgumentException("Type {attributeType.ToString()} is not supported.");
        }

        public IAttribute MergeAttribute(string attributeName, IReadOnlyList<IAttributeCollection> otherCollections)
        {
            var collections = otherCollections.Prepend(this).ToArray();
            switch (attributeName)
            {

                case Vim.G3dNext.Attributes.SceneChunkCountAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.SceneChunkCountAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.SceneChunkCountAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.SceneInstanceMeshesAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.SceneInstanceMeshesAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.SceneInstanceMeshesAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.SceneInstanceTransformsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.SceneInstanceTransformsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.SceneInstanceTransformsAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.SceneInstanceNodesAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.SceneInstanceNodesAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.SceneInstanceNodesAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.SceneInstanceGroupsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.SceneInstanceGroupsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.SceneInstanceGroupsAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.SceneInstanceTagsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.SceneInstanceTagsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.SceneInstanceTagsAttribute, System.Int64>();
                }

                case Vim.G3dNext.Attributes.SceneInstanceFlagsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.SceneInstanceFlagsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.SceneInstanceFlagsAttribute, System.UInt16>();
                }

                case Vim.G3dNext.Attributes.SceneInstanceMinsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.SceneInstanceMinsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.SceneInstanceMinsAttribute, Vim.Math3d.Vector3>();
                }

                case Vim.G3dNext.Attributes.SceneInstanceMaxsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.SceneInstanceMaxsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.SceneInstanceMaxsAttribute, Vim.Math3d.Vector3>();
                }

                case Vim.G3dNext.Attributes.SceneMeshChunksAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.SceneMeshChunksAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.SceneMeshChunksAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.SceneMeshInstanceCountsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.SceneMeshInstanceCountsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.SceneMeshInstanceCountsAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.SceneMeshIndexCountsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.SceneMeshIndexCountsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.SceneMeshIndexCountsAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.SceneMeshVertexCountsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.SceneMeshVertexCountsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.SceneMeshVertexCountsAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.SceneMeshOpaqueIndexCountsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.SceneMeshOpaqueIndexCountsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.SceneMeshOpaqueIndexCountsAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.SceneMeshOpaqueVertexCountsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.SceneMeshOpaqueVertexCountsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.SceneMeshOpaqueVertexCountsAttribute, System.Int32>();
                }

                default:
                    throw new ArgumentException(nameof(attributeName));
            }
        }

        public void Validate() 
        {
            // Ensure all the indices are either -1 or within the bounds of the attributes they are indexing into.

        }
    }
    
    // Please provide an explicit implementation in another partial class file.
    public partial class G3dMesh : ISetup
    {
        public MeshAttributeCollection Attributes;

        public G3dMesh() : this(new MeshAttributeCollection())
        {
            // empty
        }

        public G3dMesh(BFastNext bfast) : this(new MeshAttributeCollection(bfast))
        {
            // empty
        }

        public G3dMesh(MeshAttributeCollection attributes)
        {
            Attributes = attributes;

            // Method to implement in another partial class
            (this as ISetup).Setup();
        }

        public BFastNext ToBFast()
            => Attributes.ToBFast();

        
        public Vim.Math3d.Matrix4x4[] InstanceTransforms
        {
            get => Attributes.InstanceTransforms.TypedData;
            set => Attributes.InstanceTransforms.TypedData = value;
        }

        public System.Int32[] OpaqueSubmeshCounts
        {
            get => Attributes.OpaqueSubmeshCounts.TypedData;
            set => Attributes.OpaqueSubmeshCounts.TypedData = value;
        }

        public System.Int32[] SubmeshIndexOffsets
        {
            get => Attributes.SubmeshIndexOffsets.TypedData;
            set => Attributes.SubmeshIndexOffsets.TypedData = value;
        }

        public System.Int32[] SubmeshVertexOffsets
        {
            get => Attributes.SubmeshVertexOffsets.TypedData;
            set => Attributes.SubmeshVertexOffsets.TypedData = value;
        }

        public System.Int32[] SubmeshMaterials
        {
            get => Attributes.SubmeshMaterials.TypedData;
            set => Attributes.SubmeshMaterials.TypedData = value;
        }

        public Vim.Math3d.Vector3[] Positions
        {
            get => Attributes.Positions.TypedData;
            set => Attributes.Positions.TypedData = value;
        }

        public System.Int32[] Indices
        {
            get => Attributes.Indices.TypedData;
            set => Attributes.Indices.TypedData = value;
        }
    }

    
    public partial class MeshAttributeCollection : IAttributeCollection
    {
        public MeshAttributeCollection()
        {
            // empty
        }

        public MeshAttributeCollection(BFastNext bfast)
        {
            this.ReadAttributes(bfast);
        }

        public void ReadAttributes(BFastNext bfast)
        {
            foreach (var attribute in Map.Values)
            {
                attribute.ReadBFast(bfast);
            }
        }

        public IDictionary<string, IAttribute> Map { get; }
            = new Dictionary<string, IAttribute>
            {
                [Vim.G3dNext.Attributes.MeshInstanceTransformsAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshInstanceTransformsAttribute(),
                [Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountsAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountsAttribute(),
                [Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetsAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetsAttribute(),
                [Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetsAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetsAttribute(),
                [Vim.G3dNext.Attributes.MeshSubmeshMaterialsAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshSubmeshMaterialsAttribute(),
                [Vim.G3dNext.Attributes.MeshPositionsAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshPositionsAttribute(),
                [Vim.G3dNext.Attributes.MeshIndicesAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshIndicesAttribute(),
            };

        // Named Attributes.

        public Vim.G3dNext.Attributes.MeshInstanceTransformsAttribute InstanceTransforms
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MeshInstanceTransformsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshInstanceTransformsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MeshInstanceTransformsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountsAttribute OpaqueSubmeshCounts
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetsAttribute SubmeshIndexOffsets
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetsAttribute SubmeshVertexOffsets
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshSubmeshMaterialsAttribute SubmeshMaterials
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MeshSubmeshMaterialsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshSubmeshMaterialsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MeshSubmeshMaterialsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshPositionsAttribute Positions
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MeshPositionsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshPositionsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MeshPositionsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshIndicesAttribute Indices
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MeshIndicesAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshIndicesAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MeshIndicesAttribute.AttributeName] = value as IAttribute;
        }

        /// <inheritdoc/>
        public IAttribute GetAttribute(Type attributeType)
        {


                if (attributeType == typeof(Vim.G3dNext.Attributes.MeshInstanceTransformsAttribute))
                    return InstanceTransforms;


                if (attributeType == typeof(Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountsAttribute))
                    return OpaqueSubmeshCounts;


                if (attributeType == typeof(Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetsAttribute))
                    return SubmeshIndexOffsets;


                if (attributeType == typeof(Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetsAttribute))
                    return SubmeshVertexOffsets;


                if (attributeType == typeof(Vim.G3dNext.Attributes.MeshSubmeshMaterialsAttribute))
                    return SubmeshMaterials;


                if (attributeType == typeof(Vim.G3dNext.Attributes.MeshPositionsAttribute))
                    return Positions;


                if (attributeType == typeof(Vim.G3dNext.Attributes.MeshIndicesAttribute))
                    return Indices;

            throw new ArgumentException("Type {attributeType.ToString()} is not supported.");
        }

        public IAttribute MergeAttribute(string attributeName, IReadOnlyList<IAttributeCollection> otherCollections)
        {
            var collections = otherCollections.Prepend(this).ToArray();
            switch (attributeName)
            {

                case Vim.G3dNext.Attributes.MeshInstanceTransformsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.MeshInstanceTransformsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.MeshInstanceTransformsAttribute, Vim.Math3d.Matrix4x4>();
                }

                case Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountsAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetsAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetsAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetsAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetsAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.MeshSubmeshMaterialsAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.MeshSubmeshMaterialsAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.MeshPositionsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.MeshPositionsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.MeshPositionsAttribute, Vim.Math3d.Vector3>();
                }

                case Vim.G3dNext.Attributes.MeshIndicesAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.MeshIndicesAttribute>().MergeIndexAttributes();
                }

                default:
                    throw new ArgumentException(nameof(attributeName));
            }
        }

        public void Validate() 
        {
            // Ensure all the indices are either -1 or within the bounds of the attributes they are indexing into.

            {
                var maxIndex = GetAttribute(SubmeshIndexOffsets.IndexInto).Data.Length - 1;
                for (var i = 0; i < SubmeshIndexOffsets.TypedData.Length; ++i)
                {
                    var index = SubmeshIndexOffsets.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetsAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(SubmeshVertexOffsets.IndexInto).Data.Length - 1;
                for (var i = 0; i < SubmeshVertexOffsets.TypedData.Length; ++i)
                {
                    var index = SubmeshVertexOffsets.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetsAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(SubmeshMaterials.IndexInto).Data.Length - 1;
                for (var i = 0; i < SubmeshMaterials.TypedData.Length; ++i)
                {
                    var index = SubmeshMaterials.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.MeshSubmeshMaterialsAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(Indices.IndexInto).Data.Length - 1;
                for (var i = 0; i < Indices.TypedData.Length; ++i)
                {
                    var index = Indices.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.MeshIndicesAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }
        }
    }
}
