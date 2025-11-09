using System;
using System.Collections.Generic;
using System.IO;
using Vim.BFast;
using Vim.Util;
using Vim.LinqArray;
using System.Linq;

namespace Vim.Format.api_v2
{
    public enum VimAssetType
    {
        Texture,
        Render,
    }

    /// <summary>
    /// A VimAssetInfo is defined by a name and an VimAssetType, and takes care of parsing and generating asset buffer names.
    /// </summary>
    public class VimAssetInfo
    {
        public const string MainPng = "main.png";

        public const char Separator = '/';

        public readonly string Name;

        public readonly VimAssetType AssetType;

        public VimAssetInfo(string name, VimAssetType assetType)
            => (Name, AssetType) = (name, assetType);

        public static string[] SplitAssetBufferName(string assetBufferName)
            => assetBufferName?.Split(Separator);

        public static VimAssetInfo Parse(string assetBufferName)
        {
            // Validate that the asset buffer name can be split into two tokens.
            var tokens = SplitAssetBufferName(assetBufferName);
            if (tokens.Length != 2)
                throw new Exception($"The asset buffer name '{assetBufferName}' should be splittable into two tokens by a separator ('{Separator}'). These tokens represent: (0) the asset type, (1) the asset name.");

            // Validate the asset type token.
            if (!Enum.TryParse<VimAssetType>(tokens[0], true, out var assetType))
                throw new Exception($"The first token '{assetType}' in the asset buffer name '{assetBufferName}' is not a recognized asset type.");

            // Validate the asset name token.
            var name = tokens[1];
            if (string.IsNullOrEmpty(name))
                throw new Exception($"The second token in the asset buffer name '{assetBufferName}' is null or empty.");

            return new VimAssetInfo(name, assetType);
        }

        public static bool TryParse(string assetBufferName, out VimAssetInfo assetInfo)
        {
            assetInfo = null;
            try
            {
                assetInfo = Parse(assetBufferName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string AssetTypeToString(VimAssetType assetType)
            => assetType.ToString("G").ToLowerInvariant();

        public string AssetTypeString
            => AssetTypeToString(AssetType);

        public override string ToString()
            => $"{AssetTypeString}{Separator}{Name}";

        public string BufferName
            => ToString();

        public string GetDefaultAssetFilePathInDirectory(DirectoryInfo directoryInfo)
            => Path.Combine(directoryInfo.FullName, AssetTypeString.ToValidFileName(), Name.ToValidFileName());


        public static IEnumerable<INamedBuffer> GetTextures(IEnumerable<INamedBuffer> assetBuffers)
        {
            return assetBuffers.Where(IsTexture);
        }

        public static bool IsTexture(INamedBuffer assetBuffer)
        {
            return TryParse(assetBuffer.Name, out var assetInfo) && assetInfo.AssetType == VimAssetType.Texture;
        }

        /// <summary>
        /// Extracts the asset buffer to the file designated by the given FileInfo.
        /// </summary>
        public static FileInfo ExtractAsset(INamedBuffer assetBuffer, FileInfo fileInfo)
        {
            IO.CreateFileDirectory(fileInfo.FullName);
            using (var stream = fileInfo.Create())
                assetBuffer.Write(stream);
            return fileInfo;
        }

        /// <summary>
        /// Extracts the asset and returns a FileInfo representing the extracted asset on disk.<br/>
        /// Returns null if the asset could not be extracted.
        /// </summary>
        public static FileInfo ExtractAsset(INamedBuffer assetBuffer, DirectoryInfo directoryInfo)
            => !TryParse(assetBuffer.Name, out var assetInfo)
                ? null
                : ExtractAsset(assetBuffer, new FileInfo(assetInfo.GetDefaultAssetFilePathInDirectory(directoryInfo)));


    }
}
