using System;
using System.Collections.Generic;
using System.Linq;
using Vim.BFast;
using Vim.Format.ObjectModel;
using Vim.G3d;
using Vim.LinqArray;
using Vim.Math3d;

namespace Vim.Format.Merge
{
    /// <summary>
    /// The rows one output table takes from one or more source files, in output order, and where every source row
    /// went (-1 = not written). Relations are re-pointed through <see cref="Map"/> when the table is written.
    /// </summary>
    public sealed class TablePlan
    {
        public string Name { get; }
        public List<int> Source { get; } = new List<int>();
        public List<int> Row { get; } = new List<int>();
        public int[][] Map { get; }

        public TablePlan(string name, IReadOnlyList<VimTables> sources)
        {
            Name = name;
            Map = new int[sources.Count][];
            for (var s = 0; s < sources.Count; ++s)
            {
                var m = new int[sources[s].NumRows(name)];
                for (var i = 0; i < m.Length; ++i)
                    m[i] = -1;
                Map[s] = m;
            }
        }

        public int Count => Source.Count;

        /// <summary>Appends a row whose content comes from (source, row); maps that source row to it.</summary>
        public int Add(int source, int row)
        {
            var i = Source.Count;
            Source.Add(source);
            Row.Add(row);
            Map[source][row] = i;
            return i;
        }

        /// <summary>Maps another source row to an existing output row (its content is not used).</summary>
        public void Alias(int source, int row, int outRow)
            => Map[source][row] = outRow;

        /// <summary>Removes the output rows for which keep is false; the rows after them move up, and Map follows.</summary>
        public int RemoveWhere(Func<int, bool> remove)
        {
            var newIndex = new int[Source.Count];
            var src = new List<int>();
            var row = new List<int>();
            for (var i = 0; i < Source.Count; ++i)
            {
                if (remove(i)) { newIndex[i] = -1; continue; }
                newIndex[i] = src.Count;
                src.Add(Source[i]);
                row.Add(Row[i]);
            }
            var removed = Source.Count - src.Count;
            if (removed == 0) return 0;
            Source.Clear(); Source.AddRange(src);
            Row.Clear(); Row.AddRange(row);
            foreach (var m in Map)
                for (var i = 0; i < m.Length; ++i)
                    if (m[i] >= 0) m[i] = newIndex[m[i]];
            return removed;
        }

        /// <summary>The output row of a source row, or -1.</summary>
        public int Lookup(int source, int row)
        {
            if (row < 0) return -1;
            var m = Map[source];
            return row < m.Length ? m[row] : -1;
        }
    }

    public sealed class RowPlanWriteStats
    {
        public int MeshesFromSource0;
        public int MeshesFromOtherSources;
        public int MeshesReused;
        public int Instances;
        public int Shapes;
        public int Materials;
    }

    /// <summary>
    /// Writes table plans over several source files into a DocumentBuilder: every column of every table (the union of
    /// the sources' columns, missing ones filled with defaults), every relation re-pointed, and the G3D geometry kept
    /// aligned with its tables (Node rows = instances, Shape rows = shapes, Material rows = materials; meshes are the
    /// ones the written instances use).
    /// </summary>
    public static class RowPlanWriter
    {
        public static DocumentBuilder Write(
            IReadOnlyList<VimTables> sources,
            IReadOnlyDictionary<string, TablePlan> plans,
            SerializableHeader header,
            bool deduplicateMeshes,
            RowPlanWriteStats stats = null)
        {
            stats = stats ?? new RowPlanWriteStats();
            var db = new DocumentBuilder(header);

            foreach (var plan in plans.Values)
            {
                if (VimConstants.ComputedTableNames.Contains(plan.Name))
                    continue;
                db.Tables.Add(plan.Name, WriteTable(sources, plans, plan));
            }

            WriteGeometry(sources, plans, db, deduplicateMeshes, stats);

            // Assets: every source's buffers, the first source winning on a repeated name.
            foreach (var src in sources)
                foreach (var asset in src.Document.Assets ?? Array.Empty<INamedBuffer>())
                    db.AddAsset(asset);

            return db;
        }

        private static EntityTableBuilder WriteTable(IReadOnlyList<VimTables> sources, IReadOnlyDictionary<string, TablePlan> plans, TablePlan plan)
        {
            var tb = new EntityTableBuilder(plan.Name);
            var tables = sources.Select(s => s.GetTable(plan.Name)).ToArray();
            var n = plan.Count;
            var src = plan.Source;
            var rows = plan.Row;

            // Index columns
            foreach (var name in tables.Where(t => t != null).SelectMany(t => t.IndexColumns.Select(c => c.Name)).Distinct().OrderBy(x => x, StringComparer.Ordinal))
            {
                var cols = tables.Select(t => t?.IndexColumns.FirstOrDefault(c => c.Name == name)?.Array).ToArray();
                var related = ColumnExtensions.GetRelatedTableNameFromColumnName(name);
                plans.TryGetValue(related, out var relatedPlan);
                var values = new int[n];
                for (var i = 0; i < n; ++i)
                {
                    var col = cols[src[i]];
                    var v = col == null ? -1 : col[rows[i]];
                    values[i] = v < 0 ? v : relatedPlan?.Lookup(src[i], v) ?? -1;
                }
                tb.AddIndexColumn(name, values);
            }

            // String columns
            foreach (var name in tables.Where(t => t != null).SelectMany(t => t.StringColumns.Select(c => c.Name)).Distinct().OrderBy(x => x, StringComparer.Ordinal))
            {
                var cols = tables.Select(t => t?.StringColumns.FirstOrDefault(c => c.Name == name)?.Array).ToArray();
                var values = new string[n];
                for (var i = 0; i < n; ++i)
                {
                    var col = cols[src[i]];
                    values[i] = col == null ? "" : sources[src[i]].Str(col[rows[i]]);
                }
                tb.AddStringColumn(name, values);
            }

            // Data columns
            foreach (var name in tables.Where(t => t != null).SelectMany(t => t.DataColumns.Select(c => c.Name)).Distinct().OrderBy(x => x, StringComparer.Ordinal))
            {
                var cols = tables.Select(t => t?.DataColumns.FirstOrDefault(c => c.Name == name)?.Data).ToArray();
                tb.AddDataColumn(name, GatherData(name.GetTypePrefix(), cols, src, rows));
            }

            return tb;
        }

        private static IBuffer GatherData(string typePrefix, Array[] cols, List<int> src, List<int> rows)
        {
            switch (typePrefix)
            {
                case VimConstants.IntColumnNameTypePrefix: return Gather<int>(cols, src, rows).ToBuffer();
                case VimConstants.LongColumnNameTypePrefix: return Gather<long>(cols, src, rows).ToBuffer();
                case VimConstants.ByteColumnNameTypePrefix: return Gather<byte>(cols, src, rows).ToBuffer();
                case VimConstants.FloatColumnNameTypePrefix: return Gather<float>(cols, src, rows).ToBuffer();
                case VimConstants.DoubleColumnNameTypePrefix: return Gather<double>(cols, src, rows).ToBuffer();
                default: throw new Exception($"Unknown data column type prefix {typePrefix}");
            }
        }

        private static T[] Gather<T>(Array[] cols, List<int> src, List<int> rows) where T : unmanaged
        {
            var typed = cols.Select(c => c as T[]).ToArray();
            var r = new T[src.Count];
            for (var i = 0; i < r.Length; ++i)
            {
                var col = typed[src[i]];
                r[i] = col == null ? default : col[rows[i]];
            }
            return r;
        }

        // ---------------------------------------------------------------------------------------------------------
        // Geometry
        // ---------------------------------------------------------------------------------------------------------

        private sealed class G3dArrays
        {
            public Vector3[] Vertices;
            public int[] Indices;
            public int[] MeshVertexOffsets, MeshIndexOffsets, MeshSubmeshOffsets;
            public int[] SubmeshIndexOffsets, SubmeshMaterials;
            public int NumMeshes;
            public Matrix4x4[] InstanceTransforms;
            public int[] InstanceMeshes, InstanceParents;
            public ushort[] InstanceFlags;
            public Vector3[] ShapeVertices;
            public int[] ShapeVertexOffsets;
            public Vector4[] ShapeColors;
            public float[] ShapeWidths;
            public Vector4[] MaterialColors;
            public float[] MaterialGlossiness, MaterialSmoothness;

            public G3dArrays(G3D g)
            {
                Vertices = g?.Vertices?.ToArray() ?? Array.Empty<Vector3>();
                Indices = g?.Indices?.ToArray() ?? Array.Empty<int>();
                MeshSubmeshOffsets = g?.MeshSubmeshOffset?.ToArray() ?? Array.Empty<int>();
                NumMeshes = MeshSubmeshOffsets.Length;
                MeshVertexOffsets = g?.MeshVertexOffsets?.ToArray() ?? Array.Empty<int>();
                MeshIndexOffsets = g?.MeshIndexOffsets?.ToArray() ?? Array.Empty<int>();
                SubmeshIndexOffsets = g?.SubmeshIndexOffsets?.ToArray() ?? Array.Empty<int>();
                SubmeshMaterials = g?.SubmeshMaterials?.ToArray() ?? Array.Empty<int>();
                InstanceTransforms = g?.InstanceTransforms?.ToArray() ?? Array.Empty<Matrix4x4>();
                InstanceMeshes = g?.InstanceMeshes?.ToArray() ?? Array.Empty<int>();
                InstanceParents = g?.InstanceParents?.ToArray() ?? Array.Empty<int>();
                InstanceFlags = g?.InstanceFlags?.ToArray() ?? Array.Empty<ushort>();
                ShapeVertices = g?.ShapeVertices?.ToArray() ?? Array.Empty<Vector3>();
                ShapeVertexOffsets = g?.ShapeVertexOffsets?.ToArray() ?? Array.Empty<int>();
                ShapeColors = g?.ShapeColors?.ToArray() ?? Array.Empty<Vector4>();
                ShapeWidths = g?.ShapeWidths?.ToArray() ?? Array.Empty<float>();
                MaterialColors = g?.MaterialColors?.ToArray() ?? Array.Empty<Vector4>();
                MaterialGlossiness = g?.MaterialGlossiness?.ToArray() ?? Array.Empty<float>();
                MaterialSmoothness = g?.MaterialSmoothness?.ToArray() ?? Array.Empty<float>();
            }

            public (int Start, int End) VertexRange(int m)
                => (MeshVertexOffsets[m], m + 1 < NumMeshes ? MeshVertexOffsets[m + 1] : Vertices.Length);

            public (int Start, int End) IndexRange(int m)
                => (MeshIndexOffsets[m], m + 1 < NumMeshes ? MeshIndexOffsets[m + 1] : Indices.Length);

            public (int Start, int End) SubmeshRange(int m)
                => (MeshSubmeshOffsets[m], m + 1 < NumMeshes ? MeshSubmeshOffsets[m + 1] : SubmeshIndexOffsets.Length);

            public (int Start, int End) ShapeRange(int s)
                => (ShapeVertexOffsets[s], s + 1 < ShapeVertexOffsets.Length ? ShapeVertexOffsets[s + 1] : ShapeVertices.Length);
        }

        private static DocumentBuilder.SubdividedMesh ToSubdividedMesh(G3dArrays g, int m, TablePlan materialPlan, int source)
        {
            var (v0, v1) = g.VertexRange(m);
            var (i0, i1) = g.IndexRange(m);
            var (s0, s1) = g.SubmeshRange(m);
            var vertices = new List<Vector3>(v1 - v0);
            for (var i = v0; i < v1; ++i) vertices.Add(g.Vertices[i]);
            var indices = new List<int>(i1 - i0);
            for (var i = i0; i < i1; ++i) indices.Add(g.Indices[i] - v0);
            var offsets = new List<int>(s1 - s0);
            var materials = new List<int>(s1 - s0);
            for (var i = s0; i < s1; ++i)
            {
                offsets.Add(g.SubmeshIndexOffsets[i] - i0);
                var mat = g.SubmeshMaterials[i];
                materials.Add(mat < 0 || materialPlan == null ? mat : materialPlan.Lookup(source, mat));
            }
            return new DocumentBuilder.SubdividedMesh(indices, vertices, offsets, materials);
        }

        private static unsafe int Bits(float f) => *(int*)&f;

        private static ulong MeshHash(DocumentBuilder.SubdividedMesh m)
        {
            const ulong prime = 1099511628211UL;
            var h = 14695981039346656037UL;
            void Add(int x) { h = (h ^ (uint)x) * prime; }
            Add(m.Vertices.Count);
            foreach (var v in m.Vertices)
            {
                Add(Bits(v.X));
                Add(Bits(v.Y));
                Add(Bits(v.Z));
            }
            Add(m.Indices.Count);
            foreach (var i in m.Indices) Add(i);
            foreach (var i in m.SubmeshesIndexOffset) Add(i);
            foreach (var i in m.SubmeshMaterials) Add(i);
            return h;
        }

        private static void WriteGeometry(IReadOnlyList<VimTables> sources, IReadOnlyDictionary<string, TablePlan> plans, DocumentBuilder db, bool dedupe, RowPlanWriteStats stats)
        {
            var g3ds = sources.Select(s => new G3dArrays(s.Geometry)).ToArray();
            plans.TryGetValue(TableNames.Material, out var materialPlan);
            plans.TryGetValue(TableNames.Node, out var nodePlan);
            plans.TryGetValue(TableNames.Shape, out var shapePlan);

            // Materials: one renderable material per Material row, from the row's source.
            if (materialPlan != null)
            {
                for (var i = 0; i < materialPlan.Count; ++i)
                {
                    var s = materialPlan.Source[i];
                    var r = materialPlan.Row[i];
                    var g = g3ds[s];
                    if (g.MaterialColors.Length == sources[s].NumRows(TableNames.Material) && r < g.MaterialColors.Length)
                    {
                        db.Materials.Add(new DocumentBuilder.Material
                        {
                            Color = g.MaterialColors[r],
                            Glossiness = r < g.MaterialGlossiness.Length ? g.MaterialGlossiness[r] : 0,
                            Smoothness = r < g.MaterialSmoothness.Length ? g.MaterialSmoothness[r] : 0,
                        });
                    }
                    else
                    {
                        double D(string col) { var d = sources[s].Data<double>(TableNames.Material, col); return d != null && r < d.Length ? d[r] : 0; }
                        db.Materials.Add(ObjectModelStore.ConvertMaterialEntityFieldsToRenderableMaterial(
                            D("double:Color.X"), D("double:Color.Y"), D("double:Color.Z"),
                            D("double:Transparency"), D("double:Glossiness"), D("double:Smoothness")));
                    }
                }
            }
            stats.Materials = db.Materials.Count;

            // Meshes used by the written instances, in source order (source 0 first, then the others).
            var meshMaps = new int[sources.Count][];
            var hashToMeshes = dedupe ? new Dictionary<ulong, List<int>>() : null;
            for (var s = 0; s < sources.Count; ++s)
            {
                var g = g3ds[s];
                var used = new bool[g.NumMeshes];
                if (nodePlan != null)
                {
                    for (var i = 0; i < nodePlan.Count; ++i)
                    {
                        if (nodePlan.Source[i] != s) continue;
                        var r = nodePlan.Row[i];
                        var mesh = r < g.InstanceMeshes.Length ? g.InstanceMeshes[r] : -1;
                        if (mesh >= 0 && mesh < used.Length) used[mesh] = true;
                    }
                }
                var map = new int[g.NumMeshes];
                for (var m = 0; m < g.NumMeshes; ++m)
                {
                    map[m] = -1;
                    if (!used[m]) continue;
                    var sm = ToSubdividedMesh(g, m, materialPlan, s);
                    if (dedupe)
                    {
                        var h = MeshHash(sm);
                        if (s > 0 && hashToMeshes.TryGetValue(h, out var candidates))
                        {
                            var found = -1;
                            foreach (var c in candidates)
                                if (db.Meshes[c].IsEquivalentTo(sm)) { found = c; break; }
                            if (found >= 0)
                            {
                                map[m] = found;
                                stats.MeshesReused++;
                                continue;
                            }
                        }
                        if (!hashToMeshes.TryGetValue(h, out var list))
                            hashToMeshes[h] = list = new List<int>();
                        list.Add(db.Meshes.Count);
                    }
                    map[m] = db.Meshes.Count;
                    db.Meshes.Add(sm);
                    if (s == 0) stats.MeshesFromSource0++; else stats.MeshesFromOtherSources++;
                }
                meshMaps[s] = map;
            }

            // Instances: one per Node row, in Node order.
            if (nodePlan != null)
            {
                for (var i = 0; i < nodePlan.Count; ++i)
                {
                    var s = nodePlan.Source[i];
                    var r = nodePlan.Row[i];
                    var g = g3ds[s];
                    if (r >= g.InstanceTransforms.Length)
                    {
                        // A Node row without an instance: keep the alignment with an empty instance.
                        db.AddInstance(Matrix4x4.Identity, -1, 0);
                        continue;
                    }
                    var mesh = g.InstanceMeshes[r];
                    var parent = r < g.InstanceParents.Length ? g.InstanceParents[r] : -1;
                    db.Instances.Add(new DocumentBuilder.Instance
                    {
                        Transform = g.InstanceTransforms[r],
                        MeshIndex = mesh < 0 || mesh >= meshMaps[s].Length ? -1 : meshMaps[s][mesh],
                        ParentIndex = parent < 0 ? -1 : nodePlan.Lookup(s, parent),
                        InstanceFlags = (InstanceFlags)(r < g.InstanceFlags.Length ? g.InstanceFlags[r] : (ushort)0),
                    });
                }
            }
            stats.Instances = db.Instances.Count;

            // Shapes: one per Shape row, in Shape order.
            if (shapePlan != null)
            {
                for (var i = 0; i < shapePlan.Count; ++i)
                {
                    var s = shapePlan.Source[i];
                    var r = shapePlan.Row[i];
                    var g = g3ds[s];
                    if (r >= g.ShapeVertexOffsets.Length)
                    {
                        db.Shapes.Add(new DocumentBuilder.Shape { Vertices = new List<Vector3>(), Color = Vector4.Zero, Width = 0 });
                        continue;
                    }
                    var (a, b) = g.ShapeRange(r);
                    var pts = new List<Vector3>(b - a);
                    for (var k = a; k < b; ++k) pts.Add(g.ShapeVertices[k]);
                    db.Shapes.Add(new DocumentBuilder.Shape
                    {
                        Vertices = pts,
                        Color = r < g.ShapeColors.Length ? g.ShapeColors[r] : Vector4.Zero,
                        Width = r < g.ShapeWidths.Length ? g.ShapeWidths[r] : 0,
                    });
                }
            }
            stats.Shapes = db.Shapes.Count;
        }
    }
}
