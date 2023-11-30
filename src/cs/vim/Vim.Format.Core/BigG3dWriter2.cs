using System.Collections.Generic;
using System.Linq;
using Vim.G3d;
using Vim.BFast;
using Vim.BFastNextNS;
using System.IO;
using static Vim.Format.DocumentBuilder;
using Vim.Math3d;

namespace Vim.Format
{
    /// <summary>
    /// This is a helper class for writing the really big G3Ds needed in a VIM
    /// </summary>
    public class BigG3dWriter2 : IBFastComponent
    {
        BFastNext bfast;

        public BigG3dWriter2(List<SubdividedMesh> meshes, List<Instance> instances, List<Shape> shapes, List<Material> materials, G3dHeader? header = null, bool useColors = false)
        {
            var totalSubmeshCount = meshes.Select(s => s.SubmeshesIndexOffset.Count).Sum();

            // Compute the Vertex offsets and index offsets 
            var meshVertexOffsets = new int[meshes.Count];
            var meshIndexOffsets = new int[meshes.Count];
            var submeshIndexOffsets = new int[totalSubmeshCount];
            var meshSubmeshOffset = new int[meshes.Count];

            var n = meshes.Count;

            for (var i = 1; i < n; ++i)
            {
                meshVertexOffsets[i] = meshVertexOffsets[i - 1] + meshes[i - 1].Vertices.Count;
                meshIndexOffsets[i] = meshIndexOffsets[i - 1] + meshes[i - 1].Indices.Count;
                meshSubmeshOffset[i] = meshSubmeshOffset[i - 1] + meshes[i - 1].SubmeshesIndexOffset.Count;
            }

            var subIndex = 0;
            var previousIndexCount = 0;
            foreach (var geo in meshes)
            {
                foreach (var sub in geo.SubmeshesIndexOffset)
                {
                    submeshIndexOffsets[subIndex++] = sub + previousIndexCount;
                }
                previousIndexCount += geo.Indices.Count;
            }

            // Compute the shape vertex offsets
            var numShapes = shapes.Count;
            var shapeVertexOffsets = new int[numShapes];
            for (var i = 1; i < numShapes; ++i)
            {
                shapeVertexOffsets[i] = shapeVertexOffsets[i - 1] + shapes[i - 1].Vertices.Count;
            }

            bfast = new BFastNext();
            bfast.SetArray("Meta", G3dHeader.Default.ToBytes());

            bfast.SetEnumerable(CommonAttributes.Position, () => meshes.SelectMany(m => m.Vertices));
            bfast.SetEnumerable(CommonAttributes.Index, () => meshes.SelectMany(m => m.Indices));
            bfast.SetEnumerable(CommonAttributes.MeshSubmeshOffset, () => meshSubmeshOffset);
            bfast.SetEnumerable(CommonAttributes.SubmeshIndexOffset, () => submeshIndexOffsets);
            bfast.SetEnumerable(CommonAttributes.SubmeshMaterial, () => meshes.SelectMany(s => s.SubmeshMaterials));
            bfast.SetEnumerable(CommonAttributes.InstanceFlags, () => instances.Select(i => (ushort)i.InstanceFlags));
            bfast.SetEnumerable(CommonAttributes.InstanceParent, () => instances.Select(i => i.ParentIndex));
            bfast.SetEnumerable(CommonAttributes.InstanceMesh, () => instances.Select(i => i.MeshIndex));
            bfast.SetEnumerable(CommonAttributes.InstanceTransform, () => instances.Select(i => i.Transform));
            bfast.SetEnumerable(CommonAttributes.ShapeVertex, () => shapes.SelectMany(s => s.Vertices));
            bfast.SetEnumerable(CommonAttributes.ShapeVertexOffset, () => shapeVertexOffsets);
            bfast.SetEnumerable(CommonAttributes.ShapeColor, () => shapes.Select(s => s.Color));
            bfast.SetEnumerable(CommonAttributes.ShapeWidth, () => shapes.Select(s => s.Width));
            bfast.SetEnumerable(CommonAttributes.MaterialColor, () => materials.Select(i => i.Color));
            bfast.SetEnumerable(CommonAttributes.MaterialGlossiness, () => materials.Select(i => i.Glossiness));
            bfast.SetEnumerable(CommonAttributes.MaterialSmoothness, () => materials.Select(i => i.Smoothness));
        }

        public long GetSize()
            => bfast.GetSize();

        public void Write(Stream stream) => bfast.Write(stream);
    }
}
