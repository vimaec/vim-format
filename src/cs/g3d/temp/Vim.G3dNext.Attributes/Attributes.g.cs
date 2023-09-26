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

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<CornersPerFaceAttribute, System.Int32>();

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

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<VertexAttribute, Vim.Math3d.Vector3>();

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

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<IndexAttribute, System.Int32>();

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

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<InstanceTransformAttribute, Vim.Math3d.Matrix4x4>();

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

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<InstanceParentAttribute, System.Int32>();

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

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<InstanceFlagsAttribute, System.UInt16>();

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

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<InstanceMeshAttribute, System.Int32>();

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

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<MeshSubmeshOffsetAttribute, System.Int32>();

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

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<SubmeshIndexOffsetAttribute, System.Int32>();

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

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<SubmeshMaterialAttribute, System.Int32>();

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

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<ShapeVertexAttribute, Vim.Math3d.Vector3>();

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

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<ShapeVertexOffsetAttribute, System.Int32>();

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

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<ShapeColorAttribute, Vim.Math3d.Vector4>();

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

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<ShapeWidthAttribute, System.Single>();

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
    
    public partial class SubmeshVertexOffsetAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:submesh:vertexoffset:0:int32:1";

        public string Name
            => AttributeName;

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<SubmeshVertexOffsetAttribute, System.Int32>();

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
    
    public partial class MeshOpaqueSubmeshCountAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:mesh:opaquesubmeshcount:0:int32:1";

        public string Name
            => AttributeName;

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<MeshOpaqueSubmeshCountAttribute, System.Int32>();

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
    
    public partial class InstanceFileAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:instance:file:0:int32:1";

        public string Name
            => AttributeName;

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<InstanceFileAttribute, System.Int32>();

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
    
    public partial class InstanceNodeAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:instance:node:0:int32:1";

        public string Name
            => AttributeName;

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<InstanceNodeAttribute, System.Int32>();

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
    
    public partial class InstanceIndexAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:instance:index:0:int32:1";

        public string Name
            => AttributeName;

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<InstanceIndexAttribute, System.Int32>();

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
    
    public partial class InstanceGroupAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:instance:group:0:int32:1";

        public string Name
            => AttributeName;

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<InstanceGroupAttribute, System.Int32>();

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
    
    public partial class InstanceTagAttribute : IAttribute<System.Int64>
    {
        public const string AttributeName = "g3d:instance:tag:0:int64:1";

        public string Name
            => AttributeName;

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<InstanceTagAttribute, System.Int64>();

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
    
    public partial class InstanceMinAttribute : IAttribute<Vim.Math3d.Vector3>
    {
        public const string AttributeName = "g3d:instance:min:0:float32:3";

        public string Name
            => AttributeName;

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<InstanceMinAttribute, Vim.Math3d.Vector3>();

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
    
    public partial class InstanceMaxAttribute : IAttribute<Vim.Math3d.Vector3>
    {
        public const string AttributeName = "g3d:instance:max:0:float32:3";

        public string Name
            => AttributeName;

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<InstanceMaxAttribute, Vim.Math3d.Vector3>();

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
    
    public partial class MeshInstanceCountAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:mesh:instancecount:0:int32:1";

        public string Name
            => AttributeName;

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<MeshInstanceCountAttribute, System.Int32>();

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
    
    public partial class MeshVertexCountAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:mesh:vertexcount:0:int32:1";

        public string Name
            => AttributeName;

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<MeshVertexCountAttribute, System.Int32>();

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
    
    public partial class MeshIndexCountAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:mesh:indexcount:0:int32:1";

        public string Name
            => AttributeName;

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<MeshIndexCountAttribute, System.Int32>();

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
    
    public partial class MeshOpaqueVertexCountAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:mesh:opaquevertexcount:0:int32:1";

        public string Name
            => AttributeName;

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<MeshOpaqueVertexCountAttribute, System.Int32>();

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
    
    public partial class MeshOpaqueIndexCountAttribute : IAttribute<System.Int32>
    {
        public const string AttributeName = "g3d:mesh:opaqueindexcount:0:int32:1";

        public string Name
            => AttributeName;

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<MeshOpaqueIndexCountAttribute, System.Int32>();

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
    
    public partial class MaterialColorAttribute : IAttribute<Vim.Math3d.Vector4>
    {
        public const string AttributeName = "g3d:material:color:0:float32:4";

        public string Name
            => AttributeName;

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<MaterialColorAttribute, Vim.Math3d.Vector4>();

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

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<MaterialGlossinessAttribute, System.Single>();

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

        public static AttributeReader CreateAttributeReader()
            => AttributeCollectionExtensions.CreateAttributeReader<MaterialSmoothnessAttribute, System.Single>();

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

        public IDictionary<string, AttributeReader> AttributeReaders { get; }
            = new Dictionary<string, AttributeReader>
            {
                [Vim.G3dNext.Attributes.CornersPerFaceAttribute.AttributeName] = Vim.G3dNext.Attributes.CornersPerFaceAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.VertexAttribute.AttributeName] = Vim.G3dNext.Attributes.VertexAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.IndexAttribute.AttributeName] = Vim.G3dNext.Attributes.IndexAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.InstanceTransformAttribute.AttributeName] = Vim.G3dNext.Attributes.InstanceTransformAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.InstanceParentAttribute.AttributeName] = Vim.G3dNext.Attributes.InstanceParentAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.InstanceFlagsAttribute.AttributeName] = Vim.G3dNext.Attributes.InstanceFlagsAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.InstanceMeshAttribute.AttributeName] = Vim.G3dNext.Attributes.InstanceMeshAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.MeshSubmeshOffsetAttribute.AttributeName] = Vim.G3dNext.Attributes.MeshSubmeshOffsetAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.SubmeshIndexOffsetAttribute.AttributeName] = Vim.G3dNext.Attributes.SubmeshIndexOffsetAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.SubmeshMaterialAttribute.AttributeName] = Vim.G3dNext.Attributes.SubmeshMaterialAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.MaterialColorAttribute.AttributeName] = Vim.G3dNext.Attributes.MaterialColorAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.MaterialGlossinessAttribute.AttributeName] = Vim.G3dNext.Attributes.MaterialGlossinessAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.MaterialSmoothnessAttribute.AttributeName] = Vim.G3dNext.Attributes.MaterialSmoothnessAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.ShapeVertexAttribute.AttributeName] = Vim.G3dNext.Attributes.ShapeVertexAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.ShapeVertexOffsetAttribute.AttributeName] = Vim.G3dNext.Attributes.ShapeVertexOffsetAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.ShapeColorAttribute.AttributeName] = Vim.G3dNext.Attributes.ShapeColorAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.ShapeWidthAttribute.AttributeName] = Vim.G3dNext.Attributes.ShapeWidthAttribute.CreateAttributeReader(),
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
        public partial class MeshAttributeCollection : IAttributeCollection
    {
        public IEnumerable<string> AttributeNames
            => Attributes.Keys;

        public long GetSize()
            => Attributes.Values.Sum(a => a.Data.LongLength * a.AttributeDescriptor.DataElementSize);

        public IDictionary<string, IAttribute> Attributes { get; }
            = new Dictionary<string, IAttribute>
            {
                [Vim.G3dNext.Attributes.InstanceTransformAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceTransformAttribute(),
                [Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountAttribute(),
                [Vim.G3dNext.Attributes.SubmeshIndexOffsetAttribute.AttributeName] = new Vim.G3dNext.Attributes.SubmeshIndexOffsetAttribute(),
                [Vim.G3dNext.Attributes.SubmeshVertexOffsetAttribute.AttributeName] = new Vim.G3dNext.Attributes.SubmeshVertexOffsetAttribute(),
                [Vim.G3dNext.Attributes.SubmeshMaterialAttribute.AttributeName] = new Vim.G3dNext.Attributes.SubmeshMaterialAttribute(),
                [Vim.G3dNext.Attributes.VertexAttribute.AttributeName] = new Vim.G3dNext.Attributes.VertexAttribute(),
                [Vim.G3dNext.Attributes.IndexAttribute.AttributeName] = new Vim.G3dNext.Attributes.IndexAttribute(),
            };

        public IDictionary<string, AttributeReader> AttributeReaders { get; }
            = new Dictionary<string, AttributeReader>
            {
                [Vim.G3dNext.Attributes.InstanceTransformAttribute.AttributeName] = Vim.G3dNext.Attributes.InstanceTransformAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountAttribute.AttributeName] = Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.SubmeshIndexOffsetAttribute.AttributeName] = Vim.G3dNext.Attributes.SubmeshIndexOffsetAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.SubmeshVertexOffsetAttribute.AttributeName] = Vim.G3dNext.Attributes.SubmeshVertexOffsetAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.SubmeshMaterialAttribute.AttributeName] = Vim.G3dNext.Attributes.SubmeshMaterialAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.VertexAttribute.AttributeName] = Vim.G3dNext.Attributes.VertexAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.IndexAttribute.AttributeName] = Vim.G3dNext.Attributes.IndexAttribute.CreateAttributeReader(),
            };

        // Named Attributes.

        public Vim.G3dNext.Attributes.InstanceTransformAttribute InstanceTransformAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.InstanceTransformAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceTransformAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.InstanceTransformAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountAttribute MeshOpaqueSubmeshCountAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.SubmeshIndexOffsetAttribute SubmeshIndexOffsetAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.SubmeshIndexOffsetAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.SubmeshIndexOffsetAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.SubmeshIndexOffsetAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.SubmeshVertexOffsetAttribute SubmeshVertexOffsetAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.SubmeshVertexOffsetAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.SubmeshVertexOffsetAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.SubmeshVertexOffsetAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.SubmeshMaterialAttribute SubmeshMaterialAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.SubmeshMaterialAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.SubmeshMaterialAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.SubmeshMaterialAttribute.AttributeName] = value as IAttribute;
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

        /// <inheritdoc/>
        public IAttribute GetAttribute(Type attributeType)
        {

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceTransformAttribute))
                return InstanceTransformAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountAttribute))
                return MeshOpaqueSubmeshCountAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.SubmeshIndexOffsetAttribute))
                return SubmeshIndexOffsetAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.SubmeshVertexOffsetAttribute))
                return SubmeshVertexOffsetAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.SubmeshMaterialAttribute))
                return SubmeshMaterialAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.VertexAttribute))
                return VertexAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.IndexAttribute))
                return IndexAttribute;

            throw new ArgumentException("Type {attributeType.ToString()} is not supported.");
        }

        public IAttribute MergeAttribute(string attributeName, IReadOnlyList<IAttributeCollection> otherCollections)
        {
            var collections = otherCollections.Prepend(this).ToArray();
            switch (attributeName)
            {

                case Vim.G3dNext.Attributes.InstanceTransformAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.InstanceTransformAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.InstanceTransformAttribute, Vim.Math3d.Matrix4x4>();
                }

                case Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.MeshOpaqueSubmeshCountAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.SubmeshIndexOffsetAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.SubmeshIndexOffsetAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.SubmeshVertexOffsetAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.SubmeshVertexOffsetAttribute>().MergeIndexAttributes();
                }

                case Vim.G3dNext.Attributes.SubmeshMaterialAttribute.AttributeName:
                {
                    // Index Attribute
                    return collections.GetIndexedAttributesOfType<Vim.G3dNext.Attributes.SubmeshMaterialAttribute>().MergeIndexAttributes();
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

                default:
                    throw new ArgumentException(nameof(attributeName));
            }
        }

        public void Validate() 
        {
            // Ensure all the indices are either -1 or within the bounds of the attributes they are indexing into.

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
                var maxIndex = GetAttribute(SubmeshVertexOffsetAttribute.IndexInto).Data.Length - 1;
                for (var i = 0; i < SubmeshVertexOffsetAttribute.TypedData.Length; ++i)
                {
                    var index = SubmeshVertexOffsetAttribute.TypedData[i];

                    if (index == -1)
                        continue; // no relation.

                    if (index < -1 || index > maxIndex)
                        throw new Exception($"Index out of range in Vim.G3dNext.Attributes.SubmeshVertexOffsetAttribute at position {i}. Expected either -1 for no relation, or a maximum of {maxIndex} but got {index}");
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
                [Vim.G3dNext.Attributes.InstanceFileAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceFileAttribute(),
                [Vim.G3dNext.Attributes.InstanceIndexAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceIndexAttribute(),
                [Vim.G3dNext.Attributes.InstanceNodeAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceNodeAttribute(),
                [Vim.G3dNext.Attributes.InstanceGroupAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceGroupAttribute(),
                [Vim.G3dNext.Attributes.InstanceTagAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceTagAttribute(),
                [Vim.G3dNext.Attributes.InstanceFlagsAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceFlagsAttribute(),
                [Vim.G3dNext.Attributes.InstanceMinAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceMinAttribute(),
                [Vim.G3dNext.Attributes.InstanceMaxAttribute.AttributeName] = new Vim.G3dNext.Attributes.InstanceMaxAttribute(),
                [Vim.G3dNext.Attributes.MeshInstanceCountAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshInstanceCountAttribute(),
                [Vim.G3dNext.Attributes.MeshIndexCountAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshIndexCountAttribute(),
                [Vim.G3dNext.Attributes.MeshVertexCountAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshVertexCountAttribute(),
                [Vim.G3dNext.Attributes.MeshOpaqueIndexCountAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshOpaqueIndexCountAttribute(),
                [Vim.G3dNext.Attributes.MeshOpaqueVertexCountAttribute.AttributeName] = new Vim.G3dNext.Attributes.MeshOpaqueVertexCountAttribute(),
            };

        public IDictionary<string, AttributeReader> AttributeReaders { get; }
            = new Dictionary<string, AttributeReader>
            {
                [Vim.G3dNext.Attributes.InstanceFileAttribute.AttributeName] = Vim.G3dNext.Attributes.InstanceFileAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.InstanceIndexAttribute.AttributeName] = Vim.G3dNext.Attributes.InstanceIndexAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.InstanceNodeAttribute.AttributeName] = Vim.G3dNext.Attributes.InstanceNodeAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.InstanceGroupAttribute.AttributeName] = Vim.G3dNext.Attributes.InstanceGroupAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.InstanceTagAttribute.AttributeName] = Vim.G3dNext.Attributes.InstanceTagAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.InstanceFlagsAttribute.AttributeName] = Vim.G3dNext.Attributes.InstanceFlagsAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.InstanceMinAttribute.AttributeName] = Vim.G3dNext.Attributes.InstanceMinAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.InstanceMaxAttribute.AttributeName] = Vim.G3dNext.Attributes.InstanceMaxAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.MeshInstanceCountAttribute.AttributeName] = Vim.G3dNext.Attributes.MeshInstanceCountAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.MeshIndexCountAttribute.AttributeName] = Vim.G3dNext.Attributes.MeshIndexCountAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.MeshVertexCountAttribute.AttributeName] = Vim.G3dNext.Attributes.MeshVertexCountAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.MeshOpaqueIndexCountAttribute.AttributeName] = Vim.G3dNext.Attributes.MeshOpaqueIndexCountAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.MeshOpaqueVertexCountAttribute.AttributeName] = Vim.G3dNext.Attributes.MeshOpaqueVertexCountAttribute.CreateAttributeReader(),
            };

        // Named Attributes.

        public Vim.G3dNext.Attributes.InstanceFileAttribute InstanceFileAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.InstanceFileAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceFileAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.InstanceFileAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceIndexAttribute InstanceIndexAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.InstanceIndexAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceIndexAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.InstanceIndexAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceNodeAttribute InstanceNodeAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.InstanceNodeAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceNodeAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.InstanceNodeAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceGroupAttribute InstanceGroupAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.InstanceGroupAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceGroupAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.InstanceGroupAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceTagAttribute InstanceTagAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.InstanceTagAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceTagAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.InstanceTagAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceFlagsAttribute InstanceFlagsAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.InstanceFlagsAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceFlagsAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.InstanceFlagsAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceMinAttribute InstanceMinAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.InstanceMinAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceMinAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.InstanceMinAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.InstanceMaxAttribute InstanceMaxAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.InstanceMaxAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.InstanceMaxAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.InstanceMaxAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshInstanceCountAttribute MeshInstanceCountAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MeshInstanceCountAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshInstanceCountAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MeshInstanceCountAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshIndexCountAttribute MeshIndexCountAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MeshIndexCountAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshIndexCountAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MeshIndexCountAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshVertexCountAttribute MeshVertexCountAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MeshVertexCountAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshVertexCountAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MeshVertexCountAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshOpaqueIndexCountAttribute MeshOpaqueIndexCountAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MeshOpaqueIndexCountAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshOpaqueIndexCountAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MeshOpaqueIndexCountAttribute.AttributeName] = value as IAttribute;
        }

        public Vim.G3dNext.Attributes.MeshOpaqueVertexCountAttribute MeshOpaqueVertexCountAttribute
        {
            get => Attributes.TryGetValue(Vim.G3dNext.Attributes.MeshOpaqueVertexCountAttribute.AttributeName, out var attr) ? attr as Vim.G3dNext.Attributes.MeshOpaqueVertexCountAttribute : default;
            set => Attributes[Vim.G3dNext.Attributes.MeshOpaqueVertexCountAttribute.AttributeName] = value as IAttribute;
        }

        /// <inheritdoc/>
        public IAttribute GetAttribute(Type attributeType)
        {

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceFileAttribute))
                return InstanceFileAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceIndexAttribute))
                return InstanceIndexAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceNodeAttribute))
                return InstanceNodeAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceGroupAttribute))
                return InstanceGroupAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceTagAttribute))
                return InstanceTagAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceFlagsAttribute))
                return InstanceFlagsAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceMinAttribute))
                return InstanceMinAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.InstanceMaxAttribute))
                return InstanceMaxAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshInstanceCountAttribute))
                return MeshInstanceCountAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshIndexCountAttribute))
                return MeshIndexCountAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshVertexCountAttribute))
                return MeshVertexCountAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshOpaqueIndexCountAttribute))
                return MeshOpaqueIndexCountAttribute;

            if (attributeType == typeof(Vim.G3dNext.Attributes.MeshOpaqueVertexCountAttribute))
                return MeshOpaqueVertexCountAttribute;

            throw new ArgumentException("Type {attributeType.ToString()} is not supported.");
        }

        public IAttribute MergeAttribute(string attributeName, IReadOnlyList<IAttributeCollection> otherCollections)
        {
            var collections = otherCollections.Prepend(this).ToArray();
            switch (attributeName)
            {

                case Vim.G3dNext.Attributes.InstanceFileAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.InstanceFileAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.InstanceFileAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.InstanceIndexAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.InstanceIndexAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.InstanceIndexAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.InstanceNodeAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.InstanceNodeAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.InstanceNodeAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.InstanceGroupAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.InstanceGroupAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.InstanceGroupAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.InstanceTagAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.InstanceTagAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.InstanceTagAttribute, System.Int64>();
                }

                case Vim.G3dNext.Attributes.InstanceFlagsAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.InstanceFlagsAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.InstanceFlagsAttribute, System.UInt16>();
                }

                case Vim.G3dNext.Attributes.InstanceMinAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.InstanceMinAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.InstanceMinAttribute, Vim.Math3d.Vector3>();
                }

                case Vim.G3dNext.Attributes.InstanceMaxAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.InstanceMaxAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.InstanceMaxAttribute, Vim.Math3d.Vector3>();
                }

                case Vim.G3dNext.Attributes.MeshInstanceCountAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.MeshInstanceCountAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.MeshInstanceCountAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.MeshIndexCountAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.MeshIndexCountAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.MeshIndexCountAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.MeshVertexCountAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.MeshVertexCountAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.MeshVertexCountAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.MeshOpaqueIndexCountAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.MeshOpaqueIndexCountAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.MeshOpaqueIndexCountAttribute, System.Int32>();
                }

                case Vim.G3dNext.Attributes.MeshOpaqueVertexCountAttribute.AttributeName:
                {
                    // Data Attribute
                    return collections.GetAttributesOfType<Vim.G3dNext.Attributes.MeshOpaqueVertexCountAttribute>().ToArray().MergeDataAttributes<Vim.G3dNext.Attributes.MeshOpaqueVertexCountAttribute, System.Int32>();
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

        public IDictionary<string, AttributeReader> AttributeReaders { get; }
            = new Dictionary<string, AttributeReader>
            {
                [Vim.G3dNext.Attributes.MaterialColorAttribute.AttributeName] = Vim.G3dNext.Attributes.MaterialColorAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.MaterialGlossinessAttribute.AttributeName] = Vim.G3dNext.Attributes.MaterialGlossinessAttribute.CreateAttributeReader(),
                [Vim.G3dNext.Attributes.MaterialSmoothnessAttribute.AttributeName] = Vim.G3dNext.Attributes.MaterialSmoothnessAttribute.CreateAttributeReader(),
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
}
