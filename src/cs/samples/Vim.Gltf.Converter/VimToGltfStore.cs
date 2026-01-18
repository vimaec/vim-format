using SharpGLTF.Schema2;
using SharpGLTF.Validation;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Vim.Format;
using Vim.BFast;

namespace Vim.Gltf.Converter
{
    /// <summary>
    /// A bare-bones VIM to GLTF converter.
    /// </summary>
    public class VimToGltfStore
    {
        /// <summary>
        /// Converts the given VIM file into a GLTF (.glb) file.
        /// </summary>
        public static void Convert(string vimFilePath, string gltfFilePath, float scale = (float) Util.Units.FeetToMetersRatio, bool validateGltf = true)
        {
            var vim = VIM.Open(vimFilePath);
            var gd = vim.GeometryData;

            var gltfModel = ModelRoot.CreateModel();
            var gltfScene = gltfModel.UseScene(0);

            // foreach (var vimMaterial in vim.Materials.ToEnumerable())
            for (var i = 0; i < vim.GeometryData.MaterialCount; ++i)
            {
                var vimMaterialColor = gd.MaterialColors[i];

                var gltfMaterial = gltfModel.CreateMaterial();
                gltfMaterial.InitializePBRMetallicRoughness();

                var maybeColorChannel = gltfMaterial.FindChannel("BaseColor");
                if (maybeColorChannel == null)
                {
                    Debug.Fail("New material 'BaseColor' channel not found.");
                    continue;
                }

                var colorChannel = maybeColorChannel.Value;
                colorChannel.Color = new System.Numerics.Vector4(vimMaterialColor.X, vimMaterialColor.Y, vimMaterialColor.Z, vimMaterialColor.W);

                if (vimMaterialColor.W < 1)
                {
                    gltfMaterial.Alpha = AlphaMode.BLEND;
                }
            }

            // Initialize a flat vertex buffer.
            var vimVertexBufferBytes = gd.Vertices.SelectMany(v => new [] { v.X, v.Y, v.Z }).ToArray().ToBytes();
            var gltfVertexBufferView = gltfModel.UseBufferView(vimVertexBufferBytes);
            var gltfVertexAccessor = gltfModel.CreateAccessor("VERTEX_ACCESSOR");
            gltfVertexAccessor.SetVertexData(gltfVertexBufferView, 0, gd.VertexCount);

            // Initialize a flat index buffer.
            var vimIndexBufferBytes = gd.Indices.Select(i => (uint)i).ToArray().ToBytes();
            var gltfIndexBufferView = gltfModel.UseBufferView(vimIndexBufferBytes);

            // Create the meshes and their primitives (submeshes)
            var meshSubmeshCounts = gd.MeshSubmeshCount;
            var meshSubmeshOffsets = gd.MeshSubmeshOffsets;
            for (var meshIndex = 0; meshIndex < meshSubmeshCounts.Length; ++meshIndex)
            {
                var meshSubmeshCount = meshSubmeshCounts[meshIndex];
                if (meshSubmeshCount <= 0)
                    continue;

                var meshSubmeshOffset = meshSubmeshOffsets[meshIndex];

                var gltfMesh = gltfModel.CreateMesh();
                
                for (var submeshIndex = meshSubmeshOffset; submeshIndex < meshSubmeshOffset + meshSubmeshCount; ++submeshIndex)
                {
                    var submeshIndexOffset = gd.SubmeshIndexOffsets[submeshIndex];
                    var submeshIndexCount = gd.SubmeshIndexCount[submeshIndex];
                    var submeshMaterialIndex = gd.SubmeshMaterials[submeshIndex];
                    
                    var gltfPrimitive = gltfMesh.CreatePrimitive();
                    gltfPrimitive.SetVertexAccessor("POSITION", gltfVertexAccessor);

                    var gltfIndexAccessor = gltfModel.CreateAccessor("INDEX_ACCESSOR");
                    gltfIndexAccessor.SetIndexData(gltfIndexBufferView, sizeof(uint) * submeshIndexOffset, submeshIndexCount, IndexEncodingType.UNSIGNED_INT);
                    gltfPrimitive.SetIndexAccessor(gltfIndexAccessor);

                    if (submeshMaterialIndex >= 0 && submeshMaterialIndex < gltfModel.LogicalMaterials.Count)
                        gltfPrimitive.Material = gltfModel.LogicalMaterials[submeshMaterialIndex];
                }
            }

            // Create the nodes and their transforms.
            var instanceTransforms = gd.InstanceTransforms.Select(t => ConvertToGltfMatrix(t, scale)).ToArray();
            var instanceMeshIndices = gd.InstanceMeshes;
            var instanceFlags = gd.InstanceFlags;

            for (var i = 0; i < instanceTransforms.Length; ++i)
            {
                var instanceMeshIndex = instanceMeshIndices[i];
                if (instanceMeshIndex < 0 || instanceMeshIndex >= gltfModel.LogicalMeshes.Count)
                    continue;

                var instanceIsHidden = (((InstanceFlags) instanceFlags[i]) & InstanceFlags.Hidden) == InstanceFlags.Hidden;
                if (instanceIsHidden)
                    continue;

                var gltfNode = gltfScene.CreateNode();
                gltfNode.Mesh = gltfModel.LogicalMeshes[instanceMeshIndex];

                var instanceTransform = instanceTransforms[i];
                gltfNode.WorldMatrix = instanceTransform;
            }

            // Prepare the destination file
            Util.IO.Delete(gltfFilePath);
            Util.IO.CreateFileDirectory(gltfFilePath);

            // Save the model as a GLB.
            gltfModel.SaveGLB(gltfFilePath, new WriteSettings
            {
                Validation = validateGltf ? ValidationMode.Strict : ValidationMode.Skip
            });
        }

        // VIM TO GLTF AXIS SWAP
        private static readonly System.Numerics.Matrix4x4 AxisSwap = new System.Numerics.Matrix4x4(
            m11: 1, m12: 0, m13: 0, m14: 0,
            m21: 0, m22: 0, m23: -1, m24: 0,
            m31: 0, m32: 1, m33: 0, m34: 0,
            m41: 0, m42: 0, m43: 0, m44: 1);

        private static System.Numerics.Matrix4x4 ConvertToGltfMatrix(Vim.Math3d.Matrix4x4 a, float scale)
        {
            var converted = new System.Numerics.Matrix4x4(
                m11: a.M11, m12: a.M12, m13: a.M13, m14: a.M14,
                m21: a.M21, m22: a.M22, m23: a.M23, m24: a.M24,
                m31: a.M31, m32: a.M32, m33: a.M33, m34: a.M34,
                m41: a.M41, m42: a.M42, m43: a.M43, m44: a.M44) * AxisSwap * Matrix4x4.CreateScale(scale);

            // Force the last column to be the identity to pass the GLTF validator.
            converted.M14 = 0;
            converted.M24 = 0;
            converted.M34 = 0;
            converted.M44 = 1;

            return converted;
        }
    }
}
