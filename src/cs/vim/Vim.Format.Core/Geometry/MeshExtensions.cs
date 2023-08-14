using System;
using System.Collections.Generic;
using System.Linq;
using Vim.G3d;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim.Format.Geometry
{
    public static class MeshExtensions
    {
        public static IMesh ToIMesh(this IArray<GeometryAttribute> self)
            => self.ToEnumerable().ToIMesh();

        public static IMesh ToIMesh(this IEnumerable<GeometryAttribute> self)
        {
            var tmp = new GeometryAttributes(self);
            switch (tmp.NumCornersPerFace)
            {
                case 3:
                    return new TriMesh(tmp.Attributes.ToEnumerable());
                case 4:
                    return new QuadMesh(tmp.Attributes.ToEnumerable()).ToTriMesh();
                default:
                    throw new Exception($"Can not convert a geometry with {tmp.NumCornersPerFace} to a triangle mesh: only quad meshes");
            }
        }

        public static IMesh ToIMesh(this IGeometryAttributes g)
            => g is IMesh m ? m : g is QuadMesh q ? q.ToIMesh() : g.Attributes.ToIMesh();

        public static double Area(this IMesh mesh)
            => mesh.Triangles().Sum(t => t.Area);

        public static IArray<int> GetFaceMaterials(this IMesh mesh)
        {
            // SubmeshIndexOffsets: [0, A, B]
            // SubmeshIndexCount:   [X, Y, Z]
            // SubmeshMaterials:    [L, M, N]
            // ---
            // FaceMaterials:       [...Repeat(L, X / 3), ...Repeat(M, Y / 3), ...Repeat(N, Z / 3)] <-- divide by 3 for the number of corners per Triangular face
            var numCornersPerFace = mesh.NumCornersPerFace;
            return mesh.SubmeshIndexCount
                .ToEnumerable()
                .SelectMany((indexCount, i) => Enumerable.Repeat(mesh.SubmeshMaterials[i], indexCount / numCornersPerFace))
                .ToIArray();
        }

        public static IMesh Merge(this IArray<IMesh> meshes)
            => meshes.Select(m => (IGeometryAttributes)m).Merge().ToIMesh();

        public static IMesh Merge(this IEnumerable<IMesh> meshes)
            => meshes.ToIArray().Merge();

        public static IMesh Merge(this IMesh mesh, params IMesh[] others)
        {
            var gs = others.ToList();
            gs.Insert(0, mesh);
            return gs.Merge();
        }

        public static bool GeometryEquals(this IMesh mesh, IMesh other, float tolerance = Math3d.Constants.Tolerance)
        {
            if (mesh.NumFaces != other.NumFaces)
                return false;
            return mesh.Triangles().Zip(other.Triangles(), (t1, t2) => t1.AlmostEquals(t2, tolerance)).All(x => x);
        }

        public static IGeometryAttributes ReverseWindingOrder(this IMesh mesh)
        {
            var n = mesh.Indices.Count;
            var r = new int[n];
            for (var i = 0; i < n; i += 3)
            {
                r[i + 0] = mesh.Indices[i + 2];
                r[i + 1] = mesh.Indices[i + 1];
                r[i + 2] = mesh.Indices[i + 0];
            }
            return mesh.SetAttribute(r.ToIArray().ToIndexAttribute());
        }
        
        public static AABox BoundingBox(this IMesh mesh)
            => AABox.Create(mesh.Vertices.ToEnumerable());

        public static Vector3 Center(this IMesh mesh)
            => mesh.BoundingBox().Center;

        public static Int3 FaceVertexIndices(this IMesh mesh, int faceIndex)
            => new Int3(mesh.Indices[faceIndex * 3], mesh.Indices[faceIndex * 3 + 1], mesh.Indices[faceIndex * 3 + 2]);

        public static Triangle VertexIndicesToTriangle(this IMesh mesh, Int3 indices)
            => new Triangle(mesh.Vertices[indices.X], mesh.Vertices[indices.Y], mesh.Vertices[indices.Z]);

        public static Triangle Triangle(this IMesh mesh, int face)
            => mesh.VertexIndicesToTriangle(mesh.FaceVertexIndices(face));

        public static IArray<Triangle> Triangles(this IMesh mesh)
            => mesh.NumFaces.Select(mesh.Triangle);

        public static IArray<Line> GetAllEdgesAsLines(this IMesh mesh)
            => mesh.Triangles().SelectMany(tri => Tuple.Create(tri.AB, tri.BC, tri.CA));
    }
}
