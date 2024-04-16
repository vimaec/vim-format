using System.Collections.Generic;
using System.Linq;
using Vim.G3dNext;
using Vim.BFastLib;
using static Vim.Format.DocumentBuilder;
using Vim.Math3d;
using Vim.Format.Geometry;

namespace Vim.Format
{
    public class G3dBuilder
    {
        private readonly List<VimMesh> _meshes = new List<VimMesh>();
        private readonly List<Instance> _instances = new List<Instance>();
        private readonly List<Shape> _shapes = new List<Shape>();
        private readonly List<IMaterial> _materials = new List<IMaterial>();
     
        public void AddMesh(VimMesh mesh)
        {
            _meshes.Add(mesh);
        }

        public void AddInstance(Instance instance)
        {
            _instances.Add(instance);
        }

        public void AddShape(Shape shape)
        {
            _shapes.Add(shape);
        }

        public void AddMaterial(IMaterial material)
        {
            _materials.Add(material);
        }

        public int InstanceCount => _instances.Count;
        public int MeshCount => _meshes.Count;
        public int MaterialCount => _materials.Count;
        public int ShapeCount => _shapes.Count;

        public VimMesh GetMesh(int index) => _meshes[index];
        public AABox GetBox(int meshIndex)
        {
            return AABox.Create(_meshes[meshIndex].vertices);
        }

        public int[] GetVertexCounts()
        {
            return _meshes.Select(m => m.vertices.Length).ToArray();
        }

        public int[] GetFaceCounts()
        {
            return _meshes.Select(m => m.NumFaces).ToArray();
        }


        public BFast ToBFast()
        {
            var bfast = new BFast();
            var totalSubmeshCount = _meshes.Select(s => s.SubmeshCount).Sum();

            // Compute the Vertex offsets and index offsets 
            var meshVertexOffsets = new int[_meshes.Count];
            var meshIndexOffsets = new int[_meshes.Count];
            var submeshIndexOffsets = new int[totalSubmeshCount];
            var meshSubmeshOffset = new int[_meshes.Count];

            var n = _meshes.Count;

            for (var i = 1; i < n; ++i)
            {
                meshVertexOffsets[i] = meshVertexOffsets[i - 1] + _meshes[i - 1].vertices.Length;
                meshIndexOffsets[i] = meshIndexOffsets[i - 1] + _meshes[i - 1].indices.Length;
                meshSubmeshOffset[i] = meshSubmeshOffset[i - 1] + _meshes[i - 1].SubmeshCount;
            }

            var subIndex = 0;
            var previousIndexCount = 0;
            foreach (var geo in _meshes)
            {
                foreach (var sub in geo.submeshIndexOffsets)
                {
                    submeshIndexOffsets[subIndex++] = sub + previousIndexCount;
                }
                previousIndexCount += geo.indices.Length;
            }

            // Compute the shape vertex offsets
            var numShapes = _shapes.Count;
            var shapeVertexOffsets = new int[numShapes];
            for (var i = 1; i < numShapes; ++i)
            {
                shapeVertexOffsets[i] = shapeVertexOffsets[i - 1] + _shapes[i - 1].Vertices.Count;
            }

            bfast.SetEnumerable(CommonAttributes.Position, () => _meshes.SelectMany(m => m.vertices));
            bfast.SetEnumerable(CommonAttributes.Index, () => _meshes.SelectMany(m => m.indices));
            bfast.SetEnumerable(CommonAttributes.MeshSubmeshOffset, () => meshSubmeshOffset);
            bfast.SetEnumerable(CommonAttributes.SubmeshIndexOffset, () => submeshIndexOffsets);
            bfast.SetEnumerable(CommonAttributes.SubmeshMaterial, () => _meshes.SelectMany(s => s.submeshMaterials));
            bfast.SetEnumerable(CommonAttributes.InstanceFlags, () => _instances.Select(i => (ushort)i.InstanceFlags));
            bfast.SetEnumerable(CommonAttributes.InstanceParent, () => _instances.Select(i => i.ParentIndex));
            bfast.SetEnumerable(CommonAttributes.InstanceMesh, () => _instances.Select(i => i.MeshIndex));
            bfast.SetEnumerable(CommonAttributes.InstanceTransform, () => _instances.Select(i => i.Transform));
            bfast.SetEnumerable(CommonAttributes.ShapeVertex, () => _shapes.SelectMany(s => s.Vertices));
            bfast.SetEnumerable(CommonAttributes.ShapeVertexOffset, () => shapeVertexOffsets);
            bfast.SetEnumerable(CommonAttributes.ShapeColor, () => _shapes.Select(s => s.Color));
            bfast.SetEnumerable(CommonAttributes.ShapeWidth, () => _shapes.Select(s => s.Width));
            bfast.SetEnumerable(CommonAttributes.MaterialColor, () => _materials.Select(i => i.Color));
            bfast.SetEnumerable(CommonAttributes.MaterialGlossiness, () => _materials.Select(i => i.Glossiness));
            bfast.SetEnumerable(CommonAttributes.MaterialSmoothness, () => _materials.Select(i => i.Smoothness));
            return bfast;
        }
    }
}
