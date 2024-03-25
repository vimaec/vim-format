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

        // Used in MeshSandboxWindows.
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

        //Used in SplitMergeDedupTest
        public static Vector3 Center(this IMesh mesh)
       => mesh.BoundingBox().Center;

        //Used in MaxVimLoader
        public static IEnumerable<(int Material, IMesh Mesh)> SplitByMaterial(this IMesh mesh)
        {
            var submeshMaterials = mesh.SubmeshMaterials;
            if (submeshMaterials == null || submeshMaterials.Count == 0)
            {
                // Base case: no submesh materials are defined on the mesh.
                return new[] { (-1, mesh) };
            }

            var submeshIndexOffets = mesh.SubmeshIndexOffsets;
            var submeshIndexCounts = mesh.SubmeshIndexCount;
            if (submeshIndexOffets == null || submeshIndexCounts == null ||
                submeshMaterials.Count <= 1 || submeshIndexOffets.Count <= 1 || submeshIndexCounts.Count <= 1)
            {
                // Base case: only one submesh material.
                return new[] { (submeshMaterials[0], mesh) };
            }

            // Example:
            //
            // ------------
            // INPUT MESH:
            // ------------
            // Vertices            [Va, Vb, Vc, Vd, Ve, Vf, Vg] <-- 7 vertices
            // Indices             [0 (Va), 1 (Vb), 2 (Vc), 1 (Vb), 2 (Vc), 3 (Vd), 4 (Ve), 5 (Vf), 6 (Vg)] <-- 3 triangles referencing the 7 vertices
            // SubmeshIndexOffsets [0, 3, 6]
            // SubmeshIndexCount   [3, 3, 3] (computed)
            // SubmeshMaterials    [Ma, Mb, Mc]
            //
            // ------------
            // OUTPUT MESHES
            // ------------
            // - MESH FOR MATERIAL Ma
            //   Vertices:             [Va, Vb, Vc]
            //   Indices:              [0, 1, 2]
            //   SubmeshIndexOffsets:  [0]
            //   SubmeshMaterials:     [Ma]
            //
            //- MESH FOR MATERIAL Mb
            //   Vertices:             [Vb, Vc, Vd]
            //   Indices:              [0, 1, 2]
            //   SubmeshIndexOffsets:  [0]
            //   SubmeshMaterials:     [Mb]
            //
            //- MESH FOR MATERIAL Mc
            //   Vertices:             [Ve, Vf, Vg]
            //   Indices:              [0, 1, 2]
            //   SubmeshIndexOffsets:  [0]
            //   SubmeshMaterials:     [Mc]

            return mesh.SubmeshMaterials
                .Select((submeshMaterial, submeshIndex) => (submeshMaterial, submeshIndex))
                .GroupBy(t => t.submeshMaterial)
                .SelectMany(g =>
                {
                    var material = g.Key;
                    var meshes = g.Select((t, _) =>
                    {
                        var submeshMaterial = t.submeshMaterial;
                        var submeshStartIndex = submeshIndexOffets[t.submeshIndex];
                        var submeshIndexCount = submeshIndexCounts[t.submeshIndex];
                        var indexSlice = mesh.Indices.Slice(submeshStartIndex, submeshStartIndex + submeshIndexCount);
                        var newVertexAttributes = mesh.VertexAttributes().Select(attr => attr.Remap(indexSlice));
                        var newIndexAttribute = indexSlice.Count.Select(i => i).ToIndexAttribute();
                        var newSubmeshIndexOffsets = 0.Repeat(1).ToSubmeshIndexOffsetAttribute();
                        var newSubmeshMaterials = submeshMaterial.Repeat(1).ToSubmeshMaterialAttribute();

                        return newVertexAttributes
                            .Concat(mesh.NoneAttributes())
                            .Concat(mesh.WholeGeometryAttributes())
                            // TODO: TECH DEBT - face, edge, and corner attributes are ignored for now.
                            .Append(newIndexAttribute)
                            .Append(newSubmeshIndexOffsets)
                            .Append(newSubmeshMaterials)
                            .ToGeometryAttributes()
                            .ToIMesh();
                    });
                    return meshes.Select(m => (material, m));
                });
        }


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

        public static AABox BoundingBox(this IMesh mesh)
            => AABox.Create(mesh.Vertices.ToEnumerable());

        public static Int3 FaceVertexIndices(this IMesh mesh, int faceIndex)
            => new Int3(mesh.Indices[faceIndex * 3], mesh.Indices[faceIndex * 3 + 1], mesh.Indices[faceIndex * 3 + 2]);

        public static Triangle VertexIndicesToTriangle(this IMesh mesh, Int3 indices)
            => new Triangle(mesh.Vertices[indices.X], mesh.Vertices[indices.Y], mesh.Vertices[indices.Z]);

        public static Triangle Triangle(this IMesh mesh, int face)
            => mesh.VertexIndicesToTriangle(mesh.FaceVertexIndices(face));

        public static IArray<Triangle> Triangles(this IMesh mesh)
            => mesh.NumFaces.Select(mesh.Triangle);

        public static IArray<Vector3> ComputedNormals(this IMesh mesh)
            => mesh.Triangles().Select(t => t.Normal);

        public static bool Planar(this IMesh mesh, float tolerance = Math3d.Constants.Tolerance)
        {
            if (mesh.NumFaces <= 1) return true;
            var normal = mesh.Triangle(0).Normal;
            return mesh.ComputedNormals().All(n => n.AlmostEquals(normal, tolerance));
        }
    }
}
