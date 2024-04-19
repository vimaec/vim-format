using System;
using System.Collections.Generic;
using Vim.BFastLib;
using Vim.G3d;
using Vim.LinqArray;

namespace Vim.Format
{
    public static class Validation
    {
        public static void ValidateTableRows(this Document doc)
        {
            foreach (var et in doc.EntityTables.Values.ToArray())
            {
                foreach (var c in et.IndexColumns.Values.ToArray())
                {
                    if (c.Array.Length != et.NumRows)
                        throw new Exception($"Expected array length {c.Array.Length} of column {c.Name} to be the same as number of rows {et.NumRows}");
                }

                foreach (var c in et.DataColumns.Values.ToArray())
                {
                    if (c.NumElements() != et.NumRows)
                        throw new Exception($"Expected array length {c.NumElements()} of column {c.Name} to be the same as number of rows {et.NumRows}");
                }

                foreach (var c in et.StringColumns.Values.ToArray())
                {
                    if (c.Array.Length != et.NumRows)
                        throw new Exception($"Expected array length {c.Array.Length} of column {c.Name} to be the same as number of rows {et.NumRows}");
                }
            }
        }

        public static void ValidateIndexColumns(this Document doc)
        {
            foreach (var et in doc.EntityTables.Values.ToArray())
            {
                foreach (var ic in et.IndexColumns.Values.ToEnumerable())
                {
                    var table = ic.GetRelatedTable(doc);
                    if (table == null)
                        throw new Exception($"Could not find related table for index column {ic.Name}");
                }
            }
        }

        public static string[] RequiredAttributeNames => new []
        {
            // Vertices
            CommonAttributes.Position,
            CommonAttributes.Index,
            
            // Meshes
            CommonAttributes.MeshSubmeshOffset,

            // Submeshes
            CommonAttributes.SubmeshIndexOffset,

            // Instances
            CommonAttributes.InstanceMesh,
            CommonAttributes.InstanceTransform,
        };

        public static void ValidateGeometryAttributes(this Document doc)
        {
            var attributes = doc.Geometry.Attributes;
            var attributeNameSet = new HashSet<string>(attributes.Select(a => a.Name).ToEnumerable());
            foreach (var attributeName in RequiredAttributeNames)
            {
                if (!attributeNameSet.Contains(attributeName))
                    throw new Exception($"Required attribute {attributeName} was not found.");
            }
        }

        public static void ValidateAssets(this Document doc)
        {
            foreach (var asset in doc.Assets.Values.ToEnumerable())
                AssetInfo.Parse(asset.Name); // This will throw if it fails to parse.
        }

        public static void Validate(this Document doc)
        {
            doc.ValidateTableRows();
            doc.ValidateIndexColumns();
            doc.ValidateGeometryAttributes();
            doc.ValidateAssets();
        }

        // TODO: ValidateShapes() to validate VIM files which contain optional 2d data (shapes/overlays).
    }
}
