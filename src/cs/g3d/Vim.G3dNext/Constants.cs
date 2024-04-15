namespace Vim.G3dNext
{
    /// <summary>
    /// Defines method for additionnal setup after constructors in generated G3d classes.
    /// </summary>
    public interface ISetup
    {
        void Setup();
    }

    public enum MeshSection
    {
        Opaque,
        Transparent,
        All
    }
    public static class CommonAttributes
    {
        public const string ObjectFaceSize = "g3d:all:facesize:0:int32:1";
        public const string Index = "g3d:corner:index:0:int32:1";
        public const string Position = "g3d:vertex:position:0:float32:3";
        public const string VertexUv = "g3d:vertex:uv:0:float32:2";
        public const string VertexUvw = "g3d:vertex:uv:0:float32:3";
        public const string VertexNormal = "g3d:vertex:normal:0:float32:3";
        public const string VertexColor = "g3d:vertex:color:0:float32:4";
        public const string VertexColor8Bit = "g3d:vertex:color:0:int8:4";
        public const string VertexBitangent = "g3d:vertex:bitangent:0:float32:3";
        public const string VertexTangent = "g3d:vertex:tangent:0:float32:4";
        public const string VertexSelectionWeight = "g3d:vertex:weight:0:float32:1";
        public const string FaceColor = "g3d:face:color:0:float32:4";
        public const string FaceMaterial = "g3d:face:material:0:int32:1";
        public const string FaceNormal = "g3d:face:normal:0:float32:3";
        public const string MeshSubmeshOffset = "g3d:mesh:submeshoffset:0:int32:1";
        public const string InstanceTransform = "g3d:instance:transform:0:float32:16";
        public const string InstanceParent = "g3d:instance:parent:0:int32:1";
        public const string InstanceMesh = "g3d:instance:mesh:0:int32:1";
        public const string InstanceFlags = "g3d:instance:flags:0:uint16:1";
        public const string LineTangentIn = "g3d:vertex:tangent:0:float32:3";
        public const string LineTangentOut = "g3d:vertex:tangent:1:float32:3";
        public const string ShapeVertex = "g3d:shapevertex:position:0:float32:3";
        public const string ShapeVertexOffset = "g3d:shape:vertexoffset:0:int32:1";
        public const string ShapeColor = "g3d:shape:color:0:float32:4";
        public const string ShapeWidth = "g3d:shape:width:0:float32:1";
        public const string MaterialColor = "g3d:material:color:0:float32:4";
        public const string MaterialGlossiness = "g3d:material:glossiness:0:float32:1";
        public const string MaterialSmoothness = "g3d:material:smoothness:0:float32:1";
        public const string SubmeshIndexOffset = "g3d:submesh:indexoffset:0:int32:1";
        public const string SubmeshMaterial = "g3d:submesh:material:0:int32:1";
    }

    public static class Utils {
        public static bool SafeEqual<T>(this T[] a, T[] b)
        {
            if (a == null && b == null) return true;
            if (a == null) return false;
            if(b == null) return false;
            if(a.Length != b.Length) return false;
            for(var i= 0; i < a.Length; i++)
            {
                if (!a[i].Equals(b[i])) return false;
            }
            return true;
        }

        public static T SafeGet<T>(this T[] a, int i) where T : class
        {
            if (i < 0) return null;
            if (i >= a.Length) return null;
            return a[i];
        }
    }

    public static class Constants
    {
        public const string G3dPrefix = "g3d";
        public const string Separator = ":";
        public const char SeparatorChar = ':';

        public const string MetaHeaderSegmentName = "meta";
        public const long MetaHeaderSegmentNumBytes = 8; // The header is 7 bytes + 1 bytes padding.
        public const byte MetaHeaderMagicA = 0x63;
        public const byte MetaHeaderMagicB = 0xD0;

        public static readonly string[] MetaHeaderSupportedUnits = { "mm", "cm", "m\0", "km", "in", "ft", "yd", "mi" };
    }
}
