using System.Collections.Generic;
using System.Linq;
using Vim.G3d;
using Vim.BFastNS;
using static Vim.Format.DocumentBuilder;
using Vim.Math3d;

namespace Vim.Format
{
    public class G3dBuilder
    {
        private readonly List<SubdividedMesh> _meshes = new List<SubdividedMesh>();
        private readonly List<Instance> _instances = new List<Instance>();
        private readonly List<Shape> _shapes = new List<Shape>();
        private readonly List<Material> _materials = new List<Material>();
     
        public void AddMesh(SubdividedMesh mesh)
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

        public void AddMaterial(Material material)
        {
            _materials.Add(material);
        }
        public int MeshCount => _meshes.Count;

        public SubdividedMesh GetMesh(int index) => _meshes[index];
        public AABox GetBox(int meshIndex)
        {
            return AABox.Create(_meshes[meshIndex].Vertices);
        }

        public int[] GetVertexCounts()
        {
            return _meshes.Select(m => m.Vertices.Count).ToArray();
        }

        public int[] GetFaceCounts()
        {
            return _meshes.Select(m => m.Indices.Count / 3).ToArray();
        }


        public BFastNS.BFast ToBFast()
        {
            var bfast = new BFastNS.BFast();
            var totalSubmeshCount = _meshes.Select(s => s.SubmeshesIndexOffset.Count).Sum();

            // Compute the Vertex offsets and index offsets 
            var meshVertexOffsets = new int[_meshes.Count];
            var meshIndexOffsets = new int[_meshes.Count];
            var submeshIndexOffsets = new int[totalSubmeshCount];
            var meshSubmeshOffset = new int[_meshes.Count];

            var n = _meshes.Count;

            for (var i = 1; i < n; ++i)
            {
                meshVertexOffsets[i] = meshVertexOffsets[i - 1] + _meshes[i - 1].Vertices.Count;
                meshIndexOffsets[i] = meshIndexOffsets[i - 1] + _meshes[i - 1].Indices.Count;
                meshSubmeshOffset[i] = meshSubmeshOffset[i - 1] + _meshes[i - 1].SubmeshesIndexOffset.Count;
            }

            var subIndex = 0;
            var previousIndexCount = 0;
            foreach (var geo in _meshes)
            {
                foreach (var sub in geo.SubmeshesIndexOffset)
                {
                    submeshIndexOffsets[subIndex++] = sub + previousIndexCount;
                }
                previousIndexCount += geo.Indices.Count;
            }

            // Compute the shape vertex offsets
            var numShapes = _shapes.Count;
            var shapeVertexOffsets = new int[numShapes];
            for (var i = 1; i < numShapes; ++i)
            {
                shapeVertexOffsets[i] = shapeVertexOffsets[i - 1] + _shapes[i - 1].Vertices.Count;
            }

            bfast = new BFastNS.BFast();
            bfast.SetArray("Meta", G3dHeader.Default.ToBytes());

            bfast.SetEnumerable(CommonAttributes.Position, () => _meshes.SelectMany(m => m.Vertices));
            bfast.SetEnumerable(CommonAttributes.Index, () => _meshes.SelectMany(m => m.Indices));
            bfast.SetEnumerable(CommonAttributes.MeshSubmeshOffset, () => meshSubmeshOffset);
            bfast.SetEnumerable(CommonAttributes.SubmeshIndexOffset, () => submeshIndexOffsets);
            bfast.SetEnumerable(CommonAttributes.SubmeshMaterial, () => _meshes.SelectMany(s => s.SubmeshMaterials));
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
