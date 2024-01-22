﻿
namespace Vim.G3dNext
{
    public partial class G3dMaterials
    {
        void ISetup.Setup()
        {
            // empty
        }


        public G3dMaterials(G3dVim vim)
        {
            MaterialColors = vim.MaterialColors;
            MaterialGlossiness = vim.MaterialGlossiness;
            MaterialSmoothness = vim.MaterialSmoothness;
        }
    }
}