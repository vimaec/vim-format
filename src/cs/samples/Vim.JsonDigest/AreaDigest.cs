using Newtonsoft.Json;
using System.Collections.Generic;
using Vim.LinqArray;

namespace Vim.JsonDigest
{
    /// <summary>
    /// Represents an area in a BIM document
    /// </summary>
    public class AreaDigest
    {
        /// <summary>
        /// The index of the area in the VIM scene
        /// </summary>
        public int VimIndex { get; set; }

        /// <summary>
        /// Area element's ID.
        /// </summary>
        public long ElementId { get; set; }

        /// <summary>
        /// Area element's unique ID.
        /// </summary>
        public string ElementUniqueId { get; set; }

        /// <summary>
        /// The reference to the element which contains the parameters for this area.
        /// </summary>
        public int Ref_ElementDigest_VimIndex { get; set; }

        /// <summary>
        /// Area name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The BIM document in which the area belongs.
        /// </summary>
        public string BimDocumentName { get; set; }

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
        public AreaDigest() { }

        /// <summary>
        /// Returns the collection of area digests for each area in the given VIM scene. Note that areas only
        /// exist in 2d views.
        /// </summary>
        public static IEnumerable<AreaDigest> GetAreaDigestCollection(VimScene vimScene)
            => vimScene.DocumentModel.AreaList.Select(a =>
            {
                var areaElement = a.Element;

                return new AreaDigest
                {
                    VimIndex = a.Index,
                    ElementId = areaElement.Id,
                    ElementUniqueId = areaElement.UniqueId,
                    Ref_ElementDigest_VimIndex = areaElement.Index,
                    Name = areaElement.Name,
                    BimDocumentName = areaElement.BimDocument.Name,
                    Number = a.Number,
                    Area = a.Value,
                    Perimeter = a.Perimeter,
                    IsGrossInterior = a.IsGrossInterior
                };
            }).ToEnumerable();
    }
}
