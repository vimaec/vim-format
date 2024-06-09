using System.Collections.Generic;
using Vim.LinqArray;

namespace Vim.JsonDigest
{
    /// <summary>
    /// Represents a room in a BIM document
    /// </summary>
    public class RoomInfo
    {
        /// <summary>
        /// The BIM document in which the room belongs.
        /// </summary>
        public string BimDocumentName { get; set; }

        /// <summary>
        /// Room element's ID.
        /// </summary>
        public long ElementId { get; set; }

        /// <summary>
        /// Room element's unique ID.
        /// </summary>
        public string ElementUniqueId { get; set; }

        /// <summary>
        /// Room name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Room number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Room area in square feet.
        /// </summary>
        public double Area { get; set; }

        /// <summary>
        /// Room volume in cubic feet.
        /// </summary>
        public double Volume { get; set; }

        /// <summary>
        /// Room perimeter in linear feet.
        /// </summary>
        public double Perimeter { get; set; }

        /// <summary>
        /// Returns a collection of RoomInfos for each room in the given VIM scene.
        /// </summary>
        public static IEnumerable<RoomInfo> GetRoomInfoCollection(VimScene vimScene)
            => vimScene.DocumentModel.RoomList.Select(r => new RoomInfo()
            {
                // Note: A room is an element, so we can get its BIM document, its ID, and its name from its .Element relation.
                BimDocumentName = r.Element.BimDocument.Name,
                ElementId = r.Element.Id,
                ElementUniqueId = r.Element.UniqueId,
                Name = r.Element.Name,
                Number = r.Number,
                Area = r.Area,
                Volume = r.Volume,
                Perimeter = r.Perimeter
            }).ToEnumerable();
    }
}
