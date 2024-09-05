using System.Linq;
using Vim.Math3d;

namespace Vim.Format.Merge
{
    public class MergeConfigVimScenes
    {
        /// <summary>
        /// The input VIM scenes and their transforms.
        /// </summary>
        public (VimScene VimScene, Matrix4x4 Transform)[] InputVimScenesAndTransforms { get; }

        /// <summary>
        /// The input VIM scenes
        /// </summary>
        public VimScene[] InputVimScenes
            => InputVimScenesAndTransforms.Select(t => t.VimScene).ToArray();

        /// <summary>
        /// The input VIM scene transforms
        /// </summary>
        public Matrix4x4[] InputTransforms
            => InputVimScenesAndTransforms.Select(t => t.Transform).ToArray();

        /// <summary>
        /// Constructor.
        /// </summary>
        public MergeConfigVimScenes((VimScene VimScene, Matrix4x4 Transform)[] inputVimScenesAndTransforms)
            => InputVimScenesAndTransforms = inputVimScenesAndTransforms;

        /// <summary>
        /// Constructor. Applies an identity matrix to the input VimScenes
        /// </summary>
        public MergeConfigVimScenes(VimScene[] vimScenes)
            : this(vimScenes.Select(v => (v, Matrix4x4.Identity)).ToArray())
        { }
    }
}
