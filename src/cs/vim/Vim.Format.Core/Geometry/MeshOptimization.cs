using System.Collections.Generic;
using System.Linq;
using Vim.G3d;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim.Format.Geometry
{ 

    /// <summary>
    /// This class is used to compare quickly two meshes within a lookup table (e.g. Dictionary, HashTable).
    /// it looks at the positions of each corner, and the number of faces, and assures that the object ID 
    /// and material IDs are the same. 
    /// When using a class within a dictionary or hash table, the equals operator is called frequently.
    /// By converting an IMesh to a MeshHash we minimize the amount of comparisons done. It becomes 
    /// possible, but highly unlikely that two different meshes would have the same hash.
    /// </summary>
    public class MeshCommonHash
    {
        public IMeshCommon Mesh;
        public float Tolerance;
        public int NumFaces;
        public int NumVertices;
        public int TopologyHash;
        public Int3 BoxExtents;
        public Int3 BoxMin;

        public int Round(float f)
            => (int)(f / Tolerance);

        public Int3 Round(Vector3 v)
            => new Int3(Round(v.X), Round(v.Y), Round(v.Z));

        public MeshCommonHash(IMeshCommon mesh, float tolerance)
        {
            Mesh = mesh;
            Tolerance = tolerance;
            NumFaces = mesh.NumFaces;
            NumVertices = mesh.NumVertices;
            TopologyHash = Hash.Combine(mesh.Indices.ToArray());
            var box = mesh.BoundingBox();
            BoxMin = Round(box.Min);
            BoxExtents = Round(box.Extent);
        }

        public override bool Equals(object obj)
            => obj is MeshCommonHash other && Equals(other);

        public bool Equals(MeshCommonHash other)
            => NumFaces == other.NumFaces
            && NumVertices == other.NumVertices
            && BoxMin.Equals(other.BoxMin)
            && BoxExtents.Equals(other.BoxExtents)
            && Mesh.GeometryEquals(other.Mesh);

        public override int GetHashCode()
            => Hash.Combine(NumFaces, NumVertices, TopologyHash, BoxMin.GetHashCode(), BoxExtents.GetHashCode());
    }

    public static class Optimization
    {
        public static Dictionary<MeshCommonHash, List<IMeshCommon>> GroupMeshesByHash(this IEnumerable<IMeshCommon> meshes, float tolerance)
         => meshes.AsParallel().GroupBy(m => new MeshCommonHash(m, tolerance)).ToDictionary(grp => grp.Key, grp => grp.ToList());
    }
}
