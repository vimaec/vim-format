﻿using System;
using System.Collections.Generic;
using Vim.BFastLib;
using Vim.LinqArray;

namespace Vim.Format
{
    public static class Validation
    {
        public static void ValidateTableRows(this Document doc)
        {
            foreach (var et in doc.EntityTables.Values)
            {
                foreach (var c in et.IndexColumns.Values)
                {
                    if (c.Array.Length != et.NumRows)
                        throw new Exception($"Expected array length {c.Array.Length} of column {c.Name} to be the same as number of rows {et.NumRows}");
                }

                foreach (var c in et.DataColumns.Values)
                {
                    if (c.NumElements() != et.NumRows)
                        throw new Exception($"Expected array length {c.NumElements()} of column {c.Name} to be the same as number of rows {et.NumRows}");
                }

                foreach (var c in et.StringColumns.Values)
                {
                    if (c.Array.Length != et.NumRows)
                        throw new Exception($"Expected array length {c.Array.Length} of column {c.Name} to be the same as number of rows {et.NumRows}");
                }
            }
        }

        public static void ValidateIndexColumns(this Document doc)
        {
            foreach (var et in doc.EntityTables.Values)
            {
                foreach (var ic in et.IndexColumns.Values)
                {
                    var table = ic.GetRelatedTable(doc);
                    if (table == null)
                        throw new Exception($"Could not find related table for index column {ic.Name}");
                }
            }
        }

        public static void ValidateAssets(this Document doc)
        {
            foreach (var asset in doc.Assets.Values)
                AssetInfo.Parse(asset.Name); // This will throw if it fails to parse.
        }

        public static void Validate(this Document doc)
        {
            doc.ValidateTableRows();
            doc.ValidateIndexColumns();
            doc.ValidateAssets();
        }

        // TODO: ValidateShapes() to validate VIM files which contain optional 2d data (shapes/overlays).
    }
}
