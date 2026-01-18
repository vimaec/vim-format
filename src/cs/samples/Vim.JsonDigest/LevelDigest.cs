using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Vim.Format;

namespace Vim.JsonDigest
{
    public class LevelDigest
    {
        /// <summary>
        /// The index of the level in the VIM scene.
        /// </summary>
        public int VimIndex { get; set; }

        /// <summary>
        /// Level element's ID.
        /// </summary>
        public long ElementId { get; set; }

        /// <summary>
        /// Level element's unique ID.
        /// </summary>
        public string ElementUniqueId { get; set; }

        /// <summary>
        /// The reference to the element which contains the parameters for this level.
        /// </summary>
        public int Ref_ElementDigest_VimIndex { get; set; }

        /// <summary>
        /// Level name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Level's BIM document name.
        /// </summary>
        public string BimDocumentName { get; set; }

        /// <summary>
        /// Level elevation.
        /// </summary>
        public double Elevation { get; set; }

        /// <summary>
        /// Level project elevation.
        /// </summary>
        public double ProjectElevation { get; set; }

        /// <summary>
        /// JSON Constructor.
        /// </summary>
        [JsonConstructor]
        public LevelDigest() { }

        /// <summary>
        /// Returns a collection of level digests for each level in the given VIM scene.
        /// </summary>
        public static IEnumerable<LevelDigest> GetLevelDigestCollection(VIM vim)
            => vim.GetEntityTableSet().LevelTable.Select(l =>
            {
                var levelElement = l.Element;

                return new LevelDigest
                {
                    VimIndex = l.Index,
                    ElementId = levelElement.Id,
                    ElementUniqueId = levelElement.UniqueId,
                    Ref_ElementDigest_VimIndex = levelElement.Index,
                    Name = levelElement.Name,
                    BimDocumentName = levelElement.BimDocument.Name,
                    Elevation = l.Elevation,
                    ProjectElevation = l.ProjectElevation,
                };
            });
    }
}
