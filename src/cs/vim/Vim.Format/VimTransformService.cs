using System.Collections.Generic;
using System.Linq;
using Vim.Math3d;
using Vim.Util;

namespace Vim.Format
{
    public class VimTransformResult
    {
        public VimBuilder VimBuilder { get; }
        public VimEntityTableBuilder[] EntityTableBuilders { get; }
        public VimTransformResult(VimBuilder vimBuilder, VimEntityTableBuilder[] entityTableBuilders)
        {
            VimBuilder = vimBuilder;
            EntityTableBuilders = entityTableBuilders;
        }

        public void Write(string vimFilePath)
        {
            VimBuilder.Write(vimFilePath, EntityTableBuilders);
        }
    }

    public class VimTransformService
    {
        /// <summary>
        /// A string representing the application which is emitting the new VIM document.
        /// </summary>
        private readonly string _generatorString;

        /// <summary>
        /// The version of the application which is emitting the new VIM document.
        /// </summary>
        private readonly string _versionString;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="generatorString">A string representing the application which is emitting the new VIM document.</param>
        /// <param name="versionString">The version of the application which is emitting the new VIM document.</param>
        public VimTransformService(string generatorString, string versionString)
        {
            _generatorString = generatorString;
            _versionString = versionString;
        }

        /// <summary>
        /// Returns a new VIM builder in which the original meshes have been deduplicated.
        /// </summary>
        public VimTransformResult DeduplicateGeometry(VIM vim)
            => Transform(
                vim,
                _ => true,
                (_, cur) => cur,
                true);

        /// <summary>
        /// Returns a new VIM builder in which the elements and their geometry have been filtered.
        /// </summary>
        public VimTransformResult Filter(VIM vim, ElementFilter elementFilter, bool deduplicateMeshes = false)
            => Transform(
                vim,
                elementFilter,
                (_, cur) => cur,
                deduplicateMeshes);

        /// <summary>
        /// Returns a new VIM builder in which the element transforms have been multiplied by the given matrix.
        /// </summary>
        public VimTransformResult Transform(VIM vim, Matrix4x4 matrix, bool deduplicateMeshes = false)
            => Transform(
                vim,
                _ => true,
                (_, cur) => cur * matrix,
                deduplicateMeshes);

        public delegate bool ElementFilter(VimElementGeometryInfo egi);
        public delegate Matrix4x4 InstanceTransform(int instanceIndex, Matrix4x4 currentTransform);

        /// <summary>
        /// Transforms the VIM into a new VIM builder based on the given filters and transformations.
        /// </summary>
        /// <param name="vim">The VIM to transform into a new VIM builder.</param>
        /// <param name="elementFilter">Returns true if the given element should be present in the new VIM builder.</param>
        /// <param name="instanceTransform">Returns the instance's new transform (or its current transform if left unchanged).</param>
        /// <param name="deduplicateMeshes">Determines whether the mesh deduplication process is applied.</param>
        public VimTransformResult Transform(
            VIM vim,
            ElementFilter elementFilter = null,
            InstanceTransform instanceTransform = null,
            bool deduplicateMeshes = false)
        {
            var vb = new VimBuilder(_generatorString, SchemaVersion.Current, _versionString);

            var geometryData = vim.GeometryData;
            var elementGeometryInfo = vim.GetElementGeometryInfoList();

            // Filter the elements and instances we want to keep
            var oldInstanceIndexToNewInstanceIndex = new Dictionary<int, int>();
            var instanceIndicesToKeep = new List<int>();
            var elementIndicesToKeep = new HashSet<int>();
            var meshIndicesToKeep = new HashSet<int>();

            foreach (var egi in elementGeometryInfo) // Reminder: ElementGeometryInfo is 1:1 aligned with the Element table.
            {
                var keep = elementFilter?.Invoke(egi) ?? true;
                if (!keep)
                    continue;

                elementIndicesToKeep.Add(egi.ElementIndex);

                foreach (var (oldInstanceIndex, oldMeshIndex) in egi.InstanceAndMeshIndices)
                {
                    if (oldInstanceIndex != -1 && !oldInstanceIndexToNewInstanceIndex.ContainsKey(oldInstanceIndex))
                    {
                        var newInstanceIndex = instanceIndicesToKeep.Count;
                        instanceIndicesToKeep.Add(oldInstanceIndex);
                        oldInstanceIndexToNewInstanceIndex.Add(oldInstanceIndex, newInstanceIndex);
                    }

                    if (oldMeshIndex != -1 &&
                        geometryData.TryGetVimMeshView(oldMeshIndex, out var meshView) &&
                        meshView.FaceCount != 0)
                    {
                        meshIndicesToKeep.Add(oldMeshIndex);
                    }
                }
            }

            var filteredEntityTableBuilders = VimEntityTableBuilderRemapped.FilterElements(
                VimBuilder.GetVimEntityTableBuilders(vim),
                elementIndicesToKeep,
                instanceIndicesToKeep);

            var meshViewsToKeep = new List<VimMeshView>();
            var oldMeshIndexToNewMeshIndex = new Dictionary<int, int>();

            int KeepMeshView(VimMeshView meshView)
            {
                var newMeshIndex = meshViewsToKeep.Count;
                meshViewsToKeep.Add(meshView);
                oldMeshIndexToNewMeshIndex[meshView.MeshIndex] = newMeshIndex;
                return newMeshIndex;
            }

            if (deduplicateMeshes)
            {
                // Group the mesh views
                var groupedMeshes = geometryData.GroupMeshViews(meshIndicesToKeep);

                // Create the lookup from old mesh view index to common mesh view
                foreach (var g in groupedMeshes)
                {
                    var newMeshIndex = KeepMeshView(g.Key.MeshView);

                    foreach (var meshView in g)
                    {
                        oldMeshIndexToNewMeshIndex[meshView.MeshIndex] = newMeshIndex;
                    }
                }
            }
            else
            {
                foreach (var oldMeshIndex in meshIndicesToKeep)
                {
                    var meshView = geometryData.GetMeshView(oldMeshIndex);
                    if (meshView.HasValue)
                    {
                        KeepMeshView(meshView.Value);
                    }
                }
            }

            // Add the meshes.
            foreach (var meshView in meshViewsToKeep)
            {
                vb.Meshes.Add(new VimSubdividedMesh(meshView));
            }

            // Add the instances.
            foreach (var oldInstanceIndex in instanceIndicesToKeep)
            {
                var oldMeshIndex = geometryData.InstanceMeshes[oldInstanceIndex];

                var newMeshIndex = oldMeshIndex == -1
                    ? oldMeshIndex
                    : oldMeshIndexToNewMeshIndex[oldMeshIndex];

                var oldTransform = geometryData.InstanceTransforms[oldInstanceIndex];
                var newTransform = instanceTransform?.Invoke(oldInstanceIndex, oldTransform) ?? oldTransform;

                var oldParentIndex = geometryData.InstanceParents.ElementAtOrDefault(oldInstanceIndex, -1);
                var newParentIndex = oldParentIndex == -1
                    ? oldParentIndex
                    : oldInstanceIndexToNewInstanceIndex.TryGetValue(oldParentIndex, out var p) ? p : -1;

                vb.Instances.Add(new VimInstance()
                {
                    MeshIndex = newMeshIndex,
                    Transform = newTransform,
                    InstanceFlags = (InstanceFlags) geometryData.InstanceFlags[oldInstanceIndex],
                    ParentIndex = newParentIndex,
                });
            }

            // Add the materials (preserves the material indices)
            // TECH DEBT: this could be improved to remove orphaned or duplicate materials, but this would require a remapping of both the entity table and the g3d buffers.
            for (var i = 0; i < vim.GeometryData.MaterialCount; ++i)
            {
                vb.Materials.Add(new VimMaterial()
                {
                    Color = vim.GeometryData.MaterialColors.ElementAtOrDefault(i, default),
                    Glossiness = vim.GeometryData.MaterialGlossiness.ElementAtOrDefault(i, default),
                    Smoothness = vim.GeometryData.MaterialSmoothness.ElementAtOrDefault(i, default),
                });
            }

            // Add the assets
            foreach (var asset in vim.Assets)
                vb.AddAsset(asset);

            return new VimTransformResult(vb, filteredEntityTableBuilders);
        }
    }
}
