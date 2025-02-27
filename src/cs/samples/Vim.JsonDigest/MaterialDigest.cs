using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Vim.LinqArray;

namespace Vim.JsonDigest
{
    /// <summary>
    /// Represents a material
    /// </summary>
    public class MaterialDigest
    {
        /// <summary>
        /// The index of the material in the VIM scene.
        /// </summary>
        public int VimIndex { get; set; }

        /// <summary>
        /// Material element's ID.
        /// </summary>
        public long ElementId { get; set; }

        /// <summary>
        /// Material element's unique ID.
        /// </summary>
        public string ElementUniqueId { get; set; }

        /// <summary>
        /// The reference to the element which contains the parameters for this material.
        /// </summary>
        public int Ref_ElementDigest_VimIndex { get; set; }

        /// <summary>
        /// Material name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Material category
        /// </summary>
        public string MaterialCategory { get; set; }

        /// <summary>
        /// Total area of the material.
        /// </summary>
        public double TotalArea { get; set; }

        /// <summary>
        /// Total volume of the material.
        /// </summary>
        public double TotalVolume { get; set; }

        /// <summary>
        /// The occurrences of materials per element.
        /// </summary>
        public List<MaterialInElementDigest> MaterialInElementDigestCollection { get; set; }

        /// <summary>
        /// JSON Constructor.
        /// </summary>
        [JsonConstructor]
        public MaterialDigest() { }

        /// <summary>
        /// Returns the collection of material digests for each material in the given VIM scene.
        /// </summary>
        public static IEnumerable<MaterialDigest> GetMaterialDigestCollection(VimScene vimScene)
        {
            // First, we initialize the collection of MaterialInfo instances based on each material in the VIM scene.
            var materialDigests = vimScene.DocumentModel.MaterialList.Select(m =>
                new MaterialDigest()
                {
                    VimIndex = m.Index,
                    ElementId = m.Element.Id,
                    ElementUniqueId = m.Element.UniqueId,
                    Ref_ElementDigest_VimIndex = m.Element.Index,
                    Name = m.Element.Name,
                    MaterialCategory = m.MaterialCategory,
                    TotalArea = 0, // This will get updated below
                    TotalVolume = 0, // This will get updated below
                    MaterialInElementDigestCollection = new List<MaterialInElementDigest>(),
                }).ToArray();

            // Next, we iterate over all the MaterialInElement associative objects and update the MaterialInfos we created above.
            foreach (var materialInElement in vimScene.DocumentModel.MaterialInElementList.ToEnumerable())
            {
                var material = materialInElement.Material;
                var element = materialInElement.Element;

                var materialInfo = materialDigests[material.Index];

                materialInfo.MaterialInElementDigestCollection.Add(new MaterialInElementDigest()
                {
                    ElementId = element.Id,
                    ElementUniqueId = element.UniqueId,
                    ElementName = element.Name,
                    Ref_ElementDigest_VimIndex = element.Index,
                    Area = materialInElement.Area,
                    Volume = materialInElement.Volume,
                    IsPaint = materialInElement.IsPaint
                });
            }

            // Finally, we sum up the total areas and volumes.
            foreach (var materialInfo in materialDigests)
            {
                materialInfo.TotalArea = materialInfo.MaterialInElementDigestCollection.Sum(m => m.Area);
                materialInfo.TotalVolume = materialInfo.MaterialInElementDigestCollection.Sum(m => m.Volume);
            }

            return materialDigests.Where(mi => mi.MaterialInElementDigestCollection.Count > 0);
        }
    }

    /// <summary>
    /// Represents an association of a material inside (or on) an element.
    /// </summary>
    public class MaterialInElementDigest
    {
        /// <summary>
        /// Associated element's ID.
        /// </summary>
        public long ElementId { get; set; }

        /// <summary>
        /// Associated element's unique ID.
        /// </summary>
        public string ElementUniqueId { get; set; }

        /// <summary>
        /// Associated element's name
        /// </summary>
        public string ElementName { get; set; }

        /// <summary>
        /// Associated element's VIM index.
        /// </summary>
        public int Ref_ElementDigest_VimIndex { get; set; }

        /// <summary>
        /// Material area in square feet.
        /// </summary>
        public double Area { get; set; }

        /// <summary>
        /// Material volume in cubic feet
        /// </summary>
        public double Volume { get; set; }

        /// <summary>
        /// Determines whether the material is applied as paint.
        /// </summary>
        public bool IsPaint { get; set; }
    }
}
