using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Vim.BFastLib;
using Vim.Format.Geometry;
using Vim.Format.ObjectModel;
using Vim.LinqArray;
using Vim.Math3d;
using Vim.Util;

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
      
        public static void ValidateDocumentModelToG3dInvariantsNext(this VimScene vim)
        {
            var g3d = vim.Document.GeometryNext;
            var errors = new List<string>();

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

                        var count = g3d.CountOf(attributeName);

                        // We don't check the relation if the attribute is optional and absent (null).
                        if (isOptional && count < 0)
                            continue;

                        var mult = attr.AttributeReferenceMultiplicity;

                        // Validate one-to-one relationships
                        if (mult == G3dAttributeReferenceMultiplicity.OneToOne && numEntities != count)
                        {
                            errors.Add($"Multiplicity Error ({mult}); the number of entities of type \"{type.Name}\" ({numEntities}) is not equal to the number of elements in the g3d attribute \"{attributeName}\" ({count})");
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
            if (vim.GetNodeCount() != vim.DocumentModel.NumNode)
                throw new VimValidationException($"The number of {nameof(VimSceneNode)} ({vim.GetNodeCount()}) does not match the number of node entities ({vim.DocumentModel.NumNode})");
        }

        public static void ValidateShapes(this VimScene vim)
        {
            var shapes = vim.Shapes;
            if (vim.GetShapeCount() != vim.DocumentModel.NumShape)
                throw new VimValidationException($"The number of {nameof(VimShapeNext)} ({vim.GetShapeCount()}) does not match the number of shape entities ({vim.DocumentModel.NumShape})");

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

            Parallel.For(0, vim.GetShapeCount(), shapeIndex =>
            {
                var shape = shapes[shapeIndex];
                var element = vim.DocumentModel.GetShapeElementIndex(shapeIndex);
                if (element < 0)
                    throw new VimValidationException($"{nameof(Element)} is null for {nameof(VimShapeNext)} {shape.Index}");
                ValidateColorDomain($"{nameof(VimShapeNext)} color", shape.Color, Vector4.Zero, Vector4.One, shape.Index);
            });
        }

        public static void Validate(this VimScene vim, VimValidationOptions options = null)
        {
            options = options ?? new VimValidationOptions();

            vim.Document.Validate();
            vim.DocumentModel.Validate(options.ObjectModelValidationOptions);

            VimMesh.FromG3d(vim.Document.GeometryNext).Validate();

            vim.ValidateDocumentModelToG3dInvariantsNext();

            vim.ValidateNodes();
            vim.ValidateShapes();
        }

        public static void ValidateEquality(this DocumentBuilder db, VimScene vim)
        {
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
