using Newtonsoft.Json;
using System.Collections.Generic;
using Vim.LinqArray;

namespace Vim.JsonDigest
{
    /// <summary>
    /// Represents an area in a BIM document
    /// </summary>
    public class AreaInfo
    {
        /// <summary>
        /// The BIM document in which the area belongs.
        /// </summary>
        public string BimDocumentName { get; set; }

        /// <summary>
        /// Area element's ID.
        /// </summary>
        public long ElementId { get; set; }

        /// <summary>
        /// Area element's unique ID.
        /// </summary>
        public string ElementUniqueId { get; set; }

        /// <summary>
        /// Area name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Area number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Area's area in square feet
        /// </summary>
        public double Area { get; set; }

        /// <summary>
        /// Area perimeter in linear feet
        /// </summary>
        public double Perimeter { get; set; }

        /// <summary>
        /// Determines whether the area is a gross interior
        /// </summary>
        public bool IsGrossInterior { get; set; }

        /// <summary>
        /// JSON Constructor.
        /// </summary>
        [JsonConstructor]
        public AreaInfo() { }

        /// <summary>
        /// Returns the collection of area infos for each area in the given VIM scene. Note that areas only
        /// exist in 2d views.
        /// </summary>
        public static IEnumerable<AreaInfo> GetAreaInfoCollection(VimScene vimScene)
            => vimScene.DocumentModel.AreaList.Select(a => new AreaInfo
            {
                // Note: An area is an element, so we can get its BIM document, its ID, and its name from its .Element relation.
                BimDocumentName = a.Element.BimDocument.Name,
                ElementId = a.Element.Id,
                ElementUniqueId = a.Element.UniqueId,
                Name = a.Element.Name,
                Number = a.Number,
                Area = a.Value,
                Perimeter = a.Perimeter,
                IsGrossInterior = a.IsGrossInterior
            }).ToEnumerable();
    }
}
