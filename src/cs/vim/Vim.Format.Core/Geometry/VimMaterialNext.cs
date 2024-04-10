using System;
using System.Collections.Generic;
using Vim.G3dNext;
using Vim.Math3d;

namespace Vim.Format.Geometry
{
    public class VimMaterialNext : IMaterial
    {
        public G3dVim g3d;
        public int index;

        public static IEnumerable<VimMaterialNext> FromG3d(G3dVim g3d)
        {
            for(var i =0; i < g3d.GetMaterialCount(); i++)
            {
                yield return new VimMaterialNext(g3d, i);
            }
        }

        public Vector4 Color => g3d.MaterialColors[index];
        public float Smoothness => g3d.MaterialSmoothness[index];
        public float Glossiness => g3d.MaterialGlossiness[index];
        public VimMaterialNext(G3dVim g3d, int index)
        {
            this.g3d = g3d;
            this.index = index;
        }
    }
}
