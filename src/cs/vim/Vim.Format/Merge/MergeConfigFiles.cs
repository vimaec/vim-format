using System;
using System.IO;
using System.Linq;
using Vim.Math3d;
using Vim.Util;

namespace Vim.Format.Merge
{
    public class MergeConfigFiles
    {
        /// <summary>
        /// The input VIM file paths and their transforms.
        /// </summary>
        public (string VimFilePath, Matrix4x4 Transform)[] InputVimFilePathsAndTransforms { get; }

        /// <summary>
        /// The input VIM file paths
        /// </summary>
        public string[] InputVimFilePaths
            => InputVimFilePathsAndTransforms.Select(t => t.VimFilePath).ToArray();

        /// <summary>
        /// The input VIM file path transforms
        /// </summary>
        public Matrix4x4[] InputTransforms
            => InputVimFilePathsAndTransforms.Select(t => t.Transform).ToArray();

        /// <summary>
        /// The merged VIM file path.
        /// </summary>
        public string MergedVimFilePath { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MergeConfigFiles(
            (string VimFilePath, Matrix4x4 Transform)[] inputVimFilePathsAndTransforms,
            string mergedVimFilePath)
        {
            InputVimFilePathsAndTransforms = inputVimFilePathsAndTransforms;
            MergedVimFilePath = mergedVimFilePath;
        }

        /// <summary>
        /// Throws an exception if the input configuration is invalid.
        /// </summary>
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(MergedVimFilePath))
                throw new HResultException((int) ErrorCode.VimMergeConfigFilePathIsEmpty, "Merged VIM file path is empty.");

            var emptyFilePaths = InputVimFilePathsAndTransforms.Where(t => string.IsNullOrWhiteSpace(t.VimFilePath)).ToArray();
            if (emptyFilePaths.Length > 0)
            {
                var msg = string.Join(Environment.NewLine, emptyFilePaths.Select((t, i) => $"Input VIM file path at index {i} is empty."));
                throw new HResultException((int)ErrorCode.VimMergeInputFileNotFound, msg);
            }

            var notFoundFilePaths = InputVimFilePathsAndTransforms.Where(t => !File.Exists(t.VimFilePath)).ToArray();
            if (notFoundFilePaths.Length > 0)
            {
                var msg = string.Join(Environment.NewLine, notFoundFilePaths.Select(t => $"Input VIM file not found: {t.VimFilePath}"));
                throw new HResultException((int)ErrorCode.VimMergeInputFileNotFound, msg);
            }
        }
    }
}
