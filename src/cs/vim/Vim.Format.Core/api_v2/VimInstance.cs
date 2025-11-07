using System;
using Vim.Math3d;

namespace Vim.Format.api_v2
{
    public class VimInstance
    {
        public Matrix4x4 Transform;
        public int MeshIndex;
        public int ParentIndex;
        public InstanceFlags InstanceFlags;
    }

    [Flags]
    public enum InstanceFlags
    {
        /// <summary>
        /// Default - no instance options defined.
        /// </summary>
        None = 0,

        /// <summary>
        /// When enabled, indicates that the renderer (or the consuming application) should hide
        /// the instance by default.
        /// </summary>
        Hidden = 1,
    }
}