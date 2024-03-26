using System;
using System.Collections.Generic;
using System.Linq;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim.G3d
{
    public static class GeometryAttributesExtensions
    {
        public static int ExpectedElementCount(this IGeometryAttributes self, AttributeDescriptor desc)
        {
            switch (desc.Association)
            {
                case Association.assoc_all:
                    return 1;
                case Association.assoc_none:
                    return 0;
                case Association.assoc_vertex:
                    return self.NumVertices;
                case Association.assoc_face:
                    return self.NumFaces;
                case Association.assoc_corner:
                    return self.NumCorners;
                case Association.assoc_edge:
                    return self.NumCorners;
                case Association.assoc_mesh:
                    return self.NumMeshes;
                case Association.assoc_instance:
                    return self.NumInstances;
                case Association.assoc_shapevertex:
                    return self.NumShapeVertices;
                case Association.assoc_shape:
                    return self.NumShapes;
            }
            return -1;
        }

        public static GeometryAttribute<T> GetAttribute<T>(this IGeometryAttributes g, string attributeName) where T : unmanaged
            => g.GetAttribute(attributeName)?.AsType<T>();

        public static GeometryAttribute DefaultAttribute(this IGeometryAttributes self, string name)
            => self.DefaultAttribute(AttributeDescriptor.Parse(name));

        public static GeometryAttribute DefaultAttribute(this IGeometryAttributes self, AttributeDescriptor desc)
            => desc.ToDefaultAttribute(self.ExpectedElementCount(desc));

        public static IEnumerable<GeometryAttribute> NoneAttributes(this IGeometryAttributes g)
            => g.Attributes.Where(a => a.Descriptor.Association == Association.assoc_none);

        public static IEnumerable<GeometryAttribute> CornerAttributes(this IGeometryAttributes g)
            => g.Attributes.Where(a => a.Descriptor.Association == Association.assoc_corner);

        public static IEnumerable<GeometryAttribute> EdgeAttributes(this IGeometryAttributes g)
            => g.Attributes.Where(a => a.Descriptor.Association == Association.assoc_edge);

        public static IEnumerable<GeometryAttribute> FaceAttributes(this IGeometryAttributes g)
            => g.Attributes.Where(a => a.Descriptor.Association == Association.assoc_face);

        public static IEnumerable<GeometryAttribute> VertexAttributes(this IGeometryAttributes g)
            => g.Attributes.Where(a => a.Descriptor.Association == Association.assoc_vertex);

        public static IEnumerable<GeometryAttribute> WholeGeometryAttributes(this IGeometryAttributes g)
            => g.Attributes.Where(a => a.Descriptor.Association == Association.assoc_all);

        public static int FaceToCorner(this IGeometryAttributes g, int f)
            => f * g.NumCornersPerFace;

        /// <summary>
        /// Given a set of face indices, creates an array of corner indices
        /// </summary>
        public static IArray<int> FaceIndicesToCornerIndices(this IGeometryAttributes g, IArray<int> faceIndices)
            => (faceIndices.Count * g.NumCornersPerFace)
                .Select(i => g.FaceToCorner(faceIndices[i / g.NumCornersPerFace]) + i % g.NumCornersPerFace);

        public static int CornerToFace(this IGeometryAttributes g, int c)
            => c / g.NumCornersPerFace;

        public static IGeometryAttributes ToGeometryAttributes(this IEnumerable<GeometryAttribute> attributes)
            => new GeometryAttributes(attributes);

        public static IGeometryAttributes ToGeometryAttributes(this IArray<GeometryAttribute> attributes)
            => attributes.ToEnumerable().ToGeometryAttributes();

        public static IGeometryAttributes AddAttributes(this IGeometryAttributes attributes, params GeometryAttribute[] newAttributes)
            => attributes.Attributes.ToEnumerable().Concat(newAttributes).ToGeometryAttributes();

        public static GeometryAttribute GetAttributeOrDefault(this IGeometryAttributes g, string name)
            => g.GetAttribute(name) ?? g.DefaultAttribute(name);

        public static IGeometryAttributes Merge(this IArray<G3D> gs)
            => gs.Select(x => (IGeometryAttributes)x).Merge();

        public static IGeometryAttributes Merge(this IGeometryAttributes self, params IGeometryAttributes[] gs)
            => gs.ToIArray().Prepend(self).Merge();

        public static IGeometryAttributes Merge(this IArray<IGeometryAttributes> geometryAttributesArray)
        {
            if (geometryAttributesArray.Count == 0)
                return GeometryAttributes.Empty;

            var first = geometryAttributesArray[0];

            if (geometryAttributesArray.Count == 1)
                return first;
            var corners = first.NumCornersPerFace;
            if (!geometryAttributesArray.All(g => g.NumCornersPerFace == corners))
                throw new Exception("Cannot merge meshes with different numbers of corners per faces");

            // Merge all of the attributes of the different geometries
            // Except: indices, group indexes, subgeo, and instance attributes
            var attributes = first.VertexAttributes()
                .Concat(first.CornerAttributes())
                .Concat(first.EdgeAttributes())
                .Concat(first.NoneAttributes())
                .Concat(first.FaceAttributes())
                .Append(first.GetAttributeSubmeshMaterial())
                // Skip the index semantic because things get re-ordered
                .Where(attr => attr != null && attr.Descriptor.Semantic != Semantic.Index)
                .ToArray();

            // Merge the non-indexed attributes
            var others = geometryAttributesArray.Skip(1).ToEnumerable();
            var attributeList = attributes.Select(
                attr => attr.Merge(others.Select(g => g.GetAttributeOrDefault(attr.Name)))).ToList();

            // Merge the index attribute
            // numVertices:               [X],       [Y],             [Z],                   ...
            // valueOffsets:              [0],       [X],             [X+Y],                 ...
            // indices:                   [A, B, C], [D,     E,   F], [G,         H,     I], ...
            // mergedIndices:             [A, B, C], [X+D, X+E, X+F], [X+Y+G, X+Y+H, X+Y+I], ...
            var mergedIndexAttribute = geometryAttributesArray.MergeIndexedAttribute(
                ga => ga.GetAttributeIndex(),
                ga => ga.NumVertices)
                ?.ToIndexAttribute();

            if (mergedIndexAttribute != null)
                attributeList.Add(mergedIndexAttribute);

            // Merge the submesh index offset attribute
            // numCorners:                [X],       [Y],           [Z],                 ...
            // valueOffsets:              [0]        [X],           [X+Y],               ...
            // submeshIndexOffsets:       [0, A, B], [0,   C,   D], [0,       E,     F], ...
            // mergedSubmeshIndexOffsets: [0, A, B], [X, X+C, X+D], [X+Y, X+Y+E, X+Y+F], ...
            var mergedSubmeshIndexOffsetAttribute = geometryAttributesArray.MergeIndexedAttribute(
                    ga => ga.GetAttributeSubmeshIndexOffset(),
                    ga => ga.NumCorners)
                ?.ToSubmeshIndexOffsetAttribute();

            if (mergedSubmeshIndexOffsetAttribute != null)
                attributeList.Add(mergedSubmeshIndexOffsetAttribute);

            return attributeList.ToGeometryAttributes();
        }

        /// <summary>
        /// Merges the indexed attributes based on the given transformations and returns an array of integers
        /// representing the merged and offset values.
        /// </summary>
        public static int[] MergeIndexedAttribute(
            this IArray<IGeometryAttributes> geometryAttributesArray,
            Func<IGeometryAttributes, GeometryAttribute<int>> getIndexedAttributeFunc,
            Func<IGeometryAttributes, int> getValueOffsetFunc,
            int initialValueOffset = 0)
        {
            var first = geometryAttributesArray.FirstOrDefault();
            if (first == null)
                return null;

            var firstAttribute = getIndexedAttributeFunc(first);
            if (firstAttribute == null)
                return null;

            return geometryAttributesArray.MergeAttributes(
                getIndexedAttributeFunc,
                tuples =>
                {
                    var valueOffset = initialValueOffset;
                    var mergedCount = 0;

                    var merged = new int[tuples.Sum(t => t.Attribute.Data.Count)];

                    foreach (var (parent, attr) in tuples)
                    {
                        var attrData = attr.Data;
                        var attrDataCount = attr.Data.Count;

                        for (var i = 0; i < attrDataCount; ++i)
                            merged[mergedCount + i] = attrData[i] + valueOffset;

                        mergedCount += attrDataCount;
                        valueOffset += getValueOffsetFunc(parent);
                    }

                    return merged;
                });
        }

        /// <summary>
        /// Merges the attributes based on the given transformations and returns an array of merged values.
        /// </summary>
        public static T[] MergeAttributes<T>(
            this IArray<IGeometryAttributes> geometryAttributesArray,
            Func<IGeometryAttributes, GeometryAttribute<T>> getAttributeFunc,
            Func<(IGeometryAttributes Parent, GeometryAttribute<T> Attribute)[], T[]> mergeFunc) where T : unmanaged
        {
            var tuples = geometryAttributesArray
                .Select(ga => (Parent: ga, GeometryAttribute: getAttributeFunc(ga)))
                .Where(tuple => tuple.GeometryAttribute != null)
                .ToArray();

            if (tuples.Length != geometryAttributesArray.Count)
                throw new Exception("The geometry attributes array do not all contain the same attribute");

            return mergeFunc(tuples);
        }

        /// <summary>
        /// Applies a transformation function to position attributes and another to normal attributes. When deforming, we may want to
        /// apply a similar deformation to the normals. For example a matrix can change the position, rotation, and scale of a geometry,
        /// but the only changes should be to the direction of the normal, not the length.
        /// </summary>
        public static IGeometryAttributes Deform(this IGeometryAttributes g, Func<Vector3, Vector3> positionTransform, Func<Vector3, Vector3> normalTransform)
            => g.Attributes.Select(
                a =>
                    (a.Descriptor.Semantic == Semantic.Position && a is GeometryAttribute<Vector3> p) ? p.Data.Select(positionTransform).ToAttribute(a.Descriptor) :
                    (a.Descriptor.Semantic == Semantic.Normal && a is GeometryAttribute<Vector3> n) ? n.Data.Select(normalTransform).ToAttribute(a.Descriptor) :
                    a)
            .ToGeometryAttributes();

        /// <summary>
        /// Applies a deformation to points, without changing the normals. For some transformation functions this can result in incorrect normals.
        /// </summary>
        public static IGeometryAttributes Deform(this IGeometryAttributes g, Func<Vector3, Vector3> positionTransform)
            => g.Attributes.Select(
                a =>
                    (a.Descriptor.Semantic == Semantic.Position && a is GeometryAttribute<Vector3> p) ? p.Data.Select(positionTransform).ToAttribute(a.Descriptor) :
                    a)
            .ToGeometryAttributes();

        /// <summary>
        /// Applies a transformation matrix
        /// </summary>
        public static IGeometryAttributes Transform(this IGeometryAttributes g, Matrix4x4 matrix)
            => g.Deform(v => v.Transform(matrix), v => v.TransformNormal(matrix));

        public static IGeometryAttributes SetAttribute(this IGeometryAttributes self, GeometryAttribute attr)
            => self.Attributes.Where(a => !a.Descriptor.Equals(attr.Descriptor)).Append(attr).ToGeometryAttributes();

        /// <summary>
        /// Leaves the vertex buffer intact and creates a new geometry that remaps all of the group, corner, and face data.
        /// The newFaces array is a list of indices into the old face array.
        /// Note: meshes are lost.
        /// </summary>
        public static IGeometryAttributes RemapFaces(this IGeometryAttributes g, IArray<int> faceRemap)
            => g.RemapFacesAndCorners(faceRemap, g.FaceIndicesToCornerIndices(faceRemap));

        public static IEnumerable<GeometryAttribute> SetFaceSizeAttribute(this IEnumerable<GeometryAttribute> attributes, int numCornersPerFaces)
            => (numCornersPerFaces <= 0)
                   ? attributes
                   : attributes
                        .Where(attr => attr.Descriptor.Semantic != Semantic.FaceSize)
                        .Append(new[] { numCornersPerFaces }.ToObjectFaceSizeAttribute());

        /// <summary>
        /// Low-level remap function. Maps faces and corners at the same time.
        /// In some cases, this is important (e.g. triangulating quads).
        /// Note: meshes are lost.
        /// </summary>
        public static IGeometryAttributes RemapFacesAndCorners(this IGeometryAttributes g, IArray<int> faceRemap, IArray<int> cornerRemap, int numCornersPerFace = -1)
            => g.VertexAttributes()
                .Concat(g.NoneAttributes())
                .Concat(g.FaceAttributes().Select(attr => attr.Remap(faceRemap)))
                .Concat(g.EdgeAttributes().Select(attr => attr.Remap(cornerRemap)))
                .Concat(g.CornerAttributes().Select(attr => attr.Remap(cornerRemap)))
                .Concat(g.WholeGeometryAttributes())
                .SetFaceSizeAttribute(numCornersPerFace)
                .ToGeometryAttributes();

        /// <summary>
        /// Converts a quadrilateral mesh into a triangular mesh carrying over all attributes.
        /// </summary>
        public static IGeometryAttributes TriangulateQuadMesh(this IGeometryAttributes g)
        {
            if (g.NumCornersPerFace != 4) throw new Exception("Not a quad mesh");

            var cornerRemap = new int[g.NumFaces * 6];
            var faceRemap = new int[g.NumFaces * 2];
            var cur = 0;
            for (var i = 0; i < g.NumFaces; ++i)
            {
                cornerRemap[cur++] = i * 4 + 0;
                cornerRemap[cur++] = i * 4 + 1;
                cornerRemap[cur++] = i * 4 + 2;
                cornerRemap[cur++] = i * 4 + 0;
                cornerRemap[cur++] = i * 4 + 2;
                cornerRemap[cur++] = i * 4 + 3;

                faceRemap[i * 2 + 0] = i;
                faceRemap[i * 2 + 1] = i;
            }

            return g.RemapFacesAndCorners(faceRemap.ToIArray(), cornerRemap.ToIArray(), 3);
        }

        public static IGeometryAttributes CopyFaces(this IGeometryAttributes self, Func<int, bool> predicate)
            => self.RemapFaces(self.NumFaces.Select(i => i).IndicesWhere(predicate).ToIArray());

        public static IGeometryAttributes CopyFaces(this IGeometryAttributes g, int from, int count)
            => g.CopyFaces(i => i >= from && i < from + count);

        /// <summary>
        /// Updates the vertex buffer (e.g. after identifying unwanted faces) and the index
        /// buffer. Vertices are either re-ordered, removed, or deleted. Does not affect any other
        /// </summary>
        public static IGeometryAttributes RemapVertices(this IGeometryAttributes g, IArray<int> newVertices, IArray<int> newIndices)
            => (new[] { newIndices.ToIndexAttribute() }
                .Concat(
                    g.VertexAttributes()
                    .Select(attr => attr.Remap(newVertices)))
                .Concat(g.NoneAttributes())
                .Concat(g.FaceAttributes())
                .Concat(g.EdgeAttributes())
                .Concat(g.CornerAttributes())
                .Concat(g.WholeGeometryAttributes())
                )
            .ToGeometryAttributes();

        public static G3D ToG3d(this IEnumerable<GeometryAttribute> attributes, G3dHeader? header = null)
            => new G3D(attributes, header);

        public static IArray<int> IndexFlippedRemapping(this IGeometryAttributes g)
            => g.NumCorners.Select(c => ((c / g.NumCornersPerFace) + 1) * g.NumCornersPerFace - 1 - c % g.NumCornersPerFace);

        public static bool IsNormalAttribute(this GeometryAttribute attr)
            => attr.IsType<Vector3>() && attr.Descriptor.Semantic == "normal";

        public static IEnumerable<GeometryAttribute> FlipNormalAttributes(this IEnumerable<GeometryAttribute> self)
            => self.Select(attr => attr.IsNormalAttribute()
                ? attr.AsType<Vector3>().Data.Select(v => v.Inverse()).ToAttribute(attr.Descriptor)
                : attr);

        public static IGeometryAttributes FlipWindingOrder(this IGeometryAttributes g)
            => g.VertexAttributes()
                .Concat(g.NoneAttributes())
                .Concat(g.FaceAttributes())
                .Concat(g.EdgeAttributes().Select(attr => attr.Remap(g.IndexFlippedRemapping())))
                .Concat(g.CornerAttributes().Select(attr => attr.Remap(g.IndexFlippedRemapping())))
                .Concat(g.WholeGeometryAttributes())
                .FlipNormalAttributes()
                .ToGeometryAttributes();


        public static IGeometryAttributes Replace(this IGeometryAttributes self, Func<AttributeDescriptor, bool> selector, GeometryAttribute attribute)
            => self.Attributes.Where(a => !selector(a.Descriptor)).Append(attribute).ToGeometryAttributes();

        public static IGeometryAttributes Remove(this IGeometryAttributes self, Func<AttributeDescriptor, bool> selector)
            => self.Attributes.Where(a => !selector(a.Descriptor)).ToGeometryAttributes();

        public static G3D ToG3d(this IGeometryAttributes self)
            => G3D.Create(self.Attributes.ToArray());
    }
}
