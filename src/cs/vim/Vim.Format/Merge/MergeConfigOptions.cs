namespace Vim.Format.Merge
{
    public class MergeConfigOptions
    {
        /// <summary>
        /// The generator string to embed into the VIM file header
        /// </summary>
        public string GeneratorString { get; set; } = "Unknown";

        /// <summary>
        /// The version string to embed into the VIM file header.
        /// </summary>
        public string VersionString { get; set; } = "0.0.0";

        /// <summary>
        /// Preserves the BIM data
        /// </summary>
        public bool KeepBimData { get; set; } = true;

        /// <summary>
        /// Merges the given VIM files as a grid.
        /// </summary>
        public bool MergeAsGrid { get; set; } = false;

        /// <summary>
        /// Applied when merging as a grid.
        /// </summary>
        public float GridPadding { get; set; } = 0f;

        /// <summary>
        /// Deduplicates Elements and EntityWithElements based on their Element's unique ids.
        /// If the unique ID is empty, the entities are not merged.
        /// </summary>
        public bool DeduplicateEntities { get; set; } = true;
    }
}
