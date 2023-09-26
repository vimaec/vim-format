// AUTO-GENERATED FILE, DO NOT MODIFY.
// ReSharper disable All
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Vim.BFast;

namespace Vim.G3dNext.Attributes
{
    
    public partial class CornersPerFaceAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:all:facesize:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:all:facesize:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Singleton;

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
    
    public partial class VertexAttribute : IAttribute<Vim.Math3d.Vector3>
    {
        public const string AttributeName = "g3d:vertex:position:0:float32:3";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
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
    
    public partial class IndexAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:corner:index:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:corner:index:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Index;

        public Type IndexInto { get; }
            = typeof(Vim.G3dNext.Attributes.VertexAttribute);

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
    
    public partial class InstanceTransformAttribute : IAttribute<Vim.Math3d.Matrix4x4>
    {
        public const string AttributeName = "g3d:instance:transform:0:float32:16";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
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
    
    public partial class InstanceParentAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:instance:parent:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:instance:parent:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Index;

        public Type IndexInto { get; }
            = typeof(Vim.G3dNext.Attributes.InstanceTransformAttribute);

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

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
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
    
    public partial class InstanceMeshAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:instance:mesh:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:instance:mesh:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Index;

        public Type IndexInto { get; }
            = typeof(Vim.G3dNext.Attributes.MeshSubmeshOffsetAttribute);

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
    
    public partial class MeshSubmeshOffsetAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:mesh:submeshoffset:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:mesh:submeshoffset:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Index;

        public Type IndexInto { get; }
            = typeof(Vim.G3dNext.Attributes.SubmeshIndexOffsetAttribute);

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
    
    public partial class SubmeshIndexOffsetAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:submesh:indexoffset:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:submesh:indexoffset:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Index;

        public Type IndexInto { get; }
            = typeof(Vim.G3dNext.Attributes.IndexAttribute);

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
    
    public partial class SubmeshMaterialAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:submesh:material:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:submesh:material:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Index;

        public Type IndexInto { get; }
            = typeof(Vim.G3dNext.Attributes.MaterialColorAttribute);

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
    
    public partial class ShapeVertexAttribute : IAttribute<Vim.Math3d.Vector3>
    {
        public const string AttributeName = "g3d:shapevertex:position:0:float32:3";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
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
    
    public partial class ShapeVertexOffsetAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:shape:vertexoffset:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:shape:vertexoffset:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Index;

        public Type IndexInto { get; }
            = typeof(Vim.G3dNext.Attributes.ShapeVertexAttribute);

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
    
    public partial class ShapeColorAttribute : IAttribute<Vim.Math3d.Vector4>
    {
        public const string AttributeName = "g3d:shape:color:0:float32:4";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
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
    
    public partial class ShapeWidthAttribute : IAttribute<System.Single>
    {
        public const string AttributeName = "g3d:shape:width:0:float32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
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
    
    public partial class MaterialColorAttribute : IAttribute<Vim.Math3d.Vector4>
    {
        public const string AttributeName = "g3d:material:color:0:float32:4";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
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

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
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

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
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

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
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

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
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

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
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

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
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

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
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

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
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

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
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

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
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

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
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

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
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

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
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

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
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
    
    public partial class MeshInstanceTransformAttribute : IAttribute<Vim.Math3d.Matrix4x4>
    {
        public const string AttributeName = "g3d:instance:transform:0:float32:16";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
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
    
    public partial class MeshOpaqueSubmeshCountAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:mesh:opaquesubmeshcount:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
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
    
    public partial class MeshSubmeshIndexOffsetAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:submesh:indexoffset:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:submesh:indexoffset:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Index;

        public Type IndexInto { get; }
            = typeof(Vim.G3dNext.Attributes.IndexAttribute);

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
    
    public partial class MeshSubmeshVertexOffsetAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:submesh:vertexoffset:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:submesh:vertexoffset:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Index;

        public Type IndexInto { get; }
            = typeof(Vim.G3dNext.Attributes.IndexAttribute);

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
    
    public partial class MeshSubmeshMaterialAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:submesh:material:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:submesh:material:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Index;

        public Type IndexInto { get; }
            = typeof(Vim.G3dNext.Attributes.MaterialColorAttribute);

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
    
    public partial class MeshVertexAttribute : IAttribute<Vim.Math3d.Vector3>
    {
        public const string AttributeName = "g3d:vertex:position:0:float32:3";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
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
    
    public partial class MeshIndexAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:corner:index:0:int32:1";

        public string Name
            => AttributeName;

        public int Count => TypedData?.Length ?? 0;

        public void AddTo(BFastNext.BFastNext bfast)
        {
            if(TypedData != null)
            {
                bfast.AddArray(Name, TypedData);
            }
        }

        public void ReadBFast(BFastNext.BFastNext bfast)
        {
            TypedData = bfast.GetArray<System.Int32>("g3d:corner:index:0:int32:1");
        }

        public IAttributeDescriptor AttributeDescriptor { get; }
            = new AttributeDescriptor(AttributeName);

        public AttributeType AttributeType { get; }
            = AttributeType.Index;

        public Type IndexInto { get; }
            = typeof(Vim.G3dNext.Attributes.VertexAttribute);

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
    
    public class G3dVim2
    {
        public G3DNext<VimAttributeCollection> source;

        public G3dVim2()
        {
            this.source = new G3DNext<VimAttributeCollection> ();
        }

        public G3dVim2(G3DNext<VimAttributeCollection> source)
        {
            this.source = source;
        }

        public G3dVim2(BFastNext.BFastNext bfast)
        {
            this.source = new G3DNext<VimAttributeCollection>(bfast);
        }

        
        public System.Int32[] CornersPerFace
        {
            get => source.AttributeCollection.CornersPerFaceAttribute.TypedData;
            set => source.AttributeCollection.CornersPerFaceAttribute.TypedData = value;
        }

        public Vim.Math3d.Vector3[] Vertex
        {
            get => source.AttributeCollection.VertexAttribute.TypedData;
            set => source.AttributeCollection.VertexAttribute.TypedData = value;
        }

        public System.Int32[] Index
        {
            get => source.AttributeCollection.IndexAttribute.TypedData;
            set => source.AttributeCollection.IndexAttribute.TypedData = value;
        }

        public Vim.Math3d.Matrix4x4[] InstanceTransform
        {
            get => source.AttributeCollection.InstanceTransformAttribute.TypedData;
            set => source.AttributeCollection.InstanceTransformAttribute.TypedData = value;
        }

        public System.Int32[] InstanceParent
        {
            get => source.AttributeCollection.InstanceParentAttribute.TypedData;
            set => source.AttributeCollection.InstanceParentAttribute.TypedData = value;
        }

        public System.UInt16[] InstanceFlags
        {
            get => source.AttributeCollection.InstanceFlagsAttribute.TypedData;
            set => source.AttributeCollection.InstanceFlagsAttribute.TypedData = value;
        }

        public System.Int32[] InstanceMesh
        {
            get => source.AttributeCollection.InstanceMeshAttribute.TypedData;
            set => source.AttributeCollection.InstanceMeshAttribute.TypedData = value;
        }

        public System.Int32[] MeshSubmeshOffset
        {
            get => source.AttributeCollection.MeshSubmeshOffsetAttribute.TypedData;
            set => source.AttributeCollection.MeshSubmeshOffsetAttribute.TypedData = value;
        }

        public System.Int32[] SubmeshIndexOffset
        {
            get => source.AttributeCollection.SubmeshIndexOffsetAttribute.TypedData;
            set => source.AttributeCollection.SubmeshIndexOffsetAttribute.TypedData = value;
        }

        public System.Int32[] SubmeshMaterial
        {
            get => source.AttributeCollection.SubmeshMaterialAttribute.TypedData;
            set => source.AttributeCollection.SubmeshMaterialAttribute.TypedData = value;
        }

        public Vim.Math3d.Vector4[] MaterialColor
        {
            get => source.AttributeCollection.MaterialColorAttribute.TypedData;
            set => source.AttributeCollection.MaterialColorAttribute.TypedData = value;
        }

        public System.Single[] MaterialGlossiness
        {
            get => source.AttributeCollection.MaterialGlossinessAttribute.TypedData;
            set => source.AttributeCollection.MaterialGlossinessAttribute.TypedData = value;
        }

        public System.Single[] MaterialSmoothness
        {
            get => source.AttributeCollection.MaterialSmoothnessAttribute.TypedData;
            set => source.AttributeCollection.MaterialSmoothnessAttribute.TypedData = value;
        }

        public Vim.Math3d.Vector3[] ShapeVertex
        {
            get => source.AttributeCollection.ShapeVertexAttribute.TypedData;
            set => source.AttributeCollection.ShapeVertexAttribute.TypedData = value;
        }

        public System.Int32[] ShapeVertexOffset
        {
            get => source.AttributeCollection.ShapeVertexOffsetAttribute.TypedData;
            set => source.AttributeCollection.ShapeVertexOffsetAttribute.TypedData = value;
        }

        public Vim.Math3d.Vector4[] ShapeColor
        {
            get => source.AttributeCollection.ShapeColorAttribute.TypedData;
            set => source.AttributeCollection.ShapeColorAttribute.TypedData = value;
        }

        public System.Single[] ShapeWidth
        {
            get => source.AttributeCollection.ShapeWidthAttribute.TypedData;
            set => source.AttributeCollection.ShapeWidthAttribute.TypedData = value;
        }
    }

    
    public partial class VimAttributeCollection : IAttributeCollection
    {
        public IEnumerable<string> AttributeNames
            => Attributes.Keys;

        public long GetSize()
            => Attributes.Values.Sum(a => a.Data.LongLength * a.AttributeDescriptor.DataElementSize);

        public IDictionary<string, IAttribute> Attributes { get; }
            = new Dictionary<string, IAttribute>
            {
                [Vim.G3dNext.Attributes.CornersPerFaceAttribute.AttributeName] = new Vim.G3dNext.Attributes.CornersPerFaceAttribute(),
                [Vim.G3dNext.Attributes.VertexAttribute.AttributeName] = new Vim.G3dNext.Attributes.VertexAttribute(),
                [Vim.G3dNext.Attributes.IndexAttribute.AttributeName] = new Vim.G3dNext.Attributes.IndexAttribute(),
                [Vim.G3dNext.Attributes.InstanceTransformAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceTransformAttribute(),
                [Vim.G3dNext.Attributes.InstanceParentAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceParentAttribute(),
                [Vim.G3dNext.Attributes.InstanceFlagsAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceFlagsAttribute(),
                [Vim.G3dNext.Attributes.InstanceMeshAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceMeshAttribute(),
                [Vim.G3dNext.Attributes.MeshSubmeshOffsetAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshSubmeshOffsetAttribute(),
                [Vim.G3dNext.Attributes.SubmeshIndexOffsetAttribute.AttributeName] = new Vim.G3dNext.Attributes.SubmeshIndexOffsetAttribute(),
                [Vim.G3dNext.Attributes.SubmeshMaterialAttribute.AttributeName] = new Vim.G3dNext.Attributes.SubmeshMaterialAttribute(),
                [Vim.G3dNext.Attributes.MaterialColorAttribute.AttributeName] = new Vim.G3dNext.Attributes.MaterialColorAttribute(),
                [Vim.G3dNext.Attributes.MaterialGlossinessAttribute.AttributeName] = new Vim.G3dNext.Attributes.MaterialGlossinessAttribute(),
                [Vim.G3dNext.Attributes.MaterialSmoothnessAttribute.AttributeName] = new Vim.G3dNext.Attributes.MaterialSmoothnessAttribute(),
                [Vim.G3dNext.Attributes.ShapeVertexAttribute.AttributeName] = new Vim.G3dNext.Attributes.ShapeVertexAttribute(),
                [Vim.G3dNext.Attributes.ShapeVertexOffsetAttribute.AttributeName] = new Vim.G3dNext.Attributes.ShapeVertexOffsetAttribute(),
                [Vim.G3dNext.Attributes.ShapeColorAttribute.AttributeName] = new Vim.G3dNext.Attributes.ShapeColorAttribute(),
                [Vim.G3dNext.Attributes.ShapeWidthAttribute.AttributeName] = new Vim.G3dNext.Attributes.ShapeWidthAttribute(),
            };

        // Named Attributes.

        public Vim.G3dNext.Attributes.CornersPerFaceAttribute CornersPerFaceAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.CornersPerFaceAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.CornersPerFaceAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.CornersPerFaceAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.VertexAttribute VertexAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.VertexAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.VertexAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.VertexAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.IndexAttribute IndexAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.IndexAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.IndexAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.IndexAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceTransformAttribute InstanceTransformAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.InstanceTransformAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceTransformAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.InstanceTransformAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceParentAttribute InstanceParentAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.InstanceParentAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceParentAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.InstanceParentAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceFlagsAttribute InstanceFlagsAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.InstanceFlagsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceFlagsAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.InstanceFlagsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceMeshAttribute InstanceMeshAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.InstanceMeshAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceMeshAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.InstanceMeshAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshSubmeshOffsetAttribute MeshSubmeshOffsetAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MeshSubmeshOffsetAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshSubmeshOffsetAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MeshSubmeshOffsetAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.SubmeshIndexOffsetAttribute SubmeshIndexOffsetAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.SubmeshIndexOffsetAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.SubmeshIndexOffsetAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.SubmeshIndexOffsetAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.SubmeshMaterialAttribute SubmeshMaterialAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.SubmeshMaterialAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.SubmeshMaterialAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.SubmeshMaterialAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MaterialColorAttribute MaterialColorAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MaterialColorAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MaterialColorAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MaterialColorAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MaterialGlossinessAttribute MaterialGlossinessAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MaterialGlossinessAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MaterialGlossinessAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MaterialGlossinessAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MaterialSmoothnessAttribute MaterialSmoothnessAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MaterialSmoothnessAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MaterialSmoothnessAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MaterialSmoothnessAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.ShapeVertexAttribute ShapeVertexAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.ShapeVertexAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.ShapeVertexAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.ShapeVertexAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.ShapeVertexOffsetAttribute ShapeVertexOffsetAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.ShapeVertexOffsetAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.ShapeVertexOffsetAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.ShapeVertexOffsetAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.ShapeColorAttribute ShapeColorAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.ShapeColorAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.ShapeColorAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.ShapeColorAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.ShapeWidthAttribute ShapeWidthAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.ShapeWidthAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.ShapeWidthAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.ShapeWidthAttribute.AttributeName] = value as IAttribute;
        }

        /// <inheritdoc/>
        public IAttribute GetAttribute(Type attributeType)
        {

            if (attributeType == typeof(Vim.G3dNext.Attributes.CornersPerFaceAttribute))
                return CornersPerFaceAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.VertexAttribute))
                return VertexAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.IndexAttribute))
                return IndexAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceTransformAttribute))
                return InstanceTransformAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceParentAttribute))
                return InstanceParentAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceFlagsAttribute))
                return InstanceFlagsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceMeshAttribute))
                return InstanceMeshAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshSubmeshOffsetAttribute))
                return MeshSubmeshOffsetAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.SubmeshIndexOffsetAttribute))
                return SubmeshIndexOffsetAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.SubmeshMaterialAttribute))
                return SubmeshMaterialAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MaterialColorAttribute))
                return MaterialColorAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MaterialGlossinessAttribute))
                return MaterialGlossinessAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MaterialSmoothnessAttribute))
                return MaterialSmoothnessAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.ShapeVertexAttribute))
                return ShapeVertexAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.ShapeVertexOffsetAttribute))
                return ShapeVertexOffsetAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.ShapeColorAttribute))
                return ShapeColorAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.ShapeWidthAttribute))
                return ShapeWidthAttribute;

            throw new ArgumentException("Type {attributeType.ToString()} is not supported.");
        }

        public IAttribute MergeAttribute(string attributeName, IReadOnlyList<IAttributeCollection> otherCollections)
        {
            var collections = otherCollections.Prepend(this).ToArray();
            switch (attributeName)
            {

                case Vim.G3dNext.Attributes.CornersPerFaceAttribute.AttributeName:
                {
                    // Singleton Attribute (no merging)
                    return CornersPerFaceAttribute;
                }

                case Vim.G3dNext.Attributes.VertexAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.VertexAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.VertexAttribute, Vim.Math3d.Vector3>();
                }

                case Vim.G3dNext.Attributes.IndexAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.IndexAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.InstanceTransformAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.InstanceTransformAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.InstanceTransformAttribute, Vim.Math3d.Matrix4x4>();
                }

                case Vim.G3dNext.Attributes.InstanceParentAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.InstanceParentAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.InstanceFlagsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.InstanceFlagsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.InstanceFlagsAttribute, System.UInt16>();
                }

                case Vim.G3dNext.Attributes.InstanceMeshAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.InstanceMeshAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.MeshSubmeshOffsetAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.MeshSubmeshOffsetAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.SubmeshIndexOffsetAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.SubmeshIndexOffsetAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.SubmeshMaterialAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.SubmeshMaterialAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.MaterialColorAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.MaterialColorAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.MaterialColorAttribute, Vim.Math3d.Vector4>();
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

                case Vim.G3dNext.Attributes.ShapeVertexAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.ShapeVertexAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.ShapeVertexAttribute, Vim.Math3d.Vector3>();
                }

                case Vim.G3dNext.Attributes.ShapeVertexOffsetAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.ShapeVertexOffsetAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.ShapeColorAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.ShapeColorAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.ShapeColorAttribute, Vim.Math3d.Vector4>();
                }

                case Vim.G3dNext.Attributes.ShapeWidthAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.ShapeWidthAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.ShapeWidthAttribute, System.Single>();
                }

                default:
                    throw new ArgumentException(nameof(attributeName));
            }
        }

        public void Validate() 
        {
            // Ensure all the indices are either -1 or within the bounds of the attributes they are indexing into.

            {
                var maxIndex = GetAttribute(IndexAttribute.IndexInto).Data.Length - 1;
                for (var i = 0; i < IndexAttribute.TypedData.Length; ++i)
                {
                    var index = IndexAttribute.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.IndexAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(InstanceParentAttribute.IndexInto).Data.Length - 1;
                for (var i = 0; i < InstanceParentAttribute.TypedData.Length; ++i)
                {
                    var index = InstanceParentAttribute.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.InstanceParentAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(InstanceMeshAttribute.IndexInto).Data.Length - 1;
                for (var i = 0; i < InstanceMeshAttribute.TypedData.Length; ++i)
                {
                    var index = InstanceMeshAttribute.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.InstanceMeshAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(MeshSubmeshOffsetAttribute.IndexInto).Data.Length - 1;
                for (var i = 0; i < MeshSubmeshOffsetAttribute.TypedData.Length; ++i)
                {
                    var index = MeshSubmeshOffsetAttribute.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.MeshSubmeshOffsetAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(SubmeshIndexOffsetAttribute.IndexInto).Data.Length - 1;
                for (var i = 0; i < SubmeshIndexOffsetAttribute.TypedData.Length; ++i)
                {
                    var index = SubmeshIndexOffsetAttribute.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.SubmeshIndexOffsetAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(SubmeshMaterialAttribute.IndexInto).Data.Length - 1;
                for (var i = 0; i < SubmeshMaterialAttribute.TypedData.Length; ++i)
                {
                    var index = SubmeshMaterialAttribute.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.SubmeshMaterialAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(ShapeVertexOffsetAttribute.IndexInto).Data.Length - 1;
                for (var i = 0; i < ShapeVertexOffsetAttribute.TypedData.Length; ++i)
                {
                    var index = ShapeVertexOffsetAttribute.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.ShapeVertexOffsetAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }
        }
    }
    
    public class G3dMaterial2
    {
        public G3DNext<MaterialAttributeCollection> source;

        public G3dMaterial2()
        {
            this.source = new G3DNext<MaterialAttributeCollection> ();
        }

        public G3dMaterial2(G3DNext<MaterialAttributeCollection> source)
        {
            this.source = source;
        }

        public G3dMaterial2(BFastNext.BFastNext bfast)
        {
            this.source = new G3DNext<MaterialAttributeCollection>(bfast);
        }

        
        public Vim.Math3d.Vector4[] MaterialColor
        {
            get => source.AttributeCollection.MaterialColorAttribute.TypedData;
            set => source.AttributeCollection.MaterialColorAttribute.TypedData = value;
        }

        public System.Single[] MaterialGlossiness
        {
            get => source.AttributeCollection.MaterialGlossinessAttribute.TypedData;
            set => source.AttributeCollection.MaterialGlossinessAttribute.TypedData = value;
        }

        public System.Single[] MaterialSmoothness
        {
            get => source.AttributeCollection.MaterialSmoothnessAttribute.TypedData;
            set => source.AttributeCollection.MaterialSmoothnessAttribute.TypedData = value;
        }
    }

    
    public partial class MaterialAttributeCollection : IAttributeCollection
    {
        public IEnumerable<string> AttributeNames
            => Attributes.Keys;

        public long GetSize()
            => Attributes.Values.Sum(a => a.Data.LongLength * a.AttributeDescriptor.DataElementSize);

        public IDictionary<string, IAttribute> Attributes { get; }
            = new Dictionary<string, IAttribute>
            {
                [Vim.G3dNext.Attributes.MaterialColorAttribute.AttributeName] = new Vim.G3dNext.Attributes.MaterialColorAttribute(),
                [Vim.G3dNext.Attributes.MaterialGlossinessAttribute.AttributeName] = new Vim.G3dNext.Attributes.MaterialGlossinessAttribute(),
                [Vim.G3dNext.Attributes.MaterialSmoothnessAttribute.AttributeName] = new Vim.G3dNext.Attributes.MaterialSmoothnessAttribute(),
            };

        // Named Attributes.

        public Vim.G3dNext.Attributes.MaterialColorAttribute MaterialColorAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MaterialColorAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MaterialColorAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MaterialColorAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MaterialGlossinessAttribute MaterialGlossinessAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MaterialGlossinessAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MaterialGlossinessAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MaterialGlossinessAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MaterialSmoothnessAttribute MaterialSmoothnessAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MaterialSmoothnessAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MaterialSmoothnessAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MaterialSmoothnessAttribute.AttributeName] = value as IAttribute;
        }

        /// <inheritdoc/>
        public IAttribute GetAttribute(Type attributeType)
        {

            if (attributeType == typeof(Vim.G3dNext.Attributes.MaterialColorAttribute))
                return MaterialColorAttribute;

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

                case Vim.G3dNext.Attributes.MaterialColorAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.MaterialColorAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.MaterialColorAttribute, Vim.Math3d.Vector4>();
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
    
    public class G3dScene2
    {
        public G3DNext<SceneAttributeCollection> source;

        public G3dScene2()
        {
            this.source = new G3DNext<SceneAttributeCollection> ();
        }

        public G3dScene2(G3DNext<SceneAttributeCollection> source)
        {
            this.source = source;
        }

        public G3dScene2(BFastNext.BFastNext bfast)
        {
            this.source = new G3DNext<SceneAttributeCollection>(bfast);
        }

        
        public System.Int32[] InstanceFiles
        {
            get => source.AttributeCollection.InstanceFilesAttribute.TypedData;
            set => source.AttributeCollection.InstanceFilesAttribute.TypedData = value;
        }

        public System.Int32[] InstanceIndices
        {
            get => source.AttributeCollection.InstanceIndicesAttribute.TypedData;
            set => source.AttributeCollection.InstanceIndicesAttribute.TypedData = value;
        }

        public System.Int32[] InstanceNodes
        {
            get => source.AttributeCollection.InstanceNodesAttribute.TypedData;
            set => source.AttributeCollection.InstanceNodesAttribute.TypedData = value;
        }

        public System.Int32[] InstanceGroups
        {
            get => source.AttributeCollection.InstanceGroupsAttribute.TypedData;
            set => source.AttributeCollection.InstanceGroupsAttribute.TypedData = value;
        }

        public System.Int64[] InstanceTags
        {
            get => source.AttributeCollection.InstanceTagsAttribute.TypedData;
            set => source.AttributeCollection.InstanceTagsAttribute.TypedData = value;
        }

        public System.UInt16[] InstanceFlags
        {
            get => source.AttributeCollection.InstanceFlagsAttribute.TypedData;
            set => source.AttributeCollection.InstanceFlagsAttribute.TypedData = value;
        }

        public Vim.Math3d.Vector3[] InstanceMins
        {
            get => source.AttributeCollection.InstanceMinsAttribute.TypedData;
            set => source.AttributeCollection.InstanceMinsAttribute.TypedData = value;
        }

        public Vim.Math3d.Vector3[] InstanceMaxs
        {
            get => source.AttributeCollection.InstanceMaxsAttribute.TypedData;
            set => source.AttributeCollection.InstanceMaxsAttribute.TypedData = value;
        }

        public System.Int32[] MeshInstanceCounts
        {
            get => source.AttributeCollection.MeshInstanceCountsAttribute.TypedData;
            set => source.AttributeCollection.MeshInstanceCountsAttribute.TypedData = value;
        }

        public System.Int32[] MeshIndexCounts
        {
            get => source.AttributeCollection.MeshIndexCountsAttribute.TypedData;
            set => source.AttributeCollection.MeshIndexCountsAttribute.TypedData = value;
        }

        public System.Int32[] MeshVertexCounts
        {
            get => source.AttributeCollection.MeshVertexCountsAttribute.TypedData;
            set => source.AttributeCollection.MeshVertexCountsAttribute.TypedData = value;
        }

        public System.Int32[] MeshOpaqueIndexCounts
        {
            get => source.AttributeCollection.MeshOpaqueIndexCountsAttribute.TypedData;
            set => source.AttributeCollection.MeshOpaqueIndexCountsAttribute.TypedData = value;
        }

        public System.Int32[] MeshOpaqueVertexCounts
        {
            get => source.AttributeCollection.MeshOpaqueVertexCountsAttribute.TypedData;
            set => source.AttributeCollection.MeshOpaqueVertexCountsAttribute.TypedData = value;
        }
    }

    
    public partial class SceneAttributeCollection : IAttributeCollection
    {
        public IEnumerable<string> AttributeNames
            => Attributes.Keys;

        public long GetSize()
            => Attributes.Values.Sum(a => a.Data.LongLength * a.AttributeDescriptor.DataElementSize);

        public IDictionary<string, IAttribute> Attributes { get; }
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
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.InstanceFilesAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceFilesAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.InstanceFilesAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceIndicesAttribute InstanceIndicesAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.InstanceIndicesAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceIndicesAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.InstanceIndicesAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceNodesAttribute InstanceNodesAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.InstanceNodesAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceNodesAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.InstanceNodesAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceGroupsAttribute InstanceGroupsAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.InstanceGroupsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceGroupsAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.InstanceGroupsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceTagsAttribute InstanceTagsAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.InstanceTagsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceTagsAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.InstanceTagsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceFlagsAttribute InstanceFlagsAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.InstanceFlagsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceFlagsAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.InstanceFlagsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceMinsAttribute InstanceMinsAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.InstanceMinsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceMinsAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.InstanceMinsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceMaxsAttribute InstanceMaxsAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.InstanceMaxsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceMaxsAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.InstanceMaxsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshInstanceCountsAttribute MeshInstanceCountsAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MeshInstanceCountsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshInstanceCountsAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MeshInstanceCountsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshIndexCountsAttribute MeshIndexCountsAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MeshIndexCountsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshIndexCountsAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MeshIndexCountsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshVertexCountsAttribute MeshVertexCountsAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MeshVertexCountsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshVertexCountsAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MeshVertexCountsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshOpaqueIndexCountsAttribute MeshOpaqueIndexCountsAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MeshOpaqueIndexCountsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshOpaqueIndexCountsAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MeshOpaqueIndexCountsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshOpaqueVertexCountsAttribute MeshOpaqueVertexCountsAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MeshOpaqueVertexCountsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshOpaqueVertexCountsAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MeshOpaqueVertexCountsAttribute.AttributeName] = value as IAttribute;
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
    
    public class G3dMesh2
    {
        public G3DNext<MeshAttributeCollection> source;

        public G3dMesh2()
        {
            this.source = new G3DNext<MeshAttributeCollection> ();
        }

        public G3dMesh2(G3DNext<MeshAttributeCollection> source)
        {
            this.source = source;
        }

        public G3dMesh2(BFastNext.BFastNext bfast)
        {
            this.source = new G3DNext<MeshAttributeCollection>(bfast);
        }

        
        public Vim.Math3d.Matrix4x4[] MeshInstanceTransform
        {
            get => source.AttributeCollection.MeshInstanceTransformAttribute.TypedData;
            set => source.AttributeCollection.MeshInstanceTransformAttribute.TypedData = value;
        }

        public System.Int32[] MeshOpaqueSubmeshCount
        {
            get => source.AttributeCollection.MeshOpaqueSubmeshCountAttribute.TypedData;
            set => source.AttributeCollection.MeshOpaqueSubmeshCountAttribute.TypedData = value;
        }

        public System.Int32[] MeshSubmeshIndexOffset
        {
            get => source.AttributeCollection.MeshSubmeshIndexOffsetAttribute.TypedData;
            set => source.AttributeCollection.MeshSubmeshIndexOffsetAttribute.TypedData = value;
        }

        public System.Int32[] MeshSubmeshVertexOffset
        {
            get => source.AttributeCollection.MeshSubmeshVertexOffsetAttribute.TypedData;
            set => source.AttributeCollection.MeshSubmeshVertexOffsetAttribute.TypedData = value;
        }

        public System.Int32[] MeshSubmeshMaterial
        {
            get => source.AttributeCollection.MeshSubmeshMaterialAttribute.TypedData;
            set => source.AttributeCollection.MeshSubmeshMaterialAttribute.TypedData = value;
        }

        public Vim.Math3d.Vector3[] MeshVertex
        {
            get => source.AttributeCollection.MeshVertexAttribute.TypedData;
            set => source.AttributeCollection.MeshVertexAttribute.TypedData = value;
        }

        public System.Int32[] MeshIndex
        {
            get => source.AttributeCollection.MeshIndexAttribute.TypedData;
            set => source.AttributeCollection.MeshIndexAttribute.TypedData = value;
        }
    }

    
    public partial class MeshAttributeCollection : IAttributeCollection
    {
        public IEnumerable<string> AttributeNames
            => Attributes.Keys;

        public long GetSize()
            => Attributes.Values.Sum(a => a.Data.LongLength * a.AttributeDescriptor.DataElementSize);

        public IDictionary<string, IAttribute> Attributes { get; }
            = new Dictionary<string, IAttribute>
            {
                [Vim.G3dNext.Attributes.MeshInstanceTransformAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshInstanceTransformAttribute(),
                [Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountAttribute(),
                [Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetAttribute(),
                [Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetAttribute(),
                [Vim.G3dNext.Attributes.MeshSubmeshMaterialAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshSubmeshMaterialAttribute(),
                [Vim.G3dNext.Attributes.MeshVertexAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshVertexAttribute(),
                [Vim.G3dNext.Attributes.MeshIndexAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshIndexAttribute(),
            };

        // Named Attributes.

        public Vim.G3dNext.Attributes.MeshInstanceTransformAttribute MeshInstanceTransformAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MeshInstanceTransformAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshInstanceTransformAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MeshInstanceTransformAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountAttribute MeshOpaqueSubmeshCountAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetAttribute MeshSubmeshIndexOffsetAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetAttribute MeshSubmeshVertexOffsetAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshSubmeshMaterialAttribute MeshSubmeshMaterialAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MeshSubmeshMaterialAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshSubmeshMaterialAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MeshSubmeshMaterialAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshVertexAttribute MeshVertexAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MeshVertexAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshVertexAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MeshVertexAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshIndexAttribute MeshIndexAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MeshIndexAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshIndexAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MeshIndexAttribute.AttributeName] = value as IAttribute;
        }

        /// <inheritdoc/>
        public IAttribute GetAttribute(Type attributeType)
        {

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshInstanceTransformAttribute))
                return MeshInstanceTransformAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountAttribute))
                return MeshOpaqueSubmeshCountAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetAttribute))
                return MeshSubmeshIndexOffsetAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetAttribute))
                return MeshSubmeshVertexOffsetAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshSubmeshMaterialAttribute))
                return MeshSubmeshMaterialAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshVertexAttribute))
                return MeshVertexAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshIndexAttribute))
                return MeshIndexAttribute;

            throw new ArgumentException("Type {attributeType.ToString()} is not supported.");
        }

        public IAttribute MergeAttribute(string attributeName, IReadOnlyList<IAttributeCollection> otherCollections)
        {
            var collections = otherCollections.Prepend(this).ToArray();
            switch (attributeName)
            {

                case Vim.G3dNext.Attributes.MeshInstanceTransformAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.MeshInstanceTransformAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.MeshInstanceTransformAttribute, Vim.Math3d.Matrix4x4>();
                }

                case Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.MeshSubmeshMaterialAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.MeshSubmeshMaterialAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.MeshVertexAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.MeshVertexAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.MeshVertexAttribute, Vim.Math3d.Vector3>();
                }

                case Vim.G3dNext.Attributes.MeshIndexAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.MeshIndexAttribute>().MergeIndexAttributes();
                }

                default:
                    throw new ArgumentException(nameof(attributeName));
            }
        }

        public void Validate() 
        {
            // Ensure all the indices are either -1 or within the bounds of the attributes they are indexing into.

            {
                var maxIndex = GetAttribute(MeshSubmeshIndexOffsetAttribute.IndexInto).Data.Length - 1;
                for (var i = 0; i < MeshSubmeshIndexOffsetAttribute.TypedData.Length; ++i)
                {
                    var index = MeshSubmeshIndexOffsetAttribute.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.MeshSubmeshIndexOffsetAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(MeshSubmeshVertexOffsetAttribute.IndexInto).Data.Length - 1;
                for (var i = 0; i < MeshSubmeshVertexOffsetAttribute.TypedData.Length; ++i)
                {
                    var index = MeshSubmeshVertexOffsetAttribute.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.MeshSubmeshVertexOffsetAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(MeshSubmeshMaterialAttribute.IndexInto).Data.Length - 1;
                for (var i = 0; i < MeshSubmeshMaterialAttribute.TypedData.Length; ++i)
                {
                    var index = MeshSubmeshMaterialAttribute.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.MeshSubmeshMaterialAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }

            {
                var maxIndex = GetAttribute(MeshIndexAttribute.IndexInto).Data.Length - 1;
                for (var i = 0; i < MeshIndexAttribute.TypedData.Length; ++i)
                {
                    var index = MeshIndexAttribute.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.MeshIndexAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
                }
            }
        }
    }
}
