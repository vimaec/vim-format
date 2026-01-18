using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Vim.Format;

namespace Vim.JsonDigest
{
    /// <summary>
    /// Represents a room
    /// </summary>
    public class RoomDigest
    {
        /// <summary>
        /// The index of the room in the VIM scene.
        /// </summary>
        public int VimIndex { get; set; }

        /// <summary>
        /// Room element's ID.
        /// </summary>
        public long ElementId { get; set; }

        /// <summary>
        /// Room element's unique ID.
        /// </summary>
        public string ElementUniqueId { get; set; }

        /// <summary>
        /// The reference to the element which contains the parameters for this room.
        /// </summary>
        public int Ref_ElementDigest_VimIndex { get; set; }

        /// <summary>
        /// Room name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Room's BIM document name.
        /// </summary>
        public string BimDocumentName { get; set; }

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
        /// JSON Constructor.
        /// </summary>
        [JsonConstructor]
        public RoomDigest() { }

        /// <summary>
        /// Returns a collection of room digests for each room in the given VIM scene.
        /// </summary>
        public static IEnumerable<RoomDigest> GetRoomDigestCollection(VIM vim)
            => vim.GetEntityTableSet().RoomTable.Select(r =>
            {
                var roomElement = r.Element;

                return new RoomDigest()
                {
                    // Note: A room is an element, so we can get its BIM document, its ID, and its name from its .Element relation.
                    VimIndex = r.Index,
                    ElementId = roomElement.Id,
                    ElementUniqueId = roomElement.UniqueId,
                    Ref_ElementDigest_VimIndex = roomElement.Index,
                    BimDocumentName = roomElement.BimDocument.Name,
                    Name = roomElement.Name,
                    Number = r.Number,
                    Area = r.Area,
                    Volume = r.Volume,
                    Perimeter = r.Perimeter
                };
            });
    }
}
