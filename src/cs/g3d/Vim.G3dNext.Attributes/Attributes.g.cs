// AUTO-GENERATED FILE, DO NOT MODIFY.
// ReSharper disable All
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Vim.BFast;
using Vim.BFastNextNS;

namespace Vim.G3dNext.Attributes
{
    
    public partial class PositionsAttribute : IAttribute<Vim.Math3d.Vector3>
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
    
    public partial class IndicesAttribute : IAttribute<System.Int32>
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
            = typeof(Vim.G3dNext.Attributes.PositionsAttribute);

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
    
    public partial class InstanceTransformsAttribute : IAttribute<Vim.Math3d.Matrix4x4>
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
    
    public partial class InstanceParentsAttribute : IAttribute<System.Int32>
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
            = typeof(Vim.G3dNext.Attributes.InstanceTransformsAttribute);

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
    
    public partial class InstanceFlagsAttribute : IAttribute<System.UInt16>
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
    
    public partial class InstanceMeshesAttribute : IAttribute<System.Int32>
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
            = typeof(Vim.G3dNext.Attributes.MeshSubmeshOffsetsAttribute);

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
    
    public partial class MeshSubmeshOffsetsAttribute : IAttribute<System.Int32>
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
            = typeof(Vim.G3dNext.Attributes.SubmeshIndexOffsetsAttribute);

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
    
    public partial class SubmeshIndexOffsetsAttribute : IAttribute<System.Int32>
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
            = typeof(Vim.G3dNext.Attributes.IndicesAttribute);

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
    
    public partial class SubmeshMaterialsAttribute : IAttribute<System.Int32>
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
            = typeof(Vim.G3dNext.Attributes.MaterialColorsAttribute);

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
    
    public partial class ShapeVerticesAttribute : IAttribute<Vim.Math3d.Vector3>
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
    
    public partial class ShapeVertexOffsetsAttribute : IAttribute<System.Int32>
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
            = typeof(Vim.G3dNext.Attributes.ShapeVerticesAttribute);

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
    
    public partial class ShapeColorsAttribute : IAttribute<Vim.Math3d.Vector4>
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
    
    public partial class ShapeWidthsAttribute : IAttribute<System.Single>
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
    
    public partial class InstanceFilesAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:instance:file:0:int32:1";

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
            TypedData = bfast.GetArray<System.Int32>("g3d:instance:file:0:int32:1");
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
    
    public partial class InstanceNodesAttribute : IAttribute<System.Int32>
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
    
    public partial class InstanceIndicesAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:instance:index:0:int32:1";

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
            TypedData = bfast.GetArray<System.Int32>("g3d:instance:index:0:int32:1");
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
    
    public partial class InstanceGroupsAttribute : IAttribute<System.Int32>
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
    
    public partial class InstanceTagsAttribute : IAttribute<System.Int64>
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
    
    public partial class InstanceMinsAttribute : IAttribute<Vim.Math3d.Vector3>
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
    
    public partial class InstanceMaxsAttribute : IAttribute<Vim.Math3d.Vector3>
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
    
    public partial class MeshInstanceCountsAttribute : IAttribute<System.Int32>
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
    
    public partial class MeshVertexCountsAttribute : IAttribute<System.Int32>
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
    
    public partial class MeshIndexCountsAttribute : IAttribute<System.Int32>
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
    
    public partial class MeshOpaqueVertexCountsAttribute : IAttribute<System.Int32>
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
    
    public partial class MeshOpaqueIndexCountsAttribute : IAttribute<System.Int32>
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
            = typeof(Vim.G3dNext.Attributes.IndicesAttribute);

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
            = typeof(Vim.G3dNext.Attributes.IndicesAttribute);

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
            = typeof(Vim.G3dNext.Attributes.MaterialColorsAttribute);

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
            = typeof(Vim.G3dNext.Attributes.PositionsAttribute);

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
            get => Attributes.PositionsAttribute.TypedData;
            set => Attributes.PositionsAttribute.TypedData = value;
        }

        public System.Int32[] Indices
        {
            get => Attributes.IndicesAttribute.TypedData;
            set => Attributes.IndicesAttribute.TypedData = value;
        }

        public Vim.Math3d.Matrix4x4[] InstanceTransforms
        {
            get => Attributes.InstanceTransformsAttribute.TypedData;
            set => Attributes.InstanceTransformsAttribute.TypedData = value;
        }

        public System.Int32[] InstanceParents
        {
            get => Attributes.InstanceParentsAttribute.TypedData;
            set => Attributes.InstanceParentsAttribute.TypedData = value;
        }

        public System.UInt16[] InstanceFlags
        {
            get => Attributes.InstanceFlagsAttribute.TypedData;
            set => Attributes.InstanceFlagsAttribute.TypedData = value;
        }

        public System.Int32[] InstanceMeshes
        {
            get => Attributes.InstanceMeshesAttribute.TypedData;
            set => Attributes.InstanceMeshesAttribute.TypedData = value;
        }

        public System.Int32[] MeshSubmeshOffsets
        {
            get => Attributes.MeshSubmeshOffsetsAttribute.TypedData;
            set => Attributes.MeshSubmeshOffsetsAttribute.TypedData = value;
        }

        public System.Int32[] SubmeshIndexOffsets
        {
            get => Attributes.SubmeshIndexOffsetsAttribute.TypedData;
            set => Attributes.SubmeshIndexOffsetsAttribute.TypedData = value;
        }

        public System.Int32[] SubmeshMaterials
        {
            get => Attributes.SubmeshMaterialsAttribute.TypedData;
            set => Attributes.SubmeshMaterialsAttribute.TypedData = value;
        }

        public Vim.Math3d.Vector4[] MaterialColors
        {
            get => Attributes.MaterialColorsAttribute.TypedData;
            set => Attributes.MaterialColorsAttribute.TypedData = value;
        }

        public System.Single[] MaterialGlossiness
        {
            get => Attributes.MaterialGlossinessAttribute.TypedData;
            set => Attributes.MaterialGlossinessAttribute.TypedData = value;
        }

        public System.Single[] MaterialSmoothness
        {
            get => Attributes.MaterialSmoothnessAttribute.TypedData;
            set => Attributes.MaterialSmoothnessAttribute.TypedData = value;
        }

        public Vim.Math3d.Vector3[] ShapeVertices
        {
            get => Attributes.ShapeVerticesAttribute.TypedData;
            set => Attributes.ShapeVerticesAttribute.TypedData = value;
        }

        public System.Int32[] ShapeVertexOffsets
        {
            get => Attributes.ShapeVertexOffsetsAttribute.TypedData;
            set => Attributes.ShapeVertexOffsetsAttribute.TypedData = value;
        }

        public Vim.Math3d.Vector4[] ShapeColors
        {
            get => Attributes.ShapeColorsAttribute.TypedData;
            set => Attributes.ShapeColorsAttribute.TypedData = value;
        }

        public System.Single[] ShapeWidths
        {
            get => Attributes.ShapeWidthsAttribute.TypedData;
            set => Attributes.ShapeWidthsAttribute.TypedData = value;
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
                [Vim.G3dNext.Attributes.PositionsAttribute.AttributeName] = new Vim.G3dNext.Attributes.PositionsAttribute(),
                [Vim.G3dNext.Attributes.IndicesAttribute.AttributeName] = new Vim.G3dNext.Attributes.IndicesAttribute(),
                [Vim.G3dNext.Attributes.InstanceTransformsAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceTransformsAttribute(),
                [Vim.G3dNext.Attributes.InstanceParentsAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceParentsAttribute(),
                [Vim.G3dNext.Attributes.InstanceFlagsAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceFlagsAttribute(),
                [Vim.G3dNext.Attributes.InstanceMeshesAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceMeshesAttribute(),
                [Vim.G3dNext.Attributes.MeshSubmeshOffsetsAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshSubmeshOffsetsAttribute(),
                [Vim.G3dNext.Attributes.SubmeshIndexOffsetsAttribute.AttributeName] = new Vim.G3dNext.Attributes.SubmeshIndexOffsetsAttribute(),
                [Vim.G3dNext.Attributes.SubmeshMaterialsAttribute.AttributeName] = new Vim.G3dNext.Attributes.SubmeshMaterialsAttribute(),
                [Vim.G3dNext.Attributes.MaterialColorsAttribute.AttributeName] = new Vim.G3dNext.Attributes.MaterialColorsAttribute(),
                [Vim.G3dNext.Attributes.MaterialGlossinessAttribute.AttributeName] = new Vim.G3dNext.Attributes.MaterialGlossinessAttribute(),
                [Vim.G3dNext.Attributes.MaterialSmoothnessAttribute.AttributeName] = new Vim.G3dNext.Attributes.MaterialSmoothnessAttribute(),
                [Vim.G3dNext.Attributes.ShapeVerticesAttribute.AttributeName] = new Vim.G3dNext.Attributes.ShapeVerticesAttribute(),
                [Vim.G3dNext.Attributes.ShapeVertexOffsetsAttribute.AttributeName] = new Vim.G3dNext.Attributes.ShapeVertexOffsetsAttribute(),
                [Vim.G3dNext.Attributes.ShapeColorsAttribute.AttributeName] = new Vim.G3dNext.Attributes.ShapeColorsAttribute(),
                [Vim.G3dNext.Attributes.ShapeWidthsAttribute.AttributeName] = new Vim.G3dNext.Attributes.ShapeWidthsAttribute(),
            };

        // Named Attributes.

        public Vim.G3dNext.Attributes.PositionsAttribute PositionsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.PositionsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.PositionsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.PositionsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.IndicesAttribute IndicesAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.IndicesAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.IndicesAttribute : default;
            set => Map[Vim.G3dNext.Attributes.IndicesAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceTransformsAttribute InstanceTransformsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.InstanceTransformsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceTransformsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.InstanceTransformsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceParentsAttribute InstanceParentsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.InstanceParentsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceParentsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.InstanceParentsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceFlagsAttribute InstanceFlagsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.InstanceFlagsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceFlagsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.InstanceFlagsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceMeshesAttribute InstanceMeshesAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.InstanceMeshesAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceMeshesAttribute : default;
            set => Map[Vim.G3dNext.Attributes.InstanceMeshesAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshSubmeshOffsetsAttribute MeshSubmeshOffsetsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MeshSubmeshOffsetsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshSubmeshOffsetsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MeshSubmeshOffsetsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.SubmeshIndexOffsetsAttribute SubmeshIndexOffsetsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.SubmeshIndexOffsetsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.SubmeshIndexOffsetsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.SubmeshIndexOffsetsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.SubmeshMaterialsAttribute SubmeshMaterialsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.SubmeshMaterialsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.SubmeshMaterialsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.SubmeshMaterialsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MaterialColorsAttribute MaterialColorsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MaterialColorsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MaterialColorsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MaterialColorsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MaterialGlossinessAttribute MaterialGlossinessAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MaterialGlossinessAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MaterialGlossinessAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MaterialGlossinessAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MaterialSmoothnessAttribute MaterialSmoothnessAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MaterialSmoothnessAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MaterialSmoothnessAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MaterialSmoothnessAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.ShapeVerticesAttribute ShapeVerticesAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.ShapeVerticesAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.ShapeVerticesAttribute : default;
            set => Map[Vim.G3dNext.Attributes.ShapeVerticesAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.ShapeVertexOffsetsAttribute ShapeVertexOffsetsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.ShapeVertexOffsetsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.ShapeVertexOffsetsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.ShapeVertexOffsetsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.ShapeColorsAttribute ShapeColorsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.ShapeColorsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.ShapeColorsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.ShapeColorsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.ShapeWidthsAttribute ShapeWidthsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.ShapeWidthsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.ShapeWidthsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.ShapeWidthsAttribute.AttributeName] = value as IAttribute;
        }

        /// <inheritdoc/>
        public IAttribute GetAttribute(Type attributeType)
        {

            if (attributeType == typeof(Vim.G3dNext.Attributes.PositionsAttribute))
                return PositionsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.IndicesAttribute))
                return IndicesAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceTransformsAttribute))
                return InstanceTransformsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceParentsAttribute))
                return InstanceParentsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceFlagsAttribute))
                return InstanceFlagsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceMeshesAttribute))
                return InstanceMeshesAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshSubmeshOffsetsAttribute))
                return MeshSubmeshOffsetsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.SubmeshIndexOffsetsAttribute))
                return SubmeshIndexOffsetsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.SubmeshMaterialsAttribute))
                return SubmeshMaterialsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MaterialColorsAttribute))
                return MaterialColorsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MaterialGlossinessAttribute))
                return MaterialGlossinessAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MaterialSmoothnessAttribute))
                return MaterialSmoothnessAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.ShapeVerticesAttribute))
                return ShapeVerticesAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.ShapeVertexOffsetsAttribute))
                return ShapeVertexOffsetsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.ShapeColorsAttribute))
                return ShapeColorsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.ShapeWidthsAttribute))
                return ShapeWidthsAttribute;

            throw new ArgumentException("Type {attributeType.ToString()} is not supported.");
        }

        public IAttribute MergeAttribute(string attributeName, IReadOnlyList<IAttributeCollection> otherCollections)
        {
            var collections = otherCollections.Prepend(this).ToArray();
            switch (attributeName)
            {

                case Vim.G3dNext.Attributes.PositionsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.PositionsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.PositionsAttribute, Vim.Math3d.Vector3>();
                }

                case Vim.G3dNext.Attributes.IndicesAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.IndicesAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.InstanceTransformsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.InstanceTransformsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.InstanceTransformsAttribute, Vim.Math3d.Matrix4x4>();
                }

                case Vim.G3dNext.Attributes.InstanceParentsAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.InstanceParentsAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.InstanceFlagsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.InstanceFlagsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.InstanceFlagsAttribute, System.UInt16>();
                }

                case Vim.G3dNext.Attributes.InstanceMeshesAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.InstanceMeshesAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.MeshSubmeshOffsetsAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.MeshSubmeshOffsetsAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.SubmeshIndexOffsetsAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.SubmeshIndexOffsetsAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.SubmeshMaterialsAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.SubmeshMaterialsAttribute>().MergeIndexAttributes();
                }

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

                case Vim.G3dNext.Attributes.ShapeVerticesAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.ShapeVerticesAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.ShapeVerticesAttribute, Vim.Math3d.Vector3>();
                }

                case Vim.G3dNext.Attributes.ShapeVertexOffsetsAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.ShapeVertexOffsetsAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.ShapeColorsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.ShapeColorsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.ShapeColorsAttribute, Vim.Math3d.Vector4>();
                }

                case Vim.G3dNext.Attributes.ShapeWidthsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.ShapeWidthsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.ShapeWidthsAttribute, System.Single>();
                }

                default:
                    throw new ArgumentException(nameof(attributeName));
            }
        }

        public void Validate() 
        {
            // Ensure all the indices are either -1 or within the bounds of the attributes they are indexing into.

            {
                var maxIndex = GetAttribute(IndicesAttribute.IndexInto).Data.Length - 1;
                for (var i = 0; i < IndicesAttribute.TypedData.Length; ++i)
                {
                    var index = IndicesAttribute.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.IndicesAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(InstanceParentsAttribute.IndexInto).Data.Length - 1;
                for (var i = 0; i < InstanceParentsAttribute.TypedData.Length; ++i)
                {
                    var index = InstanceParentsAttribute.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.InstanceParentsAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(InstanceMeshesAttribute.IndexInto).Data.Length - 1;
                for (var i = 0; i < InstanceMeshesAttribute.TypedData.Length; ++i)
                {
                    var index = InstanceMeshesAttribute.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.InstanceMeshesAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(MeshSubmeshOffsetsAttribute.IndexInto).Data.Length - 1;
                for (var i = 0; i < MeshSubmeshOffsetsAttribute.TypedData.Length; ++i)
                {
                    var index = MeshSubmeshOffsetsAttribute.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.MeshSubmeshOffsetsAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(SubmeshIndexOffsetsAttribute.IndexInto).Data.Length - 1;
                for (var i = 0; i < SubmeshIndexOffsetsAttribute.TypedData.Length; ++i)
                {
                    var index = SubmeshIndexOffsetsAttribute.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.SubmeshIndexOffsetsAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(SubmeshMaterialsAttribute.IndexInto).Data.Length - 1;
                for (var i = 0; i < SubmeshMaterialsAttribute.TypedData.Length; ++i)
                {
                    var index = SubmeshMaterialsAttribute.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.SubmeshMaterialsAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(ShapeVertexOffsetsAttribute.IndexInto).Data.Length - 1;
                for (var i = 0; i < ShapeVertexOffsetsAttribute.TypedData.Length; ++i)
                {
                    var index = ShapeVertexOffsetsAttribute.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.ShapeVertexOffsetsAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
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
            get => Attributes.MaterialColorsAttribute.TypedData;
            set => Attributes.MaterialColorsAttribute.TypedData = value;
        }

        public System.Single[] MaterialGlossiness
        {
            get => Attributes.MaterialGlossinessAttribute.TypedData;
            set => Attributes.MaterialGlossinessAttribute.TypedData = value;
        }

        public System.Single[] MaterialSmoothness
        {
            get => Attributes.MaterialSmoothnessAttribute.TypedData;
            set => Attributes.MaterialSmoothnessAttribute.TypedData = value;
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

        public Vim.G3dNext.Attributes.MaterialColorsAttribute MaterialColorsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MaterialColorsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MaterialColorsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MaterialColorsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MaterialGlossinessAttribute MaterialGlossinessAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MaterialGlossinessAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MaterialGlossinessAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MaterialGlossinessAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MaterialSmoothnessAttribute MaterialSmoothnessAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MaterialSmoothnessAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MaterialSmoothnessAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MaterialSmoothnessAttribute.AttributeName] = value as IAttribute;
        }

        /// <inheritdoc/>
        public IAttribute GetAttribute(Type attributeType)
        {

            if (attributeType == typeof(Vim.G3dNext.Attributes.MaterialColorsAttribute))
                return MaterialColorsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MaterialGlossinessAttribute))
                return MaterialGlossinessAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MaterialSmoothnessAttribute))
                return MaterialSmoothnessAttribute;

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

        
        public System.Int32[] InstanceFiles
        {
            get => Attributes.InstanceFilesAttribute.TypedData;
            set => Attributes.InstanceFilesAttribute.TypedData = value;
        }

        public System.Int32[] InstanceIndices
        {
            get => Attributes.InstanceIndicesAttribute.TypedData;
            set => Attributes.InstanceIndicesAttribute.TypedData = value;
        }

        public System.Int32[] InstanceNodes
        {
            get => Attributes.InstanceNodesAttribute.TypedData;
            set => Attributes.InstanceNodesAttribute.TypedData = value;
        }

        public System.Int32[] InstanceGroups
        {
            get => Attributes.InstanceGroupsAttribute.TypedData;
            set => Attributes.InstanceGroupsAttribute.TypedData = value;
        }

        public System.Int64[] InstanceTags
        {
            get => Attributes.InstanceTagsAttribute.TypedData;
            set => Attributes.InstanceTagsAttribute.TypedData = value;
        }

        public System.UInt16[] InstanceFlags
        {
            get => Attributes.InstanceFlagsAttribute.TypedData;
            set => Attributes.InstanceFlagsAttribute.TypedData = value;
        }

        public Vim.Math3d.Vector3[] InstanceMins
        {
            get => Attributes.InstanceMinsAttribute.TypedData;
            set => Attributes.InstanceMinsAttribute.TypedData = value;
        }

        public Vim.Math3d.Vector3[] InstanceMaxs
        {
            get => Attributes.InstanceMaxsAttribute.TypedData;
            set => Attributes.InstanceMaxsAttribute.TypedData = value;
        }

        public System.Int32[] MeshInstanceCounts
        {
            get => Attributes.MeshInstanceCountsAttribute.TypedData;
            set => Attributes.MeshInstanceCountsAttribute.TypedData = value;
        }

        public System.Int32[] MeshIndexCounts
        {
            get => Attributes.MeshIndexCountsAttribute.TypedData;
            set => Attributes.MeshIndexCountsAttribute.TypedData = value;
        }

        public System.Int32[] MeshVertexCounts
        {
            get => Attributes.MeshVertexCountsAttribute.TypedData;
            set => Attributes.MeshVertexCountsAttribute.TypedData = value;
        }

        public System.Int32[] MeshOpaqueIndexCounts
        {
            get => Attributes.MeshOpaqueIndexCountsAttribute.TypedData;
            set => Attributes.MeshOpaqueIndexCountsAttribute.TypedData = value;
        }

        public System.Int32[] MeshOpaqueVertexCounts
        {
            get => Attributes.MeshOpaqueVertexCountsAttribute.TypedData;
            set => Attributes.MeshOpaqueVertexCountsAttribute.TypedData = value;
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
                [Vim.G3dNext.Attributes.InstanceFilesAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceFilesAttribute(),
                [Vim.G3dNext.Attributes.InstanceIndicesAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceIndicesAttribute(),
                [Vim.G3dNext.Attributes.InstanceNodesAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceNodesAttribute(),
                [Vim.G3dNext.Attributes.InstanceGroupsAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceGroupsAttribute(),
                [Vim.G3dNext.Attributes.InstanceTagsAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceTagsAttribute(),
                [Vim.G3dNext.Attributes.InstanceFlagsAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceFlagsAttribute(),
                [Vim.G3dNext.Attributes.InstanceMinsAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceMinsAttribute(),
                [Vim.G3dNext.Attributes.InstanceMaxsAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceMaxsAttribute(),
                [Vim.G3dNext.Attributes.MeshInstanceCountsAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshInstanceCountsAttribute(),
                [Vim.G3dNext.Attributes.MeshIndexCountsAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshIndexCountsAttribute(),
                [Vim.G3dNext.Attributes.MeshVertexCountsAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshVertexCountsAttribute(),
                [Vim.G3dNext.Attributes.MeshOpaqueIndexCountsAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshOpaqueIndexCountsAttribute(),
                [Vim.G3dNext.Attributes.MeshOpaqueVertexCountsAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshOpaqueVertexCountsAttribute(),
            };

        // Named Attributes.

        public Vim.G3dNext.Attributes.InstanceFilesAttribute InstanceFilesAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.InstanceFilesAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceFilesAttribute : default;
            set => Map[Vim.G3dNext.Attributes.InstanceFilesAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceIndicesAttribute InstanceIndicesAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.InstanceIndicesAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceIndicesAttribute : default;
            set => Map[Vim.G3dNext.Attributes.InstanceIndicesAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceNodesAttribute InstanceNodesAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.InstanceNodesAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceNodesAttribute : default;
            set => Map[Vim.G3dNext.Attributes.InstanceNodesAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceGroupsAttribute InstanceGroupsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.InstanceGroupsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceGroupsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.InstanceGroupsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceTagsAttribute InstanceTagsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.InstanceTagsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceTagsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.InstanceTagsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceFlagsAttribute InstanceFlagsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.InstanceFlagsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceFlagsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.InstanceFlagsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceMinsAttribute InstanceMinsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.InstanceMinsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceMinsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.InstanceMinsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceMaxsAttribute InstanceMaxsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.InstanceMaxsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceMaxsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.InstanceMaxsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshInstanceCountsAttribute MeshInstanceCountsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MeshInstanceCountsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshInstanceCountsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MeshInstanceCountsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshIndexCountsAttribute MeshIndexCountsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MeshIndexCountsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshIndexCountsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MeshIndexCountsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshVertexCountsAttribute MeshVertexCountsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MeshVertexCountsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshVertexCountsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MeshVertexCountsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshOpaqueIndexCountsAttribute MeshOpaqueIndexCountsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MeshOpaqueIndexCountsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshOpaqueIndexCountsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MeshOpaqueIndexCountsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshOpaqueVertexCountsAttribute MeshOpaqueVertexCountsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MeshOpaqueVertexCountsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshOpaqueVertexCountsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MeshOpaqueVertexCountsAttribute.AttributeName] = value as IAttribute;
        }

        /// <inheritdoc/>
        public IAttribute GetAttribute(Type attributeType)
        {

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceFilesAttribute))
                return InstanceFilesAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceIndicesAttribute))
                return InstanceIndicesAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceNodesAttribute))
                return InstanceNodesAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceGroupsAttribute))
                return InstanceGroupsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceTagsAttribute))
                return InstanceTagsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceFlagsAttribute))
                return InstanceFlagsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceMinsAttribute))
                return InstanceMinsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceMaxsAttribute))
                return InstanceMaxsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshInstanceCountsAttribute))
                return MeshInstanceCountsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshIndexCountsAttribute))
                return MeshIndexCountsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshVertexCountsAttribute))
                return MeshVertexCountsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshOpaqueIndexCountsAttribute))
                return MeshOpaqueIndexCountsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshOpaqueVertexCountsAttribute))
                return MeshOpaqueVertexCountsAttribute;

            throw new ArgumentException("Type {attributeType.ToString()} is not supported.");
        }

        public IAttribute MergeAttribute(string attributeName, IReadOnlyList<IAttributeCollection> otherCollections)
        {
            var collections = otherCollections.Prepend(this).ToArray();
            switch (attributeName)
            {

                case Vim.G3dNext.Attributes.InstanceFilesAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.InstanceFilesAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.InstanceFilesAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.InstanceIndicesAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.InstanceIndicesAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.InstanceIndicesAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.InstanceNodesAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.InstanceNodesAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.InstanceNodesAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.InstanceGroupsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.InstanceGroupsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.InstanceGroupsAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.InstanceTagsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.InstanceTagsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.InstanceTagsAttribute, System.Int64>();
                }

                case Vim.G3dNext.Attributes.InstanceFlagsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.InstanceFlagsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.InstanceFlagsAttribute, System.UInt16>();
                }

                case Vim.G3dNext.Attributes.InstanceMinsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.InstanceMinsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.InstanceMinsAttribute, Vim.Math3d.Vector3>();
                }

                case Vim.G3dNext.Attributes.InstanceMaxsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.InstanceMaxsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.InstanceMaxsAttribute, Vim.Math3d.Vector3>();
                }

                case Vim.G3dNext.Attributes.MeshInstanceCountsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.MeshInstanceCountsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.MeshInstanceCountsAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.MeshIndexCountsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.MeshIndexCountsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.MeshIndexCountsAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.MeshVertexCountsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.MeshVertexCountsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.MeshVertexCountsAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.MeshOpaqueIndexCountsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.MeshOpaqueIndexCountsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.MeshOpaqueIndexCountsAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.MeshOpaqueVertexCountsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.MeshOpaqueVertexCountsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.MeshOpaqueVertexCountsAttribute, System.Int32>();
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
            get => Attributes.MeshInstanceTransformsAttribute.TypedData;
            set => Attributes.MeshInstanceTransformsAttribute.TypedData = value;
        }

        public System.Int32[] OpaqueSubmeshCounts
        {
            get => Attributes.MeshOpaqueSubmeshCountsAttribute.TypedData;
            set => Attributes.MeshOpaqueSubmeshCountsAttribute.TypedData = value;
        }

        public System.Int32[] SubmeshIndexOffsets
        {
            get => Attributes.MeshSubmeshIndexOffsetsAttribute.TypedData;
            set => Attributes.MeshSubmeshIndexOffsetsAttribute.TypedData = value;
        }

        public System.Int32[] SubmeshVertexOffsets
        {
            get => Attributes.MeshSubmeshVertexOffsetsAttribute.TypedData;
            set => Attributes.MeshSubmeshVertexOffsetsAttribute.TypedData = value;
        }

        public System.Int32[] SubmeshMaterials
        {
            get => Attributes.MeshSubmeshMaterialsAttribute.TypedData;
            set => Attributes.MeshSubmeshMaterialsAttribute.TypedData = value;
        }

        public Vim.Math3d.Vector3[] Positions
        {
            get => Attributes.MeshPositionsAttribute.TypedData;
            set => Attributes.MeshPositionsAttribute.TypedData = value;
        }

        public System.Int32[] Indices
        {
            get => Attributes.MeshIndicesAttribute.TypedData;
            set => Attributes.MeshIndicesAttribute.TypedData = value;
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

        public Vim.G3dNext.Attributes.MeshInstanceTransformsAttribute MeshInstanceTransformsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MeshInstanceTransformsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshInstanceTransformsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MeshInstanceTransformsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountsAttribute MeshOpaqueSubmeshCountsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetsAttribute MeshSubmeshIndexOffsetsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetsAttribute MeshSubmeshVertexOffsetsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshSubmeshMaterialsAttribute MeshSubmeshMaterialsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MeshSubmeshMaterialsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshSubmeshMaterialsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MeshSubmeshMaterialsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshPositionsAttribute MeshPositionsAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MeshPositionsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshPositionsAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MeshPositionsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshIndicesAttribute MeshIndicesAttribute
        {
            get => Map.TryGetValue(Vim.G3dNext.Attributes.MeshIndicesAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshIndicesAttribute : default;
            set => Map[Vim.G3dNext.Attributes.MeshIndicesAttribute.AttributeName] = value as IAttribute;
        }

        /// <inheritdoc/>
        public IAttribute GetAttribute(Type attributeType)
        {

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshInstanceTransformsAttribute))
                return MeshInstanceTransformsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountsAttribute))
                return MeshOpaqueSubmeshCountsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetsAttribute))
                return MeshSubmeshIndexOffsetsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetsAttribute))
                return MeshSubmeshVertexOffsetsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshSubmeshMaterialsAttribute))
                return MeshSubmeshMaterialsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshPositionsAttribute))
                return MeshPositionsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshIndicesAttribute))
                return MeshIndicesAttribute;

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
                var maxIndex = GetAttribute(MeshSubmeshIndexOffsetsAttribute.IndexInto).Data.Length - 1;
                for (var i = 0; i < MeshSubmeshIndexOffsetsAttribute.TypedData.Length; ++i)
                {
                    var index = MeshSubmeshIndexOffsetsAttribute.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetsAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(MeshSubmeshVertexOffsetsAttribute.IndexInto).Data.Length - 1;
                for (var i = 0; i < MeshSubmeshVertexOffsetsAttribute.TypedData.Length; ++i)
                {
                    var index = MeshSubmeshVertexOffsetsAttribute.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetsAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(MeshSubmeshMaterialsAttribute.IndexInto).Data.Length - 1;
                for (var i = 0; i < MeshSubmeshMaterialsAttribute.TypedData.Length; ++i)
                {
                    var index = MeshSubmeshMaterialsAttribute.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.MeshSubmeshMaterialsAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(MeshIndicesAttribute.IndexInto).Data.Length - 1;
                for (var i = 0; i < MeshIndicesAttribute.TypedData.Length; ++i)
                {
                    var index = MeshIndicesAttribute.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.MeshIndicesAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }
        }
    }
}
