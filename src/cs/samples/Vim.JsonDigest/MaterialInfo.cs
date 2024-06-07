using System.Collections.Generic;
using System.Linq;
using Vim.LinqArray;

namespace Vim.JsonDigest
{
    /// <summary>
    /// Represents a material in a BIM document
    /// </summary>
    public class MaterialInfo
    {
        /// <summary>
        /// The BIM document in which the material belongs.
        /// </summary>
        public string BimDocumentName { get; set; }

        /// <summary>
        /// Material element's ID.
        /// </summary>
        public long MaterialElementId { get; set; }

        /// <summary>
        /// Material element's unique ID.
        /// </summary>
        public string MaterialElementUniqueId { get; set; }

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
        public List<MaterialInElementInfo> MaterialInElementInfo { get; set; }

        /// <summary>
        /// Returns the collection of material infos for each material in the given VIM scene.
        /// </summary>
        public static IEnumerable<MaterialInfo> GetMaterialInfoCollection(VimScene vimScene)
        {
            var materialInfos = vimScene.DocumentModel.MaterialList.Select(m =>
                new MaterialInfo()
                {
                    BimDocumentName = m.Element.BimDocument.Name,
                    MaterialElementId = m.Element.Id,
                    MaterialElementUniqueId = m.Element.UniqueId,
                    Name = m.Element.Name,
                    MaterialCategory = m.MaterialCategory,
                    TotalArea = 0, // This will get updated below
                    TotalVolume = 0, // This will get updated below
                    MaterialInElementInfo = new List<MaterialInElementInfo>()
                }).ToArray();

            // Visit all the material in element associative objects and update the material infos we created above.
            foreach (var materialInElement in vimScene.DocumentModel.MaterialInElementList.ToEnumerable())
            {
                var material = materialInElement.Material;
                var element = materialInElement.Element;

                var materialInfo = materialInfos[material.Index];

                materialInfo.MaterialInElementInfo.Add(new MaterialInElementInfo()
                {
                    ElementId = element.Id,
                    ElementUniqueId = element.UniqueId,
                    ElementName = element.Name,
                    Area = materialInElement.Area,
                    Volume = materialInElement.Volume,
                    IsPaint = materialInElement.IsPaint
                });
            }

            // Aggregate the total areas and volumes.
            foreach (var materialInfo in materialInfos)
            {
                materialInfo.TotalArea = materialInfo.MaterialInElementInfo.Sum(m => m.Area);
                materialInfo.TotalVolume = materialInfo.MaterialInElementInfo.Sum(m => m.Volume);
            }

            return materialInfos;
        }
    }

    /// <summary>
    /// Represents an association of a material in or on an element.
    /// </summary>
    public class MaterialInElementInfo
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
