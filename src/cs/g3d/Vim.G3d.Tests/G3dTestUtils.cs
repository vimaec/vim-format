using Vim.Math3d;

namespace Vim.G3d.Tests
{
    public static class G3dNextTestUtils
    {
        public static G3dVim CreateTestG3d()
        {
            var g3d = new G3dVim(
                instanceTransforms: new Matrix4x4[] { Matrix4x4.Identity },
                instanceMeshes: new int[] { 0 },
                instanceParents: new int[] { -1 },
                instanceFlags: null,
                meshSubmeshOffsets: new int[] { 0 },
                submeshIndexOffsets: new int[] { 0, 3, 6 },
                submeshMaterials: new int[] { 0 },
                indices: new int[] { 0, 1, 2, 0, 3, 2, 1, 3, 2 },
                positions: new Vector3[] { Vector3.Zero, Vector3.UnitX, Vector3.UnitY, Vector3.UnitZ },
                materialColors: new Vector4[] { new Vector4(0.25f, 0.5f, 0.75f, 1) },
                materialGlossiness: new float[] { 0.95f },
                materialSmoothness: new float[] { 0.5f },
                shapeColors: null,
                shapeVertexOffsets: null,
                shapeVertices: null,
                shapeWidths: null
            );
            g3d.Validate();


            return g3d;
        }
    }
}
