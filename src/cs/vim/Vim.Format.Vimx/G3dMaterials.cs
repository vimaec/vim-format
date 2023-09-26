using System.IO;
using Vim.Math3d;
using Vim.G3dNext.Attributes;
using Vim.G3dNext;

namespace Vim.Format.Vimx
{
    public class G3dMaterials
    {
        public G3DNext<MaterialAttributeCollection> source;

        // materials
        public Vector4[] materialColors;
        public float[] materialGlossiness;
        public float[] materialSmoothness;

        public static G3dMaterials FromArrays(
            Vector4[] materialColors,
            float[] materialGlossiness,
            float[] materialSmoothness)
        {
            var collection = new MaterialAttributeCollection();

            collection.MaterialColorAttribute.TypedData = materialColors ?? new Vector4[0];
            collection.MaterialGlossinessAttribute.TypedData = materialGlossiness ?? new float[0];
            collection.MaterialSmoothnessAttribute.TypedData = materialSmoothness ?? new float[0];

            var g3d = new G3DNext<MaterialAttributeCollection>(collection);
            return new G3dMaterials(g3d);
        }

        public static G3dMaterials FromBFast(BFastNext.BFastNext bfast)
        {
            var g3d = G3DNext<MaterialAttributeCollection>.ReadBFast(bfast);
            return new G3dMaterials(g3d);
        }

        public G3dMaterials(G3DNext<MaterialAttributeCollection> g3d)
        {
            this.source = g3d;

            // materials
            materialColors = g3d.AttributeCollection.MaterialColorAttribute.TypedData;
            materialGlossiness = g3d.AttributeCollection.MaterialGlossinessAttribute.TypedData;
            materialSmoothness = g3d.AttributeCollection.MaterialSmoothnessAttribute.TypedData;
        }
    }
}
