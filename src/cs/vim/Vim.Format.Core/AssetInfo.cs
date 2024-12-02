using System;
using System.Collections.Generic;
using System.IO;
using Vim.BFast;
using Vim.Util;
using Vim.LinqArray;

namespace Vim.Format
{
    public enum AssetType
    {
        Texture,
        Render,
    }

    /// <summary>
    /// An AssetInfo is defined by a name and an AssetType, and takes care of parsing and generating asset buffer names.
    /// </summary>
    public class AssetInfo
    {
        private const char Separator = '/';

        public string Name { get; private set; }

        private readonly AssetType _assetType;

        public AssetInfo(string name, AssetType assetType)
            => (Name, _assetType) = (name, assetType);

        private static string[] SplitAssetBufferName(string assetBufferName)
            => assetBufferName?.Split(Separator);

        public static AssetInfo Parse(string assetBufferName)
        {
            // Validate that the asset buffer name can be split into two tokens.
            var tokens = SplitAssetBufferName(assetBufferName);
            if (tokens.Length != 2)
                throw new Exception($"The asset buffer name '{assetBufferName}' should be splittable into two tokens by a separator ('{Separator}'). These tokens represent: (0) the asset type, (1) the asset name.");

            // Validate the asset type token.
            if (!Enum.TryParse<AssetType>(tokens[0], true, out var assetType))
                throw new Exception($"The first token '{assetType}' in the asset buffer name '{assetBufferName}' is not a recognized asset type.");

            // Validate the asset name token.
            var name = tokens[1];
            if (string.IsNullOrEmpty(name))
                throw new Exception($"The second token in the asset buffer name '{assetBufferName}' is null or empty.");

            return new AssetInfo(name, assetType);
        }

        public static bool TryParse(string assetBufferName, out AssetInfo assetInfo)
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

        private static string AssetTypeToString(AssetType assetType)
            => assetType.ToString("G").ToLowerInvariant();

        private string AssetTypeString
            => AssetTypeToString(_assetType);

        public override string ToString()
            => $"{AssetTypeString}{Separator}{Name}";

        public string BufferName
            => ToString();
        
        public string GetDefaultAssetFilePathInDirectory(DirectoryInfo directoryInfo)
            => Path.Combine(directoryInfo.FullName, AssetTypeString.ToValidFileName(), Name.ToValidFileName());
    }

    public static class AssetInfoExtensions
    {
        private static INamedBuffer GetAssetBuffer(this Document doc, string assetBufferName)
            => doc.Assets.GetOrDefault(assetBufferName);

        /// <summary>
        /// Extracts the asset buffer to the file designated by the given FileInfo.
        /// </summary>
        private static FileInfo ExtractAsset(this INamedBuffer assetBuffer, FileInfo fileInfo)
        {
            Util.IO.CreateFileDirectory(fileInfo.FullName);
            using (var stream = fileInfo.Create())
                assetBuffer.Write(stream);
            return fileInfo;
        }

        /// <summary>
        /// Extracts the asset and returns a FileInfo representing the extracted asset on disk.<br/>
        /// Returns null if the asset could not be extracted.
        /// </summary>
        private static FileInfo ExtractAsset(this INamedBuffer assetBuffer, DirectoryInfo directoryInfo)
            => !AssetInfo.TryParse(assetBuffer.Name, out var assetInfo)
                ? null
                : assetBuffer.ExtractAsset(new FileInfo(assetInfo.GetDefaultAssetFilePathInDirectory(directoryInfo)));

        /// <summary>
        /// Extracts the assets contained in the Document to the given directory.
        /// </summary>
        public static IEnumerable<(string assetBufferName, FileInfo assetFileInfo)> ExtractAssets(this Document doc, DirectoryInfo directoryInfo)
        {
            var result = new List<(string assetBufferName, FileInfo assetFileInfo)>();
            foreach (var assetBuffer in doc.Assets.Values.ToEnumerable())
            {
                var assetBufferName = assetBuffer.Name;
                var assetFilePath = assetBuffer.ExtractAsset(directoryInfo);
                result.Add((assetBufferName, assetFilePath));
            }
            return result;
        }

        /// <summary>
        /// Gets the byte array which defines the given asset. Returns false if the asset was not found or if the byte array is empty or null.
        /// </summary>
        private static bool TryGetAssetBytes(this Document doc, string assetBufferName, out byte[] bytes)
        {
            bytes = null;

            var buffer = doc.GetAssetBuffer(assetBufferName);
            if (!(buffer is NamedBuffer<byte> byteBuffer))
                return false;

            bytes = byteBuffer.Array;

            return bytes != null && bytes.Length > 0;
        }

        /// <summary>
        /// Gets the byte array which defines the given asset. Returns false if the asset was not found or if the byte array is empty or null.
        /// </summary>
        private static bool TryGetAssetBytes(this Document doc, AssetType assetType, string assetName, out byte[] bytes)
            => doc.TryGetAssetBytes(new AssetInfo(assetName, assetType).ToString(), out bytes);

        /// <summary>
        /// Gets the byte array which defines the main image asset. Returns false if the asset was not found or if the byte array is empty or null.
        /// </summary>
        public static bool TryGetMainImageBytes(this Document doc, out byte[] bytes)
            => doc.TryGetAssetBytes(AssetType.Render, VimConstants.MainPng, out bytes);
    }
}
