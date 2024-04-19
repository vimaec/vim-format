using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Vim.BFastLib;
using Vim.Format.Geometry;
using Vim.Format.ObjectModel;
using Vim.Util;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim.Format.SceneBuilder
{
    public class VimValidationOptions
    {
        public ObjectModelValidationOptions ObjectModelValidationOptions { get; set; } = new ObjectModelValidationOptions();
    }

    public static class Validation
    {
        public class VimValidationException : Exception
        {
            public VimValidationException(string message) : base(message) { }
        }

        public static void ValidateGeometry(this VimScene vim)
        {
            // Validate the packed geometry.
            vim.Document.Geometry.ToIMesh().Validate();

            // Validate the individual meshes.
            foreach (var g in vim.Meshes.ToEnumerable())
                g.Validate();
        }

        public static void ValidateDocumentModelToG3dInvariants(this VimScene vim)
        {
            var g3d = vim._SerializableDocument.Geometry;
            var errors = new List<string>();

            errors.AddRange(Vim.G3d.Validation.Validate(g3d).Select(e => e.ToString("G")));

            var entityTypesWithG3dReferences = new HashSet<(Type, G3dAttributeReferenceAttribute[])>(
                ObjectModelReflection.GetEntityTypes<Entity>()
                    .Select(t => (
                        type: t,
                        attrs: t.GetCustomAttributes(typeof(G3dAttributeReferenceAttribute))
                            .Select(a => a as G3dAttributeReferenceAttribute)
                            .ToArray()))
                    .Where(tuple => tuple.attrs.Length != 0));

            var dm = vim.DocumentModel;

            foreach (var tuple in entityTypesWithG3dReferences)
            {
                var (type, attrs) = tuple;
                var propertyName = type.Name + "List";
                if (dm.GetPropertyValue(propertyName) is IArray arr)
                {
                    var numEntities = arr.Count;

                    foreach (var attr in attrs)
                    {
                        var attributeName = attr.AttributeName;
                        var isOptional = attr.AttributeIsOptional;

                        var g3dAttribute = g3d.GetAttribute(attributeName);

                        // We don't check the relation if the attribute is optional and absent (null).
                        if (isOptional && g3dAttribute == null)
                            continue;

                        var g3dElementCount = g3dAttribute?.ElementCount ?? 0;
                        var mult = attr.AttributeReferenceMultiplicity;

                        // Validate one-to-one relationships
                        if (mult == G3dAttributeReferenceMultiplicity.OneToOne && numEntities != g3dElementCount)
                        {
                            errors.Add($"Multiplicity Error ({mult}); the number of entities of type \"{type.Name}\" ({numEntities}) is not equal to the number of elements in the g3d attribute \"{attributeName}\" ({g3dElementCount})");
                        }
                    }
                }
                else
                {
                    throw new VimValidationException($"DocumentModel.{propertyName} not found");
                }
            }

            if (errors.Count > 0)
            {
                throw new VimValidationException(
                    $"DocumentModel to G3d invariant error(s):{Environment.NewLine}{string.Join(Environment.NewLine, errors)}");
            }
        }

        public static void ValidateNodes(this VimScene vim)
        {
            if (vim.VimNodes.Count != vim.DocumentModel.NumNode)
                throw new VimValidationException($"The number of {nameof(VimSceneNode)} ({vim.VimNodes.Count}) does not match the number of node entities ({vim.DocumentModel.NumNode})");
        }

        public static void ValidateShapes(this VimScene vim)
        {
            var shapes = vim.VimShapes;
            var numShapes  = vim.DocumentModel.NumShape;
            if (shapes.Count != numShapes)
                throw new VimValidationException($"The number of {nameof(VimShape)} ({shapes.Count}) does not match the number of shape entities ({numShapes})");

            void ValidateColorDomain(string label, Vector4 value, Vector4 lowerInclusive, Vector4 upperInclusive, int index)
            {
                if (value.X < lowerInclusive.X ||
                    value.Y < lowerInclusive.Y ||
                    value.Z < lowerInclusive.Z ||
                    value.W < lowerInclusive.W ||
                    value.X > upperInclusive.X ||
                    value.Y > upperInclusive.Y ||
                    value.Z > upperInclusive.Z ||
                    value.W > upperInclusive.W)
                {
                    throw new Exception($"{label} {value} is not in the range [{lowerInclusive}..{upperInclusive}] for {index}");
                }
            }

            Parallel.For(0, numShapes, shapeIndex =>
            {
                var shape = shapes[shapeIndex];
                if (shape.ElementIndex < 0)
                    throw new VimValidationException($"{nameof(Element)} is null for {nameof(VimShape)} {shape.ShapeIndex}");
                ValidateColorDomain($"{nameof(VimShape)} color", shape.Color, Vector4.Zero, Vector4.One, shape.ShapeIndex);
            });
        }

        public static void Validate(this VimScene vim, VimValidationOptions options = null)
        {
            options = options ?? new VimValidationOptions();

            vim.Document.Validate();
            vim.DocumentModel.Validate(options.ObjectModelValidationOptions);
            vim.ValidateGeometry();
            vim.ValidateDocumentModelToG3dInvariants();
            vim.ValidateNodes();
            vim.ValidateShapes();
        }

        public static void ValidateEquality(this DocumentBuilder db, VimScene vim)
        {
            // Test the geometry both ways
            var vimGeoBuilders = vim.Meshes.Select(g => new DocumentBuilder.SubdividedMesh(
                g.Indices.ToList(),
                g.Vertices.ToList(),
                g.SubmeshIndexOffsets.ToList(),
                g.SubmeshMaterials.ToList()
            )).ToList();

            for (var i = 0; i < db.Geometry.MeshCount; ++i)
            {
                if (!db.Geometry.GetMesh(i).IsEquivalentTo(vimGeoBuilders[i]))
                    throw new VimValidationException($"{nameof(DocumentBuilder)} mesh {i} is not equivalent to {nameof(VimScene)} mesh {i}");

                if (!db.Geometry.GetMesh(i).ToIMesh().GeometryEquals(vim.Meshes[i]))
                    throw new VimValidationException($"{nameof(DocumentBuilder)} mesh {i} geometry is not equal to {nameof(VimScene)} mesh {i}");
            }

            // Test the assets.
            var dbAssetDictionary = db.Assets;
            var vimAssetDictionary = vim._SerializableDocument.Assets.ToDictionary(a => a.Name, a => a.ToBytes());
            if (!dbAssetDictionary.DictionaryEqual(vimAssetDictionary, new ArrayEqualityComparer<byte>()))
                throw new VimValidationException($"{nameof(DocumentBuilder)} assets are not equal to {nameof(VimScene)} assets");

            // Test the entity tables.
            var tableNames = new HashSet<string>(db.Tables.Values.Select(t => t.Name));
            foreach (var et in vim.Document.EntityTables.Keys.ToEnumerable())
            {
                if (!tableNames.Contains(et))
                    throw new VimValidationException($"{nameof(DocumentBuilder)} does not contain table name {et} from {nameof(VimScene)}");

                // TODO: compare entity table equality.
            }
        }
    }
}
